using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Management;
using Sys.Common;


public partial class Manage_System_ToolServer : System.Web.UI.Page
{
    public string CmsInfo = "";
    public string HardwareInfo = "";
    public string SoftwareInfo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ManageHelper.CheckAdminLogin();
        ManageHelper.CheckAdminPower("system_toolserver");
        CmsInfo += trShow("系统版本号：", Sys.Kernel.Software.Version);
        CmsInfo += trShow("系统数据库：", Sys.Kernel.Software.Database);
       


        //ManagementObjectSearcher searcher14 = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
        //foreach (ManagementObject share in searcher14.Get())
        //{
        //    //HardwareInfo += trShow("CPU * " + searcher14.Get().Count + "：", share["Name"].ToString() + " （二级缓存:" + share["L2CacheSize"].ToString() + "KB）");
        //    SoftwareInfo += trShow("服务器CPU * " + searcher14.Get().Count + "：", share["Name"].ToString() + " （二级缓存:" + share["L2CacheSize"].ToString() + "KB）");
        //    break;
        //}

        //ManagementObjectSearcher searcher12 = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
        //foreach (ManagementObject share in searcher12.Get())
        //{
        //    //HardwareInfo += trShow("操作系统：", share["Caption"].ToString()+ " （版本号：" + share["Version"].ToString()+"）");
        //    //HardwareInfo += trShow("制造商：", share["Manufacturer"].ToString());
        //    //HardwareInfo += trShow("计算机名：", share["csname"].ToString());
        //    //HardwareInfo += trShow("Windows目录：", share["WindowsDirectory"].ToString());
        //    SoftwareInfo += trShow("Windows目录：", share["WindowsDirectory"].ToString());
            
        //}

        

        
        SoftwareInfo += trShow("服务器名称：", Server.MachineName.ToString());
        SoftwareInfo += trShow("操作系统：", Environment.OSVersion.ToString());
        SoftwareInfo += trShow("服务器IP：", Request.ServerVariables["LOCAL_ADDR"]); 
        SoftwareInfo += trShow("服务器域名：", Request.ServerVariables["SERVER_NAME"]);
        SoftwareInfo+=trShow("服务端脚本执行超时：",Server.ScriptTimeout.ToString());
        SoftwareInfo+=trShow("服务器现在时间：",DateTime.Now.ToString());
        SoftwareInfo+=trShow("Session总数：",Session.Contents.Count.ToString());
        SoftwareInfo+=trShow("Application总数：",Application.Contents.Count.ToString());
        SoftwareInfo+=trShow(".NET Framework 版本：",Environment.Version.ToString());
        SoftwareInfo+=trShow("IIS版本：",Request.ServerVariables["SERVER_SOFTWARE"]);
        SoftwareInfo+=trShow("相对路径：",Request.ServerVariables["PATH_INFO"]);
        SoftwareInfo+=trShow("物理路径：",Request.ServerVariables["APPL_PHYSICAL_PATH"]);
        SoftwareInfo+=trShow("运行时间：",(Math.Round(double.Parse((Environment.TickCount/600/60).ToString()))/100).ToString()+"小时");

    }

    private string trShow(string key, string value)
    {
        string str = "";
        str += "<tr class='tdbg' onmouseover='this.className=\"tdbg-dark\"' onmouseout='this.className=\"tdbg\"' style='height:25px;'>";
		str += "<td align='left' style='width:200px; padding-left:20px'>"+key+"</a></td>";
		str += "<td align='left'>"+value+"</td>";
        str +=  "</tr>";
        return str;
    }
}
