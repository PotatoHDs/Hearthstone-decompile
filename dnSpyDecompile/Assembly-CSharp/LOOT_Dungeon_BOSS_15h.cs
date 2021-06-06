using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B8 RID: 952
public class LOOT_Dungeon_BOSS_15h : LOOT_Dungeon
{
	// Token: 0x0600362B RID: 13867 RVA: 0x00113E40 File Offset: 0x00112040
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_15h_Male_Human_Intro_01.prefab:eac2e5310989138459bcaccb9cf1e262",
			"VO_LOOTA_BOSS_15h_Male_Human_EmoteResponse_01.prefab:418380d3659570c4c97e6c1c118a9f6f",
			"VO_LOOTA_BOSS_15h_Male_Human_TaggedIn1_01.prefab:2f55e8f736f9f98458fb8f08f58c5a36",
			"VO_LOOTA_BOSS_15h_Male_Human_TaggedIn2_01.prefab:18063202c6aa5c749bf231bfa74ed27c",
			"VO_LOOTA_BOSS_15h_Male_Human_TaggedIn3_01.prefab:239aa3d876cb06b4eab89fc00fd64c22",
			"VO_LOOTA_BOSS_15h_Male_Human_Death_01.prefab:708c935fa2ec89b42bc095e8bf8a221c",
			"VO_LOOTA_BOSS_15h_Male_Human_PartnerDeath_01.prefab:e4fe992eef19428439e269f5ac37b08b",
			"VO_LOOTA_BOSS_15h_Male_Human_DefeatPlayer_01.prefab:83840107361ae9c479e52940ce8edf52",
			"VO_LOOTA_BOSS_32h_Male_Human_EmoteResponse_01.prefab:8ef89068a08ab764d9edd2ffd51fecca",
			"VO_LOOTA_BOSS_32h_Male_Human_TaggedIn1_01.prefab:700ba1a448e654044ac8f9af7982b78f",
			"VO_LOOTA_BOSS_32h_Male_Human_TaggedIn2_01.prefab:3efa9593adabf4c4eb74275eebb477bb",
			"VO_LOOTA_BOSS_32h_Male_Human_TaggedIn3_01.prefab:992d77a6eeaa7f643b22870a0efa0870",
			"VO_LOOTA_BOSS_32h_Male_Human_Death_01.prefab:a79d401349083e54597da55c1b21e511",
			"VO_LOOTA_BOSS_32h_Male_Human_PartnerDeath_01.prefab:9b8fa70ea9b43244e832cc445cada336"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600362C RID: 13868 RVA: 0x00113F34 File Offset: 0x00112134
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x0600362D RID: 13869 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x0600362E RID: 13870 RVA: 0x00113F4C File Offset: 0x0011214C
	protected override string GetBossDeathLine()
	{
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (cardId == "LOOTA_BOSS_15h")
		{
			return "VO_LOOTA_BOSS_15h_Male_Human_Death_01.prefab:708c935fa2ec89b42bc095e8bf8a221c";
		}
		if (cardId == "LOOTA_BOSS_32h")
		{
			return "VO_LOOTA_BOSS_32h_Male_Human_Death_01.prefab:a79d401349083e54597da55c1b21e511";
		}
		return null;
	}

	// Token: 0x0600362F RID: 13871 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003630 RID: 13872 RVA: 0x00113F98 File Offset: 0x00112198
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_15h_Male_Human_Intro_01.prefab:eac2e5310989138459bcaccb9cf1e262", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (cardId == "LOOTA_BOSS_15h")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_15h_Male_Human_EmoteResponse_01.prefab:418380d3659570c4c97e6c1c118a9f6f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			if (cardId == "LOOTA_BOSS_32h")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_32h_Male_Human_EmoteResponse_01.prefab:8ef89068a08ab764d9edd2ffd51fecca", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
	}

	// Token: 0x06003631 RID: 13873 RVA: 0x00114077 File Offset: 0x00112277
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
		Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName("");
		Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).UpdateHeroNameBanner();
		switch (missionEvent)
		{
		case 101:
		{
			int num = 100;
			int num2 = UnityEngine.Random.Range(0, 100);
			if (this.m_GeorgeTaggedInLines.Count != 0 && num >= num2)
			{
				string randomLine = this.m_GeorgeTaggedInLines[UnityEngine.Random.Range(0, this.m_GeorgeTaggedInLines.Count)];
				yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
				this.m_GeorgeTaggedInLines.Remove(randomLine);
				yield return null;
				randomLine = null;
			}
			break;
		}
		case 102:
		{
			int num = 100;
			int num2 = UnityEngine.Random.Range(0, 100);
			if (this.m_KarlTaggedInLines.Count != 0 && num >= num2)
			{
				string randomLine = this.m_KarlTaggedInLines[UnityEngine.Random.Range(0, this.m_KarlTaggedInLines.Count)];
				yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
				this.m_KarlTaggedInLines.Remove(randomLine);
				yield return null;
				randomLine = null;
			}
			break;
		}
		case 103:
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_32h_Male_Human_PartnerDeath_01.prefab:9b8fa70ea9b43244e832cc445cada336", 2.5f);
			break;
		case 104:
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_15h_Male_Human_PartnerDeath_01.prefab:e4fe992eef19428439e269f5ac37b08b", 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x04001D29 RID: 7465
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D2A RID: 7466
	private List<string> m_GeorgeTaggedInLines = new List<string>
	{
		"VO_LOOTA_BOSS_15h_Male_Human_TaggedIn1_01.prefab:2f55e8f736f9f98458fb8f08f58c5a36",
		"VO_LOOTA_BOSS_15h_Male_Human_TaggedIn2_01.prefab:18063202c6aa5c749bf231bfa74ed27c",
		"VO_LOOTA_BOSS_15h_Male_Human_TaggedIn3_01.prefab:239aa3d876cb06b4eab89fc00fd64c22"
	};

	// Token: 0x04001D2B RID: 7467
	private List<string> m_KarlTaggedInLines = new List<string>
	{
		"VO_LOOTA_BOSS_32h_Male_Human_TaggedIn1_01.prefab:700ba1a448e654044ac8f9af7982b78f",
		"VO_LOOTA_BOSS_32h_Male_Human_TaggedIn2_01.prefab:3efa9593adabf4c4eb74275eebb477bb",
		"VO_LOOTA_BOSS_32h_Male_Human_TaggedIn3_01.prefab:992d77a6eeaa7f643b22870a0efa0870"
	};
}
