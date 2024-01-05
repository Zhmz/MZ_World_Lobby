using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Google.Protobuf;
using Google.Protobuf.Collections;
using UnityEngine;

namespace Svga
{
    // 数据缓存
    public class PlayerInfo
    {
        public int MaxSize = 0;
        public Texture2D Atlas;
        public Texture2D[] textures;
        public Rect[] rects;
        public byte[] InflatedBytes;
        public MovieEntity MovieEntity;
        public Material InstancingMat;
        public Dictionary<string, Sprite> Images;
    }

    public partial class SvgaPlayer
    {
        #region Data
        public static Dictionary<string, PlayerInfo> INFO = new Dictionary<string, PlayerInfo>();
        private PlayerInfo _currInfo;
        private bool _isBatch;

        // 当前播放的svga的最大尺寸，用于创建atlas
        private int _maxSize = 0;
        // 公共的只读属性
        public bool IsBatch
        {
            get { return _isBatch; }
        }

        private Texture2D _atlas;

        public Texture2D Atlas
        {
            get { return _atlas; }
        }
        // 纹理数组，用于动态图集或传递至shader中
        private Texture2D[] _textures;
        public Texture2D[] textures
        {
            get
            {
                return _textures;
            }
        }
        // 用于记录图集元素的坐标与尺寸
        private Rect[] _rects;
        public Rect[] rects
        {
            get
            {
                return _rects;
            }
        }

        /// <summary>
        /// SVGA 文件原始二进制.
        /// </summary>
        private byte[] InflatedBytes { get; set; }

        /// <summary>
        /// MovieEntity 对象.
        /// SVGA 的所有数据将从本对象中读取.
        /// </summary>
        private MovieEntity MovieEntity { get; set; }

        /// <summary>
        /// 从 MovieEntity 中获取 SVGA 图片.
        /// MovieEntity 的 Images 属性下保存了 SVGA 的图片文件, 为一个可枚举的 byteString 集合,
        /// 键名为图片名称, 键值为 PNG 的二进制数据.
        /// </summary>
        private MapField<string, ByteString> Images => this.MovieEntity?.Images;

        private Dictionary<string, Sprite> SpriteImages;

        /// <summary>
        /// SVGA 配置参数.
        /// </summary>
        private MovieParams MovieParams => this.MovieEntity?.Params;

        /// <summary>
        /// 动画总帧数.
        /// </summary>
        private int TotalFrame => this.MovieParams?.Frames ?? 0;

        /// <summary>
        /// SVGA Sprite 对象.
        /// </summary>
        private RepeatedField<SpriteEntity> Sprites => this.MovieEntity?.Sprites;

        /// <summary>
        /// Inflate SVGA 文件, 获取其原始数据.
        /// SVGA 文件已经经过 Deflate, 所以第一步需要先进行 Inflate.
        /// </summary>
        private void InflateSvgaFile(Stream svgaFileBuffer)
        {

            byte[] inflatedBytes;

            // 微软自带的 DeflateStream 不认识文件头两个字节，SVGA 的这两字节为 78 9C，是 Deflate 的默认压缩表示字段.
            // 关于此问题请看 https://stackoverflow.com/questions/17212964/net-zlib-inflate-with-net-4-5.
            // Zlib 文件头请看 https://stackoverflow.com/questions/9050260/what-does-a-zlib-header-look-like.
            svgaFileBuffer.Seek(2, SeekOrigin.Begin);

            using (var deflatedStream = new DeflateStream(svgaFileBuffer, CompressionMode.Decompress))
            {
                using (var stream = new MemoryStream())
                {
                    deflatedStream.CopyTo(stream);
                    inflatedBytes = stream.ToArray();
                }
            }

            this.InflatedBytes = inflatedBytes;
            if (this._currInfo != null)
            {
                this._currInfo.InflatedBytes = inflatedBytes;
            }
        }

        /// <summary>
        /// 通过 Infalte 数据获取 SVGA 的 MovieEntity.
        /// </summary>
        /// <param name="inflatedBytes"></param>
        private void GetMovieEntity()
        {
            if (this.InflatedBytes != null && this._currInfo != null)
            {
                this._currInfo.MovieEntity = this.MovieEntity = MovieEntity.Parser.ParseFrom(this.InflatedBytes);
                if (IsBatch) {
                    this._currInfo.MaxSize = _maxSize = System.Math.Max(Mathf.CeilToInt(this.MovieEntity.Params.ViewBoxWidth), Mathf.CeilToInt(this.MovieEntity.Params.ViewBoxHeight));
                    this._currInfo.Atlas = this._atlas = new Texture2D(_maxSize * 2, _maxSize * 2);
                }
            }
        }

        // 根据纹理对象在图集中获取对应的位置及尺寸信息
        public Rect GetRectOfAtlas(Texture2D texture)
        {
            int index = Array.FindIndex(_textures, textureWithIndex => textureWithIndex == texture);
            return rects[index];
        }

        private void GetSpritesDic()
        {
            this._currInfo.Images = SpriteImages = new Dictionary<string, Sprite>();
            this._currInfo.textures = _textures = new Texture2D[Images.Count];
            int i = 0;
            foreach (var image in Images)
            {
                var buffer = image.Value.ToByteArray();
                var tex = new Texture2D(1, 1);
                tex.LoadImage(buffer);
                var s = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);
                SpriteImages.Add(image.Key, s);
                _textures[i] = tex;
                i++;
            }
            if(IsBatch)
            {
                this._currInfo.rects = _rects = _atlas.PackTextures(_textures, 2, _maxSize, false);
            }
        }

        /// <summary>
        /// 载入 SVGA 文件数据.
        /// </summary>
        /// <param name="svgaFileBuffer">SVGA 文件二进制 Stream.</param>
        public void LoadSvgaFileData(Stream svgaFileBuffer)
        {
            this.InflateSvgaFile(svgaFileBuffer);
            this.GetMovieEntity();
            this.GetSpritesDic();
            this.InitialView();
        }

        /// <summary>
        /// 载入 SVGA 文件数据.
        /// </summary>
        /// <param name="svgaFileBuffer">SVGA 文件二进制 Stream.</param>
        public void LoadSvgaFileData(string url, Stream svgaFileBuffer, bool isBatch = false)
        {   this._isBatch = isBatch;
            PlayerInfo info;
            if (INFO.TryGetValue(url, out info))
            {
                this._currInfo = info;
                this._atlas = info.Atlas;
                this._maxSize = info.MaxSize;
                this._textures = info.textures;
                this._rects = info.rects;
                this.InflatedBytes = info.InflatedBytes;
                this.MovieEntity = info.MovieEntity;
                this.SpriteImages = info.Images;
                this._currBatch = info.InstancingMat;
            }
            else
            {
                this._currInfo = new PlayerInfo();
                INFO.Add(url, this._currInfo);
                this.InflateSvgaFile(svgaFileBuffer);
                this.GetMovieEntity();
                this.GetSpritesDic();
            }
            this.InitialView();

        }

        #endregion
    }
}