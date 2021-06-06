using System.Collections;
using UnityEngine;

public abstract class BookPageManager : MonoBehaviour
{
	public enum PageTransitionType
	{
		NONE,
		SINGLE_PAGE_RIGHT,
		SINGLE_PAGE_LEFT,
		MANY_PAGE_RIGHT,
		MANY_PAGE_LEFT
	}

	public delegate void DelOnPageTransitionComplete(object callbackData);

	public delegate void PageTurnStartCallback(PageTransitionType transitionType);

	public delegate void PageTurnCompleteCallback(int currentPageNum);

	protected class TransitionReadyCallbackData
	{
		public BookPageDisplay m_assembledPage;

		public BookPageDisplay m_otherPage;

		public PageTransitionType m_transitionType;

		public DelOnPageTransitionComplete m_callback;

		public object m_callbackData;
	}

	public GameObject m_pageRightArrowBone;

	public GameObject m_pageLeftArrowBone;

	public PegUIElement m_pageRightClickableRegion;

	public PegUIElement m_pageLeftClickableRegion;

	public PegUIElement m_pageDraggableRegion;

	public BookPageDisplay m_pageDisplayPrefab;

	public PageTurn m_pageTurn;

	public float m_turnLeftPageSwapTiming;

	private static readonly Vector3 CURRENT_PAGE_LOCAL_POS = new Vector3(0f, 0.25f, 0f);

	private static readonly Vector3 NEXT_PAGE_LOCAL_POS = new Vector3(-300f, 0f, -300f);

	private static readonly float ARROW_SCALE_TIME = 0.6f;

	private static readonly string SHOW_ARROWS_COROUTINE_NAME = "WaitThenShowArrows";

	protected BookPageDisplay m_pageA;

	protected BookPageDisplay m_pageB;

	protected int m_currentPageNum;

	protected int m_transitionPageId;

	protected bool m_currentPageIsPageA;

	private bool m_fullyLoaded;

	protected bool m_skipNextPageTurn;

	protected PagingArrow m_pageRightArrow;

	protected PagingArrow m_pageLeftArrow;

	private bool m_rightArrowShown;

	private bool m_leftArrowShown;

	protected bool m_hasPreviousPage;

	protected bool m_hasNextPage;

	private bool m_delayShowingArrows;

	private bool m_pageTurnDisabled;

	protected bool m_pagesCurrentlyTurning;

	protected bool m_wasTouchModeEnabled;

	private readonly PlatformDependentValue<bool> ANIMATE_PAGE_TRANSITIONS = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		iOS = false,
		Android = true,
		PC = true,
		Mac = true
	};

	private readonly PlatformDependentValue<bool> ANIMATE_PAGE_TRANSITIONS_MEMORY_OVERRIDE = new PlatformDependentValue<bool>(PlatformCategory.Memory)
	{
		LowMemory = false,
		MediumMemory = false,
		HighMemory = true
	};

	public int CurrentPageNum => m_currentPageNum;

	public event PageTurnStartCallback PageTurnStart;

	public event PageTurnCompleteCallback PageTurnComplete;

	protected virtual void Awake()
	{
		m_pageLeftClickableRegion.AddEventListener(UIEventType.RELEASE, OnPageLeftPressed);
		m_pageLeftClickableRegion.SetCursorOver(PegCursor.Mode.LEFTARROW);
		m_pageLeftClickableRegion.SetCursorDown(PegCursor.Mode.LEFTARROW);
		m_pageRightClickableRegion.AddEventListener(UIEventType.RELEASE, OnPageRightPressed);
		m_pageRightClickableRegion.SetCursorOver(PegCursor.Mode.RIGHTARROW);
		m_pageRightClickableRegion.SetCursorDown(PegCursor.Mode.RIGHTARROW);
		m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
		if (UniversalInputManager.Get().IsTouchMode())
		{
			base.gameObject.AddComponent<CollectionPageManagerTouchBehavior>();
		}
		m_pageA = Object.Instantiate(m_pageDisplayPrefab);
		m_pageB = Object.Instantiate(m_pageDisplayPrefab);
		TransformUtil.AttachAndPreserveLocalTransform(m_pageA.transform, base.transform);
		TransformUtil.AttachAndPreserveLocalTransform(m_pageB.transform, base.transform);
	}

	protected virtual void Start()
	{
		BookPageDisplay alternatePage = GetAlternatePage();
		BookPageDisplay currentPage = GetCurrentPage();
		AssembleEmptyPageUI(alternatePage);
		AssembleEmptyPageUI(currentPage);
		PositionNextPage(alternatePage);
		PositionCurrentPage(currentPage);
		StartCoroutine(WaitForPagesToBeFullyLoaded());
	}

	private IEnumerator WaitForPagesToBeFullyLoaded()
	{
		while (!m_pageA.IsLoaded() || !m_pageB.IsLoaded())
		{
			yield return null;
		}
		m_fullyLoaded = true;
	}

	protected virtual void Update()
	{
		bool flag = UniversalInputManager.Get().IsTouchMode();
		if (m_wasTouchModeEnabled != flag)
		{
			HandleTouchModeChanged();
		}
	}

	public void OnBookOpening()
	{
		StopCoroutine(SHOW_ARROWS_COROUTINE_NAME);
		StartCoroutine(SHOW_ARROWS_COROUTINE_NAME);
	}

	public bool ArePagesTurning()
	{
		return m_pagesCurrentlyTurning;
	}

	public int GetTransitionPageId()
	{
		return m_transitionPageId;
	}

	public bool IsFullyLoaded()
	{
		return m_fullyLoaded;
	}

	protected virtual bool CanUserTurnPages()
	{
		if (m_pagesCurrentlyTurning)
		{
			return false;
		}
		return true;
	}

	public void FlipToPage(int newPageNum, DelOnPageTransitionComplete callback, object callbackData)
	{
		PageTransitionType transitionType = ((newPageNum == m_currentPageNum - 1) ? PageTransitionType.SINGLE_PAGE_LEFT : ((newPageNum == m_currentPageNum + 1) ? PageTransitionType.SINGLE_PAGE_RIGHT : ((newPageNum < m_currentPageNum) ? PageTransitionType.MANY_PAGE_LEFT : PageTransitionType.MANY_PAGE_RIGHT)));
		m_currentPageNum = newPageNum;
		TransitionPageWhenReady(transitionType, useCurrentPageNum: true, callback, callbackData);
	}

	private void SwapCurrentAndAltPages()
	{
		m_currentPageIsPageA = !m_currentPageIsPageA;
	}

	protected BookPageDisplay GetCurrentPage()
	{
		if (!m_currentPageIsPageA)
		{
			return m_pageB;
		}
		return m_pageA;
	}

	protected BookPageDisplay GetAlternatePage()
	{
		if (!m_currentPageIsPageA)
		{
			return m_pageA;
		}
		return m_pageB;
	}

	protected void TransitionPageWhenReady(PageTransitionType transitionType, bool useCurrentPageNum, DelOnPageTransitionComplete callback, object callbackData)
	{
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1} transitionType={2} currentPageIsPageA={3}", m_transitionPageId, m_pagesCurrentlyTurning, transitionType, m_currentPageIsPageA);
		if (m_pagesCurrentlyTurning)
		{
			Debug.LogWarning("TransitionPageWhenReady() called when m_pagesCurrentlyTurning is already true! You probably don't want to allow this [see usages of CanUserTurnPages()].");
		}
		if (HeroPickerDisplay.Get() != null && HeroPickerDisplay.Get().IsShown())
		{
			transitionType = PageTransitionType.NONE;
		}
		m_pagesCurrentlyTurning = true;
		if (this.PageTurnStart != null)
		{
			this.PageTurnStart(transitionType);
		}
		SwapCurrentAndAltPages();
		TransitionReadyCallbackData transitionReadyCallbackData = new TransitionReadyCallbackData
		{
			m_assembledPage = GetCurrentPage(),
			m_otherPage = GetAlternatePage(),
			m_transitionType = transitionType,
			m_callback = callback,
			m_callbackData = callbackData
		};
		switch (transitionType)
		{
		case PageTransitionType.SINGLE_PAGE_LEFT:
		case PageTransitionType.MANY_PAGE_LEFT:
			SoundManager.Get().LoadAndPlay("collection_manager_book_page_flip_back.prefab:371e496e1cd371144abfec472e72d9a9");
			break;
		case PageTransitionType.SINGLE_PAGE_RIGHT:
		case PageTransitionType.MANY_PAGE_RIGHT:
			SoundManager.Get().LoadAndPlay("collection_manager_book_page_flip_forward.prefab:07282310dd70fee4ca2dfdb37c545acc");
			break;
		}
		AssemblePage(transitionReadyCallbackData, useCurrentPageNum);
		OnPageTransitionRequested();
	}

	protected abstract void AssemblePage(TransitionReadyCallbackData transitionReadyCallbackData, bool useCurrentPageNum);

	protected virtual void AssembleEmptyPageUI(BookPageDisplay page)
	{
		SetHasPreviousAndNextPages(hasPreviousPage: false, hasNextPage: false);
	}

	private void PositionCurrentPage(BookPageDisplay page)
	{
		page.transform.localPosition = CURRENT_PAGE_LOCAL_POS;
	}

	private void PositionNextPage(BookPageDisplay page)
	{
		page.transform.localPosition = NEXT_PAGE_LOCAL_POS;
	}

	protected virtual void TransitionPage(object callbackData)
	{
		m_transitionPageId++;
		int transitionPageId = m_transitionPageId;
		TransitionReadyCallbackData transitionReadyCallbackData = callbackData as TransitionReadyCallbackData;
		BookPageDisplay assembledPage = transitionReadyCallbackData.m_assembledPage;
		BookPageDisplay otherPage = transitionReadyCallbackData.m_otherPage;
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1} transitionType={2} currentPageIsPageA={3}", m_transitionPageId, m_pagesCurrentlyTurning, transitionReadyCallbackData.m_transitionType, m_currentPageIsPageA);
		if (assembledPage.m_basePageRenderer != null)
		{
			m_pageTurn.SetBackPageMaterial(assembledPage.m_basePageRenderer.GetMaterial());
		}
		else
		{
			Debug.LogError("No Base Page Renderer is set for the assembled page! Back Page Material cannot be properly set!");
		}
		if ((bool)ANIMATE_PAGE_TRANSITIONS || (bool)ANIMATE_PAGE_TRANSITIONS_MEMORY_OVERRIDE)
		{
			PageTransitionType pageTransitionType = transitionReadyCallbackData.m_transitionType;
			if (TavernBrawlDisplay.IsTavernBrawlViewing() || (SceneMgr.Get().IsInDuelsMode() && !PvPDungeonRunScene.IsEditingDeck()))
			{
				pageTransitionType = PageTransitionType.NONE;
			}
			if (m_skipNextPageTurn)
			{
				pageTransitionType = PageTransitionType.NONE;
				m_skipNextPageTurn = false;
			}
			switch (pageTransitionType)
			{
			case PageTransitionType.NONE:
				PositionNextPage(otherPage);
				PositionCurrentPage(assembledPage);
				OnPageTurnComplete(transitionReadyCallbackData, transitionPageId);
				break;
			case PageTransitionType.SINGLE_PAGE_LEFT:
			case PageTransitionType.MANY_PAGE_LEFT:
				m_pageTurn.TurnLeft(assembledPage.gameObject, otherPage.gameObject, delegate(object data)
				{
					OnPageTurnComplete(data, transitionPageId);
				}, delegate(object data)
				{
					PositionPages(data, transitionPageId);
				}, transitionReadyCallbackData);
				break;
			case PageTransitionType.SINGLE_PAGE_RIGHT:
			case PageTransitionType.MANY_PAGE_RIGHT:
				m_pageTurn.TurnRight(otherPage.gameObject, assembledPage.gameObject, delegate(object data)
				{
					OnPageTurnComplete(data, transitionPageId);
				}, delegate(object data)
				{
					PositionPages(data, transitionPageId);
				}, transitionReadyCallbackData);
				break;
			}
		}
		if (!ANIMATE_PAGE_TRANSITIONS && !ANIMATE_PAGE_TRANSITIONS_MEMORY_OVERRIDE)
		{
			PositionNextPage(otherPage);
			PositionCurrentPage(assembledPage);
			OnPageTurnComplete(transitionReadyCallbackData, transitionPageId);
		}
	}

	protected virtual void OnPageTurnComplete(object callbackData, int operationId)
	{
		TransitionReadyCallbackData transitionReadyCallbackData = callbackData as TransitionReadyCallbackData;
		Log.CollectionManager.Print("transitionPageId={0} vs {1} | assembledIsCurrent={2}, pagesTurning={3} currentPageIsPageA={4}", operationId, m_transitionPageId, transitionReadyCallbackData.m_assembledPage == GetCurrentPage(), m_pagesCurrentlyTurning, m_currentPageIsPageA);
		if (operationId != m_transitionPageId)
		{
			if (transitionReadyCallbackData.m_callback != null)
			{
				transitionReadyCallbackData.m_callback(transitionReadyCallbackData.m_callbackData);
			}
			if (this.PageTurnComplete != null)
			{
				this.PageTurnComplete(m_currentPageNum);
			}
			Log.CollectionManager.PrintWarning("transitionPageId={0} vs {1} | EARLY OUT!", operationId, m_transitionPageId);
			return;
		}
		BookPageDisplay assembledPage = transitionReadyCallbackData.m_assembledPage;
		BookPageDisplay otherPage = transitionReadyCallbackData.m_otherPage;
		if (otherPage != GetCurrentPage())
		{
			assembledPage.Show();
			otherPage.Hide();
		}
		if (transitionReadyCallbackData.m_callback != null)
		{
			transitionReadyCallbackData.m_callback(transitionReadyCallbackData.m_callbackData);
		}
		if (transitionReadyCallbackData.m_assembledPage == GetCurrentPage())
		{
			Log.CollectionManager.Print("transitionPageId={0} vs {1} | set m_pagesCurrentlyTurning = false", operationId, m_transitionPageId);
			m_pagesCurrentlyTurning = false;
		}
		if (this.PageTurnComplete != null)
		{
			this.PageTurnComplete(m_currentPageNum);
		}
	}

	private void PositionPages(object callbackData, int operationId)
	{
		TransitionReadyCallbackData transitionReadyCallbackData = callbackData as TransitionReadyCallbackData;
		if (operationId == m_transitionPageId)
		{
			PositionCurrentPage(transitionReadyCallbackData.m_assembledPage);
			PositionNextPage(transitionReadyCallbackData.m_otherPage);
		}
	}

	protected virtual void PageRight(DelOnPageTransitionComplete callback, object callbackData)
	{
		m_currentPageNum++;
		TransitionPageWhenReady(PageTransitionType.SINGLE_PAGE_RIGHT, useCurrentPageNum: true, callback, callbackData);
	}

	protected virtual void PageLeft(DelOnPageTransitionComplete callback, object callbackData)
	{
		m_currentPageNum--;
		TransitionPageWhenReady(PageTransitionType.SINGLE_PAGE_LEFT, useCurrentPageNum: true, callback, callbackData);
	}

	public void EnablePageTurn(bool enable)
	{
		m_pageTurnDisabled = !enable;
		RefreshPageTurnButtons();
	}

	protected void SetHasPreviousAndNextPages(bool hasPreviousPage, bool hasNextPage)
	{
		m_hasPreviousPage = hasPreviousPage;
		m_hasNextPage = hasNextPage;
		RefreshPageTurnButtons();
	}

	protected void RefreshPageTurnButtons()
	{
		bool flag = !m_pageTurnDisabled && m_hasPreviousPage;
		m_pageLeftClickableRegion.enabled = flag;
		m_pageLeftClickableRegion.SetEnabled(flag);
		bool flag2 = !m_pageTurnDisabled && m_hasNextPage;
		m_pageRightClickableRegion.enabled = flag2;
		m_pageRightClickableRegion.SetEnabled(flag2);
		ShowArrow(m_pageLeftArrow, m_hasPreviousPage, isRightArrow: false);
		ShowArrow(m_pageRightArrow, m_hasNextPage, isRightArrow: true);
	}

	private void OnPageLeftPressed(UIEvent e)
	{
		if (CanUserTurnPages())
		{
			PageLeft(null, null);
		}
	}

	protected abstract void OnPageTransitionRequested();

	private void OnPageRightPressed(UIEvent e)
	{
		if (CanUserTurnPages())
		{
			PageRight(null, null);
		}
	}

	public void LoadPagingArrows()
	{
		if (!m_pageLeftArrow || !m_pageRightArrow)
		{
			AssetLoader.Get().InstantiatePrefab("PagingArrow.prefab:70d4430ff418d6d42a943e77dc98d523", OnPagingArrowLoaded);
		}
	}

	private void OnPagingArrowLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (!(go == null))
		{
			if (!m_pageLeftArrow)
			{
				m_pageLeftArrow = go.GetComponent<PagingArrow>();
				m_pageLeftArrow.transform.parent = m_pageLeftArrowBone.transform;
				m_pageLeftArrow.transform.localEulerAngles = Vector3.zero;
				m_pageLeftArrow.transform.position = m_pageLeftArrowBone.transform.position;
				m_pageLeftArrow.transform.localScale = Vector3.zero;
				SceneUtils.SetLayer(m_pageLeftArrow, GameLayer.TransparentFX);
			}
			if (!m_pageRightArrow)
			{
				m_pageRightArrow = Object.Instantiate(m_pageLeftArrow);
				m_pageRightArrow.transform.parent = m_pageRightArrowBone.transform;
				m_pageRightArrow.transform.localEulerAngles = Vector3.zero;
				m_pageRightArrow.transform.position = m_pageRightArrowBone.transform.position;
				m_pageRightArrow.transform.localScale = Vector3.zero;
				SceneUtils.SetLayer(m_pageRightArrow, GameLayer.TransparentFX);
			}
			RefreshPageTurnButtons();
		}
	}

	protected void ShowPagingArrowHighlight()
	{
		m_pageRightArrow.ShowHighlight();
	}

	private IEnumerator WaitThenShowArrows()
	{
		if (!(m_pageLeftArrow == null) || !(m_pageRightArrow == null))
		{
			m_delayShowingArrows = true;
			yield return new WaitForSeconds(1f);
			m_delayShowingArrows = false;
			RefreshPageTurnButtons();
		}
	}

	private void ShowArrow(PagingArrow arrow, bool show, bool isRightArrow)
	{
		if (arrow == null || (m_delayShowingArrows && show))
		{
			return;
		}
		if (isRightArrow)
		{
			if (m_rightArrowShown == show)
			{
				return;
			}
			m_rightArrowShown = show;
		}
		else
		{
			if (m_leftArrowShown == show)
			{
				return;
			}
			m_leftArrowShown = show;
		}
		Vector3 vector = (show ? new Vector3(1f, 1f, 1f) : Vector3.zero);
		iTween.EaseType easeType = (show ? iTween.EaseType.easeOutElastic : iTween.EaseType.linear);
		Hashtable args = iTween.Hash("scale", vector, "time", ARROW_SCALE_TIME, "easetype", easeType, "name", "ArrowScale");
		iTween.StopByName(arrow.gameObject, "ArrowScale");
		iTween.ScaleTo(arrow.gameObject, args);
	}

	protected virtual void HandleTouchModeChanged()
	{
		bool flag = UniversalInputManager.Get().IsTouchMode();
		if (m_wasTouchModeEnabled == flag)
		{
			Debug.LogWarning("Touch mode did not change, why are you calling BookPageManager.HandleTouchModeChanged()?");
			return;
		}
		m_wasTouchModeEnabled = flag;
		if (flag)
		{
			base.gameObject.AddComponent<CollectionPageManagerTouchBehavior>();
		}
		else
		{
			Object.Destroy(base.gameObject.GetComponent<CollectionPageManagerTouchBehavior>());
		}
	}
}
