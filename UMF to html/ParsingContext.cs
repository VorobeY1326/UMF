namespace UMF
{
    public struct ParsingContext
    {
        public ParsingContext(int? teamNumber, int? problemNumber)
        {
            TeamNumber = teamNumber;
            ProblemNumber = problemNumber;
        }

        public int? TeamNumber;
        public int? ProblemNumber;
    }
}