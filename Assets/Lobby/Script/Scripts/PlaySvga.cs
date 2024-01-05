using Svga;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class PlaySvga : MonoBehaviour
{
    public SvgaPlayer Player;
    public Canvas Canvas;
    public bool isBatching = false;

    public static string DEFAULT_SVGA_URL = "https://img3.tuwandata.com/uploads/play/1916771563790770.svga";

    // Start is called before the first frame update
    void Awake()
    {

        //Canvas canvas = this.GetComponentInParent<Canvas>();
        //canvas.worldCamera = Camera.main;
        //canvas.planeDistance = 10;

        //string path = "https://img3.tuwandata.com/uploads/play/1916771563790770.svga";
        //StartCoroutine(ReadData(path));

        //string chrismasPath = "https://img3.tuwandata.com/uploads/play/8418431577015639.svga";

        //string lipsPath = "https://img3.tuwandata.com/uploads/play/1916771563790770.svga";
        //StartCoroutine(LoadSVGA(lipsPath));
    }

    public void PlaySVGAWithURL(string giftUrl = "", bool isFullScreen = false)
    {
        if (string.IsNullOrEmpty(giftUrl))
        {
            StartCoroutine(LoadSVGA(DEFAULT_SVGA_URL, isFullScreen));
        }
        else
        {
            StartCoroutine(LoadSVGA(giftUrl, isFullScreen));
        }
    }

    IEnumerator ReadData(string path)
    {
        WWW www = new WWW(path);
        yield return www;
        while (www.isDone == false)
        {
            yield return new WaitForEndOfFrame();
        }

        var data = www.bytes;

        using (Stream stream = new MemoryStream(data))
        {
            Player?.LoadSvgaFileData(stream);
        }

        yield return new WaitForEndOfFrame();
    }

    public void Play()
    {
        Player?.Play(0, () => Debug.Log("Play complete."));
    }

    IEnumerator LoadSVGA(string path, bool isFullScreen = false)
    {
        // Download
        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();
        var data = request.downloadHandler.data;
        using (Stream stream = new MemoryStream(data))
        {
            Player.LoadSvgaFileData(path, stream, isBatching);
        }
        if (isFullScreen)
        {
            var currSize = GetComponent<RectTransform>().sizeDelta;
            Debug.LogError(currSize);
            var canvasSize = Canvas.GetComponent<RectTransform>().sizeDelta;
            Debug.LogError(canvasSize);
            transform.GetComponent<RectTransform>().localScale = new Vector3(canvasSize.x / currSize.x, canvasSize.y / currSize.y, 1);
        }
        yield return new WaitForEndOfFrame();
    }
}
