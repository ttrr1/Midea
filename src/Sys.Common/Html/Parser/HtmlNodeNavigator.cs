namespace iPortal.Common.Html.Parser
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Xml;
    using System.Xml.XPath;

    internal class HtmlNodeNavigator : XPathNavigator, IXPathNavigable
    {
        private int _attindex;
        private HtmlNode _currentnode;
        private HtmlDocument _doc;
        private HtmlNameTable _nametable;
        internal bool Trace;

        internal HtmlNodeNavigator()
        {
            this._doc = new HtmlDocument();
            this._nametable = new HtmlNameTable();
            this.Reset();
        }

        private HtmlNodeNavigator(HtmlNodeNavigator nav)
        {
            this._doc = new HtmlDocument();
            this._nametable = new HtmlNameTable();
            if (nav == null)
            {
                throw new ArgumentNullException("nav");
            }
            this.InternalTrace(null);
            this._doc = nav._doc;
            this._currentnode = nav._currentnode;
            this._attindex = nav._attindex;
            this._nametable = nav._nametable;
        }

        public HtmlNodeNavigator(Stream stream)
        {
            this._doc = new HtmlDocument();
            this._nametable = new HtmlNameTable();
            this._doc.Load(stream);
            this.Reset();
        }

        public HtmlNodeNavigator(TextReader reader)
        {
            this._doc = new HtmlDocument();
            this._nametable = new HtmlNameTable();
            this._doc.Load(reader);
            this.Reset();
        }

        internal HtmlNodeNavigator(HtmlDocument doc, HtmlNode currentNode)
        {
            this._doc = new HtmlDocument();
            this._nametable = new HtmlNameTable();
            if (currentNode == null)
            {
                throw new ArgumentNullException("currentNode");
            }
            if (currentNode.OwnerDocument != doc)
            {
                throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
            }
            this.InternalTrace(null);
            this._doc = doc;
            this.Reset();
            this._currentnode = currentNode;
        }

        public override XPathNavigator Clone()
        {
            this.InternalTrace(null);
            return new HtmlNodeNavigator(this);
        }

        public override string GetAttribute(string localName, string namespaceURI)
        {
            this.InternalTrace("localName=" + localName + ", namespaceURI=" + namespaceURI);
            HtmlAttribute attribute = this._currentnode.Attributes[localName];
            if (attribute == null)
            {
                this.InternalTrace(">null");
                return null;
            }
            this.InternalTrace(">" + attribute.Value);
            return attribute.Value;
        }

        public override string GetNamespace(string name)
        {
            this.InternalTrace("name=" + name);
            return string.Empty;
        }

        [Conditional("TRACE")]
        internal void InternalTrace(object Value)
        {
            string str2;
            string outerHtml;
            if (!this.Trace)
            {
                return;
            }
            string name = null;
            StackFrame frame = new StackFrame(1, true);
            name = frame.GetMethod().Name;
            if (this._currentnode == null)
            {
                str2 = "(null)";
            }
            else
            {
                str2 = this._currentnode.Name;
            }
            if (this._currentnode == null)
            {
                outerHtml = "(null)";
            }
            else
            {
                switch (this._currentnode.NodeType)
                {
                    case HtmlNodeType.Document:
                        outerHtml = "";
                        goto Label_00B1;

                    case HtmlNodeType.Comment:
                        outerHtml = ((HtmlCommentNode) this._currentnode).Comment;
                        goto Label_00B1;

                    case HtmlNodeType.Text:
                        outerHtml = ((HtmlTextNode) this._currentnode).Text;
                        goto Label_00B1;
                }
                outerHtml = this._currentnode.CloneNode(false).OuterHtml;
            }
        Label_00B1:;
            System.Diagnostics.Trace.WriteLine(string.Concat(new object[] { "oid=", this.GetHashCode(), ",n=", str2, ",a=", this._attindex, ",,v=", outerHtml, ",", Value }), "N!" + name);
        }

        public override bool IsSamePosition(XPathNavigator other)
        {
            HtmlNodeNavigator navigator = other as HtmlNodeNavigator;
            if (navigator == null)
            {
                this.InternalTrace(">false");
                return false;
            }
            this.InternalTrace(">" + (navigator._currentnode == this._currentnode));
            return (navigator._currentnode == this._currentnode);
        }

        public override bool MoveTo(XPathNavigator other)
        {
            HtmlNodeNavigator navigator = other as HtmlNodeNavigator;
            if (navigator == null)
            {
                this.InternalTrace(">false (nav is not an HtmlNodeNavigator)");
                return false;
            }
            this.InternalTrace(string.Concat(new object[] { "moveto oid=", navigator.GetHashCode(), ", n:", navigator._currentnode.Name, ", a:", navigator._attindex }));
            if (navigator._doc == this._doc)
            {
                this._currentnode = navigator._currentnode;
                this._attindex = navigator._attindex;
                this.InternalTrace(">true");
                return true;
            }
            this.InternalTrace(">false (???)");
            return false;
        }

        public override bool MoveToAttribute(string localName, string namespaceURI)
        {
            this.InternalTrace("localName=" + localName + ", namespaceURI=" + namespaceURI);
            int attributeIndex = this._currentnode.Attributes.GetAttributeIndex(localName);
            if (attributeIndex == -1)
            {
                this.InternalTrace(">false");
                return false;
            }
            this._attindex = attributeIndex;
            this.InternalTrace(">true");
            return true;
        }

        public override bool MoveToFirst()
        {
            if (this._currentnode.ParentNode == null)
            {
                this.InternalTrace(">false");
                return false;
            }
            if (this._currentnode.ParentNode.FirstChild == null)
            {
                this.InternalTrace(">false");
                return false;
            }
            this._currentnode = this._currentnode.ParentNode.FirstChild;
            this.InternalTrace(">true");
            return true;
        }

        public override bool MoveToFirstAttribute()
        {
            if (!this.HasAttributes)
            {
                this.InternalTrace(">false");
                return false;
            }
            this._attindex = 0;
            this.InternalTrace(">true");
            return true;
        }

        public override bool MoveToFirstChild()
        {
            if (!this._currentnode.HasChildNodes)
            {
                this.InternalTrace(">false");
                return false;
            }
            this._currentnode = this._currentnode.ChildNodes[0];
            this.InternalTrace(">true");
            return true;
        }

        public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
        {
            this.InternalTrace(null);
            return false;
        }

        public override bool MoveToId(string id)
        {
            this.InternalTrace("id=" + id);
            HtmlNode elementbyId = this._doc.GetElementbyId(id);
            if (elementbyId == null)
            {
                this.InternalTrace(">false");
                return false;
            }
            this._currentnode = elementbyId;
            this.InternalTrace(">true");
            return true;
        }

        public override bool MoveToNamespace(string name)
        {
            this.InternalTrace("name=" + name);
            return false;
        }

        public override bool MoveToNext()
        {
            if (this._currentnode.NextSibling == null)
            {
                this.InternalTrace(">false");
                return false;
            }
            this.InternalTrace("_c=" + this._currentnode.CloneNode(false).OuterHtml);
            this.InternalTrace("_n=" + this._currentnode.NextSibling.CloneNode(false).OuterHtml);
            this._currentnode = this._currentnode.NextSibling;
            this.InternalTrace(">true");
            return true;
        }

        public override bool MoveToNextAttribute()
        {
            this.InternalTrace(null);
            if (this._attindex >= (this._currentnode.Attributes.Count - 1))
            {
                this.InternalTrace(">false");
                return false;
            }
            this._attindex++;
            this.InternalTrace(">true");
            return true;
        }

        public override bool MoveToNextNamespace(XPathNamespaceScope scope)
        {
            this.InternalTrace(null);
            return false;
        }

        public override bool MoveToParent()
        {
            if (this._currentnode.ParentNode == null)
            {
                this.InternalTrace(">false");
                return false;
            }
            this._currentnode = this._currentnode.ParentNode;
            this.InternalTrace(">true");
            return true;
        }

        public override bool MoveToPrevious()
        {
            if (this._currentnode.PreviousSibling == null)
            {
                this.InternalTrace(">false");
                return false;
            }
            this._currentnode = this._currentnode.PreviousSibling;
            this.InternalTrace(">true");
            return true;
        }

        public override void MoveToRoot()
        {
            this._currentnode = this._doc.DocumentNode;
            this.InternalTrace(null);
        }

        private void Reset()
        {
            this.InternalTrace(null);
            this._currentnode = this._doc.DocumentNode;
            this._attindex = -1;
        }

        public override string BaseURI
        {
            get
            {
                this.InternalTrace(">");
                return this._nametable.GetOrAdd(string.Empty);
            }
        }

        public HtmlDocument CurrentDocument
        {
            get
            {
                return this._doc;
            }
        }

        public HtmlNode CurrentNode
        {
            get
            {
                return this._currentnode;
            }
        }

        public override bool HasAttributes
        {
            get
            {
                this.InternalTrace(">" + (this._currentnode.Attributes.Count > 0));
                return (this._currentnode.Attributes.Count > 0);
            }
        }

        public override bool HasChildren
        {
            get
            {
                this.InternalTrace(">" + (this._currentnode.ChildNodes.Count > 0));
                return (this._currentnode.ChildNodes.Count > 0);
            }
        }

        public override bool IsEmptyElement
        {
            get
            {
                this.InternalTrace(">" + !this.HasChildren);
                return !this.HasChildren;
            }
        }

        public override string LocalName
        {
            get
            {
                if (this._attindex != -1)
                {
                    this.InternalTrace("att>" + this._currentnode.Attributes[this._attindex].Name);
                    return this._nametable.GetOrAdd(this._currentnode.Attributes[this._attindex].Name);
                }
                this.InternalTrace("node>" + this._currentnode.Name);
                return this._nametable.GetOrAdd(this._currentnode.Name);
            }
        }

        public override string Name
        {
            get
            {
                this.InternalTrace(">" + this._currentnode.Name);
                return this._nametable.GetOrAdd(this._currentnode.Name);
            }
        }

        public override string NamespaceURI
        {
            get
            {
                this.InternalTrace(">");
                return this._nametable.GetOrAdd(string.Empty);
            }
        }

        public override XmlNameTable NameTable
        {
            get
            {
                this.InternalTrace(null);
                return this._nametable;
            }
        }

        public override XPathNodeType NodeType
        {
            get
            {
                switch (this._currentnode.NodeType)
                {
                    case HtmlNodeType.Document:
                        this.InternalTrace(">" + XPathNodeType.Root);
                        return XPathNodeType.Root;

                    case HtmlNodeType.Element:
                        if (this._attindex == -1)
                        {
                            this.InternalTrace(">" + XPathNodeType.Element);
                            return XPathNodeType.Element;
                        }
                        this.InternalTrace(">" + XPathNodeType.Attribute);
                        return XPathNodeType.Attribute;

                    case HtmlNodeType.Comment:
                        this.InternalTrace(">" + XPathNodeType.Comment);
                        return XPathNodeType.Comment;

                    case HtmlNodeType.Text:
                        this.InternalTrace(">" + XPathNodeType.Text);
                        return XPathNodeType.Text;
                }
                throw new NotImplementedException("Internal error: Unhandled HtmlNodeType: " + this._currentnode.NodeType);
            }
        }

        public override string Prefix
        {
            get
            {
                this.InternalTrace(null);
                return this._nametable.GetOrAdd(string.Empty);
            }
        }

        public override string Value
        {
            get
            {
                this.InternalTrace("nt=" + this._currentnode.NodeType);
                switch (this._currentnode.NodeType)
                {
                    case HtmlNodeType.Document:
                        this.InternalTrace(">");
                        return "";

                    case HtmlNodeType.Element:
                        if (this._attindex == -1)
                        {
                            return this._currentnode.InnerText;
                        }
                        this.InternalTrace(">" + this._currentnode.Attributes[this._attindex].Value);
                        return this._currentnode.Attributes[this._attindex].Value;

                    case HtmlNodeType.Comment:
                        this.InternalTrace(">" + ((HtmlCommentNode) this._currentnode).Comment);
                        return ((HtmlCommentNode) this._currentnode).Comment;

                    case HtmlNodeType.Text:
                        this.InternalTrace(">" + ((HtmlTextNode) this._currentnode).Text);
                        return ((HtmlTextNode) this._currentnode).Text;
                }
                throw new NotImplementedException("Internal error: Unhandled HtmlNodeType: " + this._currentnode.NodeType);
            }
        }

        public override string XmlLang
        {
            get
            {
                this.InternalTrace(null);
                return this._nametable.GetOrAdd(string.Empty);
            }
        }
    }
}

