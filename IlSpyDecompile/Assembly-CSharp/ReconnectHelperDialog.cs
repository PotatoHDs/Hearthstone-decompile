using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

public class ReconnectHelperDialog : DialogBase
{
	public class Info
	{
		public Action m_reconnectSuccessCallback;

		public Action m_goBackCallback;
	}

	private enum DialogState
	{
		INVALID,
		PROMPT,
		IN_PROGRESS,
		FAILURE,
		WIFI_DISABLED,
		BAD_VERSION_CAN_RESET,
		BAD_VERSION_USE_LAUNCHER,
		INACTIVE_TIMEOUT,
		RESTART_REQUIRED
	}

	private class Layout
	{
		public SpellStateType m_successRingState;

		public bool m_twoButtons;

		public GameObject m_activePanel;

		public string m_continueButtonText = "";

		public string m_choiceOneButtonText = "";

		public string m_choiceTwoButtonText = "";

		public Action m_continueButtonAction;

		public Action m_choiceButtonOneAction;

		public Action m_choiceButtonTwoAction;

		public Action m_onInit;
	}

	public UIBButton m_continueButton;

	public UIBButton m_choiceOneButton;

	public UIBButton m_choiceTwoButton;

	public GameObject m_continueButtonContainer;

	public GameObject m_choiceButtonContainer;

	public Spell m_successRingSpell;

	public GameObject m_successRingContainer;

	public GameObject m_reconnectPromptPanel;

	public GameObject m_reconnectInProgressPanel;

	public GameObject m_reconnectFailurePanel;

	public GameObject m_wifiDisabledPanel;

	public GameObject m_badVersionCanResetPanel;

	public GameObject m_badVersionUseLauncherPanel;

	public GameObject m_inactiveTimeoutPanel;

	public GameObject m_restartRequiredPanel;

	public UberText m_inProgressTextNormal;

	public UberText m_inProgressTextTimeout;

	private const float IN_PROGRESS_SPINNER_TIMEOUT_SECONDS = 20f;

	private List<GameObject> m_panels = new List<GameObject>();

	private Map<DialogState, Layout> m_stateLayouts = new Map<DialogState, Layout>();

	private DialogState m_state;

	private Action m_reconnectSuccessCallback;

	private Action m_goBackCallback;

	private void Start()
	{
		PopulatePanels();
		CreateStateMap();
		m_continueButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			m_stateLayouts[m_state].m_continueButtonAction?.Invoke();
		});
		m_choiceOneButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			m_stateLayouts[m_state].m_choiceButtonOneAction?.Invoke();
		});
		m_choiceTwoButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			m_stateLayouts[m_state].m_choiceButtonTwoAction?.Invoke();
		});
		ChangeStateToPromptBasedOnReconnectMgr();
		ReconnectMgr.Get().OnReconnectComplete += OnReconnectComplete;
	}

	private void Update()
	{
		if (m_state == DialogState.IN_PROGRESS)
		{
			UpdateWhileInProgress();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		ReconnectMgr reconnectMgr = ReconnectMgr.Get();
		if (reconnectMgr != null)
		{
			reconnectMgr.OnReconnectComplete -= OnReconnectComplete;
		}
	}

	public override void Show()
	{
		base.Show();
		BnetBar.Get().DisableButtonsByDialog(this);
		SoundManager.Get().LoadAndPlay("Expand_Up.prefab:775d97ea42498c044897f396362b9db3");
		DoShowAnimation();
		DialogBase.DoBlur();
	}

	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("Shrink_Down.prefab:a6d5184049ac041418cd5896e7d9a87a");
		DialogBase.EndBlur();
	}

	public void SetInfo(Info info)
	{
		m_reconnectSuccessCallback = info.m_reconnectSuccessCallback;
		m_goBackCallback = info.m_goBackCallback;
	}

	private void PopulatePanels()
	{
		m_panels.Add(m_reconnectPromptPanel);
		m_panels.Add(m_reconnectInProgressPanel);
		m_panels.Add(m_reconnectFailurePanel);
		m_panels.Add(m_wifiDisabledPanel);
		m_panels.Add(m_badVersionCanResetPanel);
		m_panels.Add(m_badVersionUseLauncherPanel);
		m_panels.Add(m_inactiveTimeoutPanel);
		m_panels.Add(m_restartRequiredPanel);
	}

	private void CreateStateMap()
	{
		m_stateLayouts[DialogState.PROMPT] = new Layout
		{
			m_activePanel = m_reconnectPromptPanel,
			m_twoButtons = true,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CONFIRM"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_choiceButtonOneAction = OnReconnectButtonPressed,
			m_choiceButtonTwoAction = OnGoBackButtonPressed
		};
		m_stateLayouts[DialogState.IN_PROGRESS] = new Layout
		{
			m_activePanel = m_reconnectInProgressPanel,
			m_successRingState = SpellStateType.BIRTH,
			m_continueButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_continueButtonAction = OnCancelButtonPressed
		};
		m_stateLayouts[DialogState.FAILURE] = new Layout
		{
			m_activePanel = m_reconnectFailurePanel,
			m_twoButtons = true,
			m_successRingState = SpellStateType.DEATH,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CONFIRM"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_choiceButtonOneAction = OnReconnectButtonPressed,
			m_choiceButtonTwoAction = OnGoBackButtonPressed
		};
		m_stateLayouts[DialogState.WIFI_DISABLED] = new Layout
		{
			m_activePanel = m_wifiDisabledPanel,
			m_twoButtons = true,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CONFIRM"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_choiceButtonOneAction = OnReconnectButtonPressed,
			m_choiceButtonTwoAction = OnGoBackButtonPressed
		};
		m_stateLayouts[DialogState.BAD_VERSION_CAN_RESET] = new Layout
		{
			m_activePanel = m_badVersionCanResetPanel,
			m_twoButtons = true,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_UPDATE"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_choiceButtonOneAction = OnUpdateButtonPressed,
			m_choiceButtonTwoAction = OnGoBackButtonPressed
		};
		m_stateLayouts[DialogState.BAD_VERSION_USE_LAUNCHER] = new Layout
		{
			m_activePanel = m_badVersionUseLauncherPanel,
			m_twoButtons = false,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_EXIT_GAME"),
			m_choiceButtonOneAction = OnExitGameButtonPressed,
			m_choiceButtonTwoAction = OnGoBackButtonPressed
		};
		m_stateLayouts[DialogState.INACTIVE_TIMEOUT] = new Layout
		{
			m_activePanel = m_inactiveTimeoutPanel,
			m_twoButtons = true,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CONFIRM"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_choiceButtonOneAction = OnReconnectButtonPressed,
			m_choiceButtonTwoAction = OnGoBackButtonPressed
		};
		m_stateLayouts[DialogState.RESTART_REQUIRED] = new Layout
		{
			m_activePanel = m_restartRequiredPanel,
			m_twoButtons = true,
			m_choiceOneButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_EXIT_GAME"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_RECONNECT_HELPER_CANCEL"),
			m_choiceButtonOneAction = OnExitGameButtonPressed,
			m_choiceButtonTwoAction = OnGoBackButtonPressed
		};
	}

	private void ChangeState(DialogState state)
	{
		if (state != m_state && !(this == null) && !(base.gameObject == null))
		{
			m_state = state;
			LoadState();
		}
	}

	private void LoadState()
	{
		Layout layout = m_stateLayouts[m_state];
		m_continueButton.SetText(layout.m_continueButtonText);
		m_choiceOneButton.SetText(layout.m_choiceOneButtonText);
		m_choiceTwoButton.SetText(layout.m_choiceTwoButtonText);
		m_continueButtonContainer.SetActive(!layout.m_twoButtons);
		m_choiceButtonContainer.SetActive(layout.m_twoButtons);
		m_successRingContainer.SetActive(layout.m_successRingState != SpellStateType.NONE);
		if (layout.m_successRingState != 0)
		{
			m_successRingSpell.ActivateState(layout.m_successRingState);
		}
		for (int i = 0; i < m_panels.Count; i++)
		{
			GameObject obj = m_panels[i];
			obj.SetActive(obj == layout.m_activePanel);
		}
		if (layout.m_onInit != null)
		{
			layout.m_onInit();
		}
	}

	private void ChangeStateToPromptBasedOnReconnectMgr()
	{
		if (ReconnectMgr.Get().FullResetRequired)
		{
			ChangeState_FullResetRequired();
		}
		else if (InactivePlayerKicker.Get().WasKickedForInactivity)
		{
			ChangeState(DialogState.INACTIVE_TIMEOUT);
		}
		else
		{
			ChangeState(DialogState.PROMPT);
		}
	}

	private void ChangeState_FullResetRequired()
	{
		if (ReconnectMgr.Get().UpdateRequired)
		{
			ChangeState(HearthstoneApplication.AllowResetFromFatalError ? DialogState.BAD_VERSION_CAN_RESET : DialogState.BAD_VERSION_USE_LAUNCHER);
		}
		else
		{
			ChangeState(DialogState.RESTART_REQUIRED);
		}
	}

	private void OnReconnectButtonPressed()
	{
		if (Network.IsLoggedIn())
		{
			OnReconnectSuccess();
		}
		else if (!NetworkReachabilityManager.InternetAvailable)
		{
			ChangeState(DialogState.WIFI_DISABLED);
		}
		else
		{
			ChangeToInProgressState();
		}
	}

	private void OnGoBackButtonPressed()
	{
		OnGiveUpReconnecting();
	}

	private void OnCancelButtonPressed()
	{
		OnGiveUpReconnecting();
	}

	private void OnUpdateButtonPressed()
	{
		if ((bool)HearthstoneApplication.AllowResetFromFatalError)
		{
			HearthstoneApplication.Get().Reset();
		}
		else
		{
			HearthstoneApplication.Get().Exit();
		}
	}

	private void OnExitGameButtonPressed()
	{
		HearthstoneApplication.Get().Exit();
	}

	private void OnReconnectSuccess()
	{
		ReconnectMgr.Get().SetNextReLoginCallback(m_reconnectSuccessCallback);
		Hide();
	}

	private void OnGiveUpReconnecting()
	{
		ReconnectMgr.Get().SetNextReLoginCallback(null);
		if (m_goBackCallback != null)
		{
			m_goBackCallback();
		}
		Hide();
	}

	private void OnReconnectComplete()
	{
		if (m_state == DialogState.IN_PROGRESS)
		{
			OnReconnectSuccess();
		}
	}

	private void ChangeToInProgressState()
	{
		ChangeState(DialogState.IN_PROGRESS);
		SetInProgressText(hasTimedOut: false);
		StopAllCoroutines();
		StartCoroutine(WaitThenSwitchInProgressText());
	}

	private IEnumerator WaitThenSwitchInProgressText()
	{
		yield return new WaitForSeconds(20f);
		SetInProgressText(hasTimedOut: true);
	}

	private void SetInProgressText(bool hasTimedOut)
	{
		m_inProgressTextNormal.gameObject.SetActive(!hasTimedOut);
		m_inProgressTextTimeout.gameObject.SetActive(hasTimedOut);
	}

	private void UpdateWhileInProgress()
	{
		if (ReconnectMgr.Get().FullResetRequired)
		{
			ChangeState_FullResetRequired();
		}
	}
}
