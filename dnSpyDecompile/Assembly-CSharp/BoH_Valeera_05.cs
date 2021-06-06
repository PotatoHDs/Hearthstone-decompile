using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000558 RID: 1368
public class BoH_Valeera_05 : BoH_Valeera_Dungeon
{
	// Token: 0x06004B94 RID: 19348 RVA: 0x001905FC File Offset: 0x0018E7FC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Valeera_05.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission5ExchangeD_01,
			BoH_Valeera_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Valeera_Mission5ExchangeA_02,
			BoH_Valeera_05.VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5ExchangeC_01,
			BoH_Valeera_05.VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5Victory_02,
			BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Death_01,
			BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5EmoteResponse_01,
			BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_01,
			BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_02,
			BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_03,
			BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_01,
			BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_02,
			BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_03,
			BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Intro_01,
			BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Loss_01,
			BoH_Valeera_05.VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_01,
			BoH_Valeera_05.VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_03,
			BoH_Valeera_05.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeA_01,
			BoH_Valeera_05.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeC_02,
			BoH_Valeera_05.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeA_02,
			BoH_Valeera_05.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeD_02,
			BoH_Valeera_05.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Intro_02,
			BoH_Valeera_05.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_01,
			BoH_Valeera_05.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004B95 RID: 19349 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004B96 RID: 19350 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004B97 RID: 19351 RVA: 0x001907D0 File Offset: 0x0018E9D0
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Valeera_05.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004B98 RID: 19352 RVA: 0x001907DF File Offset: 0x0018E9DF
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004B99 RID: 19353 RVA: 0x001907E7 File Offset: 0x0018E9E7
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004B9A RID: 19354 RVA: 0x001907EF File Offset: 0x0018E9EF
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Death_01;
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_ICC;
		this.m_standardEmoteResponseLine = BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5EmoteResponse_01;
	}

	// Token: 0x06004B9B RID: 19355 RVA: 0x00190824 File Offset: 0x0018EA24
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

	// Token: 0x06004B9C RID: 19356 RVA: 0x001908A8 File Offset: 0x0018EAA8
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 108)
		{
			if (missionEvent == 102)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(BoH_Valeera_05.ThrallBrassRing, BoH_Valeera_05.VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Valeera_05.GarroshBrassRing, BoH_Valeera_05.VO_Story_Hero_Garrosh_Male_Orc_Story_Valeera_Mission5ExchangeA_02, 2.5f);
				yield return base.PlayLineAlways(BoH_Valeera_05.ThrallBrassRing, BoH_Valeera_05.VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_03, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_3A0;
			}
			if (missionEvent == 108)
			{
				Actor enemyActorByCardId = base.GetEnemyActorByCardId("Story_06_Garona");
				if (enemyActorByCardId != null)
				{
					yield return base.PlayLineAlways(enemyActorByCardId, BoH_Valeera_05.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission5ExchangeD_01, 2.5f);
				}
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_05.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeD_02, 2.5f);
				goto IL_3A0;
			}
		}
		else
		{
			if (missionEvent == 109)
			{
				Actor enemyActorByCardId2 = base.GetEnemyActorByCardId("Story_06_GaronaDormant");
				if (enemyActorByCardId2 != null)
				{
					yield return base.PlayLineAlways(enemyActorByCardId2, BoH_Valeera_05.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission5ExchangeD_01, 2.5f);
				}
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_05.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeD_02, 2.5f);
				goto IL_3A0;
			}
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_05.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Valeera_05.JainaBrassRing, BoH_Valeera_05.VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5Victory_02, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_05.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_03, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_3A0;
			}
			if (missionEvent == 507)
			{
				yield return base.PlayLineAlways(actor, BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Loss_01, 2.5f);
				goto IL_3A0;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_3A0:
		yield break;
	}

	// Token: 0x06004B9D RID: 19357 RVA: 0x001908BE File Offset: 0x0018EABE
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

	// Token: 0x06004B9E RID: 19358 RVA: 0x001908D4 File Offset: 0x0018EAD4
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

	// Token: 0x06004B9F RID: 19359 RVA: 0x001908EA File Offset: 0x0018EAEA
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn == 5)
			{
				yield return base.PlayLineAlways(BoH_Valeera_05.JainaBrassRing, BoH_Valeera_05.VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5ExchangeC_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Valeera_05.VarianBrassRing, BoH_Valeera_05.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeC_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(BoH_Valeera_05.VarianBrassRing, BoH_Valeera_05.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_05.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeA_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400400D RID: 16397
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Valeera_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Valeera_Mission5ExchangeA_02.prefab:cc8e72201171551489548b303bed4779");

	// Token: 0x0400400E RID: 16398
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5ExchangeC_01.prefab:62fe0a172e6021543b4612f8d2055b14");

	// Token: 0x0400400F RID: 16399
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission5Victory_02.prefab:0b34cc033a237f54daaaf030b9927bda");

	// Token: 0x04004010 RID: 16400
	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Death_01 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Death_01.prefab:f20e0ccfc5ebb3146a3e2b9a546043fd");

	// Token: 0x04004011 RID: 16401
	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5EmoteResponse_01.prefab:bac843f870c74f14db715dd2404f6e0b");

	// Token: 0x04004012 RID: 16402
	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_01.prefab:7d3df54c14e60c34bbe527c3e68645b6");

	// Token: 0x04004013 RID: 16403
	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_02.prefab:e96c9d8ce955201489c97775369a0a66");

	// Token: 0x04004014 RID: 16404
	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_03.prefab:f762709ca55fb334a93d020487f8bf8d");

	// Token: 0x04004015 RID: 16405
	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_01.prefab:31ece93aed91a004cb10fac406bc1b67");

	// Token: 0x04004016 RID: 16406
	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_02.prefab:80ce121e13e777446b4143c2900f5f09");

	// Token: 0x04004017 RID: 16407
	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_03.prefab:bb17562dbbb4b6d4abaeb2d5690fee53");

	// Token: 0x04004018 RID: 16408
	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Intro_01.prefab:53f010d3373fa3147bc15108fd692518");

	// Token: 0x04004019 RID: 16409
	private static readonly AssetReference VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Loss_01.prefab:28c5b4cfee104f84aa3aed5a3b0937a4");

	// Token: 0x0400401A RID: 16410
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_01.prefab:f4b4d12a295d42045a3ac73ec4acd0f8");

	// Token: 0x0400401B RID: 16411
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Valeera_Mission5ExchangeA_03.prefab:013fc061ad6a0cf49ad4db4bdc78736f");

	// Token: 0x0400401C RID: 16412
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeA_01.prefab:2959bca0cd8be9a41beed97b6627554c");

	// Token: 0x0400401D RID: 16413
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeC_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission5ExchangeC_02.prefab:42a7a272e1572f54c8a5a845607121a7");

	// Token: 0x0400401E RID: 16414
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeA_02.prefab:2678f0357642663479546b57f56f3928");

	// Token: 0x0400401F RID: 16415
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeD_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5ExchangeD_02.prefab:53253aeec062c9144b24b22f2c5281c6");

	// Token: 0x04004020 RID: 16416
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Intro_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Intro_02.prefab:cb3361637933ceb4b8c76e42b558bf3f");

	// Token: 0x04004021 RID: 16417
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_01.prefab:0c4eab66504069d45a82c578b6a30037");

	// Token: 0x04004022 RID: 16418
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission5Victory_03.prefab:d5be331a0ca0ffd458013319c3ead68f");

	// Token: 0x04004023 RID: 16419
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission5ExchangeD_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission5ExchangeD_01.prefab:5734c86a800699048aa559ec5c4e7ddd");

	// Token: 0x04004024 RID: 16420
	public static readonly AssetReference VarianBrassRing = new AssetReference("Varian_BrassRing_Quote.prefab:b192b80fcc22d1145bfa81b476cecc09");

	// Token: 0x04004025 RID: 16421
	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	// Token: 0x04004026 RID: 16422
	public static readonly AssetReference GarroshBrassRing = new AssetReference("Garrosh_BrassRing_Quote.prefab:9c911310fb2bf7246ae78ef14a1b4dc5");

	// Token: 0x04004027 RID: 16423
	public static readonly AssetReference JainaBrassRing = new AssetReference("JainaMid_BrassRing_Quote.prefab:7eba171d881f6764e81abddbb125bb19");

	// Token: 0x04004028 RID: 16424
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_01,
		BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_02,
		BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5HeroPower_03
	};

	// Token: 0x04004029 RID: 16425
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_01,
		BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_02,
		BoH_Valeera_05.VO_Story_Hero_Stasia_Female_Undead_Story_Valeera_Mission5Idle_03
	};

	// Token: 0x0400402A RID: 16426
	private HashSet<string> m_playedLines = new HashSet<string>();
}
