using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200124A RID: 4682
	public class blz_commerce_scene_window_resize_requested_event_t : IDisposable
	{
		// Token: 0x0600D1B8 RID: 53688 RVA: 0x003E228C File Offset: 0x003E048C
		internal blz_commerce_scene_window_resize_requested_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D1B9 RID: 53689 RVA: 0x003E22A8 File Offset: 0x003E04A8
		internal static HandleRef getCPtr(blz_commerce_scene_window_resize_requested_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D1BA RID: 53690 RVA: 0x003E22C0 File Offset: 0x003E04C0
		~blz_commerce_scene_window_resize_requested_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D1BB RID: 53691 RVA: 0x003E22F0 File Offset: 0x003E04F0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D1BC RID: 53692 RVA: 0x003E2300 File Offset: 0x003E0500
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_scene_window_resize_requested_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x0600D1BE RID: 53694 RVA: 0x003E238C File Offset: 0x003E058C
		// (set) Token: 0x0600D1BD RID: 53693 RVA: 0x003E2378 File Offset: 0x003E0578
		public blz_commerce_vec2d_t requested_size
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_scene_window_resize_requested_event_t_requested_size_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_vec2d_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_window_resize_requested_event_t_requested_size_set(this.swigCPtr, blz_commerce_vec2d_t.getCPtr(value));
			}
		}

		// Token: 0x0600D1BF RID: 53695 RVA: 0x003E23BB File Offset: 0x003E05BB
		public blz_commerce_scene_window_resize_requested_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_scene_window_resize_requested_event_t(), true)
		{
		}

		// Token: 0x0400A2F2 RID: 41714
		private HandleRef swigCPtr;

		// Token: 0x0400A2F3 RID: 41715
		protected bool swigCMemOwn;
	}
}
