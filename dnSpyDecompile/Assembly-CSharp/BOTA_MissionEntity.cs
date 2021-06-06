using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public class BOTA_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x0600398F RID: 14735 RVA: 0x0010C851 File Offset: 0x0010AA51
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

	// Token: 0x06003990 RID: 14736 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x06003991 RID: 14737 RVA: 0x00124464 File Offset: 0x00122664
	public BOTA_MissionEntity()
	{
		this.m_gameOptions.AddOptions(BOTA_MissionEntity.s_booleanOptions, BOTA_MissionEntity.s_stringOptions);
	}

	// Token: 0x06003992 RID: 14738 RVA: 0x001244D9 File Offset: 0x001226D9
	public override void OnCreateGame()
	{
		if (!BOTA_MissionEntity.s_shownEndTurnReminder)
		{
			GameState.Get().RegisterOptionsReceivedListener(new GameState.OptionsReceivedCallback(this.OnOptionsReceived));
			GameState.Get().RegisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
		}
	}

	// Token: 0x06003993 RID: 14739 RVA: 0x00124511 File Offset: 0x00122711
	public override void OnDecommissionGame()
	{
		GameState.Get().UnregisterOptionsReceivedListener(new GameState.OptionsReceivedCallback(this.OnOptionsReceived));
		GameState.Get().UnregisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
	}

	// Token: 0x06003994 RID: 14740 RVA: 0x00124542 File Offset: 0x00122742
	public override float? GetThinkEmoteDelayOverride()
	{
		return new float?(50f + UnityEngine.Random.Range(0f, 20f));
	}

	// Token: 0x06003995 RID: 14741 RVA: 0x0012455E File Offset: 0x0012275E
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BOT);
	}

	// Token: 0x06003996 RID: 14742 RVA: 0x00124570 File Offset: 0x00122770
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BOTMulligan);
		}
	}

	// Token: 0x06003997 RID: 14743 RVA: 0x00124585 File Offset: 0x00122785
	public IEnumerator UnBusyInSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06003998 RID: 14744 RVA: 0x00124594 File Offset: 0x00122794
	private IEnumerator ShowEndTurnReminderIfNeeded()
	{
		yield return new WaitForSeconds(1f);
		Network.Options optionsPacket = GameState.Get().GetOptionsPacket();
		if (optionsPacket == null || optionsPacket.HasValidOption() || BOTA_MissionEntity.s_shownEndTurnReminder)
		{
			yield break;
		}
		BOTA_MissionEntity.s_shownEndTurnReminder = true;
		GameState.Get().UnregisterOptionsReceivedListener(new GameState.OptionsReceivedCallback(this.OnOptionsReceived));
		Vector3 position = EndTurnButton.Get().transform.position;
		position.x -= 3.1f;
		this.m_endTurnReminder = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("BOTA_PUZZLE_END_TURN_REMINDER"), true, NotificationManager.PopupTextType.BASIC);
		this.m_endTurnReminder.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
		this.m_endTurnReminderCoroutine = null;
		yield break;
	}

	// Token: 0x06003999 RID: 14745 RVA: 0x001245A4 File Offset: 0x001227A4
	private void OnOptionsReceived(object userData)
	{
		if (SpectatorManager.Get().IsInSpectatorMode())
		{
			return;
		}
		if (this.m_endTurnReminderCoroutine != null)
		{
			Gameplay.Get().StopCoroutine(this.m_endTurnReminderCoroutine);
			this.m_endTurnReminderCoroutine = null;
		}
		Network.Options optionsPacket = GameState.Get().GetOptionsPacket();
		if (optionsPacket == null)
		{
			Log.Gameplay.PrintError("BOTA_MissionEntity wants options packet but option packet is null.", Array.Empty<object>());
			return;
		}
		if (!BOTA_MissionEntity.s_shownEndTurnReminder && !optionsPacket.HasValidOption())
		{
			this.m_endTurnReminderCoroutine = Gameplay.Get().StartCoroutine(this.ShowEndTurnReminderIfNeeded());
		}
	}

	// Token: 0x0600399A RID: 14746 RVA: 0x00124625 File Offset: 0x00122825
	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		this.DestroyEndTurnReminder();
	}

	// Token: 0x0600399B RID: 14747 RVA: 0x0012462D File Offset: 0x0012282D
	public override void NotifyOfResetGameStarted()
	{
		base.NotifyOfResetGameStarted();
		this.DestroyEndTurnReminder();
	}

	// Token: 0x0600399C RID: 14748 RVA: 0x0012463C File Offset: 0x0012283C
	public override void NotifyOfResetGameFinished(Entity source, Entity oldGameEntity)
	{
		this.m_waitingForTurnStartIndicatorAfterReset = true;
		BOTA_MissionEntity bota_MissionEntity = oldGameEntity as BOTA_MissionEntity;
		this.s_lethalCompleteLines = bota_MissionEntity.s_lethalCompleteLines;
		this.lethalLineUsed = bota_MissionEntity.lethalLineUsed;
		this.m_randomEmoteLines = bota_MissionEntity.m_randomEmoteLines;
		this.m_randomIdleLines = bota_MissionEntity.m_randomIdleLines;
		this.m_randomRestartLines = bota_MissionEntity.m_randomRestartLines;
		base.NotifyOfResetGameFinished(source, oldGameEntity);
	}

	// Token: 0x0600399D RID: 14749 RVA: 0x0012469B File Offset: 0x0012289B
	public override void OnTurnStartManagerFinished()
	{
		if (this.m_waitingForTurnStartIndicatorAfterReset && GameState.Get().GetGameEntity().GetTag(GAME_TAG.PREVIOUS_PUZZLE_COMPLETED) == 0)
		{
			return;
		}
		Gameplay.Get().StartCoroutine(this.OnTurnStartManagerFinishedWithTiming());
	}

	// Token: 0x0600399E RID: 14750 RVA: 0x001246CD File Offset: 0x001228CD
	public virtual IEnumerator OnTurnStartManagerFinishedWithTiming()
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return this.RespondToPuzzleStartWithTiming();
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		int tag = GameState.Get().GetFriendlySidePlayer().GetSecretZone().GetPuzzleEntity().GetTag(GAME_TAG.PUZZLE_PROGRESS);
		string puzzleVictoryLine = this.GetPuzzleVictoryLine(tag);
		if (puzzleVictoryLine != null)
		{
			yield return base.PlayBossLine(actor, puzzleVictoryLine, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600399F RID: 14751 RVA: 0x001246DC File Offset: 0x001228DC
	protected virtual IEnumerator RespondToPuzzleStartWithTiming()
	{
		yield break;
	}

	// Token: 0x060039A0 RID: 14752 RVA: 0x001246E4 File Offset: 0x001228E4
	private void DestroyEndTurnReminder()
	{
		if (this.m_endTurnReminderCoroutine != null)
		{
			Gameplay.Get().StopCoroutine(this.m_endTurnReminderCoroutine);
			this.m_endTurnReminderCoroutine = null;
		}
		if (this.m_endTurnReminder != null)
		{
			NotificationManager.Get().DestroyNotification(this.m_endTurnReminder, 0f);
		}
	}

	// Token: 0x060039A1 RID: 14753 RVA: 0x00124733 File Offset: 0x00122933
	public override bool NotifyOfEndTurnButtonPushed()
	{
		this.DestroyEndTurnReminder();
		return true;
	}

	// Token: 0x060039A2 RID: 14754 RVA: 0x0012473C File Offset: 0x0012293C
	public override IEnumerator DoGameSpecificPostIntroActions()
	{
		this.m_entranceFinished = false;
		this.m_confirmButtonPressed = false;
		int currentPuzzleProgress = 0;
		int totalPuzzleProgress = 0;
		string puzzleName = "";
		string puzzleText = "";
		TAG_PUZZLE_TYPE puzzleType = TAG_PUZZLE_TYPE.INVALID;
		int maxNumAttempts = 2;
		if (HearthstoneApplication.IsPublic())
		{
			maxNumAttempts = 10;
		}
		bool puzzleInfoFound = false;
		int num;
		for (int i = 0; i < maxNumAttempts; i = num + 1)
		{
			puzzleInfoFound = this.LookUpPuzzleInfoFromFutureTaskLists(out currentPuzzleProgress, out totalPuzzleProgress, out puzzleName, out puzzleText, out puzzleType);
			if (puzzleInfoFound)
			{
				break;
			}
			yield return new WaitForSeconds(1f);
			num = i;
		}
		if (!puzzleInfoFound)
		{
			Log.Spells.PrintError("BOTA_MissionEntity.DoGameSpecificPostIntroActions(): puzzle info could not be found in the task lists - most likely the script for this game entity is not setting up a puzzle entity correctly.", Array.Empty<object>());
			if (puzzleType == TAG_PUZZLE_TYPE.INVALID)
			{
				yield break;
			}
		}
		GameObject gameObject = this.LoadIntroUIForPuzzleType(puzzleType);
		PuzzleProgressUI component = gameObject.GetComponent<PuzzleProgressUI>();
		if (component == null)
		{
			Log.Spells.PrintError("BOTA_MissionEntity.DoGameSpecificPostIntroActions(): No PuzzleProgressUI found on puzzle intro spell {0}.", new object[]
			{
				gameObject.gameObject.name
			});
			yield break;
		}
		component.UpdateNameAndText(puzzleName, puzzleText);
		component.UpdateProgressValues(currentPuzzleProgress, totalPuzzleProgress);
		this.m_introSpell = gameObject.GetComponent<PuzzleIntroSpell>();
		if (this.m_introSpell == null)
		{
			Log.Spells.PrintError("BOTA_MissionEntity.DoGameSpecificPostIntroActions(): No PuzzleIntroSpell found on puzzle intro spell {0}.", new object[]
			{
				gameObject.gameObject.name
			});
			yield break;
		}
		if (this.m_introSpell.GetConfirmButton() == null)
		{
			Log.Spells.PrintError("BOTA_MissionEntity.DoGameSpecificPostIntroActions(): No confirmButton found on puzzle intro spell {0}.", new object[]
			{
				gameObject.gameObject.name
			});
			yield break;
		}
		this.m_confirmButton = this.m_introSpell.GetConfirmButton().GetComponentInChildren<NormalButton>();
		if (this.m_confirmButton == null)
		{
			Log.Spells.PrintError(string.Format("BOTA_MissionEntity.DoGameSpecificPostIntroActions() - ERROR \"{0}\" has no {1} component", this.m_introSpell.GetConfirmButton(), typeof(NormalButton)), Array.Empty<object>());
			yield break;
		}
		this.m_introSpell.AddSpellEventCallback(new Spell.SpellEventCallback(this.OnSpellEvent));
		this.m_confirmButton.SetText(GameStrings.Get("GLOBAL_CONFIRM"));
		this.m_confirmButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnConfirmButtonReleased));
		this.m_confirmButton.GetComponent<Collider>().enabled = true;
		this.m_confirmButton.m_button.GetComponent<PlayMakerFSM>().SendEvent("Birth");
		this.m_introSpell.ActivateState(SpellStateType.BIRTH);
		while (this.m_introSpell != null && !this.m_introSpell.IsFinished())
		{
			if (GameState.Get().WasConcedeRequested())
			{
				if (!this.m_confirmButtonPressed && this.m_entranceFinished)
				{
					this.m_confirmButton.SetEnabled(false, false);
					this.ProgressPastConfirmButton();
				}
				yield break;
			}
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (currentPuzzleProgress == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, BOTA_MissionEntity.s_introLine, 2.5f));
		}
		else if (this.s_returnLineOverride)
		{
			GameEntity gameEntity = GameState.Get().GetGameEntity();
			gameEntity.SetTag(GAME_TAG.MISSION_EVENT, 77);
			gameEntity.SetTag(GAME_TAG.MISSION_EVENT, 0);
		}
		else
		{
			Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, BOTA_MissionEntity.s_returnLine, 2.5f));
		}
		yield break;
	}

	// Token: 0x060039A3 RID: 14755 RVA: 0x0012474C File Offset: 0x0012294C
	private GameObject LoadIntroUIForPuzzleType(TAG_PUZZLE_TYPE puzzleType)
	{
		switch (puzzleType)
		{
		case TAG_PUZZLE_TYPE.INVALID:
			Log.Spells.PrintError(string.Format("BOTA_MissionEntity.LoadIntroUIForPuzzleType() - invalid puzzle type", Array.Empty<object>()), Array.Empty<object>());
			return null;
		case TAG_PUZZLE_TYPE.MIRROR:
			return AssetLoader.Get().InstantiatePrefab(BOTA_MissionEntity.PuzzleIntroUI_Mirror, AssetLoadingOptions.None);
		case TAG_PUZZLE_TYPE.LETHAL:
			return AssetLoader.Get().InstantiatePrefab(BOTA_MissionEntity.PuzzleIntroUI_Lethal, AssetLoadingOptions.None);
		case TAG_PUZZLE_TYPE.SURVIVAL:
			return AssetLoader.Get().InstantiatePrefab(BOTA_MissionEntity.PuzzleIntroUI_Survival, AssetLoadingOptions.None);
		case TAG_PUZZLE_TYPE.CLEAR:
			return AssetLoader.Get().InstantiatePrefab(BOTA_MissionEntity.PuzzleIntroUI_Clear, AssetLoadingOptions.None);
		default:
			return null;
		}
	}

	// Token: 0x060039A4 RID: 14756 RVA: 0x001247DC File Offset: 0x001229DC
	private bool LookUpPuzzleInfoFromFutureTaskLists(out int currentPuzzleProgress, out int totalPuzzleProgress, out string puzzleName, out string puzzleText, out TAG_PUZZLE_TYPE puzzleType)
	{
		int currentPuzzleProgressFound = 0;
		int totalPuzzleProgressFound = 0;
		string puzzleNameFound = "";
		string puzzleTextFound = "";
		TAG_PUZZLE_TYPE puzzleTypeFound = TAG_PUZZLE_TYPE.INVALID;
		bool puzzleInfoFound = false;
		GameState.Get().GetPowerProcessor().ForEachTaskList(delegate(int index, PowerTaskList taskList)
		{
			if (currentPuzzleProgressFound != 0 && totalPuzzleProgressFound != 0)
			{
				return;
			}
			foreach (PowerTask powerTask in taskList.GetTaskList())
			{
				Network.PowerHistory power = powerTask.GetPower();
				if (power.Type == Network.PowerType.FULL_ENTITY)
				{
					Network.HistFullEntity histFullEntity = power as Network.HistFullEntity;
					Network.Entity.Tag tag2 = histFullEntity.Entity.Tags.Find((Network.Entity.Tag tag) => tag.Name == 982);
					if (tag2 != null)
					{
						puzzleTypeFound = (TAG_PUZZLE_TYPE)tag2.Value;
						CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(histFullEntity.Entity.CardID);
						if (cardRecord != null && cardRecord.Name != null && cardRecord.TextInHand != null)
						{
							puzzleNameFound = cardRecord.Name;
							puzzleTextFound = cardRecord.TextInHand;
						}
					}
				}
				if (power.Type == Network.PowerType.TAG_CHANGE)
				{
					Network.HistTagChange histTagChange = power as Network.HistTagChange;
					if (histTagChange.Tag == 980 && histTagChange.Value != 0)
					{
						currentPuzzleProgressFound = histTagChange.Value;
					}
					if (histTagChange.Tag == 981 && histTagChange.Value != 0)
					{
						totalPuzzleProgressFound = histTagChange.Value;
					}
					if (currentPuzzleProgressFound != 0 && totalPuzzleProgressFound != 0)
					{
						puzzleInfoFound = true;
						break;
					}
				}
			}
		});
		currentPuzzleProgress = currentPuzzleProgressFound;
		totalPuzzleProgress = totalPuzzleProgressFound;
		puzzleName = puzzleNameFound;
		puzzleText = puzzleTextFound;
		puzzleType = puzzleTypeFound;
		return puzzleInfoFound;
	}

	// Token: 0x060039A5 RID: 14757 RVA: 0x0012486C File Offset: 0x00122A6C
	private void OnConfirmButtonReleased(UIEvent e)
	{
		if (GameMgr.Get().IsSpectator())
		{
			return;
		}
		((NormalButton)e.GetElement()).SetEnabled(false, false);
		this.m_confirmButtonPressed = true;
		bool flag = GameState.Get().WasConcedeRequested();
		if (this.m_entranceFinished || flag)
		{
			this.ProgressPastConfirmButton();
		}
	}

	// Token: 0x060039A6 RID: 14758 RVA: 0x001248BA File Offset: 0x00122ABA
	private void ProgressPastConfirmButton()
	{
		this.m_introSpell.ActivateState(SpellStateType.DEATH);
		this.m_confirmButton.m_button.GetComponent<PlayMakerFSM>().SendEvent("Death");
	}

	// Token: 0x060039A7 RID: 14759 RVA: 0x001248E4 File Offset: 0x00122AE4
	private void OnSpellEvent(string eventName, object eventData, object userData)
	{
		if (eventName == "EntranceFinished")
		{
			bool flag = GameState.Get().WasConcedeRequested();
			this.m_entranceFinished = true;
			if (this.m_confirmButtonPressed || flag)
			{
				this.ProgressPastConfirmButton();
			}
		}
	}

	// Token: 0x060039A8 RID: 14760 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060039A9 RID: 14761 RVA: 0x00090064 File Offset: 0x0008E264
	protected virtual string GetBossDeathLine()
	{
		return null;
	}

	// Token: 0x060039AA RID: 14762 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x060039AB RID: 14763 RVA: 0x00112BA9 File Offset: 0x00110DA9
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 0.5f;
	}

	// Token: 0x060039AC RID: 14764 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x060039AD RID: 14765 RVA: 0x00124920 File Offset: 0x00122B20
	protected virtual void OnBossHeroPowerPlayed(Entity entity)
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
		List<string> bossHeroPowerRandomLines = this.GetBossHeroPowerRandomLines();
		string text = "";
		while (bossHeroPowerRandomLines.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, bossHeroPowerRandomLines.Count);
			text = bossHeroPowerRandomLines[index];
			bossHeroPowerRandomLines.RemoveAt(index);
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

	// Token: 0x060039AE RID: 14766 RVA: 0x001249F4 File Offset: 0x00122BF4
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		if (this.m_enemySpeaking)
		{
			yield break;
		}
		if (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield break;
		}
		if (entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		if (entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			this.OnBossHeroPowerPlayed(entity);
		}
		yield break;
	}

	// Token: 0x060039AF RID: 14767 RVA: 0x00124A0C File Offset: 0x00122C0C
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string bossDeathLine = this.GetBossDeathLine();
		if (!this.m_enemySpeaking && !string.IsNullOrEmpty(bossDeathLine) && gameResult == TAG_PLAYSTATE.WON)
		{
			if (this.GetShouldSupressDeathTextBubble())
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(bossDeathLine, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(bossDeathLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
	}

	// Token: 0x060039B0 RID: 14768 RVA: 0x00124AA4 File Offset: 0x00122CA4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (this.m_randomEmoteLines.Count == 0)
			{
				this.m_randomEmoteLines = new List<string>(this.s_emoteLines);
			}
			string text = base.PopRandomLineWithChance(this.m_randomEmoteLines);
			if (text != null)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(text, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
	}

	// Token: 0x060039B1 RID: 14769 RVA: 0x00124B28 File Offset: 0x00122D28
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (this.m_randomIdleLines.Count == 0)
		{
			this.m_randomIdleLines = new List<string>(this.s_idleLines);
		}
		string text = base.PopRandomLineWithChance(this.m_randomIdleLines);
		if (text != null)
		{
			Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, text, 2.5f));
		}
	}

	// Token: 0x060039B2 RID: 14770 RVA: 0x00124BBB File Offset: 0x00122DBB
	protected override IEnumerator RespondToResetGameFinishedWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (GameState.Get().GetGameEntity().GetTag(GAME_TAG.PREVIOUS_PUZZLE_COMPLETED) == 0)
		{
			if (this.m_randomRestartLines.Count == 0)
			{
				this.m_randomRestartLines = new List<string>(this.s_restartLines);
			}
			string text = base.PopRandomLineWithChance(this.m_randomRestartLines);
			if (text != null)
			{
				Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, text, 2.5f));
			}
		}
		yield break;
	}

	// Token: 0x060039B3 RID: 14771 RVA: 0x00124BD4 File Offset: 0x00122DD4
	private string GetPuzzleVictoryLine(int puzzleProgress)
	{
		switch (puzzleProgress)
		{
		case 1:
			return this.s_victoryLine_1;
		case 2:
			return this.s_victoryLine_2;
		case 3:
			return this.s_victoryLine_3;
		case 4:
			return this.s_victoryLine_4;
		case 5:
			return this.s_victoryLine_5;
		case 6:
			return this.s_victoryLine_6;
		case 7:
			return this.s_victoryLine_7;
		case 8:
			return this.s_victoryLine_8;
		case 9:
			return this.s_victoryLine_9;
		default:
			return null;
		}
	}

	// Token: 0x060039B4 RID: 14772 RVA: 0x00124C50 File Offset: 0x00122E50
	protected string GetLethalCompleteLine()
	{
		if (this.s_lethalCompleteLines.Count == 0)
		{
			return null;
		}
		if (this.m_enemySpeaking)
		{
			return null;
		}
		if (this.lethalLineUsed && UnityEngine.Random.Range(0, 100) >= 85)
		{
			return null;
		}
		this.lethalLineUsed = true;
		int index = UnityEngine.Random.Range(0, this.s_lethalCompleteLines.Count);
		string text = this.s_lethalCompleteLines[index];
		this.s_lethalCompleteLines.Remove(text);
		return text;
	}

	// Token: 0x04001F6E RID: 8046
	private static Map<GameEntityOption, bool> s_booleanOptions = BOTA_MissionEntity.InitBooleanOptions();

	// Token: 0x04001F6F RID: 8047
	private static Map<GameEntityOption, string> s_stringOptions = BOTA_MissionEntity.InitStringOptions();

	// Token: 0x04001F70 RID: 8048
	private static readonly AssetReference PuzzleIntroUI_Mirror = new AssetReference("PuzzleIntroUI_Mirror.prefab:d1c537160881d574f9ec948c60f7053a");

	// Token: 0x04001F71 RID: 8049
	private static readonly AssetReference PuzzleIntroUI_Lethal = new AssetReference("PuzzleIntroUI_Lethal.prefab:2991b0a18a580eb4dac344255b615563");

	// Token: 0x04001F72 RID: 8050
	private static readonly AssetReference PuzzleIntroUI_Survival = new AssetReference("PuzzleIntroUI_Survival.prefab:0ffd8ff37cf93e844b58b5babbba9e02");

	// Token: 0x04001F73 RID: 8051
	private static readonly AssetReference PuzzleIntroUI_Clear = new AssetReference("PuzzleIntroUI_Clear.prefab:47371bd3bd83eda48af01e1f9e4be1ee");

	// Token: 0x04001F74 RID: 8052
	private static bool s_shownEndTurnReminder = false;

	// Token: 0x04001F75 RID: 8053
	private Notification m_endTurnReminder;

	// Token: 0x04001F76 RID: 8054
	private Coroutine m_endTurnReminderCoroutine;

	// Token: 0x04001F77 RID: 8055
	private const float END_TURN_REMINDER_WAIT_TIME = 1f;

	// Token: 0x04001F78 RID: 8056
	private const float END_TURN_REMINDER_X_OFFSET = 3.1f;

	// Token: 0x04001F79 RID: 8057
	private const float THINK_EMOTE_DELAY_OVERRIDE = 50f;

	// Token: 0x04001F7A RID: 8058
	protected new const float TIME_TO_WAIT_BEFORE_ENDING_QUOTE = 2f;

	// Token: 0x04001F7B RID: 8059
	public bool m_waitingForTurnStartIndicatorAfterReset;

	// Token: 0x04001F7C RID: 8060
	private PuzzleIntroSpell m_introSpell;

	// Token: 0x04001F7D RID: 8061
	private NormalButton m_confirmButton;

	// Token: 0x04001F7E RID: 8062
	private bool m_entranceFinished;

	// Token: 0x04001F7F RID: 8063
	private bool m_confirmButtonPressed;

	// Token: 0x04001F80 RID: 8064
	public static string s_introLine = null;

	// Token: 0x04001F81 RID: 8065
	public static string s_returnLine = null;

	// Token: 0x04001F82 RID: 8066
	public bool s_returnLineOverride;

	// Token: 0x04001F83 RID: 8067
	public List<string> s_emoteLines = new List<string>();

	// Token: 0x04001F84 RID: 8068
	protected List<string> m_randomEmoteLines = new List<string>();

	// Token: 0x04001F85 RID: 8069
	public List<string> s_idleLines = new List<string>();

	// Token: 0x04001F86 RID: 8070
	protected List<string> m_randomIdleLines = new List<string>();

	// Token: 0x04001F87 RID: 8071
	public List<string> s_restartLines = new List<string>();

	// Token: 0x04001F88 RID: 8072
	protected List<string> m_randomRestartLines = new List<string>();

	// Token: 0x04001F89 RID: 8073
	public string s_victoryLine_1;

	// Token: 0x04001F8A RID: 8074
	public string s_victoryLine_2;

	// Token: 0x04001F8B RID: 8075
	public string s_victoryLine_3;

	// Token: 0x04001F8C RID: 8076
	public string s_victoryLine_4;

	// Token: 0x04001F8D RID: 8077
	public string s_victoryLine_5;

	// Token: 0x04001F8E RID: 8078
	public string s_victoryLine_6;

	// Token: 0x04001F8F RID: 8079
	public string s_victoryLine_7;

	// Token: 0x04001F90 RID: 8080
	public string s_victoryLine_8;

	// Token: 0x04001F91 RID: 8081
	public string s_victoryLine_9;

	// Token: 0x04001F92 RID: 8082
	public List<string> s_lethalCompleteLines = new List<string>();

	// Token: 0x04001F93 RID: 8083
	private bool lethalLineUsed;

	// Token: 0x04001F94 RID: 8084
	private const int chanceToPlay = 85;
}
