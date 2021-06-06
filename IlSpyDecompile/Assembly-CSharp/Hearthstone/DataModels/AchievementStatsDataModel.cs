using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class AchievementStatsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 240;

		private int m_Points;

		private int m_AvailablePoints;

		private int m_Unclaimed;

		private int m_CompletedAchievements;

		private int m_TotalAchievements;

		private string m_CompletionPercentage;

		private DataModelProperty[] m_properties;

		public int DataModelId => 240;

		public string DataModelDisplayName => "achievement_stats";

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

		public int AvailablePoints
		{
			get
			{
				return m_AvailablePoints;
			}
			set
			{
				if (m_AvailablePoints != value)
				{
					m_AvailablePoints = value;
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

		public int CompletedAchievements
		{
			get
			{
				return m_CompletedAchievements;
			}
			set
			{
				if (m_CompletedAchievements != value)
				{
					m_CompletedAchievements = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int TotalAchievements
		{
			get
			{
				return m_TotalAchievements;
			}
			set
			{
				if (m_TotalAchievements != value)
				{
					m_TotalAchievements = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string CompletionPercentage
		{
			get
			{
				return m_CompletionPercentage;
			}
			set
			{
				if (!(m_CompletionPercentage == value))
				{
					m_CompletionPercentage = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public AchievementStatsDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[6];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "points",
				Type = typeof(int)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "available_points",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "unclaimed",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "completed_achievements",
				Type = typeof(int)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "total_achievements",
				Type = typeof(int)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "completion_percentage",
				Type = typeof(string)
			};
			array[5] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_Points;
			int num2 = (num + m_Points.GetHashCode()) * 31;
			_ = m_AvailablePoints;
			int num3 = (num2 + m_AvailablePoints.GetHashCode()) * 31;
			_ = m_Unclaimed;
			int num4 = (num3 + m_Unclaimed.GetHashCode()) * 31;
			_ = m_CompletedAchievements;
			int num5 = (num4 + m_CompletedAchievements.GetHashCode()) * 31;
			_ = m_TotalAchievements;
			return (num5 + m_TotalAchievements.GetHashCode()) * 31 + ((m_CompletionPercentage != null) ? m_CompletionPercentage.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Points;
				return true;
			case 1:
				value = m_AvailablePoints;
				return true;
			case 2:
				value = m_Unclaimed;
				return true;
			case 3:
				value = m_CompletedAchievements;
				return true;
			case 4:
				value = m_TotalAchievements;
				return true;
			case 5:
				value = m_CompletionPercentage;
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
				Points = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				AvailablePoints = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				Unclaimed = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				CompletedAchievements = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				TotalAchievements = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				CompletionPercentage = ((value != null) ? ((string)value) : null);
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
