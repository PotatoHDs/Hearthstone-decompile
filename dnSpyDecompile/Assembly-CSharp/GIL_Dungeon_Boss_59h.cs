using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000403 RID: 1027
public class GIL_Dungeon_Boss_59h : GIL_Dungeon
{
	// Token: 0x060038D1 RID: 14545 RVA: 0x0011E270 File Offset: 0x0011C470
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_59h_Male_Undead_IntroCrowley_01.prefab:6c5d0cde28d5ee0488f66a0f53999059",
			"VO_GILA_BOSS_59h_Male_Undead_EmoteResponse_01.prefab:a7342a855d2c72749811c6d817d97280",
			"VO_GILA_BOSS_59h_Male_Undead_Death_02.prefab:a462977f734b9d8479f1c4bc7019e058",
			"VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_01.prefab:08179df12eacdaa4cb8791a604bc6b56",
			"VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_02.prefab:15775936c6acd9649897e744543f698e",
			"VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_03.prefab:1c2653cd155740844b4bfb694be5c925",
			"VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_04.prefab:6f1738ac4aaecb54687e68af9f998021",
			"VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_05.prefab:474a3f09f6bec9647aebdbceed42348d",
			"VO_GILA_BOSS_59h_Male_Undead_EventQuickShot_01.prefab:b5070091171cc654eac17b25ecb473c6",
			"VO_GILA_BOSS_59h_Male_Undead_EventQuickShot_02.prefab:142c2555be96a4748ac4e6f89893f4d5",
			"VO_GILA_BOSS_59h_Male_Undead_EventDeadlyShot_01.prefab:c3fb9ce9c84ef1649b7260ebc631464e",
			"VO_GILA_BOSS_59h_Male_Undead_EventDeadlyShot_02.prefab:1ab03dfd398e58a4eac5f176cc912a92",
			"VO_GILA_BOSS_59h_Male_Undead_EventTurn02_01.prefab:a1b990749b36db246ace5dabd2369928",
			"VO_GILA_600h_Male_Worgen_EVENT_NEMESIS_TURN02_01.prefab:081d59f0c0695c649b07f2ffaa2e02fc"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060038D2 RID: 14546 RVA: 0x00118E06 File Offset: 0x00117006
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_GILFinalBoss);
	}

	// Token: 0x060038D3 RID: 14547 RVA: 0x0011E364 File Offset: 0x0011C564
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_59h_Male_Undead_IntroCrowley_01.prefab:6c5d0cde28d5ee0488f66a0f53999059", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_59h_Male_Undead_EmoteResponse_01.prefab:a7342a855d2c72749811c6d817d97280", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060038D4 RID: 14548 RVA: 0x0011E3EB File Offset: 0x0011C5EB
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_59h_Male_Undead_Death_02.prefab:a462977f734b9d8479f1c4bc7019e058";
	}

	// Token: 0x060038D5 RID: 14549 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060038D6 RID: 14550 RVA: 0x0011E3F2 File Offset: 0x0011C5F2
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
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "BRM_013"))
		{
			if (cardId == "EX1_617")
			{
				string text = base.PopRandomLineWithChance(this.m_RandomBossPlaysDeadlyShot);
				if (text != null)
				{
					yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
				}
			}
		}
		else
		{
			string text = base.PopRandomLineWithChance(this.m_RandomBossPlaysQuickShot);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x060038D7 RID: 14551 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060038D8 RID: 14552 RVA: 0x0011E408 File Offset: 0x0011C608
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		if (missionEvent == 101)
		{
			string text = base.PopRandomLineWithChance(this.m_RandomHeroPowerKillLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x060038D9 RID: 14553 RVA: 0x0011E41E File Offset: 0x0011C61E
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		if (turn == 2)
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_59h_Male_Undead_EventTurn02_01.prefab:a1b990749b36db246ace5dabd2369928", 2.5f);
			yield return base.PlayLineOnlyOnce(playerActor, "VO_GILA_600h_Male_Worgen_EVENT_NEMESIS_TURN02_01.prefab:081d59f0c0695c649b07f2ffaa2e02fc", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x04001DD9 RID: 7641
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DDA RID: 7642
	private List<string> m_RandomHeroPowerKillLines = new List<string>
	{
		"VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_01.prefab:08179df12eacdaa4cb8791a604bc6b56",
		"VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_02.prefab:15775936c6acd9649897e744543f698e",
		"VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_03.prefab:1c2653cd155740844b4bfb694be5c925",
		"VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_04.prefab:6f1738ac4aaecb54687e68af9f998021",
		"VO_GILA_BOSS_59h_Male_Undead_HeroPowerKills_05.prefab:474a3f09f6bec9647aebdbceed42348d"
	};

	// Token: 0x04001DDB RID: 7643
	private List<string> m_RandomBossPlaysQuickShot = new List<string>
	{
		"VO_GILA_BOSS_59h_Male_Undead_EventQuickShot_01.prefab:b5070091171cc654eac17b25ecb473c6",
		"VO_GILA_BOSS_59h_Male_Undead_EventQuickShot_02.prefab:142c2555be96a4748ac4e6f89893f4d5"
	};

	// Token: 0x04001DDC RID: 7644
	private List<string> m_RandomBossPlaysDeadlyShot = new List<string>
	{
		"VO_GILA_BOSS_59h_Male_Undead_EventDeadlyShot_01.prefab:c3fb9ce9c84ef1649b7260ebc631464e",
		"VO_GILA_BOSS_59h_Male_Undead_EventDeadlyShot_02.prefab:1ab03dfd398e58a4eac5f176cc912a92"
	};
}
