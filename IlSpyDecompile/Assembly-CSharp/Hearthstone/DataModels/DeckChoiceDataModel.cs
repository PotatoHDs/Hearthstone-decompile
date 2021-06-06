using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class DeckChoiceDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 157;

		private int m_ChoiceClassID;

		private string m_ChoiceClassName;

		private string m_DeckDescription;

		private string m_ButtonClass;

		private DataModelProperty[] m_properties;

		public int DataModelId => 157;

		public string DataModelDisplayName => "deck_choice";

		public int ChoiceClassID
		{
			get
			{
				return m_ChoiceClassID;
			}
			set
			{
				if (m_ChoiceClassID != value)
				{
					m_ChoiceClassID = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string ChoiceClassName
		{
			get
			{
				return m_ChoiceClassName;
			}
			set
			{
				if (!(m_ChoiceClassName == value))
				{
					m_ChoiceClassName = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string DeckDescription
		{
			get
			{
				return m_DeckDescription;
			}
			set
			{
				if (!(m_DeckDescription == value))
				{
					m_DeckDescription = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string ButtonClass
		{
			get
			{
				return m_ButtonClass;
			}
			set
			{
				if (!(m_ButtonClass == value))
				{
					m_ButtonClass = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public DeckChoiceDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[4];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "choice_class_id",
				Type = typeof(int)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "choice_class_name",
				Type = typeof(string)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "deck_description",
				Type = typeof(string)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "button_class_name",
				Type = typeof(string)
			};
			array[3] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_ChoiceClassID;
			return (((num + m_ChoiceClassID.GetHashCode()) * 31 + ((m_ChoiceClassName != null) ? m_ChoiceClassName.GetHashCode() : 0)) * 31 + ((m_DeckDescription != null) ? m_DeckDescription.GetHashCode() : 0)) * 31 + ((m_ButtonClass != null) ? m_ButtonClass.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_ChoiceClassID;
				return true;
			case 1:
				value = m_ChoiceClassName;
				return true;
			case 2:
				value = m_DeckDescription;
				return true;
			case 3:
				value = m_ButtonClass;
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
				ChoiceClassID = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				ChoiceClassName = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				DeckDescription = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				ButtonClass = ((value != null) ? ((string)value) : null);
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
