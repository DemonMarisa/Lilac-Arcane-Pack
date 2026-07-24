using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria.ModLoader;

namespace LAP.Common.CalamityModCross
{
    public class CalTileID : ModSystem
    {
        public static int CosmicAnvilID;// 星宇砧
        public static int DraedonsForgeID;// 星宇砧
        public override void OnModLoad()
        {
            if (LAP.Instance.CalamityMod is not null)
            {
                GetCalamityTileID();
            }
        }
        [JITWhenModsEnabled("CalamityMod")]
        public static void GetCalamityTileID()
        {
            CosmicAnvilID = TileType<CosmicAnvil>();
            DraedonsForgeID = TileType<DraedonsForge>();
        }
    }
}
