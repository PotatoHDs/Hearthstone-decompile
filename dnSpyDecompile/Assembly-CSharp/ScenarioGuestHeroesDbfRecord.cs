using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000268 RID: 616
[Serializable]
public class ScenarioGuestHeroesDbfRecord : DbfRecord
{
	// Token: 0x17000460 RID: 1120
	// (get) Token: 0x06002025 RID: 8229 RVA: 0x000A0136 File Offset: 0x0009E336
	[DbfField("SCENARIO_ID")]
	public int ScenarioId
	{
		get
		{
			return this.m_scenarioId;
		}
	}

	// Token: 0x17000461 RID: 1121
	// (get) Token: 0x06002026 RID: 8230 RVA: 0x000A013E File Offset: 0x0009E33E
	[DbfField("GUEST_HERO_ID")]
	public int GuestHeroId
	{
		get
		{
			return this.m_guestHeroId;
		}
	}

	// Token: 0x17000462 RID: 1122
	// (get) Token: 0x06002027 RID: 8231 RVA: 0x000A0146 File Offset: 0x0009E346
	public GuestHeroDbfRecord GuestHeroRecord
	{
		get
		{
			return GameDbf.GuestHero.GetRecord(this.m_guestHeroId);
		}
	}

	// Token: 0x17000463 RID: 1123
	// (get) Token: 0x06002028 RID: 8232 RVA: 0x000A0158 File Offset: 0x0009E358
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x06002029 RID: 8233 RVA: 0x000A0160 File Offset: 0x0009E360
	public void SetScenarioId(int v)
	{
		this.m_scenarioId = v;
	}

	// Token: 0x0600202A RID: 8234 RVA: 0x000A0169 File Offset: 0x0009E369
	public void SetGuestHeroId(int v)
	{
		this.m_guestHeroId = v;
	}

	// Token: 0x0600202B RID: 8235 RVA: 0x000A0172 File Offset: 0x0009E372
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x0600202C RID: 8236 RVA: 0x000A017C File Offset: 0x0009E37C
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "SCENARIO_ID")
		{
			return this.m_scenarioId;
		}
		if (name == "GUEST_HERO_ID")
		{
			return this.m_guestHeroId;
		}
		if (!(name == "SORT_ORDER"))
		{
			return null;
		}
		return this.m_sortOrder;
	}

	// Token: 0x0600202D RID: 8237 RVA: 0x000A01F0 File Offset: 0x0009E3F0
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "SCENARIO_ID")
		{
			this.m_scenarioId = (int)val;
			return;
		}
		if (name == "GUEST_HERO_ID")
		{
			this.m_guestHeroId = (int)val;
			return;
		}
		if (!(name == "SORT_ORDER"))
		{
			return;
		}
		this.m_sortOrder = (int)val;
	}

	// Token: 0x0600202E RID: 8238 RVA: 0x000A0268 File Offset: 0x0009E468
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "SCENARIO_ID")
		{
			return typeof(int);
		}
		if (name == "GUEST_HERO_ID")
		{
			return typeof(int);
		}
		if (!(name == "SORT_ORDER"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x0600202F RID: 8239 RVA: 0x000A02D8 File Offset: 0x0009E4D8
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadScenarioGuestHeroesDbfRecords loadRecords = new LoadScenarioGuestHeroesDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06002030 RID: 8240 RVA: 0x000A02F0 File Offset: 0x0009E4F0
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ScenarioGuestHeroesDbfAsset scenarioGuestHeroesDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ScenarioGuestHeroesDbfAsset)) as ScenarioGuestHeroesDbfAsset;
		if (scenarioGuestHeroesDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ScenarioGuestHeroesDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < scenarioGuestHeroesDbfAsset.Records.Count; i++)
		{
			scenarioGuestHeroesDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (scenarioGuestHeroesDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06002031 RID: 8241 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06002032 RID: 8242 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001228 RID: 4648
	[SerializeField]
	private int m_scenarioId;

	// Token: 0x04001229 RID: 4649
	[SerializeField]
	private int m_guestHeroId;

	// Token: 0x0400122A RID: 4650
	[SerializeField]
	private int m_sortOrder;
}
