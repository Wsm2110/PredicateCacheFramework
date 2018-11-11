using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PredicateCacheFramework
{
    [StructLayout(LayoutKind.Explicit)]
    public struct BoolConversion
    {
        [FieldOffset(0)]
        public bool boolValue;
        [FieldOffset(0)]
        public byte byteValue;
    }
}
