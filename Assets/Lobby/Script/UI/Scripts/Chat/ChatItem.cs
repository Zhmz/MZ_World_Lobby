using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatItem
{
    public enum EnumChatItemType
    {
        NONE,
        DATE_TIME_TAG,
        NOTICE_TAG,
        LEFT_CHAT_MESSAGE,
        RIGHT_CHAT_MESSAGE,
    }

    public EnumChatItemType itemType = EnumChatItemType.NONE;
    public GameObject gameObject;
    public string text;
    public Image img;
    public System.DateTime time;
}
