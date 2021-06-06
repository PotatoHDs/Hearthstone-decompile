using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000229 RID: 553
[Serializable]
public class ProductDbfRecord : DbfRecord
{
	// Token: 0x170003A3 RID: 931
	// (get) Token: 0x06001DCD RID: 7629 RVA: 0x00098502 File Offset: 0x00096702
	public ProductClientDataDbfRecord ClientData
	{
		get
		{
			return GameDbf.ProductClientData.GetRecord((ProductClientDataDbfRecord r) => r.PmtProductId == (long)base.ID);
		}
	}

	// Token: 0x06001DCE RID: 7630 RVA: 0x00090F7E File Offset: 0x0008F17E
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		return null;
	}

	// Token: 0x06001DCF RID: 7631 RVA: 0x00090F9A File Offset: 0x0008F19A
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
		}
	}

	// Token: 0x06001DD0 RID: 7632 RVA: 0x00090FB5 File Offset: 0x0008F1B5
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x06001DD1 RID: 7633 RVA: 0x0009851A File Offset: 0x0009671A
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadProductDbfRecords loadRecords = new LoadProductDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001DD2 RID: 7634 RVA: 0x00098530 File Offset: 0x00096730
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ProductDbfAsset productDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ProductDbfAsset)) as ProductDbfAsset;
		if (productDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ProductDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < productDbfAsset.Records.Count; i++)
		{
			productDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (productDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001DD3 RID: 7635 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001DD4 RID: 7636 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}
}
