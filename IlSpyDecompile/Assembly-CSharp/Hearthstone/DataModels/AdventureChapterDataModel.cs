using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class AdventureChapterDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 3;

		private int m_ChapterNumber;

		private AdventureChapterState m_ChapterState;

		private bool m_AvailableForPurchase;

		private bool m_NewlyUnlocked;

		private bool m_NewlyCompleted;

		private bool m_TimeLocked;

		private string m_TimeLockInfoMessage;

		private int m_FirstHeroBundledWithChapter;

		private int m_SecondHeroBundledWithChapter;

		private RewardListDataModel m_CompletionRewards;

		private Reward.Type m_CompletionRewardType;

		private int m_CompletionRewardId;

		private int m_CompletionRewardQuantity;

		private bool m_IsPreviousChapterOwned;

		private bool m_PlayerOwnsChapter;

		private bool m_WantsNewlyUnlockedSequence;

		private bool m_CompletionRewardsEarned;

		private bool m_CompletionRewardsNewlyEarned;

		private int m_WingId;

		private bool m_ShowNewlyUnlockedHighlight;

		private string m_Name;

		private string m_Description;

		private string m_UnlockChapterText;

		private string m_StoreDescriptionText;

		private bool m_DisplayRaidBossHealth;

		private int m_RaidBossHealthAmount;

		private bool m_FinalPurchasableChapter;

		private int m_RaidBossStartingHealthAmount;

		private bool m_IsAnomalyModeAvailable;

		private DataModelList<AdventureMissionDataModel> m_Missions = new DataModelList<AdventureMissionDataModel>();

		private AdventureBookPageMoralAlignment m_MoralAlignment;

		private RewardListDataModel m_PurchaseRewards;

		private DataModelProperty[] m_properties;

		public int DataModelId => 3;

		public string DataModelDisplayName => "adventure_chapter";

		public int ChapterNumber
		{
			get
			{
				return m_ChapterNumber;
			}
			set
			{
				if (m_ChapterNumber != value)
				{
					m_ChapterNumber = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public AdventureChapterState ChapterState
		{
			get
			{
				return m_ChapterState;
			}
			set
			{
				if (m_ChapterState != value)
				{
					m_ChapterState = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool AvailableForPurchase
		{
			get
			{
				return m_AvailableForPurchase;
			}
			set
			{
				if (m_AvailableForPurchase != value)
				{
					m_AvailableForPurchase = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool NewlyUnlocked
		{
			get
			{
				return m_NewlyUnlocked;
			}
			set
			{
				if (m_NewlyUnlocked != value)
				{
					m_NewlyUnlocked = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool NewlyCompleted
		{
			get
			{
				return m_NewlyCompleted;
			}
			set
			{
				if (m_NewlyCompleted != value)
				{
					m_NewlyCompleted = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool TimeLocked
		{
			get
			{
				return m_TimeLocked;
			}
			set
			{
				if (m_TimeLocked != value)
				{
					m_TimeLocked = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string TimeLockInfoMessage
		{
			get
			{
				return m_TimeLockInfoMessage;
			}
			set
			{
				if (!(m_TimeLockInfoMessage == value))
				{
					m_TimeLockInfoMessage = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int FirstHeroBundledWithChapter
		{
			get
			{
				return m_FirstHeroBundledWithChapter;
			}
			set
			{
				if (m_FirstHeroBundledWithChapter != value)
				{
					m_FirstHeroBundledWithChapter = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int SecondHeroBundledWithChapter
		{
			get
			{
				return m_SecondHeroBundledWithChapter;
			}
			set
			{
				if (m_SecondHeroBundledWithChapter != value)
				{
					m_SecondHeroBundledWithChapter = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public RewardListDataModel CompletionRewards
		{
			get
			{
				return m_CompletionRewards;
			}
			set
			{
				if (m_CompletionRewards != value)
				{
					RemoveNestedDataModel(m_CompletionRewards);
					RegisterNestedDataModel(value);
					m_CompletionRewards = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public Reward.Type CompletionRewardType
		{
			get
			{
				return m_CompletionRewardType;
			}
			set
			{
				if (m_CompletionRewardType != value)
				{
					m_CompletionRewardType = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int CompletionRewardId
		{
			get
			{
				return m_CompletionRewardId;
			}
			set
			{
				if (m_CompletionRewardId != value)
				{
					m_CompletionRewardId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int CompletionRewardQuantity
		{
			get
			{
				return m_CompletionRewardQuantity;
			}
			set
			{
				if (m_CompletionRewardQuantity != value)
				{
					m_CompletionRewardQuantity = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsPreviousChapterOwned
		{
			get
			{
				return m_IsPreviousChapterOwned;
			}
			set
			{
				if (m_IsPreviousChapterOwned != value)
				{
					m_IsPreviousChapterOwned = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool PlayerOwnsChapter
		{
			get
			{
				return m_PlayerOwnsChapter;
			}
			set
			{
				if (m_PlayerOwnsChapter != value)
				{
					m_PlayerOwnsChapter = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool WantsNewlyUnlockedSequence
		{
			get
			{
				return m_WantsNewlyUnlockedSequence;
			}
			set
			{
				if (m_WantsNewlyUnlockedSequence != value)
				{
					m_WantsNewlyUnlockedSequence = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool CompletionRewardsEarned
		{
			get
			{
				return m_CompletionRewardsEarned;
			}
			set
			{
				if (m_CompletionRewardsEarned != value)
				{
					m_CompletionRewardsEarned = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool CompletionRewardsNewlyEarned
		{
			get
			{
				return m_CompletionRewardsNewlyEarned;
			}
			set
			{
				if (m_CompletionRewardsNewlyEarned != value)
				{
					m_CompletionRewardsNewlyEarned = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int WingId
		{
			get
			{
				return m_WingId;
			}
			set
			{
				if (m_WingId != value)
				{
					m_WingId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool ShowNewlyUnlockedHighlight
		{
			get
			{
				return m_ShowNewlyUnlockedHighlight;
			}
			set
			{
				if (m_ShowNewlyUnlockedHighlight != value)
				{
					m_ShowNewlyUnlockedHighlight = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
				if (!(m_Name == value))
				{
					m_Name = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string Description
		{
			get
			{
				return m_Description;
			}
			set
			{
				if (!(m_Description == value))
				{
					m_Description = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string UnlockChapterText
		{
			get
			{
				return m_UnlockChapterText;
			}
			set
			{
				if (!(m_UnlockChapterText == value))
				{
					m_UnlockChapterText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string StoreDescriptionText
		{
			get
			{
				return m_StoreDescriptionText;
			}
			set
			{
				if (!(m_StoreDescriptionText == value))
				{
					m_StoreDescriptionText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool DisplayRaidBossHealth
		{
			get
			{
				return m_DisplayRaidBossHealth;
			}
			set
			{
				if (m_DisplayRaidBossHealth != value)
				{
					m_DisplayRaidBossHealth = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int RaidBossHealthAmount
		{
			get
			{
				return m_RaidBossHealthAmount;
			}
			set
			{
				if (m_RaidBossHealthAmount != value)
				{
					m_RaidBossHealthAmount = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool FinalPurchasableChapter
		{
			get
			{
				return m_FinalPurchasableChapter;
			}
			set
			{
				if (m_FinalPurchasableChapter != value)
				{
					m_FinalPurchasableChapter = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int RaidBossStartingHealthAmount
		{
			get
			{
				return m_RaidBossStartingHealthAmount;
			}
			set
			{
				if (m_RaidBossStartingHealthAmount != value)
				{
					m_RaidBossStartingHealthAmount = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsAnomalyModeAvailable
		{
			get
			{
				return m_IsAnomalyModeAvailable;
			}
			set
			{
				if (m_IsAnomalyModeAvailable != value)
				{
					m_IsAnomalyModeAvailable = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<AdventureMissionDataModel> Missions
		{
			get
			{
				return m_Missions;
			}
			set
			{
				if (m_Missions != value)
				{
					RemoveNestedDataModel(m_Missions);
					RegisterNestedDataModel(value);
					m_Missions = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public AdventureBookPageMoralAlignment MoralAlignment
		{
			get
			{
				return m_MoralAlignment;
			}
			set
			{
				if (m_MoralAlignment != value)
				{
					m_MoralAlignment = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public RewardListDataModel PurchaseRewards
		{
			get
			{
				return m_PurchaseRewards;
			}
			set
			{
				if (m_PurchaseRewards != value)
				{
					RemoveNestedDataModel(m_PurchaseRewards);
					RegisterNestedDataModel(value);
					m_PurchaseRewards = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public AdventureChapterDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[32];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "chapter_number",
				Type = typeof(int)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "chapter_state",
				Type = typeof(AdventureChapterState)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "available_for_purchase",
				Type = typeof(bool)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "newly_unlocked",
				Type = typeof(bool)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "newly_completed",
				Type = typeof(bool)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "time_locked",
				Type = typeof(bool)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "time_lock_info_message",
				Type = typeof(string)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "first_hero_bundled_with_chapter",
				Type = typeof(int)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "second_hero_bundled_with_chapter",
				Type = typeof(int)
			};
			array[8] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 171,
				PropertyDisplayName = "completion_rewards",
				Type = typeof(RewardListDataModel)
			};
			array[9] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "DEPRECATED_completion_reward_type",
				Type = typeof(Reward.Type)
			};
			array[10] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "DEPRECATED_completion_reward_id",
				Type = typeof(int)
			};
			array[11] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "DEPRECATED_completion_reward_quantity",
				Type = typeof(int)
			};
			array[12] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "is_previous_chapter_owned",
				Type = typeof(bool)
			};
			array[13] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 13,
				PropertyDisplayName = "player_owns_chapter",
				Type = typeof(bool)
			};
			array[14] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 14,
				PropertyDisplayName = "wants_newly_unlocked_sequence",
				Type = typeof(bool)
			};
			array[15] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "completion_rewards_earned",
				Type = typeof(bool)
			};
			array[16] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 16,
				PropertyDisplayName = "completion_rewards_newly_earned",
				Type = typeof(bool)
			};
			array[17] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 17,
				PropertyDisplayName = "wing_id",
				Type = typeof(int)
			};
			array[18] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 18,
				PropertyDisplayName = "show_newly_unlocked_highlight",
				Type = typeof(bool)
			};
			array[19] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 19,
				PropertyDisplayName = "name",
				Type = typeof(string)
			};
			array[20] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 174,
				PropertyDisplayName = "description",
				Type = typeof(string)
			};
			array[21] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 20,
				PropertyDisplayName = "unlock_chapter_text",
				Type = typeof(string)
			};
			array[22] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 21,
				PropertyDisplayName = "store_description",
				Type = typeof(string)
			};
			array[23] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 22,
				PropertyDisplayName = "show_raid_boss_health",
				Type = typeof(bool)
			};
			array[24] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 23,
				PropertyDisplayName = "raid_boss_health_amount",
				Type = typeof(int)
			};
			array[25] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 24,
				PropertyDisplayName = "final_purchasable_chapter",
				Type = typeof(bool)
			};
			array[26] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 25,
				PropertyDisplayName = "raid_boss_starting_health_amount",
				Type = typeof(int)
			};
			array[27] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 26,
				PropertyDisplayName = "is_anomaly_mode_available",
				Type = typeof(bool)
			};
			array[28] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 27,
				PropertyDisplayName = "missions",
				Type = typeof(DataModelList<AdventureMissionDataModel>)
			};
			array[29] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 133,
				PropertyDisplayName = "moral_alignment",
				Type = typeof(AdventureBookPageMoralAlignment)
			};
			array[30] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 139,
				PropertyDisplayName = "purchase_rewards",
				Type = typeof(RewardListDataModel)
			};
			array[31] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_CompletionRewards);
			RegisterNestedDataModel(m_Missions);
			RegisterNestedDataModel(m_PurchaseRewards);
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_ChapterNumber;
			int num2 = (num + m_ChapterNumber.GetHashCode()) * 31;
			_ = m_ChapterState;
			int num3 = (num2 + m_ChapterState.GetHashCode()) * 31;
			_ = m_AvailableForPurchase;
			int num4 = (num3 + m_AvailableForPurchase.GetHashCode()) * 31;
			_ = m_NewlyUnlocked;
			int num5 = (num4 + m_NewlyUnlocked.GetHashCode()) * 31;
			_ = m_NewlyCompleted;
			int num6 = (num5 + m_NewlyCompleted.GetHashCode()) * 31;
			_ = m_TimeLocked;
			int num7 = ((num6 + m_TimeLocked.GetHashCode()) * 31 + ((m_TimeLockInfoMessage != null) ? m_TimeLockInfoMessage.GetHashCode() : 0)) * 31;
			_ = m_FirstHeroBundledWithChapter;
			int num8 = (num7 + m_FirstHeroBundledWithChapter.GetHashCode()) * 31;
			_ = m_SecondHeroBundledWithChapter;
			int num9 = ((num8 + m_SecondHeroBundledWithChapter.GetHashCode()) * 31 + ((m_CompletionRewards != null) ? m_CompletionRewards.GetPropertiesHashCode() : 0)) * 31;
			_ = m_CompletionRewardType;
			int num10 = (num9 + m_CompletionRewardType.GetHashCode()) * 31;
			_ = m_CompletionRewardId;
			int num11 = (num10 + m_CompletionRewardId.GetHashCode()) * 31;
			_ = m_CompletionRewardQuantity;
			int num12 = (num11 + m_CompletionRewardQuantity.GetHashCode()) * 31;
			_ = m_IsPreviousChapterOwned;
			int num13 = (num12 + m_IsPreviousChapterOwned.GetHashCode()) * 31;
			_ = m_PlayerOwnsChapter;
			int num14 = (num13 + m_PlayerOwnsChapter.GetHashCode()) * 31;
			_ = m_WantsNewlyUnlockedSequence;
			int num15 = (num14 + m_WantsNewlyUnlockedSequence.GetHashCode()) * 31;
			_ = m_CompletionRewardsEarned;
			int num16 = (num15 + m_CompletionRewardsEarned.GetHashCode()) * 31;
			_ = m_CompletionRewardsNewlyEarned;
			int num17 = (num16 + m_CompletionRewardsNewlyEarned.GetHashCode()) * 31;
			_ = m_WingId;
			int num18 = (num17 + m_WingId.GetHashCode()) * 31;
			_ = m_ShowNewlyUnlockedHighlight;
			int num19 = (((((num18 + m_ShowNewlyUnlockedHighlight.GetHashCode()) * 31 + ((m_Name != null) ? m_Name.GetHashCode() : 0)) * 31 + ((m_Description != null) ? m_Description.GetHashCode() : 0)) * 31 + ((m_UnlockChapterText != null) ? m_UnlockChapterText.GetHashCode() : 0)) * 31 + ((m_StoreDescriptionText != null) ? m_StoreDescriptionText.GetHashCode() : 0)) * 31;
			_ = m_DisplayRaidBossHealth;
			int num20 = (num19 + m_DisplayRaidBossHealth.GetHashCode()) * 31;
			_ = m_RaidBossHealthAmount;
			int num21 = (num20 + m_RaidBossHealthAmount.GetHashCode()) * 31;
			_ = m_FinalPurchasableChapter;
			int num22 = (num21 + m_FinalPurchasableChapter.GetHashCode()) * 31;
			_ = m_RaidBossStartingHealthAmount;
			int num23 = (num22 + m_RaidBossStartingHealthAmount.GetHashCode()) * 31;
			_ = m_IsAnomalyModeAvailable;
			int num24 = ((num23 + m_IsAnomalyModeAvailable.GetHashCode()) * 31 + ((m_Missions != null) ? m_Missions.GetPropertiesHashCode() : 0)) * 31;
			_ = m_MoralAlignment;
			return (num24 + m_MoralAlignment.GetHashCode()) * 31 + ((m_PurchaseRewards != null) ? m_PurchaseRewards.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_ChapterNumber;
				return true;
			case 1:
				value = m_ChapterState;
				return true;
			case 2:
				value = m_AvailableForPurchase;
				return true;
			case 3:
				value = m_NewlyUnlocked;
				return true;
			case 4:
				value = m_NewlyCompleted;
				return true;
			case 5:
				value = m_TimeLocked;
				return true;
			case 6:
				value = m_TimeLockInfoMessage;
				return true;
			case 7:
				value = m_FirstHeroBundledWithChapter;
				return true;
			case 8:
				value = m_SecondHeroBundledWithChapter;
				return true;
			case 171:
				value = m_CompletionRewards;
				return true;
			case 9:
				value = m_CompletionRewardType;
				return true;
			case 10:
				value = m_CompletionRewardId;
				return true;
			case 11:
				value = m_CompletionRewardQuantity;
				return true;
			case 12:
				value = m_IsPreviousChapterOwned;
				return true;
			case 13:
				value = m_PlayerOwnsChapter;
				return true;
			case 14:
				value = m_WantsNewlyUnlockedSequence;
				return true;
			case 15:
				value = m_CompletionRewardsEarned;
				return true;
			case 16:
				value = m_CompletionRewardsNewlyEarned;
				return true;
			case 17:
				value = m_WingId;
				return true;
			case 18:
				value = m_ShowNewlyUnlockedHighlight;
				return true;
			case 19:
				value = m_Name;
				return true;
			case 174:
				value = m_Description;
				return true;
			case 20:
				value = m_UnlockChapterText;
				return true;
			case 21:
				value = m_StoreDescriptionText;
				return true;
			case 22:
				value = m_DisplayRaidBossHealth;
				return true;
			case 23:
				value = m_RaidBossHealthAmount;
				return true;
			case 24:
				value = m_FinalPurchasableChapter;
				return true;
			case 25:
				value = m_RaidBossStartingHealthAmount;
				return true;
			case 26:
				value = m_IsAnomalyModeAvailable;
				return true;
			case 27:
				value = m_Missions;
				return true;
			case 133:
				value = m_MoralAlignment;
				return true;
			case 139:
				value = m_PurchaseRewards;
				return true;
			default:
				value = null;
				return false;
			}
		}

		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				ChapterNumber = ((value != null) ? ((int)value) : 0);
				return true;
			case 1:
				ChapterState = ((value != null) ? ((AdventureChapterState)value) : AdventureChapterState.LOCKED);
				return true;
			case 2:
				AvailableForPurchase = value != null && (bool)value;
				return true;
			case 3:
				NewlyUnlocked = value != null && (bool)value;
				return true;
			case 4:
				NewlyCompleted = value != null && (bool)value;
				return true;
			case 5:
				TimeLocked = value != null && (bool)value;
				return true;
			case 6:
				TimeLockInfoMessage = ((value != null) ? ((string)value) : null);
				return true;
			case 7:
				FirstHeroBundledWithChapter = ((value != null) ? ((int)value) : 0);
				return true;
			case 8:
				SecondHeroBundledWithChapter = ((value != null) ? ((int)value) : 0);
				return true;
			case 171:
				CompletionRewards = ((value != null) ? ((RewardListDataModel)value) : null);
				return true;
			case 9:
				CompletionRewardType = ((value != null) ? ((Reward.Type)value) : Reward.Type.ARCANE_DUST);
				return true;
			case 10:
				CompletionRewardId = ((value != null) ? ((int)value) : 0);
				return true;
			case 11:
				CompletionRewardQuantity = ((value != null) ? ((int)value) : 0);
				return true;
			case 12:
				IsPreviousChapterOwned = value != null && (bool)value;
				return true;
			case 13:
				PlayerOwnsChapter = value != null && (bool)value;
				return true;
			case 14:
				WantsNewlyUnlockedSequence = value != null && (bool)value;
				return true;
			case 15:
				CompletionRewardsEarned = value != null && (bool)value;
				return true;
			case 16:
				CompletionRewardsNewlyEarned = value != null && (bool)value;
				return true;
			case 17:
				WingId = ((value != null) ? ((int)value) : 0);
				return true;
			case 18:
				ShowNewlyUnlockedHighlight = value != null && (bool)value;
				return true;
			case 19:
				Name = ((value != null) ? ((string)value) : null);
				return true;
			case 174:
				Description = ((value != null) ? ((string)value) : null);
				return true;
			case 20:
				UnlockChapterText = ((value != null) ? ((string)value) : null);
				return true;
			case 21:
				StoreDescriptionText = ((value != null) ? ((string)value) : null);
				return true;
			case 22:
				DisplayRaidBossHealth = value != null && (bool)value;
				return true;
			case 23:
				RaidBossHealthAmount = ((value != null) ? ((int)value) : 0);
				return true;
			case 24:
				FinalPurchasableChapter = value != null && (bool)value;
				return true;
			case 25:
				RaidBossStartingHealthAmount = ((value != null) ? ((int)value) : 0);
				return true;
			case 26:
				IsAnomalyModeAvailable = value != null && (bool)value;
				return true;
			case 27:
				Missions = ((value != null) ? ((DataModelList<AdventureMissionDataModel>)value) : null);
				return true;
			case 133:
				MoralAlignment = ((value != null) ? ((AdventureBookPageMoralAlignment)value) : AdventureBookPageMoralAlignment.GOOD);
				return true;
			case 139:
				PurchaseRewards = ((value != null) ? ((RewardListDataModel)value) : null);
				return true;
			default:
				return false;
			}
		}

		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 0:
				info = Properties[0];
				return true;
			case 1:
				info = Properties[1];
				return true;
			case 2:
				info = Properties[2];
				return true;
			case 3:
				info = Properties[3];
				return true;
			case 4:
				info = Properties[4];
				return true;
			case 5:
				info = Properties[5];
				return true;
			case 6:
				info = Properties[6];
				return true;
			case 7:
				info = Properties[7];
				return true;
			case 8:
				info = Properties[8];
				return true;
			case 171:
				info = Properties[9];
				return true;
			case 9:
				info = Properties[10];
				return true;
			case 10:
				info = Properties[11];
				return true;
			case 11:
				info = Properties[12];
				return true;
			case 12:
				info = Properties[13];
				return true;
			case 13:
				info = Properties[14];
				return true;
			case 14:
				info = Properties[15];
				return true;
			case 15:
				info = Properties[16];
				return true;
			case 16:
				info = Properties[17];
				return true;
			case 17:
				info = Properties[18];
				return true;
			case 18:
				info = Properties[19];
				return true;
			case 19:
				info = Properties[20];
				return true;
			case 174:
				info = Properties[21];
				return true;
			case 20:
				info = Properties[22];
				return true;
			case 21:
				info = Properties[23];
				return true;
			case 22:
				info = Properties[24];
				return true;
			case 23:
				info = Properties[25];
				return true;
			case 24:
				info = Properties[26];
				return true;
			case 25:
				info = Properties[27];
				return true;
			case 26:
				info = Properties[28];
				return true;
			case 27:
				info = Properties[29];
				return true;
			case 133:
				info = Properties[30];
				return true;
			case 139:
				info = Properties[31];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
