using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Sys.Common
{
    public class CustomerDisplay
    {
        private string spPortName;
        private int spBaudRate;
        private StopBits spStopBits;
        private int spDataBits;

        /// <summary>  
        /// 客显发送类型  
        /// </summary>  
        public CustomerDispiayType DispiayType { get; set; }

        /// <summary>  
        /// 构造函数  
        /// </summary>  
        /// <param name="_spPortName">端口名称（COM1,COM2，COM3...）</param>  
        /// <param name="_spBaudRate">通信波特率（2400,9600....）</param>  
        /// <param name="_spStopBits">停止位</param>  
        /// <param name="_spDataBits">数据位</param>  
        public CustomerDisplay(string _spPortName, int _spBaudRate, string _spStopBits, int _spDataBits)
        {
            this.spBaudRate = _spBaudRate;
            this.spDataBits = _spDataBits;
            this.spPortName = _spPortName;
            this.spStopBits = (StopBits)(Enum.Parse(typeof(StopBits), _spStopBits));
        }

        /// <summary>  
        /// 数据信息展现  
        /// </summary>  
        /// <param name="data">发送的数据（清屏可以为null或者空）</param>  
        public void DisplayData(string data)
        {
            SerialPort serialPort = new SerialPort();
            serialPort.PortName = spPortName;
            serialPort.BaudRate = spBaudRate;
            serialPort.StopBits = spStopBits;
            serialPort.DataBits = spDataBits;
            serialPort.Open();

            //先清屏  
            serialPort.WriteLine(((char)12).ToString());
            //指示灯  
            string str = ((char)27) + @"s" + Convert.ToInt32(DispiayType);

            serialPort.WriteLine(str);

            //发送数据  
            if (!string.IsNullOrEmpty(data))
            {
                serialPort.WriteLine(((char)27).ToString() + ((char)81).ToString() + ((char)65).ToString() + data + ((char)13).ToString());
            }

            serialPort.Close();
        }

    }
}
