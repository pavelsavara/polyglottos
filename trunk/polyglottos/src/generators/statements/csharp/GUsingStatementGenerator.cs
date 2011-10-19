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
    public class GUsingStatementGenerator : GContainerGeneratorBase
    {
        protected override void GenerateProlog(IGSnippetContainer snippet)
        {
            var statement = (IGUsingStatement) snippet;
            CodeWriter.Write("using(");
            if (statement.Name != null)
            {
                CodeWriter.Write("var ");
                CodeWriter.Write(statement.Name);
                CodeWriter.Write(" = ");
            }
            Generator.GenerateSnippet(statement.Disposable.Snippets[0]);
            CodeWriter.WriteLine(")");
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