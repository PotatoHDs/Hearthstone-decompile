using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001228 RID: 4648
	public class blz_commerce_purchase_t : IDisposable
	{
		// Token: 0x0600D091 RID: 53393 RVA: 0x003DFF6A File Offset: 0x003DE16A
		internal blz_commerce_purchase_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D092 RID: 53394 RVA: 0x003DFF86 File Offset: 0x003DE186
		internal static HandleRef getCPtr(blz_commerce_purchase_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D093 RID: 53395 RVA: 0x003DFFA0 File Offset: 0x003DE1A0
		~blz_commerce_purchase_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D094 RID: 53396 RVA: 0x003DFFD0 File Offset: 0x003DE1D0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D095 RID: 53397 RVA: 0x003DFFE0 File Offset: 0x003DE1E0
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_purchase_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x0600D097 RID: 53399 RVA: 0x003E0066 File Offset: 0x003DE266
		// (set) Token: 0x0600D096 RID: 53398 RVA: 0x003E0058 File Offset: 0x003DE258
		public string product_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_purchase_t_product_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_t_product_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x0600D099 RID: 53401 RVA: 0x003E0081 File Offset: 0x003DE281
		// (set) Token: 0x0600D098 RID: 53400 RVA: 0x003E0073 File Offset: 0x003DE273
		public string currency_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_purchase_t_currency_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_t_currency_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x0600D09B RID: 53403 RVA: 0x003E00A4 File Offset: 0x003DE2A4
		// (set) Token: 0x0600D09A RID: 53402 RVA: 0x003E008E File Offset: 0x003DE28E
		public blz_commerce_browser_purchase_t browser_purchase
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_purchase_t_browser_purchase_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_browser_purchase_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_t_browser_purchase_set(this.swigCPtr, blz_commerce_browser_purchase_t.getCPtr(value));
			}
		}

		// Token: 0x0600D09C RID: 53404 RVA: 0x003E00D3 File Offset: 0x003DE2D3
		public blz_commerce_purchase_t() : this(battlenet_commercePINVOKE.new_blz_commerce_purchase_t(), true)
		{
		}

		// Token: 0x0400A27E RID: 41598
		private HandleRef swigCPtr;

		// Token: 0x0400A27F RID: 41599
		protected bool swigCMemOwn;
	}
}
