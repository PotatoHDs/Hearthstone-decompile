using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class ListableDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 258;

		private int m_ItemIndex;

		private int m_ListSize;

		private bool m_IsFirstItem;

		private bool m_IsLastItem;

		private DataModelProperty[] m_properties;

		public int DataModelId => 258;

		public string DataModelDisplayName => "listable";

		public int ItemIndex
		{
			get
			{
				return m_ItemIndex;
			}
			set
			{
				if (m_ItemIndex != value)
				{
					m_ItemIndex = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int ListSize
		{
			get
			{
				return m_ListSize;
			}
			set
			{
				if (m_ListSize != value)
				{
					m_ListSize = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsFirstItem
		{
			get
			{
				return m_IsFirstItem;
			}
			set
			{
				if (m_IsFirstItem != value)
				{
					m_IsFirstItem = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsLastItem
		{
			get
			{
				return m_IsLastItem;
			}
			set
			{
				if (m_IsLastItem != value)
				{
					m_IsLastItem = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public ListableDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[4];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "item_index",
				Type = typeof(int)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "list_size",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "is_first_item",
				Type = typeof(bool)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "is_last_item",
				Type = typeof(bool)
			};
			array[3] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_ItemIndex;
			int num2 = (num + m_ItemIndex.GetHashCode()) * 31;
			_ = m_ListSize;
			int num3 = (num2 + m_ListSize.GetHashCode()) * 31;
			_ = m_IsFirstItem;
			int num4 = (num3 + m_IsFirstItem.GetHashCode()) * 31;
			_ = m_IsLastItem;
			return num4 + m_IsLastItem.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_ItemIndex;
				return true;
			case 1:
				value = m_ListSize;
				return true;
			case 2:
				value = m_IsFirstItem;
				return true;
			case 3:
				value = m_IsLastItem;
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
				ItemIndex = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				ListSize = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				IsFirstItem = value != null && (bool)value;
				return true;
			case 3:
				IsLastItem = value != null && (bool)value;
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
