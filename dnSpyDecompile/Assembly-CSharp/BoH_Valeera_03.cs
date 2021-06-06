using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000556 RID: 1366
public class BoH_Valeera_03 : BoH_Valeera_Dungeon
{
	// Token: 0x06004B72 RID: 19314 RVA: 0x0018FC1C File Offset: 0x0018DE1C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Death_01,
			BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3EmoteResponse_01,
			BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3ExchangeA_01,
			BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3ExchangeB_02,
			BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3HeroPower_01,
			BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3HeroPower_02,
			BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3HeroPower_03,
			BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Idle_01,
			BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Idle_02,
			BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Idle_03,
			BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Intro_01,
			BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Loss_01,
			BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Victory_02,
			BoH_Valeera_03.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3ExchangeA_02,
			BoH_Valeera_03.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3ExchangeB_01,
			BoH_Valeera_03.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3ExchangeC_03,
			BoH_Valeera_03.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3Intro_02,
			BoH_Valeera_03.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3Victory_01,
			BoH_Valeera_03.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3Victory_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004B73 RID: 19315 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004B74 RID: 19316 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004B75 RID: 19317 RVA: 0x0018FDB0 File Offset: 0x0018DFB0
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Valeera_03.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004B76 RID: 19318 RVA: 0x0018FDBF File Offset: 0x0018DFBF
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004B77 RID: 19319 RVA: 0x0018FDC7 File Offset: 0x0018DFC7
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004B78 RID: 19320 RVA: 0x0018FDCF File Offset: 0x0018DFCF
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Death_01;
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_GIL;
		this.m_standardEmoteResponseLine = BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3EmoteResponse_01;
	}

	// Token: 0x06004B79 RID: 19321 RVA: 0x0018FE04 File Offset: 0x0018E004
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

	// Token: 0x06004B7A RID: 19322 RVA: 0x0018FE88 File Offset: 0x0018E088
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 102)
		{
			if (missionEvent == 507)
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Loss_01, 2.5f);
			}
			else
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_03.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3Victory_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Victory_02, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_03.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3Victory_03, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004B7B RID: 19323 RVA: 0x0018FE9E File Offset: 0x0018E09E
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

	// Token: 0x06004B7C RID: 19324 RVA: 0x0018FEB4 File Offset: 0x0018E0B4
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

	// Token: 0x06004B7D RID: 19325 RVA: 0x0018FECA File Offset: 0x0018E0CA
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn != 5)
			{
				if (turn == 11)
				{
					yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_03.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3ExchangeC_03, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_03.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3ExchangeB_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_03.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3ExchangeA_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003FD9 RID: 16345
	private static readonly AssetReference VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Death_01 = new AssetReference("VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Death_01.prefab:3d5843774ea2fb74e8f623388afac3e8");

	// Token: 0x04003FDA RID: 16346
	private static readonly AssetReference VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3EmoteResponse_01.prefab:e87df59d72560db43a0c805737acb8ab");

	// Token: 0x04003FDB RID: 16347
	private static readonly AssetReference VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3ExchangeA_01.prefab:0452236aef80d1e42ab8f513d0545ed8");

	// Token: 0x04003FDC RID: 16348
	private static readonly AssetReference VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3ExchangeB_02 = new AssetReference("VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3ExchangeB_02.prefab:7cb86c78a265acf498df7d586d467290");

	// Token: 0x04003FDD RID: 16349
	private static readonly AssetReference VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3HeroPower_01.prefab:b21d8f5ca30482640bed3ff0eaf6752e");

	// Token: 0x04003FDE RID: 16350
	private static readonly AssetReference VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3HeroPower_02.prefab:69960b8020fec1b468b0b9b2a2a791c0");

	// Token: 0x04003FDF RID: 16351
	private static readonly AssetReference VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3HeroPower_03.prefab:8f869559b4857d14da4d90dd3b7f66cc");

	// Token: 0x04003FE0 RID: 16352
	private static readonly AssetReference VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Idle_01.prefab:5d64b37a2c454c94090e725f40fd1302");

	// Token: 0x04003FE1 RID: 16353
	private static readonly AssetReference VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Idle_02.prefab:e87e4b12750dcc8408eb644c5f263740");

	// Token: 0x04003FE2 RID: 16354
	private static readonly AssetReference VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Idle_03.prefab:d1eccb2f550e9534383bc8fcdc98ff5e");

	// Token: 0x04003FE3 RID: 16355
	private static readonly AssetReference VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Intro_01.prefab:a63c2206f873c124fb2cd326cdf9f859");

	// Token: 0x04003FE4 RID: 16356
	private static readonly AssetReference VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Loss_01.prefab:fde076baa6590b041a0b1fb57d11900e");

	// Token: 0x04003FE5 RID: 16357
	private static readonly AssetReference VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Victory_02 = new AssetReference("VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Victory_02.prefab:b73a336d8bf1bfe488144dc3d2343418");

	// Token: 0x04003FE6 RID: 16358
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3ExchangeA_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3ExchangeA_02.prefab:0a9dbf95b51a19642995cfede6acf09e");

	// Token: 0x04003FE7 RID: 16359
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3ExchangeB_01.prefab:b66d5647be5ceda48a99af96e883d943");

	// Token: 0x04003FE8 RID: 16360
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3ExchangeC_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3ExchangeC_03.prefab:842a7e1730fbd15419c9aef2b17ca215");

	// Token: 0x04003FE9 RID: 16361
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3Intro_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3Intro_02.prefab:97777ec2d8c98f54a9649009784a9dad");

	// Token: 0x04003FEA RID: 16362
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3Victory_01.prefab:6f7c3e0d5816b5947b10c227cf146929");

	// Token: 0x04003FEB RID: 16363
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3Victory_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission3Victory_03.prefab:d23e2affb68181a4b916afd99c394047");

	// Token: 0x04003FEC RID: 16364
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3HeroPower_01,
		BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3HeroPower_02,
		BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3HeroPower_03
	};

	// Token: 0x04003FED RID: 16365
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Idle_01,
		BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Idle_02,
		BoH_Valeera_03.VO_Story_Hero_Daerion_Male_Human_Story_Valeera_Mission3Idle_03
	};

	// Token: 0x04003FEE RID: 16366
	private HashSet<string> m_playedLines = new HashSet<string>();
}
