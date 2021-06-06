using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010C1 RID: 4289
	public class PackDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x0600BB49 RID: 47945 RVA: 0x00393562 File Offset: 0x00391762
		public int DataModelId
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x0600BB4A RID: 47946 RVA: 0x00393566 File Offset: 0x00391766
		public string DataModelDisplayName
		{
			get
			{
				return "pack";
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x0600BB4C RID: 47948 RVA: 0x00393593 File Offset: 0x00391793
		// (set) Token: 0x0600BB4B RID: 47947 RVA: 0x0039356D File Offset: 0x0039176D
		public BoosterDbId Type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				if (this.m_Type == value)
				{
					return;
				}
				this.m_Type = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x0600BB4E RID: 47950 RVA: 0x003935C1 File Offset: 0x003917C1
		// (set) Token: 0x0600BB4D RID: 47949 RVA: 0x0039359B File Offset: 0x0039179B
		public int Quantity
		{
			get
			{
				return this.m_Quantity;
			}
			set
			{
				if (this.m_Quantity == value)
				{
					return;
				}
				this.m_Quantity = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x0600BB50 RID: 47952 RVA: 0x003935F4 File Offset: 0x003917F4
		// (set) Token: 0x0600BB4F RID: 47951 RVA: 0x003935C9 File Offset: 0x003917C9
		public string BoosterName
		{
			get
			{
				return this.m_BoosterName;
			}
			set
			{
				if (this.m_BoosterName == value)
				{
					return;
				}
				this.m_BoosterName = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x0600BB51 RID: 47953 RVA: 0x003935FC File Offset: 0x003917FC
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BB52 RID: 47954 RVA: 0x00393604 File Offset: 0x00391804
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			BoosterDbId type = this.m_Type;
			int num2 = (num + this.m_Type.GetHashCode()) * 31;
			int quantity = this.m_Quantity;
			return (num2 + this.m_Quantity.GetHashCode()) * 31 + ((this.m_BoosterName != null) ? this.m_BoosterName.GetHashCode() : 0);
		}

		// Token: 0x0600BB53 RID: 47955 RVA: 0x00393660 File Offset: 0x00391860
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Type;
				return true;
			case 1:
				value = this.m_Quantity;
				return true;
			case 2:
				value = this.m_BoosterName;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BB54 RID: 47956 RVA: 0x003936B0 File Offset: 0x003918B0
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Type = ((value != null) ? ((BoosterDbId)value) : BoosterDbId.INVALID);
				return true;
			case 1:
				this.Quantity = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.BoosterName = ((value != null) ? ((string)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BB55 RID: 47957 RVA: 0x00393710 File Offset: 0x00391910
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x0400999C RID: 39324
		public const int ModelId = 25;

		// Token: 0x0400999D RID: 39325
		private BoosterDbId m_Type;

		// Token: 0x0400999E RID: 39326
		private int m_Quantity;

		// Token: 0x0400999F RID: 39327
		private string m_BoosterName;

		// Token: 0x040099A0 RID: 39328
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "type",
				Type = typeof(BoosterDbId)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "quantity",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "booster_name",
				Type = typeof(string)
			}
		};
	}
}
