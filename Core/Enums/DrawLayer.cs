using System;

namespace LAP.Core.Enums
{
    [Flags]
    public enum DrawLayer
    {
        BeforePlayer,
        AfterPlayer,
        BeforeTiles,
        BeforeNPCs,
        AfterProjectiles,
        BeforeProjectiles,
        AfterDusts,
        BeforeDusts,
        EndCapture
    }
}
