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

namespace polyglottos.generators.java
{
    public class GMemberGeneratorBase
    {
        public static void GenerateVisibility(IGVisibility modifiers, IGCodeWriter writer)
        {
            if (modifiers.IsPublic && !(modifiers is IGPropertyGetter) && !(modifiers is IGPropertySetter))
            {
                writer.Write("public ");
            }
            if (modifiers.IsProtected)
            {
                writer.Write("protected ");
            }
            if (modifiers.IsPrivate)
            {
                writer.Write("private ");
            }
        }

        public static void GenerateModifiers(IGMember modifiers, IGCodeWriter writer)
        {
            if (!modifiers.IsInterface)
            {
                if (modifiers.IsStatic)
                {
                    writer.Write("static ");
                }

                GenerateVisibility(modifiers, writer);

                if (modifiers.IsSealed)
                {
                    writer.Write("final ");
                }
                if (modifiers.IsVirtual)
                {
                    writer.Write("virtual ");
                }
                if (modifiers.IsNative)
                {
                    writer.Write("native ");
                }
            }
        }
    }
}