using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Core;
using UnityEngine;

// Token: 0x02000B2F RID: 2863
[RequireComponent(typeof(BoxCollider))]
public class TouchList : PegUIElement
{
	// Token: 0x14000094 RID: 148
	// (add) Token: 0x060097B8 RID: 38840 RVA: 0x00310CAC File Offset: 0x0030EEAC
	// (remove) Token: 0x060097B9 RID: 38841 RVA: 0x00310CE4 File Offset: 0x0030EEE4
	public event Action Scrolled;

	// Token: 0x14000095 RID: 149
	// (add) Token: 0x060097BA RID: 38842 RVA: 0x00310D1C File Offset: 0x0030EF1C
	// (remove) Token: 0x060097BB RID: 38843 RVA: 0x00310D54 File Offset: 0x0030EF54
	public event TouchList.SelectedIndexChangingEvent SelectedIndexChanging;

	// Token: 0x14000096 RID: 150
	// (add) Token: 0x060097BC RID: 38844 RVA: 0x00310D8C File Offset: 0x0030EF8C
	// (remove) Token: 0x060097BD RID: 38845 RVA: 0x00310DC4 File Offset: 0x0030EFC4
	public event TouchList.ScrollingEnabledChangedEvent ScrollingEnabledChanged;

	// Token: 0x14000097 RID: 151
	// (add) Token: 0x060097BE RID: 38846 RVA: 0x00310DFC File Offset: 0x0030EFFC
	// (remove) Token: 0x060097BF RID: 38847 RVA: 0x00310E34 File Offset: 0x0030F034
	public event Action ClipSizeChanged;

	// Token: 0x14000098 RID: 152
	// (add) Token: 0x060097C0 RID: 38848 RVA: 0x00310E6C File Offset: 0x0030F06C
	// (remove) Token: 0x060097C1 RID: 38849 RVA: 0x00310EA4 File Offset: 0x0030F0A4
	public event TouchList.ItemDragEvent ItemDragStarted;

	// Token: 0x14000099 RID: 153
	// (add) Token: 0x060097C2 RID: 38850 RVA: 0x00310EDC File Offset: 0x0030F0DC
	// (remove) Token: 0x060097C3 RID: 38851 RVA: 0x00310F14 File Offset: 0x0030F114
	public event TouchList.ItemDragEvent ItemDragged;

	// Token: 0x1400009A RID: 154
	// (add) Token: 0x060097C4 RID: 38852 RVA: 0x00310F4C File Offset: 0x0030F14C
	// (remove) Token: 0x060097C5 RID: 38853 RVA: 0x00310F84 File Offset: 0x0030F184
	public event TouchList.ItemDragEvent ItemDragFinished;

	// Token: 0x17000898 RID: 2200
	// (get) Token: 0x060097C6 RID: 38854 RVA: 0x00310FB9 File Offset: 0x0030F1B9
	public IEnumerable<ITouchListItem> RenderedItems
	{
		get
		{
			this.EnforceInitialized();
			return this.renderedItems;
		}
	}

	// Token: 0x17000899 RID: 2201
	// (get) Token: 0x060097C7 RID: 38855 RVA: 0x00310FC7 File Offset: 0x0030F1C7
	public bool IsReadOnly
	{
		get
		{
			this.EnforceInitialized();
			return false;
		}
	}

	// Token: 0x1700089A RID: 2202
	// (get) Token: 0x060097C8 RID: 38856 RVA: 0x00310FD0 File Offset: 0x0030F1D0
	public bool IsInitialized
	{
		get
		{
			return this.content != null;
		}
	}

	// Token: 0x1700089B RID: 2203
	// (get) Token: 0x060097C9 RID: 38857 RVA: 0x00310FDE File Offset: 0x0030F1DE
	// (set) Token: 0x060097CA RID: 38858 RVA: 0x00310FEC File Offset: 0x0030F1EC
	public TouchList.ILongListBehavior LongListBehavior
	{
		get
		{
			this.EnforceInitialized();
			return this.longListBehavior;
		}
		set
		{
			this.EnforceInitialized();
			if (value == this.longListBehavior)
			{
				return;
			}
			this.allowModification = true;
			this.Clear();
			if (this.longListBehavior != null)
			{
				this.longListBehavior.ReleaseAllItems();
			}
			this.longListBehavior = value;
			if (this.longListBehavior != null)
			{
				this.RefreshList(0, false);
				this.allowModification = false;
			}
		}
	}

	// Token: 0x1700089C RID: 2204
	// (get) Token: 0x060097CB RID: 38859 RVA: 0x00311048 File Offset: 0x0030F248
	// (set) Token: 0x060097CC RID: 38860 RVA: 0x003110F8 File Offset: 0x0030F2F8
	public float ScrollValue
	{
		get
		{
			this.EnforceInitialized();
			float scrollableAmount = this.ScrollableAmount;
			float num = (scrollableAmount > 0f) ? Mathf.Clamp01(-this.content.transform.localPosition[this.layoutDimension1] / scrollableAmount) : 0f;
			if (num == 0f || num == 1f)
			{
				return -this.GetOutOfBoundsDist(this.content.transform.localPosition[this.layoutDimension1]) / Mathf.Max(this.contentSize, this.ClipSize[this.GetVector2Dimension(this.layoutDimension1)]) + num;
			}
			return num;
		}
		set
		{
			this.EnforceInitialized();
			if (this.dragBeginOffsetFromContent == null && !Mathf.Approximately(this.ScrollValue, value))
			{
				float scrollableAmount = this.ScrollableAmount;
				Vector3 localPosition = this.content.transform.localPosition;
				localPosition[this.layoutDimension1] = -Mathf.Clamp01(value) * scrollableAmount;
				this.content.transform.localPosition = localPosition;
				float num = localPosition[this.layoutDimension1] - this.lastContentPosition;
				if (num != 0f)
				{
					this.PreBufferLongListItems(num < 0f);
				}
				this.lastContentPosition = localPosition[this.layoutDimension1];
				this.FixUpScrolling();
				this.OnScrolled();
			}
		}
	}

	// Token: 0x060097CD RID: 38861 RVA: 0x003111B8 File Offset: 0x0030F3B8
	private void FixUpScrolling()
	{
		if (this.longListBehavior == null || this.renderedItems.Count <= 0 || !this.CanScroll)
		{
			return;
		}
		Bounds bounds = this.CalculateLocalClipBounds();
		ITouchListItem touchListItem = this.renderedItems[0];
		TouchList.ItemInfo itemInfo = this.itemInfos[touchListItem];
		if (itemInfo.LongListIndex == 0 && !this.CanScrollBehind)
		{
			float num = bounds.min[this.layoutDimension1];
			Vector3 min = itemInfo.Min;
			if (Mathf.Abs(min[this.layoutDimension1] - num) > 0.0001f)
			{
				Vector3 zero = Vector3.zero;
				zero[this.layoutDimension1] = num - min[this.layoutDimension1];
				for (int i = 0; i < this.renderedItems.Count; i++)
				{
					touchListItem = this.renderedItems[i];
					touchListItem.gameObject.transform.Translate(zero);
				}
				return;
			}
		}
		else if (this.renderedItems.Count > 1 && !this.CanScrollAhead)
		{
			float num2 = bounds.max[this.layoutDimension1];
			touchListItem = this.renderedItems[this.renderedItems.Count - 1];
			itemInfo = this.itemInfos[touchListItem];
			if (itemInfo.LongListIndex >= this.longListBehavior.AllItemsCount - 1)
			{
				Vector3 max = itemInfo.Max;
				if (Mathf.Abs(max[this.layoutDimension1] - num2) > 0.0001f)
				{
					Vector3 zero2 = Vector3.zero;
					zero2[this.layoutDimension1] = num2 - max[this.layoutDimension1];
					for (int j = 0; j < this.renderedItems.Count; j++)
					{
						touchListItem = this.renderedItems[j];
						touchListItem.gameObject.transform.Translate(zero2);
					}
				}
			}
		}
	}

	// Token: 0x1700089D RID: 2205
	// (get) Token: 0x060097CE RID: 38862 RVA: 0x003113AC File Offset: 0x0030F5AC
	public float ScrollableAmount
	{
		get
		{
			if (this.longListBehavior == null)
			{
				return this.excessContentSize;
			}
			return Mathf.Max(0f, this.m_fullListContentSize - this.ClipSize[this.GetVector2Dimension(this.layoutDimension1)]);
		}
	}

	// Token: 0x1700089E RID: 2206
	// (get) Token: 0x060097CF RID: 38863 RVA: 0x003113F4 File Offset: 0x0030F5F4
	public bool CanScrollAhead
	{
		get
		{
			if (!this.scrollEnabled)
			{
				return false;
			}
			if (this.ScrollValue < 1f)
			{
				return true;
			}
			if (this.longListBehavior != null && this.renderedItems.Count > 0)
			{
				ITouchListItem key = this.renderedItems.Last<ITouchListItem>();
				for (int i = this.itemInfos[key].LongListIndex + 1; i < this.longListBehavior.AllItemsCount; i++)
				{
					if (this.longListBehavior.IsItemShowable(i))
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	// Token: 0x1700089F RID: 2207
	// (get) Token: 0x060097D0 RID: 38864 RVA: 0x00311478 File Offset: 0x0030F678
	public bool CanScrollBehind
	{
		get
		{
			if (!this.scrollEnabled)
			{
				return false;
			}
			if (this.ScrollValue > 0f)
			{
				return true;
			}
			if (this.longListBehavior != null && this.renderedItems.Count > 0)
			{
				ITouchListItem key = this.renderedItems.First<ITouchListItem>();
				TouchList.ItemInfo itemInfo = this.itemInfos[key];
				if (this.longListBehavior.AllItemsCount > 0)
				{
					for (int i = itemInfo.LongListIndex - 1; i >= 0; i--)
					{
						if (this.longListBehavior.IsItemShowable(i))
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}

	// Token: 0x170008A0 RID: 2208
	// (get) Token: 0x060097D1 RID: 38865 RVA: 0x003114FF File Offset: 0x0030F6FF
	public bool CanScroll
	{
		get
		{
			return this.CanScrollAhead || this.CanScrollBehind;
		}
	}

	// Token: 0x170008A1 RID: 2209
	// (get) Token: 0x060097D2 RID: 38866 RVA: 0x00311514 File Offset: 0x0030F714
	// (set) Token: 0x060097D3 RID: 38867 RVA: 0x00311548 File Offset: 0x0030F748
	public float ViewWindowMinValue
	{
		get
		{
			return -this.content.transform.localPosition[this.layoutDimension1] / this.contentSize;
		}
		set
		{
			Vector3 localPosition = this.content.transform.localPosition;
			localPosition[this.layoutDimension1] = -Mathf.Clamp01(value) * this.contentSize;
			this.content.transform.localPosition = localPosition;
			float num = this.content.transform.localPosition[this.layoutDimension1] - this.lastContentPosition;
			if (num != 0f)
			{
				this.PreBufferLongListItems(num < 0f);
			}
			this.lastContentPosition = localPosition[this.layoutDimension1];
			this.OnScrolled();
		}
	}

	// Token: 0x170008A2 RID: 2210
	// (get) Token: 0x060097D4 RID: 38868 RVA: 0x003115E8 File Offset: 0x0030F7E8
	// (set) Token: 0x060097D5 RID: 38869 RVA: 0x00311638 File Offset: 0x0030F838
	public float ViewWindowMaxValue
	{
		get
		{
			float num = this.content.transform.localPosition[this.layoutDimension1];
			float num2 = this.ClipSize[this.GetVector2Dimension(this.layoutDimension1)];
			return (-num + num2) / this.contentSize;
		}
		set
		{
			Vector3 localPosition = this.content.transform.localPosition;
			localPosition[this.layoutDimension1] = -Mathf.Clamp01(value) * this.contentSize + this.ClipSize[this.GetVector2Dimension(this.layoutDimension1)];
			this.content.transform.localPosition = localPosition;
			float num = this.content.transform.localPosition[this.layoutDimension1] - this.lastContentPosition;
			if (num != 0f)
			{
				this.PreBufferLongListItems(num < 0f);
			}
			this.lastContentPosition = localPosition[this.layoutDimension1];
			this.OnScrolled();
		}
	}

	// Token: 0x170008A3 RID: 2211
	// (get) Token: 0x060097D6 RID: 38870 RVA: 0x003116F4 File Offset: 0x0030F8F4
	// (set) Token: 0x060097D7 RID: 38871 RVA: 0x00311744 File Offset: 0x0030F944
	public Vector2 ClipSize
	{
		get
		{
			this.EnforceInitialized();
			BoxCollider boxCollider = base.GetComponent<Collider>() as BoxCollider;
			return new Vector2(boxCollider.size.x, (this.layoutPlane == TouchList.LayoutPlane.XY) ? boxCollider.size.y : boxCollider.size.z);
		}
		set
		{
			this.EnforceInitialized();
			BoxCollider boxCollider = base.GetComponent<Collider>() as BoxCollider;
			Vector3 vector = new Vector3(value.x, 0f, 0f);
			vector[1] = ((this.layoutPlane == TouchList.LayoutPlane.XY) ? value.y : boxCollider.size.y);
			vector[2] = ((this.layoutPlane == TouchList.LayoutPlane.XZ) ? value.y : boxCollider.size.z);
			Vector3 vector2 = VectorUtils.Abs(boxCollider.size - vector);
			if (vector2.x <= 0.0001f && vector2.y <= 0.0001f && vector2.z <= 0.0001f)
			{
				return;
			}
			boxCollider.size = vector;
			this.UpdateBackgroundBounds();
			if (this.longListBehavior == null)
			{
				this.RepositionItems(0, null);
			}
			else
			{
				this.RefreshList(0, true);
			}
			if (this.ClipSizeChanged != null)
			{
				this.ClipSizeChanged();
			}
		}
	}

	// Token: 0x170008A4 RID: 2212
	// (get) Token: 0x060097D8 RID: 38872 RVA: 0x0031183D File Offset: 0x0030FA3D
	// (set) Token: 0x060097D9 RID: 38873 RVA: 0x00311850 File Offset: 0x0030FA50
	public bool SelectionEnabled
	{
		get
		{
			this.EnforceInitialized();
			return this.selection != null;
		}
		set
		{
			this.EnforceInitialized();
			if (value == this.SelectionEnabled)
			{
				return;
			}
			if (value)
			{
				this.selection = new int?(-1);
				return;
			}
			this.selection = null;
		}
	}

	// Token: 0x170008A5 RID: 2213
	// (get) Token: 0x060097DA RID: 38874 RVA: 0x0031187E File Offset: 0x0030FA7E
	// (set) Token: 0x060097DB RID: 38875 RVA: 0x003118A0 File Offset: 0x0030FAA0
	public int SelectedIndex
	{
		get
		{
			this.EnforceInitialized();
			if (this.selection == null)
			{
				return -1;
			}
			return this.selection.Value;
		}
		set
		{
			this.EnforceInitialized();
			if (this.SelectionEnabled)
			{
				int? num = this.selection;
				if (value == num.GetValueOrDefault() & num != null)
				{
					return;
				}
				if (this.SelectedIndexChanging != null && !this.SelectedIndexChanging(value))
				{
					return;
				}
				ISelectableTouchListItem selectableTouchListItem = this.SelectedItem as ISelectableTouchListItem;
				ISelectableTouchListItem selectableTouchListItem2 = ((value != -1) ? this.renderedItems[value] : null) as ISelectableTouchListItem;
				if (value == -1 || (selectableTouchListItem2 != null && selectableTouchListItem2.Selectable))
				{
					this.selection = new int?(value);
				}
				if (selectableTouchListItem != null)
				{
					num = this.selection;
					if (num.GetValueOrDefault() == value & num != null)
					{
						selectableTouchListItem.Unselected();
					}
				}
				num = this.selection;
				if ((num.GetValueOrDefault() == value & num != null) && selectableTouchListItem2 != null)
				{
					selectableTouchListItem2.Selected();
					this.ScrollToItem_Internal(selectableTouchListItem2);
				}
			}
		}
	}

	// Token: 0x170008A6 RID: 2214
	// (get) Token: 0x060097DC RID: 38876 RVA: 0x00311984 File Offset: 0x0030FB84
	// (set) Token: 0x060097DD RID: 38877 RVA: 0x003119C0 File Offset: 0x0030FBC0
	public ITouchListItem SelectedItem
	{
		get
		{
			this.EnforceInitialized();
			if (this.selection == null || this.selection.Value == -1)
			{
				return null;
			}
			return this.renderedItems[this.selection.Value];
		}
		set
		{
			this.EnforceInitialized();
			int num = this.renderedItems.IndexOf(value);
			if (num != -1)
			{
				this.SelectedIndex = num;
			}
		}
	}

	// Token: 0x060097DE RID: 38878 RVA: 0x003119EB File Offset: 0x0030FBEB
	public void Add(ITouchListItem item)
	{
		this.Add(item, true);
	}

	// Token: 0x060097DF RID: 38879 RVA: 0x003119F8 File Offset: 0x0030FBF8
	public void Add(ITouchListItem item, bool repositionItems)
	{
		this.EnforceInitialized();
		if (!this.allowModification)
		{
			return;
		}
		this.renderedItems.Add(item);
		Vector3 negatedScale = this.GetNegatedScale(item.transform.localScale);
		item.transform.parent = this.content.transform;
		item.transform.localPosition = Vector3.zero;
		item.transform.localRotation = Quaternion.identity;
		if (this.orientation == TouchList.Orientation.Vertical)
		{
			item.transform.localScale = negatedScale;
		}
		this.itemInfos[item] = new TouchList.ItemInfo(item, this.layoutPlane);
		item.gameObject.SetActive(false);
		int? num = this.selection;
		int num2 = -1;
		if ((num.GetValueOrDefault() == num2 & num != null) && item is ISelectableTouchListItem && ((ISelectableTouchListItem)item).IsSelected())
		{
			this.selection = new int?(this.renderedItems.Count - 1);
		}
		if (repositionItems)
		{
			this.RepositionItems(this.renderedItems.Count - 1, null);
			this.RecalculateLongListContentSize(true);
		}
	}

	// Token: 0x060097E0 RID: 38880 RVA: 0x00311B14 File Offset: 0x0030FD14
	public void Clear()
	{
		this.EnforceInitialized();
		if (!this.allowModification)
		{
			return;
		}
		foreach (ITouchListItem touchListItem in this.renderedItems)
		{
			Vector3 negatedScale = this.GetNegatedScale(touchListItem.transform.localScale);
			touchListItem.transform.parent = null;
			if (this.orientation == TouchList.Orientation.Vertical)
			{
				touchListItem.transform.localScale = negatedScale;
			}
		}
		this.content.transform.localPosition = Vector3.zero;
		this.lastContentPosition = 0f;
		this.renderedItems.Clear();
		this.RecalculateSize();
		this.UpdateBackgroundScroll();
		this.RecalculateLongListContentSize(true);
		if (this.SelectionEnabled)
		{
			this.SelectedIndex = -1;
		}
		if (this.m_hoveredOverItem != null)
		{
			this.m_hoveredOverItem.TriggerOut();
			this.m_hoveredOverItem = null;
		}
	}

	// Token: 0x060097E1 RID: 38881 RVA: 0x00311C10 File Offset: 0x0030FE10
	public bool Contains(ITouchListItem item)
	{
		this.EnforceInitialized();
		return this.renderedItems.Contains(item);
	}

	// Token: 0x060097E2 RID: 38882 RVA: 0x00311C24 File Offset: 0x0030FE24
	public void CopyTo(ITouchListItem[] array, int arrayIndex)
	{
		this.EnforceInitialized();
		this.renderedItems.CopyTo(array, arrayIndex);
	}

	// Token: 0x060097E3 RID: 38883 RVA: 0x00311C3C File Offset: 0x0030FE3C
	private List<ITouchListItem> GetItemsInView()
	{
		this.EnforceInitialized();
		List<ITouchListItem> list = new List<ITouchListItem>();
		float num = this.CalculateLocalClipBounds().max[this.layoutDimension1];
		int num2 = this.GetNumItemsBehindView();
		while (num2 < this.renderedItems.Count && (this.itemInfos[this.renderedItems[num2]].Min - this.content.transform.localPosition)[this.layoutDimension1] < num)
		{
			list.Add(this.renderedItems[num2]);
			num2++;
		}
		return list;
	}

	// Token: 0x060097E4 RID: 38884 RVA: 0x00311CEC File Offset: 0x0030FEEC
	public void SetVisibilityOfAllItems()
	{
		if (this.layoutSuspended)
		{
			return;
		}
		this.EnforceInitialized();
		Bounds bounds = this.CalculateLocalClipBounds();
		for (int i = 0; i < this.renderedItems.Count; i++)
		{
			ITouchListItem touchListItem = this.renderedItems[i];
			bool flag = this.IsItemVisible_Internal(i, ref bounds);
			if (flag != touchListItem.gameObject.activeSelf)
			{
				touchListItem.gameObject.SetActive(flag);
				if (!flag)
				{
					touchListItem.OnScrollOutOfView();
				}
			}
		}
	}

	// Token: 0x060097E5 RID: 38885 RVA: 0x00311D60 File Offset: 0x0030FF60
	private bool IsItemVisible_Internal(int visualizedListIndex, ref Bounds localClipBounds)
	{
		ITouchListItem key = this.renderedItems[visualizedListIndex];
		TouchList.ItemInfo itemInfo = this.itemInfos[key];
		Vector3 min = itemInfo.Min;
		Vector3 max = itemInfo.Max;
		return this.IsWithinClipBounds(min, max, ref localClipBounds);
	}

	// Token: 0x060097E6 RID: 38886 RVA: 0x00311DA4 File Offset: 0x0030FFA4
	private bool IsWithinClipBounds(Vector3 localBoundsMin, Vector3 localBoundsMax, ref Bounds localClipBounds)
	{
		float num = localClipBounds.min[this.layoutDimension1];
		float num2 = localClipBounds.max[this.layoutDimension1];
		return localBoundsMax[this.layoutDimension1] >= num && localBoundsMin[this.layoutDimension1] <= num2;
	}

	// Token: 0x060097E7 RID: 38887 RVA: 0x00311E00 File Offset: 0x00310000
	private bool IsItemVisible(int visualizedListIndex)
	{
		Bounds bounds = this.CalculateLocalClipBounds();
		return this.IsItemVisible_Internal(visualizedListIndex, ref bounds);
	}

	// Token: 0x060097E8 RID: 38888 RVA: 0x00311E1D File Offset: 0x0031001D
	public int IndexOf(ITouchListItem item)
	{
		this.EnforceInitialized();
		return this.renderedItems.IndexOf(item);
	}

	// Token: 0x060097E9 RID: 38889 RVA: 0x00311E31 File Offset: 0x00310031
	public void Insert(int index, ITouchListItem item)
	{
		this.Insert(index, item, true);
	}

	// Token: 0x060097EA RID: 38890 RVA: 0x00311E3C File Offset: 0x0031003C
	public void Insert(int index, ITouchListItem item, bool repositionItems)
	{
		this.EnforceInitialized();
		if (!this.allowModification)
		{
			return;
		}
		this.renderedItems.Insert(index, item);
		Vector3 negatedScale = this.GetNegatedScale(item.transform.localScale);
		item.transform.parent = this.content.transform;
		item.transform.localPosition = Vector3.zero;
		item.transform.localRotation = Quaternion.identity;
		if (this.orientation == TouchList.Orientation.Vertical)
		{
			item.transform.localScale = negatedScale;
		}
		this.itemInfos[item] = new TouchList.ItemInfo(item, this.layoutPlane);
		int? num = this.selection;
		int num2 = -1;
		if ((num.GetValueOrDefault() == num2 & num != null) && item is ISelectableTouchListItem && ((ISelectableTouchListItem)item).IsSelected())
		{
			this.selection = new int?(index);
		}
		if (repositionItems)
		{
			this.RepositionItems(index, null);
			this.RecalculateLongListContentSize(true);
		}
	}

	// Token: 0x060097EB RID: 38891 RVA: 0x00311F34 File Offset: 0x00310134
	public bool Remove(ITouchListItem item)
	{
		this.EnforceInitialized();
		if (!this.allowModification)
		{
			return false;
		}
		int num = this.renderedItems.IndexOf(item);
		if (num != -1)
		{
			this.RemoveAt(num, true);
			return true;
		}
		return false;
	}

	// Token: 0x060097EC RID: 38892 RVA: 0x00311F6D File Offset: 0x0031016D
	public void RemoveAt(int index)
	{
		this.RemoveAt(index, true);
	}

	// Token: 0x060097ED RID: 38893 RVA: 0x00311F78 File Offset: 0x00310178
	public void RemoveAt(int index, bool repositionItems)
	{
		this.EnforceInitialized();
		if (!this.allowModification)
		{
			return;
		}
		Vector3 negatedScale = this.GetNegatedScale(this.renderedItems[index].transform.localScale);
		ITouchListItem touchListItem = this.renderedItems[index];
		touchListItem.transform.parent = base.transform;
		if (this.orientation == TouchList.Orientation.Vertical)
		{
			this.renderedItems[index].transform.localScale = negatedScale;
		}
		this.itemInfos.Remove(this.renderedItems[index]);
		this.renderedItems.RemoveAt(index);
		int? num = this.selection;
		if (index == num.GetValueOrDefault() & num != null)
		{
			this.selection = new int?(-1);
		}
		else
		{
			num = this.selection;
			if (index < num.GetValueOrDefault() & num != null)
			{
				this.selection--;
			}
		}
		if (this.m_hoveredOverItem != null && touchListItem.GetComponent<PegUIElement>() == this.m_hoveredOverItem)
		{
			this.m_hoveredOverItem.TriggerOut();
			this.m_hoveredOverItem = null;
		}
		if (repositionItems)
		{
			this.RepositionItems(index, null);
			this.RecalculateLongListContentSize(true);
		}
	}

	// Token: 0x060097EE RID: 38894 RVA: 0x003120D5 File Offset: 0x003102D5
	public int FindIndex(Predicate<ITouchListItem> match)
	{
		this.EnforceInitialized();
		return this.renderedItems.FindIndex(match);
	}

	// Token: 0x060097EF RID: 38895 RVA: 0x003120EC File Offset: 0x003102EC
	public void Sort(Comparison<ITouchListItem> comparison)
	{
		this.EnforceInitialized();
		ITouchListItem selectedItem = this.SelectedItem;
		this.renderedItems.Sort(comparison);
		this.RepositionItems(0, null);
		this.selection = new int?(this.renderedItems.IndexOf(selectedItem));
	}

	// Token: 0x170008A7 RID: 2215
	// (get) Token: 0x060097F0 RID: 38896 RVA: 0x00312139 File Offset: 0x00310339
	public bool IsLayoutSuspended
	{
		get
		{
			return this.layoutSuspended;
		}
	}

	// Token: 0x060097F1 RID: 38897 RVA: 0x00312141 File Offset: 0x00310341
	public void SuspendLayout()
	{
		this.EnforceInitialized();
		this.layoutSuspended = true;
	}

	// Token: 0x060097F2 RID: 38898 RVA: 0x00312150 File Offset: 0x00310350
	public void ResumeLayout(bool repositionItems = true)
	{
		this.EnforceInitialized();
		this.layoutSuspended = false;
		if (repositionItems)
		{
			this.RepositionItems(0, null);
		}
	}

	// Token: 0x060097F3 RID: 38899 RVA: 0x00312180 File Offset: 0x00310380
	public void ResetState()
	{
		this.touchBeginScreenPosition = null;
		this.dragBeginOffsetFromContent = null;
		this.dragBeginContentPosition = Vector3.zero;
		this.lastTouchPosition = Vector3.zero;
		this.lastContentPosition = 0f;
		this.dragItemBegin = null;
		if (this.content != null)
		{
			this.content.transform.localPosition = Vector3.zero;
		}
	}

	// Token: 0x060097F4 RID: 38900 RVA: 0x003121F5 File Offset: 0x003103F5
	public void SetScrollingEnabled(bool enable)
	{
		this.scrollEnabled = enable;
		this.OnScrollingEnabledChanged();
	}

	// Token: 0x060097F5 RID: 38901 RVA: 0x00312204 File Offset: 0x00310404
	public void ScrollToItem(ITouchListItem item)
	{
		this.ScrollToItem_Internal(item);
	}

	// Token: 0x060097F6 RID: 38902 RVA: 0x00312210 File Offset: 0x00310410
	protected override void Awake()
	{
		base.Awake();
		this.content = new GameObject("Content");
		this.content.transform.parent = base.transform;
		TransformUtil.Identity(this.content.transform);
		this.layoutDimension1 = 0;
		this.layoutDimension2 = ((this.layoutPlane == TouchList.LayoutPlane.XY) ? 1 : 2);
		this.layoutDimension3 = 3 - this.layoutDimension2;
		if (this.orientation == TouchList.Orientation.Vertical)
		{
			GeneralUtils.Swap<int>(ref this.layoutDimension1, ref this.layoutDimension2);
			Vector3 one = Vector3.one;
			one[this.layoutDimension1] = -1f;
			base.transform.localScale = one;
		}
		if (this.background != null)
		{
			if (this.orientation == TouchList.Orientation.Vertical)
			{
				this.background.transform.localScale = this.GetNegatedScale(this.background.transform.localScale);
			}
			this.UpdateBackgroundBounds();
		}
	}

	// Token: 0x060097F7 RID: 38903 RVA: 0x00312301 File Offset: 0x00310501
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		this.m_isHoveredOverTouchList = true;
		this.OnHover(true);
	}

	// Token: 0x060097F8 RID: 38904 RVA: 0x00312311 File Offset: 0x00310511
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.m_isHoveredOverTouchList = false;
		if (this.m_hoveredOverItem != null)
		{
			this.m_hoveredOverItem.TriggerOut();
			this.m_hoveredOverItem = null;
		}
	}

	// Token: 0x060097F9 RID: 38905 RVA: 0x0031233C File Offset: 0x0031053C
	private void OnHover(bool isKnownOver)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			return;
		}
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (camera == null)
		{
			if (this.m_hoveredOverItem != null)
			{
				this.m_hoveredOverItem.TriggerOut();
				this.m_hoveredOverItem = null;
			}
			return;
		}
		RaycastHit raycastHit;
		if (!isKnownOver && (!UniversalInputManager.Get().GetInputHitInfo(camera, out raycastHit) || raycastHit.transform != base.transform) && this.m_hoveredOverItem != null)
		{
			this.m_hoveredOverItem.TriggerOut();
			this.m_hoveredOverItem = null;
		}
		base.GetComponent<Collider>().enabled = false;
		PegUIElement pegUIElement = null;
		if (UniversalInputManager.Get().GetInputHitInfo(camera, out raycastHit))
		{
			pegUIElement = raycastHit.transform.GetComponent<PegUIElement>();
		}
		base.GetComponent<Collider>().enabled = true;
		if (pegUIElement != null && this.m_hoveredOverItem != pegUIElement)
		{
			if (this.m_hoveredOverItem != null)
			{
				this.m_hoveredOverItem.TriggerOut();
			}
			pegUIElement.TriggerOver();
			this.m_hoveredOverItem = pegUIElement;
		}
	}

	// Token: 0x060097FA RID: 38906 RVA: 0x0031244C File Offset: 0x0031064C
	protected override void OnPress()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (camera == null)
		{
			return;
		}
		this.touchBeginScreenPosition = new Vector2?(UniversalInputManager.Get().GetMousePosition());
		if (this.lastContentPosition != this.content.transform.localPosition[this.layoutDimension1])
		{
			return;
		}
		Vector3 point = this.GetTouchPosition() - this.content.transform.localPosition;
		for (int i = 0; i < this.renderedItems.Count; i++)
		{
			ITouchListItem touchListItem = this.renderedItems[i];
			if ((touchListItem.IsHeader || touchListItem.Visible) && this.itemInfos[touchListItem].Contains(point, this.layoutPlane))
			{
				this.touchBeginItem = touchListItem;
				break;
			}
		}
		base.GetComponent<Collider>().enabled = false;
		PegUIElement pegUIElement = null;
		RaycastHit raycastHit;
		if (UniversalInputManager.Get().GetInputHitInfo(camera, out raycastHit))
		{
			pegUIElement = raycastHit.transform.GetComponent<PegUIElement>();
		}
		base.GetComponent<Collider>().enabled = true;
		if (pegUIElement != null)
		{
			pegUIElement.TriggerPress();
		}
	}

	// Token: 0x060097FB RID: 38907 RVA: 0x0031257C File Offset: 0x0031077C
	protected override void OnRelease()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (camera == null)
		{
			return;
		}
		if (this.touchBeginItem != null && this.dragItemBegin == null)
		{
			this.touchBeginScreenPosition = null;
			base.GetComponent<Collider>().enabled = false;
			PegUIElement pegUIElement = null;
			RaycastHit raycastHit;
			if (UniversalInputManager.Get().GetInputHitInfo(camera, out raycastHit))
			{
				pegUIElement = raycastHit.transform.GetComponent<PegUIElement>();
			}
			base.GetComponent<Collider>().enabled = true;
			if (pegUIElement != null)
			{
				pegUIElement.TriggerRelease();
				this.touchBeginItem = null;
			}
		}
	}

	// Token: 0x060097FC RID: 38908 RVA: 0x00312611 File Offset: 0x00310811
	private void EnforceInitialized()
	{
		if (!this.IsInitialized)
		{
			throw new InvalidOperationException("TouchList must be initialized before using it. Please wait for Awake to finish.");
		}
	}

	// Token: 0x060097FD RID: 38909 RVA: 0x00312626 File Offset: 0x00310826
	private void Update()
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.UpdateTouchInput();
		}
		else
		{
			this.UpdateMouseInput();
		}
		if (this.m_isHoveredOverTouchList)
		{
			this.OnHover(false);
		}
	}

	// Token: 0x060097FE RID: 38910 RVA: 0x00312654 File Offset: 0x00310854
	private void UpdateTouchInput()
	{
		Vector3 touchPosition = this.GetTouchPosition();
		if (UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			if (this.dragItemBegin != null && this.ItemDragFinished != null)
			{
				this.ItemDragFinished(this.touchBeginItem, this.GetItemDragDelta(touchPosition));
				this.dragItemBegin = null;
			}
			this.touchBeginItem = null;
			this.touchBeginScreenPosition = null;
			this.dragBeginOffsetFromContent = null;
		}
		if (this.touchBeginScreenPosition != null)
		{
			Func<int, float, bool> func = delegate(int dimension, float inchThreshold)
			{
				int vector2Dimension = this.GetVector2Dimension(dimension);
				float f = this.touchBeginScreenPosition.Value[vector2Dimension] - UniversalInputManager.Get().GetMousePosition()[vector2Dimension];
				float num9 = inchThreshold * ((Screen.dpi > 0f) ? Screen.dpi : 150f);
				return Mathf.Abs(f) > num9;
			};
			if (this.ItemDragStarted != null && func(this.layoutDimension2, 0.05f) && this.ItemDragStarted(this.touchBeginItem, this.GetItemDragDelta(touchPosition)))
			{
				this.dragItemBegin = new Vector3?(this.GetTouchPosition());
				this.touchBeginScreenPosition = null;
			}
			else if (func(this.layoutDimension1, 0.05f))
			{
				this.dragBeginContentPosition = this.content.transform.localPosition;
				this.dragBeginOffsetFromContent = new Vector3?(this.dragBeginContentPosition - this.lastTouchPosition);
				this.touchBeginItem = null;
				this.touchBeginScreenPosition = null;
			}
		}
		float num3;
		if (this.dragItemBegin != null)
		{
			if (!this.ItemDragged(this.touchBeginItem, this.GetItemDragDelta(touchPosition)))
			{
				this.dragItemBegin = null;
				this.touchBeginItem = null;
			}
		}
		else if (this.dragBeginOffsetFromContent != null)
		{
			float num = touchPosition[this.layoutDimension1] + this.dragBeginOffsetFromContent.Value[this.layoutDimension1];
			float num2 = this.GetOutOfBoundsDist(num);
			if (num2 != 0f)
			{
				num2 = TouchList.OutOfBoundsDistReducer(Mathf.Abs(num2)) * Mathf.Sign(num2);
				num = ((num2 < 0f) ? (-this.excessContentSize + num2) : num2);
			}
			Vector3 localPosition = this.content.transform.localPosition;
			this.lastContentPosition = localPosition[this.layoutDimension1];
			localPosition[this.layoutDimension1] = num;
			this.content.transform.localPosition = localPosition;
			if (this.lastContentPosition != localPosition[this.layoutDimension1])
			{
				this.OnScrolled();
			}
		}
		else
		{
			float contentPosition = this.content.transform.localPosition[this.layoutDimension1];
			float outOfBoundsDist = this.GetOutOfBoundsDist(contentPosition);
			num3 = this.content.transform.localPosition[this.layoutDimension1] - this.lastContentPosition;
			float num4 = num3 / Time.fixedDeltaTime;
			if (this.maxKineticScrollSpeed > Mathf.Epsilon)
			{
				if (num4 > 0f)
				{
					num4 = Mathf.Min(num4, this.maxKineticScrollSpeed);
				}
				else
				{
					num4 = Mathf.Max(num4, -this.maxKineticScrollSpeed);
				}
			}
			if (outOfBoundsDist != 0f)
			{
				Vector3 localPosition2 = this.content.transform.localPosition;
				this.lastContentPosition = contentPosition;
				float num5 = -400f * outOfBoundsDist - TouchList.ScrollBoundsSpringB * num4;
				float num6 = num4 + num5 * Time.fixedDeltaTime;
				ref Vector3 ptr = ref localPosition2;
				int index = this.layoutDimension1;
				ptr[index] += num6 * Time.fixedDeltaTime;
				if (Mathf.Abs(this.GetOutOfBoundsDist(localPosition2[this.layoutDimension1])) < 0.05f)
				{
					float value = (Mathf.Abs(localPosition2[this.layoutDimension1] + this.excessContentSize) < Mathf.Abs(localPosition2[this.layoutDimension1])) ? (-this.excessContentSize) : 0f;
					localPosition2[this.layoutDimension1] = value;
					this.lastContentPosition = value;
				}
				this.content.transform.localPosition = localPosition2;
				this.OnScrolled();
			}
			else if (num4 != 0f)
			{
				this.lastContentPosition = this.content.transform.localPosition[this.layoutDimension1];
				float num7 = -Mathf.Sign(num4) * 10000f;
				float num8 = num4 + num7 * Time.fixedDeltaTime;
				if (Mathf.Abs(num8) >= 0.01f && Mathf.Sign(num8) == Mathf.Sign(num4))
				{
					Vector3 localPosition3 = this.content.transform.localPosition;
					ref Vector3 ptr = ref localPosition3;
					int index = this.layoutDimension1;
					ptr[index] += num8 * Time.fixedDeltaTime;
					this.content.transform.localPosition = localPosition3;
					this.OnScrolled();
				}
			}
			else
			{
				this.FixUpScrolling();
			}
		}
		num3 = this.content.transform.localPosition[this.layoutDimension1] - this.lastContentPosition;
		if (num3 != 0f)
		{
			this.PreBufferLongListItems(num3 < 0f);
		}
		this.lastTouchPosition = touchPosition;
	}

	// Token: 0x060097FF RID: 38911 RVA: 0x00312B68 File Offset: 0x00310D68
	private void PreBufferLongListItems(bool scrolledAhead)
	{
		if (this.LongListBehavior == null)
		{
			return;
		}
		this.allowModification = true;
		if (scrolledAhead && this.GetNumItemsAheadOfView() < this.longListBehavior.MinBuffer)
		{
			bool flag = this.CanScrollAhead;
			if (this.renderedItems.Count > 0)
			{
				Bounds bounds = this.CalculateLocalClipBounds();
				ITouchListItem key = this.renderedItems[this.renderedItems.Count - 1];
				Vector3 max = this.itemInfos[key].Max;
				float num = bounds.min[this.layoutDimension1];
				if (max[this.layoutDimension1] < num)
				{
					this.RefreshList(0, true);
					flag = false;
				}
			}
			if (flag)
			{
				this.LoadAhead();
			}
		}
		else if (!scrolledAhead && this.GetNumItemsBehindView() < this.longListBehavior.MinBuffer)
		{
			bool flag2 = this.CanScrollBehind;
			if (this.renderedItems.Count > 0)
			{
				Bounds bounds2 = this.CalculateLocalClipBounds();
				ITouchListItem key2 = this.renderedItems[0];
				Vector3 min = this.itemInfos[key2].Min;
				float num2 = bounds2.max[this.layoutDimension1];
				if (min[this.layoutDimension1] > num2)
				{
					this.RefreshList(0, true);
					flag2 = false;
				}
			}
			if (flag2)
			{
				this.LoadBehind();
			}
		}
		this.allowModification = false;
	}

	// Token: 0x06009800 RID: 38912 RVA: 0x00312CCC File Offset: 0x00310ECC
	private void UpdateMouseInput()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (camera == null)
		{
			return;
		}
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		RaycastHit raycastHit;
		if (!base.GetComponent<Collider>().Raycast(ray, out raycastHit, camera.farClipPlane))
		{
			return;
		}
		float num = 0f;
		if (Input.GetAxis("Mouse ScrollWheel") < 0f && this.CanScrollAhead)
		{
			num -= this.scrollWheelIncrement;
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0f && this.CanScrollBehind)
		{
			num += this.scrollWheelIncrement;
		}
		if (Mathf.Abs(num) > Mathf.Epsilon)
		{
			float num2 = this.content.transform.localPosition[this.layoutDimension1] + num;
			if (num2 <= -this.excessContentSize)
			{
				num2 = -this.excessContentSize;
			}
			else if (num2 >= 0f)
			{
				num2 = 0f;
			}
			Vector3 localPosition = this.content.transform.localPosition;
			this.lastContentPosition = localPosition[this.layoutDimension1];
			localPosition[this.layoutDimension1] = num2;
			this.content.transform.localPosition = localPosition;
			float num3 = this.content.transform.localPosition[this.layoutDimension1] - this.lastContentPosition;
			this.lastContentPosition = this.content.transform.localPosition[this.layoutDimension1];
			if (num3 != 0f)
			{
				this.PreBufferLongListItems(num3 < 0f);
			}
			this.FixUpScrolling();
			this.OnScrolled();
		}
	}

	// Token: 0x06009801 RID: 38913 RVA: 0x00312E78 File Offset: 0x00311078
	private float GetOutOfBoundsDist(float contentPosition)
	{
		float result = 0f;
		if (contentPosition < -this.excessContentSize)
		{
			result = contentPosition + this.excessContentSize;
		}
		else if (contentPosition > 0f)
		{
			result = contentPosition;
		}
		return result;
	}

	// Token: 0x06009802 RID: 38914 RVA: 0x00312EAC File Offset: 0x003110AC
	private void ScrollToItem_Internal(ITouchListItem item)
	{
		Bounds bounds = this.CalculateLocalClipBounds();
		TouchList.ItemInfo itemInfo = this.itemInfos[item];
		float num = itemInfo.Max[this.layoutDimension1] - bounds.max[this.layoutDimension1];
		if (num > 0f)
		{
			Vector3 zero = Vector3.zero;
			zero[this.layoutDimension1] = num;
			this.content.transform.Translate(zero);
			this.lastContentPosition = this.content.transform.localPosition[this.layoutDimension1];
			this.PreBufferLongListItems(true);
			this.OnScrolled();
		}
		float num2 = bounds.min[this.layoutDimension1] - itemInfo.Min[this.layoutDimension1];
		if (num2 > 0f)
		{
			Vector3 zero2 = Vector3.zero;
			zero2[this.layoutDimension1] = -num2;
			this.content.transform.Translate(zero2);
			this.lastContentPosition = this.content.transform.localPosition[this.layoutDimension1];
			this.PreBufferLongListItems(false);
			this.OnScrolled();
		}
	}

	// Token: 0x06009803 RID: 38915 RVA: 0x00312FEA File Offset: 0x003111EA
	private void OnScrolled()
	{
		this.UpdateBackgroundScroll();
		this.SetVisibilityOfAllItems();
		if (this.Scrolled != null)
		{
			this.Scrolled();
		}
	}

	// Token: 0x06009804 RID: 38916 RVA: 0x0031300C File Offset: 0x0031120C
	private Vector3 GetTouchPosition()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (camera == null)
		{
			return Vector3.zero;
		}
		float num = Vector3.Distance(camera.transform.position, base.GetComponent<Collider>().bounds.min);
		float num2 = Vector3.Distance(camera.transform.position, base.GetComponent<Collider>().bounds.max);
		Vector3 inPoint = (num < num2) ? base.GetComponent<Collider>().bounds.min : base.GetComponent<Collider>().bounds.max;
		Plane plane = new Plane(-camera.transform.forward, inPoint);
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		float distance;
		plane.Raycast(ray, out distance);
		return base.transform.InverseTransformPoint(ray.GetPoint(distance));
	}

	// Token: 0x06009805 RID: 38917 RVA: 0x003130FC File Offset: 0x003112FC
	private float GetItemDragDelta(Vector3 touchPosition)
	{
		if (this.dragItemBegin != null)
		{
			return touchPosition[this.layoutDimension2] - this.dragItemBegin.Value[this.layoutDimension2];
		}
		return 0f;
	}

	// Token: 0x06009806 RID: 38918 RVA: 0x00313144 File Offset: 0x00311344
	private void LoadAhead()
	{
		bool flag = this.allowModification;
		bool flag2 = this.layoutSuspended;
		this.allowModification = true;
		int num = -1;
		int num2 = 0;
		int numItemsBehindView = this.GetNumItemsBehindView();
		for (int i = 0; i < numItemsBehindView - this.longListBehavior.MinBuffer; i++)
		{
			ITouchListItem item = this.renderedItems[0];
			this.RemoveAt(0, false);
			this.longListBehavior.ReleaseItem(item);
		}
		float num3 = this.CalculateLocalClipBounds().max[this.layoutDimension1];
		int num4 = 0;
		int num5 = (this.renderedItems.Count == 0) ? 0 : (this.itemInfos[this.renderedItems.Last<ITouchListItem>()].LongListIndex + 1);
		while (num5 < this.longListBehavior.AllItemsCount && this.renderedItems.Count < this.longListBehavior.MaxAcquiredItems && num4 < this.longListBehavior.MinBuffer)
		{
			if (this.longListBehavior.IsItemShowable(num5))
			{
				if (num < 0)
				{
					num = this.renderedItems.Count;
				}
				ITouchListItem touchListItem = this.longListBehavior.AcquireItem(num5);
				this.Add(touchListItem, false);
				TouchList.ItemInfo itemInfo = this.itemInfos[touchListItem];
				itemInfo.LongListIndex = num5;
				num2++;
				if (itemInfo.Min[this.layoutDimension1] > num3)
				{
					num4++;
				}
			}
			num5++;
		}
		if (num >= 0)
		{
			this.layoutSuspended = false;
			this.RepositionItems(num, null);
		}
		this.allowModification = flag;
		if (flag2 != this.layoutSuspended)
		{
			this.layoutSuspended = flag2;
		}
	}

	// Token: 0x06009807 RID: 38919 RVA: 0x003132E8 File Offset: 0x003114E8
	private void LoadBehind()
	{
		bool flag = this.allowModification;
		this.allowModification = true;
		int num = 0;
		int numItemsAheadOfView = this.GetNumItemsAheadOfView();
		for (int i = 0; i < numItemsAheadOfView - this.longListBehavior.MinBuffer; i++)
		{
			ITouchListItem item = this.renderedItems[this.renderedItems.Count - 1];
			this.RemoveAt(this.renderedItems.Count - 1, false);
			this.longListBehavior.ReleaseItem(item);
		}
		float num2 = this.CalculateLocalClipBounds().min[this.layoutDimension1];
		int num3 = 0;
		int num4 = (this.renderedItems.Count == 0) ? (this.longListBehavior.AllItemsCount - 1) : (this.itemInfos[this.renderedItems.First<ITouchListItem>()].LongListIndex - 1);
		while (num4 >= 0 && this.renderedItems.Count < this.longListBehavior.MaxAcquiredItems && num3 < this.longListBehavior.MinBuffer)
		{
			if (this.longListBehavior.IsItemShowable(num4))
			{
				ITouchListItem touchListItem = this.longListBehavior.AcquireItem(num4);
				this.InsertAndPositionBehind(touchListItem, num4);
				TouchList.ItemInfo itemInfo = this.itemInfos[touchListItem];
				itemInfo.LongListIndex = num4;
				num++;
				if (itemInfo.Max[this.layoutDimension1] < num2)
				{
					num3++;
				}
			}
			num4--;
		}
		this.allowModification = flag;
	}

	// Token: 0x06009808 RID: 38920 RVA: 0x0031345C File Offset: 0x0031165C
	private int GetNumItemsBehindView()
	{
		float num = this.CalculateLocalClipBounds().min[this.layoutDimension1];
		for (int i = 0; i < this.renderedItems.Count; i++)
		{
			ITouchListItem key = this.renderedItems[i];
			if (this.itemInfos[key].Max[this.layoutDimension1] > num)
			{
				return i;
			}
		}
		return this.renderedItems.Count;
	}

	// Token: 0x06009809 RID: 38921 RVA: 0x003134DC File Offset: 0x003116DC
	private int GetNumItemsAheadOfView()
	{
		float num = this.CalculateLocalClipBounds().max[this.layoutDimension1];
		for (int i = this.renderedItems.Count - 1; i >= 0; i--)
		{
			ITouchListItem key = this.renderedItems[i];
			if (this.itemInfos[key].Min[this.layoutDimension1] < num)
			{
				return this.renderedItems.Count - 1 - i;
			}
		}
		return this.renderedItems.Count;
	}

	// Token: 0x0600980A RID: 38922 RVA: 0x0031356C File Offset: 0x0031176C
	public void RefreshList(int startingLongListIndex, bool preserveScrolling)
	{
		if (this.longListBehavior == null)
		{
			return;
		}
		bool flag = this.allowModification;
		this.allowModification = true;
		int num = (this.SelectedItem == null) ? -1 : this.itemInfos[this.SelectedItem].LongListIndex;
		int num2 = -2;
		int num3 = -1;
		if (startingLongListIndex > 0)
		{
			for (int i = 0; i < this.renderedItems.Count; i++)
			{
				ITouchListItem key = this.renderedItems[i];
				if (this.itemInfos[key].LongListIndex >= startingLongListIndex)
				{
					num3 = i;
					break;
				}
				num2 = i;
			}
		}
		else
		{
			num3 = 0;
		}
		int num4 = (num3 == -1) ? (num2 + 1) : num3;
		Bounds bounds = base.GetComponent<Collider>().bounds;
		Vector3? initialItemPosition = null;
		Vector3 vector = Vector3.zero;
		int num5 = (this.orientation == TouchList.Orientation.Vertical) ? -1 : 1;
		if (preserveScrolling)
		{
			vector = this.content.transform.position;
			ref Vector3 ptr = ref vector;
			int index = this.layoutDimension1;
			ptr[index] -= (float)num5 * bounds.extents[this.layoutDimension1];
			ptr = ref vector;
			index = this.layoutDimension1;
			ptr[index] += (float)num5 * this.padding[this.GetVector2Dimension(this.layoutDimension1)];
			vector[this.layoutDimension2] = bounds.center[this.layoutDimension2];
			vector[this.layoutDimension3] = bounds.center[this.layoutDimension3];
			Vector3 localPosition = this.content.transform.localPosition;
			this.content.transform.localPosition = Vector3.zero;
			Bounds bounds2 = this.CalculateLocalClipBounds();
			Vector3 min = bounds2.min;
			min[this.layoutDimension1] = -localPosition[this.layoutDimension1] + bounds2.min[this.layoutDimension1];
			this.content.transform.localPosition = localPosition;
			initialItemPosition = new Vector3?(min);
			if (num2 >= 0)
			{
				ITouchListItem touchListItem = this.renderedItems[num2];
				TouchList.ItemInfo itemInfo = this.itemInfos[touchListItem];
				vector = touchListItem.transform.position - itemInfo.Offset;
				ptr = ref vector;
				index = this.layoutDimension1;
				ptr[index] += (float)num5 * this.elementSpacing;
				ITouchListItem touchListItem2 = this.renderedItems[0];
				TouchList.ItemInfo itemInfo2 = this.itemInfos[touchListItem2];
				initialItemPosition = new Vector3?(touchListItem2.transform.localPosition - itemInfo2.Offset);
			}
		}
		int num6 = 0;
		if (num4 >= 0)
		{
			for (int j = this.renderedItems.Count - 1; j >= num4; j--)
			{
				num6++;
				ITouchListItem item = this.renderedItems[j];
				this.RemoveAt(j, false);
				this.longListBehavior.ReleaseItem(item);
			}
		}
		if (num3 < 0)
		{
			num3 = num2 + 1;
			if (num3 < 0)
			{
				num3 = 0;
			}
		}
		int num7 = 0;
		int num8 = startingLongListIndex;
		while (num8 < this.longListBehavior.AllItemsCount && this.renderedItems.Count < this.longListBehavior.MaxAcquiredItems)
		{
			if (this.longListBehavior.IsItemShowable(num8))
			{
				bool flag2 = true;
				if (preserveScrolling)
				{
					flag2 = false;
					Vector3 itemSize = this.longListBehavior.GetItemSize(num8);
					Vector3 vector2 = vector;
					ref Vector3 ptr = ref vector2;
					int index = this.layoutDimension1;
					ptr[index] += (float)num5 * itemSize[this.layoutDimension1];
					if (bounds.Contains(vector) || bounds.Contains(vector2))
					{
						flag2 = true;
					}
					vector = vector2;
					ptr = ref vector;
					index = this.layoutDimension1;
					ptr[index] += (float)num5 * this.elementSpacing;
				}
				if (flag2)
				{
					num7++;
					ITouchListItem touchListItem3 = this.longListBehavior.AcquireItem(num8);
					this.Add(touchListItem3, false);
					this.itemInfos[touchListItem3].LongListIndex = num8;
				}
			}
			num8++;
		}
		this.RepositionItems(num3, initialItemPosition);
		if (num3 == 0)
		{
			this.LoadBehind();
		}
		if (num4 >= 0)
		{
			this.LoadAhead();
		}
		bool flag3 = false;
		float outOfBoundsDist = this.GetOutOfBoundsDist(this.content.transform.localPosition[this.layoutDimension1]);
		if (outOfBoundsDist != 0f && this.excessContentSize > 0f)
		{
			Vector3 localPosition2 = this.content.transform.localPosition;
			ref Vector3 ptr = ref localPosition2;
			int index = this.layoutDimension1;
			ptr[index] -= outOfBoundsDist;
			float num9 = localPosition2[this.layoutDimension1] - this.content.transform.localPosition[this.layoutDimension1];
			this.content.transform.localPosition = localPosition2;
			this.lastContentPosition = this.content.transform.localPosition[this.layoutDimension1];
			if (num9 < 0f)
			{
				this.LoadAhead();
			}
			else
			{
				this.LoadBehind();
			}
			flag3 = true;
		}
		if (num >= 0 && this.renderedItems.Count > 0 && num >= this.itemInfos[this.renderedItems.First<ITouchListItem>()].LongListIndex && num <= this.itemInfos[this.renderedItems.Last<ITouchListItem>()].LongListIndex)
		{
			for (int k = 0; k < this.renderedItems.Count; k++)
			{
				ISelectableTouchListItem selectableTouchListItem = this.renderedItems[k] as ISelectableTouchListItem;
				if (selectableTouchListItem != null && this.itemInfos[selectableTouchListItem].LongListIndex == num)
				{
					this.selection = new int?(k);
					selectableTouchListItem.Selected();
					break;
				}
			}
		}
		flag3 = (this.RecalculateLongListContentSize(false) || flag3);
		this.allowModification = flag;
		if (flag3)
		{
			this.OnScrolled();
			this.OnScrollingEnabledChanged();
		}
	}

	// Token: 0x0600980B RID: 38923 RVA: 0x00313B94 File Offset: 0x00311D94
	private void OnScrollingEnabledChanged()
	{
		if (this.ScrollingEnabledChanged == null)
		{
			return;
		}
		if (this.longListBehavior == null)
		{
			this.ScrollingEnabledChanged(this.excessContentSize > 0f && this.scrollEnabled);
			return;
		}
		this.ScrollingEnabledChanged(this.m_fullListContentSize > this.ClipSize[this.GetVector2Dimension(this.layoutDimension1)] && this.scrollEnabled);
	}

	// Token: 0x0600980C RID: 38924 RVA: 0x00313C0C File Offset: 0x00311E0C
	public void RecalculateItemSizeAndOffsets(bool ignoreCurrentPosition)
	{
		for (int i = 0; i < this.renderedItems.Count; i++)
		{
			this.itemInfos[this.renderedItems[i]].CalculateSizeAndOffset(this.layoutPlane, ignoreCurrentPosition);
		}
		this.RepositionItems(0, null);
	}

	// Token: 0x0600980D RID: 38925 RVA: 0x00313C64 File Offset: 0x00311E64
	private void RepositionItems(int startingIndex, Vector3? initialItemPosition = null)
	{
		if (this.layoutSuspended)
		{
			return;
		}
		if (this.orientation == TouchList.Orientation.Vertical)
		{
			base.transform.localScale = Vector3.one;
		}
		Vector3 localPosition = this.content.transform.localPosition;
		this.content.transform.localPosition = Vector3.zero;
		Vector3 a = this.CalculateLocalClipBounds().min;
		if (initialItemPosition != null)
		{
			a = initialItemPosition.Value;
		}
		ref Vector3 ptr = ref a;
		int index = this.layoutDimension1;
		ptr[index] += this.padding[this.GetVector2Dimension(this.layoutDimension1)];
		a[this.layoutDimension3] = 0f;
		this.content.transform.localPosition = localPosition;
		this.ValidateBreadth();
		startingIndex -= startingIndex % this.breadth;
		if (startingIndex > 0)
		{
			int num = startingIndex - this.breadth;
			float num2 = float.MinValue;
			int num3 = num;
			while (num3 < startingIndex && num3 < this.renderedItems.Count)
			{
				ITouchListItem key = this.renderedItems[num3];
				num2 = Mathf.Max(this.itemInfos[key].Max[this.layoutDimension1], num2);
				num3++;
			}
			a[this.layoutDimension1] = num2 + this.elementSpacing;
		}
		Vector3 zero = Vector3.zero;
		zero[this.layoutDimension1] = 1f;
		for (int i = startingIndex; i < this.renderedItems.Count; i++)
		{
			ITouchListItem touchListItem = this.renderedItems[i];
			if (!touchListItem.IsHeader && !touchListItem.Visible)
			{
				this.renderedItems[i].Visible = false;
				this.renderedItems[i].gameObject.SetActive(false);
			}
			else
			{
				TouchList.ItemInfo itemInfo = this.itemInfos[this.renderedItems[i]];
				Vector3 localPosition2 = a + itemInfo.Offset;
				localPosition2[this.layoutDimension2] = this.GetBreadthPosition(i) + itemInfo.Offset[this.layoutDimension2];
				this.renderedItems[i].transform.localPosition = localPosition2;
				if ((i + 1) % this.breadth == 0)
				{
					a = (itemInfo.Max[this.layoutDimension1] + this.elementSpacing) * zero;
				}
			}
		}
		this.RecalculateSize();
		this.UpdateBackgroundScroll();
		if (this.orientation == TouchList.Orientation.Vertical)
		{
			base.transform.localScale = this.GetNegatedScale(Vector3.one);
		}
		this.SetVisibilityOfAllItems();
	}

	// Token: 0x0600980E RID: 38926 RVA: 0x00313F20 File Offset: 0x00312120
	private void InsertAndPositionBehind(ITouchListItem item, int longListIndex)
	{
		if (this.renderedItems.Count == 0)
		{
			this.Add(item, true);
			return;
		}
		ITouchListItem touchListItem = this.renderedItems.FirstOrDefault<ITouchListItem>();
		if (touchListItem == null)
		{
			this.Insert(0, item, true);
			return;
		}
		if (this.orientation == TouchList.Orientation.Vertical)
		{
			base.transform.localScale = Vector3.one;
		}
		TouchList.ItemInfo itemInfo = this.itemInfos[touchListItem];
		Vector3 vector = touchListItem.transform.localPosition - itemInfo.Offset;
		this.Insert(0, item, false);
		this.itemInfos[item].LongListIndex = longListIndex;
		TouchList.ItemInfo itemInfo2 = this.itemInfos[item];
		Vector3 vector2 = vector;
		float num = itemInfo2.Size[this.layoutDimension1] + this.elementSpacing;
		vector2[this.layoutDimension1] = vector2[this.layoutDimension1] - num;
		vector2 += itemInfo2.Offset;
		item.transform.localPosition = vector2;
		int? num2 = this.selection;
		int num3 = -1;
		if ((num2.GetValueOrDefault() == num3 & num2 != null) && item is ISelectableTouchListItem && ((ISelectableTouchListItem)item).IsSelected())
		{
			this.selection = new int?(0);
		}
		this.RecalculateSize();
		this.UpdateBackgroundScroll();
		if (this.orientation == TouchList.Orientation.Vertical)
		{
			base.transform.localScale = this.GetNegatedScale(Vector3.one);
		}
		bool active = this.IsItemVisible(0);
		item.gameObject.SetActive(active);
	}

	// Token: 0x0600980F RID: 38927 RVA: 0x00314098 File Offset: 0x00312298
	private void RecalculateSize()
	{
		float num = Math.Abs((base.GetComponent<Collider>() as BoxCollider).size[this.layoutDimension1]);
		float num2 = -num / 2f;
		float num3 = num2;
		if (this.renderedItems.Any<ITouchListItem>())
		{
			this.ValidateBreadth();
			int num4 = this.renderedItems.Count - 1;
			int num5 = num4 - num4 % this.breadth;
			int num6 = Math.Min(num5 + this.breadth, this.renderedItems.Count);
			for (int i = num5; i < num6; i++)
			{
				ITouchListItem key = this.renderedItems[i];
				num3 = Math.Max(this.itemInfos[key].Max[this.layoutDimension1], num3);
			}
			this.contentSize = num3 - num2 + this.padding[this.GetVector2Dimension(this.layoutDimension1)];
			this.excessContentSize = Math.Max(this.contentSize - num, 0f);
		}
		else
		{
			this.contentSize = 0f;
			this.excessContentSize = 0f;
		}
		this.OnScrollingEnabledChanged();
	}

	// Token: 0x06009810 RID: 38928 RVA: 0x003141B8 File Offset: 0x003123B8
	public bool RecalculateLongListContentSize(bool fireOnScroll = true)
	{
		if (this.longListBehavior == null)
		{
			return false;
		}
		float fullListContentSize = this.m_fullListContentSize;
		this.m_fullListContentSize = 0f;
		bool flag = true;
		for (int i = 0; i < this.longListBehavior.AllItemsCount; i++)
		{
			if (this.longListBehavior.IsItemShowable(i))
			{
				Vector3 itemSize = this.longListBehavior.GetItemSize(i);
				this.m_fullListContentSize += itemSize[this.layoutDimension1];
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.m_fullListContentSize += this.elementSpacing;
				}
			}
		}
		if (this.m_fullListContentSize > 0f)
		{
			this.m_fullListContentSize += 2f * this.padding[this.GetVector2Dimension(this.layoutDimension1)];
		}
		bool flag2 = fullListContentSize != this.m_fullListContentSize;
		if (flag2 && fireOnScroll)
		{
			this.OnScrolled();
			this.OnScrollingEnabledChanged();
		}
		return flag2;
	}

	// Token: 0x06009811 RID: 38929 RVA: 0x003142A0 File Offset: 0x003124A0
	private void UpdateBackgroundBounds()
	{
		if (this.background == null)
		{
			return;
		}
		Vector3 size = (base.GetComponent<Collider>() as BoxCollider).size;
		size[this.layoutDimension1] = Math.Abs(size[this.layoutDimension1]);
		size[this.layoutDimension3] = 0f;
		Camera camera = CameraUtils.FindFirstByLayer((GameLayer)base.gameObject.layer);
		if (camera == null)
		{
			return;
		}
		float num = Vector3.Distance(camera.transform.position, base.GetComponent<Collider>().bounds.min);
		float num2 = Vector3.Distance(camera.transform.position, base.GetComponent<Collider>().bounds.max);
		Vector3 position = (num > num2) ? base.GetComponent<Collider>().bounds.min : base.GetComponent<Collider>().bounds.max;
		Vector3 zero = Vector3.zero;
		zero[this.layoutDimension3] = this.content.transform.InverseTransformPoint(position)[this.layoutDimension3];
		this.background.SetBounds(new Bounds(zero, size));
		this.UpdateBackgroundScroll();
	}

	// Token: 0x06009812 RID: 38930 RVA: 0x003143E4 File Offset: 0x003125E4
	private void UpdateBackgroundScroll()
	{
		if (this.background == null)
		{
			return;
		}
		float num = Math.Abs((base.GetComponent<Collider>() as BoxCollider).size[this.layoutDimension1]);
		float num2 = this.content.transform.localPosition[this.layoutDimension1];
		if (this.orientation == TouchList.Orientation.Vertical)
		{
			num2 *= -1f;
		}
		Vector2 offset = this.background.Offset;
		offset[this.GetVector2Dimension(this.layoutDimension1)] = num2 / num;
		this.background.Offset = offset;
	}

	// Token: 0x06009813 RID: 38931 RVA: 0x00314484 File Offset: 0x00312684
	private float GetBreadthPosition(int itemIndex)
	{
		float num = this.padding[this.GetVector2Dimension(this.layoutDimension2)];
		float num2 = 0f;
		int num3 = itemIndex - itemIndex % this.breadth;
		int num4 = Math.Min(num3 + this.breadth, this.renderedItems.Count);
		for (int i = num3; i < num4; i++)
		{
			if (i == itemIndex)
			{
				num2 = num;
			}
			num += this.itemInfos[this.renderedItems[i]].Size[this.layoutDimension2];
		}
		num += this.padding[this.GetVector2Dimension(this.layoutDimension2)];
		float num5 = 0f;
		float num6 = (base.GetComponent<Collider>() as BoxCollider).size[this.layoutDimension2];
		TouchList.Alignment alignment = this.alignment;
		if (this.orientation == TouchList.Orientation.Horizontal && this.alignment != TouchList.Alignment.Mid)
		{
			alignment = (this.alignment ^ TouchList.Alignment.Max);
		}
		switch (alignment)
		{
		case TouchList.Alignment.Min:
			num5 = -num6 / 2f;
			break;
		case TouchList.Alignment.Mid:
			num5 = -num / 2f;
			break;
		case TouchList.Alignment.Max:
			num5 = num6 / 2f - num;
			break;
		}
		return num5 + num2;
	}

	// Token: 0x06009814 RID: 38932 RVA: 0x003145B8 File Offset: 0x003127B8
	private Vector3 GetNegatedScale(Vector3 scale)
	{
		ref Vector3 ptr = ref scale;
		int index = (this.layoutPlane == TouchList.LayoutPlane.XY) ? 1 : 2;
		ptr[index] *= -1f;
		return scale;
	}

	// Token: 0x06009815 RID: 38933 RVA: 0x003145EA File Offset: 0x003127EA
	private int GetVector2Dimension(int vec3Dimension)
	{
		if (vec3Dimension != 0)
		{
			return 1;
		}
		return vec3Dimension;
	}

	// Token: 0x06009816 RID: 38934 RVA: 0x003145F2 File Offset: 0x003127F2
	private int GetVector3Dimension(int vec2Dimension)
	{
		if (vec2Dimension == 0 || this.layoutPlane == TouchList.LayoutPlane.XY)
		{
			return vec2Dimension;
		}
		return 2;
	}

	// Token: 0x06009817 RID: 38935 RVA: 0x00314602 File Offset: 0x00312802
	private void ValidateBreadth()
	{
		if (this.longListBehavior != null)
		{
			this.breadth = 1;
			return;
		}
		this.breadth = Math.Max(this.breadth, 1);
	}

	// Token: 0x06009818 RID: 38936 RVA: 0x00314628 File Offset: 0x00312828
	private Bounds CalculateLocalClipBounds()
	{
		Vector3 b = this.content.transform.InverseTransformPoint(base.GetComponent<Collider>().bounds.min);
		Vector3 a = this.content.transform.InverseTransformPoint(base.GetComponent<Collider>().bounds.max);
		Vector3 center = (a + b) / 2f;
		Vector3 size = VectorUtils.Abs(a - b);
		return new Bounds(center, size);
	}

	// Token: 0x04007F0E RID: 32526
	public TouchList.Orientation orientation;

	// Token: 0x04007F0F RID: 32527
	public TouchList.Alignment alignment = TouchList.Alignment.Mid;

	// Token: 0x04007F10 RID: 32528
	public TouchList.LayoutPlane layoutPlane;

	// Token: 0x04007F11 RID: 32529
	public float elementSpacing;

	// Token: 0x04007F12 RID: 32530
	public Vector2 padding = Vector2.zero;

	// Token: 0x04007F13 RID: 32531
	public int breadth = 1;

	// Token: 0x04007F14 RID: 32532
	public float itemDragFinishDistance;

	// Token: 0x04007F15 RID: 32533
	public TiledBackground background;

	// Token: 0x04007F16 RID: 32534
	public float scrollWheelIncrement = 30f;

	// Token: 0x04007F17 RID: 32535
	public Float_MobileOverride maxKineticScrollSpeed = new Float_MobileOverride();

	// Token: 0x04007F18 RID: 32536
	private GameObject content;

	// Token: 0x04007F19 RID: 32537
	private List<ITouchListItem> renderedItems = new List<ITouchListItem>();

	// Token: 0x04007F1A RID: 32538
	private Map<ITouchListItem, TouchList.ItemInfo> itemInfos = new Map<ITouchListItem, TouchList.ItemInfo>();

	// Token: 0x04007F1B RID: 32539
	private int layoutDimension1;

	// Token: 0x04007F1C RID: 32540
	private int layoutDimension2;

	// Token: 0x04007F1D RID: 32541
	private int layoutDimension3;

	// Token: 0x04007F1E RID: 32542
	private float contentSize;

	// Token: 0x04007F1F RID: 32543
	private float excessContentSize;

	// Token: 0x04007F20 RID: 32544
	private float m_fullListContentSize;

	// Token: 0x04007F21 RID: 32545
	private Vector2? touchBeginScreenPosition;

	// Token: 0x04007F22 RID: 32546
	private Vector3? dragBeginOffsetFromContent;

	// Token: 0x04007F23 RID: 32547
	private Vector3 dragBeginContentPosition = Vector3.zero;

	// Token: 0x04007F24 RID: 32548
	private Vector3 lastTouchPosition = Vector3.zero;

	// Token: 0x04007F25 RID: 32549
	private float lastContentPosition;

	// Token: 0x04007F26 RID: 32550
	private ITouchListItem touchBeginItem;

	// Token: 0x04007F27 RID: 32551
	private bool m_isHoveredOverTouchList;

	// Token: 0x04007F28 RID: 32552
	private PegUIElement m_hoveredOverItem;

	// Token: 0x04007F29 RID: 32553
	private TouchList.ILongListBehavior longListBehavior;

	// Token: 0x04007F2A RID: 32554
	private bool allowModification = true;

	// Token: 0x04007F2B RID: 32555
	private Vector3? dragItemBegin;

	// Token: 0x04007F2C RID: 32556
	private bool layoutSuspended;

	// Token: 0x04007F2D RID: 32557
	private int? selection;

	// Token: 0x04007F2E RID: 32558
	private bool scrollEnabled = true;

	// Token: 0x04007F2F RID: 32559
	private const float ScrollDragThreshold = 0.05f;

	// Token: 0x04007F30 RID: 32560
	private const float ItemDragThreshold = 0.05f;

	// Token: 0x04007F31 RID: 32561
	private const float KineticScrollFriction = 10000f;

	// Token: 0x04007F32 RID: 32562
	private const float MinKineticScrollSpeed = 0.01f;

	// Token: 0x04007F33 RID: 32563
	private const float ScrollBoundsSpringK = 400f;

	// Token: 0x04007F34 RID: 32564
	private static readonly float ScrollBoundsSpringB = Mathf.Sqrt(1600f);

	// Token: 0x04007F35 RID: 32565
	private const float MinOutOfBoundsDistance = 0.05f;

	// Token: 0x04007F36 RID: 32566
	private static readonly Func<float, float> OutOfBoundsDistReducer = (float dist) => 30f * (Mathf.Log(dist + 30f) - Mathf.Log(30f));

	// Token: 0x04007F3E RID: 32574
	private const float CLIPSIZE_EPSILON = 0.0001f;

	// Token: 0x02002770 RID: 10096
	public enum Orientation
	{
		// Token: 0x0400F40C RID: 62476
		Horizontal,
		// Token: 0x0400F40D RID: 62477
		Vertical
	}

	// Token: 0x02002771 RID: 10097
	public enum Alignment
	{
		// Token: 0x0400F40F RID: 62479
		Min,
		// Token: 0x0400F410 RID: 62480
		Mid,
		// Token: 0x0400F411 RID: 62481
		Max
	}

	// Token: 0x02002772 RID: 10098
	public enum LayoutPlane
	{
		// Token: 0x0400F413 RID: 62483
		XY,
		// Token: 0x0400F414 RID: 62484
		XZ
	}

	// Token: 0x02002773 RID: 10099
	// (Invoke) Token: 0x060139F1 RID: 80369
	public delegate bool SelectedIndexChangingEvent(int index);

	// Token: 0x02002774 RID: 10100
	// (Invoke) Token: 0x060139F5 RID: 80373
	public delegate void ScrollingEnabledChangedEvent(bool canScroll);

	// Token: 0x02002775 RID: 10101
	// (Invoke) Token: 0x060139F9 RID: 80377
	public delegate bool ItemDragEvent(ITouchListItem item, float dragAmount);

	// Token: 0x02002776 RID: 10102
	public interface ILongListBehavior
	{
		// Token: 0x17002CD4 RID: 11476
		// (get) Token: 0x060139FC RID: 80380
		int AllItemsCount { get; }

		// Token: 0x17002CD5 RID: 11477
		// (get) Token: 0x060139FD RID: 80381
		int MaxVisibleItems { get; }

		// Token: 0x17002CD6 RID: 11478
		// (get) Token: 0x060139FE RID: 80382
		int MinBuffer { get; }

		// Token: 0x060139FF RID: 80383
		void ReleaseAllItems();

		// Token: 0x06013A00 RID: 80384
		void ReleaseItem(ITouchListItem item);

		// Token: 0x06013A01 RID: 80385
		ITouchListItem AcquireItem(int index);

		// Token: 0x17002CD7 RID: 11479
		// (get) Token: 0x06013A02 RID: 80386
		int MaxAcquiredItems { get; }

		// Token: 0x06013A03 RID: 80387
		bool IsItemShowable(int allItemsIndex);

		// Token: 0x06013A04 RID: 80388
		Vector3 GetItemSize(int allItemsIndex);
	}

	// Token: 0x02002777 RID: 10103
	private class ItemInfo
	{
		// Token: 0x17002CD8 RID: 11480
		// (get) Token: 0x06013A05 RID: 80389 RVA: 0x00538B49 File Offset: 0x00536D49
		// (set) Token: 0x06013A06 RID: 80390 RVA: 0x00538B51 File Offset: 0x00536D51
		public Vector3 Size { get; private set; }

		// Token: 0x17002CD9 RID: 11481
		// (get) Token: 0x06013A07 RID: 80391 RVA: 0x00538B5A File Offset: 0x00536D5A
		// (set) Token: 0x06013A08 RID: 80392 RVA: 0x00538B62 File Offset: 0x00536D62
		public Vector3 Offset { get; private set; }

		// Token: 0x17002CDA RID: 11482
		// (get) Token: 0x06013A09 RID: 80393 RVA: 0x00538B6B File Offset: 0x00536D6B
		// (set) Token: 0x06013A0A RID: 80394 RVA: 0x00538B73 File Offset: 0x00536D73
		public int LongListIndex { get; set; }

		// Token: 0x17002CDB RID: 11483
		// (get) Token: 0x06013A0B RID: 80395 RVA: 0x00538B7C File Offset: 0x00536D7C
		public Vector3 Min
		{
			get
			{
				return this.item.transform.localPosition + Vector3.Scale(this.item.LocalBounds.min, VectorUtils.Abs(this.item.transform.localScale));
			}
		}

		// Token: 0x17002CDC RID: 11484
		// (get) Token: 0x06013A0C RID: 80396 RVA: 0x00538BCC File Offset: 0x00536DCC
		public Vector3 Max
		{
			get
			{
				return this.item.transform.localPosition + Vector3.Scale(this.item.LocalBounds.max, VectorUtils.Abs(this.item.transform.localScale));
			}
		}

		// Token: 0x06013A0D RID: 80397 RVA: 0x00538C1B File Offset: 0x00536E1B
		public ItemInfo(ITouchListItem item, TouchList.LayoutPlane layoutPlane)
		{
			this.item = item;
			this.CalculateSizeAndOffset(layoutPlane, false);
		}

		// Token: 0x06013A0E RID: 80398 RVA: 0x00538C34 File Offset: 0x00536E34
		public void CalculateSizeAndOffset(TouchList.LayoutPlane layoutPlane, bool ignoreCurrentPosition = false)
		{
			Vector3 vector = Vector3.Scale(this.item.LocalBounds.min, VectorUtils.Abs(this.item.transform.localScale));
			Vector3 vector2 = Vector3.Scale(this.item.LocalBounds.max, VectorUtils.Abs(this.item.transform.localScale));
			if (!ignoreCurrentPosition)
			{
				vector -= this.item.transform.localPosition;
				vector2 -= this.item.transform.localPosition;
			}
			this.Size = vector2 - vector;
			Vector3 a = vector;
			if (layoutPlane == TouchList.LayoutPlane.XZ)
			{
				a.y = vector2.y;
			}
			this.Offset = -a;
			if (!ignoreCurrentPosition)
			{
				this.Offset += this.item.transform.localPosition;
			}
		}

		// Token: 0x06013A0F RID: 80399 RVA: 0x00538D20 File Offset: 0x00536F20
		public bool Contains(Vector3 point, TouchList.LayoutPlane layoutPlane)
		{
			Vector3 min = this.Min;
			Vector3 max = this.Max;
			int index = (layoutPlane == TouchList.LayoutPlane.XY) ? 1 : 2;
			return point.x > min.x && point[index] > min[index] && point.x < max.x && point[index] < max[index];
		}

		// Token: 0x0400F415 RID: 62485
		private readonly ITouchListItem item;
	}
}
