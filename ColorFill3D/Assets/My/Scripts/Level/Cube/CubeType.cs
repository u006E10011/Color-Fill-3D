using System;

namespace Project
{
    [Flags]
    public enum CubeType : byte
    {
        Default = 1 << 0,
        Damaging = 1 << 1,
        Destroying = 1 << 2,
        Moveble = 1 << 3
    }
}