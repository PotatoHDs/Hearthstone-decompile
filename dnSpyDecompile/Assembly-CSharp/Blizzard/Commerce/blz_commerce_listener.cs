using System;
using System.Runtime.InteropServices;
using AOT;

namespace Blizzard.Commerce
{
	// Token: 0x0200125B RID: 4699
	public abstract class blz_commerce_listener
	{
		// Token: 0x0600D3BC RID: 54204 RVA: 0x003E3270 File Offset: 0x003E1470
		public blz_commerce_listener()
		{
			blz_commerce_listener.instance = this;
			this.delegateInstance = new blz_commerce_listener.CallbackDelegate(blz_commerce_listener.DoEvent);
		}

		// Token: 0x0600D3BD RID: 54205
		public abstract void OnEvent(IntPtr owner, blz_commerce_event_t ev);

		// Token: 0x0600D3BE RID: 54206 RVA: 0x003E3290 File Offset: 0x003E1490
		[MonoPInvokeCallback(typeof(blz_commerce_listener.CallbackDelegate))]
		private static void DoEvent(IntPtr owner, IntPtr ev)
		{
			if (blz_commerce_listener.instance != null && ev != IntPtr.Zero)
			{
				blz_commerce_listener.instance.OnEvent(owner, new blz_commerce_event_t(ev, false));
			}
		}

		// Token: 0x0400A31F RID: 41759
		public blz_commerce_listener.CallbackDelegate delegateInstance;

		// Token: 0x0400A320 RID: 41760
		private static blz_commerce_listener instance;

		// Token: 0x02002962 RID: 10594
		// (Invoke) Token: 0x06013EE1 RID: 81633
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CallbackDelegate(IntPtr owner, IntPtr ev);
	}
}
