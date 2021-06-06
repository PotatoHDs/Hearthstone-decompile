using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010C6 RID: 4294
	public class ProductSelectionDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BB9E RID: 48030 RVA: 0x00394980 File Offset: 0x00392B80
		public ProductSelectionDataModel()
		{
			base.RegisterNestedDataModel(this.m_Variant);
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x0600BB9F RID: 48031 RVA: 0x00394A7F File Offset: 0x00392C7F
		public int DataModelId
		{
			get
			{
				return 30;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x0600BBA0 RID: 48032 RVA: 0x00394A83 File Offset: 0x00392C83
		public string DataModelDisplayName
		{
			get
			{
				return "product_selection";
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x0600BBA2 RID: 48034 RVA: 0x00394AC3 File Offset: 0x00392CC3
		// (set) Token: 0x0600BBA1 RID: 48033 RVA: 0x00394A8A File Offset: 0x00392C8A
		public ProductDataModel Variant
		{
			get
			{
				return this.m_Variant;
			}
			set
			{
				if (this.m_Variant == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Variant);
				base.RegisterNestedDataModel(value);
				this.m_Variant = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x0600BBA4 RID: 48036 RVA: 0x00394AF1 File Offset: 0x00392CF1
		// (set) Token: 0x0600BBA3 RID: 48035 RVA: 0x00394ACB File Offset: 0x00392CCB
		public int VariantIndex
		{
			get
			{
				return this.m_VariantIndex;
			}
			set
			{
				if (this.m_VariantIndex == value)
				{
					return;
				}
				this.m_VariantIndex = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x0600BBA6 RID: 48038 RVA: 0x00394B1F File Offset: 0x00392D1F
		// (set) Token: 0x0600BBA5 RID: 48037 RVA: 0x00394AF9 File Offset: 0x00392CF9
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

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x0600BBA8 RID: 48040 RVA: 0x00394B4D File Offset: 0x00392D4D
		// (set) Token: 0x0600BBA7 RID: 48039 RVA: 0x00394B27 File Offset: 0x00392D27
		public int MaxQuantity
		{
			get
			{
				return this.m_MaxQuantity;
			}
			set
			{
				if (this.m_MaxQuantity == value)
				{
					return;
				}
				this.m_MaxQuantity = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x0600BBA9 RID: 48041 RVA: 0x00394B55 File Offset: 0x00392D55
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BBAA RID: 48042 RVA: 0x00394B60 File Offset: 0x00392D60
		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((this.m_Variant != null) ? this.m_Variant.GetPropertiesHashCode() : 0)) * 31;
			int variantIndex = this.m_VariantIndex;
			int num2 = (num + this.m_VariantIndex.GetHashCode()) * 31;
			int quantity = this.m_Quantity;
			int num3 = (num2 + this.m_Quantity.GetHashCode()) * 31;
			int maxQuantity = this.m_MaxQuantity;
			return num3 + this.m_MaxQuantity.GetHashCode();
		}

		// Token: 0x0600BBAB RID: 48043 RVA: 0x00394BCC File Offset: 0x00392DCC
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Variant;
				return true;
			case 1:
				value = this.m_VariantIndex;
				return true;
			case 2:
				value = this.m_Quantity;
				return true;
			case 3:
				value = this.m_MaxQuantity;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BBAC RID: 48044 RVA: 0x00394C2C File Offset: 0x00392E2C
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Variant = ((value != null) ? ((ProductDataModel)value) : null);
				return true;
			case 1:
				this.VariantIndex = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.Quantity = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				this.MaxQuantity = ((value != null) ? ((int)value) : 0);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BBAD RID: 48045 RVA: 0x00394CA4 File Offset: 0x00392EA4
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x040099BD RID: 39357
		public const int ModelId = 30;

		// Token: 0x040099BE RID: 39358
		private ProductDataModel m_Variant;

		// Token: 0x040099BF RID: 39359
		private int m_VariantIndex;

		// Token: 0x040099C0 RID: 39360
		private int m_Quantity;

		// Token: 0x040099C1 RID: 39361
		private int m_MaxQuantity;

		// Token: 0x040099C2 RID: 39362
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "variant",
				Type = typeof(ProductDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "variant_index",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "quantity",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "max_quantity",
				Type = typeof(int)
			}
		};
	}
}
