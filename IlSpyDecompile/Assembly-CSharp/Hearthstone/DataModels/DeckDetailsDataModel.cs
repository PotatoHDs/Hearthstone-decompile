using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class DeckDetailsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 290;

		private ProductDataModel m_Product;

		private MiniSetDetailsDataModel m_MiniSetDetails;

		private DataModelProperty[] m_properties;

		public int DataModelId => 290;

		public string DataModelDisplayName => "deck_details";

		public ProductDataModel Product
		{
			get
			{
				return m_Product;
			}
			set
			{
				if (m_Product != value)
				{
					RemoveNestedDataModel(m_Product);
					RegisterNestedDataModel(value);
					m_Product = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public MiniSetDetailsDataModel MiniSetDetails
		{
			get
			{
				return m_MiniSetDetails;
			}
			set
			{
				if (m_MiniSetDetails != value)
				{
					RemoveNestedDataModel(m_MiniSetDetails);
					RegisterNestedDataModel(value);
					m_MiniSetDetails = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public DeckDetailsDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[2];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "product",
				Type = typeof(ProductDataModel)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "mini_set_details",
				Type = typeof(MiniSetDetailsDataModel)
			};
			array[1] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Product);
			RegisterNestedDataModel(m_MiniSetDetails);
		}

		public int GetPropertiesHashCode()
		{
			return (17 * 31 + ((m_Product != null) ? m_Product.GetPropertiesHashCode() : 0)) * 31 + ((m_MiniSetDetails != null) ? m_MiniSetDetails.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Product;
				return true;
			case 1:
				value = m_MiniSetDetails;
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
				Product = ((value != null) ? ((ProductDataModel)value) : null);
				return true;
			case 1:
				MiniSetDetails = ((value != null) ? ((MiniSetDetailsDataModel)value) : null);
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
