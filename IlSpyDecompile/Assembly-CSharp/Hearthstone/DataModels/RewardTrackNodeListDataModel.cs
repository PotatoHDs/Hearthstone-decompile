using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class RewardTrackNodeListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 231;

		private DataModelList<RewardTrackNodeDataModel> m_Nodes = new DataModelList<RewardTrackNodeDataModel>();

		private DataModelProperty[] m_properties = new DataModelProperty[1]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "nodes",
				Type = typeof(DataModelList<RewardTrackNodeDataModel>)
			}
		};

		public int DataModelId => 231;

		public string DataModelDisplayName => "reward_track_node_list";

		public DataModelList<RewardTrackNodeDataModel> Nodes
		{
			get
			{
				return m_Nodes;
			}
			set
			{
				if (m_Nodes != value)
				{
					RemoveNestedDataModel(m_Nodes);
					RegisterNestedDataModel(value);
					m_Nodes = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public RewardTrackNodeListDataModel()
		{
			RegisterNestedDataModel(m_Nodes);
		}

		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((m_Nodes != null) ? m_Nodes.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = m_Nodes;
				return true;
			}
			value = null;
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				Nodes = ((value != null) ? ((DataModelList<RewardTrackNodeDataModel>)value) : null);
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
