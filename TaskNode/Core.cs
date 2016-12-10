using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskNode
{
    class Core
    {
        public static string ClearEscape( string str )
        {
            int length = str.Length;
            char[] oldChars = new char[length];
            //s.getChars(0, length, oldChars, 0);
            oldChars = str.ToCharArray();
            int newLen = 0;
            for (int j = 0; j < length; j++)
            {
                char ch = oldChars[j];
                if (ch >= ' ')
                {
                    oldChars[newLen] = ch;
                    newLen++;
                }
            }
            return new String(oldChars, 0, newLen);
        }

 

    }
}
