using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class MessageDebugContentDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 299;

		private string m_TestString;

		private DataModelProperty[] m_properties = new DataModelProperty[1]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "test_string",
				Type = typeof(string)
			}
		};

		public int DataModelId => 299;

		public string DataModelDisplayName => "message_debug_content";

		public string TestString
		{
			get
			{
				return m_TestString;
			}
			set
			{
				if (!(m_TestString == value))
				{
					m_TestString = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((m_TestString != null) ? m_TestString.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = m_TestString;
				return true;
			}
			value = null;
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				TestString = ((value != null) ? ((string)value) : null);
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
