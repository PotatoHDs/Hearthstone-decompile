using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200049C RID: 1180
public class ULDA_Dungeon_Boss_31h : ULDA_Dungeon
{
	// Token: 0x06003F9F RID: 16287 RVA: 0x001514D4 File Offset: 0x0014F6D4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_BossTrogg_01,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_BossTroggzor_01,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_BossWeapon_01,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_Death_01,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_DefeatPlayer_01,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_EmoteResponse_01,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_HeroPower_02,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_HeroPower_03,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_HeroPower_04,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_HeroPowerTrogg_01,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_Idle_01,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_Idle_02,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_Intro_01,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_PlayerRefreshmentVendor_01,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_PlayerSpell_01,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_PlayerTrogg_01,
			ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_PlayerUndercityHuckster_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003FA0 RID: 16288 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003FA1 RID: 16289 RVA: 0x00151648 File Offset: 0x0014F848
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003FA2 RID: 16290 RVA: 0x00151650 File Offset: 0x0014F850
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_EmoteResponse_01;
	}

	// Token: 0x06003FA3 RID: 16291 RVA: 0x00151688 File Offset: 0x0014F888
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "ULDA_Reno")
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

	// Token: 0x06003FA4 RID: 16292 RVA: 0x00151733 File Offset: 0x0014F933
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_HeroPowerTrogg_01, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_BossWeapon_01, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_PlayerSpell_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003FA5 RID: 16293 RVA: 0x00151749 File Offset: 0x0014F949
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
		if (num <= 1423937860U)
		{
			if (num <= 1050107919U)
			{
				if (num != 646074946U)
				{
					if (num != 1050107919U)
					{
						goto IL_2CD;
					}
					if (!(cardId == "LOOT_315"))
					{
						goto IL_2CD;
					}
				}
				else
				{
					if (!(cardId == "AT_111"))
					{
						goto IL_2CD;
					}
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_PlayerRefreshmentVendor_01, 2.5f);
					goto IL_2CD;
				}
			}
			else if (num != 1272939289U)
			{
				if (num != 1423937860U)
				{
					goto IL_2CD;
				}
				if (!(cardId == "GVG_068"))
				{
					goto IL_2CD;
				}
			}
			else if (!(cardId == "GVG_067"))
			{
				goto IL_2CD;
			}
		}
		else if (num <= 2246961374U)
		{
			if (num != 1945204413U)
			{
				if (num != 2246961374U)
				{
					goto IL_2CD;
				}
				if (!(cardId == "OG_330"))
				{
					goto IL_2CD;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_PlayerUndercityHuckster_01, 2.5f);
				goto IL_2CD;
			}
			else if (!(cardId == "LOOTA_109"))
			{
				goto IL_2CD;
			}
		}
		else if (num != 2998702165U)
		{
			if (num != 3348168750U)
			{
				goto IL_2CD;
			}
			if (!(cardId == "CFM_338"))
			{
				goto IL_2CD;
			}
		}
		else if (!(cardId == "LOE_018"))
		{
			goto IL_2CD;
		}
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_PlayerTrogg_01, 2.5f);
		IL_2CD:
		yield break;
	}

	// Token: 0x06003FA6 RID: 16294 RVA: 0x0015175F File Offset: 0x0014F95F
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 1272939289U)
		{
			if (num != 1050107919U)
			{
				if (num != 1097815508U)
				{
					if (num != 1272939289U)
					{
						goto IL_25A;
					}
					if (!(cardId == "GVG_067"))
					{
						goto IL_25A;
					}
				}
				else
				{
					if (!(cardId == "GVG_118"))
					{
						goto IL_25A;
					}
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_BossTroggzor_01, 2.5f);
					goto IL_25A;
				}
			}
			else if (!(cardId == "LOOT_315"))
			{
				goto IL_25A;
			}
		}
		else if (num <= 1945204413U)
		{
			if (num != 1423937860U)
			{
				if (num != 1945204413U)
				{
					goto IL_25A;
				}
				if (!(cardId == "LOOTA_109"))
				{
					goto IL_25A;
				}
			}
			else if (!(cardId == "GVG_068"))
			{
				goto IL_25A;
			}
		}
		else if (num != 2998702165U)
		{
			if (num != 3348168750U)
			{
				goto IL_25A;
			}
			if (!(cardId == "CFM_338"))
			{
				goto IL_25A;
			}
		}
		else if (!(cardId == "LOE_018"))
		{
			goto IL_25A;
		}
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_BossTrogg_01, 2.5f);
		IL_25A:
		yield break;
	}

	// Token: 0x04002C7A RID: 11386
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_BossTrogg_01 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_BossTrogg_01.prefab:af8e1ec08a124fb46a20c492676a5db7");

	// Token: 0x04002C7B RID: 11387
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_BossTroggzor_01 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_BossTroggzor_01.prefab:6467fcc50a6d23149b52054aeb195789");

	// Token: 0x04002C7C RID: 11388
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_BossWeapon_01 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_BossWeapon_01.prefab:1b349a0654244d0488d4b240f74d9dec");

	// Token: 0x04002C7D RID: 11389
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_Death_01 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_Death_01.prefab:6f4de3aa5ce367d4387602fb9e89e90e");

	// Token: 0x04002C7E RID: 11390
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_DefeatPlayer_01.prefab:34a0ba2a1b7031045bdd2af2c555f49a");

	// Token: 0x04002C7F RID: 11391
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_EmoteResponse_01.prefab:763ed9d4ad281e1428b9f080bff02c2f");

	// Token: 0x04002C80 RID: 11392
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_HeroPower_02.prefab:d1bc8f03029419e46b6fd2716d944def");

	// Token: 0x04002C81 RID: 11393
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_HeroPower_03.prefab:861afd2c7a51a2a45b54993291594283");

	// Token: 0x04002C82 RID: 11394
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_HeroPower_04.prefab:f550a7e782a72fd4cbf532dad15f5eb8");

	// Token: 0x04002C83 RID: 11395
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_HeroPowerTrogg_01 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_HeroPowerTrogg_01.prefab:e374b8b270aa2fb44a6d2bb440954f69");

	// Token: 0x04002C84 RID: 11396
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_Idle_01 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_Idle_01.prefab:5e3957d6745c69e4b84dc6d4455676ee");

	// Token: 0x04002C85 RID: 11397
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_Idle_02 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_Idle_02.prefab:8dabb442472b4474a853c4403007d637");

	// Token: 0x04002C86 RID: 11398
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_Intro_01 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_Intro_01.prefab:25ddf98b329d38c47afe26585f4958b9");

	// Token: 0x04002C87 RID: 11399
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_PlayerRefreshmentVendor_01 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_PlayerRefreshmentVendor_01.prefab:05ac247388bc6344281ed563a9325ba6");

	// Token: 0x04002C88 RID: 11400
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_PlayerSpell_01 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_PlayerSpell_01.prefab:f6403839cf952e94fb7c4b58753150c1");

	// Token: 0x04002C89 RID: 11401
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_PlayerTrogg_01 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_PlayerTrogg_01.prefab:963adacdbb3000c468b551d7991da02b");

	// Token: 0x04002C8A RID: 11402
	private static readonly AssetReference VO_ULDA_BOSS_31h_Female_Trogg_PlayerUndercityHuckster_01 = new AssetReference("VO_ULDA_BOSS_31h_Female_Trogg_PlayerUndercityHuckster_01.prefab:912264c6ae3c2aa4e9a1e1f5a70c6213");

	// Token: 0x04002C8B RID: 11403
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_HeroPower_02,
		ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_HeroPower_03,
		ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_HeroPower_04
	};

	// Token: 0x04002C8C RID: 11404
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_Idle_01,
		ULDA_Dungeon_Boss_31h.VO_ULDA_BOSS_31h_Female_Trogg_Idle_02
	};

	// Token: 0x04002C8D RID: 11405
	private HashSet<string> m_playedLines = new HashSet<string>();
}
