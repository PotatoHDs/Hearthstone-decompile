using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001258 RID: 4696
	public class blz_commerce_http_need_auth_event_t : IDisposable
	{
		// Token: 0x0600D22A RID: 53802 RVA: 0x003E2FCC File Offset: 0x003E11CC
		internal blz_commerce_http_need_auth_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D22B RID: 53803 RVA: 0x003E2FE8 File Offset: 0x003E11E8
		internal static HandleRef getCPtr(blz_commerce_http_need_auth_event_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D22C RID: 53804 RVA: 0x003E3000 File Offset: 0x003E1200
		~blz_commerce_http_need_auth_event_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D22D RID: 53805 RVA: 0x003E3030 File Offset: 0x003E1230
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D22E RID: 53806 RVA: 0x003E3040 File Offset: 0x003E1240
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_http_need_auth_event_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x0600D22F RID: 53807 RVA: 0x003E30B8 File Offset: 0x003E12B8
		public blz_commerce_http_need_auth_event_t() : this(battlenet_commercePINVOKE.new_blz_commerce_http_need_auth_event_t(), true)
		{
		}

		// Token: 0x0400A319 RID: 41753
		private HandleRef swigCPtr;

		// Token: 0x0400A31A RID: 41754
		protected bool swigCMemOwn;
	}
}
