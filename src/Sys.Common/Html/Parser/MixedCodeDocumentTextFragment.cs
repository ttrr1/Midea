namespace iPortal.Common.Html.Parser
{
    using System;

    internal class MixedCodeDocumentTextFragment : MixedCodeDocumentFragment
    {
        internal MixedCodeDocumentTextFragment(MixedCodeDocument doc) : base(doc, MixedCodeDocumentFragmentType.Text)
        {
        }

        public string Text
        {
            get
            {
                return base.FragmentText;
            }
            set
            {
                base._fragmenttext = value;
            }
        }
    }
}

