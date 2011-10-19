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
    public static class _StructureFactoryRocks
    {
        #region regions

        public static IGFileRegion AddRegion(this IGFile self, string name, Action<IGFileRegion> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGFileRegion>();
            snippet.Name = name;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGFileRegion AddRegion(this IGFileRegion self, string name, Action<IGFileRegion> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGFileRegion>();
            snippet.Name = name;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGNamespaceRegion AddRegion(this IGNamespace self, string name,
                                                  Action<IGNamespaceRegion> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGNamespaceRegion>();
            snippet.Name = name;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGNamespaceRegion AddRegion(this IGNamespaceRegion self, string name,
                                                  Action<IGNamespaceRegion> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGNamespaceRegion>();
            snippet.Name = name;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGClassRegion AddRegion(this IGClass self, string name, Action<IGClassRegion> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGClassRegion>();
            snippet.Name = name;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGClassRegion AddRegion(this IGClassRegion self, string name, Action<IGClassRegion> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGClassRegion>();
            snippet.Name = name;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGBodyRegion AddRegion(this IGBodyRegion self, string name, Action<IGBodyRegion> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGBodyRegion>();
            snippet.Name = name;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGBodyRegion AddRegion(this IGBodyRegionContainer self, string name,
                                             Action<IGBodyRegion> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGBodyRegion>();
            snippet.Name = name;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        #endregion

        #region members

        public static IGProperty AddProperty(this IGClass self, IGType returnType, string name,
                                             Action<IGProperty> with = null)
        {
            var snippet = self.Project.CreateSnippet<IGProperty>();
            snippet.Name = name;
            snippet.ReturnType = returnType;
            self._AddSnippet(snippet);
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGPropertyGetter AddGetter(this IGProperty self, Action<IGPropertyGetter> with = null)
        {
            if (self.Getter != null)
            {
                return self.Getter;
            }
            var snippet = self.Project.CreateSnippet<IGPropertyGetter>();
            self.Getter = snippet;
            if (with != null) with(snippet);
            return snippet;
        }

        public static IGPropertySetter AddSetter(this IGProperty self, Action<IGPropertySetter> with = null)
        {
            if (self.Setter != null)
            {
                return self.Setter;
            }
            var snippet = self.Project.CreateSnippet<IGPropertySetter>();
            self.Setter = snippet;
            if (with != null) with(snippet);
            return snippet;
        }

        #endregion
    }
}