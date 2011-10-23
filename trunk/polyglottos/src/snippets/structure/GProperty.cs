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

namespace polyglottos.snippets
{
    public class GProperty : GMemberSnippetBase, IGProperty
    {
        public GProperty()
        {
            Parameters = new List<IGParameter>();
            IsPublic = true;
        }

        #region IGProperty Members

        public IList<IGParameter> Parameters { get; private set; }
        public IGType ReturnType { get; set; }
        public IGPropertyGetter Getter { get; set; }
        public IGPropertySetter Setter { get; set; }

        #endregion
    }

    public abstract class GPropertyXetter : GContainerSnippetBase, IGPropertyXetter
    {
        private bool isInternal;
        private bool isPrivate;
        private bool isProtected;
        private bool isPublic;

        #region IGPropertyXetter Members

        public bool IsPublic
        {
            get { return isPublic; }
            set
            {
                if (value)
                {
                    IsInternal = false;
                    IsPrivate = false;
                    IsProtected = false;
                }
                isPublic = value;
            }
        }

        public bool IsPrivate
        {
            get { return isPrivate; }
            set
            {
                if (value)
                {
                    IsInternal = false;
                    IsPublic = false;
                    IsProtected = false;
                }
                isPrivate = value;
            }
        }

        public bool IsProtected
        {
            get { return isProtected; }
            set
            {
                if (value)
                {
                    IsPrivate = false;
                    IsPublic = false;
                }
                isProtected = value;
            }
        }

        public bool IsInternal
        {
            get { return isInternal; }
            set
            {
                if (value)
                {
                    IsPublic = false;
                    IsPrivate = false;
                }
                isInternal = value;
            }
        }

        public bool HideBody { get; set; }

        #endregion
    }

    public class GPropertyGetter : GPropertyXetter, IGPropertyGetter
    {
    }

    public class GPropertySetter : GPropertyXetter, IGPropertySetter
    {
    }
}