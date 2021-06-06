using System;
using UnityEngine;

namespace Blizzard.BlizzardErrorMobile
{
	// Token: 0x02001215 RID: 4629
	public class CallbackManager : MonoBehaviour
	{
		// Token: 0x0600CFD9 RID: 53209 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public static void RegisterExceptionHandler()
		{
		}

		// Token: 0x0600CFDA RID: 53210 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public static void UnregisterExceptionHandler()
		{
		}

		// Token: 0x0600CFDB RID: 53211 RVA: 0x0022CD1C File Offset: 0x0022AF1C
		public static long CatchCrashCaptureFromLog(long lastReadTime, string packageName)
		{
			return 0L;
		}
	}
}
