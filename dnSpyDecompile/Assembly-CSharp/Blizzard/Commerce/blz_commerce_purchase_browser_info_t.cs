using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001225 RID: 4645
	public class blz_commerce_purchase_browser_info_t : IDisposable
	{
		// Token: 0x0600D06B RID: 53355 RVA: 0x003DFB46 File Offset: 0x003DDD46
		internal blz_commerce_purchase_browser_info_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D06C RID: 53356 RVA: 0x003DFB62 File Offset: 0x003DDD62
		internal static HandleRef getCPtr(blz_commerce_purchase_browser_info_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D06D RID: 53357 RVA: 0x003DFB7C File Offset: 0x003DDD7C
		~blz_commerce_purchase_browser_info_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D06E RID: 53358 RVA: 0x003DFBAC File Offset: 0x003DDDAC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D06F RID: 53359 RVA: 0x003DFBBC File Offset: 0x003DDDBC
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_purchase_browser_info_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x0600D071 RID: 53361 RVA: 0x003DFC42 File Offset: 0x003DDE42
		// (set) Token: 0x0600D070 RID: 53360 RVA: 0x003DFC34 File Offset: 0x003DDE34
		public bool is_cancelable
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_purchase_browser_info_t_is_cancelable_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_browser_info_t_is_cancelable_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D072 RID: 53362 RVA: 0x003DFC4F File Offset: 0x003DDE4F
		public blz_commerce_purchase_browser_info_t() : this(battlenet_commercePINVOKE.new_blz_commerce_purchase_browser_info_t(), true)
		{
		}

		// Token: 0x0400A278 RID: 41592
		private HandleRef swigCPtr;

		// Token: 0x0400A279 RID: 41593
		protected bool swigCMemOwn;
	}
}
