using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001229 RID: 4649
	public class blz_commerce_checkout_browser_params_t : IDisposable
	{
		// Token: 0x0600D09D RID: 53405 RVA: 0x003E00E1 File Offset: 0x003DE2E1
		internal blz_commerce_checkout_browser_params_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D09E RID: 53406 RVA: 0x003E00FD File Offset: 0x003DE2FD
		internal static HandleRef getCPtr(blz_commerce_checkout_browser_params_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D09F RID: 53407 RVA: 0x003E0114 File Offset: 0x003DE314
		~blz_commerce_checkout_browser_params_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D0A0 RID: 53408 RVA: 0x003E0144 File Offset: 0x003DE344
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D0A1 RID: 53409 RVA: 0x003E0154 File Offset: 0x003DE354
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_checkout_browser_params_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x0600D0A3 RID: 53411 RVA: 0x003E01DA File Offset: 0x003DE3DA
		// (set) Token: 0x0600D0A2 RID: 53410 RVA: 0x003E01CC File Offset: 0x003DE3CC
		public string checkout_url
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_checkout_url_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_checkout_url_set(this.swigCPtr, value);
			}
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x0600D0A5 RID: 53413 RVA: 0x003E01F5 File Offset: 0x003DE3F5
		// (set) Token: 0x0600D0A4 RID: 53412 RVA: 0x003E01E7 File Offset: 0x003DE3E7
		public string title_code
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_title_code_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_title_code_set(this.swigCPtr, value);
			}
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x0600D0A7 RID: 53415 RVA: 0x003E0210 File Offset: 0x003DE410
		// (set) Token: 0x0600D0A6 RID: 53414 RVA: 0x003E0202 File Offset: 0x003DE402
		public string device_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_device_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_device_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x0600D0A9 RID: 53417 RVA: 0x003E022B File Offset: 0x003DE42B
		// (set) Token: 0x0600D0A8 RID: 53416 RVA: 0x003E021D File Offset: 0x003DE41D
		public string title_version
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_title_version_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_title_version_set(this.swigCPtr, value);
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x0600D0AB RID: 53419 RVA: 0x003E0246 File Offset: 0x003DE446
		// (set) Token: 0x0600D0AA RID: 53418 RVA: 0x003E0238 File Offset: 0x003DE438
		public string locale
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_locale_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_locale_set(this.swigCPtr, value);
			}
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x0600D0AD RID: 53421 RVA: 0x003E0261 File Offset: 0x003DE461
		// (set) Token: 0x0600D0AC RID: 53420 RVA: 0x003E0253 File Offset: 0x003DE453
		public string game_service_region
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_game_service_region_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_game_service_region_set(this.swigCPtr, value);
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x0600D0AF RID: 53423 RVA: 0x003E027C File Offset: 0x003DE47C
		// (set) Token: 0x0600D0AE RID: 53422 RVA: 0x003E026E File Offset: 0x003DE46E
		public string game_account_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_game_account_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_checkout_browser_params_t_game_account_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D0B0 RID: 53424 RVA: 0x003E0289 File Offset: 0x003DE489
		public blz_commerce_checkout_browser_params_t() : this(battlenet_commercePINVOKE.new_blz_commerce_checkout_browser_params_t(), true)
		{
		}

		// Token: 0x0400A280 RID: 41600
		private HandleRef swigCPtr;

		// Token: 0x0400A281 RID: 41601
		protected bool swigCMemOwn;
	}
}
