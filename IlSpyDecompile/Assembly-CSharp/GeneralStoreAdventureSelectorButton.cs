using System.Collections.Generic;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public class GeneralStoreAdventureSelectorButton : PegUIElement
{
	public UberText m_adventureTitle;

	public HighlightState m_highlight;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_selectSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_unselectSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_mouseOverSound;

	public TooltipZone m_unavailableTooltip;

	public GameLayer m_unavailableTooltipLayer = GameLayer.PerspectiveUI;

	public float m_unavailableTooltipScale = 20f;

	public GameObject m_preorderRibbon;

	private bool m_selected;

	private AdventureDbId m_adventureId;

	public void SetAdventureId(AdventureDbId adventureId)
	{
		if (m_adventureTitle != null)
		{
			AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)adventureId);
			if (record != null)
			{
				m_adventureTitle.Text = record.StoreBuyButtonLabel;
			}
		}
		m_adventureId = adventureId;
		UpdateState();
	}

	public AdventureDbId GetAdventureId()
	{
		return m_adventureId;
	}

	public void Select()
	{
		if (!m_selected)
		{
			m_selected = true;
			m_highlight.ChangeState((GetInteractionState() == InteractionState.Up) ? ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE : ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			if (!string.IsNullOrEmpty(m_selectSound))
			{
				SoundManager.Get().LoadAndPlay(m_selectSound);
			}
		}
	}

	public void Unselect()
	{
		if (m_selected)
		{
			m_selected = false;
			m_highlight.ChangeState(ActorStateType.NONE);
			if (!string.IsNullOrEmpty(m_unselectSound))
			{
				SoundManager.Get().LoadAndPlay(m_unselectSound);
			}
		}
	}

	public bool IsPrePurchase()
	{
		Network.Bundle bundle = null;
		StoreManager.Get().GetAvailableAdventureBundle(m_adventureId, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, out bundle);
		if (bundle != null)
		{
			return StoreManager.Get().IsProductPrePurchase(bundle);
		}
		return false;
	}

	public void UpdateState()
	{
		if (m_preorderRibbon != null)
		{
			m_preorderRibbon.SetActive(IsPrePurchase());
		}
	}

	public bool IsPurchasable()
	{
		ProductType adventureProductType = StoreManager.GetAdventureProductType(m_adventureId);
		if (adventureProductType == ProductType.PRODUCT_TYPE_UNKNOWN)
		{
			return false;
		}
		List<Network.Bundle> availableBundlesForProduct = StoreManager.Get().GetAvailableBundlesForProduct(adventureProductType, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION);
		if (availableBundlesForProduct != null)
		{
			return availableBundlesForProduct.Count > 0;
		}
		return false;
	}

	public bool IsAvailable()
	{
		StoreManager.Get().GetAvailableAdventureBundle(m_adventureId, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, out var bundle);
		return bundle != null;
	}

	protected override void OnOver(InteractionState oldState)
	{
		base.OnOver(oldState);
		if (IsAvailable())
		{
			m_highlight.ChangeState(ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE);
			if (!string.IsNullOrEmpty(m_mouseOverSound))
			{
				SoundManager.Get().LoadAndPlay(m_mouseOverSound);
			}
		}
		else if (m_unavailableTooltip != null)
		{
			SceneUtils.SetLayer(m_unavailableTooltip.ShowTooltip(GameStrings.Get("GLUE_STORE_ADVENTURE_BUTTON_UNAVAILABLE_HEADLINE"), GameStrings.Get("GLUE_STORE_ADVENTURE_BUTTON_UNAVAILABLE_DESCRIPTION"), m_unavailableTooltipScale), m_unavailableTooltipLayer);
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		base.OnOut(oldState);
		if (IsAvailable())
		{
			m_highlight.ChangeState(m_selected ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.NONE);
		}
		else if (m_unavailableTooltip != null)
		{
			m_unavailableTooltip.HideTooltip();
		}
	}

	protected override void OnRelease()
	{
		base.OnRelease();
		if (IsAvailable())
		{
			m_highlight.ChangeState(ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE);
		}
	}

	protected override void OnPress()
	{
		base.OnPress();
		if (IsAvailable())
		{
			m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
	}
}
