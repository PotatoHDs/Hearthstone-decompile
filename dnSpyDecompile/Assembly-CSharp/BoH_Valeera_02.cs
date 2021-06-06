using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000555 RID: 1365
public class BoH_Valeera_02 : BoH_Valeera_Dungeon
{
	// Token: 0x06004B61 RID: 19297 RVA: 0x0018F7A4 File Offset: 0x0018D9A4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2EmoteResponse_01,
			BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2ExchangeA_02,
			BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2ExchangeB_02,
			BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2ExchangeC_01,
			BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2HeroPower_01,
			BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2HeroPower_02,
			BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2HeroPower_03,
			BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Idle_01,
			BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Idle_02,
			BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Idle_03,
			BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Intro_01,
			BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Loss_01,
			BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Victory_02,
			BoH_Valeera_02.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2ExchangeA_01,
			BoH_Valeera_02.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2ExchangeB_01,
			BoH_Valeera_02.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2ExchangeC_02,
			BoH_Valeera_02.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2Intro_01,
			BoH_Valeera_02.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2Victory_01,
			BoH_Valeera_02.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2Victory_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004B62 RID: 19298 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004B63 RID: 19299 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004B64 RID: 19300 RVA: 0x0018F938 File Offset: 0x0018DB38
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Valeera_02.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2Intro_01);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004B65 RID: 19301 RVA: 0x0018F947 File Offset: 0x0018DB47
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004B66 RID: 19302 RVA: 0x0018F94F File Offset: 0x0018DB4F
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004B67 RID: 19303 RVA: 0x0018F957 File Offset: 0x0018DB57
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_TRL;
		this.m_standardEmoteResponseLine = BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2EmoteResponse_01;
	}

	// Token: 0x06004B68 RID: 19304 RVA: 0x0018F97C File Offset: 0x0018DB7C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004B69 RID: 19305 RVA: 0x0018FA04 File Offset: 0x0018DC04
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield break;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 102)
		{
			if (missionEvent != 504)
			{
				if (missionEvent != 507)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Loss_01, 2.5f);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_02.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2Victory_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Victory_02, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_02.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2Victory_03, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_02.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2ExchangeA_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004B6A RID: 19306 RVA: 0x0018FA1A File Offset: 0x0018DC1A
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004B6B RID: 19307 RVA: 0x0018FA30 File Offset: 0x0018DC30
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004B6C RID: 19308 RVA: 0x0018FA46 File Offset: 0x0018DC46
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 9)
		{
			if (turn == 13)
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2ExchangeC_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_02.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2ExchangeC_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_02.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2ExchangeB_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2ExchangeB_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003FC3 RID: 16323
	private static readonly AssetReference VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2EmoteResponse_01.prefab:edcc9389fc36fc04795482d021a4db69");

	// Token: 0x04003FC4 RID: 16324
	private static readonly AssetReference VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2ExchangeA_02 = new AssetReference("VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2ExchangeA_02.prefab:a18886530f128194a8e6c9cd551e15b2");

	// Token: 0x04003FC5 RID: 16325
	private static readonly AssetReference VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2ExchangeB_02.prefab:b60465c7d85c5d54db8deaaa6499dba8");

	// Token: 0x04003FC6 RID: 16326
	private static readonly AssetReference VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2ExchangeC_01.prefab:726e0df8f25c2db4d91a5b48fc425f31");

	// Token: 0x04003FC7 RID: 16327
	private static readonly AssetReference VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2HeroPower_01.prefab:88561e5cb1c2f334ba140bc4b096dd8a");

	// Token: 0x04003FC8 RID: 16328
	private static readonly AssetReference VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2HeroPower_02.prefab:a11ad0c7dadfaa04095e2c7ca2f6324c");

	// Token: 0x04003FC9 RID: 16329
	private static readonly AssetReference VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2HeroPower_03.prefab:1d39789c1bed6484d9af99d353465be8");

	// Token: 0x04003FCA RID: 16330
	private static readonly AssetReference VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Idle_01.prefab:9ab259f6312db614e90996f5f3847e48");

	// Token: 0x04003FCB RID: 16331
	private static readonly AssetReference VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Idle_02.prefab:ce15192b7c1cbe54aa0c3fe008985fd2");

	// Token: 0x04003FCC RID: 16332
	private static readonly AssetReference VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Idle_03.prefab:4431015f99b46d54ca86cd4e968b3343");

	// Token: 0x04003FCD RID: 16333
	private static readonly AssetReference VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Intro_01.prefab:792271b57c013194d929b312ddee74db");

	// Token: 0x04003FCE RID: 16334
	private static readonly AssetReference VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Loss_01 = new AssetReference("VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Loss_01.prefab:e80f100bf7ee29046ad65d3a85244618");

	// Token: 0x04003FCF RID: 16335
	private static readonly AssetReference VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Victory_02 = new AssetReference("VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Victory_02.prefab:1e95c910ff896224fb1880208d36798e");

	// Token: 0x04003FD0 RID: 16336
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2ExchangeA_01.prefab:e94ad0fd247aabe46abd9f9e93eb007a");

	// Token: 0x04003FD1 RID: 16337
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2ExchangeB_01.prefab:3988a17a277599f41a6fd18722430454");

	// Token: 0x04003FD2 RID: 16338
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2ExchangeC_02.prefab:ecc7c3cd7f6b36e43927ef2fb4452f0a");

	// Token: 0x04003FD3 RID: 16339
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2Intro_01.prefab:fad09fc626a9b514ba7f4ae60911f519");

	// Token: 0x04003FD4 RID: 16340
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2Victory_01.prefab:4991e283ef256ca44b0ac1e45bce9ebe");

	// Token: 0x04003FD5 RID: 16341
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2Victory_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission2Victory_03.prefab:d4ae3a322b1b85346808a2e8c311c449");

	// Token: 0x04003FD6 RID: 16342
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2HeroPower_01,
		BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2HeroPower_02,
		BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2HeroPower_03
	};

	// Token: 0x04003FD7 RID: 16343
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Idle_01,
		BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Idle_02,
		BoH_Valeera_02.VO_Story_Hero_Helka_Female_Tauren_Story_Valeera_Mission2Idle_03
	};

	// Token: 0x04003FD8 RID: 16344
	private HashSet<string> m_playedLines = new HashSet<string>();
}
