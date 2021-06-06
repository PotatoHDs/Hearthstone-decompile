using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class BaconPastGameStatsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 140;

		private CardDataModel m_Hero;

		private CardDataModel m_HeroPower;

		private int m_Place;

		private DataModelList<CardDataModel> m_Minions = new DataModelList<CardDataModel>();

		private string m_HeroName;

		private DataModelProperty[] m_properties;

		public int DataModelId => 140;

		public string DataModelDisplayName => "baconpastgamestats";

		public CardDataModel Hero
		{
			get
			{
				return m_Hero;
			}
			set
			{
				if (m_Hero != value)
				{
					RemoveNestedDataModel(m_Hero);
					RegisterNestedDataModel(value);
					m_Hero = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public CardDataModel HeroPower
		{
			get
			{
				return m_HeroPower;
			}
			set
			{
				if (m_HeroPower != value)
				{
					RemoveNestedDataModel(m_HeroPower);
					RegisterNestedDataModel(value);
					m_HeroPower = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Place
		{
			get
			{
				return m_Place;
			}
			set
			{
				if (m_Place != value)
				{
					m_Place = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<CardDataModel> Minions
		{
			get
			{
				return m_Minions;
			}
			set
			{
				if (m_Minions != value)
				{
					RemoveNestedDataModel(m_Minions);
					RegisterNestedDataModel(value);
					m_Minions = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string HeroName
		{
			get
			{
				return m_HeroName;
			}
			set
			{
				if (!(m_HeroName == value))
				{
					m_HeroName = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public BaconPastGameStatsDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[5];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "hero",
				Type = typeof(CardDataModel)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "hero_power",
				Type = typeof(CardDataModel)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "place",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "minions",
				Type = typeof(DataModelList<CardDataModel>)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "hero_name",
				Type = typeof(string)
			};
			array[4] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Hero);
			RegisterNestedDataModel(m_HeroPower);
			RegisterNestedDataModel(m_Minions);
		}

		public int GetPropertiesHashCode()
		{
			int num = ((17 * 31 + ((m_Hero != null) ? m_Hero.GetPropertiesHashCode() : 0)) * 31 + ((m_HeroPower != null) ? m_HeroPower.GetPropertiesHashCode() : 0)) * 31;
			_ = m_Place;
			return ((num + m_Place.GetHashCode()) * 31 + ((m_Minions != null) ? m_Minions.GetPropertiesHashCode() : 0)) * 31 + ((m_HeroName != null) ? m_HeroName.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 1:
				value = m_Hero;
				return true;
			case 2:
				value = m_HeroPower;
				return true;
			case 3:
				value = m_Place;
				return true;
			case 4:
				value = m_Minions;
				return true;
			case 5:
				value = m_HeroName;
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
			case 1:
				Hero = ((value != null) ? ((CardDataModel)value) : null);
				return true;
			case 2:
				HeroPower = ((value != null) ? ((CardDataModel)value) : null);
				return true;
			case 3:
				Place = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				Minions = ((value != null) ? ((DataModelList<CardDataModel>)value) : null);
				return true;
			case 5:
				HeroName = ((value != null) ? ((string)value) : null);
				return true;
			default:
				return false;
			}
		}

		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 1:
				info = Properties[0];
				return true;
			case 2:
				info = Properties[1];
				return true;
			case 3:
				info = Properties[2];
				return true;
			case 4:
				info = Properties[3];
				return true;
			case 5:
				info = Properties[4];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
