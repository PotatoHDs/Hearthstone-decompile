using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001245 RID: 4677
	public class scene_ime_selection_bounds_changed_event_t : IDisposable
	{
		// Token: 0x0600D183 RID: 53635 RVA: 0x003E1B89 File Offset: 0x003DFD89
		internal scene_ime_selection_bounds_changed_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D184 RID: 53636 RVA: 0x003E1BA5 File Offset: 0x003DFDA5
		internal static HandleRef getCPtr(scene_ime_selection_bounds_changed_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D185 RID: 53637 RVA: 0x003E1BBC File Offset: 0x003DFDBC
		~scene_ime_selection_bounds_changed_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D186 RID: 53638 RVA: 0x003E1BEC File Offset: 0x003DFDEC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D187 RID: 53639 RVA: 0x003E1BFC File Offset: 0x003DFDFC
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_scene_ime_selection_bounds_changed_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x0600D189 RID: 53641 RVA: 0x003E1C88 File Offset: 0x003DFE88
		// (set) Token: 0x0600D188 RID: 53640 RVA: 0x003E1C74 File Offset: 0x003DFE74
		public blz_commerce_scene_rect_t anchor_rect
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.scene_ime_selection_bounds_changed_event_t_anchor_rect_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_scene_rect_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_selection_bounds_changed_event_t_anchor_rect_set(this.swigCPtr, blz_commerce_scene_rect_t.getCPtr(value));
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x0600D18B RID: 53643 RVA: 0x003E1CCC File Offset: 0x003DFECC
		// (set) Token: 0x0600D18A RID: 53642 RVA: 0x003E1CB7 File Offset: 0x003DFEB7
		public blz_commerce_scene_rect_t focus_rect
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.scene_ime_selection_bounds_changed_event_t_focus_rect_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_scene_rect_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_selection_bounds_changed_event_t_focus_rect_set(this.swigCPtr, blz_commerce_scene_rect_t.getCPtr(value));
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x0600D18D RID: 53645 RVA: 0x003E1D09 File Offset: 0x003DFF09
		// (set) Token: 0x0600D18C RID: 53644 RVA: 0x003E1CFB File Offset: 0x003DFEFB
		public bool is_anchor_rect
		{
			get
			{
				return battlenet_commercePINVOKE.scene_ime_selection_bounds_changed_event_t_is_anchor_rect_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_selection_bounds_changed_event_t_is_anchor_rect_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D18E RID: 53646 RVA: 0x003E1D16 File Offset: 0x003DFF16
		public scene_ime_selection_bounds_changed_event_t() : this(battlenet_commercePINVOKE.new_scene_ime_selection_bounds_changed_event_t(), true)
		{
		}

		// Token: 0x0400A2E8 RID: 41704
		private HandleRef swigCPtr;

		// Token: 0x0400A2E9 RID: 41705
		protected bool swigCMemOwn;
	}
}
