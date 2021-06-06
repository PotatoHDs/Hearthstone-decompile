using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010CB RID: 4299
	public class ProfileGameModeStatListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BC02 RID: 48130 RVA: 0x0039633C File Offset: 0x0039453C
		public ProfileGameModeStatListDataModel()
		{
			base.RegisterNestedDataModel(this.m_Items);
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x0600BC03 RID: 48131 RVA: 0x003963A7 File Offset: 0x003945A7
		public int DataModelId
		{
			get
			{
				return 221;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x0600BC04 RID: 48132 RVA: 0x003963AE File Offset: 0x003945AE
		public string DataModelDisplayName
		{
			get
			{
				return "profile_game_mode_stat_list";
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x0600BC06 RID: 48134 RVA: 0x003963EE File Offset: 0x003945EE
		// (set) Token: 0x0600BC05 RID: 48133 RVA: 0x003963B5 File Offset: 0x003945B5
		public DataModelList<ProfileGameModeStatDataModel> Items
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

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x0600BC07 RID: 48135 RVA: 0x003963F6 File Offset: 0x003945F6
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BC08 RID: 48136 RVA: 0x003963FE File Offset: 0x003945FE
		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((this.m_Items != null) ? this.m_Items.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BC09 RID: 48137 RVA: 0x0039641C File Offset: 0x0039461C
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Items;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BC0A RID: 48138 RVA: 0x0039642F File Offset: 0x0039462F
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Items = ((value != null) ? ((DataModelList<ProfileGameModeStatDataModel>)value) : null);
				return true;
			}
			return false;
		}

		// Token: 0x0600BC0B RID: 48139 RVA: 0x00396449 File Offset: 0x00394649
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

		// Token: 0x040099E5 RID: 39397
		public const int ModelId = 221;

		// Token: 0x040099E6 RID: 39398
		private DataModelList<ProfileGameModeStatDataModel> m_Items = new DataModelList<ProfileGameModeStatDataModel>();

		// Token: 0x040099E7 RID: 39399
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "items",
				Type = typeof(DataModelList<ProfileGameModeStatDataModel>)
			}
		};
	}
}
