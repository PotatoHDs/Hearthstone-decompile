using System;
using System.Collections.Generic;
using PegasusUtil;
using UnityEngine;

// Token: 0x020006F6 RID: 1782
[CustomEditClass]
public class GeneralStoreAdventureSelectorButton : PegUIElement
{
	// Token: 0x06006367 RID: 25447 RVA: 0x002067F8 File Offset: 0x002049F8
	public void SetAdventureId(AdventureDbId adventureId)
	{
		if (this.m_adventureTitle != null)
		{
			AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)adventureId);
			if (record != null)
			{
				this.m_adventureTitle.Text = record.StoreBuyButtonLabel;
			}
		}
		this.m_adventureId = adventureId;
		this.UpdateState();
	}

	// Token: 0x06006368 RID: 25448 RVA: 0x00206845 File Offset: 0x00204A45
	public AdventureDbId GetAdventureId()
	{
		return this.m_adventureId;
	}

	// Token: 0x06006369 RID: 25449 RVA: 0x00206850 File Offset: 0x00204A50
	public void Select()
	{
		if (this.m_selected)
		{
			return;
		}
		this.m_selected = true;
		this.m_highlight.ChangeState((base.GetInteractionState() == PegUIElement.InteractionState.Up) ? ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE : ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		if (!string.IsNullOrEmpty(this.m_selectSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_selectSound);
		}
	}

	// Token: 0x0600636A RID: 25450 RVA: 0x002068AC File Offset: 0x00204AAC
	public void Unselect()
	{
		if (!this.m_selected)
		{
			return;
		}
		this.m_selected = false;
		this.m_highlight.ChangeState(ActorStateType.NONE);
		if (!string.IsNullOrEmpty(this.m_unselectSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_unselectSound);
		}
	}

	// Token: 0x0600636B RID: 25451 RVA: 0x002068F8 File Offset: 0x00204AF8
	public bool IsPrePurchase()
	{
		Network.Bundle bundle = null;
		StoreManager.Get().GetAvailableAdventureBundle(this.m_adventureId, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, out bundle);
		return bundle != null && StoreManager.Get().IsProductPrePurchase(bundle);
	}

	// Token: 0x0600636C RID: 25452 RVA: 0x0020692F File Offset: 0x00204B2F
	public void UpdateState()
	{
		if (this.m_preorderRibbon != null)
		{
			this.m_preorderRibbon.SetActive(this.IsPrePurchase());
		}
	}

	// Token: 0x0600636D RID: 25453 RVA: 0x00206950 File Offset: 0x00204B50
	public bool IsPurchasable()
	{
		ProductType adventureProductType = StoreManager.GetAdventureProductType(this.m_adventureId);
		if (adventureProductType == ProductType.PRODUCT_TYPE_UNKNOWN)
		{
			return false;
		}
		List<Network.Bundle> availableBundlesForProduct = StoreManager.Get().GetAvailableBundlesForProduct(adventureProductType, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, 0, 0);
		return availableBundlesForProduct != null && availableBundlesForProduct.Count > 0;
	}

	// Token: 0x0600636E RID: 25454 RVA: 0x00206990 File Offset: 0x00204B90
	public bool IsAvailable()
	{
		Network.Bundle bundle;
		StoreManager.Get().GetAvailableAdventureBundle(this.m_adventureId, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, out bundle);
		return bundle != null;
	}

	// Token: 0x0600636F RID: 25455 RVA: 0x002069BC File Offset: 0x00204BBC
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		base.OnOver(oldState);
		if (this.IsAvailable())
		{
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE);
			if (!string.IsNullOrEmpty(this.m_mouseOverSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_mouseOverSound);
				return;
			}
		}
		else if (this.m_unavailableTooltip != null)
		{
			SceneUtils.SetLayer(this.m_unavailableTooltip.ShowTooltip(GameStrings.Get("GLUE_STORE_ADVENTURE_BUTTON_UNAVAILABLE_HEADLINE"), GameStrings.Get("GLUE_STORE_ADVENTURE_BUTTON_UNAVAILABLE_DESCRIPTION"), this.m_unavailableTooltipScale, 0), this.m_unavailableTooltipLayer);
		}
	}

	// Token: 0x06006370 RID: 25456 RVA: 0x00206A48 File Offset: 0x00204C48
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		base.OnOut(oldState);
		if (this.IsAvailable())
		{
			this.m_highlight.ChangeState(this.m_selected ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.NONE);
			return;
		}
		if (this.m_unavailableTooltip != null)
		{
			this.m_unavailableTooltip.HideTooltip();
		}
	}

	// Token: 0x06006371 RID: 25457 RVA: 0x00206A97 File Offset: 0x00204C97
	protected override void OnRelease()
	{
		base.OnRelease();
		if (this.IsAvailable())
		{
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE);
		}
	}

	// Token: 0x06006372 RID: 25458 RVA: 0x00206AB5 File Offset: 0x00204CB5
	protected override void OnPress()
	{
		base.OnPress();
		if (this.IsAvailable())
		{
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
	}

	// Token: 0x04005273 RID: 21107
	public UberText m_adventureTitle;

	// Token: 0x04005274 RID: 21108
	public HighlightState m_highlight;

	// Token: 0x04005275 RID: 21109
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_selectSound;

	// Token: 0x04005276 RID: 21110
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_unselectSound;

	// Token: 0x04005277 RID: 21111
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_mouseOverSound;

	// Token: 0x04005278 RID: 21112
	public TooltipZone m_unavailableTooltip;

	// Token: 0x04005279 RID: 21113
	public GameLayer m_unavailableTooltipLayer = GameLayer.PerspectiveUI;

	// Token: 0x0400527A RID: 21114
	public float m_unavailableTooltipScale = 20f;

	// Token: 0x0400527B RID: 21115
	public GameObject m_preorderRibbon;

	// Token: 0x0400527C RID: 21116
	private bool m_selected;

	// Token: 0x0400527D RID: 21117
	private AdventureDbId m_adventureId;
}
