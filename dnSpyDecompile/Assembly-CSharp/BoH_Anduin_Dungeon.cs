using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000516 RID: 1302
public class BoH_Anduin_Dungeon : BoH_Andiun_MissionEntity
{
	// Token: 0x0600465F RID: 18015 RVA: 0x0017BEB8 File Offset: 0x0017A0B8
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = null;
		this.m_deathLine = null;
		this.m_standardEmoteResponseLine = null;
		this.m_BossIdleLines = new List<string>(this.GetBossIdleLines());
		this.m_BossIdleLinesCopy = new List<string>(this.GetBossIdleLines());
		this.m_OverrideMusicTrack = MusicPlaylistType.Invalid;
		this.m_OverrideMulliganMusicTrack = MusicPlaylistType.Invalid;
		this.m_Mission_EnemyHeroShouldExplodeOnDefeat = true;
		this.m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;
		this.m_OverrideBossSubtext = null;
		this.m_OverridePlayerSubtext = null;
		this.m_SupressEnemyDeathTextBubble = false;
		this.m_Mission_FriendlyPlayIdleLines = false;
	}

	// Token: 0x06004660 RID: 18016 RVA: 0x0017BF3A File Offset: 0x0017A13A
	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.BOH;
	}

	// Token: 0x06004661 RID: 18017 RVA: 0x0017BF44 File Offset: 0x0017A144
	public static BoH_Anduin_Dungeon InstantiateTemplate_SoloDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		Log.All.PrintError("BoH_Andiun_Dungeon.InstantiateTemplate_SoloDungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new BoH_Anduin_Dungeon();
	}

	// Token: 0x06004662 RID: 18018 RVA: 0x0017BF77 File Offset: 0x0017A177
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		entity.GetCardId();
		yield break;
	}

	// Token: 0x06004663 RID: 18019 RVA: 0x0017BF8D File Offset: 0x0017A18D
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		base.MissionPause(true);
		yield return this.HandleMissionEventWithTiming(514);
		base.MissionPause(false);
		yield break;
	}

	// Token: 0x06004664 RID: 18020 RVA: 0x0017BF9C File Offset: 0x0017A19C
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield break;
		}
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		UnityEngine.Random.Range(0f, 1f);
		base.GetTag(GAME_TAG.TURN);
		GameState.Get().GetGameEntity().GetTag(GAME_TAG.EXTRA_TURNS_TAKEN_THIS_GAME);
		switch (missionEvent)
		{
		case 600:
			this.m_Mission_EnemyHeroShouldExplodeOnDefeat = false;
			goto IL_6C8;
		case 601:
			this.m_Mission_FriendlyHeroShouldExplodeOnDefeat = false;
			goto IL_6C8;
		case 602:
			this.m_MissionDisableAutomaticVO = true;
			goto IL_6C8;
		case 603:
			this.m_MissionDisableAutomaticVO = false;
			goto IL_6C8;
		case 604:
		case 605:
		case 606:
		case 607:
		case 608:
		case 609:
			break;
		case 610:
			this.m_Mission_EnemyHeroShouldExplodeOnDefeat = true;
			goto IL_6C8;
		case 611:
			this.m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;
			goto IL_6C8;
		case 612:
			this.m_DoEmoteDrivenStart = true;
			goto IL_6C8;
		default:
			switch (missionEvent)
			{
			case 1000:
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				if (this.m_PlayPlayerVOLineIndex + 1 >= this.m_PlayerVOLines.Count)
				{
					this.m_PlayPlayerVOLineIndex = 0;
				}
				else
				{
					this.m_PlayPlayerVOLineIndex++;
				}
				SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
				yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
				goto IL_6C8;
			case 1001:
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
				yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
				goto IL_6C8;
			case 1002:
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				if (this.m_PlayBossVOLineIndex + 1 >= this.m_BossVOLines.Count)
				{
					this.m_PlayBossVOLineIndex = 0;
				}
				else
				{
					this.m_PlayBossVOLineIndex++;
				}
				SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
				yield return base.PlayBossLine(enemyActor, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
				goto IL_6C8;
			case 1003:
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
				yield return base.PlayBossLine(enemyActor, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
				goto IL_6C8;
			case 1004:
			case 1005:
			case 1006:
			case 1007:
			case 1008:
			case 1009:
				break;
			case 1010:
				if (this.m_forceAlwaysPlayLine)
				{
					this.m_forceAlwaysPlayLine = false;
					goto IL_6C8;
				}
				this.m_forceAlwaysPlayLine = true;
				goto IL_6C8;
			case 1011:
			{
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				foreach (string text in this.m_BossVOLines)
				{
					SceneDebugger.Get().AddMessage(text);
					yield return base.MissionPlayVO(enemyActor, text);
				}
				List<string>.Enumerator enumerator = default(List<string>.Enumerator);
				foreach (string text2 in this.m_PlayerVOLines)
				{
					SceneDebugger.Get().AddMessage(text2);
					yield return base.MissionPlayVO(enemyActor, text2);
				}
				enumerator = default(List<string>.Enumerator);
				goto IL_6C8;
			}
			case 1012:
			{
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				foreach (string text3 in this.m_BossVOLines)
				{
					SceneDebugger.Get().AddMessage(text3);
					yield return base.MissionPlayVO(enemyActor, text3);
				}
				List<string>.Enumerator enumerator = default(List<string>.Enumerator);
				goto IL_6C8;
			}
			case 1013:
			{
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				foreach (string text4 in this.m_PlayerVOLines)
				{
					SceneDebugger.Get().AddMessage(text4);
					yield return base.MissionPlayVO(enemyActor, text4);
				}
				List<string>.Enumerator enumerator = default(List<string>.Enumerator);
				goto IL_6C8;
			}
			default:
				if (missionEvent == 58023)
				{
					SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
					GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
					SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
					goto IL_6C8;
				}
				break;
			}
			break;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_6C8:
		yield break;
		yield break;
	}
}
