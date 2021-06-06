using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_vc_order_event_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public blz_commerce_http_enabled_event_t http_result
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_vc_order_event_t_http_result_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_http_enabled_event_t(intPtr, cMemoryOwn: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_order_event_t_http_result_set(swigCPtr, blz_commerce_http_enabled_event_t.getCPtr(value));
			}
		}

		public string response
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_order_event_t_response_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_order_event_t_response_set(swigCPtr, value);
			}
		}

		internal blz_commerce_vc_order_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_vc_order_event_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_vc_order_event_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_vc_order_event_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_vc_order_event_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_vc_order_event_t(), cMemoryOwn: true)
		{
		}
	}
}
