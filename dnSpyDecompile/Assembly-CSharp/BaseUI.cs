using System;
using System.Collections;
using System.IO;
using UnityEngine;

// Token: 0x02000AEB RID: 2795
public class BaseUI : MonoBehaviour
{
	// Token: 0x17000879 RID: 2169
	// (get) Token: 0x06009494 RID: 38036 RVA: 0x003029D2 File Offset: 0x00300BD2
	// (set) Token: 0x06009495 RID: 38037 RVA: 0x003029DA File Offset: 0x00300BDA
	public Camera m_BnetCamera { get; private set; }

	// Token: 0x1700087A RID: 2170
	// (get) Token: 0x06009496 RID: 38038 RVA: 0x003029E3 File Offset: 0x00300BE3
	// (set) Token: 0x06009497 RID: 38039 RVA: 0x003029EB File Offset: 0x00300BEB
	public Camera m_BnetDialogCamera { get; private set; }

	// Token: 0x06009498 RID: 38040 RVA: 0x003029F4 File Offset: 0x00300BF4
	private void Awake()
	{
		BaseUI.s_instance = this;
		this.m_BnetCamera = CameraUtils.FindFirstByLayer(GameLayer.BattleNet);
		this.m_BnetDialogCamera = CameraUtils.FindFirstByLayer(GameLayer.BattleNetDialog);
		UnityEngine.Object.Instantiate<ChatMgr>(this.m_Prefabs.m_ChatMgrPrefab, base.transform.position, Quaternion.identity).transform.parent = base.transform;
		this.m_BnetCamera.GetComponent<ScreenResizeDetector>().AddSizeChangedListener(new ScreenResizeDetector.SizeChangedCallback(this.OnScreenSizeChanged));
		base.gameObject.AddComponent<HSDontDestroyOnLoad>();
	}

	// Token: 0x06009499 RID: 38041 RVA: 0x00302A7A File Offset: 0x00300C7A
	private void OnDestroy()
	{
		BaseUI.s_instance = null;
	}

	// Token: 0x0600949A RID: 38042 RVA: 0x00302A82 File Offset: 0x00300C82
	private void Start()
	{
		this.UpdateLayout();
		InnKeepersSpecial.Init();
	}

	// Token: 0x0600949B RID: 38043 RVA: 0x00302A90 File Offset: 0x00300C90
	private void OnGUI()
	{
		if (Event.current.type == EventType.KeyUp)
		{
			KeyCode keyCode = Event.current.keyCode;
			if (keyCode == KeyCode.F13 || keyCode - KeyCode.Print <= 1)
			{
				base.StartCoroutine(BaseUI.TakeScreenshot(4f));
			}
		}
	}

	// Token: 0x0600949C RID: 38044 RVA: 0x00302AD8 File Offset: 0x00300CD8
	public static BaseUI Get()
	{
		return BaseUI.s_instance;
	}

	// Token: 0x0600949D RID: 38045 RVA: 0x00302ADF File Offset: 0x00300CDF
	public void OnLoggedIn()
	{
		this.m_BnetBar.OnLoggedIn();
	}

	// Token: 0x0600949E RID: 38046 RVA: 0x00302AEC File Offset: 0x00300CEC
	public Camera GetBnetCamera()
	{
		return this.m_BnetCamera;
	}

	// Token: 0x0600949F RID: 38047 RVA: 0x00302AF4 File Offset: 0x00300CF4
	public Camera GetBnetDialogCamera()
	{
		return this.m_BnetDialogCamera;
	}

	// Token: 0x060094A0 RID: 38048 RVA: 0x00302AFC File Offset: 0x00300CFC
	public Transform GetAddFriendBone()
	{
		if (!UniversalInputManager.Get().IsTouchMode())
		{
			return this.m_Bones.m_AddFriend;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			return this.m_Bones.m_AddFriendPhoneKeyboard;
		}
		return this.m_Bones.m_AddFriendVirtualKeyboard;
	}

	// Token: 0x060094A1 RID: 38049 RVA: 0x00302B39 File Offset: 0x00300D39
	public Transform GetRecruitAFriendBone()
	{
		return this.m_Bones.m_RecruitAFriend;
	}

	// Token: 0x060094A2 RID: 38050 RVA: 0x00302B46 File Offset: 0x00300D46
	public Transform GetChatBubbleBone()
	{
		return this.m_Bones.m_ChatBubble;
	}

	// Token: 0x060094A3 RID: 38051 RVA: 0x00302B53 File Offset: 0x00300D53
	public Transform GetGameMenuBone(bool withRatings = false)
	{
		if (SceneMgr.Get().IsInGame())
		{
			return this.m_Bones.m_InGameMenu;
		}
		if (!withRatings)
		{
			return this.m_Bones.m_BoxMenu;
		}
		return this.m_Bones.m_BoxMenuWithRatings;
	}

	// Token: 0x060094A4 RID: 38052 RVA: 0x00302B87 File Offset: 0x00300D87
	public Transform GetOptionsMenuBone()
	{
		return this.m_Bones.m_OptionsMenu;
	}

	// Token: 0x060094A5 RID: 38053 RVA: 0x00302B94 File Offset: 0x00300D94
	public Transform GetQuickChatBone()
	{
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		if ((UniversalInputManager.Get().IsTouchMode() && touchScreenService.IsTouchSupported()) || touchScreenService.IsVirtualKeyboardVisible())
		{
			return this.m_Bones.m_QuickChatVirtualKeyboard;
		}
		return this.m_Bones.m_QuickChat;
	}

	// Token: 0x060094A6 RID: 38054 RVA: 0x00302BDA File Offset: 0x00300DDA
	public Transform GetFriendsListTutorialNotificationBone()
	{
		return this.m_Bones.m_FriendsListTutorialNotification;
	}

	// Token: 0x060094A7 RID: 38055 RVA: 0x00302BE8 File Offset: 0x00300DE8
	public bool HandleKeyboardInput()
	{
		if (this.m_BnetBar != null && this.m_BnetBar.HandleKeyboardInput())
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

	// Token: 0x1700087B RID: 2171
	// (get) Token: 0x060094A8 RID: 38056 RVA: 0x00302C88 File Offset: 0x00300E88
	// (set) Token: 0x060094A9 RID: 38057 RVA: 0x00302C8F File Offset: 0x00300E8F
	public static string SavedScreenshotPath { get; private set; }

	// Token: 0x1700087C RID: 2172
	// (get) Token: 0x060094AA RID: 38058 RVA: 0x00302C98 File Offset: 0x00300E98
	public static string ScreenshotPath
	{
		get
		{
			string text;
			if (PlatformSettings.IsMobileRuntimeOS)
			{
				text = string.Format("{0}/Screenshot.png", Application.persistentDataPath);
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
				text = string.Format("{0}/Hearthstone Screenshot {1:MM-dd-yy HH.mm.ss}.png", text2, now);
				int num = 1;
				while (File.Exists(text))
				{
					text = string.Format("{0}/Hearthstone Screenshot {1:MM-dd-yy HH.mm.ss} {2}.png", text2, now, num++);
				}
			}
			return text;
		}
	}

	// Token: 0x060094AB RID: 38059 RVA: 0x00302D31 File Offset: 0x00300F31
	public static IEnumerator TakeScreenshot(float maxWaitSeconds)
	{
		BaseUI.SavedScreenshotPath = BaseUI.ScreenshotPath;
		string statusMessage = GameStrings.Get("GLOBAL_SCREENSHOT_COMPLETE");
		if (!PlatformSettings.IsMobileRuntimeOS)
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			if (!BaseUI.SavedScreenshotPath.StartsWith(folderPath))
			{
				statusMessage = GameStrings.Format("GLOBAL_SCREENSHOT_COMPLETE_SPECIFIC_DIRECTORY", new object[]
				{
					Path.GetDirectoryName(BaseUI.SavedScreenshotPath)
				});
			}
		}
		UIStatus.Get().HideIfScreenshotMessage();
		ScreenCapture.CaptureScreenshot(PlatformSettings.IsMobileRuntimeOS ? Path.GetFileName(BaseUI.SavedScreenshotPath) : BaseUI.SavedScreenshotPath);
		BaseUI.s_instance.StartCoroutine(BaseUI.NotifyOfScreenshotComplete(statusMessage));
		Log.All.Print(string.Format("screenshot saved to {0}", BaseUI.SavedScreenshotPath), Array.Empty<object>());
		yield return BaseUI.WaitUntilFileExists(BaseUI.SavedScreenshotPath, maxWaitSeconds);
		yield break;
	}

	// Token: 0x1700087D RID: 2173
	// (get) Token: 0x060094AC RID: 38060 RVA: 0x00302D40 File Offset: 0x00300F40
	public static string QRCodePath
	{
		get
		{
			if (!PlatformSettings.IsMobile())
			{
				return null;
			}
			return string.Format("{0}/QRCode.png", Application.persistentDataPath);
		}
	}

	// Token: 0x060094AD RID: 38061 RVA: 0x00302D5A File Offset: 0x00300F5A
	private void OnScreenSizeChanged(object userData)
	{
		this.UpdateLayout();
	}

	// Token: 0x060094AE RID: 38062 RVA: 0x00302D62 File Offset: 0x00300F62
	private void UpdateLayout()
	{
		this.m_BnetBar.UpdateLayout();
		if (ChatMgr.Get() != null)
		{
			ChatMgr.Get().UpdateLayout();
		}
	}

	// Token: 0x060094AF RID: 38063 RVA: 0x00302D86 File Offset: 0x00300F86
	private static IEnumerator NotifyOfScreenshotComplete(string statusMessage)
	{
		yield return null;
		UIStatus.Get().AddInfo(statusMessage, UIStatus.StatusType.SCREENSHOT);
		yield break;
	}

	// Token: 0x060094B0 RID: 38064 RVA: 0x00302D95 File Offset: 0x00300F95
	private static IEnumerator WaitUntilFileExists(string imageFileName, float maxWaitSeconds)
	{
		float num = 0.1f;
		float totalCycles = maxWaitSeconds / num;
		WaitForSeconds waitForSeconds = new WaitForSeconds(num);
		int i = 0;
		while ((float)i < totalCycles)
		{
			if (File.Exists(imageFileName))
			{
				yield break;
			}
			yield return waitForSeconds;
			int num2 = i;
			i = num2 + 1;
		}
		Log.All.PrintWarning(string.Format("screenshot never arrived on fileSystem after {0}s", maxWaitSeconds), Array.Empty<object>());
		yield break;
	}

	// Token: 0x04007C9F RID: 31903
	public BaseUIBones m_Bones;

	// Token: 0x04007CA0 RID: 31904
	public BaseUIPrefabs m_Prefabs;

	// Token: 0x04007CA1 RID: 31905
	public BnetBar m_BnetBar;

	// Token: 0x04007CA2 RID: 31906
	public ExistingAccountPopup m_ExistingAccountPopup;

	// Token: 0x04007CA3 RID: 31907
	private static BaseUI s_instance;
}
