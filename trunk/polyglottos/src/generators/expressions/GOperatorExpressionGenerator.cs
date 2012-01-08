namespace polyglottos.generators
{
    public class GOperatorExpressionGenerator : GExpressionGeneratorBase
    {
        public override void Generate(IGSnippet snippet)
        {
            var expression = (IGOperatorExpression) snippet;
            CodeWriter.Write(" ");
            CodeWriter.Write(expression.Name);
            CodeWriter.Write(" ");
            GenerateChain(expression);
        }
    }
}