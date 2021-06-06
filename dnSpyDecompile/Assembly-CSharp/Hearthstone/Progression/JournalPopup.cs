using System;
using Assets;
using Blizzard.Telemetry.WTCG.Client;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x0200110D RID: 4365
	[RequireComponent(typeof(WidgetTemplate))]
	public class JournalPopup : MonoBehaviour
	{
		// Token: 0x0600BF37 RID: 48951 RVA: 0x003A407C File Offset: 0x003A227C
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.BindDataModel(AchievementManager.Get().Categories, false);
			this.m_widget.BindDataModel(RewardTrackManager.Get().TrackDataModel, false);
			this.m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == this.CLOSE_EVENT)
				{
					this.Close();
				}
			});
			this.m_owner = base.gameObject;
			if (base.transform.parent != null && base.transform.parent.GetComponent<WidgetInstance>() != null)
			{
				this.m_owner = base.transform.parent.gameObject;
			}
			PegUI.Get().RegisterForCameraDepthPriorityHitTest(this);
			SceneMgr.Get().RegisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnPreLoadNextScene));
			FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		}

		// Token: 0x0600BF38 RID: 48952 RVA: 0x003A4160 File Offset: 0x003A2360
		private void OnDestroy()
		{
			JournalPopup.s_isShowing = false;
			Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
			if (PresenceMgr.IsInitialized())
			{
				PresenceMgr.Get().SetStatus(this.m_presencePrevStatus);
			}
			UIContext root = UIContext.GetRoot();
			if (root != null)
			{
				root.UnregisterPopup(this.m_owner);
			}
			if (PegUI.IsInitialized())
			{
				PegUI.Get().UnregisterForCameraDepthPriorityHitTest(this);
			}
			if (SceneMgr.IsInitialized())
			{
				SceneMgr.Get().UnregisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnPreLoadNextScene));
			}
			if (FatalErrorMgr.IsInitialized())
			{
				FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
			}
		}

		// Token: 0x0600BF39 RID: 48953 RVA: 0x003A4204 File Offset: 0x003A2404
		public void Show()
		{
			JournalPopup.s_isShowing = true;
			HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
			if (hearthstonePerformance != null)
			{
				hearthstonePerformance.StartPerformanceFlow(new global::FlowPerformance.SetupConfig
				{
					FlowType = Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType.JOURNAL
				});
			}
			Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
			this.m_presencePrevStatus = PresenceMgr.Get().GetStatus();
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.VIEWING_JOURNAL
			});
			this.m_widget.RegisterDoneChangingStatesListener(delegate(object _)
			{
				OverlayUI.Get().AddGameObject(this.m_owner, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
				UIContext.GetRoot().RegisterPopup(this.m_owner, UIContext.RenderCameraType.OrthographicUI, UIContext.BlurType.Standard);
				this.m_widget.TriggerEvent("SHOW", default(Widget.TriggerEventParameters));
			}, null, true, true);
		}

		// Token: 0x0600BF3A RID: 48954 RVA: 0x003A4289 File Offset: 0x003A2489
		public void Close()
		{
			HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
			if (hearthstonePerformance != null)
			{
				hearthstonePerformance.StopCurrentFlow();
			}
			UnityEngine.Object.Destroy(this.m_owner);
		}

		// Token: 0x0600BF3B RID: 48955 RVA: 0x003A42A8 File Offset: 0x003A24A8
		private bool OnNavigateBack()
		{
			this.m_widget.TriggerEvent("HIDE", default(Widget.TriggerEventParameters));
			return true;
		}

		// Token: 0x0600BF3C RID: 48956 RVA: 0x003A42D0 File Offset: 0x003A24D0
		private void OnPreLoadNextScene(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData)
		{
			this.Close();
		}

		// Token: 0x0600BF3D RID: 48957 RVA: 0x003A42D0 File Offset: 0x003A24D0
		private void OnFatalError(FatalErrorMessage message, object userData)
		{
			this.Close();
		}

		// Token: 0x04009B51 RID: 39761
		public static bool s_isShowing = false;

		// Token: 0x04009B52 RID: 39762
		public static readonly AssetReference JOURNAL_POPUP_PREFAB = new AssetReference("JournalPopup.prefab:b61d6e9bd58789647a62494cf92fb93e");

		// Token: 0x04009B53 RID: 39763
		private string CLOSE_EVENT = "CODE_CLOSE";

		// Token: 0x04009B54 RID: 39764
		private Widget m_widget;

		// Token: 0x04009B55 RID: 39765
		private GameObject m_owner;

		// Token: 0x04009B56 RID: 39766
		private Enum[] m_presencePrevStatus;
	}
}
