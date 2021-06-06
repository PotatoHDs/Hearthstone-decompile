using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public abstract class BookPageManager : MonoBehaviour
{
	// Token: 0x14000012 RID: 18
	// (add) Token: 0x06000B52 RID: 2898 RVA: 0x00042850 File Offset: 0x00040A50
	// (remove) Token: 0x06000B53 RID: 2899 RVA: 0x00042888 File Offset: 0x00040A88
	public event BookPageManager.PageTurnStartCallback PageTurnStart;

	// Token: 0x14000013 RID: 19
	// (add) Token: 0x06000B54 RID: 2900 RVA: 0x000428C0 File Offset: 0x00040AC0
	// (remove) Token: 0x06000B55 RID: 2901 RVA: 0x000428F8 File Offset: 0x00040AF8
	public event BookPageManager.PageTurnCompleteCallback PageTurnComplete;

	// Token: 0x06000B56 RID: 2902 RVA: 0x00042930 File Offset: 0x00040B30
	protected virtual void Awake()
	{
		this.m_pageLeftClickableRegion.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPageLeftPressed));
		this.m_pageLeftClickableRegion.SetCursorOver(PegCursor.Mode.LEFTARROW);
		this.m_pageLeftClickableRegion.SetCursorDown(PegCursor.Mode.LEFTARROW);
		this.m_pageRightClickableRegion.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPageRightPressed));
		this.m_pageRightClickableRegion.SetCursorOver(PegCursor.Mode.RIGHTARROW);
		this.m_pageRightClickableRegion.SetCursorDown(PegCursor.Mode.RIGHTARROW);
		this.m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
		if (UniversalInputManager.Get().IsTouchMode())
		{
			base.gameObject.AddComponent<CollectionPageManagerTouchBehavior>();
		}
		this.m_pageA = UnityEngine.Object.Instantiate<BookPageDisplay>(this.m_pageDisplayPrefab);
		this.m_pageB = UnityEngine.Object.Instantiate<BookPageDisplay>(this.m_pageDisplayPrefab);
		TransformUtil.AttachAndPreserveLocalTransform(this.m_pageA.transform, base.transform);
		TransformUtil.AttachAndPreserveLocalTransform(this.m_pageB.transform, base.transform);
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x00042A18 File Offset: 0x00040C18
	protected virtual void Start()
	{
		BookPageDisplay alternatePage = this.GetAlternatePage();
		BookPageDisplay currentPage = this.GetCurrentPage();
		this.AssembleEmptyPageUI(alternatePage);
		this.AssembleEmptyPageUI(currentPage);
		this.PositionNextPage(alternatePage);
		this.PositionCurrentPage(currentPage);
		base.StartCoroutine(this.WaitForPagesToBeFullyLoaded());
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x00042A5C File Offset: 0x00040C5C
	private IEnumerator WaitForPagesToBeFullyLoaded()
	{
		while (!this.m_pageA.IsLoaded() || !this.m_pageB.IsLoaded())
		{
			yield return null;
		}
		this.m_fullyLoaded = true;
		yield break;
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x00042A6C File Offset: 0x00040C6C
	protected virtual void Update()
	{
		bool flag = UniversalInputManager.Get().IsTouchMode();
		if (this.m_wasTouchModeEnabled != flag)
		{
			this.HandleTouchModeChanged();
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00042A93 File Offset: 0x00040C93
	public int CurrentPageNum
	{
		get
		{
			return this.m_currentPageNum;
		}
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x00042A9B File Offset: 0x00040C9B
	public void OnBookOpening()
	{
		base.StopCoroutine(BookPageManager.SHOW_ARROWS_COROUTINE_NAME);
		base.StartCoroutine(BookPageManager.SHOW_ARROWS_COROUTINE_NAME);
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x00042AB4 File Offset: 0x00040CB4
	public bool ArePagesTurning()
	{
		return this.m_pagesCurrentlyTurning;
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x00042ABC File Offset: 0x00040CBC
	public int GetTransitionPageId()
	{
		return this.m_transitionPageId;
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x00042AC4 File Offset: 0x00040CC4
	public bool IsFullyLoaded()
	{
		return this.m_fullyLoaded;
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x00042ACC File Offset: 0x00040CCC
	protected virtual bool CanUserTurnPages()
	{
		return !this.m_pagesCurrentlyTurning;
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x00042ADC File Offset: 0x00040CDC
	public void FlipToPage(int newPageNum, BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		BookPageManager.PageTransitionType transitionType;
		if (newPageNum == this.m_currentPageNum - 1)
		{
			transitionType = BookPageManager.PageTransitionType.SINGLE_PAGE_LEFT;
		}
		else if (newPageNum == this.m_currentPageNum + 1)
		{
			transitionType = BookPageManager.PageTransitionType.SINGLE_PAGE_RIGHT;
		}
		else
		{
			transitionType = ((newPageNum < this.m_currentPageNum) ? BookPageManager.PageTransitionType.MANY_PAGE_LEFT : BookPageManager.PageTransitionType.MANY_PAGE_RIGHT);
		}
		this.m_currentPageNum = newPageNum;
		this.TransitionPageWhenReady(transitionType, true, callback, callbackData);
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00042B28 File Offset: 0x00040D28
	private void SwapCurrentAndAltPages()
	{
		this.m_currentPageIsPageA = !this.m_currentPageIsPageA;
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x00042B39 File Offset: 0x00040D39
	protected BookPageDisplay GetCurrentPage()
	{
		if (!this.m_currentPageIsPageA)
		{
			return this.m_pageB;
		}
		return this.m_pageA;
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x00042B50 File Offset: 0x00040D50
	protected BookPageDisplay GetAlternatePage()
	{
		if (!this.m_currentPageIsPageA)
		{
			return this.m_pageA;
		}
		return this.m_pageB;
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x00042B68 File Offset: 0x00040D68
	protected void TransitionPageWhenReady(BookPageManager.PageTransitionType transitionType, bool useCurrentPageNum, BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1} transitionType={2} currentPageIsPageA={3}", new object[]
		{
			this.m_transitionPageId,
			this.m_pagesCurrentlyTurning,
			transitionType,
			this.m_currentPageIsPageA
		});
		if (this.m_pagesCurrentlyTurning)
		{
			Debug.LogWarning("TransitionPageWhenReady() called when m_pagesCurrentlyTurning is already true! You probably don't want to allow this [see usages of CanUserTurnPages()].");
		}
		if (HeroPickerDisplay.Get() != null && HeroPickerDisplay.Get().IsShown())
		{
			transitionType = BookPageManager.PageTransitionType.NONE;
		}
		this.m_pagesCurrentlyTurning = true;
		if (this.PageTurnStart != null)
		{
			this.PageTurnStart(transitionType);
		}
		this.SwapCurrentAndAltPages();
		BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData = new BookPageManager.TransitionReadyCallbackData
		{
			m_assembledPage = this.GetCurrentPage(),
			m_otherPage = this.GetAlternatePage(),
			m_transitionType = transitionType,
			m_callback = callback,
			m_callbackData = callbackData
		};
		switch (transitionType)
		{
		case BookPageManager.PageTransitionType.SINGLE_PAGE_RIGHT:
		case BookPageManager.PageTransitionType.MANY_PAGE_RIGHT:
			SoundManager.Get().LoadAndPlay("collection_manager_book_page_flip_forward.prefab:07282310dd70fee4ca2dfdb37c545acc");
			break;
		case BookPageManager.PageTransitionType.SINGLE_PAGE_LEFT:
		case BookPageManager.PageTransitionType.MANY_PAGE_LEFT:
			SoundManager.Get().LoadAndPlay("collection_manager_book_page_flip_back.prefab:371e496e1cd371144abfec472e72d9a9");
			break;
		}
		this.AssemblePage(transitionReadyCallbackData, useCurrentPageNum);
		this.OnPageTransitionRequested();
	}

	// Token: 0x06000B65 RID: 2917
	protected abstract void AssemblePage(BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData, bool useCurrentPageNum);

	// Token: 0x06000B66 RID: 2918 RVA: 0x00042C92 File Offset: 0x00040E92
	protected virtual void AssembleEmptyPageUI(BookPageDisplay page)
	{
		this.SetHasPreviousAndNextPages(false, false);
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x00042C9C File Offset: 0x00040E9C
	private void PositionCurrentPage(BookPageDisplay page)
	{
		page.transform.localPosition = BookPageManager.CURRENT_PAGE_LOCAL_POS;
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x00042CAE File Offset: 0x00040EAE
	private void PositionNextPage(BookPageDisplay page)
	{
		page.transform.localPosition = BookPageManager.NEXT_PAGE_LOCAL_POS;
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x00042CC0 File Offset: 0x00040EC0
	protected virtual void TransitionPage(object callbackData)
	{
		this.m_transitionPageId++;
		int transitionPageId = this.m_transitionPageId;
		BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData = callbackData as BookPageManager.TransitionReadyCallbackData;
		BookPageDisplay assembledPage = transitionReadyCallbackData.m_assembledPage;
		BookPageDisplay otherPage = transitionReadyCallbackData.m_otherPage;
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1} transitionType={2} currentPageIsPageA={3}", new object[]
		{
			this.m_transitionPageId,
			this.m_pagesCurrentlyTurning,
			transitionReadyCallbackData.m_transitionType,
			this.m_currentPageIsPageA
		});
		if (assembledPage.m_basePageRenderer != null)
		{
			this.m_pageTurn.SetBackPageMaterial(assembledPage.m_basePageRenderer.GetMaterial());
		}
		else
		{
			Debug.LogError("No Base Page Renderer is set for the assembled page! Back Page Material cannot be properly set!");
		}
		if (this.ANIMATE_PAGE_TRANSITIONS || this.ANIMATE_PAGE_TRANSITIONS_MEMORY_OVERRIDE)
		{
			BookPageManager.PageTransitionType pageTransitionType = transitionReadyCallbackData.m_transitionType;
			if (TavernBrawlDisplay.IsTavernBrawlViewing() || (SceneMgr.Get().IsInDuelsMode() && !PvPDungeonRunScene.IsEditingDeck()))
			{
				pageTransitionType = BookPageManager.PageTransitionType.NONE;
			}
			if (this.m_skipNextPageTurn)
			{
				pageTransitionType = BookPageManager.PageTransitionType.NONE;
				this.m_skipNextPageTurn = false;
			}
			switch (pageTransitionType)
			{
			case BookPageManager.PageTransitionType.NONE:
				this.PositionNextPage(otherPage);
				this.PositionCurrentPage(assembledPage);
				this.OnPageTurnComplete(transitionReadyCallbackData, transitionPageId);
				break;
			case BookPageManager.PageTransitionType.SINGLE_PAGE_RIGHT:
			case BookPageManager.PageTransitionType.MANY_PAGE_RIGHT:
				this.m_pageTurn.TurnRight(otherPage.gameObject, assembledPage.gameObject, delegate(object data)
				{
					this.OnPageTurnComplete(data, transitionPageId);
				}, delegate(object data)
				{
					this.PositionPages(data, transitionPageId);
				}, transitionReadyCallbackData);
				break;
			case BookPageManager.PageTransitionType.SINGLE_PAGE_LEFT:
			case BookPageManager.PageTransitionType.MANY_PAGE_LEFT:
				this.m_pageTurn.TurnLeft(assembledPage.gameObject, otherPage.gameObject, delegate(object data)
				{
					this.OnPageTurnComplete(data, transitionPageId);
				}, delegate(object data)
				{
					this.PositionPages(data, transitionPageId);
				}, transitionReadyCallbackData);
				break;
			}
		}
		if (!this.ANIMATE_PAGE_TRANSITIONS && !this.ANIMATE_PAGE_TRANSITIONS_MEMORY_OVERRIDE)
		{
			this.PositionNextPage(otherPage);
			this.PositionCurrentPage(assembledPage);
			this.OnPageTurnComplete(transitionReadyCallbackData, transitionPageId);
		}
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x00042EAC File Offset: 0x000410AC
	protected virtual void OnPageTurnComplete(object callbackData, int operationId)
	{
		BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData = callbackData as BookPageManager.TransitionReadyCallbackData;
		Log.CollectionManager.Print("transitionPageId={0} vs {1} | assembledIsCurrent={2}, pagesTurning={3} currentPageIsPageA={4}", new object[]
		{
			operationId,
			this.m_transitionPageId,
			transitionReadyCallbackData.m_assembledPage == this.GetCurrentPage(),
			this.m_pagesCurrentlyTurning,
			this.m_currentPageIsPageA
		});
		if (operationId != this.m_transitionPageId)
		{
			if (transitionReadyCallbackData.m_callback != null)
			{
				transitionReadyCallbackData.m_callback(transitionReadyCallbackData.m_callbackData);
			}
			if (this.PageTurnComplete != null)
			{
				this.PageTurnComplete(this.m_currentPageNum);
			}
			Log.CollectionManager.PrintWarning("transitionPageId={0} vs {1} | EARLY OUT!", new object[]
			{
				operationId,
				this.m_transitionPageId
			});
			return;
		}
		BookPageDisplay assembledPage = transitionReadyCallbackData.m_assembledPage;
		BookPageDisplay otherPage = transitionReadyCallbackData.m_otherPage;
		if (otherPage != this.GetCurrentPage())
		{
			assembledPage.Show();
			otherPage.Hide();
		}
		if (transitionReadyCallbackData.m_callback != null)
		{
			transitionReadyCallbackData.m_callback(transitionReadyCallbackData.m_callbackData);
		}
		if (transitionReadyCallbackData.m_assembledPage == this.GetCurrentPage())
		{
			Log.CollectionManager.Print("transitionPageId={0} vs {1} | set m_pagesCurrentlyTurning = false", new object[]
			{
				operationId,
				this.m_transitionPageId
			});
			this.m_pagesCurrentlyTurning = false;
		}
		if (this.PageTurnComplete != null)
		{
			this.PageTurnComplete(this.m_currentPageNum);
		}
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x0004302C File Offset: 0x0004122C
	private void PositionPages(object callbackData, int operationId)
	{
		BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData = callbackData as BookPageManager.TransitionReadyCallbackData;
		if (operationId != this.m_transitionPageId)
		{
			return;
		}
		this.PositionCurrentPage(transitionReadyCallbackData.m_assembledPage);
		this.PositionNextPage(transitionReadyCallbackData.m_otherPage);
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x00043062 File Offset: 0x00041262
	protected virtual void PageRight(BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		this.m_currentPageNum++;
		this.TransitionPageWhenReady(BookPageManager.PageTransitionType.SINGLE_PAGE_RIGHT, true, callback, callbackData);
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x0004307C File Offset: 0x0004127C
	protected virtual void PageLeft(BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		this.m_currentPageNum--;
		this.TransitionPageWhenReady(BookPageManager.PageTransitionType.SINGLE_PAGE_LEFT, true, callback, callbackData);
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x00043096 File Offset: 0x00041296
	public void EnablePageTurn(bool enable)
	{
		this.m_pageTurnDisabled = !enable;
		this.RefreshPageTurnButtons();
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x000430A8 File Offset: 0x000412A8
	protected void SetHasPreviousAndNextPages(bool hasPreviousPage, bool hasNextPage)
	{
		this.m_hasPreviousPage = hasPreviousPage;
		this.m_hasNextPage = hasNextPage;
		this.RefreshPageTurnButtons();
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x000430C0 File Offset: 0x000412C0
	protected void RefreshPageTurnButtons()
	{
		bool enabled = !this.m_pageTurnDisabled && this.m_hasPreviousPage;
		this.m_pageLeftClickableRegion.enabled = enabled;
		this.m_pageLeftClickableRegion.SetEnabled(enabled, false);
		bool enabled2 = !this.m_pageTurnDisabled && this.m_hasNextPage;
		this.m_pageRightClickableRegion.enabled = enabled2;
		this.m_pageRightClickableRegion.SetEnabled(enabled2, false);
		this.ShowArrow(this.m_pageLeftArrow, this.m_hasPreviousPage, false);
		this.ShowArrow(this.m_pageRightArrow, this.m_hasNextPage, true);
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x00043149 File Offset: 0x00041349
	private void OnPageLeftPressed(UIEvent e)
	{
		if (!this.CanUserTurnPages())
		{
			return;
		}
		this.PageLeft(null, null);
	}

	// Token: 0x06000B72 RID: 2930
	protected abstract void OnPageTransitionRequested();

	// Token: 0x06000B73 RID: 2931 RVA: 0x0004315C File Offset: 0x0004135C
	private void OnPageRightPressed(UIEvent e)
	{
		if (!this.CanUserTurnPages())
		{
			return;
		}
		this.PageRight(null, null);
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x0004316F File Offset: 0x0004136F
	public void LoadPagingArrows()
	{
		if (this.m_pageLeftArrow && this.m_pageRightArrow)
		{
			return;
		}
		AssetLoader.Get().InstantiatePrefab("PagingArrow.prefab:70d4430ff418d6d42a943e77dc98d523", new PrefabCallback<GameObject>(this.OnPagingArrowLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x000431B0 File Offset: 0x000413B0
	private void OnPagingArrowLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			return;
		}
		if (!this.m_pageLeftArrow)
		{
			this.m_pageLeftArrow = go.GetComponent<PagingArrow>();
			this.m_pageLeftArrow.transform.parent = this.m_pageLeftArrowBone.transform;
			this.m_pageLeftArrow.transform.localEulerAngles = Vector3.zero;
			this.m_pageLeftArrow.transform.position = this.m_pageLeftArrowBone.transform.position;
			this.m_pageLeftArrow.transform.localScale = Vector3.zero;
			SceneUtils.SetLayer(this.m_pageLeftArrow, GameLayer.TransparentFX);
		}
		if (!this.m_pageRightArrow)
		{
			this.m_pageRightArrow = UnityEngine.Object.Instantiate<PagingArrow>(this.m_pageLeftArrow);
			this.m_pageRightArrow.transform.parent = this.m_pageRightArrowBone.transform;
			this.m_pageRightArrow.transform.localEulerAngles = Vector3.zero;
			this.m_pageRightArrow.transform.position = this.m_pageRightArrowBone.transform.position;
			this.m_pageRightArrow.transform.localScale = Vector3.zero;
			SceneUtils.SetLayer(this.m_pageRightArrow, GameLayer.TransparentFX);
		}
		this.RefreshPageTurnButtons();
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x000432E9 File Offset: 0x000414E9
	protected void ShowPagingArrowHighlight()
	{
		this.m_pageRightArrow.ShowHighlight();
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x000432F6 File Offset: 0x000414F6
	private IEnumerator WaitThenShowArrows()
	{
		if (this.m_pageLeftArrow == null && this.m_pageRightArrow == null)
		{
			yield break;
		}
		this.m_delayShowingArrows = true;
		yield return new WaitForSeconds(1f);
		this.m_delayShowingArrows = false;
		this.RefreshPageTurnButtons();
		yield break;
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x00043308 File Offset: 0x00041508
	private void ShowArrow(PagingArrow arrow, bool show, bool isRightArrow)
	{
		if (arrow == null)
		{
			return;
		}
		if (this.m_delayShowingArrows && show)
		{
			return;
		}
		if (isRightArrow)
		{
			if (this.m_rightArrowShown == show)
			{
				return;
			}
			this.m_rightArrowShown = show;
		}
		else
		{
			if (this.m_leftArrowShown == show)
			{
				return;
			}
			this.m_leftArrowShown = show;
		}
		Vector3 vector = show ? new Vector3(1f, 1f, 1f) : Vector3.zero;
		iTween.EaseType easeType = show ? iTween.EaseType.easeOutElastic : iTween.EaseType.linear;
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			vector,
			"time",
			BookPageManager.ARROW_SCALE_TIME,
			"easetype",
			easeType,
			"name",
			"ArrowScale"
		});
		iTween.StopByName(arrow.gameObject, "ArrowScale");
		iTween.ScaleTo(arrow.gameObject, args);
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x000433EC File Offset: 0x000415EC
	protected virtual void HandleTouchModeChanged()
	{
		bool flag = UniversalInputManager.Get().IsTouchMode();
		if (this.m_wasTouchModeEnabled == flag)
		{
			Debug.LogWarning("Touch mode did not change, why are you calling BookPageManager.HandleTouchModeChanged()?");
			return;
		}
		this.m_wasTouchModeEnabled = flag;
		if (flag)
		{
			base.gameObject.AddComponent<CollectionPageManagerTouchBehavior>();
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject.GetComponent<CollectionPageManagerTouchBehavior>());
	}

	// Token: 0x04000796 RID: 1942
	public GameObject m_pageRightArrowBone;

	// Token: 0x04000797 RID: 1943
	public GameObject m_pageLeftArrowBone;

	// Token: 0x04000798 RID: 1944
	public PegUIElement m_pageRightClickableRegion;

	// Token: 0x04000799 RID: 1945
	public PegUIElement m_pageLeftClickableRegion;

	// Token: 0x0400079A RID: 1946
	public PegUIElement m_pageDraggableRegion;

	// Token: 0x0400079B RID: 1947
	public BookPageDisplay m_pageDisplayPrefab;

	// Token: 0x0400079C RID: 1948
	public PageTurn m_pageTurn;

	// Token: 0x0400079D RID: 1949
	public float m_turnLeftPageSwapTiming;

	// Token: 0x0400079E RID: 1950
	private static readonly Vector3 CURRENT_PAGE_LOCAL_POS = new Vector3(0f, 0.25f, 0f);

	// Token: 0x0400079F RID: 1951
	private static readonly Vector3 NEXT_PAGE_LOCAL_POS = new Vector3(-300f, 0f, -300f);

	// Token: 0x040007A0 RID: 1952
	private static readonly float ARROW_SCALE_TIME = 0.6f;

	// Token: 0x040007A1 RID: 1953
	private static readonly string SHOW_ARROWS_COROUTINE_NAME = "WaitThenShowArrows";

	// Token: 0x040007A4 RID: 1956
	protected BookPageDisplay m_pageA;

	// Token: 0x040007A5 RID: 1957
	protected BookPageDisplay m_pageB;

	// Token: 0x040007A6 RID: 1958
	protected int m_currentPageNum;

	// Token: 0x040007A7 RID: 1959
	protected int m_transitionPageId;

	// Token: 0x040007A8 RID: 1960
	protected bool m_currentPageIsPageA;

	// Token: 0x040007A9 RID: 1961
	private bool m_fullyLoaded;

	// Token: 0x040007AA RID: 1962
	protected bool m_skipNextPageTurn;

	// Token: 0x040007AB RID: 1963
	protected PagingArrow m_pageRightArrow;

	// Token: 0x040007AC RID: 1964
	protected PagingArrow m_pageLeftArrow;

	// Token: 0x040007AD RID: 1965
	private bool m_rightArrowShown;

	// Token: 0x040007AE RID: 1966
	private bool m_leftArrowShown;

	// Token: 0x040007AF RID: 1967
	protected bool m_hasPreviousPage;

	// Token: 0x040007B0 RID: 1968
	protected bool m_hasNextPage;

	// Token: 0x040007B1 RID: 1969
	private bool m_delayShowingArrows;

	// Token: 0x040007B2 RID: 1970
	private bool m_pageTurnDisabled;

	// Token: 0x040007B3 RID: 1971
	protected bool m_pagesCurrentlyTurning;

	// Token: 0x040007B4 RID: 1972
	protected bool m_wasTouchModeEnabled;

	// Token: 0x040007B5 RID: 1973
	private readonly PlatformDependentValue<bool> ANIMATE_PAGE_TRANSITIONS = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		iOS = false,
		Android = true,
		PC = true,
		Mac = true
	};

	// Token: 0x040007B6 RID: 1974
	private readonly PlatformDependentValue<bool> ANIMATE_PAGE_TRANSITIONS_MEMORY_OVERRIDE = new PlatformDependentValue<bool>(PlatformCategory.Memory)
	{
		LowMemory = false,
		MediumMemory = false,
		HighMemory = true
	};

	// Token: 0x020013C3 RID: 5059
	public enum PageTransitionType
	{
		// Token: 0x0400A7C2 RID: 42946
		NONE,
		// Token: 0x0400A7C3 RID: 42947
		SINGLE_PAGE_RIGHT,
		// Token: 0x0400A7C4 RID: 42948
		SINGLE_PAGE_LEFT,
		// Token: 0x0400A7C5 RID: 42949
		MANY_PAGE_RIGHT,
		// Token: 0x0400A7C6 RID: 42950
		MANY_PAGE_LEFT
	}

	// Token: 0x020013C4 RID: 5060
	// (Invoke) Token: 0x0600D889 RID: 55433
	public delegate void DelOnPageTransitionComplete(object callbackData);

	// Token: 0x020013C5 RID: 5061
	// (Invoke) Token: 0x0600D88D RID: 55437
	public delegate void PageTurnStartCallback(BookPageManager.PageTransitionType transitionType);

	// Token: 0x020013C6 RID: 5062
	// (Invoke) Token: 0x0600D891 RID: 55441
	public delegate void PageTurnCompleteCallback(int currentPageNum);

	// Token: 0x020013C7 RID: 5063
	protected class TransitionReadyCallbackData
	{
		// Token: 0x0400A7C7 RID: 42951
		public BookPageDisplay m_assembledPage;

		// Token: 0x0400A7C8 RID: 42952
		public BookPageDisplay m_otherPage;

		// Token: 0x0400A7C9 RID: 42953
		public BookPageManager.PageTransitionType m_transitionType;

		// Token: 0x0400A7CA RID: 42954
		public BookPageManager.DelOnPageTransitionComplete m_callback;

		// Token: 0x0400A7CB RID: 42955
		public object m_callbackData;
	}
}
