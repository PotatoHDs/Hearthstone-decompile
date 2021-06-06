using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000032 RID: 50
[CustomEditClass]
public class AdventureDef : MonoBehaviour
{
	// Token: 0x060001C5 RID: 453 RVA: 0x0000A61C File Offset: 0x0000881C
	public void Init(AdventureDbfRecord advRecord, List<AdventureDataDbfRecord> advDataRecords)
	{
		this.m_AdventureId = (AdventureDbId)advRecord.ID;
		this.m_AdventureName = advRecord.Name;
		this.m_SortOrder = advRecord.SortOrder;
		foreach (AdventureDataDbfRecord adventureDataDbfRecord in advDataRecords)
		{
			if (adventureDataDbfRecord.AdventureId == (int)this.m_AdventureId)
			{
				string adventureSubDefPrefab = adventureDataDbfRecord.AdventureSubDefPrefab;
				if (!string.IsNullOrEmpty(adventureSubDefPrefab))
				{
					GameObject gameObject = AssetLoader.Get().InstantiatePrefab(adventureSubDefPrefab, AssetLoadingOptions.None);
					if (!(gameObject == null))
					{
						AdventureSubDef component = gameObject.GetComponent<AdventureSubDef>();
						if (component == null)
						{
							Debug.LogError(string.Format("{0} object does not contain AdventureSubDef component.", adventureSubDefPrefab));
							UnityEngine.Object.Destroy(gameObject);
						}
						else
						{
							component.Init(adventureDataDbfRecord);
							this.m_SubDefs.Add(component.GetAdventureModeId(), component);
						}
					}
				}
			}
		}
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000A710 File Offset: 0x00008910
	public AdventureDbId GetAdventureId()
	{
		return this.m_AdventureId;
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000A718 File Offset: 0x00008918
	public string GetAdventureName()
	{
		return this.m_AdventureName;
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000A720 File Offset: 0x00008920
	public AdventureSubDef GetSubDef(AdventureModeDbId modeId)
	{
		AdventureSubDef result = null;
		this.m_SubDefs.TryGetValue(modeId, out result);
		return result;
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0000A73F File Offset: 0x0000893F
	public List<AdventureSubDef> GetSortedSubDefs()
	{
		List<AdventureSubDef> list = new List<AdventureSubDef>(this.m_SubDefs.Values);
		list.Sort((AdventureSubDef l, AdventureSubDef r) => l.GetSortOrder() - r.GetSortOrder());
		return list;
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0000A776 File Offset: 0x00008976
	public int GetSortOrder()
	{
		return this.m_SortOrder;
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0000A780 File Offset: 0x00008980
	public bool IsActiveAndPlayable()
	{
		foreach (WingDbfRecord wingDbfRecord in GameDbf.Wing.GetRecords())
		{
			if (wingDbfRecord.AdventureId == (int)this.GetAdventureId() && AdventureProgressMgr.IsWingEventActive(wingDbfRecord.ID))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060001CC RID: 460 RVA: 0x0000A7F4 File Offset: 0x000089F4
	public bool IsNestedUnderAnotherAdventureOnChooserScreen
	{
		get
		{
			return this.m_AdventureToNestUnder > AdventureDbId.INVALID;
		}
	}

	// Token: 0x0400013A RID: 314
	[CustomEditField(Sections = "Reward Banners")]
	public AdventureDef.BannerRewardType m_BannerRewardType;

	// Token: 0x0400013B RID: 315
	[CustomEditField(Sections = "Reward Banners", T = EditType.GAME_OBJECT)]
	public string m_BannerRewardPrefab;

	// Token: 0x0400013C RID: 316
	[CustomEditField(Sections = "Quotes", T = EditType.GAME_OBJECT)]
	public string m_AdventureCompleteQuotePrefab;

	// Token: 0x0400013D RID: 317
	[CustomEditField(Sections = "Quotes", T = EditType.GAME_OBJECT)]
	public string m_AdventureCompleteQuoteVOLine;

	// Token: 0x0400013E RID: 318
	[CustomEditField(Sections = "Quotes", T = EditType.GAME_OBJECT)]
	public string m_AdventureEntryQuotePrefab;

	// Token: 0x0400013F RID: 319
	[CustomEditField(Sections = "Quotes", T = EditType.GAME_OBJECT)]
	public string m_AdventureEntryQuoteVOLine;

	// Token: 0x04000140 RID: 320
	[CustomEditField(Sections = "Banners", T = EditType.GAME_OBJECT)]
	public string m_AdventureIntroBannerPrefab;

	// Token: 0x04000141 RID: 321
	[CustomEditField(Sections = "Banners", T = EditType.GAME_OBJECT)]
	public string m_AdventureDeckSelectionTutorialBannerPrefab;

	// Token: 0x04000142 RID: 322
	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_ProgressDisplayPrefab;

	// Token: 0x04000143 RID: 323
	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_WingBottomBorderPrefab;

	// Token: 0x04000144 RID: 324
	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_DefaultQuotePrefab;

	// Token: 0x04000145 RID: 325
	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_ChooserButtonPrefab;

	// Token: 0x04000146 RID: 326
	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_ChooserSubButtonPrefab;

	// Token: 0x04000147 RID: 327
	[CustomEditField(Sections = "Chooser Button", T = EditType.TEXTURE)]
	public string m_Texture;

	// Token: 0x04000148 RID: 328
	[CustomEditField(Sections = "Chooser Button")]
	public Vector2 m_TextureTiling = Vector2.one;

	// Token: 0x04000149 RID: 329
	[CustomEditField(Sections = "Chooser Button")]
	public Vector2 m_TextureOffset = Vector2.zero;

	// Token: 0x0400014A RID: 330
	[CustomEditField(Sections = "Chooser Button")]
	public AdventureDbId m_AdventureToNestUnder;

	// Token: 0x0400014B RID: 331
	[CustomEditField(Sections = "Intro Conversation")]
	public bool m_ShouldOnlyPlayIntroOnFirstSeen;

	// Token: 0x0400014C RID: 332
	[CustomEditField(Sections = "Intro Conversation")]
	public List<AdventureDef.IntroConversationLine> m_IntroConversationLines;

	// Token: 0x0400014D RID: 333
	private AdventureDbId m_AdventureId;

	// Token: 0x0400014E RID: 334
	private string m_AdventureName;

	// Token: 0x0400014F RID: 335
	private Map<AdventureModeDbId, AdventureSubDef> m_SubDefs = new Map<AdventureModeDbId, AdventureSubDef>();

	// Token: 0x04000150 RID: 336
	private int m_SortOrder;

	// Token: 0x0200129C RID: 4764
	public enum BannerRewardType
	{
		// Token: 0x0400A400 RID: 41984
		AdventureCompleteReward,
		// Token: 0x0400A401 RID: 41985
		BannerManagerPopup
	}

	// Token: 0x0200129D RID: 4765
	[Serializable]
	public class IntroConversationLine
	{
		// Token: 0x0400A402 RID: 41986
		public string CharacterPrefab;

		// Token: 0x0400A403 RID: 41987
		public string VoLinePrefab;
	}
}
