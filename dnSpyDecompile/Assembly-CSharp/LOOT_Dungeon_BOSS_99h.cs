using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003DA RID: 986
public class LOOT_Dungeon_BOSS_99h : LOOT_Dungeon
{
	// Token: 0x06003755 RID: 14165 RVA: 0x00117E3C File Offset: 0x0011603C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_12h_Male_Kobold_Intro_01.prefab:6d07bbeb5760a0b41a5033bf335a6295",
			"VO_LOOT_541_Male_Kobold_EmoteResponse_01.prefab:38b613ef138dcc241944d5153f9952ae",
			"VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure1_01.prefab:7dc532104473e1a48b0480c9a4d859ec",
			"VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure2_01.prefab:f1852b13f7af8794880e91ab43e40a42",
			"VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure3_01.prefab:3e487bc1f6aeb754eaf10b8f130bf023",
			"VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure4_01.prefab:1238f261c2268ee4e9aeef376d87d36d",
			"VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure5_01.prefab:7d8c7ea05e7e9414c97b0574ccc61612",
			"VO_LOOTA_BOSS_12h_Male_Kobold_Death_01.prefab:62bb730431732184f872d0450a9bc6e2",
			"VO_LOOTA_BOSS_12h_Male_Kobold_DefeatPlayer_01.prefab:51813afc620f6db4cafa3e8bc65aef20",
			"VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack1_01.prefab:0b541034826dc5043885c26aadeb5aa9",
			"VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack2_01.prefab:f0dd873ec050b254a971e2a3d39a57c1",
			"VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack3_01.prefab:8371c978ea042f8488c479252676315f",
			"VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack4_01.prefab:c04f2a01ff25578478ce1dab6acd9e75",
			"VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack5_01.prefab:7ccf3f74056e3274386c58f26c41a49a",
			"VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayCandle_01.prefab:4a9d8402c1a8fa749b223f2ecfccb6ec",
			"VO_LOOTA_BOSS_12h_Male_Kobold_EventFindCandle_01.prefab:02085733089e04f468b21bd668197916",
			"VO_LOOTA_BOSS_12h_Male_Kobold_EventCandleStolen_01.prefab:e13e3b43ea424f5489d8dcdc2de4a536",
			"VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayKoboldKing_01.prefab:82ee9011090b3d0478ec63e306d20526",
			"VO_Rakanishu_Male_Elemental_Intro_01.prefab:d37fc90b73f5aa94ca3e14012ed7ba88",
			"VO_Rakanishu_Male_Elemental_CastSpell1_01.prefab:55acec97d5f9cf84cb4037a83f1e382f",
			"VO_Rakanishu_Male_Elemental_CastSpell2_01.prefab:b230354a77128944cba89d7f67e57f62",
			"VO_Rakanishu_Male_Elemental_CastSpell3_01.prefab:81a88a21aca788e48bf1f323bce7741c",
			"VO_Rakanishu_Male_Elemental_CastSpell4_01.prefab:1f3680a17baa7b444bb18d666f813dba",
			"VO_Rakanishu_Male_Elemental_CastSpell5_01.prefab:7d14cf1297567d3478762c7400b32823",
			"VO_Rakanishu_Male_Elemental_EventRagnaros_02.prefab:218c7d55526aeca4e94f415d3caa94bd"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003756 RID: 14166 RVA: 0x001150F0 File Offset: 0x001132F0
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOOTFinalBoss);
	}

	// Token: 0x06003757 RID: 14167 RVA: 0x00117FA8 File Offset: 0x001161A8
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003758 RID: 14168 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003759 RID: 14169 RVA: 0x00117FBE File Offset: 0x001161BE
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_12h_Male_Kobold_Death_01.prefab:62bb730431732184f872d0450a9bc6e2";
	}

	// Token: 0x0600375A RID: 14170 RVA: 0x00117FC8 File Offset: 0x001161C8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_12h_Male_Kobold_Intro_01.prefab:6d07bbeb5760a0b41a5033bf335a6295", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOT_541_Male_Kobold_EmoteResponse_01.prefab:38b613ef138dcc241944d5153f9952ae", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600375B RID: 14171 RVA: 0x0011804F File Offset: 0x0011624F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		if (missionEvent == 1000)
		{
			yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		}
		switch (missionEvent)
		{
		case 101:
		{
			int num = 50;
			int num2 = UnityEngine.Random.Range(0, 100);
			if (this.m_HeroLines.Count != 0 && num >= num2)
			{
				string randomLine = this.m_HeroLines[UnityEngine.Random.Range(0, this.m_HeroLines.Count)];
				yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
				this.m_HeroLines.Remove(randomLine);
				yield return null;
				randomLine = null;
			}
			break;
		}
		case 102:
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventFindCandle_01.prefab:02085733089e04f468b21bd668197916", 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventCandleStolen_01.prefab:e13e3b43ea424f5489d8dcdc2de4a536", 2.5f);
			break;
		case 104:
			GameState.Get().SetBusy(true);
			if (this.m_TogwaggleCandleAttackLines.Count > 0)
			{
				int randomLineIndex = UnityEngine.Random.Range(0, this.m_TogwaggleCandleAttackLines.Count);
				string line = this.m_TogwaggleCandleAttackLines[randomLineIndex];
				this.m_TogwaggleCandleAttackLines.RemoveAt(randomLineIndex);
				yield return base.PlayLineOnlyOnce(enemyActor, line, 2.5f);
				this.m_CandleAttackLineIndex = this.m_indices[randomLineIndex];
				this.m_indices.RemoveAt(randomLineIndex);
			}
			else
			{
				this.m_CandleAttackLineIndex = UnityEngine.Random.Range(0, this.m_RakanishuCandleAttackLines.Count);
			}
			if (this.m_RakanishuCandleAttackLines[this.m_CandleAttackLineIndex] == "VO_Rakanishu_Male_Elemental_CastSpell4_01.prefab:1f3680a17baa7b444bb18d666f813dba")
			{
				this.m_CandleAttackLineIndex = UnityEngine.Random.Range(0, this.m_RakanishuCandleAttackLines.Count);
			}
			yield return base.PlayBossLine(this.m_RakanishuBigQuotePrefab, this.m_RakanishuCandleAttackLines[this.m_CandleAttackLineIndex], 2.5f);
			GameState.Get().SetBusy(false);
			break;
		}
		yield break;
	}

	// Token: 0x0600375C RID: 14172 RVA: 0x00118065 File Offset: 0x00116265
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "LOOTA_843"))
		{
			if (!(cardId == "LOOT_541"))
			{
				if (cardId == "EX1_298")
				{
					yield return base.PlayLineOnlyOnce(this.m_RakanishuBigQuotePrefab, "VO_Rakanishu_Male_Elemental_EventRagnaros_02.prefab:218c7d55526aeca4e94f415d3caa94bd", 2.5f);
				}
			}
			else
			{
				yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayKoboldKing_01.prefab:82ee9011090b3d0478ec63e306d20526", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayCandle_01.prefab:4a9d8402c1a8fa749b223f2ecfccb6ec", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600375D RID: 14173 RVA: 0x0011807B File Offset: 0x0011627B
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn == 2)
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(this.m_RakanishuBigQuotePrefab, "VO_Rakanishu_Male_Elemental_Intro_01.prefab:d37fc90b73f5aa94ca3e14012ed7ba88", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x04001D62 RID: 7522
	private string m_RakanishuBigQuotePrefab = "Rakanishu_BigQuote.prefab:48df090654e138d4d8f8275655206a59";

	// Token: 0x04001D63 RID: 7523
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D64 RID: 7524
	private int m_CandleAttackLineIndex;

	// Token: 0x04001D65 RID: 7525
	private List<string> m_HeroLines = new List<string>
	{
		"VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure1_01.prefab:7dc532104473e1a48b0480c9a4d859ec",
		"VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure2_01.prefab:f1852b13f7af8794880e91ab43e40a42",
		"VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure3_01.prefab:3e487bc1f6aeb754eaf10b8f130bf023",
		"VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure4_01.prefab:1238f261c2268ee4e9aeef376d87d36d",
		"VO_LOOTA_BOSS_12h_Male_Kobold_GetTreasure5_01.prefab:7d8c7ea05e7e9414c97b0574ccc61612"
	};

	// Token: 0x04001D66 RID: 7526
	private List<string> m_TogwaggleCandleAttackLines = new List<string>
	{
		"VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack1_01.prefab:0b541034826dc5043885c26aadeb5aa9",
		"VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack2_01.prefab:f0dd873ec050b254a971e2a3d39a57c1",
		"VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack3_01.prefab:8371c978ea042f8488c479252676315f",
		"VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack4_01.prefab:c04f2a01ff25578478ce1dab6acd9e75",
		"VO_LOOTA_BOSS_12h_Male_Kobold_CandleAttack5_01.prefab:7ccf3f74056e3274386c58f26c41a49a"
	};

	// Token: 0x04001D67 RID: 7527
	private List<int> m_indices = new List<int>
	{
		0,
		1,
		2,
		3,
		4
	};

	// Token: 0x04001D68 RID: 7528
	private List<string> m_RakanishuCandleAttackLines = new List<string>
	{
		"VO_Rakanishu_Male_Elemental_CastSpell3_01.prefab:81a88a21aca788e48bf1f323bce7741c",
		"VO_Rakanishu_Male_Elemental_CastSpell1_01.prefab:55acec97d5f9cf84cb4037a83f1e382f",
		"VO_Rakanishu_Male_Elemental_CastSpell4_01.prefab:1f3680a17baa7b444bb18d666f813dba",
		"VO_Rakanishu_Male_Elemental_CastSpell5_01.prefab:7d14cf1297567d3478762c7400b32823",
		"VO_Rakanishu_Male_Elemental_CastSpell2_01.prefab:b230354a77128944cba89d7f67e57f62"
	};
}
