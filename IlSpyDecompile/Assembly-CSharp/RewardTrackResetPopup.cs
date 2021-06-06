using Hearthstone.UI;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class RewardTrackResetPopup : MonoBehaviour
{
	public UberText m_headerText;

	private Widget m_widget;

	private void Awake()
	{
		m_widget = GetComponent<WidgetTemplate>();
		m_headerText.Text = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_POPUP_NEW_TRACK_TITLE");
	}
}
