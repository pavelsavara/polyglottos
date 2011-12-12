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

using polyglottos.generators.csharp;

namespace polyglottos.generators
{
    public class GFieldGenerator : GExpressionGeneratorBase
    {
        public override void Generate(IGSnippet snippet)
        {
            var field = (IGField) snippet;
            VerticalSpacingBegin(field, true);

            GMemberGeneratorBase.GenerateModifiers(field, CodeWriter);
            Generator.GenerateSnippet(field.ReturnType, TypeArgs.NameNamespaceArgumentsPrefix);
            CodeWriter.Write(" ");
            CodeWriter.Write(field.Name);
            if (field.Snippets.Count > 0)
            {
                CodeWriter.Write(" = ");
                GenerateChain(field);
            }
            CodeWriter.WriteLine(";");
        }
    }
}