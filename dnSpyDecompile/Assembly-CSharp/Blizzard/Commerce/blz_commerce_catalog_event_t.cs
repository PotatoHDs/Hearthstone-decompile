using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001220 RID: 4640
	public class blz_commerce_catalog_event_t : IDisposable
	{
		// Token: 0x0600D03F RID: 53311 RVA: 0x003DF601 File Offset: 0x003DD801
		internal blz_commerce_catalog_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D040 RID: 53312 RVA: 0x003DF61D File Offset: 0x003DD81D
		internal static HandleRef getCPtr(blz_commerce_catalog_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D041 RID: 53313 RVA: 0x003DF634 File Offset: 0x003DD834
		~blz_commerce_catalog_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D042 RID: 53314 RVA: 0x003DF664 File Offset: 0x003DD864
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D043 RID: 53315 RVA: 0x003DF674 File Offset: 0x003DD874
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_catalog_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x0600D045 RID: 53317 RVA: 0x003DF6FA File Offset: 0x003DD8FA
		// (set) Token: 0x0600D044 RID: 53316 RVA: 0x003DF6EC File Offset: 0x003DD8EC
		public blz_commerce_catalog_type_t catalog_type
		{
			get
			{
				return (blz_commerce_catalog_type_t)battlenet_commercePINVOKE.blz_commerce_catalog_event_t_catalog_type_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_catalog_event_t_catalog_type_set(this.swigCPtr, (int)value);
			}
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x0600D047 RID: 53319 RVA: 0x003DF715 File Offset: 0x003DD915
		// (set) Token: 0x0600D046 RID: 53318 RVA: 0x003DF707 File Offset: 0x003DD907
		public IntPtr catalog_data
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_catalog_event_t_catalog_data_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_catalog_event_t_catalog_data_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D048 RID: 53320 RVA: 0x003DF722 File Offset: 0x003DD922
		public blz_commerce_catalog_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_catalog_event_t(), true)
		{
		}

		// Token: 0x0400A26B RID: 41579
		private HandleRef swigCPtr;

		// Token: 0x0400A26C RID: 41580
		protected bool swigCMemOwn;
	}
}
