using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class PackListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 283;

		private DataModelList<PackDataModel> m_Packs = new DataModelList<PackDataModel>();

		private DataModelProperty[] m_properties = new DataModelProperty[1]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "packs",
				Type = typeof(DataModelList<PackDataModel>)
			}
		};

		public int DataModelId => 283;

		public string DataModelDisplayName => "pack_list";

		public DataModelList<PackDataModel> Packs
		{
			get
			{
				return m_Packs;
			}
			set
			{
				if (m_Packs != value)
				{
					RemoveNestedDataModel(m_Packs);
					RegisterNestedDataModel(value);
					m_Packs = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public PackListDataModel()
		{
			RegisterNestedDataModel(m_Packs);
		}

		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((m_Packs != null) ? m_Packs.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = m_Packs;
				return true;
			}
			value = null;
			return false;
		}

		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				Packs = ((value != null) ? ((DataModelList<PackDataModel>)value) : null);
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
