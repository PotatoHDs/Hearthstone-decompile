using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class MiniSetDetailsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 266;

		private DataModelList<CardTileDataModel> m_CardTiles = new DataModelList<CardTileDataModel>();

		private CardDataModel m_SelectedCard;

		private PackDataModel m_Pack;

		private DataModelProperty[] m_properties;

		public int DataModelId => 266;

		public string DataModelDisplayName => "mini_set_details";

		public DataModelList<CardTileDataModel> CardTiles
		{
			get
			{
				return m_CardTiles;
			}
			set
			{
				if (m_CardTiles != value)
				{
					RemoveNestedDataModel(m_CardTiles);
					RegisterNestedDataModel(value);
					m_CardTiles = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public CardDataModel SelectedCard
		{
			get
			{
				return m_SelectedCard;
			}
			set
			{
				if (m_SelectedCard != value)
				{
					RemoveNestedDataModel(m_SelectedCard);
					RegisterNestedDataModel(value);
					m_SelectedCard = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public PackDataModel Pack
		{
			get
			{
				return m_Pack;
			}
			set
			{
				if (m_Pack != value)
				{
					RemoveNestedDataModel(m_Pack);
					RegisterNestedDataModel(value);
					m_Pack = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public MiniSetDetailsDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[3];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 267,
				PropertyDisplayName = "card_tiles",
				Type = typeof(DataModelList<CardTileDataModel>)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 269,
				PropertyDisplayName = "selected_card",
				Type = typeof(CardDataModel)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 273,
				PropertyDisplayName = "pack",
				Type = typeof(PackDataModel)
			};
			array[2] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_CardTiles);
			RegisterNestedDataModel(m_SelectedCard);
			RegisterNestedDataModel(m_Pack);
		}

		public int GetPropertiesHashCode()
		{
			return ((17 * 31 + ((m_CardTiles != null) ? m_CardTiles.GetPropertiesHashCode() : 0)) * 31 + ((m_SelectedCard != null) ? m_SelectedCard.GetPropertiesHashCode() : 0)) * 31 + ((m_Pack != null) ? m_Pack.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 267:
				value = m_CardTiles;
				return true;
			case 269:
				value = m_SelectedCard;
				return true;
			case 273:
				value = m_Pack;
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
			case 267:
				CardTiles = ((value != null) ? ((DataModelList<CardTileDataModel>)value) : null);
				return true;
			case 269:
				SelectedCard = ((value != null) ? ((CardDataModel)value) : null);
				return true;
			case 273:
				Pack = ((value != null) ? ((PackDataModel)value) : null);
				return true;
			default:
				return false;
			}
		}

		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 267:
				info = Properties[0];
				return true;
			case 269:
				info = Properties[1];
				return true;
			case 273:
				info = Properties[2];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
