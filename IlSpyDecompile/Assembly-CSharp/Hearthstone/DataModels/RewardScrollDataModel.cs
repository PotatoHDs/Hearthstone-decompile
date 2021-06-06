using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class RewardScrollDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 257;

		private string m_DisplayName;

		private string m_Description;

		private RewardListDataModel m_RewardList;

		private DataModelProperty[] m_properties;

		public int DataModelId => 257;

		public string DataModelDisplayName => "reward_scroll";

		public string DisplayName
		{
			get
			{
				return m_DisplayName;
			}
			set
			{
				if (!(m_DisplayName == value))
				{
					m_DisplayName = value;
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

		public RewardListDataModel RewardList
		{
			get
			{
				return m_RewardList;
			}
			set
			{
				if (m_RewardList != value)
				{
					RemoveNestedDataModel(m_RewardList);
					RegisterNestedDataModel(value);
					m_RewardList = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public RewardScrollDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[3];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "display_name",
				Type = typeof(string)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "description",
				Type = typeof(string)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "reward_list",
				Type = typeof(RewardListDataModel)
			};
			array[2] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_RewardList);
		}

		public int GetPropertiesHashCode()
		{
			return ((17 * 31 + ((m_DisplayName != null) ? m_DisplayName.GetHashCode() : 0)) * 31 + ((m_Description != null) ? m_Description.GetHashCode() : 0)) * 31 + ((m_RewardList != null) ? m_RewardList.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_DisplayName;
				return true;
			case 1:
				value = m_Description;
				return true;
			case 2:
				value = m_RewardList;
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
				DisplayName = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				Description = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				RewardList = ((value != null) ? ((RewardListDataModel)value) : null);
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
