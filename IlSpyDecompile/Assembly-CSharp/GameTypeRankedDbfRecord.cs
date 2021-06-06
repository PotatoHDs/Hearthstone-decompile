using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class GameTypeRankedDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_leagueId;

	[SerializeField]
	private GameTypeRanked.FormatType m_formatType = GameTypeRanked.ParseFormatTypeValue("ft_unknown");

	[SerializeField]
	private GameTypeRanked.GameType m_gameType = GameTypeRanked.ParseGameTypeValue("bgt_unknown");

	[DbfField("LEAGUE_ID")]
	public int LeagueId => m_leagueId;

	[DbfField("FORMAT_TYPE")]
	public GameTypeRanked.FormatType FormatType => m_formatType;

	[DbfField("GAME_TYPE")]
	public GameTypeRanked.GameType GameType => m_gameType;

	public void SetLeagueId(int v)
	{
		m_leagueId = v;
	}

	public void SetFormatType(GameTypeRanked.FormatType v)
	{
		m_formatType = v;
	}

	public void SetGameType(GameTypeRanked.GameType v)
	{
		m_gameType = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"LEAGUE_ID" => m_leagueId, 
			"FORMAT_TYPE" => m_formatType, 
			"GAME_TYPE" => m_gameType, 
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
				m_formatType = GameTypeRanked.FormatType.FT_UNKNOWN;
			}
			else if (val is GameTypeRanked.FormatType || val is int)
			{
				m_formatType = (GameTypeRanked.FormatType)val;
			}
			else if (val is string)
			{
				m_formatType = GameTypeRanked.ParseFormatTypeValue((string)val);
			}
			break;
		case "GAME_TYPE":
			if (val == null)
			{
				m_gameType = GameTypeRanked.GameType.BGT_UNKNOWN;
			}
			else if (val is GameTypeRanked.GameType || val is int)
			{
				m_gameType = (GameTypeRanked.GameType)val;
			}
			else if (val is string)
			{
				m_gameType = GameTypeRanked.ParseGameTypeValue((string)val);
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
			"FORMAT_TYPE" => typeof(GameTypeRanked.FormatType), 
			"GAME_TYPE" => typeof(GameTypeRanked.GameType), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadGameTypeRankedDbfRecords loadRecords = new LoadGameTypeRankedDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		GameTypeRankedDbfAsset gameTypeRankedDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(GameTypeRankedDbfAsset)) as GameTypeRankedDbfAsset;
		if (gameTypeRankedDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"GameTypeRankedDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < gameTypeRankedDbfAsset.Records.Count; i++)
		{
			gameTypeRankedDbfAsset.Records[i].StripUnusedLocales();
		}
		records = gameTypeRankedDbfAsset.Records as List<T>;
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
