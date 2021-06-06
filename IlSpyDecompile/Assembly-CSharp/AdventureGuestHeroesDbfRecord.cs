using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class AdventureGuestHeroesDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_adventureId;

	[SerializeField]
	private int m_wingId;

	[SerializeField]
	private int m_guestHeroId;

	[SerializeField]
	private DbfLocValue m_unlockCriteriaText;

	[SerializeField]
	private DbfLocValue m_comingSoonText;

	[SerializeField]
	private int m_sortOrder;

	[SerializeField]
	private int m_customScenarioId;

	[DbfField("ADVENTURE_ID")]
	public int AdventureId => m_adventureId;

	[DbfField("WING_ID")]
	public int WingId => m_wingId;

	public WingDbfRecord WingRecord => GameDbf.Wing.GetRecord(m_wingId);

	[DbfField("GUEST_HERO_ID")]
	public int GuestHeroId => m_guestHeroId;

	public GuestHeroDbfRecord GuestHeroRecord => GameDbf.GuestHero.GetRecord(m_guestHeroId);

	[DbfField("UNLOCK_CRITERIA_TEXT")]
	public DbfLocValue UnlockCriteriaText => m_unlockCriteriaText;

	[DbfField("COMING_SOON_TEXT")]
	public DbfLocValue ComingSoonText => m_comingSoonText;

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	[DbfField("CUSTOM_SCENARIO")]
	public int CustomScenario => m_customScenarioId;

	public ScenarioDbfRecord CustomScenarioRecord => GameDbf.Scenario.GetRecord(m_customScenarioId);

	public void SetAdventureId(int v)
	{
		m_adventureId = v;
	}

	public void SetWingId(int v)
	{
		m_wingId = v;
	}

	public void SetGuestHeroId(int v)
	{
		m_guestHeroId = v;
	}

	public void SetUnlockCriteriaText(DbfLocValue v)
	{
		m_unlockCriteriaText = v;
		v.SetDebugInfo(base.ID, "UNLOCK_CRITERIA_TEXT");
	}

	public void SetComingSoonText(DbfLocValue v)
	{
		m_comingSoonText = v;
		v.SetDebugInfo(base.ID, "COMING_SOON_TEXT");
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public void SetCustomScenario(int v)
	{
		m_customScenarioId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"ADVENTURE_ID" => m_adventureId, 
			"WING_ID" => m_wingId, 
			"GUEST_HERO_ID" => m_guestHeroId, 
			"UNLOCK_CRITERIA_TEXT" => m_unlockCriteriaText, 
			"COMING_SOON_TEXT" => m_comingSoonText, 
			"SORT_ORDER" => m_sortOrder, 
			"CUSTOM_SCENARIO" => m_customScenarioId, 
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
		case "ADVENTURE_ID":
			m_adventureId = (int)val;
			break;
		case "WING_ID":
			m_wingId = (int)val;
			break;
		case "GUEST_HERO_ID":
			m_guestHeroId = (int)val;
			break;
		case "UNLOCK_CRITERIA_TEXT":
			m_unlockCriteriaText = (DbfLocValue)val;
			break;
		case "COMING_SOON_TEXT":
			m_comingSoonText = (DbfLocValue)val;
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		case "CUSTOM_SCENARIO":
			m_customScenarioId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"ADVENTURE_ID" => typeof(int), 
			"WING_ID" => typeof(int), 
			"GUEST_HERO_ID" => typeof(int), 
			"UNLOCK_CRITERIA_TEXT" => typeof(DbfLocValue), 
			"COMING_SOON_TEXT" => typeof(DbfLocValue), 
			"SORT_ORDER" => typeof(int), 
			"CUSTOM_SCENARIO" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureGuestHeroesDbfRecords loadRecords = new LoadAdventureGuestHeroesDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureGuestHeroesDbfAsset adventureGuestHeroesDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureGuestHeroesDbfAsset)) as AdventureGuestHeroesDbfAsset;
		if (adventureGuestHeroesDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"AdventureGuestHeroesDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < adventureGuestHeroesDbfAsset.Records.Count; i++)
		{
			adventureGuestHeroesDbfAsset.Records[i].StripUnusedLocales();
		}
		records = adventureGuestHeroesDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_unlockCriteriaText.StripUnusedLocales();
		m_comingSoonText.StripUnusedLocales();
	}
}
