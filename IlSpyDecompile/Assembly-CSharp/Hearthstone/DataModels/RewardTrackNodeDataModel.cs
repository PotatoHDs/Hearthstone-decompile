using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class RewardTrackNodeDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 230;

		private int m_Level;

		private string m_StyleName;

		private RewardTrackNodeRewardsDataModel m_FreeRewards;

		private RewardTrackNodeRewardsDataModel m_PremiumRewards;

		private DataModelProperty[] m_properties;

		public int DataModelId => 230;

		public string DataModelDisplayName => "reward_track_node";

		public int Level
		{
			get
			{
				return m_Level;
			}
			set
			{
				if (m_Level != value)
				{
					m_Level = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string StyleName
		{
			get
			{
				return m_StyleName;
			}
			set
			{
				if (!(m_StyleName == value))
				{
					m_StyleName = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public RewardTrackNodeRewardsDataModel FreeRewards
		{
			get
			{
				return m_FreeRewards;
			}
			set
			{
				if (m_FreeRewards != value)
				{
					RemoveNestedDataModel(m_FreeRewards);
					RegisterNestedDataModel(value);
					m_FreeRewards = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public RewardTrackNodeRewardsDataModel PremiumRewards
		{
			get
			{
				return m_PremiumRewards;
			}
			set
			{
				if (m_PremiumRewards != value)
				{
					RemoveNestedDataModel(m_PremiumRewards);
					RegisterNestedDataModel(value);
					m_PremiumRewards = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public RewardTrackNodeDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[4];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "level",
				Type = typeof(int)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "style_name",
				Type = typeof(string)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "free_rewards",
				Type = typeof(RewardTrackNodeRewardsDataModel)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "premium_rewards",
				Type = typeof(RewardTrackNodeRewardsDataModel)
			};
			array[3] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_FreeRewards);
			RegisterNestedDataModel(m_PremiumRewards);
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_Level;
			return (((num + m_Level.GetHashCode()) * 31 + ((m_StyleName != null) ? m_StyleName.GetHashCode() : 0)) * 31 + ((m_FreeRewards != null) ? m_FreeRewards.GetPropertiesHashCode() : 0)) * 31 + ((m_PremiumRewards != null) ? m_PremiumRewards.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Level;
				return true;
			case 1:
				value = m_StyleName;
				return true;
			case 6:
				value = m_FreeRewards;
				return true;
			case 7:
				value = m_PremiumRewards;
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
				Level = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				StyleName = ((value != null) ? ((string)value) : null);
				return true;
			case 6:
				FreeRewards = ((value != null) ? ((RewardTrackNodeRewardsDataModel)value) : null);
				return true;
			case 7:
				PremiumRewards = ((value != null) ? ((RewardTrackNodeRewardsDataModel)value) : null);
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
			case 6:
				info = Properties[2];
				return true;
			case 7:
				info = Properties[3];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
