using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class ProductTierDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 23;

		private string m_Header;

		private DataModelList<string> m_Tags = new DataModelList<string>();

		private string m_Style;

		private DataModelList<ShopBrowserButtonDataModel> m_BrowserButtons = new DataModelList<ShopBrowserButtonDataModel>();

		private DataModelProperty[] m_properties;

		public int DataModelId => 23;

		public string DataModelDisplayName => "product_tier";

		public string Header
		{
			get
			{
				return m_Header;
			}
			set
			{
				if (!(m_Header == value))
				{
					m_Header = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<string> Tags
		{
			get
			{
				return m_Tags;
			}
			set
			{
				if (m_Tags != value)
				{
					RemoveNestedDataModel(m_Tags);
					RegisterNestedDataModel(value);
					m_Tags = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string Style
		{
			get
			{
				return m_Style;
			}
			set
			{
				if (!(m_Style == value))
				{
					m_Style = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<ShopBrowserButtonDataModel> BrowserButtons
		{
			get
			{
				return m_BrowserButtons;
			}
			set
			{
				if (m_BrowserButtons != value)
				{
					RemoveNestedDataModel(m_BrowserButtons);
					RegisterNestedDataModel(value);
					m_BrowserButtons = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public ProductTierDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[4];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "header",
				Type = typeof(string)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "tags",
				Type = typeof(DataModelList<string>)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "style",
				Type = typeof(string)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "browser_buttons",
				Type = typeof(DataModelList<ShopBrowserButtonDataModel>)
			};
			array[3] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Tags);
			RegisterNestedDataModel(m_BrowserButtons);
		}

		public int GetPropertiesHashCode()
		{
			return (((17 * 31 + ((m_Header != null) ? m_Header.GetHashCode() : 0)) * 31 + ((m_Tags != null) ? m_Tags.GetPropertiesHashCode() : 0)) * 31 + ((m_Style != null) ? m_Style.GetHashCode() : 0)) * 31 + ((m_BrowserButtons != null) ? m_BrowserButtons.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 1:
				value = m_Header;
				return true;
			case 2:
				value = m_Tags;
				return true;
			case 3:
				value = m_Style;
				return true;
			case 4:
				value = m_BrowserButtons;
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
			case 1:
				Header = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				Tags = ((value != null) ? ((DataModelList<string>)value) : null);
				return true;
			case 3:
				Style = ((value != null) ? ((string)value) : null);
				return true;
			case 4:
				BrowserButtons = ((value != null) ? ((DataModelList<ShopBrowserButtonDataModel>)value) : null);
				return true;
			default:
				return false;
			}
		}

		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 1:
				info = Properties[0];
				return true;
			case 2:
				info = Properties[1];
				return true;
			case 3:
				info = Properties[2];
				return true;
			case 4:
				info = Properties[3];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
