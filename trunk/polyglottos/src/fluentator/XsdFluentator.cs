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
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using polyglottos.snippets;

namespace polyglottos.fluentator
{
    public class XsdFluentator : Fluentator, Fluentator.IFluentatorConfig
    {
        static readonly XmlNamespaceManager nsManager = new XmlNamespaceManager(new NameTable());
        static XsdFluentator()
        {
            nsManager.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
        }

        class XsdRoot : IType
        {
            public readonly XElement root;
            public readonly string typeNamespace;
            public readonly string xmlNamespace;
            public XsdRoot(XElement root, string typeNamespace)
            {
                this.root = root;
                this.typeNamespace = typeNamespace;
                xmlNamespace = root.Attribute(XName.Get("targetNamespace")).Value;
            }

            public bool Equals(IType other)
            {
                return false;
            }

            public override bool Equals(object obj)
            {
                return false;
            }

            public override int GetHashCode()
            {
                return 31415927;
            }

            public string TypeFullName
            {
                get { return "System.Xml.Linq.XDocument"; }
            }

            public string TypeName
            {
                get { return "XDocument"; }
            }

            public string TypeNamespace
            {
                get { return "System.Xml.Linq"; }
            }

            public IEnumerable<ITypeConstructor> Constructors
            {
                get
                {
                    return new ITypeConstructor[] { };
                }
            }

            public IEnumerable<ITypeCollection> Collections
            {
                get
                {
                    IEnumerable<XElement> collections = root.XPathSelectElements("xs:element", nsManager).Where(
                        e =>
                        {
                            XAttribute mo = e.Attribute(XName.Get("maxOccurs"));
                            return mo == null || mo.Value != "1";
                        });

                    var res = new List<ITypeCollection>();
                    foreach (var collection in collections)
                    {
                        XAttribute name = collection.Attribute(XName.Get("name"));
                        XAttribute type = collection.Attribute(XName.Get("type"));
                        if (name != null && type != null)
                        {
                            res.Add(XsdCollection(name.Value, type.Value));
                        }
                    }
                    return res;
                }
            }

            public XsdCollection XsdCollection(string name, string type)
            {
                var collection = new XsdCollection(new XsdType(type, this), name);
                return collection;
            }
        }

        private class XsdType : IType
        {
            private readonly string typeName;
            public readonly XsdRoot root;

            public XsdType(string typeName, XsdRoot root)
            {
                this.typeName = typeName;
                this.root = root;
            }

            public bool Equals(IType other)
            {
                var o = other as XsdType;
                if (o != null) return typeName == o.typeName;
                return false;
            }

            public override bool Equals(object obj)
            {
                var o = obj as XsdType;
                if (o != null) return typeName == o.typeName;
                return false;
            }

            public override int GetHashCode()
            {
                return typeName.GetHashCode();
            }

            public override string ToString()
            {
                return typeName;
            }

            public string TypeFullName
            {
                get
                {
                    return TypeNamespace + "." + TypeName;
                }
            }

            public string TypeName
            {
                get { return typeName; }
            }

            public virtual string TypeNamespace
            {
                get { return root.typeNamespace; }
                set { }
            }

            public IEnumerable<ITypeConstructor> Constructors
            {
                get
                {
                    IEnumerable<XElement> required = root.root.XPathSelectElements("xs:complexType[@name='" + typeName + "']/xs:attribute[@use='required']", nsManager);
                    var res = new List<ITypeConstructor>();
                    res.Add(new XsdConstructor(required.ToList(), root));
                    return res;
                }
            }

            public IEnumerable<ITypeCollection> Collections
            {
                get
                {
                    IEnumerable<XElement> collections = root.root.XPathSelectElements("xs:complexType[@name='" + typeName + "']/xs:sequence/xs:element", nsManager).Where(
                        e =>
                        {
                            XAttribute mo = e.Attribute(XName.Get("maxOccurs"));
                            return mo == null || mo.Value != "1";
                        });

                    var res = new List<ITypeCollection>();
                    foreach (var collection in collections)
                    {
                        XAttribute name = collection.Attribute(XName.Get("name"));
                        XAttribute type = collection.Attribute(XName.Get("type"));
                        if (name != null && type != null)
                        {
                            res.Add(root.XsdCollection(name.Value, type.Value));
                        }
                    }
                    return res;
                }
            }
        }

        private class XsdConstructor : ITypeConstructor
        {
            private readonly IList<XElement> required;
            private readonly XsdRoot root;
            public XsdConstructor(IList<XElement> required, XsdRoot root)
            {
                this.required = required;
                this.root = root;
            }

            public IEnumerable<IParameter> Parameters
            {
                get
                {
                    var res = new List<IParameter>();
                    foreach (var attr in required)
                    {
                        XAttribute name = attr.Attribute(XName.Get("name"));
                        XAttribute type = attr.Attribute(XName.Get("type"));
                        if (name != null && type != null)
                        {
                            string typeName = type.Value;
                            string typeNamespace = root.typeNamespace;
                            if (typeName == "xs:string")
                            {
                                typeName = "String";
                                typeNamespace = "System";
                            }
                            else if (typeName.StartsWith("xs:"))
                            {
                                throw new NotImplementedException(typeName);
                            }
                            res.Add(new XsdParameter(name.Value, typeName, typeNamespace, root));
                        }
                    }

                    return res;
                }
            }
        }

        private class XsdParameter : XsdType, IParameter
        {
            public XsdParameter(string paramName, string typeName, string typeNamespace, XsdRoot root)
                : base(typeName, root)
            {
                ParameterName = paramName;
                TypeNamespace = typeNamespace;
            }

            public override string TypeNamespace { get; set; }

            public string ParameterName { get; set; }
        }


        private class XsdCollection : ITypeCollection
        {
            private readonly XsdType type;
            private readonly String name;

            public XsdCollection(XsdType type, String name)
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
                get { return type; }
            }

            public override string ToString()
            {
                return type + " " + name;
            }
        }


        public void GenerateFluentAPI(XDocument Xsd, string nameSpace, string projectDirectory)
        {
            ProjectDirectory = projectDirectory;
            GenerateFluentAPI(new XsdRoot(Xsd.Root, nameSpace));
        }

        protected override IGNamespace ProcessType(IType root)
        {
            IGNamespace ns = base.ProcessType(root);
            if (!(root is XsdRoot))
            {
                XsdType type = root as XsdType;

                ns.AddClass(root.TypeName,
                    model =>
                    {
                        model.Extends = new GTextType("System.Xml.Linq.XElement");
                        foreach (ITypeConstructor typeConstructor in root.Constructors)
                        {
                            model.AddConstructor(
                                constructor =>
                                {
                                    constructor.AddParameter(GTypeClr.String, "xelementname");
                                    constructor.CallConstructorBase().AddParameter().TextExpression(
                                        "System.Xml.Linq.XName.Get(xelementname,\"" + type.root.xmlNamespace + "\")");

                                    foreach (IParameter parameter in typeConstructor.Parameters)
                                    {
                                        constructor.AddParameter(parameter.TypeFullName, parameter.ParameterName);
                                        constructor.Call("Add").AddParameter().New("System.Xml.Linq.XAttribute",
                                                nw =>
                                                {
                                                    nw.AddParameter().Call("System.Xml.Linq.XName.Get").
                                                        AddParameterValue(parameter.ParameterName);
                                                    nw.AddParameterVariable(parameter.ParameterName);
                                                });
                                    }
                                });
                        }
                    });
            }
            return ns;
        }

        protected override void AddInstanceToCollection(ITypeCollection collection, IGMethod method)
        {
            method.CallField("self").Call("Add").AddParameterVariable("item");
        }

        protected override void InstanceFactoryParams(ITypeCollection collection, ITypeConstructor constructor, IGCallConstructorExpression callConstructor)
        {
            callConstructor.AddParameterValue(collection.Name);
            base.InstanceFactoryParams(collection, constructor, callConstructor);
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
