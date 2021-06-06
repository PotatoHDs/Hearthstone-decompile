using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001F9 RID: 505
[Serializable]
public class GuestHeroSelectionRatioDbfRecord : DbfRecord
{
	// Token: 0x1700031A RID: 794
	// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00092076 File Offset: 0x00090276
	[DbfField("PVPDR_SEASON_ID")]
	public int PvpdrSeasonId
	{
		get
		{
			return this.m_pvpdrSeasonId;
		}
	}

	// Token: 0x1700031B RID: 795
	// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0009207E File Offset: 0x0009027E
	[DbfField("GUEST_HERO_ID")]
	public int GuestHeroId
	{
		get
		{
			return this.m_guestHeroId;
		}
	}

	// Token: 0x1700031C RID: 796
	// (get) Token: 0x06001C0A RID: 7178 RVA: 0x00092086 File Offset: 0x00090286
	public GuestHeroDbfRecord GuestHeroRecord
	{
		get
		{
			return GameDbf.GuestHero.GetRecord(this.m_guestHeroId);
		}
	}

	// Token: 0x1700031D RID: 797
	// (get) Token: 0x06001C0B RID: 7179 RVA: 0x00092098 File Offset: 0x00090298
	[DbfField("WEIGHT")]
	public double Weight
	{
		get
		{
			return this.m_weight;
		}
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x000920A0 File Offset: 0x000902A0
	public void SetPvpdrSeasonId(int v)
	{
		this.m_pvpdrSeasonId = v;
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x000920A9 File Offset: 0x000902A9
	public void SetGuestHeroId(int v)
	{
		this.m_guestHeroId = v;
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x000920B2 File Offset: 0x000902B2
	public void SetWeight(double v)
	{
		this.m_weight = v;
	}

	// Token: 0x06001C0F RID: 7183 RVA: 0x000920BC File Offset: 0x000902BC
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "PVPDR_SEASON_ID")
		{
			return this.m_pvpdrSeasonId;
		}
		if (name == "GUEST_HERO_ID")
		{
			return this.m_guestHeroId;
		}
		if (!(name == "WEIGHT"))
		{
			return null;
		}
		return this.m_weight;
	}

	// Token: 0x06001C10 RID: 7184 RVA: 0x00092130 File Offset: 0x00090330
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "PVPDR_SEASON_ID")
		{
			this.m_pvpdrSeasonId = (int)val;
			return;
		}
		if (name == "GUEST_HERO_ID")
		{
			this.m_guestHeroId = (int)val;
			return;
		}
		if (!(name == "WEIGHT"))
		{
			return;
		}
		this.m_weight = (double)val;
	}

	// Token: 0x06001C11 RID: 7185 RVA: 0x000921A8 File Offset: 0x000903A8
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "PVPDR_SEASON_ID")
		{
			return typeof(int);
		}
		if (name == "GUEST_HERO_ID")
		{
			return typeof(int);
		}
		if (!(name == "WEIGHT"))
		{
			return null;
		}
		return typeof(double);
	}

	// Token: 0x06001C12 RID: 7186 RVA: 0x00092218 File Offset: 0x00090418
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadGuestHeroSelectionRatioDbfRecords loadRecords = new LoadGuestHeroSelectionRatioDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001C13 RID: 7187 RVA: 0x00092230 File Offset: 0x00090430
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		GuestHeroSelectionRatioDbfAsset guestHeroSelectionRatioDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(GuestHeroSelectionRatioDbfAsset)) as GuestHeroSelectionRatioDbfAsset;
		if (guestHeroSelectionRatioDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("GuestHeroSelectionRatioDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < guestHeroSelectionRatioDbfAsset.Records.Count; i++)
		{
			guestHeroSelectionRatioDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (guestHeroSelectionRatioDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001C15 RID: 7189 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040010D2 RID: 4306
	[SerializeField]
	private int m_pvpdrSeasonId;

	// Token: 0x040010D3 RID: 4307
	[SerializeField]
	private int m_guestHeroId;

	// Token: 0x040010D4 RID: 4308
	[SerializeField]
	private double m_weight;
}
