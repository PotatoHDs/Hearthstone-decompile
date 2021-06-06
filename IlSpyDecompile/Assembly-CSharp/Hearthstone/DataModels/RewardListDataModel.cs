using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class RewardListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 34;

		private bool m_ChooseOne;

		private DataModelList<RewardItemDataModel> m_Items = new DataModelList<RewardItemDataModel>();

		private string m_Description;

		private DataModelProperty[] m_properties;

		public int DataModelId => 34;

		public string DataModelDisplayName => "reward_list";

		public bool ChooseOne
		{
			get
			{
				return m_ChooseOne;
			}
			set
			{
				if (m_ChooseOne != value)
				{
					m_ChooseOne = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<RewardItemDataModel> Items
		{
			get
			{
				return m_Items;
			}
			set
			{
				if (m_Items != value)
				{
					RemoveNestedDataModel(m_Items);
					RegisterNestedDataModel(value);
					m_Items = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string Description
		{
			get
			{
				return m_Description;
			}
			set
			{
				if (!(m_Description == value))
				{
					m_Description = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public RewardListDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[3];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "choose_one",
				Type = typeof(bool)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 35,
				PropertyDisplayName = "items",
				Type = typeof(DataModelList<RewardItemDataModel>)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "description",
				Type = typeof(string)
			};
			array[2] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Items);
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_ChooseOne;
			return ((num + m_ChooseOne.GetHashCode()) * 31 + ((m_Items != null) ? m_Items.GetPropertiesHashCode() : 0)) * 31 + ((m_Description != null) ? m_Description.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 1:
				value = m_ChooseOne;
				return true;
			case 35:
				value = m_Items;
				return true;
			case 15:
				value = m_Description;
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
				ChooseOne = value != null && (bool)value;
				return true;
			case 35:
				Items = ((value != null) ? ((DataModelList<RewardItemDataModel>)value) : null);
				return true;
			case 15:
				Description = ((value != null) ? ((string)value) : null);
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
			case 35:
				info = Properties[1];
				return true;
			case 15:
				info = Properties[2];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
