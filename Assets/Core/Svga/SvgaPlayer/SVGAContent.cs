using System.Collections;
using System.Collections.Generic;
using System.IO;
using Svga;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SVGAContent : MonoBehaviour
{
    public SvgaPlayer Player;
    public Canvas canvas;
    public string url;
    public bool isBatching;

    void Awake()
    {
        string path =
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.streamingAssetsPath + "/image.svga";
#elif UNITY_IPHONE && !UNITY_EDITOR
        "file://" + Application.streamingAssetsPath + "/image.svga";
#elif UNITY_STANDLONE_WIN||UNITY_EDITOR
            "file://" + Application.streamingAssetsPath + "/image.svga";
#else
        string.Empty;
#endif
        //https://img3.tuwandata.com/uploads/play/8418431577015639.svga
        if (!string.IsNullOrEmpty(this.url))
        {
            path = this.url;
        }
        StartCoroutine(LoadSVGA(path, true));
    }

    private Canvas getCanvas()
    {
        // 获取当前场景的所有根对象
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        // 遍历根对象数组
        foreach (GameObject rootObject in rootObjects)
        {
            // 在根对象中查找 Canvas 组件
            Canvas canvasComponent = rootObject.GetComponentInChildren<Canvas>();

            if (canvasComponent != null)
            {
                return canvasComponent;
            }
        }
        return null;
    }

    public IEnumerator LoadSVGA(string path, bool isFullScreen = false)
    {
        // Download
        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();
        var data = request.downloadHandler.data;
        using (Stream stream = new MemoryStream(data))
        {
            Player.LoadSvgaFileData(path, stream, isBatching);
        }
        if (isFullScreen && isBatching)
        {
            var currSize = GetComponent<RectTransform>().sizeDelta;
            var canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;
            transform.GetComponent<RectTransform>().localScale = new Vector3(canvasSize.x / currSize.x, canvasSize.y / currSize.y, 1);
        }
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator LoadSVGA(string path, int width, int height)
    {
        // Download
        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();
        var data = request.downloadHandler.data;
        using (Stream stream = new MemoryStream(data))
        {
            Player.LoadSvgaFileData(path, stream, isBatching);
        }
        if (isBatching)
        {
            var currSize = GetComponent<RectTransform>().sizeDelta;
            var canvasSize = new Vector2(width, height);
            transform.GetComponent<RectTransform>().localScale = new Vector3(canvasSize.x / currSize.x, canvasSize.y / currSize.y, 1);
        }
        yield return new WaitForEndOfFrame();
    }

    public void Play()
    {
        Player.Play(0, () => Debug.Log("Play complete."));
    }

    public void Pause()
    {
        Player.Pause();
    }
}