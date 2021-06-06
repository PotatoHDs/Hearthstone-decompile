using System;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.Progression;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000627 RID: 1575
[RequireComponent(typeof(WidgetTemplate))]
public class RewardTrackSeasonRoll : MonoBehaviour
{
	// Token: 0x06005826 RID: 22566 RVA: 0x001CCF44 File Offset: 0x001CB144
	private void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "CODE_HIDE_AUTO_CLAIMED_REWARDS_POPUP")
			{
				this.ShowChooseOneRewardPickerPopup();
				return;
			}
			if (eventName == "CODE_DISMISS")
			{
				this.Hide();
			}
		});
		this.m_owner = base.gameObject;
		if (base.transform.parent != null && base.transform.parent.GetComponent<WidgetInstance>() != null)
		{
			this.m_owner = base.transform.parent.gameObject;
		}
	}

	// Token: 0x06005827 RID: 22567 RVA: 0x001CCFC4 File Offset: 0x001CB1C4
	public void Initialize(Action callback, RewardTrackUnclaimedRewards rewardTrackUnclaimedRewards)
	{
		this.m_callback = callback;
		this.m_rewardTrackUnclaimedNotification = rewardTrackUnclaimedRewards;
		RewardTrackDbfRecord record = GameDbf.RewardTrack.GetRecord(rewardTrackUnclaimedRewards.RewardTrackId);
		AccountLicenseMgr accountLicenseMgr = AccountLicenseMgr.Get();
		long? num;
		if (record == null)
		{
			num = null;
		}
		else
		{
			AccountLicenseDbfRecord accountLicenseRecord = record.AccountLicenseRecord;
			num = ((accountLicenseRecord != null) ? new long?(accountLicenseRecord.LicenseId) : null);
		}
		this.m_hasPaidTrackUnlocked = accountLicenseMgr.OwnsAccountLicense(num ?? 0L);
		this.m_rewardTrackDataModel.RewardTrackId = rewardTrackUnclaimedRewards.RewardTrackId;
		this.m_rewardTrackDataModel.Level = int.MaxValue;
		foreach (PlayerRewardTrackLevelState levelState in rewardTrackUnclaimedRewards.UnclaimedLevel)
		{
			this.HandleUnclaimedRewardTracklevel(levelState, rewardTrackUnclaimedRewards.RewardTrackId, false);
			this.HandleUnclaimedRewardTracklevel(levelState, rewardTrackUnclaimedRewards.RewardTrackId, true);
		}
		this.m_widget.BindDataModel(this.m_rewardTrackDataModel, false);
		this.m_forgotRewardsPopupWidget.Hide();
	}

	// Token: 0x06005828 RID: 22568 RVA: 0x001CD0E0 File Offset: 0x001CB2E0
	public void Show()
	{
		if (this.m_rewardTrackDataModel.Unclaimed == 0)
		{
			this.Hide();
			return;
		}
		OverlayUI.Get().AddGameObject(base.transform.parent.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		UIContext.GetRoot().RegisterPopup(this.m_owner, UIContext.RenderCameraType.OrthographicUI, UIContext.BlurType.Standard);
		this.m_forgotRewardsPopupWidget.RegisterDoneChangingStatesListener(delegate(object _)
		{
			this.m_widget.GetComponentInChildren<RewardTrackForgotRewardsPopup>().Show();
		}, null, true, true);
	}

	// Token: 0x06005829 RID: 22569 RVA: 0x001CD14C File Offset: 0x001CB34C
	public void ShowChooseOneRewardPickerPopup()
	{
		if (this.m_unclaimedRewardTrackNodeDataModels.Count == 0)
		{
			this.Hide();
			return;
		}
		this.m_chooseOneItemPopupWidget.gameObject.SetActive(true);
		RewardTrackNodeRewardsDataModel rewardTrackNodeRewardsDataModel = this.m_unclaimedRewardTrackNodeDataModels.Dequeue();
		this.m_chooseOneItemPopupWidget.BindDataModel(rewardTrackNodeRewardsDataModel, false);
		this.m_chooseOneItemPopupWidget.BindDataModel(rewardTrackNodeRewardsDataModel.Items, false);
		this.m_chooseOneItemPopupWidget.RegisterDoneChangingStatesListener(delegate(object _)
		{
			this.m_widget.GetComponentInChildren<RewardTrackForgotRewardsPopup>().Show();
		}, null, true, true);
	}

	// Token: 0x0600582A RID: 22570 RVA: 0x001CD1C3 File Offset: 0x001CB3C3
	public void Hide()
	{
		this.m_widget.Hide();
		UnityEngine.Object.Destroy(this.m_owner);
	}

	// Token: 0x0600582B RID: 22571 RVA: 0x001CD1DB File Offset: 0x001CB3DB
	private void OnDestroy()
	{
		UIContext root = UIContext.GetRoot();
		if (root != null)
		{
			root.UnregisterPopup(this.m_owner);
		}
		Action callback = this.m_callback;
		if (callback == null)
		{
			return;
		}
		callback();
	}

	// Token: 0x0600582C RID: 22572 RVA: 0x001CD204 File Offset: 0x001CB404
	private void HandleUnclaimedRewardTracklevel(PlayerRewardTrackLevelState levelState, int rewardTrackId, bool forPaidTrack)
	{
		if (!this.m_hasPaidTrackUnlocked && forPaidTrack)
		{
			return;
		}
		if (ProgressUtils.HasClaimedRewardTrackReward((RewardTrackManager.RewardStatus)(forPaidTrack ? levelState.PaidRewardStatus : levelState.FreeRewardStatus)))
		{
			return;
		}
		RewardTrackDbfRecord record = GameDbf.RewardTrack.GetRecord(rewardTrackId);
		RewardTrackLevelDbfRecord rewardTrackLevelDbfRecord = (record != null) ? record.Levels.Find((RewardTrackLevelDbfRecord r) => r.Level == levelState.Level) : null;
		if (rewardTrackLevelDbfRecord == null)
		{
			Debug.LogError(string.Format("Reward track level asset not found for track id {0} level {1}", rewardTrackId, levelState.Level));
			return;
		}
		RewardListDbfRecord rewardListDbfRecord = forPaidTrack ? rewardTrackLevelDbfRecord.PaidRewardListRecord : rewardTrackLevelDbfRecord.FreeRewardListRecord;
		if (rewardListDbfRecord == null)
		{
			return;
		}
		if (rewardListDbfRecord.ChooseOne)
		{
			RewardTrackNodeRewardsDataModel item = RewardTrackFactory.CreateRewardTrackNodeRewardsDataModel(rewardListDbfRecord, this.m_rewardTrackDataModel, forPaidTrack, levelState);
			this.m_unclaimedRewardTrackNodeDataModels.Enqueue(item);
			return;
		}
		using (List<RewardItemDbfRecord>.Enumerator enumerator = rewardListDbfRecord.RewardItems.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.RewardType != RewardItem.RewardType.REWARD_TRACK_XP_BOOST)
				{
					RewardTrackDataModel rewardTrackDataModel = this.m_rewardTrackDataModel;
					int unclaimed = rewardTrackDataModel.Unclaimed + 1;
					rewardTrackDataModel.Unclaimed = unclaimed;
				}
			}
		}
	}

	// Token: 0x0600582D RID: 22573 RVA: 0x001CD340 File Offset: 0x001CB540
	public static void DebugShowFakeForgotTrackRewards()
	{
		Widget widget = WidgetInstance.Create(RewardTrackSeasonRoll.REWARD_TRACK_SEASON_ROLL_PREFAB, false);
		widget.RegisterReadyListener(delegate(object _)
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
		}, null, true);
	}

	// Token: 0x04004B99 RID: 19353
	public static readonly AssetReference REWARD_TRACK_SEASON_ROLL_PREFAB = new AssetReference("RewardTrackSeasonRoll.prefab:896a446794e9b334d937e067e63613b0");

	// Token: 0x04004B9A RID: 19354
	private const string CODE_HIDE_AUTO_CLAIMED_REWARDS_POPUP = "CODE_HIDE_AUTO_CLAIMED_REWARDS_POPUP";

	// Token: 0x04004B9B RID: 19355
	private const string CODE_DISMISS = "CODE_DISMISS";

	// Token: 0x04004B9C RID: 19356
	public Widget m_forgotRewardsPopupWidget;

	// Token: 0x04004B9D RID: 19357
	public Widget m_chooseOneItemPopupWidget;

	// Token: 0x04004B9E RID: 19358
	private Widget m_widget;

	// Token: 0x04004B9F RID: 19359
	private GameObject m_owner;

	// Token: 0x04004BA0 RID: 19360
	private Action m_callback;

	// Token: 0x04004BA1 RID: 19361
	private RewardTrackUnclaimedRewards m_rewardTrackUnclaimedNotification;

	// Token: 0x04004BA2 RID: 19362
	private RewardTrackDataModel m_rewardTrackDataModel = new RewardTrackDataModel();

	// Token: 0x04004BA3 RID: 19363
	private Queue<RewardTrackNodeRewardsDataModel> m_unclaimedRewardTrackNodeDataModels = new Queue<RewardTrackNodeRewardsDataModel>();

	// Token: 0x04004BA4 RID: 19364
	private bool m_hasPaidTrackUnlocked;
}
