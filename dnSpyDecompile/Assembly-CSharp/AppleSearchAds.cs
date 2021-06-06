using System;
using System.Collections;

// Token: 0x0200074D RID: 1869
public static class AppleSearchAds
{
	// Token: 0x060069A0 RID: 27040 RVA: 0x00226B78 File Offset: 0x00224D78
	private static void OnReturnAttributionDetails(bool success, string jsonString, int errorCode, string errorMessage)
	{
		object obj = AppleSearchAds.thisLock_;
		lock (obj)
		{
			AppleSearchAds.success_ = success;
			AppleSearchAds.jsonString_ = jsonString;
			AppleSearchAds.errorCode_ = errorCode;
			AppleSearchAds.errorMessage_ = errorMessage;
			AppleSearchAds.done_ = true;
		}
	}

	// Token: 0x060069A1 RID: 27041 RVA: 0x00226BD0 File Offset: 0x00224DD0
	private static bool RequestAttributionDetails(AppleSearchAds.AttributionCallback callback)
	{
		callback(true, "{\"Version3.1\":{\"iad-keyword\":\"Keyword\",\"iad-adgroup-id\":\"1234567890\",\"iad-campaign-id\":\"1234567890\",\"iad-lineitem-id\":\"1234567890\",\"iad-campaign-name\":\"CampaignName\",\"iad-org-name\":\"OrgName\",\"iad-conversion-date\":\"2018-07-27T22:47:19Z\",\"iad-creative-name\":\"CreativeName\",\"iad-creative-id\":\"1234567890\",\"iad-click-date\":\"2018-07-27T22:47:19Z\",\"iad-attribution\":\"true\",\"iad-adgroup-name\":\"AdGroupName\",\"iad-lineitem-name\":\"LineName\"}}", -1, "");
		return true;
	}

	// Token: 0x060069A2 RID: 27042 RVA: 0x00226BE5 File Offset: 0x00224DE5
	public static IEnumerator RequestAsync(AppleSearchAds.AttributionCallback completionCallback)
	{
		if (AppleSearchAds.RequestAttributionDetails(new AppleSearchAds.AttributionCallback(AppleSearchAds.OnReturnAttributionDetails)))
		{
			object obj;
			for (;;)
			{
				obj = AppleSearchAds.thisLock_;
				lock (obj)
				{
					if (AppleSearchAds.done_)
					{
						break;
					}
				}
				yield return null;
			}
			obj = AppleSearchAds.thisLock_;
			bool success;
			string jsonString;
			int errorCode;
			string errorMessage;
			lock (obj)
			{
				success = AppleSearchAds.success_;
				jsonString = string.Copy(AppleSearchAds.jsonString_);
				errorCode = AppleSearchAds.errorCode_;
				errorMessage = string.Copy(AppleSearchAds.errorMessage_);
			}
			completionCallback(success, jsonString, errorCode, errorMessage);
		}
		yield break;
	}

	// Token: 0x04005664 RID: 22116
	private static readonly object thisLock_ = new object();

	// Token: 0x04005665 RID: 22117
	private static bool done_ = false;

	// Token: 0x04005666 RID: 22118
	private static bool success_ = false;

	// Token: 0x04005667 RID: 22119
	private static string jsonString_;

	// Token: 0x04005668 RID: 22120
	private static int errorCode_ = -1;

	// Token: 0x04005669 RID: 22121
	private static string errorMessage_;

	// Token: 0x02002328 RID: 9000
	// (Invoke) Token: 0x06012A07 RID: 76295
	public delegate void AttributionCallback(bool success, string jsonString, int errorCode, string errorMessage);
}
