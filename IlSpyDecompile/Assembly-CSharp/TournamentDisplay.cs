using System;
using System.Collections.Generic;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class TournamentDisplay : MonoBehaviour
{
	public delegate void DelMedalChanged(NetCache.NetCacheMedalInfo medalInfo);

	public TextMesh m_modeName;

	public Vector3_MobileOverride m_deckPickerPosition;

	public Vector3 m_SetRotationOnscreenPosition = new Vector3(27.051f, 1.7f, -22.4f);

	public Vector3 m_SetRotationOffscreenPosition = new Vector3(-60f, 1.7f, -22.4f);

	public Vector3 m_SetRotationOffscreenDuringTransition = new Vector3(-260f, 1.7f, -22.4f);

	public float m_SetRotationSideInTime = 1f;

	private static TournamentDisplay s_instance;

	private bool m_allInitialized;

	private bool m_netCacheReturned;

	private bool m_deckPickerTrayLoaded;

	private DeckPickerTrayDisplay m_deckPickerTray;

	private GameObject m_deckPickerTrayGO;

	private NetCache.NetCacheMedalInfo m_currentMedalInfo;

	private List<DelMedalChanged> m_medalChangedListeners = new List<DelMedalChanged>();

	public bool SlidingInForSetRotation { get; private set; }

	private void Awake()
	{
		AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "DeckPickerTray_phone.prefab:a30124f640b5b92459bf820a4e3b1ca7" : "DeckPickerTray.prefab:3e13b59cdca14074bbce2b7d903ed895", DeckPickerTrayLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		s_instance = this;
	}

	private void OnDestroy()
	{
		s_instance = null;
		UserAttentionManager.StopBlocking(UserAttentionBlocker.SET_ROTATION_INTRO);
		UnregisterListeners();
	}

	private void Start()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_Tournament);
		NetCache.Get().RegisterScreenTourneys(UpdateTourneyPage, NetCache.DefaultErrorHandler);
	}

	private void Update()
	{
		if (!m_allInitialized && m_netCacheReturned && m_deckPickerTrayLoaded)
		{
			Log.PlayModeInvestigation.PrintInfo($"TournamentDisplay.Update() called. m_allInitialized={m_allInitialized}, m_netCacheReturned={m_netCacheReturned}, m_deckPickerTrayLoaded={m_deckPickerTrayLoaded}");
			if (SetRotationManager.ShouldShowSetRotationIntro())
			{
				Log.PlayModeInvestigation.PrintInfo("TournamentDisplay.Update() -- ShouldShowSetRotationIntro() = true");
				m_deckPickerTrayGO.transform.localPosition = m_SetRotationOffscreenDuringTransition;
				SetupSetRotation();
				Log.PlayModeInvestigation.PrintInfo("TournamentDisplay.Update() -- SetupSetRotation() is complete");
			}
			m_deckPickerTray.InitAssets();
			m_allInitialized = true;
		}
	}

	public void UpdateHeaderText()
	{
		if (m_deckPickerTray == null)
		{
			return;
		}
		if (!Options.GetInRankedPlayMode())
		{
			string headerText = GameStrings.Get("GLUE_PLAY_CASUAL");
			m_deckPickerTray.SetHeaderText(headerText);
			return;
		}
		Map<FormatType, string> obj = new Map<FormatType, string>
		{
			{
				FormatType.FT_WILD,
				"GLUE_PLAY_WILD"
			},
			{
				FormatType.FT_STANDARD,
				"GLUE_PLAY_STANDARD"
			},
			{
				FormatType.FT_CLASSIC,
				"GLUE_PLAY_CLASSIC"
			}
		};
		FormatType formatType = Options.GetFormatType();
		string headerText2;
		if (!obj.TryGetValue(formatType, out var value))
		{
			Debug.LogError("TournamentDisplay.UpdateHeaderText called in unsupported format type: " + formatType);
			headerText2 = "UNSUPPORTED HEADER TEXT " + formatType;
		}
		else
		{
			headerText2 = GameStrings.Get(value);
		}
		m_deckPickerTray.SetHeaderText(headerText2);
	}

	private void DeckPickerTrayLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_deckPickerTrayGO = go;
		m_deckPickerTray = go.GetComponent<DeckPickerTrayDisplay>();
		m_deckPickerTray.transform.parent = base.transform;
		m_deckPickerTray.transform.localPosition = m_deckPickerPosition;
		m_deckPickerTrayLoaded = true;
		UpdateHeaderText();
	}

	public void SetRotationSlideIn()
	{
		SlidingInForSetRotation = true;
		m_deckPickerTrayGO.transform.localPosition = m_SetRotationOffscreenPosition;
		iTween.MoveTo(m_deckPickerTrayGO, iTween.Hash("position", m_SetRotationOnscreenPosition, "delay", 0f, "time", m_SetRotationSideInTime, "islocal", true, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", (Action<object>)delegate
		{
			SlidingInForSetRotation = false;
		}));
	}

	private void UpdateTourneyPage()
	{
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.Tournament)
		{
			if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
				Error.AddWarningLoc("GLOBAL_FEATURE_DISABLED_TITLE", "GLOBAL_FEATURE_DISABLED_MESSAGE_PLAY");
			}
			return;
		}
		NetCache.NetCacheMedalInfo netObject = NetCache.Get().GetNetObject<NetCache.NetCacheMedalInfo>();
		bool flag = false;
		if (m_currentMedalInfo != null)
		{
			foreach (FormatType value in Enum.GetValues(typeof(FormatType)))
			{
				if (value != 0)
				{
					MedalInfoData medalInfoData = netObject.GetMedalInfoData(value);
					MedalInfoData medalInfoData2 = m_currentMedalInfo.GetMedalInfoData(value);
					if (medalInfoData == null || medalInfoData2 == null || medalInfoData.LeagueId != medalInfoData2.LeagueId || medalInfoData.StarLevel != medalInfoData2.StarLevel || medalInfoData.Stars != medalInfoData2.Stars || medalInfoData.StarsPerWin != medalInfoData2.StarsPerWin)
					{
						flag = true;
						break;
					}
				}
			}
		}
		m_currentMedalInfo = netObject;
		if (flag)
		{
			DelMedalChanged[] array = m_medalChangedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i](m_currentMedalInfo);
			}
		}
		m_netCacheReturned = true;
	}

	private void UnregisterListeners()
	{
		if (NetCache.Get() != null)
		{
			NetCache.Get().UnregisterNetCacheHandler(UpdateTourneyPage);
		}
	}

	public static TournamentDisplay Get()
	{
		return s_instance;
	}

	public void SceneUnload()
	{
		UnregisterListeners();
	}

	public NetCache.NetCacheMedalInfo GetCurrentMedalInfo()
	{
		return m_currentMedalInfo;
	}

	public void RegisterMedalChangedListener(DelMedalChanged listener)
	{
		if (!m_medalChangedListeners.Contains(listener))
		{
			m_medalChangedListeners.Add(listener);
		}
	}

	public void RemoveMedalChangedListener(DelMedalChanged listener)
	{
		m_medalChangedListeners.Remove(listener);
	}

	private void SetupSetRotation()
	{
		AssetLoader.Get().InstantiatePrefab("TheBox_TheClock.prefab:d922114c10efb5e4d8ab76d57913eff3");
	}
}
