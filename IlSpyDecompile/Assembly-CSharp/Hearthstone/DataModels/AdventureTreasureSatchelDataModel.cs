using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class AdventureTreasureSatchelDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 32;

		private DataModelList<CardDataModel> m_Cards = new DataModelList<CardDataModel>();

		private DataModelList<AdventureLoadoutOptionDataModel> m_LoadoutOptions = new DataModelList<AdventureLoadoutOptionDataModel>();

		private DataModelProperty[] m_properties;

		public int DataModelId => 32;

		public string DataModelDisplayName => "adventure_treasure_satchel";

		public DataModelList<CardDataModel> Cards
		{
			get
			{
				return m_Cards;
			}
			set
			{
				if (m_Cards != value)
				{
					RemoveNestedDataModel(m_Cards);
					RegisterNestedDataModel(value);
					m_Cards = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<AdventureLoadoutOptionDataModel> LoadoutOptions
		{
			get
			{
				return m_LoadoutOptions;
			}
			set
			{
				if (m_LoadoutOptions != value)
				{
					RemoveNestedDataModel(m_LoadoutOptions);
					RegisterNestedDataModel(value);
					m_LoadoutOptions = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public AdventureTreasureSatchelDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[2];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "cards",
				Type = typeof(DataModelList<CardDataModel>)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "loadout_options",
				Type = typeof(DataModelList<AdventureLoadoutOptionDataModel>)
			};
			array[1] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Cards);
			RegisterNestedDataModel(m_LoadoutOptions);
		}

		public int GetPropertiesHashCode()
		{
			return (17 * 31 + ((m_Cards != null) ? m_Cards.GetPropertiesHashCode() : 0)) * 31 + ((m_LoadoutOptions != null) ? m_LoadoutOptions.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Cards;
				return true;
			case 1:
				value = m_LoadoutOptions;
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
				Cards = ((value != null) ? ((DataModelList<CardDataModel>)value) : null);
				return true;
			case 1:
				LoadoutOptions = ((value != null) ? ((DataModelList<AdventureLoadoutOptionDataModel>)value) : null);
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
