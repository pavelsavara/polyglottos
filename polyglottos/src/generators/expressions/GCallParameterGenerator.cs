namespace polyglottos.generators
{
    public class GCallParameterGenerator : GGeneratorBase
    {
        public override void Generate(IGSnippet snippet)
        {
            var parameter = (IGCallParameter)snippet;
            if(parameter.IsOut)
            {
                CodeWriter.Write("out ");
            }
            if (parameter.IsRef)
            {
                CodeWriter.Write("ref ");
            }
            Generator.GenerateSnippet(parameter.Snippets[0]);
        }
    }
}