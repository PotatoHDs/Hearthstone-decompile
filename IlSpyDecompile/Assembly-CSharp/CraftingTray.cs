using System;
using System.Collections;
using Assets;
using UnityEngine;

public class CraftingTray : MonoBehaviour
{
	public UIBButton m_doneButton;

	public PegUIElement m_massDisenchantButton;

	public UberText m_potentialDustAmount;

	public UberText m_massDisenchantText;

	public CheckBox m_showGoldenCheckbox;

	public CheckBox m_showSoulboundCheckbox;

	public CheckBox m_showDiamondCheckbox;

	public HighlightState m_highlight;

	public GameObject m_massDisenchantMesh;

	public Material m_massDisenchantMaterial;

	public Material m_massDisenchantDisabledMaterial;

	private int m_dustAmount;

	private bool m_shown;

	private CollectionUtils.ViewMode m_previousViewMode;

	private static CraftingTray s_instance;

	private static PlatformDependentValue<int> MASS_DISENCHANT_MATERIAL_TO_SWITCH = new PlatformDependentValue<int>(PlatformCategory.Screen)
	{
		PC = 0,
		Phone = 1
	};

	private void Awake()
	{
		s_instance = this;
	}

	private void Start()
	{
		m_doneButton.AddEventListener(UIEventType.RELEASE, OnDoneButtonReleased);
		m_massDisenchantButton.AddEventListener(UIEventType.RELEASE, OnMassDisenchantButtonReleased);
		m_massDisenchantButton.AddEventListener(UIEventType.ROLLOVER, OnMassDisenchantButtonOver);
		m_massDisenchantButton.AddEventListener(UIEventType.ROLLOUT, OnMassDisenchantButtonOut);
		m_showGoldenCheckbox.AddEventListener(UIEventType.RELEASE, ToggleShowGolden);
		m_showGoldenCheckbox.SetChecked(isChecked: false);
		m_showSoulboundCheckbox.AddEventListener(UIEventType.RELEASE, ToggleShowSoulbound);
		m_showSoulboundCheckbox.SetChecked(isChecked: false);
		m_showDiamondCheckbox.AddEventListener(UIEventType.RELEASE, ToggleShowDiamond);
		m_showDiamondCheckbox.SetChecked(isChecked: false);
		SetMassDisenchantAmount();
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static CraftingTray Get()
	{
		return s_instance;
	}

	public void UpdateMassDisenchantAmount()
	{
		bool massDisenchantEnabled = m_dustAmount > 0 && !GameUtils.AtPrereleaseEvent();
		SetMassDisenchantEnabled(massDisenchantEnabled);
	}

	private void SetMassDisenchantEnabled(bool enabled)
	{
		m_massDisenchantButton.SetEnabled(enabled);
		m_massDisenchantText.gameObject.SetActive(enabled);
		m_potentialDustAmount.gameObject.SetActive(enabled);
		m_highlight.gameObject.SetActive(enabled);
		Renderer component = m_massDisenchantMesh.GetComponent<Renderer>();
		if (enabled)
		{
			component.SetMaterial(MASS_DISENCHANT_MATERIAL_TO_SWITCH, m_massDisenchantMaterial);
			m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
		else
		{
			component.SetMaterial(MASS_DISENCHANT_MATERIAL_TO_SWITCH, m_massDisenchantDisabledMaterial);
		}
	}

	public void SetMassDisenchantAmount()
	{
		if (base.gameObject.activeSelf)
		{
			StartCoroutine(SetMassDisenchantAmountWhenReady());
		}
	}

	private IEnumerator SetMassDisenchantAmountWhenReady()
	{
		while (MassDisenchant.Get() == null)
		{
			yield return null;
		}
		MassDisenchant.Get().UpdateContents(CollectionManager.Get().GetMassDisenchantCards());
		int num = (m_dustAmount = MassDisenchant.Get().GetTotalAmount());
		m_potentialDustAmount.Text = num.ToString();
		UpdateMassDisenchantAmount();
	}

	public void Show(bool? overrideShowSoulbound = null, bool? overrideShowGolden = null, bool? overrideShowDiamond = null, bool updatePage = true)
	{
		m_shown = true;
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.CRAFTING);
		if (overrideShowSoulbound.HasValue)
		{
			m_showSoulboundCheckbox.SetChecked(overrideShowSoulbound.Value);
		}
		if (overrideShowGolden.HasValue)
		{
			m_showGoldenCheckbox.SetChecked(overrideShowGolden.Value);
		}
		if (overrideShowDiamond.HasValue)
		{
			m_showDiamondCheckbox.SetChecked(overrideShowDiamond.Value);
		}
		SetMassDisenchantAmount();
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.ShowCraftingModeCards(m_showSoulboundCheckbox.IsChecked(), m_showGoldenCheckbox.IsChecked(), m_showDiamondCheckbox.IsChecked(), updatePage);
	}

	public void Hide()
	{
		m_shown = false;
		PresenceMgr.Get().SetPrevStatus();
		bool flag = CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.MASS_DISENCHANT;
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideCraftingTray();
			collectionManagerDisplay.m_pageManager.OnCraftingTrayHidden(flag);
		}
		if (flag)
		{
			CollectionManager.Get().GetCollectibleDisplay().SetViewMode(m_previousViewMode);
		}
	}

	public bool IsShown()
	{
		return m_shown;
	}

	private void OnDoneButtonReleased(UIEvent e)
	{
		Hide();
	}

	private void OnMassDisenchantButtonReleased(UIEvent e)
	{
		if (!CollectionManager.Get().GetCollectibleDisplay().m_pageManager.ArePagesTurning())
		{
			if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.MASS_DISENCHANT)
			{
				CollectionManager.Get().GetCollectibleDisplay().SetViewMode(m_previousViewMode);
				m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
			}
			else
			{
				m_previousViewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
				CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.MASS_DISENCHANT);
				StartCoroutine(MassDisenchant.Get().StartHighlight());
			}
			SoundManager.Get().LoadAndPlay("Hub_Click.prefab:cc2cf2b5507827149b13d12210c0f323");
		}
	}

	private void OnMassDisenchantButtonOver(UIEvent e)
	{
		m_highlight.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
		SoundManager.Get().LoadAndPlay("Hub_Mouseover.prefab:40130da7b734190479c527d6bca1a4a8");
	}

	private void OnMassDisenchantButtonOut(UIEvent e)
	{
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() != CollectionUtils.ViewMode.MASS_DISENCHANT)
		{
			int num = 0;
			try
			{
				num = int.Parse(m_potentialDustAmount.Text);
			}
			catch (Exception ex)
			{
				Log.All.PrintWarning("Exception when attempting to parse CraftingTray's m_potentialDustAmount! Exception: {0}", ex);
			}
			if (num > 0)
			{
				m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
			else
			{
				m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
			}
		}
	}

	private void ToggleShowGolden(UIEvent e)
	{
		bool flag = m_showGoldenCheckbox.IsChecked();
		bool updatePage = CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS;
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.ShowCraftingModeCards(m_showSoulboundCheckbox.IsChecked(), flag, m_showDiamondCheckbox.IsChecked(), updatePage, toggleChanged: true);
		if (flag)
		{
			SoundManager.Get().LoadAndPlay("checkbox_toggle_on.prefab:8be4c59e7387600468ac88787943da8b", base.gameObject);
		}
		else
		{
			SoundManager.Get().LoadAndPlay("checkbox_toggle_off.prefab:fa341d119cee1d14c941b63dba112af3", base.gameObject);
		}
	}

	private void ToggleShowSoulbound(UIEvent e)
	{
		bool flag = m_showSoulboundCheckbox.IsChecked();
		bool updatePage = CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS;
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.ShowCraftingModeCards(flag, m_showGoldenCheckbox.IsChecked(), m_showDiamondCheckbox.IsChecked(), updatePage, toggleChanged: true);
		if (flag)
		{
			SoundManager.Get().LoadAndPlay("checkbox_toggle_on.prefab:8be4c59e7387600468ac88787943da8b", base.gameObject);
		}
		else
		{
			SoundManager.Get().LoadAndPlay("checkbox_toggle_off.prefab:fa341d119cee1d14c941b63dba112af3", base.gameObject);
		}
	}

	private void ToggleShowDiamond(UIEvent e)
	{
		bool flag = m_showDiamondCheckbox.IsChecked();
		bool updatePage = CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS;
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.ShowCraftingModeCards(m_showSoulboundCheckbox.IsChecked(), m_showGoldenCheckbox.IsChecked(), flag, updatePage, toggleChanged: true);
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
