using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class HeroPickerDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 13;

		private bool m_HasGuestHeroes;

		private DataModelProperty[] m_properties = new DataModelProperty[1]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "has_guest_heroes",
				Type = typeof(bool)
			}
		};

		public int DataModelId => 13;

		public string DataModelDisplayName => "hero_picker";

		public bool HasGuestHeroes
		{
			get
			{
				return m_HasGuestHeroes;
			}
			set
			{
				if (m_HasGuestHeroes != value)
				{
					m_HasGuestHeroes = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_HasGuestHeroes;
			return num + m_HasGuestHeroes.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = m_HasGuestHeroes;
				return true;
			}
			value = null;
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				HasGuestHeroes = value != null && (bool)value;
				return true;
			}
			return false;
		}

		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 0)
			{
				info = Properties[0];
				return true;
			}
			info = default(DataModelProperty);
			return false;
		}
	}
}
