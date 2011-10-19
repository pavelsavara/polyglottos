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

using System;

namespace polyglottos.generators
{
    public abstract class GGeneratorBase : IGGenerator
    {
        protected IGCodeWriter CodeWriter;
        protected IGContext Context;
        protected IGSnippetGenerator Generator;

        #region IGGenerator Members

        public Type TypeOfSnippet { get; set; }

        public void Init(IGSnippetGenerator generator, IGCodeWriter codeWriter, IGContext context)
        {
            Generator = generator;
            CodeWriter = codeWriter;
            Context = context;
        }

        public abstract void Generate(IGSnippet snippet);

        #endregion

        protected void VerticalSpacingBegin(IGSnippet ns, bool first = false)
        {
            if (ns.ParentSnippet != null &&
                (first || ns.ParentSnippet.Snippets.IndexOf(ns) != 0 || ns.ParentSnippet is IGRegion))
            {
                CodeWriter.WriteLine();
            }
        }

        protected void VerticalSpacingEnd(IGSnippet ns)
        {
            if (ns.ParentSnippet != null && ns.ParentSnippet is IGRegion)
            {
                CodeWriter.WriteLine();
            }
        }

        protected void GenerateCallParams(IGCallParametersContainer expression)
        {
            for (int i = 0; i < expression.Parameters.Snippets.Count; i++)
            {
                if (i > 0)
                {
                    CodeWriter.Write(", ");
                }
                Generator.GenerateSnippet(expression.Parameters.Snippets[i]);
            }
        }
    }
}