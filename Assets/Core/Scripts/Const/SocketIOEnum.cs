namespace Tuwan.Const
{
    public enum SocketIoType
    {
        LOGIN = 0,//登录
        WHEATLIST = 1,//麦位信息
        APPLY_WHEAT = 5,//申请上麦回调
        SETCHANNEL = 33,//加入聊天室
        CHAT_TEXT = 36,//聊天弹幕
        CHAT_IMAGE = 37,//聊天图片
    }
    public enum SocketDataType
    {
        CHAT_SHOW_GIFT = 101,// 聊天礼物
    }

    public enum UIEventTag
    {
        //login 10000~19999
        EVENT_UI_LOGIN_SUCCESS = 10000,//登陆成功

        //home 20000~29999


        //world 30000~39999
        EVENT_UI_FROM_WORLD_ENTER_LOBBY = 30000,//从世界进入大厅
        EVENT_UI_SHOW_OR_HIDE_WORLD_UI = 30001,//进入或离开世界，打开或关闭世界自带UI
        EVENT_UI_ENTER_WORLD_INIT_LOBBY_FORM = 30002,//LobbyForm初始化完成

        //lobby 40000~49999
        EVENT_UI_GIFTBAG_TOGGLE_CHANGE = 40000, //礼物切换
        EVENT_UI_MOVE_PLAYER_TO_WHEAT_POSITION = 40001,//麦位用户移动位置到麦位上
        EVENT_UI_ENTER_LOBBY_FORM = 40002,//打开迪厅界面，控制部分组件展示
        EVENT_UI_CHANGE_BGM = 40003,//背景音乐切换
        EVENT_UI_MOVE_AUDIENCE_TO_LOBBY_RANDOM_POSITION = 40004,//观众位置移动到大厅随机位置
        EVENT_UI_OPEN_SVGA_ON_UI_FORM = 40005,//观众位置移动到大厅随机位置
        EVENT_UI_REFRESH_WHEAT_STATE = 40006,//更新麦位信息
        EVENT_UI_SVGA_ON_UI_ANIM_COMPLETE = 40007,//SVGAOnUI的UI部分播放完成
        EVENT_UI_SEND_GIFT = 40008,//接收url送礼物

        //common   50000~59999
        EVENT_UI_DIAMOND_CHANGE = 50000, //钻石变化
        EVENT_UI_TUWAN_PLAYER_INFO_SYNCED = 50001,//tuwanUId同步完成

        //other 90000~99999
        EVENT_UI_DEBUG_TOGGLE_CHANGE = 60000,//调试cooie回调

    }
}