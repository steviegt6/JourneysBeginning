using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Terraria.GameContent.UI.Elements;
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

        public string text;
        public Version version;

        public ChangelogData(string text, Version version)
        {
            this.text = text;
            this.version = version;
        }

        public static void PopulateChangelogList(string path, int length, Func<Stream> getStream)
        {
            if (Changelogs == null)
                Changelogs = new List<ChangelogData>();

            if (Path.GetExtension(path) == ".txt" && !path.Contains("build") && !path.Contains("description"))
            {
                string fileName = Path.GetFileNameWithoutExtension(path);
                Version version = new Version(fileName);

                if (version != null)
                {
                    string text = "";

                    using (Stream stream = getStream())
                        text = Encoding.UTF8.GetString(stream.ReadBytes(length));

                    Changelogs.Add(new ChangelogData(text, version));
                }
            }

            Changelogs = Changelogs.OrderByDescending(x => x.version).ToList();
        }
    }
}