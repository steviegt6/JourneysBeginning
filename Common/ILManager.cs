using JourneysBeginning.Common.Interfaces.Loading;
using System;
using System.Collections.Generic;

namespace JourneysBeginning.Common
{
    /// <summary>
    /// Class reposible for handling automatically-loaded IL edits and detours.
    /// </summary>
    public static class ILManager
    {
        public static bool HasLoaded { get; private set; }

        public static Dictionary<string, IILEditable> ILEdits;
        public static Dictionary<string, IDetourable> Detours;

        public static void Load()
        {
            ILEdits = new Dictionary<string, IILEditable>();
            Detours = new Dictionary<string, IDetourable>();

            Type[] types = JourneysBeginning.Instance.Code.GetTypes();

            foreach (Type type in types)
            {
                if (type.IsAssignableFrom(typeof(IILEditable)))
                {
                    IILEditable ilEdit = Activator.CreateInstance(type) as IILEditable;

                    ILEdits.Add(ilEdit.DictKey, ilEdit);
                }

                if (type.IsAssignableFrom(typeof(IDetourable)))
                {
                    IDetourable detour = Activator.CreateInstance(type) as IDetourable;

                    Detours.Add(detour.DictKey, detour);
                }
            }

            foreach (IILEditable ilEdit in ILEdits.Values)
                ilEdit.Load();

            foreach (IDetourable detour in Detours.Values)
                detour.Load();

            HasLoaded = true;
        }

        public static void Unload()
        {
            foreach (IILEditable ilEdit in ILEdits.Values)
                ilEdit.Unload();

            foreach (IDetourable detour in Detours.Values)
                detour.Unload();

            ILEdits = null;
            Detours = null;

            HasLoaded = false;
        }
    }
}