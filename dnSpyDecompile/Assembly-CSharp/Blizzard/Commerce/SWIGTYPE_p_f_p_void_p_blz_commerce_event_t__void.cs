using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200125F RID: 4703
	public class SWIGTYPE_p_f_p_void_p_blz_commerce_event_t__void
	{
		// Token: 0x0600D3E7 RID: 54247 RVA: 0x003E354D File Offset: 0x003E174D
		internal SWIGTYPE_p_f_p_void_p_blz_commerce_event_t__void(IntPtr cPtr, bool futureUse)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D3E8 RID: 54248 RVA: 0x003E3562 File Offset: 0x003E1762
		protected SWIGTYPE_p_f_p_void_p_blz_commerce_event_t__void()
		{
			this.swigCPtr = new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D3E9 RID: 54249 RVA: 0x003E357B File Offset: 0x003E177B
		internal static HandleRef getCPtr(SWIGTYPE_p_f_p_void_p_blz_commerce_event_t__void obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0400A32A RID: 41770
		private HandleRef swigCPtr;
	}
}
