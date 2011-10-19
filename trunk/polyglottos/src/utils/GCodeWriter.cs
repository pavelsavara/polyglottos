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
using System.Text;

namespace polyglottos.utils
{
    public class GCodeWriter : TextWriter, IGCodeWriter
    {
        private readonly TextWriter tw;

        private int currentIndent;

        private bool i = true;
        private string ind = string.Empty;

        public GCodeWriter(TextWriter tw)
        {
            this.tw = tw;
        }

        #region IGCodeWriter Members

        public int Indent
        {
            get { return currentIndent; }
            set
            {
                currentIndent = value;
                ind = new string(' ', Indent*4);
            }
        }

        public override Encoding Encoding
        {
            get { return tw.Encoding; }
        }

        public override void Write(char value)
        {
            if (i)
            {
                tw.Write(ind);
                i = false;
            }
            tw.Write(value);
        }

        public override void Write(string value)
        {
            if (i)
            {
                tw.Write(ind);
                i = false;
            }
            tw.Write(value);
        }

        public override void WriteLine()
        {
            if (i)
            {
                tw.Write(ind);
                i = false;
            }
            tw.WriteLine();
            i = true;
        }

        public override void WriteLine(string value)
        {
            if (i)
            {
                tw.Write(ind);
                i = false;
            }
            tw.WriteLine(value);
            i = true;
        }

        public void WriteLine(string value, bool indent)
        {
            if (i && indent)
            {
                tw.Write(ind);
                i = false;
            }
            tw.WriteLine(value);
            i = true;
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                tw.Dispose();
            }
        }
    }
}