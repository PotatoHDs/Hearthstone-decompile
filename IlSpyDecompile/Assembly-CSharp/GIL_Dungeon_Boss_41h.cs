using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_41h : GIL_Dungeon
{
	private List<string> m_HackLines = new List<string> { "VO_GILA_BOSS_41h_Male_NightElf_EventHack_01.prefab:2345227d6fc812f4eb951c5fdef805cf", "VO_GILA_BOSS_41h_Male_NightElf_EventHack_02.prefab:60157a9729d33b445956b74435784dae", "VO_GILA_BOSS_41h_Male_NightElf_EventHack_03.prefab:3ab3c3a3da9f0bf4fac82f53c868851b" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_41h_Male_NightElf_Intro_01.prefab:2ee1227d69533f24197e3d948f21f858", "VO_GILA_BOSS_41h_Male_NightElf_EmoteResponse_02.prefab:b6265a8078217254181079a5796fcc1f", "VO_GILA_BOSS_41h_Male_NightElf_Death_01.prefab:4a86b581b0c06f54f8566bd5def843a8", "VO_GILA_BOSS_41h_Male_NightElf_DefeatPlayer_01.prefab:d3365287238e41943b4c19866d607360", "VO_GILA_BOSS_41h_Male_NightElf_HeroPower_01.prefab:ccd2d7a8e82b40847a0823442c3321c9", "VO_GILA_BOSS_41h_Male_NightElf_HeroPower_02.prefab:014065c5aef92e24bbc3be7ffeb691c4", "VO_GILA_BOSS_41h_Male_NightElf_HeroPower_03.prefab:f629177909f9356428aad20b0156172b", "VO_GILA_BOSS_41h_Male_NightElf_HeroPower_04.prefab:16828aa7f71bed1479c4e0532c4eb1ba", "VO_GILA_BOSS_41h_Male_NightElf_EventHack_01.prefab:2345227d6fc812f4eb951c5fdef805cf", "VO_GILA_BOSS_41h_Male_NightElf_EventHack_02.prefab:60157a9729d33b445956b74435784dae",
			"VO_GILA_BOSS_41h_Male_NightElf_EventHack_03.prefab:3ab3c3a3da9f0bf4fac82f53c868851b"
		})
		{
			PreloadSound(item);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (cardId == "GILA_BOSS_41t")
		{
			string text = PopRandomLineWithChance(m_HackLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_GILA_BOSS_41h_Male_NightElf_HeroPower_01.prefab:ccd2d7a8e82b40847a0823442c3321c9", "VO_GILA_BOSS_41h_Male_NightElf_HeroPower_02.prefab:014065c5aef92e24bbc3be7ffeb691c4", "VO_GILA_BOSS_41h_Male_NightElf_HeroPower_03.prefab:f629177909f9356428aad20b0156172b", "VO_GILA_BOSS_41h_Male_NightElf_HeroPower_04.prefab:16828aa7f71bed1479c4e0532c4eb1ba" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_41h_Male_NightElf_Death_01.prefab:4a86b581b0c06f54f8566bd5def843a8";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_41h_Male_NightElf_Intro_01.prefab:2ee1227d69533f24197e3d948f21f858", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_41h_Male_NightElf_EmoteResponse_02.prefab:b6265a8078217254181079a5796fcc1f", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}
}
