using System.Collections;
using Hearthstone.Core.Streaming;
using Hearthstone.Streaming;
using UnityEngine;

public class DownloadFrame : MonoBehaviour
{
	public UberText m_progress;

	public GameObject m_downloadArrow;

	public GameObject m_background;

	public PegUIElement m_mouseOverZone;

	private bool m_currencyIsShowing;

	private bool m_wasShowing;

	private bool m_isAwake;

	private void Awake()
	{
		if (!m_isAwake)
		{
			m_mouseOverZone.AddEventListener(UIEventType.ROLLOVER, OnFrameMouseOver);
			m_mouseOverZone.AddEventListener(UIEventType.ROLLOUT, OnFrameMouseOut);
			HideInternal();
			m_isAwake = true;
		}
	}

	private void Update()
	{
		if (ShouldShow() && m_currencyIsShowing)
		{
			if (!m_wasShowing)
			{
				ShowInternal();
			}
			ContentDownloadStatus contentDownloadStatus = GameDownloadManagerProvider.Get().GetContentDownloadStatus(DownloadTags.Content.Base);
			m_progress.Text = $"{contentDownloadStatus.Progress * 100f:0.}%";
		}
		else if (m_wasShowing)
		{
			HideInternal();
		}
	}

	public GameObject GetTooltipObject()
	{
		TooltipZone component = GetComponent<TooltipZone>();
		if (component != null)
		{
			return component.GetTooltipObject();
		}
		return null;
	}

	private void SetChildrenActive(bool active)
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).gameObject.SetActive(active);
		}
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
		m_currencyIsShowing = false;
		HideInternal();
	}

	private void HideInternal()
	{
		m_wasShowing = false;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			SetChildrenActive(active: false);
			return;
		}
		Hashtable args = iTween.Hash("amount", 0f, "time", 0.25f, "easeType", iTween.EaseType.easeOutCubic);
		iTween.FadeTo(base.gameObject, args);
	}

	public void Show()
	{
		m_currencyIsShowing = true;
		Awake();
		if (ShouldShow())
		{
			base.gameObject.SetActive(value: true);
			ShowInternal();
		}
	}

	private void ShowInternal()
	{
		if (m_currencyIsShowing)
		{
			m_wasShowing = true;
			SetChildrenActive(active: true);
			if (!UniversalInputManager.UsePhoneUI)
			{
				Hashtable args = iTween.Hash("amount", 1f, "time", 0.25f, "easeType", iTween.EaseType.easeOutCubic);
				iTween.FadeTo(base.gameObject, args);
			}
		}
	}

	private void OnFrameMouseOver(UIEvent e)
	{
		if (ShouldShow() && m_currencyIsShowing)
		{
			string key = "GLUE_TOOLTIP_DOWNLOAD_HEADER";
			string key2 = "GLUE_TOOLTIP_DOWNLOAD_DESCRIPTION";
			TooltipPanel tooltipPanel = GetComponent<TooltipZone>().ShowTooltip(GameStrings.Get(key), GameStrings.Get(key2), 0.7f);
			SceneUtils.SetLayer(tooltipPanel.gameObject, GameLayer.BattleNet);
			tooltipPanel.transform.localEulerAngles = new Vector3(270f, 0f, 0f);
			tooltipPanel.transform.localScale = new Vector3(70f, 70f, 70f);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				TransformUtil.SetPoint(tooltipPanel, Anchor.TOP, m_mouseOverZone, Anchor.BOTTOM, Vector3.zero);
			}
			else
			{
				TransformUtil.SetPoint(tooltipPanel, Anchor.BOTTOM, m_mouseOverZone, Anchor.TOP, Vector3.zero);
			}
		}
	}

	private void OnFrameMouseOut(UIEvent e)
	{
		GetComponent<TooltipZone>().HideTooltip();
	}

	private bool ShouldShow()
	{
		return GameDownloadManagerProvider.Get().IsAnyDownloadRequestedAndIncomplete;
	}
}
