using System;
using System.IO;
using System.Runtime.InteropServices;
using AOT;

namespace Blizzard.Commerce
{
	internal class battlenet_commercePINVOKE
	{
		protected class SWIGExceptionHelper
		{
			public delegate void ExceptionDelegate(string message);

			public delegate void ExceptionArgumentDelegate(string message, string paramName);

			private static ExceptionDelegate applicationDelegate;

			private static ExceptionDelegate arithmeticDelegate;

			private static ExceptionDelegate divideByZeroDelegate;

			private static ExceptionDelegate indexOutOfRangeDelegate;

			private static ExceptionDelegate invalidCastDelegate;

			private static ExceptionDelegate invalidOperationDelegate;

			private static ExceptionDelegate ioDelegate;

			private static ExceptionDelegate nullReferenceDelegate;

			private static ExceptionDelegate outOfMemoryDelegate;

			private static ExceptionDelegate overflowDelegate;

			private static ExceptionDelegate systemDelegate;

			private static ExceptionArgumentDelegate argumentDelegate;

			private static ExceptionArgumentDelegate argumentNullDelegate;

			private static ExceptionArgumentDelegate argumentOutOfRangeDelegate;

			[DllImport("blz_commerce_sdk_plugin")]
			public static extern void SWIGRegisterExceptionCallbacks_battlenet_commerce(ExceptionDelegate applicationDelegate, ExceptionDelegate arithmeticDelegate, ExceptionDelegate divideByZeroDelegate, ExceptionDelegate indexOutOfRangeDelegate, ExceptionDelegate invalidCastDelegate, ExceptionDelegate invalidOperationDelegate, ExceptionDelegate ioDelegate, ExceptionDelegate nullReferenceDelegate, ExceptionDelegate outOfMemoryDelegate, ExceptionDelegate overflowDelegate, ExceptionDelegate systemExceptionDelegate);

			[DllImport("blz_commerce_sdk_plugin", EntryPoint = "SWIGRegisterExceptionArgumentCallbacks_battlenet_commerce")]
			public static extern void SWIGRegisterExceptionCallbacksArgument_battlenet_commerce(ExceptionArgumentDelegate argumentDelegate, ExceptionArgumentDelegate argumentNullDelegate, ExceptionArgumentDelegate argumentOutOfRangeDelegate);

			private static void SetPendingApplicationException(string message)
			{
				SWIGPendingException.Set(new ApplicationException(message, SWIGPendingException.Retrieve()));
			}

			private static void SetPendingArithmeticException(string message)
			{
				SWIGPendingException.Set(new ArithmeticException(message, SWIGPendingException.Retrieve()));
			}

			private static void SetPendingDivideByZeroException(string message)
			{
				SWIGPendingException.Set(new DivideByZeroException(message, SWIGPendingException.Retrieve()));
			}

			private static void SetPendingIndexOutOfRangeException(string message)
			{
				SWIGPendingException.Set(new IndexOutOfRangeException(message, SWIGPendingException.Retrieve()));
			}

			private static void SetPendingInvalidCastException(string message)
			{
				SWIGPendingException.Set(new InvalidCastException(message, SWIGPendingException.Retrieve()));
			}

			private static void SetPendingInvalidOperationException(string message)
			{
				SWIGPendingException.Set(new InvalidOperationException(message, SWIGPendingException.Retrieve()));
			}

			private static void SetPendingIOException(string message)
			{
				SWIGPendingException.Set(new IOException(message, SWIGPendingException.Retrieve()));
			}

			private static void SetPendingNullReferenceException(string message)
			{
				SWIGPendingException.Set(new NullReferenceException(message, SWIGPendingException.Retrieve()));
			}

			private static void SetPendingOutOfMemoryException(string message)
			{
				SWIGPendingException.Set(new OutOfMemoryException(message, SWIGPendingException.Retrieve()));
			}

			private static void SetPendingOverflowException(string message)
			{
				SWIGPendingException.Set(new OverflowException(message, SWIGPendingException.Retrieve()));
			}

			private static void SetPendingSystemException(string message)
			{
				SWIGPendingException.Set(new SystemException(message, SWIGPendingException.Retrieve()));
			}

			private static void SetPendingArgumentException(string message, string paramName)
			{
				SWIGPendingException.Set(new ArgumentException(message, paramName, SWIGPendingException.Retrieve()));
			}

			private static void SetPendingArgumentNullException(string message, string paramName)
			{
				Exception ex = SWIGPendingException.Retrieve();
				if (ex != null)
				{
					message = message + " Inner Exception: " + ex.Message;
				}
				SWIGPendingException.Set(new ArgumentNullException(paramName, message));
			}

			private static void SetPendingArgumentOutOfRangeException(string message, string paramName)
			{
				Exception ex = SWIGPendingException.Retrieve();
				if (ex != null)
				{
					message = message + " Inner Exception: " + ex.Message;
				}
				SWIGPendingException.Set(new ArgumentOutOfRangeException(paramName, message));
			}

			static SWIGExceptionHelper()
			{
				applicationDelegate = SetPendingApplicationException;
				arithmeticDelegate = SetPendingArithmeticException;
				divideByZeroDelegate = SetPendingDivideByZeroException;
				indexOutOfRangeDelegate = SetPendingIndexOutOfRangeException;
				invalidCastDelegate = SetPendingInvalidCastException;
				invalidOperationDelegate = SetPendingInvalidOperationException;
				ioDelegate = SetPendingIOException;
				nullReferenceDelegate = SetPendingNullReferenceException;
				outOfMemoryDelegate = SetPendingOutOfMemoryException;
				overflowDelegate = SetPendingOverflowException;
				systemDelegate = SetPendingSystemException;
				argumentDelegate = SetPendingArgumentException;
				argumentNullDelegate = SetPendingArgumentNullException;
				argumentOutOfRangeDelegate = SetPendingArgumentOutOfRangeException;
				SWIGRegisterExceptionCallbacks_battlenet_commerce(applicationDelegate, arithmeticDelegate, divideByZeroDelegate, indexOutOfRangeDelegate, invalidCastDelegate, invalidOperationDelegate, ioDelegate, nullReferenceDelegate, outOfMemoryDelegate, overflowDelegate, systemDelegate);
				SWIGRegisterExceptionCallbacksArgument_battlenet_commerce(argumentDelegate, argumentNullDelegate, argumentOutOfRangeDelegate);
			}
		}

		public class SWIGPendingException
		{
			[ThreadStatic]
			private static Exception pendingException;

			private static int numExceptionsPending;

			private static object exceptionsLock;

			public static bool Pending
			{
				get
				{
					bool result = false;
					if (numExceptionsPending > 0 && pendingException != null)
					{
						result = true;
					}
					return result;
				}
			}

			public static void Set(Exception e)
			{
				if (pendingException != null)
				{
					throw new ApplicationException("FATAL: An earlier pending exception from unmanaged code was missed and thus not thrown (" + pendingException.ToString() + ")", e);
				}
				pendingException = e;
				lock (exceptionsLock)
				{
					numExceptionsPending++;
				}
			}

			public static Exception Retrieve()
			{
				Exception result = null;
				if (numExceptionsPending > 0 && pendingException != null)
				{
					result = pendingException;
					pendingException = null;
					lock (exceptionsLock)
					{
						numExceptionsPending--;
						return result;
					}
				}
				return result;
			}

			static SWIGPendingException()
			{
				exceptionsLock = new object();
			}
		}

		protected class SWIGStringHelper
		{
			public delegate string SWIGStringDelegate(string message);

			private static SWIGStringDelegate stringDelegate;

			[DllImport("blz_commerce_sdk_plugin")]
			public static extern void SWIGRegisterStringCallback_battlenet_commerce(SWIGStringDelegate stringDelegate);

			[MonoPInvokeCallback(typeof(SWIGStringDelegate))]
			private static string CreateString(string cString)
			{
				return cString;
			}

			static SWIGStringHelper()
			{
				stringDelegate = CreateString;
				SWIGRegisterStringCallback_battlenet_commerce(stringDelegate);
			}
		}

		protected static SWIGStringHelper swigStringHelper;

		private const string DLL_NAME = "blz_commerce_sdk_plugin";

		static battlenet_commercePINVOKE()
		{
			swigStringHelper = new SWIGStringHelper();
		}

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blzCommercePairArray___")]
		public static extern IntPtr new_blzCommercePairArray(int jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blzCommercePairArray___")]
		public static extern void delete_blzCommercePairArray(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blzCommercePairArray_getitem___")]
		public static extern IntPtr blzCommercePairArray_getitem(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blzCommercePairArray_setitem___")]
		public static extern void blzCommercePairArray_setitem(HandleRef jarg1, int jarg2, HandleRef jarg3);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_event_t_catalog_type_set___")]
		public static extern void blz_commerce_catalog_event_t_catalog_type_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_event_t_catalog_type_get___")]
		public static extern int blz_commerce_catalog_event_t_catalog_type_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_event_t_catalog_data_set___")]
		public static extern void blz_commerce_catalog_event_t_catalog_data_set(HandleRef jarg1, IntPtr jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_event_t_catalog_data_get___")]
		public static extern IntPtr blz_commerce_catalog_event_t_catalog_data_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_catalog_event_t___")]
		public static extern IntPtr new_blz_commerce_catalog_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_catalog_event_t___")]
		public static extern void delete_blz_commerce_catalog_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_product_load_event_t_http_result_set___")]
		public static extern void blz_commerce_catalog_product_load_event_t_http_result_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_product_load_event_t_http_result_get___")]
		public static extern IntPtr blz_commerce_catalog_product_load_event_t_http_result_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_product_load_event_t_response_set___")]
		public static extern void blz_commerce_catalog_product_load_event_t_response_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_product_load_event_t_response_get___")]
		public static extern string blz_commerce_catalog_product_load_event_t_response_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_catalog_product_load_event_t___")]
		public static extern IntPtr new_blz_commerce_catalog_product_load_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_catalog_product_load_event_t___")]
		public static extern void delete_blz_commerce_catalog_product_load_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_personalized_shop_event_t_http_result_set___")]
		public static extern void blz_commerce_catalog_personalized_shop_event_t_http_result_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_personalized_shop_event_t_http_result_get___")]
		public static extern IntPtr blz_commerce_catalog_personalized_shop_event_t_http_result_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_personalized_shop_event_t_response_set___")]
		public static extern void blz_commerce_catalog_personalized_shop_event_t_response_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_catalog_personalized_shop_event_t_response_get___")]
		public static extern string blz_commerce_catalog_personalized_shop_event_t_response_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_catalog_personalized_shop_event_t___")]
		public static extern IntPtr new_blz_commerce_catalog_personalized_shop_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_catalog_personalized_shop_event_t___")]
		public static extern void delete_blz_commerce_catalog_personalized_shop_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_product_id_set___")]
		public static extern void blz_commerce_product_info_t_product_id_set(HandleRef jarg1, long jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_product_id_get___")]
		public static extern long blz_commerce_product_info_t_product_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_title_set___")]
		public static extern void blz_commerce_product_info_t_title_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_title_get___")]
		public static extern string blz_commerce_product_info_t_title_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_standard_price_set___")]
		public static extern void blz_commerce_product_info_t_standard_price_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_standard_price_get___")]
		public static extern string blz_commerce_product_info_t_standard_price_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_sale_price_set___")]
		public static extern void blz_commerce_product_info_t_sale_price_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_product_info_t_sale_price_get___")]
		public static extern string blz_commerce_product_info_t_sale_price_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_product_info_t___")]
		public static extern IntPtr new_blz_commerce_product_info_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_product_info_t___")]
		public static extern void delete_blz_commerce_product_info_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register_catalog___")]
		public static extern IntPtr blz_commerce_register_catalog();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_catalog_load_products___")]
		public static extern IntPtr blz_catalog_load_products(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_catalog_personalized_shop___")]
		public static extern IntPtr blz_catalog_personalized_shop(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_browser_info_t_is_cancelable_set___")]
		public static extern void blz_commerce_purchase_browser_info_t_is_cancelable_set(HandleRef jarg1, bool jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_browser_info_t_is_cancelable_get___")]
		public static extern bool blz_commerce_purchase_browser_info_t_is_cancelable_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_purchase_browser_info_t___")]
		public static extern IntPtr new_blz_commerce_purchase_browser_info_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_purchase_browser_info_t___")]
		public static extern void delete_blz_commerce_purchase_browser_info_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_product_id_set___")]
		public static extern void blz_commerce_purchase_event_t_product_id_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_product_id_get___")]
		public static extern string blz_commerce_purchase_event_t_product_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_transaction_id_set___")]
		public static extern void blz_commerce_purchase_event_t_transaction_id_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_transaction_id_get___")]
		public static extern string blz_commerce_purchase_event_t_transaction_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_status_set___")]
		public static extern void blz_commerce_purchase_event_t_status_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_status_get___")]
		public static extern int blz_commerce_purchase_event_t_status_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_error_code_set___")]
		public static extern void blz_commerce_purchase_event_t_error_code_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_error_code_get___")]
		public static extern string blz_commerce_purchase_event_t_error_code_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_browser_info_set___")]
		public static extern void blz_commerce_purchase_event_t_browser_info_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_event_t_browser_info_get___")]
		public static extern IntPtr blz_commerce_purchase_event_t_browser_info_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_purchase_event_t___")]
		public static extern IntPtr new_blz_commerce_purchase_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_purchase_event_t___")]
		public static extern void delete_blz_commerce_purchase_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_routing_key_set___")]
		public static extern void blz_commerce_browser_purchase_t_routing_key_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_routing_key_get___")]
		public static extern string blz_commerce_browser_purchase_t_routing_key_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_server_validation_signature_set___")]
		public static extern void blz_commerce_browser_purchase_t_server_validation_signature_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_server_validation_signature_get___")]
		public static extern string blz_commerce_browser_purchase_t_server_validation_signature_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_sso_token_set___")]
		public static extern void blz_commerce_browser_purchase_t_sso_token_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_sso_token_get___")]
		public static extern string blz_commerce_browser_purchase_t_sso_token_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_externalTransactionId_set___")]
		public static extern void blz_commerce_browser_purchase_t_externalTransactionId_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_purchase_t_externalTransactionId_get___")]
		public static extern string blz_commerce_browser_purchase_t_externalTransactionId_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_browser_purchase_t___")]
		public static extern IntPtr new_blz_commerce_browser_purchase_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_browser_purchase_t___")]
		public static extern void delete_blz_commerce_browser_purchase_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_t_product_id_set___")]
		public static extern void blz_commerce_purchase_t_product_id_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_t_product_id_get___")]
		public static extern string blz_commerce_purchase_t_product_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_t_currency_id_set___")]
		public static extern void blz_commerce_purchase_t_currency_id_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_t_currency_id_get___")]
		public static extern string blz_commerce_purchase_t_currency_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_t_browser_purchase_set___")]
		public static extern void blz_commerce_purchase_t_browser_purchase_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_purchase_t_browser_purchase_get___")]
		public static extern IntPtr blz_commerce_purchase_t_browser_purchase_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_purchase_t___")]
		public static extern IntPtr new_blz_commerce_purchase_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_purchase_t___")]
		public static extern void delete_blz_commerce_purchase_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_checkout_url_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_checkout_url_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_checkout_url_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_checkout_url_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_title_code_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_title_code_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_title_code_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_title_code_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_device_id_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_device_id_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_device_id_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_device_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_title_version_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_title_version_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_title_version_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_title_version_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_locale_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_locale_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_locale_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_locale_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_game_service_region_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_game_service_region_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_game_service_region_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_game_service_region_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_game_account_id_set___")]
		public static extern void blz_commerce_checkout_browser_params_t_game_account_id_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_checkout_browser_params_t_game_account_id_get___")]
		public static extern string blz_commerce_checkout_browser_params_t_game_account_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_checkout_browser_params_t___")]
		public static extern IntPtr new_blz_commerce_checkout_browser_params_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_checkout_browser_params_t___")]
		public static extern void delete_blz_commerce_checkout_browser_params_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_CHECKOUT_BROWSER_PARAM_get___")]
		public static extern string BLZ_COMMERCE_CHECKOUT_BROWSER_PARAM_get();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register_checkout___")]
		public static extern IntPtr blz_commerce_register_checkout();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_checkout_purchase___")]
		public static extern IntPtr blz_checkout_purchase(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_checkout_resume___")]
		public static extern IntPtr blz_checkout_resume(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_checkout_consume___")]
		public static extern IntPtr blz_checkout_consume(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_sdk_t___")]
		public static extern IntPtr new_blz_commerce_sdk_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_sdk_t___")]
		public static extern void delete_blz_commerce_sdk_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_enabled_event_t_ok_set___")]
		public static extern void blz_commerce_http_enabled_event_t_ok_set(HandleRef jarg1, bool jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_enabled_event_t_ok_get___")]
		public static extern bool blz_commerce_http_enabled_event_t_ok_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_enabled_event_t_result_code_set___")]
		public static extern void blz_commerce_http_enabled_event_t_result_code_set(HandleRef jarg1, long jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_enabled_event_t_result_code_get___")]
		public static extern long blz_commerce_http_enabled_event_t_result_code_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_enabled_event_t_message_set___")]
		public static extern void blz_commerce_http_enabled_event_t_message_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_enabled_event_t_message_get___")]
		public static extern string blz_commerce_http_enabled_event_t_message_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_http_enabled_event_t___")]
		public static extern IntPtr new_blz_commerce_http_enabled_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_http_enabled_event_t___")]
		public static extern void delete_blz_commerce_http_enabled_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_event_t_type_set___")]
		public static extern void blz_commerce_event_t_type_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_event_t_type_get___")]
		public static extern int blz_commerce_event_t_type_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_event_t_data_set___")]
		public static extern void blz_commerce_event_t_data_set(HandleRef jarg1, IntPtr jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_event_t_data_get___")]
		public static extern IntPtr blz_commerce_event_t_data_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_event_t_reference_id_set___")]
		public static extern void blz_commerce_event_t_reference_id_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_event_t_reference_id_get___")]
		public static extern int blz_commerce_event_t_reference_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_event_t___")]
		public static extern IntPtr new_blz_commerce_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_event_t___")]
		public static extern void delete_blz_commerce_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_result_t_state_set___")]
		public static extern void blz_commerce_result_t_state_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_result_t_state_get___")]
		public static extern int blz_commerce_result_t_state_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_result_t_data_set___")]
		public static extern void blz_commerce_result_t_data_set(HandleRef jarg1, IntPtr jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_result_t_data_get___")]
		public static extern IntPtr blz_commerce_result_t_data_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_result_t_reference_id_set___")]
		public static extern void blz_commerce_result_t_reference_id_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_result_t_reference_id_get___")]
		public static extern int blz_commerce_result_t_reference_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_result_t___")]
		public static extern IntPtr new_blz_commerce_result_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_result_t___")]
		public static extern void delete_blz_commerce_result_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_sdk_create_result_t_state_set___")]
		public static extern void blz_commerce_sdk_create_result_t_state_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_sdk_create_result_t_state_get___")]
		public static extern int blz_commerce_sdk_create_result_t_state_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_sdk_create_result_t_sdk_set___")]
		public static extern void blz_commerce_sdk_create_result_t_sdk_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_sdk_create_result_t_sdk_get___")]
		public static extern IntPtr blz_commerce_sdk_create_result_t_sdk_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_sdk_create_result_t___")]
		public static extern IntPtr new_blz_commerce_sdk_create_result_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_sdk_create_result_t___")]
		public static extern void delete_blz_commerce_sdk_create_result_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_pair_t_key_set___")]
		public static extern void blz_commerce_pair_t_key_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_pair_t_key_get___")]
		public static extern string blz_commerce_pair_t_key_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_pair_t_data_set___")]
		public static extern void blz_commerce_pair_t_data_set(HandleRef jarg1, IntPtr jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_pair_t_data_get___")]
		public static extern IntPtr blz_commerce_pair_t_data_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_pair_t___")]
		public static extern IntPtr new_blz_commerce_pair_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_pair_t___")]
		public static extern void delete_blz_commerce_pair_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_owned_listener_t_owner_set___")]
		public static extern void blz_commerce_owned_listener_t_owner_set(HandleRef jarg1, IntPtr jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_owned_listener_t_owner_get___")]
		public static extern IntPtr blz_commerce_owned_listener_t_owner_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_owned_listener_t_listener_set___")]
		public static extern void blz_commerce_owned_listener_t_listener_set(HandleRef jarg1, blz_commerce_listener.CallbackDelegate jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_owned_listener_t_listener_get___")]
		public static extern blz_commerce_listener.CallbackDelegate blz_commerce_owned_listener_t_listener_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_owned_listener_t___")]
		public static extern IntPtr new_blz_commerce_owned_listener_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_owned_listener_t___")]
		public static extern void delete_blz_commerce_owned_listener_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_config_set___")]
		public static extern void blz_commerce_manifest_t_config_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_config_get___")]
		public static extern IntPtr blz_commerce_manifest_t_config_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_post_init_set___")]
		public static extern void blz_commerce_manifest_t_post_init_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_post_init_get___")]
		public static extern IntPtr blz_commerce_manifest_t_post_init_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_terminate_set___")]
		public static extern void blz_commerce_manifest_t_terminate_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_terminate_get___")]
		public static extern IntPtr blz_commerce_manifest_t_terminate_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_update_set___")]
		public static extern void blz_commerce_manifest_t_update_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_update_get___")]
		public static extern IntPtr blz_commerce_manifest_t_update_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_get_name_set___")]
		public static extern void blz_commerce_manifest_t_get_name_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_get_name_get___")]
		public static extern IntPtr blz_commerce_manifest_t_get_name_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_get_scopes_set___")]
		public static extern void blz_commerce_manifest_t_get_scopes_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_get_scopes_get___")]
		public static extern IntPtr blz_commerce_manifest_t_get_scopes_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_dependencies_set___")]
		public static extern void blz_commerce_manifest_t_dependencies_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_dependencies_get___")]
		public static extern IntPtr blz_commerce_manifest_t_dependencies_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_dependency_count_set___")]
		public static extern void blz_commerce_manifest_t_dependency_count_set(HandleRef jarg1, uint jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_manifest_t_dependency_count_get___")]
		public static extern uint blz_commerce_manifest_t_dependency_count_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_manifest_t___")]
		public static extern IntPtr new_blz_commerce_manifest_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_manifest_t___")]
		public static extern void delete_blz_commerce_manifest_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_LOG_SUBSYSTEM_set___")]
		public static extern void BLZ_COMMERCE_LOG_SUBSYSTEM_set(string jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_LOG_SUBSYSTEM_get___")]
		public static extern string BLZ_COMMERCE_LOG_SUBSYSTEM_get();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_ANDROID_ACTIVITY_PARAM_get___")]
		public static extern string BLZ_COMMERCE_ANDROID_ACTIVITY_PARAM_get();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_ONESTORE_KEY_get___")]
		public static extern string BLZ_COMMERCE_ONESTORE_KEY_get();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_JAVA_VM_get___")]
		public static extern string BLZ_COMMERCE_JAVA_VM_get();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_INVALID_ID_get___")]
		public static extern int BLZ_COMMERCE_INVALID_ID_get();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_create___")]
		public static extern IntPtr blz_commerce_create();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register___")]
		public static extern IntPtr blz_commerce_register(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_add_listener___")]
		public static extern IntPtr blz_commerce_add_listener(HandleRef jarg1, IntPtr jarg2, blz_commerce_listener.CallbackDelegate jarg3);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register_log___")]
		public static extern IntPtr blz_commerce_register_log(IntPtr jarg1, blz_commerce_log_hook.CallbackDelegate jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_init___")]
		public static extern IntPtr blz_commerce_init(HandleRef jarg1, HandleRef jarg2, int jarg3);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_update___")]
		public static extern IntPtr blz_commerce_update(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_terminate___")]
		public static extern IntPtr blz_commerce_terminate(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_event_t_vc_type_set___")]
		public static extern void blz_commerce_vc_event_t_vc_type_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_event_t_vc_type_get___")]
		public static extern int blz_commerce_vc_event_t_vc_type_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_event_t_vc_data_set___")]
		public static extern void blz_commerce_vc_event_t_vc_data_set(HandleRef jarg1, IntPtr jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_event_t_vc_data_get___")]
		public static extern IntPtr blz_commerce_vc_event_t_vc_data_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_vc_event_t___")]
		public static extern IntPtr new_blz_commerce_vc_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_vc_event_t___")]
		public static extern void delete_blz_commerce_vc_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_get_balance_event_t_http_result_set___")]
		public static extern void blz_commerce_vc_get_balance_event_t_http_result_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_get_balance_event_t_http_result_get___")]
		public static extern IntPtr blz_commerce_vc_get_balance_event_t_http_result_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_get_balance_event_t_response_set___")]
		public static extern void blz_commerce_vc_get_balance_event_t_response_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_get_balance_event_t_response_get___")]
		public static extern string blz_commerce_vc_get_balance_event_t_response_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_vc_get_balance_event_t___")]
		public static extern IntPtr new_blz_commerce_vc_get_balance_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_vc_get_balance_event_t___")]
		public static extern void delete_blz_commerce_vc_get_balance_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_order_event_t_http_result_set___")]
		public static extern void blz_commerce_vc_order_event_t_http_result_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_order_event_t_http_result_get___")]
		public static extern IntPtr blz_commerce_vc_order_event_t_http_result_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_order_event_t_response_set___")]
		public static extern void blz_commerce_vc_order_event_t_response_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_order_event_t_response_get___")]
		public static extern string blz_commerce_vc_order_event_t_response_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_vc_order_event_t___")]
		public static extern IntPtr new_blz_commerce_vc_order_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_vc_order_event_t___")]
		public static extern void delete_blz_commerce_vc_order_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_currency_code_set___")]
		public static extern void blz_commerce_vc_purchase_t_currency_code_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_currency_code_get___")]
		public static extern string blz_commerce_vc_purchase_t_currency_code_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_product_id_set___")]
		public static extern void blz_commerce_vc_purchase_t_product_id_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_product_id_get___")]
		public static extern int blz_commerce_vc_purchase_t_product_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_game_service_region_id_set___")]
		public static extern void blz_commerce_vc_purchase_t_game_service_region_id_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_game_service_region_id_get___")]
		public static extern int blz_commerce_vc_purchase_t_game_service_region_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_quantity_set___")]
		public static extern void blz_commerce_vc_purchase_t_quantity_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_quantity_get___")]
		public static extern int blz_commerce_vc_purchase_t_quantity_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_external_transaction_id_set___")]
		public static extern void blz_commerce_vc_purchase_t_external_transaction_id_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_external_transaction_id_get___")]
		public static extern string blz_commerce_vc_purchase_t_external_transaction_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_title_id_set___")]
		public static extern void blz_commerce_vc_purchase_t_title_id_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase_t_title_id_get___")]
		public static extern string blz_commerce_vc_purchase_t_title_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_vc_purchase_t___")]
		public static extern IntPtr new_blz_commerce_vc_purchase_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_vc_purchase_t___")]
		public static extern void delete_blz_commerce_vc_purchase_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register_vc___")]
		public static extern IntPtr blz_commerce_register_vc();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_get_balance___")]
		public static extern IntPtr blz_commerce_vc_get_balance(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vc_purchase___")]
		public static extern IntPtr blz_commerce_vc_purchase(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vec2d_t_x_set___")]
		public static extern void blz_commerce_vec2d_t_x_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vec2d_t_x_get___")]
		public static extern int blz_commerce_vec2d_t_x_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vec2d_t_y_set___")]
		public static extern void blz_commerce_vec2d_t_y_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_vec2d_t_y_get___")]
		public static extern int blz_commerce_vec2d_t_y_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_vec2d_t___")]
		public static extern IntPtr new_blz_commerce_vec2d_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_vec2d_t___")]
		public static extern void delete_blz_commerce_vec2d_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_range_t_from_set___")]
		public static extern void scene_range_t_from_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_range_t_from_get___")]
		public static extern int scene_range_t_from_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_range_t_to_set___")]
		public static extern void scene_range_t_to_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_range_t_to_get___")]
		public static extern int scene_range_t_to_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_scene_range_t___")]
		public static extern IntPtr new_scene_range_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_scene_range_t___")]
		public static extern void delete_scene_range_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_x_set___")]
		public static extern void blz_commerce_scene_rect_t_x_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_x_get___")]
		public static extern int blz_commerce_scene_rect_t_x_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_y_set___")]
		public static extern void blz_commerce_scene_rect_t_y_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_y_get___")]
		public static extern int blz_commerce_scene_rect_t_y_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_w_set___")]
		public static extern void blz_commerce_scene_rect_t_w_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_w_get___")]
		public static extern int blz_commerce_scene_rect_t_w_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_h_set___")]
		public static extern void blz_commerce_scene_rect_t_h_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_rect_t_h_get___")]
		public static extern int blz_commerce_scene_rect_t_h_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_scene_rect_t___")]
		public static extern IntPtr new_blz_commerce_scene_rect_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_scene_rect_t___")]
		public static extern void delete_blz_commerce_scene_rect_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_composition_range_changed_event_t_selected_range_set___")]
		public static extern void scene_ime_composition_range_changed_event_t_selected_range_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_composition_range_changed_event_t_selected_range_get___")]
		public static extern IntPtr scene_ime_composition_range_changed_event_t_selected_range_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_composition_range_changed_event_t_character_bounds_set___")]
		public static extern void scene_ime_composition_range_changed_event_t_character_bounds_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_composition_range_changed_event_t_character_bounds_get___")]
		public static extern IntPtr scene_ime_composition_range_changed_event_t_character_bounds_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_composition_range_changed_event_t_character_bounds_size_set___")]
		public static extern void scene_ime_composition_range_changed_event_t_character_bounds_size_set(HandleRef jarg1, uint jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_composition_range_changed_event_t_character_bounds_size_get___")]
		public static extern uint scene_ime_composition_range_changed_event_t_character_bounds_size_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_scene_ime_composition_range_changed_event_t___")]
		public static extern IntPtr new_scene_ime_composition_range_changed_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_scene_ime_composition_range_changed_event_t___")]
		public static extern void delete_scene_ime_composition_range_changed_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_state_changed_event_t_input_type_set___")]
		public static extern void scene_ime_state_changed_event_t_input_type_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_state_changed_event_t_input_type_get___")]
		public static extern int scene_ime_state_changed_event_t_input_type_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_state_changed_event_t_surrounding_text_set___")]
		public static extern void scene_ime_state_changed_event_t_surrounding_text_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_state_changed_event_t_surrounding_text_get___")]
		public static extern string scene_ime_state_changed_event_t_surrounding_text_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_scene_ime_state_changed_event_t___")]
		public static extern IntPtr new_scene_ime_state_changed_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_scene_ime_state_changed_event_t___")]
		public static extern void delete_scene_ime_state_changed_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_text_selection_changed_event_t_text_set___")]
		public static extern void scene_ime_text_selection_changed_event_t_text_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_text_selection_changed_event_t_text_get___")]
		public static extern string scene_ime_text_selection_changed_event_t_text_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_text_selection_changed_event_t_offset_set___")]
		public static extern void scene_ime_text_selection_changed_event_t_offset_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_text_selection_changed_event_t_offset_get___")]
		public static extern int scene_ime_text_selection_changed_event_t_offset_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_text_selection_changed_event_t_selected_range_set___")]
		public static extern void scene_ime_text_selection_changed_event_t_selected_range_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_text_selection_changed_event_t_selected_range_get___")]
		public static extern IntPtr scene_ime_text_selection_changed_event_t_selected_range_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_scene_ime_text_selection_changed_event_t___")]
		public static extern IntPtr new_scene_ime_text_selection_changed_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_scene_ime_text_selection_changed_event_t___")]
		public static extern void delete_scene_ime_text_selection_changed_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_selection_bounds_changed_event_t_anchor_rect_set___")]
		public static extern void scene_ime_selection_bounds_changed_event_t_anchor_rect_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_selection_bounds_changed_event_t_anchor_rect_get___")]
		public static extern IntPtr scene_ime_selection_bounds_changed_event_t_anchor_rect_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_selection_bounds_changed_event_t_focus_rect_set___")]
		public static extern void scene_ime_selection_bounds_changed_event_t_focus_rect_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_selection_bounds_changed_event_t_focus_rect_get___")]
		public static extern IntPtr scene_ime_selection_bounds_changed_event_t_focus_rect_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_selection_bounds_changed_event_t_is_anchor_rect_set___")]
		public static extern void scene_ime_selection_bounds_changed_event_t_is_anchor_rect_set(HandleRef jarg1, bool jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_scene_ime_selection_bounds_changed_event_t_is_anchor_rect_get___")]
		public static extern bool scene_ime_selection_bounds_changed_event_t_is_anchor_rect_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_scene_ime_selection_bounds_changed_event_t___")]
		public static extern IntPtr new_scene_ime_selection_bounds_changed_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_scene_ime_selection_bounds_changed_event_t___")]
		public static extern void delete_scene_ime_selection_bounds_changed_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_event_t_scene_type_set___")]
		public static extern void blz_commerce_scene_event_t_scene_type_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_event_t_scene_type_get___")]
		public static extern int blz_commerce_scene_event_t_scene_type_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_event_t_scene_data_set___")]
		public static extern void blz_commerce_scene_event_t_scene_data_set(HandleRef jarg1, IntPtr jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_event_t_scene_data_get___")]
		public static extern IntPtr blz_commerce_scene_event_t_scene_data_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_scene_event_t___")]
		public static extern IntPtr new_blz_commerce_scene_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_scene_event_t___")]
		public static extern void delete_blz_commerce_scene_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_buffer_t_bytes_set___")]
		public static extern void blz_commerce_buffer_t_bytes_set(HandleRef jarg1, IntPtr jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_buffer_t_bytes_get___")]
		public static extern IntPtr blz_commerce_buffer_t_bytes_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_buffer_t_len_set___")]
		public static extern void blz_commerce_buffer_t_len_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_buffer_t_len_get___")]
		public static extern int blz_commerce_buffer_t_len_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_buffer_t___")]
		public static extern IntPtr new_blz_commerce_buffer_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_buffer_t___")]
		public static extern void delete_blz_commerce_buffer_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_buffer_update_event_t_dirty_rects_set___")]
		public static extern void blz_commerce_scene_buffer_update_event_t_dirty_rects_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_buffer_update_event_t_dirty_rects_get___")]
		public static extern IntPtr blz_commerce_scene_buffer_update_event_t_dirty_rects_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_buffer_update_event_t_dirty_rect_size_set___")]
		public static extern void blz_commerce_scene_buffer_update_event_t_dirty_rect_size_set(HandleRef jarg1, uint jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_buffer_update_event_t_dirty_rect_size_get___")]
		public static extern uint blz_commerce_scene_buffer_update_event_t_dirty_rect_size_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_buffer_update_event_t_buffer_set___")]
		public static extern void blz_commerce_scene_buffer_update_event_t_buffer_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_buffer_update_event_t_buffer_get___")]
		public static extern IntPtr blz_commerce_scene_buffer_update_event_t_buffer_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_scene_buffer_update_event_t___")]
		public static extern IntPtr new_blz_commerce_scene_buffer_update_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_scene_buffer_update_event_t___")]
		public static extern void delete_blz_commerce_scene_buffer_update_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_window_resize_event_t_win_size_set___")]
		public static extern void blz_commerce_scene_window_resize_event_t_win_size_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_window_resize_event_t_win_size_get___")]
		public static extern IntPtr blz_commerce_scene_window_resize_event_t_win_size_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_window_resize_event_t_buffer_size_set___")]
		public static extern void blz_commerce_scene_window_resize_event_t_buffer_size_set(HandleRef jarg1, uint jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_window_resize_event_t_buffer_size_get___")]
		public static extern uint blz_commerce_scene_window_resize_event_t_buffer_size_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_scene_window_resize_event_t___")]
		public static extern IntPtr new_blz_commerce_scene_window_resize_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_scene_window_resize_event_t___")]
		public static extern void delete_blz_commerce_scene_window_resize_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_window_resize_requested_event_t_requested_size_set___")]
		public static extern void blz_commerce_scene_window_resize_requested_event_t_requested_size_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_window_resize_requested_event_t_requested_size_get___")]
		public static extern IntPtr blz_commerce_scene_window_resize_requested_event_t_requested_size_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_scene_window_resize_requested_event_t___")]
		public static extern IntPtr new_blz_commerce_scene_window_resize_requested_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_scene_window_resize_requested_event_t___")]
		public static extern void delete_blz_commerce_scene_window_resize_requested_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_external_link_event_t_url_set___")]
		public static extern void blz_commerce_scene_external_link_event_t_url_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_scene_external_link_event_t_url_get___")]
		public static extern string blz_commerce_scene_external_link_event_t_url_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_scene_external_link_event_t___")]
		public static extern IntPtr new_blz_commerce_scene_external_link_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_scene_external_link_event_t___")]
		public static extern void delete_blz_commerce_scene_external_link_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_keyCode_set___")]
		public static extern void blz_commerce_key_input_t_keyCode_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_keyCode_get___")]
		public static extern int blz_commerce_key_input_t_keyCode_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_type_set___")]
		public static extern void blz_commerce_key_input_t_type_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_type_get___")]
		public static extern int blz_commerce_key_input_t_type_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_character_set___")]
		public static extern void blz_commerce_key_input_t_character_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_character_get___")]
		public static extern int blz_commerce_key_input_t_character_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_modifiers_set___")]
		public static extern void blz_commerce_key_input_t_modifiers_set(HandleRef jarg1, uint jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_key_input_t_modifiers_get___")]
		public static extern uint blz_commerce_key_input_t_modifiers_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_key_input_t___")]
		public static extern IntPtr new_blz_commerce_key_input_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_key_input_t___")]
		public static extern void delete_blz_commerce_key_input_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_button_set___")]
		public static extern void blz_commerce_mouse_input_t_button_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_button_get___")]
		public static extern int blz_commerce_mouse_input_t_button_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_type_set___")]
		public static extern void blz_commerce_mouse_input_t_type_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_type_get___")]
		public static extern int blz_commerce_mouse_input_t_type_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_coords_set___")]
		public static extern void blz_commerce_mouse_input_t_coords_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_coords_get___")]
		public static extern IntPtr blz_commerce_mouse_input_t_coords_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_mods_set___")]
		public static extern void blz_commerce_mouse_input_t_mods_set(HandleRef jarg1, uint jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_input_t_mods_get___")]
		public static extern uint blz_commerce_mouse_input_t_mods_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_mouse_input_t___")]
		public static extern IntPtr new_blz_commerce_mouse_input_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_mouse_input_t___")]
		public static extern void delete_blz_commerce_mouse_input_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_character_input_t_character_set___")]
		public static extern void blz_commerce_character_input_t_character_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_character_input_t_character_get___")]
		public static extern int blz_commerce_character_input_t_character_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_character_input_t_modifiers_set___")]
		public static extern void blz_commerce_character_input_t_modifiers_set(HandleRef jarg1, uint jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_character_input_t_modifiers_get___")]
		public static extern uint blz_commerce_character_input_t_modifiers_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_character_input_t___")]
		public static extern IntPtr new_blz_commerce_character_input_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_character_input_t___")]
		public static extern void delete_blz_commerce_character_input_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_move_t_coords_set___")]
		public static extern void blz_commerce_mouse_move_t_coords_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_move_t_coords_get___")]
		public static extern IntPtr blz_commerce_mouse_move_t_coords_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_move_t_mod_set___")]
		public static extern void blz_commerce_mouse_move_t_mod_set(HandleRef jarg1, uint jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_move_t_mod_get___")]
		public static extern uint blz_commerce_mouse_move_t_mod_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_mouse_move_t___")]
		public static extern IntPtr new_blz_commerce_mouse_move_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_mouse_move_t___")]
		public static extern void delete_blz_commerce_mouse_move_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_wheel_t_delta_set___")]
		public static extern void blz_commerce_mouse_wheel_t_delta_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_wheel_t_delta_get___")]
		public static extern int blz_commerce_mouse_wheel_t_delta_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_wheel_t_coords_set___")]
		public static extern void blz_commerce_mouse_wheel_t_coords_set(HandleRef jarg1, HandleRef jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_wheel_t_coords_get___")]
		public static extern IntPtr blz_commerce_mouse_wheel_t_coords_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_wheel_t_mod_set___")]
		public static extern void blz_commerce_mouse_wheel_t_mod_set(HandleRef jarg1, uint jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_mouse_wheel_t_mod_get___")]
		public static extern uint blz_commerce_mouse_wheel_t_mod_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_mouse_wheel_t___")]
		public static extern IntPtr new_blz_commerce_mouse_wheel_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_mouse_wheel_t___")]
		public static extern void delete_blz_commerce_mouse_wheel_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_window_close_t_browser_id_set___")]
		public static extern void blz_commerce_window_close_t_browser_id_set(HandleRef jarg1, uint jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_window_close_t_browser_id_get___")]
		public static extern uint blz_commerce_window_close_t_browser_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_window_close_t___")]
		public static extern IntPtr new_blz_commerce_window_close_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_window_close_t___")]
		public static extern void delete_blz_commerce_window_close_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_window_width_set___")]
		public static extern void blz_commerce_browser_params_t_window_width_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_window_width_get___")]
		public static extern int blz_commerce_browser_params_t_window_width_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_window_height_set___")]
		public static extern void blz_commerce_browser_params_t_window_height_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_window_height_get___")]
		public static extern int blz_commerce_browser_params_t_window_height_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_max_window_width_set___")]
		public static extern void blz_commerce_browser_params_t_max_window_width_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_max_window_width_get___")]
		public static extern int blz_commerce_browser_params_t_max_window_width_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_max_window_height_set___")]
		public static extern void blz_commerce_browser_params_t_max_window_height_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_max_window_height_get___")]
		public static extern int blz_commerce_browser_params_t_max_window_height_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_log_directory_set___")]
		public static extern void blz_commerce_browser_params_t_log_directory_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_log_directory_get___")]
		public static extern string blz_commerce_browser_params_t_log_directory_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_browser_directory_set___")]
		public static extern void blz_commerce_browser_params_t_browser_directory_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_browser_directory_get___")]
		public static extern string blz_commerce_browser_params_t_browser_directory_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_is_prod_set___")]
		public static extern void blz_commerce_browser_params_t_is_prod_set(HandleRef jarg1, bool jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_params_t_is_prod_get___")]
		public static extern bool blz_commerce_browser_params_t_is_prod_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_browser_params_t___")]
		public static extern IntPtr new_blz_commerce_browser_params_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_browser_params_t___")]
		public static extern void delete_blz_commerce_browser_params_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_BROWSER_PARAM_get___")]
		public static extern string BLZ_COMMERCE_BROWSER_PARAM_get();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register_scene___")]
		public static extern IntPtr blz_commerce_register_scene();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_browser_send_event___")]
		public static extern IntPtr blz_commerce_browser_send_event(HandleRef jarg1, int jarg2, IntPtr jarg3);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_event_t_http_type_set___")]
		public static extern void blz_commerce_http_event_t_http_type_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_event_t_http_type_get___")]
		public static extern int blz_commerce_http_event_t_http_type_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_event_t_http_data_set___")]
		public static extern void blz_commerce_http_event_t_http_data_set(HandleRef jarg1, IntPtr jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_event_t_http_data_get___")]
		public static extern IntPtr blz_commerce_http_event_t_http_data_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_http_event_t___")]
		public static extern IntPtr new_blz_commerce_http_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_http_event_t___")]
		public static extern void delete_blz_commerce_http_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_http_need_auth_event_t___")]
		public static extern IntPtr new_blz_commerce_http_need_auth_event_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_http_need_auth_event_t___")]
		public static extern void delete_blz_commerce_http_need_auth_event_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_client_id_set___")]
		public static extern void blz_commerce_http_params_t_client_id_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_client_id_get___")]
		public static extern string blz_commerce_http_params_t_client_id_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_token_set___")]
		public static extern void blz_commerce_http_params_t_token_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_token_get___")]
		public static extern string blz_commerce_http_params_t_token_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_title_code_set___")]
		public static extern void blz_commerce_http_params_t_title_code_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_title_code_get___")]
		public static extern string blz_commerce_http_params_t_title_code_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_title_version_set___")]
		public static extern void blz_commerce_http_params_t_title_version_set(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_title_version_get___")]
		public static extern string blz_commerce_http_params_t_title_version_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_region_set___")]
		public static extern void blz_commerce_http_params_t_region_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_region_get___")]
		public static extern int blz_commerce_http_params_t_region_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_environment_set___")]
		public static extern void blz_commerce_http_params_t_environment_set(HandleRef jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_params_t_environment_get___")]
		public static extern int blz_commerce_http_params_t_environment_get(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_new_blz_commerce_http_params_t___")]
		public static extern IntPtr new_blz_commerce_http_params_t();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_delete_blz_commerce_http_params_t___")]
		public static extern void delete_blz_commerce_http_params_t(HandleRef jarg1);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_BLZ_COMMERCE_HTTP_PARAM_get___")]
		public static extern string BLZ_COMMERCE_HTTP_PARAM_get();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_register_http___")]
		public static extern IntPtr blz_commerce_register_http();

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_http_refresh_auth___")]
		public static extern IntPtr blz_commerce_http_refresh_auth(HandleRef jarg1, string jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_generate_transaction_id___")]
		public static extern string blz_commerce_generate_transaction_id(int jarg1, int jarg2);

		[DllImport("blz_commerce_sdk_plugin", EntryPoint = "CSharp_BlizzardfCommerce_blz_commerce_program_to_4cc___")]
		public static extern int blz_commerce_program_to_4cc(string jarg1);
	}
}
