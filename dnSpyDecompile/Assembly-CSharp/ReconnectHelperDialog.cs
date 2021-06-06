using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

// Token: 0x02000651 RID: 1617
public class ReconnectHelperDialog : DialogBase
{
	// Token: 0x06005B6A RID: 23402 RVA: 0x001DCDA0 File Offset: 0x001DAFA0
	private void Start()
	{
		this.PopulatePanels();
		this.CreateStateMap();
		this.m_continueButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			Action continueButtonAction = this.m_stateLayouts[this.m_state].m_continueButtonAction;
			if (continueButtonAction == null)
			{
				return;
			}
			continueButtonAction();
		});
		this.m_choiceOneButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			Action choiceButtonOneAction = this.m_stateLayouts[this.m_state].m_choiceButtonOneAction;
			if (choiceButtonOneAction == null)
			{
				return;
			}
			choiceButtonOneAction();
		});
		this.m_choiceTwoButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			Action choiceButtonTwoAction = this.m_stateLayouts[this.m_state].m_choiceButtonTwoAction;
			if (choiceButtonTwoAction == null)
			{
				return;
			}
			choiceButtonTwoAction();
		});
		this.ChangeStateToPromptBasedOnReconnectMgr();
		ReconnectMgr.Get().OnReconnectComplete += this.OnReconnectComplete;
	}

	// Token: 0x06005B6B RID: 23403 RVA: 0x001DCE20 File Offset: 0x001DB020
	private void Update()
	{
		if (this.m_state == ReconnectHelperDialog.DialogState.IN_PROGRESS)
		{
			this.UpdateWhileInProgress();
		}
	}

	// Token: 0x06005B6C RID: 23404 RVA: 0x001DCE34 File Offset: 0x001DB034
	protected override void OnDestroy()
	{
		base.OnDestroy();
		ReconnectMgr reconnectMgr = ReconnectMgr.Get();
		if (reconnectMgr != null)
		{
			reconnectMgr.OnReconnectComplete -= this.OnReconnectComplete;
		}
	}

	// Token: 0x06005B6D RID: 23405 RVA: 0x000C2782 File Offset: 0x000C0982
	public override void Show()
	{
		base.Show();
		BnetBar.Get().DisableButtonsByDialog(this);
		SoundManager.Get().LoadAndPlay("Expand_Up.prefab:775d97ea42498c044897f396362b9db3");
		this.DoShowAnimation();
		DialogBase.DoBlur();
	}

	// Token: 0x06005B6E RID: 23406 RVA: 0x001DCE62 File Offset: 0x001DB062
	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("Shrink_Down.prefab:a6d5184049ac041418cd5896e7d9a87a");
		DialogBase.EndBlur();
	}

	// Token: 0x06005B6F RID: 23407 RVA: 0x001DCE83 File Offset: 0x001DB083
	public void SetInfo(ReconnectHelperDialog.Info info)
	{
		this.m_reconnectSuccessCallback = info.m_reconnectSuccessCallback;
		this.m_goBackCallback = info.m_goBackCallback;
	}

	// Token: 0x06005B70 RID: 23408 RVA: 0x001DCEA0 File Offset: 0x001DB0A0
	private void PopulatePanels()
	{
		this.m_panels.Add(this.m_reconnectPromptPanel);
		this.m_panels.Add(this.m_reconnectInProgressPanel);
		this.m_panels.Add(this.m_reconnectFailurePanel);
		this.m_panels.Add(this.m_wifiDisabledPanel);
		this.m_panels.Add(this.m_badVersionCanResetPanel);
		this.m_panels.Add(this.m_badVersionUseLauncherPanel);
		this.m_panels.Add(this.m_inactiveTimeoutPanel);
		this.m_panels.Add(this.m_restartRequiredPanel);
	}

	// Token: 0x06005B71 RID: 23409 RVA: 0x001DCF38 File Offset: 0x001DB138
	private void CreateStateMap()
	{
		this.m_stateLayouts[ReconnectHelperDialog.DialogState.PROMPT] = new ReconnectHelperDialog.Layout
		{
			m_activePanel = this.m_reconnectPromptPanel,
			m_twoButtons = true,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CONFIRM"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_choiceButtonOneAction = new Action(this.OnReconnectButtonPressed),
			m_choiceButtonTwoAction = new Action(this.OnGoBackButtonPressed)
		};
		this.m_stateLayouts[ReconnectHelperDialog.DialogState.IN_PROGRESS] = new ReconnectHelperDialog.Layout
		{
			m_activePanel = this.m_reconnectInProgressPanel,
			m_successRingState = SpellStateType.BIRTH,
			m_continueButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_continueButtonAction = new Action(this.OnCancelButtonPressed)
		};
		this.m_stateLayouts[ReconnectHelperDialog.DialogState.FAILURE] = new ReconnectHelperDialog.Layout
		{
			m_activePanel = this.m_reconnectFailurePanel,
			m_twoButtons = true,
			m_successRingState = SpellStateType.DEATH,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CONFIRM"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_choiceButtonOneAction = new Action(this.OnReconnectButtonPressed),
			m_choiceButtonTwoAction = new Action(this.OnGoBackButtonPressed)
		};
		this.m_stateLayouts[ReconnectHelperDialog.DialogState.WIFI_DISABLED] = new ReconnectHelperDialog.Layout
		{
			m_activePanel = this.m_wifiDisabledPanel,
			m_twoButtons = true,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CONFIRM"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_choiceButtonOneAction = new Action(this.OnReconnectButtonPressed),
			m_choiceButtonTwoAction = new Action(this.OnGoBackButtonPressed)
		};
		this.m_stateLayouts[ReconnectHelperDialog.DialogState.BAD_VERSION_CAN_RESET] = new ReconnectHelperDialog.Layout
		{
			m_activePanel = this.m_badVersionCanResetPanel,
			m_twoButtons = true,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_UPDATE"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_choiceButtonOneAction = new Action(this.OnUpdateButtonPressed),
			m_choiceButtonTwoAction = new Action(this.OnGoBackButtonPressed)
		};
		this.m_stateLayouts[ReconnectHelperDialog.DialogState.BAD_VERSION_USE_LAUNCHER] = new ReconnectHelperDialog.Layout
		{
			m_activePanel = this.m_badVersionUseLauncherPanel,
			m_twoButtons = false,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_EXIT_GAME"),
			m_choiceButtonOneAction = new Action(this.OnExitGameButtonPressed),
			m_choiceButtonTwoAction = new Action(this.OnGoBackButtonPressed)
		};
		this.m_stateLayouts[ReconnectHelperDialog.DialogState.INACTIVE_TIMEOUT] = new ReconnectHelperDialog.Layout
		{
			m_activePanel = this.m_inactiveTimeoutPanel,
			m_twoButtons = true,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CONFIRM"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_choiceButtonOneAction = new Action(this.OnReconnectButtonPressed),
			m_choiceButtonTwoAction = new Action(this.OnGoBackButtonPressed)
		};
		this.m_stateLayouts[ReconnectHelperDialog.DialogState.RESTART_REQUIRED] = new ReconnectHelperDialog.Layout
		{
			m_activePanel = this.m_restartRequiredPanel,
			m_twoButtons = true,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_EXIT_GAME"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_choiceButtonOneAction = new Action(this.OnExitGameButtonPressed),
			m_choiceButtonTwoAction = new Action(this.OnGoBackButtonPressed)
		};
	}

	// Token: 0x06005B72 RID: 23410 RVA: 0x001DD25A File Offset: 0x001DB45A
	private void ChangeState(ReconnectHelperDialog.DialogState state)
	{
		if (state == this.m_state || this == null || base.gameObject == null)
		{
			return;
		}
		this.m_state = state;
		this.LoadState();
	}

	// Token: 0x06005B73 RID: 23411 RVA: 0x001DD28C File Offset: 0x001DB48C
	private void LoadState()
	{
		ReconnectHelperDialog.Layout layout = this.m_stateLayouts[this.m_state];
		this.m_continueButton.SetText(layout.m_continueButtonText);
		this.m_choiceOneButton.SetText(layout.m_choiceOneButtonText);
		this.m_choiceTwoButton.SetText(layout.m_choiceTwoButtonText);
		this.m_continueButtonContainer.SetActive(!layout.m_twoButtons);
		this.m_choiceButtonContainer.SetActive(layout.m_twoButtons);
		this.m_successRingContainer.SetActive(layout.m_successRingState > SpellStateType.NONE);
		if (layout.m_successRingState != SpellStateType.NONE)
		{
			this.m_successRingSpell.ActivateState(layout.m_successRingState);
		}
		for (int i = 0; i < this.m_panels.Count; i++)
		{
			GameObject gameObject = this.m_panels[i];
			gameObject.SetActive(gameObject == layout.m_activePanel);
		}
		if (layout.m_onInit != null)
		{
			layout.m_onInit();
		}
	}

	// Token: 0x06005B74 RID: 23412 RVA: 0x001DD376 File Offset: 0x001DB576
	private void ChangeStateToPromptBasedOnReconnectMgr()
	{
		if (ReconnectMgr.Get().FullResetRequired)
		{
			this.ChangeState_FullResetRequired();
			return;
		}
		if (InactivePlayerKicker.Get().WasKickedForInactivity)
		{
			this.ChangeState(ReconnectHelperDialog.DialogState.INACTIVE_TIMEOUT);
			return;
		}
		this.ChangeState(ReconnectHelperDialog.DialogState.PROMPT);
	}

	// Token: 0x06005B75 RID: 23413 RVA: 0x001DD3A6 File Offset: 0x001DB5A6
	private void ChangeState_FullResetRequired()
	{
		if (ReconnectMgr.Get().UpdateRequired)
		{
			this.ChangeState(HearthstoneApplication.AllowResetFromFatalError ? ReconnectHelperDialog.DialogState.BAD_VERSION_CAN_RESET : ReconnectHelperDialog.DialogState.BAD_VERSION_USE_LAUNCHER);
			return;
		}
		this.ChangeState(ReconnectHelperDialog.DialogState.RESTART_REQUIRED);
	}

	// Token: 0x06005B76 RID: 23414 RVA: 0x001DD3D2 File Offset: 0x001DB5D2
	private void OnReconnectButtonPressed()
	{
		if (Network.IsLoggedIn())
		{
			this.OnReconnectSuccess();
			return;
		}
		if (!NetworkReachabilityManager.InternetAvailable)
		{
			this.ChangeState(ReconnectHelperDialog.DialogState.WIFI_DISABLED);
			return;
		}
		this.ChangeToInProgressState();
	}

	// Token: 0x06005B77 RID: 23415 RVA: 0x001DD3F7 File Offset: 0x001DB5F7
	private void OnGoBackButtonPressed()
	{
		this.OnGiveUpReconnecting();
	}

	// Token: 0x06005B78 RID: 23416 RVA: 0x001DD3F7 File Offset: 0x001DB5F7
	private void OnCancelButtonPressed()
	{
		this.OnGiveUpReconnecting();
	}

	// Token: 0x06005B79 RID: 23417 RVA: 0x000D6936 File Offset: 0x000D4B36
	private void OnUpdateButtonPressed()
	{
		if (HearthstoneApplication.AllowResetFromFatalError)
		{
			HearthstoneApplication.Get().Reset();
			return;
		}
		HearthstoneApplication.Get().Exit();
	}

	// Token: 0x06005B7A RID: 23418 RVA: 0x001DD3FF File Offset: 0x001DB5FF
	private void OnExitGameButtonPressed()
	{
		HearthstoneApplication.Get().Exit();
	}

	// Token: 0x06005B7B RID: 23419 RVA: 0x001DD40B File Offset: 0x001DB60B
	private void OnReconnectSuccess()
	{
		ReconnectMgr.Get().SetNextReLoginCallback(this.m_reconnectSuccessCallback);
		this.Hide();
	}

	// Token: 0x06005B7C RID: 23420 RVA: 0x001DD423 File Offset: 0x001DB623
	private void OnGiveUpReconnecting()
	{
		ReconnectMgr.Get().SetNextReLoginCallback(null);
		if (this.m_goBackCallback != null)
		{
			this.m_goBackCallback();
		}
		this.Hide();
	}

	// Token: 0x06005B7D RID: 23421 RVA: 0x001DD449 File Offset: 0x001DB649
	private void OnReconnectComplete()
	{
		if (this.m_state == ReconnectHelperDialog.DialogState.IN_PROGRESS)
		{
			this.OnReconnectSuccess();
		}
	}

	// Token: 0x06005B7E RID: 23422 RVA: 0x001DD45A File Offset: 0x001DB65A
	private void ChangeToInProgressState()
	{
		this.ChangeState(ReconnectHelperDialog.DialogState.IN_PROGRESS);
		this.SetInProgressText(false);
		base.StopAllCoroutines();
		base.StartCoroutine(this.WaitThenSwitchInProgressText());
	}

	// Token: 0x06005B7F RID: 23423 RVA: 0x001DD47D File Offset: 0x001DB67D
	private IEnumerator WaitThenSwitchInProgressText()
	{
		yield return new WaitForSeconds(20f);
		this.SetInProgressText(true);
		yield break;
	}

	// Token: 0x06005B80 RID: 23424 RVA: 0x001DD48C File Offset: 0x001DB68C
	private void SetInProgressText(bool hasTimedOut)
	{
		this.m_inProgressTextNormal.gameObject.SetActive(!hasTimedOut);
		this.m_inProgressTextTimeout.gameObject.SetActive(hasTimedOut);
	}

	// Token: 0x06005B81 RID: 23425 RVA: 0x001DD4B3 File Offset: 0x001DB6B3
	private void UpdateWhileInProgress()
	{
		if (ReconnectMgr.Get().FullResetRequired)
		{
			this.ChangeState_FullResetRequired();
		}
	}

	// Token: 0x04004E1E RID: 19998
	public UIBButton m_continueButton;

	// Token: 0x04004E1F RID: 19999
	public UIBButton m_choiceOneButton;

	// Token: 0x04004E20 RID: 20000
	public UIBButton m_choiceTwoButton;

	// Token: 0x04004E21 RID: 20001
	public GameObject m_continueButtonContainer;

	// Token: 0x04004E22 RID: 20002
	public GameObject m_choiceButtonContainer;

	// Token: 0x04004E23 RID: 20003
	public Spell m_successRingSpell;

	// Token: 0x04004E24 RID: 20004
	public GameObject m_successRingContainer;

	// Token: 0x04004E25 RID: 20005
	public GameObject m_reconnectPromptPanel;

	// Token: 0x04004E26 RID: 20006
	public GameObject m_reconnectInProgressPanel;

	// Token: 0x04004E27 RID: 20007
	public GameObject m_reconnectFailurePanel;

	// Token: 0x04004E28 RID: 20008
	public GameObject m_wifiDisabledPanel;

	// Token: 0x04004E29 RID: 20009
	public GameObject m_badVersionCanResetPanel;

	// Token: 0x04004E2A RID: 20010
	public GameObject m_badVersionUseLauncherPanel;

	// Token: 0x04004E2B RID: 20011
	public GameObject m_inactiveTimeoutPanel;

	// Token: 0x04004E2C RID: 20012
	public GameObject m_restartRequiredPanel;

	// Token: 0x04004E2D RID: 20013
	public UberText m_inProgressTextNormal;

	// Token: 0x04004E2E RID: 20014
	public UberText m_inProgressTextTimeout;

	// Token: 0x04004E2F RID: 20015
	private const float IN_PROGRESS_SPINNER_TIMEOUT_SECONDS = 20f;

	// Token: 0x04004E30 RID: 20016
	private List<GameObject> m_panels = new List<GameObject>();

	// Token: 0x04004E31 RID: 20017
	private Map<ReconnectHelperDialog.DialogState, ReconnectHelperDialog.Layout> m_stateLayouts = new Map<ReconnectHelperDialog.DialogState, ReconnectHelperDialog.Layout>();

	// Token: 0x04004E32 RID: 20018
	private ReconnectHelperDialog.DialogState m_state;

	// Token: 0x04004E33 RID: 20019
	private Action m_reconnectSuccessCallback;

	// Token: 0x04004E34 RID: 20020
	private Action m_goBackCallback;

	// Token: 0x0200216E RID: 8558
	public class Info
	{
		// Token: 0x0400E043 RID: 57411
		public Action m_reconnectSuccessCallback;

		// Token: 0x0400E044 RID: 57412
		public Action m_goBackCallback;
	}

	// Token: 0x0200216F RID: 8559
	private enum DialogState
	{
		// Token: 0x0400E046 RID: 57414
		INVALID,
		// Token: 0x0400E047 RID: 57415
		PROMPT,
		// Token: 0x0400E048 RID: 57416
		IN_PROGRESS,
		// Token: 0x0400E049 RID: 57417
		FAILURE,
		// Token: 0x0400E04A RID: 57418
		WIFI_DISABLED,
		// Token: 0x0400E04B RID: 57419
		BAD_VERSION_CAN_RESET,
		// Token: 0x0400E04C RID: 57420
		BAD_VERSION_USE_LAUNCHER,
		// Token: 0x0400E04D RID: 57421
		INACTIVE_TIMEOUT,
		// Token: 0x0400E04E RID: 57422
		RESTART_REQUIRED
	}

	// Token: 0x02002170 RID: 8560
	private class Layout
	{
		// Token: 0x0400E04F RID: 57423
		public SpellStateType m_successRingState;

		// Token: 0x0400E050 RID: 57424
		public bool m_twoButtons;

		// Token: 0x0400E051 RID: 57425
		public GameObject m_activePanel;

		// Token: 0x0400E052 RID: 57426
		public string m_continueButtonText = "";

		// Token: 0x0400E053 RID: 57427
		public string m_choiceOneButtonText = "";

		// Token: 0x0400E054 RID: 57428
		public string m_choiceTwoButtonText = "";

		// Token: 0x0400E055 RID: 57429
		public Action m_continueButtonAction;

		// Token: 0x0400E056 RID: 57430
		public Action m_choiceButtonOneAction;

		// Token: 0x0400E057 RID: 57431
		public Action m_choiceButtonTwoAction;

		// Token: 0x0400E058 RID: 57432
		public Action m_onInit;
	}
}
