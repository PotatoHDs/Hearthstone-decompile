using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_47h : LOOT_Dungeon
{
	private static readonly AssetReference LOOTA_BOSS_47h_Lava_FilledRoom_Death = new AssetReference("LOOTA_BOSS_47h_Lava_FilledRoom_Death.prefab:2753d2ebd9bd40b458a33c552832df00");

	private static readonly AssetReference LOOTA_BOSS_47h_Lava_FilledRoom_Emote = new AssetReference("LOOTA_BOSS_47h_Lava_FilledRoom_Emote.prefab:ceb20cb5ceec4a549886c63442ac8b93");

	private static readonly AssetReference LOOTA_BOSS_47h_Lava_FilledRoom_Intro = new AssetReference("LOOTA_BOSS_47h_Lava_FilledRoom_Intro.prefab:79a4d695d025efc4a82d056a338932e5");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { LOOTA_BOSS_47h_Lava_FilledRoom_Death, LOOTA_BOSS_47h_Lava_FilledRoom_Emote, LOOTA_BOSS_47h_Lava_FilledRoom_Intro })
		{
			PreloadSound(item);
		}
	}

	protected override string GetBossDeathLine()
	{
		return LOOTA_BOSS_47h_Lava_FilledRoom_Death;
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(LOOTA_BOSS_47h_Lava_FilledRoom_Intro, Notification.SpeechBubbleDirection.None, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(LOOTA_BOSS_47h_Lava_FilledRoom_Emote, Notification.SpeechBubbleDirection.TopRight, actor));
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
