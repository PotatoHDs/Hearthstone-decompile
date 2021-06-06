using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class ShopBrowserButtonDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 19;

		private string m_DisplayText;

		private ProductDataModel m_DisplayProduct;

		private float m_SlotWidth;

		private float m_SlotHeight;

		private bool m_Hovered;

		private bool m_IsFiller;

		private DataModelProperty[] m_properties;

		public int DataModelId => 19;

		public string DataModelDisplayName => "shop_browser_button";

		public string DisplayText
		{
			get
			{
				return m_DisplayText;
			}
			set
			{
				if (!(m_DisplayText == value))
				{
					m_DisplayText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public ProductDataModel DisplayProduct
		{
			get
			{
				return m_DisplayProduct;
			}
			set
			{
				if (m_DisplayProduct != value)
				{
					RemoveNestedDataModel(m_DisplayProduct);
					RegisterNestedDataModel(value);
					m_DisplayProduct = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public float SlotWidth
		{
			get
			{
				return m_SlotWidth;
			}
			set
			{
				if (m_SlotWidth != value)
				{
					m_SlotWidth = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public float SlotHeight
		{
			get
			{
				return m_SlotHeight;
			}
			set
			{
				if (m_SlotHeight != value)
				{
					m_SlotHeight = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool Hovered
		{
			get
			{
				return m_Hovered;
			}
			set
			{
				if (m_Hovered != value)
				{
					m_Hovered = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsFiller
		{
			get
			{
				return m_IsFiller;
			}
			set
			{
				if (m_IsFiller != value)
				{
					m_IsFiller = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public ShopBrowserButtonDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[6];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "display_text",
				Type = typeof(string)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "display_product",
				Type = typeof(ProductDataModel)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "slot_width",
				Type = typeof(float)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "slot_height",
				Type = typeof(float)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "hovered",
				Type = typeof(bool)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "is_filler",
				Type = typeof(bool)
			};
			array[5] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_DisplayProduct);
		}

		public int GetPropertiesHashCode()
		{
			int num = ((17 * 31 + ((m_DisplayText != null) ? m_DisplayText.GetHashCode() : 0)) * 31 + ((m_DisplayProduct != null) ? m_DisplayProduct.GetPropertiesHashCode() : 0)) * 31;
			_ = m_SlotWidth;
			int num2 = (num + m_SlotWidth.GetHashCode()) * 31;
			_ = m_SlotHeight;
			int num3 = (num2 + m_SlotHeight.GetHashCode()) * 31;
			_ = m_Hovered;
			int num4 = (num3 + m_Hovered.GetHashCode()) * 31;
			_ = m_IsFiller;
			return num4 + m_IsFiller.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_DisplayText;
				return true;
			case 1:
				value = m_DisplayProduct;
				return true;
			case 3:
				value = m_SlotWidth;
				return true;
			case 4:
				value = m_SlotHeight;
				return true;
			case 5:
				value = m_Hovered;
				return true;
			case 6:
				value = m_IsFiller;
				return true;
			default:
				value = null;
				return false;
			}
		}

		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				DisplayText = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				DisplayProduct = ((value != null) ? ((ProductDataModel)value) : null);
				return true;
			case 3:
				SlotWidth = ((value != null) ? ((float)value) : 0f);
				return true;
			case 4:
				SlotHeight = ((value != null) ? ((float)value) : 0f);
				return true;
			case 5:
				Hovered = value != null && (bool)value;
				return true;
			case 6:
				IsFiller = value != null && (bool)value;
				return true;
			default:
				return false;
			}
		}

		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 0:
				info = Properties[0];
				return true;
			case 1:
				info = Properties[1];
				return true;
			case 3:
				info = Properties[2];
				return true;
			case 4:
				info = Properties[3];
				return true;
			case 5:
				info = Properties[4];
				return true;
			case 6:
				info = Properties[5];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
