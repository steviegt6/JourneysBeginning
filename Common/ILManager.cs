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
        public static bool HasLoaded { get; private set; }

        public static Dictionary<string, ILEdit> ILEdits;
        public static Dictionary<string, Detour> Detours;

        public static void Load()
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

        public static void Unload()
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