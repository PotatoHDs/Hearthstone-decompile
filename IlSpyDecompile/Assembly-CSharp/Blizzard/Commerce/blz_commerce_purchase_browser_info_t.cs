using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_purchase_browser_info_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public bool is_cancelable
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_purchase_browser_info_t_is_cancelable_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_purchase_browser_info_t_is_cancelable_set(swigCPtr, value);
			}
		}

		internal blz_commerce_purchase_browser_info_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_purchase_browser_info_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_purchase_browser_info_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_purchase_browser_info_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_purchase_browser_info_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_purchase_browser_info_t(), cMemoryOwn: true)
		{
		}
	}
}
