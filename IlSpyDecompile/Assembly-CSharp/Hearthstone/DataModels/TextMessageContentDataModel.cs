using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class TextMessageContentDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 300;

		private string m_Title;

		private string m_IconType;

		private string m_BodyText;

		private DataModelProperty[] m_properties;

		public int DataModelId => 300;

		public string DataModelDisplayName => "text_message_content";

		public string Title
		{
			get
			{
				return m_Title;
			}
			set
			{
				if (!(m_Title == value))
				{
					m_Title = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string IconType
		{
			get
			{
				return m_IconType;
			}
			set
			{
				if (!(m_IconType == value))
				{
					m_IconType = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string BodyText
		{
			get
			{
				return m_BodyText;
			}
			set
			{
				if (!(m_BodyText == value))
				{
					m_BodyText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public TextMessageContentDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[3];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "title",
				Type = typeof(string)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "icon_type",
				Type = typeof(string)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "body_text",
				Type = typeof(string)
			};
			array[2] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			return ((17 * 31 + ((m_Title != null) ? m_Title.GetHashCode() : 0)) * 31 + ((m_IconType != null) ? m_IconType.GetHashCode() : 0)) * 31 + ((m_BodyText != null) ? m_BodyText.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Title;
				return true;
			case 1:
				value = m_IconType;
				return true;
			case 2:
				value = m_BodyText;
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
				Title = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				IconType = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				BodyText = ((value != null) ? ((string)value) : null);
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
