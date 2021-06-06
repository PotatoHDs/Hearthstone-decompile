using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001B3 RID: 435
[Serializable]
public class CharacterDialogDbfRecord : DbfRecord
{
	// Token: 0x1700027A RID: 634
	// (get) Token: 0x060019B7 RID: 6583 RVA: 0x000896AA File Offset: 0x000878AA
	[DbfField("ON_COMPLETE_BANNER_ID")]
	public int OnCompleteBannerId
	{
		get
		{
			return this.m_onCompleteBannerId;
		}
	}

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x060019B8 RID: 6584 RVA: 0x000896B2 File Offset: 0x000878B2
	public BannerDbfRecord OnCompleteBannerRecord
	{
		get
		{
			return GameDbf.Banner.GetRecord(this.m_onCompleteBannerId);
		}
	}

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x060019B9 RID: 6585 RVA: 0x000896C4 File Offset: 0x000878C4
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x060019BA RID: 6586 RVA: 0x000896CC File Offset: 0x000878CC
	[DbfField("IGNORE_POPUPS")]
	public bool IgnorePopups
	{
		get
		{
			return this.m_ignorePopups;
		}
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x060019BB RID: 6587 RVA: 0x000896D4 File Offset: 0x000878D4
	[DbfField("BLOCK_INPUT")]
	public bool BlockInput
	{
		get
		{
			return this.m_blockInput;
		}
	}

	// Token: 0x1700027F RID: 639
	// (get) Token: 0x060019BC RID: 6588 RVA: 0x000896DC File Offset: 0x000878DC
	[DbfField("DEFER_ON_COMPLETE")]
	public bool DeferOnComplete
	{
		get
		{
			return this.m_deferOnComplete;
		}
	}

	// Token: 0x17000280 RID: 640
	// (get) Token: 0x060019BD RID: 6589 RVA: 0x000896E4 File Offset: 0x000878E4
	public List<CharacterDialogItemsDbfRecord> Items
	{
		get
		{
			return GameDbf.CharacterDialogItems.GetRecords((CharacterDialogItemsDbfRecord r) => r.CharacterDialogId == base.ID, -1);
		}
	}

	// Token: 0x060019BE RID: 6590 RVA: 0x000896FD File Offset: 0x000878FD
	public void SetOnCompleteBannerId(int v)
	{
		this.m_onCompleteBannerId = v;
	}

	// Token: 0x060019BF RID: 6591 RVA: 0x00089706 File Offset: 0x00087906
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x060019C0 RID: 6592 RVA: 0x0008970F File Offset: 0x0008790F
	public void SetIgnorePopups(bool v)
	{
		this.m_ignorePopups = v;
	}

	// Token: 0x060019C1 RID: 6593 RVA: 0x00089718 File Offset: 0x00087918
	public void SetBlockInput(bool v)
	{
		this.m_blockInput = v;
	}

	// Token: 0x060019C2 RID: 6594 RVA: 0x00089721 File Offset: 0x00087921
	public void SetDeferOnComplete(bool v)
	{
		this.m_deferOnComplete = v;
	}

	// Token: 0x060019C3 RID: 6595 RVA: 0x0008972C File Offset: 0x0008792C
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "ON_COMPLETE_BANNER_ID")
		{
			return this.m_onCompleteBannerId;
		}
		if (name == "NOTE_DESC")
		{
			return this.m_noteDesc;
		}
		if (name == "IGNORE_POPUPS")
		{
			return this.m_ignorePopups;
		}
		if (name == "BLOCK_INPUT")
		{
			return this.m_blockInput;
		}
		if (!(name == "DEFER_ON_COMPLETE"))
		{
			return null;
		}
		return this.m_deferOnComplete;
	}

	// Token: 0x060019C4 RID: 6596 RVA: 0x000897D0 File Offset: 0x000879D0
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "ON_COMPLETE_BANNER_ID")
		{
			this.m_onCompleteBannerId = (int)val;
			return;
		}
		if (name == "NOTE_DESC")
		{
			this.m_noteDesc = (string)val;
			return;
		}
		if (name == "IGNORE_POPUPS")
		{
			this.m_ignorePopups = (bool)val;
			return;
		}
		if (name == "BLOCK_INPUT")
		{
			this.m_blockInput = (bool)val;
			return;
		}
		if (!(name == "DEFER_ON_COMPLETE"))
		{
			return;
		}
		this.m_deferOnComplete = (bool)val;
	}

	// Token: 0x060019C5 RID: 6597 RVA: 0x0008987C File Offset: 0x00087A7C
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "ON_COMPLETE_BANNER_ID")
		{
			return typeof(int);
		}
		if (name == "NOTE_DESC")
		{
			return typeof(string);
		}
		if (name == "IGNORE_POPUPS")
		{
			return typeof(bool);
		}
		if (name == "BLOCK_INPUT")
		{
			return typeof(bool);
		}
		if (!(name == "DEFER_ON_COMPLETE"))
		{
			return null;
		}
		return typeof(bool);
	}

	// Token: 0x060019C6 RID: 6598 RVA: 0x0008991C File Offset: 0x00087B1C
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCharacterDialogDbfRecords loadRecords = new LoadCharacterDialogDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060019C7 RID: 6599 RVA: 0x00089934 File Offset: 0x00087B34
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CharacterDialogDbfAsset characterDialogDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CharacterDialogDbfAsset)) as CharacterDialogDbfAsset;
		if (characterDialogDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CharacterDialogDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < characterDialogDbfAsset.Records.Count; i++)
		{
			characterDialogDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (characterDialogDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060019C8 RID: 6600 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060019C9 RID: 6601 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000FB4 RID: 4020
	[SerializeField]
	private int m_onCompleteBannerId;

	// Token: 0x04000FB5 RID: 4021
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04000FB6 RID: 4022
	[SerializeField]
	private bool m_ignorePopups = true;

	// Token: 0x04000FB7 RID: 4023
	[SerializeField]
	private bool m_blockInput;

	// Token: 0x04000FB8 RID: 4024
	[SerializeField]
	private bool m_deferOnComplete = true;
}
