using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000305 RID: 773
public class EndTurnButton : MonoBehaviour
{
	// Token: 0x17000500 RID: 1280
	// (get) Token: 0x0600297F RID: 10623 RVA: 0x000D32BF File Offset: 0x000D14BF
	public bool IsDisabled
	{
		get
		{
			return this.m_disabled;
		}
	}

	// Token: 0x06002980 RID: 10624 RVA: 0x000D32C7 File Offset: 0x000D14C7
	private void Awake()
	{
		EndTurnButton.s_instance = this;
		this.m_MyTurnText.Text = this.GetEndTurnText();
		this.m_WaitingText.Text = "";
		base.GetComponent<Collider>().enabled = false;
	}

	// Token: 0x06002981 RID: 10625 RVA: 0x000D32FC File Offset: 0x000D14FC
	private void OnDestroy()
	{
		EndTurnButton.s_instance = null;
	}

	// Token: 0x06002982 RID: 10626 RVA: 0x000D3304 File Offset: 0x000D1504
	private void Start()
	{
		base.StartCoroutine(this.WaitAFrameAndThenChangeState());
	}

	// Token: 0x06002983 RID: 10627 RVA: 0x000D3313 File Offset: 0x000D1513
	public static EndTurnButton Get()
	{
		return EndTurnButton.s_instance;
	}

	// Token: 0x06002984 RID: 10628 RVA: 0x000D331C File Offset: 0x000D151C
	public void Reset()
	{
		bool flag = this.HasExtraTurn();
		TurnStartManager.Get().NotifyOfExtraTurn(TurnStartManager.Get().GetExtraTurnSpell(true), !flag, true);
		bool flag2 = this.OpponentHasExtraTurn();
		TurnStartManager.Get().NotifyOfExtraTurn(TurnStartManager.Get().GetExtraTurnSpell(false), !flag2, false);
		this.UpdateState();
		GameState gameState = GameState.Get();
		if (gameState.IsPastBeginPhase() && gameState.IsLocalSidePlayerTurn())
		{
			base.GetComponent<Collider>().enabled = true;
			return;
		}
		base.GetComponent<Collider>().enabled = false;
	}

	// Token: 0x06002985 RID: 10629 RVA: 0x000D33A0 File Offset: 0x000D15A0
	public GameObject GetButtonContainer()
	{
		return base.transform.Find("ButtonContainer").gameObject;
	}

	// Token: 0x06002986 RID: 10630 RVA: 0x000D33B8 File Offset: 0x000D15B8
	public void PlayPushDownAnimation()
	{
		if (this.m_inputBlocked || this.IsInWaitingState() || this.m_pressed)
		{
			return;
		}
		this.m_pressed = true;
		this.GetButtonContainer().GetComponent<Animation>().Play("ENDTURN_PRESSED_DOWN");
		SoundManager.Get().LoadAndPlay("FX_EndTurn_Down.prefab:7f967e178760e5d409cec10ad56cc3ff");
	}

	// Token: 0x06002987 RID: 10631 RVA: 0x000D3410 File Offset: 0x000D1610
	public void PlayButtonUpAnimation()
	{
		if (this.m_inputBlocked || this.IsInWaitingState() || !this.m_pressed)
		{
			return;
		}
		this.m_pressed = false;
		this.GetButtonContainer().GetComponent<Animation>().Play("ENDTURN_PRESSED_UP");
		SoundManager.Get().LoadAndPlay("FX_EndTurn_Up.prefab:aa092f360d27b5244b030e737d720ba6");
	}

	// Token: 0x06002988 RID: 10632 RVA: 0x000D3468 File Offset: 0x000D1668
	public bool IsInWaitingState()
	{
		ActorStateType activeStateType = this.m_ActorStateMgr.GetActiveStateType();
		return activeStateType == ActorStateType.ENDTURN_WAITING || activeStateType == ActorStateType.ENDTURN_NMP_2_WAITING || activeStateType == ActorStateType.ENDTURN_WAITING_TIMER;
	}

	// Token: 0x06002989 RID: 10633 RVA: 0x000D3498 File Offset: 0x000D1698
	public bool IsInNMPState()
	{
		ActorStateType activeStateType = this.m_ActorStateMgr.GetActiveStateType();
		return activeStateType == ActorStateType.ENDTURN_NO_MORE_PLAYS || activeStateType == ActorStateType.EXTRATURN_NO_MORE_PLAYS;
	}

	// Token: 0x0600298A RID: 10634 RVA: 0x000D34C0 File Offset: 0x000D16C0
	public bool IsInYouHavePlaysState()
	{
		ActorStateType activeStateType = this.m_ActorStateMgr.GetActiveStateType();
		return activeStateType == ActorStateType.ENDTURN_YOUR_TURN || activeStateType == ActorStateType.EXTRATURN_YOUR_TURN;
	}

	// Token: 0x0600298B RID: 10635 RVA: 0x000D34E8 File Offset: 0x000D16E8
	public bool HasNoMorePlays()
	{
		Network.Options optionsPacket = GameState.Get().GetOptionsPacket();
		return optionsPacket != null && !optionsPacket.HasValidOption();
	}

	// Token: 0x0600298C RID: 10636 RVA: 0x000D350E File Offset: 0x000D170E
	public bool IsInputBlocked()
	{
		return this.m_inputBlocked || this.m_inputBlockers > 0;
	}

	// Token: 0x0600298D RID: 10637 RVA: 0x000D3523 File Offset: 0x000D1723
	public void AddInputBlocker()
	{
		this.m_inputBlockers++;
	}

	// Token: 0x0600298E RID: 10638 RVA: 0x000D3533 File Offset: 0x000D1733
	public void RemoveInputBlocker()
	{
		this.m_inputBlockers--;
	}

	// Token: 0x0600298F RID: 10639 RVA: 0x000D3543 File Offset: 0x000D1743
	public void HandleMouseOver()
	{
		this.m_mousedOver = true;
		if (this.m_inputBlocked)
		{
			return;
		}
		this.PutInMouseOverState();
	}

	// Token: 0x06002990 RID: 10640 RVA: 0x000D355B File Offset: 0x000D175B
	public void HandleMouseOut()
	{
		this.m_mousedOver = false;
		if (this.m_inputBlocked)
		{
			return;
		}
		if (this.m_pressed)
		{
			this.PlayButtonUpAnimation();
		}
		this.PutInMouseOffState();
	}

	// Token: 0x06002991 RID: 10641 RVA: 0x000D3584 File Offset: 0x000D1784
	private void PutInMouseOverState()
	{
		if (this.IsInNMPState())
		{
			this.m_WhiteHighlight.SetActive(false);
			this.m_GreenHighlight.SetActive(true);
			Hashtable args = iTween.Hash(new object[]
			{
				"from",
				this.m_GreenHighlight.GetComponent<Renderer>().GetMaterial().GetFloat("_Intensity"),
				"to",
				1.4f,
				"time",
				0.15f,
				"easetype",
				iTween.EaseType.linear,
				"onupdate",
				"OnUpdateIntensityValue",
				"onupdatetarget",
				base.gameObject,
				"name",
				"ENDTURN_INTENSITY"
			});
			iTween.StopByName(base.gameObject, "ENDTURN_INTENSITY");
			iTween.ValueTo(base.gameObject, args);
			return;
		}
		if (this.IsInYouHavePlaysState())
		{
			this.m_WhiteHighlight.SetActive(true);
			this.m_GreenHighlight.SetActive(false);
			return;
		}
		this.m_WhiteHighlight.SetActive(false);
		this.m_GreenHighlight.SetActive(false);
	}

	// Token: 0x06002992 RID: 10642 RVA: 0x000D36B4 File Offset: 0x000D18B4
	private void PutInMouseOffState()
	{
		this.m_WhiteHighlight.SetActive(false);
		if (this.IsInNMPState())
		{
			this.m_GreenHighlight.SetActive(true);
			Hashtable args = iTween.Hash(new object[]
			{
				"from",
				this.m_GreenHighlight.GetComponent<Renderer>().GetMaterial().GetFloat("_Intensity"),
				"to",
				1.1f,
				"time",
				0.15f,
				"easetype",
				iTween.EaseType.linear,
				"onupdate",
				"OnUpdateIntensityValue",
				"onupdatetarget",
				base.gameObject,
				"name",
				"ENDTURN_INTENSITY"
			});
			iTween.StopByName(base.gameObject, "ENDTURN_INTENSITY");
			iTween.ValueTo(base.gameObject, args);
			return;
		}
		this.m_GreenHighlight.SetActive(false);
	}

	// Token: 0x06002993 RID: 10643 RVA: 0x000D37B6 File Offset: 0x000D19B6
	private void OnUpdateIntensityValue(float newValue)
	{
		this.m_GreenHighlight.GetComponent<Renderer>().GetMaterial().SetFloat("_Intensity", newValue);
	}

	// Token: 0x06002994 RID: 10644 RVA: 0x000D37D3 File Offset: 0x000D19D3
	private IEnumerator WaitAFrameAndThenChangeState()
	{
		yield return null;
		if (GameState.Get() == null)
		{
			Log.Gameplay.PrintError("EndTurnButton.WaitAFrameAndThenChangeState(): Game state does not exist.", Array.Empty<object>());
			yield break;
		}
		if (GameState.Get().IsGameCreated())
		{
			this.HandleGameStart();
		}
		else
		{
			this.m_ActorStateMgr.ChangeState(ActorStateType.ENDTURN_WAITING);
			GameState.Get().RegisterCreateGameListener(new GameState.CreateGameCallback(this.OnCreateGame));
		}
		yield break;
	}

	// Token: 0x06002995 RID: 10645 RVA: 0x000D37E4 File Offset: 0x000D19E4
	private void HandleGameStart()
	{
		this.UpdateState();
		this.ApplyAlternativeAppearance();
		GameState gameState = GameState.Get();
		if (gameState.IsPastBeginPhase() && gameState.IsLocalSidePlayerTurn())
		{
			base.GetComponent<Collider>().enabled = true;
			GameState.Get().RegisterOptionsReceivedListener(new GameState.OptionsReceivedCallback(this.OnOptionsReceived));
		}
	}

	// Token: 0x06002996 RID: 10646 RVA: 0x000D3838 File Offset: 0x000D1A38
	private int GetCurrentAlternativeAppearanceIndex()
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return 0;
		}
		GameEntity gameEntity = gameState.GetGameEntity();
		if (gameEntity == null)
		{
			return 0;
		}
		return gameEntity.GetTag(GAME_TAG.END_TURN_BUTTON_ALTERNATIVE_APPEARANCE);
	}

	// Token: 0x06002997 RID: 10647 RVA: 0x000D3868 File Offset: 0x000D1A68
	public void ApplyAlternativeAppearance()
	{
		int currentAlternativeAppearanceIndex = this.GetCurrentAlternativeAppearanceIndex();
		if (currentAlternativeAppearanceIndex != 1)
		{
			return;
		}
		if (this.m_AlternativeMaterials.Count >= currentAlternativeAppearanceIndex && this.m_AlternativeMaterials[currentAlternativeAppearanceIndex - 1] != null)
		{
			this.m_EndTurnButtonMesh.GetComponent<Renderer>().SetMaterial(this.m_AlternativeMaterials[currentAlternativeAppearanceIndex - 1]);
			return;
		}
		Log.Gameplay.PrintError("EndTurnButton.ApplyAlternativeAppearance(): No material exists for appearance  {0}.", new object[]
		{
			currentAlternativeAppearanceIndex
		});
	}

	// Token: 0x06002998 RID: 10648 RVA: 0x000D38E8 File Offset: 0x000D1AE8
	private void SetButtonState(ActorStateType stateType)
	{
		if (this.m_ActorStateMgr == null)
		{
			Debug.Log("End Turn Button Actor State Manager is missing!");
			return;
		}
		if (this.m_ActorStateMgr.GetActiveStateType() == stateType)
		{
			return;
		}
		if (this.IsInputBlocked())
		{
			return;
		}
		if (this.m_disabled && stateType != ActorStateType.ENDTURN_WAITING)
		{
			return;
		}
		this.m_ActorStateMgr.ChangeState(stateType);
		if (stateType == ActorStateType.ENDTURN_YOUR_TURN || stateType == ActorStateType.ENDTURN_WAITING_TIMER)
		{
			this.m_inputBlocked = true;
			base.StartCoroutine(this.WaitUntilAnimationIsCompleteAndThenUnblockInput());
		}
	}

	// Token: 0x06002999 RID: 10649 RVA: 0x000D395F File Offset: 0x000D1B5F
	private IEnumerator WaitUntilAnimationIsCompleteAndThenUnblockInput()
	{
		yield return new WaitForSeconds(this.m_ActorStateMgr.GetMaximumAnimationTimeOfActiveStates());
		this.m_inputBlocked = false;
		if (this.HasNoMorePlays())
		{
			this.SetStateToNoMorePlays();
		}
		yield break;
	}

	// Token: 0x0600299A RID: 10650 RVA: 0x000D3970 File Offset: 0x000D1B70
	private void UpdateState()
	{
		if (GameState.Get().IsMulliganManagerActive())
		{
			return;
		}
		if (GameState.Get().IsTurnStartManagerBlockingInput())
		{
			return;
		}
		if (!GameState.Get().IsLocalSidePlayerTurn() || !GameState.Get().GetGameEntity().IsCurrentTurnRealTime())
		{
			this.UpdateButtonText();
			this.SetStateToWaiting();
			return;
		}
		if (GameState.Get().GetResponseMode() == GameState.ResponseMode.NONE)
		{
			return;
		}
		this.SetStateToYourTurn();
	}

	// Token: 0x0600299B RID: 10651 RVA: 0x000D39D4 File Offset: 0x000D1BD4
	public void DisplayExtraTurnState()
	{
		this.UpdateState();
	}

	// Token: 0x0600299C RID: 10652 RVA: 0x000D39DC File Offset: 0x000D1BDC
	private bool HasExtraTurn()
	{
		return GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.NUM_TURNS_LEFT) > 1;
	}

	// Token: 0x0600299D RID: 10653 RVA: 0x000D39F5 File Offset: 0x000D1BF5
	private bool OpponentHasExtraTurn()
	{
		return GameState.Get().GetOpposingSidePlayer().GetTag(GAME_TAG.NUM_TURNS_LEFT) > 1;
	}

	// Token: 0x0600299E RID: 10654 RVA: 0x000D3A0E File Offset: 0x000D1C0E
	private ActorStateType GetAppropriateYourTurnState()
	{
		if (!this.HasExtraTurn())
		{
			return ActorStateType.ENDTURN_YOUR_TURN;
		}
		if (this.IsInWaitingState())
		{
			return ActorStateType.WAITING_TO_EXTRATURN;
		}
		return ActorStateType.EXTRATURN_YOUR_TURN;
	}

	// Token: 0x0600299F RID: 10655 RVA: 0x000D3A28 File Offset: 0x000D1C28
	private ActorStateType GetAppropriateYourTurnNMPState()
	{
		if (this.HasExtraTurn())
		{
			return ActorStateType.EXTRATURN_NO_MORE_PLAYS;
		}
		return ActorStateType.ENDTURN_NO_MORE_PLAYS;
	}

	// Token: 0x060029A0 RID: 10656 RVA: 0x000D3A38 File Offset: 0x000D1C38
	private string GetEndTurnText()
	{
		switch (this.GetCurrentAlternativeAppearanceIndex())
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

	// Token: 0x060029A1 RID: 10657 RVA: 0x000D3A80 File Offset: 0x000D1C80
	private string GetEnemyTurnText()
	{
		int currentAlternativeAppearanceIndex = this.GetCurrentAlternativeAppearanceIndex();
		if (currentAlternativeAppearanceIndex - 1 <= 2)
		{
			return "";
		}
		return GameStrings.Get("GAMEPLAY_ENEMY_TURN");
	}

	// Token: 0x060029A2 RID: 10658 RVA: 0x000D3AAC File Offset: 0x000D1CAC
	private void UpdateButtonText()
	{
		switch (this.GetCurrentAlternativeAppearanceIndex())
		{
		case 1:
			this.m_MyTurnText.SetGameStringText("");
			this.m_WaitingText.SetGameStringText("");
			break;
		case 2:
			this.m_MyTurnText.SetGameStringText("GAMEPLAY_DONE_TURN");
			this.m_WaitingText.SetGameStringText("");
			break;
		case 3:
			this.m_MyTurnText.SetGameStringText("");
			this.m_WaitingText.SetGameStringText("");
			break;
		default:
			if (this.HasExtraTurn())
			{
				this.m_MyTurnText.SetGameStringText(GameStrings.Get("GAMEPLAY_NEXT_TURN"));
				this.m_WaitingText.SetGameStringText(GameStrings.Get("GAMEPLAY_NEXT_TURN"));
			}
			else
			{
				this.m_MyTurnText.SetGameStringText(GameStrings.Get("GAMEPLAY_END_TURN"));
				this.m_WaitingText.SetGameStringText(GameStrings.Get("GAMEPLAY_ENEMY_TURN"));
			}
			break;
		}
		this.m_MyTurnText.UpdateText();
		this.m_WaitingText.UpdateText();
	}

	// Token: 0x060029A3 RID: 10659 RVA: 0x000D3BB8 File Offset: 0x000D1DB8
	private void SetStateToYourTurn()
	{
		this.UpdateButtonText();
		if (this.m_ActorStateMgr == null)
		{
			return;
		}
		if (this.HasNoMorePlays())
		{
			this.SetStateToNoMorePlays();
			return;
		}
		this.SetButtonState(this.GetAppropriateYourTurnState());
		if (this.m_mousedOver)
		{
			this.PutInMouseOverState();
			return;
		}
		this.PutInMouseOffState();
	}

	// Token: 0x060029A4 RID: 10660 RVA: 0x000D3C0C File Offset: 0x000D1E0C
	private void SetStateToNoMorePlays()
	{
		if (this.m_ActorStateMgr == null)
		{
			return;
		}
		if (this.IsInWaitingState())
		{
			this.SetButtonState(this.GetAppropriateYourTurnState());
		}
		else
		{
			this.SetButtonState(this.GetAppropriateYourTurnNMPState());
			if (this.m_mousedOver)
			{
				this.PutInMouseOverState();
			}
			else
			{
				this.PutInMouseOffState();
			}
		}
		if (!this.m_playedNmpSoundThisTurn && !GameState.Get().GetGameEntity().HasTag(GAME_TAG.SUPPRESS_JOBS_DONE_VO))
		{
			this.m_playedNmpSoundThisTurn = true;
			base.StartCoroutine(this.PlayEndTurnSound());
		}
	}

	// Token: 0x060029A5 RID: 10661 RVA: 0x000D3C94 File Offset: 0x000D1E94
	private void SetStateToWaiting()
	{
		if (this.m_ActorStateMgr == null)
		{
			return;
		}
		if (this.IsInWaitingState())
		{
			return;
		}
		if (GameState.Get().IsGameOver())
		{
			return;
		}
		if (this.IsInNMPState())
		{
			this.SetButtonState(ActorStateType.ENDTURN_NMP_2_WAITING);
		}
		else
		{
			this.SetButtonState(ActorStateType.ENDTURN_WAITING);
		}
		this.PutInMouseOffState();
	}

	// Token: 0x060029A6 RID: 10662 RVA: 0x000D3CE6 File Offset: 0x000D1EE6
	private IEnumerator PlayEndTurnSound()
	{
		yield return new WaitForSeconds(1.5f);
		if (this.IsInNMPState())
		{
			SoundManager.Get().LoadAndPlay("VO_JobsDone.prefab:88cda3fac32785c4d8101966b7604cc3", base.gameObject);
		}
		yield break;
	}

	// Token: 0x060029A7 RID: 10663 RVA: 0x000D3CF5 File Offset: 0x000D1EF5
	private void OnCreateGame(GameState.CreateGamePhase phase, object userData)
	{
		if (phase != GameState.CreateGamePhase.CREATED)
		{
			return;
		}
		GameState.Get().UnregisterCreateGameListener(new GameState.CreateGameCallback(this.OnCreateGame));
		this.HandleGameStart();
	}

	// Token: 0x060029A8 RID: 10664 RVA: 0x000D3D19 File Offset: 0x000D1F19
	public void OnMulliganEnded()
	{
		this.m_WaitingText.Text = this.GetEnemyTurnText();
	}

	// Token: 0x060029A9 RID: 10665 RVA: 0x000D3D2C File Offset: 0x000D1F2C
	public void OnTurnStartManagerFinished()
	{
		if (!GameState.Get().GetGameEntity().IsCurrentTurnRealTime())
		{
			return;
		}
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		this.m_playedNmpSoundThisTurn = false;
		this.SetStateToYourTurn();
		base.GetComponent<Collider>().enabled = true;
		GameState.Get().RegisterOptionsReceivedListener(new GameState.OptionsReceivedCallback(this.OnOptionsReceived));
	}

	// Token: 0x060029AA RID: 10666 RVA: 0x000D39D4 File Offset: 0x000D1BD4
	public void OnTurnChanged()
	{
		this.UpdateState();
	}

	// Token: 0x060029AB RID: 10667 RVA: 0x000D3D86 File Offset: 0x000D1F86
	public void OnEndTurnRequested()
	{
		PegCursor.Get().SetMode(PegCursor.Mode.WAITING);
		this.SetStateToWaiting();
		base.GetComponent<Collider>().enabled = false;
		GameState.Get().UnregisterOptionsReceivedListener(new GameState.OptionsReceivedCallback(this.OnOptionsReceived));
	}

	// Token: 0x060029AC RID: 10668 RVA: 0x000D39D4 File Offset: 0x000D1BD4
	private void OnOptionsReceived(object userData)
	{
		this.UpdateState();
	}

	// Token: 0x060029AD RID: 10669 RVA: 0x000D3DBC File Offset: 0x000D1FBC
	public void OnTurnTimerStart()
	{
		if (this.m_inputBlocked)
		{
			return;
		}
		bool mousedOver = this.m_mousedOver;
	}

	// Token: 0x060029AE RID: 10670 RVA: 0x000D3DCE File Offset: 0x000D1FCE
	public void OnTurnTimerEnded(bool isFriendlyPlayerTurnTimer)
	{
		if (!isFriendlyPlayerTurnTimer)
		{
			return;
		}
		this.SetButtonState(ActorStateType.ENDTURN_WAITING_TIMER);
	}

	// Token: 0x060029AF RID: 10671 RVA: 0x000D3DDC File Offset: 0x000D1FDC
	public void SetDisabled(bool disabled)
	{
		this.m_disabled = disabled;
		if (this.m_disabled)
		{
			this.SetButtonState(ActorStateType.ENDTURN_WAITING);
		}
	}

	// Token: 0x04001796 RID: 6038
	public ActorStateMgr m_ActorStateMgr;

	// Token: 0x04001797 RID: 6039
	public UberText m_MyTurnText;

	// Token: 0x04001798 RID: 6040
	public UberText m_WaitingText;

	// Token: 0x04001799 RID: 6041
	public GameObject m_GreenHighlight;

	// Token: 0x0400179A RID: 6042
	public GameObject m_WhiteHighlight;

	// Token: 0x0400179B RID: 6043
	public GameObject m_EndTurnButtonMesh;

	// Token: 0x0400179C RID: 6044
	public List<Material> m_AlternativeMaterials;

	// Token: 0x0400179D RID: 6045
	private static EndTurnButton s_instance;

	// Token: 0x0400179E RID: 6046
	private bool m_inputBlocked;

	// Token: 0x0400179F RID: 6047
	private bool m_pressed;

	// Token: 0x040017A0 RID: 6048
	private bool m_playedNmpSoundThisTurn;

	// Token: 0x040017A1 RID: 6049
	private bool m_mousedOver;

	// Token: 0x040017A2 RID: 6050
	private bool m_disabled;

	// Token: 0x040017A3 RID: 6051
	private int m_inputBlockers;

	// Token: 0x02001641 RID: 5697
	private enum AlternativeAppearance
	{
		// Token: 0x0400B078 RID: 45176
		DEFAULT,
		// Token: 0x0400B079 RID: 45177
		RESTART_BUTTON,
		// Token: 0x0400B07A RID: 45178
		DONE_BUTTON,
		// Token: 0x0400B07B RID: 45179
		COUNTDOWN
	}
}
