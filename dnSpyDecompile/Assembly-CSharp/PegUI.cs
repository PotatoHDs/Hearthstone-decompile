using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x02000AD9 RID: 2777
public class PegUI : MonoBehaviour
{
	// Token: 0x14000093 RID: 147
	// (add) Token: 0x060093E1 RID: 37857 RVA: 0x00300364 File Offset: 0x002FE564
	// (remove) Token: 0x060093E2 RID: 37858 RVA: 0x00300398 File Offset: 0x002FE598
	public static event Action<PegUIElement> OnReleasePreTrigger;

	// Token: 0x060093E3 RID: 37859 RVA: 0x003003CB File Offset: 0x002FE5CB
	private void Awake()
	{
		PegUI.s_instance = this;
		this.m_lastClickPosition = Vector3.zero;
		base.gameObject.AddComponent<HSDontDestroyOnLoad>();
	}

	// Token: 0x060093E4 RID: 37860 RVA: 0x003003EC File Offset: 0x002FE5EC
	private void OnDestroy()
	{
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().UnregisterMouseOnOrOffScreenListener(new UniversalInputManager.MouseOnOrOffScreenCallback(this.OnMouseOnOrOffScreen));
		}
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.RemoveFocusChangedListener(new HearthstoneApplication.FocusChangedCallback(this.OnAppFocusChanged));
		}
		PegUI.s_instance = null;
	}

	// Token: 0x060093E5 RID: 37861 RVA: 0x00300440 File Offset: 0x002FE640
	private void Start()
	{
		Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("PegUI.RegisterMouseOnOrOffScreenListener", new Action(this.RegisterMouseListener), new IJobDependency[]
		{
			HearthstoneServices.CreateServiceDependency(typeof(UniversalInputManager))
		}));
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.AddFocusChangedListener(new HearthstoneApplication.FocusChangedCallback(this.OnAppFocusChanged));
		}
	}

	// Token: 0x060093E6 RID: 37862 RVA: 0x003004A3 File Offset: 0x002FE6A3
	private void Update()
	{
		this.MouseInputUpdate();
	}

	// Token: 0x060093E7 RID: 37863 RVA: 0x003004AB File Offset: 0x002FE6AB
	public static PegUI Get()
	{
		return PegUI.s_instance;
	}

	// Token: 0x060093E8 RID: 37864 RVA: 0x003004B2 File Offset: 0x002FE6B2
	public static bool IsInitialized()
	{
		return PegUI.s_instance != null;
	}

	// Token: 0x060093E9 RID: 37865 RVA: 0x003004BF File Offset: 0x002FE6BF
	public PegUIElement GetMousedOverElement()
	{
		return this.m_currentElement;
	}

	// Token: 0x060093EA RID: 37866 RVA: 0x003004C7 File Offset: 0x002FE6C7
	public PegUIElement GetMouseDownElement()
	{
		return this.m_mouseDownElement;
	}

	// Token: 0x060093EB RID: 37867 RVA: 0x003004CF File Offset: 0x002FE6CF
	public PegUIElement GetPrevMousedOverElement()
	{
		return this.m_prevElement;
	}

	// Token: 0x060093EC RID: 37868 RVA: 0x003004D7 File Offset: 0x002FE6D7
	public void AddInputCamera(Camera cam)
	{
		if (cam == null)
		{
			Debug.Log("Trying to add a null camera!");
			return;
		}
		this.m_UICams.Add(cam);
	}

	// Token: 0x060093ED RID: 37869 RVA: 0x003004F9 File Offset: 0x002FE6F9
	public void RemoveInputCamera(Camera cam)
	{
		if (cam != null)
		{
			this.m_UICams.Remove(cam);
		}
	}

	// Token: 0x060093EE RID: 37870 RVA: 0x00300514 File Offset: 0x002FE714
	public PegUIElement FindHitElement()
	{
		RaycastHit raycastHit;
		return this.FindHitElement(out raycastHit);
	}

	// Token: 0x060093EF RID: 37871 RVA: 0x0030052C File Offset: 0x002FE72C
	public PegUIElement FindHitElement(out RaycastHit hit)
	{
		UniversalInputManager universalInputManager = UniversalInputManager.Get();
		if (universalInputManager.IsTouchMode() && !universalInputManager.GetMouseButton(0) && !universalInputManager.GetMouseButtonUp(0))
		{
			hit = default(RaycastHit);
			return null;
		}
		SceneDebugger sceneDebugger = null;
		if (HearthstoneServices.TryGet<SceneDebugger>(out sceneDebugger) && sceneDebugger.IsMouseOverGui())
		{
			hit = default(RaycastHit);
			return null;
		}
		if (this.m_newHitDetectionComponents.Count > 0 && universalInputManager.GetInputHitInfoByCameraDepth(out hit))
		{
			return this.TryGetPegUIElementFromHit(hit);
		}
		foreach (GameLayer layer in PegUI.HIT_TEST_PRIORITY)
		{
			if (universalInputManager.GetInputHitInfo(layer, out hit))
			{
				return this.TryGetPegUIElementFromHit(hit);
			}
		}
		for (int j = this.m_UICams.Count - 1; j >= 0; j--)
		{
			Camera camera = this.m_UICams[j];
			if (camera == null)
			{
				this.m_UICams.RemoveAt(j);
			}
			else if (universalInputManager.GetInputHitInfo(camera, out hit))
			{
				return this.TryGetPegUIElementFromHit(hit);
			}
		}
		hit = default(RaycastHit);
		return null;
	}

	// Token: 0x060093F0 RID: 37872 RVA: 0x00300638 File Offset: 0x002FE838
	private PegUIElement TryGetPegUIElementFromHit(RaycastHit hit)
	{
		PegUIElement component = hit.transform.GetComponent<PegUIElement>();
		if (component != null)
		{
			return component;
		}
		PegUIElementProxy component2 = hit.transform.GetComponent<PegUIElementProxy>();
		if (component2 != null)
		{
			return component2.Owner;
		}
		return null;
	}

	// Token: 0x060093F1 RID: 37873 RVA: 0x0030067B File Offset: 0x002FE87B
	public void DoMouseDown(PegUIElement element, Vector3 mouseDownPos)
	{
		this.m_currentElement = element;
		this.m_mouseDownElement = element;
		this.m_currentElement.TriggerPress();
		this.m_lastClickPosition = mouseDownPos;
		if (this.IsDragAmountAboveDragTolerance(this.m_currentElement))
		{
			this.m_currentElement.TriggerDrag();
		}
	}

	// Token: 0x060093F2 RID: 37874 RVA: 0x003006B8 File Offset: 0x002FE8B8
	public void RemoveAsMouseDownElement(PegUIElement element)
	{
		if (this.m_mouseDownElement == null || element != this.m_mouseDownElement)
		{
			return;
		}
		this.m_mouseDownElement.TriggerReleaseAll(this.m_currentElement == this.m_mouseDownElement);
		this.m_mouseDownElement = null;
	}

	// Token: 0x060093F3 RID: 37875 RVA: 0x00300708 File Offset: 0x002FE908
	private void MouseInputUpdate()
	{
		if (UniversalInputManager.Get() == null)
		{
			return;
		}
		if (!this.m_hasFocus || this.m_uguiActive)
		{
			return;
		}
		bool flag = false;
		using (List<PegUICustomBehavior>.Enumerator enumerator = this.m_customBehaviors.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.UpdateUI())
				{
					flag = true;
					break;
				}
			}
		}
		if (flag)
		{
			if (this.m_mouseDownElement != null)
			{
				this.m_mouseDownElement.TriggerOut();
			}
			this.m_mouseDownElement = null;
			return;
		}
		if (UniversalInputManager.Get().GetMouseButton(0) && this.m_mouseDownElement != null && this.m_lastClickPosition != Vector3.zero && this.IsDragAmountAboveDragTolerance(this.m_mouseDownElement))
		{
			this.m_mouseDownElement.TriggerDrag();
		}
		if (this.m_lastClickTimer != -1f)
		{
			this.m_lastClickTimer += Time.deltaTime;
		}
		if (this.m_mouseDownTimer != -1f)
		{
			this.m_mouseDownTimer += Time.deltaTime;
		}
		PegUIElement pegUIElement = this.FindHitElement();
		if (pegUIElement != null && HearthstoneApplication.IsInternal() && Options.Get().GetInt(Option.PEGUI_DEBUG) >= 3)
		{
			Debug.Log(string.Format("{0,-7} {1}", "HIT:", DebugUtils.GetHierarchyPath(pegUIElement, '/')));
		}
		bool flag2 = !UniversalInputManager.Get().IsTouchMode() || UniversalInputManager.Get().GetMouseButton(0);
		if (flag2)
		{
			this.m_prevElement = this.m_currentElement;
		}
		if (pegUIElement && pegUIElement.IsEnabled())
		{
			this.m_currentElement = pegUIElement;
		}
		else if (flag2)
		{
			this.m_currentElement = null;
		}
		if (this.m_prevElement && this.m_currentElement != this.m_prevElement)
		{
			if (PegCursor.Get() != null)
			{
				PegCursor.Get().SetMode(PegCursor.Mode.UP);
			}
			this.m_prevElement.TriggerOut();
			this.m_lastClickTimer = -1f;
		}
		if (UniversalInputManager.Get().GetMouseButton(0) && this.m_mouseDownElement != null && this.m_currentElement == this.m_mouseDownElement && this.m_mouseDownTimer > 0.45f)
		{
			this.m_mouseDownElement.TriggerHold();
		}
		if (this.m_currentElement == null)
		{
			if (PegCursor.Get() != null)
			{
				if (UniversalInputManager.Get().GetMouseButtonDown(0))
				{
					PegCursor.Get().SetMode(PegCursor.Mode.DOWN);
				}
				else if (!UniversalInputManager.Get().GetMouseButton(0))
				{
					PegCursor.Get().SetMode(PegCursor.Mode.UP);
				}
			}
			if (this.m_mouseDownElement && UniversalInputManager.Get().GetMouseButtonUp(0))
			{
				this.m_mouseDownElement.TriggerReleaseAll(false);
				this.m_mouseDownElement = null;
			}
			return;
		}
		if (!this.UpdateMouseLeftClick())
		{
			this.UpdateMouseLeftHold();
		}
		this.UpdateMouseRightClick();
		this.UpdateMouseOver();
	}

	// Token: 0x060093F4 RID: 37876 RVA: 0x003009DC File Offset: 0x002FEBDC
	private void RegisterMouseListener()
	{
		UniversalInputManager.Get().RegisterMouseOnOrOffScreenListener(new UniversalInputManager.MouseOnOrOffScreenCallback(this.OnMouseOnOrOffScreen));
	}

	// Token: 0x060093F5 RID: 37877 RVA: 0x003009F8 File Offset: 0x002FEBF8
	private bool UpdateMouseLeftClick()
	{
		bool result = false;
		if (UniversalInputManager.Get().GetMouseButtonDown(0))
		{
			result = true;
			if (PegCursor.Get() != null)
			{
				if (this.m_currentElement.GetCursorDown() != PegCursor.Mode.NONE)
				{
					PegCursor.Get().SetMode(this.m_currentElement.GetCursorDown());
				}
				else
				{
					PegCursor.Get().SetMode(PegCursor.Mode.DOWN);
				}
			}
			this.m_mouseDownTimer = 0f;
			if (UniversalInputManager.Get().IsTouchMode() && this.m_currentElement.GetReceiveOverWithMouseDown())
			{
				this.m_currentElement.TriggerOver();
			}
			this.m_currentElement.TriggerPress();
			this.m_lastClickPosition = UniversalInputManager.Get().GetMousePosition();
			this.m_mouseDownElement = this.m_currentElement;
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			result = true;
			if (this.m_lastClickTimer > 0f && this.m_lastClickTimer <= 0.7f && this.m_currentElement.DoubleClickEnabled)
			{
				this.m_currentElement.TriggerDoubleClick();
				this.m_lastClickTimer = -1f;
			}
			else
			{
				if (this.m_mouseDownElement == this.m_currentElement || this.m_currentElement.GetReceiveReleaseWithoutMouseDown())
				{
					if (this.m_mouseDownTimer <= 0.4f)
					{
						this.m_currentElement.TriggerTap();
					}
					if (PegUI.OnReleasePreTrigger != null)
					{
						PegUI.OnReleasePreTrigger(this.m_currentElement);
					}
					this.m_currentElement.TriggerRelease();
				}
				if (this.m_mouseDownElement)
				{
					this.m_lastClickTimer = 0f;
					this.m_mouseDownElement.TriggerReleaseAll(this.m_currentElement == this.m_mouseDownElement);
					this.m_mouseDownElement = null;
				}
			}
			if (this.m_currentElement.GetReceiveOverWithMouseDown())
			{
				this.m_currentElement.TriggerOut();
			}
			if (PegCursor.Get() != null)
			{
				PegCursor.Get().SetMode((this.m_currentElement.GetCursorOver() != PegCursor.Mode.NONE) ? this.m_currentElement.GetCursorOver() : PegCursor.Mode.OVER);
			}
			this.m_mouseDownTimer = -1f;
			this.m_lastClickPosition = Vector3.zero;
			if (UniversalInputManager.Get().IsTouchMode())
			{
				this.m_currentElement = null;
				this.m_prevElement = null;
			}
		}
		return result;
	}

	// Token: 0x060093F6 RID: 37878 RVA: 0x00300C14 File Offset: 0x002FEE14
	private bool UpdateMouseLeftHold()
	{
		if (!UniversalInputManager.Get().GetMouseButton(0))
		{
			return false;
		}
		if (this.m_currentElement.GetReceiveOverWithMouseDown() && this.m_currentElement != this.m_prevElement)
		{
			if (PegCursor.Get() != null)
			{
				if (this.m_currentElement.GetCursorOver() != PegCursor.Mode.NONE)
				{
					PegCursor.Get().SetMode(this.m_currentElement.GetCursorOver());
				}
				else
				{
					PegCursor.Get().SetMode(PegCursor.Mode.OVER);
				}
			}
			this.m_currentElement.TriggerOver();
		}
		return true;
	}

	// Token: 0x060093F7 RID: 37879 RVA: 0x00300C9C File Offset: 0x002FEE9C
	private bool UpdateMouseRightClick()
	{
		bool result = false;
		if (UniversalInputManager.Get().GetMouseButtonDown(1))
		{
			result = true;
			if (this.m_currentElement != null)
			{
				this.m_currentElement.TriggerRightClick();
			}
		}
		return result;
	}

	// Token: 0x060093F8 RID: 37880 RVA: 0x00300CD4 File Offset: 0x002FEED4
	private void UpdateMouseOver()
	{
		if (this.m_currentElement == null)
		{
			return;
		}
		if (UniversalInputManager.Get().IsTouchMode() && (!UniversalInputManager.Get().GetMouseButton(0) || !this.m_currentElement.GetReceiveOverWithMouseDown()))
		{
			return;
		}
		if (this.m_currentElement == this.m_prevElement)
		{
			return;
		}
		if (PegCursor.Get() != null)
		{
			if (this.m_currentElement.GetCursorOver() != PegCursor.Mode.NONE)
			{
				PegCursor.Get().SetMode(this.m_currentElement.GetCursorOver());
			}
			else
			{
				PegCursor.Get().SetMode(PegCursor.Mode.OVER);
			}
		}
		this.m_currentElement.TriggerOver();
	}

	// Token: 0x060093F9 RID: 37881 RVA: 0x00300D74 File Offset: 0x002FEF74
	private void OnAppFocusChanged(bool focus, object userData)
	{
		this.m_hasFocus = focus;
	}

	// Token: 0x060093FA RID: 37882 RVA: 0x00300D7D File Offset: 0x002FEF7D
	public void OnUGUIActiveChanged(bool active)
	{
		this.m_uguiActive = active;
	}

	// Token: 0x060093FB RID: 37883 RVA: 0x00300D88 File Offset: 0x002FEF88
	private void OnMouseOnOrOffScreen(bool onScreen)
	{
		if (onScreen)
		{
			return;
		}
		this.m_lastClickPosition = Vector3.zero;
		if (this.m_currentElement != null)
		{
			this.m_currentElement.TriggerOut();
			this.m_currentElement = null;
		}
		if (PegCursor.Get() != null)
		{
			PegCursor.Get().SetMode(PegCursor.Mode.UP);
		}
		if (this.m_prevElement != null)
		{
			this.m_prevElement.TriggerOut();
			this.m_prevElement = null;
		}
		this.m_lastClickTimer = -1f;
	}

	// Token: 0x060093FC RID: 37884 RVA: 0x00300E08 File Offset: 0x002FF008
	private bool IsDragAmountAboveDragTolerance(PegUIElement draggedElement)
	{
		Vector3 vector = UniversalInputManager.Get().GetMousePosition() - this.m_lastClickPosition;
		Vector3 dragTolerance = draggedElement.GetDragTolerance();
		return (dragTolerance.x != 0f && Mathf.Abs(vector.x) > Mathf.Abs(dragTolerance.x)) || (dragTolerance.y != 0f && Mathf.Abs(vector.y) > Mathf.Abs(dragTolerance.y)) || (dragTolerance.z != 0f && Mathf.Abs(vector.z) > Mathf.Abs(dragTolerance.z));
	}

	// Token: 0x060093FD RID: 37885 RVA: 0x00300EA5 File Offset: 0x002FF0A5
	public void EnableSwipeDetection(Rect swipeRect, PegUI.DelSwipeListener listener)
	{
		this.m_swipeListener = listener;
	}

	// Token: 0x060093FE RID: 37886 RVA: 0x00300EAE File Offset: 0x002FF0AE
	public void CancelSwipeDetection(PegUI.DelSwipeListener listener)
	{
		if (listener == this.m_swipeListener)
		{
			this.m_swipeListener = null;
		}
	}

	// Token: 0x060093FF RID: 37887 RVA: 0x00300EC5 File Offset: 0x002FF0C5
	public void RegisterCustomBehavior(PegUICustomBehavior behavior)
	{
		this.m_customBehaviors.Add(behavior);
	}

	// Token: 0x06009400 RID: 37888 RVA: 0x00300ED3 File Offset: 0x002FF0D3
	public void UnregisterCustomBehavior(PegUICustomBehavior behavior)
	{
		this.m_customBehaviors.Remove(behavior);
	}

	// Token: 0x06009401 RID: 37889 RVA: 0x00300EE2 File Offset: 0x002FF0E2
	public bool IsUsingCameraDepthPriorityHitTest()
	{
		return this.m_newHitDetectionComponents.Count > 0;
	}

	// Token: 0x06009402 RID: 37890 RVA: 0x00300EF2 File Offset: 0x002FF0F2
	public void RegisterForCameraDepthPriorityHitTest(Component component)
	{
		this.m_newHitDetectionComponents.Add(component);
	}

	// Token: 0x06009403 RID: 37891 RVA: 0x00300F00 File Offset: 0x002FF100
	public void UnregisterForCameraDepthPriorityHitTest(Component component)
	{
		this.m_newHitDetectionComponents.Remove(component);
	}

	// Token: 0x04007C24 RID: 31780
	public Camera orthographicUICam;

	// Token: 0x04007C25 RID: 31781
	private static readonly GameLayer[] HIT_TEST_PRIORITY = new GameLayer[]
	{
		GameLayer.IgnoreFullScreenEffects,
		GameLayer.BackgroundUI,
		GameLayer.PerspectiveUI,
		GameLayer.CameraMask,
		GameLayer.UI,
		GameLayer.BattleNet,
		GameLayer.BattleNetFriendList,
		GameLayer.BattleNetChat,
		GameLayer.BattleNetDialog,
		GameLayer.HighPriorityUI,
		GameLayer.Reserved29
	};

	// Token: 0x04007C26 RID: 31782
	private List<Camera> m_UICams = new List<Camera>();

	// Token: 0x04007C27 RID: 31783
	private PegUIElement m_prevElement;

	// Token: 0x04007C28 RID: 31784
	private PegUIElement m_currentElement;

	// Token: 0x04007C29 RID: 31785
	private PegUIElement m_mouseDownElement;

	// Token: 0x04007C2A RID: 31786
	private static PegUI s_instance;

	// Token: 0x04007C2B RID: 31787
	private float m_mouseDownTimer;

	// Token: 0x04007C2C RID: 31788
	private float m_lastClickTimer;

	// Token: 0x04007C2D RID: 31789
	private Vector3 m_lastClickPosition;

	// Token: 0x04007C2E RID: 31790
	private const float PRESS_VS_TAP_TOLERANCE = 0.4f;

	// Token: 0x04007C2F RID: 31791
	private const float HOLD_TOLERANCE = 0.45f;

	// Token: 0x04007C30 RID: 31792
	private const float DOUBLECLICK_TOLERANCE = 0.7f;

	// Token: 0x04007C31 RID: 31793
	private const float DOUBLECLICK_COUNT_DISABLED = -1f;

	// Token: 0x04007C32 RID: 31794
	private const float MOUSEDOWN_COUNT_DISABLED = -1f;

	// Token: 0x04007C33 RID: 31795
	private List<PegUICustomBehavior> m_customBehaviors = new List<PegUICustomBehavior>();

	// Token: 0x04007C34 RID: 31796
	private List<Component> m_newHitDetectionComponents = new List<Component>();

	// Token: 0x04007C35 RID: 31797
	private PegUI.DelSwipeListener m_swipeListener;

	// Token: 0x04007C36 RID: 31798
	private bool m_hasFocus = true;

	// Token: 0x04007C37 RID: 31799
	private bool m_uguiActive;

	// Token: 0x02002711 RID: 10001
	public enum Layer
	{
		// Token: 0x0400F34C RID: 62284
		MANUAL,
		// Token: 0x0400F34D RID: 62285
		RELATIVE_TO_PARENT,
		// Token: 0x0400F34E RID: 62286
		BACKGROUND,
		// Token: 0x0400F34F RID: 62287
		HUD,
		// Token: 0x0400F350 RID: 62288
		DIALOG
	}

	// Token: 0x02002712 RID: 10002
	public enum SWIPE_DIRECTION
	{
		// Token: 0x0400F352 RID: 62290
		RIGHT,
		// Token: 0x0400F353 RID: 62291
		LEFT
	}

	// Token: 0x02002713 RID: 10003
	// (Invoke) Token: 0x060138D4 RID: 80084
	public delegate void DelSwipeListener(PegUI.SWIPE_DIRECTION direction);
}
