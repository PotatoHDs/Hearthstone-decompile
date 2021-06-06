using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001221 RID: 4641
	public class blz_commerce_catalog_product_load_event_t : IDisposable
	{
		// Token: 0x0600D049 RID: 53321 RVA: 0x003DF730 File Offset: 0x003DD930
		internal blz_commerce_catalog_product_load_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D04A RID: 53322 RVA: 0x003DF74C File Offset: 0x003DD94C
		internal static HandleRef getCPtr(blz_commerce_catalog_product_load_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D04B RID: 53323 RVA: 0x003DF764 File Offset: 0x003DD964
		~blz_commerce_catalog_product_load_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D04C RID: 53324 RVA: 0x003DF794 File Offset: 0x003DD994
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D04D RID: 53325 RVA: 0x003DF7A4 File Offset: 0x003DD9A4
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_catalog_product_load_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x0600D04F RID: 53327 RVA: 0x003DF830 File Offset: 0x003DDA30
		// (set) Token: 0x0600D04E RID: 53326 RVA: 0x003DF81C File Offset: 0x003DDA1C
		public blz_commerce_http_enabled_event_t http_result
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_catalog_product_load_event_t_http_result_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_http_enabled_event_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_catalog_product_load_event_t_http_result_set(this.swigCPtr, blz_commerce_http_enabled_event_t.getCPtr(value));
			}
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x0600D051 RID: 53329 RVA: 0x003DF86D File Offset: 0x003DDA6D
		// (set) Token: 0x0600D050 RID: 53328 RVA: 0x003DF85F File Offset: 0x003DDA5F
		public string response
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_catalog_product_load_event_t_response_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_catalog_product_load_event_t_response_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D052 RID: 53330 RVA: 0x003DF87A File Offset: 0x003DDA7A
		public blz_commerce_catalog_product_load_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_catalog_product_load_event_t(), true)
		{
		}

		// Token: 0x0400A26D RID: 41581
		private HandleRef swigCPtr;

		// Token: 0x0400A26E RID: 41582
		protected bool swigCMemOwn;
	}
}
