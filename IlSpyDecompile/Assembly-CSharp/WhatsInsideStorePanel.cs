using Hearthstone.UI;
using UnityEngine;

public class WhatsInsideStorePanel : MonoBehaviour
{
	public UIBButton m_whatsInsideButton;

	public string m_rewardCardId;

	private AssetReference m_whatsInsideWidgetPrefab = new AssetReference("AdventureStorymodeWhatsInsideStore.prefab:099ec2422fde4054495382cee27d8c06");

	private Widget m_whatsInsideWidget;

	private UIBPopup m_whatsInsidePopup;

	private UIBButton m_whatsInsideBackButton;

	private Actor m_legendaryCardActor;

	private void Start()
	{
		m_whatsInsideButton.AddEventListener(UIEventType.RELEASE, OnWhatsInsideButtonRelease);
	}

	private void OnWhatsInsideButtonRelease(UIEvent e)
	{
		if (m_whatsInsideWidget == null)
		{
			m_whatsInsideWidget = WidgetInstance.Create(m_whatsInsideWidgetPrefab);
		}
		m_whatsInsideWidget.RegisterReadyListener(delegate
		{
			if (m_whatsInsidePopup == null)
			{
				m_whatsInsidePopup = m_whatsInsideWidget.GetComponentInChildren<UIBPopup>();
			}
			if (m_whatsInsideBackButton == null)
			{
				m_whatsInsideBackButton = m_whatsInsideWidget.GetComponentInChildren<UIBButton>();
				m_whatsInsideBackButton.AddEventListener(UIEventType.RELEASE, delegate
				{
					m_whatsInsidePopup.Hide();
				});
			}
			m_legendaryCardActor = m_whatsInsideWidget.GetComponentInChildren<Actor>(includeInactive: true);
			if (m_legendaryCardActor != null)
			{
				using DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(m_rewardCardId);
				m_legendaryCardActor.SetPremium(TAG_PREMIUM.GOLDEN);
				m_legendaryCardActor.SetFullDef(fullDef);
				m_legendaryCardActor.UpdateAllComponents();
			}
			m_whatsInsidePopup.Show(useOverlayUI: false);
		});
	}
}
