using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200124C RID: 4684
	public class blz_commerce_key_input_t : IDisposable
	{
		// Token: 0x0600D1C8 RID: 53704 RVA: 0x003E24DD File Offset: 0x003E06DD
		internal blz_commerce_key_input_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D1C9 RID: 53705 RVA: 0x003E24F9 File Offset: 0x003E06F9
		internal static HandleRef getCPtr(blz_commerce_key_input_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D1CA RID: 53706 RVA: 0x003E2510 File Offset: 0x003E0710
		~blz_commerce_key_input_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D1CB RID: 53707 RVA: 0x003E2540 File Offset: 0x003E0740
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D1CC RID: 53708 RVA: 0x003E2550 File Offset: 0x003E0750
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_key_input_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x0600D1CE RID: 53710 RVA: 0x003E25D6 File Offset: 0x003E07D6
		// (set) Token: 0x0600D1CD RID: 53709 RVA: 0x003E25C8 File Offset: 0x003E07C8
		public int keyCode
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_key_input_t_keyCode_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_key_input_t_keyCode_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x0600D1D0 RID: 53712 RVA: 0x003E25F1 File Offset: 0x003E07F1
		// (set) Token: 0x0600D1CF RID: 53711 RVA: 0x003E25E3 File Offset: 0x003E07E3
		public blz_commerce_scene_input_type_t type
		{
			get
			{
				return (blz_commerce_scene_input_type_t)battlenet_commercePINVOKE.blz_commerce_key_input_t_type_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_key_input_t_type_set(this.swigCPtr, (int)value);
			}
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x0600D1D2 RID: 53714 RVA: 0x003E260C File Offset: 0x003E080C
		// (set) Token: 0x0600D1D1 RID: 53713 RVA: 0x003E25FE File Offset: 0x003E07FE
		public int character
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_key_input_t_character_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_key_input_t_character_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x0600D1D4 RID: 53716 RVA: 0x003E2627 File Offset: 0x003E0827
		// (set) Token: 0x0600D1D3 RID: 53715 RVA: 0x003E2619 File Offset: 0x003E0819
		public uint modifiers
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_key_input_t_modifiers_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_key_input_t_modifiers_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D1D5 RID: 53717 RVA: 0x003E2634 File Offset: 0x003E0834
		public blz_commerce_key_input_t() : this(battlenet_commercePINVOKE.new_blz_commerce_key_input_t(), true)
		{
		}

		// Token: 0x0400A2F6 RID: 41718
		private HandleRef swigCPtr;

		// Token: 0x0400A2F7 RID: 41719
		protected bool swigCMemOwn;
	}
}
