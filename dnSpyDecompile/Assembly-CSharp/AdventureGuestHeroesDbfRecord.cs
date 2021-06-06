using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000171 RID: 369
[Serializable]
public class AdventureGuestHeroesDbfRecord : DbfRecord
{
	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x0600175E RID: 5982 RVA: 0x00081982 File Offset: 0x0007FB82
	[DbfField("ADVENTURE_ID")]
	public int AdventureId
	{
		get
		{
			return this.m_adventureId;
		}
	}

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x0600175F RID: 5983 RVA: 0x0008198A File Offset: 0x0007FB8A
	[DbfField("WING_ID")]
	public int WingId
	{
		get
		{
			return this.m_wingId;
		}
	}

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x06001760 RID: 5984 RVA: 0x00081992 File Offset: 0x0007FB92
	public WingDbfRecord WingRecord
	{
		get
		{
			return GameDbf.Wing.GetRecord(this.m_wingId);
		}
	}

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x06001761 RID: 5985 RVA: 0x000819A4 File Offset: 0x0007FBA4
	[DbfField("GUEST_HERO_ID")]
	public int GuestHeroId
	{
		get
		{
			return this.m_guestHeroId;
		}
	}

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x06001762 RID: 5986 RVA: 0x000819AC File Offset: 0x0007FBAC
	public GuestHeroDbfRecord GuestHeroRecord
	{
		get
		{
			return GameDbf.GuestHero.GetRecord(this.m_guestHeroId);
		}
	}

	// Token: 0x170001CB RID: 459
	// (get) Token: 0x06001763 RID: 5987 RVA: 0x000819BE File Offset: 0x0007FBBE
	[DbfField("UNLOCK_CRITERIA_TEXT")]
	public DbfLocValue UnlockCriteriaText
	{
		get
		{
			return this.m_unlockCriteriaText;
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x06001764 RID: 5988 RVA: 0x000819C6 File Offset: 0x0007FBC6
	[DbfField("COMING_SOON_TEXT")]
	public DbfLocValue ComingSoonText
	{
		get
		{
			return this.m_comingSoonText;
		}
	}

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x06001765 RID: 5989 RVA: 0x000819CE File Offset: 0x0007FBCE
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x06001766 RID: 5990 RVA: 0x000819D6 File Offset: 0x0007FBD6
	[DbfField("CUSTOM_SCENARIO")]
	public int CustomScenario
	{
		get
		{
			return this.m_customScenarioId;
		}
	}

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06001767 RID: 5991 RVA: 0x000819DE File Offset: 0x0007FBDE
	public ScenarioDbfRecord CustomScenarioRecord
	{
		get
		{
			return GameDbf.Scenario.GetRecord(this.m_customScenarioId);
		}
	}

	// Token: 0x06001768 RID: 5992 RVA: 0x000819F0 File Offset: 0x0007FBF0
	public void SetAdventureId(int v)
	{
		this.m_adventureId = v;
	}

	// Token: 0x06001769 RID: 5993 RVA: 0x000819F9 File Offset: 0x0007FBF9
	public void SetWingId(int v)
	{
		this.m_wingId = v;
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x00081A02 File Offset: 0x0007FC02
	public void SetGuestHeroId(int v)
	{
		this.m_guestHeroId = v;
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x00081A0B File Offset: 0x0007FC0B
	public void SetUnlockCriteriaText(DbfLocValue v)
	{
		this.m_unlockCriteriaText = v;
		v.SetDebugInfo(base.ID, "UNLOCK_CRITERIA_TEXT");
	}

	// Token: 0x0600176C RID: 5996 RVA: 0x00081A25 File Offset: 0x0007FC25
	public void SetComingSoonText(DbfLocValue v)
	{
		this.m_comingSoonText = v;
		v.SetDebugInfo(base.ID, "COMING_SOON_TEXT");
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x00081A3F File Offset: 0x0007FC3F
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x0600176E RID: 5998 RVA: 0x00081A48 File Offset: 0x0007FC48
	public void SetCustomScenario(int v)
	{
		this.m_customScenarioId = v;
	}

	// Token: 0x0600176F RID: 5999 RVA: 0x00081A54 File Offset: 0x0007FC54
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1559555090U)
		{
			if (num <= 864600850U)
			{
				if (num != 190718801U)
				{
					if (num == 864600850U)
					{
						if (name == "COMING_SOON_TEXT")
						{
							return this.m_comingSoonText;
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
				if (num == 1559555090U)
				{
					if (name == "WING_ID")
					{
						return this.m_wingId;
					}
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (num <= 3115778841U)
		{
			if (num != 1966695012U)
			{
				if (num == 3115778841U)
				{
					if (name == "CUSTOM_SCENARIO")
					{
						return this.m_customScenarioId;
					}
				}
			}
			else if (name == "GUEST_HERO_ID")
			{
				return this.m_guestHeroId;
			}
		}
		else if (num != 3710150967U)
		{
			if (num == 4214602626U)
			{
				if (name == "SORT_ORDER")
				{
					return this.m_sortOrder;
				}
			}
		}
		else if (name == "UNLOCK_CRITERIA_TEXT")
		{
			return this.m_unlockCriteriaText;
		}
		return null;
	}

	// Token: 0x06001770 RID: 6000 RVA: 0x00081BB8 File Offset: 0x0007FDB8
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1559555090U)
		{
			if (num <= 864600850U)
			{
				if (num != 190718801U)
				{
					if (num != 864600850U)
					{
						return;
					}
					if (!(name == "COMING_SOON_TEXT"))
					{
						return;
					}
					this.m_comingSoonText = (DbfLocValue)val;
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
				if (num != 1559555090U)
				{
					return;
				}
				if (!(name == "WING_ID"))
				{
					return;
				}
				this.m_wingId = (int)val;
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
		else if (num <= 3115778841U)
		{
			if (num != 1966695012U)
			{
				if (num != 3115778841U)
				{
					return;
				}
				if (!(name == "CUSTOM_SCENARIO"))
				{
					return;
				}
				this.m_customScenarioId = (int)val;
				return;
			}
			else
			{
				if (!(name == "GUEST_HERO_ID"))
				{
					return;
				}
				this.m_guestHeroId = (int)val;
				return;
			}
		}
		else if (num != 3710150967U)
		{
			if (num != 4214602626U)
			{
				return;
			}
			if (!(name == "SORT_ORDER"))
			{
				return;
			}
			this.m_sortOrder = (int)val;
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

	// Token: 0x06001771 RID: 6001 RVA: 0x00081D04 File Offset: 0x0007FF04
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1559555090U)
		{
			if (num <= 864600850U)
			{
				if (num != 190718801U)
				{
					if (num == 864600850U)
					{
						if (name == "COMING_SOON_TEXT")
						{
							return typeof(DbfLocValue);
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
				if (num == 1559555090U)
				{
					if (name == "WING_ID")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 3115778841U)
		{
			if (num != 1966695012U)
			{
				if (num == 3115778841U)
				{
					if (name == "CUSTOM_SCENARIO")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "GUEST_HERO_ID")
			{
				return typeof(int);
			}
		}
		else if (num != 3710150967U)
		{
			if (num == 4214602626U)
			{
				if (name == "SORT_ORDER")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "UNLOCK_CRITERIA_TEXT")
		{
			return typeof(DbfLocValue);
		}
		return null;
	}

	// Token: 0x06001772 RID: 6002 RVA: 0x00081E6A File Offset: 0x0008006A
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureGuestHeroesDbfRecords loadRecords = new LoadAdventureGuestHeroesDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001773 RID: 6003 RVA: 0x00081E80 File Offset: 0x00080080
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureGuestHeroesDbfAsset adventureGuestHeroesDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureGuestHeroesDbfAsset)) as AdventureGuestHeroesDbfAsset;
		if (adventureGuestHeroesDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AdventureGuestHeroesDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < adventureGuestHeroesDbfAsset.Records.Count; i++)
		{
			adventureGuestHeroesDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (adventureGuestHeroesDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001774 RID: 6004 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001775 RID: 6005 RVA: 0x00081EFF File Offset: 0x000800FF
	public override void StripUnusedLocales()
	{
		this.m_unlockCriteriaText.StripUnusedLocales();
		this.m_comingSoonText.StripUnusedLocales();
	}

	// Token: 0x04000EF1 RID: 3825
	[SerializeField]
	private int m_adventureId;

	// Token: 0x04000EF2 RID: 3826
	[SerializeField]
	private int m_wingId;

	// Token: 0x04000EF3 RID: 3827
	[SerializeField]
	private int m_guestHeroId;

	// Token: 0x04000EF4 RID: 3828
	[SerializeField]
	private DbfLocValue m_unlockCriteriaText;

	// Token: 0x04000EF5 RID: 3829
	[SerializeField]
	private DbfLocValue m_comingSoonText;

	// Token: 0x04000EF6 RID: 3830
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x04000EF7 RID: 3831
	[SerializeField]
	private int m_customScenarioId;
}
