using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tuwan
{
    public class EnterChatTag : MonoBehaviour
    {
        public HorizontalLayoutGroup HorizontalLayout;
        public RawImage VipIcon;
        public Text NickName;
        // 父级对象的RectTransform
        public RectTransform parentTransform;
        RectTransform currTransform;
        [HideInInspector]
        public GifTextureContent GifContent;
        void Start()
        {
            // 获取父级对象的RectTransform组件
            currTransform = GetComponent<RectTransform>();
        }

        public void SetNick(string nick, Texture2D vipIcon)
        {
            NickName.text = nick;
            VipIcon.texture = vipIcon;
            AdjustParentWidth();
        }

        public void SetNick(string nick)
        {
            NickName.text = nick;
        }

        public void SetVip(string vipIcon)
        {
            if (vipIcon.Contains(".gif"))
            {
                GifContent = VipIcon.GetComponent<GifTextureContent>();
                var parentVip = VipIcon.transform.parent;
                var parVipImg = parentVip.GetComponent<RawImage>();
                GifContent.LoadGif(vipIcon, (Texture2D tex) => {
                    if(!parVipImg.texture)
                    {
                        parVipImg.texture = ChatUtils.CopyTextureData(tex);
                    }
                }, false);
            }
            else
            {
                ChatUtils.LoadRemoteSprite(vipIcon, (Sprite sprite) =>
                {
                    VipIcon.texture = sprite.texture;
                });
            }
            AdjustParentWidth();
        }

        public void AdjustParentWidth()
        {
            //HorizontalLayout.SetLayoutHorizontal();
            // 获取父级对象下所有子对象的RectTransform组件
            RectTransform[] childRectTransforms = GetComponentsInChildren<RectTransform>();

            float totalWidth = 0f;
            var parentVip = VipIcon.transform.parent.GetComponent<RectTransform>();
            // 计算所有子对象的宽度之和
            foreach (RectTransform childRectTransform in childRectTransforms)
            {
                if (childRectTransform == currTransform || childRectTransform == parentVip) continue;
                totalWidth += childRectTransform.rect.width;
            }

            // 应用总宽度到父级对象的RectTransform
            currTransform.sizeDelta = new Vector2(totalWidth, currTransform.sizeDelta.y);
            if(parentTransform)
            {
                parentTransform.sizeDelta = new Vector2(totalWidth, parentTransform.sizeDelta.y);
            }
        }
    }
}
