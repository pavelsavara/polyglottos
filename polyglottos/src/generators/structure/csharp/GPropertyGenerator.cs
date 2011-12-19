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
    public class GPropertyGenerator : GGeneratorBase
    {
        protected virtual void WriteGenericArguments(IGSnippetContainer snippet)
        {
            var property = (IGProperty)snippet;
            if (property.GenericArguments.Count > 0)
            {
                CodeWriter.Write("<");
                for (int i = 0; i < property.GenericArguments.Count; i++)
                {
                    IGType genericParameter = property.GenericArguments[i];
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
            var property = (IGProperty) snippet;
            VerticalSpacingBegin(property, true);

            GMemberGeneratorBase.GenerateModifiers(property, CodeWriter);
            Generator.GenerateSnippet(property.ReturnType, TypeArgs.NameNamespaceArgumentsPrefix);
            CodeWriter.Write(" ");
            if (property.ExplicitInterface != null)
            {
                Generator.GenerateSnippet(property.ExplicitInterface, TypeArgs.NameNamespaceArgumentsPrefix);
                CodeWriter.Write('.');
            }
            if(property.IsIndexer)
            {
                WriteGenericArguments(property);
                CodeWriter.Write("this[");
                for (int i = 0; i < property.Parameters.Count; i++)
                {
                    IGParameter parameter = property.Parameters[i];
                    if (i > 0)
                    {
                        CodeWriter.Write(", ");
                    }
                    Generator.GenerateSnippet(parameter);
                }
                CodeWriter.Write("]");
            }
            else
            {
                WriteGenericArguments(property);
                CodeWriter.Write(property.Name);
            }
            CodeWriter.WriteLine(" {");
            CodeWriter.Indent++;
            if (property.Getter != null)
            {
                Generator.GenerateSnippet(property.Getter);
            }
            if (property.Setter != null)
            {
                Generator.GenerateSnippet(property.Setter);
            }
            CodeWriter.Indent--;
            CodeWriter.WriteLine("}");
        }
    }

    public class GPropertyGetterGenerator : GContainerGeneratorBase
    {
        protected override void GenerateProlog(IGSnippetContainer snippet)
        {
            GMemberGeneratorBase.GenerateVisibility((IGPropertyXetter) snippet, CodeWriter);
            CodeWriter.WriteLine("get {");
            CodeWriter.Indent++;
        }

        protected override void GenerateEpilog(IGSnippetContainer snippet)
        {
            CodeWriter.Indent--;
            CodeWriter.WriteLine("}");
        }
    }

    public class GPropertySetterGenerator : GContainerGeneratorBase
    {
        protected override void GenerateProlog(IGSnippetContainer snippet)
        {
            GMemberGeneratorBase.GenerateVisibility((IGPropertyXetter) snippet, CodeWriter);
            CodeWriter.WriteLine("set {");
            CodeWriter.Indent++;
        }

        protected override void GenerateEpilog(IGSnippetContainer snippet)
        {
            CodeWriter.Indent--;
            CodeWriter.WriteLine("}");
        }
    }
}