using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004FD RID: 1277
public class BTA_Fight_19 : BTA_Dungeon_Heroic
{
	// Token: 0x060044BC RID: 17596 RVA: 0x0017446C File Offset: 0x0017266C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Attack_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_GrimRally_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Riftcleaver_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_SummonCube_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossDeath_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStart_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStartIllidan_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_02,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_03,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Emote_Response_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Kelidan_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Magtheridon_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_FelOrc_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Warden_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_02,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_03,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleA_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleB_01,
			BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_UI_Mission_Fight_19_CoinSelect_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060044BD RID: 17597 RVA: 0x00174620 File Offset: 0x00172820
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_19h_IdleLines;
	}

	// Token: 0x060044BE RID: 17598 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060044BF RID: 17599 RVA: 0x00174628 File Offset: 0x00172828
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossDeath_01;
		this.m_standardEmoteResponseLine = BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Emote_Response_01;
	}

	// Token: 0x060044C0 RID: 17600 RVA: 0x00174650 File Offset: 0x00172850
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "HERO_10" || cardId == "HERO_10a")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStartIllidan_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x060044C1 RID: 17601 RVA: 0x00174750 File Offset: 0x00172950
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 115)
		{
			if (missionEvent != 500)
			{
				if (missionEvent != 507)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_missionEventTrigger507Lines);
				}
			}
			else
			{
				base.PlaySound(BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Attack_01, 1f, true, false);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_missionEventTrigger115Lines);
		}
		yield break;
	}

	// Token: 0x060044C2 RID: 17602 RVA: 0x00174766 File Offset: 0x00172966
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 3217092201U)
		{
			if (num <= 1835110085U)
			{
				if (num != 682362337U)
				{
					if (num != 1175835834U)
					{
						if (num != 1835110085U)
						{
							goto IL_3FE;
						}
						if (!(cardId == "BT_726"))
						{
							goto IL_3FE;
						}
					}
					else if (!(cardId == "BT_262"))
					{
						goto IL_3FE;
					}
				}
				else if (!(cardId == "CFM_609"))
				{
					goto IL_3FE;
				}
			}
			else if (num <= 2170515370U)
			{
				if (num != 1989030184U)
				{
					if (num != 2170515370U)
					{
						goto IL_3FE;
					}
					if (!(cardId == "BT_730"))
					{
						goto IL_3FE;
					}
				}
				else if (!(cardId == "BT_123t"))
				{
					goto IL_3FE;
				}
			}
			else if (num != 3021815209U)
			{
				if (num != 3217092201U)
				{
					goto IL_3FE;
				}
				if (!(cardId == "BT_850"))
				{
					goto IL_3FE;
				}
				yield return base.PlayLineAlways(actor, BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Magtheridon_01, 2.5f);
				goto IL_3FE;
			}
			else if (!(cardId == "BT_256"))
			{
				goto IL_3FE;
			}
		}
		else if (num <= 3431360517U)
		{
			if (num <= 3296006732U)
			{
				if (num != 3229734899U)
				{
					if (num != 3296006732U)
					{
						goto IL_3FE;
					}
					if (!(cardId == "BT_196"))
					{
						goto IL_3FE;
					}
					yield return base.PlayLineAlways(actor, BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Kelidan_01, 2.5f);
					goto IL_3FE;
				}
				else if (!(cardId == "BT_140"))
				{
					goto IL_3FE;
				}
			}
			else if (num != 3381027660U)
			{
				if (num != 3431360517U)
				{
					goto IL_3FE;
				}
				if (!(cardId == "BT_120"))
				{
					goto IL_3FE;
				}
			}
			else if (!(cardId == "BT_123"))
			{
				goto IL_3FE;
			}
		}
		else if (num <= 4200209911U)
		{
			if (num != 3464768660U)
			{
				if (num != 4200209911U)
				{
					goto IL_3FE;
				}
				if (!(cardId == "BT_715"))
				{
					goto IL_3FE;
				}
			}
			else if (!(cardId == "BT_138"))
			{
				goto IL_3FE;
			}
		}
		else if (num != 4216987530U)
		{
			if (num != 4232783452U)
			{
				goto IL_3FE;
			}
			if (!(cardId == "DRG_063"))
			{
				goto IL_3FE;
			}
		}
		else if (!(cardId == "BT_716"))
		{
			goto IL_3FE;
		}
		yield return base.PlayLineOnlyOnce(actor, BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_FelOrc_01, 2.5f);
		IL_3FE:
		yield break;
	}

	// Token: 0x060044C3 RID: 17603 RVA: 0x0017477C File Offset: 0x0017297C
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "BT_737") && !(cardId == "BT_729"))
		{
			if (!(cardId == "EX1_301"))
			{
				if (cardId == "ULD_258")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_GrimRally_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Riftcleaver_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Warden_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060044C4 RID: 17604 RVA: 0x00174792 File Offset: 0x00172992
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x04003784 RID: 14212
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Attack_01.prefab:b8ab368d1c2fc7444bf66912c16822b8");

	// Token: 0x04003785 RID: 14213
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_GrimRally_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_GrimRally_01.prefab:f14339031c01bdd40bf7365431f0d8d3");

	// Token: 0x04003786 RID: 14214
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Riftcleaver_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Riftcleaver_01.prefab:bf4e0c7b747120c47ad156475523488f");

	// Token: 0x04003787 RID: 14215
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_SummonCube_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_SummonCube_01.prefab:d6aa49fc61b2cc84ab88bb9ba5dc7dad");

	// Token: 0x04003788 RID: 14216
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossDeath_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossDeath_01.prefab:0e692b6708edb9d4bbd6e03e05d97bc6");

	// Token: 0x04003789 RID: 14217
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStart_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStart_01.prefab:3dc62a0eec39fb64ab0ff3e1d24131bd");

	// Token: 0x0400378A RID: 14218
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStartIllidan_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStartIllidan_01.prefab:7a407e6d58c4c1d4ab1a84649280fc2e");

	// Token: 0x0400378B RID: 14219
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_01.prefab:08852d382e735da45af9527547c0b4e1");

	// Token: 0x0400378C RID: 14220
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_02 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_02.prefab:afaaeb3b05b9b1b4492b7204aca2dec4");

	// Token: 0x0400378D RID: 14221
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_03 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_03.prefab:4955e26177e712c47b6fedfbdbbec69f");

	// Token: 0x0400378E RID: 14222
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Emote_Response_01.prefab:2b1a1eae46be9db44b6e2a3bf00d241d");

	// Token: 0x0400378F RID: 14223
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Kelidan_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Kelidan_01.prefab:95210bc2fcc850b44bd3f8d297aa3b59");

	// Token: 0x04003790 RID: 14224
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Magtheridon_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Magtheridon_01.prefab:e640de0c77e7fae43b3c3ba5744d5feb");

	// Token: 0x04003791 RID: 14225
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_FelOrc_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_FelOrc_01.prefab:d6a7e55471dc27342a140237eadc2e99");

	// Token: 0x04003792 RID: 14226
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Warden_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Warden_01.prefab:9a10c9750f5c0284e9e4cbdbabdacffe");

	// Token: 0x04003793 RID: 14227
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_01.prefab:843d7f27a3f25c742b661b42aeaaaac3");

	// Token: 0x04003794 RID: 14228
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_02 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_02.prefab:50c5f7a0c973881439081382107d7753");

	// Token: 0x04003795 RID: 14229
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_03 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_03.prefab:520ed1631f9a375408a89e5ba105abd5");

	// Token: 0x04003796 RID: 14230
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleA_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleA_01.prefab:e9a60b5e9970d9f4096b374ca1ac8548");

	// Token: 0x04003797 RID: 14231
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleB_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleB_01.prefab:848ed1bbb818fae448f4d32bee656a7a");

	// Token: 0x04003798 RID: 14232
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_UI_Mission_Fight_19_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_UI_Mission_Fight_19_CoinSelect_01.prefab:0a14a62d2f5320844889bca7e653df89");

	// Token: 0x04003799 RID: 14233
	private List<string> m_missionEventTrigger115Lines = new List<string>
	{
		BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_SummonCube_01,
		BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_01,
		BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_02,
		BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_03
	};

	// Token: 0x0400379A RID: 14234
	private List<string> m_missionEventTrigger507Lines = new List<string>
	{
		BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_01,
		BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_02,
		BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_03
	};

	// Token: 0x0400379B RID: 14235
	private List<string> m_VO_BTA_BOSS_19h_IdleLines = new List<string>
	{
		BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleA_01,
		BTA_Fight_19.VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleB_01
	};

	// Token: 0x0400379C RID: 14236
	private HashSet<string> m_playedLines = new HashSet<string>();
}
