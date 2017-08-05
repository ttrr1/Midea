namespace iPortal.Common.Html.Parser
{
    using System;

    internal class HtmlAttribute : IComparable
    {
        internal int _line;
        internal int _lineposition;
        internal string _name;
        internal int _namelength;
        internal int _namestartindex;
        internal HtmlDocument _ownerdocument;
        internal HtmlNode _ownernode;
        internal int _streamposition;
        internal string _value;
        internal int _valuelength;
        internal int _valuestartindex;

        internal HtmlAttribute(HtmlDocument ownerdocument)
        {
            this._ownerdocument = ownerdocument;
        }

        public HtmlAttribute Clone()
        {
            HtmlAttribute attribute = new HtmlAttribute(this._ownerdocument);
            attribute.Name = this.Name;
            attribute.Value = this.Value;
            return attribute;
        }

        public int CompareTo(object obj)
        {
            HtmlAttribute attribute = obj as HtmlAttribute;
            if (attribute == null)
            {
                throw new ArgumentException("obj");
            }
            return this.Name.CompareTo(attribute.Name);
        }

        public int Line
        {
            get
            {
                return this._line;
            }
        }

        public int LinePosition
        {
            get
            {
                return this._lineposition;
            }
        }

        public string Name
        {
            get
            {
                if (this._name == null)
                {
                    this._name = this._ownerdocument._text.Substring(this._namestartindex, this._namelength).ToLower();
                }
                return this._name;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this._name = value.ToLower();
                if (this._ownernode != null)
                {
                    this._ownernode._innerchanged = true;
                    this._ownernode._outerchanged = true;
                }
            }
        }

        public HtmlDocument OwnerDocument
        {
            get
            {
                return this._ownerdocument;
            }
        }

        public HtmlNode OwnerNode
        {
            get
            {
                return this._ownernode;
            }
        }

        public int StreamPosition
        {
            get
            {
                return this._streamposition;
            }
        }

        public string Value
        {
            get
            {
                if (this._value == null)
                {
                    this._value = this._ownerdocument._text.Substring(this._valuestartindex, this._valuelength);
                }
                return this._value;
            }
            set
            {
                this._value = value;
                if (this._ownernode != null)
                {
                    this._ownernode._innerchanged = true;
                    this._ownernode._outerchanged = true;
                }
            }
        }

        internal string XmlName
        {
            get
            {
                return HtmlDocument.GetXmlName(this.Name);
            }
        }

        internal string XmlValue
        {
            get
            {
                return this.Value;
            }
        }
    }
}

