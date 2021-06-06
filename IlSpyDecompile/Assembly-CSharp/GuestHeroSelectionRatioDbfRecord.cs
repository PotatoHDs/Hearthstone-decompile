using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class GuestHeroSelectionRatioDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_pvpdrSeasonId;

	[SerializeField]
	private int m_guestHeroId;

	[SerializeField]
	private double m_weight;

	[DbfField("PVPDR_SEASON_ID")]
	public int PvpdrSeasonId => m_pvpdrSeasonId;

	[DbfField("GUEST_HERO_ID")]
	public int GuestHeroId => m_guestHeroId;

	public GuestHeroDbfRecord GuestHeroRecord => GameDbf.GuestHero.GetRecord(m_guestHeroId);

	[DbfField("WEIGHT")]
	public double Weight => m_weight;

	public void SetPvpdrSeasonId(int v)
	{
		m_pvpdrSeasonId = v;
	}

	public void SetGuestHeroId(int v)
	{
		m_guestHeroId = v;
	}

	public void SetWeight(double v)
	{
		m_weight = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"PVPDR_SEASON_ID" => m_pvpdrSeasonId, 
			"GUEST_HERO_ID" => m_guestHeroId, 
			"WEIGHT" => m_weight, 
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
		case "PVPDR_SEASON_ID":
			m_pvpdrSeasonId = (int)val;
			break;
		case "GUEST_HERO_ID":
			m_guestHeroId = (int)val;
			break;
		case "WEIGHT":
			m_weight = (double)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"PVPDR_SEASON_ID" => typeof(int), 
			"GUEST_HERO_ID" => typeof(int), 
			"WEIGHT" => typeof(double), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadGuestHeroSelectionRatioDbfRecords loadRecords = new LoadGuestHeroSelectionRatioDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		GuestHeroSelectionRatioDbfAsset guestHeroSelectionRatioDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(GuestHeroSelectionRatioDbfAsset)) as GuestHeroSelectionRatioDbfAsset;
		if (guestHeroSelectionRatioDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"GuestHeroSelectionRatioDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < guestHeroSelectionRatioDbfAsset.Records.Count; i++)
		{
			guestHeroSelectionRatioDbfAsset.Records[i].StripUnusedLocales();
		}
		records = guestHeroSelectionRatioDbfAsset.Records as List<T>;
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
