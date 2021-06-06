using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200122E RID: 4654
	public class blz_commerce_event_t : IDisposable
	{
		// Token: 0x0600D0C3 RID: 53443 RVA: 0x003E04DF File Offset: 0x003DE6DF
		internal blz_commerce_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D0C4 RID: 53444 RVA: 0x003E04FB File Offset: 0x003DE6FB
		internal static HandleRef getCPtr(blz_commerce_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D0C5 RID: 53445 RVA: 0x003E0514 File Offset: 0x003DE714
		~blz_commerce_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D0C6 RID: 53446 RVA: 0x003E0544 File Offset: 0x003DE744
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D0C7 RID: 53447 RVA: 0x003E0554 File Offset: 0x003DE754
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x0600D0C9 RID: 53449 RVA: 0x003E05DA File Offset: 0x003DE7DA
		// (set) Token: 0x0600D0C8 RID: 53448 RVA: 0x003E05CC File Offset: 0x003DE7CC
		public blz_commerce_event_type_t type
		{
			get
			{
				return (blz_commerce_event_type_t)battlenet_commercePINVOKE.blz_commerce_event_t_type_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_event_t_type_set(this.swigCPtr, (int)value);
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x0600D0CB RID: 53451 RVA: 0x003E05F5 File Offset: 0x003DE7F5
		// (set) Token: 0x0600D0CA RID: 53450 RVA: 0x003E05E7 File Offset: 0x003DE7E7
		public IntPtr data
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_event_t_data_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_event_t_data_set(this.swigCPtr, value);
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x0600D0CD RID: 53453 RVA: 0x003E0610 File Offset: 0x003DE810
		// (set) Token: 0x0600D0CC RID: 53452 RVA: 0x003E0602 File Offset: 0x003DE802
		public int reference_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_event_t_reference_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_event_t_reference_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D0CE RID: 53454 RVA: 0x003E061D File Offset: 0x003DE81D
		public blz_commerce_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_event_t(), true)
		{
		}

		// Token: 0x0400A28F RID: 41615
		private HandleRef swigCPtr;

		// Token: 0x0400A290 RID: 41616
		protected bool swigCMemOwn;
	}
}
