using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003E6 RID: 998
public class GIL_Dungeon_Boss_29h : GIL_Dungeon
{
	// Token: 0x060037C6 RID: 14278 RVA: 0x0011A4E4 File Offset: 0x001186E4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_29h_Female_Satyr_Intro_01.prefab:7ec7479acf5acb74b9e4302ff92736e0",
			"VO_GILA_BOSS_29h_Female_Satyr_EmoteResponse_01.prefab:05b465e15cf38234aa54811463501d0d",
			"VO_GILA_BOSS_29h_Female_Satyr_Death_01.prefab:a601eaae85a47ee47a88c04719e621c1",
			"VO_GILA_BOSS_29h_Female_Satyr_DefeatPlayer_01.prefab:f019545d544dcf94dbec559817062b6e",
			"VO_GILA_BOSS_29h_Female_Satyr_HeroPower_01.prefab:aaf9dbf346ce0e64ab4762569c8becff",
			"VO_GILA_BOSS_29h_Female_Satyr_HeroPower_02.prefab:183ce152b1903cf47934d9689370aab9",
			"VO_GILA_BOSS_29h_Female_Satyr_HeroPower_03.prefab:833d3be257d014d4da2645b4940f01f4",
			"VO_GILA_BOSS_29h_Female_Satyr_HeroPower_04.prefab:c9b2c969d0100a844a457a08ab032b29",
			"VO_GILA_BOSS_29h_Female_Satyr_HeroPower_05.prefab:6ce18bfe1350eab4398c36e1d72742da",
			"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysSmallMinion_01.prefab:5af3535278e2eb2429448aa9e8d03f2c",
			"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysSmallMinion_02.prefab:76e60f5f24349214aa5584a05665d6a2",
			"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysSmallMinion_03.prefab:457cc49a55afe104c96a18083353ff1e",
			"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysEnchantingTune_01.prefab:9fb7303928f352b44bda765d3f497c5b",
			"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysEnchantingTune_02.prefab:ee95531b139acdf4b9db63538a5a2c08",
			"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysEnchantingTune_03.prefab:04d18c6d8f3882a4b9e48a7a40a585ae",
			"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysOldMilitiaHorn_01.prefab:5bdaf92c95c002143884314d0a70588d"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060037C7 RID: 14279 RVA: 0x0011A5EC File Offset: 0x001187EC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_29h_Female_Satyr_Intro_01.prefab:7ec7479acf5acb74b9e4302ff92736e0", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_29h_Female_Satyr_EmoteResponse_01.prefab:05b465e15cf38234aa54811463501d0d", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060037C8 RID: 14280 RVA: 0x0011A673 File Offset: 0x00118873
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_29h_Female_Satyr_Death_01.prefab:a601eaae85a47ee47a88c04719e621c1";
	}

	// Token: 0x060037C9 RID: 14281 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060037CA RID: 14282 RVA: 0x0011A67A File Offset: 0x0011887A
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string cardId = entity.GetCardId();
		if (cardId == "GILA_BOSS_29t")
		{
			string text = base.PopRandomLineWithChance(this.m_TunesLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x060037CB RID: 14283 RVA: 0x0011A690 File Offset: 0x00118890
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_29h_Female_Satyr_HeroPower_01.prefab:aaf9dbf346ce0e64ab4762569c8becff",
			"VO_GILA_BOSS_29h_Female_Satyr_HeroPower_02.prefab:183ce152b1903cf47934d9689370aab9",
			"VO_GILA_BOSS_29h_Female_Satyr_HeroPower_03.prefab:833d3be257d014d4da2645b4940f01f4",
			"VO_GILA_BOSS_29h_Female_Satyr_HeroPower_04.prefab:c9b2c969d0100a844a457a08ab032b29",
			"VO_GILA_BOSS_29h_Female_Satyr_HeroPower_05.prefab:6ce18bfe1350eab4398c36e1d72742da"
		};
	}

	// Token: 0x060037CC RID: 14284 RVA: 0x0011A6CE File Offset: 0x001188CE
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
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
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string a = cardID;
		if (a == "GILA_852a")
		{
			yield return base.PlayBossLine(enemyActor, "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysOldMilitiaHorn_01.prefab:5bdaf92c95c002143884314d0a70588d", 2.5f);
		}
		if (entity.GetATK() <= 1 && entity.IsMinion() && this.m_SmallMinionLines.Count != 0)
		{
			string text = this.m_SmallMinionLines[UnityEngine.Random.Range(0, this.m_SmallMinionLines.Count)];
			this.m_SmallMinionLines.Remove(text);
			yield return base.PlayLineOnlyOnce(enemyActor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x04001D93 RID: 7571
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D94 RID: 7572
	private List<string> m_SmallMinionLines = new List<string>
	{
		"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysSmallMinion_01.prefab:5af3535278e2eb2429448aa9e8d03f2c",
		"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysSmallMinion_02.prefab:76e60f5f24349214aa5584a05665d6a2",
		"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysSmallMinion_03.prefab:457cc49a55afe104c96a18083353ff1e"
	};

	// Token: 0x04001D95 RID: 7573
	private List<string> m_TunesLines = new List<string>
	{
		"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysEnchantingTune_01.prefab:9fb7303928f352b44bda765d3f497c5b",
		"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysEnchantingTune_02.prefab:ee95531b139acdf4b9db63538a5a2c08",
		"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysEnchantingTune_03.prefab:04d18c6d8f3882a4b9e48a7a40a585ae"
	};
}
