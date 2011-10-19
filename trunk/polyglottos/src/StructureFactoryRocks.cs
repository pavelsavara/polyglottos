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

namespace polyglottos
{
    public static class _StructureFactoryRocks
    {
        #region classes & namespaces

        public static IGNamespace AddNamespace(this IGNamespaceContainer self, string name,
                                               Action<IGNamespace> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGNamespace>();
            snippet.Name = name;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGClass AddClass(this IGClassContainer self, string name, Action<IGClass> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGClass>();
            snippet.Name = name;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGClass AddClass(this IGClassContainer self, string name, IGType declaration,
                                       Action<IGClass> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGClass>();
            snippet.Name = name;
            snippet.DeclaringType = declaration;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        #endregion

        #region members

        public static IGField AddField(this IGClass self, IGType returnType, string name, Action<IGField> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGField>();
            snippet.Name = name;
            snippet.ReturnType = returnType;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGMethod AddMethod(this IGClass self, IGType returnType, string name, Action<IGMethod> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGMethod>();
            snippet.Name = name;
            snippet.ReturnType = returnType;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        //string type sugar
        public static IGMethod AddMethod(this IGClass self, string returnType, string name, Action<IGMethod> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGTextType>();
            snippet.Name = returnType;
            snippet.IsLocalName = true;
            return self.AddMethod(snippet, name, with);
        }

        public static IGConstructor AddConstructor(this IGClass self, string name, Action<IGConstructor> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGConstructor>();
            snippet.Name = name;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        #endregion

        #region member body

        public static IGParameter AddParameter(this IGMethod self, IGType type, string name,
                                               Action<IGParameter> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGParameter>();
            snippet.Name = name;
            snippet.Type = type;
            self.Parameters.Add(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        //string type sugar
        public static IGParameter AddParameter(this IGMethod self, string type, string name,
                                               Action<IGParameter> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGTextType>();
            snippet.Name = type;
            snippet.IsLocalName = true;
            return self.AddParameter(snippet, name, with);
        }

        public static IGXmlDocSnippet AddXmlDoc(this IGXmlDocContainer self, string line,
                                                Action<IGXmlDocSnippet> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGXmlDocSnippet>();
            snippet.Line = line;
            self.XmlDocSnippets.Add(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        #endregion

        #region other

        public static IGComment AddComment(this IGCommentContainer self, string comment, Action<IGComment> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGComment>();
            snippet.Comment = comment;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGTextSnippet AddTextSnippet(this IGSnippetContainer self, string text,
                                                   Action<IGTextSnippet> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGTextSnippet>();
            snippet.Name = text;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        #endregion
    }
}