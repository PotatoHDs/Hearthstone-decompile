using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000205 RID: 517
[Serializable]
public class LeagueGameTypeDbfRecord : DbfRecord
{
	// Token: 0x1700033F RID: 831
	// (get) Token: 0x06001C7E RID: 7294 RVA: 0x00093A36 File Offset: 0x00091C36
	[DbfField("LEAGUE_ID")]
	public int LeagueId
	{
		get
		{
			return this.m_leagueId;
		}
	}

	// Token: 0x17000340 RID: 832
	// (get) Token: 0x06001C7F RID: 7295 RVA: 0x00093A3E File Offset: 0x00091C3E
	[DbfField("FORMAT_TYPE")]
	public LeagueGameType.FormatType FormatType
	{
		get
		{
			return this.m_formatType;
		}
	}

	// Token: 0x17000341 RID: 833
	// (get) Token: 0x06001C80 RID: 7296 RVA: 0x00093A46 File Offset: 0x00091C46
	[DbfField("BNET_GAME_TYPE")]
	public LeagueGameType.BnetGameType BnetGameType
	{
		get
		{
			return this.m_bnetGameType;
		}
	}

	// Token: 0x06001C81 RID: 7297 RVA: 0x00093A4E File Offset: 0x00091C4E
	public void SetLeagueId(int v)
	{
		this.m_leagueId = v;
	}

	// Token: 0x06001C82 RID: 7298 RVA: 0x00093A57 File Offset: 0x00091C57
	public void SetFormatType(LeagueGameType.FormatType v)
	{
		this.m_formatType = v;
	}

	// Token: 0x06001C83 RID: 7299 RVA: 0x00093A60 File Offset: 0x00091C60
	public void SetBnetGameType(LeagueGameType.BnetGameType v)
	{
		this.m_bnetGameType = v;
	}

	// Token: 0x06001C84 RID: 7300 RVA: 0x00093A6C File Offset: 0x00091C6C
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
		if (!(name == "BNET_GAME_TYPE"))
		{
			return null;
		}
		return this.m_bnetGameType;
	}

	// Token: 0x06001C85 RID: 7301 RVA: 0x00093AE0 File Offset: 0x00091CE0
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
				if (!(name == "BNET_GAME_TYPE"))
				{
					return;
				}
				if (val == null)
				{
					this.m_bnetGameType = LeagueGameType.BnetGameType.BGT_UNKNOWN;
					return;
				}
				if (val is LeagueGameType.BnetGameType || val is int)
				{
					this.m_bnetGameType = (LeagueGameType.BnetGameType)val;
					return;
				}
				if (val is string)
				{
					this.m_bnetGameType = LeagueGameType.ParseBnetGameTypeValue((string)val);
				}
			}
			else
			{
				if (val == null)
				{
					this.m_formatType = LeagueGameType.FormatType.FT_UNKNOWN;
					return;
				}
				if (val is LeagueGameType.FormatType || val is int)
				{
					this.m_formatType = (LeagueGameType.FormatType)val;
					return;
				}
				if (val is string)
				{
					this.m_formatType = LeagueGameType.ParseFormatTypeValue((string)val);
					return;
				}
			}
			return;
		}
		this.m_leagueId = (int)val;
	}

	// Token: 0x06001C86 RID: 7302 RVA: 0x00093BC0 File Offset: 0x00091DC0
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
			return typeof(LeagueGameType.FormatType);
		}
		if (!(name == "BNET_GAME_TYPE"))
		{
			return null;
		}
		return typeof(LeagueGameType.BnetGameType);
	}

	// Token: 0x06001C87 RID: 7303 RVA: 0x00093C30 File Offset: 0x00091E30
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadLeagueGameTypeDbfRecords loadRecords = new LoadLeagueGameTypeDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001C88 RID: 7304 RVA: 0x00093C48 File Offset: 0x00091E48
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		LeagueGameTypeDbfAsset leagueGameTypeDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(LeagueGameTypeDbfAsset)) as LeagueGameTypeDbfAsset;
		if (leagueGameTypeDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("LeagueGameTypeDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < leagueGameTypeDbfAsset.Records.Count; i++)
		{
			leagueGameTypeDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (leagueGameTypeDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001C89 RID: 7305 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001C8A RID: 7306 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040010F9 RID: 4345
	[SerializeField]
	private int m_leagueId;

	// Token: 0x040010FA RID: 4346
	[SerializeField]
	private LeagueGameType.FormatType m_formatType;

	// Token: 0x040010FB RID: 4347
	[SerializeField]
	private LeagueGameType.BnetGameType m_bnetGameType;
}
