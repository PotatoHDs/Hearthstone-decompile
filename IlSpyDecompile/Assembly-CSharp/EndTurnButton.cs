using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
	private enum AlternativeAppearance
	{
		DEFAULT,
		RESTART_BUTTON,
		DONE_BUTTON,
		COUNTDOWN
	}

	public ActorStateMgr m_ActorStateMgr;

	public UberText m_MyTurnText;

	public UberText m_WaitingText;

	public GameObject m_GreenHighlight;

	public GameObject m_WhiteHighlight;

	public GameObject m_EndTurnButtonMesh;

	public List<Material> m_AlternativeMaterials;

	private static EndTurnButton s_instance;

	private bool m_inputBlocked;

	private bool m_pressed;

	private bool m_playedNmpSoundThisTurn;

	private bool m_mousedOver;

	private bool m_disabled;

	private int m_inputBlockers;

	public bool IsDisabled => m_disabled;

	private void Awake()
	{
		s_instance = this;
		m_MyTurnText.Text = GetEndTurnText();
		m_WaitingText.Text = "";
		GetComponent<Collider>().enabled = false;
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	private void Start()
	{
		StartCoroutine(WaitAFrameAndThenChangeState());
	}

	public static EndTurnButton Get()
	{
		return s_instance;
	}

	public void Reset()
	{
		bool flag = HasExtraTurn();
		TurnStartManager.Get().NotifyOfExtraTurn(TurnStartManager.Get().GetExtraTurnSpell(), !flag);
		bool flag2 = OpponentHasExtraTurn();
		TurnStartManager.Get().NotifyOfExtraTurn(TurnStartManager.Get().GetExtraTurnSpell(isFriendly: false), !flag2, isFriendly: false);
		UpdateState();
		GameState gameState = GameState.Get();
		if (gameState.IsPastBeginPhase() && gameState.IsLocalSidePlayerTurn())
		{
			GetComponent<Collider>().enabled = true;
		}
		else
		{
			GetComponent<Collider>().enabled = false;
		}
	}

	public GameObject GetButtonContainer()
	{
		return base.transform.Find("ButtonContainer").gameObject;
	}

	public void PlayPushDownAnimation()
	{
		if (!m_inputBlocked && !IsInWaitingState() && !m_pressed)
		{
			m_pressed = true;
			GetButtonContainer().GetComponent<Animation>().Play("ENDTURN_PRESSED_DOWN");
			SoundManager.Get().LoadAndPlay("FX_EndTurn_Down.prefab:7f967e178760e5d409cec10ad56cc3ff");
		}
	}

	public void PlayButtonUpAnimation()
	{
		if (!m_inputBlocked && !IsInWaitingState() && m_pressed)
		{
			m_pressed = false;
			GetButtonContainer().GetComponent<Animation>().Play("ENDTURN_PRESSED_UP");
			SoundManager.Get().LoadAndPlay("FX_EndTurn_Up.prefab:aa092f360d27b5244b030e737d720ba6");
		}
	}

	public bool IsInWaitingState()
	{
		return m_ActorStateMgr.GetActiveStateType() switch
		{
			ActorStateType.ENDTURN_WAITING => true, 
			ActorStateType.ENDTURN_NMP_2_WAITING => true, 
			ActorStateType.ENDTURN_WAITING_TIMER => true, 
			_ => false, 
		};
	}

	public bool IsInNMPState()
	{
		return m_ActorStateMgr.GetActiveStateType() switch
		{
			ActorStateType.ENDTURN_NO_MORE_PLAYS => true, 
			ActorStateType.EXTRATURN_NO_MORE_PLAYS => true, 
			_ => false, 
		};
	}

	public bool IsInYouHavePlaysState()
	{
		return m_ActorStateMgr.GetActiveStateType() switch
		{
			ActorStateType.ENDTURN_YOUR_TURN => true, 
			ActorStateType.EXTRATURN_YOUR_TURN => true, 
			_ => false, 
		};
	}

	public bool HasNoMorePlays()
	{
		Network.Options optionsPacket = GameState.Get().GetOptionsPacket();
		if (optionsPacket == null)
		{
			return false;
		}
		return !optionsPacket.HasValidOption();
	}

	public bool IsInputBlocked()
	{
		if (!m_inputBlocked)
		{
			return m_inputBlockers > 0;
		}
		return true;
	}

	public void AddInputBlocker()
	{
		m_inputBlockers++;
	}

	public void RemoveInputBlocker()
	{
		m_inputBlockers--;
	}

	public void HandleMouseOver()
	{
		m_mousedOver = true;
		if (!m_inputBlocked)
		{
			PutInMouseOverState();
		}
	}

	public void HandleMouseOut()
	{
		m_mousedOver = false;
		if (!m_inputBlocked)
		{
			if (m_pressed)
			{
				PlayButtonUpAnimation();
			}
			PutInMouseOffState();
		}
	}

	private void PutInMouseOverState()
	{
		if (IsInNMPState())
		{
			m_WhiteHighlight.SetActive(value: false);
			m_GreenHighlight.SetActive(value: true);
			Hashtable args = iTween.Hash("from", m_GreenHighlight.GetComponent<Renderer>().GetMaterial().GetFloat("_Intensity"), "to", 1.4f, "time", 0.15f, "easetype", iTween.EaseType.linear, "onupdate", "OnUpdateIntensityValue", "onupdatetarget", base.gameObject, "name", "ENDTURN_INTENSITY");
			iTween.StopByName(base.gameObject, "ENDTURN_INTENSITY");
			iTween.ValueTo(base.gameObject, args);
		}
		else if (IsInYouHavePlaysState())
		{
			m_WhiteHighlight.SetActive(value: true);
			m_GreenHighlight.SetActive(value: false);
		}
		else
		{
			m_WhiteHighlight.SetActive(value: false);
			m_GreenHighlight.SetActive(value: false);
		}
	}

	private void PutInMouseOffState()
	{
		m_WhiteHighlight.SetActive(value: false);
		if (IsInNMPState())
		{
			m_GreenHighlight.SetActive(value: true);
			Hashtable args = iTween.Hash("from", m_GreenHighlight.GetComponent<Renderer>().GetMaterial().GetFloat("_Intensity"), "to", 1.1f, "time", 0.15f, "easetype", iTween.EaseType.linear, "onupdate", "OnUpdateIntensityValue", "onupdatetarget", base.gameObject, "name", "ENDTURN_INTENSITY");
			iTween.StopByName(base.gameObject, "ENDTURN_INTENSITY");
			iTween.ValueTo(base.gameObject, args);
		}
		else
		{
			m_GreenHighlight.SetActive(value: false);
		}
	}

	private void OnUpdateIntensityValue(float newValue)
	{
		m_GreenHighlight.GetComponent<Renderer>().GetMaterial().SetFloat("_Intensity", newValue);
	}

	private IEnumerator WaitAFrameAndThenChangeState()
	{
		yield return null;
		if (GameState.Get() == null)
		{
			Log.Gameplay.PrintError("EndTurnButton.WaitAFrameAndThenChangeState(): Game state does not exist.");
			yield break;
		}
		if (GameState.Get().IsGameCreated())
		{
			HandleGameStart();
			yield break;
		}
		m_ActorStateMgr.ChangeState(ActorStateType.ENDTURN_WAITING);
		GameState.Get().RegisterCreateGameListener(OnCreateGame);
	}

	private void HandleGameStart()
	{
		UpdateState();
		ApplyAlternativeAppearance();
		GameState gameState = GameState.Get();
		if (gameState.IsPastBeginPhase() && gameState.IsLocalSidePlayerTurn())
		{
			GetComponent<Collider>().enabled = true;
			GameState.Get().RegisterOptionsReceivedListener(OnOptionsReceived);
		}
	}

	private int GetCurrentAlternativeAppearanceIndex()
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return 0;
		}
		return gameState.GetGameEntity()?.GetTag(GAME_TAG.END_TURN_BUTTON_ALTERNATIVE_APPEARANCE) ?? 0;
	}

	public void ApplyAlternativeAppearance()
	{
		int currentAlternativeAppearanceIndex = GetCurrentAlternativeAppearanceIndex();
		if (currentAlternativeAppearanceIndex != 1)
		{
			_ = 2;
			return;
		}
		if (m_AlternativeMaterials.Count >= currentAlternativeAppearanceIndex && m_AlternativeMaterials[currentAlternativeAppearanceIndex - 1] != null)
		{
			m_EndTurnButtonMesh.GetComponent<Renderer>().SetMaterial(m_AlternativeMaterials[currentAlternativeAppearanceIndex - 1]);
			return;
		}
		Log.Gameplay.PrintError("EndTurnButton.ApplyAlternativeAppearance(): No material exists for appearance  {0}.", currentAlternativeAppearanceIndex);
	}

	private void SetButtonState(ActorStateType stateType)
	{
		if (m_ActorStateMgr == null)
		{
			Debug.Log("End Turn Button Actor State Manager is missing!");
		}
		else if (m_ActorStateMgr.GetActiveStateType() != stateType && !IsInputBlocked() && (!m_disabled || stateType == ActorStateType.ENDTURN_WAITING))
		{
			m_ActorStateMgr.ChangeState(stateType);
			if (stateType == ActorStateType.ENDTURN_YOUR_TURN || stateType == ActorStateType.ENDTURN_WAITING_TIMER)
			{
				m_inputBlocked = true;
				StartCoroutine(WaitUntilAnimationIsCompleteAndThenUnblockInput());
			}
		}
	}

	private IEnumerator WaitUntilAnimationIsCompleteAndThenUnblockInput()
	{
		yield return new WaitForSeconds(m_ActorStateMgr.GetMaximumAnimationTimeOfActiveStates());
		m_inputBlocked = false;
		if (HasNoMorePlays())
		{
			SetStateToNoMorePlays();
		}
	}

	private void UpdateState()
	{
		if (!GameState.Get().IsMulliganManagerActive() && !GameState.Get().IsTurnStartManagerBlockingInput())
		{
			if (!GameState.Get().IsLocalSidePlayerTurn() || !GameState.Get().GetGameEntity().IsCurrentTurnRealTime())
			{
				UpdateButtonText();
				SetStateToWaiting();
			}
			else if (GameState.Get().GetResponseMode() != 0)
			{
				SetStateToYourTurn();
			}
		}
	}

	public void DisplayExtraTurnState()
	{
		UpdateState();
	}

	private bool HasExtraTurn()
	{
		return GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.NUM_TURNS_LEFT) > 1;
	}

	private bool OpponentHasExtraTurn()
	{
		return GameState.Get().GetOpposingSidePlayer().GetTag(GAME_TAG.NUM_TURNS_LEFT) > 1;
	}

	private ActorStateType GetAppropriateYourTurnState()
	{
		if (HasExtraTurn())
		{
			if (IsInWaitingState())
			{
				return ActorStateType.WAITING_TO_EXTRATURN;
			}
			return ActorStateType.EXTRATURN_YOUR_TURN;
		}
		return ActorStateType.ENDTURN_YOUR_TURN;
	}

	private ActorStateType GetAppropriateYourTurnNMPState()
	{
		if (HasExtraTurn())
		{
			return ActorStateType.EXTRATURN_NO_MORE_PLAYS;
		}
		return ActorStateType.ENDTURN_NO_MORE_PLAYS;
	}

	private string GetEndTurnText()
	{
		switch (GetCurrentAlternativeAppearanceIndex())
		{
		case 1:
		case 3:
			return "";
		case 2:
			return GameStrings.Get("GAMEPLAY_DONE_TURN");
		default:
			return GameStrings.Get("GAMEPLAY_END_TURN");
		}
	}

	private string GetEnemyTurnText()
	{
		int currentAlternativeAppearanceIndex = GetCurrentAlternativeAppearanceIndex();
		if ((uint)(currentAlternativeAppearanceIndex - 1) <= 2u)
		{
			return "";
		}
		return GameStrings.Get("GAMEPLAY_ENEMY_TURN");
	}

	private void UpdateButtonText()
	{
		switch (GetCurrentAlternativeAppearanceIndex())
		{
		case 1:
			m_MyTurnText.SetGameStringText("");
			m_WaitingText.SetGameStringText("");
			break;
		case 2:
			m_MyTurnText.SetGameStringText("GAMEPLAY_DONE_TURN");
			m_WaitingText.SetGameStringText("");
			break;
		case 3:
			m_MyTurnText.SetGameStringText("");
			m_WaitingText.SetGameStringText("");
			break;
		default:
			if (HasExtraTurn())
			{
				m_MyTurnText.SetGameStringText(GameStrings.Get("GAMEPLAY_NEXT_TURN"));
				m_WaitingText.SetGameStringText(GameStrings.Get("GAMEPLAY_NEXT_TURN"));
			}
			else
			{
				m_MyTurnText.SetGameStringText(GameStrings.Get("GAMEPLAY_END_TURN"));
				m_WaitingText.SetGameStringText(GameStrings.Get("GAMEPLAY_ENEMY_TURN"));
			}
			break;
		}
		m_MyTurnText.UpdateText();
		m_WaitingText.UpdateText();
	}

	private void SetStateToYourTurn()
	{
		UpdateButtonText();
		if (m_ActorStateMgr == null)
		{
			return;
		}
		if (HasNoMorePlays())
		{
			SetStateToNoMorePlays();
			return;
		}
		SetButtonState(GetAppropriateYourTurnState());
		if (m_mousedOver)
		{
			PutInMouseOverState();
		}
		else
		{
			PutInMouseOffState();
		}
	}

	private void SetStateToNoMorePlays()
	{
		if (m_ActorStateMgr == null)
		{
			return;
		}
		if (IsInWaitingState())
		{
			SetButtonState(GetAppropriateYourTurnState());
		}
		else
		{
			SetButtonState(GetAppropriateYourTurnNMPState());
			if (m_mousedOver)
			{
				PutInMouseOverState();
			}
			else
			{
				PutInMouseOffState();
			}
		}
		if (!m_playedNmpSoundThisTurn && !GameState.Get().GetGameEntity().HasTag(GAME_TAG.SUPPRESS_JOBS_DONE_VO))
		{
			m_playedNmpSoundThisTurn = true;
			StartCoroutine(PlayEndTurnSound());
		}
	}

	private void SetStateToWaiting()
	{
		if (!(m_ActorStateMgr == null) && !IsInWaitingState() && !GameState.Get().IsGameOver())
		{
			if (IsInNMPState())
			{
				SetButtonState(ActorStateType.ENDTURN_NMP_2_WAITING);
			}
			else
			{
				SetButtonState(ActorStateType.ENDTURN_WAITING);
			}
			PutInMouseOffState();
		}
	}

	private IEnumerator PlayEndTurnSound()
	{
		yield return new WaitForSeconds(1.5f);
		if (IsInNMPState())
		{
			SoundManager.Get().LoadAndPlay("VO_JobsDone.prefab:88cda3fac32785c4d8101966b7604cc3", base.gameObject);
		}
	}

	private void OnCreateGame(GameState.CreateGamePhase phase, object userData)
	{
		if (phase == GameState.CreateGamePhase.CREATED)
		{
			GameState.Get().UnregisterCreateGameListener(OnCreateGame);
			HandleGameStart();
		}
	}

	public void OnMulliganEnded()
	{
		m_WaitingText.Text = GetEnemyTurnText();
	}

	public void OnTurnStartManagerFinished()
	{
		if (GameState.Get().GetGameEntity().IsCurrentTurnRealTime())
		{
			PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
			m_playedNmpSoundThisTurn = false;
			SetStateToYourTurn();
			GetComponent<Collider>().enabled = true;
			GameState.Get().RegisterOptionsReceivedListener(OnOptionsReceived);
		}
	}

	public void OnTurnChanged()
	{
		UpdateState();
	}

	public void OnEndTurnRequested()
	{
		PegCursor.Get().SetMode(PegCursor.Mode.WAITING);
		SetStateToWaiting();
		GetComponent<Collider>().enabled = false;
		GameState.Get().UnregisterOptionsReceivedListener(OnOptionsReceived);
	}

	private void OnOptionsReceived(object userData)
	{
		UpdateState();
	}

	public void OnTurnTimerStart()
	{
		if (!m_inputBlocked)
		{
			_ = m_mousedOver;
		}
	}

	public void OnTurnTimerEnded(bool isFriendlyPlayerTurnTimer)
	{
		if (isFriendlyPlayerTurnTimer)
		{
			SetButtonState(ActorStateType.ENDTURN_WAITING_TIMER);
		}
	}

	public void SetDisabled(bool disabled)
	{
		m_disabled = disabled;
		if (m_disabled)
		{
			SetButtonState(ActorStateType.ENDTURN_WAITING);
		}
	}
}
