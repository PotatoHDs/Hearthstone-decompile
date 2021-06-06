using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class AccountDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 153;

		private Locale m_Language;

		private DataModelProperty[] m_properties = new DataModelProperty[1]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "language",
				Type = typeof(Locale)
			}
		};

		public int DataModelId => 153;

		public string DataModelDisplayName => "account";

		public Locale Language
		{
			get
			{
				return m_Language;
			}
			set
			{
				if (m_Language != value)
				{
					m_Language = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_Language;
			return num + m_Language.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = m_Language;
				return true;
			}
			value = null;
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				Language = ((value != null) ? ((Locale)value) : Locale.enUS);
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
