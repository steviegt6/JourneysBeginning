namespace JourneysBeginning.Common.Interfaces.Loading
{
    /// <summary>
    /// Interface that automatically loads Detours (method swaps). <br />
    /// Be sure to hook in <see cref="ILoadable.Load"/> and unhook in <see cref="ILoadable.Unload"/>.
    /// </summary>
    public interface IDetourable : ILoadable
    {
        string DictKey { get; }
    }
}