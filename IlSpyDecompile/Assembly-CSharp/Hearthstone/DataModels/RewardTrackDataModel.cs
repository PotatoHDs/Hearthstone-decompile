using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class RewardTrackDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 229;

		private int m_RewardTrackId;

		private int m_Level;

		private int m_Xp;

		private int m_XpNeeded;

		private int m_XpBonusPercent;

		private bool m_PremiumRewardsUnlocked;

		private string m_XpProgress;

		private int m_Unclaimed;

		private int m_LevelSoftCap;

		private string m_XpBoostText;

		private int m_LevelHardCap;

		private int m_Season;

		private int m_SeasonLastSeen;

		private DataModelProperty[] m_properties;

		public int DataModelId => 229;

		public string DataModelDisplayName => "reward_track";

		public int RewardTrackId
		{
			get
			{
				return m_RewardTrackId;
			}
			set
			{
				if (m_RewardTrackId != value)
				{
					m_RewardTrackId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

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

		public int Xp
		{
			get
			{
				return m_Xp;
			}
			set
			{
				if (m_Xp != value)
				{
					m_Xp = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int XpNeeded
		{
			get
			{
				return m_XpNeeded;
			}
			set
			{
				if (m_XpNeeded != value)
				{
					m_XpNeeded = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int XpBonusPercent
		{
			get
			{
				return m_XpBonusPercent;
			}
			set
			{
				if (m_XpBonusPercent != value)
				{
					m_XpBonusPercent = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool PremiumRewardsUnlocked
		{
			get
			{
				return m_PremiumRewardsUnlocked;
			}
			set
			{
				if (m_PremiumRewardsUnlocked != value)
				{
					m_PremiumRewardsUnlocked = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string XpProgress
		{
			get
			{
				return m_XpProgress;
			}
			set
			{
				if (!(m_XpProgress == value))
				{
					m_XpProgress = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Unclaimed
		{
			get
			{
				return m_Unclaimed;
			}
			set
			{
				if (m_Unclaimed != value)
				{
					m_Unclaimed = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int LevelSoftCap
		{
			get
			{
				return m_LevelSoftCap;
			}
			set
			{
				if (m_LevelSoftCap != value)
				{
					m_LevelSoftCap = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string XpBoostText
		{
			get
			{
				return m_XpBoostText;
			}
			set
			{
				if (!(m_XpBoostText == value))
				{
					m_XpBoostText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int LevelHardCap
		{
			get
			{
				return m_LevelHardCap;
			}
			set
			{
				if (m_LevelHardCap != value)
				{
					m_LevelHardCap = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Season
		{
			get
			{
				return m_Season;
			}
			set
			{
				if (m_Season != value)
				{
					m_Season = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int SeasonLastSeen
		{
			get
			{
				return m_SeasonLastSeen;
			}
			set
			{
				if (m_SeasonLastSeen != value)
				{
					m_SeasonLastSeen = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public RewardTrackDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[13];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "reward_track_id",
				Type = typeof(int)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "level",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "xp",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "xp_needed",
				Type = typeof(int)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "xp_bonus_percent",
				Type = typeof(int)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "premium_rewards_unlocked",
				Type = typeof(bool)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "xp_progress",
				Type = typeof(string)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "unclaimed",
				Type = typeof(int)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "level_soft_cap",
				Type = typeof(int)
			};
			array[8] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "xp_boost_text",
				Type = typeof(string)
			};
			array[9] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "level_hard_cap",
				Type = typeof(int)
			};
			array[10] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "season",
				Type = typeof(int)
			};
			array[11] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "season_last_seen",
				Type = typeof(int)
			};
			array[12] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_RewardTrackId;
			int num2 = (num + m_RewardTrackId.GetHashCode()) * 31;
			_ = m_Level;
			int num3 = (num2 + m_Level.GetHashCode()) * 31;
			_ = m_Xp;
			int num4 = (num3 + m_Xp.GetHashCode()) * 31;
			_ = m_XpNeeded;
			int num5 = (num4 + m_XpNeeded.GetHashCode()) * 31;
			_ = m_XpBonusPercent;
			int num6 = (num5 + m_XpBonusPercent.GetHashCode()) * 31;
			_ = m_PremiumRewardsUnlocked;
			int num7 = ((num6 + m_PremiumRewardsUnlocked.GetHashCode()) * 31 + ((m_XpProgress != null) ? m_XpProgress.GetHashCode() : 0)) * 31;
			_ = m_Unclaimed;
			int num8 = (num7 + m_Unclaimed.GetHashCode()) * 31;
			_ = m_LevelSoftCap;
			int num9 = ((num8 + m_LevelSoftCap.GetHashCode()) * 31 + ((m_XpBoostText != null) ? m_XpBoostText.GetHashCode() : 0)) * 31;
			_ = m_LevelHardCap;
			int num10 = (num9 + m_LevelHardCap.GetHashCode()) * 31;
			_ = m_Season;
			int num11 = (num10 + m_Season.GetHashCode()) * 31;
			_ = m_SeasonLastSeen;
			return num11 + m_SeasonLastSeen.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_RewardTrackId;
				return true;
			case 1:
				value = m_Level;
				return true;
			case 2:
				value = m_Xp;
				return true;
			case 3:
				value = m_XpNeeded;
				return true;
			case 4:
				value = m_XpBonusPercent;
				return true;
			case 5:
				value = m_PremiumRewardsUnlocked;
				return true;
			case 6:
				value = m_XpProgress;
				return true;
			case 7:
				value = m_Unclaimed;
				return true;
			case 8:
				value = m_LevelSoftCap;
				return true;
			case 9:
				value = m_XpBoostText;
				return true;
			case 10:
				value = m_LevelHardCap;
				return true;
			case 11:
				value = m_Season;
				return true;
			case 12:
				value = m_SeasonLastSeen;
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
				RewardTrackId = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				Level = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				Xp = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				XpNeeded = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				XpBonusPercent = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				PremiumRewardsUnlocked = value != null && (bool)value;
				return true;
			case 6:
				XpProgress = ((value != null) ? ((string)value) : null);
				return true;
			case 7:
				Unclaimed = ((value != null) ? ((int)value) : 0);
				return true;
			case 8:
				LevelSoftCap = ((value != null) ? ((int)value) : 0);
				return true;
			case 9:
				XpBoostText = ((value != null) ? ((string)value) : null);
				return true;
			case 10:
				LevelHardCap = ((value != null) ? ((int)value) : 0);
				return true;
			case 11:
				Season = ((value != null) ? ((int)value) : 0);
				return true;
			case 12:
				SeasonLastSeen = ((value != null) ? ((int)value) : 0);
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
			case 5:
				info = Properties[5];
				return true;
			case 6:
				info = Properties[6];
				return true;
			case 7:
				info = Properties[7];
				return true;
			case 8:
				info = Properties[8];
				return true;
			case 9:
				info = Properties[9];
				return true;
			case 10:
				info = Properties[10];
				return true;
			case 11:
				info = Properties[11];
				return true;
			case 12:
				info = Properties[12];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
