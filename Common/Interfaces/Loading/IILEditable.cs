namespace JourneysBeginning.Common.Interfaces.Loading
{
    /// <summary>
    /// Interface that automatically loads IL edits. <br />
    /// Be sure to hook in <see cref="ILoadable.Load"/> and unhook in <see cref="ILoadable.Unload"/>.
    /// </summary>
    public interface IILEditable : ILoadable
    {
        string DictKey { get; }
    }
}