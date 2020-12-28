using System.Reflection;
using Terraria.ModLoader;

namespace JourneysBeginning
{
    public class JourneysBeginning : Mod
    {
        public static JourneysBeginning Instance { get; private set; }

        public static Assembly TMLAssembly { get; private set; }

        public JourneysBeginning()
        {
            Instance = this;
            TMLAssembly = typeof(ModLoader).Assembly;
        }
    }
}