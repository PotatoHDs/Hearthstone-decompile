using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000545 RID: 1349
public class BoH_Thrall_06 : BoH_Thrall_Dungeon
{
	// Token: 0x06004A07 RID: 18951 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004A08 RID: 18952 RVA: 0x0018A71C File Offset: 0x0018891C
	public BoH_Thrall_06()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Thrall_06.s_booleanOptions);
	}

	// Token: 0x06004A09 RID: 18953 RVA: 0x0018A7F4 File Offset: 0x001889F4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6EmoteResponse_01,
			BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6ExchangeA_01,
			BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6ExchangeB_01,
			BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6ExchangeC_01,
			BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6HeroPower_01,
			BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6HeroPower_02,
			BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6HeroPower_03,
			BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Idle_01,
			BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Idle_02,
			BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Idle_03,
			BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Intro_01,
			BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Loss_01,
			BoH_Thrall_06.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission6ExchangeD_01,
			BoH_Thrall_06.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission6Intro_02,
			BoH_Thrall_06.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission6Victory_02,
			BoH_Thrall_06.VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6ExchangeE_01,
			BoH_Thrall_06.VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6ExchangeF_01,
			BoH_Thrall_06.VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6ExchangeG_01,
			BoH_Thrall_06.VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6Victory_03,
			BoH_Thrall_06.VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6Victory_04,
			BoH_Thrall_06.VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6ExchangeD_01,
			BoH_Thrall_06.VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6ExchangeD_02,
			BoH_Thrall_06.VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6ExchangeE_02,
			BoH_Thrall_06.VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004A0A RID: 18954 RVA: 0x0018A9D8 File Offset: 0x00188BD8
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x06004A0B RID: 18955 RVA: 0x0018A9EC File Offset: 0x00188BEC
	private void SetPopupPosition()
	{
		if (this.friendlySidePlayer.IsCurrentPlayer())
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = -66f;
				return;
			}
			this.popUpPos.z = -44f;
			return;
		}
		else
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = 66f;
				return;
			}
			this.popUpPos.z = 44f;
			return;
		}
	}

	// Token: 0x06004A0C RID: 18956 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004A0D RID: 18957 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004A0E RID: 18958 RVA: 0x0018AA61 File Offset: 0x00188C61
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Thrall_06.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission6Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004A0F RID: 18959 RVA: 0x0018AA70 File Offset: 0x00188C70
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004A10 RID: 18960 RVA: 0x0018AA78 File Offset: 0x00188C78
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004A11 RID: 18961 RVA: 0x0018AA80 File Offset: 0x00188C80
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_DRGEVILBoss;
		this.m_standardEmoteResponseLine = BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6EmoteResponse_01;
	}

	// Token: 0x06004A12 RID: 18962 RVA: 0x0018AAA4 File Offset: 0x00188CA4
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

	// Token: 0x06004A13 RID: 18963 RVA: 0x0018AB28 File Offset: 0x00188D28
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		this.popUpPos = new Vector3(0f, 0f, -40f);
		if (missionEvent <= 228)
		{
			switch (missionEvent)
			{
			case 101:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_06.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission6ExchangeD_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Thrall_06.YseraBrassRing, BoH_Thrall_06.VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6ExchangeE_02, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_3BE;
			case 102:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(BoH_Thrall_06.AlexstraszaBrassRing, BoH_Thrall_06.VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6ExchangeF_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_3BE;
			case 103:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(BoH_Thrall_06.YseraBrassRing, BoH_Thrall_06.VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6Victory_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_06.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission6Victory_02, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_3BE;
			default:
				if (missionEvent == 228)
				{
					yield return new WaitForSeconds(2f);
					Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
					NotificationManager.Get().DestroyNotification(notification, 7.5f);
					goto IL_3BE;
				}
				break;
			}
		}
		else
		{
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(BoH_Thrall_06.AlexstraszaBrassRing, BoH_Thrall_06.VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6Victory_03, 2.5f);
				yield return base.PlayLineAlways(BoH_Thrall_06.AlexstraszaBrassRing, BoH_Thrall_06.VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6Victory_04, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_3BE;
			}
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_3BE;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_3BE:
		yield break;
	}

	// Token: 0x06004A14 RID: 18964 RVA: 0x0018AB3E File Offset: 0x00188D3E
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

	// Token: 0x06004A15 RID: 18965 RVA: 0x0018AB54 File Offset: 0x00188D54
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

	// Token: 0x06004A16 RID: 18966 RVA: 0x0018AB6A File Offset: 0x00188D6A
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn <= 7)
		{
			if (turn != 2)
			{
				if (turn == 7)
				{
					yield return base.PlayLineAlways(actor, BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6ExchangeB_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6ExchangeA_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Thrall_06.AlexstraszaBrassRing, BoH_Thrall_06.VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6ExchangeE_01, 2.5f);
			}
		}
		else if (turn != 9)
		{
			if (turn != 13)
			{
				if (turn == 17)
				{
					yield return base.PlayLineAlways(BoH_Thrall_06.AlexstraszaBrassRing, BoH_Thrall_06.VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6ExchangeG_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(BoH_Thrall_06.YseraBrassRing, BoH_Thrall_06.VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6ExchangeD_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Thrall_06.YseraBrassRing, BoH_Thrall_06.VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6ExchangeD_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6ExchangeC_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003E39 RID: 15929
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Thrall_06.InitBooleanOptions();

	// Token: 0x04003E3A RID: 15930
	private static readonly AssetReference VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6EmoteResponse_01.prefab:92b91b27228aec34b8b757611ef74bd4");

	// Token: 0x04003E3B RID: 15931
	private static readonly AssetReference VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6ExchangeA_01.prefab:73a5fb568fa366b4c9904861016a9145");

	// Token: 0x04003E3C RID: 15932
	private static readonly AssetReference VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6ExchangeB_01.prefab:19e5e2193d33adf49bc92537ceec0472");

	// Token: 0x04003E3D RID: 15933
	private static readonly AssetReference VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6ExchangeC_01.prefab:b30cd3ba963151b4fb33a35ad068e4ff");

	// Token: 0x04003E3E RID: 15934
	private static readonly AssetReference VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6HeroPower_01.prefab:0d6af03e113ef2f42bf20ccdbd979e80");

	// Token: 0x04003E3F RID: 15935
	private static readonly AssetReference VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6HeroPower_02.prefab:b6568430774a8534e82fb19876e637a7");

	// Token: 0x04003E40 RID: 15936
	private static readonly AssetReference VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6HeroPower_03 = new AssetReference("VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6HeroPower_03.prefab:6c0782548c43df243bc0d9046498dadc");

	// Token: 0x04003E41 RID: 15937
	private static readonly AssetReference VO__Male_Dragon_Death_01 = new AssetReference("VO__Male_Dragon_Death_01.prefab:321728cfd9ce8574fa48b0774c2512bb");

	// Token: 0x04003E42 RID: 15938
	private static readonly AssetReference VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Idle_01.prefab:d605daf6e3c8b6e499fab7dc3374034d");

	// Token: 0x04003E43 RID: 15939
	private static readonly AssetReference VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Idle_02.prefab:964549b2c57d67144b93e2d315511425");

	// Token: 0x04003E44 RID: 15940
	private static readonly AssetReference VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Idle_03 = new AssetReference("VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Idle_03.prefab:3e93e62d068fedd42a1ab1c5ab9fb7f3");

	// Token: 0x04003E45 RID: 15941
	private static readonly AssetReference VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Intro_01.prefab:cd55b9f860b573b4283466f1b0ec6316");

	// Token: 0x04003E46 RID: 15942
	private static readonly AssetReference VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Loss_01.prefab:9e77d973df735dc41bb3f32e73b6252e");

	// Token: 0x04003E47 RID: 15943
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission6ExchangeD_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission6ExchangeD_01.prefab:2c5a654c942263b48917a0459c785320");

	// Token: 0x04003E48 RID: 15944
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission6Intro_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission6Intro_02.prefab:53ba9fde0fe84f5429e38f29190b8b48");

	// Token: 0x04003E49 RID: 15945
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission6Victory_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission6Victory_02.prefab:51ec8ac6cb0941f49952dffb934efc63");

	// Token: 0x04003E4A RID: 15946
	private static readonly AssetReference VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6ExchangeE_01 = new AssetReference("VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6ExchangeE_01.prefab:99a41c61c39ca6645b4151f2d75bf4b8");

	// Token: 0x04003E4B RID: 15947
	private static readonly AssetReference VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6ExchangeF_01 = new AssetReference("VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6ExchangeF_01.prefab:1e052585567ff7844b8ac4bf8324756b");

	// Token: 0x04003E4C RID: 15948
	private static readonly AssetReference VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6ExchangeG_01 = new AssetReference("VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6ExchangeG_01.prefab:223f14d322a4c724a8f9ad8d229a0aab");

	// Token: 0x04003E4D RID: 15949
	private static readonly AssetReference VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6Victory_03 = new AssetReference("VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6Victory_03.prefab:61bffbf62b059f94693c504e5fa1e7ec");

	// Token: 0x04003E4E RID: 15950
	private static readonly AssetReference VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6Victory_04 = new AssetReference("VO_Story_Minion_Alexstrasza_Female_Dragon_Story_Thrall_Mission6Victory_04.prefab:823c1ceb303626b458af2574c260858d");

	// Token: 0x04003E4F RID: 15951
	private static readonly AssetReference VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6ExchangeD_01 = new AssetReference("VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6ExchangeD_01.prefab:63b57afdcabf7834891d0a24a7fa989d");

	// Token: 0x04003E50 RID: 15952
	private static readonly AssetReference VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6ExchangeD_02 = new AssetReference("VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6ExchangeD_02.prefab:e7e6581c52ede504e9a6c0587aec018a");

	// Token: 0x04003E51 RID: 15953
	private static readonly AssetReference VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6ExchangeE_02 = new AssetReference("VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6ExchangeE_02.prefab:cc779132bdcc70f489679151906a43eb");

	// Token: 0x04003E52 RID: 15954
	private static readonly AssetReference VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6Victory_01 = new AssetReference("VO_Story_Minion_Ysera_Female_Dragon_Story_Thrall_Mission6Victory_01.prefab:2417b626e021b0547ba62dad704d7319");

	// Token: 0x04003E53 RID: 15955
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			228,
			new string[]
			{
				"BOH_THRALL_06"
			}
		}
	};

	// Token: 0x04003E54 RID: 15956
	private Player friendlySidePlayer;

	// Token: 0x04003E55 RID: 15957
	private Entity playerEntity;

	// Token: 0x04003E56 RID: 15958
	private float popUpScale = 1.25f;

	// Token: 0x04003E57 RID: 15959
	private Vector3 popUpPos;

	// Token: 0x04003E58 RID: 15960
	private Notification StartPopup;

	// Token: 0x04003E59 RID: 15961
	public static readonly AssetReference AlexstraszaBrassRing = new AssetReference("Alexstrasza_BrassRing_Quote.prefab:329e7a3e53fbf9f4997fea9db57921ba");

	// Token: 0x04003E5A RID: 15962
	public static readonly AssetReference YseraBrassRing = new AssetReference("Ysera_BrassRing_Quote.prefab:1b5ee7911e0cc0f48bff1d9ea60a95e1");

	// Token: 0x04003E5B RID: 15963
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6HeroPower_01,
		BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6HeroPower_02,
		BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6HeroPower_03
	};

	// Token: 0x04003E5C RID: 15964
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Idle_01,
		BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Idle_02,
		BoH_Thrall_06.VO_Story_Hero_Deathwing_Male_Dragon_Story_Thrall_Mission6Idle_03
	};

	// Token: 0x04003E5D RID: 15965
	private HashSet<string> m_playedLines = new HashSet<string>();
}
