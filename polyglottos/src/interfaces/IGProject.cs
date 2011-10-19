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
using System.Collections.Generic;
using System.IO;

namespace polyglottos
{
    public interface IGProject : IGFileContainer
    {
        //factories
        T CreateSnippet<T>() where T : class, IGSnippet;
        IGCodeGenerator CreateGenerator(IGContext context = null);

        //container
        IList<IGFile> Files { get; }
        IGFile AddFile(string fileName, Action<IGFile> with = null);

        //generator wrappers
        string GenerateSnippet(IGSnippet snippet, IGContext context = null);
        void GenerateSnippet(IGSnippet snippet, IGCodeWriter codeWriter, IGContext context = null);
        void GenerateSnippet(IGSnippet snippet, TextWriter writer, IGContext context = null);
        void GenerateFile(IGFile file, IGContext context = null);
        void GenerateAllFiles(IGContext context = null);
    }
}