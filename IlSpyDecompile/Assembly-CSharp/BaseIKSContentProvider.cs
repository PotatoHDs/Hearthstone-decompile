using System;
using System.Collections;
using Content.Delivery;
using MiniJSON;

public abstract class BaseIKSContentProvider
{
	public enum AdAttributes
	{
		AD_IMPORTANCE,
		AD_PUBLISH,
		AD_CAMPAIGN_NAME,
		AD_TITLE,
		AD_SUBTITLE,
		AD_LINK,
		AD_MAX_VIEW_COUNT,
		AD_GAME_ACTION,
		AD_BUTTON_TEXT,
		AD_TITLE_OFFSET_X,
		AD_TITLE_OFFSET_Y,
		AD_SUBTITLE_OFFSET_X,
		AD_SUBTITLE_OFFSET_Y,
		AD_TITLE_FONT_SIZE,
		AD_SUBTITLE_FONT_SIZE,
		AD_URL,
		AD_CLIENT_VERSION,
		AD_PLATFORM,
		AD_ANDROID_STORE,
		AD_VISIBILITY
	}

	public const string IKS_SAVE_LOCATION = "IKS_LAST_STORED_RESPONSE";

	public abstract bool Ready { get; }

	public abstract void InitializeJsonURL(string customURL);

	public abstract JsonList GetRootListNode(JsonNode response);

	public abstract IEnumerator GetQuery(ResponseProcessHandler responseProcessHandler, object param, bool force);

	public abstract InnKeepersSpecialAd ReadInnKeepersSpecialAd(JsonNode adNode);

	public static T GetIntegerFromNode<T>(string keyName, JsonNode node)
	{
		if (node.ContainsKey(keyName) && node[keyName] != null)
		{
			if (node[keyName] is string)
			{
				return (T)Convert.ChangeType(int.Parse((string)node[keyName]), typeof(T));
			}
			return (T)Convert.ChangeType(node[keyName], typeof(T));
		}
		return (T)(object)0;
	}

	public static T GetStringFromNode<T>(string keyName, JsonNode node)
	{
		return (T)(node.ContainsKey(keyName) ? node[keyName] : string.Empty);
	}
}
