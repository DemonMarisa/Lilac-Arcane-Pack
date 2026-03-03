namespace LAP.Common.Utilities
{
    public static class ModCrossUtils
    {
        public static bool HasCalamityMod()
        {
            return LAP.Instance.CalamityMod is not null;
        }
    }
}
