using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200122D RID: 4653
	public class blz_commerce_http_enabled_event_t : IDisposable
	{
		// Token: 0x0600D0B7 RID: 53431 RVA: 0x003E0392 File Offset: 0x003DE592
		internal blz_commerce_http_enabled_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D0B8 RID: 53432 RVA: 0x003E03AE File Offset: 0x003DE5AE
		internal static HandleRef getCPtr(blz_commerce_http_enabled_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D0B9 RID: 53433 RVA: 0x003E03C8 File Offset: 0x003DE5C8
		~blz_commerce_http_enabled_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D0BA RID: 53434 RVA: 0x003E03F8 File Offset: 0x003DE5F8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D0BB RID: 53435 RVA: 0x003E0408 File Offset: 0x003DE608
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_http_enabled_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x0600D0BD RID: 53437 RVA: 0x003E048E File Offset: 0x003DE68E
		// (set) Token: 0x0600D0BC RID: 53436 RVA: 0x003E0480 File Offset: 0x003DE680
		public bool ok
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_http_enabled_event_t_ok_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_http_enabled_event_t_ok_set(this.swigCPtr, value);
			}
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x0600D0BF RID: 53439 RVA: 0x003E04A9 File Offset: 0x003DE6A9
		// (set) Token: 0x0600D0BE RID: 53438 RVA: 0x003E049B File Offset: 0x003DE69B
		public long result_code
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_http_enabled_event_t_result_code_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_http_enabled_event_t_result_code_set(this.swigCPtr, value);
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x0600D0C1 RID: 53441 RVA: 0x003E04C4 File Offset: 0x003DE6C4
		// (set) Token: 0x0600D0C0 RID: 53440 RVA: 0x003E04B6 File Offset: 0x003DE6B6
		public string message
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_http_enabled_event_t_message_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_http_enabled_event_t_message_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D0C2 RID: 53442 RVA: 0x003E04D1 File Offset: 0x003DE6D1
		public blz_commerce_http_enabled_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_http_enabled_event_t(), true)
		{
		}

		// Token: 0x0400A28D RID: 41613
		private HandleRef swigCPtr;

		// Token: 0x0400A28E RID: 41614
		protected bool swigCMemOwn;
	}
}
