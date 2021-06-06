using System.IO;

public interface IOfflineDataSerializer
{
	void Serialize(OfflineDataCache.OfflineData data, BinaryWriter writer);

	OfflineDataCache.OfflineData Deserialize(BinaryReader reader);
}
