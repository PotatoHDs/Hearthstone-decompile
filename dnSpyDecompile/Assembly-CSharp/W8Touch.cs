using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x020005FB RID: 1531
public class W8Touch : ITouchScreenService, IService, IHasUpdate
{
	// Token: 0x1400002E RID: 46
	// (add) Token: 0x0600534A RID: 21322 RVA: 0x001B34D0 File Offset: 0x001B16D0
	// (remove) Token: 0x0600534B RID: 21323 RVA: 0x001B3508 File Offset: 0x001B1708
	private event Action VirtualKeyboardDidShow;

	// Token: 0x1400002F RID: 47
	// (add) Token: 0x0600534C RID: 21324 RVA: 0x001B3540 File Offset: 0x001B1740
	// (remove) Token: 0x0600534D RID: 21325 RVA: 0x001B3578 File Offset: 0x001B1778
	private event Action VirtualKeyboardDidHide;

	// Token: 0x0600534E RID: 21326
	[DllImport("User32.dll")]
	public static extern IntPtr FindWindow(string className, string windowName);

	// Token: 0x0600534F RID: 21327 RVA: 0x001B35AD File Offset: 0x001B17AD
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		if (this.LoadW8TouchDLL())
		{
			this.m_isWindows8OrGreater = W8Touch.DLL_W8IsWindows8OrGreater();
		}
		this.m_touchState = new W8Touch.TouchState[5];
		for (int i = 0; i < 5; i++)
		{
			this.m_touchState[i] = W8Touch.TouchState.None;
		}
		Processor.RegisterOnGUIDelegate(new Action(this.OnGUI));
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().OnShutdown += this.OnApplicationQuit;
		}
		yield break;
	}

	// Token: 0x06005350 RID: 21328 RVA: 0x001B35BC File Offset: 0x001B17BC
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(UniversalInputManager)
		};
	}

	// Token: 0x06005351 RID: 21329 RVA: 0x001B35D1 File Offset: 0x001B17D1
	public void Shutdown()
	{
		HearthstoneApplication.Get().OnShutdown -= this.OnApplicationQuit;
	}

	// Token: 0x06005352 RID: 21330 RVA: 0x001B35EC File Offset: 0x001B17EC
	public void Update()
	{
		if (!this.IsInitialized())
		{
			return;
		}
		W8Touch.DLL_W8GetDesktopRect(out this.m_desktopRect);
		bool flag = W8Touch.DLL_W8IsVirtualKeyboardVisible();
		if (flag != this.m_isVirtualKeyboardVisible)
		{
			this.m_isVirtualKeyboardVisible = flag;
			if (flag && this.VirtualKeyboardDidShow != null)
			{
				this.VirtualKeyboardDidShow();
			}
			else if (!flag && this.VirtualKeyboardDidHide != null)
			{
				this.VirtualKeyboardDidHide();
			}
		}
		if (this.m_isVirtualKeyboardVisible)
		{
			this.m_isVirtualKeyboardShowRequested = false;
		}
		else
		{
			this.m_isVirtualKeyboardHideRequested = false;
		}
		PowerSource batteryMode = this.GetBatteryMode();
		GraphicsManager graphicsManager;
		if (batteryMode != this.m_lastPowerSourceState && HearthstoneServices.TryGet<GraphicsManager>(out graphicsManager))
		{
			Log.W8Touch.Print("PowerSource Change Detected: {0}", new object[]
			{
				batteryMode
			});
			this.m_lastPowerSourceState = batteryMode;
			graphicsManager.RenderQualityLevel = (GraphicsQuality)Options.Get().GetInt(Option.GFX_QUALITY);
		}
		if ((!W8Touch.DLL_W8IsLastEventFromTouch() && UniversalInputManager.Get().UseWindowsTouch()) || (W8Touch.DLL_W8IsLastEventFromTouch() && !UniversalInputManager.Get().UseWindowsTouch()))
		{
			this.ToggleTouchMode();
		}
		if (this.m_touchState != null)
		{
			int num = W8Touch.DLL_W8GetTouchPointCount();
			for (int i = 0; i < 5; i++)
			{
				W8Touch.tTouchData tTouchData = new W8Touch.tTouchData();
				bool flag2 = false;
				if (i < num)
				{
					flag2 = W8Touch.DLL_W8GetTouchPoint(i, tTouchData);
				}
				if (flag2 && i == 0)
				{
					Vector2 vector = this.TransformTouchPosition(new Vector2((float)tTouchData.m_x, (float)tTouchData.m_y));
					if (this.m_touchPosition.x != -1f && this.m_touchPosition.y != -1f && this.m_touchState[i] == W8Touch.TouchState.Down)
					{
						this.m_touchDelta.x = vector.x - this.m_touchPosition.x;
						this.m_touchDelta.y = vector.y - this.m_touchPosition.y;
					}
					else
					{
						this.m_touchDelta.x = (this.m_touchDelta.y = 0f);
					}
					this.m_touchPosition.x = vector.x;
					this.m_touchPosition.y = vector.y;
				}
				if (flag2 && tTouchData.m_ID != -1)
				{
					if (this.m_touchState[i] == W8Touch.TouchState.Down || this.m_touchState[i] == W8Touch.TouchState.InitialDown)
					{
						this.m_touchState[i] = W8Touch.TouchState.Down;
					}
					else
					{
						this.m_touchState[i] = W8Touch.TouchState.InitialDown;
					}
				}
				else if (this.m_touchState[i] == W8Touch.TouchState.Down || this.m_touchState[i] == W8Touch.TouchState.InitialDown)
				{
					this.m_touchState[i] = W8Touch.TouchState.InitialUp;
				}
				else
				{
					this.m_touchState[i] = W8Touch.TouchState.None;
				}
			}
		}
	}

	// Token: 0x06005353 RID: 21331 RVA: 0x001B388D File Offset: 0x001B1A8D
	private void OnGUI()
	{
		if (!this.m_isWindows8OrGreater && this.m_DLL == IntPtr.Zero)
		{
			return;
		}
		if (!this.m_initialized)
		{
			this.InitializeDLL();
		}
	}

	// Token: 0x06005354 RID: 21332 RVA: 0x001B38B8 File Offset: 0x001B1AB8
	private Vector2 TransformTouchPosition(Vector2 touchInput)
	{
		Vector2 result = default(Vector2);
		if (Screen.fullScreen)
		{
			float num = (float)Screen.width / (float)Screen.height;
			float num2 = (float)this.m_desktopRect.Right / (float)this.m_desktopRect.Bottom;
			if (Mathf.Abs(num - num2) < Mathf.Epsilon)
			{
				float num3 = (float)Screen.width / (float)this.m_desktopRect.Right;
				float num4 = (float)Screen.height / (float)this.m_desktopRect.Bottom;
				result.x = touchInput.x * num3;
				result.y = ((float)this.m_desktopRect.Bottom - touchInput.y) * num4;
			}
			else if (num < num2)
			{
				float num5 = (float)this.m_desktopRect.Bottom;
				float num6 = num5 * num;
				float num7 = (float)Screen.height / num5;
				float num8 = (float)Screen.width / num6;
				float num9 = ((float)this.m_desktopRect.Right - num6) / 2f;
				result.x = (touchInput.x - num9) * num8;
				result.y = ((float)this.m_desktopRect.Bottom - touchInput.y) * num7;
			}
			else
			{
				float num10 = (float)this.m_desktopRect.Right;
				float num11 = num10 / num;
				float num12 = (float)Screen.height / num11;
				float num13 = (float)Screen.width / num10;
				float num14 = ((float)this.m_desktopRect.Bottom - num11) / 2f;
				result.x = touchInput.x * num13;
				result.y = ((float)this.m_desktopRect.Bottom - touchInput.y - num14) * num12;
			}
		}
		else
		{
			result.x = touchInput.x;
			result.y = (float)Screen.height - touchInput.y;
		}
		return result;
	}

	// Token: 0x06005355 RID: 21333 RVA: 0x001B3A78 File Offset: 0x001B1C78
	private void ToggleTouchMode()
	{
		if (!this.IsInitialized())
		{
			return;
		}
		bool @bool = Options.Get().GetBool(Option.TOUCH_MODE);
		Options.Get().SetBool(Option.TOUCH_MODE, !@bool);
	}

	// Token: 0x06005356 RID: 21334 RVA: 0x001B3AAC File Offset: 0x001B1CAC
	public void ShowKeyboard()
	{
		if (!this.IsInitialized() || this.m_isVirtualKeyboardShowRequested || (this.m_isVirtualKeyboardVisible && !this.m_isVirtualKeyboardHideRequested))
		{
			return;
		}
		if (this.m_isVirtualKeyboardHideRequested)
		{
			this.m_isVirtualKeyboardHideRequested = false;
		}
		W8Touch.KeyboardFlags keyboardFlags = (W8Touch.KeyboardFlags)W8Touch.DLL_W8ShowKeyboard();
		W8Touch.KeyboardFlags keyboardFlags2 = keyboardFlags & W8Touch.KeyboardFlags.Shown;
		if ((keyboardFlags & W8Touch.KeyboardFlags.Shown) == W8Touch.KeyboardFlags.Shown && (keyboardFlags & W8Touch.KeyboardFlags.SuccessTabTip) == W8Touch.KeyboardFlags.SuccessTabTip)
		{
			this.m_isVirtualKeyboardShowRequested = true;
		}
	}

	// Token: 0x06005357 RID: 21335 RVA: 0x001B3B0D File Offset: 0x001B1D0D
	public void HideKeyboard()
	{
		if (!this.IsInitialized() && !this.m_isVirtualKeyboardVisible)
		{
			return;
		}
		if (this.m_isVirtualKeyboardShowRequested)
		{
			this.m_isVirtualKeyboardShowRequested = false;
		}
		if (W8Touch.DLL_W8HideKeyboard() == 0)
		{
			this.m_isVirtualKeyboardHideRequested = true;
		}
	}

	// Token: 0x06005358 RID: 21336 RVA: 0x001B3B42 File Offset: 0x001B1D42
	public void ShowOSK()
	{
		if (!this.IsInitialized())
		{
			return;
		}
		int num = W8Touch.DLL_W8ShowOSK() & 1;
	}

	// Token: 0x06005359 RID: 21337 RVA: 0x001B3B5C File Offset: 0x001B1D5C
	public string GetIntelDeviceName()
	{
		if (!this.IsInitialized())
		{
			return null;
		}
		return W8Touch.IntelDevice.GetDeviceName(W8Touch.DLL_W8GetDeviceId());
	}

	// Token: 0x0600535A RID: 21338 RVA: 0x001B3B77 File Offset: 0x001B1D77
	public PowerSource GetBatteryMode()
	{
		if (!this.IsInitialized())
		{
			return PowerSource.Unintialized;
		}
		return (PowerSource)W8Touch.DLL_W8GetBatteryMode();
	}

	// Token: 0x0600535B RID: 21339 RVA: 0x001B3B8D File Offset: 0x001B1D8D
	public int GetPercentBatteryLife()
	{
		if (!this.IsInitialized())
		{
			return -1;
		}
		return W8Touch.DLL_W8GetPercentBatteryLife();
	}

	// Token: 0x0600535C RID: 21340 RVA: 0x001B3BA3 File Offset: 0x001B1DA3
	public bool IsVirtualKeyboardVisible()
	{
		return this.IsInitialized() && this.m_isVirtualKeyboardVisible;
	}

	// Token: 0x0600535D RID: 21341 RVA: 0x001B3BB5 File Offset: 0x001B1DB5
	public bool GetTouch(int touchCount)
	{
		return this.IsInitialized() && this.m_touchState != null && touchCount < 5 && (this.m_touchState[touchCount] == W8Touch.TouchState.InitialDown || this.m_touchState[touchCount] == W8Touch.TouchState.Down);
	}

	// Token: 0x0600535E RID: 21342 RVA: 0x001B3BE6 File Offset: 0x001B1DE6
	public bool GetTouchDown(int touchCount)
	{
		return this.IsInitialized() && this.m_touchState != null && touchCount < 5 && this.m_touchState[touchCount] == W8Touch.TouchState.InitialDown;
	}

	// Token: 0x0600535F RID: 21343 RVA: 0x001B3C0C File Offset: 0x001B1E0C
	public bool GetTouchUp(int touchCount)
	{
		return this.IsInitialized() && this.m_touchState != null && touchCount < 5 && this.m_touchState[touchCount] == W8Touch.TouchState.InitialUp;
	}

	// Token: 0x06005360 RID: 21344 RVA: 0x001B3C34 File Offset: 0x001B1E34
	public Vector3 GetTouchPosition()
	{
		if (!this.IsInitialized() || this.m_touchState == null)
		{
			return new Vector3(0f, 0f, 0f);
		}
		return new Vector3(this.m_touchPosition.x, this.m_touchPosition.y, this.m_touchPosition.z);
	}

	// Token: 0x06005361 RID: 21345 RVA: 0x001B3C8C File Offset: 0x001B1E8C
	public Vector2 GetTouchDelta()
	{
		if (!this.IsInitialized() || this.m_touchState == null)
		{
			return new Vector2(0f, 0f);
		}
		return new Vector2(this.m_touchDelta.x, this.m_touchDelta.y);
	}

	// Token: 0x06005362 RID: 21346 RVA: 0x001B3CCC File Offset: 0x001B1ECC
	public Vector3 GetTouchPositionForGUI()
	{
		if (!this.IsInitialized() || this.m_touchState == null)
		{
			return new Vector3(0f, 0f, 0f);
		}
		Vector2 vector = this.TransformTouchPosition(this.m_touchPosition);
		return new Vector3(vector.x, vector.y, this.m_touchPosition.z);
	}

	// Token: 0x06005363 RID: 21347 RVA: 0x001B3D2C File Offset: 0x001B1F2C
	public bool IsTouchSupported()
	{
		return this.m_isWindows8OrGreater;
	}

	// Token: 0x06005364 RID: 21348 RVA: 0x001B3D34 File Offset: 0x001B1F34
	public void AddOnVirtualKeyboardShowListener(Action listener)
	{
		this.VirtualKeyboardDidShow -= listener;
		this.VirtualKeyboardDidShow += listener;
	}

	// Token: 0x06005365 RID: 21349 RVA: 0x001B3D44 File Offset: 0x001B1F44
	public void RemoveOnVirtualKeyboardShowListener(Action listener)
	{
		this.VirtualKeyboardDidShow -= listener;
	}

	// Token: 0x06005366 RID: 21350 RVA: 0x001B3D4D File Offset: 0x001B1F4D
	public void AddOnVirtualKeyboardHideListener(Action listener)
	{
		this.VirtualKeyboardDidHide -= listener;
		this.VirtualKeyboardDidHide += listener;
	}

	// Token: 0x06005367 RID: 21351 RVA: 0x001B3D5D File Offset: 0x001B1F5D
	public void RemoveOnVirtualKeyboardHideListener(Action listener)
	{
		this.VirtualKeyboardDidHide -= listener;
	}

	// Token: 0x06005368 RID: 21352 RVA: 0x001B3D66 File Offset: 0x001B1F66
	private IntPtr GetFunction(string name)
	{
		IntPtr procAddress = DLLUtils.GetProcAddress(this.m_DLL, name);
		if (procAddress == IntPtr.Zero)
		{
			Debug.LogError("Could not load W8TouchDLL." + name + "()");
			this.OnApplicationQuit();
		}
		return procAddress;
	}

	// Token: 0x06005369 RID: 21353 RVA: 0x001B3D9C File Offset: 0x001B1F9C
	private bool LoadW8TouchDLL()
	{
		if (Environment.OSVersion.Version.Major < 6 || (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor < 2))
		{
			Log.W8Touch.Print("Windows Version is Pre-Windows 8", Array.Empty<object>());
			return false;
		}
		if (this.m_DLL == IntPtr.Zero)
		{
			this.m_DLL = FileUtils.LoadPlugin("W8TouchDLL", false);
			if (this.m_DLL == IntPtr.Zero)
			{
				Log.W8Touch.Print("Could not load W8TouchDLL.dll", Array.Empty<object>());
				return false;
			}
		}
		W8Touch.DLL_W8ShowKeyboard = (W8Touch.DelW8ShowKeyboard)Marshal.GetDelegateForFunctionPointer(this.GetFunction("W8_ShowKeyboard"), typeof(W8Touch.DelW8ShowKeyboard));
		W8Touch.DLL_W8HideKeyboard = (W8Touch.DelW8HideKeyboard)Marshal.GetDelegateForFunctionPointer(this.GetFunction("W8_HideKeyboard"), typeof(W8Touch.DelW8HideKeyboard));
		W8Touch.DLL_W8ShowOSK = (W8Touch.DelW8ShowOSK)Marshal.GetDelegateForFunctionPointer(this.GetFunction("W8_ShowOSK"), typeof(W8Touch.DelW8ShowOSK));
		W8Touch.DLL_W8Initialize = (W8Touch.DelW8Initialize)Marshal.GetDelegateForFunctionPointer(this.GetFunction("W8_Initialize"), typeof(W8Touch.DelW8Initialize));
		W8Touch.DLL_W8Shutdown = (W8Touch.DelW8Shutdown)Marshal.GetDelegateForFunctionPointer(this.GetFunction("W8_Shutdown"), typeof(W8Touch.DelW8Shutdown));
		W8Touch.DLL_W8GetDeviceId = (W8Touch.DelW8GetDeviceId)Marshal.GetDelegateForFunctionPointer(this.GetFunction("W8_GetDeviceId"), typeof(W8Touch.DelW8GetDeviceId));
		W8Touch.DLL_W8IsWindows8OrGreater = (W8Touch.DelW8IsWindows8OrGreater)Marshal.GetDelegateForFunctionPointer(this.GetFunction("W8_IsWindows8OrGreater"), typeof(W8Touch.DelW8IsWindows8OrGreater));
		W8Touch.DLL_W8IsLastEventFromTouch = (W8Touch.DelW8IsLastEventFromTouch)Marshal.GetDelegateForFunctionPointer(this.GetFunction("W8_IsLastEventFromTouch"), typeof(W8Touch.DelW8IsLastEventFromTouch));
		W8Touch.DLL_W8GetBatteryMode = (W8Touch.DelW8GetBatteryMode)Marshal.GetDelegateForFunctionPointer(this.GetFunction("W8_GetBatteryMode"), typeof(W8Touch.DelW8GetBatteryMode));
		W8Touch.DLL_W8GetPercentBatteryLife = (W8Touch.DelW8GetPercentBatteryLife)Marshal.GetDelegateForFunctionPointer(this.GetFunction("W8_GetPercentBatteryLife"), typeof(W8Touch.DelW8GetPercentBatteryLife));
		W8Touch.DLL_W8GetDesktopRect = (W8Touch.DelW8GetDesktopRect)Marshal.GetDelegateForFunctionPointer(this.GetFunction("W8_GetDesktopRect"), typeof(W8Touch.DelW8GetDesktopRect));
		W8Touch.DLL_W8IsVirtualKeyboardVisible = (W8Touch.DelW8IsVirtualKeyboardVisible)Marshal.GetDelegateForFunctionPointer(this.GetFunction("W8_IsVirtualKeyboardVisible"), typeof(W8Touch.DelW8IsVirtualKeyboardVisible));
		W8Touch.DLL_W8GetTouchPointCount = (W8Touch.DelW8GetTouchPointCount)Marshal.GetDelegateForFunctionPointer(this.GetFunction("GetTouchPointCount"), typeof(W8Touch.DelW8GetTouchPointCount));
		W8Touch.DLL_W8GetTouchPoint = (W8Touch.DelW8GetTouchPoint)Marshal.GetDelegateForFunctionPointer(this.GetFunction("GetTouchPoint"), typeof(W8Touch.DelW8GetTouchPoint));
		return true;
	}

	// Token: 0x0600536A RID: 21354 RVA: 0x001B403C File Offset: 0x001B223C
	private void OnApplicationQuit()
	{
		Log.W8Touch.Print("W8Touch.AppQuit()", Array.Empty<object>());
		if (this.m_DLL == IntPtr.Zero)
		{
			return;
		}
		this.ResetWindowFeedbackSetting();
		if (W8Touch.DLL_W8Shutdown != null && this.m_initialized)
		{
			W8Touch.DLL_W8Shutdown();
			this.m_initialized = false;
		}
		if (!DLLUtils.FreeLibrary(this.m_DLL))
		{
			Debug.Log("Error unloading W8TouchDLL.dll");
		}
		this.m_DLL = IntPtr.Zero;
	}

	// Token: 0x0600536B RID: 21355 RVA: 0x001B40B8 File Offset: 0x001B22B8
	private bool IsInitialized()
	{
		return this.m_DLL != IntPtr.Zero && this.m_isWindows8OrGreater && this.m_initialized;
	}

	// Token: 0x0600536C RID: 21356 RVA: 0x001B40DC File Offset: 0x001B22DC
	private void InitializeDLL()
	{
		if (this.m_intializationAttemptCount >= 10)
		{
			return;
		}
		string windowName = GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE");
		if (W8Touch.DLL_W8Initialize(windowName) < 0)
		{
			this.m_intializationAttemptCount++;
			return;
		}
		Log.W8Touch.Print("W8Touch Start Success!", Array.Empty<object>());
		this.m_initialized = true;
		IntPtr intPtr = DLLUtils.LoadLibrary("User32.DLL");
		if (intPtr == IntPtr.Zero)
		{
			Log.W8Touch.Print("Could not load User32.DLL", Array.Empty<object>());
			return;
		}
		IntPtr procAddress = DLLUtils.GetProcAddress(intPtr, "SetWindowFeedbackSetting");
		if (procAddress == IntPtr.Zero)
		{
			Log.W8Touch.Print("Could not load User32.SetWindowFeedbackSetting()", Array.Empty<object>());
		}
		else
		{
			IntPtr intPtr2 = W8Touch.FindWindow(null, "Hearthstone");
			if (intPtr2 == IntPtr.Zero)
			{
				intPtr2 = W8Touch.FindWindow(null, GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE"));
			}
			if (intPtr2 == IntPtr.Zero)
			{
				Log.W8Touch.Print("Unable to retrieve Hearthstone window handle!", Array.Empty<object>());
			}
			else
			{
				W8Touch.DelSetWindowFeedbackSetting delSetWindowFeedbackSetting = (W8Touch.DelSetWindowFeedbackSetting)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(W8Touch.DelSetWindowFeedbackSetting));
				int num = Marshal.SizeOf(typeof(int));
				IntPtr intPtr3 = Marshal.AllocHGlobal(num);
				Marshal.WriteInt32(intPtr3, 0, this.m_bWindowFeedbackSettingValue ? 1 : 0);
				bool bIsWindowFeedbackDisabled = true;
				if (!delSetWindowFeedbackSetting(intPtr2, W8Touch.FEEDBACK_TYPE.FEEDBACK_TOUCH_CONTACTVISUALIZATION, 0U, Convert.ToUInt32(num), intPtr3))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_CONTACTVISUALIZATION failed!", Array.Empty<object>());
					bIsWindowFeedbackDisabled = false;
				}
				if (!delSetWindowFeedbackSetting(intPtr2, W8Touch.FEEDBACK_TYPE.FEEDBACK_TOUCH_TAP, 0U, Convert.ToUInt32(num), intPtr3))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_TAP failed!", Array.Empty<object>());
					bIsWindowFeedbackDisabled = false;
				}
				if (!delSetWindowFeedbackSetting(intPtr2, W8Touch.FEEDBACK_TYPE.FEEDBACK_TOUCH_PRESSANDHOLD, 0U, Convert.ToUInt32(num), intPtr3))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_PRESSANDHOLD failed!", Array.Empty<object>());
					bIsWindowFeedbackDisabled = false;
				}
				if (!delSetWindowFeedbackSetting(intPtr2, W8Touch.FEEDBACK_TYPE.FEEDBACK_TOUCH_DOUBLETAP, 0U, Convert.ToUInt32(num), intPtr3))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_DOUBLETAP failed!", Array.Empty<object>());
					bIsWindowFeedbackDisabled = false;
				}
				if (!delSetWindowFeedbackSetting(intPtr2, W8Touch.FEEDBACK_TYPE.FEEDBACK_TOUCH_RIGHTTAP, 0U, Convert.ToUInt32(num), intPtr3))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_RIGHTTAP failed!", Array.Empty<object>());
					bIsWindowFeedbackDisabled = false;
				}
				if (!delSetWindowFeedbackSetting(intPtr2, W8Touch.FEEDBACK_TYPE.FEEDBACK_GESTURE_PRESSANDTAP, 0U, Convert.ToUInt32(num), intPtr3))
				{
					Log.W8Touch.Print("FEEDBACK_GESTURE_PRESSANDTAP failed!", Array.Empty<object>());
					bIsWindowFeedbackDisabled = false;
				}
				this.m_bIsWindowFeedbackDisabled = bIsWindowFeedbackDisabled;
				if (this.m_bIsWindowFeedbackDisabled)
				{
					Log.W8Touch.Print("Windows 8 Feedback Touch Gestures Disabled!", Array.Empty<object>());
				}
				Marshal.FreeHGlobal(intPtr3);
			}
		}
		if (!DLLUtils.FreeLibrary(intPtr))
		{
			Log.W8Touch.Print("Error unloading User32.dll", Array.Empty<object>());
		}
	}

	// Token: 0x0600536D RID: 21357 RVA: 0x001B4378 File Offset: 0x001B2578
	private void ResetWindowFeedbackSetting()
	{
		if (this.m_initialized && this.m_bIsWindowFeedbackDisabled)
		{
			IntPtr intPtr = DLLUtils.LoadLibrary("User32.DLL");
			if (intPtr == IntPtr.Zero)
			{
				Log.W8Touch.Print("Could not load User32.DLL", Array.Empty<object>());
				return;
			}
			IntPtr procAddress = DLLUtils.GetProcAddress(intPtr, "SetWindowFeedbackSetting");
			if (procAddress == IntPtr.Zero)
			{
				Log.W8Touch.Print("Could not load User32.SetWindowFeedbackSetting()", Array.Empty<object>());
			}
			else
			{
				IntPtr intPtr2 = W8Touch.FindWindow(null, "Hearthstone");
				if (intPtr2 == IntPtr.Zero)
				{
					intPtr2 = W8Touch.FindWindow(null, GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE"));
				}
				if (intPtr2 == IntPtr.Zero)
				{
					Log.W8Touch.Print("Unable to retrieve Hearthstone window handle!", Array.Empty<object>());
				}
				else
				{
					W8Touch.DelSetWindowFeedbackSetting delSetWindowFeedbackSetting = (W8Touch.DelSetWindowFeedbackSetting)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(W8Touch.DelSetWindowFeedbackSetting));
					IntPtr intPtr3 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
					Marshal.WriteInt32(intPtr3, 0, this.m_bWindowFeedbackSettingValue ? 1 : 0);
					bool flag = true;
					if (!delSetWindowFeedbackSetting(intPtr2, W8Touch.FEEDBACK_TYPE.FEEDBACK_TOUCH_CONTACTVISUALIZATION, 0U, 0U, IntPtr.Zero))
					{
						Log.W8Touch.Print("FEEDBACK_TOUCH_CONTACTVISUALIZATION failed!", Array.Empty<object>());
						flag = false;
					}
					if (!delSetWindowFeedbackSetting(intPtr2, W8Touch.FEEDBACK_TYPE.FEEDBACK_TOUCH_TAP, 0U, 0U, IntPtr.Zero))
					{
						Log.W8Touch.Print("FEEDBACK_TOUCH_TAP failed!", Array.Empty<object>());
						flag = false;
					}
					if (!delSetWindowFeedbackSetting(intPtr2, W8Touch.FEEDBACK_TYPE.FEEDBACK_TOUCH_PRESSANDHOLD, 0U, 0U, IntPtr.Zero))
					{
						Log.W8Touch.Print("FEEDBACK_TOUCH_PRESSANDHOLD failed!", Array.Empty<object>());
						flag = false;
					}
					if (!delSetWindowFeedbackSetting(intPtr2, W8Touch.FEEDBACK_TYPE.FEEDBACK_TOUCH_DOUBLETAP, 0U, 0U, IntPtr.Zero))
					{
						Log.W8Touch.Print("FEEDBACK_TOUCH_DOUBLETAP failed!", Array.Empty<object>());
						flag = false;
					}
					if (!delSetWindowFeedbackSetting(intPtr2, W8Touch.FEEDBACK_TYPE.FEEDBACK_TOUCH_RIGHTTAP, 0U, 0U, IntPtr.Zero))
					{
						Log.W8Touch.Print("FEEDBACK_TOUCH_RIGHTTAP failed!", Array.Empty<object>());
						flag = false;
					}
					if (!delSetWindowFeedbackSetting(intPtr2, W8Touch.FEEDBACK_TYPE.FEEDBACK_GESTURE_PRESSANDTAP, 0U, 0U, IntPtr.Zero))
					{
						Log.W8Touch.Print("FEEDBACK_GESTURE_PRESSANDTAP failed!", Array.Empty<object>());
						flag = false;
					}
					this.m_bIsWindowFeedbackDisabled = !flag;
					if (!this.m_bIsWindowFeedbackDisabled)
					{
						Log.W8Touch.Print("Windows 8 Feedback Touch Gestures Reset!", Array.Empty<object>());
					}
					Marshal.FreeHGlobal(intPtr3);
				}
			}
			if (!DLLUtils.FreeLibrary(intPtr))
			{
				Log.W8Touch.Print("Error unloading User32.dll", Array.Empty<object>());
			}
		}
	}

	// Token: 0x040049F0 RID: 18928
	private bool m_initialized;

	// Token: 0x040049F1 RID: 18929
	public bool m_isWindows8OrGreater;

	// Token: 0x040049F4 RID: 18932
	private IntPtr m_DLL = IntPtr.Zero;

	// Token: 0x040049F5 RID: 18933
	private const int MaxTouches = 5;

	// Token: 0x040049F6 RID: 18934
	private const int MaxInitializationAttempts = 10;

	// Token: 0x040049F7 RID: 18935
	private int m_intializationAttemptCount;

	// Token: 0x040049F8 RID: 18936
	private W8Touch.TouchState[] m_touchState;

	// Token: 0x040049F9 RID: 18937
	private Vector3 m_touchPosition = new Vector3(-1f, -1f, 0f);

	// Token: 0x040049FA RID: 18938
	private Vector2 m_touchDelta = new Vector2(0f, 0f);

	// Token: 0x040049FB RID: 18939
	private W8Touch.RECT m_desktopRect;

	// Token: 0x040049FC RID: 18940
	private bool m_isVirtualKeyboardVisible;

	// Token: 0x040049FD RID: 18941
	private bool m_isVirtualKeyboardShowRequested;

	// Token: 0x040049FE RID: 18942
	private bool m_isVirtualKeyboardHideRequested;

	// Token: 0x040049FF RID: 18943
	private PowerSource m_lastPowerSourceState = PowerSource.Unintialized;

	// Token: 0x04004A00 RID: 18944
	private bool m_bWindowFeedbackSettingValue;

	// Token: 0x04004A01 RID: 18945
	private bool m_bIsWindowFeedbackDisabled;

	// Token: 0x04004A02 RID: 18946
	private static W8Touch.DelW8ShowKeyboard DLL_W8ShowKeyboard;

	// Token: 0x04004A03 RID: 18947
	private static W8Touch.DelW8HideKeyboard DLL_W8HideKeyboard;

	// Token: 0x04004A04 RID: 18948
	private static W8Touch.DelW8ShowOSK DLL_W8ShowOSK;

	// Token: 0x04004A05 RID: 18949
	private static W8Touch.DelW8Initialize DLL_W8Initialize;

	// Token: 0x04004A06 RID: 18950
	private static W8Touch.DelW8Shutdown DLL_W8Shutdown;

	// Token: 0x04004A07 RID: 18951
	private static W8Touch.DelW8GetDeviceId DLL_W8GetDeviceId;

	// Token: 0x04004A08 RID: 18952
	private static W8Touch.DelW8IsWindows8OrGreater DLL_W8IsWindows8OrGreater;

	// Token: 0x04004A09 RID: 18953
	private static W8Touch.DelW8IsLastEventFromTouch DLL_W8IsLastEventFromTouch;

	// Token: 0x04004A0A RID: 18954
	private static W8Touch.DelW8GetBatteryMode DLL_W8GetBatteryMode;

	// Token: 0x04004A0B RID: 18955
	private static W8Touch.DelW8GetPercentBatteryLife DLL_W8GetPercentBatteryLife;

	// Token: 0x04004A0C RID: 18956
	private static W8Touch.DelW8GetDesktopRect DLL_W8GetDesktopRect;

	// Token: 0x04004A0D RID: 18957
	private static W8Touch.DelW8IsVirtualKeyboardVisible DLL_W8IsVirtualKeyboardVisible;

	// Token: 0x04004A0E RID: 18958
	private static W8Touch.DelW8GetTouchPointCount DLL_W8GetTouchPointCount;

	// Token: 0x04004A0F RID: 18959
	private static W8Touch.DelW8GetTouchPoint DLL_W8GetTouchPoint;

	// Token: 0x0200202B RID: 8235
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class tTouchData
	{
		// Token: 0x0400DC49 RID: 56393
		public int m_x;

		// Token: 0x0400DC4A RID: 56394
		public int m_y;

		// Token: 0x0400DC4B RID: 56395
		public int m_ID;

		// Token: 0x0400DC4C RID: 56396
		public int m_Time;
	}

	// Token: 0x0200202C RID: 8236
	public struct RECT
	{
		// Token: 0x0400DC4D RID: 56397
		public int Left;

		// Token: 0x0400DC4E RID: 56398
		public int Top;

		// Token: 0x0400DC4F RID: 56399
		public int Right;

		// Token: 0x0400DC50 RID: 56400
		public int Bottom;
	}

	// Token: 0x0200202D RID: 8237
	[Flags]
	public enum KeyboardFlags
	{
		// Token: 0x0400DC52 RID: 56402
		Shown = 1,
		// Token: 0x0400DC53 RID: 56403
		NotShown = 2,
		// Token: 0x0400DC54 RID: 56404
		SuccessTabTip = 4,
		// Token: 0x0400DC55 RID: 56405
		SuccessOSK = 8,
		// Token: 0x0400DC56 RID: 56406
		ErrorTabTip = 16,
		// Token: 0x0400DC57 RID: 56407
		ErrorOSK = 32,
		// Token: 0x0400DC58 RID: 56408
		NotFoundTabTip = 64,
		// Token: 0x0400DC59 RID: 56409
		NotFoundOSK = 128
	}

	// Token: 0x0200202E RID: 8238
	public enum TouchState
	{
		// Token: 0x0400DC5B RID: 56411
		None,
		// Token: 0x0400DC5C RID: 56412
		InitialDown,
		// Token: 0x0400DC5D RID: 56413
		Down,
		// Token: 0x0400DC5E RID: 56414
		InitialUp
	}

	// Token: 0x0200202F RID: 8239
	public class IntelDevice
	{
		// Token: 0x06011C52 RID: 72786 RVA: 0x004F8068 File Offset: 0x004F6268
		public static string GetDeviceName(int deviceId)
		{
			string result;
			if (!W8Touch.IntelDevice.DeviceIdMap.TryGetValue(deviceId, out result))
			{
				return "";
			}
			return result;
		}

		// Token: 0x0400DC5F RID: 56415
		private static readonly Map<int, string> DeviceIdMap = new Map<int, string>
		{
			{
				30720,
				"Auburn"
			},
			{
				28961,
				"Whitney"
			},
			{
				28963,
				"Whitney"
			},
			{
				28965,
				"Whitney"
			},
			{
				4402,
				"Solono"
			},
			{
				9570,
				"Brookdale"
			},
			{
				13698,
				"Montara"
			},
			{
				9586,
				"Springdale"
			},
			{
				9602,
				"Grantsdale"
			},
			{
				10114,
				"Grantsdale"
			},
			{
				9618,
				"Alviso"
			},
			{
				10130,
				"Alviso"
			},
			{
				10098,
				"Lakeport-G"
			},
			{
				10102,
				"Lakeport-G"
			},
			{
				10146,
				"Calistoga"
			},
			{
				10150,
				"Calistoga"
			},
			{
				10626,
				"Broadwater-G"
			},
			{
				10627,
				"Broadwater-G"
			},
			{
				10610,
				"Broadwater-G"
			},
			{
				10611,
				"Broadwater-G"
			},
			{
				10642,
				"Broadwater-G"
			},
			{
				10643,
				"Broadwater-G"
			},
			{
				10658,
				"Broadwater-G"
			},
			{
				10659,
				"Broadwater-G"
			},
			{
				10754,
				"Crestline"
			},
			{
				10755,
				"Crestline"
			},
			{
				10770,
				"Crestline"
			},
			{
				10771,
				"Crestline"
			},
			{
				10674,
				"Bearlake"
			},
			{
				10675,
				"Bearlake"
			},
			{
				10690,
				"Bearlake"
			},
			{
				10691,
				"Bearlake"
			},
			{
				10706,
				"Bearlake"
			},
			{
				10707,
				"Bearlake"
			},
			{
				10818,
				"Cantiga"
			},
			{
				10819,
				"Cantiga"
			},
			{
				11778,
				"Eaglelake"
			},
			{
				11779,
				"Eaglelake"
			},
			{
				11810,
				"Eaglelake"
			},
			{
				11811,
				"Eaglelake"
			},
			{
				11794,
				"Eaglelake"
			},
			{
				11795,
				"Eaglelake"
			},
			{
				11826,
				"Eaglelake"
			},
			{
				11827,
				"Eaglelake"
			},
			{
				11842,
				"Eaglelake"
			},
			{
				11843,
				"Eaglelake"
			},
			{
				11922,
				"Eaglelake"
			},
			{
				11923,
				"Eaglelake"
			},
			{
				70,
				"Arrandale"
			},
			{
				66,
				"Clarkdale"
			},
			{
				262,
				"Mobile_SandyBridge_GT1"
			},
			{
				278,
				"Mobile_SandyBridge_GT2"
			},
			{
				294,
				"Mobile_SandyBridge_GT2+"
			},
			{
				258,
				"DT_SandyBridge_GT2+"
			},
			{
				274,
				"DT_SandyBridge_GT2+"
			},
			{
				290,
				"DT_SandyBridge_GT2+"
			},
			{
				266,
				"SandyBridge_Server"
			},
			{
				270,
				"SandyBridge_Reserved"
			},
			{
				338,
				"Desktop_IvyBridge_GT1"
			},
			{
				342,
				"Mobile_IvyBridge_GT1"
			},
			{
				346,
				"Server_IvyBridge_GT1"
			},
			{
				350,
				"Reserved_IvyBridge_GT1"
			},
			{
				354,
				"Desktop_IvyBridge_GT2"
			},
			{
				358,
				"Mobile_IvyBridge_GT2"
			},
			{
				362,
				"Server_IvyBridge_GT2"
			},
			{
				1026,
				"Desktop_Haswell_GT1_Y6W"
			},
			{
				1030,
				"Mobile_Haswell_GT1_Y6W"
			},
			{
				1034,
				"Server_Haswell_GT1"
			},
			{
				1042,
				"Desktop_Haswell_GT2_U15W"
			},
			{
				1046,
				"Mobile_Haswell_GT2_U15W"
			},
			{
				1051,
				"Workstation_Haswell_GT2"
			},
			{
				1050,
				"Server_Haswell_GT2"
			},
			{
				1054,
				"Reserved_Haswell_DT_GT1.5_U15W"
			},
			{
				2566,
				"Mobile_Haswell_ULT_GT1_Y6W"
			},
			{
				2574,
				"Mobile_Haswell_ULX_GT1_Y6W"
			},
			{
				2582,
				"Mobile_Haswell_ULT_GT2_U15W"
			},
			{
				2590,
				"Mobile_Haswell_ULX_GT2_Y6W"
			},
			{
				2598,
				"Mobile_Haswell_ULT_GT3_U28W"
			},
			{
				2606,
				"Mobile_Haswell_ULT_GT3@28_U28W"
			},
			{
				3346,
				"Desktop_Haswell_GT2F"
			},
			{
				3350,
				"Mobile_Haswell_GT2F"
			},
			{
				3362,
				"Desktop_Crystal-Well_GT3"
			},
			{
				3366,
				"Mobile_Crystal-Well_GT3"
			},
			{
				3370,
				"Server_Crystal-Well_GT3"
			},
			{
				3889,
				"BayTrail"
			},
			{
				33032,
				"Poulsbo"
			},
			{
				33033,
				"Poulsbo"
			},
			{
				2255,
				"CloverTrail"
			},
			{
				40961,
				"CloverTrail"
			},
			{
				40962,
				"CloverTrail"
			},
			{
				40977,
				"CloverTrail"
			},
			{
				40978,
				"CloverTrail"
			}
		};
	}

	// Token: 0x02002030 RID: 8240
	// (Invoke) Token: 0x06011C56 RID: 72790
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8ShowKeyboard();

	// Token: 0x02002031 RID: 8241
	// (Invoke) Token: 0x06011C5A RID: 72794
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8HideKeyboard();

	// Token: 0x02002032 RID: 8242
	// (Invoke) Token: 0x06011C5E RID: 72798
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8ShowOSK();

	// Token: 0x02002033 RID: 8243
	// (Invoke) Token: 0x06011C62 RID: 72802
	[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
	private delegate int DelW8Initialize(string windowName);

	// Token: 0x02002034 RID: 8244
	// (Invoke) Token: 0x06011C66 RID: 72806
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate void DelW8Shutdown();

	// Token: 0x02002035 RID: 8245
	// (Invoke) Token: 0x06011C6A RID: 72810
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8GetDeviceId();

	// Token: 0x02002036 RID: 8246
	// (Invoke) Token: 0x06011C6E RID: 72814
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate bool DelW8IsWindows8OrGreater();

	// Token: 0x02002037 RID: 8247
	// (Invoke) Token: 0x06011C72 RID: 72818
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate bool DelW8IsLastEventFromTouch();

	// Token: 0x02002038 RID: 8248
	// (Invoke) Token: 0x06011C76 RID: 72822
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8GetBatteryMode();

	// Token: 0x02002039 RID: 8249
	// (Invoke) Token: 0x06011C7A RID: 72826
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8GetPercentBatteryLife();

	// Token: 0x0200203A RID: 8250
	// (Invoke) Token: 0x06011C7E RID: 72830
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate void DelW8GetDesktopRect(out W8Touch.RECT desktopRect);

	// Token: 0x0200203B RID: 8251
	// (Invoke) Token: 0x06011C82 RID: 72834
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate bool DelW8IsVirtualKeyboardVisible();

	// Token: 0x0200203C RID: 8252
	// (Invoke) Token: 0x06011C86 RID: 72838
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8GetTouchPointCount();

	// Token: 0x0200203D RID: 8253
	// (Invoke) Token: 0x06011C8A RID: 72842
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate bool DelW8GetTouchPoint(int i, W8Touch.tTouchData n);

	// Token: 0x0200203E RID: 8254
	public enum FEEDBACK_TYPE
	{
		// Token: 0x0400DC61 RID: 56417
		FEEDBACK_TOUCH_CONTACTVISUALIZATION = 1,
		// Token: 0x0400DC62 RID: 56418
		FEEDBACK_PEN_BARRELVISUALIZATION,
		// Token: 0x0400DC63 RID: 56419
		FEEDBACK_PEN_TAP,
		// Token: 0x0400DC64 RID: 56420
		FEEDBACK_PEN_DOUBLETAP,
		// Token: 0x0400DC65 RID: 56421
		FEEDBACK_PEN_PRESSANDHOLD,
		// Token: 0x0400DC66 RID: 56422
		FEEDBACK_PEN_RIGHTTAP,
		// Token: 0x0400DC67 RID: 56423
		FEEDBACK_TOUCH_TAP,
		// Token: 0x0400DC68 RID: 56424
		FEEDBACK_TOUCH_DOUBLETAP,
		// Token: 0x0400DC69 RID: 56425
		FEEDBACK_TOUCH_PRESSANDHOLD,
		// Token: 0x0400DC6A RID: 56426
		FEEDBACK_TOUCH_RIGHTTAP,
		// Token: 0x0400DC6B RID: 56427
		FEEDBACK_GESTURE_PRESSANDTAP
	}

	// Token: 0x0200203F RID: 8255
	// (Invoke) Token: 0x06011C8E RID: 72846
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate bool DelSetWindowFeedbackSetting(IntPtr hwnd, W8Touch.FEEDBACK_TYPE feedback, uint dwFlags, uint size, IntPtr configuration);
}
