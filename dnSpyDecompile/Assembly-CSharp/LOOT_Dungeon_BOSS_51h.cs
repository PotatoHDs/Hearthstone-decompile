using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003D6 RID: 982
public class LOOT_Dungeon_BOSS_51h : LOOT_Dungeon
{
	// Token: 0x06003732 RID: 14130 RVA: 0x00117268 File Offset: 0x00115468
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Intro_01.prefab:c513358ed8344364dbf85ee7bb4f8eca",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_EmoteResponse_01.prefab:777b3055ee92eae42b1b27826e242da7",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Song1A_01.prefab:9c6e2abda71d0ec49b5074792dfbf771",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Song1B_01.prefab:b8319bccf79c8b74d92c485cb6dd786c",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Song1C_01.prefab:5f1305268addceb4c9a673711114f60d",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Song1D_01.prefab:b541bc13aa2323348bfcd150bbd16fab",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Song2A_01.prefab:6d84e27d81f910c40bbd4e5ba577a581",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Song2B_01.prefab:8c675c5b762b25846be298354893ceca",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Song2C_01.prefab:633c3a18d8e1b3849a627c6058f5d4e3",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Song2D_01.prefab:df121bc515d9fe940853a5fc6de0aa80",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Song3A_01.prefab:7bd1910702d44ed4f8914cab4375f0d5",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Song3B_01.prefab:db88baed4964457498d39a08d7e852a9",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Song3C_01.prefab:b67a33acc2bab62468194ccd6700dffb",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Song3D_01.prefab:f0d2f6830d3ef74419a6fddfdb383faf",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_KillingBlowReady_01.prefab:df8c2af5e5f7f4d4584ff6c6a87dfea6",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Death_01.prefab:e5c8b619095374542bac028ed3654007",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_DefeatPlayer_02.prefab:8ae8f6b7e7ae0a2479e671cc7129bd7a",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_StealDoomsayer_01.prefab:44f418a3c657052439d81529f24d64ed",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_StealMinstrel_01.prefab:8cfb6f7ad88bd1443b6047f37bd70954",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_EventBoombox_01.prefab:d031062f6c4754b42a8a0975a1b221a1",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_01.prefab:56af47117277b9e429bf3c7e59633d18",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_02.prefab:c824cad0fcdc132409358f46526f2610",
			"VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_03.prefab:b81beae0c979cdf4c8fcc651c5dd9dd3",
			"RussellTheBard_Death_Underlay.prefab:8d76a143441379e40a36cb5b7c84b9b9",
			"RussellTheBard_Intro_Underlay.prefab:19ca7e93a4a3ef541a154a5dd830c1a7",
			"RussellTheBard_KillingBlow_Underlay.prefab:fd10c20e6e25f2647b75b9f79e633656",
			"RussellTheBard_Short_Strum_Underlay.prefab:09e52281d7049da458f12cb79ee395da",
			"RussellTheBard_Song_Underlay_part1.prefab:ea1958f63cb2e40469adc2c5803c061e",
			"RussellTheBard_Song_Underlay_part2.prefab:f3dd880fa456f6e4aa48b7af55ddd2c6",
			"RussellTheBard_Song3A_01_Underlay.prefab:ce68b6200ed0a7543acb8776ee1a78d7",
			"RussellTheBard_Song3B_01_Underlay.prefab:eb9516af5cca1cf4c95f3455ef3a7fc1",
			"RussellTheBard_Song3C_01_Underlay.prefab:a69e44df16d21b548becbed9fe9cfb03",
			"RussellTheBard_Song3D_01_Underlay.prefab:08d078248c7b20647aa0cbf0b2bf79e4"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003733 RID: 14131 RVA: 0x0011742C File Offset: 0x0011562C
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
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (this.m_IdleSongLines.Count == 0)
		{
			return;
		}
		string line = this.m_IdleSongLines[0];
		string soundPath = this.m_IdleUnderlay[0];
		this.m_IdleSongLines.RemoveAt(0);
		this.m_IdleUnderlay.RemoveAt(0);
		base.PlaySound(soundPath, 1f, true, false);
		Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
	}

	// Token: 0x06003734 RID: 14132 RVA: 0x001174DF File Offset: 0x001156DF
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003735 RID: 14133 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003736 RID: 14134 RVA: 0x001174F5 File Offset: 0x001156F5
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_51h_Male_Dwarf_Death_01.prefab:e5c8b619095374542bac028ed3654007";
	}

	// Token: 0x06003737 RID: 14135 RVA: 0x001174FC File Offset: 0x001156FC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			base.PlaySound("RussellTheBard_Intro_Underlay.prefab:19ca7e93a4a3ef541a154a5dd830c1a7", 1f, true, false);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_51h_Male_Dwarf_Intro_01.prefab:c513358ed8344364dbf85ee7bb4f8eca", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			base.PlaySound("RussellTheBard_Short_Strum_Underlay.prefab:09e52281d7049da458f12cb79ee395da", 1f, true, false);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_51h_Male_Dwarf_EmoteResponse_01.prefab:777b3055ee92eae42b1b27826e242da7", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003738 RID: 14136 RVA: 0x001175A7 File Offset: 0x001157A7
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		switch (missionEvent)
		{
		case 101:
			base.PlaySound("RussellTheBard_Short_Strum_Underlay.prefab:09e52281d7049da458f12cb79ee395da", 1f, true, false);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_51h_Male_Dwarf_StealDoomsayer_01.prefab:44f418a3c657052439d81529f24d64ed", 2.5f);
			break;
		case 102:
			base.PlaySound("RussellTheBard_Short_Strum_Underlay.prefab:09e52281d7049da458f12cb79ee395da", 1f, true, false);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_51h_Male_Dwarf_StealMinstrel_01.prefab:8cfb6f7ad88bd1443b6047f37bd70954", 2.5f);
			break;
		case 103:
		{
			int num = 100;
			int num2 = UnityEngine.Random.Range(0, 100);
			if (this.m_heroPowerLines.Count != 0 && num >= num2)
			{
				string randomLine = this.m_heroPowerLines[UnityEngine.Random.Range(0, this.m_heroPowerLines.Count)];
				base.PlaySound("RussellTheBard_Short_Strum_Underlay.prefab:09e52281d7049da458f12cb79ee395da", 1f, true, false);
				yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
				this.m_heroPowerLines.Remove(randomLine);
				yield return null;
				randomLine = null;
			}
			break;
		}
		}
		yield break;
	}

	// Token: 0x06003739 RID: 14137 RVA: 0x001175BD File Offset: 0x001157BD
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
		if (cardId == "LOOTA_838")
		{
			base.PlaySound("RussellTheBard_Short_Strum_Underlay.prefab:09e52281d7049da458f12cb79ee395da", 1f, true, false);
			yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_51h_Male_Dwarf_EventBoombox_01.prefab:d031062f6c4754b42a8a0975a1b221a1", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001D57 RID: 7511
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D58 RID: 7512
	private List<string> m_heroPowerLines = new List<string>
	{
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_01.prefab:56af47117277b9e429bf3c7e59633d18",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_02.prefab:c824cad0fcdc132409358f46526f2610",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_03.prefab:b81beae0c979cdf4c8fcc651c5dd9dd3"
	};

	// Token: 0x04001D59 RID: 7513
	private List<string> m_IdleSongLines = new List<string>
	{
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Song1A_01.prefab:9c6e2abda71d0ec49b5074792dfbf771",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Song1B_01.prefab:b8319bccf79c8b74d92c485cb6dd786c",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Song1C_01.prefab:5f1305268addceb4c9a673711114f60d",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Song1D_01.prefab:b541bc13aa2323348bfcd150bbd16fab",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Song2A_01.prefab:6d84e27d81f910c40bbd4e5ba577a581",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Song2B_01.prefab:8c675c5b762b25846be298354893ceca",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Song2C_01.prefab:633c3a18d8e1b3849a627c6058f5d4e3",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Song2D_01.prefab:df121bc515d9fe940853a5fc6de0aa80",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Song3A_01.prefab:7bd1910702d44ed4f8914cab4375f0d5",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Song3B_01.prefab:db88baed4964457498d39a08d7e852a9",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Song3C_01.prefab:b67a33acc2bab62468194ccd6700dffb",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_Song3D_01.prefab:f0d2f6830d3ef74419a6fddfdb383faf",
		"VO_LOOTA_BOSS_51h_Male_Dwarf_KillingBlowReady_01.prefab:df8c2af5e5f7f4d4584ff6c6a87dfea6"
	};

	// Token: 0x04001D5A RID: 7514
	private List<string> m_IdleUnderlay = new List<string>
	{
		"RussellTheBard_Song_Underlay_part1.prefab:ea1958f63cb2e40469adc2c5803c061e",
		"RussellTheBard_Song_Underlay_part2.prefab:f3dd880fa456f6e4aa48b7af55ddd2c6",
		"RussellTheBard_Song_Underlay_part1.prefab:ea1958f63cb2e40469adc2c5803c061e",
		"RussellTheBard_Song_Underlay_part2.prefab:f3dd880fa456f6e4aa48b7af55ddd2c6",
		"RussellTheBard_Song_Underlay_part1.prefab:ea1958f63cb2e40469adc2c5803c061e",
		"RussellTheBard_Song_Underlay_part2.prefab:f3dd880fa456f6e4aa48b7af55ddd2c6",
		"RussellTheBard_Song_Underlay_part1.prefab:ea1958f63cb2e40469adc2c5803c061e",
		"RussellTheBard_Song_Underlay_part2.prefab:f3dd880fa456f6e4aa48b7af55ddd2c6",
		"RussellTheBard_Song3A_01_Underlay.prefab:ce68b6200ed0a7543acb8776ee1a78d7",
		"RussellTheBard_Song3B_01_Underlay.prefab:eb9516af5cca1cf4c95f3455ef3a7fc1",
		"RussellTheBard_Song3C_01_Underlay.prefab:a69e44df16d21b548becbed9fe9cfb03",
		"RussellTheBard_Song3D_01_Underlay.prefab:08d078248c7b20647aa0cbf0b2bf79e4",
		"RussellTheBard_KillingBlow_Underlay.prefab:fd10c20e6e25f2647b75b9f79e633656"
	};
}
