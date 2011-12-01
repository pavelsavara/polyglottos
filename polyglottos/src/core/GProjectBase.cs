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
using System.Linq;
using polyglottos.snippets;
using polyglottos.utils;

namespace polyglottos
{
    public abstract class GProjectBase : GContainerSnippetBase, IGProject
    {
        private readonly Dictionary<Type, Type> snipetMapping = new Dictionary<Type, Type>();

        protected GProjectBase()
        {
            RegisterKnownSnippets();
        }

        #region IGProject Members

        public IList<IGFile> Files
        {
            get { return Snippets.OfType<IGFile>().ToList(); }
        }

        public virtual T CreateSnippet<T>() where T : class, IGSnippet
        {
            T result;

            Type type = typeof (T);
            Type resultType;
            if (!snipetMapping.TryGetValue(type, out resultType))
            {
                if (type.IsInterface)
                {
                    throw new InvalidProgramException("Unknown implementation of " + type.FullName);
                }
                result = (T) Activator.CreateInstance(type);
            }
            else
            {
                result = (T) Activator.CreateInstance(resultType);
            }

            result.Project = this;

            return result;
        }

        public IGFile AddFile(string fileName, Action<IGFile> with = null)
        {
            var file = CreateSnippet<IGFile>();
            file.Name = fileName;
            _AddSnippet(file);
            if (with != null) with(file);
            return file;
        }

        public string GenerateSnippet(IGSnippet snippet, IGContext context = null)
        {
            IGCodeGenerator generator = CreateGenerator(context);
            using (var stringWriter = new StringWriter())
            {
                using (var codeWriter = new GCodeWriter(stringWriter))
                {
                    generator.Generate(snippet, codeWriter);
                    return stringWriter.ToString();
                }
            }
        }

        public void GenerateSnippet(IGSnippet snippet, IGCodeWriter codeWriter, IGContext context = null)
        {
            IGCodeGenerator generator = CreateGenerator(context);
            generator.Generate(snippet, codeWriter);
        }

        public void GenerateSnippet(IGSnippet snippet, TextWriter writer, IGContext context = null)
        {
            IGCodeGenerator generator = CreateGenerator(context);
            using (var codeWriter = new GCodeWriter(writer))
            {
                generator.Generate(snippet, codeWriter);
            }
        }

        public void GenerateFile(IGFile file, IGContext context = null)
        {
            IGCodeGenerator generator = CreateGenerator(context);
            generator.Generate(file);
        }

        public void GenerateAllFiles(IGContext context = null)
        {
            IGCodeGenerator generator = CreateGenerator(context);
            foreach (IGFile file in snippets.OfType<IGFile>())
            {
                generator.Generate(file);
            }
        }

        #endregion

        /// <summary>
        /// override this and register your own snippets, or replace the snippet implementation with your own.
        /// </summary>
        protected virtual void RegisterKnownSnippets()
        {
            //structure
            RegisterSnippet<IGFile, GFile>();
            RegisterSnippet<IGNamespace, GNamespace>();
            RegisterSnippet<IGClass, GClass>();
            RegisterSnippet<IGComment, GComment>();
            RegisterSnippet<IGTextSnippet, GTextSnippet>();
            RegisterSnippet<IGAttributeSnippet, GAttributeSnippet>();

            //members
            RegisterSnippet<IGMethod, GMethod>();
            RegisterSnippet<IGConstructor, GConstructor>();
            RegisterSnippet<IGField, GField>();

            //members decoration
            RegisterSnippet<IGParameter, GParameter>();
            RegisterSnippet<IGXmlDocSnippet, GXmlDocSnippet>();

            //statements
            RegisterSnippet<IGTextStatement, GTextStatement>();
            RegisterSnippet<IGAssignStatement, GAssignStatement>();
            RegisterSnippet<IGReturnStatement, GReturnStatement>();
            RegisterSnippet<IGThrowStatement, GThrowStatement>();

            //expressions
            RegisterSnippet<IGCallMethodExpression, GCallMethodExpression>();
            RegisterSnippet<IGCallFieldExpression, GCallFieldExpression>();
            RegisterSnippet<IGCallConstructorExpression, GCallConstructorExpression>();
            RegisterSnippet<IGCallIndexerExpression, GCallIndexerExpression>();
            RegisterSnippet<IGStaticClassExpression, GStaticClassExpression>();
            RegisterSnippet<IGLiteralExpression, GLiteralExpression>();
            RegisterSnippet<IGTypeofExpression, GTypeofExpression>();
            RegisterSnippet<IGCastExpression, GCastExpression>();
            RegisterSnippet<IGDeclareStatement, GDeclareStatement>();
            RegisterSnippet<IGTextExpression, GTextExpression>();

            //types
            RegisterSnippet<IGTextType, GTextType>();
        }

        /// <summary>
        /// Override this and implement generator factory
        /// </summary>
        public abstract IGCodeGenerator CreateGenerator(IGContext context = null);

        protected virtual void RegisterSnippet<TTo>()
        {
            snipetMapping[typeof (TTo)] = typeof (TTo);
        }

        protected virtual void RegisterSnippet<TFrom, TTo>()
            where TFrom : class, IGSnippet
            where TTo : class, IGSnippet, TFrom, new()
        {
            snipetMapping[typeof (TTo)] = typeof (TTo);
            snipetMapping[typeof (TFrom)] = typeof (TTo);
        }
    }
}