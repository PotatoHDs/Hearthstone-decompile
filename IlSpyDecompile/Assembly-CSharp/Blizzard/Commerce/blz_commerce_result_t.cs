using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_result_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public blz_commerce_result_state_t state
		{
			get
			{
				return (blz_commerce_result_state_t)battlenet_commercePINVOKE.blz_commerce_result_t_state_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_result_t_state_set(swigCPtr, (int)value);
			}
		}

		public IntPtr data
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_result_t_data_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_result_t_data_set(swigCPtr, value);
			}
		}

		public int reference_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_result_t_reference_id_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_result_t_reference_id_set(swigCPtr, value);
			}
		}

		internal blz_commerce_result_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_result_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_result_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_result_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_result_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_result_t(), cMemoryOwn: true)
		{
		}
	}
}
