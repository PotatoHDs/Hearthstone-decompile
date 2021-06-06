using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_vc_event_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public blz_commerce_vc_type_t vc_type
		{
			get
			{
				return (blz_commerce_vc_type_t)battlenet_commercePINVOKE.blz_commerce_vc_event_t_vc_type_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_event_t_vc_type_set(swigCPtr, (int)value);
			}
		}

		public IntPtr vc_data
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vc_event_t_vc_data_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vc_event_t_vc_data_set(swigCPtr, value);
			}
		}

		internal blz_commerce_vc_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_vc_event_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_vc_event_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_vc_event_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_vc_event_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_vc_event_t(), cMemoryOwn: true)
		{
		}
	}
}
