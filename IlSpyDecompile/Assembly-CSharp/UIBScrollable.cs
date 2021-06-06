using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

[CustomEditClass]
public class UIBScrollable : PegUICustomBehavior
{
	public enum ScrollDirection
	{
		X,
		Y,
		Z
	}

	public enum HeightMode
	{
		UseHeightCallback,
		UseScrollableItem,
		UseBoxCollider
	}

	public enum ScrollWheelMode
	{
		ScaledToScrollSize,
		FixedRate
	}

	public interface IContent
	{
		UIBScrollable Scrollable { get; }
	}

	private class ContentComponent : MonoBehaviour, IContent
	{
		public UIBScrollable Scrollable { get; set; }
	}

	public delegate void EnableScroll(bool enabled);

	public delegate float ScrollHeightCallback();

	public delegate void ScrollTurnedOn(bool on);

	public delegate void OnScrollComplete(float percentage);

	public delegate void OnTouchScrollStarted();

	public delegate void OnTouchScrollEnded();

	public delegate void VisibleAffected(GameObject obj, bool visible);

	protected class VisibleAffectedObject
	{
		public GameObject Obj;

		public Vector3 Extents;

		public bool Visible;

		public VisibleAffected Callback;
	}

	[CustomEditField(Sections = "Camera Settings")]
	public bool m_UseCameraFromLayer;

	[CustomEditField(Sections = "Preferences")]
	public float m_ScrollWheelAmount = 0.1f;

	[CustomEditField(Sections = "Preferences")]
	public ScrollWheelMode m_ScrollWheelMode;

	[CustomEditField(Sections = "Preferences")]
	public float m_ScrollBottomPadding;

	[CustomEditField(Sections = "Preferences")]
	public iTween.EaseType m_ScrollEaseType = iTween.Defaults.easeType;

	[CustomEditField(Sections = "Preferences")]
	public float m_ScrollTweenTime = 0.2f;

	[CustomEditField(Sections = "Preferences")]
	public ScrollDirection m_ScrollPlane = ScrollDirection.Z;

	[CustomEditField(Sections = "Preferences")]
	public bool m_ScrollDirectionReverse;

	[CustomEditField(Sections = "Preferences")]
	[Tooltip("If scrolling is active, all PegUI calls will be suppressed")]
	public bool m_OverridePegUI;

	[CustomEditField(Sections = "Preferences")]
	public bool m_ForceScrollAreaHitTest;

	[CustomEditField(Sections = "Preferences")]
	public bool m_ScrollOnMouseDrag;

	[CustomEditField(Sections = "Bounds Settings")]
	public BoxCollider m_ScrollBounds;

	[CustomEditField(Sections = "Optional Bounds Settings")]
	public BoxCollider m_TouchDragFullArea;

	[CustomEditField(Sections = "Thumb Settings")]
	public BoxCollider m_ScrollTrack;

	[CustomEditField(Sections = "Thumb Settings")]
	public ScrollBarThumb m_ScrollThumb;

	[CustomEditField(Sections = "Thumb Settings")]
	public bool m_HideThumbWhenDisabled;

	[CustomEditField(Sections = "Thumb Settings")]
	public GameObject m_scrollTrackCover;

	[CustomEditField(Sections = "Bounds Settings")]
	public GameObject m_ScrollObject;

	[CustomEditField(Sections = "Bounds Settings")]
	public float m_VisibleObjectThreshold;

	[CustomEditField(Sections = "Preferences")]
	public bool m_UseScrollContentsInHitTest = true;

	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Drag distance required to initiate deck tile dragging (inches)")]
	public float m_DeckTileDragThreshold = 0.04f;

	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Drag distance required to initiate scroll dragging (inches)")]
	public float m_ScrollDragThreshold = 0.04f;

	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Stopping speed for scrolling after the user has let go")]
	public float m_MinKineticScrollSpeed = 0.01f;

	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Resistance for slowing down scrolling after the user has let go")]
	public float m_KineticScrollFriction = 6f;

	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Strength of the boundary springs")]
	public float m_ScrollBoundsSpringK = 700f;

	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Distance at which the out-of-bounds scroll value will snapped to 0 or 1")]
	public float m_MinOutOfBoundsScrollValue = 0.001f;

	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Use this to match scaling issues.")]
	public float m_ScrollDeltaMultiplier = 1f;

	[CustomEditField(Sections = "Touch Settings")]
	public List<BoxCollider> m_TouchScrollBlockers = new List<BoxCollider>();

	public HeightMode m_HeightMode = HeightMode.UseScrollableItem;

	private bool m_Enabled = true;

	private float m_ScrollValue;

	private float m_LastTouchScrollValue;

	private bool m_InputBlocked;

	private bool m_Pause;

	private bool m_PauseUpdateScrollHeight;

	private bool m_overrideHideThumb;

	private Vector2? m_TouchBeginScreenPos;

	private Vector3? m_TouchDragBeginWorldPos;

	private float m_TouchDragBeginScrollValue;

	private float prevScrollValue;

	private Vector3 m_ScrollAreaStartPos;

	private float m_ScrollThumbStartYPos;

	private ScrollHeightCallback m_ScrollHeightCallback;

	private List<EnableScroll> m_EnableScrollListeners = new List<EnableScroll>();

	private float m_LastScrollHeightRecorded;

	private float m_PolledScrollHeight;

	private List<VisibleAffectedObject> m_VisibleAffectedObjects = new List<VisibleAffectedObject>();

	private List<OnTouchScrollStarted> m_TouchScrollStartedListeners = new List<OnTouchScrollStarted>();

	private List<OnTouchScrollEnded> m_TouchScrollEndedListeners = new List<OnTouchScrollEnded>();

	private bool m_ForceShowVisibleAffectedObjects;

	private static Map<string, float> s_SavedScrollValues = new Map<string, float>();

	[CustomEditField(Sections = "Scroll")]
	public float ScrollValue
	{
		get
		{
			return m_ScrollValue;
		}
		set
		{
			if (!Application.isEditor)
			{
				SetScroll(value, blockInputWhileScrolling: false, clamp: false);
			}
		}
	}

	[Overridable]
	public float ImmediateScrollValue
	{
		get
		{
			return m_ScrollValue;
		}
		set
		{
			SetScrollImmediate(value);
		}
	}

	public static void DefaultVisibleAffectedCallback(GameObject obj, bool visible)
	{
		if (obj.activeSelf != visible)
		{
			obj.SetActive(visible);
		}
	}

	protected override void Awake()
	{
		ResetScrollStartPosition();
		SaveScrollThumbStartHeight();
		if (m_ScrollTrack != null && !UniversalInputManager.UsePhoneUI)
		{
			PegUIElement component = m_ScrollTrack.GetComponent<PegUIElement>();
			if (component != null)
			{
				component.AddEventListener(UIEventType.PRESS, delegate
				{
					StartDragging();
				});
			}
			PegUIElement component2 = m_ScrollThumb.GetComponent<PegUIElement>();
			if (component2 != null)
			{
				component2.AddEventListener(UIEventType.PRESS, delegate
				{
					StartDragging();
				});
			}
		}
		if (m_OverridePegUI)
		{
			base.Awake();
		}
		if (m_ScrollObject != null)
		{
			m_ScrollObject.AddComponent<ContentComponent>().Scrollable = this;
		}
	}

	public void Start()
	{
		if (m_scrollTrackCover != null)
		{
			m_scrollTrackCover.SetActive(value: false);
		}
	}

	protected override void OnDestroy()
	{
		if (m_OverridePegUI)
		{
			base.OnDestroy();
		}
	}

	private void Update()
	{
		UpdateScroll();
		if (!m_Enabled || m_InputBlocked || m_Pause || GetScrollCamera() == null)
		{
			return;
		}
		if (IsInputOverScrollableArea(m_ScrollBounds, out var _))
		{
			float axis = Input.GetAxis("Mouse ScrollWheel");
			if (axis != 0f)
			{
				float num = 0f;
				num = ((m_ScrollWheelMode != ScrollWheelMode.FixedRate) ? (m_ScrollWheelAmount * 10f) : (m_ScrollWheelAmount / GetTotalWorldScrollHeight()));
				AddScroll(0f - axis * num);
			}
		}
		if (m_ScrollThumb != null && m_ScrollThumb.IsDragging())
		{
			DragThumb();
		}
		else if (UniversalInputManager.Get().IsTouchMode() || m_ScrollOnMouseDrag)
		{
			DragContent();
		}
	}

	private bool IsInputOverScrollableArea(BoxCollider scrollableBounds, out RaycastHit hitInfo)
	{
		Camera scrollCamera = GetScrollCamera();
		if (UniversalInputManager.Get() == null || scrollCamera == null || m_ScrollBounds == null)
		{
			hitInfo = default(RaycastHit);
			return false;
		}
		bool flag = false;
		flag = (m_ForceScrollAreaHitTest ? UniversalInputManager.Get().ForcedInputIsOver(scrollCamera, scrollableBounds.gameObject, out hitInfo) : ((!PegUI.IsInitialized() || !PegUI.Get().IsUsingCameraDepthPriorityHitTest()) ? UniversalInputManager.Get().InputIsOver(scrollCamera, scrollableBounds.gameObject, out hitInfo) : UniversalInputManager.Get().InputIsOverByCameraDepth(scrollableBounds.gameObject, out hitInfo)));
		if (m_UseScrollContentsInHitTest && m_ScrollObject != null)
		{
			flag |= hitInfo.collider != null && hitInfo.collider.transform.IsChildOf(m_ScrollObject.transform);
		}
		return flag;
	}

	public override bool UpdateUI()
	{
		if (IsTouchDragging())
		{
			return m_Enabled;
		}
		return false;
	}

	public void ResetScrollStartPosition()
	{
		if (m_ScrollObject != null)
		{
			m_ScrollAreaStartPos = m_ScrollObject.transform.localPosition;
		}
	}

	public void ResetScrollStartPosition(Vector3 position)
	{
		if (m_ScrollObject != null)
		{
			m_ScrollAreaStartPos = position;
		}
	}

	public void AddVisibleAffectedObject(GameObject obj, Vector3 extents, bool visible, VisibleAffected callback = null)
	{
		m_VisibleAffectedObjects.Add(new VisibleAffectedObject
		{
			Obj = obj,
			Extents = extents,
			Visible = visible,
			Callback = ((callback == null) ? new VisibleAffected(DefaultVisibleAffectedCallback) : callback)
		});
	}

	public void RemoveVisibleAffectedObject(GameObject obj, VisibleAffected callback)
	{
		m_VisibleAffectedObjects.RemoveAll((VisibleAffectedObject o) => (object)o.Obj == obj && o.Callback == callback);
	}

	public void ClearVisibleAffectObjects()
	{
		m_VisibleAffectedObjects.Clear();
	}

	public void ForceVisibleAffectedObjectsShow(bool show)
	{
		if (m_ForceShowVisibleAffectedObjects != show)
		{
			m_ForceShowVisibleAffectedObjects = show;
			UpdateAndFireVisibleAffectedObjects();
		}
	}

	public void AddEnableScrollListener(EnableScroll dlg)
	{
		m_EnableScrollListeners.Add(dlg);
	}

	public void RemoveEnableScrollListener(EnableScroll dlg)
	{
		m_EnableScrollListeners.Remove(dlg);
	}

	public void AddTouchScrollStartedListener(OnTouchScrollStarted dlg)
	{
		m_TouchScrollStartedListeners.Add(dlg);
	}

	public void RemoveTouchScrollStartedListener(OnTouchScrollStarted dlg)
	{
		m_TouchScrollStartedListeners.Remove(dlg);
	}

	public void AddTouchScrollEndedListener(OnTouchScrollEnded dlg)
	{
		m_TouchScrollEndedListeners.Add(dlg);
	}

	public void RemoveTouchScrollEndedListener(OnTouchScrollEnded dlg)
	{
		m_TouchScrollEndedListeners.Remove(dlg);
	}

	public void Pause(bool pause)
	{
		m_Pause = pause;
	}

	public void PauseUpdateScrollHeight(bool pause)
	{
		m_PauseUpdateScrollHeight = pause;
	}

	public void Enable(bool enable)
	{
		if (m_Enabled != enable)
		{
			m_Enabled = enable;
			if (m_scrollTrackCover != null)
			{
				m_scrollTrackCover.SetActive(!enable);
			}
			RefreshShowThumb();
			if (m_Enabled)
			{
				ResetTouchDrag();
			}
			FireEnableScrollEvent();
		}
	}

	public bool IsEnabled()
	{
		return m_Enabled;
	}

	public bool IsEnabledAndScrollable()
	{
		if (m_Enabled)
		{
			return IsScrollNeeded();
		}
		return false;
	}

	public float GetScroll()
	{
		return m_ScrollValue;
	}

	public void SaveScroll(string savedName)
	{
		s_SavedScrollValues[savedName] = m_ScrollValue;
	}

	public void LoadScroll(string savedName, bool snap)
	{
		float value = 0f;
		if (s_SavedScrollValues.TryGetValue(savedName, out value))
		{
			if (snap)
			{
				SetScrollSnap(value);
			}
			else
			{
				SetScroll(value);
			}
			ResetTouchDrag();
		}
	}

	public bool EnableIfNeeded()
	{
		bool flag = IsScrollNeeded();
		Enable(flag);
		return flag;
	}

	public bool IsScrollNeeded()
	{
		return GetTotalWorldScrollHeight() > 0f;
	}

	public float PollScrollHeight()
	{
		switch (m_HeightMode)
		{
		case HeightMode.UseHeightCallback:
			if (m_ScrollHeightCallback == null)
			{
				return m_PolledScrollHeight;
			}
			return m_ScrollHeightCallback();
		case HeightMode.UseScrollableItem:
			return GetScrollableItemsHeight();
		default:
			return 0f;
		}
	}

	public void SetScroll(float percentage, bool blockInputWhileScrolling = false, bool clamp = true)
	{
		SetScroll(percentage, null, blockInputWhileScrolling, clamp);
	}

	public void SetScroll(float percentage, iTween.EaseType tweenType, float tweenTime, bool blockInputWhileScrolling = false, bool clamp = true)
	{
		SetScroll(percentage, null, tweenType, tweenTime, blockInputWhileScrolling, clamp);
	}

	public void SetScrollSnap(float percentage, bool clamp = true)
	{
		SetScrollSnap(percentage, null, clamp);
	}

	public void SetScroll(float percentage, OnScrollComplete scrollComplete, bool blockInputWhileScrolling = false, bool clamp = true)
	{
		StartCoroutine(SetScrollWait(percentage, scrollComplete, blockInputWhileScrolling, tween: true, null, null, clamp));
	}

	public void SetScroll(float percentage, OnScrollComplete scrollComplete, iTween.EaseType tweenType, float tweenTime, bool blockInputWhileScrolling = false, bool clamp = true)
	{
		StartCoroutine(SetScrollWait(percentage, scrollComplete, blockInputWhileScrolling, tween: true, tweenType, tweenTime, clamp));
	}

	public void SetScrollSnap(float percentage, OnScrollComplete scrollComplete, bool clamp = true)
	{
		m_PolledScrollHeight = PollScrollHeight();
		m_LastScrollHeightRecorded = m_PolledScrollHeight;
		ScrollTo(percentage, scrollComplete, blockInputWhileScrolling: false, tween: false, null, null, clamp);
		ResetTouchDrag();
	}

	public void StopScroll()
	{
		Vector3 scrollAreaStartPos = m_ScrollAreaStartPos;
		Vector3 vector = scrollAreaStartPos;
		Vector3 totalScrollHeightVector = GetTotalScrollHeightVector(convertToLocalSpace: true);
		vector += totalScrollHeightVector * (m_ScrollDirectionReverse ? (-1f) : 1f);
		Vector3 localPosition = m_ScrollObject.transform.localPosition;
		float scrollImmediate = (((vector - scrollAreaStartPos).magnitude > float.Epsilon) ? ((localPosition - scrollAreaStartPos).magnitude / (vector - scrollAreaStartPos).magnitude) : 0f);
		iTween.Stop(m_ScrollObject);
		SetScrollImmediate(scrollImmediate);
	}

	public void SetScrollHeightCallback(ScrollHeightCallback dlg, bool refresh = false, bool resetScroll = false)
	{
		float? setResetScroll = null;
		if (resetScroll)
		{
			setResetScroll = 0f;
		}
		SetScrollHeightCallback(dlg, setResetScroll, refresh);
	}

	public void SetScrollHeightCallback(ScrollHeightCallback dlg, float? setResetScroll, bool refresh = false)
	{
		m_VisibleAffectedObjects.Clear();
		m_ScrollHeightCallback = dlg;
		if (setResetScroll.HasValue)
		{
			m_ScrollValue = setResetScroll.Value;
			ResetTouchDrag();
		}
		if (refresh)
		{
			UpdateScroll();
			UpdateThumbPosition();
			UpdateScrollObjectPosition(tween: true, null, null, null);
		}
		m_PolledScrollHeight = PollScrollHeight();
		m_LastScrollHeightRecorded = m_PolledScrollHeight;
	}

	public void SetHeight(float height)
	{
		m_ScrollHeightCallback = null;
		m_PolledScrollHeight = height;
		UpdateHeight();
	}

	public void UpdateScroll()
	{
		if (!m_PauseUpdateScrollHeight)
		{
			m_PolledScrollHeight = PollScrollHeight();
			UpdateHeight();
		}
	}

	public void CenterWorldPosition(Vector3 position)
	{
		float percentage = m_ScrollObject.transform.InverseTransformPoint(position)[(int)m_ScrollPlane] / (0f - (m_PolledScrollHeight + m_ScrollBottomPadding)) * 2f - 0.5f;
		StartCoroutine(BlockInput(m_ScrollTweenTime));
		SetScroll(percentage);
	}

	public bool IsObjectVisibleInScrollArea(GameObject obj, Vector3 extents, bool fullyVisible = false)
	{
		int scrollPlane = (int)m_ScrollPlane;
		float num = obj.transform.position[scrollPlane] - extents[scrollPlane];
		float num2 = obj.transform.position[scrollPlane] + extents[scrollPlane];
		Bounds bounds = m_ScrollBounds.bounds;
		float num3 = bounds.min[scrollPlane] - m_VisibleObjectThreshold;
		float num4 = bounds.max[scrollPlane] + m_VisibleObjectThreshold;
		bool flag = num >= num3 && num <= num4;
		bool flag2 = num2 >= num3 && num2 <= num4;
		if (fullyVisible)
		{
			return flag && flag2;
		}
		return flag || flag2;
	}

	public bool CenterObjectInView(GameObject obj, float positionOffset, OnScrollComplete scrollComplete, iTween.EaseType tweenType, float tweenTime, bool blockInputWhileScrolling = false)
	{
		float z = m_ScrollBounds.bounds.extents.z;
		return ScrollObjectIntoView(obj, positionOffset, z, scrollComplete, tweenType, tweenTime, blockInputWhileScrolling);
	}

	public bool ScrollObjectIntoView(GameObject obj, float positionOffset, float axisExtent, OnScrollComplete scrollComplete, iTween.EaseType tweenType, float tweenTime, bool blockInputWhileScrolling = false)
	{
		int scrollPlane = (int)m_ScrollPlane;
		float num = obj.transform.position[scrollPlane] + positionOffset - axisExtent;
		float num2 = obj.transform.position[scrollPlane] + positionOffset + axisExtent;
		Bounds bounds = m_ScrollBounds.bounds;
		float num3 = bounds.min[scrollPlane] - m_VisibleObjectThreshold;
		float num4 = bounds.max[scrollPlane] + m_VisibleObjectThreshold;
		bool flag = num >= num3;
		bool flag2 = num2 <= num4;
		if (flag && flag2)
		{
			return false;
		}
		float percentage = 0f;
		if (!flag)
		{
			float z = GetTotalScrollHeightVector().z;
			if (z == 0f)
			{
				Debug.LogWarning("UIBScrollable.ScrollObjectIntoView() - scrollHeight calculated as 0, cannot calculate scroll percentage!");
			}
			else
			{
				float num5 = Mathf.Abs(Math.Abs(num3 - num) / z);
				percentage = m_ScrollValue + num5;
			}
		}
		else if (!flag2)
		{
			float z2 = GetTotalScrollHeightVector().z;
			if (z2 == 0f)
			{
				Debug.LogWarning("UIBScrollable.ScrollObjectIntoView() - scrollHeight calculated as 0, cannot calculate scroll percentage!");
			}
			else
			{
				float num6 = Mathf.Abs(Math.Abs(num4 - num2) / z2);
				percentage = m_ScrollValue - num6;
			}
		}
		SetScroll(percentage, scrollComplete, tweenType, tweenTime, blockInputWhileScrolling);
		return true;
	}

	public bool IsDragging()
	{
		if (!(m_ScrollThumb != null) || !m_ScrollThumb.IsDragging())
		{
			return m_TouchBeginScreenPos.HasValue;
		}
		return true;
	}

	public bool IsTouchDragging()
	{
		if (!m_TouchBeginScreenPos.HasValue)
		{
			return false;
		}
		float num = Mathf.Abs(UniversalInputManager.Get().GetMousePosition().y - m_TouchBeginScreenPos.Value.y);
		return m_ScrollDragThreshold * ((Screen.dpi > 0f) ? Screen.dpi : 150f) <= num;
	}

	public void SetScrollImmediate(float percentage)
	{
		ScrollTo(percentage, null, blockInputWhileScrolling: false, tween: false, null, 0f, clamp: true);
		ResetTouchDrag();
	}

	public void SetScrollImmediate(float percentage, OnScrollComplete scrollComplete, bool blockInputWhileScrolling, bool tween, iTween.EaseType? tweenType, float? tweenTime, bool clamp)
	{
		ScrollTo(percentage, scrollComplete, blockInputWhileScrolling, tween, tweenType, tweenTime, clamp);
		ResetTouchDrag();
	}

	public void SetHideThumb(bool value)
	{
		m_overrideHideThumb = value;
		RefreshShowThumb();
	}

	private void RefreshShowThumb()
	{
		if (!(m_ScrollThumb != null))
		{
			return;
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_ScrollThumb.gameObject.SetActive(value: false);
			return;
		}
		bool flag = (m_Enabled || !m_HideThumbWhenDisabled) && !m_overrideHideThumb;
		if (flag != m_ScrollThumb.gameObject.activeSelf)
		{
			m_ScrollThumb.gameObject.SetActive(flag);
			if (flag)
			{
				UpdateThumbPosition();
			}
		}
	}

	private void StartDragging()
	{
		if (!m_InputBlocked && !m_Pause && m_Enabled)
		{
			m_ScrollThumb.StartDragging();
		}
	}

	private void UpdateHeight()
	{
		if (Mathf.Abs(m_PolledScrollHeight - m_LastScrollHeightRecorded) > 0.001f)
		{
			if (!EnableIfNeeded())
			{
				m_ScrollValue = 0f;
			}
			UpdateThumbPosition();
			UpdateScrollObjectPosition(tween: false, null, null, null);
			ResetTouchDrag();
		}
		m_LastScrollHeightRecorded = m_PolledScrollHeight;
	}

	private void DragContent()
	{
		if (UniversalInputManager.Get().GetMouseButtonDown(0))
		{
			if (GetWorldTouchPosition().HasValue)
			{
				m_TouchBeginScreenPos = UniversalInputManager.Get().GetMousePosition();
				return;
			}
		}
		else if (UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			m_TouchBeginScreenPos = null;
			m_TouchDragBeginWorldPos = null;
			FireTouchEndEvent();
		}
		if (m_TouchDragBeginWorldPos.HasValue)
		{
			Vector3? worldTouchPositionOnDragArea = GetWorldTouchPositionOnDragArea();
			if (worldTouchPositionOnDragArea.HasValue)
			{
				int scrollPlane = (int)m_ScrollPlane;
				m_LastTouchScrollValue = m_ScrollValue;
				float worldDelta = worldTouchPositionOnDragArea.Value[scrollPlane] - m_TouchDragBeginWorldPos.Value[scrollPlane];
				float scrollValueDelta = GetScrollValueDelta(worldDelta);
				float num = m_TouchDragBeginScrollValue + scrollValueDelta;
				float outOfBoundsDist = GetOutOfBoundsDist(num);
				if (outOfBoundsDist != 0f)
				{
					outOfBoundsDist = Mathf.Log10(Mathf.Abs(outOfBoundsDist) + 1f) * Mathf.Sign(outOfBoundsDist);
					num = ((outOfBoundsDist < 0f) ? outOfBoundsDist : (outOfBoundsDist + 1f));
				}
				ScrollTo(Mathf.Lerp(prevScrollValue, num, 0.9f), null, blockInputWhileScrolling: false, tween: false, null, null, clamp: false);
				prevScrollValue = num;
			}
			return;
		}
		if (m_TouchBeginScreenPos.HasValue)
		{
			float num2 = Mathf.Abs(UniversalInputManager.Get().GetMousePosition().x - m_TouchBeginScreenPos.Value.x);
			float num3 = Mathf.Abs(UniversalInputManager.Get().GetMousePosition().y - m_TouchBeginScreenPos.Value.y);
			bool num4 = num2 > m_DeckTileDragThreshold * ((Screen.dpi > 0f) ? Screen.dpi : 150f);
			bool flag = num3 > m_ScrollDragThreshold * ((Screen.dpi > 0f) ? Screen.dpi : 150f);
			if (num4 && (num2 >= num3 || !flag))
			{
				m_TouchBeginScreenPos = null;
			}
			else if (flag)
			{
				m_TouchDragBeginWorldPos = GetWorldTouchPositionOnDragArea();
				m_TouchDragBeginScrollValue = m_ScrollValue;
				m_LastTouchScrollValue = m_ScrollValue;
				FireTouchStartEvent();
			}
			return;
		}
		float num5 = (m_ScrollValue - m_LastTouchScrollValue) / Time.fixedDeltaTime;
		float outOfBoundsDist2 = GetOutOfBoundsDist(m_ScrollValue);
		if (outOfBoundsDist2 != 0f)
		{
			if (Mathf.Abs(outOfBoundsDist2) >= m_MinOutOfBoundsScrollValue)
			{
				float num6 = (0f - m_ScrollBoundsSpringK) * outOfBoundsDist2 - Mathf.Sqrt(4f * m_ScrollBoundsSpringK) * num5;
				num5 += num6 * Time.fixedDeltaTime;
				m_LastTouchScrollValue = m_ScrollValue;
				ScrollTo(m_ScrollValue + num5 * Time.fixedDeltaTime, null, blockInputWhileScrolling: false, tween: false, null, null, clamp: false);
			}
			if (Mathf.Abs(GetOutOfBoundsDist(m_ScrollValue)) < m_MinOutOfBoundsScrollValue)
			{
				ScrollTo(Mathf.Round(m_ScrollValue), null, blockInputWhileScrolling: false, tween: false, null, null, clamp: false);
				m_LastTouchScrollValue = m_ScrollValue;
			}
			prevScrollValue = m_ScrollValue;
		}
		else if (m_LastTouchScrollValue != m_ScrollValue)
		{
			float num7 = Mathf.Sign(num5);
			num5 -= num7 * m_KineticScrollFriction * Time.fixedDeltaTime;
			m_LastTouchScrollValue = m_ScrollValue;
			if (Mathf.Abs(num5) >= m_MinKineticScrollSpeed && Mathf.Sign(num5) == num7)
			{
				ScrollTo(m_ScrollValue + num5 * Time.fixedDeltaTime, null, blockInputWhileScrolling: false, tween: false, null, null, clamp: false);
			}
		}
	}

	private void DragThumb()
	{
		Vector3 min = m_ScrollTrack.bounds.min;
		Camera camera = CameraUtils.FindFirstByLayer(m_ScrollTrack.gameObject.layer);
		Plane plane = new Plane(-camera.transform.forward, min);
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		if (plane.Raycast(ray, out var enter))
		{
			Vector3 point = ray.GetPoint(enter);
			float scrollTrackTop1D = GetScrollTrackTop1D();
			float scrollTrackBtm1D = GetScrollTrackBtm1D();
			float num = Mathf.Clamp01((point[(int)m_ScrollPlane] - scrollTrackTop1D) / (scrollTrackBtm1D - scrollTrackTop1D));
			if (Mathf.Abs(m_ScrollValue - num) > Mathf.Epsilon)
			{
				m_ScrollValue = num;
				UpdateThumbPosition();
				UpdateScrollObjectPosition(tween: false, null, null, null);
			}
		}
		ResetTouchDrag();
	}

	private void ResetTouchDrag()
	{
		bool hasValue = m_TouchDragBeginWorldPos.HasValue;
		m_TouchBeginScreenPos = null;
		m_TouchDragBeginWorldPos = null;
		m_TouchDragBeginScrollValue = m_ScrollValue;
		m_LastTouchScrollValue = m_ScrollValue;
		if (hasValue)
		{
			FireTouchEndEvent();
		}
	}

	private float GetScrollTrackTop1D()
	{
		return GetScrollTrackTop()[(int)m_ScrollPlane];
	}

	private float GetScrollTrackBtm1D()
	{
		return GetScrollTrackBtm()[(int)m_ScrollPlane];
	}

	private Vector3 GetScrollTrackTop()
	{
		if (m_ScrollTrack == null)
		{
			return Vector3.zero;
		}
		if (m_ScrollPlane == ScrollDirection.X)
		{
			return m_ScrollTrack.bounds.min;
		}
		return m_ScrollTrack.bounds.max;
	}

	private Vector3 GetScrollTrackBtm()
	{
		if (m_ScrollTrack == null)
		{
			return Vector3.zero;
		}
		if (m_ScrollPlane == ScrollDirection.X)
		{
			return m_ScrollTrack.bounds.max;
		}
		return m_ScrollTrack.bounds.min;
	}

	private void AddScroll(float amount)
	{
		ScrollTo(m_ScrollValue + amount, null, blockInputWhileScrolling: false, tween: true, null, null, clamp: true);
		ResetTouchDrag();
	}

	private void ScrollTo(float percentage, OnScrollComplete scrollComplete, bool blockInputWhileScrolling, bool tween, iTween.EaseType? tweenType, float? tweenTime, bool clamp)
	{
		m_ScrollValue = (clamp ? Mathf.Clamp01(percentage) : percentage);
		UpdateThumbPosition();
		UpdateScrollObjectPosition(tween, scrollComplete, tweenType, tweenTime, blockInputWhileScrolling);
	}

	private void UpdateThumbPosition()
	{
		if (!(m_ScrollThumb == null))
		{
			Vector3 scrollTrackTop = GetScrollTrackTop();
			Vector3 scrollTrackBtm = GetScrollTrackBtm();
			float num = scrollTrackTop[(int)m_ScrollPlane];
			float num2 = scrollTrackBtm[(int)m_ScrollPlane];
			Vector3 position = scrollTrackTop + (scrollTrackBtm - scrollTrackTop) * 0.5f;
			position[(int)m_ScrollPlane] = num + (num2 - num) * Mathf.Clamp01(m_ScrollValue);
			m_ScrollThumb.transform.position = position;
			if (m_ScrollPlane == ScrollDirection.Z)
			{
				Vector3 localPosition = m_ScrollThumb.transform.localPosition;
				m_ScrollThumb.transform.localPosition = new Vector3(localPosition.x, m_ScrollThumbStartYPos, localPosition.z);
			}
		}
	}

	private void UpdateScrollObjectPosition(bool tween, OnScrollComplete scrollComplete, iTween.EaseType? tweenType, float? tweenTime, bool blockInputWhileScrolling = false)
	{
		if (m_ScrollObject == null)
		{
			return;
		}
		Vector3 scrollAreaStartPos = m_ScrollAreaStartPos;
		Vector3 vector = scrollAreaStartPos;
		Vector3 totalScrollHeightVector = GetTotalScrollHeightVector(convertToLocalSpace: true);
		vector += totalScrollHeightVector * (m_ScrollDirectionReverse ? (-1f) : 1f);
		Vector3 vector2 = scrollAreaStartPos + m_ScrollValue * (vector - scrollAreaStartPos);
		if (float.IsNaN(vector2.x) || float.IsNaN(vector2.y) || float.IsNaN(vector2.z))
		{
			return;
		}
		if (tween)
		{
			iTween.MoveTo(m_ScrollObject, iTween.Hash("position", vector2, "time", tweenTime.HasValue ? tweenTime.Value : m_ScrollTweenTime, "isLocal", true, "easetype", tweenType.HasValue ? tweenType.Value : m_ScrollEaseType, "onupdate", (Action<object>)delegate
			{
				UpdateAndFireVisibleAffectedObjects();
			}, "oncomplete", (Action<object>)delegate
			{
				UpdateAndFireVisibleAffectedObjects();
				if (scrollComplete != null)
				{
					scrollComplete(m_ScrollValue);
				}
			}));
		}
		else
		{
			if (m_ScrollPlane == ScrollDirection.Z)
			{
				m_ScrollObject.transform.localPosition = new Vector3(vector2.x, m_ScrollObject.transform.localPosition.y, vector2.z);
			}
			else
			{
				m_ScrollObject.transform.localPosition = vector2;
			}
			UpdateAndFireVisibleAffectedObjects();
			if (scrollComplete != null)
			{
				scrollComplete(m_ScrollValue);
			}
		}
	}

	private IEnumerator SetScrollWait(float percentage, OnScrollComplete scrollComplete, bool blockInputWhileScrolling, bool tween, iTween.EaseType? tweenType, float? tweenTime, bool clamp)
	{
		yield return null;
		ScrollTo(percentage, scrollComplete, blockInputWhileScrolling, tween, tweenType, tweenTime, clamp);
		ResetTouchDrag();
	}

	private IEnumerator BlockInput(float blockTime)
	{
		m_InputBlocked = true;
		yield return new WaitForSeconds(blockTime);
		m_InputBlocked = false;
	}

	private Vector3 GetTotalScrollHeightVector(bool convertToLocalSpace = false)
	{
		if (m_ScrollObject == null)
		{
			Log.All.PrintWarning("GetTotalScrollHeightVector() returning zero. m_ScrollObject is null!");
			return Vector3.zero;
		}
		float num = m_PolledScrollHeight - GetScrollBoundsHeight();
		if (num < 0f)
		{
			return Vector3.zero;
		}
		Vector3 vector = Vector3.zero;
		vector[(int)m_ScrollPlane] = num;
		if (convertToLocalSpace)
		{
			vector = m_ScrollObject.transform.parent.worldToLocalMatrix * vector;
		}
		if (m_ScrollBottomPadding > 0f)
		{
			vector += vector.normalized * m_ScrollBottomPadding;
		}
		return vector;
	}

	private float GetTotalWorldScrollHeight()
	{
		return GetTotalScrollHeightVector().magnitude;
	}

	private Vector3? GetWorldTouchPosition()
	{
		return GetWorldTouchPosition(m_ScrollBounds);
	}

	private Vector3? GetWorldTouchPositionOnDragArea()
	{
		Vector3? result = null;
		if (m_TouchDragFullArea != null)
		{
			result = GetWorldTouchPosition(m_TouchDragFullArea);
		}
		if (!result.HasValue && m_ScrollBounds != null)
		{
			result = GetWorldTouchPosition(m_ScrollBounds);
		}
		return result;
	}

	private Vector3? GetWorldTouchPosition(BoxCollider bounds)
	{
		Camera scrollCamera = GetScrollCamera();
		if (scrollCamera == null)
		{
			return null;
		}
		Ray ray = scrollCamera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		RaycastHit hitInfo;
		foreach (BoxCollider touchScrollBlocker in m_TouchScrollBlockers)
		{
			if (touchScrollBlocker.Raycast(ray, out hitInfo, float.MaxValue))
			{
				return null;
			}
		}
		if (IsInputOverScrollableArea(bounds, out hitInfo))
		{
			return ray.GetPoint(hitInfo.distance);
		}
		return null;
	}

	private float GetScrollValueDelta(float worldDelta)
	{
		return m_ScrollDeltaMultiplier * worldDelta / GetTotalWorldScrollHeight();
	}

	private float GetOutOfBoundsDist(float scrollValue)
	{
		if (scrollValue < 0f)
		{
			return scrollValue;
		}
		if (scrollValue > 1f)
		{
			return scrollValue - 1f;
		}
		return 0f;
	}

	private void FireEnableScrollEvent()
	{
		EnableScroll[] array = m_EnableScrollListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](m_Enabled);
		}
	}

	public void UpdateAndFireVisibleAffectedObjects()
	{
		VisibleAffectedObject[] array = m_VisibleAffectedObjects.ToArray();
		foreach (VisibleAffectedObject visibleAffectedObject in array)
		{
			bool flag = IsObjectVisibleInScrollArea(visibleAffectedObject.Obj, visibleAffectedObject.Extents) || m_ForceShowVisibleAffectedObjects;
			if (flag != visibleAffectedObject.Visible)
			{
				visibleAffectedObject.Visible = flag;
				visibleAffectedObject.Callback(visibleAffectedObject.Obj, flag);
			}
		}
	}

	private float GetScrollBoundsHeight()
	{
		if (m_ScrollObject == null)
		{
			Debug.LogWarning("No m_ScrollObject set for this UIBScrollable!");
			return 0f;
		}
		return m_ScrollBounds.bounds.size[(int)m_ScrollPlane];
	}

	private void FireTouchStartEvent()
	{
		OnTouchScrollStarted[] array = m_TouchScrollStartedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	private void FireTouchEndEvent()
	{
		OnTouchScrollEnded[] array = m_TouchScrollEndedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	private float GetScrollableItemsHeight()
	{
		Vector3 min = Vector3.zero;
		Vector3 max = Vector3.zero;
		if (GetScrollableItemsMinMax(ref min, ref max) == null)
		{
			return 0f;
		}
		int scrollPlane = (int)m_ScrollPlane;
		return max[scrollPlane] - min[scrollPlane];
	}

	private UIBScrollableItem[] GetScrollableItemsMinMax(ref Vector3 min, ref Vector3 max)
	{
		if (m_ScrollObject == null)
		{
			return null;
		}
		UIBScrollableItem[] componentsInChildren = m_ScrollObject.GetComponentsInChildren<UIBScrollableItem>(includeInactive: true);
		if (componentsInChildren == null || componentsInChildren.Length == 0)
		{
			return null;
		}
		min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
		UIBScrollableItem[] array = componentsInChildren;
		foreach (UIBScrollableItem uIBScrollableItem in array)
		{
			if (uIBScrollableItem.IsActive())
			{
				uIBScrollableItem.GetWorldBounds(out var min2, out var max2);
				min.x = Math.Min(min.x, Math.Min(min2.x, max2.x));
				min.y = Math.Min(min.y, Math.Min(min2.y, max2.y));
				min.z = Math.Min(min.z, Math.Min(min2.z, max2.z));
				max.x = Math.Max(max.x, Math.Max(min2.x, max2.x));
				max.y = Math.Max(max.y, Math.Max(min2.y, max2.y));
				max.z = Math.Max(max.z, Math.Max(min2.z, max2.z));
			}
		}
		return componentsInChildren;
	}

	private BoxCollider[] GetBoxCollidersMinMax(ref Vector3 min, ref Vector3 max)
	{
		return null;
	}

	private Camera GetScrollCamera()
	{
		if (m_UseCameraFromLayer)
		{
			return CameraUtils.FindFirstByLayer(base.gameObject.layer);
		}
		Box box = Box.Get();
		if (box == null)
		{
			return null;
		}
		return box.GetCamera();
	}

	private void SaveScrollThumbStartHeight()
	{
		if (m_ScrollThumb != null)
		{
			m_ScrollThumbStartYPos = m_ScrollThumb.transform.localPosition.y;
		}
	}
}
