using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001246 RID: 4678
	public class blz_commerce_scene_event_t : IDisposable
	{
		// Token: 0x0600D18F RID: 53647 RVA: 0x003E1D24 File Offset: 0x003DFF24
		internal blz_commerce_scene_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D190 RID: 53648 RVA: 0x003E1D40 File Offset: 0x003DFF40
		internal static HandleRef getCPtr(blz_commerce_scene_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D191 RID: 53649 RVA: 0x003E1D58 File Offset: 0x003DFF58
		~blz_commerce_scene_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D192 RID: 53650 RVA: 0x003E1D88 File Offset: 0x003DFF88
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D193 RID: 53651 RVA: 0x003E1D98 File Offset: 0x003DFF98
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_scene_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x0600D195 RID: 53653 RVA: 0x003E1E1E File Offset: 0x003E001E
		// (set) Token: 0x0600D194 RID: 53652 RVA: 0x003E1E10 File Offset: 0x003E0010
		public blz_commerce_scene_type_t scene_type
		{
			get
			{
				return (blz_commerce_scene_type_t)battlenet_commercePINVOKE.blz_commerce_scene_event_t_scene_type_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_event_t_scene_type_set(this.swigCPtr, (int)value);
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x0600D197 RID: 53655 RVA: 0x003E1E39 File Offset: 0x003E0039
		// (set) Token: 0x0600D196 RID: 53654 RVA: 0x003E1E2B File Offset: 0x003E002B
		public IntPtr scene_data
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_scene_event_t_scene_data_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_event_t_scene_data_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D198 RID: 53656 RVA: 0x003E1E46 File Offset: 0x003E0046
		public blz_commerce_scene_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_scene_event_t(), true)
		{
		}

		// Token: 0x0400A2EA RID: 41706
		private HandleRef swigCPtr;

		// Token: 0x0400A2EB RID: 41707
		protected bool swigCMemOwn;
	}
}
