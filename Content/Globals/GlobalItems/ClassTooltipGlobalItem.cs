using System.Collections.Generic;
using JourneysBeginning.Content.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JourneysBeginning.Content.Globals.GlobalItems
{
    public class ClassTooltipGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.ModItem is JBBaseItem)
            {
                JBBaseItem jbItem = item.ModItem as JBBaseItem;

                if (!string.IsNullOrEmpty(jbItem.ClassLineText))
                {
                    TooltipLine subclassLine = new TooltipLine(Mod, $"{Mod.Name}:SubclassTooltip", $"- {jbItem.ClassLineText} -")
                    {
                        overrideColor = jbItem.ClassLineColor
                    };

                    tooltips.Insert(tooltips.FindIndex(tip => tip.mod == "Terraria" && tip.Name == "ItemName") + 1, subclassLine);
                }
            }
            else
            {
                string className = "";
                Color classColor = Color.White;

                if (item.DamageType == DamageClass.Melee)
                {
                    className = "Melee";
                    classColor = new Color(255, 165, 0); // Bright orange
                }
                else if (item.DamageType == DamageClass.Ranged)
                {
                    className = "Ranged";
                    classColor = new Color(64, 224, 208); // Turquoise
                }
                else if (item.DamageType == DamageClass.Magic)
                {
                    className = "Magic";
                    classColor = new Color(255, 0, 255); // Magenta
                }
                else if (item.DamageType == DamageClass.Summon)
                {
                    className = "Summon";
                    classColor = new Color(0, 191, 255); // Deep, sky blue
                }
                else if (item.DamageType == DamageClass.Throwing)
                {
                    className = "Thrower";
                    classColor = new Color(127, 255, 0); // Bright, yellow-ish green (chartreuse), similar to Thorium's White Dwarf Fragments
                }

                if (!string.IsNullOrEmpty(className))
                {
                    TooltipLine subclassLine = new TooltipLine(Mod, $"{Mod.Name}:SubclassTooltip", $"- {className} -")
                    {
                        overrideColor = classColor
                    };

                    tooltips.Insert(tooltips.FindIndex(tip => tip.mod == "Terraria" && tip.Name == "ItemName") + 1, subclassLine);
                }
            }
        }
    }
}
