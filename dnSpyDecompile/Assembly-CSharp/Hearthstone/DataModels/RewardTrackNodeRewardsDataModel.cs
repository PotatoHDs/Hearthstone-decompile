using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010DC RID: 4316
	public class RewardTrackNodeRewardsDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BD5E RID: 48478 RVA: 0x0039BE94 File Offset: 0x0039A094
		public RewardTrackNodeRewardsDataModel()
		{
			base.RegisterNestedDataModel(this.m_Items);
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x0600BD5F RID: 48479 RVA: 0x0039BFC8 File Offset: 0x0039A1C8
		public int DataModelId
		{
			get
			{
				return 236;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x0600BD60 RID: 48480 RVA: 0x0039BFCF File Offset: 0x0039A1CF
		public string DataModelDisplayName
		{
			get
			{
				return "reward_track_node_rewards";
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x0600BD62 RID: 48482 RVA: 0x0039BFFC File Offset: 0x0039A1FC
		// (set) Token: 0x0600BD61 RID: 48481 RVA: 0x0039BFD6 File Offset: 0x0039A1D6
		public int Level
		{
			get
			{
				return this.m_Level;
			}
			set
			{
				if (this.m_Level == value)
				{
					return;
				}
				this.m_Level = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x0600BD64 RID: 48484 RVA: 0x0039C02F File Offset: 0x0039A22F
		// (set) Token: 0x0600BD63 RID: 48483 RVA: 0x0039C004 File Offset: 0x0039A204
		public string Summary
		{
			get
			{
				return this.m_Summary;
			}
			set
			{
				if (this.m_Summary == value)
				{
					return;
				}
				this.m_Summary = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x0600BD66 RID: 48486 RVA: 0x0039C05D File Offset: 0x0039A25D
		// (set) Token: 0x0600BD65 RID: 48485 RVA: 0x0039C037 File Offset: 0x0039A237
		public bool IsPremium
		{
			get
			{
				return this.m_IsPremium;
			}
			set
			{
				if (this.m_IsPremium == value)
				{
					return;
				}
				this.m_IsPremium = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x0600BD68 RID: 48488 RVA: 0x0039C08B File Offset: 0x0039A28B
		// (set) Token: 0x0600BD67 RID: 48487 RVA: 0x0039C065 File Offset: 0x0039A265
		public bool IsClaimed
		{
			get
			{
				return this.m_IsClaimed;
			}
			set
			{
				if (this.m_IsClaimed == value)
				{
					return;
				}
				this.m_IsClaimed = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x0600BD6A RID: 48490 RVA: 0x0039C0CC File Offset: 0x0039A2CC
		// (set) Token: 0x0600BD69 RID: 48489 RVA: 0x0039C093 File Offset: 0x0039A293
		public RewardListDataModel Items
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

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x0600BD6B RID: 48491 RVA: 0x0039C0D4 File Offset: 0x0039A2D4
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BD6C RID: 48492 RVA: 0x0039C0DC File Offset: 0x0039A2DC
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int level = this.m_Level;
			int num2 = ((num + this.m_Level.GetHashCode()) * 31 + ((this.m_Summary != null) ? this.m_Summary.GetHashCode() : 0)) * 31;
			bool isPremium = this.m_IsPremium;
			int num3 = (num2 + this.m_IsPremium.GetHashCode()) * 31;
			bool isClaimed = this.m_IsClaimed;
			return (num3 + this.m_IsClaimed.GetHashCode()) * 31 + ((this.m_Items != null) ? this.m_Items.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BD6D RID: 48493 RVA: 0x0039C164 File Offset: 0x0039A364
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Level;
				return true;
			case 1:
				value = this.m_Summary;
				return true;
			case 2:
				value = this.m_IsPremium;
				return true;
			case 3:
				value = this.m_IsClaimed;
				return true;
			case 4:
				value = this.m_Items;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BD6E RID: 48494 RVA: 0x0039C1D4 File Offset: 0x0039A3D4
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Level = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				this.Summary = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				this.IsPremium = (value != null && (bool)value);
				return true;
			case 3:
				this.IsClaimed = (value != null && (bool)value);
				return true;
			case 4:
				this.Items = ((value != null) ? ((RewardListDataModel)value) : null);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BD6F RID: 48495 RVA: 0x0039C264 File Offset: 0x0039A464
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x04009A71 RID: 39537
		public const int ModelId = 236;

		// Token: 0x04009A72 RID: 39538
		private int m_Level;

		// Token: 0x04009A73 RID: 39539
		private string m_Summary;

		// Token: 0x04009A74 RID: 39540
		private bool m_IsPremium;

		// Token: 0x04009A75 RID: 39541
		private bool m_IsClaimed;

		// Token: 0x04009A76 RID: 39542
		private RewardListDataModel m_Items;

		// Token: 0x04009A77 RID: 39543
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "level",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "summary",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "is_premium",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "is_claimed",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "items",
				Type = typeof(RewardListDataModel)
			}
		};
	}
}
