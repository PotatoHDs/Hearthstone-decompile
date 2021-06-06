using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class JournalMetaDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 279;

		private int m_TabIndex;

		private DataModelProperty[] m_properties = new DataModelProperty[1]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "tab_index",
				Type = typeof(int)
			}
		};

		public int DataModelId => 279;

		public string DataModelDisplayName => "journal_meta";

		public int TabIndex
		{
			get
			{
				return m_TabIndex;
			}
			set
			{
				if (m_TabIndex != value)
				{
					m_TabIndex = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_TabIndex;
			return num + m_TabIndex.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = m_TabIndex;
				return true;
			}
			value = null;
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				TabIndex = ((value != null) ? ((int)value) : 0);
				return true;
			}
			return false;
		}

		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 0)
			{
				info = Properties[0];
				return true;
			}
			info = default(DataModelProperty);
			return false;
		}
	}
}
