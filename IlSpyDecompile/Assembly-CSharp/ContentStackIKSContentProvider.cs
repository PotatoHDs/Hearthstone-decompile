using System;
using System.Collections;
using bgs;
using Content.Delivery;
using MiniJSON;

public class ContentStackIKSContentProvider : BaseIKSContentProvider
{
	private ContentStackConnect m_connect = new ContentStackConnect();

	public override bool Ready => m_connect.Ready;

	public override void InitializeJsonURL(string customURL)
	{
		m_connect.InitializeURL("innkeeper_special", Vars.Key("ContentStack.Env").GetStr("production"), Localization.GetBnetLocaleName(), BattleNet.GetCurrentRegion() == constants.BnetRegion.REGION_CN, "IKS_LAST_STORED_RESPONSE", 0);
		if (!string.IsNullOrEmpty(customURL))
		{
			m_connect.ResetServiceURL(customURL);
		}
	}

	public override JsonList GetRootListNode(JsonNode response)
	{
		if (response.ContainsKey("entries"))
		{
			return response["entries"] as JsonList;
		}
		if (response.ContainsKey("entry"))
		{
			return new JsonList { response["entry"] };
		}
		return null;
	}

	public override IEnumerator GetQuery(ResponseProcessHandler responseProcessHandler, object param, bool force)
	{
		return m_connect.Query(responseProcessHandler, param, string.Empty, force);
	}

	public override InnKeepersSpecialAd ReadInnKeepersSpecialAd(JsonNode adNode)
	{
		return new InnKeepersSpecialAd
		{
			Importance = GetAttribute<int>(adNode, AdAttributes.AD_IMPORTANCE),
			PublishDate = GetAttribute<long>(adNode, AdAttributes.AD_PUBLISH),
			CampaignName = GetAttribute<string>(adNode, AdAttributes.AD_CAMPAIGN_NAME),
			Title = GetAttribute<string>(adNode, AdAttributes.AD_TITLE),
			SubTitle = GetAttribute<string>(adNode, AdAttributes.AD_SUBTITLE),
			Link = GetAttribute<string>(adNode, AdAttributes.AD_LINK),
			MaxViewCount = GetAttribute<int>(adNode, AdAttributes.AD_MAX_VIEW_COUNT),
			GameAction = GetAttribute<string>(adNode, AdAttributes.AD_GAME_ACTION),
			ButtonText = GetAttribute<string>(adNode, AdAttributes.AD_BUTTON_TEXT),
			TitleOffsetX = GetAttribute<int>(adNode, AdAttributes.AD_TITLE_OFFSET_X),
			TitleOffsetY = GetAttribute<int>(adNode, AdAttributes.AD_TITLE_OFFSET_Y),
			SubTitleOffsetX = GetAttribute<int>(adNode, AdAttributes.AD_SUBTITLE_OFFSET_X),
			SubTitleOffsetY = GetAttribute<int>(adNode, AdAttributes.AD_SUBTITLE_OFFSET_Y),
			TitleFontSize = GetAttribute<int>(adNode, AdAttributes.AD_TITLE_FONT_SIZE),
			SubTitleFontSize = GetAttribute<int>(adNode, AdAttributes.AD_SUBTITLE_FONT_SIZE),
			ImageUrl = GetAttribute<string>(adNode, AdAttributes.AD_URL),
			ClientVersion = GetAttribute<string>(adNode, AdAttributes.AD_CLIENT_VERSION),
			Platform = GetAttribute<string>(adNode, AdAttributes.AD_PLATFORM),
			AndroidStore = GetAttribute<string>(adNode, AdAttributes.AD_ANDROID_STORE),
			Visibility = StringUtils.CompareIgnoreCase("public", GetAttribute<string>(adNode, AdAttributes.AD_VISIBILITY))
		};
	}

	private T GetAttribute<T>(JsonNode adNode, AdAttributes attr)
	{
		switch (attr)
		{
		case AdAttributes.AD_IMPORTANCE:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("importance", adNode);
		case AdAttributes.AD_PUBLISH:
			return (T)(object)Convert.ToDateTime((string)(adNode["publish_details"] as JsonNode)["time"]).Ticks;
		case AdAttributes.AD_CAMPAIGN_NAME:
			return BaseIKSContentProvider.GetStringFromNode<T>("title", adNode);
		case AdAttributes.AD_TITLE:
			return BaseIKSContentProvider.GetStringFromNode<T>("in_game_ad_title", adNode);
		case AdAttributes.AD_SUBTITLE:
			return BaseIKSContentProvider.GetStringFromNode<T>("in_game_ad_subtitle", adNode);
		case AdAttributes.AD_LINK:
			return BaseIKSContentProvider.GetStringFromNode<T>("url", adNode);
		case AdAttributes.AD_MAX_VIEW_COUNT:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("maxviewcount", adNode);
		case AdAttributes.AD_GAME_ACTION:
			return BaseIKSContentProvider.GetStringFromNode<T>("gameaction", adNode);
		case AdAttributes.AD_BUTTON_TEXT:
			return BaseIKSContentProvider.GetStringFromNode<T>("buttontext", adNode);
		case AdAttributes.AD_TITLE_OFFSET_X:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("titleoffsetx", adNode);
		case AdAttributes.AD_TITLE_OFFSET_Y:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("titleoffsety", adNode);
		case AdAttributes.AD_SUBTITLE_OFFSET_X:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("subtitleoffsetx", adNode);
		case AdAttributes.AD_SUBTITLE_OFFSET_Y:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("subtitleoffsety", adNode);
		case AdAttributes.AD_TITLE_FONT_SIZE:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("titlefontsize", adNode);
		case AdAttributes.AD_SUBTITLE_FONT_SIZE:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("subtitlefontsize", adNode);
		case AdAttributes.AD_URL:
		{
			JsonNode node = adNode["in_game_ad"] as JsonNode;
			return BaseIKSContentProvider.GetStringFromNode<T>("url", node);
		}
		case AdAttributes.AD_CLIENT_VERSION:
			return BaseIKSContentProvider.GetStringFromNode<T>("clientversion", adNode);
		case AdAttributes.AD_PLATFORM:
			return BaseIKSContentProvider.GetStringFromNode<T>("platform", adNode);
		case AdAttributes.AD_ANDROID_STORE:
			return BaseIKSContentProvider.GetStringFromNode<T>("androidstore", adNode);
		case AdAttributes.AD_VISIBILITY:
			return BaseIKSContentProvider.GetStringFromNode<T>("visibility", adNode);
		default:
			return default(T);
		}
	}
}
