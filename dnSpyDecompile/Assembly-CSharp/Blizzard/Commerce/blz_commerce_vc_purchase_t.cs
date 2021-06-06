using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200123A RID: 4666
	public class blz_commerce_vc_purchase_t : IDisposable
	{
		// Token: 0x0600D12D RID: 53549 RVA: 0x003E11E4 File Offset: 0x003DF3E4
		internal blz_commerce_vc_purchase_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D12E RID: 53550 RVA: 0x003E1200 File Offset: 0x003DF400
		internal static HandleRef getCPtr(blz_commerce_vc_purchase_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D12F RID: 53551 RVA: 0x003E1218 File Offset: 0x003DF418
		~blz_commerce_vc_purchase_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D130 RID: 53552 RVA: 0x003E1248 File Offset: 0x003DF448
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D131 RID: 53553 RVA: 0x003E1258 File Offset: 0x003DF458
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_vc_purchase_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x0600D133 RID: 53555 RVA: 0x003E12DE File Offset: 0x003DF4DE
		// (set) Token: 0x0600D132 RID: 53554 RVA: 0x003E12D0 File Offset: 0x003DF4D0
		public string currency_code
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_currency_code_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_currency_code_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x0600D135 RID: 53557 RVA: 0x003E12F9 File Offset: 0x003DF4F9
		// (set) Token: 0x0600D134 RID: 53556 RVA: 0x003E12EB File Offset: 0x003DF4EB
		public int product_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_product_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_product_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x0600D137 RID: 53559 RVA: 0x003E1314 File Offset: 0x003DF514
		// (set) Token: 0x0600D136 RID: 53558 RVA: 0x003E1306 File Offset: 0x003DF506
		public int game_service_region_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_game_service_region_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_game_service_region_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x0600D139 RID: 53561 RVA: 0x003E132F File Offset: 0x003DF52F
		// (set) Token: 0x0600D138 RID: 53560 RVA: 0x003E1321 File Offset: 0x003DF521
		public int quantity
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_quantity_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_quantity_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x0600D13B RID: 53563 RVA: 0x003E134A File Offset: 0x003DF54A
		// (set) Token: 0x0600D13A RID: 53562 RVA: 0x003E133C File Offset: 0x003DF53C
		public string external_transaction_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_external_transaction_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_external_transaction_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x0600D13D RID: 53565 RVA: 0x003E1365 File Offset: 0x003DF565
		// (set) Token: 0x0600D13C RID: 53564 RVA: 0x003E1357 File Offset: 0x003DF557
		public string title_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_title_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_title_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D13E RID: 53566 RVA: 0x003E1372 File Offset: 0x003DF572
		public blz_commerce_vc_purchase_t() : this(battlenet_commercePINVOKE.new_blz_commerce_vc_purchase_t(), true)
		{
		}

		// Token: 0x0400A2B1 RID: 41649
		private HandleRef swigCPtr;

		// Token: 0x0400A2B2 RID: 41650
		protected bool swigCMemOwn;
	}
}
