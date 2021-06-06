using System;

namespace bgs.Wrapper.Impl
{
	// Token: 0x0200026D RID: 621
	internal class DefaultLogger : LoggerBase
	{
		// Token: 0x060025A1 RID: 9633 RVA: 0x00085E99 File Offset: 0x00084099
		public override void Log(LogLevel logLevel, string str, string sourceName = "")
		{
			Console.WriteLine(str);
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x00085EA1 File Offset: 0x000840A1
		public override void LogDebug(string message, string sourceName = "")
		{
			this.Log(LogLevel.Debug, message, "");
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x00085EB0 File Offset: 0x000840B0
		public override void LogInfo(string message, string sourceName = "")
		{
			this.Log(LogLevel.Info, message, "");
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x00085EBF File Offset: 0x000840BF
		public override void LogWarning(string message, string sourceName = "")
		{
			this.Log(LogLevel.Warning, message, "");
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x00085ECE File Offset: 0x000840CE
		public override void LogError(string message, string sourceName = "")
		{
			this.Log(LogLevel.Error, message, "");
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x00085EDD File Offset: 0x000840DD
		public override void LogException(string message, string sourceName = "")
		{
			this.Log(LogLevel.Exception, message, "");
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x00085EEC File Offset: 0x000840EC
		public override void LogFatal(string message, string sourceName = "")
		{
			this.Log(LogLevel.Fatal, message, "");
		}
	}
}
