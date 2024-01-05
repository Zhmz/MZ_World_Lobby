using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GifTextureContent : MonoBehaviour
{
    public string Url;
    public bool AutoPlay;
    // Start is called before the first frame update
    void Start()
    {
        if(AutoPlay)
        {
            LoadGif(Url);
        }
    }

    public void LoadGif(string url, Action<Texture2D> onTextureLoaded = null, bool isLoadSkip = true)
    {
        Url = url;
        int lastIndex = url.LastIndexOf('/');
        PGif.iPlayGif(url, gameObject, url.Substring(lastIndex + 1), (texture2D) =>
        {
            onTextureLoaded?.Invoke(texture2D);
            if (onTextureLoaded != null && isLoadSkip) return;
            var meshRendererComponent = GetComponent<MeshRenderer>();
            if(meshRendererComponent)
            {
                meshRendererComponent.material.mainTexture = texture2D;
            }
            var rawImage = GetComponent<RawImage>();
            if(rawImage)
            {
                rawImage.texture = texture2D;
            }
            var image = GetComponent<Image>();
            if (image)
            {
                image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
            }
        });
    }
}
