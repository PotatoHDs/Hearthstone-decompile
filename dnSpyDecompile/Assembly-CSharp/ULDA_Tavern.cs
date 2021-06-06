using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004CD RID: 1229
public class ULDA_Tavern : ULDA_Dungeon
{
	// Token: 0x060041C0 RID: 16832 RVA: 0x0015F7E0 File Offset: 0x0015D9E0
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

	// Token: 0x060041C1 RID: 16833 RVA: 0x0015F842 File Offset: 0x0015DA42
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x060041C2 RID: 16834 RVA: 0x0015F84C File Offset: 0x0015DA4C
	public ULDA_Tavern()
	{
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameplaySceneLoaded));
	}

	// Token: 0x060041C3 RID: 16835 RVA: 0x0015FD58 File Offset: 0x0015DF58
	~ULDA_Tavern()
	{
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameplaySceneLoaded));
		}
	}

	// Token: 0x060041C4 RID: 16836 RVA: 0x0015FD9C File Offset: 0x0015DF9C
	private void OnGameplaySceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode != SceneMgr.Mode.GAMEPLAY)
		{
			return;
		}
		SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameplaySceneLoaded));
		ManaCrystalMgr.Get().SetEnemyManaCounterActive(false);
	}

	// Token: 0x060041C5 RID: 16837 RVA: 0x0015FDC5 File Offset: 0x0015DFC5
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = ULDA_Tavern.VO_ULDA_900h_Male_Human_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTheChallenge_03;
	}

	// Token: 0x060041C6 RID: 16838 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	// Token: 0x060041C7 RID: 16839 RVA: 0x00146D3A File Offset: 0x00144F3A
	public override bool DoAlternateMulliganIntro()
	{
		new TavernMulliganIntro().Show(GameEntity.Coroutines);
		return true;
	}

	// Token: 0x060041C8 RID: 16840 RVA: 0x0015FDF0 File Offset: 0x0015DFF0
	public override void NotifyOfGameOver(TAG_PLAYSTATE playState)
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		Network.Get().DisconnectFromGameServer();
		SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
		GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
		SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x060041C9 RID: 16841 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060041CA RID: 16842 RVA: 0x0015FE35 File Offset: 0x0015E035
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

	// Token: 0x060041CB RID: 16843 RVA: 0x0015FE60 File Offset: 0x0015E060
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Tavern.VO_ULDA_900h_Male_Human_Death_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_Idle_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_Idle_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_Idle_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_Idle1_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_Idle2_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_Intro_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_Intro_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroBrann_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroDesert_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroElise_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroFinley_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroHallsofOrigination_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroLostCity_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroReno_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroTomb_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_Outro_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_Outro_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_Outro_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_Outro_04,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_OutroTomb_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerBrood_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerBrood_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerBrood_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerDismiss_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerDismiss_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerDismiss_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerGoodFood_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerGoodFood_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerGoodFood_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerKindle_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerKindle_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerKindle_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruit_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruit_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruit_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRightHandMan_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRightHandMan_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRightHandMan_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTakeAChance_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTakeAChance_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTakeAChance_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTallTales_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTallTales_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTallTales_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTellAStory_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTellAStory_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTellAStory_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTheChallenge_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTheChallenge_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTheChallenge_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerYoureAllFired_01,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerYoureAllFired_02,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerYoureAllFired_03,
			ULDA_Tavern.VO_ULDA_900h_Male_Human_RareTreasure_01,
			ULDA_Tavern.VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_01,
			ULDA_Tavern.VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_02,
			ULDA_Tavern.VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_03,
			ULDA_Tavern.VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_01,
			ULDA_Tavern.VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_02,
			ULDA_Tavern.VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_03,
			ULDA_Tavern.VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_01,
			ULDA_Tavern.VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_02,
			ULDA_Tavern.VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_03,
			ULDA_Tavern.VO_ULDA_Reno_Male_Human_Entering_The_Inn_01,
			ULDA_Tavern.VO_ULDA_Reno_Male_Human_Entering_The_Inn_02,
			ULDA_Tavern.VO_ULDA_Reno_Male_Human_Entering_The_Inn_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060041CC RID: 16844 RVA: 0x00160384 File Offset: 0x0015E584
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
	}

	// Token: 0x060041CD RID: 16845 RVA: 0x001603DD File Offset: 0x0015E5DD
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
							this.m_IntroLines.Add(ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroElise_01);
						}
					}
					else
					{
						yield return base.PlayRandomLineAlways(actor, this.m_IntroFinleyLines);
						this.m_IntroLines.Add(ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroFinley_01);
					}
				}
				else
				{
					yield return base.PlayRandomLineAlways(actor, this.m_IntroBrannLines);
					this.m_IntroLines.Add(ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroBrann_01);
				}
			}
			else
			{
				yield return base.PlayRandomLineAlways(actor, this.m_IntroRenoLines);
				this.m_IntroLines.Add(ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroReno_01);
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
			yield return base.PlayBossLine(enemyActor, ULDA_Tavern.VO_ULDA_900h_Male_Human_OutroTomb_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 110:
			this.m_IntroLines.Add(ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroLostCity_01);
			break;
		case 111:
			this.m_IntroLines.Add(ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroDesert_01);
			break;
		case 112:
			this.m_IntroLines.Add(ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroTomb_01);
			break;
		case 113:
			this.m_IntroLines.Add(ULDA_Tavern.VO_ULDA_900h_Male_Human_IntroHallsofOrigination_01);
			break;
		}
		yield break;
	}

	// Token: 0x060041CE RID: 16846 RVA: 0x001603F3 File Offset: 0x0015E5F3
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
						yield return base.PlayLineOnlyOnce(actor, ULDA_Tavern.VO_ULDA_900h_Male_Human_RareTreasure_01, 2.5f);
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

	// Token: 0x040030B9 RID: 12473
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Death_01 = new AssetReference("VO_ULDA_900h_Male_Human_Death_01.prefab:a618512d0ae93c6419d1b9f1720f0c7d");

	// Token: 0x040030BA RID: 12474
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle_01 = new AssetReference("VO_ULDA_900h_Male_Human_Idle_01.prefab:65498b246600ab84b9eb4e96184e45e1");

	// Token: 0x040030BB RID: 12475
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle_02 = new AssetReference("VO_ULDA_900h_Male_Human_Idle_02.prefab:48566fa9d5068774ca67f1ec23f06d29");

	// Token: 0x040030BC RID: 12476
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle_03 = new AssetReference("VO_ULDA_900h_Male_Human_Idle_03.prefab:a6ce884acf40eb74180e9e752fb41657");

	// Token: 0x040030BD RID: 12477
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle1_01 = new AssetReference("VO_ULDA_900h_Male_Human_Idle1_01.prefab:7f4788f127fa1684e982d622e4424fd1");

	// Token: 0x040030BE RID: 12478
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle2_01 = new AssetReference("VO_ULDA_900h_Male_Human_Idle2_01.prefab:ff21e137e4df36747a30929d6ab7d92b");

	// Token: 0x040030BF RID: 12479
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Intro_01 = new AssetReference("VO_ULDA_900h_Male_Human_Intro_01.prefab:9bf19e53c470da045bfe20d61dc0e585");

	// Token: 0x040030C0 RID: 12480
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Intro_02 = new AssetReference("VO_ULDA_900h_Male_Human_Intro_02.prefab:bdcfc890b9ad2be4396e1648c0397bcb");

	// Token: 0x040030C1 RID: 12481
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroBrann_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroBrann_01.prefab:3041ed0482e59554b8940d2a934fe9d0");

	// Token: 0x040030C2 RID: 12482
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroDesert_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroDesert_01.prefab:75caa9e4ea688d34188356b2499c12a3");

	// Token: 0x040030C3 RID: 12483
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroElise_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroElise_01.prefab:53ce4c280b8f5c444999fdcc49027d03");

	// Token: 0x040030C4 RID: 12484
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroFinley_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroFinley_01.prefab:31b5df6d6a9eb8b488fc0a0c5feb7188");

	// Token: 0x040030C5 RID: 12485
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroHallsofOrigination_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroHallsofOrigination_01.prefab:a45e068e096abd6409a0c197b3792ea8");

	// Token: 0x040030C6 RID: 12486
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroLostCity_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroLostCity_01.prefab:50fcf1a49c9555649a4c8db3e363cdf6");

	// Token: 0x040030C7 RID: 12487
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroReno_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroReno_01.prefab:a576c5b44d4278f4bb56938af4edd0d9");

	// Token: 0x040030C8 RID: 12488
	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroTomb_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroTomb_01.prefab:568a63b73074b0346afc8bb2aa240acf");

	// Token: 0x040030C9 RID: 12489
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Outro_01 = new AssetReference("VO_ULDA_900h_Male_Human_Outro_01.prefab:f90597594b5e1fa4fb4c398c9d371ad8");

	// Token: 0x040030CA RID: 12490
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Outro_02 = new AssetReference("VO_ULDA_900h_Male_Human_Outro_02.prefab:9c881a8dd90f185408b4e31ca9bc2e2c");

	// Token: 0x040030CB RID: 12491
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Outro_03 = new AssetReference("VO_ULDA_900h_Male_Human_Outro_03.prefab:88e4f84362decef4989053c6088f3fed");

	// Token: 0x040030CC RID: 12492
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Outro_04 = new AssetReference("VO_ULDA_900h_Male_Human_Outro_04.prefab:c3866464c40079d4b9d8763b0d5a5b7a");

	// Token: 0x040030CD RID: 12493
	private static readonly AssetReference VO_ULDA_900h_Male_Human_OutroTomb_01 = new AssetReference("VO_ULDA_900h_Male_Human_OutroTomb_01.prefab:5a7668fdb3254f4479a086e60bcf8651");

	// Token: 0x040030CE RID: 12494
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerBrood_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerBrood_01.prefab:16b2ac46ef0a3dd4c8fc196a097e9121");

	// Token: 0x040030CF RID: 12495
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerBrood_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerBrood_02.prefab:52986adc218b63549946a774a79e6c87");

	// Token: 0x040030D0 RID: 12496
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerBrood_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerBrood_03.prefab:12641e68851fb6a45921086eb72c5259");

	// Token: 0x040030D1 RID: 12497
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerDismiss_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerDismiss_01.prefab:e3c6117dc85931b488820418b5ebca46");

	// Token: 0x040030D2 RID: 12498
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerDismiss_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerDismiss_02.prefab:007a20f573821fe46abad2320f42588d");

	// Token: 0x040030D3 RID: 12499
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerDismiss_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerDismiss_03.prefab:47ec8429b2702de499444fef95972af2");

	// Token: 0x040030D4 RID: 12500
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerGoodFood_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerGoodFood_01.prefab:e469dcf6a9bf2ae4aa04a0315e8780b9");

	// Token: 0x040030D5 RID: 12501
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerGoodFood_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerGoodFood_02.prefab:ebe23f92c5ac3814f81c2cf3b7d97cf5");

	// Token: 0x040030D6 RID: 12502
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerGoodFood_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerGoodFood_03.prefab:a77a0845a6119174688bbad816c37dab");

	// Token: 0x040030D7 RID: 12503
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerKindle_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerKindle_01.prefab:1bad65c353a782249aebc03b0fdfd6a4");

	// Token: 0x040030D8 RID: 12504
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerKindle_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerKindle_02.prefab:a17c1f358216e274bb40d32a3080c298");

	// Token: 0x040030D9 RID: 12505
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerKindle_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerKindle_03.prefab:7f1ac96d6edebf14699b348f4ec447df");

	// Token: 0x040030DA RID: 12506
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruit_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruit_01.prefab:667dec4793e3fbb4ca6dee70ee293081");

	// Token: 0x040030DB RID: 12507
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruit_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruit_02.prefab:703cb4d6eabc74b478d5f322d5b07fc0");

	// Token: 0x040030DC RID: 12508
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruit_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruit_03.prefab:bd69258416bf3bd40a67de1b07fac6a1");

	// Token: 0x040030DD RID: 12509
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_01.prefab:ca3d6ea5ea7bcc645875af6997c3c99c");

	// Token: 0x040030DE RID: 12510
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_02.prefab:ec1c81a1e32aad3498d8b261cfc74e8f");

	// Token: 0x040030DF RID: 12511
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_03.prefab:eea6ed6b4c5c7304c81b89c6a7565304");

	// Token: 0x040030E0 RID: 12512
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRightHandMan_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRightHandMan_01.prefab:2313c06d0d5e09e44b0f7e60a08c9435");

	// Token: 0x040030E1 RID: 12513
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRightHandMan_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRightHandMan_02.prefab:39f1dcbe34147af4b9e2597a632ccb09");

	// Token: 0x040030E2 RID: 12514
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRightHandMan_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRightHandMan_03.prefab:07ee37e9bdd88a542a66570c6d1c8835");

	// Token: 0x040030E3 RID: 12515
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_01.prefab:cbc0b3b5edf633e479922267130ae75b");

	// Token: 0x040030E4 RID: 12516
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_02.prefab:7b080fb528b74b543b22482754490e6f");

	// Token: 0x040030E5 RID: 12517
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_03.prefab:50fa4cf8fd9e03e43a72b5786b392323");

	// Token: 0x040030E6 RID: 12518
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTakeAChance_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTakeAChance_01.prefab:45e8ed29067f702449397cd16d377b3c");

	// Token: 0x040030E7 RID: 12519
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTakeAChance_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTakeAChance_02.prefab:5002f55604bdd4849987f659ed624f49");

	// Token: 0x040030E8 RID: 12520
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTakeAChance_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTakeAChance_03.prefab:75d8f482869d1c94d97730e882d555c4");

	// Token: 0x040030E9 RID: 12521
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTallTales_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTallTales_01.prefab:91e8eeec3f5cba64d8d83560e237fd74");

	// Token: 0x040030EA RID: 12522
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTallTales_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTallTales_02.prefab:715791bca57fa894c85a5348cc09c037");

	// Token: 0x040030EB RID: 12523
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTallTales_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTallTales_03.prefab:f97e00a29a5ef4b43931c49725c0ab81");

	// Token: 0x040030EC RID: 12524
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTellAStory_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTellAStory_01.prefab:2cbce5768dcedda44bab0bbdc8af3b43");

	// Token: 0x040030ED RID: 12525
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTellAStory_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTellAStory_02.prefab:fc516e58206fe3241a8e22131f43c5af");

	// Token: 0x040030EE RID: 12526
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTellAStory_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTellAStory_03.prefab:da2016f741eca1647a9d2baf4223521d");

	// Token: 0x040030EF RID: 12527
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheChallenge_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheChallenge_01.prefab:5b4c8f675f9096d49a1ad23239339d33");

	// Token: 0x040030F0 RID: 12528
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheChallenge_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheChallenge_02.prefab:4518bcd42ea25f1488bfbc6711ee1379");

	// Token: 0x040030F1 RID: 12529
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheChallenge_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheChallenge_03.prefab:03a6ca9852b70ca4e82d99254776fd42");

	// Token: 0x040030F2 RID: 12530
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_01.prefab:44ec582a2eaa8984f84a78bd77921a56");

	// Token: 0x040030F3 RID: 12531
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_02.prefab:cc1d5dc0d55b8c24aab751c7857cb31d");

	// Token: 0x040030F4 RID: 12532
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_03.prefab:3f5016af315c8a844945d63d7a011677");

	// Token: 0x040030F5 RID: 12533
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerYoureAllFired_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerYoureAllFired_01.prefab:1571c80b735c54441a4ecf2df09f33ca");

	// Token: 0x040030F6 RID: 12534
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerYoureAllFired_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerYoureAllFired_02.prefab:248ad8c32e6b98244a31eaa6fdf448bc");

	// Token: 0x040030F7 RID: 12535
	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerYoureAllFired_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerYoureAllFired_03.prefab:4b810d42e0221574f9747a9a3eb24ffc");

	// Token: 0x040030F8 RID: 12536
	private static readonly AssetReference VO_ULDA_900h_Male_Human_RareTreasure_01 = new AssetReference("VO_ULDA_900h_Male_Human_RareTreasure_01.prefab:d9c549f0640fd344db572338ccfcdbd8");

	// Token: 0x040030F9 RID: 12537
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_01.prefab:540ac575d524e4745bfdc00458b1d088");

	// Token: 0x040030FA RID: 12538
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_02 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_02.prefab:99f3e65a7220c664b880ce0c876e3bcd");

	// Token: 0x040030FB RID: 12539
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_03 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_03.prefab:799e5530f557a3c4795b6dd013413459");

	// Token: 0x040030FC RID: 12540
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_01.prefab:1942472c07315c447b94ebcb589e8a26");

	// Token: 0x040030FD RID: 12541
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_02 = new AssetReference("VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_02.prefab:81aefc8eef05603408d8197329d25693");

	// Token: 0x040030FE RID: 12542
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_03 = new AssetReference("VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_03.prefab:a4c5d93c7ad167945b947544d56be8e9");

	// Token: 0x040030FF RID: 12543
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_01.prefab:6e3a6ea1c8e0e714aae1a97ce3eb164e");

	// Token: 0x04003100 RID: 12544
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_02 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_02.prefab:d1ca72a295335a14db0f86a3aa2db056");

	// Token: 0x04003101 RID: 12545
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_03 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_03.prefab:93b587275021b9341b704497105dea84");

	// Token: 0x04003102 RID: 12546
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Entering_The_Inn_01 = new AssetReference("VO_ULDA_Reno_Male_Human_Entering_The_Inn_01.prefab:881fafab42858244f8ff9c3c361247b2");

	// Token: 0x04003103 RID: 12547
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Entering_The_Inn_02 = new AssetReference("VO_ULDA_Reno_Male_Human_Entering_The_Inn_02.prefab:d9148ac445aa53b48ab7714f8263008d");

	// Token: 0x04003104 RID: 12548
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Entering_The_Inn_03 = new AssetReference("VO_ULDA_Reno_Male_Human_Entering_The_Inn_03.prefab:bfd07b9ad56d7c744a35d9c2cf0127df");

	// Token: 0x04003105 RID: 12549
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_Idle_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_Idle_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_Idle_03,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_Idle1_01
	};

	// Token: 0x04003106 RID: 12550
	private List<string> m_IntroLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_Intro_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_Intro_02
	};

	// Token: 0x04003107 RID: 12551
	private List<string> m_OutroLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_Outro_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_Outro_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_Outro_03,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_Outro_04
	};

	// Token: 0x04003108 RID: 12552
	private List<string> m_PlayerBroodLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerBrood_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerBrood_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerBrood_03
	};

	// Token: 0x04003109 RID: 12553
	private List<string> m_PlayerDismissLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerDismiss_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerDismiss_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerDismiss_03
	};

	// Token: 0x0400310A RID: 12554
	private List<string> m_PlayerGoodFoodLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerGoodFood_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerGoodFood_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerGoodFood_03
	};

	// Token: 0x0400310B RID: 12555
	private List<string> m_PlayerKindleLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerKindle_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerKindle_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerKindle_03
	};

	// Token: 0x0400310C RID: 12556
	private List<string> m_PlayerRecruitLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruit_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruit_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruit_03
	};

	// Token: 0x0400310D RID: 12557
	private List<string> m_PlayerRecruitAVeteranLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_03
	};

	// Token: 0x0400310E RID: 12558
	private List<string> m_PlayerRightHandManLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRightHandMan_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRightHandMan_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRightHandMan_03
	};

	// Token: 0x0400310F RID: 12559
	private List<string> m_PlayerRoundofDrinksLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_03
	};

	// Token: 0x04003110 RID: 12560
	private List<string> m_PlayerTakeAChanceLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTakeAChance_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTakeAChance_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTakeAChance_03
	};

	// Token: 0x04003111 RID: 12561
	private List<string> m_PlayerTallTalesLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTallTales_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTallTales_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTallTales_03
	};

	// Token: 0x04003112 RID: 12562
	private List<string> m_PlayerTellAStoryLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTellAStory_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTellAStory_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTellAStory_03
	};

	// Token: 0x04003113 RID: 12563
	private List<string> m_PlayerTheChallengeLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTheChallenge_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTheChallenge_02
	};

	// Token: 0x04003114 RID: 12564
	private List<string> m_PlayerTheGangsAllHereLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_03
	};

	// Token: 0x04003115 RID: 12565
	private List<string> m_PlayerYoureAllFiredLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerYoureAllFired_01,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerYoureAllFired_02,
		ULDA_Tavern.VO_ULDA_900h_Male_Human_PlayerYoureAllFired_03
	};

	// Token: 0x04003116 RID: 12566
	private List<string> m_IntroRenoLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_Reno_Male_Human_Entering_The_Inn_01,
		ULDA_Tavern.VO_ULDA_Reno_Male_Human_Entering_The_Inn_02,
		ULDA_Tavern.VO_ULDA_Reno_Male_Human_Entering_The_Inn_03
	};

	// Token: 0x04003117 RID: 12567
	private List<string> m_IntroBrannLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_01,
		ULDA_Tavern.VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_02,
		ULDA_Tavern.VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_03
	};

	// Token: 0x04003118 RID: 12568
	private List<string> m_IntroEliseLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_01,
		ULDA_Tavern.VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_02,
		ULDA_Tavern.VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_03
	};

	// Token: 0x04003119 RID: 12569
	private List<string> m_IntroFinleyLines = new List<string>
	{
		ULDA_Tavern.VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_01,
		ULDA_Tavern.VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_02,
		ULDA_Tavern.VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_03
	};

	// Token: 0x0400311A RID: 12570
	private HashSet<string> m_playedLines = new HashSet<string>();
}
