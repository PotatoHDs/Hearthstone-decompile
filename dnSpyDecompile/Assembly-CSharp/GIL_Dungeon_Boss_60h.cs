using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000404 RID: 1028
public class GIL_Dungeon_Boss_60h : GIL_Dungeon
{
	// Token: 0x060038DC RID: 14556 RVA: 0x0011E4D8 File Offset: 0x0011C6D8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_60h_Male_HumanGhost_IntroTess_01.prefab:fff42d3944e8bd74bbd966676233a3ee",
			"VO_GILA_BOSS_60h_Male_HumanGhost_EmoteResponseTess_01.prefab:6315438179aa5bf48857edee7132976a",
			"VO_GILA_BOSS_60h_Male_HumanGhost_Death_02.prefab:a4d7f96185d3ee04d9c4d84b926b9f07",
			"VO_GILA_BOSS_60h_Male_HumanGhost_HeroPower_01.prefab:52dbc5a597a03034392514d2929f0bb7",
			"VO_GILA_BOSS_60h_Male_HumanGhost_HeroPower_07.prefab:7cfa077bbb9c82b4aa5575c5fe88b994",
			"VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayPatches_02.prefab:0be9e962c132e8847af72531643812e3",
			"VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayKragg_02.prefab:5921fc1f19b5f2b479b6e1433b947dd0",
			"VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayKingsblade_01.prefab:d5916616acd23f348b16805f3ff33dd1",
			"VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayKingsblade_02.prefab:13151159e0811c543a607ae06de474c9",
			"VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayerKingsblade_01.prefab:fa4a18427dcec41409bb74eac8198200",
			"VO_GILA_BOSS_60h_Male_HumanGhost_EventBigWeapon_01.prefab:b3f3e20c23274f4418d2fbc006c2dc25",
			"VO_GILA_BOSS_60h_Male_HumanGhost_EventBigWeapon_02.prefab:c586c7ae2997be44eb7f613a19f79eca",
			"VO_GILA_BOSS_60h_Male_HumanGhost_EventBigWeapon_03.prefab:263e1671c8b6ca34c8b09a589dd6aa4f",
			"VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayTuskarrRaider_01.prefab:13ac2c5bc7b2dba4f89fe041aacd4811",
			"VO_GILA_BOSS_60h_Male_HumanGhost_EventTurn2_01.prefab:295193b74c3645a459aa8b244a69f74d",
			"VO_GILA_500h3_Female_Human_EVENT_NEMESIS_TURN2_01.prefab:66706a81a9665c843a3c30eefaf9fdbe"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060038DD RID: 14557 RVA: 0x00118E06 File Offset: 0x00117006
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_GILFinalBoss);
	}

	// Token: 0x060038DE RID: 14558 RVA: 0x0011E5E0 File Offset: 0x0011C7E0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_60h_Male_HumanGhost_IntroTess_01.prefab:fff42d3944e8bd74bbd966676233a3ee", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_60h_Male_HumanGhost_EmoteResponseTess_01.prefab:6315438179aa5bf48857edee7132976a", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060038DF RID: 14559 RVA: 0x0011E667 File Offset: 0x0011C867
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_60h_Male_HumanGhost_Death_02.prefab:a4d7f96185d3ee04d9c4d84b926b9f07";
	}

	// Token: 0x060038E0 RID: 14560 RVA: 0x0011E66E File Offset: 0x0011C86E
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardID = entity.GetCardId();
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (entity.IsWeapon() && entity.GetATK() >= 4 && (!(cardID == "LOOT_542") || this.m_RandomKingsbaneLines.Count < 1))
		{
			string text = base.PopRandomLineWithChance(this.m_RandomBigWeaponLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(enemyActor, text, 2.5f);
			}
		}
		string a = cardID;
		if (a == "LOOT_542")
		{
			string text2 = base.PopRandomLineWithChance(this.m_RandomKingsbaneLines);
			if (text2 != null)
			{
				yield return base.PlayLineOnlyOnce(enemyActor, text2, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x060038E1 RID: 14561 RVA: 0x0011E684 File Offset: 0x0011C884
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_60h_Male_HumanGhost_HeroPower_01.prefab:52dbc5a597a03034392514d2929f0bb7",
			"VO_GILA_BOSS_60h_Male_HumanGhost_HeroPower_07.prefab:7cfa077bbb9c82b4aa5575c5fe88b994"
		};
	}

	// Token: 0x060038E2 RID: 14562 RVA: 0x0011E6A1 File Offset: 0x0011C8A1
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "AT_070"))
		{
			if (!(cardId == "LOOT_542"))
			{
				if (cardId == "GILA_611")
				{
					yield return base.PlayBossLine(actor, "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayTuskarrRaider_01.prefab:13ac2c5bc7b2dba4f89fe041aacd4811", 2.5f);
				}
			}
			else
			{
				yield return base.PlayBossLine(actor, "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayerKingsblade_01.prefab:fa4a18427dcec41409bb74eac8198200", 2.5f);
			}
		}
		else
		{
			yield return base.PlayBossLine(actor, "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayKragg_02.prefab:5921fc1f19b5f2b479b6e1433b947dd0", 2.5f);
		}
		yield break;
	}

	// Token: 0x060038E3 RID: 14563 RVA: 0x0011E6B7 File Offset: 0x0011C8B7
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayBossLine(actor, "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayPatches_02.prefab:0be9e962c132e8847af72531643812e3", 2.5f);
		}
		yield break;
	}

	// Token: 0x060038E4 RID: 14564 RVA: 0x0011E6CD File Offset: 0x0011C8CD
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		if (turn == 2)
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_500h3_Female_Human_EVENT_NEMESIS_TURN2_01.prefab:66706a81a9665c843a3c30eefaf9fdbe", 2.5f);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_60h_Male_HumanGhost_EventTurn2_01.prefab:295193b74c3645a459aa8b244a69f74d", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x04001DDD RID: 7645
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DDE RID: 7646
	private List<string> m_RandomKingsbaneLines = new List<string>
	{
		"VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayKingsblade_01.prefab:d5916616acd23f348b16805f3ff33dd1",
		"VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayKingsblade_02.prefab:13151159e0811c543a607ae06de474c9"
	};

	// Token: 0x04001DDF RID: 7647
	private List<string> m_RandomBigWeaponLines = new List<string>
	{
		"VO_GILA_BOSS_60h_Male_HumanGhost_EventBigWeapon_01.prefab:b3f3e20c23274f4418d2fbc006c2dc25",
		"VO_GILA_BOSS_60h_Male_HumanGhost_EventBigWeapon_02.prefab:c586c7ae2997be44eb7f613a19f79eca",
		"VO_GILA_BOSS_60h_Male_HumanGhost_EventBigWeapon_03.prefab:263e1671c8b6ca34c8b09a589dd6aa4f"
	};
}
