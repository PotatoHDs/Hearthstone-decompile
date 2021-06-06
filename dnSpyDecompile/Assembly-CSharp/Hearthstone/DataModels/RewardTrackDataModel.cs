using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010D9 RID: 4313
	public class RewardTrackDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x0600BD23 RID: 48419 RVA: 0x0039B235 File Offset: 0x00399435
		public int DataModelId
		{
			get
			{
				return 229;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x0600BD24 RID: 48420 RVA: 0x0039B23C File Offset: 0x0039943C
		public string DataModelDisplayName
		{
			get
			{
				return "reward_track";
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x0600BD26 RID: 48422 RVA: 0x0039B269 File Offset: 0x00399469
		// (set) Token: 0x0600BD25 RID: 48421 RVA: 0x0039B243 File Offset: 0x00399443
		public int RewardTrackId
		{
			get
			{
				return this.m_RewardTrackId;
			}
			set
			{
				if (this.m_RewardTrackId == value)
				{
					return;
				}
				this.m_RewardTrackId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x0600BD28 RID: 48424 RVA: 0x0039B297 File Offset: 0x00399497
		// (set) Token: 0x0600BD27 RID: 48423 RVA: 0x0039B271 File Offset: 0x00399471
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

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x0600BD2A RID: 48426 RVA: 0x0039B2C5 File Offset: 0x003994C5
		// (set) Token: 0x0600BD29 RID: 48425 RVA: 0x0039B29F File Offset: 0x0039949F
		public int Xp
		{
			get
			{
				return this.m_Xp;
			}
			set
			{
				if (this.m_Xp == value)
				{
					return;
				}
				this.m_Xp = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600BD2C RID: 48428 RVA: 0x0039B2F3 File Offset: 0x003994F3
		// (set) Token: 0x0600BD2B RID: 48427 RVA: 0x0039B2CD File Offset: 0x003994CD
		public int XpNeeded
		{
			get
			{
				return this.m_XpNeeded;
			}
			set
			{
				if (this.m_XpNeeded == value)
				{
					return;
				}
				this.m_XpNeeded = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600BD2E RID: 48430 RVA: 0x0039B321 File Offset: 0x00399521
		// (set) Token: 0x0600BD2D RID: 48429 RVA: 0x0039B2FB File Offset: 0x003994FB
		public int XpBonusPercent
		{
			get
			{
				return this.m_XpBonusPercent;
			}
			set
			{
				if (this.m_XpBonusPercent == value)
				{
					return;
				}
				this.m_XpBonusPercent = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x0600BD30 RID: 48432 RVA: 0x0039B34F File Offset: 0x0039954F
		// (set) Token: 0x0600BD2F RID: 48431 RVA: 0x0039B329 File Offset: 0x00399529
		public bool PremiumRewardsUnlocked
		{
			get
			{
				return this.m_PremiumRewardsUnlocked;
			}
			set
			{
				if (this.m_PremiumRewardsUnlocked == value)
				{
					return;
				}
				this.m_PremiumRewardsUnlocked = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x0600BD32 RID: 48434 RVA: 0x0039B382 File Offset: 0x00399582
		// (set) Token: 0x0600BD31 RID: 48433 RVA: 0x0039B357 File Offset: 0x00399557
		public string XpProgress
		{
			get
			{
				return this.m_XpProgress;
			}
			set
			{
				if (this.m_XpProgress == value)
				{
					return;
				}
				this.m_XpProgress = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x0600BD34 RID: 48436 RVA: 0x0039B3B0 File Offset: 0x003995B0
		// (set) Token: 0x0600BD33 RID: 48435 RVA: 0x0039B38A File Offset: 0x0039958A
		public int Unclaimed
		{
			get
			{
				return this.m_Unclaimed;
			}
			set
			{
				if (this.m_Unclaimed == value)
				{
					return;
				}
				this.m_Unclaimed = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x0600BD36 RID: 48438 RVA: 0x0039B3DE File Offset: 0x003995DE
		// (set) Token: 0x0600BD35 RID: 48437 RVA: 0x0039B3B8 File Offset: 0x003995B8
		public int LevelSoftCap
		{
			get
			{
				return this.m_LevelSoftCap;
			}
			set
			{
				if (this.m_LevelSoftCap == value)
				{
					return;
				}
				this.m_LevelSoftCap = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x0600BD38 RID: 48440 RVA: 0x0039B411 File Offset: 0x00399611
		// (set) Token: 0x0600BD37 RID: 48439 RVA: 0x0039B3E6 File Offset: 0x003995E6
		public string XpBoostText
		{
			get
			{
				return this.m_XpBoostText;
			}
			set
			{
				if (this.m_XpBoostText == value)
				{
					return;
				}
				this.m_XpBoostText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x0600BD3A RID: 48442 RVA: 0x0039B43F File Offset: 0x0039963F
		// (set) Token: 0x0600BD39 RID: 48441 RVA: 0x0039B419 File Offset: 0x00399619
		public int LevelHardCap
		{
			get
			{
				return this.m_LevelHardCap;
			}
			set
			{
				if (this.m_LevelHardCap == value)
				{
					return;
				}
				this.m_LevelHardCap = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x0600BD3C RID: 48444 RVA: 0x0039B46D File Offset: 0x0039966D
		// (set) Token: 0x0600BD3B RID: 48443 RVA: 0x0039B447 File Offset: 0x00399647
		public int Season
		{
			get
			{
				return this.m_Season;
			}
			set
			{
				if (this.m_Season == value)
				{
					return;
				}
				this.m_Season = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x0600BD3E RID: 48446 RVA: 0x0039B49B File Offset: 0x0039969B
		// (set) Token: 0x0600BD3D RID: 48445 RVA: 0x0039B475 File Offset: 0x00399675
		public int SeasonLastSeen
		{
			get
			{
				return this.m_SeasonLastSeen;
			}
			set
			{
				if (this.m_SeasonLastSeen == value)
				{
					return;
				}
				this.m_SeasonLastSeen = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x0600BD3F RID: 48447 RVA: 0x0039B4A3 File Offset: 0x003996A3
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BD40 RID: 48448 RVA: 0x0039B4AC File Offset: 0x003996AC
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int rewardTrackId = this.m_RewardTrackId;
			int num2 = (num + this.m_RewardTrackId.GetHashCode()) * 31;
			int level = this.m_Level;
			int num3 = (num2 + this.m_Level.GetHashCode()) * 31;
			int xp = this.m_Xp;
			int num4 = (num3 + this.m_Xp.GetHashCode()) * 31;
			int xpNeeded = this.m_XpNeeded;
			int num5 = (num4 + this.m_XpNeeded.GetHashCode()) * 31;
			int xpBonusPercent = this.m_XpBonusPercent;
			int num6 = (num5 + this.m_XpBonusPercent.GetHashCode()) * 31;
			bool premiumRewardsUnlocked = this.m_PremiumRewardsUnlocked;
			int num7 = ((num6 + this.m_PremiumRewardsUnlocked.GetHashCode()) * 31 + ((this.m_XpProgress != null) ? this.m_XpProgress.GetHashCode() : 0)) * 31;
			int unclaimed = this.m_Unclaimed;
			int num8 = (num7 + this.m_Unclaimed.GetHashCode()) * 31;
			int levelSoftCap = this.m_LevelSoftCap;
			int num9 = ((num8 + this.m_LevelSoftCap.GetHashCode()) * 31 + ((this.m_XpBoostText != null) ? this.m_XpBoostText.GetHashCode() : 0)) * 31;
			int levelHardCap = this.m_LevelHardCap;
			int num10 = (num9 + this.m_LevelHardCap.GetHashCode()) * 31;
			int season = this.m_Season;
			int num11 = (num10 + this.m_Season.GetHashCode()) * 31;
			int seasonLastSeen = this.m_SeasonLastSeen;
			return num11 + this.m_SeasonLastSeen.GetHashCode();
		}

		// Token: 0x0600BD41 RID: 48449 RVA: 0x0039B5E4 File Offset: 0x003997E4
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_RewardTrackId;
				return true;
			case 1:
				value = this.m_Level;
				return true;
			case 2:
				value = this.m_Xp;
				return true;
			case 3:
				value = this.m_XpNeeded;
				return true;
			case 4:
				value = this.m_XpBonusPercent;
				return true;
			case 5:
				value = this.m_PremiumRewardsUnlocked;
				return true;
			case 6:
				value = this.m_XpProgress;
				return true;
			case 7:
				value = this.m_Unclaimed;
				return true;
			case 8:
				value = this.m_LevelSoftCap;
				return true;
			case 9:
				value = this.m_XpBoostText;
				return true;
			case 10:
				value = this.m_LevelHardCap;
				return true;
			case 11:
				value = this.m_Season;
				return true;
			case 12:
				value = this.m_SeasonLastSeen;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x0600BD42 RID: 48450 RVA: 0x0039B6F0 File Offset: 0x003998F0
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.RewardTrackId = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				this.Level = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.Xp = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				this.XpNeeded = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				this.XpBonusPercent = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				this.PremiumRewardsUnlocked = (value != null && (bool)value);
				return true;
			case 6:
				this.XpProgress = ((value != null) ? ((string)value) : null);
				return true;
			case 7:
				this.Unclaimed = ((value != null) ? ((int)value) : 0);
				return true;
			case 8:
				this.LevelSoftCap = ((value != null) ? ((int)value) : 0);
				return true;
			case 9:
				this.XpBoostText = ((value != null) ? ((string)value) : null);
				return true;
			case 10:
				this.LevelHardCap = ((value != null) ? ((int)value) : 0);
				return true;
			case 11:
				this.Season = ((value != null) ? ((int)value) : 0);
				return true;
			case 12:
				this.SeasonLastSeen = ((value != null) ? ((int)value) : 0);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600BD43 RID: 48451 RVA: 0x0039B844 File Offset: 0x00399A44
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
			case 7:
				info = this.Properties[7];
				return true;
			case 8:
				info = this.Properties[8];
				return true;
			case 9:
				info = this.Properties[9];
				return true;
			case 10:
				info = this.Properties[10];
				return true;
			case 11:
				info = this.Properties[11];
				return true;
			case 12:
				info = this.Properties[12];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}

		// Token: 0x04009A59 RID: 39513
		public const int ModelId = 229;

		// Token: 0x04009A5A RID: 39514
		private int m_RewardTrackId;

		// Token: 0x04009A5B RID: 39515
		private int m_Level;

		// Token: 0x04009A5C RID: 39516
		private int m_Xp;

		// Token: 0x04009A5D RID: 39517
		private int m_XpNeeded;

		// Token: 0x04009A5E RID: 39518
		private int m_XpBonusPercent;

		// Token: 0x04009A5F RID: 39519
		private bool m_PremiumRewardsUnlocked;

		// Token: 0x04009A60 RID: 39520
		private string m_XpProgress;

		// Token: 0x04009A61 RID: 39521
		private int m_Unclaimed;

		// Token: 0x04009A62 RID: 39522
		private int m_LevelSoftCap;

		// Token: 0x04009A63 RID: 39523
		private string m_XpBoostText;

		// Token: 0x04009A64 RID: 39524
		private int m_LevelHardCap;

		// Token: 0x04009A65 RID: 39525
		private int m_Season;

		// Token: 0x04009A66 RID: 39526
		private int m_SeasonLastSeen;

		// Token: 0x04009A67 RID: 39527
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "reward_track_id",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "level",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "xp",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "xp_needed",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "xp_bonus_percent",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "premium_rewards_unlocked",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "xp_progress",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "unclaimed",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "level_soft_cap",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "xp_boost_text",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "level_hard_cap",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "season",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "season_last_seen",
				Type = typeof(int)
			}
		};
	}
}
