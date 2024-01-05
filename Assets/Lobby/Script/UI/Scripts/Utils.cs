using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

namespace Tuwan.UI
{
    public class Utils : MonoBehaviour
    {
        public static string SpriteToBase64String(Sprite sprite)
        {
            int sourceMipLevel = 0;
            Texture2D srcTex = sprite.texture;
            Texture2D destination = new Texture2D(srcTex.width, srcTex.height);
            Color[] pixels = srcTex.GetPixels(sourceMipLevel);

            // 如果要把图像旋转180°可以调用如下方法
            // System.Array.Reverse(pixels, 0, pixels.Length);

            // Set the pixels of the destination Texture2D.
            int destinationMipLevel = 0;
            destination.SetPixels(pixels, destinationMipLevel);

            // Apply changes to the destination Texture2D, which uploads its data to the GPU.
            destination.Apply();

            string enc = "data:image/png;base64," + Convert.ToBase64String(destination.EncodeToPNG());

            return UnityWebRequest.EscapeURL(enc);
        }
    }
}
