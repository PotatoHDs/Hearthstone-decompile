using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_60h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_RandomKingsbaneLines = new List<string> { "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayKingsblade_01.prefab:d5916616acd23f348b16805f3ff33dd1", "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayKingsblade_02.prefab:13151159e0811c543a607ae06de474c9" };

	private List<string> m_RandomBigWeaponLines = new List<string> { "VO_GILA_BOSS_60h_Male_HumanGhost_EventBigWeapon_01.prefab:b3f3e20c23274f4418d2fbc006c2dc25", "VO_GILA_BOSS_60h_Male_HumanGhost_EventBigWeapon_02.prefab:c586c7ae2997be44eb7f613a19f79eca", "VO_GILA_BOSS_60h_Male_HumanGhost_EventBigWeapon_03.prefab:263e1671c8b6ca34c8b09a589dd6aa4f" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_60h_Male_HumanGhost_IntroTess_01.prefab:fff42d3944e8bd74bbd966676233a3ee", "VO_GILA_BOSS_60h_Male_HumanGhost_EmoteResponseTess_01.prefab:6315438179aa5bf48857edee7132976a", "VO_GILA_BOSS_60h_Male_HumanGhost_Death_02.prefab:a4d7f96185d3ee04d9c4d84b926b9f07", "VO_GILA_BOSS_60h_Male_HumanGhost_HeroPower_01.prefab:52dbc5a597a03034392514d2929f0bb7", "VO_GILA_BOSS_60h_Male_HumanGhost_HeroPower_07.prefab:7cfa077bbb9c82b4aa5575c5fe88b994", "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayPatches_02.prefab:0be9e962c132e8847af72531643812e3", "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayKragg_02.prefab:5921fc1f19b5f2b479b6e1433b947dd0", "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayKingsblade_01.prefab:d5916616acd23f348b16805f3ff33dd1", "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayKingsblade_02.prefab:13151159e0811c543a607ae06de474c9", "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayerKingsblade_01.prefab:fa4a18427dcec41409bb74eac8198200",
			"VO_GILA_BOSS_60h_Male_HumanGhost_EventBigWeapon_01.prefab:b3f3e20c23274f4418d2fbc006c2dc25", "VO_GILA_BOSS_60h_Male_HumanGhost_EventBigWeapon_02.prefab:c586c7ae2997be44eb7f613a19f79eca", "VO_GILA_BOSS_60h_Male_HumanGhost_EventBigWeapon_03.prefab:263e1671c8b6ca34c8b09a589dd6aa4f", "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayTuskarrRaider_01.prefab:13ac2c5bc7b2dba4f89fe041aacd4811", "VO_GILA_BOSS_60h_Male_HumanGhost_EventTurn2_01.prefab:295193b74c3645a459aa8b244a69f74d", "VO_GILA_500h3_Female_Human_EVENT_NEMESIS_TURN2_01.prefab:66706a81a9665c843a3c30eefaf9fdbe"
		})
		{
			PreloadSound(item);
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_GILFinalBoss);
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_60h_Male_HumanGhost_IntroTess_01.prefab:fff42d3944e8bd74bbd966676233a3ee", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_60h_Male_HumanGhost_EmoteResponseTess_01.prefab:6315438179aa5bf48857edee7132976a", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_60h_Male_HumanGhost_Death_02.prefab:a4d7f96185d3ee04d9c4d84b926b9f07";
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardID = entity.GetCardId();
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (entity.IsWeapon() && entity.GetATK() >= 4 && (!(cardID == "LOOT_542") || m_RandomKingsbaneLines.Count < 1))
		{
			string text = PopRandomLineWithChance(m_RandomBigWeaponLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(enemyActor, text);
			}
		}
		if (cardID == "LOOT_542")
		{
			string text2 = PopRandomLineWithChance(m_RandomKingsbaneLines);
			if (text2 != null)
			{
				yield return PlayLineOnlyOnce(enemyActor, text2);
			}
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_GILA_BOSS_60h_Male_HumanGhost_HeroPower_01.prefab:52dbc5a597a03034392514d2929f0bb7", "VO_GILA_BOSS_60h_Male_HumanGhost_HeroPower_07.prefab:7cfa077bbb9c82b4aa5575c5fe88b994" };
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "AT_070":
				yield return PlayBossLine(actor, "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayKragg_02.prefab:5921fc1f19b5f2b479b6e1433b947dd0");
				break;
			case "LOOT_542":
				yield return PlayBossLine(actor, "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayerKingsblade_01.prefab:fa4a18427dcec41409bb74eac8198200");
				break;
			case "GILA_611":
				yield return PlayBossLine(actor, "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayTuskarrRaider_01.prefab:13ac2c5bc7b2dba4f89fe041aacd4811");
				break;
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (missionEvent == 101)
		{
			yield return PlayBossLine(actor, "VO_GILA_BOSS_60h_Male_HumanGhost_EventPlayPatches_02.prefab:0be9e962c132e8847af72531643812e3");
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		if (turn == 2)
		{
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(actor, "VO_GILA_500h3_Female_Human_EVENT_NEMESIS_TURN2_01.prefab:66706a81a9665c843a3c30eefaf9fdbe");
			yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_60h_Male_HumanGhost_EventTurn2_01.prefab:295193b74c3645a459aa8b244a69f74d");
			GameState.Get().SetBusy(busy: false);
		}
	}
}
