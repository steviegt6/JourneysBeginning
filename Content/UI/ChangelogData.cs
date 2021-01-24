using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace JourneysBeginning.Content.UI
{
    /// <summary>
    /// Struct containing basic data for a changelog.
    /// </summary>
    public struct ChangelogData
    {
        public static List<ChangelogData> Changelogs;
        public static UIImage ChangelogButton;

        /// <summary>
        /// Raw text. Try using <see cref="ToString"/>.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The changelog's respective version.
        /// </summary>
        public Version Version { get; set; }

        public ChangelogData(string text, Version version)
        {
            Text = text;
            Version = version;
        }

        public override string ToString()
        {
            string text = Text;

            if (text.StartsWith("*"))
                text.Remove(0, 1); // Remove the asterisk if the text starts with one.

            return text;
        }

        public static void PopulateChangelogList(Mod mod)
        {
            // Create a list of existing changelogs.
            List<string> changelogs = new List<string>
            {
                "1.0.0.0"
            };

            Changelogs = new List<ChangelogData>();

            // Attempt to read every file specified.
            foreach (string changelog in changelogs)
            {
                string file = $"Changelogs{Path.DirectorySeparatorChar}{changelog}.txt";

                if (mod.FileExists(file))
                    Changelogs.Add(new ChangelogData(Encoding.UTF8.GetString(mod.GetFileBytes(file)), new Version(changelog)));
            }

            // Sort by Version.
            Changelogs = Changelogs.OrderByDescending(x => x.Version).ToList();
        }

        internal static void Unload()
        {
            ChangelogButton = null;
            Changelogs = null;
        }
    }
}