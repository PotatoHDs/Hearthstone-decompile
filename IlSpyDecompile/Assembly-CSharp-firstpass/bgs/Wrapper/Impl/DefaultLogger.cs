using System;

namespace bgs.Wrapper.Impl
{
	internal class DefaultLogger : LoggerBase
	{
		public override void Log(LogLevel logLevel, string str, string sourceName = "")
		{
			Console.WriteLine(str);
		}

		public override void LogDebug(string message, string sourceName = "")
		{
			Log(LogLevel.Debug, message);
		}

		public override void LogInfo(string message, string sourceName = "")
		{
			Log(LogLevel.Info, message);
		}

		public override void LogWarning(string message, string sourceName = "")
		{
			Log(LogLevel.Warning, message);
		}

		public override void LogError(string message, string sourceName = "")
		{
			Log(LogLevel.Error, message);
		}

		public override void LogException(string message, string sourceName = "")
		{
			Log(LogLevel.Exception, message);
		}

		public override void LogFatal(string message, string sourceName = "")
		{
			Log(LogLevel.Fatal, message);
		}
	}
}
