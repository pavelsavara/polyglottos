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
using System.IO;
using polyglottos.snippets;
using polyglottos.utils;

namespace polyglottos.generators
{
    public abstract class CodeGeneratorBase : IGCodeGenerator, IGSnippetGenerator
    {
        private readonly Dictionary<Type, IGGenerator> strategies = new Dictionary<Type, IGGenerator>();
        private readonly IGContext context;
        private IGCodeWriter codeWriter;

        protected IGContext Context
        {
            get { return context; }
        }

        protected IGCodeWriter CodeWriter
        {
            get { return codeWriter; }
        }

        protected CodeGeneratorBase(IGContext context = null)
        {
            this.context = context ?? new GContext();

            RegisterKnownGenerators();
        }

        #region IGCodeGenerator Members

        public void Generate(IGSnippet snippet, IGCodeWriter writer)
        {
            codeWriter = writer;
            foreach (IGGenerator strat in strategies.Values)
            {
                strat.Init(this, writer, Context);
            }
            ((IGSnippetGenerator) this).GenerateSnippet(snippet);
        }

        public void Generate(IGFile snippet)
        {
            IGGenerator writerGenerator;
            if (!strategies.TryGetValue(typeof (IGWriterGenerator), out writerGenerator))
            {
                throw new InvalidProgramException(GetType().Name + ": Unknow generator strategy for " +
                                                  typeof (IGWriterGenerator));
            }

            using (IGCodeWriter cw = ((IGWriterGenerator) writerGenerator).CreateWriter(snippet))
            {
                Generate(snippet, cw);
            }
        }

        #endregion

        #region IGSnippetGenerator Members

        void IGSnippetGenerator.GenerateSnippet(IGSnippet snippet)
        {
            IGGenerator strategy;
            Type snippetType = snippet.GetType();
            if (!strategies.TryGetValue(snippetType, out strategy))
            {
                throw new InvalidProgramException(GetType().Name + ": Unknow generator strategy for " + snippetType);
            }

            strategy.Generate(snippet);
        }

        public void GenerateSnippet(IGType snippet, TypeArgs args)
        {
            IGGenerator strategy;
            Type snippetType = snippet.GetType();
            if (!strategies.TryGetValue(snippetType, out strategy))
            {
                throw new InvalidProgramException(GetType().Name + ": Unknow generator strategy for " + snippetType);
            }

            ((IGTypeGenerator) strategy).GenerateArgs(snippet, args);
        }

        #endregion

        protected virtual void RegisterKnownGenerators()
        {
            //structure
            RegisterStrategy<GFile, GFileGenerator>();
            RegisterStrategy<GTextSnippet, GTextSnippetGenerator>();

            //members
            RegisterStrategy<GField, GFieldGenerator>();

            //member decoration
            RegisterStrategy<GParameter, GParameterGenerator>();
            RegisterStrategy<GComment, GCommentGenerator>();
            RegisterStrategy<GXmlDocSnippet, GXmlDocSnippetGenerator>();

            //statements
            RegisterStrategy<GTextStatement, GTextStatementGenerator>();
            RegisterStrategy<GReturnStatement, GReturnStatementGenerator>();
            RegisterStrategy<GThrowStatement, GThrowStatementGenerator>();
            RegisterStrategy<GAssignStatement, GAssignStatementGenerator>();
            RegisterStrategy<GDeclareStatement, GDeclareStatementGenerator>();

            //expressions
            RegisterStrategy<GCallMethodExpression, GCallMethodExpressionGenerator>();
            RegisterStrategy<GCallConstructorExpression, GCallConstructorExpressionGenerator>();
            RegisterStrategy<GTextExpression, GTextExpressionGenerator>();
            RegisterStrategy<GCallIndexerExpression, GCallIndexerExpressionGenerator>();
            RegisterStrategy<GStaticClassExpression, GStaticClassExpressionGenerator>();
            RegisterStrategy<GCastExpression, GCastExpressionGenerator>();

            //types
            RegisterStrategy<GTextType, GTextTypeGenerator>();
        }

        protected void RegisterStrategy<TSnippet, TStrategy>()
            where TSnippet : IGSnippet
            where TStrategy : IGGenerator, new()
        {
            var strategy = new TStrategy();
            strategies[typeof (TSnippet)] = strategy;
            strategy.TypeOfSnippet = typeof (TSnippet);
            if (typeof (IGWriterGenerator).IsAssignableFrom(typeof (TStrategy)))
            {
                strategies[typeof (IGWriterGenerator)] = strategy;
            }
        }

        public void Generate(IGSnippet snippet, TextWriter textWriter)
        {
            Generate(snippet, (IGCodeWriter) new GCodeWriter(textWriter));
        }
    }
}