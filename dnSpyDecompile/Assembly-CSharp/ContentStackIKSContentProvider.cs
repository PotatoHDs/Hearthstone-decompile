using System;
using System.Collections;
using bgs;
using Content.Delivery;
using MiniJSON;

// Token: 0x02000630 RID: 1584
public class ContentStackIKSContentProvider : BaseIKSContentProvider
{
	// Token: 0x1700052D RID: 1325
	// (get) Token: 0x060058F8 RID: 22776 RVA: 0x001D0476 File Offset: 0x001CE676
	public override bool Ready
	{
		get
		{
			return this.m_connect.Ready;
		}
	}

	// Token: 0x060058F9 RID: 22777 RVA: 0x001D0484 File Offset: 0x001CE684
	public override void InitializeJsonURL(string customURL)
	{
		this.m_connect.InitializeURL("innkeeper_special", Vars.Key("ContentStack.Env").GetStr("production"), Localization.GetBnetLocaleName(), BattleNet.GetCurrentRegion() == constants.BnetRegion.REGION_CN, "IKS_LAST_STORED_RESPONSE", 0);
		if (!string.IsNullOrEmpty(customURL))
		{
			this.m_connect.ResetServiceURL(customURL);
		}
	}

	// Token: 0x060058FA RID: 22778 RVA: 0x001D04DC File Offset: 0x001CE6DC
	public override JsonList GetRootListNode(JsonNode response)
	{
		if (response.ContainsKey("entries"))
		{
			return response["entries"] as JsonList;
		}
		if (response.ContainsKey("entry"))
		{
			return new JsonList
			{
				response["entry"]
			};
		}
		return null;
	}

	// Token: 0x060058FB RID: 22779 RVA: 0x001D052C File Offset: 0x001CE72C
	public override IEnumerator GetQuery(ResponseProcessHandler responseProcessHandler, object param, bool force)
	{
		return this.m_connect.Query(responseProcessHandler, param, string.Empty, force);
	}

	// Token: 0x060058FC RID: 22780 RVA: 0x001D0544 File Offset: 0x001CE744
	public override InnKeepersSpecialAd ReadInnKeepersSpecialAd(JsonNode adNode)
	{
		return new InnKeepersSpecialAd
		{
			Importance = this.GetAttribute<int>(adNode, BaseIKSContentProvider.AdAttributes.AD_IMPORTANCE),
			PublishDate = this.GetAttribute<long>(adNode, BaseIKSContentProvider.AdAttributes.AD_PUBLISH),
			CampaignName = this.GetAttribute<string>(adNode, BaseIKSContentProvider.AdAttributes.AD_CAMPAIGN_NAME),
			Title = this.GetAttribute<string>(adNode, BaseIKSContentProvider.AdAttributes.AD_TITLE),
			SubTitle = this.GetAttribute<string>(adNode, BaseIKSContentProvider.AdAttributes.AD_SUBTITLE),
			Link = this.GetAttribute<string>(adNode, BaseIKSContentProvider.AdAttributes.AD_LINK),
			MaxViewCount = this.GetAttribute<int>(adNode, BaseIKSContentProvider.AdAttributes.AD_MAX_VIEW_COUNT),
			GameAction = this.GetAttribute<string>(adNode, BaseIKSContentProvider.AdAttributes.AD_GAME_ACTION),
			ButtonText = this.GetAttribute<string>(adNode, BaseIKSContentProvider.AdAttributes.AD_BUTTON_TEXT),
			TitleOffsetX = this.GetAttribute<int>(adNode, BaseIKSContentProvider.AdAttributes.AD_TITLE_OFFSET_X),
			TitleOffsetY = this.GetAttribute<int>(adNode, BaseIKSContentProvider.AdAttributes.AD_TITLE_OFFSET_Y),
			SubTitleOffsetX = this.GetAttribute<int>(adNode, BaseIKSContentProvider.AdAttributes.AD_SUBTITLE_OFFSET_X),
			SubTitleOffsetY = this.GetAttribute<int>(adNode, BaseIKSContentProvider.AdAttributes.AD_SUBTITLE_OFFSET_Y),
			TitleFontSize = this.GetAttribute<int>(adNode, BaseIKSContentProvider.AdAttributes.AD_TITLE_FONT_SIZE),
			SubTitleFontSize = this.GetAttribute<int>(adNode, BaseIKSContentProvider.AdAttributes.AD_SUBTITLE_FONT_SIZE),
			ImageUrl = this.GetAttribute<string>(adNode, BaseIKSContentProvider.AdAttributes.AD_URL),
			ClientVersion = this.GetAttribute<string>(adNode, BaseIKSContentProvider.AdAttributes.AD_CLIENT_VERSION),
			Platform = this.GetAttribute<string>(adNode, BaseIKSContentProvider.AdAttributes.AD_PLATFORM),
			AndroidStore = this.GetAttribute<string>(adNode, BaseIKSContentProvider.AdAttributes.AD_ANDROID_STORE),
			Visibility = StringUtils.CompareIgnoreCase("public", this.GetAttribute<string>(adNode, BaseIKSContentProvider.AdAttributes.AD_VISIBILITY))
		};
	}

	// Token: 0x060058FD RID: 22781 RVA: 0x001D0684 File Offset: 0x001CE884
	private T GetAttribute<T>(JsonNode adNode, BaseIKSContentProvider.AdAttributes attr)
	{
		switch (attr)
		{
		case BaseIKSContentProvider.AdAttributes.AD_IMPORTANCE:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("importance", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_PUBLISH:
			return (T)((object)Convert.ToDateTime((string)(adNode["publish_details"] as JsonNode)["time"]).Ticks);
		case BaseIKSContentProvider.AdAttributes.AD_CAMPAIGN_NAME:
			return BaseIKSContentProvider.GetStringFromNode<T>("title", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_TITLE:
			return BaseIKSContentProvider.GetStringFromNode<T>("in_game_ad_title", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_SUBTITLE:
			return BaseIKSContentProvider.GetStringFromNode<T>("in_game_ad_subtitle", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_LINK:
			return BaseIKSContentProvider.GetStringFromNode<T>("url", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_MAX_VIEW_COUNT:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("maxviewcount", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_GAME_ACTION:
			return BaseIKSContentProvider.GetStringFromNode<T>("gameaction", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_BUTTON_TEXT:
			return BaseIKSContentProvider.GetStringFromNode<T>("buttontext", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_TITLE_OFFSET_X:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("titleoffsetx", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_TITLE_OFFSET_Y:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("titleoffsety", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_SUBTITLE_OFFSET_X:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("subtitleoffsetx", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_SUBTITLE_OFFSET_Y:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("subtitleoffsety", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_TITLE_FONT_SIZE:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("titlefontsize", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_SUBTITLE_FONT_SIZE:
			return BaseIKSContentProvider.GetIntegerFromNode<T>("subtitlefontsize", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_URL:
		{
			JsonNode node = adNode["in_game_ad"] as JsonNode;
			return BaseIKSContentProvider.GetStringFromNode<T>("url", node);
		}
		case BaseIKSContentProvider.AdAttributes.AD_CLIENT_VERSION:
			return BaseIKSContentProvider.GetStringFromNode<T>("clientversion", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_PLATFORM:
			return BaseIKSContentProvider.GetStringFromNode<T>("platform", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_ANDROID_STORE:
			return BaseIKSContentProvider.GetStringFromNode<T>("androidstore", adNode);
		case BaseIKSContentProvider.AdAttributes.AD_VISIBILITY:
			return BaseIKSContentProvider.GetStringFromNode<T>("visibility", adNode);
		default:
			return default(T);
		}
	}

	// Token: 0x04004C2B RID: 19499
	private ContentStackConnect m_connect = new ContentStackConnect();
}
