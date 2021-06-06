using System;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class ErrorCategory
	{
		public const string RETRYABLE_FAILURE = "RETRYABLE_FAILURE";

		public const string NON_RETRYABLE_FAILURE = "NON_RETRYABLE_FAILURE";
	}
}
