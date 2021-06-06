using System;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000733 RID: 1843
public class WhatsInsideStorePanel : MonoBehaviour
{
	// Token: 0x06006783 RID: 26499 RVA: 0x0021BB5B File Offset: 0x00219D5B
	private void Start()
	{
		this.m_whatsInsideButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnWhatsInsideButtonRelease));
	}

	// Token: 0x06006784 RID: 26500 RVA: 0x0021BB76 File Offset: 0x00219D76
	private void OnWhatsInsideButtonRelease(UIEvent e)
	{
		if (this.m_whatsInsideWidget == null)
		{
			this.m_whatsInsideWidget = WidgetInstance.Create(this.m_whatsInsideWidgetPrefab, false);
		}
		this.m_whatsInsideWidget.RegisterReadyListener(delegate(object _)
		{
			if (this.m_whatsInsidePopup == null)
			{
				this.m_whatsInsidePopup = this.m_whatsInsideWidget.GetComponentInChildren<UIBPopup>();
			}
			if (this.m_whatsInsideBackButton == null)
			{
				this.m_whatsInsideBackButton = this.m_whatsInsideWidget.GetComponentInChildren<UIBButton>();
				this.m_whatsInsideBackButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent backEvent)
				{
					this.m_whatsInsidePopup.Hide();
				});
			}
			this.m_legendaryCardActor = this.m_whatsInsideWidget.GetComponentInChildren<Actor>(true);
			if (this.m_legendaryCardActor != null)
			{
				using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(this.m_rewardCardId, null))
				{
					this.m_legendaryCardActor.SetPremium(TAG_PREMIUM.GOLDEN);
					this.m_legendaryCardActor.SetFullDef(fullDef);
					this.m_legendaryCardActor.UpdateAllComponents();
				}
			}
			this.m_whatsInsidePopup.Show(false);
		}, null, true);
	}

	// Token: 0x0400553A RID: 21818
	public UIBButton m_whatsInsideButton;

	// Token: 0x0400553B RID: 21819
	public string m_rewardCardId;

	// Token: 0x0400553C RID: 21820
	private AssetReference m_whatsInsideWidgetPrefab = new AssetReference("AdventureStorymodeWhatsInsideStore.prefab:099ec2422fde4054495382cee27d8c06");

	// Token: 0x0400553D RID: 21821
	private Widget m_whatsInsideWidget;

	// Token: 0x0400553E RID: 21822
	private UIBPopup m_whatsInsidePopup;

	// Token: 0x0400553F RID: 21823
	private UIBButton m_whatsInsideBackButton;

	// Token: 0x04005540 RID: 21824
	private Actor m_legendaryCardActor;
}
