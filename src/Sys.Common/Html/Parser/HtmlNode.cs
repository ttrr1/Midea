namespace iPortal.Common.Html.Parser
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Xml.XPath;

    internal class HtmlNode : IXPathNavigable
    {
        internal HtmlAttributeCollection _attributes;
        internal HtmlNodeCollection _childnodes;
        internal HtmlNode _endnode;
        internal bool _innerchanged;
        internal string _innerhtml;
        internal int _innerlength;
        internal int _innerstartindex;
        internal int _line;
        internal int _lineposition;
        internal string _name;
        internal int _namelength;
        internal int _namestartindex;
        internal HtmlNode _nextnode;
        internal HtmlNodeType _nodetype;
        internal bool _outerchanged;
        internal string _outerhtml;
        internal int _outerlength;
        internal int _outerstartindex;
        internal HtmlDocument _ownerdocument;
        internal HtmlNode _parentnode;
        internal HtmlNode _prevnode;
        internal HtmlNode _prevwithsamename;
        internal bool _starttag;
        internal int _streamposition;
        public static Hashtable ElementsFlags = new Hashtable();
        public static readonly string HtmlNodeTypeNameComment = "#comment";
        public static readonly string HtmlNodeTypeNameDocument = "#document";
        public static readonly string HtmlNodeTypeNameText = "#text";

        static HtmlNode()
        {
            ElementsFlags.Add("script", HtmlElementFlag.CData);
            ElementsFlags.Add("style", HtmlElementFlag.CData);
            ElementsFlags.Add("noxhtml", HtmlElementFlag.CData);
            ElementsFlags.Add("base", HtmlElementFlag.Empty);
            //ElementsFlags.Add("link", HtmlElementFlag.Empty);
            ElementsFlags.Add("meta", HtmlElementFlag.Empty);
            ElementsFlags.Add("isindex", HtmlElementFlag.Empty);
            ElementsFlags.Add("hr", HtmlElementFlag.Empty);
            ElementsFlags.Add("col", HtmlElementFlag.Empty);
            ElementsFlags.Add("img", HtmlElementFlag.Empty);
            ElementsFlags.Add("param", HtmlElementFlag.Empty);
            ElementsFlags.Add("embed", HtmlElementFlag.Empty);
            ElementsFlags.Add("frame", HtmlElementFlag.Empty);
            ElementsFlags.Add("wbr", HtmlElementFlag.Empty);
            ElementsFlags.Add("bgsound", HtmlElementFlag.Empty);
            ElementsFlags.Add("spacer", HtmlElementFlag.Empty);
            ElementsFlags.Add("keygen", HtmlElementFlag.Empty);
            ElementsFlags.Add("area", HtmlElementFlag.Empty);
            ElementsFlags.Add("input", HtmlElementFlag.Empty);
            ElementsFlags.Add("basefont", HtmlElementFlag.Empty);
            ElementsFlags.Add("form", HtmlElementFlag.CanOverlap | HtmlElementFlag.Empty);
            ElementsFlags.Add("option", HtmlElementFlag.Empty);
            ElementsFlags.Add("br", HtmlElementFlag.Closed | HtmlElementFlag.Empty);
        }

        internal HtmlNode(HtmlNodeType type, HtmlDocument ownerdocument, int index)
        {
            this._nodetype = type;
            this._ownerdocument = ownerdocument;
            this._outerstartindex = index;
            switch (type)
            {
                case HtmlNodeType.Document:
                    this._name = HtmlNodeTypeNameDocument;
                    this._endnode = this;
                    break;

                case HtmlNodeType.Comment:
                    this._name = HtmlNodeTypeNameComment;
                    this._endnode = this;
                    break;

                case HtmlNodeType.Text:
                    this._name = HtmlNodeTypeNameText;
                    this._endnode = this;
                    break;
            }
            if (((this._ownerdocument._openednodes != null) && !this.Closed) && (-1 != index))
            {
                this._ownerdocument._openednodes.Add(index, this);
            }
            if (((-1 == index) && (type != HtmlNodeType.Comment)) && (type != HtmlNodeType.Text))
            {
                this._outerchanged = true;
                this._innerchanged = true;
            }
        }

        public HtmlNode AppendChild(HtmlNode newChild)
        {
            if (newChild == null)
            {
                throw new ArgumentNullException("newChild");
            }
            this.ChildNodes.Append(newChild);
            this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
            this._outerchanged = true;
            this._innerchanged = true;
            return newChild;
        }

        public void AppendChildren(HtmlNodeCollection newChildren)
        {
            if (newChildren == null)
            {
                throw new ArgumentNullException("newChildrend");
            }
            foreach (HtmlNode node in newChildren)
            {
                this.AppendChild(node);
            }
        }

        public static bool CanOverlapElement(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            object obj2 = ElementsFlags[name.ToLower()];
            if (obj2 == null)
            {
                return false;
            }
            return ((((HtmlElementFlag) obj2) & HtmlElementFlag.CanOverlap) != ((HtmlElementFlag) 0));
        }

        public HtmlNode Clone()
        {
            return this.CloneNode(true);
        }

        public HtmlNode CloneNode(bool deep)
        {
            HtmlNode node = this._ownerdocument.CreateNode(this._nodetype);
            node._name = this.Name;
            switch (this._nodetype)
            {
                case HtmlNodeType.Comment:
                    ((HtmlCommentNode) node).Comment = ((HtmlCommentNode) this).Comment;
                    return node;

                case HtmlNodeType.Text:
                    ((HtmlTextNode) node).Text = ((HtmlTextNode) this).Text;
                    return node;
            }
            if (this.HasAttributes)
            {
                using (HtmlAttributeCollection.HtmlAttributeEnumerator enumerator = this._attributes.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        HtmlAttribute newAttribute = enumerator.Current.Clone();
                        node.Attributes.Append(newAttribute);
                    }
                }
            }
            if (this.HasClosingAttributes)
            {
                node._endnode = this._endnode.CloneNode(false);
                using (HtmlAttributeCollection.HtmlAttributeEnumerator enumerator2 = this._endnode._attributes.GetEnumerator())
                {
                    while (enumerator2.MoveNext())
                    {
                        HtmlAttribute attribute4 = enumerator2.Current.Clone();
                        node._endnode._attributes.Append(attribute4);
                    }
                }
            }
            if (deep)
            {
                if (!this.HasChildNodes)
                {
                    return node;
                }
                using (HtmlNodeCollection.HtmlNodeEnumerator enumerator3 = this._childnodes.GetEnumerator())
                {
                    while (enumerator3.MoveNext())
                    {
                        HtmlNode newChild = enumerator3.Current.Clone();
                        node.AppendChild(newChild);
                    }
                }
            }
            return node;
        }

        public HtmlNode CloneNode(string newName)
        {
            return this.CloneNode(newName, true);
        }

        public HtmlNode CloneNode(string newName, bool deep)
        {
            if (newName == null)
            {
                throw new ArgumentNullException("newName");
            }
            HtmlNode node = this.CloneNode(deep);
            node._name = newName;
            return node;
        }

        internal void CloseNode(HtmlNode endnode)
        {
            if (!this._ownerdocument.OptionAutoCloseOnEnd && (this._childnodes != null))
            {
                foreach (HtmlNode node in this._childnodes)
                {
                    if (!node.Closed)
                    {
                        HtmlNode node2 = new HtmlNode(this.NodeType, this._ownerdocument, -1);
                        node2._endnode = node2;
                        node.CloseNode(node2);
                    }
                }
            }
            if (!this.Closed)
            {
                this._endnode = endnode;
                if (this._ownerdocument._openednodes != null)
                {
                    this._ownerdocument._openednodes.Remove(this._outerstartindex);
                }
                HtmlNode node3 = this._ownerdocument._lastnodes[this.Name] as HtmlNode;
                if (node3 == this)
                {
                    this._ownerdocument._lastnodes.Remove(this.Name);
                    this._ownerdocument.UpdateLastParentNode();
                }
                if (endnode != this)
                {
                    this._innerstartindex = this._outerstartindex + this._outerlength;
                    this._innerlength = endnode._outerstartindex - this._innerstartindex;
                    this._outerlength = (endnode._outerstartindex + endnode._outerlength) - this._outerstartindex;
                }
            }
        }

        public void CopyFrom(HtmlNode node)
        {
            this.CopyFrom(node, true);
        }

        public void CopyFrom(HtmlNode node, bool deep)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            this.Attributes.RemoveAll();
            if (node.HasAttributes)
            {
                foreach (HtmlAttribute attribute in node.Attributes)
                {
                    this.SetAttributeValue(attribute.Name, attribute.Value);
                }
            }
            if (!deep)
            {
                this.RemoveAllChildren();
                if (node.HasChildNodes)
                {
                    foreach (HtmlNode node2 in node.ChildNodes)
                    {
                        this.AppendChild(node2.CloneNode(true));
                    }
                }
            }
        }

        public XPathNavigator CreateNavigator()
        {
            return new HtmlNodeNavigator(this._ownerdocument, this);
        }

        public static HtmlNode CreateNode(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            return document.DocumentNode.FirstChild;
        }

        public bool GetAttributeValue(string name, bool def)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (!this.HasAttributes)
            {
                return def;
            }
            HtmlAttribute attribute = this.Attributes[name];
            if (attribute == null)
            {
                return def;
            }
            try
            {
                return Convert.ToBoolean(attribute.Value);
            }
            catch
            {
                return def;
            }
        }

        public int GetAttributeValue(string name, int def)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (!this.HasAttributes)
            {
                return def;
            }
            HtmlAttribute attribute = this.Attributes[name];
            if (attribute == null)
            {
                return def;
            }
            try
            {
                return Convert.ToInt32(attribute.Value);
            }
            catch
            {
                return def;
            }
        }

        public string GetAttributeValue(string name, string def)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (!this.HasAttributes)
            {
                return def;
            }
            HtmlAttribute attribute = this.Attributes[name];
            if (attribute == null)
            {
                return def;
            }
            return attribute.Value;
        }

        internal string GetId()
        {
            HtmlAttribute attribute = this.Attributes["id"];
            if (attribute == null)
            {
                return null;
            }
            return attribute.Value;
        }

        public HtmlNode InsertAfter(HtmlNode newChild, HtmlNode refChild)
        {
            if (newChild == null)
            {
                throw new ArgumentNullException("newChild");
            }
            if (refChild == null)
            {
                return this.PrependChild(newChild);
            }
            if (newChild != refChild)
            {
                int num = -1;
                if (this._childnodes != null)
                {
                    num = this._childnodes[refChild];
                }
                if (num == -1)
                {
                    throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
                }
                this._childnodes.Insert(num + 1, newChild);
                this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
                this._outerchanged = true;
                this._innerchanged = true;
            }
            return newChild;
        }

        public HtmlNode InsertBefore(HtmlNode newChild, HtmlNode refChild)
        {
            if (newChild == null)
            {
                throw new ArgumentNullException("newChild");
            }
            if (refChild == null)
            {
                return this.AppendChild(newChild);
            }
            if (newChild != refChild)
            {
                int index = -1;
                if (this._childnodes != null)
                {
                    index = this._childnodes[refChild];
                }
                if (index == -1)
                {
                    throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
                }
                this._childnodes.Insert(index, newChild);
                this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
                this._outerchanged = true;
                this._innerchanged = true;
            }
            return newChild;
        }

        public static bool IsCDataElement(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            object obj2 = ElementsFlags[name.ToLower()];
            if (obj2 == null)
            {
                return false;
            }
            return ((((HtmlElementFlag) obj2) & HtmlElementFlag.CData) != ((HtmlElementFlag) 0));
        }

        public static bool IsClosedElement(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            object obj2 = ElementsFlags[name.ToLower()];
            if (obj2 == null)
            {
                return false;
            }
            return ((((HtmlElementFlag) obj2) & HtmlElementFlag.Closed) != ((HtmlElementFlag) 0));
        }

        public static bool IsEmptyElement(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (name.Length == 0)
            {
                return true;
            }
            if ('!' == name[0])
            {
                return true;
            }
            if ('?' == name[0])
            {
                return true;
            }
            object obj2 = ElementsFlags[name.ToLower()];
            if (obj2 == null)
            {
                return false;
            }
            return ((((HtmlElementFlag) obj2) & HtmlElementFlag.Empty) != ((HtmlElementFlag) 0));
        }

        private static bool IsExistInList(string strVal, string[] stcVal)
        {
            foreach (string str in stcVal)
            {
                if ((!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(strVal)) && (string.Compare(str, strVal, true) == 0))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsOverlappedClosingElement(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            if (text.Length <= 4)
            {
                return false;
            }
            return ((((text[0] == '<') && (text[text.Length - 1] == '>')) && (text[1] == '/')) && CanOverlapElement(text.Substring(2, text.Length - 3)));
        }

        public HtmlNode PrependChild(HtmlNode newChild)
        {
            if (newChild == null)
            {
                throw new ArgumentNullException("newChild");
            }
            this.ChildNodes.Prepend(newChild);
            this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
            this._outerchanged = true;
            this._innerchanged = true;
            return newChild;
        }

        public void PrependChildren(HtmlNodeCollection newChildren)
        {
            if (newChildren == null)
            {
                throw new ArgumentNullException("newChildren");
            }
            foreach (HtmlNode node in newChildren)
            {
                this.PrependChild(node);
            }
        }

        public void RemoveAll()
        {
            this.RemoveAllChildren();
            if (this.HasAttributes)
            {
                this._attributes.Clear();
            }
            if (((this._endnode != null) && (this._endnode != this)) && (this._endnode._attributes != null))
            {
                this._endnode._attributes.Clear();
            }
            this._outerchanged = true;
            this._innerchanged = true;
        }

        public void RemoveAllChildren()
        {
            if (this.HasChildNodes)
            {
                if (this._ownerdocument.OptionUseIdAttribute)
                {
                    foreach (HtmlNode node in this._childnodes)
                    {
                        this._ownerdocument.SetIdForNode(null, node.GetId());
                    }
                }
                this._childnodes.Clear();
                this._outerchanged = true;
                this._innerchanged = true;
            }
        }

        public HtmlNode RemoveChild(HtmlNode oldChild)
        {
            if (oldChild == null)
            {
                throw new ArgumentNullException("oldChild");
            }
            int index = -1;
            if (this._childnodes != null)
            {
                index = this._childnodes[oldChild];
            }
            if (index == -1)
            {
                throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
            }
            this._childnodes.Remove(index);
            this._ownerdocument.SetIdForNode(null, oldChild.GetId());
            this._outerchanged = true;
            this._innerchanged = true;
            return oldChild;
        }

        public HtmlNode RemoveChild(HtmlNode oldChild, bool keepGrandChildren)
        {
            if (oldChild == null)
            {
                throw new ArgumentNullException("oldChild");
            }
            if ((oldChild._childnodes != null) && keepGrandChildren)
            {
                HtmlNode previousSibling = oldChild.PreviousSibling;
                foreach (HtmlNode node2 in oldChild._childnodes)
                {
                    this.InsertAfter(node2, previousSibling);
                }
            }
            this.RemoveChild(oldChild);
            this._outerchanged = true;
            this._innerchanged = true;
            return oldChild;
        }

        public HtmlNode ReplaceChild(HtmlNode newChild, HtmlNode oldChild)
        {
            if (newChild == null)
            {
                return this.RemoveChild(oldChild);
            }
            if (oldChild == null)
            {
                return this.AppendChild(newChild);
            }
            int index = -1;
            if (this._childnodes != null)
            {
                index = this._childnodes[oldChild];
            }
            if (index == -1)
            {
                throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
            }
            this._childnodes.Replace(index, newChild);
            this._ownerdocument.SetIdForNode(null, oldChild.GetId());
            this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
            this._outerchanged = true;
            this._innerchanged = true;
            return newChild;
        }

        public HtmlNodeCollection SelectNodes(string xpath)
        {
            HtmlNodeCollection nodes = new HtmlNodeCollection(null);
            XPathNodeIterator iterator = new HtmlNodeNavigator(this._ownerdocument, this).Select(xpath);
            while (iterator.MoveNext())
            {
                HtmlNodeNavigator current = (HtmlNodeNavigator) iterator.Current;
                nodes.Add(current.CurrentNode);
            }
            if (nodes.Count == 0)
            {
                return null;
            }
            return nodes;
        }

        public HtmlNode SelectSingleNode(string xpath)
        {
            if (xpath == null)
            {
                throw new ArgumentNullException("xpath");
            }
            XPathNodeIterator iterator = new HtmlNodeNavigator(this._ownerdocument, this).Select(xpath);
            if (!iterator.MoveNext())
            {
                return null;
            }
            HtmlNodeNavigator current = (HtmlNodeNavigator) iterator.Current;
            return current.CurrentNode;
        }

        public HtmlAttribute SetAttributeValue(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            HtmlAttribute attribute = this.Attributes[name];
            if (attribute == null)
            {
                return this.Attributes.Append(this._ownerdocument.CreateAttribute(name, value));
            }
            attribute.Value = value;
            return attribute;
        }

        internal void SetId(string id)
        {
            HtmlAttribute attribute = this.Attributes["id"];
            if (attribute == null)
            {
                attribute = this._ownerdocument.CreateAttribute("id");
            }
            attribute.Value = id;
            this._ownerdocument.SetIdForNode(this, attribute.Value);
            this._outerchanged = true;
        }

        internal void WriteAttribute(TextWriter outText, HtmlAttribute att)
        {
            string name = att.Name;
            if (IsExistInList(att.Name, this._ownerdocument.AllowedAttributes))
            {
                outText.Write(" " + name + "=\"" + att.Value + "\"");
            }
        }

        internal void WriteAttributes(TextWriter outText)
        {
            if (this._attributes != null)
            {
                bool flag = false;
                foreach (HtmlAttribute attribute in this._attributes)
                {
                    if ((string.Compare(attribute.Name, "href", true) == 0) && (string.Compare(attribute.Name, "target", true) != 0))
                    {
                        flag = true;
                    }
                    this.WriteAttribute(outText, attribute);
                    if (flag)
                    {
                        outText.Write(" target=\"_blank\"");
                    }
                }
            }
        }

        public string WriteContentTo()
        {
            StringWriter outText = new StringWriter();
            this.WriteContentTo(outText);
            outText.Flush();
            return outText.ToString();
        }

        public void WriteContentTo(TextWriter outText)
        {
            if (this._childnodes != null)
            {
                using (HtmlNodeCollection.HtmlNodeEnumerator enumerator = this._childnodes.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.WriteTo(outText);
                    }
                }
            }
        }

        public string WriteTo()
        {
            StringWriter outText = new StringWriter();
            this.WriteTo(outText);
            outText.Flush();
            return outText.ToString();
        }

        public void WriteTo(TextWriter outText)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = false;
            bool flag8 = false;
            switch (this._nodetype)
            {
                case HtmlNodeType.Document:
                    this.WriteContentTo(outText);
                    return;

                case HtmlNodeType.Element:
                {
                    string name = this.Name;
                    if (!IsExistInList(name, this._ownerdocument.AllowedTags))
                    {
                        break;
                    }
                    if (this.HasChildNodes)
                    {
                        outText.Write("<" + name);
                        this.WriteAttributes(outText);
                        outText.Write(">");
                        this.WriteContentTo(outText);
                        outText.Write("</" + name);
                        outText.Write(">");
                        break;
                    }
                    if (!IsEmptyElement(this.Name) || (this.Name.Length <= 0))
                    {
                        break;
                    }
                    HtmlNode parentNode = this.ParentNode;
                    flag4 = (parentNode == null) || (parentNode.NodeType == HtmlNodeType.Document);
                    HtmlNode nextSibling = this.NextSibling;
                    flag5 = nextSibling != null;
                    HtmlNode node6 = flag5 ? nextSibling.ParentNode : null;
                    flag7 = (node6 == null) || (node6.NodeType == HtmlNodeType.Document);
                    HtmlNode previousSibling = this.PreviousSibling;
                    flag6 = previousSibling != null;
                    HtmlNode node8 = flag6 ? previousSibling.ParentNode : null;
                    flag8 = (node8 == null) || (node8.NodeType == HtmlNodeType.Document);
                    if (flag4 && (!flag6 || !flag8))
                    {
                        outText.Write("<p>");
                    }
                    outText.Write("<" + name);
                    this.WriteAttributes(outText);
                    if (this.Name[0] == '?')
                    {
                        outText.Write("?");
                    }
                    outText.Write(" />");
                    if (!flag4 || (flag5 && flag7))
                    {
                        break;
                    }
                    outText.Write("</p>");
                    return;
                }
                case HtmlNodeType.Comment:
                    break;

                case HtmlNodeType.Text:
                {
                    HtmlNode node = this.ParentNode;
                    flag = (node == null) || (node.NodeType == HtmlNodeType.Document);
                    HtmlNode node2 = this.NextSibling;
                    flag2 = (node2 != null) && IsEmptyElement(node2.Name);
                    HtmlNode node3 = this.PreviousSibling;
                    flag3 = (node3 != null) && IsEmptyElement(node3.Name);
                    if (string.IsNullOrEmpty(this.InnerText.Trim()))
                    {
                        break;
                    }
                    string text = ((HtmlTextNode) this).Text;
                    if (flag && !flag3)
                    {
                        outText.Write("<p>");
                    }
                    outText.Write(text);
                    if (!flag || flag2)
                    {
                        break;
                    }
                    outText.Write("</p>");
                    return;
                }
                default:
                    return;
            }
        }

        public HtmlAttributeCollection Attributes
        {
            get
            {
                if (!this.HasAttributes)
                {
                    this._attributes = new HtmlAttributeCollection(this);
                }
                return this._attributes;
            }
        }

        public HtmlNodeCollection ChildNodes
        {
            get
            {
                if (this._childnodes == null)
                {
                    this._childnodes = new HtmlNodeCollection(this);
                }
                return this._childnodes;
            }
        }

        public bool Closed
        {
            get
            {
                return (this._endnode != null);
            }
        }

        public HtmlAttributeCollection ClosingAttributes
        {
            get
            {
                if (!this.HasClosingAttributes)
                {
                    return new HtmlAttributeCollection(this);
                }
                return this._endnode.Attributes;
            }
        }

        internal HtmlNode EndNode
        {
            get
            {
                return this._endnode;
            }
        }

        public HtmlNode FirstChild
        {
            get
            {
                if (!this.HasChildNodes)
                {
                    return null;
                }
                return this._childnodes[0];
            }
        }

        public bool HasAttributes
        {
            get
            {
                if (this._attributes == null)
                {
                    return false;
                }
                if (this._attributes.Count <= 0)
                {
                    return false;
                }
                return true;
            }
        }

        public bool HasChildNodes
        {
            get
            {
                if (this._childnodes == null)
                {
                    return false;
                }
                if (this._childnodes.Count <= 0)
                {
                    return false;
                }
                return true;
            }
        }

        public bool HasClosingAttributes
        {
            get
            {
                if ((this._endnode == null) || (this._endnode == this))
                {
                    return false;
                }
                if (this._endnode._attributes == null)
                {
                    return false;
                }
                if (this._endnode._attributes.Count <= 0)
                {
                    return false;
                }
                return true;
            }
        }

        public string Id
        {
            get
            {
                if (this._ownerdocument._nodesid == null)
                {
                    throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
                }
                return this.GetId();
            }
            set
            {
                if (this._ownerdocument._nodesid == null)
                {
                    throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
                }
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this.SetId(value);
            }
        }

        public virtual string InnerHtml
        {
            get
            {
                if (this._innerchanged)
                {
                    this._innerhtml = this.WriteContentTo();
                    this._innerchanged = false;
                    return this._innerhtml;
                }
                if (this._innerhtml != null)
                {
                    return this._innerhtml;
                }
                if (this._innerstartindex < 0)
                {
                    return string.Empty;
                }
                return this._ownerdocument._text.Substring(this._innerstartindex, this._innerlength);
            }
            set
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(value);
                this.RemoveAllChildren();
                this.AppendChildren(document.DocumentNode.ChildNodes);
            }
        }

        public virtual string InnerText
        {
            get
            {
                if (this._nodetype == HtmlNodeType.Text)
                {
                    return ((HtmlTextNode) this).Text;
                }
                if (this._nodetype == HtmlNodeType.Comment)
                {
                    return ((HtmlCommentNode) this).Comment;
                }
                if (!this.HasChildNodes)
                {
                    return string.Empty;
                }
                string str = null;
                foreach (HtmlNode node in this.ChildNodes)
                {
                    str = str + node.InnerText;
                }
                return str;
            }
        }

        public HtmlNode LastChild
        {
            get
            {
                if (!this.HasChildNodes)
                {
                    return null;
                }
                return this._childnodes[this._childnodes.Count - 1];
            }
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
                this._name = value;
            }
        }

        public HtmlNode NextSibling
        {
            get
            {
                return this._nextnode;
            }
        }

        public HtmlNodeType NodeType
        {
            get
            {
                return this._nodetype;
            }
        }

        public virtual string OuterHtml
        {
            get
            {
                if (this._outerchanged)
                {
                    this._outerhtml = this.WriteTo();
                    this._outerchanged = false;
                    return this._outerhtml;
                }
                if (this._outerhtml != null)
                {
                    return this._outerhtml;
                }
                if (this._outerstartindex < 0)
                {
                    return string.Empty;
                }
                return this._ownerdocument._text.Substring(this._outerstartindex, this._outerlength);
            }
        }

        public HtmlDocument OwnerDocument
        {
            get
            {
                return this._ownerdocument;
            }
        }

        public HtmlNode ParentNode
        {
            get
            {
                return this._parentnode;
            }
        }

        public HtmlNode PreviousSibling
        {
            get
            {
                return this._prevnode;
            }
        }

        public int StreamPosition
        {
            get
            {
                return this._streamposition;
            }
        }
    }
}

