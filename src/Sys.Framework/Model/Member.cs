using System;
namespace Sys.Model
{
    /// <summary>
    /// 实体类Member 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Member
    {
        public Member()
        { }
        #region Model
        private int _userid;
        private string _username;
        private int _groupid;
        private string _groupname;
        private string _email;
        private int _emailstate;
        private DateTime _createtime;
        private string _createip;
        private string _question;
        private string _answer;
        private int _logintimes;
        private DateTime _lastlogintime;
        private string _lastloginip;
        private DateTime _thislogintime;
        private string _thisloginip;
        private DateTime _activetime;
        private int _state=1;
        private int _isvip;
        private DateTime _vipstarttime;
        private DateTime _vipendtime;
        private DateTime _birthday;
        private string _realname;
        private string _sign;
        private string _qq;
        private string _msn;
        private string _guid;
        private int _integral;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 用户组ID
        /// </summary>
        public int GroupID
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        /// <summary>
        /// 用户组名字
        /// </summary>
        public string GroupName
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// Emaik状态 1激活0没激活
        /// </summary>
        public int EmailState
        {
            set { _emailstate = value; }
            get { return _emailstate; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 创建ＩＰ
        /// </summary>
        public string CreateIP
        {
            set { _createip = value; }
            get { return _createip; }
        }
        /// <summary>
        /// 找回密码问题
        /// </summary>
        public string Question
        {
            set { _question = value; }
            get { return _question; }
        }
        /// <summary>
        /// 找回密码答案
        /// </summary>
        public string Answer
        {
            set { _answer = value; }
            get { return _answer; }
        }
        /// <summary>
        /// 登陆次数
        /// </summary>
        public int LoginTimes
        {
            set { _logintimes = value; }
            get { return _logintimes; }
        }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime
        {
            set { _lastlogintime = value; }
            get { return _lastlogintime; }
        }
        /// <summary>
        /// 上次登录ＩＰ
        /// </summary>
        public string LastLoginIP
        {
            set { _lastloginip = value; }
            get { return _lastloginip; }
        }
        /// <summary>
        /// 本次登录时间
        /// </summary>
        public DateTime ThisLoginTime
        {
            set { _thislogintime = value; }
            get { return _thislogintime; }
        }
        /// <summary>
        /// 本次登录ｉｐ
        /// </summary>
        public string ThisLoginIP
        {
            set { _thisloginip = value; }
            get { return _thisloginip; }
        }
        /// <summary>
        /// 最后活动时间
        /// </summary>
        public DateTime ActiveTime
        {
            set { _activetime = value; }
            get { return _activetime; }
        }
        /// <summary>
        /// 帐户状态 0锁定 1正常
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 是否是ＶＩＰ
        /// </summary>
        public int IsVIP
        {
            set { _isvip = value; }
            get { return _isvip; }
        }
        /// <summary>
        /// ＶＩＰ开始时间
        /// </summary>
        public DateTime VIPStartTime
        {
            set { _vipstarttime = value; }
            get { return _vipstarttime; }
        }
        /// <summary>
        /// ＶＩＰ结束时间
        /// </summary>
        public DateTime VIPEndTime
        {
            set { _vipendtime = value; }
            get { return _vipendtime; }
        }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName
        {
            set { _realname = value; }
            get { return _realname; }
        }
        /// <summary>
        /// 签名
        /// </summary>
        public string Sign
        {
            set { _sign = value; }
            get { return _sign; }
        }
        /// <summary>
        /// ＱＱ
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>
        /// ＭＳＮ
        /// </summary>
        public string MSN
        {
            set { _msn = value; }
            get { return _msn; }
        }
        /// <summary>
        /// GUID
        /// </summary>
        public string Guid
        {
            set { _guid = value; }
            get { return _guid; }
        }

        /// <summary>
        /// 用户积分
        /// </summary>
        public int Integral
        {
            set { _integral = value; }
            get { return _integral; }
        }

        #endregion Model

    }
}

