using System;

namespace Hearthstone.UI.Logging
{
	public class LogMessage
	{
		public readonly string Type = "";

		public readonly DateTime Timestamp = DateTime.Now;

		public readonly object Context;

		public readonly string Path = string.Empty;

		public readonly string Message = string.Empty;

		public readonly LogLevel Level = LogLevel.Info;

		public LogMessage(string message, object context, LogLevel level = LogLevel.Info, string path = "", string type = "")
		{
			Type = type;
			Message = message;
			Context = context;
			Level = level;
			Path = path;
		}

		public LogMessage(string message, object context, DateTime timestamp, LogLevel level = LogLevel.Info, string path = "", string type = "")
		{
			Type = type;
			Message = message;
			Context = context;
			Timestamp = timestamp;
			Level = level;
			Path = path;
		}
	}
}
