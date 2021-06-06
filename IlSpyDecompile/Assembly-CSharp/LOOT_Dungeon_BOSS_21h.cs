using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_21h : LOOT_Dungeon
{
	private static readonly AssetReference LOOTA_BOSS_21h_GnoshTheGreatWorm_Death = new AssetReference("LOOTA_BOSS_21h_GnoshTheGreatWorm_Death.prefab:96326178d2fdd5c42bc4f6f05961c93b");

	private static readonly AssetReference LOOTA_BOSS_21h_GnoshTheGreatWorm_Intro = new AssetReference("LOOTA_BOSS_21h_GnoshTheGreatWorm_Intro.prefab:e398752e1baa4c74c9a7782056589c86");

	private static readonly AssetReference LOOTA_BOSS_21h_GnoshTheGreatWorm_EmoteResponse = new AssetReference("LOOTA_BOSS_21h_GnoshTheGreatWorm_EmoteResponse.prefab:fba69559e1920b4418245a9a1ef99130");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { LOOTA_BOSS_21h_GnoshTheGreatWorm_Death, LOOTA_BOSS_21h_GnoshTheGreatWorm_Intro, LOOTA_BOSS_21h_GnoshTheGreatWorm_EmoteResponse })
		{
			PreloadSound(item);
		}
	}

	protected override string GetBossDeathLine()
	{
		return LOOTA_BOSS_21h_GnoshTheGreatWorm_Death;
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(LOOTA_BOSS_21h_GnoshTheGreatWorm_Intro, Notification.SpeechBubbleDirection.None, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(LOOTA_BOSS_21h_GnoshTheGreatWorm_EmoteResponse, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		yield return PlayLoyalSideKickBetrayal(missionEvent);
	}
}
