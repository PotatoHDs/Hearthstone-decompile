using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_browser_purchase_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public string routing_key
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_routing_key_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_routing_key_set(swigCPtr, value);
			}
		}

		public string server_validation_signature
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_server_validation_signature_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_server_validation_signature_set(swigCPtr, value);
			}
		}

		public string sso_token
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_sso_token_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_sso_token_set(swigCPtr, value);
			}
		}

		public string externalTransactionId
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_externalTransactionId_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_purchase_t_externalTransactionId_set(swigCPtr, value);
			}
		}

		internal blz_commerce_browser_purchase_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_browser_purchase_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_browser_purchase_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_browser_purchase_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_browser_purchase_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_browser_purchase_t(), cMemoryOwn: true)
		{
		}
	}
}
