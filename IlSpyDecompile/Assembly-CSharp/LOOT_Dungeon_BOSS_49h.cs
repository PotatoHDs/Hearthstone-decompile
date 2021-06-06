using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_49h : LOOT_Dungeon
{
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_49h_Male_Elemental_Intro_01.prefab:ac8ed0481b700e54ea96b415b7254a29", "VO_LOOTA_BOSS_49h_Male_Elemental_EmoteResponse_01.prefab:fea117c2a4e020b4d8db820b9a991c51", "VO_LOOTA_BOSS_49h_Male_Elemental_HeroPower1_01.prefab:cf576d8d32edf2a4899d11c634d41c01", "VO_LOOTA_BOSS_49h_Male_Elemental_HeroPower2_01.prefab:d1dd68ead56129547a000d910a498cac", "VO_LOOTA_BOSS_49h_Male_Elemental_HeroPower3_01.prefab:ffda1be6d974ba747aa87df0dfc83174", "VO_LOOTA_BOSS_49h_Male_Elemental_Death_01.prefab:4acf15c5d3b4a8347bec1d0959a526bf", "VO_LOOTA_BOSS_49h_Male_Elemental_DefeatPlayer_01.prefab:8e0e0ccbc81c5e740b6463e6edf7c364" })
		{
			PreloadSound(item);
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOOTFinalBoss);
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_LOOTA_BOSS_49h_Male_Elemental_HeroPower1_01.prefab:cf576d8d32edf2a4899d11c634d41c01", "VO_LOOTA_BOSS_49h_Male_Elemental_HeroPower2_01.prefab:d1dd68ead56129547a000d910a498cac", "VO_LOOTA_BOSS_49h_Male_Elemental_HeroPower3_01.prefab:ffda1be6d974ba747aa87df0dfc83174" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_49h_Male_Elemental_Death_01.prefab:4acf15c5d3b4a8347bec1d0959a526bf";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_49h_Male_Elemental_Intro_01.prefab:ac8ed0481b700e54ea96b415b7254a29", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_49h_Male_Elemental_EmoteResponse_01.prefab:fea117c2a4e020b4d8db820b9a991c51", Notification.SpeechBubbleDirection.TopRight, actor));
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
