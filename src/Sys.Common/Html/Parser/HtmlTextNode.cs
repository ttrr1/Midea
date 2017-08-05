namespace iPortal.Common.Html.Parser
{
    using System;
    using System.Text;

    internal class HtmlTextNode : HtmlNode
    {
        private string _text;

        internal HtmlTextNode(HtmlDocument ownerdocument, int index) : base(HtmlNodeType.Text, ownerdocument, index)
        {
        }

        private void DeleteTxt(int intOffSet, int intCount)
        {
            int num = (this.Text != null) ? this.Text.Length : 0;
            if ((num > 0) && (num < (intOffSet + intCount)))
            {
                intCount = Math.Max(num - intOffSet, 0);
            }
            string str = new StringBuilder(this.Text).Remove(intOffSet, intCount).ToString();
            this.Text = str;
        }

        public override string InnerHtml
        {
            get
            {
                return this.OuterHtml;
            }
            set
            {
                this._text = value;
            }
        }

        public override string OuterHtml
        {
            get
            {
                if (this._text == null)
                {
                    return base.OuterHtml;
                }
                return this._text;
            }
        }

        public string Text
        {
            get
            {
                if (this._text == null)
                {
                    return base.OuterHtml;
                }
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }
    }
}

