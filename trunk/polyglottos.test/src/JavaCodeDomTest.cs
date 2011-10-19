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
using NUnit.Framework;
using polyglottos.java;

namespace polyglottos.test
{
    [TestFixture]
    public class JavaCodeDomTest
    {
        private readonly GProjectJava GProjectJava = new GProjectJava();

        [Test]
        public void TestJava()
        {
            IGFile gFile = GProjectJava.AddFile(@"filename.cs",
                file => file.AddNamespace("zamo",
                    zamo =>
                        {
                            zamo.AddComment("asasa");
                            zamo.AddClass("MyClass",
                                clazz =>
                                    {
                                        clazz.AddMethod(GTypeJvm.Void, "Init",
                                            method =>
                                                {
                                                    method.AddParameter("Foo", "foo");
                                                    method.AddXmlDoc("<summary>sdfdfsf</summary>");
                                                    method.AddComment("smart foo method");
                                                    method.Return().Call("test",
                                                        test =>
                                                            {
                                                                test.AddParameter().Call("Helper.Sos",
                                                                    sosParams => sosParams.AddParameterValue("...--..."))
                                                                    .Indexer(index => index.AddParameterValue(1));
                                                                test.AddParameterVariable("localVar1");
                                                                test.AddParameterVariable("localVar2",
                                                                    localVar2 =>
                                                                    localVar2.Indexer(p3 => _BodyFactoryRocks.AddParameterVariable(p3, "sd")));
                                                            })
                                                        .Call("baz").AddParameter().TypeOf(GTypeJvm.Integer);
                                                    method.AddTextStatement("return null");
                                                });
                                        clazz.AddConstructor("MyClass",
                                            constructor =>
                                                {
                                                    constructor.AddParameter(GTypeJvm.Int, "i");
                                                    constructor.CallConstructorBase(bp => bp.AddParameterValue("654"));
                                                    constructor.ThrowNotImplemented();
                                                    constructor.CallStatic(GTypeJvm.Integer, "Parse",
                                                        ps => ps.AddParameterValue("123"));
                                                    constructor.ThrowNew(GTypeJvm.NotImplementedException);
                                                });
                                        clazz.AddField(GTypeJvm.Bool, "isDummy", dummy => dummy.IsPrivate = true).CallStatic(GTypeJvm.Bool, "Parse", pa => pa.AddParameterValue("true"));
                                        clazz.AddField(GTypeJvm.Bool, "isSmart", dummy => dummy.IsPrivate = true);
                                    });
                        }));

            var cg = new JavaCodeGenerator();
            cg.Generate(gFile, Console.Out);
        }
    }
}
