using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Tuwan
{
    public class ChatUtils
    {
        public static Dictionary<string, string> UserAvatarUrls = new Dictionary<string, string>
        {
            { "#u1#", "https://img3.tuwandata.com/uploads/repayment/" },
            { "#u2#", "https://res.tuwan.com/templet/play/images/app/" },
            { "#u3#", "https://img3.tuwandata.com/uploads/play/" },
            { "#u4#", "https://ucavatar.tuwan.com/data/avatar/" },
            { "#u5#", "https://ucavatar2.tuwan.com/data/avatar/" },
            { "#u6#", "https://ucavatar3.tuwan.com/data/avatar/" },
            { "#u7#", "https://img3.tuwandata.com/uploads/play2/banner/" },
            { "#u8#", "https://img3.tuwandata.com/uploads/chatroom/" },
            { "#u9#", "https://img1.tuwandata.com/im/" },
            { "#u10#", "https://ucavatar1.tuwan.com/data/avatar/" }
        };

        public static Texture2D CopyTextureData(Texture2D source)
        {
            int width = source.width;
            int height = source.height;

            // 获取原始数据
            Color[] sourceColors = source.GetPixels();

            // 创建新的纹理
            Texture2D copiedTexture = new Texture2D(width, height, source.format, source.mipmapCount > 1);

            // 将原始数据加载到新的纹理中
            copiedTexture.SetPixels(sourceColors);

            // 应用更改
            copiedTexture.Apply();

            return copiedTexture;
        }

        public static string GetSessionId(string uid)
        {
            return "sessionid_" + uid + "_" + DateTime.Now.Ticks + "_" + GetUUID();
        }

        // 生成UUID
        public static string GetUUID()
        {
            return S4() + S4();
        }

        private static string S4()
        {
            return ((int)((1 + new System.Random().NextDouble()) * 0x10000)).ToString("X4").Substring(1);
        }
        public static string ReplacePath(string iconPath)
        {
            string resPath = "";
            foreach (var entry in UserAvatarUrls)
            {
                if (iconPath.Contains(entry.Key))
                {
                    resPath = iconPath.Replace(entry.Key, entry.Value);
                    return resPath;
                }
            }
            return resPath;
        }
        public static IEnumerator LoadRemoteSprite(string url, Action<Sprite> onSpriteLoaded)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    // 获取下载的纹理
                    Texture2D texture = DownloadHandlerTexture.GetContent(www);

                    // 创建Sprite对象
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    // 调用回调
                    onSpriteLoaded?.Invoke(sprite);
                }
                else
                {
                    Debug.LogError("Failed to download image from " + url + ". Error: " + www.error);
                }
            }
        }
    }
}