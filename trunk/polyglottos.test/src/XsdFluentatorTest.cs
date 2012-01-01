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

using System;
using System.Xml.Linq;
using demomodel;
using NUnit.Framework;
using polyglottos.fluentator;

namespace polyglottos.test
{
    
    [TestFixture]
    public class XsdFluentatorTest
    {
        [Test]
        public void TestXSDFluentator()
        {
            var rf = new XsdFluentator();

            rf.GenerateFluentAPI(XDocument.Load(@"..\..\DemoModel\library.xsd"), "demomodel", @"..\..\DemoModel");
        }
        
        [Test]
        public void TestLibraryBuilder()
        {
            var doc=new XDocument();
            doc.AddLibrary("Prague",prague =>
            {
                prague.AddBook("Saturnin",saturnin =>
                {
                    saturnin.AddAuthor("Zdenek Jirotka");
                });
                prague.AddBook("Bylo Nas 5", saturnin =>
                {
                    saturnin.AddAuthor("Karel Polacek");
                });
            });
            Console.WriteLine(doc);
        }
    }
}