using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200016B RID: 363
[Serializable]
public class AdventureDbfRecord : DbfRecord
{
	// Token: 0x17000190 RID: 400
	// (get) Token: 0x060016DD RID: 5853 RVA: 0x0007F642 File Offset: 0x0007D842
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x17000191 RID: 401
	// (get) Token: 0x060016DE RID: 5854 RVA: 0x0007F64A File Offset: 0x0007D84A
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000192 RID: 402
	// (get) Token: 0x060016DF RID: 5855 RVA: 0x0007F652 File Offset: 0x0007D852
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x17000193 RID: 403
	// (get) Token: 0x060016E0 RID: 5856 RVA: 0x0007F65A File Offset: 0x0007D85A
	[DbfField("STORE_BUY_BUTTON_LABEL")]
	public DbfLocValue StoreBuyButtonLabel
	{
		get
		{
			return this.m_storeBuyButtonLabel;
		}
	}

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x060016E1 RID: 5857 RVA: 0x0007F662 File Offset: 0x0007D862
	[DbfField("STORE_BUY_WINGS_1_HEADLINE")]
	public DbfLocValue StoreBuyWings1Headline
	{
		get
		{
			return this.m_storeBuyWings1Headline;
		}
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0007F66A File Offset: 0x0007D86A
	[DbfField("STORE_BUY_WINGS_2_HEADLINE")]
	public DbfLocValue StoreBuyWings2Headline
	{
		get
		{
			return this.m_storeBuyWings2Headline;
		}
	}

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x060016E3 RID: 5859 RVA: 0x0007F672 File Offset: 0x0007D872
	[DbfField("STORE_BUY_WINGS_3_HEADLINE")]
	public DbfLocValue StoreBuyWings3Headline
	{
		get
		{
			return this.m_storeBuyWings3Headline;
		}
	}

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x060016E4 RID: 5860 RVA: 0x0007F67A File Offset: 0x0007D87A
	[DbfField("STORE_BUY_WINGS_4_HEADLINE")]
	public DbfLocValue StoreBuyWings4Headline
	{
		get
		{
			return this.m_storeBuyWings4Headline;
		}
	}

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x060016E5 RID: 5861 RVA: 0x0007F682 File Offset: 0x0007D882
	[DbfField("STORE_BUY_WINGS_5_HEADLINE")]
	public DbfLocValue StoreBuyWings5Headline
	{
		get
		{
			return this.m_storeBuyWings5Headline;
		}
	}

	// Token: 0x17000199 RID: 409
	// (get) Token: 0x060016E6 RID: 5862 RVA: 0x0007F68A File Offset: 0x0007D88A
	[DbfField("STORE_OWNED_HEADLINE")]
	public DbfLocValue StoreOwnedHeadline
	{
		get
		{
			return this.m_storeOwnedHeadline;
		}
	}

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0007F692 File Offset: 0x0007D892
	[DbfField("STORE_PREORDER_HEADLINE")]
	public DbfLocValue StorePreorderHeadline
	{
		get
		{
			return this.m_storePreorderHeadline;
		}
	}

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0007F69A File Offset: 0x0007D89A
	[DbfField("STORE_BUY_WINGS_1_DESC")]
	public DbfLocValue StoreBuyWings1Desc
	{
		get
		{
			return this.m_storeBuyWings1Desc;
		}
	}

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0007F6A2 File Offset: 0x0007D8A2
	[DbfField("STORE_BUY_WINGS_2_DESC")]
	public DbfLocValue StoreBuyWings2Desc
	{
		get
		{
			return this.m_storeBuyWings2Desc;
		}
	}

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x060016EA RID: 5866 RVA: 0x0007F6AA File Offset: 0x0007D8AA
	[DbfField("STORE_BUY_WINGS_3_DESC")]
	public DbfLocValue StoreBuyWings3Desc
	{
		get
		{
			return this.m_storeBuyWings3Desc;
		}
	}

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x060016EB RID: 5867 RVA: 0x0007F6B2 File Offset: 0x0007D8B2
	[DbfField("STORE_BUY_WINGS_4_DESC")]
	public DbfLocValue StoreBuyWings4Desc
	{
		get
		{
			return this.m_storeBuyWings4Desc;
		}
	}

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x060016EC RID: 5868 RVA: 0x0007F6BA File Offset: 0x0007D8BA
	[DbfField("STORE_BUY_WINGS_5_DESC")]
	public DbfLocValue StoreBuyWings5Desc
	{
		get
		{
			return this.m_storeBuyWings5Desc;
		}
	}

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x060016ED RID: 5869 RVA: 0x0007F6C2 File Offset: 0x0007D8C2
	[DbfField("STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_TRUE")]
	public DbfLocValue StoreBuyRemainingWingsDescTimelockedTrue
	{
		get
		{
			return this.m_storeBuyRemainingWingsDescTimelockedTrue;
		}
	}

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x060016EE RID: 5870 RVA: 0x0007F6CA File Offset: 0x0007D8CA
	[DbfField("STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_FALSE")]
	public DbfLocValue StoreBuyRemainingWingsDescTimelockedFalse
	{
		get
		{
			return this.m_storeBuyRemainingWingsDescTimelockedFalse;
		}
	}

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x060016EF RID: 5871 RVA: 0x0007F6D2 File Offset: 0x0007D8D2
	[DbfField("STORE_OWNED_DESC")]
	public DbfLocValue StoreOwnedDesc
	{
		get
		{
			return this.m_storeOwnedDesc;
		}
	}

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x060016F0 RID: 5872 RVA: 0x0007F6DA File Offset: 0x0007D8DA
	[DbfField("STORE_PREORDER_WINGS_1_DESC")]
	public DbfLocValue StorePreorderWings1Desc
	{
		get
		{
			return this.m_storePreorderWings1Desc;
		}
	}

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0007F6E2 File Offset: 0x0007D8E2
	[DbfField("STORE_PREORDER_WINGS_2_DESC")]
	public DbfLocValue StorePreorderWings2Desc
	{
		get
		{
			return this.m_storePreorderWings2Desc;
		}
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0007F6EA File Offset: 0x0007D8EA
	[DbfField("STORE_PREORDER_WINGS_3_DESC")]
	public DbfLocValue StorePreorderWings3Desc
	{
		get
		{
			return this.m_storePreorderWings3Desc;
		}
	}

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0007F6F2 File Offset: 0x0007D8F2
	[DbfField("STORE_PREORDER_WINGS_4_DESC")]
	public DbfLocValue StorePreorderWings4Desc
	{
		get
		{
			return this.m_storePreorderWings4Desc;
		}
	}

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0007F6FA File Offset: 0x0007D8FA
	[DbfField("STORE_PREORDER_WINGS_5_DESC")]
	public DbfLocValue StorePreorderWings5Desc
	{
		get
		{
			return this.m_storePreorderWings5Desc;
		}
	}

	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0007F702 File Offset: 0x0007D902
	[DbfField("STORE_PREORDER_RADIO_TEXT")]
	public DbfLocValue StorePreorderRadioText
	{
		get
		{
			return this.m_storePreorderRadioText;
		}
	}

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x060016F6 RID: 5878 RVA: 0x0007F70A File Offset: 0x0007D90A
	[DbfField("STORE_PREVIEW_REWARDS_TEXT")]
	public DbfLocValue StorePreviewRewardsText
	{
		get
		{
			return this.m_storePreviewRewardsText;
		}
	}

	// Token: 0x170001AA RID: 426
	// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0007F712 File Offset: 0x0007D912
	[DbfField("ADVENTURE_DEF_PREFAB")]
	public string AdventureDefPrefab
	{
		get
		{
			return this.m_adventureDefPrefab;
		}
	}

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x060016F8 RID: 5880 RVA: 0x0007F71A File Offset: 0x0007D91A
	[DbfField("STORE_PREFAB")]
	public string StorePrefab
	{
		get
		{
			return this.m_storePrefab;
		}
	}

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x060016F9 RID: 5881 RVA: 0x0007F722 File Offset: 0x0007D922
	[DbfField("LEAVING_SOON")]
	public bool LeavingSoon
	{
		get
		{
			return this.m_leavingSoon;
		}
	}

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x060016FA RID: 5882 RVA: 0x0007F72A File Offset: 0x0007D92A
	[DbfField("LEAVING_SOON_TEXT")]
	public DbfLocValue LeavingSoonText
	{
		get
		{
			return this.m_leavingSoonText;
		}
	}

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x060016FB RID: 5883 RVA: 0x0007F732 File Offset: 0x0007D932
	[DbfField("GAME_MODE_ICON")]
	public string GameModeIcon
	{
		get
		{
			return this.m_gameModeIcon;
		}
	}

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x060016FC RID: 5884 RVA: 0x0007F73A File Offset: 0x0007D93A
	[DbfField("PRODUCT_STRING_KEY")]
	public string ProductStringKey
	{
		get
		{
			return this.m_productStringKey;
		}
	}

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x060016FD RID: 5885 RVA: 0x0007F742 File Offset: 0x0007D942
	[DbfField("STANDARD_EVENT")]
	public string StandardEvent
	{
		get
		{
			return this.m_standardEvent;
		}
	}

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x060016FE RID: 5886 RVA: 0x0007F74A File Offset: 0x0007D94A
	[DbfField("COMING_SOON_EVENT")]
	public string ComingSoonEvent
	{
		get
		{
			return this.m_comingSoonEvent;
		}
	}

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x060016FF RID: 5887 RVA: 0x0007F752 File Offset: 0x0007D952
	[DbfField("COMING_SOON_TEXT")]
	public DbfLocValue ComingSoonText
	{
		get
		{
			return this.m_comingSoonText;
		}
	}

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x06001700 RID: 5888 RVA: 0x0007F75A File Offset: 0x0007D95A
	[DbfField("MAP_PAGE_HAS_BUTTONS_TO_CHAPTERS")]
	public bool MapPageHasButtonsToChapters
	{
		get
		{
			return this.m_mapPageHasButtonsToChapters;
		}
	}

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x06001701 RID: 5889 RVA: 0x0007F762 File Offset: 0x0007D962
	public List<AdventureDataDbfRecord> AdventureData
	{
		get
		{
			return GameDbf.AdventureData.GetRecords((AdventureDataDbfRecord r) => r.AdventureId == base.ID, -1);
		}
	}

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x06001702 RID: 5890 RVA: 0x0007F77B File Offset: 0x0007D97B
	public List<AdventureDeckDbfRecord> AdventureDecks
	{
		get
		{
			return GameDbf.AdventureDeck.GetRecords((AdventureDeckDbfRecord r) => r.AdventureId == base.ID, -1);
		}
	}

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x06001703 RID: 5891 RVA: 0x0007F794 File Offset: 0x0007D994
	public List<AdventureGuestHeroesDbfRecord> GuestHeroes
	{
		get
		{
			return GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == base.ID, -1);
		}
	}

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x06001704 RID: 5892 RVA: 0x0007F7AD File Offset: 0x0007D9AD
	public List<AdventureHeroPowerDbfRecord> AdventureHeroPowers
	{
		get
		{
			return GameDbf.AdventureHeroPower.GetRecords((AdventureHeroPowerDbfRecord r) => r.AdventureId == base.ID, -1);
		}
	}

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x06001705 RID: 5893 RVA: 0x0007F7C6 File Offset: 0x0007D9C6
	public List<AdventureLoadoutTreasuresDbfRecord> AdventureLoadoutTreasures
	{
		get
		{
			return GameDbf.AdventureLoadoutTreasures.GetRecords((AdventureLoadoutTreasuresDbfRecord r) => r.AdventureId == base.ID, -1);
		}
	}

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x06001706 RID: 5894 RVA: 0x0007F7DF File Offset: 0x0007D9DF
	public List<WingDbfRecord> Wings
	{
		get
		{
			return GameDbf.Wing.GetRecords((WingDbfRecord r) => r.AdventureId == base.ID, -1);
		}
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x0007F7F8 File Offset: 0x0007D9F8
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x0007F801 File Offset: 0x0007DA01
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x0007F81B File Offset: 0x0007DA1B
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x0600170A RID: 5898 RVA: 0x0007F824 File Offset: 0x0007DA24
	public void SetStoreBuyButtonLabel(DbfLocValue v)
	{
		this.m_storeBuyButtonLabel = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_BUTTON_LABEL");
	}

	// Token: 0x0600170B RID: 5899 RVA: 0x0007F83E File Offset: 0x0007DA3E
	public void SetStoreBuyWings1Headline(DbfLocValue v)
	{
		this.m_storeBuyWings1Headline = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_1_HEADLINE");
	}

	// Token: 0x0600170C RID: 5900 RVA: 0x0007F858 File Offset: 0x0007DA58
	public void SetStoreBuyWings2Headline(DbfLocValue v)
	{
		this.m_storeBuyWings2Headline = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_2_HEADLINE");
	}

	// Token: 0x0600170D RID: 5901 RVA: 0x0007F872 File Offset: 0x0007DA72
	public void SetStoreBuyWings3Headline(DbfLocValue v)
	{
		this.m_storeBuyWings3Headline = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_3_HEADLINE");
	}

	// Token: 0x0600170E RID: 5902 RVA: 0x0007F88C File Offset: 0x0007DA8C
	public void SetStoreBuyWings4Headline(DbfLocValue v)
	{
		this.m_storeBuyWings4Headline = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_4_HEADLINE");
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x0007F8A6 File Offset: 0x0007DAA6
	public void SetStoreBuyWings5Headline(DbfLocValue v)
	{
		this.m_storeBuyWings5Headline = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_5_HEADLINE");
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x0007F8C0 File Offset: 0x0007DAC0
	public void SetStoreOwnedHeadline(DbfLocValue v)
	{
		this.m_storeOwnedHeadline = v;
		v.SetDebugInfo(base.ID, "STORE_OWNED_HEADLINE");
	}

	// Token: 0x06001711 RID: 5905 RVA: 0x0007F8DA File Offset: 0x0007DADA
	public void SetStorePreorderHeadline(DbfLocValue v)
	{
		this.m_storePreorderHeadline = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_HEADLINE");
	}

	// Token: 0x06001712 RID: 5906 RVA: 0x0007F8F4 File Offset: 0x0007DAF4
	public void SetStoreBuyWings1Desc(DbfLocValue v)
	{
		this.m_storeBuyWings1Desc = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_1_DESC");
	}

	// Token: 0x06001713 RID: 5907 RVA: 0x0007F90E File Offset: 0x0007DB0E
	public void SetStoreBuyWings2Desc(DbfLocValue v)
	{
		this.m_storeBuyWings2Desc = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_2_DESC");
	}

	// Token: 0x06001714 RID: 5908 RVA: 0x0007F928 File Offset: 0x0007DB28
	public void SetStoreBuyWings3Desc(DbfLocValue v)
	{
		this.m_storeBuyWings3Desc = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_3_DESC");
	}

	// Token: 0x06001715 RID: 5909 RVA: 0x0007F942 File Offset: 0x0007DB42
	public void SetStoreBuyWings4Desc(DbfLocValue v)
	{
		this.m_storeBuyWings4Desc = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_4_DESC");
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x0007F95C File Offset: 0x0007DB5C
	public void SetStoreBuyWings5Desc(DbfLocValue v)
	{
		this.m_storeBuyWings5Desc = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_5_DESC");
	}

	// Token: 0x06001717 RID: 5911 RVA: 0x0007F976 File Offset: 0x0007DB76
	public void SetStoreBuyRemainingWingsDescTimelockedTrue(DbfLocValue v)
	{
		this.m_storeBuyRemainingWingsDescTimelockedTrue = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_TRUE");
	}

	// Token: 0x06001718 RID: 5912 RVA: 0x0007F990 File Offset: 0x0007DB90
	public void SetStoreBuyRemainingWingsDescTimelockedFalse(DbfLocValue v)
	{
		this.m_storeBuyRemainingWingsDescTimelockedFalse = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_FALSE");
	}

	// Token: 0x06001719 RID: 5913 RVA: 0x0007F9AA File Offset: 0x0007DBAA
	public void SetStoreOwnedDesc(DbfLocValue v)
	{
		this.m_storeOwnedDesc = v;
		v.SetDebugInfo(base.ID, "STORE_OWNED_DESC");
	}

	// Token: 0x0600171A RID: 5914 RVA: 0x0007F9C4 File Offset: 0x0007DBC4
	public void SetStorePreorderWings1Desc(DbfLocValue v)
	{
		this.m_storePreorderWings1Desc = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_WINGS_1_DESC");
	}

	// Token: 0x0600171B RID: 5915 RVA: 0x0007F9DE File Offset: 0x0007DBDE
	public void SetStorePreorderWings2Desc(DbfLocValue v)
	{
		this.m_storePreorderWings2Desc = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_WINGS_2_DESC");
	}

	// Token: 0x0600171C RID: 5916 RVA: 0x0007F9F8 File Offset: 0x0007DBF8
	public void SetStorePreorderWings3Desc(DbfLocValue v)
	{
		this.m_storePreorderWings3Desc = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_WINGS_3_DESC");
	}

	// Token: 0x0600171D RID: 5917 RVA: 0x0007FA12 File Offset: 0x0007DC12
	public void SetStorePreorderWings4Desc(DbfLocValue v)
	{
		this.m_storePreorderWings4Desc = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_WINGS_4_DESC");
	}

	// Token: 0x0600171E RID: 5918 RVA: 0x0007FA2C File Offset: 0x0007DC2C
	public void SetStorePreorderWings5Desc(DbfLocValue v)
	{
		this.m_storePreorderWings5Desc = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_WINGS_5_DESC");
	}

	// Token: 0x0600171F RID: 5919 RVA: 0x0007FA46 File Offset: 0x0007DC46
	public void SetStorePreorderRadioText(DbfLocValue v)
	{
		this.m_storePreorderRadioText = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_RADIO_TEXT");
	}

	// Token: 0x06001720 RID: 5920 RVA: 0x0007FA60 File Offset: 0x0007DC60
	public void SetStorePreviewRewardsText(DbfLocValue v)
	{
		this.m_storePreviewRewardsText = v;
		v.SetDebugInfo(base.ID, "STORE_PREVIEW_REWARDS_TEXT");
	}

	// Token: 0x06001721 RID: 5921 RVA: 0x0007FA7A File Offset: 0x0007DC7A
	public void SetAdventureDefPrefab(string v)
	{
		this.m_adventureDefPrefab = v;
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x0007FA83 File Offset: 0x0007DC83
	public void SetStorePrefab(string v)
	{
		this.m_storePrefab = v;
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x0007FA8C File Offset: 0x0007DC8C
	public void SetLeavingSoon(bool v)
	{
		this.m_leavingSoon = v;
	}

	// Token: 0x06001724 RID: 5924 RVA: 0x0007FA95 File Offset: 0x0007DC95
	public void SetLeavingSoonText(DbfLocValue v)
	{
		this.m_leavingSoonText = v;
		v.SetDebugInfo(base.ID, "LEAVING_SOON_TEXT");
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x0007FAAF File Offset: 0x0007DCAF
	public void SetGameModeIcon(string v)
	{
		this.m_gameModeIcon = v;
	}

	// Token: 0x06001726 RID: 5926 RVA: 0x0007FAB8 File Offset: 0x0007DCB8
	public void SetProductStringKey(string v)
	{
		this.m_productStringKey = v;
	}

	// Token: 0x06001727 RID: 5927 RVA: 0x0007FAC1 File Offset: 0x0007DCC1
	public void SetStandardEvent(string v)
	{
		this.m_standardEvent = v;
	}

	// Token: 0x06001728 RID: 5928 RVA: 0x0007FACA File Offset: 0x0007DCCA
	public void SetComingSoonEvent(string v)
	{
		this.m_comingSoonEvent = v;
	}

	// Token: 0x06001729 RID: 5929 RVA: 0x0007FAD3 File Offset: 0x0007DCD3
	public void SetComingSoonText(DbfLocValue v)
	{
		this.m_comingSoonText = v;
		v.SetDebugInfo(base.ID, "COMING_SOON_TEXT");
	}

	// Token: 0x0600172A RID: 5930 RVA: 0x0007FAED File Offset: 0x0007DCED
	public void SetMapPageHasButtonsToChapters(bool v)
	{
		this.m_mapPageHasButtonsToChapters = v;
	}

	// Token: 0x0600172B RID: 5931 RVA: 0x0007FAF8 File Offset: 0x0007DCF8
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1688767887U)
		{
			if (num <= 646730901U)
			{
				if (num <= 397921193U)
				{
					if (num <= 72904445U)
					{
						if (num != 72252647U)
						{
							if (num == 72904445U)
							{
								if (name == "STORE_BUY_WINGS_2_DESC")
								{
									return this.m_storeBuyWings2Desc;
								}
							}
						}
						else if (name == "STORE_BUY_WINGS_4_DESC")
						{
							return this.m_storeBuyWings4Desc;
						}
					}
					else if (num != 389673047U)
					{
						if (num == 397921193U)
						{
							if (name == "STORE_PREORDER_WINGS_5_DESC")
							{
								return this.m_storePreorderWings5Desc;
							}
						}
					}
					else if (name == "LEAVING_SOON_TEXT")
					{
						return this.m_leavingSoonText;
					}
				}
				else if (num <= 554784477U)
				{
					if (num != 499019123U)
					{
						if (num == 554784477U)
						{
							if (name == "STORE_PREFAB")
							{
								return this.m_storePrefab;
							}
						}
					}
					else if (name == "STORE_BUY_WINGS_1_HEADLINE")
					{
						return this.m_storeBuyWings1Headline;
					}
				}
				else if (num != 598991083U)
				{
					if (num != 634862270U)
					{
						if (num == 646730901U)
						{
							if (name == "STORE_OWNED_HEADLINE")
							{
								return this.m_storeOwnedHeadline;
							}
						}
					}
					else if (name == "STORE_PREORDER_RADIO_TEXT")
					{
						return this.m_storePreorderRadioText;
					}
				}
				else if (name == "STORE_PREORDER_WINGS_3_DESC")
				{
					return this.m_storePreorderWings3Desc;
				}
			}
			else if (num <= 1387956774U)
			{
				if (num <= 1164573759U)
				{
					if (num != 864600850U)
					{
						if (num == 1164573759U)
						{
							if (name == "LEAVING_SOON")
							{
								return this.m_leavingSoon;
							}
						}
					}
					else if (name == "COMING_SOON_TEXT")
					{
						return this.m_comingSoonText;
					}
				}
				else if (num != 1240037012U)
				{
					if (num == 1387956774U)
					{
						if (name == "NAME")
						{
							return this.m_name;
						}
					}
				}
				else if (name == "STORE_BUY_WINGS_2_HEADLINE")
				{
					return this.m_storeBuyWings2Headline;
				}
			}
			else if (num <= 1458105184U)
			{
				if (num != 1424749021U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "GAME_MODE_ICON")
				{
					return this.m_gameModeIcon;
				}
			}
			else if (num != 1491069082U)
			{
				if (num != 1553026664U)
				{
					if (num == 1688767887U)
					{
						if (name == "STORE_BUY_BUTTON_LABEL")
						{
							return this.m_storeBuyButtonLabel;
						}
					}
				}
				else if (name == "PRODUCT_STRING_KEY")
				{
					return this.m_productStringKey;
				}
			}
			else if (name == "STORE_BUY_WINGS_5_DESC")
			{
				return this.m_storeBuyWings5Desc;
			}
		}
		else if (num <= 2609858387U)
		{
			if (num <= 2278395484U)
			{
				if (num <= 2191767247U)
				{
					if (num != 2143204526U)
					{
						if (num == 2191767247U)
						{
							if (name == "STANDARD_EVENT")
							{
								return this.m_standardEvent;
							}
						}
					}
					else if (name == "STORE_PREORDER_WINGS_2_DESC")
					{
						return this.m_storePreorderWings2Desc;
					}
				}
				else if (num != 2269500855U)
				{
					if (num == 2278395484U)
					{
						if (name == "STORE_PREVIEW_REWARDS_TEXT")
						{
							return this.m_storePreviewRewardsText;
						}
					}
				}
				else if (name == "STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_FALSE")
				{
					return this.m_storeBuyRemainingWingsDescTimelockedFalse;
				}
			}
			else if (num <= 2450510477U)
			{
				if (num != 2377316180U)
				{
					if (num == 2450510477U)
					{
						if (name == "STORE_PREORDER_HEADLINE")
						{
							return this.m_storePreorderHeadline;
						}
					}
				}
				else if (name == "STORE_OWNED_DESC")
				{
					return this.m_storeOwnedDesc;
				}
			}
			else if (num != 2462803332U)
			{
				if (num != 2558737930U)
				{
					if (num == 2609858387U)
					{
						if (name == "MAP_PAGE_HAS_BUTTONS_TO_CHAPTERS")
						{
							return this.m_mapPageHasButtonsToChapters;
						}
					}
				}
				else if (name == "STORE_BUY_WINGS_4_HEADLINE")
				{
					return this.m_storeBuyWings4Headline;
				}
			}
			else if (name == "STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_TRUE")
			{
				return this.m_storeBuyRemainingWingsDescTimelockedTrue;
			}
		}
		else if (num <= 3687130011U)
		{
			if (num <= 3022554311U)
			{
				if (num != 2658528061U)
				{
					if (num == 3022554311U)
					{
						if (name == "NOTE_DESC")
						{
							return this.m_noteDesc;
						}
					}
				}
				else if (name == "STORE_PREORDER_WINGS_1_DESC")
				{
					return this.m_storePreorderWings1Desc;
				}
			}
			else if (num != 3034224585U)
			{
				if (num != 3425700768U)
				{
					if (num == 3687130011U)
					{
						if (name == "COMING_SOON_EVENT")
						{
							return this.m_comingSoonEvent;
						}
					}
				}
				else if (name == "STORE_BUY_WINGS_3_DESC")
				{
					return this.m_storeBuyWings3Desc;
				}
			}
			else if (name == "STORE_BUY_WINGS_3_HEADLINE")
			{
				return this.m_storeBuyWings3Headline;
			}
		}
		else if (num <= 3977533932U)
		{
			if (num != 3958914583U)
			{
				if (num == 3977533932U)
				{
					if (name == "STORE_PREORDER_WINGS_4_DESC")
					{
						return this.m_storePreorderWings4Desc;
					}
				}
			}
			else if (name == "STORE_BUY_WINGS_5_HEADLINE")
			{
				return this.m_storeBuyWings5Headline;
			}
		}
		else if (num != 3991768310U)
		{
			if (num != 4214602626U)
			{
				if (num == 4277005678U)
				{
					if (name == "STORE_BUY_WINGS_1_DESC")
					{
						return this.m_storeBuyWings1Desc;
					}
				}
			}
			else if (name == "SORT_ORDER")
			{
				return this.m_sortOrder;
			}
		}
		else if (name == "ADVENTURE_DEF_PREFAB")
		{
			return this.m_adventureDefPrefab;
		}
		return null;
	}

	// Token: 0x0600172C RID: 5932 RVA: 0x00080194 File Offset: 0x0007E394
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1688767887U)
		{
			if (num <= 646730901U)
			{
				if (num <= 397921193U)
				{
					if (num <= 72904445U)
					{
						if (num != 72252647U)
						{
							if (num != 72904445U)
							{
								return;
							}
							if (!(name == "STORE_BUY_WINGS_2_DESC"))
							{
								return;
							}
							this.m_storeBuyWings2Desc = (DbfLocValue)val;
							return;
						}
						else
						{
							if (!(name == "STORE_BUY_WINGS_4_DESC"))
							{
								return;
							}
							this.m_storeBuyWings4Desc = (DbfLocValue)val;
							return;
						}
					}
					else if (num != 389673047U)
					{
						if (num != 397921193U)
						{
							return;
						}
						if (!(name == "STORE_PREORDER_WINGS_5_DESC"))
						{
							return;
						}
						this.m_storePreorderWings5Desc = (DbfLocValue)val;
						return;
					}
					else
					{
						if (!(name == "LEAVING_SOON_TEXT"))
						{
							return;
						}
						this.m_leavingSoonText = (DbfLocValue)val;
						return;
					}
				}
				else if (num <= 554784477U)
				{
					if (num != 499019123U)
					{
						if (num != 554784477U)
						{
							return;
						}
						if (!(name == "STORE_PREFAB"))
						{
							return;
						}
						this.m_storePrefab = (string)val;
						return;
					}
					else
					{
						if (!(name == "STORE_BUY_WINGS_1_HEADLINE"))
						{
							return;
						}
						this.m_storeBuyWings1Headline = (DbfLocValue)val;
						return;
					}
				}
				else if (num != 598991083U)
				{
					if (num != 634862270U)
					{
						if (num != 646730901U)
						{
							return;
						}
						if (!(name == "STORE_OWNED_HEADLINE"))
						{
							return;
						}
						this.m_storeOwnedHeadline = (DbfLocValue)val;
						return;
					}
					else
					{
						if (!(name == "STORE_PREORDER_RADIO_TEXT"))
						{
							return;
						}
						this.m_storePreorderRadioText = (DbfLocValue)val;
						return;
					}
				}
				else
				{
					if (!(name == "STORE_PREORDER_WINGS_3_DESC"))
					{
						return;
					}
					this.m_storePreorderWings3Desc = (DbfLocValue)val;
					return;
				}
			}
			else if (num <= 1387956774U)
			{
				if (num <= 1164573759U)
				{
					if (num != 864600850U)
					{
						if (num != 1164573759U)
						{
							return;
						}
						if (!(name == "LEAVING_SOON"))
						{
							return;
						}
						this.m_leavingSoon = (bool)val;
						return;
					}
					else
					{
						if (!(name == "COMING_SOON_TEXT"))
						{
							return;
						}
						this.m_comingSoonText = (DbfLocValue)val;
						return;
					}
				}
				else if (num != 1240037012U)
				{
					if (num != 1387956774U)
					{
						return;
					}
					if (!(name == "NAME"))
					{
						return;
					}
					this.m_name = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "STORE_BUY_WINGS_2_HEADLINE"))
					{
						return;
					}
					this.m_storeBuyWings2Headline = (DbfLocValue)val;
					return;
				}
			}
			else if (num <= 1458105184U)
			{
				if (num != 1424749021U)
				{
					if (num != 1458105184U)
					{
						return;
					}
					if (!(name == "ID"))
					{
						return;
					}
					base.SetID((int)val);
					return;
				}
				else
				{
					if (!(name == "GAME_MODE_ICON"))
					{
						return;
					}
					this.m_gameModeIcon = (string)val;
					return;
				}
			}
			else if (num != 1491069082U)
			{
				if (num != 1553026664U)
				{
					if (num != 1688767887U)
					{
						return;
					}
					if (!(name == "STORE_BUY_BUTTON_LABEL"))
					{
						return;
					}
					this.m_storeBuyButtonLabel = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "PRODUCT_STRING_KEY"))
					{
						return;
					}
					this.m_productStringKey = (string)val;
					return;
				}
			}
			else
			{
				if (!(name == "STORE_BUY_WINGS_5_DESC"))
				{
					return;
				}
				this.m_storeBuyWings5Desc = (DbfLocValue)val;
				return;
			}
		}
		else if (num <= 2609858387U)
		{
			if (num <= 2278395484U)
			{
				if (num <= 2191767247U)
				{
					if (num != 2143204526U)
					{
						if (num != 2191767247U)
						{
							return;
						}
						if (!(name == "STANDARD_EVENT"))
						{
							return;
						}
						this.m_standardEvent = (string)val;
						return;
					}
					else
					{
						if (!(name == "STORE_PREORDER_WINGS_2_DESC"))
						{
							return;
						}
						this.m_storePreorderWings2Desc = (DbfLocValue)val;
						return;
					}
				}
				else if (num != 2269500855U)
				{
					if (num != 2278395484U)
					{
						return;
					}
					if (!(name == "STORE_PREVIEW_REWARDS_TEXT"))
					{
						return;
					}
					this.m_storePreviewRewardsText = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_FALSE"))
					{
						return;
					}
					this.m_storeBuyRemainingWingsDescTimelockedFalse = (DbfLocValue)val;
					return;
				}
			}
			else if (num <= 2450510477U)
			{
				if (num != 2377316180U)
				{
					if (num != 2450510477U)
					{
						return;
					}
					if (!(name == "STORE_PREORDER_HEADLINE"))
					{
						return;
					}
					this.m_storePreorderHeadline = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "STORE_OWNED_DESC"))
					{
						return;
					}
					this.m_storeOwnedDesc = (DbfLocValue)val;
					return;
				}
			}
			else if (num != 2462803332U)
			{
				if (num != 2558737930U)
				{
					if (num != 2609858387U)
					{
						return;
					}
					if (!(name == "MAP_PAGE_HAS_BUTTONS_TO_CHAPTERS"))
					{
						return;
					}
					this.m_mapPageHasButtonsToChapters = (bool)val;
					return;
				}
				else
				{
					if (!(name == "STORE_BUY_WINGS_4_HEADLINE"))
					{
						return;
					}
					this.m_storeBuyWings4Headline = (DbfLocValue)val;
					return;
				}
			}
			else
			{
				if (!(name == "STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_TRUE"))
				{
					return;
				}
				this.m_storeBuyRemainingWingsDescTimelockedTrue = (DbfLocValue)val;
				return;
			}
		}
		else if (num <= 3687130011U)
		{
			if (num <= 3022554311U)
			{
				if (num != 2658528061U)
				{
					if (num != 3022554311U)
					{
						return;
					}
					if (!(name == "NOTE_DESC"))
					{
						return;
					}
					this.m_noteDesc = (string)val;
					return;
				}
				else
				{
					if (!(name == "STORE_PREORDER_WINGS_1_DESC"))
					{
						return;
					}
					this.m_storePreorderWings1Desc = (DbfLocValue)val;
					return;
				}
			}
			else if (num != 3034224585U)
			{
				if (num != 3425700768U)
				{
					if (num != 3687130011U)
					{
						return;
					}
					if (!(name == "COMING_SOON_EVENT"))
					{
						return;
					}
					this.m_comingSoonEvent = (string)val;
					return;
				}
				else
				{
					if (!(name == "STORE_BUY_WINGS_3_DESC"))
					{
						return;
					}
					this.m_storeBuyWings3Desc = (DbfLocValue)val;
					return;
				}
			}
			else
			{
				if (!(name == "STORE_BUY_WINGS_3_HEADLINE"))
				{
					return;
				}
				this.m_storeBuyWings3Headline = (DbfLocValue)val;
				return;
			}
		}
		else if (num <= 3977533932U)
		{
			if (num != 3958914583U)
			{
				if (num != 3977533932U)
				{
					return;
				}
				if (!(name == "STORE_PREORDER_WINGS_4_DESC"))
				{
					return;
				}
				this.m_storePreorderWings4Desc = (DbfLocValue)val;
				return;
			}
			else
			{
				if (!(name == "STORE_BUY_WINGS_5_HEADLINE"))
				{
					return;
				}
				this.m_storeBuyWings5Headline = (DbfLocValue)val;
				return;
			}
		}
		else if (num != 3991768310U)
		{
			if (num != 4214602626U)
			{
				if (num != 4277005678U)
				{
					return;
				}
				if (!(name == "STORE_BUY_WINGS_1_DESC"))
				{
					return;
				}
				this.m_storeBuyWings1Desc = (DbfLocValue)val;
				return;
			}
			else
			{
				if (!(name == "SORT_ORDER"))
				{
					return;
				}
				this.m_sortOrder = (int)val;
				return;
			}
		}
		else
		{
			if (!(name == "ADVENTURE_DEF_PREFAB"))
			{
				return;
			}
			this.m_adventureDefPrefab = (string)val;
			return;
		}
	}

	// Token: 0x0600172D RID: 5933 RVA: 0x0008081C File Offset: 0x0007EA1C
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1688767887U)
		{
			if (num <= 646730901U)
			{
				if (num <= 397921193U)
				{
					if (num <= 72904445U)
					{
						if (num != 72252647U)
						{
							if (num == 72904445U)
							{
								if (name == "STORE_BUY_WINGS_2_DESC")
								{
									return typeof(DbfLocValue);
								}
							}
						}
						else if (name == "STORE_BUY_WINGS_4_DESC")
						{
							return typeof(DbfLocValue);
						}
					}
					else if (num != 389673047U)
					{
						if (num == 397921193U)
						{
							if (name == "STORE_PREORDER_WINGS_5_DESC")
							{
								return typeof(DbfLocValue);
							}
						}
					}
					else if (name == "LEAVING_SOON_TEXT")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (num <= 554784477U)
				{
					if (num != 499019123U)
					{
						if (num == 554784477U)
						{
							if (name == "STORE_PREFAB")
							{
								return typeof(string);
							}
						}
					}
					else if (name == "STORE_BUY_WINGS_1_HEADLINE")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (num != 598991083U)
				{
					if (num != 634862270U)
					{
						if (num == 646730901U)
						{
							if (name == "STORE_OWNED_HEADLINE")
							{
								return typeof(DbfLocValue);
							}
						}
					}
					else if (name == "STORE_PREORDER_RADIO_TEXT")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (name == "STORE_PREORDER_WINGS_3_DESC")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num <= 1387956774U)
			{
				if (num <= 1164573759U)
				{
					if (num != 864600850U)
					{
						if (num == 1164573759U)
						{
							if (name == "LEAVING_SOON")
							{
								return typeof(bool);
							}
						}
					}
					else if (name == "COMING_SOON_TEXT")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (num != 1240037012U)
				{
					if (num == 1387956774U)
					{
						if (name == "NAME")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "STORE_BUY_WINGS_2_HEADLINE")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num <= 1458105184U)
			{
				if (num != 1424749021U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "GAME_MODE_ICON")
				{
					return typeof(string);
				}
			}
			else if (num != 1491069082U)
			{
				if (num != 1553026664U)
				{
					if (num == 1688767887U)
					{
						if (name == "STORE_BUY_BUTTON_LABEL")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "PRODUCT_STRING_KEY")
				{
					return typeof(string);
				}
			}
			else if (name == "STORE_BUY_WINGS_5_DESC")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num <= 2609858387U)
		{
			if (num <= 2278395484U)
			{
				if (num <= 2191767247U)
				{
					if (num != 2143204526U)
					{
						if (num == 2191767247U)
						{
							if (name == "STANDARD_EVENT")
							{
								return typeof(string);
							}
						}
					}
					else if (name == "STORE_PREORDER_WINGS_2_DESC")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (num != 2269500855U)
				{
					if (num == 2278395484U)
					{
						if (name == "STORE_PREVIEW_REWARDS_TEXT")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_FALSE")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num <= 2450510477U)
			{
				if (num != 2377316180U)
				{
					if (num == 2450510477U)
					{
						if (name == "STORE_PREORDER_HEADLINE")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "STORE_OWNED_DESC")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num != 2462803332U)
			{
				if (num != 2558737930U)
				{
					if (num == 2609858387U)
					{
						if (name == "MAP_PAGE_HAS_BUTTONS_TO_CHAPTERS")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "STORE_BUY_WINGS_4_HEADLINE")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (name == "STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_TRUE")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num <= 3687130011U)
		{
			if (num <= 3022554311U)
			{
				if (num != 2658528061U)
				{
					if (num == 3022554311U)
					{
						if (name == "NOTE_DESC")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "STORE_PREORDER_WINGS_1_DESC")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num != 3034224585U)
			{
				if (num != 3425700768U)
				{
					if (num == 3687130011U)
					{
						if (name == "COMING_SOON_EVENT")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "STORE_BUY_WINGS_3_DESC")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (name == "STORE_BUY_WINGS_3_HEADLINE")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num <= 3977533932U)
		{
			if (num != 3958914583U)
			{
				if (num == 3977533932U)
				{
					if (name == "STORE_PREORDER_WINGS_4_DESC")
					{
						return typeof(DbfLocValue);
					}
				}
			}
			else if (name == "STORE_BUY_WINGS_5_HEADLINE")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num != 3991768310U)
		{
			if (num != 4214602626U)
			{
				if (num == 4277005678U)
				{
					if (name == "STORE_BUY_WINGS_1_DESC")
					{
						return typeof(DbfLocValue);
					}
				}
			}
			else if (name == "SORT_ORDER")
			{
				return typeof(int);
			}
		}
		else if (name == "ADVENTURE_DEF_PREFAB")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x0600172E RID: 5934 RVA: 0x00080F36 File Offset: 0x0007F136
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureDbfRecords loadRecords = new LoadAdventureDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600172F RID: 5935 RVA: 0x00080F4C File Offset: 0x0007F14C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureDbfAsset adventureDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureDbfAsset)) as AdventureDbfAsset;
		if (adventureDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AdventureDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < adventureDbfAsset.Records.Count; i++)
		{
			adventureDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (adventureDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001730 RID: 5936 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001731 RID: 5937 RVA: 0x00080FCC File Offset: 0x0007F1CC
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_storeBuyButtonLabel.StripUnusedLocales();
		this.m_storeBuyWings1Headline.StripUnusedLocales();
		this.m_storeBuyWings2Headline.StripUnusedLocales();
		this.m_storeBuyWings3Headline.StripUnusedLocales();
		this.m_storeBuyWings4Headline.StripUnusedLocales();
		this.m_storeBuyWings5Headline.StripUnusedLocales();
		this.m_storeOwnedHeadline.StripUnusedLocales();
		this.m_storePreorderHeadline.StripUnusedLocales();
		this.m_storeBuyWings1Desc.StripUnusedLocales();
		this.m_storeBuyWings2Desc.StripUnusedLocales();
		this.m_storeBuyWings3Desc.StripUnusedLocales();
		this.m_storeBuyWings4Desc.StripUnusedLocales();
		this.m_storeBuyWings5Desc.StripUnusedLocales();
		this.m_storeBuyRemainingWingsDescTimelockedTrue.StripUnusedLocales();
		this.m_storeBuyRemainingWingsDescTimelockedFalse.StripUnusedLocales();
		this.m_storeOwnedDesc.StripUnusedLocales();
		this.m_storePreorderWings1Desc.StripUnusedLocales();
		this.m_storePreorderWings2Desc.StripUnusedLocales();
		this.m_storePreorderWings3Desc.StripUnusedLocales();
		this.m_storePreorderWings4Desc.StripUnusedLocales();
		this.m_storePreorderWings5Desc.StripUnusedLocales();
		this.m_storePreorderRadioText.StripUnusedLocales();
		this.m_storePreviewRewardsText.StripUnusedLocales();
		this.m_leavingSoonText.StripUnusedLocales();
		this.m_comingSoonText.StripUnusedLocales();
	}

	// Token: 0x04000EC0 RID: 3776
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04000EC1 RID: 3777
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x04000EC2 RID: 3778
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x04000EC3 RID: 3779
	[SerializeField]
	private DbfLocValue m_storeBuyButtonLabel;

	// Token: 0x04000EC4 RID: 3780
	[SerializeField]
	private DbfLocValue m_storeBuyWings1Headline;

	// Token: 0x04000EC5 RID: 3781
	[SerializeField]
	private DbfLocValue m_storeBuyWings2Headline;

	// Token: 0x04000EC6 RID: 3782
	[SerializeField]
	private DbfLocValue m_storeBuyWings3Headline;

	// Token: 0x04000EC7 RID: 3783
	[SerializeField]
	private DbfLocValue m_storeBuyWings4Headline;

	// Token: 0x04000EC8 RID: 3784
	[SerializeField]
	private DbfLocValue m_storeBuyWings5Headline;

	// Token: 0x04000EC9 RID: 3785
	[SerializeField]
	private DbfLocValue m_storeOwnedHeadline;

	// Token: 0x04000ECA RID: 3786
	[SerializeField]
	private DbfLocValue m_storePreorderHeadline;

	// Token: 0x04000ECB RID: 3787
	[SerializeField]
	private DbfLocValue m_storeBuyWings1Desc;

	// Token: 0x04000ECC RID: 3788
	[SerializeField]
	private DbfLocValue m_storeBuyWings2Desc;

	// Token: 0x04000ECD RID: 3789
	[SerializeField]
	private DbfLocValue m_storeBuyWings3Desc;

	// Token: 0x04000ECE RID: 3790
	[SerializeField]
	private DbfLocValue m_storeBuyWings4Desc;

	// Token: 0x04000ECF RID: 3791
	[SerializeField]
	private DbfLocValue m_storeBuyWings5Desc;

	// Token: 0x04000ED0 RID: 3792
	[SerializeField]
	private DbfLocValue m_storeBuyRemainingWingsDescTimelockedTrue;

	// Token: 0x04000ED1 RID: 3793
	[SerializeField]
	private DbfLocValue m_storeBuyRemainingWingsDescTimelockedFalse;

	// Token: 0x04000ED2 RID: 3794
	[SerializeField]
	private DbfLocValue m_storeOwnedDesc;

	// Token: 0x04000ED3 RID: 3795
	[SerializeField]
	private DbfLocValue m_storePreorderWings1Desc;

	// Token: 0x04000ED4 RID: 3796
	[SerializeField]
	private DbfLocValue m_storePreorderWings2Desc;

	// Token: 0x04000ED5 RID: 3797
	[SerializeField]
	private DbfLocValue m_storePreorderWings3Desc;

	// Token: 0x04000ED6 RID: 3798
	[SerializeField]
	private DbfLocValue m_storePreorderWings4Desc;

	// Token: 0x04000ED7 RID: 3799
	[SerializeField]
	private DbfLocValue m_storePreorderWings5Desc;

	// Token: 0x04000ED8 RID: 3800
	[SerializeField]
	private DbfLocValue m_storePreorderRadioText;

	// Token: 0x04000ED9 RID: 3801
	[SerializeField]
	private DbfLocValue m_storePreviewRewardsText;

	// Token: 0x04000EDA RID: 3802
	[SerializeField]
	private string m_adventureDefPrefab;

	// Token: 0x04000EDB RID: 3803
	[SerializeField]
	private string m_storePrefab;

	// Token: 0x04000EDC RID: 3804
	[SerializeField]
	private bool m_leavingSoon;

	// Token: 0x04000EDD RID: 3805
	[SerializeField]
	private DbfLocValue m_leavingSoonText;

	// Token: 0x04000EDE RID: 3806
	[SerializeField]
	private string m_gameModeIcon;

	// Token: 0x04000EDF RID: 3807
	[SerializeField]
	private string m_productStringKey;

	// Token: 0x04000EE0 RID: 3808
	[SerializeField]
	private string m_standardEvent = "always";

	// Token: 0x04000EE1 RID: 3809
	[SerializeField]
	private string m_comingSoonEvent = "never";

	// Token: 0x04000EE2 RID: 3810
	[SerializeField]
	private DbfLocValue m_comingSoonText;

	// Token: 0x04000EE3 RID: 3811
	[SerializeField]
	private bool m_mapPageHasButtonsToChapters;
}
