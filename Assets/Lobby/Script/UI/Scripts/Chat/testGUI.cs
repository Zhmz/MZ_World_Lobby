using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tuwan
{
    public class testGUI : MonoBehaviour
    {
        public ChatUI ui;

        string text = "";

        public Sprite leftPhotoSprite;
        public Sprite rightPhotoSprite;
        //// Use this for initialization
        //void Start () {

        //}

        //// Update is called once per frame
        //void Update () {

        //}

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 150, 35), "Add Time Tag"))
            {
                ui.AddDateTimeTag();
            }

            if (GUI.Button(new Rect(0, 40, 150, 35), "Add Notice Tag"))
            {
                ui.AddNoticeTag("This is notice text!");
            }

            if (GUI.Button(new Rect(0, 120, 150, 35), "Left Short Msg"))
            {
                ui.AddChatMessage("Hello", ChatUI.EnumChatMessageType.MessageLeft);
            }

            if (GUI.Button(new Rect(0, 160, 150, 35), "Left Middle Msg"))
            {
                ui.AddChatMessage("ABCDEFGHIJKLMNOPQRSTUVWXYZ!!", ChatUI.EnumChatMessageType.MessageLeft);
            }

            if (GUI.Button(new Rect(0, 200, 150, 35), "Left Long Msg"))
            {
                ui.AddChatMessage("ABCDEFGHIJKLMNOPQRSTUVWXYZ!ABCDEFGHIJKLMNOPQRSTUVWXYZ!ABCDEFGHIJKLMNOPQRSTUVWXYZ!ABCDEFGHIJKLMNOPQRSTUVWXYZ!ABCDEFGHIJKLMNOPQRSTUVWXYZ!ABCDEFGHIJKLMNOPQRSTUVWXYZ!", ChatUI.EnumChatMessageType.MessageLeft);
            }
            if (GUI.Button(new Rect(0, 240, 150, 35), "Left Chinese Msg"))
            {
                ui.AddChatMessage("湖水这样沉静，这样蓝，一朵洁白的花闪在秋光里很阴暗；早晨，一个少女来湖边叹气，十六岁的影子比红宝石美丽。青海省城有一个郡王，可怕的欲念", ChatUI.EnumChatMessageType.MessageLeft);
            }

            if (GUI.Button(new Rect(0, 280, 150, 35), "Left Photo Msg"))
            {
                ui.AddChatImage(leftPhotoSprite, ChatUI.EnumChatMessageType.MessageLeft);
            }


            if (GUI.Button(new Rect(0, 350, 150, 35), "Right Short Msg"))
            {
                ui.AddChatMessage("Hello", ChatUI.EnumChatMessageType.MessageRight);
            }

            if (GUI.Button(new Rect(0, 390, 150, 35), "Right Middle Msg"))
            {
                ui.AddChatMessage("ABCDEFGHIJKLMNOPQRSTUVWXYZ!!", ChatUI.EnumChatMessageType.MessageRight);
            }

            if (GUI.Button(new Rect(0, 430, 150, 35), "Right Long Msg"))
            {
                ui.AddChatMessage("ABCDEFGHIJKLMNOPQRSTUVWXYZ!ABCDEFGHIJKLMNOPQRSTUVWXYZ!ABCDEFGHIJKLMNOPQRSTUVWXYZ!ABCDEFGHIJKLMNOPQRSTUVWXYZ!ABCDEFGHIJKLMNOPQRSTUVWXYZ!ABCDEFGHIJKLMNOPQRSTUVWXYZ!", ChatUI.EnumChatMessageType.MessageRight);
            }
            if (GUI.Button(new Rect(0, 470, 150, 35), "Right Chinese Msg"))
            {
                ui.AddChatMessage("湖水这样沉静，这样蓝，一朵洁白的花闪在秋光里很阴暗；早晨，一个少女来湖边叹气，十六岁的影子比红宝石美丽。青海省城有一个郡王，可怕的欲念", ChatUI.EnumChatMessageType.MessageRight);
            }
            if (GUI.Button(new Rect(0, 510, 150, 35), "Right Photo Msg"))
            {
                ui.AddChatImage(rightPhotoSprite, ChatUI.EnumChatMessageType.MessageRight);
            }



            if (GUI.Button(new Rect(Screen.width - 200, 0, 150, 35), "Clear messages"))
            {
                ui.ClearChatItems();
            }
            if (GUI.Button(new Rect(Screen.width - 200, 40, 150, 35), "Get all items"))
            {
                text = "";
                List<ChatItem> items = ui.GetChatItems();
                foreach (ChatItem item in items)
                {
                    if (item.itemType != ChatItem.EnumChatItemType.DATE_TIME_TAG)
                    {
                        text += item.time.ToShortTimeString() + item.text + "\n\r";
                    }
                }
            }

            if (text != "")
            {
                GUI.Label(new Rect(Screen.width - 500, 100, 500, 1000), text);
            }
        }
    }
}