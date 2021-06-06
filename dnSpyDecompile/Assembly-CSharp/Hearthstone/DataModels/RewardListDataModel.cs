using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010D7 RID: 4311
	public class RewardListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BD06 RID: 48390 RVA: 0x0039A994 File Offset: 0x00398B94
		public RewardListDataModel()
		{
			base.RegisterNestedDataModel(this.m_Items);
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x0600BD07 RID: 48391 RVA: 0x0039AA6B File Offset: 0x00398C6B
		public int DataModelId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x0600BD08 RID: 48392 RVA: 0x0039AA6F File Offset: 0x00398C6F
		public string DataModelDisplayName
		{
			get
			{
				return "reward_list";
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x0600BD0A RID: 48394 RVA: 0x0039AA9C File Offset: 0x00398C9C
		// (set) Token: 0x0600BD09 RID: 48393 RVA: 0x0039AA76 File Offset: 0x00398C76
		public bool ChooseOne
		{
			get
			{
				return this.m_ChooseOne;
			}
			set
			{
				if (this.m_ChooseOne == value)
				{
					return;
				}
				this.m_ChooseOne = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x0600BD0C RID: 48396 RVA: 0x0039AADD File Offset: 0x00398CDD
		// (set) Token: 0x0600BD0B RID: 48395 RVA: 0x0039AAA4 File Offset: 0x00398CA4
		public DataModelList<RewardItemDataModel> Items
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

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x0600BD0E RID: 48398 RVA: 0x0039AB10 File Offset: 0x00398D10
		// (set) Token: 0x0600BD0D RID: 48397 RVA: 0x0039AAE5 File Offset: 0x00398CE5
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

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x0600BD0F RID: 48399 RVA: 0x0039AB18 File Offset: 0x00398D18
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BD10 RID: 48400 RVA: 0x0039AB20 File Offset: 0x00398D20
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			bool chooseOne = this.m_ChooseOne;
			return ((num + this.m_ChooseOne.GetHashCode()) * 31 + ((this.m_Items != null) ? this.m_Items.GetPropertiesHashCode() : 0)) * 31 + ((this.m_Description != null) ? this.m_Description.GetHashCode() : 0);
		}

		// Token: 0x0600BD11 RID: 48401 RVA: 0x0039AB79 File Offset: 0x00398D79
		public bool GetPropertyValue(int id, out object value)
		{
			if (id == 1)
			{
				value = this.m_ChooseOne;
				return true;
			}
			if (id == 15)
			{
				value = this.m_Description;
				return true;
			}
			if (id != 35)
			{
				value = null;
				return false;
			}
			value = this.m_Items;
			return true;
		}

		// Token: 0x0600BD12 RID: 48402 RVA: 0x0039ABB4 File Offset: 0x00398DB4
		public bool SetPropertyValue(int id, object value)
		{
			if (id == 1)
			{
				this.ChooseOne = (value != null && (bool)value);
				return true;
			}
			if (id == 15)
			{
				this.Description = ((value != null) ? ((string)value) : null);
				return true;
			}
			if (id != 35)
			{
				return false;
			}
			this.Items = ((value != null) ? ((DataModelList<RewardItemDataModel>)value) : null);
			return true;
		}

		// Token: 0x0600BD13 RID: 48403 RVA: 0x0039AC10 File Offset: 0x00398E10
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id == 1)
			{
				info = this.Properties[0];
				return true;
			}
			if (id == 15)
			{
				info = this.Properties[2];
				return true;
			}
			if (id != 35)
			{
				info = default(DataModelProperty);
				return false;
			}
			info = this.Properties[1];
			return true;
		}

		// Token: 0x04009A4F RID: 39503
		public const int ModelId = 34;

		// Token: 0x04009A50 RID: 39504
		private bool m_ChooseOne;

		// Token: 0x04009A51 RID: 39505
		private DataModelList<RewardItemDataModel> m_Items = new DataModelList<RewardItemDataModel>();

		// Token: 0x04009A52 RID: 39506
		private string m_Description;

		// Token: 0x04009A53 RID: 39507
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "choose_one",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 35,
				PropertyDisplayName = "items",
				Type = typeof(DataModelList<RewardItemDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "description",
				Type = typeof(string)
			}
		};
	}
}
