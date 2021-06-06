using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using MiniJSON;

namespace Content.Delivery
{
	// Token: 0x02000FA5 RID: 4005
	public class DamoclesIKSContentProvider : BaseIKSContentProvider
	{
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x0600AE2C RID: 44588 RVA: 0x0036387C File Offset: 0x00361A7C
		public override bool Ready
		{
			get
			{
				return this.m_connect.Ready;
			}
		}

		// Token: 0x0600AE2D RID: 44589 RVA: 0x00363889 File Offset: 0x00361A89
		public override void InitializeJsonURL(string customURL)
		{
			this.m_connect.InitializeNydusURL(Localization.GetLocaleName(), BattleNet.GetCurrentRegion() == constants.BnetRegion.REGION_CN, "IKS_LAST_STORED_RESPONSE");
			if (!string.IsNullOrEmpty(customURL))
			{
				this.m_connect.ResetServiceURL(customURL);
			}
		}

		// Token: 0x0600AE2E RID: 44590 RVA: 0x003638BC File Offset: 0x00361ABC
		public override JsonList GetRootListNode(JsonNode response)
		{
			return response["ads"] as JsonList;
		}

		// Token: 0x0600AE2F RID: 44591 RVA: 0x003638CE File Offset: 0x00361ACE
		public override IEnumerator GetQuery(ResponseProcessHandler responseProcessHandler, object param, bool force)
		{
			return this.m_connect.Query(responseProcessHandler, param, string.Empty, force);
		}

		// Token: 0x0600AE30 RID: 44592 RVA: 0x003638E4 File Offset: 0x00361AE4
		public override InnKeepersSpecialAd ReadInnKeepersSpecialAd(JsonNode adNode)
		{
			JsonNode contents = null;
			if (adNode.ContainsKey("contents"))
			{
				contents = (((List<object>)adNode["contents"])[0] as JsonNode);
			}
			JsonNode adMetaDataFromResponseAdObject = this.GetAdMetaDataFromResponseAdObject(adNode);
			InnKeepersSpecialAd innKeepersSpecialAd = new InnKeepersSpecialAd();
			innKeepersSpecialAd.Importance = this.GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_IMPORTANCE);
			innKeepersSpecialAd.PublishDate = this.GetAttribute<long>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_PUBLISH);
			innKeepersSpecialAd.CampaignName = this.GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_CAMPAIGN_NAME);
			innKeepersSpecialAd.Title = this.GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_TITLE);
			innKeepersSpecialAd.SubTitle = this.GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_SUBTITLE);
			innKeepersSpecialAd.Link = this.GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_LINK);
			innKeepersSpecialAd.MaxViewCount = this.GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_MAX_VIEW_COUNT);
			if (innKeepersSpecialAd.MaxViewCount == 0)
			{
				innKeepersSpecialAd.MaxViewCount = 3;
			}
			innKeepersSpecialAd.GameAction = this.GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_GAME_ACTION);
			innKeepersSpecialAd.ButtonText = this.GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_BUTTON_TEXT);
			innKeepersSpecialAd.TitleOffsetX = this.GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_TITLE_OFFSET_X);
			innKeepersSpecialAd.TitleOffsetY = this.GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_TITLE_OFFSET_Y);
			innKeepersSpecialAd.SubTitleOffsetX = this.GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_SUBTITLE_OFFSET_X);
			innKeepersSpecialAd.SubTitleOffsetY = this.GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_SUBTITLE_OFFSET_Y);
			innKeepersSpecialAd.TitleFontSize = this.GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_TITLE_FONT_SIZE);
			innKeepersSpecialAd.SubTitleFontSize = this.GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_SUBTITLE_FONT_SIZE);
			innKeepersSpecialAd.ImageUrl = this.GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_URL);
			innKeepersSpecialAd.ClientVersion = this.GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_CLIENT_VERSION);
			innKeepersSpecialAd.Platform = this.GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_PLATFORM);
			innKeepersSpecialAd.AndroidStore = this.GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_ANDROID_STORE);
			innKeepersSpecialAd.Visibility = StringUtils.CompareIgnoreCase("public", this.GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, BaseIKSContentProvider.AdAttributes.AD_VISIBILITY));
			return innKeepersSpecialAd;
		}

		// Token: 0x0600AE31 RID: 44593 RVA: 0x00363A90 File Offset: 0x00361C90
		private T GetAttribute<T>(JsonNode adNode, JsonNode contents, JsonNode meta, BaseIKSContentProvider.AdAttributes attr)
		{
			switch (attr)
			{
			case BaseIKSContentProvider.AdAttributes.AD_IMPORTANCE:
			{
				T t = BaseIKSContentProvider.GetIntegerFromNode<T>("importance", adNode);
				if ((bool)adNode["maxImportance"])
				{
					t = (T)((object)6);
				}
				return (T)((object)t);
			}
			case BaseIKSContentProvider.AdAttributes.AD_PUBLISH:
				return (T)((object)adNode["publish"]);
			case BaseIKSContentProvider.AdAttributes.AD_CAMPAIGN_NAME:
				return BaseIKSContentProvider.GetStringFromNode<T>("campaignName", adNode);
			case BaseIKSContentProvider.AdAttributes.AD_TITLE:
				return BaseIKSContentProvider.GetStringFromNode<T>("title", contents);
			case BaseIKSContentProvider.AdAttributes.AD_SUBTITLE:
				return BaseIKSContentProvider.GetStringFromNode<T>("subtitle", contents);
			case BaseIKSContentProvider.AdAttributes.AD_LINK:
			{
				T stringFromNode = BaseIKSContentProvider.GetStringFromNode<T>("link", contents);
				if (!EqualityComparer<T>.Default.Equals(stringFromNode, default(T)))
				{
					return stringFromNode;
				}
				return BaseIKSContentProvider.GetStringFromNode<T>("link", adNode);
			}
			case BaseIKSContentProvider.AdAttributes.AD_MAX_VIEW_COUNT:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("maxViewCount", meta);
			case BaseIKSContentProvider.AdAttributes.AD_GAME_ACTION:
				return BaseIKSContentProvider.GetStringFromNode<T>("gameAction", meta);
			case BaseIKSContentProvider.AdAttributes.AD_BUTTON_TEXT:
				return BaseIKSContentProvider.GetStringFromNode<T>("buttonText", meta);
			case BaseIKSContentProvider.AdAttributes.AD_TITLE_OFFSET_X:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("titleOffsetX", meta);
			case BaseIKSContentProvider.AdAttributes.AD_TITLE_OFFSET_Y:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("titleOffsetY", meta);
			case BaseIKSContentProvider.AdAttributes.AD_SUBTITLE_OFFSET_X:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("subtitleOffsetX", meta);
			case BaseIKSContentProvider.AdAttributes.AD_SUBTITLE_OFFSET_Y:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("subtitleOffsetY", meta);
			case BaseIKSContentProvider.AdAttributes.AD_TITLE_FONT_SIZE:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("titleFontSize", meta);
			case BaseIKSContentProvider.AdAttributes.AD_SUBTITLE_FONT_SIZE:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("subtitleFontSize", meta);
			case BaseIKSContentProvider.AdAttributes.AD_URL:
			{
				JsonNode node = contents["media"] as JsonNode;
				return BaseIKSContentProvider.GetStringFromNode<T>("url", node);
			}
			case BaseIKSContentProvider.AdAttributes.AD_CLIENT_VERSION:
				return BaseIKSContentProvider.GetStringFromNode<T>("clientVersion", meta);
			case BaseIKSContentProvider.AdAttributes.AD_PLATFORM:
				return BaseIKSContentProvider.GetStringFromNode<T>("platform", meta);
			case BaseIKSContentProvider.AdAttributes.AD_ANDROID_STORE:
				return BaseIKSContentProvider.GetStringFromNode<T>("androidStore", meta);
			case BaseIKSContentProvider.AdAttributes.AD_VISIBILITY:
				return BaseIKSContentProvider.GetStringFromNode<T>("visibility", meta);
			default:
				return default(T);
			}
		}

		// Token: 0x0600AE32 RID: 44594 RVA: 0x00363C58 File Offset: 0x00361E58
		private JsonNode GetAdMetaDataFromResponseAdObject(JsonNode adNode)
		{
			JsonNode jsonNode = new JsonNode();
			if (adNode.ContainsKey("metadata") && adNode["metadata"] != null)
			{
				foreach (object obj in ((List<object>)adNode["metadata"]))
				{
					JsonNode jsonNode2 = obj as JsonNode;
					jsonNode[(string)jsonNode2["key"]] = (string)jsonNode2["value"];
				}
			}
			return jsonNode;
		}

		// Token: 0x040094EE RID: 38126
		private const int DEFAULT_MAX_IMPORTANCE = 6;

		// Token: 0x040094EF RID: 38127
		private const int DEFAULT_MAX_MESSAGE_VIEW_COUNT = 3;

		// Token: 0x040094F0 RID: 38128
		private DamoclesConnect m_connect = new DamoclesConnect();
	}
}
