using UnityEngine;

public class ShowAllCardsTab : MonoBehaviour
{
	public CheckBox m_showAllCardsCheckBox;

	public CheckBox m_includePremiumsCheckBox;

	private void Awake()
	{
		m_showAllCardsCheckBox.SetButtonText(GameStrings.Get("GLUE_COLLECTION_SHOW_ALL_CARDS"));
		m_includePremiumsCheckBox.SetButtonText(GameStrings.Get("GLUE_COLLECTION_INCLUDE_PREMIUMS"));
	}

	private void Start()
	{
		m_includePremiumsCheckBox.AddEventListener(UIEventType.RELEASE, ToggleIncludePremiums);
		m_showAllCardsCheckBox.SetChecked(isChecked: false);
		m_includePremiumsCheckBox.SetChecked(isChecked: false);
		m_includePremiumsCheckBox.gameObject.SetActive(value: false);
	}

	public bool IsShowAllChecked()
	{
		return m_showAllCardsCheckBox.IsChecked();
	}

	private void ToggleIncludePremiums(UIEvent e)
	{
		bool flag = m_includePremiumsCheckBox.IsChecked();
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.ShowPremiumCardsNotOwned(flag);
		}
		if (flag)
		{
			SoundManager.Get().LoadAndPlay("checkbox_toggle_on.prefab:8be4c59e7387600468ac88787943da8b", base.gameObject);
		}
		else
		{
			SoundManager.Get().LoadAndPlay("checkbox_toggle_off.prefab:fa341d119cee1d14c941b63dba112af3", base.gameObject);
		}
	}
}
