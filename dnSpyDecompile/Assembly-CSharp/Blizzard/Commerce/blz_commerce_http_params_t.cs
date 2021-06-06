using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001259 RID: 4697
	public class blz_commerce_http_params_t : IDisposable
	{
		// Token: 0x0600D230 RID: 53808 RVA: 0x003E30C6 File Offset: 0x003E12C6
		internal blz_commerce_http_params_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D231 RID: 53809 RVA: 0x003E30E2 File Offset: 0x003E12E2
		internal static HandleRef getCPtr(blz_commerce_http_params_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D232 RID: 53810 RVA: 0x003E30FC File Offset: 0x003E12FC
		~blz_commerce_http_params_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D233 RID: 53811 RVA: 0x003E312C File Offset: 0x003E132C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D234 RID: 53812 RVA: 0x003E313C File Offset: 0x003E133C
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_http_params_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x0600D236 RID: 53814 RVA: 0x003E31C2 File Offset: 0x003E13C2
		// (set) Token: 0x0600D235 RID: 53813 RVA: 0x003E31B4 File Offset: 0x003E13B4
		public string client_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_http_params_t_client_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_http_params_t_client_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x0600D238 RID: 53816 RVA: 0x003E31DD File Offset: 0x003E13DD
		// (set) Token: 0x0600D237 RID: 53815 RVA: 0x003E31CF File Offset: 0x003E13CF
		public string token
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_http_params_t_token_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_http_params_t_token_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x0600D23A RID: 53818 RVA: 0x003E31F8 File Offset: 0x003E13F8
		// (set) Token: 0x0600D239 RID: 53817 RVA: 0x003E31EA File Offset: 0x003E13EA
		public string title_code
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_http_params_t_title_code_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_http_params_t_title_code_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x0600D23C RID: 53820 RVA: 0x003E3213 File Offset: 0x003E1413
		// (set) Token: 0x0600D23B RID: 53819 RVA: 0x003E3205 File Offset: 0x003E1405
		public string title_version
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_http_params_t_title_version_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_http_params_t_title_version_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x0600D23E RID: 53822 RVA: 0x003E322E File Offset: 0x003E142E
		// (set) Token: 0x0600D23D RID: 53821 RVA: 0x003E3220 File Offset: 0x003E1420
		public int region
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_http_params_t_region_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_http_params_t_region_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x0600D240 RID: 53824 RVA: 0x003E3249 File Offset: 0x003E1449
		// (set) Token: 0x0600D23F RID: 53823 RVA: 0x003E323B File Offset: 0x003E143B
		public blz_commerce_http_environment_t environment
		{
			get
			{
				return (blz_commerce_http_environment_t)battlenet_commercePINVOKE.blz_commerce_http_params_t_environment_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_http_params_t_environment_set(this.swigCPtr, (int)value);
			}
		}

		// Token: 0x0600D241 RID: 53825 RVA: 0x003E3256 File Offset: 0x003E1456
		public blz_commerce_http_params_t() : this(battlenet_commercePINVOKE.new_blz_commerce_http_params_t(), true)
		{
		}

		// Token: 0x0400A31B RID: 41755
		private HandleRef swigCPtr;

		// Token: 0x0400A31C RID: 41756
		protected bool swigCMemOwn;
	}
}
