using System;
using Blizzard.T5.Services;

// Token: 0x02000099 RID: 153
public interface ITokenFetcherService : IService
{
	// Token: 0x06000997 RID: 2455
	string GenerateWebAuthToken(bool forceGenerate = false, bool writeToConfig = true);

	// Token: 0x06000998 RID: 2456
	string GetCurrentAuthToken();
}
