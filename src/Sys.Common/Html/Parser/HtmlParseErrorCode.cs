namespace iPortal.Common.Html.Parser
{
    using System;

    internal enum HtmlParseErrorCode
    {
        TagNotClosed,
        TagNotOpened,
        CharsetMismatch,
        EndTagNotRequired,
        EndTagInvalidHere
    }
}

