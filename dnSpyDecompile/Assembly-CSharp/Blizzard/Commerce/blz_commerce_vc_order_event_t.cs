using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001239 RID: 4665
	public class blz_commerce_vc_order_event_t : IDisposable
	{
		// Token: 0x0600D123 RID: 53539 RVA: 0x003E108C File Offset: 0x003DF28C
		internal blz_commerce_vc_order_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D124 RID: 53540 RVA: 0x003E10A8 File Offset: 0x003DF2A8
		internal static HandleRef getCPtr(blz_commerce_vc_order_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D125 RID: 53541 RVA: 0x003E10C0 File Offset: 0x003DF2C0
		~blz_commerce_vc_order_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D126 RID: 53542 RVA: 0x003E10F0 File Offset: 0x003DF2F0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D127 RID: 53543 RVA: 0x003E1100 File Offset: 0x003DF300
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_vc_order_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x0600D129 RID: 53545 RVA: 0x003E118C File Offset: 0x003DF38C
		// (set) Token: 0x0600D128 RID: 53544 RVA: 0x003E1178 File Offset: 0x003DF378
		public blz_commerce_http_enabled_event_t http_result
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_vc_order_event_t_http_result_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_http_enabled_event_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_order_event_t_http_result_set(this.swigCPtr, blz_commerce_http_enabled_event_t.getCPtr(value));
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x0600D12B RID: 53547 RVA: 0x003E11C9 File Offset: 0x003DF3C9
		// (set) Token: 0x0600D12A RID: 53546 RVA: 0x003E11BB File Offset: 0x003DF3BB
		public string response
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_order_event_t_response_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_order_event_t_response_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D12C RID: 53548 RVA: 0x003E11D6 File Offset: 0x003DF3D6
		public blz_commerce_vc_order_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_vc_order_event_t(), true)
		{
		}

		// Token: 0x0400A2AF RID: 41647
		private HandleRef swigCPtr;

		// Token: 0x0400A2B0 RID: 41648
		protected bool swigCMemOwn;
	}
}
