using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000469 RID: 1129
public class DALA_Dungeon_Boss_60h : DALA_Dungeon
{
	// Token: 0x06003D36 RID: 15670 RVA: 0x00140FA0 File Offset: 0x0013F1A0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Death_03,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_DefeatPlayer_02,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_EmoteResponse_01,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_HeroPower_01,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_HeroPower_02,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_HeroPower_03,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_HeroPower_04,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Idle_01,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Idle_02,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Idle_03,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Intro_01,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_03,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_04,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerPlaysFloatingHead_01,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerSavedByHead_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003D37 RID: 15671 RVA: 0x001410F4 File Offset: 0x0013F2F4
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Death_03;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_EmoteResponse_01;
	}

	// Token: 0x06003D38 RID: 15672 RVA: 0x0014112C File Offset: 0x0013F32C
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_HeroPower_01,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_HeroPower_02,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_HeroPower_03,
			DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_HeroPower_04
		};
	}

	// Token: 0x06003D39 RID: 15673 RVA: 0x0014117E File Offset: 0x0013F37E
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_60h.m_IdleLines;
	}

	// Token: 0x06003D3A RID: 15674 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003D3B RID: 15675 RVA: 0x00141185 File Offset: 0x0013F385
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 102)
		{
			if (missionEvent != 103)
			{
				if (missionEvent == 526)
				{
					yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerPlaysFloatingHead_01, 2.5f);
				}
				else
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
			}
			else
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_60h.m_CopySpell);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerSavedByHead_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003D3C RID: 15676 RVA: 0x0014119B File Offset: 0x0013F39B
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
		yield break;
	}

	// Token: 0x06003D3D RID: 15677 RVA: 0x001411B1 File Offset: 0x0013F3B1
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
		yield break;
	}

	// Token: 0x040027E4 RID: 10212
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Death_03 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Death_03.prefab:91c97f2113caf5d4da4e60ac34e39ba6");

	// Token: 0x040027E5 RID: 10213
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_DefeatPlayer_02.prefab:adc29f00e12bfaf47a7339fd3487e8a9");

	// Token: 0x040027E6 RID: 10214
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_EmoteResponse_01.prefab:08225fdd9ba844e45abf869181733f72");

	// Token: 0x040027E7 RID: 10215
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_HeroPower_01.prefab:b72cce13135e55e4598b54aef801c22b");

	// Token: 0x040027E8 RID: 10216
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_HeroPower_02.prefab:5be333e58b79bb74e8750858b88d7cd0");

	// Token: 0x040027E9 RID: 10217
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_HeroPower_03.prefab:49715743f3d78634ca06950b6cf5fb7a");

	// Token: 0x040027EA RID: 10218
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_HeroPower_04.prefab:ae04e3a849f20264092fce92e76b5061");

	// Token: 0x040027EB RID: 10219
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Idle_01.prefab:e9602fe7e5572a7479b648c0451599d5");

	// Token: 0x040027EC RID: 10220
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Idle_02.prefab:6ddb5d5dbc5d8fb4c82b267384fde9fe");

	// Token: 0x040027ED RID: 10221
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Idle_03.prefab:b6331b5a121661d43a81162595c68d7d");

	// Token: 0x040027EE RID: 10222
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Intro_01.prefab:055c4ee8194fc5d43b42b59bd84468ad");

	// Token: 0x040027EF RID: 10223
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_03 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_03.prefab:12efa1dd4e9b5e24dab0a09e61655b5b");

	// Token: 0x040027F0 RID: 10224
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_04 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_04.prefab:982ae5988e6e0f948a1981e443456882");

	// Token: 0x040027F1 RID: 10225
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerPlaysFloatingHead_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerPlaysFloatingHead_01.prefab:ad61843dd935ca44dadb5e98fc15e823");

	// Token: 0x040027F2 RID: 10226
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerSavedByHead_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_Trigger_PlayerSavedByHead_01.prefab:47fa09d01ffcdb04dbf095a0365fb542");

	// Token: 0x040027F3 RID: 10227
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Idle_01,
		DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Idle_02,
		DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Idle_03
	};

	// Token: 0x040027F4 RID: 10228
	private static List<string> m_CopySpell = new List<string>
	{
		DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_03,
		DALA_Dungeon_Boss_60h.VO_DALA_BOSS_60h_Male_Human_Trigger_Copyspell_04
	};

	// Token: 0x040027F5 RID: 10229
	private HashSet<string> m_playedLines = new HashSet<string>();
}
