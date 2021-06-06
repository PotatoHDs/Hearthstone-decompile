using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class ProfilePageClassProgress : MonoBehaviour
	{
		public Widget m_upperClassIconList;

		public Widget m_lowerClassIconList;

		public int m_maxClassIconsPerRow;

		private Widget m_widget;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			UpdateClassIcons();
		}

		private void UpdateClassIcons()
		{
			List<global::Achievement> achievesInGroup = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO, isComplete: true);
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
			TAG_CLASS[] cLASS_TAB_ORDER = CollectionPageManager.CLASS_TAB_ORDER;
			foreach (TAG_CLASS tagClass in cLASS_TAB_ORDER)
			{
				if (tagClass == TAG_CLASS.NEUTRAL)
				{
					continue;
				}
				ProfileClassIconDataModel profileClassIconDataModel = new ProfileClassIconDataModel();
				profileClassIconDataModel.Name = GameStrings.GetClassName(tagClass);
				profileClassIconDataModel.TagClass = tagClass;
				global::Achievement achievement = achievesInGroup?.Find((global::Achievement o) => o.ClassReward.HasValue && o.ClassReward.Value == tagClass);
				profileClassIconDataModel.IsUnlocked = achievement != null;
				global::Achievement achievement2 = achievesInGroup2?.Find((global::Achievement o) => o.MyHeroClassRequirement.HasValue && o.MyHeroClassRequirement.Value == tagClass);
				profileClassIconDataModel.IsGolden = achievement2.IsCompleted();
				profileClassIconDataModel.GoldWinsReq = achievement2.MaxProgress;
				global::Achievement achievement3 = achievesInGroup3?.Find((global::Achievement o) => o.MyHeroClassRequirement.HasValue && o.MyHeroClassRequirement.Value == tagClass);
				profileClassIconDataModel.IsPremium = achievement3.IsCompleted();
				profileClassIconDataModel.PremiumWinsReq = achievement3.MaxProgress;
				NetCache.HeroLevel heroLevel = netObject.Levels.Find((NetCache.HeroLevel o) => o.Class == tagClass);
				profileClassIconDataModel.CurrentLevel = heroLevel.CurrentLevel.Level;
				profileClassIconDataModel.MaxLevel = heroLevel.CurrentLevel.MaxLevel;
				profileClassIconDataModel.CurrentLevelXP = heroLevel.CurrentLevel.XP;
				profileClassIconDataModel.CurrentLevelXPMax = heroLevel.CurrentLevel.MaxXP;
				profileClassIconDataModel.IsMaxLevel = heroLevel.CurrentLevel.IsMaxLevel();
				profileClassIconDataModel.Wins = (profileClassIconDataModel.IsGolden ? achievement3.Progress : achievement2.Progress);
				profileClassIconDataModel.WinsText = GameStrings.Format("GLOBAL_PROGRESSION_PROFILE_ARENA_WINS", profileClassIconDataModel.Wins);
				string nextRewardTitle;
				string nextRewardDescription;
				if (!profileClassIconDataModel.IsUnlocked)
				{
					string text = "";
					string heroCardId = CollectionManager.GetHeroCardId(tagClass, CardHero.HeroType.VANILLA);
					if (!string.IsNullOrEmpty(heroCardId))
					{
						DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(heroCardId);
						if (fullDef?.CardDef != null)
						{
							text = fullDef.EntityDef.GetShortName();
						}
					}
					profileClassIconDataModel.TooltipTitle = profileClassIconDataModel.Name;
					profileClassIconDataModel.TooltipDesc = GameStrings.Format("GLUE_HERO_LOCKED_DESC", text, profileClassIconDataModel.Name);
				}
				else if (RewardUtils.GetNextHeroLevelRewardText(tagClass, heroLevel.CurrentLevel.Level, totalLevel, out nextRewardTitle, out nextRewardDescription))
				{
					profileClassIconDataModel.TooltipTitle = nextRewardTitle;
					profileClassIconDataModel.TooltipDesc = nextRewardDescription;
				}
				else if (heroLevel.CurrentLevel.IsMaxLevel())
				{
					profileClassIconDataModel.TooltipTitle = GameStrings.Format("GLOBAL_PROGRESSION_TOOLTIP_TOTAL_CLASS_WINS", profileClassIconDataModel.Name);
					if (profileClassIconDataModel.IsPremium)
					{
						profileClassIconDataModel.TooltipDesc = GameStrings.Format("GLOBAL_PROGRESSION_TOOLTIP_PREMIUM_WINS_DONE_DESC", profileClassIconDataModel.PremiumWinsReq, profileClassIconDataModel.Name, achievement3.Name);
					}
					else if (profileClassIconDataModel.IsGolden)
					{
						profileClassIconDataModel.TooltipDesc = GameStrings.Format("GLOBAL_PROGRESSION_TOOLTIP_PREMIUM_WINS_DESC", profileClassIconDataModel.PremiumWinsReq, profileClassIconDataModel.Name, achievement3.Name);
					}
					else
					{
						profileClassIconDataModel.TooltipDesc = GameStrings.Format("GLOBAL_PROGRESSION_TOOLTIP_GOLDEN_WINS_DESC", profileClassIconDataModel.GoldWinsReq, profileClassIconDataModel.Name);
					}
				}
				else
				{
					profileClassIconDataModel.TooltipTitle = profileClassIconDataModel.Name;
					profileClassIconDataModel.TooltipDesc = GameStrings.Format("GLOBAL_PROGRESSION_TOOLTIP_CLASS_DEFAULT_DESC", profileClassIconDataModel.CurrentLevel);
				}
				if (profileClassIconListDataModel.Icons.Count < m_maxClassIconsPerRow)
				{
					profileClassIconListDataModel.Icons.Add(profileClassIconDataModel);
				}
				else
				{
					profileClassIconListDataModel2.Icons.Add(profileClassIconDataModel);
				}
			}
			m_upperClassIconList.BindDataModel(profileClassIconListDataModel);
			m_lowerClassIconList.BindDataModel(profileClassIconListDataModel2);
		}
	}
}
