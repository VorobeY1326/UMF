namespace UMF.Tokens
{
    public class DigitToken : IToken
    {
        public string Parse(string input, ref int offset)
        {
            if (offset >= input.Length)
                return null;
            if (char.IsDigit(input[offset]))
            {
                offset++;
                return input.Substring(offset - 1, 1);
            }
            return null;
        }
    }
}