using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003C2 RID: 962
public class LOOT_Dungeon_BOSS_25h : LOOT_Dungeon
{
	// Token: 0x0600367E RID: 13950 RVA: 0x00114FF0 File Offset: 0x001131F0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_25h_Male_Dragon_Intro_01.prefab:add998ba4e2e01b44aaac7fba9c2b4cc",
			"VO_LOOTA_BOSS_25h_Male_Dragon_EmoteResponse_01.prefab:e90ae3d9090ead040a3b44f8a4e01c4b",
			"VO_LOOTA_BOSS_25h_Male_Dragon_TreasureDestroy1_01.prefab:23e65c02cbddc904cb3eab22458ce562",
			"VO_LOOTA_BOSS_25h_Male_Dragon_TreasureDestroy2_01.prefab:f6385398f90fcc04298333ec6aea9d0b",
			"VO_LOOTA_BOSS_25h_Male_Dragon_TreasureDestroy3_01.prefab:33e551f7421525d42ab6e70ffa3e3177",
			"VO_LOOTA_BOSS_25h_Male_Dragon_TreasureDestroy4_02.prefab:12c9b6c6be374d740b3d1f65ebe63e41",
			"VO_LOOTA_BOSS_25h_Male_Dragon_TreasureDestroy5_01.prefab:d1aebeaf8b55f2b4e9780dfdaa0dc47b",
			"VO_LOOTA_BOSS_25h_Male_Dragon_HeroPower1_01.prefab:65776ccd923a75f40bb012264058ca12",
			"VO_LOOTA_BOSS_25h_Male_Dragon_HeroPower2_01.prefab:60d746ad07b34e341bc1bc84c7a07dd1",
			"VO_LOOTA_BOSS_25h_Male_Dragon_HeroPower3_01.prefab:7de0b19ebe39ca247b8421f748b98525",
			"VO_LOOTA_BOSS_25h_Male_Dragon_HeroPower4_01.prefab:7172dc8c3e6a2f94787abc3e997249f0",
			"VO_LOOTA_BOSS_25h_Male_Dragon_HeroPower5_01.prefab:8610ec1c1ee27094894ec6f10d8b2f88",
			"VO_LOOTA_BOSS_25h_Male_Dragon_Death_01.prefab:663cc8fb06978b2419904c179be13721",
			"VO_LOOTA_BOSS_25h_Male_Dragon_DefeatPlayer_01.prefab:4cb072ca9af2c1d41b2b50ef57bbd7e2",
			"VO_LOOTA_BOSS_25h_Male_Dragon_EventMarinTheFox_01.prefab:7660408d472cffa498431e1c6bf0b6bb"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600367F RID: 13951 RVA: 0x001150F0 File Offset: 0x001132F0
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOOTFinalBoss);
	}

	// Token: 0x06003680 RID: 13952 RVA: 0x00115102 File Offset: 0x00113302
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003681 RID: 13953 RVA: 0x00115118 File Offset: 0x00113318
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_25h_Male_Dragon_HeroPower1_01.prefab:65776ccd923a75f40bb012264058ca12",
			"VO_LOOTA_BOSS_25h_Male_Dragon_HeroPower2_01.prefab:60d746ad07b34e341bc1bc84c7a07dd1",
			"VO_LOOTA_BOSS_25h_Male_Dragon_HeroPower3_01.prefab:7de0b19ebe39ca247b8421f748b98525",
			"VO_LOOTA_BOSS_25h_Male_Dragon_HeroPower4_01.prefab:7172dc8c3e6a2f94787abc3e997249f0",
			"VO_LOOTA_BOSS_25h_Male_Dragon_HeroPower5_01.prefab:8610ec1c1ee27094894ec6f10d8b2f88"
		};
	}

	// Token: 0x06003682 RID: 13954 RVA: 0x00115156 File Offset: 0x00113356
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_25h_Male_Dragon_Death_01.prefab:663cc8fb06978b2419904c179be13721";
	}

	// Token: 0x06003683 RID: 13955 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003684 RID: 13956 RVA: 0x00115160 File Offset: 0x00113360
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_25h_Male_Dragon_Intro_01.prefab:add998ba4e2e01b44aaac7fba9c2b4cc", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_25h_Male_Dragon_EmoteResponse_01.prefab:e90ae3d9090ead040a3b44f8a4e01c4b", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003685 RID: 13957 RVA: 0x001151E7 File Offset: 0x001133E7
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
		if (missionEvent == 101)
		{
			int num = 50;
			int num2 = UnityEngine.Random.Range(0, 100);
			if (this.m_TreasureDestroyedLines.Count != 0 && num >= num2)
			{
				string randomLine = this.m_TreasureDestroyedLines[UnityEngine.Random.Range(0, this.m_TreasureDestroyedLines.Count)];
				yield return base.PlayLineOnlyOnce(enemyActor, randomLine, 2.5f);
				this.m_TreasureDestroyedLines.Remove(randomLine);
				yield return null;
				randomLine = null;
			}
		}
		yield break;
	}

	// Token: 0x06003686 RID: 13958 RVA: 0x001151FD File Offset: 0x001133FD
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
		if (cardId == "LOOT_357")
		{
			yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_25h_Male_Dragon_EventMarinTheFox_01.prefab:7660408d472cffa498431e1c6bf0b6bb", 2.5f);
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001D3B RID: 7483
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D3C RID: 7484
	private List<string> m_TreasureDestroyedLines = new List<string>
	{
		"VO_LOOTA_BOSS_25h_Male_Dragon_TreasureDestroy1_01.prefab:23e65c02cbddc904cb3eab22458ce562",
		"VO_LOOTA_BOSS_25h_Male_Dragon_TreasureDestroy2_01.prefab:f6385398f90fcc04298333ec6aea9d0b",
		"VO_LOOTA_BOSS_25h_Male_Dragon_TreasureDestroy3_01.prefab:33e551f7421525d42ab6e70ffa3e3177",
		"VO_LOOTA_BOSS_25h_Male_Dragon_TreasureDestroy4_02.prefab:12c9b6c6be374d740b3d1f65ebe63e41",
		"VO_LOOTA_BOSS_25h_Male_Dragon_TreasureDestroy5_01.prefab:d1aebeaf8b55f2b4e9780dfdaa0dc47b"
	};
}
