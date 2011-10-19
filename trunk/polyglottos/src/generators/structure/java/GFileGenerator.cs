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
using System.IO;

namespace polyglottos.generators.java
{
    public class GFileGenerator : generators.GFileGenerator
    {
        public override IGCodeWriter CreateWriter(IGFile snippet)
        {
            if (File.Exists(snippet.Name))
            {
                Context.Add("OldFileContent", File.ReadAllText(snippet.Name));
            }
            return base.CreateWriter(snippet);
        }

        public static bool GetOldExtension(IDictionary<string, object> context, string starttag, ref string extension,
                                           string endtag)
        {
            var oldFileContent = context["OldFileContent"] as string;
            if (oldFileContent != null)
            {
                int start;
                int end;
                if ((start = oldFileContent.IndexOf(starttag)) != -1
                    && (end = oldFileContent.IndexOf(endtag, start)) != -1)
                {
                    int cutStart = start + starttag.Length + 2;
                    int cutLen = end - cutStart;
                    string old = oldFileContent.Substring(cutStart, cutLen);
                    if (!old.Trim().Equals(extension.Trim()))
                    {
                        int indexOf = old.LastIndexOf("\r\n");
                        if (indexOf != -1)
                        {
                            old = old.Substring(0, indexOf);
                        }
                        extension = old;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}