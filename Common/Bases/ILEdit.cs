using JourneysBeginning.Common.Interfaces.Loading;

namespace JourneysBeginning.Common.Bases
{
    public abstract class ILEdit : IILEditable
    {
        public abstract string DictKey { get; }

        public abstract void Load();

        public abstract void Unload();
    }
}