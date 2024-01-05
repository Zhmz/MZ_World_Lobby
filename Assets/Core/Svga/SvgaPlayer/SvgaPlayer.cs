using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Svga
{
    
    public partial class SvgaPlayer : MonoBehaviour
    {
        /// <summary>
        /// 舞台是否已经初始化.
        /// </summary>
        private bool _isStageInited;
        private readonly List<ShaderPropertyController> _subSpritesList =
            new List<ShaderPropertyController>();
        public Material unBatch;
        public Material batch;
        private Material _currBatch;
        public bool autoPlay;
        public Material InstancingMat
        {
            get { return this._currBatch; }
        }

        private void InitialView()
        {
            var viewBox = new Vector2(MovieParams.ViewBoxWidth, MovieParams.ViewBoxHeight);
            GetComponent<RectTransform>().sizeDelta = viewBox;
            var sprites = Sprites;
            if (!this._currBatch) {
                this._currBatch = new Material(batch);
                this._currInfo.InstancingMat = this._currBatch;
            }
            
            for (int i = 0; i < sprites.Count; i++)
            {
                // 如果是需要代码绘制的图层，如rect，circle等，暂时先不处理
                if(!SpriteImages.ContainsKey(sprites[i].ImageKey))
                {
                    continue;
                }
                // 创建SpriteEntity实体
                var SpriteObject = new GameObject(sprites[i].ImageKey);
                ShaderPropertyController props = SpriteObject.AddComponent<ShaderPropertyController>();
                Sprite currSprite = SpriteImages[sprites[i].ImageKey];
                SpriteObject.transform.SetParent(transform);
                if(!IsBatch)
                {
                    SpriteObject.AddComponent<Image>().material = new Material(unBatch);
                    // 设置初始布局
                    var rect = SpriteObject.GetComponent<RectTransform>();
                    rect.anchorMin = new Vector2(0, 1);
                    rect.anchorMax = new Vector2(0, 1);

                    rect.pivot = new Vector2(0, 1);
                    rect.anchoredPosition = Vector2.zero;
                    rect.localScale = Vector3.one;
                    // 赋 Sprite 贴图
                    var img = SpriteObject.GetComponent<Image>();
                    img.sprite = currSprite;
                    rect.sizeDelta = new Vector2(currSprite.texture.width,
                        currSprite.texture.height);
                } else
                {
                    // SpriteObject.AddComponent<RectTransform>();
                    MeshRenderer renderer = SpriteObject.AddComponent<MeshRenderer>();
                    MeshFilter meshFilter = SpriteObject.AddComponent<MeshFilter>();
                    meshFilter.mesh = Resources.GetBuiltinResource<Mesh>("Quad.fbx");
                    renderer.material = this._currBatch;
                    this._currBatch.SetTexture("_AtlasTex", Atlas);
                    SpriteObject.transform.localPosition = Vector3.zero;
                    SpriteObject.transform.localScale = Vector3.one;
                    SpriteObject.transform.localEulerAngles = Vector3.zero;
                    props.InstancingProps = new MaterialPropertyBlock();
                    Rect currRect = GetRectOfAtlas(currSprite.texture);
                    props.InstancingProps.SetVector("_MatCol1", new Vector4(1, 0, 0, 0));
                    props.InstancingProps.SetVector("_MatCol2", new Vector4(0, 1, 0, 0));
                    props.InstancingProps.SetVector("_MatCol3", new Vector4(0, 0, 1, 0));
                    props.InstancingProps.SetVector("_MatCol4", new Vector4(0, 0, 0, 1));
                    props.InstancingProps.SetVector("_InsColor", new Vector4(1, 1, 1, 1));
                    props.InstancingProps.SetVector("_TexSize", new Vector4(currSprite.texture.width, currSprite.texture.height, 0, 0));
                    props.InstancingProps.SetVector("_TexInfo", new Vector4(currRect.x, currRect.y, currRect.width, currRect.height));
                    SpriteObject.GetComponent<MeshRenderer>().SetPropertyBlock(props.InstancingProps);
                }
                
                // 捆绑数据
                var frameList = new List<FrameEntity>();
                //Debug.Log(sprites);
                foreach (var frame in sprites[i].Frames)
                {
                    frameList.Add(frame);
                }

                var spc = SpriteObject.GetComponent<ShaderPropertyController>();
                spc.frameEntities = frameList;
                _subSpritesList.Add(spc);

                _isStageInited = true;
            }
            if(autoPlay)
            {
                Play();
            }
        }
    }
}