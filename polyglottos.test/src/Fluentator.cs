// ReSharper disable ConvertToLambdaExpression

using System;
using System.Collections.Generic;
using System.IO;
using polyglottos.csharp;

namespace polyglottos.test.src
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

    public abstract class Fluentator
    {
        protected abstract IFluentatorConfig Config { get; }

        private readonly Queue<IType> work=new Queue<IType>();
        private HashSet<IType> known = new HashSet<IType>();

        private void EnqueueWork(IType wi)
        {
            if(!known.Contains(wi))
            {
                known.Add(wi);
                work.Enqueue(wi);
            }
        }

        protected void GenerateFluentAPI(IType root)
        {
            work.Enqueue(root);

            var project = new GProjectCSharp();
            while (work.Count>0)
            {
                ProcessType(project, work.Dequeue());
            }

            project.GenerateAllFiles();
        }

        protected virtual void ProcessType(GProjectCSharp project, IType root)
        {
            foreach (ITypeCollection c in root.Collections)
            {
                ITypeCollection collection = c;
                EnqueueWork(collection.Type);
                project.AddFile(Path.Combine(Config.ProjectDirectory, root.TypeName+".gen.cs"),
                    file =>
                        {
                            file.AddNamespace(root.TypeNamespace,
                                ns =>
                                    {
                                        ns.AddClass(root.TypeName + "Extensions",
                                            cls =>
                                                {
                                                    cls.IsStatic = true;
                                                    foreach (ITypeConstructor constructor in collection.Type.Constructors)
                                                    {
                                                        AddConstructor(cls, root, collection, constructor);
                                                    }
                                                });
                                    });
                        });
            }
        }

        protected virtual void AddConstructor(IGClass cls, IType root, ITypeCollection collection, ITypeConstructor constructor)
        {
            IType child = collection.Type;
            cls.AddMethod(child.TypeFullName, Config.AddPrefix + child.TypeName,
                method =>
                    {
                        method.IsStatic = true;
                        method.AddParameter(root.TypeFullName, "self",
                            self => { self.IsThis = true; });
                        foreach (IParameter parameter in constructor.Parameters)
                        {
                            method.AddParameter(parameter.TypeFullName, parameter.ParameterName);
                        }
                        method.AddParameter("System.Action<" + child.TypeFullName + ">", "result").DefaultValue(null);

                        IGExpressionStartContainer item = method.Declare(child.TypeFullName, "item");
                        InstanceFactory(item, child, constructor);

                        AddInstanceToCollection(collection, method);

                        method.AddTextStatement("if (result != null) result(item)");

                        method.Return().TextExpression("item");
                    });
        }

        protected virtual void AddInstanceToCollection(ITypeCollection collection, IGMethod method)
        {
            method.CallField("self").CallField(collection.Name).Call("Add").AddParameterVariable("item");
        }

        protected virtual IGCallParametersContainer InstanceFactory(IGExpressionStartContainer item, IType child, ITypeConstructor constructor)
        {
            return item.New(child.TypeFullName,
                ni=>
                    {
                        foreach (var parameter in constructor.Parameters)
                        {
                            ni.AddParameterVariable(parameter.ParameterName);
                        }
                    });
        }
    }
}
