using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_purchase_event_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public string product_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_purchase_event_t_product_id_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_event_t_product_id_set(swigCPtr, value);
			}
		}

		public string transaction_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_purchase_event_t_transaction_id_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_event_t_transaction_id_set(swigCPtr, value);
			}
		}

		public blz_commerce_purchase_status_t status
		{
			get
			{
				return (blz_commerce_purchase_status_t)battlenet_commercePINVOKE.blz_commerce_purchase_event_t_status_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_event_t_status_set(swigCPtr, (int)value);
			}
		}

		public string error_code
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_purchase_event_t_error_code_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_event_t_error_code_set(swigCPtr, value);
			}
		}

		public blz_commerce_purchase_browser_info_t browser_info
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_purchase_event_t_browser_info_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_purchase_browser_info_t(intPtr, cMemoryOwn: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_event_t_browser_info_set(swigCPtr, blz_commerce_purchase_browser_info_t.getCPtr(value));
			}
		}

		internal blz_commerce_purchase_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_purchase_event_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_purchase_event_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_purchase_event_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_purchase_event_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_purchase_event_t(), cMemoryOwn: true)
		{
		}
	}
}
