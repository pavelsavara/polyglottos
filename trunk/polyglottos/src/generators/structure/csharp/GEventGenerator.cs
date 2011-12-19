namespace polyglottos.generators.csharp
{
    public class GEventGenerator : GGeneratorBase
    {
        protected virtual void WriteGenericArguments(IGSnippetContainer snippet)
        {
            var gEvent = (IGEvent)snippet;
            if (gEvent.GenericArguments.Count > 0)
            {
                CodeWriter.Write("<");
                for (int i = 0; i < gEvent.GenericArguments.Count; i++)
                {
                    IGType genericParameter = gEvent.GenericArguments[i];
                    if (i > 0)
                    {
                        CodeWriter.Write(", ");
                    }
                    Generator.GenerateSnippet(genericParameter, TypeArgs.NameNamespaceArgumentsPrefix);
                }
                CodeWriter.Write("> ");
            }
        }

        public override void Generate(IGSnippet snippet)
        {
            var gEvent = (IGEvent)snippet;
            VerticalSpacingBegin(gEvent, true);

            GMemberGeneratorBase.GenerateModifiers(gEvent, CodeWriter);
            CodeWriter.Write("event ");
            Generator.GenerateSnippet(gEvent.ReturnType, TypeArgs.NameNamespaceArgumentsPrefix);
            CodeWriter.Write(" ");
            if (gEvent.ExplicitInterface != null)
            {
                Generator.GenerateSnippet(gEvent.ExplicitInterface, TypeArgs.NameNamespaceArgumentsPrefix);
                CodeWriter.Write('.');
            }
            WriteGenericArguments(gEvent);
            CodeWriter.Write(gEvent.Name);
            CodeWriter.WriteLine(" {");
            CodeWriter.Indent++;
            if (gEvent.Adder != null)
            {
                Generator.GenerateSnippet(gEvent.Adder);
            }
            if (gEvent.Remover != null)
            {
                Generator.GenerateSnippet(gEvent.Remover);
            }
            CodeWriter.Indent--;
            CodeWriter.WriteLine("}");
        }
    }

    public class GEventAdderGenerator : GContainerGeneratorBase
    {
        protected override void GenerateProlog(IGSnippetContainer snippet)
        {
            GMemberGeneratorBase.GenerateVisibility((IGPropertyXetter)snippet, CodeWriter);
            CodeWriter.WriteLine("add {");
            CodeWriter.Indent++;
        }

        protected override void GenerateEpilog(IGSnippetContainer snippet)
        {
            CodeWriter.Indent--;
            CodeWriter.WriteLine("}");
        }
    }

    public class GEventRemoverGenerator : GContainerGeneratorBase
    {
        protected override void GenerateProlog(IGSnippetContainer snippet)
        {
            GMemberGeneratorBase.GenerateVisibility((IGPropertyXetter)snippet, CodeWriter);
            CodeWriter.WriteLine("remove {");
            CodeWriter.Indent++;
        }

        protected override void GenerateEpilog(IGSnippetContainer snippet)
        {
            CodeWriter.Indent--;
            CodeWriter.WriteLine("}");
        }
    }
}