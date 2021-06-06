using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001237 RID: 4663
	public class blz_commerce_vc_event_t : IDisposable
	{
		// Token: 0x0600D10F RID: 53519 RVA: 0x003E0E04 File Offset: 0x003DF004
		internal blz_commerce_vc_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D110 RID: 53520 RVA: 0x003E0E20 File Offset: 0x003DF020
		internal static HandleRef getCPtr(blz_commerce_vc_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D111 RID: 53521 RVA: 0x003E0E38 File Offset: 0x003DF038
		~blz_commerce_vc_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D112 RID: 53522 RVA: 0x003E0E68 File Offset: 0x003DF068
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D113 RID: 53523 RVA: 0x003E0E78 File Offset: 0x003DF078
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_vc_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x0600D115 RID: 53525 RVA: 0x003E0EFE File Offset: 0x003DF0FE
		// (set) Token: 0x0600D114 RID: 53524 RVA: 0x003E0EF0 File Offset: 0x003DF0F0
		public blz_commerce_vc_type_t vc_type
		{
			get
			{
				return (blz_commerce_vc_type_t)battlenet_commercePINVOKE.blz_commerce_vc_event_t_vc_type_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_event_t_vc_type_set(this.swigCPtr, (int)value);
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x0600D117 RID: 53527 RVA: 0x003E0F19 File Offset: 0x003DF119
		// (set) Token: 0x0600D116 RID: 53526 RVA: 0x003E0F0B File Offset: 0x003DF10B
		public IntPtr vc_data
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_event_t_vc_data_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_event_t_vc_data_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D118 RID: 53528 RVA: 0x003E0F26 File Offset: 0x003DF126
		public blz_commerce_vc_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_vc_event_t(), true)
		{
		}

		// Token: 0x0400A2AB RID: 41643
		private HandleRef swigCPtr;

		// Token: 0x0400A2AC RID: 41644
		protected bool swigCMemOwn;
	}
}
