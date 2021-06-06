using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000551 RID: 1361
public class BoH_Uther_08 : BoH_Uther_Dungeon
{
	// Token: 0x06004B07 RID: 19207 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004B08 RID: 19208 RVA: 0x0018E288 File Offset: 0x0018C488
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8EmoteResponse_01,
			BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeA_01,
			BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeB_01,
			BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeC_01,
			BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_01,
			BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_02,
			BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_03,
			BoH_Uther_08.VO_TB_PrinceHunter_ArthasH_Male_Human_HunterPrince_Victory_01,
			BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_01,
			BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_02,
			BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_03,
			BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Intro_01,
			BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Loss_01,
			BoH_Uther_08.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeA_01,
			BoH_Uther_08.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeB_01,
			BoH_Uther_08.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeC_01,
			BoH_Uther_08.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004B09 RID: 19209 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004B0A RID: 19210 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004B0B RID: 19211 RVA: 0x0018E3FC File Offset: 0x0018C5FC
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Uther_08.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004B0C RID: 19212 RVA: 0x0018E40B File Offset: 0x0018C60B
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004B0D RID: 19213 RVA: 0x0018E413 File Offset: 0x0018C613
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004B0E RID: 19214 RVA: 0x0018E41B File Offset: 0x0018C61B
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8EmoteResponse_01;
	}

	// Token: 0x06004B0F RID: 19215 RVA: 0x0018E434 File Offset: 0x0018C634
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004B10 RID: 19216 RVA: 0x0018E4BD File Offset: 0x0018C6BD
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
			yield return base.PlayLineAlways(actor, BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Loss_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06004B11 RID: 19217 RVA: 0x0018E4D3 File Offset: 0x0018C6D3
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

	// Token: 0x06004B12 RID: 19218 RVA: 0x0018E4E9 File Offset: 0x0018C6E9
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

	// Token: 0x06004B13 RID: 19219 RVA: 0x0018E4FF File Offset: 0x0018C6FF
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 5)
		{
			if (turn == 13)
			{
				yield return base.PlayLineAlways(actor, BoH_Uther_08.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeC_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeC_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Uther_08.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeB_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeB_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004B14 RID: 19220 RVA: 0x0018E515 File Offset: 0x0018C715
	public override void NotifyOfMulliganEnded()
	{
		base.NotifyOfMulliganEnded();
		this.InitVisuals();
	}

	// Token: 0x06004B15 RID: 19221 RVA: 0x0018E524 File Offset: 0x0018C724
	private void InitVisuals()
	{
		int cost = base.GetCost();
		this.InitTurnCounter(cost);
	}

	// Token: 0x06004B16 RID: 19222 RVA: 0x0018E540 File Offset: 0x0018C740
	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.COST && change.newValue != change.oldValue)
		{
			this.UpdateVisuals(change.newValue);
		}
	}

	// Token: 0x06004B17 RID: 19223 RVA: 0x0018E57C File Offset: 0x0018C77C
	private void InitTurnCounter(int cost)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("LOE_Turn_Timer.prefab:b05530aa55868554fb8f0c66632b3c22", AssetLoadingOptions.None);
		this.m_turnCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = this.m_turnCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmBool("RunningMan").Value = true;
		component.FsmVariables.GetFsmBool("MineCart").Value = false;
		component.FsmVariables.GetFsmBool("Airship").Value = false;
		component.FsmVariables.GetFsmBool("Destroyer").Value = false;
		component.SendEvent("Birth");
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_turnCounter.transform.parent = actor.gameObject.transform;
		this.m_turnCounter.transform.localPosition = new Vector3(-1.4f, 0.187f, -0.11f);
		this.m_turnCounter.transform.localScale = Vector3.one * 0.52f;
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x06004B18 RID: 19224 RVA: 0x0018E692 File Offset: 0x0018C892
	private void UpdateVisuals(int cost)
	{
		this.UpdateTurnCounter(cost);
	}

	// Token: 0x06004B19 RID: 19225 RVA: 0x0018E69C File Offset: 0x0018C89C
	private void UpdateMineCartArt()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_mineCartArt.DoPortraitSwap(actor);
	}

	// Token: 0x06004B1A RID: 19226 RVA: 0x0018E6CA File Offset: 0x0018C8CA
	private void UpdateTurnCounter(int cost)
	{
		this.m_turnCounter.GetComponent<PlayMakerFSM>().SendEvent("Action");
		if (cost <= 0)
		{
			UnityEngine.Object.Destroy(this.m_turnCounter.gameObject);
			return;
		}
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x06004B1B RID: 19227 RVA: 0x0018E700 File Offset: 0x0018C900
	private void UpdateTurnCounterText(int cost)
	{
		GameStrings.PluralNumber[] pluralNumbers = new GameStrings.PluralNumber[]
		{
			new GameStrings.PluralNumber
			{
				m_index = 0,
				m_number = cost
			}
		};
		string headlineString = GameStrings.FormatPlurals("BOH_UTHER_08", pluralNumbers, Array.Empty<object>());
		this.m_turnCounter.ChangeDialogText(headlineString, cost.ToString(), "", "");
	}

	// Token: 0x06004B1C RID: 19228 RVA: 0x0010F5C8 File Offset: 0x0010D7C8
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICCLichKing);
	}

	// Token: 0x04003F81 RID: 16257
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Uther_08.InitBooleanOptions();

	// Token: 0x04003F82 RID: 16258
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8EmoteResponse_01.prefab:1c863967bbf51124b91a032af52bc611");

	// Token: 0x04003F83 RID: 16259
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeA_01.prefab:5228d5240d7332b499320186d0c22a08");

	// Token: 0x04003F84 RID: 16260
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeB_01.prefab:17a4c7b01980e0643981c80486d6b203");

	// Token: 0x04003F85 RID: 16261
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8ExchangeC_01.prefab:d5d9b45756b579647980236ed457695c");

	// Token: 0x04003F86 RID: 16262
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_01.prefab:a1f4a52d68e2f1b42a051fac5f244663");

	// Token: 0x04003F87 RID: 16263
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_02.prefab:b6b6233ceeb482147a88ac462f7c0b4c");

	// Token: 0x04003F88 RID: 16264
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_03 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_03.prefab:e920578aa0df881489de3cd1c7ecd1a1");

	// Token: 0x04003F89 RID: 16265
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_01.prefab:f9365764ca569b1458f61865e91d0b8f");

	// Token: 0x04003F8A RID: 16266
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_02.prefab:b2809a7f35d86604e9e8371f58b19749");

	// Token: 0x04003F8B RID: 16267
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_03.prefab:e0414ff298743f34296b99c08bd63703");

	// Token: 0x04003F8C RID: 16268
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Loss_01.prefab:fa3924befd4c9044999c773b893437cf");

	// Token: 0x04003F8D RID: 16269
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeA_01.prefab:5ae0bef312acadf4b9728831e17f5f63");

	// Token: 0x04003F8E RID: 16270
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeC_01.prefab:482d703e8500cd54a9a42cd579e87525");

	// Token: 0x04003F8F RID: 16271
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8Intro_01.prefab:d0e1fba54e9156446bdd8e9f72578615");

	// Token: 0x04003F90 RID: 16272
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Intro_01.prefab:16e9a108765122f4e88b7f5dec76918a");

	// Token: 0x04003F91 RID: 16273
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission8ExchangeB_01.prefab:0e2fd2dbe0efe79429e611b614c92d06");

	// Token: 0x04003F92 RID: 16274
	private static readonly AssetReference VO_TB_PrinceHunter_ArthasH_Male_Human_HunterPrince_Victory_01 = new AssetReference("VO_TB_PrinceHunter_ArthasH_Male_Human_HunterPrince_Victory_01.prefab:2c5b3ea5536f97f499f1d5edc20a6a25");

	// Token: 0x04003F93 RID: 16275
	private List<string> m_HeroPowerLines = new List<string>
	{
		BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_01,
		BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8HeroPower_02,
		BoH_Uther_08.VO_TB_PrinceHunter_ArthasH_Male_Human_HunterPrince_Victory_01
	};

	// Token: 0x04003F94 RID: 16276
	private List<string> m_IdleLines = new List<string>
	{
		BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_01,
		BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_02,
		BoH_Uther_08.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission8Idle_03
	};

	// Token: 0x04003F95 RID: 16277
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04003F96 RID: 16278
	private Notification m_turnCounter;

	// Token: 0x04003F97 RID: 16279
	private MineCartRushArt m_mineCartArt;
}
