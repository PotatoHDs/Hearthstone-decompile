using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class RewardTrackNodeRewardsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 236;

		private int m_Level;

		private string m_Summary;

		private bool m_IsPremium;

		private bool m_IsClaimed;

		private RewardListDataModel m_Items;

		private DataModelProperty[] m_properties;

		public int DataModelId => 236;

		public string DataModelDisplayName => "reward_track_node_rewards";

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

		public string Summary
		{
			get
			{
				return m_Summary;
			}
			set
			{
				if (!(m_Summary == value))
				{
					m_Summary = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsPremium
		{
			get
			{
				return m_IsPremium;
			}
			set
			{
				if (m_IsPremium != value)
				{
					m_IsPremium = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsClaimed
		{
			get
			{
				return m_IsClaimed;
			}
			set
			{
				if (m_IsClaimed != value)
				{
					m_IsClaimed = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public RewardListDataModel Items
		{
			get
			{
				return m_Items;
			}
			set
			{
				if (m_Items != value)
				{
					RemoveNestedDataModel(m_Items);
					RegisterNestedDataModel(value);
					m_Items = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public RewardTrackNodeRewardsDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[5];
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
				PropertyDisplayName = "summary",
				Type = typeof(string)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "is_premium",
				Type = typeof(bool)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "is_claimed",
				Type = typeof(bool)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "items",
				Type = typeof(RewardListDataModel)
			};
			array[4] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Items);
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_Level;
			int num2 = ((num + m_Level.GetHashCode()) * 31 + ((m_Summary != null) ? m_Summary.GetHashCode() : 0)) * 31;
			_ = m_IsPremium;
			int num3 = (num2 + m_IsPremium.GetHashCode()) * 31;
			_ = m_IsClaimed;
			return (num3 + m_IsClaimed.GetHashCode()) * 31 + ((m_Items != null) ? m_Items.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Level;
				return true;
			case 1:
				value = m_Summary;
				return true;
			case 2:
				value = m_IsPremium;
				return true;
			case 3:
				value = m_IsClaimed;
				return true;
			case 4:
				value = m_Items;
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
				Summary = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				IsPremium = value != null && (bool)value;
				return true;
			case 3:
				IsClaimed = value != null && (bool)value;
				return true;
			case 4:
				Items = ((value != null) ? ((RewardListDataModel)value) : null);
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
			case 2:
				info = Properties[2];
				return true;
			case 3:
				info = Properties[3];
				return true;
			case 4:
				info = Properties[4];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
