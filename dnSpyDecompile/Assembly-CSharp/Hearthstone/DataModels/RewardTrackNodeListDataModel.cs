using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010DB RID: 4315
	public class RewardTrackNodeListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BD54 RID: 48468 RVA: 0x0039BD64 File Offset: 0x00399F64
		public RewardTrackNodeListDataModel()
		{
			base.RegisterNestedDataModel(this.m_Nodes);
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x0600BD55 RID: 48469 RVA: 0x0039BDCF File Offset: 0x00399FCF
		public int DataModelId
		{
			get
			{
				return 231;
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x0600BD56 RID: 48470 RVA: 0x0039BDD6 File Offset: 0x00399FD6
		public string DataModelDisplayName
		{
			get
			{
				return "reward_track_node_list";
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x0600BD58 RID: 48472 RVA: 0x0039BE16 File Offset: 0x0039A016
		// (set) Token: 0x0600BD57 RID: 48471 RVA: 0x0039BDDD File Offset: 0x00399FDD
		public DataModelList<RewardTrackNodeDataModel> Nodes
		{
			get
			{
				return this.m_Nodes;
			}
			set
			{
				if (this.m_Nodes == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Nodes);
				base.RegisterNestedDataModel(value);
				this.m_Nodes = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600BD59 RID: 48473 RVA: 0x0039BE1E File Offset: 0x0039A01E
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BD5A RID: 48474 RVA: 0x0039BE26 File Offset: 0x0039A026
		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((this.m_Nodes != null) ? this.m_Nodes.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BD5B RID: 48475 RVA: 0x0039BE44 File Offset: 0x0039A044
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Nodes;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BD5C RID: 48476 RVA: 0x0039BE57 File Offset: 0x0039A057
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Nodes = ((value != null) ? ((DataModelList<RewardTrackNodeDataModel>)value) : null);
				return true;
			}
			return false;
		}

		// Token: 0x0600BD5D RID: 48477 RVA: 0x0039BE71 File Offset: 0x0039A071
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 0)
			{
				info = this.Properties[0];
				return true;
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x04009A6E RID: 39534
		public const int ModelId = 231;

		// Token: 0x04009A6F RID: 39535
		private DataModelList<RewardTrackNodeDataModel> m_Nodes = new DataModelList<RewardTrackNodeDataModel>();

		// Token: 0x04009A70 RID: 39536
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "nodes",
				Type = typeof(DataModelList<RewardTrackNodeDataModel>)
			}
		};
	}
}
