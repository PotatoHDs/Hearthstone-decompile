using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200016E RID: 366
[Serializable]
public class AdventureDeckDbfRecord : DbfRecord
{
	// Token: 0x170001BA RID: 442
	// (get) Token: 0x0600173D RID: 5949 RVA: 0x0008120E File Offset: 0x0007F40E
	[DbfField("ADVENTURE_ID")]
	public int AdventureId
	{
		get
		{
			return this.m_adventureId;
		}
	}

	// Token: 0x170001BB RID: 443
	// (get) Token: 0x0600173E RID: 5950 RVA: 0x00081216 File Offset: 0x0007F416
	[DbfField("CLASS_ID")]
	public int ClassId
	{
		get
		{
			return this.m_classId;
		}
	}

	// Token: 0x170001BC RID: 444
	// (get) Token: 0x0600173F RID: 5951 RVA: 0x0008121E File Offset: 0x0007F41E
	public ClassDbfRecord ClassRecord
	{
		get
		{
			return GameDbf.Class.GetRecord(this.m_classId);
		}
	}

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x06001740 RID: 5952 RVA: 0x00081230 File Offset: 0x0007F430
	[DbfField("DECK_ID")]
	public int DeckId
	{
		get
		{
			return this.m_deckId;
		}
	}

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x06001741 RID: 5953 RVA: 0x00081238 File Offset: 0x0007F438
	public DeckDbfRecord DeckRecord
	{
		get
		{
			return GameDbf.Deck.GetRecord(this.m_deckId);
		}
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x06001742 RID: 5954 RVA: 0x0008124A File Offset: 0x0007F44A
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x06001743 RID: 5955 RVA: 0x00081252 File Offset: 0x0007F452
	[DbfField("UNLOCK_CRITERIA_TEXT")]
	public DbfLocValue UnlockCriteriaText
	{
		get
		{
			return this.m_unlockCriteriaText;
		}
	}

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x06001744 RID: 5956 RVA: 0x0008125A File Offset: 0x0007F45A
	[DbfField("UNLOCKED_DESCRIPTION_TEXT")]
	public DbfLocValue UnlockedDescriptionText
	{
		get
		{
			return this.m_unlockedDescriptionText;
		}
	}

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x06001745 RID: 5957 RVA: 0x00081262 File Offset: 0x0007F462
	[DbfField("UNLOCK_GAME_SAVE_SUBKEY")]
	public int UnlockGameSaveSubkey
	{
		get
		{
			return this.m_unlockGameSaveSubkeyId;
		}
	}

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x06001746 RID: 5958 RVA: 0x0008126A File Offset: 0x0007F46A
	public GameSaveSubkeyDbfRecord UnlockGameSaveSubkeyRecord
	{
		get
		{
			return GameDbf.GameSaveSubkey.GetRecord(this.m_unlockGameSaveSubkeyId);
		}
	}

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x06001747 RID: 5959 RVA: 0x0008127C File Offset: 0x0007F47C
	[DbfField("UNLOCK_VALUE")]
	public int UnlockValue
	{
		get
		{
			return this.m_unlockValue;
		}
	}

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x06001748 RID: 5960 RVA: 0x00081284 File Offset: 0x0007F484
	[DbfField("DISPLAY_TEXTURE")]
	public string DisplayTexture
	{
		get
		{
			return this.m_displayTexture;
		}
	}

	// Token: 0x06001749 RID: 5961 RVA: 0x0008128C File Offset: 0x0007F48C
	public void SetAdventureId(int v)
	{
		this.m_adventureId = v;
	}

	// Token: 0x0600174A RID: 5962 RVA: 0x00081295 File Offset: 0x0007F495
	public void SetClassId(int v)
	{
		this.m_classId = v;
	}

	// Token: 0x0600174B RID: 5963 RVA: 0x0008129E File Offset: 0x0007F49E
	public void SetDeckId(int v)
	{
		this.m_deckId = v;
	}

	// Token: 0x0600174C RID: 5964 RVA: 0x000812A7 File Offset: 0x0007F4A7
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x0600174D RID: 5965 RVA: 0x000812B0 File Offset: 0x0007F4B0
	public void SetUnlockCriteriaText(DbfLocValue v)
	{
		this.m_unlockCriteriaText = v;
		v.SetDebugInfo(base.ID, "UNLOCK_CRITERIA_TEXT");
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x000812CA File Offset: 0x0007F4CA
	public void SetUnlockedDescriptionText(DbfLocValue v)
	{
		this.m_unlockedDescriptionText = v;
		v.SetDebugInfo(base.ID, "UNLOCKED_DESCRIPTION_TEXT");
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x000812E4 File Offset: 0x0007F4E4
	public void SetUnlockGameSaveSubkey(int v)
	{
		this.m_unlockGameSaveSubkeyId = v;
	}

	// Token: 0x06001750 RID: 5968 RVA: 0x000812ED File Offset: 0x0007F4ED
	public void SetUnlockValue(int v)
	{
		this.m_unlockValue = v;
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x000812F6 File Offset: 0x0007F4F6
	public void SetDisplayTexture(string v)
	{
		this.m_displayTexture = v;
	}

	// Token: 0x06001752 RID: 5970 RVA: 0x00081300 File Offset: 0x0007F500
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1674937439U)
		{
			if (num <= 771121008U)
			{
				if (num != 190718801U)
				{
					if (num == 771121008U)
					{
						if (name == "DECK_ID")
						{
							return this.m_deckId;
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
			if (num != 2452245441U)
			{
				if (num == 3710150967U)
				{
					if (name == "UNLOCK_CRITERIA_TEXT")
					{
						return this.m_unlockCriteriaText;
					}
				}
			}
			else if (name == "DISPLAY_TEXTURE")
			{
				return this.m_displayTexture;
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

	// Token: 0x06001753 RID: 5971 RVA: 0x000814D0 File Offset: 0x0007F6D0
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1674937439U)
		{
			if (num <= 771121008U)
			{
				if (num != 190718801U)
				{
					if (num != 771121008U)
					{
						return;
					}
					if (!(name == "DECK_ID"))
					{
						return;
					}
					this.m_deckId = (int)val;
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
			if (num != 2452245441U)
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
				if (!(name == "DISPLAY_TEXTURE"))
				{
					return;
				}
				this.m_displayTexture = (string)val;
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

	// Token: 0x06001754 RID: 5972 RVA: 0x00081668 File Offset: 0x0007F868
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1674937439U)
		{
			if (num <= 771121008U)
			{
				if (num != 190718801U)
				{
					if (num == 771121008U)
					{
						if (name == "DECK_ID")
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
			if (num != 2452245441U)
			{
				if (num == 3710150967U)
				{
					if (name == "UNLOCK_CRITERIA_TEXT")
					{
						return typeof(DbfLocValue);
					}
				}
			}
			else if (name == "DISPLAY_TEXTURE")
			{
				return typeof(string);
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

	// Token: 0x06001755 RID: 5973 RVA: 0x00081839 File Offset: 0x0007FA39
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureDeckDbfRecords loadRecords = new LoadAdventureDeckDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001756 RID: 5974 RVA: 0x00081850 File Offset: 0x0007FA50
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureDeckDbfAsset adventureDeckDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureDeckDbfAsset)) as AdventureDeckDbfAsset;
		if (adventureDeckDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AdventureDeckDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < adventureDeckDbfAsset.Records.Count; i++)
		{
			adventureDeckDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (adventureDeckDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001757 RID: 5975 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001758 RID: 5976 RVA: 0x000818CF File Offset: 0x0007FACF
	public override void StripUnusedLocales()
	{
		this.m_unlockCriteriaText.StripUnusedLocales();
		this.m_unlockedDescriptionText.StripUnusedLocales();
	}

	// Token: 0x04000EE6 RID: 3814
	[SerializeField]
	private int m_adventureId;

	// Token: 0x04000EE7 RID: 3815
	[SerializeField]
	private int m_classId;

	// Token: 0x04000EE8 RID: 3816
	[SerializeField]
	private int m_deckId;

	// Token: 0x04000EE9 RID: 3817
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x04000EEA RID: 3818
	[SerializeField]
	private DbfLocValue m_unlockCriteriaText;

	// Token: 0x04000EEB RID: 3819
	[SerializeField]
	private DbfLocValue m_unlockedDescriptionText;

	// Token: 0x04000EEC RID: 3820
	[SerializeField]
	private int m_unlockGameSaveSubkeyId;

	// Token: 0x04000EED RID: 3821
	[SerializeField]
	private int m_unlockValue;

	// Token: 0x04000EEE RID: 3822
	[SerializeField]
	private string m_displayTexture;
}
