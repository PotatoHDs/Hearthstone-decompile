using System;
using System.Runtime.InteropServices;
using AOT;

namespace Blizzard.Commerce
{
	public abstract class blz_commerce_listener
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CallbackDelegate(IntPtr owner, IntPtr ev);

		public CallbackDelegate delegateInstance;

		private static blz_commerce_listener instance;

		public blz_commerce_listener()
		{
			instance = this;
			delegateInstance = DoEvent;
		}

		public abstract void OnEvent(IntPtr owner, blz_commerce_event_t ev);

		[MonoPInvokeCallback(typeof(CallbackDelegate))]
		private static void DoEvent(IntPtr owner, IntPtr ev)
		{
			if (instance != null && ev != IntPtr.Zero)
			{
				instance.OnEvent(owner, new blz_commerce_event_t(ev, cMemoryOwn: false));
			}
		}
	}
}
