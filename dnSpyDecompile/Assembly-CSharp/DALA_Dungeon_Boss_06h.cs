using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000433 RID: 1075
public class DALA_Dungeon_Boss_06h : DALA_Dungeon
{
	// Token: 0x06003A94 RID: 14996 RVA: 0x0012EAB4 File Offset: 0x0012CCB4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_Attack_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_BossWarriorSpell_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_Death_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_DefeatPlayer_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponse_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseGreetings_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseOops_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseThanks_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseThreaten_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseThreaten_02,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseWellPlayed_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseWow_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_HeroPower_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_HeroPower_02,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_HeroPower_03,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_Intro_03,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_PlayerArmor_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_PlayerDKCardHeroic_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_PlayerGorehowl_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_Attack_03,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_BossCharge_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_BossGorehowl_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseGreetings_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseOops_02,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseThanks_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseThreaten_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseWellPlayed_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseWow_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_HeroPower_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_HeroPower_02,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_HeroPower_03,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_PlayerScourgelord_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_PlayerWhirlwind_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_Idle_01,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_Idle_02,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_Idle_03,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_Idle_02,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_Idle_03,
			DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_Idle_05
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003A95 RID: 14997 RVA: 0x0012ED88 File Offset: 0x0012CF88
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_Intro_03;
		this.m_deathLine = DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_Death_01;
	}

	// Token: 0x06003A96 RID: 14998 RVA: 0x0012EDB0 File Offset: 0x0012CFB0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		string cardId2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (cardId == DALA_Dungeon_Boss_06h.DAZZIK_CARDID && emoteType == EmoteType.START && cardId2 != "DALA_Chu" && cardId2 != "DALA_George")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (cardId == DALA_Dungeon_Boss_06h.DAZZIK_DK_CARDID)
			{
				switch (emoteType)
				{
				case EmoteType.GREETINGS:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseGreetings_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case EmoteType.WELL_PLAYED:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseWellPlayed_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case EmoteType.OOPS:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseOops_02, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case EmoteType.THREATEN:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseThreaten_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case EmoteType.THANKS:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseThanks_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				default:
					if (emoteType != EmoteType.WOW)
					{
						Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseGreetings_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					}
					else
					{
						Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseWow_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					}
					break;
				}
			}
			if (cardId == DALA_Dungeon_Boss_06h.DAZZIK_CARDID)
			{
				switch (emoteType)
				{
				case EmoteType.GREETINGS:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseGreetings_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					return;
				case EmoteType.WELL_PLAYED:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseWellPlayed_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					return;
				case EmoteType.OOPS:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseOops_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					return;
				case EmoteType.THREATEN:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseThreaten_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					return;
				case EmoteType.THANKS:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseThanks_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					return;
				default:
					if (emoteType == EmoteType.WOW)
					{
						Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseWow_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
						return;
					}
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				}
			}
		}
	}

	// Token: 0x06003A97 RID: 14999 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003A98 RID: 15000 RVA: 0x0012F175 File Offset: 0x0012D375
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_BossWarriorSpell_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_PlayerArmor_01, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_Attack_03, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_Attack_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003A99 RID: 15001 RVA: 0x0012F18B File Offset: 0x0012D38B
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardID = entity.GetCardId();
		this.m_playedLines.Add(cardID);
		string enemyHeroCardID = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (enemyHeroCardID == DALA_Dungeon_Boss_06h.DAZZIK_CARDID)
		{
			string text = cardID;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 565382510U)
			{
				if (num <= 531827272U)
				{
					if (num != 515196748U)
					{
						if (num != 531827272U)
						{
							goto IL_31A;
						}
						if (!(text == "ICC_832"))
						{
							goto IL_31A;
						}
					}
					else if (!(text == "ICC_829"))
					{
						goto IL_31A;
					}
				}
				else if (num != 531974367U)
				{
					if (num != 548604891U)
					{
						if (num != 565382510U)
						{
							goto IL_31A;
						}
						if (!(text == "ICC_830"))
						{
							goto IL_31A;
						}
					}
					else if (!(text == "ICC_833"))
					{
						goto IL_31A;
					}
				}
				else if (!(text == "ICC_828"))
				{
					goto IL_31A;
				}
			}
			else if (num <= 615862462U)
			{
				if (num != 582160129U)
				{
					if (num != 615862462U)
					{
						goto IL_31A;
					}
					if (!(text == "ICC_827"))
					{
						goto IL_31A;
					}
				}
				else if (!(text == "ICC_831"))
				{
					goto IL_31A;
				}
			}
			else if (num != 632492986U)
			{
				if (num != 1900257086U)
				{
					if (num != 1999498950U)
					{
						goto IL_31A;
					}
					if (!(text == "ICC_481"))
					{
						goto IL_31A;
					}
				}
				else
				{
					if (!(text == "EX1_411"))
					{
						goto IL_31A;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_PlayerGorehowl_01, 2.5f);
					goto IL_31A;
				}
			}
			else if (!(text == "ICC_834"))
			{
				goto IL_31A;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_PlayerDKCardHeroic_01, 2.5f);
		}
		IL_31A:
		if (enemyHeroCardID == DALA_Dungeon_Boss_06h.DAZZIK_DK_CARDID)
		{
			string text = cardID;
			if (!(text == "ICC_834"))
			{
				if (text == "EX1_400")
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_PlayerWhirlwind_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_PlayerScourgelord_01, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x06003A9A RID: 15002 RVA: 0x0012F1A1 File Offset: 0x0012D3A1
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
		if (entity.GetCardType() == TAG_CARDTYPE.HERO_POWER && entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			this.OnBossHeroPowerPlayed(entity);
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId() == DALA_Dungeon_Boss_06h.DAZZIK_DK_CARDID)
		{
			if (!(cardId == "CS2_103"))
			{
				if (cardId == "EX1_411")
				{
					yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_BossGorehowl_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_BossCharge_01, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x06003A9B RID: 15003 RVA: 0x0012F1B8 File Offset: 0x0012D3B8
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
		float num = UnityEngine.Random.Range(1f, 101f);
		if (thinkEmoteBossThinkChancePercentage > num)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			if (cardId == DALA_Dungeon_Boss_06h.DAZZIK_CARDID)
			{
				string line = base.PopRandomLine(this.m_IdleLinesCopy);
				if (this.m_IdleLinesCopy.Count == 0)
				{
					this.m_IdleLinesCopy = new List<string>(DALA_Dungeon_Boss_06h.m_IdleLines);
				}
				Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
				return;
			}
			if (cardId == DALA_Dungeon_Boss_06h.DAZZIK_DK_CARDID)
			{
				string line2 = base.PopRandomLine(this.m_DKIdleLinesCopy);
				if (this.m_DKIdleLinesCopy.Count == 0)
				{
					this.m_DKIdleLinesCopy = new List<string>(DALA_Dungeon_Boss_06h.m_DKIdleLines);
				}
				Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line2, 2.5f));
				return;
			}
		}
		else
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
		}
	}

	// Token: 0x06003A9C RID: 15004 RVA: 0x0012F328 File Offset: 0x0012D528
	protected override void OnBossHeroPowerPlayed(Entity entity)
	{
		float num = this.ChanceToPlayBossHeroPowerVOLine();
		float num2 = UnityEngine.Random.Range(0f, 1f);
		if (this.m_enemySpeaking)
		{
			return;
		}
		if (num < num2)
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (actor == null)
		{
			return;
		}
		List<string> list = null;
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (cardId == DALA_Dungeon_Boss_06h.DAZZIK_CARDID)
		{
			list = this.m_HeroPowerLines;
		}
		if (cardId == DALA_Dungeon_Boss_06h.DAZZIK_DK_CARDID)
		{
			list = this.m_DKHeroPowerLines;
		}
		string text = "";
		while (list.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			text = list[index];
			list.RemoveAt(index);
			if (!NotificationManager.Get().HasSoundPlayedThisSession(text))
			{
				break;
			}
		}
		if (text == "")
		{
			return;
		}
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce(text, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x06003A9D RID: 15005 RVA: 0x0012F434 File Offset: 0x0012D634
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId() != DALA_Dungeon_Boss_06h.DAZZIK_CARDID)
		{
			return;
		}
		if (!this.m_enemySpeaking && !string.IsNullOrEmpty(this.m_deathLine) && gameResult == TAG_PLAYSTATE.WON)
		{
			if (this.GetShouldSuppressDeathTextBubble())
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_deathLine, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_deathLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
	}

	// Token: 0x04002274 RID: 8820
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_Attack_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_Attack_01.prefab:15dd88b078005e54397f95c95af81ff2");

	// Token: 0x04002275 RID: 8821
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_BossWarriorSpell_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_BossWarriorSpell_01.prefab:7df6ea496dcc4ba4d8c1e23092de4ed4");

	// Token: 0x04002276 RID: 8822
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_Death_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_Death_01.prefab:7f93d455de1f91c4682c69f5d06baa1c");

	// Token: 0x04002277 RID: 8823
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_DefeatPlayer_01.prefab:d6a0cdbb8f4d3834281fc2ead4c4f57f");

	// Token: 0x04002278 RID: 8824
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_EmoteResponse_01.prefab:9b9908062f8314b488e5fd02ae670bbf");

	// Token: 0x04002279 RID: 8825
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseGreetings_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseGreetings_01.prefab:41c01f6cc71c1264586f6759706dd570");

	// Token: 0x0400227A RID: 8826
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseOops_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseOops_01.prefab:8c76cee51431f63429a8b76cbd8d5f22");

	// Token: 0x0400227B RID: 8827
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseThanks_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseThanks_01.prefab:f8ed42fac5f4ce846819e6d426fd7f1a");

	// Token: 0x0400227C RID: 8828
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseThreaten_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseThreaten_01.prefab:b5565c9a7f3be1a4ea0a394f84eb5b02");

	// Token: 0x0400227D RID: 8829
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseThreaten_02 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseThreaten_02.prefab:ec94bb20b735a6f46b5ce7fda59183f8");

	// Token: 0x0400227E RID: 8830
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseWellPlayed_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseWellPlayed_01.prefab:9c8f2cb7e807ad94487803044118c4af");

	// Token: 0x0400227F RID: 8831
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseWow_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_EmoteResponseWow_01.prefab:e326467f25f85b5429f2ae90ec58dd76");

	// Token: 0x04002280 RID: 8832
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_HeroPower_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_HeroPower_01.prefab:8acd66684adcd034782705d51f2c3c4d");

	// Token: 0x04002281 RID: 8833
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_HeroPower_02 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_HeroPower_02.prefab:ce05eddba59e90243a39fee727a34f19");

	// Token: 0x04002282 RID: 8834
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_HeroPower_03 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_HeroPower_03.prefab:6bd3befa9b1da504bbe60aac688d31df");

	// Token: 0x04002283 RID: 8835
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_Intro_03 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_Intro_03.prefab:ad88627146b8feb43b5b56b2a464971e");

	// Token: 0x04002284 RID: 8836
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_PlayerArmor_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_PlayerArmor_01.prefab:4698fd92375f0d34c901d1c6a21635d6");

	// Token: 0x04002285 RID: 8837
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_PlayerDKCardHeroic_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_PlayerDKCardHeroic_01.prefab:6d3ae02e263997347b5cac50397c435e");

	// Token: 0x04002286 RID: 8838
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_PlayerGorehowl_01 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_PlayerGorehowl_01.prefab:40a4a78c4165e8d47945ae7eebf3bfb0");

	// Token: 0x04002287 RID: 8839
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_Attack_03 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_Attack_03.prefab:665540cff61744b4098f744b5bc4ba51");

	// Token: 0x04002288 RID: 8840
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_BossCharge_01 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_BossCharge_01.prefab:81ac521d762da4b458a615e86a17f553");

	// Token: 0x04002289 RID: 8841
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_BossGorehowl_01 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_BossGorehowl_01.prefab:8a121f8af0bdba3428842f0eccc174c0");

	// Token: 0x0400228A RID: 8842
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseGreetings_01 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseGreetings_01.prefab:68b4c1b71f8931c49ae9addd1481a851");

	// Token: 0x0400228B RID: 8843
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseOops_02 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseOops_02.prefab:ca3fee93b5f9e9a4c86f8a474acaf109");

	// Token: 0x0400228C RID: 8844
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseThanks_01 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseThanks_01.prefab:d0468e6da7afc114cb20ccf3a8cbd137");

	// Token: 0x0400228D RID: 8845
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseThreaten_01 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseThreaten_01.prefab:23948cc4b84fd4f4cae96938fc821cdc");

	// Token: 0x0400228E RID: 8846
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseWellPlayed_01 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseWellPlayed_01.prefab:5ffe0d163d1bd2a4d96be52faacf53d7");

	// Token: 0x0400228F RID: 8847
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseWow_01 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_EmoteResponseWow_01.prefab:a8f2ca41f54b2fe48be84789555bdb43");

	// Token: 0x04002290 RID: 8848
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_HeroPower_01 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_HeroPower_01.prefab:a55235537a698a04ab1a69ba7ff3f16e");

	// Token: 0x04002291 RID: 8849
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_HeroPower_02 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_HeroPower_02.prefab:8a96a7cf141bc524c914ed457c6bfe58");

	// Token: 0x04002292 RID: 8850
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_HeroPower_03 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_HeroPower_03.prefab:aa9cb380c83542542869235c6e7973b6");

	// Token: 0x04002293 RID: 8851
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_PlayerScourgelord_01 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_PlayerScourgelord_01.prefab:df08d25d7cb0e604795cf8e89ac58d45");

	// Token: 0x04002294 RID: 8852
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_PlayerWhirlwind_01 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_PlayerWhirlwind_01.prefab:976141c1bb0621a48813994be3d3e11f");

	// Token: 0x04002295 RID: 8853
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_Idle_01 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_Idle_01.prefab:b70421d5abdf9b840aed30d2245115a4");

	// Token: 0x04002296 RID: 8854
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_Idle_02 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_Idle_02.prefab:57f5de88521bbb342aae4e3f922f9a77");

	// Token: 0x04002297 RID: 8855
	private static readonly AssetReference VO_DALA_BOSS_06dk_Male_Goblin_Idle_03 = new AssetReference("VO_DALA_BOSS_06dk_Male_Goblin_Idle_03.prefab:53eed559da61c304b98e666d9fe03378");

	// Token: 0x04002298 RID: 8856
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_Idle_02 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_Idle_02.prefab:4b985e77744ef30429120e25c285bf28");

	// Token: 0x04002299 RID: 8857
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_Idle_03 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_Idle_03.prefab:4ca2a710058db864db917dd0bce87b5a");

	// Token: 0x0400229A RID: 8858
	private static readonly AssetReference VO_DALA_BOSS_06h_Male_Goblin_Idle_05 = new AssetReference("VO_DALA_BOSS_06h_Male_Goblin_Idle_05.prefab:cdfd7e9dc8a0dac4c85ac6af2aa68477");

	// Token: 0x0400229B RID: 8859
	private static readonly string DAZZIK_CARDID = "DALA_BOSS_06h";

	// Token: 0x0400229C RID: 8860
	private static readonly string DAZZIK_DK_CARDID = "DALA_BOSS_06dk";

	// Token: 0x0400229D RID: 8861
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_Idle_02,
		DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_Idle_03,
		DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_Idle_05
	};

	// Token: 0x0400229E RID: 8862
	private static List<string> m_DKIdleLines = new List<string>
	{
		DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_Idle_01,
		DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_Idle_02,
		DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_Idle_03
	};

	// Token: 0x0400229F RID: 8863
	private List<string> m_IdleLinesCopy = new List<string>(DALA_Dungeon_Boss_06h.m_IdleLines);

	// Token: 0x040022A0 RID: 8864
	private List<string> m_DKIdleLinesCopy = new List<string>(DALA_Dungeon_Boss_06h.m_DKIdleLines);

	// Token: 0x040022A1 RID: 8865
	private List<string> m_HeroPowerLines = new List<string>
	{
		DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_HeroPower_01,
		DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_HeroPower_02,
		DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06h_Male_Goblin_HeroPower_03
	};

	// Token: 0x040022A2 RID: 8866
	private List<string> m_DKHeroPowerLines = new List<string>
	{
		DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_HeroPower_01,
		DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_HeroPower_02,
		DALA_Dungeon_Boss_06h.VO_DALA_BOSS_06dk_Male_Goblin_HeroPower_03
	};

	// Token: 0x040022A3 RID: 8867
	private HashSet<string> m_playedLines = new HashSet<string>();
}
