﻿#region Copyright (C) 2011 by Pavel Savara

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

namespace polyglottos.generators
{
    public class GTextTypeGenerator : GGeneratorBase, IGTypeGenerator
    {
        public override void Generate(IGSnippet snippet)
        {
            GenerateArgs((IGType)snippet, TypeArgs.NameNamespaceArgumentsPrefix);
        }

        public void GenerateArgs(IGType snippet, TypeArgs nameArgs)
        {
            var textStatement = (IGTextType)snippet;
            if (!textStatement.IsLocalName && Context["language"].Equals("CSharp") && ((nameArgs & TypeArgs.GlobalPrefix)!=0))
            {
                CodeWriter.Write("global::");
            }
            CodeWriter.Write(textStatement.Name);
        }
    }
}