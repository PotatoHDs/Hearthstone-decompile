using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using UnityEngine;

public class ResizeManager
{
	public const int REDUCE_MAX_WINDOW_SIZE_X = 0;

	public const int REDUCE_MAX_WINDOW_SIZE_Y = 0;

	private const float RESIZE_COOLDOWN = 1f;

	private Action<int, int> m_onResolutionChanged;

	private Action<int, int> m_checkPosition;

	private bool m_lastFullScreen;

	private int m_lastWindowedWidth;

	private int m_lastWindowedHeight;

	private int m_lastWidth;

	private int m_lastHeight;

	private float m_lastChangedResolutionTime = float.MinValue;

	public ResizeManager(Action<int, int> onResolutionChanged, Action<int, int> checkPosition)
	{
		m_onResolutionChanged = onResolutionChanged;
		bool @bool = Options.Get().GetBool(Option.GFX_FULLSCREEN, defaultVal: true);
		m_checkPosition = checkPosition;
		m_lastFullScreen = @bool;
		int num;
		int num2;
		if (@bool)
		{
			num = Options.Get().GetInt(Option.GFX_WIDTH, Screen.currentResolution.width);
			num2 = Options.Get().GetInt(Option.GFX_HEIGHT, Screen.currentResolution.height);
			m_lastWindowedWidth = (int)((float)Screen.currentResolution.width * 0.75f);
			m_lastWindowedHeight = (int)((float)Screen.currentResolution.height * 0.75f);
			if (!Options.Get().HasOption(Option.GFX_WIDTH) || !Options.Get().HasOption(Option.GFX_HEIGHT))
			{
				string intelDeviceName = HearthstoneServices.Get<ITouchScreenService>().GetIntelDeviceName();
				if (intelDeviceName != null && ((intelDeviceName.Contains("Haswell") && intelDeviceName.Contains("Y6W")) || (intelDeviceName.Contains("Haswell") && intelDeviceName.Contains("U15W"))) && Screen.currentResolution.height >= 1080)
				{
					num = 1920;
					num2 = 1080;
				}
			}
			if (num == Screen.currentResolution.width && num2 == Screen.currentResolution.height && @bool == Screen.fullScreen)
			{
				return;
			}
		}
		else
		{
			num = Options.Get().GetInt(Option.GFX_WIDTH, (int)((float)Screen.currentResolution.width * 0.75f));
			num2 = Options.Get().GetInt(Option.GFX_HEIGHT, (int)((float)Screen.currentResolution.height * 0.75f));
			m_lastWindowedWidth = num;
			m_lastWindowedHeight = num2;
		}
		SetScreenResolution(num, num2, @bool, fadeToBlack: true);
		m_lastWidth = Screen.width;
		m_lastHeight = Screen.height;
	}

	public void Update()
	{
		if (Screen.fullScreen && !m_lastFullScreen)
		{
			m_lastFullScreen = true;
			GraphicsResolution largestResolution = GraphicsResolution.GetLargestResolution();
			Screen.SetResolution(largestResolution.x, largestResolution.y, fullscreen: true);
			m_onResolutionChanged(largestResolution.x, largestResolution.y);
			Options.Get().SetBool(Option.GFX_FULLSCREEN, Screen.fullScreen);
			Options.Get().SetInt(Option.GFX_WIDTH, largestResolution.x);
			Options.Get().SetInt(Option.GFX_HEIGHT, largestResolution.y);
			return;
		}
		if (!Screen.fullScreen && m_lastFullScreen)
		{
			m_lastFullScreen = false;
			if (m_lastWindowedWidth > 0 && m_lastWindowedHeight > 0)
			{
				Screen.SetResolution(m_lastWindowedWidth, m_lastWindowedHeight, fullscreen: false);
				m_onResolutionChanged(m_lastWindowedWidth, m_lastWindowedHeight);
				Options.Get().SetBool(Option.GFX_FULLSCREEN, Screen.fullScreen);
				Options.Get().SetInt(Option.GFX_WIDTH, m_lastWindowedWidth);
				Options.Get().SetInt(Option.GFX_HEIGHT, m_lastWindowedHeight);
				return;
			}
			int num = (int)((float)Screen.currentResolution.width * 0.75f);
			int num2 = (int)((float)Screen.currentResolution.height * 0.75f);
			if (!GraphicsResolution.IsAspectRatioWithinLimit(num, num2, !Screen.fullScreen))
			{
				int[] array = GraphicsResolution.CalcAspectRatioLimit(num, num2);
				num = array[0];
				num2 = array[1];
			}
			Screen.SetResolution(num, num2, fullscreen: false);
			m_onResolutionChanged(num, num2);
			Options.Get().SetBool(Option.GFX_FULLSCREEN, Screen.fullScreen);
			Options.Get().SetInt(Option.GFX_WIDTH, num);
			Options.Get().SetInt(Option.GFX_HEIGHT, num2);
			return;
		}
		int num3 = Screen.width;
		int num4 = Screen.height;
		if (!GraphicsResolution.IsAspectRatioWithinLimit(num3, num4, !Screen.fullScreen))
		{
			int[] array2 = GraphicsResolution.CalcAspectRatioLimit(num3, num4);
			num3 = array2[0];
			num4 = array2[1];
		}
		m_lastFullScreen = Screen.fullScreen;
		if (!m_lastFullScreen)
		{
			int num5 = 0;
			int num6 = 0;
			int[] windowPosition = GraphicsManager.GetWindowPosition();
			num5 = windowPosition[0];
			num6 = windowPosition[1];
			if (m_lastWidth != num3 || m_lastHeight != num4)
			{
				m_onResolutionChanged(num3, num4);
			}
			m_lastWidth = num3;
			m_lastHeight = num4;
			m_lastWindowedWidth = num3;
			m_lastWindowedHeight = num4;
			if (m_lastWidth == Screen.width && m_lastHeight == Screen.height)
			{
				m_lastChangedResolutionTime = Time.time;
			}
			else if (m_lastChangedResolutionTime + 1f < Time.time && m_lastChangedResolutionTime + 1f > Time.time - Time.deltaTime)
			{
				SetScreenResolution(num3, num4, Screen.fullScreen);
				Options.Get().SetInt(Option.GFX_WIDTH, num3);
				Options.Get().SetInt(Option.GFX_HEIGHT, num4);
				m_checkPosition(num5, num6);
			}
		}
	}

	public void SetScreenResolution(int width, int height, bool fullscreen)
	{
		SetScreenResolution(width, height, fullscreen, fadeToBlack: false);
	}

	public void SetScreenResolution(int width, int height, bool fullscreen, bool fadeToBlack)
	{
		if (height > Screen.currentResolution.height && !fullscreen)
		{
			height = Screen.currentResolution.height;
		}
		if (width > Screen.currentResolution.width && !fullscreen)
		{
			width = Screen.currentResolution.width;
		}
		if (fullscreen && fullscreen != m_lastFullScreen)
		{
			height = Screen.currentResolution.height;
			width = Screen.currentResolution.width;
		}
		Processor.QueueJob("ResizeManager.SetRes", Job_SetRes(width, height, fullscreen, fadeToBlack));
	}

	private IEnumerator<IAsyncJobResult> Job_SetRes(int width, int height, bool fullscreen)
	{
		return Job_SetRes(width, height, fullscreen, fadeToBlack: false);
	}

	private IEnumerator<IAsyncJobResult> Job_SetRes(int width, int height, bool fullscreen, bool fadeToBlack)
	{
		yield return HearthstoneServices.CreateServiceSoftDependency(typeof(SceneMgr));
		SceneMgr service;
		LoadingScreen loadingScreen = ((!HearthstoneServices.TryGet<SceneMgr>(out service)) ? UnityEngine.Object.FindObjectOfType<LoadingScreen>() : service.LoadingScreen);
		CameraFade cameraFade = loadingScreen.GetCameraFade();
		Camera camera = loadingScreen.GetFxCamera();
		float prevDepth = camera.depth;
		Color prevColor = cameraFade.m_Color;
		float prevFade = cameraFade.m_Fade;
		bool prevROA = cameraFade.m_RenderOverAll;
		if (!fadeToBlack)
		{
			cameraFade.m_Color = Color.black;
			cameraFade.m_Fade = 1f;
			cameraFade.m_RenderOverAll = true;
		}
		yield return null;
		if (!GraphicsResolution.IsAspectRatioWithinLimit(width, height, !Screen.fullScreen))
		{
			int[] array = GraphicsResolution.CalcAspectRatioLimit(width, height);
			width = array[0];
			height = array[1];
		}
		if (fullscreen != m_lastFullScreen)
		{
			if (fullscreen)
			{
				width = Screen.currentResolution.width;
				height = Screen.currentResolution.height;
			}
			else
			{
				width = m_lastWindowedWidth;
				height = m_lastWindowedHeight;
			}
		}
		m_lastFullScreen = fullscreen;
		Screen.SetResolution(width, height, fullscreen);
		yield return null;
		Screen.SetResolution(width, height, fullscreen);
		m_lastWidth = Screen.width;
		m_lastHeight = Screen.height;
		if (!fullscreen)
		{
			m_lastWindowedWidth = width;
			m_lastWindowedHeight = height;
		}
		camera.depth = prevDepth;
		cameraFade.m_Color = prevColor;
		cameraFade.m_Fade = prevFade;
		cameraFade.m_RenderOverAll = prevROA;
		m_onResolutionChanged(width, height);
	}
}
