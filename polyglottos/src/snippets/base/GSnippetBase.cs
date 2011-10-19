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

namespace polyglottos.snippets
{
    public abstract class GSnippetBase : IGSnippet
    {
        private string name;

        public virtual object CustomData { get; set; }

        public virtual IGProject Project { get; set; }

        public virtual IGSnippetContainer ParentSnippet { get; set; }

        public virtual string Name
        {
            get { return name; }
            set
            {
                string old = name;
                name = value;
                if (name != null && ParentSnippet != null)
                {
                    ParentSnippet._RenameSnippet(this, old);
                }
            }
        }

        public override string ToString()
        {
            return "(" + GetType().Name + ") " + Name;
        }
    }
}