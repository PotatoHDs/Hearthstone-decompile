using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002D3 RID: 723
public class VictoryScreenFinleyBubbleHearth : VictoryScreen
{
	// Token: 0x0600260D RID: 9741 RVA: 0x000BF298 File Offset: 0x000BD498
	protected override void Awake()
	{
		base.Awake();
		Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		this.m_LichPortraitRenderer.GetMaterial().SetTexture(this.m_PortraitTextureName, heroCard.GetPortraitTexture());
		VictoryTwoScoop victoryTwoScoop = this.m_twoScoop as VictoryTwoScoop;
		if (victoryTwoScoop != null)
		{
			victoryTwoScoop.SetOverrideHero(GameState.Get().GetFriendlySidePlayer().GetStartingHero().GetEntityDef());
			return;
		}
		Log.Gameplay.PrintError("VictoryScreenICCPrologue.Awake() - m_twoScoop is not an instance of VictoryTwoScoop!", Array.Empty<object>());
	}

	// Token: 0x0600260E RID: 9742 RVA: 0x000BF31B File Offset: 0x000BD51B
	protected override void ShowStandardFlow()
	{
		base.ShowTwoScoop();
	}

	// Token: 0x0600260F RID: 9743 RVA: 0x000BF323 File Offset: 0x000BD523
	protected override void OnTwoScoopShown()
	{
		base.OnTwoScoopShown();
		base.StartCoroutine(this.PlayAnim());
	}

	// Token: 0x06002610 RID: 9744 RVA: 0x000BF338 File Offset: 0x000BD538
	private IEnumerator PlayAnim()
	{
		ICC_01_LICHKING missionEntity = GameState.Get().GetGameEntity() as ICC_01_LICHKING;
		if (missionEntity != null)
		{
			yield return new WaitForSeconds(VictoryScreenFinleyBubbleHearth.FINLEY_LINE_DELAY_SEC);
			while (NotificationManager.Get().IsQuotePlaying)
			{
				yield return null;
			}
			yield return base.StartCoroutine(missionEntity.PlayTirionVictoryScreenLine());
			if (this.m_BurnAwayAudio != null)
			{
				SoundManager.Get().Play(this.m_BurnAwayAudio, null, null, null);
			}
			this.m_BurnAwayAnimation["LichHeroBurnAway"].speed = VictoryScreenFinleyBubbleHearth.LICH_BURN_ANIM_SPEED;
			this.m_BurnAwayAnimation.Play("LichHeroBurnAway");
			yield return base.StartCoroutine(missionEntity.PlayJainaVictoryScreenLine(this.m_twoScoop.m_heroActor));
		}
		else
		{
			Log.Gameplay.PrintError("VictoryScreenICCPrologue.PlayAnim(): GameEntity is not an instance of ICC_01_LICHKING!.", Array.Empty<object>());
		}
		this.m_hitbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(base.ContinueButtonPress_PrevMode));
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_continueText.gameObject.SetActive(true);
		}
		yield break;
	}

	// Token: 0x04001557 RID: 5463
	public Animation m_BurnAwayAnimation;

	// Token: 0x04001558 RID: 5464
	public AudioSource m_BurnAwayAudio;

	// Token: 0x04001559 RID: 5465
	public Renderer m_LichPortraitRenderer;

	// Token: 0x0400155A RID: 5466
	public string m_PortraitTextureName;

	// Token: 0x0400155B RID: 5467
	private static readonly float FINLEY_LINE_DELAY_SEC = 4.5f;

	// Token: 0x0400155C RID: 5468
	private static readonly float LICH_BURN_ANIM_SPEED = 0.25f;
}
