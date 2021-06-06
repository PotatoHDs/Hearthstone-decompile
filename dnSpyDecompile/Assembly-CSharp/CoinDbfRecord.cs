using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001C2 RID: 450
[Serializable]
public class CoinDbfRecord : DbfRecord
{
	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06001A27 RID: 6695 RVA: 0x0008AC02 File Offset: 0x00088E02
	[DbfField("ENABLED")]
	public bool Enabled
	{
		get
		{
			return this.m_enabled;
		}
	}

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x06001A28 RID: 6696 RVA: 0x0008AC0A File Offset: 0x00088E0A
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x06001A29 RID: 6697 RVA: 0x0008AC12 File Offset: 0x00088E12
	public CardDbfRecord CardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_cardId);
		}
	}

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06001A2A RID: 6698 RVA: 0x0008AC24 File Offset: 0x00088E24
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06001A2B RID: 6699 RVA: 0x0008AC2C File Offset: 0x00088E2C
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x06001A2C RID: 6700 RVA: 0x0008AC34 File Offset: 0x00088E34
	public void SetEnabled(bool v)
	{
		this.m_enabled = v;
	}

	// Token: 0x06001A2D RID: 6701 RVA: 0x0008AC3D File Offset: 0x00088E3D
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x06001A2E RID: 6702 RVA: 0x0008AC46 File Offset: 0x00088E46
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x06001A2F RID: 6703 RVA: 0x0008AC60 File Offset: 0x00088E60
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x06001A30 RID: 6704 RVA: 0x0008AC7C File Offset: 0x00088E7C
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "ENABLED")
		{
			return this.m_enabled;
		}
		if (name == "CARD_ID")
		{
			return this.m_cardId;
		}
		if (name == "NAME")
		{
			return this.m_name;
		}
		if (!(name == "DESCRIPTION"))
		{
			return null;
		}
		return this.m_description;
	}

	// Token: 0x06001A31 RID: 6705 RVA: 0x0008AD00 File Offset: 0x00088F00
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "ENABLED")
		{
			this.m_enabled = (bool)val;
			return;
		}
		if (name == "CARD_ID")
		{
			this.m_cardId = (int)val;
			return;
		}
		if (name == "NAME")
		{
			this.m_name = (DbfLocValue)val;
			return;
		}
		if (!(name == "DESCRIPTION"))
		{
			return;
		}
		this.m_description = (DbfLocValue)val;
	}

	// Token: 0x06001A32 RID: 6706 RVA: 0x0008AD90 File Offset: 0x00088F90
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "ENABLED")
		{
			return typeof(bool);
		}
		if (name == "CARD_ID")
		{
			return typeof(int);
		}
		if (name == "NAME")
		{
			return typeof(DbfLocValue);
		}
		if (!(name == "DESCRIPTION"))
		{
			return null;
		}
		return typeof(DbfLocValue);
	}

	// Token: 0x06001A33 RID: 6707 RVA: 0x0008AE18 File Offset: 0x00089018
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCoinDbfRecords loadRecords = new LoadCoinDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001A34 RID: 6708 RVA: 0x0008AE30 File Offset: 0x00089030
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CoinDbfAsset coinDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CoinDbfAsset)) as CoinDbfAsset;
		if (coinDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CoinDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < coinDbfAsset.Records.Count; i++)
		{
			coinDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (coinDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001A35 RID: 6709 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001A36 RID: 6710 RVA: 0x0008AEAF File Offset: 0x000890AF
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_description.StripUnusedLocales();
	}

	// Token: 0x04000FD6 RID: 4054
	[SerializeField]
	private bool m_enabled;

	// Token: 0x04000FD7 RID: 4055
	[SerializeField]
	private int m_cardId;

	// Token: 0x04000FD8 RID: 4056
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x04000FD9 RID: 4057
	[SerializeField]
	private DbfLocValue m_description;
}
