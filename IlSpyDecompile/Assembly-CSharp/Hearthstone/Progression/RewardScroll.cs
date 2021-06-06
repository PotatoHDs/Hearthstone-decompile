using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class RewardScroll : MonoBehaviour
	{
		private const string HIDE = "CODE_HIDE";

		private const string SHOW_REWARD = "CODE_SHOW_REWARD";

		private Widget m_widget;

		private GameObject m_owner;

		private event Action OnRewardScrollHidden;

		private event Action OnRewardScrollShown;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_HIDE")
				{
					Hide();
				}
			});
			m_owner = base.gameObject;
			if (base.transform.parent != null && base.transform.parent.GetComponent<WidgetInstance>() != null)
			{
				m_owner = base.transform.parent.gameObject;
			}
		}

		public void Initialize(RewardScrollDataModel dataModel, Action onHiddenCallback = null, Action onShownCallback = null)
		{
			this.OnRewardScrollHidden = onHiddenCallback;
			this.OnRewardScrollShown = onShownCallback;
			m_widget.BindDataModel(dataModel);
		}

		public void Show()
		{
			OverlayUI.Get().AddGameObject(m_owner);
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
			{
				UIContext.GetRoot().ShowPopup(base.gameObject);
			}
			m_widget.TriggerEvent("CODE_SHOW_REWARD", new Widget.TriggerEventParameters
			{
				NoDownwardPropagation = true
			});
			this.OnRewardScrollShown?.Invoke();
		}

		private void Hide()
		{
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
			{
				UIContext.GetRoot().DismissPopup(base.gameObject);
			}
			this.OnRewardScrollHidden?.Invoke();
			UnityEngine.Object.Destroy(m_owner);
		}

		public static void DebugShowFake(RewardScrollDataModel dataModel)
		{
			Widget widget = WidgetInstance.Create(RewardPresenter.REWARD_PREFAB);
			widget.RegisterReadyListener(delegate
			{
				widget.GetComponentInChildren<RewardScroll>().Initialize(dataModel);
			});
			widget.RegisterDoneChangingStatesListener(delegate
			{
				widget.GetComponentInChildren<RewardScroll>().Show();
			}, null, callImmediatelyIfSet: true, doOnce: true);
		}
	}
}
