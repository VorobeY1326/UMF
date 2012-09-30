namespace UMF.Tokens
{
    public class LetterToken : IToken
    {
        public string Parse(string input, ref int offset)
        {
            if (offset >= input.Length)
                return null;
            if (char.IsLetter(input[offset]))
            {
                offset++;
                return input.Substring(offset - 1, 1);
            }
            return null;
        }
    }
}