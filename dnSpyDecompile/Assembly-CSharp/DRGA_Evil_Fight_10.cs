using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D8 RID: 1240
public class DRGA_Evil_Fight_10 : DRGA_Dungeon
{
	// Token: 0x06004272 RID: 17010 RVA: 0x0016461C File Offset: 0x0016281C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_10_Turn_03_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Death_George_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_01_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_02_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_03_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_George_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_George_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Karl_Dies_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossAttack_George_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStart_01_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStartHeroic_01_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_EmoteResponse_George_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_01_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_02_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_01_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_02_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_03_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_04_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_05_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_01_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_02_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_Death_Karl_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_GeorgeDies_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_01_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_02_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_03_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossAttack_Karl_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStart_02_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStartHeroic_02_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_EmoteResponse_Karl_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_01_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_02_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_LeagueOfExplorers_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_01_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_02_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_03_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_04_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_05_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_06_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_01_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_02_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_EmoteResponseALT_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_01_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_02_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_03_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_04_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_05_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_06_01,
			DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_Transform_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004273 RID: 17011 RVA: 0x001649B0 File Offset: 0x00162BB0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Death_George_01;
	}

	// Token: 0x06004274 RID: 17012 RVA: 0x001649C8 File Offset: 0x00162BC8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Gameplay.Get().StartCoroutine(this.PlayMultipleVOLinesForEmotes(emoteType, emoteSpell));
	}

	// Token: 0x06004275 RID: 17013 RVA: 0x001649DD File Offset: 0x00162BDD
	protected IEnumerator PlayMultipleVOLinesForEmotes(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStart_01_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
			if (this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStartHeroic_01_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
			if (!this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStart_02_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
			if (this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStartHeroic_02_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId() == "DRGA_BOSS_11h2")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_EmoteResponse_George_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			}
			else
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_EmoteResponse_Karl_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			}
		}
		yield return null;
		yield break;
	}

	// Token: 0x06004276 RID: 17014 RVA: 0x001649F3 File Offset: 0x00162BF3
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Death_George_01, 2.5f);
			this.m_deathLine = DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_Death_Karl_01;
			GameState.Get().SetBusy(false);
			goto IL_711;
		case 101:
		case 105:
		case 107:
		case 108:
		case 111:
		case 116:
		case 121:
		case 123:
		case 124:
		case 125:
		case 127:
			break;
		case 102:
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Karl_Dies_01, 2.5f);
			goto IL_711;
		case 103:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossAttack_George_01, 2.5f);
			goto IL_711;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_StartOfTurnGeorgeAfterCartBurned);
			goto IL_711;
		case 106:
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_02_01, 2.5f);
			goto IL_711;
		case 109:
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_05_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_05_01, 2.5f);
			goto IL_711;
		case 110:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_01_01, 2.5f);
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_02_01, 2.5f);
			goto IL_711;
		case 112:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_Death_Karl_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_711;
		case 113:
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_GeorgeDies_01, 2.5f);
			goto IL_711;
		case 114:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossAttack_Karl_01, 2.5f);
			goto IL_711;
		case 115:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_StartOfTurnKarlAfterCartBurned);
			goto IL_711;
		case 117:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineOnlyOnce(DRGA_Dungeon.RafaamBrassRing, DRGA_Evil_Fight_10.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_10_Turn_03_01, 2.5f);
				goto IL_711;
			}
			goto IL_711;
		case 118:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_01_01, 2.5f);
				goto IL_711;
			}
			goto IL_711;
		case 119:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_02_01, 2.5f);
				goto IL_711;
			}
			goto IL_711;
		case 120:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_Transform_01, 2.5f);
				goto IL_711;
			}
			goto IL_711;
		case 122:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_missionEventHeroPowerKarl);
			goto IL_711;
		case 126:
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_04_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_04_01, 2.5f);
			goto IL_711;
		case 128:
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_06_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_06_01, 2.5f);
			goto IL_711;
		case 129:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineInOrderOnce(actor, this.m_VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_Elemental_HeroPowerLines);
				goto IL_711;
			}
			goto IL_711;
		case 130:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineInOrderOnce(actor, this.m_VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_DragonForm_HeroPowerLines);
				goto IL_711;
			}
			goto IL_711;
		default:
			if (missionEvent == 151)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_missionEventHeroPowerGeorge);
				goto IL_711;
			}
			break;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_711:
		yield break;
	}

	// Token: 0x06004277 RID: 17015 RVA: 0x00164A09 File Offset: 0x00162C09
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if ((cardId == "ULD_139" || cardId == "ULD_500" || cardId == "ULD_156" || cardId == "ULD_238") && this.m_Heroic)
		{
			yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_LeagueOfExplorers_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004278 RID: 17016 RVA: 0x00164A1F File Offset: 0x00162C1F
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		int num = UnityEngine.Random.Range(1, 4);
		if (!(cardId == "DRGA_BOSS_11t"))
		{
			if (cardId == "DRGA_BOSS_12t")
			{
				if (num <= 2)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_KarlBossPlaysHighFive);
				}
				else
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_01_01, 2.5f);
					yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_01_01, 2.5f);
				}
			}
		}
		else if (GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId() == "DRGA_BOSS_16h2")
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_01_01, 2.5f);
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_02_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_01_01, 2.5f);
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_03_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004279 RID: 17017 RVA: 0x00164A38 File Offset: 0x00162C38
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		float thinkEmoteBossThinkChancePercentage = this.GetThinkEmoteBossThinkChancePercentage();
		float num = UnityEngine.Random.Range(0f, 1f);
		if (thinkEmoteBossThinkChancePercentage <= num)
		{
			EmoteType emoteType = EmoteType.THINK1;
			switch (UnityEngine.Random.Range(1, 4))
			{
			case 1:
				emoteType = EmoteType.THINK1;
				break;
			case 2:
				emoteType = EmoteType.THINK2;
				break;
			case 3:
				emoteType = EmoteType.THINK3;
				break;
			}
			GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType);
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (cardId == "DRGA_BOSS_11h2")
		{
			string line = base.PopRandomLine(this.m_GeorgeBossIdleLinesCopy);
			if (this.m_GeorgeBossIdleLinesCopy.Count == 0)
			{
				this.m_GeorgeBossIdleLinesCopy = new List<string>(DRGA_Evil_Fight_10.m_VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdleLines);
			}
			Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
			return;
		}
		string line2 = base.PopRandomLine(this.m_KarlBossIdleLinesCopy);
		if (this.m_KarlBossIdleLinesCopy.Count == 0)
		{
			this.m_KarlBossIdleLinesCopy = new List<string>(DRGA_Evil_Fight_10.m_VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdleLines);
		}
		Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line2, 2.5f));
	}

	// Token: 0x04003235 RID: 12853
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_10_Turn_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_10_Turn_03_01.prefab:1a76bb04f0b5c90428f979c7a36fdb18");

	// Token: 0x04003236 RID: 12854
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Death_George_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Death_George_01.prefab:a62bfc220208c054c9818ca6a22f3f61");

	// Token: 0x04003237 RID: 12855
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_01_01.prefab:0bbb473ccf08c77469198f3a57007ac4");

	// Token: 0x04003238 RID: 12856
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_02_01.prefab:ec6ff6ef08206ca4f96a55098d744707");

	// Token: 0x04003239 RID: 12857
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_03_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_03_01.prefab:82c8845ca47801f438425b165aec30f4");

	// Token: 0x0400323A RID: 12858
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_George_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_George_01.prefab:d39765fbe184f8f43bc753f9f8508b01");

	// Token: 0x0400323B RID: 12859
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_George_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_George_01.prefab:4b3ed9dfe0ad6ee439815e780359f417");

	// Token: 0x0400323C RID: 12860
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Karl_Dies_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Karl_Dies_01.prefab:3d2c93b6491fa8f46bf191639739dc24");

	// Token: 0x0400323D RID: 12861
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossAttack_George_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossAttack_George_01.prefab:e88cdbc8792372d4199bd74014d332c3");

	// Token: 0x0400323E RID: 12862
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStart_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStart_01_01.prefab:dc04bd8bdd724cc43af01121352c92e4");

	// Token: 0x0400323F RID: 12863
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStartHeroic_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStartHeroic_01_01.prefab:cb084b7a87de7af49b6867944a212f88");

	// Token: 0x04003240 RID: 12864
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_EmoteResponse_George_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_EmoteResponse_George_01.prefab:0727589b0e2cdc04a934823cd0051b1b");

	// Token: 0x04003241 RID: 12865
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_01_01.prefab:57c964a0738e8b24b8be0f0f35ac36e1");

	// Token: 0x04003242 RID: 12866
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_02_01.prefab:d1fb32fbde218e94582369de527fa6ff");

	// Token: 0x04003243 RID: 12867
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_01_01.prefab:9ac8a7281e06b3649922677aaf147703");

	// Token: 0x04003244 RID: 12868
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_02_01.prefab:4eab5896ccea9324c9764b06c804a343");

	// Token: 0x04003245 RID: 12869
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_03_01.prefab:e966c427e58f5ae468b0286e20fcc0ae");

	// Token: 0x04003246 RID: 12870
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_04_01.prefab:3f7f113d6b46fda4ab610f5a66129eb5");

	// Token: 0x04003247 RID: 12871
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_05_01.prefab:e4fbfcf4b70c47743881de14690745ba");

	// Token: 0x04003248 RID: 12872
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_01_01.prefab:bc3da3275c25c5e439709a00565e34d6");

	// Token: 0x04003249 RID: 12873
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_02_01.prefab:8a8f215823a69f74cabf01fc48b6d9ab");

	// Token: 0x0400324A RID: 12874
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_Death_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_Death_Karl_01.prefab:2f5f773d469f39948a4919159132ca40");

	// Token: 0x0400324B RID: 12875
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_GeorgeDies_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_GeorgeDies_01.prefab:eefe3075acc27b642a89128615573812");

	// Token: 0x0400324C RID: 12876
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01.prefab:038cf01d36976394cb45c66da9ba7fa5");

	// Token: 0x0400324D RID: 12877
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01.prefab:77abd879f3c394643940e0227ffeadbd");

	// Token: 0x0400324E RID: 12878
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_01_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_01_01.prefab:e878544e8c9b15d41a4907885483b942");

	// Token: 0x0400324F RID: 12879
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_02_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_02_01.prefab:4c50321ebaeb2a24ba4c077dc6f86108");

	// Token: 0x04003250 RID: 12880
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_03_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_03_01.prefab:6ab4a3cf5711ebd4ba440b67559448d8");

	// Token: 0x04003251 RID: 12881
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossAttack_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossAttack_Karl_01.prefab:337ea46f1d0d63a4f9fe31ca7c3f427f");

	// Token: 0x04003252 RID: 12882
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStart_02_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStart_02_01.prefab:4087e874ae2d74c419b2b4a9ef0dc3a5");

	// Token: 0x04003253 RID: 12883
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStartHeroic_02_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStartHeroic_02_01.prefab:0f75923a791d4984eb2b4161a4915e11");

	// Token: 0x04003254 RID: 12884
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_EmoteResponse_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_EmoteResponse_Karl_01.prefab:70eb6fb0515207f4a8823036b8cde803");

	// Token: 0x04003255 RID: 12885
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_01_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_01_01.prefab:7c09562f58c6167418400bc5d8e0efb9");

	// Token: 0x04003256 RID: 12886
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_02_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_02_01.prefab:1198bf78270c25846951383068559b3d");

	// Token: 0x04003257 RID: 12887
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_LeagueOfExplorers_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_LeagueOfExplorers_01.prefab:1597c19f3860a4b4bb5bf4852af58270");

	// Token: 0x04003258 RID: 12888
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_01_01.prefab:3fecf4d3a6d75474dbda7a167aea1942");

	// Token: 0x04003259 RID: 12889
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_02_01.prefab:5e7161726b9dca449ae6264d2920ecc3");

	// Token: 0x0400325A RID: 12890
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_03_01.prefab:27f2ea84128c1804596941fae02c2eba");

	// Token: 0x0400325B RID: 12891
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_04_01.prefab:6389ee6b308b29944bc19f0da2062ece");

	// Token: 0x0400325C RID: 12892
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_05_01.prefab:49a2b1f6f32d81a45852e3119f7be628");

	// Token: 0x0400325D RID: 12893
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_06_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_06_01.prefab:a91332acb6b06194991aeb212229d743");

	// Token: 0x0400325E RID: 12894
	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_01_01.prefab:9429f118ec9bad944863e8115b4438cb");

	// Token: 0x0400325F RID: 12895
	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_02_01.prefab:42e12dde630a3c546ba23495fec5814f");

	// Token: 0x04003260 RID: 12896
	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_EmoteResponseALT_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_EmoteResponseALT_01.prefab:385373b8b872ad64aa124582667abfb4");

	// Token: 0x04003261 RID: 12897
	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_01_01.prefab:717a16cf3d92ba54fa9c29a13e476438");

	// Token: 0x04003262 RID: 12898
	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_02_01.prefab:6337c3d0e2a1ccb4a899c038404c291b");

	// Token: 0x04003263 RID: 12899
	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_03_01.prefab:95827e1c461408e418ee1d4212cf4494");

	// Token: 0x04003264 RID: 12900
	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_04_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_04_01.prefab:e2a44f5e0bc0cf04b908bd03c7315820");

	// Token: 0x04003265 RID: 12901
	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_05_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_05_01.prefab:6704a51ceedf5cc459e5e86fddb993a9");

	// Token: 0x04003266 RID: 12902
	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_06_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_06_01.prefab:853ccc4d07a089944a3c2016a324e718");

	// Token: 0x04003267 RID: 12903
	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_Transform_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_Transform_01.prefab:47062c53e13db26459bd48fb9b6bc44c");

	// Token: 0x04003268 RID: 12904
	private List<string> m_missionEventHeroPowerGeorge = new List<string>
	{
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_George_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_George_01
	};

	// Token: 0x04003269 RID: 12905
	private List<string> m_KarlBossPlaysHighFive = new List<string>
	{
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_06_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_04_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_02_01
	};

	// Token: 0x0400326A RID: 12906
	private List<string> m_StartOfTurnKarlAfterCartBurned = new List<string>
	{
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_01_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_02_01
	};

	// Token: 0x0400326B RID: 12907
	private List<string> m_StartOfTurnGeorgeAfterCartBurned = new List<string>
	{
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_01_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_02_01
	};

	// Token: 0x0400326C RID: 12908
	private List<string> m_missionEventHeroPowerKarl = new List<string>
	{
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01
	};

	// Token: 0x0400326D RID: 12909
	private static List<string> m_VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdleLines = new List<string>
	{
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_01_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_02_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_03_01
	};

	// Token: 0x0400326E RID: 12910
	private List<string> m_GeorgeBossIdleLinesCopy = new List<string>(DRGA_Evil_Fight_10.m_VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdleLines);

	// Token: 0x0400326F RID: 12911
	private static List<string> m_VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdleLines = new List<string>
	{
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_01_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_02_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_03_01
	};

	// Token: 0x04003270 RID: 12912
	private List<string> m_KarlBossIdleLinesCopy = new List<string>(DRGA_Evil_Fight_10.m_VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdleLines);

	// Token: 0x04003271 RID: 12913
	private List<string> m_VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_Elemental_HeroPowerLines = new List<string>
	{
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_01_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_02_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_03_01
	};

	// Token: 0x04003272 RID: 12914
	private List<string> m_VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_DragonForm_HeroPowerLines = new List<string>
	{
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_04_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_05_01,
		DRGA_Evil_Fight_10.VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_06_01
	};

	// Token: 0x04003273 RID: 12915
	private HashSet<string> m_playedLines = new HashSet<string>();
}
