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

using polyglottos.generators;
using polyglottos.generators.java;
using polyglottos.snippets;
using GFileGenerator = polyglottos.generators.java.GFileGenerator;

namespace polyglottos.java
{
    public class JavaCodeGenerator : CodeGeneratorBase
    {
        public JavaCodeGenerator(IGContext context = null)
            : base(context)
        {
            Context["language"] = "Java";
        }

        protected override void RegisterKnownGenerators()
        {
            base.RegisterKnownGenerators();

            RegisterStrategy<GFile, GFileGenerator>();
            RegisterStrategy<GNamespace, GNamespaceGenerator>();
            RegisterStrategy<GClass, GClassGenerator>();
            RegisterStrategy<GAttributeSnippet, GAttributeGenerator>();

            //members
            RegisterStrategy<GMethod, GMethodGenerator>();
            RegisterStrategy<GConstructor, GConstructorGenerator>();
            RegisterStrategy<GParameter, GParameterGenerator>();
            RegisterStrategy<GField, GFieldGenerator>();

            //expressions
            RegisterStrategy<GLiteralExpression, GLiteralExpressionGenerator>();
            RegisterStrategy<GTypeofExpression, GTypeofExpressionGenerator>();
        }
    }
}