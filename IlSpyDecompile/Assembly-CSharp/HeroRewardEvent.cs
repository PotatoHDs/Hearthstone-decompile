using System.Collections.Generic;
using Assets;
using UnityEngine;

public class HeroRewardEvent : MonoBehaviour
{
	public delegate void AnimationDoneListener();

	public PlayMakerFSM m_playmaker;

	public Transform m_heroBone;

	public GameObject m_burningHero;

	private VictoryTwoScoop m_victoryTwoScoop;

	private List<AnimationDoneListener> m_animationDoneListeners = new List<AnimationDoneListener>();

	private DefLoader.DisposableCardDef m_VanillaHeroCardDef;

	private DefLoader.DisposableCardDef m_PremiumHeroCardDef;

	private Achievement m_RewardAchieve;

	private QuestToast.DelOnCloseQuestToast m_ContinueCallback;

	private void Awake()
	{
		LoadHeroCardDefs();
	}

	private void OnDestroy()
	{
		m_VanillaHeroCardDef?.Dispose();
		m_PremiumHeroCardDef?.Dispose();
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
		m_playmaker.SendEvent("Action");
		m_victoryTwoScoop.HideXpBar();
		m_victoryTwoScoop.m_bannerLabel.Text = "";
	}

	public void Hide()
	{
		m_playmaker.SendEvent("Done");
		SoundManager.Get().LoadAndPlay("rank_window_shrink.prefab:9c6393a1d207a07439c22f31ef405a7c");
	}

	public void SetHeroBurnAwayTexture(Texture heroTexture)
	{
		m_burningHero.GetComponent<Renderer>().GetMaterial().mainTexture = heroTexture;
	}

	public void HideTwoScoop()
	{
		m_victoryTwoScoop.Hide();
	}

	public void HideHeroActor()
	{
		m_victoryTwoScoop.m_heroActor.Hide();
	}

	public void SetVictoryTwoScoop(VictoryTwoScoop twoScoop)
	{
		m_victoryTwoScoop = twoScoop;
	}

	public void SetRewardAchieve(Achievement achieve, QuestToast.DelOnCloseQuestToast continueCallback)
	{
		m_RewardAchieve = achieve;
		m_ContinueCallback = continueCallback;
	}

	public void SwapHeroToVanilla()
	{
		if (m_VanillaHeroCardDef != null)
		{
			m_victoryTwoScoop.m_heroActor.SetCardDef(m_VanillaHeroCardDef);
			m_victoryTwoScoop.m_heroActor.UpdateAllComponents();
		}
	}

	public void SwapMaterialToPremium()
	{
		m_victoryTwoScoop.m_heroActor.SetPremium(TAG_PREMIUM.GOLDEN);
		m_victoryTwoScoop.m_heroActor.UpdateAllComponents();
	}

	public void SwapHeroToPremium()
	{
		m_victoryTwoScoop.m_heroActor.SetCardDef(m_PremiumHeroCardDef);
		m_victoryTwoScoop.m_heroActor.UpdateAllComponents();
	}

	public void ShowHeroRewardBanner()
	{
		if (m_RewardAchieve != null)
		{
			QuestToast.ShowQuestToast(UserAttentionBlocker.NONE, m_ContinueCallback, updateCacheValues: false, m_RewardAchieve);
		}
	}

	public void AnimationDone()
	{
		FireAnimationDoneEvent();
	}

	private void FireAnimationDoneEvent()
	{
		AnimationDoneListener[] array = m_animationDoneListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	public void RegisterAnimationDoneListener(AnimationDoneListener listener)
	{
		if (!m_animationDoneListeners.Contains(listener))
		{
			m_animationDoneListeners.Add(listener);
		}
	}

	public void RemoveAnimationDoneListener(AnimationDoneListener listener)
	{
		m_animationDoneListeners.Remove(listener);
	}

	private void LoadHeroCardDefs()
	{
		Player player = null;
		foreach (Player value in GameState.Get().GetPlayerMap().Values)
		{
			if (value.GetSide() == Player.Side.FRIENDLY)
			{
				player = value;
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
		DefLoader.Get().LoadCardDef(heroCardId, OnPremiumHeroCardDefLoaded, null, quality);
		if (heroEntityDef.GetCardSet() == TAG_CARD_SET.HERO_SKINS)
		{
			string heroCardId2 = CollectionManager.GetHeroCardId(heroEntityDef.GetClass(), CardHero.HeroType.VANILLA);
			quality = new CardPortraitQuality(3, TAG_PREMIUM.NORMAL);
			DefLoader.Get().LoadCardDef(heroCardId2, OnVanillaHeroCardDefLoaded, null, quality);
		}
	}

	private void OnVanillaHeroCardDefLoaded(string cardId, DefLoader.DisposableCardDef def, object userData)
	{
		m_VanillaHeroCardDef?.Dispose();
		m_VanillaHeroCardDef = def;
	}

	private void OnPremiumHeroCardDefLoaded(string cardId, DefLoader.DisposableCardDef def, object userData)
	{
		m_PremiumHeroCardDef?.Dispose();
		m_PremiumHeroCardDef = def;
	}
}
