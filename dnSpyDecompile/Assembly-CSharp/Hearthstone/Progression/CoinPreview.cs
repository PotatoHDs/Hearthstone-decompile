using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x0200110B RID: 4363
	[RequireComponent(typeof(WidgetTemplate))]
	public class CoinPreview : MonoBehaviour
	{
		// Token: 0x0600BF2B RID: 48939 RVA: 0x003A3D62 File Offset: 0x003A1F62
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_HIDE")
				{
					this.Hide();
					return;
				}
				if (eventName == "CODE_SHOW")
				{
					UIContext.GetRoot().ShowPopup(base.gameObject, UIContext.BlurType.None);
					FullScreenFXMgr.Get().StartStandardBlurVignette(0.5f);
				}
			});
		}

		// Token: 0x0600BF2C RID: 48940 RVA: 0x003A3D88 File Offset: 0x003A1F88
		public void Initialize(CardDataModel cardDataModel, int coinId, Transform startTransform)
		{
			this.UpdateCoinCard(cardDataModel);
			if (startTransform != null)
			{
				this.m_startPositionMarker.transform.position = startTransform.position;
				this.m_startPositionMarker.transform.localScale = startTransform.localScale;
			}
			CoinManager coinManager = CoinManager.Get();
			HashSet<int> coinsOwned = coinManager.GetCoinsOwned();
			if (coinsOwned != null && coinsOwned.Contains(coinId) && coinManager.GetFavoriteCoinId() != coinId)
			{
				this.m_favoriteButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
				{
					coinManager.RequestSetFavoriteCoin(coinId);
					this.Hide();
				});
				return;
			}
			this.m_favoriteButton.Flip(false, true);
			this.m_favoriteButton.SetEnabled(false, false);
		}

		// Token: 0x0600BF2D RID: 48941 RVA: 0x003A3E58 File Offset: 0x003A2058
		public void Hide()
		{
			FullScreenFXMgr.Get().EndStandardBlurVignette(0f, null);
			UIContext.GetRoot().DismissPopup(base.gameObject);
			if (this.m_widget == null)
			{
				return;
			}
			this.m_widget.Hide();
			UnityEngine.Object.Destroy(base.transform.parent.gameObject);
		}

		// Token: 0x0600BF2E RID: 48942 RVA: 0x003A3EB4 File Offset: 0x003A20B4
		private void UpdateCoinCard(CardDataModel cardDataModel)
		{
			this.m_widget.BindDataModel(cardDataModel, false);
		}

		// Token: 0x04009B46 RID: 39750
		public UIBButton m_favoriteButton;

		// Token: 0x04009B47 RID: 39751
		public GameObject m_startPositionMarker;

		// Token: 0x04009B48 RID: 39752
		private WidgetTemplate m_widget;

		// Token: 0x04009B49 RID: 39753
		private const string CODE_HIDE = "CODE_HIDE";

		// Token: 0x04009B4A RID: 39754
		private const string CODE_SHOW = "CODE_SHOW";
	}
}
