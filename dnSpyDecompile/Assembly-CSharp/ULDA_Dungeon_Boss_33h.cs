using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200049E RID: 1182
public class ULDA_Dungeon_Boss_33h : ULDA_Dungeon
{
	// Token: 0x06003FBA RID: 16314 RVA: 0x00151E10 File Offset: 0x00150010
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_BossFirstMech_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_BossViciousScraphound_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_DeathALT_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_DefeatPlayer_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_EmoteResponse_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_02,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_04,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_05,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerBossMinionOnly_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerMinionDestroy_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerNoMinions_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerPlayerMinionOnly_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_Idle_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_Idle_02,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_Idle_03,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_Intro_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_PlayerEMPOperative_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_PlayerMaskedContender_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_PlayerWrenchcalibur_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_Take20Damage_01,
			ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_TurnOne_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003FBB RID: 16315 RVA: 0x00151FD4 File Offset: 0x001501D4
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003FBC RID: 16316 RVA: 0x00151FDC File Offset: 0x001501DC
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003FBD RID: 16317 RVA: 0x00151FE4 File Offset: 0x001501E4
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_EmoteResponse_01;
	}

	// Token: 0x06003FBE RID: 16318 RVA: 0x0015201C File Offset: 0x0015021C
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

	// Token: 0x06003FBF RID: 16319 RVA: 0x001520A5 File Offset: 0x001502A5
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
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_TurnOne_01, 2.5f);
			goto IL_1EC;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_Take20Damage_01, 2.5f);
			goto IL_1EC;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerNoMinions_01, 2.5f);
			goto IL_1EC;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerBossMinionOnly_01, 2.5f);
			goto IL_1EC;
		case 106:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerMinionDestroy_01, 2.5f);
			goto IL_1EC;
		case 107:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_BossFirstMech_01, 2.5f);
			goto IL_1EC;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_1EC:
		yield break;
	}

	// Token: 0x06003FC0 RID: 16320 RVA: 0x001520BB File Offset: 0x001502BB
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
		if (!(cardId == "DAL_759"))
		{
			if (!(cardId == "BOT_540"))
			{
				if (!(cardId == "TRL_530"))
				{
					if (cardId == "DAL_063")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_PlayerWrenchcalibur_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_PlayerMaskedContender_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_PlayerEMPOperative_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_BossViciousScraphound_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003FC1 RID: 16321 RVA: 0x001520D1 File Offset: 0x001502D1
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

	// Token: 0x04002CA4 RID: 11428
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_BossFirstMech_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_BossFirstMech_01.prefab:d80348395d0e80b43aa5f903a5799da3");

	// Token: 0x04002CA5 RID: 11429
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_BossViciousScraphound_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_BossViciousScraphound_01.prefab:8d8609718015d494381e46298983b3a6");

	// Token: 0x04002CA6 RID: 11430
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_DeathALT_01.prefab:d0b713b2a683e20478e95c4ee16bdefa");

	// Token: 0x04002CA7 RID: 11431
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_DefeatPlayer_01.prefab:0a87f9bad19ce11498b1448d09728d66");

	// Token: 0x04002CA8 RID: 11432
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_EmoteResponse_01.prefab:83b97b0f553b2674db5eeb9ed31c362e");

	// Token: 0x04002CA9 RID: 11433
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_01.prefab:2a82517c083674a4598cb74993fae711");

	// Token: 0x04002CAA RID: 11434
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_02.prefab:8a94adad8acbb2946b89854f0c3371e6");

	// Token: 0x04002CAB RID: 11435
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_04.prefab:1672fa110802b5147b99b79eff7138b0");

	// Token: 0x04002CAC RID: 11436
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_05.prefab:b0a730614d4fb4547953b170506da852");

	// Token: 0x04002CAD RID: 11437
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerBossMinionOnly_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerBossMinionOnly_01.prefab:93ffda6280c8417469cd4bb7e8c5bb6b");

	// Token: 0x04002CAE RID: 11438
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerMinionDestroy_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerMinionDestroy_01.prefab:451f2bfc23c1c6c4fbf238c6831b8e9b");

	// Token: 0x04002CAF RID: 11439
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerNoMinions_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerNoMinions_01.prefab:8219b43b0018d1541bc4c1f17cd13edd");

	// Token: 0x04002CB0 RID: 11440
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerPlayerMinionOnly_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerPlayerMinionOnly_01.prefab:49dad37437303474894d12d650afbab7");

	// Token: 0x04002CB1 RID: 11441
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_Idle_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_Idle_01.prefab:e27b9ccdd2c730b4b9d5403b7c0354ef");

	// Token: 0x04002CB2 RID: 11442
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_Idle_02 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_Idle_02.prefab:860ed7510ef3e0842bcc36ceaf510c14");

	// Token: 0x04002CB3 RID: 11443
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_Idle_03 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_Idle_03.prefab:c47782b1377a5374dadd5320fe1692f8");

	// Token: 0x04002CB4 RID: 11444
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_Intro_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_Intro_01.prefab:f54369690a0d210459bc50e46add0ef8");

	// Token: 0x04002CB5 RID: 11445
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_PlayerEMPOperative_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_PlayerEMPOperative_01.prefab:cd33f4afb106db7449f8ad336f2853ee");

	// Token: 0x04002CB6 RID: 11446
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_PlayerMaskedContender_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_PlayerMaskedContender_01.prefab:9a9495d4097787a4aadb02901e0d2180");

	// Token: 0x04002CB7 RID: 11447
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_PlayerWrenchcalibur_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_PlayerWrenchcalibur_01.prefab:56abaf82459f4214f925f6542ffd78b9");

	// Token: 0x04002CB8 RID: 11448
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_Take20Damage_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_Take20Damage_01.prefab:89d75002cc1c305438390f205c279647");

	// Token: 0x04002CB9 RID: 11449
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_TurnOne_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_TurnOne_01.prefab:6d40927df7682d541826f6bec4944ba9");

	// Token: 0x04002CBA RID: 11450
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_01,
		ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_02,
		ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_04,
		ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_05
	};

	// Token: 0x04002CBB RID: 11451
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_Idle_01,
		ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_Idle_02,
		ULDA_Dungeon_Boss_33h.VO_ULDA_BOSS_33h_Male_Gnome_Idle_03
	};

	// Token: 0x04002CBC RID: 11452
	private HashSet<string> m_playedLines = new HashSet<string>();
}
