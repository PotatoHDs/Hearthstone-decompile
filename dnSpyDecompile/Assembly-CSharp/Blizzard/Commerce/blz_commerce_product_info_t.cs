using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001223 RID: 4643
	public class blz_commerce_product_info_t : IDisposable
	{
		// Token: 0x0600D05D RID: 53341 RVA: 0x003DF9E0 File Offset: 0x003DDBE0
		internal blz_commerce_product_info_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D05E RID: 53342 RVA: 0x003DF9FC File Offset: 0x003DDBFC
		internal static HandleRef getCPtr(blz_commerce_product_info_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D05F RID: 53343 RVA: 0x003DFA14 File Offset: 0x003DDC14
		~blz_commerce_product_info_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D060 RID: 53344 RVA: 0x003DFA44 File Offset: 0x003DDC44
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D061 RID: 53345 RVA: 0x003DFA54 File Offset: 0x003DDC54
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_product_info_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x0600D063 RID: 53347 RVA: 0x003DFADA File Offset: 0x003DDCDA
		// (set) Token: 0x0600D062 RID: 53346 RVA: 0x003DFACC File Offset: 0x003DDCCC
		public long product_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_product_info_t_product_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_product_info_t_product_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x0600D065 RID: 53349 RVA: 0x003DFAF5 File Offset: 0x003DDCF5
		// (set) Token: 0x0600D064 RID: 53348 RVA: 0x003DFAE7 File Offset: 0x003DDCE7
		public string title
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_product_info_t_title_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_product_info_t_title_set(this.swigCPtr, value);
			}
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x0600D067 RID: 53351 RVA: 0x003DFB10 File Offset: 0x003DDD10
		// (set) Token: 0x0600D066 RID: 53350 RVA: 0x003DFB02 File Offset: 0x003DDD02
		public string standard_price
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_product_info_t_standard_price_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_product_info_t_standard_price_set(this.swigCPtr, value);
			}
		}

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x0600D069 RID: 53353 RVA: 0x003DFB2B File Offset: 0x003DDD2B
		// (set) Token: 0x0600D068 RID: 53352 RVA: 0x003DFB1D File Offset: 0x003DDD1D
		public string sale_price
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_product_info_t_sale_price_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_product_info_t_sale_price_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D06A RID: 53354 RVA: 0x003DFB38 File Offset: 0x003DDD38
		public blz_commerce_product_info_t() : this(battlenet_commercePINVOKE.new_blz_commerce_product_info_t(), true)
		{
		}

		// Token: 0x0400A271 RID: 41585
		private HandleRef swigCPtr;

		// Token: 0x0400A272 RID: 41586
		protected bool swigCMemOwn;
	}
}
