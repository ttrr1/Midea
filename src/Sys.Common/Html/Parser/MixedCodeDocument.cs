namespace iPortal.Common.Html.Parser
{
    using System;
    using System.IO;
    using System.Text;

    internal class MixedCodeDocument
    {
        private int _c;
        internal MixedCodeDocumentFragmentList _codefragments;
        private MixedCodeDocumentFragment _currentfragment;
        internal MixedCodeDocumentFragmentList _fragments;
        private int _index;
        private int _line;
        private int _lineposition;
        private ParseState _state;
        internal string _text;
        internal MixedCodeDocumentFragmentList _textfragments;
        public string TokenCodeEnd = "%>";
        public string TokenCodeStart = "<%";
        public string TokenDirective = "@";
        public string TokenResponseWrite = "Response.Write ";
        private string TokenTextBlock = "TextBlock({0})";

        public MixedCodeDocument()
        {
            this._codefragments = new MixedCodeDocumentFragmentList(this);
            this._textfragments = new MixedCodeDocumentFragmentList(this);
            this._fragments = new MixedCodeDocumentFragmentList(this);
        }

        public MixedCodeDocumentCodeFragment CreateCodeFragment()
        {
            return (MixedCodeDocumentCodeFragment) this.CreateFragment(MixedCodeDocumentFragmentType.Code);
        }

        internal MixedCodeDocumentFragment CreateFragment(MixedCodeDocumentFragmentType type)
        {
            switch (type)
            {
                case MixedCodeDocumentFragmentType.Code:
                    return new MixedCodeDocumentCodeFragment(this);

                case MixedCodeDocumentFragmentType.Text:
                    return new MixedCodeDocumentTextFragment(this);
            }
            throw new NotSupportedException();
        }

        public MixedCodeDocumentTextFragment CreateTextFragment()
        {
            return (MixedCodeDocumentTextFragment) this.CreateFragment(MixedCodeDocumentFragmentType.Text);
        }

        private void IncrementPosition()
        {
            this._index++;
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

        public void Load(Stream stream)
        {
            this.Load(new StreamReader(stream));
        }

        public void Load(TextReader reader)
        {
            this._codefragments.Clear();
            this._textfragments.Clear();
            this._text = reader.ReadToEnd();
            reader.Close();
            this.Parse();
        }

        public void Load(string path)
        {
            this.Load(new StreamReader(path));
        }

        public void Load(Stream stream, bool detectEncodingFromByteOrderMarks)
        {
            this.Load(new StreamReader(stream, detectEncodingFromByteOrderMarks));
        }

        public void Load(Stream stream, Encoding encoding)
        {
            this.Load(new StreamReader(stream, encoding));
        }

        public void Load(string path, bool detectEncodingFromByteOrderMarks)
        {
            this.Load(new StreamReader(path, detectEncodingFromByteOrderMarks));
        }

        public void Load(string path, Encoding encoding)
        {
            this.Load(new StreamReader(path, encoding));
        }

        public void Load(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
        {
            this.Load(new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks));
        }

        public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
        {
            this.Load(new StreamReader(path, encoding, detectEncodingFromByteOrderMarks));
        }

        public void Load(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
        {
            this.Load(new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, buffersize));
        }

        public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
        {
            this.Load(new StreamReader(path, encoding, detectEncodingFromByteOrderMarks, buffersize));
        }

        public void LoadHtml(string html)
        {
            this.Load(new StringReader(html));
        }

        private void Parse()
        {
            this._state = ParseState.Text;
            this._index = 0;
            this._currentfragment = this.CreateFragment(MixedCodeDocumentFragmentType.Text);
            while (this._index < this._text.Length)
            {
                this._c = this._text[this._index];
                this.IncrementPosition();
                switch (this._state)
                {
                    case ParseState.Text:
                        if (((this._index + this.TokenCodeStart.Length) < this._text.Length) && (this._text.Substring(this._index - 1, this.TokenCodeStart.Length) == this.TokenCodeStart))
                        {
                            this._state = ParseState.Code;
                            this._currentfragment._length = (this._index - 1) - this._currentfragment._index;
                            this._currentfragment = this.CreateFragment(MixedCodeDocumentFragmentType.Code);
                            this.SetPosition();
                        }
                        break;

                    case ParseState.Code:
                        if (((this._index + this.TokenCodeEnd.Length) < this._text.Length) && (this._text.Substring(this._index - 1, this.TokenCodeEnd.Length) == this.TokenCodeEnd))
                        {
                            this._state = ParseState.Text;
                            this._currentfragment._length = (this._index + this.TokenCodeEnd.Length) - this._currentfragment._index;
                            this._index += this.TokenCodeEnd.Length;
                            this._lineposition += this.TokenCodeEnd.Length;
                            this._currentfragment = this.CreateFragment(MixedCodeDocumentFragmentType.Text);
                            this.SetPosition();
                        }
                        break;
                }
            }
            this._currentfragment._length = this._index - this._currentfragment._index;
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
            writer.Flush();
        }

        private void SetPosition()
        {
            this._currentfragment._line = this._line;
            this._currentfragment._lineposition = this._lineposition;
            this._currentfragment._index = this._index - 1;
            this._currentfragment._length = 0;
        }

        public string Code
        {
            get
            {
                string str = "";
                int num = 0;
                foreach (MixedCodeDocumentFragment fragment in this._fragments)
                {
                    switch (fragment._type)
                    {
                        case MixedCodeDocumentFragmentType.Code:
                        {
                            str = str + ((MixedCodeDocumentCodeFragment) fragment).Code + "\n";
                            continue;
                        }
                        case MixedCodeDocumentFragmentType.Text:
                        {
                            str = str + this.TokenResponseWrite + string.Format(this.TokenTextBlock, num) + "\n";
                            num++;
                            continue;
                        }
                    }
                }
                return str;
            }
        }

        public MixedCodeDocumentFragmentList CodeFragments
        {
            get
            {
                return this._codefragments;
            }
        }

        public MixedCodeDocumentFragmentList Fragments
        {
            get
            {
                return this._fragments;
            }
        }

        public MixedCodeDocumentFragmentList TextFragments
        {
            get
            {
                return this._textfragments;
            }
        }

        private enum ParseState
        {
            Text,
            Code
        }
    }
}

