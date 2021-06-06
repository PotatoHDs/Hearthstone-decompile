using System.Collections;

public static class AppleSearchAds
{
	public delegate void AttributionCallback(bool success, string jsonString, int errorCode, string errorMessage);

	private static readonly object thisLock_ = new object();

	private static bool done_ = false;

	private static bool success_ = false;

	private static string jsonString_;

	private static int errorCode_ = -1;

	private static string errorMessage_;

	private static void OnReturnAttributionDetails(bool success, string jsonString, int errorCode, string errorMessage)
	{
		lock (thisLock_)
		{
			success_ = success;
			jsonString_ = jsonString;
			errorCode_ = errorCode;
			errorMessage_ = errorMessage;
			done_ = true;
		}
	}

	private static bool RequestAttributionDetails(AttributionCallback callback)
	{
		callback(success: true, "{\"Version3.1\":{\"iad-keyword\":\"Keyword\",\"iad-adgroup-id\":\"1234567890\",\"iad-campaign-id\":\"1234567890\",\"iad-lineitem-id\":\"1234567890\",\"iad-campaign-name\":\"CampaignName\",\"iad-org-name\":\"OrgName\",\"iad-conversion-date\":\"2018-07-27T22:47:19Z\",\"iad-creative-name\":\"CreativeName\",\"iad-creative-id\":\"1234567890\",\"iad-click-date\":\"2018-07-27T22:47:19Z\",\"iad-attribution\":\"true\",\"iad-adgroup-name\":\"AdGroupName\",\"iad-lineitem-name\":\"LineName\"}}", -1, "");
		return true;
	}

	public static IEnumerator RequestAsync(AttributionCallback completionCallback)
	{
		if (!RequestAttributionDetails(OnReturnAttributionDetails))
		{
			yield break;
		}
		while (true)
		{
			lock (thisLock_)
			{
				if (done_)
				{
					break;
				}
			}
			yield return null;
		}
		bool success;
		string jsonString;
		int errorCode;
		string errorMessage;
		lock (thisLock_)
		{
			success = success_;
			jsonString = string.Copy(jsonString_);
			errorCode = errorCode_;
			errorMessage = string.Copy(errorMessage_);
		}
		completionCallback(success, jsonString, errorCode, errorMessage);
	}
}
