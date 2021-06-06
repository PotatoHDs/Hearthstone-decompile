using Hearthstone.UI;
using PegasusShared;

namespace Hearthstone.DataModels
{
	public class TavernBrawlDetailsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 158;

		private BrawlType m_BrawlType;

		private TavernBrawlMode m_BrawlMode;

		private FormatType m_FormatType;

		private int m_TicketType;

		private int m_MaxWins;

		private int m_MaxLosses;

		private string m_Title;

		private string m_RulesDesc;

		private TavernBrawlPopupType m_PopupType;

		private string m_RewardDesc;

		private string m_MinRewardDesc;

		private string m_MaxRewardDesc;

		private string m_EndConditionDesc;

		private DataModelProperty[] m_properties;

		public int DataModelId => 158;

		public string DataModelDisplayName => "tavern_brawl_details";

		public BrawlType BrawlType
		{
			get
			{
				return m_BrawlType;
			}
			set
			{
				if (m_BrawlType != value)
				{
					m_BrawlType = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public TavernBrawlMode BrawlMode
		{
			get
			{
				return m_BrawlMode;
			}
			set
			{
				if (m_BrawlMode != value)
				{
					m_BrawlMode = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public FormatType FormatType
		{
			get
			{
				return m_FormatType;
			}
			set
			{
				if (m_FormatType != value)
				{
					m_FormatType = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int TicketType
		{
			get
			{
				return m_TicketType;
			}
			set
			{
				if (m_TicketType != value)
				{
					m_TicketType = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int MaxWins
		{
			get
			{
				return m_MaxWins;
			}
			set
			{
				if (m_MaxWins != value)
				{
					m_MaxWins = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int MaxLosses
		{
			get
			{
				return m_MaxLosses;
			}
			set
			{
				if (m_MaxLosses != value)
				{
					m_MaxLosses = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string Title
		{
			get
			{
				return m_Title;
			}
			set
			{
				if (!(m_Title == value))
				{
					m_Title = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string RulesDesc
		{
			get
			{
				return m_RulesDesc;
			}
			set
			{
				if (!(m_RulesDesc == value))
				{
					m_RulesDesc = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public TavernBrawlPopupType PopupType
		{
			get
			{
				return m_PopupType;
			}
			set
			{
				if (m_PopupType != value)
				{
					m_PopupType = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string RewardDesc
		{
			get
			{
				return m_RewardDesc;
			}
			set
			{
				if (!(m_RewardDesc == value))
				{
					m_RewardDesc = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string MinRewardDesc
		{
			get
			{
				return m_MinRewardDesc;
			}
			set
			{
				if (!(m_MinRewardDesc == value))
				{
					m_MinRewardDesc = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string MaxRewardDesc
		{
			get
			{
				return m_MaxRewardDesc;
			}
			set
			{
				if (!(m_MaxRewardDesc == value))
				{
					m_MaxRewardDesc = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string EndConditionDesc
		{
			get
			{
				return m_EndConditionDesc;
			}
			set
			{
				if (!(m_EndConditionDesc == value))
				{
					m_EndConditionDesc = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public TavernBrawlDetailsDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[13];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "brawl_type",
				Type = typeof(BrawlType)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "brawl_mode",
				Type = typeof(TavernBrawlMode)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "format_type",
				Type = typeof(FormatType)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "ticket_type",
				Type = typeof(int)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "max_wins",
				Type = typeof(int)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "max_losses",
				Type = typeof(int)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "title",
				Type = typeof(string)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "rules_desc",
				Type = typeof(string)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "popup_type",
				Type = typeof(TavernBrawlPopupType)
			};
			array[8] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "reward_desc",
				Type = typeof(string)
			};
			array[9] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "min_reward_desc",
				Type = typeof(string)
			};
			array[10] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "max_reward_desc",
				Type = typeof(string)
			};
			array[11] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "end_condition_desc",
				Type = typeof(string)
			};
			array[12] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_BrawlType;
			int num2 = (num + m_BrawlType.GetHashCode()) * 31;
			_ = m_BrawlMode;
			int num3 = (num2 + m_BrawlMode.GetHashCode()) * 31;
			_ = m_FormatType;
			int num4 = (num3 + m_FormatType.GetHashCode()) * 31;
			_ = m_TicketType;
			int num5 = (num4 + m_TicketType.GetHashCode()) * 31;
			_ = m_MaxWins;
			int num6 = (num5 + m_MaxWins.GetHashCode()) * 31;
			_ = m_MaxLosses;
			int num7 = (((num6 + m_MaxLosses.GetHashCode()) * 31 + ((m_Title != null) ? m_Title.GetHashCode() : 0)) * 31 + ((m_RulesDesc != null) ? m_RulesDesc.GetHashCode() : 0)) * 31;
			_ = m_PopupType;
			return ((((num7 + m_PopupType.GetHashCode()) * 31 + ((m_RewardDesc != null) ? m_RewardDesc.GetHashCode() : 0)) * 31 + ((m_MinRewardDesc != null) ? m_MinRewardDesc.GetHashCode() : 0)) * 31 + ((m_MaxRewardDesc != null) ? m_MaxRewardDesc.GetHashCode() : 0)) * 31 + ((m_EndConditionDesc != null) ? m_EndConditionDesc.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_BrawlType;
				return true;
			case 1:
				value = m_BrawlMode;
				return true;
			case 2:
				value = m_FormatType;
				return true;
			case 3:
				value = m_TicketType;
				return true;
			case 4:
				value = m_MaxWins;
				return true;
			case 5:
				value = m_MaxLosses;
				return true;
			case 6:
				value = m_Title;
				return true;
			case 7:
				value = m_RulesDesc;
				return true;
			case 8:
				value = m_PopupType;
				return true;
			case 9:
				value = m_RewardDesc;
				return true;
			case 10:
				value = m_MinRewardDesc;
				return true;
			case 11:
				value = m_MaxRewardDesc;
				return true;
			case 12:
				value = m_EndConditionDesc;
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
				BrawlType = ((value != null) ? ((BrawlType)value) : BrawlType.BRAWL_TYPE_UNKNOWN);
				return true;
			case 1:
				BrawlMode = ((value != null) ? ((TavernBrawlMode)value) : TavernBrawlMode.TB_MODE_NORMAL);
				return true;
			case 2:
				FormatType = ((value != null) ? ((FormatType)value) : FormatType.FT_UNKNOWN);
				return true;
			case 3:
				TicketType = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				MaxWins = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				MaxLosses = ((value != null) ? ((int)value) : 0);
				return true;
			case 6:
				Title = ((value != null) ? ((string)value) : null);
				return true;
			case 7:
				RulesDesc = ((value != null) ? ((string)value) : null);
				return true;
			case 8:
				PopupType = ((value != null) ? ((TavernBrawlPopupType)value) : TavernBrawlPopupType.POPUP_TYPE_NONE);
				return true;
			case 9:
				RewardDesc = ((value != null) ? ((string)value) : null);
				return true;
			case 10:
				MinRewardDesc = ((value != null) ? ((string)value) : null);
				return true;
			case 11:
				MaxRewardDesc = ((value != null) ? ((string)value) : null);
				return true;
			case 12:
				EndConditionDesc = ((value != null) ? ((string)value) : null);
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
