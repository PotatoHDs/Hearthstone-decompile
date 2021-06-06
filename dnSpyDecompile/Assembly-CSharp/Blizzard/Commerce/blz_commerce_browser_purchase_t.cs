using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001227 RID: 4647
	public class blz_commerce_browser_purchase_t : IDisposable
	{
		// Token: 0x0600D083 RID: 53379 RVA: 0x003DFE05 File Offset: 0x003DE005
		internal blz_commerce_browser_purchase_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D084 RID: 53380 RVA: 0x003DFE21 File Offset: 0x003DE021
		internal static HandleRef getCPtr(blz_commerce_browser_purchase_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D085 RID: 53381 RVA: 0x003DFE38 File Offset: 0x003DE038
		~blz_commerce_browser_purchase_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D086 RID: 53382 RVA: 0x003DFE68 File Offset: 0x003DE068
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D087 RID: 53383 RVA: 0x003DFE78 File Offset: 0x003DE078
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_browser_purchase_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x0600D089 RID: 53385 RVA: 0x003DFEFE File Offset: 0x003DE0FE
		// (set) Token: 0x0600D088 RID: 53384 RVA: 0x003DFEF0 File Offset: 0x003DE0F0
		public string routing_key
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_routing_key_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_routing_key_set(this.swigCPtr, value);
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x0600D08B RID: 53387 RVA: 0x003DFF19 File Offset: 0x003DE119
		// (set) Token: 0x0600D08A RID: 53386 RVA: 0x003DFF0B File Offset: 0x003DE10B
		public string server_validation_signature
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_server_validation_signature_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_server_validation_signature_set(this.swigCPtr, value);
			}
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x0600D08D RID: 53389 RVA: 0x003DFF34 File Offset: 0x003DE134
		// (set) Token: 0x0600D08C RID: 53388 RVA: 0x003DFF26 File Offset: 0x003DE126
		public string sso_token
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_sso_token_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_sso_token_set(this.swigCPtr, value);
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x0600D08F RID: 53391 RVA: 0x003DFF4F File Offset: 0x003DE14F
		// (set) Token: 0x0600D08E RID: 53390 RVA: 0x003DFF41 File Offset: 0x003DE141
		public string externalTransactionId
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_externalTransactionId_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_externalTransactionId_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D090 RID: 53392 RVA: 0x003DFF5C File Offset: 0x003DE15C
		public blz_commerce_browser_purchase_t() : this(battlenet_commercePINVOKE.new_blz_commerce_browser_purchase_t(), true)
		{
		}

		// Token: 0x0400A27C RID: 41596
		private HandleRef swigCPtr;

		// Token: 0x0400A27D RID: 41597
		protected bool swigCMemOwn;
	}
}
