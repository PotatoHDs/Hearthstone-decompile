using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class scene_range_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public int from
		{
			get
			{
				return battlenet_commercePINVOKE.scene_range_t_from_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.scene_range_t_from_set(swigCPtr, value);
			}
		}

		public int to
		{
			get
			{
				return battlenet_commercePINVOKE.scene_range_t_to_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.scene_range_t_to_set(swigCPtr, value);
			}
		}

		internal scene_range_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(scene_range_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~scene_range_t()
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
						battlenet_commercePINVOKE.delete_scene_range_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public scene_range_t()
			: this(battlenet_commercePINVOKE.new_scene_range_t(), cMemoryOwn: true)
		{
		}
	}
}
