using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Sys.Common
{
    public class PosPrinter
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
        public PosPrinter()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public PosPrinter(string prnPort)
        {
            _prnPort = prnPort;//打印机端口
        }
        public string PrintLine(string str)
        {
            // const  OPEN_EXISTING = 3;
            // const   GENERIC_READ =&H80000000;
            // const  GENERIC_WRITE = &H40000000;
            var iHandle = CreateFile(_prnPort, 0x50000000, 0, 0, OPEN_EXISTING, 0, 0);
            if (iHandle.ToInt32() == -1)
            {
                Console.WriteLine(iHandle.ToString());
                return "没有连接打印机或者打印机端口不是LPT1";

            }
            Console.WriteLine(iHandle.ToString());
            var fs = new FileStream(iHandle, FileAccess.ReadWrite);
            var sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine("        苏州市平价超市       ");

            //var tmp = "<table><tr><td>1</td><td>2</td><td>3</td></tr></table>";

            //sw.WriteLine("标头");
            sw.WriteLine("-------------------------------");
            sw.WriteLine("商品名称   数量    单价   小计");
            sw.WriteLine(str);
            sw.WriteLine(); sw.WriteLine(); sw.WriteLine(); sw.WriteLine(); sw.WriteLine();

            sw.Close();
            fs.Close();
            return "打印机连接成功";
        }

    }
}
