
using LAP.Core.Graphics.Lightning;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LAP.Content
{
    public class TestItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.damage = 55;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 42;
            Item.rare = ItemRarityID.Orange;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileID.BloodArrow;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            return null;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            LightningBuilder.SpawnLightning(Main.MouseWorld - Vector2.UnitY * 1000, Main.MouseWorld, Color.White, Color.Gold, 50, 2, 15, 4);
            LightningBuilder.SpawnLightning(Main.MouseWorld - Vector2.UnitY * 1000, Main.MouseWorld, Color.White, Color.Gold, 75, 1, 15, 4);
            LightningBuilder.SpawnLightning(Main.MouseWorld - Vector2.UnitY * 1000, Main.MouseWorld, Color.White, Color.Gold, 75, 1, 15, 4);
            LightningBuilder.SpawnLightning(Main.MouseWorld - Vector2.UnitY * 1000, Main.MouseWorld, Color.White, Color.Gold, 25, 3, 15, 4);
            for (int i = 0; i < 6; i++)
            {
                Vector2 EndPos = -Vector2.UnitY.RotateRandom(MathHelper.PiOver4 * 1.6f) * Main.rand.NextFloat(100f, 150f);
                LightningBuilder.SpawnLightning(Main.MouseWorld, Main.MouseWorld + EndPos, Color.White, Color.Gold, 25, 1, 20, 2);
            }
            return false;
        }
    }
}
