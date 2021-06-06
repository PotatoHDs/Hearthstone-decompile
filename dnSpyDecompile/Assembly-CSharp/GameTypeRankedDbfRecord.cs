using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001F0 RID: 496
[Serializable]
public class GameTypeRankedDbfRecord : DbfRecord
{
	// Token: 0x17000309 RID: 777
	// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x00091102 File Offset: 0x0008F302
	[DbfField("LEAGUE_ID")]
	public int LeagueId
	{
		get
		{
			return this.m_leagueId;
		}
	}

	// Token: 0x1700030A RID: 778
	// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x0009110A File Offset: 0x0008F30A
	[DbfField("FORMAT_TYPE")]
	public GameTypeRanked.FormatType FormatType
	{
		get
		{
			return this.m_formatType;
		}
	}

	// Token: 0x1700030B RID: 779
	// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x00091112 File Offset: 0x0008F312
	[DbfField("GAME_TYPE")]
	public GameTypeRanked.GameType GameType
	{
		get
		{
			return this.m_gameType;
		}
	}

	// Token: 0x06001BC6 RID: 7110 RVA: 0x0009111A File Offset: 0x0008F31A
	public void SetLeagueId(int v)
	{
		this.m_leagueId = v;
	}

	// Token: 0x06001BC7 RID: 7111 RVA: 0x00091123 File Offset: 0x0008F323
	public void SetFormatType(GameTypeRanked.FormatType v)
	{
		this.m_formatType = v;
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x0009112C File Offset: 0x0008F32C
	public void SetGameType(GameTypeRanked.GameType v)
	{
		this.m_gameType = v;
	}

	// Token: 0x06001BC9 RID: 7113 RVA: 0x00091138 File Offset: 0x0008F338
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "LEAGUE_ID")
		{
			return this.m_leagueId;
		}
		if (name == "FORMAT_TYPE")
		{
			return this.m_formatType;
		}
		if (!(name == "GAME_TYPE"))
		{
			return null;
		}
		return this.m_gameType;
	}

	// Token: 0x06001BCA RID: 7114 RVA: 0x000911AC File Offset: 0x0008F3AC
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (!(name == "LEAGUE_ID"))
		{
			if (!(name == "FORMAT_TYPE"))
			{
				if (!(name == "GAME_TYPE"))
				{
					return;
				}
				if (val == null)
				{
					this.m_gameType = GameTypeRanked.GameType.BGT_UNKNOWN;
					return;
				}
				if (val is GameTypeRanked.GameType || val is int)
				{
					this.m_gameType = (GameTypeRanked.GameType)val;
					return;
				}
				if (val is string)
				{
					this.m_gameType = GameTypeRanked.ParseGameTypeValue((string)val);
				}
			}
			else
			{
				if (val == null)
				{
					this.m_formatType = GameTypeRanked.FormatType.FT_UNKNOWN;
					return;
				}
				if (val is GameTypeRanked.FormatType || val is int)
				{
					this.m_formatType = (GameTypeRanked.FormatType)val;
					return;
				}
				if (val is string)
				{
					this.m_formatType = GameTypeRanked.ParseFormatTypeValue((string)val);
					return;
				}
			}
			return;
		}
		this.m_leagueId = (int)val;
	}

	// Token: 0x06001BCB RID: 7115 RVA: 0x0009128C File Offset: 0x0008F48C
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "LEAGUE_ID")
		{
			return typeof(int);
		}
		if (name == "FORMAT_TYPE")
		{
			return typeof(GameTypeRanked.FormatType);
		}
		if (!(name == "GAME_TYPE"))
		{
			return null;
		}
		return typeof(GameTypeRanked.GameType);
	}

	// Token: 0x06001BCC RID: 7116 RVA: 0x000912FC File Offset: 0x0008F4FC
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadGameTypeRankedDbfRecords loadRecords = new LoadGameTypeRankedDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001BCD RID: 7117 RVA: 0x00091314 File Offset: 0x0008F514
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		GameTypeRankedDbfAsset gameTypeRankedDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(GameTypeRankedDbfAsset)) as GameTypeRankedDbfAsset;
		if (gameTypeRankedDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("GameTypeRankedDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < gameTypeRankedDbfAsset.Records.Count; i++)
		{
			gameTypeRankedDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (gameTypeRankedDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001BCE RID: 7118 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001BCF RID: 7119 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040010BC RID: 4284
	[SerializeField]
	private int m_leagueId;

	// Token: 0x040010BD RID: 4285
	[SerializeField]
	private GameTypeRanked.FormatType m_formatType = GameTypeRanked.ParseFormatTypeValue("ft_unknown");

	// Token: 0x040010BE RID: 4286
	[SerializeField]
	private GameTypeRanked.GameType m_gameType = GameTypeRanked.ParseGameTypeValue("bgt_unknown");
}
