using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010D6 RID: 4310
	public class RewardItemDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BCEA RID: 48362 RVA: 0x0039A0EC File Offset: 0x003982EC
		public RewardItemDataModel()
		{
			base.RegisterNestedDataModel(this.m_Booster);
			base.RegisterNestedDataModel(this.m_CardBack);
			base.RegisterNestedDataModel(this.m_Currency);
			base.RegisterNestedDataModel(this.m_Card);
			base.RegisterNestedDataModel(this.m_RandomCard);
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x0600BCEB RID: 48363 RVA: 0x0039A35D File Offset: 0x0039855D
		public int DataModelId
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x0600BCEC RID: 48364 RVA: 0x0039A361 File Offset: 0x00398561
		public string DataModelDisplayName
		{
			get
			{
				return "reward_item";
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x0600BCEE RID: 48366 RVA: 0x0039A38E File Offset: 0x0039858E
		// (set) Token: 0x0600BCED RID: 48365 RVA: 0x0039A368 File Offset: 0x00398568
		public long PmtLicenseId
		{
			get
			{
				return this.m_PmtLicenseId;
			}
			set
			{
				if (this.m_PmtLicenseId == value)
				{
					return;
				}
				this.m_PmtLicenseId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x0600BCF0 RID: 48368 RVA: 0x0039A3BC File Offset: 0x003985BC
		// (set) Token: 0x0600BCEF RID: 48367 RVA: 0x0039A396 File Offset: 0x00398596
		public RewardItemType ItemType
		{
			get
			{
				return this.m_ItemType;
			}
			set
			{
				if (this.m_ItemType == value)
				{
					return;
				}
				this.m_ItemType = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x0600BCF2 RID: 48370 RVA: 0x0039A3EA File Offset: 0x003985EA
		// (set) Token: 0x0600BCF1 RID: 48369 RVA: 0x0039A3C4 File Offset: 0x003985C4
		public int ItemId
		{
			get
			{
				return this.m_ItemId;
			}
			set
			{
				if (this.m_ItemId == value)
				{
					return;
				}
				this.m_ItemId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x0600BCF4 RID: 48372 RVA: 0x0039A418 File Offset: 0x00398618
		// (set) Token: 0x0600BCF3 RID: 48371 RVA: 0x0039A3F2 File Offset: 0x003985F2
		public int Quantity
		{
			get
			{
				return this.m_Quantity;
			}
			set
			{
				if (this.m_Quantity == value)
				{
					return;
				}
				this.m_Quantity = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x0600BCF6 RID: 48374 RVA: 0x0039A459 File Offset: 0x00398659
		// (set) Token: 0x0600BCF5 RID: 48373 RVA: 0x0039A420 File Offset: 0x00398620
		public PackDataModel Booster
		{
			get
			{
				return this.m_Booster;
			}
			set
			{
				if (this.m_Booster == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Booster);
				base.RegisterNestedDataModel(value);
				this.m_Booster = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x0600BCF8 RID: 48376 RVA: 0x0039A49A File Offset: 0x0039869A
		// (set) Token: 0x0600BCF7 RID: 48375 RVA: 0x0039A461 File Offset: 0x00398661
		public CardBackDataModel CardBack
		{
			get
			{
				return this.m_CardBack;
			}
			set
			{
				if (this.m_CardBack == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_CardBack);
				base.RegisterNestedDataModel(value);
				this.m_CardBack = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x0600BCFA RID: 48378 RVA: 0x0039A4DB File Offset: 0x003986DB
		// (set) Token: 0x0600BCF9 RID: 48377 RVA: 0x0039A4A2 File Offset: 0x003986A2
		public PriceDataModel Currency
		{
			get
			{
				return this.m_Currency;
			}
			set
			{
				if (this.m_Currency == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Currency);
				base.RegisterNestedDataModel(value);
				this.m_Currency = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x0600BCFC RID: 48380 RVA: 0x0039A509 File Offset: 0x00398709
		// (set) Token: 0x0600BCFB RID: 48379 RVA: 0x0039A4E3 File Offset: 0x003986E3
		public int AssetId
		{
			get
			{
				return this.m_AssetId;
			}
			set
			{
				if (this.m_AssetId == value)
				{
					return;
				}
				this.m_AssetId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x0600BCFE RID: 48382 RVA: 0x0039A54A File Offset: 0x0039874A
		// (set) Token: 0x0600BCFD RID: 48381 RVA: 0x0039A511 File Offset: 0x00398711
		public CardDataModel Card
		{
			get
			{
				return this.m_Card;
			}
			set
			{
				if (this.m_Card == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Card);
				base.RegisterNestedDataModel(value);
				this.m_Card = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x0600BD00 RID: 48384 RVA: 0x0039A58B File Offset: 0x0039878B
		// (set) Token: 0x0600BCFF RID: 48383 RVA: 0x0039A552 File Offset: 0x00398752
		public RandomCardDataModel RandomCard
		{
			get
			{
				return this.m_RandomCard;
			}
			set
			{
				if (this.m_RandomCard == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_RandomCard);
				base.RegisterNestedDataModel(value);
				this.m_RandomCard = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x0600BD01 RID: 48385 RVA: 0x0039A593 File Offset: 0x00398793
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BD02 RID: 48386 RVA: 0x0039A59C File Offset: 0x0039879C
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			long pmtLicenseId = this.m_PmtLicenseId;
			int num2 = (num + this.m_PmtLicenseId.GetHashCode()) * 31;
			RewardItemType itemType = this.m_ItemType;
			int num3 = (num2 + this.m_ItemType.GetHashCode()) * 31;
			int itemId = this.m_ItemId;
			int num4 = (num3 + this.m_ItemId.GetHashCode()) * 31;
			int quantity = this.m_Quantity;
			int num5 = ((((num4 + this.m_Quantity.GetHashCode()) * 31 + ((this.m_Booster != null) ? this.m_Booster.GetPropertiesHashCode() : 0)) * 31 + ((this.m_CardBack != null) ? this.m_CardBack.GetPropertiesHashCode() : 0)) * 31 + ((this.m_Currency != null) ? this.m_Currency.GetPropertiesHashCode() : 0)) * 31;
			int assetId = this.m_AssetId;
			return ((num5 + this.m_AssetId.GetHashCode()) * 31 + ((this.m_Card != null) ? this.m_Card.GetPropertiesHashCode() : 0)) * 31 + ((this.m_RandomCard != null) ? this.m_RandomCard.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BD03 RID: 48387 RVA: 0x0039A6A4 File Offset: 0x003988A4
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_PmtLicenseId;
				return true;
			case 1:
				value = this.m_ItemType;
				return true;
			case 2:
				value = this.m_ItemId;
				return true;
			case 3:
				value = this.m_Quantity;
				return true;
			case 4:
				value = this.m_Booster;
				return true;
			case 5:
				value = this.m_CardBack;
				return true;
			case 6:
				value = this.m_Currency;
				return true;
			case 7:
				value = this.m_AssetId;
				return true;
			default:
				if (id == 110)
				{
					value = this.m_Card;
					return true;
				}
				if (id != 111)
				{
					value = null;
					return false;
				}
				value = this.m_RandomCard;
				return true;
			}
		}

		// Token: 0x0600BD04 RID: 48388 RVA: 0x0039A764 File Offset: 0x00398964
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.PmtLicenseId = ((value != null) ? ((long)value) : 0L);
				return true;
			case 1:
				this.ItemType = ((value != null) ? ((RewardItemType)value) : RewardItemType.UNDEFINED);
				return true;
			case 2:
				this.ItemId = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				this.Quantity = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				this.Booster = ((value != null) ? ((PackDataModel)value) : null);
				return true;
			case 5:
				this.CardBack = ((value != null) ? ((CardBackDataModel)value) : null);
				return true;
			case 6:
				this.Currency = ((value != null) ? ((PriceDataModel)value) : null);
				return true;
			case 7:
				this.AssetId = ((value != null) ? ((int)value) : 0);
				return true;
			default:
				if (id == 110)
				{
					this.Card = ((value != null) ? ((CardDataModel)value) : null);
					return true;
				}
				if (id != 111)
				{
					return false;
				}
				this.RandomCard = ((value != null) ? ((RandomCardDataModel)value) : null);
				return true;
			}
		}

		// Token: 0x0600BD05 RID: 48389 RVA: 0x0039A878 File Offset: 0x00398A78
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 0:
				info = this.Properties[0];
				return true;
			case 1:
				info = this.Properties[1];
				return true;
			case 2:
				info = this.Properties[2];
				return true;
			case 3:
				info = this.Properties[3];
				return true;
			case 4:
				info = this.Properties[4];
				return true;
			case 5:
				info = this.Properties[5];
				return true;
			case 6:
				info = this.Properties[6];
				return true;
			case 7:
				info = this.Properties[7];
				return true;
			default:
				if (id == 110)
				{
					info = this.Properties[8];
					return true;
				}
				if (id != 111)
				{
					info = default(DataModelProperty);
					return false;
				}
				info = this.Properties[9];
				return true;
			}
		}

		// Token: 0x04009A43 RID: 39491
		public const int ModelId = 17;

		// Token: 0x04009A44 RID: 39492
		private long m_PmtLicenseId;

		// Token: 0x04009A45 RID: 39493
		private RewardItemType m_ItemType;

		// Token: 0x04009A46 RID: 39494
		private int m_ItemId;

		// Token: 0x04009A47 RID: 39495
		private int m_Quantity;

		// Token: 0x04009A48 RID: 39496
		private PackDataModel m_Booster;

		// Token: 0x04009A49 RID: 39497
		private CardBackDataModel m_CardBack;

		// Token: 0x04009A4A RID: 39498
		private PriceDataModel m_Currency;

		// Token: 0x04009A4B RID: 39499
		private int m_AssetId;

		// Token: 0x04009A4C RID: 39500
		private CardDataModel m_Card;

		// Token: 0x04009A4D RID: 39501
		private RandomCardDataModel m_RandomCard;

		// Token: 0x04009A4E RID: 39502
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "pmt_license_id",
				Type = typeof(long)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "item_type",
				Type = typeof(RewardItemType)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "item_id",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "quantity",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "booster",
				Type = typeof(PackDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "card_back",
				Type = typeof(CardBackDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "currency",
				Type = typeof(PriceDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "asset_id",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 110,
				PropertyDisplayName = "card",
				Type = typeof(CardDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 111,
				PropertyDisplayName = "random_card",
				Type = typeof(RandomCardDataModel)
			}
		};
	}
}
