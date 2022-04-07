namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Vheos.Tools.Extensions.General;

    public enum PressedDeselectBehavior
    {

        ReleaseAndDeselect,
        KeepPressedButDeselect,
        KeepPressedAndSelected,
    }

    public enum BuiltInLayer
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,
    }

    public enum ExpandableState
    {
        Expanding,
        Expanded,
        Collapsing,
        Collapsed,
    }

    public enum PredefinedTeam
    {
        None,
        Allies,
        Enemies,
    }

    [Flags]
    public enum Axes
    {
        X = 1 << 0,
        Y = 1 << 1,
        Z = 1 << 2,

        XY = X | Y,
        XZ = X | Z,
        YX = XY,
        YZ = Y | Z,
        ZX = XZ,
        ZY = YZ,

        XYZ = X | Y | Z,
        XZY = XYZ,
        YXZ = XYZ,
        YZX = XYZ,
        ZXY = XYZ,
        ZYX = XYZ,
    }
}