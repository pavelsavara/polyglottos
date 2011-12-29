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

namespace polyglottos.generators.csharp
{
    public class GLiteralExpressionGenerator : GExpressionGeneratorBase
    {
        public override void Generate(IGSnippet snippet)
        {
            var expression = (IGLiteralExpression) snippet;
            object value = expression.Value;
            WriteValue(value);
            GenerateChain(expression);
        }

        private void WriteValue(object value)
        {
            if (value == null)
            {
                CodeWriter.Write("null");
                return;
            }
            if (value is string)
            {
                string esc = ((string) value).Replace("\"", "\\\"")
                    .Replace("\\", "\\\\")
                    .Replace("\r", "\\r")
                    .Replace("\n", "\\n")
                    ;
                CodeWriter.Write("\"" + esc + "\"");
                return;
            }
            if (value is bool)
            {
                CodeWriter.Write((bool) value ? "true" : "false");
                return;
            }
            if (value is decimal)
            {
                CodeWriter.Write(value + "m");
                return;
            }
            if (value is DateTime)
            {
                var dt = (DateTime) value;
                CodeWriter.Write(value + "DateTime.ParseExact(\"" + dt.ToUniversalTime() + "\")");
                return;
            }
            CodeWriter.Write(value.ToString());
        }
    }
}