using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000226 RID: 550
[Serializable]
public class ProductClientDataDbfRecord : DbfRecord
{
	// Token: 0x170003A0 RID: 928
	// (get) Token: 0x06001DBB RID: 7611 RVA: 0x0009820E File Offset: 0x0009640E
	[DbfField("PMT_PRODUCT_ID")]
	public long PmtProductId
	{
		get
		{
			return this.m_pmtProductId;
		}
	}

	// Token: 0x170003A1 RID: 929
	// (get) Token: 0x06001DBC RID: 7612 RVA: 0x00098216 File Offset: 0x00096416
	[DbfField("POPUP_TITLE")]
	public DbfLocValue PopupTitle
	{
		get
		{
			return this.m_popupTitle;
		}
	}

	// Token: 0x170003A2 RID: 930
	// (get) Token: 0x06001DBD RID: 7613 RVA: 0x0009821E File Offset: 0x0009641E
	[DbfField("POPUP_BODY")]
	public DbfLocValue PopupBody
	{
		get
		{
			return this.m_popupBody;
		}
	}

	// Token: 0x06001DBE RID: 7614 RVA: 0x00098226 File Offset: 0x00096426
	public void SetPmtProductId(long v)
	{
		this.m_pmtProductId = v;
	}

	// Token: 0x06001DBF RID: 7615 RVA: 0x0009822F File Offset: 0x0009642F
	public void SetPopupTitle(DbfLocValue v)
	{
		this.m_popupTitle = v;
		v.SetDebugInfo(base.ID, "POPUP_TITLE");
	}

	// Token: 0x06001DC0 RID: 7616 RVA: 0x00098249 File Offset: 0x00096449
	public void SetPopupBody(DbfLocValue v)
	{
		this.m_popupBody = v;
		v.SetDebugInfo(base.ID, "POPUP_BODY");
	}

	// Token: 0x06001DC1 RID: 7617 RVA: 0x00098264 File Offset: 0x00096464
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "PMT_PRODUCT_ID")
		{
			return this.m_pmtProductId;
		}
		if (name == "POPUP_TITLE")
		{
			return this.m_popupTitle;
		}
		if (!(name == "POPUP_BODY"))
		{
			return null;
		}
		return this.m_popupBody;
	}

	// Token: 0x06001DC2 RID: 7618 RVA: 0x000982D0 File Offset: 0x000964D0
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "PMT_PRODUCT_ID")
		{
			this.m_pmtProductId = (long)val;
			return;
		}
		if (name == "POPUP_TITLE")
		{
			this.m_popupTitle = (DbfLocValue)val;
			return;
		}
		if (!(name == "POPUP_BODY"))
		{
			return;
		}
		this.m_popupBody = (DbfLocValue)val;
	}

	// Token: 0x06001DC3 RID: 7619 RVA: 0x00098348 File Offset: 0x00096548
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "PMT_PRODUCT_ID")
		{
			return typeof(long);
		}
		if (name == "POPUP_TITLE")
		{
			return typeof(DbfLocValue);
		}
		if (!(name == "POPUP_BODY"))
		{
			return null;
		}
		return typeof(DbfLocValue);
	}

	// Token: 0x06001DC4 RID: 7620 RVA: 0x000983B8 File Offset: 0x000965B8
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadProductClientDataDbfRecords loadRecords = new LoadProductClientDataDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001DC5 RID: 7621 RVA: 0x000983D0 File Offset: 0x000965D0
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ProductClientDataDbfAsset productClientDataDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ProductClientDataDbfAsset)) as ProductClientDataDbfAsset;
		if (productClientDataDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ProductClientDataDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < productClientDataDbfAsset.Records.Count; i++)
		{
			productClientDataDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (productClientDataDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001DC6 RID: 7622 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001DC7 RID: 7623 RVA: 0x0009844F File Offset: 0x0009664F
	public override void StripUnusedLocales()
	{
		this.m_popupTitle.StripUnusedLocales();
		this.m_popupBody.StripUnusedLocales();
	}

	// Token: 0x04001165 RID: 4453
	[SerializeField]
	private long m_pmtProductId;

	// Token: 0x04001166 RID: 4454
	[SerializeField]
	private DbfLocValue m_popupTitle;

	// Token: 0x04001167 RID: 4455
	[SerializeField]
	private DbfLocValue m_popupBody;
}
