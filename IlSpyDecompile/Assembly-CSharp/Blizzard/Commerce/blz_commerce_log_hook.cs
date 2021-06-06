using System;
using System.Runtime.InteropServices;
using AOT;

namespace Blizzard.Commerce
{
	public abstract class blz_commerce_log_hook
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CallbackDelegate(IntPtr owner, CommerceLogLevel level, string subsystem, string message);

		public CallbackDelegate delegateInstance;

		private static blz_commerce_log_hook instance;

		public blz_commerce_log_hook()
		{
			instance = this;
			delegateInstance = DoLog;
		}

		public abstract void OnLogEvent(IntPtr owner, CommerceLogLevel level, string subsystem, string message);

		[MonoPInvokeCallback(typeof(CallbackDelegate))]
		private static void DoLog(IntPtr owner, CommerceLogLevel level, string subsystem, string message)
		{
			instance.OnLogEvent(owner, level, subsystem, message);
		}
	}
}
