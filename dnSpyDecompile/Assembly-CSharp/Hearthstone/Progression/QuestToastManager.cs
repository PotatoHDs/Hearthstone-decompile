using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;

namespace Hearthstone.Progression
{
	// Token: 0x0200111B RID: 4379
	public class QuestToastManager : IService
	{
		// Token: 0x0600BFDD RID: 49117 RVA: 0x003A74F3 File Offset: 0x003A56F3
		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			HearthstoneApplication.Get().WillReset += this.WillReset;
			serviceLocator.Get<Network>().RegisterNetHandler(InitialClientState.PacketID.ID, new Network.NetHandler(this.OnInitialClientState), null);
			QuestManager.Get().OnQuestProgress += this.OnQuestProgress;
			yield break;
		}

		// Token: 0x0600BFDE RID: 49118 RVA: 0x003A7509 File Offset: 0x003A5709
		public Type[] GetDependencies()
		{
			return new Type[]
			{
				typeof(Network),
				typeof(QuestManager)
			};
		}

		// Token: 0x0600BFDF RID: 49119 RVA: 0x003A752C File Offset: 0x003A572C
		public void Shutdown()
		{
			QuestManager questManager;
			if (HearthstoneServices.TryGet<QuestManager>(out questManager))
			{
				questManager.OnQuestProgress -= this.OnQuestProgress;
			}
		}

		// Token: 0x0600BFE0 RID: 49120 RVA: 0x003A7554 File Offset: 0x003A5754
		private void OnQuestProgress(QuestDataModel questDataModel)
		{
			this.m_questList.Add(questDataModel);
		}

		// Token: 0x0600BFE1 RID: 49121 RVA: 0x003A7562 File Offset: 0x003A5762
		private void WillReset()
		{
			this.m_isSystemEnabled = false;
			this.m_questList.Clear();
		}

		// Token: 0x0600BFE2 RID: 49122 RVA: 0x003A7576 File Offset: 0x003A5776
		public static QuestToastManager Get()
		{
			return HearthstoneServices.Get<QuestToastManager>();
		}

		// Token: 0x0600BFE3 RID: 49123 RVA: 0x003A7580 File Offset: 0x003A5780
		public void ShowQuestProgress()
		{
			if (!this.m_isSystemEnabled)
			{
				return;
			}
			using (List<QuestDataModel>.Enumerator enumerator = this.m_questList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					QuestToastManager.<>c__DisplayClass10_0 CS$<>8__locals1 = new QuestToastManager.<>c__DisplayClass10_0();
					CS$<>8__locals1.<>4__this = this;
					CS$<>8__locals1.quest = enumerator.Current;
					Widget widget = WidgetInstance.Create(QuestToastManager.QUEST_PROGRESS_TOAST_PREFAB, false);
					widget.RegisterReadyListener(delegate(object _)
					{
						QuestProgressToast componentInChildren = widget.GetComponentInChildren<QuestProgressToast>();
						componentInChildren.Initialize(CS$<>8__locals1.quest);
						OverlayUI.Get().AddGameObject(componentInChildren.transform.parent.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
						CS$<>8__locals1.<>4__this.m_activeToasts.Add(componentInChildren);
						CS$<>8__locals1.<>4__this.UpdateToastPositions();
						componentInChildren.Show();
					}, null, true);
					widget.RegisterDeactivatedListener(delegate(object _)
					{
						CS$<>8__locals1.<>4__this.m_activeToasts.Remove(widget.GetComponentInChildren<QuestProgressToast>());
						CS$<>8__locals1.<>4__this.UpdateToastPositions();
					}, null);
				}
			}
			this.m_questList.Clear();
		}

		// Token: 0x0600BFE4 RID: 49124 RVA: 0x003A7648 File Offset: 0x003A5848
		private void UpdateToastPositions()
		{
			int num = 0;
			foreach (QuestProgressToast questProgressToast in this.m_activeToasts)
			{
				if (num > 0)
				{
					TransformUtil.SetPoint(questProgressToast.gameObject, Anchor.BOTTOM, this.m_activeToasts[num - 1].gameObject, Anchor.TOP, this.m_activeToasts[num - 1].GetOffset());
				}
				num++;
			}
		}

		// Token: 0x0600BFE5 RID: 49125 RVA: 0x003A76D4 File Offset: 0x003A58D4
		private void OnInitialClientState()
		{
			InitialClientState initialClientState = Network.Get().GetInitialClientState();
			if (initialClientState != null && initialClientState.HasGuardianVars)
			{
				this.m_isSystemEnabled = initialClientState.GuardianVars.ProgressionEnabled;
			}
		}

		// Token: 0x04009BA3 RID: 39843
		public static readonly AssetReference QUEST_PROGRESS_TOAST_PREFAB = new AssetReference("QuestProgressToast.prefab:a14a1594ad6e85242a7b50b26c840edd");

		// Token: 0x04009BA4 RID: 39844
		private bool m_isSystemEnabled;

		// Token: 0x04009BA5 RID: 39845
		private List<QuestDataModel> m_questList = new List<QuestDataModel>();

		// Token: 0x04009BA6 RID: 39846
		private List<QuestProgressToast> m_activeToasts = new List<QuestProgressToast>();
	}
}
