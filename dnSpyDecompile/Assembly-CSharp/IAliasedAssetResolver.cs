using System;
using Blizzard.T5.Services;

// Token: 0x02000852 RID: 2130
public interface IAliasedAssetResolver : IService
{
	// Token: 0x06007366 RID: 29542
	AssetReference GetCardDefAssetRefFromCardId(string cardId);
}
