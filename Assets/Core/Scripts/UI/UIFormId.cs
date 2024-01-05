//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace Tuwan
{
    /// <summary>
    /// 界面编号。
    /// </summary>
    public enum UIFormId
    {
        Undefined = 0,

        /// <summary>
        /// 弹出框。
        /// </summary>
        DialogForm = 1,

        /// <summary>
        /// 主菜单。
        /// </summary>
        MenuForm = 100,

        /// <summary>
        /// 设置。
        /// </summary>
        SettingForm = 101,

        /// <summary>
        /// 关于。
        /// </summary>
        AboutForm = 102,

        /// <summary>
        /// 登录。
        /// </summary>
        LoginForm = 200,

        /// <summary>
        /// 短信验证码登录。
        /// </summary>
        MobileLoginForm = 201,

        /// <summary>
        /// Debug 调试。
        /// </summary>
        DebugForm = 202,

        /// <summary>
        /// 主页。
        /// </summary>
        HomeForm = 300,

        /// <summary>
        /// 世界UI。
        /// </summary>
        WorldForm = 400,

        /// <summary>
        /// 大厅UI。
        /// </summary>
        LobbyForm = 500,

        /// <summary>
        /// 迪厅聊天界面
        /// </summary>
        ChatUI = 600,
        /// <summary>
        /// 礼物UI。
        /// </summary>
        GiftForm = 501,
        /// <summary>
        /// 排麦UI。
        /// </summary>
        RequestMicForm = 502,
        /// <summary>
        /// BGM UI。
        /// </summary>
        LobbyBGMForm = 503,
        /// <summary>
        /// 设置UI。
        /// </summary>
        LobbySettingsForm = 504,
        /// <summary>
        /// 玩家信息UI。
        /// </summary>
        PlayerInfoForm = 505,
        /// <summary>
        /// UI上的SVGA
        /// </summary>
        SVGAOnUIForm = 506,
        /// <summary>
        /// 场景加载界面
        /// </summary>
        SceenLoading = 700
    }
}
