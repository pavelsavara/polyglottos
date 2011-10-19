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
    public class GClass : GContainerSnippetBase, IGClass
    {
        private bool isInternal;
        private bool isPrivate;
        private bool isProtected;
        private bool isPublic;

        public GClass()
        {
            Implements = new List<IGType>();
            XmlDocSnippets = new List<IGXmlDocSnippet>();
            IsPublic = true;
            GenericArguments = new List<IGType>();
        }

        #region IGClass Members

        public IList<IGType> GenericArguments { get; private set; }
        public IGType DeclaringType { get; set; }

        public bool IsInterface { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsStatic { get; set; }
        public bool IsSealed { get; set; }
        public IGType Extends { get; set; }
        public IList<IGType> Implements { get; private set; }
        public bool IsPartial { get; set; }

        public IList<IGXmlDocSnippet> XmlDocSnippets { get; private set; }

        public bool IsPublic
        {
            get { return isPublic; }
            set
            {
                if (value)
                {
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

        #endregion
    }
}