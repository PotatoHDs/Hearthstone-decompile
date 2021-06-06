using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200048F RID: 1167
public class ULDA_Dungeon_Boss_18h : ULDA_Dungeon
{
	// Token: 0x06003EFE RID: 16126 RVA: 0x0014E274 File Offset: 0x0014C474
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_BossCurseofRafaam_01,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_BossCurseofWeakness_01,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_BossLeperGnome_01,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_Death_01,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_DefeatPlayer_01,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_EmoteResponse_01,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_HeroPower_02,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_HeroPower_03,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_HeroPower_04,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_Idle_01,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_Idle_02,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_Idle_03,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_Intro_01,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_IntroFinleyResponse_01,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_PlayerPilotedShredder_01,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_PlayerPlague_01,
			ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_PlayerThermaplugg_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003EFF RID: 16127 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003F00 RID: 16128 RVA: 0x0014E3E8 File Offset: 0x0014C5E8
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003F01 RID: 16129 RVA: 0x0014E3F0 File Offset: 0x0014C5F0
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003F02 RID: 16130 RVA: 0x0014E3F8 File Offset: 0x0014C5F8
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_EmoteResponse_01;
	}

	// Token: 0x06003F03 RID: 16131 RVA: 0x0014E430 File Offset: 0x0014C630
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_IntroFinleyResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "ULDA_Reno" && cardId != "ULDA_Brann")
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

	// Token: 0x06003F04 RID: 16132 RVA: 0x0014E527 File Offset: 0x0014C727
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003F05 RID: 16133 RVA: 0x0014E53D File Offset: 0x0014C73D
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 316364135U)
		{
			if (num != 232623135U)
			{
				if (num != 282955992U)
				{
					if (num != 316364135U)
					{
						goto IL_299;
					}
					if (!(cardId == "ULD_707"))
					{
						goto IL_299;
					}
				}
				else if (!(cardId == "ULD_715"))
				{
					goto IL_299;
				}
			}
			else if (!(cardId == "ULD_718"))
			{
				goto IL_299;
			}
		}
		else if (num <= 930039318U)
		{
			if (num != 316511230U)
			{
				if (num != 930039318U)
				{
					goto IL_299;
				}
				if (!(cardId == "GVG_116"))
				{
					goto IL_299;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_PlayerThermaplugg_01, 2.5f);
				goto IL_299;
			}
			else if (!(cardId == "ULD_717"))
			{
				goto IL_299;
			}
		}
		else if (num != 1777725921U)
		{
			if (num != 3704163853U)
			{
				goto IL_299;
			}
			if (!(cardId == "GVG_096"))
			{
				goto IL_299;
			}
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_PlayerPilotedShredder_01, 2.5f);
			goto IL_299;
		}
		else if (!(cardId == "ULD_172"))
		{
			goto IL_299;
		}
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_PlayerPlague_01, 2.5f);
		IL_299:
		yield break;
	}

	// Token: 0x06003F06 RID: 16134 RVA: 0x0014E553 File Offset: 0x0014C753
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
		if (!(cardId == "LOE_007"))
		{
			if (!(cardId == "GIL_665"))
			{
				if (cardId == "EX1_029")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_BossLeperGnome_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_BossCurseofWeakness_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_BossCurseofRafaam_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002B98 RID: 11160
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_BossCurseofRafaam_01 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_BossCurseofRafaam_01.prefab:8d3aabd960041cb449ccfbb70c8c1909");

	// Token: 0x04002B99 RID: 11161
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_BossCurseofWeakness_01 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_BossCurseofWeakness_01.prefab:985a74422cace944dadb7c1222ebae4c");

	// Token: 0x04002B9A RID: 11162
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_BossLeperGnome_01 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_BossLeperGnome_01.prefab:7c6cca069aee4a44ca055568ef8402a3");

	// Token: 0x04002B9B RID: 11163
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_Death_01 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_Death_01.prefab:15179da5009a1524991a7b1d094940cc");

	// Token: 0x04002B9C RID: 11164
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_DefeatPlayer_01.prefab:508e5a159c9be324a9a2686ee50db98c");

	// Token: 0x04002B9D RID: 11165
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_EmoteResponse_01.prefab:0d17d0519ec7f9e449e0766d3cd90036");

	// Token: 0x04002B9E RID: 11166
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_HeroPower_02.prefab:9ff0d01cf24d228498180fc49cd7f388");

	// Token: 0x04002B9F RID: 11167
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_HeroPower_03.prefab:aa72ac465697c3d4395ece51b5d18267");

	// Token: 0x04002BA0 RID: 11168
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_HeroPower_04.prefab:1f6f035fa5adf254985140ece7fadc31");

	// Token: 0x04002BA1 RID: 11169
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_Idle_01 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_Idle_01.prefab:dd295f7b1d6ef2b42a3dbce95159f796");

	// Token: 0x04002BA2 RID: 11170
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_Idle_02 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_Idle_02.prefab:f2027a387beaba340becba9af5641a5f");

	// Token: 0x04002BA3 RID: 11171
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_Idle_03 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_Idle_03.prefab:9048b74a696b9a74cac55fe27089b2e2");

	// Token: 0x04002BA4 RID: 11172
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_Intro_01 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_Intro_01.prefab:f1417663b85d9294eaa192946a4de224");

	// Token: 0x04002BA5 RID: 11173
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_IntroFinleyResponse_01 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_IntroFinleyResponse_01.prefab:d18c0430263ac0a41b8c7200fadfc309");

	// Token: 0x04002BA6 RID: 11174
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_PlayerPilotedShredder_01 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_PlayerPilotedShredder_01.prefab:1a18075e95afd3749ac1e39240cb8acc");

	// Token: 0x04002BA7 RID: 11175
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_PlayerPlague_01 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_PlayerPlague_01.prefab:b4531194ef377fd47a862204938dbda8");

	// Token: 0x04002BA8 RID: 11176
	private static readonly AssetReference VO_ULDA_BOSS_18h_Male_Gnome_PlayerThermaplugg_01 = new AssetReference("VO_ULDA_BOSS_18h_Male_Gnome_PlayerThermaplugg_01.prefab:5f848ea8ad1536e4fbba109d53afdff6");

	// Token: 0x04002BA9 RID: 11177
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_HeroPower_02,
		ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_HeroPower_03,
		ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_HeroPower_04
	};

	// Token: 0x04002BAA RID: 11178
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_Idle_01,
		ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_Idle_02,
		ULDA_Dungeon_Boss_18h.VO_ULDA_BOSS_18h_Male_Gnome_Idle_03
	};

	// Token: 0x04002BAB RID: 11179
	private HashSet<string> m_playedLines = new HashSet<string>();
}
