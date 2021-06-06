using System;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class ShowAllCardsTab : MonoBehaviour
{
	// Token: 0x060014DD RID: 5341 RVA: 0x00077A6D File Offset: 0x00075C6D
	private void Awake()
	{
		this.m_showAllCardsCheckBox.SetButtonText(GameStrings.Get("GLUE_COLLECTION_SHOW_ALL_CARDS"));
		this.m_includePremiumsCheckBox.SetButtonText(GameStrings.Get("GLUE_COLLECTION_INCLUDE_PREMIUMS"));
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x00077A9C File Offset: 0x00075C9C
	private void Start()
	{
		this.m_includePremiumsCheckBox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ToggleIncludePremiums));
		this.m_showAllCardsCheckBox.SetChecked(false);
		this.m_includePremiumsCheckBox.SetChecked(false);
		this.m_includePremiumsCheckBox.gameObject.SetActive(false);
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x00077AEB File Offset: 0x00075CEB
	public bool IsShowAllChecked()
	{
		return this.m_showAllCardsCheckBox.IsChecked();
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x00077AF8 File Offset: 0x00075CF8
	private void ToggleIncludePremiums(UIEvent e)
	{
		bool flag = this.m_includePremiumsCheckBox.IsChecked();
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.ShowPremiumCardsNotOwned(flag);
		}
		if (flag)
		{
			SoundManager.Get().LoadAndPlay("checkbox_toggle_on.prefab:8be4c59e7387600468ac88787943da8b", base.gameObject);
			return;
		}
		SoundManager.Get().LoadAndPlay("checkbox_toggle_off.prefab:fa341d119cee1d14c941b63dba112af3", base.gameObject);
	}

	// Token: 0x04000DF0 RID: 3568
	public CheckBox m_showAllCardsCheckBox;

	// Token: 0x04000DF1 RID: 3569
	public CheckBox m_includePremiumsCheckBox;
}
