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
using System.Text;

namespace polyglottos
{
    public interface IGCodeWriter : IDisposable
    {
        int Indent { get; set; }
        Encoding Encoding { get; }
        IFormatProvider FormatProvider { get; }
        string NewLine { get; set; }
        void Write(char value);
        void Write(string value);
        void WriteLine();
        void WriteLine(string value);
        void WriteLine(string value, bool indent);
        void Flush();
        void Write(char[] buffer);
        void Write(char[] buffer, int index, int count);
        void Write(bool value);
        void Write(int value);
        void Write(uint value);
        void Write(long value);
        void Write(ulong value);
        void Write(float value);
        void Write(double value);
        void Write(decimal value);
        void Write(object value);
        void Write(string format, object arg0);
        void Write(string format, object arg0, object arg1);
        void Write(string format, object arg0, object arg1, object arg2);
        void Write(string format, params object[] arg);
        void WriteLine(char value);
        void WriteLine(char[] buffer);
        void WriteLine(char[] buffer, int index, int count);
        void WriteLine(bool value);
        void WriteLine(int value);
        void WriteLine(uint value);
        void WriteLine(long value);
        void WriteLine(ulong value);
        void WriteLine(float value);
        void WriteLine(double value);
        void WriteLine(decimal value);
        void WriteLine(object value);
        void WriteLine(string format, object arg0);
        void WriteLine(string format, object arg0, object arg1);
        void WriteLine(string format, object arg0, object arg1, object arg2);
        void WriteLine(string format, params object[] arg);
    }
}