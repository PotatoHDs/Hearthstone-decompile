using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010C2 RID: 4290
	public class PackListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BB56 RID: 47958 RVA: 0x00393778 File Offset: 0x00391978
		public PackListDataModel()
		{
			base.RegisterNestedDataModel(this.m_Packs);
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x0600BB57 RID: 47959 RVA: 0x003937E3 File Offset: 0x003919E3
		public int DataModelId
		{
			get
			{
				return 283;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x0600BB58 RID: 47960 RVA: 0x003937EA File Offset: 0x003919EA
		public string DataModelDisplayName
		{
			get
			{
				return "pack_list";
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x0600BB5A RID: 47962 RVA: 0x0039382A File Offset: 0x00391A2A
		// (set) Token: 0x0600BB59 RID: 47961 RVA: 0x003937F1 File Offset: 0x003919F1
		public DataModelList<PackDataModel> Packs
		{
			get
			{
				return this.m_Packs;
			}
			set
			{
				if (this.m_Packs == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Packs);
				base.RegisterNestedDataModel(value);
				this.m_Packs = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x0600BB5B RID: 47963 RVA: 0x00393832 File Offset: 0x00391A32
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BB5C RID: 47964 RVA: 0x0039383A File Offset: 0x00391A3A
		public int GetPropertiesHashCode()
		{
			return 17 * 31 + ((this.m_Packs != null) ? this.m_Packs.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BB5D RID: 47965 RVA: 0x00393858 File Offset: 0x00391A58
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Packs;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BB5E RID: 47966 RVA: 0x0039386B File Offset: 0x00391A6B
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Packs = ((value != null) ? ((DataModelList<PackDataModel>)value) : null);
				return true;
			}
			return false;
		}

		// Token: 0x0600BB5F RID: 47967 RVA: 0x00393885 File Offset: 0x00391A85
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

		// Token: 0x040099A1 RID: 39329
		public const int ModelId = 283;

		// Token: 0x040099A2 RID: 39330
		private DataModelList<PackDataModel> m_Packs = new DataModelList<PackDataModel>();

		// Token: 0x040099A3 RID: 39331
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "packs",
				Type = typeof(DataModelList<PackDataModel>)
			}
		};
	}
}
