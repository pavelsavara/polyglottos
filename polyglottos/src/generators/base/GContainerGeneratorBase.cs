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

using System.Collections.Generic;

namespace polyglottos.generators
{
    public abstract class GContainerGeneratorBase : GGeneratorBase
    {
        protected virtual void GenerateProlog(IGSnippetContainer snippet)
        {
        }

        protected virtual void GenerateEpilog(IGSnippetContainer snippet)
        {
        }

        protected virtual void GenerateBody(IGSnippetContainer snippet)
        {
            IList<IGSnippet> snippets = snippet.Snippets;
            for (int i = 0; i < snippets.Count; i++)
            {
                IGSnippet childSnippet = snippets[i];
                Generator.GenerateSnippet(childSnippet);
                if (snippet is IGStatementContainer && childSnippet is IGExpression && !(childSnippet is IGStatement))
                {
                    CodeWriter.WriteLine(";");
                }
            }
        }

        public override void Generate(IGSnippet snippet)
        {
            GenerateProlog((IGSnippetContainer) snippet);
            GenerateBody((IGSnippetContainer) snippet);
            GenerateEpilog((IGSnippetContainer) snippet);
        }
    }
}