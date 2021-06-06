using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class AchievementSubcategoryListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 261;

		private DataModelList<AchievementSubcategoryDataModel> m_Subcategories = new DataModelList<AchievementSubcategoryDataModel>();

		private DataModelProperty[] m_properties = new DataModelProperty[1]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "subcategories",
				Type = typeof(DataModelList<AchievementSubcategoryDataModel>)
			}
		};

		public int DataModelId => 261;

		public string DataModelDisplayName => "achievement_subcategory_list";

		public DataModelList<AchievementSubcategoryDataModel> Subcategories
		{
			get
			{
				return m_Subcategories;
			}
			set
			{
				if (m_Subcategories != value)
				{
					RemoveNestedDataModel(m_Subcategories);
					RegisterNestedDataModel(value);
					m_Subcategories = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public AchievementSubcategoryListDataModel()
		{
			RegisterNestedDataModel(m_Subcategories);
		}

		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((m_Subcategories != null) ? m_Subcategories.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = m_Subcategories;
				return true;
			}
			value = null;
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				Subcategories = ((value != null) ? ((DataModelList<AchievementSubcategoryDataModel>)value) : null);
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
