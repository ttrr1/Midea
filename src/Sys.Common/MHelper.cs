using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace sz71096.Common
{
    public static class MHelper
    {
        public static int news_page_count = 15;

        public static int width = Convert.ToInt32(ConfigurationManager.AppSettings["width"]);
        public static int height = Convert.ToInt32(ConfigurationManager.AppSettings["height"]);

        public static string GetSpaces(int count)
        {
            string str = "";
            for (int i = 0; i < count; i++)
            {
                str += "　";
            }
            return str;
        }
    }
}
