using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_browser_params_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public int window_width
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_window_width_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_window_width_set(swigCPtr, value);
			}
		}

		public int window_height
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_window_height_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_window_height_set(swigCPtr, value);
			}
		}

		public int max_window_width
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_max_window_width_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_max_window_width_set(swigCPtr, value);
			}
		}

		public int max_window_height
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_max_window_height_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_max_window_height_set(swigCPtr, value);
			}
		}

		public string log_directory
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_log_directory_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_log_directory_set(swigCPtr, value);
			}
		}

		public string browser_directory
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_browser_directory_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_browser_directory_set(swigCPtr, value);
			}
		}

		public bool is_prod
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_browser_params_t_is_prod_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_browser_params_t_is_prod_set(swigCPtr, value);
			}
		}

		internal blz_commerce_browser_params_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_browser_params_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_browser_params_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_browser_params_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_browser_params_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_browser_params_t(), cMemoryOwn: true)
		{
		}
	}
}
