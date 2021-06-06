using System;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001120 RID: 4384
	[RequireComponent(typeof(WidgetTemplate))]
	public class RewardTrackDisplay : MonoBehaviour
	{
		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x0600C018 RID: 49176 RVA: 0x003A878F File Offset: 0x003A698F
		// (set) Token: 0x0600C017 RID: 49175 RVA: 0x003A8754 File Offset: 0x003A6954
		[Overridable]
		public int CurrentPageNumber
		{
			get
			{
				return RewardTrackManager.Get().CurrentPageNumber;
			}
			set
			{
				int totalPages = RewardTrackManager.Get().TotalPages;
				int num = Mathf.Clamp(value, 1, totalPages);
				if (this.CurrentPageNumber <= 0 || num == this.CurrentPageNumber)
				{
					return;
				}
				this.TurnToPage(num);
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x0600C019 RID: 49177 RVA: 0x003A879C File Offset: 0x003A699C
		public float WidgetTransformWidth
		{
			get
			{
				WidgetTransform component = base.GetComponent<WidgetTransform>();
				if (component == null)
				{
					return 0f;
				}
				return (base.transform.localToWorldMatrix * component.Rect.size).x;
			}
		}

		// Token: 0x0600C01A RID: 49178 RVA: 0x003A87E8 File Offset: 0x003A69E8
		private void Awake()
		{
			if (this.m_mapSegment1 == null || this.m_mapSegment2 == null)
			{
				Debug.LogError("RewardTrackDisplay: Map Instances not set and the reward track will not load.");
				return;
			}
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.BindDataModel(RewardTrackManager.Get().TrackDataModel, false);
			this.m_widget.BindDataModel(RewardTrackManager.Get().PageDataModel, false);
			this.m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_TURN_PAGE_LEFT")
				{
					this.CurrentPageNumber--;
					return;
				}
				if (eventName == "CODE_TURN_PAGE_RIGHT")
				{
					this.CurrentPageNumber++;
					return;
				}
				if (eventName == "CODE_TURN_ANIM_FINISHED")
				{
					this.m_activePageInstance = this.NextPageInstance;
					this.NextPageInstance.gameObject.SetActive(false);
					this.m_isChangingPage = false;
					return;
				}
				if (!(eventName == "CODE_SHOW_TAVERN_PASS"))
				{
					return;
				}
				if (Network.IsLoggedIn())
				{
					Shop.OpenToTavernPassPageWhenReady();
					return;
				}
				ProgressUtils.ShowOfflinePopup();
			});
			this.m_numberOfItemsPerPage = Mathf.Max(1, this.m_numberOfItemsPerPage);
			this.m_activePageInstance = this.m_mapSegment1;
		}

		// Token: 0x0600C01B RID: 49179 RVA: 0x003A888C File Offset: 0x003A6A8C
		private void OnEnable()
		{
			int trackLevel = RewardTrackManager.Get().TrackDataModel.Level;
			int levelCap = RewardTrackManager.Get().RewardTrackAsset.LevelCapSoft;
			RewardTrackLevelDbfRecord rewardTrackLevelDbfRecord = (from record in RewardTrackManager.Get().RewardTrackAsset.Levels.Where(delegate(RewardTrackLevelDbfRecord record)
			{
				bool flag = record.Level <= levelCap;
				bool flag2 = record.Level <= trackLevel;
				bool flag3 = RewardTrackManager.Get().HasUnclaimedRewardsForLevel(record);
				return flag && flag2 && flag3;
			})
			orderby record.Level
			select record).FirstOrDefault<RewardTrackLevelDbfRecord>();
			int num = (rewardTrackLevelDbfRecord != null) ? rewardTrackLevelDbfRecord.Level : Mathf.Min(trackLevel + 1, levelCap);
			this.SetPageData(Mathf.CeilToInt((float)num / (float)this.m_numberOfItemsPerPage), this.m_activePageInstance);
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x0600C01C RID: 49180 RVA: 0x003A894D File Offset: 0x003A6B4D
		private WidgetInstance NextPageInstance
		{
			get
			{
				if (this.m_activePageInstance == this.m_mapSegment1)
				{
					return this.m_mapSegment2;
				}
				return this.m_mapSegment1;
			}
		}

		// Token: 0x0600C01D RID: 49181 RVA: 0x003A896F File Offset: 0x003A6B6F
		private void SetPageData(int pageNumber, WidgetInstance widgetInstance)
		{
			RewardTrackManager.Get().SetRewardTrackNodePage(pageNumber, this.m_numberOfItemsPerPage);
			widgetInstance.BindDataModel(RewardTrackManager.Get().NodesDataModel.CloneDataModel<RewardTrackNodeListDataModel>(), false);
			widgetInstance.BindDataModel(RewardTrackManager.Get().PageDataModel.CloneDataModel<PageInfoDataModel>(), false);
		}

		// Token: 0x0600C01E RID: 49182 RVA: 0x003A89B0 File Offset: 0x003A6BB0
		private void TurnToPage(int pageNumber)
		{
			if (this.m_isChangingPage)
			{
				return;
			}
			this.m_isChangingPage = true;
			Component activePageInstance = this.m_activePageInstance;
			WidgetInstance nextPageInstance = this.NextPageInstance;
			int delta = pageNumber - this.CurrentPageNumber;
			float num = (float)((delta < 0) ? -1 : 1);
			Vector3 position = activePageInstance.transform.position;
			Vector3 position2 = nextPageInstance.transform.position;
			position2.x = position.x + num * this.WidgetTransformWidth;
			nextPageInstance.transform.position = position2;
			nextPageInstance.gameObject.SetActive(true);
			nextPageInstance.RegisterDoneChangingStatesListener(delegate(object _)
			{
				string eventName = (delta < 0) ? "CODE_NEXT_LEFT_PAGE_FINISHED" : "CODE_NEXT_RIGHT_PAGE_FINISHED";
				this.m_widget.TriggerEvent(eventName, new Widget.TriggerEventParameters
				{
					NoDownwardPropagation = true
				});
				nextPageInstance.Show();
			}, null, false, true);
			nextPageInstance.Hide();
			this.SetPageData(pageNumber, nextPageInstance);
		}

		// Token: 0x04009BCA RID: 39882
		public const string TURN_PAGE_LEFT = "CODE_TURN_PAGE_LEFT";

		// Token: 0x04009BCB RID: 39883
		public const string TURN_PAGE_RIGHT = "CODE_TURN_PAGE_RIGHT";

		// Token: 0x04009BCC RID: 39884
		public const string TURN_ANIM_FINISHED = "CODE_TURN_ANIM_FINISHED";

		// Token: 0x04009BCD RID: 39885
		public const string SHOW_TAVERN_PASS = "CODE_SHOW_TAVERN_PASS";

		// Token: 0x04009BCE RID: 39886
		public const string NEXT_LEFT_PAGE_FINISHED = "CODE_NEXT_LEFT_PAGE_FINISHED";

		// Token: 0x04009BCF RID: 39887
		public const string NEXT_RIGHT_PAGE_FINISHED = "CODE_NEXT_RIGHT_PAGE_FINISHED";

		// Token: 0x04009BD0 RID: 39888
		[SerializeField]
		private WidgetInstance m_mapSegment1;

		// Token: 0x04009BD1 RID: 39889
		[SerializeField]
		private WidgetInstance m_mapSegment2;

		// Token: 0x04009BD2 RID: 39890
		[SerializeField]
		private int m_numberOfItemsPerPage = 5;

		// Token: 0x04009BD3 RID: 39891
		private WidgetTemplate m_widget;

		// Token: 0x04009BD4 RID: 39892
		private WidgetInstance m_activePageInstance;

		// Token: 0x04009BD5 RID: 39893
		private bool m_isChangingPage;
	}
}
