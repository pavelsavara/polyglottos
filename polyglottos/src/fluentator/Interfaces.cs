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

namespace polyglottos.fluentator
{
    public abstract partial class Fluentator
    {
        public interface ITypeConstructor
        {
            IEnumerable<IParameter> Parameters { get; }
        }

        public interface ITypeCollection
        {
            string Name { get; }
            IType Type { get; }
        }

        public interface IType : IEquatable<IType>
        {
            string TypeFullName { get; }
            string TypeName { get; }
            string TypeNamespace { get; }
            IEnumerable<ITypeConstructor> Constructors { get; }
            IEnumerable<ITypeCollection> Collections { get; }
        }

        public interface IParameter : IType
        {
            string ParameterName { get; }
        }

        public interface IFluentatorConfig
        {
            string AddPrefix { get; }
            string AddPostfix { get; }
            string ProjectDirectory { get; }
        }
    }
}
