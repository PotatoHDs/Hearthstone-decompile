using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001D7 RID: 471
[Serializable]
public class DeckTemplateDbfRecord : DbfRecord
{
	// Token: 0x170002BF RID: 703
	// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0008CA0E File Offset: 0x0008AC0E
	[DbfField("CLASS_ID")]
	public int ClassId
	{
		get
		{
			return this.m_classId;
		}
	}

	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x06001ACA RID: 6858 RVA: 0x0008CA16 File Offset: 0x0008AC16
	public ClassDbfRecord ClassRecord
	{
		get
		{
			return GameDbf.Class.GetRecord(this.m_classId);
		}
	}

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x06001ACB RID: 6859 RVA: 0x0008CA28 File Offset: 0x0008AC28
	[DbfField("EVENT")]
	public string Event
	{
		get
		{
			return this.m_event;
		}
	}

	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x06001ACC RID: 6860 RVA: 0x0008CA30 File Offset: 0x0008AC30
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x170002C3 RID: 707
	// (get) Token: 0x06001ACD RID: 6861 RVA: 0x0008CA38 File Offset: 0x0008AC38
	[DbfField("DECK_ID")]
	public int DeckId
	{
		get
		{
			return this.m_deckId;
		}
	}

	// Token: 0x170002C4 RID: 708
	// (get) Token: 0x06001ACE RID: 6862 RVA: 0x0008CA40 File Offset: 0x0008AC40
	public DeckDbfRecord DeckRecord
	{
		get
		{
			return GameDbf.Deck.GetRecord(this.m_deckId);
		}
	}

	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0008CA52 File Offset: 0x0008AC52
	[DbfField("DISPLAY_TEXTURE")]
	public string DisplayTexture
	{
		get
		{
			return this.m_displayTexture;
		}
	}

	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x0008CA5A File Offset: 0x0008AC5A
	[DbfField("IS_FREE_REWARD")]
	public bool IsFreeReward
	{
		get
		{
			return this.m_isFreeReward;
		}
	}

	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x0008CA62 File Offset: 0x0008AC62
	[DbfField("IS_STARTER_DECK")]
	public bool IsStarterDeck
	{
		get
		{
			return this.m_isStarterDeck;
		}
	}

	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x0008CA6A File Offset: 0x0008AC6A
	[DbfField("FORMAT_TYPE")]
	public DeckTemplate.FormatType FormatType
	{
		get
		{
			return this.m_formatType;
		}
	}

	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x0008CA72 File Offset: 0x0008AC72
	[DbfField("DISPLAY_CARD_ID")]
	public int DisplayCardId
	{
		get
		{
			return this.m_displayCardId;
		}
	}

	// Token: 0x170002CA RID: 714
	// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x0008CA7A File Offset: 0x0008AC7A
	public CardDbfRecord DisplayCardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_displayCardId);
		}
	}

	// Token: 0x06001AD5 RID: 6869 RVA: 0x0008CA8C File Offset: 0x0008AC8C
	public void SetClassId(int v)
	{
		this.m_classId = v;
	}

	// Token: 0x06001AD6 RID: 6870 RVA: 0x0008CA95 File Offset: 0x0008AC95
	public void SetEvent(string v)
	{
		this.m_event = v;
	}

	// Token: 0x06001AD7 RID: 6871 RVA: 0x0008CA9E File Offset: 0x0008AC9E
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x06001AD8 RID: 6872 RVA: 0x0008CAA7 File Offset: 0x0008ACA7
	public void SetDeckId(int v)
	{
		this.m_deckId = v;
	}

	// Token: 0x06001AD9 RID: 6873 RVA: 0x0008CAB0 File Offset: 0x0008ACB0
	public void SetDisplayTexture(string v)
	{
		this.m_displayTexture = v;
	}

	// Token: 0x06001ADA RID: 6874 RVA: 0x0008CAB9 File Offset: 0x0008ACB9
	public void SetIsFreeReward(bool v)
	{
		this.m_isFreeReward = v;
	}

	// Token: 0x06001ADB RID: 6875 RVA: 0x0008CAC2 File Offset: 0x0008ACC2
	public void SetIsStarterDeck(bool v)
	{
		this.m_isStarterDeck = v;
	}

	// Token: 0x06001ADC RID: 6876 RVA: 0x0008CACB File Offset: 0x0008ACCB
	public void SetFormatType(DeckTemplate.FormatType v)
	{
		this.m_formatType = v;
	}

	// Token: 0x06001ADD RID: 6877 RVA: 0x0008CAD4 File Offset: 0x0008ACD4
	public void SetDisplayCardId(int v)
	{
		this.m_displayCardId = v;
	}

	// Token: 0x06001ADE RID: 6878 RVA: 0x0008CAE0 File Offset: 0x0008ACE0
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num <= 236776447U)
			{
				if (num != 96691247U)
				{
					if (num == 236776447U)
					{
						if (name == "EVENT")
						{
							return this.m_event;
						}
					}
				}
				else if (name == "FORMAT_TYPE")
				{
					return this.m_formatType;
				}
			}
			else if (num != 281873083U)
			{
				if (num != 771121008U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "DECK_ID")
				{
					return this.m_deckId;
				}
			}
			else if (name == "IS_STARTER_DECK")
			{
				return this.m_isStarterDeck;
			}
		}
		else if (num <= 3420453062U)
		{
			if (num != 2452245441U)
			{
				if (num == 3420453062U)
				{
					if (name == "IS_FREE_REWARD")
					{
						return this.m_isFreeReward;
					}
				}
			}
			else if (name == "DISPLAY_TEXTURE")
			{
				return this.m_displayTexture;
			}
		}
		else if (num != 4214602626U)
		{
			if (num != 4257872637U)
			{
				if (num == 4274360108U)
				{
					if (name == "DISPLAY_CARD_ID")
					{
						return this.m_displayCardId;
					}
				}
			}
			else if (name == "CLASS_ID")
			{
				return this.m_classId;
			}
		}
		else if (name == "SORT_ORDER")
		{
			return this.m_sortOrder;
		}
		return null;
	}

	// Token: 0x06001ADF RID: 6879 RVA: 0x0008CCB4 File Offset: 0x0008AEB4
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num <= 236776447U)
			{
				if (num != 96691247U)
				{
					if (num != 236776447U)
					{
						return;
					}
					if (!(name == "EVENT"))
					{
						return;
					}
					this.m_event = (string)val;
					return;
				}
				else
				{
					if (!(name == "FORMAT_TYPE"))
					{
						return;
					}
					if (val == null)
					{
						this.m_formatType = DeckTemplate.FormatType.FT_UNKNOWN;
						return;
					}
					if (val is DeckTemplate.FormatType || val is int)
					{
						this.m_formatType = (DeckTemplate.FormatType)val;
						return;
					}
					if (val is string)
					{
						this.m_formatType = DeckTemplate.ParseFormatTypeValue((string)val);
						return;
					}
				}
			}
			else if (num != 281873083U)
			{
				if (num != 771121008U)
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
					if (!(name == "DECK_ID"))
					{
						return;
					}
					this.m_deckId = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "IS_STARTER_DECK"))
				{
					return;
				}
				this.m_isStarterDeck = (bool)val;
				return;
			}
		}
		else if (num <= 3420453062U)
		{
			if (num != 2452245441U)
			{
				if (num != 3420453062U)
				{
					return;
				}
				if (!(name == "IS_FREE_REWARD"))
				{
					return;
				}
				this.m_isFreeReward = (bool)val;
				return;
			}
			else
			{
				if (!(name == "DISPLAY_TEXTURE"))
				{
					return;
				}
				this.m_displayTexture = (string)val;
				return;
			}
		}
		else if (num != 4214602626U)
		{
			if (num != 4257872637U)
			{
				if (num != 4274360108U)
				{
					return;
				}
				if (!(name == "DISPLAY_CARD_ID"))
				{
					return;
				}
				this.m_displayCardId = (int)val;
			}
			else
			{
				if (!(name == "CLASS_ID"))
				{
					return;
				}
				this.m_classId = (int)val;
				return;
			}
		}
		else
		{
			if (!(name == "SORT_ORDER"))
			{
				return;
			}
			this.m_sortOrder = (int)val;
			return;
		}
	}

	// Token: 0x06001AE0 RID: 6880 RVA: 0x0008CE8C File Offset: 0x0008B08C
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num <= 236776447U)
			{
				if (num != 96691247U)
				{
					if (num == 236776447U)
					{
						if (name == "EVENT")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "FORMAT_TYPE")
				{
					return typeof(DeckTemplate.FormatType);
				}
			}
			else if (num != 281873083U)
			{
				if (num != 771121008U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "DECK_ID")
				{
					return typeof(int);
				}
			}
			else if (name == "IS_STARTER_DECK")
			{
				return typeof(bool);
			}
		}
		else if (num <= 3420453062U)
		{
			if (num != 2452245441U)
			{
				if (num == 3420453062U)
				{
					if (name == "IS_FREE_REWARD")
					{
						return typeof(bool);
					}
				}
			}
			else if (name == "DISPLAY_TEXTURE")
			{
				return typeof(string);
			}
		}
		else if (num != 4214602626U)
		{
			if (num != 4257872637U)
			{
				if (num == 4274360108U)
				{
					if (name == "DISPLAY_CARD_ID")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "CLASS_ID")
			{
				return typeof(int);
			}
		}
		else if (name == "SORT_ORDER")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x06001AE1 RID: 6881 RVA: 0x0008D05D File Offset: 0x0008B25D
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadDeckTemplateDbfRecords loadRecords = new LoadDeckTemplateDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001AE2 RID: 6882 RVA: 0x0008D074 File Offset: 0x0008B274
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		DeckTemplateDbfAsset deckTemplateDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(DeckTemplateDbfAsset)) as DeckTemplateDbfAsset;
		if (deckTemplateDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("DeckTemplateDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < deckTemplateDbfAsset.Records.Count; i++)
		{
			deckTemplateDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (deckTemplateDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001AE3 RID: 6883 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001AE4 RID: 6884 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001005 RID: 4101
	[SerializeField]
	private int m_classId;

	// Token: 0x04001006 RID: 4102
	[SerializeField]
	private string m_event;

	// Token: 0x04001007 RID: 4103
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x04001008 RID: 4104
	[SerializeField]
	private int m_deckId;

	// Token: 0x04001009 RID: 4105
	[SerializeField]
	private string m_displayTexture;

	// Token: 0x0400100A RID: 4106
	[SerializeField]
	private bool m_isFreeReward;

	// Token: 0x0400100B RID: 4107
	[SerializeField]
	private bool m_isStarterDeck;

	// Token: 0x0400100C RID: 4108
	[SerializeField]
	private DeckTemplate.FormatType m_formatType = DeckTemplate.FormatType.FT_STANDARD;

	// Token: 0x0400100D RID: 4109
	[SerializeField]
	private int m_displayCardId;
}
