using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class PageInfoDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 255;

		private int m_PageNumber;

		private int m_TotalPages;

		private int m_ItemsPerPage;

		private string m_InfoText;

		private DataModelProperty[] m_properties;

		public int DataModelId => 255;

		public string DataModelDisplayName => "page_info";

		public int PageNumber
		{
			get
			{
				return m_PageNumber;
			}
			set
			{
				if (m_PageNumber != value)
				{
					m_PageNumber = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int TotalPages
		{
			get
			{
				return m_TotalPages;
			}
			set
			{
				if (m_TotalPages != value)
				{
					m_TotalPages = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int ItemsPerPage
		{
			get
			{
				return m_ItemsPerPage;
			}
			set
			{
				if (m_ItemsPerPage != value)
				{
					m_ItemsPerPage = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string InfoText
		{
			get
			{
				return m_InfoText;
			}
			set
			{
				if (!(m_InfoText == value))
				{
					m_InfoText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public PageInfoDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[4];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "page_number",
				Type = typeof(int)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "total_pages",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "items_per_page",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "info_text",
				Type = typeof(string)
			};
			array[3] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_PageNumber;
			int num2 = (num + m_PageNumber.GetHashCode()) * 31;
			_ = m_TotalPages;
			int num3 = (num2 + m_TotalPages.GetHashCode()) * 31;
			_ = m_ItemsPerPage;
			return (num3 + m_ItemsPerPage.GetHashCode()) * 31 + ((m_InfoText != null) ? m_InfoText.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_PageNumber;
				return true;
			case 1:
				value = m_TotalPages;
				return true;
			case 2:
				value = m_ItemsPerPage;
				return true;
			case 3:
				value = m_InfoText;
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
				PageNumber = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				TotalPages = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				ItemsPerPage = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				InfoText = ((value != null) ? ((string)value) : null);
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
