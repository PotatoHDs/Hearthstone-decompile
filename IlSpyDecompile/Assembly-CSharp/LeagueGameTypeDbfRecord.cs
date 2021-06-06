using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class LeagueGameTypeDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_leagueId;

	[SerializeField]
	private LeagueGameType.FormatType m_formatType;

	[SerializeField]
	private LeagueGameType.BnetGameType m_bnetGameType;

	[DbfField("LEAGUE_ID")]
	public int LeagueId => m_leagueId;

	[DbfField("FORMAT_TYPE")]
	public LeagueGameType.FormatType FormatType => m_formatType;

	[DbfField("BNET_GAME_TYPE")]
	public LeagueGameType.BnetGameType BnetGameType => m_bnetGameType;

	public void SetLeagueId(int v)
	{
		m_leagueId = v;
	}

	public void SetFormatType(LeagueGameType.FormatType v)
	{
		m_formatType = v;
	}

	public void SetBnetGameType(LeagueGameType.BnetGameType v)
	{
		m_bnetGameType = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"LEAGUE_ID" => m_leagueId, 
			"FORMAT_TYPE" => m_formatType, 
			"BNET_GAME_TYPE" => m_bnetGameType, 
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
		case "LEAGUE_ID":
			m_leagueId = (int)val;
			break;
		case "FORMAT_TYPE":
			if (val == null)
			{
				m_formatType = LeagueGameType.FormatType.FT_UNKNOWN;
			}
			else if (val is LeagueGameType.FormatType || val is int)
			{
				m_formatType = (LeagueGameType.FormatType)val;
			}
			else if (val is string)
			{
				m_formatType = LeagueGameType.ParseFormatTypeValue((string)val);
			}
			break;
		case "BNET_GAME_TYPE":
			if (val == null)
			{
				m_bnetGameType = LeagueGameType.BnetGameType.BGT_UNKNOWN;
			}
			else if (val is LeagueGameType.BnetGameType || val is int)
			{
				m_bnetGameType = (LeagueGameType.BnetGameType)val;
			}
			else if (val is string)
			{
				m_bnetGameType = LeagueGameType.ParseBnetGameTypeValue((string)val);
			}
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"LEAGUE_ID" => typeof(int), 
			"FORMAT_TYPE" => typeof(LeagueGameType.FormatType), 
			"BNET_GAME_TYPE" => typeof(LeagueGameType.BnetGameType), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadLeagueGameTypeDbfRecords loadRecords = new LoadLeagueGameTypeDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		LeagueGameTypeDbfAsset leagueGameTypeDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(LeagueGameTypeDbfAsset)) as LeagueGameTypeDbfAsset;
		if (leagueGameTypeDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"LeagueGameTypeDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < leagueGameTypeDbfAsset.Records.Count; i++)
		{
			leagueGameTypeDbfAsset.Records[i].StripUnusedLocales();
		}
		records = leagueGameTypeDbfAsset.Records as List<T>;
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
