using Blizzard.T5.Services;

public interface ITokenFetcherService : IService
{
	string GenerateWebAuthToken(bool forceGenerate = false, bool writeToConfig = true);

	string GetCurrentAuthToken();
}
