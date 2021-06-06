using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class CardBackDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 26;

		private int m_CardBackId;

		private DataModelProperty[] m_properties = new DataModelProperty[1]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "id",
				Type = typeof(int)
			}
		};

		public int DataModelId => 26;

		public string DataModelDisplayName => "cardback";

		public int CardBackId
		{
			get
			{
				return m_CardBackId;
			}
			set
			{
				if (m_CardBackId != value)
				{
					m_CardBackId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_CardBackId;
			return num + m_CardBackId.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = m_CardBackId;
				return true;
			}
			value = null;
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				CardBackId = ((value != null) ? ((int)value) : 0);
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
