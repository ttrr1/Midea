namespace iPortal.Common.Html.Parser
{
    using System;

    internal abstract class MixedCodeDocumentFragment
    {
        internal MixedCodeDocument _doc;
        internal string _fragmenttext;
        internal int _index;
        internal int _length;
        internal int _line;
        internal int _lineposition;
        internal MixedCodeDocumentFragmentType _type;

        internal MixedCodeDocumentFragment(MixedCodeDocument doc, MixedCodeDocumentFragmentType type)
        {
            this._doc = doc;
            this._type = type;
            switch (type)
            {
                case MixedCodeDocumentFragmentType.Code:
                    this._doc._codefragments.Append(this);
                    break;

                case MixedCodeDocumentFragmentType.Text:
                    this._doc._textfragments.Append(this);
                    break;
            }
            this._doc._fragments.Append(this);
        }

        public string FragmentText
        {
            get
            {
                if (this._fragmenttext == null)
                {
                    this._fragmenttext = this._doc._text.Substring(this._index, this._length);
                }
                return this._fragmenttext;
            }
        }

        public MixedCodeDocumentFragmentType FragmentType
        {
            get
            {
                return this._type;
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

        public int StreamPosition
        {
            get
            {
                return this._index;
            }
        }
    }
}

