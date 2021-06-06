using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class ProductSelectionDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 30;

		private ProductDataModel m_Variant;

		private int m_VariantIndex;

		private int m_Quantity;

		private int m_MaxQuantity;

		private DataModelProperty[] m_properties;

		public int DataModelId => 30;

		public string DataModelDisplayName => "product_selection";

		public ProductDataModel Variant
		{
			get
			{
				return m_Variant;
			}
			set
			{
				if (m_Variant != value)
				{
					RemoveNestedDataModel(m_Variant);
					RegisterNestedDataModel(value);
					m_Variant = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int VariantIndex
		{
			get
			{
				return m_VariantIndex;
			}
			set
			{
				if (m_VariantIndex != value)
				{
					m_VariantIndex = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Quantity
		{
			get
			{
				return m_Quantity;
			}
			set
			{
				if (m_Quantity != value)
				{
					m_Quantity = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int MaxQuantity
		{
			get
			{
				return m_MaxQuantity;
			}
			set
			{
				if (m_MaxQuantity != value)
				{
					m_MaxQuantity = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public ProductSelectionDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[4];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "variant",
				Type = typeof(ProductDataModel)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "variant_index",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "quantity",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "max_quantity",
				Type = typeof(int)
			};
			array[3] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Variant);
		}

		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((m_Variant != null) ? m_Variant.GetPropertiesHashCode() : 0)) * 31;
			_ = m_VariantIndex;
			int num2 = (num + m_VariantIndex.GetHashCode()) * 31;
			_ = m_Quantity;
			int num3 = (num2 + m_Quantity.GetHashCode()) * 31;
			_ = m_MaxQuantity;
			return num3 + m_MaxQuantity.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Variant;
				return true;
			case 1:
				value = m_VariantIndex;
				return true;
			case 2:
				value = m_Quantity;
				return true;
			case 3:
				value = m_MaxQuantity;
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
				Variant = ((value != null) ? ((ProductDataModel)value) : null);
				return true;
			case 1:
				VariantIndex = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				Quantity = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				MaxQuantity = ((value != null) ? ((int)value) : 0);
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
			case 2:
				info = Properties[2];
				return true;
			case 3:
				info = Properties[3];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
