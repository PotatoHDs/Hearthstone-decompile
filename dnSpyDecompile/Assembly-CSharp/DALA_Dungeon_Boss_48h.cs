using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200045D RID: 1117
public class DALA_Dungeon_Boss_48h : DALA_Dungeon
{
	// Token: 0x06003CA2 RID: 15522 RVA: 0x0013C330 File Offset: 0x0013A530
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_BossBigDemon_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_BossCalloftheVoid_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_BossVoidcaller_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_02,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_03,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_Death_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_DefeatPlayer_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_EmoteResponse_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPower_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPower_02,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPower_03,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPower_04,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPower_05,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_02,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_Idle_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_Idle_02,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_Idle_03,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_Intro_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_IntroGeorge_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_IntroTekahn_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_PlayerRenounceDarkness_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_PlayerTwistingNether_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_PlayerVoidLord_01,
			DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_PlayerVoidShift_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003CA3 RID: 15523 RVA: 0x0013C534 File Offset: 0x0013A734
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_EmoteResponse_01;
	}

	// Token: 0x06003CA4 RID: 15524 RVA: 0x0013C56C File Offset: 0x0013A76C
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_48h.m_IdleLines;
	}

	// Token: 0x06003CA5 RID: 15525 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003CA6 RID: 15526 RVA: 0x0013C574 File Offset: 0x0013A774
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Tekahn")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_IntroTekahn_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Squeamlish")
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

	// Token: 0x06003CA7 RID: 15527 RVA: 0x0013C69A File Offset: 0x0013A89A
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_BossBigDemon_01, 2.5f);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_48h.m_HeroPower);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_48h.m_HeroPowerBig);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003CA8 RID: 15528 RVA: 0x0013C6B0 File Offset: 0x0013A8B0
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
		if (!(cardId == "OG_118"))
		{
			if (!(cardId == "EX1_312"))
			{
				if (!(cardId == "LOOT_368"))
				{
					if (cardId == "DALA_BOSS_48t")
					{
						yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_PlayerVoidShift_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_PlayerVoidLord_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_PlayerTwistingNether_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_PlayerRenounceDarkness_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003CA9 RID: 15529 RVA: 0x0013C6C6 File Offset: 0x0013A8C6
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "EX1_181"))
		{
			if (!(cardId == "FP1_022"))
			{
				if (cardId == "DALA_BOSS_48t")
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_48h.m_BossVoidShift);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_BossVoidcaller_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_BossCalloftheVoid_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002654 RID: 9812
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_BossBigDemon_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_BossBigDemon_01.prefab:540608faa9951d0438c1072432f3101f");

	// Token: 0x04002655 RID: 9813
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_BossCalloftheVoid_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_BossCalloftheVoid_01.prefab:8b729ca0f3d81fa41ac740067e0b6a79");

	// Token: 0x04002656 RID: 9814
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_BossVoidcaller_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_BossVoidcaller_01.prefab:32e4f1e234cf2e14aa46a0bff14cec79");

	// Token: 0x04002657 RID: 9815
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_01.prefab:0f426d7c9d114e54baf7a93ba04128de");

	// Token: 0x04002658 RID: 9816
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_02 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_02.prefab:ab8366f4da6552046b2cb39c1c973c75");

	// Token: 0x04002659 RID: 9817
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_03 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_03.prefab:72e216b521811f4468a80469404271d5");

	// Token: 0x0400265A RID: 9818
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_Death_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_Death_01.prefab:e31e7fc2bff9d0340aca7a9b7bbb6cdf");

	// Token: 0x0400265B RID: 9819
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_DefeatPlayer_01.prefab:95da793bc82ba6d4ab851daf7fed9aa4");

	// Token: 0x0400265C RID: 9820
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_EmoteResponse_01.prefab:bd98f103e89e3f848aa078e4e470d2fd");

	// Token: 0x0400265D RID: 9821
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPower_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPower_01.prefab:2746848f84ede83439c4e33a9949e72c");

	// Token: 0x0400265E RID: 9822
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPower_02 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPower_02.prefab:646b1065715e34447a0588b9e4f478cc");

	// Token: 0x0400265F RID: 9823
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPower_03 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPower_03.prefab:6a540e539e081644c822b83072ee633c");

	// Token: 0x04002660 RID: 9824
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPower_04 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPower_04.prefab:0fd7034e9bff5d04dbeb9c19db4d6d92");

	// Token: 0x04002661 RID: 9825
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPower_05 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPower_05.prefab:fcd54861bab56ad449bd23c4b7871eb0");

	// Token: 0x04002662 RID: 9826
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_01.prefab:d83636839190ef44d91b1c3f82768340");

	// Token: 0x04002663 RID: 9827
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_02 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_02.prefab:c3841504c31aec94a9282ce005fb9b8b");

	// Token: 0x04002664 RID: 9828
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_Idle_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_Idle_01.prefab:bdba1379ecac54746b11124e07f916cd");

	// Token: 0x04002665 RID: 9829
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_Idle_02 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_Idle_02.prefab:eba030fd7f2f91a40b78e1befa150837");

	// Token: 0x04002666 RID: 9830
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_Idle_03 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_Idle_03.prefab:63ac32bf12427b94695650e5db376401");

	// Token: 0x04002667 RID: 9831
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_Intro_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_Intro_01.prefab:1a003f3c97d47ea4b927ef393a0b6f43");

	// Token: 0x04002668 RID: 9832
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_IntroGeorge_01.prefab:e1199523d54583846a515a81b7bffef7");

	// Token: 0x04002669 RID: 9833
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_IntroTekahn_01.prefab:35fd955540a09624d976ec2a7db8a603");

	// Token: 0x0400266A RID: 9834
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_PlayerRenounceDarkness_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_PlayerRenounceDarkness_01.prefab:100ee9cac794a78488efe09158a0a45b");

	// Token: 0x0400266B RID: 9835
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_PlayerTwistingNether_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_PlayerTwistingNether_01.prefab:4a336fab02ace6145bce4cde6641339a");

	// Token: 0x0400266C RID: 9836
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_PlayerVoidLord_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_PlayerVoidLord_01.prefab:8db7671dc135c8f4c9aeecb029f70000");

	// Token: 0x0400266D RID: 9837
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_PlayerVoidShift_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_PlayerVoidShift_01.prefab:a134616a98fd3f94998ce2b53fa74396");

	// Token: 0x0400266E RID: 9838
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x0400266F RID: 9839
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_Idle_01,
		DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_Idle_02,
		DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_Idle_03
	};

	// Token: 0x04002670 RID: 9840
	private static List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPower_01,
		DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPower_02,
		DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPower_03,
		DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPower_04,
		DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPower_05
	};

	// Token: 0x04002671 RID: 9841
	private static List<string> m_HeroPowerBig = new List<string>
	{
		DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_01,
		DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_02
	};

	// Token: 0x04002672 RID: 9842
	private static List<string> m_BossVoidShift = new List<string>
	{
		DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_01,
		DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_02,
		DALA_Dungeon_Boss_48h.VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_03
	};
}
