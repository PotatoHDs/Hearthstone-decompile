using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010CD RID: 4301
	public class PrototypeNestedList1DataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BC22 RID: 48162 RVA: 0x00396A6C File Offset: 0x00394C6C
		public PrototypeNestedList1DataModel()
		{
			base.RegisterNestedDataModel(this.m_List);
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x0600BC23 RID: 48163 RVA: 0x00396B0C File Offset: 0x00394D0C
		public int DataModelId
		{
			get
			{
				return 294;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x0600BC24 RID: 48164 RVA: 0x00396B13 File Offset: 0x00394D13
		public string DataModelDisplayName
		{
			get
			{
				return "prototype_nested_list1";
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x0600BC26 RID: 48166 RVA: 0x00396B40 File Offset: 0x00394D40
		// (set) Token: 0x0600BC25 RID: 48165 RVA: 0x00396B1A File Offset: 0x00394D1A
		public int Int
		{
			get
			{
				return this.m_Int;
			}
			set
			{
				if (this.m_Int == value)
				{
					return;
				}
				this.m_Int = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x0600BC28 RID: 48168 RVA: 0x00396B81 File Offset: 0x00394D81
		// (set) Token: 0x0600BC27 RID: 48167 RVA: 0x00396B48 File Offset: 0x00394D48
		public DataModelList<PrototypeNestedList2DataModel> List
		{
			get
			{
				return this.m_List;
			}
			set
			{
				if (this.m_List == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_List);
				base.RegisterNestedDataModel(value);
				this.m_List = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x0600BC29 RID: 48169 RVA: 0x00396B89 File Offset: 0x00394D89
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BC2A RID: 48170 RVA: 0x00396B91 File Offset: 0x00394D91
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int @int = this.m_Int;
			return (num + this.m_Int.GetHashCode()) * 31 + ((this.m_List != null) ? this.m_List.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BC2B RID: 48171 RVA: 0x00396BC5 File Offset: 0x00394DC5
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 0)
			{
				value = this.m_Int;
				return true;
			}
			if (id != 1)
			{
				value = null;
				return false;
			}
			value = this.m_List;
			return true;
		}

		// Token: 0x0600BC2C RID: 48172 RVA: 0x00396BED File Offset: 0x00394DED
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 0)
			{
				this.Int = ((value != null) ? ((int)value) : 0);
				return true;
			}
			if (id != 1)
			{
				return false;
			}
			this.List = ((value != null) ? ((DataModelList<PrototypeNestedList2DataModel>)value) : null);
			return true;
		}

		// Token: 0x0600BC2D RID: 48173 RVA: 0x00396C21 File Offset: 0x00394E21
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

		// Token: 0x040099F1 RID: 39409
		public const int ModelId = 294;

		// Token: 0x040099F2 RID: 39410
		private int m_Int;

		// Token: 0x040099F3 RID: 39411
		private DataModelList<PrototypeNestedList2DataModel> m_List = new DataModelList<PrototypeNestedList2DataModel>();

		// Token: 0x040099F4 RID: 39412
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "int",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "list",
				Type = typeof(DataModelList<PrototypeNestedList2DataModel>)
			}
		};
	}
}
