using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010DE RID: 4318
	public class ShopBrowserButtonDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BD80 RID: 48512 RVA: 0x0039C690 File Offset: 0x0039A890
		public ShopBrowserButtonDataModel()
		{
			base.RegisterNestedDataModel(this.m_DisplayProduct);
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x0600BD81 RID: 48513 RVA: 0x0039C7F9 File Offset: 0x0039A9F9
		public int DataModelId
		{
			get
			{
				return 19;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x0600BD82 RID: 48514 RVA: 0x0039C7FD File Offset: 0x0039A9FD
		public string DataModelDisplayName
		{
			get
			{
				return "shop_browser_button";
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x0600BD84 RID: 48516 RVA: 0x0039C82F File Offset: 0x0039AA2F
		// (set) Token: 0x0600BD83 RID: 48515 RVA: 0x0039C804 File Offset: 0x0039AA04
		public string DisplayText
		{
			get
			{
				return this.m_DisplayText;
			}
			set
			{
				if (this.m_DisplayText == value)
				{
					return;
				}
				this.m_DisplayText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x0600BD86 RID: 48518 RVA: 0x0039C870 File Offset: 0x0039AA70
		// (set) Token: 0x0600BD85 RID: 48517 RVA: 0x0039C837 File Offset: 0x0039AA37
		public ProductDataModel DisplayProduct
		{
			get
			{
				return this.m_DisplayProduct;
			}
			set
			{
				if (this.m_DisplayProduct == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_DisplayProduct);
				base.RegisterNestedDataModel(value);
				this.m_DisplayProduct = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x0600BD88 RID: 48520 RVA: 0x0039C89E File Offset: 0x0039AA9E
		// (set) Token: 0x0600BD87 RID: 48519 RVA: 0x0039C878 File Offset: 0x0039AA78
		public float SlotWidth
		{
			get
			{
				return this.m_SlotWidth;
			}
			set
			{
				if (this.m_SlotWidth == value)
				{
					return;
				}
				this.m_SlotWidth = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x0600BD8A RID: 48522 RVA: 0x0039C8CC File Offset: 0x0039AACC
		// (set) Token: 0x0600BD89 RID: 48521 RVA: 0x0039C8A6 File Offset: 0x0039AAA6
		public float SlotHeight
		{
			get
			{
				return this.m_SlotHeight;
			}
			set
			{
				if (this.m_SlotHeight == value)
				{
					return;
				}
				this.m_SlotHeight = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x0600BD8C RID: 48524 RVA: 0x0039C8FA File Offset: 0x0039AAFA
		// (set) Token: 0x0600BD8B RID: 48523 RVA: 0x0039C8D4 File Offset: 0x0039AAD4
		public bool Hovered
		{
			get
			{
				return this.m_Hovered;
			}
			set
			{
				if (this.m_Hovered == value)
				{
					return;
				}
				this.m_Hovered = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x0600BD8E RID: 48526 RVA: 0x0039C928 File Offset: 0x0039AB28
		// (set) Token: 0x0600BD8D RID: 48525 RVA: 0x0039C902 File Offset: 0x0039AB02
		public bool IsFiller
		{
			get
			{
				return this.m_IsFiller;
			}
			set
			{
				if (this.m_IsFiller == value)
				{
					return;
				}
				this.m_IsFiller = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x0600BD8F RID: 48527 RVA: 0x0039C930 File Offset: 0x0039AB30
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BD90 RID: 48528 RVA: 0x0039C938 File Offset: 0x0039AB38
		public int GetPropertiesHashCode()
		{
			int num = ((17 * 31 + ((this.m_DisplayText != null) ? this.m_DisplayText.GetHashCode() : 0)) * 31 + ((this.m_DisplayProduct != null) ? this.m_DisplayProduct.GetPropertiesHashCode() : 0)) * 31;
			float slotWidth = this.m_SlotWidth;
			int num2 = (num + this.m_SlotWidth.GetHashCode()) * 31;
			float slotHeight = this.m_SlotHeight;
			int num3 = (num2 + this.m_SlotHeight.GetHashCode()) * 31;
			bool hovered = this.m_Hovered;
			int num4 = (num3 + this.m_Hovered.GetHashCode()) * 31;
			bool isFiller = this.m_IsFiller;
			return num4 + this.m_IsFiller.GetHashCode();
		}

		// Token: 0x0600BD91 RID: 48529 RVA: 0x0039C9D4 File Offset: 0x0039ABD4
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_DisplayText;
				return true;
			case 1:
				value = this.m_DisplayProduct;
				return true;
			case 3:
				value = this.m_SlotWidth;
				return true;
			case 4:
				value = this.m_SlotHeight;
				return true;
			case 5:
				value = this.m_Hovered;
				return true;
			case 6:
				value = this.m_IsFiller;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BD92 RID: 48530 RVA: 0x0039CA5C File Offset: 0x0039AC5C
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.DisplayText = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				this.DisplayProduct = ((value != null) ? ((ProductDataModel)value) : null);
				return true;
			case 3:
				this.SlotWidth = ((value != null) ? ((float)value) : 0f);
				return true;
			case 4:
				this.SlotHeight = ((value != null) ? ((float)value) : 0f);
				return true;
			case 5:
				this.Hovered = (value != null && (bool)value);
				return true;
			case 6:
				this.IsFiller = (value != null && (bool)value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BD93 RID: 48531 RVA: 0x0039CB14 File Offset: 0x0039AD14
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
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x04009A7E RID: 39550
		public const int ModelId = 19;

		// Token: 0x04009A7F RID: 39551
		private string m_DisplayText;

		// Token: 0x04009A80 RID: 39552
		private ProductDataModel m_DisplayProduct;

		// Token: 0x04009A81 RID: 39553
		private float m_SlotWidth;

		// Token: 0x04009A82 RID: 39554
		private float m_SlotHeight;

		// Token: 0x04009A83 RID: 39555
		private bool m_Hovered;

		// Token: 0x04009A84 RID: 39556
		private bool m_IsFiller;

		// Token: 0x04009A85 RID: 39557
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "display_text",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "display_product",
				Type = typeof(ProductDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "slot_width",
				Type = typeof(float)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "slot_height",
				Type = typeof(float)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "hovered",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "is_filler",
				Type = typeof(bool)
			}
		};
	}
}
