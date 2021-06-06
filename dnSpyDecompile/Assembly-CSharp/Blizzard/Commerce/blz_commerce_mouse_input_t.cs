using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200124E RID: 4686
	public class blz_commerce_mouse_input_t : IDisposable
	{
		// Token: 0x0600D1D6 RID: 53718 RVA: 0x003E2642 File Offset: 0x003E0842
		internal blz_commerce_mouse_input_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D1D7 RID: 53719 RVA: 0x003E265E File Offset: 0x003E085E
		internal static HandleRef getCPtr(blz_commerce_mouse_input_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D1D8 RID: 53720 RVA: 0x003E2678 File Offset: 0x003E0878
		~blz_commerce_mouse_input_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D1D9 RID: 53721 RVA: 0x003E26A8 File Offset: 0x003E08A8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D1DA RID: 53722 RVA: 0x003E26B8 File Offset: 0x003E08B8
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_mouse_input_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x0600D1DC RID: 53724 RVA: 0x003E273E File Offset: 0x003E093E
		// (set) Token: 0x0600D1DB RID: 53723 RVA: 0x003E2730 File Offset: 0x003E0930
		public blz_commerce_scene_mouse_button_t button
		{
			get
			{
				return (blz_commerce_scene_mouse_button_t)battlenet_commercePINVOKE.blz_commerce_mouse_input_t_button_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_mouse_input_t_button_set(this.swigCPtr, (int)value);
			}
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x0600D1DE RID: 53726 RVA: 0x003E2759 File Offset: 0x003E0959
		// (set) Token: 0x0600D1DD RID: 53725 RVA: 0x003E274B File Offset: 0x003E094B
		public blz_commerce_scene_input_type_t type
		{
			get
			{
				return (blz_commerce_scene_input_type_t)battlenet_commercePINVOKE.blz_commerce_mouse_input_t_type_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_mouse_input_t_type_set(this.swigCPtr, (int)value);
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x0600D1E0 RID: 53728 RVA: 0x003E277C File Offset: 0x003E097C
		// (set) Token: 0x0600D1DF RID: 53727 RVA: 0x003E2766 File Offset: 0x003E0966
		public blz_commerce_vec2d_t coords
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_mouse_input_t_coords_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_vec2d_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_mouse_input_t_coords_set(this.swigCPtr, blz_commerce_vec2d_t.getCPtr(value));
			}
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x0600D1E2 RID: 53730 RVA: 0x003E27B9 File Offset: 0x003E09B9
		// (set) Token: 0x0600D1E1 RID: 53729 RVA: 0x003E27AB File Offset: 0x003E09AB
		public uint mods
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_mouse_input_t_mods_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_mouse_input_t_mods_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D1E3 RID: 53731 RVA: 0x003E27C6 File Offset: 0x003E09C6
		public blz_commerce_mouse_input_t() : this(battlenet_commercePINVOKE.new_blz_commerce_mouse_input_t(), true)
		{
		}

		// Token: 0x0400A2FC RID: 41724
		private HandleRef swigCPtr;

		// Token: 0x0400A2FD RID: 41725
		protected bool swigCMemOwn;
	}
}
