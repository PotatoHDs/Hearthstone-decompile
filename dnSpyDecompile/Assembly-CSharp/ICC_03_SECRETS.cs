using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003A6 RID: 934
public class ICC_03_SECRETS : ICC_MissionEntity
{
	// Token: 0x06003577 RID: 13687 RVA: 0x0010C851 File Offset: 0x0010AA51
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

	// Token: 0x06003578 RID: 13688 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x06003579 RID: 13689 RVA: 0x0010FAC9 File Offset: 0x0010DCC9
	public ICC_03_SECRETS()
	{
		this.m_gameOptions.AddOptions(ICC_03_SECRETS.s_booleanOptions, ICC_03_SECRETS.s_stringOptions);
	}

	// Token: 0x0600357A RID: 13690 RVA: 0x0010FAE8 File Offset: 0x0010DCE8
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

	// Token: 0x0600357B RID: 13691 RVA: 0x0010FBDC File Offset: 0x0010DDDC
	public override void NotifyOfMulliganInitialized()
	{
		base.NotifyOfMulliganInitialized();
		this.m_mostRecentMissionEvent = base.GetTag(GAME_TAG.MISSION_EVENT);
		this.InitVisuals();
	}

	// Token: 0x0600357C RID: 13692 RVA: 0x0010C993 File Offset: 0x0010AB93
	public override void NotifyOfMulliganEnded()
	{
		base.NotifyOfMulliganEnded();
		GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor().GetHealthObject().Hide();
	}

	// Token: 0x0600357D RID: 13693 RVA: 0x0010FBF8 File Offset: 0x0010DDF8
	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.COST)
		{
			this.UpdateVisuals(change.newValue);
		}
	}

	// Token: 0x0600357E RID: 13694 RVA: 0x0010FC24 File Offset: 0x0010DE24
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

	// Token: 0x0600357F RID: 13695 RVA: 0x0010FC8A File Offset: 0x0010DE8A
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 1)
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayMissionFlavorLine("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_Sidekick_Mission03_01", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003580 RID: 13696 RVA: 0x0010FCA0 File Offset: 0x0010DEA0
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

	// Token: 0x06003581 RID: 13697 RVA: 0x0010CA7A File Offset: 0x0010AC7A
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_03_RESPONSE.prefab:e38dfdb8a73972343b81c64ddb44171b", 3f, 1f, true, false));
		}
	}

	// Token: 0x06003582 RID: 13698 RVA: 0x0010FCB6 File Offset: 0x0010DEB6
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a", "VO_LOE_03_WIN.prefab:3b8d8d12b4f129c428c975ccc353a785", 0f, false, false));
		}
		yield break;
	}

	// Token: 0x06003583 RID: 13699 RVA: 0x0010FCCC File Offset: 0x0010DECC
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

	// Token: 0x06003584 RID: 13700 RVA: 0x0010FD08 File Offset: 0x0010DF08
	private void InitTempleArt(int cost)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("TempleArt.prefab:c5d0fc0812982fc4ba576e2b0cdfa548", AssetLoadingOptions.None);
		this.m_templeArt = gameObject.GetComponent<TempleArt>();
		this.UpdateTempleArt(cost);
	}

	// Token: 0x06003585 RID: 13701 RVA: 0x0010FD40 File Offset: 0x0010DF40
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

	// Token: 0x06003586 RID: 13702 RVA: 0x0010FE2A File Offset: 0x0010E02A
	private void UpdateVisuals(int cost)
	{
		this.UpdateTempleArt(cost);
		if (this.m_turnCounter)
		{
			this.UpdateTurnCounter(cost);
		}
	}

	// Token: 0x06003587 RID: 13703 RVA: 0x0010FE48 File Offset: 0x0010E048
	private void UpdateTempleArt(int cost)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_templeArt.DoPortraitSwap(actor, cost);
	}

	// Token: 0x06003588 RID: 13704 RVA: 0x0010FE77 File Offset: 0x0010E077
	private void UpdateTurnCounter(int cost)
	{
		this.m_turnCounter.GetComponent<PlayMakerFSM>().SendEvent("Action");
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x06003589 RID: 13705 RVA: 0x0010FE98 File Offset: 0x0010E098
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

	// Token: 0x04001CEB RID: 7403
	private static Map<GameEntityOption, bool> s_booleanOptions = ICC_03_SECRETS.InitBooleanOptions();

	// Token: 0x04001CEC RID: 7404
	private static Map<GameEntityOption, string> s_stringOptions = ICC_03_SECRETS.InitStringOptions();

	// Token: 0x04001CED RID: 7405
	private Notification m_turnCounter;

	// Token: 0x04001CEE RID: 7406
	private TempleArt m_templeArt;

	// Token: 0x04001CEF RID: 7407
	private int m_mostRecentMissionEvent;
}
