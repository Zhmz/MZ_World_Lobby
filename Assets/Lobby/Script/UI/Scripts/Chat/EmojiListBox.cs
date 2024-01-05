using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tuwan {
    public class EmojiListBox : MonoBehaviour
    {
        public ChatUI chatUI;
        public GameObject spriteButton;
        public Sprite[] sprites;

        RectTransform rootTransform;
        [HideInInspector]
        public bool IsShow = false;
        // Use this for initialization
        void Start()
        {
            rootTransform = GetComponent<RectTransform>();
            InstallList();

        }

        public void HideSelf()
        {
            IsShow = false;
            var group = transform.parent.GetComponent<CanvasGroup>();
            group.interactable = false;
            group.blocksRaycasts = false;
            group.alpha = 0;
        }

        public void ShowSelf()
        {
            IsShow = true;
            var group = transform.parent.GetComponent<CanvasGroup>();
            group.interactable = true;
            group.blocksRaycasts = true;
            group.alpha = 1;
        }

        IEnumerator onButtonClick(Sprite clickedSprite)
        {
            HideSelf();
            yield return new WaitForEndOfFrame();
            chatUI.Behaviour.SendChatImage(clickedSprite);
        }
        void InstallList()
        {
            float leftOffset = 0;
            float firstOffset = 0;
            if (sprites == null)
                return;

            for (int i = 0; i < sprites.Length; i++)
            {
                GameObject go = Instantiate(spriteButton);
                RectTransform goRect = go.GetComponent<RectTransform>();
                goRect.SetParent(rootTransform);

                goRect.anchorMin = new Vector2(0, 0);
                goRect.anchorMax = new Vector2(0, 1);

                goRect.offsetMin = new Vector2(5, 5);

                float imgHeight = rootTransform.rect.height;
                float imgWidth = sprites[i].rect.width / sprites[i].rect.height * imgHeight;
                goRect.offsetMax = new Vector2(imgWidth, -5);

                //if (i == 0)
                firstOffset = imgWidth / 2f;

                leftOffset += imgWidth;

                goRect.localPosition = new Vector3(leftOffset - rootTransform.rect.width / 2f - firstOffset, 0);
                Image img = go.GetComponent<Image>();
                img.sprite = sprites[i];

                Button btn = go.GetComponent<Button>();
                //btn.onClick.AddListener(ButtonClick);
                btn.onClick.AddListener(() => StartCoroutine(onButtonClick(img.sprite)));
            }

            Vector2 imax = rootTransform.offsetMax;
            imax.x = leftOffset - rootTransform.rect.width;
            rootTransform.offsetMax = imax;

        }
    }
}
