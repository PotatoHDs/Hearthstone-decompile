using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class RewardTrackForgotRewardsPopup : MonoBehaviour
{
	public UberText m_headerText;

	public UberText m_bodyText;

	private Widget m_widget;

	private readonly string CODE_HIDE = "CODE_HIDE";

	private void Awake()
	{
		m_widget = GetComponent<WidgetTemplate>();
		m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == CODE_HIDE)
			{
				Hide();
			}
		});
	}

	public void Show()
	{
		RewardTrackDataModel dataModel = m_widget.GetDataModel<RewardTrackDataModel>();
		if (dataModel == null)
		{
			Debug.LogWarning("Unexpected state: no bound RewardTrackDataModel");
			return;
		}
		GameStrings.PluralNumber[] pluralNumbers = new GameStrings.PluralNumber[1]
		{
			new GameStrings.PluralNumber
			{
				m_index = 0,
				m_number = dataModel.Unclaimed
			}
		};
		m_headerText.Text = GameStrings.FormatPlurals("GLUE_PROGRESSION_REWARD_TRACK_POPUP_FORGOT_REWARDS_TITLE", pluralNumbers);
		m_bodyText.Text = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_POPUP_FORGOT_REWARDS_BODY", dataModel.Unclaimed);
		m_widget.Show();
	}

	public void Hide()
	{
		m_widget.GetComponentInParent<RewardTrackSeasonRoll>();
		m_widget.Hide();
	}
}
