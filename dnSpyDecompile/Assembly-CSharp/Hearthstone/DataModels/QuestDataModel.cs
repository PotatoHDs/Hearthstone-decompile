using System;
using Assets;
using Hearthstone.Progression;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010D1 RID: 4305
	public class QuestDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600BC7A RID: 48250 RVA: 0x003982D4 File Offset: 0x003964D4
		public QuestDataModel()
		{
			base.RegisterNestedDataModel(this.m_Icon);
			base.RegisterNestedDataModel(this.m_Rewards);
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x0600BC7B RID: 48251 RVA: 0x0039863F File Offset: 0x0039683F
		public int DataModelId
		{
			get
			{
				return 207;
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x0600BC7C RID: 48252 RVA: 0x00398646 File Offset: 0x00396846
		public string DataModelDisplayName
		{
			get
			{
				return "quest";
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x0600BC7E RID: 48254 RVA: 0x00398678 File Offset: 0x00396878
		// (set) Token: 0x0600BC7D RID: 48253 RVA: 0x0039864D File Offset: 0x0039684D
		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				if (this.m_Name == value)
				{
					return;
				}
				this.m_Name = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x0600BC80 RID: 48256 RVA: 0x003986A6 File Offset: 0x003968A6
		// (set) Token: 0x0600BC7F RID: 48255 RVA: 0x00398680 File Offset: 0x00396880
		public int Progress
		{
			get
			{
				return this.m_Progress;
			}
			set
			{
				if (this.m_Progress == value)
				{
					return;
				}
				this.m_Progress = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x0600BC82 RID: 48258 RVA: 0x003986D4 File Offset: 0x003968D4
		// (set) Token: 0x0600BC81 RID: 48257 RVA: 0x003986AE File Offset: 0x003968AE
		public int Quota
		{
			get
			{
				return this.m_Quota;
			}
			set
			{
				if (this.m_Quota == value)
				{
					return;
				}
				this.m_Quota = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x0600BC84 RID: 48260 RVA: 0x00398707 File Offset: 0x00396907
		// (set) Token: 0x0600BC83 RID: 48259 RVA: 0x003986DC File Offset: 0x003968DC
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

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x0600BC86 RID: 48262 RVA: 0x00398748 File Offset: 0x00396948
		// (set) Token: 0x0600BC85 RID: 48261 RVA: 0x0039870F File Offset: 0x0039690F
		public DataModelList<string> Icon
		{
			get
			{
				return this.m_Icon;
			}
			set
			{
				if (this.m_Icon == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Icon);
				base.RegisterNestedDataModel(value);
				this.m_Icon = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x0600BC88 RID: 48264 RVA: 0x00398776 File Offset: 0x00396976
		// (set) Token: 0x0600BC87 RID: 48263 RVA: 0x00398750 File Offset: 0x00396950
		public QuestPool.QuestPoolType PoolType
		{
			get
			{
				return this.m_PoolType;
			}
			set
			{
				if (this.m_PoolType == value)
				{
					return;
				}
				this.m_PoolType = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x0600BC8A RID: 48266 RVA: 0x003987A9 File Offset: 0x003969A9
		// (set) Token: 0x0600BC89 RID: 48265 RVA: 0x0039877E File Offset: 0x0039697E
		public string TimeUntilNextQuest
		{
			get
			{
				return this.m_TimeUntilNextQuest;
			}
			set
			{
				if (this.m_TimeUntilNextQuest == value)
				{
					return;
				}
				this.m_TimeUntilNextQuest = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x0600BC8C RID: 48268 RVA: 0x003987D7 File Offset: 0x003969D7
		// (set) Token: 0x0600BC8B RID: 48267 RVA: 0x003987B1 File Offset: 0x003969B1
		public int RerollCount
		{
			get
			{
				return this.m_RerollCount;
			}
			set
			{
				if (this.m_RerollCount == value)
				{
					return;
				}
				this.m_RerollCount = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x0600BC8E RID: 48270 RVA: 0x00398805 File Offset: 0x00396A05
		// (set) Token: 0x0600BC8D RID: 48269 RVA: 0x003987DF File Offset: 0x003969DF
		public QuestManager.QuestTileDisplayMode DisplayMode
		{
			get
			{
				return this.m_DisplayMode;
			}
			set
			{
				if (this.m_DisplayMode == value)
				{
					return;
				}
				this.m_DisplayMode = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x0600BC90 RID: 48272 RVA: 0x00398846 File Offset: 0x00396A46
		// (set) Token: 0x0600BC8F RID: 48271 RVA: 0x0039880D File Offset: 0x00396A0D
		public RewardListDataModel Rewards
		{
			get
			{
				return this.m_Rewards;
			}
			set
			{
				if (this.m_Rewards == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Rewards);
				base.RegisterNestedDataModel(value);
				this.m_Rewards = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x0600BC92 RID: 48274 RVA: 0x00398874 File Offset: 0x00396A74
		// (set) Token: 0x0600BC91 RID: 48273 RVA: 0x0039884E File Offset: 0x00396A4E
		public int QuestId
		{
			get
			{
				return this.m_QuestId;
			}
			set
			{
				if (this.m_QuestId == value)
				{
					return;
				}
				this.m_QuestId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x0600BC94 RID: 48276 RVA: 0x003988A2 File Offset: 0x00396AA2
		// (set) Token: 0x0600BC93 RID: 48275 RVA: 0x0039887C File Offset: 0x00396A7C
		public int RewardTrackXp
		{
			get
			{
				return this.m_RewardTrackXp;
			}
			set
			{
				if (this.m_RewardTrackXp == value)
				{
					return;
				}
				this.m_RewardTrackXp = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x0600BC96 RID: 48278 RVA: 0x003988D5 File Offset: 0x00396AD5
		// (set) Token: 0x0600BC95 RID: 48277 RVA: 0x003988AA File Offset: 0x00396AAA
		public string ProgressMessage
		{
			get
			{
				return this.m_ProgressMessage;
			}
			set
			{
				if (this.m_ProgressMessage == value)
				{
					return;
				}
				this.m_ProgressMessage = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x0600BC98 RID: 48280 RVA: 0x00398903 File Offset: 0x00396B03
		// (set) Token: 0x0600BC97 RID: 48279 RVA: 0x003988DD File Offset: 0x00396ADD
		public QuestManager.QuestStatus Status
		{
			get
			{
				return this.m_Status;
			}
			set
			{
				if (this.m_Status == value)
				{
					return;
				}
				this.m_Status = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600BC9A RID: 48282 RVA: 0x00398931 File Offset: 0x00396B31
		// (set) Token: 0x0600BC99 RID: 48281 RVA: 0x0039890B File Offset: 0x00396B0B
		public int PoolId
		{
			get
			{
				return this.m_PoolId;
			}
			set
			{
				if (this.m_PoolId == value)
				{
					return;
				}
				this.m_PoolId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600BC9B RID: 48283 RVA: 0x00398939 File Offset: 0x00396B39
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600BC9C RID: 48284 RVA: 0x00398944 File Offset: 0x00396B44
		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0)) * 31;
			int progress = this.m_Progress;
			int num2 = (num + this.m_Progress.GetHashCode()) * 31;
			int quota = this.m_Quota;
			int num3 = (((num2 + this.m_Quota.GetHashCode()) * 31 + ((this.m_Description != null) ? this.m_Description.GetHashCode() : 0)) * 31 + ((this.m_Icon != null) ? this.m_Icon.GetPropertiesHashCode() : 0)) * 31;
			QuestPool.QuestPoolType poolType = this.m_PoolType;
			int num4 = ((num3 + this.m_PoolType.GetHashCode()) * 31 + ((this.m_TimeUntilNextQuest != null) ? this.m_TimeUntilNextQuest.GetHashCode() : 0)) * 31;
			int rerollCount = this.m_RerollCount;
			int num5 = (num4 + this.m_RerollCount.GetHashCode()) * 31;
			QuestManager.QuestTileDisplayMode displayMode = this.m_DisplayMode;
			int num6 = ((num5 + this.m_DisplayMode.GetHashCode()) * 31 + ((this.m_Rewards != null) ? this.m_Rewards.GetPropertiesHashCode() : 0)) * 31;
			int questId = this.m_QuestId;
			int num7 = (num6 + this.m_QuestId.GetHashCode()) * 31;
			int rewardTrackXp = this.m_RewardTrackXp;
			int num8 = ((num7 + this.m_RewardTrackXp.GetHashCode()) * 31 + ((this.m_ProgressMessage != null) ? this.m_ProgressMessage.GetHashCode() : 0)) * 31;
			QuestManager.QuestStatus status = this.m_Status;
			int num9 = (num8 + this.m_Status.GetHashCode()) * 31;
			int poolId = this.m_PoolId;
			return num9 + this.m_PoolId.GetHashCode();
		}

		// Token: 0x0600BC9D RID: 48285 RVA: 0x00398AC8 File Offset: 0x00396CC8
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_Name;
				return true;
			case 1:
				value = this.m_Progress;
				return true;
			case 2:
				value = this.m_Quota;
				return true;
			case 3:
				value = this.m_Description;
				return true;
			case 5:
				value = this.m_Icon;
				return true;
			case 6:
				value = this.m_PoolType;
				return true;
			case 7:
				value = this.m_TimeUntilNextQuest;
				return true;
			case 8:
				value = this.m_RerollCount;
				return true;
			case 9:
				value = this.m_DisplayMode;
				return true;
			case 10:
				value = this.m_Rewards;
				return true;
			case 11:
				value = this.m_QuestId;
				return true;
			case 12:
				value = this.m_RewardTrackXp;
				return true;
			case 13:
				value = this.m_ProgressMessage;
				return true;
			case 14:
				value = this.m_Status;
				return true;
			case 15:
				value = this.m_PoolId;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600BC9E RID: 48286 RVA: 0x00398BE8 File Offset: 0x00396DE8
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.Name = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				this.Progress = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				this.Quota = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				this.Description = ((value != null) ? ((string)value) : null);
				return true;
			case 5:
				this.Icon = ((value != null) ? ((DataModelList<string>)value) : null);
				return true;
			case 6:
				this.PoolType = ((value != null) ? ((QuestPool.QuestPoolType)value) : QuestPool.QuestPoolType.NONE);
				return true;
			case 7:
				this.TimeUntilNextQuest = ((value != null) ? ((string)value) : null);
				return true;
			case 8:
				this.RerollCount = ((value != null) ? ((int)value) : 0);
				return true;
			case 9:
				this.DisplayMode = ((value != null) ? ((QuestManager.QuestTileDisplayMode)value) : QuestManager.QuestTileDisplayMode.DEFAULT);
				return true;
			case 10:
				this.Rewards = ((value != null) ? ((RewardListDataModel)value) : null);
				return true;
			case 11:
				this.QuestId = ((value != null) ? ((int)value) : 0);
				return true;
			case 12:
				this.RewardTrackXp = ((value != null) ? ((int)value) : 0);
				return true;
			case 13:
				this.ProgressMessage = ((value != null) ? ((string)value) : null);
				return true;
			case 14:
				this.Status = ((value != null) ? ((QuestManager.QuestStatus)value) : QuestManager.QuestStatus.UNKNOWN);
				return true;
			case 15:
				this.PoolId = ((value != null) ? ((int)value) : 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600BC9F RID: 48287 RVA: 0x00398D70 File Offset: 0x00396F70
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
			case 5:
				info = this.Properties[4];
				return true;
			case 6:
				info = this.Properties[5];
				return true;
			case 7:
				info = this.Properties[6];
				return true;
			case 8:
				info = this.Properties[7];
				return true;
			case 9:
				info = this.Properties[8];
				return true;
			case 10:
				info = this.Properties[9];
				return true;
			case 11:
				info = this.Properties[10];
				return true;
			case 12:
				info = this.Properties[11];
				return true;
			case 13:
				info = this.Properties[12];
				return true;
			case 14:
				info = this.Properties[13];
				return true;
			case 15:
				info = this.Properties[14];
				return true;
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x04009A15 RID: 39445
		public const int ModelId = 207;

		// Token: 0x04009A16 RID: 39446
		private string m_Name;

		// Token: 0x04009A17 RID: 39447
		private int m_Progress;

		// Token: 0x04009A18 RID: 39448
		private int m_Quota;

		// Token: 0x04009A19 RID: 39449
		private string m_Description;

		// Token: 0x04009A1A RID: 39450
		private DataModelList<string> m_Icon = new DataModelList<string>();

		// Token: 0x04009A1B RID: 39451
		private QuestPool.QuestPoolType m_PoolType;

		// Token: 0x04009A1C RID: 39452
		private string m_TimeUntilNextQuest;

		// Token: 0x04009A1D RID: 39453
		private int m_RerollCount;

		// Token: 0x04009A1E RID: 39454
		private QuestManager.QuestTileDisplayMode m_DisplayMode;

		// Token: 0x04009A1F RID: 39455
		private RewardListDataModel m_Rewards;

		// Token: 0x04009A20 RID: 39456
		private int m_QuestId;

		// Token: 0x04009A21 RID: 39457
		private int m_RewardTrackXp;

		// Token: 0x04009A22 RID: 39458
		private string m_ProgressMessage;

		// Token: 0x04009A23 RID: 39459
		private QuestManager.QuestStatus m_Status;

		// Token: 0x04009A24 RID: 39460
		private int m_PoolId;

		// Token: 0x04009A25 RID: 39461
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "progress",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "quota",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "description",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "icon",
				Type = typeof(DataModelList<string>)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "pool",
				Type = typeof(QuestPool.QuestPoolType)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "time_until_next_quest",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "reroll_count",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "display_mode",
				Type = typeof(QuestManager.QuestTileDisplayMode)
			},
			new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "rewards",
				Type = typeof(RewardListDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "quest_id",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "reward_track_xp",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 13,
				PropertyDisplayName = "progress_message",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 14,
				PropertyDisplayName = "status",
				Type = typeof(QuestManager.QuestStatus)
			},
			new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "pool_id",
				Type = typeof(int)
			}
		};
	}
}
