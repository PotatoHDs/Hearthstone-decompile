using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public class VictoryScreenICCPrologue : VictoryScreen
{
	// Token: 0x0600261E RID: 9758 RVA: 0x000BF7AC File Offset: 0x000BD9AC
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

	// Token: 0x0600261F RID: 9759 RVA: 0x000BF31B File Offset: 0x000BD51B
	protected override void ShowStandardFlow()
	{
		base.ShowTwoScoop();
	}

	// Token: 0x06002620 RID: 9760 RVA: 0x000BF82F File Offset: 0x000BDA2F
	protected override void OnTwoScoopShown()
	{
		base.OnTwoScoopShown();
		base.StartCoroutine(this.PlayAnim());
	}

	// Token: 0x06002621 RID: 9761 RVA: 0x000BF844 File Offset: 0x000BDA44
	private IEnumerator PlayAnim()
	{
		ICC_01_LICHKING missionEntity = GameState.Get().GetGameEntity() as ICC_01_LICHKING;
		if (missionEntity != null)
		{
			yield return new WaitForSeconds(VictoryScreenICCPrologue.TIRION_LINE_DELAY_SEC);
			while (NotificationManager.Get().IsQuotePlaying)
			{
				yield return null;
			}
			yield return base.StartCoroutine(missionEntity.PlayTirionVictoryScreenLine());
			if (this.m_BurnAwayAudio != null)
			{
				SoundManager.Get().Play(this.m_BurnAwayAudio, null, null, null);
			}
			this.m_BurnAwayAnimation["LichHeroBurnAway"].speed = VictoryScreenICCPrologue.LICH_BURN_ANIM_SPEED;
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

	// Token: 0x0400157A RID: 5498
	public Animation m_BurnAwayAnimation;

	// Token: 0x0400157B RID: 5499
	public AudioSource m_BurnAwayAudio;

	// Token: 0x0400157C RID: 5500
	public Renderer m_LichPortraitRenderer;

	// Token: 0x0400157D RID: 5501
	public string m_PortraitTextureName;

	// Token: 0x0400157E RID: 5502
	private static readonly float TIRION_LINE_DELAY_SEC = 4.5f;

	// Token: 0x0400157F RID: 5503
	private static readonly float LICH_BURN_ANIM_SPEED = 0.25f;
}
