using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010CF RID: 4303
	public class PrototypeNestedList3DataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BC3A RID: 48186 RVA: 0x00396E4C File Offset: 0x0039504C
		public PrototypeNestedList3DataModel()
		{
			base.RegisterNestedDataModel(this.m_List);
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600BC3B RID: 48187 RVA: 0x00396EEC File Offset: 0x003950EC
		public int DataModelId
		{
			get
			{
				return 296;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600BC3C RID: 48188 RVA: 0x00396EF3 File Offset: 0x003950F3
		public string DataModelDisplayName
		{
			get
			{
				return "prototype_nested_list3";
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x0600BC3E RID: 48190 RVA: 0x00396F20 File Offset: 0x00395120
		// (set) Token: 0x0600BC3D RID: 48189 RVA: 0x00396EFA File Offset: 0x003950FA
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

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x0600BC40 RID: 48192 RVA: 0x00396F61 File Offset: 0x00395161
		// (set) Token: 0x0600BC3F RID: 48191 RVA: 0x00396F28 File Offset: 0x00395128
		public DataModelList<PrototypeDataModel> List
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

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x0600BC41 RID: 48193 RVA: 0x00396F69 File Offset: 0x00395169
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BC42 RID: 48194 RVA: 0x00396F71 File Offset: 0x00395171
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int @int = this.m_Int;
			return (num + this.m_Int.GetHashCode()) * 31 + ((this.m_List != null) ? this.m_List.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BC43 RID: 48195 RVA: 0x00396FA5 File Offset: 0x003951A5
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

		// Token: 0x0600BC44 RID: 48196 RVA: 0x00396FCD File Offset: 0x003951CD
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
			this.List = ((value != null) ? ((DataModelList<PrototypeDataModel>)value) : null);
			return true;
		}

		// Token: 0x0600BC45 RID: 48197 RVA: 0x00397001 File Offset: 0x00395201
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

		// Token: 0x040099F9 RID: 39417
		public const int ModelId = 296;

		// Token: 0x040099FA RID: 39418
		private int m_Int;

		// Token: 0x040099FB RID: 39419
		private DataModelList<PrototypeDataModel> m_List = new DataModelList<PrototypeDataModel>();

		// Token: 0x040099FC RID: 39420
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
				Type = typeof(DataModelList<PrototypeDataModel>)
			}
		};
	}
}
