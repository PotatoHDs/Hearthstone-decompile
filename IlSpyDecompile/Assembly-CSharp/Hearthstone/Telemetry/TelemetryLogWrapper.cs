using Blizzard.Telemetry;

namespace Hearthstone.Telemetry
{
	public class TelemetryLogWrapper : ILogger
	{
		public void LogDebug(string message)
		{
			Log.Telemetry.PrintDebug("[SDK] {0}", message);
		}

		public void LogInfo(string message)
		{
			Log.Telemetry.PrintInfo("[SDK] {0}", message);
		}

		public void LogWarning(string message)
		{
			Log.Telemetry.PrintWarning("[SDK] {0}", message);
		}

		public void LogError(string message)
		{
			Log.Telemetry.PrintError("[SDK] {0}", message);
		}

		public void LogFatal(string message)
		{
			Log.Telemetry.PrintError("[SDK] {0}", message);
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return true;
		}
	}
}
