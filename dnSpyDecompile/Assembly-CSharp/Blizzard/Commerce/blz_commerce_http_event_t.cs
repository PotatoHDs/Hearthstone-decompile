using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001257 RID: 4695
	public class blz_commerce_http_event_t : IDisposable
	{
		// Token: 0x0600D220 RID: 53792 RVA: 0x003E2E9B File Offset: 0x003E109B
		internal blz_commerce_http_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D221 RID: 53793 RVA: 0x003E2EB7 File Offset: 0x003E10B7
		internal static HandleRef getCPtr(blz_commerce_http_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D222 RID: 53794 RVA: 0x003E2ED0 File Offset: 0x003E10D0
		~blz_commerce_http_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D223 RID: 53795 RVA: 0x003E2F00 File Offset: 0x003E1100
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D224 RID: 53796 RVA: 0x003E2F10 File Offset: 0x003E1110
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_http_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x0600D226 RID: 53798 RVA: 0x003E2F96 File Offset: 0x003E1196
		// (set) Token: 0x0600D225 RID: 53797 RVA: 0x003E2F88 File Offset: 0x003E1188
		public blz_commerce_http_type_t http_type
		{
			get
			{
				return (blz_commerce_http_type_t)battlenet_commercePINVOKE.blz_commerce_http_event_t_http_type_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_http_event_t_http_type_set(this.swigCPtr, (int)value);
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x0600D228 RID: 53800 RVA: 0x003E2FB1 File Offset: 0x003E11B1
		// (set) Token: 0x0600D227 RID: 53799 RVA: 0x003E2FA3 File Offset: 0x003E11A3
		public IntPtr http_data
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_http_event_t_http_data_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_http_event_t_http_data_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D229 RID: 53801 RVA: 0x003E2FBE File Offset: 0x003E11BE
		public blz_commerce_http_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_http_event_t(), true)
		{
		}

		// Token: 0x0400A317 RID: 41751
		private HandleRef swigCPtr;

		// Token: 0x0400A318 RID: 41752
		protected bool swigCMemOwn;
	}
}
