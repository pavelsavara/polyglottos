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

namespace polyglottos
{
    #region regions

    public interface IGRegion : IGRegionContainer
    {
    }

    public interface IGFileRegion : IGRegion, IGNamespaceContainer, IGCommentContainer, IGClassContainer
    {
    }

    public interface IGNamespaceRegion : IGRegion, IGCommentContainer, IGClassContainer
    {
    }

    public interface IGClassRegion : IGRegion, IGMemberContainer, IGCommentContainer
    {
    }

    public interface IGBodyRegion : IGRegion, IGStatementContainer, IGCommentContainer
    {
    }

    #endregion

    #region classes & namespaces

    public interface IGVisibility
    {
        bool IsPublic { get; set; }

        bool IsPrivate { get; set; }

        bool IsInternal { get; set; }

        bool IsProtected { get; set; }
    }

    public interface IGFile : IGNamespaceContainer, IGCommentContainer, IGClassContainer, IGRegionContainer
    {
    }

    public interface IGNamespace : IGCommentContainer, IGRegionContainer, IGClassContainer, IGNamespaceContainer
    {
    }

    public interface IGClass : IGMemberContainer, IGVisibility, IGCommentContainer, IGXmlDocContainer, IGClassContainer, IGAttributeContainer
    {
        IGType DeclaringType { get; set; }
        IGType Extends { get; set; }
        IList<IGType> Implements { get; }
        IList<IGType> GenericArguments { get; }

        bool IsInterface { get; set; }
        bool IsAbstract { get; set; }
        bool IsStatic { get; set; }
        bool IsSealed { get; set; }
        bool IsPartial { get; set; }
    }

    #endregion

    #region members

    public interface IGMember : IGVisibility, IGXmlDocContainer, IGAttributeContainer
    {
        bool IsPartial { get; set; }

        bool IsVirtual { get; set; }

        bool IsOverride { get; set; }

        bool IsSealed { get; set; }

        bool IsNew { get; set; }

        bool IsSynchronized { get; set; }

        bool IsNative { get; set; }

        bool IsAbstract { get; set; }

        bool IsInterface { get; set; }

        bool IsStatic { get; set; }

        bool HideBody { get; set; }

        bool IsOperator { get; set; }

        bool IsImplicit { get; set; }

        bool IsExplicit { get; set; }
    }

    public interface IGMethod : IGMember, IGStatementContainer, IGCommentContainer, IGRegionContainer,
                                IGBodyRegionContainer, IGParameterContainer
    {
        IGType ReturnType { get; set; }
        IGType ExplicitInterface { get; set; }
        IList<IGType> GenericArguments { get; }
    }

    public interface IGConstructor : IGMethod
    {
        IGCallMethod ConstructorBaseCall { get; set; }
    }

    public interface IGField : IGMember, IGExpressionStartContainer
    {
        IGType ReturnType { get; set; }
    }

    public interface IGProperty : IGMethod
    {
        bool IsIndexer { get; set; }
        IGPropertyGetter Getter { get; set; }
        IGPropertySetter Setter { get; set; }
    }


    public interface IGPropertyXetter : IGCommentContainer, IGRegionContainer, IGStatementContainer,
                                        IGBodyRegionContainer, IGVisibility
    {
        bool HideBody { get; set; }
    }

    public interface IGPropertyGetter : IGPropertyXetter
    {
    }

    public interface IGPropertySetter : IGPropertyXetter
    {
    }

    #endregion

    #region member body

    public interface IGAttributeSnippet : IGCallParametersContainer
    {
        IGType Type { get; set; }
    }

    public interface IGXmlDocSnippet : IGSnippet
    {
        string Line { get; set; }
    }


    public interface IGComment : IGSnippet
    {
        string Comment { get; set; }
    }

    public interface IGParameter : IGSnippet
    {
        IGType Type { get; set; }
        bool IsThis { get; set; }
    }

    public interface IGTextSnippet : IGSnippet
    {
        bool Line { get; set; }
    }

    #endregion
}