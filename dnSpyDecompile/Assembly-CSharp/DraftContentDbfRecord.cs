using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001DA RID: 474
[Serializable]
public class DraftContentDbfRecord : DbfRecord
{
	// Token: 0x170002CB RID: 715
	// (get) Token: 0x06001AEA RID: 6890 RVA: 0x0008D19E File Offset: 0x0008B39E
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x170002CC RID: 716
	// (get) Token: 0x06001AEB RID: 6891 RVA: 0x0008D1A6 File Offset: 0x0008B3A6
	[DbfField("SLOT")]
	public int Slot
	{
		get
		{
			return this.m_slot;
		}
	}

	// Token: 0x170002CD RID: 717
	// (get) Token: 0x06001AEC RID: 6892 RVA: 0x0008D1AE File Offset: 0x0008B3AE
	[DbfField("DECK_ID")]
	public int DeckId
	{
		get
		{
			return this.m_deckId;
		}
	}

	// Token: 0x170002CE RID: 718
	// (get) Token: 0x06001AED RID: 6893 RVA: 0x0008D1B6 File Offset: 0x0008B3B6
	public DeckDbfRecord DeckRecord
	{
		get
		{
			return GameDbf.Deck.GetRecord(this.m_deckId);
		}
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x06001AEE RID: 6894 RVA: 0x0008D1C8 File Offset: 0x0008B3C8
	[DbfField("SLOT_TYPE")]
	public DraftContent.SlotType SlotType
	{
		get
		{
			return this.m_slotType;
		}
	}

	// Token: 0x06001AEF RID: 6895 RVA: 0x0008D1D0 File Offset: 0x0008B3D0
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001AF0 RID: 6896 RVA: 0x0008D1D9 File Offset: 0x0008B3D9
	public void SetSlot(int v)
	{
		this.m_slot = v;
	}

	// Token: 0x06001AF1 RID: 6897 RVA: 0x0008D1E2 File Offset: 0x0008B3E2
	public void SetDeckId(int v)
	{
		this.m_deckId = v;
	}

	// Token: 0x06001AF2 RID: 6898 RVA: 0x0008D1EB File Offset: 0x0008B3EB
	public void SetSlotType(DraftContent.SlotType v)
	{
		this.m_slotType = v;
	}

	// Token: 0x06001AF3 RID: 6899 RVA: 0x0008D1F4 File Offset: 0x0008B3F4
	public override object GetVar(string name)
	{
		if (name == "NOTE_DESC")
		{
			return this.m_noteDesc;
		}
		if (name == "SLOT")
		{
			return this.m_slot;
		}
		if (name == "DECK_ID")
		{
			return this.m_deckId;
		}
		if (!(name == "SLOT_TYPE"))
		{
			return null;
		}
		return this.m_slotType;
	}

	// Token: 0x06001AF4 RID: 6900 RVA: 0x0008D264 File Offset: 0x0008B464
	public override void SetVar(string name, object val)
	{
		if (name == "NOTE_DESC")
		{
			this.m_noteDesc = (string)val;
			return;
		}
		if (name == "SLOT")
		{
			this.m_slot = (int)val;
			return;
		}
		if (name == "DECK_ID")
		{
			this.m_deckId = (int)val;
			return;
		}
		if (!(name == "SLOT_TYPE"))
		{
			return;
		}
		if (val == null)
		{
			this.m_slotType = DraftContent.SlotType.NONE;
			return;
		}
		if (val is DraftContent.SlotType || val is int)
		{
			this.m_slotType = (DraftContent.SlotType)val;
			return;
		}
		if (val is string)
		{
			this.m_slotType = DraftContent.ParseSlotTypeValue((string)val);
		}
	}

	// Token: 0x06001AF5 RID: 6901 RVA: 0x0008D310 File Offset: 0x0008B510
	public override Type GetVarType(string name)
	{
		if (name == "NOTE_DESC")
		{
			return typeof(string);
		}
		if (name == "SLOT")
		{
			return typeof(int);
		}
		if (name == "DECK_ID")
		{
			return typeof(int);
		}
		if (!(name == "SLOT_TYPE"))
		{
			return null;
		}
		return typeof(DraftContent.SlotType);
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x0008D380 File Offset: 0x0008B580
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadDraftContentDbfRecords loadRecords = new LoadDraftContentDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x0008D398 File Offset: 0x0008B598
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		DraftContentDbfAsset draftContentDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(DraftContentDbfAsset)) as DraftContentDbfAsset;
		if (draftContentDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("DraftContentDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < draftContentDbfAsset.Records.Count; i++)
		{
			draftContentDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (draftContentDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001AF9 RID: 6905 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001010 RID: 4112
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04001011 RID: 4113
	[SerializeField]
	private int m_slot;

	// Token: 0x04001012 RID: 4114
	[SerializeField]
	private int m_deckId;

	// Token: 0x04001013 RID: 4115
	[SerializeField]
	private DraftContent.SlotType m_slotType;
}
