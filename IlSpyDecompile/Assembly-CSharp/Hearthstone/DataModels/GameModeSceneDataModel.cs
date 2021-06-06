using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class GameModeSceneDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 173;

		private DataModelList<GameModeButtonDataModel> m_GameModeButtons = new DataModelList<GameModeButtonDataModel>();

		private int m_LastSelectedGameModeRecordId;

		private DataModelProperty[] m_properties;

		public int DataModelId => 173;

		public string DataModelDisplayName => "gamemodescene";

		public DataModelList<GameModeButtonDataModel> GameModeButtons
		{
			get
			{
				return m_GameModeButtons;
			}
			set
			{
				if (m_GameModeButtons != value)
				{
					RemoveNestedDataModel(m_GameModeButtons);
					RegisterNestedDataModel(value);
					m_GameModeButtons = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int LastSelectedGameModeRecordId
		{
			get
			{
				return m_LastSelectedGameModeRecordId;
			}
			set
			{
				if (m_LastSelectedGameModeRecordId != value)
				{
					m_LastSelectedGameModeRecordId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public GameModeSceneDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[2];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "gamemodesbuttons",
				Type = typeof(DataModelList<GameModeButtonDataModel>)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "lastselectedgamemoderecordid",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_GameModeButtons);
		}

		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((m_GameModeButtons != null) ? m_GameModeButtons.GetPropertiesHashCode() : 0)) * 31;
			_ = m_LastSelectedGameModeRecordId;
			return num + m_LastSelectedGameModeRecordId.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_GameModeButtons;
				return true;
			case 1:
				value = m_LastSelectedGameModeRecordId;
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
				GameModeButtons = ((value != null) ? ((DataModelList<GameModeButtonDataModel>)value) : null);
				return true;
			case 1:
				LastSelectedGameModeRecordId = ((value != null) ? ((int)value) : 0);
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
