using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x02000A7B RID: 2683
public class ResizeManager
{
	// Token: 0x06009013 RID: 36883 RVA: 0x002EC470 File Offset: 0x002EA670
	public ResizeManager(Action<int, int> onResolutionChanged, Action<int, int> checkPosition)
	{
		this.m_onResolutionChanged = onResolutionChanged;
		bool @bool = Options.Get().GetBool(Option.GFX_FULLSCREEN, true);
		this.m_checkPosition = checkPosition;
		this.m_lastFullScreen = @bool;
		int num;
		int num2;
		if (@bool)
		{
			num = Options.Get().GetInt(Option.GFX_WIDTH, Screen.currentResolution.width);
			num2 = Options.Get().GetInt(Option.GFX_HEIGHT, Screen.currentResolution.height);
			this.m_lastWindowedWidth = (int)((float)Screen.currentResolution.width * 0.75f);
			this.m_lastWindowedHeight = (int)((float)Screen.currentResolution.height * 0.75f);
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
			this.m_lastWindowedWidth = num;
			this.m_lastWindowedHeight = num2;
		}
		this.SetScreenResolution(num, num2, @bool, true);
		this.m_lastWidth = Screen.width;
		this.m_lastHeight = Screen.height;
	}

	// Token: 0x06009014 RID: 36884 RVA: 0x002EC640 File Offset: 0x002EA840
	public void Update()
	{
		if (Screen.fullScreen && !this.m_lastFullScreen)
		{
			this.m_lastFullScreen = true;
			GraphicsResolution largestResolution = GraphicsResolution.GetLargestResolution();
			Screen.SetResolution(largestResolution.x, largestResolution.y, true);
			this.m_onResolutionChanged(largestResolution.x, largestResolution.y);
			Options.Get().SetBool(Option.GFX_FULLSCREEN, Screen.fullScreen);
			Options.Get().SetInt(Option.GFX_WIDTH, largestResolution.x);
			Options.Get().SetInt(Option.GFX_HEIGHT, largestResolution.y);
			return;
		}
		if (!Screen.fullScreen && this.m_lastFullScreen)
		{
			this.m_lastFullScreen = false;
			if (this.m_lastWindowedWidth > 0 && this.m_lastWindowedHeight > 0)
			{
				Screen.SetResolution(this.m_lastWindowedWidth, this.m_lastWindowedHeight, false);
				this.m_onResolutionChanged(this.m_lastWindowedWidth, this.m_lastWindowedHeight);
				Options.Get().SetBool(Option.GFX_FULLSCREEN, Screen.fullScreen);
				Options.Get().SetInt(Option.GFX_WIDTH, this.m_lastWindowedWidth);
				Options.Get().SetInt(Option.GFX_HEIGHT, this.m_lastWindowedHeight);
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
			Screen.SetResolution(num, num2, false);
			this.m_onResolutionChanged(num, num2);
			Options.Get().SetBool(Option.GFX_FULLSCREEN, Screen.fullScreen);
			Options.Get().SetInt(Option.GFX_WIDTH, num);
			Options.Get().SetInt(Option.GFX_HEIGHT, num2);
			return;
		}
		else
		{
			int num3 = Screen.width;
			int num4 = Screen.height;
			if (!GraphicsResolution.IsAspectRatioWithinLimit(num3, num4, !Screen.fullScreen))
			{
				int[] array2 = GraphicsResolution.CalcAspectRatioLimit(num3, num4);
				num3 = array2[0];
				num4 = array2[1];
			}
			this.m_lastFullScreen = Screen.fullScreen;
			if (this.m_lastFullScreen)
			{
				return;
			}
			int[] windowPosition = GraphicsManager.GetWindowPosition();
			int arg = windowPosition[0];
			int arg2 = windowPosition[1];
			if (this.m_lastWidth != num3 || this.m_lastHeight != num4)
			{
				this.m_onResolutionChanged(num3, num4);
			}
			this.m_lastWidth = num3;
			this.m_lastHeight = num4;
			this.m_lastWindowedWidth = num3;
			this.m_lastWindowedHeight = num4;
			if (this.m_lastWidth == Screen.width && this.m_lastHeight == Screen.height)
			{
				this.m_lastChangedResolutionTime = Time.time;
				return;
			}
			if (this.m_lastChangedResolutionTime + 1f < Time.time && this.m_lastChangedResolutionTime + 1f > Time.time - Time.deltaTime)
			{
				this.SetScreenResolution(num3, num4, Screen.fullScreen);
				Options.Get().SetInt(Option.GFX_WIDTH, num3);
				Options.Get().SetInt(Option.GFX_HEIGHT, num4);
				this.m_checkPosition(arg, arg2);
			}
			return;
		}
	}

	// Token: 0x06009015 RID: 36885 RVA: 0x002EC908 File Offset: 0x002EAB08
	public void SetScreenResolution(int width, int height, bool fullscreen)
	{
		this.SetScreenResolution(width, height, fullscreen, false);
	}

	// Token: 0x06009016 RID: 36886 RVA: 0x002EC914 File Offset: 0x002EAB14
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
		if (fullscreen && fullscreen != this.m_lastFullScreen)
		{
			height = Screen.currentResolution.height;
			width = Screen.currentResolution.width;
		}
		Processor.QueueJob("ResizeManager.SetRes", this.Job_SetRes(width, height, fullscreen, fadeToBlack), Array.Empty<IJobDependency>());
	}

	// Token: 0x06009017 RID: 36887 RVA: 0x002EC9AA File Offset: 0x002EABAA
	private IEnumerator<IAsyncJobResult> Job_SetRes(int width, int height, bool fullscreen)
	{
		return this.Job_SetRes(width, height, fullscreen, false);
	}

	// Token: 0x06009018 RID: 36888 RVA: 0x002EC9B6 File Offset: 0x002EABB6
	private IEnumerator<IAsyncJobResult> Job_SetRes(int width, int height, bool fullscreen, bool fadeToBlack)
	{
		yield return HearthstoneServices.CreateServiceSoftDependency(typeof(SceneMgr));
		SceneMgr sceneMgr;
		LoadingScreen loadingScreen;
		if (HearthstoneServices.TryGet<SceneMgr>(out sceneMgr))
		{
			loadingScreen = sceneMgr.LoadingScreen;
		}
		else
		{
			loadingScreen = UnityEngine.Object.FindObjectOfType<LoadingScreen>();
		}
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
		if (fullscreen != this.m_lastFullScreen)
		{
			if (fullscreen)
			{
				width = Screen.currentResolution.width;
				height = Screen.currentResolution.height;
			}
			else
			{
				width = this.m_lastWindowedWidth;
				height = this.m_lastWindowedHeight;
			}
		}
		this.m_lastFullScreen = fullscreen;
		Screen.SetResolution(width, height, fullscreen);
		yield return null;
		Screen.SetResolution(width, height, fullscreen);
		this.m_lastWidth = Screen.width;
		this.m_lastHeight = Screen.height;
		if (!fullscreen)
		{
			this.m_lastWindowedWidth = width;
			this.m_lastWindowedHeight = height;
		}
		camera.depth = prevDepth;
		cameraFade.m_Color = prevColor;
		cameraFade.m_Fade = prevFade;
		cameraFade.m_RenderOverAll = prevROA;
		this.m_onResolutionChanged(width, height);
		yield break;
	}

	// Token: 0x040078F1 RID: 30961
	public const int REDUCE_MAX_WINDOW_SIZE_X = 0;

	// Token: 0x040078F2 RID: 30962
	public const int REDUCE_MAX_WINDOW_SIZE_Y = 0;

	// Token: 0x040078F3 RID: 30963
	private const float RESIZE_COOLDOWN = 1f;

	// Token: 0x040078F4 RID: 30964
	private Action<int, int> m_onResolutionChanged;

	// Token: 0x040078F5 RID: 30965
	private Action<int, int> m_checkPosition;

	// Token: 0x040078F6 RID: 30966
	private bool m_lastFullScreen;

	// Token: 0x040078F7 RID: 30967
	private int m_lastWindowedWidth;

	// Token: 0x040078F8 RID: 30968
	private int m_lastWindowedHeight;

	// Token: 0x040078F9 RID: 30969
	private int m_lastWidth;

	// Token: 0x040078FA RID: 30970
	private int m_lastHeight;

	// Token: 0x040078FB RID: 30971
	private float m_lastChangedResolutionTime = float.MinValue;
}
