using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200046A RID: 1130
public class DALA_Dungeon_Boss_61h : DALA_Dungeon
{
	// Token: 0x06003D43 RID: 15683 RVA: 0x00141330 File Offset: 0x0013F530
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_01,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_02,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_03,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_04,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_05,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpiderKill_01,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossSpreadingPlague_01,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_Death_02,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_DefeatPlayer_01,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_EmoteResponse_01,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_01,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_02,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_03,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_04,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_05,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_01,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_02,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerPhaseSpider_01,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_Idle_01,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_Idle_02,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_Idle_03,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_Intro_01,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_PlayerBallOfSpiders_02,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_PlayerPsychicScream_01,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_PlayerSprint_01,
			DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_PlayerWebspinner_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003D44 RID: 15684 RVA: 0x00141534 File Offset: 0x0013F734
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_EmoteResponse_01;
	}

	// Token: 0x06003D45 RID: 15685 RVA: 0x0014156C File Offset: 0x0013F76C
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_61h.m_IdleLines;
	}

	// Token: 0x06003D46 RID: 15686 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003D47 RID: 15687 RVA: 0x00141573 File Offset: 0x0013F773
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_61h.m_BossHeroPower);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_61h.m_BossHeroPowerNother);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerPhaseSpider_01, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpiderKill_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003D48 RID: 15688 RVA: 0x00141589 File Offset: 0x0013F789
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (!(cardId == "AT_062"))
		{
			if (!(cardId == "LOOT_008"))
			{
				if (!(cardId == "CS2_077"))
				{
					if (cardId == "FP1_011")
					{
						yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_PlayerWebspinner_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_PlayerSprint_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_PlayerPsychicScream_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_PlayerBallOfSpiders_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003D49 RID: 15689 RVA: 0x0014159F File Offset: 0x0013F79F
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "DALA_BOSS_61t"))
		{
			if (cardId == "ICC_054")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossSpreadingPlague_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_61h.m_BossPhaseSpider);
		}
		yield break;
	}

	// Token: 0x040027F6 RID: 10230
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_01.prefab:e2b00cc4b7117444b8fb6fad50b81049");

	// Token: 0x040027F7 RID: 10231
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_02 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_02.prefab:6db75ecf1068fc341ac0d2af8631b967");

	// Token: 0x040027F8 RID: 10232
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_03 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_03.prefab:87a836e9e0e627044b9f7aba3f7f98fd");

	// Token: 0x040027F9 RID: 10233
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_04 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_04.prefab:d25c6095327d36946bc09b2ceb989ae5");

	// Token: 0x040027FA RID: 10234
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_05 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_05.prefab:86b99f60dbf2b9044986a8d3e9349c3a");

	// Token: 0x040027FB RID: 10235
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpiderKill_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpiderKill_01.prefab:a0f0aba5f77a0514d9b3cde30888417c");

	// Token: 0x040027FC RID: 10236
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossSpreadingPlague_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossSpreadingPlague_01.prefab:12d3d8b3273863541bef7d00fe1ca97e");

	// Token: 0x040027FD RID: 10237
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_Death_02 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_Death_02.prefab:f74bcb8bfcd2b374faf232796fcfe708");

	// Token: 0x040027FE RID: 10238
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_DefeatPlayer_01.prefab:bac74142c865d294b905cedb4305199f");

	// Token: 0x040027FF RID: 10239
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_EmoteResponse_01.prefab:ce177fd3cdad44649a95033a505d3756");

	// Token: 0x04002800 RID: 10240
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_01.prefab:ef42a5249d098704a9c5677319866470");

	// Token: 0x04002801 RID: 10241
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_02 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_02.prefab:7be30312fdcfc8347ab269bbeac2ae11");

	// Token: 0x04002802 RID: 10242
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_03 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_03.prefab:a2a287b8a8992e3458598f3af2f77062");

	// Token: 0x04002803 RID: 10243
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_04 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_04.prefab:79747c90f7775394f8d51c080143696e");

	// Token: 0x04002804 RID: 10244
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_05 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_05.prefab:0ce87fc719e7a4d45b9533bfbbce090d");

	// Token: 0x04002805 RID: 10245
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_01.prefab:e2b220f2df88a25438a60f5ba74e02e2");

	// Token: 0x04002806 RID: 10246
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_02 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_02.prefab:d338bc84f9e7895439edde1b3ae4afee");

	// Token: 0x04002807 RID: 10247
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerPhaseSpider_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerPhaseSpider_01.prefab:1ba74c41b50fa994ea9d2541b3cf96f1");

	// Token: 0x04002808 RID: 10248
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_Idle_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_Idle_01.prefab:1d9d125fa4c28d147b73815fa14fece5");

	// Token: 0x04002809 RID: 10249
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_Idle_02 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_Idle_02.prefab:d2dd489c53f062c40b6dc1de5f316ee0");

	// Token: 0x0400280A RID: 10250
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_Idle_03 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_Idle_03.prefab:e39d4d53cf0fefc4eacdaaab670bb96d");

	// Token: 0x0400280B RID: 10251
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_Intro_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_Intro_01.prefab:342e742f2b674314f89b85cf43ac700e");

	// Token: 0x0400280C RID: 10252
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_PlayerBallOfSpiders_02 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_PlayerBallOfSpiders_02.prefab:13b7b135706cb2543a783ccb684e585c");

	// Token: 0x0400280D RID: 10253
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_PlayerPsychicScream_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_PlayerPsychicScream_01.prefab:64bfe9d1b5e9241498d27222a396be69");

	// Token: 0x0400280E RID: 10254
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_PlayerSprint_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_PlayerSprint_01.prefab:d41ff9a919cf3dd41a1a776f38c7c248");

	// Token: 0x0400280F RID: 10255
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_PlayerWebspinner_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_PlayerWebspinner_01.prefab:1adb258e203c9a84987fe9fdeb460644");

	// Token: 0x04002810 RID: 10256
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04002811 RID: 10257
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_Idle_01,
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_Idle_02,
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_Idle_03
	};

	// Token: 0x04002812 RID: 10258
	private static List<string> m_BossPhaseSpider = new List<string>
	{
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_01,
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_02,
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_03,
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_04,
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_05
	};

	// Token: 0x04002813 RID: 10259
	private static List<string> m_BossHeroPower = new List<string>
	{
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_01,
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_02,
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_03,
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_04,
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_05
	};

	// Token: 0x04002814 RID: 10260
	private static List<string> m_BossHeroPowerNother = new List<string>
	{
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_01,
		DALA_Dungeon_Boss_61h.VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_02
	};
}
