using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001A4 RID: 420
[Serializable]
public class CardSetDbfRecord : DbfRecord
{
	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06001948 RID: 6472 RVA: 0x00088352 File Offset: 0x00086552
	[DbfField("IS_COLLECTIBLE")]
	public bool IsCollectible
	{
		get
		{
			return this.m_isCollectible;
		}
	}

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06001949 RID: 6473 RVA: 0x0008835A File Offset: 0x0008655A
	[DbfField("IS_CORE_CARD_SET")]
	public bool IsCoreCardSet
	{
		get
		{
			return this.m_isCoreCardSet;
		}
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x0600194A RID: 6474 RVA: 0x00088362 File Offset: 0x00086562
	[DbfField("LEGACY_CARD_SET_EVENT")]
	public string LegacyCardSetEvent
	{
		get
		{
			return this.m_legacyCardSetEvent;
		}
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x0600194B RID: 6475 RVA: 0x0008836A File Offset: 0x0008656A
	[DbfField("IS_FEATURED_CARD_SET")]
	public bool IsFeaturedCardSet
	{
		get
		{
			return this.m_isFeaturedCardSet;
		}
	}

	// Token: 0x17000263 RID: 611
	// (get) Token: 0x0600194C RID: 6476 RVA: 0x00088372 File Offset: 0x00086572
	[DbfField("STANDARD_EVENT")]
	public string StandardEvent
	{
		get
		{
			return this.m_standardEvent;
		}
	}

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x0600194D RID: 6477 RVA: 0x0008837A File Offset: 0x0008657A
	[DbfField("CRAFTABLE_WHEN_WILD")]
	public bool CraftableWhenWild
	{
		get
		{
			return this.m_craftableWhenWild;
		}
	}

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x0600194E RID: 6478 RVA: 0x00088382 File Offset: 0x00086582
	[DbfField("CARD_WATERMARK_TEXTURE")]
	public string CardWatermarkTexture
	{
		get
		{
			return this.m_cardWatermarkTexture;
		}
	}

	// Token: 0x17000266 RID: 614
	// (get) Token: 0x0600194F RID: 6479 RVA: 0x0008838A File Offset: 0x0008658A
	[DbfField("SET_FILTER_EVENT")]
	public string SetFilterEvent
	{
		get
		{
			return this.m_setFilterEvent;
		}
	}

	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06001950 RID: 6480 RVA: 0x00088392 File Offset: 0x00086592
	[DbfField("FILTER_ICON_TEXTURE")]
	public string FilterIconTexture
	{
		get
		{
			return this.m_filterIconTexture;
		}
	}

	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06001951 RID: 6481 RVA: 0x0008839A File Offset: 0x0008659A
	[DbfField("FILTER_ICON_OFFSET_X")]
	public double FilterIconOffsetX
	{
		get
		{
			return this.m_filterIconOffsetX;
		}
	}

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06001952 RID: 6482 RVA: 0x000883A2 File Offset: 0x000865A2
	[DbfField("FILTER_ICON_OFFSET_Y")]
	public double FilterIconOffsetY
	{
		get
		{
			return this.m_filterIconOffsetY;
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06001953 RID: 6483 RVA: 0x000883AA File Offset: 0x000865AA
	[DbfField("RELEASE_ORDER")]
	public int ReleaseOrder
	{
		get
		{
			return this.m_releaseOrder;
		}
	}

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06001954 RID: 6484 RVA: 0x000883B2 File Offset: 0x000865B2
	public List<CardSetSpellOverrideDbfRecord> SpellOverrides
	{
		get
		{
			return GameDbf.CardSetSpellOverride.GetRecords((CardSetSpellOverrideDbfRecord r) => r.CardSetId == base.ID, -1);
		}
	}

	// Token: 0x06001955 RID: 6485 RVA: 0x000883CB File Offset: 0x000865CB
	public void SetIsCollectible(bool v)
	{
		this.m_isCollectible = v;
	}

	// Token: 0x06001956 RID: 6486 RVA: 0x000883D4 File Offset: 0x000865D4
	public void SetIsCoreCardSet(bool v)
	{
		this.m_isCoreCardSet = v;
	}

	// Token: 0x06001957 RID: 6487 RVA: 0x000883DD File Offset: 0x000865DD
	public void SetLegacyCardSetEvent(string v)
	{
		this.m_legacyCardSetEvent = v;
	}

	// Token: 0x06001958 RID: 6488 RVA: 0x000883E6 File Offset: 0x000865E6
	public void SetIsFeaturedCardSet(bool v)
	{
		this.m_isFeaturedCardSet = v;
	}

	// Token: 0x06001959 RID: 6489 RVA: 0x000883EF File Offset: 0x000865EF
	public void SetStandardEvent(string v)
	{
		this.m_standardEvent = v;
	}

	// Token: 0x0600195A RID: 6490 RVA: 0x000883F8 File Offset: 0x000865F8
	public void SetCraftableWhenWild(bool v)
	{
		this.m_craftableWhenWild = v;
	}

	// Token: 0x0600195B RID: 6491 RVA: 0x00088401 File Offset: 0x00086601
	public void SetCardWatermarkTexture(string v)
	{
		this.m_cardWatermarkTexture = v;
	}

	// Token: 0x0600195C RID: 6492 RVA: 0x0008840A File Offset: 0x0008660A
	public void SetSetFilterEvent(string v)
	{
		this.m_setFilterEvent = v;
	}

	// Token: 0x0600195D RID: 6493 RVA: 0x00088413 File Offset: 0x00086613
	public void SetFilterIconTexture(string v)
	{
		this.m_filterIconTexture = v;
	}

	// Token: 0x0600195E RID: 6494 RVA: 0x0008841C File Offset: 0x0008661C
	public void SetFilterIconOffsetX(double v)
	{
		this.m_filterIconOffsetX = v;
	}

	// Token: 0x0600195F RID: 6495 RVA: 0x00088425 File Offset: 0x00086625
	public void SetFilterIconOffsetY(double v)
	{
		this.m_filterIconOffsetY = v;
	}

	// Token: 0x06001960 RID: 6496 RVA: 0x0008842E File Offset: 0x0008662E
	public void SetReleaseOrder(int v)
	{
		this.m_releaseOrder = v;
	}

	// Token: 0x06001961 RID: 6497 RVA: 0x00088438 File Offset: 0x00086638
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1738766585U)
		{
			if (num <= 1398813849U)
			{
				if (num != 436653950U)
				{
					if (num != 509783156U)
					{
						if (num == 1398813849U)
						{
							if (name == "SET_FILTER_EVENT")
							{
								return this.m_setFilterEvent;
							}
						}
					}
					else if (name == "IS_FEATURED_CARD_SET")
					{
						return this.m_isFeaturedCardSet;
					}
				}
				else if (name == "CARD_WATERMARK_TEXTURE")
				{
					return this.m_cardWatermarkTexture;
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1737310453U)
				{
					if (num == 1738766585U)
					{
						if (name == "LEGACY_CARD_SET_EVENT")
						{
							return this.m_legacyCardSetEvent;
						}
					}
				}
				else if (name == "RELEASE_ORDER")
				{
					return this.m_releaseOrder;
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (num <= 2351058211U)
		{
			if (num != 2029953557U)
			{
				if (num != 2191767247U)
				{
					if (num == 2351058211U)
					{
						if (name == "CRAFTABLE_WHEN_WILD")
						{
							return this.m_craftableWhenWild;
						}
					}
				}
				else if (name == "STANDARD_EVENT")
				{
					return this.m_standardEvent;
				}
			}
			else if (name == "FILTER_ICON_TEXTURE")
			{
				return this.m_filterIconTexture;
			}
		}
		else if (num <= 3940822416U)
		{
			if (num != 2807272363U)
			{
				if (num == 3940822416U)
				{
					if (name == "IS_COLLECTIBLE")
					{
						return this.m_isCollectible;
					}
				}
			}
			else if (name == "IS_CORE_CARD_SET")
			{
				return this.m_isCoreCardSet;
			}
		}
		else if (num != 4124193314U)
		{
			if (num == 4140970933U)
			{
				if (name == "FILTER_ICON_OFFSET_Y")
				{
					return this.m_filterIconOffsetY;
				}
			}
		}
		else if (name == "FILTER_ICON_OFFSET_X")
		{
			return this.m_filterIconOffsetX;
		}
		return null;
	}

	// Token: 0x06001962 RID: 6498 RVA: 0x000886A0 File Offset: 0x000868A0
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1738766585U)
		{
			if (num <= 1398813849U)
			{
				if (num != 436653950U)
				{
					if (num != 509783156U)
					{
						if (num != 1398813849U)
						{
							return;
						}
						if (!(name == "SET_FILTER_EVENT"))
						{
							return;
						}
						this.m_setFilterEvent = (string)val;
						return;
					}
					else
					{
						if (!(name == "IS_FEATURED_CARD_SET"))
						{
							return;
						}
						this.m_isFeaturedCardSet = (bool)val;
						return;
					}
				}
				else
				{
					if (!(name == "CARD_WATERMARK_TEXTURE"))
					{
						return;
					}
					this.m_cardWatermarkTexture = (string)val;
					return;
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1737310453U)
				{
					if (num != 1738766585U)
					{
						return;
					}
					if (!(name == "LEGACY_CARD_SET_EVENT"))
					{
						return;
					}
					this.m_legacyCardSetEvent = (string)val;
					return;
				}
				else
				{
					if (!(name == "RELEASE_ORDER"))
					{
						return;
					}
					this.m_releaseOrder = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "ID"))
				{
					return;
				}
				base.SetID((int)val);
				return;
			}
		}
		else if (num <= 2351058211U)
		{
			if (num != 2029953557U)
			{
				if (num != 2191767247U)
				{
					if (num != 2351058211U)
					{
						return;
					}
					if (!(name == "CRAFTABLE_WHEN_WILD"))
					{
						return;
					}
					this.m_craftableWhenWild = (bool)val;
					return;
				}
				else
				{
					if (!(name == "STANDARD_EVENT"))
					{
						return;
					}
					this.m_standardEvent = (string)val;
					return;
				}
			}
			else
			{
				if (!(name == "FILTER_ICON_TEXTURE"))
				{
					return;
				}
				this.m_filterIconTexture = (string)val;
				return;
			}
		}
		else if (num <= 3940822416U)
		{
			if (num != 2807272363U)
			{
				if (num != 3940822416U)
				{
					return;
				}
				if (!(name == "IS_COLLECTIBLE"))
				{
					return;
				}
				this.m_isCollectible = (bool)val;
				return;
			}
			else
			{
				if (!(name == "IS_CORE_CARD_SET"))
				{
					return;
				}
				this.m_isCoreCardSet = (bool)val;
				return;
			}
		}
		else if (num != 4124193314U)
		{
			if (num != 4140970933U)
			{
				return;
			}
			if (!(name == "FILTER_ICON_OFFSET_Y"))
			{
				return;
			}
			this.m_filterIconOffsetY = (double)val;
			return;
		}
		else
		{
			if (!(name == "FILTER_ICON_OFFSET_X"))
			{
				return;
			}
			this.m_filterIconOffsetX = (double)val;
			return;
		}
	}

	// Token: 0x06001963 RID: 6499 RVA: 0x000888E4 File Offset: 0x00086AE4
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1738766585U)
		{
			if (num <= 1398813849U)
			{
				if (num != 436653950U)
				{
					if (num != 509783156U)
					{
						if (num == 1398813849U)
						{
							if (name == "SET_FILTER_EVENT")
							{
								return typeof(string);
							}
						}
					}
					else if (name == "IS_FEATURED_CARD_SET")
					{
						return typeof(bool);
					}
				}
				else if (name == "CARD_WATERMARK_TEXTURE")
				{
					return typeof(string);
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1737310453U)
				{
					if (num == 1738766585U)
					{
						if (name == "LEGACY_CARD_SET_EVENT")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "RELEASE_ORDER")
				{
					return typeof(int);
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 2351058211U)
		{
			if (num != 2029953557U)
			{
				if (num != 2191767247U)
				{
					if (num == 2351058211U)
					{
						if (name == "CRAFTABLE_WHEN_WILD")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "STANDARD_EVENT")
				{
					return typeof(string);
				}
			}
			else if (name == "FILTER_ICON_TEXTURE")
			{
				return typeof(string);
			}
		}
		else if (num <= 3940822416U)
		{
			if (num != 2807272363U)
			{
				if (num == 3940822416U)
				{
					if (name == "IS_COLLECTIBLE")
					{
						return typeof(bool);
					}
				}
			}
			else if (name == "IS_CORE_CARD_SET")
			{
				return typeof(bool);
			}
		}
		else if (num != 4124193314U)
		{
			if (num == 4140970933U)
			{
				if (name == "FILTER_ICON_OFFSET_Y")
				{
					return typeof(double);
				}
			}
		}
		else if (name == "FILTER_ICON_OFFSET_X")
		{
			return typeof(double);
		}
		return null;
	}

	// Token: 0x06001964 RID: 6500 RVA: 0x00088B5B File Offset: 0x00086D5B
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardSetDbfRecords loadRecords = new LoadCardSetDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001965 RID: 6501 RVA: 0x00088B74 File Offset: 0x00086D74
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardSetDbfAsset cardSetDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardSetDbfAsset)) as CardSetDbfAsset;
		if (cardSetDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CardSetDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < cardSetDbfAsset.Records.Count; i++)
		{
			cardSetDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (cardSetDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001966 RID: 6502 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001967 RID: 6503 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000F92 RID: 3986
	[SerializeField]
	private bool m_isCollectible = true;

	// Token: 0x04000F93 RID: 3987
	[SerializeField]
	private bool m_isCoreCardSet;

	// Token: 0x04000F94 RID: 3988
	[SerializeField]
	private string m_legacyCardSetEvent = "never";

	// Token: 0x04000F95 RID: 3989
	[SerializeField]
	private bool m_isFeaturedCardSet;

	// Token: 0x04000F96 RID: 3990
	[SerializeField]
	private string m_standardEvent = "always";

	// Token: 0x04000F97 RID: 3991
	[SerializeField]
	private bool m_craftableWhenWild;

	// Token: 0x04000F98 RID: 3992
	[SerializeField]
	private string m_cardWatermarkTexture;

	// Token: 0x04000F99 RID: 3993
	[SerializeField]
	private string m_setFilterEvent = "always";

	// Token: 0x04000F9A RID: 3994
	[SerializeField]
	private string m_filterIconTexture;

	// Token: 0x04000F9B RID: 3995
	[SerializeField]
	private double m_filterIconOffsetX;

	// Token: 0x04000F9C RID: 3996
	[SerializeField]
	private double m_filterIconOffsetY;

	// Token: 0x04000F9D RID: 3997
	[SerializeField]
	private int m_releaseOrder;
}
