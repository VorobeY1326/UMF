using System;

namespace UMF.Tokens
{
    public class ParsingException : Exception
    {
        public ParsingException(string message)
            : base(message)
        {
        }
    }
}