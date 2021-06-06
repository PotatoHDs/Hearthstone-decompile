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
	// Token: 0x0200111C RID: 4380
	[RequireComponent(typeof(WidgetTemplate))]
	public class QuestXpReward : MonoBehaviour
	{
		// Token: 0x0600BFE8 RID: 49128 RVA: 0x003A7738 File Offset: 0x003A5938
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_xpBarMeshReference.RegisterReadyListener<Transform>(delegate(Transform t)
			{
				FsmGameObject fsmGameObject = this.m_fsm.FsmVariables.GetFsmGameObject("XpBarMesh");
				if (fsmGameObject != null)
				{
					fsmGameObject.Value = t.gameObject;
				}
			});
			this.m_questXpRoot.RegisterReadyListener<Transform>(delegate(Transform t)
			{
				FsmGameObject fsmGameObject = this.m_fsm.FsmVariables.GetFsmGameObject("QuestXpRoot");
				if (fsmGameObject != null)
				{
					fsmGameObject.Value = t.gameObject;
				}
			});
			this.m_xpProgressText.RegisterReadyListener<Transform>(delegate(Transform t)
			{
				FsmGameObject fsmGameObject = this.m_fsm.FsmVariables.GetFsmGameObject("XpProgressText");
				if (fsmGameObject != null)
				{
					fsmGameObject.Value = t.gameObject;
				}
			});
			this.m_levelText.RegisterReadyListener<Transform>(delegate(Transform t)
			{
				FsmGameObject fsmGameObject = this.m_fsm.FsmVariables.GetFsmGameObject("LevelText");
				if (fsmGameObject != null)
				{
					fsmGameObject.Value = t.gameObject;
				}
			});
			this.m_xpDisplayText.RegisterReadyListener<Transform>(delegate(Transform t)
			{
				FsmGameObject fsmGameObject = this.m_fsm.FsmVariables.GetFsmGameObject("XpDisplayText");
				if (fsmGameObject != null)
				{
					fsmGameObject.Value = t.gameObject;
				}
			});
		}

		// Token: 0x0600BFE9 RID: 49129 RVA: 0x003A77C4 File Offset: 0x003A59C4
		public void Initialize(List<RewardTrackXpChange> xpChanges)
		{
			if (xpChanges.Count == 0)
			{
				return;
			}
			this.m_pauseOnNext = false;
			this.m_isShowing = true;
			this.m_gameXpCausedLevel = false;
			this.m_isIntro = false;
			this.m_dataModel = RewardTrackManager.Get().TrackDataModel.CloneDataModel<RewardTrackDataModel>();
			RewardTrackDbfRecord rewardTrackAsset = RewardTrackManager.Get().RewardTrackAsset;
			this.m_dataModel.LevelHardCap = ((rewardTrackAsset != null) ? rewardTrackAsset.Levels.Count : 0);
			this.m_xpChanges = (from n in xpChanges
			orderby n.CurrLevel, n.CurrXp
			select n).ToList<RewardTrackXpChange>();
			this.m_dataModel.Xp = this.m_xpChanges[0].PrevXp;
			this.m_dataModel.Level = this.m_xpChanges[0].PrevLevel;
			this.UpdateDataModelXpNeeded();
			this.UpdateDataModelXpProgress(this.m_dataModel.Xp);
			this.m_widget.BindDataModel(this.m_dataModel, false);
			this.UpdateXpAndLevelFsmVars();
		}

		// Token: 0x0600BFEA RID: 49130 RVA: 0x003A78ED File Offset: 0x003A5AED
		public void ShowXpGains(Action callback)
		{
			this.m_callback = callback;
			base.StartCoroutine(this.ShowXpGainsWhenReady());
		}

		// Token: 0x0600BFEB RID: 49131 RVA: 0x003A7904 File Offset: 0x003A5B04
		public void ShowNextXpGain()
		{
			if (this.m_xpChanges.Count == 0)
			{
				if (RewardXpNotificationManager.Get().JustShowGameXp)
				{
					this.UpdateDataModelXpProgress(this.m_targetXp);
					return;
				}
				if (this.m_isShowing)
				{
					this.Hide();
					this.m_isShowing = false;
					return;
				}
				Action callback = this.m_callback;
				if (callback == null)
				{
					return;
				}
				callback();
				return;
			}
			else
			{
				if (this.m_pauseOnNext)
				{
					this.m_pauseOnNext = false;
					return;
				}
				RewardTrackXpChange rewardTrackXpChange = this.m_xpChanges[0];
				bool flag = rewardTrackXpChange.RewardSourceType == 4;
				if (RewardXpNotificationManager.Get().JustShowGameXp && !flag)
				{
					this.UpdateDataModelXpProgress(this.m_targetXp);
					return;
				}
				this.m_xpChanges.RemoveAt(0);
				this.m_dataModel.Level = rewardTrackXpChange.PrevLevel;
				this.m_dataModel.Xp = rewardTrackXpChange.PrevXp;
				this.m_targetLevel = rewardTrackXpChange.CurrLevel;
				this.m_targetXp = rewardTrackXpChange.CurrXp;
				this.m_gameXpCausedLevel = (flag && rewardTrackXpChange.PrevLevel != rewardTrackXpChange.CurrLevel);
				this.UpdateDataModelXpNeeded();
				this.UpdateXpAndLevelFsmVars();
				this.UpdateDataModelXpProgress(this.m_dataModel.Xp);
				if (rewardTrackXpChange.RewardSourceType == 1)
				{
					QuestDataModel questDataModel = QuestManager.Get().CreateQuestDataModelById(rewardTrackXpChange.RewardSourceId);
					questDataModel.RerollCount = 0;
					this.m_questTileWidget.BindDataModel(questDataModel, false);
					this.AnimateQuestTile();
					return;
				}
				if (flag)
				{
					this.ShowIntroXp();
					if (this.m_xpChanges.Count > 0)
					{
						this.m_isIntro = true;
						return;
					}
					if (RewardXpNotificationManager.Get().JustShowGameXp && this.m_xpChanges.Count == 0)
					{
						this.m_questTileWidget.TriggerEvent("DISABLE_INTERACTION", default(Widget.TriggerEventParameters));
						this.m_pauseOnNext = true;
						return;
					}
				}
				else
				{
					this.AnimateXpBar();
				}
				return;
			}
		}

		// Token: 0x0600BFEC RID: 49132 RVA: 0x003A7AB8 File Offset: 0x003A5CB8
		public void ClearAndHide()
		{
			if (this.m_xpChanges != null && this.m_xpChanges.Count != 0)
			{
				this.m_xpChanges.Clear();
			}
			if (this.m_widget != null)
			{
				this.m_widget.Hide();
			}
			this.m_pauseOnNext = false;
			Action callback = this.m_callback;
			if (callback == null)
			{
				return;
			}
			callback();
		}

		// Token: 0x0600BFED RID: 49133 RVA: 0x003A7B15 File Offset: 0x003A5D15
		public void ContinueNotifications()
		{
			this.m_pauseOnNext = false;
			this.ShowNextXpGain();
		}

		// Token: 0x0600BFEE RID: 49134 RVA: 0x003A7B24 File Offset: 0x003A5D24
		public void Hide()
		{
			this.UpdateDataModelXpProgress(this.m_targetXp);
			this.m_fsm.SendEvent("Popup_Outro");
		}

		// Token: 0x0600BFEF RID: 49135 RVA: 0x003A7B42 File Offset: 0x003A5D42
		private IEnumerator ShowXpGainsWhenReady()
		{
			while (!this.m_widget.IsReady || this.m_widget.IsChangingStates)
			{
				yield return null;
			}
			this.ShowNextXpGain();
			yield break;
		}

		// Token: 0x0600BFF0 RID: 49136 RVA: 0x003A7B54 File Offset: 0x003A5D54
		private void UpdateDataModelXpNeeded()
		{
			RewardTrackLevelDbfRecord rewardTrackLevelRecord = RewardTrackManager.Get().GetRewardTrackLevelRecord(this.m_dataModel.Level);
			this.m_dataModel.XpNeeded = ((rewardTrackLevelRecord != null) ? rewardTrackLevelRecord.XpNeeded : 0);
		}

		// Token: 0x0600BFF1 RID: 49137 RVA: 0x003A7B90 File Offset: 0x003A5D90
		private void UpdateXpAndLevelFsmVars()
		{
			int num = this.m_dataModel.Level + 1;
			FsmInt fsmInt = this.m_fsm.FsmVariables.GetFsmInt("StartLevel");
			if (fsmInt != null)
			{
				fsmInt.Value = num;
			}
			FsmInt fsmInt2 = this.m_fsm.FsmVariables.GetFsmInt("StartXp");
			if (fsmInt2 != null)
			{
				fsmInt2.Value = this.m_dataModel.Xp;
			}
			FsmInt fsmInt3 = this.m_fsm.FsmVariables.GetFsmInt("EndLevel");
			if (fsmInt3 != null)
			{
				if (this.m_dataModel.Level < this.m_targetLevel)
				{
					fsmInt3.Value = num + 1;
				}
				else
				{
					fsmInt3.Value = num;
				}
			}
			FsmInt fsmInt4 = this.m_fsm.FsmVariables.GetFsmInt("EndXp");
			if (fsmInt4 != null)
			{
				if (this.m_dataModel.Level < this.m_targetLevel)
				{
					fsmInt4.Value = this.m_dataModel.XpNeeded;
				}
				else
				{
					fsmInt4.Value = this.m_targetXp;
				}
			}
			FsmInt fsmInt5 = this.m_fsm.FsmVariables.GetFsmInt("XpNeeded");
			if (fsmInt5 != null)
			{
				fsmInt5.Value = this.m_dataModel.XpNeeded;
			}
			FsmFloat fsmFloat = this.m_fsm.FsmVariables.GetFsmFloat("StartBarPct");
			if (fsmFloat != null)
			{
				fsmFloat.Value = (float)fsmInt2.Value / (float)fsmInt5.Value;
			}
			FsmFloat fsmFloat2 = this.m_fsm.FsmVariables.GetFsmFloat("EndBarPct");
			if (fsmFloat2 != null)
			{
				fsmFloat2.Value = (float)fsmInt4.Value / (float)fsmInt5.Value;
			}
		}

		// Token: 0x0600BFF2 RID: 49138 RVA: 0x003A7D14 File Offset: 0x003A5F14
		private int CalculateIntroXpGained()
		{
			bool flag = this.m_dataModel.Level < this.m_targetLevel;
			int num = (flag ? this.m_dataModel.XpNeeded : this.m_targetXp) - this.m_dataModel.Xp;
			if (this.m_dataModel.Level + 1 < this.m_targetLevel)
			{
				for (int i = this.m_dataModel.Level + 1; i < this.m_targetLevel; i++)
				{
					RewardTrackLevelDbfRecord rewardTrackLevelRecord = RewardTrackManager.Get().GetRewardTrackLevelRecord(i);
					num += ((rewardTrackLevelRecord != null) ? rewardTrackLevelRecord.XpNeeded : 0);
				}
			}
			if (flag)
			{
				num += this.m_targetXp;
			}
			return num;
		}

		// Token: 0x0600BFF3 RID: 49139 RVA: 0x003A7DB4 File Offset: 0x003A5FB4
		private void UpdateIntroXpVar(int xpGained)
		{
			FsmInt fsmInt = this.m_fsm.FsmVariables.GetFsmInt("XpGained");
			if (fsmInt != null)
			{
				fsmInt.Value = xpGained;
			}
		}

		// Token: 0x0600BFF4 RID: 49140 RVA: 0x003A7DE1 File Offset: 0x003A5FE1
		private void AnimateQuestTile()
		{
			this.m_fsm.SendEvent("AnimateQuestTile");
		}

		// Token: 0x0600BFF5 RID: 49141 RVA: 0x003A7DF3 File Offset: 0x003A5FF3
		private void AnimateXpBar()
		{
			this.m_fsm.SendEvent("AnimateXpBar");
		}

		// Token: 0x0600BFF6 RID: 49142 RVA: 0x003A7E05 File Offset: 0x003A6005
		private void AnimateLevelFromGameXp()
		{
			this.m_fsm.SendEvent("LevelFromGameXp");
		}

		// Token: 0x0600BFF7 RID: 49143 RVA: 0x003A7E17 File Offset: 0x003A6017
		private void AnimateIntroXp()
		{
			this.m_fsm.SendEvent("Intro");
		}

		// Token: 0x0600BFF8 RID: 49144 RVA: 0x003A7E29 File Offset: 0x003A6029
		private void ShowIntroXp()
		{
			this.UpdateIntroXpVar(this.CalculateIntroXpGained());
			this.AnimateIntroXp();
		}

		// Token: 0x0600BFF9 RID: 49145 RVA: 0x003A7E40 File Offset: 0x003A6040
		private void OnPlayMakerFinished()
		{
			if (this.m_isIntro)
			{
				this.m_isIntro = false;
				this.AnimateXpBar();
				return;
			}
			bool flag = false;
			if (this.m_dataModel.Level < this.m_targetLevel || this.m_dataModel.Xp < this.m_targetXp)
			{
				if (this.m_dataModel.Level < this.m_targetLevel)
				{
					this.m_dataModel.Level++;
					this.m_dataModel.Xp = 0;
					this.UpdateDataModelXpNeeded();
					if (this.m_dataModel.Level < this.m_targetLevel || this.m_dataModel.Xp < this.m_targetXp)
					{
						this.UpdateXpAndLevelFsmVars();
						this.UpdateDataModelXpProgress(this.m_dataModel.Xp);
						if (this.m_gameXpCausedLevel)
						{
							this.AnimateLevelFromGameXp();
						}
						else
						{
							this.AnimateXpBar();
						}
						flag = true;
					}
				}
				else
				{
					this.m_dataModel.Xp = this.m_targetXp;
				}
			}
			if (!flag)
			{
				this.ShowNextXpGain();
			}
		}

		// Token: 0x0600BFFA RID: 49146 RVA: 0x003A7F37 File Offset: 0x003A6137
		private void UpdateDataModelXpProgress(int currentAnimatedXp)
		{
			this.m_dataModel.XpProgress = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_XP_PROGRESS", new object[]
			{
				currentAnimatedXp,
				this.m_dataModel.XpNeeded
			});
		}

		// Token: 0x04009BA7 RID: 39847
		[SerializeField]
		private PlayMakerFSM m_fsm;

		// Token: 0x04009BA8 RID: 39848
		[SerializeField]
		private Widget m_questTileWidget;

		// Token: 0x04009BA9 RID: 39849
		[SerializeField]
		private AsyncReference m_xpBarMeshReference;

		// Token: 0x04009BAA RID: 39850
		[SerializeField]
		private AsyncReference m_questXpRoot;

		// Token: 0x04009BAB RID: 39851
		[SerializeField]
		private AsyncReference m_xpProgressText;

		// Token: 0x04009BAC RID: 39852
		[SerializeField]
		private AsyncReference m_levelText;

		// Token: 0x04009BAD RID: 39853
		[SerializeField]
		private AsyncReference m_xpDisplayText;

		// Token: 0x04009BAE RID: 39854
		private Widget m_widget;

		// Token: 0x04009BAF RID: 39855
		private List<RewardTrackXpChange> m_xpChanges;

		// Token: 0x04009BB0 RID: 39856
		private RewardTrackDataModel m_dataModel;

		// Token: 0x04009BB1 RID: 39857
		private int m_targetLevel;

		// Token: 0x04009BB2 RID: 39858
		private int m_targetXp;

		// Token: 0x04009BB3 RID: 39859
		private Action m_callback;

		// Token: 0x04009BB4 RID: 39860
		private bool m_pauseOnNext;

		// Token: 0x04009BB5 RID: 39861
		private bool m_isShowing;

		// Token: 0x04009BB6 RID: 39862
		private bool m_gameXpCausedLevel;

		// Token: 0x04009BB7 RID: 39863
		private bool m_isIntro;
	}
}
