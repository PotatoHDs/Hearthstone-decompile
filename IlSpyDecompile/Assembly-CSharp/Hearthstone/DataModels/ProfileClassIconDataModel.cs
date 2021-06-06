using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class ProfileClassIconDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 232;

		private TAG_CLASS m_TagClass;

		private bool m_IsUnlocked;

		private bool m_IsGolden;

		private int m_CurrentLevel;

		private int m_MaxLevel;

		private long m_CurrentLevelXP;

		private long m_CurrentLevelXPMax;

		private string m_WinsText;

		private bool m_IsMaxLevel;

		private string m_Name;

		private string m_TooltipTitle;

		private string m_TooltipDesc;

		private bool m_IsPremium;

		private int m_GoldWinsReq;

		private int m_PremiumWinsReq;

		private int m_Wins;

		private DataModelProperty[] m_properties;

		public int DataModelId => 232;

		public string DataModelDisplayName => "profile_class_icon";

		public TAG_CLASS TagClass
		{
			get
			{
				return m_TagClass;
			}
			set
			{
				if (m_TagClass != value)
				{
					m_TagClass = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsUnlocked
		{
			get
			{
				return m_IsUnlocked;
			}
			set
			{
				if (m_IsUnlocked != value)
				{
					m_IsUnlocked = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsGolden
		{
			get
			{
				return m_IsGolden;
			}
			set
			{
				if (m_IsGolden != value)
				{
					m_IsGolden = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int CurrentLevel
		{
			get
			{
				return m_CurrentLevel;
			}
			set
			{
				if (m_CurrentLevel != value)
				{
					m_CurrentLevel = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int MaxLevel
		{
			get
			{
				return m_MaxLevel;
			}
			set
			{
				if (m_MaxLevel != value)
				{
					m_MaxLevel = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public long CurrentLevelXP
		{
			get
			{
				return m_CurrentLevelXP;
			}
			set
			{
				if (m_CurrentLevelXP != value)
				{
					m_CurrentLevelXP = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public long CurrentLevelXPMax
		{
			get
			{
				return m_CurrentLevelXPMax;
			}
			set
			{
				if (m_CurrentLevelXPMax != value)
				{
					m_CurrentLevelXPMax = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string WinsText
		{
			get
			{
				return m_WinsText;
			}
			set
			{
				if (!(m_WinsText == value))
				{
					m_WinsText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsMaxLevel
		{
			get
			{
				return m_IsMaxLevel;
			}
			set
			{
				if (m_IsMaxLevel != value)
				{
					m_IsMaxLevel = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

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

		public string TooltipTitle
		{
			get
			{
				return m_TooltipTitle;
			}
			set
			{
				if (!(m_TooltipTitle == value))
				{
					m_TooltipTitle = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string TooltipDesc
		{
			get
			{
				return m_TooltipDesc;
			}
			set
			{
				if (!(m_TooltipDesc == value))
				{
					m_TooltipDesc = value;
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

		public int GoldWinsReq
		{
			get
			{
				return m_GoldWinsReq;
			}
			set
			{
				if (m_GoldWinsReq != value)
				{
					m_GoldWinsReq = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int PremiumWinsReq
		{
			get
			{
				return m_PremiumWinsReq;
			}
			set
			{
				if (m_PremiumWinsReq != value)
				{
					m_PremiumWinsReq = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Wins
		{
			get
			{
				return m_Wins;
			}
			set
			{
				if (m_Wins != value)
				{
					m_Wins = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public ProfileClassIconDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[16];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "tag_class",
				Type = typeof(TAG_CLASS)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "is_unlocked",
				Type = typeof(bool)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "is_golden",
				Type = typeof(bool)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "current_level",
				Type = typeof(int)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "max_level",
				Type = typeof(int)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "current_level_xp",
				Type = typeof(long)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "current_level_xp_max",
				Type = typeof(long)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "wins_text",
				Type = typeof(string)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "is_max_level",
				Type = typeof(bool)
			};
			array[8] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "name",
				Type = typeof(string)
			};
			array[9] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "tooltip_title",
				Type = typeof(string)
			};
			array[10] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "tooltip_desc",
				Type = typeof(string)
			};
			array[11] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "is_premium",
				Type = typeof(bool)
			};
			array[12] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 13,
				PropertyDisplayName = "gold_wins_req",
				Type = typeof(int)
			};
			array[13] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 14,
				PropertyDisplayName = "premium_wins_req",
				Type = typeof(int)
			};
			array[14] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "wins",
				Type = typeof(int)
			};
			array[15] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_TagClass;
			int num2 = (num + m_TagClass.GetHashCode()) * 31;
			_ = m_IsUnlocked;
			int num3 = (num2 + m_IsUnlocked.GetHashCode()) * 31;
			_ = m_IsGolden;
			int num4 = (num3 + m_IsGolden.GetHashCode()) * 31;
			_ = m_CurrentLevel;
			int num5 = (num4 + m_CurrentLevel.GetHashCode()) * 31;
			_ = m_MaxLevel;
			int num6 = (num5 + m_MaxLevel.GetHashCode()) * 31;
			_ = m_CurrentLevelXP;
			int num7 = (num6 + m_CurrentLevelXP.GetHashCode()) * 31;
			_ = m_CurrentLevelXPMax;
			int num8 = ((num7 + m_CurrentLevelXPMax.GetHashCode()) * 31 + ((m_WinsText != null) ? m_WinsText.GetHashCode() : 0)) * 31;
			_ = m_IsMaxLevel;
			int num9 = ((((num8 + m_IsMaxLevel.GetHashCode()) * 31 + ((m_Name != null) ? m_Name.GetHashCode() : 0)) * 31 + ((m_TooltipTitle != null) ? m_TooltipTitle.GetHashCode() : 0)) * 31 + ((m_TooltipDesc != null) ? m_TooltipDesc.GetHashCode() : 0)) * 31;
			_ = m_IsPremium;
			int num10 = (num9 + m_IsPremium.GetHashCode()) * 31;
			_ = m_GoldWinsReq;
			int num11 = (num10 + m_GoldWinsReq.GetHashCode()) * 31;
			_ = m_PremiumWinsReq;
			int num12 = (num11 + m_PremiumWinsReq.GetHashCode()) * 31;
			_ = m_Wins;
			return num12 + m_Wins.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_TagClass;
				return true;
			case 1:
				value = m_IsUnlocked;
				return true;
			case 2:
				value = m_IsGolden;
				return true;
			case 3:
				value = m_CurrentLevel;
				return true;
			case 4:
				value = m_MaxLevel;
				return true;
			case 5:
				value = m_CurrentLevelXP;
				return true;
			case 6:
				value = m_CurrentLevelXPMax;
				return true;
			case 7:
				value = m_WinsText;
				return true;
			case 8:
				value = m_IsMaxLevel;
				return true;
			case 9:
				value = m_Name;
				return true;
			case 10:
				value = m_TooltipTitle;
				return true;
			case 11:
				value = m_TooltipDesc;
				return true;
			case 12:
				value = m_IsPremium;
				return true;
			case 13:
				value = m_GoldWinsReq;
				return true;
			case 14:
				value = m_PremiumWinsReq;
				return true;
			case 15:
				value = m_Wins;
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
				TagClass = ((value != null) ? ((TAG_CLASS)value) : TAG_CLASS.INVALID);
				return true;
			case 1:
				IsUnlocked = value != null && (bool)value;
				return true;
			case 2:
				IsGolden = value != null && (bool)value;
				return true;
			case 3:
				CurrentLevel = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				MaxLevel = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				CurrentLevelXP = ((value != null) ? ((long)value) : 0);
				return true;
			case 6:
				CurrentLevelXPMax = ((value != null) ? ((long)value) : 0);
				return true;
			case 7:
				WinsText = ((value != null) ? ((string)value) : null);
				return true;
			case 8:
				IsMaxLevel = value != null && (bool)value;
				return true;
			case 9:
				Name = ((value != null) ? ((string)value) : null);
				return true;
			case 10:
				TooltipTitle = ((value != null) ? ((string)value) : null);
				return true;
			case 11:
				TooltipDesc = ((value != null) ? ((string)value) : null);
				return true;
			case 12:
				IsPremium = value != null && (bool)value;
				return true;
			case 13:
				GoldWinsReq = ((value != null) ? ((int)value) : 0);
				return true;
			case 14:
				PremiumWinsReq = ((value != null) ? ((int)value) : 0);
				return true;
			case 15:
				Wins = ((value != null) ? ((int)value) : 0);
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
