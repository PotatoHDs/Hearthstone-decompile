using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001249 RID: 4681
	public class blz_commerce_scene_window_resize_event_t : IDisposable
	{
		// Token: 0x0600D1AE RID: 53678 RVA: 0x003E2135 File Offset: 0x003E0335
		internal blz_commerce_scene_window_resize_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D1AF RID: 53679 RVA: 0x003E2151 File Offset: 0x003E0351
		internal static HandleRef getCPtr(blz_commerce_scene_window_resize_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D1B0 RID: 53680 RVA: 0x003E2168 File Offset: 0x003E0368
		~blz_commerce_scene_window_resize_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D1B1 RID: 53681 RVA: 0x003E2198 File Offset: 0x003E0398
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D1B2 RID: 53682 RVA: 0x003E21A8 File Offset: 0x003E03A8
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_scene_window_resize_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x0600D1B4 RID: 53684 RVA: 0x003E2234 File Offset: 0x003E0434
		// (set) Token: 0x0600D1B3 RID: 53683 RVA: 0x003E2220 File Offset: 0x003E0420
		public blz_commerce_vec2d_t win_size
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_scene_window_resize_event_t_win_size_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_vec2d_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_window_resize_event_t_win_size_set(this.swigCPtr, blz_commerce_vec2d_t.getCPtr(value));
			}
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x0600D1B6 RID: 53686 RVA: 0x003E2271 File Offset: 0x003E0471
		// (set) Token: 0x0600D1B5 RID: 53685 RVA: 0x003E2263 File Offset: 0x003E0463
		public uint buffer_size
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_scene_window_resize_event_t_buffer_size_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_window_resize_event_t_buffer_size_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D1B7 RID: 53687 RVA: 0x003E227E File Offset: 0x003E047E
		public blz_commerce_scene_window_resize_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_scene_window_resize_event_t(), true)
		{
		}

		// Token: 0x0400A2F0 RID: 41712
		private HandleRef swigCPtr;

		// Token: 0x0400A2F1 RID: 41713
		protected bool swigCMemOwn;
	}
}
