using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000454 RID: 1108
public class DALA_Dungeon_Boss_39h : DALA_Dungeon
{
	// Token: 0x06003C32 RID: 15410 RVA: 0x001399AC File Offset: 0x00137BAC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_Death_01,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_DefeatPlayer_01,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_EmoteResponse_01,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_HeroPower_01,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_HeroPower_02,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_HeroPower_03,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_HeroPower_04,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_HeroPower_05,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_HeroPower_06,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_Idle_01,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_Idle_02,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_Idle_03,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_Intro_01,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_IntroGeorge_01,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_PlayerDeathKnight_01,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorge_01,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorgeSTART_01,
			DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_PlayerHolySpell_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003C33 RID: 15411 RVA: 0x00139B30 File Offset: 0x00137D30
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_EmoteResponse_01;
	}

	// Token: 0x06003C34 RID: 15412 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003C35 RID: 15413 RVA: 0x00139B68 File Offset: 0x00137D68
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_39h.m_IdleLines;
	}

	// Token: 0x06003C36 RID: 15414 RVA: 0x00139B6F File Offset: 0x00137D6F
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorge_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorgeSTART_01, 2.5f);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_39h.m_HeroPower);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003C37 RID: 15415 RVA: 0x00139B88 File Offset: 0x00137D88
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003C38 RID: 15416 RVA: 0x00139C33 File Offset: 0x00137E33
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 615862462U)
		{
			if (num <= 531974367U)
			{
				if (num <= 515196748U)
				{
					if (num != 454320641U)
					{
						if (num != 515196748U)
						{
							goto IL_3FE;
						}
						if (!(cardId == "ICC_829"))
						{
							goto IL_3FE;
						}
					}
					else
					{
						if (!(cardId == "AT_011"))
						{
							goto IL_3FE;
						}
						goto IL_3CD;
					}
				}
				else if (num != 531827272U)
				{
					if (num != 531974367U)
					{
						goto IL_3FE;
					}
					if (!(cardId == "ICC_828"))
					{
						goto IL_3FE;
					}
				}
				else if (!(cardId == "ICC_832"))
				{
					goto IL_3FE;
				}
			}
			else if (num <= 565382510U)
			{
				if (num != 548604891U)
				{
					if (num != 565382510U)
					{
						goto IL_3FE;
					}
					if (!(cardId == "ICC_830"))
					{
						goto IL_3FE;
					}
				}
				else if (!(cardId == "ICC_833"))
				{
					goto IL_3FE;
				}
			}
			else if (num != 582160129U)
			{
				if (num != 615862462U)
				{
					goto IL_3FE;
				}
				if (!(cardId == "ICC_827"))
				{
					goto IL_3FE;
				}
			}
			else if (!(cardId == "ICC_831"))
			{
				goto IL_3FE;
			}
		}
		else if (num <= 1863182255U)
		{
			if (num <= 962160208U)
			{
				if (num != 632492986U)
				{
					if (num != 962160208U)
					{
						goto IL_3FE;
					}
					if (!(cardId == "GIL_134"))
					{
						goto IL_3FE;
					}
					goto IL_3CD;
				}
				else if (!(cardId == "ICC_834"))
				{
					goto IL_3FE;
				}
			}
			else if (num != 963337312U)
			{
				if (num != 1863182255U)
				{
					goto IL_3FE;
				}
				if (!(cardId == "CS2_089"))
				{
					goto IL_3FE;
				}
				goto IL_3CD;
			}
			else
			{
				if (!(cardId == "EX1_624"))
				{
					goto IL_3FE;
				}
				goto IL_3CD;
			}
		}
		else if (num <= 1999498950U)
		{
			if (num != 1871136581U)
			{
				if (num != 1999498950U)
				{
					goto IL_3FE;
				}
				if (!(cardId == "ICC_481"))
				{
					goto IL_3FE;
				}
			}
			else
			{
				if (!(cardId == "CS1_130"))
				{
					goto IL_3FE;
				}
				goto IL_3CD;
			}
		}
		else if (num != 2031105540U)
		{
			if (num != 3683340220U)
			{
				if (num != 4253852669U)
				{
					goto IL_3FE;
				}
				if (!(cardId == "CS1_112"))
				{
					goto IL_3FE;
				}
				goto IL_3CD;
			}
			else
			{
				if (!(cardId == "EX1_365"))
				{
					goto IL_3FE;
				}
				goto IL_3CD;
			}
		}
		else
		{
			if (!(cardId == "CS2_093"))
			{
				goto IL_3FE;
			}
			goto IL_3CD;
		}
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_PlayerDeathKnight_01, 2.5f);
		goto IL_3FE;
		IL_3CD:
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_PlayerHolySpell_01, 2.5f);
		IL_3FE:
		yield break;
	}

	// Token: 0x06003C39 RID: 15417 RVA: 0x00139C49 File Offset: 0x00137E49
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
		yield break;
	}

	// Token: 0x04002593 RID: 9619
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_Death_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_Death_01.prefab:e6054b803c717e24c9208f51763cff93");

	// Token: 0x04002594 RID: 9620
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_DefeatPlayer_01.prefab:5a1bed9db02a44640b9fb292539ee3b6");

	// Token: 0x04002595 RID: 9621
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_EmoteResponse_01.prefab:5e821931754f8e14d90accf92242ef2c");

	// Token: 0x04002596 RID: 9622
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_HeroPower_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_HeroPower_01.prefab:afc618ecc329f624bac4643e1e50b00c");

	// Token: 0x04002597 RID: 9623
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_HeroPower_02 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_HeroPower_02.prefab:71b540dbd96248649bdba9536cb7981e");

	// Token: 0x04002598 RID: 9624
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_HeroPower_03 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_HeroPower_03.prefab:60b7d0c6d14c3784d8499ae21846a66b");

	// Token: 0x04002599 RID: 9625
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_HeroPower_04 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_HeroPower_04.prefab:cbf2c89b47d1e654ba9b189351f6a4fa");

	// Token: 0x0400259A RID: 9626
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_HeroPower_05 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_HeroPower_05.prefab:c5c668583ca30a6418a3a94c44242408");

	// Token: 0x0400259B RID: 9627
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_HeroPower_06 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_HeroPower_06.prefab:b80aeb9cf33e46149a37d4d21d73a74f");

	// Token: 0x0400259C RID: 9628
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_Idle_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_Idle_01.prefab:4957cfb564c733947bcf4e45022a34a2");

	// Token: 0x0400259D RID: 9629
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_Idle_02 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_Idle_02.prefab:365686a1ce5a60642b536ff814f70e69");

	// Token: 0x0400259E RID: 9630
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_Idle_03 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_Idle_03.prefab:0fd376c26c189df478cedbc3fb1f2881");

	// Token: 0x0400259F RID: 9631
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_Intro_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_Intro_01.prefab:a3659800ac23e2c428b2e1b44dd4c4b8");

	// Token: 0x040025A0 RID: 9632
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_IntroGeorge_01.prefab:9e49459701d5a284eb0da6665fceb7dd");

	// Token: 0x040025A1 RID: 9633
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_PlayerDeathKnight_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_PlayerDeathKnight_01.prefab:b0e5fac69736c3945986027ce6272a33");

	// Token: 0x040025A2 RID: 9634
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorge_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorge_01.prefab:016abe45f62b7014ebc93068c91292a5");

	// Token: 0x040025A3 RID: 9635
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorgeSTART_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_PlayerGeorgeSTART_01.prefab:496837dce3c5e5d43b1110afcb3951d4");

	// Token: 0x040025A4 RID: 9636
	private static readonly AssetReference VO_DALA_BOSS_39h_Male_Draenei_PlayerHolySpell_01 = new AssetReference("VO_DALA_BOSS_39h_Male_Draenei_PlayerHolySpell_01.prefab:47c09de8023ee3e4b8dc91a461afce57");

	// Token: 0x040025A5 RID: 9637
	private static List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_HeroPower_01,
		DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_HeroPower_02,
		DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_HeroPower_03,
		DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_HeroPower_04,
		DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_HeroPower_05,
		DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_HeroPower_06
	};

	// Token: 0x040025A6 RID: 9638
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x040025A7 RID: 9639
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_Idle_01,
		DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_Idle_02,
		DALA_Dungeon_Boss_39h.VO_DALA_BOSS_39h_Male_Draenei_Idle_03
	};
}
