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

namespace polyglottos.csharp
{
    public static class _BodyFactoryRocks
    {
        #region statements

        //sugar
        public static IGStatement ThrowNotImplemented(this IGStatementContainer self)
        {
            return self.ThrowNew(GTypeClr.NotImplementedException);
        }

        public static IGUsingStatement Using(this IGStatementContainer self, string name,
                                             Action<IGExpressionStartContainer> disposable = null,
                                             Action<IGUsingStatement> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGUsingStatement>();
            snippet.Name = name;
            self._AddSnippet(snippet);
            if (disposable != null) disposable(snippet.Disposable);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGUsingStatement Using(this IGStatementContainer self,
                                             Action<IGExpressionStartContainer> disposable = null,
                                             Action<IGUsingStatement> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGUsingStatement>();
            self._AddSnippet(snippet);
            if (disposable != null) disposable(snippet.Disposable);
            if (with != null) with(snippet);
            return snippet;
        }

        //sugar
        public static IGUsingStatement UsingNew(this IGStatementContainer self, IGType type,
                                                Action<IGCallConstructorExpression> disposable = null,
                                                Action<IGUsingStatement> with = null)
        {
            return self.Using(d => d.New(type, disposable), with);
        }

        //sugar
        public static IGUsingStatement UsingNew(this IGStatementContainer self, IGType type, string name,
                                                Action<IGCallConstructorExpression> disposable = null,
                                                Action<IGUsingStatement> with = null)
        {
            return self.Using(name, d => d.New(type, disposable), with);
        }

        // string type sugar
        public static IGUsingStatement UsingNew(this IGStatementContainer self, string type, string name,
                                                Action<IGCallConstructorExpression> disposable = null,
                                                Action<IGUsingStatement> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGTextType>();
            snippet.Name = type;
            snippet.IsLocalName = true;
            return self.UsingNew(snippet, name, disposable, with);
        }

        // string type sugar
        public static IGUsingStatement UsingNew(this IGStatementContainer self, string type,
                                                Action<IGCallConstructorExpression> disposable = null,
                                                Action<IGUsingStatement> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGTextType>();
            snippet.Name = type;
            snippet.IsLocalName = true;
            return self.UsingNew(snippet, disposable, with);
        }

        #endregion

        #region generics

        public static void AddGenericArgument(this IGMethod self, IGType typeArgument)
        {
            self.GenericArguments.Add(typeArgument);
        }

        public static void AddGenericArgument(this IGClass self, IGType typeArgument)
        {
            self.GenericArguments.Add(typeArgument);
        }

        #endregion
    }
}