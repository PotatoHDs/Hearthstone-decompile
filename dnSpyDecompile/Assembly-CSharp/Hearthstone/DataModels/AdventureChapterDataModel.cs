using System;
using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	// Token: 0x020010A2 RID: 4258
	public class AdventureChapterDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		// Token: 0x0600B8F4 RID: 47348 RVA: 0x00389E1C File Offset: 0x0038801C
		public AdventureChapterDataModel()
		{
			base.RegisterNestedDataModel(this.m_CompletionRewards);
			base.RegisterNestedDataModel(this.m_Missions);
			base.RegisterNestedDataModel(this.m_PurchaseRewards);
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600B8F5 RID: 47349 RVA: 0x00005302 File Offset: 0x00003502
		public int DataModelId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600B8F6 RID: 47350 RVA: 0x0038A545 File Offset: 0x00388745
		public string DataModelDisplayName
		{
			get
			{
				return "adventure_chapter";
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600B8F8 RID: 47352 RVA: 0x0038A572 File Offset: 0x00388772
		// (set) Token: 0x0600B8F7 RID: 47351 RVA: 0x0038A54C File Offset: 0x0038874C
		public int ChapterNumber
		{
			get
			{
				return this.m_ChapterNumber;
			}
			set
			{
				if (this.m_ChapterNumber == value)
				{
					return;
				}
				this.m_ChapterNumber = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x0600B8FA RID: 47354 RVA: 0x0038A5A0 File Offset: 0x003887A0
		// (set) Token: 0x0600B8F9 RID: 47353 RVA: 0x0038A57A File Offset: 0x0038877A
		public AdventureChapterState ChapterState
		{
			get
			{
				return this.m_ChapterState;
			}
			set
			{
				if (this.m_ChapterState == value)
				{
					return;
				}
				this.m_ChapterState = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x0600B8FC RID: 47356 RVA: 0x0038A5CE File Offset: 0x003887CE
		// (set) Token: 0x0600B8FB RID: 47355 RVA: 0x0038A5A8 File Offset: 0x003887A8
		public bool AvailableForPurchase
		{
			get
			{
				return this.m_AvailableForPurchase;
			}
			set
			{
				if (this.m_AvailableForPurchase == value)
				{
					return;
				}
				this.m_AvailableForPurchase = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600B8FE RID: 47358 RVA: 0x0038A5FC File Offset: 0x003887FC
		// (set) Token: 0x0600B8FD RID: 47357 RVA: 0x0038A5D6 File Offset: 0x003887D6
		public bool NewlyUnlocked
		{
			get
			{
				return this.m_NewlyUnlocked;
			}
			set
			{
				if (this.m_NewlyUnlocked == value)
				{
					return;
				}
				this.m_NewlyUnlocked = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600B900 RID: 47360 RVA: 0x0038A62A File Offset: 0x0038882A
		// (set) Token: 0x0600B8FF RID: 47359 RVA: 0x0038A604 File Offset: 0x00388804
		public bool NewlyCompleted
		{
			get
			{
				return this.m_NewlyCompleted;
			}
			set
			{
				if (this.m_NewlyCompleted == value)
				{
					return;
				}
				this.m_NewlyCompleted = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600B902 RID: 47362 RVA: 0x0038A658 File Offset: 0x00388858
		// (set) Token: 0x0600B901 RID: 47361 RVA: 0x0038A632 File Offset: 0x00388832
		public bool TimeLocked
		{
			get
			{
				return this.m_TimeLocked;
			}
			set
			{
				if (this.m_TimeLocked == value)
				{
					return;
				}
				this.m_TimeLocked = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x0600B904 RID: 47364 RVA: 0x0038A68B File Offset: 0x0038888B
		// (set) Token: 0x0600B903 RID: 47363 RVA: 0x0038A660 File Offset: 0x00388860
		public string TimeLockInfoMessage
		{
			get
			{
				return this.m_TimeLockInfoMessage;
			}
			set
			{
				if (this.m_TimeLockInfoMessage == value)
				{
					return;
				}
				this.m_TimeLockInfoMessage = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600B906 RID: 47366 RVA: 0x0038A6B9 File Offset: 0x003888B9
		// (set) Token: 0x0600B905 RID: 47365 RVA: 0x0038A693 File Offset: 0x00388893
		public int FirstHeroBundledWithChapter
		{
			get
			{
				return this.m_FirstHeroBundledWithChapter;
			}
			set
			{
				if (this.m_FirstHeroBundledWithChapter == value)
				{
					return;
				}
				this.m_FirstHeroBundledWithChapter = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x0600B908 RID: 47368 RVA: 0x0038A6E7 File Offset: 0x003888E7
		// (set) Token: 0x0600B907 RID: 47367 RVA: 0x0038A6C1 File Offset: 0x003888C1
		public int SecondHeroBundledWithChapter
		{
			get
			{
				return this.m_SecondHeroBundledWithChapter;
			}
			set
			{
				if (this.m_SecondHeroBundledWithChapter == value)
				{
					return;
				}
				this.m_SecondHeroBundledWithChapter = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x0600B90A RID: 47370 RVA: 0x0038A728 File Offset: 0x00388928
		// (set) Token: 0x0600B909 RID: 47369 RVA: 0x0038A6EF File Offset: 0x003888EF
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

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x0600B90C RID: 47372 RVA: 0x0038A756 File Offset: 0x00388956
		// (set) Token: 0x0600B90B RID: 47371 RVA: 0x0038A730 File Offset: 0x00388930
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

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600B90E RID: 47374 RVA: 0x0038A784 File Offset: 0x00388984
		// (set) Token: 0x0600B90D RID: 47373 RVA: 0x0038A75E File Offset: 0x0038895E
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

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600B910 RID: 47376 RVA: 0x0038A7B2 File Offset: 0x003889B2
		// (set) Token: 0x0600B90F RID: 47375 RVA: 0x0038A78C File Offset: 0x0038898C
		public int CompletionRewardQuantity
		{
			get
			{
				return this.m_CompletionRewardQuantity;
			}
			set
			{
				if (this.m_CompletionRewardQuantity == value)
				{
					return;
				}
				this.m_CompletionRewardQuantity = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x0600B912 RID: 47378 RVA: 0x0038A7E0 File Offset: 0x003889E0
		// (set) Token: 0x0600B911 RID: 47377 RVA: 0x0038A7BA File Offset: 0x003889BA
		public bool IsPreviousChapterOwned
		{
			get
			{
				return this.m_IsPreviousChapterOwned;
			}
			set
			{
				if (this.m_IsPreviousChapterOwned == value)
				{
					return;
				}
				this.m_IsPreviousChapterOwned = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x0600B914 RID: 47380 RVA: 0x0038A80E File Offset: 0x00388A0E
		// (set) Token: 0x0600B913 RID: 47379 RVA: 0x0038A7E8 File Offset: 0x003889E8
		public bool PlayerOwnsChapter
		{
			get
			{
				return this.m_PlayerOwnsChapter;
			}
			set
			{
				if (this.m_PlayerOwnsChapter == value)
				{
					return;
				}
				this.m_PlayerOwnsChapter = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x0600B916 RID: 47382 RVA: 0x0038A83C File Offset: 0x00388A3C
		// (set) Token: 0x0600B915 RID: 47381 RVA: 0x0038A816 File Offset: 0x00388A16
		public bool WantsNewlyUnlockedSequence
		{
			get
			{
				return this.m_WantsNewlyUnlockedSequence;
			}
			set
			{
				if (this.m_WantsNewlyUnlockedSequence == value)
				{
					return;
				}
				this.m_WantsNewlyUnlockedSequence = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600B918 RID: 47384 RVA: 0x0038A86A File Offset: 0x00388A6A
		// (set) Token: 0x0600B917 RID: 47383 RVA: 0x0038A844 File Offset: 0x00388A44
		public bool CompletionRewardsEarned
		{
			get
			{
				return this.m_CompletionRewardsEarned;
			}
			set
			{
				if (this.m_CompletionRewardsEarned == value)
				{
					return;
				}
				this.m_CompletionRewardsEarned = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600B91A RID: 47386 RVA: 0x0038A898 File Offset: 0x00388A98
		// (set) Token: 0x0600B919 RID: 47385 RVA: 0x0038A872 File Offset: 0x00388A72
		public bool CompletionRewardsNewlyEarned
		{
			get
			{
				return this.m_CompletionRewardsNewlyEarned;
			}
			set
			{
				if (this.m_CompletionRewardsNewlyEarned == value)
				{
					return;
				}
				this.m_CompletionRewardsNewlyEarned = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x0600B91C RID: 47388 RVA: 0x0038A8C6 File Offset: 0x00388AC6
		// (set) Token: 0x0600B91B RID: 47387 RVA: 0x0038A8A0 File Offset: 0x00388AA0
		public int WingId
		{
			get
			{
				return this.m_WingId;
			}
			set
			{
				if (this.m_WingId == value)
				{
					return;
				}
				this.m_WingId = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600B91E RID: 47390 RVA: 0x0038A8F4 File Offset: 0x00388AF4
		// (set) Token: 0x0600B91D RID: 47389 RVA: 0x0038A8CE File Offset: 0x00388ACE
		public bool ShowNewlyUnlockedHighlight
		{
			get
			{
				return this.m_ShowNewlyUnlockedHighlight;
			}
			set
			{
				if (this.m_ShowNewlyUnlockedHighlight == value)
				{
					return;
				}
				this.m_ShowNewlyUnlockedHighlight = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600B920 RID: 47392 RVA: 0x0038A927 File Offset: 0x00388B27
		// (set) Token: 0x0600B91F RID: 47391 RVA: 0x0038A8FC File Offset: 0x00388AFC
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

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600B922 RID: 47394 RVA: 0x0038A95A File Offset: 0x00388B5A
		// (set) Token: 0x0600B921 RID: 47393 RVA: 0x0038A92F File Offset: 0x00388B2F
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

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x0600B924 RID: 47396 RVA: 0x0038A98D File Offset: 0x00388B8D
		// (set) Token: 0x0600B923 RID: 47395 RVA: 0x0038A962 File Offset: 0x00388B62
		public string UnlockChapterText
		{
			get
			{
				return this.m_UnlockChapterText;
			}
			set
			{
				if (this.m_UnlockChapterText == value)
				{
					return;
				}
				this.m_UnlockChapterText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x0600B926 RID: 47398 RVA: 0x0038A9C0 File Offset: 0x00388BC0
		// (set) Token: 0x0600B925 RID: 47397 RVA: 0x0038A995 File Offset: 0x00388B95
		public string StoreDescriptionText
		{
			get
			{
				return this.m_StoreDescriptionText;
			}
			set
			{
				if (this.m_StoreDescriptionText == value)
				{
					return;
				}
				this.m_StoreDescriptionText = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600B928 RID: 47400 RVA: 0x0038A9EE File Offset: 0x00388BEE
		// (set) Token: 0x0600B927 RID: 47399 RVA: 0x0038A9C8 File Offset: 0x00388BC8
		public bool DisplayRaidBossHealth
		{
			get
			{
				return this.m_DisplayRaidBossHealth;
			}
			set
			{
				if (this.m_DisplayRaidBossHealth == value)
				{
					return;
				}
				this.m_DisplayRaidBossHealth = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600B92A RID: 47402 RVA: 0x0038AA1C File Offset: 0x00388C1C
		// (set) Token: 0x0600B929 RID: 47401 RVA: 0x0038A9F6 File Offset: 0x00388BF6
		public int RaidBossHealthAmount
		{
			get
			{
				return this.m_RaidBossHealthAmount;
			}
			set
			{
				if (this.m_RaidBossHealthAmount == value)
				{
					return;
				}
				this.m_RaidBossHealthAmount = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600B92C RID: 47404 RVA: 0x0038AA4A File Offset: 0x00388C4A
		// (set) Token: 0x0600B92B RID: 47403 RVA: 0x0038AA24 File Offset: 0x00388C24
		public bool FinalPurchasableChapter
		{
			get
			{
				return this.m_FinalPurchasableChapter;
			}
			set
			{
				if (this.m_FinalPurchasableChapter == value)
				{
					return;
				}
				this.m_FinalPurchasableChapter = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600B92E RID: 47406 RVA: 0x0038AA78 File Offset: 0x00388C78
		// (set) Token: 0x0600B92D RID: 47405 RVA: 0x0038AA52 File Offset: 0x00388C52
		public int RaidBossStartingHealthAmount
		{
			get
			{
				return this.m_RaidBossStartingHealthAmount;
			}
			set
			{
				if (this.m_RaidBossStartingHealthAmount == value)
				{
					return;
				}
				this.m_RaidBossStartingHealthAmount = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600B930 RID: 47408 RVA: 0x0038AAA6 File Offset: 0x00388CA6
		// (set) Token: 0x0600B92F RID: 47407 RVA: 0x0038AA80 File Offset: 0x00388C80
		public bool IsAnomalyModeAvailable
		{
			get
			{
				return this.m_IsAnomalyModeAvailable;
			}
			set
			{
				if (this.m_IsAnomalyModeAvailable == value)
				{
					return;
				}
				this.m_IsAnomalyModeAvailable = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600B932 RID: 47410 RVA: 0x0038AAE7 File Offset: 0x00388CE7
		// (set) Token: 0x0600B931 RID: 47409 RVA: 0x0038AAAE File Offset: 0x00388CAE
		public DataModelList<AdventureMissionDataModel> Missions
		{
			get
			{
				return this.m_Missions;
			}
			set
			{
				if (this.m_Missions == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_Missions);
				base.RegisterNestedDataModel(value);
				this.m_Missions = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x0600B934 RID: 47412 RVA: 0x0038AB15 File Offset: 0x00388D15
		// (set) Token: 0x0600B933 RID: 47411 RVA: 0x0038AAEF File Offset: 0x00388CEF
		public AdventureBookPageMoralAlignment MoralAlignment
		{
			get
			{
				return this.m_MoralAlignment;
			}
			set
			{
				if (this.m_MoralAlignment == value)
				{
					return;
				}
				this.m_MoralAlignment = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x0600B936 RID: 47414 RVA: 0x0038AB56 File Offset: 0x00388D56
		// (set) Token: 0x0600B935 RID: 47413 RVA: 0x0038AB1D File Offset: 0x00388D1D
		public RewardListDataModel PurchaseRewards
		{
			get
			{
				return this.m_PurchaseRewards;
			}
			set
			{
				if (this.m_PurchaseRewards == value)
				{
					return;
				}
				base.RemoveNestedDataModel(this.m_PurchaseRewards);
				base.RegisterNestedDataModel(value);
				this.m_PurchaseRewards = value;
				base.DispatchChangedListeners(null);
				DataContext.DataVersion++;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x0600B937 RID: 47415 RVA: 0x0038AB5E File Offset: 0x00388D5E
		public DataModelProperty[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B938 RID: 47416 RVA: 0x0038AB68 File Offset: 0x00388D68
		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			int chapterNumber = this.m_ChapterNumber;
			int num2 = (num + this.m_ChapterNumber.GetHashCode()) * 31;
			AdventureChapterState chapterState = this.m_ChapterState;
			int num3 = (num2 + this.m_ChapterState.GetHashCode()) * 31;
			bool availableForPurchase = this.m_AvailableForPurchase;
			int num4 = (num3 + this.m_AvailableForPurchase.GetHashCode()) * 31;
			bool newlyUnlocked = this.m_NewlyUnlocked;
			int num5 = (num4 + this.m_NewlyUnlocked.GetHashCode()) * 31;
			bool newlyCompleted = this.m_NewlyCompleted;
			int num6 = (num5 + this.m_NewlyCompleted.GetHashCode()) * 31;
			bool timeLocked = this.m_TimeLocked;
			int num7 = ((num6 + this.m_TimeLocked.GetHashCode()) * 31 + ((this.m_TimeLockInfoMessage != null) ? this.m_TimeLockInfoMessage.GetHashCode() : 0)) * 31;
			int firstHeroBundledWithChapter = this.m_FirstHeroBundledWithChapter;
			int num8 = (num7 + this.m_FirstHeroBundledWithChapter.GetHashCode()) * 31;
			int secondHeroBundledWithChapter = this.m_SecondHeroBundledWithChapter;
			int num9 = ((num8 + this.m_SecondHeroBundledWithChapter.GetHashCode()) * 31 + ((this.m_CompletionRewards != null) ? this.m_CompletionRewards.GetPropertiesHashCode() : 0)) * 31;
			Reward.Type completionRewardType = this.m_CompletionRewardType;
			int num10 = (num9 + this.m_CompletionRewardType.GetHashCode()) * 31;
			int completionRewardId = this.m_CompletionRewardId;
			int num11 = (num10 + this.m_CompletionRewardId.GetHashCode()) * 31;
			int completionRewardQuantity = this.m_CompletionRewardQuantity;
			int num12 = (num11 + this.m_CompletionRewardQuantity.GetHashCode()) * 31;
			bool isPreviousChapterOwned = this.m_IsPreviousChapterOwned;
			int num13 = (num12 + this.m_IsPreviousChapterOwned.GetHashCode()) * 31;
			bool playerOwnsChapter = this.m_PlayerOwnsChapter;
			int num14 = (num13 + this.m_PlayerOwnsChapter.GetHashCode()) * 31;
			bool wantsNewlyUnlockedSequence = this.m_WantsNewlyUnlockedSequence;
			int num15 = (num14 + this.m_WantsNewlyUnlockedSequence.GetHashCode()) * 31;
			bool completionRewardsEarned = this.m_CompletionRewardsEarned;
			int num16 = (num15 + this.m_CompletionRewardsEarned.GetHashCode()) * 31;
			bool completionRewardsNewlyEarned = this.m_CompletionRewardsNewlyEarned;
			int num17 = (num16 + this.m_CompletionRewardsNewlyEarned.GetHashCode()) * 31;
			int wingId = this.m_WingId;
			int num18 = (num17 + this.m_WingId.GetHashCode()) * 31;
			bool showNewlyUnlockedHighlight = this.m_ShowNewlyUnlockedHighlight;
			int num19 = (((((num18 + this.m_ShowNewlyUnlockedHighlight.GetHashCode()) * 31 + ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0)) * 31 + ((this.m_Description != null) ? this.m_Description.GetHashCode() : 0)) * 31 + ((this.m_UnlockChapterText != null) ? this.m_UnlockChapterText.GetHashCode() : 0)) * 31 + ((this.m_StoreDescriptionText != null) ? this.m_StoreDescriptionText.GetHashCode() : 0)) * 31;
			bool displayRaidBossHealth = this.m_DisplayRaidBossHealth;
			int num20 = (num19 + this.m_DisplayRaidBossHealth.GetHashCode()) * 31;
			int raidBossHealthAmount = this.m_RaidBossHealthAmount;
			int num21 = (num20 + this.m_RaidBossHealthAmount.GetHashCode()) * 31;
			bool finalPurchasableChapter = this.m_FinalPurchasableChapter;
			int num22 = (num21 + this.m_FinalPurchasableChapter.GetHashCode()) * 31;
			int raidBossStartingHealthAmount = this.m_RaidBossStartingHealthAmount;
			int num23 = (num22 + this.m_RaidBossStartingHealthAmount.GetHashCode()) * 31;
			bool isAnomalyModeAvailable = this.m_IsAnomalyModeAvailable;
			int num24 = ((num23 + this.m_IsAnomalyModeAvailable.GetHashCode()) * 31 + ((this.m_Missions != null) ? this.m_Missions.GetPropertiesHashCode() : 0)) * 31;
			AdventureBookPageMoralAlignment moralAlignment = this.m_MoralAlignment;
			return (num24 + this.m_MoralAlignment.GetHashCode()) * 31 + ((this.m_PurchaseRewards != null) ? this.m_PurchaseRewards.GetPropertiesHashCode() : 0);
		}

		// Token: 0x0600B939 RID: 47417 RVA: 0x0038AE6C File Offset: 0x0038906C
		public bool GetPropertyValue(int id, out object value)
		{
			if (id <= 133)
			{
				switch (id)
				{
				case 0:
					value = this.m_ChapterNumber;
					return true;
				case 1:
					value = this.m_ChapterState;
					return true;
				case 2:
					value = this.m_AvailableForPurchase;
					return true;
				case 3:
					value = this.m_NewlyUnlocked;
					return true;
				case 4:
					value = this.m_NewlyCompleted;
					return true;
				case 5:
					value = this.m_TimeLocked;
					return true;
				case 6:
					value = this.m_TimeLockInfoMessage;
					return true;
				case 7:
					value = this.m_FirstHeroBundledWithChapter;
					return true;
				case 8:
					value = this.m_SecondHeroBundledWithChapter;
					return true;
				case 9:
					value = this.m_CompletionRewardType;
					return true;
				case 10:
					value = this.m_CompletionRewardId;
					return true;
				case 11:
					value = this.m_CompletionRewardQuantity;
					return true;
				case 12:
					value = this.m_IsPreviousChapterOwned;
					return true;
				case 13:
					value = this.m_PlayerOwnsChapter;
					return true;
				case 14:
					value = this.m_WantsNewlyUnlockedSequence;
					return true;
				case 15:
					value = this.m_CompletionRewardsEarned;
					return true;
				case 16:
					value = this.m_CompletionRewardsNewlyEarned;
					return true;
				case 17:
					value = this.m_WingId;
					return true;
				case 18:
					value = this.m_ShowNewlyUnlockedHighlight;
					return true;
				case 19:
					value = this.m_Name;
					return true;
				case 20:
					value = this.m_UnlockChapterText;
					return true;
				case 21:
					value = this.m_StoreDescriptionText;
					return true;
				case 22:
					value = this.m_DisplayRaidBossHealth;
					return true;
				case 23:
					value = this.m_RaidBossHealthAmount;
					return true;
				case 24:
					value = this.m_FinalPurchasableChapter;
					return true;
				case 25:
					value = this.m_RaidBossStartingHealthAmount;
					return true;
				case 26:
					value = this.m_IsAnomalyModeAvailable;
					return true;
				case 27:
					value = this.m_Missions;
					return true;
				default:
					if (id == 133)
					{
						value = this.m_MoralAlignment;
						return true;
					}
					break;
				}
			}
			else
			{
				if (id == 139)
				{
					value = this.m_PurchaseRewards;
					return true;
				}
				if (id == 171)
				{
					value = this.m_CompletionRewards;
					return true;
				}
				if (id == 174)
				{
					value = this.m_Description;
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x0600B93A RID: 47418 RVA: 0x0038B0EC File Offset: 0x003892EC
		public bool SetPropertyValue(int id, object value)
		{
			if (id <= 133)
			{
				switch (id)
				{
				case 0:
					this.ChapterNumber = ((value != null) ? ((int)value) : 0);
					return true;
				case 1:
					this.ChapterState = ((value != null) ? ((AdventureChapterState)value) : AdventureChapterState.LOCKED);
					return true;
				case 2:
					this.AvailableForPurchase = (value != null && (bool)value);
					return true;
				case 3:
					this.NewlyUnlocked = (value != null && (bool)value);
					return true;
				case 4:
					this.NewlyCompleted = (value != null && (bool)value);
					return true;
				case 5:
					this.TimeLocked = (value != null && (bool)value);
					return true;
				case 6:
					this.TimeLockInfoMessage = ((value != null) ? ((string)value) : null);
					return true;
				case 7:
					this.FirstHeroBundledWithChapter = ((value != null) ? ((int)value) : 0);
					return true;
				case 8:
					this.SecondHeroBundledWithChapter = ((value != null) ? ((int)value) : 0);
					return true;
				case 9:
					this.CompletionRewardType = ((value != null) ? ((Reward.Type)value) : Reward.Type.ARCANE_DUST);
					return true;
				case 10:
					this.CompletionRewardId = ((value != null) ? ((int)value) : 0);
					return true;
				case 11:
					this.CompletionRewardQuantity = ((value != null) ? ((int)value) : 0);
					return true;
				case 12:
					this.IsPreviousChapterOwned = (value != null && (bool)value);
					return true;
				case 13:
					this.PlayerOwnsChapter = (value != null && (bool)value);
					return true;
				case 14:
					this.WantsNewlyUnlockedSequence = (value != null && (bool)value);
					return true;
				case 15:
					this.CompletionRewardsEarned = (value != null && (bool)value);
					return true;
				case 16:
					this.CompletionRewardsNewlyEarned = (value != null && (bool)value);
					return true;
				case 17:
					this.WingId = ((value != null) ? ((int)value) : 0);
					return true;
				case 18:
					this.ShowNewlyUnlockedHighlight = (value != null && (bool)value);
					return true;
				case 19:
					this.Name = ((value != null) ? ((string)value) : null);
					return true;
				case 20:
					this.UnlockChapterText = ((value != null) ? ((string)value) : null);
					return true;
				case 21:
					this.StoreDescriptionText = ((value != null) ? ((string)value) : null);
					return true;
				case 22:
					this.DisplayRaidBossHealth = (value != null && (bool)value);
					return true;
				case 23:
					this.RaidBossHealthAmount = ((value != null) ? ((int)value) : 0);
					return true;
				case 24:
					this.FinalPurchasableChapter = (value != null && (bool)value);
					return true;
				case 25:
					this.RaidBossStartingHealthAmount = ((value != null) ? ((int)value) : 0);
					return true;
				case 26:
					this.IsAnomalyModeAvailable = (value != null && (bool)value);
					return true;
				case 27:
					this.Missions = ((value != null) ? ((DataModelList<AdventureMissionDataModel>)value) : null);
					return true;
				default:
					if (id == 133)
					{
						this.MoralAlignment = ((value != null) ? ((AdventureBookPageMoralAlignment)value) : AdventureBookPageMoralAlignment.GOOD);
						return true;
					}
					break;
				}
			}
			else
			{
				if (id == 139)
				{
					this.PurchaseRewards = ((value != null) ? ((RewardListDataModel)value) : null);
					return true;
				}
				if (id == 171)
				{
					this.CompletionRewards = ((value != null) ? ((RewardListDataModel)value) : null);
					return true;
				}
				if (id == 174)
				{
					this.Description = ((value != null) ? ((string)value) : null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600B93B RID: 47419 RVA: 0x0038B434 File Offset: 0x00389634
		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			if (id <= 133)
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
					info = this.Properties[10];
					return true;
				case 10:
					info = this.Properties[11];
					return true;
				case 11:
					info = this.Properties[12];
					return true;
				case 12:
					info = this.Properties[13];
					return true;
				case 13:
					info = this.Properties[14];
					return true;
				case 14:
					info = this.Properties[15];
					return true;
				case 15:
					info = this.Properties[16];
					return true;
				case 16:
					info = this.Properties[17];
					return true;
				case 17:
					info = this.Properties[18];
					return true;
				case 18:
					info = this.Properties[19];
					return true;
				case 19:
					info = this.Properties[20];
					return true;
				case 20:
					info = this.Properties[22];
					return true;
				case 21:
					info = this.Properties[23];
					return true;
				case 22:
					info = this.Properties[24];
					return true;
				case 23:
					info = this.Properties[25];
					return true;
				case 24:
					info = this.Properties[26];
					return true;
				case 25:
					info = this.Properties[27];
					return true;
				case 26:
					info = this.Properties[28];
					return true;
				case 27:
					info = this.Properties[29];
					return true;
				default:
					if (id == 133)
					{
						info = this.Properties[30];
						return true;
					}
					break;
				}
			}
			else
			{
				if (id == 139)
				{
					info = this.Properties[31];
					return true;
				}
				if (id == 171)
				{
					info = this.Properties[9];
					return true;
				}
				if (id == 174)
				{
					info = this.Properties[21];
					return true;
				}
			}
			info = default(DataModelProperty);
			return false;
		}

		// Token: 0x040098B0 RID: 39088
		public const int ModelId = 3;

		// Token: 0x040098B1 RID: 39089
		private int m_ChapterNumber;

		// Token: 0x040098B2 RID: 39090
		private AdventureChapterState m_ChapterState;

		// Token: 0x040098B3 RID: 39091
		private bool m_AvailableForPurchase;

		// Token: 0x040098B4 RID: 39092
		private bool m_NewlyUnlocked;

		// Token: 0x040098B5 RID: 39093
		private bool m_NewlyCompleted;

		// Token: 0x040098B6 RID: 39094
		private bool m_TimeLocked;

		// Token: 0x040098B7 RID: 39095
		private string m_TimeLockInfoMessage;

		// Token: 0x040098B8 RID: 39096
		private int m_FirstHeroBundledWithChapter;

		// Token: 0x040098B9 RID: 39097
		private int m_SecondHeroBundledWithChapter;

		// Token: 0x040098BA RID: 39098
		private RewardListDataModel m_CompletionRewards;

		// Token: 0x040098BB RID: 39099
		private Reward.Type m_CompletionRewardType;

		// Token: 0x040098BC RID: 39100
		private int m_CompletionRewardId;

		// Token: 0x040098BD RID: 39101
		private int m_CompletionRewardQuantity;

		// Token: 0x040098BE RID: 39102
		private bool m_IsPreviousChapterOwned;

		// Token: 0x040098BF RID: 39103
		private bool m_PlayerOwnsChapter;

		// Token: 0x040098C0 RID: 39104
		private bool m_WantsNewlyUnlockedSequence;

		// Token: 0x040098C1 RID: 39105
		private bool m_CompletionRewardsEarned;

		// Token: 0x040098C2 RID: 39106
		private bool m_CompletionRewardsNewlyEarned;

		// Token: 0x040098C3 RID: 39107
		private int m_WingId;

		// Token: 0x040098C4 RID: 39108
		private bool m_ShowNewlyUnlockedHighlight;

		// Token: 0x040098C5 RID: 39109
		private string m_Name;

		// Token: 0x040098C6 RID: 39110
		private string m_Description;

		// Token: 0x040098C7 RID: 39111
		private string m_UnlockChapterText;

		// Token: 0x040098C8 RID: 39112
		private string m_StoreDescriptionText;

		// Token: 0x040098C9 RID: 39113
		private bool m_DisplayRaidBossHealth;

		// Token: 0x040098CA RID: 39114
		private int m_RaidBossHealthAmount;

		// Token: 0x040098CB RID: 39115
		private bool m_FinalPurchasableChapter;

		// Token: 0x040098CC RID: 39116
		private int m_RaidBossStartingHealthAmount;

		// Token: 0x040098CD RID: 39117
		private bool m_IsAnomalyModeAvailable;

		// Token: 0x040098CE RID: 39118
		private DataModelList<AdventureMissionDataModel> m_Missions = new DataModelList<AdventureMissionDataModel>();

		// Token: 0x040098CF RID: 39119
		private AdventureBookPageMoralAlignment m_MoralAlignment;

		// Token: 0x040098D0 RID: 39120
		private RewardListDataModel m_PurchaseRewards;

		// Token: 0x040098D1 RID: 39121
		private DataModelProperty[] m_properties = new DataModelProperty[]
		{
			new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "chapter_number",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "chapter_state",
				Type = typeof(AdventureChapterState)
			},
			new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "available_for_purchase",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "newly_unlocked",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "newly_completed",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "time_locked",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "time_lock_info_message",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "first_hero_bundled_with_chapter",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "second_hero_bundled_with_chapter",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 171,
				PropertyDisplayName = "completion_rewards",
				Type = typeof(RewardListDataModel)
			},
			new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "DEPRECATED_completion_reward_type",
				Type = typeof(Reward.Type)
			},
			new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "DEPRECATED_completion_reward_id",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "DEPRECATED_completion_reward_quantity",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 12,
				PropertyDisplayName = "is_previous_chapter_owned",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 13,
				PropertyDisplayName = "player_owns_chapter",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 14,
				PropertyDisplayName = "wants_newly_unlocked_sequence",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 15,
				PropertyDisplayName = "completion_rewards_earned",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 16,
				PropertyDisplayName = "completion_rewards_newly_earned",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 17,
				PropertyDisplayName = "wing_id",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 18,
				PropertyDisplayName = "show_newly_unlocked_highlight",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 19,
				PropertyDisplayName = "name",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 174,
				PropertyDisplayName = "description",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 20,
				PropertyDisplayName = "unlock_chapter_text",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 21,
				PropertyDisplayName = "store_description",
				Type = typeof(string)
			},
			new DataModelProperty
			{
				PropertyId = 22,
				PropertyDisplayName = "show_raid_boss_health",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 23,
				PropertyDisplayName = "raid_boss_health_amount",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 24,
				PropertyDisplayName = "final_purchasable_chapter",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 25,
				PropertyDisplayName = "raid_boss_starting_health_amount",
				Type = typeof(int)
			},
			new DataModelProperty
			{
				PropertyId = 26,
				PropertyDisplayName = "is_anomaly_mode_available",
				Type = typeof(bool)
			},
			new DataModelProperty
			{
				PropertyId = 27,
				PropertyDisplayName = "missions",
				Type = typeof(DataModelList<AdventureMissionDataModel>)
			},
			new DataModelProperty
			{
				PropertyId = 133,
				PropertyDisplayName = "moral_alignment",
				Type = typeof(AdventureBookPageMoralAlignment)
			},
			new DataModelProperty
			{
				PropertyId = 139,
				PropertyDisplayName = "purchase_rewards",
				Type = typeof(RewardListDataModel)
			}
		};
	}
}
