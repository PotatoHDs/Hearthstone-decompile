namespace bgs
{
	public class Logger
	{
		public LogLevel GetDefaultLevel()
		{
			return LogLevel.Debug;
		}

		public void Print(string format, params object[] args)
		{
			LogLevel defaultLevel = GetDefaultLevel();
			Print(defaultLevel, format, args);
		}

		public void Print(LogLevel level, string format, params object[] args)
		{
			string message = ((args.Length != 0) ? string.Format(format, args) : format);
			Print(level, message);
		}

		public void Print(LogLevel level, string message)
		{
			LogAdapter.Log(level, message);
		}
	}
}
