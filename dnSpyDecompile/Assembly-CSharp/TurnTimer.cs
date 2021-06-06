using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000351 RID: 849
public class TurnTimer : MonoBehaviour
{
	// Token: 0x06003143 RID: 12611 RVA: 0x000FD390 File Offset: 0x000FB590
	private void Awake()
	{
		TurnTimer.s_instance = this;
		this.m_spell = base.GetComponent<Spell>();
		this.m_spell.AddStateStartedCallback(new Spell.StateStartedCallback(this.OnSpellStateStarted));
		if (GameState.Get() != null)
		{
			GameState.Get().RegisterCurrentPlayerChangedListener(new GameState.CurrentPlayerChangedCallback(this.OnCurrentPlayerChanged));
			GameState.Get().RegisterFriendlyTurnStartedListener(new GameState.FriendlyTurnStartedCallback(this.OnFriendlyTurnStarted), null);
			GameState.Get().RegisterTurnTimerUpdateListener(new GameState.TurnTimerUpdateCallback(this.OnTurnTimerUpdate));
			GameState.Get().RegisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
		}
		this.SetGameModeSettings(new TurnTimerGameModeSettings());
	}

	// Token: 0x06003144 RID: 12612 RVA: 0x000FD438 File Offset: 0x000FB638
	private void OnDestroy()
	{
		TurnTimer.s_instance = null;
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterCurrentPlayerChangedListener(new GameState.CurrentPlayerChangedCallback(this.OnCurrentPlayerChanged));
			GameState.Get().UnregisterFriendlyTurnStartedListener(new GameState.FriendlyTurnStartedCallback(this.OnFriendlyTurnStarted), null);
			GameState.Get().UnregisterTurnTimerUpdateListener(new GameState.TurnTimerUpdateCallback(this.OnTurnTimerUpdate));
			GameState.Get().UnregisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
		}
	}

	// Token: 0x06003145 RID: 12613 RVA: 0x000FD4B0 File Offset: 0x000FB6B0
	private void Update()
	{
		this.UpdateCountdownText();
	}

	// Token: 0x06003146 RID: 12614 RVA: 0x000FD4B8 File Offset: 0x000FB6B8
	public static TurnTimer Get()
	{
		return TurnTimer.s_instance;
	}

	// Token: 0x06003147 RID: 12615 RVA: 0x000FD4BF File Offset: 0x000FB6BF
	public bool HasCountdownTimeout()
	{
		return this.m_countdownTimeoutSec > Mathf.Epsilon;
	}

	// Token: 0x06003148 RID: 12616 RVA: 0x000FD4CE File Offset: 0x000FB6CE
	public void OnEndTurnRequested()
	{
		if (!this.HasCountdownTimeout())
		{
			return;
		}
		this.ChangeState(TurnTimerState.KILL);
	}

	// Token: 0x06003149 RID: 12617 RVA: 0x000FD4E0 File Offset: 0x000FB6E0
	public bool IsRopeActive()
	{
		return this.m_state == TurnTimerState.COUNTDOWN;
	}

	// Token: 0x0600314A RID: 12618 RVA: 0x000FD4EC File Offset: 0x000FB6EC
	public void SetGameModeSettings(TurnTimerGameModeSettings settings)
	{
		this.m_gameModeSettings = settings;
		PlayMakerFSM component = base.GetComponent<PlayMakerFSM>();
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

	// Token: 0x0600314B RID: 12619 RVA: 0x000FD5A2 File Offset: 0x000FB7A2
	private void ChangeState(TurnTimerState state)
	{
		this.ChangeSpellState(state);
	}

	// Token: 0x0600314C RID: 12620 RVA: 0x000FD5AB File Offset: 0x000FB7AB
	private void ChangeStateImpl(TurnTimerState state)
	{
		if (state == TurnTimerState.START)
		{
			this.ChangeState_Start();
			return;
		}
		if (state == TurnTimerState.COUNTDOWN)
		{
			this.ChangeState_Countdown();
			return;
		}
		if (state == TurnTimerState.TIMEOUT)
		{
			this.ChangeState_Timeout();
			return;
		}
		if (state == TurnTimerState.KILL)
		{
			this.ChangeState_Kill();
		}
	}

	// Token: 0x0600314D RID: 12621 RVA: 0x000FD5D8 File Offset: 0x000FB7D8
	private void ChangeState_Start()
	{
		this.m_state = TurnTimerState.START;
		if (GameState.Get() != null && GameState.Get().GetCurrentPlayer() != null)
		{
			Card heroCard = GameState.Get().GetCurrentPlayer().GetHeroCard();
			if (heroCard != null)
			{
				heroCard.PlayEmote(EmoteType.TIME);
			}
			this.m_currentTimerBelongsToFriendlySidePlayer = GameState.Get().IsFriendlySidePlayerTurn();
		}
	}

	// Token: 0x0600314E RID: 12622 RVA: 0x000FD631 File Offset: 0x000FB831
	private void ChangeState_Countdown()
	{
		this.m_state = TurnTimerState.COUNTDOWN;
		this.m_countdownTimeoutSec = this.ComputeCountdownRemainingSec();
		this.StartCountdownAnimsWhenBelowCap(this.m_countdownTimeoutSec);
	}

	// Token: 0x0600314F RID: 12623 RVA: 0x000FD654 File Offset: 0x000FB854
	private void ChangeState_Timeout()
	{
		this.m_state = TurnTimerState.TIMEOUT;
		this.m_countdownEndTimestamp = 0f;
		if (EndTurnButton.Get() != null)
		{
			EndTurnButton.Get().OnTurnTimerEnded(this.m_currentTimerBelongsToFriendlySidePlayer);
		}
		GameState gameState = GameState.Get();
		if (gameState != null)
		{
			GameEntity gameEntity = gameState.GetGameEntity();
			if (gameEntity != null)
			{
				gameEntity.OnTurnTimerEnded(this.m_currentTimerBelongsToFriendlySidePlayer);
			}
		}
		this.StopCountdownAnims();
		this.UpdateCountdownAnims(0f);
	}

	// Token: 0x06003150 RID: 12624 RVA: 0x000FD6C1 File Offset: 0x000FB8C1
	private void ChangeState_Kill()
	{
		this.m_state = TurnTimerState.KILL;
		this.m_countdownEndTimestamp = 0f;
		this.StopCountdownAnims();
		this.UpdateCountdownAnims(0f);
	}

	// Token: 0x06003151 RID: 12625 RVA: 0x000FD6E8 File Offset: 0x000FB8E8
	private void ChangeSpellState(TurnTimerState timerState)
	{
		SpellStateType stateType = this.TranslateTimerStateToSpellState(timerState);
		this.m_spell.ActivateState(stateType);
		if (timerState == TurnTimerState.START)
		{
			base.StartCoroutine(this.TimerBirthAnimateMaterialValues());
		}
	}

	// Token: 0x06003152 RID: 12626 RVA: 0x000FD71A File Offset: 0x000FB91A
	private IEnumerator TimerBirthAnimateMaterialValues()
	{
		float endTime = Time.timeSinceLevelLoad + 1f;
		while (Time.timeSinceLevelLoad < endTime)
		{
			this.OnUpdateFuseMatVal(this.m_FuseXamountAnimation);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06003153 RID: 12627 RVA: 0x000FD72C File Offset: 0x000FB92C
	private void OnSpellStateStarted(Spell spell, SpellStateType prevStateType, object userData)
	{
		SpellStateType activeState = spell.GetActiveState();
		TurnTimerState state = this.TranslateSpellStateToTimerState(activeState);
		this.ChangeStateImpl(state);
	}

	// Token: 0x06003154 RID: 12628 RVA: 0x000FD74F File Offset: 0x000FB94F
	private SpellStateType TranslateTimerStateToSpellState(TurnTimerState timerState)
	{
		if (timerState == TurnTimerState.START)
		{
			return SpellStateType.BIRTH;
		}
		if (timerState == TurnTimerState.COUNTDOWN)
		{
			return SpellStateType.IDLE;
		}
		if (timerState == TurnTimerState.TIMEOUT)
		{
			return SpellStateType.DEATH;
		}
		if (timerState == TurnTimerState.KILL)
		{
			return SpellStateType.CANCEL;
		}
		return SpellStateType.NONE;
	}

	// Token: 0x06003155 RID: 12629 RVA: 0x000FD76A File Offset: 0x000FB96A
	private TurnTimerState TranslateSpellStateToTimerState(SpellStateType spellState)
	{
		if (spellState == SpellStateType.BIRTH)
		{
			return TurnTimerState.START;
		}
		if (spellState == SpellStateType.IDLE)
		{
			return TurnTimerState.COUNTDOWN;
		}
		if (spellState == SpellStateType.DEATH)
		{
			return TurnTimerState.TIMEOUT;
		}
		if (spellState == SpellStateType.CANCEL)
		{
			return TurnTimerState.KILL;
		}
		return TurnTimerState.NONE;
	}

	// Token: 0x06003156 RID: 12630 RVA: 0x000FD785 File Offset: 0x000FB985
	private bool ShouldUpdateCountdownRemaining()
	{
		return this.m_state == TurnTimerState.COUNTDOWN;
	}

	// Token: 0x06003157 RID: 12631 RVA: 0x000FD793 File Offset: 0x000FB993
	private void StopCountdownAnims()
	{
		iTween.StopByName(this.m_SparksObject, this.GenerateMoveAnimName());
		iTween.StopByName(this.m_FuseWickObject, this.GenerateMatValAnimName());
	}

	// Token: 0x06003158 RID: 12632 RVA: 0x000FD7B8 File Offset: 0x000FB9B8
	private float UpdateCountdownAnims(float countdownRemainingSec)
	{
		float t = this.ComputeCountdownProgress(countdownRemainingSec);
		this.m_SparksObject.transform.position = Vector3.Lerp(this.m_SparksFinishBone.position, this.m_SparksStartBone.position, t);
		float num = Mathf.Lerp(this.m_FuseMatValFinish, this.m_FuseMatValStart, t);
		this.m_FuseWickObject.GetComponent<Renderer>().GetMaterial().SetFloat(this.m_FuseMatValName, num);
		this.m_FuseShadowObject.GetComponent<Renderer>().GetMaterial().SetFloat(this.m_FuseMatValName, num);
		return num;
	}

	// Token: 0x06003159 RID: 12633 RVA: 0x000FD845 File Offset: 0x000FBA45
	private void StartCountdownAnimsWhenBelowCap(float countdownRemainingSec)
	{
		if (this.m_countdownAnimsWhenBelowCapCoroutine != null)
		{
			base.StopCoroutine(this.m_countdownAnimsWhenBelowCapCoroutine);
		}
		this.m_countdownAnimsWhenBelowCapCoroutine = base.StartCoroutine(this.StartCountdownAnimsWhenBelowCapCoroutine(countdownRemainingSec));
	}

	// Token: 0x0600315A RID: 12634 RVA: 0x000FD86E File Offset: 0x000FBA6E
	private IEnumerator StartCountdownAnimsWhenBelowCapCoroutine(float countdownRemainingSec)
	{
		float secondsRemaining = countdownRemainingSec;
		if (countdownRemainingSec > this.m_RopeCapSeconds)
		{
			yield return new WaitForSecondsRealtime(countdownRemainingSec - this.m_RopeCapSeconds);
			secondsRemaining = this.m_RopeCapSeconds;
		}
		this.HandleTurnTimerUpdateAnims(secondsRemaining);
		this.m_countdownAnimsWhenBelowCapCoroutine = null;
		yield break;
	}

	// Token: 0x0600315B RID: 12635 RVA: 0x000FD884 File Offset: 0x000FBA84
	private void StartCountdownAnims(float startingMatVal, float countdownRemainingSec)
	{
		this.m_lastTickSecondNumber = Mathf.CeilToInt(this.m_RopeCapSeconds);
		this.m_currentMoveAnimId += 1U;
		this.m_currentMatValAnimId += 1U;
		Hashtable args = iTween.Hash(new object[]
		{
			"name",
			this.GenerateMoveAnimName(),
			"time",
			countdownRemainingSec,
			"position",
			this.m_SparksFinishBone.position,
			"ignoretimescale",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.MoveTo(this.m_SparksObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"name",
			this.GenerateMatValAnimName(),
			"time",
			countdownRemainingSec,
			"from",
			startingMatVal,
			"to",
			this.m_FuseMatValFinish,
			"ignoretimescale",
			true,
			"easetype",
			iTween.EaseType.linear,
			"onupdate",
			"OnUpdateFuseMatVal",
			"onupdatetarget",
			base.gameObject
		});
		iTween.ValueTo(this.m_FuseWickObject, args2);
	}

	// Token: 0x0600315C RID: 12636 RVA: 0x000FD9E5 File Offset: 0x000FBBE5
	private string GenerateMoveAnimName()
	{
		return string.Format("SparksMove{0}", this.m_currentMoveAnimId);
	}

	// Token: 0x0600315D RID: 12637 RVA: 0x000FD9FC File Offset: 0x000FBBFC
	private string GenerateMatValAnimName()
	{
		return string.Format("FuseMatVal{0}", this.m_currentMatValAnimId);
	}

	// Token: 0x0600315E RID: 12638 RVA: 0x000FDA13 File Offset: 0x000FBC13
	private void OnUpdateFuseMatVal(float val)
	{
		this.m_FuseWickObject.GetComponent<Renderer>().GetMaterial().SetFloat(this.m_FuseMatValName, val);
		this.m_FuseShadowObject.GetComponent<Renderer>().GetMaterial().SetFloat(this.m_FuseMatValName, val);
	}

	// Token: 0x0600315F RID: 12639 RVA: 0x000FDA50 File Offset: 0x000FBC50
	private void RestartCountdownAnims(float countdownRemainingSec)
	{
		this.StopCountdownAnims();
		float startingMatVal = this.UpdateCountdownAnims(countdownRemainingSec);
		this.StartCountdownAnims(startingMatVal, countdownRemainingSec);
	}

	// Token: 0x06003160 RID: 12640 RVA: 0x000FDA74 File Offset: 0x000FBC74
	private void UpdateCountdownTimeout()
	{
		this.m_countdownTimeoutSec = 0f;
		if (GameState.Get() == null)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (currentPlayer == null)
		{
			return;
		}
		if (!currentPlayer.HasTag(GAME_TAG.TIMEOUT))
		{
			return;
		}
		int tag = currentPlayer.GetTag(GAME_TAG.TIMEOUT);
		this.m_countdownTimeoutSec = (float)tag;
	}

	// Token: 0x06003161 RID: 12641 RVA: 0x000FDAC0 File Offset: 0x000FBCC0
	private float ComputeCountdownRemainingSec()
	{
		float num = this.m_countdownEndTimestamp - Time.realtimeSinceStartup;
		if (num < 0f)
		{
			return 0f;
		}
		return num;
	}

	// Token: 0x06003162 RID: 12642 RVA: 0x000FDAE9 File Offset: 0x000FBCE9
	private float ComputeCountdownProgress(float countdownRemainingSec)
	{
		if (countdownRemainingSec <= Mathf.Epsilon)
		{
			return 0f;
		}
		return countdownRemainingSec / this.m_countdownTimeoutSec;
	}

	// Token: 0x06003163 RID: 12643 RVA: 0x000FDB01 File Offset: 0x000FBD01
	private void OnCurrentPlayerChanged(Player player, object userData)
	{
		if (this.m_state == TurnTimerState.COUNTDOWN || this.m_state == TurnTimerState.START)
		{
			this.ChangeState(TurnTimerState.KILL);
		}
		this.UpdateCountdownTimeout();
	}

	// Token: 0x06003164 RID: 12644 RVA: 0x000FDB22 File Offset: 0x000FBD22
	private void OnFriendlyTurnStarted(object userData)
	{
		if (!this.HasCountdownTimeout() && !this.m_waitingForTurnStartManagerFinish)
		{
			return;
		}
		if (this.m_waitingForTurnStartManagerFinish)
		{
			this.ChangeState(TurnTimerState.START);
		}
		this.m_waitingForTurnStartManagerFinish = false;
	}

	// Token: 0x06003165 RID: 12645 RVA: 0x000FDB4C File Offset: 0x000FBD4C
	private void OnTurnTimerUpdate(TurnTimerUpdate update, object userData)
	{
		this.m_countdownEndTimestamp = update.GetEndTimestamp();
		if (!update.ShouldShow())
		{
			if (this.m_state == TurnTimerState.COUNTDOWN || this.m_state == TurnTimerState.START)
			{
				this.ChangeState(TurnTimerState.KILL);
			}
			return;
		}
		float secondsRemaining = update.GetSecondsRemaining();
		if (secondsRemaining <= Mathf.Epsilon)
		{
			this.OnTurnTimedOut();
			return;
		}
		if (secondsRemaining > this.m_RopeCapSeconds)
		{
			this.StartCountdownAnimsWhenBelowCap(secondsRemaining);
			return;
		}
		this.HandleTurnTimerUpdateAnims(secondsRemaining);
	}

	// Token: 0x06003166 RID: 12646 RVA: 0x000FDBB8 File Offset: 0x000FBDB8
	private void HandleTurnTimerUpdateAnims(float secondsRemaining)
	{
		if (GameState.Get() != null && GameState.Get().IsGameOverNowOrPending())
		{
			return;
		}
		if (this.m_state == TurnTimerState.COUNTDOWN)
		{
			this.RestartCountdownAnims(secondsRemaining);
			return;
		}
		if (this.ComputeCountdownRemainingSec() == 0f)
		{
			return;
		}
		if (GameState.Get().IsTurnStartManagerActive())
		{
			this.m_waitingForTurnStartManagerFinish = true;
			return;
		}
		base.StartCoroutine(this.EnterStartStateWhenReady());
	}

	// Token: 0x06003167 RID: 12647 RVA: 0x000FDC19 File Offset: 0x000FBE19
	private IEnumerator EnterStartStateWhenReady()
	{
		while (GameState.Get() == null || GameState.Get().GetCurrentPlayer() == null)
		{
			yield return null;
		}
		this.ChangeState(TurnTimerState.START);
		yield break;
	}

	// Token: 0x06003168 RID: 12648 RVA: 0x000FDC28 File Offset: 0x000FBE28
	private void OnTurnTimedOut()
	{
		if (!this.HasCountdownTimeout())
		{
			return;
		}
		this.ChangeState(TurnTimerState.TIMEOUT);
	}

	// Token: 0x06003169 RID: 12649 RVA: 0x000FDC3A File Offset: 0x000FBE3A
	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		if (this.m_state == TurnTimerState.COUNTDOWN || this.m_state == TurnTimerState.START)
		{
			this.ChangeState(TurnTimerState.KILL);
		}
	}

	// Token: 0x0600316A RID: 12650 RVA: 0x000FDC58 File Offset: 0x000FBE58
	private void UpdateCountdownText()
	{
		if (GameState.Get() == null || GameState.Get().GetGameEntity() == null || GameState.Get().IsGameOver())
		{
			return;
		}
		float num = this.ComputeCountdownRemainingSec();
		this.m_CountdownText.Text = GameState.Get().GetGameEntity().GetTurnTimerCountdownText(num);
		this.m_CountdownText.TextColor = ((num > 0f && num < this.m_RopeCapSeconds) ? this.m_CountdownTextColorRope : this.m_CountdownTextColorNormal);
		if (this.m_gameModeSettings.m_PlayTickSound)
		{
			int num2 = Mathf.CeilToInt(num);
			if (this.m_lastTickSecondNumber > num2)
			{
				this.m_lastTickSecondNumber = num2;
				SoundManager.Get().Play((num2 == 0) ? this.m_FinalTickSound.GetComponent<AudioSource>() : this.m_TickSound.GetComponent<AudioSource>(), null, null, null);
			}
		}
	}

	// Token: 0x04001B6A RID: 7018
	public float m_DebugTimeout = 30f;

	// Token: 0x04001B6B RID: 7019
	public float m_DebugTimeoutStart = 20f;

	// Token: 0x04001B6C RID: 7020
	public float m_RopeCapSeconds = 20f;

	// Token: 0x04001B6D RID: 7021
	public GameObject m_SparksObject;

	// Token: 0x04001B6E RID: 7022
	public Transform m_SparksStartBone;

	// Token: 0x04001B6F RID: 7023
	public Transform m_SparksFinishBone;

	// Token: 0x04001B70 RID: 7024
	public UberText m_CountdownText;

	// Token: 0x04001B71 RID: 7025
	public Color m_CountdownTextColorNormal;

	// Token: 0x04001B72 RID: 7026
	public Color m_CountdownTextColorRope;

	// Token: 0x04001B73 RID: 7027
	public GameObject m_FuseWickObject;

	// Token: 0x04001B74 RID: 7028
	public GameObject m_FuseShadowObject;

	// Token: 0x04001B75 RID: 7029
	public string m_FuseMatValName = "_Xamount";

	// Token: 0x04001B76 RID: 7030
	public float m_FuseMatValStart = 0.42f;

	// Token: 0x04001B77 RID: 7031
	public float m_FuseMatValFinish = -1.5f;

	// Token: 0x04001B78 RID: 7032
	public float m_FuseXamountAnimation = -1.5f;

	// Token: 0x04001B79 RID: 7033
	public SoundDef m_TickSound;

	// Token: 0x04001B7A RID: 7034
	public SoundDef m_FinalTickSound;

	// Token: 0x04001B7B RID: 7035
	private const float BIRTH_ANIMATION_TIME = 1f;

	// Token: 0x04001B7C RID: 7036
	private static TurnTimer s_instance;

	// Token: 0x04001B7D RID: 7037
	private Spell m_spell;

	// Token: 0x04001B7E RID: 7038
	private TurnTimerState m_state;

	// Token: 0x04001B7F RID: 7039
	private float m_countdownTimeoutSec;

	// Token: 0x04001B80 RID: 7040
	private float m_countdownEndTimestamp;

	// Token: 0x04001B81 RID: 7041
	private uint m_currentMoveAnimId;

	// Token: 0x04001B82 RID: 7042
	private uint m_currentMatValAnimId;

	// Token: 0x04001B83 RID: 7043
	private bool m_currentTimerBelongsToFriendlySidePlayer;

	// Token: 0x04001B84 RID: 7044
	private bool m_waitingForTurnStartManagerFinish;

	// Token: 0x04001B85 RID: 7045
	private int m_lastTickSecondNumber;

	// Token: 0x04001B86 RID: 7046
	private Coroutine m_countdownAnimsWhenBelowCapCoroutine;

	// Token: 0x04001B87 RID: 7047
	private TurnTimerGameModeSettings m_gameModeSettings;
}
