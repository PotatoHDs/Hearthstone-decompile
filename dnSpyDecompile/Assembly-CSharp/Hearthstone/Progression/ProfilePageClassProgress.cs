using System;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001112 RID: 4370
	[RequireComponent(typeof(WidgetTemplate))]
	public class ProfilePageClassProgress : MonoBehaviour
	{
		// Token: 0x0600BF5A RID: 48986 RVA: 0x003A47E2 File Offset: 0x003A29E2
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.UpdateClassIcons();
		}

		// Token: 0x0600BF5B RID: 48987 RVA: 0x003A47F8 File Offset: 0x003A29F8
		private void UpdateClassIcons()
		{
			List<global::Achievement> achievesInGroup = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO, true);
			List<global::Achievement> achievesInGroup2 = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.GOLDHERO);
			List<global::Achievement> achievesInGroup3 = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.PREMIUMHERO);
			NetCache.NetCacheHeroLevels netObject = NetCache.Get().GetNetObject<NetCache.NetCacheHeroLevels>();
			if (netObject == null)
			{
				return;
			}
			int totalLevel = GameUtils.GetTotalHeroLevel() ?? 0;
			ProfileClassIconListDataModel profileClassIconListDataModel = new ProfileClassIconListDataModel();
			ProfileClassIconListDataModel profileClassIconListDataModel2 = new ProfileClassIconListDataModel();
			TAG_CLASS[] class_TAB_ORDER = CollectionPageManager.CLASS_TAB_ORDER;
			for (int i = 0; i < class_TAB_ORDER.Length; i++)
			{
				TAG_CLASS tagClass = class_TAB_ORDER[i];
				if (tagClass != TAG_CLASS.NEUTRAL)
				{
					ProfileClassIconDataModel profileClassIconDataModel = new ProfileClassIconDataModel();
					profileClassIconDataModel.Name = GameStrings.GetClassName(tagClass);
					profileClassIconDataModel.TagClass = tagClass;
					global::Achievement achievement = (achievesInGroup != null) ? achievesInGroup.Find((global::Achievement o) => o.ClassReward != null && o.ClassReward.Value == tagClass) : null;
					profileClassIconDataModel.IsUnlocked = (achievement != null);
					global::Achievement achievement2 = (achievesInGroup2 != null) ? achievesInGroup2.Find((global::Achievement o) => o.MyHeroClassRequirement != null && o.MyHeroClassRequirement.Value == tagClass) : null;
					profileClassIconDataModel.IsGolden = achievement2.IsCompleted();
					profileClassIconDataModel.GoldWinsReq = achievement2.MaxProgress;
					global::Achievement achievement3 = (achievesInGroup3 != null) ? achievesInGroup3.Find((global::Achievement o) => o.MyHeroClassRequirement != null && o.MyHeroClassRequirement.Value == tagClass) : null;
					profileClassIconDataModel.IsPremium = achievement3.IsCompleted();
					profileClassIconDataModel.PremiumWinsReq = achievement3.MaxProgress;
					NetCache.HeroLevel heroLevel = netObject.Levels.Find((NetCache.HeroLevel o) => o.Class == tagClass);
					profileClassIconDataModel.CurrentLevel = heroLevel.CurrentLevel.Level;
					profileClassIconDataModel.MaxLevel = heroLevel.CurrentLevel.MaxLevel;
					profileClassIconDataModel.CurrentLevelXP = heroLevel.CurrentLevel.XP;
					profileClassIconDataModel.CurrentLevelXPMax = heroLevel.CurrentLevel.MaxXP;
					profileClassIconDataModel.IsMaxLevel = heroLevel.CurrentLevel.IsMaxLevel();
					profileClassIconDataModel.Wins = (profileClassIconDataModel.IsGolden ? achievement3.Progress : achievement2.Progress);
					profileClassIconDataModel.WinsText = GameStrings.Format("GLOBAL_PROGRESSION_PROFILE_ARENA_WINS", new object[]
					{
						profileClassIconDataModel.Wins
					});
					string tooltipTitle;
					string tooltipDesc;
					if (!profileClassIconDataModel.IsUnlocked)
					{
						string text = "";
						string heroCardId = CollectionManager.GetHeroCardId(tagClass, CardHero.HeroType.VANILLA);
						if (!string.IsNullOrEmpty(heroCardId))
						{
							DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(heroCardId, null);
							if (((fullDef != null) ? fullDef.CardDef : null) != null)
							{
								text = fullDef.EntityDef.GetShortName();
							}
						}
						profileClassIconDataModel.TooltipTitle = profileClassIconDataModel.Name;
						profileClassIconDataModel.TooltipDesc = GameStrings.Format("GLUE_HERO_LOCKED_DESC", new object[]
						{
							text,
							profileClassIconDataModel.Name
						});
					}
					else if (RewardUtils.GetNextHeroLevelRewardText(tagClass, heroLevel.CurrentLevel.Level, totalLevel, out tooltipTitle, out tooltipDesc))
					{
						profileClassIconDataModel.TooltipTitle = tooltipTitle;
						profileClassIconDataModel.TooltipDesc = tooltipDesc;
					}
					else if (heroLevel.CurrentLevel.IsMaxLevel())
					{
						profileClassIconDataModel.TooltipTitle = GameStrings.Format("GLOBAL_PROGRESSION_TOOLTIP_TOTAL_CLASS_WINS", new object[]
						{
							profileClassIconDataModel.Name
						});
						if (profileClassIconDataModel.IsPremium)
						{
							profileClassIconDataModel.TooltipDesc = GameStrings.Format("GLOBAL_PROGRESSION_TOOLTIP_PREMIUM_WINS_DONE_DESC", new object[]
							{
								profileClassIconDataModel.PremiumWinsReq,
								profileClassIconDataModel.Name,
								achievement3.Name
							});
						}
						else if (profileClassIconDataModel.IsGolden)
						{
							profileClassIconDataModel.TooltipDesc = GameStrings.Format("GLOBAL_PROGRESSION_TOOLTIP_PREMIUM_WINS_DESC", new object[]
							{
								profileClassIconDataModel.PremiumWinsReq,
								profileClassIconDataModel.Name,
								achievement3.Name
							});
						}
						else
						{
							profileClassIconDataModel.TooltipDesc = GameStrings.Format("GLOBAL_PROGRESSION_TOOLTIP_GOLDEN_WINS_DESC", new object[]
							{
								profileClassIconDataModel.GoldWinsReq,
								profileClassIconDataModel.Name
							});
						}
					}
					else
					{
						profileClassIconDataModel.TooltipTitle = profileClassIconDataModel.Name;
						profileClassIconDataModel.TooltipDesc = GameStrings.Format("GLOBAL_PROGRESSION_TOOLTIP_CLASS_DEFAULT_DESC", new object[]
						{
							profileClassIconDataModel.CurrentLevel
						});
					}
					if (profileClassIconListDataModel.Icons.Count < this.m_maxClassIconsPerRow)
					{
						profileClassIconListDataModel.Icons.Add(profileClassIconDataModel);
					}
					else
					{
						profileClassIconListDataModel2.Icons.Add(profileClassIconDataModel);
					}
				}
			}
			this.m_upperClassIconList.BindDataModel(profileClassIconListDataModel, false);
			this.m_lowerClassIconList.BindDataModel(profileClassIconListDataModel2, false);
		}

		// Token: 0x04009B67 RID: 39783
		public Widget m_upperClassIconList;

		// Token: 0x04009B68 RID: 39784
		public Widget m_lowerClassIconList;

		// Token: 0x04009B69 RID: 39785
		public int m_maxClassIconsPerRow;

		// Token: 0x04009B6A RID: 39786
		private Widget m_widget;
	}
}
