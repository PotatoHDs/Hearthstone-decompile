using Assets;
using Hearthstone.Progression;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class QuestDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 207;

		private string m_Name;

		private int m_Progress;

		private int m_Quota;

		private string m_Description;

		private DataModelList<string> m_Icon = new DataModelList<string>();

		private QuestPool.QuestPoolType m_PoolType;

		private string m_TimeUntilNextQuest;

		private int m_RerollCount;

		private QuestManager.QuestTileDisplayMode m_DisplayMode;

		private RewardListDataModel m_Rewards;

		private int m_QuestId;

		private int m_RewardTrackXp;

		private string m_ProgressMessage;

		private QuestManager.QuestStatus m_Status;

		private int m_PoolId;

		private DataModelProperty[] m_properties;

		public int DataModelId => 207;

		public string DataModelDisplayName => "quest";

		public string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
				if (!(m_Name == value))
				{
					m_Name = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Progress
		{
			get
			{
				return m_Progress;
			}
			set
			{
				if (m_Progress != value)
				{
					m_Progress = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Quota
		{
			get
			{
				return m_Quota;
			}
			set
			{
				if (m_Quota != value)
				{
					m_Quota = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string Description
		{
			get
			{
				return m_Description;
			}
			set
			{
				if (!(m_Description == value))
				{
					m_Description = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<string> Icon
		{
			get
			{
				return m_Icon;
			}
			set
			{
				if (m_Icon != value)
				{
					RemoveNestedDataModel(m_Icon);
					RegisterNestedDataModel(value);
					m_Icon = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public QuestPool.QuestPoolType PoolType
		{
			get
			{
				return m_PoolType;
			}
			set
			{
				if (m_PoolType != value)
				{
					m_PoolType = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string TimeUntilNextQuest
		{
			get
			{
				return m_TimeUntilNextQuest;
			}
			set
			{
				if (!(m_TimeUntilNextQuest == value))
				{
					m_TimeUntilNextQuest = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int RerollCount
		{
			get
			{
				return m_RerollCount;
			}
			set
			{
				if (m_RerollCount != value)
				{
					m_RerollCount = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public QuestManager.QuestTileDisplayMode DisplayMode
		{
			get
			{
				return m_DisplayMode;
			}
			set
			{
				if (m_DisplayMode != value)
				{
					m_DisplayMode = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public RewardListDataModel Rewards
		{
			get
			{
				return m_Rewards;
			}
			set
			{
				if (m_Rewards != value)
				{
					RemoveNestedDataModel(m_Rewards);
					RegisterNestedDataModel(value);
					m_Rewards = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int QuestId
		{
			get
			{
				return m_QuestId;
			}
			set
			{
				if (m_QuestId != value)
				{
					m_QuestId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int RewardTrackXp
		{
			get
			{
				return m_RewardTrackXp;
			}
			set
			{
				if (m_RewardTrackXp != value)
				{
					m_RewardTrackXp = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string ProgressMessage
		{
			get
			{
				return m_ProgressMessage;
			}
			set
			{
				if (!(m_ProgressMessage == value))
				{
					m_ProgressMessage = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public QuestManager.QuestStatus Status
		{
			get
			{
				return m_Status;
			}
			set
			{
				if (m_Status != value)
				{
					m_Status = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int PoolId
		{
			get
			{
				return m_PoolId;
			}
			set
			{
				if (m_PoolId != value)
				{
					m_PoolId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public QuestDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[15];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "name",
				Type = typeof(string)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "progress",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "quota",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "description",
				Type = typeof(string)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "icon",
				Type = typeof(DataModelList<string>)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "pool",
				Type = typeof(QuestPool.QuestPoolType)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "time_until_next_quest",
				Type = typeof(string)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "reroll_count",
				Type = typeof(int)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "display_mode",
				Type = typeof(QuestManager.QuestTileDisplayMode)
			};
			array[8] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "rewards",
				Type = typeof(RewardListDataModel)
			};
			array[9] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "quest_id",
				Type = typeof(int)
			};
			array[10] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "reward_track_xp",
				Type = typeof(int)
			};
			array[11] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 13,
				PropertyDisplayName = "progress_message",
				Type = typeof(string)
			};
			array[12] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 14,
				PropertyDisplayName = "status",
				Type = typeof(QuestManager.QuestStatus)
			};
			array[13] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "pool_id",
				Type = typeof(int)
			};
			array[14] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Icon);
			RegisterNestedDataModel(m_Rewards);
		}

		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((m_Name != null) ? m_Name.GetHashCode() : 0)) * 31;
			_ = m_Progress;
			int num2 = (num + m_Progress.GetHashCode()) * 31;
			_ = m_Quota;
			int num3 = (((num2 + m_Quota.GetHashCode()) * 31 + ((m_Description != null) ? m_Description.GetHashCode() : 0)) * 31 + ((m_Icon != null) ? m_Icon.GetPropertiesHashCode() : 0)) * 31;
			_ = m_PoolType;
			int num4 = ((num3 + m_PoolType.GetHashCode()) * 31 + ((m_TimeUntilNextQuest != null) ? m_TimeUntilNextQuest.GetHashCode() : 0)) * 31;
			_ = m_RerollCount;
			int num5 = (num4 + m_RerollCount.GetHashCode()) * 31;
			_ = m_DisplayMode;
			int num6 = ((num5 + m_DisplayMode.GetHashCode()) * 31 + ((m_Rewards != null) ? m_Rewards.GetPropertiesHashCode() : 0)) * 31;
			_ = m_QuestId;
			int num7 = (num6 + m_QuestId.GetHashCode()) * 31;
			_ = m_RewardTrackXp;
			int num8 = ((num7 + m_RewardTrackXp.GetHashCode()) * 31 + ((m_ProgressMessage != null) ? m_ProgressMessage.GetHashCode() : 0)) * 31;
			_ = m_Status;
			int num9 = (num8 + m_Status.GetHashCode()) * 31;
			_ = m_PoolId;
			return num9 + m_PoolId.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Name;
				return true;
			case 1:
				value = m_Progress;
				return true;
			case 2:
				value = m_Quota;
				return true;
			case 3:
				value = m_Description;
				return true;
			case 5:
				value = m_Icon;
				return true;
			case 6:
				value = m_PoolType;
				return true;
			case 7:
				value = m_TimeUntilNextQuest;
				return true;
			case 8:
				value = m_RerollCount;
				return true;
			case 9:
				value = m_DisplayMode;
				return true;
			case 10:
				value = m_Rewards;
				return true;
			case 11:
				value = m_QuestId;
				return true;
			case 12:
				value = m_RewardTrackXp;
				return true;
			case 13:
				value = m_ProgressMessage;
				return true;
			case 14:
				value = m_Status;
				return true;
			case 15:
				value = m_PoolId;
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
				Name = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				Progress = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				Quota = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				Description = ((value != null) ? ((string)value) : null);
				return true;
			case 5:
				Icon = ((value != null) ? ((DataModelList<string>)value) : null);
				return true;
			case 6:
				PoolType = ((value != null) ? ((QuestPool.QuestPoolType)value) : QuestPool.QuestPoolType.NONE);
				return true;
			case 7:
				TimeUntilNextQuest = ((value != null) ? ((string)value) : null);
				return true;
			case 8:
				RerollCount = ((value != null) ? ((int)value) : 0);
				return true;
			case 9:
				DisplayMode = ((value != null) ? ((QuestManager.QuestTileDisplayMode)value) : QuestManager.QuestTileDisplayMode.DEFAULT);
				return true;
			case 10:
				Rewards = ((value != null) ? ((RewardListDataModel)value) : null);
				return true;
			case 11:
				QuestId = ((value != null) ? ((int)value) : 0);
				return true;
			case 12:
				RewardTrackXp = ((value != null) ? ((int)value) : 0);
				return true;
			case 13:
				ProgressMessage = ((value != null) ? ((string)value) : null);
				return true;
			case 14:
				Status = ((value != null) ? ((QuestManager.QuestStatus)value) : QuestManager.QuestStatus.UNKNOWN);
				return true;
			case 15:
				PoolId = ((value != null) ? ((int)value) : 0);
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
			case 5:
				info = Properties[4];
				return true;
			case 6:
				info = Properties[5];
				return true;
			case 7:
				info = Properties[6];
				return true;
			case 8:
				info = Properties[7];
				return true;
			case 9:
				info = Properties[8];
				return true;
			case 10:
				info = Properties[9];
				return true;
			case 11:
				info = Properties[10];
				return true;
			case 12:
				info = Properties[11];
				return true;
			case 13:
				info = Properties[12];
				return true;
			case 14:
				info = Properties[13];
				return true;
			case 15:
				info = Properties[14];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
