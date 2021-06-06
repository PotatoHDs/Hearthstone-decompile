using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001260 RID: 4704
	public class SWIGTYPE_p_f_p_blz_commerce_sdk_t__void
	{
		// Token: 0x0600D3EA RID: 54250 RVA: 0x003E3592 File Offset: 0x003E1792
		internal SWIGTYPE_p_f_p_blz_commerce_sdk_t__void(IntPtr cPtr, bool futureUse)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D3EB RID: 54251 RVA: 0x003E35A7 File Offset: 0x003E17A7
		protected SWIGTYPE_p_f_p_blz_commerce_sdk_t__void()
		{
			this.swigCPtr = new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D3EC RID: 54252 RVA: 0x003E35C0 File Offset: 0x003E17C0
		internal static HandleRef getCPtr(SWIGTYPE_p_f_p_blz_commerce_sdk_t__void obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0400A32B RID: 41771
		private HandleRef swigCPtr;
	}
}
