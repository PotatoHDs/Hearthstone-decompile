using System;
using System.Collections;
using Assets;
using UnityEngine;

// Token: 0x0200011F RID: 287
public class CraftingTray : MonoBehaviour
{
	// Token: 0x060012F9 RID: 4857 RVA: 0x0006CA8D File Offset: 0x0006AC8D
	private void Awake()
	{
		CraftingTray.s_instance = this;
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x0006CA98 File Offset: 0x0006AC98
	private void Start()
	{
		this.m_doneButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDoneButtonReleased));
		this.m_massDisenchantButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnMassDisenchantButtonReleased));
		this.m_massDisenchantButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnMassDisenchantButtonOver));
		this.m_massDisenchantButton.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnMassDisenchantButtonOut));
		this.m_showGoldenCheckbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ToggleShowGolden));
		this.m_showGoldenCheckbox.SetChecked(false);
		this.m_showSoulboundCheckbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ToggleShowSoulbound));
		this.m_showSoulboundCheckbox.SetChecked(false);
		this.m_showDiamondCheckbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ToggleShowDiamond));
		this.m_showDiamondCheckbox.SetChecked(false);
		this.SetMassDisenchantAmount();
	}

	// Token: 0x060012FB RID: 4859 RVA: 0x0006CB7E File Offset: 0x0006AD7E
	private void OnDestroy()
	{
		CraftingTray.s_instance = null;
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x0006CB86 File Offset: 0x0006AD86
	public static CraftingTray Get()
	{
		return CraftingTray.s_instance;
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x0006CB90 File Offset: 0x0006AD90
	public void UpdateMassDisenchantAmount()
	{
		bool massDisenchantEnabled = this.m_dustAmount > 0 && !GameUtils.AtPrereleaseEvent();
		this.SetMassDisenchantEnabled(massDisenchantEnabled);
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x0006CBBC File Offset: 0x0006ADBC
	private void SetMassDisenchantEnabled(bool enabled)
	{
		this.m_massDisenchantButton.SetEnabled(enabled, false);
		this.m_massDisenchantText.gameObject.SetActive(enabled);
		this.m_potentialDustAmount.gameObject.SetActive(enabled);
		this.m_highlight.gameObject.SetActive(enabled);
		Renderer component = this.m_massDisenchantMesh.GetComponent<Renderer>();
		if (enabled)
		{
			component.SetMaterial(CraftingTray.MASS_DISENCHANT_MATERIAL_TO_SWITCH, this.m_massDisenchantMaterial);
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			return;
		}
		component.SetMaterial(CraftingTray.MASS_DISENCHANT_MATERIAL_TO_SWITCH, this.m_massDisenchantDisabledMaterial);
	}

	// Token: 0x060012FF RID: 4863 RVA: 0x0006CC53 File Offset: 0x0006AE53
	public void SetMassDisenchantAmount()
	{
		if (base.gameObject.activeSelf)
		{
			base.StartCoroutine(this.SetMassDisenchantAmountWhenReady());
		}
	}

	// Token: 0x06001300 RID: 4864 RVA: 0x0006CC6F File Offset: 0x0006AE6F
	private IEnumerator SetMassDisenchantAmountWhenReady()
	{
		while (MassDisenchant.Get() == null)
		{
			yield return null;
		}
		MassDisenchant.Get().UpdateContents(CollectionManager.Get().GetMassDisenchantCards());
		int totalAmount = MassDisenchant.Get().GetTotalAmount();
		this.m_dustAmount = totalAmount;
		this.m_potentialDustAmount.Text = totalAmount.ToString();
		this.UpdateMassDisenchantAmount();
		yield break;
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x0006CC80 File Offset: 0x0006AE80
	public void Show(bool? overrideShowSoulbound = null, bool? overrideShowGolden = null, bool? overrideShowDiamond = null, bool updatePage = true)
	{
		this.m_shown = true;
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.CRAFTING
		});
		if (overrideShowSoulbound != null)
		{
			this.m_showSoulboundCheckbox.SetChecked(overrideShowSoulbound.Value);
		}
		if (overrideShowGolden != null)
		{
			this.m_showGoldenCheckbox.SetChecked(overrideShowGolden.Value);
		}
		if (overrideShowDiamond != null)
		{
			this.m_showDiamondCheckbox.SetChecked(overrideShowDiamond.Value);
		}
		this.SetMassDisenchantAmount();
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.ShowCraftingModeCards(this.m_showSoulboundCheckbox.IsChecked(), this.m_showGoldenCheckbox.IsChecked(), this.m_showDiamondCheckbox.IsChecked(), updatePage, false);
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x0006CD40 File Offset: 0x0006AF40
	public void Hide()
	{
		this.m_shown = false;
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
			CollectionManager.Get().GetCollectibleDisplay().SetViewMode(this.m_previousViewMode, null);
		}
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x0006CDB6 File Offset: 0x0006AFB6
	public bool IsShown()
	{
		return this.m_shown;
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x0006CDBE File Offset: 0x0006AFBE
	private void OnDoneButtonReleased(UIEvent e)
	{
		this.Hide();
	}

	// Token: 0x06001305 RID: 4869 RVA: 0x0006CDC8 File Offset: 0x0006AFC8
	private void OnMassDisenchantButtonReleased(UIEvent e)
	{
		if (!CollectionManager.Get().GetCollectibleDisplay().m_pageManager.ArePagesTurning())
		{
			if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.MASS_DISENCHANT)
			{
				CollectionManager.Get().GetCollectibleDisplay().SetViewMode(this.m_previousViewMode, null);
				this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
			}
			else
			{
				this.m_previousViewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
				CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.MASS_DISENCHANT, null);
				base.StartCoroutine(MassDisenchant.Get().StartHighlight());
			}
			SoundManager.Get().LoadAndPlay("Hub_Click.prefab:cc2cf2b5507827149b13d12210c0f323");
		}
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x0006CE71 File Offset: 0x0006B071
	private void OnMassDisenchantButtonOver(UIEvent e)
	{
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
		SoundManager.Get().LoadAndPlay("Hub_Mouseover.prefab:40130da7b734190479c527d6bca1a4a8");
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x0006CE98 File Offset: 0x0006B098
	private void OnMassDisenchantButtonOut(UIEvent e)
	{
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() != CollectionUtils.ViewMode.MASS_DISENCHANT)
		{
			int num = 0;
			try
			{
				num = int.Parse(this.m_potentialDustAmount.Text);
			}
			catch (Exception ex)
			{
				Log.All.PrintWarning("Exception when attempting to parse CraftingTray's m_potentialDustAmount! Exception: {0}", new object[]
				{
					ex
				});
			}
			if (num > 0)
			{
				this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
				return;
			}
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x0006CF1C File Offset: 0x0006B11C
	private void ToggleShowGolden(UIEvent e)
	{
		bool flag = this.m_showGoldenCheckbox.IsChecked();
		bool updatePage = CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS;
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.ShowCraftingModeCards(this.m_showSoulboundCheckbox.IsChecked(), flag, this.m_showDiamondCheckbox.IsChecked(), updatePage, true);
		if (flag)
		{
			SoundManager.Get().LoadAndPlay("checkbox_toggle_on.prefab:8be4c59e7387600468ac88787943da8b", base.gameObject);
			return;
		}
		SoundManager.Get().LoadAndPlay("checkbox_toggle_off.prefab:fa341d119cee1d14c941b63dba112af3", base.gameObject);
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x0006CFB0 File Offset: 0x0006B1B0
	private void ToggleShowSoulbound(UIEvent e)
	{
		bool flag = this.m_showSoulboundCheckbox.IsChecked();
		bool updatePage = CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS;
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.ShowCraftingModeCards(flag, this.m_showGoldenCheckbox.IsChecked(), this.m_showDiamondCheckbox.IsChecked(), updatePage, true);
		if (flag)
		{
			SoundManager.Get().LoadAndPlay("checkbox_toggle_on.prefab:8be4c59e7387600468ac88787943da8b", base.gameObject);
			return;
		}
		SoundManager.Get().LoadAndPlay("checkbox_toggle_off.prefab:fa341d119cee1d14c941b63dba112af3", base.gameObject);
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x0006D044 File Offset: 0x0006B244
	private void ToggleShowDiamond(UIEvent e)
	{
		bool flag = this.m_showDiamondCheckbox.IsChecked();
		bool updatePage = CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS;
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.ShowCraftingModeCards(this.m_showSoulboundCheckbox.IsChecked(), this.m_showGoldenCheckbox.IsChecked(), flag, updatePage, true);
		if (flag)
		{
			SoundManager.Get().LoadAndPlay("checkbox_toggle_on.prefab:8be4c59e7387600468ac88787943da8b", base.gameObject);
			return;
		}
		SoundManager.Get().LoadAndPlay("checkbox_toggle_off.prefab:fa341d119cee1d14c941b63dba112af3", base.gameObject);
	}

	// Token: 0x04000C40 RID: 3136
	public UIBButton m_doneButton;

	// Token: 0x04000C41 RID: 3137
	public PegUIElement m_massDisenchantButton;

	// Token: 0x04000C42 RID: 3138
	public UberText m_potentialDustAmount;

	// Token: 0x04000C43 RID: 3139
	public UberText m_massDisenchantText;

	// Token: 0x04000C44 RID: 3140
	public CheckBox m_showGoldenCheckbox;

	// Token: 0x04000C45 RID: 3141
	public CheckBox m_showSoulboundCheckbox;

	// Token: 0x04000C46 RID: 3142
	public CheckBox m_showDiamondCheckbox;

	// Token: 0x04000C47 RID: 3143
	public HighlightState m_highlight;

	// Token: 0x04000C48 RID: 3144
	public GameObject m_massDisenchantMesh;

	// Token: 0x04000C49 RID: 3145
	public Material m_massDisenchantMaterial;

	// Token: 0x04000C4A RID: 3146
	public Material m_massDisenchantDisabledMaterial;

	// Token: 0x04000C4B RID: 3147
	private int m_dustAmount;

	// Token: 0x04000C4C RID: 3148
	private bool m_shown;

	// Token: 0x04000C4D RID: 3149
	private CollectionUtils.ViewMode m_previousViewMode;

	// Token: 0x04000C4E RID: 3150
	private static CraftingTray s_instance;

	// Token: 0x04000C4F RID: 3151
	private static PlatformDependentValue<int> MASS_DISENCHANT_MATERIAL_TO_SWITCH = new PlatformDependentValue<int>(PlatformCategory.Screen)
	{
		PC = 0,
		Phone = 1
	};
}
