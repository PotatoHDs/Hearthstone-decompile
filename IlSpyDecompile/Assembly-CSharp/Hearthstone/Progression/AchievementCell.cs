using System;
using System.Collections;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class AchievementCell : MonoBehaviour
	{
		public const string CLAIM_ACHIEVEMENT = "CODE_CLAIM_ACHIEVEMENT";

		public const string SECTION_CHANGED = "CODE_SECTION_CHANGED";

		public const string ACHIEVEMENT_CHANGED = "CODE_ACHIEVEMENT_CHANGED";

		public const string CLAIM_RESPONSE_RECEIVED = "CODE_CLAIM_RESPONSE_RECEIVED";

		public const string CLAIM_ANIMATION_COMPLETED = "CODE_CLAIM_ANIMATION_COMPLETED";

		public const string HIDE_ACHIEVEMENT_TILE = "CODE_HIDE_ACHIEVEMENT_TILE";

		public const string SHOW_ACHIEVEMENT_TILE = "CODE_SHOW_ACHIEVEMENT_TILE";

		[Tooltip("Per-achievement delay to stagger the appearance of achievements within the list.")]
		public float m_streamDelay = 0.08f;

		[Tooltip("Max number of achievements to delay before revealing them all.")]
		public int m_streamMax = 10;

		private Widget m_widget;

		private bool m_waitingForClaimResponse;

		private bool m_waitingForClaimAnimation;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterEventListener(HandleEvent);
			HideTile();
			AchievementSection componentInParent = GetComponentInParent<AchievementSection>();
			if (componentInParent != null)
			{
				componentInParent.OnSectionChanged += HandleSectionChanged;
				componentInParent.OnSectionShown += HandleSectionShown;
			}
		}

		private void OnDestroy()
		{
			StopAllCoroutines();
			m_waitingForClaimResponse = false;
			m_waitingForClaimAnimation = false;
			AchievementSection componentInParent = GetComponentInParent<AchievementSection>();
			if (componentInParent != null)
			{
				componentInParent.OnSectionChanged -= HandleSectionChanged;
				componentInParent.OnSectionShown -= HandleSectionShown;
			}
		}

		private void HandleEvent(string eventName)
		{
			switch (eventName)
			{
			case "CODE_CLAIM_ACHIEVEMENT":
				HandleClaimAchievement();
				break;
			case "CODE_ACHIEVEMENT_CHANGED":
				HandleCellAchievementChanged();
				break;
			case "CODE_CLAIM_RESPONSE_RECEIVED":
				HandleClaimResponseReceived();
				break;
			case "CODE_CLAIM_ANIMATION_COMPLETED":
				HandleClaimAnimationCompleted();
				break;
			}
		}

		private void HandleClaimAchievement()
		{
			if (!Network.IsLoggedIn())
			{
				ProgressUtils.ShowOfflinePopup();
				return;
			}
			AchievementDataModel dataModel = m_widget.GetDataModel<AchievementDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound achievement.");
			}
			else
			{
				StartClaimSequence(dataModel.ID);
			}
		}

		private void StartClaimSequence(int achievementId)
		{
			if (AchievementManager.Get().ClaimAchievementReward(achievementId))
			{
				m_waitingForClaimResponse = true;
				m_waitingForClaimAnimation = true;
			}
		}

		private void HandleSectionChanged(AchievementSectionDataModel section)
		{
			StopAllCoroutines();
			m_waitingForClaimResponse = false;
			m_waitingForClaimAnimation = false;
			HideTile();
		}

		private void HandleSectionShown(AchievementSectionDataModel section)
		{
			StartCoroutine(WaitAndShowTile(ComputeAchievementDelay(section)));
		}

		private void HandleCellAchievementChanged()
		{
			m_waitingForClaimResponse = false;
			UpdateIfReady();
		}

		private void HandleClaimResponseReceived()
		{
			m_waitingForClaimResponse = false;
			UpdateIfReady();
		}

		private void HandleClaimAnimationCompleted()
		{
			m_waitingForClaimAnimation = false;
			UpdateIfReady();
		}

		private void UpdateIfReady()
		{
			if (!m_waitingForClaimResponse && !m_waitingForClaimAnimation)
			{
				UpdateTile();
			}
		}

		private void UpdateTile()
		{
			AchievementTile componentInChildren = GetComponentInChildren<AchievementTile>();
			if (componentInChildren == null)
			{
				Debug.LogWarning("Unexpected state: achievement tile not found");
				return;
			}
			AchievementDataModel dataModel = m_widget.GetDataModel<AchievementDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound achievement");
			}
			else
			{
				componentInChildren.BindDataModel(dataModel.CloneDataModel());
			}
		}

		private void HideTile()
		{
			m_widget.TriggerEvent("CODE_HIDE_ACHIEVEMENT_TILE");
		}

		private IEnumerator WaitAndShowTile(float delay)
		{
			yield return new WaitForSeconds(delay);
			m_widget.TriggerEvent("CODE_SHOW_ACHIEVEMENT_TILE");
		}

		private float ComputeAchievementDelay(AchievementSectionDataModel section)
		{
			AchievementDataModel dataModel = m_widget.GetDataModel<AchievementDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound achievement");
				return 0f;
			}
			int num = Math.Min(section.PreviousTileCount + section.GetCurrentSortedAchievements().IndexOf(dataModel), m_streamMax);
			return (float)Mathf.Max(0, num - section.PreviousTileCount) * m_streamDelay;
		}
	}
}
