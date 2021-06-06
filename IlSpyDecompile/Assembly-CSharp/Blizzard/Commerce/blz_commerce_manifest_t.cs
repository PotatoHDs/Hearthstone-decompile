using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_manifest_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public SWIGTYPE_p_f_p_blz_commerce_sdk_t_a___q_const__blz_commerce_pair_t_uint32_t__blz_commerce_result_t config
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_config_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new SWIGTYPE_p_f_p_blz_commerce_sdk_t_a___q_const__blz_commerce_pair_t_uint32_t__blz_commerce_result_t(intPtr, futureUse: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_config_set(swigCPtr, SWIGTYPE_p_f_p_blz_commerce_sdk_t_a___q_const__blz_commerce_pair_t_uint32_t__blz_commerce_result_t.getCPtr(value));
			}
		}

		public SWIGTYPE_p_f_p_blz_commerce_sdk_t_a___q_const__blz_commerce_pair_t_uint32_t__blz_commerce_result_t post_init
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_post_init_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new SWIGTYPE_p_f_p_blz_commerce_sdk_t_a___q_const__blz_commerce_pair_t_uint32_t__blz_commerce_result_t(intPtr, futureUse: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_post_init_set(swigCPtr, SWIGTYPE_p_f_p_blz_commerce_sdk_t_a___q_const__blz_commerce_pair_t_uint32_t__blz_commerce_result_t.getCPtr(value));
			}
		}

		public SWIGTYPE_p_f_p_blz_commerce_sdk_t__void terminate
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_terminate_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new SWIGTYPE_p_f_p_blz_commerce_sdk_t__void(intPtr, futureUse: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_terminate_set(swigCPtr, SWIGTYPE_p_f_p_blz_commerce_sdk_t__void.getCPtr(value));
			}
		}

		public SWIGTYPE_p_f_p_blz_commerce_sdk_t__void update
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_update_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new SWIGTYPE_p_f_p_blz_commerce_sdk_t__void(intPtr, futureUse: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_update_set(swigCPtr, SWIGTYPE_p_f_p_blz_commerce_sdk_t__void.getCPtr(value));
			}
		}

		public SWIGTYPE_p_f___p_char get_name
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_get_name_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new SWIGTYPE_p_f___p_char(intPtr, futureUse: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_get_name_set(swigCPtr, SWIGTYPE_p_f___p_char.getCPtr(value));
			}
		}

		public SWIGTYPE_p_f___p_char get_scopes
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_get_scopes_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new SWIGTYPE_p_f___p_char(intPtr, futureUse: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_get_scopes_set(swigCPtr, SWIGTYPE_p_f___p_char.getCPtr(value));
			}
		}

		public blz_commerce_manifest_t dependencies
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_manifest_t_dependencies_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_manifest_t(intPtr, cMemoryOwn: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_dependencies_set(swigCPtr, getCPtr(value));
			}
		}

		public uint dependency_count
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_manifest_t_dependency_count_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_manifest_t_dependency_count_set(swigCPtr, value);
			}
		}

		internal blz_commerce_manifest_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_manifest_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_manifest_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_manifest_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_manifest_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_manifest_t(), cMemoryOwn: true)
		{
		}
	}
}
