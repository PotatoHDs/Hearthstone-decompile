using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_buffer_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public byte[] bytes
		{
			get
			{
				int num = len;
				IntPtr source = battlenet_commercePINVOKE.blz_commerce_buffer_t_bytes_get(swigCPtr);
				byte[] array = new byte[num];
				Marshal.Copy(source, array, 0, num);
				return array;
			}
		}

		public int len
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_buffer_t_len_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_buffer_t_len_set(swigCPtr, value);
			}
		}

		internal blz_commerce_buffer_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_buffer_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_buffer_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_buffer_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_buffer_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_buffer_t(), cMemoryOwn: true)
		{
		}
	}
}
