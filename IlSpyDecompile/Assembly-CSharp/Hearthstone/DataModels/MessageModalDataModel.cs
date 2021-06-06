using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class MessageModalDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 298;

		private string m_ContentType;

		private DataModelProperty[] m_properties = new DataModelProperty[1]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "content_type",
				Type = typeof(string)
			}
		};

		public int DataModelId => 298;

		public string DataModelDisplayName => "message_modal";

		public string ContentType
		{
			get
			{
				return m_ContentType;
			}
			set
			{
				if (!(m_ContentType == value))
				{
					m_ContentType = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((m_ContentType != null) ? m_ContentType.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = m_ContentType;
				return true;
			}
			value = null;
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				ContentType = ((value != null) ? ((string)value) : null);
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
