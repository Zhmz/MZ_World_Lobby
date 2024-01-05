using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tuwan;
using GameFramework.Event;
using Tuwan.Const;
using UnityEngine.UI;

namespace Lobby
{
    public class SVGAOnUIForm : UGuiForm
    {
        //public PlaySvga svga;
        //public Canvas SvgaUICanvas;

        PlaySvga svga;
        public RawImage RenderTex;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            svga = GameObject.Find("SvgaView").GetComponent<PlaySvga>();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            EventCenter.inst.AddEventListener((int)UIEventTag.EVENT_UI_SVGA_ON_UI_ANIM_COMPLETE, CompleteAnim);

            if (userData is string)
            {
                string svgaUrl = userData as string;
                svga.PlaySVGAWithURL(svgaUrl, true);
            }

            EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_OPEN_SVGA_ON_UI_FORM, RenderTex);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            EventCenter.inst.RemoveEventListener((int)UIEventTag.EVENT_UI_SVGA_ON_UI_ANIM_COMPLETE, CompleteAnim);
        }

        private void CompleteAnim()
        {
            Close();
        }
    }
}