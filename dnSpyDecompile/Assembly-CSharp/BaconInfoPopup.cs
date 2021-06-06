using System;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class BaconInfoPopup : MonoBehaviour
{
	// Token: 0x0600060B RID: 1547 RVA: 0x00023AA4 File Offset: 0x00021CA4
	private void Start()
	{
		this.m_PlayTutorialButtonReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnPlayTutorialButtonReady));
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x00023ABD File Offset: 0x00021CBD
	public void OnPlayTutorialButtonReady(VisualController buttonVisualController)
	{
		if (buttonVisualController == null)
		{
			Error.AddDevWarning("UI Error!", "PlayTutorialButton could not be found! You will not be able to click 'Play Tutorial'!", Array.Empty<object>());
			return;
		}
		buttonVisualController.gameObject.GetComponent<UIBButton>().AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.PlayTutorialButtonRelease));
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x00023AFC File Offset: 0x00021CFC
	public void PlayTutorialButtonRelease(UIEvent e)
	{
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.BattlegroundsTutorial)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_TOOLTIP_BUTTON_BACON_HEADLINE"),
				m_text = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
			return;
		}
		if (PartyManager.Get().IsInParty())
		{
			AlertPopup.PopupInfo info2 = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_TOOLTIP_BUTTON_BACON_HEADLINE"),
				m_text = GameStrings.Get("GLUE_BACON_PARTY_TUTORIAL_DISABLED"),
				m_showAlertIcon = false,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info2);
			return;
		}
		GameMgr.Get().FindGame(GameType.GT_VS_AI, FormatType.FT_WILD, 3539, 0, 0L, null, null, false, null, GameType.GT_UNKNOWN);
	}

	// Token: 0x0400043E RID: 1086
	public AsyncReference m_PlayTutorialButtonReference;
}
