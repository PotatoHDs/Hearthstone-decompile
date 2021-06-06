namespace Blizzard.BlizzardErrorMobile
{
	public interface IExceptionLogger
	{
		void LogDebug(string format, params object[] args);

		void LogInfo(string format, params object[] args);

		void LogWarning(string format, params object[] args);

		void LogError(string format, params object[] args);
	}
}
