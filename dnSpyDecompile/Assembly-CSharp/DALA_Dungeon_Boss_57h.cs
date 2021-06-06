using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000466 RID: 1126
public class DALA_Dungeon_Boss_57h : DALA_Dungeon
{
	// Token: 0x06003D10 RID: 15632 RVA: 0x0013F634 File Offset: 0x0013D834
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_02,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Death_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_DefeatPlayer_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_EmoteResponse_02,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_02,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_03,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPower_02,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPower_03,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPower_04,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPower_05,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPower_06,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPower_07,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_02,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_03,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Idle_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Idle_02,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Idle_04,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Idle_05,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Intro_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_IntroChu_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_IntroEudora_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_IntroVessina_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Misc_02,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Misc_03,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Misc_04,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerBad_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerBagOfCoins_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerBurgle_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_02,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_03,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinFirst_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinIntoNothing_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerFlyBy_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_02,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_03,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerPickPocket_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerTradePrince_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_01,
			DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003D11 RID: 15633 RVA: 0x0013F948 File Offset: 0x0013DB48
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_EmoteResponse_02;
	}

	// Token: 0x06003D12 RID: 15634 RVA: 0x0013F980 File Offset: 0x0013DB80
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_57h.m_IdleLines;
	}

	// Token: 0x06003D13 RID: 15635 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003D14 RID: 15636 RVA: 0x0013F988 File Offset: 0x0013DB88
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_IntroChu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Vessina")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_IntroVessina_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003D15 RID: 15637 RVA: 0x0013FAAE File Offset: 0x0013DCAE
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_02, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_03, 2.5f);
			break;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_57h.m_TurnStart);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerBad_01, 2.5f);
			break;
		case 106:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerBagOfCoins_01, 2.5f);
			break;
		case 107:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinFirst_01, 2.5f);
			break;
		case 108:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_57h.m_BossTreasure);
			break;
		case 109:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_57h.m_HeroPower);
			break;
		case 110:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_57h.m_HeroPowerFull);
			break;
		case 111:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinIntoNothing_01, 2.5f);
			break;
		case 112:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_57h.m_BossHeroPowerPlayerTreasure);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003D16 RID: 15638 RVA: 0x0013FAC4 File Offset: 0x0013DCC4
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
		if (!(cardId == "GVG_028t"))
		{
			if (!(cardId == "GAME_005"))
			{
				if (!(cardId == "AT_033"))
				{
					if (!(cardId == "DALA_716"))
					{
						if (!(cardId == "GIL_696"))
						{
							if (cardId == "GVG_028")
							{
								yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerTradePrince_01, 2.5f);
							}
						}
						else
						{
							yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerPickPocket_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerFlyBy_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerBurgle_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_57h.m_PlayerCoin);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_57h.m_PlayerGallywixCoin);
		}
		yield break;
	}

	// Token: 0x06003D17 RID: 15639 RVA: 0x0013FADA File Offset: 0x0013DCDA
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

	// Token: 0x0400275B RID: 10075
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_01.prefab:ced64f2c110cb67458879002cab82f29");

	// Token: 0x0400275C RID: 10076
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_02.prefab:bd0e7563bb4f242479d263ccdcd0a1bd");

	// Token: 0x0400275D RID: 10077
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Death_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Death_01.prefab:3d06a2f7602ace246b80d3ee714c85aa");

	// Token: 0x0400275E RID: 10078
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_DefeatPlayer_01.prefab:f52bec174e5c1634f9e985a503dada62");

	// Token: 0x0400275F RID: 10079
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_EmoteResponse_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_EmoteResponse_02.prefab:950c06f75131ddb41a5c6c37ed341fe6");

	// Token: 0x04002760 RID: 10080
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_01.prefab:43f575d764ffe6e48ae9a659ba97edcb");

	// Token: 0x04002761 RID: 10081
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_02.prefab:52a2046aa711f7c4ba1b2e2bbde6a7ca");

	// Token: 0x04002762 RID: 10082
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_03 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_03.prefab:6ad216df39a34f0449b0024dcb65828d");

	// Token: 0x04002763 RID: 10083
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPower_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPower_02.prefab:2da790703ec1d794bb2de1cd945e7c90");

	// Token: 0x04002764 RID: 10084
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPower_03 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPower_03.prefab:af328878f7ca4e64fbff865cbfac9f8b");

	// Token: 0x04002765 RID: 10085
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPower_04 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPower_04.prefab:c0e8d64f03b88384a9a632b6b5f2c774");

	// Token: 0x04002766 RID: 10086
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPower_05 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPower_05.prefab:4953e4b0b676ee1499607a8da172a09d");

	// Token: 0x04002767 RID: 10087
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPower_06 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPower_06.prefab:16e03d9ec5545534c94df0c785758b34");

	// Token: 0x04002768 RID: 10088
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPower_07 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPower_07.prefab:dfcf804334ac8124db554949a9e5fdb9");

	// Token: 0x04002769 RID: 10089
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_01.prefab:b40dfd799252546419423a095596fda3");

	// Token: 0x0400276A RID: 10090
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_02.prefab:88fbad2e65dbcf3469033620ec7615c6");

	// Token: 0x0400276B RID: 10091
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_03 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_03.prefab:eb4452cbcb5872145bb308d9253f7635");

	// Token: 0x0400276C RID: 10092
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Idle_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Idle_01.prefab:5c80ced9bd43b2f4da8241676f883e1d");

	// Token: 0x0400276D RID: 10093
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Idle_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Idle_02.prefab:c221b3e6f8dcd9b45a4bd6d659253bf1");

	// Token: 0x0400276E RID: 10094
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Idle_04 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Idle_04.prefab:2a7995b044d4b034c9bdb6f6f6ca058e");

	// Token: 0x0400276F RID: 10095
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Idle_05 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Idle_05.prefab:71ee0407c84ffef4785aaea1d2be9c9d");

	// Token: 0x04002770 RID: 10096
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Intro_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Intro_01.prefab:a7d3855778023c24bacc5a87042b59c1");

	// Token: 0x04002771 RID: 10097
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_IntroChu_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_IntroChu_01.prefab:ca36347bf274b10419017d5941c61734");

	// Token: 0x04002772 RID: 10098
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_IntroEudora_01.prefab:ecfa97d881ff8ca40be5b18bc6c75e21");

	// Token: 0x04002773 RID: 10099
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_IntroVessina_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_IntroVessina_01.prefab:1e5fd968703990f43a270002a14dd038");

	// Token: 0x04002774 RID: 10100
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Misc_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Misc_02.prefab:2f5b3c750b527c94aa93edbc468b6647");

	// Token: 0x04002775 RID: 10101
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Misc_03 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Misc_03.prefab:8110c73b9f92d2244bc9937ac18badad");

	// Token: 0x04002776 RID: 10102
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Misc_04 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Misc_04.prefab:52b0f7d192ef6fe43a5fbbdd51e18ab7");

	// Token: 0x04002777 RID: 10103
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerBad_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerBad_01.prefab:be8a39fbb8e59394d8fec39444a44c85");

	// Token: 0x04002778 RID: 10104
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerBagOfCoins_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerBagOfCoins_01.prefab:890661ed9d9b69e4d955e1ff2830f022");

	// Token: 0x04002779 RID: 10105
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerBurgle_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerBurgle_01.prefab:11142355acb01b642a3d2805ac8f20c6");

	// Token: 0x0400277A RID: 10106
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_02.prefab:b703c2f718c7599489b7ee4eae340832");

	// Token: 0x0400277B RID: 10107
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_03 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_03.prefab:744b469db5ddb2841b4daf1988cf4ebb");

	// Token: 0x0400277C RID: 10108
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinFirst_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinFirst_01.prefab:ce694a72971033f4dbf419d87ed46f0d");

	// Token: 0x0400277D RID: 10109
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinIntoNothing_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinIntoNothing_01.prefab:a7bea1563689a7e42928e2446c735f6b");

	// Token: 0x0400277E RID: 10110
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerFlyBy_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerFlyBy_01.prefab:48f6ac39b4697264cbcdd8587704752d");

	// Token: 0x0400277F RID: 10111
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_01.prefab:32b7750008044614f8e1acf82d896dcd");

	// Token: 0x04002780 RID: 10112
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_02.prefab:4fabbdba6e96ec84cbe84fd8a53eafa8");

	// Token: 0x04002781 RID: 10113
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_03 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_03.prefab:d769bb53029cae849a8f186a88fd1fe3");

	// Token: 0x04002782 RID: 10114
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerPickPocket_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerPickPocket_01.prefab:804d4d9ab14b64945917af6615990466");

	// Token: 0x04002783 RID: 10115
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerTradePrince_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerTradePrince_01.prefab:60450c82f88c3494fbd1d65744f71ca2");

	// Token: 0x04002784 RID: 10116
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_01.prefab:a39ac99ecf7147040a72e283b3946d31");

	// Token: 0x04002785 RID: 10117
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_02.prefab:697f599064e2c5d4dbd8f0cf652e5844");

	// Token: 0x04002786 RID: 10118
	private static List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPower_02,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPower_03,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPower_04,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPower_05,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPower_06,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPower_07
	};

	// Token: 0x04002787 RID: 10119
	private static List<string> m_HeroPowerFull = new List<string>
	{
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_01,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_02,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_03
	};

	// Token: 0x04002788 RID: 10120
	private static List<string> m_BossTreasure = new List<string>
	{
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_01,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_02
	};

	// Token: 0x04002789 RID: 10121
	private static List<string> m_TurnStart = new List<string>
	{
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Misc_02,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Misc_03,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Misc_04
	};

	// Token: 0x0400278A RID: 10122
	private static List<string> m_PlayerGallywixCoin = new List<string>
	{
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_01,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_02,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_03
	};

	// Token: 0x0400278B RID: 10123
	private static List<string> m_BossHeroPowerPlayerTreasure = new List<string>
	{
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_01,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_02
	};

	// Token: 0x0400278C RID: 10124
	private static List<string> m_PlayerCoin = new List<string>
	{
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_02,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_03
	};

	// Token: 0x0400278D RID: 10125
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Idle_01,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Idle_02,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Idle_04,
		DALA_Dungeon_Boss_57h.VO_DALA_BOSS_57h_Male_Goblin_Idle_05
	};

	// Token: 0x0400278E RID: 10126
	private HashSet<string> m_playedLines = new HashSet<string>();
}
