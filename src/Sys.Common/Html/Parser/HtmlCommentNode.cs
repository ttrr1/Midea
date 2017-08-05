namespace iPortal.Common.Html.Parser
{
    using System;

    internal class HtmlCommentNode : HtmlNode
    {
        private string _comment;

        internal HtmlCommentNode(HtmlDocument ownerdocument, int index) : base(HtmlNodeType.Comment, ownerdocument, index)
        {
        }

        public string Comment
        {
            get
            {
                if (this._comment == null)
                {
                    return base.InnerHtml;
                }
                return this._comment;
            }
            set
            {
                this._comment = value;
            }
        }

        public override string InnerHtml
        {
            get
            {
                if (this._comment == null)
                {
                    return base.InnerHtml;
                }
                return this._comment;
            }
            set
            {
                this._comment = value;
            }
        }

        public override string OuterHtml
        {
            get
            {
                if (this._comment == null)
                {
                    return base.OuterHtml;
                }
                return ("<!--" + this._comment + "-->");
            }
        }
    }
}

