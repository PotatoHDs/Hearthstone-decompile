using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
	public BaseUIBones m_Bones;

	public BaseUIPrefabs m_Prefabs;

	public BnetBar m_BnetBar;

	public ExistingAccountPopup m_ExistingAccountPopup;

	private static BaseUI s_instance;

	public Camera m_BnetCamera { get; private set; }

	public Camera m_BnetDialogCamera { get; private set; }

	public static string SavedScreenshotPath { get; private set; }

	public static string ScreenshotPath
	{
		get
		{
			string text = null;
			if (PlatformSettings.IsMobileRuntimeOS)
			{
				text = $"{Application.persistentDataPath}/Screenshot.png";
				if (File.Exists(text))
				{
					File.Delete(text);
				}
			}
			else
			{
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				DateTime now = DateTime.Now;
				string text2 = Options.Get().GetString(Option.SCREENSHOT_DIRECTORY, folderPath);
				if (!Directory.Exists(text2))
				{
					text2 = folderPath;
				}
				text = $"{text2}/Hearthstone Screenshot {now:MM-dd-yy HH.mm.ss}.png";
				int num = 1;
				while (File.Exists(text))
				{
					text = $"{text2}/Hearthstone Screenshot {now:MM-dd-yy HH.mm.ss} {num++}.png";
				}
			}
			return text;
		}
	}

	public static string QRCodePath
	{
		get
		{
			if (!PlatformSettings.IsMobile())
			{
				return null;
			}
			return $"{Application.persistentDataPath}/QRCode.png";
		}
	}

	private void Awake()
	{
		s_instance = this;
		m_BnetCamera = CameraUtils.FindFirstByLayer(GameLayer.BattleNet);
		m_BnetDialogCamera = CameraUtils.FindFirstByLayer(GameLayer.BattleNetDialog);
		UnityEngine.Object.Instantiate(m_Prefabs.m_ChatMgrPrefab, base.transform.position, Quaternion.identity).transform.parent = base.transform;
		m_BnetCamera.GetComponent<ScreenResizeDetector>().AddSizeChangedListener(OnScreenSizeChanged);
		base.gameObject.AddComponent<HSDontDestroyOnLoad>();
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	private void Start()
	{
		UpdateLayout();
		InnKeepersSpecial.Init();
	}

	private void OnGUI()
	{
		if (Event.current.type == EventType.KeyUp)
		{
			KeyCode keyCode = Event.current.keyCode;
			if (keyCode == KeyCode.F13 || (uint)(keyCode - 316) <= 1u)
			{
				StartCoroutine(TakeScreenshot(4f));
			}
		}
	}

	public static BaseUI Get()
	{
		return s_instance;
	}

	public void OnLoggedIn()
	{
		m_BnetBar.OnLoggedIn();
	}

	public Camera GetBnetCamera()
	{
		return m_BnetCamera;
	}

	public Camera GetBnetDialogCamera()
	{
		return m_BnetDialogCamera;
	}

	public Transform GetAddFriendBone()
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				return m_Bones.m_AddFriendPhoneKeyboard;
			}
			return m_Bones.m_AddFriendVirtualKeyboard;
		}
		return m_Bones.m_AddFriend;
	}

	public Transform GetRecruitAFriendBone()
	{
		return m_Bones.m_RecruitAFriend;
	}

	public Transform GetChatBubbleBone()
	{
		return m_Bones.m_ChatBubble;
	}

	public Transform GetGameMenuBone(bool withRatings = false)
	{
		if (SceneMgr.Get().IsInGame())
		{
			return m_Bones.m_InGameMenu;
		}
		if (!withRatings)
		{
			return m_Bones.m_BoxMenu;
		}
		return m_Bones.m_BoxMenuWithRatings;
	}

	public Transform GetOptionsMenuBone()
	{
		return m_Bones.m_OptionsMenu;
	}

	public Transform GetQuickChatBone()
	{
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		if ((UniversalInputManager.Get().IsTouchMode() && touchScreenService.IsTouchSupported()) || touchScreenService.IsVirtualKeyboardVisible())
		{
			return m_Bones.m_QuickChatVirtualKeyboard;
		}
		return m_Bones.m_QuickChat;
	}

	public Transform GetFriendsListTutorialNotificationBone()
	{
		return m_Bones.m_FriendsListTutorialNotification;
	}

	public bool HandleKeyboardInput()
	{
		if (m_BnetBar != null && m_BnetBar.HandleKeyboardInput())
		{
			return true;
		}
		if ((InputCollection.GetKey(KeyCode.LeftControl) || InputCollection.GetKey(KeyCode.RightControl) || InputCollection.GetKey(KeyCode.LeftCommand) || InputCollection.GetKey(KeyCode.RightCommand)) && (InputCollection.GetKey(KeyCode.LeftShift) || InputCollection.GetKey(KeyCode.RightShift)) && InputCollection.GetKeyDown(KeyCode.S) && Options.Get() != null)
		{
			bool @bool = Options.Get().GetBool(Option.STREAMER_MODE);
			Options.Get().SetBool(Option.STREAMER_MODE, !@bool);
		}
		return false;
	}

	public static IEnumerator TakeScreenshot(float maxWaitSeconds)
	{
		SavedScreenshotPath = ScreenshotPath;
		string statusMessage = GameStrings.Get("GLOBAL_SCREENSHOT_COMPLETE");
		if (!PlatformSettings.IsMobileRuntimeOS)
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			if (!SavedScreenshotPath.StartsWith(folderPath))
			{
				statusMessage = GameStrings.Format("GLOBAL_SCREENSHOT_COMPLETE_SPECIFIC_DIRECTORY", Path.GetDirectoryName(SavedScreenshotPath));
			}
		}
		UIStatus.Get().HideIfScreenshotMessage();
		ScreenCapture.CaptureScreenshot(PlatformSettings.IsMobileRuntimeOS ? Path.GetFileName(SavedScreenshotPath) : SavedScreenshotPath);
		s_instance.StartCoroutine(NotifyOfScreenshotComplete(statusMessage));
		Log.All.Print($"screenshot saved to {SavedScreenshotPath}");
		yield return WaitUntilFileExists(SavedScreenshotPath, maxWaitSeconds);
	}

	private void OnScreenSizeChanged(object userData)
	{
		UpdateLayout();
	}

	private void UpdateLayout()
	{
		m_BnetBar.UpdateLayout();
		if (ChatMgr.Get() != null)
		{
			ChatMgr.Get().UpdateLayout();
		}
	}

	private static IEnumerator NotifyOfScreenshotComplete(string statusMessage)
	{
		yield return null;
		UIStatus.Get().AddInfo(statusMessage, UIStatus.StatusType.SCREENSHOT);
	}

	private static IEnumerator WaitUntilFileExists(string imageFileName, float maxWaitSeconds)
	{
		float num = 0.1f;
		float totalCycles = maxWaitSeconds / num;
		WaitForSeconds waitForSeconds = new WaitForSeconds(num);
		for (int i = 0; (float)i < totalCycles; i++)
		{
			if (File.Exists(imageFileName))
			{
				yield break;
			}
			yield return waitForSeconds;
		}
		Log.All.PrintWarning($"screenshot never arrived on fileSystem after {maxWaitSeconds}s");
	}
}
