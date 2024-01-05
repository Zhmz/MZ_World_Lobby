using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tuwan
{

    public partial class ChatUI : MonoBehaviour
    {

        public enum EnumChatMessageType
        {
            MessageLeft,
            MessageRight,
        }
        //public float chatInputHeight = 180f;

        RectTransform rootTransform;

        [Header("Controls")]
        public RectTransform contentTransform;
        public InputField inputField;
        // 是否只要输入完成就自动发送
        public bool isAutoSend = false;
        [Header("Sprite")]
        public Sprite leftFaceSprite;
        public Sprite rightFaceSprite;

        [Header("MessageBox")]
        public GameObject dateTimeTag;
        public GameObject noticeTag;
        public GameObject chatLeftMessageBox;
        public AudioClip chatLeftMessageSound;

        public GameObject chatRightMessageBox;
        public AudioClip chatRightMessageSound;

        float contentMinSize = 0;
        float contentHeight = 0;

        Text prevText;

        AudioSource audioSource;

        List<ChatItem> chatItems = new List<ChatItem>();

        DateTime timeTag;

        float MESSAGE_ICONS_WIDTH = 65;
        float MESSAGE_MIN_WIDTH = 130;
        float MESSAGE_MAX_WIDTH = 300;
        float MESSAGE_RIGHT_MAGIN = 10;

        float MESSAGE_SPRITE_BORDER_WIDTH = 25;

        public delegate void chatMessage(string text, EnumChatMessageType messageType);
        public event chatMessage OnChatMessage;

        // Use this for initialization
        void Start()
        {

            timeTag = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            rootTransform = GetComponent<RectTransform>();

            //float w = rootTransform.offsetMax.x - rootTransform.offsetMin.x;
            float w = rootTransform.rect.width;
            MESSAGE_MAX_WIDTH = w / 3 * 2;


            //contentMinSize = contentTransform.rect.height;//  contentTransform.offsetMax.y - contentTransform.offsetMin.y;
            //contentMinSize = rootTransform.rect.height;
            contentMinSize = contentTransform.rect.height;

            if (inputField != null && isAutoSend)
            {
                inputField.onEndEdit.AddListener(delegate
                {
                    if (inputField.text == "")
                        return;

                    AddChatMessage(inputField.text, EnumChatMessageType.MessageRight);
                    if (OnChatMessage != null)
                        OnChatMessage(inputField.text, EnumChatMessageType.MessageRight);
                //AddLeftChatMessageBox(inputField.text);
                inputField.text = "";
                    inputField.ActivateInputField();
                });
            }

            if (chatLeftMessageSound != null || chatRightMessageSound != null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            InstallContent();
        }

        /// <summary>
        /// Add date time tag
        /// </summary>
        public void AddDateTimeTag()
        {
            //float h = contentTransform.offsetMax.y - contentTransform.offsetMin.y;
            float h = contentTransform.rect.height;

            string time1 = System.DateTime.Now.Hour.ToString();
            string time2 = System.DateTime.Now.Minute.ToString();

            if (time1.Length == 1) time1 = "0" + time1;
            if (time2.Length == 1) time2 = "0" + time2;

            time1 = time1 + ":" + time2;

            GameObject goTag;

            if (dateTimeTag != null)
            {
                goTag = Instantiate(dateTimeTag);
                goTag.name = "DateTime " + time1;
                RectTransform rectTag = goTag.GetComponent<RectTransform>();
                rectTag.parent = contentTransform;

                //float rectHeight = rectTag.offsetMax.y - rectTag.offsetMin.y;
                float rectHeight = rectTag.rect.height;

                rectTag.localPosition = new Vector3(0, h / 2 - rectHeight / 2 - contentHeight);

                ChatItem item = new ChatItem();
                item.gameObject = goTag;
                item.text = time1;
                item.time = DateTime.Now;
                item.itemType = ChatItem.EnumChatItemType.DATE_TIME_TAG;

                chatItems.Add(item);

                Transform goChild = rectTag.Find("DateTimeText");
                if (goChild != null)
                {
                    Text txt = goChild.GetComponent<Text>();
                    if (txt != null)
                    {
                        txt.text = time1;
                    }
                }

                contentHeight += rectHeight;

                CalcContentHeight();
            }
        }

        public ChatItem AddNoticeTag(string text)
        {
            //float h = contentTransform.offsetMax.y - contentTransform.offsetMin.y;
            float h = contentTransform.rect.height;

            GameObject goTag;
            ChatItem item = null;
            if (noticeTag != null)
            {
                goTag = Instantiate(noticeTag);
                goTag.name = "NoticeTag";
                RectTransform rectTag = goTag.GetComponent<RectTransform>();
                rectTag.parent = contentTransform;

                //float rectHeight = rectTag.offsetMax.y - rectTag.offsetMin.y;
                float rectHeight = rectTag.rect.height;

                rectTag.localPosition = new Vector3(0, h / 2 - rectHeight / 2 - contentHeight);

                item = new ChatItem();
                item.gameObject = goTag;
                item.text = text;
                item.time = DateTime.Now;
                item.itemType = ChatItem.EnumChatItemType.NOTICE_TAG;

                chatItems.Add(item);

                Transform goChild = rectTag.Find("NoticeText");
                if (goChild != null)
                {
                    Text txt = goChild.GetComponent<Text>();
                    if (txt != null)
                    {
                        txt.text = text;
                    }
                }

                contentHeight += rectHeight;

                CalcContentHeight();
            }
            return item;
        }

        public ChatItem AddChatImage(Sprite sprite, EnumChatMessageType messageType)
        {
            GameObject templateObject = null;
            switch (messageType)
            {
                case EnumChatMessageType.MessageLeft:
                    templateObject = chatLeftMessageBox;
                    break;
                case EnumChatMessageType.MessageRight:
                    templateObject = chatRightMessageBox;
                    break;
            }

            if (templateObject == null)
                return null;

            //判断时间是否超过1分钟?
            TimeSpan ts = DateTime.Now - timeTag;
            if (ts.TotalMinutes >= 1)
            {
                AddDateTimeTag();
                timeTag = DateTime.Now;
            }
            //float w = rootTransform.offsetMax.x - rootTransform.offsetMin.x;
            //float h = contentTransform.offsetMax.y - contentTransform.offsetMin.y;
            float w = rootTransform.rect.width;
            float h = contentTransform.rect.height;
            GameObject goChat;
            ChatItem item = null;
            if (templateObject != null)
            {
                goChat = Instantiate(templateObject);
                RectTransform rectChat = goChat.GetComponent<RectTransform>();
                rectChat.parent = contentTransform;

                //设置Face
                if (leftFaceSprite != null)
                {
                    Transform tranFace = rectChat.Find("Face/FaceImage");
                    if (tranFace != null)
                    {
                        Image imgFace = tranFace.GetComponent<Image>();
                        if (imgFace != null)
                        {
                            if (messageType == EnumChatMessageType.MessageLeft)
                                imgFace.sprite = leftFaceSprite;
                            else
                                imgFace.sprite = rightFaceSprite;
                        }
                    }
                }

                //设置图片
                Transform tranText = rectChat.Find("MessageBox/MessageText");
                DestroyObject(tranText.gameObject);

                GameObject goSprite = new GameObject();
                goSprite.name = "MessageSprite";
                RectTransform spriteRect = goSprite.AddComponent<RectTransform>();
                spriteRect.SetParent(rectChat.Find("MessageBox"));
                spriteRect.anchorMin = new Vector2(0, 0);
                spriteRect.anchorMax = new Vector2(1, 1);

                spriteRect.offsetMin = new Vector2(5, 5);
                spriteRect.offsetMax = new Vector2(-5, -5);

                Image imgSprite = goSprite.AddComponent<Image>();

                float imgWidth = sprite.rect.width;
                float imgHeight = sprite.rect.height;

                if (imgWidth > MESSAGE_MAX_WIDTH)
                {
                    imgWidth = MESSAGE_MAX_WIDTH;
                    //imgHeight = sprite.rect.width / sprite.rect.height * imgWidth;
                    imgHeight = sprite.rect.height / sprite.rect.width * imgWidth;
                }

                rectChat.offsetMax = new Vector2(rectChat.offsetMin.x + imgWidth + MESSAGE_SPRITE_BORDER_WIDTH, rectChat.offsetMax.y + imgHeight - rectChat.rect.height);

                imgSprite.sprite = sprite;

                float rectWidth = rectChat.offsetMax.x - rectChat.offsetMin.x;
                float rectHeight = rectChat.offsetMax.y - rectChat.offsetMin.y;

                if (messageType == EnumChatMessageType.MessageLeft)
                {
                    //向左对齐
                    rectChat.localPosition = new Vector3(rectWidth / 2 - w / 2, h / 2 - rectHeight / 2 - contentHeight);
                }
                else
                {
                    //向右对齐
                    rectChat.localPosition = new Vector3(w / 2 - rectWidth / 2 - MESSAGE_RIGHT_MAGIN, h / 2 - rectHeight / 2 - contentHeight);

                }


                item = new ChatItem();
                item.gameObject = goChat;
                item.time = DateTime.Now;
                item.img = imgSprite;
                if (messageType == EnumChatMessageType.MessageLeft)
                    item.itemType = ChatItem.EnumChatItemType.LEFT_CHAT_MESSAGE;
                else
                    item.itemType = ChatItem.EnumChatItemType.RIGHT_CHAT_MESSAGE;

                chatItems.Add(item);

                contentHeight += rectHeight;
                CalcContentHeight();
            }

            PlaySound(messageType);
            return item;
        }

        public ChatItem AddChatMessage(string text, EnumChatMessageType messageType)
        {
            GameObject templateObject = null;
            switch (messageType)
            {
                case EnumChatMessageType.MessageLeft:
                    templateObject = chatLeftMessageBox;
                    break;
                case EnumChatMessageType.MessageRight:
                    templateObject = chatRightMessageBox;
                    break;
            }

            if (templateObject == null)
                return null;

            //判断时间是否超过1分钟?
            TimeSpan ts = DateTime.Now - timeTag;
            if (ts.TotalMinutes >= 1)
            {
                AddDateTimeTag();
                timeTag = DateTime.Now;
            }
            //float w = rootTransform.offsetMax.x - rootTransform.offsetMin.x;
            //float h = contentTransform.offsetMax.y - contentTransform.offsetMin.y;
            float w = rootTransform.rect.width;
            float h = contentTransform.rect.height;
            GameObject goChat;
            ChatItem item = null;
            if (templateObject != null)
            {
                goChat = Instantiate(templateObject);
                RectTransform rectChat = goChat.GetComponent<RectTransform>();
                rectChat.parent = contentTransform;

                //设置Face
                if (leftFaceSprite != null)
                {
                    Transform tranFace = rectChat.Find("Face/FaceImage");
                    if (tranFace != null)
                    {
                        Image imgFace = tranFace.GetComponent<Image>();
                        if (imgFace != null)
                        {
                            if (messageType == EnumChatMessageType.MessageLeft)
                                imgFace.sprite = leftFaceSprite;
                            else
                                imgFace.sprite = rightFaceSprite;
                        }
                    }
                }

                //设置文字
                Transform tranText = rectChat.Find("MessageBox/MessageText");
                if (tranText != null)
                {
                    Text txt = tranText.GetComponent<Text>();
                    txt.text = text;

                    prevText = txt;

                    //计算文字的大小
                    TextGenerationSettings settings;
                    TextGenerator tg;
                    tg = txt.cachedTextGeneratorForLayout;
                    settings = txt.GetGenerationSettings(new Vector2(MESSAGE_MAX_WIDTH - MESSAGE_ICONS_WIDTH, 0));

                    Canvas.ForceUpdateCanvases();
                    float txtWidth = tg.GetPreferredWidth(txt.text, settings) / txt.pixelsPerUnit;

                    //float txtHeight = tg.GetPreferredHeight(txt.text.Substring(0, 1), settings) / txt.pixelsPerUnit;
                    float txtHeight = tg.GetPreferredHeight(txt.text, settings) / txt.pixelsPerUnit;


                    //修正带有中文字符在3-5个时，出现的文字被裁切的问题
                    if (txt.text.Length > 3 && txt.text.Length <= 5)
                    {
                        //txtWidth = txtWidth * checkChineseString(txt.text);
                        txt.horizontalOverflow = HorizontalWrapMode.Overflow;
                        if (txt.text.Length == 5)
                        {
                            txtWidth = txtWidth * checkChineseString(txt.text);
                        }
                    }

                    if (txtWidth + MESSAGE_ICONS_WIDTH < MESSAGE_MIN_WIDTH)
                    {
                        //设置自己为最小宽度
                        rectChat.offsetMax = new Vector2(rectChat.offsetMin.x + MESSAGE_MIN_WIDTH, rectChat.offsetMax.y);

                    }

                    if (txtWidth + MESSAGE_ICONS_WIDTH > MESSAGE_MIN_WIDTH && txtWidth < MESSAGE_MAX_WIDTH)
                    {
                        //单行，设置宽度
                        float wth = txtWidth + 80f;
                        if (messageType == EnumChatMessageType.MessageRight) wth += MESSAGE_RIGHT_MAGIN;
                        rectChat.offsetMax = new Vector2(rectChat.offsetMin.x + wth, rectChat.offsetMax.y);
                    }

                    if (txtWidth + MESSAGE_ICONS_WIDTH > MESSAGE_MAX_WIDTH)
                    {
                        //多行，设置最宽的宽度

                        int lineHeight = (int)((txtWidth) / MESSAGE_MAX_WIDTH) - 1;
                        if (lineHeight < 1) lineHeight = 1;

                        //float hh = txtHeight* lineHeight;
                        float hh = txtHeight;
                        rectChat.offsetMax = new Vector2(rectChat.offsetMin.x + MESSAGE_MAX_WIDTH, rectChat.offsetMax.y + hh);

                    }

                }

                float rectWidth = rectChat.offsetMax.x - rectChat.offsetMin.x;
                float rectHeight = rectChat.offsetMax.y - rectChat.offsetMin.y;

                if (messageType == EnumChatMessageType.MessageLeft)
                {
                    //向左对齐
                    rectChat.localPosition = new Vector3(rectWidth / 2 - w / 2, h / 2 - rectHeight / 2 - contentHeight);
                }
                else
                {
                    //向右对齐
                    rectChat.localPosition = new Vector3(w / 2 - rectWidth / 2 - MESSAGE_RIGHT_MAGIN, h / 2 - rectHeight / 2 - contentHeight);

                }

                item = new ChatItem();
                item.gameObject = goChat;
                item.text = text;
                item.time = DateTime.Now;
                if (messageType == EnumChatMessageType.MessageLeft)
                    item.itemType = ChatItem.EnumChatItemType.LEFT_CHAT_MESSAGE;
                else
                    item.itemType = ChatItem.EnumChatItemType.RIGHT_CHAT_MESSAGE;

                chatItems.Add(item);


                contentHeight += rectHeight;
                CalcContentHeight();
            }

            PlaySound(messageType);
            return item;
        }

        // 判断一个字符是否是中文
        bool isChinese(char c)
        {
            return c >= 0x4E00 && c <= 0x9FA5;
        }

        bool isChineseString(string str)
        {
            char[] ch = str.ToCharArray();
            if (str != null)
            {
                for (int i = 0; i < ch.Length; i++)
                {
                    if (isChinese(ch[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        float checkChineseString(string str)
        {
            if (isChineseString(str))
                return 1.1f;
            else
                return 1f;
        }


        void PlaySound(EnumChatMessageType messageType)
        {
            if (audioSource == null)
                return;

            AudioClip clip = null;

            switch (messageType)
            {
                case EnumChatMessageType.MessageLeft:
                    clip = chatLeftMessageSound;
                    break;
                case EnumChatMessageType.MessageRight:
                    clip = chatRightMessageSound;
                    break;
            }

            if (clip == null)
                return;

            audioSource.clip = clip;
            audioSource.Play();
        }

        void InstallContent()
        {
            //float h = rootTransform.rect.height; //rootTransform.offsetMax.y - rootTransform.offsetMin.y;

            RectTransform rect = (RectTransform)contentTransform.parent.parent;
            ////float hp = Mathf.Abs(rect.offsetMax.y - rect.offsetMin.y);
            //float hp = rect.rect.height;

            float h = rect.rect.height;
            //h = h - hp;

            contentTransform.offsetMin = new Vector2(0, 0);
            contentTransform.offsetMax = new Vector2(0, h);
            contentTransform.localPosition = new Vector3(0, 0);

        }

        //重新计算Content的高度并移动到最底部
        void CalcContentHeight()
        {
            if (contentHeight <= contentMinSize)
            {
                //contentHeight = contentMinSize;
                return;
            }
            //float w = rootTransform.offsetMax.x - rootTransform.offsetMin.x;
            //float h = rootTransform.offsetMax.y - rootTransform.offsetMin.y;
            RectTransform rect = (RectTransform)contentTransform.parent.parent;
            float h = rect.rect.height;

            //RectTransform rect = (RectTransform)contentTransform.parent.parent;
            ////float hp = Mathf.Abs(rect.offsetMax.y - rect.offsetMin.y);
            //float hp = rect.rect.height;
            //h = h - hp;

            contentTransform.offsetMin = new Vector2(0, 0);
            contentTransform.offsetMax = new Vector2(0, contentHeight);
            contentTransform.localPosition = new Vector3(0, contentHeight / 2 - h / 2);

        }

        public void ClearChatItems()
        {
            foreach (ChatItem item in chatItems)
            {
                Destroy(item.gameObject);
            }
            chatItems.Clear();

            timeTag = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            contentHeight = 0;
            InstallContent();
        }

        public ChatItem GetChatItem(int index)
        {
            if (index < 0) return null;
            if (index >= chatItems.Count) return null;

            return chatItems[index];
        }

        public List<ChatItem> GetChatItems()
        {
            return chatItems;
        }


    }
}