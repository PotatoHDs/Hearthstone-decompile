using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class ShopBadgeDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 18;

		private ShopButtonDisplay.DisplayType m_BadgeType;

		private int m_AssetId;

		private int m_Quantity;

		private float m_Scale;

		private DataModelProperty[] m_properties;

		public int DataModelId => 18;

		public string DataModelDisplayName => "shop_badge";

		public ShopButtonDisplay.DisplayType BadgeType
		{
			get
			{
				return m_BadgeType;
			}
			set
			{
				if (m_BadgeType != value)
				{
					m_BadgeType = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int AssetId
		{
			get
			{
				return m_AssetId;
			}
			set
			{
				if (m_AssetId != value)
				{
					m_AssetId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Quantity
		{
			get
			{
				return m_Quantity;
			}
			set
			{
				if (m_Quantity != value)
				{
					m_Quantity = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public float Scale
		{
			get
			{
				return m_Scale;
			}
			set
			{
				if (m_Scale != value)
				{
					m_Scale = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public ShopBadgeDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[4];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "badge_type",
				Type = typeof(ShopButtonDisplay.DisplayType)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "asset_id",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "quantity",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "scale",
				Type = typeof(float)
			};
			array[3] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_BadgeType;
			int num2 = (num + m_BadgeType.GetHashCode()) * 31;
			_ = m_AssetId;
			int num3 = (num2 + m_AssetId.GetHashCode()) * 31;
			_ = m_Quantity;
			int num4 = (num3 + m_Quantity.GetHashCode()) * 31;
			_ = m_Scale;
			return num4 + m_Scale.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_BadgeType;
				return true;
			case 1:
				value = m_AssetId;
				return true;
			case 2:
				value = m_Quantity;
				return true;
			case 3:
				value = m_Scale;
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
				BadgeType = ((value != null) ? ((ShopButtonDisplay.DisplayType)value) : ShopButtonDisplay.DisplayType.BOOSTER);
				return true;
			case 1:
				AssetId = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				Quantity = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				Scale = ((value != null) ? ((float)value) : 0f);
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
