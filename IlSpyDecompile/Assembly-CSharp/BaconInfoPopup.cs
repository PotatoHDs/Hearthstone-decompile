using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

public class BaconInfoPopup : MonoBehaviour
{
	public AsyncReference m_PlayTutorialButtonReference;

	private void Start()
	{
		m_PlayTutorialButtonReference.RegisterReadyListener<VisualController>(OnPlayTutorialButtonReady);
	}

	public void OnPlayTutorialButtonReady(VisualController buttonVisualController)
	{
		if (buttonVisualController == null)
		{
			Error.AddDevWarning("UI Error!", "PlayTutorialButton could not be found! You will not be able to click 'Play Tutorial'!");
		}
		else
		{
			buttonVisualController.gameObject.GetComponent<UIBButton>().AddEventListener(UIEventType.RELEASE, PlayTutorialButtonRelease);
		}
	}

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
		}
		else if (PartyManager.Get().IsInParty())
		{
			AlertPopup.PopupInfo info2 = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_TOOLTIP_BUTTON_BACON_HEADLINE"),
				m_text = GameStrings.Get("GLUE_BACON_PARTY_TUTORIAL_DISABLED"),
				m_showAlertIcon = false,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info2);
		}
		else
		{
			GameMgr.Get().FindGame(GameType.GT_VS_AI, FormatType.FT_WILD, 3539, 0, 0L);
		}
	}
}
