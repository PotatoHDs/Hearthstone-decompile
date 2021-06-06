using System;
using System.Collections;
using Hearthstone.Core.Streaming;
using Hearthstone.Streaming;
using UnityEngine;

// Token: 0x02000086 RID: 134
public class DownloadFrame : MonoBehaviour
{
	// Token: 0x060007A1 RID: 1953 RVA: 0x0002B47C File Offset: 0x0002967C
	private void Awake()
	{
		if (!this.m_isAwake)
		{
			this.m_mouseOverZone.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnFrameMouseOver));
			this.m_mouseOverZone.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnFrameMouseOut));
			this.HideInternal();
			this.m_isAwake = true;
		}
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x0002B4D0 File Offset: 0x000296D0
	private void Update()
	{
		if (this.ShouldShow() && this.m_currencyIsShowing)
		{
			if (!this.m_wasShowing)
			{
				this.ShowInternal();
			}
			ContentDownloadStatus contentDownloadStatus = GameDownloadManagerProvider.Get().GetContentDownloadStatus(DownloadTags.Content.Base);
			this.m_progress.Text = string.Format("{0:0.}%", contentDownloadStatus.Progress * 100f);
			return;
		}
		if (this.m_wasShowing)
		{
			this.HideInternal();
		}
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0002B53C File Offset: 0x0002973C
	public GameObject GetTooltipObject()
	{
		TooltipZone component = base.GetComponent<TooltipZone>();
		if (component != null)
		{
			return component.GetTooltipObject(0);
		}
		return null;
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0002B564 File Offset: 0x00029764
	private void SetChildrenActive(bool active)
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).gameObject.SetActive(active);
		}
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x0002B59E File Offset: 0x0002979E
	public void Hide()
	{
		base.gameObject.SetActive(false);
		this.m_currencyIsShowing = false;
		this.HideInternal();
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x0002B5BC File Offset: 0x000297BC
	private void HideInternal()
	{
		this.m_wasShowing = false;
		if (UniversalInputManager.UsePhoneUI)
		{
			this.SetChildrenActive(false);
			return;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"time",
			0.25f,
			"easeType",
			iTween.EaseType.easeOutCubic
		});
		iTween.FadeTo(base.gameObject, args);
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x0002B637 File Offset: 0x00029837
	public void Show()
	{
		this.m_currencyIsShowing = true;
		this.Awake();
		if (this.ShouldShow())
		{
			base.gameObject.SetActive(true);
			this.ShowInternal();
		}
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x0002B660 File Offset: 0x00029860
	private void ShowInternal()
	{
		if (this.m_currencyIsShowing)
		{
			this.m_wasShowing = true;
			this.SetChildrenActive(true);
			if (!UniversalInputManager.UsePhoneUI)
			{
				Hashtable args = iTween.Hash(new object[]
				{
					"amount",
					1f,
					"time",
					0.25f,
					"easeType",
					iTween.EaseType.easeOutCubic
				});
				iTween.FadeTo(base.gameObject, args);
			}
		}
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x0002B6E4 File Offset: 0x000298E4
	private void OnFrameMouseOver(UIEvent e)
	{
		if (this.ShouldShow() && this.m_currencyIsShowing)
		{
			string key = "GLUE_TOOLTIP_DOWNLOAD_HEADER";
			string key2 = "GLUE_TOOLTIP_DOWNLOAD_DESCRIPTION";
			TooltipPanel tooltipPanel = base.GetComponent<TooltipZone>().ShowTooltip(GameStrings.Get(key), GameStrings.Get(key2), 0.7f, 0);
			SceneUtils.SetLayer(tooltipPanel.gameObject, GameLayer.BattleNet);
			tooltipPanel.transform.localEulerAngles = new Vector3(270f, 0f, 0f);
			tooltipPanel.transform.localScale = new Vector3(70f, 70f, 70f);
			if (UniversalInputManager.UsePhoneUI)
			{
				TransformUtil.SetPoint(tooltipPanel, Anchor.TOP, this.m_mouseOverZone, Anchor.BOTTOM, Vector3.zero);
				return;
			}
			TransformUtil.SetPoint(tooltipPanel, Anchor.BOTTOM, this.m_mouseOverZone, Anchor.TOP, Vector3.zero);
		}
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x0002B336 File Offset: 0x00029536
	private void OnFrameMouseOut(UIEvent e)
	{
		base.GetComponent<TooltipZone>().HideTooltip();
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0002B7AF File Offset: 0x000299AF
	private bool ShouldShow()
	{
		return GameDownloadManagerProvider.Get().IsAnyDownloadRequestedAndIncomplete;
	}

	// Token: 0x04000529 RID: 1321
	public UberText m_progress;

	// Token: 0x0400052A RID: 1322
	public GameObject m_downloadArrow;

	// Token: 0x0400052B RID: 1323
	public GameObject m_background;

	// Token: 0x0400052C RID: 1324
	public PegUIElement m_mouseOverZone;

	// Token: 0x0400052D RID: 1325
	private bool m_currencyIsShowing;

	// Token: 0x0400052E RID: 1326
	private bool m_wasShowing;

	// Token: 0x0400052F RID: 1327
	private bool m_isAwake;
}
