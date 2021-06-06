using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200038E RID: 910
public class LOE07_MineCart : LOE_MissionEntity
{
	// Token: 0x060034A8 RID: 13480 RVA: 0x0010C851 File Offset: 0x0010AA51
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

	// Token: 0x060034A9 RID: 13481 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x060034AA RID: 13482 RVA: 0x0010CDE7 File Offset: 0x0010AFE7
	public LOE07_MineCart()
	{
		this.m_gameOptions.AddOptions(LOE07_MineCart.s_booleanOptions, LOE07_MineCart.s_stringOptions);
	}

	// Token: 0x060034AB RID: 13483 RVA: 0x0010CE0F File Offset: 0x0010B00F
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOE_Minecart);
	}

	// Token: 0x060034AC RID: 13484 RVA: 0x0010CE24 File Offset: 0x0010B024
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOE_07_START.prefab:9707703d728d10a47a13b7fb9b978c15");
		base.PreloadSound("VO_LOE_07_WIN.prefab:2eef218b9ec26a44cac30cbc83ea01b6");
		base.PreloadSound("VO_LOEA07_MINE_ARCHAEDAS.prefab:882b69209ca43844a9faa3767dc8fc84");
		base.PreloadSound("VO_LOEA07_MINE_DETONATE.prefab:761ba0f22ade3c64c8826d5823ba060f");
		base.PreloadSound("VO_LOEA07_MINE_RAMMING.prefab:a97ddf505676fc64db1bec8b236e3619");
		base.PreloadSound("VO_LOEA07_MINE_PARROT.prefab:101be2462fc0a964d8dc7b328be15e20");
		base.PreloadSound("VO_LOEA07_MINE_BOOMZOOKA.prefab:54d6205b2fe93334c8b32f302a7095e3");
		base.PreloadSound("VO_LOEA07_MINE_DYNAMITE.prefab:dc00acb77f796994ba54c000ab83a313");
		base.PreloadSound("VO_LOEA07_MINE_BOOM.prefab:a60ee4fd18095fc4795b96e72a3d30d9");
		base.PreloadSound("VO_LOEA07_MINE_BARREL_FORWARD.prefab:16e53571a0835b3459dac9228e127fc9");
		base.PreloadSound("VO_LOEA07_MINE_HUNKER_DOWN.prefab:f7ee4ae0725e8e44bafaf7918e5c8164");
		base.PreloadSound("VO_LOEA07_MINE_SPIKED_DECOY.prefab:0ee62cdb45e19e1499e78b4c23aa1ef2");
		base.PreloadSound("VO_LOEA07_MINE_REPAIRS.prefab:f933572c4ce958646ad8530cb42caabb");
		base.PreloadSound("VO_BRANN_WIN_07_ALT_07.prefab:69e827ff4bdd1074abec8904aadf9729");
		base.PreloadSound("VO_LOE_07_RESPONSE.prefab:2d967602b4fcf05478426a17998fe534");
		base.PreloadSound("Mine_response.prefab:5a1713d5acac1f34bacb52ef005eb417");
	}

	// Token: 0x060034AD RID: 13485 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	// Token: 0x060034AE RID: 13486 RVA: 0x0010CEE1 File Offset: 0x0010B0E1
	public override void NotifyOfMulliganInitialized()
	{
		base.NotifyOfMulliganInitialized();
		this.InitVisuals();
	}

	// Token: 0x060034AF RID: 13487 RVA: 0x0010C993 File Offset: 0x0010AB93
	public override void NotifyOfMulliganEnded()
	{
		base.NotifyOfMulliganEnded();
		GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor().GetHealthObject().Hide();
	}

	// Token: 0x060034B0 RID: 13488 RVA: 0x0010CEF0 File Offset: 0x0010B0F0
	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.COST && change.newValue != change.oldValue)
		{
			this.UpdateVisuals(change.newValue);
		}
	}

	// Token: 0x060034B1 RID: 13489 RVA: 0x0010CF2A File Offset: 0x0010B12A
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn != 1)
		{
			if (turn == 11)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOE_07_WIN.prefab:2eef218b9ec26a44cac30cbc83ea01b6", 3f, 1f, false, false));
			}
		}
		else
		{
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOE_07_START.prefab:9707703d728d10a47a13b7fb9b978c15", 3f, 1f, false, false));
		}
		yield break;
	}

	// Token: 0x060034B2 RID: 13490 RVA: 0x0010CF40 File Offset: 0x0010B140
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
			SoundManager.Get().LoadAndPlay("Mine_response.prefab:5a1713d5acac1f34bacb52ef005eb417", actor.gameObject);
		}
	}

	// Token: 0x060034B3 RID: 13491 RVA: 0x0010CF8E File Offset: 0x0010B18E
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
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		string cardId = entity.GetCardId();
		if (cardId == "LOEA07_16")
		{
			this.m_playedLines.Add(cardId);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_ARCHAEDAS.prefab:882b69209ca43844a9faa3767dc8fc84", 3f, 1f, false, false));
		}
		yield break;
	}

	// Token: 0x060034B4 RID: 13492 RVA: 0x0010CFA4 File Offset: 0x0010B1A4
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		string cardId = entity.GetCardId();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 4075521742U)
		{
			if (num <= 1760063225U)
			{
				if (num != 1743285606U)
				{
					if (num == 1760063225U)
					{
						if (cardId == "LOEA07_18")
						{
							this.m_playedLines.Add(cardId);
							yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_DYNAMITE.prefab:dc00acb77f796994ba54c000ab83a313", 3f, 1f, false, false));
						}
					}
				}
				else if (cardId == "LOEA07_19")
				{
					this.m_playedLines.Add(cardId);
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_RAMMING.prefab:a97ddf505676fc64db1bec8b236e3619", 3f, 1f, false, false));
				}
			}
			else if (num != 3740116457U)
			{
				if (num != 4008411266U)
				{
					if (num == 4075521742U)
					{
						if (cardId == "LOEA07_24")
						{
							this.m_playedLines.Add(cardId);
							yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_SPIKED_DECOY.prefab:0ee62cdb45e19e1499e78b4c23aa1ef2", 3f, 1f, false, false));
						}
					}
				}
				else if (cardId == "LOEA07_28")
				{
					this.m_playedLines.Add(cardId);
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_REPAIRS.prefab:f933572c4ce958646ad8530cb42caabb", 3f, 1f, false, false));
				}
			}
			else if (cardId == "LOEA07_32")
			{
				this.m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_BOOMZOOKA.prefab:54d6205b2fe93334c8b32f302a7095e3", 3f, 1f, false, false));
			}
		}
		else if (num <= 4109076980U)
		{
			if (num != 4092299361U)
			{
				if (num == 4109076980U)
				{
					if (cardId == "LOEA07_22")
					{
						this.m_playedLines.Add(cardId);
						yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_HUNKER_DOWN.prefab:f7ee4ae0725e8e44bafaf7918e5c8164", 3f, 1f, false, false));
					}
				}
			}
			else if (cardId == "LOEA07_25")
			{
				this.m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_PARROT.prefab:101be2462fc0a964d8dc7b328be15e20", 3f, 1f, false, false));
			}
		}
		else if (num != 4125854599U)
		{
			if (num != 4142632218U)
			{
				if (num == 4159409837U)
				{
					if (cardId == "LOEA07_21")
					{
						this.m_playedLines.Add(cardId);
						yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_BARREL_FORWARD.prefab:16e53571a0835b3459dac9228e127fc9", 3f, 1f, false, false));
					}
				}
			}
			else if (cardId == "LOEA07_20")
			{
				this.m_playedLines.Add(cardId);
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_BOOM.prefab:a60ee4fd18095fc4795b96e72a3d30d9", 3f, 1f, false, false));
			}
		}
		else if (cardId == "LOEA07_23")
		{
			this.m_playedLines.Add(cardId);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA07_MINE_DETONATE.prefab:761ba0f22ade3c64c8826d5823ba060f", 3f, 1f, false, false));
		}
		yield break;
	}

	// Token: 0x060034B5 RID: 13493 RVA: 0x0010CFBA File Offset: 0x0010B1BA
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Brann_Quote.prefab:2c11651ab7740924189734944b8d7089", "VO_BRANN_WIN_07_ALT_07.prefab:69e827ff4bdd1074abec8904aadf9729", 0f, false, false));
		}
		yield break;
	}

	// Token: 0x060034B6 RID: 13494 RVA: 0x0010CFD0 File Offset: 0x0010B1D0
	private void InitVisuals()
	{
		int cost = base.GetCost();
		this.InitMineCartArt();
		this.InitTurnCounter(cost);
	}

	// Token: 0x060034B7 RID: 13495 RVA: 0x0010CFF4 File Offset: 0x0010B1F4
	private void InitMineCartArt()
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("MineCartRushArt.prefab:77a3c4475a44dac4a91cef7cc9c621e5", AssetLoadingOptions.None);
		this.m_mineCartArt = gameObject.GetComponent<MineCartRushArt>();
	}

	// Token: 0x060034B8 RID: 13496 RVA: 0x0010D024 File Offset: 0x0010B224
	private void InitTurnCounter(int cost)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("LOE_Turn_Timer.prefab:b05530aa55868554fb8f0c66632b3c22", AssetLoadingOptions.None);
		this.m_turnCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = this.m_turnCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmBool("RunningMan").Value = false;
		component.FsmVariables.GetFsmBool("MineCart").Value = true;
		component.SendEvent("Birth");
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_turnCounter.transform.parent = actor.gameObject.transform;
		this.m_turnCounter.transform.localPosition = new Vector3(-1.4f, 0.187f, -0.11f);
		this.m_turnCounter.transform.localScale = Vector3.one * 0.52f;
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x060034B9 RID: 13497 RVA: 0x0010D10E File Offset: 0x0010B30E
	private void UpdateVisuals(int cost)
	{
		this.UpdateMineCartArt();
		this.UpdateTurnCounter(cost);
	}

	// Token: 0x060034BA RID: 13498 RVA: 0x0010D120 File Offset: 0x0010B320
	private void UpdateMineCartArt()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_mineCartArt.DoPortraitSwap(actor);
	}

	// Token: 0x060034BB RID: 13499 RVA: 0x0010D14E File Offset: 0x0010B34E
	private void UpdateTurnCounter(int cost)
	{
		this.m_turnCounter.GetComponent<PlayMakerFSM>().SendEvent("Action");
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x060034BC RID: 13500 RVA: 0x0010D16C File Offset: 0x0010B36C
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

	// Token: 0x04001CC0 RID: 7360
	private static Map<GameEntityOption, bool> s_booleanOptions = LOE07_MineCart.InitBooleanOptions();

	// Token: 0x04001CC1 RID: 7361
	private static Map<GameEntityOption, string> s_stringOptions = LOE07_MineCart.InitStringOptions();

	// Token: 0x04001CC2 RID: 7362
	private Notification m_turnCounter;

	// Token: 0x04001CC3 RID: 7363
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001CC4 RID: 7364
	private MineCartRushArt m_mineCartArt;
}
