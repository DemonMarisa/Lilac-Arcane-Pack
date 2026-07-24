
using LAP.Core.Graphics.Lightning;
using LAP.Core.Presets.Content;
using LAP.Core.SystemsLoader;
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
            // LAPContent.AddScreenCaustics(90, Main.MouseWorld, 0.15f, 0.8f, 0.1f, 1f, false, false);
            LightningSetting settning = new LightningSetting(Main.MouseWorld - Vector2.UnitY * 1000, Main.MouseWorld, Color.Gold, 20, 15, 45, 6, 0.5f, 3, 100, 0.6f, 40);
            LightningBuilder.SpawnLightning(settning);
            // ParticlePreset.NewTrailGlowBall(position, velocity * 0.4f, Color.White, 60, 0.2f, false);
            return false;
        }
    }
}
