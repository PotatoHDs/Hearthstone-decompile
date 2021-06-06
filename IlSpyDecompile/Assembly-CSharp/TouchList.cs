using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Core;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TouchList : PegUIElement
{
	public enum Orientation
	{
		Horizontal,
		Vertical
	}

	public enum Alignment
	{
		Min,
		Mid,
		Max
	}

	public enum LayoutPlane
	{
		XY,
		XZ
	}

	public delegate bool SelectedIndexChangingEvent(int index);

	public delegate void ScrollingEnabledChangedEvent(bool canScroll);

	public delegate bool ItemDragEvent(ITouchListItem item, float dragAmount);

	public interface ILongListBehavior
	{
		int AllItemsCount { get; }

		int MaxVisibleItems { get; }

		int MinBuffer { get; }

		int MaxAcquiredItems { get; }

		void ReleaseAllItems();

		void ReleaseItem(ITouchListItem item);

		ITouchListItem AcquireItem(int index);

		bool IsItemShowable(int allItemsIndex);

		Vector3 GetItemSize(int allItemsIndex);
	}

	private class ItemInfo
	{
		private readonly ITouchListItem item;

		public Vector3 Size { get; private set; }

		public Vector3 Offset { get; private set; }

		public int LongListIndex { get; set; }

		public Vector3 Min => item.transform.localPosition + Vector3.Scale(item.LocalBounds.min, VectorUtils.Abs(item.transform.localScale));

		public Vector3 Max => item.transform.localPosition + Vector3.Scale(item.LocalBounds.max, VectorUtils.Abs(item.transform.localScale));

		public ItemInfo(ITouchListItem item, LayoutPlane layoutPlane)
		{
			this.item = item;
			CalculateSizeAndOffset(layoutPlane);
		}

		public void CalculateSizeAndOffset(LayoutPlane layoutPlane, bool ignoreCurrentPosition = false)
		{
			Vector3 vector = Vector3.Scale(item.LocalBounds.min, VectorUtils.Abs(item.transform.localScale));
			Vector3 vector2 = Vector3.Scale(item.LocalBounds.max, VectorUtils.Abs(item.transform.localScale));
			if (!ignoreCurrentPosition)
			{
				vector -= item.transform.localPosition;
				vector2 -= item.transform.localPosition;
			}
			Size = vector2 - vector;
			Vector3 vector3 = vector;
			if (layoutPlane == LayoutPlane.XZ)
			{
				vector3.y = vector2.y;
			}
			Offset = -vector3;
			if (!ignoreCurrentPosition)
			{
				Offset += item.transform.localPosition;
			}
		}

		public bool Contains(Vector3 point, LayoutPlane layoutPlane)
		{
			Vector3 min = Min;
			Vector3 max = Max;
			int index = ((layoutPlane == LayoutPlane.XY) ? 1 : 2);
			if (point.x > min.x && point[index] > min[index] && point.x < max.x)
			{
				return point[index] < max[index];
			}
			return false;
		}
	}

	public Orientation orientation;

	public Alignment alignment = Alignment.Mid;

	public LayoutPlane layoutPlane;

	public float elementSpacing;

	public Vector2 padding = Vector2.zero;

	public int breadth = 1;

	public float itemDragFinishDistance;

	public TiledBackground background;

	public float scrollWheelIncrement = 30f;

	public Float_MobileOverride maxKineticScrollSpeed = new Float_MobileOverride();

	private GameObject content;

	private List<ITouchListItem> renderedItems = new List<ITouchListItem>();

	private Map<ITouchListItem, ItemInfo> itemInfos = new Map<ITouchListItem, ItemInfo>();

	private int layoutDimension1;

	private int layoutDimension2;

	private int layoutDimension3;

	private float contentSize;

	private float excessContentSize;

	private float m_fullListContentSize;

	private Vector2? touchBeginScreenPosition;

	private Vector3? dragBeginOffsetFromContent;

	private Vector3 dragBeginContentPosition = Vector3.zero;

	private Vector3 lastTouchPosition = Vector3.zero;

	private float lastContentPosition;

	private ITouchListItem touchBeginItem;

	private bool m_isHoveredOverTouchList;

	private PegUIElement m_hoveredOverItem;

	private ILongListBehavior longListBehavior;

	private bool allowModification = true;

	private Vector3? dragItemBegin;

	private bool layoutSuspended;

	private int? selection;

	private bool scrollEnabled = true;

	private const float ScrollDragThreshold = 0.05f;

	private const float ItemDragThreshold = 0.05f;

	private const float KineticScrollFriction = 10000f;

	private const float MinKineticScrollSpeed = 0.01f;

	private const float ScrollBoundsSpringK = 400f;

	private static readonly float ScrollBoundsSpringB = Mathf.Sqrt(1600f);

	private const float MinOutOfBoundsDistance = 0.05f;

	private static readonly Func<float, float> OutOfBoundsDistReducer = (float dist) => 30f * (Mathf.Log(dist + 30f) - Mathf.Log(30f));

	private const float CLIPSIZE_EPSILON = 0.0001f;

	public IEnumerable<ITouchListItem> RenderedItems
	{
		get
		{
			EnforceInitialized();
			return renderedItems;
		}
	}

	public bool IsReadOnly
	{
		get
		{
			EnforceInitialized();
			return false;
		}
	}

	public bool IsInitialized => content != null;

	public ILongListBehavior LongListBehavior
	{
		get
		{
			EnforceInitialized();
			return longListBehavior;
		}
		set
		{
			EnforceInitialized();
			if (value != longListBehavior)
			{
				allowModification = true;
				Clear();
				if (longListBehavior != null)
				{
					longListBehavior.ReleaseAllItems();
				}
				longListBehavior = value;
				if (longListBehavior != null)
				{
					RefreshList(0, preserveScrolling: false);
					allowModification = false;
				}
			}
		}
	}

	public float ScrollValue
	{
		get
		{
			EnforceInitialized();
			float scrollableAmount = ScrollableAmount;
			float num = ((scrollableAmount > 0f) ? Mathf.Clamp01((0f - content.transform.localPosition[layoutDimension1]) / scrollableAmount) : 0f);
			if (num == 0f || num == 1f)
			{
				return (0f - GetOutOfBoundsDist(content.transform.localPosition[layoutDimension1])) / Mathf.Max(contentSize, ClipSize[GetVector2Dimension(layoutDimension1)]) + num;
			}
			return num;
		}
		set
		{
			EnforceInitialized();
			if (!dragBeginOffsetFromContent.HasValue && !Mathf.Approximately(ScrollValue, value))
			{
				float scrollableAmount = ScrollableAmount;
				Vector3 localPosition = content.transform.localPosition;
				localPosition[layoutDimension1] = (0f - Mathf.Clamp01(value)) * scrollableAmount;
				content.transform.localPosition = localPosition;
				float num = localPosition[layoutDimension1] - lastContentPosition;
				if (num != 0f)
				{
					PreBufferLongListItems(num < 0f);
				}
				lastContentPosition = localPosition[layoutDimension1];
				FixUpScrolling();
				OnScrolled();
			}
		}
	}

	public float ScrollableAmount
	{
		get
		{
			if (longListBehavior == null)
			{
				return excessContentSize;
			}
			return Mathf.Max(0f, m_fullListContentSize - ClipSize[GetVector2Dimension(layoutDimension1)]);
		}
	}

	public bool CanScrollAhead
	{
		get
		{
			if (!scrollEnabled)
			{
				return false;
			}
			if (ScrollValue < 1f)
			{
				return true;
			}
			if (longListBehavior != null && renderedItems.Count > 0)
			{
				ITouchListItem key = renderedItems.Last();
				for (int i = itemInfos[key].LongListIndex + 1; i < longListBehavior.AllItemsCount; i++)
				{
					if (longListBehavior.IsItemShowable(i))
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	public bool CanScrollBehind
	{
		get
		{
			if (!scrollEnabled)
			{
				return false;
			}
			if (ScrollValue > 0f)
			{
				return true;
			}
			if (longListBehavior != null && renderedItems.Count > 0)
			{
				ITouchListItem key = renderedItems.First();
				ItemInfo itemInfo = itemInfos[key];
				if (longListBehavior.AllItemsCount > 0)
				{
					for (int num = itemInfo.LongListIndex - 1; num >= 0; num--)
					{
						if (longListBehavior.IsItemShowable(num))
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}

	public bool CanScroll
	{
		get
		{
			if (!CanScrollAhead)
			{
				return CanScrollBehind;
			}
			return true;
		}
	}

	public float ViewWindowMinValue
	{
		get
		{
			return (0f - content.transform.localPosition[layoutDimension1]) / contentSize;
		}
		set
		{
			Vector3 localPosition = content.transform.localPosition;
			localPosition[layoutDimension1] = (0f - Mathf.Clamp01(value)) * contentSize;
			content.transform.localPosition = localPosition;
			float num = content.transform.localPosition[layoutDimension1] - lastContentPosition;
			if (num != 0f)
			{
				PreBufferLongListItems(num < 0f);
			}
			lastContentPosition = localPosition[layoutDimension1];
			OnScrolled();
		}
	}

	public float ViewWindowMaxValue
	{
		get
		{
			float num = content.transform.localPosition[layoutDimension1];
			float num2 = ClipSize[GetVector2Dimension(layoutDimension1)];
			return (0f - num + num2) / contentSize;
		}
		set
		{
			Vector3 localPosition = content.transform.localPosition;
			localPosition[layoutDimension1] = (0f - Mathf.Clamp01(value)) * contentSize + ClipSize[GetVector2Dimension(layoutDimension1)];
			content.transform.localPosition = localPosition;
			float num = content.transform.localPosition[layoutDimension1] - lastContentPosition;
			if (num != 0f)
			{
				PreBufferLongListItems(num < 0f);
			}
			lastContentPosition = localPosition[layoutDimension1];
			OnScrolled();
		}
	}

	public Vector2 ClipSize
	{
		get
		{
			EnforceInitialized();
			BoxCollider boxCollider = GetComponent<Collider>() as BoxCollider;
			return new Vector2(boxCollider.size.x, (layoutPlane == LayoutPlane.XY) ? boxCollider.size.y : boxCollider.size.z);
		}
		set
		{
			EnforceInitialized();
			BoxCollider boxCollider = GetComponent<Collider>() as BoxCollider;
			Vector3 vector = new Vector3(value.x, 0f, 0f);
			vector[1] = ((layoutPlane == LayoutPlane.XY) ? value.y : boxCollider.size.y);
			vector[2] = ((layoutPlane == LayoutPlane.XZ) ? value.y : boxCollider.size.z);
			Vector3 vector2 = VectorUtils.Abs(boxCollider.size - vector);
			if (!(vector2.x <= 0.0001f) || !(vector2.y <= 0.0001f) || !(vector2.z <= 0.0001f))
			{
				boxCollider.size = vector;
				UpdateBackgroundBounds();
				if (longListBehavior == null)
				{
					RepositionItems(0);
				}
				else
				{
					RefreshList(0, preserveScrolling: true);
				}
				if (this.ClipSizeChanged != null)
				{
					this.ClipSizeChanged();
				}
			}
		}
	}

	public bool SelectionEnabled
	{
		get
		{
			EnforceInitialized();
			return selection.HasValue;
		}
		set
		{
			EnforceInitialized();
			if (value != SelectionEnabled)
			{
				if (value)
				{
					selection = -1;
				}
				else
				{
					selection = null;
				}
			}
		}
	}

	public int SelectedIndex
	{
		get
		{
			EnforceInitialized();
			if (!selection.HasValue)
			{
				return -1;
			}
			return selection.Value;
		}
		set
		{
			EnforceInitialized();
			if (SelectionEnabled && value != selection && (this.SelectedIndexChanging == null || this.SelectedIndexChanging(value)))
			{
				ISelectableTouchListItem selectableTouchListItem = SelectedItem as ISelectableTouchListItem;
				ISelectableTouchListItem selectableTouchListItem2 = ((value != -1) ? renderedItems[value] : null) as ISelectableTouchListItem;
				if (value == -1 || (selectableTouchListItem2 != null && selectableTouchListItem2.Selectable))
				{
					selection = value;
				}
				if (selectableTouchListItem != null && selection == value)
				{
					selectableTouchListItem.Unselected();
				}
				if (selection == value && selectableTouchListItem2 != null)
				{
					selectableTouchListItem2.Selected();
					ScrollToItem_Internal(selectableTouchListItem2);
				}
			}
		}
	}

	public ITouchListItem SelectedItem
	{
		get
		{
			EnforceInitialized();
			if (!selection.HasValue || selection.Value == -1)
			{
				return null;
			}
			return renderedItems[selection.Value];
		}
		set
		{
			EnforceInitialized();
			int num = renderedItems.IndexOf(value);
			if (num != -1)
			{
				SelectedIndex = num;
			}
		}
	}

	public bool IsLayoutSuspended => layoutSuspended;

	public event Action Scrolled;

	public event SelectedIndexChangingEvent SelectedIndexChanging;

	public event ScrollingEnabledChangedEvent ScrollingEnabledChanged;

	public event Action ClipSizeChanged;

	public event ItemDragEvent ItemDragStarted;

	public event ItemDragEvent ItemDragged;

	public event ItemDragEvent ItemDragFinished;

	private void FixUpScrolling()
	{
		if (longListBehavior == null || renderedItems.Count <= 0 || !CanScroll)
		{
			return;
		}
		Bounds bounds = CalculateLocalClipBounds();
		ITouchListItem key = renderedItems[0];
		ItemInfo itemInfo = itemInfos[key];
		if (itemInfo.LongListIndex == 0 && !CanScrollBehind)
		{
			float num = bounds.min[layoutDimension1];
			Vector3 min = itemInfo.Min;
			if (Mathf.Abs(min[layoutDimension1] - num) > 0.0001f)
			{
				Vector3 zero = Vector3.zero;
				zero[layoutDimension1] = num - min[layoutDimension1];
				for (int i = 0; i < renderedItems.Count; i++)
				{
					key = renderedItems[i];
					key.gameObject.transform.Translate(zero);
				}
			}
		}
		else
		{
			if (renderedItems.Count <= 1 || CanScrollAhead)
			{
				return;
			}
			float num2 = bounds.max[layoutDimension1];
			key = renderedItems[renderedItems.Count - 1];
			itemInfo = itemInfos[key];
			if (itemInfo.LongListIndex < longListBehavior.AllItemsCount - 1)
			{
				return;
			}
			Vector3 max = itemInfo.Max;
			if (Mathf.Abs(max[layoutDimension1] - num2) > 0.0001f)
			{
				Vector3 zero2 = Vector3.zero;
				zero2[layoutDimension1] = num2 - max[layoutDimension1];
				for (int j = 0; j < renderedItems.Count; j++)
				{
					key = renderedItems[j];
					key.gameObject.transform.Translate(zero2);
				}
			}
		}
	}

	public void Add(ITouchListItem item)
	{
		Add(item, repositionItems: true);
	}

	public void Add(ITouchListItem item, bool repositionItems)
	{
		EnforceInitialized();
		if (allowModification)
		{
			renderedItems.Add(item);
			Vector3 negatedScale = GetNegatedScale(item.transform.localScale);
			item.transform.parent = content.transform;
			item.transform.localPosition = Vector3.zero;
			item.transform.localRotation = Quaternion.identity;
			if (orientation == Orientation.Vertical)
			{
				item.transform.localScale = negatedScale;
			}
			itemInfos[item] = new ItemInfo(item, layoutPlane);
			item.gameObject.SetActive(value: false);
			if (selection == -1 && item is ISelectableTouchListItem && ((ISelectableTouchListItem)item).IsSelected())
			{
				selection = renderedItems.Count - 1;
			}
			if (repositionItems)
			{
				RepositionItems(renderedItems.Count - 1);
				RecalculateLongListContentSize();
			}
		}
	}

	public void Clear()
	{
		EnforceInitialized();
		if (!allowModification)
		{
			return;
		}
		foreach (ITouchListItem renderedItem in renderedItems)
		{
			Vector3 negatedScale = GetNegatedScale(renderedItem.transform.localScale);
			renderedItem.transform.parent = null;
			if (orientation == Orientation.Vertical)
			{
				renderedItem.transform.localScale = negatedScale;
			}
		}
		content.transform.localPosition = Vector3.zero;
		lastContentPosition = 0f;
		renderedItems.Clear();
		RecalculateSize();
		UpdateBackgroundScroll();
		RecalculateLongListContentSize();
		if (SelectionEnabled)
		{
			SelectedIndex = -1;
		}
		if (m_hoveredOverItem != null)
		{
			m_hoveredOverItem.TriggerOut();
			m_hoveredOverItem = null;
		}
	}

	public bool Contains(ITouchListItem item)
	{
		EnforceInitialized();
		return renderedItems.Contains(item);
	}

	public void CopyTo(ITouchListItem[] array, int arrayIndex)
	{
		EnforceInitialized();
		renderedItems.CopyTo(array, arrayIndex);
	}

	private List<ITouchListItem> GetItemsInView()
	{
		EnforceInitialized();
		List<ITouchListItem> list = new List<ITouchListItem>();
		float num = CalculateLocalClipBounds().max[layoutDimension1];
		for (int i = GetNumItemsBehindView(); i < renderedItems.Count && !((itemInfos[renderedItems[i]].Min - content.transform.localPosition)[layoutDimension1] >= num); i++)
		{
			list.Add(renderedItems[i]);
		}
		return list;
	}

	public void SetVisibilityOfAllItems()
	{
		if (layoutSuspended)
		{
			return;
		}
		EnforceInitialized();
		Bounds localClipBounds = CalculateLocalClipBounds();
		for (int i = 0; i < renderedItems.Count; i++)
		{
			ITouchListItem touchListItem = renderedItems[i];
			bool flag = IsItemVisible_Internal(i, ref localClipBounds);
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

	private bool IsItemVisible_Internal(int visualizedListIndex, ref Bounds localClipBounds)
	{
		ITouchListItem key = renderedItems[visualizedListIndex];
		ItemInfo itemInfo = itemInfos[key];
		Vector3 min = itemInfo.Min;
		Vector3 max = itemInfo.Max;
		if (!IsWithinClipBounds(min, max, ref localClipBounds))
		{
			return false;
		}
		return true;
	}

	private bool IsWithinClipBounds(Vector3 localBoundsMin, Vector3 localBoundsMax, ref Bounds localClipBounds)
	{
		float num = localClipBounds.min[layoutDimension1];
		float num2 = localClipBounds.max[layoutDimension1];
		if (localBoundsMax[layoutDimension1] < num)
		{
			return false;
		}
		if (localBoundsMin[layoutDimension1] > num2)
		{
			return false;
		}
		return true;
	}

	private bool IsItemVisible(int visualizedListIndex)
	{
		Bounds localClipBounds = CalculateLocalClipBounds();
		return IsItemVisible_Internal(visualizedListIndex, ref localClipBounds);
	}

	public int IndexOf(ITouchListItem item)
	{
		EnforceInitialized();
		return renderedItems.IndexOf(item);
	}

	public void Insert(int index, ITouchListItem item)
	{
		Insert(index, item, repositionItems: true);
	}

	public void Insert(int index, ITouchListItem item, bool repositionItems)
	{
		EnforceInitialized();
		if (allowModification)
		{
			renderedItems.Insert(index, item);
			Vector3 negatedScale = GetNegatedScale(item.transform.localScale);
			item.transform.parent = content.transform;
			item.transform.localPosition = Vector3.zero;
			item.transform.localRotation = Quaternion.identity;
			if (orientation == Orientation.Vertical)
			{
				item.transform.localScale = negatedScale;
			}
			itemInfos[item] = new ItemInfo(item, layoutPlane);
			if (selection == -1 && item is ISelectableTouchListItem && ((ISelectableTouchListItem)item).IsSelected())
			{
				selection = index;
			}
			if (repositionItems)
			{
				RepositionItems(index);
				RecalculateLongListContentSize();
			}
		}
	}

	public bool Remove(ITouchListItem item)
	{
		EnforceInitialized();
		if (!allowModification)
		{
			return false;
		}
		int num = renderedItems.IndexOf(item);
		if (num != -1)
		{
			RemoveAt(num, repositionItems: true);
			return true;
		}
		return false;
	}

	public void RemoveAt(int index)
	{
		RemoveAt(index, repositionItems: true);
	}

	public void RemoveAt(int index, bool repositionItems)
	{
		EnforceInitialized();
		if (allowModification)
		{
			Vector3 negatedScale = GetNegatedScale(renderedItems[index].transform.localScale);
			ITouchListItem touchListItem = renderedItems[index];
			touchListItem.transform.parent = base.transform;
			if (orientation == Orientation.Vertical)
			{
				renderedItems[index].transform.localScale = negatedScale;
			}
			itemInfos.Remove(renderedItems[index]);
			renderedItems.RemoveAt(index);
			if (index == selection)
			{
				selection = -1;
			}
			else if (index < selection)
			{
				selection--;
			}
			if (m_hoveredOverItem != null && touchListItem.GetComponent<PegUIElement>() == m_hoveredOverItem)
			{
				m_hoveredOverItem.TriggerOut();
				m_hoveredOverItem = null;
			}
			if (repositionItems)
			{
				RepositionItems(index);
				RecalculateLongListContentSize();
			}
		}
	}

	public int FindIndex(Predicate<ITouchListItem> match)
	{
		EnforceInitialized();
		return renderedItems.FindIndex(match);
	}

	public void Sort(Comparison<ITouchListItem> comparison)
	{
		EnforceInitialized();
		ITouchListItem selectedItem = SelectedItem;
		renderedItems.Sort(comparison);
		RepositionItems(0);
		selection = renderedItems.IndexOf(selectedItem);
	}

	public void SuspendLayout()
	{
		EnforceInitialized();
		layoutSuspended = true;
	}

	public void ResumeLayout(bool repositionItems = true)
	{
		EnforceInitialized();
		layoutSuspended = false;
		if (repositionItems)
		{
			RepositionItems(0);
		}
	}

	public void ResetState()
	{
		touchBeginScreenPosition = null;
		dragBeginOffsetFromContent = null;
		dragBeginContentPosition = Vector3.zero;
		lastTouchPosition = Vector3.zero;
		lastContentPosition = 0f;
		dragItemBegin = null;
		if (content != null)
		{
			content.transform.localPosition = Vector3.zero;
		}
	}

	public void SetScrollingEnabled(bool enable)
	{
		scrollEnabled = enable;
		OnScrollingEnabledChanged();
	}

	public void ScrollToItem(ITouchListItem item)
	{
		ScrollToItem_Internal(item);
	}

	protected override void Awake()
	{
		base.Awake();
		content = new GameObject("Content");
		content.transform.parent = base.transform;
		TransformUtil.Identity(content.transform);
		layoutDimension1 = 0;
		layoutDimension2 = ((layoutPlane == LayoutPlane.XY) ? 1 : 2);
		layoutDimension3 = 3 - layoutDimension2;
		if (orientation == Orientation.Vertical)
		{
			GeneralUtils.Swap(ref layoutDimension1, ref layoutDimension2);
			Vector3 one = Vector3.one;
			one[layoutDimension1] = -1f;
			base.transform.localScale = one;
		}
		if (background != null)
		{
			if (orientation == Orientation.Vertical)
			{
				background.transform.localScale = GetNegatedScale(background.transform.localScale);
			}
			UpdateBackgroundBounds();
		}
	}

	protected override void OnOver(InteractionState oldState)
	{
		m_isHoveredOverTouchList = true;
		OnHover(isKnownOver: true);
	}

	protected override void OnOut(InteractionState oldState)
	{
		m_isHoveredOverTouchList = false;
		if (m_hoveredOverItem != null)
		{
			m_hoveredOverItem.TriggerOut();
			m_hoveredOverItem = null;
		}
	}

	private void OnHover(bool isKnownOver)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			return;
		}
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (camera == null)
		{
			if (m_hoveredOverItem != null)
			{
				m_hoveredOverItem.TriggerOut();
				m_hoveredOverItem = null;
			}
			return;
		}
		if (!isKnownOver && (!UniversalInputManager.Get().GetInputHitInfo(camera, out var hitInfo) || hitInfo.transform != base.transform) && m_hoveredOverItem != null)
		{
			m_hoveredOverItem.TriggerOut();
			m_hoveredOverItem = null;
		}
		GetComponent<Collider>().enabled = false;
		PegUIElement pegUIElement = null;
		if (UniversalInputManager.Get().GetInputHitInfo(camera, out hitInfo))
		{
			pegUIElement = hitInfo.transform.GetComponent<PegUIElement>();
		}
		GetComponent<Collider>().enabled = true;
		if (pegUIElement != null && m_hoveredOverItem != pegUIElement)
		{
			if (m_hoveredOverItem != null)
			{
				m_hoveredOverItem.TriggerOut();
			}
			pegUIElement.TriggerOver();
			m_hoveredOverItem = pegUIElement;
		}
	}

	protected override void OnPress()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (camera == null)
		{
			return;
		}
		touchBeginScreenPosition = UniversalInputManager.Get().GetMousePosition();
		if (lastContentPosition != content.transform.localPosition[layoutDimension1])
		{
			return;
		}
		Vector3 point = GetTouchPosition() - content.transform.localPosition;
		for (int i = 0; i < renderedItems.Count; i++)
		{
			ITouchListItem touchListItem = renderedItems[i];
			if ((touchListItem.IsHeader || touchListItem.Visible) && itemInfos[touchListItem].Contains(point, layoutPlane))
			{
				touchBeginItem = touchListItem;
				break;
			}
		}
		GetComponent<Collider>().enabled = false;
		PegUIElement pegUIElement = null;
		if (UniversalInputManager.Get().GetInputHitInfo(camera, out var hitInfo))
		{
			pegUIElement = hitInfo.transform.GetComponent<PegUIElement>();
		}
		GetComponent<Collider>().enabled = true;
		if (pegUIElement != null)
		{
			pegUIElement.TriggerPress();
		}
	}

	protected override void OnRelease()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (!(camera == null) && touchBeginItem != null && !dragItemBegin.HasValue)
		{
			touchBeginScreenPosition = null;
			GetComponent<Collider>().enabled = false;
			PegUIElement pegUIElement = null;
			if (UniversalInputManager.Get().GetInputHitInfo(camera, out var hitInfo))
			{
				pegUIElement = hitInfo.transform.GetComponent<PegUIElement>();
			}
			GetComponent<Collider>().enabled = true;
			if (pegUIElement != null)
			{
				pegUIElement.TriggerRelease();
				touchBeginItem = null;
			}
		}
	}

	private void EnforceInitialized()
	{
		if (!IsInitialized)
		{
			throw new InvalidOperationException("TouchList must be initialized before using it. Please wait for Awake to finish.");
		}
	}

	private void Update()
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			UpdateTouchInput();
		}
		else
		{
			UpdateMouseInput();
		}
		if (m_isHoveredOverTouchList)
		{
			OnHover(isKnownOver: false);
		}
	}

	private void UpdateTouchInput()
	{
		Vector3 touchPosition = GetTouchPosition();
		if (UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			if (dragItemBegin.HasValue && this.ItemDragFinished != null)
			{
				this.ItemDragFinished(touchBeginItem, GetItemDragDelta(touchPosition));
				dragItemBegin = null;
			}
			touchBeginItem = null;
			touchBeginScreenPosition = null;
			dragBeginOffsetFromContent = null;
		}
		if (touchBeginScreenPosition.HasValue)
		{
			Func<int, float, bool> func = delegate(int dimension, float inchThreshold)
			{
				int vector2Dimension = GetVector2Dimension(dimension);
				float f = touchBeginScreenPosition.Value[vector2Dimension] - UniversalInputManager.Get().GetMousePosition()[vector2Dimension];
				float num8 = inchThreshold * ((Screen.dpi > 0f) ? Screen.dpi : 150f);
				return Mathf.Abs(f) > num8;
			};
			if (this.ItemDragStarted != null && func(layoutDimension2, 0.05f) && this.ItemDragStarted(touchBeginItem, GetItemDragDelta(touchPosition)))
			{
				dragItemBegin = GetTouchPosition();
				touchBeginScreenPosition = null;
			}
			else if (func(layoutDimension1, 0.05f))
			{
				dragBeginContentPosition = content.transform.localPosition;
				dragBeginOffsetFromContent = dragBeginContentPosition - lastTouchPosition;
				touchBeginItem = null;
				touchBeginScreenPosition = null;
			}
		}
		float num2;
		if (dragItemBegin.HasValue)
		{
			if (!this.ItemDragged(touchBeginItem, GetItemDragDelta(touchPosition)))
			{
				dragItemBegin = null;
				touchBeginItem = null;
			}
		}
		else if (dragBeginOffsetFromContent.HasValue)
		{
			float num = touchPosition[layoutDimension1] + dragBeginOffsetFromContent.Value[layoutDimension1];
			float outOfBoundsDist = GetOutOfBoundsDist(num);
			if (outOfBoundsDist != 0f)
			{
				outOfBoundsDist = OutOfBoundsDistReducer(Mathf.Abs(outOfBoundsDist)) * Mathf.Sign(outOfBoundsDist);
				num = ((outOfBoundsDist < 0f) ? (0f - excessContentSize + outOfBoundsDist) : outOfBoundsDist);
			}
			Vector3 localPosition = content.transform.localPosition;
			lastContentPosition = localPosition[layoutDimension1];
			localPosition[layoutDimension1] = num;
			content.transform.localPosition = localPosition;
			if (lastContentPosition != localPosition[layoutDimension1])
			{
				OnScrolled();
			}
		}
		else
		{
			float contentPosition = content.transform.localPosition[layoutDimension1];
			float outOfBoundsDist2 = GetOutOfBoundsDist(contentPosition);
			num2 = content.transform.localPosition[layoutDimension1] - lastContentPosition;
			float num3 = num2 / Time.fixedDeltaTime;
			if ((float)maxKineticScrollSpeed > Mathf.Epsilon)
			{
				num3 = ((!(num3 > 0f)) ? Mathf.Max(num3, 0f - (float)maxKineticScrollSpeed) : Mathf.Min(num3, maxKineticScrollSpeed));
			}
			if (outOfBoundsDist2 != 0f)
			{
				Vector3 localPosition2 = content.transform.localPosition;
				lastContentPosition = contentPosition;
				float num4 = -400f * outOfBoundsDist2 - ScrollBoundsSpringB * num3;
				float num5 = num3 + num4 * Time.fixedDeltaTime;
				localPosition2[layoutDimension1] += num5 * Time.fixedDeltaTime;
				if (Mathf.Abs(GetOutOfBoundsDist(localPosition2[layoutDimension1])) < 0.05f)
				{
					float value = ((Mathf.Abs(localPosition2[layoutDimension1] + excessContentSize) < Mathf.Abs(localPosition2[layoutDimension1])) ? (0f - excessContentSize) : 0f);
					localPosition2[layoutDimension1] = value;
					lastContentPosition = value;
				}
				content.transform.localPosition = localPosition2;
				OnScrolled();
			}
			else if (num3 != 0f)
			{
				lastContentPosition = content.transform.localPosition[layoutDimension1];
				float num6 = (0f - Mathf.Sign(num3)) * 10000f;
				float num7 = num3 + num6 * Time.fixedDeltaTime;
				if (Mathf.Abs(num7) >= 0.01f && Mathf.Sign(num7) == Mathf.Sign(num3))
				{
					Vector3 localPosition3 = content.transform.localPosition;
					localPosition3[layoutDimension1] += num7 * Time.fixedDeltaTime;
					content.transform.localPosition = localPosition3;
					OnScrolled();
				}
			}
			else
			{
				FixUpScrolling();
			}
		}
		num2 = content.transform.localPosition[layoutDimension1] - lastContentPosition;
		if (num2 != 0f)
		{
			PreBufferLongListItems(num2 < 0f);
		}
		lastTouchPosition = touchPosition;
	}

	private void PreBufferLongListItems(bool scrolledAhead)
	{
		if (LongListBehavior == null)
		{
			return;
		}
		allowModification = true;
		if (scrolledAhead && GetNumItemsAheadOfView() < longListBehavior.MinBuffer)
		{
			bool flag = CanScrollAhead;
			if (renderedItems.Count > 0)
			{
				Bounds bounds = CalculateLocalClipBounds();
				ITouchListItem key = renderedItems[renderedItems.Count - 1];
				Vector3 max = itemInfos[key].Max;
				float num = bounds.min[layoutDimension1];
				if (max[layoutDimension1] < num)
				{
					RefreshList(0, preserveScrolling: true);
					flag = false;
				}
			}
			if (flag)
			{
				LoadAhead();
			}
		}
		else if (!scrolledAhead && GetNumItemsBehindView() < longListBehavior.MinBuffer)
		{
			bool flag2 = CanScrollBehind;
			if (renderedItems.Count > 0)
			{
				Bounds bounds2 = CalculateLocalClipBounds();
				ITouchListItem key2 = renderedItems[0];
				Vector3 min = itemInfos[key2].Min;
				float num2 = bounds2.max[layoutDimension1];
				if (min[layoutDimension1] > num2)
				{
					RefreshList(0, preserveScrolling: true);
					flag2 = false;
				}
			}
			if (flag2)
			{
				LoadBehind();
			}
		}
		allowModification = false;
	}

	private void UpdateMouseInput()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (camera == null)
		{
			return;
		}
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		if (!GetComponent<Collider>().Raycast(ray, out var _, camera.farClipPlane))
		{
			return;
		}
		float num = 0f;
		if (Input.GetAxis("Mouse ScrollWheel") < 0f && CanScrollAhead)
		{
			num -= scrollWheelIncrement;
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0f && CanScrollBehind)
		{
			num += scrollWheelIncrement;
		}
		if (Mathf.Abs(num) > Mathf.Epsilon)
		{
			float num2 = content.transform.localPosition[layoutDimension1] + num;
			if (num2 <= 0f - excessContentSize)
			{
				num2 = 0f - excessContentSize;
			}
			else if (num2 >= 0f)
			{
				num2 = 0f;
			}
			Vector3 localPosition = content.transform.localPosition;
			lastContentPosition = localPosition[layoutDimension1];
			localPosition[layoutDimension1] = num2;
			content.transform.localPosition = localPosition;
			float num3 = content.transform.localPosition[layoutDimension1] - lastContentPosition;
			lastContentPosition = content.transform.localPosition[layoutDimension1];
			if (num3 != 0f)
			{
				PreBufferLongListItems(num3 < 0f);
			}
			FixUpScrolling();
			OnScrolled();
		}
	}

	private float GetOutOfBoundsDist(float contentPosition)
	{
		float result = 0f;
		if (contentPosition < 0f - excessContentSize)
		{
			result = contentPosition + excessContentSize;
		}
		else if (contentPosition > 0f)
		{
			result = contentPosition;
		}
		return result;
	}

	private void ScrollToItem_Internal(ITouchListItem item)
	{
		Bounds bounds = CalculateLocalClipBounds();
		ItemInfo itemInfo = itemInfos[item];
		float num = itemInfo.Max[layoutDimension1] - bounds.max[layoutDimension1];
		if (num > 0f)
		{
			Vector3 zero = Vector3.zero;
			zero[layoutDimension1] = num;
			content.transform.Translate(zero);
			lastContentPosition = content.transform.localPosition[layoutDimension1];
			PreBufferLongListItems(scrolledAhead: true);
			OnScrolled();
		}
		float num2 = bounds.min[layoutDimension1] - itemInfo.Min[layoutDimension1];
		if (num2 > 0f)
		{
			Vector3 zero2 = Vector3.zero;
			zero2[layoutDimension1] = 0f - num2;
			content.transform.Translate(zero2);
			lastContentPosition = content.transform.localPosition[layoutDimension1];
			PreBufferLongListItems(scrolledAhead: false);
			OnScrolled();
		}
	}

	private void OnScrolled()
	{
		UpdateBackgroundScroll();
		SetVisibilityOfAllItems();
		if (this.Scrolled != null)
		{
			this.Scrolled();
		}
	}

	private Vector3 GetTouchPosition()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (camera == null)
		{
			return Vector3.zero;
		}
		float num = Vector3.Distance(camera.transform.position, GetComponent<Collider>().bounds.min);
		float num2 = Vector3.Distance(camera.transform.position, GetComponent<Collider>().bounds.max);
		Vector3 inPoint = ((num < num2) ? GetComponent<Collider>().bounds.min : GetComponent<Collider>().bounds.max);
		Plane plane = new Plane(-camera.transform.forward, inPoint);
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		plane.Raycast(ray, out var enter);
		return base.transform.InverseTransformPoint(ray.GetPoint(enter));
	}

	private float GetItemDragDelta(Vector3 touchPosition)
	{
		if (dragItemBegin.HasValue)
		{
			return touchPosition[layoutDimension2] - dragItemBegin.Value[layoutDimension2];
		}
		return 0f;
	}

	private void LoadAhead()
	{
		bool flag = allowModification;
		bool flag2 = layoutSuspended;
		allowModification = true;
		int num = -1;
		int num2 = 0;
		int numItemsBehindView = GetNumItemsBehindView();
		for (int i = 0; i < numItemsBehindView - longListBehavior.MinBuffer; i++)
		{
			ITouchListItem item = renderedItems[0];
			RemoveAt(0, repositionItems: false);
			longListBehavior.ReleaseItem(item);
		}
		float num3 = CalculateLocalClipBounds().max[layoutDimension1];
		int num4 = 0;
		for (int j = ((renderedItems.Count != 0) ? (itemInfos[renderedItems.Last()].LongListIndex + 1) : 0); j < longListBehavior.AllItemsCount; j++)
		{
			if (renderedItems.Count >= longListBehavior.MaxAcquiredItems)
			{
				break;
			}
			if (num4 >= longListBehavior.MinBuffer)
			{
				break;
			}
			if (longListBehavior.IsItemShowable(j))
			{
				if (num < 0)
				{
					num = renderedItems.Count;
				}
				ITouchListItem touchListItem = longListBehavior.AcquireItem(j);
				Add(touchListItem, repositionItems: false);
				ItemInfo itemInfo = itemInfos[touchListItem];
				itemInfo.LongListIndex = j;
				num2++;
				if (itemInfo.Min[layoutDimension1] > num3)
				{
					num4++;
				}
			}
		}
		if (num >= 0)
		{
			layoutSuspended = false;
			RepositionItems(num);
		}
		allowModification = flag;
		if (flag2 != layoutSuspended)
		{
			layoutSuspended = flag2;
		}
	}

	private void LoadBehind()
	{
		bool flag = allowModification;
		allowModification = true;
		int num = 0;
		int numItemsAheadOfView = GetNumItemsAheadOfView();
		for (int i = 0; i < numItemsAheadOfView - longListBehavior.MinBuffer; i++)
		{
			ITouchListItem item = renderedItems[renderedItems.Count - 1];
			RemoveAt(renderedItems.Count - 1, repositionItems: false);
			longListBehavior.ReleaseItem(item);
		}
		float num2 = CalculateLocalClipBounds().min[layoutDimension1];
		int num3 = 0;
		int num4 = ((renderedItems.Count == 0) ? (longListBehavior.AllItemsCount - 1) : (itemInfos[renderedItems.First()].LongListIndex - 1));
		while (num4 >= 0 && renderedItems.Count < longListBehavior.MaxAcquiredItems && num3 < longListBehavior.MinBuffer)
		{
			if (longListBehavior.IsItemShowable(num4))
			{
				ITouchListItem touchListItem = longListBehavior.AcquireItem(num4);
				InsertAndPositionBehind(touchListItem, num4);
				ItemInfo itemInfo = itemInfos[touchListItem];
				itemInfo.LongListIndex = num4;
				num++;
				if (itemInfo.Max[layoutDimension1] < num2)
				{
					num3++;
				}
			}
			num4--;
		}
		allowModification = flag;
	}

	private int GetNumItemsBehindView()
	{
		float num = CalculateLocalClipBounds().min[layoutDimension1];
		for (int i = 0; i < renderedItems.Count; i++)
		{
			ITouchListItem key = renderedItems[i];
			if (itemInfos[key].Max[layoutDimension1] > num)
			{
				return i;
			}
		}
		return renderedItems.Count;
	}

	private int GetNumItemsAheadOfView()
	{
		float num = CalculateLocalClipBounds().max[layoutDimension1];
		for (int num2 = renderedItems.Count - 1; num2 >= 0; num2--)
		{
			ITouchListItem key = renderedItems[num2];
			if (itemInfos[key].Min[layoutDimension1] < num)
			{
				return renderedItems.Count - 1 - num2;
			}
		}
		return renderedItems.Count;
	}

	public void RefreshList(int startingLongListIndex, bool preserveScrolling)
	{
		if (longListBehavior == null)
		{
			return;
		}
		bool flag = allowModification;
		allowModification = true;
		int num = ((SelectedItem == null) ? (-1) : itemInfos[SelectedItem].LongListIndex);
		int num2 = -2;
		int num3 = -1;
		if (startingLongListIndex > 0)
		{
			for (int i = 0; i < renderedItems.Count; i++)
			{
				ITouchListItem key = renderedItems[i];
				if (itemInfos[key].LongListIndex < startingLongListIndex)
				{
					num2 = i;
					continue;
				}
				num3 = i;
				break;
			}
		}
		else
		{
			num3 = 0;
		}
		int num4 = ((num3 == -1) ? (num2 + 1) : num3);
		Bounds bounds = GetComponent<Collider>().bounds;
		Vector3? initialItemPosition = null;
		Vector3 vector = Vector3.zero;
		int num5 = ((orientation != Orientation.Vertical) ? 1 : (-1));
		if (preserveScrolling)
		{
			vector = content.transform.position;
			vector[layoutDimension1] -= (float)num5 * bounds.extents[layoutDimension1];
			vector[layoutDimension1] += (float)num5 * padding[GetVector2Dimension(layoutDimension1)];
			vector[layoutDimension2] = bounds.center[layoutDimension2];
			vector[layoutDimension3] = bounds.center[layoutDimension3];
			Vector3 localPosition = content.transform.localPosition;
			content.transform.localPosition = Vector3.zero;
			Bounds bounds2 = CalculateLocalClipBounds();
			Vector3 min = bounds2.min;
			min[layoutDimension1] = 0f - localPosition[layoutDimension1] + bounds2.min[layoutDimension1];
			content.transform.localPosition = localPosition;
			initialItemPosition = min;
			if (num2 >= 0)
			{
				ITouchListItem touchListItem = renderedItems[num2];
				ItemInfo itemInfo = itemInfos[touchListItem];
				vector = touchListItem.transform.position - itemInfo.Offset;
				vector[layoutDimension1] += (float)num5 * elementSpacing;
				ITouchListItem touchListItem2 = renderedItems[0];
				ItemInfo itemInfo2 = itemInfos[touchListItem2];
				initialItemPosition = touchListItem2.transform.localPosition - itemInfo2.Offset;
			}
		}
		int num6 = 0;
		if (num4 >= 0)
		{
			for (int num7 = renderedItems.Count - 1; num7 >= num4; num7--)
			{
				num6++;
				ITouchListItem item = renderedItems[num7];
				RemoveAt(num7, repositionItems: false);
				longListBehavior.ReleaseItem(item);
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
		int num8 = 0;
		for (int j = startingLongListIndex; j < longListBehavior.AllItemsCount; j++)
		{
			if (renderedItems.Count >= longListBehavior.MaxAcquiredItems)
			{
				break;
			}
			if (!longListBehavior.IsItemShowable(j))
			{
				continue;
			}
			bool flag2 = true;
			if (preserveScrolling)
			{
				flag2 = false;
				Vector3 itemSize = longListBehavior.GetItemSize(j);
				Vector3 vector2 = vector;
				vector2[layoutDimension1] += (float)num5 * itemSize[layoutDimension1];
				if (bounds.Contains(vector) || bounds.Contains(vector2))
				{
					flag2 = true;
				}
				vector = vector2;
				vector[layoutDimension1] += (float)num5 * elementSpacing;
			}
			if (flag2)
			{
				num8++;
				ITouchListItem touchListItem3 = longListBehavior.AcquireItem(j);
				Add(touchListItem3, repositionItems: false);
				itemInfos[touchListItem3].LongListIndex = j;
			}
		}
		RepositionItems(num3, initialItemPosition);
		if (num3 == 0)
		{
			LoadBehind();
		}
		if (num4 >= 0)
		{
			LoadAhead();
		}
		bool flag3 = false;
		float outOfBoundsDist = GetOutOfBoundsDist(content.transform.localPosition[layoutDimension1]);
		if (outOfBoundsDist != 0f && excessContentSize > 0f)
		{
			Vector3 localPosition2 = content.transform.localPosition;
			localPosition2[layoutDimension1] -= outOfBoundsDist;
			float num9 = localPosition2[layoutDimension1] - content.transform.localPosition[layoutDimension1];
			content.transform.localPosition = localPosition2;
			lastContentPosition = content.transform.localPosition[layoutDimension1];
			if (num9 < 0f)
			{
				LoadAhead();
			}
			else
			{
				LoadBehind();
			}
			flag3 = true;
		}
		if (num >= 0 && renderedItems.Count > 0 && num >= itemInfos[renderedItems.First()].LongListIndex && num <= itemInfos[renderedItems.Last()].LongListIndex)
		{
			for (int k = 0; k < renderedItems.Count; k++)
			{
				ISelectableTouchListItem selectableTouchListItem = renderedItems[k] as ISelectableTouchListItem;
				if (selectableTouchListItem != null && itemInfos[selectableTouchListItem].LongListIndex == num)
				{
					selection = k;
					selectableTouchListItem.Selected();
					break;
				}
			}
		}
		flag3 = RecalculateLongListContentSize(fireOnScroll: false) || flag3;
		allowModification = flag;
		if (flag3)
		{
			OnScrolled();
			OnScrollingEnabledChanged();
		}
	}

	private void OnScrollingEnabledChanged()
	{
		if (this.ScrollingEnabledChanged != null)
		{
			if (longListBehavior == null)
			{
				this.ScrollingEnabledChanged(excessContentSize > 0f && scrollEnabled);
			}
			else
			{
				this.ScrollingEnabledChanged(m_fullListContentSize > ClipSize[GetVector2Dimension(layoutDimension1)] && scrollEnabled);
			}
		}
	}

	public void RecalculateItemSizeAndOffsets(bool ignoreCurrentPosition)
	{
		for (int i = 0; i < renderedItems.Count; i++)
		{
			itemInfos[renderedItems[i]].CalculateSizeAndOffset(layoutPlane, ignoreCurrentPosition);
		}
		RepositionItems(0);
	}

	private void RepositionItems(int startingIndex, Vector3? initialItemPosition = null)
	{
		if (layoutSuspended)
		{
			return;
		}
		if (orientation == Orientation.Vertical)
		{
			base.transform.localScale = Vector3.one;
		}
		Vector3 localPosition = content.transform.localPosition;
		content.transform.localPosition = Vector3.zero;
		Vector3 vector = CalculateLocalClipBounds().min;
		if (initialItemPosition.HasValue)
		{
			vector = initialItemPosition.Value;
		}
		vector[layoutDimension1] += padding[GetVector2Dimension(layoutDimension1)];
		vector[layoutDimension3] = 0f;
		content.transform.localPosition = localPosition;
		ValidateBreadth();
		startingIndex -= startingIndex % breadth;
		if (startingIndex > 0)
		{
			int num = startingIndex - breadth;
			float num2 = float.MinValue;
			for (int i = num; i < startingIndex && i < renderedItems.Count; i++)
			{
				ITouchListItem key = renderedItems[i];
				num2 = Mathf.Max(itemInfos[key].Max[layoutDimension1], num2);
			}
			vector[layoutDimension1] = num2 + elementSpacing;
		}
		Vector3 zero = Vector3.zero;
		zero[layoutDimension1] = 1f;
		for (int j = startingIndex; j < renderedItems.Count; j++)
		{
			ITouchListItem touchListItem = renderedItems[j];
			if (!touchListItem.IsHeader && !touchListItem.Visible)
			{
				renderedItems[j].Visible = false;
				renderedItems[j].gameObject.SetActive(value: false);
				continue;
			}
			ItemInfo itemInfo = itemInfos[renderedItems[j]];
			Vector3 localPosition2 = vector + itemInfo.Offset;
			localPosition2[layoutDimension2] = GetBreadthPosition(j) + itemInfo.Offset[layoutDimension2];
			renderedItems[j].transform.localPosition = localPosition2;
			if ((j + 1) % breadth == 0)
			{
				vector = (itemInfo.Max[layoutDimension1] + elementSpacing) * zero;
			}
		}
		RecalculateSize();
		UpdateBackgroundScroll();
		if (orientation == Orientation.Vertical)
		{
			base.transform.localScale = GetNegatedScale(Vector3.one);
		}
		SetVisibilityOfAllItems();
	}

	private void InsertAndPositionBehind(ITouchListItem item, int longListIndex)
	{
		if (renderedItems.Count == 0)
		{
			Add(item, repositionItems: true);
			return;
		}
		ITouchListItem touchListItem = renderedItems.FirstOrDefault();
		if (touchListItem == null)
		{
			Insert(0, item, repositionItems: true);
			return;
		}
		if (orientation == Orientation.Vertical)
		{
			base.transform.localScale = Vector3.one;
		}
		ItemInfo itemInfo = itemInfos[touchListItem];
		Vector3 vector = touchListItem.transform.localPosition - itemInfo.Offset;
		Insert(0, item, repositionItems: false);
		itemInfos[item].LongListIndex = longListIndex;
		ItemInfo itemInfo2 = itemInfos[item];
		Vector3 localPosition = vector;
		float num = itemInfo2.Size[layoutDimension1] + elementSpacing;
		localPosition[layoutDimension1] -= num;
		localPosition += itemInfo2.Offset;
		item.transform.localPosition = localPosition;
		if (selection == -1 && item is ISelectableTouchListItem && ((ISelectableTouchListItem)item).IsSelected())
		{
			selection = 0;
		}
		RecalculateSize();
		UpdateBackgroundScroll();
		if (orientation == Orientation.Vertical)
		{
			base.transform.localScale = GetNegatedScale(Vector3.one);
		}
		bool active = IsItemVisible(0);
		item.gameObject.SetActive(active);
	}

	private void RecalculateSize()
	{
		float num = Math.Abs((GetComponent<Collider>() as BoxCollider).size[layoutDimension1]);
		float num2 = (0f - num) / 2f;
		float num3 = num2;
		if (renderedItems.Any())
		{
			ValidateBreadth();
			int num4 = renderedItems.Count - 1;
			int num5 = num4 - num4 % breadth;
			int num6 = Math.Min(num5 + breadth, renderedItems.Count);
			for (int i = num5; i < num6; i++)
			{
				ITouchListItem key = renderedItems[i];
				num3 = Math.Max(itemInfos[key].Max[layoutDimension1], num3);
			}
			contentSize = num3 - num2 + padding[GetVector2Dimension(layoutDimension1)];
			excessContentSize = Math.Max(contentSize - num, 0f);
		}
		else
		{
			contentSize = 0f;
			excessContentSize = 0f;
		}
		OnScrollingEnabledChanged();
	}

	public bool RecalculateLongListContentSize(bool fireOnScroll = true)
	{
		if (longListBehavior == null)
		{
			return false;
		}
		float fullListContentSize = m_fullListContentSize;
		m_fullListContentSize = 0f;
		bool flag = true;
		for (int i = 0; i < longListBehavior.AllItemsCount; i++)
		{
			if (longListBehavior.IsItemShowable(i))
			{
				m_fullListContentSize += longListBehavior.GetItemSize(i)[layoutDimension1];
				if (flag)
				{
					flag = false;
				}
				else
				{
					m_fullListContentSize += elementSpacing;
				}
			}
		}
		if (m_fullListContentSize > 0f)
		{
			m_fullListContentSize += 2f * padding[GetVector2Dimension(layoutDimension1)];
		}
		bool num = fullListContentSize != m_fullListContentSize;
		if (num && fireOnScroll)
		{
			OnScrolled();
			OnScrollingEnabledChanged();
		}
		return num;
	}

	private void UpdateBackgroundBounds()
	{
		if (!(background == null))
		{
			Vector3 size = (GetComponent<Collider>() as BoxCollider).size;
			size[layoutDimension1] = Math.Abs(size[layoutDimension1]);
			size[layoutDimension3] = 0f;
			Camera camera = CameraUtils.FindFirstByLayer((GameLayer)base.gameObject.layer);
			if (!(camera == null))
			{
				float num = Vector3.Distance(camera.transform.position, GetComponent<Collider>().bounds.min);
				float num2 = Vector3.Distance(camera.transform.position, GetComponent<Collider>().bounds.max);
				Vector3 position = ((num > num2) ? GetComponent<Collider>().bounds.min : GetComponent<Collider>().bounds.max);
				Vector3 zero = Vector3.zero;
				zero[layoutDimension3] = content.transform.InverseTransformPoint(position)[layoutDimension3];
				background.SetBounds(new Bounds(zero, size));
				UpdateBackgroundScroll();
			}
		}
	}

	private void UpdateBackgroundScroll()
	{
		if (!(background == null))
		{
			float num = Math.Abs((GetComponent<Collider>() as BoxCollider).size[layoutDimension1]);
			float num2 = content.transform.localPosition[layoutDimension1];
			if (orientation == Orientation.Vertical)
			{
				num2 *= -1f;
			}
			Vector2 offset = background.Offset;
			offset[GetVector2Dimension(layoutDimension1)] = num2 / num;
			background.Offset = offset;
		}
	}

	private float GetBreadthPosition(int itemIndex)
	{
		float num = padding[GetVector2Dimension(layoutDimension2)];
		float num2 = 0f;
		int num3 = itemIndex - itemIndex % breadth;
		int num4 = Math.Min(num3 + breadth, renderedItems.Count);
		for (int i = num3; i < num4; i++)
		{
			if (i == itemIndex)
			{
				num2 = num;
			}
			num += itemInfos[renderedItems[i]].Size[layoutDimension2];
		}
		num += padding[GetVector2Dimension(layoutDimension2)];
		float num5 = 0f;
		float num6 = (GetComponent<Collider>() as BoxCollider).size[layoutDimension2];
		Alignment alignment = this.alignment;
		if (orientation == Orientation.Horizontal && this.alignment != Alignment.Mid)
		{
			alignment = this.alignment ^ Alignment.Max;
		}
		switch (alignment)
		{
		case Alignment.Min:
			num5 = (0f - num6) / 2f;
			break;
		case Alignment.Mid:
			num5 = (0f - num) / 2f;
			break;
		case Alignment.Max:
			num5 = num6 / 2f - num;
			break;
		}
		return num5 + num2;
	}

	private Vector3 GetNegatedScale(Vector3 scale)
	{
		scale[(layoutPlane == LayoutPlane.XY) ? 1 : 2] *= -1f;
		return scale;
	}

	private int GetVector2Dimension(int vec3Dimension)
	{
		if (vec3Dimension != 0)
		{
			return 1;
		}
		return vec3Dimension;
	}

	private int GetVector3Dimension(int vec2Dimension)
	{
		if (vec2Dimension == 0 || layoutPlane == LayoutPlane.XY)
		{
			return vec2Dimension;
		}
		return 2;
	}

	private void ValidateBreadth()
	{
		if (longListBehavior != null)
		{
			breadth = 1;
		}
		else
		{
			breadth = Math.Max(breadth, 1);
		}
	}

	private Bounds CalculateLocalClipBounds()
	{
		Vector3 vector = content.transform.InverseTransformPoint(GetComponent<Collider>().bounds.min);
		Vector3 vector2 = content.transform.InverseTransformPoint(GetComponent<Collider>().bounds.max);
		Vector3 center = (vector2 + vector) / 2f;
		Vector3 size = VectorUtils.Abs(vector2 - vector);
		return new Bounds(center, size);
	}
}
