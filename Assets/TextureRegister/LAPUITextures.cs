using Terraria.ModLoader;

namespace LAP.Assets.TextureRegister
{
    public partial class LAPTextureRegister : ModSystem
    {
        public static string UIPath => "LAP/Assets/TextureRegister/UI";
        public static Tex2DWithPath CDBG_Edge { get; private set; }
        public static Tex2DWithPath CDBG_Middle { get; private set; }
        public static Tex2DWithPath SingleCD { get; private set; }
        public static Tex2DWithPath SingleCD_Bloom { get; private set; }
        public static Tex2DWithPath SingleCD_OverLayer { get; private set; }
        public static Tex2DWithPath BottomPattern { get; private set; }
        public static Tex2DWithPath FocusUIEdge { get; private set; }
        public static Tex2DWithPath FocusUIMiddle { get; private set; }
        public static Tex2DWithPath TopPattern { get; private set; }
        public static Tex2DWithPath EL_Bloom { get; private set; }
        public static Tex2DWithPath EL_Flower { get; private set; }
        public static Tex2DWithPath EL_HoverEdge { get; private set; }
        public static Tex2DWithPath EL_Net { get; private set; }
        public static Tex2DWithPath EL_Arrow { get; private set; }
        public static void LoadUI()
        {
            CDBG_Edge = new Tex2DWithPath($"{UIPath}/CDs/CDBG_Edge");
            CDBG_Middle = new Tex2DWithPath($"{UIPath}/CDs/CDBG_Middle");
            SingleCD = new Tex2DWithPath($"{UIPath}/CDs/SingleCD");
            SingleCD_Bloom = new Tex2DWithPath($"{UIPath}/CDs/SingleCD_Bloom");
            SingleCD_OverLayer = new Tex2DWithPath($"{UIPath}/CDs/SingleCD_OverLayer");
            BottomPattern = new Tex2DWithPath($"{UIPath}/Focus/BottomPattern");
            FocusUIEdge = new Tex2DWithPath($"{UIPath}/Focus/FocusUIEdge");
            FocusUIMiddle = new Tex2DWithPath($"{UIPath}/Focus/FocusUIMiddle");
            TopPattern = new Tex2DWithPath($"{UIPath}/Focus/TopPattern");
            EL_Bloom = new Tex2DWithPath($"{UIPath}/EnderLilies/Bloom");
            EL_Flower = new Tex2DWithPath($"{UIPath}/EnderLilies/Flower");
            EL_HoverEdge = new Tex2DWithPath($"{UIPath}/EnderLilies/HoverEdge");
            EL_Net = new Tex2DWithPath($"{UIPath}/EnderLilies/Net");
            EL_Arrow = new Tex2DWithPath($"{UIPath}/EnderLilies/Arrow");
        }
        public static void UnloadUI()
        {
            CDBG_Edge = null;
            CDBG_Middle = null;
            SingleCD = null;
            SingleCD_Bloom = null;
            SingleCD_OverLayer = null;
            BottomPattern = null;
            FocusUIEdge = null;
            FocusUIMiddle = null;
            TopPattern = null;
            EL_Bloom = null;
            EL_Flower = null;
            EL_HoverEdge = null;
            EL_Net = null;
            EL_Arrow = null;
        }
    }
}
