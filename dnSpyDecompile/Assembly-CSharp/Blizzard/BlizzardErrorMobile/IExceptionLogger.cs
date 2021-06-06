using System;

namespace Blizzard.BlizzardErrorMobile
{
	// Token: 0x02001217 RID: 4631
	public interface IExceptionLogger
	{
		// Token: 0x0600CFE4 RID: 53220
		void LogDebug(string format, params object[] args);

		// Token: 0x0600CFE5 RID: 53221
		void LogInfo(string format, params object[] args);

		// Token: 0x0600CFE6 RID: 53222
		void LogWarning(string format, params object[] args);

		// Token: 0x0600CFE7 RID: 53223
		void LogError(string format, params object[] args);
	}
}
