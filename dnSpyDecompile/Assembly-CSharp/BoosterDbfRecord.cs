using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200018C RID: 396
[Serializable]
public class BoosterDbfRecord : DbfRecord
{
	// Token: 0x1700020B RID: 523
	// (get) Token: 0x06001842 RID: 6210 RVA: 0x000846F2 File Offset: 0x000828F2
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x06001843 RID: 6211 RVA: 0x000846FA File Offset: 0x000828FA
	[DbfField("LATEST_EXPANSION_ORDER")]
	public int LatestExpansionOrder
	{
		get
		{
			return this.m_latestExpansionOrder;
		}
	}

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x06001844 RID: 6212 RVA: 0x00084702 File Offset: 0x00082902
	[DbfField("LIST_DISPLAY_ORDER")]
	public int ListDisplayOrder
	{
		get
		{
			return this.m_listDisplayOrder;
		}
	}

	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06001845 RID: 6213 RVA: 0x0008470A File Offset: 0x0008290A
	[DbfField("LIST_DISPLAY_ORDER_CATEGORY")]
	public int ListDisplayOrderCategory
	{
		get
		{
			return this.m_listDisplayOrderCategory;
		}
	}

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x06001846 RID: 6214 RVA: 0x00084712 File Offset: 0x00082912
	[DbfField("OPEN_PACK_EVENT")]
	public string OpenPackEvent
	{
		get
		{
			return this.m_openPackEvent;
		}
	}

	// Token: 0x17000210 RID: 528
	// (get) Token: 0x06001847 RID: 6215 RVA: 0x0008471A File Offset: 0x0008291A
	[Obsolete("renamed to PRERELEASE_OPEN_PACK_EVENT")]
	[DbfField("DEPRECATED_OPEN_PACK_EVENT")]
	public string DeprecatedOpenPackEvent
	{
		get
		{
			return this.m_deprecatedOpenPackEvent;
		}
	}

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x06001848 RID: 6216 RVA: 0x00084722 File Offset: 0x00082922
	[DbfField("PRERELEASE_OPEN_PACK_EVENT")]
	public string PrereleaseOpenPackEvent
	{
		get
		{
			return this.m_prereleaseOpenPackEvent;
		}
	}

	// Token: 0x17000212 RID: 530
	// (get) Token: 0x06001849 RID: 6217 RVA: 0x0008472A File Offset: 0x0008292A
	[DbfField("BUY_WITH_GOLD_EVENT")]
	public string BuyWithGoldEvent
	{
		get
		{
			return this.m_buyWithGoldEvent;
		}
	}

	// Token: 0x17000213 RID: 531
	// (get) Token: 0x0600184A RID: 6218 RVA: 0x00084732 File Offset: 0x00082932
	[DbfField("REWARDABLE_EVENT")]
	public string RewardableEvent
	{
		get
		{
			return this.m_rewardableEvent;
		}
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x0600184B RID: 6219 RVA: 0x0008473A File Offset: 0x0008293A
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x0600184C RID: 6220 RVA: 0x00084742 File Offset: 0x00082942
	[DbfField("PACK_OPENING_PREFAB")]
	public string PackOpeningPrefab
	{
		get
		{
			return this.m_packOpeningPrefab;
		}
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x0600184D RID: 6221 RVA: 0x0008474A File Offset: 0x0008294A
	[DbfField("PACK_OPENING_FX_PREFAB")]
	public string PackOpeningFxPrefab
	{
		get
		{
			return this.m_packOpeningFxPrefab;
		}
	}

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x0600184E RID: 6222 RVA: 0x00084752 File Offset: 0x00082952
	[DbfField("STORE_PREFAB")]
	public string StorePrefab
	{
		get
		{
			return this.m_storePrefab;
		}
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x0600184F RID: 6223 RVA: 0x0008475A File Offset: 0x0008295A
	[DbfField("ARENA_PREFAB")]
	public string ArenaPrefab
	{
		get
		{
			return this.m_arenaPrefab;
		}
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x06001850 RID: 6224 RVA: 0x00084762 File Offset: 0x00082962
	[DbfField("LEAVING_SOON")]
	public bool LeavingSoon
	{
		get
		{
			return this.m_leavingSoon;
		}
	}

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x06001851 RID: 6225 RVA: 0x0008476A File Offset: 0x0008296A
	[DbfField("LEAVING_SOON_TEXT")]
	public DbfLocValue LeavingSoonText
	{
		get
		{
			return this.m_leavingSoonText;
		}
	}

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x06001852 RID: 6226 RVA: 0x00084772 File Offset: 0x00082972
	[DbfField("STANDARD_EVENT")]
	public string StandardEvent
	{
		get
		{
			return this.m_standardEvent;
		}
	}

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x06001853 RID: 6227 RVA: 0x0008477A File Offset: 0x0008297A
	[Obsolete("DEPRECATED. Hasn't been used in a long time.")]
	[DbfField("SHOW_IN_STORE")]
	public bool ShowInStore
	{
		get
		{
			return this.m_showInStore;
		}
	}

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x06001854 RID: 6228 RVA: 0x00084782 File Offset: 0x00082982
	[DbfField("RANKED_REWARD_INITIAL_SEASON")]
	public int RankedRewardInitialSeason
	{
		get
		{
			return this.m_rankedRewardInitialSeason;
		}
	}

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x06001855 RID: 6229 RVA: 0x0008478A File Offset: 0x0008298A
	[DbfField("QUEST_ICON_PATH")]
	public string QuestIconPath
	{
		get
		{
			return this.m_questIconPath;
		}
	}

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x06001856 RID: 6230 RVA: 0x00084792 File Offset: 0x00082992
	[DbfField("QUEST_ICON_OFFSET_X")]
	public double QuestIconOffsetX
	{
		get
		{
			return this.m_questIconOffsetX;
		}
	}

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x06001857 RID: 6231 RVA: 0x0008479A File Offset: 0x0008299A
	[DbfField("QUEST_ICON_OFFSET_Y")]
	public double QuestIconOffsetY
	{
		get
		{
			return this.m_questIconOffsetY;
		}
	}

	// Token: 0x06001858 RID: 6232 RVA: 0x000847A2 File Offset: 0x000829A2
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001859 RID: 6233 RVA: 0x000847AB File Offset: 0x000829AB
	public void SetLatestExpansionOrder(int v)
	{
		this.m_latestExpansionOrder = v;
	}

	// Token: 0x0600185A RID: 6234 RVA: 0x000847B4 File Offset: 0x000829B4
	public void SetListDisplayOrder(int v)
	{
		this.m_listDisplayOrder = v;
	}

	// Token: 0x0600185B RID: 6235 RVA: 0x000847BD File Offset: 0x000829BD
	public void SetListDisplayOrderCategory(int v)
	{
		this.m_listDisplayOrderCategory = v;
	}

	// Token: 0x0600185C RID: 6236 RVA: 0x000847C6 File Offset: 0x000829C6
	public void SetOpenPackEvent(string v)
	{
		this.m_openPackEvent = v;
	}

	// Token: 0x0600185D RID: 6237 RVA: 0x000847CF File Offset: 0x000829CF
	[Obsolete("renamed to PRERELEASE_OPEN_PACK_EVENT")]
	public void SetDeprecatedOpenPackEvent(string v)
	{
		this.m_deprecatedOpenPackEvent = v;
	}

	// Token: 0x0600185E RID: 6238 RVA: 0x000847D8 File Offset: 0x000829D8
	public void SetPrereleaseOpenPackEvent(string v)
	{
		this.m_prereleaseOpenPackEvent = v;
	}

	// Token: 0x0600185F RID: 6239 RVA: 0x000847E1 File Offset: 0x000829E1
	public void SetBuyWithGoldEvent(string v)
	{
		this.m_buyWithGoldEvent = v;
	}

	// Token: 0x06001860 RID: 6240 RVA: 0x000847EA File Offset: 0x000829EA
	public void SetRewardableEvent(string v)
	{
		this.m_rewardableEvent = v;
	}

	// Token: 0x06001861 RID: 6241 RVA: 0x000847F3 File Offset: 0x000829F3
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x06001862 RID: 6242 RVA: 0x0008480D File Offset: 0x00082A0D
	public void SetPackOpeningPrefab(string v)
	{
		this.m_packOpeningPrefab = v;
	}

	// Token: 0x06001863 RID: 6243 RVA: 0x00084816 File Offset: 0x00082A16
	public void SetPackOpeningFxPrefab(string v)
	{
		this.m_packOpeningFxPrefab = v;
	}

	// Token: 0x06001864 RID: 6244 RVA: 0x0008481F File Offset: 0x00082A1F
	public void SetStorePrefab(string v)
	{
		this.m_storePrefab = v;
	}

	// Token: 0x06001865 RID: 6245 RVA: 0x00084828 File Offset: 0x00082A28
	public void SetArenaPrefab(string v)
	{
		this.m_arenaPrefab = v;
	}

	// Token: 0x06001866 RID: 6246 RVA: 0x00084831 File Offset: 0x00082A31
	public void SetLeavingSoon(bool v)
	{
		this.m_leavingSoon = v;
	}

	// Token: 0x06001867 RID: 6247 RVA: 0x0008483A File Offset: 0x00082A3A
	public void SetLeavingSoonText(DbfLocValue v)
	{
		this.m_leavingSoonText = v;
		v.SetDebugInfo(base.ID, "LEAVING_SOON_TEXT");
	}

	// Token: 0x06001868 RID: 6248 RVA: 0x00084854 File Offset: 0x00082A54
	public void SetStandardEvent(string v)
	{
		this.m_standardEvent = v;
	}

	// Token: 0x06001869 RID: 6249 RVA: 0x0008485D File Offset: 0x00082A5D
	[Obsolete("DEPRECATED. Hasn't been used in a long time.")]
	public void SetShowInStore(bool v)
	{
		this.m_showInStore = v;
	}

	// Token: 0x0600186A RID: 6250 RVA: 0x00084866 File Offset: 0x00082A66
	public void SetRankedRewardInitialSeason(int v)
	{
		this.m_rankedRewardInitialSeason = v;
	}

	// Token: 0x0600186B RID: 6251 RVA: 0x0008486F File Offset: 0x00082A6F
	public void SetQuestIconPath(string v)
	{
		this.m_questIconPath = v;
	}

	// Token: 0x0600186C RID: 6252 RVA: 0x00084878 File Offset: 0x00082A78
	public void SetQuestIconOffsetX(double v)
	{
		this.m_questIconOffsetX = v;
	}

	// Token: 0x0600186D RID: 6253 RVA: 0x00084881 File Offset: 0x00082A81
	public void SetQuestIconOffsetY(double v)
	{
		this.m_questIconOffsetY = v;
	}

	// Token: 0x0600186E RID: 6254 RVA: 0x0008488C File Offset: 0x00082A8C
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num <= 756535192U)
			{
				if (num <= 207194469U)
				{
					if (num != 5044059U)
					{
						if (num == 207194469U)
						{
							if (name == "RANKED_REWARD_INITIAL_SEASON")
							{
								return this.m_rankedRewardInitialSeason;
							}
						}
					}
					else if (name == "QUEST_ICON_OFFSET_Y")
					{
						return this.m_questIconOffsetY;
					}
				}
				else if (num != 389673047U)
				{
					if (num != 554784477U)
					{
						if (num == 756535192U)
						{
							if (name == "LIST_DISPLAY_ORDER_CATEGORY")
							{
								return this.m_listDisplayOrderCategory;
							}
						}
					}
					else if (name == "STORE_PREFAB")
					{
						return this.m_storePrefab;
					}
				}
				else if (name == "LEAVING_SOON_TEXT")
				{
					return this.m_leavingSoonText;
				}
			}
			else if (num <= 1164573759U)
			{
				if (num != 778560520U)
				{
					if (num != 918639297U)
					{
						if (num == 1164573759U)
						{
							if (name == "LEAVING_SOON")
							{
								return this.m_leavingSoon;
							}
						}
					}
					else if (name == "REWARDABLE_EVENT")
					{
						return this.m_rewardableEvent;
					}
				}
				else if (name == "BUY_WITH_GOLD_EVENT")
				{
					return this.m_buyWithGoldEvent;
				}
			}
			else if (num != 1387956774U)
			{
				if (num != 1425399448U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "SHOW_IN_STORE")
				{
					return this.m_showInStore;
				}
			}
			else if (name == "NAME")
			{
				return this.m_name;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num <= 2191767247U)
			{
				if (num != 1461476575U)
				{
					if (num != 1998723901U)
					{
						if (num == 2191767247U)
						{
							if (name == "STANDARD_EVENT")
							{
								return this.m_standardEvent;
							}
						}
					}
					else if (name == "PACK_OPENING_FX_PREFAB")
					{
						return this.m_packOpeningFxPrefab;
					}
				}
				else if (name == "LIST_DISPLAY_ORDER")
				{
					return this.m_listDisplayOrder;
				}
			}
			else if (num != 2343696819U)
			{
				if (num != 2830500322U)
				{
					if (num == 3022554311U)
					{
						if (name == "NOTE_DESC")
						{
							return this.m_noteDesc;
						}
					}
				}
				else if (name == "PACK_OPENING_PREFAB")
				{
					return this.m_packOpeningPrefab;
				}
			}
			else if (name == "ARENA_PREFAB")
			{
				return this.m_arenaPrefab;
			}
		}
		else if (num <= 3577915015U)
		{
			if (num != 3328465851U)
			{
				if (num != 3430609171U)
				{
					if (num == 3577915015U)
					{
						if (name == "PRERELEASE_OPEN_PACK_EVENT")
						{
							return this.m_prereleaseOpenPackEvent;
						}
					}
				}
				else if (name == "QUEST_ICON_PATH")
				{
					return this.m_questIconPath;
				}
			}
			else if (name == "LATEST_EXPANSION_ORDER")
			{
				return this.m_latestExpansionOrder;
			}
		}
		else if (num != 4145311956U)
		{
			if (num != 4226534232U)
			{
				if (num == 4283233736U)
				{
					if (name == "QUEST_ICON_OFFSET_X")
					{
						return this.m_questIconOffsetX;
					}
				}
			}
			else if (name == "DEPRECATED_OPEN_PACK_EVENT")
			{
				return this.m_deprecatedOpenPackEvent;
			}
		}
		else if (name == "OPEN_PACK_EVENT")
		{
			return this.m_openPackEvent;
		}
		return null;
	}

	// Token: 0x0600186F RID: 6255 RVA: 0x00084CAC File Offset: 0x00082EAC
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num <= 756535192U)
			{
				if (num <= 207194469U)
				{
					if (num != 5044059U)
					{
						if (num != 207194469U)
						{
							return;
						}
						if (!(name == "RANKED_REWARD_INITIAL_SEASON"))
						{
							return;
						}
						this.m_rankedRewardInitialSeason = (int)val;
						return;
					}
					else
					{
						if (!(name == "QUEST_ICON_OFFSET_Y"))
						{
							return;
						}
						this.m_questIconOffsetY = (double)val;
						return;
					}
				}
				else if (num != 389673047U)
				{
					if (num != 554784477U)
					{
						if (num != 756535192U)
						{
							return;
						}
						if (!(name == "LIST_DISPLAY_ORDER_CATEGORY"))
						{
							return;
						}
						this.m_listDisplayOrderCategory = (int)val;
						return;
					}
					else
					{
						if (!(name == "STORE_PREFAB"))
						{
							return;
						}
						this.m_storePrefab = (string)val;
						return;
					}
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
			else if (num <= 1164573759U)
			{
				if (num != 778560520U)
				{
					if (num != 918639297U)
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
						if (!(name == "REWARDABLE_EVENT"))
						{
							return;
						}
						this.m_rewardableEvent = (string)val;
						return;
					}
				}
				else
				{
					if (!(name == "BUY_WITH_GOLD_EVENT"))
					{
						return;
					}
					this.m_buyWithGoldEvent = (string)val;
					return;
				}
			}
			else if (num != 1387956774U)
			{
				if (num != 1425399448U)
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
					if (!(name == "SHOW_IN_STORE"))
					{
						return;
					}
					this.m_showInStore = (bool)val;
					return;
				}
			}
			else
			{
				if (!(name == "NAME"))
				{
					return;
				}
				this.m_name = (DbfLocValue)val;
				return;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num <= 2191767247U)
			{
				if (num != 1461476575U)
				{
					if (num != 1998723901U)
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
						if (!(name == "PACK_OPENING_FX_PREFAB"))
						{
							return;
						}
						this.m_packOpeningFxPrefab = (string)val;
						return;
					}
				}
				else
				{
					if (!(name == "LIST_DISPLAY_ORDER"))
					{
						return;
					}
					this.m_listDisplayOrder = (int)val;
					return;
				}
			}
			else if (num != 2343696819U)
			{
				if (num != 2830500322U)
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
					if (!(name == "PACK_OPENING_PREFAB"))
					{
						return;
					}
					this.m_packOpeningPrefab = (string)val;
					return;
				}
			}
			else
			{
				if (!(name == "ARENA_PREFAB"))
				{
					return;
				}
				this.m_arenaPrefab = (string)val;
				return;
			}
		}
		else if (num <= 3577915015U)
		{
			if (num != 3328465851U)
			{
				if (num != 3430609171U)
				{
					if (num != 3577915015U)
					{
						return;
					}
					if (!(name == "PRERELEASE_OPEN_PACK_EVENT"))
					{
						return;
					}
					this.m_prereleaseOpenPackEvent = (string)val;
					return;
				}
				else
				{
					if (!(name == "QUEST_ICON_PATH"))
					{
						return;
					}
					this.m_questIconPath = (string)val;
					return;
				}
			}
			else
			{
				if (!(name == "LATEST_EXPANSION_ORDER"))
				{
					return;
				}
				this.m_latestExpansionOrder = (int)val;
				return;
			}
		}
		else if (num != 4145311956U)
		{
			if (num != 4226534232U)
			{
				if (num != 4283233736U)
				{
					return;
				}
				if (!(name == "QUEST_ICON_OFFSET_X"))
				{
					return;
				}
				this.m_questIconOffsetX = (double)val;
				return;
			}
			else
			{
				if (!(name == "DEPRECATED_OPEN_PACK_EVENT"))
				{
					return;
				}
				this.m_deprecatedOpenPackEvent = (string)val;
				return;
			}
		}
		else
		{
			if (!(name == "OPEN_PACK_EVENT"))
			{
				return;
			}
			this.m_openPackEvent = (string)val;
			return;
		}
	}

	// Token: 0x06001870 RID: 6256 RVA: 0x000850A8 File Offset: 0x000832A8
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num <= 756535192U)
			{
				if (num <= 207194469U)
				{
					if (num != 5044059U)
					{
						if (num == 207194469U)
						{
							if (name == "RANKED_REWARD_INITIAL_SEASON")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "QUEST_ICON_OFFSET_Y")
					{
						return typeof(double);
					}
				}
				else if (num != 389673047U)
				{
					if (num != 554784477U)
					{
						if (num == 756535192U)
						{
							if (name == "LIST_DISPLAY_ORDER_CATEGORY")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "STORE_PREFAB")
					{
						return typeof(string);
					}
				}
				else if (name == "LEAVING_SOON_TEXT")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num <= 1164573759U)
			{
				if (num != 778560520U)
				{
					if (num != 918639297U)
					{
						if (num == 1164573759U)
						{
							if (name == "LEAVING_SOON")
							{
								return typeof(bool);
							}
						}
					}
					else if (name == "REWARDABLE_EVENT")
					{
						return typeof(string);
					}
				}
				else if (name == "BUY_WITH_GOLD_EVENT")
				{
					return typeof(string);
				}
			}
			else if (num != 1387956774U)
			{
				if (num != 1425399448U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "SHOW_IN_STORE")
				{
					return typeof(bool);
				}
			}
			else if (name == "NAME")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num <= 3022554311U)
		{
			if (num <= 2191767247U)
			{
				if (num != 1461476575U)
				{
					if (num != 1998723901U)
					{
						if (num == 2191767247U)
						{
							if (name == "STANDARD_EVENT")
							{
								return typeof(string);
							}
						}
					}
					else if (name == "PACK_OPENING_FX_PREFAB")
					{
						return typeof(string);
					}
				}
				else if (name == "LIST_DISPLAY_ORDER")
				{
					return typeof(int);
				}
			}
			else if (num != 2343696819U)
			{
				if (num != 2830500322U)
				{
					if (num == 3022554311U)
					{
						if (name == "NOTE_DESC")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "PACK_OPENING_PREFAB")
				{
					return typeof(string);
				}
			}
			else if (name == "ARENA_PREFAB")
			{
				return typeof(string);
			}
		}
		else if (num <= 3577915015U)
		{
			if (num != 3328465851U)
			{
				if (num != 3430609171U)
				{
					if (num == 3577915015U)
					{
						if (name == "PRERELEASE_OPEN_PACK_EVENT")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "QUEST_ICON_PATH")
				{
					return typeof(string);
				}
			}
			else if (name == "LATEST_EXPANSION_ORDER")
			{
				return typeof(int);
			}
		}
		else if (num != 4145311956U)
		{
			if (num != 4226534232U)
			{
				if (num == 4283233736U)
				{
					if (name == "QUEST_ICON_OFFSET_X")
					{
						return typeof(double);
					}
				}
			}
			else if (name == "DEPRECATED_OPEN_PACK_EVENT")
			{
				return typeof(string);
			}
		}
		else if (name == "OPEN_PACK_EVENT")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x06001871 RID: 6257 RVA: 0x000854F7 File Offset: 0x000836F7
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadBoosterDbfRecords loadRecords = new LoadBoosterDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001872 RID: 6258 RVA: 0x00085510 File Offset: 0x00083710
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		BoosterDbfAsset boosterDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(BoosterDbfAsset)) as BoosterDbfAsset;
		if (boosterDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("BoosterDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < boosterDbfAsset.Records.Count; i++)
		{
			boosterDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (boosterDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001873 RID: 6259 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001874 RID: 6260 RVA: 0x0008558F File Offset: 0x0008378F
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_leavingSoonText.StripUnusedLocales();
	}

	// Token: 0x04000F36 RID: 3894
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04000F37 RID: 3895
	[SerializeField]
	private int m_latestExpansionOrder;

	// Token: 0x04000F38 RID: 3896
	[SerializeField]
	private int m_listDisplayOrder;

	// Token: 0x04000F39 RID: 3897
	[SerializeField]
	private int m_listDisplayOrderCategory;

	// Token: 0x04000F3A RID: 3898
	[SerializeField]
	private string m_openPackEvent = "none";

	// Token: 0x04000F3B RID: 3899
	[SerializeField]
	private string m_deprecatedOpenPackEvent = "never";

	// Token: 0x04000F3C RID: 3900
	[SerializeField]
	private string m_prereleaseOpenPackEvent = "never";

	// Token: 0x04000F3D RID: 3901
	[SerializeField]
	private string m_buyWithGoldEvent = "never";

	// Token: 0x04000F3E RID: 3902
	[SerializeField]
	private string m_rewardableEvent = "none";

	// Token: 0x04000F3F RID: 3903
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x04000F40 RID: 3904
	[SerializeField]
	private string m_packOpeningPrefab;

	// Token: 0x04000F41 RID: 3905
	[SerializeField]
	private string m_packOpeningFxPrefab;

	// Token: 0x04000F42 RID: 3906
	[SerializeField]
	private string m_storePrefab;

	// Token: 0x04000F43 RID: 3907
	[SerializeField]
	private string m_arenaPrefab;

	// Token: 0x04000F44 RID: 3908
	[SerializeField]
	private bool m_leavingSoon;

	// Token: 0x04000F45 RID: 3909
	[SerializeField]
	private DbfLocValue m_leavingSoonText;

	// Token: 0x04000F46 RID: 3910
	[SerializeField]
	private string m_standardEvent = "always";

	// Token: 0x04000F47 RID: 3911
	[SerializeField]
	private bool m_showInStore;

	// Token: 0x04000F48 RID: 3912
	[SerializeField]
	private int m_rankedRewardInitialSeason;

	// Token: 0x04000F49 RID: 3913
	[SerializeField]
	private string m_questIconPath;

	// Token: 0x04000F4A RID: 3914
	[SerializeField]
	private double m_questIconOffsetX;

	// Token: 0x04000F4B RID: 3915
	[SerializeField]
	private double m_questIconOffsetY;
}
