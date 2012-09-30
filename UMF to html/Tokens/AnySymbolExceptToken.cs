namespace UMF.Tokens
{
    public class AnySymbolExceptToken : IToken
    {
        private readonly char[] exceptingChars;

        public AnySymbolExceptToken(params char[] exceptingChars)
        {
            this.exceptingChars = exceptingChars;
        }

        public string Parse(string input, ref int offset)
        {
            if (offset >= input.Length)
                return null;
            foreach (var c in exceptingChars)
            {
                if (input[offset] == c)
                    return null;
            }
            offset++;
            return input.Substring(offset - 1, 1);
        }
    }
}