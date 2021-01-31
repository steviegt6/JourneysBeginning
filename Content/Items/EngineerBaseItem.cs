using Terraria.ModLoader;
using JourneysBeginning.Content.DamageClasses;
using Microsoft.Xna.Framework;
using System.IO;

namespace JourneysBeginning.Content.Items
{
    public abstract class EngineerBaseItem : JBBaseItem
    {
        public override string ClassLineText => "Engineer";

        public override Color ClassLineColor => new Color(110, 190, 150);

        /// <summary>
        /// Serves <see cref="SetDefaults"/>' purpose, called at the end of <see cref="SetDefaults"/>.
        /// </summary>
        public virtual void SetDefaultsSafely()
        {
        }

        public sealed override void SetDefaults() => Item.DamageType = ModContent.GetInstance<Engineer>();
    }
}
