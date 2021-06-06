using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using Hearthstone.Core.Streaming;
using Hearthstone.Streaming;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class BnetBarMenuButton : PegUIElement
{
	// Token: 0x17000058 RID: 88
	// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00027493 File Offset: 0x00025693
	// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0002748A File Offset: 0x0002568A
	public BnetBarMenuButton.State CurrentState { get; private set; }

	// Token: 0x060006C9 RID: 1737 RVA: 0x0002749B File Offset: 0x0002569B
	public void LockHighlight(bool isLocked)
	{
		this.m_highlightsByState[this.CurrentState].SetActive(isLocked);
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x060006CA RID: 1738 RVA: 0x000274B4 File Offset: 0x000256B4
	private IGameDownloadManager DownloadManager
	{
		get
		{
			return GameDownloadManagerProvider.Get();
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x060006CB RID: 1739 RVA: 0x000274BB File Offset: 0x000256BB
	private GameObject Highlight
	{
		get
		{
			return this.m_highlightsByState[this.CurrentState];
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x060006CC RID: 1740 RVA: 0x000274CE File Offset: 0x000256CE
	private MeshRenderer Background
	{
		get
		{
			return this.m_backgroundsByState[this.CurrentState];
		}
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x000274E4 File Offset: 0x000256E4
	protected override void Awake()
	{
		base.Awake();
		this.m_highlightsByState = new Dictionary<BnetBarMenuButton.State, GameObject>
		{
			{
				BnetBarMenuButton.State.Default,
				this.m_defaultHighlight
			},
			{
				BnetBarMenuButton.State.Downloading,
				this.m_downloadingHighlight
			}
		};
		this.m_backgroundsByState = new Dictionary<BnetBarMenuButton.State, MeshRenderer>
		{
			{
				BnetBarMenuButton.State.Default,
				this.m_defaultBackground
			},
			{
				BnetBarMenuButton.State.Downloading,
				this.m_downloadingBackground
			}
		};
		foreach (MeshRenderer meshRenderer in this.m_backgroundsByState.Values)
		{
			if (meshRenderer != null)
			{
				meshRenderer.gameObject.SetActive(false);
			}
		}
		if (this.Background != null)
		{
			this.m_backgroundMaterial = this.Background.GetMaterial();
			if (this.m_backgroundMaterial != null)
			{
				this.m_originalLightingBlend = this.m_backgroundMaterial.GetFloat("_LightingBlend");
			}
		}
		if (this.m_downloadingArrow != null)
		{
			this.m_arrowMaterial = this.m_downloadingArrow.GetMaterial();
		}
		if (this.m_offlineSection != null)
		{
			this.m_offlineSection.SetActive(false);
			this.m_offlineSectionStartingPosition = this.m_offlineSection.transform.localPosition;
		}
		if (this.m_offlineSectionButton != null)
		{
			this.m_offlineSectionButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnOfflineSectionRelease));
			this.m_offlineSectionButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnOfflineSectionRollover));
			this.m_offlineSectionButton.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnOfflineSectionRollout));
		}
		this.UpdateHighlight();
		this.ChangeState(BnetBarMenuButton.State.Default, true);
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x00027694 File Offset: 0x00025894
	private void Update()
	{
		bool offlineSectionActive = !Network.IsLoggedIn() && Network.ShouldBeConnectedToAurora() && !BattleNet.IsHeadlessAccount() && SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY;
		this.SetOfflineSectionActive(offlineSectionActive);
		if (this.DownloadManager == null)
		{
			return;
		}
		TagDownloadStatus currentDownloadStatus = this.DownloadManager.GetCurrentDownloadStatus();
		if (currentDownloadStatus == null || currentDownloadStatus.BytesTotal == 0L)
		{
			this.ChangeState(BnetBarMenuButton.State.Default, false);
			return;
		}
		int num = (int)(currentDownloadStatus.Progress * 100f);
		double bytesPerSecond = this.DownloadManager.BytesPerSecond;
		if (num == 0 || (num == this.m_progressVal && bytesPerSecond == this.m_downloadSpeed && (this.CurrentState != BnetBarMenuButton.State.Downloading || !this.DownloadManager.IsInterrupted)))
		{
			return;
		}
		this.m_progressVal = num;
		this.m_downloadSpeed = bytesPerSecond;
		if (!this.DownloadManager.IsAnyDownloadRequestedAndIncomplete)
		{
			this.m_downloadProgressText.Text = "...";
			this.ChangeStateAfterDelay(BnetBarMenuButton.State.Default, 5f);
			return;
		}
		if (this.DownloadManager.IsInterrupted)
		{
			this.m_downloadProgressText.Text = GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_PAUSED");
			this.m_arrowMaterial.mainTextureOffset = new Vector3(this.m_inactiveArrowTextureOffset, 0f);
			this.ChangeState(BnetBarMenuButton.State.Downloading, false);
			return;
		}
		this.m_downloadProgressText.Text = string.Format("{0:0.}%", num);
		this.m_arrowMaterial.mainTextureOffset = new Vector3(this.m_normalArrowTextureOffset, 0f);
		this.ChangeState(BnetBarMenuButton.State.Downloading, false);
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x00027806 File Offset: 0x00025A06
	private void ChangeStateAfterDelay(BnetBarMenuButton.State newState, float seconds)
	{
		if (this.m_delayedChangeStateCoroutine != null)
		{
			base.StopCoroutine(this.m_delayedChangeStateCoroutine);
		}
		this.m_delayedChangeStateCoroutine = base.StartCoroutine(this.ChangeStateAfterDelayCoroutine(newState, seconds));
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x00027830 File Offset: 0x00025A30
	private IEnumerator ChangeStateAfterDelayCoroutine(BnetBarMenuButton.State newState, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		this.ChangeState(newState, false);
		yield break;
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x00027850 File Offset: 0x00025A50
	private void ChangeState(BnetBarMenuButton.State nextState, bool force = false)
	{
		if (this.CurrentState == nextState && !force)
		{
			return;
		}
		if (this.m_delayedChangeStateCoroutine != null)
		{
			base.StopCoroutine(this.m_delayedChangeStateCoroutine);
		}
		BnetBarMenuButton.State currentState = this.CurrentState;
		this.m_backgroundsByState[currentState].gameObject.SetActive(false);
		this.m_backgroundsByState[nextState].gameObject.SetActive(true);
		this.m_highlightsByState[nextState].gameObject.SetActive(this.m_highlightsByState[this.CurrentState].gameObject.activeSelf);
		this.m_highlightsByState[this.CurrentState].gameObject.SetActive(false);
		this.CurrentState = nextState;
		if (this.CurrentState == BnetBarMenuButton.State.Default)
		{
			this.m_downloadProgressText.Text = "";
		}
		if (this.StateChanged != null)
		{
			this.StateChanged();
		}
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x00027932 File Offset: 0x00025B32
	public bool IsSelected()
	{
		return this.m_selected;
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x0002793A File Offset: 0x00025B3A
	public void SetSelected(bool enable)
	{
		if (enable == this.m_selected)
		{
			return;
		}
		this.m_selected = enable;
		this.UpdateHighlight();
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x00027953 File Offset: 0x00025B53
	public override void SetEnabled(bool enabled, bool isInternal = false)
	{
		base.SetEnabled(enabled, isInternal);
		if (this.m_backgroundMaterial != null)
		{
			this.m_backgroundMaterial.SetFloat("_LightingBlend", enabled ? this.m_originalLightingBlend : 0.8f);
		}
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x0002798C File Offset: 0x00025B8C
	public void SetPhoneStatusBarState(int nElements)
	{
		if (nElements == this.m_phoneBarStatus)
		{
			return;
		}
		this.m_phoneBarStatus = nElements;
		switch (nElements)
		{
		case 0:
			this.m_phoneBar.SetActive(false);
			return;
		case 1:
		{
			this.m_phoneBar.SetActive(true);
			iTween.Stop(this.m_phoneBar);
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				this.m_phoneBarOneElementBone.position,
				"time",
				1f,
				"isLocal",
				false,
				"easetype",
				iTween.EaseType.easeOutExpo,
				"onupdate",
				"OnStatusBarUpdate",
				"onupdatetarget",
				base.gameObject
			});
			iTween.MoveTo(this.m_phoneBar, args);
			return;
		}
		case 2:
		{
			this.m_phoneBar.SetActive(true);
			iTween.Stop(this.m_phoneBar);
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				this.m_phoneBarTwoElementBone.position,
				"time",
				1f,
				"isLocal",
				false,
				"easetype",
				iTween.EaseType.easeOutExpo,
				"onupdate",
				"OnStatusBarUpdate",
				"onupdatetarget",
				base.gameObject
			});
			iTween.MoveTo(this.m_phoneBar, args);
			return;
		}
		default:
			Debug.LogError("Invalid phone status bar state " + nElements);
			return;
		}
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00027B33 File Offset: 0x00025D33
	public void OnStatusBarUpdate()
	{
		BnetBar.Get().UpdateLayout();
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00027B40 File Offset: 0x00025D40
	public float GetCurrencyFrameOffsetX()
	{
		bool flag = this.CurrentState == BnetBarMenuButton.State.Default;
		if (!flag && this.m_shouldShowOfflineSection)
		{
			return this.CURRENCY_FRAME_OFFSET_STATE_OFFLINE;
		}
		if (flag && this.m_shouldShowOfflineSection)
		{
			return -75f;
		}
		if (!flag && !this.m_shouldShowOfflineSection)
		{
			return -75f;
		}
		return this.CURRENCY_FRAME_OFFSET_DEFAULT;
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x00027B9B File Offset: 0x00025D9B
	private bool ShouldBeHighlighted()
	{
		return this.m_selected || base.GetInteractionState() == PegUIElement.InteractionState.Over;
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x00027BB0 File Offset: 0x00025DB0
	protected virtual void UpdateHighlight()
	{
		bool flag = this.ShouldBeHighlighted();
		if (!flag)
		{
			BnetBar bnetBar = BnetBar.Get();
			if (bnetBar != null && bnetBar.IsGameMenuShown())
			{
				flag = true;
			}
		}
		if (this.Highlight.activeSelf == flag)
		{
			return;
		}
		this.Highlight.SetActive(flag);
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x00027BFC File Offset: 0x00025DFC
	private void ShowGameMenuTooltip(TooltipZone tooltipZone, string tooltipHeader, string tooltipDescription)
	{
		TooltipPanel tooltipPanel = tooltipZone.ShowTooltip(tooltipHeader, tooltipDescription, 0.7f, 0);
		SceneUtils.SetLayer(tooltipPanel.gameObject, GameLayer.BattleNet);
		tooltipPanel.transform.localEulerAngles = new Vector3(270f, 0f, 0f);
		tooltipPanel.transform.localScale = new Vector3(82.35294f, 70f, 90.32258f);
		TransformUtil.SetPoint(tooltipPanel, Anchor.BOTTOM, base.gameObject, Anchor.TOP, new Vector3(-98.22766f, 0f, 0f));
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00027C84 File Offset: 0x00025E84
	private void SetOfflineSectionActive(bool active)
	{
		if (active == this.m_shouldShowOfflineSection)
		{
			return;
		}
		this.m_shouldShowOfflineSection = active;
		this.m_offlineSection.SetActive(active);
		this.m_offlineSection.transform.localPosition = this.m_offlineSectionStartingPosition;
		if (this.CurrentState != BnetBarMenuButton.State.Default)
		{
			this.m_offlineSection.transform.localPosition -= Vector3.right * 86f;
		}
		if (this.StateChanged != null)
		{
			this.StateChanged();
		}
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00027D09 File Offset: 0x00025F09
	private void OnOfflineSectionRelease(UIEvent e)
	{
		if (!DialogManager.Get().ShowingDialog())
		{
			DialogManager.Get().ShowReconnectHelperDialog(null, null);
			this.m_offlineTooltipZone.HideTooltip();
			this.m_offlineHighlight_OfflineSection.SetActive(false);
		}
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x00027D3C File Offset: 0x00025F3C
	private void OnOfflineSectionRollover(UIEvent e)
	{
		if (!DialogManager.Get().ShowingDialog())
		{
			this.m_offlineHighlight_OfflineSection.SetActive(true);
			this.ShowGameMenuTooltip(this.m_offlineTooltipZone, GameStrings.Get("GLOBAL_TOOLTIP_MENU_OFFLINE_HEADER"), GameStrings.Get("GLOBAL_TOOLTIP_MENU_OFFLINE_DESC"));
			SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9");
		}
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x00027D95 File Offset: 0x00025F95
	private void OnOfflineSectionRollout(UIEvent e)
	{
		this.m_offlineTooltipZone.HideTooltip();
		this.m_offlineHighlight_OfflineSection.SetActive(false);
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x00027DAE File Offset: 0x00025FAE
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		this.ShowGameMenuTooltip(base.GetComponent<TooltipZone>(), GameStrings.Get("GLOBAL_TOOLTIP_MENU_HEADER"), GameStrings.Get("GLOBAL_TOOLTIP_MENU_DESC"));
		SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9");
		this.UpdateHighlight();
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x00027DEA File Offset: 0x00025FEA
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		base.GetComponent<TooltipZone>().HideTooltip();
		this.UpdateHighlight();
	}

	// Token: 0x040004AB RID: 1195
	[SerializeField]
	private GameObject m_phoneBar;

	// Token: 0x040004AC RID: 1196
	[SerializeField]
	private Transform m_phoneBarOneElementBone;

	// Token: 0x040004AD RID: 1197
	[SerializeField]
	private Transform m_phoneBarTwoElementBone;

	// Token: 0x040004AE RID: 1198
	[Header("Default")]
	[SerializeField]
	private GameObject m_defaultHighlight;

	// Token: 0x040004AF RID: 1199
	[SerializeField]
	private MeshRenderer m_defaultBackground;

	// Token: 0x040004B0 RID: 1200
	[Header("Downloading")]
	[SerializeField]
	private GameObject m_downloadingHighlight;

	// Token: 0x040004B1 RID: 1201
	[SerializeField]
	private MeshRenderer m_downloadingBackground;

	// Token: 0x040004B2 RID: 1202
	[SerializeField]
	private MeshRenderer m_downloadingArrow;

	// Token: 0x040004B3 RID: 1203
	[SerializeField]
	private UberText m_downloadProgressText;

	// Token: 0x040004B4 RID: 1204
	[SerializeField]
	private float m_normalArrowTextureOffset;

	// Token: 0x040004B5 RID: 1205
	[SerializeField]
	private float m_inactiveArrowTextureOffset = 0.205f;

	// Token: 0x040004B6 RID: 1206
	[Header("Offline")]
	[SerializeField]
	private GameObject m_offlineSection;

	// Token: 0x040004B7 RID: 1207
	[SerializeField]
	private GameObject m_offlineHighlight_OfflineSection;

	// Token: 0x040004B8 RID: 1208
	[SerializeField]
	private PegUIElement m_offlineSectionButton;

	// Token: 0x040004B9 RID: 1209
	[SerializeField]
	private TooltipZone m_offlineTooltipZone;

	// Token: 0x040004BB RID: 1211
	private bool m_selected;

	// Token: 0x040004BC RID: 1212
	private int m_phoneBarStatus = -1;

	// Token: 0x040004BD RID: 1213
	private float m_originalLightingBlend;

	// Token: 0x040004BE RID: 1214
	private Dictionary<BnetBarMenuButton.State, GameObject> m_highlightsByState;

	// Token: 0x040004BF RID: 1215
	private Dictionary<BnetBarMenuButton.State, MeshRenderer> m_backgroundsByState;

	// Token: 0x040004C0 RID: 1216
	private Material m_backgroundMaterial;

	// Token: 0x040004C1 RID: 1217
	private Material m_arrowMaterial;

	// Token: 0x040004C2 RID: 1218
	private Coroutine m_delayedChangeStateCoroutine;

	// Token: 0x040004C3 RID: 1219
	private int m_progressVal;

	// Token: 0x040004C4 RID: 1220
	private double m_downloadSpeed;

	// Token: 0x040004C5 RID: 1221
	private bool m_shouldShowOfflineSection;

	// Token: 0x040004C6 RID: 1222
	private Vector3 m_offlineSectionStartingPosition;

	// Token: 0x040004C7 RID: 1223
	public Action StateChanged;

	// Token: 0x040004C8 RID: 1224
	private const float CURRENCY_FRAME_OFFSET_STATE = -75f;

	// Token: 0x040004C9 RID: 1225
	private readonly PlatformDependentValue<float> CURRENCY_FRAME_OFFSET_DEFAULT = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = -25f,
		Tablet = -25f,
		Phone = -35f
	};

	// Token: 0x040004CA RID: 1226
	private readonly PlatformDependentValue<float> CURRENCY_FRAME_OFFSET_STATE_OFFLINE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = -140f,
		Tablet = -140f,
		Phone = -215f
	};

	// Token: 0x040004CB RID: 1227
	private const float OFFLINE_SECTION_STATE_OFFSET = 86f;

	// Token: 0x02001374 RID: 4980
	public enum State
	{
		// Token: 0x0400A6CD RID: 42701
		Default,
		// Token: 0x0400A6CE RID: 42702
		Downloading
	}
}
