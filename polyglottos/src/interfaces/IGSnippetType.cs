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

namespace polyglottos
{
    [Flags]
    public enum TypeArgs
    {
        Name = 0x1,
        Namespace = 0x2,
        Arguments = 0x4,
        Constraints = 0x8,
        GlobalPrefix = 0x10,
        Reflection = 0x20,
        Signature = 0x40,
        Filename = 0x80,

        NameNamespace = Name | Namespace,
        NameNamespaceArguments = Name | Namespace | Arguments,
        NameNamespaceArgumentsPrefix = Name | Namespace | Arguments | GlobalPrefix,
        All = Name | Namespace | Arguments | Constraints | GlobalPrefix,
    }

    public interface IGType : IGSnippet
    {
        bool IsLocalName { get; set; }
    }

    public interface IGTextType : IGType
    {
    }
}