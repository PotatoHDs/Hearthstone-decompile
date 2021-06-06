using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200047B RID: 1147
public class DALA_Tavern : DALA_Dungeon
{
	// Token: 0x06003E0E RID: 15886 RVA: 0x00146B84 File Offset: 0x00144D84
	public DALA_Tavern()
	{
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameplaySceneLoaded));
	}

	// Token: 0x06003E0F RID: 15887 RVA: 0x00146C68 File Offset: 0x00144E68
	~DALA_Tavern()
	{
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameplaySceneLoaded));
		}
	}

	// Token: 0x06003E10 RID: 15888 RVA: 0x00146CAC File Offset: 0x00144EAC
	private void OnGameplaySceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode != SceneMgr.Mode.GAMEPLAY)
		{
			return;
		}
		SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameplaySceneLoaded));
		ManaCrystalMgr.Get().SetEnemyManaCounterActive(false);
	}

	// Token: 0x06003E11 RID: 15889 RVA: 0x00146CD8 File Offset: 0x00144ED8
	protected override void HandleMainReadyStep()
	{
		if (GameState.Get() == null)
		{
			Log.Gameplay.PrintError("DALA_Tavern.HandleMainReadyStep(): GameState is null.", Array.Empty<object>());
			return;
		}
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		if (gameEntity == null)
		{
			Log.Gameplay.PrintError("DALA_Tavern.HandleMainReadyStep(): GameEntity is null.", Array.Empty<object>());
			return;
		}
		if (gameEntity.GetTag(GAME_TAG.TURN) == 1)
		{
			GameState.Get().SetMulliganBusy(true);
		}
	}

	// Token: 0x06003E12 RID: 15890 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	// Token: 0x06003E13 RID: 15891 RVA: 0x00146D3A File Offset: 0x00144F3A
	public override bool DoAlternateMulliganIntro()
	{
		new TavernMulliganIntro().Show(GameEntity.Coroutines);
		return true;
	}

	// Token: 0x06003E14 RID: 15892 RVA: 0x00146D4C File Offset: 0x00144F4C
	public override void NotifyOfGameOver(TAG_PLAYSTATE playState)
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		Network.Get().DisconnectFromGameServer();
		SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
		GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
		SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x06003E15 RID: 15893 RVA: 0x00146D94 File Offset: 0x00144F94
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Tavern.VO_DALA_900h_Male_Human_Idle_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_Idle_02,
			DALA_Tavern.VO_DALA_900h_Male_Human_Idle_03,
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_02,
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_03,
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_04,
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_05,
			DALA_Tavern.VO_DALA_900h_Male_Human_IntroChu_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_IntroEudora_02,
			DALA_Tavern.VO_DALA_900h_Male_Human_IntroGeorge_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_IntroKriziki_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_IntroOlBarkeye_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_IntroRakanishu_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_IntroSqueamlish_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_IntroTekahn_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_IntroVessina_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_Outro_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_Outro_02,
			DALA_Tavern.VO_DALA_900h_Male_Human_Outro_03,
			DALA_Tavern.VO_DALA_900h_Male_Human_Outro_04,
			DALA_Tavern.VO_DALA_900h_Male_Human_Outro_05,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerBrood_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerBrood_02,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerDismiss_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerGoodFood_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerKindle_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerRecruit_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerRecruitAVeteran_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerRightHandMan_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerRoundOfDrinks_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerTakeAChance_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerTallTales_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerTellAStory_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerTheGangsAllHere_01,
			DALA_Tavern.VO_DALA_900h_Male_Human_PlayerYoureAllFired_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_Death_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_Idle_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_Idle_02,
			DALA_Tavern.VO_DALA_901h_Male_Mech_Idle_03,
			DALA_Tavern.VO_DALA_901h_Male_Mech_Intro_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_Intro_02,
			DALA_Tavern.VO_DALA_901h_Male_Mech_Intro_03,
			DALA_Tavern.VO_DALA_901h_Male_Mech_Intro_04,
			DALA_Tavern.VO_DALA_901h_Male_Mech_Intro_05,
			DALA_Tavern.VO_DALA_901h_Male_Mech_Intro_06,
			DALA_Tavern.VO_DALA_901h_Male_Mech_IntroChu_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_IntroEudora_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_IntroGeorge_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_IntroKriziki_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_IntroOlBarkeye_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_IntroRakanishu_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_IntroSqueamlish_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_IntroTekahn_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_IntroVessina_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_Outro_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_Outro_02,
			DALA_Tavern.VO_DALA_901h_Male_Mech_Outro_03,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerBrood_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerBrood_02,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerDismiss_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerGoodFood_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerKindle_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerRecruit_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerRecruitAVeteran_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerRightHandMan_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerRightHandMan_02,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerRoundOfDrinks_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerTakeAChance_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerTallTales_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerTellAStory_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerTheChallenge_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerTheGangsAllHere_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerYoureAllFired_01,
			DALA_Tavern.VO_DALA_901h_Male_Mech_TurnOne_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
		GenericDungeonMissionEntity.VOPool value = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_DRUID_VO);
		this.m_VOPools.Add(909, value);
		GenericDungeonMissionEntity.VOPool value2 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_HUNTER_VO);
		this.m_VOPools.Add(910, value2);
		GenericDungeonMissionEntity.VOPool value3 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_MAGE_VO);
		this.m_VOPools.Add(911, value3);
		GenericDungeonMissionEntity.VOPool value4 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_PALADIN_VO);
		this.m_VOPools.Add(912, value4);
		GenericDungeonMissionEntity.VOPool value5 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_PRIEST_VO);
		this.m_VOPools.Add(913, value5);
		GenericDungeonMissionEntity.VOPool value6 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_ROGUE_VO);
		this.m_VOPools.Add(914, value6);
		GenericDungeonMissionEntity.VOPool value7 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_SHAMAN_VO);
		this.m_VOPools.Add(915, value7);
		GenericDungeonMissionEntity.VOPool value8 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_WARLOCK_VO);
		this.m_VOPools.Add(916, value8);
		GenericDungeonMissionEntity.VOPool value9 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_Tavern.VO_DALA_900h_Male_Human_Intro_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_WARRIOR_VO);
		this.m_VOPools.Add(917, value9);
	}

	// Token: 0x06003E16 RID: 15894 RVA: 0x001474B8 File Offset: 0x001456B8
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = DALA_Tavern.VO_DALA_900h_Male_Human_Intro_05;
	}

	// Token: 0x06003E17 RID: 15895 RVA: 0x001474D0 File Offset: 0x001456D0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (cardId == DALA_Tavern.BARTENDOTRON_CARDID)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Tavern.VO_DALA_901h_Male_Mech_TurnOne_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
	}

	// Token: 0x06003E18 RID: 15896 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003E19 RID: 15897 RVA: 0x0014757A File Offset: 0x0014577A
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		GenericDungeonMissionEntity.VOPool vopool = null;
		int num = 0;
		string item = null;
		string cardId2 = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (cardId2 == DALA_Tavern.BARTENDERBOB_CARDID)
		{
			if (missionEvent != 101)
			{
				if (missionEvent != 102)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					GameState.Get().SetBusy(true);
					if (DALA_Tavern.m_OutroLinesCopy.Count == 0)
					{
						DALA_Tavern.m_OutroLinesCopy = new List<string>(DALA_Tavern.m_OutroLines);
					}
					string soundPath = base.PopRandomLine(DALA_Tavern.m_OutroLinesCopy);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(soundPath, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				if (cardId == "DALA_Squeamlish")
				{
					num = 909;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_900h_Male_Human_IntroSqueamlish_01;
				}
				if (cardId == "DALA_Barkeye")
				{
					num = 910;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_900h_Male_Human_IntroOlBarkeye_01;
				}
				if (cardId == "DALA_Rakanishu")
				{
					num = 911;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_900h_Male_Human_IntroRakanishu_01;
				}
				if (cardId == "DALA_George")
				{
					num = 912;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_900h_Male_Human_IntroGeorge_01;
				}
				if (cardId == "DALA_Kriziki")
				{
					num = 913;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_900h_Male_Human_IntroKriziki_01;
				}
				if (cardId == "DALA_Eudora")
				{
					num = 913;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_900h_Male_Human_IntroEudora_02;
				}
				if (cardId == "DALA_Vessina")
				{
					num = 915;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_900h_Male_Human_IntroVessina_01;
				}
				if (cardId == "DALA_Tekahn")
				{
					num = 916;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_900h_Male_Human_IntroTekahn_01;
				}
				if (cardId == "DALA_Chu")
				{
					num = 917;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_900h_Male_Human_IntroChu_01;
				}
				long num2;
				GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.ADVENTURE_DATA_CLIENT_DALARAN, vopool.m_oncePerAccountGameSaveSubkey, out num2);
				if (num2 > 0L)
				{
					for (int i = 0; i < 3; i++)
					{
						this.m_IntroLines.Add(item);
					}
					string soundPath2 = base.PopRandomLine(this.m_IntroLines);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(soundPath2, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				}
				else
				{
					yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(num));
				}
			}
			yield break;
		}
		if (cardId2 == DALA_Tavern.BARTENDOTRON_CARDID)
		{
			if (missionEvent != 101)
			{
				if (missionEvent != 102)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					GameState.Get().SetBusy(true);
					if (DALA_Tavern.m_Bartendortron_OutroLinesCopy.Count == 0)
					{
						DALA_Tavern.m_Bartendortron_OutroLinesCopy = new List<string>(DALA_Tavern.m_Bartendortron_OutroLines);
					}
					string soundPath3 = base.PopRandomLine(DALA_Tavern.m_Bartendortron_OutroLinesCopy);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(soundPath3, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				if (cardId == "DALA_Squeamlish")
				{
					num = 909;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_901h_Male_Mech_IntroSqueamlish_01;
				}
				if (cardId == "DALA_Barkeye")
				{
					num = 910;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_901h_Male_Mech_IntroOlBarkeye_01;
				}
				if (cardId == "DALA_Rakanishu")
				{
					num = 911;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_901h_Male_Mech_IntroRakanishu_01;
				}
				if (cardId == "DALA_George")
				{
					num = 912;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_901h_Male_Mech_IntroGeorge_01;
				}
				if (cardId == "DALA_Kriziki")
				{
					num = 913;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_901h_Male_Mech_IntroKriziki_01;
				}
				if (cardId == "DALA_Eudora")
				{
					num = 913;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_901h_Male_Mech_IntroEudora_01;
				}
				if (cardId == "DALA_Vessina")
				{
					num = 915;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_901h_Male_Mech_IntroVessina_01;
				}
				if (cardId == "DALA_Tekahn")
				{
					num = 916;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_901h_Male_Mech_IntroTekahn_01;
				}
				if (cardId == "DALA_Chu")
				{
					num = 917;
					vopool = this.m_VOPools[num];
					item = DALA_Tavern.VO_DALA_901h_Male_Mech_IntroChu_01;
				}
				for (int j = 0; j < 3; j++)
				{
					this.m_Bartendortron_IntroLines.Add(item);
				}
				string soundPath4 = base.PopRandomLine(this.m_Bartendortron_IntroLines);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(soundPath4, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		yield break;
	}

	// Token: 0x06003E1A RID: 15898 RVA: 0x00147590 File Offset: 0x00145790
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		string enemyHeroCardID = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (enemyHeroCardID == DALA_Tavern.BARTENDERBOB_CARDID)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
			if (num <= 3387828626U)
			{
				if (num <= 3354420483U)
				{
					if (num != 3337642864U)
					{
						if (num != 3354273388U)
						{
							if (num == 3354420483U)
							{
								if (cardId == "DALA_906")
								{
									yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerRoundOfDrinks_01, 2.5f);
								}
							}
						}
						else if (cardId == "DALA_910")
						{
							yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerTheGangsAllHere_01, 2.5f);
						}
					}
					else if (cardId == "DALA_907")
					{
						yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerRecruitAVeteran_01, 2.5f);
					}
				}
				else if (num != 3371051007U)
				{
					if (num != 3371198102U)
					{
						if (num == 3387828626U)
						{
							if (cardId == "DALA_912")
							{
								if (UnityEngine.Random.Range(1, 11) == 1)
								{
									yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerBrood_02, 2.5f);
								}
								else
								{
									yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerBrood_01, 2.5f);
								}
							}
						}
					}
					else if (cardId == "DALA_905")
					{
						yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerRightHandMan_01, 2.5f);
					}
				}
				else if (cardId == "DALA_911")
				{
					yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerKindle_01, 2.5f);
				}
			}
			else if (num <= 3404753340U)
			{
				if (num != 3387975721U)
				{
					if (num != 3404606245U)
					{
						if (num == 3404753340U)
						{
							if (cardId == "DALA_903")
							{
								yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerTakeAChance_01, 2.5f);
							}
						}
					}
					else if (cardId == "DALA_913")
					{
						yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerTallTales_01, 2.5f);
					}
				}
				else if (cardId == "DALA_904")
				{
					yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerGoodFood_01, 2.5f);
				}
			}
			else if (num <= 3438308578U)
			{
				if (num != 3421530959U)
				{
					if (num == 3438308578U)
					{
						if (cardId == "DALA_901")
						{
							yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerRecruit_01, 2.5f);
						}
					}
				}
				else if (cardId == "DALA_902")
				{
					yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerDismiss_01, 2.5f);
				}
			}
			else if (num != 3572529530U)
			{
				if (num == 3589307149U)
				{
					if (cardId == "DALA_908")
					{
						yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerTellAStory_01, 2.5f);
					}
				}
			}
			else if (cardId == "DALA_909")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_900h_Male_Human_PlayerYoureAllFired_01, 2.5f);
			}
		}
		else if (enemyHeroCardID == DALA_Tavern.BARTENDOTRON_CARDID)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
			if (num <= 3387828626U)
			{
				if (num <= 3354420483U)
				{
					if (num != 3337642864U)
					{
						if (num != 3354273388U)
						{
							if (num == 3354420483U)
							{
								if (cardId == "DALA_906")
								{
									yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerRoundOfDrinks_01, 2.5f);
								}
							}
						}
						else if (cardId == "DALA_910")
						{
							yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerTheGangsAllHere_01, 2.5f);
						}
					}
					else if (cardId == "DALA_907")
					{
						yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerRecruitAVeteran_01, 2.5f);
					}
				}
				else if (num != 3371051007U)
				{
					if (num != 3371198102U)
					{
						if (num == 3387828626U)
						{
							if (cardId == "DALA_912")
							{
								if (UnityEngine.Random.Range(1, 11) == 1)
								{
									yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerBrood_02, 2.5f);
								}
								else
								{
									yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerBrood_01, 2.5f);
								}
							}
						}
					}
					else if (cardId == "DALA_905")
					{
						if (UnityEngine.Random.Range(1, 3) == 1)
						{
							yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerRightHandMan_01, 2.5f);
						}
						else
						{
							yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerRightHandMan_02, 2.5f);
						}
					}
				}
				else if (cardId == "DALA_911")
				{
					yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerKindle_01, 2.5f);
				}
			}
			else if (num <= 3404753340U)
			{
				if (num != 3387975721U)
				{
					if (num != 3404606245U)
					{
						if (num == 3404753340U)
						{
							if (cardId == "DALA_903")
							{
								yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerTakeAChance_01, 2.5f);
							}
						}
					}
					else if (cardId == "DALA_913")
					{
						yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerTallTales_01, 2.5f);
					}
				}
				else if (cardId == "DALA_904")
				{
					yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerGoodFood_01, 2.5f);
				}
			}
			else if (num <= 3438308578U)
			{
				if (num != 3421530959U)
				{
					if (num == 3438308578U)
					{
						if (cardId == "DALA_901")
						{
							yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerRecruit_01, 2.5f);
						}
					}
				}
				else if (cardId == "DALA_902")
				{
					yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerDismiss_01, 2.5f);
				}
			}
			else if (num != 3572529530U)
			{
				if (num == 3589307149U)
				{
					if (cardId == "DALA_908")
					{
						yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerTellAStory_01, 2.5f);
					}
				}
			}
			else if (cardId == "DALA_909")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Tavern.VO_DALA_901h_Male_Mech_PlayerYoureAllFired_01, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x06003E1B RID: 15899 RVA: 0x001475A8 File Offset: 0x001457A8
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (this.m_thinkEmoteFirstRun)
		{
			if (cardId == DALA_Tavern.BARTENDOTRON_CARDID)
			{
				this.m_BossIdleLines = new List<string>(DALA_Tavern.m_Bartendortron_IdleLines);
				this.m_BossIdleLinesCopy = new List<string>(DALA_Tavern.m_Bartendortron_IdleLines);
			}
			else
			{
				this.m_BossIdleLines = new List<string>(DALA_Tavern.m_IdleLines);
				this.m_BossIdleLinesCopy = new List<string>(DALA_Tavern.m_IdleLines);
			}
			this.m_thinkEmoteFirstRun = false;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		float thinkEmoteBossThinkChancePercentage = this.GetThinkEmoteBossThinkChancePercentage();
		float num = UnityEngine.Random.Range(0f, 1f);
		if (thinkEmoteBossThinkChancePercentage > num && this.m_BossIdleLines != null && this.m_BossIdleLines.Count != 0)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			string line = base.PopRandomLine(this.m_BossIdleLinesCopy);
			if (this.m_BossIdleLinesCopy.Count == 0)
			{
				if (cardId == DALA_Tavern.BARTENDOTRON_CARDID)
				{
					this.m_BossIdleLinesCopy = new List<string>(DALA_Tavern.m_Bartendortron_IdleLines);
				}
				else
				{
					this.m_BossIdleLinesCopy = new List<string>(DALA_Tavern.m_IdleLines);
				}
			}
			Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
			return;
		}
		EmoteType emoteType = EmoteType.THINK1;
		switch (UnityEngine.Random.Range(1, 4))
		{
		case 1:
			emoteType = EmoteType.THINK1;
			break;
		case 2:
			emoteType = EmoteType.THINK2;
			break;
		case 3:
			emoteType = EmoteType.THINK3;
			break;
		}
		GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType);
	}

	// Token: 0x06003E1C RID: 15900 RVA: 0x0014774B File Offset: 0x0014594B
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

	// Token: 0x040029AA RID: 10666
	private static readonly AssetReference VO_DALA_900h_Male_Human_Idle_01 = new AssetReference("VO_DALA_900h_Male_Human_Idle_01.prefab:dbb905dd9ace1b640902d08aa66182d1");

	// Token: 0x040029AB RID: 10667
	private static readonly AssetReference VO_DALA_900h_Male_Human_Idle_02 = new AssetReference("VO_DALA_900h_Male_Human_Idle_02.prefab:3cf9566a837907a4cb8c1d0e39362c9a");

	// Token: 0x040029AC RID: 10668
	private static readonly AssetReference VO_DALA_900h_Male_Human_Idle_03 = new AssetReference("VO_DALA_900h_Male_Human_Idle_03.prefab:bafa81859a8927049a740a1b588fd113");

	// Token: 0x040029AD RID: 10669
	private static readonly AssetReference VO_DALA_900h_Male_Human_Intro_01 = new AssetReference("VO_DALA_900h_Male_Human_Intro_01.prefab:9449f0b4c1b7d4a4282e3fbe7a4d0066");

	// Token: 0x040029AE RID: 10670
	private static readonly AssetReference VO_DALA_900h_Male_Human_Intro_02 = new AssetReference("VO_DALA_900h_Male_Human_Intro_02.prefab:b3d2d7fc342eaf7408dc9947c7cdaa53");

	// Token: 0x040029AF RID: 10671
	private static readonly AssetReference VO_DALA_900h_Male_Human_Intro_03 = new AssetReference("VO_DALA_900h_Male_Human_Intro_03.prefab:10d8ce68d0c9a7b4db783bad35362bf1");

	// Token: 0x040029B0 RID: 10672
	private static readonly AssetReference VO_DALA_900h_Male_Human_Intro_04 = new AssetReference("VO_DALA_900h_Male_Human_Intro_04.prefab:76f5856098d17a24ba4dc1ce80716f7e");

	// Token: 0x040029B1 RID: 10673
	private static readonly AssetReference VO_DALA_900h_Male_Human_Intro_05 = new AssetReference("VO_DALA_900h_Male_Human_Intro_05.prefab:3ced63167e3a40e4c88a46daeada9620");

	// Token: 0x040029B2 RID: 10674
	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroChu_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroChu_01.prefab:134d9cc8075977a47a22fa78abd264af");

	// Token: 0x040029B3 RID: 10675
	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroEudora_02 = new AssetReference("VO_DALA_900h_Male_Human_IntroEudora_02.prefab:9475700429a8c624ab33b9b9b3a49339");

	// Token: 0x040029B4 RID: 10676
	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroGeorge_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroGeorge_01.prefab:f150d96a7f351e747844e4a453d6a6cc");

	// Token: 0x040029B5 RID: 10677
	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroKriziki_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroKriziki_01.prefab:94f757d0bddea8b4cbd612783bbdf01f");

	// Token: 0x040029B6 RID: 10678
	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroOlBarkeye_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroOlBarkeye_01.prefab:1785f482c8f4ae34c87f1873f8ad9524");

	// Token: 0x040029B7 RID: 10679
	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroRakanishu_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroRakanishu_01.prefab:a74a7d981b5a08a499dc9dd48eec153d");

	// Token: 0x040029B8 RID: 10680
	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroSqueamlish_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroSqueamlish_01.prefab:15c66b1ecd6382d4691117223da64058");

	// Token: 0x040029B9 RID: 10681
	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroTekahn_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroTekahn_01.prefab:efa24b64d3be70a46b7197fd01fd6b41");

	// Token: 0x040029BA RID: 10682
	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroVessina_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroVessina_01.prefab:34908c49072198c45ac3669014c81c45");

	// Token: 0x040029BB RID: 10683
	private static readonly AssetReference VO_DALA_900h_Male_Human_Outro_01 = new AssetReference("VO_DALA_900h_Male_Human_Outro_01.prefab:500f3e8787af1c9489f10617f89f30c5");

	// Token: 0x040029BC RID: 10684
	private static readonly AssetReference VO_DALA_900h_Male_Human_Outro_02 = new AssetReference("VO_DALA_900h_Male_Human_Outro_02.prefab:4da33b7a627c3794d922b351514801e3");

	// Token: 0x040029BD RID: 10685
	private static readonly AssetReference VO_DALA_900h_Male_Human_Outro_03 = new AssetReference("VO_DALA_900h_Male_Human_Outro_03.prefab:6253c613cd1653f489a9f9b44a88a297");

	// Token: 0x040029BE RID: 10686
	private static readonly AssetReference VO_DALA_900h_Male_Human_Outro_04 = new AssetReference("VO_DALA_900h_Male_Human_Outro_04.prefab:9b0f7be66c854c345bd2cbb5e439952c");

	// Token: 0x040029BF RID: 10687
	private static readonly AssetReference VO_DALA_900h_Male_Human_Outro_05 = new AssetReference("VO_DALA_900h_Male_Human_Outro_05.prefab:4c204ac82e7ded14e9dae3940f9a8dd6");

	// Token: 0x040029C0 RID: 10688
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerBrood_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerBrood_01.prefab:456313fb3b1046a47a8313a452bb6415");

	// Token: 0x040029C1 RID: 10689
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerBrood_02 = new AssetReference("VO_DALA_900h_Male_Human_PlayerBrood_02.prefab:87bdd7dd4ed293b4aa951322c7fcd523");

	// Token: 0x040029C2 RID: 10690
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerDismiss_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerDismiss_01.prefab:42f71bfccfd097d4dbc0fbe4f5058639");

	// Token: 0x040029C3 RID: 10691
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerGoodFood_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerGoodFood_01.prefab:3c8d6db526d2ace43b737f26347a99f7");

	// Token: 0x040029C4 RID: 10692
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerKindle_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerKindle_01.prefab:17ea2f82c22e95e4c912e07b77027d31");

	// Token: 0x040029C5 RID: 10693
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerRecruit_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerRecruit_01.prefab:adfe05e034ce3d14bb4d6fa37d00a5a1");

	// Token: 0x040029C6 RID: 10694
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerRecruitAVeteran_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerRecruitAVeteran_01.prefab:67afc87e17c97a744b9c8b72b59043d6");

	// Token: 0x040029C7 RID: 10695
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerRightHandMan_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerRightHandMan_01.prefab:0009033bd405db74cb50a95a226230d9");

	// Token: 0x040029C8 RID: 10696
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerRoundOfDrinks_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerRoundOfDrinks_01.prefab:2cac7a36c24b2404ca7c866a9a3c64a2");

	// Token: 0x040029C9 RID: 10697
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerTakeAChance_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerTakeAChance_01.prefab:50ed7a2465d6a5f4790e3193342c6583");

	// Token: 0x040029CA RID: 10698
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerTallTales_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerTallTales_01.prefab:2180fc84653ed864ca5877374c146e17");

	// Token: 0x040029CB RID: 10699
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerTellAStory_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerTellAStory_01.prefab:f2d640c4155e1054ea9e00ec808adbba");

	// Token: 0x040029CC RID: 10700
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerTheGangsAllHere_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerTheGangsAllHere_01.prefab:82ae89ac0b8edcf458093421a2eb54cc");

	// Token: 0x040029CD RID: 10701
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerYoureAllFired_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerYoureAllFired_01.prefab:dbc3069fd98fdbd41bb555cb7eb13f54");

	// Token: 0x040029CE RID: 10702
	private static readonly AssetReference VO_DALA_901h_Male_Mech_Death_01 = new AssetReference("VO_DALA_901h_Male_Mech_Death_01.prefab:9fe4235dcd6f963479e28fa1625fe1af");

	// Token: 0x040029CF RID: 10703
	private static readonly AssetReference VO_DALA_901h_Male_Mech_Idle_01 = new AssetReference("VO_DALA_901h_Male_Mech_Idle_01.prefab:158422eed9e14fe4c813b76ae7482f06");

	// Token: 0x040029D0 RID: 10704
	private static readonly AssetReference VO_DALA_901h_Male_Mech_Idle_02 = new AssetReference("VO_DALA_901h_Male_Mech_Idle_02.prefab:e5029757a8fbf964cbdf2c1ed6c2ec1a");

	// Token: 0x040029D1 RID: 10705
	private static readonly AssetReference VO_DALA_901h_Male_Mech_Idle_03 = new AssetReference("VO_DALA_901h_Male_Mech_Idle_03.prefab:1386f2b7fe103a049b2fa65db88c1ab5");

	// Token: 0x040029D2 RID: 10706
	private static readonly AssetReference VO_DALA_901h_Male_Mech_Intro_01 = new AssetReference("VO_DALA_901h_Male_Mech_Intro_01.prefab:5678393cc999bb142b38330e1ef94dbd");

	// Token: 0x040029D3 RID: 10707
	private static readonly AssetReference VO_DALA_901h_Male_Mech_Intro_02 = new AssetReference("VO_DALA_901h_Male_Mech_Intro_02.prefab:e323453876c6a0147a286b8cbb1d5930");

	// Token: 0x040029D4 RID: 10708
	private static readonly AssetReference VO_DALA_901h_Male_Mech_Intro_03 = new AssetReference("VO_DALA_901h_Male_Mech_Intro_03.prefab:a8f66126889493f4583a448c39694f00");

	// Token: 0x040029D5 RID: 10709
	private static readonly AssetReference VO_DALA_901h_Male_Mech_Intro_04 = new AssetReference("VO_DALA_901h_Male_Mech_Intro_04.prefab:434888aec49048149852ba382bda95c0");

	// Token: 0x040029D6 RID: 10710
	private static readonly AssetReference VO_DALA_901h_Male_Mech_Intro_05 = new AssetReference("VO_DALA_901h_Male_Mech_Intro_05.prefab:bafa9206894d60f4089b2d3638489549");

	// Token: 0x040029D7 RID: 10711
	private static readonly AssetReference VO_DALA_901h_Male_Mech_Intro_06 = new AssetReference("VO_DALA_901h_Male_Mech_Intro_06.prefab:74b222aa30ec2bf47b9bbd8bde74708f");

	// Token: 0x040029D8 RID: 10712
	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroChu_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroChu_01.prefab:ecd36b7d5b6b47248825c5034dd2901c");

	// Token: 0x040029D9 RID: 10713
	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroEudora_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroEudora_01.prefab:43ac2c5eeb8a5b648a1d6b15cd0b2dbe");

	// Token: 0x040029DA RID: 10714
	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroGeorge_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroGeorge_01.prefab:94452ed7ae3d677479c85ca032b2dfa6");

	// Token: 0x040029DB RID: 10715
	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroKriziki_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroKriziki_01.prefab:ced09968469c30049982057db41c0eaf");

	// Token: 0x040029DC RID: 10716
	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroOlBarkeye_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroOlBarkeye_01.prefab:bc3fce38befd28a438363481245d13a2");

	// Token: 0x040029DD RID: 10717
	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroRakanishu_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroRakanishu_01.prefab:521a52551a4f4b1459cbae7c8c0d306c");

	// Token: 0x040029DE RID: 10718
	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroSqueamlish_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroSqueamlish_01.prefab:474837621fc9ed340ae1097f6b419b1e");

	// Token: 0x040029DF RID: 10719
	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroTekahn_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroTekahn_01.prefab:c07db9a51b36f064894ab7e1cbcec09a");

	// Token: 0x040029E0 RID: 10720
	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroVessina_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroVessina_01.prefab:d2da83f427149d440b6b395c49bbea19");

	// Token: 0x040029E1 RID: 10721
	private static readonly AssetReference VO_DALA_901h_Male_Mech_Outro_01 = new AssetReference("VO_DALA_901h_Male_Mech_Outro_01.prefab:806d5323d81326149bb3982dba145114");

	// Token: 0x040029E2 RID: 10722
	private static readonly AssetReference VO_DALA_901h_Male_Mech_Outro_02 = new AssetReference("VO_DALA_901h_Male_Mech_Outro_02.prefab:953b85d08c6945a479916401a33f3638");

	// Token: 0x040029E3 RID: 10723
	private static readonly AssetReference VO_DALA_901h_Male_Mech_Outro_03 = new AssetReference("VO_DALA_901h_Male_Mech_Outro_03.prefab:ec27ba35d9003d74d843ef754ec9911d");

	// Token: 0x040029E4 RID: 10724
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerBrood_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerBrood_01.prefab:511bf126da14f124e9610f4c68caa95d");

	// Token: 0x040029E5 RID: 10725
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerBrood_02 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerBrood_02.prefab:5724a6477ee0f7147adaeca3b29fdbbb");

	// Token: 0x040029E6 RID: 10726
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerDismiss_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerDismiss_01.prefab:f7e9245aedfa70e4c8fa7354ee29ad77");

	// Token: 0x040029E7 RID: 10727
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerGoodFood_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerGoodFood_01.prefab:4cfcc28f63a76724cbcc0f309cc2ee31");

	// Token: 0x040029E8 RID: 10728
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerKindle_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerKindle_01.prefab:af41bd38f2a8498438cdc2f504673e8e");

	// Token: 0x040029E9 RID: 10729
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerRecruit_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerRecruit_01.prefab:efb4f80235646e74e974007b58e0369e");

	// Token: 0x040029EA RID: 10730
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerRecruitAVeteran_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerRecruitAVeteran_01.prefab:5c6ae624000940648ac87c574ab5f963");

	// Token: 0x040029EB RID: 10731
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerRightHandMan_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerRightHandMan_01.prefab:61177d7c5129bac4f9243493a128a4a9");

	// Token: 0x040029EC RID: 10732
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerRightHandMan_02 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerRightHandMan_02.prefab:5e7dedc2026f31e47b7066f289acb4b2");

	// Token: 0x040029ED RID: 10733
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerRoundOfDrinks_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerRoundOfDrinks_01.prefab:a35fb1d03e06217488f91b6ba6bec037");

	// Token: 0x040029EE RID: 10734
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerTakeAChance_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerTakeAChance_01.prefab:b3afb8901d6ae6c41aaab5124fbc0531");

	// Token: 0x040029EF RID: 10735
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerTallTales_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerTallTales_01.prefab:942537c8075b3a64ca5a4936f2a0bae1");

	// Token: 0x040029F0 RID: 10736
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerTellAStory_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerTellAStory_01.prefab:cc13e37231d58694a86846223ec7b75c");

	// Token: 0x040029F1 RID: 10737
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerTheChallenge_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerTheChallenge_01.prefab:becce32e9a04e0b4aaa28b9a71cffe50");

	// Token: 0x040029F2 RID: 10738
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerTheGangsAllHere_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerTheGangsAllHere_01.prefab:088705a54b564a64bb5f2b9fb8c9e3ac");

	// Token: 0x040029F3 RID: 10739
	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerYoureAllFired_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerYoureAllFired_01.prefab:0acaaae59417b49468ac19b44c0c23cd");

	// Token: 0x040029F4 RID: 10740
	private static readonly AssetReference VO_DALA_901h_Male_Mech_TurnOne_01 = new AssetReference("VO_DALA_901h_Male_Mech_TurnOne_01.prefab:3c551789dd2875e48aac37174c6fc972");

	// Token: 0x040029F5 RID: 10741
	private static readonly string BARTENDERBOB_CARDID = "DALA_BOSS_99h";

	// Token: 0x040029F6 RID: 10742
	private static readonly string BARTENDOTRON_CARDID = "DALA_BOSS_98h";

	// Token: 0x040029F7 RID: 10743
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Tavern.VO_DALA_900h_Male_Human_Idle_01,
		DALA_Tavern.VO_DALA_900h_Male_Human_Idle_02,
		DALA_Tavern.VO_DALA_900h_Male_Human_Idle_03
	};

	// Token: 0x040029F8 RID: 10744
	private static List<string> m_Bartendortron_IdleLines = new List<string>
	{
		DALA_Tavern.VO_DALA_901h_Male_Mech_Idle_01,
		DALA_Tavern.VO_DALA_901h_Male_Mech_Idle_02,
		DALA_Tavern.VO_DALA_901h_Male_Mech_Idle_03
	};

	// Token: 0x040029F9 RID: 10745
	private static List<string> m_OutroLinesCopy = new List<string>();

	// Token: 0x040029FA RID: 10746
	private static List<string> m_OutroLines = new List<string>
	{
		DALA_Tavern.VO_DALA_900h_Male_Human_Outro_01,
		DALA_Tavern.VO_DALA_900h_Male_Human_Outro_02,
		DALA_Tavern.VO_DALA_900h_Male_Human_Outro_03,
		DALA_Tavern.VO_DALA_900h_Male_Human_Outro_04,
		DALA_Tavern.VO_DALA_900h_Male_Human_Outro_05
	};

	// Token: 0x040029FB RID: 10747
	private static List<string> m_Bartendortron_OutroLinesCopy = new List<string>();

	// Token: 0x040029FC RID: 10748
	private static List<string> m_Bartendortron_OutroLines = new List<string>
	{
		DALA_Tavern.VO_DALA_901h_Male_Mech_Outro_01,
		DALA_Tavern.VO_DALA_901h_Male_Mech_Outro_02,
		DALA_Tavern.VO_DALA_901h_Male_Mech_Outro_03
	};

	// Token: 0x040029FD RID: 10749
	private List<string> m_IntroLines = new List<string>
	{
		DALA_Tavern.VO_DALA_900h_Male_Human_Intro_02,
		DALA_Tavern.VO_DALA_900h_Male_Human_Intro_03,
		DALA_Tavern.VO_DALA_900h_Male_Human_Intro_04
	};

	// Token: 0x040029FE RID: 10750
	private List<string> m_Bartendortron_IntroLines = new List<string>
	{
		DALA_Tavern.VO_DALA_901h_Male_Mech_Intro_01,
		DALA_Tavern.VO_DALA_901h_Male_Mech_Intro_02,
		DALA_Tavern.VO_DALA_901h_Male_Mech_Intro_03,
		DALA_Tavern.VO_DALA_901h_Male_Mech_Intro_04,
		DALA_Tavern.VO_DALA_901h_Male_Mech_Intro_05,
		DALA_Tavern.VO_DALA_901h_Male_Mech_Intro_06
	};

	// Token: 0x040029FF RID: 10751
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04002A00 RID: 10752
	public bool m_thinkEmoteFirstRun = true;
}
