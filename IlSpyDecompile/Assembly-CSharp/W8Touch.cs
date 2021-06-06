using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

public class W8Touch : ITouchScreenService, IService, IHasUpdate
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class tTouchData
	{
		public int m_x;

		public int m_y;

		public int m_ID;

		public int m_Time;
	}

	public struct RECT
	{
		public int Left;

		public int Top;

		public int Right;

		public int Bottom;
	}

	[Flags]
	public enum KeyboardFlags
	{
		Shown = 0x1,
		NotShown = 0x2,
		SuccessTabTip = 0x4,
		SuccessOSK = 0x8,
		ErrorTabTip = 0x10,
		ErrorOSK = 0x20,
		NotFoundTabTip = 0x40,
		NotFoundOSK = 0x80
	}

	public enum TouchState
	{
		None,
		InitialDown,
		Down,
		InitialUp
	}

	public class IntelDevice
	{
		private static readonly Map<int, string> DeviceIdMap = new Map<int, string>
		{
			{ 30720, "Auburn" },
			{ 28961, "Whitney" },
			{ 28963, "Whitney" },
			{ 28965, "Whitney" },
			{ 4402, "Solono" },
			{ 9570, "Brookdale" },
			{ 13698, "Montara" },
			{ 9586, "Springdale" },
			{ 9602, "Grantsdale" },
			{ 10114, "Grantsdale" },
			{ 9618, "Alviso" },
			{ 10130, "Alviso" },
			{ 10098, "Lakeport-G" },
			{ 10102, "Lakeport-G" },
			{ 10146, "Calistoga" },
			{ 10150, "Calistoga" },
			{ 10626, "Broadwater-G" },
			{ 10627, "Broadwater-G" },
			{ 10610, "Broadwater-G" },
			{ 10611, "Broadwater-G" },
			{ 10642, "Broadwater-G" },
			{ 10643, "Broadwater-G" },
			{ 10658, "Broadwater-G" },
			{ 10659, "Broadwater-G" },
			{ 10754, "Crestline" },
			{ 10755, "Crestline" },
			{ 10770, "Crestline" },
			{ 10771, "Crestline" },
			{ 10674, "Bearlake" },
			{ 10675, "Bearlake" },
			{ 10690, "Bearlake" },
			{ 10691, "Bearlake" },
			{ 10706, "Bearlake" },
			{ 10707, "Bearlake" },
			{ 10818, "Cantiga" },
			{ 10819, "Cantiga" },
			{ 11778, "Eaglelake" },
			{ 11779, "Eaglelake" },
			{ 11810, "Eaglelake" },
			{ 11811, "Eaglelake" },
			{ 11794, "Eaglelake" },
			{ 11795, "Eaglelake" },
			{ 11826, "Eaglelake" },
			{ 11827, "Eaglelake" },
			{ 11842, "Eaglelake" },
			{ 11843, "Eaglelake" },
			{ 11922, "Eaglelake" },
			{ 11923, "Eaglelake" },
			{ 70, "Arrandale" },
			{ 66, "Clarkdale" },
			{ 262, "Mobile_SandyBridge_GT1" },
			{ 278, "Mobile_SandyBridge_GT2" },
			{ 294, "Mobile_SandyBridge_GT2+" },
			{ 258, "DT_SandyBridge_GT2+" },
			{ 274, "DT_SandyBridge_GT2+" },
			{ 290, "DT_SandyBridge_GT2+" },
			{ 266, "SandyBridge_Server" },
			{ 270, "SandyBridge_Reserved" },
			{ 338, "Desktop_IvyBridge_GT1" },
			{ 342, "Mobile_IvyBridge_GT1" },
			{ 346, "Server_IvyBridge_GT1" },
			{ 350, "Reserved_IvyBridge_GT1" },
			{ 354, "Desktop_IvyBridge_GT2" },
			{ 358, "Mobile_IvyBridge_GT2" },
			{ 362, "Server_IvyBridge_GT2" },
			{ 1026, "Desktop_Haswell_GT1_Y6W" },
			{ 1030, "Mobile_Haswell_GT1_Y6W" },
			{ 1034, "Server_Haswell_GT1" },
			{ 1042, "Desktop_Haswell_GT2_U15W" },
			{ 1046, "Mobile_Haswell_GT2_U15W" },
			{ 1051, "Workstation_Haswell_GT2" },
			{ 1050, "Server_Haswell_GT2" },
			{ 1054, "Reserved_Haswell_DT_GT1.5_U15W" },
			{ 2566, "Mobile_Haswell_ULT_GT1_Y6W" },
			{ 2574, "Mobile_Haswell_ULX_GT1_Y6W" },
			{ 2582, "Mobile_Haswell_ULT_GT2_U15W" },
			{ 2590, "Mobile_Haswell_ULX_GT2_Y6W" },
			{ 2598, "Mobile_Haswell_ULT_GT3_U28W" },
			{ 2606, "Mobile_Haswell_ULT_GT3@28_U28W" },
			{ 3346, "Desktop_Haswell_GT2F" },
			{ 3350, "Mobile_Haswell_GT2F" },
			{ 3362, "Desktop_Crystal-Well_GT3" },
			{ 3366, "Mobile_Crystal-Well_GT3" },
			{ 3370, "Server_Crystal-Well_GT3" },
			{ 3889, "BayTrail" },
			{ 33032, "Poulsbo" },
			{ 33033, "Poulsbo" },
			{ 2255, "CloverTrail" },
			{ 40961, "CloverTrail" },
			{ 40962, "CloverTrail" },
			{ 40977, "CloverTrail" },
			{ 40978, "CloverTrail" }
		};

		public static string GetDeviceName(int deviceId)
		{
			if (!DeviceIdMap.TryGetValue(deviceId, out var value))
			{
				return "";
			}
			return value;
		}
	}

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8ShowKeyboard();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8HideKeyboard();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8ShowOSK();

	[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
	private delegate int DelW8Initialize(string windowName);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate void DelW8Shutdown();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8GetDeviceId();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate bool DelW8IsWindows8OrGreater();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate bool DelW8IsLastEventFromTouch();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8GetBatteryMode();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8GetPercentBatteryLife();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate void DelW8GetDesktopRect(out RECT desktopRect);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate bool DelW8IsVirtualKeyboardVisible();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate int DelW8GetTouchPointCount();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate bool DelW8GetTouchPoint(int i, tTouchData n);

	public enum FEEDBACK_TYPE
	{
		FEEDBACK_TOUCH_CONTACTVISUALIZATION = 1,
		FEEDBACK_PEN_BARRELVISUALIZATION,
		FEEDBACK_PEN_TAP,
		FEEDBACK_PEN_DOUBLETAP,
		FEEDBACK_PEN_PRESSANDHOLD,
		FEEDBACK_PEN_RIGHTTAP,
		FEEDBACK_TOUCH_TAP,
		FEEDBACK_TOUCH_DOUBLETAP,
		FEEDBACK_TOUCH_PRESSANDHOLD,
		FEEDBACK_TOUCH_RIGHTTAP,
		FEEDBACK_GESTURE_PRESSANDTAP
	}

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate bool DelSetWindowFeedbackSetting(IntPtr hwnd, FEEDBACK_TYPE feedback, uint dwFlags, uint size, IntPtr configuration);

	private bool m_initialized;

	public bool m_isWindows8OrGreater;

	private IntPtr m_DLL = IntPtr.Zero;

	private const int MaxTouches = 5;

	private const int MaxInitializationAttempts = 10;

	private int m_intializationAttemptCount;

	private TouchState[] m_touchState;

	private Vector3 m_touchPosition = new Vector3(-1f, -1f, 0f);

	private Vector2 m_touchDelta = new Vector2(0f, 0f);

	private RECT m_desktopRect;

	private bool m_isVirtualKeyboardVisible;

	private bool m_isVirtualKeyboardShowRequested;

	private bool m_isVirtualKeyboardHideRequested;

	private PowerSource m_lastPowerSourceState = PowerSource.Unintialized;

	private bool m_bWindowFeedbackSettingValue;

	private bool m_bIsWindowFeedbackDisabled;

	private static DelW8ShowKeyboard DLL_W8ShowKeyboard;

	private static DelW8HideKeyboard DLL_W8HideKeyboard;

	private static DelW8ShowOSK DLL_W8ShowOSK;

	private static DelW8Initialize DLL_W8Initialize;

	private static DelW8Shutdown DLL_W8Shutdown;

	private static DelW8GetDeviceId DLL_W8GetDeviceId;

	private static DelW8IsWindows8OrGreater DLL_W8IsWindows8OrGreater;

	private static DelW8IsLastEventFromTouch DLL_W8IsLastEventFromTouch;

	private static DelW8GetBatteryMode DLL_W8GetBatteryMode;

	private static DelW8GetPercentBatteryLife DLL_W8GetPercentBatteryLife;

	private static DelW8GetDesktopRect DLL_W8GetDesktopRect;

	private static DelW8IsVirtualKeyboardVisible DLL_W8IsVirtualKeyboardVisible;

	private static DelW8GetTouchPointCount DLL_W8GetTouchPointCount;

	private static DelW8GetTouchPoint DLL_W8GetTouchPoint;

	private event Action VirtualKeyboardDidShow;

	private event Action VirtualKeyboardDidHide;

	[DllImport("User32.dll")]
	public static extern IntPtr FindWindow(string className, string windowName);

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		if (LoadW8TouchDLL())
		{
			m_isWindows8OrGreater = DLL_W8IsWindows8OrGreater();
		}
		m_touchState = new TouchState[5];
		for (int i = 0; i < 5; i++)
		{
			m_touchState[i] = TouchState.None;
		}
		Processor.RegisterOnGUIDelegate(OnGUI);
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().OnShutdown += OnApplicationQuit;
		}
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(UniversalInputManager) };
	}

	public void Shutdown()
	{
		HearthstoneApplication.Get().OnShutdown -= OnApplicationQuit;
	}

	public void Update()
	{
		if (!IsInitialized())
		{
			return;
		}
		DLL_W8GetDesktopRect(out m_desktopRect);
		bool flag = DLL_W8IsVirtualKeyboardVisible();
		if (flag != m_isVirtualKeyboardVisible)
		{
			m_isVirtualKeyboardVisible = flag;
			if (flag && this.VirtualKeyboardDidShow != null)
			{
				this.VirtualKeyboardDidShow();
			}
			else if (!flag && this.VirtualKeyboardDidHide != null)
			{
				this.VirtualKeyboardDidHide();
			}
		}
		if (m_isVirtualKeyboardVisible)
		{
			m_isVirtualKeyboardShowRequested = false;
		}
		else
		{
			m_isVirtualKeyboardHideRequested = false;
		}
		PowerSource batteryMode = GetBatteryMode();
		if (batteryMode != m_lastPowerSourceState && HearthstoneServices.TryGet<GraphicsManager>(out var service))
		{
			Log.W8Touch.Print("PowerSource Change Detected: {0}", batteryMode);
			m_lastPowerSourceState = batteryMode;
			service.RenderQualityLevel = (GraphicsQuality)Options.Get().GetInt(Option.GFX_QUALITY);
		}
		if ((!DLL_W8IsLastEventFromTouch() && UniversalInputManager.Get().UseWindowsTouch()) || (DLL_W8IsLastEventFromTouch() && !UniversalInputManager.Get().UseWindowsTouch()))
		{
			ToggleTouchMode();
		}
		if (m_touchState == null)
		{
			return;
		}
		int num = DLL_W8GetTouchPointCount();
		for (int i = 0; i < 5; i++)
		{
			tTouchData tTouchData = new tTouchData();
			bool flag2 = false;
			if (i < num)
			{
				flag2 = DLL_W8GetTouchPoint(i, tTouchData);
			}
			if (flag2 && i == 0)
			{
				Vector2 vector = TransformTouchPosition(new Vector2(tTouchData.m_x, tTouchData.m_y));
				if (m_touchPosition.x != -1f && m_touchPosition.y != -1f && m_touchState[i] == TouchState.Down)
				{
					m_touchDelta.x = vector.x - m_touchPosition.x;
					m_touchDelta.y = vector.y - m_touchPosition.y;
				}
				else
				{
					m_touchDelta.x = (m_touchDelta.y = 0f);
				}
				m_touchPosition.x = vector.x;
				m_touchPosition.y = vector.y;
			}
			if (flag2 && tTouchData.m_ID != -1)
			{
				if (m_touchState[i] == TouchState.Down || m_touchState[i] == TouchState.InitialDown)
				{
					m_touchState[i] = TouchState.Down;
				}
				else
				{
					m_touchState[i] = TouchState.InitialDown;
				}
			}
			else if (m_touchState[i] == TouchState.Down || m_touchState[i] == TouchState.InitialDown)
			{
				m_touchState[i] = TouchState.InitialUp;
			}
			else
			{
				m_touchState[i] = TouchState.None;
			}
		}
	}

	private void OnGUI()
	{
		if ((m_isWindows8OrGreater || !(m_DLL == IntPtr.Zero)) && !m_initialized)
		{
			InitializeDLL();
		}
	}

	private Vector2 TransformTouchPosition(Vector2 touchInput)
	{
		Vector2 result = default(Vector2);
		if (Screen.fullScreen)
		{
			float num = (float)Screen.width / (float)Screen.height;
			float num2 = (float)m_desktopRect.Right / (float)m_desktopRect.Bottom;
			if (Mathf.Abs(num - num2) < Mathf.Epsilon)
			{
				float num3 = (float)Screen.width / (float)m_desktopRect.Right;
				float num4 = (float)Screen.height / (float)m_desktopRect.Bottom;
				result.x = touchInput.x * num3;
				result.y = ((float)m_desktopRect.Bottom - touchInput.y) * num4;
			}
			else if (num < num2)
			{
				float num5 = m_desktopRect.Bottom;
				float num6 = num5 * num;
				float num7 = (float)Screen.height / num5;
				float num8 = (float)Screen.width / num6;
				float num9 = ((float)m_desktopRect.Right - num6) / 2f;
				result.x = (touchInput.x - num9) * num8;
				result.y = ((float)m_desktopRect.Bottom - touchInput.y) * num7;
			}
			else
			{
				float num10 = m_desktopRect.Right;
				float num11 = num10 / num;
				float num12 = (float)Screen.height / num11;
				float num13 = (float)Screen.width / num10;
				float num14 = ((float)m_desktopRect.Bottom - num11) / 2f;
				result.x = touchInput.x * num13;
				result.y = ((float)m_desktopRect.Bottom - touchInput.y - num14) * num12;
			}
		}
		else
		{
			result.x = touchInput.x;
			result.y = (float)Screen.height - touchInput.y;
		}
		return result;
	}

	private void ToggleTouchMode()
	{
		if (IsInitialized())
		{
			bool @bool = Options.Get().GetBool(Option.TOUCH_MODE);
			Options.Get().SetBool(Option.TOUCH_MODE, !@bool);
		}
	}

	public void ShowKeyboard()
	{
		if (IsInitialized() && !m_isVirtualKeyboardShowRequested && (!m_isVirtualKeyboardVisible || m_isVirtualKeyboardHideRequested))
		{
			if (m_isVirtualKeyboardHideRequested)
			{
				m_isVirtualKeyboardHideRequested = false;
			}
			KeyboardFlags keyboardFlags = (KeyboardFlags)DLL_W8ShowKeyboard();
			_ = keyboardFlags & KeyboardFlags.Shown;
			_ = 1;
			if ((keyboardFlags & KeyboardFlags.Shown) == KeyboardFlags.Shown && (keyboardFlags & KeyboardFlags.SuccessTabTip) == KeyboardFlags.SuccessTabTip)
			{
				m_isVirtualKeyboardShowRequested = true;
			}
		}
	}

	public void HideKeyboard()
	{
		if (IsInitialized() || m_isVirtualKeyboardVisible)
		{
			if (m_isVirtualKeyboardShowRequested)
			{
				m_isVirtualKeyboardShowRequested = false;
			}
			if (DLL_W8HideKeyboard() == 0)
			{
				m_isVirtualKeyboardHideRequested = true;
			}
		}
	}

	public void ShowOSK()
	{
		if (IsInitialized())
		{
			_ = DLL_W8ShowOSK() & 1;
			_ = 1;
		}
	}

	public string GetIntelDeviceName()
	{
		if (!IsInitialized())
		{
			return null;
		}
		return IntelDevice.GetDeviceName(DLL_W8GetDeviceId());
	}

	public PowerSource GetBatteryMode()
	{
		if (!IsInitialized())
		{
			return PowerSource.Unintialized;
		}
		return (PowerSource)DLL_W8GetBatteryMode();
	}

	public int GetPercentBatteryLife()
	{
		if (!IsInitialized())
		{
			return -1;
		}
		return DLL_W8GetPercentBatteryLife();
	}

	public bool IsVirtualKeyboardVisible()
	{
		if (!IsInitialized())
		{
			return false;
		}
		return m_isVirtualKeyboardVisible;
	}

	public bool GetTouch(int touchCount)
	{
		if (!IsInitialized() || m_touchState == null || touchCount >= 5)
		{
			return false;
		}
		if (m_touchState[touchCount] == TouchState.InitialDown || m_touchState[touchCount] == TouchState.Down)
		{
			return true;
		}
		return false;
	}

	public bool GetTouchDown(int touchCount)
	{
		if (!IsInitialized() || m_touchState == null || touchCount >= 5)
		{
			return false;
		}
		if (m_touchState[touchCount] == TouchState.InitialDown)
		{
			return true;
		}
		return false;
	}

	public bool GetTouchUp(int touchCount)
	{
		if (!IsInitialized() || m_touchState == null || touchCount >= 5)
		{
			return false;
		}
		if (m_touchState[touchCount] == TouchState.InitialUp)
		{
			return true;
		}
		return false;
	}

	public Vector3 GetTouchPosition()
	{
		if (!IsInitialized() || m_touchState == null)
		{
			return new Vector3(0f, 0f, 0f);
		}
		return new Vector3(m_touchPosition.x, m_touchPosition.y, m_touchPosition.z);
	}

	public Vector2 GetTouchDelta()
	{
		if (!IsInitialized() || m_touchState == null)
		{
			return new Vector2(0f, 0f);
		}
		return new Vector2(m_touchDelta.x, m_touchDelta.y);
	}

	public Vector3 GetTouchPositionForGUI()
	{
		if (!IsInitialized() || m_touchState == null)
		{
			return new Vector3(0f, 0f, 0f);
		}
		Vector2 vector = TransformTouchPosition(m_touchPosition);
		return new Vector3(vector.x, vector.y, m_touchPosition.z);
	}

	public bool IsTouchSupported()
	{
		return m_isWindows8OrGreater;
	}

	public void AddOnVirtualKeyboardShowListener(Action listener)
	{
		VirtualKeyboardDidShow -= listener;
		VirtualKeyboardDidShow += listener;
	}

	public void RemoveOnVirtualKeyboardShowListener(Action listener)
	{
		VirtualKeyboardDidShow -= listener;
	}

	public void AddOnVirtualKeyboardHideListener(Action listener)
	{
		VirtualKeyboardDidHide -= listener;
		VirtualKeyboardDidHide += listener;
	}

	public void RemoveOnVirtualKeyboardHideListener(Action listener)
	{
		VirtualKeyboardDidHide -= listener;
	}

	private IntPtr GetFunction(string name)
	{
		IntPtr procAddress = DLLUtils.GetProcAddress(m_DLL, name);
		if (procAddress == IntPtr.Zero)
		{
			Debug.LogError("Could not load W8TouchDLL." + name + "()");
			OnApplicationQuit();
		}
		return procAddress;
	}

	private bool LoadW8TouchDLL()
	{
		if (Environment.OSVersion.Version.Major < 6 || (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor < 2))
		{
			Log.W8Touch.Print("Windows Version is Pre-Windows 8");
			return false;
		}
		if (m_DLL == IntPtr.Zero)
		{
			m_DLL = FileUtils.LoadPlugin("W8TouchDLL", handleError: false);
			if (m_DLL == IntPtr.Zero)
			{
				Log.W8Touch.Print("Could not load W8TouchDLL.dll");
				return false;
			}
		}
		DLL_W8ShowKeyboard = (DelW8ShowKeyboard)Marshal.GetDelegateForFunctionPointer(GetFunction("W8_ShowKeyboard"), typeof(DelW8ShowKeyboard));
		DLL_W8HideKeyboard = (DelW8HideKeyboard)Marshal.GetDelegateForFunctionPointer(GetFunction("W8_HideKeyboard"), typeof(DelW8HideKeyboard));
		DLL_W8ShowOSK = (DelW8ShowOSK)Marshal.GetDelegateForFunctionPointer(GetFunction("W8_ShowOSK"), typeof(DelW8ShowOSK));
		DLL_W8Initialize = (DelW8Initialize)Marshal.GetDelegateForFunctionPointer(GetFunction("W8_Initialize"), typeof(DelW8Initialize));
		DLL_W8Shutdown = (DelW8Shutdown)Marshal.GetDelegateForFunctionPointer(GetFunction("W8_Shutdown"), typeof(DelW8Shutdown));
		DLL_W8GetDeviceId = (DelW8GetDeviceId)Marshal.GetDelegateForFunctionPointer(GetFunction("W8_GetDeviceId"), typeof(DelW8GetDeviceId));
		DLL_W8IsWindows8OrGreater = (DelW8IsWindows8OrGreater)Marshal.GetDelegateForFunctionPointer(GetFunction("W8_IsWindows8OrGreater"), typeof(DelW8IsWindows8OrGreater));
		DLL_W8IsLastEventFromTouch = (DelW8IsLastEventFromTouch)Marshal.GetDelegateForFunctionPointer(GetFunction("W8_IsLastEventFromTouch"), typeof(DelW8IsLastEventFromTouch));
		DLL_W8GetBatteryMode = (DelW8GetBatteryMode)Marshal.GetDelegateForFunctionPointer(GetFunction("W8_GetBatteryMode"), typeof(DelW8GetBatteryMode));
		DLL_W8GetPercentBatteryLife = (DelW8GetPercentBatteryLife)Marshal.GetDelegateForFunctionPointer(GetFunction("W8_GetPercentBatteryLife"), typeof(DelW8GetPercentBatteryLife));
		DLL_W8GetDesktopRect = (DelW8GetDesktopRect)Marshal.GetDelegateForFunctionPointer(GetFunction("W8_GetDesktopRect"), typeof(DelW8GetDesktopRect));
		DLL_W8IsVirtualKeyboardVisible = (DelW8IsVirtualKeyboardVisible)Marshal.GetDelegateForFunctionPointer(GetFunction("W8_IsVirtualKeyboardVisible"), typeof(DelW8IsVirtualKeyboardVisible));
		DLL_W8GetTouchPointCount = (DelW8GetTouchPointCount)Marshal.GetDelegateForFunctionPointer(GetFunction("GetTouchPointCount"), typeof(DelW8GetTouchPointCount));
		DLL_W8GetTouchPoint = (DelW8GetTouchPoint)Marshal.GetDelegateForFunctionPointer(GetFunction("GetTouchPoint"), typeof(DelW8GetTouchPoint));
		return true;
	}

	private void OnApplicationQuit()
	{
		Log.W8Touch.Print("W8Touch.AppQuit()");
		if (!(m_DLL == IntPtr.Zero))
		{
			ResetWindowFeedbackSetting();
			if (DLL_W8Shutdown != null && m_initialized)
			{
				DLL_W8Shutdown();
				m_initialized = false;
			}
			if (!DLLUtils.FreeLibrary(m_DLL))
			{
				Debug.Log("Error unloading W8TouchDLL.dll");
			}
			m_DLL = IntPtr.Zero;
		}
	}

	private bool IsInitialized()
	{
		if (m_DLL != IntPtr.Zero && m_isWindows8OrGreater)
		{
			return m_initialized;
		}
		return false;
	}

	private void InitializeDLL()
	{
		if (m_intializationAttemptCount >= 10)
		{
			return;
		}
		string windowName = GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE");
		if (DLL_W8Initialize(windowName) < 0)
		{
			m_intializationAttemptCount++;
			return;
		}
		Log.W8Touch.Print("W8Touch Start Success!");
		m_initialized = true;
		IntPtr intPtr = DLLUtils.LoadLibrary("User32.DLL");
		if (intPtr == IntPtr.Zero)
		{
			Log.W8Touch.Print("Could not load User32.DLL");
			return;
		}
		IntPtr procAddress = DLLUtils.GetProcAddress(intPtr, "SetWindowFeedbackSetting");
		if (procAddress == IntPtr.Zero)
		{
			Log.W8Touch.Print("Could not load User32.SetWindowFeedbackSetting()");
		}
		else
		{
			IntPtr intPtr2 = FindWindow(null, "Hearthstone");
			if (intPtr2 == IntPtr.Zero)
			{
				intPtr2 = FindWindow(null, GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE"));
			}
			if (intPtr2 == IntPtr.Zero)
			{
				Log.W8Touch.Print("Unable to retrieve Hearthstone window handle!");
			}
			else
			{
				DelSetWindowFeedbackSetting obj = (DelSetWindowFeedbackSetting)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DelSetWindowFeedbackSetting));
				int num = Marshal.SizeOf(typeof(int));
				IntPtr intPtr3 = Marshal.AllocHGlobal(num);
				Marshal.WriteInt32(intPtr3, 0, m_bWindowFeedbackSettingValue ? 1 : 0);
				bool bIsWindowFeedbackDisabled = true;
				if (!obj(intPtr2, FEEDBACK_TYPE.FEEDBACK_TOUCH_CONTACTVISUALIZATION, 0u, Convert.ToUInt32(num), intPtr3))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_CONTACTVISUALIZATION failed!");
					bIsWindowFeedbackDisabled = false;
				}
				if (!obj(intPtr2, FEEDBACK_TYPE.FEEDBACK_TOUCH_TAP, 0u, Convert.ToUInt32(num), intPtr3))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_TAP failed!");
					bIsWindowFeedbackDisabled = false;
				}
				if (!obj(intPtr2, FEEDBACK_TYPE.FEEDBACK_TOUCH_PRESSANDHOLD, 0u, Convert.ToUInt32(num), intPtr3))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_PRESSANDHOLD failed!");
					bIsWindowFeedbackDisabled = false;
				}
				if (!obj(intPtr2, FEEDBACK_TYPE.FEEDBACK_TOUCH_DOUBLETAP, 0u, Convert.ToUInt32(num), intPtr3))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_DOUBLETAP failed!");
					bIsWindowFeedbackDisabled = false;
				}
				if (!obj(intPtr2, FEEDBACK_TYPE.FEEDBACK_TOUCH_RIGHTTAP, 0u, Convert.ToUInt32(num), intPtr3))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_RIGHTTAP failed!");
					bIsWindowFeedbackDisabled = false;
				}
				if (!obj(intPtr2, FEEDBACK_TYPE.FEEDBACK_GESTURE_PRESSANDTAP, 0u, Convert.ToUInt32(num), intPtr3))
				{
					Log.W8Touch.Print("FEEDBACK_GESTURE_PRESSANDTAP failed!");
					bIsWindowFeedbackDisabled = false;
				}
				m_bIsWindowFeedbackDisabled = bIsWindowFeedbackDisabled;
				if (m_bIsWindowFeedbackDisabled)
				{
					Log.W8Touch.Print("Windows 8 Feedback Touch Gestures Disabled!");
				}
				Marshal.FreeHGlobal(intPtr3);
			}
		}
		if (!DLLUtils.FreeLibrary(intPtr))
		{
			Log.W8Touch.Print("Error unloading User32.dll");
		}
	}

	private void ResetWindowFeedbackSetting()
	{
		if (!m_initialized || !m_bIsWindowFeedbackDisabled)
		{
			return;
		}
		IntPtr intPtr = DLLUtils.LoadLibrary("User32.DLL");
		if (intPtr == IntPtr.Zero)
		{
			Log.W8Touch.Print("Could not load User32.DLL");
			return;
		}
		IntPtr procAddress = DLLUtils.GetProcAddress(intPtr, "SetWindowFeedbackSetting");
		if (procAddress == IntPtr.Zero)
		{
			Log.W8Touch.Print("Could not load User32.SetWindowFeedbackSetting()");
		}
		else
		{
			IntPtr intPtr2 = FindWindow(null, "Hearthstone");
			if (intPtr2 == IntPtr.Zero)
			{
				intPtr2 = FindWindow(null, GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE"));
			}
			if (intPtr2 == IntPtr.Zero)
			{
				Log.W8Touch.Print("Unable to retrieve Hearthstone window handle!");
			}
			else
			{
				DelSetWindowFeedbackSetting obj = (DelSetWindowFeedbackSetting)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DelSetWindowFeedbackSetting));
				IntPtr intPtr3 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.WriteInt32(intPtr3, 0, m_bWindowFeedbackSettingValue ? 1 : 0);
				bool flag = true;
				if (!obj(intPtr2, FEEDBACK_TYPE.FEEDBACK_TOUCH_CONTACTVISUALIZATION, 0u, 0u, IntPtr.Zero))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_CONTACTVISUALIZATION failed!");
					flag = false;
				}
				if (!obj(intPtr2, FEEDBACK_TYPE.FEEDBACK_TOUCH_TAP, 0u, 0u, IntPtr.Zero))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_TAP failed!");
					flag = false;
				}
				if (!obj(intPtr2, FEEDBACK_TYPE.FEEDBACK_TOUCH_PRESSANDHOLD, 0u, 0u, IntPtr.Zero))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_PRESSANDHOLD failed!");
					flag = false;
				}
				if (!obj(intPtr2, FEEDBACK_TYPE.FEEDBACK_TOUCH_DOUBLETAP, 0u, 0u, IntPtr.Zero))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_DOUBLETAP failed!");
					flag = false;
				}
				if (!obj(intPtr2, FEEDBACK_TYPE.FEEDBACK_TOUCH_RIGHTTAP, 0u, 0u, IntPtr.Zero))
				{
					Log.W8Touch.Print("FEEDBACK_TOUCH_RIGHTTAP failed!");
					flag = false;
				}
				if (!obj(intPtr2, FEEDBACK_TYPE.FEEDBACK_GESTURE_PRESSANDTAP, 0u, 0u, IntPtr.Zero))
				{
					Log.W8Touch.Print("FEEDBACK_GESTURE_PRESSANDTAP failed!");
					flag = false;
				}
				m_bIsWindowFeedbackDisabled = !flag;
				if (!m_bIsWindowFeedbackDisabled)
				{
					Log.W8Touch.Print("Windows 8 Feedback Touch Gestures Reset!");
				}
				Marshal.FreeHGlobal(intPtr3);
			}
		}
		if (!DLLUtils.FreeLibrary(intPtr))
		{
			Log.W8Touch.Print("Error unloading User32.dll");
		}
	}
}
