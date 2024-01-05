using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using Tuwan.Const;
using Tuwan.Lobby.Entity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GiftItem : MonoBehaviour
{
    public Text GiftName;
    public Text GiftCost;
    public Image GiftIcon;
    public GameObject Current;
    private GiftItemResponse GiftData = null;
    void OnEnable()
    {
        EventCenter.inst.AddEventListener<int>((int)UIEventTag.EVENT_UI_GIFTBAG_TOGGLE_CHANGE, OnToggleChange);
    }
    void OnDisable()
    {
        EventCenter.inst.RemoveEventListener<int>((int)UIEventTag.EVENT_UI_GIFTBAG_TOGGLE_CHANGE, OnToggleChange);
    }
    private void OnToggleChange(int GiftId)
    {
        if (Current.activeInHierarchy)
        {
            Current.SetActive(false);
        }
        if (GiftId == GiftData.id)
        {
            Current.SetActive(true);
        }
    }

    public void Init(int Index, GiftItemResponse data)
    {
        GiftData = data;
        GiftName.text = data.title;
        GiftCost.text = data.diamond + "钻";
        ResManager.inst.LoadTextureUrl(GiftIcon, data.pic);
        Current.SetActive(Index == 0);
    }
    public void OnClickSelect()
    {
        EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_GIFTBAG_TOGGLE_CHANGE, GiftData.id);
    }
}
