using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class HeroClassIconsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 33;

		private DataModelList<TAG_CLASS> m_Classes = new DataModelList<TAG_CLASS>();

		private DataModelProperty[] m_properties = new DataModelProperty[1]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "classes",
				Type = typeof(DataModelList<TAG_CLASS>)
			}
		};

		public int DataModelId => 33;

		public string DataModelDisplayName => "hero_class_icons";

		public DataModelList<TAG_CLASS> Classes
		{
			get
			{
				return m_Classes;
			}
			set
			{
				if (m_Classes != value)
				{
					RemoveNestedDataModel(m_Classes);
					RegisterNestedDataModel(value);
					m_Classes = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public HeroClassIconsDataModel()
		{
			RegisterNestedDataModel(m_Classes);
		}

		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((m_Classes != null) ? m_Classes.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = m_Classes;
				return true;
			}
			value = null;
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				Classes = ((value != null) ? ((DataModelList<TAG_CLASS>)value) : null);
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
