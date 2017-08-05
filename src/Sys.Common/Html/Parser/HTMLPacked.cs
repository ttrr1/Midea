namespace iPortal.Common.Html.Parser
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    public class HTMLPacked
    {
        public string[] AllowedAttributes = new string[] { "class", "href", "target", "border", "src", "align", "width", "height", "color", "size", "rel", "alt" };
        public string[] AllowedTags = new string[] { 
            "p", "b", "i", "u", "em", "big", "small", "div", "img", "span", "blockquote", "strike", "code", "pre", "br", "ul", 
            "ol", "li", "del", "ins", "strong", "a", "font", "dl", "dd", "dt", "h6"
         };

        public string Parser(string strVal)
        {
            if (!string.IsNullOrEmpty(strVal))
            {
                var document = new HtmlDocument
                                   {
                                       AllowedAttributes = this.AllowedAttributes,
                                       AllowedTags = this.AllowedTags,
                                       OptionAutoCloseOnEnd = true,
                                       OptionFixNestedTags = true
                                   };
                document.LoadHtml(strVal);
                var writer = new StringWriter();
                document.Save(writer);
                strVal = writer.ToString();
                strVal = Regex.Replace(strVal, @"[\n\r]+", "\n");
                writer.Close();
            }
            return strVal;
        }
    }
}

