using System.Collections;

public class TB02_CoOp : MissionEntity
{
	private Card m_bossCard;

	private void SetUpBossCard()
	{
		if (m_bossCard == null)
		{
			int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
			Entity entity = GameState.Get().GetEntity(tag);
			if (entity != null)
			{
				m_bossCard = entity.GetCard();
			}
		}
	}

	public override void PreloadAssets()
	{
		PreloadSound("FX_MinionSummon_Cast.prefab:d0a0997a72042914f8779e138bb2755e");
		PreloadSound("CleanMechSmall_Trigger_Underlay.prefab:c943e7c65e3196d48a630fb118a2458b");
		PreloadSound("CleanMechLarge_Play_Underlay.prefab:ba8c1d07706f9284b8013f05e8d1f664");
		PreloadSound("CleanMechLarge_Death_Underlay.prefab:a7cad6027a0d13444a1ad0c49e6f7f23");
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		SetUpBossCard();
		if (!(m_bossCard == null) && turn == 1)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechWithCustomGameString("CleanMechSmall_Trigger_Underlay.prefab:c943e7c65e3196d48a630fb118a2458b", "VO_COOP02_00", Notification.SpeechBubbleDirection.TopRight, m_bossCard.GetActor()));
		}
		yield break;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		SetUpBossCard();
		if (!(m_bossCard == null))
		{
			switch (missionEvent)
			{
			case 5:
				GameState.Get().SetBusy(busy: true);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechWithCustomGameString("CleanMechLarge_Play_Underlay.prefab:ba8c1d07706f9284b8013f05e8d1f664", "VO_COOP02_ABILITY_05", Notification.SpeechBubbleDirection.TopRight, m_bossCard.GetActor()));
				GameState.Get().SetBusy(busy: false);
				break;
			case 6:
				GameState.Get().SetBusy(busy: true);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechWithCustomGameString("CleanMechLarge_Death_Underlay.prefab:a7cad6027a0d13444a1ad0c49e6f7f23", "VO_COOP02_ABILITY_06", Notification.SpeechBubbleDirection.TopRight, m_bossCard.GetActor()));
				GameState.Get().SetBusy(busy: false);
				break;
			}
		}
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}
}
