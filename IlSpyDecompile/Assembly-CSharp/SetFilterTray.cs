using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.AssetManager;
using PegasusShared;
using UnityEngine;

public class SetFilterTray : MonoBehaviour
{
	public UIBScrollable m_scroller;

	public GameObject m_contents;

	public CollectionSetFilterDropdownToggle m_toggleButton;

	public PegUIElement m_hideArea;

	public GameObject m_trayObject;

	public GameObject m_contentsBone;

	public GameObject m_headerPrefab;

	public GameObject m_itemPrefab;

	public GameObject m_showBone;

	public GameObject m_hideBone;

	public GameObject m_setFilterButtonGlow;

	private bool m_shown;

	private FormatType m_formatType = FormatType.FT_WILD;

	private bool m_editingDeck;

	private bool m_showUnownedSets;

	private bool m_isAnimating;

	private bool m_glowEnabled;

	private List<SetFilterItem> m_items = new List<SetFilterItem>();

	private float m_lastCollectionQueryTime;

	private HashSet<TAG_CARD_SET> m_setsWithOwnedCards = new HashSet<TAG_CARD_SET>();

	private SetFilterItem m_selected;

	private SetFilterItem m_lastSelected;

	private void Awake()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			m_toggleButton.AddEventListener(UIEventType.PRESS, delegate
			{
				Show(show: true);
			});
			m_hideArea.AddEventListener(UIEventType.PRESS, delegate
			{
				Show(show: false);
			});
			m_trayObject.SetActive(value: false);
		}
		else
		{
			m_hideArea.gameObject.SetActive(value: false);
		}
		m_toggleButton.gameObject.SetActive(value: false);
	}

	public void SetButtonShown(bool isShown)
	{
		m_toggleButton.gameObject.SetActive(isShown);
	}

	public void SetButtonEnabled(bool isEnabled)
	{
		m_toggleButton.SetEnabled(isEnabled);
		m_toggleButton.SetEnabledVisual(isEnabled);
		if (m_setFilterButtonGlow != null)
		{
			m_setFilterButtonGlow.SetActive(m_glowEnabled && isEnabled);
		}
	}

	public void SetFilterButtonGlowActive(bool active)
	{
		m_glowEnabled = active;
		if (m_setFilterButtonGlow != null)
		{
			m_setFilterButtonGlow.SetActive(active);
		}
	}

	public void AddHeader(string headerName, FormatType formatType)
	{
		GameObject obj = Object.Instantiate(m_headerPrefab);
		GameUtils.SetParent(obj, m_contents);
		obj.SetActive(value: false);
		SetFilterItem component = obj.GetComponent<SetFilterItem>();
		UIBScrollableItem component2 = obj.GetComponent<UIBScrollableItem>();
		component.IsHeader = true;
		component.Text = headerName;
		component.Height = component2.m_size.z;
		component.FormatType = formatType;
		m_items.Add(component);
	}

	public void AddItem(string itemName, string iconTextureAssetRef, UnityEngine.Vector2? iconOffset, SetFilterItem.ItemSelectedCallback callback, List<TAG_CARD_SET> data, FormatType formatType, bool isAllStandard = false)
	{
		SetFilterItem callbackData = AddItemUsingTexture(itemName, null, iconOffset, callback, data, null, formatType, isAllStandard);
		if (string.IsNullOrEmpty(iconTextureAssetRef))
		{
			return;
		}
		AssetHandleCallback<Texture> callback2 = delegate(AssetReference assetRef, AssetHandle<Texture> texture, object loadTextureCbData)
		{
			SetFilterItem setFilterItem = loadTextureCbData as SetFilterItem;
			if (setFilterItem == null)
			{
				texture?.Dispose();
			}
			else
			{
				HearthstoneServices.Get<DisposablesCleaner>()?.Attach(setFilterItem, texture);
				setFilterItem.IconTexture = texture;
				setFilterItem.IconOffset = iconOffset;
			}
		};
		AssetLoader.Get().LoadAsset(iconTextureAssetRef, callback2, callbackData);
	}

	public SetFilterItem AddItemUsingTexture(string itemName, Texture iconTexture, UnityEngine.Vector2? iconOffset, SetFilterItem.ItemSelectedCallback callback, List<TAG_CARD_SET> cardSets, List<int> specificCards, FormatType formatType, bool isAllStandard = false, bool tooltipActive = false, string tooltipHeadline = null, string tooltipDescription = null)
	{
		GameObject gameObject = Object.Instantiate(m_itemPrefab);
		SetFilterItem item = gameObject.GetComponent<SetFilterItem>();
		GameUtils.SetParent(gameObject, m_contents);
		gameObject.SetActive(value: false);
		SetFilterItem component = gameObject.GetComponent<SetFilterItem>();
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
		m_items.Add(item);
		component.AddEventListener(UIEventType.RELEASE, delegate
		{
			Select(item);
		});
		return item;
	}

	public void SelectFirstItem(bool transitionPage = true)
	{
		foreach (SetFilterItem item in m_items)
		{
			if (!item.IsHeader)
			{
				UIBScrollableItem component = item.GetComponent<UIBScrollableItem>();
				if (component != null && component.m_active == UIBScrollableItem.ActiveState.Active)
				{
					Select(item, transitionPage);
					break;
				}
			}
		}
	}

	public bool SelectFirstItemWithFormat(FormatType formatType, bool transitionPage = true)
	{
		SetFilterItem setFilterItem = m_items.Where((SetFilterItem item) => !item.IsHeader && item.FormatType == formatType && item.GetComponent<UIBScrollableItem>() != null && item.GetComponent<UIBScrollableItem>().m_active == UIBScrollableItem.ActiveState.Active).FirstOrDefault();
		if (setFilterItem == null)
		{
			return false;
		}
		Select(setFilterItem, transitionPage);
		return true;
	}

	public bool HasActiveFilter()
	{
		foreach (SetFilterItem item in m_items)
		{
			if (!item.IsHeader && item.isActiveAndEnabled)
			{
				if (item == m_selected)
				{
					return false;
				}
				return true;
			}
		}
		return false;
	}

	public void Select(SetFilterItem item, bool callCallback = true, bool transitionPage = true)
	{
		if (!(item == m_selected))
		{
			if (m_selected != null)
			{
				m_selected.SetSelected(selected: false);
				m_lastSelected = m_selected;
			}
			m_selected = item;
			item.SetSelected(selected: true);
			if (callCallback)
			{
				item.Callback(item.CardSets, item.SpecificCards, item.FormatType, item, transitionPage);
			}
			m_toggleButton.SetToggleIcon(item.IconTexture, item.IconOffset.Value);
		}
	}

	public void SelectPreviouslySelectedItem()
	{
		Select(m_lastSelected, callCallback: false);
	}

	public void UpdateSetFilters(FormatType formatType, bool editingDeck, bool showUnownedSets)
	{
		if (m_formatType != formatType || m_editingDeck != editingDeck || m_showUnownedSets != showUnownedSets)
		{
			m_formatType = formatType;
			m_editingDeck = editingDeck;
			m_showUnownedSets = showUnownedSets;
			Arrange();
		}
	}

	public void ClearFilter(bool transitionPage = true)
	{
		SelectFirstItem(transitionPage);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			SetButtonShown(isShown: false);
		}
	}

	public void Show(bool show)
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			if (m_isAnimating)
			{
				return;
			}
			m_shown = show;
			m_trayObject.SetActive(value: true);
			m_hideArea.gameObject.SetActive(value: true);
			UIBHighlight component = m_toggleButton.GetComponent<UIBHighlight>();
			if (component != null)
			{
				component.AlwaysOver = show;
			}
			m_isAnimating = true;
			if (show)
			{
				Arrange();
				m_trayObject.transform.localPosition = m_hideBone.transform.localPosition;
				Hashtable args = iTween.Hash("position", m_showBone.transform.localPosition, "time", 0.35f, "easeType", iTween.EaseType.easeOutCubic, "isLocal", true, "oncomplete", "FinishFilterShown", "oncompletetarget", base.gameObject);
				iTween.MoveTo(m_trayObject, args);
				SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880", base.gameObject);
			}
			else
			{
				m_trayObject.transform.localPosition = m_showBone.transform.localPosition;
				Hashtable args2 = iTween.Hash("position", m_hideBone.transform.localPosition, "time", 0.25f, "easeType", iTween.EaseType.easeOutCubic, "isLocal", true, "oncomplete", "FinishFilterHidden", "oncompletetarget", base.gameObject);
				iTween.MoveTo(m_trayObject, args2);
				SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf", base.gameObject);
			}
			m_hideArea.gameObject.SetActive(m_shown);
		}
		else
		{
			m_shown = show;
			if (show)
			{
				Arrange();
			}
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideSetFilterTutorial();
		}
	}

	public bool IsShown()
	{
		return m_shown;
	}

	private void FinishFilterShown()
	{
		m_isAnimating = false;
	}

	private void FinishFilterHidden()
	{
		m_isAnimating = false;
		m_trayObject.SetActive(value: false);
		m_hideArea.gameObject.SetActive(value: false);
	}

	private void Arrange()
	{
		m_scroller.ClearVisibleAffectObjects();
		if (!m_showUnownedSets)
		{
			EvaluateOwnership();
		}
		Vector3 position = m_contentsBone.transform.position;
		bool flag = false;
		foreach (SetFilterItem item in m_items)
		{
			UIBScrollableItem component = item.GetComponent<UIBScrollableItem>();
			if (component == null)
			{
				Debug.LogWarning("SetFilterItem has no UIBScrollableItem component!");
				continue;
			}
			bool flag2 = false;
			if (item.FormatType == FormatType.FT_WILD && m_formatType != FormatType.FT_WILD)
			{
				flag2 = true;
			}
			else if (item.FormatType == FormatType.FT_CLASSIC && m_formatType == FormatType.FT_STANDARD)
			{
				flag2 = true;
			}
			else if (item.FormatType != FormatType.FT_CLASSIC && m_formatType == FormatType.FT_CLASSIC)
			{
				flag2 = true;
			}
			else if (m_editingDeck && item.IsAllStandard && m_formatType == FormatType.FT_WILD)
			{
				flag2 = true;
			}
			else if (m_editingDeck && item.FormatType == FormatType.FT_CLASSIC && m_formatType != FormatType.FT_CLASSIC)
			{
				flag2 = true;
			}
			else if (!m_showUnownedSets && !OwnCardInSetsForItem(item))
			{
				flag2 = true;
			}
			if (flag2)
			{
				if (item == m_selected)
				{
					flag = true;
				}
				item.gameObject.SetActive(value: false);
				component.m_active = UIBScrollableItem.ActiveState.Inactive;
			}
			else
			{
				item.gameObject.SetActive(value: true);
				component.m_active = UIBScrollableItem.ActiveState.Active;
				item.gameObject.transform.position = position;
				position.z -= item.Height;
				m_scroller.AddVisibleAffectedObject(item.gameObject, new Vector3(item.Height, item.Height, item.Height), visible: true);
			}
		}
		if (flag)
		{
			SelectFirstItem();
		}
		m_scroller.UpdateAndFireVisibleAffectedObjects();
	}

	private void EvaluateOwnership()
	{
		if (m_lastCollectionQueryTime > CollectionManager.Get().CollectionLastModifiedTime())
		{
			return;
		}
		m_setsWithOwnedCards.Clear();
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		List<CollectibleCard> allCards = CollectionManager.Get().GetAllCards();
		for (int i = 0; i < allCards.Count; i++)
		{
			CollectibleCard collectibleCard = allCards[i];
			if (collectibleCard.OwnedCount > 0)
			{
				m_setsWithOwnedCards.Add(collectibleCard.Set);
			}
		}
		Log.Performance.Print("SetFilterTray - Evaluating Ownership took {0} seconds.", Time.realtimeSinceStartup - realtimeSinceStartup);
		m_lastCollectionQueryTime = Time.realtimeSinceStartup;
	}

	private bool OwnCardInSetsForItem(SetFilterItem item)
	{
		if (item.CardSets == null)
		{
			return true;
		}
		for (int i = 0; i < item.CardSets.Count; i++)
		{
			if (m_setsWithOwnedCards.Contains(item.CardSets[i]))
			{
				return true;
			}
		}
		return false;
	}

	public void RemoveAllItems()
	{
		m_items.Clear();
	}
}
