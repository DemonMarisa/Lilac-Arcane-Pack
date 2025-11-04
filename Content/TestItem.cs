using LAP.Assets.Musics;
using LAP.Content.Configs;
using LAP.Core.MusicEvent;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using System;
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
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 42;
            Item.rare = ItemRarityID.Orange;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileID.BloodArrow;

            Item.LAP().DrawUCASmallIcon = true;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                MusicEventManger.PlayList.Clear();
                MusicEventManger.CurrentEvent = null;
                MusicEventManger.CurrentTrackTimer = TimeSpan.Zero;
                return true;
            }
            MusicEventManger.AddMusicEventEntry(MusicRegister.MainThemeMagnoliaPath, TimeSpan.FromSeconds(141d), () => LAPConfig.Instance.PlayerMagnoliaMainTheme, TimeSpan.FromSeconds(5d));
            return true;
        }
        public override bool? UseItem(Player player)
        {
            return null;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Main.NewText("物品的Shoot");
            return false;
        }
    }
}
