using System.IO;

namespace JourneysBeginning.Content.Items
{
    public abstract class ResourceEngineerBaseItem : EngineerBaseItem
    {
        public int resource;

        // Should ensure that the item instance doesn't have a different resource amount if you do smth like drop it and have someone else pick it up,
        // or leave and rejoin.
        public override void NetSend(BinaryWriter writer) => writer.Write(resource);

        public override void NetReceive(BinaryReader reader) => resource = reader.ReadInt32();
    }
}
