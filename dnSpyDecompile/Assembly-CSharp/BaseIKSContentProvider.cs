using System;
using System.Collections;
using Content.Delivery;
using MiniJSON;

// Token: 0x0200062E RID: 1582
public abstract class BaseIKSContentProvider
{
	// Token: 0x1700052C RID: 1324
	// (get) Token: 0x060058E8 RID: 22760
	public abstract bool Ready { get; }

	// Token: 0x060058E9 RID: 22761
	public abstract void InitializeJsonURL(string customURL);

	// Token: 0x060058EA RID: 22762
	public abstract JsonList GetRootListNode(JsonNode response);

	// Token: 0x060058EB RID: 22763
	public abstract IEnumerator GetQuery(ResponseProcessHandler responseProcessHandler, object param, bool force);

	// Token: 0x060058EC RID: 22764
	public abstract InnKeepersSpecialAd ReadInnKeepersSpecialAd(JsonNode adNode);

	// Token: 0x060058ED RID: 22765 RVA: 0x001D028C File Offset: 0x001CE48C
	public static T GetIntegerFromNode<T>(string keyName, JsonNode node)
	{
		if (!node.ContainsKey(keyName) || node[keyName] == null)
		{
			return (T)((object)0);
		}
		if (node[keyName] is string)
		{
			return (T)((object)Convert.ChangeType(int.Parse((string)node[keyName]), typeof(T)));
		}
		return (T)((object)Convert.ChangeType(node[keyName], typeof(T)));
	}

	// Token: 0x060058EE RID: 22766 RVA: 0x001D030B File Offset: 0x001CE50B
	public static T GetStringFromNode<T>(string keyName, JsonNode node)
	{
		return (T)((object)(node.ContainsKey(keyName) ? node[keyName] : string.Empty));
	}

	// Token: 0x04004C1C RID: 19484
	public const string IKS_SAVE_LOCATION = "IKS_LAST_STORED_RESPONSE";

	// Token: 0x02002133 RID: 8499
	public enum AdAttributes
	{
		// Token: 0x0400DF7C RID: 57212
		AD_IMPORTANCE,
		// Token: 0x0400DF7D RID: 57213
		AD_PUBLISH,
		// Token: 0x0400DF7E RID: 57214
		AD_CAMPAIGN_NAME,
		// Token: 0x0400DF7F RID: 57215
		AD_TITLE,
		// Token: 0x0400DF80 RID: 57216
		AD_SUBTITLE,
		// Token: 0x0400DF81 RID: 57217
		AD_LINK,
		// Token: 0x0400DF82 RID: 57218
		AD_MAX_VIEW_COUNT,
		// Token: 0x0400DF83 RID: 57219
		AD_GAME_ACTION,
		// Token: 0x0400DF84 RID: 57220
		AD_BUTTON_TEXT,
		// Token: 0x0400DF85 RID: 57221
		AD_TITLE_OFFSET_X,
		// Token: 0x0400DF86 RID: 57222
		AD_TITLE_OFFSET_Y,
		// Token: 0x0400DF87 RID: 57223
		AD_SUBTITLE_OFFSET_X,
		// Token: 0x0400DF88 RID: 57224
		AD_SUBTITLE_OFFSET_Y,
		// Token: 0x0400DF89 RID: 57225
		AD_TITLE_FONT_SIZE,
		// Token: 0x0400DF8A RID: 57226
		AD_SUBTITLE_FONT_SIZE,
		// Token: 0x0400DF8B RID: 57227
		AD_URL,
		// Token: 0x0400DF8C RID: 57228
		AD_CLIENT_VERSION,
		// Token: 0x0400DF8D RID: 57229
		AD_PLATFORM,
		// Token: 0x0400DF8E RID: 57230
		AD_ANDROID_STORE,
		// Token: 0x0400DF8F RID: 57231
		AD_VISIBILITY
	}
}
