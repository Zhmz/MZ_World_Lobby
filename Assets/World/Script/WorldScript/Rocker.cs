/*当前脚本挂载在摇杆活动区域*/
using Tuwan;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rocker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // 摇杆的底座,摇杆的棍子
    public GameObject rockerBase;
    public GameObject rockerStick;

    // 初始位置
    Vector2 initPos;
    // 摇杆拖动限制范围
    float astrict;
    // 摇杆拖动后获得的向量
    [HideInInspector]
    public Vector3 MoveDirection;

    RectTransform m_Handle;

    void Start()
    {
        //// 获得摇杆底座
        //rockerBase = GameObject.Find("RockerBase");
        //// 获得摇杆的棍子
        //rockerStick = GameObject.Find("Rocker");
        // 将摇杆的初始位置记录下来，用于拖动结束后还原初始位置
        initPos = rockerBase.GetComponent<RectTransform>().position;
        // 限制范围
        astrict = 100;

        m_Handle = rockerStick.GetComponent<RectTransform>();
    }

    // 检测鼠标书否按下
    public void OnPointerDown(PointerEventData eventData)
    {
        // 设置摇杆的位置为当前鼠标点击的位置
        rockerBase.transform.position = eventData.position;
    }

    // 检测鼠标抬起
    public void OnPointerUp(PointerEventData eventData)
    {
        // 摇杆底座回到指定位置
        rockerBase.GetComponent<RectTransform>().position = initPos;
        // 摇杆回到指定位置
        rockerStick.GetComponent<RectTransform>().localPosition = Vector2.zero;
    }

    // 开始拖动
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 当前摇杆底座的位置
        Vector2 temp = rockerBase.GetComponent<RectTransform>().position;
        // 当前摇杆的相对位置
        rockerStick.GetComponent<RectTransform>().localPosition = Vector3.ClampMagnitude(eventData.position - temp, astrict);
        // 通过摇杆获得的移动方向
        MoveDirection = (rockerStick.transform.position - rockerBase.transform.position).normalized;
        LobbyData.inst.RockerMovement = MoveDirection;
    }

    // 拖动中
    public void OnDrag(PointerEventData eventData)
    {
        // 当前摇杆底座的位置
        Vector2 temp = rockerBase.GetComponent<RectTransform>().position;
        // 当前摇杆的相对位置
        rockerStick.GetComponent<RectTransform>().localPosition = Vector3.ClampMagnitude(eventData.position - temp, astrict);
        // 通过摇杆获得的移动方向
        MoveDirection = (rockerStick.transform.position - rockerBase.transform.position).normalized;
        LobbyData.inst.RockerMovement = MoveDirection;
    }

    // 结束拖动
    public void OnEndDrag(PointerEventData eventData)
    {
        // 拖动结束还原摇杆位置
        rockerBase.GetComponent<RectTransform>().position = initPos;
        // 还原摇杆棍子的位置
        rockerStick.GetComponent<RectTransform>().localPosition = Vector2.zero;
        // 通过摇杆获得的移动方向
        MoveDirection = Vector3.zero;
        LobbyData.inst.RockerMovement = MoveDirection;
    }

    public void SetRockerPos(float horizontal, float vertical)
    {
        Vector3 dir = new Vector3(horizontal, vertical, 0).normalized;
        m_Handle.localPosition = astrict * dir;
    }

    //private void Update()
    //{
    //    Vector3 movement = LobbyData.inst.RockerMovement;
    //    SetRockerPos(movement.x, movement.y);
    //}
}
