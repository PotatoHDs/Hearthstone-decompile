using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001242 RID: 4674
	public class scene_ime_composition_range_changed_event_t : IDisposable
	{
		// Token: 0x0600D161 RID: 53601 RVA: 0x003E1746 File Offset: 0x003DF946
		internal scene_ime_composition_range_changed_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D162 RID: 53602 RVA: 0x003E1762 File Offset: 0x003DF962
		internal static HandleRef getCPtr(scene_ime_composition_range_changed_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D163 RID: 53603 RVA: 0x003E177C File Offset: 0x003DF97C
		~scene_ime_composition_range_changed_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D164 RID: 53604 RVA: 0x003E17AC File Offset: 0x003DF9AC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D165 RID: 53605 RVA: 0x003E17BC File Offset: 0x003DF9BC
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_scene_ime_composition_range_changed_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x0600D167 RID: 53607 RVA: 0x003E1848 File Offset: 0x003DFA48
		// (set) Token: 0x0600D166 RID: 53606 RVA: 0x003E1834 File Offset: 0x003DFA34
		public scene_range_t selected_range
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.scene_ime_composition_range_changed_event_t_selected_range_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new scene_range_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_composition_range_changed_event_t_selected_range_set(this.swigCPtr, scene_range_t.getCPtr(value));
			}
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x0600D169 RID: 53609 RVA: 0x003E188C File Offset: 0x003DFA8C
		// (set) Token: 0x0600D168 RID: 53608 RVA: 0x003E1877 File Offset: 0x003DFA77
		public blz_commerce_scene_rect_t character_bounds
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.scene_ime_composition_range_changed_event_t_character_bounds_get(this.swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_scene_rect_t(intPtr, false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_composition_range_changed_event_t_character_bounds_set(this.swigCPtr, blz_commerce_scene_rect_t.getCPtr(value));
			}
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x0600D16B RID: 53611 RVA: 0x003E18C9 File Offset: 0x003DFAC9
		// (set) Token: 0x0600D16A RID: 53610 RVA: 0x003E18BB File Offset: 0x003DFABB
		public uint character_bounds_size
		{
			get
			{
				return battlenet_commercePINVOKE.scene_ime_composition_range_changed_event_t_character_bounds_size_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_composition_range_changed_event_t_character_bounds_size_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D16C RID: 53612 RVA: 0x003E18D6 File Offset: 0x003DFAD6
		public scene_ime_composition_range_changed_event_t() : this(battlenet_commercePINVOKE.new_scene_ime_composition_range_changed_event_t(), true)
		{
		}

		// Token: 0x0400A2E2 RID: 41698
		private HandleRef swigCPtr;

		// Token: 0x0400A2E3 RID: 41699
		protected bool swigCMemOwn;
	}
}
