using System;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class AdventureDef : MonoBehaviour
{
	public enum BannerRewardType
	{
		AdventureCompleteReward,
		BannerManagerPopup
	}

	[Serializable]
	public class IntroConversationLine
	{
		public string CharacterPrefab;

		public string VoLinePrefab;
	}

	[CustomEditField(Sections = "Reward Banners")]
	public BannerRewardType m_BannerRewardType;

	[CustomEditField(Sections = "Reward Banners", T = EditType.GAME_OBJECT)]
	public string m_BannerRewardPrefab;

	[CustomEditField(Sections = "Quotes", T = EditType.GAME_OBJECT)]
	public string m_AdventureCompleteQuotePrefab;

	[CustomEditField(Sections = "Quotes", T = EditType.GAME_OBJECT)]
	public string m_AdventureCompleteQuoteVOLine;

	[CustomEditField(Sections = "Quotes", T = EditType.GAME_OBJECT)]
	public string m_AdventureEntryQuotePrefab;

	[CustomEditField(Sections = "Quotes", T = EditType.GAME_OBJECT)]
	public string m_AdventureEntryQuoteVOLine;

	[CustomEditField(Sections = "Banners", T = EditType.GAME_OBJECT)]
	public string m_AdventureIntroBannerPrefab;

	[CustomEditField(Sections = "Banners", T = EditType.GAME_OBJECT)]
	public string m_AdventureDeckSelectionTutorialBannerPrefab;

	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_ProgressDisplayPrefab;

	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_WingBottomBorderPrefab;

	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_DefaultQuotePrefab;

	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_ChooserButtonPrefab;

	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_ChooserSubButtonPrefab;

	[CustomEditField(Sections = "Chooser Button", T = EditType.TEXTURE)]
	public string m_Texture;

	[CustomEditField(Sections = "Chooser Button")]
	public Vector2 m_TextureTiling = Vector2.one;

	[CustomEditField(Sections = "Chooser Button")]
	public Vector2 m_TextureOffset = Vector2.zero;

	[CustomEditField(Sections = "Chooser Button")]
	public AdventureDbId m_AdventureToNestUnder;

	[CustomEditField(Sections = "Intro Conversation")]
	public bool m_ShouldOnlyPlayIntroOnFirstSeen;

	[CustomEditField(Sections = "Intro Conversation")]
	public List<IntroConversationLine> m_IntroConversationLines;

	private AdventureDbId m_AdventureId;

	private string m_AdventureName;

	private Map<AdventureModeDbId, AdventureSubDef> m_SubDefs = new Map<AdventureModeDbId, AdventureSubDef>();

	private int m_SortOrder;

	public bool IsNestedUnderAnotherAdventureOnChooserScreen => m_AdventureToNestUnder != AdventureDbId.INVALID;

	public void Init(AdventureDbfRecord advRecord, List<AdventureDataDbfRecord> advDataRecords)
	{
		m_AdventureId = (AdventureDbId)advRecord.ID;
		m_AdventureName = advRecord.Name;
		m_SortOrder = advRecord.SortOrder;
		foreach (AdventureDataDbfRecord advDataRecord in advDataRecords)
		{
			if (advDataRecord.AdventureId != (int)m_AdventureId)
			{
				continue;
			}
			string adventureSubDefPrefab = advDataRecord.AdventureSubDefPrefab;
			if (string.IsNullOrEmpty(adventureSubDefPrefab))
			{
				continue;
			}
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(adventureSubDefPrefab);
			if (!(gameObject == null))
			{
				AdventureSubDef component = gameObject.GetComponent<AdventureSubDef>();
				if (component == null)
				{
					Debug.LogError($"{adventureSubDefPrefab} object does not contain AdventureSubDef component.");
					UnityEngine.Object.Destroy(gameObject);
				}
				else
				{
					component.Init(advDataRecord);
					m_SubDefs.Add(component.GetAdventureModeId(), component);
				}
			}
		}
	}

	public AdventureDbId GetAdventureId()
	{
		return m_AdventureId;
	}

	public string GetAdventureName()
	{
		return m_AdventureName;
	}

	public AdventureSubDef GetSubDef(AdventureModeDbId modeId)
	{
		AdventureSubDef value = null;
		m_SubDefs.TryGetValue(modeId, out value);
		return value;
	}

	public List<AdventureSubDef> GetSortedSubDefs()
	{
		List<AdventureSubDef> list = new List<AdventureSubDef>(m_SubDefs.Values);
		list.Sort((AdventureSubDef l, AdventureSubDef r) => l.GetSortOrder() - r.GetSortOrder());
		return list;
	}

	public int GetSortOrder()
	{
		return m_SortOrder;
	}

	public bool IsActiveAndPlayable()
	{
		foreach (WingDbfRecord record in GameDbf.Wing.GetRecords())
		{
			if (record.AdventureId == (int)GetAdventureId() && AdventureProgressMgr.IsWingEventActive(record.ID))
			{
				return true;
			}
		}
		return false;
	}
}
