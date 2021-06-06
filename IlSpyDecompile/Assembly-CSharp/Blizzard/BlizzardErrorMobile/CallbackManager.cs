using UnityEngine;

namespace Blizzard.BlizzardErrorMobile
{
	public class CallbackManager : MonoBehaviour
	{
		public static void RegisterExceptionHandler()
		{
		}

		public static void UnregisterExceptionHandler()
		{
		}

		public static long CatchCrashCaptureFromLog(long lastReadTime, string packageName)
		{
			return 0L;
		}
	}
}
