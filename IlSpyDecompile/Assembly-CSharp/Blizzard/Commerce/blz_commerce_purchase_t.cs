using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_purchase_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public string product_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_purchase_t_product_id_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_t_product_id_set(swigCPtr, value);
			}
		}

		public string currency_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_purchase_t_currency_id_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_t_currency_id_set(swigCPtr, value);
			}
		}

		public blz_commerce_browser_purchase_t browser_purchase
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_purchase_t_browser_purchase_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_browser_purchase_t(intPtr, cMemoryOwn: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_t_browser_purchase_set(swigCPtr, blz_commerce_browser_purchase_t.getCPtr(value));
			}
		}

		internal blz_commerce_purchase_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_purchase_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_purchase_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_purchase_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_purchase_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_purchase_t(), cMemoryOwn: true)
		{
		}
	}
}
