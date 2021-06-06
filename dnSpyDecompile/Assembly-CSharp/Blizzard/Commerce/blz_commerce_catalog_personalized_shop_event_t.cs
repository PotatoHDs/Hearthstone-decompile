using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001222 RID: 4642
	public class blz_commerce_catalog_personalized_shop_event_t : IDisposable
	{
		// Token: 0x0600D053 RID: 53331 RVA: 0x003DF888 File Offset: 0x003DDA88
		internal blz_commerce_catalog_personalized_shop_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D054 RID: 53332 RVA: 0x003DF8A4 File Offset: 0x003DDAA4
		internal static HandleRef getCPtr(blz_commerce_catalog_personalized_shop_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D055 RID: 53333 RVA: 0x003DF8BC File Offset: 0x003DDABC
		~blz_commerce_catalog_personalized_shop_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D056 RID: 53334 RVA: 0x003DF8EC File Offset: 0x003DDAEC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D057 RID: 53335 RVA: 0x003DF8FC File Offset: 0x003DDAFC
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_catalog_personalized_shop_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x0600D059 RID: 53337 RVA: 0x003DF988 File Offset: 0x003DDB88
		// (set) Token: 0x0600D058 RID: 53336 RVA: 0x003DF974 File Offset: 0x003DDB74
		public blz_commerce_http_enabled_event_t http_result
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_catalog_personalized_shop_event_t_http_result_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_http_enabled_event_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_catalog_personalized_shop_event_t_http_result_set(this.swigCPtr, blz_commerce_http_enabled_event_t.getCPtr(value));
			}
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x0600D05B RID: 53339 RVA: 0x003DF9C5 File Offset: 0x003DDBC5
		// (set) Token: 0x0600D05A RID: 53338 RVA: 0x003DF9B7 File Offset: 0x003DDBB7
		public string response
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_catalog_personalized_shop_event_t_response_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_catalog_personalized_shop_event_t_response_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D05C RID: 53340 RVA: 0x003DF9D2 File Offset: 0x003DDBD2
		public blz_commerce_catalog_personalized_shop_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_catalog_personalized_shop_event_t(), true)
		{
		}

		// Token: 0x0400A26F RID: 41583
		private HandleRef swigCPtr;

		// Token: 0x0400A270 RID: 41584
		protected bool swigCMemOwn;
	}
}
