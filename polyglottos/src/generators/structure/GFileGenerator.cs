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

using System.IO;
using polyglottos.utils;

namespace polyglottos.generators
{
    public class GFileGenerator : GContainerGeneratorBase, IGWriterGenerator
    {
        #region IGWriterGenerator Members

        public virtual IGCodeWriter CreateWriter(IGFile snippet)
        {
            string dir = Path.GetDirectoryName(snippet.Name);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return new GCodeWriter(new StreamWriter(new GDiffOnlyFileStream(snippet.Name)));
        }

        #endregion
    }
}