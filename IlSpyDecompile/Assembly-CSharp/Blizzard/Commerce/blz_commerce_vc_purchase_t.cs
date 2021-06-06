using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_vc_purchase_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public string currency_code
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_currency_code_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_currency_code_set(swigCPtr, value);
			}
		}

		public int product_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_product_id_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_product_id_set(swigCPtr, value);
			}
		}

		public int game_service_region_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_game_service_region_id_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_game_service_region_id_set(swigCPtr, value);
			}
		}

		public int quantity
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_quantity_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_quantity_set(swigCPtr, value);
			}
		}

		public string external_transaction_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_external_transaction_id_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_external_transaction_id_set(swigCPtr, value);
			}
		}

		public string title_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_title_id_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_purchase_t_title_id_set(swigCPtr, value);
			}
		}

		internal blz_commerce_vc_purchase_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_vc_purchase_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_vc_purchase_t()
		{
			Dispose(disposing: false);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_vc_purchase_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_vc_purchase_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_vc_purchase_t(), cMemoryOwn: true)
		{
		}
	}
}
