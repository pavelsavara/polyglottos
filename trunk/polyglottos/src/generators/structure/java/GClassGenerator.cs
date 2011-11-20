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
            if (clazz.GenericArguments.Count > 0)
            {
                CodeWriter.Write("<");
                for (int i = 0; i < clazz.GenericArguments.Count; i++)
                {
                    var argument = clazz.GenericArguments[i];
                    if (i > 0)
                    {
                        CodeWriter.Write(", ");
                    }
                    Generator.GenerateSnippet(argument, TypeArgs.All);
                }
                CodeWriter.Write(">");
            }
            if (clazz.Extends != null)
            {
                CodeWriter.Write(" extends ");
                Generator.GenerateSnippet(clazz.Extends, TypeArgs.NameNamespaceArguments);
            }
            if (clazz.Implements.Count > 0)
            {
                for (int i = 0; i < clazz.Implements.Count; i++)
                {
                    CodeWriter.Write(i == 0
                                         ? (clazz.IsInterface
                                                ? " extends "
                                                : " implements ")
                                         : ", ");
                    Generator.GenerateSnippet(clazz.Implements[i], TypeArgs.NameNamespaceArguments);
                }
            }
            IfcExtension(clazz.Name);
            
            CodeWriter.WriteLine("{");
            CodeWriter.Indent++;

            CodeExtension(clazz.Name);
        }

        //TODO refactor out
        private void IfcExtension(string name)
        {
            string ifcStart = "// <j4ni-" + name + ">";
            string extension = "// put user interfaces here";
            string ifcEnd = "// </j4ni-" + name + ">";

            bool old = GFileGenerator.GetOldExtension(Context, ifcStart, ref extension, ifcEnd);

            CodeWriter.WriteLine();
            CodeWriter.WriteLine(ifcStart);
            CodeWriter.WriteLine(extension, !old);
            CodeWriter.WriteLine(ifcEnd);
        }

        private void CodeExtension(string name)
        {
            string bodyStart = "// <j4nb-" + name + "> ";
            string extension = "// put user members here";
            string bodyEnd = "// </j4nb-" + name + ">";

            bool old = GFileGenerator.GetOldExtension(Context, bodyStart, ref extension, bodyEnd);

            CodeWriter.WriteLine(bodyStart);
            CodeWriter.WriteLine(extension, !old);
            CodeWriter.WriteLine(bodyEnd);
        }

        protected override void GenerateEpilog(IGSnippetContainer snippet)
        {
            CodeWriter.Indent--;
            CodeWriter.WriteLine("}");
            VerticalSpacingEnd(snippet);
        }
    }
}