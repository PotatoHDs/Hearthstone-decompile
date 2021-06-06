using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003F3 RID: 1011
public class GIL_Dungeon_Boss_42h : GIL_Dungeon
{
	// Token: 0x06003839 RID: 14393 RVA: 0x0011BE90 File Offset: 0x0011A090
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_42h_Male_Ogre_Intro_01.prefab:9149ad8f8b7c26945b6801fe5f0f86d5",
			"VO_GILA_BOSS_42h_Male_Ogre_EmoteResponse_01.prefab:b77493c05eb4de74dbd3b21e3359713e",
			"VO_GILA_BOSS_42h_Male_Ogre_Death_02.prefab:97ab39658338f224aa30bf1247c6b61e",
			"VO_GILA_BOSS_42h_Male_Ogre_EventHeroPowerFriendly_01.prefab:f39845f182dbb084889d0ad1aed416ca",
			"VO_GILA_BOSS_42h_Male_Ogre_EventHeroPowerFriendly_02.prefab:9418c5fe0915ea64ea02701df85ffacb",
			"VO_GILA_BOSS_42h_Male_Ogre_EventHeroPowerEnemy_01.prefab:ef150c3ba4a60d5458d9e247362591d2",
			"VO_GILA_BOSS_42h_Male_Ogre_EventHeroPowerEnemy_02.prefab:57607d76779377e4dbc655d6305af41c",
			"VO_GILA_BOSS_42h_Male_Ogre_EventHeroPowerEnemy_03.prefab:cd4e52c1b0db06046966c62b9648b2eb"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600383A RID: 14394 RVA: 0x0011BF40 File Offset: 0x0011A140
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_42h_Male_Ogre_Intro_01.prefab:9149ad8f8b7c26945b6801fe5f0f86d5", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_42h_Male_Ogre_EmoteResponse_01.prefab:b77493c05eb4de74dbd3b21e3359713e", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600383B RID: 14395 RVA: 0x0011BFC7 File Offset: 0x0011A1C7
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_42h_Male_Ogre_Death_02.prefab:97ab39658338f224aa30bf1247c6b61e";
	}

	// Token: 0x0600383C RID: 14396 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600383D RID: 14397 RVA: 0x0011BFCE File Offset: 0x0011A1CE
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x0600383E RID: 14398 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x0600383F RID: 14399 RVA: 0x0011BFE4 File Offset: 0x0011A1E4
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
		if (missionEvent != 101)
		{
			if (missionEvent == 102)
			{
				string text = base.PopRandomLineWithChance(this.m_RandomBossLines);
				if (text != null)
				{
					yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
				}
			}
		}
		else
		{
			string text = base.PopRandomLineWithChance(this.m_RandomPlayerLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001DAD RID: 7597
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DAE RID: 7598
	private List<string> m_RandomPlayerLines = new List<string>
	{
		"VO_GILA_BOSS_42h_Male_Ogre_EventHeroPowerEnemy_01.prefab:ef150c3ba4a60d5458d9e247362591d2",
		"VO_GILA_BOSS_42h_Male_Ogre_EventHeroPowerEnemy_02.prefab:57607d76779377e4dbc655d6305af41c",
		"VO_GILA_BOSS_42h_Male_Ogre_EventHeroPowerEnemy_03.prefab:cd4e52c1b0db06046966c62b9648b2eb"
	};

	// Token: 0x04001DAF RID: 7599
	private List<string> m_RandomBossLines = new List<string>
	{
		"VO_GILA_BOSS_42h_Male_Ogre_EventHeroPowerFriendly_01.prefab:f39845f182dbb084889d0ad1aed416ca",
		"VO_GILA_BOSS_42h_Male_Ogre_EventHeroPowerFriendly_02.prefab:9418c5fe0915ea64ea02701df85ffacb"
	};
}
