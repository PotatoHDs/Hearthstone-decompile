using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010C5 RID: 4293
	public class ProductDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BB7E RID: 47998 RVA: 0x00393F04 File Offset: 0x00392104
		public ProductDataModel()
		{
			base.RegisterNestedDataModel(this.m_Tags);
			base.RegisterNestedDataModel(this.m_Items);
			base.RegisterNestedDataModel(this.m_Prices);
			base.RegisterNestedDataModel(this.m_Variants);
			base.RegisterNestedDataModel(this.m_RewardList);
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x0600BB7F RID: 47999 RVA: 0x0039420F File Offset: 0x0039240F
		public int DataModelId
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x0600BB80 RID: 48000 RVA: 0x00394213 File Offset: 0x00392413
		public string DataModelDisplayName
		{
			get
			{
				return "product";
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x0600BB82 RID: 48002 RVA: 0x00394240 File Offset: 0x00392440
		// (set) Token: 0x0600BB81 RID: 48001 RVA: 0x0039421A File Offset: 0x0039241A
		public long PmtId
		{
			get
			{
				return this.m_PmtId;
			}
			set
			{
				if (this.m_PmtId == value)
				{
					return;
				}
				this.m_PmtId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600BB84 RID: 48004 RVA: 0x00394273 File Offset: 0x00392473
		// (set) Token: 0x0600BB83 RID: 48003 RVA: 0x00394248 File Offset: 0x00392448
		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				if (this.m_Name == value)
				{
					return;
				}
				this.m_Name = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600BB86 RID: 48006 RVA: 0x003942A6 File Offset: 0x003924A6
		// (set) Token: 0x0600BB85 RID: 48005 RVA: 0x0039427B File Offset: 0x0039247B
		public string Description
		{
			get
			{
				return this.m_Description;
			}
			set
			{
				if (this.m_Description == value)
				{
					return;
				}
				this.m_Description = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x0600BB88 RID: 48008 RVA: 0x003942E7 File Offset: 0x003924E7
		// (set) Token: 0x0600BB87 RID: 48007 RVA: 0x003942AE File Offset: 0x003924AE
		public DataModelList<string> Tags
		{
			get
			{
				return this.m_Tags;
			}
			set
			{
				if (this.m_Tags == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Tags);
				base.RegisterNestedDataModel(value);
				this.m_Tags = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x0600BB8A RID: 48010 RVA: 0x00394328 File Offset: 0x00392528
		// (set) Token: 0x0600BB89 RID: 48009 RVA: 0x003942EF File Offset: 0x003924EF
		public DataModelList<RewardItemDataModel> Items
		{
			get
			{
				return this.m_Items;
			}
			set
			{
				if (this.m_Items == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Items);
				base.RegisterNestedDataModel(value);
				this.m_Items = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x0600BB8C RID: 48012 RVA: 0x00394369 File Offset: 0x00392569
		// (set) Token: 0x0600BB8B RID: 48011 RVA: 0x00394330 File Offset: 0x00392530
		public DataModelList<PriceDataModel> Prices
		{
			get
			{
				return this.m_Prices;
			}
			set
			{
				if (this.m_Prices == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Prices);
				base.RegisterNestedDataModel(value);
				this.m_Prices = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x0600BB8E RID: 48014 RVA: 0x003943AA File Offset: 0x003925AA
		// (set) Token: 0x0600BB8D RID: 48013 RVA: 0x00394371 File Offset: 0x00392571
		public DataModelList<ProductDataModel> Variants
		{
			get
			{
				return this.m_Variants;
			}
			set
			{
				if (this.m_Variants == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Variants);
				base.RegisterNestedDataModel(value);
				this.m_Variants = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x0600BB90 RID: 48016 RVA: 0x003943D8 File Offset: 0x003925D8
		// (set) Token: 0x0600BB8F RID: 48015 RVA: 0x003943B2 File Offset: 0x003925B2
		public ProductAvailability Availability
		{
			get
			{
				return this.m_Availability;
			}
			set
			{
				if (this.m_Availability == value)
				{
					return;
				}
				this.m_Availability = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x0600BB92 RID: 48018 RVA: 0x00394419 File Offset: 0x00392619
		// (set) Token: 0x0600BB91 RID: 48017 RVA: 0x003943E0 File Offset: 0x003925E0
		public RewardListDataModel RewardList
		{
			get
			{
				return this.m_RewardList;
			}
			set
			{
				if (this.m_RewardList == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_RewardList);
				base.RegisterNestedDataModel(value);
				this.m_RewardList = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x0600BB94 RID: 48020 RVA: 0x0039444C File Offset: 0x0039264C
		// (set) Token: 0x0600BB93 RID: 48019 RVA: 0x00394421 File Offset: 0x00392621
		public string DescriptionHeader
		{
			get
			{
				return this.m_DescriptionHeader;
			}
			set
			{
				if (this.m_DescriptionHeader == value)
				{
					return;
				}
				this.m_DescriptionHeader = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x0600BB96 RID: 48022 RVA: 0x0039447F File Offset: 0x0039267F
		// (set) Token: 0x0600BB95 RID: 48021 RVA: 0x00394454 File Offset: 0x00392654
		public string VariantName
		{
			get
			{
				return this.m_VariantName;
			}
			set
			{
				if (this.m_VariantName == value)
				{
					return;
				}
				this.m_VariantName = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x0600BB98 RID: 48024 RVA: 0x003944B2 File Offset: 0x003926B2
		// (set) Token: 0x0600BB97 RID: 48023 RVA: 0x00394487 File Offset: 0x00392687
		public string FlavorText
		{
			get
			{
				return this.m_FlavorText;
			}
			set
			{
				if (this.m_FlavorText == value)
				{
					return;
				}
				this.m_FlavorText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x0600BB99 RID: 48025 RVA: 0x003944BA File Offset: 0x003926BA
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BB9A RID: 48026 RVA: 0x003944C4 File Offset: 0x003926C4
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			long pmtId = this.m_PmtId;
			int num2 = (((((((num + this.m_PmtId.GetHashCode()) * 31 + ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0)) * 31 + ((this.m_Description != null) ? this.m_Description.GetHashCode() : 0)) * 31 + ((this.m_Tags != null) ? this.m_Tags.GetPropertiesHashCode() : 0)) * 31 + ((this.m_Items != null) ? this.m_Items.GetPropertiesHashCode() : 0)) * 31 + ((this.m_Prices != null) ? this.m_Prices.GetPropertiesHashCode() : 0)) * 31 + ((this.m_Variants != null) ? this.m_Variants.GetPropertiesHashCode() : 0)) * 31;
			ProductAvailability availability = this.m_Availability;
			return ((((num2 + this.m_Availability.GetHashCode()) * 31 + ((this.m_RewardList != null) ? this.m_RewardList.GetPropertiesHashCode() : 0)) * 31 + ((this.m_DescriptionHeader != null) ? this.m_DescriptionHeader.GetHashCode() : 0)) * 31 + ((this.m_VariantName != null) ? this.m_VariantName.GetHashCode() : 0)) * 31 + ((this.m_FlavorText != null) ? this.m_FlavorText.GetHashCode() : 0);
		}

		// Token: 0x0600BB9B RID: 48027 RVA: 0x0039460C File Offset: 0x0039280C
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_PmtId;
				return true;
			case 1:
				value = this.m_Name;
				return true;
			case 2:
				value = this.m_Description;
				return true;
			case 3:
				value = this.m_Tags;
				return true;
			case 4:
				value = this.m_Items;
				return true;
			case 5:
				value = this.m_Prices;
				return true;
			case 6:
				value = this.m_Variants;
				return true;
			case 7:
				value = this.m_Availability;
				return true;
			default:
				switch (id)
				{
				case 36:
					value = this.m_RewardList;
					return true;
				case 38:
					value = this.m_DescriptionHeader;
					return true;
				case 39:
					value = this.m_VariantName;
					return true;
				case 40:
					value = this.m_FlavorText;
					return true;
				}
				value = null;
				return false;
			}
		}

		// Token: 0x0600BB9C RID: 48028 RVA: 0x003946E8 File Offset: 0x003928E8
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.PmtId = ((value != null) ? ((long)value) : 0L);
				return true;
			case 1:
				this.Name = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				this.Description = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				this.Tags = ((value != null) ? ((DataModelList<string>)value) : null);
				return true;
			case 4:
				this.Items = ((value != null) ? ((DataModelList<RewardItemDataModel>)value) : null);
				return true;
			case 5:
				this.Prices = ((value != null) ? ((DataModelList<PriceDataModel>)value) : null);
				return true;
			case 6:
				this.Variants = ((value != null) ? ((DataModelList<ProductDataModel>)value) : null);
				return true;
			case 7:
				this.Availability = ((value != null) ? ((ProductAvailability)value) : ProductAvailability.UNDEFINED);
				return true;
			default:
				switch (id)
				{
				case 36:
					this.RewardList = ((value != null) ? ((RewardListDataModel)value) : null);
					return true;
				case 38:
					this.DescriptionHeader = ((value != null) ? ((string)value) : null);
					return true;
				case 39:
					this.VariantName = ((value != null) ? ((string)value) : null);
					return true;
				case 40:
					this.FlavorText = ((value != null) ? ((string)value) : null);
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600BB9D RID: 48029 RVA: 0x00394830 File Offset: 0x00392A30
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
				switch (id)
				{
				case 36:
					info = this.Properties[8];
					return true;
				case 38:
					info = this.Properties[9];
					return true;
				case 39:
					info = this.Properties[10];
					return true;
				case 40:
					info = this.Properties[11];
					return true;
				}
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x040099AF RID: 39343
		public const int ModelId = 15;

		// Token: 0x040099B0 RID: 39344
		private long m_PmtId;

		// Token: 0x040099B1 RID: 39345
		private string m_Name;

		// Token: 0x040099B2 RID: 39346
		private string m_Description;

		// Token: 0x040099B3 RID: 39347
		private DataModelList<string> m_Tags = new DataModelList<string>();

		// Token: 0x040099B4 RID: 39348
		private DataModelList<RewardItemDataModel> m_Items = new DataModelList<RewardItemDataModel>();

		// Token: 0x040099B5 RID: 39349
		private DataModelList<PriceDataModel> m_Prices = new DataModelList<PriceDataModel>();

		// Token: 0x040099B6 RID: 39350
		private DataModelList<ProductDataModel> m_Variants = new DataModelList<ProductDataModel>();

		// Token: 0x040099B7 RID: 39351
		private ProductAvailability m_Availability;

		// Token: 0x040099B8 RID: 39352
		private RewardListDataModel m_RewardList;

		// Token: 0x040099B9 RID: 39353
		private string m_DescriptionHeader;

		// Token: 0x040099BA RID: 39354
		private string m_VariantName;

		// Token: 0x040099BB RID: 39355
		private string m_FlavorText;

		// Token: 0x040099BC RID: 39356
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "pmt_id",
				Type = typeof(long)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "description",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "tags",
				Type = typeof(DataModelList<string>)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "items",
				Type = typeof(DataModelList<RewardItemDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "prices",
				Type = typeof(DataModelList<PriceDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "variants",
				Type = typeof(DataModelList<ProductDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "availability",
				Type = typeof(ProductAvailability)
			},
			new DataModelProperty
			{
				PropertyId = 36,
				PropertyDisplayName = "reward_list",
				Type = typeof(RewardListDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 38,
				PropertyDisplayName = "description_header",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 39,
				PropertyDisplayName = "variant_name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 40,
				PropertyDisplayName = "flavor_text",
				Type = typeof(string)
			}
		};
	}
}
