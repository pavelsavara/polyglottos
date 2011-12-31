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
using System.Linq;
using System.Reflection;

namespace polyglottos.fluentator
{
    public class ReflectionFluentator : Fluentator, Fluentator.IFluentatorConfig
    {
        private class ReflType : IType
        {
            private readonly Type type;

            public ReflType(Type type)
            {
                this.type = type;
            }

            public string TypeFullName
            {
                get { return type.FullName; }
            }

            public string TypeName
            {
                get { return type.Name; }
            }

            public string TypeNamespace
            {
                get { return type.Namespace; }
            }

            public IEnumerable<ITypeConstructor> Constructors
            {
                get
                {
                    return type.GetConstructors().
                        Select(constructor => new ReflConstructor(constructor))
                        .Cast<ITypeConstructor>();
                }
            }

            public IEnumerable<ITypeCollection> Collections
            {
                get
                {
                    PropertyInfo[] properties = type.GetProperties();
                    FieldInfo[] fields = type.GetFields();

                    return properties.Where(p => CollectionTest(p.PropertyType))
                        .Select(p => new ReflCollection(p.PropertyType.GetGenericArguments()[0], p.Name))
                        .Union(fields.Where(f => CollectionTest(f.FieldType))
                            .Select(f => new ReflCollection(f.FieldType.GetGenericArguments()[0], f.Name)))
                        .Cast<ITypeCollection>();
                }
            }

            private static bool CollectionTest(Type propertyType)
            {
                return propertyType.IsGenericType &&
                       typeof(ICollection<>).MakeGenericType(propertyType.GetGenericArguments()).IsAssignableFrom(
                           propertyType);
            }

            public bool Equals(IType other)
            {
                var o = other as ReflType;
                if (o != null) return type == o.type;
                return false;
            }

            public override bool Equals(object obj)
            {
                return type.Equals(obj);
            }

            public override int GetHashCode()
            {
                return type.GetHashCode();
            }

            public override string ToString()
            {
                return TypeName;
            }
        }

        private class ReflConstructor : ITypeConstructor
        {
            private readonly ConstructorInfo constructor;

            public ReflConstructor(ConstructorInfo constructor)
            {
                this.constructor = constructor;
            }

            public IEnumerable<IParameter> Parameters
            {
                get
                {
                    return constructor.GetParameters()
                        .Select(parameter => new RelfParameter(parameter))
                        .Cast<IParameter>();
                }
            }

            public override string ToString()
            {
                return constructor.ToString();
            }
        }

        private class RelfParameter : ReflType, IParameter
        {
            private readonly ParameterInfo parameter;

            public RelfParameter(ParameterInfo parameter)
                : base(parameter.ParameterType)
            {
                this.parameter = parameter;
            }

            public string ParameterName
            {
                get { return parameter.Name; }
            }

            public override string ToString()
            {
                return parameter.ToString();
            }
        }

        private class ReflCollection : ITypeCollection
        {
            private readonly Type type;
            private readonly String name;

            public ReflCollection(Type type, String name)
            {
                this.type = type;
                this.name = name;
            }

            public string Name
            {
                get { return name; }
            }

            public IType Type
            {
                get { return new ReflType(type); }
            }

            public override string ToString()
            {
                return type + " " + name;
            }
        }

        public void GenerateFluentAPI(Type root, string projectDirectory)
        {
            ProjectDirectory = projectDirectory;
            GenerateFluentAPI(new ReflType(root));
        }

        protected override IFluentatorConfig Config
        {
            get { return this; }
        }

        public string AddPrefix
        {
            get { return "Add"; }
        }

        public string AddPostfix
        {
            get { return ""; }
        }

        public string ProjectDirectory { get; set; }
    }
}
