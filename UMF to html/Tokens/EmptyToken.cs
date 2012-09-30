namespace UMF.Tokens
{
    public class EmptyToken : IToken
    {
        public string Parse(string input, ref int offset)
        {
            return "";
        }
    }
}