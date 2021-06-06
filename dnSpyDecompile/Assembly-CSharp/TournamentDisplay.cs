using System;
using System.Collections.Generic;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000741 RID: 1857
public class TournamentDisplay : MonoBehaviour
{
	// Token: 0x06006936 RID: 26934 RVA: 0x00224A91 File Offset: 0x00222C91
	private void Awake()
	{
		AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "DeckPickerTray_phone.prefab:a30124f640b5b92459bf820a4e3b1ca7" : "DeckPickerTray.prefab:3e13b59cdca14074bbce2b7d903ed895", new PrefabCallback<GameObject>(this.DeckPickerTrayLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		TournamentDisplay.s_instance = this;
	}

	// Token: 0x06006937 RID: 26935 RVA: 0x00224ACF File Offset: 0x00222CCF
	private void OnDestroy()
	{
		TournamentDisplay.s_instance = null;
		UserAttentionManager.StopBlocking(UserAttentionBlocker.SET_ROTATION_INTRO);
		this.UnregisterListeners();
	}

	// Token: 0x06006938 RID: 26936 RVA: 0x00224AE3 File Offset: 0x00222CE3
	private void Start()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_Tournament);
		NetCache.Get().RegisterScreenTourneys(new NetCache.NetCacheCallback(this.UpdateTourneyPage), new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x06006939 RID: 26937 RVA: 0x00224B14 File Offset: 0x00222D14
	private void Update()
	{
		if (this.m_allInitialized)
		{
			return;
		}
		if (this.m_netCacheReturned && this.m_deckPickerTrayLoaded)
		{
			Log.PlayModeInvestigation.PrintInfo(string.Format("TournamentDisplay.Update() called. m_allInitialized={0}, m_netCacheReturned={1}, m_deckPickerTrayLoaded={2}", this.m_allInitialized, this.m_netCacheReturned, this.m_deckPickerTrayLoaded), Array.Empty<object>());
			if (SetRotationManager.ShouldShowSetRotationIntro())
			{
				Log.PlayModeInvestigation.PrintInfo("TournamentDisplay.Update() -- ShouldShowSetRotationIntro() = true", Array.Empty<object>());
				this.m_deckPickerTrayGO.transform.localPosition = this.m_SetRotationOffscreenDuringTransition;
				this.SetupSetRotation();
				Log.PlayModeInvestigation.PrintInfo("TournamentDisplay.Update() -- SetupSetRotation() is complete", Array.Empty<object>());
			}
			this.m_deckPickerTray.InitAssets();
			this.m_allInitialized = true;
		}
	}

	// Token: 0x0600693A RID: 26938 RVA: 0x00224BD8 File Offset: 0x00222DD8
	public void UpdateHeaderText()
	{
		if (this.m_deckPickerTray == null)
		{
			return;
		}
		if (!Options.GetInRankedPlayMode())
		{
			string headerText = GameStrings.Get("GLUE_PLAY_CASUAL");
			this.m_deckPickerTray.SetHeaderText(headerText);
			return;
		}
		Map<FormatType, string> map = new Map<FormatType, string>();
		map.Add(FormatType.FT_WILD, "GLUE_PLAY_WILD");
		map.Add(FormatType.FT_STANDARD, "GLUE_PLAY_STANDARD");
		map.Add(FormatType.FT_CLASSIC, "GLUE_PLAY_CLASSIC");
		FormatType formatType = Options.GetFormatType();
		string key;
		string headerText2;
		if (!map.TryGetValue(formatType, out key))
		{
			Debug.LogError("TournamentDisplay.UpdateHeaderText called in unsupported format type: " + formatType.ToString());
			headerText2 = "UNSUPPORTED HEADER TEXT " + formatType.ToString();
		}
		else
		{
			headerText2 = GameStrings.Get(key);
		}
		this.m_deckPickerTray.SetHeaderText(headerText2);
	}

	// Token: 0x0600693B RID: 26939 RVA: 0x00224C98 File Offset: 0x00222E98
	private void DeckPickerTrayLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_deckPickerTrayGO = go;
		this.m_deckPickerTray = go.GetComponent<DeckPickerTrayDisplay>();
		this.m_deckPickerTray.transform.parent = base.transform;
		this.m_deckPickerTray.transform.localPosition = this.m_deckPickerPosition;
		this.m_deckPickerTrayLoaded = true;
		this.UpdateHeaderText();
	}

	// Token: 0x1700065F RID: 1631
	// (get) Token: 0x0600693C RID: 26940 RVA: 0x00224CF6 File Offset: 0x00222EF6
	// (set) Token: 0x0600693D RID: 26941 RVA: 0x00224CFE File Offset: 0x00222EFE
	public bool SlidingInForSetRotation { get; private set; }

	// Token: 0x0600693E RID: 26942 RVA: 0x00224D08 File Offset: 0x00222F08
	public void SetRotationSlideIn()
	{
		this.SlidingInForSetRotation = true;
		this.m_deckPickerTrayGO.transform.localPosition = this.m_SetRotationOffscreenPosition;
		iTween.MoveTo(this.m_deckPickerTrayGO, iTween.Hash(new object[]
		{
			"position",
			this.m_SetRotationOnscreenPosition,
			"delay",
			0f,
			"time",
			this.m_SetRotationSideInTime,
			"islocal",
			true,
			"easetype",
			iTween.EaseType.easeOutBounce,
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				this.SlidingInForSetRotation = false;
			})
		}));
	}

	// Token: 0x0600693F RID: 26943 RVA: 0x00224DC8 File Offset: 0x00222FC8
	private void UpdateTourneyPage()
	{
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.Tournament)
		{
			NetCache.NetCacheMedalInfo netObject = NetCache.Get().GetNetObject<NetCache.NetCacheMedalInfo>();
			bool flag = false;
			if (this.m_currentMedalInfo != null)
			{
				foreach (object obj in Enum.GetValues(typeof(FormatType)))
				{
					FormatType formatType = (FormatType)obj;
					if (formatType != FormatType.FT_UNKNOWN)
					{
						MedalInfoData medalInfoData = netObject.GetMedalInfoData(formatType);
						MedalInfoData medalInfoData2 = this.m_currentMedalInfo.GetMedalInfoData(formatType);
						if (medalInfoData == null || medalInfoData2 == null || medalInfoData.LeagueId != medalInfoData2.LeagueId || medalInfoData.StarLevel != medalInfoData2.StarLevel || medalInfoData.Stars != medalInfoData2.Stars || medalInfoData.StarsPerWin != medalInfoData2.StarsPerWin)
						{
							flag = true;
							break;
						}
					}
				}
			}
			this.m_currentMedalInfo = netObject;
			if (flag)
			{
				TournamentDisplay.DelMedalChanged[] array = this.m_medalChangedListeners.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					array[i](this.m_currentMedalInfo);
				}
			}
			this.m_netCacheReturned = true;
			return;
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			return;
		}
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		Error.AddWarningLoc("GLOBAL_FEATURE_DISABLED_TITLE", "GLOBAL_FEATURE_DISABLED_MESSAGE_PLAY", Array.Empty<object>());
	}

	// Token: 0x06006940 RID: 26944 RVA: 0x00224F2C File Offset: 0x0022312C
	private void UnregisterListeners()
	{
		if (NetCache.Get() != null)
		{
			NetCache.Get().UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.UpdateTourneyPage));
		}
	}

	// Token: 0x06006941 RID: 26945 RVA: 0x00224F4B File Offset: 0x0022314B
	public static TournamentDisplay Get()
	{
		return TournamentDisplay.s_instance;
	}

	// Token: 0x06006942 RID: 26946 RVA: 0x00224F52 File Offset: 0x00223152
	public void SceneUnload()
	{
		this.UnregisterListeners();
	}

	// Token: 0x06006943 RID: 26947 RVA: 0x00224F5A File Offset: 0x0022315A
	public NetCache.NetCacheMedalInfo GetCurrentMedalInfo()
	{
		return this.m_currentMedalInfo;
	}

	// Token: 0x06006944 RID: 26948 RVA: 0x00224F62 File Offset: 0x00223162
	public void RegisterMedalChangedListener(TournamentDisplay.DelMedalChanged listener)
	{
		if (this.m_medalChangedListeners.Contains(listener))
		{
			return;
		}
		this.m_medalChangedListeners.Add(listener);
	}

	// Token: 0x06006945 RID: 26949 RVA: 0x00224F7F File Offset: 0x0022317F
	public void RemoveMedalChangedListener(TournamentDisplay.DelMedalChanged listener)
	{
		this.m_medalChangedListeners.Remove(listener);
	}

	// Token: 0x06006946 RID: 26950 RVA: 0x00224F8E File Offset: 0x0022318E
	private void SetupSetRotation()
	{
		AssetLoader.Get().InstantiatePrefab("TheBox_TheClock.prefab:d922114c10efb5e4d8ab76d57913eff3", AssetLoadingOptions.None);
	}

	// Token: 0x04005612 RID: 22034
	public TextMesh m_modeName;

	// Token: 0x04005613 RID: 22035
	public Vector3_MobileOverride m_deckPickerPosition;

	// Token: 0x04005614 RID: 22036
	public Vector3 m_SetRotationOnscreenPosition = new Vector3(27.051f, 1.7f, -22.4f);

	// Token: 0x04005615 RID: 22037
	public Vector3 m_SetRotationOffscreenPosition = new Vector3(-60f, 1.7f, -22.4f);

	// Token: 0x04005616 RID: 22038
	public Vector3 m_SetRotationOffscreenDuringTransition = new Vector3(-260f, 1.7f, -22.4f);

	// Token: 0x04005617 RID: 22039
	public float m_SetRotationSideInTime = 1f;

	// Token: 0x04005618 RID: 22040
	private static TournamentDisplay s_instance;

	// Token: 0x04005619 RID: 22041
	private bool m_allInitialized;

	// Token: 0x0400561A RID: 22042
	private bool m_netCacheReturned;

	// Token: 0x0400561B RID: 22043
	private bool m_deckPickerTrayLoaded;

	// Token: 0x0400561C RID: 22044
	private DeckPickerTrayDisplay m_deckPickerTray;

	// Token: 0x0400561D RID: 22045
	private GameObject m_deckPickerTrayGO;

	// Token: 0x0400561E RID: 22046
	private NetCache.NetCacheMedalInfo m_currentMedalInfo;

	// Token: 0x0400561F RID: 22047
	private List<TournamentDisplay.DelMedalChanged> m_medalChangedListeners = new List<TournamentDisplay.DelMedalChanged>();

	// Token: 0x0200231A RID: 8986
	// (Invoke) Token: 0x060129D2 RID: 76242
	public delegate void DelMedalChanged(NetCache.NetCacheMedalInfo medalInfo);
}
