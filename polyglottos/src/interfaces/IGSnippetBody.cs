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
    #region statements

    public interface IGStatement : IGExpressionStartContainer
    {
    }

    public interface IGNoDotChain
    {
    }

    public interface IGUsingStatement : IGStatement, IGStatementContainer
    {
        IGExpressionStartContainer Disposable { get; set; }
    }

    public interface IGCatchStatement : IGStatementContainer
    {
        IGType Type { get; set; }
    }

    public interface IGTryCatchFinallyStatement : IGStatementContainer
    {
        IList<IGCatchStatement> Catches { get; set; }
        IGStatementContainer Finally { get; set; }
    }

    
    public interface IGBlockStatement : IGStatement, IGStatementContainer
    {
    }

    public interface IGDeclareStatement : IGStatement
    {
        IGType Type { get; set; }
    }

    public interface IGTextStatement : IGStatement
    {
    }

    public interface IGAssignStatement : IGStatement
    {
        string PrefixOperator { get; set; }
    }

    public interface IGReturnStatement : IGStatement
    {
    }

    public interface IGThrowStatement : IGStatement
    {
    }

    #endregion

    #region Expressions

    /// <summary>
    /// Accepts expressions. It could be list of parameters or it could be at end of the expression chain.
    /// </summary>
    public interface IGExpressionContainer : IGSnippetContainer
    {
    }

    public interface IGExpressionStartContainer : IGExpressionContainer
    {
    }


    public interface IGExpression : IGExpressionContainer
    {
    }

    public interface IGOperatorExpression : IGExpression, IGExpressionStartContainer
    {
    }

    public interface IGCallMethod : IGCallParametersContainer
    {
    }

    public interface IGCallMethodExpression : IGCallMethod, IGExpression
    {
        IList<IGType> GenericArguments { get; }
    }

    public interface IGTypeofExpression : IGExpression
    {
        IGType Type { get; set; }
    }

    public interface IGDefaultExpression : IGExpression
    {
        IGType Type { get; set; }
    }

    public interface IGCastExpression : IGExpression, IGCallParametersContainer
    {
        IGType Type { get; set; }
    }

    public interface IGCallConstructorExpression : IGExpression, IGCallParametersContainer
    {
        IGType Type { get; set; }
        bool IsArray { get; set; }
    }

    public interface IGStaticClassExpression : IGExpression
    {
        IGType Type { get; set; }
    }

    public interface IGCallIndexerExpression : IGExpression, IGCallParametersContainer
    {
    }

    public interface IGCallFieldExpression : IGExpression
    {
    }

    public interface IGTextExpression : IGExpression
    {
    }

    public interface IGLiteralExpression : IGExpression
    {
        object Value { get; set; }
    }

    #endregion

    #region Call parameters

    public interface IGCallParameter : IGExpressionStartContainer
    {
        bool IsOut { get; set; }
        bool IsRef { get; set; }
    }

    public interface IGCallParameters : IGSnippetContainer
    {
    }

    public interface IGCallParametersContainer : IGSnippet
    {
        IGCallParameters Parameters { get; }
    }

    #endregion
}