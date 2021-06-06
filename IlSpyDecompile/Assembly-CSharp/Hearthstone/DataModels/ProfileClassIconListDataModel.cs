using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class ProfileClassIconListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 233;

		private DataModelList<ProfileClassIconDataModel> m_Icons = new DataModelList<ProfileClassIconDataModel>();

		private DataModelProperty[] m_properties = new DataModelProperty[1]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "icons",
				Type = typeof(DataModelList<ProfileClassIconDataModel>)
			}
		};

		public int DataModelId => 233;

		public string DataModelDisplayName => "profile_class_icon_list";

		public DataModelList<ProfileClassIconDataModel> Icons
		{
			get
			{
				return m_Icons;
			}
			set
			{
				if (m_Icons != value)
				{
					RemoveNestedDataModel(m_Icons);
					RegisterNestedDataModel(value);
					m_Icons = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public ProfileClassIconListDataModel()
		{
			RegisterNestedDataModel(m_Icons);
		}

		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((m_Icons != null) ? m_Icons.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = m_Icons;
				return true;
			}
			value = null;
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				Icons = ((value != null) ? ((DataModelList<ProfileClassIconDataModel>)value) : null);
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
