using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010A4 RID: 4260
	public class AdventureDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B948 RID: 47432 RVA: 0x0038B9AC File Offset: 0x00389BAC
		public AdventureDataModel()
		{
			base.RegisterNestedDataModel(this.m_CompletionRewards);
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x0600B949 RID: 47433 RVA: 0x0038BC94 File Offset: 0x00389E94
		public int DataModelId
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600B94A RID: 47434 RVA: 0x0038BC97 File Offset: 0x00389E97
		public string DataModelDisplayName
		{
			get
			{
				return "adventure";
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x0600B94C RID: 47436 RVA: 0x0038BCC4 File Offset: 0x00389EC4
		// (set) Token: 0x0600B94B RID: 47435 RVA: 0x0038BC9E File Offset: 0x00389E9E
		public bool AnomalyActivated
		{
			get
			{
				return this.m_AnomalyActivated;
			}
			set
			{
				if (this.m_AnomalyActivated == value)
				{
					return;
				}
				this.m_AnomalyActivated = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600B94E RID: 47438 RVA: 0x0038BCF2 File Offset: 0x00389EF2
		// (set) Token: 0x0600B94D RID: 47437 RVA: 0x0038BCCC File Offset: 0x00389ECC
		public bool AllChaptersCompleted
		{
			get
			{
				return this.m_AllChaptersCompleted;
			}
			set
			{
				if (this.m_AllChaptersCompleted == value)
				{
					return;
				}
				this.m_AllChaptersCompleted = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x0600B950 RID: 47440 RVA: 0x0038BD20 File Offset: 0x00389F20
		// (set) Token: 0x0600B94F RID: 47439 RVA: 0x0038BCFA File Offset: 0x00389EFA
		public bool AllChaptersOwned
		{
			get
			{
				return this.m_AllChaptersOwned;
			}
			set
			{
				if (this.m_AllChaptersOwned == value)
				{
					return;
				}
				this.m_AllChaptersOwned = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x0600B952 RID: 47442 RVA: 0x0038BD4E File Offset: 0x00389F4E
		// (set) Token: 0x0600B951 RID: 47441 RVA: 0x0038BD28 File Offset: 0x00389F28
		public AdventureDbId SelectedAdventure
		{
			get
			{
				return this.m_SelectedAdventure;
			}
			set
			{
				if (this.m_SelectedAdventure == value)
				{
					return;
				}
				this.m_SelectedAdventure = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x0600B954 RID: 47444 RVA: 0x0038BD7C File Offset: 0x00389F7C
		// (set) Token: 0x0600B953 RID: 47443 RVA: 0x0038BD56 File Offset: 0x00389F56
		public AdventureModeDbId SelectedAdventureMode
		{
			get
			{
				return this.m_SelectedAdventureMode;
			}
			set
			{
				if (this.m_SelectedAdventureMode == value)
				{
					return;
				}
				this.m_SelectedAdventureMode = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x0600B956 RID: 47446 RVA: 0x0038BDAA File Offset: 0x00389FAA
		// (set) Token: 0x0600B955 RID: 47445 RVA: 0x0038BD84 File Offset: 0x00389F84
		public bool MapNewlyCompleted
		{
			get
			{
				return this.m_MapNewlyCompleted;
			}
			set
			{
				if (this.m_MapNewlyCompleted == value)
				{
					return;
				}
				this.m_MapNewlyCompleted = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x0600B958 RID: 47448 RVA: 0x0038BDEB File Offset: 0x00389FEB
		// (set) Token: 0x0600B957 RID: 47447 RVA: 0x0038BDB2 File Offset: 0x00389FB2
		public RewardListDataModel CompletionRewards
		{
			get
			{
				return this.m_CompletionRewards;
			}
			set
			{
				if (this.m_CompletionRewards == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_CompletionRewards);
				base.RegisterNestedDataModel(value);
				this.m_CompletionRewards = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x0600B95A RID: 47450 RVA: 0x0038BE19 File Offset: 0x0038A019
		// (set) Token: 0x0600B959 RID: 47449 RVA: 0x0038BDF3 File Offset: 0x00389FF3
		public Reward.Type CompletionRewardType
		{
			get
			{
				return this.m_CompletionRewardType;
			}
			set
			{
				if (this.m_CompletionRewardType == value)
				{
					return;
				}
				this.m_CompletionRewardType = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600B95C RID: 47452 RVA: 0x0038BE47 File Offset: 0x0038A047
		// (set) Token: 0x0600B95B RID: 47451 RVA: 0x0038BE21 File Offset: 0x0038A021
		public int CompletionRewardId
		{
			get
			{
				return this.m_CompletionRewardId;
			}
			set
			{
				if (this.m_CompletionRewardId == value)
				{
					return;
				}
				this.m_CompletionRewardId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600B95E RID: 47454 RVA: 0x0038BE7A File Offset: 0x0038A07A
		// (set) Token: 0x0600B95D RID: 47453 RVA: 0x0038BE4F File Offset: 0x0038A04F
		public string StoreDescriptionTextTimelockedTrue
		{
			get
			{
				return this.m_StoreDescriptionTextTimelockedTrue;
			}
			set
			{
				if (this.m_StoreDescriptionTextTimelockedTrue == value)
				{
					return;
				}
				this.m_StoreDescriptionTextTimelockedTrue = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600B960 RID: 47456 RVA: 0x0038BEAD File Offset: 0x0038A0AD
		// (set) Token: 0x0600B95F RID: 47455 RVA: 0x0038BE82 File Offset: 0x0038A082
		public string StoreDescriptionTextTimelockedFalse
		{
			get
			{
				return this.m_StoreDescriptionTextTimelockedFalse;
			}
			set
			{
				if (this.m_StoreDescriptionTextTimelockedFalse == value)
				{
					return;
				}
				this.m_StoreDescriptionTextTimelockedFalse = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x0600B962 RID: 47458 RVA: 0x0038BEDB File Offset: 0x0038A0DB
		// (set) Token: 0x0600B961 RID: 47457 RVA: 0x0038BEB5 File Offset: 0x0038A0B5
		public bool ShouldSeeFirstTimeFlow
		{
			get
			{
				return this.m_ShouldSeeFirstTimeFlow;
			}
			set
			{
				if (this.m_ShouldSeeFirstTimeFlow == value)
				{
					return;
				}
				this.m_ShouldSeeFirstTimeFlow = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x0600B964 RID: 47460 RVA: 0x0038BF09 File Offset: 0x0038A109
		// (set) Token: 0x0600B963 RID: 47459 RVA: 0x0038BEE3 File Offset: 0x0038A0E3
		public bool IsSelectedModeHeroic
		{
			get
			{
				return this.m_IsSelectedModeHeroic;
			}
			set
			{
				if (this.m_IsSelectedModeHeroic == value)
				{
					return;
				}
				this.m_IsSelectedModeHeroic = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x0600B965 RID: 47461 RVA: 0x0038BF11 File Offset: 0x0038A111
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B966 RID: 47462 RVA: 0x0038BF1C File Offset: 0x0038A11C
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			bool anomalyActivated = this.m_AnomalyActivated;
			int num2 = (num + this.m_AnomalyActivated.GetHashCode()) * 31;
			bool allChaptersCompleted = this.m_AllChaptersCompleted;
			int num3 = (num2 + this.m_AllChaptersCompleted.GetHashCode()) * 31;
			bool allChaptersOwned = this.m_AllChaptersOwned;
			int num4 = (num3 + this.m_AllChaptersOwned.GetHashCode()) * 31;
			AdventureDbId selectedAdventure = this.m_SelectedAdventure;
			int num5 = (num4 + this.m_SelectedAdventure.GetHashCode()) * 31;
			AdventureModeDbId selectedAdventureMode = this.m_SelectedAdventureMode;
			int num6 = (num5 + this.m_SelectedAdventureMode.GetHashCode()) * 31;
			bool mapNewlyCompleted = this.m_MapNewlyCompleted;
			int num7 = ((num6 + this.m_MapNewlyCompleted.GetHashCode()) * 31 + ((this.m_CompletionRewards != null) ? this.m_CompletionRewards.GetPropertiesHashCode() : 0)) * 31;
			Reward.Type completionRewardType = this.m_CompletionRewardType;
			int num8 = (num7 + this.m_CompletionRewardType.GetHashCode()) * 31;
			int completionRewardId = this.m_CompletionRewardId;
			int num9 = (((num8 + this.m_CompletionRewardId.GetHashCode()) * 31 + ((this.m_StoreDescriptionTextTimelockedTrue != null) ? this.m_StoreDescriptionTextTimelockedTrue.GetHashCode() : 0)) * 31 + ((this.m_StoreDescriptionTextTimelockedFalse != null) ? this.m_StoreDescriptionTextTimelockedFalse.GetHashCode() : 0)) * 31;
			bool shouldSeeFirstTimeFlow = this.m_ShouldSeeFirstTimeFlow;
			int num10 = (num9 + this.m_ShouldSeeFirstTimeFlow.GetHashCode()) * 31;
			bool isSelectedModeHeroic = this.m_IsSelectedModeHeroic;
			return num10 + this.m_IsSelectedModeHeroic.GetHashCode();
		}

		// Token: 0x0600B967 RID: 47463 RVA: 0x0038C068 File Offset: 0x0038A268
		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = this.m_AnomalyActivated;
				return true;
			case 1:
				value = this.m_AllChaptersCompleted;
				return true;
			case 2:
				value = this.m_AllChaptersOwned;
				return true;
			case 3:
				value = this.m_SelectedAdventure;
				return true;
			case 4:
				value = this.m_SelectedAdventureMode;
				return true;
			case 5:
				value = this.m_MapNewlyCompleted;
				return true;
			case 6:
				value = this.m_CompletionRewardType;
				return true;
			case 7:
				value = this.m_CompletionRewardId;
				return true;
			case 8:
				value = this.m_StoreDescriptionTextTimelockedTrue;
				return true;
			case 9:
				value = this.m_StoreDescriptionTextTimelockedFalse;
				return true;
			case 10:
				value = this.m_ShouldSeeFirstTimeFlow;
				return true;
			default:
				if (id == 126)
				{
					value = this.m_IsSelectedModeHeroic;
					return true;
				}
				if (id != 170)
				{
					value = null;
					return false;
				}
				value = this.m_CompletionRewards;
				return true;
			}
		}

		// Token: 0x0600B968 RID: 47464 RVA: 0x0038C174 File Offset: 0x0038A374
		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				this.AnomalyActivated = (value != null && (bool)value);
				return true;
			case 1:
				this.AllChaptersCompleted = (value != null && (bool)value);
				return true;
			case 2:
				this.AllChaptersOwned = (value != null && (bool)value);
				return true;
			case 3:
				this.SelectedAdventure = ((value != null) ? ((AdventureDbId)value) : AdventureDbId.INVALID);
				return true;
			case 4:
				this.SelectedAdventureMode = ((value != null) ? ((AdventureModeDbId)value) : AdventureModeDbId.INVALID);
				return true;
			case 5:
				this.MapNewlyCompleted = (value != null && (bool)value);
				return true;
			case 6:
				this.CompletionRewardType = ((value != null) ? ((Reward.Type)value) : Reward.Type.ARCANE_DUST);
				return true;
			case 7:
				this.CompletionRewardId = ((value != null) ? ((int)value) : 0);
				return true;
			case 8:
				this.StoreDescriptionTextTimelockedTrue = ((value != null) ? ((string)value) : null);
				return true;
			case 9:
				this.StoreDescriptionTextTimelockedFalse = ((value != null) ? ((string)value) : null);
				return true;
			case 10:
				this.ShouldSeeFirstTimeFlow = (value != null && (bool)value);
				return true;
			default:
				if (id == 126)
				{
					this.IsSelectedModeHeroic = (value != null && (bool)value);
					return true;
				}
				if (id != 170)
				{
					return false;
				}
				this.CompletionRewards = ((value != null) ? ((RewardListDataModel)value) : null);
				return true;
			}
		}

		// Token: 0x0600B969 RID: 47465 RVA: 0x0038C2D0 File Offset: 0x0038A4D0
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
				info = this.Properties[7];
				return true;
			case 7:
				info = this.Properties[8];
				return true;
			case 8:
				info = this.Properties[9];
				return true;
			case 9:
				info = this.Properties[10];
				return true;
			case 10:
				info = this.Properties[11];
				return true;
			default:
				if (id == 126)
				{
					info = this.Properties[12];
					return true;
				}
				if (id != 170)
				{
					info = default(DataModelProperty);
					return false;
				}
				info = this.Properties[6];
				return true;
			}
		}

		// Token: 0x040098D6 RID: 39126
		public const int ModelId = 7;

		// Token: 0x040098D7 RID: 39127
		private bool m_AnomalyActivated;

		// Token: 0x040098D8 RID: 39128
		private bool m_AllChaptersCompleted;

		// Token: 0x040098D9 RID: 39129
		private bool m_AllChaptersOwned;

		// Token: 0x040098DA RID: 39130
		private AdventureDbId m_SelectedAdventure;

		// Token: 0x040098DB RID: 39131
		private AdventureModeDbId m_SelectedAdventureMode;

		// Token: 0x040098DC RID: 39132
		private bool m_MapNewlyCompleted;

		// Token: 0x040098DD RID: 39133
		private RewardListDataModel m_CompletionRewards;

		// Token: 0x040098DE RID: 39134
		private Reward.Type m_CompletionRewardType;

		// Token: 0x040098DF RID: 39135
		private int m_CompletionRewardId;

		// Token: 0x040098E0 RID: 39136
		private string m_StoreDescriptionTextTimelockedTrue;

		// Token: 0x040098E1 RID: 39137
		private string m_StoreDescriptionTextTimelockedFalse;

		// Token: 0x040098E2 RID: 39138
		private bool m_ShouldSeeFirstTimeFlow;

		// Token: 0x040098E3 RID: 39139
		private bool m_IsSelectedModeHeroic;

		// Token: 0x040098E4 RID: 39140
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "anomaly_activated",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "all_chapters_completed",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "all_chapters_owned",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "selected_adventure",
				Type = typeof(AdventureDbId)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "selected_adventure_mode",
				Type = typeof(AdventureModeDbId)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "map_newly_completed",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 170,
				PropertyDisplayName = "completion_rewards",
				Type = typeof(RewardListDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "DEPRECATED_completion_reward_type",
				Type = typeof(Reward.Type)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "DEPRECATED_completion_reward_id",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "store_description_timelocked_true",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "store_description_timelocked_false",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "should_see_first_time_flow",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 126,
				PropertyDisplayName = "is_selected_mode_heroic",
				Type = typeof(bool)
			}
		};
	}
}
