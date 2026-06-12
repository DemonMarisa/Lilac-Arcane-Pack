using System;

namespace LAP.Core.Enums
{
    [Flags]
    public enum DrawLayer
    {
        BeforeTiles,
        BeforeNPCs,
        AfterProjectiles,
        BeforeProjectiles,
        BeforePlayer,
        AfterDusts,
        BeforeDusts,
        EndCapture
    }
}
