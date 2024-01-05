using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuwan.Proto
{
    public class LoginRoomInfoData
    {
        [JsonProperty("uid")]
        public int Uid { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("desclist")]
        public List<object> Desclist { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("complete_state")]
        public int CompleteState { get; set; }

        [JsonProperty("img_cover")]
        public string ImgCover { get; set; }

        [JsonProperty("img_cover_app")]
        public string ImgCoverApp { get; set; }

        [JsonProperty("miniapp_img")]
        public string MiniappImg { get; set; }

        [JsonProperty("seats")]
        public int Seats { get; set; }

        [JsonProperty("channelKey")]
        public string ChannelKey { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("rtm_token")]
        public string RtmToken { get; set; }

        [JsonProperty("access")]
        public int Access { get; set; }

        [JsonProperty("speaking")]
        public int Speaking { get; set; }

        [JsonProperty("time")]
        public int Time { get; set; }

        [JsonProperty("typeid")]
        public int Typeid { get; set; }

        [JsonProperty("cid")]
        public int Cid { get; set; }

        [JsonProperty("vipid")]
        public int Vipid { get; set; }

        [JsonProperty("pcid")]
        public int Pcid { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("socketToken")]
        public string SocketToken { get; set; }

        [JsonProperty("socketDomain")]
        public string SocketDomain { get; set; }

        [JsonProperty("socketPort")]
        public int SocketPort { get; set; }

        [JsonProperty("manage_uid")]
        public string ManageUid { get; set; }

        [JsonProperty("anchor_ids")]
        public List<string> AnchorIds { get; set; }

        [JsonProperty("waiter_ids")]
        public List<string> WaiterIds { get; set; }

        [JsonProperty("boss_ids")]
        public List<object> BossIds { get; set; }

        [JsonProperty("narrator_ids")]
        public List<object> NarratorIds { get; set; }

        [JsonProperty("skin")]
        public string Skin { get; set; }

        [JsonProperty("skin_active")]
        public string SkinActive { get; set; }

        [JsonProperty("fleet")]
        public int Fleet { get; set; }

        [JsonProperty("fleetnum")]
        public int Fleetnum { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("fleet_type")]
        public int FleetType { get; set; }

        [JsonProperty("game_icon")]
        public string GameIcon { get; set; }

        [JsonProperty("game_name")]
        public string GameName { get; set; }

        [JsonProperty("game_desc")]
        public string GameDesc { get; set; }

        [JsonProperty("course_url")]
        public string CourseUrl { get; set; }

        [JsonProperty("pg_android")]
        public string PgAndroid { get; set; }

        [JsonProperty("pg_ios")]
        public string PgIos { get; set; }

        [JsonProperty("naming")]
        public string Naming { get; set; }

        [JsonProperty("rule_desc")]
        public string RuleDesc { get; set; }

        [JsonProperty("h5")]
        public string H5 { get; set; }

        [JsonProperty("first")]
        public int First { get; set; }

        [JsonProperty("ptype")]
        public int Ptype { get; set; }

        [JsonProperty("pgameid")]
        public int Pgameid { get; set; }

        [JsonProperty("pchannel")]
        public string Pchannel { get; set; }

        [JsonProperty("ptitle")]
        public string Ptitle { get; set; }

        [JsonProperty("pListType")]
        public int PListType { get; set; }

        [JsonProperty("wheat_bgs")]
        public List<object> WheatBgs { get; set; }

        [JsonProperty("opentype")]
        public int Opentype { get; set; }

        [JsonProperty("roomid")]
        public int Roomid { get; set; }

        [JsonProperty("time_check")]
        public int TimeCheck { get; set; }

        [JsonProperty("list_type")]
        public int ListType { get; set; }

        [JsonProperty("android_update")]
        public int AndroidUpdate { get; set; }

        [JsonProperty("ios_update")]
        public int IosUpdate { get; set; }

        [JsonProperty("msg_limit")]
        public int MsgLimit { get; set; }

        [JsonProperty("asmr")]
        public int Asmr { get; set; }

        [JsonProperty("cap_role")]
        public string CapRole { get; set; }

        [JsonProperty("fleet_name")]
        public string FleetName { get; set; }

        [JsonProperty("more_name")]
        public string MoreName { get; set; }

        [JsonProperty("create_name")]
        public string CreateName { get; set; }

        [JsonProperty("area_name")]
        public string AreaName { get; set; }

        [JsonProperty("level_name")]
        public string LevelName { get; set; }

        [JsonProperty("roomType")]
        public int RoomType { get; set; }

        [JsonProperty("img_head")]
        public string ImgHead { get; set; }

        [JsonProperty("img_head2")]
        public string ImgHead2 { get; set; }

        [JsonProperty("admin_user")]
        public List<int> AdminUser { get; set; }

        [JsonProperty("admin_spe_user")]
        public List<int> AdminSpeUser { get; set; }

        [JsonProperty("manage_user")]
        public List<object> ManageUser { get; set; }

        [JsonProperty("guidance_task")]
        public int GuidanceTask { get; set; }

        [JsonProperty("beginning_active")]
        public int BeginningActive { get; set; }

        [JsonProperty("active_url")]
        public string ActiveUrl { get; set; }

        [JsonProperty("group_id")]
        public int GroupId { get; set; }

        [JsonProperty("group_img")]
        public string GroupImg { get; set; }

        [JsonProperty("order_song")]
        public int OrderSong { get; set; }

        [JsonProperty("isHaveNewPost")]
        public string IsHaveNewPost { get; set; }

        [JsonProperty("hat")]
        public string Hat { get; set; }

        [JsonProperty("is_show_lyric")]
        public int IsShowLyric { get; set; }

        [JsonProperty("lyric_socketDomain")]
        public string LyricSocketDomain { get; set; }

        [JsonProperty("lyric_socketPort")]
        public int LyricSocketPort { get; set; }

        [JsonProperty("gift_hide")]
        public int GiftHide { get; set; }

        [JsonProperty("expire_time")]
        public int ExpireTime { get; set; }

        [JsonProperty("img_wheat_pc")]
        public string ImgWheatPc { get; set; }

        [JsonProperty("img_wheat_app")]
        public string ImgWheatApp { get; set; }

        [JsonProperty("vip_pack")]
        public int VipPack { get; set; }

        [JsonProperty("at")]
        public int At { get; set; }

        [JsonProperty("is_match")]
        public int IsMatch { get; set; }

        [JsonProperty("live")]
        public int Live { get; set; }

        [JsonProperty("order_btn")]
        public string OrderBtn { get; set; }

        [JsonProperty("game_type")]
        public int GameType { get; set; }

        [JsonProperty("new_chat_mode")]
        public int NewChatMode { get; set; }

        [JsonProperty("gameid")]
        public int Gameid { get; set; }

        [JsonProperty("dtid")]
        public int Dtid { get; set; }

        [JsonProperty("wb_uuid")]
        public string WbUuid { get; set; }

        [JsonProperty("wb_token")]
        public string WbToken { get; set; }

        [JsonProperty("gift_queue")]
        public int GiftQueue { get; set; }

        [JsonProperty("room_manage")]
        public int RoomManage { get; set; }

        [JsonProperty("is_hall_captain")]
        public int IsHallCaptain { get; set; }

        [JsonProperty("fleet_list_img")]
        public string FleetListImg { get; set; }


        [JsonProperty("is_patrol")]
        public int IsPatrol { get; set; }


        [JsonProperty("allow_solo_fleet")]
        public int AllowSoloFleet { get; set; }

        [JsonProperty("is_user_game")]
        public int IsUserGame { get; set; }

        [JsonProperty("game_start_type")]
        public int GameStartType { get; set; }

        [JsonProperty("voice_type")]
        public int VoiceType { get; set; }

        [JsonProperty("voice_token")]
        public string VoiceToken { get; set; }

        [JsonProperty("wolf_user_level")]
        public int WolfUserLevel { get; set; }

        [JsonProperty("is_double_chatroom")]
        public int IsDoubleChatroom { get; set; }

        [JsonProperty("other_channel")]
        public string OtherChannel { get; set; }

        [JsonProperty("other_channel_key")]
        public string OtherChannelKey { get; set; }

        [JsonProperty("source_channel_key")]
        public string SourceChannelKey { get; set; }

        [JsonProperty("source_channel_uid")]
        public int SourceChannelUid { get; set; }

        [JsonProperty("volume_tips")]
        public int VolumeTips { get; set; }

        [JsonProperty("volume_log")]
        public int VolumeLog { get; set; }

        [JsonProperty("volume_low_num")]
        public int VolumeLowNum { get; set; }

        [JsonProperty("volume_normal_num")]
        public int VolumeNormalNum { get; set; }

        [JsonProperty("volume_time")]
        public int VolumeTime { get; set; }

        [JsonProperty("role_voice_code")]
        public int RoleVoiceCode { get; set; }

        [JsonProperty("svga_clear_mode")]
        public int SvgaClearMode { get; set; }

        [JsonProperty("yjx_song_switch")]
        public int YjxSongSwitch { get; set; }

        [JsonProperty("is_single_chorus")]
        public int IsSingleChorus { get; set; }

        [JsonProperty("voice_switch")]
        public int VoiceSwitch { get; set; }

        [JsonProperty("use_rebuild_gift_pop_flag")]
        public int UseRebuildGiftPopFlag { get; set; }

        [JsonProperty("forward")]
        public int Forward { get; set; }

        [JsonProperty("chat_act_type")]
        public int ChatActType { get; set; }

        [JsonProperty("use_rebuild_gift_pop_flag1")]
        public int UseRebuildGiftPopFlag1 { get; set; }

        [JsonProperty("room_seat_view_mode")]
        public int RoomSeatViewMode { get; set; }

        [JsonProperty("all_wheat_icon")]
        public string AllWheatIcon { get; set; }

        [JsonProperty("invite_cd_time")]
        public int InviteCdTime { get; set; }

        [JsonProperty("mute_room_message")]
        public bool MuteRoomMessage { get; set; }

        [JsonProperty("bgm_switch")]
        public bool BgmSwitch { get; set; }

        [JsonProperty("task_url")]
        public string TaskUrl { get; set; }

        [JsonProperty("broadcast_msg")]
        public string BroadcastMsg { get; set; }

        [JsonProperty("is_redpacket")]
        public int IsRedpacket { get; set; }

    }
}
