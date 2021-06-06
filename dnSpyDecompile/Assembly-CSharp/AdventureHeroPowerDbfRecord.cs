using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000174 RID: 372
[Serializable]
public class AdventureHeroPowerDbfRecord : DbfRecord
{
	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x0600177B RID: 6011 RVA: 0x00081FB2 File Offset: 0x000801B2
	[DbfField("ADVENTURE_ID")]
	public int AdventureId
	{
		get
		{
			return this.m_adventureId;
		}
	}

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x0600177C RID: 6012 RVA: 0x00081FBA File Offset: 0x000801BA
	[DbfField("CLASS_ID")]
	public int ClassId
	{
		get
		{
			return this.m_classId;
		}
	}

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x0600177D RID: 6013 RVA: 0x00081FC2 File Offset: 0x000801C2
	public ClassDbfRecord ClassRecord
	{
		get
		{
			return GameDbf.Class.GetRecord(this.m_classId);
		}
	}

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x0600177E RID: 6014 RVA: 0x00081FD4 File Offset: 0x000801D4
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x0600177F RID: 6015 RVA: 0x00081FDC File Offset: 0x000801DC
	public CardDbfRecord CardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_cardId);
		}
	}

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x06001780 RID: 6016 RVA: 0x00081FEE File Offset: 0x000801EE
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x06001781 RID: 6017 RVA: 0x00081FF6 File Offset: 0x000801F6
	[DbfField("IS_DEFAULT")]
	public bool IsDefault
	{
		get
		{
			return this.m_isDefault;
		}
	}

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x06001782 RID: 6018 RVA: 0x00081FFE File Offset: 0x000801FE
	[DbfField("UNLOCK_CRITERIA_TEXT")]
	public DbfLocValue UnlockCriteriaText
	{
		get
		{
			return this.m_unlockCriteriaText;
		}
	}

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x06001783 RID: 6019 RVA: 0x00082006 File Offset: 0x00080206
	[DbfField("UNLOCKED_DESCRIPTION_TEXT")]
	public DbfLocValue UnlockedDescriptionText
	{
		get
		{
			return this.m_unlockedDescriptionText;
		}
	}

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x06001784 RID: 6020 RVA: 0x0008200E File Offset: 0x0008020E
	[DbfField("UNLOCK_GAME_SAVE_SUBKEY")]
	public int UnlockGameSaveSubkey
	{
		get
		{
			return this.m_unlockGameSaveSubkeyId;
		}
	}

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x06001785 RID: 6021 RVA: 0x00082016 File Offset: 0x00080216
	public GameSaveSubkeyDbfRecord UnlockGameSaveSubkeyRecord
	{
		get
		{
			return GameDbf.GameSaveSubkey.GetRecord(this.m_unlockGameSaveSubkeyId);
		}
	}

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x06001786 RID: 6022 RVA: 0x00082028 File Offset: 0x00080228
	[DbfField("UNLOCK_VALUE")]
	public int UnlockValue
	{
		get
		{
			return this.m_unlockValue;
		}
	}

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x06001787 RID: 6023 RVA: 0x00082030 File Offset: 0x00080230
	[DbfField("UNLOCK_ACHIEVEMENT")]
	public int UnlockAchievement
	{
		get
		{
			return this.m_unlockAchievementId;
		}
	}

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x06001788 RID: 6024 RVA: 0x00082038 File Offset: 0x00080238
	public AchievementDbfRecord UnlockAchievementRecord
	{
		get
		{
			return GameDbf.Achievement.GetRecord(this.m_unlockAchievementId);
		}
	}

	// Token: 0x06001789 RID: 6025 RVA: 0x0008204A File Offset: 0x0008024A
	public void SetAdventureId(int v)
	{
		this.m_adventureId = v;
	}

	// Token: 0x0600178A RID: 6026 RVA: 0x00082053 File Offset: 0x00080253
	public void SetClassId(int v)
	{
		this.m_classId = v;
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x0008205C File Offset: 0x0008025C
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x0600178C RID: 6028 RVA: 0x00082065 File Offset: 0x00080265
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x0600178D RID: 6029 RVA: 0x0008206E File Offset: 0x0008026E
	public void SetIsDefault(bool v)
	{
		this.m_isDefault = v;
	}

	// Token: 0x0600178E RID: 6030 RVA: 0x00082077 File Offset: 0x00080277
	public void SetUnlockCriteriaText(DbfLocValue v)
	{
		this.m_unlockCriteriaText = v;
		v.SetDebugInfo(base.ID, "UNLOCK_CRITERIA_TEXT");
	}

	// Token: 0x0600178F RID: 6031 RVA: 0x00082091 File Offset: 0x00080291
	public void SetUnlockedDescriptionText(DbfLocValue v)
	{
		this.m_unlockedDescriptionText = v;
		v.SetDebugInfo(base.ID, "UNLOCKED_DESCRIPTION_TEXT");
	}

	// Token: 0x06001790 RID: 6032 RVA: 0x000820AB File Offset: 0x000802AB
	public void SetUnlockGameSaveSubkey(int v)
	{
		this.m_unlockGameSaveSubkeyId = v;
	}

	// Token: 0x06001791 RID: 6033 RVA: 0x000820B4 File Offset: 0x000802B4
	public void SetUnlockValue(int v)
	{
		this.m_unlockValue = v;
	}

	// Token: 0x06001792 RID: 6034 RVA: 0x000820BD File Offset: 0x000802BD
	public void SetUnlockAchievement(int v)
	{
		this.m_unlockAchievementId = v;
	}

	// Token: 0x06001793 RID: 6035 RVA: 0x000820C8 File Offset: 0x000802C8
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1674937439U)
		{
			if (num <= 451390141U)
			{
				if (num != 190718801U)
				{
					if (num == 451390141U)
					{
						if (name == "CARD_ID")
						{
							return this.m_cardId;
						}
					}
				}
				else if (name == "ADVENTURE_ID")
				{
					return this.m_adventureId;
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1608019912U)
				{
					if (num == 1674937439U)
					{
						if (name == "UNLOCK_VALUE")
						{
							return this.m_unlockValue;
						}
					}
				}
				else if (name == "UNLOCK_GAME_SAVE_SUBKEY")
				{
					return this.m_unlockGameSaveSubkeyId;
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (num <= 3710150967U)
		{
			if (num != 2401654691U)
			{
				if (num != 3034864917U)
				{
					if (num == 3710150967U)
					{
						if (name == "UNLOCK_CRITERIA_TEXT")
						{
							return this.m_unlockCriteriaText;
						}
					}
				}
				else if (name == "UNLOCK_ACHIEVEMENT")
				{
					return this.m_unlockAchievementId;
				}
			}
			else if (name == "IS_DEFAULT")
			{
				return this.m_isDefault;
			}
		}
		else if (num != 4070522309U)
		{
			if (num != 4214602626U)
			{
				if (num == 4257872637U)
				{
					if (name == "CLASS_ID")
					{
						return this.m_classId;
					}
				}
			}
			else if (name == "SORT_ORDER")
			{
				return this.m_sortOrder;
			}
		}
		else if (name == "UNLOCKED_DESCRIPTION_TEXT")
		{
			return this.m_unlockedDescriptionText;
		}
		return null;
	}

	// Token: 0x06001794 RID: 6036 RVA: 0x000822D0 File Offset: 0x000804D0
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1674937439U)
		{
			if (num <= 451390141U)
			{
				if (num != 190718801U)
				{
					if (num != 451390141U)
					{
						return;
					}
					if (!(name == "CARD_ID"))
					{
						return;
					}
					this.m_cardId = (int)val;
					return;
				}
				else
				{
					if (!(name == "ADVENTURE_ID"))
					{
						return;
					}
					this.m_adventureId = (int)val;
					return;
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1608019912U)
				{
					if (num != 1674937439U)
					{
						return;
					}
					if (!(name == "UNLOCK_VALUE"))
					{
						return;
					}
					this.m_unlockValue = (int)val;
					return;
				}
				else
				{
					if (!(name == "UNLOCK_GAME_SAVE_SUBKEY"))
					{
						return;
					}
					this.m_unlockGameSaveSubkeyId = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "ID"))
				{
					return;
				}
				base.SetID((int)val);
				return;
			}
		}
		else if (num <= 3710150967U)
		{
			if (num != 2401654691U)
			{
				if (num != 3034864917U)
				{
					if (num != 3710150967U)
					{
						return;
					}
					if (!(name == "UNLOCK_CRITERIA_TEXT"))
					{
						return;
					}
					this.m_unlockCriteriaText = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "UNLOCK_ACHIEVEMENT"))
					{
						return;
					}
					this.m_unlockAchievementId = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "IS_DEFAULT"))
				{
					return;
				}
				this.m_isDefault = (bool)val;
				return;
			}
		}
		else if (num != 4070522309U)
		{
			if (num != 4214602626U)
			{
				if (num != 4257872637U)
				{
					return;
				}
				if (!(name == "CLASS_ID"))
				{
					return;
				}
				this.m_classId = (int)val;
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
			if (!(name == "UNLOCKED_DESCRIPTION_TEXT"))
			{
				return;
			}
			this.m_unlockedDescriptionText = (DbfLocValue)val;
			return;
		}
	}

	// Token: 0x06001795 RID: 6037 RVA: 0x000824BC File Offset: 0x000806BC
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1674937439U)
		{
			if (num <= 451390141U)
			{
				if (num != 190718801U)
				{
					if (num == 451390141U)
					{
						if (name == "CARD_ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "ADVENTURE_ID")
				{
					return typeof(int);
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1608019912U)
				{
					if (num == 1674937439U)
					{
						if (name == "UNLOCK_VALUE")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "UNLOCK_GAME_SAVE_SUBKEY")
				{
					return typeof(int);
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 3710150967U)
		{
			if (num != 2401654691U)
			{
				if (num != 3034864917U)
				{
					if (num == 3710150967U)
					{
						if (name == "UNLOCK_CRITERIA_TEXT")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "UNLOCK_ACHIEVEMENT")
				{
					return typeof(int);
				}
			}
			else if (name == "IS_DEFAULT")
			{
				return typeof(bool);
			}
		}
		else if (num != 4070522309U)
		{
			if (num != 4214602626U)
			{
				if (num == 4257872637U)
				{
					if (name == "CLASS_ID")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "SORT_ORDER")
			{
				return typeof(int);
			}
		}
		else if (name == "UNLOCKED_DESCRIPTION_TEXT")
		{
			return typeof(DbfLocValue);
		}
		return null;
	}

	// Token: 0x06001796 RID: 6038 RVA: 0x000826C1 File Offset: 0x000808C1
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureHeroPowerDbfRecords loadRecords = new LoadAdventureHeroPowerDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001797 RID: 6039 RVA: 0x000826D8 File Offset: 0x000808D8
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureHeroPowerDbfAsset adventureHeroPowerDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureHeroPowerDbfAsset)) as AdventureHeroPowerDbfAsset;
		if (adventureHeroPowerDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AdventureHeroPowerDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < adventureHeroPowerDbfAsset.Records.Count; i++)
		{
			adventureHeroPowerDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (adventureHeroPowerDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001798 RID: 6040 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001799 RID: 6041 RVA: 0x00082757 File Offset: 0x00080957
	public override void StripUnusedLocales()
	{
		this.m_unlockCriteriaText.StripUnusedLocales();
		this.m_unlockedDescriptionText.StripUnusedLocales();
	}

	// Token: 0x04000EFA RID: 3834
	[SerializeField]
	private int m_adventureId;

	// Token: 0x04000EFB RID: 3835
	[SerializeField]
	private int m_classId;

	// Token: 0x04000EFC RID: 3836
	[SerializeField]
	private int m_cardId;

	// Token: 0x04000EFD RID: 3837
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x04000EFE RID: 3838
	[SerializeField]
	private bool m_isDefault;

	// Token: 0x04000EFF RID: 3839
	[SerializeField]
	private DbfLocValue m_unlockCriteriaText;

	// Token: 0x04000F00 RID: 3840
	[SerializeField]
	private DbfLocValue m_unlockedDescriptionText;

	// Token: 0x04000F01 RID: 3841
	[SerializeField]
	private int m_unlockGameSaveSubkeyId;

	// Token: 0x04000F02 RID: 3842
	[SerializeField]
	private int m_unlockValue;

	// Token: 0x04000F03 RID: 3843
	[SerializeField]
	private int m_unlockAchievementId;
}
