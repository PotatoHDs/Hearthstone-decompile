using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class CoinPreview : MonoBehaviour
	{
		public UIBButton m_favoriteButton;

		public GameObject m_startPositionMarker;

		private WidgetTemplate m_widget;

		private const string CODE_HIDE = "CODE_HIDE";

		private const string CODE_SHOW = "CODE_SHOW";

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_HIDE")
				{
					Hide();
				}
				else if (eventName == "CODE_SHOW")
				{
					UIContext.GetRoot().ShowPopup(base.gameObject, UIContext.BlurType.None);
					FullScreenFXMgr.Get().StartStandardBlurVignette(0.5f);
				}
			});
		}

		public void Initialize(CardDataModel cardDataModel, int coinId, Transform startTransform)
		{
			UpdateCoinCard(cardDataModel);
			if (startTransform != null)
			{
				m_startPositionMarker.transform.position = startTransform.position;
				m_startPositionMarker.transform.localScale = startTransform.localScale;
			}
			CoinManager coinManager = CoinManager.Get();
			if ((coinManager.GetCoinsOwned()?.Contains(coinId) ?? false) && coinManager.GetFavoriteCoinId() != coinId)
			{
				m_favoriteButton.AddEventListener(UIEventType.RELEASE, delegate
				{
					coinManager.RequestSetFavoriteCoin(coinId);
					Hide();
				});
			}
			else
			{
				m_favoriteButton.Flip(faceUp: false, forceImmediate: true);
				m_favoriteButton.SetEnabled(enabled: false);
			}
		}

		public void Hide()
		{
			FullScreenFXMgr.Get().EndStandardBlurVignette(0f);
			UIContext.GetRoot().DismissPopup(base.gameObject);
			if (!(m_widget == null))
			{
				m_widget.Hide();
				Object.Destroy(base.transform.parent.gameObject);
			}
		}

		private void UpdateCoinCard(CardDataModel cardDataModel)
		{
			m_widget.BindDataModel(cardDataModel);
		}
	}
}
