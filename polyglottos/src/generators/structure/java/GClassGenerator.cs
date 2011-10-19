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

namespace polyglottos.generators.java
{
    public class GClassGenerator : GContainerGeneratorBase
    {
        protected override void GenerateProlog(IGSnippetContainer snippet)
        {
            var clazz = (IGClass) snippet;

            VerticalSpacingBegin(clazz, true);

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
            if (clazz.IsSealed)
            {
                CodeWriter.Write("final ");
            }
            if (clazz.IsAbstract)
            {
                CodeWriter.Write("abstract ");
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
            /*if (GenericArguments.Count > 0)
            {
                CodeWriter.Write("<");
                for (int i = 0; i < GenericArguments.Count; i++)
                {
                    var argument = GenericArguments[i];
                    if (i > 0)
                    {
                        CodeWriter.Write(", ");
                    }
                    argument.GenerateArgs(generator, CodeWriter, context, TypeSnipetArgs.NameNamespaceArguments);
                }
                CodeWriter.Write(">");
            }*/
            if (clazz.Extends != null)
            {
                CodeWriter.Write(" extends ");
                Generator.GenerateSnippet(clazz.Extends, TypeArgs.NameNamespaceArguments);
                foreach (IGType implement in clazz.Implements)
                {
                    CodeWriter.Write(" implements ");
                    Generator.GenerateSnippet(implement, TypeArgs.NameNamespaceArguments);
                }
            }
            else if (clazz.Implements.Count > 0)
            {
                for (int i = 0; i < clazz.Implements.Count; i++)
                {
                    CodeWriter.Write(i == 0 ? " implements " : ", ");
                    Generator.GenerateSnippet(clazz.Implements[i], TypeArgs.NameNamespaceArguments);
                }
            }
            CodeWriter.WriteLine("{");
            /*
            foreach (var argument in GenericArguments)
            {
                argument.GenerateArgs(generator, CodeWriter, context, TypeSnipetArgs.Constraints);
            }*/
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