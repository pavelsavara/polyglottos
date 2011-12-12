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
    public class GClassGenerator : GContainerGeneratorBase
    {
        protected override void GenerateProlog(IGSnippetContainer snippet)
        {
            var clazz = (IGClass) snippet;

            VerticalSpacingBegin(clazz);

            foreach (var xmlDocSnippet in clazz.XmlDocSnippets)
            {
                Generator.GenerateSnippet(xmlDocSnippet);
            }
            foreach (var attributeSnippet in clazz.AttributeSnippets)
            {
                Generator.GenerateSnippet(attributeSnippet);
            }

            if (clazz.IsStatic)
            {
                CodeWriter.Write("static ");
            }
            if (clazz.IsPublic)
            {
                CodeWriter.Write("public ");
            }
            if (clazz.IsPrivate)
            {
                CodeWriter.Write("private ");
            }
            if (clazz.IsInternal)
            {
                CodeWriter.Write("internal ");
            }
            if (clazz.IsSealed)
            {
                CodeWriter.Write("sealed ");
            }
            if (clazz.IsAbstract)
            {
                CodeWriter.Write("abstract ");
            }
            if (clazz.IsPartial)
            {
                CodeWriter.Write("partial ");
            }
            CodeWriter.Write(clazz.IsInterface ? "interface " : "class ");
            if (clazz.DeclaringType != null)
            {
                Generator.GenerateSnippet(clazz.DeclaringType, TypeArgs.Name);
            }
            else
            {
                CodeWriter.Write(clazz.Name);
            }

            if (clazz.GenericArguments.Count > 0)
            {
                CodeWriter.Write("<");
                for (int i = 0; i < clazz.GenericArguments.Count; i++)
                {
                    IGType argument = clazz.GenericArguments[i];
                    if (i > 0)
                    {
                        CodeWriter.Write(", ");
                    }
                    Generator.GenerateSnippet(argument, TypeArgs.NameNamespaceArgumentsPrefix);
                }
                CodeWriter.Write(">");
            }
            if (clazz.Extends != null)
            {
                CodeWriter.Write(" : ");
                Generator.GenerateSnippet(clazz.Extends, TypeArgs.NameNamespaceArgumentsPrefix);
                foreach (IGType implement in clazz.Implements)
                {
                    CodeWriter.Write(", ");
                    Generator.GenerateSnippet(implement, TypeArgs.NameNamespaceArgumentsPrefix);
                }
            }
            else if (clazz.Implements.Count > 0)
            {
                for (int i = 0; i < clazz.Implements.Count; i++)
                {
                    CodeWriter.Write(i == 0 ? " : " : ", ");
                    Generator.GenerateSnippet(clazz.Implements[i], TypeArgs.NameNamespaceArgumentsPrefix);
                }
            }
            CodeWriter.WriteLine();
            foreach (IGType argument in clazz.GenericArguments)
            {
                Generator.GenerateSnippet(argument, TypeArgs.Constraints);
            }
            CodeWriter.WriteLine("{");
            CodeWriter.Indent++;
        }

        protected override void GenerateEpilog(IGSnippetContainer snippet)
        {
            CodeWriter.Indent--;
            CodeWriter.WriteLine("}");
            VerticalSpacingEnd(snippet);
        }
    }
}