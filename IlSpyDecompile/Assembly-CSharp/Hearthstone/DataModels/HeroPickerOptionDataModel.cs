using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class HeroPickerOptionDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 6;

		private bool m_IsTimelocked;

		private bool m_IsNewlyUnlocked;

		private string m_TimeLockInfoText;

		private bool m_IsUnowned;

		private string m_UnlockCriteriaText;

		private string m_ComingSoonText;

		private DataModelProperty[] m_properties;

		public int DataModelId => 6;

		public string DataModelDisplayName => "hero_picker_option";

		public bool IsTimelocked
		{
			get
			{
				return m_IsTimelocked;
			}
			set
			{
				if (m_IsTimelocked != value)
				{
					m_IsTimelocked = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsNewlyUnlocked
		{
			get
			{
				return m_IsNewlyUnlocked;
			}
			set
			{
				if (m_IsNewlyUnlocked != value)
				{
					m_IsNewlyUnlocked = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string TimeLockInfoText
		{
			get
			{
				return m_TimeLockInfoText;
			}
			set
			{
				if (!(m_TimeLockInfoText == value))
				{
					m_TimeLockInfoText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsUnowned
		{
			get
			{
				return m_IsUnowned;
			}
			set
			{
				if (m_IsUnowned != value)
				{
					m_IsUnowned = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string UnlockCriteriaText
		{
			get
			{
				return m_UnlockCriteriaText;
			}
			set
			{
				if (!(m_UnlockCriteriaText == value))
				{
					m_UnlockCriteriaText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string ComingSoonText
		{
			get
			{
				return m_ComingSoonText;
			}
			set
			{
				if (!(m_ComingSoonText == value))
				{
					m_ComingSoonText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public HeroPickerOptionDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[6];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "is_timelocked",
				Type = typeof(bool)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "is_newly_unlocked",
				Type = typeof(bool)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "time_lock_info_text",
				Type = typeof(string)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "is_unowned",
				Type = typeof(bool)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "unlock_criteria_text",
				Type = typeof(string)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "coming_soon_text",
				Type = typeof(string)
			};
			array[5] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_IsTimelocked;
			int num2 = (num + m_IsTimelocked.GetHashCode()) * 31;
			_ = m_IsNewlyUnlocked;
			int num3 = ((num2 + m_IsNewlyUnlocked.GetHashCode()) * 31 + ((m_TimeLockInfoText != null) ? m_TimeLockInfoText.GetHashCode() : 0)) * 31;
			_ = m_IsUnowned;
			return ((num3 + m_IsUnowned.GetHashCode()) * 31 + ((m_UnlockCriteriaText != null) ? m_UnlockCriteriaText.GetHashCode() : 0)) * 31 + ((m_ComingSoonText != null) ? m_ComingSoonText.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_IsTimelocked;
				return true;
			case 1:
				value = m_IsNewlyUnlocked;
				return true;
			case 2:
				value = m_TimeLockInfoText;
				return true;
			case 3:
				value = m_IsUnowned;
				return true;
			case 4:
				value = m_UnlockCriteriaText;
				return true;
			case 5:
				value = m_ComingSoonText;
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
				IsTimelocked = value != null && (bool)value;
				return true;
			case 1:
				IsNewlyUnlocked = value != null && (bool)value;
				return true;
			case 2:
				TimeLockInfoText = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				IsUnowned = value != null && (bool)value;
				return true;
			case 4:
				UnlockCriteriaText = ((value != null) ? ((string)value) : null);
				return true;
			case 5:
				ComingSoonText = ((value != null) ? ((string)value) : null);
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
