using System;
using System.Collections.Generic;
using Blizzard.MobileAuth.MiniJson;
using UnityEngine;

namespace Blizzard.MobileAuth
{
	public struct BlzMobileAuthError
	{
		public enum ErrorCode
		{
			Unknown = 74600000,
			ResultMissingAuthToken = 74600100,
			ResultMissingAccountID = 74600101,
			ImportInvalidAuthToken = 74600200,
			ImportInvalidRegion = 74600201,
			AccountInfoService = 74600300,
			RegionResolverRetrieval = 74600400,
			SecurityUnableToGenerateNonce = 74600500,
			SecurityUnableToDecryptAuthToken = 74600501,
			WebSSOGeneratorUnavailable = 74600700,
			WebSSOGeneratorUnableToConstructURL = 74600701,
			WebSSOGeneratorUnknownError = 74600702,
			WebSSOGeneratorInternalServerError = 74600703,
			WebSSOGeneratorInvalidRequest = 74600704,
			WebSSOGeneratorAccountNotFound = 74600705,
			WebSSOGeneratorInvalidApplication = 74600706,
			WebSSOGeneratorInvalidAccountStatus = 74600707,
			WebSSOGeneratorInvalidLoginToken = 74600708,
			GuestSoftAccountCreationFailure = 74600800,
			GuestSoftAccountCreationDenied = 74600801,
			GuestSoftAccountAuthenticationFailed = 74600802,
			GuestSoftAccountCreationUnexpectedError = 74600803,
			BattleTagInfoRetrievalFailed = 74600900,
			BattleTagChangeFailed = 74600901
		}

		public int errorCode;

		public string errorMessage;

		public string errorContext;

		public BlzMobileAuthError(string errorJson)
		{
			Dictionary<string, object> dictionary = Json.Deserialize(errorJson) as Dictionary<string, object>;
			errorCode = ParseCodeToInt(dictionary["errorCode"].ToString());
			errorMessage = dictionary["errorMessage"].ToString();
			errorContext = dictionary["errorContext"].ToString();
		}

		public BlzMobileAuthError(string errorCode, string errorMessage, string errorContext)
		{
			this.errorCode = ParseCodeToInt(errorCode);
			this.errorMessage = errorMessage;
			this.errorContext = errorContext;
		}

		private static int ParseCodeToInt(string codeString)
		{
			try
			{
				return int.Parse(codeString);
			}
			catch (Exception ex)
			{
				Debug.Log("BlzMobileAuthGameObject OnError could not parse error code: " + ex.Message);
				return 0;
			}
		}
	}
}
