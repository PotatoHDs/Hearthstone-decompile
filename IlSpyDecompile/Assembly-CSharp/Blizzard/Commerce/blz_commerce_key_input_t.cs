using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_key_input_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public int keyCode
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_key_input_t_keyCode_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_key_input_t_keyCode_set(swigCPtr, value);
			}
		}

		public blz_commerce_scene_input_type_t type
		{
			get
			{
				return (blz_commerce_scene_input_type_t)battlenet_commercePINVOKE.blz_commerce_key_input_t_type_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_key_input_t_type_set(swigCPtr, (int)value);
			}
		}

		public int character
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_key_input_t_character_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_key_input_t_character_set(swigCPtr, value);
			}
		}

		public uint modifiers
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_key_input_t_modifiers_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_key_input_t_modifiers_set(swigCPtr, value);
			}
		}

		internal blz_commerce_key_input_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_key_input_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_key_input_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_key_input_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_key_input_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_key_input_t(), cMemoryOwn: true)
		{
		}
	}
}
