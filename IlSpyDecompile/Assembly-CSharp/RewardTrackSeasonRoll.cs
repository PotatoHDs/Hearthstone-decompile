using System;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.Progression;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class RewardTrackSeasonRoll : MonoBehaviour
{
	public static readonly AssetReference REWARD_TRACK_SEASON_ROLL_PREFAB = new AssetReference("RewardTrackSeasonRoll.prefab:896a446794e9b334d937e067e63613b0");

	private const string CODE_HIDE_AUTO_CLAIMED_REWARDS_POPUP = "CODE_HIDE_AUTO_CLAIMED_REWARDS_POPUP";

	private const string CODE_DISMISS = "CODE_DISMISS";

	public Widget m_forgotRewardsPopupWidget;

	public Widget m_chooseOneItemPopupWidget;

	private Widget m_widget;

	private GameObject m_owner;

	private Action m_callback;

	private RewardTrackUnclaimedRewards m_rewardTrackUnclaimedNotification;

	private RewardTrackDataModel m_rewardTrackDataModel = new RewardTrackDataModel();

	private Queue<RewardTrackNodeRewardsDataModel> m_unclaimedRewardTrackNodeDataModels = new Queue<RewardTrackNodeRewardsDataModel>();

	private bool m_hasPaidTrackUnlocked;

	private void Awake()
	{
		m_widget = GetComponent<WidgetTemplate>();
		m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "CODE_HIDE_AUTO_CLAIMED_REWARDS_POPUP")
			{
				ShowChooseOneRewardPickerPopup();
			}
			else if (eventName == "CODE_DISMISS")
			{
				Hide();
			}
		});
		m_owner = base.gameObject;
		if (base.transform.parent != null && base.transform.parent.GetComponent<WidgetInstance>() != null)
		{
			m_owner = base.transform.parent.gameObject;
		}
	}

	public void Initialize(Action callback, RewardTrackUnclaimedRewards rewardTrackUnclaimedRewards)
	{
		m_callback = callback;
		m_rewardTrackUnclaimedNotification = rewardTrackUnclaimedRewards;
		RewardTrackDbfRecord record = GameDbf.RewardTrack.GetRecord(rewardTrackUnclaimedRewards.RewardTrackId);
		m_hasPaidTrackUnlocked = AccountLicenseMgr.Get().OwnsAccountLicense(record?.AccountLicenseRecord?.LicenseId ?? 0);
		m_rewardTrackDataModel.RewardTrackId = rewardTrackUnclaimedRewards.RewardTrackId;
		m_rewardTrackDataModel.Level = int.MaxValue;
		foreach (PlayerRewardTrackLevelState item in rewardTrackUnclaimedRewards.UnclaimedLevel)
		{
			HandleUnclaimedRewardTracklevel(item, rewardTrackUnclaimedRewards.RewardTrackId, forPaidTrack: false);
			HandleUnclaimedRewardTracklevel(item, rewardTrackUnclaimedRewards.RewardTrackId, forPaidTrack: true);
		}
		m_widget.BindDataModel(m_rewardTrackDataModel);
		m_forgotRewardsPopupWidget.Hide();
	}

	public void Show()
	{
		if (m_rewardTrackDataModel.Unclaimed == 0)
		{
			Hide();
			return;
		}
		OverlayUI.Get().AddGameObject(base.transform.parent.gameObject);
		UIContext.GetRoot().RegisterPopup(m_owner, UIContext.RenderCameraType.OrthographicUI);
		m_forgotRewardsPopupWidget.RegisterDoneChangingStatesListener(delegate
		{
			m_widget.GetComponentInChildren<RewardTrackForgotRewardsPopup>().Show();
		}, null, callImmediatelyIfSet: true, doOnce: true);
	}

	public void ShowChooseOneRewardPickerPopup()
	{
		if (m_unclaimedRewardTrackNodeDataModels.Count == 0)
		{
			Hide();
			return;
		}
		m_chooseOneItemPopupWidget.gameObject.SetActive(value: true);
		RewardTrackNodeRewardsDataModel rewardTrackNodeRewardsDataModel = m_unclaimedRewardTrackNodeDataModels.Dequeue();
		m_chooseOneItemPopupWidget.BindDataModel(rewardTrackNodeRewardsDataModel);
		m_chooseOneItemPopupWidget.BindDataModel(rewardTrackNodeRewardsDataModel.Items);
		m_chooseOneItemPopupWidget.RegisterDoneChangingStatesListener(delegate
		{
			m_widget.GetComponentInChildren<RewardTrackForgotRewardsPopup>().Show();
		}, null, callImmediatelyIfSet: true, doOnce: true);
	}

	public void Hide()
	{
		m_widget.Hide();
		UnityEngine.Object.Destroy(m_owner);
	}

	private void OnDestroy()
	{
		UIContext.GetRoot()?.UnregisterPopup(m_owner);
		m_callback?.Invoke();
	}

	private void HandleUnclaimedRewardTracklevel(PlayerRewardTrackLevelState levelState, int rewardTrackId, bool forPaidTrack)
	{
		if ((!m_hasPaidTrackUnlocked && forPaidTrack) || ProgressUtils.HasClaimedRewardTrackReward((RewardTrackManager.RewardStatus)(forPaidTrack ? levelState.PaidRewardStatus : levelState.FreeRewardStatus)))
		{
			return;
		}
		RewardTrackLevelDbfRecord rewardTrackLevelDbfRecord = GameDbf.RewardTrack.GetRecord(rewardTrackId)?.Levels.Find((RewardTrackLevelDbfRecord r) => r.Level == levelState.Level);
		if (rewardTrackLevelDbfRecord == null)
		{
			Debug.LogError($"Reward track level asset not found for track id {rewardTrackId} level {levelState.Level}");
			return;
		}
		RewardListDbfRecord rewardListDbfRecord = (forPaidTrack ? rewardTrackLevelDbfRecord.PaidRewardListRecord : rewardTrackLevelDbfRecord.FreeRewardListRecord);
		if (rewardListDbfRecord == null)
		{
			return;
		}
		if (rewardListDbfRecord.ChooseOne)
		{
			RewardTrackNodeRewardsDataModel item = RewardTrackFactory.CreateRewardTrackNodeRewardsDataModel(rewardListDbfRecord, m_rewardTrackDataModel, forPaidTrack, levelState);
			m_unclaimedRewardTrackNodeDataModels.Enqueue(item);
			return;
		}
		foreach (RewardItemDbfRecord rewardItem in rewardListDbfRecord.RewardItems)
		{
			if (rewardItem.RewardType != RewardItem.RewardType.REWARD_TRACK_XP_BOOST)
			{
				m_rewardTrackDataModel.Unclaimed++;
			}
		}
	}

	public static void DebugShowFakeForgotTrackRewards()
	{
		Widget widget = WidgetInstance.Create(REWARD_TRACK_SEASON_ROLL_PREFAB);
		widget.RegisterReadyListener(delegate
		{
			RewardTrackSeasonRoll componentInChildren = widget.GetComponentInChildren<RewardTrackSeasonRoll>();
			RewardTrackUnclaimedRewards rewardTrackUnclaimedRewards = new RewardTrackUnclaimedRewards
			{
				RewardTrackId = 2
			};
			PlayerRewardTrackLevelState item = new PlayerRewardTrackLevelState
			{
				Level = 50,
				FreeRewardStatus = 0,
				PaidRewardStatus = 2
			};
			rewardTrackUnclaimedRewards.UnclaimedLevel.Add(item);
			componentInChildren.Initialize(null, rewardTrackUnclaimedRewards);
			componentInChildren.Show();
		});
	}
}
