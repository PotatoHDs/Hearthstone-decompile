using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D7 RID: 1239
public class DRGA_Evil_Fight_09 : DRGA_Dungeon
{
	// Token: 0x0600425C RID: 16988 RVA: 0x0010C851 File Offset: 0x0010AA51
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

	// Token: 0x0600425D RID: 16989 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x0600425E RID: 16990 RVA: 0x00163E1C File Offset: 0x0016201C
	public DRGA_Evil_Fight_09()
	{
		this.m_gameOptions.AddOptions(DRGA_Evil_Fight_09.s_booleanOptions, DRGA_Evil_Fight_09.s_stringOptions);
	}

	// Token: 0x0600425F RID: 16991 RVA: 0x00163EF0 File Offset: 0x001620F0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn02_Event_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn04_Event_02_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_02_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_03_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn10_Event_02_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Victory_02_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_02_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_03_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_02_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn01_Event_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn02_Event_02_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_02_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn04_Event_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn05_Event_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn06_Event_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn07_Event_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn08_Event_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn09_Event_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn10_Event_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Victory_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_Boss_Death_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossAttack_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossStart_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_01_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_02_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_03_01,
			DRGA_Evil_Fight_09.VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_EmoteResponse_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004260 RID: 16992 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004261 RID: 16993 RVA: 0x00164134 File Offset: 0x00162334
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Evil_Fight_09.VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_Boss_Death_01;
	}

	// Token: 0x06004262 RID: 16994 RVA: 0x0016414C File Offset: 0x0016234C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_09.VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_09.VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004263 RID: 16995 RVA: 0x001641DD File Offset: 0x001623DD
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn01_Event_01_01, 2.5f);
				goto IL_649;
			}
			goto IL_649;
		case 101:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, DRGA_Evil_Fight_09.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn02_Event_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn02_Event_02_01, 2.5f);
				goto IL_649;
			}
			goto IL_649;
		case 102:
		case 104:
		case 106:
		case 110:
		case 111:
		case 115:
		case 117:
		case 118:
		case 119:
			break;
		case 103:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_02_01, 2.5f);
				goto IL_649;
			}
			goto IL_649;
		case 105:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn04_Event_01_01, 2.5f);
				yield return base.PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, DRGA_Evil_Fight_09.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn04_Event_02_01, 2.5f);
				goto IL_649;
			}
			goto IL_649;
		case 107:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn05_Event_01_01, 2.5f);
				goto IL_649;
			}
			goto IL_649;
		case 108:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn06_Event_01_01, 2.5f);
				goto IL_649;
			}
			goto IL_649;
		case 109:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn07_Event_01_01, 2.5f);
				yield return base.PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, DRGA_Evil_Fight_09.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_02_01, 2.5f);
				yield return base.PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, DRGA_Evil_Fight_09.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_03_01, 2.5f);
				goto IL_649;
			}
			goto IL_649;
		case 112:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn08_Event_01_01, 2.5f);
				goto IL_649;
			}
			goto IL_649;
		case 113:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn09_Event_01_01, 2.5f);
				goto IL_649;
			}
			goto IL_649;
		case 114:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn10_Event_01_01, 2.5f);
				yield return base.PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, DRGA_Evil_Fight_09.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn10_Event_02_01, 2.5f);
				goto IL_649;
			}
			goto IL_649;
		case 116:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Victory_01_01, 2.5f);
				yield return base.PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, DRGA_Evil_Fight_09.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Victory_02_01, 2.5f);
				goto IL_649;
			}
			goto IL_649;
		case 120:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_missionEventTrigger120Lines);
			goto IL_649;
		case 121:
			if (!this.m_Heroic)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestructLines);
				goto IL_649;
			}
			goto IL_649;
		default:
			if (missionEvent == 251)
			{
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPowerLines);
					goto IL_649;
				}
				goto IL_649;
			}
			break;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_649:
		yield break;
	}

	// Token: 0x06004264 RID: 16996 RVA: 0x001641F3 File Offset: 0x001623F3
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
		yield break;
	}

	// Token: 0x06004265 RID: 16997 RVA: 0x00164209 File Offset: 0x00162409
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
		yield break;
	}

	// Token: 0x06004266 RID: 16998 RVA: 0x0016421F File Offset: 0x0016241F
	public override void NotifyOfMulliganEnded()
	{
		base.NotifyOfMulliganEnded();
		this.InitVisuals();
	}

	// Token: 0x06004267 RID: 16999 RVA: 0x00164230 File Offset: 0x00162430
	private void InitVisuals()
	{
		int cost = base.GetCost();
		this.InitTurnCounter(cost);
	}

	// Token: 0x06004268 RID: 17000 RVA: 0x0016424C File Offset: 0x0016244C
	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.COST && change.newValue != change.oldValue)
		{
			this.UpdateVisuals(change.newValue);
		}
	}

	// Token: 0x06004269 RID: 17001 RVA: 0x00164288 File Offset: 0x00162488
	private void InitTurnCounter(int cost)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("DRG_Airship_Turn_Timer.prefab:d29fadcd008628c408fe1d26485ea2b9", AssetLoadingOptions.None);
		this.m_turnCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = this.m_turnCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmBool("RunningMan").Value = false;
		component.FsmVariables.GetFsmBool("MineCart").Value = false;
		component.FsmVariables.GetFsmBool("Airship").Value = true;
		component.SendEvent("Birth");
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_turnCounter.transform.parent = actor.gameObject.transform;
		this.m_turnCounter.transform.localPosition = new Vector3(-1.4f, 0.187f, -0.11f);
		this.m_turnCounter.transform.localScale = Vector3.one * 0.52f;
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x0600426A RID: 17002 RVA: 0x00164388 File Offset: 0x00162588
	private void UpdateVisuals(int cost)
	{
		this.UpdateTurnCounter(cost);
	}

	// Token: 0x0600426B RID: 17003 RVA: 0x00164394 File Offset: 0x00162594
	private void UpdateMineCartArt()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_mineCartArt.DoPortraitSwap(actor);
	}

	// Token: 0x0600426C RID: 17004 RVA: 0x001643C2 File Offset: 0x001625C2
	private void UpdateTurnCounter(int cost)
	{
		this.m_turnCounter.GetComponent<PlayMakerFSM>().SendEvent("Action");
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x0600426D RID: 17005 RVA: 0x001643E0 File Offset: 0x001625E0
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
		string headlineString = GameStrings.FormatPlurals("MISSION_AIRSHIPCOUNTERNAME", pluralNumbers, Array.Empty<object>());
		this.m_turnCounter.ChangeDialogText(headlineString, cost.ToString(), "", "");
	}

	// Token: 0x0400320F RID: 12815
	private static Map<GameEntityOption, bool> s_booleanOptions = DRGA_Evil_Fight_09.InitBooleanOptions();

	// Token: 0x04003210 RID: 12816
	private static Map<GameEntityOption, string> s_stringOptions = DRGA_Evil_Fight_09.InitStringOptions();

	// Token: 0x04003211 RID: 12817
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn02_Event_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn02_Event_01_01.prefab:c098f600a91794847b963864b4123d70");

	// Token: 0x04003212 RID: 12818
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn04_Event_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn04_Event_02_01.prefab:0b633b6af9f7598419df0f5e011f3713");

	// Token: 0x04003213 RID: 12819
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_02_01.prefab:dbceb85b58708dd4e8ee164969943b8b");

	// Token: 0x04003214 RID: 12820
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn07_Event_03_01.prefab:36eeafd146bb8c14f95d9fec522ebe5e");

	// Token: 0x04003215 RID: 12821
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn10_Event_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Turn10_Event_02_01.prefab:c87f4c39020589940bc1dbb5e5d49c25");

	// Token: 0x04003216 RID: 12822
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Victory_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_09_Victory_02_01.prefab:4695c80cb7c22f2438e5c51b1ae05d84");

	// Token: 0x04003217 RID: 12823
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_01_01.prefab:8b47a4fdcdcd6f4478956144fd02b25e");

	// Token: 0x04003218 RID: 12824
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_02_01.prefab:1069f930a320d7d4ea605301b1a1b72b");

	// Token: 0x04003219 RID: 12825
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_03_01.prefab:299a1799efb19f84eb0fbb804c26407f");

	// Token: 0x0400321A RID: 12826
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_01_01.prefab:69f229b2265870a4d821f2f1492bc3bc");

	// Token: 0x0400321B RID: 12827
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_02_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_02_01.prefab:ad778543779b586498c2bad181c449f9");

	// Token: 0x0400321C RID: 12828
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn01_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn01_Event_01_01.prefab:7d0f6cce02a5d8445bcec173bd5739c1");

	// Token: 0x0400321D RID: 12829
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn02_Event_02_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn02_Event_02_01.prefab:83cbae8952bb1e04ea1c10838a26d242");

	// Token: 0x0400321E RID: 12830
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_01_01.prefab:c43f2da90e28bae4bad02294371490d1");

	// Token: 0x0400321F RID: 12831
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_02_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn03_Event_02_01.prefab:6aa67d702fb162846a5cef0d1ad4bf84");

	// Token: 0x04003220 RID: 12832
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn04_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn04_Event_01_01.prefab:e9cee199fb8702a44834e6d8b749cbb4");

	// Token: 0x04003221 RID: 12833
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn05_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn05_Event_01_01.prefab:dda018f33f140154d96f0604dd6e1981");

	// Token: 0x04003222 RID: 12834
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn06_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn06_Event_01_01.prefab:9b2cdaaf97642654b91c51935e6ab243");

	// Token: 0x04003223 RID: 12835
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn07_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn07_Event_01_01.prefab:cd6946e744c16cb4ba680cc48e0efa3f");

	// Token: 0x04003224 RID: 12836
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn08_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn08_Event_01_01.prefab:67200a9c2a55ab74ab6ffca10fb89910");

	// Token: 0x04003225 RID: 12837
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn09_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn09_Event_01_01.prefab:5044542e0b9c95f40926d9b8b198872a");

	// Token: 0x04003226 RID: 12838
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn10_Event_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Turn10_Event_01_01.prefab:f51e17efc85119e42b269f844ecffca8");

	// Token: 0x04003227 RID: 12839
	private static readonly AssetReference VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Victory_01_01 = new AssetReference("VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_Victory_01_01.prefab:b3820d6b62883b44fa92c064ee67f5f8");

	// Token: 0x04003228 RID: 12840
	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_Boss_Death_01.prefab:0dd77a6081e33d746ac6c5f3d8999260");

	// Token: 0x04003229 RID: 12841
	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossAttack_01.prefab:64cef6b12b8548a408176108ff6c3cd8");

	// Token: 0x0400322A RID: 12842
	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossStart_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_BossStart_01.prefab:c885c65a55b66fc4bba39c8c460a3a8f");

	// Token: 0x0400322B RID: 12843
	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_01_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_01_01.prefab:e6d9064529fbdb041a29e231378d17ee");

	// Token: 0x0400322C RID: 12844
	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_02_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_02_01.prefab:3baa599da06ca3642b57c14281d2d56c");

	// Token: 0x0400322D RID: 12845
	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_03_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_03_01.prefab:1f39731113c03a847976d11bf92a9cd4");

	// Token: 0x0400322E RID: 12846
	private static readonly AssetReference VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_EmoteResponse_01.prefab:32afd2cd65439ee47961c0b6cf918b2f");

	// Token: 0x0400322F RID: 12847
	private List<string> m_missionEventTrigger120Lines = new List<string>
	{
		DRGA_Evil_Fight_09.VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_01_01,
		DRGA_Evil_Fight_09.VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_02_01,
		DRGA_Evil_Fight_09.VO_DRGA_BOSS_32h_Male_Dragon_Evil_Fight_09_DragonDies_03_01
	};

	// Token: 0x04003230 RID: 12848
	private List<string> m_VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPowerLines = new List<string>
	{
		DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_01_01,
		DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_02_01,
		DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_HeroPower_03_01
	};

	// Token: 0x04003231 RID: 12849
	private List<string> m_VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestructLines = new List<string>
	{
		DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_01_01,
		DRGA_Evil_Fight_09.VO_DRGA_BOSS_14h_Female_Vulpera_Evil_Fight_09_SelfDestruct_02_01
	};

	// Token: 0x04003232 RID: 12850
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04003233 RID: 12851
	private Notification m_turnCounter;

	// Token: 0x04003234 RID: 12852
	private MineCartRushArt m_mineCartArt;
}
