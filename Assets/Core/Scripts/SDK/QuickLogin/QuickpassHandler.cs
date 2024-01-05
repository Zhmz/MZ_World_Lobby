using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
#if UNITY_IPHONE
using Newtonsoft.Json;
#endif
using System;
using LitJson;
using Unity.VisualScripting;
using System.IO;

public struct QuickpassUiConfig
{
    public string statusBarColor; // 状态栏颜色
    public bool isStatusBarDarkColor;// 设置状态栏字体图标颜色是否为暗色(黑色)
    public string navBackIcon;// 导航栏返回按钮
    public int navBackIconWidth;// 导航栏返回按钮宽
    public int navBackIconHeight;// 导航栏返回按钮高
    public int navBackIconGravity; // 设置导航栏返回图标位置
    public bool isHideBackIcon; // 是否隐藏导航栏返回按钮
    public int navHeight; // 导航栏高度
    public string navBackgroundColor; // 导航栏背景颜色
    public string navTitle; // 导航栏标题
    public int navTitleSize;// 导航栏标题字体大小
    public bool isNavTitleBold; // 导航栏标题是否为粗体
    public string navTitleColor; // 导航栏标题颜色
    public bool isHideNav; // 是否隐藏导航栏
    public string logoIconName; // 应用 logo 图标
    public int logoWidth; // logo宽度
    public int logoHeight; //  logo 高度
    public int logoTopYOffset; //  logo 顶部 Y 轴偏移
    public int logoBottomYOffset; //  logo 距离屏幕底部偏移
    public int logoXOffset; //  logo 水平方向的偏移
    public bool isHideLogo; // 是否隐藏 logo
    public string maskNumberColor; // 手机掩码颜色
    public int maskNumberSize; // 手机掩码字体大小
    public int maskNumberDpSize; // 手机掩码字体大小 dp
    public int maskNumberTopYOffset; // 手机掩码顶部Y轴偏移
    public int maskNumberBottomYOffset; // 手机掩码距离屏幕底部偏移
    public int maskNumberXOffset; // 手机掩码水平方向的偏移
    public int sloganSize; // 认证品牌字体大小
    public int sloganDpSize; // 认证品牌字体大小 dp
    public string sloganColor; // 认证品牌颜色
    public int sloganTopYOffset; // 认证品牌顶部 Y 轴偏移
    public int sloganBottomYOffset; // 认证品牌距离屏幕底部偏移
    public int sloganXOffset; // 认证品牌水平方向的偏移
    public string loginBtnText; // 登录按钮文本
    public int loginBtnTextSize; // 登录按钮文本字体大小
    public int loginBtnTextDpSize; // 登录按钮文本字体大小 dp
    public string loginBtnTextColor; // 登录按钮文本颜色
    public int loginBtnWidth; // 登录按钮宽度
    public int loginBtnHeight; // 登录按钮高度
    public string loginBtnBackgroundRes; // 登录按钮背景图标
    public int loginBtnTopYOffset; // 登录按钮顶部Y轴偏移
    public int loginBtnBottomYOffset; // 登录按钮距离屏幕底部偏移
    public int loginBtnXOffset; // 登录按钮水平方向的偏移
    public string privacyTextColor; // 隐私栏文本颜色，不包括协议
    public string privacyProtocolColor; // 隐私栏协议颜色
    public int privacySize; // 隐私栏区域字体大小
    public int privacyDpSize; // 隐私栏区域字体大小 dp
    public int privacyTopYOffset; // 隐私栏顶部Y轴偏移
    public int privacyBottomYOffset; // 隐私栏距离屏幕底部偏移
    public int privacyTextMarginLeft; // 隐私栏文本和复选框的距离
    public int privacyMarginLeft; // 设置隐私栏左侧边距
    public int privacyMarginRight; // 设置隐私栏右侧边距
    public bool privacyState; // 隐私栏协议复选框勾选状态
    public bool isHidePrivacySmh; // 是否隐藏隐私协议书名号《》
    public bool isHidePrivacyCheckBox; // 是否隐藏隐私栏勾选框
    public bool isPrivacyTextGravityCenter; // 协议文本是否居中
    public int checkBoxGravity; // 隐私栏勾选框与文本协议对齐方式
    public int checkBoxWith; // 设置隐私栏复选框宽度
    public int checkBoxHeight; // 设置隐私栏复选框高度
    public string checkedImageName; // 隐私栏复选框选中时的图片资源
    public string unCheckedImageName; // 隐私栏复选框未选中时的图片资源
    public string privacyTextStart; // 隐私栏声明部分起始文案
    public string protocolText; // 设置隐私栏协议文本
    public string protocolLink; // 设置隐私栏协议链接
    public string protocol2Text; // 设置隐私栏协议 2 文本
    public string protocol2Link;  // 设置隐私栏协议 2 链接
    public string protocol3Text; // 设置隐私栏协议 3 文本
    public string protocol3Link;  // 设置隐私栏协议 3 链接
    public string privacyTextEnd; // 隐私栏声明部分尾部文案
    public string protocolNavTitleCm; // 移动协议 Web 页面导航栏标题
    public string protocolNavTitleCu; // 联通协议 Web 页面导航栏标题
    public string protocolNavTitleCt; // 电信协议 Web 页面导航栏标题
    public string protocolNavBackIcon; // 协议 Web 页面导航栏返回图标
    public int protocolNavHeight; // 协议 Web 页面导航栏高度
    public string protocolNavTitleColor; // 协议Web页面导航栏标题颜色
    public int protocolNavTitleSize; // 协议Web页面导航栏标题大小
    public int protocolNavTitleDpSize; // 协议Web页面导航栏标题大小 dp
    public int protocolNavBackIconWidth; // 协议 Web 页面导航栏返回按钮宽度
    public int protocolNavBackIconHeight; // 协议 Web 页面导航栏返回按钮高度
    public string protocolNavColor; // 协议Web页面导航栏颜色
    public string backgroundImage; // 登录页面背景
    public string backgroundGif; // 登录页面背景为 Gif
    public string backgroundVideo; // 登录页面背景为视频
    public string backgroundVideoImage; // 视频背景时的预览图
    public string enterAnimation; // 设置授权页进场动画
    public string exitAnimation; // 设置授权页退出动画
    public bool isLandscape; // 是否横屏
    public bool isDialogMode; // 是否弹窗模式
    public int dialogWidth; // 授权页弹窗宽度
    public int dialogHeight; // 授权页弹窗高度
    public int dialogX; // 授权页弹窗 X 轴偏移量，以屏幕中心为原点
    public int dialogY; // 授权页弹窗 Y 轴偏移量，以屏幕中心为原点
    public bool isBottomDialog; // 授权页弹窗是否贴于屏幕底部
    public bool isProtocolDialogMode; // 协议详情页是否开启弹窗模式
    public bool isPrivacyDialogAuto; // 协议未勾选弹窗点击是否自动登录
    public string privacyDialogText; // 协议未勾选弹窗自定义message
    public float privacyDialogSize; // 协议未勾选弹窗文本字体大小
    public bool iShowLoading; // 是否显示授权页授权登录loading
    public bool backPressedAvailable; // 授权页物理返回键是否可用
    public List<Widget> widgets; // 自定义view
};

public struct Widget
{
    public string viewId; // view的id，点击可返回
    public string type; // 只支持 Button TextView ImageView
    public string text; // 文本内容
    public float textSize; // 文本大小
    public string textColor; // 文本颜色
    public int left; // 距离左边的距离
    public int top; // 距离顶部的距离
    public int right; // 距离右边的距离
    public int bottom; // 距离底部的距离
    public int width; // 宽度
    public int height; // 高度
    public string backgroundColor; // 背景颜色
    public string backgroundImage; // 背景图片名字
    public bool isGravityCenter; // 文本内容是否居中
    public int positionType; // 在授权页的哪个区域 0：在内容区域 1：在状态栏区域
}

public class QuickpassHandler
{
    private static AndroidJavaObject objQuickLogin;

#if UNITY_IPHONE
    [DllImport("__Internal")]
    static extern void init(string businessId, int timeout);
#endif

#if UNITY_IPHONE
    [DllImport("__Internal")]
    static extern void preFetchNumber(IntPtr preFetchNumberHandler);
#endif

#if UNITY_IPHONE
    [DllImport("__Internal")]
    static extern void setupUiConfig(string json, IntPtr UiConfigHandler);
#endif

#if UNITY_IPHONE
    [DllImport("__Internal")]
    static extern void OnPassLogin(bool animated, IntPtr OnPassLoginHandler);
#endif

#if UNITY_IPHONE
    [DllImport("__Internal")]
    static extern bool checkVerifyEnable();
#endif

#if UNITY_IPHONE
    [DllImport("__Internal")]
    static extern void closeAuthController();
#endif

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PreFetchNumberHandler(string resultString);

    [AOT.MonoPInvokeCallback(typeof(PreFetchNumberHandler))]
    static void preFetchNumberHandler(string resultStr)
    {
        Debug.Log(resultStr + "======");
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void UiConfigHandler(string resultString);

    [AOT.MonoPInvokeCallback(typeof(UiConfigHandler))]
    static void uiConfigHandler(string resultStr)
    {
        Debug.Log(resultStr);
#if UNITY_IPHONE
        JsonData jsonObject = JsonMapper.ToObject(resultStr);
        if (jsonObject["action"].ToString() == "handleCustomLabel")
        {
            closeAuthController();
        }
#endif
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void OnPassLoginHandler(string resultString);

    [AOT.MonoPInvokeCallback(typeof(OnPassLoginHandler))]
    static void onPassLoginHandler(string resultStr)
    {
        Debug.Log(resultStr + "==========");
#if UNITY_IPHONE
        closeAuthController();
#endif

    }

    //  businessId:业务id，timeout 预取号超时时间，单位毫秒，debug 是否打开日志开关
    public static void Init(string businessId, int timeout = 6000, bool debug = false)
    {
#if UNITY_ANDROID
        AndroidJavaClass jClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = jClass.GetStatic<AndroidJavaObject>("currentActivity");
        var applicationContext = objActivity.Call<AndroidJavaObject>("getApplicationContext");
        objQuickLogin = new AndroidJavaClass("com.netease.nis.quicklogin.QuickLogin").CallStatic<AndroidJavaObject>("getInstance");
        objQuickLogin.Call("init", applicationContext, businessId);
        objQuickLogin.Call("setDebugMode", debug);
        objQuickLogin.Call("setPrefetchNumberTimeout", timeout);
#endif
#if UNITY_IPHONE
        init(businessId, timeout);
#endif
    }

    public static void setIOSUiConfig(string json)
    {
#if UNITY_IPHONE
        UiConfigHandler handler = new UiConfigHandler(uiConfigHandler);
        IntPtr fp = Marshal.GetFunctionPointerForDelegate(handler);
        setupUiConfig(json, fp);
        Debug.Log(json);
#endif
    }
    public static void showQuickLogin()
    {
#if UNITY_ANDROID
        QuickpassUiConfig uiConfig = new QuickpassUiConfig();
        uiConfig.statusBarColor = "#ffff00";
        uiConfig.isStatusBarDarkColor = true;
        uiConfig.navBackIcon = "yd_ic_close";
        uiConfig.navBackIconWidth = 15;
        uiConfig.navBackIconHeight = 15;
        uiConfig.navHeight = 50;
        uiConfig.navBackgroundColor = "#0000ff";
        uiConfig.navTitle = "一键登录/注册";
        uiConfig.navTitleSize = 20;
        uiConfig.isNavTitleBold = true;
        uiConfig.navTitleColor = "#ffffff";
        uiConfig.logoIconName = "yd_ic_logo";
        uiConfig.logoWidth = 70;
        uiConfig.logoHeight = 70;
        uiConfig.logoTopYOffset = 50;
        uiConfig.logoBottomYOffset = 0;
        uiConfig.logoXOffset = 0;
        uiConfig.maskNumberColor = "#0000ff";
        uiConfig.maskNumberSize = 15;
        uiConfig.maskNumberTopYOffset = 150;
        uiConfig.maskNumberBottomYOffset = 0;
        uiConfig.maskNumberXOffset = 0;
        uiConfig.sloganSize = 15;
        uiConfig.sloganColor = "#00ff00";
        uiConfig.sloganTopYOffset = 200;
        uiConfig.sloganBottomYOffset = 0;
        uiConfig.sloganXOffset = 0;
        uiConfig.loginBtnText = "同意协议并登录";
        uiConfig.loginBtnTextSize = 15;
        uiConfig.loginBtnTextColor = "#ffffff";
        uiConfig.loginBtnWidth = 300;
        uiConfig.loginBtnHeight = 45;
        uiConfig.loginBtnTopYOffset = 250;
        uiConfig.loginBtnBottomYOffset = 0;
        uiConfig.loginBtnXOffset = 0;
        uiConfig.loginBtnBackgroundRes = "yd_btn_bg";
        uiConfig.privacyTextColor = "#00ffff";
        uiConfig.privacyProtocolColor = "#ff00ff";
        uiConfig.privacySize = 12;
        uiConfig.privacyTopYOffset = 0;
        uiConfig.privacyBottomYOffset = 50;
        uiConfig.privacyTextMarginLeft = 6;
        uiConfig.privacyMarginLeft = 6;
        uiConfig.privacyMarginRight = 6;
        uiConfig.privacyState = true;
        uiConfig.isHidePrivacySmh = false;
        uiConfig.isHidePrivacyCheckBox = false;
        uiConfig.isPrivacyTextGravityCenter = false;
        uiConfig.checkBoxGravity = 48;
        uiConfig.privacyTextStart = "登录即同意";
        uiConfig.protocolText = "服务条款一";
        uiConfig.protocolLink = "https://www.baidu.com";
        uiConfig.protocol2Text = "服务条款二";
        uiConfig.protocol2Link = "https://www.baidu.com";
        uiConfig.protocol3Text = "服务条款三";
        uiConfig.protocol3Link = "https://www.baidu.com";
        uiConfig.privacyTextEnd = "且授权易盾一键登录SDK使用本机号码";
        uiConfig.protocolNavTitleCm = "中国移动服务条款";
        uiConfig.protocolNavTitleCu = "中国联通服务条款";
        uiConfig.protocolNavTitleCt = "中国电信服务条款";
        uiConfig.protocolNavBackIcon = "ic_close";
        uiConfig.protocolNavBackIconWidth = 15;
        uiConfig.protocolNavBackIconHeight = 15;
        uiConfig.protocolNavTitleColor = "#ffffff";
        uiConfig.protocolNavColor = "#0000ff";
        uiConfig.iShowLoading = true;
        uiConfig.backPressedAvailable = true;
        List<Widget> widgets = new List<Widget>();
        Widget widget = new Widget();
        widget.viewId = "title";
        widget.type = "TextView";
        widget.top = 310;
        widget.width = 300;
        widget.height = 45;
        widget.textSize = 12;
        widget.text = "其他登录方式";
        widget.textColor = "#ffffff";
        widget.backgroundImage = "yd_btn_bg";
        widget.isGravityCenter = true;
        widget.positionType = 0;
        widgets.Add(widget);
        uiConfig.widgets = widgets;
        QuickpassHandler.SetUiConfig(uiConfig);
#endif
#if UNITY_IPHONE
        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/ios-light-config.json");
        string json = sr.ReadToEnd();
        Debug.Log(json);
        QuickpassHandler.setIOSUiConfig(json);
#endif
        QuickpassHandler.OnPassLogin();
    }
    // 设置自定义样式
    public static void SetUiConfig(QuickpassUiConfig uiConfig)
    {

#if UNITY_ANDROID
        AndroidJavaObject objUiConfigBuilder = new AndroidJavaObject("com.netease.nis.quicklogin.helper.UnifyUiConfig$Builder");
        if (uiConfig.statusBarColor != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setStatusBarColor", ParseColor(uiConfig.statusBarColor));
        }
        if (uiConfig.isStatusBarDarkColor)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setStatusBarDarkColor", uiConfig.isStatusBarDarkColor);
        }
        if (uiConfig.navBackIcon != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setNavigationIcon", uiConfig.navBackIcon);
        }
        if (uiConfig.navBackIconWidth != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setNavigationBackIconWidth", uiConfig.navBackIconWidth);
        }
        if (uiConfig.navBackIconHeight != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setNavigationBackIconHeight", uiConfig.navBackIconHeight);
        }
        if (uiConfig.navBackIconGravity != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setNavigationIconGravity", uiConfig.navBackIconGravity);
        }
        if (uiConfig.isHideBackIcon)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setHideNavigationBackIcon", uiConfig.isHideBackIcon);
        }
        if (uiConfig.navHeight != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setNavigationHeight", uiConfig.navHeight);
        }
        if (uiConfig.navBackgroundColor != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setNavigationBackgroundColor", ParseColor(uiConfig.navBackgroundColor));
        }
        if (uiConfig.navTitle != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setNavigationTitle", uiConfig.navTitle);
        }
        if (uiConfig.navTitleSize != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setNavTitleSize", uiConfig.navTitleSize);
        }
        if (uiConfig.isNavTitleBold)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setNavTitleBold", uiConfig.isNavTitleBold);
        }
        if (uiConfig.navTitleColor != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setNavigationTitleColor", ParseColor(uiConfig.navTitleColor));
        }
        if (uiConfig.isHideNav)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setHideNavigation", uiConfig.isHideNav);
        }
        if (uiConfig.logoIconName != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLogoIconName", uiConfig.logoIconName);
        }
        if (uiConfig.logoWidth != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLogoWidth", uiConfig.logoWidth);
        }
        if (uiConfig.logoHeight != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLogoHeight", uiConfig.logoHeight);
        }
        if (uiConfig.logoTopYOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLogoTopYOffset", uiConfig.logoTopYOffset);
        }
        if (uiConfig.logoBottomYOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLogoBottomYOffset", uiConfig.logoBottomYOffset);
        }
        if (uiConfig.logoXOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLogoXOffset", uiConfig.logoXOffset);
        }
        if (uiConfig.isHideLogo)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setHideLogo", uiConfig.isHideLogo);
        }
        if (uiConfig.maskNumberSize != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setMaskNumberSize", uiConfig.maskNumberSize);
        }
        if (uiConfig.maskNumberDpSize != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setMaskNumberDpSize", uiConfig.maskNumberDpSize);
        }
        if (uiConfig.maskNumberColor != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setMaskNumberColor", ParseColor(uiConfig.maskNumberColor));
        }
        if (uiConfig.maskNumberXOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setMaskNumberXOffset", uiConfig.maskNumberXOffset);
        }
        if (uiConfig.maskNumberTopYOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setMaskNumberTopYOffset", uiConfig.maskNumberTopYOffset);
        }
        if (uiConfig.maskNumberBottomYOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setMaskNumberBottomYOffset", uiConfig.maskNumberBottomYOffset);
        }
        if (uiConfig.sloganSize != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setSloganSize", uiConfig.sloganSize);
        }
        if (uiConfig.sloganDpSize != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setSloganDpSize", uiConfig.sloganDpSize);
        }
        if (uiConfig.sloganColor != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setSloganColor", ParseColor(uiConfig.sloganColor));
        }
        if (uiConfig.sloganTopYOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setSloganTopYOffset", uiConfig.sloganTopYOffset);
        }
        if (uiConfig.sloganXOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setSloganXOffset", uiConfig.sloganXOffset);
        }
        if (uiConfig.sloganBottomYOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setSloganBottomYOffset", uiConfig.sloganBottomYOffset);
        }
        if (uiConfig.loginBtnText != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLoginBtnText", uiConfig.loginBtnText);
        }
        if (uiConfig.loginBtnBackgroundRes != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLoginBtnBackgroundRes", uiConfig.loginBtnBackgroundRes);
        }
        if (uiConfig.loginBtnTextColor != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLoginBtnTextColor", ParseColor(uiConfig.loginBtnTextColor));
        }
        if (uiConfig.loginBtnTextSize != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLoginBtnTextSize", uiConfig.loginBtnTextSize);
        }
        if (uiConfig.loginBtnTextDpSize != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLoginBtnTextDpSize", uiConfig.loginBtnTextDpSize);
        }
        if (uiConfig.loginBtnHeight != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLoginBtnHeight", uiConfig.loginBtnHeight);
        }
        if (uiConfig.loginBtnWidth != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLoginBtnWidth", uiConfig.loginBtnWidth);
        }
        if (uiConfig.loginBtnTopYOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLoginBtnTopYOffset", uiConfig.loginBtnTopYOffset);
        }
        if (uiConfig.loginBtnBottomYOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLoginBtnBottomYOffset", uiConfig.loginBtnBottomYOffset);
        }
        if (uiConfig.loginBtnXOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLoginBtnXOffset", uiConfig.loginBtnXOffset);
        }
        if (uiConfig.privacyTextColor != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyTextColor", ParseColor(uiConfig.privacyTextColor));
        }
        if (uiConfig.privacyProtocolColor != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyProtocolColor", ParseColor(uiConfig.privacyProtocolColor));
        }
        if (uiConfig.privacySize != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacySize", uiConfig.privacySize);
        }
        if (uiConfig.privacyDpSize != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyDpSize", uiConfig.privacyDpSize);
        }
        if (uiConfig.privacyTopYOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyTopYOffset", uiConfig.privacyTopYOffset);
        }
        if (uiConfig.privacyBottomYOffset != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyBottomYOffset", uiConfig.privacyBottomYOffset);
        }
        if (uiConfig.privacyTextMarginLeft != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyTextMarginLeft", uiConfig.privacyTextMarginLeft);
        }
        if (uiConfig.privacyMarginLeft != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyMarginLeft", uiConfig.privacyMarginLeft);
        }
        if (uiConfig.privacyMarginRight != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyMarginRight", uiConfig.privacyMarginRight);
        }
        if (!uiConfig.privacyState)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyState", uiConfig.privacyState);
        }
        if (uiConfig.isHidePrivacySmh)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setHidePrivacySmh", uiConfig.isHidePrivacySmh);
        }
        if (uiConfig.isHidePrivacyCheckBox)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setHidePrivacyCheckBox", uiConfig.isHidePrivacyCheckBox);
        }
        if (uiConfig.isPrivacyTextGravityCenter)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyTextGravityCenter", uiConfig.isPrivacyTextGravityCenter);
        }
        if (uiConfig.checkBoxGravity != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setCheckBoxGravity", uiConfig.checkBoxGravity);
        }
        if (uiConfig.checkBoxWith != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyCheckBoxWidth", uiConfig.checkBoxWith);
        }
        if (uiConfig.checkBoxHeight != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyCheckBoxHeight", uiConfig.checkBoxHeight);
        }
        if (uiConfig.checkedImageName != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setCheckedImageName", uiConfig.checkedImageName);
        }
        if (uiConfig.unCheckedImageName != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setUnCheckedImageName", uiConfig.unCheckedImageName);
        }
        if (uiConfig.privacyTextStart != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyTextStart", uiConfig.privacyTextStart);
        }
        if (uiConfig.protocolText != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocolText", uiConfig.protocolText);
        }
        if (uiConfig.protocolLink != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocolLink", uiConfig.protocolLink);
        }
        if (uiConfig.protocol2Text != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocol2Text", uiConfig.protocol2Text);
        }
        if (uiConfig.protocol2Link != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocol2Link", uiConfig.protocol2Link);
        }
        if (uiConfig.protocol3Text != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocol3Text", uiConfig.protocol3Text);
        }
        if (uiConfig.protocol3Link != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocol3Link", uiConfig.protocol3Link);
        }
        if (uiConfig.privacyTextEnd != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyTextEnd", uiConfig.privacyTextEnd);
        }
        if (uiConfig.backgroundImage != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setBackgroundImage", uiConfig.backgroundImage);
        }
        if (uiConfig.backgroundVideo != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setBackgroundVideo", uiConfig.backgroundVideo, uiConfig.backgroundVideoImage);
        }
        if (uiConfig.enterAnimation != null && uiConfig.exitAnimation != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setActivityTranslateAnimation", uiConfig.enterAnimation, uiConfig.exitAnimation);
        }
        if (uiConfig.backgroundGif != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setBackgroundGif", uiConfig.backgroundGif);
        }
        if (uiConfig.protocolNavTitleCm != null & uiConfig.protocolNavTitleCu != null & uiConfig.protocolNavTitleCt != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocolPageNavTitle", uiConfig.protocolNavTitleCm, uiConfig.protocolNavTitleCu, uiConfig.protocolNavTitleCt);
        }
        if (uiConfig.protocolNavBackIcon != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocolPageNavBackIcon", uiConfig.protocolNavBackIcon);
        }
        if (uiConfig.protocolNavColor != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocolPageNavColor", ParseColor(uiConfig.protocolNavColor));
        }
        if (uiConfig.protocolNavHeight != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocolPageNavHeight", uiConfig.protocolNavHeight);
        }
        if (uiConfig.protocolNavTitleColor != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocolPageNavTitleColor", ParseColor(uiConfig.protocolNavTitleColor));
        }
        if (uiConfig.protocolNavTitleSize != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocolPageNavTitleSize", uiConfig.protocolNavTitleSize);
        }
        if (uiConfig.protocolNavTitleDpSize != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocolPageNavTitleDpSize", uiConfig.protocolNavTitleDpSize);
        }
        if (uiConfig.protocolNavBackIconWidth != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocolPageNavBackIconWidth", uiConfig.protocolNavBackIconWidth);
        }
        if (uiConfig.protocolNavBackIconHeight != 0)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setProtocolPageNavBackIconHeight", uiConfig.protocolNavBackIconHeight);
        }
        if (uiConfig.isLandscape)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setLandscape", uiConfig.isLandscape);
        }
        if (uiConfig.isDialogMode)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setDialogMode", uiConfig.isDialogMode, uiConfig.dialogWidth, uiConfig.dialogHeight, uiConfig.dialogX, uiConfig.dialogY, uiConfig.isBottomDialog);
        }
        objUiConfigBuilder.Call<AndroidJavaObject>("setProtocolDialogMode", uiConfig.isProtocolDialogMode);
        objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyDialogAuto", uiConfig.isPrivacyDialogAuto);
        if (uiConfig.privacyDialogText != null)
        {
            objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyDialogText", uiConfig.privacyDialogText);
        }
        objUiConfigBuilder.Call<AndroidJavaObject>("setPrivacyDialogTextSize", uiConfig.privacyDialogSize);
        objUiConfigBuilder.Call<AndroidJavaObject>("setLoadingVisible", uiConfig.iShowLoading);
        objUiConfigBuilder.Call<AndroidJavaObject>("setBackPressedAvailable", uiConfig.backPressedAvailable);
        objUiConfigBuilder.Call<AndroidJavaObject>("setClickEventListener", new ClickEventListener());
        AndroidJavaClass jClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = jClass.GetStatic<AndroidJavaObject>("currentActivity");
        if (uiConfig.widgets != null)
        {
            if (uiConfig.widgets.Count != 0)
            {
                foreach (Widget widget in uiConfig.widgets)
                {
                    AndroidJavaObject jo = null;
                    if ("Button" == widget.type)
                    {
                        jo = new AndroidJavaObject("android.widget.Button", objActivity);
                        jo.Call("setText", widget.text == null ? "" : widget.text);
                        if (widget.isGravityCenter)
                        {
                            jo.Call("setGravity", 17);
                        }

                        if (widget.textSize != 0)
                        {
                            jo.Call("setTextSize", widget.textSize);
                        }
                        if (widget.textColor != null)
                        {
                            jo.Call("setTextColor", ParseColor(widget.textColor));
                        }
                    }
                    else if ("TextView" == widget.type)
                    {
                        jo = new AndroidJavaObject("android.widget.TextView", objActivity);
                        jo.Call("setText", widget.text == null ? "" : widget.text);
                        if (widget.isGravityCenter)
                        {
                            jo.Call("setGravity", 17);
                        }
                        if (widget.textSize != 0)
                        {
                            jo.Call("setTextSize", widget.textSize);
                        }
                        if (widget.textColor != null)
                        {
                            jo.Call("setTextColor", ParseColor(widget.textColor));
                        }
                    }
                    else if ("ImageView" == widget.type)
                    {
                        jo = new AndroidJavaObject("android.widget.ImageView", objActivity);
                    }


                    if (jo != null)
                    {
                        if (widget.viewId != null)
                        {
                            jo.Call("setTag", widget.viewId);
                        }
                        float scale = objActivity.Call<AndroidJavaObject>("getResources").Call<AndroidJavaObject>("getDisplayMetrics").Get<float>("density");
                        int width = (int)(widget.width * scale + 0.5f);
                        int height = (int)(widget.height * scale + 0.5f);
                        AndroidJavaObject layout = new AndroidJavaObject("android.widget.RelativeLayout$LayoutParams", width == 0 ? -2 : width, height == 0 ? -2 : height);
                        if (widget.left != 0)
                        {
                            layout.Call("addRule", 9, 15);
                            layout.Set("leftMargin", (int)(widget.left * scale + 0.5f));
                        }
                        if (widget.top != 0)
                        {
                            layout.Set("topMargin", (int)(widget.top * scale + 0.5f));
                        }
                        if (widget.right != 0)
                        {
                            layout.Call("addRule", 11, 15);
                            layout.Set("rightMargin", (int)(widget.right * scale + 0.5f));
                        }
                        if (widget.bottom != 0)
                        {
                            layout.Call("addRule", 12, 15);
                            layout.Set("bottomMargin", (int)(widget.bottom * scale + 0.5f));
                        }
                        if (widget.left == 0 && widget.right == 0)
                        {
                            layout.Call("addRule", 14);
                        }
                        jo.Call("setLayoutParams", layout);
                        if (widget.backgroundColor != null)
                        {
                            jo.Call("setBackgroundColor", ParseColor(widget.backgroundColor));
                        }
                        if (widget.backgroundImage != null)
                        {
                            int rid = objActivity.Call<AndroidJavaObject>("getResources").Call<int>("getIdentifier", widget.backgroundImage, "drawable", objActivity.Call<string>("getPackageName"));
                            jo.Call("setBackground", objActivity.Call<AndroidJavaObject>("getResources").Call<AndroidJavaObject>("getDrawable", rid));
                        }

                        objUiConfigBuilder.Call<AndroidJavaObject>("addCustomView", jo, widget.viewId, 0, new CustomViewListener());
                    }

                }
            }
        }

        var applicationContext = objActivity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaObject objUiConfig = objUiConfigBuilder.Call<AndroidJavaObject>("build", applicationContext);
        if (objQuickLogin != null)
        {
            objQuickLogin.Call("setUnifyUiConfig", objUiConfig);
        }
        else
        {
            Debug.Log("号码认证单例对象为空");
        }
#endif
    }
    // 预取号
    public static void PreFetchNumber()
    {
#if UNITY_ANDROID
        if (objQuickLogin != null)
        {
            objQuickLogin.Call("prefetchMobileNumber", new QuickpassPreCallback());
        }
        else
        {
            Debug.Log("号码认证单例对象为空");
        }
#endif

#if UNITY_IPHONE
        PreFetchNumberHandler handler = new PreFetchNumberHandler(preFetchNumberHandler);
        IntPtr fp = Marshal.GetFunctionPointerForDelegate(handler);
        preFetchNumber(fp);
        Debug.Log("------");
#endif
    }

    // 取号
    public static void OnPassLogin()
    {
#if UNITY_ANDROID
        if (objQuickLogin != null)
        {
            objQuickLogin.Call("onePass", new QuickpassCallback());
        }
        else
        {
            Debug.Log("号码认证单例对象为空");
        }
#endif
#if UNITY_IPHONE
        OnPassLoginHandler handler = new OnPassLoginHandler(onPassLoginHandler);
        IntPtr fp = Marshal.GetFunctionPointerForDelegate(handler);
        OnPassLogin(true, fp);
#endif
    }

    // 关闭授权页
    public static void CloseLoginAuthView()
    {
#if UNITY_ANDROID
        if (objQuickLogin != null)
        {
            objQuickLogin.Call("quitActivity");
        }
        else
        {
            Debug.Log("号码认证单例对象为空");
        }
#endif
    }

    // 当前环境是否支持号码认证
    public static bool CheckVerifyEnable()
    {
#if UNITY_ANDROID
        if (objQuickLogin != null)
        {
            AndroidJavaClass jClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject objActivity = jClass.GetStatic<AndroidJavaObject>("currentActivity");
            var applicationContext = objActivity.Call<AndroidJavaObject>("getApplicationContext");
            int type = objQuickLogin.Call<int>("checkNetWork", applicationContext);
            if (type != 5 && type != 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.Log("号码认证单例对象为空");
            return false;
        }
#endif

#if UNITY_IPHONE
        bool enalbe = checkVerifyEnable();
        Debug.Log(enalbe);
        return enalbe;
#endif
        return false;
    }

    private static int ParseColor(string colorStr)
    {
        AndroidJavaClass colorClass = new AndroidJavaClass("android.graphics.Color");
        return colorClass.CallStatic<int>("parseColor", colorStr);
    }
}