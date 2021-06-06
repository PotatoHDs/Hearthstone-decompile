using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B0D RID: 2829
[CustomEditClass]
public class DropdownControl : PegUIElement
{
	// Token: 0x06009678 RID: 38520 RVA: 0x0030B5A8 File Offset: 0x003097A8
	public void Start()
	{
		this.m_button.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.onUserPressedButton();
		});
		this.m_selectedItem.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.onUserPressedSelection(this.m_selectedItem);
		});
		this.m_cancelCatcher.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.onUserCancelled();
		});
		this.hideMenu();
	}

	// Token: 0x06009679 RID: 38521 RVA: 0x0030B608 File Offset: 0x00309808
	public void addItem(object value)
	{
		DropdownMenuItem item = (DropdownMenuItem)GameUtils.Instantiate(this.m_menuItemTemplate, this.m_menuItemContainer.gameObject, false);
		item.gameObject.transform.localRotation = this.m_menuItemTemplate.transform.localRotation;
		item.gameObject.transform.localScale = this.m_menuItemTemplate.transform.localScale;
		this.m_items.Add(item);
		if (this.m_overrideFont != null)
		{
			item.m_text.TrueTypeFont = this.m_overrideFont;
		}
		item.SetValue(value, this.m_itemTextCallback(value));
		item.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.onUserItemClicked(item);
		});
		item.gameObject.SetActive(true);
		this.layoutMenu();
	}

	// Token: 0x0600967A RID: 38522 RVA: 0x0030B70C File Offset: 0x0030990C
	public bool removeItem(object value)
	{
		int num = this.findItemIndex(value);
		if (num < 0)
		{
			return false;
		}
		Component component = this.m_items[num];
		this.m_items.RemoveAt(num);
		UnityEngine.Object.Destroy(component.gameObject);
		this.layoutMenu();
		return true;
	}

	// Token: 0x0600967B RID: 38523 RVA: 0x0030B750 File Offset: 0x00309950
	public void clearItems()
	{
		foreach (DropdownMenuItem dropdownMenuItem in this.m_items)
		{
			UnityEngine.Object.Destroy(dropdownMenuItem.gameObject);
		}
		this.layoutMenu();
	}

	// Token: 0x0600967C RID: 38524 RVA: 0x0030B7AC File Offset: 0x003099AC
	public void setSelectionToLastItem()
	{
		this.m_selectedItem.SetValue(null, this.m_unselectedItemText);
		if (this.m_items.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.m_items.Count - 1; i++)
		{
			this.m_items[i].SetSelected(false);
		}
		DropdownMenuItem dropdownMenuItem = this.m_items[this.m_items.Count - 1];
		dropdownMenuItem.SetSelected(true);
		this.m_selectedItem.SetValue(dropdownMenuItem.GetValue(), this.m_itemTextCallback(dropdownMenuItem.GetValue()));
	}

	// Token: 0x0600967D RID: 38525 RVA: 0x0030B848 File Offset: 0x00309A48
	public void setSelectionToFirstItem()
	{
		this.m_selectedItem.SetValue(null, this.m_unselectedItemText);
		if (this.m_items.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.m_items.Count - 1; i++)
		{
			this.m_items[i].SetSelected(false);
		}
		DropdownMenuItem dropdownMenuItem = this.m_items[0];
		dropdownMenuItem.SetSelected(true);
		this.m_selectedItem.SetValue(dropdownMenuItem.GetValue(), this.m_itemTextCallback(dropdownMenuItem.GetValue()));
	}

	// Token: 0x0600967E RID: 38526 RVA: 0x0030B8D5 File Offset: 0x00309AD5
	public object getSelection()
	{
		return this.m_selectedItem.GetValue();
	}

	// Token: 0x0600967F RID: 38527 RVA: 0x0030B8E4 File Offset: 0x00309AE4
	public void setSelection(object val)
	{
		this.m_selectedItem.SetValue(null, this.m_unselectedItemText);
		for (int i = 0; i < this.m_items.Count; i++)
		{
			DropdownMenuItem dropdownMenuItem = this.m_items[i];
			object value = dropdownMenuItem.GetValue();
			if ((value == null && val == null) || value.Equals(val))
			{
				dropdownMenuItem.SetSelected(true);
				this.m_selectedItem.SetValue(value, this.m_itemTextCallback(value));
			}
			else
			{
				dropdownMenuItem.SetSelected(false);
			}
		}
	}

	// Token: 0x06009680 RID: 38528 RVA: 0x0030B964 File Offset: 0x00309B64
	public void onUserPressedButton()
	{
		this.showMenu();
	}

	// Token: 0x06009681 RID: 38529 RVA: 0x0030B964 File Offset: 0x00309B64
	public void onUserPressedSelection(DropdownMenuItem item)
	{
		this.showMenu();
	}

	// Token: 0x06009682 RID: 38530 RVA: 0x0030B96C File Offset: 0x00309B6C
	public void onUserItemClicked(DropdownMenuItem item)
	{
		this.hideMenu();
		object selection = this.getSelection();
		object value = item.GetValue();
		this.setSelection(value);
		this.m_itemChosenCallback(value, selection);
	}

	// Token: 0x06009683 RID: 38531 RVA: 0x0030B9A1 File Offset: 0x00309BA1
	public void onUserCancelled()
	{
		if (SoundManager.Get().IsInitialized())
		{
			SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		}
		this.hideMenu();
	}

	// Token: 0x06009684 RID: 38532 RVA: 0x0030B9C9 File Offset: 0x00309BC9
	public void setUnselectedItemText(string text)
	{
		this.m_unselectedItemText = text;
	}

	// Token: 0x06009685 RID: 38533 RVA: 0x0030B9D2 File Offset: 0x00309BD2
	public DropdownControl.itemChosenCallback getItemChosenCallback()
	{
		return this.m_itemChosenCallback;
	}

	// Token: 0x06009686 RID: 38534 RVA: 0x0030B9DA File Offset: 0x00309BDA
	public void setItemChosenCallback(DropdownControl.itemChosenCallback callback)
	{
		DropdownControl.itemChosenCallback itemChosenCallback = callback;
		if (callback == null && (itemChosenCallback = DropdownControl.<>c.<>9__22_0) == null)
		{
			itemChosenCallback = (DropdownControl.<>c.<>9__22_0 = delegate(object <p0>, object <p1>)
			{
			});
		}
		this.m_itemChosenCallback = itemChosenCallback;
	}

	// Token: 0x06009687 RID: 38535 RVA: 0x0030BA06 File Offset: 0x00309C06
	public DropdownControl.itemTextCallback getItemTextCallback()
	{
		return this.m_itemTextCallback;
	}

	// Token: 0x06009688 RID: 38536 RVA: 0x0030BA0E File Offset: 0x00309C0E
	public void setItemTextCallback(DropdownControl.itemTextCallback callback)
	{
		this.m_itemTextCallback = (callback ?? new DropdownControl.itemTextCallback(DropdownControl.defaultItemTextCallback));
	}

	// Token: 0x06009689 RID: 38537 RVA: 0x0030435F File Offset: 0x0030255F
	public static string defaultItemTextCallback(object val)
	{
		if (val != null)
		{
			return val.ToString();
		}
		return string.Empty;
	}

	// Token: 0x0600968A RID: 38538 RVA: 0x0030BA27 File Offset: 0x00309C27
	public bool isMenuShown()
	{
		return this.m_menu.gameObject.activeInHierarchy;
	}

	// Token: 0x0600968B RID: 38539 RVA: 0x0030BA39 File Offset: 0x00309C39
	public DropdownControl.menuShownCallback getMenuShownCallback()
	{
		return this.m_menuShownCallback;
	}

	// Token: 0x0600968C RID: 38540 RVA: 0x0030BA41 File Offset: 0x00309C41
	public void setMenuShownCallback(DropdownControl.menuShownCallback callback)
	{
		this.m_menuShownCallback = callback;
	}

	// Token: 0x0600968D RID: 38541 RVA: 0x0030BA4A File Offset: 0x00309C4A
	public void setFont(Font font)
	{
		this.m_overrideFont = font;
		this.m_selectedItem.m_text.TrueTypeFont = font;
		this.m_menuItemTemplate.m_text.TrueTypeFont = font;
	}

	// Token: 0x0600968E RID: 38542 RVA: 0x0030BA75 File Offset: 0x00309C75
	private void showMenu()
	{
		this.m_cancelCatcher.gameObject.SetActive(true);
		this.m_menu.gameObject.SetActive(true);
		this.layoutMenu();
		this.m_menuShownCallback(true);
	}

	// Token: 0x0600968F RID: 38543 RVA: 0x0030BAAB File Offset: 0x00309CAB
	private void hideMenu()
	{
		this.m_cancelCatcher.gameObject.SetActive(false);
		this.m_menu.gameObject.SetActive(false);
		this.m_menuShownCallback(false);
	}

	// Token: 0x06009690 RID: 38544 RVA: 0x0030BADC File Offset: 0x00309CDC
	private void layoutMenu()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		this.m_menuItemTemplate.gameObject.SetActive(true);
		OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(this.m_menuItemTemplate.gameObject, true);
		if (orientedBounds == null)
		{
			return;
		}
		float num = orientedBounds.Extents[1].magnitude * 2f;
		this.m_menuItemTemplate.gameObject.SetActive(false);
		this.m_menuItemContainer.ClearSlices();
		for (int i = 0; i < this.m_items.Count; i++)
		{
			this.m_menuItemContainer.AddSlice(this.m_items[i].gameObject);
		}
		this.m_menuItemContainer.UpdateSlices();
		if (this.m_items.Count <= 1)
		{
			TransformUtil.SetLocalScaleZ(this.m_menuMiddle, 0.001f);
		}
		else
		{
			TransformUtil.SetLocalScaleToWorldDimension(this.m_menuMiddle, new WorldDimensionIndex[]
			{
				new WorldDimensionIndex(num * (float)(this.m_items.Count - 1), 2)
			});
		}
		this.m_menu.UpdateSlices();
	}

	// Token: 0x06009691 RID: 38545 RVA: 0x0030BBE8 File Offset: 0x00309DE8
	private int findItemIndex(object value)
	{
		for (int i = 0; i < this.m_items.Count; i++)
		{
			if (this.m_items[i].GetValue() == value)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06009692 RID: 38546 RVA: 0x0030BC24 File Offset: 0x00309E24
	private DropdownMenuItem findItem(object value)
	{
		for (int i = 0; i < this.m_items.Count; i++)
		{
			DropdownMenuItem dropdownMenuItem = this.m_items[i];
			if (dropdownMenuItem.GetValue() == value)
			{
				return dropdownMenuItem;
			}
		}
		return null;
	}

	// Token: 0x04007E19 RID: 32281
	[CustomEditField(Sections = "Buttons")]
	public DropdownMenuItem m_selectedItem;

	// Token: 0x04007E1A RID: 32282
	[CustomEditField(Sections = "Buttons")]
	public PegUIElement m_cancelCatcher;

	// Token: 0x04007E1B RID: 32283
	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_button;

	// Token: 0x04007E1C RID: 32284
	[CustomEditField(Sections = "Menu")]
	public MultiSliceElement m_menu;

	// Token: 0x04007E1D RID: 32285
	[CustomEditField(Sections = "Menu")]
	public GameObject m_menuMiddle;

	// Token: 0x04007E1E RID: 32286
	[CustomEditField(Sections = "Menu")]
	public MultiSliceElement m_menuItemContainer;

	// Token: 0x04007E1F RID: 32287
	[CustomEditField(Sections = "Menu Templates")]
	public DropdownMenuItem m_menuItemTemplate;

	// Token: 0x04007E20 RID: 32288
	private string m_unselectedItemText = string.Empty;

	// Token: 0x04007E21 RID: 32289
	private DropdownControl.itemChosenCallback m_itemChosenCallback = delegate(object <p0>, object <p1>)
	{
	};

	// Token: 0x04007E22 RID: 32290
	private DropdownControl.itemTextCallback m_itemTextCallback = new DropdownControl.itemTextCallback(DropdownControl.defaultItemTextCallback);

	// Token: 0x04007E23 RID: 32291
	private DropdownControl.menuShownCallback m_menuShownCallback = delegate(bool <p0>)
	{
	};

	// Token: 0x04007E24 RID: 32292
	private List<DropdownMenuItem> m_items = new List<DropdownMenuItem>();

	// Token: 0x04007E25 RID: 32293
	private Font m_overrideFont;

	// Token: 0x0200275A RID: 10074
	// (Invoke) Token: 0x060139AE RID: 80302
	public delegate void itemChosenCallback(object selection, object prevSelection);

	// Token: 0x0200275B RID: 10075
	// (Invoke) Token: 0x060139B2 RID: 80306
	public delegate string itemTextCallback(object val);

	// Token: 0x0200275C RID: 10076
	// (Invoke) Token: 0x060139B6 RID: 80310
	public delegate void menuShownCallback(bool shown);
}
