using System.Collections;
using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

public class BOTA_MissionEntity : GenericDungeonMissionEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private static readonly AssetReference PuzzleIntroUI_Mirror = new AssetReference("PuzzleIntroUI_Mirror.prefab:d1c537160881d574f9ec948c60f7053a");

	private static readonly AssetReference PuzzleIntroUI_Lethal = new AssetReference("PuzzleIntroUI_Lethal.prefab:2991b0a18a580eb4dac344255b615563");

	private static readonly AssetReference PuzzleIntroUI_Survival = new AssetReference("PuzzleIntroUI_Survival.prefab:0ffd8ff37cf93e844b58b5babbba9e02");

	private static readonly AssetReference PuzzleIntroUI_Clear = new AssetReference("PuzzleIntroUI_Clear.prefab:47371bd3bd83eda48af01e1f9e4be1ee");

	private static bool s_shownEndTurnReminder = false;

	private Notification m_endTurnReminder;

	private Coroutine m_endTurnReminderCoroutine;

	private const float END_TURN_REMINDER_WAIT_TIME = 1f;

	private const float END_TURN_REMINDER_X_OFFSET = 3.1f;

	private const float THINK_EMOTE_DELAY_OVERRIDE = 50f;

	protected new const float TIME_TO_WAIT_BEFORE_ENDING_QUOTE = 2f;

	public bool m_waitingForTurnStartIndicatorAfterReset;

	private PuzzleIntroSpell m_introSpell;

	private NormalButton m_confirmButton;

	private bool m_entranceFinished;

	private bool m_confirmButtonPressed;

	public static string s_introLine = null;

	public static string s_returnLine = null;

	public bool s_returnLineOverride;

	public List<string> s_emoteLines = new List<string>();

	protected List<string> m_randomEmoteLines = new List<string>();

	public List<string> s_idleLines = new List<string>();

	protected List<string> m_randomIdleLines = new List<string>();

	public List<string> s_restartLines = new List<string>();

	protected List<string> m_randomRestartLines = new List<string>();

	public string s_victoryLine_1;

	public string s_victoryLine_2;

	public string s_victoryLine_3;

	public string s_victoryLine_4;

	public string s_victoryLine_5;

	public string s_victoryLine_6;

	public string s_victoryLine_7;

	public string s_victoryLine_8;

	public string s_victoryLine_9;

	public List<string> s_lethalCompleteLines = new List<string>();

	private bool lethalLineUsed;

	private const int chanceToPlay = 85;

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.HANDLE_COIN,
			false
		} };
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	public BOTA_MissionEntity()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	public override void OnCreateGame()
	{
		if (!s_shownEndTurnReminder)
		{
			GameState.Get().RegisterOptionsReceivedListener(OnOptionsReceived);
			GameState.Get().RegisterGameOverListener(OnGameOver);
		}
	}

	public override void OnDecommissionGame()
	{
		GameState.Get().UnregisterOptionsReceivedListener(OnOptionsReceived);
		GameState.Get().UnregisterGameOverListener(OnGameOver);
	}

	public override float? GetThinkEmoteDelayOverride()
	{
		return 50f + Random.Range(0f, 20f);
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BOT);
	}

	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BOTMulligan);
		}
	}

	public IEnumerator UnBusyInSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GameState.Get().SetBusy(busy: false);
	}

	private IEnumerator ShowEndTurnReminderIfNeeded()
	{
		yield return new WaitForSeconds(1f);
		Network.Options optionsPacket = GameState.Get().GetOptionsPacket();
		if (optionsPacket != null && !optionsPacket.HasValidOption() && !s_shownEndTurnReminder)
		{
			s_shownEndTurnReminder = true;
			GameState.Get().UnregisterOptionsReceivedListener(OnOptionsReceived);
			Vector3 position = EndTurnButton.Get().transform.position;
			position.x -= 3.1f;
			m_endTurnReminder = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("BOTA_PUZZLE_END_TURN_REMINDER"));
			m_endTurnReminder.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
			m_endTurnReminderCoroutine = null;
		}
	}

	private void OnOptionsReceived(object userData)
	{
		if (!SpectatorManager.Get().IsInSpectatorMode())
		{
			if (m_endTurnReminderCoroutine != null)
			{
				Gameplay.Get().StopCoroutine(m_endTurnReminderCoroutine);
				m_endTurnReminderCoroutine = null;
			}
			Network.Options optionsPacket = GameState.Get().GetOptionsPacket();
			if (optionsPacket == null)
			{
				Log.Gameplay.PrintError("BOTA_MissionEntity wants options packet but option packet is null.");
			}
			else if (!s_shownEndTurnReminder && !optionsPacket.HasValidOption())
			{
				m_endTurnReminderCoroutine = Gameplay.Get().StartCoroutine(ShowEndTurnReminderIfNeeded());
			}
		}
	}

	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		DestroyEndTurnReminder();
	}

	public override void NotifyOfResetGameStarted()
	{
		base.NotifyOfResetGameStarted();
		DestroyEndTurnReminder();
	}

	public override void NotifyOfResetGameFinished(Entity source, Entity oldGameEntity)
	{
		m_waitingForTurnStartIndicatorAfterReset = true;
		BOTA_MissionEntity bOTA_MissionEntity = oldGameEntity as BOTA_MissionEntity;
		s_lethalCompleteLines = bOTA_MissionEntity.s_lethalCompleteLines;
		lethalLineUsed = bOTA_MissionEntity.lethalLineUsed;
		m_randomEmoteLines = bOTA_MissionEntity.m_randomEmoteLines;
		m_randomIdleLines = bOTA_MissionEntity.m_randomIdleLines;
		m_randomRestartLines = bOTA_MissionEntity.m_randomRestartLines;
		base.NotifyOfResetGameFinished(source, oldGameEntity);
	}

	public override void OnTurnStartManagerFinished()
	{
		if (!m_waitingForTurnStartIndicatorAfterReset || GameState.Get().GetGameEntity().GetTag(GAME_TAG.PREVIOUS_PUZZLE_COMPLETED) != 0)
		{
			Gameplay.Get().StartCoroutine(OnTurnStartManagerFinishedWithTiming());
		}
	}

	public virtual IEnumerator OnTurnStartManagerFinishedWithTiming()
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		yield return RespondToPuzzleStartWithTiming();
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		int tag = GameState.Get().GetFriendlySidePlayer().GetSecretZone()
			.GetPuzzleEntity()
			.GetTag(GAME_TAG.PUZZLE_PROGRESS);
		string puzzleVictoryLine = GetPuzzleVictoryLine(tag);
		if (puzzleVictoryLine != null)
		{
			yield return PlayBossLine(actor, puzzleVictoryLine);
		}
	}

	protected virtual IEnumerator RespondToPuzzleStartWithTiming()
	{
		yield break;
	}

	private void DestroyEndTurnReminder()
	{
		if (m_endTurnReminderCoroutine != null)
		{
			Gameplay.Get().StopCoroutine(m_endTurnReminderCoroutine);
			m_endTurnReminderCoroutine = null;
		}
		if (m_endTurnReminder != null)
		{
			NotificationManager.Get().DestroyNotification(m_endTurnReminder, 0f);
		}
	}

	public override bool NotifyOfEndTurnButtonPushed()
	{
		DestroyEndTurnReminder();
		return true;
	}

	public override IEnumerator DoGameSpecificPostIntroActions()
	{
		m_entranceFinished = false;
		m_confirmButtonPressed = false;
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
		for (int i = 0; i < maxNumAttempts; i++)
		{
			puzzleInfoFound = LookUpPuzzleInfoFromFutureTaskLists(out currentPuzzleProgress, out totalPuzzleProgress, out puzzleName, out puzzleText, out puzzleType);
			if (puzzleInfoFound)
			{
				break;
			}
			yield return new WaitForSeconds(1f);
		}
		if (!puzzleInfoFound)
		{
			Log.Spells.PrintError("BOTA_MissionEntity.DoGameSpecificPostIntroActions(): puzzle info could not be found in the task lists - most likely the script for this game entity is not setting up a puzzle entity correctly.");
			if (puzzleType == TAG_PUZZLE_TYPE.INVALID)
			{
				yield break;
			}
		}
		GameObject gameObject = LoadIntroUIForPuzzleType(puzzleType);
		PuzzleProgressUI component = gameObject.GetComponent<PuzzleProgressUI>();
		if (component == null)
		{
			Log.Spells.PrintError("BOTA_MissionEntity.DoGameSpecificPostIntroActions(): No PuzzleProgressUI found on puzzle intro spell {0}.", gameObject.gameObject.name);
			yield break;
		}
		component.UpdateNameAndText(puzzleName, puzzleText);
		component.UpdateProgressValues(currentPuzzleProgress, totalPuzzleProgress);
		m_introSpell = gameObject.GetComponent<PuzzleIntroSpell>();
		if (m_introSpell == null)
		{
			Log.Spells.PrintError("BOTA_MissionEntity.DoGameSpecificPostIntroActions(): No PuzzleIntroSpell found on puzzle intro spell {0}.", gameObject.gameObject.name);
			yield break;
		}
		if (m_introSpell.GetConfirmButton() == null)
		{
			Log.Spells.PrintError("BOTA_MissionEntity.DoGameSpecificPostIntroActions(): No confirmButton found on puzzle intro spell {0}.", gameObject.gameObject.name);
			yield break;
		}
		m_confirmButton = m_introSpell.GetConfirmButton().GetComponentInChildren<NormalButton>();
		if (m_confirmButton == null)
		{
			Log.Spells.PrintError($"BOTA_MissionEntity.DoGameSpecificPostIntroActions() - ERROR \"{m_introSpell.GetConfirmButton()}\" has no {typeof(NormalButton)} component");
			yield break;
		}
		m_introSpell.AddSpellEventCallback(OnSpellEvent);
		m_confirmButton.SetText(GameStrings.Get("GLOBAL_CONFIRM"));
		m_confirmButton.AddEventListener(UIEventType.RELEASE, OnConfirmButtonReleased);
		m_confirmButton.GetComponent<Collider>().enabled = true;
		m_confirmButton.m_button.GetComponent<PlayMakerFSM>().SendEvent("Birth");
		m_introSpell.ActivateState(SpellStateType.BIRTH);
		while (m_introSpell != null && !m_introSpell.IsFinished())
		{
			if (GameState.Get().WasConcedeRequested())
			{
				if (!m_confirmButtonPressed && m_entranceFinished)
				{
					m_confirmButton.SetEnabled(enabled: false);
					ProgressPastConfirmButton();
				}
				yield break;
			}
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (currentPuzzleProgress == 1)
		{
			Gameplay.Get().StartCoroutine(PlayBossLine(actor, s_introLine));
		}
		else if (s_returnLineOverride)
		{
			GameEntity gameEntity = GameState.Get().GetGameEntity();
			gameEntity.SetTag(GAME_TAG.MISSION_EVENT, 77);
			gameEntity.SetTag(GAME_TAG.MISSION_EVENT, 0);
		}
		else
		{
			Gameplay.Get().StartCoroutine(PlayBossLine(actor, s_returnLine));
		}
	}

	private GameObject LoadIntroUIForPuzzleType(TAG_PUZZLE_TYPE puzzleType)
	{
		switch (puzzleType)
		{
		case TAG_PUZZLE_TYPE.INVALID:
			Log.Spells.PrintError($"BOTA_MissionEntity.LoadIntroUIForPuzzleType() - invalid puzzle type");
			return null;
		case TAG_PUZZLE_TYPE.MIRROR:
			return AssetLoader.Get().InstantiatePrefab(PuzzleIntroUI_Mirror);
		case TAG_PUZZLE_TYPE.LETHAL:
			return AssetLoader.Get().InstantiatePrefab(PuzzleIntroUI_Lethal);
		case TAG_PUZZLE_TYPE.SURVIVAL:
			return AssetLoader.Get().InstantiatePrefab(PuzzleIntroUI_Survival);
		case TAG_PUZZLE_TYPE.CLEAR:
			return AssetLoader.Get().InstantiatePrefab(PuzzleIntroUI_Clear);
		default:
			return null;
		}
	}

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
			if (currentPuzzleProgressFound == 0 || totalPuzzleProgressFound == 0)
			{
				foreach (PowerTask task in taskList.GetTaskList())
				{
					Network.PowerHistory power = task.GetPower();
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
			}
		});
		currentPuzzleProgress = currentPuzzleProgressFound;
		totalPuzzleProgress = totalPuzzleProgressFound;
		puzzleName = puzzleNameFound;
		puzzleText = puzzleTextFound;
		puzzleType = puzzleTypeFound;
		return puzzleInfoFound;
	}

	private void OnConfirmButtonReleased(UIEvent e)
	{
		if (!GameMgr.Get().IsSpectator())
		{
			((NormalButton)e.GetElement()).SetEnabled(enabled: false);
			m_confirmButtonPressed = true;
			bool flag = GameState.Get().WasConcedeRequested();
			if (m_entranceFinished || flag)
			{
				ProgressPastConfirmButton();
			}
		}
	}

	private void ProgressPastConfirmButton()
	{
		m_introSpell.ActivateState(SpellStateType.DEATH);
		m_confirmButton.m_button.GetComponent<PlayMakerFSM>().SendEvent("Death");
	}

	private void OnSpellEvent(string eventName, object eventData, object userData)
	{
		if (eventName == "EntranceFinished")
		{
			bool flag = GameState.Get().WasConcedeRequested();
			m_entranceFinished = true;
			if (m_confirmButtonPressed || flag)
			{
				ProgressPastConfirmButton();
			}
		}
	}

	protected virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected virtual string GetBossDeathLine()
	{
		return null;
	}

	protected virtual bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 0.5f;
	}

	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	protected virtual void OnBossHeroPowerPlayed(Entity entity)
	{
		float num = ChanceToPlayBossHeroPowerVOLine();
		float num2 = Random.Range(0f, 1f);
		if (m_enemySpeaking || num < num2)
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (actor == null)
		{
			return;
		}
		List<string> bossHeroPowerRandomLines = GetBossHeroPowerRandomLines();
		string text = "";
		while (bossHeroPowerRandomLines.Count > 0)
		{
			int index = Random.Range(0, bossHeroPowerRandomLines.Count);
			text = bossHeroPowerRandomLines[index];
			bossHeroPowerRandomLines.RemoveAt(index);
			if (!NotificationManager.Get().HasSoundPlayedThisSession(text))
			{
				break;
			}
		}
		if (!(text == ""))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce(text, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		if (!m_enemySpeaking && entity.GetCardType() != 0 && entity.GetCardType() == TAG_CARDTYPE.HERO_POWER && entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			OnBossHeroPowerPlayed(entity);
		}
		yield break;
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string bossDeathLine = GetBossDeathLine();
		if (!m_enemySpeaking && !string.IsNullOrEmpty(bossDeathLine) && gameResult == TAG_PLAYSTATE.WON)
		{
			if (GetShouldSupressDeathTextBubble())
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(bossDeathLine, Notification.SpeechBubbleDirection.None, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(bossDeathLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (m_randomEmoteLines.Count == 0)
			{
				m_randomEmoteLines = new List<string>(s_emoteLines);
			}
			string text = PopRandomLineWithChance(m_randomEmoteLines);
			if (text != null)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(text, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (currentPlayer.IsFriendlySide() && !currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			if (m_randomIdleLines.Count == 0)
			{
				m_randomIdleLines = new List<string>(s_idleLines);
			}
			string text = PopRandomLineWithChance(m_randomIdleLines);
			if (text != null)
			{
				Gameplay.Get().StartCoroutine(PlayBossLine(actor, text));
			}
		}
	}

	protected override IEnumerator RespondToResetGameFinishedWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (GameState.Get().GetGameEntity().GetTag(GAME_TAG.PREVIOUS_PUZZLE_COMPLETED) == 0)
		{
			if (m_randomRestartLines.Count == 0)
			{
				m_randomRestartLines = new List<string>(s_restartLines);
			}
			string text = PopRandomLineWithChance(m_randomRestartLines);
			if (text != null)
			{
				Gameplay.Get().StartCoroutine(PlayBossLine(actor, text));
			}
		}
	}

	private string GetPuzzleVictoryLine(int puzzleProgress)
	{
		return puzzleProgress switch
		{
			1 => s_victoryLine_1, 
			2 => s_victoryLine_2, 
			3 => s_victoryLine_3, 
			4 => s_victoryLine_4, 
			5 => s_victoryLine_5, 
			6 => s_victoryLine_6, 
			7 => s_victoryLine_7, 
			8 => s_victoryLine_8, 
			9 => s_victoryLine_9, 
			_ => null, 
		};
	}

	protected string GetLethalCompleteLine()
	{
		if (s_lethalCompleteLines.Count == 0)
		{
			return null;
		}
		if (m_enemySpeaking)
		{
			return null;
		}
		if (lethalLineUsed && Random.Range(0, 100) >= 85)
		{
			return null;
		}
		lethalLineUsed = true;
		int index = Random.Range(0, s_lethalCompleteLines.Count);
		string text = s_lethalCompleteLines[index];
		s_lethalCompleteLines.Remove(text);
		return text;
	}
}
