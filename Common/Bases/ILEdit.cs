namespace JourneysBeginning.Common.Bases
{
    /// <summary>
    /// Abstract class that allows autoloading <c>IL edits</c>.
    /// </summary>
    public abstract class ILEdit
    {
        public abstract string DictKey { get; }

        public abstract void Load();

        public abstract void Unload();
    }
}