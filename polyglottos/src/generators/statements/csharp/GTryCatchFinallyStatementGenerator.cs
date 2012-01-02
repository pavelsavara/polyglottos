namespace polyglottos.generators.csharp
{
    public class GTryCatchFinallyStatementGenerator : GContainerGeneratorBase
    {
        protected override void GenerateProlog(IGSnippetContainer snippet)
        {
            CodeWriter.WriteLine("try");
            CodeWriter.WriteLine("{");
            CodeWriter.Indent++;
        }

        protected override void GenerateEpilog(IGSnippetContainer snippet)
        {
            var statement = (IGTryCatchFinallyStatement)snippet;

            CodeWriter.Indent--;
            CodeWriter.WriteLine("}");
            foreach (var ctch in statement.Catches)
            {
                CodeWriter.Write("catch (");
                Generator.GenerateSnippet(ctch.Type);
                CodeWriter.Write(" ");
                CodeWriter.Write(ctch.Name);
                CodeWriter.WriteLine(")");
                CodeWriter.WriteLine("{");
                CodeWriter.Indent++;
                foreach (var ts in ctch.Snippets)
                {
                    Generator.GenerateSnippet(ts);
                }
                CodeWriter.Indent--;
                CodeWriter.WriteLine("}");
            }

            if (statement.Finally.Snippets.Count > 0)
            {
                CodeWriter.Write("finally");
                CodeWriter.WriteLine("{");
                CodeWriter.Indent++;
                foreach (var ts in statement.Finally.Snippets)
                {
                    Generator.GenerateSnippet(ts);
                }
                CodeWriter.Indent--;
                CodeWriter.WriteLine("}");
            }
        }
    }
}