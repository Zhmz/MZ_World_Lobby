using System.Collections;
using System.Collections.Generic;
using FishNet.Transporting.Tugboat;
using UnityEngine;
using UnityEngine.UI;
using World;

public class IpInfo : MonoBehaviour
{
    public Tugboat tugboat;
    public SuntailStartDemo startDemo;

    public Text InfoText;
    public InputField IpInput;
    public Button ConfirmButton;

    private string m_CurPlatform;
    private string m_CurIp;

    // Start is called before the first frame update
    void Start()
    {
        m_CurPlatform = startDemo.StartType.ToString();
        m_CurIp = tugboat.GetClientAddress();
        InfoText.text = string.Format("当前端是{0}\n当前ip是{1}",m_CurPlatform,m_CurIp);
    }

    private void OnEnable()
    {
        ConfirmButton.onClick.AddListener(this.onConfirmButtonClick);
    }

    private void OnDisable()
    {
        ConfirmButton.onClick.RemoveListener(this.onConfirmButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void onConfirmButtonClick()
    {
        if (string.IsNullOrEmpty(IpInput.text))
        {
            return;
        }

        m_CurIp = IpInput.text;
        tugboat.SetClientAddress(m_CurIp);
        InfoText.text = string.Format("当前端是{0}\n当前ip是{1}", m_CurPlatform, m_CurIp);
    }
}
