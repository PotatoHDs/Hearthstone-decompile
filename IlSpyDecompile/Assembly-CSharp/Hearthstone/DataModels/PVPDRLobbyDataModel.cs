using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class PVPDRLobbyDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 181;

		private bool m_HasSession;

		private int m_Season;

		private int m_Wins;

		private int m_Losses;

		private int m_Rating;

		private string m_TimeRemainingString;

		private int m_PaidRating;

		private int m_HighWatermark;

		private string m_Unlocks;

		private bool m_RecentWin;

		private bool m_RecentLoss;

		private bool m_IsEarlyAccess;

		private bool m_IsPaidEntry;

		private bool m_IsSessionActive;

		private int m_LastRatingChange;

		private bool m_IsPaidUnlocked;

		private bool m_IsFreeUnlocked;

		private string m_NoticeHeaderString;

		private string m_NoticeDescString;

		private int m_LastPlayedMode;

		private bool m_IsRatingNotice;

		private string m_NoticeRatingString;

		private DataModelProperty[] m_properties;

		public int DataModelId => 181;

		public string DataModelDisplayName => "pvpdrlobby";

		public bool HasSession
		{
			get
			{
				return m_HasSession;
			}
			set
			{
				if (m_HasSession != value)
				{
					m_HasSession = value;
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

		public int Losses
		{
			get
			{
				return m_Losses;
			}
			set
			{
				if (m_Losses != value)
				{
					m_Losses = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Rating
		{
			get
			{
				return m_Rating;
			}
			set
			{
				if (m_Rating != value)
				{
					m_Rating = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string TimeRemainingString
		{
			get
			{
				return m_TimeRemainingString;
			}
			set
			{
				if (!(m_TimeRemainingString == value))
				{
					m_TimeRemainingString = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int PaidRating
		{
			get
			{
				return m_PaidRating;
			}
			set
			{
				if (m_PaidRating != value)
				{
					m_PaidRating = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int HighWatermark
		{
			get
			{
				return m_HighWatermark;
			}
			set
			{
				if (m_HighWatermark != value)
				{
					m_HighWatermark = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string Unlocks
		{
			get
			{
				return m_Unlocks;
			}
			set
			{
				if (!(m_Unlocks == value))
				{
					m_Unlocks = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool RecentWin
		{
			get
			{
				return m_RecentWin;
			}
			set
			{
				if (m_RecentWin != value)
				{
					m_RecentWin = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool RecentLoss
		{
			get
			{
				return m_RecentLoss;
			}
			set
			{
				if (m_RecentLoss != value)
				{
					m_RecentLoss = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsEarlyAccess
		{
			get
			{
				return m_IsEarlyAccess;
			}
			set
			{
				if (m_IsEarlyAccess != value)
				{
					m_IsEarlyAccess = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsPaidEntry
		{
			get
			{
				return m_IsPaidEntry;
			}
			set
			{
				if (m_IsPaidEntry != value)
				{
					m_IsPaidEntry = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsSessionActive
		{
			get
			{
				return m_IsSessionActive;
			}
			set
			{
				if (m_IsSessionActive != value)
				{
					m_IsSessionActive = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int LastRatingChange
		{
			get
			{
				return m_LastRatingChange;
			}
			set
			{
				if (m_LastRatingChange != value)
				{
					m_LastRatingChange = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsPaidUnlocked
		{
			get
			{
				return m_IsPaidUnlocked;
			}
			set
			{
				if (m_IsPaidUnlocked != value)
				{
					m_IsPaidUnlocked = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsFreeUnlocked
		{
			get
			{
				return m_IsFreeUnlocked;
			}
			set
			{
				if (m_IsFreeUnlocked != value)
				{
					m_IsFreeUnlocked = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string NoticeHeaderString
		{
			get
			{
				return m_NoticeHeaderString;
			}
			set
			{
				if (!(m_NoticeHeaderString == value))
				{
					m_NoticeHeaderString = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string NoticeDescString
		{
			get
			{
				return m_NoticeDescString;
			}
			set
			{
				if (!(m_NoticeDescString == value))
				{
					m_NoticeDescString = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int LastPlayedMode
		{
			get
			{
				return m_LastPlayedMode;
			}
			set
			{
				if (m_LastPlayedMode != value)
				{
					m_LastPlayedMode = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsRatingNotice
		{
			get
			{
				return m_IsRatingNotice;
			}
			set
			{
				if (m_IsRatingNotice != value)
				{
					m_IsRatingNotice = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string NoticeRatingString
		{
			get
			{
				return m_NoticeRatingString;
			}
			set
			{
				if (!(m_NoticeRatingString == value))
				{
					m_NoticeRatingString = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public PVPDRLobbyDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[22];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 182,
				PropertyDisplayName = "has_session",
				Type = typeof(bool)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 183,
				PropertyDisplayName = "season",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 184,
				PropertyDisplayName = "wins",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 185,
				PropertyDisplayName = "losses",
				Type = typeof(int)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 186,
				PropertyDisplayName = "rating",
				Type = typeof(int)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 187,
				PropertyDisplayName = "time_remaining_string",
				Type = typeof(string)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 188,
				PropertyDisplayName = "paid_rating",
				Type = typeof(int)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 211,
				PropertyDisplayName = "high_watermark",
				Type = typeof(int)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 212,
				PropertyDisplayName = "unlocks",
				Type = typeof(string)
			};
			array[8] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 219,
				PropertyDisplayName = "recent_win",
				Type = typeof(bool)
			};
			array[9] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 220,
				PropertyDisplayName = "recent_loss",
				Type = typeof(bool)
			};
			array[10] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 235,
				PropertyDisplayName = "is_early_access",
				Type = typeof(bool)
			};
			array[11] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 237,
				PropertyDisplayName = "is_paid_entry",
				Type = typeof(bool)
			};
			array[12] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 241,
				PropertyDisplayName = "is_session_active",
				Type = typeof(bool)
			};
			array[13] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 243,
				PropertyDisplayName = "last_rating_change",
				Type = typeof(int)
			};
			array[14] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 244,
				PropertyDisplayName = "is_paid_unlocked",
				Type = typeof(bool)
			};
			array[15] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 245,
				PropertyDisplayName = "is_free_unlocked",
				Type = typeof(bool)
			};
			array[16] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 257,
				PropertyDisplayName = "notice_header_string",
				Type = typeof(string)
			};
			array[17] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 258,
				PropertyDisplayName = "notice_desc_string",
				Type = typeof(string)
			};
			array[18] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 260,
				PropertyDisplayName = "last_played_mode",
				Type = typeof(int)
			};
			array[19] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 261,
				PropertyDisplayName = "is_rating_notice",
				Type = typeof(bool)
			};
			array[20] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 262,
				PropertyDisplayName = "notice_rating_string",
				Type = typeof(string)
			};
			array[21] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_HasSession;
			int num2 = (num + m_HasSession.GetHashCode()) * 31;
			_ = m_Season;
			int num3 = (num2 + m_Season.GetHashCode()) * 31;
			_ = m_Wins;
			int num4 = (num3 + m_Wins.GetHashCode()) * 31;
			_ = m_Losses;
			int num5 = (num4 + m_Losses.GetHashCode()) * 31;
			_ = m_Rating;
			int num6 = ((num5 + m_Rating.GetHashCode()) * 31 + ((m_TimeRemainingString != null) ? m_TimeRemainingString.GetHashCode() : 0)) * 31;
			_ = m_PaidRating;
			int num7 = (num6 + m_PaidRating.GetHashCode()) * 31;
			_ = m_HighWatermark;
			int num8 = ((num7 + m_HighWatermark.GetHashCode()) * 31 + ((m_Unlocks != null) ? m_Unlocks.GetHashCode() : 0)) * 31;
			_ = m_RecentWin;
			int num9 = (num8 + m_RecentWin.GetHashCode()) * 31;
			_ = m_RecentLoss;
			int num10 = (num9 + m_RecentLoss.GetHashCode()) * 31;
			_ = m_IsEarlyAccess;
			int num11 = (num10 + m_IsEarlyAccess.GetHashCode()) * 31;
			_ = m_IsPaidEntry;
			int num12 = (num11 + m_IsPaidEntry.GetHashCode()) * 31;
			_ = m_IsSessionActive;
			int num13 = (num12 + m_IsSessionActive.GetHashCode()) * 31;
			_ = m_LastRatingChange;
			int num14 = (num13 + m_LastRatingChange.GetHashCode()) * 31;
			_ = m_IsPaidUnlocked;
			int num15 = (num14 + m_IsPaidUnlocked.GetHashCode()) * 31;
			_ = m_IsFreeUnlocked;
			int num16 = (((num15 + m_IsFreeUnlocked.GetHashCode()) * 31 + ((m_NoticeHeaderString != null) ? m_NoticeHeaderString.GetHashCode() : 0)) * 31 + ((m_NoticeDescString != null) ? m_NoticeDescString.GetHashCode() : 0)) * 31;
			_ = m_LastPlayedMode;
			int num17 = (num16 + m_LastPlayedMode.GetHashCode()) * 31;
			_ = m_IsRatingNotice;
			return (num17 + m_IsRatingNotice.GetHashCode()) * 31 + ((m_NoticeRatingString != null) ? m_NoticeRatingString.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 182:
				value = m_HasSession;
				return true;
			case 183:
				value = m_Season;
				return true;
			case 184:
				value = m_Wins;
				return true;
			case 185:
				value = m_Losses;
				return true;
			case 186:
				value = m_Rating;
				return true;
			case 187:
				value = m_TimeRemainingString;
				return true;
			case 188:
				value = m_PaidRating;
				return true;
			case 211:
				value = m_HighWatermark;
				return true;
			case 212:
				value = m_Unlocks;
				return true;
			case 219:
				value = m_RecentWin;
				return true;
			case 220:
				value = m_RecentLoss;
				return true;
			case 235:
				value = m_IsEarlyAccess;
				return true;
			case 237:
				value = m_IsPaidEntry;
				return true;
			case 241:
				value = m_IsSessionActive;
				return true;
			case 243:
				value = m_LastRatingChange;
				return true;
			case 244:
				value = m_IsPaidUnlocked;
				return true;
			case 245:
				value = m_IsFreeUnlocked;
				return true;
			case 257:
				value = m_NoticeHeaderString;
				return true;
			case 258:
				value = m_NoticeDescString;
				return true;
			case 260:
				value = m_LastPlayedMode;
				return true;
			case 261:
				value = m_IsRatingNotice;
				return true;
			case 262:
				value = m_NoticeRatingString;
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
			case 182:
				HasSession = value != null && (bool)value;
				return true;
			case 183:
				Season = ((value != null) ? ((int)value) : 0);
				return true;
			case 184:
				Wins = ((value != null) ? ((int)value) : 0);
				return true;
			case 185:
				Losses = ((value != null) ? ((int)value) : 0);
				return true;
			case 186:
				Rating = ((value != null) ? ((int)value) : 0);
				return true;
			case 187:
				TimeRemainingString = ((value != null) ? ((string)value) : null);
				return true;
			case 188:
				PaidRating = ((value != null) ? ((int)value) : 0);
				return true;
			case 211:
				HighWatermark = ((value != null) ? ((int)value) : 0);
				return true;
			case 212:
				Unlocks = ((value != null) ? ((string)value) : null);
				return true;
			case 219:
				RecentWin = value != null && (bool)value;
				return true;
			case 220:
				RecentLoss = value != null && (bool)value;
				return true;
			case 235:
				IsEarlyAccess = value != null && (bool)value;
				return true;
			case 237:
				IsPaidEntry = value != null && (bool)value;
				return true;
			case 241:
				IsSessionActive = value != null && (bool)value;
				return true;
			case 243:
				LastRatingChange = ((value != null) ? ((int)value) : 0);
				return true;
			case 244:
				IsPaidUnlocked = value != null && (bool)value;
				return true;
			case 245:
				IsFreeUnlocked = value != null && (bool)value;
				return true;
			case 257:
				NoticeHeaderString = ((value != null) ? ((string)value) : null);
				return true;
			case 258:
				NoticeDescString = ((value != null) ? ((string)value) : null);
				return true;
			case 260:
				LastPlayedMode = ((value != null) ? ((int)value) : 0);
				return true;
			case 261:
				IsRatingNotice = value != null && (bool)value;
				return true;
			case 262:
				NoticeRatingString = ((value != null) ? ((string)value) : null);
				return true;
			default:
				return false;
			}
		}

		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 182:
				info = Properties[0];
				return true;
			case 183:
				info = Properties[1];
				return true;
			case 184:
				info = Properties[2];
				return true;
			case 185:
				info = Properties[3];
				return true;
			case 186:
				info = Properties[4];
				return true;
			case 187:
				info = Properties[5];
				return true;
			case 188:
				info = Properties[6];
				return true;
			case 211:
				info = Properties[7];
				return true;
			case 212:
				info = Properties[8];
				return true;
			case 219:
				info = Properties[9];
				return true;
			case 220:
				info = Properties[10];
				return true;
			case 235:
				info = Properties[11];
				return true;
			case 237:
				info = Properties[12];
				return true;
			case 241:
				info = Properties[13];
				return true;
			case 243:
				info = Properties[14];
				return true;
			case 244:
				info = Properties[15];
				return true;
			case 245:
				info = Properties[16];
				return true;
			case 257:
				info = Properties[17];
				return true;
			case 258:
				info = Properties[18];
				return true;
			case 260:
				info = Properties[19];
				return true;
			case 261:
				info = Properties[20];
				return true;
			case 262:
				info = Properties[21];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
