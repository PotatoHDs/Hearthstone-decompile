using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001254 RID: 4692
	public class blz_commerce_browser_params_t : IDisposable
	{
		// Token: 0x0600D20C RID: 53772 RVA: 0x003E2CE5 File Offset: 0x003E0EE5
		internal blz_commerce_browser_params_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D20D RID: 53773 RVA: 0x003E2D01 File Offset: 0x003E0F01
		internal static HandleRef getCPtr(blz_commerce_browser_params_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D20E RID: 53774 RVA: 0x003E2D18 File Offset: 0x003E0F18
		~blz_commerce_browser_params_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D20F RID: 53775 RVA: 0x003E2D48 File Offset: 0x003E0F48
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D210 RID: 53776 RVA: 0x003E2D58 File Offset: 0x003E0F58
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_browser_params_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x0600D212 RID: 53778 RVA: 0x003E2DDE File Offset: 0x003E0FDE
		// (set) Token: 0x0600D211 RID: 53777 RVA: 0x003E2DD0 File Offset: 0x003E0FD0
		public int window_width
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_window_width_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_window_width_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x0600D214 RID: 53780 RVA: 0x003E2DF9 File Offset: 0x003E0FF9
		// (set) Token: 0x0600D213 RID: 53779 RVA: 0x003E2DEB File Offset: 0x003E0FEB
		public int window_height
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_window_height_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_window_height_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x0600D216 RID: 53782 RVA: 0x003E2E14 File Offset: 0x003E1014
		// (set) Token: 0x0600D215 RID: 53781 RVA: 0x003E2E06 File Offset: 0x003E1006
		public int max_window_width
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_max_window_width_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_max_window_width_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x0600D218 RID: 53784 RVA: 0x003E2E2F File Offset: 0x003E102F
		// (set) Token: 0x0600D217 RID: 53783 RVA: 0x003E2E21 File Offset: 0x003E1021
		public int max_window_height
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_max_window_height_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_max_window_height_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x0600D21A RID: 53786 RVA: 0x003E2E4A File Offset: 0x003E104A
		// (set) Token: 0x0600D219 RID: 53785 RVA: 0x003E2E3C File Offset: 0x003E103C
		public string log_directory
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_log_directory_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_log_directory_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x0600D21C RID: 53788 RVA: 0x003E2E65 File Offset: 0x003E1065
		// (set) Token: 0x0600D21B RID: 53787 RVA: 0x003E2E57 File Offset: 0x003E1057
		public string browser_directory
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_browser_directory_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_browser_directory_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x0600D21E RID: 53790 RVA: 0x003E2E80 File Offset: 0x003E1080
		// (set) Token: 0x0600D21D RID: 53789 RVA: 0x003E2E72 File Offset: 0x003E1072
		public bool is_prod
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_is_prod_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_is_prod_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D21F RID: 53791 RVA: 0x003E2E8D File Offset: 0x003E108D
		public blz_commerce_browser_params_t() : this(battlenet_commercePINVOKE.new_blz_commerce_browser_params_t(), true)
		{
		}

		// Token: 0x0400A30E RID: 41742
		private HandleRef swigCPtr;

		// Token: 0x0400A30F RID: 41743
		protected bool swigCMemOwn;
	}
}
