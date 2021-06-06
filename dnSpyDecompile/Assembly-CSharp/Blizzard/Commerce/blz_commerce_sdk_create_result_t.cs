using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001230 RID: 4656
	public class blz_commerce_sdk_create_result_t : IDisposable
	{
		// Token: 0x0600D0DB RID: 53467 RVA: 0x003E0777 File Offset: 0x003DE977
		internal blz_commerce_sdk_create_result_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D0DC RID: 53468 RVA: 0x003E0793 File Offset: 0x003DE993
		internal static HandleRef getCPtr(blz_commerce_sdk_create_result_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D0DD RID: 53469 RVA: 0x003E07AC File Offset: 0x003DE9AC
		~blz_commerce_sdk_create_result_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D0DE RID: 53470 RVA: 0x003E07DC File Offset: 0x003DE9DC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D0DF RID: 53471 RVA: 0x003E07EC File Offset: 0x003DE9EC
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_sdk_create_result_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x0600D0E1 RID: 53473 RVA: 0x003E0872 File Offset: 0x003DEA72
		// (set) Token: 0x0600D0E0 RID: 53472 RVA: 0x003E0864 File Offset: 0x003DEA64
		public blz_commerce_result_state_t state
		{
			get
			{
				return (blz_commerce_result_state_t)battlenet_commercePINVOKE.blz_commerce_sdk_create_result_t_state_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_sdk_create_result_t_state_set(this.swigCPtr, (int)value);
			}
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x0600D0E3 RID: 53475 RVA: 0x003E0894 File Offset: 0x003DEA94
		// (set) Token: 0x0600D0E2 RID: 53474 RVA: 0x003E087F File Offset: 0x003DEA7F
		public blz_commerce_sdk_t sdk
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_sdk_create_result_t_sdk_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_sdk_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_sdk_create_result_t_sdk_set(this.swigCPtr, blz_commerce_sdk_t.getCPtr(value));
			}
		}

		// Token: 0x0600D0E4 RID: 53476 RVA: 0x003E08C3 File Offset: 0x003DEAC3
		public blz_commerce_sdk_create_result_t() : this(battlenet_commercePINVOKE.new_blz_commerce_sdk_create_result_t(), true)
		{
		}

		// Token: 0x0400A293 RID: 41619
		private HandleRef swigCPtr;

		// Token: 0x0400A294 RID: 41620
		protected bool swigCMemOwn;
	}
}
