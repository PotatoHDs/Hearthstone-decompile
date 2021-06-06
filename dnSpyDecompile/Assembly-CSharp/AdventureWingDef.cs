using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000058 RID: 88
[CustomEditClass]
public class AdventureWingDef : MonoBehaviour
{
	// Token: 0x0600052E RID: 1326 RVA: 0x0001E660 File Offset: 0x0001C860
	public void Init(WingDbfRecord wingRecord)
	{
		this.m_AdventureId = (AdventureDbId)wingRecord.AdventureId;
		this.m_WingId = (WingDbId)wingRecord.ID;
		this.m_OwnershipPrereq = (WingDbId)wingRecord.OwnershipPrereqWingId;
		this.m_SortOrder = wingRecord.SortOrder;
		this.m_UnlockOrder = wingRecord.UnlockOrder;
		this.m_WingName = wingRecord.Name;
		this.m_ComingSoonLabel = wingRecord.ComingSoonLabel;
		this.m_RequiresLabel = wingRecord.RequiresLabel;
		this.m_OpenPrereq = (WingDbId)wingRecord.OpenPrereqWingId;
		this.m_OpeningDiscouragedLabel = wingRecord.OpenDiscouragedLabel;
		this.m_OpeningDiscouragedWarning = wingRecord.OpenDiscouragedWarning;
		this.m_MustCompleteOpenPrereq = wingRecord.MustCompleteOpenPrereq;
		this.m_UnlocksAutomatically = wingRecord.UnlocksAutomatically;
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x0001E722 File Offset: 0x0001C922
	public AdventureDbId GetAdventureId()
	{
		return this.m_AdventureId;
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x0001E72A File Offset: 0x0001C92A
	public WingDbId GetWingId()
	{
		return this.m_WingId;
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x0001E732 File Offset: 0x0001C932
	public WingDbId GetOwnershipPrereqId()
	{
		return this.m_OwnershipPrereq;
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x0001E73A File Offset: 0x0001C93A
	public int GetSortOrder()
	{
		return this.m_SortOrder;
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x0001E742 File Offset: 0x0001C942
	public int GetUnlockOrder()
	{
		return this.m_UnlockOrder;
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x0001E74A File Offset: 0x0001C94A
	public string GetWingName()
	{
		return this.m_WingName;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x0001E752 File Offset: 0x0001C952
	public string GetComingSoonLabel()
	{
		return this.m_ComingSoonLabel;
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x0001E75A File Offset: 0x0001C95A
	public string GetRequiresLabel()
	{
		return this.m_RequiresLabel;
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x0001E762 File Offset: 0x0001C962
	public WingDbId GetOpenPrereqId()
	{
		return this.m_OpenPrereq;
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x0001E76A File Offset: 0x0001C96A
	public string GetOpeningNotRecommendedLabel()
	{
		return this.m_OpeningDiscouragedLabel;
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x0001E772 File Offset: 0x0001C972
	public string GetOpeningNotRecommendedWarning()
	{
		return this.m_OpeningDiscouragedWarning;
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x0001E77A File Offset: 0x0001C97A
	public bool GetMustCompleteOpenPrereq()
	{
		return this.m_MustCompleteOpenPrereq;
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x0001E782 File Offset: 0x0001C982
	public bool GetUnlocksAutomatically()
	{
		return this.m_UnlocksAutomatically;
	}

	// Token: 0x04000372 RID: 882
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_WingPrefab;

	// Token: 0x04000373 RID: 883
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_CoinPrefab;

	// Token: 0x04000374 RID: 884
	public bool CoinsStartFaceUp;

	// Token: 0x04000375 RID: 885
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_RewardsPrefab;

	// Token: 0x04000376 RID: 886
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_UnlockSpellPrefab;

	// Token: 0x04000377 RID: 887
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_AccentPrefab;

	// Token: 0x04000378 RID: 888
	[CustomEditField(Sections = "Opening Quote", T = EditType.GAME_OBJECT)]
	public string m_OpenQuotePrefab;

	// Token: 0x04000379 RID: 889
	[CustomEditField(Sections = "Opening Quote", T = EditType.GAME_OBJECT)]
	public string m_OpenQuoteVOLine;

	// Token: 0x0400037A RID: 890
	[CustomEditField(Sections = "Opening Quote")]
	public float m_OpenQuoteDelay;

	// Token: 0x0400037B RID: 891
	[CustomEditField(Sections = "Opening Quote")]
	public bool m_PlayOpenQuoteInHeroic = true;

	// Token: 0x0400037C RID: 892
	[CustomEditField(Sections = "Wing Open Popup", T = EditType.GAME_OBJECT)]
	public string m_WingOpenPopup;

	// Token: 0x0400037D RID: 893
	[CustomEditField(Sections = "Complete Quote", T = EditType.GAME_OBJECT)]
	public string m_CompleteQuotePrefab;

	// Token: 0x0400037E RID: 894
	[CustomEditField(Sections = "Complete Quote", T = EditType.GAME_OBJECT)]
	public string m_CompleteQuoteVOLine;

	// Token: 0x0400037F RID: 895
	[CustomEditField(Sections = "Complete Quote", T = EditType.GAME_OBJECT)]
	public string m_CompleteQuoteNextWingLockedPrefab;

	// Token: 0x04000380 RID: 896
	[CustomEditField(Sections = "Complete Quote", T = EditType.GAME_OBJECT)]
	public string m_CompleteQuoteNextWingLockedVOLine;

	// Token: 0x04000381 RID: 897
	[CustomEditField(Sections = "Complete Quote", T = EditType.GAME_OBJECT)]
	public bool m_PlayCompleteQuoteInHeroic = true;

	// Token: 0x04000382 RID: 898
	[CustomEditField(Sections = "Rewards Preview")]
	public List<string> m_SpecificRewardsPreviewCards;

	// Token: 0x04000383 RID: 899
	[CustomEditField(Sections = "Rewards Preview")]
	public List<int> m_SpecificRewardsPreviewCardBacks;

	// Token: 0x04000384 RID: 900
	[CustomEditField(Sections = "Rewards Preview")]
	public List<BoosterDbId> m_SpecificRewardsPreviewBoosters;

	// Token: 0x04000385 RID: 901
	[CustomEditField(Sections = "Rewards Preview")]
	public int m_HiddenRewardsPreviewCount;

	// Token: 0x04000386 RID: 902
	[CustomEditField(Sections = "Loc Strings")]
	public string m_LockedLocString;

	// Token: 0x04000387 RID: 903
	[CustomEditField(Sections = "Loc Strings")]
	public string m_LockedPurchaseLocString;

	// Token: 0x04000388 RID: 904
	private AdventureDbId m_AdventureId;

	// Token: 0x04000389 RID: 905
	private WingDbId m_WingId;

	// Token: 0x0400038A RID: 906
	private WingDbId m_OwnershipPrereq;

	// Token: 0x0400038B RID: 907
	private int m_SortOrder;

	// Token: 0x0400038C RID: 908
	private int m_UnlockOrder;

	// Token: 0x0400038D RID: 909
	private string m_WingName;

	// Token: 0x0400038E RID: 910
	private string m_ComingSoonLabel;

	// Token: 0x0400038F RID: 911
	private string m_RequiresLabel;

	// Token: 0x04000390 RID: 912
	private WingDbId m_OpenPrereq;

	// Token: 0x04000391 RID: 913
	private string m_OpeningDiscouragedLabel;

	// Token: 0x04000392 RID: 914
	private string m_OpeningDiscouragedWarning;

	// Token: 0x04000393 RID: 915
	private bool m_MustCompleteOpenPrereq;

	// Token: 0x04000394 RID: 916
	private bool m_UnlocksAutomatically;
}
