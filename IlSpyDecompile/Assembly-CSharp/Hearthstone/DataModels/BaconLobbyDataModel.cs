using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class BaconLobbyDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 43;

		private int m_Rating;

		private int m_FirstPlaceFinishes;

		private int m_Top4Finishes;

		private BoosterDbId m_PremiumPackType;

		private int m_PremiumPackOwnedCount;

		private bool m_BonusesLicenseOwned;

		private DataModelProperty[] m_properties;

		public int DataModelId => 43;

		public string DataModelDisplayName => "baconlobby";

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

		public int FirstPlaceFinishes
		{
			get
			{
				return m_FirstPlaceFinishes;
			}
			set
			{
				if (m_FirstPlaceFinishes != value)
				{
					m_FirstPlaceFinishes = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Top4Finishes
		{
			get
			{
				return m_Top4Finishes;
			}
			set
			{
				if (m_Top4Finishes != value)
				{
					m_Top4Finishes = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public BoosterDbId PremiumPackType
		{
			get
			{
				return m_PremiumPackType;
			}
			set
			{
				if (m_PremiumPackType != value)
				{
					m_PremiumPackType = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int PremiumPackOwnedCount
		{
			get
			{
				return m_PremiumPackOwnedCount;
			}
			set
			{
				if (m_PremiumPackOwnedCount != value)
				{
					m_PremiumPackOwnedCount = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool BonusesLicenseOwned
		{
			get
			{
				return m_BonusesLicenseOwned;
			}
			set
			{
				if (m_BonusesLicenseOwned != value)
				{
					m_BonusesLicenseOwned = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public BaconLobbyDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[6];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "rating",
				Type = typeof(int)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "first_place_finishes",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "top_4_finishes",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "premium_pack_type",
				Type = typeof(BoosterDbId)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "premium_pack_owned_count",
				Type = typeof(int)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "bonuses_license_owned",
				Type = typeof(bool)
			};
			array[5] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_Rating;
			int num2 = (num + m_Rating.GetHashCode()) * 31;
			_ = m_FirstPlaceFinishes;
			int num3 = (num2 + m_FirstPlaceFinishes.GetHashCode()) * 31;
			_ = m_Top4Finishes;
			int num4 = (num3 + m_Top4Finishes.GetHashCode()) * 31;
			_ = m_PremiumPackType;
			int num5 = (num4 + m_PremiumPackType.GetHashCode()) * 31;
			_ = m_PremiumPackOwnedCount;
			int num6 = (num5 + m_PremiumPackOwnedCount.GetHashCode()) * 31;
			_ = m_BonusesLicenseOwned;
			return num6 + m_BonusesLicenseOwned.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Rating;
				return true;
			case 1:
				value = m_FirstPlaceFinishes;
				return true;
			case 2:
				value = m_Top4Finishes;
				return true;
			case 3:
				value = m_PremiumPackType;
				return true;
			case 4:
				value = m_PremiumPackOwnedCount;
				return true;
			case 5:
				value = m_BonusesLicenseOwned;
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
				Rating = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				FirstPlaceFinishes = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				Top4Finishes = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				PremiumPackType = ((value != null) ? ((BoosterDbId)value) : BoosterDbId.INVALID);
				return true;
			case 4:
				PremiumPackOwnedCount = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				BonusesLicenseOwned = value != null && (bool)value;
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
