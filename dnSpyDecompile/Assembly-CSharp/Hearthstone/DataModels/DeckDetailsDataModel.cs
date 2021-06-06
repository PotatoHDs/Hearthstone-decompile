using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010B1 RID: 4273
	public class DeckDetailsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BA5C RID: 47708 RVA: 0x003903A0 File Offset: 0x0038E5A0
		public DeckDetailsDataModel()
		{
			base.RegisterNestedDataModel(this.m_Product);
			base.RegisterNestedDataModel(this.m_MiniSetDetails);
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x0600BA5D RID: 47709 RVA: 0x00390441 File Offset: 0x0038E641
		public int DataModelId
		{
			get
			{
				return 290;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x0600BA5E RID: 47710 RVA: 0x00390448 File Offset: 0x0038E648
		public string DataModelDisplayName
		{
			get
			{
				return "deck_details";
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x0600BA60 RID: 47712 RVA: 0x00390488 File Offset: 0x0038E688
		// (set) Token: 0x0600BA5F RID: 47711 RVA: 0x0039044F File Offset: 0x0038E64F
		public ProductDataModel Product
		{
			get
			{
				return this.m_Product;
			}
			set
			{
				if (this.m_Product == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Product);
				base.RegisterNestedDataModel(value);
				this.m_Product = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x0600BA62 RID: 47714 RVA: 0x003904C9 File Offset: 0x0038E6C9
		// (set) Token: 0x0600BA61 RID: 47713 RVA: 0x00390490 File Offset: 0x0038E690
		public MiniSetDetailsDataModel MiniSetDetails
		{
			get
			{
				return this.m_MiniSetDetails;
			}
			set
			{
				if (this.m_MiniSetDetails == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_MiniSetDetails);
				base.RegisterNestedDataModel(value);
				this.m_MiniSetDetails = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600BA63 RID: 47715 RVA: 0x003904D1 File Offset: 0x0038E6D1
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BA64 RID: 47716 RVA: 0x003904D9 File Offset: 0x0038E6D9
		public int GetPropertiesHashCode()
		{
			return (17 * 31 + ((this.m_Product != null) ? this.m_Product.GetPropertiesHashCode() : 0)) * 31 + ((this.m_MiniSetDetails != null) ? this.m_MiniSetDetails.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BA65 RID: 47717 RVA: 0x00390511 File Offset: 0x0038E711
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Product;
				return true;
			}
			if (id != 1)
			{
				value = null;
				return false;
			}
			value = this.m_MiniSetDetails;
			return true;
		}

		// Token: 0x0600BA66 RID: 47718 RVA: 0x00390534 File Offset: 0x0038E734
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Product = ((value != null) ? ((ProductDataModel)value) : null);
				return true;
			}
			if (id != 1)
			{
				return false;
			}
			this.MiniSetDetails = ((value != null) ? ((MiniSetDetailsDataModel)value) : null);
			return true;
		}

		// Token: 0x0600BA67 RID: 47719 RVA: 0x00390568 File Offset: 0x0038E768
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 0)
			{
				info = this.Properties[0];
				return true;
			}
			if (id != 1)
			{
				info = default(DataModelProperty);
				return false;
			}
			info = this.Properties[1];
			return true;
		}

		// Token: 0x04009946 RID: 39238
		public const int ModelId = 290;

		// Token: 0x04009947 RID: 39239
		private ProductDataModel m_Product;

		// Token: 0x04009948 RID: 39240
		private MiniSetDetailsDataModel m_MiniSetDetails;

		// Token: 0x04009949 RID: 39241
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "product",
				Type = typeof(ProductDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "mini_set_details",
				Type = typeof(MiniSetDetailsDataModel)
			}
		};
	}
}
