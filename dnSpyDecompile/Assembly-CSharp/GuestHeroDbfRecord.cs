using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001F6 RID: 502
[Serializable]
public class GuestHeroDbfRecord : DbfRecord
{
	// Token: 0x17000315 RID: 789
	// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x00091D06 File Offset: 0x0008FF06
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x17000316 RID: 790
	// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x00091D0E File Offset: 0x0008FF0E
	public CardDbfRecord CardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_cardId);
		}
	}

	// Token: 0x17000317 RID: 791
	// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x00091D20 File Offset: 0x0008FF20
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000318 RID: 792
	// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x00091D28 File Offset: 0x0008FF28
	[DbfField("FLAVOR_TEXT")]
	public DbfLocValue FlavorText
	{
		get
		{
			return this.m_flavorText;
		}
	}

	// Token: 0x17000319 RID: 793
	// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00091D30 File Offset: 0x0008FF30
	[DbfField("UNLOCK_EVENT")]
	public string UnlockEvent
	{
		get
		{
			return this.m_unlockEvent;
		}
	}

	// Token: 0x06001BF8 RID: 7160 RVA: 0x00091D38 File Offset: 0x0008FF38
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x06001BF9 RID: 7161 RVA: 0x00091D41 File Offset: 0x0008FF41
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x06001BFA RID: 7162 RVA: 0x00091D5B File Offset: 0x0008FF5B
	public void SetFlavorText(DbfLocValue v)
	{
		this.m_flavorText = v;
		v.SetDebugInfo(base.ID, "FLAVOR_TEXT");
	}

	// Token: 0x06001BFB RID: 7163 RVA: 0x00091D75 File Offset: 0x0008FF75
	public void SetUnlockEvent(string v)
	{
		this.m_unlockEvent = v;
	}

	// Token: 0x06001BFC RID: 7164 RVA: 0x00091D80 File Offset: 0x0008FF80
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "CARD_ID")
		{
			return this.m_cardId;
		}
		if (name == "NAME")
		{
			return this.m_name;
		}
		if (name == "FLAVOR_TEXT")
		{
			return this.m_flavorText;
		}
		if (!(name == "UNLOCK_EVENT"))
		{
			return null;
		}
		return this.m_unlockEvent;
	}

	// Token: 0x06001BFD RID: 7165 RVA: 0x00091E00 File Offset: 0x00090000
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
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
		if (name == "FLAVOR_TEXT")
		{
			this.m_flavorText = (DbfLocValue)val;
			return;
		}
		if (!(name == "UNLOCK_EVENT"))
		{
			return;
		}
		this.m_unlockEvent = (string)val;
	}

	// Token: 0x06001BFE RID: 7166 RVA: 0x00091E90 File Offset: 0x00090090
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "CARD_ID")
		{
			return typeof(int);
		}
		if (name == "NAME")
		{
			return typeof(DbfLocValue);
		}
		if (name == "FLAVOR_TEXT")
		{
			return typeof(DbfLocValue);
		}
		if (!(name == "UNLOCK_EVENT"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x06001BFF RID: 7167 RVA: 0x00091F18 File Offset: 0x00090118
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadGuestHeroDbfRecords loadRecords = new LoadGuestHeroDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001C00 RID: 7168 RVA: 0x00091F30 File Offset: 0x00090130
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		GuestHeroDbfAsset guestHeroDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(GuestHeroDbfAsset)) as GuestHeroDbfAsset;
		if (guestHeroDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("GuestHeroDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < guestHeroDbfAsset.Records.Count; i++)
		{
			guestHeroDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (guestHeroDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001C01 RID: 7169 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001C02 RID: 7170 RVA: 0x00091FAF File Offset: 0x000901AF
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_flavorText.StripUnusedLocales();
	}

	// Token: 0x040010CC RID: 4300
	[SerializeField]
	private int m_cardId;

	// Token: 0x040010CD RID: 4301
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x040010CE RID: 4302
	[SerializeField]
	private DbfLocValue m_flavorText;

	// Token: 0x040010CF RID: 4303
	[SerializeField]
	private string m_unlockEvent = "none";
}
