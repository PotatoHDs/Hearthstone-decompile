using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class CollectionPageManagerTouchBehavior : PegUICustomBehavior
{
	// Token: 0x06001258 RID: 4696 RVA: 0x000690B8 File Offset: 0x000672B8
	protected override void Awake()
	{
		base.Awake();
		BookPageManager component = base.GetComponent<BookPageManager>();
		this.pageLeftRegion = component.m_pageLeftClickableRegion;
		this.pageRightRegion = component.m_pageRightClickableRegion;
		this.pageDragRegion = component.m_pageDraggableRegion;
		this.pageDragRegion.gameObject.SetActive(true);
		this.pageDragRegion.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnPageDraggableRegionDown));
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x00069120 File Offset: 0x00067320
	protected override void OnDestroy()
	{
		this.pageDragRegion.gameObject.SetActive(false);
		this.pageDragRegion.RemoveEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnPageDraggableRegionDown));
		base.OnDestroy();
	}

	// Token: 0x0600125A RID: 4698 RVA: 0x00069154 File Offset: 0x00067354
	public override bool UpdateUI()
	{
		if ((CollectionInputMgr.Get() != null && CollectionInputMgr.Get().HasHeldCard()) || (CraftingManager.Get() != null && CraftingManager.Get().IsCardShowing()))
		{
			return false;
		}
		bool flag = false;
		if (UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			flag = (this.swipeState == CollectionPageManagerTouchBehavior.SwipeState.Success);
			this.swipeState = CollectionPageManagerTouchBehavior.SwipeState.None;
		}
		return this.swipeState != CollectionPageManagerTouchBehavior.SwipeState.None || flag;
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x000691C0 File Offset: 0x000673C0
	private void OnPageDraggableRegionDown(UIEvent e)
	{
		if (base.gameObject == null)
		{
			return;
		}
		this.TryStartPageTurnGesture();
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x000691D7 File Offset: 0x000673D7
	private void TryStartPageTurnGesture()
	{
		if (this.swipeState == CollectionPageManagerTouchBehavior.SwipeState.Update)
		{
			return;
		}
		base.StartCoroutine(this.HandlePageTurnGesture());
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x000691F0 File Offset: 0x000673F0
	private Vector2 GetTouchPosition()
	{
		Vector3 touchPosition = HearthstoneServices.Get<ITouchScreenService>().GetTouchPosition();
		return new Vector2(touchPosition.x, touchPosition.y);
	}

	// Token: 0x0600125E RID: 4702 RVA: 0x00069219 File Offset: 0x00067419
	private IEnumerator HandlePageTurnGesture()
	{
		if (!UniversalInputManager.Get().IsTouchMode())
		{
			yield return null;
		}
		this.m_swipeStartPosition = this.GetTouchPosition();
		this.swipeState = CollectionPageManagerTouchBehavior.SwipeState.Update;
		float pixelTurnDist = Mathf.Clamp(this.TurnDist * (float)Screen.currentResolution.width, 2f, 300f);
		PegUIElement touchDownPageTurnRegion = this.HitTestPageTurnRegions();
		while (!UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			float x = (this.GetTouchPosition() - this.m_swipeStartPosition).x;
			if (x <= -pixelTurnDist && this.pageRightRegion.enabled)
			{
				this.pageRightRegion.TriggerRelease();
				this.swipeState = CollectionPageManagerTouchBehavior.SwipeState.Success;
				yield break;
			}
			if (x >= pixelTurnDist && this.pageLeftRegion.enabled)
			{
				this.pageLeftRegion.TriggerRelease();
				this.swipeState = CollectionPageManagerTouchBehavior.SwipeState.Success;
				yield break;
			}
			yield return null;
		}
		if (touchDownPageTurnRegion != null && touchDownPageTurnRegion == this.HitTestPageTurnRegions())
		{
			touchDownPageTurnRegion.TriggerRelease();
		}
		this.swipeState = CollectionPageManagerTouchBehavior.SwipeState.None;
		yield break;
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x00069228 File Offset: 0x00067428
	private PegUIElement HitTestPageTurnRegions()
	{
		PegUIElement pegUIElement = null;
		this.pageDragRegion.GetComponent<Collider>().enabled = false;
		RaycastHit raycastHit;
		if (UniversalInputManager.Get().GetInputHitInfo(out raycastHit))
		{
			pegUIElement = raycastHit.collider.GetComponent<PegUIElement>();
			if (pegUIElement != this.pageLeftRegion && pegUIElement != this.pageRightRegion)
			{
				pegUIElement = null;
			}
		}
		this.pageDragRegion.GetComponent<Collider>().enabled = true;
		return pegUIElement;
	}

	// Token: 0x04000BB4 RID: 2996
	private float TurnDist = 0.07f;

	// Token: 0x04000BB5 RID: 2997
	private PegUIElement pageLeftRegion;

	// Token: 0x04000BB6 RID: 2998
	private PegUIElement pageRightRegion;

	// Token: 0x04000BB7 RID: 2999
	private PegUIElement pageDragRegion;

	// Token: 0x04000BB8 RID: 3000
	private CollectionPageManagerTouchBehavior.SwipeState swipeState;

	// Token: 0x04000BB9 RID: 3001
	private Vector2 m_swipeStartPosition;

	// Token: 0x0200149C RID: 5276
	private enum SwipeState
	{
		// Token: 0x0400AA5A RID: 43610
		None,
		// Token: 0x0400AA5B RID: 43611
		Update,
		// Token: 0x0400AA5C RID: 43612
		Success
	}
}
