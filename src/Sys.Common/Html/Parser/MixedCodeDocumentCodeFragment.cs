namespace iPortal.Common.Html.Parser
{
    using System;

    internal class MixedCodeDocumentCodeFragment : MixedCodeDocumentFragment
    {
        internal string _code;

        internal MixedCodeDocumentCodeFragment(MixedCodeDocument doc) : base(doc, MixedCodeDocumentFragmentType.Code)
        {
        }

        public string Code
        {
            get
            {
                if (this._code == null)
                {
                    this._code = base.FragmentText.Substring(base._doc.TokenCodeStart.Length, ((base.FragmentText.Length - base._doc.TokenCodeEnd.Length) - base._doc.TokenCodeStart.Length) - 1).Trim();
                    if (this._code.StartsWith("="))
                    {
                        this._code = base._doc.TokenResponseWrite + this._code.Substring(1, this._code.Length - 1);
                    }
                }
                return this._code;
            }
            set
            {
                this._code = value;
            }
        }
    }
}

