using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace bgs
{
	// Token: 0x0200026A RID: 618
	public abstract class LoggerBase : LoggerInterface
	{
		// Token: 0x0600258F RID: 9615
		public abstract void Log(LogLevel logLevel, string str, string sourceName = "");

		// Token: 0x06002590 RID: 9616
		public abstract void LogDebug(string message, string sourceName = "");

		// Token: 0x06002591 RID: 9617
		public abstract void LogInfo(string message, string sourceName = "");

		// Token: 0x06002592 RID: 9618
		public abstract void LogWarning(string message, string sourceName = "");

		// Token: 0x06002593 RID: 9619
		public abstract void LogError(string message, string sourceName = "");

		// Token: 0x06002594 RID: 9620
		public abstract void LogException(string message, string sourceName = "");

		// Token: 0x06002595 RID: 9621
		public abstract void LogFatal(string message, string sourceName = "");

		// Token: 0x06002596 RID: 9622 RVA: 0x00085D9C File Offset: 0x00083F9C
		public void LogDebugStackTrace(string message, int maxFrames, int skipFrames = 0, string logSource = "")
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(message + "\n");
			for (int i = 1 + skipFrames; i < maxFrames; i++)
			{
				StackFrame frame = new StackTrace(new StackFrame(i, true)).GetFrame(0);
				if (frame == null || !(frame.GetMethod() != null) || frame.GetMethod().ToString().StartsWith("<"))
				{
					break;
				}
				stringBuilder.Append(string.Format("File \"{0}\", line {1} -- {2}\n", Path.GetFileName(frame.GetFileName()), frame.GetFileLineNumber(), frame.GetMethod()));
			}
			this.Log(LogLevel.Debug, stringBuilder.ToString().TrimEnd(Array.Empty<char>()), logSource);
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x00085E50 File Offset: 0x00084050
		protected string FormatStackTrace(StackFrame sf, bool fullPath = false)
		{
			if (sf != null)
			{
				string arg = fullPath ? sf.GetFileName() : Path.GetFileName(sf.GetFileName());
				return string.Format(" ({2} at {0}:{1})", arg, sf.GetFileLineNumber(), sf.GetMethod());
			}
			return "";
		}
	}
}
