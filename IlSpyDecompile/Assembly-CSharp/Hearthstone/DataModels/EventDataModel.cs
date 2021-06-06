using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class EventDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 120;

		private string m_SourceName;

		private object m_Payload;

		private DataModelProperty[] m_properties;

		public int DataModelId => 120;

		public string DataModelDisplayName => "event";

		public string SourceName
		{
			get
			{
				return m_SourceName;
			}
			set
			{
				if (!(m_SourceName == value))
				{
					m_SourceName = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public object Payload
		{
			get
			{
				return m_Payload;
			}
			set
			{
				if (m_Payload != value)
				{
					m_Payload = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public EventDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[2];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "source_name",
				Type = typeof(string)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "payload",
				Type = typeof(object)
			};
			array[1] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			return (17 * 31 + ((m_SourceName != null) ? m_SourceName.GetHashCode() : 0)) * 31 + ((m_Payload != null) ? m_Payload.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_SourceName;
				return true;
			case 1:
				value = m_Payload;
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
				SourceName = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				Payload = ((value != null) ? value : null);
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
