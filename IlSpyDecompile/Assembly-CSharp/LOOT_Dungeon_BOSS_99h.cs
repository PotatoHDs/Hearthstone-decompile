using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOOT_Dungeon_BOSS_99h : LOOT_Dungeon
{
	private string m_RakanishuBigQuotePrefab = "Rakanishu_BigQuote.prefab:48df090654e138d4d8f8275655206a59";

	private HashSet<string> m_playedLines = new HashSet<string>();

	private int m_CandleAttackLineIndex;

	private List<string> m_HeroLines = new List<string> { "VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure1_01.prefab:7dc532104473e1a48b0480c9a4d859ec", "VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure2_01.prefab:f1852b13f7af8794880e91ab43e40a42", "VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure3_01.prefab:3e487bc1f6aeb754eaf10b8f130bf023", "VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure4_01.prefab:1238f261c2268ee4e9aeef376d87d36d", "VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure5_01.prefab:7d8c7ea05e7e9414c97b0574ccc61612" };

	private List<string> m_TogwaggleCandleAttackLines = new List<string> { "VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack1_01.prefab:0b541034826dc5043885c26aadeb5aa9", "VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack2_01.prefab:f0dd873ec050b254a971e2a3d39a57c1", "VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack3_01.prefab:8371c978ea042f8488c479252676315f", "VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack4_01.prefab:c04f2a01ff25578478ce1dab6acd9e75", "VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack5_01.prefab:7ccf3f74056e3274386c58f26c41a49a" };

	private List<int> m_indices = new List<int> { 0, 1, 2, 3, 4 };

	private List<string> m_RakanishuCandleAttackLines = new List<string> { "VO_Rakanishu_Male_Elemental_CastSpell3_01.prefab:81a88a21aca788e48bf1f323bce7741c", "VO_Rakanishu_Male_Elemental_CastSpell1_01.prefab:55acec97d5f9cf84cb4037a83f1e382f", "VO_Rakanishu_Male_Elemental_CastSpell4_01.prefab:1f3680a17baa7b444bb18d666f813dba", "VO_Rakanishu_Male_Elemental_CastSpell5_01.prefab:7d14cf1297567d3478762c7400b32823", "VO_Rakanishu_Male_Elemental_CastSpell2_01.prefab:b230354a77128944cba89d7f67e57f62" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_LOOTA_BOSS_12h_Male_Kobold_Intro_01.prefab:6d07bbeb5760a0b41a5033bf335a6295", "VO_LOOT_541_Male_Kobold_EmoteResponse_01.prefab:38b613ef138dcc241944d5153f9952ae", "VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure1_01.prefab:7dc532104473e1a48b0480c9a4d859ec", "VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure2_01.prefab:f1852b13f7af8794880e91ab43e40a42", "VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure3_01.prefab:3e487bc1f6aeb754eaf10b8f130bf023", "VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure4_01.prefab:1238f261c2268ee4e9aeef376d87d36d", "VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure5_01.prefab:7d8c7ea05e7e9414c97b0574ccc61612", "VO_LOOTA_BOSS_12h_Male_Kobold_Death_01.prefab:62bb730431732184f872d0450a9bc6e2", "VO_LOOTA_BOSS_12h_Male_Kobold_DefeatPlayer_01.prefab:51813afc620f6db4cafa3e8bc65aef20", "VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack1_01.prefab:0b541034826dc5043885c26aadeb5aa9",
			"VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack2_01.prefab:f0dd873ec050b254a971e2a3d39a57c1", "VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack3_01.prefab:8371c978ea042f8488c479252676315f", "VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack4_01.prefab:c04f2a01ff25578478ce1dab6acd9e75", "VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack5_01.prefab:7ccf3f74056e3274386c58f26c41a49a", "VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayCandle_01.prefab:4a9d8402c1a8fa749b223f2ecfccb6ec", "VO_LOOTA_BOSS_12h_Male_Kobold_EventFindCandle_01.prefab:02085733089e04f468b21bd668197916", "VO_LOOTA_BOSS_12h_Male_Kobold_EventCandleStolen_01.prefab:e13e3b43ea424f5489d8dcdc2de4a536", "VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayKoboldKing_01.prefab:82ee9011090b3d0478ec63e306d20526", "VO_Rakanishu_Male_Elemental_Intro_01.prefab:d37fc90b73f5aa94ca3e14012ed7ba88", "VO_Rakanishu_Male_Elemental_CastSpell1_01.prefab:55acec97d5f9cf84cb4037a83f1e382f",
			"VO_Rakanishu_Male_Elemental_CastSpell2_01.prefab:b230354a77128944cba89d7f67e57f62", "VO_Rakanishu_Male_Elemental_CastSpell3_01.prefab:81a88a21aca788e48bf1f323bce7741c", "VO_Rakanishu_Male_Elemental_CastSpell4_01.prefab:1f3680a17baa7b444bb18d666f813dba", "VO_Rakanishu_Male_Elemental_CastSpell5_01.prefab:7d14cf1297567d3478762c7400b32823", "VO_Rakanishu_Male_Elemental_EventRagnaros_02.prefab:218c7d55526aeca4e94f415d3caa94bd"
		})
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
		return new List<string>();
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_12h_Male_Kobold_Death_01.prefab:62bb730431732184f872d0450a9bc6e2";
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_12h_Male_Kobold_Intro_01.prefab:6d07bbeb5760a0b41a5033bf335a6295", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOT_541_Male_Kobold_EmoteResponse_01.prefab:38b613ef138dcc241944d5153f9952ae", Notification.SpeechBubbleDirection.TopRight, actor));
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
		if (m_playedLines.Contains(item))
		{
			yield break;
		}
		if (missionEvent == 1000)
		{
			yield return PlayLoyalSideKickBetrayal(missionEvent);
		}
		switch (missionEvent)
		{
		case 101:
		{
			int num = 50;
			int num2 = Random.Range(0, 100);
			if (m_HeroLines.Count != 0 && num >= num2)
			{
				string randomLine = m_HeroLines[Random.Range(0, m_HeroLines.Count)];
				yield return PlayLineOnlyOnce(enemyActor, randomLine);
				m_HeroLines.Remove(randomLine);
				yield return null;
			}
			break;
		}
		case 102:
			yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventFindCandle_01.prefab:02085733089e04f468b21bd668197916");
			break;
		case 103:
			yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventCandleStolen_01.prefab:e13e3b43ea424f5489d8dcdc2de4a536");
			break;
		case 104:
			GameState.Get().SetBusy(busy: true);
			if (m_TogwaggleCandleAttackLines.Count > 0)
			{
				int randomLineIndex = Random.Range(0, m_TogwaggleCandleAttackLines.Count);
				string line = m_TogwaggleCandleAttackLines[randomLineIndex];
				m_TogwaggleCandleAttackLines.RemoveAt(randomLineIndex);
				yield return PlayLineOnlyOnce(enemyActor, line);
				m_CandleAttackLineIndex = m_indices[randomLineIndex];
				m_indices.RemoveAt(randomLineIndex);
			}
			else
			{
				m_CandleAttackLineIndex = Random.Range(0, m_RakanishuCandleAttackLines.Count);
			}
			if (m_RakanishuCandleAttackLines[m_CandleAttackLineIndex] == "VO_Rakanishu_Male_Elemental_CastSpell4_01.prefab:1f3680a17baa7b444bb18d666f813dba")
			{
				m_CandleAttackLineIndex = Random.Range(0, m_RakanishuCandleAttackLines.Count);
			}
			yield return PlayBossLine(m_RakanishuBigQuotePrefab, m_RakanishuCandleAttackLines[m_CandleAttackLineIndex]);
			GameState.Get().SetBusy(busy: false);
			break;
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "LOOTA_843":
				yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayCandle_01.prefab:4a9d8402c1a8fa749b223f2ecfccb6ec");
				break;
			case "LOOT_541":
				yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayKoboldKing_01.prefab:82ee9011090b3d0478ec63e306d20526");
				break;
			case "EX1_298":
				yield return PlayLineOnlyOnce(m_RakanishuBigQuotePrefab, "VO_Rakanishu_Male_Elemental_EventRagnaros_02.prefab:218c7d55526aeca4e94f415d3caa94bd");
				break;
			}
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn == 2)
		{
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(m_RakanishuBigQuotePrefab, "VO_Rakanishu_Male_Elemental_Intro_01.prefab:d37fc90b73f5aa94ca3e14012ed7ba88");
			GameState.Get().SetBusy(busy: false);
		}
	}
}
