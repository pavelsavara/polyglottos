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
using System.Collections.ObjectModel;

namespace polyglottos.snippets
{
    public abstract class GContainerSnippetBase : GSnippetBase, IGSnippetContainer
    {
        private readonly Dictionary<string, IGSnippet> names = new Dictionary<string, IGSnippet>();
        protected readonly List<IGSnippet> snippets = new List<IGSnippet>();

        public virtual IGSnippet this[string name]
        {
            get { return names[name]; }
        }

        #region IGSnippetContainer Members

        public virtual void _AddSnippet(IGSnippet snippet)
        {
            snippets.Add(snippet);
            Register(snippet);
        }

        public virtual void _InsertSnippet(int index, IGSnippet snippet)
        {
            snippets.Insert(index, snippet);
            Register(snippet);
        }

        public virtual void _RemoveSnippet(IGSnippet snippet)
        {
            snippets.Remove(snippet);
            IGSnippet nm = snippet;
            IGSnippet candiate;
            if (nm != null && nm.Name != null && names.TryGetValue(nm.Name, out candiate) && candiate == snippet)
            {
                names.Remove(nm.Name);
            }
        }

        public virtual void _RenameSnippet(IGSnippet snippet, string oldName)
        {
            throw new NotImplementedException();
        }

        public void _MoveSnippetTo(IGSnippet snippet, int newIndex)
        {
            throw new NotImplementedException();
        }

        public virtual IList<IGSnippet> Snippets
        {
            get { return new ReadOnlyCollection<IGSnippet>(snippets); }
        }

        #endregion

        protected virtual IGSnippetContainer GetThisAsParent()
        {
            return this;
        }

        protected virtual void Register(IGSnippet snippet)
        {
            if (snippet.ParentSnippet != null)
            {
                throw new ArgumentException("IChildCodeSnippet can't have 2 parents");
            }
            snippet.ParentSnippet = GetThisAsParent();

            if (snippet.Name != null)
            {
                // duplicate ParameterNames are overwritten
                names[snippet.Name] = snippet;
            }
        }
    }
}