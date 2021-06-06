using System;
using Blizzard.Commerce;
using UnityEngine;

namespace Hearthstone.Commerce
{
	public class BlizzardCommerceLogger : blz_commerce_log_hook
	{
		public override void OnLogEvent(IntPtr owner, CommerceLogLevel level, string subsystem, string message)
		{
			switch (level)
			{
			case CommerceLogLevel.NOISE:
				Debug.LogFormat("[COMMMERCE|NOISE ({0})] {1}", subsystem, message);
				break;
			case CommerceLogLevel.DEBUG:
				Debug.LogFormat("[COMMMERCE|DEBUG ({0})] {1}", subsystem, message);
				break;
			case CommerceLogLevel.INFO:
				Debug.LogFormat("[COMMMERCE|INFO ({0})] {1}", subsystem, message);
				break;
			case CommerceLogLevel.WARNING:
				Debug.LogWarningFormat("[COMMMERCE|WARNING ({0})] {1}", subsystem, message);
				break;
			case CommerceLogLevel.ERROR:
				Debug.LogErrorFormat("[COMMMERCE|ERROR ({0})] {1}", subsystem, message);
				break;
			case CommerceLogLevel.FATAL:
				Debug.LogErrorFormat("[COMMMERCE|FATAL ({0})] {1}", subsystem, message);
				break;
			}
		}
	}
}
