using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_22h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_22h_Male_Murloc_Intro_01.prefab:0d178c00638692b4093f9b7b0511d3b5", "VO_LOOTA_BOSS_22h_Male_Murloc_EmoteResponse_01.prefab:c9544ca612ac6144098259cac8d014bc", "VO_LOOTA_BOSS_22h_Male_Murloc_FishSmall_01.prefab:60c1d4374ab18bc419b47e6212c2d69e", "VO_LOOTA_BOSS_22h_Male_Murloc_FishMedium_01.prefab:e496cf9664f2b334b9a9511929144f56", "VO_LOOTA_BOSS_22h_Male_Murloc_FishLarge_02.prefab:dced70262b5d79f4095264bb1d74814c", "VO_LOOTA_BOSS_22h_Male_Murloc_Death_01.prefab:dab13998aedc64649afc23ada85aae06", "VO_LOOTA_BOSS_22h_Male_Murloc_DeathSpecial_01.prefab:4bc5951e1876f704693c475e0689419e", "VO_LOOTA_BOSS_22h_Male_Murloc_DefeatPlayer_01.prefab:7ceef4781a175854d921248187b25d63", "VO_LOOTA_BOSS_22h_Male_Murloc_EventSummonCrab_01.prefab:ed45607f96ac8014fbb5e1d42e94ce58", "VO_LOOTA_BOSS_22h_Male_Murloc_EventFishUpChest_01.prefab:4383c94c10459fb46b77dde1b4b0341a" })
		{
			PreloadSound(item);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_22h_Male_Murloc_Death_01.prefab:dab13998aedc64649afc23ada85aae06";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_22h_Male_Murloc_Intro_01.prefab:0d178c00638692b4093f9b7b0511d3b5", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_22h_Male_Murloc_EmoteResponse_01.prefab:c9544ca612ac6144098259cac8d014bc", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (!m_playedLines.Contains(item))
		{
			yield return PlayLoyalSideKickBetrayal(missionEvent);
			switch (missionEvent)
			{
			case 101:
				yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_22h_Male_Murloc_FishSmall_01.prefab:60c1d4374ab18bc419b47e6212c2d69e");
				yield return null;
				break;
			case 102:
				yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_22h_Male_Murloc_FishMedium_01.prefab:e496cf9664f2b334b9a9511929144f56");
				yield return null;
				break;
			case 103:
				yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_22h_Male_Murloc_FishLarge_02.prefab:dced70262b5d79f4095264bb1d74814c");
				yield return null;
				break;
			case 104:
				yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_22h_Male_Murloc_EventSummonCrab_01.prefab:ed45607f96ac8014fbb5e1d42e94ce58");
				yield return null;
				break;
			case 105:
				yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_22h_Male_Murloc_EventFishUpChest_01.prefab:4383c94c10459fb46b77dde1b4b0341a");
				yield return null;
				break;
			}
		}
	}
}
