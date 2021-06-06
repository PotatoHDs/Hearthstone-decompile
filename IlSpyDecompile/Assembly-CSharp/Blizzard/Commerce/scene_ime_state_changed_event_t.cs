using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class scene_ime_state_changed_event_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public scene_ime_input_type_t input_type
		{
			get
			{
				return (scene_ime_input_type_t)battlenet_commercePINVOKE.scene_ime_state_changed_event_t_input_type_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_state_changed_event_t_input_type_set(swigCPtr, (int)value);
			}
		}

		public string surrounding_text
		{
			get
			{
				return battlenet_commercePINVOKE.scene_ime_state_changed_event_t_surrounding_text_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_state_changed_event_t_surrounding_text_set(swigCPtr, value);
			}
		}

		internal scene_ime_state_changed_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(scene_ime_state_changed_event_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~scene_ime_state_changed_event_t()
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
						battlenet_commercePINVOKE.delete_scene_ime_state_changed_event_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public scene_ime_state_changed_event_t()
			: this(battlenet_commercePINVOKE.new_scene_ime_state_changed_event_t(), cMemoryOwn: true)
		{
		}
	}
}
