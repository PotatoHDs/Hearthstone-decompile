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

// Token: 0x02000A38 RID: 2616
public class GraphicsManager : IService, IHasUpdate
{
	// Token: 0x170007EB RID: 2027
	// (get) Token: 0x06008CA8 RID: 36008 RVA: 0x002D07FE File Offset: 0x002CE9FE
	// (set) Token: 0x06008CA9 RID: 36009 RVA: 0x002D0806 File Offset: 0x002CEA06
	public GraphicsQuality RenderQualityLevel
	{
		get
		{
			return this.m_GraphicsQuality;
		}
		set
		{
			this.m_GraphicsQuality = value;
			Options.Get().SetInt(Option.GFX_QUALITY, (int)this.m_GraphicsQuality);
			this.UpdateQualitySettings();
		}
	}

	// Token: 0x170007EC RID: 2028
	// (get) Token: 0x06008CAA RID: 36010 RVA: 0x002D0827 File Offset: 0x002CEA27
	public bool RealtimeShadows
	{
		get
		{
			return this.m_RealtimeShadows;
		}
	}

	// Token: 0x14000092 RID: 146
	// (add) Token: 0x06008CAB RID: 36011 RVA: 0x002D0830 File Offset: 0x002CEA30
	// (remove) Token: 0x06008CAC RID: 36012 RVA: 0x002D0868 File Offset: 0x002CEA68
	public event Action<int, int> OnResolutionChangedEvent;

	// Token: 0x06008CAD RID: 36013 RVA: 0x002D089D File Offset: 0x002CEA9D
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield return new JobDefinition("GraphicsManager.LoadFXAA", this.Job_LoadFXAA(), Array.Empty<IJobDependency>());
		this.m_DisableLowQualityObjects = new List<GameObject>();
		if (!Options.Get().HasOption(Option.GFX_QUALITY))
		{
			string intelDeviceName = serviceLocator.Get<ITouchScreenService>().GetIntelDeviceName();
			Log.Graphics.Print("Intel Device Name = {0}", new object[]
			{
				intelDeviceName
			});
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
		this.m_GraphicsQuality = (GraphicsQuality)Options.Get().GetInt(Option.GFX_QUALITY);
		this.m_resizeManager = new ResizeManager(new Action<int, int>(this.OnResolutionChanged), new Action<int, int>(this.CheckPosition));
		this.InitializeScreen();
		this.UpdateQualitySettings();
		if (Options.Get().HasOption(Option.GFX_TARGET_FRAME_RATE))
		{
			this.m_targetFramerate = Options.Get().GetInt(Option.GFX_TARGET_FRAME_RATE);
		}
		Application.targetFrameRate = this.m_targetFramerate;
		this.LogSystemInfo();
		yield break;
	}

	// Token: 0x06008CAE RID: 36014 RVA: 0x002D08B3 File Offset: 0x002CEAB3
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(ITouchScreenService)
		};
	}

	// Token: 0x06008CAF RID: 36015 RVA: 0x002D08C8 File Offset: 0x002CEAC8
	public void Shutdown()
	{
		if (!Screen.fullScreen)
		{
			Options.Get().SetInt(Option.GFX_WIDTH, Screen.width);
			Options.Get().SetInt(Option.GFX_HEIGHT, Screen.height);
			int[] windowPosition = GraphicsManager.GetWindowPosition();
			Options.Get().SetInt(Option.GFX_WIN_POSX, windowPosition[0]);
			Options.Get().SetInt(Option.GFX_WIN_POSY, windowPosition[1]);
		}
		this.OnResolutionChangedEvent = null;
	}

	// Token: 0x06008CB0 RID: 36016 RVA: 0x002D0929 File Offset: 0x002CEB29
	public void Update()
	{
		this.m_resizeManager.Update();
	}

	// Token: 0x06008CB1 RID: 36017 RVA: 0x002D0936 File Offset: 0x002CEB36
	private void CheckPosition(int posX, int posY)
	{
		Processor.RunCoroutine(this.SetPos(posX, posY, 0f), null);
	}

	// Token: 0x06008CB2 RID: 36018 RVA: 0x002D094C File Offset: 0x002CEB4C
	public static GraphicsManager Get()
	{
		return HearthstoneServices.Get<GraphicsManager>();
	}

	// Token: 0x06008CB3 RID: 36019 RVA: 0x002D0953 File Offset: 0x002CEB53
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
				return;
			}
		}
		else
		{
			Application.targetFrameRate = this.m_targetFramerate;
		}
	}

	// Token: 0x06008CB4 RID: 36020 RVA: 0x002D097D File Offset: 0x002CEB7D
	public void RegisterLowQualityDisableObject(GameObject lowQualityObject)
	{
		if (this.m_DisableLowQualityObjects.Contains(lowQualityObject))
		{
			return;
		}
		this.m_DisableLowQualityObjects.Add(lowQualityObject);
	}

	// Token: 0x06008CB5 RID: 36021 RVA: 0x002D099A File Offset: 0x002CEB9A
	public void DeregisterLowQualityDisableObject(GameObject lowQualityObject)
	{
		if (this.m_DisableLowQualityObjects.Contains(lowQualityObject))
		{
			this.m_DisableLowQualityObjects.Remove(lowQualityObject);
		}
	}

	// Token: 0x06008CB6 RID: 36022 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool isVeryLowQualityDevice()
	{
		return false;
	}

	// Token: 0x06008CB7 RID: 36023 RVA: 0x002D09B8 File Offset: 0x002CEBB8
	private static void _GetLimits(ref GraphicsManager.GPULimits limits)
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

	// Token: 0x06008CB8 RID: 36024 RVA: 0x002D0A3C File Offset: 0x002CEC3C
	public GraphicsManager.GPULimits GetGPULimits()
	{
		GraphicsManager.GPULimits result = default(GraphicsManager.GPULimits);
		GraphicsManager._GetLimits(ref result);
		return result;
	}

	// Token: 0x06008CB9 RID: 36025 RVA: 0x002D0A5C File Offset: 0x002CEC5C
	private void InitializeScreen()
	{
		if (!Options.Get().GetBool(Option.GFX_FULLSCREEN))
		{
			if (!Options.Get().HasOption(Option.GFX_WIN_POSX) || !Options.Get().HasOption(Option.GFX_WIN_POSY))
			{
				return;
			}
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
			Processor.RunCoroutine(this.SetPos(num, num2, 0.6f), null);
		}
	}

	// Token: 0x06008CBA RID: 36026 RVA: 0x002D0ACE File Offset: 0x002CECCE
	private void UpdateQualitySettings()
	{
		Log.Graphics.Print("GraphicsManager Update, Graphics Quality: " + this.m_GraphicsQuality.ToString(), Array.Empty<object>());
		this.UpdateRenderQualitySettings();
		this.UpdateAntiAliasing();
	}

	// Token: 0x06008CBB RID: 36027
	[DllImport("user32.dll")]
	private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

	// Token: 0x06008CBC RID: 36028
	[DllImport("user32.dll")]
	private static extern IntPtr FindWindow(string className, string windowName);

	// Token: 0x06008CBD RID: 36029
	[DllImport("user32.dll")]
	private static extern bool EnumWindows(GraphicsManager.EnumWindowsProc enumProc, IntPtr lParam);

	// Token: 0x06008CBE RID: 36030
	[DllImport("user32.dll", SetLastError = true)]
	private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

	// Token: 0x06008CBF RID: 36031 RVA: 0x002D0B08 File Offset: 0x002CED08
	private static bool SetWindowPosition(int x, int y, int resX = 0, int resY = 0)
	{
		if (Vars.IsOptionsFileOverridden())
		{
			GraphicsManager.SetWindowPos(GraphicsManager.GetCurrentProcessWindow(), 0, x, y, resX, resY, (resX * resY == 0) ? 1 : 0);
			return true;
		}
		IntPtr activeWindow = GraphicsManager.GetActiveWindow();
		IntPtr value = GraphicsManager.FindWindow(null, "Hearthstone");
		if (activeWindow == value)
		{
			GraphicsManager.SetWindowPos(activeWindow, 0, x, y, resX, resY, (resX * resY == 0) ? 1 : 0);
			return true;
		}
		return false;
	}

	// Token: 0x06008CC0 RID: 36032 RVA: 0x002D0B6A File Offset: 0x002CED6A
	private static IntPtr GetCurrentProcessWindow()
	{
		IntPtr foundWindow = IntPtr.Zero;
		GraphicsManager.EnumWindows(delegate(IntPtr window, IntPtr param)
		{
			uint num = 0U;
			GraphicsManager.GetWindowThreadProcessId(window, out num);
			if ((long)Process.GetCurrentProcess().Id == (long)((ulong)num))
			{
				foundWindow = window;
				return false;
			}
			return true;
		}, IntPtr.Zero);
		return foundWindow;
	}

	// Token: 0x06008CC1 RID: 36033
	[DllImport("user32.dll")]
	private static extern IntPtr GetForegroundWindow();

	// Token: 0x06008CC2 RID: 36034 RVA: 0x002D0B98 File Offset: 0x002CED98
	private static IntPtr GetActiveWindow()
	{
		return GraphicsManager.GetForegroundWindow();
	}

	// Token: 0x06008CC3 RID: 36035
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetWindowRect(IntPtr hWnd, out GraphicsManager.RECT lpRect);

	// Token: 0x06008CC4 RID: 36036 RVA: 0x002D0BA0 File Offset: 0x002CEDA0
	public static int[] GetWindowPosition()
	{
		int[] array = new int[2];
		GraphicsManager.RECT rect = default(GraphicsManager.RECT);
		GraphicsManager.GetWindowRect(GraphicsManager.GetCurrentProcessWindow(), out rect);
		array[0] = rect.Left;
		array[1] = rect.Top;
		return array;
	}

	// Token: 0x06008CC5 RID: 36037 RVA: 0x002D0BDA File Offset: 0x002CEDDA
	public void SetScreenResolution(int width, int height, bool fullscreen)
	{
		this.m_resizeManager.SetScreenResolution(width, height, fullscreen);
	}

	// Token: 0x06008CC6 RID: 36038 RVA: 0x002D0BEC File Offset: 0x002CEDEC
	private void OnResolutionChanged(int width, int height)
	{
		int[] windowPosition = GraphicsManager.GetWindowPosition();
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
		if (this.m_initialPositionSet)
		{
			Processor.RunCoroutine(this.SetPos(num, num2, 0f), null);
		}
	}

	// Token: 0x06008CC7 RID: 36039 RVA: 0x002D0CC1 File Offset: 0x002CEEC1
	private IEnumerator SetPos(int x, int y, float delay = 0f)
	{
		if (HearthstoneApplication.IsInternal() && !Vars.IsOptionsFileOverridden())
		{
			this.m_initialPositionSet = true;
			yield break;
		}
		yield return new WaitForSeconds(delay);
		this.m_winPosX = x;
		this.m_winPosY = y;
		int[] currentPos = GraphicsManager.GetWindowPosition();
		int[] newPos = new int[]
		{
			this.m_winPosX,
			this.m_winPosY
		};
		float startTime = Time.time;
		while (currentPos != newPos && Time.time < startTime + 1f)
		{
			newPos[0] = this.m_winPosX;
			newPos[1] = this.m_winPosY;
			if (!GraphicsManager.SetWindowPosition(this.m_winPosX, this.m_winPosY, 0, 0))
			{
				break;
			}
			currentPos = GraphicsManager.GetWindowPosition();
			yield return null;
		}
		this.m_initialPositionSet = true;
		yield break;
	}

	// Token: 0x06008CC8 RID: 36040 RVA: 0x002D0CE8 File Offset: 0x002CEEE8
	private void UpdateAntiAliasing()
	{
		bool flag = false;
		int num = 0;
		if (this.m_GraphicsQuality == GraphicsQuality.Low)
		{
			num = 0;
			flag = false;
		}
		if (this.m_GraphicsQuality == GraphicsQuality.Medium)
		{
			num = 2;
			flag = false;
			ITouchScreenService touchScreenService;
			if (HearthstoneServices.TryGet<ITouchScreenService>(out touchScreenService))
			{
				string intelDeviceName = touchScreenService.GetIntelDeviceName();
				if (intelDeviceName != null && (intelDeviceName.Equals("BayTrail") || intelDeviceName.Equals("Poulsbo") || intelDeviceName.Equals("CloverTrail") || (intelDeviceName.Contains("Haswell") && intelDeviceName.Contains("Y6W"))))
				{
					num = 0;
				}
			}
		}
		if (this.m_GraphicsQuality == GraphicsQuality.High)
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
				goto IL_C8;
			}
			num = 0;
			flag = true;
		}
		IL_C8:
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

	// Token: 0x06008CC9 RID: 36041 RVA: 0x002D0E40 File Offset: 0x002CF040
	private void UpdateRenderQualitySettings()
	{
		int vSyncCount = 0;
		int num = 101;
		if (this.m_GraphicsQuality == GraphicsQuality.Low)
		{
			this.m_targetFramerate = 30;
			vSyncCount = 0;
			this.m_RealtimeShadows = false;
			this.SetQualityByName("Low");
			num = 101;
		}
		if (this.m_GraphicsQuality == GraphicsQuality.Medium)
		{
			this.m_targetFramerate = 30;
			vSyncCount = 0;
			this.m_RealtimeShadows = false;
			this.SetQualityByName("Medium");
			num = 201;
		}
		if (this.m_GraphicsQuality == GraphicsQuality.High)
		{
			this.m_targetFramerate = 60;
			vSyncCount = 1;
			this.m_RealtimeShadows = true;
			this.SetQualityByName("High");
			num = 301;
		}
		Shader.DisableKeyword("LOW_QUALITY");
		if (Options.Get().HasOption(Option.GFX_TARGET_FRAME_RATE))
		{
			this.m_targetFramerate = Options.Get().GetInt(Option.GFX_TARGET_FRAME_RATE);
		}
		else
		{
			ITouchScreenService touchScreenService;
			if (HearthstoneServices.TryGet<ITouchScreenService>(out touchScreenService) && touchScreenService.GetBatteryMode() == PowerSource.BatteryPower && this.m_targetFramerate > 30)
			{
				Log.Graphics.Print("Battery Mode Detected - Clamping Target Frame Rate from {0} to 30", new object[]
				{
					this.m_targetFramerate
				});
				this.m_targetFramerate = 30;
				vSyncCount = 0;
			}
			Application.targetFrameRate = this.m_targetFramerate;
		}
		if (Options.Get().HasOption(Option.GFX_VSYNC))
		{
			QualitySettings.vSyncCount = Options.Get().GetInt(Option.GFX_VSYNC);
		}
		else
		{
			QualitySettings.vSyncCount = vSyncCount;
		}
		Log.Graphics.Print(string.Format("Target frame rate: {0}", Application.targetFrameRate), Array.Empty<object>());
		foreach (ProjectedShadow projectedShadow in UnityEngine.Object.FindObjectsOfType(typeof(ProjectedShadow)) as ProjectedShadow[])
		{
			projectedShadow.enabled = (!this.m_RealtimeShadows || projectedShadow.m_enabledAlongsideRealtimeShadows);
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
		foreach (GameObject gameObject in this.m_DisableLowQualityObjects)
		{
			if (!(gameObject == null))
			{
				if (this.m_GraphicsQuality == GraphicsQuality.Low)
				{
					Log.Graphics.Print(string.Format("Low Quality Disable: {0}", gameObject.name), Array.Empty<object>());
					gameObject.SetActive(false);
				}
				else
				{
					Log.Graphics.Print(string.Format("Low Quality Enable: {0}", gameObject.name), Array.Empty<object>());
					gameObject.SetActive(true);
				}
			}
		}
		Shader.globalMaximumLOD = num;
		this.SetScreenEffects();
	}

	// Token: 0x06008CCA RID: 36042 RVA: 0x002D10F8 File Offset: 0x002CF2F8
	private void SetScreenEffects()
	{
		if (ScreenEffectsMgr.Get() != null)
		{
			if (this.m_GraphicsQuality == GraphicsQuality.Low)
			{
				ScreenEffectsMgr.Get().SetActive(false);
				return;
			}
			ScreenEffectsMgr.Get().SetActive(true);
		}
	}

	// Token: 0x06008CCB RID: 36043 RVA: 0x002D1120 File Offset: 0x002CF320
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
			UnityEngine.Debug.LogError(string.Format("GraphicsManager: Quality Level not found: {0}", qualityName));
			return;
		}
		QualitySettings.SetQualityLevel(index, true);
	}

	// Token: 0x06008CCC RID: 36044 RVA: 0x002D116C File Offset: 0x002CF36C
	private void LogSystemInfo()
	{
		UnityEngine.Debug.Log("System Info:");
		UnityEngine.Debug.Log(string.Format("SystemInfo - Device Name: {0}", SystemInfo.deviceName));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Device Model: {0}", SystemInfo.deviceModel));
		UnityEngine.Debug.Log(string.Format("SystemInfo - OS: {0}", SystemInfo.operatingSystem));
		UnityEngine.Debug.Log(string.Format("SystemInfo - CPU Type: {0}", SystemInfo.processorType));
		UnityEngine.Debug.Log(string.Format("SystemInfo - CPU Cores: {0}", SystemInfo.processorCount));
		UnityEngine.Debug.Log(string.Format("SystemInfo - System Memory: {0}", SystemInfo.systemMemorySize));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Screen Resolution: {0}x{1}", Screen.currentResolution.width, Screen.currentResolution.height));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Screen DPI: {0}", Screen.dpi));
		UnityEngine.Debug.Log(string.Format("SystemInfo - GPU ID: {0}", SystemInfo.graphicsDeviceID));
		UnityEngine.Debug.Log(string.Format("SystemInfo - GPU Name: {0}", SystemInfo.graphicsDeviceName));
		UnityEngine.Debug.Log(string.Format("SystemInfo - GPU Vendor: {0}", SystemInfo.graphicsDeviceVendor));
		UnityEngine.Debug.Log(string.Format("SystemInfo - GPU Memory: {0}", SystemInfo.graphicsMemorySize));
		UnityEngine.Debug.Log(string.Format("SystemInfo - GPU Shader Level: {0}", SystemInfo.graphicsShaderLevel));
		UnityEngine.Debug.Log(string.Format("SystemInfo - GPU NPOT Support: {0}", SystemInfo.npotSupport));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Graphics API (version): {0}", SystemInfo.graphicsDeviceVersion));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Graphics API (type): {0}", SystemInfo.graphicsDeviceType));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Graphics Supported Render Target Count: {0}", SystemInfo.supportedRenderTargetCount));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Graphics Supports 3D Textures: {0}", SystemInfo.supports3DTextures));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Graphics Supports Compute Shaders: {0}", SystemInfo.supportsComputeShaders));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Graphics Supports Image Effects: {0}", SystemInfo.supportsImageEffects));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Graphics Supports Render To Cubemap: {0}", SystemInfo.supportsRenderToCubemap));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Graphics Supports Shadows: {0}", SystemInfo.supportsShadows));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Graphics Supports Sparse Textures: {0}", SystemInfo.supportsSparseTextures));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Graphics RenderTextureFormat.ARGBHalf: {0}", SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf)));
		UnityEngine.Debug.Log(string.Format("SystemInfo - Graphics Metal Support: {0}", SystemInfo.graphicsDeviceVersion.StartsWith("Metal")));
	}

	// Token: 0x06008CCD RID: 36045 RVA: 0x002D13F8 File Offset: 0x002CF5F8
	private void LogGPULimits()
	{
		GraphicsManager.GPULimits gpulimits = this.GetGPULimits();
		UnityEngine.Debug.Log("GPU Limits:");
		UnityEngine.Debug.Log(string.Format("GPU - Fragment High Precision: {0}", gpulimits.highPrecisionBits));
		UnityEngine.Debug.Log(string.Format("GPU - Fragment Medium Precision: {0}", gpulimits.mediumPrecisionBits));
		UnityEngine.Debug.Log(string.Format("GPU - Fragment Low Precision: {0}", gpulimits.lowPrecisionBits));
		UnityEngine.Debug.Log(string.Format("GPU - Fragment Max Texture Units: {0}", gpulimits.maxFragmentTextureUnits));
		UnityEngine.Debug.Log(string.Format("GPU - Vertex Max Texture Units: {0}", gpulimits.maxVertexTextureUnits));
		UnityEngine.Debug.Log(string.Format("GPU - Combined Max Texture Units: {0}", gpulimits.maxCombinedTextureUnits));
		UnityEngine.Debug.Log(string.Format("GPU - Max Texture Size: {0}", gpulimits.maxTextureSize));
		UnityEngine.Debug.Log(string.Format("GPU - Max Cube-Map Texture Size: {0}", gpulimits.maxCubeMapSize));
		UnityEngine.Debug.Log(string.Format("GPU - Max Renderbuffer Size: {0}", gpulimits.maxRenderBufferSize));
		UnityEngine.Debug.Log(string.Format("GPU - Fragment Max Uniform Vectors: {0}", gpulimits.maxFragmentUniforms));
		UnityEngine.Debug.Log(string.Format("GPU - Vertex Max Uniform Vectors: {0}", gpulimits.maxVertexUniforms));
		UnityEngine.Debug.Log(string.Format("GPU - Max Varying Vectors: {0}", gpulimits.maxVaryings));
		UnityEngine.Debug.Log(string.Format("GPU - Vertex Max Attribs: {0}", gpulimits.maxVertexAttribs));
	}

	// Token: 0x06008CCE RID: 36046 RVA: 0x002D1568 File Offset: 0x002CF768
	private IEnumerator<IAsyncJobResult> Job_LoadFXAA()
	{
		LoadResource loadResource = new LoadResource("Prefabs/FXAA", LoadResourceFlags.AutoInstantiateOnLoad | LoadResourceFlags.FailOnError);
		yield return loadResource;
		yield break;
	}

	// Token: 0x04007580 RID: 30080
	private const int ANDROID_MIN_DPI_HIGH_RES_TEXTURES = 180;

	// Token: 0x04007581 RID: 30081
	private const int DRAGGING_TARGET_FRAMERATE = 60;

	// Token: 0x04007582 RID: 30082
	private GraphicsQuality m_GraphicsQuality;

	// Token: 0x04007583 RID: 30083
	private bool m_RealtimeShadows;

	// Token: 0x04007584 RID: 30084
	private List<GameObject> m_DisableLowQualityObjects;

	// Token: 0x04007585 RID: 30085
	private int m_targetFramerate = 30;

	// Token: 0x04007586 RID: 30086
	private int m_winPosX;

	// Token: 0x04007587 RID: 30087
	private int m_winPosY;

	// Token: 0x04007588 RID: 30088
	private bool m_initialPositionSet;

	// Token: 0x04007589 RID: 30089
	private ResizeManager m_resizeManager;

	// Token: 0x020026A3 RID: 9891
	public struct GPULimits
	{
		// Token: 0x0400F158 RID: 61784
		public int highPrecisionBits;

		// Token: 0x0400F159 RID: 61785
		public int mediumPrecisionBits;

		// Token: 0x0400F15A RID: 61786
		public int lowPrecisionBits;

		// Token: 0x0400F15B RID: 61787
		public int maxFragmentTextureUnits;

		// Token: 0x0400F15C RID: 61788
		public int maxVertexTextureUnits;

		// Token: 0x0400F15D RID: 61789
		public int maxCombinedTextureUnits;

		// Token: 0x0400F15E RID: 61790
		public int maxTextureSize;

		// Token: 0x0400F15F RID: 61791
		public int maxCubeMapSize;

		// Token: 0x0400F160 RID: 61792
		public int maxRenderBufferSize;

		// Token: 0x0400F161 RID: 61793
		public int maxFragmentUniforms;

		// Token: 0x0400F162 RID: 61794
		public int maxVertexUniforms;

		// Token: 0x0400F163 RID: 61795
		public int maxVaryings;

		// Token: 0x0400F164 RID: 61796
		public int maxVertexAttribs;
	}

	// Token: 0x020026A4 RID: 9892
	// (Invoke) Token: 0x060137EA RID: 79850
	private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

	// Token: 0x020026A5 RID: 9893
	private struct RECT
	{
		// Token: 0x0400F165 RID: 61797
		public int Left;

		// Token: 0x0400F166 RID: 61798
		public int Top;

		// Token: 0x0400F167 RID: 61799
		public int Right;

		// Token: 0x0400F168 RID: 61800
		public int Bottom;
	}
}
