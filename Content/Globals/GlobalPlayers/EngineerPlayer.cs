using Terraria.ModLoader;

namespace JourneysBeginning.Content.Globals.GlobalPlayers {
    public class EngineerPlayer : ModPlayer {
        public override bool CloneNewInstances => false;
        public int charges;
        public EngineerPlayer() {
            // as of right now, charges are a pretty ethereal concept. later on they might be expandable, 
            // but for now, they'll be capped to 3.
            charges = 3;
        }
    }
}
