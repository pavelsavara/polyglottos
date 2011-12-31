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
using polyglottos.snippets;

namespace polyglottos
{
    public static class _BodyFactoryRocks
    {
        #region statements

        public static IGStatement AddTextStatement(this IGStatementContainer self, string statement)
        {
            var snippet = self.Project.CreateSnippet<GTextStatement>();
            snippet.Name = statement;
            self._AddSnippet(snippet);
            return snippet;
        }

        public static IGStatement ThrowNew(this IGStatementContainer self, IGType exception)
        {
            var thr = self.Project.CreateSnippet<IGThrowStatement>();
            thr.New(exception);
            self._AddSnippet(thr);
            return thr;
        }

        public static IGStatement Return(this IGStatementContainer self, Action<IGStatement> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGReturnStatement>();
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGStatement Assign(this IGStatementContainer self, string variable,
                                         Action<IGStatement> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGAssignStatement>();
            snippet.Name = variable;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGStatement Declare(this IGStatementContainer self, IGType type, string variable,
                                          Action<IGStatement> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGDeclareStatement>();
            snippet.Name = variable;
            snippet.Type = type;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        //string type sugar
        public static IGStatement Declare(this IGStatementContainer self, string type, string variable,
                                          Action<IGStatement> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGTextType>();
            snippet.Name = type;
            snippet.IsLocalName = true;
            return self.Declare(snippet, variable, with);
        }

        public static IGCallMethod CallConstructorBase(this IGConstructor self, Action<IGCallMethod> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGCallMethodExpression>();
            self.ConstructorBaseCall = snippet;
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGBlockStatement BlockStatement(this IGStatementContainer self,
                                             Action<IGBlockStatement> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGBlockStatement>();
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        #endregion

        #region expressions

        public static IGExpression Value(this IGExpressionStartContainer self, object value,
                                         Action<IGExpression> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGLiteralExpression>();
            snippet.Value = value;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGExpression TextExpression(this IGExpressionContainer self, string variableName,
                                                  Action<IGExpression> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGTextExpression>();
            snippet.Name = variableName;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGExpression Static(this IGExpressionStartContainer self, IGType type,
                                          Action<IGExpression> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGStaticClassExpression>();
            snippet.Type = type;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        //string type sugar
        public static IGExpression Static(this IGExpressionStartContainer self, string type,
                                          Action<IGExpression> result = null)
        {
            var snippet = self.Project.CreateSnippet<IGTextType>();
            snippet.Name = type;
            snippet.IsLocalName = true;
            return self.Static(snippet, result);
        }

        public static IGCallConstructorExpression New(this IGExpressionStartContainer self, IGType type,
                                                      Action<IGCallConstructorExpression> result = null)
        {
            var snippet = self.Project.CreateSnippet<IGCallConstructorExpression>();
            snippet.Type = type;
            self._AddSnippet(snippet);
            if (result != null) result(snippet);
            return snippet;
        }

        //string type sugar
        public static IGCallConstructorExpression New(this IGExpressionStartContainer self, string type,
                                          Action<IGCallConstructorExpression> result = null)
        {
            var snippet = self.Project.CreateSnippet<IGTextType>();
            snippet.Name = type;
            snippet.IsLocalName = true;
            return self.New(snippet, result);
        }

        
        public static IGCastExpression Cast(this IGExpressionStartContainer self, IGType type,
                                            Action<IGCastExpression> result = null)
        {
            var snippet = self.Project.CreateSnippet<IGCastExpression>();
            snippet.Type = type;
            self._AddSnippet(snippet);
            if (result != null) result(snippet);
            return snippet;
        }


        public static IGTypeofExpression TypeOf(this IGExpressionStartContainer self, IGType type,
                                                Action<IGTypeofExpression> result = null)
        {
            var snippet = self.Project.CreateSnippet<IGTypeofExpression>();
            snippet.Type = type;
            self._AddSnippet(snippet);
            if (result != null) result(snippet);
            return snippet;
        }

        // string type sugar
        public static IGTypeofExpression TypeOf(this IGExpressionStartContainer self, string type,
                                                Action<IGTypeofExpression> result = null)
        {
            var snippet = self.Project.CreateSnippet<IGTextType>();
            snippet.Name = type;
            snippet.IsLocalName = true;
            return self.TypeOf(snippet, result);
        }

        public static IGDefaultExpression Default(this IGExpressionStartContainer self, IGType type,
                                                Action<IGDefaultExpression> result = null)
        {
            var snippet = self.Project.CreateSnippet<IGDefaultExpression>();
            snippet.Type = type;
            self._AddSnippet(snippet);
            if (result != null) result(snippet);
            return snippet;
        }

        // string type sugar
        public static IGDefaultExpression Default(this IGExpressionStartContainer self, string type,
                                                Action<IGDefaultExpression> result = null)
        {
            var snippet = self.Project.CreateSnippet<IGTextType>();
            snippet.Name = type;
            snippet.IsLocalName = true;
            return self.Default(snippet, result);
        }

        public static IGCallMethodExpression Call(this IGExpressionContainer self, string methodName,
                                                  Action<IGCallMethodExpression> result = null)
        {
            var snippet = self.Project.CreateSnippet<IGCallMethodExpression>();
            snippet.Name = methodName;
            self._AddSnippet(snippet);
            if (result != null) result(snippet);
            return snippet;
        }

        public static IGCallFieldExpression CallField(this IGExpressionContainer self, string fieldName,
                                                  Action<IGCallFieldExpression> result = null)
        {
            var snippet = self.Project.CreateSnippet<IGCallFieldExpression>();
            snippet.Name = fieldName;
            self._AddSnippet(snippet);
            if (result != null) result(snippet);
            return snippet;
        }

        public static IGCallMethodExpression CallStatic(this IGExpressionStartContainer self, IGType type,
                                                        string methodName, Action<IGCallMethodExpression> result = null)
        {
            return self.Static(type).Call(methodName, result);
        }

        // string type sugar
        public static IGCallMethodExpression CallStatic(this IGExpressionStartContainer self, string type,
                                                        string methodName, Action<IGCallMethodExpression> result = null)
        {
            return self.Static(type).Call(methodName, result);
        }

        public static IGCallIndexerExpression Indexer(this IGExpression self,
                                                      Action<IGCallIndexerExpression> result = null)
        {
            var snippet = self.Project.CreateSnippet<IGCallIndexerExpression>();
            self._AddSnippet(snippet);
            if (result != null) result(snippet);
            return snippet;
        }

        #endregion

        #region Parameters

        public static IGExpression DefaultValue(this IGParameter self, object value,
                                         Action<IGExpression> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGLiteralExpression>();
            snippet.Value = value;
            self.Default._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGCallParameters AddParameter(this IGCallParametersContainer self)
        {
            return self.Parameters;
        }

        //sugar
        public static IGExpression AddParameterValue(this IGCallParametersContainer self, object value,
                                                     Action<IGExpression> with = null)
        {
            return self.Parameters.Value(value, with);
        }

        //sugar
        public static IGExpression AddParameterNull(this IGCallParametersContainer self,
                                                    Action<IGExpression> with = null)
        {
            return self.Parameters.Value(null, with);
        }

        //obsolete sugar
        public static IGExpression AddParameterVariable(this IGCallParametersContainer self, string variable,
                                                        Action<IGExpression> with = null)
        {
            return self.Parameters.TextExpression(variable, with);
        }

        #endregion

        #region Queries

        public static IList<IGMethod> GetMethods(this IGClass self)
        {
            return self.Snippets.Where(x => x is IGMethod).Cast<IGMethod>().ToList();
        }

        #endregion
    }
}