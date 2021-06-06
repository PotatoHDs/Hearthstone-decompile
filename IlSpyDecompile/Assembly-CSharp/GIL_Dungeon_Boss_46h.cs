using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_46h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_RandomLines = new List<string> { "VO_GILA_BOSS_46h_Male_Worgen_EventPlaysWorgen_01.prefab:7e95c21db5f63094bb9078b4c11d3a32", "VO_GILA_BOSS_46h_Male_Worgen_EventPlaysWorgen_03.prefab:d6abf1d6660959147af580da61058831", "VO_GILA_BOSS_46h_Male_Worgen_EventPlaysWorgen_06.prefab:bb4338cac09ab744a8c9ce0d3822dd5a" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_46h_Male_Worgen_Intro_01.prefab:793a3ad2cd2405741ad472479cf70932", "VO_GILA_BOSS_46h_Male_Worgen_EmoteResponse_01.prefab:b86300a09c3a5ab43bb21a558a2aa331", "VO_GILA_BOSS_46h_Male_Worgen_Death_01.prefab:f43afb982dcd460418125e4c4c91ef38", "VO_GILA_BOSS_46h_Male_Worgen_EventPlaysWorgen_01.prefab:7e95c21db5f63094bb9078b4c11d3a32", "VO_GILA_BOSS_46h_Male_Worgen_EventPlaysWorgen_03.prefab:d6abf1d6660959147af580da61058831", "VO_GILA_BOSS_46h_Male_Worgen_EventPlaysWorgen_06.prefab:bb4338cac09ab744a8c9ce0d3822dd5a", "VO_GILA_BOSS_46h_Male_Worgen_EventPlayTrapper_02.prefab:0f49f185267a7144689d07bf20405e02", "VO_GILA_BOSS_46h_Male_Worgen_EventPlayBloodMoon_01.prefab:95888d40608ed4344a19e1f276275a59" })
		{
			PreloadSound(item);
		}
	}

	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_46h_Male_Worgen_Intro_01.prefab:793a3ad2cd2405741ad472479cf70932", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_46h_Male_Worgen_EmoteResponse_01.prefab:b86300a09c3a5ab43bb21a558a2aa331", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_46h_Male_Worgen_Death_01.prefab:f43afb982dcd460418125e4c4c91ef38";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (cardId)
		{
		case "GILA_412":
			yield return PlayBossLine(actor, "VO_GILA_BOSS_46h_Male_Worgen_EventPlayBloodMoon_01.prefab:95888d40608ed4344a19e1f276275a59");
			break;
		case "GILA_851a":
			yield return PlayBossLine(actor, "VO_GILA_BOSS_46h_Male_Worgen_EventPlayTrapper_02.prefab:0f49f185267a7144689d07bf20405e02");
			break;
		case "KAR_005":
		case "KAR_005a":
		case "DS1_070":
		case "OG_295":
		case "ICC_031":
		case "EX1_412":
		case "OG_247":
		case "CFM_665":
		case "EX1_010":
		case "OG_292":
		case "GIL_113":
		case "GIL_580":
		case "GIL_547":
		case "GIL_201t":
		case "GIL_534":
		case "GIL_509":
		case "GIL_118":
		case "GIL_683t":
		case "GIL_202t":
		case "GIL_648":
		case "GIL_682":
		case "GIL_692":
		{
			string text = PopRandomLineWithChance(m_RandomLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
			break;
		}
		}
	}
}
