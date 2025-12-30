using System;

namespace LAP.Core.Enums
{
    [Flags]
    public enum DrawLayer
    {
        BeforeTiles,
        BeforeNPCs,
        BeforeProjectiles,
        BeforePlayer,
        BeforeDusts,
        AfterDusts,
    }
}
