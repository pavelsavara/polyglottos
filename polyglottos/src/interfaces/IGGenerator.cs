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

using System;

namespace polyglottos
{
    public interface IGGenerator
    {
        Type TypeOfSnippet { get; set; }
        void Init(IGSnippetGenerator generator, IGCodeWriter codeWriter, IGContext context);
        void Generate(IGSnippet snippet);
    }

    public interface IGWriterGenerator : IGGenerator
    {
        IGCodeWriter CreateWriter(IGFile snippet);
    }

    public interface IGTypeGenerator : IGGenerator
    {
        void GenerateArgs(IGType snippet, TypeArgs nameArgs);
    }
}