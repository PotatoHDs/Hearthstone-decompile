using System;
using System.IO;
using System.Runtime.InteropServices;
using AOT;

namespace Blizzard.Commerce
{
	// Token: 0x0200125A RID: 4698
	internal class battlenet_commercePINVOKE
	{
		// Token: 0x0600D243 RID: 53827
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blzCommercePairArray___")]
		public static extern IntPtr new_blzCommercePairArray(int jarg1);

		// Token: 0x0600D244 RID: 53828
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blzCommercePairArray___")]
		public static extern void delete_blzCommercePairArray(HandleRef jarg1);

		// Token: 0x0600D245 RID: 53829
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blzCommercePairArray_getitem___")]
		public static extern IntPtr blzCommercePairArray_getitem(HandleRef jarg1, int jarg2);

		// Token: 0x0600D246 RID: 53830
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blzCommercePairArray_setitem___")]
		public static extern void blzCommercePairArray_setitem(HandleRef jarg1, int jarg2, HandleRef jarg3);

		// Token: 0x0600D247 RID: 53831
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_event_t_catalog_type_set___")]
		public static extern void blz_commerce_catalog_event_t_catalog_type_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D248 RID: 53832
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_event_t_catalog_type_get___")]
		public static extern int blz_commerce_catalog_event_t_catalog_type_get(HandleRef jarg1);

		// Token: 0x0600D249 RID: 53833
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_event_t_catalog_data_set___")]
		public static extern void blz_commerce_catalog_event_t_catalog_data_set(HandleRef jarg1, IntPtr jarg2);

		// Token: 0x0600D24A RID: 53834
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_event_t_catalog_data_get___")]
		public static extern IntPtr blz_commerce_catalog_event_t_catalog_data_get(HandleRef jarg1);

		// Token: 0x0600D24B RID: 53835
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_catalog_event_t___")]
		public static extern IntPtr new_blz_commerce_catalog_event_t();

		// Token: 0x0600D24C RID: 53836
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_catalog_event_t___")]
		public static extern void delete_blz_commerce_catalog_event_t(HandleRef jarg1);

		// Token: 0x0600D24D RID: 53837
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_product_load_event_t_http_result_set___")]
		public static extern void blz_commerce_catalog_product_load_event_t_http_result_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D24E RID: 53838
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_product_load_event_t_http_result_get___")]
		public static extern IntPtr blz_commerce_catalog_product_load_event_t_http_result_get(HandleRef jarg1);

		// Token: 0x0600D24F RID: 53839
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_product_load_event_t_response_set___")]
		public static extern void blz_commerce_catalog_product_load_event_t_response_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D250 RID: 53840
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_product_load_event_t_response_get___")]
		public static extern string blz_commerce_catalog_product_load_event_t_response_get(HandleRef jarg1);

		// Token: 0x0600D251 RID: 53841
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_catalog_product_load_event_t___")]
		public static extern IntPtr new_blz_commerce_catalog_product_load_event_t();

		// Token: 0x0600D252 RID: 53842
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_catalog_product_load_event_t___")]
		public static extern void delete_blz_commerce_catalog_product_load_event_t(HandleRef jarg1);

		// Token: 0x0600D253 RID: 53843
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_personalized_shop_event_t_http_result_set___")]
		public static extern void blz_commerce_catalog_personalized_shop_event_t_http_result_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D254 RID: 53844
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_personalized_shop_event_t_http_result_get___")]
		public static extern IntPtr blz_commerce_catalog_personalized_shop_event_t_http_result_get(HandleRef jarg1);

		// Token: 0x0600D255 RID: 53845
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_personalized_shop_event_t_response_set___")]
		public static extern void blz_commerce_catalog_personalized_shop_event_t_response_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D256 RID: 53846
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_personalized_shop_event_t_response_get___")]
		public static extern string blz_commerce_catalog_personalized_shop_event_t_response_get(HandleRef jarg1);

		// Token: 0x0600D257 RID: 53847
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_catalog_personalized_shop_event_t___")]
		public static extern IntPtr new_blz_commerce_catalog_personalized_shop_event_t();

		// Token: 0x0600D258 RID: 53848
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_catalog_personalized_shop_event_t___")]
		public static extern void delete_blz_commerce_catalog_personalized_shop_event_t(HandleRef jarg1);

		// Token: 0x0600D259 RID: 53849
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_product_id_set___")]
		public static extern void blz_commerce_product_info_t_product_id_set(HandleRef jarg1, long jarg2);

		// Token: 0x0600D25A RID: 53850
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_product_id_get___")]
		public static extern long blz_commerce_product_info_t_product_id_get(HandleRef jarg1);

		// Token: 0x0600D25B RID: 53851
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_title_set___")]
		public static extern void blz_commerce_product_info_t_title_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D25C RID: 53852
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_title_get___")]
		public static extern string blz_commerce_product_info_t_title_get(HandleRef jarg1);

		// Token: 0x0600D25D RID: 53853
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_standard_price_set___")]
		public static extern void blz_commerce_product_info_t_standard_price_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D25E RID: 53854
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_standard_price_get___")]
		public static extern string blz_commerce_product_info_t_standard_price_get(HandleRef jarg1);

		// Token: 0x0600D25F RID: 53855
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_sale_price_set___")]
		public static extern void blz_commerce_product_info_t_sale_price_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D260 RID: 53856
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_sale_price_get___")]
		public static extern string blz_commerce_product_info_t_sale_price_get(HandleRef jarg1);

		// Token: 0x0600D261 RID: 53857
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_product_info_t___")]
		public static extern IntPtr new_blz_commerce_product_info_t();

		// Token: 0x0600D262 RID: 53858
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_product_info_t___")]
		public static extern void delete_blz_commerce_product_info_t(HandleRef jarg1);

		// Token: 0x0600D263 RID: 53859
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register_catalog___")]
		public static extern IntPtr blz_commerce_register_catalog();

		// Token: 0x0600D264 RID: 53860
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_catalog_load_products___")]
		public static extern IntPtr blz_catalog_load_products(HandleRef jarg1, string jarg2);

		// Token: 0x0600D265 RID: 53861
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_catalog_personalized_shop___")]
		public static extern IntPtr blz_catalog_personalized_shop(HandleRef jarg1, string jarg2);

		// Token: 0x0600D266 RID: 53862
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_browser_info_t_is_cancelable_set___")]
		public static extern void blz_commerce_purchase_browser_info_t_is_cancelable_set(HandleRef jarg1, bool jarg2);

		// Token: 0x0600D267 RID: 53863
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_browser_info_t_is_cancelable_get___")]
		public static extern bool blz_commerce_purchase_browser_info_t_is_cancelable_get(HandleRef jarg1);

		// Token: 0x0600D268 RID: 53864
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_purchase_browser_info_t___")]
		public static extern IntPtr new_blz_commerce_purchase_browser_info_t();

		// Token: 0x0600D269 RID: 53865
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_purchase_browser_info_t___")]
		public static extern void delete_blz_commerce_purchase_browser_info_t(HandleRef jarg1);

		// Token: 0x0600D26A RID: 53866
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_product_id_set___")]
		public static extern void blz_commerce_purchase_event_t_product_id_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D26B RID: 53867
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_product_id_get___")]
		public static extern string blz_commerce_purchase_event_t_product_id_get(HandleRef jarg1);

		// Token: 0x0600D26C RID: 53868
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_transaction_id_set___")]
		public static extern void blz_commerce_purchase_event_t_transaction_id_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D26D RID: 53869
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_transaction_id_get___")]
		public static extern string blz_commerce_purchase_event_t_transaction_id_get(HandleRef jarg1);

		// Token: 0x0600D26E RID: 53870
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_status_set___")]
		public static extern void blz_commerce_purchase_event_t_status_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D26F RID: 53871
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_status_get___")]
		public static extern int blz_commerce_purchase_event_t_status_get(HandleRef jarg1);

		// Token: 0x0600D270 RID: 53872
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_error_code_set___")]
		public static extern void blz_commerce_purchase_event_t_error_code_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D271 RID: 53873
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_error_code_get___")]
		public static extern string blz_commerce_purchase_event_t_error_code_get(HandleRef jarg1);

		// Token: 0x0600D272 RID: 53874
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_browser_info_set___")]
		public static extern void blz_commerce_purchase_event_t_browser_info_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D273 RID: 53875
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_browser_info_get___")]
		public static extern IntPtr blz_commerce_purchase_event_t_browser_info_get(HandleRef jarg1);

		// Token: 0x0600D274 RID: 53876
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_purchase_event_t___")]
		public static extern IntPtr new_blz_commerce_purchase_event_t();

		// Token: 0x0600D275 RID: 53877
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_purchase_event_t___")]
		public static extern void delete_blz_commerce_purchase_event_t(HandleRef jarg1);

		// Token: 0x0600D276 RID: 53878
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_routing_key_set___")]
		public static extern void blz_commerce_browser_purchase_t_routing_key_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D277 RID: 53879
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_routing_key_get___")]
		public static extern string blz_commerce_browser_purchase_t_routing_key_get(HandleRef jarg1);

		// Token: 0x0600D278 RID: 53880
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_server_validation_signature_set___")]
		public static extern void blz_commerce_browser_purchase_t_server_validation_signature_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D279 RID: 53881
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_server_validation_signature_get___")]
		public static extern string blz_commerce_browser_purchase_t_server_validation_signature_get(HandleRef jarg1);

		// Token: 0x0600D27A RID: 53882
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_sso_token_set___")]
		public static extern void blz_commerce_browser_purchase_t_sso_token_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D27B RID: 53883
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_sso_token_get___")]
		public static extern string blz_commerce_browser_purchase_t_sso_token_get(HandleRef jarg1);

		// Token: 0x0600D27C RID: 53884
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_externalTransactionId_set___")]
		public static extern void blz_commerce_browser_purchase_t_externalTransactionId_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D27D RID: 53885
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_externalTransactionId_get___")]
		public static extern string blz_commerce_browser_purchase_t_externalTransactionId_get(HandleRef jarg1);

		// Token: 0x0600D27E RID: 53886
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_browser_purchase_t___")]
		public static extern IntPtr new_blz_commerce_browser_purchase_t();

		// Token: 0x0600D27F RID: 53887
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_browser_purchase_t___")]
		public static extern void delete_blz_commerce_browser_purchase_t(HandleRef jarg1);

		// Token: 0x0600D280 RID: 53888
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_t_product_id_set___")]
		public static extern void blz_commerce_purchase_t_product_id_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D281 RID: 53889
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_t_product_id_get___")]
		public static extern string blz_commerce_purchase_t_product_id_get(HandleRef jarg1);

		// Token: 0x0600D282 RID: 53890
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_t_currency_id_set___")]
		public static extern void blz_commerce_purchase_t_currency_id_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D283 RID: 53891
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_t_currency_id_get___")]
		public static extern string blz_commerce_purchase_t_currency_id_get(HandleRef jarg1);

		// Token: 0x0600D284 RID: 53892
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_t_browser_purchase_set___")]
		public static extern void blz_commerce_purchase_t_browser_purchase_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D285 RID: 53893
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_t_browser_purchase_get___")]
		public static extern IntPtr blz_commerce_purchase_t_browser_purchase_get(HandleRef jarg1);

		// Token: 0x0600D286 RID: 53894
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_purchase_t___")]
		public static extern IntPtr new_blz_commerce_purchase_t();

		// Token: 0x0600D287 RID: 53895
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_purchase_t___")]
		public static extern void delete_blz_commerce_purchase_t(HandleRef jarg1);

		// Token: 0x0600D288 RID: 53896
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_checkout_url_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_checkout_url_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D289 RID: 53897
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_checkout_url_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_checkout_url_get(HandleRef jarg1);

		// Token: 0x0600D28A RID: 53898
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_title_code_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_title_code_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D28B RID: 53899
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_title_code_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_title_code_get(HandleRef jarg1);

		// Token: 0x0600D28C RID: 53900
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_device_id_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_device_id_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D28D RID: 53901
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_device_id_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_device_id_get(HandleRef jarg1);

		// Token: 0x0600D28E RID: 53902
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_title_version_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_title_version_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D28F RID: 53903
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_title_version_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_title_version_get(HandleRef jarg1);

		// Token: 0x0600D290 RID: 53904
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_locale_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_locale_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D291 RID: 53905
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_locale_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_locale_get(HandleRef jarg1);

		// Token: 0x0600D292 RID: 53906
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_game_service_region_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_game_service_region_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D293 RID: 53907
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_game_service_region_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_game_service_region_get(HandleRef jarg1);

		// Token: 0x0600D294 RID: 53908
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_game_account_id_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_game_account_id_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D295 RID: 53909
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_game_account_id_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_game_account_id_get(HandleRef jarg1);

		// Token: 0x0600D296 RID: 53910
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_checkout_browser_params_t___")]
		public static extern IntPtr new_blz_commerce_checkout_browser_params_t();

		// Token: 0x0600D297 RID: 53911
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_checkout_browser_params_t___")]
		public static extern void delete_blz_commerce_checkout_browser_params_t(HandleRef jarg1);

		// Token: 0x0600D298 RID: 53912
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_CHECKOUT_BROWSER_PARAM_get___")]
		public static extern string BLZ_COMMERCE_CHECKOUT_BROWSER_PARAM_get();

		// Token: 0x0600D299 RID: 53913
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register_checkout___")]
		public static extern IntPtr blz_commerce_register_checkout();

		// Token: 0x0600D29A RID: 53914
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_checkout_purchase___")]
		public static extern IntPtr blz_checkout_purchase(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D29B RID: 53915
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_checkout_resume___")]
		public static extern IntPtr blz_checkout_resume(HandleRef jarg1);

		// Token: 0x0600D29C RID: 53916
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_checkout_consume___")]
		public static extern IntPtr blz_checkout_consume(HandleRef jarg1, string jarg2);

		// Token: 0x0600D29D RID: 53917
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_sdk_t___")]
		public static extern IntPtr new_blz_commerce_sdk_t();

		// Token: 0x0600D29E RID: 53918
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_sdk_t___")]
		public static extern void delete_blz_commerce_sdk_t(HandleRef jarg1);

		// Token: 0x0600D29F RID: 53919
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_enabled_event_t_ok_set___")]
		public static extern void blz_commerce_http_enabled_event_t_ok_set(HandleRef jarg1, bool jarg2);

		// Token: 0x0600D2A0 RID: 53920
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_enabled_event_t_ok_get___")]
		public static extern bool blz_commerce_http_enabled_event_t_ok_get(HandleRef jarg1);

		// Token: 0x0600D2A1 RID: 53921
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_enabled_event_t_result_code_set___")]
		public static extern void blz_commerce_http_enabled_event_t_result_code_set(HandleRef jarg1, long jarg2);

		// Token: 0x0600D2A2 RID: 53922
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_enabled_event_t_result_code_get___")]
		public static extern long blz_commerce_http_enabled_event_t_result_code_get(HandleRef jarg1);

		// Token: 0x0600D2A3 RID: 53923
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_enabled_event_t_message_set___")]
		public static extern void blz_commerce_http_enabled_event_t_message_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D2A4 RID: 53924
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_enabled_event_t_message_get___")]
		public static extern string blz_commerce_http_enabled_event_t_message_get(HandleRef jarg1);

		// Token: 0x0600D2A5 RID: 53925
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_http_enabled_event_t___")]
		public static extern IntPtr new_blz_commerce_http_enabled_event_t();

		// Token: 0x0600D2A6 RID: 53926
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_http_enabled_event_t___")]
		public static extern void delete_blz_commerce_http_enabled_event_t(HandleRef jarg1);

		// Token: 0x0600D2A7 RID: 53927
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_event_t_type_set___")]
		public static extern void blz_commerce_event_t_type_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D2A8 RID: 53928
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_event_t_type_get___")]
		public static extern int blz_commerce_event_t_type_get(HandleRef jarg1);

		// Token: 0x0600D2A9 RID: 53929
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_event_t_data_set___")]
		public static extern void blz_commerce_event_t_data_set(HandleRef jarg1, IntPtr jarg2);

		// Token: 0x0600D2AA RID: 53930
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_event_t_data_get___")]
		public static extern IntPtr blz_commerce_event_t_data_get(HandleRef jarg1);

		// Token: 0x0600D2AB RID: 53931
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_event_t_reference_id_set___")]
		public static extern void blz_commerce_event_t_reference_id_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D2AC RID: 53932
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_event_t_reference_id_get___")]
		public static extern int blz_commerce_event_t_reference_id_get(HandleRef jarg1);

		// Token: 0x0600D2AD RID: 53933
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_event_t___")]
		public static extern IntPtr new_blz_commerce_event_t();

		// Token: 0x0600D2AE RID: 53934
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_event_t___")]
		public static extern void delete_blz_commerce_event_t(HandleRef jarg1);

		// Token: 0x0600D2AF RID: 53935
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_result_t_state_set___")]
		public static extern void blz_commerce_result_t_state_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D2B0 RID: 53936
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_result_t_state_get___")]
		public static extern int blz_commerce_result_t_state_get(HandleRef jarg1);

		// Token: 0x0600D2B1 RID: 53937
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_result_t_data_set___")]
		public static extern void blz_commerce_result_t_data_set(HandleRef jarg1, IntPtr jarg2);

		// Token: 0x0600D2B2 RID: 53938
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_result_t_data_get___")]
		public static extern IntPtr blz_commerce_result_t_data_get(HandleRef jarg1);

		// Token: 0x0600D2B3 RID: 53939
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_result_t_reference_id_set___")]
		public static extern void blz_commerce_result_t_reference_id_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D2B4 RID: 53940
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_result_t_reference_id_get___")]
		public static extern int blz_commerce_result_t_reference_id_get(HandleRef jarg1);

		// Token: 0x0600D2B5 RID: 53941
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_result_t___")]
		public static extern IntPtr new_blz_commerce_result_t();

		// Token: 0x0600D2B6 RID: 53942
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_result_t___")]
		public static extern void delete_blz_commerce_result_t(HandleRef jarg1);

		// Token: 0x0600D2B7 RID: 53943
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_sdk_create_result_t_state_set___")]
		public static extern void blz_commerce_sdk_create_result_t_state_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D2B8 RID: 53944
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_sdk_create_result_t_state_get___")]
		public static extern int blz_commerce_sdk_create_result_t_state_get(HandleRef jarg1);

		// Token: 0x0600D2B9 RID: 53945
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_sdk_create_result_t_sdk_set___")]
		public static extern void blz_commerce_sdk_create_result_t_sdk_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D2BA RID: 53946
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_sdk_create_result_t_sdk_get___")]
		public static extern IntPtr blz_commerce_sdk_create_result_t_sdk_get(HandleRef jarg1);

		// Token: 0x0600D2BB RID: 53947
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_sdk_create_result_t___")]
		public static extern IntPtr new_blz_commerce_sdk_create_result_t();

		// Token: 0x0600D2BC RID: 53948
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_sdk_create_result_t___")]
		public static extern void delete_blz_commerce_sdk_create_result_t(HandleRef jarg1);

		// Token: 0x0600D2BD RID: 53949
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_pair_t_key_set___")]
		public static extern void blz_commerce_pair_t_key_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D2BE RID: 53950
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_pair_t_key_get___")]
		public static extern string blz_commerce_pair_t_key_get(HandleRef jarg1);

		// Token: 0x0600D2BF RID: 53951
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_pair_t_data_set___")]
		public static extern void blz_commerce_pair_t_data_set(HandleRef jarg1, IntPtr jarg2);

		// Token: 0x0600D2C0 RID: 53952
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_pair_t_data_get___")]
		public static extern IntPtr blz_commerce_pair_t_data_get(HandleRef jarg1);

		// Token: 0x0600D2C1 RID: 53953
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_pair_t___")]
		public static extern IntPtr new_blz_commerce_pair_t();

		// Token: 0x0600D2C2 RID: 53954
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_pair_t___")]
		public static extern void delete_blz_commerce_pair_t(HandleRef jarg1);

		// Token: 0x0600D2C3 RID: 53955
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_owned_listener_t_owner_set___")]
		public static extern void blz_commerce_owned_listener_t_owner_set(HandleRef jarg1, IntPtr jarg2);

		// Token: 0x0600D2C4 RID: 53956
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_owned_listener_t_owner_get___")]
		public static extern IntPtr blz_commerce_owned_listener_t_owner_get(HandleRef jarg1);

		// Token: 0x0600D2C5 RID: 53957
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_owned_listener_t_listener_set___")]
		public static extern void blz_commerce_owned_listener_t_listener_set(HandleRef jarg1, blz_commerce_listener.CallbackDelegate jarg2);

		// Token: 0x0600D2C6 RID: 53958
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_owned_listener_t_listener_get___")]
		public static extern blz_commerce_listener.CallbackDelegate blz_commerce_owned_listener_t_listener_get(HandleRef jarg1);

		// Token: 0x0600D2C7 RID: 53959
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_owned_listener_t___")]
		public static extern IntPtr new_blz_commerce_owned_listener_t();

		// Token: 0x0600D2C8 RID: 53960
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_owned_listener_t___")]
		public static extern void delete_blz_commerce_owned_listener_t(HandleRef jarg1);

		// Token: 0x0600D2C9 RID: 53961
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_config_set___")]
		public static extern void blz_commerce_manifest_t_config_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D2CA RID: 53962
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_config_get___")]
		public static extern IntPtr blz_commerce_manifest_t_config_get(HandleRef jarg1);

		// Token: 0x0600D2CB RID: 53963
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_post_init_set___")]
		public static extern void blz_commerce_manifest_t_post_init_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D2CC RID: 53964
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_post_init_get___")]
		public static extern IntPtr blz_commerce_manifest_t_post_init_get(HandleRef jarg1);

		// Token: 0x0600D2CD RID: 53965
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_terminate_set___")]
		public static extern void blz_commerce_manifest_t_terminate_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D2CE RID: 53966
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_terminate_get___")]
		public static extern IntPtr blz_commerce_manifest_t_terminate_get(HandleRef jarg1);

		// Token: 0x0600D2CF RID: 53967
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_update_set___")]
		public static extern void blz_commerce_manifest_t_update_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D2D0 RID: 53968
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_update_get___")]
		public static extern IntPtr blz_commerce_manifest_t_update_get(HandleRef jarg1);

		// Token: 0x0600D2D1 RID: 53969
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_get_name_set___")]
		public static extern void blz_commerce_manifest_t_get_name_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D2D2 RID: 53970
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_get_name_get___")]
		public static extern IntPtr blz_commerce_manifest_t_get_name_get(HandleRef jarg1);

		// Token: 0x0600D2D3 RID: 53971
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_get_scopes_set___")]
		public static extern void blz_commerce_manifest_t_get_scopes_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D2D4 RID: 53972
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_get_scopes_get___")]
		public static extern IntPtr blz_commerce_manifest_t_get_scopes_get(HandleRef jarg1);

		// Token: 0x0600D2D5 RID: 53973
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_dependencies_set___")]
		public static extern void blz_commerce_manifest_t_dependencies_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D2D6 RID: 53974
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_dependencies_get___")]
		public static extern IntPtr blz_commerce_manifest_t_dependencies_get(HandleRef jarg1);

		// Token: 0x0600D2D7 RID: 53975
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_dependency_count_set___")]
		public static extern void blz_commerce_manifest_t_dependency_count_set(HandleRef jarg1, uint jarg2);

		// Token: 0x0600D2D8 RID: 53976
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_dependency_count_get___")]
		public static extern uint blz_commerce_manifest_t_dependency_count_get(HandleRef jarg1);

		// Token: 0x0600D2D9 RID: 53977
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_manifest_t___")]
		public static extern IntPtr new_blz_commerce_manifest_t();

		// Token: 0x0600D2DA RID: 53978
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_manifest_t___")]
		public static extern void delete_blz_commerce_manifest_t(HandleRef jarg1);

		// Token: 0x0600D2DB RID: 53979
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_LOG_SUBSYSTEM_set___")]
		public static extern void BLZ_COMMERCE_LOG_SUBSYSTEM_set(string jarg1);

		// Token: 0x0600D2DC RID: 53980
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_LOG_SUBSYSTEM_get___")]
		public static extern string BLZ_COMMERCE_LOG_SUBSYSTEM_get();

		// Token: 0x0600D2DD RID: 53981
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_ANDROID_ACTIVITY_PARAM_get___")]
		public static extern string BLZ_COMMERCE_ANDROID_ACTIVITY_PARAM_get();

		// Token: 0x0600D2DE RID: 53982
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_ONESTORE_KEY_get___")]
		public static extern string BLZ_COMMERCE_ONESTORE_KEY_get();

		// Token: 0x0600D2DF RID: 53983
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_JAVA_VM_get___")]
		public static extern string BLZ_COMMERCE_JAVA_VM_get();

		// Token: 0x0600D2E0 RID: 53984
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_INVALID_ID_get___")]
		public static extern int BLZ_COMMERCE_INVALID_ID_get();

		// Token: 0x0600D2E1 RID: 53985
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_create___")]
		public static extern IntPtr blz_commerce_create();

		// Token: 0x0600D2E2 RID: 53986
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register___")]
		public static extern IntPtr blz_commerce_register(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D2E3 RID: 53987
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_add_listener___")]
		public static extern IntPtr blz_commerce_add_listener(HandleRef jarg1, IntPtr jarg2, blz_commerce_listener.CallbackDelegate jarg3);

		// Token: 0x0600D2E4 RID: 53988
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register_log___")]
		public static extern IntPtr blz_commerce_register_log(IntPtr jarg1, blz_commerce_log_hook.CallbackDelegate jarg2);

		// Token: 0x0600D2E5 RID: 53989
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_init___")]
		public static extern IntPtr blz_commerce_init(HandleRef jarg1, HandleRef jarg2, int jarg3);

		// Token: 0x0600D2E6 RID: 53990
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_update___")]
		public static extern IntPtr blz_commerce_update(HandleRef jarg1);

		// Token: 0x0600D2E7 RID: 53991
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_terminate___")]
		public static extern IntPtr blz_commerce_terminate(HandleRef jarg1);

		// Token: 0x0600D2E8 RID: 53992
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_event_t_vc_type_set___")]
		public static extern void blz_commerce_vc_event_t_vc_type_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D2E9 RID: 53993
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_event_t_vc_type_get___")]
		public static extern int blz_commerce_vc_event_t_vc_type_get(HandleRef jarg1);

		// Token: 0x0600D2EA RID: 53994
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_event_t_vc_data_set___")]
		public static extern void blz_commerce_vc_event_t_vc_data_set(HandleRef jarg1, IntPtr jarg2);

		// Token: 0x0600D2EB RID: 53995
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_event_t_vc_data_get___")]
		public static extern IntPtr blz_commerce_vc_event_t_vc_data_get(HandleRef jarg1);

		// Token: 0x0600D2EC RID: 53996
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_vc_event_t___")]
		public static extern IntPtr new_blz_commerce_vc_event_t();

		// Token: 0x0600D2ED RID: 53997
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_vc_event_t___")]
		public static extern void delete_blz_commerce_vc_event_t(HandleRef jarg1);

		// Token: 0x0600D2EE RID: 53998
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_get_balance_event_t_http_result_set___")]
		public static extern void blz_commerce_vc_get_balance_event_t_http_result_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D2EF RID: 53999
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_get_balance_event_t_http_result_get___")]
		public static extern IntPtr blz_commerce_vc_get_balance_event_t_http_result_get(HandleRef jarg1);

		// Token: 0x0600D2F0 RID: 54000
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_get_balance_event_t_response_set___")]
		public static extern void blz_commerce_vc_get_balance_event_t_response_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D2F1 RID: 54001
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_get_balance_event_t_response_get___")]
		public static extern string blz_commerce_vc_get_balance_event_t_response_get(HandleRef jarg1);

		// Token: 0x0600D2F2 RID: 54002
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_vc_get_balance_event_t___")]
		public static extern IntPtr new_blz_commerce_vc_get_balance_event_t();

		// Token: 0x0600D2F3 RID: 54003
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_vc_get_balance_event_t___")]
		public static extern void delete_blz_commerce_vc_get_balance_event_t(HandleRef jarg1);

		// Token: 0x0600D2F4 RID: 54004
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_order_event_t_http_result_set___")]
		public static extern void blz_commerce_vc_order_event_t_http_result_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D2F5 RID: 54005
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_order_event_t_http_result_get___")]
		public static extern IntPtr blz_commerce_vc_order_event_t_http_result_get(HandleRef jarg1);

		// Token: 0x0600D2F6 RID: 54006
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_order_event_t_response_set___")]
		public static extern void blz_commerce_vc_order_event_t_response_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D2F7 RID: 54007
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_order_event_t_response_get___")]
		public static extern string blz_commerce_vc_order_event_t_response_get(HandleRef jarg1);

		// Token: 0x0600D2F8 RID: 54008
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_vc_order_event_t___")]
		public static extern IntPtr new_blz_commerce_vc_order_event_t();

		// Token: 0x0600D2F9 RID: 54009
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_vc_order_event_t___")]
		public static extern void delete_blz_commerce_vc_order_event_t(HandleRef jarg1);

		// Token: 0x0600D2FA RID: 54010
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_currency_code_set___")]
		public static extern void blz_commerce_vc_purchase_t_currency_code_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D2FB RID: 54011
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_currency_code_get___")]
		public static extern string blz_commerce_vc_purchase_t_currency_code_get(HandleRef jarg1);

		// Token: 0x0600D2FC RID: 54012
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_product_id_set___")]
		public static extern void blz_commerce_vc_purchase_t_product_id_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D2FD RID: 54013
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_product_id_get___")]
		public static extern int blz_commerce_vc_purchase_t_product_id_get(HandleRef jarg1);

		// Token: 0x0600D2FE RID: 54014
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_game_service_region_id_set___")]
		public static extern void blz_commerce_vc_purchase_t_game_service_region_id_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D2FF RID: 54015
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_game_service_region_id_get___")]
		public static extern int blz_commerce_vc_purchase_t_game_service_region_id_get(HandleRef jarg1);

		// Token: 0x0600D300 RID: 54016
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_quantity_set___")]
		public static extern void blz_commerce_vc_purchase_t_quantity_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D301 RID: 54017
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_quantity_get___")]
		public static extern int blz_commerce_vc_purchase_t_quantity_get(HandleRef jarg1);

		// Token: 0x0600D302 RID: 54018
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_external_transaction_id_set___")]
		public static extern void blz_commerce_vc_purchase_t_external_transaction_id_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D303 RID: 54019
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_external_transaction_id_get___")]
		public static extern string blz_commerce_vc_purchase_t_external_transaction_id_get(HandleRef jarg1);

		// Token: 0x0600D304 RID: 54020
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_title_id_set___")]
		public static extern void blz_commerce_vc_purchase_t_title_id_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D305 RID: 54021
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_title_id_get___")]
		public static extern string blz_commerce_vc_purchase_t_title_id_get(HandleRef jarg1);

		// Token: 0x0600D306 RID: 54022
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_vc_purchase_t___")]
		public static extern IntPtr new_blz_commerce_vc_purchase_t();

		// Token: 0x0600D307 RID: 54023
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_vc_purchase_t___")]
		public static extern void delete_blz_commerce_vc_purchase_t(HandleRef jarg1);

		// Token: 0x0600D308 RID: 54024
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register_vc___")]
		public static extern IntPtr blz_commerce_register_vc();

		// Token: 0x0600D309 RID: 54025
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_get_balance___")]
		public static extern IntPtr blz_commerce_vc_get_balance(HandleRef jarg1, string jarg2);

		// Token: 0x0600D30A RID: 54026
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase___")]
		public static extern IntPtr blz_commerce_vc_purchase(HandleRef jarg1, string jarg2);

		// Token: 0x0600D30B RID: 54027
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vec2d_t_x_set___")]
		public static extern void blz_commerce_vec2d_t_x_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D30C RID: 54028
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vec2d_t_x_get___")]
		public static extern int blz_commerce_vec2d_t_x_get(HandleRef jarg1);

		// Token: 0x0600D30D RID: 54029
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vec2d_t_y_set___")]
		public static extern void blz_commerce_vec2d_t_y_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D30E RID: 54030
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vec2d_t_y_get___")]
		public static extern int blz_commerce_vec2d_t_y_get(HandleRef jarg1);

		// Token: 0x0600D30F RID: 54031
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_vec2d_t___")]
		public static extern IntPtr new_blz_commerce_vec2d_t();

		// Token: 0x0600D310 RID: 54032
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_vec2d_t___")]
		public static extern void delete_blz_commerce_vec2d_t(HandleRef jarg1);

		// Token: 0x0600D311 RID: 54033
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_range_t_from_set___")]
		public static extern void scene_range_t_from_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D312 RID: 54034
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_range_t_from_get___")]
		public static extern int scene_range_t_from_get(HandleRef jarg1);

		// Token: 0x0600D313 RID: 54035
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_range_t_to_set___")]
		public static extern void scene_range_t_to_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D314 RID: 54036
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_range_t_to_get___")]
		public static extern int scene_range_t_to_get(HandleRef jarg1);

		// Token: 0x0600D315 RID: 54037
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_scene_range_t___")]
		public static extern IntPtr new_scene_range_t();

		// Token: 0x0600D316 RID: 54038
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_scene_range_t___")]
		public static extern void delete_scene_range_t(HandleRef jarg1);

		// Token: 0x0600D317 RID: 54039
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_x_set___")]
		public static extern void blz_commerce_scene_rect_t_x_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D318 RID: 54040
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_x_get___")]
		public static extern int blz_commerce_scene_rect_t_x_get(HandleRef jarg1);

		// Token: 0x0600D319 RID: 54041
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_y_set___")]
		public static extern void blz_commerce_scene_rect_t_y_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D31A RID: 54042
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_y_get___")]
		public static extern int blz_commerce_scene_rect_t_y_get(HandleRef jarg1);

		// Token: 0x0600D31B RID: 54043
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_w_set___")]
		public static extern void blz_commerce_scene_rect_t_w_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D31C RID: 54044
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_w_get___")]
		public static extern int blz_commerce_scene_rect_t_w_get(HandleRef jarg1);

		// Token: 0x0600D31D RID: 54045
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_h_set___")]
		public static extern void blz_commerce_scene_rect_t_h_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D31E RID: 54046
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_h_get___")]
		public static extern int blz_commerce_scene_rect_t_h_get(HandleRef jarg1);

		// Token: 0x0600D31F RID: 54047
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_scene_rect_t___")]
		public static extern IntPtr new_blz_commerce_scene_rect_t();

		// Token: 0x0600D320 RID: 54048
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_scene_rect_t___")]
		public static extern void delete_blz_commerce_scene_rect_t(HandleRef jarg1);

		// Token: 0x0600D321 RID: 54049
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_composition_range_changed_event_t_selected_range_set___")]
		public static extern void scene_ime_composition_range_changed_event_t_selected_range_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D322 RID: 54050
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_composition_range_changed_event_t_selected_range_get___")]
		public static extern IntPtr scene_ime_composition_range_changed_event_t_selected_range_get(HandleRef jarg1);

		// Token: 0x0600D323 RID: 54051
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_composition_range_changed_event_t_character_bounds_set___")]
		public static extern void scene_ime_composition_range_changed_event_t_character_bounds_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D324 RID: 54052
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_composition_range_changed_event_t_character_bounds_get___")]
		public static extern IntPtr scene_ime_composition_range_changed_event_t_character_bounds_get(HandleRef jarg1);

		// Token: 0x0600D325 RID: 54053
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_composition_range_changed_event_t_character_bounds_size_set___")]
		public static extern void scene_ime_composition_range_changed_event_t_character_bounds_size_set(HandleRef jarg1, uint jarg2);

		// Token: 0x0600D326 RID: 54054
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_composition_range_changed_event_t_character_bounds_size_get___")]
		public static extern uint scene_ime_composition_range_changed_event_t_character_bounds_size_get(HandleRef jarg1);

		// Token: 0x0600D327 RID: 54055
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_scene_ime_composition_range_changed_event_t___")]
		public static extern IntPtr new_scene_ime_composition_range_changed_event_t();

		// Token: 0x0600D328 RID: 54056
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_scene_ime_composition_range_changed_event_t___")]
		public static extern void delete_scene_ime_composition_range_changed_event_t(HandleRef jarg1);

		// Token: 0x0600D329 RID: 54057
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_state_changed_event_t_input_type_set___")]
		public static extern void scene_ime_state_changed_event_t_input_type_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D32A RID: 54058
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_state_changed_event_t_input_type_get___")]
		public static extern int scene_ime_state_changed_event_t_input_type_get(HandleRef jarg1);

		// Token: 0x0600D32B RID: 54059
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_state_changed_event_t_surrounding_text_set___")]
		public static extern void scene_ime_state_changed_event_t_surrounding_text_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D32C RID: 54060
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_state_changed_event_t_surrounding_text_get___")]
		public static extern string scene_ime_state_changed_event_t_surrounding_text_get(HandleRef jarg1);

		// Token: 0x0600D32D RID: 54061
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_scene_ime_state_changed_event_t___")]
		public static extern IntPtr new_scene_ime_state_changed_event_t();

		// Token: 0x0600D32E RID: 54062
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_scene_ime_state_changed_event_t___")]
		public static extern void delete_scene_ime_state_changed_event_t(HandleRef jarg1);

		// Token: 0x0600D32F RID: 54063
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_text_selection_changed_event_t_text_set___")]
		public static extern void scene_ime_text_selection_changed_event_t_text_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D330 RID: 54064
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_text_selection_changed_event_t_text_get___")]
		public static extern string scene_ime_text_selection_changed_event_t_text_get(HandleRef jarg1);

		// Token: 0x0600D331 RID: 54065
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_text_selection_changed_event_t_offset_set___")]
		public static extern void scene_ime_text_selection_changed_event_t_offset_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D332 RID: 54066
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_text_selection_changed_event_t_offset_get___")]
		public static extern int scene_ime_text_selection_changed_event_t_offset_get(HandleRef jarg1);

		// Token: 0x0600D333 RID: 54067
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_text_selection_changed_event_t_selected_range_set___")]
		public static extern void scene_ime_text_selection_changed_event_t_selected_range_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D334 RID: 54068
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_text_selection_changed_event_t_selected_range_get___")]
		public static extern IntPtr scene_ime_text_selection_changed_event_t_selected_range_get(HandleRef jarg1);

		// Token: 0x0600D335 RID: 54069
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_scene_ime_text_selection_changed_event_t___")]
		public static extern IntPtr new_scene_ime_text_selection_changed_event_t();

		// Token: 0x0600D336 RID: 54070
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_scene_ime_text_selection_changed_event_t___")]
		public static extern void delete_scene_ime_text_selection_changed_event_t(HandleRef jarg1);

		// Token: 0x0600D337 RID: 54071
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_selection_bounds_changed_event_t_anchor_rect_set___")]
		public static extern void scene_ime_selection_bounds_changed_event_t_anchor_rect_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D338 RID: 54072
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_selection_bounds_changed_event_t_anchor_rect_get___")]
		public static extern IntPtr scene_ime_selection_bounds_changed_event_t_anchor_rect_get(HandleRef jarg1);

		// Token: 0x0600D339 RID: 54073
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_selection_bounds_changed_event_t_focus_rect_set___")]
		public static extern void scene_ime_selection_bounds_changed_event_t_focus_rect_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D33A RID: 54074
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_selection_bounds_changed_event_t_focus_rect_get___")]
		public static extern IntPtr scene_ime_selection_bounds_changed_event_t_focus_rect_get(HandleRef jarg1);

		// Token: 0x0600D33B RID: 54075
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_selection_bounds_changed_event_t_is_anchor_rect_set___")]
		public static extern void scene_ime_selection_bounds_changed_event_t_is_anchor_rect_set(HandleRef jarg1, bool jarg2);

		// Token: 0x0600D33C RID: 54076
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_selection_bounds_changed_event_t_is_anchor_rect_get___")]
		public static extern bool scene_ime_selection_bounds_changed_event_t_is_anchor_rect_get(HandleRef jarg1);

		// Token: 0x0600D33D RID: 54077
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_scene_ime_selection_bounds_changed_event_t___")]
		public static extern IntPtr new_scene_ime_selection_bounds_changed_event_t();

		// Token: 0x0600D33E RID: 54078
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_scene_ime_selection_bounds_changed_event_t___")]
		public static extern void delete_scene_ime_selection_bounds_changed_event_t(HandleRef jarg1);

		// Token: 0x0600D33F RID: 54079
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_event_t_scene_type_set___")]
		public static extern void blz_commerce_scene_event_t_scene_type_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D340 RID: 54080
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_event_t_scene_type_get___")]
		public static extern int blz_commerce_scene_event_t_scene_type_get(HandleRef jarg1);

		// Token: 0x0600D341 RID: 54081
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_event_t_scene_data_set___")]
		public static extern void blz_commerce_scene_event_t_scene_data_set(HandleRef jarg1, IntPtr jarg2);

		// Token: 0x0600D342 RID: 54082
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_event_t_scene_data_get___")]
		public static extern IntPtr blz_commerce_scene_event_t_scene_data_get(HandleRef jarg1);

		// Token: 0x0600D343 RID: 54083
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_scene_event_t___")]
		public static extern IntPtr new_blz_commerce_scene_event_t();

		// Token: 0x0600D344 RID: 54084
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_scene_event_t___")]
		public static extern void delete_blz_commerce_scene_event_t(HandleRef jarg1);

		// Token: 0x0600D345 RID: 54085
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_buffer_t_bytes_set___")]
		public static extern void blz_commerce_buffer_t_bytes_set(HandleRef jarg1, IntPtr jarg2);

		// Token: 0x0600D346 RID: 54086
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_buffer_t_bytes_get___")]
		public static extern IntPtr blz_commerce_buffer_t_bytes_get(HandleRef jarg1);

		// Token: 0x0600D347 RID: 54087
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_buffer_t_len_set___")]
		public static extern void blz_commerce_buffer_t_len_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D348 RID: 54088
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_buffer_t_len_get___")]
		public static extern int blz_commerce_buffer_t_len_get(HandleRef jarg1);

		// Token: 0x0600D349 RID: 54089
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_buffer_t___")]
		public static extern IntPtr new_blz_commerce_buffer_t();

		// Token: 0x0600D34A RID: 54090
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_buffer_t___")]
		public static extern void delete_blz_commerce_buffer_t(HandleRef jarg1);

		// Token: 0x0600D34B RID: 54091
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_buffer_update_event_t_dirty_rects_set___")]
		public static extern void blz_commerce_scene_buffer_update_event_t_dirty_rects_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D34C RID: 54092
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_buffer_update_event_t_dirty_rects_get___")]
		public static extern IntPtr blz_commerce_scene_buffer_update_event_t_dirty_rects_get(HandleRef jarg1);

		// Token: 0x0600D34D RID: 54093
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_buffer_update_event_t_dirty_rect_size_set___")]
		public static extern void blz_commerce_scene_buffer_update_event_t_dirty_rect_size_set(HandleRef jarg1, uint jarg2);

		// Token: 0x0600D34E RID: 54094
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_buffer_update_event_t_dirty_rect_size_get___")]
		public static extern uint blz_commerce_scene_buffer_update_event_t_dirty_rect_size_get(HandleRef jarg1);

		// Token: 0x0600D34F RID: 54095
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_buffer_update_event_t_buffer_set___")]
		public static extern void blz_commerce_scene_buffer_update_event_t_buffer_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D350 RID: 54096
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_buffer_update_event_t_buffer_get___")]
		public static extern IntPtr blz_commerce_scene_buffer_update_event_t_buffer_get(HandleRef jarg1);

		// Token: 0x0600D351 RID: 54097
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_scene_buffer_update_event_t___")]
		public static extern IntPtr new_blz_commerce_scene_buffer_update_event_t();

		// Token: 0x0600D352 RID: 54098
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_scene_buffer_update_event_t___")]
		public static extern void delete_blz_commerce_scene_buffer_update_event_t(HandleRef jarg1);

		// Token: 0x0600D353 RID: 54099
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_window_resize_event_t_win_size_set___")]
		public static extern void blz_commerce_scene_window_resize_event_t_win_size_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D354 RID: 54100
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_window_resize_event_t_win_size_get___")]
		public static extern IntPtr blz_commerce_scene_window_resize_event_t_win_size_get(HandleRef jarg1);

		// Token: 0x0600D355 RID: 54101
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_window_resize_event_t_buffer_size_set___")]
		public static extern void blz_commerce_scene_window_resize_event_t_buffer_size_set(HandleRef jarg1, uint jarg2);

		// Token: 0x0600D356 RID: 54102
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_window_resize_event_t_buffer_size_get___")]
		public static extern uint blz_commerce_scene_window_resize_event_t_buffer_size_get(HandleRef jarg1);

		// Token: 0x0600D357 RID: 54103
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_scene_window_resize_event_t___")]
		public static extern IntPtr new_blz_commerce_scene_window_resize_event_t();

		// Token: 0x0600D358 RID: 54104
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_scene_window_resize_event_t___")]
		public static extern void delete_blz_commerce_scene_window_resize_event_t(HandleRef jarg1);

		// Token: 0x0600D359 RID: 54105
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_window_resize_requested_event_t_requested_size_set___")]
		public static extern void blz_commerce_scene_window_resize_requested_event_t_requested_size_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D35A RID: 54106
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_window_resize_requested_event_t_requested_size_get___")]
		public static extern IntPtr blz_commerce_scene_window_resize_requested_event_t_requested_size_get(HandleRef jarg1);

		// Token: 0x0600D35B RID: 54107
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_scene_window_resize_requested_event_t___")]
		public static extern IntPtr new_blz_commerce_scene_window_resize_requested_event_t();

		// Token: 0x0600D35C RID: 54108
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_scene_window_resize_requested_event_t___")]
		public static extern void delete_blz_commerce_scene_window_resize_requested_event_t(HandleRef jarg1);

		// Token: 0x0600D35D RID: 54109
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_external_link_event_t_url_set___")]
		public static extern void blz_commerce_scene_external_link_event_t_url_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D35E RID: 54110
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_external_link_event_t_url_get___")]
		public static extern string blz_commerce_scene_external_link_event_t_url_get(HandleRef jarg1);

		// Token: 0x0600D35F RID: 54111
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_scene_external_link_event_t___")]
		public static extern IntPtr new_blz_commerce_scene_external_link_event_t();

		// Token: 0x0600D360 RID: 54112
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_scene_external_link_event_t___")]
		public static extern void delete_blz_commerce_scene_external_link_event_t(HandleRef jarg1);

		// Token: 0x0600D361 RID: 54113
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_keyCode_set___")]
		public static extern void blz_commerce_key_input_t_keyCode_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D362 RID: 54114
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_keyCode_get___")]
		public static extern int blz_commerce_key_input_t_keyCode_get(HandleRef jarg1);

		// Token: 0x0600D363 RID: 54115
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_type_set___")]
		public static extern void blz_commerce_key_input_t_type_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D364 RID: 54116
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_type_get___")]
		public static extern int blz_commerce_key_input_t_type_get(HandleRef jarg1);

		// Token: 0x0600D365 RID: 54117
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_character_set___")]
		public static extern void blz_commerce_key_input_t_character_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D366 RID: 54118
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_character_get___")]
		public static extern int blz_commerce_key_input_t_character_get(HandleRef jarg1);

		// Token: 0x0600D367 RID: 54119
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_modifiers_set___")]
		public static extern void blz_commerce_key_input_t_modifiers_set(HandleRef jarg1, uint jarg2);

		// Token: 0x0600D368 RID: 54120
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_modifiers_get___")]
		public static extern uint blz_commerce_key_input_t_modifiers_get(HandleRef jarg1);

		// Token: 0x0600D369 RID: 54121
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_key_input_t___")]
		public static extern IntPtr new_blz_commerce_key_input_t();

		// Token: 0x0600D36A RID: 54122
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_key_input_t___")]
		public static extern void delete_blz_commerce_key_input_t(HandleRef jarg1);

		// Token: 0x0600D36B RID: 54123
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_button_set___")]
		public static extern void blz_commerce_mouse_input_t_button_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D36C RID: 54124
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_button_get___")]
		public static extern int blz_commerce_mouse_input_t_button_get(HandleRef jarg1);

		// Token: 0x0600D36D RID: 54125
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_type_set___")]
		public static extern void blz_commerce_mouse_input_t_type_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D36E RID: 54126
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_type_get___")]
		public static extern int blz_commerce_mouse_input_t_type_get(HandleRef jarg1);

		// Token: 0x0600D36F RID: 54127
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_coords_set___")]
		public static extern void blz_commerce_mouse_input_t_coords_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D370 RID: 54128
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_coords_get___")]
		public static extern IntPtr blz_commerce_mouse_input_t_coords_get(HandleRef jarg1);

		// Token: 0x0600D371 RID: 54129
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_mods_set___")]
		public static extern void blz_commerce_mouse_input_t_mods_set(HandleRef jarg1, uint jarg2);

		// Token: 0x0600D372 RID: 54130
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_mods_get___")]
		public static extern uint blz_commerce_mouse_input_t_mods_get(HandleRef jarg1);

		// Token: 0x0600D373 RID: 54131
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_mouse_input_t___")]
		public static extern IntPtr new_blz_commerce_mouse_input_t();

		// Token: 0x0600D374 RID: 54132
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_mouse_input_t___")]
		public static extern void delete_blz_commerce_mouse_input_t(HandleRef jarg1);

		// Token: 0x0600D375 RID: 54133
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_character_input_t_character_set___")]
		public static extern void blz_commerce_character_input_t_character_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D376 RID: 54134
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_character_input_t_character_get___")]
		public static extern int blz_commerce_character_input_t_character_get(HandleRef jarg1);

		// Token: 0x0600D377 RID: 54135
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_character_input_t_modifiers_set___")]
		public static extern void blz_commerce_character_input_t_modifiers_set(HandleRef jarg1, uint jarg2);

		// Token: 0x0600D378 RID: 54136
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_character_input_t_modifiers_get___")]
		public static extern uint blz_commerce_character_input_t_modifiers_get(HandleRef jarg1);

		// Token: 0x0600D379 RID: 54137
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_character_input_t___")]
		public static extern IntPtr new_blz_commerce_character_input_t();

		// Token: 0x0600D37A RID: 54138
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_character_input_t___")]
		public static extern void delete_blz_commerce_character_input_t(HandleRef jarg1);

		// Token: 0x0600D37B RID: 54139
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_move_t_coords_set___")]
		public static extern void blz_commerce_mouse_move_t_coords_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D37C RID: 54140
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_move_t_coords_get___")]
		public static extern IntPtr blz_commerce_mouse_move_t_coords_get(HandleRef jarg1);

		// Token: 0x0600D37D RID: 54141
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_move_t_mod_set___")]
		public static extern void blz_commerce_mouse_move_t_mod_set(HandleRef jarg1, uint jarg2);

		// Token: 0x0600D37E RID: 54142
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_move_t_mod_get___")]
		public static extern uint blz_commerce_mouse_move_t_mod_get(HandleRef jarg1);

		// Token: 0x0600D37F RID: 54143
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_mouse_move_t___")]
		public static extern IntPtr new_blz_commerce_mouse_move_t();

		// Token: 0x0600D380 RID: 54144
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_mouse_move_t___")]
		public static extern void delete_blz_commerce_mouse_move_t(HandleRef jarg1);

		// Token: 0x0600D381 RID: 54145
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_wheel_t_delta_set___")]
		public static extern void blz_commerce_mouse_wheel_t_delta_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D382 RID: 54146
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_wheel_t_delta_get___")]
		public static extern int blz_commerce_mouse_wheel_t_delta_get(HandleRef jarg1);

		// Token: 0x0600D383 RID: 54147
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_wheel_t_coords_set___")]
		public static extern void blz_commerce_mouse_wheel_t_coords_set(HandleRef jarg1, HandleRef jarg2);

		// Token: 0x0600D384 RID: 54148
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_wheel_t_coords_get___")]
		public static extern IntPtr blz_commerce_mouse_wheel_t_coords_get(HandleRef jarg1);

		// Token: 0x0600D385 RID: 54149
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_wheel_t_mod_set___")]
		public static extern void blz_commerce_mouse_wheel_t_mod_set(HandleRef jarg1, uint jarg2);

		// Token: 0x0600D386 RID: 54150
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_wheel_t_mod_get___")]
		public static extern uint blz_commerce_mouse_wheel_t_mod_get(HandleRef jarg1);

		// Token: 0x0600D387 RID: 54151
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_mouse_wheel_t___")]
		public static extern IntPtr new_blz_commerce_mouse_wheel_t();

		// Token: 0x0600D388 RID: 54152
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_mouse_wheel_t___")]
		public static extern void delete_blz_commerce_mouse_wheel_t(HandleRef jarg1);

		// Token: 0x0600D389 RID: 54153
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_window_close_t_browser_id_set___")]
		public static extern void blz_commerce_window_close_t_browser_id_set(HandleRef jarg1, uint jarg2);

		// Token: 0x0600D38A RID: 54154
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_window_close_t_browser_id_get___")]
		public static extern uint blz_commerce_window_close_t_browser_id_get(HandleRef jarg1);

		// Token: 0x0600D38B RID: 54155
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_window_close_t___")]
		public static extern IntPtr new_blz_commerce_window_close_t();

		// Token: 0x0600D38C RID: 54156
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_window_close_t___")]
		public static extern void delete_blz_commerce_window_close_t(HandleRef jarg1);

		// Token: 0x0600D38D RID: 54157
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_window_width_set___")]
		public static extern void blz_commerce_browser_params_t_window_width_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D38E RID: 54158
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_window_width_get___")]
		public static extern int blz_commerce_browser_params_t_window_width_get(HandleRef jarg1);

		// Token: 0x0600D38F RID: 54159
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_window_height_set___")]
		public static extern void blz_commerce_browser_params_t_window_height_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D390 RID: 54160
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_window_height_get___")]
		public static extern int blz_commerce_browser_params_t_window_height_get(HandleRef jarg1);

		// Token: 0x0600D391 RID: 54161
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_max_window_width_set___")]
		public static extern void blz_commerce_browser_params_t_max_window_width_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D392 RID: 54162
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_max_window_width_get___")]
		public static extern int blz_commerce_browser_params_t_max_window_width_get(HandleRef jarg1);

		// Token: 0x0600D393 RID: 54163
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_max_window_height_set___")]
		public static extern void blz_commerce_browser_params_t_max_window_height_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D394 RID: 54164
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_max_window_height_get___")]
		public static extern int blz_commerce_browser_params_t_max_window_height_get(HandleRef jarg1);

		// Token: 0x0600D395 RID: 54165
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_log_directory_set___")]
		public static extern void blz_commerce_browser_params_t_log_directory_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D396 RID: 54166
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_log_directory_get___")]
		public static extern string blz_commerce_browser_params_t_log_directory_get(HandleRef jarg1);

		// Token: 0x0600D397 RID: 54167
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_browser_directory_set___")]
		public static extern void blz_commerce_browser_params_t_browser_directory_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D398 RID: 54168
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_browser_directory_get___")]
		public static extern string blz_commerce_browser_params_t_browser_directory_get(HandleRef jarg1);

		// Token: 0x0600D399 RID: 54169
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_is_prod_set___")]
		public static extern void blz_commerce_browser_params_t_is_prod_set(HandleRef jarg1, bool jarg2);

		// Token: 0x0600D39A RID: 54170
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_is_prod_get___")]
		public static extern bool blz_commerce_browser_params_t_is_prod_get(HandleRef jarg1);

		// Token: 0x0600D39B RID: 54171
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_browser_params_t___")]
		public static extern IntPtr new_blz_commerce_browser_params_t();

		// Token: 0x0600D39C RID: 54172
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_browser_params_t___")]
		public static extern void delete_blz_commerce_browser_params_t(HandleRef jarg1);

		// Token: 0x0600D39D RID: 54173
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_BROWSER_PARAM_get___")]
		public static extern string BLZ_COMMERCE_BROWSER_PARAM_get();

		// Token: 0x0600D39E RID: 54174
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register_scene___")]
		public static extern IntPtr blz_commerce_register_scene();

		// Token: 0x0600D39F RID: 54175
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_send_event___")]
		public static extern IntPtr blz_commerce_browser_send_event(HandleRef jarg1, int jarg2, IntPtr jarg3);

		// Token: 0x0600D3A0 RID: 54176
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_event_t_http_type_set___")]
		public static extern void blz_commerce_http_event_t_http_type_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D3A1 RID: 54177
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_event_t_http_type_get___")]
		public static extern int blz_commerce_http_event_t_http_type_get(HandleRef jarg1);

		// Token: 0x0600D3A2 RID: 54178
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_event_t_http_data_set___")]
		public static extern void blz_commerce_http_event_t_http_data_set(HandleRef jarg1, IntPtr jarg2);

		// Token: 0x0600D3A3 RID: 54179
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_event_t_http_data_get___")]
		public static extern IntPtr blz_commerce_http_event_t_http_data_get(HandleRef jarg1);

		// Token: 0x0600D3A4 RID: 54180
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_http_event_t___")]
		public static extern IntPtr new_blz_commerce_http_event_t();

		// Token: 0x0600D3A5 RID: 54181
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_http_event_t___")]
		public static extern void delete_blz_commerce_http_event_t(HandleRef jarg1);

		// Token: 0x0600D3A6 RID: 54182
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_http_need_auth_event_t___")]
		public static extern IntPtr new_blz_commerce_http_need_auth_event_t();

		// Token: 0x0600D3A7 RID: 54183
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_http_need_auth_event_t___")]
		public static extern void delete_blz_commerce_http_need_auth_event_t(HandleRef jarg1);

		// Token: 0x0600D3A8 RID: 54184
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_client_id_set___")]
		public static extern void blz_commerce_http_params_t_client_id_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D3A9 RID: 54185
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_client_id_get___")]
		public static extern string blz_commerce_http_params_t_client_id_get(HandleRef jarg1);

		// Token: 0x0600D3AA RID: 54186
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_token_set___")]
		public static extern void blz_commerce_http_params_t_token_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D3AB RID: 54187
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_token_get___")]
		public static extern string blz_commerce_http_params_t_token_get(HandleRef jarg1);

		// Token: 0x0600D3AC RID: 54188
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_title_code_set___")]
		public static extern void blz_commerce_http_params_t_title_code_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D3AD RID: 54189
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_title_code_get___")]
		public static extern string blz_commerce_http_params_t_title_code_get(HandleRef jarg1);

		// Token: 0x0600D3AE RID: 54190
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_title_version_set___")]
		public static extern void blz_commerce_http_params_t_title_version_set(HandleRef jarg1, string jarg2);

		// Token: 0x0600D3AF RID: 54191
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_title_version_get___")]
		public static extern string blz_commerce_http_params_t_title_version_get(HandleRef jarg1);

		// Token: 0x0600D3B0 RID: 54192
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_region_set___")]
		public static extern void blz_commerce_http_params_t_region_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D3B1 RID: 54193
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_region_get___")]
		public static extern int blz_commerce_http_params_t_region_get(HandleRef jarg1);

		// Token: 0x0600D3B2 RID: 54194
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_environment_set___")]
		public static extern void blz_commerce_http_params_t_environment_set(HandleRef jarg1, int jarg2);

		// Token: 0x0600D3B3 RID: 54195
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_environment_get___")]
		public static extern int blz_commerce_http_params_t_environment_get(HandleRef jarg1);

		// Token: 0x0600D3B4 RID: 54196
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_http_params_t___")]
		public static extern IntPtr new_blz_commerce_http_params_t();

		// Token: 0x0600D3B5 RID: 54197
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_http_params_t___")]
		public static extern void delete_blz_commerce_http_params_t(HandleRef jarg1);

		// Token: 0x0600D3B6 RID: 54198
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_HTTP_PARAM_get___")]
		public static extern string BLZ_COMMERCE_HTTP_PARAM_get();

		// Token: 0x0600D3B7 RID: 54199
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register_http___")]
		public static extern IntPtr blz_commerce_register_http();

		// Token: 0x0600D3B8 RID: 54200
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_refresh_auth___")]
		public static extern IntPtr blz_commerce_http_refresh_auth(HandleRef jarg1, string jarg2);

		// Token: 0x0600D3B9 RID: 54201
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_generate_transaction_id___")]
		public static extern string blz_commerce_generate_transaction_id(int jarg1, int jarg2);

		// Token: 0x0600D3BA RID: 54202
		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_program_to_4cc___")]
		public static extern int blz_commerce_program_to_4cc(string jarg1);

		// Token: 0x0400A31D RID: 41757
		protected static battlenet_commercePINVOKE.SWIGStringHelper swigStringHelper = new battlenet_commercePINVOKE.SWIGStringHelper();

		// Token: 0x0400A31E RID: 41758
		private const string DLL_NAME = "blz_commerce_sdk_plugin";

		// Token: 0x0200295F RID: 10591
		protected class SWIGExceptionHelper
		{
			// Token: 0x06013EC5 RID: 81605
			[DllImport("blz_commerce_sdk_plugin")]
			public static extern void SWIGRegisterExceptionCallbacks_battlenet_commerce(battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate applicationDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate arithmeticDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate divideByZeroDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate indexOutOfRangeDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate invalidCastDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate invalidOperationDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate ioDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate nullReferenceDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate outOfMemoryDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate overflowDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate systemExceptionDelegate);

			// Token: 0x06013EC6 RID: 81606
			[DllImport("blz_commerce_sdk_plugin", EntryPoint = "SWIGRegisterExceptionArgumentCallbacks_battlenet_commerce")]
			public static extern void SWIGRegisterExceptionCallbacksArgument_battlenet_commerce(battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate argumentDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate argumentNullDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate argumentOutOfRangeDelegate);

			// Token: 0x06013EC7 RID: 81607 RVA: 0x005412A0 File Offset: 0x0053F4A0
			private static void SetPendingApplicationException(string message)
			{
				battlenet_commercePINVOKE.SWIGPendingException.Set(new ApplicationException(message, battlenet_commercePINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06013EC8 RID: 81608 RVA: 0x005412B2 File Offset: 0x0053F4B2
			private static void SetPendingArithmeticException(string message)
			{
				battlenet_commercePINVOKE.SWIGPendingException.Set(new ArithmeticException(message, battlenet_commercePINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06013EC9 RID: 81609 RVA: 0x005412C4 File Offset: 0x0053F4C4
			private static void SetPendingDivideByZeroException(string message)
			{
				battlenet_commercePINVOKE.SWIGPendingException.Set(new DivideByZeroException(message, battlenet_commercePINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06013ECA RID: 81610 RVA: 0x005412D6 File Offset: 0x0053F4D6
			private static void SetPendingIndexOutOfRangeException(string message)
			{
				battlenet_commercePINVOKE.SWIGPendingException.Set(new IndexOutOfRangeException(message, battlenet_commercePINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06013ECB RID: 81611 RVA: 0x005412E8 File Offset: 0x0053F4E8
			private static void SetPendingInvalidCastException(string message)
			{
				battlenet_commercePINVOKE.SWIGPendingException.Set(new InvalidCastException(message, battlenet_commercePINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06013ECC RID: 81612 RVA: 0x005412FA File Offset: 0x0053F4FA
			private static void SetPendingInvalidOperationException(string message)
			{
				battlenet_commercePINVOKE.SWIGPendingException.Set(new InvalidOperationException(message, battlenet_commercePINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06013ECD RID: 81613 RVA: 0x0054130C File Offset: 0x0053F50C
			private static void SetPendingIOException(string message)
			{
				battlenet_commercePINVOKE.SWIGPendingException.Set(new IOException(message, battlenet_commercePINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06013ECE RID: 81614 RVA: 0x0054131E File Offset: 0x0053F51E
			private static void SetPendingNullReferenceException(string message)
			{
				battlenet_commercePINVOKE.SWIGPendingException.Set(new NullReferenceException(message, battlenet_commercePINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06013ECF RID: 81615 RVA: 0x00541330 File Offset: 0x0053F530
			private static void SetPendingOutOfMemoryException(string message)
			{
				battlenet_commercePINVOKE.SWIGPendingException.Set(new OutOfMemoryException(message, battlenet_commercePINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06013ED0 RID: 81616 RVA: 0x00541342 File Offset: 0x0053F542
			private static void SetPendingOverflowException(string message)
			{
				battlenet_commercePINVOKE.SWIGPendingException.Set(new OverflowException(message, battlenet_commercePINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06013ED1 RID: 81617 RVA: 0x00541354 File Offset: 0x0053F554
			private static void SetPendingSystemException(string message)
			{
				battlenet_commercePINVOKE.SWIGPendingException.Set(new SystemException(message, battlenet_commercePINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06013ED2 RID: 81618 RVA: 0x00541366 File Offset: 0x0053F566
			private static void SetPendingArgumentException(string message, string paramName)
			{
				battlenet_commercePINVOKE.SWIGPendingException.Set(new ArgumentException(message, paramName, battlenet_commercePINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06013ED3 RID: 81619 RVA: 0x0054137C File Offset: 0x0053F57C
			private static void SetPendingArgumentNullException(string message, string paramName)
			{
				Exception ex = battlenet_commercePINVOKE.SWIGPendingException.Retrieve();
				if (ex != null)
				{
					message = message + " Inner Exception: " + ex.Message;
				}
				battlenet_commercePINVOKE.SWIGPendingException.Set(new ArgumentNullException(paramName, message));
			}

			// Token: 0x06013ED4 RID: 81620 RVA: 0x005413B4 File Offset: 0x0053F5B4
			private static void SetPendingArgumentOutOfRangeException(string message, string paramName)
			{
				Exception ex = battlenet_commercePINVOKE.SWIGPendingException.Retrieve();
				if (ex != null)
				{
					message = message + " Inner Exception: " + ex.Message;
				}
				battlenet_commercePINVOKE.SWIGPendingException.Set(new ArgumentOutOfRangeException(paramName, message));
			}

			// Token: 0x06013ED5 RID: 81621 RVA: 0x005413EC File Offset: 0x0053F5EC
			static SWIGExceptionHelper()
			{
				battlenet_commercePINVOKE.SWIGExceptionHelper.SWIGRegisterExceptionCallbacks_battlenet_commerce(battlenet_commercePINVOKE.SWIGExceptionHelper.applicationDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.arithmeticDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.divideByZeroDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.indexOutOfRangeDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.invalidCastDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.invalidOperationDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.ioDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.nullReferenceDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.outOfMemoryDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.overflowDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.systemDelegate);
				battlenet_commercePINVOKE.SWIGExceptionHelper.SWIGRegisterExceptionCallbacksArgument_battlenet_commerce(battlenet_commercePINVOKE.SWIGExceptionHelper.argumentDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.argumentNullDelegate, battlenet_commercePINVOKE.SWIGExceptionHelper.argumentOutOfRangeDelegate);
			}

			// Token: 0x0400FCC3 RID: 64707
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate applicationDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingApplicationException);

			// Token: 0x0400FCC4 RID: 64708
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate arithmeticDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingArithmeticException);

			// Token: 0x0400FCC5 RID: 64709
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate divideByZeroDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingDivideByZeroException);

			// Token: 0x0400FCC6 RID: 64710
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate indexOutOfRangeDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingIndexOutOfRangeException);

			// Token: 0x0400FCC7 RID: 64711
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate invalidCastDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingInvalidCastException);

			// Token: 0x0400FCC8 RID: 64712
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate invalidOperationDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingInvalidOperationException);

			// Token: 0x0400FCC9 RID: 64713
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate ioDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingIOException);

			// Token: 0x0400FCCA RID: 64714
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate nullReferenceDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingNullReferenceException);

			// Token: 0x0400FCCB RID: 64715
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate outOfMemoryDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingOutOfMemoryException);

			// Token: 0x0400FCCC RID: 64716
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate overflowDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingOverflowException);

			// Token: 0x0400FCCD RID: 64717
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate systemDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingSystemException);

			// Token: 0x0400FCCE RID: 64718
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate argumentDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingArgumentException);

			// Token: 0x0400FCCF RID: 64719
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate argumentNullDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingArgumentNullException);

			// Token: 0x0400FCD0 RID: 64720
			private static battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate argumentOutOfRangeDelegate = new battlenet_commercePINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate(battlenet_commercePINVOKE.SWIGExceptionHelper.SetPendingArgumentOutOfRangeException);

			// Token: 0x020029B5 RID: 10677
			// (Invoke) Token: 0x06013FC7 RID: 81863
			public delegate void ExceptionDelegate(string message);

			// Token: 0x020029B6 RID: 10678
			// (Invoke) Token: 0x06013FCB RID: 81867
			public delegate void ExceptionArgumentDelegate(string message, string paramName);
		}

		// Token: 0x02002960 RID: 10592
		public class SWIGPendingException
		{
			// Token: 0x17002D87 RID: 11655
			// (get) Token: 0x06013ED7 RID: 81623 RVA: 0x00541538 File Offset: 0x0053F738
			public static bool Pending
			{
				get
				{
					bool result = false;
					if (battlenet_commercePINVOKE.SWIGPendingException.numExceptionsPending > 0 && battlenet_commercePINVOKE.SWIGPendingException.pendingException != null)
					{
						result = true;
					}
					return result;
				}
			}

			// Token: 0x06013ED8 RID: 81624 RVA: 0x0054155C File Offset: 0x0053F75C
			public static void Set(Exception e)
			{
				if (battlenet_commercePINVOKE.SWIGPendingException.pendingException != null)
				{
					throw new ApplicationException("FATAL: An earlier pending exception from unmanaged code was missed and thus not thrown (" + battlenet_commercePINVOKE.SWIGPendingException.pendingException.ToString() + ")", e);
				}
				battlenet_commercePINVOKE.SWIGPendingException.pendingException = e;
				object obj = battlenet_commercePINVOKE.SWIGPendingException.exceptionsLock;
				lock (obj)
				{
					battlenet_commercePINVOKE.SWIGPendingException.numExceptionsPending++;
				}
			}

			// Token: 0x06013ED9 RID: 81625 RVA: 0x005415D0 File Offset: 0x0053F7D0
			public static Exception Retrieve()
			{
				Exception result = null;
				if (battlenet_commercePINVOKE.SWIGPendingException.numExceptionsPending > 0 && battlenet_commercePINVOKE.SWIGPendingException.pendingException != null)
				{
					result = battlenet_commercePINVOKE.SWIGPendingException.pendingException;
					battlenet_commercePINVOKE.SWIGPendingException.pendingException = null;
					object obj = battlenet_commercePINVOKE.SWIGPendingException.exceptionsLock;
					lock (obj)
					{
						battlenet_commercePINVOKE.SWIGPendingException.numExceptionsPending--;
					}
				}
				return result;
			}

			// Token: 0x0400FCD1 RID: 64721
			[ThreadStatic]
			private static Exception pendingException;

			// Token: 0x0400FCD2 RID: 64722
			private static int numExceptionsPending;

			// Token: 0x0400FCD3 RID: 64723
			private static object exceptionsLock = new object();
		}

		// Token: 0x02002961 RID: 10593
		protected class SWIGStringHelper
		{
			// Token: 0x06013EDC RID: 81628
			[DllImport("blz_commerce_sdk_plugin")]
			public static extern void SWIGRegisterStringCallback_battlenet_commerce(battlenet_commercePINVOKE.SWIGStringHelper.SWIGStringDelegate stringDelegate);

			// Token: 0x06013EDD RID: 81629 RVA: 0x00005576 File Offset: 0x00003776
			[MonoPInvokeCallback(typeof(battlenet_commercePINVOKE.SWIGStringHelper.SWIGStringDelegate))]
			private static string CreateString(string cString)
			{
				return cString;
			}

			// Token: 0x06013EDE RID: 81630 RVA: 0x00541640 File Offset: 0x0053F840
			static SWIGStringHelper()
			{
				battlenet_commercePINVOKE.SWIGStringHelper.SWIGRegisterStringCallback_battlenet_commerce(battlenet_commercePINVOKE.SWIGStringHelper.stringDelegate);
			}

			// Token: 0x0400FCD4 RID: 64724
			private static battlenet_commercePINVOKE.SWIGStringHelper.SWIGStringDelegate stringDelegate = new battlenet_commercePINVOKE.SWIGStringHelper.SWIGStringDelegate(battlenet_commercePINVOKE.SWIGStringHelper.CreateString);

			// Token: 0x020029B7 RID: 10679
			// (Invoke) Token: 0x06013FCF RID: 81871
			public delegate string SWIGStringDelegate(string message);
		}
	}
}
