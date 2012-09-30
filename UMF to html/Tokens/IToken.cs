namespace UMF.Tokens
{
    public interface IToken
    {
        string Parse(string input, ref int offset);
    }
}