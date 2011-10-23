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

using polyglottos.snippets;

namespace polyglottos.generators.csharp
{
    public class GMethodGenerator : GContainerGeneratorBase
    {
        protected virtual void WriteGenericArguments(IGSnippetContainer snippet)
        {
            var method = (GMethod) snippet;
            if (method.GenericArguments.Count > 0)
            {
                CodeWriter.Write("<");
                for (int i = 0; i < method.GenericArguments.Count; i++)
                {
                    IGType genericParameter = method.GenericArguments[i];
                    if (i > 0)
                    {
                        CodeWriter.Write(", ");
                    }
                    Generator.GenerateSnippet(genericParameter, TypeArgs.All);
                }
                CodeWriter.Write("> ");
            }
        }

        protected override void GenerateProlog(IGSnippetContainer snippet)
        {
            var method = (GMethod) snippet;

            VerticalSpacingBegin(method);

            foreach (IGXmlDocSnippet xmlDocSnippet in method.XmlDocSnippets)
            {
                Generator.GenerateSnippet(xmlDocSnippet);
            }
            GMemberGeneratorBase.GenerateModifiers(method, CodeWriter);
            if (method.ReturnType != null)
            {
                Generator.GenerateSnippet(method.ReturnType, TypeArgs.NameNamespaceArguments);
                CodeWriter.Write(" ");
            }
            if (method.ExplicitInterface != null)
            {
                Generator.GenerateSnippet(method.ExplicitInterface, TypeArgs.NameNamespaceArguments);
                CodeWriter.Write('.');
            }
            if (!method.IsOperator)
            {
                CodeWriter.Write(method.Name);
                WriteGenericArguments(method);
            }
            CodeWriter.Write("(");
            for (int i = 0; i < method.Parameters.Count; i++)
            {
                IGParameter parameter = method.Parameters[i];
                if (i > 0)
                {
                    CodeWriter.Write(", ");
                }
                Generator.GenerateSnippet(parameter.Type, TypeArgs.NameNamespaceArguments);
                CodeWriter.Write(" ");
                CodeWriter.Write(parameter.Name);
            }

            if (method.HideBody)
            {
                CodeWriter.WriteLine(");");
            }
            else
            {
                CodeWriter.WriteLine(")");
                if (method.ConstructorBaseCall != null)
                {
                    CodeWriter.Write("    : base(");
                    GenerateCallParams(method.ConstructorBaseCall);
                    CodeWriter.WriteLine(")");
                }
                CodeWriter.WriteLine("{");
                CodeWriter.Indent++;
            }
        }

        protected override void GenerateBody(IGSnippetContainer snippet)
        {
            var method = (IGMethod) snippet;
            if (!method.HideBody)
            {
                base.GenerateBody(snippet);
            }
        }

        protected override void GenerateEpilog(IGSnippetContainer snippet)
        {
            var method = (IGMethod) snippet;
            if (!method.HideBody)
            {
                CodeWriter.Indent--;
                CodeWriter.WriteLine("}");
            }
            VerticalSpacingEnd(snippet);
        }
    }
}