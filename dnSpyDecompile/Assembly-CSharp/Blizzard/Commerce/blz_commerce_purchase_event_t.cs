using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001226 RID: 4646
	public class blz_commerce_purchase_event_t : IDisposable
	{
		// Token: 0x0600D073 RID: 53363 RVA: 0x003DFC5D File Offset: 0x003DDE5D
		internal blz_commerce_purchase_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D074 RID: 53364 RVA: 0x003DFC79 File Offset: 0x003DDE79
		internal static HandleRef getCPtr(blz_commerce_purchase_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D075 RID: 53365 RVA: 0x003DFC90 File Offset: 0x003DDE90
		~blz_commerce_purchase_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D076 RID: 53366 RVA: 0x003DFCC0 File Offset: 0x003DDEC0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D077 RID: 53367 RVA: 0x003DFCD0 File Offset: 0x003DDED0
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_purchase_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x0600D079 RID: 53369 RVA: 0x003DFD56 File Offset: 0x003DDF56
		// (set) Token: 0x0600D078 RID: 53368 RVA: 0x003DFD48 File Offset: 0x003DDF48
		public string product_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_purchase_event_t_product_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_event_t_product_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x0600D07B RID: 53371 RVA: 0x003DFD71 File Offset: 0x003DDF71
		// (set) Token: 0x0600D07A RID: 53370 RVA: 0x003DFD63 File Offset: 0x003DDF63
		public string transaction_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_purchase_event_t_transaction_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_event_t_transaction_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x0600D07D RID: 53373 RVA: 0x003DFD8C File Offset: 0x003DDF8C
		// (set) Token: 0x0600D07C RID: 53372 RVA: 0x003DFD7E File Offset: 0x003DDF7E
		public blz_commerce_purchase_status_t status
		{
			get
			{
				return (blz_commerce_purchase_status_t)battlenet_commercePINVOKE.blz_commerce_purchase_event_t_status_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_event_t_status_set(this.swigCPtr, (int)value);
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x0600D07F RID: 53375 RVA: 0x003DFDA7 File Offset: 0x003DDFA7
		// (set) Token: 0x0600D07E RID: 53374 RVA: 0x003DFD99 File Offset: 0x003DDF99
		public string error_code
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_purchase_event_t_error_code_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_event_t_error_code_set(this.swigCPtr, value);
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x0600D081 RID: 53377 RVA: 0x003DFDC8 File Offset: 0x003DDFC8
		// (set) Token: 0x0600D080 RID: 53376 RVA: 0x003DFDB4 File Offset: 0x003DDFB4
		public blz_commerce_purchase_browser_info_t browser_info
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_purchase_event_t_browser_info_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_purchase_browser_info_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_event_t_browser_info_set(this.swigCPtr, blz_commerce_purchase_browser_info_t.getCPtr(value));
			}
		}

		// Token: 0x0600D082 RID: 53378 RVA: 0x003DFDF7 File Offset: 0x003DDFF7
		public blz_commerce_purchase_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_purchase_event_t(), true)
		{
		}

		// Token: 0x0400A27A RID: 41594
		private HandleRef swigCPtr;

		// Token: 0x0400A27B RID: 41595
		protected bool swigCMemOwn;
	}
}
