namespace JourneysBeginning.Common.Bases
{
    /// <summary>
    /// Abstract class that allows autoloading <c>Detours</c>.
    /// </summary>
    public abstract class Detour
    {
        public abstract string DictKey { get; }

        public abstract void Load();

        public abstract void Unload();
    }
}