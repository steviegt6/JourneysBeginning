using System.Collections.Generic;
using JourneysBeginning.Common.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JourneysBeginning.Content.Globals.GlobalItems
{
    public class BetterTooltipsGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Item unmodifiedItem = new Item(item.type);

            if (tooltips.TryGetVanillaTooltipLine("Favorite", out TooltipLine favoriteLine))
                favoriteLine.overrideColor = JourneysBeginning.TerrariaGoldYellow;

            if (tooltips.TryGetVanillaTooltipLine("FavoriteDesc", out TooltipLine favoriteDescLine))
                favoriteDescLine.overrideColor = JourneysBeginning.TerrariaGoldYellow;

            if (tooltips.TryGetVanillaTooltipLine("Social", out TooltipLine socialLine))
                socialLine.overrideColor = Color.Gray;

            if (tooltips.TryGetVanillaTooltipLine("SocialDesc", out TooltipLine socialDescLine))
                socialDescLine.overrideColor = Color.Gray;

            if (tooltips.TryGetVanillaTooltipLine("Damage", out TooltipLine damageLine))
            {
                int baseDamage = unmodifiedItem.damage;
                int currentDamage = Main.LocalPlayer.GetWeaponDamage(item);
                int diffDamage = currentDamage - baseDamage;
                Color drawColor = diffDamage > 0 ? new Color(120, 190, 120) : new Color(190, 120, 120);
                char sign = diffDamage > 0 ? '+' : '-';

                if (diffDamage != 0)
                {
                    if (diffDamage < 0)
                        diffDamage = -diffDamage;

                    damageLine.text += $" [c/{drawColor.Hex3()}:({baseDamage} {sign} {diffDamage})]";
                }

                damageLine.overrideColor = new Color(255, 204 - 30, 203 - 30);
            }

            if (tooltips.TryGetVanillaTooltipLine("CritChance", out TooltipLine critChanceLine))
            {
                int baseCrit = unmodifiedItem.crit + 4;
                int currentCrit = Main.LocalPlayer.GetWeaponCrit(item);
                int diffCrit = currentCrit - baseCrit;
                Color drawColor = diffCrit > 0 ? new Color(120, 190, 120) : new Color(190, 120, 120);
                char sign = diffCrit > 0 ? '+' : '-';

                if (diffCrit != 0)
                {
                    if (diffCrit < 0)
                        diffCrit = -diffCrit;

                    critChanceLine.text += $" [c/{drawColor.Hex3()}:({baseCrit} {sign} {diffCrit})]";
                }

                critChanceLine.overrideColor = new Color(255, 255, 102); // Lighter yellow
            }

            if (tooltips.TryGetVanillaTooltipLine("Knockback", out TooltipLine knockbackLine))
            {
                float baseKB = unmodifiedItem.knockBack;
                float currentKB = Main.LocalPlayer.GetWeaponKnockback(item, item.knockBack);
                float diffKB = currentKB - baseKB;
                Color drawColor = currentKB > 0f ? new Color(120, 190, 120) : new Color(190, 120, 120);
                char sign = diffKB > 0f ? '+' : '-';

                knockbackLine.text += $" ({currentKB})";

                if (diffKB != 0f)
                {
                    if (diffKB < 0f)
                        diffKB = -diffKB;

                    knockbackLine.text += $" [c/{drawColor.Hex3()}:({baseKB} {sign} {diffKB})]";
                }
            }

            if (tooltips.TryGetVanillaTooltipLine("FishingPower", out TooltipLine fishingPowerLine))
                fishingPowerLine.overrideColor = Color.CornflowerBlue;

            if (tooltips.TryGetVanillaTooltipLine("NeedsBait", out TooltipLine needsBaitLine))
                needsBaitLine.overrideColor = Color.CornflowerBlue;

            if (tooltips.TryGetVanillaTooltipLine("BaitPower", out TooltipLine baitPowerLine))
                baitPowerLine.overrideColor = Color.CornflowerBlue;

            if (tooltips.TryGetVanillaTooltipLine("Quest", out TooltipLine questLine))
                questLine.overrideColor = Color.Gold;

            if (tooltips.TryGetVanillaTooltipLine("Vanity", out TooltipLine vanityLine))
                vanityLine.overrideColor = Color.LightGray;

            if (tooltips.TryGetVanillaTooltipLine("VanityLegal", out TooltipLine vanityLegalLine))
                tooltips.Remove(vanityLegalLine);

            if (tooltips.TryGetVanillaTooltipLine("Defense", out TooltipLine defenseLine))
                defenseLine.overrideColor = Color.DarkGray;

            if (tooltips.TryGetVanillaTooltipLine("HealLife", out TooltipLine healLifeLine))
                healLifeLine.overrideColor = CombatText.HealLife;

            if (tooltips.TryGetVanillaTooltipLine("HealMana", out TooltipLine healManaLine))
                healManaLine.overrideColor = CombatText.HealMana;

            if (tooltips.TryGetVanillaTooltipLine("UseMana", out TooltipLine useManaLine))
            {
                int baseMana = unmodifiedItem.mana;
                int currentMana = (int)(item.mana * Main.LocalPlayer.manaCost);
                int diffMana = currentMana - baseMana;
                Color drawColor = currentMana > 0f ? new Color(120, 190, 120) : new Color(190, 120, 120);
                char sign = diffMana > 0f ? '+' : '-';

                if (diffMana != 0f)
                {
                    if (diffMana < 0f)
                        diffMana = -diffMana;

                    knockbackLine.text += $" [c/{drawColor.Hex3()}:({baseMana} {sign} {diffMana})]";
                }

                useManaLine.overrideColor = CombatText.HealMana;
            }

            if (tooltips.TryGetVanillaTooltipLine("Expert", out TooltipLine expertLine))
                expertLine.overrideColor = Main.DiscoColor;

            if (tooltips.TryGetVanillaTooltipLine("Master", out TooltipLine masterLine))
                masterLine.overrideColor = new Color(255, (byte)(Main.masterColor * 200f), 0);

            if (item.ModItem != null)
                tooltips.Add(new TooltipLine(Mod, $"{Mod.Name}:ModItemMod", $"[{Mod.Name}:{item.ModItem.Name}]") { overrideColor = Colors.RarityBlue });
        }
    }
}
