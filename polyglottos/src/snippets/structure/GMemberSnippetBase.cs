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
    public abstract class GMemberSnippetBase : GContainerSnippetBase, IGMember
    {
        private bool isInternal;
        private bool isPrivate;
        private bool isProtected;
        private bool isPublic;

        protected GMemberSnippetBase()
        {
            XmlDocSnippets = new List<IGXmlDocSnippet>();
            AttributeSnippets=new List<IGAttributeSnippet>();
        }

        #region IGMember Members

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

        public bool IsPartial { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsOverride { get; set; }
        public bool IsSealed { get; set; }
        public bool IsNew { get; set; }
        public bool IsNative { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsInterface { get; set; }
        public bool IsStatic { get; set; }
        public bool HideBody { get; set; }
        public bool IsOperator { get; set; }
        public bool IsImplicit { get; set; }
        public bool IsExplicit { get; set; }
        public bool IsSynchronized { get; set; }

        public IList<IGXmlDocSnippet> XmlDocSnippets { get; private set; }

        public IList<IGAttributeSnippet> AttributeSnippets { get; private set; }
        #endregion
    }
}