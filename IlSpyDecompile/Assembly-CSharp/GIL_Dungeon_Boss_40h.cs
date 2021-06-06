using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_40h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_PlayerAxe = new List<string> { "VO_GILA_BOSS_40h_Female_Treant_EventPlaysAxe_01.prefab:fb8468b00a5f0cc428dfcfce728fa042", "VO_GILA_BOSS_40h_Female_Treant_EventPlaysAxe_02.prefab:0a5c52faccaf8c5489a2da55a4e3590f" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_40h_Female_Treant_Intro_01.prefab:5d14b06a592ef5640ae7acbdb74c031a", "VO_GILA_BOSS_40h_Female_Treant_EmoteResponse_01.prefab:1252f452e179aea4c97325973e1c3dc4", "VO_GILA_BOSS_40h_Female_Treant_Death_01.prefab:14e1eb2eba09ae74eafa6c80011b74bb", "VO_GILA_BOSS_40h_Female_Treant_HeroPower_01.prefab:3a425a60e1d4a9c4dbc092e0a82bbf20", "VO_GILA_BOSS_40h_Female_Treant_HeroPower_02.prefab:ba115807ed6f853428c7742cdb023f39", "VO_GILA_BOSS_40h_Female_Treant_HeroPower_03.prefab:3c769fa8be22e194ca01135dd8169999", "VO_GILA_BOSS_40h_Female_Treant_HeroPower_04.prefab:0add75f1f2992354b89863a7b0672469", "VO_GILA_BOSS_40h_Female_Treant_EventPlaysWoodsmansAxe_01.prefab:f765de0ef498d5f48998db8dac4ff08a", "VO_GILA_BOSS_40h_Female_Treant_EventPlaysAxe_01.prefab:fb8468b00a5f0cc428dfcfce728fa042", "VO_GILA_BOSS_40h_Female_Treant_EventPlaysAxe_02.prefab:0a5c52faccaf8c5489a2da55a4e3590f" })
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
		return new List<string> { "VO_GILA_BOSS_40h_Female_Treant_HeroPower_01.prefab:3a425a60e1d4a9c4dbc092e0a82bbf20", "VO_GILA_BOSS_40h_Female_Treant_HeroPower_02.prefab:ba115807ed6f853428c7742cdb023f39", "VO_GILA_BOSS_40h_Female_Treant_HeroPower_03.prefab:3c769fa8be22e194ca01135dd8169999", "VO_GILA_BOSS_40h_Female_Treant_HeroPower_04.prefab:0add75f1f2992354b89863a7b0672469" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_40h_Female_Treant_Death_01.prefab:14e1eb2eba09ae74eafa6c80011b74bb";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_40h_Female_Treant_Intro_01.prefab:5d14b06a592ef5640ae7acbdb74c031a", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_40h_Female_Treant_EmoteResponse_01.prefab:1252f452e179aea4c97325973e1c3dc4", Notification.SpeechBubbleDirection.TopRight, actor));
		}
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
		case "GIL_653":
			yield return PlayEasterEggLine(actor, "VO_GILA_BOSS_40h_Female_Treant_EventPlaysWoodsmansAxe_01.prefab:f765de0ef498d5f48998db8dac4ff08a");
			break;
		case "CS2_106":
		case "EX1_247":
		case "EX1_411":
		case "FP1_021":
		case "ICC_236":
		case "CS2_112":
		case "LOOT_380":
		case "EX1_398":
		{
			string text = PopRandomLineWithChance(m_PlayerAxe);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
			break;
		}
		}
	}
}
