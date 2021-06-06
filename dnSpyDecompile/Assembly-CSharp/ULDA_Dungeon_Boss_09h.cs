using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000486 RID: 1158
public class ULDA_Dungeon_Boss_09h : ULDA_Dungeon
{
	// Token: 0x06003E9A RID: 16026 RVA: 0x0014C068 File Offset: 0x0014A268
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_BossDemolisher_01,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_BossHeavyMetal_01,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_BossOmegaAssembly_01,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_DeathALT_01,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_DefeatPlayer_01,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_EmoteResponse_01,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_HeroPower_01,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_HeroPower_02,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_HeroPowerLowArmor_01,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_Idle_01,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_Idle_02,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_Idle_03,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_Intro_01,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_PlayerDemolisher_01,
			ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_PlayerMechathun_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003E9B RID: 16027 RVA: 0x0014C1BC File Offset: 0x0014A3BC
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003E9C RID: 16028 RVA: 0x0014C1C4 File Offset: 0x0014A3C4
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_EmoteResponse_01;
	}

	// Token: 0x06003E9D RID: 16029 RVA: 0x0014C1FC File Offset: 0x0014A3FC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003E9E RID: 16030 RVA: 0x0014C285 File Offset: 0x0014A485
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
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_HeroPowerLowArmor_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003E9F RID: 16031 RVA: 0x0014C29B File Offset: 0x0014A49B
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
		if (!(cardId == "EX1_102"))
		{
			if (cardId == "BOT_424")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_PlayerMechathun_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_PlayerDemolisher_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003EA0 RID: 16032 RVA: 0x0014C2B1 File Offset: 0x0014A4B1
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
		if (!(cardId == "BOT_424"))
		{
			if (!(cardId == "TRL_324"))
			{
				if (cardId == "BOT_299")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_BossOmegaAssembly_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_BossHeavyMetal_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_BossDemolisher_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002B00 RID: 11008
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_BossDemolisher_01 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_BossDemolisher_01.prefab:6a769776cb71790418056207c6b1f1c9");

	// Token: 0x04002B01 RID: 11009
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_BossHeavyMetal_01 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_BossHeavyMetal_01.prefab:bd5485fe8a87db143b786b0a2c1c032f");

	// Token: 0x04002B02 RID: 11010
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_BossOmegaAssembly_01 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_BossOmegaAssembly_01.prefab:75e9ae1206632af47836d7831d0e9f9e");

	// Token: 0x04002B03 RID: 11011
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_DeathALT_01.prefab:f54f591ff1a95fc46a8d329026d77fe7");

	// Token: 0x04002B04 RID: 11012
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_DefeatPlayer_01.prefab:d4fc2b8a8441a1648a7cf6005fc17536");

	// Token: 0x04002B05 RID: 11013
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_EmoteResponse_01.prefab:50eaa1ca98962a545ae303c92d3ed40f");

	// Token: 0x04002B06 RID: 11014
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_HeroPower_01.prefab:4c8ae017981bac447a59b2029471eda8");

	// Token: 0x04002B07 RID: 11015
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_HeroPower_02.prefab:459259d4540cd934491731abbdfd7e06");

	// Token: 0x04002B08 RID: 11016
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_HeroPowerLowArmor_01 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_HeroPowerLowArmor_01.prefab:23c385895cb8d1642a1b518bc9ea1f19");

	// Token: 0x04002B09 RID: 11017
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_Idle_01 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_Idle_01.prefab:fdbb1c54c2da7df42a307d47b24c828a");

	// Token: 0x04002B0A RID: 11018
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_Idle_02 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_Idle_02.prefab:bc31cefc4fef68f4e844d5a1419ecbae");

	// Token: 0x04002B0B RID: 11019
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_Idle_03 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_Idle_03.prefab:694ecc4f130d2fc49a25a6bea4e81fdd");

	// Token: 0x04002B0C RID: 11020
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_Intro_01 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_Intro_01.prefab:ef27d916fda53be49aa4e791a2dbfd24");

	// Token: 0x04002B0D RID: 11021
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_PlayerDemolisher_01 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_PlayerDemolisher_01.prefab:2093352b5ed095f49b4b444373a15390");

	// Token: 0x04002B0E RID: 11022
	private static readonly AssetReference VO_ULDA_BOSS_09h_Male_Mech_PlayerMechathun_01 = new AssetReference("VO_ULDA_BOSS_09h_Male_Mech_PlayerMechathun_01.prefab:73dd4a80d04e6d944b5400de5cb91797");

	// Token: 0x04002B0F RID: 11023
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_HeroPower_01,
		ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_HeroPower_02
	};

	// Token: 0x04002B10 RID: 11024
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_Idle_01,
		ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_Idle_02,
		ULDA_Dungeon_Boss_09h.VO_ULDA_BOSS_09h_Male_Mech_Idle_03
	};

	// Token: 0x04002B11 RID: 11025
	private HashSet<string> m_playedLines = new HashSet<string>();
}
