using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_34h : LOOT_Dungeon
{
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_34h_Male_Demon_Intro_01.prefab:f920329aab2358b46acc4a4078ac1d47", "VO_LOOTA_BOSS_34h_Male_Demon_EmoteResponse_01.prefab:f6ab96c6db6844141868af6c3719748c", "VO_LOOTA_BOSS_34h_Male_Demon_HeroPower1_01.prefab:4050260455fd49949a326d2fd7a8de4e", "VO_LOOTA_BOSS_34h_Male_Demon_HeroPower2_01.prefab:2465f3f831f76ef4bacb23d22f75cfc3", "VO_LOOTA_BOSS_34h_Male_Demon_HeroPower3_01.prefab:3a4ee40cba1dc9b45b3e8def5bc46a6a", "VO_LOOTA_BOSS_34h_Male_Demon_HeroPower4_01.prefab:4cfa80ab6d8338e43a13d63b03d2e81d", "VO_LOOTA_BOSS_34h_Male_Demon_HeroPower5_01.prefab:94d02c154a8ee724b965c48dd11e29df", "VO_LOOTA_BOSS_34h_Male_Demon_Death_01.prefab:61c182fcaec41bb4e8f14fcc0f55e4f4", "VO_LOOTA_BOSS_34h_Male_Demon_DefeatPlayer_01.prefab:1640e62dc70ba9a4188e22d0bc32a2fa" })
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
		return new List<string> { "VO_LOOTA_BOSS_34h_Male_Demon_HeroPower1_01.prefab:4050260455fd49949a326d2fd7a8de4e", "VO_LOOTA_BOSS_34h_Male_Demon_HeroPower2_01.prefab:2465f3f831f76ef4bacb23d22f75cfc3", "VO_LOOTA_BOSS_34h_Male_Demon_HeroPower3_01.prefab:3a4ee40cba1dc9b45b3e8def5bc46a6a", "VO_LOOTA_BOSS_34h_Male_Demon_HeroPower4_01.prefab:4cfa80ab6d8338e43a13d63b03d2e81d", "VO_LOOTA_BOSS_34h_Male_Demon_HeroPower5_01.prefab:94d02c154a8ee724b965c48dd11e29df" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_34h_Male_Demon_Death_01.prefab:61c182fcaec41bb4e8f14fcc0f55e4f4";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_34h_Male_Demon_Intro_01.prefab:f920329aab2358b46acc4a4078ac1d47", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_34h_Male_Demon_EmoteResponse_01.prefab:f6ab96c6db6844141868af6c3719748c", Notification.SpeechBubbleDirection.TopRight, actor));
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
