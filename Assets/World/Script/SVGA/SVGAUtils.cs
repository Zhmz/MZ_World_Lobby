using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tuwan;
using Lobby;
using DG.Tweening;
using UnityEngine.UI;
using GameFramework.Event;
using Tuwan.Const;

namespace World
{
    public static class SVGAUtils
    {
        public static void SendGiftAnimByCoordTransformation(RawImage renderTex, List<Transform> receiverTransList, RectTransform svgaUI)
        {
            if (receiverTransList.Count == 0)
            {
                return;
            }
            Vector3 worldPos = receiverTransList[0].position;
            worldPos.y += 2;

            // 先将3D坐标转换成屏幕坐标
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);

            Debug.LogError("screenPoint = " + screenPoint);

            Sequence seq = DOTween.Sequence();
            Ease method = Ease.OutQuad;
            seq.AppendInterval(2f)
                .Append(renderTex.DOColor(new Color(1, 1, 1, 0.3f), 2f).SetEase(method))
                .Join(renderTex.transform.DOScale(0f, 2f).SetEase(method))
                .Join(renderTex.transform.DOMove(screenPoint, 2f).SetEase(method))
                .AppendCallback(() =>
                {
                    //关闭界面
                    EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_SVGA_ON_UI_ANIM_COMPLETE);
                })
                .AppendInterval(0.5f)
                .AppendCallback(() =>
                {
                    renderTex.color = new Color(1, 1, 1, 1f);
                    renderTex.transform.localScale = new Vector3(1, 1, 1);
                    renderTex.transform.localPosition = Vector3.zero;
                })
                .Play();

        }

        public static void SendGiftAnimOnUI(string giftUrl, RawImage renderTex, Vector3 startWorldPos, List<Transform> receiverTransList, GameObject giftGO, GameObject parentGO)
        {
            Ease method = Ease.OutQuad;
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(2f)
                .AppendCallback(() =>
                {
                    renderTex.DOColor(new Color(1, 1, 1, 0.3f), 2f).SetEase(method);
                    renderTex.transform.DOScale(0f, 2).SetEase(method);
                })
                .AppendInterval(2f)
                .AppendCallback(() =>
                {
                    //关闭界面
                    EventCenter.inst.EventTrigger((int)UIEventTag.EVENT_UI_SVGA_ON_UI_ANIM_COMPLETE);

                    Debug.LogError("DOScale End");
                    //世界变化
                    for (int i = 0; i < receiverTransList.Count; i++)
                    {
                        Transform receiverTrans = receiverTransList[i];
                        GameObject svgaGiftObj = PoolManager.CreateGameObject(giftGO, parentGO);
                        svgaGiftObj.transform.position = startWorldPos;
                        TraceAnim(svgaGiftObj, startWorldPos, receiverTrans, ETraceType.Bezier);
                    }
                    //Sequence seqAlphaScale = DOTween.Sequence();
                    //seqAlphaScale.AppendInterval(0.1f)
                    //.AppendCallback(() =>
                    //{
                    //    renderTex.color = new Color(1, 1, 1, 1f);
                    //    renderTex.transform.localScale = new Vector3(1, 1, 1);
                    //})
                    //.Play();
                })
                .AppendInterval(0.5f)
                .AppendCallback(() =>
                {
                    renderTex.color = new Color(1, 1, 1, 1f);
                    renderTex.transform.localScale = new Vector3(1, 1, 1);
                })
                .Play();

        }

        public static void SendGiftAnim(string giftUrl, PlaySvga svga, Transform sender, List<Transform> receiverList, GameObject giftGO, GameObject parentGO)
        {
            Vector3 position = sender.position;
            position.y = position.y + 2;

            ETraceType traceType = ETraceType.Bezier;
            //对象池创建
            for (int i = 0; i < receiverList.Count; i++)
            {
                Transform curReceiver = receiverList[i];

                GameObject svgaGiftObj = PoolManager.CreateGameObject(giftGO, parentGO);
                svgaGiftObj.transform.position = position;
                svgaGiftObj.transform.rotation = sender.rotation;

                svga.PlaySVGAWithURL(giftUrl);

                TraceAnim(svgaGiftObj, position, curReceiver, traceType);
            }
        }

        public static void TraceAnim(GameObject stuff, Vector3 startPos, Transform receiverTrans, ETraceType traceType = ETraceType.Line)
        {
            BaseTrace trace = stuff.GetComponent<BaseTrace>();
            if (trace == null)
            {
                if (traceType == ETraceType.Line)
                {
                    trace = stuff.AddComponent<LineTrace>();
                }
                else if (traceType == ETraceType.Arc)
                {
                    trace = stuff.AddComponent<ArcTrace>();
                    (trace as ArcTrace).SetArcHeight(3);
                }
                else if (traceType == ETraceType.Bezier)
                {
                    trace = stuff.AddComponent<BezierTrace>();
                    (trace as BezierTrace).SetBezierParamPosSimplely(startPos, receiverTrans.position, 3);
                }
            }
            trace.SetDestination(receiverTrans, new Vector3(0, 2, 0));
            trace.SetSpeed(10);
        }
    }
}
