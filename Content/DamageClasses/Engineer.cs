using Terraria.ModLoader;

namespace JourneysBeginning.Content.DamageClasses {
    public class Engineer : DamageClass {
        public override void SetupContent() {
            this.ClassName.SetDefault("engineered damage");
        }
        protected override float GetBenefitFrom(DamageClass damageClass) => damageClass == DamageClass.Generic ? 1f : 0f;
    }
}
