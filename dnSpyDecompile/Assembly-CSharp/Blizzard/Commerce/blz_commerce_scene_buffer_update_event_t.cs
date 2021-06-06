using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001248 RID: 4680
	public class blz_commerce_scene_buffer_update_event_t : IDisposable
	{
		// Token: 0x0600D1A2 RID: 53666 RVA: 0x003E1F98 File Offset: 0x003E0198
		internal blz_commerce_scene_buffer_update_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D1A3 RID: 53667 RVA: 0x003E1FB4 File Offset: 0x003E01B4
		internal static HandleRef getCPtr(blz_commerce_scene_buffer_update_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D1A4 RID: 53668 RVA: 0x003E1FCC File Offset: 0x003E01CC
		~blz_commerce_scene_buffer_update_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D1A5 RID: 53669 RVA: 0x003E1FFC File Offset: 0x003E01FC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D1A6 RID: 53670 RVA: 0x003E200C File Offset: 0x003E020C
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_scene_buffer_update_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x0600D1A8 RID: 53672 RVA: 0x003E2098 File Offset: 0x003E0298
		// (set) Token: 0x0600D1A7 RID: 53671 RVA: 0x003E2084 File Offset: 0x003E0284
		public blz_commerce_scene_rect_t dirty_rects
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_scene_buffer_update_event_t_dirty_rects_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_scene_rect_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_buffer_update_event_t_dirty_rects_set(this.swigCPtr, blz_commerce_scene_rect_t.getCPtr(value));
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x0600D1AA RID: 53674 RVA: 0x003E20D5 File Offset: 0x003E02D5
		// (set) Token: 0x0600D1A9 RID: 53673 RVA: 0x003E20C7 File Offset: 0x003E02C7
		public uint dirty_rect_size
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_scene_buffer_update_event_t_dirty_rect_size_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_buffer_update_event_t_dirty_rect_size_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x0600D1AC RID: 53676 RVA: 0x003E20F8 File Offset: 0x003E02F8
		// (set) Token: 0x0600D1AB RID: 53675 RVA: 0x003E20E2 File Offset: 0x003E02E2
		public blz_commerce_buffer_t buffer
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_scene_buffer_update_event_t_buffer_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_buffer_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_buffer_update_event_t_buffer_set(this.swigCPtr, blz_commerce_buffer_t.getCPtr(value));
			}
		}

		// Token: 0x0600D1AD RID: 53677 RVA: 0x003E2127 File Offset: 0x003E0327
		public blz_commerce_scene_buffer_update_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_scene_buffer_update_event_t(), true)
		{
		}

		// Token: 0x0400A2EE RID: 41710
		private HandleRef swigCPtr;

		// Token: 0x0400A2EF RID: 41711
		protected bool swigCMemOwn;
	}
}
