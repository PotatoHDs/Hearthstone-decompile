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

// Token: 0x0200067F RID: 1663
public class RewardUtils
{
	// Token: 0x06005D03 RID: 23811 RVA: 0x001E25EC File Offset: 0x001E07EC
	public static List<RewardData> GetRewards(List<NetCache.ProfileNotice> notices)
	{
		List<RewardData> result = new List<RewardData>();
		foreach (NetCache.ProfileNotice profileNotice in notices)
		{
			RewardData rewardData = null;
			NetCache.ProfileNotice.NoticeType type = profileNotice.Type;
			if (type <= NetCache.ProfileNotice.NoticeType.EVENT)
			{
				switch (type)
				{
				case NetCache.ProfileNotice.NoticeType.REWARD_BOOSTER:
				{
					NetCache.ProfileNoticeRewardBooster profileNoticeRewardBooster = profileNotice as NetCache.ProfileNoticeRewardBooster;
					rewardData = new BoosterPackRewardData(profileNoticeRewardBooster.Id, profileNoticeRewardBooster.Count);
					break;
				}
				case NetCache.ProfileNotice.NoticeType.REWARD_CARD:
				{
					NetCache.ProfileNoticeRewardCard profileNoticeRewardCard = profileNotice as NetCache.ProfileNoticeRewardCard;
					rewardData = new CardRewardData(profileNoticeRewardCard.CardID, profileNoticeRewardCard.Premium, profileNoticeRewardCard.Quantity);
					break;
				}
				case NetCache.ProfileNotice.NoticeType.DISCONNECTED_GAME:
				case NetCache.ProfileNotice.NoticeType.PRECON_DECK:
				case NetCache.ProfileNotice.NoticeType.PURCHASE:
					continue;
				case NetCache.ProfileNotice.NoticeType.REWARD_DUST:
					if (profileNotice.Origin == NetCache.ProfileNotice.NoticeOrigin.HOF_COMPENSATION)
					{
						continue;
					}
					rewardData = new ArcaneDustRewardData((profileNotice as NetCache.ProfileNoticeRewardDust).Amount);
					break;
				case NetCache.ProfileNotice.NoticeType.REWARD_MOUNT:
					rewardData = new MountRewardData((MountRewardData.MountType)(profileNotice as NetCache.ProfileNoticeRewardMount).MountID);
					break;
				case NetCache.ProfileNotice.NoticeType.REWARD_FORGE:
					rewardData = new ForgeTicketRewardData((profileNotice as NetCache.ProfileNoticeRewardForge).Quantity);
					break;
				case NetCache.ProfileNotice.NoticeType.REWARD_CURRENCY:
				{
					NetCache.ProfileNoticeRewardCurrency profileNoticeRewardCurrency = profileNotice as NetCache.ProfileNoticeRewardCurrency;
					PegasusShared.CurrencyType currencyType = profileNoticeRewardCurrency.CurrencyType;
					if (currencyType != PegasusShared.CurrencyType.CURRENCY_TYPE_GOLD)
					{
						if (currencyType == PegasusShared.CurrencyType.CURRENCY_TYPE_ARCANE_ORBS)
						{
							rewardData = RewardUtils.CreateArcaneOrbRewardData(profileNoticeRewardCurrency.Amount);
						}
					}
					else
					{
						rewardData = new GoldRewardData((long)profileNoticeRewardCurrency.Amount, new DateTime?(DateTime.FromFileTimeUtc(profileNoticeRewardCurrency.Date)));
					}
					break;
				}
				case NetCache.ProfileNotice.NoticeType.REWARD_CARD_BACK:
					rewardData = new CardBackRewardData((profileNotice as NetCache.ProfileNoticeRewardCardBack).CardBackID);
					break;
				default:
					if (type != NetCache.ProfileNotice.NoticeType.EVENT)
					{
						continue;
					}
					rewardData = new EventRewardData((profileNotice as NetCache.ProfileNoticeEvent).EventType);
					break;
				}
			}
			else if (type != NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST)
			{
				if (type != NetCache.ProfileNotice.NoticeType.MINI_SET_GRANTED)
				{
					continue;
				}
				rewardData = new MiniSetRewardData((profileNotice as NetCache.ProfileNoticeMiniSetGranted).MiniSetID);
			}
			else
			{
				RewardUtils.AddRewardDataForGenericRewardChest(profileNotice as NetCache.ProfileNoticeGenericRewardChest, ref result);
				rewardData = null;
			}
			if (rewardData != null)
			{
				RewardUtils.SetNoticeAndAddRewardDataToList(profileNotice, ref rewardData, ref result);
			}
		}
		return result;
	}

	// Token: 0x06005D04 RID: 23812 RVA: 0x001E27EC File Offset: 0x001E09EC
	private static void SetNoticeAndAddRewardDataToList(NetCache.ProfileNotice notice, ref RewardData rewardData, ref List<RewardData> rewardDataList)
	{
		rewardData.SetOrigin(notice.Origin, notice.OriginData);
		rewardData.AddNoticeID(notice.NoticeID);
		RewardUtils.AddRewardDataToList(rewardData, rewardDataList);
	}

	// Token: 0x06005D05 RID: 23813 RVA: 0x001E2818 File Offset: 0x001E0A18
	private static void AddRewardDataForGenericRewardChest(NetCache.ProfileNoticeGenericRewardChest notice, ref List<RewardData> rewardDataList)
	{
		RewardChest rewardChest = notice.RewardChest;
		if (rewardChest == null)
		{
			return;
		}
		RewardUtils.AddRewardDataForGenericRewardChestBag(notice, rewardChest.Bag1, 1, ref rewardDataList);
		RewardUtils.AddRewardDataForGenericRewardChestBag(notice, rewardChest.Bag2, 2, ref rewardDataList);
		RewardUtils.AddRewardDataForGenericRewardChestBag(notice, rewardChest.Bag3, 3, ref rewardDataList);
		RewardUtils.AddRewardDataForGenericRewardChestBag(notice, rewardChest.Bag4, 4, ref rewardDataList);
		RewardUtils.AddRewardDataForGenericRewardChestBag(notice, rewardChest.Bag5, 5, ref rewardDataList);
	}

	// Token: 0x06005D06 RID: 23814 RVA: 0x001E2878 File Offset: 0x001E0A78
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
				text = record2.Name.GetString(true);
			}
			if (record2.Description != null && string.IsNullOrEmpty(text2))
			{
				text2 = record2.Description.GetString(true);
			}
		}
		RewardData rewardData = Network.ConvertRewardBag(rewardBag);
		if (rewardData != null)
		{
			rewardData.RewardChestAssetId = new int?(notice.RewardChestAssetId);
			rewardData.RewardChestBagNum = new int?(bagNum);
			rewardData.NameOverride = text;
			rewardData.DescriptionOverride = text2;
			RewardUtils.SetNoticeAndAddRewardDataToList(notice, ref rewardData, ref rewardDataList);
		}
	}

	// Token: 0x06005D07 RID: 23815 RVA: 0x001E297C File Offset: 0x001E0B7C
	public static void GetViewableRewards(List<RewardData> rewardDataList, HashSet<Assets.Achieve.RewardTiming> rewardTimings, out List<RewardData> rewardsToShow, out List<RewardData> genericRewardChestsToShow, ref List<RewardData> purchasedCardRewardsToShow, ref List<global::Achievement> completedQuests)
	{
		rewardsToShow = new List<RewardData>();
		genericRewardChestsToShow = new List<RewardData>();
		if (completedQuests == null)
		{
			completedQuests = new List<global::Achievement>();
		}
		foreach (RewardData rewardData in rewardDataList)
		{
			Log.Achievements.Print("RewardUtils.GetViewableRewards() - processing reward {0}", new object[]
			{
				rewardData
			});
			if (NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT == rewardData.Origin)
			{
				global::Achievement completedQuest = AchieveManager.Get().GetAchievement((int)rewardData.OriginData);
				if (completedQuest != null)
				{
					List<long> noticeIDs = rewardData.GetNoticeIDs();
					global::Achievement achievement = completedQuests.Find((global::Achievement obj) => completedQuest.ID == obj.ID);
					if (achievement != null)
					{
						using (List<long>.Enumerator enumerator2 = noticeIDs.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								long noticeID = enumerator2.Current;
								achievement.AddRewardNoticeID(noticeID);
							}
							continue;
						}
					}
					foreach (long noticeID2 in noticeIDs)
					{
						completedQuest.AddRewardNoticeID(noticeID2);
					}
					if (rewardTimings.Contains(completedQuest.RewardTiming))
					{
						completedQuests.Add(completedQuest);
					}
				}
			}
			else if (rewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.GENERIC_REWARD_CHEST_ACHIEVE)
			{
				global::Achievement achievement2 = AchieveManager.Get().GetAchievement((int)rewardData.OriginData);
				if (achievement2 == null || rewardTimings.Contains(achievement2.RewardTiming))
				{
					genericRewardChestsToShow.Add(rewardData);
				}
			}
			else if (rewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.GENERIC_REWARD_CHEST)
			{
				if (rewardData.Origin != NetCache.ProfileNotice.NoticeOrigin.NOTICE_ORIGIN_DUELS)
				{
					genericRewardChestsToShow.Add(rewardData);
				}
			}
			else
			{
				bool flag = false;
				Reward.Type rewardType = rewardData.RewardType;
				switch (rewardType)
				{
				case Reward.Type.ARCANE_DUST:
				case Reward.Type.BOOSTER_PACK:
				case Reward.Type.GOLD:
					flag = true;
					break;
				case Reward.Type.CARD:
				{
					CardRewardData cardRewardData = rewardData as CardRewardData;
					if (cardRewardData.CardID.Equals("HERO_08") && cardRewardData.Premium == TAG_PREMIUM.NORMAL)
					{
						flag = false;
						rewardData.AcknowledgeNotices();
						CollectionManager.Get().AddCardReward(cardRewardData, false);
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
				case Reward.Type.CARD_BACK:
					flag = (NetCache.ProfileNotice.NoticeOrigin.SEASON != rewardData.Origin);
					break;
				case Reward.Type.CRAFTABLE_CARD:
					break;
				case Reward.Type.FORGE_TICKET:
				{
					bool flag2 = false;
					if (NetCache.ProfileNotice.NoticeOrigin.BLIZZCON == rewardData.Origin && 2013L == rewardData.OriginData)
					{
						flag2 = true;
					}
					if (rewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.OUT_OF_BAND_LICENSE)
					{
						Log.Achievements.Print(string.Format("RewardUtils.GetViewableRewards(): auto-acking notices for out of band license reward {0}", rewardData), Array.Empty<object>());
						flag2 = true;
					}
					if (flag2)
					{
						rewardData.AcknowledgeNotices();
					}
					flag = false;
					break;
				}
				default:
					if (rewardType == Reward.Type.MINI_SET)
					{
						flag = false;
						if (purchasedCardRewardsToShow != null)
						{
							purchasedCardRewardsToShow.Add(rewardData);
						}
					}
					break;
				}
				if (flag)
				{
					rewardsToShow.Add(rewardData);
				}
			}
		}
	}

	// Token: 0x06005D08 RID: 23816 RVA: 0x001E2CE8 File Offset: 0x001E0EE8
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
				EntityBase entityDef = DefLoader.Get().GetEntityDef(cardRewardData.CardID);
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
			else
			{
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
				if (Reward.Type.MOUNT == r2.RewardType)
				{
					return 1;
				}
				return 0;
			}
		});
	}

	// Token: 0x06005D09 RID: 23817 RVA: 0x001E2D18 File Offset: 0x001E0F18
	public static void AddRewardDataToList(RewardData newRewardData, List<RewardData> existingRewardDataList)
	{
		CardRewardData duplicateCardDataReward = RewardUtils.GetDuplicateCardDataReward(newRewardData, existingRewardDataList);
		if (duplicateCardDataReward == null)
		{
			existingRewardDataList.Add(newRewardData);
			return;
		}
		CardRewardData other = newRewardData as CardRewardData;
		duplicateCardDataReward.Merge(other);
	}

	// Token: 0x06005D0A RID: 23818 RVA: 0x001E2D48 File Offset: 0x001E0F48
	public static bool GetNextHeroLevelRewardText(TAG_CLASS heroClass, int heroLevel, int totalLevel, out string nextRewardTitle, out string nextRewardDescription)
	{
		int num;
		RewardData nextHeroLevelReward = FixedRewardsMgr.Get().GetNextHeroLevelReward(heroClass, heroLevel, out num);
		int num2;
		RewardData nextTotalLevelReward = FixedRewardsMgr.Get().GetNextTotalLevelReward(totalLevel, out num2);
		nextRewardTitle = string.Empty;
		nextRewardDescription = string.Empty;
		bool flag = num > 0;
		bool flag2 = num2 > 0;
		if (!flag && !flag2)
		{
			return false;
		}
		int num3 = 0;
		int num4 = num - heroLevel;
		int num5 = num2 - totalLevel;
		if (flag && (!flag2 || num4 <= num5))
		{
			num3 = num;
			nextRewardDescription = RewardUtils.GetRewardText(nextHeroLevelReward);
		}
		if (flag && flag2 && num4 == num5)
		{
			nextRewardDescription += "\n";
		}
		if (flag2 && (!flag || num5 <= num4))
		{
			num3 = heroLevel + num5;
			nextRewardDescription += RewardUtils.GetRewardText(nextTotalLevelReward);
		}
		if (num3 > 0)
		{
			nextRewardTitle = GameStrings.Format("GLOBAL_HERO_LEVEL_NEXT_REWARD_TITLE", new object[]
			{
				num3
			});
		}
		return nextRewardTitle != string.Empty;
	}

	// Token: 0x06005D0B RID: 23819 RVA: 0x001E2E30 File Offset: 0x001E1030
	public static string GetRewardText(RewardData rewardData)
	{
		if (rewardData == null)
		{
			return string.Empty;
		}
		switch (rewardData.RewardType)
		{
		case Reward.Type.ARCANE_DUST:
		{
			ArcaneDustRewardData arcaneDustRewardData = rewardData as ArcaneDustRewardData;
			return GameStrings.Format("GLOBAL_HERO_LEVEL_REWARD_ARCANE_DUST", new object[]
			{
				arcaneDustRewardData.Amount
			});
		}
		case Reward.Type.BOOSTER_PACK:
		{
			BoosterPackRewardData boosterPackRewardData = rewardData as BoosterPackRewardData;
			string text = GameDbf.Booster.GetRecord(boosterPackRewardData.Id).Name;
			return GameStrings.Format("GLOBAL_HERO_LEVEL_REWARD_BOOSTER", new object[]
			{
				text
			});
		}
		case Reward.Type.CARD:
		{
			CardRewardData cardRewardData = rewardData as CardRewardData;
			EntityDef entityDef = DefLoader.Get().GetEntityDef(cardRewardData.CardID);
			if (cardRewardData.Premium == TAG_PREMIUM.GOLDEN)
			{
				return GameStrings.Format("GLOBAL_HERO_LEVEL_REWARD_GOLDEN_CARD", new object[]
				{
					GameStrings.Get("GLOBAL_COLLECTION_GOLDEN"),
					entityDef.GetName()
				});
			}
			return entityDef.GetName();
		}
		case Reward.Type.GOLD:
		{
			GoldRewardData goldRewardData = rewardData as GoldRewardData;
			return GameStrings.Format("GLOBAL_HERO_LEVEL_REWARD_GOLD", new object[]
			{
				goldRewardData.Amount
			});
		}
		}
		return "UNKNOWN";
	}

	// Token: 0x06005D0C RID: 23820 RVA: 0x001E2F68 File Offset: 0x001E1168
	public static bool ShowReward(UserAttentionBlocker blocker, Reward reward, bool updateCacheValues, Vector3 rewardPunchScale, Vector3 rewardScale, AnimationUtil.DelOnShownWithPunch callback, object callbackData)
	{
		return RewardUtils.ShowReward_Internal(blocker, reward, updateCacheValues, rewardPunchScale, rewardScale, string.Empty, null, callback, callbackData);
	}

	// Token: 0x06005D0D RID: 23821 RVA: 0x001E2F8C File Offset: 0x001E118C
	public static bool ShowReward(UserAttentionBlocker blocker, Reward reward, bool updateCacheValues, Vector3 rewardPunchScale, Vector3 rewardScale, string callbackName = "", object callbackData = null, GameObject callbackGO = null)
	{
		return RewardUtils.ShowReward_Internal(blocker, reward, updateCacheValues, rewardPunchScale, rewardScale, callbackName, callbackGO, null, callbackData);
	}

	// Token: 0x06005D0E RID: 23822 RVA: 0x001E2FAC File Offset: 0x001E11AC
	public static void SetupRewardIcon(RewardData rewardData, Renderer rewardRenderer, UberText rewardAmountLabel, out float amountToScaleReward, bool doubleGold = false)
	{
		UnityEngine.Vector2 zero = UnityEngine.Vector2.zero;
		amountToScaleReward = 1f;
		rewardAmountLabel.gameObject.SetActive(false);
		Material rewardMaterial = (rewardRenderer != null) ? rewardRenderer.GetMaterial() : null;
		AssetHandleCallback<Texture> callback = delegate(AssetReference assetRef, AssetHandle<Texture> texture, object loadTextureCbData)
		{
			if (rewardRenderer != null)
			{
				DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
				if (disposablesCleaner != null)
				{
					disposablesCleaner.Attach(rewardRenderer, texture);
				}
				if (rewardMaterial != null)
				{
					rewardMaterial.mainTexture = texture;
					return;
				}
			}
			else if (texture != null)
			{
				texture.Dispose();
			}
		};
		Reward.Type rewardType = rewardData.RewardType;
		switch (rewardType)
		{
		case Reward.Type.ARCANE_DUST:
		{
			AssetLoader.Get().LoadAsset<Texture>(RewardUtils.QUEST_REWARDS_TEXTURE_PAGE_2, callback, null, AssetLoadingOptions.None);
			zero = new UnityEngine.Vector2(0.25f, 0f);
			ArcaneDustRewardData arcaneDustRewardData = rewardData as ArcaneDustRewardData;
			rewardAmountLabel.Text = arcaneDustRewardData.Amount.ToString();
			rewardAmountLabel.gameObject.SetActive(true);
			break;
		}
		case Reward.Type.BOOSTER_PACK:
		{
			BoosterPackRewardData boosterPackRewardData = rewardData as BoosterPackRewardData;
			BoosterDbfRecord record = GameDbf.Booster.GetRecord(boosterPackRewardData.Id);
			if (!string.IsNullOrEmpty(record.QuestIconPath))
			{
				AssetLoader.Get().LoadAsset<Texture>(record.QuestIconPath, callback, null, AssetLoadingOptions.None);
				zero = new UnityEngine.Vector2((float)record.QuestIconOffsetX, (float)record.QuestIconOffsetY);
			}
			else
			{
				Log.Achievements.PrintWarning("Booster Record ID = {0} does not have proper reward icon data", new object[]
				{
					boosterPackRewardData.Id
				});
				zero = new UnityEngine.Vector2(0f, 0.75f);
				if (boosterPackRewardData.Id == 11 && boosterPackRewardData.Count > 1)
				{
					zero = new UnityEngine.Vector2(0f, 0.5f);
				}
			}
			break;
		}
		case Reward.Type.CARD:
		{
			CardRewardData cardRewardData = rewardData as CardRewardData;
			if (cardRewardData.CardID == "HERO_03a")
			{
				zero = new UnityEngine.Vector2(0.75f, 0.5f);
			}
			else if (cardRewardData.CardID == "HERO_06a")
			{
				zero = new UnityEngine.Vector2(0.75f, 0.25f);
			}
			else
			{
				zero = new UnityEngine.Vector2(0.5f, 0f);
			}
			break;
		}
		case Reward.Type.CARD_BACK:
		case Reward.Type.CRAFTABLE_CARD:
			break;
		case Reward.Type.FORGE_TICKET:
			zero = new UnityEngine.Vector2(0.75f, 0.75f);
			amountToScaleReward = 1.46881f;
			break;
		case Reward.Type.GOLD:
		{
			zero = new UnityEngine.Vector2(0.25f, 0.75f);
			long num = ((GoldRewardData)rewardData).Amount;
			if (doubleGold)
			{
				num *= 2L;
			}
			rewardAmountLabel.Text = num.ToString();
			rewardAmountLabel.gameObject.SetActive(true);
			break;
		}
		default:
			if (rewardType == Reward.Type.ARCANE_ORBS)
			{
				AssetLoader.Get().LoadAsset<Texture>(RewardUtils.ARCANE_ORB_ICON, callback, null, AssetLoadingOptions.None);
				rewardAmountLabel.Text = ((SimpleRewardData)rewardData).Amount.ToString();
				rewardAmountLabel.gameObject.SetActive(true);
				rewardMaterial.mainTextureScale = new UnityEngine.Vector2(4f, 4f);
			}
			break;
		}
		rewardMaterial.mainTextureOffset = zero;
	}

	// Token: 0x06005D0F RID: 23823 RVA: 0x001E328C File Offset: 0x001E148C
	public static void ShowQuestChestReward(string title, string desc, List<RewardData> rewards, Transform rewardBone, Action doneCallback, bool fromNotice = false, int noticeID = -1, string prefab = "RewardChest_Lock.prefab:06ffa33e82036694e8cacb96aa7b48e8")
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			ChestRewardDisplay componentInChildren = go.GetComponentInChildren<ChestRewardDisplay>();
			componentInChildren.RegisterDoneCallback(doneCallback);
			GameUtils.SetParent(componentInChildren.m_parent.transform, rewardBone, false);
			if (!componentInChildren.ShowRewards_Quest(rewards, rewardBone, title, desc, fromNotice, noticeID))
			{
				UnityEngine.Object.Destroy(go);
			}
		};
		AssetLoader.Get().InstantiatePrefab(prefab, callback, null, AssetLoadingOptions.None);
	}

	// Token: 0x06005D10 RID: 23824 RVA: 0x001E32F4 File Offset: 0x001E14F4
	public static void ShowTavernBrawlRewards(int wins, List<RewardData> rewards, Transform rewardBone, Action doneCallback, bool fromNotice = false, NetCache.ProfileNoticeTavernBrawlRewards notice = null)
	{
		bool flag = (fromNotice ? notice.Mode : TavernBrawlManager.Get().CurrentSeasonBrawlMode) != TavernBrawlMode.TB_MODE_NORMAL;
		long noticeID = (notice == null) ? 0L : notice.NoticeID;
		if (!flag)
		{
			RewardUtils.ShowSessionTavernBrawlRewards(wins, rewards, rewardBone, doneCallback, fromNotice, noticeID);
			return;
		}
		RewardUtils.ShowHeroicSessionTavernBrawlRewards(wins, rewards, rewardBone, doneCallback, fromNotice, noticeID);
	}

	// Token: 0x06005D11 RID: 23825 RVA: 0x001E3344 File Offset: 0x001E1544
	public static void ShowSessionTavernBrawlRewards(int wins, List<RewardData> rewards, Transform rewardBone, Action doneCallback, bool fromNotice = false, long noticeID = -1L)
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			ChestRewardDisplay componentInChildren = go.GetComponentInChildren<ChestRewardDisplay>();
			componentInChildren.RegisterDoneCallback(doneCallback);
			GameUtils.SetParent(componentInChildren.m_parent.transform, rewardBone, false);
			if (!componentInChildren.ShowRewards_TavernBrawl(wins, rewards, rewardBone, fromNotice, noticeID))
			{
				UnityEngine.Object.Destroy(go);
			}
		};
		AssetLoader.Get().InstantiatePrefab("RewardChest_Lock.prefab:06ffa33e82036694e8cacb96aa7b48e8", callback, null, AssetLoadingOptions.None);
	}

	// Token: 0x06005D12 RID: 23826 RVA: 0x001E33A8 File Offset: 0x001E15A8
	public static void ShowLeaguePromotionRewards(int leagueId, List<RewardData> rewards, Transform rewardBone, Action doneCallback, bool fromNotice = false, long noticeID = -1L)
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			ChestRewardDisplay componentInChildren = go.GetComponentInChildren<ChestRewardDisplay>();
			componentInChildren.RegisterDoneCallback(doneCallback);
			GameUtils.SetParent(componentInChildren.m_parent.transform, rewardBone, false);
			if (!componentInChildren.ShowRewards_LeaguePromotion(leagueId, rewards, rewardBone, fromNotice, noticeID))
			{
				UnityEngine.Object.Destroy(go);
			}
		};
		AssetLoader.Get().InstantiatePrefab("RewardChest_Lock.prefab:06ffa33e82036694e8cacb96aa7b48e8", callback, null, AssetLoadingOptions.None);
	}

	// Token: 0x06005D13 RID: 23827 RVA: 0x001E340C File Offset: 0x001E160C
	public static void ShowHeroicSessionTavernBrawlRewards(int wins, List<RewardData> rewards, Transform rewardBone, Action doneCallback, bool fromNotice = false, long noticeID = -1L)
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			HeroicBrawlRewardDisplay component = go.GetComponent<HeroicBrawlRewardDisplay>();
			component.RegisterDoneCallback(doneCallback);
			TransformUtil.AttachAndPreserveLocalTransform(component.transform, rewardBone);
			component.ShowRewards(wins, rewards, fromNotice, noticeID);
		};
		AssetLoader.Get().InstantiatePrefab("HeroicBrawlReward.prefab:8f49f1fcb5ca4485d9b6b22993e1b1ab", callback, null, AssetLoadingOptions.None);
	}

	// Token: 0x06005D14 RID: 23828 RVA: 0x001E3470 File Offset: 0x001E1670
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

	// Token: 0x06005D15 RID: 23829 RVA: 0x001E36FC File Offset: 0x001E18FC
	public static void SetQuestTileNameLinePosition(GameObject nameLine, UberText questName, float padding)
	{
		bool flag = questName.isHidden();
		if (flag)
		{
			questName.Show();
		}
		TransformUtil.SetPoint(nameLine, Anchor.TOP, questName, Anchor.BOTTOM);
		nameLine.transform.localPosition = new Vector3(nameLine.transform.localPosition.x, nameLine.transform.localPosition.y, nameLine.transform.localPosition.z + padding);
		if (flag)
		{
			questName.Hide();
		}
	}

	// Token: 0x06005D16 RID: 23830 RVA: 0x001E376C File Offset: 0x001E196C
	public static RewardChestContentsDbfRecord GetRewardChestContents(int rewardChestAssetId, int rewardLevel)
	{
		if (GameDbf.RewardChest.HasRecord(rewardChestAssetId))
		{
			return GameDbf.RewardChestContents.GetRecord((RewardChestContentsDbfRecord r) => r.RewardChestId == rewardChestAssetId && r.RewardLevel == rewardLevel);
		}
		return null;
	}

	// Token: 0x06005D17 RID: 23831 RVA: 0x001E37B8 File Offset: 0x001E19B8
	public static List<RewardData> GetRewardDataFromRewardChestAsset(int rewardChestAssetId, int rewardLevel)
	{
		List<RewardData> result = new List<RewardData>();
		RewardChestContentsDbfRecord rewardChestContents = RewardUtils.GetRewardChestContents(rewardChestAssetId, rewardLevel);
		if (rewardChestContents != null)
		{
			int seasonId = 0;
			RewardUtils.AddRewardDataStubForBag(rewardChestContents.Bag1, seasonId, ref result);
			RewardUtils.AddRewardDataStubForBag(rewardChestContents.Bag2, seasonId, ref result);
			RewardUtils.AddRewardDataStubForBag(rewardChestContents.Bag3, seasonId, ref result);
			RewardUtils.AddRewardDataStubForBag(rewardChestContents.Bag4, seasonId, ref result);
			RewardUtils.AddRewardDataStubForBag(rewardChestContents.Bag5, seasonId, ref result);
		}
		return result;
	}

	// Token: 0x06005D18 RID: 23832 RVA: 0x001E3820 File Offset: 0x001E1A20
	public static void AddRewardDataStubForBag(int bagId, int seasonId, ref List<RewardData> rewardData)
	{
		RewardBagDbfRecord record = GameDbf.RewardBag.GetRecord((RewardBagDbfRecord r) => r.BagId == bagId);
		if (record == null)
		{
			return;
		}
		string text = record.Reward.ToString().ToLower();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
		if (num <= 1713982284U)
		{
			if (num <= 1063971127U)
			{
				if (num != 450433639U)
				{
					if (num != 677810107U)
					{
						if (num != 1063971127U)
						{
							return;
						}
						if (!(text == "leg"))
						{
							return;
						}
						rewardData.Add(new RandomCardRewardData(TAG_RARITY.LEGENDARY, TAG_PREMIUM.NORMAL, record.Base));
						return;
					}
					else
					{
						if (!(text == "specific_pack"))
						{
							return;
						}
						rewardData.Add(new BoosterPackRewardData(record.RewardData, record.Base));
						return;
					}
				}
				else
				{
					if (!(text == "reward_chest_contents"))
					{
						return;
					}
					RewardChestContentsDbfRecord record2 = GameDbf.RewardChestContents.GetRecord(record.RewardData);
					if (record2 == null)
					{
						Log.All.PrintWarning("No reward chest contents of id {0} found on client for random card reward", new object[]
						{
							record.RewardData
						});
						return;
					}
					RewardUtils.ProcessRewardChestContents(record2, seasonId, ref rewardData);
					return;
				}
			}
			else if (num <= 1515197298U)
			{
				if (num != 1323503782U)
				{
					if (num != 1515197298U)
					{
						return;
					}
					if (!(text == "gleg"))
					{
						return;
					}
					rewardData.Add(new RandomCardRewardData(TAG_RARITY.LEGENDARY, TAG_PREMIUM.GOLDEN, record.Base));
					return;
				}
				else
				{
					if (!(text == "latest_pack"))
					{
						return;
					}
					rewardData.Add(new BoosterPackRewardData(record.RewardData, record.Base, new int?(record.BagId)));
					return;
				}
			}
			else if (num != 1595085287U)
			{
				if (num != 1713982284U)
				{
					return;
				}
				if (!(text == "ranked_season_reward_pack"))
				{
					return;
				}
				int rankedRewardBoosterIdForSeasonId = RankMgr.Get().GetRankedRewardBoosterIdForSeasonId(seasonId);
				rewardData.Add(new BoosterPackRewardData(rankedRewardBoosterIdForSeasonId, record.Base));
				return;
			}
			else
			{
				if (!(text == "gcom"))
				{
					return;
				}
				rewardData.Add(new RandomCardRewardData(TAG_RARITY.COMMON, TAG_PREMIUM.GOLDEN, record.Base));
				return;
			}
		}
		else if (num <= 2691813054U)
		{
			if (num <= 2279256383U)
			{
				if (num != 2155806056U)
				{
					if (num != 2279256383U)
					{
						return;
					}
					if (!(text == "gepic"))
					{
						return;
					}
					rewardData.Add(new RandomCardRewardData(TAG_RARITY.EPIC, TAG_PREMIUM.GOLDEN, record.Base));
					return;
				}
				else
				{
					if (!(text == "epic"))
					{
						return;
					}
					rewardData.Add(new RandomCardRewardData(TAG_RARITY.EPIC, TAG_PREMIUM.NORMAL, record.Base));
					return;
				}
			}
			else if (num != 2459073245U)
			{
				if (num != 2691813054U)
				{
					return;
				}
				if (!(text == "grare"))
				{
					return;
				}
				rewardData.Add(new RandomCardRewardData(TAG_RARITY.RARE, TAG_PREMIUM.GOLDEN, record.Base));
				return;
			}
			else
			{
				if (!(text == "rare"))
				{
					return;
				}
				rewardData.Add(new RandomCardRewardData(TAG_RARITY.RARE, TAG_PREMIUM.NORMAL, record.Base));
				return;
			}
		}
		else if (num <= 3389733797U)
		{
			if (num != 3317817953U)
			{
				if (num != 3389733797U)
				{
					return;
				}
				if (!(text == "dust"))
				{
					return;
				}
				rewardData.Add(new ArcaneDustRewardData(record.Base));
				return;
			}
			else
			{
				if (!(text == "random_card"))
				{
					return;
				}
				rewardData.Add(new RandomCardRewardData(RewardUtils.GetRarityForRandomCardReward(record.RewardData), TAG_PREMIUM.NORMAL));
				return;
			}
		}
		else if (num != 3966162835U)
		{
			if (num != 4052603614U)
			{
				return;
			}
			if (!(text == "com"))
			{
				return;
			}
			rewardData.Add(new RandomCardRewardData(TAG_RARITY.COMMON, TAG_PREMIUM.NORMAL, record.Base));
			return;
		}
		else
		{
			if (!(text == "gold"))
			{
				return;
			}
			rewardData.Add(new GoldRewardData((long)record.Base));
			return;
		}
	}

	// Token: 0x06005D19 RID: 23833 RVA: 0x001E3BE8 File Offset: 0x001E1DE8
	public static void ProcessRewardChestContents(RewardChestContentsDbfRecord rewardChestContents, int seasonId, ref List<RewardData> rewardData)
	{
		using (List<int>.Enumerator enumerator = new List<int>
		{
			rewardChestContents.Bag1,
			rewardChestContents.Bag2,
			rewardChestContents.Bag3,
			rewardChestContents.Bag4,
			rewardChestContents.Bag5
		}.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int bag = enumerator.Current;
				if (bag != 0)
				{
					RewardBagDbfRecord record = GameDbf.RewardBag.GetRecord((RewardBagDbfRecord r) => r.BagId == bag);
					if (record != null && !(record.Reward.ToString().ToLower() == "reward_chest_contents"))
					{
						RewardUtils.AddRewardDataStubForBag(bag, seasonId, ref rewardData);
					}
				}
			}
		}
	}

	// Token: 0x06005D1A RID: 23834 RVA: 0x001E3CD0 File Offset: 0x001E1ED0
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
		AssetLoader.Get().LoadGameObject("RewardBoxes.prefab:f136fead3d6a148c6801f1e3bd2e8267", callback, null, false, true, true);
	}

	// Token: 0x06005D1B RID: 23835 RVA: 0x001E3D20 File Offset: 0x001E1F20
	public static TAG_RARITY GetRarityForRandomCardReward(int boosterCardSetId)
	{
		BoosterCardSetDbfRecord record = GameDbf.BoosterCardSet.GetRecord(boosterCardSetId);
		if (record == null)
		{
			Log.All.PrintWarning("No BoosterCardSet of id [{0}] found)", new object[]
			{
				boosterCardSetId
			});
			return TAG_RARITY.INVALID;
		}
		SubsetDbfRecord subsetRecord = record.SubsetRecord;
		if (subsetRecord == null)
		{
			Log.All.PrintWarning("No subset of id {0} found on client for random card reward on boosterCardSet {1}", new object[]
			{
				record.SubsetId,
				record.ID
			});
			return TAG_RARITY.INVALID;
		}
		IEnumerable<SubsetRuleDbfRecord> source = from r in subsetRecord.Rules
		where r.Tag == 203
		select r;
		SubsetRuleDbfRecord subsetRuleDbfRecord = source.FirstOrDefault<SubsetRuleDbfRecord>();
		if (source.Count<SubsetRuleDbfRecord>() != 1 || subsetRuleDbfRecord == null || subsetRuleDbfRecord.RuleIsNot || subsetRuleDbfRecord.MinValue != subsetRuleDbfRecord.MaxValue)
		{
			Log.All.PrintWarning("Random card display requires exactly one rarity rule to specify a single rarity (subset id [{0}])", new object[]
			{
				subsetRecord.ID
			});
			return TAG_RARITY.INVALID;
		}
		return (TAG_RARITY)subsetRuleDbfRecord.MinValue;
	}

	// Token: 0x06005D1C RID: 23836 RVA: 0x001E3E18 File Offset: 0x001E2018
	public static UserAttentionBlocker GetUserAttentionBlockerForReward(NetCache.ProfileNotice.NoticeOrigin origin, long originData)
	{
		if (origin != NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT && origin != NetCache.ProfileNotice.NoticeOrigin.GENERIC_REWARD_CHEST_ACHIEVE)
		{
			return UserAttentionBlocker.NONE;
		}
		AchieveDbfRecord record = GameDbf.Achieve.GetRecord((int)originData);
		if (record == null)
		{
			return UserAttentionBlocker.NONE;
		}
		return (UserAttentionBlocker)record.AttentionBlocker;
	}

	// Token: 0x06005D1D RID: 23837 RVA: 0x001E3E48 File Offset: 0x001E2048
	private static bool ShowReward_Internal(UserAttentionBlocker blocker, Reward reward, bool updateCacheValues, Vector3 rewardPunchScale, Vector3 rewardScale, string gameObjectCallbackName, GameObject callbackGO, AnimationUtil.DelOnShownWithPunch onShowPunchCallback, object callbackData)
	{
		if (reward == null)
		{
			return false;
		}
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "RewardUtils.ShowReward:" + ((reward == null || reward.Data == null) ? "null" : string.Concat(new object[]
		{
			reward.Data.Origin,
			":",
			reward.Data.OriginData,
			":",
			reward.Data.RewardType
		}))))
		{
			return false;
		}
		Log.Achievements.Print("RewardUtils: Showing Reward: reward={0} reward.Data={1}", new object[]
		{
			reward,
			reward.Data
		});
		AnimationUtil.ShowWithPunch(reward.gameObject, RewardUtils.REWARD_HIDDEN_SCALE, rewardPunchScale, rewardScale, gameObjectCallbackName, true, callbackGO, callbackData, onShowPunchCallback);
		reward.Show(updateCacheValues);
		RewardUtils.ShowInnkeeperQuoteForReward(reward);
		return true;
	}

	// Token: 0x06005D1E RID: 23838 RVA: 0x001E3F2C File Offset: 0x001E212C
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
			return cardRewardData.CardID.Equals(newCardRewardData.CardID) && cardRewardData.Premium.Equals(newCardRewardData.Premium) && cardRewardData.Origin.Equals(newCardRewardData.Origin) && cardRewardData.OriginData.Equals(newCardRewardData.OriginData);
		}) as CardRewardData;
	}

	// Token: 0x06005D1F RID: 23839 RVA: 0x001E3F6C File Offset: 0x001E216C
	private static void ShowInnkeeperQuoteForReward(Reward reward)
	{
		if (reward == null)
		{
			return;
		}
		if (Reward.Type.CARD == reward.RewardType)
		{
			CardRewardData.InnKeeperTrigger innKeeperLine = (reward.Data as CardRewardData).InnKeeperLine;
			if (innKeeperLine != CardRewardData.InnKeeperTrigger.CORE_CLASS_SET_COMPLETE)
			{
				if (innKeeperLine != CardRewardData.InnKeeperTrigger.SECOND_REWARD_EVER)
				{
					return;
				}
				if (!Options.Get().GetBool(Option.HAS_BEEN_NUDGED_TO_CM, false))
				{
					NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_NUDGE_CM_X"), "VO_INNKEEPER2_NUDGE_COLLECTION_10.prefab:b20c7d803cf82fb46830cba5d4bda11e", 0f, null, false);
					Options.Get().SetBool(Option.HAS_BEEN_NUDGED_TO_CM, true);
				}
			}
			else
			{
				Notification innkeeperQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_BASIC_DONE1_11"), "VO_INNKEEPER_BASIC_DONE1_11.prefab:9b8f8ab262305c54dbb6c847ac8b1fdb", 0f, null, false);
				if (!Options.Get().GetBool(Option.HAS_SEEN_ALL_BASIC_CLASS_CARDS_COMPLETE, false))
				{
					Processor.RunCoroutine(RewardUtils.NotifyOfExpertPacksNeeded(innkeeperQuote), null);
					return;
				}
			}
		}
	}

	// Token: 0x06005D20 RID: 23840 RVA: 0x001E402F File Offset: 0x001E222F
	private static IEnumerator NotifyOfExpertPacksNeeded(Notification innkeeperQuote)
	{
		while (innkeeperQuote.GetAudio() == null)
		{
			yield return null;
		}
		yield return new WaitForSeconds(innkeeperQuote.GetAudio().clip.length);
		NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_BASIC_DONE2_12"), "VO_INNKEEPER_BASIC_DONE2_12.prefab:b20f6a03438c5b440a2963095330589c", 0f, null, false);
		Options.Get().SetBool(Option.HAS_SEEN_ALL_BASIC_CLASS_CARDS_COMPLETE, true);
		yield break;
	}

	// Token: 0x06005D21 RID: 23841 RVA: 0x001E403E File Offset: 0x001E223E
	public static SimpleRewardData CreateArcaneOrbRewardData(int amount)
	{
		return new SimpleRewardData(Reward.Type.ARCANE_ORBS, amount)
		{
			RewardHeadlineText = GameStrings.Get("GLOBAL_REWARD_ARCANE_ORBS_HEADLINE")
		};
	}

	// Token: 0x06005D22 RID: 23842 RVA: 0x001E4058 File Offset: 0x001E2258
	public static DeckRewardData CreateDeckRewardData(int deckId, int classId)
	{
		return new DeckRewardData(deckId, classId);
	}

	// Token: 0x06005D23 RID: 23843 RVA: 0x001E4064 File Offset: 0x001E2264
	public static bool AttemptDeckTemplateLookupFromSellableDeckId(int deckId, out DeckTemplateDbfRecord deckTemplateRecord)
	{
		deckTemplateRecord = null;
		List<SellableDeckDbfRecord> records = GameDbf.SellableDeck.GetRecords((SellableDeckDbfRecord r) => r.DeckTemplateRecord != null && r.DeckTemplateRecord.DeckId == deckId, -1);
		if (records.Count == 0)
		{
			Log.Store.PrintWarning("[RewardUtils.AttemptDeckTemplateLookupFromSellableDeckId] Failed to find DB record for deck reward! (ID {0})", new object[]
			{
				deckId
			});
			return false;
		}
		if (records.Count > 1)
		{
			Log.Store.PrintWarning("[RewardUtils.AttemptDeckTemplateLookupFromSellableDeckId] Found multiple rewardable deck records that grant the same deck! (ID {0})", new object[]
			{
				deckId
			});
			return false;
		}
		SellableDeckDbfRecord sellableDeckDbfRecord = records[0];
		if (sellableDeckDbfRecord.DeckTemplateRecord == null || sellableDeckDbfRecord.DeckTemplateRecord.DeckRecord == null)
		{
			Log.Store.PrintWarning("[RewardUtils.AttemptDeckTemplateLookupFromSellableDeckId] The DB record {0} for deck reward does NOT have a deck template with a valid deck record!", new object[]
			{
				sellableDeckDbfRecord.ID
			});
			return false;
		}
		deckTemplateRecord = sellableDeckDbfRecord.DeckTemplateRecord;
		return true;
	}

	// Token: 0x06005D24 RID: 23844 RVA: 0x001E4140 File Offset: 0x001E2340
	public static bool CopyDeckTemplateRecordToClipboard(DeckTemplateDbfRecord deckTemplateDBfRecord, bool usePremiumCardsFromCollection = true)
	{
		ShareableDeck shareableDeckFromTemplateRecord = CollectionManager.GetShareableDeckFromTemplateRecord(deckTemplateDBfRecord, usePremiumCardsFromCollection);
		if (shareableDeckFromTemplateRecord == null)
		{
			Log.Store.PrintWarning("[RewardUtils.CopyDeckTemplateRecordToClipboard] Failed to get a sharable deck for deck template record {0}!", new object[]
			{
				deckTemplateDBfRecord.ID
			});
			return false;
		}
		ClipboardUtils.CopyToClipboard(shareableDeckFromTemplateRecord.Serialize(true));
		return true;
	}

	// Token: 0x06005D25 RID: 23845 RVA: 0x001E418C File Offset: 0x001E238C
	public static RewardItemDataModel RewardDataToRewardItemDataModel(RewardData rewardData)
	{
		TAG_RARITY rarity = TAG_RARITY.INVALID;
		TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
		RewardItemDataModel rewardItemDataModel = null;
		Reward.Type rewardType = rewardData.RewardType;
		switch (rewardType)
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
		case Reward.Type.CARD:
		{
			CardRewardData cardRewardData = rewardData as CardRewardData;
			RewardItemDataModel rewardItemDataModel2 = new RewardItemDataModel
			{
				ItemType = RewardItemType.CARD,
				ItemId = GameUtils.TranslateCardIdToDbId(cardRewardData.CardID, false),
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
		default:
			if (rewardType != Reward.Type.RANDOM_CARD)
			{
				if (rewardType != Reward.Type.ARCANE_ORBS)
				{
					Log.All.PrintWarning("RewardDataToRewardItemDataModel() - RewardData of type {0} is not currently supported!", new object[]
					{
						rewardData.RewardType
					});
				}
				else
				{
					SimpleRewardData simpleRewardData = rewardData as SimpleRewardData;
					rewardItemDataModel = new RewardItemDataModel
					{
						ItemType = RewardItemType.ARCANE_ORBS,
						Quantity = simpleRewardData.Amount
					};
				}
			}
			else
			{
				RandomCardRewardData randomCardRewardData = rewardData as RandomCardRewardData;
				rewardItemDataModel = new RewardItemDataModel
				{
					ItemType = RewardItemType.RANDOM_CARD,
					Quantity = randomCardRewardData.Count
				};
				rarity = randomCardRewardData.Rarity;
				premium = randomCardRewardData.Premium;
			}
			break;
		}
		if (rewardItemDataModel != null)
		{
			string warningPrefix = string.Format("RewardData Error [Type = {0}]: ", rewardData.RewardType);
			if (!RewardUtils.InitializeRewardItemDataModel(rewardItemDataModel, rarity, premium, warningPrefix))
			{
				rewardItemDataModel = null;
			}
		}
		return rewardItemDataModel;
	}

	// Token: 0x06005D26 RID: 23846 RVA: 0x001E4352 File Offset: 0x001E2552
	public static RewardListDataModel CreateRewardListDataModelFromRewardListId(int rewardListId, int chooseOneRewardItemId = 0, List<RewardItemOutput> rewardItemOutputs = null)
	{
		return RewardUtils.CreateRewardListDataModelFromRewardListRecord(GameDbf.RewardList.GetRecord(rewardListId), chooseOneRewardItemId, rewardItemOutputs);
	}

	// Token: 0x06005D27 RID: 23847 RVA: 0x001E4368 File Offset: 0x001E2568
	public static RewardListDataModel CreateRewardListDataModelFromRewardListRecord(RewardListDbfRecord rewardListRecord, int chooseOneRewardItemId = 0, List<RewardItemOutput> rewardItemOutputs = null)
	{
		if (rewardListRecord == null)
		{
			return null;
		}
		RewardListDataModel rewardListDataModel = new RewardListDataModel();
		rewardListDataModel.ChooseOne = rewardListRecord.ChooseOne;
		rewardListDataModel.Items = (from r in rewardListRecord.RewardItems
		where chooseOneRewardItemId <= 0 || r.ID == chooseOneRewardItemId
		select r).SelectMany(delegate(RewardItemDbfRecord r)
		{
			RewardItemDbfRecord r2 = r;
			List<RewardItemOutput> rewardItemOutputs2 = rewardItemOutputs;
			int? rewardItemOutputData;
			if (rewardItemOutputs2 == null)
			{
				rewardItemOutputData = null;
			}
			else
			{
				RewardItemOutput rewardItemOutput = rewardItemOutputs2.Find((RewardItemOutput rio) => rio.RewardItemId == r.ID);
				rewardItemOutputData = ((rewardItemOutput != null) ? new int?(rewardItemOutput.OutputData) : null);
			}
			return RewardFactory.CreateRewardItemDataModel(r2, rewardItemOutputData);
		}).OrderBy((RewardItemDataModel item) => item, new RewardUtils.RewardItemComparer()).ToDataModelList<RewardItemDataModel>();
		rewardListDataModel.Description = rewardListRecord.Description;
		return rewardListDataModel;
	}

	// Token: 0x06005D28 RID: 23848 RVA: 0x001E440C File Offset: 0x001E260C
	public static bool InitializeRewardItemDataModelForShop(RewardItemDataModel item, Network.BundleItem netBundleItem = null, string warningPrefix = null)
	{
		TAG_RARITY rarity = TAG_RARITY.INVALID;
		TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
		RewardItemType itemType = item.ItemType;
		if (itemType != RewardItemType.HERO_SKIN)
		{
			if (itemType != RewardItemType.RANDOM_CARD)
			{
				if (itemType == RewardItemType.CARD)
				{
					premium = TAG_PREMIUM.NORMAL;
				}
			}
			else
			{
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
				rarity = RewardUtils.GetRarityForRandomCardReward(item.ItemId);
			}
		}
		else
		{
			premium = TAG_PREMIUM.GOLDEN;
		}
		return RewardUtils.InitializeRewardItemDataModel(item, rarity, premium, warningPrefix);
	}

	// Token: 0x06005D29 RID: 23849 RVA: 0x001E44BC File Offset: 0x001E26BC
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
				return true;
			}
			if (warningPrefix != null)
			{
				Log.All.PrintWarning("{0} Booster Item has unrecognized booster id [{1}]", new object[]
				{
					warningPrefix,
					item.ItemId
				});
				return result;
			}
			return result;
		case RewardItemType.DUST:
		case RewardItemType.RUNESTONES:
		case RewardItemType.ARCANE_ORBS:
			item.Currency = new PriceDataModel
			{
				Currency = RewardUtils.RewardItemTypeToCurrencyType(item.ItemType),
				Amount = (float)item.Quantity,
				DisplayText = item.Quantity.ToString()
			};
			return !ShopUtils.IsCurrencyVirtual(item.Currency.Currency) || ShopUtils.IsVirtualCurrencyEnabled();
		case RewardItemType.HERO_SKIN:
		{
			CardDbfRecord record = GameDbf.Card.GetRecord(item.ItemId);
			if (record == null)
			{
				Log.All.PrintWarning("{0} Hero Skin Item has unknown card id [{1}]", new object[]
				{
					warningPrefix,
					item.ItemId
				});
				return result;
			}
			if (record.CardHero != null)
			{
				item.Card = new CardDataModel
				{
					CardId = record.NoteMiniGuid,
					Premium = premium
				};
				return true;
			}
			Log.All.PrintWarning("{0} Hero Skin Item has invalid card id [{1}] where card dbf record has no CARD_HERO subtable. NoteMiniGuid = {2}", new object[]
			{
				warningPrefix,
				item.ItemId,
				record.NoteMiniGuid
			});
			return result;
		}
		case RewardItemType.CARD_BACK:
			if (GameDbf.CardBack.HasRecord(item.ItemId))
			{
				item.CardBack = new CardBackDataModel
				{
					CardBackId = item.ItemId
				};
				return true;
			}
			if (warningPrefix != null)
			{
				Log.All.PrintWarning("{0} Card Back Item has unrecognized card back id [{1}]", new object[]
				{
					warningPrefix,
					item.ItemId
				});
				return result;
			}
			return result;
		case RewardItemType.ADVENTURE_WING:
			if (GameDbf.Wing.HasRecord(item.ItemId))
			{
				return true;
			}
			Log.All.PrintWarning("{0} Adventure Wing Item has unknown adventure wing id [{1}]", new object[]
			{
				warningPrefix,
				item.ItemId
			});
			return result;
		case RewardItemType.ARENA_TICKET:
		case RewardItemType.BATTLEGROUNDS_BONUS:
		case RewardItemType.TAVERN_BRAWL_TICKET:
		case RewardItemType.PROGRESSION_BONUS:
		case RewardItemType.REWARD_TRACK_XP_BOOST:
			return true;
		case RewardItemType.RANDOM_CARD:
			item.RandomCard = new RandomCardDataModel
			{
				Premium = premium,
				Rarity = rarity,
				Count = item.Quantity
			};
			return rarity > TAG_RARITY.INVALID;
		case RewardItemType.CARD:
		{
			CardDbfRecord record2 = GameDbf.Card.GetRecord(item.ItemId);
			if (record2 != null)
			{
				item.Card = new CardDataModel
				{
					CardId = record2.NoteMiniGuid,
					Premium = premium
				};
				return true;
			}
			Log.All.PrintWarning("{0} Card Item has unknown card id [{1}]", new object[]
			{
				warningPrefix,
				item.ItemId
			});
			return result;
		}
		case RewardItemType.CUSTOM_COIN:
		{
			CoinDbfRecord record3 = GameDbf.Coin.GetRecord(item.ItemId);
			if (record3 == null)
			{
				Log.All.PrintWarning("{0} Custom Coin Item has unknown id [{1}]", new object[]
				{
					warningPrefix,
					item.ItemId
				});
				return result;
			}
			CardDbfRecord record4 = GameDbf.Card.GetRecord(record3.CardId);
			if (record4 != null)
			{
				item.Card = new CardDataModel
				{
					CardId = record4.NoteMiniGuid,
					Premium = premium
				};
				return true;
			}
			Log.All.PrintWarning("{0} Custom Coin Item has unknown card id [{1}]", new object[]
			{
				warningPrefix,
				record3.CardId
			});
			return result;
		}
		case RewardItemType.MINI_SET:
		{
			int itemId = item.ItemId;
			if (GameDbf.MiniSet.GetRecord(itemId) == null)
			{
				Log.All.PrintWarning("{0} Mini-Set has unknown MINI_SET id [{1}]", new object[]
				{
					warningPrefix,
					itemId
				});
				return result;
			}
			return true;
		}
		case RewardItemType.SELLABLE_DECK:
		{
			int itemId2 = item.ItemId;
			if (GameDbf.SellableDeck.GetRecord(itemId2) == null)
			{
				Log.All.PrintWarning("{0} Sellable deck has unknown SELLABLE_DECK id [{1}]", new object[]
				{
					warningPrefix,
					itemId2
				});
				return result;
			}
			return true;
		}
		}
		Log.All.PrintWarning("{0} Item has unsupported item type [{1}]", new object[]
		{
			warningPrefix,
			item.ItemType
		});
		return result;
	}

	// Token: 0x06005D2A RID: 23850 RVA: 0x001E4937 File Offset: 0x001E2B37
	public static global::CurrencyType RewardItemTypeToCurrencyType(RewardItemType itemType)
	{
		if (itemType == RewardItemType.DUST)
		{
			return global::CurrencyType.DUST;
		}
		if (itemType == RewardItemType.RUNESTONES)
		{
			return global::CurrencyType.RUNESTONES;
		}
		if (itemType != RewardItemType.ARCANE_ORBS)
		{
			return global::CurrencyType.NONE;
		}
		return global::CurrencyType.ARCANE_ORBS;
	}

	// Token: 0x06005D2B RID: 23851 RVA: 0x001E4950 File Offset: 0x001E2B50
	public static int GetRewardItemTypeSortOrder(RewardItemType itemType)
	{
		switch (itemType)
		{
		case RewardItemType.BOOSTER:
			return 800;
		case RewardItemType.DUST:
			return 1100;
		case RewardItemType.HERO_SKIN:
			return 100;
		case RewardItemType.CARD_BACK:
			return 200;
		case RewardItemType.ADVENTURE_WING:
			return 700;
		case RewardItemType.ARENA_TICKET:
			return 500;
		case RewardItemType.RANDOM_CARD:
			return 400;
		case RewardItemType.RUNESTONES:
			return 900;
		case RewardItemType.ARCANE_ORBS:
			return 1000;
		case RewardItemType.ADVENTURE:
			return 600;
		case RewardItemType.CARD:
			return 300;
		case RewardItemType.BATTLEGROUNDS_BONUS:
			return 450;
		case RewardItemType.TAVERN_BRAWL_TICKET:
			return 550;
		case RewardItemType.PROGRESSION_BONUS:
			return 1200;
		case RewardItemType.REWARD_TRACK_XP_BOOST:
			return 50;
		case RewardItemType.MINI_SET:
			return 1300;
		case RewardItemType.CARD_SUBSET:
			return 425;
		case RewardItemType.SELLABLE_DECK:
			return 1400;
		}
		return int.MaxValue;
	}

	// Token: 0x06005D2C RID: 23852 RVA: 0x001E4A24 File Offset: 0x001E2C24
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
		int rewardItemTypeSortOrder = RewardUtils.GetRewardItemTypeSortOrder(xItem.ItemType);
		int rewardItemTypeSortOrder2 = RewardUtils.GetRewardItemTypeSortOrder(yItem.ItemType);
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

	// Token: 0x06005D2D RID: 23853 RVA: 0x001E4AE6 File Offset: 0x001E2CE6
	public static void SetNewRewardedDeck(long collectionDeckId)
	{
		Options.Get().SetLong(Option.NEWEST_REWARDED_DECK_ID, collectionDeckId);
	}

	// Token: 0x06005D2E RID: 23854 RVA: 0x001E4AF5 File Offset: 0x001E2CF5
	public static bool HasNewRewardedDeck(out long collectionDeckId)
	{
		collectionDeckId = Options.Get().GetLong(Option.NEWEST_REWARDED_DECK_ID);
		return collectionDeckId != 0L;
	}

	// Token: 0x06005D2F RID: 23855 RVA: 0x001E4B0B File Offset: 0x001E2D0B
	public static void MarkNewestRewardedDeckAsSeen()
	{
		RewardUtils.SetNewRewardedDeck(0L);
	}

	// Token: 0x06005D30 RID: 23856 RVA: 0x001E4B14 File Offset: 0x001E2D14
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

	// Token: 0x06005D31 RID: 23857 RVA: 0x001E4B7C File Offset: 0x001E2D7C
	public static int GetSortOrderFromItems(DataModelList<RewardItemDataModel> items)
	{
		using (IEnumerator<RewardItemDataModel> enumerator = items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int result;
				if (RewardUtils.AttemptToGetItemSortOrder(enumerator.Current, out result))
				{
					return result;
				}
			}
		}
		return 0;
	}

	// Token: 0x06005D32 RID: 23858 RVA: 0x001E4BCC File Offset: 0x001E2DCC
	public static bool AttemptToGetItemSortOrder(RewardItemDataModel item, out int sortOrder)
	{
		if (item != null && item.ItemType == RewardItemType.SELLABLE_DECK && RewardUtils.IsValidSellableDeckRecordId(item.ItemId))
		{
			sortOrder = RewardUtils.GetSortOrderForSellableDeck(item.ItemId);
			return true;
		}
		sortOrder = 0;
		return false;
	}

	// Token: 0x06005D33 RID: 23859 RVA: 0x001E4BFB File Offset: 0x001E2DFB
	public static bool IsValidSellableDeckRecordId(int sellableDeckRecordId)
	{
		SellableDeckDbfRecord record = GameDbf.SellableDeck.GetRecord(sellableDeckRecordId);
		return ((record != null) ? record.DeckTemplateRecord : null) != null;
	}

	// Token: 0x06005D34 RID: 23860 RVA: 0x001E4C17 File Offset: 0x001E2E17
	public static int GetSortOrderForSellableDeck(int sellableDeckRecordId)
	{
		return GameDbf.SellableDeck.GetRecord(sellableDeckRecordId).DeckTemplateRecord.SortOrder;
	}

	// Token: 0x04004ED7 RID: 20183
	public static readonly Vector3 REWARD_HIDDEN_SCALE = new Vector3(0.001f, 0.001f, 0.001f);

	// Token: 0x04004ED8 RID: 20184
	public static readonly float REWARD_HIDE_TIME = 0.25f;

	// Token: 0x04004ED9 RID: 20185
	private static readonly AssetReference QUEST_REWARDS_TEXTURE_PAGE_2 = new AssetReference("QuestRewards2.psd:1de88a86bd486434dab6ab887ca40254");

	// Token: 0x04004EDA RID: 20186
	private static readonly AssetReference QUEST_REWARDS_TEXTURE_PAGE_3 = new AssetReference("QuestRewards3.tif:2ea38c006b55fe6428ec79fa68f5dbe2");

	// Token: 0x04004EDB RID: 20187
	private static readonly AssetReference ARCANE_ORB_ICON = new AssetReference("Shop_VC2_Arcane_Orb_Icon.tif:b47e50430b8b4554688cc9e385ced3f2");

	// Token: 0x02002189 RID: 8585
	public class RewardItemComparer : IComparer<RewardItemDataModel>
	{
		// Token: 0x060123E4 RID: 74724 RVA: 0x00502085 File Offset: 0x00500285
		public int Compare(RewardItemDataModel first, RewardItemDataModel second)
		{
			return RewardUtils.CompareItemsForSort(first, second);
		}
	}

	// Token: 0x0200218A RID: 8586
	public class RewardOwnedItemComparer : IComparer<RewardItemDataModel>
	{
		// Token: 0x060123E6 RID: 74726 RVA: 0x0050208E File Offset: 0x0050028E
		public int Compare(RewardItemDataModel first, RewardItemDataModel second)
		{
			return RewardUtils.CompareOwnedItemsForSort(first, second);
		}
	}
}
