using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class AchievementSectionListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 228;

		private DataModelList<AchievementSectionDataModel> m_Sections = new DataModelList<AchievementSectionDataModel>();

		private DataModelProperty[] m_properties = new DataModelProperty[1]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "sections",
				Type = typeof(DataModelList<AchievementSectionDataModel>)
			}
		};

		public int DataModelId => 228;

		public string DataModelDisplayName => "achievement_section_list";

		public DataModelList<AchievementSectionDataModel> Sections
		{
			get
			{
				return m_Sections;
			}
			set
			{
				if (m_Sections != value)
				{
					RemoveNestedDataModel(m_Sections);
					RegisterNestedDataModel(value);
					m_Sections = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public AchievementSectionListDataModel()
		{
			RegisterNestedDataModel(m_Sections);
		}

		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((m_Sections != null) ? m_Sections.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = m_Sections;
				return true;
			}
			value = null;
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				Sections = ((value != null) ? ((DataModelList<AchievementSectionDataModel>)value) : null);
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
