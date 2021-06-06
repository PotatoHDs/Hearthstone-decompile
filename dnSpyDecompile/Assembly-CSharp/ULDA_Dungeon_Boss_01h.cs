using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200047E RID: 1150
public class ULDA_Dungeon_Boss_01h : ULDA_Dungeon
{
	// Token: 0x06003E48 RID: 15944 RVA: 0x0014A594 File Offset: 0x00148794
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_BossLackey_01,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_BossMarkedShot_01,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_BossPressurePlate_01,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_BossTriggerRapidFire_01,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_Death_01,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_DefeatPlayer_01,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_EmoteResponse_01,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_HeroPower_01,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_HeroPower_02,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_HeroPower_04,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_HeroPower_05,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_Idle_01,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_Idle_02,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_Idle_03,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_Intro_01,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_IntroFinley_01,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_PlayerBlunderbuss_01,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_PlayerGatlingWand_01,
			ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_PlayerGoblinBomb_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003E49 RID: 15945 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003E4A RID: 15946 RVA: 0x0014A728 File Offset: 0x00148928
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003E4B RID: 15947 RVA: 0x0014A730 File Offset: 0x00148930
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003E4C RID: 15948 RVA: 0x0014A738 File Offset: 0x00148938
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_EmoteResponse_01;
	}

	// Token: 0x06003E4D RID: 15949 RVA: 0x0014A770 File Offset: 0x00148970
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_IntroFinley_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003E4E RID: 15950 RVA: 0x0014A84A File Offset: 0x00148A4A
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003E4F RID: 15951 RVA: 0x0014A860 File Offset: 0x00148A60
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
		if (!(cardId == "ULDA_401"))
		{
			if (!(cardId == "ULDA_207"))
			{
				if (cardId == "BOT_031")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_PlayerGoblinBomb_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_PlayerGatlingWand_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_PlayerBlunderbuss_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003E50 RID: 15952 RVA: 0x0014A876 File Offset: 0x00148A76
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
		if (num <= 3598724010U)
		{
			if (num <= 2820981954U)
			{
				if (num != 2787426716U)
				{
					if (num != 2820981954U)
					{
						goto IL_31B;
					}
					if (!(cardId == "DAL_373"))
					{
						goto IL_31B;
					}
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_BossTriggerRapidFire_01, 2.5f);
					goto IL_31B;
				}
				else
				{
					if (!(cardId == "DAL_371"))
					{
						goto IL_31B;
					}
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_BossMarkedShot_01, 2.5f);
					goto IL_31B;
				}
			}
			else if (num != 3514835915U)
			{
				if (num != 3598724010U)
				{
					goto IL_31B;
				}
				if (!(cardId == "DAL_614"))
				{
					goto IL_31B;
				}
			}
			else if (!(cardId == "DAL_613"))
			{
				goto IL_31B;
			}
		}
		else if (num <= 3656421891U)
		{
			if (num != 3615501629U)
			{
				if (num != 3656421891U)
				{
					goto IL_31B;
				}
				if (!(cardId == "ULD_152"))
				{
					goto IL_31B;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_BossPressurePlate_01, 2.5f);
				goto IL_31B;
			}
			else if (!(cardId == "DAL_615"))
			{
				goto IL_31B;
			}
		}
		else if (num != 3785628939U)
		{
			if (num != 3786761772U)
			{
				if (num != 3995164034U)
				{
					goto IL_31B;
				}
				if (!(cardId == "ULD_616"))
				{
					goto IL_31B;
				}
			}
			else if (!(cardId == "DAL_739"))
			{
				goto IL_31B;
			}
		}
		else if (!(cardId == "DAL_741"))
		{
			goto IL_31B;
		}
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_BossLackey_01, 2.5f);
		IL_31B:
		yield break;
	}

	// Token: 0x04002A8C RID: 10892
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_BossLackey_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_BossLackey_01.prefab:210378a566be5d64cb7e1cb9831be00b");

	// Token: 0x04002A8D RID: 10893
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_BossMarkedShot_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_BossMarkedShot_01.prefab:53d7f7f6fd7148148b170560acee7859");

	// Token: 0x04002A8E RID: 10894
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_BossPressurePlate_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_BossPressurePlate_01.prefab:8247465a3f19e8c40a60cceec85dd836");

	// Token: 0x04002A8F RID: 10895
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_BossTriggerRapidFire_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_BossTriggerRapidFire_01.prefab:b090f8036c62d81428590821605aefc9");

	// Token: 0x04002A90 RID: 10896
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_Death_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_Death_01.prefab:0667acc72b6264c41a5378f7dae7a25e");

	// Token: 0x04002A91 RID: 10897
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_DefeatPlayer_01.prefab:6e605d1820c14124cbf0ad98f9b6ad93");

	// Token: 0x04002A92 RID: 10898
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_EmoteResponse_01.prefab:004b2084937f4d24b93c0e86a2ed985e");

	// Token: 0x04002A93 RID: 10899
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_HeroPower_01.prefab:cf017339da5e28248b7b5405a15fde10");

	// Token: 0x04002A94 RID: 10900
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_HeroPower_02.prefab:d9469ab5e488c85409d301facb1a0f3f");

	// Token: 0x04002A95 RID: 10901
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_HeroPower_04.prefab:52e3323c9a5fb3e42b7461a5332265b8");

	// Token: 0x04002A96 RID: 10902
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_HeroPower_05.prefab:cabd559ba2df7ff41af8f0bbe3033bc6");

	// Token: 0x04002A97 RID: 10903
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_Idle_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_Idle_01.prefab:055e09a2b533b62469c79d12c7553766");

	// Token: 0x04002A98 RID: 10904
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_Idle_02 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_Idle_02.prefab:aca575db3a6bdd041a0e7864e9d07cd9");

	// Token: 0x04002A99 RID: 10905
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_Idle_03 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_Idle_03.prefab:dfcd00f746d57644482e276d355450a9");

	// Token: 0x04002A9A RID: 10906
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_Intro_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_Intro_01.prefab:0ceccd4cc4c695f499d9afe387bb2094");

	// Token: 0x04002A9B RID: 10907
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_IntroFinley_01.prefab:4179dc8ffc9623446a904c6854f75d4b");

	// Token: 0x04002A9C RID: 10908
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_PlayerBlunderbuss_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_PlayerBlunderbuss_01.prefab:86979e14b68e54f46b3a712b6f429af9");

	// Token: 0x04002A9D RID: 10909
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_PlayerGatlingWand_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_PlayerGatlingWand_01.prefab:321bcdb0194dbf142ab39539f8500276");

	// Token: 0x04002A9E RID: 10910
	private static readonly AssetReference VO_ULDA_BOSS_01h_Male_Human_PlayerGoblinBomb_01 = new AssetReference("VO_ULDA_BOSS_01h_Male_Human_PlayerGoblinBomb_01.prefab:4af7a4406d320e4418a87bcd21f6618e");

	// Token: 0x04002A9F RID: 10911
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_HeroPower_01,
		ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_HeroPower_02,
		ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_HeroPower_04,
		ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_HeroPower_05
	};

	// Token: 0x04002AA0 RID: 10912
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_Idle_01,
		ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_Idle_02,
		ULDA_Dungeon_Boss_01h.VO_ULDA_BOSS_01h_Male_Human_Idle_03
	};

	// Token: 0x04002AA1 RID: 10913
	private HashSet<string> m_playedLines = new HashSet<string>();
}
