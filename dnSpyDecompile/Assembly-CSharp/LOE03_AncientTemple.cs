using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200038C RID: 908
public class LOE03_AncientTemple : LOE_MissionEntity
{
	// Token: 0x0600348E RID: 13454 RVA: 0x0010C851 File Offset: 0x0010AA51
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.HANDLE_COIN,
				false
			}
		};
	}

	// Token: 0x0600348F RID: 13455 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x06003490 RID: 13456 RVA: 0x0010C867 File Offset: 0x0010AA67
	public LOE03_AncientTemple()
	{
		this.m_gameOptions.AddOptions(LOE03_AncientTemple.s_booleanOptions, LOE03_AncientTemple.s_stringOptions);
	}

	// Token: 0x06003491 RID: 13457 RVA: 0x0010C884 File Offset: 0x0010AA84
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOE_03_TURN_5_4.prefab:d35f6f70b2c0eca44920996f1d1b280b");
		base.PreloadSound("VO_LOE_03_TURN_6.prefab:43a3554df2b302c428fdc7325ab913ed");
		base.PreloadSound("VO_LOE_03_TURN_9.prefab:3f4b6112b2fea054693987a5cfbaf29b");
		base.PreloadSound("VO_LOE_03_TURN_5.prefab:cd01ccc585ea5e541bb6d6bf014ab57f");
		base.PreloadSound("VO_LOE_03_TURN_4_GOOD.prefab:04e58ac2fa0d1874caa45fe4bd009c16");
		base.PreloadSound("VO_LOE_03_TURN_4_BAD.prefab:2f27d2a958ffdd44387be7a1fe070234");
		base.PreloadSound("VO_LOE_03_TURN_6_2.prefab:e648bc075f6c30249a9221100bec6c06");
		base.PreloadSound("VO_LOE_03_TURN_4_NEITHER.prefab:ddb8aae686b170648898dc591fc7a554");
		base.PreloadSound("VO_LOE_03_TURN_3_WARNING.prefab:e9e9a86a32fba3842b95b38d71afe678");
		base.PreloadSound("VO_LOE_03_TURN_2.prefab:053b45bd9efaedb4f9178135644347b5");
		base.PreloadSound("VO_LOE_03_TURN_2_2.prefab:da4366528d8d3e84397dde52274585b0");
		base.PreloadSound("VO_LOE_03_TURN_4.prefab:d81cbf6a2f8657740a049208a3cc48e6");
		base.PreloadSound("VO_LOE_03_TURN_7.prefab:3ba8d9e054983934798a9ba027841605");
		base.PreloadSound("VO_LOE_03_TURN_7_2.prefab:79e0b4c1a04228749897dff2ca0d9edd");
		base.PreloadSound("VO_LOE_03_TURN_3_BOULDER.prefab:d633a3b18e9199b4d9f61b7fe8ce6527");
		base.PreloadSound("VO_LOE_03_TURN_1.prefab:90d2bc2b8b80a5444b9c15759d179dd7");
		base.PreloadSound("VO_LOE_03_TURN_8.prefab:7047eaf23edc34b42b0b171b7124d1b5");
		base.PreloadSound("VO_LOE_03_TURN_10.prefab:621285e424dd9e74c85573e03f34fb68");
		base.PreloadSound("VO_LOE_03_WIN.prefab:3b8d8d12b4f129c428c975ccc353a785");
		base.PreloadSound("VO_LOE_WING_1_WIN_2.prefab:db68235e589657e4ba8a94ff1458e299");
		base.PreloadSound("VO_LOE_03_RESPONSE.prefab:e38dfdb8a73972343b81c64ddb44171b");
	}

	// Token: 0x06003492 RID: 13458 RVA: 0x0010C978 File Offset: 0x0010AB78
	public override void NotifyOfMulliganInitialized()
	{
		base.NotifyOfMulliganInitialized();
		this.m_mostRecentMissionEvent = base.GetTag(GAME_TAG.MISSION_EVENT);
		this.InitVisuals();
	}

	// Token: 0x06003493 RID: 13459 RVA: 0x0010C993 File Offset: 0x0010AB93
	public override void NotifyOfMulliganEnded()
	{
		base.NotifyOfMulliganEnded();
		GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor().GetHealthObject().Hide();
	}

	// Token: 0x06003494 RID: 13460 RVA: 0x0010C9BC File Offset: 0x0010ABBC
	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.COST)
		{
			this.UpdateVisuals(change.newValue);
		}
	}

	// Token: 0x06003495 RID: 13461 RVA: 0x0010C9E8 File Offset: 0x0010ABE8
	public override string CustomChoiceBannerText()
	{
		if (base.GetTag<TAG_STEP>(GAME_TAG.STEP) == TAG_STEP.MAIN_START)
		{
			string text = null;
			int mostRecentMissionEvent = this.m_mostRecentMissionEvent;
			if (mostRecentMissionEvent != 4)
			{
				switch (mostRecentMissionEvent)
				{
				case 10:
					text = "MISSION_GLOWING_POOL";
					break;
				case 11:
					text = "MISSION_PIT_OF_SPIKES";
					break;
				case 12:
					text = "MISSION_TAKE_THE_SHORTCUT";
					break;
				}
			}
			else
			{
				text = "MISSION_STATUES_EYE";
			}
			if (text != null)
			{
				return GameStrings.Get(text);
			}
		}
		return null;
	}

	// Token: 0x06003496 RID: 13462 RVA: 0x0010CA4E File Offset: 0x0010AC4E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		this.m_mostRecentMissionEvent = missionEvent;
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		switch (missionEvent)
		{
		case 1:
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_5_4.prefab:d35f6f70b2c0eca44920996f1d1b280b", 3f, 1f, true, false));
			break;
		case 2:
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_6.prefab:43a3554df2b302c428fdc7325ab913ed", 3f, 1f, false, false));
			GameState.Get().SetBusy(false);
			break;
		case 3:
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_9.prefab:3f4b6112b2fea054693987a5cfbaf29b", 3f, 1f, false, false));
			break;
		case 4:
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_5.prefab:cd01ccc585ea5e541bb6d6bf014ab57f", 3f, 1f, true, false));
			GameState.Get().SetBusy(false);
			break;
		case 5:
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_4_GOOD.prefab:04e58ac2fa0d1874caa45fe4bd009c16", 3f, 1f, true, false));
			break;
		case 6:
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_4_BAD.prefab:2f27d2a958ffdd44387be7a1fe070234", 3f, 1f, false, false));
			break;
		case 7:
			while (GameState.Get().IsBusy())
			{
				yield return null;
			}
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_6_2.prefab:e648bc075f6c30249a9221100bec6c06", 3f, 1f, false, false));
			break;
		case 8:
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_4_NEITHER.prefab:ddb8aae686b170648898dc591fc7a554", 3f, 1f, false, false));
			break;
		case 9:
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_3_WARNING.prefab:e9e9a86a32fba3842b95b38d71afe678", 3f, 1f, true, false));
			GameState.Get().SetBusy(false);
			break;
		case 10:
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_2.prefab:053b45bd9efaedb4f9178135644347b5", 3f, 1f, true, false));
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_03_TURN_2_2.prefab:da4366528d8d3e84397dde52274585b0", 3f, 1f, false, false));
			break;
		case 11:
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_4.prefab:d81cbf6a2f8657740a049208a3cc48e6", 3f, 1f, true, false));
			break;
		case 12:
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_7.prefab:3ba8d9e054983934798a9ba027841605", 3f, 1f, true, false));
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_03_TURN_7_2.prefab:79e0b4c1a04228749897dff2ca0d9edd", 3f, 1f, false, false));
			break;
		case 13:
			GameState.Get().SetBusy(false);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_3_BOULDER.prefab:d633a3b18e9199b4d9f61b7fe8ce6527", 3f, 1f, true, false));
			GameState.Get().SetBusy(false);
			break;
		}
		yield break;
	}

	// Token: 0x06003497 RID: 13463 RVA: 0x0010CA64 File Offset: 0x0010AC64
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor().ActivateSpellDeathState(SpellType.IMMUNE);
		if (turn == 1)
		{
			int cost = base.GetCost();
			this.InitTurnCounter(cost);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_1.prefab:90d2bc2b8b80a5444b9c15759d179dd7", 3f, 1f, false, false));
		}
		if (turn % 2 == 0)
		{
			int cost2 = base.GetCost();
			if (cost2 != 1)
			{
				if (cost2 == 3)
				{
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_8.prefab:7047eaf23edc34b42b0b171b7124d1b5", 3f, 1f, false, false));
				}
			}
			else
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_TURN_10.prefab:621285e424dd9e74c85573e03f34fb68", 3f, 1f, false, false));
			}
		}
		yield break;
	}

	// Token: 0x06003498 RID: 13464 RVA: 0x0010CA7A File Offset: 0x0010AC7A
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_RESPONSE.prefab:e38dfdb8a73972343b81c64ddb44171b", 3f, 1f, true, false));
		}
	}

	// Token: 0x06003499 RID: 13465 RVA: 0x0010CAB0 File Offset: 0x0010ACB0
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a", "VO_LOE_03_WIN.prefab:3b8d8d12b4f129c428c975ccc353a785", 0f, false, false));
		}
		yield break;
	}

	// Token: 0x0600349A RID: 13466 RVA: 0x0010CAC8 File Offset: 0x0010ACC8
	private void InitVisuals()
	{
		int cost = base.GetCost();
		int tag = base.GetTag(GAME_TAG.TURN);
		this.InitTempleArt(cost);
		if (tag >= 1 && GameState.Get().IsPastBeginPhase())
		{
			this.InitTurnCounter(cost);
		}
	}

	// Token: 0x0600349B RID: 13467 RVA: 0x0010CB04 File Offset: 0x0010AD04
	private void InitTempleArt(int cost)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("TempleArt.prefab:c5d0fc0812982fc4ba576e2b0cdfa548", AssetLoadingOptions.None);
		this.m_templeArt = gameObject.GetComponent<TempleArt>();
		this.UpdateTempleArt(cost);
	}

	// Token: 0x0600349C RID: 13468 RVA: 0x0010CB3C File Offset: 0x0010AD3C
	private void InitTurnCounter(int cost)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("LOE_Turn_Timer.prefab:b05530aa55868554fb8f0c66632b3c22", AssetLoadingOptions.None);
		this.m_turnCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = this.m_turnCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmBool("RunningMan").Value = true;
		component.FsmVariables.GetFsmBool("MineCart").Value = false;
		component.SendEvent("Birth");
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_turnCounter.transform.parent = actor.gameObject.transform;
		this.m_turnCounter.transform.localPosition = new Vector3(-1.4f, 0.187f, -0.11f);
		this.m_turnCounter.transform.localScale = Vector3.one * 0.52f;
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x0600349D RID: 13469 RVA: 0x0010CC26 File Offset: 0x0010AE26
	private void UpdateVisuals(int cost)
	{
		this.UpdateTempleArt(cost);
		if (this.m_turnCounter)
		{
			this.UpdateTurnCounter(cost);
		}
	}

	// Token: 0x0600349E RID: 13470 RVA: 0x0010CC44 File Offset: 0x0010AE44
	private void UpdateTempleArt(int cost)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_templeArt.DoPortraitSwap(actor, cost);
	}

	// Token: 0x0600349F RID: 13471 RVA: 0x0010CC73 File Offset: 0x0010AE73
	private void UpdateTurnCounter(int cost)
	{
		this.m_turnCounter.GetComponent<PlayMakerFSM>().SendEvent("Action");
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x060034A0 RID: 13472 RVA: 0x0010CC94 File Offset: 0x0010AE94
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
		string headlineString = GameStrings.FormatPlurals("MISSION_DEFAULTCOUNTERNAME", pluralNumbers, Array.Empty<object>());
		this.m_turnCounter.ChangeDialogText(headlineString, cost.ToString(), "", "");
	}

	// Token: 0x04001CBB RID: 7355
	private static Map<GameEntityOption, bool> s_booleanOptions = LOE03_AncientTemple.InitBooleanOptions();

	// Token: 0x04001CBC RID: 7356
	private static Map<GameEntityOption, string> s_stringOptions = LOE03_AncientTemple.InitStringOptions();

	// Token: 0x04001CBD RID: 7357
	private Notification m_turnCounter;

	// Token: 0x04001CBE RID: 7358
	private TempleArt m_templeArt;

	// Token: 0x04001CBF RID: 7359
	private int m_mostRecentMissionEvent;
}
