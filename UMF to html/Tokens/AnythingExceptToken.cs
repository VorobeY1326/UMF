using System.Text;

namespace UMF.Tokens
{
    public class AnythingExceptToken : IToken
    {
        private readonly IToken exceptingToken;

        public AnythingExceptToken(IToken exceptingToken)
        {
            this.exceptingToken = exceptingToken;
        }

        public string Parse(string input, ref int offset)
        {
            if (offset >= input.Length)
                return null;
            var result = new StringBuilder();
            while (offset < input.Length)
            {
                int currentOffset = offset;
                if (exceptingToken.Parse(input, ref offset) != null)
                {
                    offset = currentOffset;
                    break;
                }
                result.Append(input[offset++]);
            }
            if (result.Length == 0)
                return null;
            return result.ToString();
        }
    }
}