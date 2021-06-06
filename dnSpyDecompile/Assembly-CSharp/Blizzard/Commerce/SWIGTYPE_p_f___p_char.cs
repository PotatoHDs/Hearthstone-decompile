using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001261 RID: 4705
	public class SWIGTYPE_p_f___p_char
	{
		// Token: 0x0600D3ED RID: 54253 RVA: 0x003E35D7 File Offset: 0x003E17D7
		internal SWIGTYPE_p_f___p_char(IntPtr cPtr, bool futureUse)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D3EE RID: 54254 RVA: 0x003E35EC File Offset: 0x003E17EC
		protected SWIGTYPE_p_f___p_char()
		{
			this.swigCPtr = new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D3EF RID: 54255 RVA: 0x003E3605 File Offset: 0x003E1805
		internal static HandleRef getCPtr(SWIGTYPE_p_f___p_char obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0400A32C RID: 41772
		private HandleRef swigCPtr;
	}
}
