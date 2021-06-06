using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010D8 RID: 4312
	public class RewardScrollDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BD14 RID: 48404 RVA: 0x0039AC74 File Offset: 0x00398E74
		public RewardScrollDataModel()
		{
			base.RegisterNestedDataModel(this.m_RewardList);
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x0600BD15 RID: 48405 RVA: 0x0039AD3E File Offset: 0x00398F3E
		public int DataModelId
		{
			get
			{
				return 257;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x0600BD16 RID: 48406 RVA: 0x0039AD45 File Offset: 0x00398F45
		public string DataModelDisplayName
		{
			get
			{
				return "reward_scroll";
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x0600BD18 RID: 48408 RVA: 0x0039AD77 File Offset: 0x00398F77
		// (set) Token: 0x0600BD17 RID: 48407 RVA: 0x0039AD4C File Offset: 0x00398F4C
		public string DisplayName
		{
			get
			{
				return this.m_DisplayName;
			}
			set
			{
				if (this.m_DisplayName == value)
				{
					return;
				}
				this.m_DisplayName = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x0600BD1A RID: 48410 RVA: 0x0039ADAA File Offset: 0x00398FAA
		// (set) Token: 0x0600BD19 RID: 48409 RVA: 0x0039AD7F File Offset: 0x00398F7F
		public string Description
		{
			get
			{
				return this.m_Description;
			}
			set
			{
				if (this.m_Description == value)
				{
					return;
				}
				this.m_Description = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x0600BD1C RID: 48412 RVA: 0x0039ADEB File Offset: 0x00398FEB
		// (set) Token: 0x0600BD1B RID: 48411 RVA: 0x0039ADB2 File Offset: 0x00398FB2
		public RewardListDataModel RewardList
		{
			get
			{
				return this.m_RewardList;
			}
			set
			{
				if (this.m_RewardList == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_RewardList);
				base.RegisterNestedDataModel(value);
				this.m_RewardList = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x0600BD1D RID: 48413 RVA: 0x0039ADF3 File Offset: 0x00398FF3
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BD1E RID: 48414 RVA: 0x0039ADFC File Offset: 0x00398FFC
		public int GetPropertiesHashCode()
		{
			return ((17 * 31 + ((this.m_DisplayName != null) ? this.m_DisplayName.GetHashCode() : 0)) * 31 + ((this.m_Description != null) ? this.m_Description.GetHashCode() : 0)) * 31 + ((this.m_RewardList != null) ? this.m_RewardList.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BD1F RID: 48415 RVA: 0x0039AE59 File Offset: 0x00399059
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_DisplayName;
				return true;
			case 1:
				value = this.m_Description;
				return true;
			case 2:
				value = this.m_RewardList;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BD20 RID: 48416 RVA: 0x0039AE94 File Offset: 0x00399094
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.DisplayName = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				this.Description = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				this.RewardList = ((value != null) ? ((RewardListDataModel)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BD21 RID: 48417 RVA: 0x0039AEF4 File Offset: 0x003990F4
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

		// Token: 0x04009A54 RID: 39508
		public const int ModelId = 257;

		// Token: 0x04009A55 RID: 39509
		private string m_DisplayName;

		// Token: 0x04009A56 RID: 39510
		private string m_Description;

		// Token: 0x04009A57 RID: 39511
		private RewardListDataModel m_RewardList;

		// Token: 0x04009A58 RID: 39512
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "display_name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "description",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "reward_list",
				Type = typeof(RewardListDataModel)
			}
		};
	}
}
