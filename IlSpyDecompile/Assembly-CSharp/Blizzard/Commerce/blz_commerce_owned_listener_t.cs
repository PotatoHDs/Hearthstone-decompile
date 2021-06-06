using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_owned_listener_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public IntPtr owner
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_owned_listener_t_owner_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_owned_listener_t_owner_set(swigCPtr, value);
			}
		}

		public blz_commerce_listener listener
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		internal blz_commerce_owned_listener_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_owned_listener_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_owned_listener_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_owned_listener_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_owned_listener_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_owned_listener_t(), cMemoryOwn: true)
		{
		}
	}
}
