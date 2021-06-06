using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200124B RID: 4683
	public class blz_commerce_scene_external_link_event_t : IDisposable
	{
		// Token: 0x0600D1C0 RID: 53696 RVA: 0x003E23C9 File Offset: 0x003E05C9
		internal blz_commerce_scene_external_link_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D1C1 RID: 53697 RVA: 0x003E23E5 File Offset: 0x003E05E5
		internal static HandleRef getCPtr(blz_commerce_scene_external_link_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D1C2 RID: 53698 RVA: 0x003E23FC File Offset: 0x003E05FC
		~blz_commerce_scene_external_link_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D1C3 RID: 53699 RVA: 0x003E242C File Offset: 0x003E062C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D1C4 RID: 53700 RVA: 0x003E243C File Offset: 0x003E063C
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_scene_external_link_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x0600D1C6 RID: 53702 RVA: 0x003E24C2 File Offset: 0x003E06C2
		// (set) Token: 0x0600D1C5 RID: 53701 RVA: 0x003E24B4 File Offset: 0x003E06B4
		public string url
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_scene_external_link_event_t_url_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_external_link_event_t_url_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D1C7 RID: 53703 RVA: 0x003E24CF File Offset: 0x003E06CF
		public blz_commerce_scene_external_link_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_scene_external_link_event_t(), true)
		{
		}

		// Token: 0x0400A2F4 RID: 41716
		private HandleRef swigCPtr;

		// Token: 0x0400A2F5 RID: 41717
		protected bool swigCMemOwn;
	}
}
