using System;

namespace Blizzard.Commerce
{
	// Token: 0x0200125E RID: 4702
	public class battlenet_commerce
	{
		// Token: 0x0600D3C2 RID: 54210 RVA: 0x003E32E8 File Offset: 0x003E14E8
		public static blz_commerce_pair_t new_blzCommercePairArray(int nelements)
		{
			IntPtr intPtr = battlenet_commercePINVOKE.new_blzCommercePairArray(nelements);
			if (!(intPtr == IntPtr.Zero))
			{
				return new blz_commerce_pair_t(intPtr, false);
			}
			return null;
		}

		// Token: 0x0600D3C3 RID: 54211 RVA: 0x003E3312 File Offset: 0x003E1512
		public static void delete_blzCommercePairArray(blz_commerce_pair_t ary)
		{
			battlenet_commercePINVOKE.delete_blzCommercePairArray(blz_commerce_pair_t.getCPtr(ary));
		}

		// Token: 0x0600D3C4 RID: 54212 RVA: 0x003E331F File Offset: 0x003E151F
		public static blz_commerce_pair_t blzCommercePairArray_getitem(blz_commerce_pair_t ary, int index)
		{
			return new blz_commerce_pair_t(battlenet_commercePINVOKE.blzCommercePairArray_getitem(blz_commerce_pair_t.getCPtr(ary), index), true);
		}

		// Token: 0x0600D3C5 RID: 54213 RVA: 0x003E3333 File Offset: 0x003E1533
		public static void blzCommercePairArray_setitem(blz_commerce_pair_t ary, int index, blz_commerce_pair_t value)
		{
			battlenet_commercePINVOKE.blzCommercePairArray_setitem(blz_commerce_pair_t.getCPtr(ary), index, blz_commerce_pair_t.getCPtr(value));
			if (battlenet_commercePINVOKE.SWIGPendingException.Pending)
			{
				throw battlenet_commercePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600D3C6 RID: 54214 RVA: 0x003E3354 File Offset: 0x003E1554
		public static blz_commerce_manifest_t blz_commerce_register_catalog()
		{
			return new blz_commerce_manifest_t(battlenet_commercePINVOKE.blz_commerce_register_catalog(), true);
		}

		// Token: 0x0600D3C7 RID: 54215 RVA: 0x003E3361 File Offset: 0x003E1561
		public static blz_commerce_result_t blz_catalog_load_products(blz_commerce_sdk_t sdk, string product_request)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_catalog_load_products(blz_commerce_sdk_t.getCPtr(sdk), product_request), true);
		}

		// Token: 0x0600D3C8 RID: 54216 RVA: 0x003E3375 File Offset: 0x003E1575
		public static blz_commerce_result_t blz_catalog_personalized_shop(blz_commerce_sdk_t sdk, string personalized_shop_request)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_catalog_personalized_shop(blz_commerce_sdk_t.getCPtr(sdk), personalized_shop_request), true);
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x0600D3C9 RID: 54217 RVA: 0x003E3389 File Offset: 0x003E1589
		public static string BLZ_COMMERCE_CHECKOUT_BROWSER_PARAM
		{
			get
			{
				return battlenet_commercePINVOKE.BLZ_COMMERCE_CHECKOUT_BROWSER_PARAM_get();
			}
		}

		// Token: 0x0600D3CA RID: 54218 RVA: 0x003E3390 File Offset: 0x003E1590
		public static blz_commerce_manifest_t blz_commerce_register_checkout()
		{
			return new blz_commerce_manifest_t(battlenet_commercePINVOKE.blz_commerce_register_checkout(), true);
		}

		// Token: 0x0600D3CB RID: 54219 RVA: 0x003E339D File Offset: 0x003E159D
		public static blz_commerce_result_t blz_checkout_purchase(blz_commerce_sdk_t sdk, blz_commerce_purchase_t purchase)
		{
			blz_commerce_result_t result = new blz_commerce_result_t(battlenet_commercePINVOKE.blz_checkout_purchase(blz_commerce_sdk_t.getCPtr(sdk), blz_commerce_purchase_t.getCPtr(purchase)), true);
			if (battlenet_commercePINVOKE.SWIGPendingException.Pending)
			{
				throw battlenet_commercePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		// Token: 0x0600D3CC RID: 54220 RVA: 0x003E33C3 File Offset: 0x003E15C3
		public static blz_commerce_result_t blz_checkout_resume(blz_commerce_sdk_t sdk)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_checkout_resume(blz_commerce_sdk_t.getCPtr(sdk)), true);
		}

		// Token: 0x0600D3CD RID: 54221 RVA: 0x003E33D6 File Offset: 0x003E15D6
		public static blz_commerce_result_t blz_checkout_consume(blz_commerce_sdk_t sdk, string transaction_id)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_checkout_consume(blz_commerce_sdk_t.getCPtr(sdk), transaction_id), true);
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x0600D3CF RID: 54223 RVA: 0x003E33F2 File Offset: 0x003E15F2
		// (set) Token: 0x0600D3CE RID: 54222 RVA: 0x003E33EA File Offset: 0x003E15EA
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

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x0600D3D0 RID: 54224 RVA: 0x003E33F9 File Offset: 0x003E15F9
		public static string BLZ_COMMERCE_ANDROID_ACTIVITY_PARAM
		{
			get
			{
				return battlenet_commercePINVOKE.BLZ_COMMERCE_ANDROID_ACTIVITY_PARAM_get();
			}
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x0600D3D1 RID: 54225 RVA: 0x003E3400 File Offset: 0x003E1600
		public static string BLZ_COMMERCE_ONESTORE_KEY
		{
			get
			{
				return battlenet_commercePINVOKE.BLZ_COMMERCE_ONESTORE_KEY_get();
			}
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x0600D3D2 RID: 54226 RVA: 0x003E3407 File Offset: 0x003E1607
		public static string BLZ_COMMERCE_JAVA_VM
		{
			get
			{
				return battlenet_commercePINVOKE.BLZ_COMMERCE_JAVA_VM_get();
			}
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x0600D3D3 RID: 54227 RVA: 0x003E340E File Offset: 0x003E160E
		public static int BLZ_COMMERCE_INVALID_ID
		{
			get
			{
				return battlenet_commercePINVOKE.BLZ_COMMERCE_INVALID_ID_get();
			}
		}

		// Token: 0x0600D3D4 RID: 54228 RVA: 0x003E3415 File Offset: 0x003E1615
		public static blz_commerce_sdk_create_result_t blz_commerce_create()
		{
			return new blz_commerce_sdk_create_result_t(battlenet_commercePINVOKE.blz_commerce_create(), true);
		}

		// Token: 0x0600D3D5 RID: 54229 RVA: 0x003E3422 File Offset: 0x003E1622
		public static blz_commerce_result_t blz_commerce_register(blz_commerce_sdk_t sdk, blz_commerce_manifest_t module)
		{
			blz_commerce_result_t result = new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_register(blz_commerce_sdk_t.getCPtr(sdk), blz_commerce_manifest_t.getCPtr(module)), true);
			if (battlenet_commercePINVOKE.SWIGPendingException.Pending)
			{
				throw battlenet_commercePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}

		// Token: 0x0600D3D6 RID: 54230 RVA: 0x003E3448 File Offset: 0x003E1648
		public static blz_commerce_result_t blz_commerce_add_listener(blz_commerce_sdk_t sdk, IntPtr owner, blz_commerce_listener listener)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_add_listener(blz_commerce_sdk_t.getCPtr(sdk), owner, listener.delegateInstance), true);
		}

		// Token: 0x0600D3D7 RID: 54231 RVA: 0x003E3462 File Offset: 0x003E1662
		public static blz_commerce_result_t blz_commerce_register_log(IntPtr owner, blz_commerce_log_hook callback)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_register_log(owner, callback.delegateInstance), true);
		}

		// Token: 0x0600D3D8 RID: 54232 RVA: 0x003E3476 File Offset: 0x003E1676
		public static blz_commerce_result_t blz_commerce_init(blz_commerce_sdk_t sdk, blz_commerce_pair_t initPairs, int count)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_init(blz_commerce_sdk_t.getCPtr(sdk), blz_commerce_pair_t.getCPtr(initPairs), count), true);
		}

		// Token: 0x0600D3D9 RID: 54233 RVA: 0x003E3490 File Offset: 0x003E1690
		public static blz_commerce_result_t blz_commerce_update(blz_commerce_sdk_t sdk)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_update(blz_commerce_sdk_t.getCPtr(sdk)), true);
		}

		// Token: 0x0600D3DA RID: 54234 RVA: 0x003E34A3 File Offset: 0x003E16A3
		public static blz_commerce_result_t blz_commerce_terminate(blz_commerce_sdk_t sdk)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_terminate(blz_commerce_sdk_t.getCPtr(sdk)), true);
		}

		// Token: 0x0600D3DB RID: 54235 RVA: 0x003E34B6 File Offset: 0x003E16B6
		public static blz_commerce_manifest_t blz_commerce_register_vc()
		{
			return new blz_commerce_manifest_t(battlenet_commercePINVOKE.blz_commerce_register_vc(), true);
		}

		// Token: 0x0600D3DC RID: 54236 RVA: 0x003E34C3 File Offset: 0x003E16C3
		public static blz_commerce_result_t blz_commerce_vc_get_balance(blz_commerce_sdk_t sdk, string get_balance_request)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_vc_get_balance(blz_commerce_sdk_t.getCPtr(sdk), get_balance_request), true);
		}

		// Token: 0x0600D3DD RID: 54237 RVA: 0x003E34D7 File Offset: 0x003E16D7
		public static blz_commerce_result_t blz_commerce_vc_purchase(blz_commerce_sdk_t sdk, string place_order_with_vc_request)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_vc_purchase(blz_commerce_sdk_t.getCPtr(sdk), place_order_with_vc_request), true);
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x0600D3DE RID: 54238 RVA: 0x003E34EB File Offset: 0x003E16EB
		public static string BLZ_COMMERCE_BROWSER_PARAM
		{
			get
			{
				return battlenet_commercePINVOKE.BLZ_COMMERCE_BROWSER_PARAM_get();
			}
		}

		// Token: 0x0600D3DF RID: 54239 RVA: 0x003E34F2 File Offset: 0x003E16F2
		public static blz_commerce_manifest_t blz_commerce_register_scene()
		{
			return new blz_commerce_manifest_t(battlenet_commercePINVOKE.blz_commerce_register_scene(), true);
		}

		// Token: 0x0600D3E0 RID: 54240 RVA: 0x003E34FF File Offset: 0x003E16FF
		public static blz_commerce_result_t blz_commerce_browser_send_event(blz_commerce_sdk_t sdk, blz_commerce_browser_event_type_t type, IntPtr data)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_browser_send_event(blz_commerce_sdk_t.getCPtr(sdk), (int)type, data), true);
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x0600D3E1 RID: 54241 RVA: 0x003E3514 File Offset: 0x003E1714
		public static string BLZ_COMMERCE_HTTP_PARAM
		{
			get
			{
				return battlenet_commercePINVOKE.BLZ_COMMERCE_HTTP_PARAM_get();
			}
		}

		// Token: 0x0600D3E2 RID: 54242 RVA: 0x003E351B File Offset: 0x003E171B
		public static blz_commerce_manifest_t blz_commerce_register_http()
		{
			return new blz_commerce_manifest_t(battlenet_commercePINVOKE.blz_commerce_register_http(), true);
		}

		// Token: 0x0600D3E3 RID: 54243 RVA: 0x003E3528 File Offset: 0x003E1728
		public static blz_commerce_result_t blz_commerce_http_refresh_auth(blz_commerce_sdk_t sdk, string sso)
		{
			return new blz_commerce_result_t(battlenet_commercePINVOKE.blz_commerce_http_refresh_auth(blz_commerce_sdk_t.getCPtr(sdk), sso), true);
		}

		// Token: 0x0600D3E4 RID: 54244 RVA: 0x003E353C File Offset: 0x003E173C
		public static string blz_commerce_generate_transaction_id(int title_id, int game_service_region)
		{
			return battlenet_commercePINVOKE.blz_commerce_generate_transaction_id(title_id, game_service_region);
		}

		// Token: 0x0600D3E5 RID: 54245 RVA: 0x003E3545 File Offset: 0x003E1745
		public static int blz_commerce_program_to_4cc(string program_id)
		{
			return battlenet_commercePINVOKE.blz_commerce_program_to_4cc(program_id);
		}
	}
}
