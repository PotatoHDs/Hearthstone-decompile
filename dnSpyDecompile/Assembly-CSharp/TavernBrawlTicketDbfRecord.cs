using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000286 RID: 646
[Serializable]
public class TavernBrawlTicketDbfRecord : DbfRecord
{
	// Token: 0x1700048D RID: 1165
	// (get) Token: 0x060020F2 RID: 8434 RVA: 0x000A241E File Offset: 0x000A061E
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x1700048E RID: 1166
	// (get) Token: 0x060020F3 RID: 8435 RVA: 0x000A2426 File Offset: 0x000A0626
	[DbfField("CAN_BE_OWNED")]
	public bool CanBeOwned
	{
		get
		{
			return this.m_canBeOwned;
		}
	}

	// Token: 0x1700048F RID: 1167
	// (get) Token: 0x060020F4 RID: 8436 RVA: 0x000A242E File Offset: 0x000A062E
	[DbfField("CAN_BE_PURCHASED")]
	public bool CanBePurchased
	{
		get
		{
			return this.m_canBePurchased;
		}
	}

	// Token: 0x17000490 RID: 1168
	// (get) Token: 0x060020F5 RID: 8437 RVA: 0x000A2436 File Offset: 0x000A0636
	[DbfField("STORE_NAME")]
	public DbfLocValue StoreName
	{
		get
		{
			return this.m_storeName;
		}
	}

	// Token: 0x060020F6 RID: 8438 RVA: 0x000A243E File Offset: 0x000A063E
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x060020F7 RID: 8439 RVA: 0x000A2447 File Offset: 0x000A0647
	public void SetCanBeOwned(bool v)
	{
		this.m_canBeOwned = v;
	}

	// Token: 0x060020F8 RID: 8440 RVA: 0x000A2450 File Offset: 0x000A0650
	public void SetCanBePurchased(bool v)
	{
		this.m_canBePurchased = v;
	}

	// Token: 0x060020F9 RID: 8441 RVA: 0x000A2459 File Offset: 0x000A0659
	public void SetStoreName(DbfLocValue v)
	{
		this.m_storeName = v;
		v.SetDebugInfo(base.ID, "STORE_NAME");
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x000A2474 File Offset: 0x000A0674
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "NOTE_DESC")
		{
			return this.m_noteDesc;
		}
		if (name == "CAN_BE_OWNED")
		{
			return this.m_canBeOwned;
		}
		if (name == "CAN_BE_PURCHASED")
		{
			return this.m_canBePurchased;
		}
		if (!(name == "STORE_NAME"))
		{
			return null;
		}
		return this.m_storeName;
	}

	// Token: 0x060020FB RID: 8443 RVA: 0x000A24F8 File Offset: 0x000A06F8
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "NOTE_DESC")
		{
			this.m_noteDesc = (string)val;
			return;
		}
		if (name == "CAN_BE_OWNED")
		{
			this.m_canBeOwned = (bool)val;
			return;
		}
		if (name == "CAN_BE_PURCHASED")
		{
			this.m_canBePurchased = (bool)val;
			return;
		}
		if (!(name == "STORE_NAME"))
		{
			return;
		}
		this.m_storeName = (DbfLocValue)val;
	}

	// Token: 0x060020FC RID: 8444 RVA: 0x000A2588 File Offset: 0x000A0788
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "NOTE_DESC")
		{
			return typeof(string);
		}
		if (name == "CAN_BE_OWNED")
		{
			return typeof(bool);
		}
		if (name == "CAN_BE_PURCHASED")
		{
			return typeof(bool);
		}
		if (!(name == "STORE_NAME"))
		{
			return null;
		}
		return typeof(DbfLocValue);
	}

	// Token: 0x060020FD RID: 8445 RVA: 0x000A2610 File Offset: 0x000A0810
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadTavernBrawlTicketDbfRecords loadRecords = new LoadTavernBrawlTicketDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060020FE RID: 8446 RVA: 0x000A2628 File Offset: 0x000A0828
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		TavernBrawlTicketDbfAsset tavernBrawlTicketDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(TavernBrawlTicketDbfAsset)) as TavernBrawlTicketDbfAsset;
		if (tavernBrawlTicketDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("TavernBrawlTicketDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < tavernBrawlTicketDbfAsset.Records.Count; i++)
		{
			tavernBrawlTicketDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (tavernBrawlTicketDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060020FF RID: 8447 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06002100 RID: 8448 RVA: 0x000A26A7 File Offset: 0x000A08A7
	public override void StripUnusedLocales()
	{
		this.m_storeName.StripUnusedLocales();
	}

	// Token: 0x04001261 RID: 4705
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04001262 RID: 4706
	[SerializeField]
	private bool m_canBeOwned;

	// Token: 0x04001263 RID: 4707
	[SerializeField]
	private bool m_canBePurchased;

	// Token: 0x04001264 RID: 4708
	[SerializeField]
	private DbfLocValue m_storeName;
}
