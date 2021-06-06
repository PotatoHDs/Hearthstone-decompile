using System;
using Blizzard.Commerce;
using UnityEngine;

namespace Hearthstone.Commerce
{
	// Token: 0x02001073 RID: 4211
	public class BlizzardCommerceLogger : blz_commerce_log_hook
	{
		// Token: 0x0600B5F9 RID: 46585 RVA: 0x0037D700 File Offset: 0x0037B900
		public override void OnLogEvent(IntPtr owner, CommerceLogLevel level, string subsystem, string message)
		{
			switch (level)
			{
			case CommerceLogLevel.NOISE:
				Debug.LogFormat("[COMMMERCE|NOISE ({0})] {1}", new object[]
				{
					subsystem,
					message
				});
				return;
			case CommerceLogLevel.DEBUG:
				Debug.LogFormat("[COMMMERCE|DEBUG ({0})] {1}", new object[]
				{
					subsystem,
					message
				});
				return;
			case CommerceLogLevel.INFO:
				Debug.LogFormat("[COMMMERCE|INFO ({0})] {1}", new object[]
				{
					subsystem,
					message
				});
				return;
			case CommerceLogLevel.WARNING:
				Debug.LogWarningFormat("[COMMMERCE|WARNING ({0})] {1}", new object[]
				{
					subsystem,
					message
				});
				return;
			case CommerceLogLevel.ERROR:
				Debug.LogErrorFormat("[COMMMERCE|ERROR ({0})] {1}", new object[]
				{
					subsystem,
					message
				});
				return;
			case CommerceLogLevel.FATAL:
				Debug.LogErrorFormat("[COMMMERCE|FATAL ({0})] {1}", new object[]
				{
					subsystem,
					message
				});
				return;
			default:
				return;
			}
		}
	}
}
