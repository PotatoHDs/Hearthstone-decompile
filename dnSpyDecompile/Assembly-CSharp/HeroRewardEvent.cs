using System;
using System.Collections.Generic;
using Assets;
using UnityEngine;

// Token: 0x020002CF RID: 719
public class HeroRewardEvent : MonoBehaviour
{
	// Token: 0x060025D2 RID: 9682 RVA: 0x000BE33C File Offset: 0x000BC53C
	private void Awake()
	{
		this.LoadHeroCardDefs();
	}

	// Token: 0x060025D3 RID: 9683 RVA: 0x000BE344 File Offset: 0x000BC544
	private void OnDestroy()
	{
		DefLoader.DisposableCardDef vanillaHeroCardDef = this.m_VanillaHeroCardDef;
		if (vanillaHeroCardDef != null)
		{
			vanillaHeroCardDef.Dispose();
		}
		DefLoader.DisposableCardDef premiumHeroCardDef = this.m_PremiumHeroCardDef;
		if (premiumHeroCardDef == null)
		{
			return;
		}
		premiumHeroCardDef.Dispose();
	}

	// Token: 0x060025D4 RID: 9684 RVA: 0x000BE367 File Offset: 0x000BC567
	public void Show()
	{
		base.gameObject.SetActive(true);
		this.m_playmaker.SendEvent("Action");
		this.m_victoryTwoScoop.HideXpBar();
		this.m_victoryTwoScoop.m_bannerLabel.Text = "";
	}

	// Token: 0x060025D5 RID: 9685 RVA: 0x000BE3A5 File Offset: 0x000BC5A5
	public void Hide()
	{
		this.m_playmaker.SendEvent("Done");
		SoundManager.Get().LoadAndPlay("rank_window_shrink.prefab:9c6393a1d207a07439c22f31ef405a7c");
	}

	// Token: 0x060025D6 RID: 9686 RVA: 0x000BE3CB File Offset: 0x000BC5CB
	public void SetHeroBurnAwayTexture(Texture heroTexture)
	{
		this.m_burningHero.GetComponent<Renderer>().GetMaterial().mainTexture = heroTexture;
	}

	// Token: 0x060025D7 RID: 9687 RVA: 0x000BE3E3 File Offset: 0x000BC5E3
	public void HideTwoScoop()
	{
		this.m_victoryTwoScoop.Hide();
	}

	// Token: 0x060025D8 RID: 9688 RVA: 0x000BE3F0 File Offset: 0x000BC5F0
	public void HideHeroActor()
	{
		this.m_victoryTwoScoop.m_heroActor.Hide();
	}

	// Token: 0x060025D9 RID: 9689 RVA: 0x000BE402 File Offset: 0x000BC602
	public void SetVictoryTwoScoop(VictoryTwoScoop twoScoop)
	{
		this.m_victoryTwoScoop = twoScoop;
	}

	// Token: 0x060025DA RID: 9690 RVA: 0x000BE40B File Offset: 0x000BC60B
	public void SetRewardAchieve(global::Achievement achieve, QuestToast.DelOnCloseQuestToast continueCallback)
	{
		this.m_RewardAchieve = achieve;
		this.m_ContinueCallback = continueCallback;
	}

	// Token: 0x060025DB RID: 9691 RVA: 0x000BE41B File Offset: 0x000BC61B
	public void SwapHeroToVanilla()
	{
		if (this.m_VanillaHeroCardDef == null)
		{
			return;
		}
		this.m_victoryTwoScoop.m_heroActor.SetCardDef(this.m_VanillaHeroCardDef);
		this.m_victoryTwoScoop.m_heroActor.UpdateAllComponents();
	}

	// Token: 0x060025DC RID: 9692 RVA: 0x000BE44C File Offset: 0x000BC64C
	public void SwapMaterialToPremium()
	{
		this.m_victoryTwoScoop.m_heroActor.SetPremium(TAG_PREMIUM.GOLDEN);
		this.m_victoryTwoScoop.m_heroActor.UpdateAllComponents();
	}

	// Token: 0x060025DD RID: 9693 RVA: 0x000BE46F File Offset: 0x000BC66F
	public void SwapHeroToPremium()
	{
		this.m_victoryTwoScoop.m_heroActor.SetCardDef(this.m_PremiumHeroCardDef);
		this.m_victoryTwoScoop.m_heroActor.UpdateAllComponents();
	}

	// Token: 0x060025DE RID: 9694 RVA: 0x000BE497 File Offset: 0x000BC697
	public void ShowHeroRewardBanner()
	{
		if (this.m_RewardAchieve != null)
		{
			QuestToast.ShowQuestToast(UserAttentionBlocker.NONE, this.m_ContinueCallback, false, this.m_RewardAchieve);
		}
	}

	// Token: 0x060025DF RID: 9695 RVA: 0x000BE4B4 File Offset: 0x000BC6B4
	public void AnimationDone()
	{
		this.FireAnimationDoneEvent();
	}

	// Token: 0x060025E0 RID: 9696 RVA: 0x000BE4BC File Offset: 0x000BC6BC
	private void FireAnimationDoneEvent()
	{
		HeroRewardEvent.AnimationDoneListener[] array = this.m_animationDoneListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x060025E1 RID: 9697 RVA: 0x000BE4EB File Offset: 0x000BC6EB
	public void RegisterAnimationDoneListener(HeroRewardEvent.AnimationDoneListener listener)
	{
		if (this.m_animationDoneListeners.Contains(listener))
		{
			return;
		}
		this.m_animationDoneListeners.Add(listener);
	}

	// Token: 0x060025E2 RID: 9698 RVA: 0x000BE508 File Offset: 0x000BC708
	public void RemoveAnimationDoneListener(HeroRewardEvent.AnimationDoneListener listener)
	{
		this.m_animationDoneListeners.Remove(listener);
	}

	// Token: 0x060025E3 RID: 9699 RVA: 0x000BE518 File Offset: 0x000BC718
	private void LoadHeroCardDefs()
	{
		Player player = null;
		foreach (Player player2 in GameState.Get().GetPlayerMap().Values)
		{
			if (player2.GetSide() == Player.Side.FRIENDLY)
			{
				player = player2;
				break;
			}
		}
		if (player == null)
		{
			Debug.LogWarning("GoldenHeroEvent.LoadVanillaHeroCardDef() - currentPlayer == null");
			return;
		}
		EntityDef heroEntityDef = player.GetHeroEntityDef();
		string heroCardId = CollectionManager.GetHeroCardId(heroEntityDef.GetClass(), CardHero.HeroType.HONORED);
		CardPortraitQuality quality = new CardPortraitQuality(3, TAG_PREMIUM.GOLDEN);
		DefLoader.Get().LoadCardDef(heroCardId, new DefLoader.LoadDefCallback<DefLoader.DisposableCardDef>(this.OnPremiumHeroCardDefLoaded), null, quality);
		if (heroEntityDef.GetCardSet() == TAG_CARD_SET.HERO_SKINS)
		{
			string heroCardId2 = CollectionManager.GetHeroCardId(heroEntityDef.GetClass(), CardHero.HeroType.VANILLA);
			quality = new CardPortraitQuality(3, TAG_PREMIUM.NORMAL);
			DefLoader.Get().LoadCardDef(heroCardId2, new DefLoader.LoadDefCallback<DefLoader.DisposableCardDef>(this.OnVanillaHeroCardDefLoaded), null, quality);
		}
	}

	// Token: 0x060025E4 RID: 9700 RVA: 0x000BE600 File Offset: 0x000BC800
	private void OnVanillaHeroCardDefLoaded(string cardId, DefLoader.DisposableCardDef def, object userData)
	{
		DefLoader.DisposableCardDef vanillaHeroCardDef = this.m_VanillaHeroCardDef;
		if (vanillaHeroCardDef != null)
		{
			vanillaHeroCardDef.Dispose();
		}
		this.m_VanillaHeroCardDef = def;
	}

	// Token: 0x060025E5 RID: 9701 RVA: 0x000BE61A File Offset: 0x000BC81A
	private void OnPremiumHeroCardDefLoaded(string cardId, DefLoader.DisposableCardDef def, object userData)
	{
		DefLoader.DisposableCardDef premiumHeroCardDef = this.m_PremiumHeroCardDef;
		if (premiumHeroCardDef != null)
		{
			premiumHeroCardDef.Dispose();
		}
		this.m_PremiumHeroCardDef = def;
	}

	// Token: 0x04001534 RID: 5428
	public PlayMakerFSM m_playmaker;

	// Token: 0x04001535 RID: 5429
	public Transform m_heroBone;

	// Token: 0x04001536 RID: 5430
	public GameObject m_burningHero;

	// Token: 0x04001537 RID: 5431
	private VictoryTwoScoop m_victoryTwoScoop;

	// Token: 0x04001538 RID: 5432
	private List<HeroRewardEvent.AnimationDoneListener> m_animationDoneListeners = new List<HeroRewardEvent.AnimationDoneListener>();

	// Token: 0x04001539 RID: 5433
	private DefLoader.DisposableCardDef m_VanillaHeroCardDef;

	// Token: 0x0400153A RID: 5434
	private DefLoader.DisposableCardDef m_PremiumHeroCardDef;

	// Token: 0x0400153B RID: 5435
	private global::Achievement m_RewardAchieve;

	// Token: 0x0400153C RID: 5436
	private QuestToast.DelOnCloseQuestToast m_ContinueCallback;

	// Token: 0x020015E4 RID: 5604
	// (Invoke) Token: 0x0600E20D RID: 57869
	public delegate void AnimationDoneListener();
}
