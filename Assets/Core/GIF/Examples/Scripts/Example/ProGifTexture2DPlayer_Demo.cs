
using UnityEngine;
using UnityEngine.UI;

public class ProGifTexture2DPlayer_Demo : MonoBehaviour
{
    public ProGifPlayerTexture2D m_ProGifPlayerTexture2D;

    public RawImage m_RawImage;
    public Renderer m_Renderer;

    public RawImage m_RawImage2;
    public Renderer m_Renderer2;

    void Start ()
    {
        // Use gif Player Component directly: -----------------------------------------------------
        m_ProGifPlayerTexture2D.Play("https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fb-ssl.duitang.com%2Fuploads%2Fitem%2F201801%2F31%2F20180131194435_LUndG.gif&refer=http%3A%2F%2Fb-ssl.duitang.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=auto?sec=1706232465&t=43e26e77910b1ed48f5c8d05f2f1265f", false);
        m_ProGifPlayerTexture2D.OnTexture2DCallback = (texture2D) =>
        {
            // get and display the decoded texture here:
            m_RawImage.texture = texture2D;
            m_Renderer.material.mainTexture = texture2D;

            // set the texture to other texture fields of the shader
            //m_Renderer.material.SetTexture("_MetallicGlossMap", texture2D);
        };


        // Use the PGif manager: ------------------------------------------------------ 
        PGif.iPlayGif("https://media.giphy.com/media/p4wvewkDf9OYWjIW2P/giphy.gif", m_RawImage2.gameObject, "MyGifPlayerName 01", (texture2D) => {
            // get and display the decoded texture here:
            m_RawImage2.texture = texture2D;
        });
        
        PGif.iPlayGif("https://media.giphy.com/media/GOPutjEbvhBHmH455X/giphy.gif", m_Renderer2.gameObject, "MyGifPlayerName 02", (texture2D) => {
            // get and display the decoded texture here:
            m_Renderer2.material.mainTexture = texture2D;
        });
    }
}
