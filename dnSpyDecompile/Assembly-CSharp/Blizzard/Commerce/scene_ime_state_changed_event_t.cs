using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001243 RID: 4675
	public class scene_ime_state_changed_event_t : IDisposable
	{
		// Token: 0x0600D16D RID: 53613 RVA: 0x003E18E4 File Offset: 0x003DFAE4
		internal scene_ime_state_changed_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D16E RID: 53614 RVA: 0x003E1900 File Offset: 0x003DFB00
		internal static HandleRef getCPtr(scene_ime_state_changed_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D16F RID: 53615 RVA: 0x003E1918 File Offset: 0x003DFB18
		~scene_ime_state_changed_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D170 RID: 53616 RVA: 0x003E1948 File Offset: 0x003DFB48
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D171 RID: 53617 RVA: 0x003E1958 File Offset: 0x003DFB58
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_scene_ime_state_changed_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x0600D173 RID: 53619 RVA: 0x003E19DE File Offset: 0x003DFBDE
		// (set) Token: 0x0600D172 RID: 53618 RVA: 0x003E19D0 File Offset: 0x003DFBD0
		public scene_ime_input_type_t input_type
		{
			get
			{
				return (scene_ime_input_type_t)battlenet_commercePINVOKE.scene_ime_state_changed_event_t_input_type_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_state_changed_event_t_input_type_set(this.swigCPtr, (int)value);
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x0600D175 RID: 53621 RVA: 0x003E19F9 File Offset: 0x003DFBF9
		// (set) Token: 0x0600D174 RID: 53620 RVA: 0x003E19EB File Offset: 0x003DFBEB
		public string surrounding_text
		{
			get
			{
				return battlenet_commercePINVOKE.scene_ime_state_changed_event_t_surrounding_text_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_state_changed_event_t_surrounding_text_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D176 RID: 53622 RVA: 0x003E1A06 File Offset: 0x003DFC06
		public scene_ime_state_changed_event_t() : this(battlenet_commercePINVOKE.new_scene_ime_state_changed_event_t(), true)
		{
		}

		// Token: 0x0400A2E4 RID: 41700
		private HandleRef swigCPtr;

		// Token: 0x0400A2E5 RID: 41701
		protected bool swigCMemOwn;
	}
}
