using System;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x0200111F RID: 4383
	[RequireComponent(typeof(WidgetTemplate))]
	public class RewardTrackChooseOneItemPopup : MonoBehaviour
	{
		// Token: 0x0600C013 RID: 49171 RVA: 0x003A851C File Offset: 0x003A671C
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.BindDataModel(this.m_pageInfo, false);
			this.m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_TURN_PAGE_LEFT")
				{
					this.SetPageData(this.m_pageInfo.PageNumber - 1);
					return;
				}
				if (eventName == "CODE_TURN_PAGE_RIGHT")
				{
					this.SetPageData(this.m_pageInfo.PageNumber + 1);
					return;
				}
				if (eventName == "FROM_ACHIEVE")
				{
					this.m_rewardListDataModel = this.m_widget.GetDataModel<AchievementDataModel>().RewardList.CloneDataModel<RewardListDataModel>();
					this.SetPageData(1);
					return;
				}
				if (!(eventName == "FROM_TRACK"))
				{
					return;
				}
				this.m_rewardListDataModel = this.m_widget.GetDataModel<RewardTrackNodeRewardsDataModel>().Items.CloneDataModel<RewardListDataModel>();
				this.SetPageData(1);
			});
			this.m_numberOfItemsPerPage = Mathf.Max(1, this.m_numberOfItemsPerPage);
		}

		// Token: 0x0600C014 RID: 49172 RVA: 0x003A8570 File Offset: 0x003A6770
		private void SetPageData(int pageNumber)
		{
			if (this.m_rewardListDataModel == null)
			{
				Debug.LogError("RewardTrackChooseOneItemPopup.SetPageData: RewardTrackNodeRewardsDataModel was not bound!");
				return;
			}
			this.m_pageInfo.ItemsPerPage = this.m_numberOfItemsPerPage;
			this.m_pageInfo.TotalPages = Mathf.CeilToInt((float)this.m_rewardListDataModel.Items.Count / (float)this.m_numberOfItemsPerPage);
			this.m_pageInfo.PageNumber = Mathf.Clamp(pageNumber, 1, this.m_pageInfo.TotalPages);
			if (this.m_pageInfo.PageNumber != pageNumber)
			{
				return;
			}
			int startIndex = this.m_numberOfItemsPerPage * (pageNumber - 1);
			int endIndex = startIndex + this.m_numberOfItemsPerPage - 1;
			RewardListDataModel rewardListDataModel = new RewardListDataModel();
			rewardListDataModel.Items = this.m_rewardListDataModel.Items.Where((RewardItemDataModel item, int index) => index >= startIndex && index <= endIndex).ToDataModelList<RewardItemDataModel>();
			int count = this.m_numberOfItemsPerPage - rewardListDataModel.Items.Count;
			rewardListDataModel.Items.AddRange(Enumerable.Repeat<RewardItemDataModel>(new RewardItemDataModel(), count).ToArray<RewardItemDataModel>());
			this.m_widget.BindDataModel(rewardListDataModel, false);
		}

		// Token: 0x04009BC2 RID: 39874
		public const string TURN_PAGE_LEFT = "CODE_TURN_PAGE_LEFT";

		// Token: 0x04009BC3 RID: 39875
		public const string TURN_PAGE_RIGHT = "CODE_TURN_PAGE_RIGHT";

		// Token: 0x04009BC4 RID: 39876
		public const string FROM_ACHIEVE = "FROM_ACHIEVE";

		// Token: 0x04009BC5 RID: 39877
		public const string FROM_TRACK = "FROM_TRACK";

		// Token: 0x04009BC6 RID: 39878
		[SerializeField]
		private int m_numberOfItemsPerPage = 4;

		// Token: 0x04009BC7 RID: 39879
		private WidgetTemplate m_widget;

		// Token: 0x04009BC8 RID: 39880
		private PageInfoDataModel m_pageInfo = new PageInfoDataModel();

		// Token: 0x04009BC9 RID: 39881
		private RewardListDataModel m_rewardListDataModel;
	}
}
