using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class ScenarioGuestHeroesDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_scenarioId;

	[SerializeField]
	private int m_guestHeroId;

	[SerializeField]
	private int m_sortOrder;

	[DbfField("SCENARIO_ID")]
	public int ScenarioId => m_scenarioId;

	[DbfField("GUEST_HERO_ID")]
	public int GuestHeroId => m_guestHeroId;

	public GuestHeroDbfRecord GuestHeroRecord => GameDbf.GuestHero.GetRecord(m_guestHeroId);

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	public void SetScenarioId(int v)
	{
		m_scenarioId = v;
	}

	public void SetGuestHeroId(int v)
	{
		m_guestHeroId = v;
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"SCENARIO_ID" => m_scenarioId, 
			"GUEST_HERO_ID" => m_guestHeroId, 
			"SORT_ORDER" => m_sortOrder, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "ID":
			SetID((int)val);
			break;
		case "SCENARIO_ID":
			m_scenarioId = (int)val;
			break;
		case "GUEST_HERO_ID":
			m_guestHeroId = (int)val;
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"SCENARIO_ID" => typeof(int), 
			"GUEST_HERO_ID" => typeof(int), 
			"SORT_ORDER" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadScenarioGuestHeroesDbfRecords loadRecords = new LoadScenarioGuestHeroesDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ScenarioGuestHeroesDbfAsset scenarioGuestHeroesDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ScenarioGuestHeroesDbfAsset)) as ScenarioGuestHeroesDbfAsset;
		if (scenarioGuestHeroesDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"ScenarioGuestHeroesDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < scenarioGuestHeroesDbfAsset.Records.Count; i++)
		{
			scenarioGuestHeroesDbfAsset.Records[i].StripUnusedLocales();
		}
		records = scenarioGuestHeroesDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
	}
}
