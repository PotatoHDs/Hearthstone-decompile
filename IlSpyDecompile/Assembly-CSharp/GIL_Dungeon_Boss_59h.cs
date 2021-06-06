using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_59h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_RandomHeroPowerKillLines = new List<string> { "VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_01.prefab:08179df12eacdaa4cb8791a604bc6b56", "VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_02.prefab:15775936c6acd9649897e744543f698e", "VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_03.prefab:1c2653cd155740844b4bfb694be5c925", "VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_04.prefab:6f1738ac4aaecb54687e68af9f998021", "VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_05.prefab:474a3f09f6bec9647aebdbceed42348d" };

	private List<string> m_RandomBossPlaysQuickShot = new List<string> { "VO_GILA_BOSS_59h_Male_Undead_EventQuickShot_01.prefab:b5070091171cc654eac17b25ecb473c6", "VO_GILA_BOSS_59h_Male_Undead_EventQuickShot_02.prefab:142c2555be96a4748ac4e6f89893f4d5" };

	private List<string> m_RandomBossPlaysDeadlyShot = new List<string> { "VO_GILA_BOSS_59h_Male_Undead_EventDeadlyShot_01.prefab:c3fb9ce9c84ef1649b7260ebc631464e", "VO_GILA_BOSS_59h_Male_Undead_EventDeadlyShot_02.prefab:1ab03dfd398e58a4eac5f176cc912a92" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_59h_Male_Undead_IntroCrowley_01.prefab:6c5d0cde28d5ee0488f66a0f53999059", "VO_GILA_BOSS_59h_Male_Undead_EmoteResponse_01.prefab:a7342a855d2c72749811c6d817d97280", "VO_GILA_BOSS_59h_Male_Undead_Death_02.prefab:a462977f734b9d8479f1c4bc7019e058", "VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_01.prefab:08179df12eacdaa4cb8791a604bc6b56", "VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_02.prefab:15775936c6acd9649897e744543f698e", "VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_03.prefab:1c2653cd155740844b4bfb694be5c925", "VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_04.prefab:6f1738ac4aaecb54687e68af9f998021", "VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_05.prefab:474a3f09f6bec9647aebdbceed42348d", "VO_GILA_BOSS_59h_Male_Undead_EventQuickShot_01.prefab:b5070091171cc654eac17b25ecb473c6", "VO_GILA_BOSS_59h_Male_Undead_EventQuickShot_02.prefab:142c2555be96a4748ac4e6f89893f4d5",
			"VO_GILA_BOSS_59h_Male_Undead_EventDeadlyShot_01.prefab:c3fb9ce9c84ef1649b7260ebc631464e", "VO_GILA_BOSS_59h_Male_Undead_EventDeadlyShot_02.prefab:1ab03dfd398e58a4eac5f176cc912a92", "VO_GILA_BOSS_59h_Male_Undead_EventTurn02_01.prefab:a1b990749b36db246ace5dabd2369928", "VO_GILA_600h_Male_Worgen_EVENT_NEMESIS_TURN02_01.prefab:081d59f0c0695c649b07f2ffaa2e02fc"
		})
		{
			PreloadSound(item);
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_GILFinalBoss);
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_59h_Male_Undead_IntroCrowley_01.prefab:6c5d0cde28d5ee0488f66a0f53999059", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_59h_Male_Undead_EmoteResponse_01.prefab:a7342a855d2c72749811c6d817d97280", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_59h_Male_Undead_Death_02.prefab:a462977f734b9d8479f1c4bc7019e058";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
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
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "BRM_013"))
		{
			if (cardId == "EX1_617")
			{
				string text = PopRandomLineWithChance(m_RandomBossPlaysDeadlyShot);
				if (text != null)
				{
					yield return PlayLineOnlyOnce(actor, text);
				}
			}
		}
		else
		{
			string text = PopRandomLineWithChance(m_RandomBossPlaysQuickShot);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (!m_playedLines.Contains(item) && missionEvent == 101)
		{
			string text = PopRandomLineWithChance(m_RandomHeroPowerKillLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		if (turn == 2)
		{
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_59h_Male_Undead_EventTurn02_01.prefab:a1b990749b36db246ace5dabd2369928");
			yield return PlayLineOnlyOnce(playerActor, "VO_GILA_600h_Male_Worgen_EVENT_NEMESIS_TURN02_01.prefab:081d59f0c0695c649b07f2ffaa2e02fc");
			GameState.Get().SetBusy(busy: false);
		}
	}
}
