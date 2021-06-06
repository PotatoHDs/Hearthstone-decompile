using System;

namespace Blizzard.Commerce
{
	public class battlenet_commerce
	{
		public static string BLZ_COMMERCE_CHECKOUT_BROWSER_PARAM => battlenet_commercePINVOKE.BLZ_COMMERCE_CHECKOUT_BROWSER_PARAM_get();

		public static string BLZ_COMMERCE_LOG_SUBSYSTEM
		{
			get
			{
				return battlenet_commercePINVOKE.BLZ_COMMERCE_LOG_SUBSYSTEM_get();
			}
			set
			{
				battlenet_commercePINVOKE.BLZ_COMMERCE_LOG_SUBSYSTEM_set(value);
			}
		}

		public static string BLZ_COMMERCE_ANDROID_ACTIVITY_PARAM => battlenet_commercePINVOKE.BLZ_COMMERCE_ANDROID_ACTIVITY_PARAM_get();

		public static string BLZ_COMMERCE_ONESTORE_KEY => battlenet_commercePINVOKE.BLZ_COMMERCE_ONESTORE_KEY_get();

		public static string BLZ_COMMERCE_JAVA_VM => battlenet_commercePINVOKE.BLZ_COMMERCE_JAVA_VM_get();

		public static int BLZ_COMMERCE_INVALID_ID => battlenet_commercePINVOKE.BLZ_COMMERCE_INVALID_ID_get();

		public static string BLZ_COMMERCE_BROWSER_PARAM => battlenet_commercePINVOKE.BLZ_COMMERCE_BROWSER_PARAM_get();

		public static string BLZ_COMMERCE_HTTP_PARAM => battlenet_commercePINVOKE.BLZ_COMMERCE_HTTP_PARAM_get();

		public static blz_commerce_pair_t new_blzCommercePairArray(int nelements)
		{
			IntPtr intPtr = battlenet_commercePINVOKE.new_blzCommercePairArray(nelements);
			if (!(intPtr == IntPtr.Zero))
			{
				return new blz_commerce_pair_t(intPtr, cMemoryOwn: false);
			}
			return null;
		}

		public static void delete_blzCommercePairArray(blz_commerce_pair_t ary)
		{
			battlenet_commercePINVOKE.delete_blzCommercePairArray(blz_commerce_pair_t.getCPtr(ary));
		}

		public static blz_commerce_pair_t blzCommercePairArray_getitem(blz_commerce_pair_t ary, int index)
		{
			return new blz_commerce_pair_t(battlenet_commercePINVOKE.blzCommercePairArray_getitem(blz_commerce_pair_t.getCPtr(ary), index), cMemoryOwn: true);
		}

		public static void blzCommercePairArray_setitem(blz_commerce_pair_t ary, int index, blz_commerce_pair_t value)
		{
			battlenet_commercePINVOKE.blzCommercePairArray_setitem(blz_commerce_pair_t.getCPtr(ary), index, blz_commerce_pair_t.getCPtr(value));
			if (battlenet_commercePINVOKE.SWIGPendingException.Pending)
			{
				throw battlenet_commercePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public static blz_commerce_manifest_t blz_commerce_register_catalog()
		{
			return new blz_commerce_manifest_t(battlenet_commercePINVOKE.blz_commerce_register_catalog(), cMemoryOwn: true);
		}

		public static blz_commerce_result_t blz_catalog_load_products(blz_commerce_sdk_t sdk, string product_request)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_catalog_load_products(blz_commerce_sdk_t.getCPtr(sdk), product_request), cMemoryOwn: true);
		}

		public static blz_commerce_result_t blz_catalog_personalized_shop(blz_commerce_sdk_t sdk, string personalized_shop_request)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_catalog_personalized_shop(blz_commerce_sdk_t.getCPtr(sdk), personalized_shop_request), cMemoryOwn: true);
		}

		public static blz_commerce_manifest_t blz_commerce_register_checkout()
		{
			return new blz_commerce_manifest_t(battlenet_commercePINVOKE.blz_commerce_register_checkout(), cMemoryOwn: true);
		}

		public static blz_commerce_result_t blz_checkout_purchase(blz_commerce_sdk_t sdk, blz_commerce_purchase_t purchase)
		{
			blz_commerce_result_t result = new blz_commerce_result_t(battlenet_commercePINVOKE.blz_checkout_purchase(blz_commerce_sdk_t.getCPtr(sdk), blz_commerce_purchase_t.getCPtr(purchase)), cMemoryOwn: true);
			if (battlenet_commercePINVOKE.SWIGPendingException.Pending)
			{
				throw battlenet_commercePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public static blz_commerce_result_t blz_checkout_resume(blz_commerce_sdk_t sdk)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_checkout_resume(blz_commerce_sdk_t.getCPtr(sdk)), cMemoryOwn: true);
		}

		public static blz_commerce_result_t blz_checkout_consume(blz_commerce_sdk_t sdk, string transaction_id)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_checkout_consume(blz_commerce_sdk_t.getCPtr(sdk), transaction_id), cMemoryOwn: true);
		}

		public static blz_commerce_sdk_create_result_t blz_commerce_create()
		{
			return new blz_commerce_sdk_create_result_t(battlenet_commercePINVOKE.blz_commerce_create(), cMemoryOwn: true);
		}

		public static blz_commerce_result_t blz_commerce_register(blz_commerce_sdk_t sdk, blz_commerce_manifest_t module)
		{
			blz_commerce_result_t result = new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_register(blz_commerce_sdk_t.getCPtr(sdk), blz_commerce_manifest_t.getCPtr(module)), cMemoryOwn: true);
			if (battlenet_commercePINVOKE.SWIGPendingException.Pending)
			{
				throw battlenet_commercePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		public static blz_commerce_result_t blz_commerce_add_listener(blz_commerce_sdk_t sdk, IntPtr owner, blz_commerce_listener listener)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_add_listener(blz_commerce_sdk_t.getCPtr(sdk), owner, listener.delegateInstance), cMemoryOwn: true);
		}

		public static blz_commerce_result_t blz_commerce_register_log(IntPtr owner, blz_commerce_log_hook callback)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_register_log(owner, callback.delegateInstance), cMemoryOwn: true);
		}

		public static blz_commerce_result_t blz_commerce_init(blz_commerce_sdk_t sdk, blz_commerce_pair_t initPairs, int count)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_init(blz_commerce_sdk_t.getCPtr(sdk), blz_commerce_pair_t.getCPtr(initPairs), count), cMemoryOwn: true);
		}

		public static blz_commerce_result_t blz_commerce_update(blz_commerce_sdk_t sdk)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_update(blz_commerce_sdk_t.getCPtr(sdk)), cMemoryOwn: true);
		}

		public static blz_commerce_result_t blz_commerce_terminate(blz_commerce_sdk_t sdk)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_terminate(blz_commerce_sdk_t.getCPtr(sdk)), cMemoryOwn: true);
		}

		public static blz_commerce_manifest_t blz_commerce_register_vc()
		{
			return new blz_commerce_manifest_t(battlenet_commercePINVOKE.blz_commerce_register_vc(), cMemoryOwn: true);
		}

		public static blz_commerce_result_t blz_commerce_vc_get_balance(blz_commerce_sdk_t sdk, string get_balance_request)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_vc_get_balance(blz_commerce_sdk_t.getCPtr(sdk), get_balance_request), cMemoryOwn: true);
		}

		public static blz_commerce_result_t blz_commerce_vc_purchase(blz_commerce_sdk_t sdk, string place_order_with_vc_request)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_vc_purchase(blz_commerce_sdk_t.getCPtr(sdk), place_order_with_vc_request), cMemoryOwn: true);
		}

		public static blz_commerce_manifest_t blz_commerce_register_scene()
		{
			return new blz_commerce_manifest_t(battlenet_commercePINVOKE.blz_commerce_register_scene(), cMemoryOwn: true);
		}

		public static blz_commerce_result_t blz_commerce_browser_send_event(blz_commerce_sdk_t sdk, blz_commerce_browser_event_type_t type, IntPtr data)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_browser_send_event(blz_commerce_sdk_t.getCPtr(sdk), (int)type, data), cMemoryOwn: true);
		}

		public static blz_commerce_manifest_t blz_commerce_register_http()
		{
			return new blz_commerce_manifest_t(battlenet_commercePINVOKE.blz_commerce_register_http(), cMemoryOwn: true);
		}

		public static blz_commerce_result_t blz_commerce_http_refresh_auth(blz_commerce_sdk_t sdk, string sso)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_http_refresh_auth(blz_commerce_sdk_t.getCPtr(sdk), sso), cMemoryOwn: true);
		}

		public static string blz_commerce_generate_transaction_id(int title_id, int game_service_region)
		{
			return battlenet_commercePINVOKE.blz_commerce_generate_transaction_id(title_id, game_service_region);
		}

		public static int blz_commerce_program_to_4cc(string program_id)
		{
			return battlenet_commercePINVOKE.blz_commerce_program_to_4cc(program_id);
		}
	}
}
