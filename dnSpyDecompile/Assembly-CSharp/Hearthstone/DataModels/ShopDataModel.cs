using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010DF RID: 4319
	public class ShopDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BD94 RID: 48532 RVA: 0x0039CBC8 File Offset: 0x0039ADC8
		public ShopDataModel()
		{
			base.RegisterNestedDataModel(this.m_Tiers);
			base.RegisterNestedDataModel(this.m_VirtualCurrency);
			base.RegisterNestedDataModel(this.m_BoosterCurrency);
			base.RegisterNestedDataModel(this.m_VirtualCurrencyBalance);
			base.RegisterNestedDataModel(this.m_BoosterCurrencyBalance);
			base.RegisterNestedDataModel(this.m_GoldBalance);
			base.RegisterNestedDataModel(this.m_DustBalance);
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x0600BD95 RID: 48533 RVA: 0x0039CE93 File Offset: 0x0039B093
		public int DataModelId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x0600BD96 RID: 48534 RVA: 0x0039CE97 File Offset: 0x0039B097
		public string DataModelDisplayName
		{
			get
			{
				return "shop";
			}
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x0600BD98 RID: 48536 RVA: 0x0039CEC4 File Offset: 0x0039B0C4
		// (set) Token: 0x0600BD97 RID: 48535 RVA: 0x0039CE9E File Offset: 0x0039B09E
		public bool IsWild
		{
			get
			{
				return this.m_IsWild;
			}
			set
			{
				if (this.m_IsWild == value)
				{
					return;
				}
				this.m_IsWild = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x0600BD9A RID: 48538 RVA: 0x0039CF05 File Offset: 0x0039B105
		// (set) Token: 0x0600BD99 RID: 48537 RVA: 0x0039CECC File Offset: 0x0039B0CC
		public DataModelList<ProductTierDataModel> Tiers
		{
			get
			{
				return this.m_Tiers;
			}
			set
			{
				if (this.m_Tiers == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Tiers);
				base.RegisterNestedDataModel(value);
				this.m_Tiers = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x0600BD9C RID: 48540 RVA: 0x0039CF46 File Offset: 0x0039B146
		// (set) Token: 0x0600BD9B RID: 48539 RVA: 0x0039CF0D File Offset: 0x0039B10D
		public ProductDataModel VirtualCurrency
		{
			get
			{
				return this.m_VirtualCurrency;
			}
			set
			{
				if (this.m_VirtualCurrency == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_VirtualCurrency);
				base.RegisterNestedDataModel(value);
				this.m_VirtualCurrency = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x0600BD9E RID: 48542 RVA: 0x0039CF87 File Offset: 0x0039B187
		// (set) Token: 0x0600BD9D RID: 48541 RVA: 0x0039CF4E File Offset: 0x0039B14E
		public ProductDataModel BoosterCurrency
		{
			get
			{
				return this.m_BoosterCurrency;
			}
			set
			{
				if (this.m_BoosterCurrency == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_BoosterCurrency);
				base.RegisterNestedDataModel(value);
				this.m_BoosterCurrency = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x0600BDA0 RID: 48544 RVA: 0x0039CFB5 File Offset: 0x0039B1B5
		// (set) Token: 0x0600BD9F RID: 48543 RVA: 0x0039CF8F File Offset: 0x0039B18F
		public bool AutoconvertCurrency
		{
			get
			{
				return this.m_AutoconvertCurrency;
			}
			set
			{
				if (this.m_AutoconvertCurrency == value)
				{
					return;
				}
				this.m_AutoconvertCurrency = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x0600BDA2 RID: 48546 RVA: 0x0039CFF6 File Offset: 0x0039B1F6
		// (set) Token: 0x0600BDA1 RID: 48545 RVA: 0x0039CFBD File Offset: 0x0039B1BD
		public PriceDataModel VirtualCurrencyBalance
		{
			get
			{
				return this.m_VirtualCurrencyBalance;
			}
			set
			{
				if (this.m_VirtualCurrencyBalance == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_VirtualCurrencyBalance);
				base.RegisterNestedDataModel(value);
				this.m_VirtualCurrencyBalance = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x0600BDA4 RID: 48548 RVA: 0x0039D037 File Offset: 0x0039B237
		// (set) Token: 0x0600BDA3 RID: 48547 RVA: 0x0039CFFE File Offset: 0x0039B1FE
		public PriceDataModel BoosterCurrencyBalance
		{
			get
			{
				return this.m_BoosterCurrencyBalance;
			}
			set
			{
				if (this.m_BoosterCurrencyBalance == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_BoosterCurrencyBalance);
				base.RegisterNestedDataModel(value);
				this.m_BoosterCurrencyBalance = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x0600BDA6 RID: 48550 RVA: 0x0039D078 File Offset: 0x0039B278
		// (set) Token: 0x0600BDA5 RID: 48549 RVA: 0x0039D03F File Offset: 0x0039B23F
		public PriceDataModel GoldBalance
		{
			get
			{
				return this.m_GoldBalance;
			}
			set
			{
				if (this.m_GoldBalance == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_GoldBalance);
				base.RegisterNestedDataModel(value);
				this.m_GoldBalance = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x0600BDA8 RID: 48552 RVA: 0x0039D0B9 File Offset: 0x0039B2B9
		// (set) Token: 0x0600BDA7 RID: 48551 RVA: 0x0039D080 File Offset: 0x0039B280
		public PriceDataModel DustBalance
		{
			get
			{
				return this.m_DustBalance;
			}
			set
			{
				if (this.m_DustBalance == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_DustBalance);
				base.RegisterNestedDataModel(value);
				this.m_DustBalance = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x0600BDAA RID: 48554 RVA: 0x0039D0E7 File Offset: 0x0039B2E7
		// (set) Token: 0x0600BDA9 RID: 48553 RVA: 0x0039D0C1 File Offset: 0x0039B2C1
		public bool HasNewItems
		{
			get
			{
				return this.m_HasNewItems;
			}
			set
			{
				if (this.m_HasNewItems == value)
				{
					return;
				}
				this.m_HasNewItems = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x0600BDAC RID: 48556 RVA: 0x0039D115 File Offset: 0x0039B315
		// (set) Token: 0x0600BDAB RID: 48555 RVA: 0x0039D0EF File Offset: 0x0039B2EF
		public int TavernTicketBalance
		{
			get
			{
				return this.m_TavernTicketBalance;
			}
			set
			{
				if (this.m_TavernTicketBalance == value)
				{
					return;
				}
				this.m_TavernTicketBalance = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x0600BDAD RID: 48557 RVA: 0x0039D11D File Offset: 0x0039B31D
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BDAE RID: 48558 RVA: 0x0039D128 File Offset: 0x0039B328
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			bool isWild = this.m_IsWild;
			int num2 = ((((num + this.m_IsWild.GetHashCode()) * 31 + ((this.m_Tiers != null) ? this.m_Tiers.GetPropertiesHashCode() : 0)) * 31 + ((this.m_VirtualCurrency != null) ? this.m_VirtualCurrency.GetPropertiesHashCode() : 0)) * 31 + ((this.m_BoosterCurrency != null) ? this.m_BoosterCurrency.GetPropertiesHashCode() : 0)) * 31;
			bool autoconvertCurrency = this.m_AutoconvertCurrency;
			int num3 = (((((num2 + this.m_AutoconvertCurrency.GetHashCode()) * 31 + ((this.m_VirtualCurrencyBalance != null) ? this.m_VirtualCurrencyBalance.GetPropertiesHashCode() : 0)) * 31 + ((this.m_BoosterCurrencyBalance != null) ? this.m_BoosterCurrencyBalance.GetPropertiesHashCode() : 0)) * 31 + ((this.m_GoldBalance != null) ? this.m_GoldBalance.GetPropertiesHashCode() : 0)) * 31 + ((this.m_DustBalance != null) ? this.m_DustBalance.GetPropertiesHashCode() : 0)) * 31;
			bool hasNewItems = this.m_HasNewItems;
			int num4 = (num3 + this.m_HasNewItems.GetHashCode()) * 31;
			int tavernTicketBalance = this.m_TavernTicketBalance;
			return num4 + this.m_TavernTicketBalance.GetHashCode();
		}

		// Token: 0x0600BDAF RID: 48559 RVA: 0x0039D248 File Offset: 0x0039B448
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_IsWild;
				return true;
			case 1:
				value = this.m_Tiers;
				return true;
			case 3:
				value = this.m_VirtualCurrency;
				return true;
			case 4:
				value = this.m_BoosterCurrency;
				return true;
			case 5:
				value = this.m_AutoconvertCurrency;
				return true;
			case 6:
				value = this.m_VirtualCurrencyBalance;
				return true;
			case 7:
				value = this.m_BoosterCurrencyBalance;
				return true;
			case 8:
				value = this.m_GoldBalance;
				return true;
			case 9:
				value = this.m_DustBalance;
				return true;
			case 10:
				value = this.m_HasNewItems;
				return true;
			case 11:
				value = this.m_TavernTicketBalance;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BDB0 RID: 48560 RVA: 0x0039D318 File Offset: 0x0039B518
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.IsWild = (value != null && (bool)value);
				return true;
			case 1:
				this.Tiers = ((value != null) ? ((DataModelList<ProductTierDataModel>)value) : null);
				return true;
			case 3:
				this.VirtualCurrency = ((value != null) ? ((ProductDataModel)value) : null);
				return true;
			case 4:
				this.BoosterCurrency = ((value != null) ? ((ProductDataModel)value) : null);
				return true;
			case 5:
				this.AutoconvertCurrency = (value != null && (bool)value);
				return true;
			case 6:
				this.VirtualCurrencyBalance = ((value != null) ? ((PriceDataModel)value) : null);
				return true;
			case 7:
				this.BoosterCurrencyBalance = ((value != null) ? ((PriceDataModel)value) : null);
				return true;
			case 8:
				this.GoldBalance = ((value != null) ? ((PriceDataModel)value) : null);
				return true;
			case 9:
				this.DustBalance = ((value != null) ? ((PriceDataModel)value) : null);
				return true;
			case 10:
				this.HasNewItems = (value != null && (bool)value);
				return true;
			case 11:
				this.TavernTicketBalance = ((value != null) ? ((int)value) : 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600BDB1 RID: 48561 RVA: 0x0039D440 File Offset: 0x0039B640
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
			case 3:
				info = this.Properties[2];
				return true;
			case 4:
				info = this.Properties[3];
				return true;
			case 5:
				info = this.Properties[4];
				return true;
			case 6:
				info = this.Properties[5];
				return true;
			case 7:
				info = this.Properties[6];
				return true;
			case 8:
				info = this.Properties[7];
				return true;
			case 9:
				info = this.Properties[8];
				return true;
			case 10:
				info = this.Properties[9];
				return true;
			case 11:
				info = this.Properties[10];
				return true;
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x04009A86 RID: 39558
		public const int ModelId = 24;

		// Token: 0x04009A87 RID: 39559
		private bool m_IsWild;

		// Token: 0x04009A88 RID: 39560
		private DataModelList<ProductTierDataModel> m_Tiers = new DataModelList<ProductTierDataModel>();

		// Token: 0x04009A89 RID: 39561
		private ProductDataModel m_VirtualCurrency;

		// Token: 0x04009A8A RID: 39562
		private ProductDataModel m_BoosterCurrency;

		// Token: 0x04009A8B RID: 39563
		private bool m_AutoconvertCurrency;

		// Token: 0x04009A8C RID: 39564
		private PriceDataModel m_VirtualCurrencyBalance;

		// Token: 0x04009A8D RID: 39565
		private PriceDataModel m_BoosterCurrencyBalance;

		// Token: 0x04009A8E RID: 39566
		private PriceDataModel m_GoldBalance;

		// Token: 0x04009A8F RID: 39567
		private PriceDataModel m_DustBalance;

		// Token: 0x04009A90 RID: 39568
		private bool m_HasNewItems;

		// Token: 0x04009A91 RID: 39569
		private int m_TavernTicketBalance;

		// Token: 0x04009A92 RID: 39570
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "is_wild",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "tiers",
				Type = typeof(DataModelList<ProductTierDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "virtual_currency",
				Type = typeof(ProductDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "booster_currency",
				Type = typeof(ProductDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "autoconvert_currency",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "virtual_currency_balance",
				Type = typeof(PriceDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "booster_currency_balance",
				Type = typeof(PriceDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "gold_balance",
				Type = typeof(PriceDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "dust_balance",
				Type = typeof(PriceDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "has_new_items",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "tavern_ticket_balance",
				Type = typeof(int)
			}
		};
	}
}
