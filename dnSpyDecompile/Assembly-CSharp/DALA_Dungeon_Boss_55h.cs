using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000464 RID: 1124
public class DALA_Dungeon_Boss_55h : DALA_Dungeon
{
	// Token: 0x06003CF8 RID: 15608 RVA: 0x0013E868 File Offset: 0x0013CA68
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_BossIronforgePortal_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_BossWeaponEquip_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Death_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_DefeatPlayer_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_EmoteResponse_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Exposition_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Exposition_02,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPower_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPower_02,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPower_04,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPower_05,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPowerFull_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPowerMany_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPowerMany_02,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPowerMany_03,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Idle_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Idle_02,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Idle_03,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Intro_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_IntroGeorge_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_PlayerBigBoomba_02,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_PlayerHyperblaster_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_PlayerLackey_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_PlayerVillainLegendary_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_PlayerVioletWarden_01,
			DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_TurnOne_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003CF9 RID: 15609 RVA: 0x0013EA6C File Offset: 0x0013CC6C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_EmoteResponse_01;
	}

	// Token: 0x06003CFA RID: 15610 RVA: 0x0013EAA4 File Offset: 0x0013CCA4
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_55h.m_IdleLines;
	}

	// Token: 0x06003CFB RID: 15611 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003CFC RID: 15612 RVA: 0x0013EAAC File Offset: 0x0013CCAC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Eudora")
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

	// Token: 0x06003CFD RID: 15613 RVA: 0x0013EB93 File Offset: 0x0013CD93
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_TurnOne_02, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Exposition_02, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Exposition_01, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_BossWeaponEquip_01, 2.5f);
			break;
		case 105:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_55h.m_BossHeroPower);
			break;
		case 106:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_55h.m_HeroPowerMany);
			break;
		case 107:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPowerFull_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003CFE RID: 15614 RVA: 0x0013EBA9 File Offset: 0x0013CDA9
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
		if (num > 2741455189U)
		{
			if (num <= 3598724010U)
			{
				if (num != 2832205903U)
				{
					if (num != 3514835915U)
					{
						if (num != 3598724010U)
						{
							goto IL_3DF;
						}
						if (!(cardId == "DAL_614"))
						{
							goto IL_3DF;
						}
					}
					else if (!(cardId == "DAL_613"))
					{
						goto IL_3DF;
					}
				}
				else
				{
					if (!(cardId == "LOE_092"))
					{
						goto IL_3DF;
					}
					goto IL_3AD;
				}
			}
			else if (num != 3615501629U)
			{
				if (num != 3785628939U)
				{
					if (num != 3786761772U)
					{
						goto IL_3DF;
					}
					if (!(cardId == "DAL_739"))
					{
						goto IL_3DF;
					}
				}
				else if (!(cardId == "DAL_741"))
				{
					goto IL_3DF;
				}
			}
			else if (!(cardId == "DAL_615"))
			{
				goto IL_3DF;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_PlayerLackey_01, 2.5f);
			goto IL_3DF;
		}
		if (num <= 1313737616U)
		{
			if (num != 476329529U)
			{
				if (num != 778032481U)
				{
					if (num != 1313737616U)
					{
						goto IL_3DF;
					}
					if (!(cardId == "DALA_723"))
					{
						goto IL_3DF;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_PlayerHyperblaster_01, 2.5f);
					goto IL_3DF;
				}
				else if (!(cardId == "DAL_417"))
				{
					goto IL_3DF;
				}
			}
			else if (!(cardId == "DAL_431"))
			{
				goto IL_3DF;
			}
		}
		else if (num != 1431180949U)
		{
			if (num != 1778382322U)
			{
				if (num != 2741455189U)
				{
					goto IL_3DF;
				}
				if (!(cardId == "DAL_422"))
				{
					goto IL_3DF;
				}
			}
			else
			{
				if (!(cardId == "DAL_096"))
				{
					goto IL_3DF;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_PlayerVioletWarden_01, 2.5f);
				goto IL_3DF;
			}
		}
		else
		{
			if (!(cardId == "DALA_724"))
			{
				goto IL_3DF;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_PlayerBigBoomba_02, 2.5f);
			goto IL_3DF;
		}
		IL_3AD:
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_PlayerVillainLegendary_01, 2.5f);
		IL_3DF:
		yield break;
	}

	// Token: 0x06003CFF RID: 15615 RVA: 0x0013EBBF File Offset: 0x0013CDBF
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
		if (cardId == "KAR_091")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_BossIronforgePortal_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002712 RID: 10002
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_BossIronforgePortal_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_BossIronforgePortal_01.prefab:a71e219608fcdd04aa4a7d4776c1a558");

	// Token: 0x04002713 RID: 10003
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_BossWeaponEquip_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_BossWeaponEquip_01.prefab:55d140ff585b4b64397788bd07c54a76");

	// Token: 0x04002714 RID: 10004
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_Death_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_Death_01.prefab:7fa220001eeb478418dd7b7309d03c10");

	// Token: 0x04002715 RID: 10005
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_DefeatPlayer_01.prefab:a912782e2b587744793d2d18b41b02e6");

	// Token: 0x04002716 RID: 10006
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_EmoteResponse_01.prefab:53656d2bc6cffb64b99c6bb72b9fd792");

	// Token: 0x04002717 RID: 10007
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_Exposition_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_Exposition_01.prefab:7b0f11701b6c2b04ea634a560ae1acbf");

	// Token: 0x04002718 RID: 10008
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_Exposition_02 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_Exposition_02.prefab:4e0c4d020ba2a8943aadcd87980c01bb");

	// Token: 0x04002719 RID: 10009
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_HeroPower_01.prefab:c638bc2846bb6c141b58fd66b74c1fc4");

	// Token: 0x0400271A RID: 10010
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_HeroPower_02.prefab:464d1914a1522984c8849d9205ffc1a0");

	// Token: 0x0400271B RID: 10011
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_HeroPower_04.prefab:f75f61a614eea2c48ac339eecba56bc9");

	// Token: 0x0400271C RID: 10012
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_HeroPower_05 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_HeroPower_05.prefab:f1c267858447f2f4ea8dba80caeffb39");

	// Token: 0x0400271D RID: 10013
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_HeroPowerFull_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_HeroPowerFull_01.prefab:2cd82130b7e62d04db7495d009f7d51f");

	// Token: 0x0400271E RID: 10014
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_HeroPowerMany_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_HeroPowerMany_01.prefab:d3dea3a9cf41a764f97a3ee848cf5c5f");

	// Token: 0x0400271F RID: 10015
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_HeroPowerMany_02 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_HeroPowerMany_02.prefab:06adc3a5006373144860e6cce662733d");

	// Token: 0x04002720 RID: 10016
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_HeroPowerMany_03 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_HeroPowerMany_03.prefab:a3e3f29b886948240831a44c2c9207fc");

	// Token: 0x04002721 RID: 10017
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_Idle_01.prefab:0b0b7eacdfb36454cb6f324dba55090d");

	// Token: 0x04002722 RID: 10018
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_Idle_02.prefab:24ae67e54b0a0fd459d810f2f5b02d5a");

	// Token: 0x04002723 RID: 10019
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_Idle_03.prefab:99dd91211c559cf4083ae5ac757c44f4");

	// Token: 0x04002724 RID: 10020
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_Intro_01.prefab:4c7229cf7ae7880489ae19653691c1db");

	// Token: 0x04002725 RID: 10021
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_IntroGeorge_01.prefab:8ae3e8bbd355a584c9af8ecbba373abc");

	// Token: 0x04002726 RID: 10022
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_PlayerBigBoomba_02 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_PlayerBigBoomba_02.prefab:a3f6f447aac8f9b4ba621c8308ea0b10");

	// Token: 0x04002727 RID: 10023
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_PlayerHyperblaster_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_PlayerHyperblaster_01.prefab:e594eecd9eb04bc479ef16d5989a9b48");

	// Token: 0x04002728 RID: 10024
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_PlayerLackey_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_PlayerLackey_01.prefab:73a116e98338275469094d59195d1013");

	// Token: 0x04002729 RID: 10025
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_PlayerVillainLegendary_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_PlayerVillainLegendary_01.prefab:d1a78bd5a712a8b4cb8600d82d5b0c96");

	// Token: 0x0400272A RID: 10026
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_PlayerVioletWarden_01 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_PlayerVioletWarden_01.prefab:f47bdd589e1ee614c852c3eb092df7a9");

	// Token: 0x0400272B RID: 10027
	private static readonly AssetReference VO_DALA_BOSS_55h_Male_Human_TurnOne_02 = new AssetReference("VO_DALA_BOSS_55h_Male_Human_TurnOne_02.prefab:41bb39d61593b7c42a519d772521aeac");

	// Token: 0x0400272C RID: 10028
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Idle_01,
		DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Idle_02,
		DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_Idle_03
	};

	// Token: 0x0400272D RID: 10029
	private static List<string> m_BossHeroPower = new List<string>
	{
		DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPower_01,
		DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPower_02,
		DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPower_04,
		DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPower_05
	};

	// Token: 0x0400272E RID: 10030
	private static List<string> m_HeroPowerMany = new List<string>
	{
		DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPowerMany_01,
		DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPowerMany_02,
		DALA_Dungeon_Boss_55h.VO_DALA_BOSS_55h_Male_Human_HeroPowerMany_03
	};

	// Token: 0x0400272F RID: 10031
	private HashSet<string> m_playedLines = new HashSet<string>();
}
