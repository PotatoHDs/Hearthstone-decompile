using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class DeviceDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 0;

		private bool m_Mobile;

		private OSCategory m_Category;

		private bool m_Notch;

		private ScreenCategory m_Screen;

		private AspectRatio m_AspectRatio;

		private DataModelProperty[] m_properties;

		public int DataModelId => 0;

		public string DataModelDisplayName => "device";

		public bool Mobile
		{
			get
			{
				return m_Mobile;
			}
			set
			{
				if (m_Mobile != value)
				{
					m_Mobile = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public OSCategory Category
		{
			get
			{
				return m_Category;
			}
			set
			{
				if (m_Category != value)
				{
					m_Category = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool Notch
		{
			get
			{
				return m_Notch;
			}
			set
			{
				if (m_Notch != value)
				{
					m_Notch = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public ScreenCategory Screen
		{
			get
			{
				return m_Screen;
			}
			set
			{
				if (m_Screen != value)
				{
					m_Screen = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public AspectRatio AspectRatio
		{
			get
			{
				return m_AspectRatio;
			}
			set
			{
				if (m_AspectRatio != value)
				{
					m_AspectRatio = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public DeviceDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[5];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "mobile",
				Type = typeof(bool)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "category",
				Type = typeof(OSCategory)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "notch",
				Type = typeof(bool)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "screen",
				Type = typeof(ScreenCategory)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "aspect_ratio",
				Type = typeof(AspectRatio)
			};
			array[4] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_Mobile;
			int num2 = (num + m_Mobile.GetHashCode()) * 31;
			_ = m_Category;
			int num3 = (num2 + m_Category.GetHashCode()) * 31;
			_ = m_Notch;
			int num4 = (num3 + m_Notch.GetHashCode()) * 31;
			_ = m_Screen;
			int num5 = (num4 + m_Screen.GetHashCode()) * 31;
			_ = m_AspectRatio;
			return num5 + m_AspectRatio.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Mobile;
				return true;
			case 1:
				value = m_Category;
				return true;
			case 2:
				value = m_Notch;
				return true;
			case 3:
				value = m_Screen;
				return true;
			case 4:
				value = m_AspectRatio;
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
				Mobile = value != null && (bool)value;
				return true;
			case 1:
				Category = ((value != null) ? ((OSCategory)value) : ((OSCategory)0));
				return true;
			case 2:
				Notch = value != null && (bool)value;
				return true;
			case 3:
				Screen = ((value != null) ? ((ScreenCategory)value) : ((ScreenCategory)0));
				return true;
			case 4:
				AspectRatio = ((value != null) ? ((AspectRatio)value) : AspectRatio.Unknown);
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
