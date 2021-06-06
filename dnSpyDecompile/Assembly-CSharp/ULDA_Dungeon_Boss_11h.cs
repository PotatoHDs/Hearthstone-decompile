using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000488 RID: 1160
public class ULDA_Dungeon_Boss_11h : ULDA_Dungeon
{
	// Token: 0x06003EB3 RID: 16051 RVA: 0x0014C980 File Offset: 0x0014AB80
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_BossFireball_01,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_BossFlamestrike_01,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_BossTombWarden_01,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_DeathALT_01,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_DefeatPlayer_01,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_EmoteResponse_01,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_HeroPower_01,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_HeroPower_03,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_HeroPower_04,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_HeroPower_05,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_Idle1_01,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_Idle2_02,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_Idle3_03,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_Intro_01,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_IntroBrannResponse_01,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_PlayerDrBoom_01,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_PlayerKobold_01,
			ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_PlayerLargeMech_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003EB4 RID: 16052 RVA: 0x0014CB04 File Offset: 0x0014AD04
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_EmoteResponse_01;
	}

	// Token: 0x06003EB5 RID: 16053 RVA: 0x0014CB3C File Offset: 0x0014AD3C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_IntroBrannResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003EB6 RID: 16054 RVA: 0x0014CC16 File Offset: 0x0014AE16
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
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_PlayerLargeMech_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
		}
		yield break;
	}

	// Token: 0x06003EB7 RID: 16055 RVA: 0x0014CC2C File Offset: 0x0014AE2C
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
		if (num <= 1510711040U)
		{
			if (num <= 963594556U)
			{
				if (num <= 778032481U)
				{
					if (num != 216571437U)
					{
						if (num != 778032481U)
						{
							goto IL_3DE;
						}
						if (!(cardId == "DAL_417"))
						{
							goto IL_3DE;
						}
						goto IL_3B2;
					}
					else
					{
						if (!(cardId == "LOOT_014"))
						{
							goto IL_3DE;
						}
						goto IL_3B2;
					}
				}
				else if (num != 888934466U)
				{
					if (num != 963594556U)
					{
						goto IL_3DE;
					}
					if (!(cardId == "GVG_110"))
					{
						goto IL_3DE;
					}
				}
				else
				{
					if (!(cardId == "OG_082"))
					{
						goto IL_3DE;
					}
					goto IL_3B2;
				}
			}
			else if (num <= 1110709447U)
			{
				if (num != 1084795990U)
				{
					if (num != 1110709447U)
					{
						goto IL_3DE;
					}
					if (!(cardId == "LOOT_412"))
					{
						goto IL_3DE;
					}
					goto IL_3B2;
				}
				else
				{
					if (!(cardId == "LOOT_367"))
					{
						goto IL_3DE;
					}
					goto IL_3B2;
				}
			}
			else if (num != 1340872041U)
			{
				if (num != 1510711040U)
				{
					goto IL_3DE;
				}
				if (!(cardId == "ULD_184"))
				{
					goto IL_3DE;
				}
				goto IL_3B2;
			}
			else
			{
				if (!(cardId == "CS2_142"))
				{
					goto IL_3DE;
				}
				goto IL_3B2;
			}
		}
		else if (num <= 3333291126U)
		{
			if (num <= 2230621192U)
			{
				if (num != 2079328431U)
				{
					if (num != 2230621192U)
					{
						goto IL_3DE;
					}
					if (!(cardId == "LOOT_062"))
					{
						goto IL_3DE;
					}
					goto IL_3B2;
				}
				else
				{
					if (!(cardId == "LOOT_041"))
					{
						goto IL_3DE;
					}
					goto IL_3B2;
				}
			}
			else if (num != 2747171160U)
			{
				if (num != 3333291126U)
				{
					goto IL_3DE;
				}
				if (!(cardId == "LOOT_389"))
				{
					goto IL_3DE;
				}
				goto IL_3B2;
			}
			else
			{
				if (!(cardId == "LOOT_541"))
				{
					goto IL_3DE;
				}
				goto IL_3B2;
			}
		}
		else if (num <= 3500375768U)
		{
			if (num != 3450734459U)
			{
				if (num != 3500375768U)
				{
					goto IL_3DE;
				}
				if (!(cardId == "LOOT_347"))
				{
					goto IL_3DE;
				}
				goto IL_3B2;
			}
			else
			{
				if (!(cardId == "LOOT_382"))
				{
					goto IL_3DE;
				}
				goto IL_3B2;
			}
		}
		else if (num != 3598724010U)
		{
			if (num != 4196772219U)
			{
				goto IL_3DE;
			}
			if (!(cardId == "DAL_064"))
			{
				goto IL_3DE;
			}
		}
		else
		{
			if (!(cardId == "DAL_614"))
			{
				goto IL_3DE;
			}
			goto IL_3B2;
		}
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_PlayerDrBoom_01, 2.5f);
		goto IL_3DE;
		IL_3B2:
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_PlayerKobold_01, 2.5f);
		IL_3DE:
		yield break;
	}

	// Token: 0x06003EB8 RID: 16056 RVA: 0x0014CC42 File Offset: 0x0014AE42
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
		if (!(cardId == "CS2_029"))
		{
			if (!(cardId == "CS2_032"))
			{
				if (cardId == "ULD_253")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_BossTombWarden_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_BossFlamestrike_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_BossFireball_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002B29 RID: 11049
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_BossFireball_01 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_BossFireball_01.prefab:a9bc1a89d1342b54ba636a4af3a9054c");

	// Token: 0x04002B2A RID: 11050
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_BossFlamestrike_01 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_BossFlamestrike_01.prefab:2a1233559d309f54aad7a8898dde6fbc");

	// Token: 0x04002B2B RID: 11051
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_BossTombWarden_01 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_BossTombWarden_01.prefab:543cfb02873b4ba45969a03ed8ba3c3d");

	// Token: 0x04002B2C RID: 11052
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_DeathALT_01.prefab:bde450fd9b831a34ca4fd72a06065cce");

	// Token: 0x04002B2D RID: 11053
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_DefeatPlayer_01.prefab:1acbd439713bab34fb277f38a5801177");

	// Token: 0x04002B2E RID: 11054
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_EmoteResponse_01.prefab:a8fe00c1179535449b9bacd63b7b719b");

	// Token: 0x04002B2F RID: 11055
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_HeroPower_01.prefab:3cd1c82fe46697148b129bd79ac6495d");

	// Token: 0x04002B30 RID: 11056
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_HeroPower_03.prefab:65f6fe0474443b745b1294f3523f1d61");

	// Token: 0x04002B31 RID: 11057
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_HeroPower_04.prefab:1ad76cd877be8d946872948a18629beb");

	// Token: 0x04002B32 RID: 11058
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_HeroPower_05.prefab:f25e4acdd8969e0428b0f53eefa1879f");

	// Token: 0x04002B33 RID: 11059
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_Idle1_01 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_Idle1_01.prefab:fa133439491238c4fb4c5570d04fa478");

	// Token: 0x04002B34 RID: 11060
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_Idle2_02 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_Idle2_02.prefab:c530a76ea1469364ba6d6328043e0a5d");

	// Token: 0x04002B35 RID: 11061
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_Idle3_03 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_Idle3_03.prefab:9e8f2f16970ef204392072d59cb25106");

	// Token: 0x04002B36 RID: 11062
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_Intro_01 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_Intro_01.prefab:0d2a41dde5a7b294a81b0fc7bc414262");

	// Token: 0x04002B37 RID: 11063
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_IntroBrannResponse_01 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_IntroBrannResponse_01.prefab:ee8c6a117004c4442b094d077f935ebb");

	// Token: 0x04002B38 RID: 11064
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_PlayerDrBoom_01 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_PlayerDrBoom_01.prefab:aedf5be25ff0400479fe964900e019a1");

	// Token: 0x04002B39 RID: 11065
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_PlayerKobold_01 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_PlayerKobold_01.prefab:554a9d5bae3df3d498a35f4bf55484af");

	// Token: 0x04002B3A RID: 11066
	private static readonly AssetReference VO_ULDA_BOSS_11h_Male_Mech_PlayerLargeMech_01 = new AssetReference("VO_ULDA_BOSS_11h_Male_Mech_PlayerLargeMech_01.prefab:894d5af10a9b49843ac4483642f69462");

	// Token: 0x04002B3B RID: 11067
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_HeroPower_01,
		ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_HeroPower_03,
		ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_HeroPower_04,
		ULDA_Dungeon_Boss_11h.VO_ULDA_BOSS_11h_Male_Mech_HeroPower_05
	};

	// Token: 0x04002B3C RID: 11068
	private HashSet<string> m_playedLines = new HashSet<string>();
}
