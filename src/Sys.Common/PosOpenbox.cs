using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Sys.Common
{
    public class PosOpenbox
    {
        const int OPEN_EXISTING = 3;
        readonly string _prnPort = "LPT1";
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFile(string lpFileName,
        int dwDesiredAccess,
        int dwShareMode,
        int lpSecurityAttributes,
        int dwCreationDisposition,
        int dwFlagsAndAttributes,
        int hTemplateFile);
        public PosOpenbox()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public PosOpenbox(string prnPort)
        {
            this._prnPort = prnPort;
        }
        public string PrintLine()
        {
            var iHandle = CreateFile(_prnPort, 0x40000000, 0, 0, OPEN_EXISTING, 0, 0);
            if (iHandle.ToInt32() == -1)
            {
                return "打开LPT1失败";
            }
            var fs = new FileStream(iHandle, FileAccess.ReadWrite);
            var sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(((char)27) + "p" + ((char)0) + ((char)60) + ((char)255));
            sw.Close();
            fs.Close();
            return "";
        }
    }
}