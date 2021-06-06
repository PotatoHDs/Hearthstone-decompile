using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200122A RID: 4650
	public class blz_commerce_sdk_t : IDisposable
	{
		// Token: 0x0600D0B1 RID: 53425 RVA: 0x003E0297 File Offset: 0x003DE497
		internal blz_commerce_sdk_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D0B2 RID: 53426 RVA: 0x003E02B3 File Offset: 0x003DE4B3
		internal static HandleRef getCPtr(blz_commerce_sdk_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D0B3 RID: 53427 RVA: 0x003E02CC File Offset: 0x003DE4CC
		~blz_commerce_sdk_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D0B4 RID: 53428 RVA: 0x003E02FC File Offset: 0x003DE4FC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D0B5 RID: 53429 RVA: 0x003E030C File Offset: 0x003DE50C
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_sdk_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x0600D0B6 RID: 53430 RVA: 0x003E0384 File Offset: 0x003DE584
		public blz_commerce_sdk_t() : this(battlenet_commercePINVOKE.new_blz_commerce_sdk_t(), true)
		{
		}

		// Token: 0x0400A282 RID: 41602
		private HandleRef swigCPtr;

		// Token: 0x0400A283 RID: 41603
		protected bool swigCMemOwn;
	}
}
