using System;
using UnityEngine;

// Token: 0x02000AE2 RID: 2786
public class SwipeHandler : PegUICustomBehavior
{
	// Token: 0x06009472 RID: 38002 RVA: 0x0030202E File Offset: 0x0030022E
	public override bool UpdateUI()
	{
		return UniversalInputManager.Get().IsTouchMode() && this.HandleSwipeGesture();
	}

	// Token: 0x06009473 RID: 38003 RVA: 0x00302044 File Offset: 0x00300244
	private bool InSwipeRect(Vector2 v)
	{
		return v.x >= this.m_swipeRect.x && v.x <= this.m_swipeRect.x + this.m_swipeRect.width && v.y >= this.m_swipeRect.y && v.y <= this.m_swipeRect.y + this.m_swipeRect.height;
	}

	// Token: 0x06009474 RID: 38004 RVA: 0x003020BC File Offset: 0x003002BC
	private bool CheckSwipe()
	{
		float num = this.m_swipeStart.x - UniversalInputManager.Get().GetMousePosition().x;
		float num2 = 0.09f + ((this.m_swipeElement != null) ? 0.035f : 0f);
		float num3 = (float)Screen.width * num2;
		if (Mathf.Abs(num) > num3)
		{
			SwipeHandler.SWIPE_DIRECTION direction;
			if (num < 0f)
			{
				direction = SwipeHandler.SWIPE_DIRECTION.RIGHT;
			}
			else
			{
				direction = SwipeHandler.SWIPE_DIRECTION.LEFT;
			}
			if (SwipeHandler.m_swipeListener != null)
			{
				SwipeHandler.m_swipeListener(direction);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06009475 RID: 38005 RVA: 0x0030213C File Offset: 0x0030033C
	private bool HandleSwipeGesture()
	{
		this.m_swipeRect = CameraUtils.CreateGUIScreenRect(Camera.main, this.m_upperLeftBone, this.m_lowerRightBone);
		if (UniversalInputManager.Get().GetMouseButtonDown(0) && this.InSwipeRect(UniversalInputManager.Get().GetMousePosition()))
		{
			this.m_checkingPossibleSwipe = true;
			this.m_swipeDetectTimer = 0f;
			this.m_swipeStart = UniversalInputManager.Get().GetMousePosition();
			this.m_swipeElement = PegUI.Get().FindHitElement();
			return true;
		}
		if (this.m_checkingPossibleSwipe)
		{
			this.m_swipeDetectTimer += Time.deltaTime;
			if (UniversalInputManager.Get().GetMouseButtonUp(0))
			{
				this.m_checkingPossibleSwipe = false;
				if (!this.CheckSwipe() && this.m_swipeElement != null && this.m_swipeElement == PegUI.Get().FindHitElement())
				{
					this.m_swipeElement.TriggerPress();
					this.m_swipeElement.TriggerRelease();
				}
				return true;
			}
			if (this.m_swipeDetectTimer < 0.1f)
			{
				return true;
			}
			this.m_checkingPossibleSwipe = false;
			if (this.CheckSwipe())
			{
				return true;
			}
			if (this.m_swipeElement != null)
			{
				PegUI.Get().DoMouseDown(this.m_swipeElement, this.m_swipeStart);
			}
		}
		return false;
	}

	// Token: 0x06009476 RID: 38006 RVA: 0x0030227A File Offset: 0x0030047A
	public void RegisterSwipeListener(SwipeHandler.DelSwipeListener listener)
	{
		SwipeHandler.m_swipeListener = listener;
	}

	// Token: 0x04007C63 RID: 31843
	public Transform m_upperLeftBone;

	// Token: 0x04007C64 RID: 31844
	public Transform m_lowerRightBone;

	// Token: 0x04007C65 RID: 31845
	private const float SWIPE_DETECT_DURATION = 0.1f;

	// Token: 0x04007C66 RID: 31846
	private const float SWIPE_DETECT_WIDTH = 0.09f;

	// Token: 0x04007C67 RID: 31847
	private const float SWIPE_FROM_TARGET_PENALTY = 0.035f;

	// Token: 0x04007C68 RID: 31848
	private float m_swipeDetectTimer;

	// Token: 0x04007C69 RID: 31849
	private bool m_checkingPossibleSwipe;

	// Token: 0x04007C6A RID: 31850
	private Vector3 m_swipeStart;

	// Token: 0x04007C6B RID: 31851
	private PegUIElement m_swipeElement;

	// Token: 0x04007C6C RID: 31852
	private Rect m_swipeRect;

	// Token: 0x04007C6D RID: 31853
	private static SwipeHandler.DelSwipeListener m_swipeListener;

	// Token: 0x0200271A RID: 10010
	public enum SWIPE_DIRECTION
	{
		// Token: 0x0400F367 RID: 62311
		RIGHT,
		// Token: 0x0400F368 RID: 62312
		LEFT
	}

	// Token: 0x0200271B RID: 10011
	// (Invoke) Token: 0x060138E4 RID: 80100
	public delegate void DelSwipeListener(SwipeHandler.SWIPE_DIRECTION direction);
}
