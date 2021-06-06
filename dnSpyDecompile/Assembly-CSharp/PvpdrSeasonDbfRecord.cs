using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200022F RID: 559
[Serializable]
public class PvpdrSeasonDbfRecord : DbfRecord
{
	// Token: 0x170003A8 RID: 936
	// (get) Token: 0x06001DEE RID: 7662 RVA: 0x0009892E File Offset: 0x00096B2E
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x170003A9 RID: 937
	// (get) Token: 0x06001DEF RID: 7663 RVA: 0x00098936 File Offset: 0x00096B36
	[DbfField("EVENT")]
	public string Event
	{
		get
		{
			return this.m_event;
		}
	}

	// Token: 0x170003AA RID: 938
	// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x0009893E File Offset: 0x00096B3E
	[DbfField("ADVENTURE_ID")]
	public int AdventureId
	{
		get
		{
			return this.m_adventureId;
		}
	}

	// Token: 0x170003AB RID: 939
	// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x00098946 File Offset: 0x00096B46
	public AdventureDbfRecord AdventureRecord
	{
		get
		{
			return GameDbf.Adventure.GetRecord(this.m_adventureId);
		}
	}

	// Token: 0x170003AC RID: 940
	// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x00098958 File Offset: 0x00096B58
	[DbfField("SCENARIO_ID")]
	public int ScenarioId
	{
		get
		{
			return this.m_scenarioId;
		}
	}

	// Token: 0x170003AD RID: 941
	// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x00098960 File Offset: 0x00096B60
	public ScenarioDbfRecord ScenarioRecord
	{
		get
		{
			return GameDbf.Scenario.GetRecord(this.m_scenarioId);
		}
	}

	// Token: 0x170003AE RID: 942
	// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x00098972 File Offset: 0x00096B72
	[DbfField("MAX_WINS")]
	public int MaxWins
	{
		get
		{
			return this.m_maxWins;
		}
	}

	// Token: 0x170003AF RID: 943
	// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x0009897A File Offset: 0x00096B7A
	[DbfField("MAX_LOSSES")]
	public int MaxLosses
	{
		get
		{
			return this.m_maxLosses;
		}
	}

	// Token: 0x170003B0 RID: 944
	// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x00098982 File Offset: 0x00096B82
	[DbfField("DECK_DISPLAY_RULESET_ID")]
	public int DeckDisplayRulesetId
	{
		get
		{
			return this.m_deckDisplayRulesetId;
		}
	}

	// Token: 0x170003B1 RID: 945
	// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x0009898A File Offset: 0x00096B8A
	public DeckRulesetDbfRecord DeckDisplayRulesetRecord
	{
		get
		{
			return GameDbf.DeckRuleset.GetRecord(this.m_deckDisplayRulesetId);
		}
	}

	// Token: 0x170003B2 RID: 946
	// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x0009899C File Offset: 0x00096B9C
	[DbfField("MAX_HEROES_DRAFTED")]
	public int MaxHeroesDrafted
	{
		get
		{
			return this.m_maxHeroesDrafted;
		}
	}

	// Token: 0x170003B3 RID: 947
	// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x000989A4 File Offset: 0x00096BA4
	[DbfField("REWARD_CHEST_ID")]
	public int RewardChestId
	{
		get
		{
			return this.m_rewardChestId;
		}
	}

	// Token: 0x170003B4 RID: 948
	// (get) Token: 0x06001DFA RID: 7674 RVA: 0x000989AC File Offset: 0x00096BAC
	public RewardChestDbfRecord RewardChestRecord
	{
		get
		{
			return GameDbf.RewardChest.GetRecord(this.m_rewardChestId);
		}
	}

	// Token: 0x170003B5 RID: 949
	// (get) Token: 0x06001DFB RID: 7675 RVA: 0x000989BE File Offset: 0x00096BBE
	public List<GuestHeroSelectionRatioDbfRecord> GuestHeroSelectionRatio
	{
		get
		{
			return GameDbf.GuestHeroSelectionRatio.GetRecords((GuestHeroSelectionRatioDbfRecord r) => r.PvpdrSeasonId == base.ID, -1);
		}
	}

	// Token: 0x06001DFC RID: 7676 RVA: 0x000989D7 File Offset: 0x00096BD7
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001DFD RID: 7677 RVA: 0x000989E0 File Offset: 0x00096BE0
	public void SetEvent(string v)
	{
		this.m_event = v;
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x000989E9 File Offset: 0x00096BE9
	public void SetAdventureId(int v)
	{
		this.m_adventureId = v;
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x000989F2 File Offset: 0x00096BF2
	public void SetScenarioId(int v)
	{
		this.m_scenarioId = v;
	}

	// Token: 0x06001E00 RID: 7680 RVA: 0x000989FB File Offset: 0x00096BFB
	public void SetMaxWins(int v)
	{
		this.m_maxWins = v;
	}

	// Token: 0x06001E01 RID: 7681 RVA: 0x00098A04 File Offset: 0x00096C04
	public void SetMaxLosses(int v)
	{
		this.m_maxLosses = v;
	}

	// Token: 0x06001E02 RID: 7682 RVA: 0x00098A0D File Offset: 0x00096C0D
	public void SetDeckDisplayRulesetId(int v)
	{
		this.m_deckDisplayRulesetId = v;
	}

	// Token: 0x06001E03 RID: 7683 RVA: 0x00098A16 File Offset: 0x00096C16
	public void SetMaxHeroesDrafted(int v)
	{
		this.m_maxHeroesDrafted = v;
	}

	// Token: 0x06001E04 RID: 7684 RVA: 0x00098A1F File Offset: 0x00096C1F
	public void SetRewardChestId(int v)
	{
		this.m_rewardChestId = v;
	}

	// Token: 0x06001E05 RID: 7685 RVA: 0x00098A28 File Offset: 0x00096C28
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 767343776U)
		{
			if (num <= 236776447U)
			{
				if (num != 190718801U)
				{
					if (num == 236776447U)
					{
						if (name == "EVENT")
						{
							return this.m_event;
						}
					}
				}
				else if (name == "ADVENTURE_ID")
				{
					return this.m_adventureId;
				}
			}
			else if (num != 544621747U)
			{
				if (num != 693605261U)
				{
					if (num == 767343776U)
					{
						if (name == "DECK_DISPLAY_RULESET_ID")
						{
							return this.m_deckDisplayRulesetId;
						}
					}
				}
				else if (name == "SCENARIO_ID")
				{
					return this.m_scenarioId;
				}
			}
			else if (name == "MAX_HEROES_DRAFTED")
			{
				return this.m_maxHeroesDrafted;
			}
		}
		else if (num <= 1458105184U)
		{
			if (num != 807866572U)
			{
				if (num == 1458105184U)
				{
					if (name == "ID")
					{
						return base.ID;
					}
				}
			}
			else if (name == "REWARD_CHEST_ID")
			{
				return this.m_rewardChestId;
			}
		}
		else if (num != 1831643509U)
		{
			if (num != 3022554311U)
			{
				if (num == 4242835337U)
				{
					if (name == "MAX_LOSSES")
					{
						return this.m_maxLosses;
					}
				}
			}
			else if (name == "NOTE_DESC")
			{
				return this.m_noteDesc;
			}
		}
		else if (name == "MAX_WINS")
		{
			return this.m_maxWins;
		}
		return null;
	}

	// Token: 0x06001E06 RID: 7686 RVA: 0x00098BF4 File Offset: 0x00096DF4
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 767343776U)
		{
			if (num <= 236776447U)
			{
				if (num != 190718801U)
				{
					if (num != 236776447U)
					{
						return;
					}
					if (!(name == "EVENT"))
					{
						return;
					}
					this.m_event = (string)val;
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
			else if (num != 544621747U)
			{
				if (num != 693605261U)
				{
					if (num != 767343776U)
					{
						return;
					}
					if (!(name == "DECK_DISPLAY_RULESET_ID"))
					{
						return;
					}
					this.m_deckDisplayRulesetId = (int)val;
					return;
				}
				else
				{
					if (!(name == "SCENARIO_ID"))
					{
						return;
					}
					this.m_scenarioId = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "MAX_HEROES_DRAFTED"))
				{
					return;
				}
				this.m_maxHeroesDrafted = (int)val;
				return;
			}
		}
		else if (num <= 1458105184U)
		{
			if (num != 807866572U)
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
				if (!(name == "REWARD_CHEST_ID"))
				{
					return;
				}
				this.m_rewardChestId = (int)val;
				return;
			}
		}
		else if (num != 1831643509U)
		{
			if (num != 3022554311U)
			{
				if (num != 4242835337U)
				{
					return;
				}
				if (!(name == "MAX_LOSSES"))
				{
					return;
				}
				this.m_maxLosses = (int)val;
				return;
			}
			else
			{
				if (!(name == "NOTE_DESC"))
				{
					return;
				}
				this.m_noteDesc = (string)val;
				return;
			}
		}
		else
		{
			if (!(name == "MAX_WINS"))
			{
				return;
			}
			this.m_maxWins = (int)val;
			return;
		}
	}

	// Token: 0x06001E07 RID: 7687 RVA: 0x00098D90 File Offset: 0x00096F90
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 767343776U)
		{
			if (num <= 236776447U)
			{
				if (num != 190718801U)
				{
					if (num == 236776447U)
					{
						if (name == "EVENT")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "ADVENTURE_ID")
				{
					return typeof(int);
				}
			}
			else if (num != 544621747U)
			{
				if (num != 693605261U)
				{
					if (num == 767343776U)
					{
						if (name == "DECK_DISPLAY_RULESET_ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "SCENARIO_ID")
				{
					return typeof(int);
				}
			}
			else if (name == "MAX_HEROES_DRAFTED")
			{
				return typeof(int);
			}
		}
		else if (num <= 1458105184U)
		{
			if (num != 807866572U)
			{
				if (num == 1458105184U)
				{
					if (name == "ID")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "REWARD_CHEST_ID")
			{
				return typeof(int);
			}
		}
		else if (num != 1831643509U)
		{
			if (num != 3022554311U)
			{
				if (num == 4242835337U)
				{
					if (name == "MAX_LOSSES")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "NOTE_DESC")
			{
				return typeof(string);
			}
		}
		else if (name == "MAX_WINS")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x06001E08 RID: 7688 RVA: 0x00098F64 File Offset: 0x00097164
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadPvpdrSeasonDbfRecords loadRecords = new LoadPvpdrSeasonDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001E09 RID: 7689 RVA: 0x00098F7C File Offset: 0x0009717C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		PvpdrSeasonDbfAsset pvpdrSeasonDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(PvpdrSeasonDbfAsset)) as PvpdrSeasonDbfAsset;
		if (pvpdrSeasonDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("PvpdrSeasonDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < pvpdrSeasonDbfAsset.Records.Count; i++)
		{
			pvpdrSeasonDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (pvpdrSeasonDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001E0A RID: 7690 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001E0B RID: 7691 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001171 RID: 4465
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04001172 RID: 4466
	[SerializeField]
	private string m_event;

	// Token: 0x04001173 RID: 4467
	[SerializeField]
	private int m_adventureId;

	// Token: 0x04001174 RID: 4468
	[SerializeField]
	private int m_scenarioId;

	// Token: 0x04001175 RID: 4469
	[SerializeField]
	private int m_maxWins;

	// Token: 0x04001176 RID: 4470
	[SerializeField]
	private int m_maxLosses;

	// Token: 0x04001177 RID: 4471
	[SerializeField]
	private int m_deckDisplayRulesetId;

	// Token: 0x04001178 RID: 4472
	[SerializeField]
	private int m_maxHeroesDrafted;

	// Token: 0x04001179 RID: 4473
	[SerializeField]
	private int m_rewardChestId;
}
