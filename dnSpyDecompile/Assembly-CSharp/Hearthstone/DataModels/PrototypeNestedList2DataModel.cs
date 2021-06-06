using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010CE RID: 4302
	public class PrototypeNestedList2DataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BC2E RID: 48174 RVA: 0x00396C5C File Offset: 0x00394E5C
		public PrototypeNestedList2DataModel()
		{
			base.RegisterNestedDataModel(this.m_List);
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x0600BC2F RID: 48175 RVA: 0x00396CFC File Offset: 0x00394EFC
		public int DataModelId
		{
			get
			{
				return 295;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600BC30 RID: 48176 RVA: 0x00396D03 File Offset: 0x00394F03
		public string DataModelDisplayName
		{
			get
			{
				return "prototype_nested_list2";
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x0600BC32 RID: 48178 RVA: 0x00396D30 File Offset: 0x00394F30
		// (set) Token: 0x0600BC31 RID: 48177 RVA: 0x00396D0A File Offset: 0x00394F0A
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

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x0600BC34 RID: 48180 RVA: 0x00396D71 File Offset: 0x00394F71
		// (set) Token: 0x0600BC33 RID: 48179 RVA: 0x00396D38 File Offset: 0x00394F38
		public DataModelList<PrototypeNestedList3DataModel> List
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

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x0600BC35 RID: 48181 RVA: 0x00396D79 File Offset: 0x00394F79
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BC36 RID: 48182 RVA: 0x00396D81 File Offset: 0x00394F81
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int @int = this.m_Int;
			return (num + this.m_Int.GetHashCode()) * 31 + ((this.m_List != null) ? this.m_List.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BC37 RID: 48183 RVA: 0x00396DB5 File Offset: 0x00394FB5
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

		// Token: 0x0600BC38 RID: 48184 RVA: 0x00396DDD File Offset: 0x00394FDD
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
			this.List = ((value != null) ? ((DataModelList<PrototypeNestedList3DataModel>)value) : null);
			return true;
		}

		// Token: 0x0600BC39 RID: 48185 RVA: 0x00396E11 File Offset: 0x00395011
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

		// Token: 0x040099F5 RID: 39413
		public const int ModelId = 295;

		// Token: 0x040099F6 RID: 39414
		private int m_Int;

		// Token: 0x040099F7 RID: 39415
		private DataModelList<PrototypeNestedList3DataModel> m_List = new DataModelList<PrototypeNestedList3DataModel>();

		// Token: 0x040099F8 RID: 39416
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
				Type = typeof(DataModelList<PrototypeNestedList3DataModel>)
			}
		};
	}
}
