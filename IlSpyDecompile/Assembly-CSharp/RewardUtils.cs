using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using Blizzard.T5.AssetManager;
using Hearthstone.Core;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class RewardUtils
{
	public class RewardItemComparer : IComparer<RewardItemDataModel>
	{
		public int Compare(RewardItemDataModel first, RewardItemDataModel second)
		{
			return CompareItemsForSort(first, second);
		}
	}

	public class RewardOwnedItemComparer : IComparer<RewardItemDataModel>
	{
		public int Compare(RewardItemDataModel first, RewardItemDataModel second)
		{
			return CompareOwnedItemsForSort(first, second);
		}
	}

	public static readonly Vector3 REWARD_HIDDEN_SCALE = new Vector3(0.001f, 0.001f, 0.001f);

	public static readonly float REWARD_HIDE_TIME = 0.25f;

	private static readonly AssetReference QUEST_REWARDS_TEXTURE_PAGE_2 = new AssetReference("QuestRewards2.psd:1de88a86bd486434dab6ab887ca40254");

	private static readonly AssetReference QUEST_REWARDS_TEXTURE_PAGE_3 = new AssetReference("QuestRewards3.tif:2ea38c006b55fe6428ec79fa68f5dbe2");

	private static readonly AssetReference ARCANE_ORB_ICON = new AssetReference("Shop_VC2_Arcane_Orb_Icon.tif:b47e50430b8b4554688cc9e385ced3f2");

	public static List<RewardData> GetRewards(List<NetCache.ProfileNotice> notices)
	{
		List<RewardData> rewardDataList = new List<RewardData>();
		foreach (NetCache.ProfileNotice notice in notices)
		{
			RewardData rewardData = null;
			switch (notice.Type)
			{
			case NetCache.ProfileNotice.NoticeType.REWARD_BOOSTER:
			{
				NetCache.ProfileNoticeRewardBooster profileNoticeRewardBooster = notice as NetCache.ProfileNoticeRewardBooster;
				rewardData = new BoosterPackRewardData(profileNoticeRewardBooster.Id, profileNoticeRewardBooster.Count);
				break;
			}
			case NetCache.ProfileNotice.NoticeType.REWARD_CARD:
			{
				NetCache.ProfileNoticeRewardCard profileNoticeRewardCard = notice as NetCache.ProfileNoticeRewardCard;
				rewardData = new CardRewardData(profileNoticeRewardCard.CardID, profileNoticeRewardCard.Premium, profileNoticeRewardCard.Quantity);
				break;
			}
			case NetCache.ProfileNotice.NoticeType.REWARD_DUST:
				if (notice.Origin == NetCache.ProfileNotice.NoticeOrigin.HOF_COMPENSATION)
				{
					continue;
				}
				rewardData = new ArcaneDustRewardData((notice as NetCache.ProfileNoticeRewardDust).Amount);
				break;
			case NetCache.ProfileNotice.NoticeType.REWARD_MOUNT:
				rewardData = new MountRewardData((MountRewardData.MountType)(notice as NetCache.ProfileNoticeRewardMount).MountID);
				break;
			case NetCache.ProfileNotice.NoticeType.REWARD_FORGE:
				rewardData = new ForgeTicketRewardData((notice as NetCache.ProfileNoticeRewardForge).Quantity);
				break;
			case NetCache.ProfileNotice.NoticeType.REWARD_CURRENCY:
			{
				NetCache.ProfileNoticeRewardCurrency profileNoticeRewardCurrency = notice as NetCache.ProfileNoticeRewardCurrency;
				switch (profileNoticeRewardCurrency.CurrencyType)
				{
				case PegasusShared.CurrencyType.CURRENCY_TYPE_ARCANE_ORBS:
					rewardData = CreateArcaneOrbRewardData(profileNoticeRewardCurrency.Amount);
					break;
				case PegasusShared.CurrencyType.CURRENCY_TYPE_GOLD:
					rewardData = new GoldRewardData(profileNoticeRewardCurrency.Amount, DateTime.FromFileTimeUtc(profileNoticeRewardCurrency.Date));
					break;
				}
				break;
			}
			case NetCache.ProfileNotice.NoticeType.REWARD_CARD_BACK:
				rewardData = new CardBackRewardData((notice as NetCache.ProfileNoticeRewardCardBack).CardBackID);
				break;
			case NetCache.ProfileNotice.NoticeType.EVENT:
				rewardData = new EventRewardData((notice as NetCache.ProfileNoticeEvent).EventType);
				break;
			case NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST:
				AddRewardDataForGenericRewardChest(notice as NetCache.ProfileNoticeGenericRewardChest, ref rewardDataList);
				rewardData = null;
				break;
			case NetCache.ProfileNotice.NoticeType.MINI_SET_GRANTED:
				rewardData = new MiniSetRewardData((notice as NetCache.ProfileNoticeMiniSetGranted).MiniSetID);
				break;
			default:
				continue;
			}
			if (rewardData != null)
			{
				SetNoticeAndAddRewardDataToList(notice, ref rewardData, ref rewardDataList);
			}
		}
		return rewardDataList;
	}

	private static void SetNoticeAndAddRewardDataToList(NetCache.ProfileNotice notice, ref RewardData rewardData, ref List<RewardData> rewardDataList)
	{
		rewardData.SetOrigin(notice.Origin, notice.OriginData);
		rewardData.AddNoticeID(notice.NoticeID);
		AddRewardDataToList(rewardData, rewardDataList);
	}

	private static void AddRewardDataForGenericRewardChest(NetCache.ProfileNoticeGenericRewardChest notice, ref List<RewardData> rewardDataList)
	{
		RewardChest rewardChest = notice.RewardChest;
		if (rewardChest != null)
		{
			AddRewardDataForGenericRewardChestBag(notice, rewardChest.Bag1, 1, ref rewardDataList);
			AddRewardDataForGenericRewardChestBag(notice, rewardChest.Bag2, 2, ref rewardDataList);
			AddRewardDataForGenericRewardChestBag(notice, rewardChest.Bag3, 3, ref rewardDataList);
			AddRewardDataForGenericRewardChestBag(notice, rewardChest.Bag4, 4, ref rewardDataList);
			AddRewardDataForGenericRewardChestBag(notice, rewardChest.Bag5, 5, ref rewardDataList);
		}
	}

	private static void AddRewardDataForGenericRewardChestBag(NetCache.ProfileNoticeGenericRewardChest notice, PegasusShared.RewardBag rewardBag, int bagNum, ref List<RewardData> rewardDataList)
	{
		if (rewardBag == null)
		{
			return;
		}
		string text = string.Empty;
		string text2 = string.Empty;
		if (notice.Origin == NetCache.ProfileNotice.NoticeOrigin.GENERIC_REWARD_CHEST_ACHIEVE)
		{
			AchieveDbfRecord record = GameDbf.Achieve.GetRecord((int)notice.OriginData);
			if (record != null)
			{
				text = record.Name;
				text2 = record.Description;
			}
		}
		if ((string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2)) && GameDbf.RewardChest.HasRecord(notice.RewardChestAssetId))
		{
			RewardChestDbfRecord record2 = GameDbf.RewardChest.GetRecord(notice.RewardChestAssetId);
			if (record2.Name != null && string.IsNullOrEmpty(text))
			{
				text = record2.Name.GetString();
			}
			if (record2.Description != null && string.IsNullOrEmpty(text2))
			{
				text2 = record2.Description.GetString();
			}
		}
		RewardData rewardData = Network.ConvertRewardBag(rewardBag);
		if (rewardData != null)
		{
			rewardData.RewardChestAssetId = notice.RewardChestAssetId;
			rewardData.RewardChestBagNum = bagNum;
			rewardData.NameOverride = text;
			rewardData.DescriptionOverride = text2;
			SetNoticeAndAddRewardDataToList(notice, ref rewardData, ref rewardDataList);
		}
	}

	public static void GetViewableRewards(List<RewardData> rewardDataList, HashSet<Assets.Achieve.RewardTiming> rewardTimings, out List<RewardData> rewardsToShow, out List<RewardData> genericRewardChestsToShow, ref List<RewardData> purchasedCardRewardsToShow, ref List<Achievement> completedQuests)
	{
		rewardsToShow = new List<RewardData>();
		genericRewardChestsToShow = new List<RewardData>();
		if (completedQuests == null)
		{
			completedQuests = new List<Achievement>();
		}
		foreach (RewardData rewardData in rewardDataList)
		{
			Log.Achievements.Print("RewardUtils.GetViewableRewards() - processing reward {0}", rewardData);
			if (NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT == rewardData.Origin)
			{
				Achievement completedQuest = AchieveManager.Get().GetAchievement((int)rewardData.OriginData);
				if (completedQuest == null)
				{
					continue;
				}
				List<long> noticeIDs = rewardData.GetNoticeIDs();
				Achievement achievement = completedQuests.Find((Achievement obj) => completedQuest.ID == obj.ID);
				if (achievement != null)
				{
					foreach (long item in noticeIDs)
					{
						achievement.AddRewardNoticeID(item);
					}
					continue;
				}
				foreach (long item2 in noticeIDs)
				{
					completedQuest.AddRewardNoticeID(item2);
				}
				if (rewardTimings.Contains(completedQuest.RewardTiming))
				{
					completedQuests.Add(completedQuest);
				}
				continue;
			}
			if (rewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.GENERIC_REWARD_CHEST_ACHIEVE)
			{
				Achievement achievement2 = AchieveManager.Get().GetAchievement((int)rewardData.OriginData);
				if (achievement2 == null || rewardTimings.Contains(achievement2.RewardTiming))
				{
					genericRewardChestsToShow.Add(rewardData);
				}
				continue;
			}
			if (rewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.GENERIC_REWARD_CHEST)
			{
				if (rewardData.Origin != NetCache.ProfileNotice.NoticeOrigin.NOTICE_ORIGIN_DUELS)
				{
					genericRewardChestsToShow.Add(rewardData);
				}
				continue;
			}
			bool flag = false;
			switch (rewardData.RewardType)
			{
			case Reward.Type.CARD:
			{
				CardRewardData cardRewardData = rewardData as CardRewardData;
				if (cardRewardData.CardID.Equals("HERO_08") && cardRewardData.Premium == TAG_PREMIUM.NORMAL)
				{
					flag = false;
					rewardData.AcknowledgeNotices();
					CollectionManager.Get().AddCardReward(cardRewardData, markAsNew: false);
				}
				else if (NetCache.ProfileNotice.NoticeOrigin.FROM_PURCHASE == rewardData.Origin || NetCache.ProfileNotice.NoticeOrigin.OUT_OF_BAND_LICENSE == rewardData.Origin)
				{
					flag = false;
					if (StoreManager.Get() != null && StoreManager.Get().WillStoreDisplayNotice(rewardData.Origin, NetCache.ProfileNotice.NoticeType.REWARD_CARD, rewardData.OriginData))
					{
						rewardData.AcknowledgeNotices();
					}
					else if (purchasedCardRewardsToShow != null)
					{
						purchasedCardRewardsToShow.Add(rewardData);
					}
				}
				else
				{
					flag = true;
				}
				break;
			}
			case Reward.Type.MINI_SET:
				flag = false;
				if (purchasedCardRewardsToShow != null)
				{
					purchasedCardRewardsToShow.Add(rewardData);
				}
				break;
			case Reward.Type.ARCANE_DUST:
			case Reward.Type.BOOSTER_PACK:
			case Reward.Type.GOLD:
				flag = true;
				break;
			case Reward.Type.FORGE_TICKET:
			{
				bool flag2 = false;
				if (NetCache.ProfileNotice.NoticeOrigin.BLIZZCON == rewardData.Origin && 2013 == rewardData.OriginData)
				{
					flag2 = true;
				}
				if (rewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.OUT_OF_BAND_LICENSE)
				{
					Log.Achievements.Print($"RewardUtils.GetViewableRewards(): auto-acking notices for out of band license reward {rewardData}");
					flag2 = true;
				}
				if (flag2)
				{
					rewardData.AcknowledgeNotices();
				}
				flag = false;
				break;
			}
			case Reward.Type.CARD_BACK:
				flag = NetCache.ProfileNotice.NoticeOrigin.SEASON != rewardData.Origin;
				break;
			}
			if (flag)
			{
				rewardsToShow.Add(rewardData);
			}
		}
	}

	public static void SortRewards(ref List<Reward> rewards)
	{
		if (rewards == null)
		{
			return;
		}
		rewards.Sort(delegate(Reward r1, Reward r2)
		{
			if (r1.RewardType == r2.RewardType)
			{
				if (r1.RewardType != Reward.Type.CARD)
				{
					return 0;
				}
				CardRewardData cardRewardData = r1.Data as CardRewardData;
				CardRewardData cardRewardData2 = r2.Data as CardRewardData;
				EntityDef entityDef = DefLoader.Get().GetEntityDef(cardRewardData.CardID);
				EntityDef entityDef2 = DefLoader.Get().GetEntityDef(cardRewardData2.CardID);
				bool flag = entityDef.IsHeroSkin();
				bool flag2 = entityDef2.IsHeroSkin();
				if (flag == flag2)
				{
					return 0;
				}
				if (!flag)
				{
					return 1;
				}
				return -1;
			}
			if (Reward.Type.CARD_BACK == r1.RewardType)
			{
				return -1;
			}
			if (Reward.Type.CARD_BACK == r2.RewardType)
			{
				return 1;
			}
			if (Reward.Type.CARD == r1.RewardType)
			{
				return -1;
			}
			if (Reward.Type.CARD == r2.RewardType)
			{
				return 1;
			}
			if (Reward.Type.BOOSTER_PACK == r1.RewardType)
			{
				return -1;
			}
			if (Reward.Type.BOOSTER_PACK == r2.RewardType)
			{
				return 1;
			}
			if (Reward.Type.MOUNT == r1.RewardType)
			{
				return -1;
			}
			return (Reward.Type.MOUNT == r2.RewardType) ? 1 : 0;
		});
	}

	public static void AddRewardDataToList(RewardData newRewardData, List<RewardData> existingRewardDataList)
	{
		CardRewardData duplicateCardDataReward = GetDuplicateCardDataReward(newRewardData, existingRewardDataList);
		if (duplicateCardDataReward == null)
		{
			existingRewardDataList.Add(newRewardData);
			return;
		}
		CardRewardData other = newRewardData as CardRewardData;
		duplicateCardDataReward.Merge(other);
	}

	public static bool GetNextHeroLevelRewardText(TAG_CLASS heroClass, int heroLevel, int totalLevel, out string nextRewardTitle, out string nextRewardDescription)
	{
		int nextRewardLevel;
		RewardData nextHeroLevelReward = FixedRewardsMgr.Get().GetNextHeroLevelReward(heroClass, heroLevel, out nextRewardLevel);
		int nextRewardLevel2;
		RewardData nextTotalLevelReward = FixedRewardsMgr.Get().GetNextTotalLevelReward(totalLevel, out nextRewardLevel2);
		nextRewardTitle = string.Empty;
		nextRewardDescription = string.Empty;
		bool flag = nextRewardLevel > 0;
		bool flag2 = nextRewardLevel2 > 0;
		if (!flag && !flag2)
		{
			return false;
		}
		int num = 0;
		int num2 = nextRewardLevel - heroLevel;
		int num3 = nextRewardLevel2 - totalLevel;
		if (flag && (!flag2 || num2 <= num3))
		{
			num = nextRewardLevel;
			nextRewardDescription = GetRewardText(nextHeroLevelReward);
		}
		if (flag && flag2 && num2 == num3)
		{
			nextRewardDescription += "\n";
		}
		if (flag2 && (!flag || num3 <= num2))
		{
			num = heroLevel + num3;
			nextRewardDescription += GetRewardText(nextTotalLevelReward);
		}
		if (num > 0)
		{
			nextRewardTitle = GameStrings.Format("GLOBAL_HERO_LEVEL_NEXT_REWARD_TITLE", num);
		}
		return nextRewardTitle != string.Empty;
	}

	public static string GetRewardText(RewardData rewardData)
	{
		if (rewardData == null)
		{
			return string.Empty;
		}
		switch (rewardData.RewardType)
		{
		case Reward.Type.CARD:
		{
			CardRewardData cardRewardData = rewardData as CardRewardData;
			EntityDef entityDef = DefLoader.Get().GetEntityDef(cardRewardData.CardID);
			if (cardRewardData.Premium == TAG_PREMIUM.GOLDEN)
			{
				return GameStrings.Format("GLOBAL_HERO_LEVEL_REWARD_GOLDEN_CARD", GameStrings.Get("GLOBAL_COLLECTION_GOLDEN"), entityDef.GetName());
			}
			return entityDef.GetName();
		}
		case Reward.Type.BOOSTER_PACK:
		{
			BoosterPackRewardData boosterPackRewardData = rewardData as BoosterPackRewardData;
			string text = GameDbf.Booster.GetRecord(boosterPackRewardData.Id).Name;
			return GameStrings.Format("GLOBAL_HERO_LEVEL_REWARD_BOOSTER", text);
		}
		case Reward.Type.ARCANE_DUST:
		{
			ArcaneDustRewardData arcaneDustRewardData = rewardData as ArcaneDustRewardData;
			return GameStrings.Format("GLOBAL_HERO_LEVEL_REWARD_ARCANE_DUST", arcaneDustRewardData.Amount);
		}
		case Reward.Type.GOLD:
		{
			GoldRewardData goldRewardData = rewardData as GoldRewardData;
			return GameStrings.Format("GLOBAL_HERO_LEVEL_REWARD_GOLD", goldRewardData.Amount);
		}
		default:
			return "UNKNOWN";
		}
	}

	public static bool ShowReward(UserAttentionBlocker blocker, Reward reward, bool updateCacheValues, Vector3 rewardPunchScale, Vector3 rewardScale, AnimationUtil.DelOnShownWithPunch callback, object callbackData)
	{
		return ShowReward_Internal(blocker, reward, updateCacheValues, rewardPunchScale, rewardScale, string.Empty, null, callback, callbackData);
	}

	public static bool ShowReward(UserAttentionBlocker blocker, Reward reward, bool updateCacheValues, Vector3 rewardPunchScale, Vector3 rewardScale, string callbackName = "", object callbackData = null, GameObject callbackGO = null)
	{
		return ShowReward_Internal(blocker, reward, updateCacheValues, rewardPunchScale, rewardScale, callbackName, callbackGO, null, callbackData);
	}

	public static void SetupRewardIcon(RewardData rewardData, Renderer rewardRenderer, UberText rewardAmountLabel, out float amountToScaleReward, bool doubleGold = false)
	{
		UnityEngine.Vector2 mainTextureOffset = UnityEngine.Vector2.zero;
		amountToScaleReward = 1f;
		rewardAmountLabel.gameObject.SetActive(value: false);
		Material rewardMaterial = ((rewardRenderer != null) ? rewardRenderer.GetMaterial() : null);
		AssetHandleCallback<Texture> callback = delegate(AssetReference assetRef, AssetHandle<Texture> texture, object loadTextureCbData)
		{
			if (rewardRenderer != null)
			{
				HearthstoneServices.Get<DisposablesCleaner>()?.Attach(rewardRenderer, texture);
				if (rewardMaterial != null)
				{
					rewardMaterial.mainTexture = texture;
				}
			}
			else
			{
				texture?.Dispose();
			}
		};
		switch (rewardData.RewardType)
		{
		case Reward.Type.ARCANE_DUST:
		{
			AssetLoader.Get().LoadAsset(QUEST_REWARDS_TEXTURE_PAGE_2, callback);
			mainTextureOffset = new UnityEngine.Vector2(0.25f, 0f);
			ArcaneDustRewardData arcaneDustRewardData = rewardData as ArcaneDustRewardData;
			rewardAmountLabel.Text = arcaneDustRewardData.Amount.ToString();
			rewardAmountLabel.gameObject.SetActive(value: true);
			break;
		}
		case Reward.Type.BOOSTER_PACK:
		{
			BoosterPackRewardData boosterPackRewardData = rewardData as BoosterPackRewardData;
			BoosterDbfRecord record = GameDbf.Booster.GetRecord(boosterPackRewardData.Id);
			if (!string.IsNullOrEmpty(record.QuestIconPath))
			{
				AssetLoader.Get().LoadAsset(record.QuestIconPath, callback);
				mainTextureOffset = new UnityEngine.Vector2((float)record.QuestIconOffsetX, (float)record.QuestIconOffsetY);
				break;
			}
			Log.Achievements.PrintWarning("Booster Record ID = {0} does not have proper reward icon data", boosterPackRewardData.Id);
			mainTextureOffset = new UnityEngine.Vector2(0f, 0.75f);
			if (boosterPackRewardData.Id == 11 && boosterPackRewardData.Count > 1)
			{
				mainTextureOffset = new UnityEngine.Vector2(0f, 0.5f);
			}
			break;
		}
		case Reward.Type.CARD:
		{
			CardRewardData cardRewardData = rewardData as CardRewardData;
			mainTextureOffset = ((!(cardRewardData.CardID == "HERO_03a")) ? ((!(cardRewardData.CardID == "HERO_06a")) ? new UnityEngine.Vector2(0.5f, 0f) : new UnityEngine.Vector2(0.75f, 0.25f)) : new UnityEngine.Vector2(0.75f, 0.5f));
			break;
		}
		case Reward.Type.FORGE_TICKET:
			mainTextureOffset = new UnityEngine.Vector2(0.75f, 0.75f);
			amountToScaleReward = 1.46881f;
			break;
		case Reward.Type.GOLD:
		{
			mainTextureOffset = new UnityEngine.Vector2(0.25f, 0.75f);
			long num = ((GoldRewardData)rewardData).Amount;
			if (doubleGold)
			{
				num *= 2;
			}
			rewardAmountLabel.Text = num.ToString();
			rewardAmountLabel.gameObject.SetActive(value: true);
			break;
		}
		case Reward.Type.ARCANE_ORBS:
			AssetLoader.Get().LoadAsset(ARCANE_ORB_ICON, callback);
			rewardAmountLabel.Text = ((SimpleRewardData)rewardData).Amount.ToString();
			rewardAmountLabel.gameObject.SetActive(value: true);
			rewardMaterial.mainTextureScale = new UnityEngine.Vector2(4f, 4f);
			break;
		}
		rewardMaterial.mainTextureOffset = mainTextureOffset;
	}

	public static void ShowQuestChestReward(string title, string desc, List<RewardData> rewards, Transform rewardBone, Action doneCallback, bool fromNotice = false, int noticeID = -1, string prefab = "RewardChest_Lock.prefab:06ffa33e82036694e8cacb96aa7b48e8")
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			ChestRewardDisplay componentInChildren = go.GetComponentInChildren<ChestRewardDisplay>();
			componentInChildren.RegisterDoneCallback(doneCallback);
			GameUtils.SetParent(componentInChildren.m_parent.transform, rewardBone);
			if (!componentInChildren.ShowRewards_Quest(rewards, rewardBone, title, desc, fromNotice, noticeID))
			{
				UnityEngine.Object.Destroy(go);
			}
		};
		AssetLoader.Get().InstantiatePrefab(prefab, callback);
	}

	public static void ShowTavernBrawlRewards(int wins, List<RewardData> rewards, Transform rewardBone, Action doneCallback, bool fromNotice = false, NetCache.ProfileNoticeTavernBrawlRewards notice = null)
	{
		TavernBrawlMode num = (fromNotice ? notice.Mode : TavernBrawlManager.Get().CurrentSeasonBrawlMode);
		long noticeID = notice?.NoticeID ?? 0;
		if (num == TavernBrawlMode.TB_MODE_NORMAL)
		{
			ShowSessionTavernBrawlRewards(wins, rewards, rewardBone, doneCallback, fromNotice, noticeID);
		}
		else
		{
			ShowHeroicSessionTavernBrawlRewards(wins, rewards, rewardBone, doneCallback, fromNotice, noticeID);
		}
	}

	public static void ShowSessionTavernBrawlRewards(int wins, List<RewardData> rewards, Transform rewardBone, Action doneCallback, bool fromNotice = false, long noticeID = -1L)
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			ChestRewardDisplay componentInChildren = go.GetComponentInChildren<ChestRewardDisplay>();
			componentInChildren.RegisterDoneCallback(doneCallback);
			GameUtils.SetParent(componentInChildren.m_parent.transform, rewardBone);
			if (!componentInChildren.ShowRewards_TavernBrawl(wins, rewards, rewardBone, fromNotice, noticeID))
			{
				UnityEngine.Object.Destroy(go);
			}
		};
		AssetLoader.Get().InstantiatePrefab("RewardChest_Lock.prefab:06ffa33e82036694e8cacb96aa7b48e8", callback);
	}

	public static void ShowLeaguePromotionRewards(int leagueId, List<RewardData> rewards, Transform rewardBone, Action doneCallback, bool fromNotice = false, long noticeID = -1L)
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			ChestRewardDisplay componentInChildren = go.GetComponentInChildren<ChestRewardDisplay>();
			componentInChildren.RegisterDoneCallback(doneCallback);
			GameUtils.SetParent(componentInChildren.m_parent.transform, rewardBone);
			if (!componentInChildren.ShowRewards_LeaguePromotion(leagueId, rewards, rewardBone, fromNotice, noticeID))
			{
				UnityEngine.Object.Destroy(go);
			}
		};
		AssetLoader.Get().InstantiatePrefab("RewardChest_Lock.prefab:06ffa33e82036694e8cacb96aa7b48e8", callback);
	}

	public static void ShowHeroicSessionTavernBrawlRewards(int wins, List<RewardData> rewards, Transform rewardBone, Action doneCallback, bool fromNotice = false, long noticeID = -1L)
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			HeroicBrawlRewardDisplay component = go.GetComponent<HeroicBrawlRewardDisplay>();
			component.RegisterDoneCallback(doneCallback);
			TransformUtil.AttachAndPreserveLocalTransform(component.transform, rewardBone);
			component.ShowRewards(wins, rewards, fromNotice, noticeID);
		};
		AssetLoader.Get().InstantiatePrefab("HeroicBrawlReward.prefab:8f49f1fcb5ca4485d9b6b22993e1b1ab", callback);
	}

	public static RewardChest GenerateTavernBrawlRewardChest_CHEAT(int wins, TavernBrawlMode mode)
	{
		RewardChest rewardChest = new RewardChest();
		PegasusShared.RewardBag rewardBag = new PegasusShared.RewardBag();
		rewardBag.RewardBooster = new ProfileNoticeRewardBooster();
		rewardBag.RewardBooster.BoosterType = 1;
		int boosterCount = 0;
		int num = 0;
		switch (wins)
		{
		case 0:
			boosterCount = 1;
			break;
		case 1:
			boosterCount = 2;
			break;
		case 2:
			boosterCount = 4;
			break;
		case 3:
			boosterCount = 4;
			num = 120;
			break;
		case 4:
			boosterCount = 5;
			num = 230;
			break;
		case 5:
			boosterCount = 6;
			num = 260;
			break;
		case 6:
			boosterCount = 7;
			num = 290;
			break;
		case 7:
			boosterCount = 8;
			num = 320;
			break;
		case 8:
			boosterCount = 9;
			num = 350;
			break;
		case 9:
			boosterCount = 14;
			num = 500;
			break;
		case 10:
			boosterCount = 15;
			num = 550;
			break;
		case 11:
			boosterCount = 20;
			num = 600;
			break;
		case 12:
			boosterCount = 50;
			num = 1000;
			break;
		}
		rewardBag.RewardBooster.BoosterCount = boosterCount;
		rewardChest.Bag.Add(rewardBag);
		if (wins > 2)
		{
			PegasusShared.RewardBag rewardBag2 = new PegasusShared.RewardBag();
			rewardBag2.RewardDust = new ProfileNoticeRewardDust();
			rewardBag2.RewardDust.Amount = num + UnityEngine.Random.Range(-4, 4) * 5;
			PegasusShared.RewardBag rewardBag3 = new PegasusShared.RewardBag();
			rewardBag3.RewardGold = new ProfileNoticeRewardCurrency();
			rewardBag3.RewardGold.Amount = num + UnityEngine.Random.Range(-4, 4) * 5;
			rewardChest.Bag.Add(rewardBag3);
			rewardChest.Bag.Add(rewardBag2);
		}
		if (wins > 9)
		{
			PegasusShared.RewardBag rewardBag4 = new PegasusShared.RewardBag();
			rewardBag4.RewardCard = new ProfileNoticeRewardCard();
			rewardBag4.RewardCard.Card = new PegasusShared.CardDef();
			rewardBag4.RewardCard.Card.Premium = 1;
			rewardBag4.RewardCard.Card.Asset = 834;
			rewardChest.Bag.Add(rewardBag4);
		}
		if (wins > 10)
		{
			PegasusShared.RewardBag rewardBag5 = new PegasusShared.RewardBag();
			rewardBag5.RewardCard = new ProfileNoticeRewardCard();
			rewardBag5.RewardCard.Card = new PegasusShared.CardDef();
			rewardBag5.RewardCard.Card.Premium = 1;
			rewardBag5.RewardCard.Card.Asset = 374;
			rewardChest.Bag.Add(rewardBag5);
		}
		if (wins > 11 && mode == TavernBrawlMode.TB_MODE_HEROIC)
		{
			PegasusShared.RewardBag rewardBag6 = new PegasusShared.RewardBag();
			rewardBag6.RewardCard = new ProfileNoticeRewardCard();
			rewardBag6.RewardCard.Card = new PegasusShared.CardDef();
			rewardBag6.RewardCard.Card.Premium = 1;
			rewardBag6.RewardCard.Card.Asset = 640;
			rewardChest.Bag.Add(rewardBag6);
		}
		return rewardChest;
	}

	public static void SetQuestTileNameLinePosition(GameObject nameLine, UberText questName, float padding)
	{
		bool num = questName.isHidden();
		if (num)
		{
			questName.Show();
		}
		TransformUtil.SetPoint(nameLine, Anchor.TOP, questName, Anchor.BOTTOM);
		nameLine.transform.localPosition = new Vector3(nameLine.transform.localPosition.x, nameLine.transform.localPosition.y, nameLine.transform.localPosition.z + padding);
		if (num)
		{
			questName.Hide();
		}
	}

	public static RewardChestContentsDbfRecord GetRewardChestContents(int rewardChestAssetId, int rewardLevel)
	{
		if (GameDbf.RewardChest.HasRecord(rewardChestAssetId))
		{
			return GameDbf.RewardChestContents.GetRecord((RewardChestContentsDbfRecord r) => r.RewardChestId == rewardChestAssetId && r.RewardLevel == rewardLevel);
		}
		return null;
	}

	public static List<RewardData> GetRewardDataFromRewardChestAsset(int rewardChestAssetId, int rewardLevel)
	{
		List<RewardData> rewardData = new List<RewardData>();
		RewardChestContentsDbfRecord rewardChestContents = GetRewardChestContents(rewardChestAssetId, rewardLevel);
		if (rewardChestContents != null)
		{
			int seasonId = 0;
			AddRewardDataStubForBag(rewardChestContents.Bag1, seasonId, ref rewardData);
			AddRewardDataStubForBag(rewardChestContents.Bag2, seasonId, ref rewardData);
			AddRewardDataStubForBag(rewardChestContents.Bag3, seasonId, ref rewardData);
			AddRewardDataStubForBag(rewardChestContents.Bag4, seasonId, ref rewardData);
			AddRewardDataStubForBag(rewardChestContents.Bag5, seasonId, ref rewardData);
		}
		return rewardData;
	}

	public static void AddRewardDataStubForBag(int bagId, int seasonId, ref List<RewardData> rewardData)
	{
		RewardBagDbfRecord record = GameDbf.RewardBag.GetRecord((RewardBagDbfRecord r) => r.BagId == bagId);
		if (record == null)
		{
			return;
		}
		switch (record.Reward.ToString().ToLower())
		{
		case "random_card":
			rewardData.Add(new RandomCardRewardData(GetRarityForRandomCardReward(record.RewardData), TAG_PREMIUM.NORMAL));
			break;
		case "com":
			rewardData.Add(new RandomCardRewardData(TAG_RARITY.COMMON, TAG_PREMIUM.NORMAL, record.Base));
			break;
		case "gcom":
			rewardData.Add(new RandomCardRewardData(TAG_RARITY.COMMON, TAG_PREMIUM.GOLDEN, record.Base));
			break;
		case "rare":
			rewardData.Add(new RandomCardRewardData(TAG_RARITY.RARE, TAG_PREMIUM.NORMAL, record.Base));
			break;
		case "grare":
			rewardData.Add(new RandomCardRewardData(TAG_RARITY.RARE, TAG_PREMIUM.GOLDEN, record.Base));
			break;
		case "epic":
			rewardData.Add(new RandomCardRewardData(TAG_RARITY.EPIC, TAG_PREMIUM.NORMAL, record.Base));
			break;
		case "gepic":
			rewardData.Add(new RandomCardRewardData(TAG_RARITY.EPIC, TAG_PREMIUM.GOLDEN, record.Base));
			break;
		case "leg":
			rewardData.Add(new RandomCardRewardData(TAG_RARITY.LEGENDARY, TAG_PREMIUM.NORMAL, record.Base));
			break;
		case "gleg":
			rewardData.Add(new RandomCardRewardData(TAG_RARITY.LEGENDARY, TAG_PREMIUM.GOLDEN, record.Base));
			break;
		case "ranked_season_reward_pack":
		{
			int rankedRewardBoosterIdForSeasonId = RankMgr.Get().GetRankedRewardBoosterIdForSeasonId(seasonId);
			rewardData.Add(new BoosterPackRewardData(rankedRewardBoosterIdForSeasonId, record.Base));
			break;
		}
		case "latest_pack":
			rewardData.Add(new BoosterPackRewardData(record.RewardData, record.Base, record.BagId));
			break;
		case "specific_pack":
			rewardData.Add(new BoosterPackRewardData(record.RewardData, record.Base));
			break;
		case "gold":
			rewardData.Add(new GoldRewardData(record.Base));
			break;
		case "dust":
			rewardData.Add(new ArcaneDustRewardData(record.Base));
			break;
		case "reward_chest_contents":
		{
			RewardChestContentsDbfRecord record2 = GameDbf.RewardChestContents.GetRecord(record.RewardData);
			if (record2 == null)
			{
				Log.All.PrintWarning("No reward chest contents of id {0} found on client for random card reward", record.RewardData);
			}
			else
			{
				ProcessRewardChestContents(record2, seasonId, ref rewardData);
			}
			break;
		}
		}
	}

	public static void ProcessRewardChestContents(RewardChestContentsDbfRecord rewardChestContents, int seasonId, ref List<RewardData> rewardData)
	{
		foreach (int bag in new List<int> { rewardChestContents.Bag1, rewardChestContents.Bag2, rewardChestContents.Bag3, rewardChestContents.Bag4, rewardChestContents.Bag5 })
		{
			if (bag != 0)
			{
				RewardBagDbfRecord record = GameDbf.RewardBag.GetRecord((RewardBagDbfRecord r) => r.BagId == bag);
				if (record != null && !(record.Reward.ToString().ToLower() == "reward_chest_contents"))
				{
					AddRewardDataStubForBag(bag, seasonId, ref rewardData);
				}
			}
		}
	}

	public static void ShowRewardBoxes(List<RewardData> rewards, Action doneCallback, Transform bone = null)
	{
		AssetLoader.GameObjectCallback callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			if (SoundManager.Get() != null)
			{
				SoundManager.Get().LoadAndPlay("card_turn_over_legendary.prefab:a8140f686bff601459e954bc23de35e0");
			}
			RewardBoxesDisplay component = go.GetComponent<RewardBoxesDisplay>();
			component.SetRewards(rewards);
			component.m_playBoxFlyoutSound = false;
			component.SetLayer(GameLayer.IgnoreFullScreenEffects);
			component.RegisterDoneCallback(doneCallback);
			if (bone != null)
			{
				component.transform.position = bone.position;
				component.transform.localRotation = bone.localRotation;
				component.transform.localScale = bone.localScale;
			}
			component.AnimateRewards();
		};
		AssetLoader.Get().LoadGameObject("RewardBoxes.prefab:f136fead3d6a148c6801f1e3bd2e8267", callback);
	}

	public static TAG_RARITY GetRarityForRandomCardReward(int boosterCardSetId)
	{
		BoosterCardSetDbfRecord record = GameDbf.BoosterCardSet.GetRecord(boosterCardSetId);
		if (record == null)
		{
			Log.All.PrintWarning("No BoosterCardSet of id [{0}] found)", boosterCardSetId);
			return TAG_RARITY.INVALID;
		}
		SubsetDbfRecord subsetRecord = record.SubsetRecord;
		if (subsetRecord == null)
		{
			Log.All.PrintWarning("No subset of id {0} found on client for random card reward on boosterCardSet {1}", record.SubsetId, record.ID);
			return TAG_RARITY.INVALID;
		}
		IEnumerable<SubsetRuleDbfRecord> source = subsetRecord.Rules.Where((SubsetRuleDbfRecord r) => r.Tag == 203);
		SubsetRuleDbfRecord subsetRuleDbfRecord = source.FirstOrDefault();
		if (source.Count() != 1 || subsetRuleDbfRecord == null || subsetRuleDbfRecord.RuleIsNot || subsetRuleDbfRecord.MinValue != subsetRuleDbfRecord.MaxValue)
		{
			Log.All.PrintWarning("Random card display requires exactly one rarity rule to specify a single rarity (subset id [{0}])", subsetRecord.ID);
			return TAG_RARITY.INVALID;
		}
		return (TAG_RARITY)subsetRuleDbfRecord.MinValue;
	}

	public static UserAttentionBlocker GetUserAttentionBlockerForReward(NetCache.ProfileNotice.NoticeOrigin origin, long originData)
	{
		if (origin != NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT && origin != NetCache.ProfileNotice.NoticeOrigin.GENERIC_REWARD_CHEST_ACHIEVE)
		{
			return UserAttentionBlocker.NONE;
		}
		return (UserAttentionBlocker)(GameDbf.Achieve.GetRecord((int)originData)?.AttentionBlocker ?? Assets.Achieve.AttentionBlocker.NONE);
	}

	private static bool ShowReward_Internal(UserAttentionBlocker blocker, Reward reward, bool updateCacheValues, Vector3 rewardPunchScale, Vector3 rewardScale, string gameObjectCallbackName, GameObject callbackGO, AnimationUtil.DelOnShownWithPunch onShowPunchCallback, object callbackData)
	{
		if (reward == null)
		{
			return false;
		}
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "RewardUtils.ShowReward:" + ((reward == null || reward.Data == null) ? "null" : string.Concat(reward.Data.Origin, ":", reward.Data.OriginData, ":", reward.Data.RewardType))))
		{
			return false;
		}
		Log.Achievements.Print("RewardUtils: Showing Reward: reward={0} reward.Data={1}", reward, reward.Data);
		AnimationUtil.ShowWithPunch(reward.gameObject, REWARD_HIDDEN_SCALE, rewardPunchScale, rewardScale, gameObjectCallbackName, noFade: true, callbackGO, callbackData, onShowPunchCallback);
		reward.Show(updateCacheValues);
		ShowInnkeeperQuoteForReward(reward);
		return true;
	}

	private static CardRewardData GetDuplicateCardDataReward(RewardData newRewardData, List<RewardData> existingRewardData)
	{
		if (!(newRewardData is CardRewardData))
		{
			return null;
		}
		CardRewardData newCardRewardData = newRewardData as CardRewardData;
		return existingRewardData.Find(delegate(RewardData obj)
		{
			if (!(obj is CardRewardData))
			{
				return false;
			}
			CardRewardData cardRewardData = obj as CardRewardData;
			if (!cardRewardData.CardID.Equals(newCardRewardData.CardID))
			{
				return false;
			}
			if (!cardRewardData.Premium.Equals(newCardRewardData.Premium))
			{
				return false;
			}
			return cardRewardData.Origin.Equals(newCardRewardData.Origin) && cardRewardData.OriginData.Equals(newCardRewardData.OriginData);
		}) as CardRewardData;
	}

	private static void ShowInnkeeperQuoteForReward(Reward reward)
	{
		if (reward == null || Reward.Type.CARD != reward.RewardType)
		{
			return;
		}
		switch ((reward.Data as CardRewardData).InnKeeperLine)
		{
		case CardRewardData.InnKeeperTrigger.CORE_CLASS_SET_COMPLETE:
		{
			Notification innkeeperQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_BASIC_DONE1_11"), "VO_INNKEEPER_BASIC_DONE1_11.prefab:9b8f8ab262305c54dbb6c847ac8b1fdb");
			if (!Options.Get().GetBool(Option.HAS_SEEN_ALL_BASIC_CLASS_CARDS_COMPLETE, defaultVal: false))
			{
				Processor.RunCoroutine(NotifyOfExpertPacksNeeded(innkeeperQuote));
			}
			break;
		}
		case CardRewardData.InnKeeperTrigger.SECOND_REWARD_EVER:
			if (!Options.Get().GetBool(Option.HAS_BEEN_NUDGED_TO_CM, defaultVal: false))
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_NUDGE_CM_X"), "VO_INNKEEPER2_NUDGE_COLLECTION_10.prefab:b20c7d803cf82fb46830cba5d4bda11e");
				Options.Get().SetBool(Option.HAS_BEEN_NUDGED_TO_CM, val: true);
			}
			break;
		}
	}

	private static IEnumerator NotifyOfExpertPacksNeeded(Notification innkeeperQuote)
	{
		while (innkeeperQuote.GetAudio() == null)
		{
			yield return null;
		}
		yield return new WaitForSeconds(innkeeperQuote.GetAudio().clip.length);
		NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_BASIC_DONE2_12"), "VO_INNKEEPER_BASIC_DONE2_12.prefab:b20f6a03438c5b440a2963095330589c");
		Options.Get().SetBool(Option.HAS_SEEN_ALL_BASIC_CLASS_CARDS_COMPLETE, val: true);
	}

	public static SimpleRewardData CreateArcaneOrbRewardData(int amount)
	{
		return new SimpleRewardData(Reward.Type.ARCANE_ORBS, amount)
		{
			RewardHeadlineText = GameStrings.Get("GLOBAL_REWARD_ARCANE_ORBS_HEADLINE")
		};
	}

	public static DeckRewardData CreateDeckRewardData(int deckId, int classId)
	{
		return new DeckRewardData(deckId, classId);
	}

	public static bool AttemptDeckTemplateLookupFromSellableDeckId(int deckId, out DeckTemplateDbfRecord deckTemplateRecord)
	{
		deckTemplateRecord = null;
		List<SellableDeckDbfRecord> records = GameDbf.SellableDeck.GetRecords((SellableDeckDbfRecord r) => r.DeckTemplateRecord != null && r.DeckTemplateRecord.DeckId == deckId);
		if (records.Count == 0)
		{
			Log.Store.PrintWarning("[RewardUtils.AttemptDeckTemplateLookupFromSellableDeckId] Failed to find DB record for deck reward! (ID {0})", deckId);
			return false;
		}
		if (records.Count > 1)
		{
			Log.Store.PrintWarning("[RewardUtils.AttemptDeckTemplateLookupFromSellableDeckId] Found multiple rewardable deck records that grant the same deck! (ID {0})", deckId);
			return false;
		}
		SellableDeckDbfRecord sellableDeckDbfRecord = records[0];
		if (sellableDeckDbfRecord.DeckTemplateRecord == null || sellableDeckDbfRecord.DeckTemplateRecord.DeckRecord == null)
		{
			Log.Store.PrintWarning("[RewardUtils.AttemptDeckTemplateLookupFromSellableDeckId] The DB record {0} for deck reward does NOT have a deck template with a valid deck record!", sellableDeckDbfRecord.ID);
			return false;
		}
		deckTemplateRecord = sellableDeckDbfRecord.DeckTemplateRecord;
		return true;
	}

	public static bool CopyDeckTemplateRecordToClipboard(DeckTemplateDbfRecord deckTemplateDBfRecord, bool usePremiumCardsFromCollection = true)
	{
		ShareableDeck shareableDeckFromTemplateRecord = CollectionManager.GetShareableDeckFromTemplateRecord(deckTemplateDBfRecord, usePremiumCardsFromCollection);
		if (shareableDeckFromTemplateRecord == null)
		{
			Log.Store.PrintWarning("[RewardUtils.CopyDeckTemplateRecordToClipboard] Failed to get a sharable deck for deck template record {0}!", deckTemplateDBfRecord.ID);
			return false;
		}
		ClipboardUtils.CopyToClipboard(shareableDeckFromTemplateRecord.Serialize());
		return true;
	}

	public static RewardItemDataModel RewardDataToRewardItemDataModel(RewardData rewardData)
	{
		TAG_RARITY rarity = TAG_RARITY.INVALID;
		TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
		RewardItemDataModel rewardItemDataModel = null;
		switch (rewardData.RewardType)
		{
		case Reward.Type.ARCANE_DUST:
		{
			ArcaneDustRewardData arcaneDustRewardData = rewardData as ArcaneDustRewardData;
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.DUST,
				Quantity = arcaneDustRewardData.Amount
			};
			break;
		}
		case Reward.Type.CARD:
		{
			CardRewardData cardRewardData = rewardData as CardRewardData;
			RewardItemDataModel rewardItemDataModel2 = new RewardItemDataModel
			{
				ItemType = RewardItemType.CARD,
				ItemId = GameUtils.TranslateCardIdToDbId(cardRewardData.CardID),
				Quantity = cardRewardData.Count
			};
			if (GameDbf.Card.GetRecord(rewardItemDataModel2.ItemId) != null)
			{
				premium = cardRewardData.Premium;
				rewardItemDataModel = rewardItemDataModel2;
			}
			break;
		}
		case Reward.Type.CARD_BACK:
		{
			CardBackRewardData cardBackRewardData = rewardData as CardBackRewardData;
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.CARD_BACK,
				ItemId = cardBackRewardData.CardBackID
			};
			break;
		}
		case Reward.Type.RANDOM_CARD:
		{
			RandomCardRewardData randomCardRewardData = rewardData as RandomCardRewardData;
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.RANDOM_CARD,
				Quantity = randomCardRewardData.Count
			};
			rarity = randomCardRewardData.Rarity;
			premium = randomCardRewardData.Premium;
			break;
		}
		case Reward.Type.ARCANE_ORBS:
		{
			SimpleRewardData simpleRewardData = rewardData as SimpleRewardData;
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.ARCANE_ORBS,
				Quantity = simpleRewardData.Amount
			};
			break;
		}
		case Reward.Type.BOOSTER_PACK:
		{
			BoosterPackRewardData boosterPackRewardData = rewardData as BoosterPackRewardData;
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.BOOSTER,
				ItemId = boosterPackRewardData.Id,
				Quantity = boosterPackRewardData.Count
			};
			break;
		}
		default:
			Log.All.PrintWarning("RewardDataToRewardItemDataModel() - RewardData of type {0} is not currently supported!", rewardData.RewardType);
			break;
		}
		if (rewardItemDataModel != null)
		{
			string warningPrefix = $"RewardData Error [Type = {rewardData.RewardType}]: ";
			if (!InitializeRewardItemDataModel(rewardItemDataModel, rarity, premium, warningPrefix))
			{
				rewardItemDataModel = null;
			}
		}
		return rewardItemDataModel;
	}

	public static RewardListDataModel CreateRewardListDataModelFromRewardListId(int rewardListId, int chooseOneRewardItemId = 0, List<RewardItemOutput> rewardItemOutputs = null)
	{
		return CreateRewardListDataModelFromRewardListRecord(GameDbf.RewardList.GetRecord(rewardListId), chooseOneRewardItemId, rewardItemOutputs);
	}

	public static RewardListDataModel CreateRewardListDataModelFromRewardListRecord(RewardListDbfRecord rewardListRecord, int chooseOneRewardItemId = 0, List<RewardItemOutput> rewardItemOutputs = null)
	{
		if (rewardListRecord == null)
		{
			return null;
		}
		return new RewardListDataModel
		{
			ChooseOne = rewardListRecord.ChooseOne,
			Items = rewardListRecord.RewardItems.Where((RewardItemDbfRecord r) => chooseOneRewardItemId <= 0 || r.ID == chooseOneRewardItemId).SelectMany((RewardItemDbfRecord r) => RewardFactory.CreateRewardItemDataModel(r, rewardItemOutputs?.Find((RewardItemOutput rio) => rio.RewardItemId == r.ID)?.OutputData)).OrderBy((RewardItemDataModel item) => item, new RewardItemComparer())
				.ToDataModelList(),
			Description = rewardListRecord.Description
		};
	}

	public static bool InitializeRewardItemDataModelForShop(RewardItemDataModel item, Network.BundleItem netBundleItem = null, string warningPrefix = null)
	{
		TAG_RARITY rarity = TAG_RARITY.INVALID;
		TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
		switch (item.ItemType)
		{
		case RewardItemType.RANDOM_CARD:
			if (netBundleItem != null)
			{
				KeyValuePair<string, string> keyValuePair = netBundleItem.Attributes.FirstOrDefault((KeyValuePair<string, string> att) => att.Key.Equals("premium"));
				if (!string.IsNullOrEmpty(keyValuePair.Value))
				{
					if (keyValuePair.Value.Equals("1"))
					{
						premium = TAG_PREMIUM.GOLDEN;
					}
					else if (keyValuePair.Value.Equals("2"))
					{
						premium = TAG_PREMIUM.DIAMOND;
					}
				}
			}
			rarity = GetRarityForRandomCardReward(item.ItemId);
			break;
		case RewardItemType.HERO_SKIN:
			premium = TAG_PREMIUM.GOLDEN;
			break;
		case RewardItemType.CARD:
			premium = TAG_PREMIUM.NORMAL;
			break;
		}
		return InitializeRewardItemDataModel(item, rarity, premium, warningPrefix);
	}

	public static bool InitializeRewardItemDataModel(RewardItemDataModel item, TAG_RARITY rarity, TAG_PREMIUM premium, string warningPrefix = null)
	{
		bool result = false;
		switch (item.ItemType)
		{
		case RewardItemType.BOOSTER:
			if (GameDbf.Booster.HasRecord(item.ItemId))
			{
				item.Booster = new PackDataModel
				{
					Type = (BoosterDbId)item.ItemId,
					Quantity = item.Quantity
				};
				result = true;
			}
			else if (warningPrefix != null)
			{
				Log.All.PrintWarning("{0} Booster Item has unrecognized booster id [{1}]", warningPrefix, item.ItemId);
			}
			break;
		case RewardItemType.RANDOM_CARD:
			item.RandomCard = new RandomCardDataModel
			{
				Premium = premium,
				Rarity = rarity,
				Count = item.Quantity
			};
			result = rarity != TAG_RARITY.INVALID;
			break;
		case RewardItemType.ARENA_TICKET:
		case RewardItemType.BATTLEGROUNDS_BONUS:
		case RewardItemType.TAVERN_BRAWL_TICKET:
		case RewardItemType.PROGRESSION_BONUS:
		case RewardItemType.REWARD_TRACK_XP_BOOST:
			result = true;
			break;
		case RewardItemType.DUST:
		case RewardItemType.RUNESTONES:
		case RewardItemType.ARCANE_ORBS:
			item.Currency = new PriceDataModel
			{
				Currency = RewardItemTypeToCurrencyType(item.ItemType),
				Amount = item.Quantity,
				DisplayText = item.Quantity.ToString()
			};
			result = !ShopUtils.IsCurrencyVirtual(item.Currency.Currency) || ShopUtils.IsVirtualCurrencyEnabled();
			break;
		case RewardItemType.CARD_BACK:
			if (GameDbf.CardBack.HasRecord(item.ItemId))
			{
				item.CardBack = new CardBackDataModel
				{
					CardBackId = item.ItemId
				};
				result = true;
			}
			else if (warningPrefix != null)
			{
				Log.All.PrintWarning("{0} Card Back Item has unrecognized card back id [{1}]", warningPrefix, item.ItemId);
			}
			break;
		case RewardItemType.HERO_SKIN:
		{
			CardDbfRecord record4 = GameDbf.Card.GetRecord(item.ItemId);
			if (record4 != null)
			{
				if (record4.CardHero != null)
				{
					item.Card = new CardDataModel
					{
						CardId = record4.NoteMiniGuid,
						Premium = premium
					};
					result = true;
				}
				else
				{
					Log.All.PrintWarning("{0} Hero Skin Item has invalid card id [{1}] where card dbf record has no CARD_HERO subtable. NoteMiniGuid = {2}", warningPrefix, item.ItemId, record4.NoteMiniGuid);
				}
			}
			else
			{
				Log.All.PrintWarning("{0} Hero Skin Item has unknown card id [{1}]", warningPrefix, item.ItemId);
			}
			break;
		}
		case RewardItemType.CUSTOM_COIN:
		{
			CoinDbfRecord record = GameDbf.Coin.GetRecord(item.ItemId);
			if (record != null)
			{
				CardDbfRecord record2 = GameDbf.Card.GetRecord(record.CardId);
				if (record2 != null)
				{
					item.Card = new CardDataModel
					{
						CardId = record2.NoteMiniGuid,
						Premium = premium
					};
					result = true;
				}
				else
				{
					Log.All.PrintWarning("{0} Custom Coin Item has unknown card id [{1}]", warningPrefix, record.CardId);
				}
			}
			else
			{
				Log.All.PrintWarning("{0} Custom Coin Item has unknown id [{1}]", warningPrefix, item.ItemId);
			}
			break;
		}
		case RewardItemType.CARD:
		{
			CardDbfRecord record3 = GameDbf.Card.GetRecord(item.ItemId);
			if (record3 != null)
			{
				item.Card = new CardDataModel
				{
					CardId = record3.NoteMiniGuid,
					Premium = premium
				};
				result = true;
			}
			else
			{
				Log.All.PrintWarning("{0} Card Item has unknown card id [{1}]", warningPrefix, item.ItemId);
			}
			break;
		}
		case RewardItemType.ADVENTURE_WING:
			if (GameDbf.Wing.HasRecord(item.ItemId))
			{
				result = true;
				break;
			}
			Log.All.PrintWarning("{0} Adventure Wing Item has unknown adventure wing id [{1}]", warningPrefix, item.ItemId);
			break;
		case RewardItemType.MINI_SET:
		{
			int itemId2 = item.ItemId;
			if (GameDbf.MiniSet.GetRecord(itemId2) == null)
			{
				Log.All.PrintWarning("{0} Mini-Set has unknown MINI_SET id [{1}]", warningPrefix, itemId2);
			}
			else
			{
				result = true;
			}
			break;
		}
		case RewardItemType.SELLABLE_DECK:
		{
			int itemId = item.ItemId;
			if (GameDbf.SellableDeck.GetRecord(itemId) == null)
			{
				Log.All.PrintWarning("{0} Sellable deck has unknown SELLABLE_DECK id [{1}]", warningPrefix, itemId);
			}
			else
			{
				result = true;
			}
			break;
		}
		default:
			Log.All.PrintWarning("{0} Item has unsupported item type [{1}]", warningPrefix, item.ItemType);
			break;
		}
		return result;
	}

	public static CurrencyType RewardItemTypeToCurrencyType(RewardItemType itemType)
	{
		return itemType switch
		{
			RewardItemType.DUST => CurrencyType.DUST, 
			RewardItemType.ARCANE_ORBS => CurrencyType.ARCANE_ORBS, 
			RewardItemType.RUNESTONES => CurrencyType.RUNESTONES, 
			_ => CurrencyType.NONE, 
		};
	}

	public static int GetRewardItemTypeSortOrder(RewardItemType itemType)
	{
		return itemType switch
		{
			RewardItemType.REWARD_TRACK_XP_BOOST => 50, 
			RewardItemType.HERO_SKIN => 100, 
			RewardItemType.CARD_BACK => 200, 
			RewardItemType.CARD => 300, 
			RewardItemType.RANDOM_CARD => 400, 
			RewardItemType.CARD_SUBSET => 425, 
			RewardItemType.BATTLEGROUNDS_BONUS => 450, 
			RewardItemType.ARENA_TICKET => 500, 
			RewardItemType.TAVERN_BRAWL_TICKET => 550, 
			RewardItemType.ADVENTURE => 600, 
			RewardItemType.ADVENTURE_WING => 700, 
			RewardItemType.BOOSTER => 800, 
			RewardItemType.RUNESTONES => 900, 
			RewardItemType.ARCANE_ORBS => 1000, 
			RewardItemType.DUST => 1100, 
			RewardItemType.PROGRESSION_BONUS => 1200, 
			RewardItemType.MINI_SET => 1300, 
			RewardItemType.SELLABLE_DECK => 1400, 
			_ => int.MaxValue, 
		};
	}

	public static int CompareItemsForSort(RewardItemDataModel xItem, RewardItemDataModel yItem)
	{
		if (xItem == null && yItem == null)
		{
			return 0;
		}
		if (xItem == null)
		{
			return 1;
		}
		if (yItem == null)
		{
			return -1;
		}
		int rewardItemTypeSortOrder = GetRewardItemTypeSortOrder(xItem.ItemType);
		int rewardItemTypeSortOrder2 = GetRewardItemTypeSortOrder(yItem.ItemType);
		if (rewardItemTypeSortOrder < rewardItemTypeSortOrder2)
		{
			return -1;
		}
		if (rewardItemTypeSortOrder > rewardItemTypeSortOrder2)
		{
			return 1;
		}
		if (xItem.Quantity > yItem.Quantity)
		{
			return -1;
		}
		if (xItem.Quantity < yItem.Quantity)
		{
			return 1;
		}
		if (xItem.Booster != null && yItem.Booster != null)
		{
			BoosterDbfRecord record = GameDbf.Booster.GetRecord((int)xItem.Booster.Type);
			BoosterDbfRecord record2 = GameDbf.Booster.GetRecord((int)yItem.Booster.Type);
			if (record == null && record2 == null)
			{
				return 0;
			}
			if (record == null)
			{
				return 1;
			}
			if (record2 == null)
			{
				return -1;
			}
			int num = GameUtils.PackSortingPredicate(record, record2);
			if (num != 0)
			{
				return num;
			}
		}
		return 0;
	}

	public static void SetNewRewardedDeck(long collectionDeckId)
	{
		Options.Get().SetLong(Option.NEWEST_REWARDED_DECK_ID, collectionDeckId);
	}

	public static bool HasNewRewardedDeck(out long collectionDeckId)
	{
		collectionDeckId = Options.Get().GetLong(Option.NEWEST_REWARDED_DECK_ID);
		return collectionDeckId != 0;
	}

	public static void MarkNewestRewardedDeckAsSeen()
	{
		SetNewRewardedDeck(0L);
	}

	public static int CompareOwnedItemsForSort(RewardItemDataModel xItem, RewardItemDataModel yItem)
	{
		if (xItem == null && yItem == null)
		{
			return 0;
		}
		if (xItem == null || xItem.Card == null)
		{
			return 1;
		}
		if (yItem == null || yItem.Card == null)
		{
			return -1;
		}
		if (xItem.Card.Owned && !yItem.Card.Owned)
		{
			return 1;
		}
		if (!xItem.Card.Owned && yItem.Card.Owned)
		{
			return -1;
		}
		return 0;
	}

	public static int GetSortOrderFromItems(DataModelList<RewardItemDataModel> items)
	{
		foreach (RewardItemDataModel item in items)
		{
			if (AttemptToGetItemSortOrder(item, out var sortOrder))
			{
				return sortOrder;
			}
		}
		return 0;
	}

	public static bool AttemptToGetItemSortOrder(RewardItemDataModel item, out int sortOrder)
	{
		if (item != null && item.ItemType == RewardItemType.SELLABLE_DECK && IsValidSellableDeckRecordId(item.ItemId))
		{
			sortOrder = GetSortOrderForSellableDeck(item.ItemId);
			return true;
		}
		sortOrder = 0;
		return false;
	}

	public static bool IsValidSellableDeckRecordId(int sellableDeckRecordId)
	{
		return GameDbf.SellableDeck.GetRecord(sellableDeckRecordId)?.DeckTemplateRecord != null;
	}

	public static int GetSortOrderForSellableDeck(int sellableDeckRecordId)
	{
		return GameDbf.SellableDeck.GetRecord(sellableDeckRecordId).DeckTemplateRecord.SortOrder;
	}
}
