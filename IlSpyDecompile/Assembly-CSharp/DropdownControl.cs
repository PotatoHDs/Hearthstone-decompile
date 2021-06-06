using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class DropdownControl : PegUIElement
{
	public delegate void itemChosenCallback(object selection, object prevSelection);

	public delegate string itemTextCallback(object val);

	public delegate void menuShownCallback(bool shown);

	[CustomEditField(Sections = "Buttons")]
	public DropdownMenuItem m_selectedItem;

	[CustomEditField(Sections = "Buttons")]
	public PegUIElement m_cancelCatcher;

	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_button;

	[CustomEditField(Sections = "Menu")]
	public MultiSliceElement m_menu;

	[CustomEditField(Sections = "Menu")]
	public GameObject m_menuMiddle;

	[CustomEditField(Sections = "Menu")]
	public MultiSliceElement m_menuItemContainer;

	[CustomEditField(Sections = "Menu Templates")]
	public DropdownMenuItem m_menuItemTemplate;

	private string m_unselectedItemText = string.Empty;

	private itemChosenCallback m_itemChosenCallback = delegate
	{
	};

	private itemTextCallback m_itemTextCallback = defaultItemTextCallback;

	private menuShownCallback m_menuShownCallback = delegate
	{
	};

	private List<DropdownMenuItem> m_items = new List<DropdownMenuItem>();

	private Font m_overrideFont;

	public void Start()
	{
		m_button.AddEventListener(UIEventType.RELEASE, delegate
		{
			onUserPressedButton();
		});
		m_selectedItem.AddEventListener(UIEventType.RELEASE, delegate
		{
			onUserPressedSelection(m_selectedItem);
		});
		m_cancelCatcher.AddEventListener(UIEventType.RELEASE, delegate
		{
			onUserCancelled();
		});
		hideMenu();
	}

	public void addItem(object value)
	{
		DropdownMenuItem item = (DropdownMenuItem)GameUtils.Instantiate(m_menuItemTemplate, m_menuItemContainer.gameObject);
		item.gameObject.transform.localRotation = m_menuItemTemplate.transform.localRotation;
		item.gameObject.transform.localScale = m_menuItemTemplate.transform.localScale;
		m_items.Add(item);
		if (m_overrideFont != null)
		{
			item.m_text.TrueTypeFont = m_overrideFont;
		}
		item.SetValue(value, m_itemTextCallback(value));
		item.AddEventListener(UIEventType.RELEASE, delegate
		{
			onUserItemClicked(item);
		});
		item.gameObject.SetActive(value: true);
		layoutMenu();
	}

	public bool removeItem(object value)
	{
		int num = findItemIndex(value);
		if (num < 0)
		{
			return false;
		}
		DropdownMenuItem dropdownMenuItem = m_items[num];
		m_items.RemoveAt(num);
		Object.Destroy(dropdownMenuItem.gameObject);
		layoutMenu();
		return true;
	}

	public void clearItems()
	{
		foreach (DropdownMenuItem item in m_items)
		{
			Object.Destroy(item.gameObject);
		}
		layoutMenu();
	}

	public void setSelectionToLastItem()
	{
		m_selectedItem.SetValue(null, m_unselectedItemText);
		if (m_items.Count != 0)
		{
			for (int i = 0; i < m_items.Count - 1; i++)
			{
				m_items[i].SetSelected(selected: false);
			}
			DropdownMenuItem dropdownMenuItem = m_items[m_items.Count - 1];
			dropdownMenuItem.SetSelected(selected: true);
			m_selectedItem.SetValue(dropdownMenuItem.GetValue(), m_itemTextCallback(dropdownMenuItem.GetValue()));
		}
	}

	public void setSelectionToFirstItem()
	{
		m_selectedItem.SetValue(null, m_unselectedItemText);
		if (m_items.Count != 0)
		{
			for (int i = 0; i < m_items.Count - 1; i++)
			{
				m_items[i].SetSelected(selected: false);
			}
			DropdownMenuItem dropdownMenuItem = m_items[0];
			dropdownMenuItem.SetSelected(selected: true);
			m_selectedItem.SetValue(dropdownMenuItem.GetValue(), m_itemTextCallback(dropdownMenuItem.GetValue()));
		}
	}

	public object getSelection()
	{
		return m_selectedItem.GetValue();
	}

	public void setSelection(object val)
	{
		m_selectedItem.SetValue(null, m_unselectedItemText);
		for (int i = 0; i < m_items.Count; i++)
		{
			DropdownMenuItem dropdownMenuItem = m_items[i];
			object value = dropdownMenuItem.GetValue();
			if ((value == null && val == null) || value.Equals(val))
			{
				dropdownMenuItem.SetSelected(selected: true);
				m_selectedItem.SetValue(value, m_itemTextCallback(value));
			}
			else
			{
				dropdownMenuItem.SetSelected(selected: false);
			}
		}
	}

	public void onUserPressedButton()
	{
		showMenu();
	}

	public void onUserPressedSelection(DropdownMenuItem item)
	{
		showMenu();
	}

	public void onUserItemClicked(DropdownMenuItem item)
	{
		hideMenu();
		object selection = getSelection();
		object value = item.GetValue();
		setSelection(value);
		m_itemChosenCallback(value, selection);
	}

	public void onUserCancelled()
	{
		if (SoundManager.Get().IsInitialized())
		{
			SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		}
		hideMenu();
	}

	public void setUnselectedItemText(string text)
	{
		m_unselectedItemText = text;
	}

	public itemChosenCallback getItemChosenCallback()
	{
		return m_itemChosenCallback;
	}

	public void setItemChosenCallback(itemChosenCallback callback)
	{
		m_itemChosenCallback = callback ?? ((itemChosenCallback)delegate
		{
		});
	}

	public itemTextCallback getItemTextCallback()
	{
		return m_itemTextCallback;
	}

	public void setItemTextCallback(itemTextCallback callback)
	{
		m_itemTextCallback = callback ?? new itemTextCallback(defaultItemTextCallback);
	}

	public static string defaultItemTextCallback(object val)
	{
		if (val != null)
		{
			return val.ToString();
		}
		return string.Empty;
	}

	public bool isMenuShown()
	{
		return m_menu.gameObject.activeInHierarchy;
	}

	public menuShownCallback getMenuShownCallback()
	{
		return m_menuShownCallback;
	}

	public void setMenuShownCallback(menuShownCallback callback)
	{
		m_menuShownCallback = callback;
	}

	public void setFont(Font font)
	{
		m_overrideFont = font;
		m_selectedItem.m_text.TrueTypeFont = font;
		m_menuItemTemplate.m_text.TrueTypeFont = font;
	}

	private void showMenu()
	{
		m_cancelCatcher.gameObject.SetActive(value: true);
		m_menu.gameObject.SetActive(value: true);
		layoutMenu();
		m_menuShownCallback(shown: true);
	}

	private void hideMenu()
	{
		m_cancelCatcher.gameObject.SetActive(value: false);
		m_menu.gameObject.SetActive(value: false);
		m_menuShownCallback(shown: false);
	}

	private void layoutMenu()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		m_menuItemTemplate.gameObject.SetActive(value: true);
		OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(m_menuItemTemplate.gameObject);
		if (orientedBounds != null)
		{
			float num = orientedBounds.Extents[1].magnitude * 2f;
			m_menuItemTemplate.gameObject.SetActive(value: false);
			m_menuItemContainer.ClearSlices();
			for (int i = 0; i < m_items.Count; i++)
			{
				m_menuItemContainer.AddSlice(m_items[i].gameObject);
			}
			m_menuItemContainer.UpdateSlices();
			if (m_items.Count <= 1)
			{
				TransformUtil.SetLocalScaleZ(m_menuMiddle, 0.001f);
			}
			else
			{
				TransformUtil.SetLocalScaleToWorldDimension(m_menuMiddle, new WorldDimensionIndex(num * (float)(m_items.Count - 1), 2));
			}
			m_menu.UpdateSlices();
		}
	}

	private int findItemIndex(object value)
	{
		for (int i = 0; i < m_items.Count; i++)
		{
			if (m_items[i].GetValue() == value)
			{
				return i;
			}
		}
		return -1;
	}

	private DropdownMenuItem findItem(object value)
	{
		for (int i = 0; i < m_items.Count; i++)
		{
			DropdownMenuItem dropdownMenuItem = m_items[i];
			if (dropdownMenuItem.GetValue() == value)
			{
				return dropdownMenuItem;
			}
		}
		return null;
	}
}
