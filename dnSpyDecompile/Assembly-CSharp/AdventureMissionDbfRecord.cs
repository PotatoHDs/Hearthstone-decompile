using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200017A RID: 378
[Serializable]
public class AdventureMissionDbfRecord : DbfRecord
{
	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x060017CD RID: 6093 RVA: 0x0008336A File Offset: 0x0008156A
	[DbfField("SCENARIO_ID")]
	public int ScenarioId
	{
		get
		{
			return this.m_scenarioId;
		}
	}

	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x060017CE RID: 6094 RVA: 0x00083372 File Offset: 0x00081572
	public ScenarioDbfRecord ScenarioRecord
	{
		get
		{
			return GameDbf.Scenario.GetRecord(this.m_scenarioId);
		}
	}

	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x060017CF RID: 6095 RVA: 0x00083384 File Offset: 0x00081584
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0008338C File Offset: 0x0008158C
	[DbfField("REQ_WING_ID")]
	public int ReqWingId
	{
		get
		{
			return this.m_reqWingId;
		}
	}

	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x060017D1 RID: 6097 RVA: 0x00083394 File Offset: 0x00081594
	public WingDbfRecord ReqWingRecord
	{
		get
		{
			return GameDbf.Wing.GetRecord(this.m_reqWingId);
		}
	}

	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x060017D2 RID: 6098 RVA: 0x000833A6 File Offset: 0x000815A6
	[DbfField("REQ_PROGRESS")]
	public int ReqProgress
	{
		get
		{
			return this.m_reqProgress;
		}
	}

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x060017D3 RID: 6099 RVA: 0x000833AE File Offset: 0x000815AE
	[DbfField("REQ_FLAGS")]
	public ulong ReqFlags
	{
		get
		{
			return this.m_reqFlags;
		}
	}

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x060017D4 RID: 6100 RVA: 0x000833B6 File Offset: 0x000815B6
	[DbfField("GRANTS_WING_ID")]
	public int GrantsWingId
	{
		get
		{
			return this.m_grantsWingId;
		}
	}

	// Token: 0x170001FA RID: 506
	// (get) Token: 0x060017D5 RID: 6101 RVA: 0x000833BE File Offset: 0x000815BE
	public WingDbfRecord GrantsWingRecord
	{
		get
		{
			return GameDbf.Wing.GetRecord(this.m_grantsWingId);
		}
	}

	// Token: 0x170001FB RID: 507
	// (get) Token: 0x060017D6 RID: 6102 RVA: 0x000833D0 File Offset: 0x000815D0
	[DbfField("GRANTS_PROGRESS")]
	public int GrantsProgress
	{
		get
		{
			return this.m_grantsProgress;
		}
	}

	// Token: 0x170001FC RID: 508
	// (get) Token: 0x060017D7 RID: 6103 RVA: 0x000833D8 File Offset: 0x000815D8
	[DbfField("GRANTS_FLAGS")]
	public ulong GrantsFlags
	{
		get
		{
			return this.m_grantsFlags;
		}
	}

	// Token: 0x170001FD RID: 509
	// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000833E0 File Offset: 0x000815E0
	[DbfField("BOSS_DEF_ASSET_PATH")]
	public string BossDefAssetPath
	{
		get
		{
			return this.m_bossDefAssetPath;
		}
	}

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x060017D9 RID: 6105 RVA: 0x000833E8 File Offset: 0x000815E8
	[DbfField("CLASS_CHALLENGE_PREFAB_POPUP")]
	public string ClassChallengePrefabPopup
	{
		get
		{
			return this.m_classChallengePrefabPopup;
		}
	}

	// Token: 0x060017DA RID: 6106 RVA: 0x000833F0 File Offset: 0x000815F0
	public void SetScenarioId(int v)
	{
		this.m_scenarioId = v;
	}

	// Token: 0x060017DB RID: 6107 RVA: 0x000833F9 File Offset: 0x000815F9
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x060017DC RID: 6108 RVA: 0x00083402 File Offset: 0x00081602
	public void SetReqWingId(int v)
	{
		this.m_reqWingId = v;
	}

	// Token: 0x060017DD RID: 6109 RVA: 0x0008340B File Offset: 0x0008160B
	public void SetReqProgress(int v)
	{
		this.m_reqProgress = v;
	}

	// Token: 0x060017DE RID: 6110 RVA: 0x00083414 File Offset: 0x00081614
	public void SetReqFlags(ulong v)
	{
		this.m_reqFlags = v;
	}

	// Token: 0x060017DF RID: 6111 RVA: 0x0008341D File Offset: 0x0008161D
	public void SetGrantsWingId(int v)
	{
		this.m_grantsWingId = v;
	}

	// Token: 0x060017E0 RID: 6112 RVA: 0x00083426 File Offset: 0x00081626
	public void SetGrantsProgress(int v)
	{
		this.m_grantsProgress = v;
	}

	// Token: 0x060017E1 RID: 6113 RVA: 0x0008342F File Offset: 0x0008162F
	public void SetGrantsFlags(ulong v)
	{
		this.m_grantsFlags = v;
	}

	// Token: 0x060017E2 RID: 6114 RVA: 0x00083438 File Offset: 0x00081638
	public void SetBossDefAssetPath(string v)
	{
		this.m_bossDefAssetPath = v;
	}

	// Token: 0x060017E3 RID: 6115 RVA: 0x00083441 File Offset: 0x00081641
	public void SetClassChallengePrefabPopup(string v)
	{
		this.m_classChallengePrefabPopup = v;
	}

	// Token: 0x060017E4 RID: 6116 RVA: 0x0008344C File Offset: 0x0008164C
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2150619894U)
		{
			if (num <= 600120207U)
			{
				if (num != 283653000U)
				{
					if (num == 600120207U)
					{
						if (name == "REQ_PROGRESS")
						{
							return this.m_reqProgress;
						}
					}
				}
				else if (name == "GRANTS_WING_ID")
				{
					return this.m_grantsWingId;
				}
			}
			else if (num != 693605261U)
			{
				if (num != 1458105184U)
				{
					if (num == 2150619894U)
					{
						if (name == "GRANTS_FLAGS")
						{
							return this.m_grantsFlags;
						}
					}
				}
				else if (name == "ID")
				{
					return base.ID;
				}
			}
			else if (name == "SCENARIO_ID")
			{
				return this.m_scenarioId;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num != 2682090115U)
			{
				if (num != 2785465717U)
				{
					if (num == 3022554311U)
					{
						if (name == "NOTE_DESC")
						{
							return this.m_noteDesc;
						}
					}
				}
				else if (name == "CLASS_CHALLENGE_PREFAB_POPUP")
				{
					return this.m_classChallengePrefabPopup;
				}
			}
			else if (name == "BOSS_DEF_ASSET_PATH")
			{
				return this.m_bossDefAssetPath;
			}
		}
		else if (num != 3117134272U)
		{
			if (num != 3810699191U)
			{
				if (num == 3979977061U)
				{
					if (name == "REQ_WING_ID")
					{
						return this.m_reqWingId;
					}
				}
			}
			else if (name == "REQ_FLAGS")
			{
				return this.m_reqFlags;
			}
		}
		else if (name == "GRANTS_PROGRESS")
		{
			return this.m_grantsProgress;
		}
		return null;
	}

	// Token: 0x060017E5 RID: 6117 RVA: 0x0008364C File Offset: 0x0008184C
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2150619894U)
		{
			if (num <= 600120207U)
			{
				if (num != 283653000U)
				{
					if (num != 600120207U)
					{
						return;
					}
					if (!(name == "REQ_PROGRESS"))
					{
						return;
					}
					this.m_reqProgress = (int)val;
					return;
				}
				else
				{
					if (!(name == "GRANTS_WING_ID"))
					{
						return;
					}
					this.m_grantsWingId = (int)val;
					return;
				}
			}
			else if (num != 693605261U)
			{
				if (num != 1458105184U)
				{
					if (num != 2150619894U)
					{
						return;
					}
					if (!(name == "GRANTS_FLAGS"))
					{
						return;
					}
					this.m_grantsFlags = (ulong)val;
					return;
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
		else if (num <= 3022554311U)
		{
			if (num != 2682090115U)
			{
				if (num != 2785465717U)
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
					if (!(name == "CLASS_CHALLENGE_PREFAB_POPUP"))
					{
						return;
					}
					this.m_classChallengePrefabPopup = (string)val;
					return;
				}
			}
			else
			{
				if (!(name == "BOSS_DEF_ASSET_PATH"))
				{
					return;
				}
				this.m_bossDefAssetPath = (string)val;
				return;
			}
		}
		else if (num != 3117134272U)
		{
			if (num != 3810699191U)
			{
				if (num != 3979977061U)
				{
					return;
				}
				if (!(name == "REQ_WING_ID"))
				{
					return;
				}
				this.m_reqWingId = (int)val;
				return;
			}
			else
			{
				if (!(name == "REQ_FLAGS"))
				{
					return;
				}
				this.m_reqFlags = (ulong)val;
				return;
			}
		}
		else
		{
			if (!(name == "GRANTS_PROGRESS"))
			{
				return;
			}
			this.m_grantsProgress = (int)val;
			return;
		}
	}

	// Token: 0x060017E6 RID: 6118 RVA: 0x00083830 File Offset: 0x00081A30
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2150619894U)
		{
			if (num <= 600120207U)
			{
				if (num != 283653000U)
				{
					if (num == 600120207U)
					{
						if (name == "REQ_PROGRESS")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "GRANTS_WING_ID")
				{
					return typeof(int);
				}
			}
			else if (num != 693605261U)
			{
				if (num != 1458105184U)
				{
					if (num == 2150619894U)
					{
						if (name == "GRANTS_FLAGS")
						{
							return typeof(ulong);
						}
					}
				}
				else if (name == "ID")
				{
					return typeof(int);
				}
			}
			else if (name == "SCENARIO_ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 3022554311U)
		{
			if (num != 2682090115U)
			{
				if (num != 2785465717U)
				{
					if (num == 3022554311U)
					{
						if (name == "NOTE_DESC")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "CLASS_CHALLENGE_PREFAB_POPUP")
				{
					return typeof(string);
				}
			}
			else if (name == "BOSS_DEF_ASSET_PATH")
			{
				return typeof(string);
			}
		}
		else if (num != 3117134272U)
		{
			if (num != 3810699191U)
			{
				if (num == 3979977061U)
				{
					if (name == "REQ_WING_ID")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "REQ_FLAGS")
			{
				return typeof(ulong);
			}
		}
		else if (name == "GRANTS_PROGRESS")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x060017E7 RID: 6119 RVA: 0x00083A2F File Offset: 0x00081C2F
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureMissionDbfRecords loadRecords = new LoadAdventureMissionDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060017E8 RID: 6120 RVA: 0x00083A48 File Offset: 0x00081C48
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureMissionDbfAsset adventureMissionDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureMissionDbfAsset)) as AdventureMissionDbfAsset;
		if (adventureMissionDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AdventureMissionDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < adventureMissionDbfAsset.Records.Count; i++)
		{
			adventureMissionDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (adventureMissionDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060017E9 RID: 6121 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060017EA RID: 6122 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000F16 RID: 3862
	[SerializeField]
	private int m_scenarioId;

	// Token: 0x04000F17 RID: 3863
	[SerializeField]
	private string m_noteDesc = "SYSDATE ";

	// Token: 0x04000F18 RID: 3864
	[SerializeField]
	private int m_reqWingId;

	// Token: 0x04000F19 RID: 3865
	[SerializeField]
	private int m_reqProgress;

	// Token: 0x04000F1A RID: 3866
	[SerializeField]
	private ulong m_reqFlags;

	// Token: 0x04000F1B RID: 3867
	[SerializeField]
	private int m_grantsWingId;

	// Token: 0x04000F1C RID: 3868
	[SerializeField]
	private int m_grantsProgress;

	// Token: 0x04000F1D RID: 3869
	[SerializeField]
	private ulong m_grantsFlags;

	// Token: 0x04000F1E RID: 3870
	[SerializeField]
	private string m_bossDefAssetPath;

	// Token: 0x04000F1F RID: 3871
	[SerializeField]
	private string m_classChallengePrefabPopup;
}
