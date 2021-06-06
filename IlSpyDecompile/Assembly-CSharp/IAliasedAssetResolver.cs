using Blizzard.T5.Services;

public interface IAliasedAssetResolver : IService
{
	AssetReference GetCardDefAssetRefFromCardId(string cardId);
}
