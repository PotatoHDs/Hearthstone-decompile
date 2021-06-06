using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000177 RID: 375
[Serializable]
public class AdventureLoadoutTreasuresDbfRecord : DbfRecord
{
	// Token: 0x170001DE RID: 478
	// (get) Token: 0x0600179F RID: 6047 RVA: 0x0008280A File Offset: 0x00080A0A
	[DbfField("ADVENTURE_ID")]
	public int AdventureId
	{
		get
		{
			return this.m_adventureId;
		}
	}

	// Token: 0x170001DF RID: 479
	// (get) Token: 0x060017A0 RID: 6048 RVA: 0x00082812 File Offset: 0x00080A12
	[DbfField("CLASS_ID")]
	public int ClassId
	{
		get
		{
			return this.m_classId;
		}
	}

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x060017A1 RID: 6049 RVA: 0x0008281A File Offset: 0x00080A1A
	public ClassDbfRecord ClassRecord
	{
		get
		{
			return GameDbf.Class.GetRecord(this.m_classId);
		}
	}

	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0008282C File Offset: 0x00080A2C
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x060017A3 RID: 6051 RVA: 0x00082834 File Offset: 0x00080A34
	public CardDbfRecord CardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_cardId);
		}
	}

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x060017A4 RID: 6052 RVA: 0x00082846 File Offset: 0x00080A46
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x060017A5 RID: 6053 RVA: 0x0008284E File Offset: 0x00080A4E
	[DbfField("IS_DEFAULT")]
	public bool IsDefault
	{
		get
		{
			return this.m_isDefault;
		}
	}

	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x060017A6 RID: 6054 RVA: 0x00082856 File Offset: 0x00080A56
	[DbfField("UNLOCK_CRITERIA_TEXT")]
	public DbfLocValue UnlockCriteriaText
	{
		get
		{
			return this.m_unlockCriteriaText;
		}
	}

	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0008285E File Offset: 0x00080A5E
	[DbfField("UNLOCKED_DESCRIPTION_TEXT")]
	public DbfLocValue UnlockedDescriptionText
	{
		get
		{
			return this.m_unlockedDescriptionText;
		}
	}

	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x060017A8 RID: 6056 RVA: 0x00082866 File Offset: 0x00080A66
	[DbfField("UNLOCK_GAME_SAVE_SUBKEY")]
	public int UnlockGameSaveSubkey
	{
		get
		{
			return this.m_unlockGameSaveSubkeyId;
		}
	}

	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x060017A9 RID: 6057 RVA: 0x0008286E File Offset: 0x00080A6E
	public GameSaveSubkeyDbfRecord UnlockGameSaveSubkeyRecord
	{
		get
		{
			return GameDbf.GameSaveSubkey.GetRecord(this.m_unlockGameSaveSubkeyId);
		}
	}

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x060017AA RID: 6058 RVA: 0x00082880 File Offset: 0x00080A80
	[DbfField("UNLOCK_VALUE")]
	public int UnlockValue
	{
		get
		{
			return this.m_unlockValue;
		}
	}

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x060017AB RID: 6059 RVA: 0x00082888 File Offset: 0x00080A88
	[DbfField("UNLOCK_ACHIEVEMENT")]
	public int UnlockAchievement
	{
		get
		{
			return this.m_unlockAchievementId;
		}
	}

	// Token: 0x170001EB RID: 491
	// (get) Token: 0x060017AC RID: 6060 RVA: 0x00082890 File Offset: 0x00080A90
	public AchievementDbfRecord UnlockAchievementRecord
	{
		get
		{
			return GameDbf.Achievement.GetRecord(this.m_unlockAchievementId);
		}
	}

	// Token: 0x170001EC RID: 492
	// (get) Token: 0x060017AD RID: 6061 RVA: 0x000828A2 File Offset: 0x00080AA2
	[DbfField("UPGRADED_CARD_ID")]
	public int UpgradedCardId
	{
		get
		{
			return this.m_upgradedCardId;
		}
	}

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x060017AE RID: 6062 RVA: 0x000828AA File Offset: 0x00080AAA
	public CardDbfRecord UpgradedCardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_upgradedCardId);
		}
	}

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x060017AF RID: 6063 RVA: 0x000828BC File Offset: 0x00080ABC
	[DbfField("UPGRADED_DESCRIPTION_TEXT")]
	public DbfLocValue UpgradedDescriptionText
	{
		get
		{
			return this.m_upgradedDescriptionText;
		}
	}

	// Token: 0x170001EF RID: 495
	// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000828C4 File Offset: 0x00080AC4
	[DbfField("UPGRADE_GAME_SAVE_SUBKEY")]
	public int UpgradeGameSaveSubkey
	{
		get
		{
			return this.m_upgradeGameSaveSubkeyId;
		}
	}

	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x060017B1 RID: 6065 RVA: 0x000828CC File Offset: 0x00080ACC
	public GameSaveSubkeyDbfRecord UpgradeGameSaveSubkeyRecord
	{
		get
		{
			return GameDbf.GameSaveSubkey.GetRecord(this.m_upgradeGameSaveSubkeyId);
		}
	}

	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000828DE File Offset: 0x00080ADE
	[DbfField("UPGRADE_VALUE")]
	public int UpgradeValue
	{
		get
		{
			return this.m_upgradeValue;
		}
	}

	// Token: 0x060017B3 RID: 6067 RVA: 0x000828E6 File Offset: 0x00080AE6
	public void SetAdventureId(int v)
	{
		this.m_adventureId = v;
	}

	// Token: 0x060017B4 RID: 6068 RVA: 0x000828EF File Offset: 0x00080AEF
	public void SetClassId(int v)
	{
		this.m_classId = v;
	}

	// Token: 0x060017B5 RID: 6069 RVA: 0x000828F8 File Offset: 0x00080AF8
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x060017B6 RID: 6070 RVA: 0x00082901 File Offset: 0x00080B01
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x060017B7 RID: 6071 RVA: 0x0008290A File Offset: 0x00080B0A
	public void SetIsDefault(bool v)
	{
		this.m_isDefault = v;
	}

	// Token: 0x060017B8 RID: 6072 RVA: 0x00082913 File Offset: 0x00080B13
	public void SetUnlockCriteriaText(DbfLocValue v)
	{
		this.m_unlockCriteriaText = v;
		v.SetDebugInfo(base.ID, "UNLOCK_CRITERIA_TEXT");
	}

	// Token: 0x060017B9 RID: 6073 RVA: 0x0008292D File Offset: 0x00080B2D
	public void SetUnlockedDescriptionText(DbfLocValue v)
	{
		this.m_unlockedDescriptionText = v;
		v.SetDebugInfo(base.ID, "UNLOCKED_DESCRIPTION_TEXT");
	}

	// Token: 0x060017BA RID: 6074 RVA: 0x00082947 File Offset: 0x00080B47
	public void SetUnlockGameSaveSubkey(int v)
	{
		this.m_unlockGameSaveSubkeyId = v;
	}

	// Token: 0x060017BB RID: 6075 RVA: 0x00082950 File Offset: 0x00080B50
	public void SetUnlockValue(int v)
	{
		this.m_unlockValue = v;
	}

	// Token: 0x060017BC RID: 6076 RVA: 0x00082959 File Offset: 0x00080B59
	public void SetUnlockAchievement(int v)
	{
		this.m_unlockAchievementId = v;
	}

	// Token: 0x060017BD RID: 6077 RVA: 0x00082962 File Offset: 0x00080B62
	public void SetUpgradedCardId(int v)
	{
		this.m_upgradedCardId = v;
	}

	// Token: 0x060017BE RID: 6078 RVA: 0x0008296B File Offset: 0x00080B6B
	public void SetUpgradedDescriptionText(DbfLocValue v)
	{
		this.m_upgradedDescriptionText = v;
		v.SetDebugInfo(base.ID, "UPGRADED_DESCRIPTION_TEXT");
	}

	// Token: 0x060017BF RID: 6079 RVA: 0x00082985 File Offset: 0x00080B85
	public void SetUpgradeGameSaveSubkey(int v)
	{
		this.m_upgradeGameSaveSubkeyId = v;
	}

	// Token: 0x060017C0 RID: 6080 RVA: 0x0008298E File Offset: 0x00080B8E
	public void SetUpgradeValue(int v)
	{
		this.m_upgradeValue = v;
	}

	// Token: 0x060017C1 RID: 6081 RVA: 0x00082998 File Offset: 0x00080B98
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2364829034U)
		{
			if (num <= 1458105184U)
			{
				if (num != 190718801U)
				{
					if (num != 451390141U)
					{
						if (num == 1458105184U)
						{
							if (name == "ID")
							{
								return base.ID;
							}
						}
					}
					else if (name == "CARD_ID")
					{
						return this.m_cardId;
					}
				}
				else if (name == "ADVENTURE_ID")
				{
					return this.m_adventureId;
				}
			}
			else if (num <= 1674937439U)
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
			else if (num != 1785070112U)
			{
				if (num == 2364829034U)
				{
					if (name == "UPGRADE_GAME_SAVE_SUBKEY")
					{
						return this.m_upgradeGameSaveSubkeyId;
					}
				}
			}
			else if (name == "UPGRADED_CARD_ID")
			{
				return this.m_upgradedCardId;
			}
		}
		else if (num <= 3034864917U)
		{
			if (num <= 2477374857U)
			{
				if (num != 2401654691U)
				{
					if (num == 2477374857U)
					{
						if (name == "UPGRADE_VALUE")
						{
							return this.m_upgradeValue;
						}
					}
				}
				else if (name == "IS_DEFAULT")
				{
					return this.m_isDefault;
				}
			}
			else if (num != 2795899714U)
			{
				if (num == 3034864917U)
				{
					if (name == "UNLOCK_ACHIEVEMENT")
					{
						return this.m_unlockAchievementId;
					}
				}
			}
			else if (name == "UPGRADED_DESCRIPTION_TEXT")
			{
				return this.m_upgradedDescriptionText;
			}
		}
		else if (num <= 4070522309U)
		{
			if (num != 3710150967U)
			{
				if (num == 4070522309U)
				{
					if (name == "UNLOCKED_DESCRIPTION_TEXT")
					{
						return this.m_unlockedDescriptionText;
					}
				}
			}
			else if (name == "UNLOCK_CRITERIA_TEXT")
			{
				return this.m_unlockCriteriaText;
			}
		}
		else if (num != 4214602626U)
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
		return null;
	}

	// Token: 0x060017C2 RID: 6082 RVA: 0x00082C80 File Offset: 0x00080E80
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2364829034U)
		{
			if (num <= 1458105184U)
			{
				if (num != 190718801U)
				{
					if (num != 451390141U)
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
						if (!(name == "CARD_ID"))
						{
							return;
						}
						this.m_cardId = (int)val;
						return;
					}
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
			else if (num <= 1674937439U)
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
			else if (num != 1785070112U)
			{
				if (num != 2364829034U)
				{
					return;
				}
				if (!(name == "UPGRADE_GAME_SAVE_SUBKEY"))
				{
					return;
				}
				this.m_upgradeGameSaveSubkeyId = (int)val;
				return;
			}
			else
			{
				if (!(name == "UPGRADED_CARD_ID"))
				{
					return;
				}
				this.m_upgradedCardId = (int)val;
				return;
			}
		}
		else if (num <= 3034864917U)
		{
			if (num <= 2477374857U)
			{
				if (num != 2401654691U)
				{
					if (num != 2477374857U)
					{
						return;
					}
					if (!(name == "UPGRADE_VALUE"))
					{
						return;
					}
					this.m_upgradeValue = (int)val;
					return;
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
			else if (num != 2795899714U)
			{
				if (num != 3034864917U)
				{
					return;
				}
				if (!(name == "UNLOCK_ACHIEVEMENT"))
				{
					return;
				}
				this.m_unlockAchievementId = (int)val;
				return;
			}
			else
			{
				if (!(name == "UPGRADED_DESCRIPTION_TEXT"))
				{
					return;
				}
				this.m_upgradedDescriptionText = (DbfLocValue)val;
				return;
			}
		}
		else if (num <= 4070522309U)
		{
			if (num != 3710150967U)
			{
				if (num != 4070522309U)
				{
					return;
				}
				if (!(name == "UNLOCKED_DESCRIPTION_TEXT"))
				{
					return;
				}
				this.m_unlockedDescriptionText = (DbfLocValue)val;
				return;
			}
			else
			{
				if (!(name == "UNLOCK_CRITERIA_TEXT"))
				{
					return;
				}
				this.m_unlockCriteriaText = (DbfLocValue)val;
				return;
			}
		}
		else if (num != 4214602626U)
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

	// Token: 0x060017C3 RID: 6083 RVA: 0x00082F2C File Offset: 0x0008112C
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2364829034U)
		{
			if (num <= 1458105184U)
			{
				if (num != 190718801U)
				{
					if (num != 451390141U)
					{
						if (num == 1458105184U)
						{
							if (name == "ID")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "CARD_ID")
					{
						return typeof(int);
					}
				}
				else if (name == "ADVENTURE_ID")
				{
					return typeof(int);
				}
			}
			else if (num <= 1674937439U)
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
			else if (num != 1785070112U)
			{
				if (num == 2364829034U)
				{
					if (name == "UPGRADE_GAME_SAVE_SUBKEY")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "UPGRADED_CARD_ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 3034864917U)
		{
			if (num <= 2477374857U)
			{
				if (num != 2401654691U)
				{
					if (num == 2477374857U)
					{
						if (name == "UPGRADE_VALUE")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "IS_DEFAULT")
				{
					return typeof(bool);
				}
			}
			else if (num != 2795899714U)
			{
				if (num == 3034864917U)
				{
					if (name == "UNLOCK_ACHIEVEMENT")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "UPGRADED_DESCRIPTION_TEXT")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num <= 4070522309U)
		{
			if (num != 3710150967U)
			{
				if (num == 4070522309U)
				{
					if (name == "UNLOCKED_DESCRIPTION_TEXT")
					{
						return typeof(DbfLocValue);
					}
				}
			}
			else if (name == "UNLOCK_CRITERIA_TEXT")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num != 4214602626U)
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
		return null;
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x00083213 File Offset: 0x00081413
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureLoadoutTreasuresDbfRecords loadRecords = new LoadAdventureLoadoutTreasuresDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060017C5 RID: 6085 RVA: 0x0008322C File Offset: 0x0008142C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureLoadoutTreasuresDbfAsset adventureLoadoutTreasuresDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureLoadoutTreasuresDbfAsset)) as AdventureLoadoutTreasuresDbfAsset;
		if (adventureLoadoutTreasuresDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AdventureLoadoutTreasuresDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < adventureLoadoutTreasuresDbfAsset.Records.Count; i++)
		{
			adventureLoadoutTreasuresDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (adventureLoadoutTreasuresDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060017C6 RID: 6086 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x000832AB File Offset: 0x000814AB
	public override void StripUnusedLocales()
	{
		this.m_unlockCriteriaText.StripUnusedLocales();
		this.m_unlockedDescriptionText.StripUnusedLocales();
		this.m_upgradedDescriptionText.StripUnusedLocales();
	}

	// Token: 0x04000F06 RID: 3846
	[SerializeField]
	private int m_adventureId;

	// Token: 0x04000F07 RID: 3847
	[SerializeField]
	private int m_classId;

	// Token: 0x04000F08 RID: 3848
	[SerializeField]
	private int m_cardId;

	// Token: 0x04000F09 RID: 3849
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x04000F0A RID: 3850
	[SerializeField]
	private bool m_isDefault;

	// Token: 0x04000F0B RID: 3851
	[SerializeField]
	private DbfLocValue m_unlockCriteriaText;

	// Token: 0x04000F0C RID: 3852
	[SerializeField]
	private DbfLocValue m_unlockedDescriptionText;

	// Token: 0x04000F0D RID: 3853
	[SerializeField]
	private int m_unlockGameSaveSubkeyId;

	// Token: 0x04000F0E RID: 3854
	[SerializeField]
	private int m_unlockValue;

	// Token: 0x04000F0F RID: 3855
	[SerializeField]
	private int m_unlockAchievementId;

	// Token: 0x04000F10 RID: 3856
	[SerializeField]
	private int m_upgradedCardId;

	// Token: 0x04000F11 RID: 3857
	[SerializeField]
	private DbfLocValue m_upgradedDescriptionText;

	// Token: 0x04000F12 RID: 3858
	[SerializeField]
	private int m_upgradeGameSaveSubkeyId;

	// Token: 0x04000F13 RID: 3859
	[SerializeField]
	private int m_upgradeValue;
}
