using System.Collections;
using UnityEngine;

public class TurnTimer : MonoBehaviour
{
	public float m_DebugTimeout = 30f;

	public float m_DebugTimeoutStart = 20f;

	public float m_RopeCapSeconds = 20f;

	public GameObject m_SparksObject;

	public Transform m_SparksStartBone;

	public Transform m_SparksFinishBone;

	public UberText m_CountdownText;

	public Color m_CountdownTextColorNormal;

	public Color m_CountdownTextColorRope;

	public GameObject m_FuseWickObject;

	public GameObject m_FuseShadowObject;

	public string m_FuseMatValName = "_Xamount";

	public float m_FuseMatValStart = 0.42f;

	public float m_FuseMatValFinish = -1.5f;

	public float m_FuseXamountAnimation = -1.5f;

	public SoundDef m_TickSound;

	public SoundDef m_FinalTickSound;

	private const float BIRTH_ANIMATION_TIME = 1f;

	private static TurnTimer s_instance;

	private Spell m_spell;

	private TurnTimerState m_state;

	private float m_countdownTimeoutSec;

	private float m_countdownEndTimestamp;

	private uint m_currentMoveAnimId;

	private uint m_currentMatValAnimId;

	private bool m_currentTimerBelongsToFriendlySidePlayer;

	private bool m_waitingForTurnStartManagerFinish;

	private int m_lastTickSecondNumber;

	private Coroutine m_countdownAnimsWhenBelowCapCoroutine;

	private TurnTimerGameModeSettings m_gameModeSettings;

	private void Awake()
	{
		s_instance = this;
		m_spell = GetComponent<Spell>();
		m_spell.AddStateStartedCallback(OnSpellStateStarted);
		if (GameState.Get() != null)
		{
			GameState.Get().RegisterCurrentPlayerChangedListener(OnCurrentPlayerChanged);
			GameState.Get().RegisterFriendlyTurnStartedListener(OnFriendlyTurnStarted);
			GameState.Get().RegisterTurnTimerUpdateListener(OnTurnTimerUpdate);
			GameState.Get().RegisterGameOverListener(OnGameOver);
		}
		SetGameModeSettings(new TurnTimerGameModeSettings());
	}

	private void OnDestroy()
	{
		s_instance = null;
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterCurrentPlayerChangedListener(OnCurrentPlayerChanged);
			GameState.Get().UnregisterFriendlyTurnStartedListener(OnFriendlyTurnStarted);
			GameState.Get().UnregisterTurnTimerUpdateListener(OnTurnTimerUpdate);
			GameState.Get().UnregisterGameOverListener(OnGameOver);
		}
	}

	private void Update()
	{
		UpdateCountdownText();
	}

	public static TurnTimer Get()
	{
		return s_instance;
	}

	public bool HasCountdownTimeout()
	{
		return m_countdownTimeoutSec > Mathf.Epsilon;
	}

	public void OnEndTurnRequested()
	{
		if (HasCountdownTimeout())
		{
			ChangeState(TurnTimerState.KILL);
		}
	}

	public bool IsRopeActive()
	{
		return m_state == TurnTimerState.COUNTDOWN;
	}

	public void SetGameModeSettings(TurnTimerGameModeSettings settings)
	{
		m_gameModeSettings = settings;
		PlayMakerFSM component = GetComponent<PlayMakerFSM>();
		if (component == null)
		{
			Debug.LogError("No playmaker attached to TurnTimer!");
			return;
		}
		component.FsmVariables.GetFsmBool("PlayTimeoutFx").Value = settings.m_PlayTimeoutFx;
		component.FsmVariables.GetFsmBool("PlayMusicStinger").Value = settings.m_PlayMusicStinger;
		component.FsmVariables.GetFsmFloat("RopeFuseVolume").Value = settings.m_RopeFuseVolume;
		component.FsmVariables.GetFsmFloat("RopeRolloutVolume").Value = settings.m_RopeRolloutVolume;
		component.FsmVariables.GetFsmFloat("EndTurnButtonExplosionVolume").Value = settings.m_EndTurnButtonExplosionVolume;
	}

	private void ChangeState(TurnTimerState state)
	{
		ChangeSpellState(state);
	}

	private void ChangeStateImpl(TurnTimerState state)
	{
		switch (state)
		{
		case TurnTimerState.START:
			ChangeState_Start();
			break;
		case TurnTimerState.COUNTDOWN:
			ChangeState_Countdown();
			break;
		case TurnTimerState.TIMEOUT:
			ChangeState_Timeout();
			break;
		case TurnTimerState.KILL:
			ChangeState_Kill();
			break;
		}
	}

	private void ChangeState_Start()
	{
		m_state = TurnTimerState.START;
		if (GameState.Get() != null && GameState.Get().GetCurrentPlayer() != null)
		{
			Card heroCard = GameState.Get().GetCurrentPlayer().GetHeroCard();
			if (heroCard != null)
			{
				heroCard.PlayEmote(EmoteType.TIME);
			}
			m_currentTimerBelongsToFriendlySidePlayer = GameState.Get().IsFriendlySidePlayerTurn();
		}
	}

	private void ChangeState_Countdown()
	{
		m_state = TurnTimerState.COUNTDOWN;
		m_countdownTimeoutSec = ComputeCountdownRemainingSec();
		StartCountdownAnimsWhenBelowCap(m_countdownTimeoutSec);
	}

	private void ChangeState_Timeout()
	{
		m_state = TurnTimerState.TIMEOUT;
		m_countdownEndTimestamp = 0f;
		if (EndTurnButton.Get() != null)
		{
			EndTurnButton.Get().OnTurnTimerEnded(m_currentTimerBelongsToFriendlySidePlayer);
		}
		GameState.Get()?.GetGameEntity()?.OnTurnTimerEnded(m_currentTimerBelongsToFriendlySidePlayer);
		StopCountdownAnims();
		UpdateCountdownAnims(0f);
	}

	private void ChangeState_Kill()
	{
		m_state = TurnTimerState.KILL;
		m_countdownEndTimestamp = 0f;
		StopCountdownAnims();
		UpdateCountdownAnims(0f);
	}

	private void ChangeSpellState(TurnTimerState timerState)
	{
		SpellStateType stateType = TranslateTimerStateToSpellState(timerState);
		m_spell.ActivateState(stateType);
		if (timerState == TurnTimerState.START)
		{
			StartCoroutine(TimerBirthAnimateMaterialValues());
		}
	}

	private IEnumerator TimerBirthAnimateMaterialValues()
	{
		float endTime = Time.timeSinceLevelLoad + 1f;
		while (Time.timeSinceLevelLoad < endTime)
		{
			OnUpdateFuseMatVal(m_FuseXamountAnimation);
			yield return null;
		}
	}

	private void OnSpellStateStarted(Spell spell, SpellStateType prevStateType, object userData)
	{
		SpellStateType activeState = spell.GetActiveState();
		TurnTimerState state = TranslateSpellStateToTimerState(activeState);
		ChangeStateImpl(state);
	}

	private SpellStateType TranslateTimerStateToSpellState(TurnTimerState timerState)
	{
		return timerState switch
		{
			TurnTimerState.START => SpellStateType.BIRTH, 
			TurnTimerState.COUNTDOWN => SpellStateType.IDLE, 
			TurnTimerState.TIMEOUT => SpellStateType.DEATH, 
			TurnTimerState.KILL => SpellStateType.CANCEL, 
			_ => SpellStateType.NONE, 
		};
	}

	private TurnTimerState TranslateSpellStateToTimerState(SpellStateType spellState)
	{
		return spellState switch
		{
			SpellStateType.BIRTH => TurnTimerState.START, 
			SpellStateType.IDLE => TurnTimerState.COUNTDOWN, 
			SpellStateType.DEATH => TurnTimerState.TIMEOUT, 
			SpellStateType.CANCEL => TurnTimerState.KILL, 
			_ => TurnTimerState.NONE, 
		};
	}

	private bool ShouldUpdateCountdownRemaining()
	{
		if (m_state == TurnTimerState.COUNTDOWN)
		{
			return true;
		}
		return false;
	}

	private void StopCountdownAnims()
	{
		iTween.StopByName(m_SparksObject, GenerateMoveAnimName());
		iTween.StopByName(m_FuseWickObject, GenerateMatValAnimName());
	}

	private float UpdateCountdownAnims(float countdownRemainingSec)
	{
		float t = ComputeCountdownProgress(countdownRemainingSec);
		m_SparksObject.transform.position = Vector3.Lerp(m_SparksFinishBone.position, m_SparksStartBone.position, t);
		float num = Mathf.Lerp(m_FuseMatValFinish, m_FuseMatValStart, t);
		m_FuseWickObject.GetComponent<Renderer>().GetMaterial().SetFloat(m_FuseMatValName, num);
		m_FuseShadowObject.GetComponent<Renderer>().GetMaterial().SetFloat(m_FuseMatValName, num);
		return num;
	}

	private void StartCountdownAnimsWhenBelowCap(float countdownRemainingSec)
	{
		if (m_countdownAnimsWhenBelowCapCoroutine != null)
		{
			StopCoroutine(m_countdownAnimsWhenBelowCapCoroutine);
		}
		m_countdownAnimsWhenBelowCapCoroutine = StartCoroutine(StartCountdownAnimsWhenBelowCapCoroutine(countdownRemainingSec));
	}

	private IEnumerator StartCountdownAnimsWhenBelowCapCoroutine(float countdownRemainingSec)
	{
		float secondsRemaining = countdownRemainingSec;
		if (countdownRemainingSec > m_RopeCapSeconds)
		{
			yield return new WaitForSecondsRealtime(countdownRemainingSec - m_RopeCapSeconds);
			secondsRemaining = m_RopeCapSeconds;
		}
		HandleTurnTimerUpdateAnims(secondsRemaining);
		m_countdownAnimsWhenBelowCapCoroutine = null;
	}

	private void StartCountdownAnims(float startingMatVal, float countdownRemainingSec)
	{
		m_lastTickSecondNumber = Mathf.CeilToInt(m_RopeCapSeconds);
		m_currentMoveAnimId++;
		m_currentMatValAnimId++;
		Hashtable args = iTween.Hash("name", GenerateMoveAnimName(), "time", countdownRemainingSec, "position", m_SparksFinishBone.position, "ignoretimescale", true, "easetype", iTween.EaseType.linear);
		iTween.MoveTo(m_SparksObject, args);
		Hashtable args2 = iTween.Hash("name", GenerateMatValAnimName(), "time", countdownRemainingSec, "from", startingMatVal, "to", m_FuseMatValFinish, "ignoretimescale", true, "easetype", iTween.EaseType.linear, "onupdate", "OnUpdateFuseMatVal", "onupdatetarget", base.gameObject);
		iTween.ValueTo(m_FuseWickObject, args2);
	}

	private string GenerateMoveAnimName()
	{
		return $"SparksMove{m_currentMoveAnimId}";
	}

	private string GenerateMatValAnimName()
	{
		return $"FuseMatVal{m_currentMatValAnimId}";
	}

	private void OnUpdateFuseMatVal(float val)
	{
		m_FuseWickObject.GetComponent<Renderer>().GetMaterial().SetFloat(m_FuseMatValName, val);
		m_FuseShadowObject.GetComponent<Renderer>().GetMaterial().SetFloat(m_FuseMatValName, val);
	}

	private void RestartCountdownAnims(float countdownRemainingSec)
	{
		StopCountdownAnims();
		float startingMatVal = UpdateCountdownAnims(countdownRemainingSec);
		StartCountdownAnims(startingMatVal, countdownRemainingSec);
	}

	private void UpdateCountdownTimeout()
	{
		m_countdownTimeoutSec = 0f;
		if (GameState.Get() != null)
		{
			Player currentPlayer = GameState.Get().GetCurrentPlayer();
			if (currentPlayer != null && currentPlayer.HasTag(GAME_TAG.TIMEOUT))
			{
				int num = currentPlayer.GetTag(GAME_TAG.TIMEOUT);
				m_countdownTimeoutSec = num;
			}
		}
	}

	private float ComputeCountdownRemainingSec()
	{
		float num = m_countdownEndTimestamp - Time.realtimeSinceStartup;
		if (num < 0f)
		{
			return 0f;
		}
		return num;
	}

	private float ComputeCountdownProgress(float countdownRemainingSec)
	{
		if (countdownRemainingSec <= Mathf.Epsilon)
		{
			return 0f;
		}
		return countdownRemainingSec / m_countdownTimeoutSec;
	}

	private void OnCurrentPlayerChanged(Player player, object userData)
	{
		if (m_state == TurnTimerState.COUNTDOWN || m_state == TurnTimerState.START)
		{
			ChangeState(TurnTimerState.KILL);
		}
		UpdateCountdownTimeout();
	}

	private void OnFriendlyTurnStarted(object userData)
	{
		if (HasCountdownTimeout() || m_waitingForTurnStartManagerFinish)
		{
			if (m_waitingForTurnStartManagerFinish)
			{
				ChangeState(TurnTimerState.START);
			}
			m_waitingForTurnStartManagerFinish = false;
		}
	}

	private void OnTurnTimerUpdate(TurnTimerUpdate update, object userData)
	{
		m_countdownEndTimestamp = update.GetEndTimestamp();
		if (!update.ShouldShow())
		{
			if (m_state == TurnTimerState.COUNTDOWN || m_state == TurnTimerState.START)
			{
				ChangeState(TurnTimerState.KILL);
			}
			return;
		}
		float secondsRemaining = update.GetSecondsRemaining();
		if (secondsRemaining <= Mathf.Epsilon)
		{
			OnTurnTimedOut();
		}
		else if (secondsRemaining > m_RopeCapSeconds)
		{
			StartCountdownAnimsWhenBelowCap(secondsRemaining);
		}
		else
		{
			HandleTurnTimerUpdateAnims(secondsRemaining);
		}
	}

	private void HandleTurnTimerUpdateAnims(float secondsRemaining)
	{
		if (GameState.Get() != null && GameState.Get().IsGameOverNowOrPending())
		{
			return;
		}
		if (m_state == TurnTimerState.COUNTDOWN)
		{
			RestartCountdownAnims(secondsRemaining);
		}
		else if (ComputeCountdownRemainingSec() != 0f)
		{
			if (GameState.Get().IsTurnStartManagerActive())
			{
				m_waitingForTurnStartManagerFinish = true;
			}
			else
			{
				StartCoroutine(EnterStartStateWhenReady());
			}
		}
	}

	private IEnumerator EnterStartStateWhenReady()
	{
		while (GameState.Get() == null || GameState.Get().GetCurrentPlayer() == null)
		{
			yield return null;
		}
		ChangeState(TurnTimerState.START);
	}

	private void OnTurnTimedOut()
	{
		if (HasCountdownTimeout())
		{
			ChangeState(TurnTimerState.TIMEOUT);
		}
	}

	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		if (m_state == TurnTimerState.COUNTDOWN || m_state == TurnTimerState.START)
		{
			ChangeState(TurnTimerState.KILL);
		}
	}

	private void UpdateCountdownText()
	{
		if (GameState.Get() == null || GameState.Get().GetGameEntity() == null || GameState.Get().IsGameOver())
		{
			return;
		}
		float num = ComputeCountdownRemainingSec();
		m_CountdownText.Text = GameState.Get().GetGameEntity().GetTurnTimerCountdownText(num);
		m_CountdownText.TextColor = ((num > 0f && num < m_RopeCapSeconds) ? m_CountdownTextColorRope : m_CountdownTextColorNormal);
		if (m_gameModeSettings.m_PlayTickSound)
		{
			int num2 = Mathf.CeilToInt(num);
			if (m_lastTickSecondNumber > num2)
			{
				m_lastTickSecondNumber = num2;
				SoundManager.Get().Play((num2 == 0) ? m_FinalTickSound.GetComponent<AudioSource>() : m_TickSound.GetComponent<AudioSource>());
			}
		}
	}
}
