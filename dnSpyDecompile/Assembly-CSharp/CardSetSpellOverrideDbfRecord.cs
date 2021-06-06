using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001A7 RID: 423
[Serializable]
public class CardSetSpellOverrideDbfRecord : DbfRecord
{
	// Token: 0x1700026C RID: 620
	// (get) Token: 0x0600196E RID: 6510 RVA: 0x00088CCE File Offset: 0x00086ECE
	[DbfField("CARD_SET_ID")]
	public int CardSetId
	{
		get
		{
			return this.m_cardSetId;
		}
	}

	// Token: 0x1700026D RID: 621
	// (get) Token: 0x0600196F RID: 6511 RVA: 0x00088CD6 File Offset: 0x00086ED6
	[DbfField("SPELL_TYPE")]
	public string SpellType
	{
		get
		{
			return this.m_spellType;
		}
	}

	// Token: 0x1700026E RID: 622
	// (get) Token: 0x06001970 RID: 6512 RVA: 0x00088CDE File Offset: 0x00086EDE
	[DbfField("OVERRIDE_PREFAB")]
	public string OverridePrefab
	{
		get
		{
			return this.m_overridePrefab;
		}
	}

	// Token: 0x06001971 RID: 6513 RVA: 0x00088CE6 File Offset: 0x00086EE6
	public void SetCardSetId(int v)
	{
		this.m_cardSetId = v;
	}

	// Token: 0x06001972 RID: 6514 RVA: 0x00088CEF File Offset: 0x00086EEF
	public void SetSpellType(string v)
	{
		this.m_spellType = v;
	}

	// Token: 0x06001973 RID: 6515 RVA: 0x00088CF8 File Offset: 0x00086EF8
	public void SetOverridePrefab(string v)
	{
		this.m_overridePrefab = v;
	}

	// Token: 0x06001974 RID: 6516 RVA: 0x00088D04 File Offset: 0x00086F04
	public override object GetVar(string name)
	{
		if (name == "CARD_SET_ID")
		{
			return this.m_cardSetId;
		}
		if (name == "SPELL_TYPE")
		{
			return this.m_spellType;
		}
		if (!(name == "OVERRIDE_PREFAB"))
		{
			return null;
		}
		return this.m_overridePrefab;
	}

	// Token: 0x06001975 RID: 6517 RVA: 0x00088D58 File Offset: 0x00086F58
	public override void SetVar(string name, object val)
	{
		if (name == "CARD_SET_ID")
		{
			this.m_cardSetId = (int)val;
			return;
		}
		if (name == "SPELL_TYPE")
		{
			this.m_spellType = (string)val;
			return;
		}
		if (!(name == "OVERRIDE_PREFAB"))
		{
			return;
		}
		this.m_overridePrefab = (string)val;
	}

	// Token: 0x06001976 RID: 6518 RVA: 0x00088DB4 File Offset: 0x00086FB4
	public override Type GetVarType(string name)
	{
		if (name == "CARD_SET_ID")
		{
			return typeof(int);
		}
		if (name == "SPELL_TYPE")
		{
			return typeof(string);
		}
		if (!(name == "OVERRIDE_PREFAB"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x06001977 RID: 6519 RVA: 0x00088E0C File Offset: 0x0008700C
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardSetSpellOverrideDbfRecords loadRecords = new LoadCardSetSpellOverrideDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001978 RID: 6520 RVA: 0x00088E24 File Offset: 0x00087024
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardSetSpellOverrideDbfAsset cardSetSpellOverrideDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardSetSpellOverrideDbfAsset)) as CardSetSpellOverrideDbfAsset;
		if (cardSetSpellOverrideDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CardSetSpellOverrideDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < cardSetSpellOverrideDbfAsset.Records.Count; i++)
		{
			cardSetSpellOverrideDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (cardSetSpellOverrideDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001979 RID: 6521 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600197A RID: 6522 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000FA0 RID: 4000
	[SerializeField]
	private int m_cardSetId;

	// Token: 0x04000FA1 RID: 4001
	[SerializeField]
	private string m_spellType = "NONE";

	// Token: 0x04000FA2 RID: 4002
	[SerializeField]
	private string m_overridePrefab;
}
