using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000430 RID: 1072
public class DALA_Dungeon_Boss_03h : DALA_Dungeon
{
	// Token: 0x06003A6C RID: 14956 RVA: 0x0012D88C File Offset: 0x0012BA8C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_01,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_02,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_03,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossFireSpell_01,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossPresto_01,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossPresto_02,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossPresto_03,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossPresto_04,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_01,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_02,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_03,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossSpell_01,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossSpell_02,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_01,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_02,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_03,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_Death_03,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_DefeatPlayer_02,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_EmoteResponse_01,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_HeroPower_02,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_HeroPower_03,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_HeroPower_04,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_Idle_01,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_Idle_02,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_Idle_04,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_Intro_01,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_IntroGeorge_01,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_01,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003A6D RID: 14957 RVA: 0x0012DAC0 File Offset: 0x0012BCC0
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_HeroPower_02,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_HeroPower_03,
			DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_HeroPower_04
		};
	}

	// Token: 0x06003A6E RID: 14958 RVA: 0x0012DAF7 File Offset: 0x0012BCF7
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_03h.m_IdleLines;
	}

	// Token: 0x06003A6F RID: 14959 RVA: 0x0012DAFE File Offset: 0x0012BCFE
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_Death_03;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_EmoteResponse_01;
	}

	// Token: 0x06003A70 RID: 14960 RVA: 0x0012DB38 File Offset: 0x0012BD38
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Chu" && cardId != "DALA_Vessina")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		else
		{
			if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x06003A71 RID: 14961 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003A72 RID: 14962 RVA: 0x0012DC2F File Offset: 0x0012BE2F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 102)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossPresto_04, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BossSpell);
		}
		yield break;
	}

	// Token: 0x06003A73 RID: 14963 RVA: 0x0012DC45 File Offset: 0x0012BE45
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (cardId == "DALA_BOSS_03t" || cardId == "DALA_BOSS_03t2" || cardId == "DALA_BOSS_03t3" || cardId == "DALA_BOSS_03t4")
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_PlayerBossSpell);
		}
		yield break;
	}

	// Token: 0x06003A74 RID: 14964 RVA: 0x0012DC5B File Offset: 0x0012BE5B
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "DALA_BOSS_03t"))
		{
			if (!(cardId == "DALA_BOSS_03t2"))
			{
				if (!(cardId == "DALA_BOSS_03t3"))
				{
					if (!(cardId == "DALA_BOSS_03t4"))
					{
						if (cardId == "GIL_147")
						{
							yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossFireSpell_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BossYoggers);
					}
				}
				else
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BossPresto);
				}
			}
			else
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BossBunnifitronus);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BossReductomara);
		}
		yield break;
	}

	// Token: 0x0400221D RID: 8733
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_01.prefab:16976478bdca1bb41a4cd3903a2e502e");

	// Token: 0x0400221E RID: 8734
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_02.prefab:87b79e63418b6f24e827268842a28e2f");

	// Token: 0x0400221F RID: 8735
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_03 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_03.prefab:b8211fca6a3cd614a84cd09b6044449d");

	// Token: 0x04002220 RID: 8736
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossFireSpell_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossFireSpell_01.prefab:708c0acd0b75eef43b67a7f1efe0f43f");

	// Token: 0x04002221 RID: 8737
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossPresto_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossPresto_01.prefab:b069918550be0a24e8a85dd04fe847b3");

	// Token: 0x04002222 RID: 8738
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossPresto_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossPresto_02.prefab:b4ebbcbf9f919984590b27ef49262623");

	// Token: 0x04002223 RID: 8739
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossPresto_03 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossPresto_03.prefab:f44f15adb95d7cb4c812671be1cc1c7a");

	// Token: 0x04002224 RID: 8740
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossPresto_04 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossPresto_04.prefab:ed5f44d3a5b90764f8bfa3fbb5926eeb");

	// Token: 0x04002225 RID: 8741
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_01.prefab:5c0002137d68f17468f48f752e4cae6e");

	// Token: 0x04002226 RID: 8742
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_02.prefab:8297447eaf8711845ac24407433e3e2e");

	// Token: 0x04002227 RID: 8743
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_03 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_03.prefab:0f0347be7d6966c4d9877b8d42912837");

	// Token: 0x04002228 RID: 8744
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossSpell_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossSpell_01.prefab:a7b9ab72aa814264b913486991e71b10");

	// Token: 0x04002229 RID: 8745
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossSpell_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossSpell_02.prefab:cc6765c289562e7478459df57a36dddc");

	// Token: 0x0400222A RID: 8746
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_01.prefab:6b4ecb20342992c49a31676700e13988");

	// Token: 0x0400222B RID: 8747
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_02.prefab:bdf4e0ddd4e025645ac57c780b747e8e");

	// Token: 0x0400222C RID: 8748
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_03 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_03.prefab:cd6099343fd020544968c54145c893e8");

	// Token: 0x0400222D RID: 8749
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_Death_03 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_Death_03.prefab:62438176772786e4383069d604e42766");

	// Token: 0x0400222E RID: 8750
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_DefeatPlayer_02.prefab:7194f7ddc4463524984926fe6e687a0f");

	// Token: 0x0400222F RID: 8751
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_EmoteResponse_01.prefab:209c31fdaf50f4949871aed09e282485");

	// Token: 0x04002230 RID: 8752
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_HeroPower_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_HeroPower_02.prefab:8c0d04d7904b2024096f83ee6ced3091");

	// Token: 0x04002231 RID: 8753
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_HeroPower_03 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_HeroPower_03.prefab:5559e68af0ac45e46a14efb8d91c970c");

	// Token: 0x04002232 RID: 8754
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_HeroPower_04 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_HeroPower_04.prefab:38fb3ef64c2e2b94cbc73a5a4cee61eb");

	// Token: 0x04002233 RID: 8755
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_Idle_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_Idle_01.prefab:f93ca7e9021783d42a38c42d5a07a094");

	// Token: 0x04002234 RID: 8756
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_Idle_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_Idle_02.prefab:99d3be1976028a8439a4bddba50c0b6d");

	// Token: 0x04002235 RID: 8757
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_Idle_04 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_Idle_04.prefab:99c4f5047113f9247be1ce60fff837fa");

	// Token: 0x04002236 RID: 8758
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_Intro_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_Intro_01.prefab:75457983cd77c824d8fefa9b5196ee9e");

	// Token: 0x04002237 RID: 8759
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_IntroGeorge_01.prefab:8dd82ee0ffe129a4aa4ae109391ba793");

	// Token: 0x04002238 RID: 8760
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_01.prefab:94fce89c59818ef4d85fbf51b1bdf314");

	// Token: 0x04002239 RID: 8761
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_02.prefab:ff5ccb4f4aa2b35499b0419a47610fa6");

	// Token: 0x0400223A RID: 8762
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_Idle_01,
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_Idle_02,
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_Idle_04
	};

	// Token: 0x0400223B RID: 8763
	private List<string> m_BossReductomara = new List<string>
	{
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_01,
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_02,
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_03
	};

	// Token: 0x0400223C RID: 8764
	private List<string> m_BossBunnifitronus = new List<string>
	{
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_01,
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_02,
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_03
	};

	// Token: 0x0400223D RID: 8765
	private List<string> m_BossPresto = new List<string>
	{
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossPresto_01,
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossPresto_02,
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossPresto_03
	};

	// Token: 0x0400223E RID: 8766
	private List<string> m_BossYoggers = new List<string>
	{
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_01,
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_02,
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_03
	};

	// Token: 0x0400223F RID: 8767
	private List<string> m_BossSpell = new List<string>
	{
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossSpell_01,
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_BossSpell_02
	};

	// Token: 0x04002240 RID: 8768
	private List<string> m_PlayerBossSpell = new List<string>
	{
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_01,
		DALA_Dungeon_Boss_03h.VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_02
	};

	// Token: 0x04002241 RID: 8769
	private HashSet<string> m_playedLines = new HashSet<string>();
}
