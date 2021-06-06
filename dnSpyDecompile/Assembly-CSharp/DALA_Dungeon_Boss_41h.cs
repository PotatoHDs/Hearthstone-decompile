using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000456 RID: 1110
public class DALA_Dungeon_Boss_41h : DALA_Dungeon
{
	// Token: 0x06003C4A RID: 15434 RVA: 0x0013A37C File Offset: 0x0013857C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_BossKillCommand_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_BossUnleashtheHounds_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_Death_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_DefeatPlayer_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_EmoteResponse_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_02,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_04,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_05,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_Idle_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_Idle_02,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_Idle_03,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_Intro_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_IntroChu_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_IntroGeorge_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_IntroKriziki_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_IntroMongrel_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_IntroRakanishu_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_IntroTekahn_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_IntroVessina_02,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_Misc_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_PlayerBeast_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_PlayerBigGameHunter_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_PlayerHitsFirst_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_PlayerHunterWeapon_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_PlayerHyperBlaster_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003C4B RID: 15435 RVA: 0x0013A580 File Offset: 0x00138780
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_EmoteResponse_01;
	}

	// Token: 0x06003C4C RID: 15436 RVA: 0x0013A5B8 File Offset: 0x001387B8
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_41h.m_IdleLines;
	}

	// Token: 0x06003C4D RID: 15437 RVA: 0x0013A5C0 File Offset: 0x001387C0
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_01,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_02,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_04,
			DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_05
		};
	}

	// Token: 0x06003C4E RID: 15438 RVA: 0x0013A614 File Offset: 0x00138814
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_IntroChu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_IntroMongrel_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Vessina")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_IntroVessina_02, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Kriziki" && cardId != "DALA_Rakanishu" && cardId != "DALA_Tekahn" && cardId != "DALA_Eudora")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
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

	// Token: 0x06003C4F RID: 15439 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003C50 RID: 15440 RVA: 0x0013A7D9 File Offset: 0x001389D9
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_PlayerBeast_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_PlayerHitsFirst_01, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_PlayerHunterWeapon_01, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_Misc_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003C51 RID: 15441 RVA: 0x0013A7EF File Offset: 0x001389EF
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (!(cardId == "EX1_005"))
		{
			if (cardId == "DALA_723")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_PlayerHyperBlaster_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_PlayerBigGameHunter_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003C52 RID: 15442 RVA: 0x0013A805 File Offset: 0x00138A05
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
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
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "EX1_539"))
		{
			if (cardId == "EX1_538")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_BossUnleashtheHounds_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_BossKillCommand_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040025BF RID: 9663
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_BossKillCommand_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_BossKillCommand_01.prefab:f4bdc2ab157fd73458d08057e8bfb920");

	// Token: 0x040025C0 RID: 9664
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_BossUnleashtheHounds_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_BossUnleashtheHounds_01.prefab:2ebb5814bf5f1ff4c9b0bf1567367f44");

	// Token: 0x040025C1 RID: 9665
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_Death_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_Death_01.prefab:e5e7424969182cb45818ff64e4c328b3");

	// Token: 0x040025C2 RID: 9666
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_DefeatPlayer_01.prefab:953fdc9700382ac439ad4dbb5b2e6c34");

	// Token: 0x040025C3 RID: 9667
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_EmoteResponse_01.prefab:5877e5a049f4d664786defe3694ae21a");

	// Token: 0x040025C4 RID: 9668
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_01.prefab:ed45eae2d303bd14f857b02a5ff232cf");

	// Token: 0x040025C5 RID: 9669
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_02 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_02.prefab:6a8badd8657aeb14a9c4aa6e6364f9fb");

	// Token: 0x040025C6 RID: 9670
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_04 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_04.prefab:bba06439cb597654aa0aaa36258ef7b9");

	// Token: 0x040025C7 RID: 9671
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_05 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_HeroPower_05.prefab:b65ce7482ef35c64b80790840910dd8f");

	// Token: 0x040025C8 RID: 9672
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_Idle_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_Idle_01.prefab:4c2744462214cec419ab6a48fde7923e");

	// Token: 0x040025C9 RID: 9673
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_Idle_02 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_Idle_02.prefab:af4832b2d4ac2cf4aab5f6bca8645075");

	// Token: 0x040025CA RID: 9674
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_Idle_03 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_Idle_03.prefab:2a4b262db6a8a4c4e893c11866acbd87");

	// Token: 0x040025CB RID: 9675
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_Intro_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_Intro_01.prefab:d819c9261cf3869408d3a7e278d7579e");

	// Token: 0x040025CC RID: 9676
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_IntroChu_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_IntroChu_01.prefab:1714e092db43fd2489d8b062af1f8558");

	// Token: 0x040025CD RID: 9677
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_IntroGeorge_01.prefab:f240a34e273e4674aaa1f8552db887db");

	// Token: 0x040025CE RID: 9678
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_IntroKriziki_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_IntroKriziki_01.prefab:a48abc5898681454780fa2a3dcf1b0cd");

	// Token: 0x040025CF RID: 9679
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_IntroMongrel_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_IntroMongrel_01.prefab:8245fc02da8852945bc65bed142b3bad");

	// Token: 0x040025D0 RID: 9680
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_IntroRakanishu_01.prefab:839de3fdc28ffec448f64173a0348cbc");

	// Token: 0x040025D1 RID: 9681
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_IntroTekahn_01.prefab:849f53e65a54d40499e26d92ae40435e");

	// Token: 0x040025D2 RID: 9682
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_IntroVessina_02 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_IntroVessina_02.prefab:378a6799d75468e48b3800debfb66bf3");

	// Token: 0x040025D3 RID: 9683
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_Misc_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_Misc_01.prefab:6498676d3c093b34c93c2e603fda30fe");

	// Token: 0x040025D4 RID: 9684
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_PlayerBeast_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_PlayerBeast_01.prefab:80a3a5dc7cad2ea438c9bded691f4f03");

	// Token: 0x040025D5 RID: 9685
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_PlayerBigGameHunter_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_PlayerBigGameHunter_01.prefab:02f33ec39a1ee494b871d26e2af30e1c");

	// Token: 0x040025D6 RID: 9686
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_PlayerHitsFirst_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_PlayerHitsFirst_01.prefab:72c8e6c081de5ff449d742fa7cfe474a");

	// Token: 0x040025D7 RID: 9687
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_PlayerHunterWeapon_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_PlayerHunterWeapon_01.prefab:cbb07827eacd19840b279ed47b1f6580");

	// Token: 0x040025D8 RID: 9688
	private static readonly AssetReference VO_DALA_BOSS_41h_Male_Dwarf_PlayerHyperBlaster_01 = new AssetReference("VO_DALA_BOSS_41h_Male_Dwarf_PlayerHyperBlaster_01.prefab:7c15a2a15fbe25d4eb184dd39e18319b");

	// Token: 0x040025D9 RID: 9689
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_Idle_01,
		DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_Idle_02,
		DALA_Dungeon_Boss_41h.VO_DALA_BOSS_41h_Male_Dwarf_Idle_03
	};

	// Token: 0x040025DA RID: 9690
	private HashSet<string> m_playedLines = new HashSet<string>();
}
