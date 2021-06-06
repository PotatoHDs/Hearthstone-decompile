using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.AssetManager;
using PegasusShared;
using UnityEngine;

// Token: 0x0200013C RID: 316
public class SetFilterTray : MonoBehaviour
{
	// Token: 0x060014A5 RID: 5285 RVA: 0x00075DE4 File Offset: 0x00073FE4
	private void Awake()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_toggleButton.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
			{
				this.Show(true);
			});
			this.m_hideArea.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
			{
				this.Show(false);
			});
			this.m_trayObject.SetActive(false);
		}
		else
		{
			this.m_hideArea.gameObject.SetActive(false);
		}
		this.m_toggleButton.gameObject.SetActive(false);
	}

	// Token: 0x060014A6 RID: 5286 RVA: 0x00075E5F File Offset: 0x0007405F
	public void SetButtonShown(bool isShown)
	{
		this.m_toggleButton.gameObject.SetActive(isShown);
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x00075E72 File Offset: 0x00074072
	public void SetButtonEnabled(bool isEnabled)
	{
		this.m_toggleButton.SetEnabled(isEnabled, false);
		this.m_toggleButton.SetEnabledVisual(isEnabled);
		if (this.m_setFilterButtonGlow != null)
		{
			this.m_setFilterButtonGlow.SetActive(this.m_glowEnabled && isEnabled);
		}
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x00075EAE File Offset: 0x000740AE
	public void SetFilterButtonGlowActive(bool active)
	{
		this.m_glowEnabled = active;
		if (this.m_setFilterButtonGlow != null)
		{
			this.m_setFilterButtonGlow.SetActive(active);
		}
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x00075ED4 File Offset: 0x000740D4
	public void AddHeader(string headerName, FormatType formatType)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_headerPrefab);
		GameUtils.SetParent(gameObject, this.m_contents, false);
		gameObject.SetActive(false);
		SetFilterItem component = gameObject.GetComponent<SetFilterItem>();
		UIBScrollableItem component2 = gameObject.GetComponent<UIBScrollableItem>();
		component.IsHeader = true;
		component.Text = headerName;
		component.Height = component2.m_size.z;
		component.FormatType = formatType;
		this.m_items.Add(component);
	}

	// Token: 0x060014AA RID: 5290 RVA: 0x00075F40 File Offset: 0x00074140
	public void AddItem(string itemName, string iconTextureAssetRef, UnityEngine.Vector2? iconOffset, SetFilterItem.ItemSelectedCallback callback, List<TAG_CARD_SET> data, FormatType formatType, bool isAllStandard = false)
	{
		SetFilterItem callbackData = this.AddItemUsingTexture(itemName, null, iconOffset, callback, data, null, formatType, isAllStandard, false, null, null);
		if (string.IsNullOrEmpty(iconTextureAssetRef))
		{
			return;
		}
		AssetHandleCallback<Texture> callback2 = delegate(AssetReference assetRef, AssetHandle<Texture> texture, object loadTextureCbData)
		{
			SetFilterItem setFilterItem = loadTextureCbData as SetFilterItem;
			if (setFilterItem == null)
			{
				if (texture != null)
				{
					texture.Dispose();
				}
				return;
			}
			DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
			if (disposablesCleaner != null)
			{
				disposablesCleaner.Attach(setFilterItem, texture);
			}
			setFilterItem.IconTexture = texture;
			setFilterItem.IconOffset = iconOffset;
		};
		AssetLoader.Get().LoadAsset<Texture>(iconTextureAssetRef, callback2, callbackData, AssetLoadingOptions.None);
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x00075FA0 File Offset: 0x000741A0
	public SetFilterItem AddItemUsingTexture(string itemName, Texture iconTexture, UnityEngine.Vector2? iconOffset, SetFilterItem.ItemSelectedCallback callback, List<TAG_CARD_SET> cardSets, List<int> specificCards, FormatType formatType, bool isAllStandard = false, bool tooltipActive = false, string tooltipHeadline = null, string tooltipDescription = null)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_itemPrefab);
		SetFilterItem item = gameObject.GetComponent<SetFilterItem>();
		GameUtils.SetParent(gameObject, this.m_contents, false);
		gameObject.SetActive(false);
		PegUIElement component = gameObject.GetComponent<SetFilterItem>();
		UIBScrollableItem component2 = gameObject.GetComponent<UIBScrollableItem>();
		item.IsHeader = false;
		item.Text = itemName;
		item.Height = component2.m_size.z;
		item.FormatType = formatType;
		item.IsAllStandard = isAllStandard;
		item.CardSets = cardSets;
		item.SpecificCards = specificCards;
		item.Callback = callback;
		item.IconTexture = iconTexture;
		item.IconOffset = iconOffset;
		item.TooltipHeadline = tooltipHeadline;
		item.TooltipDescription = tooltipDescription;
		item.Tooltip.ScreenConstraintLayerOverride = GameLayer.Default;
		item.ShowTooltip = tooltipActive;
		this.m_items.Add(item);
		component.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.Select(item, true, true);
		});
		return item;
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x000760DC File Offset: 0x000742DC
	public void SelectFirstItem(bool transitionPage = true)
	{
		foreach (SetFilterItem setFilterItem in this.m_items)
		{
			if (!setFilterItem.IsHeader)
			{
				UIBScrollableItem component = setFilterItem.GetComponent<UIBScrollableItem>();
				if (component != null && component.m_active == UIBScrollableItem.ActiveState.Active)
				{
					this.Select(setFilterItem, transitionPage, true);
					break;
				}
			}
		}
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x00076154 File Offset: 0x00074354
	public bool SelectFirstItemWithFormat(FormatType formatType, bool transitionPage = true)
	{
		SetFilterItem setFilterItem = (from item in this.m_items
		where !item.IsHeader && item.FormatType == formatType && item.GetComponent<UIBScrollableItem>() != null && item.GetComponent<UIBScrollableItem>().m_active == UIBScrollableItem.ActiveState.Active
		select item).FirstOrDefault<SetFilterItem>();
		if (setFilterItem == null)
		{
			return false;
		}
		this.Select(setFilterItem, transitionPage, true);
		return true;
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x000761A0 File Offset: 0x000743A0
	public bool HasActiveFilter()
	{
		foreach (SetFilterItem setFilterItem in this.m_items)
		{
			if (!setFilterItem.IsHeader && setFilterItem.isActiveAndEnabled)
			{
				if (setFilterItem == this.m_selected)
				{
					return false;
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x060014AF RID: 5295 RVA: 0x00076218 File Offset: 0x00074418
	public void Select(SetFilterItem item, bool callCallback = true, bool transitionPage = true)
	{
		if (item == this.m_selected)
		{
			return;
		}
		if (this.m_selected != null)
		{
			this.m_selected.SetSelected(false);
			this.m_lastSelected = this.m_selected;
		}
		this.m_selected = item;
		item.SetSelected(true);
		if (callCallback)
		{
			item.Callback(item.CardSets, item.SpecificCards, item.FormatType, item, transitionPage);
		}
		this.m_toggleButton.SetToggleIcon(item.IconTexture, item.IconOffset.Value);
	}

	// Token: 0x060014B0 RID: 5296 RVA: 0x000762A9 File Offset: 0x000744A9
	public void SelectPreviouslySelectedItem()
	{
		this.Select(this.m_lastSelected, false, true);
	}

	// Token: 0x060014B1 RID: 5297 RVA: 0x000762B9 File Offset: 0x000744B9
	public void UpdateSetFilters(FormatType formatType, bool editingDeck, bool showUnownedSets)
	{
		if (this.m_formatType != formatType || this.m_editingDeck != editingDeck || this.m_showUnownedSets != showUnownedSets)
		{
			this.m_formatType = formatType;
			this.m_editingDeck = editingDeck;
			this.m_showUnownedSets = showUnownedSets;
			this.Arrange();
		}
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x000762F1 File Offset: 0x000744F1
	public void ClearFilter(bool transitionPage = true)
	{
		this.SelectFirstItem(transitionPage);
		if (UniversalInputManager.UsePhoneUI)
		{
			this.SetButtonShown(false);
		}
	}

	// Token: 0x060014B3 RID: 5299 RVA: 0x00076310 File Offset: 0x00074510
	public void Show(bool show)
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			if (this.m_isAnimating)
			{
				return;
			}
			this.m_shown = show;
			this.m_trayObject.SetActive(true);
			this.m_hideArea.gameObject.SetActive(true);
			UIBHighlight component = this.m_toggleButton.GetComponent<UIBHighlight>();
			if (component != null)
			{
				component.AlwaysOver = show;
			}
			this.m_isAnimating = true;
			if (show)
			{
				this.Arrange();
				this.m_trayObject.transform.localPosition = this.m_hideBone.transform.localPosition;
				Hashtable args = iTween.Hash(new object[]
				{
					"position",
					this.m_showBone.transform.localPosition,
					"time",
					0.35f,
					"easeType",
					iTween.EaseType.easeOutCubic,
					"isLocal",
					true,
					"oncomplete",
					"FinishFilterShown",
					"oncompletetarget",
					base.gameObject
				});
				iTween.MoveTo(this.m_trayObject, args);
				SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880", base.gameObject);
			}
			else
			{
				this.m_trayObject.transform.localPosition = this.m_showBone.transform.localPosition;
				Hashtable args2 = iTween.Hash(new object[]
				{
					"position",
					this.m_hideBone.transform.localPosition,
					"time",
					0.25f,
					"easeType",
					iTween.EaseType.easeOutCubic,
					"isLocal",
					true,
					"oncomplete",
					"FinishFilterHidden",
					"oncompletetarget",
					base.gameObject
				});
				iTween.MoveTo(this.m_trayObject, args2);
				SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf", base.gameObject);
			}
			this.m_hideArea.gameObject.SetActive(this.m_shown);
		}
		else
		{
			this.m_shown = show;
			if (show)
			{
				this.Arrange();
			}
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideSetFilterTutorial();
		}
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x00076570 File Offset: 0x00074770
	public bool IsShown()
	{
		return this.m_shown;
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x00076578 File Offset: 0x00074778
	private void FinishFilterShown()
	{
		this.m_isAnimating = false;
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x00076581 File Offset: 0x00074781
	private void FinishFilterHidden()
	{
		this.m_isAnimating = false;
		this.m_trayObject.SetActive(false);
		this.m_hideArea.gameObject.SetActive(false);
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x000765A8 File Offset: 0x000747A8
	private void Arrange()
	{
		this.m_scroller.ClearVisibleAffectObjects();
		if (!this.m_showUnownedSets)
		{
			this.EvaluateOwnership();
		}
		Vector3 position = this.m_contentsBone.transform.position;
		bool flag = false;
		foreach (SetFilterItem setFilterItem in this.m_items)
		{
			UIBScrollableItem component = setFilterItem.GetComponent<UIBScrollableItem>();
			if (component == null)
			{
				Debug.LogWarning("SetFilterItem has no UIBScrollableItem component!");
			}
			else
			{
				bool flag2 = false;
				if (setFilterItem.FormatType == FormatType.FT_WILD && this.m_formatType != FormatType.FT_WILD)
				{
					flag2 = true;
				}
				else if (setFilterItem.FormatType == FormatType.FT_CLASSIC && this.m_formatType == FormatType.FT_STANDARD)
				{
					flag2 = true;
				}
				else if (setFilterItem.FormatType != FormatType.FT_CLASSIC && this.m_formatType == FormatType.FT_CLASSIC)
				{
					flag2 = true;
				}
				else if (this.m_editingDeck && setFilterItem.IsAllStandard && this.m_formatType == FormatType.FT_WILD)
				{
					flag2 = true;
				}
				else if (this.m_editingDeck && setFilterItem.FormatType == FormatType.FT_CLASSIC && this.m_formatType != FormatType.FT_CLASSIC)
				{
					flag2 = true;
				}
				else if (!this.m_showUnownedSets && !this.OwnCardInSetsForItem(setFilterItem))
				{
					flag2 = true;
				}
				if (flag2)
				{
					if (setFilterItem == this.m_selected)
					{
						flag = true;
					}
					setFilterItem.gameObject.SetActive(false);
					component.m_active = UIBScrollableItem.ActiveState.Inactive;
				}
				else
				{
					setFilterItem.gameObject.SetActive(true);
					component.m_active = UIBScrollableItem.ActiveState.Active;
					setFilterItem.gameObject.transform.position = position;
					position.z -= setFilterItem.Height;
					this.m_scroller.AddVisibleAffectedObject(setFilterItem.gameObject, new Vector3(setFilterItem.Height, setFilterItem.Height, setFilterItem.Height), true, null);
				}
			}
		}
		if (flag)
		{
			this.SelectFirstItem(true);
		}
		this.m_scroller.UpdateAndFireVisibleAffectedObjects();
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x0007678C File Offset: 0x0007498C
	private void EvaluateOwnership()
	{
		if (this.m_lastCollectionQueryTime > CollectionManager.Get().CollectionLastModifiedTime())
		{
			return;
		}
		this.m_setsWithOwnedCards.Clear();
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		List<CollectibleCard> allCards = CollectionManager.Get().GetAllCards();
		for (int i = 0; i < allCards.Count; i++)
		{
			CollectibleCard collectibleCard = allCards[i];
			if (collectibleCard.OwnedCount > 0)
			{
				this.m_setsWithOwnedCards.Add(collectibleCard.Set);
			}
		}
		Log.Performance.Print("SetFilterTray - Evaluating Ownership took {0} seconds.", new object[]
		{
			Time.realtimeSinceStartup - realtimeSinceStartup
		});
		this.m_lastCollectionQueryTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x0007682C File Offset: 0x00074A2C
	private bool OwnCardInSetsForItem(SetFilterItem item)
	{
		if (item.CardSets == null)
		{
			return true;
		}
		for (int i = 0; i < item.CardSets.Count; i++)
		{
			if (this.m_setsWithOwnedCards.Contains(item.CardSets[i]))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060014BA RID: 5306 RVA: 0x00076875 File Offset: 0x00074A75
	public void RemoveAllItems()
	{
		this.m_items.Clear();
	}

	// Token: 0x04000DCF RID: 3535
	public UIBScrollable m_scroller;

	// Token: 0x04000DD0 RID: 3536
	public GameObject m_contents;

	// Token: 0x04000DD1 RID: 3537
	public CollectionSetFilterDropdownToggle m_toggleButton;

	// Token: 0x04000DD2 RID: 3538
	public PegUIElement m_hideArea;

	// Token: 0x04000DD3 RID: 3539
	public GameObject m_trayObject;

	// Token: 0x04000DD4 RID: 3540
	public GameObject m_contentsBone;

	// Token: 0x04000DD5 RID: 3541
	public GameObject m_headerPrefab;

	// Token: 0x04000DD6 RID: 3542
	public GameObject m_itemPrefab;

	// Token: 0x04000DD7 RID: 3543
	public GameObject m_showBone;

	// Token: 0x04000DD8 RID: 3544
	public GameObject m_hideBone;

	// Token: 0x04000DD9 RID: 3545
	public GameObject m_setFilterButtonGlow;

	// Token: 0x04000DDA RID: 3546
	private bool m_shown;

	// Token: 0x04000DDB RID: 3547
	private FormatType m_formatType = FormatType.FT_WILD;

	// Token: 0x04000DDC RID: 3548
	private bool m_editingDeck;

	// Token: 0x04000DDD RID: 3549
	private bool m_showUnownedSets;

	// Token: 0x04000DDE RID: 3550
	private bool m_isAnimating;

	// Token: 0x04000DDF RID: 3551
	private bool m_glowEnabled;

	// Token: 0x04000DE0 RID: 3552
	private List<SetFilterItem> m_items = new List<SetFilterItem>();

	// Token: 0x04000DE1 RID: 3553
	private float m_lastCollectionQueryTime;

	// Token: 0x04000DE2 RID: 3554
	private HashSet<TAG_CARD_SET> m_setsWithOwnedCards = new HashSet<TAG_CARD_SET>();

	// Token: 0x04000DE3 RID: 3555
	private SetFilterItem m_selected;

	// Token: 0x04000DE4 RID: 3556
	private SetFilterItem m_lastSelected;
}
