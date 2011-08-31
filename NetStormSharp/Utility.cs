using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetStormSharp
{
    public static class Utility
    {
        public static bool CompareBytes(byte[] array1, byte[] array2)
        {
            if (array1 == null || array2 == null)
                return false;

            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }
    }
}
