using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010DA RID: 4314
	public class RewardTrackNodeDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BD44 RID: 48452 RVA: 0x0039B9A0 File Offset: 0x00399BA0
		public RewardTrackNodeDataModel()
		{
			base.RegisterNestedDataModel(this.m_FreeRewards);
			base.RegisterNestedDataModel(this.m_PremiumRewards);
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x0600BD45 RID: 48453 RVA: 0x0039BAAB File Offset: 0x00399CAB
		public int DataModelId
		{
			get
			{
				return 230;
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x0600BD46 RID: 48454 RVA: 0x0039BAB2 File Offset: 0x00399CB2
		public string DataModelDisplayName
		{
			get
			{
				return "reward_track_node";
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x0600BD48 RID: 48456 RVA: 0x0039BADF File Offset: 0x00399CDF
		// (set) Token: 0x0600BD47 RID: 48455 RVA: 0x0039BAB9 File Offset: 0x00399CB9
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

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x0600BD4A RID: 48458 RVA: 0x0039BB12 File Offset: 0x00399D12
		// (set) Token: 0x0600BD49 RID: 48457 RVA: 0x0039BAE7 File Offset: 0x00399CE7
		public string StyleName
		{
			get
			{
				return this.m_StyleName;
			}
			set
			{
				if (this.m_StyleName == value)
				{
					return;
				}
				this.m_StyleName = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x0600BD4C RID: 48460 RVA: 0x0039BB53 File Offset: 0x00399D53
		// (set) Token: 0x0600BD4B RID: 48459 RVA: 0x0039BB1A File Offset: 0x00399D1A
		public RewardTrackNodeRewardsDataModel FreeRewards
		{
			get
			{
				return this.m_FreeRewards;
			}
			set
			{
				if (this.m_FreeRewards == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_FreeRewards);
				base.RegisterNestedDataModel(value);
				this.m_FreeRewards = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x0600BD4E RID: 48462 RVA: 0x0039BB94 File Offset: 0x00399D94
		// (set) Token: 0x0600BD4D RID: 48461 RVA: 0x0039BB5B File Offset: 0x00399D5B
		public RewardTrackNodeRewardsDataModel PremiumRewards
		{
			get
			{
				return this.m_PremiumRewards;
			}
			set
			{
				if (this.m_PremiumRewards == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_PremiumRewards);
				base.RegisterNestedDataModel(value);
				this.m_PremiumRewards = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x0600BD4F RID: 48463 RVA: 0x0039BB9C File Offset: 0x00399D9C
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BD50 RID: 48464 RVA: 0x0039BBA4 File Offset: 0x00399DA4
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int level = this.m_Level;
			return (((num + this.m_Level.GetHashCode()) * 31 + ((this.m_StyleName != null) ? this.m_StyleName.GetHashCode() : 0)) * 31 + ((this.m_FreeRewards != null) ? this.m_FreeRewards.GetPropertiesHashCode() : 0)) * 31 + ((this.m_PremiumRewards != null) ? this.m_PremiumRewards.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600BD51 RID: 48465 RVA: 0x0039BC18 File Offset: 0x00399E18
		public bool GetPropertyValue(int id, out object value)
		{
			if (id <= 1)
			{
				if (id == 0)
				{
					value = this.m_Level;
					return true;
				}
				if (id == 1)
				{
					value = this.m_StyleName;
					return true;
				}
			}
			else
			{
				if (id == 6)
				{
					value = this.m_FreeRewards;
					return true;
				}
				if (id == 7)
				{
					value = this.m_PremiumRewards;
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x0600BD52 RID: 48466 RVA: 0x0039BC70 File Offset: 0x00399E70
		public bool SetPropertyValue(int id, object value)
		{
			if (id <= 1)
			{
				if (id == 0)
				{
					this.Level = ((value != null) ? ((int)value) : 0);
					return true;
				}
				if (id == 1)
				{
					this.StyleName = ((value != null) ? ((string)value) : null);
					return true;
				}
			}
			else
			{
				if (id == 6)
				{
					this.FreeRewards = ((value != null) ? ((RewardTrackNodeRewardsDataModel)value) : null);
					return true;
				}
				if (id == 7)
				{
					this.PremiumRewards = ((value != null) ? ((RewardTrackNodeRewardsDataModel)value) : null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600BD53 RID: 48467 RVA: 0x0039BCE8 File Offset: 0x00399EE8
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id <= 1)
			{
				if (id == 0)
				{
					info = this.Properties[0];
					return true;
				}
				if (id == 1)
				{
					info = this.Properties[1];
					return true;
				}
			}
			else
			{
				if (id == 6)
				{
					info = this.Properties[2];
					return true;
				}
				if (id == 7)
				{
					info = this.Properties[3];
					return true;
				}
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x04009A68 RID: 39528
		public const int ModelId = 230;

		// Token: 0x04009A69 RID: 39529
		private int m_Level;

		// Token: 0x04009A6A RID: 39530
		private string m_StyleName;

		// Token: 0x04009A6B RID: 39531
		private RewardTrackNodeRewardsDataModel m_FreeRewards;

		// Token: 0x04009A6C RID: 39532
		private RewardTrackNodeRewardsDataModel m_PremiumRewards;

		// Token: 0x04009A6D RID: 39533
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
				PropertyDisplayName = "style_name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "free_rewards",
				Type = typeof(RewardTrackNodeRewardsDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "premium_rewards",
				Type = typeof(RewardTrackNodeRewardsDataModel)
			}
		};
	}
}
