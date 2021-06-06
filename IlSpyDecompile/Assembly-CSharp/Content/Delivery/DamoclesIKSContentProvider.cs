using System.Collections;
using System.Collections.Generic;
using bgs;
using MiniJSON;

namespace Content.Delivery
{
	public class DamoclesIKSContentProvider : BaseIKSContentProvider
	{
		private const int DEFAULT_MAX_IMPORTANCE = 6;

		private const int DEFAULT_MAX_MESSAGE_VIEW_COUNT = 3;

		private DamoclesConnect m_connect = new DamoclesConnect();

		public override bool Ready => m_connect.Ready;

		public override void InitializeJsonURL(string customURL)
		{
			m_connect.InitializeNydusURL(Localization.GetLocaleName(), BattleNet.GetCurrentRegion() == constants.BnetRegion.REGION_CN, "IKS_LAST_STORED_RESPONSE");
			if (!string.IsNullOrEmpty(customURL))
			{
				m_connect.ResetServiceURL(customURL);
			}
		}

		public override JsonList GetRootListNode(JsonNode response)
		{
			return response["ads"] as JsonList;
		}

		public override IEnumerator GetQuery(ResponseProcessHandler responseProcessHandler, object param, bool force)
		{
			return m_connect.Query(responseProcessHandler, param, string.Empty, force);
		}

		public override InnKeepersSpecialAd ReadInnKeepersSpecialAd(JsonNode adNode)
		{
			JsonNode contents = null;
			if (adNode.ContainsKey("contents"))
			{
				contents = ((List<object>)adNode["contents"])[0] as JsonNode;
			}
			JsonNode adMetaDataFromResponseAdObject = GetAdMetaDataFromResponseAdObject(adNode);
			InnKeepersSpecialAd innKeepersSpecialAd = new InnKeepersSpecialAd();
			innKeepersSpecialAd.Importance = GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_IMPORTANCE);
			innKeepersSpecialAd.PublishDate = GetAttribute<long>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_PUBLISH);
			innKeepersSpecialAd.CampaignName = GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_CAMPAIGN_NAME);
			innKeepersSpecialAd.Title = GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_TITLE);
			innKeepersSpecialAd.SubTitle = GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_SUBTITLE);
			innKeepersSpecialAd.Link = GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_LINK);
			innKeepersSpecialAd.MaxViewCount = GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_MAX_VIEW_COUNT);
			if (innKeepersSpecialAd.MaxViewCount == 0)
			{
				innKeepersSpecialAd.MaxViewCount = 3;
			}
			innKeepersSpecialAd.GameAction = GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_GAME_ACTION);
			innKeepersSpecialAd.ButtonText = GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_BUTTON_TEXT);
			innKeepersSpecialAd.TitleOffsetX = GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_TITLE_OFFSET_X);
			innKeepersSpecialAd.TitleOffsetY = GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_TITLE_OFFSET_Y);
			innKeepersSpecialAd.SubTitleOffsetX = GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_SUBTITLE_OFFSET_X);
			innKeepersSpecialAd.SubTitleOffsetY = GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_SUBTITLE_OFFSET_Y);
			innKeepersSpecialAd.TitleFontSize = GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_TITLE_FONT_SIZE);
			innKeepersSpecialAd.SubTitleFontSize = GetAttribute<int>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_SUBTITLE_FONT_SIZE);
			innKeepersSpecialAd.ImageUrl = GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_URL);
			innKeepersSpecialAd.ClientVersion = GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_CLIENT_VERSION);
			innKeepersSpecialAd.Platform = GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_PLATFORM);
			innKeepersSpecialAd.AndroidStore = GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_ANDROID_STORE);
			innKeepersSpecialAd.Visibility = StringUtils.CompareIgnoreCase("public", GetAttribute<string>(adNode, contents, adMetaDataFromResponseAdObject, AdAttributes.AD_VISIBILITY));
			return innKeepersSpecialAd;
		}

		private T GetAttribute<T>(JsonNode adNode, JsonNode contents, JsonNode meta, AdAttributes attr)
		{
			switch (attr)
			{
			case AdAttributes.AD_IMPORTANCE:
			{
				T val = BaseIKSContentProvider.GetIntegerFromNode<T>("importance", adNode);
				if ((bool)adNode["maxImportance"])
				{
					val = (T)(object)6;
				}
				return (T)(object)val;
			}
			case AdAttributes.AD_PUBLISH:
				return (T)adNode["publish"];
			case AdAttributes.AD_CAMPAIGN_NAME:
				return BaseIKSContentProvider.GetStringFromNode<T>("campaignName", adNode);
			case AdAttributes.AD_TITLE:
				return BaseIKSContentProvider.GetStringFromNode<T>("title", contents);
			case AdAttributes.AD_SUBTITLE:
				return BaseIKSContentProvider.GetStringFromNode<T>("subtitle", contents);
			case AdAttributes.AD_LINK:
			{
				T stringFromNode = BaseIKSContentProvider.GetStringFromNode<T>("link", contents);
				if (!EqualityComparer<T>.Default.Equals(stringFromNode, default(T)))
				{
					return stringFromNode;
				}
				return BaseIKSContentProvider.GetStringFromNode<T>("link", adNode);
			}
			case AdAttributes.AD_MAX_VIEW_COUNT:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("maxViewCount", meta);
			case AdAttributes.AD_GAME_ACTION:
				return BaseIKSContentProvider.GetStringFromNode<T>("gameAction", meta);
			case AdAttributes.AD_BUTTON_TEXT:
				return BaseIKSContentProvider.GetStringFromNode<T>("buttonText", meta);
			case AdAttributes.AD_TITLE_OFFSET_X:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("titleOffsetX", meta);
			case AdAttributes.AD_TITLE_OFFSET_Y:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("titleOffsetY", meta);
			case AdAttributes.AD_SUBTITLE_OFFSET_X:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("subtitleOffsetX", meta);
			case AdAttributes.AD_SUBTITLE_OFFSET_Y:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("subtitleOffsetY", meta);
			case AdAttributes.AD_TITLE_FONT_SIZE:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("titleFontSize", meta);
			case AdAttributes.AD_SUBTITLE_FONT_SIZE:
				return BaseIKSContentProvider.GetIntegerFromNode<T>("subtitleFontSize", meta);
			case AdAttributes.AD_URL:
			{
				JsonNode node = contents["media"] as JsonNode;
				return BaseIKSContentProvider.GetStringFromNode<T>("url", node);
			}
			case AdAttributes.AD_CLIENT_VERSION:
				return BaseIKSContentProvider.GetStringFromNode<T>("clientVersion", meta);
			case AdAttributes.AD_PLATFORM:
				return BaseIKSContentProvider.GetStringFromNode<T>("platform", meta);
			case AdAttributes.AD_ANDROID_STORE:
				return BaseIKSContentProvider.GetStringFromNode<T>("androidStore", meta);
			case AdAttributes.AD_VISIBILITY:
				return BaseIKSContentProvider.GetStringFromNode<T>("visibility", meta);
			default:
				return default(T);
			}
		}

		private JsonNode GetAdMetaDataFromResponseAdObject(JsonNode adNode)
		{
			JsonNode jsonNode = new JsonNode();
			if (adNode.ContainsKey("metadata") && adNode["metadata"] != null)
			{
				foreach (object item in (List<object>)adNode["metadata"])
				{
					JsonNode jsonNode2 = item as JsonNode;
					jsonNode[(string)jsonNode2["key"]] = (string)jsonNode2["value"];
				}
				return jsonNode;
			}
			return jsonNode;
		}
	}
}
