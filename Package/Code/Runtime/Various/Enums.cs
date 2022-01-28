namespace Vheos.Games.Core
{
    using System;

    public enum BuiltInLayer
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,
    }

    [Flags]
    public enum Axes
    {
        X = 1 << 0,
        Y = 1 << 1,
        Z = 1 << 2,
    }
}