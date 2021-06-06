using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001244 RID: 4676
	public class scene_ime_text_selection_changed_event_t : IDisposable
	{
		// Token: 0x0600D177 RID: 53623 RVA: 0x003E1A14 File Offset: 0x003DFC14
		internal scene_ime_text_selection_changed_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D178 RID: 53624 RVA: 0x003E1A30 File Offset: 0x003DFC30
		internal static HandleRef getCPtr(scene_ime_text_selection_changed_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D179 RID: 53625 RVA: 0x003E1A48 File Offset: 0x003DFC48
		~scene_ime_text_selection_changed_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D17A RID: 53626 RVA: 0x003E1A78 File Offset: 0x003DFC78
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D17B RID: 53627 RVA: 0x003E1A88 File Offset: 0x003DFC88
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_scene_ime_text_selection_changed_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x0600D17D RID: 53629 RVA: 0x003E1B0E File Offset: 0x003DFD0E
		// (set) Token: 0x0600D17C RID: 53628 RVA: 0x003E1B00 File Offset: 0x003DFD00
		public string text
		{
			get
			{
				return battlenet_commercePINVOKE.scene_ime_text_selection_changed_event_t_text_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_text_selection_changed_event_t_text_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x0600D17F RID: 53631 RVA: 0x003E1B29 File Offset: 0x003DFD29
		// (set) Token: 0x0600D17E RID: 53630 RVA: 0x003E1B1B File Offset: 0x003DFD1B
		public int offset
		{
			get
			{
				return battlenet_commercePINVOKE.scene_ime_text_selection_changed_event_t_offset_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_text_selection_changed_event_t_offset_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x0600D181 RID: 53633 RVA: 0x003E1B4C File Offset: 0x003DFD4C
		// (set) Token: 0x0600D180 RID: 53632 RVA: 0x003E1B36 File Offset: 0x003DFD36
		public scene_range_t selected_range
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.scene_ime_text_selection_changed_event_t_selected_range_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new scene_range_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_text_selection_changed_event_t_selected_range_set(this.swigCPtr, scene_range_t.getCPtr(value));
			}
		}

		// Token: 0x0600D182 RID: 53634 RVA: 0x003E1B7B File Offset: 0x003DFD7B
		public scene_ime_text_selection_changed_event_t() : this(battlenet_commercePINVOKE.new_scene_ime_text_selection_changed_event_t(), true)
		{
		}

		// Token: 0x0400A2E6 RID: 41702
		private HandleRef swigCPtr;

		// Token: 0x0400A2E7 RID: 41703
		protected bool swigCMemOwn;
	}
}
