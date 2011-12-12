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
        public override void Generate(IGSnippet snippet)
        {
            var field = (IGProperty) snippet;
            VerticalSpacingBegin(field, true);

            GMemberGeneratorBase.GenerateModifiers(field, CodeWriter);
            Generator.GenerateSnippet(field.ReturnType, TypeArgs.NameNamespaceArgumentsPrefix);
            CodeWriter.Write(" ");
            CodeWriter.Write(field.Name);
            CodeWriter.WriteLine(" {");
            CodeWriter.Indent++;
            if (field.Getter != null)
            {
                Generator.GenerateSnippet(field.Getter);
            }
            if (field.Setter != null)
            {
                Generator.GenerateSnippet(field.Setter);
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