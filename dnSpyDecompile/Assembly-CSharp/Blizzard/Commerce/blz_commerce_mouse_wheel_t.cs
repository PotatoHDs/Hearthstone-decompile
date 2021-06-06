using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001251 RID: 4689
	public class blz_commerce_mouse_wheel_t : IDisposable
	{
		// Token: 0x0600D1F8 RID: 53752 RVA: 0x003E2A5C File Offset: 0x003E0C5C
		internal blz_commerce_mouse_wheel_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D1F9 RID: 53753 RVA: 0x003E2A78 File Offset: 0x003E0C78
		internal static HandleRef getCPtr(blz_commerce_mouse_wheel_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D1FA RID: 53754 RVA: 0x003E2A90 File Offset: 0x003E0C90
		~blz_commerce_mouse_wheel_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D1FB RID: 53755 RVA: 0x003E2AC0 File Offset: 0x003E0CC0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D1FC RID: 53756 RVA: 0x003E2AD0 File Offset: 0x003E0CD0
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_mouse_wheel_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x0600D1FE RID: 53758 RVA: 0x003E2B56 File Offset: 0x003E0D56
		// (set) Token: 0x0600D1FD RID: 53757 RVA: 0x003E2B48 File Offset: 0x003E0D48
		public int delta
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_mouse_wheel_t_delta_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_mouse_wheel_t_delta_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x0600D200 RID: 53760 RVA: 0x003E2B78 File Offset: 0x003E0D78
		// (set) Token: 0x0600D1FF RID: 53759 RVA: 0x003E2B63 File Offset: 0x003E0D63
		public blz_commerce_vec2d_t coords
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_mouse_wheel_t_coords_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_vec2d_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_mouse_wheel_t_coords_set(this.swigCPtr, blz_commerce_vec2d_t.getCPtr(value));
			}
		}

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x0600D202 RID: 53762 RVA: 0x003E2BB5 File Offset: 0x003E0DB5
		// (set) Token: 0x0600D201 RID: 53761 RVA: 0x003E2BA7 File Offset: 0x003E0DA7
		public uint mod
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_mouse_wheel_t_mod_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_mouse_wheel_t_mod_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D203 RID: 53763 RVA: 0x003E2BC2 File Offset: 0x003E0DC2
		public blz_commerce_mouse_wheel_t() : this(battlenet_commercePINVOKE.new_blz_commerce_mouse_wheel_t(), true)
		{
		}

		// Token: 0x0400A302 RID: 41730
		private HandleRef swigCPtr;

		// Token: 0x0400A303 RID: 41731
		protected bool swigCMemOwn;
	}
}
