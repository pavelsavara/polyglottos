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

using System.Collections.Generic;
using System.Linq;

namespace polyglottos
{
    public static class _QueryRocks
    {
        public static IEnumerable<TChild> DeepSearch<TContainer, TChild>(TContainer container)
            where TChild : IGSnippet
            where TContainer : IGSnippetContainer
        {
            return container.Snippets.SelectMany(s =>
                                                     {
                                                         bool isContainer =
                                                             typeof (TContainer).IsAssignableFrom(s.GetType());
                                                         bool isChild = typeof (TChild).IsAssignableFrom(s.GetType());

                                                         return isChild
                                                                    ? (isContainer
                                                                           ? (IEnumerable<TChild>)
                                                                             new List<TChild>(
                                                                                 DeepSearch<TContainer, TChild>(
                                                                                     ((TContainer) s))) {(TChild) s}
                                                                           : new[] {(TChild) s})
                                                                    : (isContainer
                                                                           ? DeepSearch<TContainer, TChild>(
                                                                               ((TContainer) s))
                                                                           : new TChild[] {});
                                                     });
        }

        public static IEnumerable<TChild> ShalowSearch<TContainer, TChild>(TContainer container)
            where TChild : IGSnippet
            where TContainer : IGSnippetContainer
        {
            return container.Snippets.OfType<TChild>();
        }

        public static TChild ShalowSearch<TContainer, TChild>(TContainer container, string name)
            where TChild : IGSnippet
            where TContainer : IGSnippetContainer
        {
            return ShalowSearch<TContainer, TChild>(container).Where(s => s.Name.Contains(name)).SingleOrDefault();
        }

        public static TChild DeepSearch<TContainer, TChild>(TContainer container, string name)
            where TChild : IGSnippet
            where TContainer : IGSnippetContainer
        {
            return DeepSearch<TContainer, TChild>(container).Where(s => s.Name.Contains(name)).SingleOrDefault();
        }

        public static IEnumerable<IGFile> GetFiles(this IGFileContainer self)
        {
            return ShalowSearch<IGFileContainer, IGFile>(self);
        }

        public static IGFile GetFile(this IGFileContainer self, string name)
        {
            return ShalowSearch<IGFileContainer, IGFile>(self, name);
        }

        public static IEnumerable<IGRegion> GetAllRegions(this IGRegionContainer self)
        {
            return DeepSearch<IGRegionContainer, IGRegion>(self);
        }

        public static IGRegion GetRegion(this IGRegionContainer self, string name)
        {
            return DeepSearch<IGRegionContainer, IGRegion>(self, name);
        }

        public static IEnumerable<IGNamespace> GetAllNamespaces(this IGNamespaceContainer self)
        {
            return DeepSearch<IGNamespaceContainer, IGNamespace>(self);
        }

        public static IEnumerable<IGNamespace> GetNamespaces(this IGNamespaceContainer self)
        {
            return ShalowSearch<IGNamespaceContainer, IGNamespace>(self);
        }

        public static IGNamespace GetNamespace(this IGNamespaceContainer self, string name)
        {
            return DeepSearch<IGNamespaceContainer, IGNamespace>(self, name);
        }

        public static IEnumerable<IGComment> GetComments(this IGCommentContainer self)
        {
            return ShalowSearch<IGCommentContainer, IGComment>(self);
        }

        public static IEnumerable<IGComment> GetAllComments(this IGCommentContainer self)
        {
            return DeepSearch<IGCommentContainer, IGComment>(self);
        }

        public static IEnumerable<IGClass> GetAllComments(this IGClassContainer self)
        {
            return DeepSearch<IGClassContainer, IGClass>(self);
        }

        public static IEnumerable<IGClass> GetClasses(this IGClassContainer self)
        {
            return ShalowSearch<IGClassContainer, IGClass>(self);
        }

        public static IEnumerable<IGClass> GetAllClasses(this IGClassContainer self)
        {
            return DeepSearch<IGClassContainer, IGClass>(self);
        }

        public static IGClass GetClass(this IGClassContainer self, string name)
        {
            return DeepSearch<IGClassContainer, IGClass>(self, name);
        }

        public static IEnumerable<IGMember> GetMembers(this IGMemberContainer self)
        {
            return ShalowSearch<IGMemberContainer, IGMember>(self);
        }

        public static IEnumerable<IGMethod> GetMethods(this IGMemberContainer self)
        {
            return ShalowSearch<IGMemberContainer, IGMethod>(self);
        }

        public static IGMethod GetMethod(this IGMemberContainer self, string name)
        {
            return ShalowSearch<IGMemberContainer, IGMethod>(self, name);
        }

        public static IEnumerable<IGConstructor> GetConstructors(this IGMemberContainer self)
        {
            return ShalowSearch<IGMemberContainer, IGConstructor>(self);
        }

        public static IEnumerable<IGField> GetFields(this IGMemberContainer self)
        {
            return ShalowSearch<IGMemberContainer, IGField>(self);
        }
    }
}