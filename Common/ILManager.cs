using JourneysBeginning.Common.Bases;
using System;
using System.Collections.Generic;

namespace JourneysBeginning.Common
{
    /// <summary>
    /// Class responsible for handling automatically-loaded IL edits and detours.
    /// </summary>
    public static class ILManager
    {
        /// <summary>
        /// Whether all IL edits and Detours have been hooked and applied.
        /// </summary>
        public static bool HasLoaded { get; private set; }

        /// <summary>
        /// A dictionary of all IL edits added by <see cref="JourneysBeginning"/>. <br />
        /// The key for each <see cref="ILEdit"/> is <c>Type.MethodName</c>.
        /// </summary>
        public static Dictionary<string, ILEdit> ILEdits;

        /// <summary>
        /// A dictionary of all detours added by <see cref="JourneysBeginning"/>. <br />
        /// The key for each <see cref="Detour"/> is <c>Type.MethodName</c>.
        /// </summary>
        public static Dictionary<string, Detour> Detours;

        internal static void Load()
        {
            ILEdits = new Dictionary<string, ILEdit>();
            Detours = new Dictionary<string, Detour>();

            Type[] types = JourneysBeginning.Instance.Code.GetTypes();

            foreach (Type type in types)
            {
                if (type.IsAbstract || type.GetConstructor(new Type[0]) == null)
                    continue;

                if (type.IsSubclassOf(typeof(ILEdit)))
                {
                    ILEdit ilEdit = Activator.CreateInstance(type) as ILEdit;

                    ILEdits.Add(ilEdit.DictKey, ilEdit);
                }

                if (type.IsSubclassOf(typeof(Detour)))
                {
                    Detour detour = Activator.CreateInstance(type) as Detour;

                    Detours.Add(detour.DictKey, detour);
                }
            }

            JourneysBeginning.ModLogger.Debug($"Found {ILEdits.Count} IL edits to load!");

            foreach (ILEdit ilEdit in ILEdits.Values)
                ilEdit.Load();

            JourneysBeginning.ModLogger.Debug($"Found {Detours.Count} detours to load!");

            foreach (Detour detour in Detours.Values)
                detour.Load();

            HasLoaded = true;
        }

        internal static void Unload()
        {
            foreach (ILEdit ilEdit in ILEdits.Values)
                ilEdit.Unload();

            foreach (Detour detour in Detours.Values)
                detour.Unload();

            ILEdits = null;
            Detours = null;

            HasLoaded = false;
        }
    }
}