using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010C9 RID: 4297
	public class ProfileClassIconListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BBE6 RID: 48102 RVA: 0x00395D90 File Offset: 0x00393F90
		public ProfileClassIconListDataModel()
		{
			base.RegisterNestedDataModel(this.m_Icons);
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x0600BBE7 RID: 48103 RVA: 0x00395DFB File Offset: 0x00393FFB
		public int DataModelId
		{
			get
			{
				return 233;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x0600BBE8 RID: 48104 RVA: 0x00395E02 File Offset: 0x00394002
		public string DataModelDisplayName
		{
			get
			{
				return "profile_class_icon_list";
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x0600BBEA RID: 48106 RVA: 0x00395E42 File Offset: 0x00394042
		// (set) Token: 0x0600BBE9 RID: 48105 RVA: 0x00395E09 File Offset: 0x00394009
		public DataModelList<ProfileClassIconDataModel> Icons
		{
			get
			{
				return this.m_Icons;
			}
			set
			{
				if (this.m_Icons == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Icons);
				base.RegisterNestedDataModel(value);
				this.m_Icons = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x0600BBEB RID: 48107 RVA: 0x00395E4A File Offset: 0x0039404A
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BBEC RID: 48108 RVA: 0x00395E52 File Offset: 0x00394052
		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((this.m_Icons != null) ? this.m_Icons.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BBED RID: 48109 RVA: 0x00395E70 File Offset: 0x00394070
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Icons;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BBEE RID: 48110 RVA: 0x00395E83 File Offset: 0x00394083
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Icons = ((value != null) ? ((DataModelList<ProfileClassIconDataModel>)value) : null);
				return true;
			}
			return false;
		}

		// Token: 0x0600BBEF RID: 48111 RVA: 0x00395E9D File Offset: 0x0039409D
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

		// Token: 0x040099DB RID: 39387
		public const int ModelId = 233;

		// Token: 0x040099DC RID: 39388
		private DataModelList<ProfileClassIconDataModel> m_Icons = new DataModelList<ProfileClassIconDataModel>();

		// Token: 0x040099DD RID: 39389
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "icons",
				Type = typeof(DataModelList<ProfileClassIconDataModel>)
			}
		};
	}
}
