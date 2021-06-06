using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001250 RID: 4688
	public class blz_commerce_mouse_move_t : IDisposable
	{
		// Token: 0x0600D1EE RID: 53742 RVA: 0x003E2904 File Offset: 0x003E0B04
		internal blz_commerce_mouse_move_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D1EF RID: 53743 RVA: 0x003E2920 File Offset: 0x003E0B20
		internal static HandleRef getCPtr(blz_commerce_mouse_move_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D1F0 RID: 53744 RVA: 0x003E2938 File Offset: 0x003E0B38
		~blz_commerce_mouse_move_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D1F1 RID: 53745 RVA: 0x003E2968 File Offset: 0x003E0B68
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D1F2 RID: 53746 RVA: 0x003E2978 File Offset: 0x003E0B78
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_mouse_move_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x0600D1F4 RID: 53748 RVA: 0x003E2A04 File Offset: 0x003E0C04
		// (set) Token: 0x0600D1F3 RID: 53747 RVA: 0x003E29F0 File Offset: 0x003E0BF0
		public blz_commerce_vec2d_t coords
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_mouse_move_t_coords_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_vec2d_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_mouse_move_t_coords_set(this.swigCPtr, blz_commerce_vec2d_t.getCPtr(value));
			}
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x0600D1F6 RID: 53750 RVA: 0x003E2A41 File Offset: 0x003E0C41
		// (set) Token: 0x0600D1F5 RID: 53749 RVA: 0x003E2A33 File Offset: 0x003E0C33
		public uint mod
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_mouse_move_t_mod_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_mouse_move_t_mod_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D1F7 RID: 53751 RVA: 0x003E2A4E File Offset: 0x003E0C4E
		public blz_commerce_mouse_move_t() : this(battlenet_commercePINVOKE.new_blz_commerce_mouse_move_t(), true)
		{
		}

		// Token: 0x0400A300 RID: 41728
		private HandleRef swigCPtr;

		// Token: 0x0400A301 RID: 41729
		protected bool swigCMemOwn;
	}
}
