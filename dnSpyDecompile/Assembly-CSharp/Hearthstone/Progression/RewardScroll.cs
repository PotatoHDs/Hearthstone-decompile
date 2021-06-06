using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x0200111E RID: 4382
	[RequireComponent(typeof(WidgetTemplate))]
	public class RewardScroll : MonoBehaviour
	{
		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x0600C008 RID: 49160 RVA: 0x003A827C File Offset: 0x003A647C
		// (remove) Token: 0x0600C009 RID: 49161 RVA: 0x003A82B4 File Offset: 0x003A64B4
		private event Action OnRewardScrollHidden;

		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x0600C00A RID: 49162 RVA: 0x003A82EC File Offset: 0x003A64EC
		// (remove) Token: 0x0600C00B RID: 49163 RVA: 0x003A8324 File Offset: 0x003A6524
		private event Action OnRewardScrollShown;

		// Token: 0x0600C00C RID: 49164 RVA: 0x003A835C File Offset: 0x003A655C
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_HIDE")
				{
					this.Hide();
				}
			});
			this.m_owner = base.gameObject;
			if (base.transform.parent != null && base.transform.parent.GetComponent<WidgetInstance>() != null)
			{
				this.m_owner = base.transform.parent.gameObject;
			}
		}

		// Token: 0x0600C00D RID: 49165 RVA: 0x003A83D9 File Offset: 0x003A65D9
		public void Initialize(RewardScrollDataModel dataModel, Action onHiddenCallback = null, Action onShownCallback = null)
		{
			this.OnRewardScrollHidden = onHiddenCallback;
			this.OnRewardScrollShown = onShownCallback;
			this.m_widget.BindDataModel(dataModel, false);
		}

		// Token: 0x0600C00E RID: 49166 RVA: 0x003A83F8 File Offset: 0x003A65F8
		public void Show()
		{
			OverlayUI.Get().AddGameObject(this.m_owner, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
			{
				UIContext.GetRoot().ShowPopup(base.gameObject, UIContext.BlurType.Standard);
			}
			this.m_widget.TriggerEvent("CODE_SHOW_REWARD", new Widget.TriggerEventParameters
			{
				NoDownwardPropagation = true
			});
			Action onRewardScrollShown = this.OnRewardScrollShown;
			if (onRewardScrollShown == null)
			{
				return;
			}
			onRewardScrollShown();
		}

		// Token: 0x0600C00F RID: 49167 RVA: 0x003A8469 File Offset: 0x003A6669
		private void Hide()
		{
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
			{
				UIContext.GetRoot().DismissPopup(base.gameObject);
			}
			Action onRewardScrollHidden = this.OnRewardScrollHidden;
			if (onRewardScrollHidden != null)
			{
				onRewardScrollHidden();
			}
			UnityEngine.Object.Destroy(this.m_owner);
		}

		// Token: 0x0600C010 RID: 49168 RVA: 0x003A84A4 File Offset: 0x003A66A4
		public static void DebugShowFake(RewardScrollDataModel dataModel)
		{
			Widget widget = WidgetInstance.Create(RewardPresenter.REWARD_PREFAB, false);
			widget.RegisterReadyListener(delegate(object _)
			{
				widget.GetComponentInChildren<RewardScroll>().Initialize(dataModel, null, null);
			}, null, true);
			widget.RegisterDoneChangingStatesListener(delegate(object _)
			{
				widget.GetComponentInChildren<RewardScroll>().Show();
			}, null, true, true);
		}

		// Token: 0x04009BBC RID: 39868
		private const string HIDE = "CODE_HIDE";

		// Token: 0x04009BBD RID: 39869
		private const string SHOW_REWARD = "CODE_SHOW_REWARD";

		// Token: 0x04009BC0 RID: 39872
		private Widget m_widget;

		// Token: 0x04009BC1 RID: 39873
		private GameObject m_owner;
	}
}
