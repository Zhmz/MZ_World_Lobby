using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tuwan
{
    public class ToolBarCall : MonoBehaviour
    {
        public EmojiListBox EmojiList;
        public EmojiListBox PhotoList;

        public void ShowEmojiListBox()
        {
            if (EmojiList.IsShow) {
                EmojiList.HideSelf();
            } else
            {
                EmojiList.ShowSelf();
            }
            PhotoList.HideSelf();
        }

        public void ShowPhotoListBox()
        {
            EmojiList.HideSelf();
            if (PhotoList.IsShow)
            {
                PhotoList.HideSelf();
            }
            else
            {
                PhotoList.ShowSelf();
            }
        }
    }
}