using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000624 RID: 1572
[RequireComponent(typeof(WidgetTemplate))]
public class RewardTrackForgotRewardsPopup : MonoBehaviour
{
	// Token: 0x06005819 RID: 22553 RVA: 0x001CCCB5 File Offset: 0x001CAEB5
	private void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == this.CODE_HIDE)
			{
				this.Hide();
			}
		});
	}

	// Token: 0x0600581A RID: 22554 RVA: 0x001CCCDC File Offset: 0x001CAEDC
	public void Show()
	{
		RewardTrackDataModel dataModel = this.m_widget.GetDataModel<RewardTrackDataModel>();
		if (dataModel == null)
		{
			Debug.LogWarning("Unexpected state: no bound RewardTrackDataModel");
			return;
		}
		GameStrings.PluralNumber[] pluralNumbers = new GameStrings.PluralNumber[]
		{
			new GameStrings.PluralNumber
			{
				m_index = 0,
				m_number = dataModel.Unclaimed
			}
		};
		this.m_headerText.Text = GameStrings.FormatPlurals("GLUE_PROGRESSION_REWARD_TRACK_POPUP_FORGOT_REWARDS_TITLE", pluralNumbers, Array.Empty<object>());
		this.m_bodyText.Text = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_POPUP_FORGOT_REWARDS_BODY", new object[]
		{
			dataModel.Unclaimed
		});
		this.m_widget.Show();
	}

	// Token: 0x0600581B RID: 22555 RVA: 0x001CCD74 File Offset: 0x001CAF74
	public void Hide()
	{
		this.m_widget.GetComponentInParent<RewardTrackSeasonRoll>();
		this.m_widget.Hide();
	}

	// Token: 0x04004B90 RID: 19344
	public UberText m_headerText;

	// Token: 0x04004B91 RID: 19345
	public UberText m_bodyText;

	// Token: 0x04004B92 RID: 19346
	private Widget m_widget;

	// Token: 0x04004B93 RID: 19347
	private readonly string CODE_HIDE = "CODE_HIDE";
}
