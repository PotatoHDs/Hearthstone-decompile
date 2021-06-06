using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class QuestListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 208;

		private DataModelList<QuestDataModel> m_Quests = new DataModelList<QuestDataModel>();

		private DataModelProperty[] m_properties = new DataModelProperty[1]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "quests",
				Type = typeof(DataModelList<QuestDataModel>)
			}
		};

		public int DataModelId => 208;

		public string DataModelDisplayName => "quest_list";

		public DataModelList<QuestDataModel> Quests
		{
			get
			{
				return m_Quests;
			}
			set
			{
				if (m_Quests != value)
				{
					RemoveNestedDataModel(m_Quests);
					RegisterNestedDataModel(value);
					m_Quests = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public QuestListDataModel()
		{
			RegisterNestedDataModel(m_Quests);
		}

		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((m_Quests != null) ? m_Quests.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = m_Quests;
				return true;
			}
			value = null;
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				Quests = ((value != null) ? ((DataModelList<QuestDataModel>)value) : null);
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
