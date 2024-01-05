#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class ScreenUtils
{
    public static GameViewSizeGroupType s_CurGameViewSizeType =
#if UNITY_ANDROID
        GameViewSizeGroupType.Android;
#elif UNITY_IPHONE || UNITY_IOS
        GameViewSizeGroupType.iOS;
#else
        GameViewSizeGroupType.Standalone;
#endif

    public static void SetLandscapeResolution()
    {
#if UNITY_EDITOR
        int landscapeIndex = GameViewUtils.FindSize(s_CurGameViewSizeType, 1334, 750);

        var gvWndType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        var selectedSizeIndexProp = gvWndType.GetProperty("selectedSizeIndex",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var gvWnd = EditorWindow.GetWindow(gvWndType);
        selectedSizeIndexProp.SetValue(gvWnd, landscapeIndex, null);
#endif
    }

    public static void SetPortraitResolution()
    {
#if UNITY_EDITOR
        int portraitIndex = GameViewUtils.FindSize(s_CurGameViewSizeType, 750,1334);

        var gvWndType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        var selectedSizeIndexProp = gvWndType.GetProperty("selectedSizeIndex",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var gvWnd = EditorWindow.GetWindow(gvWndType);
        selectedSizeIndexProp.SetValue(gvWnd, portraitIndex, null);
#endif
    }
}
#endif