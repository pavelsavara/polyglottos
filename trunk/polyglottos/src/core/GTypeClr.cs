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

using polyglottos.snippets;

namespace polyglottos
{
    public class GTypeClr
    {
        public static readonly IGType Void = new GTextType("void", true);
        public static readonly IGType Int = new GTextType("int", true);
        public static readonly IGType Long = new GTextType("long", true);
        public static readonly IGType Byte = new GTextType("byte", true);
        public static readonly IGType Bool = new GTextType("bool", true);
        public static readonly IGType Char = new GTextType("char", true);
        public static readonly IGType String = new GTextType("string", true);
        public static readonly IGType Decimal = new GTextType("decimal", true);
        public static readonly IGType Double = new GTextType("double", true);
        public static readonly IGType Object = new GTextType("object", true);
        public static readonly IGType Type = new GTextType("System.Type");
        public static readonly IGType IntPtr = new GTextType("System.IntPtr");
        public static readonly IGType IntPtrArray = new GTextType("System.IntPtr[]");
        public static readonly IGType NotImplementedException = new GTextType("System.NotImplementedException");
        public static readonly IGType SystemException = new GTextType("System.Exception");
    }
}