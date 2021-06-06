using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

public class UniversalInputManager : IService, IHasUpdate
{
	public delegate void MouseOnOrOffScreenCallback(bool onScreen);

	public delegate void TextInputUpdatedCallback(string input);

	public delegate bool TextInputPreprocessCallback(Event e);

	public delegate void TextInputCompletedCallback(string input);

	public delegate void TextInputCanceledCallback(bool userRequested, GameObject requester);

	public delegate void TextInputUnfocusedCallback();

	public class TextInputParams
	{
		public GameObject m_owner;

		public bool m_password;

		public bool m_number;

		public bool m_multiLine;

		public Rect m_rect;

		public TextInputUpdatedCallback m_updatedCallback;

		public TextInputPreprocessCallback m_preprocessCallback;

		public TextInputCompletedCallback m_completedCallback;

		public TextInputCanceledCallback m_canceledCallback;

		public TextInputUnfocusedCallback m_unfocusedCallback;

		public int m_maxCharacters;

		public Font m_font;

		public TextAnchor? m_alignment;

		public string m_text;

		public bool m_touchScreenKeyboardHideInput;

		public int m_touchScreenKeyboardType;

		public bool m_inputKeepFocusOnComplete;

		public Color? m_color;

		public bool m_showVirtualKeyboard = true;

		public bool m_hideVirtualKeyboardOnComplete = true;

		public bool m_useNativeKeyboard;
	}

	private enum TextInputIgnoreState
	{
		INVALID,
		COMPLETE_KEY_UP,
		CANCEL_KEY_UP,
		NEXT_CALL
	}

	private static UniversalInputManager s_instance;

	private static readonly PlatformDependentValue<bool> IsTouchDevice = new PlatformDependentValue<bool>(PlatformCategory.Input)
	{
		Mouse = false,
		Touch = true
	};

	private const float TEXT_INPUT_RECT_HEIGHT_OFFSET = 3f;

	private const int TEXT_INPUT_MAX_FONT_SIZE = 96;

	private const int TEXT_INPUT_MIN_FONT_SIZE = 2;

	private const int TEXT_INPUT_FONT_SIZE_INSET = 4;

	private const int TEXT_INPUT_IME_FONT_SIZE_INSET = 9;

	private const string TEXT_INPUT_NAME = "UniversalInputManagerTextInput";

	private static readonly GameLayer[] HIT_TEST_PRIORITY_ORDER = new GameLayer[11]
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

	private static readonly GameLayer[] IGNORE_HIT_TEST_LAYERS = new GameLayer[10]
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

	private static bool IsIMEEverUsed = false;

	private bool m_mouseOnScreen;

	private List<MouseOnOrOffScreenCallback> m_mouseOnOrOffScreenListeners = new List<MouseOnOrOffScreenCallback>();

	private Map<GameLayer, int> m_hitTestPriorityMap;

	private bool m_gameDialogActive;

	private bool m_systemDialogActive;

	private int m_offCameraHitTestMask;

	private Camera m_mainEffectsCamera;

	private FullScreenEffects m_currentFullScreenEffect;

	private List<Camera> m_cameraMaskCameras = new List<Camera>();

	private List<Camera> m_sortedByDepthCameras = new List<Camera>();

	private int m_hitTestLayerBits;

	private List<Camera> m_ignoredCameras = new List<Camera>();

	private GameObject m_inputOwner;

	private TextInputUpdatedCallback m_inputUpdatedCallback;

	private TextInputPreprocessCallback m_inputPreprocessCallback;

	private TextInputCompletedCallback m_inputCompletedCallback;

	private TextInputCanceledCallback m_inputCanceledCallback;

	private TextInputUnfocusedCallback m_inputUnfocusedCallback;

	private bool m_inputPassword;

	private bool m_inputNumber;

	private bool m_inputMultiLine;

	private bool m_inputActive;

	private bool m_inputFocused;

	private bool m_inputKeepFocusOnComplete;

	private string m_inputText;

	private Rect m_inputNormalizedRect;

	private Vector2 m_inputInitialScreenSize;

	private int m_inputMaxCharacters;

	private Font m_inputFont;

	private TextAnchor m_inputAlignment;

	private Color? m_inputColor;

	private Font m_defaultInputFont;

	private TextAnchor m_defaultInputAlignment;

	private bool m_inputNeedsFocus;

	private bool m_tabKeyDown;

	private bool m_inputNeedsFocusFromTabKeyDown;

	private TextInputIgnoreState m_inputIgnoreState;

	private GameObject m_sceneObject;

	private bool m_hideVirtualKeyboardOnComplete = true;

	private GUISkinContainer m_skinContainer;

	private GUISkin m_skin;

	private HearthstoneCheckout m_hearthstoneCheckout;

	private bool m_shouldHandleCheats;

	private SceneMgr m_sceneMgr;

	private bool m_isTouchMode;

	private Event m_processingEvent = new Event();

	public static readonly PlatformDependentValue<bool> UsePhoneUI = new PlatformDependentValue<bool>(PlatformCategory.Screen)
	{
		Phone = true,
		Tablet = false,
		PC = false
	};

	private GameObject SceneObject
	{
		get
		{
			if (m_sceneObject == null)
			{
				m_sceneObject = new GameObject("UniversalInputManagerSceneObject", typeof(HSDontDestroyOnLoad));
			}
			return m_sceneObject;
		}
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		Processor.QueueJob("UniversalInputManager.SetupGUISkin", SetupGUISkin(), HearthstoneServices.CreateServiceDependency(typeof(IAssetLoader)));
		CreateHitTestPriorityMap();
		m_mouseOnScreen = InputUtil.IsMouseOnScreen();
		Processor.RegisterOnGUIDelegate(OnGUI);
		m_shouldHandleCheats = !HearthstoneApplication.IsPublic();
		UpdateIsTouchMode();
		Options.Get().RegisterChangedListener(Option.TOUCH_MODE, OnTouchModeChangedCallback);
		yield break;
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
		s_instance = null;
		Options.Get().UnregisterChangedListener(Option.TOUCH_MODE, OnTouchModeChangedCallback);
	}

	public void Update()
	{
		UpdateCamerasByDepth();
		UpdateMouseOnOrOffScreen();
		UpdateInput();
		CleanDeadCameras();
	}

	private void OnGUI()
	{
		IgnoreGUIInput();
		HandleGUIInputInactive();
		HandleGUIInputActive();
	}

	public static UniversalInputManager Get()
	{
		if (s_instance == null)
		{
			s_instance = HearthstoneServices.Get<UniversalInputManager>();
		}
		return s_instance;
	}

	public void SetGUISkin(GUISkinContainer skinContainer)
	{
		if (m_skinContainer != null)
		{
			UnityEngine.Object.Destroy(m_skinContainer.gameObject);
		}
		m_skinContainer = skinContainer;
		m_skinContainer.transform.parent = SceneObject.transform;
		m_skin = skinContainer.GetGUISkin();
		m_defaultInputAlignment = m_skin.textField.alignment;
		m_defaultInputFont = m_skin.textField.font;
	}

	public bool IsTouchMode()
	{
		return m_isTouchMode;
	}

	private void UpdateIsTouchMode()
	{
		m_isTouchMode = (bool)IsTouchDevice || Options.Get().GetBool(Option.TOUCH_MODE);
	}

	public bool UseWindowsTouch()
	{
		if (IsTouchMode())
		{
			return !PlatformSettings.IsEmulating;
		}
		return false;
	}

	public bool WasTouchCanceled()
	{
		if (!IsTouchDevice)
		{
			return false;
		}
		Touch[] touches = Input.touches;
		foreach (Touch touch in touches)
		{
			if (touch.phase == TouchPhase.Canceled)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsMouseOnScreen()
	{
		return m_mouseOnScreen;
	}

	public bool RegisterMouseOnOrOffScreenListener(MouseOnOrOffScreenCallback listener)
	{
		if (m_mouseOnOrOffScreenListeners.Contains(listener))
		{
			return false;
		}
		m_mouseOnOrOffScreenListeners.Add(listener);
		return true;
	}

	public bool UnregisterMouseOnOrOffScreenListener(MouseOnOrOffScreenCallback listener)
	{
		return m_mouseOnOrOffScreenListeners.Remove(listener);
	}

	public void SetGameDialogActive(bool active)
	{
		m_gameDialogActive = active;
	}

	public void SetSystemDialogActive(bool active)
	{
		m_systemDialogActive = active;
	}

	public void UseTextInput(TextInputParams parms, bool force = false)
	{
		if (force || !(parms.m_owner == m_inputOwner))
		{
			if (m_inputOwner != null && m_inputOwner != parms.m_owner)
			{
				ObjectCancelTextInput(parms.m_owner);
			}
			m_inputOwner = parms.m_owner;
			m_inputUpdatedCallback = parms.m_updatedCallback;
			m_inputPreprocessCallback = parms.m_preprocessCallback;
			m_inputCompletedCallback = parms.m_completedCallback;
			m_inputCanceledCallback = parms.m_canceledCallback;
			m_inputUnfocusedCallback = parms.m_unfocusedCallback;
			m_inputPassword = parms.m_password;
			m_inputNumber = parms.m_number;
			m_inputMultiLine = parms.m_multiLine;
			m_inputActive = true;
			m_inputFocused = false;
			m_inputText = parms.m_text ?? string.Empty;
			m_inputNormalizedRect = parms.m_rect;
			m_inputInitialScreenSize.x = Screen.width;
			m_inputInitialScreenSize.y = Screen.height;
			m_inputMaxCharacters = parms.m_maxCharacters;
			m_inputColor = parms.m_color;
			m_inputAlignment = parms.m_alignment ?? m_defaultInputAlignment;
			m_inputFont = parms.m_font ?? m_defaultInputFont;
			m_inputNeedsFocus = true;
			m_inputIgnoreState = TextInputIgnoreState.INVALID;
			m_inputKeepFocusOnComplete = parms.m_inputKeepFocusOnComplete;
			if (IsTextInputPassword())
			{
				Input.imeCompositionMode = IMECompositionMode.Off;
			}
			m_hideVirtualKeyboardOnComplete = parms.m_hideVirtualKeyboardOnComplete;
			if (UseWindowsTouch() && parms.m_showVirtualKeyboard)
			{
				HearthstoneServices.Get<ITouchScreenService>().ShowKeyboard();
			}
		}
	}

	public void CancelTextInput(GameObject requester, bool force = false)
	{
		if (IsTextInputActive() && (force || !(requester != m_inputOwner)))
		{
			ObjectCancelTextInput(requester);
		}
	}

	public void FocusTextInput(GameObject owner)
	{
		if (!(owner != m_inputOwner))
		{
			if (!m_tabKeyDown)
			{
				m_inputNeedsFocus = true;
			}
			else
			{
				m_inputNeedsFocusFromTabKeyDown = true;
			}
		}
	}

	public void UpdateTextInputRect(GameObject owner, Rect rect)
	{
		if (!(owner != m_inputOwner))
		{
			m_inputNormalizedRect = rect;
			m_inputInitialScreenSize.x = Screen.width;
			m_inputInitialScreenSize.y = Screen.height;
		}
	}

	public bool IsTextInputPassword()
	{
		return m_inputPassword;
	}

	public bool IsTextInputActive()
	{
		return m_inputActive;
	}

	public string GetInputText()
	{
		return m_inputText;
	}

	public void SetInputText(string text, bool moveCursorToEnd = false)
	{
		m_inputText = text ?? string.Empty;
		if (moveCursorToEnd)
		{
			Processor.ScheduleCallback(0f, realTime: false, delegate
			{
				((TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl))?.MoveTextEnd();
			});
		}
	}

	public bool InputIsOver(GameObject gameObj)
	{
		RaycastHit hitInfo;
		return InputIsOver(gameObj, out hitInfo);
	}

	public bool InputIsOver(GameObject gameObj, out RaycastHit hitInfo)
	{
		LayerMask mask = ((GameLayer)gameObj.layer).LayerBit();
		if (!Raycast(null, mask, out var _, out hitInfo))
		{
			return false;
		}
		return hitInfo.collider.gameObject == gameObj;
	}

	public bool InputIsOver(GameObject gameObj, int layerMask, out RaycastHit hitInfo)
	{
		if (!Raycast(null, layerMask, out var _, out hitInfo))
		{
			return false;
		}
		return hitInfo.collider.gameObject == gameObj;
	}

	public bool InputIsOver(Camera camera, GameObject gameObj)
	{
		RaycastHit hitInfo;
		return InputIsOver(camera, gameObj, out hitInfo);
	}

	public bool InputIsOver(Camera camera, GameObject gameObj, out RaycastHit hitInfo)
	{
		LayerMask mask = ((GameLayer)gameObj.layer).LayerBit();
		if (!Raycast(camera, mask, out var _, out hitInfo))
		{
			return false;
		}
		return hitInfo.collider.gameObject == gameObj;
	}

	public bool InputIsOverByCameraDepth(GameObject gameObj, out RaycastHit hitInfo)
	{
		if (!GetInputHitInfoByCameraDepth(out hitInfo))
		{
			return false;
		}
		return hitInfo.collider.gameObject == gameObj;
	}

	public bool ForcedInputIsOver(Camera camera, GameObject gameObj)
	{
		RaycastHit hitInfo;
		return ForcedInputIsOver(camera, gameObj, out hitInfo);
	}

	public bool ForcedInputIsOver(Camera camera, GameObject gameObj, out RaycastHit hitInfo)
	{
		LayerMask layerMask = ((GameLayer)gameObj.layer).LayerBit();
		if (!CameraUtils.Raycast(camera, GetMousePosition(), layerMask, out hitInfo))
		{
			return false;
		}
		return hitInfo.collider.gameObject == gameObj;
	}

	public bool ForcedUnblockableInputIsOver(Camera camera, GameObject gameObj, out RaycastHit hitInfo)
	{
		LayerMask layerMask = ((GameLayer)gameObj.layer).LayerBit();
		hitInfo = default(RaycastHit);
		RaycastHit[] hitInfos = null;
		if (!CameraUtils.RaycastAll(camera, GetMousePosition(), layerMask, out hitInfos))
		{
			return false;
		}
		RaycastHit[] array = hitInfos;
		for (int i = 0; i < array.Length; i++)
		{
			RaycastHit raycastHit = array[i];
			if (raycastHit.collider.gameObject == gameObj)
			{
				hitInfo = raycastHit;
				return true;
			}
		}
		return false;
	}

	public bool InputHitAnyObject(GameLayer layer)
	{
		RaycastHit hitInfo;
		return GetInputHitInfo(layer, out hitInfo);
	}

	public bool InputHitAnyObject(LayerMask layerMask)
	{
		RaycastHit hitInfo;
		return GetInputHitInfo(layerMask, out hitInfo);
	}

	public bool InputHitAnyObject(Camera requestedCamera)
	{
		RaycastHit hitInfo;
		if (requestedCamera == null)
		{
			return GetInputHitInfo(out hitInfo);
		}
		return GetInputHitInfo(requestedCamera, requestedCamera.cullingMask, out hitInfo);
	}

	public bool InputHitAnyObject(Camera requestedCamera, GameLayer layer)
	{
		RaycastHit hitInfo;
		return GetInputHitInfo(requestedCamera, layer, out hitInfo);
	}

	public bool InputHitAnyObject(Camera requestedCamera, LayerMask mask)
	{
		RaycastHit hitInfo;
		return GetInputHitInfo(requestedCamera, mask, out hitInfo);
	}

	public bool GetInputHitInfo(out RaycastHit hitInfo)
	{
		return GetInputHitInfo(GameLayer.Default, out hitInfo);
	}

	public bool GetInputHitInfo(GameLayer layer, out RaycastHit hitInfo)
	{
		return GetInputHitInfo(layer.LayerBit(), out hitInfo);
	}

	public bool GetInputHitInfo(LayerMask mask, out RaycastHit hitInfo)
	{
		Camera requestedCamera = GuessBestHitTestCamera(mask);
		return GetInputHitInfo(requestedCamera, mask, out hitInfo);
	}

	public bool GetInputHitInfo(Camera requestedCamera, out RaycastHit hitInfo)
	{
		if (requestedCamera == null)
		{
			return GetInputHitInfo(out hitInfo);
		}
		return GetInputHitInfo(requestedCamera, requestedCamera.cullingMask, out hitInfo);
	}

	public bool GetInputHitInfo(Camera requestedCamera, GameLayer layer, out RaycastHit hitInfo)
	{
		Camera camera;
		return Raycast(requestedCamera, layer.LayerBit(), out camera, out hitInfo);
	}

	public bool GetInputHitInfo(Camera requestedCamera, LayerMask mask, out RaycastHit hitInfo)
	{
		Camera camera;
		return Raycast(requestedCamera, mask, out camera, out hitInfo);
	}

	public bool GetInputHitInfoByCameraDepth(out RaycastHit hitInfo)
	{
		foreach (Camera sortedByDepthCamera in m_sortedByDepthCameras)
		{
			if (!(sortedByDepthCamera == null))
			{
				LayerMask layerMask = sortedByDepthCamera.cullingMask & m_hitTestLayerBits;
				if ((int)layerMask != 0 && sortedByDepthCamera != null && Raycast(sortedByDepthCamera, layerMask, out hitInfo))
				{
					return true;
				}
			}
		}
		hitInfo = default(RaycastHit);
		return false;
	}

	public bool GetInputHitInfoByCameraDepth(LayerMask layer, out RaycastHit hitInfo)
	{
		foreach (Camera sortedByDepthCamera in m_sortedByDepthCameras)
		{
			LayerMask layerMask = sortedByDepthCamera.cullingMask & (int)layer;
			if ((int)layerMask != 0 && sortedByDepthCamera != null && Raycast(sortedByDepthCamera, layerMask, out hitInfo))
			{
				return true;
			}
		}
		hitInfo = default(RaycastHit);
		return false;
	}

	public bool GetInputPointOnPlane(Vector3 origin, out Vector3 point)
	{
		return GetInputPointOnPlane(GameLayer.Default, origin, out point);
	}

	public bool GetInputPointOnPlane(GameLayer layer, Vector3 origin, out Vector3 point)
	{
		point = Vector3.zero;
		LayerMask mask = layer.LayerBit();
		if (!Raycast(null, mask, out var camera, out var _))
		{
			return false;
		}
		Ray ray = camera.ScreenPointToRay(GetMousePosition());
		Vector3 inNormal = -camera.transform.forward;
		if (!new Plane(inNormal, origin).Raycast(ray, out var enter))
		{
			return false;
		}
		point = ray.GetPoint(enter);
		return true;
	}

	public bool CanHitTestOffCamera(GameLayer layer)
	{
		return CanHitTestOffCamera(layer.LayerBit());
	}

	public bool CanHitTestOffCamera(LayerMask layerMask)
	{
		return (m_offCameraHitTestMask & (int)layerMask) != 0;
	}

	public void EnableHitTestOffCamera(GameLayer layer, bool enable)
	{
		EnableHitTestOffCamera(layer.LayerBit(), enable);
	}

	public void EnableHitTestOffCamera(LayerMask mask, bool enable)
	{
		if (enable)
		{
			m_offCameraHitTestMask |= mask;
		}
		else
		{
			m_offCameraHitTestMask &= ~(int)mask;
		}
	}

	public void SetCurrentFullScreenEffect(FullScreenEffects effect)
	{
		m_currentFullScreenEffect = effect;
	}

	public bool GetMouseButton(int button)
	{
		if (UseWindowsTouch())
		{
			return HearthstoneServices.Get<ITouchScreenService>().GetTouch(button);
		}
		return InputCollection.GetMouseButton(button);
	}

	public bool GetMouseButtonDown(int button)
	{
		if (UseWindowsTouch())
		{
			return HearthstoneServices.Get<ITouchScreenService>().GetTouchDown(button);
		}
		return InputCollection.GetMouseButtonDown(button);
	}

	public bool GetMouseButtonUp(int button)
	{
		if (UseWindowsTouch())
		{
			return HearthstoneServices.Get<ITouchScreenService>().GetTouchUp(button);
		}
		return InputCollection.GetMouseButtonUp(button);
	}

	public Vector3 GetMousePosition()
	{
		if (UseWindowsTouch())
		{
			return HearthstoneServices.Get<ITouchScreenService>().GetTouchPosition();
		}
		return InputCollection.GetMousePosition();
	}

	public bool AddCameraMaskCamera(Camera camera)
	{
		if (m_cameraMaskCameras.Contains(camera))
		{
			return false;
		}
		m_cameraMaskCameras.Add(camera);
		return true;
	}

	public bool RemoveCameraMaskCamera(Camera camera)
	{
		return m_cameraMaskCameras.Remove(camera);
	}

	public bool AddIgnoredCamera(Camera camera)
	{
		if (m_ignoredCameras.Contains(camera))
		{
			return false;
		}
		m_ignoredCameras.Add(camera);
		return true;
	}

	public bool RemoveIgnoredCamera(Camera camera)
	{
		return m_ignoredCameras.Remove(camera);
	}

	private void CreateHitTestPriorityMap()
	{
		m_hitTestPriorityMap = new Map<GameLayer, int>();
		int num = 1;
		for (int i = 0; i < HIT_TEST_PRIORITY_ORDER.Length; i++)
		{
			GameLayer key = HIT_TEST_PRIORITY_ORDER[i];
			m_hitTestPriorityMap.Add(key, num++);
		}
		foreach (GameLayer value in Enum.GetValues(typeof(GameLayer)))
		{
			m_hitTestLayerBits |= value.LayerBit();
			if (!m_hitTestPriorityMap.ContainsKey(value))
			{
				m_hitTestPriorityMap.Add(value, 0);
			}
		}
		int num2 = 0;
		GameLayer[] iGNORE_HIT_TEST_LAYERS = IGNORE_HIT_TEST_LAYERS;
		foreach (GameLayer gameLayer2 in iGNORE_HIT_TEST_LAYERS)
		{
			num2 |= gameLayer2.LayerBit();
		}
		m_hitTestLayerBits &= ~num2;
	}

	private void UpdateCamerasByDepth()
	{
		m_sortedByDepthCameras.Clear();
		m_sortedByDepthCameras.AddRange(Camera.allCameras.OrderByDescending((Camera cam) => cam.depth));
	}

	private void CleanDeadCameras()
	{
		GeneralUtils.CleanDeadObjectsFromList(m_cameraMaskCameras);
		GeneralUtils.CleanDeadObjectsFromList(m_ignoredCameras);
	}

	private IEnumerator<IAsyncJobResult> SetupGUISkin()
	{
		InstantiatePrefab loadGUISkin = new InstantiatePrefab("TextInputGUISkin.prefab:506d0b8720fda4ac298d5ea645aaedd5");
		yield return loadGUISkin;
		SetGUISkin(loadGUISkin.InstantiatedPrefab.GetComponent<GUISkinContainer>());
		yield return null;
	}

	private Camera GuessBestHitTestCamera(LayerMask mask)
	{
		Camera[] allCameras = Camera.allCameras;
		foreach (Camera camera in allCameras)
		{
			if (!m_ignoredCameras.Contains(camera) && (camera.cullingMask & (int)mask) != 0)
			{
				return camera;
			}
		}
		return null;
	}

	private bool Raycast(Camera requestedCamera, LayerMask mask, out Camera camera, out RaycastHit hitInfo, bool ignorePriority = false)
	{
		hitInfo = default(RaycastHit);
		if (!ignorePriority)
		{
			foreach (Camera cameraMaskCamera in m_cameraMaskCameras)
			{
				Camera camera2 = (camera = cameraMaskCamera);
				LayerMask mask2 = GameLayer.CameraMask.LayerBit();
				if (RaycastWithPriority(camera2, mask2, out hitInfo))
				{
					return true;
				}
			}
			if (m_mainEffectsCamera == null)
			{
				m_mainEffectsCamera = CameraUtils.FindFullScreenEffectsCamera(activeOnly: false);
			}
			if (m_mainEffectsCamera != null)
			{
				LayerMask mask3 = GameLayer.IgnoreFullScreenEffects.LayerBit();
				if (RaycastWithPriority(m_mainEffectsCamera, mask3, out hitInfo))
				{
					camera = m_mainEffectsCamera;
					return true;
				}
			}
		}
		camera = requestedCamera;
		if (camera != null)
		{
			return RaycastWithPriority(camera, mask, out hitInfo);
		}
		camera = Camera.main;
		return RaycastWithPriority(camera, mask, out hitInfo);
	}

	private bool Raycast(Camera camera, LayerMask mask, out RaycastHit hitInfo)
	{
		if (camera != null)
		{
			return RaycastAgainstBlockingLayers(camera, mask, out hitInfo);
		}
		return RaycastAgainstBlockingLayers(Camera.main, mask, out hitInfo);
	}

	private bool RaycastWithPriority(Camera camera, LayerMask mask, out RaycastHit hitInfo)
	{
		hitInfo = default(RaycastHit);
		if (camera == null)
		{
			return false;
		}
		if (!FilteredRaycast(camera, GetMousePosition(), mask, out hitInfo))
		{
			return false;
		}
		GameLayer layer = (GameLayer)hitInfo.collider.gameObject.layer;
		if (HigherPriorityCollisionExists(layer))
		{
			return false;
		}
		return true;
	}

	private bool RaycastAgainstBlockingLayers(Camera camera, LayerMask mask, out RaycastHit hitInfo)
	{
		hitInfo = default(RaycastHit);
		if (camera == null)
		{
			return false;
		}
		if (!FilteredRaycast(camera, GetMousePosition(), mask, out hitInfo))
		{
			return false;
		}
		GameLayer layer = (GameLayer)hitInfo.collider.gameObject.layer;
		if (m_systemDialogActive && m_hitTestPriorityMap[layer] < m_hitTestPriorityMap[GameLayer.UI])
		{
			return false;
		}
		if (m_gameDialogActive && m_hitTestPriorityMap[layer] < m_hitTestPriorityMap[GameLayer.IgnoreFullScreenEffects])
		{
			return false;
		}
		if (m_currentFullScreenEffect != null && m_currentFullScreenEffect.HasActiveEffects && camera.depth < m_currentFullScreenEffect.Camera.depth)
		{
			return false;
		}
		return true;
	}

	private bool FilteredRaycast(Camera camera, Vector3 screenPoint, LayerMask mask, out RaycastHit hitInfo)
	{
		if (CanHitTestOffCamera(mask))
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

	private bool HigherPriorityCollisionExists(GameLayer layer)
	{
		if (m_systemDialogActive && m_hitTestPriorityMap[layer] < m_hitTestPriorityMap[GameLayer.UI])
		{
			return true;
		}
		if (m_gameDialogActive && m_hitTestPriorityMap[layer] < m_hitTestPriorityMap[GameLayer.IgnoreFullScreenEffects])
		{
			return true;
		}
		LayerMask higherPriorityLayerMask = GetHigherPriorityLayerMask(layer);
		Camera[] allCameras = Camera.allCameras;
		foreach (Camera camera in allCameras)
		{
			if (!m_ignoredCameras.Contains(camera) && (camera.cullingMask & (int)higherPriorityLayerMask) != 0 && FilteredRaycast(camera, GetMousePosition(), higherPriorityLayerMask, out var hitInfo))
			{
				GameLayer layer2 = (GameLayer)hitInfo.collider.gameObject.layer;
				if ((camera.cullingMask & layer2.LayerBit()) != 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	private LayerMask GetHigherPriorityLayerMask(GameLayer layer)
	{
		int num = m_hitTestPriorityMap[layer];
		LayerMask layerMask = 0;
		foreach (KeyValuePair<GameLayer, int> item in m_hitTestPriorityMap)
		{
			GameLayer key = item.Key;
			if (item.Value > num)
			{
				layerMask = (int)layerMask | key.LayerBit();
			}
		}
		return layerMask;
	}

	private void UpdateMouseOnOrOffScreen()
	{
		bool flag = InputUtil.IsMouseOnScreen();
		if (flag != m_mouseOnScreen)
		{
			m_mouseOnScreen = flag;
			MouseOnOrOffScreenCallback[] array = m_mouseOnOrOffScreenListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i](flag);
			}
		}
	}

	private void UpdateInput()
	{
		if (UpdateTextInput())
		{
			return;
		}
		InputManager inputManager = InputManager.Get();
		if ((inputManager != null && inputManager.HandleKeyboardInput()) || HearthstoneCheckoutBlocksInput())
		{
			return;
		}
		if (m_shouldHandleCheats)
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
		if (m_sceneMgr != null || HearthstoneServices.TryGet<SceneMgr>(out m_sceneMgr))
		{
			PegasusScene scene = m_sceneMgr.GetScene();
			if (scene != null && scene.HandleKeyboardInput())
			{
				return;
			}
		}
		BaseUI baseUI = BaseUI.Get();
		if (baseUI != null)
		{
			baseUI.HandleKeyboardInput();
		}
	}

	private bool UpdateTextInput()
	{
		if (Input.imeIsSelected || !string.IsNullOrEmpty(Input.compositionString))
		{
			IsIMEEverUsed = true;
		}
		if (m_inputNeedsFocusFromTabKeyDown)
		{
			m_inputNeedsFocusFromTabKeyDown = false;
			m_inputNeedsFocus = true;
		}
		if (!m_inputActive)
		{
			return false;
		}
		return m_inputFocused;
	}

	private void UserCancelTextInput()
	{
		CancelTextInput(userRequested: true, null);
	}

	private void ObjectCancelTextInput(GameObject requester)
	{
		CancelTextInput(userRequested: false, requester);
	}

	private void CancelTextInput(bool userRequested, GameObject requester)
	{
		if (IsTextInputPassword())
		{
			Input.imeCompositionMode = IMECompositionMode.Auto;
		}
		if (requester != null && requester == m_inputOwner)
		{
			ClearTextInputVars();
		}
		else
		{
			TextInputCanceledCallback inputCanceledCallback = m_inputCanceledCallback;
			ClearTextInputVars();
			inputCanceledCallback?.Invoke(userRequested, requester);
		}
		if (UseWindowsTouch())
		{
			HearthstoneServices.Get<ITouchScreenService>().HideKeyboard();
		}
	}

	private void ResetKeyboard()
	{
		if (UseWindowsTouch() && m_hideVirtualKeyboardOnComplete)
		{
			HearthstoneServices.Get<ITouchScreenService>().HideKeyboard();
		}
	}

	private void CompleteTextInput()
	{
		if (IsTextInputPassword())
		{
			Input.imeCompositionMode = IMECompositionMode.Auto;
		}
		TextInputCompletedCallback inputCompletedCallback = m_inputCompletedCallback;
		if (!m_inputKeepFocusOnComplete)
		{
			ClearTextInputVars();
		}
		try
		{
			inputCompletedCallback?.Invoke(m_inputText);
			m_inputText = string.Empty;
		}
		catch (Exception ex)
		{
			ResetKeyboard();
			throw ex;
		}
		ResetKeyboard();
	}

	private void ClearTextInputVars()
	{
		m_inputActive = false;
		m_inputFocused = false;
		m_inputOwner = null;
		m_inputMaxCharacters = 0;
		m_inputUpdatedCallback = null;
		m_inputCompletedCallback = null;
		m_inputCanceledCallback = null;
		m_inputUnfocusedCallback = null;
		_ = Application.isEditor;
	}

	private bool IgnoreGUIInput()
	{
		if (m_inputIgnoreState == TextInputIgnoreState.INVALID)
		{
			return false;
		}
		if (Event.current.type != EventType.KeyUp)
		{
			return false;
		}
		switch (Event.current.keyCode)
		{
		case KeyCode.Return:
			if (m_inputIgnoreState == TextInputIgnoreState.COMPLETE_KEY_UP)
			{
				m_inputIgnoreState = TextInputIgnoreState.NEXT_CALL;
			}
			return true;
		case KeyCode.Escape:
			if (m_inputIgnoreState == TextInputIgnoreState.CANCEL_KEY_UP)
			{
				m_inputIgnoreState = TextInputIgnoreState.NEXT_CALL;
			}
			return true;
		default:
			return false;
		}
	}

	private void HandleGUIInputInactive()
	{
		if (m_inputActive)
		{
			return;
		}
		if (m_inputIgnoreState != 0)
		{
			if (m_inputIgnoreState == TextInputIgnoreState.NEXT_CALL)
			{
				m_inputIgnoreState = TextInputIgnoreState.INVALID;
			}
		}
		else if (!HearthstoneCheckoutBlocksInput() && ChatMgr.Get() != null)
		{
			ChatMgr.Get().HandleGUIInput();
		}
	}

	private void HandleGUIInputActive()
	{
		if (!m_inputActive || !PreprocessGUITextInput())
		{
			return;
		}
		Vector2 screenSize = new Vector2(Screen.width, Screen.height);
		Rect inputScreenRect = ComputeTextInputRect(screenSize);
		SetupTextInput(screenSize, inputScreenRect);
		string text = ShowTextInput(inputScreenRect);
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		if (UseWindowsTouch() && !touchScreenService.IsVirtualKeyboardVisible() && GetMouseButtonDown(0) && inputScreenRect.Contains(touchScreenService.GetTouchPositionForGUI()))
		{
			touchScreenService.ShowKeyboard();
		}
		UpdateTextInputFocus();
		if (m_inputFocused && m_inputText != text)
		{
			if (m_inputNumber)
			{
				text = StringUtils.StripNonNumbers(text);
			}
			if (!m_inputMultiLine)
			{
				text = StringUtils.StripNewlines(text);
			}
			m_inputText = text;
			if (m_inputUpdatedCallback != null)
			{
				m_inputUpdatedCallback(text);
			}
		}
	}

	private bool PreprocessGUITextInput()
	{
		UpdateTabKeyDown();
		if (m_inputPreprocessCallback != null)
		{
			m_inputPreprocessCallback(Event.current);
			if (!m_inputActive)
			{
				return false;
			}
		}
		if (ProcessTextInputFinishKeys())
		{
			return false;
		}
		return true;
	}

	private void UpdateTabKeyDown()
	{
		m_tabKeyDown = Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Tab;
	}

	private bool ProcessTextInputFinishKeys()
	{
		if (!m_inputFocused)
		{
			return false;
		}
		if (Event.current.type != EventType.KeyDown)
		{
			return false;
		}
		switch (Event.current.keyCode)
		{
		case KeyCode.Return:
		case KeyCode.KeypadEnter:
			m_inputIgnoreState = TextInputIgnoreState.COMPLETE_KEY_UP;
			CompleteTextInput();
			return true;
		case KeyCode.Escape:
			m_inputIgnoreState = TextInputIgnoreState.CANCEL_KEY_UP;
			UserCancelTextInput();
			return true;
		default:
			return false;
		}
	}

	private void SetupTextInput(Vector2 screenSize, Rect inputScreenRect)
	{
		GUI.skin = m_skin;
		GUI.skin.textField.font = m_inputFont;
		int fontSize = ComputeTextInputFontSize(screenSize, inputScreenRect.height);
		GUI.skin.textField.fontSize = fontSize;
		if (m_inputColor.HasValue)
		{
			GUI.color = m_inputColor.Value;
		}
		GUI.skin.textField.alignment = m_inputAlignment;
		GUI.SetNextControlName("UniversalInputManagerTextInput");
	}

	private string ShowTextInput(Rect inputScreenRect)
	{
		if (m_inputPassword)
		{
			if (m_inputMaxCharacters <= 0)
			{
				return GUI.PasswordField(inputScreenRect, m_inputText, '*');
			}
			return GUI.PasswordField(inputScreenRect, m_inputText, '*', m_inputMaxCharacters);
		}
		if (m_inputMaxCharacters <= 0)
		{
			return GUI.TextField(inputScreenRect, m_inputText);
		}
		return GUI.TextField(inputScreenRect, m_inputText, m_inputMaxCharacters);
	}

	private void UpdateTextInputFocus()
	{
		if (m_inputNeedsFocus)
		{
			GUI.FocusControl("UniversalInputManagerTextInput");
			m_inputFocused = true;
			m_inputNeedsFocus = false;
			return;
		}
		bool inputFocused = m_inputFocused;
		m_inputFocused = GUI.GetNameOfFocusedControl() == "UniversalInputManagerTextInput";
		TextInputUnfocusedCallback inputUnfocusedCallback = m_inputUnfocusedCallback;
		if (!m_inputFocused && inputFocused)
		{
			inputUnfocusedCallback?.Invoke();
		}
	}

	private Rect ComputeTextInputRect(Vector2 screenSize)
	{
		float num = screenSize.x / screenSize.y;
		float num2 = m_inputInitialScreenSize.x / m_inputInitialScreenSize.y / num;
		float num3 = screenSize.y / m_inputInitialScreenSize.y;
		float num4 = (0.5f - m_inputNormalizedRect.x) * m_inputInitialScreenSize.x * num3;
		return new Rect(screenSize.x * 0.5f - num4, m_inputNormalizedRect.y * screenSize.y - 1.5f, m_inputNormalizedRect.width * screenSize.x * num2, m_inputNormalizedRect.height * screenSize.y + 1.5f);
	}

	private int ComputeTextInputFontSize(Vector2 screenSize, float rectHeight)
	{
		int num = Mathf.CeilToInt(rectHeight);
		num = ((!Localization.IsIMELocale() && !IsIMEEverUsed) ? (num - 4) : (num - 9));
		return Mathf.Clamp(num, 2, 96);
	}

	private bool HearthstoneCheckoutBlocksInput()
	{
		if (m_hearthstoneCheckout == null && !HearthstoneServices.TryGet<HearthstoneCheckout>(out m_hearthstoneCheckout))
		{
			return false;
		}
		return m_hearthstoneCheckout.ShouldBlockInput();
	}

	private void OnTouchModeChangedCallback(Option option, object prevvalue, bool existed, object userdata)
	{
		UpdateIsTouchMode();
	}
}
