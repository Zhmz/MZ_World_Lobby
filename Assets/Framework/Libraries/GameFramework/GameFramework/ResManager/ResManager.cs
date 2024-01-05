using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResManager : MonoBehaviour
{
    private volatile static ResManager m_instance;
    //线程锁。当多线程访问时，同一时刻仅允许一个线程访问
    private static object m_locker = new object();
    //单例初始化
    public static ResManager inst
    {
        get
        {
            //线程锁。防止同时判断为null时同时创建对象
            return m_instance;
        }
    }
    private void Awake()
    {
        m_instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadTexture(Image obj, string path)
    {
        // Application.dataPath + "/Resources/Image/Guide1.png"
        string finalPath = Application.dataPath + "/Resources/" + path;
        StartCoroutine(ILoadTexture(obj, finalPath));
    }

    public void LoadTextureUrl(Image obj, string url)
    {
        StartCoroutine(ILoadTexture(obj, url));
    }
    IEnumerator ILoadTexture(Image obj, string url)
    {
        //请求WWW
        WWW www = new WWW(url);
        yield return www;
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            //获取Texture
            Texture2D texture = www.texture;
            //创建Sprite
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            obj.sprite = sprite;
        }
    }
}

