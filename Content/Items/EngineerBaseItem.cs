using Terraria.ModLoader;
using JourneysBeginning.Content.DamageClasses;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.IO;

namespace JourneysBeginning.Content.Items
{
    public abstract class EngineerBaseItem : ModItem
    {
        public static readonly Color ThemeColor = new Color(110, 190, 150);

        // Protected set instead of virtual get only because you might want to change it at any time?
        public string Subclass { get; protected set; }
        protected virtual bool UseSubclassLine { get; }

        public override void SetDefaults() => Item.DamageType = ModContent.GetInstance<Engineer>();

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string subText = UseSubclassLine ? Subclass + " " + "subclass" : Subclass;
            TooltipLine tooltip = new TooltipLine(JourneysBeginning.Instance, "SubclassTip", $"- {subText} -")
            {
                overrideColor = ThemeColor
            };

            int index = Subclass == "Accessory" ?
                tooltips.FindIndex(t => t.mod == "Terraria" && t.Name == "ItemName") :
                tooltips.FindIndex(t => t.mod == "Terraria" && t.Name == "Damage");

            tooltips.Insert(index + 1, tooltip);
        }
    }

    public abstract class EngineerAccessory : EngineerBaseItem
    {
        protected override bool UseSubclassLine => false;
        public EngineerAccessory()
        {
            Subclass = "Engineer Accessory";
        }
    }

    public abstract class EngineerWeapon : EngineerBaseItem
    {
        protected override bool UseSubclassLine => true;
        public override void SetDefaults() => Item.DamageType = ModContent.GetInstance<Engineer>();
    }

    public abstract class AggressiveSubclassWeapon : EngineerBaseItem
    {
        protected int resource;

        public AggressiveSubclassWeapon()
        {
            Subclass = "Aggressive Engineering";
        }

        // Should ensure that the item instance doesn't have a different resource amount if you do smth like drop it and have someone else pick it up,
        // or leave and rejoin.
        public override void NetSend(BinaryWriter writer) => writer.Write(resource);

        public override void NetReceive(BinaryReader reader) => resource = reader.ReadInt32();
    }
}
