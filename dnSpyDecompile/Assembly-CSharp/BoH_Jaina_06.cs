using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000527 RID: 1319
public class BoH_Jaina_06 : BoH_Jaina_Dungeon
{
	// Token: 0x060047AE RID: 18350 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060047AF RID: 18351 RVA: 0x00180E04 File Offset: 0x0017F004
	public BoH_Jaina_06()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Jaina_06.s_booleanOptions);
	}

	// Token: 0x060047B0 RID: 18352 RVA: 0x00180EE4 File Offset: 0x0017F0E4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6EmoteResponse_01,
			BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeA_01,
			BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeB_01,
			BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPower_01,
			BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPower_02,
			BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPower_03,
			BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Idle_01,
			BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Idle_02,
			BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Idle_03,
			BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Intro_01,
			BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Loss_01,
			BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Victory_01,
			BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeA_01,
			BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeB_01,
			BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeC_01,
			BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesAttacked_01,
			BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesAttacked_02,
			BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesGone_01,
			BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Intro_01,
			BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Victory_01,
			BoH_Jaina_06.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6ExchangeC_01,
			BoH_Jaina_06.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6Victory_01,
			BoH_Jaina_06.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060047B1 RID: 18353 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060047B2 RID: 18354 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060047B3 RID: 18355 RVA: 0x001810B8 File Offset: 0x0017F2B8
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060047B4 RID: 18356 RVA: 0x001810C7 File Offset: 0x0017F2C7
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6IdleLines;
	}

	// Token: 0x060047B5 RID: 18357 RVA: 0x001810CF File Offset: 0x0017F2CF
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPowerLines;
	}

	// Token: 0x060047B6 RID: 18358 RVA: 0x001810D8 File Offset: 0x0017F2D8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060047B7 RID: 18359 RVA: 0x00181160 File Offset: 0x0017F360
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
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
		switch (missionEvent)
		{
		case 101:
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.3f, 0.3f, 0.3f), 1f);
			yield return new WaitForSeconds(0.5f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.2f, 0.2f, 0.2f), 5f);
			yield return new WaitForSeconds(5f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.25f, 0.25f, 0.25f), 5f);
			yield return new WaitForSeconds(5.5f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.8f, 0.8f, 0.8f), 0.5f);
			break;
		case 102:
			yield return new WaitForSeconds(5f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.4f, 0.4f, 0.4f), 5f);
			break;
		case 103:
			yield return new WaitForSeconds(10f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.6f, 0.6f, 0.6f), 5f);
			break;
		case 104:
			yield return new WaitForSeconds(15f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(0.8f, 0.8f, 0.8f), 5f);
			break;
		case 105:
			yield return new WaitForSeconds(20f);
			CameraShakeMgr.Shake(Camera.main, new Vector3(1f, 1f, 1f), 5f);
			break;
		case 106:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(BoH_Jaina_06.KalecBrassRing, BoH_Jaina_06.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 107:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(BoH_Jaina_06.KalecBrassRing, BoH_Jaina_06.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6Victory_02, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		default:
			switch (missionEvent)
			{
			case 501:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Victory_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Jaina_06.KalecBrassRing, BoH_Jaina_06.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6Victory_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Victory_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Jaina_06.KalecBrassRing, BoH_Jaina_06.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6Victory_02, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_5BB;
			case 502:
				this.GatesAttackedCounter++;
				if (this.GatesAttackedCounter == 3)
				{
					yield return null;
					goto IL_5BB;
				}
				yield return base.PlayLineInOrderOnce(friendlyActor, this.m_VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesAttackedLines);
				goto IL_5BB;
			case 504:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_5BB;
			}
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		IL_5BB:
		yield break;
	}

	// Token: 0x060047B8 RID: 18360 RVA: 0x00181176 File Offset: 0x0017F376
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

	// Token: 0x060047B9 RID: 18361 RVA: 0x0018118C File Offset: 0x0017F38C
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

	// Token: 0x060047BA RID: 18362 RVA: 0x001811A2 File Offset: 0x0017F3A2
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
				if (turn == 15)
				{
					yield return base.PlayLineAlways(BoH_Jaina_06.KalecBrassRing, BoH_Jaina_06.VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeC_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060047BB RID: 18363 RVA: 0x00176C37 File Offset: 0x00174E37
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}

	// Token: 0x04003B3D RID: 15165
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Jaina_06.InitBooleanOptions();

	// Token: 0x04003B3E RID: 15166
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6EmoteResponse_01.prefab:0008dcdf6af7a634a95520271a435cca");

	// Token: 0x04003B3F RID: 15167
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeA_01.prefab:70108c0bc54581b45824711a1339d995");

	// Token: 0x04003B40 RID: 15168
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6ExchangeB_01.prefab:fd52a2692a7029a489fe89d01bb2e700");

	// Token: 0x04003B41 RID: 15169
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPower_01.prefab:19bed1ab98b7f074c8e060ab12c82392");

	// Token: 0x04003B42 RID: 15170
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPower_02.prefab:eb49ba598eea48369e524a76b6c20d46");

	// Token: 0x04003B43 RID: 15171
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPower_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPower_03.prefab:6565ac890eee41428f3eeef7f2e3c063");

	// Token: 0x04003B44 RID: 15172
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Idle_01.prefab:d0894223340afec4780d9b3bb2a6164e");

	// Token: 0x04003B45 RID: 15173
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Idle_02.prefab:ba5271a4f8fb42f4d9114d0d168a29bb");

	// Token: 0x04003B46 RID: 15174
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Idle_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Idle_03.prefab:56a99a8ebdad8ca48a998840731a002c");

	// Token: 0x04003B47 RID: 15175
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Intro_01.prefab:5b6fd62f56bf2ed49b7521cb031accde");

	// Token: 0x04003B48 RID: 15176
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Loss_01.prefab:18b2ce084f187ef4aba70e8c84fba6b1");

	// Token: 0x04003B49 RID: 15177
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Victory_01.prefab:3853008ffe2d76644a6267865191a8c4");

	// Token: 0x04003B4A RID: 15178
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeA_01.prefab:61bacbf4a566b464bb984cc9dec7ff1a");

	// Token: 0x04003B4B RID: 15179
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeB_01.prefab:46dd9feda9fb8ec44ad324269cfda61a");

	// Token: 0x04003B4C RID: 15180
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6ExchangeC_01.prefab:eebaeeda2975d8d4a836540db88e8202");

	// Token: 0x04003B4D RID: 15181
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesAttacked_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesAttacked_01.prefab:0ffd0e856e107194e930bfadde42634a");

	// Token: 0x04003B4E RID: 15182
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesAttacked_02 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesAttacked_02.prefab:c452ecd2cdc46cc4384f876c28d0dc0b");

	// Token: 0x04003B4F RID: 15183
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesGone_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesGone_01.prefab:aba652abd2add6b4492e0343539be428");

	// Token: 0x04003B50 RID: 15184
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Intro_01.prefab:999a6a662df578943afb152b1fdc9451");

	// Token: 0x04003B51 RID: 15185
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6Victory_01.prefab:951cdabc40fe62d4cacaee2fa730b402");

	// Token: 0x04003B52 RID: 15186
	private static readonly AssetReference VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6ExchangeC_01 = new AssetReference("VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6ExchangeC_01.prefab:7fbd004de4db7804a8c7139eaebe9ba5");

	// Token: 0x04003B53 RID: 15187
	private static readonly AssetReference VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6Victory_01 = new AssetReference("VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6Victory_01.prefab:72ff20259dc92d147841a3ad16995802");

	// Token: 0x04003B54 RID: 15188
	private static readonly AssetReference VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6Victory_02 = new AssetReference("VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission6Victory_02.prefab:16deb7e0336609145b5b2da4de482ef9");

	// Token: 0x04003B55 RID: 15189
	public static readonly AssetReference KalecBrassRing = new AssetReference("Kalec_BrassRing_Quote.prefab:b96062478a5eccd47bd5e94f1ad7312f");

	// Token: 0x04003B56 RID: 15190
	private List<string> m_VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPowerLines = new List<string>
	{
		BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPower_01,
		BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPower_02,
		BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6HeroPower_03
	};

	// Token: 0x04003B57 RID: 15191
	private List<string> m_VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6IdleLines = new List<string>
	{
		BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Idle_01,
		BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Idle_02,
		BoH_Jaina_06.VO_Story_Hero_Garrosh_Male_Orc_Story_Jaina_Mission6Idle_03
	};

	// Token: 0x04003B58 RID: 15192
	private List<string> m_VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesAttackedLines = new List<string>
	{
		BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesAttacked_01,
		BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesAttacked_02,
		BoH_Jaina_06.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission6GatesGone_01
	};

	// Token: 0x04003B59 RID: 15193
	private int GatesAttackedCounter;

	// Token: 0x04003B5A RID: 15194
	private HashSet<string> m_playedLines = new HashSet<string>();
}
