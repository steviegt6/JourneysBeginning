namespace JourneysBeginning.Common.Interfaces.Loading
{
    /// <summary>
    /// Interface containing a <see cref="Load"/> and <see cref="Unload"/> method.
    /// </summary>
    public interface ILoadable
    {
        void Load();

        void Unload();
    }
}