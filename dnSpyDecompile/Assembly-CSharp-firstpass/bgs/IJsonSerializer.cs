using System;

namespace bgs
{
	// Token: 0x02000223 RID: 547
	public interface IJsonSerializer
	{
		// Token: 0x06002303 RID: 8963
		T Deserialize<T>(string json);

		// Token: 0x06002304 RID: 8964
		string Serialize(object obj);
	}
}
