using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000529 RID: 1321
public class BoH_Jaina_08 : BoH_Jaina_Dungeon
{
	// Token: 0x060047D3 RID: 18387 RVA: 0x0016DED1 File Offset: 0x0016C0D1
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	// Token: 0x060047D4 RID: 18388 RVA: 0x001817C8 File Offset: 0x0017F9C8
	public BoH_Jaina_08()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Jaina_08.s_booleanOptions);
	}

	// Token: 0x060047D5 RID: 18389 RVA: 0x0018186C File Offset: 0x0017FA6C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Death_01,
			BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8EmoteResponse_01,
			BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeB_01,
			BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeC_01,
			BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeD_01,
			BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_01,
			BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_02,
			BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_03,
			BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_01,
			BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_02,
			BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_03,
			BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Intro_01,
			BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Loss_01,
			BoH_Jaina_08.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeA_01,
			BoH_Jaina_08.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeB_01,
			BoH_Jaina_08.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeC_01,
			BoH_Jaina_08.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeD_01,
			BoH_Jaina_08.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Intro_01,
			BoH_Jaina_08.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_01,
			BoH_Jaina_08.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_02,
			BoH_Jaina_08.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060047D6 RID: 18390 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060047D7 RID: 18391 RVA: 0x00181A20 File Offset: 0x0017FC20
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Jaina_08.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060047D8 RID: 18392 RVA: 0x00181A2F File Offset: 0x0017FC2F
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8IdleLines;
	}

	// Token: 0x060047D9 RID: 18393 RVA: 0x00181A37 File Offset: 0x0017FC37
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPowerLines;
	}

	// Token: 0x060047DA RID: 18394 RVA: 0x00181A3F File Offset: 0x0017FC3F
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Death_01;
		this.m_standardEmoteResponseLine = BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8EmoteResponse_01;
	}

	// Token: 0x060047DB RID: 18395 RVA: 0x00181A68 File Offset: 0x0017FC68
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

	// Token: 0x060047DC RID: 18396 RVA: 0x00181AEC File Offset: 0x0017FCEC
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 504)
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Loss_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x060047DD RID: 18397 RVA: 0x00181B02 File Offset: 0x0017FD02
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

	// Token: 0x060047DE RID: 18398 RVA: 0x00181B18 File Offset: 0x0017FD18
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

	// Token: 0x060047DF RID: 18399 RVA: 0x00181B2E File Offset: 0x0017FD2E
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn <= 3)
		{
			if (turn != 1)
			{
				if (turn == 3)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeB_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_08.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeB_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_08.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeA_01, 2.5f);
			}
		}
		else if (turn != 7)
		{
			if (turn == 9)
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_08.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeD_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeD_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeC_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_08.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeC_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060047E0 RID: 18400 RVA: 0x0016646C File Offset: 0x0016466C
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRGEVILBoss);
	}

	// Token: 0x04003B73 RID: 15219
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Jaina_08.InitBooleanOptions();

	// Token: 0x04003B74 RID: 15220
	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Death_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Death_01.prefab:fbc72de6710c2364c81a98e37280b5f0");

	// Token: 0x04003B75 RID: 15221
	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8EmoteResponse_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8EmoteResponse_01.prefab:49250351557f61440aaa16548117f595");

	// Token: 0x04003B76 RID: 15222
	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeB_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeB_01.prefab:17d70f81add992e40a9f0f4ac5a70fc5");

	// Token: 0x04003B77 RID: 15223
	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeC_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeC_01.prefab:2a229c03821ff9b43a4643fb3397a13c");

	// Token: 0x04003B78 RID: 15224
	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeD_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8ExchangeD_01.prefab:20161037a0b160c4dab3a4092cabc7ea");

	// Token: 0x04003B79 RID: 15225
	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_01.prefab:474d4054251041d44bae684265b8bdaf");

	// Token: 0x04003B7A RID: 15226
	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_02 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_02.prefab:f24767464968cfe4ba9049b6f612b34a");

	// Token: 0x04003B7B RID: 15227
	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_03 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_03.prefab:e6e603b63904d7848b4053f8bfd2ef9a");

	// Token: 0x04003B7C RID: 15228
	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_01.prefab:baf05bfec7a95ef40800fc39814ac330");

	// Token: 0x04003B7D RID: 15229
	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_02 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_02.prefab:ec00245c1a07bef4fbb62bb38dcdebea");

	// Token: 0x04003B7E RID: 15230
	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_03 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_03.prefab:1d0fb7287ab7dff4da9898d0a98f3fbe");

	// Token: 0x04003B7F RID: 15231
	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Intro_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Intro_01.prefab:87d63628f43897744b211feb7e766ac3");

	// Token: 0x04003B80 RID: 15232
	private static readonly AssetReference VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Loss_01 = new AssetReference("VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Loss_01.prefab:ce1ac52480e326d4787ddee42735507f");

	// Token: 0x04003B81 RID: 15233
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeA_01.prefab:1b276a3e89a5c4c469b09f738f32deda");

	// Token: 0x04003B82 RID: 15234
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeB_01.prefab:0d3348dd7ec33474fa7b158365fcc8e6");

	// Token: 0x04003B83 RID: 15235
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeC_01.prefab:8b7754c3a70a79a4f94ce2ec283a5086");

	// Token: 0x04003B84 RID: 15236
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeD_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8ExchangeD_01.prefab:c1f26293e6483e74eb4a97b295bb7eca");

	// Token: 0x04003B85 RID: 15237
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Intro_01.prefab:898fb49c280bfb644acd6fe7c9249114");

	// Token: 0x04003B86 RID: 15238
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_01.prefab:e8c982df62483924f871940dba3a6dab");

	// Token: 0x04003B87 RID: 15239
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_02 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_02.prefab:04b4f7f1a0c948e4cb00c506bfc33395");

	// Token: 0x04003B88 RID: 15240
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_03 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission8Victory_03.prefab:5cb58b8880cde904cb7ff6aa03145202");

	// Token: 0x04003B89 RID: 15241
	private List<string> m_VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPowerLines = new List<string>
	{
		BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_01,
		BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_02,
		BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8HeroPower_03
	};

	// Token: 0x04003B8A RID: 15242
	private List<string> m_VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8IdleLines = new List<string>
	{
		BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_01,
		BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_02,
		BoH_Jaina_08.VO_Story_01_Aethas_Male_BloodElf_Story_Jaina_Mission8Idle_03
	};

	// Token: 0x04003B8B RID: 15243
	private HashSet<string> m_playedLines = new HashSet<string>();
}
