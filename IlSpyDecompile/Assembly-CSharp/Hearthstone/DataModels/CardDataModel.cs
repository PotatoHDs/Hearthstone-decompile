using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class CardDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 27;

		private string m_CardId;

		private TAG_PREMIUM m_Premium;

		private string m_FlavorText;

		private int m_Attack;

		private int m_Health;

		private int m_Mana;

		private DataModelList<SpellType> m_SpellTypes = new DataModelList<SpellType>();

		private string m_Name;

		private bool m_Owned;

		private DataModelProperty[] m_properties;

		public int DataModelId => 27;

		public string DataModelDisplayName => "card";

		public string CardId
		{
			get
			{
				return m_CardId;
			}
			set
			{
				if (!(m_CardId == value))
				{
					m_CardId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public TAG_PREMIUM Premium
		{
			get
			{
				return m_Premium;
			}
			set
			{
				if (m_Premium != value)
				{
					m_Premium = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string FlavorText
		{
			get
			{
				return m_FlavorText;
			}
			set
			{
				if (!(m_FlavorText == value))
				{
					m_FlavorText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Attack
		{
			get
			{
				return m_Attack;
			}
			set
			{
				if (m_Attack != value)
				{
					m_Attack = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Health
		{
			get
			{
				return m_Health;
			}
			set
			{
				if (m_Health != value)
				{
					m_Health = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Mana
		{
			get
			{
				return m_Mana;
			}
			set
			{
				if (m_Mana != value)
				{
					m_Mana = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<SpellType> SpellTypes
		{
			get
			{
				return m_SpellTypes;
			}
			set
			{
				if (m_SpellTypes != value)
				{
					RemoveNestedDataModel(m_SpellTypes);
					RegisterNestedDataModel(value);
					m_SpellTypes = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
				if (!(m_Name == value))
				{
					m_Name = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool Owned
		{
			get
			{
				return m_Owned;
			}
			set
			{
				if (m_Owned != value)
				{
					m_Owned = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public CardDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[9];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "id",
				Type = typeof(string)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "premium",
				Type = typeof(TAG_PREMIUM)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "flavor_text",
				Type = typeof(string)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "attack",
				Type = typeof(int)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "health",
				Type = typeof(int)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "mana",
				Type = typeof(int)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "spell_types",
				Type = typeof(DataModelList<SpellType>)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "name",
				Type = typeof(string)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "owned",
				Type = typeof(bool)
			};
			array[8] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_SpellTypes);
		}

		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((m_CardId != null) ? m_CardId.GetHashCode() : 0)) * 31;
			_ = m_Premium;
			int num2 = ((num + m_Premium.GetHashCode()) * 31 + ((m_FlavorText != null) ? m_FlavorText.GetHashCode() : 0)) * 31;
			_ = m_Attack;
			int num3 = (num2 + m_Attack.GetHashCode()) * 31;
			_ = m_Health;
			int num4 = (num3 + m_Health.GetHashCode()) * 31;
			_ = m_Mana;
			int num5 = (((num4 + m_Mana.GetHashCode()) * 31 + ((m_SpellTypes != null) ? m_SpellTypes.GetPropertiesHashCode() : 0)) * 31 + ((m_Name != null) ? m_Name.GetHashCode() : 0)) * 31;
			_ = m_Owned;
			return num5 + m_Owned.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_CardId;
				return true;
			case 1:
				value = m_Premium;
				return true;
			case 2:
				value = m_FlavorText;
				return true;
			case 3:
				value = m_Attack;
				return true;
			case 4:
				value = m_Health;
				return true;
			case 5:
				value = m_Mana;
				return true;
			case 6:
				value = m_SpellTypes;
				return true;
			case 7:
				value = m_Name;
				return true;
			case 8:
				value = m_Owned;
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
				CardId = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				Premium = ((value != null) ? ((TAG_PREMIUM)value) : TAG_PREMIUM.NORMAL);
				return true;
			case 2:
				FlavorText = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				Attack = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				Health = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				Mana = ((value != null) ? ((int)value) : 0);
				return true;
			case 6:
				SpellTypes = ((value != null) ? ((DataModelList<SpellType>)value) : null);
				return true;
			case 7:
				Name = ((value != null) ? ((string)value) : null);
				return true;
			case 8:
				Owned = value != null && (bool)value;
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
			case 6:
				info = Properties[6];
				return true;
			case 7:
				info = Properties[7];
				return true;
			case 8:
				info = Properties[8];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
