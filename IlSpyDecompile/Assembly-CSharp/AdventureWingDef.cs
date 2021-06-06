using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class AdventureWingDef : MonoBehaviour
{
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_WingPrefab;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_CoinPrefab;

	public bool CoinsStartFaceUp;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_RewardsPrefab;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_UnlockSpellPrefab;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_AccentPrefab;

	[CustomEditField(Sections = "Opening Quote", T = EditType.GAME_OBJECT)]
	public string m_OpenQuotePrefab;

	[CustomEditField(Sections = "Opening Quote", T = EditType.GAME_OBJECT)]
	public string m_OpenQuoteVOLine;

	[CustomEditField(Sections = "Opening Quote")]
	public float m_OpenQuoteDelay;

	[CustomEditField(Sections = "Opening Quote")]
	public bool m_PlayOpenQuoteInHeroic = true;

	[CustomEditField(Sections = "Wing Open Popup", T = EditType.GAME_OBJECT)]
	public string m_WingOpenPopup;

	[CustomEditField(Sections = "Complete Quote", T = EditType.GAME_OBJECT)]
	public string m_CompleteQuotePrefab;

	[CustomEditField(Sections = "Complete Quote", T = EditType.GAME_OBJECT)]
	public string m_CompleteQuoteVOLine;

	[CustomEditField(Sections = "Complete Quote", T = EditType.GAME_OBJECT)]
	public string m_CompleteQuoteNextWingLockedPrefab;

	[CustomEditField(Sections = "Complete Quote", T = EditType.GAME_OBJECT)]
	public string m_CompleteQuoteNextWingLockedVOLine;

	[CustomEditField(Sections = "Complete Quote", T = EditType.GAME_OBJECT)]
	public bool m_PlayCompleteQuoteInHeroic = true;

	[CustomEditField(Sections = "Rewards Preview")]
	public List<string> m_SpecificRewardsPreviewCards;

	[CustomEditField(Sections = "Rewards Preview")]
	public List<int> m_SpecificRewardsPreviewCardBacks;

	[CustomEditField(Sections = "Rewards Preview")]
	public List<BoosterDbId> m_SpecificRewardsPreviewBoosters;

	[CustomEditField(Sections = "Rewards Preview")]
	public int m_HiddenRewardsPreviewCount;

	[CustomEditField(Sections = "Loc Strings")]
	public string m_LockedLocString;

	[CustomEditField(Sections = "Loc Strings")]
	public string m_LockedPurchaseLocString;

	private AdventureDbId m_AdventureId;

	private WingDbId m_WingId;

	private WingDbId m_OwnershipPrereq;

	private int m_SortOrder;

	private int m_UnlockOrder;

	private string m_WingName;

	private string m_ComingSoonLabel;

	private string m_RequiresLabel;

	private WingDbId m_OpenPrereq;

	private string m_OpeningDiscouragedLabel;

	private string m_OpeningDiscouragedWarning;

	private bool m_MustCompleteOpenPrereq;

	private bool m_UnlocksAutomatically;

	public void Init(WingDbfRecord wingRecord)
	{
		m_AdventureId = (AdventureDbId)wingRecord.AdventureId;
		m_WingId = (WingDbId)wingRecord.ID;
		m_OwnershipPrereq = (WingDbId)wingRecord.OwnershipPrereqWingId;
		m_SortOrder = wingRecord.SortOrder;
		m_UnlockOrder = wingRecord.UnlockOrder;
		m_WingName = wingRecord.Name;
		m_ComingSoonLabel = wingRecord.ComingSoonLabel;
		m_RequiresLabel = wingRecord.RequiresLabel;
		m_OpenPrereq = (WingDbId)wingRecord.OpenPrereqWingId;
		m_OpeningDiscouragedLabel = wingRecord.OpenDiscouragedLabel;
		m_OpeningDiscouragedWarning = wingRecord.OpenDiscouragedWarning;
		m_MustCompleteOpenPrereq = wingRecord.MustCompleteOpenPrereq;
		m_UnlocksAutomatically = wingRecord.UnlocksAutomatically;
	}

	public AdventureDbId GetAdventureId()
	{
		return m_AdventureId;
	}

	public WingDbId GetWingId()
	{
		return m_WingId;
	}

	public WingDbId GetOwnershipPrereqId()
	{
		return m_OwnershipPrereq;
	}

	public int GetSortOrder()
	{
		return m_SortOrder;
	}

	public int GetUnlockOrder()
	{
		return m_UnlockOrder;
	}

	public string GetWingName()
	{
		return m_WingName;
	}

	public string GetComingSoonLabel()
	{
		return m_ComingSoonLabel;
	}

	public string GetRequiresLabel()
	{
		return m_RequiresLabel;
	}

	public WingDbId GetOpenPrereqId()
	{
		return m_OpenPrereq;
	}

	public string GetOpeningNotRecommendedLabel()
	{
		return m_OpeningDiscouragedLabel;
	}

	public string GetOpeningNotRecommendedWarning()
	{
		return m_OpeningDiscouragedWarning;
	}

	public bool GetMustCompleteOpenPrereq()
	{
		return m_MustCompleteOpenPrereq;
	}

	public bool GetUnlocksAutomatically()
	{
		return m_UnlocksAutomatically;
	}
}
