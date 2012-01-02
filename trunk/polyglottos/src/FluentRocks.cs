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

namespace polyglottos
{
    public static class _FluentRocks
    {
        public static TOut If<TIn, TOut>(this TIn self, bool condition, Func<TIn, TOut> isTrue,
                                         Func<TIn, TOut> isFalse = null, Action<TOut> with = null)
            where TIn : IGSnippet
            where TOut : IGSnippet
        {
            TOut result = condition
                              ? isTrue(self)
                              : isFalse != null
                                    ? isFalse(self)
                                    : default(TOut);
            if (with != null) with(result);
            return result;
        }

        public static TOut If<TIn, TOut>(this TIn self, bool condition, Func<TOut> isTrue, Func<TOut> isFalse = null,
                                         Action<TOut> with = null)
            where TIn : IGSnippet
            where TOut : IGSnippet
        {
            TOut result = condition
                              ? isTrue()
                              : isFalse != null
                                    ? isFalse()
                                    : default(TOut);
            if (with != null) with(result);
            return result;
        }


        public static TOut Opt<TIn, TOut>(this TIn self, bool condition, Func<TIn, TOut> isTrue)
            where TIn : TOut, IGSnippet
            where TOut : IGSnippet
        {
            return condition ? isTrue(self) : self;
        }

        public static T With<T>(this T self, Action<T> with = null)
        {
            if (with != null) with(self);
            return self;
        }
    }
}