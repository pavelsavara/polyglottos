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

using polyglottos.snippets;

namespace polyglottos
{
    public class GTypeJvm
    {
        public static readonly IGType Void = new GTextType("void", true);
        public static readonly IGType Int = new GTextType("int", true);
        public static readonly IGType Long = new GTextType("long", true);
        public static readonly IGType Byte = new GTextType("byte", true);
        public static readonly IGType Bool = new GTextType("boolean", true);
        public static readonly IGType Char = new GTextType("char", true);
        public static readonly IGType Double = new GTextType("double", true);
        public static readonly IGType Float = new GTextType("float", true);
        public static readonly IGType String = new GTextType("java.lang.String");
        public static readonly IGType Integer = new GTextType("java.lang.Integer");
        public static readonly IGType Class = new GTextType("java.lang.Class");
        public static readonly IGType NotImplementedException = new GTextType("java.lang.NotImplementedException");
    }
}