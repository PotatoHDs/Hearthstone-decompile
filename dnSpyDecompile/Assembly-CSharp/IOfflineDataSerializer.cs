using System;
using System.IO;

// Token: 0x020008F3 RID: 2291
public interface IOfflineDataSerializer
{
	// Token: 0x06007F62 RID: 32610
	void Serialize(OfflineDataCache.OfflineData data, BinaryWriter writer);

	// Token: 0x06007F63 RID: 32611
	OfflineDataCache.OfflineData Deserialize(BinaryReader reader);
}
