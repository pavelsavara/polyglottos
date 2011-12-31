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

// ReSharper disable ConvertToLambdaExpression

using System.Collections.Generic;
using System.IO;
using polyglottos.csharp;

namespace polyglottos.fluentator
{
    public abstract partial class Fluentator : GProjectCSharp
    {
        protected abstract IFluentatorConfig Config { get; }

        private readonly Queue<IType> work = new Queue<IType>();
        private readonly HashSet<IType> known = new HashSet<IType>();

        private void EnqueueWork(IType wi)
        {
            if (!known.Contains(wi))
            {
                known.Add(wi);
                work.Enqueue(wi);
            }
        }

        protected void GenerateFluentAPI(IType root)
        {
            GenerateFluentAPI(new[] { root });
        }

        protected void GenerateFluentAPI(IEnumerable<IType> roots)
        {
            foreach (var root in roots)
            {
                work.Enqueue(root);
            }

            while (work.Count > 0)
            {
                ProcessType(work.Dequeue());
            }

            GenerateAllFiles();
        }

        protected virtual IGNamespace ProcessType(IType root)
        {
            IGNamespace res = null;
            AddFile(Path.Combine(Config.ProjectDirectory, root.TypeName + ".gen.cs"),
                file =>
                {
                    res = file.AddNamespace(root.TypeNamespace,
                        ns =>
                        {
                            ns.AddClass(root.TypeName + "Extensions",
                                cls =>
                                {
                                    cls.IsStatic = true;
                                    foreach (ITypeCollection collection in root.Collections)
                                    {
                                        EnqueueWork(collection.Type);
                                        foreach (ITypeConstructor constructor in collection.Type.Constructors)
                                        {
                                            AddConstructor(cls, root, collection, constructor);
                                        }
                                    }
                                });
                        });

                });
            return res;
        }

        protected virtual IGMethod AddConstructor(IGClass cls, IType root, ITypeCollection collection, ITypeConstructor constructor)
        {
            IType child = collection.Type;
            IGMethod res = cls.AddMethod(child.TypeFullName, Config.AddPrefix + child.TypeName + Config.AddPostfix,
                method =>
                {
                    method.IsStatic = true;
                    method.AddParameter(root.TypeFullName, "self", self => { self.IsThis = true; });

                    AddConstructorParameters(collection, constructor, method);

                    method.AddParameter("System.Action<" + child.TypeFullName + ">", "result").DefaultValue(null);

                    IGExpressionStartContainer item = method.Declare(child.TypeFullName, "item");
                    InstanceFactory(item, collection, constructor);

                    AddInstanceToCollection(collection, method);

                    method.AddTextStatement("if (result != null) result(item)");

                    method.Return().TextExpression("item");
                });
            return res;
        }

        protected virtual void AddConstructorParameters(ITypeCollection collection, ITypeConstructor constructor, IGMethod method)
        {
            foreach (IParameter parameter in constructor.Parameters)
            {
                method.AddParameter(parameter.TypeFullName, parameter.ParameterName);
            }
        }

        protected virtual void AddInstanceToCollection(ITypeCollection collection, IGMethod method)
        {
            method.CallField("self").CallField(collection.Name).Call("Add").AddParameterVariable("item");
        }

        protected virtual IGCallParametersContainer InstanceFactory(IGExpressionStartContainer item, ITypeCollection collection, ITypeConstructor constructor)
        {
            return item.New(collection.Type.TypeFullName,
                callConstructor =>
                {
                    InstanceFactoryParams(collection, constructor, callConstructor);
                });
        }

        protected virtual void InstanceFactoryParams(ITypeCollection collection, ITypeConstructor constructor, IGCallConstructorExpression callConstructor)
        {
            foreach (var parameter in constructor.Parameters)
            {
                callConstructor.AddParameterVariable(parameter.ParameterName);
            }
        }
    }
}
