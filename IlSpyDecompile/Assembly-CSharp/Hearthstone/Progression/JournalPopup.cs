using System;
using Assets;
using Blizzard.Telemetry.WTCG.Client;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class JournalPopup : MonoBehaviour
	{
		public static bool s_isShowing = false;

		public static readonly AssetReference JOURNAL_POPUP_PREFAB = new AssetReference("JournalPopup.prefab:b61d6e9bd58789647a62494cf92fb93e");

		private string CLOSE_EVENT = "CODE_CLOSE";

		private Widget m_widget;

		private GameObject m_owner;

		private Enum[] m_presencePrevStatus;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.BindDataModel(AchievementManager.Get().Categories);
			m_widget.BindDataModel(RewardTrackManager.Get().TrackDataModel);
			m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == CLOSE_EVENT)
				{
					Close();
				}
			});
			m_owner = base.gameObject;
			if (base.transform.parent != null && base.transform.parent.GetComponent<WidgetInstance>() != null)
			{
				m_owner = base.transform.parent.gameObject;
			}
			PegUI.Get().RegisterForCameraDepthPriorityHitTest(this);
			SceneMgr.Get().RegisterScenePreLoadEvent(OnPreLoadNextScene);
			FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		}

		private void OnDestroy()
		{
			s_isShowing = false;
			Navigation.RemoveHandler(OnNavigateBack);
			if (PresenceMgr.IsInitialized())
			{
				PresenceMgr.Get().SetStatus(m_presencePrevStatus);
			}
			UIContext.GetRoot()?.UnregisterPopup(m_owner);
			if (PegUI.IsInitialized())
			{
				PegUI.Get().UnregisterForCameraDepthPriorityHitTest(this);
			}
			if (SceneMgr.IsInitialized())
			{
				SceneMgr.Get().UnregisterScenePreLoadEvent(OnPreLoadNextScene);
			}
			if (FatalErrorMgr.IsInitialized())
			{
				FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
			}
		}

		public void Show()
		{
			s_isShowing = true;
			HearthstonePerformance.Get()?.StartPerformanceFlow(new FlowPerformance.SetupConfig
			{
				FlowType = Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType.JOURNAL
			});
			Navigation.Push(OnNavigateBack);
			m_presencePrevStatus = PresenceMgr.Get().GetStatus();
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.VIEWING_JOURNAL);
			m_widget.RegisterDoneChangingStatesListener(delegate
			{
				OverlayUI.Get().AddGameObject(m_owner);
				UIContext.GetRoot().RegisterPopup(m_owner, UIContext.RenderCameraType.OrthographicUI);
				m_widget.TriggerEvent("SHOW");
			}, null, callImmediatelyIfSet: true, doOnce: true);
		}

		public void Close()
		{
			HearthstonePerformance.Get()?.StopCurrentFlow();
			UnityEngine.Object.Destroy(m_owner);
		}

		private bool OnNavigateBack()
		{
			m_widget.TriggerEvent("HIDE");
			return true;
		}

		private void OnPreLoadNextScene(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData)
		{
			Close();
		}

		private void OnFatalError(FatalErrorMessage message, object userData)
		{
			Close();
		}
	}
}
