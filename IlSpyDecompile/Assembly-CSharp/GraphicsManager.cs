using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

public class GraphicsManager : IService, IHasUpdate
{
	public struct GPULimits
	{
		public int highPrecisionBits;

		public int mediumPrecisionBits;

		public int lowPrecisionBits;

		public int maxFragmentTextureUnits;

		public int maxVertexTextureUnits;

		public int maxCombinedTextureUnits;

		public int maxTextureSize;

		public int maxCubeMapSize;

		public int maxRenderBufferSize;

		public int maxFragmentUniforms;

		public int maxVertexUniforms;

		public int maxVaryings;

		public int maxVertexAttribs;
	}

	private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

	private struct RECT
	{
		public int Left;

		public int Top;

		public int Right;

		public int Bottom;
	}

	private const int ANDROID_MIN_DPI_HIGH_RES_TEXTURES = 180;

	private const int DRAGGING_TARGET_FRAMERATE = 60;

	private GraphicsQuality m_GraphicsQuality;

	private bool m_RealtimeShadows;

	private List<GameObject> m_DisableLowQualityObjects;

	private int m_targetFramerate = 30;

	private int m_winPosX;

	private int m_winPosY;

	private bool m_initialPositionSet;

	private ResizeManager m_resizeManager;

	public GraphicsQuality RenderQualityLevel
	{
		get
		{
			return m_GraphicsQuality;
		}
		set
		{
			m_GraphicsQuality = value;
			Options.Get().SetInt(Option.GFX_QUALITY, (int)m_GraphicsQuality);
			UpdateQualitySettings();
		}
	}

	public bool RealtimeShadows => m_RealtimeShadows;

	public event Action<int, int> OnResolutionChangedEvent;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield return new JobDefinition("GraphicsManager.LoadFXAA", Job_LoadFXAA());
		m_DisableLowQualityObjects = new List<GameObject>();
		if (!Options.Get().HasOption(Option.GFX_QUALITY))
		{
			string intelDeviceName = serviceLocator.Get<ITouchScreenService>().GetIntelDeviceName();
			Log.Graphics.Print("Intel Device Name = {0}", intelDeviceName);
			if (intelDeviceName != null && intelDeviceName.Contains("Haswell") && intelDeviceName.Contains("U28W"))
			{
				if (Screen.currentResolution.height > 1080)
				{
					Options.Get().SetInt(Option.GFX_QUALITY, 0);
				}
			}
			else if (intelDeviceName != null && intelDeviceName.Contains("Crystal-Well"))
			{
				Options.Get().SetInt(Option.GFX_QUALITY, 2);
			}
			else if (intelDeviceName != null && intelDeviceName.Contains("BayTrail"))
			{
				Options.Get().SetInt(Option.GFX_QUALITY, 0);
			}
		}
		m_GraphicsQuality = (GraphicsQuality)Options.Get().GetInt(Option.GFX_QUALITY);
		m_resizeManager = new ResizeManager(OnResolutionChanged, CheckPosition);
		InitializeScreen();
		UpdateQualitySettings();
		if (Options.Get().HasOption(Option.GFX_TARGET_FRAME_RATE))
		{
			m_targetFramerate = Options.Get().GetInt(Option.GFX_TARGET_FRAME_RATE);
		}
		Application.targetFrameRate = m_targetFramerate;
		LogSystemInfo();
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(ITouchScreenService) };
	}

	public void Shutdown()
	{
		if (!Screen.fullScreen)
		{
			Options.Get().SetInt(Option.GFX_WIDTH, Screen.width);
			Options.Get().SetInt(Option.GFX_HEIGHT, Screen.height);
			int[] windowPosition = GetWindowPosition();
			Options.Get().SetInt(Option.GFX_WIN_POSX, windowPosition[0]);
			Options.Get().SetInt(Option.GFX_WIN_POSY, windowPosition[1]);
		}
		this.OnResolutionChangedEvent = null;
	}

	public void Update()
	{
		m_resizeManager.Update();
	}

	private void CheckPosition(int posX, int posY)
	{
		Processor.RunCoroutine(SetPos(posX, posY));
	}

	public static GraphicsManager Get()
	{
		return HearthstoneServices.Get<GraphicsManager>();
	}

	public void SetDraggingFramerate(bool isDragging)
	{
		if (Application.targetFrameRate <= 0)
		{
			return;
		}
		if (isDragging)
		{
			if (Application.targetFrameRate < 60)
			{
				Application.targetFrameRate = 60;
			}
		}
		else
		{
			Application.targetFrameRate = m_targetFramerate;
		}
	}

	public void RegisterLowQualityDisableObject(GameObject lowQualityObject)
	{
		if (!m_DisableLowQualityObjects.Contains(lowQualityObject))
		{
			m_DisableLowQualityObjects.Add(lowQualityObject);
		}
	}

	public void DeregisterLowQualityDisableObject(GameObject lowQualityObject)
	{
		if (m_DisableLowQualityObjects.Contains(lowQualityObject))
		{
			m_DisableLowQualityObjects.Remove(lowQualityObject);
		}
	}

	public bool isVeryLowQualityDevice()
	{
		return false;
	}

	private static void _GetLimits(ref GPULimits limits)
	{
		limits.highPrecisionBits = 16;
		limits.mediumPrecisionBits = 16;
		limits.lowPrecisionBits = 23;
		limits.maxFragmentTextureUnits = 16;
		limits.maxVertexTextureUnits = 16;
		limits.maxCombinedTextureUnits = 32;
		limits.maxTextureSize = 8192;
		limits.maxCubeMapSize = 8192;
		limits.maxRenderBufferSize = 8192;
		limits.maxFragmentUniforms = 256;
		limits.maxVertexUniforms = 256;
		limits.maxVaryings = 32;
		limits.maxVertexAttribs = 32;
	}

	public GPULimits GetGPULimits()
	{
		GPULimits limits = default(GPULimits);
		_GetLimits(ref limits);
		return limits;
	}

	private void InitializeScreen()
	{
		if (!Options.Get().GetBool(Option.GFX_FULLSCREEN) && Options.Get().HasOption(Option.GFX_WIN_POSX) && Options.Get().HasOption(Option.GFX_WIN_POSY))
		{
			int num = Options.Get().GetInt(Option.GFX_WIN_POSX);
			int num2 = Options.Get().GetInt(Option.GFX_WIN_POSY);
			if (num < 0)
			{
				num = 0;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			Processor.RunCoroutine(SetPos(num, num2, 0.6f));
		}
	}

	private void UpdateQualitySettings()
	{
		Log.Graphics.Print("GraphicsManager Update, Graphics Quality: " + m_GraphicsQuality);
		UpdateRenderQualitySettings();
		UpdateAntiAliasing();
	}

	[DllImport("user32.dll")]
	private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

	[DllImport("user32.dll")]
	private static extern IntPtr FindWindow(string className, string windowName);

	[DllImport("user32.dll")]
	private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

	[DllImport("user32.dll", SetLastError = true)]
	private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

	private static bool SetWindowPosition(int x, int y, int resX = 0, int resY = 0)
	{
		if (Vars.IsOptionsFileOverridden())
		{
			SetWindowPos(GetCurrentProcessWindow(), 0, x, y, resX, resY, (resX * resY == 0) ? 1 : 0);
			return true;
		}
		IntPtr activeWindow = GetActiveWindow();
		IntPtr intPtr = FindWindow(null, "Hearthstone");
		if (activeWindow == intPtr)
		{
			SetWindowPos(activeWindow, 0, x, y, resX, resY, (resX * resY == 0) ? 1 : 0);
			return true;
		}
		return false;
	}

	private static IntPtr GetCurrentProcessWindow()
	{
		IntPtr foundWindow = IntPtr.Zero;
		EnumWindows(delegate(IntPtr window, IntPtr param)
		{
			uint lpdwProcessId = 0u;
			GetWindowThreadProcessId(window, out lpdwProcessId);
			if (Process.GetCurrentProcess().Id == lpdwProcessId)
			{
				foundWindow = window;
				return false;
			}
			return true;
		}, IntPtr.Zero);
		return foundWindow;
	}

	[DllImport("user32.dll")]
	private static extern IntPtr GetForegroundWindow();

	private static IntPtr GetActiveWindow()
	{
		return GetForegroundWindow();
	}

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

	public static int[] GetWindowPosition()
	{
		int[] array = new int[2];
		RECT lpRect = default(RECT);
		GetWindowRect(GetCurrentProcessWindow(), out lpRect);
		array[0] = lpRect.Left;
		array[1] = lpRect.Top;
		return array;
	}

	public void SetScreenResolution(int width, int height, bool fullscreen)
	{
		m_resizeManager.SetScreenResolution(width, height, fullscreen);
	}

	private void OnResolutionChanged(int width, int height)
	{
		int[] windowPosition = GetWindowPosition();
		int num = windowPosition[0];
		int num2 = windowPosition[1];
		if (num + width > Screen.currentResolution.width)
		{
			num = Screen.currentResolution.width - width;
		}
		if (num2 + height > Screen.currentResolution.height)
		{
			num2 = Screen.currentResolution.height - height;
		}
		if (num < 0 || num > Screen.currentResolution.width)
		{
			num = 0;
		}
		if (num2 + height > Screen.currentResolution.height)
		{
			num2 = 0;
		}
		if (num2 < 0 || num2 > Screen.currentResolution.height)
		{
			num2 = 0;
		}
		if (this.OnResolutionChangedEvent != null && !PlatformSettings.IsMobileRuntimeOS)
		{
			this.OnResolutionChangedEvent(width, height);
		}
		if (m_initialPositionSet)
		{
			Processor.RunCoroutine(SetPos(num, num2));
		}
	}

	private IEnumerator SetPos(int x, int y, float delay = 0f)
	{
		if (HearthstoneApplication.IsInternal() && !Vars.IsOptionsFileOverridden())
		{
			m_initialPositionSet = true;
			yield break;
		}
		yield return new WaitForSeconds(delay);
		m_winPosX = x;
		m_winPosY = y;
		int[] currentPos = GetWindowPosition();
		int[] newPos = new int[2] { m_winPosX, m_winPosY };
		float startTime = Time.time;
		while (currentPos != newPos && Time.time < startTime + 1f)
		{
			newPos[0] = m_winPosX;
			newPos[1] = m_winPosY;
			if (!SetWindowPosition(m_winPosX, m_winPosY))
			{
				break;
			}
			currentPos = GetWindowPosition();
			yield return null;
		}
		m_initialPositionSet = true;
	}

	private void UpdateAntiAliasing()
	{
		bool flag = false;
		int num = 0;
		if (m_GraphicsQuality == GraphicsQuality.Low)
		{
			num = 0;
			flag = false;
		}
		if (m_GraphicsQuality == GraphicsQuality.Medium)
		{
			num = 2;
			flag = false;
			if (HearthstoneServices.TryGet<ITouchScreenService>(out var service))
			{
				string intelDeviceName = service.GetIntelDeviceName();
				if (intelDeviceName != null && (intelDeviceName.Equals("BayTrail") || intelDeviceName.Equals("Poulsbo") || intelDeviceName.Equals("CloverTrail") || (intelDeviceName.Contains("Haswell") && intelDeviceName.Contains("Y6W"))))
				{
					num = 0;
				}
			}
		}
		if (m_GraphicsQuality == GraphicsQuality.High)
		{
			switch (Localization.GetLocale())
			{
			case Locale.koKR:
			case Locale.ruRU:
			case Locale.zhTW:
			case Locale.zhCN:
			case Locale.plPL:
			case Locale.jaJP:
			case Locale.thTH:
				num = 2;
				flag = false;
				break;
			default:
				num = 0;
				flag = true;
				break;
			}
		}
		if (Options.Get().HasOption(Option.GFX_MSAA))
		{
			num = Options.Get().GetInt(Option.GFX_MSAA);
		}
		if (Options.Get().HasOption(Option.GFX_FXAA))
		{
			flag = Options.Get().GetBool(Option.GFX_FXAA);
		}
		if (flag)
		{
			num = 2;
		}
		if (num > 0)
		{
			flag = false;
		}
		if (num == 0)
		{
			num = 2;
		}
		QualitySettings.antiAliasing = num;
		FullScreenAntialiasing[] array = UnityEngine.Object.FindObjectsOfType(typeof(FullScreenAntialiasing)) as FullScreenAntialiasing[];
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = flag;
		}
	}

	private void UpdateRenderQualitySettings()
	{
		int vSyncCount = 0;
		int num = 101;
		if (m_GraphicsQuality == GraphicsQuality.Low)
		{
			m_targetFramerate = 30;
			vSyncCount = 0;
			m_RealtimeShadows = false;
			SetQualityByName("Low");
			num = 101;
		}
		if (m_GraphicsQuality == GraphicsQuality.Medium)
		{
			m_targetFramerate = 30;
			vSyncCount = 0;
			m_RealtimeShadows = false;
			SetQualityByName("Medium");
			num = 201;
		}
		if (m_GraphicsQuality == GraphicsQuality.High)
		{
			m_targetFramerate = 60;
			vSyncCount = 1;
			m_RealtimeShadows = true;
			SetQualityByName("High");
			num = 301;
		}
		Shader.DisableKeyword("LOW_QUALITY");
		if (Options.Get().HasOption(Option.GFX_TARGET_FRAME_RATE))
		{
			m_targetFramerate = Options.Get().GetInt(Option.GFX_TARGET_FRAME_RATE);
		}
		else
		{
			if (HearthstoneServices.TryGet<ITouchScreenService>(out var service) && service.GetBatteryMode() == PowerSource.BatteryPower && m_targetFramerate > 30)
			{
				Log.Graphics.Print("Battery Mode Detected - Clamping Target Frame Rate from {0} to 30", m_targetFramerate);
				m_targetFramerate = 30;
				vSyncCount = 0;
			}
			Application.targetFrameRate = m_targetFramerate;
		}
		if (Options.Get().HasOption(Option.GFX_VSYNC))
		{
			QualitySettings.vSyncCount = Options.Get().GetInt(Option.GFX_VSYNC);
		}
		else
		{
			QualitySettings.vSyncCount = vSyncCount;
		}
		Log.Graphics.Print($"Target frame rate: {Application.targetFrameRate}");
		ProjectedShadow[] array = UnityEngine.Object.FindObjectsOfType(typeof(ProjectedShadow)) as ProjectedShadow[];
		foreach (ProjectedShadow projectedShadow in array)
		{
			projectedShadow.enabled = !m_RealtimeShadows || projectedShadow.m_enabledAlongsideRealtimeShadows;
		}
		RenderToTexture[] array2 = UnityEngine.Object.FindObjectsOfType(typeof(RenderToTexture)) as RenderToTexture[];
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].ForceTextureRebuild();
		}
		Shader[] array3 = UnityEngine.Object.FindObjectsOfType(typeof(Shader)) as Shader[];
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].maximumLOD = num;
		}
		foreach (GameObject disableLowQualityObject in m_DisableLowQualityObjects)
		{
			if (!(disableLowQualityObject == null))
			{
				if (m_GraphicsQuality == GraphicsQuality.Low)
				{
					Log.Graphics.Print($"Low Quality Disable: {disableLowQualityObject.name}");
					disableLowQualityObject.SetActive(value: false);
				}
				else
				{
					Log.Graphics.Print($"Low Quality Enable: {disableLowQualityObject.name}");
					disableLowQualityObject.SetActive(value: true);
				}
			}
		}
		Shader.globalMaximumLOD = num;
		SetScreenEffects();
	}

	private void SetScreenEffects()
	{
		if (ScreenEffectsMgr.Get() != null)
		{
			if (m_GraphicsQuality == GraphicsQuality.Low)
			{
				ScreenEffectsMgr.Get().SetActive(enabled: false);
			}
			else
			{
				ScreenEffectsMgr.Get().SetActive(enabled: true);
			}
		}
	}

	private void SetQualityByName(string qualityName)
	{
		string[] names = QualitySettings.names;
		int index = -1;
		int i;
		for (i = 0; i < names.Length; i++)
		{
			if (names[i] == qualityName)
			{
				index = i;
			}
		}
		if (i < 0)
		{
			UnityEngine.Debug.LogError($"GraphicsManager: Quality Level not found: {qualityName}");
		}
		else
		{
			QualitySettings.SetQualityLevel(index, applyExpensiveChanges: true);
		}
	}

	private void LogSystemInfo()
	{
		UnityEngine.Debug.Log("System Info:");
		UnityEngine.Debug.Log($"SystemInfo - Device Name: {SystemInfo.deviceName}");
		UnityEngine.Debug.Log($"SystemInfo - Device Model: {SystemInfo.deviceModel}");
		UnityEngine.Debug.Log($"SystemInfo - OS: {SystemInfo.operatingSystem}");
		UnityEngine.Debug.Log($"SystemInfo - CPU Type: {SystemInfo.processorType}");
		UnityEngine.Debug.Log($"SystemInfo - CPU Cores: {SystemInfo.processorCount}");
		UnityEngine.Debug.Log($"SystemInfo - System Memory: {SystemInfo.systemMemorySize}");
		UnityEngine.Debug.Log($"SystemInfo - Screen Resolution: {Screen.currentResolution.width}x{Screen.currentResolution.height}");
		UnityEngine.Debug.Log($"SystemInfo - Screen DPI: {Screen.dpi}");
		UnityEngine.Debug.Log($"SystemInfo - GPU ID: {SystemInfo.graphicsDeviceID}");
		UnityEngine.Debug.Log($"SystemInfo - GPU Name: {SystemInfo.graphicsDeviceName}");
		UnityEngine.Debug.Log($"SystemInfo - GPU Vendor: {SystemInfo.graphicsDeviceVendor}");
		UnityEngine.Debug.Log($"SystemInfo - GPU Memory: {SystemInfo.graphicsMemorySize}");
		UnityEngine.Debug.Log($"SystemInfo - GPU Shader Level: {SystemInfo.graphicsShaderLevel}");
		UnityEngine.Debug.Log($"SystemInfo - GPU NPOT Support: {SystemInfo.npotSupport}");
		UnityEngine.Debug.Log($"SystemInfo - Graphics API (version): {SystemInfo.graphicsDeviceVersion}");
		UnityEngine.Debug.Log($"SystemInfo - Graphics API (type): {SystemInfo.graphicsDeviceType}");
		UnityEngine.Debug.Log($"SystemInfo - Graphics Supported Render Target Count: {SystemInfo.supportedRenderTargetCount}");
		UnityEngine.Debug.Log($"SystemInfo - Graphics Supports 3D Textures: {SystemInfo.supports3DTextures}");
		UnityEngine.Debug.Log($"SystemInfo - Graphics Supports Compute Shaders: {SystemInfo.supportsComputeShaders}");
		UnityEngine.Debug.Log($"SystemInfo - Graphics Supports Image Effects: {SystemInfo.supportsImageEffects}");
		UnityEngine.Debug.Log($"SystemInfo - Graphics Supports Render To Cubemap: {SystemInfo.supportsRenderToCubemap}");
		UnityEngine.Debug.Log($"SystemInfo - Graphics Supports Shadows: {SystemInfo.supportsShadows}");
		UnityEngine.Debug.Log($"SystemInfo - Graphics Supports Sparse Textures: {SystemInfo.supportsSparseTextures}");
		UnityEngine.Debug.Log($"SystemInfo - Graphics RenderTextureFormat.ARGBHalf: {SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf)}");
		UnityEngine.Debug.Log(string.Format("SystemInfo - Graphics Metal Support: {0}", SystemInfo.graphicsDeviceVersion.StartsWith("Metal")));
	}

	private void LogGPULimits()
	{
		GPULimits gPULimits = GetGPULimits();
		UnityEngine.Debug.Log("GPU Limits:");
		UnityEngine.Debug.Log($"GPU - Fragment High Precision: {gPULimits.highPrecisionBits}");
		UnityEngine.Debug.Log($"GPU - Fragment Medium Precision: {gPULimits.mediumPrecisionBits}");
		UnityEngine.Debug.Log($"GPU - Fragment Low Precision: {gPULimits.lowPrecisionBits}");
		UnityEngine.Debug.Log($"GPU - Fragment Max Texture Units: {gPULimits.maxFragmentTextureUnits}");
		UnityEngine.Debug.Log($"GPU - Vertex Max Texture Units: {gPULimits.maxVertexTextureUnits}");
		UnityEngine.Debug.Log($"GPU - Combined Max Texture Units: {gPULimits.maxCombinedTextureUnits}");
		UnityEngine.Debug.Log($"GPU - Max Texture Size: {gPULimits.maxTextureSize}");
		UnityEngine.Debug.Log($"GPU - Max Cube-Map Texture Size: {gPULimits.maxCubeMapSize}");
		UnityEngine.Debug.Log($"GPU - Max Renderbuffer Size: {gPULimits.maxRenderBufferSize}");
		UnityEngine.Debug.Log($"GPU - Fragment Max Uniform Vectors: {gPULimits.maxFragmentUniforms}");
		UnityEngine.Debug.Log($"GPU - Vertex Max Uniform Vectors: {gPULimits.maxVertexUniforms}");
		UnityEngine.Debug.Log($"GPU - Max Varying Vectors: {gPULimits.maxVaryings}");
		UnityEngine.Debug.Log($"GPU - Vertex Max Attribs: {gPULimits.maxVertexAttribs}");
	}

	private IEnumerator<IAsyncJobResult> Job_LoadFXAA()
	{
		yield return new LoadResource("Prefabs/FXAA", LoadResourceFlags.AutoInstantiateOnLoad | LoadResourceFlags.FailOnError);
	}
}
