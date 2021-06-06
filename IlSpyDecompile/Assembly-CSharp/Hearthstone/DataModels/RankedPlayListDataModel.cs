using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class RankedPlayListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 168;

		private DataModelList<RankedPlayDataModel> m_Items = new DataModelList<RankedPlayDataModel>();

		private int m_TotalWins;

		private DataModelProperty[] m_properties;

		public int DataModelId => 168;

		public string DataModelDisplayName => "ranked_play_list";

		public DataModelList<RankedPlayDataModel> Items
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

		public int TotalWins
		{
			get
			{
				return m_TotalWins;
			}
			set
			{
				if (m_TotalWins != value)
				{
					m_TotalWins = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public RankedPlayListDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[2];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "items",
				Type = typeof(DataModelList<RankedPlayDataModel>)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "total_wins",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Items);
		}

		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((m_Items != null) ? m_Items.GetPropertiesHashCode() : 0)) * 31;
			_ = m_TotalWins;
			return num + m_TotalWins.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Items;
				return true;
			case 1:
				value = m_TotalWins;
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
				Items = ((value != null) ? ((DataModelList<RankedPlayDataModel>)value) : null);
				return true;
			case 1:
				TotalWins = ((value != null) ? ((int)value) : 0);
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
