using System;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.DataModels
{
	// Token: 0x020010CC RID: 4300
	public class PrototypeDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BC0C RID: 48140 RVA: 0x0039646C File Offset: 0x0039466C
		public PrototypeDataModel()
		{
			base.RegisterNestedDataModel(this.m_List1);
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x0600BC0D RID: 48141 RVA: 0x000052EC File Offset: 0x000034EC
		public int DataModelId
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x0600BC0E RID: 48142 RVA: 0x00396615 File Offset: 0x00394815
		public string DataModelDisplayName
		{
			get
			{
				return "prototype";
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x0600BC10 RID: 48144 RVA: 0x00396642 File Offset: 0x00394842
		// (set) Token: 0x0600BC0F RID: 48143 RVA: 0x0039661C File Offset: 0x0039481C
		public bool Bool1
		{
			get
			{
				return this.m_Bool1;
			}
			set
			{
				if (this.m_Bool1 == value)
				{
					return;
				}
				this.m_Bool1 = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x0600BC12 RID: 48146 RVA: 0x00396670 File Offset: 0x00394870
		// (set) Token: 0x0600BC11 RID: 48145 RVA: 0x0039664A File Offset: 0x0039484A
		public int Int1
		{
			get
			{
				return this.m_Int1;
			}
			set
			{
				if (this.m_Int1 == value)
				{
					return;
				}
				this.m_Int1 = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x0600BC14 RID: 48148 RVA: 0x0039669E File Offset: 0x0039489E
		// (set) Token: 0x0600BC13 RID: 48147 RVA: 0x00396678 File Offset: 0x00394878
		public float Float1
		{
			get
			{
				return this.m_Float1;
			}
			set
			{
				if (this.m_Float1 == value)
				{
					return;
				}
				this.m_Float1 = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600BC16 RID: 48150 RVA: 0x003966D1 File Offset: 0x003948D1
		// (set) Token: 0x0600BC15 RID: 48149 RVA: 0x003966A6 File Offset: 0x003948A6
		public string String1
		{
			get
			{
				return this.m_String1;
			}
			set
			{
				if (this.m_String1 == value)
				{
					return;
				}
				this.m_String1 = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600BC18 RID: 48152 RVA: 0x00396704 File Offset: 0x00394904
		// (set) Token: 0x0600BC17 RID: 48151 RVA: 0x003966D9 File Offset: 0x003948D9
		public Texture Texture1
		{
			get
			{
				return this.m_Texture1;
			}
			set
			{
				if (this.m_Texture1 == value)
				{
					return;
				}
				this.m_Texture1 = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x0600BC1A RID: 48154 RVA: 0x00396737 File Offset: 0x00394937
		// (set) Token: 0x0600BC19 RID: 48153 RVA: 0x0039670C File Offset: 0x0039490C
		public Material Material1
		{
			get
			{
				return this.m_Material1;
			}
			set
			{
				if (this.m_Material1 == value)
				{
					return;
				}
				this.m_Material1 = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x0600BC1C RID: 48156 RVA: 0x00396778 File Offset: 0x00394978
		// (set) Token: 0x0600BC1B RID: 48155 RVA: 0x0039673F File Offset: 0x0039493F
		public DataModelList<PrototypeDataModel> List1
		{
			get
			{
				return this.m_List1;
			}
			set
			{
				if (this.m_List1 == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_List1);
				base.RegisterNestedDataModel(value);
				this.m_List1 = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x0600BC1D RID: 48157 RVA: 0x00396780 File Offset: 0x00394980
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BC1E RID: 48158 RVA: 0x00396788 File Offset: 0x00394988
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			bool @bool = this.m_Bool1;
			int num2 = (num + this.m_Bool1.GetHashCode()) * 31;
			int @int = this.m_Int1;
			int num3 = (num2 + this.m_Int1.GetHashCode()) * 31;
			float @float = this.m_Float1;
			return ((((num3 + this.m_Float1.GetHashCode()) * 31 + ((this.m_String1 != null) ? this.m_String1.GetHashCode() : 0)) * 31 + ((this.m_Texture1 != null) ? this.m_Texture1.GetHashCode() : 0)) * 31 + ((this.m_Material1 != null) ? this.m_Material1.GetHashCode() : 0)) * 31 + ((this.m_List1 != null) ? this.m_List1.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BC1F RID: 48159 RVA: 0x00396850 File Offset: 0x00394A50
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Bool1;
				return true;
			case 1:
				value = this.m_Int1;
				return true;
			case 2:
				value = this.m_Float1;
				return true;
			case 3:
				value = this.m_String1;
				return true;
			case 4:
				value = this.m_Texture1;
				return true;
			case 5:
				value = this.m_Material1;
				return true;
			case 6:
				value = this.m_List1;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BC20 RID: 48160 RVA: 0x003968DC File Offset: 0x00394ADC
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Bool1 = (value != null && (bool)value);
				return true;
			case 1:
				this.Int1 = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.Float1 = ((value != null) ? ((float)value) : 0f);
				return true;
			case 3:
				this.String1 = ((value != null) ? ((string)value) : null);
				return true;
			case 4:
				this.Texture1 = ((value != null) ? ((Texture)value) : null);
				return true;
			case 5:
				this.Material1 = ((value != null) ? ((Material)value) : null);
				return true;
			case 6:
				this.List1 = ((value != null) ? ((DataModelList<PrototypeDataModel>)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BC21 RID: 48161 RVA: 0x003969A4 File Offset: 0x00394BA4
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 0:
				info = this.Properties[0];
				return true;
			case 1:
				info = this.Properties[1];
				return true;
			case 2:
				info = this.Properties[2];
				return true;
			case 3:
				info = this.Properties[3];
				return true;
			case 4:
				info = this.Properties[4];
				return true;
			case 5:
				info = this.Properties[5];
				return true;
			case 6:
				info = this.Properties[6];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x040099E8 RID: 39400
		public const int ModelId = 1;

		// Token: 0x040099E9 RID: 39401
		private bool m_Bool1;

		// Token: 0x040099EA RID: 39402
		private int m_Int1;

		// Token: 0x040099EB RID: 39403
		private float m_Float1;

		// Token: 0x040099EC RID: 39404
		private string m_String1;

		// Token: 0x040099ED RID: 39405
		private Texture m_Texture1;

		// Token: 0x040099EE RID: 39406
		private Material m_Material1;

		// Token: 0x040099EF RID: 39407
		private DataModelList<PrototypeDataModel> m_List1 = new DataModelList<PrototypeDataModel>();

		// Token: 0x040099F0 RID: 39408
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "bool1",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "int1",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "float1",
				Type = typeof(float)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "string1",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "texture1",
				Type = typeof(Texture)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "material1",
				Type = typeof(Material)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "list1",
				Type = typeof(DataModelList<PrototypeDataModel>)
			}
		};
	}
}
