using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.DataModels
{
	public class AdventureMissionDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 14;

		private ScenarioDbId m_ScenarioId;

		private AdventureMissionState m_MissionState;

		private Material m_CoinPortraitMaterial;

		private bool m_Selected;

		private RewardListDataModel m_Rewards;

		private bool m_NewlyUnlocked;

		private bool m_NewlyCompleted;

		private DataModelProperty[] m_properties;

		public int DataModelId => 14;

		public string DataModelDisplayName => "adventure_mission";

		public ScenarioDbId ScenarioId
		{
			get
			{
				return m_ScenarioId;
			}
			set
			{
				if (m_ScenarioId != value)
				{
					m_ScenarioId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public AdventureMissionState MissionState
		{
			get
			{
				return m_MissionState;
			}
			set
			{
				if (m_MissionState != value)
				{
					m_MissionState = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public Material CoinPortraitMaterial
		{
			get
			{
				return m_CoinPortraitMaterial;
			}
			set
			{
				if (!(m_CoinPortraitMaterial == value))
				{
					m_CoinPortraitMaterial = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool Selected
		{
			get
			{
				return m_Selected;
			}
			set
			{
				if (m_Selected != value)
				{
					m_Selected = value;
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

		public bool NewlyUnlocked
		{
			get
			{
				return m_NewlyUnlocked;
			}
			set
			{
				if (m_NewlyUnlocked != value)
				{
					m_NewlyUnlocked = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool NewlyCompleted
		{
			get
			{
				return m_NewlyCompleted;
			}
			set
			{
				if (m_NewlyCompleted != value)
				{
					m_NewlyCompleted = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public AdventureMissionDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[7];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "scenario_id",
				Type = typeof(ScenarioDbId)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "mission_state",
				Type = typeof(AdventureMissionState)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 115,
				PropertyDisplayName = "coin_portrait_material",
				Type = typeof(Material)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 118,
				PropertyDisplayName = "selected",
				Type = typeof(bool)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 136,
				PropertyDisplayName = "rewards",
				Type = typeof(RewardListDataModel)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 137,
				PropertyDisplayName = "newly_unlocked",
				Type = typeof(bool)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 138,
				PropertyDisplayName = "newly_completed",
				Type = typeof(bool)
			};
			array[6] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Rewards);
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_ScenarioId;
			int num2 = (num + m_ScenarioId.GetHashCode()) * 31;
			_ = m_MissionState;
			int num3 = ((num2 + m_MissionState.GetHashCode()) * 31 + ((m_CoinPortraitMaterial != null) ? m_CoinPortraitMaterial.GetHashCode() : 0)) * 31;
			_ = m_Selected;
			int num4 = ((num3 + m_Selected.GetHashCode()) * 31 + ((m_Rewards != null) ? m_Rewards.GetPropertiesHashCode() : 0)) * 31;
			_ = m_NewlyUnlocked;
			int num5 = (num4 + m_NewlyUnlocked.GetHashCode()) * 31;
			_ = m_NewlyCompleted;
			return num5 + m_NewlyCompleted.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_ScenarioId;
				return true;
			case 1:
				value = m_MissionState;
				return true;
			case 115:
				value = m_CoinPortraitMaterial;
				return true;
			case 118:
				value = m_Selected;
				return true;
			case 136:
				value = m_Rewards;
				return true;
			case 137:
				value = m_NewlyUnlocked;
				return true;
			case 138:
				value = m_NewlyCompleted;
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
				ScenarioId = ((value != null) ? ((ScenarioDbId)value) : ScenarioDbId.INVALID);
				return true;
			case 1:
				MissionState = ((value != null) ? ((AdventureMissionState)value) : AdventureMissionState.LOCKED);
				return true;
			case 115:
				CoinPortraitMaterial = ((value != null) ? ((Material)value) : null);
				return true;
			case 118:
				Selected = value != null && (bool)value;
				return true;
			case 136:
				Rewards = ((value != null) ? ((RewardListDataModel)value) : null);
				return true;
			case 137:
				NewlyUnlocked = value != null && (bool)value;
				return true;
			case 138:
				NewlyCompleted = value != null && (bool)value;
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
			case 115:
				info = Properties[2];
				return true;
			case 118:
				info = Properties[3];
				return true;
			case 136:
				info = Properties[4];
				return true;
			case 137:
				info = Properties[5];
				return true;
			case 138:
				info = Properties[6];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
