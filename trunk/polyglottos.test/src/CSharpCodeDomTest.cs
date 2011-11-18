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
using System.Linq;
using NUnit.Framework;
using polyglottos.csharp;

namespace polyglottos.test
{
    [TestFixture]
    public class CSharpCodeDomTest
    {
        [Test]
        public void TestCSharp()
        {
            var project = new GProjectCSharp();

            IGFile file = project.AddFile(@"MyClass.cs",
                fileMyClass => fileMyClass.AddRegion("generated",
                    generated => generated.AddNamespace("zamo",
                        zamo =>
                            {
                                zamo.AddNamespace("tap");
                                zamo.AddRegion("cool", cool => cool.AddComment("note"));
                                zamo.AddClass("MyClass",
                                    clazz =>
                                        {
                                            clazz.AddField(GTypeClr.Bool, "isDummy", dummy => dummy.IsPrivate = true).
                                                CallStatic(
                                                    GTypeClr.Bool, "Parse", pa => pa.AddParameterValue("true"));
                                            clazz.AddField(GTypeClr.Bool, "isSmart", dummy => dummy.IsPrivate = true);

                                            clazz.AddProperty(GTypeClr.String, "FirstName",
                                                firstName =>
                                                    {
                                                        firstName.AddGetter(getter => getter.Return().Value("Gogo"));
                                                        firstName.AddSetter(setter => setter.ThrowNotImplemented()).
                                                            IsProtected = true;
                                                    });
                                            clazz.AddProperty(GTypeClr.String, "WriteOnly",
                                                writeOnly =>
                                                writeOnly.AddSetter(
                                                    setter =>
                                                    setter.Assign("writeOnly", ass => ass.TextExpression("value"))));
                                            clazz.AddProperty(GTypeClr.String, "ReadOnly",
                                                readOnly => readOnly.AddGetter(getter => getter.ThrowNotImplemented()));

                                            clazz.AddConstructor("MyClass",
                                                constructor =>
                                                    {
                                                        constructor.AddParameter(GTypeClr.Int, "i");
                                                        constructor.CallConstructorBase(
                                                            baseCall => baseCall.AddParameterValue(654));
                                                        constructor.ThrowNotImplemented();
                                                        constructor.CallStatic(GTypeClr.Int, "Parse",
                                                            parse => parse.AddParameterValue("123"));
                                                        constructor.UsingNew("FooDisp",
                                                            newFoo => newFoo.AddParameterVariable("i"),
                                                            usingFoo => usingFoo.UsingNew("BarDisp", "bar", null,
                                                                usingBar =>
                                                                usingBar.ThrowNew(GTypeClr.NotImplementedException)));
                                                    });

                                            clazz.AddMethod(GTypeClr.Void, "Init",
                                                init =>
                                                    {
                                                        init.AddParameter("Foo", "foo");
                                                        init.AddXmlDoc("<summary>sdfdfsf</summary>");
                                                        init.AddComment("smart foo method");
                                                        init.Return().Call("test",
                                                            test =>
                                                                {
                                                                    test.AddParameter().CallStatic("Helper", "Sos",
                                                                        sosParams =>
                                                                        sosParams.AddParameterValue("...--..."))
                                                                        .Indexer(indexer => indexer.AddParameterValue(1));
                                                                    test.AddParameterVariable("localVar1");
                                                                    test.AddParameterVariable("localVar2",
                                                                        localVar2 =>
                                                                        localVar2.Indexer(
                                                                            indexer =>
                                                                            indexer.AddParameterVariable("sd")));
                                                                })
                                                            .Call("baz").AddParameter().Cast(GTypeClr.Object).
                                                            AddParameter().TypeOf("string");
                                                        init.AddRegion("dead code").AddTextStatement("return null");

                                                        init.Declare(GTypeClr.String, "text")
                                                            .Static(GTypeClr.String).TextExpression("Empty");
                                                    });

                                            clazz.AddTextSnippet(
                                                "internal event Action<int> NumberGenertor = (i) => Console.WriteLine(i);\n");
                                        });
                            })));

            foreach (IGNamespace ns in file.GetAllNamespaces())
            {
                ns.AddComment("I was here!");
                foreach (IGClass clazz in ns.GetAllClasses())
                {
                    foreach (IGMethod method in clazz.GetMethods().Where(m => m.IsPublic))
                    {
                        method.AddXmlDoc("<summmary>pure beauty</summmary>");
                    }
                }
            }

            file.GetClass("MyClass").GetMethod("Init").AddComment("Found it");

            project.GenerateSnippet(file, Console.Out);
        }
    }
}
