using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010D5 RID: 4309
	public class RankedPlayListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BCDE RID: 48350 RVA: 0x00399EFC File Offset: 0x003980FC
		public RankedPlayListDataModel()
		{
			base.RegisterNestedDataModel(this.m_Items);
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x0600BCDF RID: 48351 RVA: 0x00399F9C File Offset: 0x0039819C
		public int DataModelId
		{
			get
			{
				return 168;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x0600BCE0 RID: 48352 RVA: 0x00399FA3 File Offset: 0x003981A3
		public string DataModelDisplayName
		{
			get
			{
				return "ranked_play_list";
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x0600BCE2 RID: 48354 RVA: 0x00399FE3 File Offset: 0x003981E3
		// (set) Token: 0x0600BCE1 RID: 48353 RVA: 0x00399FAA File Offset: 0x003981AA
		public DataModelList<RankedPlayDataModel> Items
		{
			get
			{
				return this.m_Items;
			}
			set
			{
				if (this.m_Items == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Items);
				base.RegisterNestedDataModel(value);
				this.m_Items = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x0600BCE4 RID: 48356 RVA: 0x0039A011 File Offset: 0x00398211
		// (set) Token: 0x0600BCE3 RID: 48355 RVA: 0x00399FEB File Offset: 0x003981EB
		public int TotalWins
		{
			get
			{
				return this.m_TotalWins;
			}
			set
			{
				if (this.m_TotalWins == value)
				{
					return;
				}
				this.m_TotalWins = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x0600BCE5 RID: 48357 RVA: 0x0039A019 File Offset: 0x00398219
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BCE6 RID: 48358 RVA: 0x0039A021 File Offset: 0x00398221
		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((this.m_Items != null) ? this.m_Items.GetPropertiesHashCode() : 0)) * 31;
			int totalWins = this.m_TotalWins;
			return num + this.m_TotalWins.GetHashCode();
		}

		// Token: 0x0600BCE7 RID: 48359 RVA: 0x0039A055 File Offset: 0x00398255
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Items;
				return true;
			}
			if (id != 1)
			{
				value = null;
				return false;
			}
			value = this.m_TotalWins;
			return true;
		}

		// Token: 0x0600BCE8 RID: 48360 RVA: 0x0039A07D File Offset: 0x0039827D
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Items = ((value != null) ? ((DataModelList<RankedPlayDataModel>)value) : null);
				return true;
			}
			if (id != 1)
			{
				return false;
			}
			this.TotalWins = ((value != null) ? ((int)value) : 0);
			return true;
		}

		// Token: 0x0600BCE9 RID: 48361 RVA: 0x0039A0B1 File Offset: 0x003982B1
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 0)
			{
				info = this.Properties[0];
				return true;
			}
			if (id != 1)
			{
				info = default(DataModelProperty);
				return false;
			}
			info = this.Properties[1];
			return true;
		}

		// Token: 0x04009A3F RID: 39487
		public const int ModelId = 168;

		// Token: 0x04009A40 RID: 39488
		private DataModelList<RankedPlayDataModel> m_Items = new DataModelList<RankedPlayDataModel>();

		// Token: 0x04009A41 RID: 39489
		private int m_TotalWins;

		// Token: 0x04009A42 RID: 39490
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "items",
				Type = typeof(DataModelList<RankedPlayDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "total_wins",
				Type = typeof(int)
			}
		};
	}
}
