using System.Linq;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class RewardTrackDisplay : MonoBehaviour
	{
		public const string TURN_PAGE_LEFT = "CODE_TURN_PAGE_LEFT";

		public const string TURN_PAGE_RIGHT = "CODE_TURN_PAGE_RIGHT";

		public const string TURN_ANIM_FINISHED = "CODE_TURN_ANIM_FINISHED";

		public const string SHOW_TAVERN_PASS = "CODE_SHOW_TAVERN_PASS";

		public const string NEXT_LEFT_PAGE_FINISHED = "CODE_NEXT_LEFT_PAGE_FINISHED";

		public const string NEXT_RIGHT_PAGE_FINISHED = "CODE_NEXT_RIGHT_PAGE_FINISHED";

		[SerializeField]
		private WidgetInstance m_mapSegment1;

		[SerializeField]
		private WidgetInstance m_mapSegment2;

		[SerializeField]
		private int m_numberOfItemsPerPage = 5;

		private WidgetTemplate m_widget;

		private WidgetInstance m_activePageInstance;

		private bool m_isChangingPage;

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
				if (CurrentPageNumber > 0 && num != CurrentPageNumber)
				{
					TurnToPage(num);
				}
			}
		}

		public float WidgetTransformWidth
		{
			get
			{
				WidgetTransform component = GetComponent<WidgetTransform>();
				if (component == null)
				{
					return 0f;
				}
				return (base.transform.localToWorldMatrix * component.Rect.size).x;
			}
		}

		private WidgetInstance NextPageInstance
		{
			get
			{
				if (m_activePageInstance == m_mapSegment1)
				{
					return m_mapSegment2;
				}
				return m_mapSegment1;
			}
		}

		private void Awake()
		{
			if (m_mapSegment1 == null || m_mapSegment2 == null)
			{
				Debug.LogError("RewardTrackDisplay: Map Instances not set and the reward track will not load.");
				return;
			}
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.BindDataModel(RewardTrackManager.Get().TrackDataModel);
			m_widget.BindDataModel(RewardTrackManager.Get().PageDataModel);
			m_widget.RegisterEventListener(delegate(string eventName)
			{
				switch (eventName)
				{
				case "CODE_TURN_PAGE_LEFT":
					CurrentPageNumber--;
					break;
				case "CODE_TURN_PAGE_RIGHT":
					CurrentPageNumber++;
					break;
				case "CODE_TURN_ANIM_FINISHED":
					m_activePageInstance = NextPageInstance;
					NextPageInstance.gameObject.SetActive(value: false);
					m_isChangingPage = false;
					break;
				case "CODE_SHOW_TAVERN_PASS":
					if (Network.IsLoggedIn())
					{
						Shop.OpenToTavernPassPageWhenReady();
					}
					else
					{
						ProgressUtils.ShowOfflinePopup();
					}
					break;
				}
			});
			m_numberOfItemsPerPage = Mathf.Max(1, m_numberOfItemsPerPage);
			m_activePageInstance = m_mapSegment1;
		}

		private void OnEnable()
		{
			int trackLevel = RewardTrackManager.Get().TrackDataModel.Level;
			int levelCap = RewardTrackManager.Get().RewardTrackAsset.LevelCapSoft;
			int num = (from record in RewardTrackManager.Get().RewardTrackAsset.Levels.Where(delegate(RewardTrackLevelDbfRecord record)
				{
					bool num2 = record.Level <= levelCap;
					bool flag = record.Level <= trackLevel;
					bool flag2 = RewardTrackManager.Get().HasUnclaimedRewardsForLevel(record);
					return num2 && flag && flag2;
				})
				orderby record.Level
				select record).FirstOrDefault()?.Level ?? Mathf.Min(trackLevel + 1, levelCap);
			SetPageData(Mathf.CeilToInt((float)num / (float)m_numberOfItemsPerPage), m_activePageInstance);
		}

		private void SetPageData(int pageNumber, WidgetInstance widgetInstance)
		{
			RewardTrackManager.Get().SetRewardTrackNodePage(pageNumber, m_numberOfItemsPerPage);
			widgetInstance.BindDataModel(RewardTrackManager.Get().NodesDataModel.CloneDataModel());
			widgetInstance.BindDataModel(RewardTrackManager.Get().PageDataModel.CloneDataModel());
		}

		private void TurnToPage(int pageNumber)
		{
			if (!m_isChangingPage)
			{
				m_isChangingPage = true;
				WidgetInstance activePageInstance = m_activePageInstance;
				WidgetInstance nextPageInstance = NextPageInstance;
				int delta = pageNumber - CurrentPageNumber;
				float num = ((delta >= 0) ? 1 : (-1));
				Vector3 position = activePageInstance.transform.position;
				Vector3 position2 = nextPageInstance.transform.position;
				position2.x = position.x + num * WidgetTransformWidth;
				nextPageInstance.transform.position = position2;
				nextPageInstance.gameObject.SetActive(value: true);
				nextPageInstance.RegisterDoneChangingStatesListener(delegate
				{
					string eventName = ((delta < 0) ? "CODE_NEXT_LEFT_PAGE_FINISHED" : "CODE_NEXT_RIGHT_PAGE_FINISHED");
					m_widget.TriggerEvent(eventName, new Widget.TriggerEventParameters
					{
						NoDownwardPropagation = true
					});
					nextPageInstance.Show();
				}, null, callImmediatelyIfSet: false, doOnce: true);
				nextPageInstance.Hide();
				SetPageData(pageNumber, nextPageInstance);
			}
		}
	}
}
