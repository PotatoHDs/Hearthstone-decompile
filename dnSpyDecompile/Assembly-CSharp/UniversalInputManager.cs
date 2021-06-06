using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x02000938 RID: 2360
public class UniversalInputManager : IService, IHasUpdate
{
	// Token: 0x1700076C RID: 1900
	// (get) Token: 0x0600824F RID: 33359 RVA: 0x002A5906 File Offset: 0x002A3B06
	private GameObject SceneObject
	{
		get
		{
			if (this.m_sceneObject == null)
			{
				this.m_sceneObject = new GameObject("UniversalInputManagerSceneObject", new Type[]
				{
					typeof(HSDontDestroyOnLoad)
				});
			}
			return this.m_sceneObject;
		}
	}

	// Token: 0x06008250 RID: 33360 RVA: 0x002A593F File Offset: 0x002A3B3F
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		Processor.QueueJob("UniversalInputManager.SetupGUISkin", this.SetupGUISkin(), new IJobDependency[]
		{
			HearthstoneServices.CreateServiceDependency(typeof(IAssetLoader))
		});
		this.CreateHitTestPriorityMap();
		this.m_mouseOnScreen = InputUtil.IsMouseOnScreen();
		Processor.RegisterOnGUIDelegate(new Action(this.OnGUI));
		this.m_shouldHandleCheats = !HearthstoneApplication.IsPublic();
		this.UpdateIsTouchMode();
		Options.Get().RegisterChangedListener(Option.TOUCH_MODE, new Options.ChangedCallback(this.OnTouchModeChangedCallback));
		yield break;
	}

	// Token: 0x06008251 RID: 33361 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x06008252 RID: 33362 RVA: 0x002A594E File Offset: 0x002A3B4E
	public void Shutdown()
	{
		UniversalInputManager.s_instance = null;
		Options.Get().UnregisterChangedListener(Option.TOUCH_MODE, new Options.ChangedCallback(this.OnTouchModeChangedCallback));
	}

	// Token: 0x06008253 RID: 33363 RVA: 0x002A596F File Offset: 0x002A3B6F
	public void Update()
	{
		this.UpdateCamerasByDepth();
		this.UpdateMouseOnOrOffScreen();
		this.UpdateInput();
		this.CleanDeadCameras();
	}

	// Token: 0x06008254 RID: 33364 RVA: 0x002A5989 File Offset: 0x002A3B89
	private void OnGUI()
	{
		this.IgnoreGUIInput();
		this.HandleGUIInputInactive();
		this.HandleGUIInputActive();
	}

	// Token: 0x06008255 RID: 33365 RVA: 0x002A599E File Offset: 0x002A3B9E
	public static UniversalInputManager Get()
	{
		if (UniversalInputManager.s_instance == null)
		{
			UniversalInputManager.s_instance = HearthstoneServices.Get<UniversalInputManager>();
		}
		return UniversalInputManager.s_instance;
	}

	// Token: 0x06008256 RID: 33366 RVA: 0x002A59B8 File Offset: 0x002A3BB8
	public void SetGUISkin(GUISkinContainer skinContainer)
	{
		if (this.m_skinContainer != null)
		{
			UnityEngine.Object.Destroy(this.m_skinContainer.gameObject);
		}
		this.m_skinContainer = skinContainer;
		this.m_skinContainer.transform.parent = this.SceneObject.transform;
		this.m_skin = skinContainer.GetGUISkin();
		this.m_defaultInputAlignment = this.m_skin.textField.alignment;
		this.m_defaultInputFont = this.m_skin.textField.font;
	}

	// Token: 0x06008257 RID: 33367 RVA: 0x002A5A3D File Offset: 0x002A3C3D
	public bool IsTouchMode()
	{
		return this.m_isTouchMode;
	}

	// Token: 0x06008258 RID: 33368 RVA: 0x002A5A45 File Offset: 0x002A3C45
	private void UpdateIsTouchMode()
	{
		this.m_isTouchMode = (UniversalInputManager.IsTouchDevice || Options.Get().GetBool(Option.TOUCH_MODE));
	}

	// Token: 0x06008259 RID: 33369 RVA: 0x002A5A68 File Offset: 0x002A3C68
	public bool UseWindowsTouch()
	{
		return this.IsTouchMode() && !PlatformSettings.IsEmulating;
	}

	// Token: 0x0600825A RID: 33370 RVA: 0x002A5A7C File Offset: 0x002A3C7C
	public bool WasTouchCanceled()
	{
		if (!UniversalInputManager.IsTouchDevice)
		{
			return false;
		}
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Canceled)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600825B RID: 33371 RVA: 0x002A5AC0 File Offset: 0x002A3CC0
	public bool IsMouseOnScreen()
	{
		return this.m_mouseOnScreen;
	}

	// Token: 0x0600825C RID: 33372 RVA: 0x002A5AC8 File Offset: 0x002A3CC8
	public bool RegisterMouseOnOrOffScreenListener(UniversalInputManager.MouseOnOrOffScreenCallback listener)
	{
		if (this.m_mouseOnOrOffScreenListeners.Contains(listener))
		{
			return false;
		}
		this.m_mouseOnOrOffScreenListeners.Add(listener);
		return true;
	}

	// Token: 0x0600825D RID: 33373 RVA: 0x002A5AE7 File Offset: 0x002A3CE7
	public bool UnregisterMouseOnOrOffScreenListener(UniversalInputManager.MouseOnOrOffScreenCallback listener)
	{
		return this.m_mouseOnOrOffScreenListeners.Remove(listener);
	}

	// Token: 0x0600825E RID: 33374 RVA: 0x002A5AF5 File Offset: 0x002A3CF5
	public void SetGameDialogActive(bool active)
	{
		this.m_gameDialogActive = active;
	}

	// Token: 0x0600825F RID: 33375 RVA: 0x002A5AFE File Offset: 0x002A3CFE
	public void SetSystemDialogActive(bool active)
	{
		this.m_systemDialogActive = active;
	}

	// Token: 0x06008260 RID: 33376 RVA: 0x002A5B08 File Offset: 0x002A3D08
	public void UseTextInput(UniversalInputManager.TextInputParams parms, bool force = false)
	{
		if (!force && parms.m_owner == this.m_inputOwner)
		{
			return;
		}
		if (this.m_inputOwner != null && this.m_inputOwner != parms.m_owner)
		{
			this.ObjectCancelTextInput(parms.m_owner);
		}
		this.m_inputOwner = parms.m_owner;
		this.m_inputUpdatedCallback = parms.m_updatedCallback;
		this.m_inputPreprocessCallback = parms.m_preprocessCallback;
		this.m_inputCompletedCallback = parms.m_completedCallback;
		this.m_inputCanceledCallback = parms.m_canceledCallback;
		this.m_inputUnfocusedCallback = parms.m_unfocusedCallback;
		this.m_inputPassword = parms.m_password;
		this.m_inputNumber = parms.m_number;
		this.m_inputMultiLine = parms.m_multiLine;
		this.m_inputActive = true;
		this.m_inputFocused = false;
		this.m_inputText = (parms.m_text ?? string.Empty);
		this.m_inputNormalizedRect = parms.m_rect;
		this.m_inputInitialScreenSize.x = (float)Screen.width;
		this.m_inputInitialScreenSize.y = (float)Screen.height;
		this.m_inputMaxCharacters = parms.m_maxCharacters;
		this.m_inputColor = parms.m_color;
		this.m_inputAlignment = (parms.m_alignment ?? this.m_defaultInputAlignment);
		this.m_inputFont = (parms.m_font ?? this.m_defaultInputFont);
		this.m_inputNeedsFocus = true;
		this.m_inputIgnoreState = UniversalInputManager.TextInputIgnoreState.INVALID;
		this.m_inputKeepFocusOnComplete = parms.m_inputKeepFocusOnComplete;
		if (this.IsTextInputPassword())
		{
			Input.imeCompositionMode = IMECompositionMode.Off;
		}
		this.m_hideVirtualKeyboardOnComplete = parms.m_hideVirtualKeyboardOnComplete;
		if (this.UseWindowsTouch() && parms.m_showVirtualKeyboard)
		{
			HearthstoneServices.Get<ITouchScreenService>().ShowKeyboard();
		}
	}

	// Token: 0x06008261 RID: 33377 RVA: 0x002A5CB7 File Offset: 0x002A3EB7
	public void CancelTextInput(GameObject requester, bool force = false)
	{
		if (!this.IsTextInputActive())
		{
			return;
		}
		if (!force && requester != this.m_inputOwner)
		{
			return;
		}
		this.ObjectCancelTextInput(requester);
	}

	// Token: 0x06008262 RID: 33378 RVA: 0x002A5CDB File Offset: 0x002A3EDB
	public void FocusTextInput(GameObject owner)
	{
		if (owner != this.m_inputOwner)
		{
			return;
		}
		if (!this.m_tabKeyDown)
		{
			this.m_inputNeedsFocus = true;
			return;
		}
		this.m_inputNeedsFocusFromTabKeyDown = true;
	}

	// Token: 0x06008263 RID: 33379 RVA: 0x002A5D03 File Offset: 0x002A3F03
	public void UpdateTextInputRect(GameObject owner, Rect rect)
	{
		if (owner != this.m_inputOwner)
		{
			return;
		}
		this.m_inputNormalizedRect = rect;
		this.m_inputInitialScreenSize.x = (float)Screen.width;
		this.m_inputInitialScreenSize.y = (float)Screen.height;
	}

	// Token: 0x06008264 RID: 33380 RVA: 0x002A5D3D File Offset: 0x002A3F3D
	public bool IsTextInputPassword()
	{
		return this.m_inputPassword;
	}

	// Token: 0x06008265 RID: 33381 RVA: 0x002A5D45 File Offset: 0x002A3F45
	public bool IsTextInputActive()
	{
		return this.m_inputActive;
	}

	// Token: 0x06008266 RID: 33382 RVA: 0x002A5D4D File Offset: 0x002A3F4D
	public string GetInputText()
	{
		return this.m_inputText;
	}

	// Token: 0x06008267 RID: 33383 RVA: 0x002A5D58 File Offset: 0x002A3F58
	public void SetInputText(string text, bool moveCursorToEnd = false)
	{
		this.m_inputText = (text ?? string.Empty);
		if (moveCursorToEnd)
		{
			Processor.ScheduleCallback(0f, false, delegate(object u)
			{
				TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
				if (textEditor != null)
				{
					textEditor.MoveTextEnd();
				}
			}, null);
		}
	}

	// Token: 0x06008268 RID: 33384 RVA: 0x002A5DA4 File Offset: 0x002A3FA4
	public bool InputIsOver(GameObject gameObj)
	{
		RaycastHit raycastHit;
		return this.InputIsOver(gameObj, out raycastHit);
	}

	// Token: 0x06008269 RID: 33385 RVA: 0x002A5DBC File Offset: 0x002A3FBC
	public bool InputIsOver(GameObject gameObj, out RaycastHit hitInfo)
	{
		LayerMask mask = ((GameLayer)gameObj.layer).LayerBit();
		Camera camera;
		return this.Raycast(null, mask, out camera, out hitInfo, false) && hitInfo.collider.gameObject == gameObj;
	}

	// Token: 0x0600826A RID: 33386 RVA: 0x002A5DFC File Offset: 0x002A3FFC
	public bool InputIsOver(GameObject gameObj, int layerMask, out RaycastHit hitInfo)
	{
		Camera camera;
		return this.Raycast(null, layerMask, out camera, out hitInfo, false) && hitInfo.collider.gameObject == gameObj;
	}

	// Token: 0x0600826B RID: 33387 RVA: 0x002A5E30 File Offset: 0x002A4030
	public bool InputIsOver(Camera camera, GameObject gameObj)
	{
		RaycastHit raycastHit;
		return this.InputIsOver(camera, gameObj, out raycastHit);
	}

	// Token: 0x0600826C RID: 33388 RVA: 0x002A5E48 File Offset: 0x002A4048
	public bool InputIsOver(Camera camera, GameObject gameObj, out RaycastHit hitInfo)
	{
		LayerMask mask = ((GameLayer)gameObj.layer).LayerBit();
		Camera camera2;
		return this.Raycast(camera, mask, out camera2, out hitInfo, false) && hitInfo.collider.gameObject == gameObj;
	}

	// Token: 0x0600826D RID: 33389 RVA: 0x002A5E87 File Offset: 0x002A4087
	public bool InputIsOverByCameraDepth(GameObject gameObj, out RaycastHit hitInfo)
	{
		return this.GetInputHitInfoByCameraDepth(out hitInfo) && hitInfo.collider.gameObject == gameObj;
	}

	// Token: 0x0600826E RID: 33390 RVA: 0x002A5EA8 File Offset: 0x002A40A8
	public bool ForcedInputIsOver(Camera camera, GameObject gameObj)
	{
		RaycastHit raycastHit;
		return this.ForcedInputIsOver(camera, gameObj, out raycastHit);
	}

	// Token: 0x0600826F RID: 33391 RVA: 0x002A5EC0 File Offset: 0x002A40C0
	public bool ForcedInputIsOver(Camera camera, GameObject gameObj, out RaycastHit hitInfo)
	{
		LayerMask layerMask = ((GameLayer)gameObj.layer).LayerBit();
		return CameraUtils.Raycast(camera, this.GetMousePosition(), layerMask, out hitInfo) && hitInfo.collider.gameObject == gameObj;
	}

	// Token: 0x06008270 RID: 33392 RVA: 0x002A5F04 File Offset: 0x002A4104
	public bool ForcedUnblockableInputIsOver(Camera camera, GameObject gameObj, out RaycastHit hitInfo)
	{
		LayerMask layerMask = ((GameLayer)gameObj.layer).LayerBit();
		hitInfo = default(RaycastHit);
		RaycastHit[] array = null;
		if (!CameraUtils.RaycastAll(camera, this.GetMousePosition(), layerMask, out array))
		{
			return false;
		}
		foreach (RaycastHit raycastHit in array)
		{
			if (raycastHit.collider.gameObject == gameObj)
			{
				hitInfo = raycastHit;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06008271 RID: 33393 RVA: 0x002A5F78 File Offset: 0x002A4178
	public bool InputHitAnyObject(GameLayer layer)
	{
		RaycastHit raycastHit;
		return this.GetInputHitInfo(layer, out raycastHit);
	}

	// Token: 0x06008272 RID: 33394 RVA: 0x002A5F90 File Offset: 0x002A4190
	public bool InputHitAnyObject(LayerMask layerMask)
	{
		RaycastHit raycastHit;
		return this.GetInputHitInfo(layerMask, out raycastHit);
	}

	// Token: 0x06008273 RID: 33395 RVA: 0x002A5FA8 File Offset: 0x002A41A8
	public bool InputHitAnyObject(Camera requestedCamera)
	{
		RaycastHit raycastHit;
		if (requestedCamera == null)
		{
			return this.GetInputHitInfo(out raycastHit);
		}
		return this.GetInputHitInfo(requestedCamera, requestedCamera.cullingMask, out raycastHit);
	}

	// Token: 0x06008274 RID: 33396 RVA: 0x002A5FDC File Offset: 0x002A41DC
	public bool InputHitAnyObject(Camera requestedCamera, GameLayer layer)
	{
		RaycastHit raycastHit;
		return this.GetInputHitInfo(requestedCamera, layer, out raycastHit);
	}

	// Token: 0x06008275 RID: 33397 RVA: 0x002A5FF4 File Offset: 0x002A41F4
	public bool InputHitAnyObject(Camera requestedCamera, LayerMask mask)
	{
		RaycastHit raycastHit;
		return this.GetInputHitInfo(requestedCamera, mask, out raycastHit);
	}

	// Token: 0x06008276 RID: 33398 RVA: 0x002A600B File Offset: 0x002A420B
	public bool GetInputHitInfo(out RaycastHit hitInfo)
	{
		return this.GetInputHitInfo(GameLayer.Default, out hitInfo);
	}

	// Token: 0x06008277 RID: 33399 RVA: 0x002A6015 File Offset: 0x002A4215
	public bool GetInputHitInfo(GameLayer layer, out RaycastHit hitInfo)
	{
		return this.GetInputHitInfo(layer.LayerBit(), out hitInfo);
	}

	// Token: 0x06008278 RID: 33400 RVA: 0x002A602C File Offset: 0x002A422C
	public bool GetInputHitInfo(LayerMask mask, out RaycastHit hitInfo)
	{
		Camera requestedCamera = this.GuessBestHitTestCamera(mask);
		return this.GetInputHitInfo(requestedCamera, mask, out hitInfo);
	}

	// Token: 0x06008279 RID: 33401 RVA: 0x002A604A File Offset: 0x002A424A
	public bool GetInputHitInfo(Camera requestedCamera, out RaycastHit hitInfo)
	{
		if (requestedCamera == null)
		{
			return this.GetInputHitInfo(out hitInfo);
		}
		return this.GetInputHitInfo(requestedCamera, requestedCamera.cullingMask, out hitInfo);
	}

	// Token: 0x0600827A RID: 33402 RVA: 0x002A6070 File Offset: 0x002A4270
	public bool GetInputHitInfo(Camera requestedCamera, GameLayer layer, out RaycastHit hitInfo)
	{
		Camera camera;
		return this.Raycast(requestedCamera, layer.LayerBit(), out camera, out hitInfo, false);
	}

	// Token: 0x0600827B RID: 33403 RVA: 0x002A6094 File Offset: 0x002A4294
	public bool GetInputHitInfo(Camera requestedCamera, LayerMask mask, out RaycastHit hitInfo)
	{
		Camera camera;
		return this.Raycast(requestedCamera, mask, out camera, out hitInfo, false);
	}

	// Token: 0x0600827C RID: 33404 RVA: 0x002A60B0 File Offset: 0x002A42B0
	public bool GetInputHitInfoByCameraDepth(out RaycastHit hitInfo)
	{
		foreach (Camera camera in this.m_sortedByDepthCameras)
		{
			if (!(camera == null))
			{
				LayerMask mask = camera.cullingMask & this.m_hitTestLayerBits;
				if (mask != 0 && camera != null && this.Raycast(camera, mask, out hitInfo))
				{
					return true;
				}
			}
		}
		hitInfo = default(RaycastHit);
		return false;
	}

	// Token: 0x0600827D RID: 33405 RVA: 0x002A6144 File Offset: 0x002A4344
	public bool GetInputHitInfoByCameraDepth(LayerMask layer, out RaycastHit hitInfo)
	{
		foreach (Camera camera in this.m_sortedByDepthCameras)
		{
			LayerMask mask = camera.cullingMask & layer;
			if (mask != 0 && camera != null && this.Raycast(camera, mask, out hitInfo))
			{
				return true;
			}
		}
		hitInfo = default(RaycastHit);
		return false;
	}

	// Token: 0x0600827E RID: 33406 RVA: 0x002A61D0 File Offset: 0x002A43D0
	public bool GetInputPointOnPlane(Vector3 origin, out Vector3 point)
	{
		return this.GetInputPointOnPlane(GameLayer.Default, origin, out point);
	}

	// Token: 0x0600827F RID: 33407 RVA: 0x002A61DC File Offset: 0x002A43DC
	public bool GetInputPointOnPlane(GameLayer layer, Vector3 origin, out Vector3 point)
	{
		point = Vector3.zero;
		LayerMask mask = layer.LayerBit();
		Camera camera;
		RaycastHit raycastHit;
		if (!this.Raycast(null, mask, out camera, out raycastHit, false))
		{
			return false;
		}
		Ray ray = camera.ScreenPointToRay(this.GetMousePosition());
		Vector3 inNormal = -camera.transform.forward;
		Plane plane = new Plane(inNormal, origin);
		float distance;
		if (!plane.Raycast(ray, out distance))
		{
			return false;
		}
		point = ray.GetPoint(distance);
		return true;
	}

	// Token: 0x06008280 RID: 33408 RVA: 0x002A6258 File Offset: 0x002A4458
	public bool CanHitTestOffCamera(GameLayer layer)
	{
		return this.CanHitTestOffCamera(layer.LayerBit());
	}

	// Token: 0x06008281 RID: 33409 RVA: 0x002A626B File Offset: 0x002A446B
	public bool CanHitTestOffCamera(LayerMask layerMask)
	{
		return (this.m_offCameraHitTestMask & layerMask) != 0;
	}

	// Token: 0x06008282 RID: 33410 RVA: 0x002A627D File Offset: 0x002A447D
	public void EnableHitTestOffCamera(GameLayer layer, bool enable)
	{
		this.EnableHitTestOffCamera(layer.LayerBit(), enable);
	}

	// Token: 0x06008283 RID: 33411 RVA: 0x002A6291 File Offset: 0x002A4491
	public void EnableHitTestOffCamera(LayerMask mask, bool enable)
	{
		if (enable)
		{
			this.m_offCameraHitTestMask |= mask;
			return;
		}
		this.m_offCameraHitTestMask &= ~mask;
	}

	// Token: 0x06008284 RID: 33412 RVA: 0x002A62BE File Offset: 0x002A44BE
	public void SetCurrentFullScreenEffect(FullScreenEffects effect)
	{
		this.m_currentFullScreenEffect = effect;
	}

	// Token: 0x06008285 RID: 33413 RVA: 0x002A62C7 File Offset: 0x002A44C7
	public bool GetMouseButton(int button)
	{
		if (this.UseWindowsTouch())
		{
			return HearthstoneServices.Get<ITouchScreenService>().GetTouch(button);
		}
		return InputCollection.GetMouseButton(button);
	}

	// Token: 0x06008286 RID: 33414 RVA: 0x002A62E3 File Offset: 0x002A44E3
	public bool GetMouseButtonDown(int button)
	{
		if (this.UseWindowsTouch())
		{
			return HearthstoneServices.Get<ITouchScreenService>().GetTouchDown(button);
		}
		return InputCollection.GetMouseButtonDown(button);
	}

	// Token: 0x06008287 RID: 33415 RVA: 0x002A62FF File Offset: 0x002A44FF
	public bool GetMouseButtonUp(int button)
	{
		if (this.UseWindowsTouch())
		{
			return HearthstoneServices.Get<ITouchScreenService>().GetTouchUp(button);
		}
		return InputCollection.GetMouseButtonUp(button);
	}

	// Token: 0x06008288 RID: 33416 RVA: 0x002A631B File Offset: 0x002A451B
	public Vector3 GetMousePosition()
	{
		if (this.UseWindowsTouch())
		{
			return HearthstoneServices.Get<ITouchScreenService>().GetTouchPosition();
		}
		return InputCollection.GetMousePosition();
	}

	// Token: 0x06008289 RID: 33417 RVA: 0x002A6335 File Offset: 0x002A4535
	public bool AddCameraMaskCamera(Camera camera)
	{
		if (this.m_cameraMaskCameras.Contains(camera))
		{
			return false;
		}
		this.m_cameraMaskCameras.Add(camera);
		return true;
	}

	// Token: 0x0600828A RID: 33418 RVA: 0x002A6354 File Offset: 0x002A4554
	public bool RemoveCameraMaskCamera(Camera camera)
	{
		return this.m_cameraMaskCameras.Remove(camera);
	}

	// Token: 0x0600828B RID: 33419 RVA: 0x002A6362 File Offset: 0x002A4562
	public bool AddIgnoredCamera(Camera camera)
	{
		if (this.m_ignoredCameras.Contains(camera))
		{
			return false;
		}
		this.m_ignoredCameras.Add(camera);
		return true;
	}

	// Token: 0x0600828C RID: 33420 RVA: 0x002A6381 File Offset: 0x002A4581
	public bool RemoveIgnoredCamera(Camera camera)
	{
		return this.m_ignoredCameras.Remove(camera);
	}

	// Token: 0x0600828D RID: 33421 RVA: 0x002A6390 File Offset: 0x002A4590
	private void CreateHitTestPriorityMap()
	{
		this.m_hitTestPriorityMap = new Map<GameLayer, int>();
		int num = 1;
		for (int i = 0; i < UniversalInputManager.HIT_TEST_PRIORITY_ORDER.Length; i++)
		{
			GameLayer key = UniversalInputManager.HIT_TEST_PRIORITY_ORDER[i];
			this.m_hitTestPriorityMap.Add(key, num++);
		}
		foreach (object obj in Enum.GetValues(typeof(GameLayer)))
		{
			GameLayer gameLayer = (GameLayer)obj;
			this.m_hitTestLayerBits |= gameLayer.LayerBit();
			if (!this.m_hitTestPriorityMap.ContainsKey(gameLayer))
			{
				this.m_hitTestPriorityMap.Add(gameLayer, 0);
			}
		}
		int num2 = 0;
		foreach (GameLayer gameLayer2 in UniversalInputManager.IGNORE_HIT_TEST_LAYERS)
		{
			num2 |= gameLayer2.LayerBit();
		}
		this.m_hitTestLayerBits &= ~num2;
	}

	// Token: 0x0600828E RID: 33422 RVA: 0x002A6498 File Offset: 0x002A4698
	private void UpdateCamerasByDepth()
	{
		this.m_sortedByDepthCameras.Clear();
		this.m_sortedByDepthCameras.AddRange(from cam in Camera.allCameras
		orderby cam.depth descending
		select cam);
	}

	// Token: 0x0600828F RID: 33423 RVA: 0x002A64E4 File Offset: 0x002A46E4
	private void CleanDeadCameras()
	{
		GeneralUtils.CleanDeadObjectsFromList<Camera>(this.m_cameraMaskCameras);
		GeneralUtils.CleanDeadObjectsFromList<Camera>(this.m_ignoredCameras);
	}

	// Token: 0x06008290 RID: 33424 RVA: 0x002A64FC File Offset: 0x002A46FC
	private IEnumerator<IAsyncJobResult> SetupGUISkin()
	{
		InstantiatePrefab loadGUISkin = new InstantiatePrefab("TextInputGUISkin.prefab:506d0b8720fda4ac298d5ea645aaedd5");
		yield return loadGUISkin;
		this.SetGUISkin(loadGUISkin.InstantiatedPrefab.GetComponent<GUISkinContainer>());
		yield return null;
		yield break;
	}

	// Token: 0x06008291 RID: 33425 RVA: 0x002A650C File Offset: 0x002A470C
	private Camera GuessBestHitTestCamera(LayerMask mask)
	{
		foreach (Camera camera in Camera.allCameras)
		{
			if (!this.m_ignoredCameras.Contains(camera) && (camera.cullingMask & mask) != 0)
			{
				return camera;
			}
		}
		return null;
	}

	// Token: 0x06008292 RID: 33426 RVA: 0x002A6554 File Offset: 0x002A4754
	private bool Raycast(Camera requestedCamera, LayerMask mask, out Camera camera, out RaycastHit hitInfo, bool ignorePriority = false)
	{
		hitInfo = default(RaycastHit);
		if (!ignorePriority)
		{
			foreach (Camera camera2 in this.m_cameraMaskCameras)
			{
				camera = camera2;
				LayerMask mask2 = GameLayer.CameraMask.LayerBit();
				if (this.RaycastWithPriority(camera2, mask2, out hitInfo))
				{
					return true;
				}
			}
			if (this.m_mainEffectsCamera == null)
			{
				this.m_mainEffectsCamera = CameraUtils.FindFullScreenEffectsCamera(false);
			}
			if (!(this.m_mainEffectsCamera != null))
			{
				goto IL_B3;
			}
			LayerMask mask3 = GameLayer.IgnoreFullScreenEffects.LayerBit();
			if (this.RaycastWithPriority(this.m_mainEffectsCamera, mask3, out hitInfo))
			{
				camera = this.m_mainEffectsCamera;
				return true;
			}
		}
		IL_B3:
		camera = requestedCamera;
		if (camera != null)
		{
			return this.RaycastWithPriority(camera, mask, out hitInfo);
		}
		camera = Camera.main;
		return this.RaycastWithPriority(camera, mask, out hitInfo);
	}

	// Token: 0x06008293 RID: 33427 RVA: 0x002A6654 File Offset: 0x002A4854
	private bool Raycast(Camera camera, LayerMask mask, out RaycastHit hitInfo)
	{
		if (camera != null)
		{
			return this.RaycastAgainstBlockingLayers(camera, mask, out hitInfo);
		}
		return this.RaycastAgainstBlockingLayers(Camera.main, mask, out hitInfo);
	}

	// Token: 0x06008294 RID: 33428 RVA: 0x002A6678 File Offset: 0x002A4878
	private bool RaycastWithPriority(Camera camera, LayerMask mask, out RaycastHit hitInfo)
	{
		hitInfo = default(RaycastHit);
		if (camera == null)
		{
			return false;
		}
		if (!this.FilteredRaycast(camera, this.GetMousePosition(), mask, out hitInfo))
		{
			return false;
		}
		GameLayer layer = (GameLayer)hitInfo.collider.gameObject.layer;
		return !this.HigherPriorityCollisionExists(layer);
	}

	// Token: 0x06008295 RID: 33429 RVA: 0x002A66C8 File Offset: 0x002A48C8
	private bool RaycastAgainstBlockingLayers(Camera camera, LayerMask mask, out RaycastHit hitInfo)
	{
		hitInfo = default(RaycastHit);
		if (camera == null)
		{
			return false;
		}
		if (!this.FilteredRaycast(camera, this.GetMousePosition(), mask, out hitInfo))
		{
			return false;
		}
		GameLayer layer = (GameLayer)hitInfo.collider.gameObject.layer;
		return (!this.m_systemDialogActive || this.m_hitTestPriorityMap[layer] >= this.m_hitTestPriorityMap[GameLayer.UI]) && (!this.m_gameDialogActive || this.m_hitTestPriorityMap[layer] >= this.m_hitTestPriorityMap[GameLayer.IgnoreFullScreenEffects]) && (!(this.m_currentFullScreenEffect != null) || !this.m_currentFullScreenEffect.HasActiveEffects || camera.depth >= this.m_currentFullScreenEffect.Camera.depth);
	}

	// Token: 0x06008296 RID: 33430 RVA: 0x002A678A File Offset: 0x002A498A
	private bool FilteredRaycast(Camera camera, Vector3 screenPoint, LayerMask mask, out RaycastHit hitInfo)
	{
		if (this.CanHitTestOffCamera(mask))
		{
			if (!Physics.Raycast(camera.ScreenPointToRay(screenPoint), out hitInfo, camera.farClipPlane, mask))
			{
				return false;
			}
		}
		else if (!CameraUtils.Raycast(camera, screenPoint, mask, out hitInfo))
		{
			return false;
		}
		return true;
	}

	// Token: 0x06008297 RID: 33431 RVA: 0x002A67C4 File Offset: 0x002A49C4
	private bool HigherPriorityCollisionExists(GameLayer layer)
	{
		if (this.m_systemDialogActive && this.m_hitTestPriorityMap[layer] < this.m_hitTestPriorityMap[GameLayer.UI])
		{
			return true;
		}
		if (this.m_gameDialogActive && this.m_hitTestPriorityMap[layer] < this.m_hitTestPriorityMap[GameLayer.IgnoreFullScreenEffects])
		{
			return true;
		}
		LayerMask higherPriorityLayerMask = this.GetHigherPriorityLayerMask(layer);
		foreach (Camera camera in Camera.allCameras)
		{
			RaycastHit raycastHit;
			if (!this.m_ignoredCameras.Contains(camera) && (camera.cullingMask & higherPriorityLayerMask) != 0 && this.FilteredRaycast(camera, this.GetMousePosition(), higherPriorityLayerMask, out raycastHit))
			{
				GameLayer layer2 = (GameLayer)raycastHit.collider.gameObject.layer;
				if ((camera.cullingMask & layer2.LayerBit()) != 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06008298 RID: 33432 RVA: 0x002A6890 File Offset: 0x002A4A90
	private LayerMask GetHigherPriorityLayerMask(GameLayer layer)
	{
		int num = this.m_hitTestPriorityMap[layer];
		LayerMask layerMask = 0;
		foreach (KeyValuePair<GameLayer, int> keyValuePair in this.m_hitTestPriorityMap)
		{
			GameLayer key = keyValuePair.Key;
			if (keyValuePair.Value > num)
			{
				layerMask |= key.LayerBit();
			}
		}
		return layerMask;
	}

	// Token: 0x06008299 RID: 33433 RVA: 0x002A6918 File Offset: 0x002A4B18
	private void UpdateMouseOnOrOffScreen()
	{
		bool flag = InputUtil.IsMouseOnScreen();
		if (flag == this.m_mouseOnScreen)
		{
			return;
		}
		this.m_mouseOnScreen = flag;
		UniversalInputManager.MouseOnOrOffScreenCallback[] array = this.m_mouseOnOrOffScreenListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](flag);
		}
	}

	// Token: 0x0600829A RID: 33434 RVA: 0x002A6960 File Offset: 0x002A4B60
	private void UpdateInput()
	{
		if (this.UpdateTextInput())
		{
			return;
		}
		InputManager inputManager = InputManager.Get();
		if (inputManager != null && inputManager.HandleKeyboardInput())
		{
			return;
		}
		if (this.HearthstoneCheckoutBlocksInput())
		{
			return;
		}
		if (this.m_shouldHandleCheats)
		{
			CheatMgr cheatMgr = CheatMgr.Get();
			if (cheatMgr != null && cheatMgr.HandleKeyboardInput())
			{
				return;
			}
			Cheats cheats = Cheats.Get();
			if (cheats != null && cheats.HandleKeyboardInput())
			{
				return;
			}
		}
		DialogManager dialogManager = DialogManager.Get();
		if (dialogManager != null && dialogManager.HandleKeyboardInput())
		{
			return;
		}
		CollectionInputMgr collectionInputMgr = CollectionInputMgr.Get();
		if (collectionInputMgr != null && collectionInputMgr.HandleKeyboardInput())
		{
			return;
		}
		DraftInputManager draftInputManager = DraftInputManager.Get();
		if (draftInputManager != null && draftInputManager.HandleKeyboardInput())
		{
			return;
		}
		PackOpening packOpening = PackOpening.Get();
		if (packOpening != null && packOpening.HandleKeyboardInput())
		{
			return;
		}
		if (this.m_sceneMgr != null || HearthstoneServices.TryGet<SceneMgr>(out this.m_sceneMgr))
		{
			PegasusScene scene = this.m_sceneMgr.GetScene();
			if (scene != null && scene.HandleKeyboardInput())
			{
				return;
			}
		}
		BaseUI baseUI = BaseUI.Get();
		if (baseUI != null)
		{
			baseUI.HandleKeyboardInput();
			return;
		}
	}

	// Token: 0x0600829B RID: 33435 RVA: 0x002A6A7C File Offset: 0x002A4C7C
	private bool UpdateTextInput()
	{
		if (Input.imeIsSelected || !string.IsNullOrEmpty(Input.compositionString))
		{
			UniversalInputManager.IsIMEEverUsed = true;
		}
		if (this.m_inputNeedsFocusFromTabKeyDown)
		{
			this.m_inputNeedsFocusFromTabKeyDown = false;
			this.m_inputNeedsFocus = true;
		}
		return this.m_inputActive && this.m_inputFocused;
	}

	// Token: 0x0600829C RID: 33436 RVA: 0x002A6AC8 File Offset: 0x002A4CC8
	private void UserCancelTextInput()
	{
		this.CancelTextInput(true, null);
	}

	// Token: 0x0600829D RID: 33437 RVA: 0x002A6AD2 File Offset: 0x002A4CD2
	private void ObjectCancelTextInput(GameObject requester)
	{
		this.CancelTextInput(false, requester);
	}

	// Token: 0x0600829E RID: 33438 RVA: 0x002A6ADC File Offset: 0x002A4CDC
	private void CancelTextInput(bool userRequested, GameObject requester)
	{
		if (this.IsTextInputPassword())
		{
			Input.imeCompositionMode = IMECompositionMode.Auto;
		}
		if (requester != null && requester == this.m_inputOwner)
		{
			this.ClearTextInputVars();
		}
		else
		{
			UniversalInputManager.TextInputCanceledCallback inputCanceledCallback = this.m_inputCanceledCallback;
			this.ClearTextInputVars();
			if (inputCanceledCallback != null)
			{
				inputCanceledCallback(userRequested, requester);
			}
		}
		if (this.UseWindowsTouch())
		{
			HearthstoneServices.Get<ITouchScreenService>().HideKeyboard();
		}
	}

	// Token: 0x0600829F RID: 33439 RVA: 0x002A6B40 File Offset: 0x002A4D40
	private void ResetKeyboard()
	{
		if (this.UseWindowsTouch() && this.m_hideVirtualKeyboardOnComplete)
		{
			HearthstoneServices.Get<ITouchScreenService>().HideKeyboard();
		}
	}

	// Token: 0x060082A0 RID: 33440 RVA: 0x002A6B5C File Offset: 0x002A4D5C
	private void CompleteTextInput()
	{
		if (this.IsTextInputPassword())
		{
			Input.imeCompositionMode = IMECompositionMode.Auto;
		}
		UniversalInputManager.TextInputCompletedCallback inputCompletedCallback = this.m_inputCompletedCallback;
		if (!this.m_inputKeepFocusOnComplete)
		{
			this.ClearTextInputVars();
		}
		try
		{
			if (inputCompletedCallback != null)
			{
				inputCompletedCallback(this.m_inputText);
			}
			this.m_inputText = string.Empty;
		}
		catch (Exception ex)
		{
			this.ResetKeyboard();
			throw ex;
		}
		this.ResetKeyboard();
	}

	// Token: 0x060082A1 RID: 33441 RVA: 0x002A6BC8 File Offset: 0x002A4DC8
	private void ClearTextInputVars()
	{
		this.m_inputActive = false;
		this.m_inputFocused = false;
		this.m_inputOwner = null;
		this.m_inputMaxCharacters = 0;
		this.m_inputUpdatedCallback = null;
		this.m_inputCompletedCallback = null;
		this.m_inputCanceledCallback = null;
		this.m_inputUnfocusedCallback = null;
		bool isEditor = Application.isEditor;
	}

	// Token: 0x060082A2 RID: 33442 RVA: 0x002A6C08 File Offset: 0x002A4E08
	private bool IgnoreGUIInput()
	{
		if (this.m_inputIgnoreState == UniversalInputManager.TextInputIgnoreState.INVALID)
		{
			return false;
		}
		if (Event.current.type != EventType.KeyUp)
		{
			return false;
		}
		KeyCode keyCode = Event.current.keyCode;
		if (keyCode == KeyCode.Return)
		{
			if (this.m_inputIgnoreState == UniversalInputManager.TextInputIgnoreState.COMPLETE_KEY_UP)
			{
				this.m_inputIgnoreState = UniversalInputManager.TextInputIgnoreState.NEXT_CALL;
			}
			return true;
		}
		if (keyCode != KeyCode.Escape)
		{
			return false;
		}
		if (this.m_inputIgnoreState == UniversalInputManager.TextInputIgnoreState.CANCEL_KEY_UP)
		{
			this.m_inputIgnoreState = UniversalInputManager.TextInputIgnoreState.NEXT_CALL;
		}
		return true;
	}

	// Token: 0x060082A3 RID: 33443 RVA: 0x002A6C6C File Offset: 0x002A4E6C
	private void HandleGUIInputInactive()
	{
		if (this.m_inputActive)
		{
			return;
		}
		if (this.m_inputIgnoreState != UniversalInputManager.TextInputIgnoreState.INVALID)
		{
			if (this.m_inputIgnoreState == UniversalInputManager.TextInputIgnoreState.NEXT_CALL)
			{
				this.m_inputIgnoreState = UniversalInputManager.TextInputIgnoreState.INVALID;
			}
			return;
		}
		if (this.HearthstoneCheckoutBlocksInput())
		{
			return;
		}
		if (ChatMgr.Get() != null)
		{
			ChatMgr.Get().HandleGUIInput();
		}
	}

	// Token: 0x060082A4 RID: 33444 RVA: 0x002A6CBC File Offset: 0x002A4EBC
	private void HandleGUIInputActive()
	{
		if (!this.m_inputActive)
		{
			return;
		}
		if (!this.PreprocessGUITextInput())
		{
			return;
		}
		Vector2 screenSize = new Vector2((float)Screen.width, (float)Screen.height);
		Rect inputScreenRect = this.ComputeTextInputRect(screenSize);
		this.SetupTextInput(screenSize, inputScreenRect);
		string text = this.ShowTextInput(inputScreenRect);
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		if (this.UseWindowsTouch() && !touchScreenService.IsVirtualKeyboardVisible() && this.GetMouseButtonDown(0) && inputScreenRect.Contains(touchScreenService.GetTouchPositionForGUI()))
		{
			touchScreenService.ShowKeyboard();
		}
		this.UpdateTextInputFocus();
		if (!this.m_inputFocused)
		{
			return;
		}
		if (this.m_inputText != text)
		{
			if (this.m_inputNumber)
			{
				text = StringUtils.StripNonNumbers(text);
			}
			if (!this.m_inputMultiLine)
			{
				text = StringUtils.StripNewlines(text);
			}
			this.m_inputText = text;
			if (this.m_inputUpdatedCallback != null)
			{
				this.m_inputUpdatedCallback(text);
			}
		}
	}

	// Token: 0x060082A5 RID: 33445 RVA: 0x002A6D90 File Offset: 0x002A4F90
	private bool PreprocessGUITextInput()
	{
		this.UpdateTabKeyDown();
		if (this.m_inputPreprocessCallback != null)
		{
			this.m_inputPreprocessCallback(Event.current);
			if (!this.m_inputActive)
			{
				return false;
			}
		}
		return !this.ProcessTextInputFinishKeys();
	}

	// Token: 0x060082A6 RID: 33446 RVA: 0x002A6DC6 File Offset: 0x002A4FC6
	private void UpdateTabKeyDown()
	{
		this.m_tabKeyDown = (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Tab);
	}

	// Token: 0x060082A7 RID: 33447 RVA: 0x002A6DEC File Offset: 0x002A4FEC
	private bool ProcessTextInputFinishKeys()
	{
		if (!this.m_inputFocused)
		{
			return false;
		}
		if (Event.current.type != EventType.KeyDown)
		{
			return false;
		}
		KeyCode keyCode = Event.current.keyCode;
		if (keyCode != KeyCode.Return)
		{
			if (keyCode == KeyCode.Escape)
			{
				this.m_inputIgnoreState = UniversalInputManager.TextInputIgnoreState.CANCEL_KEY_UP;
				this.UserCancelTextInput();
				return true;
			}
			if (keyCode != KeyCode.KeypadEnter)
			{
				return false;
			}
		}
		this.m_inputIgnoreState = UniversalInputManager.TextInputIgnoreState.COMPLETE_KEY_UP;
		this.CompleteTextInput();
		return true;
	}

	// Token: 0x060082A8 RID: 33448 RVA: 0x002A6E50 File Offset: 0x002A5050
	private void SetupTextInput(Vector2 screenSize, Rect inputScreenRect)
	{
		GUI.skin = this.m_skin;
		GUI.skin.textField.font = this.m_inputFont;
		int fontSize = this.ComputeTextInputFontSize(screenSize, inputScreenRect.height);
		GUI.skin.textField.fontSize = fontSize;
		if (this.m_inputColor != null)
		{
			GUI.color = this.m_inputColor.Value;
		}
		GUI.skin.textField.alignment = this.m_inputAlignment;
		GUI.SetNextControlName("UniversalInputManagerTextInput");
	}

	// Token: 0x060082A9 RID: 33449 RVA: 0x002A6ED8 File Offset: 0x002A50D8
	private string ShowTextInput(Rect inputScreenRect)
	{
		string result;
		if (this.m_inputPassword)
		{
			if (this.m_inputMaxCharacters <= 0)
			{
				result = GUI.PasswordField(inputScreenRect, this.m_inputText, '*');
			}
			else
			{
				result = GUI.PasswordField(inputScreenRect, this.m_inputText, '*', this.m_inputMaxCharacters);
			}
		}
		else if (this.m_inputMaxCharacters <= 0)
		{
			result = GUI.TextField(inputScreenRect, this.m_inputText);
		}
		else
		{
			result = GUI.TextField(inputScreenRect, this.m_inputText, this.m_inputMaxCharacters);
		}
		return result;
	}

	// Token: 0x060082AA RID: 33450 RVA: 0x002A6F4C File Offset: 0x002A514C
	private void UpdateTextInputFocus()
	{
		if (this.m_inputNeedsFocus)
		{
			GUI.FocusControl("UniversalInputManagerTextInput");
			this.m_inputFocused = true;
			this.m_inputNeedsFocus = false;
			return;
		}
		bool inputFocused = this.m_inputFocused;
		this.m_inputFocused = (GUI.GetNameOfFocusedControl() == "UniversalInputManagerTextInput");
		UniversalInputManager.TextInputUnfocusedCallback inputUnfocusedCallback = this.m_inputUnfocusedCallback;
		if (!this.m_inputFocused && inputFocused && inputUnfocusedCallback != null)
		{
			inputUnfocusedCallback();
		}
	}

	// Token: 0x060082AB RID: 33451 RVA: 0x002A6FB4 File Offset: 0x002A51B4
	private Rect ComputeTextInputRect(Vector2 screenSize)
	{
		float num = screenSize.x / screenSize.y;
		float num2 = this.m_inputInitialScreenSize.x / this.m_inputInitialScreenSize.y / num;
		float num3 = screenSize.y / this.m_inputInitialScreenSize.y;
		float num4 = (0.5f - this.m_inputNormalizedRect.x) * this.m_inputInitialScreenSize.x * num3;
		return new Rect(screenSize.x * 0.5f - num4, this.m_inputNormalizedRect.y * screenSize.y - 1.5f, this.m_inputNormalizedRect.width * screenSize.x * num2, this.m_inputNormalizedRect.height * screenSize.y + 1.5f);
	}

	// Token: 0x060082AC RID: 33452 RVA: 0x002A7074 File Offset: 0x002A5274
	private int ComputeTextInputFontSize(Vector2 screenSize, float rectHeight)
	{
		int num = Mathf.CeilToInt(rectHeight);
		if (Localization.IsIMELocale() || UniversalInputManager.IsIMEEverUsed)
		{
			num -= 9;
		}
		else
		{
			num -= 4;
		}
		return Mathf.Clamp(num, 2, 96);
	}

	// Token: 0x060082AD RID: 33453 RVA: 0x002A70AC File Offset: 0x002A52AC
	private bool HearthstoneCheckoutBlocksInput()
	{
		return (this.m_hearthstoneCheckout != null || HearthstoneServices.TryGet<HearthstoneCheckout>(out this.m_hearthstoneCheckout)) && this.m_hearthstoneCheckout.ShouldBlockInput();
	}

	// Token: 0x060082AE RID: 33454 RVA: 0x002A70D0 File Offset: 0x002A52D0
	private void OnTouchModeChangedCallback(Option option, object prevvalue, bool existed, object userdata)
	{
		this.UpdateIsTouchMode();
	}

	// Token: 0x04006D27 RID: 27943
	private static UniversalInputManager s_instance;

	// Token: 0x04006D28 RID: 27944
	private static readonly PlatformDependentValue<bool> IsTouchDevice = new PlatformDependentValue<bool>(PlatformCategory.Input)
	{
		Mouse = false,
		Touch = true
	};

	// Token: 0x04006D29 RID: 27945
	private const float TEXT_INPUT_RECT_HEIGHT_OFFSET = 3f;

	// Token: 0x04006D2A RID: 27946
	private const int TEXT_INPUT_MAX_FONT_SIZE = 96;

	// Token: 0x04006D2B RID: 27947
	private const int TEXT_INPUT_MIN_FONT_SIZE = 2;

	// Token: 0x04006D2C RID: 27948
	private const int TEXT_INPUT_FONT_SIZE_INSET = 4;

	// Token: 0x04006D2D RID: 27949
	private const int TEXT_INPUT_IME_FONT_SIZE_INSET = 9;

	// Token: 0x04006D2E RID: 27950
	private const string TEXT_INPUT_NAME = "UniversalInputManagerTextInput";

	// Token: 0x04006D2F RID: 27951
	private static readonly GameLayer[] HIT_TEST_PRIORITY_ORDER = new GameLayer[]
	{
		GameLayer.IgnoreFullScreenEffects,
		GameLayer.Reserved29,
		GameLayer.BackgroundUI,
		GameLayer.PerspectiveUI,
		GameLayer.CameraMask,
		GameLayer.UI,
		GameLayer.BattleNet,
		GameLayer.BattleNetFriendList,
		GameLayer.BattleNetChat,
		GameLayer.BattleNetDialog,
		GameLayer.HighPriorityUI
	};

	// Token: 0x04006D30 RID: 27952
	private static readonly GameLayer[] IGNORE_HIT_TEST_LAYERS = new GameLayer[]
	{
		GameLayer.TransparentFX,
		GameLayer.IgnoreRaycast,
		GameLayer.Water,
		GameLayer.Tooltip,
		GameLayer.NoLight,
		GameLayer.Effects,
		GameLayer.FXCollide,
		GameLayer.ScreenEffects,
		GameLayer.InvisibleRender,
		GameLayer.CameraFade
	};

	// Token: 0x04006D31 RID: 27953
	private static bool IsIMEEverUsed = false;

	// Token: 0x04006D32 RID: 27954
	private bool m_mouseOnScreen;

	// Token: 0x04006D33 RID: 27955
	private List<UniversalInputManager.MouseOnOrOffScreenCallback> m_mouseOnOrOffScreenListeners = new List<UniversalInputManager.MouseOnOrOffScreenCallback>();

	// Token: 0x04006D34 RID: 27956
	private Map<GameLayer, int> m_hitTestPriorityMap;

	// Token: 0x04006D35 RID: 27957
	private bool m_gameDialogActive;

	// Token: 0x04006D36 RID: 27958
	private bool m_systemDialogActive;

	// Token: 0x04006D37 RID: 27959
	private int m_offCameraHitTestMask;

	// Token: 0x04006D38 RID: 27960
	private Camera m_mainEffectsCamera;

	// Token: 0x04006D39 RID: 27961
	private FullScreenEffects m_currentFullScreenEffect;

	// Token: 0x04006D3A RID: 27962
	private List<Camera> m_cameraMaskCameras = new List<Camera>();

	// Token: 0x04006D3B RID: 27963
	private List<Camera> m_sortedByDepthCameras = new List<Camera>();

	// Token: 0x04006D3C RID: 27964
	private int m_hitTestLayerBits;

	// Token: 0x04006D3D RID: 27965
	private List<Camera> m_ignoredCameras = new List<Camera>();

	// Token: 0x04006D3E RID: 27966
	private GameObject m_inputOwner;

	// Token: 0x04006D3F RID: 27967
	private UniversalInputManager.TextInputUpdatedCallback m_inputUpdatedCallback;

	// Token: 0x04006D40 RID: 27968
	private UniversalInputManager.TextInputPreprocessCallback m_inputPreprocessCallback;

	// Token: 0x04006D41 RID: 27969
	private UniversalInputManager.TextInputCompletedCallback m_inputCompletedCallback;

	// Token: 0x04006D42 RID: 27970
	private UniversalInputManager.TextInputCanceledCallback m_inputCanceledCallback;

	// Token: 0x04006D43 RID: 27971
	private UniversalInputManager.TextInputUnfocusedCallback m_inputUnfocusedCallback;

	// Token: 0x04006D44 RID: 27972
	private bool m_inputPassword;

	// Token: 0x04006D45 RID: 27973
	private bool m_inputNumber;

	// Token: 0x04006D46 RID: 27974
	private bool m_inputMultiLine;

	// Token: 0x04006D47 RID: 27975
	private bool m_inputActive;

	// Token: 0x04006D48 RID: 27976
	private bool m_inputFocused;

	// Token: 0x04006D49 RID: 27977
	private bool m_inputKeepFocusOnComplete;

	// Token: 0x04006D4A RID: 27978
	private string m_inputText;

	// Token: 0x04006D4B RID: 27979
	private Rect m_inputNormalizedRect;

	// Token: 0x04006D4C RID: 27980
	private Vector2 m_inputInitialScreenSize;

	// Token: 0x04006D4D RID: 27981
	private int m_inputMaxCharacters;

	// Token: 0x04006D4E RID: 27982
	private Font m_inputFont;

	// Token: 0x04006D4F RID: 27983
	private TextAnchor m_inputAlignment;

	// Token: 0x04006D50 RID: 27984
	private Color? m_inputColor;

	// Token: 0x04006D51 RID: 27985
	private Font m_defaultInputFont;

	// Token: 0x04006D52 RID: 27986
	private TextAnchor m_defaultInputAlignment;

	// Token: 0x04006D53 RID: 27987
	private bool m_inputNeedsFocus;

	// Token: 0x04006D54 RID: 27988
	private bool m_tabKeyDown;

	// Token: 0x04006D55 RID: 27989
	private bool m_inputNeedsFocusFromTabKeyDown;

	// Token: 0x04006D56 RID: 27990
	private UniversalInputManager.TextInputIgnoreState m_inputIgnoreState;

	// Token: 0x04006D57 RID: 27991
	private GameObject m_sceneObject;

	// Token: 0x04006D58 RID: 27992
	private bool m_hideVirtualKeyboardOnComplete = true;

	// Token: 0x04006D59 RID: 27993
	private GUISkinContainer m_skinContainer;

	// Token: 0x04006D5A RID: 27994
	private GUISkin m_skin;

	// Token: 0x04006D5B RID: 27995
	private HearthstoneCheckout m_hearthstoneCheckout;

	// Token: 0x04006D5C RID: 27996
	private bool m_shouldHandleCheats;

	// Token: 0x04006D5D RID: 27997
	private SceneMgr m_sceneMgr;

	// Token: 0x04006D5E RID: 27998
	private bool m_isTouchMode;

	// Token: 0x04006D5F RID: 27999
	private Event m_processingEvent = new Event();

	// Token: 0x04006D60 RID: 28000
	public static readonly PlatformDependentValue<bool> UsePhoneUI = new PlatformDependentValue<bool>(PlatformCategory.Screen)
	{
		Phone = true,
		Tablet = false,
		PC = false
	};

	// Token: 0x020025FB RID: 9723
	// (Invoke) Token: 0x0601353E RID: 79166
	public delegate void MouseOnOrOffScreenCallback(bool onScreen);

	// Token: 0x020025FC RID: 9724
	// (Invoke) Token: 0x06013542 RID: 79170
	public delegate void TextInputUpdatedCallback(string input);

	// Token: 0x020025FD RID: 9725
	// (Invoke) Token: 0x06013546 RID: 79174
	public delegate bool TextInputPreprocessCallback(Event e);

	// Token: 0x020025FE RID: 9726
	// (Invoke) Token: 0x0601354A RID: 79178
	public delegate void TextInputCompletedCallback(string input);

	// Token: 0x020025FF RID: 9727
	// (Invoke) Token: 0x0601354E RID: 79182
	public delegate void TextInputCanceledCallback(bool userRequested, GameObject requester);

	// Token: 0x02002600 RID: 9728
	// (Invoke) Token: 0x06013552 RID: 79186
	public delegate void TextInputUnfocusedCallback();

	// Token: 0x02002601 RID: 9729
	public class TextInputParams
	{
		// Token: 0x0400EF3F RID: 61247
		public GameObject m_owner;

		// Token: 0x0400EF40 RID: 61248
		public bool m_password;

		// Token: 0x0400EF41 RID: 61249
		public bool m_number;

		// Token: 0x0400EF42 RID: 61250
		public bool m_multiLine;

		// Token: 0x0400EF43 RID: 61251
		public Rect m_rect;

		// Token: 0x0400EF44 RID: 61252
		public UniversalInputManager.TextInputUpdatedCallback m_updatedCallback;

		// Token: 0x0400EF45 RID: 61253
		public UniversalInputManager.TextInputPreprocessCallback m_preprocessCallback;

		// Token: 0x0400EF46 RID: 61254
		public UniversalInputManager.TextInputCompletedCallback m_completedCallback;

		// Token: 0x0400EF47 RID: 61255
		public UniversalInputManager.TextInputCanceledCallback m_canceledCallback;

		// Token: 0x0400EF48 RID: 61256
		public UniversalInputManager.TextInputUnfocusedCallback m_unfocusedCallback;

		// Token: 0x0400EF49 RID: 61257
		public int m_maxCharacters;

		// Token: 0x0400EF4A RID: 61258
		public Font m_font;

		// Token: 0x0400EF4B RID: 61259
		public TextAnchor? m_alignment;

		// Token: 0x0400EF4C RID: 61260
		public string m_text;

		// Token: 0x0400EF4D RID: 61261
		public bool m_touchScreenKeyboardHideInput;

		// Token: 0x0400EF4E RID: 61262
		public int m_touchScreenKeyboardType;

		// Token: 0x0400EF4F RID: 61263
		public bool m_inputKeepFocusOnComplete;

		// Token: 0x0400EF50 RID: 61264
		public Color? m_color;

		// Token: 0x0400EF51 RID: 61265
		public bool m_showVirtualKeyboard = true;

		// Token: 0x0400EF52 RID: 61266
		public bool m_hideVirtualKeyboardOnComplete = true;

		// Token: 0x0400EF53 RID: 61267
		public bool m_useNativeKeyboard;
	}

	// Token: 0x02002602 RID: 9730
	private enum TextInputIgnoreState
	{
		// Token: 0x0400EF55 RID: 61269
		INVALID,
		// Token: 0x0400EF56 RID: 61270
		COMPLETE_KEY_UP,
		// Token: 0x0400EF57 RID: 61271
		CANCEL_KEY_UP,
		// Token: 0x0400EF58 RID: 61272
		NEXT_CALL
	}
}
