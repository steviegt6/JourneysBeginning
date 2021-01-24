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

        public static void PopulateChangelogList(string path, int length, Func<Stream> getStream)
        {
            if (Changelogs == null)
                Changelogs = new List<ChangelogData>();

            // Check for text files, ignore build.txt and description.txt.
            if (Path.GetExtension(path) == ".txt" && !path.Contains("build") && !path.Contains("description"))
            {
                // Convert the file name to a Version to allow for easy sorting.
                string fileName = Path.GetFileNameWithoutExtension(path);
                Version version = new Version(fileName);

                // If it was successfully converted to a version, convert the bytes to a string and add the changelog data.
                if (version != null)
                {
                    string text = "";

                    using (Stream stream = getStream())
                        text = Encoding.UTF8.GetString(stream.ReadBytes(length));

                    Changelogs.Add(new ChangelogData(text, version));
                }
            }

            // Sort by Version.
            Changelogs = Changelogs.OrderByDescending(x => x.Version).ToList();
        }
    }
}