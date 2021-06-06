using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class SWIGTYPE_p_f_p_blz_commerce_sdk_t__void
	{
		private HandleRef swigCPtr;

		internal SWIGTYPE_p_f_p_blz_commerce_sdk_t__void(IntPtr cPtr, bool futureUse)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}

		protected SWIGTYPE_p_f_p_blz_commerce_sdk_t__void()
		{
			swigCPtr = new HandleRef(null, IntPtr.Zero);
		}

		internal static HandleRef getCPtr(SWIGTYPE_p_f_p_blz_commerce_sdk_t__void obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	}
}
