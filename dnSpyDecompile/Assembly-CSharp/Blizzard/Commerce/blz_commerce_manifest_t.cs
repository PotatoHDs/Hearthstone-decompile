using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001233 RID: 4659
	public class blz_commerce_manifest_t : IDisposable
	{
		// Token: 0x0600D0F9 RID: 53497 RVA: 0x003E0B15 File Offset: 0x003DED15
		internal blz_commerce_manifest_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D0FA RID: 53498 RVA: 0x003E0B31 File Offset: 0x003DED31
		internal static HandleRef getCPtr(blz_commerce_manifest_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D0FB RID: 53499 RVA: 0x003E0B48 File Offset: 0x003DED48
		~blz_commerce_manifest_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D0FC RID: 53500 RVA: 0x003E0B78 File Offset: 0x003DED78
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D0FD RID: 53501 RVA: 0x003E0B88 File Offset: 0x003DED88
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_manifest_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x0600D0FF RID: 53503 RVA: 0x003E0C14 File Offset: 0x003DEE14
		// (set) Token: 0x0600D0FE RID: 53502 RVA: 0x003E0C00 File Offset: 0x003DEE00
		public SWIGTYPE_p_f_p_blz_commerce_sdk_t_a___q_const__blz_commerce_pair_t_uint32_t__blz_commerce_result_t config
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_config_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new SWIGTYPE_p_f_p_blz_commerce_sdk_t_a___q_const__blz_commerce_pair_t_uint32_t__blz_commerce_result_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_config_set(this.swigCPtr, SWIGTYPE_p_f_p_blz_commerce_sdk_t_a___q_const__blz_commerce_pair_t_uint32_t__blz_commerce_result_t.getCPtr(value));
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x0600D101 RID: 53505 RVA: 0x003E0C58 File Offset: 0x003DEE58
		// (set) Token: 0x0600D100 RID: 53504 RVA: 0x003E0C43 File Offset: 0x003DEE43
		public SWIGTYPE_p_f_p_blz_commerce_sdk_t_a___q_const__blz_commerce_pair_t_uint32_t__blz_commerce_result_t post_init
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_post_init_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new SWIGTYPE_p_f_p_blz_commerce_sdk_t_a___q_const__blz_commerce_pair_t_uint32_t__blz_commerce_result_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_post_init_set(this.swigCPtr, SWIGTYPE_p_f_p_blz_commerce_sdk_t_a___q_const__blz_commerce_pair_t_uint32_t__blz_commerce_result_t.getCPtr(value));
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x0600D103 RID: 53507 RVA: 0x003E0C9C File Offset: 0x003DEE9C
		// (set) Token: 0x0600D102 RID: 53506 RVA: 0x003E0C87 File Offset: 0x003DEE87
		public SWIGTYPE_p_f_p_blz_commerce_sdk_t__void terminate
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_terminate_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new SWIGTYPE_p_f_p_blz_commerce_sdk_t__void(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_terminate_set(this.swigCPtr, SWIGTYPE_p_f_p_blz_commerce_sdk_t__void.getCPtr(value));
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x0600D105 RID: 53509 RVA: 0x003E0CE0 File Offset: 0x003DEEE0
		// (set) Token: 0x0600D104 RID: 53508 RVA: 0x003E0CCB File Offset: 0x003DEECB
		public SWIGTYPE_p_f_p_blz_commerce_sdk_t__void update
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_update_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new SWIGTYPE_p_f_p_blz_commerce_sdk_t__void(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_update_set(this.swigCPtr, SWIGTYPE_p_f_p_blz_commerce_sdk_t__void.getCPtr(value));
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x0600D107 RID: 53511 RVA: 0x003E0D24 File Offset: 0x003DEF24
		// (set) Token: 0x0600D106 RID: 53510 RVA: 0x003E0D0F File Offset: 0x003DEF0F
		public SWIGTYPE_p_f___p_char get_name
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_get_name_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new SWIGTYPE_p_f___p_char(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_get_name_set(this.swigCPtr, SWIGTYPE_p_f___p_char.getCPtr(value));
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x0600D109 RID: 53513 RVA: 0x003E0D68 File Offset: 0x003DEF68
		// (set) Token: 0x0600D108 RID: 53512 RVA: 0x003E0D53 File Offset: 0x003DEF53
		public SWIGTYPE_p_f___p_char get_scopes
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_get_scopes_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new SWIGTYPE_p_f___p_char(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_get_scopes_set(this.swigCPtr, SWIGTYPE_p_f___p_char.getCPtr(value));
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x0600D10B RID: 53515 RVA: 0x003E0DAC File Offset: 0x003DEFAC
		// (set) Token: 0x0600D10A RID: 53514 RVA: 0x003E0D97 File Offset: 0x003DEF97
		public blz_commerce_manifest_t dependencies
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_dependencies_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_manifest_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_dependencies_set(this.swigCPtr, blz_commerce_manifest_t.getCPtr(value));
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x0600D10D RID: 53517 RVA: 0x003E0DE9 File Offset: 0x003DEFE9
		// (set) Token: 0x0600D10C RID: 53516 RVA: 0x003E0DDB File Offset: 0x003DEFDB
		public uint dependency_count
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_manifest_t_dependency_count_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_dependency_count_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D10E RID: 53518 RVA: 0x003E0DF6 File Offset: 0x003DEFF6
		public blz_commerce_manifest_t() : this(battlenet_commercePINVOKE.new_blz_commerce_manifest_t(), true)
		{
		}

		// Token: 0x0400A299 RID: 41625
		private HandleRef swigCPtr;

		// Token: 0x0400A29A RID: 41626
		protected bool swigCMemOwn;
	}
}
