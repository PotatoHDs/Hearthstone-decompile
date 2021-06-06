using System;
using System.Collections;

// Token: 0x020005C7 RID: 1479
public class TB02_CoOp : MissionEntity
{
	// Token: 0x0600515A RID: 20826 RVA: 0x001AC1AC File Offset: 0x001AA3AC
	private void SetUpBossCard()
	{
		if (this.m_bossCard == null)
		{
			int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
			Entity entity = GameState.Get().GetEntity(tag);
			if (entity != null)
			{
				this.m_bossCard = entity.GetCard();
			}
		}
	}

	// Token: 0x0600515B RID: 20827 RVA: 0x001AC1F3 File Offset: 0x001AA3F3
	public override void PreloadAssets()
	{
		base.PreloadSound("FX_MinionSummon_Cast.prefab:d0a0997a72042914f8779e138bb2755e");
		base.PreloadSound("CleanMechSmall_Trigger_Underlay.prefab:c943e7c65e3196d48a630fb118a2458b");
		base.PreloadSound("CleanMechLarge_Play_Underlay.prefab:ba8c1d07706f9284b8013f05e8d1f664");
		base.PreloadSound("CleanMechLarge_Death_Underlay.prefab:a7cad6027a0d13444a1ad0c49e6f7f23");
	}

	// Token: 0x0600515C RID: 20828 RVA: 0x001AC221 File Offset: 0x001AA421
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		this.SetUpBossCard();
		if (this.m_bossCard == null)
		{
			yield break;
		}
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("CleanMechSmall_Trigger_Underlay.prefab:c943e7c65e3196d48a630fb118a2458b", "VO_COOP02_00", Notification.SpeechBubbleDirection.TopRight, this.m_bossCard.GetActor(), 1f, true, false));
		}
		yield break;
	}

	// Token: 0x0600515D RID: 20829 RVA: 0x001AC237 File Offset: 0x001AA437
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.SetUpBossCard();
		if (this.m_bossCard == null)
		{
			yield break;
		}
		if (missionEvent != 5)
		{
			if (missionEvent == 6)
			{
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("CleanMechLarge_Death_Underlay.prefab:a7cad6027a0d13444a1ad0c49e6f7f23", "VO_COOP02_ABILITY_06", Notification.SpeechBubbleDirection.TopRight, this.m_bossCard.GetActor(), 1f, true, false));
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("CleanMechLarge_Play_Underlay.prefab:ba8c1d07706f9284b8013f05e8d1f664", "VO_COOP02_ABILITY_05", Notification.SpeechBubbleDirection.TopRight, this.m_bossCard.GetActor(), 1f, true, false));
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x0600515E RID: 20830 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x040048BD RID: 18621
	private Card m_bossCard;
}
