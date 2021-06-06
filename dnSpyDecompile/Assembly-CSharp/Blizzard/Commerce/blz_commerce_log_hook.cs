using System;
using System.Runtime.InteropServices;
using AOT;

namespace Blizzard.Commerce
{
	// Token: 0x0200125C RID: 4700
	public abstract class blz_commerce_log_hook
	{
		// Token: 0x0600D3BF RID: 54207 RVA: 0x003E32B8 File Offset: 0x003E14B8
		public blz_commerce_log_hook()
		{
			blz_commerce_log_hook.instance = this;
			this.delegateInstance = new blz_commerce_log_hook.CallbackDelegate(blz_commerce_log_hook.DoLog);
		}

		// Token: 0x0600D3C0 RID: 54208
		public abstract void OnLogEvent(IntPtr owner, CommerceLogLevel level, string subsystem, string message);

		// Token: 0x0600D3C1 RID: 54209 RVA: 0x003E32D8 File Offset: 0x003E14D8
		[MonoPInvokeCallback(typeof(blz_commerce_log_hook.CallbackDelegate))]
		private static void DoLog(IntPtr owner, CommerceLogLevel level, string subsystem, string message)
		{
			blz_commerce_log_hook.instance.OnLogEvent(owner, level, subsystem, message);
		}

		// Token: 0x0400A321 RID: 41761
		public blz_commerce_log_hook.CallbackDelegate delegateInstance;

		// Token: 0x0400A322 RID: 41762
		private static blz_commerce_log_hook instance;

		// Token: 0x02002963 RID: 10595
		// (Invoke) Token: 0x06013EE5 RID: 81637
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CallbackDelegate(IntPtr owner, CommerceLogLevel level, string subsystem, string message);
	}
}
