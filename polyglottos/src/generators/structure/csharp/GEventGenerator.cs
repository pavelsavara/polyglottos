#region Copyright (C) 2011 by Pavel Savara

/*
This file is part of polyglottos library - code generator tool
http://code.google.com/p/polyglottos/

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#endregion

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