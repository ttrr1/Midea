namespace iPortal.Common.Html.Parser
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.XPath;

    internal class HtmlDocument : IXPathNavigable
    {
        private int _c;
        private HtmlAttribute _currentattribute;
        private HtmlNode _currentnode;
        private HtmlNode _documentnode;
        private bool _fullcomment;
        private int _index;
        internal Hashtable _lastnodes = new Hashtable();
        private HtmlNode _lastparentnode;
        private int _line;
        private int _lineposition;
        private int _maxlineposition;
        internal Hashtable _nodesid;
        private ParseState _oldstate;
        internal Hashtable _openednodes = new Hashtable();
        private ArrayList _parseerrors = new ArrayList();
        private string _remainder = string.Empty;
        private int _remainderOffset;
        private ParseState _state;
        internal string _text;
        public string[] AllowedAttributes = new string[] { "class", "href", "target", "border", "src", "align", "width", "height", "color", "size" };
        public string[] AllowedTags = new string[] { 
            "p", "b", "i", "u", "em", "big", "small", "div", "img", "span", "blockquote", "strike", "code", "pre", "br", "hr", 
            "ul", "ol", "li", "del", "ins", "strong", "a", "font", "dl", "dd", "dt", "h6", "h4", "h5"
         };
        internal static readonly string HtmlExceptionRefNotChild = "Reference node must be a child of this node";
        internal static readonly string HtmlExceptionUseIdAttributeFalse = "You need to set UseIdAttribute property to true to enable this feature";
        public bool OptionAutoCloseOnEnd = true;
        public int OptionExtractErrorSourceTextMaxLength = 100;
        public bool OptionFixNestedTags = true;
        public bool OptionUseIdAttribute = true;

        public HtmlDocument()
        {
            this._documentnode = this.CreateNode(HtmlNodeType.Document, 0);
        }

        private void CloseCurrentNode()
        {
            if (!this._currentnode.Closed)
            {
                bool flag = false;
                HtmlNode node = (HtmlNode) this._lastnodes[this._currentnode.Name];
                if (node != null)
                {
                    if (this.OptionFixNestedTags && this.FindResetterNodes(node, this.GetResetters(this._currentnode.Name)))
                    {
                        flag = true;
                    }
                    if (!flag)
                    {
                        this._lastnodes[this._currentnode.Name] = node._prevwithsamename;
                        node.CloseNode(this._currentnode);
                    }
                }
                else if (!HtmlNode.IsClosedElement(this._currentnode.Name))
                {
                    if (HtmlNode.CanOverlapElement(this._currentnode.Name))
                    {
                        HtmlNode newChild = this.CreateNode(HtmlNodeType.Text, this._currentnode._outerstartindex);
                        newChild._outerlength = this._currentnode._outerlength;
                        ((HtmlTextNode) newChild).Text = ((HtmlTextNode) newChild).Text.ToLower();
                        if (this._lastparentnode != null)
                        {
                            this._lastparentnode.AppendChild(newChild);
                        }
                    }
                    else if (!HtmlNode.IsEmptyElement(this._currentnode.Name))
                    {
                        flag = true;
                    }
                }
                else
                {
                    this._currentnode.CloseNode(this._currentnode);
                    if (this._lastparentnode != null)
                    {
                        HtmlNode node2 = null;
                        Stack stack = new Stack();
                        for (HtmlNode node3 = this._lastparentnode.LastChild; node3 != null; node3 = node3.PreviousSibling)
                        {
                            if ((node3.Name == this._currentnode.Name) && !node3.HasChildNodes)
                            {
                                node2 = node3;
                                break;
                            }
                            stack.Push(node3);
                        }
                        if (node2 != null)
                        {
                            HtmlNode oldChild = null;
                            while (stack.Count != 0)
                            {
                                oldChild = (HtmlNode) stack.Pop();
                                this._lastparentnode.RemoveChild(oldChild);
                                node2.AppendChild(oldChild);
                            }
                        }
                        else
                        {
                            this._lastparentnode.AppendChild(this._currentnode);
                        }
                    }
                }
                if ((!flag && (this._lastparentnode != null)) && (!HtmlNode.IsClosedElement(this._currentnode.Name) || this._currentnode._starttag))
                {
                    this.UpdateLastParentNode();
                }
            }
        }

        internal HtmlAttribute CreateAttribute()
        {
            return new HtmlAttribute(this);
        }

        public HtmlAttribute CreateAttribute(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            HtmlAttribute attribute = this.CreateAttribute();
            attribute.Name = name;
            return attribute;
        }

        public HtmlAttribute CreateAttribute(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            HtmlAttribute attribute = this.CreateAttribute(name);
            attribute.Value = value;
            return attribute;
        }

        public HtmlCommentNode CreateComment()
        {
            return (HtmlCommentNode) this.CreateNode(HtmlNodeType.Comment);
        }

        public HtmlCommentNode CreateComment(string comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }
            HtmlCommentNode node = this.CreateComment();
            node.Comment = comment;
            return node;
        }

        public HtmlNode CreateElement(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            HtmlNode node = this.CreateNode(HtmlNodeType.Element);
            node._name = name;
            return node;
        }

        public XPathNavigator CreateNavigator()
        {
            return new HtmlNodeNavigator(this, this._documentnode);
        }

        internal HtmlNode CreateNode(HtmlNodeType type)
        {
            return this.CreateNode(type, -1);
        }

        internal HtmlNode CreateNode(HtmlNodeType type, int index)
        {
            switch (type)
            {
                case HtmlNodeType.Comment:
                    return new HtmlCommentNode(this, index);

                case HtmlNodeType.Text:
                    return new HtmlTextNode(this, index);
            }
            return new HtmlNode(type, this, index);
        }

        public HtmlTextNode CreateTextNode()
        {
            return (HtmlTextNode) this.CreateNode(HtmlNodeType.Text);
        }

        public HtmlTextNode CreateTextNode(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            HtmlTextNode node = this.CreateTextNode();
            node.Text = text;
            return node;
        }

        private string CurrentAttributeName()
        {
            return this._text.Substring(this._currentattribute._namestartindex, this._currentattribute._namelength);
        }

        private string CurrentAttributeValue()
        {
            return this._text.Substring(this._currentattribute._valuestartindex, this._currentattribute._valuelength);
        }

        private string CurrentNodeInner()
        {
            return this._text.Substring(this._currentnode._innerstartindex, this._currentnode._innerlength);
        }

        private string CurrentNodeName()
        {
            return this._text.Substring(this._currentnode._namestartindex, this._currentnode._namelength);
        }

        private string CurrentNodeOuter()
        {
            return this._text.Substring(this._currentnode._outerstartindex, this._currentnode._outerlength);
        }

        private void DecrementPosition()
        {
            this._index--;
            if (this._lineposition == 1)
            {
                this._lineposition = this._maxlineposition;
                this._line--;
            }
            else
            {
                this._lineposition--;
            }
        }

        private HtmlNode FindResetterNode(HtmlNode node, string name)
        {
            HtmlNode node2 = (HtmlNode) this._lastnodes[name];
            if (node2 == null)
            {
                return null;
            }
            if (node2.Closed)
            {
                return null;
            }
            if (node2._streamposition < node._streamposition)
            {
                return null;
            }
            return node2;
        }

        private bool FindResetterNodes(HtmlNode node, string[] names)
        {
            if (names != null)
            {
                for (int i = 0; i < names.Length; i++)
                {
                    if (this.FindResetterNode(node, names[i]) != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void FixNestedTag(string name, string[] resetters)
        {
            if (resetters != null)
            {
                HtmlNode node = (HtmlNode) this._lastnodes[name];
                if (((node != null) && !node.Closed) && !this.FindResetterNodes(node, resetters))
                {
                    HtmlNode endnode = new HtmlNode(node.NodeType, this, -1);
                    endnode._endnode = endnode;
                    node.CloseNode(endnode);
                }
            }
        }

        private void FixNestedTags()
        {
            if (this._currentnode._starttag)
            {
                string name = this.CurrentNodeName().ToLower();
                this.FixNestedTag(name, this.GetResetters(name));
            }
        }

        public HtmlNode GetElementbyId(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            if (this._nodesid == null)
            {
                throw new Exception(HtmlExceptionUseIdAttributeFalse);
            }
            return (this._nodesid[id.ToLower()] as HtmlNode);
        }

        private string[] GetResetters(string name)
        {
            switch (name)
            {
                case "li":
                    return new string[] { "ul" };

                case "tr":
                    return new string[] { "table" };

                case "th":
                case "td":
                    return new string[] { "tr", "table" };
            }
            return null;
        }

        internal HtmlNode GetXmlDeclaration()
        {
            if (this._documentnode.HasChildNodes)
            {
                foreach (HtmlNode node in this._documentnode._childnodes)
                {
                    if (node.Name == "?xml")
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        public static string GetXmlName(string name)
        {
            string str = string.Empty;
            bool flag = true;
            for (int i = 0; i < name.Length; i++)
            {
                if ((((name[i] >= 'a') && (name[i] <= 'z')) || ((name[i] >= '0') && (name[i] <= '9'))) || (((name[i] == '_') || (name[i] == '-')) || (name[i] == '.')))
                {
                    str = str + name[i];
                }
                else
                {
                    flag = false;
                    byte[] bytes = Encoding.UTF8.GetBytes(new char[] { name[i] });
                    for (int j = 0; j < bytes.Length; j++)
                    {
                        str = str + bytes[j].ToString("x2");
                    }
                    str = str + "_";
                }
            }
            if (flag)
            {
                return str;
            }
            return ("_" + str);
        }

        public static string HtmlEncode(string html)
        {
            if (html == null)
            {
                throw new ArgumentNullException("html");
            }
            Regex regex = new Regex("&(?!(amp;)|(lt;)|(gt;)|(quot;))", RegexOptions.IgnoreCase);
            return regex.Replace(html, "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
        }

        private void IncrementPosition()
        {
            this._index++;
            this._maxlineposition = this._lineposition;
            if (this._c == 10)
            {
                this._lineposition = 1;
                this._line++;
            }
            else
            {
                this._lineposition++;
            }
        }

        public static bool IsWhiteSpace(int c)
        {
            if (((c != 10) && (c != 13)) && ((c != 0x20) && (c != 9)))
            {
                return false;
            }
            return true;
        }

        public void Load(Stream stream)
        {
            this.Load(new StreamReader(stream));
        }

        public void Load(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (this.OptionUseIdAttribute)
            {
                this._nodesid = new Hashtable();
            }
            else
            {
                this._nodesid = null;
            }
            StreamReader reader2 = reader as StreamReader;
            if (reader2 != null)
            {
                try
                {
                    reader2.Peek();
                }
                catch
                {
                }
            }
            this._text = reader.ReadToEnd();
            this._documentnode = this.CreateNode(HtmlNodeType.Document, 0);
            this.Parse();
        }

        public void LoadHtml(string html)
        {
            if (html == null)
            {
                throw new ArgumentNullException("html");
            }
            StringReader reader = new StringReader(html);
            this.Load(reader);
            reader.Close();
        }

        private bool NewCheck()
        {
            if (this._c != 60)
            {
                return false;
            }
            if ((this._index >= this._text.Length) || (this._text[this._index] != '%'))
            {
                if (!this.PushNodeEnd(this._index - 1, true))
                {
                    this._index = this._text.Length;
                    return true;
                }
                this._state = ParseState.WhichTag;
                if (((this._index - 1) <= (this._text.Length - 2)) && (this._text[this._index] == '!'))
                {
                    this.PushNodeStart(HtmlNodeType.Comment, this._index - 1);
                    this.PushNodeNameStart(true, this._index);
                    this.PushNodeNameEnd(this._index + 1);
                    this._state = ParseState.Comment;
                    if (this._index < (this._text.Length - 2))
                    {
                        if ((this._text[this._index + 1] == '-') && (this._text[this._index + 2] == '-'))
                        {
                            this._fullcomment = true;
                        }
                        else
                        {
                            this._fullcomment = false;
                        }
                    }
                    return true;
                }
                this.PushNodeStart(HtmlNodeType.Element, this._index - 1);
                return true;
            }
            ParseState state = this._state;
            switch (state)
            {
                case ParseState.WhichTag:
                    this.PushNodeNameStart(true, this._index - 1);
                    this._state = ParseState.Tag;
                    break;

                case ParseState.Tag:
                    break;

                case ParseState.BetweenAttributes:
                    this.PushAttributeNameStart(this._index - 1);
                    break;

                default:
                    if (state == ParseState.AttributeAfterEquals)
                    {
                        this.PushAttributeValueStart(this._index - 1);
                    }
                    break;
            }
            this._oldstate = this._state;
            this._state = ParseState.ServerSideCode;
            return true;
        }

        private void Parse()
        {
            int num = 0;
            this._lastnodes = new Hashtable();
            this._c = 0;
            this._fullcomment = false;
            this._parseerrors = new ArrayList();
            this._line = 1;
            this._lineposition = 1;
            this._maxlineposition = 1;
            this._state = ParseState.Text;
            this._oldstate = this._state;
            this._documentnode._innerlength = this._text.Length;
            this._documentnode._outerlength = this._text.Length;
            this._remainderOffset = this._text.Length;
            this._lastparentnode = this._documentnode;
            this._currentnode = this.CreateNode(HtmlNodeType.Text, 0);
            this._currentattribute = null;
            this._index = 0;
            this.PushNodeStart(HtmlNodeType.Text, 0);
            while (this._index < this._text.Length)
            {
                this._c = this._text[this._index];
                this.IncrementPosition();
                switch (this._state)
                {
                    case ParseState.Text:
                    {
                        if (!this.NewCheck())
                        {
                        }
                        continue;
                    }
                    case ParseState.WhichTag:
                        if (this.NewCheck())
                        {
                            continue;
                        }
                        if (this._c != 0x2f)
                        {
                            break;
                        }
                        this.PushNodeNameStart(false, this._index);
                        goto Label_016C;

                    case ParseState.Tag:
                    {
                        if (!this.NewCheck())
                        {
                            if (!IsWhiteSpace(this._c))
                            {
                                goto Label_01B6;
                            }
                            this.PushNodeNameEnd(this._index - 1);
                            if (this._state == ParseState.Tag)
                            {
                                this._state = ParseState.BetweenAttributes;
                            }
                        }
                        continue;
                    }
                    case ParseState.BetweenAttributes:
                    {
                        if (!this.NewCheck() && !IsWhiteSpace(this._c))
                        {
                            if ((this._c != 0x2f) && (this._c != 0x3f))
                            {
                                goto Label_0292;
                            }
                            this._state = ParseState.EmptyTag;
                        }
                        continue;
                    }
                    case ParseState.EmptyTag:
                    {
                        if (!this.NewCheck())
                        {
                            if (this._c != 0x3e)
                            {
                                goto Label_035F;
                            }
                            if (this.PushNodeEnd(this._index, true))
                            {
                                goto Label_033A;
                            }
                            this._index = this._text.Length;
                        }
                        continue;
                    }
                    case ParseState.AttributeName:
                    {
                        if (!this.NewCheck())
                        {
                            if (!IsWhiteSpace(this._c))
                            {
                                goto Label_039D;
                            }
                            this.PushAttributeNameEnd(this._index - 1);
                            this._state = ParseState.AttributeBeforeEquals;
                        }
                        continue;
                    }
                    case ParseState.AttributeBeforeEquals:
                    {
                        if (!this.NewCheck() && !IsWhiteSpace(this._c))
                        {
                            if (this._c != 0x3e)
                            {
                                goto Label_0495;
                            }
                            if (this.PushNodeEnd(this._index, false))
                            {
                                goto Label_0470;
                            }
                            this._index = this._text.Length;
                        }
                        continue;
                    }
                    case ParseState.AttributeAfterEquals:
                    {
                        if (!this.NewCheck() && !IsWhiteSpace(this._c))
                        {
                            if ((this._c != 0x27) && (this._c != 0x22))
                            {
                                goto Label_050C;
                            }
                            this._state = ParseState.QuotedAttributeValue;
                            this.PushAttributeValueStart(this._index);
                            num = this._c;
                        }
                        continue;
                    }
                    case ParseState.AttributeValue:
                    {
                        if (!this.NewCheck())
                        {
                            if (!IsWhiteSpace(this._c))
                            {
                                goto Label_05AC;
                            }
                            this.PushAttributeValueEnd(this._index - 1);
                            this._state = ParseState.BetweenAttributes;
                        }
                        continue;
                    }
                    case ParseState.Comment:
                    {
                        if ((this._c == 0x3e) && (!this._fullcomment || ((this._text[this._index - 2] == '-') && (this._text[this._index - 3] == '-'))))
                        {
                            if (!this.PushNodeEnd(this._index, false))
                            {
                                this._index = this._text.Length;
                            }
                            else
                            {
                                this._state = ParseState.Text;
                                this.PushNodeStart(HtmlNodeType.Text, this._index);
                            }
                        }
                        continue;
                    }
                    case ParseState.QuotedAttributeValue:
                    {
                        if (this._c != num)
                        {
                            goto Label_0634;
                        }
                        this.PushAttributeValueEnd(this._index - 1);
                        this._state = ParseState.BetweenAttributes;
                        continue;
                    }
                    case ParseState.ServerSideCode:
                    {
                        if (((this._c != 0x25) || (this._index >= this._text.Length)) || (this._text[this._index] != '>'))
                        {
                            continue;
                        }
                        ParseState state2 = this._oldstate;
                        if (state2 == ParseState.BetweenAttributes)
                        {
                            goto Label_0765;
                        }
                        if (state2 != ParseState.AttributeAfterEquals)
                        {
                            goto Label_077C;
                        }
                        this._state = ParseState.AttributeValue;
                        goto Label_0788;
                    }
                    case ParseState.PcData:
                    {
                        if (((this._currentnode._namelength + 3) <= (this._text.Length - (this._index - 1))) && (string.Compare(this._text.Substring(this._index - 1, this._currentnode._namelength + 2), "</" + this._currentnode.Name, true) == 0))
                        {
                            int c = this._text[((this._index - 1) + 2) + this._currentnode.Name.Length];
                            if ((c == 0x3e) || IsWhiteSpace(c))
                            {
                                HtmlNode newChild = this.CreateNode(HtmlNodeType.Text, this._currentnode._outerstartindex + this._currentnode._outerlength);
                                newChild._outerlength = (this._index - 1) - newChild._outerstartindex;
                                this._currentnode.AppendChild(newChild);
                                this.PushNodeStart(HtmlNodeType.Element, this._index - 1);
                                this.PushNodeNameStart(false, (this._index - 1) + 2);
                                this._state = ParseState.Tag;
                                this.IncrementPosition();
                            }
                        }
                        continue;
                    }
                    default:
                    {
                        continue;
                    }
                }
                this.PushNodeNameStart(true, this._index - 1);
                this.DecrementPosition();
            Label_016C:
                this._state = ParseState.Tag;
                continue;
            Label_01B6:
                if (this._c == 0x2f)
                {
                    this.PushNodeNameEnd(this._index - 1);
                    if (this._state == ParseState.Tag)
                    {
                        this._state = ParseState.EmptyTag;
                    }
                }
                else if (this._c == 0x3e)
                {
                    this.PushNodeNameEnd(this._index - 1);
                    if (this._state == ParseState.Tag)
                    {
                        if (!this.PushNodeEnd(this._index, false))
                        {
                            this._index = this._text.Length;
                        }
                        else if (this._state == ParseState.Tag)
                        {
                            this._state = ParseState.Text;
                            this.PushNodeStart(HtmlNodeType.Text, this._index);
                        }
                    }
                }
                continue;
            Label_0292:
                if (this._c == 0x3e)
                {
                    if (!this.PushNodeEnd(this._index, false))
                    {
                        this._index = this._text.Length;
                    }
                    else if (this._state == ParseState.BetweenAttributes)
                    {
                        this._state = ParseState.Text;
                        this.PushNodeStart(HtmlNodeType.Text, this._index);
                    }
                }
                else
                {
                    this.PushAttributeNameStart(this._index - 1);
                    this._state = ParseState.AttributeName;
                }
                continue;
            Label_033A:
                if (this._state == ParseState.EmptyTag)
                {
                    this._state = ParseState.Text;
                    this.PushNodeStart(HtmlNodeType.Text, this._index);
                }
                continue;
            Label_035F:
                this._state = ParseState.BetweenAttributes;
                continue;
            Label_039D:
                if (this._c == 0x3d)
                {
                    this.PushAttributeNameEnd(this._index - 1);
                    this._state = ParseState.AttributeAfterEquals;
                }
                else if (this._c == 0x3e)
                {
                    this.PushAttributeNameEnd(this._index - 1);
                    if (!this.PushNodeEnd(this._index, false))
                    {
                        this._index = this._text.Length;
                    }
                    else if (this._state == ParseState.AttributeName)
                    {
                        this._state = ParseState.Text;
                        this.PushNodeStart(HtmlNodeType.Text, this._index);
                    }
                }
                continue;
            Label_0470:
                if (this._state == ParseState.AttributeBeforeEquals)
                {
                    this._state = ParseState.Text;
                    this.PushNodeStart(HtmlNodeType.Text, this._index);
                }
                continue;
            Label_0495:
                if (this._c == 0x3d)
                {
                    this._state = ParseState.AttributeAfterEquals;
                }
                else
                {
                    this._state = ParseState.BetweenAttributes;
                    this.DecrementPosition();
                }
                continue;
            Label_050C:
                if (this._c == 0x3e)
                {
                    if (!this.PushNodeEnd(this._index, false))
                    {
                        this._index = this._text.Length;
                    }
                    else if (this._state == ParseState.AttributeAfterEquals)
                    {
                        this._state = ParseState.Text;
                        this.PushNodeStart(HtmlNodeType.Text, this._index);
                    }
                }
                else
                {
                    this.PushAttributeValueStart(this._index - 1);
                    this._state = ParseState.AttributeValue;
                }
                continue;
            Label_05AC:
                if (this._c == 0x3e)
                {
                    this.PushAttributeValueEnd(this._index - 1);
                    if (!this.PushNodeEnd(this._index, false))
                    {
                        this._index = this._text.Length;
                    }
                    else if (this._state == ParseState.AttributeValue)
                    {
                        this._state = ParseState.Text;
                        this.PushNodeStart(HtmlNodeType.Text, this._index);
                    }
                }
                continue;
            Label_0634:
                if (((this._c == 60) && (this._index < this._text.Length)) && (this._text[this._index] == '%'))
                {
                    this._oldstate = this._state;
                    this._state = ParseState.ServerSideCode;
                }
                continue;
            Label_0765:
                this.PushAttributeNameEnd(this._index + 1);
                this._state = ParseState.BetweenAttributes;
                goto Label_0788;
            Label_077C:
                this._state = this._oldstate;
            Label_0788:
                this.IncrementPosition();
            }
            if (this._currentnode._namestartindex > 0)
            {
                this.PushNodeNameEnd(this._index);
            }
            this.PushNodeEnd(this._index, false);
            this._lastnodes.Clear();
        }

        private void PushAttributeNameEnd(int index)
        {
            this._currentattribute._namelength = index - this._currentattribute._namestartindex;
            this._currentnode.Attributes.Append(this._currentattribute);
        }

        private void PushAttributeNameStart(int index)
        {
            this._currentattribute = this.CreateAttribute();
            this._currentattribute._namestartindex = index;
            this._currentattribute._line = this._line;
            this._currentattribute._lineposition = this._lineposition;
            this._currentattribute._streamposition = index;
        }

        private void PushAttributeValueEnd(int index)
        {
            this._currentattribute._valuelength = index - this._currentattribute._valuestartindex;
        }

        private void PushAttributeValueStart(int index)
        {
            this._currentattribute._valuestartindex = index;
        }

        private bool PushNodeEnd(int index, bool close)
        {
            this._currentnode._outerlength = index - this._currentnode._outerstartindex;
            if ((this._currentnode._nodetype == HtmlNodeType.Text) || (this._currentnode._nodetype == HtmlNodeType.Comment))
            {
                if (this._currentnode._outerlength > 0)
                {
                    this._currentnode._innerlength = this._currentnode._outerlength;
                    this._currentnode._innerstartindex = this._currentnode._outerstartindex;
                    if (this._lastparentnode != null)
                    {
                        this._lastparentnode.AppendChild(this._currentnode);
                    }
                }
            }
            else if (this._currentnode._starttag && (this._lastparentnode != this._currentnode))
            {
                if (this._lastparentnode != null)
                {
                    this._lastparentnode.AppendChild(this._currentnode);
                }
                HtmlNode node = (HtmlNode) this._lastnodes[this._currentnode.Name];
                this._currentnode._prevwithsamename = node;
                this._lastnodes[this._currentnode.Name] = this._currentnode;
                if ((this._currentnode.NodeType == HtmlNodeType.Document) || (this._currentnode.NodeType == HtmlNodeType.Element))
                {
                    this._lastparentnode = this._currentnode;
                }
                if (HtmlNode.IsCDataElement(this.CurrentNodeName()))
                {
                    this._state = ParseState.PcData;
                    return true;
                }
                if (HtmlNode.IsClosedElement(this._currentnode.Name) || HtmlNode.IsEmptyElement(this._currentnode.Name))
                {
                    close = true;
                }
            }
            if (close || !this._currentnode._starttag)
            {
                this.CloseCurrentNode();
            }
            return true;
        }

        private void PushNodeNameEnd(int index)
        {
            this._currentnode._namelength = index - this._currentnode._namestartindex;
            if (this.OptionFixNestedTags)
            {
                this.FixNestedTags();
            }
        }

        private void PushNodeNameStart(bool starttag, int index)
        {
            this._currentnode._starttag = starttag;
            this._currentnode._namestartindex = index;
        }

        private void PushNodeStart(HtmlNodeType type, int index)
        {
            this._currentnode = this.CreateNode(type, index);
            this._currentnode._line = this._line;
            this._currentnode._lineposition = this._lineposition;
            if (type == HtmlNodeType.Element)
            {
                this._currentnode._lineposition--;
            }
            this._currentnode._streamposition = index;
        }

        public void Save(Stream outStream)
        {
            StreamWriter writer = new StreamWriter(outStream);
            this.Save(writer);
        }

        public void Save(StreamWriter writer)
        {
            this.Save((TextWriter) writer);
        }

        public void Save(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            this.DocumentNode.WriteTo(writer);
        }

        internal void SetIdForNode(HtmlNode node, string id)
        {
            if (this.OptionUseIdAttribute && ((this._nodesid != null) && (id != null)))
            {
                if (node == null)
                {
                    this._nodesid.Remove(id.ToLower());
                }
                else
                {
                    this._nodesid[id.ToLower()] = node;
                }
            }
        }

        internal void UpdateLastParentNode()
        {
            do
            {
                if (this._lastparentnode.Closed)
                {
                    this._lastparentnode = this._lastparentnode.ParentNode;
                }
            }
            while ((this._lastparentnode != null) && this._lastparentnode.Closed);
            if (this._lastparentnode == null)
            {
                this._lastparentnode = this._documentnode;
            }
        }

        public HtmlNode DocumentNode
        {
            get
            {
                return this._documentnode;
            }
        }

        public ArrayList ParseErrors
        {
            get
            {
                return this._parseerrors;
            }
        }

        public string Remainder
        {
            get
            {
                return this._remainder;
            }
        }

        public int RemainderOffset
        {
            get
            {
                return this._remainderOffset;
            }
        }

        private enum ParseState
        {
            Text,
            WhichTag,
            Tag,
            BetweenAttributes,
            EmptyTag,
            AttributeName,
            AttributeBeforeEquals,
            AttributeAfterEquals,
            AttributeValue,
            Comment,
            QuotedAttributeValue,
            ServerSideCode,
            PcData
        }
    }
}

