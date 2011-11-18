namespace polyglottos.generators
{
    public class GBlockStatementGenerator : GContainerGeneratorBase
    {
        protected override void GenerateProlog(IGSnippetContainer snippet)
        {
            CodeWriter.WriteLine("{");
            CodeWriter.Indent++;
        }

        protected override void GenerateEpilog(IGSnippetContainer snippet)
        {
            CodeWriter.Indent--;
            CodeWriter.WriteLine("}");
        }
    }
}