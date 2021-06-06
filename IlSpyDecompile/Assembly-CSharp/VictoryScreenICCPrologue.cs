using System.Collections;
using UnityEngine;

public class VictoryScreenICCPrologue : VictoryScreen
{
	public Animation m_BurnAwayAnimation;

	public AudioSource m_BurnAwayAudio;

	public Renderer m_LichPortraitRenderer;

	public string m_PortraitTextureName;

	private static readonly float TIRION_LINE_DELAY_SEC = 4.5f;

	private static readonly float LICH_BURN_ANIM_SPEED = 0.25f;

	protected override void Awake()
	{
		base.Awake();
		Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		m_LichPortraitRenderer.GetMaterial().SetTexture(m_PortraitTextureName, heroCard.GetPortraitTexture());
		VictoryTwoScoop victoryTwoScoop = m_twoScoop as VictoryTwoScoop;
		if (victoryTwoScoop != null)
		{
			victoryTwoScoop.SetOverrideHero(GameState.Get().GetFriendlySidePlayer().GetStartingHero()
				.GetEntityDef());
		}
		else
		{
			Log.Gameplay.PrintError("VictoryScreenICCPrologue.Awake() - m_twoScoop is not an instance of VictoryTwoScoop!");
		}
	}

	protected override void ShowStandardFlow()
	{
		ShowTwoScoop();
	}

	protected override void OnTwoScoopShown()
	{
		base.OnTwoScoopShown();
		StartCoroutine(PlayAnim());
	}

	private IEnumerator PlayAnim()
	{
		ICC_01_LICHKING missionEntity = GameState.Get().GetGameEntity() as ICC_01_LICHKING;
		if (missionEntity != null)
		{
			yield return new WaitForSeconds(TIRION_LINE_DELAY_SEC);
			while (NotificationManager.Get().IsQuotePlaying)
			{
				yield return null;
			}
			yield return StartCoroutine(missionEntity.PlayTirionVictoryScreenLine());
			if (m_BurnAwayAudio != null)
			{
				SoundManager.Get().Play(m_BurnAwayAudio);
			}
			m_BurnAwayAnimation["LichHeroBurnAway"].speed = LICH_BURN_ANIM_SPEED;
			m_BurnAwayAnimation.Play("LichHeroBurnAway");
			yield return StartCoroutine(missionEntity.PlayJainaVictoryScreenLine(m_twoScoop.m_heroActor));
		}
		else
		{
			Log.Gameplay.PrintError("VictoryScreenICCPrologue.PlayAnim(): GameEntity is not an instance of ICC_01_LICHKING!.");
		}
		m_hitbox.AddEventListener(UIEventType.RELEASE, base.ContinueButtonPress_PrevMode);
		if (!UniversalInputManager.UsePhoneUI)
		{
			m_continueText.gameObject.SetActive(value: true);
		}
	}
}
