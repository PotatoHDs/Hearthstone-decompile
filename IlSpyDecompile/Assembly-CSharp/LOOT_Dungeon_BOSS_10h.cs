using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_10h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_10h_Male_Human_Intro_01.prefab:dbf217aa7867af24c97df2ec233d16c6", "VO_LOOTA_BOSS_10h_Male_Human_EmoteResponse_01.prefab:7e27eb6c0a6af0f47ab431f370fe18b1", "VO_LOOTA_BOSS_10h_Male_Human_HeroPower1_01.prefab:a516a5848203a4046bef0eae50bf486f", "VO_LOOTA_BOSS_10h_Male_Human_HeroPower2_01.prefab:33e6717114ad72f448ba755b7a7590e8", "VO_LOOTA_BOSS_10h_Male_Human_HeroPower3_01.prefab:85ffc97097009c742bd2c6216bbd7c13", "VO_LOOTA_BOSS_10h_Male_Human_HeroPower4_01.prefab:84c0fded35d23bc4cbc47fc739d21ed9", "VO_LOOTA_BOSS_10h_Male_Human_HeroPower5_01.prefab:7c3d72d131e405d4c80d022b70aefebf", "VO_LOOTA_BOSS_10h_Male_Human_Death_01.prefab:a110948e22ecf2d42a4b45013614ed84", "VO_LOOTA_BOSS_10h_Male_Human_DefeatPlayer_02.prefab:642987c181c1b3f46845b5fa7ef62927", "VO_LOOTA_BOSS_10h_Male_Human_EventOverdraw_01.prefab:61b6b60a935250f47829ebdcb43c6cfd" })
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
		return new List<string> { "VO_LOOTA_BOSS_10h_Male_Human_HeroPower1_01.prefab:a516a5848203a4046bef0eae50bf486f", "VO_LOOTA_BOSS_10h_Male_Human_HeroPower2_01.prefab:33e6717114ad72f448ba755b7a7590e8", "VO_LOOTA_BOSS_10h_Male_Human_HeroPower3_01.prefab:85ffc97097009c742bd2c6216bbd7c13", "VO_LOOTA_BOSS_10h_Male_Human_HeroPower4_01.prefab:84c0fded35d23bc4cbc47fc739d21ed9", "VO_LOOTA_BOSS_10h_Male_Human_HeroPower5_01.prefab:7c3d72d131e405d4c80d022b70aefebf" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_10h_Male_Human_Death_01.prefab:a110948e22ecf2d42a4b45013614ed84";
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_10h_Male_Human_Intro_01.prefab:dbf217aa7867af24c97df2ec233d16c6", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_10h_Male_Human_EmoteResponse_01.prefab:7e27eb6c0a6af0f47ab431f370fe18b1", Notification.SpeechBubbleDirection.TopRight, actor));
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
			if (missionEvent == 101)
			{
				yield return PlayEasterEggLine(enemyActor, "VO_LOOTA_BOSS_10h_Male_Human_EventOverdraw_01.prefab:61b6b60a935250f47829ebdcb43c6cfd");
			}
		}
	}
}
