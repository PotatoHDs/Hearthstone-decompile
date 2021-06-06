using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200044A RID: 1098
public class DALA_Dungeon_Boss_29h : DALA_Dungeon
{
	// Token: 0x06003BB3 RID: 15283 RVA: 0x001367EC File Offset: 0x001349EC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_Death_01,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_DefeatPlayer_01,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_EmoteResponse_01,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_01,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_02,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_03,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_04,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_01,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_02,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_Idle_01,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_Idle_02,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_Idle_03,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_Intro_01,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_IntroChu_01,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_IntroRakanishu_01,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_PlayerElemental_01,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_PlayerPickaxe_01,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_01,
			DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003BB4 RID: 15284 RVA: 0x00136980 File Offset: 0x00134B80
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_EmoteResponse_01;
	}

	// Token: 0x06003BB5 RID: 15285 RVA: 0x001369B8 File Offset: 0x00134BB8
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_29h.m_IdleLines;
	}

	// Token: 0x06003BB6 RID: 15286 RVA: 0x001369C0 File Offset: 0x00134BC0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_IntroChu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		else
		{
			if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x06003BB7 RID: 15287 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003BB8 RID: 15288 RVA: 0x00136AD9 File Offset: 0x00134CD9
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_29h.m_HeroPower);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_29h.m_HeroPowerRock);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_29h.m_PlayerSmallElemental);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_PlayerElemental_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003BB9 RID: 15289 RVA: 0x00136AEF File Offset: 0x00134CEF
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
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (cardId == "LOOTA_835")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_PlayerPickaxe_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003BBA RID: 15290 RVA: 0x00136B05 File Offset: 0x00134D05
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x040024AE RID: 9390
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_Death_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_Death_01.prefab:94c9f11b845b3ab4780730bb735f7caa");

	// Token: 0x040024AF RID: 9391
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_DefeatPlayer_01.prefab:4e5ff81e78229794d990ca9042e6a589");

	// Token: 0x040024B0 RID: 9392
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_EmoteResponse_01.prefab:4582b3e9dcdffe34b8421018884d6f74");

	// Token: 0x040024B1 RID: 9393
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_01.prefab:9e35fc7f8fc6f0b4a9721971b787465a");

	// Token: 0x040024B2 RID: 9394
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_02 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_02.prefab:c026f7d12961ddf469a8dfc368f6381f");

	// Token: 0x040024B3 RID: 9395
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_03 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_03.prefab:ac262729856029d4981645428564161c");

	// Token: 0x040024B4 RID: 9396
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_04 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_04.prefab:4b2a11130918cc440b4fb06edd7ca083");

	// Token: 0x040024B5 RID: 9397
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_01.prefab:3596b6ff6a67ebf41a92c35036fde4ba");

	// Token: 0x040024B6 RID: 9398
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_02 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_02.prefab:6f026439664bf2a459d3c8f9f99ea78c");

	// Token: 0x040024B7 RID: 9399
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_Idle_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_Idle_01.prefab:16ae9ef60444a1241acde3d6ea81099c");

	// Token: 0x040024B8 RID: 9400
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_Idle_02 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_Idle_02.prefab:f826221137cae434984b543e9ac5506b");

	// Token: 0x040024B9 RID: 9401
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_Idle_03 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_Idle_03.prefab:414bbf0c5737b434fbad16e8167ef250");

	// Token: 0x040024BA RID: 9402
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_Intro_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_Intro_01.prefab:5f689cf34bb792340bc7903e34ef1ba9");

	// Token: 0x040024BB RID: 9403
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_IntroChu_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_IntroChu_01.prefab:4d981f3d19de5284699c385313e5e347");

	// Token: 0x040024BC RID: 9404
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_IntroRakanishu_01.prefab:ba36cc9555cfd204db5ec7d13ec4b42b");

	// Token: 0x040024BD RID: 9405
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_PlayerElemental_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_PlayerElemental_01.prefab:d545a3a4c99a17d4dbcfe0a396405c97");

	// Token: 0x040024BE RID: 9406
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_PlayerPickaxe_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_PlayerPickaxe_01.prefab:20580efab6735ff498ab1f7b69c2d421");

	// Token: 0x040024BF RID: 9407
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_01.prefab:ca8980c823164bc44bf1cd28bafd7f20");

	// Token: 0x040024C0 RID: 9408
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_02 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_02.prefab:ce6f05aa9e4b90241a5a70a5df9505e5");

	// Token: 0x040024C1 RID: 9409
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_Idle_01,
		DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_Idle_02,
		DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_Idle_03
	};

	// Token: 0x040024C2 RID: 9410
	private static List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_01,
		DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_02,
		DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_03,
		DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_04
	};

	// Token: 0x040024C3 RID: 9411
	private static List<string> m_HeroPowerRock = new List<string>
	{
		DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_01,
		DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_02
	};

	// Token: 0x040024C4 RID: 9412
	private static List<string> m_PlayerSmallElemental = new List<string>
	{
		DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_01,
		DALA_Dungeon_Boss_29h.VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_02
	};

	// Token: 0x040024C5 RID: 9413
	private HashSet<string> m_playedLines = new HashSet<string>();
}
