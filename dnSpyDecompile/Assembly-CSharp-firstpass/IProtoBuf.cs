using System;
using System.IO;

// Token: 0x02000009 RID: 9
public interface IProtoBuf
{
	// Token: 0x06000051 RID: 81
	void Deserialize(Stream stream);

	// Token: 0x06000052 RID: 82
	void Serialize(Stream stream);

	// Token: 0x06000053 RID: 83
	uint GetSerializedSize();
}
