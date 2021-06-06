using System.Collections;
using UnityEngine;

public class CollectionPageManagerTouchBehavior : PegUICustomBehavior
{
	private enum SwipeState
	{
		None,
		Update,
		Success
	}

	private float TurnDist = 0.07f;

	private PegUIElement pageLeftRegion;

	private PegUIElement pageRightRegion;

	private PegUIElement pageDragRegion;

	private SwipeState swipeState;

	private Vector2 m_swipeStartPosition;

	protected override void Awake()
	{
		base.Awake();
		BookPageManager component = GetComponent<BookPageManager>();
		pageLeftRegion = component.m_pageLeftClickableRegion;
		pageRightRegion = component.m_pageRightClickableRegion;
		pageDragRegion = component.m_pageDraggableRegion;
		pageDragRegion.gameObject.SetActive(value: true);
		pageDragRegion.AddEventListener(UIEventType.PRESS, OnPageDraggableRegionDown);
	}

	protected override void OnDestroy()
	{
		pageDragRegion.gameObject.SetActive(value: false);
		pageDragRegion.RemoveEventListener(UIEventType.PRESS, OnPageDraggableRegionDown);
		base.OnDestroy();
	}

	public override bool UpdateUI()
	{
		if ((CollectionInputMgr.Get() != null && CollectionInputMgr.Get().HasHeldCard()) || (CraftingManager.Get() != null && CraftingManager.Get().IsCardShowing()))
		{
			return false;
		}
		bool result = false;
		if (UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			result = swipeState == SwipeState.Success;
			swipeState = SwipeState.None;
		}
		if (swipeState != 0)
		{
			return true;
		}
		return result;
	}

	private void OnPageDraggableRegionDown(UIEvent e)
	{
		if (!(base.gameObject == null))
		{
			TryStartPageTurnGesture();
		}
	}

	private void TryStartPageTurnGesture()
	{
		if (swipeState != SwipeState.Update)
		{
			StartCoroutine(HandlePageTurnGesture());
		}
	}

	private Vector2 GetTouchPosition()
	{
		Vector3 touchPosition = HearthstoneServices.Get<ITouchScreenService>().GetTouchPosition();
		return new Vector2(touchPosition.x, touchPosition.y);
	}

	private IEnumerator HandlePageTurnGesture()
	{
		if (!UniversalInputManager.Get().IsTouchMode())
		{
			yield return null;
		}
		m_swipeStartPosition = GetTouchPosition();
		swipeState = SwipeState.Update;
		float pixelTurnDist = Mathf.Clamp(TurnDist * (float)Screen.currentResolution.width, 2f, 300f);
		PegUIElement touchDownPageTurnRegion = HitTestPageTurnRegions();
		while (!UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			float x = (GetTouchPosition() - m_swipeStartPosition).x;
			if (x <= 0f - pixelTurnDist && pageRightRegion.enabled)
			{
				pageRightRegion.TriggerRelease();
				swipeState = SwipeState.Success;
				yield break;
			}
			if (x >= pixelTurnDist && pageLeftRegion.enabled)
			{
				pageLeftRegion.TriggerRelease();
				swipeState = SwipeState.Success;
				yield break;
			}
			yield return null;
		}
		if (touchDownPageTurnRegion != null && touchDownPageTurnRegion == HitTestPageTurnRegions())
		{
			touchDownPageTurnRegion.TriggerRelease();
		}
		swipeState = SwipeState.None;
	}

	private PegUIElement HitTestPageTurnRegions()
	{
		PegUIElement pegUIElement = null;
		pageDragRegion.GetComponent<Collider>().enabled = false;
		if (UniversalInputManager.Get().GetInputHitInfo(out var hitInfo))
		{
			pegUIElement = hitInfo.collider.GetComponent<PegUIElement>();
			if (pegUIElement != pageLeftRegion && pegUIElement != pageRightRegion)
			{
				pegUIElement = null;
			}
		}
		pageDragRegion.GetComponent<Collider>().enabled = true;
		return pegUIElement;
	}
}
