using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GameFramework.Event;
using Tuwan.Const;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Events;
using Tuwan.Chat;

namespace Tuwan
{
    enum MessageType
    {
        TEXT = 1,
        IMAGE = 2
    }
    public partial class ChatUI
    {
        [HideInInspector]
        public ChatBehaviour Behaviour = null;
        public Transform GifTemp = null;
        protected Dictionary<Transform, ChatItem> chatGifs = new Dictionary<Transform, ChatItem>();
        public ChatItem AddChatMessage(string nick, string msg, EnumChatMessageType messageType = EnumChatMessageType.MessageLeft)
        {
            if (string.IsNullOrEmpty(msg))
            {
                return null;
            }
            var item = AddChatMessage(msg, messageType);
            if (item == null)
            {
                return null;
            }
            changeNick(item, nick);
            return item;
        }

        private void changeNick(ChatItem item, string nick)
        {
            RectTransform rectChat = item.gameObject.GetComponent<RectTransform>();
            Transform nickTrans = rectChat.Find("Nick");
            if (nickTrans)
            {
                var text = nickTrans.GetComponent<Text>();
                text.text = nick;
            }
        }

        public ChatItem AddChatImage(string nick, Sprite sprite, EnumChatMessageType messageType = EnumChatMessageType.MessageLeft)
        {
            var item = AddChatImage(sprite, messageType);
            if (item == null)
            {
                return null;
            }
            changeNick(item, nick);
            return item;
        }

        public void AddChatGifImage(string nick, string url, Action<ChatItem> action, EnumChatMessageType messageType = EnumChatMessageType.MessageLeft)
        {
            var currGifTransform = Instantiate(GifTemp, GifTemp.parent.transform);
            GifTextureContent content = currGifTransform.GetComponent<GifTextureContent>();
            content.LoadGif(url, (texture2D) =>
            {
                var sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                ChatItem item = null;
                if (!chatGifs.ContainsKey(currGifTransform))
                {
                    item = AddChatImage(sprite, messageType);
                    chatGifs.Add(currGifTransform, item);
                    if (item != null)
                    {
                        changeNick(item, nick);
                    }
                }
                chatGifs.TryGetValue(currGifTransform, out item);
                item.img.sprite = sprite;
                action?.Invoke(item);
            });

        }

        public IEnumerator AddEnterChatTag(UserEnterInfo info)
        {
            var item = AddNoticeTag(info.nickname);

            var itemObj = item.gameObject;
            var enterTag = itemObj.GetComponentInChildren<EnterChatTag>();
            enterTag.SetNick(info.nickname);
            // 由于nick的大小是动态变化的所以需要等待ContentSizeFitter执行完成后，hor layout才好执行
            yield return new WaitForEndOfFrame();
            enterTag.SetVip(ChatUtils.ReplacePath(info.vipicon));
        }
    }

    public class ChatBehaviour : UGuiForm
    {
        public InputField inputField;
        public ChatUI Chat;
        public Sprite LeftHeadPic;
        public Sprite RightHeadPic;
        private Queue<IChatInfo> chatQueue = new Queue<IChatInfo>();
        private UnityAction<int, string> txtMsgAction;
        private UnityAction<int, string> picMsgAction;
        private UnityAction<int, string> enterMsgAction;
        static string ReplaceEmptyArrayWithObject(string input, string propertyName)
        {
            // 使用正则表达式替换空的数组
            string pattern = $"\"{propertyName}\": []";
            string replacement = $"\"{propertyName}\": {{}}";
            string result = input.Replace(pattern, replacement);
            return result;
        }

        void loadGifVipIcon(ChatInfo currChatInfo, ChatItem item)
        {
            RectTransform rectChat = item.gameObject.GetComponent<RectTransform>();
            Transform tranFace = rectChat.Find("Face/FaceImage");
            Transform tranParentFace = rectChat.Find("Face");
            if (tranFace != null)
            {
                GifTextureContent gifContent = tranFace.GetComponent<GifTextureContent>();
                var parImg = tranParentFace.GetComponent<Image>();
                gifContent.LoadGif(ChatUtils.ReplacePath(currChatInfo.userinfo.vipicon), (Texture2D tex) => {
                    if(!parImg.sprite)
                    {
                        parImg.sprite = Sprite.Create(ChatUtils.CopyTextureData(tex), new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                    }
                }, false);
            }
        }

        IEnumerator CreateChatItem(ChatInfo currChatInfo)
        {
            ChatItem item = null;
            var vipicon = currChatInfo.userinfo.vipicon;
            if (!string.IsNullOrEmpty(currChatInfo.message))
            {
                item = Chat.AddChatMessage(currChatInfo.userinfo.nickname, currChatInfo.message);
                if (vipicon.Contains(".gif")) loadGifVipIcon(currChatInfo, item);
            }
            else if (!string.IsNullOrEmpty(currChatInfo.thumb))
            {
                if (currChatInfo.thumb.Contains(".gif"))
                {
                    Chat.AddChatGifImage(currChatInfo.userinfo.nickname, currChatInfo.thumb, (ChatItem item) =>
                    {
                        if (item != null && vipicon.Contains(".gif"))
                        {
                            loadGifVipIcon(currChatInfo, item);
                        }
                    });
                }
                else
                {
                    yield return ChatUtils.LoadRemoteSprite(currChatInfo.thumb, (Sprite sprite) =>
                    {
                        item = Chat.AddChatImage(currChatInfo.userinfo.nickname, sprite);
                        if (vipicon.Contains(".gif")) loadGifVipIcon(currChatInfo, item);
                    });
                }
            }
            if (!vipicon.Contains(".gif"))
            {
                yield return ChatUtils.LoadRemoteSprite(ChatUtils.ReplacePath(currChatInfo.userinfo.vipicon), (Sprite sprite) =>
                {
                    if (item == null)
                    {
                        return;
                    }
                    RectTransform rectChat = item.gameObject.GetComponent<RectTransform>();
                    Transform tranFace = rectChat.Find("Face/FaceImage");
                    if (tranFace != null)
                    {
                        RawImage imgFace = tranFace.GetComponent<RawImage>();
                        imgFace.texture = sprite.texture;
                    }
                });
            }
        }

        public void SendChatMessage()
        {
            if (inputField)
            {
                var text = inputField.text;
                var userInfo = Store.Config.UserInfo;
                var uploadObj = new
                {
                    userinfo = userInfo,
                    type = 1,
                    message = text,
                    version = 1,
                    subType = 0,
                    atUsers = "",
                    to = "",
                    sessionid = ChatUtils.GetSessionId(userInfo.uid + "")
                };
                NetManager.inst.Emit(Const.SocketResquestName.Message, new
                {
                    message = Newtonsoft.Json.JsonConvert.SerializeObject(uploadObj)
                });
                inputField.text = "";
            }
        }

        public void SendChatImage(Sprite sprite)
        {
            var base64 = UI.Utils.SpriteToBase64String(sprite);
            var uploadImg = HttpRequestUtil.POST(API.UPLOAD_IMG, "img=" + base64, "application/x-www-form-urlencoded; charset=UTF-8");
            var uploadInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<UploadImg>(uploadImg);
            // 如果没有错误
            if (uploadInfo.error == 0)
            {
                var thumb = uploadInfo.data.thumb;
                var img = uploadInfo.data.image;
                var ratio = uploadInfo.data.ratio;
                var userInfo = Store.Config.UserInfo;
                var uploadObj = new
                {
                    userinfo = userInfo,
                    type = 2,
                    thumb = thumb,
                    image = img,
                    ratio = ratio,
                    payEmoji = "",
                    version = 1,
                    to = "",
                    sessionid = ChatUtils.GetSessionId(userInfo.uid + "")
                };
                NetManager.inst.Emit(Const.SocketResquestName.Message, new
                {
                    message = Newtonsoft.Json.JsonConvert.SerializeObject(uploadObj)
                });
            }
        }

        private void messageAction(int type, string data)
        {
            switch (type)
            {
                case 0:
                    var currData = ReplaceEmptyArrayWithObject(data.Trim(), "bubble_app");
                    var currChatInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ChatInfo>(currData);
                    enqueueChatMessage(currChatInfo);
                    break;
            }
        }

        private void OnEnable()
        {
            Chat.Behaviour = this;
            txtMsgAction = new UnityAction<int, string>(messageAction);
            EventCenter.inst.AddSocketEventListener((int)SocketIoType.CHAT_TEXT, txtMsgAction);
            picMsgAction = new UnityAction<int, string>(messageAction);
            EventCenter.inst.AddSocketEventListener((int)SocketIoType.CHAT_IMAGE, picMsgAction);
            enterMsgAction = new UnityAction<int, string>((int type, string data) =>
            {
                if (type == 4)
                {
                    var enterInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<UserEnterInfo>(data);
                    enqueueChatMessage(enterInfo);
                }
            });
            EventCenter.inst.AddSocketEventListener((int)SocketIoType.SETCHANNEL, enterMsgAction);
        }

        private void OnDisable()
        {
            EventCenter.inst.RemoveSocketEventListener((int)SocketIoType.CHAT_TEXT, txtMsgAction);
            EventCenter.inst.RemoveSocketEventListener((int)SocketIoType.CHAT_IMAGE, picMsgAction);
            EventCenter.inst.RemoveSocketEventListener((int)SocketIoType.SETCHANNEL, enterMsgAction);
        }
        private void enqueueChatMessage(IChatInfo chatInfo)
        {
            chatQueue.Enqueue(chatInfo);
        }

        private void processChatMessages()
        {
            while (chatQueue.Count > 0)
            {
                var currChat = chatQueue.Dequeue();
                if (currChat is ChatInfo)
                {
                    StartCoroutine(CreateChatItem(currChat as ChatInfo));
                }
                else if (currChat is UserEnterInfo)
                {
                    var enterInfo = currChat as UserEnterInfo;
                    StartCoroutine(Chat.AddEnterChatTag(enterInfo));
                }
            }
        }

        private void Update()
        {
            processChatMessages();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }
    }
}