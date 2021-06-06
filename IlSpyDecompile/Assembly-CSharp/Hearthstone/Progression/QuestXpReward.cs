using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using HutongGames.PlayMaker;
using PegasusUtil;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class QuestXpReward : MonoBehaviour
	{
		[SerializeField]
		private PlayMakerFSM m_fsm;

		[SerializeField]
		private Widget m_questTileWidget;

		[SerializeField]
		private AsyncReference m_xpBarMeshReference;

		[SerializeField]
		private AsyncReference m_questXpRoot;

		[SerializeField]
		private AsyncReference m_xpProgressText;

		[SerializeField]
		private AsyncReference m_levelText;

		[SerializeField]
		private AsyncReference m_xpDisplayText;

		private Widget m_widget;

		private List<RewardTrackXpChange> m_xpChanges;

		private RewardTrackDataModel m_dataModel;

		private int m_targetLevel;

		private int m_targetXp;

		private Action m_callback;

		private bool m_pauseOnNext;

		private bool m_isShowing;

		private bool m_gameXpCausedLevel;

		private bool m_isIntro;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_xpBarMeshReference.RegisterReadyListener(delegate(Transform t)
			{
				FsmGameObject fsmGameObject5 = m_fsm.FsmVariables.GetFsmGameObject("XpBarMesh");
				if (fsmGameObject5 != null)
				{
					fsmGameObject5.Value = t.gameObject;
				}
			});
			m_questXpRoot.RegisterReadyListener(delegate(Transform t)
			{
				FsmGameObject fsmGameObject4 = m_fsm.FsmVariables.GetFsmGameObject("QuestXpRoot");
				if (fsmGameObject4 != null)
				{
					fsmGameObject4.Value = t.gameObject;
				}
			});
			m_xpProgressText.RegisterReadyListener(delegate(Transform t)
			{
				FsmGameObject fsmGameObject3 = m_fsm.FsmVariables.GetFsmGameObject("XpProgressText");
				if (fsmGameObject3 != null)
				{
					fsmGameObject3.Value = t.gameObject;
				}
			});
			m_levelText.RegisterReadyListener(delegate(Transform t)
			{
				FsmGameObject fsmGameObject2 = m_fsm.FsmVariables.GetFsmGameObject("LevelText");
				if (fsmGameObject2 != null)
				{
					fsmGameObject2.Value = t.gameObject;
				}
			});
			m_xpDisplayText.RegisterReadyListener(delegate(Transform t)
			{
				FsmGameObject fsmGameObject = m_fsm.FsmVariables.GetFsmGameObject("XpDisplayText");
				if (fsmGameObject != null)
				{
					fsmGameObject.Value = t.gameObject;
				}
			});
		}

		public void Initialize(List<RewardTrackXpChange> xpChanges)
		{
			if (xpChanges.Count != 0)
			{
				m_pauseOnNext = false;
				m_isShowing = true;
				m_gameXpCausedLevel = false;
				m_isIntro = false;
				m_dataModel = RewardTrackManager.Get().TrackDataModel.CloneDataModel();
				RewardTrackDbfRecord rewardTrackAsset = RewardTrackManager.Get().RewardTrackAsset;
				m_dataModel.LevelHardCap = rewardTrackAsset?.Levels.Count ?? 0;
				m_xpChanges = (from n in xpChanges
					orderby n.CurrLevel, n.CurrXp
					select n).ToList();
				m_dataModel.Xp = m_xpChanges[0].PrevXp;
				m_dataModel.Level = m_xpChanges[0].PrevLevel;
				UpdateDataModelXpNeeded();
				UpdateDataModelXpProgress(m_dataModel.Xp);
				m_widget.BindDataModel(m_dataModel);
				UpdateXpAndLevelFsmVars();
			}
		}

		public void ShowXpGains(Action callback)
		{
			m_callback = callback;
			StartCoroutine(ShowXpGainsWhenReady());
		}

		public void ShowNextXpGain()
		{
			if (m_xpChanges.Count == 0)
			{
				if (RewardXpNotificationManager.Get().JustShowGameXp)
				{
					UpdateDataModelXpProgress(m_targetXp);
				}
				else if (m_isShowing)
				{
					Hide();
					m_isShowing = false;
				}
				else
				{
					m_callback?.Invoke();
				}
				return;
			}
			if (m_pauseOnNext)
			{
				m_pauseOnNext = false;
				return;
			}
			RewardTrackXpChange rewardTrackXpChange = m_xpChanges[0];
			bool flag = rewardTrackXpChange.RewardSourceType == 4;
			if (RewardXpNotificationManager.Get().JustShowGameXp && !flag)
			{
				UpdateDataModelXpProgress(m_targetXp);
				return;
			}
			m_xpChanges.RemoveAt(0);
			m_dataModel.Level = rewardTrackXpChange.PrevLevel;
			m_dataModel.Xp = rewardTrackXpChange.PrevXp;
			m_targetLevel = rewardTrackXpChange.CurrLevel;
			m_targetXp = rewardTrackXpChange.CurrXp;
			m_gameXpCausedLevel = flag && rewardTrackXpChange.PrevLevel != rewardTrackXpChange.CurrLevel;
			UpdateDataModelXpNeeded();
			UpdateXpAndLevelFsmVars();
			UpdateDataModelXpProgress(m_dataModel.Xp);
			if (rewardTrackXpChange.RewardSourceType == 1)
			{
				QuestDataModel questDataModel = QuestManager.Get().CreateQuestDataModelById(rewardTrackXpChange.RewardSourceId);
				questDataModel.RerollCount = 0;
				m_questTileWidget.BindDataModel(questDataModel);
				AnimateQuestTile();
			}
			else if (flag)
			{
				ShowIntroXp();
				if (m_xpChanges.Count > 0)
				{
					m_isIntro = true;
				}
				else if (RewardXpNotificationManager.Get().JustShowGameXp && m_xpChanges.Count == 0)
				{
					m_questTileWidget.TriggerEvent("DISABLE_INTERACTION");
					m_pauseOnNext = true;
				}
			}
			else
			{
				AnimateXpBar();
			}
		}

		public void ClearAndHide()
		{
			if (m_xpChanges != null && m_xpChanges.Count != 0)
			{
				m_xpChanges.Clear();
			}
			if (m_widget != null)
			{
				m_widget.Hide();
			}
			m_pauseOnNext = false;
			m_callback?.Invoke();
		}

		public void ContinueNotifications()
		{
			m_pauseOnNext = false;
			ShowNextXpGain();
		}

		public void Hide()
		{
			UpdateDataModelXpProgress(m_targetXp);
			m_fsm.SendEvent("Popup_Outro");
		}

		private IEnumerator ShowXpGainsWhenReady()
		{
			while (!m_widget.IsReady || m_widget.IsChangingStates)
			{
				yield return null;
			}
			ShowNextXpGain();
		}

		private void UpdateDataModelXpNeeded()
		{
			RewardTrackLevelDbfRecord rewardTrackLevelRecord = RewardTrackManager.Get().GetRewardTrackLevelRecord(m_dataModel.Level);
			m_dataModel.XpNeeded = rewardTrackLevelRecord?.XpNeeded ?? 0;
		}

		private void UpdateXpAndLevelFsmVars()
		{
			int num = m_dataModel.Level + 1;
			FsmInt fsmInt = m_fsm.FsmVariables.GetFsmInt("StartLevel");
			if (fsmInt != null)
			{
				fsmInt.Value = num;
			}
			FsmInt fsmInt2 = m_fsm.FsmVariables.GetFsmInt("StartXp");
			if (fsmInt2 != null)
			{
				fsmInt2.Value = m_dataModel.Xp;
			}
			FsmInt fsmInt3 = m_fsm.FsmVariables.GetFsmInt("EndLevel");
			if (fsmInt3 != null)
			{
				if (m_dataModel.Level < m_targetLevel)
				{
					fsmInt3.Value = num + 1;
				}
				else
				{
					fsmInt3.Value = num;
				}
			}
			FsmInt fsmInt4 = m_fsm.FsmVariables.GetFsmInt("EndXp");
			if (fsmInt4 != null)
			{
				if (m_dataModel.Level < m_targetLevel)
				{
					fsmInt4.Value = m_dataModel.XpNeeded;
				}
				else
				{
					fsmInt4.Value = m_targetXp;
				}
			}
			FsmInt fsmInt5 = m_fsm.FsmVariables.GetFsmInt("XpNeeded");
			if (fsmInt5 != null)
			{
				fsmInt5.Value = m_dataModel.XpNeeded;
			}
			FsmFloat fsmFloat = m_fsm.FsmVariables.GetFsmFloat("StartBarPct");
			if (fsmFloat != null)
			{
				fsmFloat.Value = (float)fsmInt2.Value / (float)fsmInt5.Value;
			}
			FsmFloat fsmFloat2 = m_fsm.FsmVariables.GetFsmFloat("EndBarPct");
			if (fsmFloat2 != null)
			{
				fsmFloat2.Value = (float)fsmInt4.Value / (float)fsmInt5.Value;
			}
		}

		private int CalculateIntroXpGained()
		{
			bool flag = m_dataModel.Level < m_targetLevel;
			int num = (flag ? m_dataModel.XpNeeded : m_targetXp) - m_dataModel.Xp;
			if (m_dataModel.Level + 1 < m_targetLevel)
			{
				for (int i = m_dataModel.Level + 1; i < m_targetLevel; i++)
				{
					num += RewardTrackManager.Get().GetRewardTrackLevelRecord(i)?.XpNeeded ?? 0;
				}
			}
			if (flag)
			{
				num += m_targetXp;
			}
			return num;
		}

		private void UpdateIntroXpVar(int xpGained)
		{
			FsmInt fsmInt = m_fsm.FsmVariables.GetFsmInt("XpGained");
			if (fsmInt != null)
			{
				fsmInt.Value = xpGained;
			}
		}

		private void AnimateQuestTile()
		{
			m_fsm.SendEvent("AnimateQuestTile");
		}

		private void AnimateXpBar()
		{
			m_fsm.SendEvent("AnimateXpBar");
		}

		private void AnimateLevelFromGameXp()
		{
			m_fsm.SendEvent("LevelFromGameXp");
		}

		private void AnimateIntroXp()
		{
			m_fsm.SendEvent("Intro");
		}

		private void ShowIntroXp()
		{
			UpdateIntroXpVar(CalculateIntroXpGained());
			AnimateIntroXp();
		}

		private void OnPlayMakerFinished()
		{
			if (m_isIntro)
			{
				m_isIntro = false;
				AnimateXpBar();
				return;
			}
			bool flag = false;
			if (m_dataModel.Level < m_targetLevel || m_dataModel.Xp < m_targetXp)
			{
				if (m_dataModel.Level < m_targetLevel)
				{
					m_dataModel.Level++;
					m_dataModel.Xp = 0;
					UpdateDataModelXpNeeded();
					if (m_dataModel.Level < m_targetLevel || m_dataModel.Xp < m_targetXp)
					{
						UpdateXpAndLevelFsmVars();
						UpdateDataModelXpProgress(m_dataModel.Xp);
						if (m_gameXpCausedLevel)
						{
							AnimateLevelFromGameXp();
						}
						else
						{
							AnimateXpBar();
						}
						flag = true;
					}
				}
				else
				{
					m_dataModel.Xp = m_targetXp;
				}
			}
			if (!flag)
			{
				ShowNextXpGain();
			}
		}

		private void UpdateDataModelXpProgress(int currentAnimatedXp)
		{
			m_dataModel.XpProgress = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_XP_PROGRESS", currentAnimatedXp, m_dataModel.XpNeeded);
		}
	}
}
