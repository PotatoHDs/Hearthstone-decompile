using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using Hearthstone.Core.Streaming;
using Hearthstone.Streaming;
using UnityEngine;

public class BnetBarMenuButton : PegUIElement
{
	public enum State
	{
		Default,
		Downloading
	}

	[SerializeField]
	private GameObject m_phoneBar;

	[SerializeField]
	private Transform m_phoneBarOneElementBone;

	[SerializeField]
	private Transform m_phoneBarTwoElementBone;

	[Header("Default")]
	[SerializeField]
	private GameObject m_defaultHighlight;

	[SerializeField]
	private MeshRenderer m_defaultBackground;

	[Header("Downloading")]
	[SerializeField]
	private GameObject m_downloadingHighlight;

	[SerializeField]
	private MeshRenderer m_downloadingBackground;

	[SerializeField]
	private MeshRenderer m_downloadingArrow;

	[SerializeField]
	private UberText m_downloadProgressText;

	[SerializeField]
	private float m_normalArrowTextureOffset;

	[SerializeField]
	private float m_inactiveArrowTextureOffset = 0.205f;

	[Header("Offline")]
	[SerializeField]
	private GameObject m_offlineSection;

	[SerializeField]
	private GameObject m_offlineHighlight_OfflineSection;

	[SerializeField]
	private PegUIElement m_offlineSectionButton;

	[SerializeField]
	private TooltipZone m_offlineTooltipZone;

	private bool m_selected;

	private int m_phoneBarStatus = -1;

	private float m_originalLightingBlend;

	private Dictionary<State, GameObject> m_highlightsByState;

	private Dictionary<State, MeshRenderer> m_backgroundsByState;

	private Material m_backgroundMaterial;

	private Material m_arrowMaterial;

	private Coroutine m_delayedChangeStateCoroutine;

	private int m_progressVal;

	private double m_downloadSpeed;

	private bool m_shouldShowOfflineSection;

	private Vector3 m_offlineSectionStartingPosition;

	public Action StateChanged;

	private const float CURRENCY_FRAME_OFFSET_STATE = -75f;

	private readonly PlatformDependentValue<float> CURRENCY_FRAME_OFFSET_DEFAULT = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = -25f,
		Tablet = -25f,
		Phone = -35f
	};

	private readonly PlatformDependentValue<float> CURRENCY_FRAME_OFFSET_STATE_OFFLINE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = -140f,
		Tablet = -140f,
		Phone = -215f
	};

	private const float OFFLINE_SECTION_STATE_OFFSET = 86f;

	public State CurrentState { get; private set; }

	private IGameDownloadManager DownloadManager => GameDownloadManagerProvider.Get();

	private GameObject Highlight => m_highlightsByState[CurrentState];

	private MeshRenderer Background => m_backgroundsByState[CurrentState];

	public void LockHighlight(bool isLocked)
	{
		m_highlightsByState[CurrentState].SetActive(isLocked);
	}

	protected override void Awake()
	{
		base.Awake();
		m_highlightsByState = new Dictionary<State, GameObject>
		{
			{
				State.Default,
				m_defaultHighlight
			},
			{
				State.Downloading,
				m_downloadingHighlight
			}
		};
		m_backgroundsByState = new Dictionary<State, MeshRenderer>
		{
			{
				State.Default,
				m_defaultBackground
			},
			{
				State.Downloading,
				m_downloadingBackground
			}
		};
		foreach (MeshRenderer value in m_backgroundsByState.Values)
		{
			if (value != null)
			{
				value.gameObject.SetActive(value: false);
			}
		}
		if (Background != null)
		{
			m_backgroundMaterial = Background.GetMaterial();
			if (m_backgroundMaterial != null)
			{
				m_originalLightingBlend = m_backgroundMaterial.GetFloat("_LightingBlend");
			}
		}
		if (m_downloadingArrow != null)
		{
			m_arrowMaterial = m_downloadingArrow.GetMaterial();
		}
		if (m_offlineSection != null)
		{
			m_offlineSection.SetActive(value: false);
			m_offlineSectionStartingPosition = m_offlineSection.transform.localPosition;
		}
		if (m_offlineSectionButton != null)
		{
			m_offlineSectionButton.AddEventListener(UIEventType.RELEASE, OnOfflineSectionRelease);
			m_offlineSectionButton.AddEventListener(UIEventType.ROLLOVER, OnOfflineSectionRollover);
			m_offlineSectionButton.AddEventListener(UIEventType.ROLLOUT, OnOfflineSectionRollout);
		}
		UpdateHighlight();
		ChangeState(State.Default, force: true);
	}

	private void Update()
	{
		bool offlineSectionActive = !Network.IsLoggedIn() && Network.ShouldBeConnectedToAurora() && !BattleNet.IsHeadlessAccount() && SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY;
		SetOfflineSectionActive(offlineSectionActive);
		if (DownloadManager == null)
		{
			return;
		}
		TagDownloadStatus currentDownloadStatus = DownloadManager.GetCurrentDownloadStatus();
		if (currentDownloadStatus == null || currentDownloadStatus.BytesTotal == 0L)
		{
			ChangeState(State.Default);
			return;
		}
		int num = (int)(currentDownloadStatus.Progress * 100f);
		double bytesPerSecond = DownloadManager.BytesPerSecond;
		if (num != 0 && (num != m_progressVal || bytesPerSecond != m_downloadSpeed || (CurrentState == State.Downloading && DownloadManager.IsInterrupted)))
		{
			m_progressVal = num;
			m_downloadSpeed = bytesPerSecond;
			if (!DownloadManager.IsAnyDownloadRequestedAndIncomplete)
			{
				m_downloadProgressText.Text = "...";
				ChangeStateAfterDelay(State.Default, 5f);
			}
			else if (DownloadManager.IsInterrupted)
			{
				m_downloadProgressText.Text = GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_PAUSED");
				m_arrowMaterial.mainTextureOffset = new Vector3(m_inactiveArrowTextureOffset, 0f);
				ChangeState(State.Downloading);
			}
			else
			{
				m_downloadProgressText.Text = $"{num:0.}%";
				m_arrowMaterial.mainTextureOffset = new Vector3(m_normalArrowTextureOffset, 0f);
				ChangeState(State.Downloading);
			}
		}
	}

	private void ChangeStateAfterDelay(State newState, float seconds)
	{
		if (m_delayedChangeStateCoroutine != null)
		{
			StopCoroutine(m_delayedChangeStateCoroutine);
		}
		m_delayedChangeStateCoroutine = StartCoroutine(ChangeStateAfterDelayCoroutine(newState, seconds));
	}

	private IEnumerator ChangeStateAfterDelayCoroutine(State newState, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		ChangeState(newState);
	}

	private void ChangeState(State nextState, bool force = false)
	{
		if (CurrentState != nextState || force)
		{
			if (m_delayedChangeStateCoroutine != null)
			{
				StopCoroutine(m_delayedChangeStateCoroutine);
			}
			State currentState = CurrentState;
			m_backgroundsByState[currentState].gameObject.SetActive(value: false);
			m_backgroundsByState[nextState].gameObject.SetActive(value: true);
			m_highlightsByState[nextState].gameObject.SetActive(m_highlightsByState[CurrentState].gameObject.activeSelf);
			m_highlightsByState[CurrentState].gameObject.SetActive(value: false);
			CurrentState = nextState;
			if (CurrentState == State.Default)
			{
				m_downloadProgressText.Text = "";
			}
			if (StateChanged != null)
			{
				StateChanged();
			}
		}
	}

	public bool IsSelected()
	{
		return m_selected;
	}

	public void SetSelected(bool enable)
	{
		if (enable != m_selected)
		{
			m_selected = enable;
			UpdateHighlight();
		}
	}

	public override void SetEnabled(bool enabled, bool isInternal = false)
	{
		base.SetEnabled(enabled, isInternal);
		if (m_backgroundMaterial != null)
		{
			m_backgroundMaterial.SetFloat("_LightingBlend", enabled ? m_originalLightingBlend : 0.8f);
		}
	}

	public void SetPhoneStatusBarState(int nElements)
	{
		if (nElements != m_phoneBarStatus)
		{
			m_phoneBarStatus = nElements;
			switch (nElements)
			{
			case 0:
				m_phoneBar.SetActive(value: false);
				break;
			case 1:
			{
				m_phoneBar.SetActive(value: true);
				iTween.Stop(m_phoneBar);
				Hashtable args = iTween.Hash("position", m_phoneBarOneElementBone.position, "time", 1f, "isLocal", false, "easetype", iTween.EaseType.easeOutExpo, "onupdate", "OnStatusBarUpdate", "onupdatetarget", base.gameObject);
				iTween.MoveTo(m_phoneBar, args);
				break;
			}
			case 2:
			{
				m_phoneBar.SetActive(value: true);
				iTween.Stop(m_phoneBar);
				Hashtable args = iTween.Hash("position", m_phoneBarTwoElementBone.position, "time", 1f, "isLocal", false, "easetype", iTween.EaseType.easeOutExpo, "onupdate", "OnStatusBarUpdate", "onupdatetarget", base.gameObject);
				iTween.MoveTo(m_phoneBar, args);
				break;
			}
			default:
				Debug.LogError("Invalid phone status bar state " + nElements);
				break;
			}
		}
	}

	public void OnStatusBarUpdate()
	{
		BnetBar.Get().UpdateLayout();
	}

	public float GetCurrencyFrameOffsetX()
	{
		bool flag = CurrentState == State.Default;
		if (!flag && m_shouldShowOfflineSection)
		{
			return CURRENCY_FRAME_OFFSET_STATE_OFFLINE;
		}
		if (flag && m_shouldShowOfflineSection)
		{
			return -75f;
		}
		if (!flag && !m_shouldShowOfflineSection)
		{
			return -75f;
		}
		return CURRENCY_FRAME_OFFSET_DEFAULT;
	}

	private bool ShouldBeHighlighted()
	{
		if (!m_selected)
		{
			return GetInteractionState() == InteractionState.Over;
		}
		return true;
	}

	protected virtual void UpdateHighlight()
	{
		bool flag = ShouldBeHighlighted();
		if (!flag)
		{
			BnetBar bnetBar = BnetBar.Get();
			if (bnetBar != null && bnetBar.IsGameMenuShown())
			{
				flag = true;
			}
		}
		if (Highlight.activeSelf != flag)
		{
			Highlight.SetActive(flag);
		}
	}

	private void ShowGameMenuTooltip(TooltipZone tooltipZone, string tooltipHeader, string tooltipDescription)
	{
		TooltipPanel tooltipPanel = tooltipZone.ShowTooltip(tooltipHeader, tooltipDescription, 0.7f);
		SceneUtils.SetLayer(tooltipPanel.gameObject, GameLayer.BattleNet);
		tooltipPanel.transform.localEulerAngles = new Vector3(270f, 0f, 0f);
		tooltipPanel.transform.localScale = new Vector3(82.35294f, 70f, 90.32258f);
		TransformUtil.SetPoint(tooltipPanel, Anchor.BOTTOM, base.gameObject, Anchor.TOP, new Vector3(-98.22766f, 0f, 0f));
	}

	private void SetOfflineSectionActive(bool active)
	{
		if (active != m_shouldShowOfflineSection)
		{
			m_shouldShowOfflineSection = active;
			m_offlineSection.SetActive(active);
			m_offlineSection.transform.localPosition = m_offlineSectionStartingPosition;
			if (CurrentState != 0)
			{
				m_offlineSection.transform.localPosition -= Vector3.right * 86f;
			}
			if (StateChanged != null)
			{
				StateChanged();
			}
		}
	}

	private void OnOfflineSectionRelease(UIEvent e)
	{
		if (!DialogManager.Get().ShowingDialog())
		{
			DialogManager.Get().ShowReconnectHelperDialog();
			m_offlineTooltipZone.HideTooltip();
			m_offlineHighlight_OfflineSection.SetActive(value: false);
		}
	}

	private void OnOfflineSectionRollover(UIEvent e)
	{
		if (!DialogManager.Get().ShowingDialog())
		{
			m_offlineHighlight_OfflineSection.SetActive(value: true);
			ShowGameMenuTooltip(m_offlineTooltipZone, GameStrings.Get("GLOBAL_TOOLTIP_MENU_OFFLINE_HEADER"), GameStrings.Get("GLOBAL_TOOLTIP_MENU_OFFLINE_DESC"));
			SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9");
		}
	}

	private void OnOfflineSectionRollout(UIEvent e)
	{
		m_offlineTooltipZone.HideTooltip();
		m_offlineHighlight_OfflineSection.SetActive(value: false);
	}

	protected override void OnOver(InteractionState oldState)
	{
		ShowGameMenuTooltip(GetComponent<TooltipZone>(), GameStrings.Get("GLOBAL_TOOLTIP_MENU_HEADER"), GameStrings.Get("GLOBAL_TOOLTIP_MENU_DESC"));
		SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9");
		UpdateHighlight();
	}

	protected override void OnOut(InteractionState oldState)
	{
		GetComponent<TooltipZone>().HideTooltip();
		UpdateHighlight();
	}
}
