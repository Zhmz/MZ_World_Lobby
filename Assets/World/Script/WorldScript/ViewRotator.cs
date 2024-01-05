using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class ViewRotator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    CinemachineFreeLook freeLook;
    public float RotateSpeedX = 0.5f;
    public float RotateSpeedY = 0.5f;

    private float m_OriginX = 0;
    private float m_OriginY = 0;
    // Start is called before the first frame update
    void Start()
    {
        freeLook = GameObject.Find("FreeLook Camera").GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 检测鼠标书否按下
    public void OnPointerDown(PointerEventData eventData)
    {
        m_OriginX = eventData.position.x;
        m_OriginY = eventData.position.y;
    }

    // 检测鼠标抬起
    public void OnPointerUp(PointerEventData eventData)
    {
    }

    // 开始拖动
    public void OnBeginDrag(PointerEventData eventData)
    {
        m_OriginX = eventData.position.x;
        m_OriginY = eventData.position.y;
    }

    // 拖动中
    public void OnDrag(PointerEventData eventData)
    {
        float rotateValueX = (eventData.position.x - m_OriginX) * RotateSpeedX * Time.deltaTime;
        freeLook.m_XAxis.Value = freeLook.m_XAxis.Value + rotateValueX;
        float rotateValueY = -(eventData.position.y - m_OriginY) * 0.01f * RotateSpeedY * Time.deltaTime;
        freeLook.m_YAxis.Value = Mathf.Clamp01(freeLook.m_YAxis.Value + rotateValueY);
    }

    // 结束拖动
    public void OnEndDrag(PointerEventData eventData)
    {
        float rotateValue = (eventData.position.x - m_OriginX) * RotateSpeedX * Time.deltaTime;
        freeLook.m_XAxis.Value = freeLook.m_XAxis.Value + rotateValue;
        float rotateValueY = -(eventData.position.y - m_OriginY) * 0.01f * RotateSpeedY * Time.deltaTime;
        freeLook.m_YAxis.Value = Mathf.Clamp01(freeLook.m_YAxis.Value + rotateValueY);
 
    }
}
