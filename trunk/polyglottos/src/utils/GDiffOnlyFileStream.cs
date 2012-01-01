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

using System.IO;
using System.Linq;

namespace polyglottos.utils
{
    public class GDiffOnlyFileStream : MemoryStream
    {
        private readonly string targetFileName;
        public GDiffOnlyFileStream(string targetFileName)
        {
            this.targetFileName = targetFileName;
        }

        public override void Close()
        {
            base.Close();
            var originalBytes = File.ReadAllBytes(targetFileName);
            var newBytes = ToArray();
            if(Equals(originalBytes, newBytes))
            {
                File.WriteAllBytes(targetFileName, newBytes);
            }
        }

        public static bool Equals(byte[] originalBytes, byte[] newBytes)
        {
            return (originalBytes.Length != newBytes.Length || originalBytes.Where((t, i) => t != newBytes[i]).Any());
        }
    }
}