namespace MathGame.ivangar
{
    public record MathOperation
    {
        public int OperandA { get; init; }
        public int OperandB { get; init; }
        public char Operation { get; init; }
        public int UserAnswer { get; init; }
        public string ScoreMark { get; init; } = string.Empty;

        public override string ToString()
        {
            return $"{OperandA} {Operation} {OperandB} = {UserAnswer,-10} {ScoreMark,-5}";
        }
    }
}
