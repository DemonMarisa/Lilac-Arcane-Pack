using LAP.Core.Keybind;
using LAP.Core.LAPSource;
using LAP.Core.SystemsLoader;
using LAP.Core.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace LAP.Core.BaseClass
{
    public abstract class BaseSkillWeapon : ModItem, ILocalizedModType
    {
        public override void HoldItem(Player player)
        {
            UpdateHoldItem(player);
            // 只在本地调用
            if (player.whoAmI != Main.myPlayer)
                return;
            if (player.itemTime > 0)
                return;
            if (Item.LAP().SkillShoot == -1)
                return;
            if (LAPKeybind.WeaponSkillHotKey.JustPressed && !Main.blockMouse)
            {
                if (Main.playerInventory)
                {
                    if (Main.hoverItemName != "")
                        return;
                }
                if (!CanUseWeaponSkill(player))
                    return;
                EntitySource_ItemUse_WeaponSkill soure = new (player, Item);
                Vector2 position = player.Center;
                Vector2 velocity = player.GetPlayerToMouseVector2() * Item.LAP().SkillShootSpeed;
                int damage = player.GetWeaponDamage(Item);
                int type = Item.LAP().SkillShoot;
                float knockback = player.GetWeaponKnockback(Item);
                if (Item.LAP().WeaponSkillRealFocusCost != 0)
                {
                    if (PreCheckFocus(player, Item.LAP().WeaponSkillRealFocusCost))
                    {
                        if (!player.CheckFocus(Item.LAP().WeaponSkillRealFocusCost, false))
                            return;
                    }
                }
                if (Item.LAP().WeaponSkillRealManaCost != 0)
                {
                    if (PreCheckMana(player, Item.LAP().WeaponSkillRealManaCost))
                    {
                        if (!player.CheckMana(Item.LAP().WeaponSkillRealManaCost, false))
                            return;
                    }
                }
                if (PrePayFocus(player, Item.LAP().WeaponSkillRealFocusCost))
                {
                    if (Item.LAP().WeaponSkillRealFocusCost != 0)
                        player.CheckFocus(Item.LAP().WeaponSkillRealFocusCost, true);
                }
                if (PrePayMana(player, Item.LAP().WeaponSkillRealManaCost))
                {
                    if (Item.LAP().WeaponSkillRealManaCost != 0)
                        player.CheckMana(player.ActiveItem(), Item.LAP().WeaponSkillRealManaCost, true);
                }
                WeaponSkill(player, soure, position, velocity, type, damage, knockback);
            }
        }
        public virtual bool PreCheckFocus(Player player, int focusCost)
        {
            return true;
        }
        public virtual bool PreCheckMana(Player player, int manaCost)
        {
            return true;
        }
        public virtual bool PrePayFocus(Player player, int focusCost)
        {
            return true;
        }
        public virtual bool PrePayMana(Player player, int manaCost)
        {
            return true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.IntegrateHotkey(LAPKeybind.WeaponSkillHotKey);
        }
        public virtual bool CanUseWeaponSkill(Player player)
        {
            return true;
        }
        public virtual void WeaponSkill(Player player, EntitySource_ItemUse_WeaponSkill source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

        }
        public virtual void UpdateHoldItem(Player player)
        {

        }
    }
}
