using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000AF6 RID: 2806
[CustomEditClass]
public class UIBScrollable : PegUICustomBehavior
{
	// Token: 0x17000883 RID: 2179
	// (get) Token: 0x06009541 RID: 38209 RVA: 0x00305744 File Offset: 0x00303944
	// (set) Token: 0x06009542 RID: 38210 RVA: 0x0030574C File Offset: 0x0030394C
	[CustomEditField(Sections = "Scroll")]
	public float ScrollValue
	{
		get
		{
			return this.m_ScrollValue;
		}
		set
		{
			if (Application.isEditor)
			{
				return;
			}
			this.SetScroll(value, false, false);
		}
	}

	// Token: 0x17000884 RID: 2180
	// (get) Token: 0x06009543 RID: 38211 RVA: 0x00305744 File Offset: 0x00303944
	// (set) Token: 0x06009544 RID: 38212 RVA: 0x0030575F File Offset: 0x0030395F
	[Overridable]
	public float ImmediateScrollValue
	{
		get
		{
			return this.m_ScrollValue;
		}
		set
		{
			this.SetScrollImmediate(value);
		}
	}

	// Token: 0x06009545 RID: 38213 RVA: 0x00305768 File Offset: 0x00303968
	public static void DefaultVisibleAffectedCallback(GameObject obj, bool visible)
	{
		if (obj.activeSelf != visible)
		{
			obj.SetActive(visible);
		}
	}

	// Token: 0x06009546 RID: 38214 RVA: 0x0030577C File Offset: 0x0030397C
	protected override void Awake()
	{
		this.ResetScrollStartPosition();
		this.SaveScrollThumbStartHeight();
		if (this.m_ScrollTrack != null && !UniversalInputManager.UsePhoneUI)
		{
			PegUIElement component = this.m_ScrollTrack.GetComponent<PegUIElement>();
			if (component != null)
			{
				component.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
				{
					this.StartDragging();
				});
			}
			PegUIElement component2 = this.m_ScrollThumb.GetComponent<PegUIElement>();
			if (component2 != null)
			{
				component2.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
				{
					this.StartDragging();
				});
			}
		}
		if (this.m_OverridePegUI)
		{
			base.Awake();
		}
		if (this.m_ScrollObject != null)
		{
			this.m_ScrollObject.AddComponent<UIBScrollable.ContentComponent>().Scrollable = this;
		}
	}

	// Token: 0x06009547 RID: 38215 RVA: 0x0030582E File Offset: 0x00303A2E
	public void Start()
	{
		if (this.m_scrollTrackCover != null)
		{
			this.m_scrollTrackCover.SetActive(false);
		}
	}

	// Token: 0x06009548 RID: 38216 RVA: 0x0030584A File Offset: 0x00303A4A
	protected override void OnDestroy()
	{
		if (this.m_OverridePegUI)
		{
			base.OnDestroy();
		}
	}

	// Token: 0x06009549 RID: 38217 RVA: 0x0030585C File Offset: 0x00303A5C
	private void Update()
	{
		this.UpdateScroll();
		if (!this.m_Enabled || this.m_InputBlocked || this.m_Pause)
		{
			return;
		}
		if (this.GetScrollCamera() == null)
		{
			return;
		}
		RaycastHit raycastHit;
		if (this.IsInputOverScrollableArea(this.m_ScrollBounds, out raycastHit))
		{
			float axis = Input.GetAxis("Mouse ScrollWheel");
			if (axis != 0f)
			{
				float num;
				if (this.m_ScrollWheelMode == UIBScrollable.ScrollWheelMode.FixedRate)
				{
					num = this.m_ScrollWheelAmount / this.GetTotalWorldScrollHeight();
				}
				else
				{
					num = this.m_ScrollWheelAmount * 10f;
				}
				this.AddScroll(0f - axis * num);
			}
		}
		if (this.m_ScrollThumb != null && this.m_ScrollThumb.IsDragging())
		{
			this.DragThumb();
			return;
		}
		if (UniversalInputManager.Get().IsTouchMode() || this.m_ScrollOnMouseDrag)
		{
			this.DragContent();
		}
	}

	// Token: 0x0600954A RID: 38218 RVA: 0x00305934 File Offset: 0x00303B34
	private bool IsInputOverScrollableArea(BoxCollider scrollableBounds, out RaycastHit hitInfo)
	{
		Camera scrollCamera = this.GetScrollCamera();
		if (UniversalInputManager.Get() == null || scrollCamera == null || this.m_ScrollBounds == null)
		{
			hitInfo = default(RaycastHit);
			return false;
		}
		bool flag;
		if (this.m_ForceScrollAreaHitTest)
		{
			flag = UniversalInputManager.Get().ForcedInputIsOver(scrollCamera, scrollableBounds.gameObject, out hitInfo);
		}
		else if (PegUI.IsInitialized() && PegUI.Get().IsUsingCameraDepthPriorityHitTest())
		{
			flag = UniversalInputManager.Get().InputIsOverByCameraDepth(scrollableBounds.gameObject, out hitInfo);
		}
		else
		{
			flag = UniversalInputManager.Get().InputIsOver(scrollCamera, scrollableBounds.gameObject, out hitInfo);
		}
		if (this.m_UseScrollContentsInHitTest && this.m_ScrollObject != null)
		{
			flag |= (hitInfo.collider != null && hitInfo.collider.transform.IsChildOf(this.m_ScrollObject.transform));
		}
		return flag;
	}

	// Token: 0x0600954B RID: 38219 RVA: 0x00305A0E File Offset: 0x00303C0E
	public override bool UpdateUI()
	{
		return this.IsTouchDragging() && this.m_Enabled;
	}

	// Token: 0x0600954C RID: 38220 RVA: 0x00305A20 File Offset: 0x00303C20
	public void ResetScrollStartPosition()
	{
		if (this.m_ScrollObject != null)
		{
			this.m_ScrollAreaStartPos = this.m_ScrollObject.transform.localPosition;
		}
	}

	// Token: 0x0600954D RID: 38221 RVA: 0x00305A46 File Offset: 0x00303C46
	public void ResetScrollStartPosition(Vector3 position)
	{
		if (this.m_ScrollObject != null)
		{
			this.m_ScrollAreaStartPos = position;
		}
	}

	// Token: 0x0600954E RID: 38222 RVA: 0x00305A60 File Offset: 0x00303C60
	public void AddVisibleAffectedObject(GameObject obj, Vector3 extents, bool visible, UIBScrollable.VisibleAffected callback = null)
	{
		this.m_VisibleAffectedObjects.Add(new UIBScrollable.VisibleAffectedObject
		{
			Obj = obj,
			Extents = extents,
			Visible = visible,
			Callback = ((callback == null) ? new UIBScrollable.VisibleAffected(UIBScrollable.DefaultVisibleAffectedCallback) : callback)
		});
	}

	// Token: 0x0600954F RID: 38223 RVA: 0x00305AAC File Offset: 0x00303CAC
	public void RemoveVisibleAffectedObject(GameObject obj, UIBScrollable.VisibleAffected callback)
	{
		this.m_VisibleAffectedObjects.RemoveAll((UIBScrollable.VisibleAffectedObject o) => o.Obj == obj && o.Callback == callback);
	}

	// Token: 0x06009550 RID: 38224 RVA: 0x00305AE5 File Offset: 0x00303CE5
	public void ClearVisibleAffectObjects()
	{
		this.m_VisibleAffectedObjects.Clear();
	}

	// Token: 0x06009551 RID: 38225 RVA: 0x00305AF2 File Offset: 0x00303CF2
	public void ForceVisibleAffectedObjectsShow(bool show)
	{
		if (this.m_ForceShowVisibleAffectedObjects != show)
		{
			this.m_ForceShowVisibleAffectedObjects = show;
			this.UpdateAndFireVisibleAffectedObjects();
		}
	}

	// Token: 0x06009552 RID: 38226 RVA: 0x00305B0A File Offset: 0x00303D0A
	public void AddEnableScrollListener(UIBScrollable.EnableScroll dlg)
	{
		this.m_EnableScrollListeners.Add(dlg);
	}

	// Token: 0x06009553 RID: 38227 RVA: 0x00305B18 File Offset: 0x00303D18
	public void RemoveEnableScrollListener(UIBScrollable.EnableScroll dlg)
	{
		this.m_EnableScrollListeners.Remove(dlg);
	}

	// Token: 0x06009554 RID: 38228 RVA: 0x00305B27 File Offset: 0x00303D27
	public void AddTouchScrollStartedListener(UIBScrollable.OnTouchScrollStarted dlg)
	{
		this.m_TouchScrollStartedListeners.Add(dlg);
	}

	// Token: 0x06009555 RID: 38229 RVA: 0x00305B35 File Offset: 0x00303D35
	public void RemoveTouchScrollStartedListener(UIBScrollable.OnTouchScrollStarted dlg)
	{
		this.m_TouchScrollStartedListeners.Remove(dlg);
	}

	// Token: 0x06009556 RID: 38230 RVA: 0x00305B44 File Offset: 0x00303D44
	public void AddTouchScrollEndedListener(UIBScrollable.OnTouchScrollEnded dlg)
	{
		this.m_TouchScrollEndedListeners.Add(dlg);
	}

	// Token: 0x06009557 RID: 38231 RVA: 0x00305B52 File Offset: 0x00303D52
	public void RemoveTouchScrollEndedListener(UIBScrollable.OnTouchScrollEnded dlg)
	{
		this.m_TouchScrollEndedListeners.Remove(dlg);
	}

	// Token: 0x06009558 RID: 38232 RVA: 0x00305B61 File Offset: 0x00303D61
	public void Pause(bool pause)
	{
		this.m_Pause = pause;
	}

	// Token: 0x06009559 RID: 38233 RVA: 0x00305B6A File Offset: 0x00303D6A
	public void PauseUpdateScrollHeight(bool pause)
	{
		this.m_PauseUpdateScrollHeight = pause;
	}

	// Token: 0x0600955A RID: 38234 RVA: 0x00305B74 File Offset: 0x00303D74
	public void Enable(bool enable)
	{
		if (this.m_Enabled == enable)
		{
			return;
		}
		this.m_Enabled = enable;
		if (this.m_scrollTrackCover != null)
		{
			this.m_scrollTrackCover.SetActive(!enable);
		}
		this.RefreshShowThumb();
		if (this.m_Enabled)
		{
			this.ResetTouchDrag();
		}
		this.FireEnableScrollEvent();
	}

	// Token: 0x0600955B RID: 38235 RVA: 0x00305BC9 File Offset: 0x00303DC9
	public bool IsEnabled()
	{
		return this.m_Enabled;
	}

	// Token: 0x0600955C RID: 38236 RVA: 0x00305BD1 File Offset: 0x00303DD1
	public bool IsEnabledAndScrollable()
	{
		return this.m_Enabled && this.IsScrollNeeded();
	}

	// Token: 0x0600955D RID: 38237 RVA: 0x00305744 File Offset: 0x00303944
	public float GetScroll()
	{
		return this.m_ScrollValue;
	}

	// Token: 0x0600955E RID: 38238 RVA: 0x00305BE3 File Offset: 0x00303DE3
	public void SaveScroll(string savedName)
	{
		UIBScrollable.s_SavedScrollValues[savedName] = this.m_ScrollValue;
	}

	// Token: 0x0600955F RID: 38239 RVA: 0x00305BF8 File Offset: 0x00303DF8
	public void LoadScroll(string savedName, bool snap)
	{
		float percentage = 0f;
		if (UIBScrollable.s_SavedScrollValues.TryGetValue(savedName, out percentage))
		{
			if (snap)
			{
				this.SetScrollSnap(percentage, true);
			}
			else
			{
				this.SetScroll(percentage, false, true);
			}
			this.ResetTouchDrag();
		}
	}

	// Token: 0x06009560 RID: 38240 RVA: 0x00305C38 File Offset: 0x00303E38
	public bool EnableIfNeeded()
	{
		bool flag = this.IsScrollNeeded();
		this.Enable(flag);
		return flag;
	}

	// Token: 0x06009561 RID: 38241 RVA: 0x00305C54 File Offset: 0x00303E54
	public bool IsScrollNeeded()
	{
		return this.GetTotalWorldScrollHeight() > 0f;
	}

	// Token: 0x06009562 RID: 38242 RVA: 0x00305C64 File Offset: 0x00303E64
	public float PollScrollHeight()
	{
		UIBScrollable.HeightMode heightMode = this.m_HeightMode;
		if (heightMode != UIBScrollable.HeightMode.UseHeightCallback)
		{
			if (heightMode != UIBScrollable.HeightMode.UseScrollableItem)
			{
				return 0f;
			}
			return this.GetScrollableItemsHeight();
		}
		else
		{
			if (this.m_ScrollHeightCallback == null)
			{
				return this.m_PolledScrollHeight;
			}
			return this.m_ScrollHeightCallback();
		}
	}

	// Token: 0x06009563 RID: 38243 RVA: 0x00305CA8 File Offset: 0x00303EA8
	public void SetScroll(float percentage, bool blockInputWhileScrolling = false, bool clamp = true)
	{
		this.SetScroll(percentage, null, blockInputWhileScrolling, clamp);
	}

	// Token: 0x06009564 RID: 38244 RVA: 0x00305CB4 File Offset: 0x00303EB4
	public void SetScroll(float percentage, iTween.EaseType tweenType, float tweenTime, bool blockInputWhileScrolling = false, bool clamp = true)
	{
		this.SetScroll(percentage, null, tweenType, tweenTime, blockInputWhileScrolling, clamp);
	}

	// Token: 0x06009565 RID: 38245 RVA: 0x00305CC4 File Offset: 0x00303EC4
	public void SetScrollSnap(float percentage, bool clamp = true)
	{
		this.SetScrollSnap(percentage, null, clamp);
	}

	// Token: 0x06009566 RID: 38246 RVA: 0x00305CD0 File Offset: 0x00303ED0
	public void SetScroll(float percentage, UIBScrollable.OnScrollComplete scrollComplete, bool blockInputWhileScrolling = false, bool clamp = true)
	{
		base.StartCoroutine(this.SetScrollWait(percentage, scrollComplete, blockInputWhileScrolling, true, null, null, clamp));
	}

	// Token: 0x06009567 RID: 38247 RVA: 0x00305D04 File Offset: 0x00303F04
	public void SetScroll(float percentage, UIBScrollable.OnScrollComplete scrollComplete, iTween.EaseType tweenType, float tweenTime, bool blockInputWhileScrolling = false, bool clamp = true)
	{
		base.StartCoroutine(this.SetScrollWait(percentage, scrollComplete, blockInputWhileScrolling, true, new iTween.EaseType?(tweenType), new float?(tweenTime), clamp));
	}

	// Token: 0x06009568 RID: 38248 RVA: 0x00305D34 File Offset: 0x00303F34
	public void SetScrollSnap(float percentage, UIBScrollable.OnScrollComplete scrollComplete, bool clamp = true)
	{
		this.m_PolledScrollHeight = this.PollScrollHeight();
		this.m_LastScrollHeightRecorded = this.m_PolledScrollHeight;
		this.ScrollTo(percentage, scrollComplete, false, false, null, null, clamp);
		this.ResetTouchDrag();
	}

	// Token: 0x06009569 RID: 38249 RVA: 0x00305D7C File Offset: 0x00303F7C
	public void StopScroll()
	{
		Vector3 scrollAreaStartPos = this.m_ScrollAreaStartPos;
		Vector3 a = scrollAreaStartPos;
		Vector3 totalScrollHeightVector = this.GetTotalScrollHeightVector(true);
		a += totalScrollHeightVector * (this.m_ScrollDirectionReverse ? -1f : 1f);
		Vector3 localPosition = this.m_ScrollObject.transform.localPosition;
		float scrollImmediate = ((a - scrollAreaStartPos).magnitude > float.Epsilon) ? ((localPosition - scrollAreaStartPos).magnitude / (a - scrollAreaStartPos).magnitude) : 0f;
		iTween.Stop(this.m_ScrollObject);
		this.SetScrollImmediate(scrollImmediate);
	}

	// Token: 0x0600956A RID: 38250 RVA: 0x00305E20 File Offset: 0x00304020
	public void SetScrollHeightCallback(UIBScrollable.ScrollHeightCallback dlg, bool refresh = false, bool resetScroll = false)
	{
		float? setResetScroll = null;
		if (resetScroll)
		{
			setResetScroll = new float?(0f);
		}
		this.SetScrollHeightCallback(dlg, setResetScroll, refresh);
	}

	// Token: 0x0600956B RID: 38251 RVA: 0x00305E50 File Offset: 0x00304050
	public void SetScrollHeightCallback(UIBScrollable.ScrollHeightCallback dlg, float? setResetScroll, bool refresh = false)
	{
		this.m_VisibleAffectedObjects.Clear();
		this.m_ScrollHeightCallback = dlg;
		if (setResetScroll != null)
		{
			this.m_ScrollValue = setResetScroll.Value;
			this.ResetTouchDrag();
		}
		if (refresh)
		{
			this.UpdateScroll();
			this.UpdateThumbPosition();
			this.UpdateScrollObjectPosition(true, null, null, null, false);
		}
		this.m_PolledScrollHeight = this.PollScrollHeight();
		this.m_LastScrollHeightRecorded = this.m_PolledScrollHeight;
	}

	// Token: 0x0600956C RID: 38252 RVA: 0x00305ECD File Offset: 0x003040CD
	public void SetHeight(float height)
	{
		this.m_ScrollHeightCallback = null;
		this.m_PolledScrollHeight = height;
		this.UpdateHeight();
	}

	// Token: 0x0600956D RID: 38253 RVA: 0x00305EE3 File Offset: 0x003040E3
	public void UpdateScroll()
	{
		if (!this.m_PauseUpdateScrollHeight)
		{
			this.m_PolledScrollHeight = this.PollScrollHeight();
			this.UpdateHeight();
		}
	}

	// Token: 0x0600956E RID: 38254 RVA: 0x00305F00 File Offset: 0x00304100
	public void CenterWorldPosition(Vector3 position)
	{
		float percentage = this.m_ScrollObject.transform.InverseTransformPoint(position)[(int)this.m_ScrollPlane] / -(this.m_PolledScrollHeight + this.m_ScrollBottomPadding) * 2f - 0.5f;
		base.StartCoroutine(this.BlockInput(this.m_ScrollTweenTime));
		this.SetScroll(percentage, false, true);
	}

	// Token: 0x0600956F RID: 38255 RVA: 0x00305F64 File Offset: 0x00304164
	public bool IsObjectVisibleInScrollArea(GameObject obj, Vector3 extents, bool fullyVisible = false)
	{
		int scrollPlane = (int)this.m_ScrollPlane;
		float num = obj.transform.position[scrollPlane] - extents[scrollPlane];
		float num2 = obj.transform.position[scrollPlane] + extents[scrollPlane];
		Bounds bounds = this.m_ScrollBounds.bounds;
		float num3 = bounds.min[scrollPlane] - this.m_VisibleObjectThreshold;
		float num4 = bounds.max[scrollPlane] + this.m_VisibleObjectThreshold;
		bool flag = num >= num3 && num <= num4;
		bool flag2 = num2 >= num3 && num2 <= num4;
		if (fullyVisible)
		{
			return flag && flag2;
		}
		return flag || flag2;
	}

	// Token: 0x06009570 RID: 38256 RVA: 0x00306028 File Offset: 0x00304228
	public bool CenterObjectInView(GameObject obj, float positionOffset, UIBScrollable.OnScrollComplete scrollComplete, iTween.EaseType tweenType, float tweenTime, bool blockInputWhileScrolling = false)
	{
		float z = this.m_ScrollBounds.bounds.extents.z;
		return this.ScrollObjectIntoView(obj, positionOffset, z, scrollComplete, tweenType, tweenTime, blockInputWhileScrolling);
	}

	// Token: 0x06009571 RID: 38257 RVA: 0x00306060 File Offset: 0x00304260
	public bool ScrollObjectIntoView(GameObject obj, float positionOffset, float axisExtent, UIBScrollable.OnScrollComplete scrollComplete, iTween.EaseType tweenType, float tweenTime, bool blockInputWhileScrolling = false)
	{
		int scrollPlane = (int)this.m_ScrollPlane;
		float num = obj.transform.position[scrollPlane] + positionOffset - axisExtent;
		float num2 = obj.transform.position[scrollPlane] + positionOffset + axisExtent;
		Bounds bounds = this.m_ScrollBounds.bounds;
		float num3 = bounds.min[scrollPlane] - this.m_VisibleObjectThreshold;
		float num4 = bounds.max[scrollPlane] + this.m_VisibleObjectThreshold;
		bool flag = num >= num3;
		bool flag2 = num2 <= num4;
		if (flag && flag2)
		{
			return false;
		}
		float percentage = 0f;
		if (!flag)
		{
			float z = this.GetTotalScrollHeightVector(false).z;
			if (z == 0f)
			{
				Debug.LogWarning("UIBScrollable.ScrollObjectIntoView() - scrollHeight calculated as 0, cannot calculate scroll percentage!");
			}
			else
			{
				float num5 = Mathf.Abs(Math.Abs(num3 - num) / z);
				percentage = this.m_ScrollValue + num5;
			}
		}
		else if (!flag2)
		{
			float z2 = this.GetTotalScrollHeightVector(false).z;
			if (z2 == 0f)
			{
				Debug.LogWarning("UIBScrollable.ScrollObjectIntoView() - scrollHeight calculated as 0, cannot calculate scroll percentage!");
			}
			else
			{
				float num6 = Mathf.Abs(Math.Abs(num4 - num2) / z2);
				percentage = this.m_ScrollValue - num6;
			}
		}
		this.SetScroll(percentage, scrollComplete, tweenType, tweenTime, blockInputWhileScrolling, true);
		return true;
	}

	// Token: 0x06009572 RID: 38258 RVA: 0x003061AA File Offset: 0x003043AA
	public bool IsDragging()
	{
		return (this.m_ScrollThumb != null && this.m_ScrollThumb.IsDragging()) || this.m_TouchBeginScreenPos != null;
	}

	// Token: 0x06009573 RID: 38259 RVA: 0x003061D4 File Offset: 0x003043D4
	public bool IsTouchDragging()
	{
		if (this.m_TouchBeginScreenPos == null)
		{
			return false;
		}
		float num = Mathf.Abs(UniversalInputManager.Get().GetMousePosition().y - this.m_TouchBeginScreenPos.Value.y);
		return this.m_ScrollDragThreshold * ((Screen.dpi > 0f) ? Screen.dpi : 150f) <= num;
	}

	// Token: 0x06009574 RID: 38260 RVA: 0x0030623C File Offset: 0x0030443C
	public void SetScrollImmediate(float percentage)
	{
		this.ScrollTo(percentage, null, false, false, null, new float?(0f), true);
		this.ResetTouchDrag();
	}

	// Token: 0x06009575 RID: 38261 RVA: 0x0030626D File Offset: 0x0030446D
	public void SetScrollImmediate(float percentage, UIBScrollable.OnScrollComplete scrollComplete, bool blockInputWhileScrolling, bool tween, iTween.EaseType? tweenType, float? tweenTime, bool clamp)
	{
		this.ScrollTo(percentage, scrollComplete, blockInputWhileScrolling, tween, tweenType, tweenTime, clamp);
		this.ResetTouchDrag();
	}

	// Token: 0x06009576 RID: 38262 RVA: 0x00306286 File Offset: 0x00304486
	public void SetHideThumb(bool value)
	{
		this.m_overrideHideThumb = value;
		this.RefreshShowThumb();
	}

	// Token: 0x06009577 RID: 38263 RVA: 0x00306298 File Offset: 0x00304498
	private void RefreshShowThumb()
	{
		if (this.m_ScrollThumb != null)
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_ScrollThumb.gameObject.SetActive(false);
				return;
			}
			bool flag = (this.m_Enabled || !this.m_HideThumbWhenDisabled) && !this.m_overrideHideThumb;
			if (flag != this.m_ScrollThumb.gameObject.activeSelf)
			{
				this.m_ScrollThumb.gameObject.SetActive(flag);
				if (flag)
				{
					this.UpdateThumbPosition();
				}
			}
		}
	}

	// Token: 0x06009578 RID: 38264 RVA: 0x0030631B File Offset: 0x0030451B
	private void StartDragging()
	{
		if (this.m_InputBlocked || this.m_Pause || !this.m_Enabled)
		{
			return;
		}
		this.m_ScrollThumb.StartDragging();
	}

	// Token: 0x06009579 RID: 38265 RVA: 0x00306344 File Offset: 0x00304544
	private void UpdateHeight()
	{
		if (Mathf.Abs(this.m_PolledScrollHeight - this.m_LastScrollHeightRecorded) > 0.001f)
		{
			if (!this.EnableIfNeeded())
			{
				this.m_ScrollValue = 0f;
			}
			this.UpdateThumbPosition();
			this.UpdateScrollObjectPosition(false, null, null, null, false);
			this.ResetTouchDrag();
		}
		this.m_LastScrollHeightRecorded = this.m_PolledScrollHeight;
	}

	// Token: 0x0600957A RID: 38266 RVA: 0x003063B0 File Offset: 0x003045B0
	private void DragContent()
	{
		if (UniversalInputManager.Get().GetMouseButtonDown(0))
		{
			if (this.GetWorldTouchPosition() != null)
			{
				this.m_TouchBeginScreenPos = new Vector2?(UniversalInputManager.Get().GetMousePosition());
				return;
			}
		}
		else if (UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			this.m_TouchBeginScreenPos = null;
			this.m_TouchDragBeginWorldPos = null;
			this.FireTouchEndEvent();
		}
		if (this.m_TouchDragBeginWorldPos != null)
		{
			Vector3? worldTouchPositionOnDragArea = this.GetWorldTouchPositionOnDragArea();
			if (worldTouchPositionOnDragArea != null)
			{
				int scrollPlane = (int)this.m_ScrollPlane;
				this.m_LastTouchScrollValue = this.m_ScrollValue;
				float worldDelta = worldTouchPositionOnDragArea.Value[scrollPlane] - this.m_TouchDragBeginWorldPos.Value[scrollPlane];
				float scrollValueDelta = this.GetScrollValueDelta(worldDelta);
				float num = this.m_TouchDragBeginScrollValue + scrollValueDelta;
				float num2 = this.GetOutOfBoundsDist(num);
				if (num2 != 0f)
				{
					num2 = Mathf.Log10(Mathf.Abs(num2) + 1f) * Mathf.Sign(num2);
					num = ((num2 < 0f) ? num2 : (num2 + 1f));
				}
				this.ScrollTo(Mathf.Lerp(this.prevScrollValue, num, 0.9f), null, false, false, null, null, false);
				this.prevScrollValue = num;
				return;
			}
		}
		else if (this.m_TouchBeginScreenPos != null)
		{
			float num3 = Mathf.Abs(UniversalInputManager.Get().GetMousePosition().x - this.m_TouchBeginScreenPos.Value.x);
			float num4 = Mathf.Abs(UniversalInputManager.Get().GetMousePosition().y - this.m_TouchBeginScreenPos.Value.y);
			bool flag = num3 > this.m_DeckTileDragThreshold * ((Screen.dpi > 0f) ? Screen.dpi : 150f);
			bool flag2 = num4 > this.m_ScrollDragThreshold * ((Screen.dpi > 0f) ? Screen.dpi : 150f);
			if (flag && (num3 >= num4 || !flag2))
			{
				this.m_TouchBeginScreenPos = null;
				return;
			}
			if (flag2)
			{
				this.m_TouchDragBeginWorldPos = this.GetWorldTouchPositionOnDragArea();
				this.m_TouchDragBeginScrollValue = this.m_ScrollValue;
				this.m_LastTouchScrollValue = this.m_ScrollValue;
				this.FireTouchStartEvent();
				return;
			}
		}
		else
		{
			float num5 = (this.m_ScrollValue - this.m_LastTouchScrollValue) / Time.fixedDeltaTime;
			float outOfBoundsDist = this.GetOutOfBoundsDist(this.m_ScrollValue);
			if (outOfBoundsDist != 0f)
			{
				if (Mathf.Abs(outOfBoundsDist) >= this.m_MinOutOfBoundsScrollValue)
				{
					float num6 = -this.m_ScrollBoundsSpringK * outOfBoundsDist - Mathf.Sqrt(4f * this.m_ScrollBoundsSpringK) * num5;
					num5 += num6 * Time.fixedDeltaTime;
					this.m_LastTouchScrollValue = this.m_ScrollValue;
					this.ScrollTo(this.m_ScrollValue + num5 * Time.fixedDeltaTime, null, false, false, null, null, false);
				}
				if (Mathf.Abs(this.GetOutOfBoundsDist(this.m_ScrollValue)) < this.m_MinOutOfBoundsScrollValue)
				{
					this.ScrollTo(Mathf.Round(this.m_ScrollValue), null, false, false, null, null, false);
					this.m_LastTouchScrollValue = this.m_ScrollValue;
				}
				this.prevScrollValue = this.m_ScrollValue;
				return;
			}
			if (this.m_LastTouchScrollValue != this.m_ScrollValue)
			{
				float num7 = Mathf.Sign(num5);
				num5 -= num7 * this.m_KineticScrollFriction * Time.fixedDeltaTime;
				this.m_LastTouchScrollValue = this.m_ScrollValue;
				if (Mathf.Abs(num5) >= this.m_MinKineticScrollSpeed && Mathf.Sign(num5) == num7)
				{
					this.ScrollTo(this.m_ScrollValue + num5 * Time.fixedDeltaTime, null, false, false, null, null, false);
				}
			}
		}
	}

	// Token: 0x0600957B RID: 38267 RVA: 0x00306780 File Offset: 0x00304980
	private void DragThumb()
	{
		Vector3 min = this.m_ScrollTrack.bounds.min;
		Camera camera = CameraUtils.FindFirstByLayer(this.m_ScrollTrack.gameObject.layer);
		Plane plane = new Plane(-camera.transform.forward, min);
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		float distance;
		if (plane.Raycast(ray, out distance))
		{
			Vector3 point = ray.GetPoint(distance);
			float scrollTrackTop1D = this.GetScrollTrackTop1D();
			float scrollTrackBtm1D = this.GetScrollTrackBtm1D();
			float num = Mathf.Clamp01((point[(int)this.m_ScrollPlane] - scrollTrackTop1D) / (scrollTrackBtm1D - scrollTrackTop1D));
			if (Mathf.Abs(this.m_ScrollValue - num) > Mathf.Epsilon)
			{
				this.m_ScrollValue = num;
				this.UpdateThumbPosition();
				this.UpdateScrollObjectPosition(false, null, null, null, false);
			}
		}
		this.ResetTouchDrag();
	}

	// Token: 0x0600957C RID: 38268 RVA: 0x0030686C File Offset: 0x00304A6C
	private void ResetTouchDrag()
	{
		bool flag = this.m_TouchDragBeginWorldPos != null;
		this.m_TouchBeginScreenPos = null;
		this.m_TouchDragBeginWorldPos = null;
		this.m_TouchDragBeginScrollValue = this.m_ScrollValue;
		this.m_LastTouchScrollValue = this.m_ScrollValue;
		if (flag)
		{
			this.FireTouchEndEvent();
		}
	}

	// Token: 0x0600957D RID: 38269 RVA: 0x003068BC File Offset: 0x00304ABC
	private float GetScrollTrackTop1D()
	{
		return this.GetScrollTrackTop()[(int)this.m_ScrollPlane];
	}

	// Token: 0x0600957E RID: 38270 RVA: 0x003068E0 File Offset: 0x00304AE0
	private float GetScrollTrackBtm1D()
	{
		return this.GetScrollTrackBtm()[(int)this.m_ScrollPlane];
	}

	// Token: 0x0600957F RID: 38271 RVA: 0x00306904 File Offset: 0x00304B04
	private Vector3 GetScrollTrackTop()
	{
		if (this.m_ScrollTrack == null)
		{
			return Vector3.zero;
		}
		if (this.m_ScrollPlane == UIBScrollable.ScrollDirection.X)
		{
			return this.m_ScrollTrack.bounds.min;
		}
		return this.m_ScrollTrack.bounds.max;
	}

	// Token: 0x06009580 RID: 38272 RVA: 0x00306954 File Offset: 0x00304B54
	private Vector3 GetScrollTrackBtm()
	{
		if (this.m_ScrollTrack == null)
		{
			return Vector3.zero;
		}
		if (this.m_ScrollPlane == UIBScrollable.ScrollDirection.X)
		{
			return this.m_ScrollTrack.bounds.max;
		}
		return this.m_ScrollTrack.bounds.min;
	}

	// Token: 0x06009581 RID: 38273 RVA: 0x003069A4 File Offset: 0x00304BA4
	private void AddScroll(float amount)
	{
		this.ScrollTo(this.m_ScrollValue + amount, null, false, true, null, null, true);
		this.ResetTouchDrag();
	}

	// Token: 0x06009582 RID: 38274 RVA: 0x003069DB File Offset: 0x00304BDB
	private void ScrollTo(float percentage, UIBScrollable.OnScrollComplete scrollComplete, bool blockInputWhileScrolling, bool tween, iTween.EaseType? tweenType, float? tweenTime, bool clamp)
	{
		this.m_ScrollValue = (clamp ? Mathf.Clamp01(percentage) : percentage);
		this.UpdateThumbPosition();
		this.UpdateScrollObjectPosition(tween, scrollComplete, tweenType, tweenTime, blockInputWhileScrolling);
	}

	// Token: 0x06009583 RID: 38275 RVA: 0x00306A04 File Offset: 0x00304C04
	private void UpdateThumbPosition()
	{
		if (this.m_ScrollThumb == null)
		{
			return;
		}
		Vector3 scrollTrackTop = this.GetScrollTrackTop();
		Vector3 scrollTrackBtm = this.GetScrollTrackBtm();
		float num = scrollTrackTop[(int)this.m_ScrollPlane];
		float num2 = scrollTrackBtm[(int)this.m_ScrollPlane];
		Vector3 position = scrollTrackTop + (scrollTrackBtm - scrollTrackTop) * 0.5f;
		position[(int)this.m_ScrollPlane] = num + (num2 - num) * Mathf.Clamp01(this.m_ScrollValue);
		this.m_ScrollThumb.transform.position = position;
		if (this.m_ScrollPlane == UIBScrollable.ScrollDirection.Z)
		{
			Vector3 localPosition = this.m_ScrollThumb.transform.localPosition;
			this.m_ScrollThumb.transform.localPosition = new Vector3(localPosition.x, this.m_ScrollThumbStartYPos, localPosition.z);
		}
	}

	// Token: 0x06009584 RID: 38276 RVA: 0x00306AD8 File Offset: 0x00304CD8
	private void UpdateScrollObjectPosition(bool tween, UIBScrollable.OnScrollComplete scrollComplete, iTween.EaseType? tweenType, float? tweenTime, bool blockInputWhileScrolling = false)
	{
		if (this.m_ScrollObject == null)
		{
			return;
		}
		Vector3 scrollAreaStartPos = this.m_ScrollAreaStartPos;
		Vector3 a = scrollAreaStartPos;
		Vector3 totalScrollHeightVector = this.GetTotalScrollHeightVector(true);
		a += totalScrollHeightVector * (this.m_ScrollDirectionReverse ? -1f : 1f);
		Vector3 vector = scrollAreaStartPos + this.m_ScrollValue * (a - scrollAreaStartPos);
		if (float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z))
		{
			return;
		}
		if (tween)
		{
			iTween.MoveTo(this.m_ScrollObject, iTween.Hash(new object[]
			{
				"position",
				vector,
				"time",
				(tweenTime != null) ? tweenTime.Value : this.m_ScrollTweenTime,
				"isLocal",
				true,
				"easetype",
				(tweenType != null) ? tweenType.Value : this.m_ScrollEaseType,
				"onupdate",
				new Action<object>(delegate(object newVal)
				{
					this.UpdateAndFireVisibleAffectedObjects();
				}),
				"oncomplete",
				new Action<object>(delegate(object newVal)
				{
					this.UpdateAndFireVisibleAffectedObjects();
					if (scrollComplete != null)
					{
						scrollComplete(this.m_ScrollValue);
					}
				})
			}));
			return;
		}
		if (this.m_ScrollPlane == UIBScrollable.ScrollDirection.Z)
		{
			this.m_ScrollObject.transform.localPosition = new Vector3(vector.x, this.m_ScrollObject.transform.localPosition.y, vector.z);
		}
		else
		{
			this.m_ScrollObject.transform.localPosition = vector;
		}
		this.UpdateAndFireVisibleAffectedObjects();
		if (scrollComplete != null)
		{
			scrollComplete(this.m_ScrollValue);
		}
	}

	// Token: 0x06009585 RID: 38277 RVA: 0x00306CB8 File Offset: 0x00304EB8
	private IEnumerator SetScrollWait(float percentage, UIBScrollable.OnScrollComplete scrollComplete, bool blockInputWhileScrolling, bool tween, iTween.EaseType? tweenType, float? tweenTime, bool clamp)
	{
		yield return null;
		this.ScrollTo(percentage, scrollComplete, blockInputWhileScrolling, tween, tweenType, tweenTime, clamp);
		this.ResetTouchDrag();
		yield break;
	}

	// Token: 0x06009586 RID: 38278 RVA: 0x00306D07 File Offset: 0x00304F07
	private IEnumerator BlockInput(float blockTime)
	{
		this.m_InputBlocked = true;
		yield return new WaitForSeconds(blockTime);
		this.m_InputBlocked = false;
		yield break;
	}

	// Token: 0x06009587 RID: 38279 RVA: 0x00306D20 File Offset: 0x00304F20
	private Vector3 GetTotalScrollHeightVector(bool convertToLocalSpace = false)
	{
		if (this.m_ScrollObject == null)
		{
			Log.All.PrintWarning("GetTotalScrollHeightVector() returning zero. m_ScrollObject is null!", Array.Empty<object>());
			return Vector3.zero;
		}
		float num = this.m_PolledScrollHeight - this.GetScrollBoundsHeight();
		if (num < 0f)
		{
			return Vector3.zero;
		}
		Vector3 vector = Vector3.zero;
		vector[(int)this.m_ScrollPlane] = num;
		if (convertToLocalSpace)
		{
			vector = this.m_ScrollObject.transform.parent.worldToLocalMatrix * vector;
		}
		if (this.m_ScrollBottomPadding > 0f)
		{
			vector += vector.normalized * this.m_ScrollBottomPadding;
		}
		return vector;
	}

	// Token: 0x06009588 RID: 38280 RVA: 0x00306DD8 File Offset: 0x00304FD8
	private float GetTotalWorldScrollHeight()
	{
		return this.GetTotalScrollHeightVector(false).magnitude;
	}

	// Token: 0x06009589 RID: 38281 RVA: 0x00306DF4 File Offset: 0x00304FF4
	private Vector3? GetWorldTouchPosition()
	{
		return this.GetWorldTouchPosition(this.m_ScrollBounds);
	}

	// Token: 0x0600958A RID: 38282 RVA: 0x00306E04 File Offset: 0x00305004
	private Vector3? GetWorldTouchPositionOnDragArea()
	{
		Vector3? result = null;
		if (this.m_TouchDragFullArea != null)
		{
			result = this.GetWorldTouchPosition(this.m_TouchDragFullArea);
		}
		if (result == null && this.m_ScrollBounds != null)
		{
			result = this.GetWorldTouchPosition(this.m_ScrollBounds);
		}
		return result;
	}

	// Token: 0x0600958B RID: 38283 RVA: 0x00306E5C File Offset: 0x0030505C
	private Vector3? GetWorldTouchPosition(BoxCollider bounds)
	{
		Camera scrollCamera = this.GetScrollCamera();
		if (scrollCamera == null)
		{
			Vector3? result = null;
			return result;
		}
		Ray ray = scrollCamera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		RaycastHit raycastHit;
		using (List<BoxCollider>.Enumerator enumerator = this.m_TouchScrollBlockers.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Raycast(ray, out raycastHit, 3.4028235E+38f))
				{
					Vector3? result = null;
					return result;
				}
			}
		}
		if (this.IsInputOverScrollableArea(bounds, out raycastHit))
		{
			return new Vector3?(ray.GetPoint(raycastHit.distance));
		}
		return null;
	}

	// Token: 0x0600958C RID: 38284 RVA: 0x00306F1C File Offset: 0x0030511C
	private float GetScrollValueDelta(float worldDelta)
	{
		return this.m_ScrollDeltaMultiplier * worldDelta / this.GetTotalWorldScrollHeight();
	}

	// Token: 0x0600958D RID: 38285 RVA: 0x00306F2D File Offset: 0x0030512D
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

	// Token: 0x0600958E RID: 38286 RVA: 0x00306F50 File Offset: 0x00305150
	private void FireEnableScrollEvent()
	{
		UIBScrollable.EnableScroll[] array = this.m_EnableScrollListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this.m_Enabled);
		}
	}

	// Token: 0x0600958F RID: 38287 RVA: 0x00306F88 File Offset: 0x00305188
	public void UpdateAndFireVisibleAffectedObjects()
	{
		foreach (UIBScrollable.VisibleAffectedObject visibleAffectedObject in this.m_VisibleAffectedObjects.ToArray())
		{
			bool flag = this.IsObjectVisibleInScrollArea(visibleAffectedObject.Obj, visibleAffectedObject.Extents, false) || this.m_ForceShowVisibleAffectedObjects;
			if (flag != visibleAffectedObject.Visible)
			{
				visibleAffectedObject.Visible = flag;
				visibleAffectedObject.Callback(visibleAffectedObject.Obj, flag);
			}
		}
	}

	// Token: 0x06009590 RID: 38288 RVA: 0x00306FF4 File Offset: 0x003051F4
	private float GetScrollBoundsHeight()
	{
		if (this.m_ScrollObject == null)
		{
			Debug.LogWarning("No m_ScrollObject set for this UIBScrollable!");
			return 0f;
		}
		return this.m_ScrollBounds.bounds.size[(int)this.m_ScrollPlane];
	}

	// Token: 0x06009591 RID: 38289 RVA: 0x00307040 File Offset: 0x00305240
	private void FireTouchStartEvent()
	{
		UIBScrollable.OnTouchScrollStarted[] array = this.m_TouchScrollStartedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x06009592 RID: 38290 RVA: 0x00307070 File Offset: 0x00305270
	private void FireTouchEndEvent()
	{
		UIBScrollable.OnTouchScrollEnded[] array = this.m_TouchScrollEndedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x06009593 RID: 38291 RVA: 0x003070A0 File Offset: 0x003052A0
	private float GetScrollableItemsHeight()
	{
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		if (this.GetScrollableItemsMinMax(ref zero, ref zero2) == null)
		{
			return 0f;
		}
		int scrollPlane = (int)this.m_ScrollPlane;
		return zero2[scrollPlane] - zero[scrollPlane];
	}

	// Token: 0x06009594 RID: 38292 RVA: 0x003070E4 File Offset: 0x003052E4
	private UIBScrollableItem[] GetScrollableItemsMinMax(ref Vector3 min, ref Vector3 max)
	{
		if (this.m_ScrollObject == null)
		{
			return null;
		}
		UIBScrollableItem[] componentsInChildren = this.m_ScrollObject.GetComponentsInChildren<UIBScrollableItem>(true);
		if (componentsInChildren == null || componentsInChildren.Length == 0)
		{
			return null;
		}
		min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
		foreach (UIBScrollableItem uibscrollableItem in componentsInChildren)
		{
			if (uibscrollableItem.IsActive())
			{
				Vector3 vector;
				Vector3 vector2;
				uibscrollableItem.GetWorldBounds(out vector, out vector2);
				min.x = Math.Min(min.x, Math.Min(vector.x, vector2.x));
				min.y = Math.Min(min.y, Math.Min(vector.y, vector2.y));
				min.z = Math.Min(min.z, Math.Min(vector.z, vector2.z));
				max.x = Math.Max(max.x, Math.Max(vector.x, vector2.x));
				max.y = Math.Max(max.y, Math.Max(vector.y, vector2.y));
				max.z = Math.Max(max.z, Math.Max(vector.z, vector2.z));
			}
		}
		return componentsInChildren;
	}

	// Token: 0x06009595 RID: 38293 RVA: 0x00090064 File Offset: 0x0008E264
	private BoxCollider[] GetBoxCollidersMinMax(ref Vector3 min, ref Vector3 max)
	{
		return null;
	}

	// Token: 0x06009596 RID: 38294 RVA: 0x00307254 File Offset: 0x00305454
	private Camera GetScrollCamera()
	{
		if (this.m_UseCameraFromLayer)
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

	// Token: 0x06009597 RID: 38295 RVA: 0x00307291 File Offset: 0x00305491
	private void SaveScrollThumbStartHeight()
	{
		if (this.m_ScrollThumb != null)
		{
			this.m_ScrollThumbStartYPos = this.m_ScrollThumb.transform.localPosition.y;
		}
	}

	// Token: 0x04007D2B RID: 32043
	[CustomEditField(Sections = "Camera Settings")]
	public bool m_UseCameraFromLayer;

	// Token: 0x04007D2C RID: 32044
	[CustomEditField(Sections = "Preferences")]
	public float m_ScrollWheelAmount = 0.1f;

	// Token: 0x04007D2D RID: 32045
	[CustomEditField(Sections = "Preferences")]
	public UIBScrollable.ScrollWheelMode m_ScrollWheelMode;

	// Token: 0x04007D2E RID: 32046
	[CustomEditField(Sections = "Preferences")]
	public float m_ScrollBottomPadding;

	// Token: 0x04007D2F RID: 32047
	[CustomEditField(Sections = "Preferences")]
	public iTween.EaseType m_ScrollEaseType = iTween.Defaults.easeType;

	// Token: 0x04007D30 RID: 32048
	[CustomEditField(Sections = "Preferences")]
	public float m_ScrollTweenTime = 0.2f;

	// Token: 0x04007D31 RID: 32049
	[CustomEditField(Sections = "Preferences")]
	public UIBScrollable.ScrollDirection m_ScrollPlane = UIBScrollable.ScrollDirection.Z;

	// Token: 0x04007D32 RID: 32050
	[CustomEditField(Sections = "Preferences")]
	public bool m_ScrollDirectionReverse;

	// Token: 0x04007D33 RID: 32051
	[CustomEditField(Sections = "Preferences")]
	[Tooltip("If scrolling is active, all PegUI calls will be suppressed")]
	public bool m_OverridePegUI;

	// Token: 0x04007D34 RID: 32052
	[CustomEditField(Sections = "Preferences")]
	public bool m_ForceScrollAreaHitTest;

	// Token: 0x04007D35 RID: 32053
	[CustomEditField(Sections = "Preferences")]
	public bool m_ScrollOnMouseDrag;

	// Token: 0x04007D36 RID: 32054
	[CustomEditField(Sections = "Bounds Settings")]
	public BoxCollider m_ScrollBounds;

	// Token: 0x04007D37 RID: 32055
	[CustomEditField(Sections = "Optional Bounds Settings")]
	public BoxCollider m_TouchDragFullArea;

	// Token: 0x04007D38 RID: 32056
	[CustomEditField(Sections = "Thumb Settings")]
	public BoxCollider m_ScrollTrack;

	// Token: 0x04007D39 RID: 32057
	[CustomEditField(Sections = "Thumb Settings")]
	public ScrollBarThumb m_ScrollThumb;

	// Token: 0x04007D3A RID: 32058
	[CustomEditField(Sections = "Thumb Settings")]
	public bool m_HideThumbWhenDisabled;

	// Token: 0x04007D3B RID: 32059
	[CustomEditField(Sections = "Thumb Settings")]
	public GameObject m_scrollTrackCover;

	// Token: 0x04007D3C RID: 32060
	[CustomEditField(Sections = "Bounds Settings")]
	public GameObject m_ScrollObject;

	// Token: 0x04007D3D RID: 32061
	[CustomEditField(Sections = "Bounds Settings")]
	public float m_VisibleObjectThreshold;

	// Token: 0x04007D3E RID: 32062
	[CustomEditField(Sections = "Preferences")]
	public bool m_UseScrollContentsInHitTest = true;

	// Token: 0x04007D3F RID: 32063
	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Drag distance required to initiate deck tile dragging (inches)")]
	public float m_DeckTileDragThreshold = 0.04f;

	// Token: 0x04007D40 RID: 32064
	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Drag distance required to initiate scroll dragging (inches)")]
	public float m_ScrollDragThreshold = 0.04f;

	// Token: 0x04007D41 RID: 32065
	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Stopping speed for scrolling after the user has let go")]
	public float m_MinKineticScrollSpeed = 0.01f;

	// Token: 0x04007D42 RID: 32066
	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Resistance for slowing down scrolling after the user has let go")]
	public float m_KineticScrollFriction = 6f;

	// Token: 0x04007D43 RID: 32067
	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Strength of the boundary springs")]
	public float m_ScrollBoundsSpringK = 700f;

	// Token: 0x04007D44 RID: 32068
	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Distance at which the out-of-bounds scroll value will snapped to 0 or 1")]
	public float m_MinOutOfBoundsScrollValue = 0.001f;

	// Token: 0x04007D45 RID: 32069
	[CustomEditField(Sections = "Touch Settings")]
	[Tooltip("Use this to match scaling issues.")]
	public float m_ScrollDeltaMultiplier = 1f;

	// Token: 0x04007D46 RID: 32070
	[CustomEditField(Sections = "Touch Settings")]
	public List<BoxCollider> m_TouchScrollBlockers = new List<BoxCollider>();

	// Token: 0x04007D47 RID: 32071
	public UIBScrollable.HeightMode m_HeightMode = UIBScrollable.HeightMode.UseScrollableItem;

	// Token: 0x04007D48 RID: 32072
	private bool m_Enabled = true;

	// Token: 0x04007D49 RID: 32073
	private float m_ScrollValue;

	// Token: 0x04007D4A RID: 32074
	private float m_LastTouchScrollValue;

	// Token: 0x04007D4B RID: 32075
	private bool m_InputBlocked;

	// Token: 0x04007D4C RID: 32076
	private bool m_Pause;

	// Token: 0x04007D4D RID: 32077
	private bool m_PauseUpdateScrollHeight;

	// Token: 0x04007D4E RID: 32078
	private bool m_overrideHideThumb;

	// Token: 0x04007D4F RID: 32079
	private Vector2? m_TouchBeginScreenPos;

	// Token: 0x04007D50 RID: 32080
	private Vector3? m_TouchDragBeginWorldPos;

	// Token: 0x04007D51 RID: 32081
	private float m_TouchDragBeginScrollValue;

	// Token: 0x04007D52 RID: 32082
	private float prevScrollValue;

	// Token: 0x04007D53 RID: 32083
	private Vector3 m_ScrollAreaStartPos;

	// Token: 0x04007D54 RID: 32084
	private float m_ScrollThumbStartYPos;

	// Token: 0x04007D55 RID: 32085
	private UIBScrollable.ScrollHeightCallback m_ScrollHeightCallback;

	// Token: 0x04007D56 RID: 32086
	private List<UIBScrollable.EnableScroll> m_EnableScrollListeners = new List<UIBScrollable.EnableScroll>();

	// Token: 0x04007D57 RID: 32087
	private float m_LastScrollHeightRecorded;

	// Token: 0x04007D58 RID: 32088
	private float m_PolledScrollHeight;

	// Token: 0x04007D59 RID: 32089
	private List<UIBScrollable.VisibleAffectedObject> m_VisibleAffectedObjects = new List<UIBScrollable.VisibleAffectedObject>();

	// Token: 0x04007D5A RID: 32090
	private List<UIBScrollable.OnTouchScrollStarted> m_TouchScrollStartedListeners = new List<UIBScrollable.OnTouchScrollStarted>();

	// Token: 0x04007D5B RID: 32091
	private List<UIBScrollable.OnTouchScrollEnded> m_TouchScrollEndedListeners = new List<UIBScrollable.OnTouchScrollEnded>();

	// Token: 0x04007D5C RID: 32092
	private bool m_ForceShowVisibleAffectedObjects;

	// Token: 0x04007D5D RID: 32093
	private static Map<string, float> s_SavedScrollValues = new Map<string, float>();

	// Token: 0x02002730 RID: 10032
	public enum ScrollDirection
	{
		// Token: 0x0400F39F RID: 62367
		X,
		// Token: 0x0400F3A0 RID: 62368
		Y,
		// Token: 0x0400F3A1 RID: 62369
		Z
	}

	// Token: 0x02002731 RID: 10033
	public enum HeightMode
	{
		// Token: 0x0400F3A3 RID: 62371
		UseHeightCallback,
		// Token: 0x0400F3A4 RID: 62372
		UseScrollableItem,
		// Token: 0x0400F3A5 RID: 62373
		UseBoxCollider
	}

	// Token: 0x02002732 RID: 10034
	public enum ScrollWheelMode
	{
		// Token: 0x0400F3A7 RID: 62375
		ScaledToScrollSize,
		// Token: 0x0400F3A8 RID: 62376
		FixedRate
	}

	// Token: 0x02002733 RID: 10035
	public interface IContent
	{
		// Token: 0x17002CC0 RID: 11456
		// (get) Token: 0x06013922 RID: 80162
		UIBScrollable Scrollable { get; }
	}

	// Token: 0x02002734 RID: 10036
	private class ContentComponent : MonoBehaviour, UIBScrollable.IContent
	{
		// Token: 0x17002CC1 RID: 11457
		// (get) Token: 0x06013923 RID: 80163 RVA: 0x0053806B File Offset: 0x0053626B
		// (set) Token: 0x06013924 RID: 80164 RVA: 0x00538073 File Offset: 0x00536273
		public UIBScrollable Scrollable { get; set; }
	}

	// Token: 0x02002735 RID: 10037
	// (Invoke) Token: 0x06013927 RID: 80167
	public delegate void EnableScroll(bool enabled);

	// Token: 0x02002736 RID: 10038
	// (Invoke) Token: 0x0601392B RID: 80171
	public delegate float ScrollHeightCallback();

	// Token: 0x02002737 RID: 10039
	// (Invoke) Token: 0x0601392F RID: 80175
	public delegate void ScrollTurnedOn(bool on);

	// Token: 0x02002738 RID: 10040
	// (Invoke) Token: 0x06013933 RID: 80179
	public delegate void OnScrollComplete(float percentage);

	// Token: 0x02002739 RID: 10041
	// (Invoke) Token: 0x06013937 RID: 80183
	public delegate void OnTouchScrollStarted();

	// Token: 0x0200273A RID: 10042
	// (Invoke) Token: 0x0601393B RID: 80187
	public delegate void OnTouchScrollEnded();

	// Token: 0x0200273B RID: 10043
	// (Invoke) Token: 0x0601393F RID: 80191
	public delegate void VisibleAffected(GameObject obj, bool visible);

	// Token: 0x0200273C RID: 10044
	protected class VisibleAffectedObject
	{
		// Token: 0x0400F3AA RID: 62378
		public GameObject Obj;

		// Token: 0x0400F3AB RID: 62379
		public Vector3 Extents;

		// Token: 0x0400F3AC RID: 62380
		public bool Visible;

		// Token: 0x0400F3AD RID: 62381
		public UIBScrollable.VisibleAffected Callback;
	}
}
