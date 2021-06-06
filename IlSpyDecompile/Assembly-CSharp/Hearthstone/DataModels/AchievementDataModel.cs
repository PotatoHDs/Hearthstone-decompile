using Hearthstone.Progression;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class AchievementDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 222;

		private string m_Name;

		private int m_Progress;

		private int m_Quota;

		private string m_Description;

		private string m_StyleName;

		private int m_Points;

		private AchievementManager.AchievementStatus m_Status;

		private string m_CompletionDate;

		private RewardListDataModel m_RewardList;

		private string m_ProgressMessage;

		private int m_ID;

		private int m_NextTierID;

		private int m_Tier;

		private int m_MaxTier;

		private int m_SortOrder;

		private string m_RewardSummary;

		private string m_TierMessage;

		private int m_RewardTrackXp;

		private int m_RewardTrackXpBonusPercent;

		private int m_RewardTrackXpBonusAdjusted;

		private bool m_AllowExceedQuota;

		private DataModelProperty[] m_properties;

		public int DataModelId => 222;

		public string DataModelDisplayName => "achievement";

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

		public int Points
		{
			get
			{
				return m_Points;
			}
			set
			{
				if (m_Points != value)
				{
					m_Points = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public AchievementManager.AchievementStatus Status
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

		public string CompletionDate
		{
			get
			{
				return m_CompletionDate;
			}
			set
			{
				if (!(m_CompletionDate == value))
				{
					m_CompletionDate = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public RewardListDataModel RewardList
		{
			get
			{
				return m_RewardList;
			}
			set
			{
				if (m_RewardList != value)
				{
					RemoveNestedDataModel(m_RewardList);
					RegisterNestedDataModel(value);
					m_RewardList = value;
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

		public int ID
		{
			get
			{
				return m_ID;
			}
			set
			{
				if (m_ID != value)
				{
					m_ID = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int NextTierID
		{
			get
			{
				return m_NextTierID;
			}
			set
			{
				if (m_NextTierID != value)
				{
					m_NextTierID = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Tier
		{
			get
			{
				return m_Tier;
			}
			set
			{
				if (m_Tier != value)
				{
					m_Tier = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int MaxTier
		{
			get
			{
				return m_MaxTier;
			}
			set
			{
				if (m_MaxTier != value)
				{
					m_MaxTier = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int SortOrder
		{
			get
			{
				return m_SortOrder;
			}
			set
			{
				if (m_SortOrder != value)
				{
					m_SortOrder = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string RewardSummary
		{
			get
			{
				return m_RewardSummary;
			}
			set
			{
				if (!(m_RewardSummary == value))
				{
					m_RewardSummary = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string TierMessage
		{
			get
			{
				return m_TierMessage;
			}
			set
			{
				if (!(m_TierMessage == value))
				{
					m_TierMessage = value;
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

		public int RewardTrackXpBonusPercent
		{
			get
			{
				return m_RewardTrackXpBonusPercent;
			}
			set
			{
				if (m_RewardTrackXpBonusPercent != value)
				{
					m_RewardTrackXpBonusPercent = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int RewardTrackXpBonusAdjusted
		{
			get
			{
				return m_RewardTrackXpBonusAdjusted;
			}
			set
			{
				if (m_RewardTrackXpBonusAdjusted != value)
				{
					m_RewardTrackXpBonusAdjusted = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool AllowExceedQuota
		{
			get
			{
				return m_AllowExceedQuota;
			}
			set
			{
				if (m_AllowExceedQuota != value)
				{
					m_AllowExceedQuota = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public AchievementDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[21];
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
				PropertyId = 4,
				PropertyDisplayName = "style_name",
				Type = typeof(string)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "points",
				Type = typeof(int)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "status",
				Type = typeof(AchievementManager.AchievementStatus)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "completion_date",
				Type = typeof(string)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "reward_list",
				Type = typeof(RewardListDataModel)
			};
			array[8] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "progress_message",
				Type = typeof(string)
			};
			array[9] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "id",
				Type = typeof(int)
			};
			array[10] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "next_tier_id",
				Type = typeof(int)
			};
			array[11] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "tier",
				Type = typeof(int)
			};
			array[12] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 13,
				PropertyDisplayName = "max_tier",
				Type = typeof(int)
			};
			array[13] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 14,
				PropertyDisplayName = "sort_order",
				Type = typeof(int)
			};
			array[14] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "reward_summary",
				Type = typeof(string)
			};
			array[15] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 16,
				PropertyDisplayName = "tier_message",
				Type = typeof(string)
			};
			array[16] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 17,
				PropertyDisplayName = "reward_track_xp",
				Type = typeof(int)
			};
			array[17] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 18,
				PropertyDisplayName = "reward_track_xp_bonus_percent",
				Type = typeof(int)
			};
			array[18] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 19,
				PropertyDisplayName = "reward_track_xp_bonus_adjusted",
				Type = typeof(int)
			};
			array[19] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 20,
				PropertyDisplayName = "allow_exceed_quota",
				Type = typeof(bool)
			};
			array[20] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_RewardList);
		}

		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((m_Name != null) ? m_Name.GetHashCode() : 0)) * 31;
			_ = m_Progress;
			int num2 = (num + m_Progress.GetHashCode()) * 31;
			_ = m_Quota;
			int num3 = (((num2 + m_Quota.GetHashCode()) * 31 + ((m_Description != null) ? m_Description.GetHashCode() : 0)) * 31 + ((m_StyleName != null) ? m_StyleName.GetHashCode() : 0)) * 31;
			_ = m_Points;
			int num4 = (num3 + m_Points.GetHashCode()) * 31;
			_ = m_Status;
			int num5 = ((((num4 + m_Status.GetHashCode()) * 31 + ((m_CompletionDate != null) ? m_CompletionDate.GetHashCode() : 0)) * 31 + ((m_RewardList != null) ? m_RewardList.GetPropertiesHashCode() : 0)) * 31 + ((m_ProgressMessage != null) ? m_ProgressMessage.GetHashCode() : 0)) * 31;
			_ = m_ID;
			int num6 = (num5 + m_ID.GetHashCode()) * 31;
			_ = m_NextTierID;
			int num7 = (num6 + m_NextTierID.GetHashCode()) * 31;
			_ = m_Tier;
			int num8 = (num7 + m_Tier.GetHashCode()) * 31;
			_ = m_MaxTier;
			int num9 = (num8 + m_MaxTier.GetHashCode()) * 31;
			_ = m_SortOrder;
			int num10 = (((num9 + m_SortOrder.GetHashCode()) * 31 + ((m_RewardSummary != null) ? m_RewardSummary.GetHashCode() : 0)) * 31 + ((m_TierMessage != null) ? m_TierMessage.GetHashCode() : 0)) * 31;
			_ = m_RewardTrackXp;
			int num11 = (num10 + m_RewardTrackXp.GetHashCode()) * 31;
			_ = m_RewardTrackXpBonusPercent;
			int num12 = (num11 + m_RewardTrackXpBonusPercent.GetHashCode()) * 31;
			_ = m_RewardTrackXpBonusAdjusted;
			int num13 = (num12 + m_RewardTrackXpBonusAdjusted.GetHashCode()) * 31;
			_ = m_AllowExceedQuota;
			return num13 + m_AllowExceedQuota.GetHashCode();
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
			case 4:
				value = m_StyleName;
				return true;
			case 5:
				value = m_Points;
				return true;
			case 6:
				value = m_Status;
				return true;
			case 7:
				value = m_CompletionDate;
				return true;
			case 8:
				value = m_RewardList;
				return true;
			case 9:
				value = m_ProgressMessage;
				return true;
			case 10:
				value = m_ID;
				return true;
			case 11:
				value = m_NextTierID;
				return true;
			case 12:
				value = m_Tier;
				return true;
			case 13:
				value = m_MaxTier;
				return true;
			case 14:
				value = m_SortOrder;
				return true;
			case 15:
				value = m_RewardSummary;
				return true;
			case 16:
				value = m_TierMessage;
				return true;
			case 17:
				value = m_RewardTrackXp;
				return true;
			case 18:
				value = m_RewardTrackXpBonusPercent;
				return true;
			case 19:
				value = m_RewardTrackXpBonusAdjusted;
				return true;
			case 20:
				value = m_AllowExceedQuota;
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
			case 4:
				StyleName = ((value != null) ? ((string)value) : null);
				return true;
			case 5:
				Points = ((value != null) ? ((int)value) : 0);
				return true;
			case 6:
				Status = ((value != null) ? ((AchievementManager.AchievementStatus)value) : AchievementManager.AchievementStatus.UNKNOWN);
				return true;
			case 7:
				CompletionDate = ((value != null) ? ((string)value) : null);
				return true;
			case 8:
				RewardList = ((value != null) ? ((RewardListDataModel)value) : null);
				return true;
			case 9:
				ProgressMessage = ((value != null) ? ((string)value) : null);
				return true;
			case 10:
				ID = ((value != null) ? ((int)value) : 0);
				return true;
			case 11:
				NextTierID = ((value != null) ? ((int)value) : 0);
				return true;
			case 12:
				Tier = ((value != null) ? ((int)value) : 0);
				return true;
			case 13:
				MaxTier = ((value != null) ? ((int)value) : 0);
				return true;
			case 14:
				SortOrder = ((value != null) ? ((int)value) : 0);
				return true;
			case 15:
				RewardSummary = ((value != null) ? ((string)value) : null);
				return true;
			case 16:
				TierMessage = ((value != null) ? ((string)value) : null);
				return true;
			case 17:
				RewardTrackXp = ((value != null) ? ((int)value) : 0);
				return true;
			case 18:
				RewardTrackXpBonusPercent = ((value != null) ? ((int)value) : 0);
				return true;
			case 19:
				RewardTrackXpBonusAdjusted = ((value != null) ? ((int)value) : 0);
				return true;
			case 20:
				AllowExceedQuota = value != null && (bool)value;
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
			case 13:
				info = Properties[13];
				return true;
			case 14:
				info = Properties[14];
				return true;
			case 15:
				info = Properties[15];
				return true;
			case 16:
				info = Properties[16];
				return true;
			case 17:
				info = Properties[17];
				return true;
			case 18:
				info = Properties[18];
				return true;
			case 19:
				info = Properties[19];
				return true;
			case 20:
				info = Properties[20];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
