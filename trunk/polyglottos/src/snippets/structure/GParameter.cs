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

namespace polyglottos.snippets
{
    public class GParameter : GSnippetBase, IGParameter
    {
        public GParameter()
        {
            Default = new GCallParams();
        }

        public bool IsThis { get; set; }

        public IGType Type { get; set; }

        public IGCallParameters Default { get; private set; }

        public override IGProject Project
        {
            get { return base.Project; }
            set
            {
                Default.Project = value;
                base.Project = value;
            }
        }

        public override string ToString()
        {
            return "(" + GetType().Name + ") " + (Type == null ? "" : Type + " ") + Name;
        }
    }
}