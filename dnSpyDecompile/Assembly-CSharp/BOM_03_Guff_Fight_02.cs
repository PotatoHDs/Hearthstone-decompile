using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000574 RID: 1396
public class BOM_03_Guff_Fight_02 : BOM_03_Guff_Dungeon
{
	// Token: 0x06004DA8 RID: 19880 RVA: 0x0019A6CC File Offset: 0x001988CC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_03_Guff_Fight_02.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeA_Brukan_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeD_Brukan_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeF_Brukan_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeG_Brukan_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeA_Dawngrasp_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeD_Dawngrasp_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeF_Dawngrasp_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeG_Dawngrasp_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2Victory_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeA_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeB_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeB_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeC_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeD_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeF_03,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Brukan_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Brukan_03,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Dawngrasp_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Dawngrasp_03,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Rokara_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Rokara_03,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Tamsin_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Tamsin_03,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2Intro_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2Victory_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeA_Rokara_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeD_Rokara_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeF_Rokara_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeG_Rokara_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeA_Tamsin_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeD_Tamsin_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeF_Tamsin_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeG_Tamsin_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeG_Tamsin_04,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Death_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2EmoteResponse_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2ExchangeC_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2ExchangeE_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2ExchangeF_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2HeroPower_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2HeroPower_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Idle_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Idle_02,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Idle_03,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Loss_01,
			BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission1Intro_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004DA9 RID: 19881 RVA: 0x0019A49C File Offset: 0x0019869C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
	}

	// Token: 0x06004DAA RID: 19882 RVA: 0x0019AA10 File Offset: 0x00198C10
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (missionEvent <= 101)
		{
			if (missionEvent != 100)
			{
				if (missionEvent == 101)
				{
					GameState.Get().SetBusy(true);
					yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2ExchangeF_01);
					if (this.HeroPowerBrukan)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeF_Brukan_02);
					}
					if (this.HeroPowerDawngrasp)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeF_Dawngrasp_02);
					}
					if (this.HeroPowerRokara)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeF_Rokara_02);
					}
					if (this.HeroPowerTamsin)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeF_Tamsin_02);
					}
					GameState.Get().SetBusy(false);
					goto IL_4C5;
				}
			}
			else
			{
				if (base.shouldPlayBanterVO())
				{
					yield return base.MissionPlayVOOnce(enemyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2ExchangeE_01);
					goto IL_4C5;
				}
				goto IL_4C5;
			}
		}
		else
		{
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2Victory_01);
				if (this.HeroPowerDawngrasp)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2Victory_02);
				}
				else
				{
					yield return base.MissionPlayVO(this.Dawngrasp_BrassRing, BOM_03_Guff_Fight_02.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2Victory_02);
				}
				GameState.Get().SetBusy(false);
				goto IL_4C5;
			}
			if (missionEvent == 506)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Loss_01);
				GameState.Get().SetBusy(false);
				goto IL_4C5;
			}
			switch (missionEvent)
			{
			case 510:
				yield return base.MissionPlayVO(enemyActor, this.m_InGame_BossUsesHeroPower);
				goto IL_4C5;
			case 514:
				yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2Intro_01);
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission1Intro_02);
				goto IL_4C5;
			case 515:
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2EmoteResponse_01);
				goto IL_4C5;
			case 516:
				yield return base.MissionPlaySound(enemyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Death_01);
				goto IL_4C5;
			case 517:
				yield return base.MissionPlayVO(enemyActor, this.m_InGame_BossIdle);
				goto IL_4C5;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_4C5:
		yield break;
	}

	// Token: 0x06004DAB RID: 19883 RVA: 0x0019AA26 File Offset: 0x00198C26
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

	// Token: 0x06004DAC RID: 19884 RVA: 0x0019AA3C File Offset: 0x00198C3C
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

	// Token: 0x06004DAD RID: 19885 RVA: 0x0019AA52 File Offset: 0x00198C52
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn != 5)
			{
				switch (turn)
				{
				case 9:
					yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2ExchangeC_01);
					yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeC_02);
					break;
				case 11:
					if (this.HeroPowerBrukan)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeD_Brukan_01);
					}
					if (this.HeroPowerDawngrasp)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeD_Dawngrasp_01);
					}
					if (this.HeroPowerRokara)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeD_Rokara_01);
					}
					if (this.HeroPowerTamsin)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeD_Tamsin_01);
					}
					yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeD_02);
					break;
				case 13:
					if (this.HeroPowerBrukan)
					{
						yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Brukan_01);
					}
					if (this.HeroPowerDawngrasp)
					{
						yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Dawngrasp_01);
					}
					if (this.HeroPowerTamsin)
					{
						yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Tamsin_01);
					}
					if (this.HeroPowerRokara)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeG_Rokara_01);
					}
					if (this.HeroPowerBrukan)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeG_Brukan_02);
					}
					if (this.HeroPowerDawngrasp)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeG_Dawngrasp_02);
					}
					if (this.HeroPowerRokara)
					{
						yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Rokara_02);
					}
					if (this.HeroPowerTamsin)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeG_Tamsin_02);
					}
					if (this.HeroPowerBrukan)
					{
						yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Brukan_03);
					}
					if (this.HeroPowerDawngrasp)
					{
						yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Dawngrasp_03);
					}
					if (this.HeroPowerRokara)
					{
						yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Rokara_03);
					}
					if (this.HeroPowerTamsin)
					{
						yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Tamsin_03);
					}
					if (this.HeroPowerTamsin)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeG_Tamsin_04);
					}
					break;
				}
			}
			else
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeB_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeB_02);
			}
		}
		else
		{
			if (this.HeroPowerBrukan)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeA_Brukan_01);
			}
			if (this.HeroPowerDawngrasp)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeA_Dawngrasp_01);
			}
			if (this.HeroPowerRokara)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeA_Rokara_01);
			}
			if (this.HeroPowerTamsin)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeA_Tamsin_01);
			}
			yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_02.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeA_01);
		}
		yield break;
	}

	// Token: 0x06004DAE RID: 19886 RVA: 0x0019AA68 File Offset: 0x00198C68
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
		string headlineString = GameStrings.FormatPlurals("BOM_GUFF_02", pluralNumbers, Array.Empty<object>());
		this.m_turnCounter.ChangeDialogText(headlineString, cost.ToString(), "", "");
	}

	// Token: 0x06004DAF RID: 19887 RVA: 0x0019AAC0 File Offset: 0x00198CC0
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

	// Token: 0x06004DB0 RID: 19888 RVA: 0x0019ABD8 File Offset: 0x00198DD8
	private void InitVisuals()
	{
		int cost = base.GetCost();
		this.InitTurnCounter(cost);
	}

	// Token: 0x06004DB1 RID: 19889 RVA: 0x0019ABF3 File Offset: 0x00198DF3
	public override void NotifyOfMulliganEnded()
	{
		base.NotifyOfMulliganEnded();
		this.InitVisuals();
	}

	// Token: 0x06004DB2 RID: 19890 RVA: 0x0019AC01 File Offset: 0x00198E01
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

	// Token: 0x06004DB3 RID: 19891 RVA: 0x0019AC34 File Offset: 0x00198E34
	private void UpdateVisuals(int cost)
	{
		this.UpdateTurnCounter(cost);
	}

	// Token: 0x06004DB4 RID: 19892 RVA: 0x0019AC40 File Offset: 0x00198E40
	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.COST && change.newValue != change.oldValue)
		{
			this.UpdateVisuals(change.newValue);
		}
	}

	// Token: 0x040043E7 RID: 17383
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeA_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeA_Brukan_01.prefab:4048abecc5f5d9e4b9c3b502ed28ffa8");

	// Token: 0x040043E8 RID: 17384
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeD_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeD_Brukan_01.prefab:565147a0bf7bcaa45bd43c847a9be39d");

	// Token: 0x040043E9 RID: 17385
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeF_Brukan_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeF_Brukan_02.prefab:93f10c08ef9a7be4b8807a7b932fffc3");

	// Token: 0x040043EA RID: 17386
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeG_Brukan_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2ExchangeG_Brukan_02.prefab:123e7d8e8400b0a469009b6e6256a775");

	// Token: 0x040043EB RID: 17387
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeA_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeA_Dawngrasp_01.prefab:9114b5c918b70a24fa981b6eeafabe86");

	// Token: 0x040043EC RID: 17388
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeD_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeD_Dawngrasp_01.prefab:917b11e0abbb48f47bb0812b5322d853");

	// Token: 0x040043ED RID: 17389
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeF_Dawngrasp_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeF_Dawngrasp_02.prefab:197d2515b69357e48aece71eaec3b50f");

	// Token: 0x040043EE RID: 17390
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeG_Dawngrasp_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2ExchangeG_Dawngrasp_02.prefab:5265fcb5214fc0e44bd7f53049e43d1d");

	// Token: 0x040043EF RID: 17391
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2Victory_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2Victory_02.prefab:b05764f2716d66747b5dd6aad46e8f85");

	// Token: 0x040043F0 RID: 17392
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeA_01.prefab:a4ef7d2aea7a0b94f958a175f718cabe");

	// Token: 0x040043F1 RID: 17393
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeB_01.prefab:eec31bf76b1ef8d4b9e78fa00c335d09");

	// Token: 0x040043F2 RID: 17394
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeB_02.prefab:6fb2021a95cae5c4781942644fd847b8");

	// Token: 0x040043F3 RID: 17395
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeC_02.prefab:70fe3692347fdbe46acba10b23147330");

	// Token: 0x040043F4 RID: 17396
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeD_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeD_02.prefab:15eea1277afd90f43907a605cbae14a9");

	// Token: 0x040043F5 RID: 17397
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeF_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeF_03.prefab:26fd16064dd46684da43f095cd07842d");

	// Token: 0x040043F6 RID: 17398
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Brukan_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Brukan_01.prefab:6560a1f68a6984a4db44f3beed050da7");

	// Token: 0x040043F7 RID: 17399
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Brukan_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Brukan_03.prefab:acfc74424c3cec248b051c6546b9a649");

	// Token: 0x040043F8 RID: 17400
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Dawngrasp_01.prefab:8b843f6c8dfa713408ae5c790906d9e0");

	// Token: 0x040043F9 RID: 17401
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Dawngrasp_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Dawngrasp_03.prefab:24578ab27cbb0d047b2d969bd58320f3");

	// Token: 0x040043FA RID: 17402
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Rokara_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Rokara_02.prefab:f55ce1c57ba84664d9bc31127de3dd03");

	// Token: 0x040043FB RID: 17403
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Rokara_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Rokara_03.prefab:905827b20a57b564087e796090a55f21");

	// Token: 0x040043FC RID: 17404
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Tamsin_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Tamsin_01.prefab:b6c40c2b92c012a4f8fbfff313451169");

	// Token: 0x040043FD RID: 17405
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Tamsin_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2ExchangeG_Tamsin_03.prefab:f8689b519590e6d42bd68d3c123bda85");

	// Token: 0x040043FE RID: 17406
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2Intro_01.prefab:663e6d90e3ac35f42b6c90740cf2c3de");

	// Token: 0x040043FF RID: 17407
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission2Victory_01.prefab:208d9d9ab5ae34d45a6028fa22ff23ec");

	// Token: 0x04004400 RID: 17408
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeA_Rokara_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeA_Rokara_01.prefab:94705348fc7268540beb369fc920af57");

	// Token: 0x04004401 RID: 17409
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeD_Rokara_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeD_Rokara_01.prefab:116a8bad22ddf0d47aa4145953283785");

	// Token: 0x04004402 RID: 17410
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeF_Rokara_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeF_Rokara_02.prefab:fd1091c831e18a346b42aa7c7d717712");

	// Token: 0x04004403 RID: 17411
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeG_Rokara_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2ExchangeG_Rokara_01.prefab:a96c818325485c24f9618c1736897b56");

	// Token: 0x04004404 RID: 17412
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeA_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeA_Tamsin_01.prefab:04df5c8a687374144a3e28429e6070f7");

	// Token: 0x04004405 RID: 17413
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeD_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeD_Tamsin_01.prefab:35ccb5968b9445f43b8d2af2b08a1dd2");

	// Token: 0x04004406 RID: 17414
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeF_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeF_Tamsin_02.prefab:418486aff3a268c489665365412411eb");

	// Token: 0x04004407 RID: 17415
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeG_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeG_Tamsin_02.prefab:e2acfe14e8f78fd4d80883040987b263");

	// Token: 0x04004408 RID: 17416
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeG_Tamsin_04 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2ExchangeG_Tamsin_04.prefab:0a57d476318b0a14f9e8fbb667e4bf95");

	// Token: 0x04004409 RID: 17417
	private static readonly AssetReference VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Death_01 = new AssetReference("VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Death_01.prefab:d13da2377cb72f342abc00ca0643d762");

	// Token: 0x0400440A RID: 17418
	private static readonly AssetReference VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2EmoteResponse_01.prefab:0fadc5a7770d93c46926f8dba2549b71");

	// Token: 0x0400440B RID: 17419
	private static readonly AssetReference VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2ExchangeC_01.prefab:9c281132a6c798f4291cefc779277ee8");

	// Token: 0x0400440C RID: 17420
	private static readonly AssetReference VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2ExchangeE_01 = new AssetReference("VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2ExchangeE_01.prefab:5f05714849fc5154dac6eb1b96e7c4b9");

	// Token: 0x0400440D RID: 17421
	private static readonly AssetReference VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2ExchangeF_01 = new AssetReference("VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2ExchangeF_01.prefab:7fa0be704639ea14fbdad9906d7abf76");

	// Token: 0x0400440E RID: 17422
	private static readonly AssetReference VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2HeroPower_01.prefab:c44d8017ffae8cf4e828d751a94eab5c");

	// Token: 0x0400440F RID: 17423
	private static readonly AssetReference VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2HeroPower_02.prefab:898f15f448bae1644a48af0f1c17334d");

	// Token: 0x04004410 RID: 17424
	private static readonly AssetReference VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Idle_01.prefab:f7918814131d1eb48944e3a1cb791ff2");

	// Token: 0x04004411 RID: 17425
	private static readonly AssetReference VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Idle_02.prefab:9585b601489e4e54ab83a2cec9f51ee0");

	// Token: 0x04004412 RID: 17426
	private static readonly AssetReference VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Idle_03.prefab:d00ed71232615a141a82e2c1bdc7a7de");

	// Token: 0x04004413 RID: 17427
	private static readonly AssetReference VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Loss_01 = new AssetReference("VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Loss_01.prefab:aecc46cfa733fd14eada7b4b3f68484f");

	// Token: 0x04004414 RID: 17428
	private static readonly AssetReference VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission1Intro_02.prefab:4cb8d27be7ad7954d8525dec402b45f2");

	// Token: 0x04004415 RID: 17429
	private List<string> m_InGame_BossIdle = new List<string>
	{
		BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Idle_01,
		BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Idle_02,
		BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2Idle_03
	};

	// Token: 0x04004416 RID: 17430
	private List<string> m_InGame_BossUsesHeroPower = new List<string>
	{
		BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2HeroPower_01,
		BOM_03_Guff_Fight_02.VO_Story_Hero_Vapos_Male_Elemental_Story_Guff_Mission2HeroPower_02
	};

	// Token: 0x04004417 RID: 17431
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04004418 RID: 17432
	private Notification m_turnCounter;
}
