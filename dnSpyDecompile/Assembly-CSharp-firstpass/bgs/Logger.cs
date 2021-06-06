using System;

namespace bgs
{
	// Token: 0x02000257 RID: 599
	public class Logger
	{
		// Token: 0x060024E0 RID: 9440 RVA: 0x00003D6E File Offset: 0x00001F6E
		public LogLevel GetDefaultLevel()
		{
			return LogLevel.Debug;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00082590 File Offset: 0x00080790
		public void Print(string format, params object[] args)
		{
			LogLevel defaultLevel = this.GetDefaultLevel();
			this.Print(defaultLevel, format, args);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000825B0 File Offset: 0x000807B0
		public void Print(LogLevel level, string format, params object[] args)
		{
			string message;
			if (args.Length == 0)
			{
				message = format;
			}
			else
			{
				message = string.Format(format, args);
			}
			this.Print(level, message);
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000825D5 File Offset: 0x000807D5
		public void Print(LogLevel level, string message)
		{
			LogAdapter.Log(level, message, "");
		}
	}
}
