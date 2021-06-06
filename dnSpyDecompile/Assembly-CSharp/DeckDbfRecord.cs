using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001CB RID: 459
[Serializable]
public class DeckDbfRecord : DbfRecord
{
	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x06001A64 RID: 6756 RVA: 0x0008B58E File Offset: 0x0008978E
	[DbfField("NOTE_NAME")]
	public string NoteName
	{
		get
		{
			return this.m_noteName;
		}
	}

	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x06001A65 RID: 6757 RVA: 0x0008B596 File Offset: 0x00089796
	[DbfField("TOP_CARD_ID")]
	public int TopCardId
	{
		get
		{
			return this.m_topCardId;
		}
	}

	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x06001A66 RID: 6758 RVA: 0x0008B59E File Offset: 0x0008979E
	public DeckCardDbfRecord TopCardRecord
	{
		get
		{
			return GameDbf.DeckCard.GetRecord(this.m_topCardId);
		}
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x06001A67 RID: 6759 RVA: 0x0008B5B0 File Offset: 0x000897B0
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0008B5B8 File Offset: 0x000897B8
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0008B5C0 File Offset: 0x000897C0
	[DbfField("ALT_DESCRIPTION")]
	public DbfLocValue AltDescription
	{
		get
		{
			return this.m_altDescription;
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0008B5C8 File Offset: 0x000897C8
	[Obsolete("no longer used")]
	[DbfField("PRECON_CLASS")]
	public int PreconClass
	{
		get
		{
			return this.m_preconClass;
		}
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x06001A6B RID: 6763 RVA: 0x0008B5D0 File Offset: 0x000897D0
	public List<DeckCardDbfRecord> Cards
	{
		get
		{
			return GameDbf.DeckCard.GetRecords((DeckCardDbfRecord r) => r.DeckId == base.ID, -1);
		}
	}

	// Token: 0x06001A6C RID: 6764 RVA: 0x0008B5E9 File Offset: 0x000897E9
	public void SetNoteName(string v)
	{
		this.m_noteName = v;
	}

	// Token: 0x06001A6D RID: 6765 RVA: 0x0008B5F2 File Offset: 0x000897F2
	public void SetTopCardId(int v)
	{
		this.m_topCardId = v;
	}

	// Token: 0x06001A6E RID: 6766 RVA: 0x0008B5FB File Offset: 0x000897FB
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x06001A6F RID: 6767 RVA: 0x0008B615 File Offset: 0x00089815
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x06001A70 RID: 6768 RVA: 0x0008B62F File Offset: 0x0008982F
	public void SetAltDescription(DbfLocValue v)
	{
		this.m_altDescription = v;
		v.SetDebugInfo(base.ID, "ALT_DESCRIPTION");
	}

	// Token: 0x06001A71 RID: 6769 RVA: 0x0008B649 File Offset: 0x00089849
	[Obsolete("no longer used")]
	public void SetPreconClass(int v)
	{
		this.m_preconClass = v;
	}

	// Token: 0x06001A72 RID: 6770 RVA: 0x0008B654 File Offset: 0x00089854
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 1103584457U)
			{
				if (num != 1387956774U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "NAME")
				{
					return this.m_name;
				}
			}
			else if (name == "DESCRIPTION")
			{
				return this.m_description;
			}
		}
		else if (num <= 1927439553U)
		{
			if (num != 1629023597U)
			{
				if (num == 1927439553U)
				{
					if (name == "TOP_CARD_ID")
					{
						return this.m_topCardId;
					}
				}
			}
			else if (name == "ALT_DESCRIPTION")
			{
				return this.m_altDescription;
			}
		}
		else if (num != 2485258469U)
		{
			if (num == 2986314849U)
			{
				if (name == "PRECON_CLASS")
				{
					return this.m_preconClass;
				}
			}
		}
		else if (name == "NOTE_NAME")
		{
			return this.m_noteName;
		}
		return null;
	}

	// Token: 0x06001A73 RID: 6771 RVA: 0x0008B774 File Offset: 0x00089974
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 1103584457U)
			{
				if (num != 1387956774U)
				{
					if (num != 1458105184U)
					{
						return;
					}
					if (!(name == "ID"))
					{
						return;
					}
					base.SetID((int)val);
					return;
				}
				else
				{
					if (!(name == "NAME"))
					{
						return;
					}
					this.m_name = (DbfLocValue)val;
					return;
				}
			}
			else
			{
				if (!(name == "DESCRIPTION"))
				{
					return;
				}
				this.m_description = (DbfLocValue)val;
				return;
			}
		}
		else if (num <= 1927439553U)
		{
			if (num != 1629023597U)
			{
				if (num != 1927439553U)
				{
					return;
				}
				if (!(name == "TOP_CARD_ID"))
				{
					return;
				}
				this.m_topCardId = (int)val;
				return;
			}
			else
			{
				if (!(name == "ALT_DESCRIPTION"))
				{
					return;
				}
				this.m_altDescription = (DbfLocValue)val;
				return;
			}
		}
		else if (num != 2485258469U)
		{
			if (num != 2986314849U)
			{
				return;
			}
			if (!(name == "PRECON_CLASS"))
			{
				return;
			}
			this.m_preconClass = (int)val;
			return;
		}
		else
		{
			if (!(name == "NOTE_NAME"))
			{
				return;
			}
			this.m_noteName = (string)val;
			return;
		}
	}

	// Token: 0x06001A74 RID: 6772 RVA: 0x0008B890 File Offset: 0x00089A90
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 1103584457U)
			{
				if (num != 1387956774U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "NAME")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (name == "DESCRIPTION")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num <= 1927439553U)
		{
			if (num != 1629023597U)
			{
				if (num == 1927439553U)
				{
					if (name == "TOP_CARD_ID")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "ALT_DESCRIPTION")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num != 2485258469U)
		{
			if (num == 2986314849U)
			{
				if (name == "PRECON_CLASS")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "NOTE_NAME")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x06001A75 RID: 6773 RVA: 0x0008B9BE File Offset: 0x00089BBE
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadDeckDbfRecords loadRecords = new LoadDeckDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001A76 RID: 6774 RVA: 0x0008B9D4 File Offset: 0x00089BD4
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		DeckDbfAsset deckDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(DeckDbfAsset)) as DeckDbfAsset;
		if (deckDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("DeckDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < deckDbfAsset.Records.Count; i++)
		{
			deckDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (deckDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001A77 RID: 6775 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001A78 RID: 6776 RVA: 0x0008BA53 File Offset: 0x00089C53
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_description.StripUnusedLocales();
		this.m_altDescription.StripUnusedLocales();
	}

	// Token: 0x04000FE7 RID: 4071
	[SerializeField]
	private string m_noteName;

	// Token: 0x04000FE8 RID: 4072
	[SerializeField]
	private int m_topCardId;

	// Token: 0x04000FE9 RID: 4073
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x04000FEA RID: 4074
	[SerializeField]
	private DbfLocValue m_description;

	// Token: 0x04000FEB RID: 4075
	[SerializeField]
	private DbfLocValue m_altDescription;

	// Token: 0x04000FEC RID: 4076
	[SerializeField]
	private int m_preconClass;
}
