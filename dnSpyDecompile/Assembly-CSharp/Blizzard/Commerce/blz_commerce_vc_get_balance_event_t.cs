using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001238 RID: 4664
	public class blz_commerce_vc_get_balance_event_t : IDisposable
	{
		// Token: 0x0600D119 RID: 53529 RVA: 0x003E0F34 File Offset: 0x003DF134
		internal blz_commerce_vc_get_balance_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D11A RID: 53530 RVA: 0x003E0F50 File Offset: 0x003DF150
		internal static HandleRef getCPtr(blz_commerce_vc_get_balance_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D11B RID: 53531 RVA: 0x003E0F68 File Offset: 0x003DF168
		~blz_commerce_vc_get_balance_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D11C RID: 53532 RVA: 0x003E0F98 File Offset: 0x003DF198
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D11D RID: 53533 RVA: 0x003E0FA8 File Offset: 0x003DF1A8
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_vc_get_balance_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x0600D11F RID: 53535 RVA: 0x003E1034 File Offset: 0x003DF234
		// (set) Token: 0x0600D11E RID: 53534 RVA: 0x003E1020 File Offset: 0x003DF220
		public blz_commerce_http_enabled_event_t http_result
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_vc_get_balance_event_t_http_result_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_http_enabled_event_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_get_balance_event_t_http_result_set(this.swigCPtr, blz_commerce_http_enabled_event_t.getCPtr(value));
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x0600D121 RID: 53537 RVA: 0x003E1071 File Offset: 0x003DF271
		// (set) Token: 0x0600D120 RID: 53536 RVA: 0x003E1063 File Offset: 0x003DF263
		public string response
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_get_balance_event_t_response_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_get_balance_event_t_response_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D122 RID: 53538 RVA: 0x003E107E File Offset: 0x003DF27E
		public blz_commerce_vc_get_balance_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_vc_get_balance_event_t(), true)
		{
		}

		// Token: 0x0400A2AD RID: 41645
		private HandleRef swigCPtr;

		// Token: 0x0400A2AE RID: 41646
		protected bool swigCMemOwn;
	}
}
