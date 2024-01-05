using System.Collections.Generic;

public class IChatInfo
{

}
public class UserEnterInfo: IChatInfo
{
    public int uid { get; set; }
    public string nickname { get; set; }
    public string avatar { get; set; }
    public int playvip { get; set; }
    public string vipicon { get; set; }
    public string svga_pc { get; set; }
    public string svga_app { get; set; }
    public int show_type { get; set; }
    public List<object> attr { get; set; }
    public string medal { get; set; }
    public string color { get; set; }
    public int teacher { get; set; }
    public int is_show { get; set; }
    public List<object> services { get; set; }
    public int face { get; set; }
    public string share_nickname { get; set; }
    public string share_desc { get; set; }
    public string share_color { get; set; }
    public string name_color { get; set; }
    public string vap_pc_url { get; set; }
    public string vap_pc_config { get; set; }
    public string vap_app_url { get; set; }
    public string vap_app_md5 { get; set; }
    public int is_full { get; set; }
}

public class Bubble_app
{
    /// <summary>
    /// 
    /// </summary>
    public string bg { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string bg2x { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string bgpc { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string bgpc_color { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int bgpc_height { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int bgpc_width { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string icon { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string icon_pc { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string text_color { get; set; }
}

public class Userinfo
{
    /// <summary>
    /// 
    /// </summary>
    public string avatar { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int bubble { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Bubble_app bubble_app { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string color { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int face { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string grade_pic { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> guard { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int isupdate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string match_user_icon { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string medal { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string name_color { get; set; }
    /// <summary>
    /// 向日葵#1128
    /// </summary>
    public string nickname { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string pendant { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string platforms { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int sex { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string share_color { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string share_desc { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string share_nickname { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int teacher { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string time { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int uid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string vipicon { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int viplevel { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int vipuid { get; set; }
}

public class ChatInfo:IChatInfo
{
    /// <summary>
    /// 
    /// </summary>
    public List<string> atUsers { get; set; }
    /// <summary>
    /// 略略略
    /// </summary>
    public string message { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string safe_msg { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string sessionid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int subType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string to { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string uniqid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Userinfo userinfo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string version { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int fid { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int typeid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int cid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string uid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string image { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string payEmoji { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double ratio { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string thumb { get; set; }
}