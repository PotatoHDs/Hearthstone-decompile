using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005BF RID: 1471
public class TB_RoadToNR_Tavern : ULDA_Dungeon
{
	// Token: 0x06005106 RID: 20742 RVA: 0x001AA0F4 File Offset: 0x001A82F4
	protected override void HandleMainReadyStep()
	{
		if (GameState.Get() == null)
		{
			Log.Gameplay.PrintError("ULDA_Tavern.HandleMainReadyStep(): GameState is null.", Array.Empty<object>());
			return;
		}
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		if (gameEntity == null)
		{
			Log.Gameplay.PrintError("ULDA_Tavern.HandleMainReadyStep(): GameEntity is null.", Array.Empty<object>());
			return;
		}
		if (gameEntity.GetTag(GAME_TAG.TURN) == 1)
		{
			GameState.Get().SetMulliganBusy(true);
		}
	}

	// Token: 0x06005107 RID: 20743 RVA: 0x001AA156 File Offset: 0x001A8356
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06005108 RID: 20744 RVA: 0x001AA15E File Offset: 0x001A835E
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Death_01;
		this.m_standardEmoteResponseLine = TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTheChallenge_03;
	}

	// Token: 0x06005109 RID: 20745 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	// Token: 0x0600510A RID: 20746 RVA: 0x00146D3A File Offset: 0x00144F3A
	public override bool DoAlternateMulliganIntro()
	{
		new TavernMulliganIntro().Show(GameEntity.Coroutines);
		return true;
	}

	// Token: 0x0600510B RID: 20747 RVA: 0x001AA188 File Offset: 0x001A8388
	public override void NotifyOfGameOver(TAG_PLAYSTATE playState)
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		Network.Get().DisconnectFromGameServer();
		SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
		GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
		SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x0600510C RID: 20748 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600510D RID: 20749 RVA: 0x001AA1CD File Offset: 0x001A83CD
	public override string[] GetOverrideBoardClickSounds()
	{
		return new string[]
		{
			"TavernBoard_WoodPoke_Creak_1.prefab:ab4c166deba73ef4c80d46dca53aaf14",
			"TavernBoard_WoodPoke_Creak_2.prefab:272821729af020e46acb68d4e3f29e8e",
			"TavernBoard_WoodPoke_Creak_3.prefab:80220d29df0ae4849be9a9029b33088a",
			"TavernBoard_WoodPoke_Creak_4.prefab:0a478645bd054864c9ac52804f3be118"
		};
	}

	// Token: 0x0600510E RID: 20750 RVA: 0x001AA1F8 File Offset: 0x001A83F8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Death_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Idle_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Idle_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Idle1_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Idle2_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Intro_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Intro_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroBrann_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroDesert_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroElise_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroFinley_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroHallsofOrigination_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroLostCity_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroReno_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroTomb_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Outro_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Outro_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Outro_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Outro_04,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_OutroTomb_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerBrood_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerBrood_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerBrood_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerDismiss_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerDismiss_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerDismiss_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerGoodFood_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerGoodFood_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerGoodFood_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerKindle_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerKindle_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerKindle_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruit_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruit_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruit_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRightHandMan_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRightHandMan_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRightHandMan_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTakeAChance_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTakeAChance_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTakeAChance_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTallTales_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTallTales_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTallTales_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTellAStory_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTellAStory_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTellAStory_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTheChallenge_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTheChallenge_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTheChallenge_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerYoureAllFired_01,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerYoureAllFired_02,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerYoureAllFired_03,
			TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_RareTreasure_01,
			TB_RoadToNR_Tavern.VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_01,
			TB_RoadToNR_Tavern.VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_02,
			TB_RoadToNR_Tavern.VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_03,
			TB_RoadToNR_Tavern.VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_01,
			TB_RoadToNR_Tavern.VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_02,
			TB_RoadToNR_Tavern.VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_03,
			TB_RoadToNR_Tavern.VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_01,
			TB_RoadToNR_Tavern.VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_02,
			TB_RoadToNR_Tavern.VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_03,
			TB_RoadToNR_Tavern.VO_ULDA_Reno_Male_Human_Entering_The_Inn_01,
			TB_RoadToNR_Tavern.VO_ULDA_Reno_Male_Human_Entering_The_Inn_02,
			TB_RoadToNR_Tavern.VO_ULDA_Reno_Male_Human_Entering_The_Inn_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600510F RID: 20751 RVA: 0x001AA70C File Offset: 0x001A890C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
	}

	// Token: 0x06005110 RID: 20752 RVA: 0x001AA765 File Offset: 0x001A8965
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			if (!(cardId == "ULDA_Reno"))
			{
				if (!(cardId == "ULDA_Brann"))
				{
					if (!(cardId == "ULDA_Finley"))
					{
						if (cardId == "ULDA_Elise")
						{
							yield return base.PlayRandomLineAlways(actor, this.m_IntroEliseLines);
							this.m_IntroLines.Add(TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroElise_01);
						}
					}
					else
					{
						yield return base.PlayRandomLineAlways(actor, this.m_IntroFinleyLines);
						this.m_IntroLines.Add(TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroFinley_01);
					}
				}
				else
				{
					yield return base.PlayRandomLineAlways(actor, this.m_IntroBrannLines);
					this.m_IntroLines.Add(TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroBrann_01);
				}
			}
			else
			{
				yield return base.PlayRandomLineAlways(actor, this.m_IntroRenoLines);
				this.m_IntroLines.Add(TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroReno_01);
			}
			yield return base.PlayRandomLineAlways(enemyActor, this.m_IntroLines);
			break;
		case 102:
			GameState.Get().SetBusy(true);
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_OutroLines);
			GameState.Get().SetBusy(false);
			break;
		case 103:
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(enemyActor, TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_OutroTomb_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 104:
		case 105:
		case 106:
		case 107:
		case 108:
		case 109:
			break;
		case 110:
			this.m_IntroLines.Add(TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroLostCity_01);
			break;
		case 111:
			this.m_IntroLines.Add(TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroDesert_01);
			break;
		case 112:
			this.m_IntroLines.Add(TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroTomb_01);
			break;
		case 113:
			this.m_IntroLines.Add(TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_IntroHallsofOrigination_01);
			break;
		default:
			if (missionEvent == 60313)
			{
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(3f);
				GameState.Get().SetBusy(false);
			}
			break;
		}
		yield break;
	}

	// Token: 0x06005111 RID: 20753 RVA: 0x001AA77B File Offset: 0x001A897B
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
		if (num <= 3371051007U)
		{
			if (num <= 593710542U)
			{
				if (num != 425934352U)
				{
					if (num != 509822447U)
					{
						if (num == 593710542U)
						{
							if (cardId == "ULDA_608")
							{
								yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerTheChallengeLines);
							}
						}
					}
					else if (cardId == "ULDA_607")
					{
						yield return base.PlayLineOnlyOnce(actor, TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_RareTreasure_01, 2.5f);
					}
				}
				else if (cardId == "ULDA_602")
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerTellAStoryLines);
				}
			}
			else if (num <= 3354273388U)
			{
				if (num != 3337642864U)
				{
					if (num == 3354273388U)
					{
						if (cardId == "DALA_910")
						{
							yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerTheGangsAllHereLines);
						}
					}
				}
				else if (cardId == "DALA_907")
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerRecruitAVeteranLines);
				}
			}
			else if (num != 3354420483U)
			{
				if (num == 3371051007U)
				{
					if (cardId == "DALA_911")
					{
						yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerKindleLines);
					}
				}
			}
			else if (cardId == "DALA_906")
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerRoundofDrinksLines);
			}
		}
		else if (num <= 3404606245U)
		{
			if (num <= 3387828626U)
			{
				if (num != 3371198102U)
				{
					if (num == 3387828626U)
					{
						if (cardId == "DALA_912")
						{
							yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerBroodLines);
						}
					}
				}
				else if (cardId == "DALA_905")
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerRightHandManLines);
				}
			}
			else if (num != 3387975721U)
			{
				if (num == 3404606245U)
				{
					if (cardId == "DALA_913")
					{
						yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerTallTalesLines);
					}
				}
			}
			else if (cardId == "DALA_904")
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerGoodFoodLines);
			}
		}
		else if (num <= 3421530959U)
		{
			if (num != 3404753340U)
			{
				if (num == 3421530959U)
				{
					if (cardId == "DALA_902")
					{
						yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerDismissLines);
					}
				}
			}
			else if (cardId == "DALA_903")
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerTakeAChanceLines);
			}
		}
		else if (num != 3438308578U)
		{
			if (num == 3572529530U)
			{
				if (cardId == "DALA_909")
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerYoureAllFiredLines);
				}
			}
		}
		else if (cardId == "DALA_901")
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerRecruitLines);
		}
		yield break;
	}

	// Token: 0x04004815 RID: 18453
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Death_01 = new AssetReference("VO_ULDA_900h_Male_Human_Death_01.prefab:a618512d0ae93c6419d1b9f1720f0c7d");

	// Token: 0x04004816 RID: 18454
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle_01 = new AssetReference("VO_ULDA_900h_Male_Human_Idle_01.prefab:65498b246600ab84b9eb4e96184e45e1");

	// Token: 0x04004817 RID: 18455
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle_02 = new AssetReference("VO_ULDA_900h_Male_Human_Idle_02.prefab:48566fa9d5068774ca67f1ec23f06d29");

	// Token: 0x04004818 RID: 18456
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle_03 = new AssetReference("VO_ULDA_900h_Male_Human_Idle_03.prefab:a6ce884acf40eb74180e9e752fb41657");

	// Token: 0x04004819 RID: 18457
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle1_01 = new AssetReference("VO_ULDA_900h_Male_Human_Idle1_01.prefab:7f4788f127fa1684e982d622e4424fd1");

	// Token: 0x0400481A RID: 18458
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle2_01 = new AssetReference("VO_ULDA_900h_Male_Human_Idle2_01.prefab:ff21e137e4df36747a30929d6ab7d92b");

	// Token: 0x0400481B RID: 18459
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Intro_01 = new AssetReference("VO_ULDA_900h_Male_Human_Intro_01.prefab:9bf19e53c470da045bfe20d61dc0e585");

	// Token: 0x0400481C RID: 18460
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Intro_02 = new AssetReference("VO_ULDA_900h_Male_Human_Intro_02.prefab:bdcfc890b9ad2be4396e1648c0397bcb");

	// Token: 0x0400481D RID: 18461
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroBrann_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroBrann_01.prefab:3041ed0482e59554b8940d2a934fe9d0");

	// Token: 0x0400481E RID: 18462
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroDesert_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroDesert_01.prefab:75caa9e4ea688d34188356b2499c12a3");

	// Token: 0x0400481F RID: 18463
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroElise_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroElise_01.prefab:53ce4c280b8f5c444999fdcc49027d03");

	// Token: 0x04004820 RID: 18464
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroFinley_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroFinley_01.prefab:31b5df6d6a9eb8b488fc0a0c5feb7188");

	// Token: 0x04004821 RID: 18465
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroHallsofOrigination_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroHallsofOrigination_01.prefab:a45e068e096abd6409a0c197b3792ea8");

	// Token: 0x04004822 RID: 18466
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroLostCity_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroLostCity_01.prefab:50fcf1a49c9555649a4c8db3e363cdf6");

	// Token: 0x04004823 RID: 18467
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroReno_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroReno_01.prefab:a576c5b44d4278f4bb56938af4edd0d9");

	// Token: 0x04004824 RID: 18468
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroTomb_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroTomb_01.prefab:568a63b73074b0346afc8bb2aa240acf");

	// Token: 0x04004825 RID: 18469
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Outro_01 = new AssetReference("VO_ULDA_900h_Male_Human_Outro_01.prefab:f90597594b5e1fa4fb4c398c9d371ad8");

	// Token: 0x04004826 RID: 18470
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Outro_02 = new AssetReference("VO_ULDA_900h_Male_Human_Outro_02.prefab:9c881a8dd90f185408b4e31ca9bc2e2c");

	// Token: 0x04004827 RID: 18471
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Outro_03 = new AssetReference("VO_ULDA_900h_Male_Human_Outro_03.prefab:88e4f84362decef4989053c6088f3fed");

	// Token: 0x04004828 RID: 18472
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Outro_04 = new AssetReference("VO_ULDA_900h_Male_Human_Outro_04.prefab:c3866464c40079d4b9d8763b0d5a5b7a");

	// Token: 0x04004829 RID: 18473
	private static readonly AssetReference VO_ULDA_900h_Male_Human_OutroTomb_01 = new AssetReference("VO_ULDA_900h_Male_Human_OutroTomb_01.prefab:5a7668fdb3254f4479a086e60bcf8651");

	// Token: 0x0400482A RID: 18474
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerBrood_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerBrood_01.prefab:16b2ac46ef0a3dd4c8fc196a097e9121");

	// Token: 0x0400482B RID: 18475
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerBrood_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerBrood_02.prefab:52986adc218b63549946a774a79e6c87");

	// Token: 0x0400482C RID: 18476
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerBrood_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerBrood_03.prefab:12641e68851fb6a45921086eb72c5259");

	// Token: 0x0400482D RID: 18477
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerDismiss_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerDismiss_01.prefab:e3c6117dc85931b488820418b5ebca46");

	// Token: 0x0400482E RID: 18478
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerDismiss_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerDismiss_02.prefab:007a20f573821fe46abad2320f42588d");

	// Token: 0x0400482F RID: 18479
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerDismiss_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerDismiss_03.prefab:47ec8429b2702de499444fef95972af2");

	// Token: 0x04004830 RID: 18480
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerGoodFood_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerGoodFood_01.prefab:e469dcf6a9bf2ae4aa04a0315e8780b9");

	// Token: 0x04004831 RID: 18481
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerGoodFood_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerGoodFood_02.prefab:ebe23f92c5ac3814f81c2cf3b7d97cf5");

	// Token: 0x04004832 RID: 18482
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerGoodFood_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerGoodFood_03.prefab:a77a0845a6119174688bbad816c37dab");

	// Token: 0x04004833 RID: 18483
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerKindle_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerKindle_01.prefab:1bad65c353a782249aebc03b0fdfd6a4");

	// Token: 0x04004834 RID: 18484
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerKindle_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerKindle_02.prefab:a17c1f358216e274bb40d32a3080c298");

	// Token: 0x04004835 RID: 18485
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerKindle_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerKindle_03.prefab:7f1ac96d6edebf14699b348f4ec447df");

	// Token: 0x04004836 RID: 18486
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruit_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruit_01.prefab:667dec4793e3fbb4ca6dee70ee293081");

	// Token: 0x04004837 RID: 18487
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruit_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruit_02.prefab:703cb4d6eabc74b478d5f322d5b07fc0");

	// Token: 0x04004838 RID: 18488
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruit_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruit_03.prefab:bd69258416bf3bd40a67de1b07fac6a1");

	// Token: 0x04004839 RID: 18489
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_01.prefab:ca3d6ea5ea7bcc645875af6997c3c99c");

	// Token: 0x0400483A RID: 18490
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_02.prefab:ec1c81a1e32aad3498d8b261cfc74e8f");

	// Token: 0x0400483B RID: 18491
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_03.prefab:eea6ed6b4c5c7304c81b89c6a7565304");

	// Token: 0x0400483C RID: 18492
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRightHandMan_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRightHandMan_01.prefab:2313c06d0d5e09e44b0f7e60a08c9435");

	// Token: 0x0400483D RID: 18493
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRightHandMan_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRightHandMan_02.prefab:39f1dcbe34147af4b9e2597a632ccb09");

	// Token: 0x0400483E RID: 18494
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRightHandMan_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRightHandMan_03.prefab:07ee37e9bdd88a542a66570c6d1c8835");

	// Token: 0x0400483F RID: 18495
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_01.prefab:cbc0b3b5edf633e479922267130ae75b");

	// Token: 0x04004840 RID: 18496
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_02.prefab:7b080fb528b74b543b22482754490e6f");

	// Token: 0x04004841 RID: 18497
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_03.prefab:50fa4cf8fd9e03e43a72b5786b392323");

	// Token: 0x04004842 RID: 18498
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTakeAChance_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTakeAChance_01.prefab:45e8ed29067f702449397cd16d377b3c");

	// Token: 0x04004843 RID: 18499
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTakeAChance_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTakeAChance_02.prefab:5002f55604bdd4849987f659ed624f49");

	// Token: 0x04004844 RID: 18500
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTakeAChance_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTakeAChance_03.prefab:75d8f482869d1c94d97730e882d555c4");

	// Token: 0x04004845 RID: 18501
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTallTales_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTallTales_01.prefab:91e8eeec3f5cba64d8d83560e237fd74");

	// Token: 0x04004846 RID: 18502
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTallTales_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTallTales_02.prefab:715791bca57fa894c85a5348cc09c037");

	// Token: 0x04004847 RID: 18503
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTallTales_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTallTales_03.prefab:f97e00a29a5ef4b43931c49725c0ab81");

	// Token: 0x04004848 RID: 18504
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTellAStory_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTellAStory_01.prefab:2cbce5768dcedda44bab0bbdc8af3b43");

	// Token: 0x04004849 RID: 18505
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTellAStory_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTellAStory_02.prefab:fc516e58206fe3241a8e22131f43c5af");

	// Token: 0x0400484A RID: 18506
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTellAStory_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTellAStory_03.prefab:da2016f741eca1647a9d2baf4223521d");

	// Token: 0x0400484B RID: 18507
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheChallenge_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheChallenge_01.prefab:5b4c8f675f9096d49a1ad23239339d33");

	// Token: 0x0400484C RID: 18508
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheChallenge_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheChallenge_02.prefab:4518bcd42ea25f1488bfbc6711ee1379");

	// Token: 0x0400484D RID: 18509
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheChallenge_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheChallenge_03.prefab:03a6ca9852b70ca4e82d99254776fd42");

	// Token: 0x0400484E RID: 18510
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_01.prefab:44ec582a2eaa8984f84a78bd77921a56");

	// Token: 0x0400484F RID: 18511
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_02.prefab:cc1d5dc0d55b8c24aab751c7857cb31d");

	// Token: 0x04004850 RID: 18512
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_03.prefab:3f5016af315c8a844945d63d7a011677");

	// Token: 0x04004851 RID: 18513
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerYoureAllFired_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerYoureAllFired_01.prefab:1571c80b735c54441a4ecf2df09f33ca");

	// Token: 0x04004852 RID: 18514
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerYoureAllFired_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerYoureAllFired_02.prefab:248ad8c32e6b98244a31eaa6fdf448bc");

	// Token: 0x04004853 RID: 18515
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerYoureAllFired_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerYoureAllFired_03.prefab:4b810d42e0221574f9747a9a3eb24ffc");

	// Token: 0x04004854 RID: 18516
	private static readonly AssetReference VO_ULDA_900h_Male_Human_RareTreasure_01 = new AssetReference("VO_ULDA_900h_Male_Human_RareTreasure_01.prefab:d9c549f0640fd344db572338ccfcdbd8");

	// Token: 0x04004855 RID: 18517
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_01.prefab:540ac575d524e4745bfdc00458b1d088");

	// Token: 0x04004856 RID: 18518
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_02 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_02.prefab:99f3e65a7220c664b880ce0c876e3bcd");

	// Token: 0x04004857 RID: 18519
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_03 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_03.prefab:799e5530f557a3c4795b6dd013413459");

	// Token: 0x04004858 RID: 18520
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_01.prefab:1942472c07315c447b94ebcb589e8a26");

	// Token: 0x04004859 RID: 18521
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_02 = new AssetReference("VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_02.prefab:81aefc8eef05603408d8197329d25693");

	// Token: 0x0400485A RID: 18522
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_03 = new AssetReference("VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_03.prefab:a4c5d93c7ad167945b947544d56be8e9");

	// Token: 0x0400485B RID: 18523
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_01.prefab:6e3a6ea1c8e0e714aae1a97ce3eb164e");

	// Token: 0x0400485C RID: 18524
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_02 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_02.prefab:d1ca72a295335a14db0f86a3aa2db056");

	// Token: 0x0400485D RID: 18525
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_03 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_03.prefab:93b587275021b9341b704497105dea84");

	// Token: 0x0400485E RID: 18526
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Entering_The_Inn_01 = new AssetReference("VO_ULDA_Reno_Male_Human_Entering_The_Inn_01.prefab:881fafab42858244f8ff9c3c361247b2");

	// Token: 0x0400485F RID: 18527
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Entering_The_Inn_02 = new AssetReference("VO_ULDA_Reno_Male_Human_Entering_The_Inn_02.prefab:d9148ac445aa53b48ab7714f8263008d");

	// Token: 0x04004860 RID: 18528
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Entering_The_Inn_03 = new AssetReference("VO_ULDA_Reno_Male_Human_Entering_The_Inn_03.prefab:bfd07b9ad56d7c744a35d9c2cf0127df");

	// Token: 0x04004861 RID: 18529
	private List<string> m_IdleLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Idle_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Idle_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Idle_03,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Idle1_01
	};

	// Token: 0x04004862 RID: 18530
	private List<string> m_IntroLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Intro_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Intro_02
	};

	// Token: 0x04004863 RID: 18531
	private List<string> m_OutroLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Outro_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Outro_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Outro_03,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_Outro_04
	};

	// Token: 0x04004864 RID: 18532
	private List<string> m_PlayerBroodLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerBrood_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerBrood_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerBrood_03
	};

	// Token: 0x04004865 RID: 18533
	private List<string> m_PlayerDismissLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerDismiss_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerDismiss_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerDismiss_03
	};

	// Token: 0x04004866 RID: 18534
	private List<string> m_PlayerGoodFoodLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerGoodFood_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerGoodFood_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerGoodFood_03
	};

	// Token: 0x04004867 RID: 18535
	private List<string> m_PlayerKindleLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerKindle_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerKindle_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerKindle_03
	};

	// Token: 0x04004868 RID: 18536
	private List<string> m_PlayerRecruitLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruit_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruit_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruit_03
	};

	// Token: 0x04004869 RID: 18537
	private List<string> m_PlayerRecruitAVeteranLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_03
	};

	// Token: 0x0400486A RID: 18538
	private List<string> m_PlayerRightHandManLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRightHandMan_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRightHandMan_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRightHandMan_03
	};

	// Token: 0x0400486B RID: 18539
	private List<string> m_PlayerRoundofDrinksLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_03
	};

	// Token: 0x0400486C RID: 18540
	private List<string> m_PlayerTakeAChanceLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTakeAChance_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTakeAChance_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTakeAChance_03
	};

	// Token: 0x0400486D RID: 18541
	private List<string> m_PlayerTallTalesLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTallTales_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTallTales_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTallTales_03
	};

	// Token: 0x0400486E RID: 18542
	private List<string> m_PlayerTellAStoryLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTellAStory_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTellAStory_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTellAStory_03
	};

	// Token: 0x0400486F RID: 18543
	private List<string> m_PlayerTheChallengeLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTheChallenge_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTheChallenge_02
	};

	// Token: 0x04004870 RID: 18544
	private List<string> m_PlayerTheGangsAllHereLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_03
	};

	// Token: 0x04004871 RID: 18545
	private List<string> m_PlayerYoureAllFiredLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerYoureAllFired_01,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerYoureAllFired_02,
		TB_RoadToNR_Tavern.VO_ULDA_900h_Male_Human_PlayerYoureAllFired_03
	};

	// Token: 0x04004872 RID: 18546
	private List<string> m_IntroRenoLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_Reno_Male_Human_Entering_The_Inn_01,
		TB_RoadToNR_Tavern.VO_ULDA_Reno_Male_Human_Entering_The_Inn_02,
		TB_RoadToNR_Tavern.VO_ULDA_Reno_Male_Human_Entering_The_Inn_03
	};

	// Token: 0x04004873 RID: 18547
	private List<string> m_IntroBrannLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_01,
		TB_RoadToNR_Tavern.VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_02,
		TB_RoadToNR_Tavern.VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_03
	};

	// Token: 0x04004874 RID: 18548
	private List<string> m_IntroEliseLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_01,
		TB_RoadToNR_Tavern.VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_02,
		TB_RoadToNR_Tavern.VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_03
	};

	// Token: 0x04004875 RID: 18549
	private List<string> m_IntroFinleyLines = new List<string>
	{
		TB_RoadToNR_Tavern.VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_01,
		TB_RoadToNR_Tavern.VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_02,
		TB_RoadToNR_Tavern.VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_03
	};

	// Token: 0x04004876 RID: 18550
	private HashSet<string> m_playedLines = new HashSet<string>();
}
