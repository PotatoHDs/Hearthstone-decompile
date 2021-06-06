using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using bgs;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using PegasusClient;
using PegasusFSG;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public class FiresideGatheringManager : IService, IHasUpdate, IHasFixedUpdate
{
	public enum FiresideGatheringMode
	{
		NONE,
		MAIN_SCREEN,
		FRIENDLY_CHALLENGE,
		FRIENDLY_CHALLENGE_BRAWL,
		FIRESIDE_BRAWL
	}

	public delegate void CheckedInToFSGCallback(FSGConfig gathering);

	public delegate void CheckedOutOfFSGCallback(FSGConfig gathering);

	public delegate void RequestNearbyFSGsCallback();

	public delegate void NearbyFSGsChangedCallback();

	public delegate void OnCloseSign();

	public delegate void OnInnkeeperSetupFinishedCallback(bool success);

	public delegate void FSGSignClosedCallback();

	public delegate void FSGSignShownCallback();

	public delegate void OnPatronListUpdatedCallback(List<BnetPlayer> addedToDisplayablePatronList, List<BnetPlayer> removedFromDisplayablePatronList);

	public const long INVALID_FSG_ID = 0L;

	private FSGConfig m_currentFSG;

	private byte[] m_currentFSGSharedSecretKey;

	private HashSet<int> m_innkeeperSelectedBrawlLibraryItemIds = new HashSet<int>();

	private List<FSGConfig> m_nearbyFSGs = new List<FSGConfig>();

	private HashSet<BnetPlayer> m_knownPatronsFromServer = new HashSet<BnetPlayer>();

	private HashSet<BnetPlayer> m_knownPatronsFromPresence = new HashSet<BnetPlayer>();

	private HashSet<BnetPlayer> m_displayablePatrons = new HashSet<BnetPlayer>();

	private HashSet<BnetPlayer> m_pendingPatrons = new HashSet<BnetPlayer>();

	private bool m_isAppendingPatronList;

	private Map<long, BnetPlayer> m_innkeepers = new Map<long, BnetPlayer>();

	private bool m_checkInRequestPending;

	private bool m_checkInDialogShown;

	private bool m_nearbyFSGsFoundEventSent;

	private Notification m_tooltipShowing;

	private ClientLocationData m_locationData;

	private Map<string, AccessPointInfo> m_accumulatedAccessPoints = new Map<string, AccessPointInfo>();

	private bool m_hasBegunLocationDataGatheringForLogin;

	private FiresideGatheringSign m_currentSign;

	private Transform m_smallSignContainer;

	private OnCloseSign m_currentSignCallback;

	private bool m_haltFSGNotificationsAndCheckins;

	private bool m_fsgSignShown;

	private bool m_doAutoInnkeeperSetup = true;

	private bool m_haltAutoCheckinWhileInnkeeperSetup;

	private bool m_errorOccuredOnCheckin;

	private bool m_waitingForCheckIn;

	private FSGConfig m_innkeeperFSG;

	private bool m_fsgAvailableToCheckin;

	private bool m_isRequestNearbyFSGsPending;

	private double m_gpsCheatOffset;

	private static bool s_cacheFSGEnabled = false;

	private static bool s_cacheGPSEnabled = false;

	private static bool s_cacheWifiEnabled = false;

	private bool m_gpsCheatingLocation;

	private double m_gpsCheatLatitude;

	private double m_gpsCheatLongitude;

	private FSGConfig m_cachedFakeCheatFsg;

	private const string BACKGROUND_TEXTURE_SHADER_VAL = "_BackgroundTex";

	private const string MAJOR_TEXTURE_SHADER_VAL = "_MajorTex";

	private const string MINOR_TEXTURE_SHADER_VAL = "_MinorTex";

	private static readonly AssetReference m_tavernSignAsset = new AssetReference("FSG_TavernSign.prefab:8ce9cae2230ceda45a5f20996b704a9b");

	private GameObject m_sceneObject;

	private static readonly AssetReference[] m_fsgShields = new AssetReference[8]
	{
		new AssetReference("shield_01.prefab:78363d95f6d2de34fbc560266fea640d"),
		new AssetReference("shield_02.prefab:c377b1e43c7e56940b5976606e4c204d"),
		new AssetReference("shield_03.prefab:1aff0388b4ec9a541914c6001d89a1a4"),
		new AssetReference("shield_04.prefab:b8c72df88e9b2a346be4921349e95d69"),
		new AssetReference("shield_05.prefab:8b4ff22e9b7e20a44afdce7d38c71179"),
		new AssetReference("shield_06.prefab:e0adca09a959f1c4ea1921958d4b7b88"),
		new AssetReference("shield_07.prefab:62ae59fe1ee4edb41ab4e2f56f2c4c9d"),
		new AssetReference("shield_08.prefab:2ac01bcf753502d4391495e1ba01297f")
	};

	private static readonly AssetReference[] m_backgroundTextures = new AssetReference[15]
	{
		new AssetReference("FSG_BG_01.psd:e688e3dbcd82aa540bd5a237b8046087"),
		new AssetReference("FSG_BG_02.psd:ae5f9d676c6184d41b976845d4131392"),
		new AssetReference("FSG_BG_03.psd:2bfa2796138e44b4db4ffd7d7c35048c"),
		new AssetReference("FSG_BG_04.psd:4968e80d9d5570a49bf34a477953c463"),
		new AssetReference("FSG_BG_05.psd:8b2ce0acdd997df4d9a235d10b0b0245"),
		new AssetReference("FSG_BG_06.psd:50c2055eec0ae094e8c44d98a86ec997"),
		new AssetReference("FSG_BG_07.psd:2e9be5a80e6fb8c4ab5e8f30ecb529b5"),
		new AssetReference("FSG_BG_08.psd:3413b3c98ad07944b923e5b475c5cb71"),
		new AssetReference("FSG_BG_09.psd:495ad5978abcac5428426c9422b34f54"),
		new AssetReference("FSG_BG_10.psd:6840b0525caaafc46ada6c96aec606c7"),
		new AssetReference("FSG_BG_11.psd:183ea8ead0f840b458c3b6b6feaecd9e"),
		new AssetReference("FSG_BG_12.psd:36143d8a02d74644a95f4f875b084687"),
		new AssetReference("FSG_BG_13.psd:e86b53637b4c7f940af65cf93f139ad7"),
		new AssetReference("FSG_BG_14.psd:ca315657cd75a3d4183070b7620eea46"),
		new AssetReference("FSG_BG_15.psd:ba08c85d3825071429b4452a05e1f869")
	};

	private static readonly AssetReference[] m_majorTextures = new AssetReference[85]
	{
		new AssetReference("FSG_major_icon_01.psd:07f39638ef5fac0409bceafcfe91a017"),
		new AssetReference("FSG_major_icon_02.psd:ba033ce365731044dbd0a6447b927516"),
		new AssetReference("FSG_major_icon_03.psd:4e63ef4c0d31305449bc692cd8ed4296"),
		new AssetReference("FSG_major_icon_04.psd:1600613c07c2b894db3985ae0d058df6"),
		new AssetReference("FSG_major_icon_05.psd:2b3cec372d669a14bac09798de77f4aa"),
		new AssetReference("FSG_major_icon_06.psd:7f3790b88769cd745bae9b0bca991a42"),
		new AssetReference("FSG_major_icon_07.psd:d6de41cea2ada024e8e4f561f7604691"),
		new AssetReference("FSG_major_icon_08.psd:7511ad8cbc11b8f4abc097895bf36f72"),
		new AssetReference("FSG_major_icon_09.psd:52a8139050006ee4f8a713228ec0680e"),
		new AssetReference("FSG_major_icon_10.psd:783574c20786759499bd49291d09dd0b"),
		new AssetReference("FSG_major_icon_11.psd:abec391ccf583f2409e4274963e11fb7"),
		new AssetReference("FSG_major_icon_12.psd:ef67cc5c43169bd4a9fbf9e429fff2c1"),
		new AssetReference("FSG_major_icon_13.psd:bec11e064c7f3fd408c59174e001a566"),
		new AssetReference("FSG_major_icon_14.psd:db89aa7c56a75e542b41b4b68934150b"),
		new AssetReference("FSG_major_icon_15.psd:7227d20dda7e8b743b5b3429065b94cb"),
		new AssetReference("FSG_major_icon_16.psd:9a95f2321a81b034bb21bdd8af813dd7"),
		new AssetReference("FSG_major_icon_17.psd:7e3e3309328a8d24ca4d73a819455026"),
		new AssetReference("FSG_major_icon_18.psd:664f85016c825ae4d87b32f1e2aee030"),
		new AssetReference("FSG_major_icon_19.psd:585e794cb0197db47bf865aa7342bfce"),
		new AssetReference("FSG_major_icon_20.psd:fed552b35702f944eb3d71eaaebd811b"),
		new AssetReference("FSG_major_icon_21.psd:53a8ce78f94668b419be52058eb62744"),
		new AssetReference("FSG_major_icon_22.psd:e3f2458e7131899489ca0cd3e96202ce"),
		new AssetReference("FSG_major_icon_23.psd:061bd991c06d66b45a3e6e85ee917085"),
		new AssetReference("FSG_major_icon_24.psd:968f4450552f1234786ebc685dcfab37"),
		new AssetReference("FSG_major_icon_25.psd:0e7774e4335227148b30c32a319dcd91"),
		new AssetReference("FSG_major_icon_26.psd:9d7beaf2c0b180b4b836ce5d3ba213f0"),
		new AssetReference("FSG_major_icon_27.psd:7055b2b8640998f478e59eb17063d434"),
		new AssetReference("FSG_major_icon_28.psd:3b0eae9bf1035f943a83b5da9ea06265"),
		new AssetReference("FSG_major_icon_29.psd:733546c7388d3db4da3875a12d2dbb04"),
		new AssetReference("FSG_major_icon_30.psd:b3189578cae8a2a418edd28b18871e5a"),
		new AssetReference("FSG_major_icon_31.psd:440512e280677784e9d203d183bd4b3b"),
		new AssetReference("FSG_major_icon_32.psd:d6ba8937a47ae3443bf494d7081df118"),
		new AssetReference("FSG_major_icon_33.psd:8817a57cf03ca3b459f8179201e9ef61"),
		new AssetReference("FSG_major_icon_34.psd:11244accdd35fcf4eadff77a526e674c"),
		new AssetReference("FSG_major_icon_35.psd:9b5ab5a32b35f9744869f20eda9a5a3f"),
		new AssetReference("FSG_major_icon_36.psd:b63250e48235c3446aaed3d3eda8a039"),
		new AssetReference("FSG_major_icon_37.psd:06d29c693b13e4341bdccc98879318f3"),
		new AssetReference("FSG_major_icon_38.psd:b595fe851a209284090902687ef4719a"),
		new AssetReference("FSG_major_icon_39.psd:b1a27e92316e6154695947477b692f31"),
		new AssetReference("FSG_major_icon_40.psd:aed8fc8c49d256b4a9bdbb8d1f2653b4"),
		new AssetReference("FSG_major_icon_41.psd:d0c01bd273040e54fa1e37b4b968e21d"),
		new AssetReference("FSG_major_icon_42.psd:cad47cc1621a9a74aacb7538e62ed968"),
		new AssetReference("FSG_major_icon_43.psd:6a62aa135e1d7f24e9d16f232c819771"),
		new AssetReference("FSG_major_icon_44.psd:e1fa19d8f78ed604986f3e3b08da86e3"),
		new AssetReference("FSG_major_icon_45.psd:94a2d34daafb0db48aeee1bb29f30d94"),
		new AssetReference("FSG_major_icon_46.psd:eae1bb9b57e5d2d46b76c2da550d9361"),
		new AssetReference("FSG_major_icon_47.psd:f6d114a4c539da7409ed64c04a6b4d1c"),
		new AssetReference("FSG_major_icon_48.psd:13482644107b8124baaa27c4b69f7f40"),
		new AssetReference("FSG_major_icon_49.psd:60f5e08764889bd4891230677667c06a"),
		new AssetReference("FSG_major_icon_50.psd:ae3b236c6985ed149a8e11cae889891d"),
		new AssetReference("FSG_major_icon_51.psd:be89f941faf7e884f9517b3cc758cc95"),
		new AssetReference("FSG_major_icon_52.psd:d64ccacd8e3575f4f8934868aeeebac0"),
		new AssetReference("FSG_major_icon_53.psd:85e1dfeb648dd25478c278b10452ea04"),
		new AssetReference("FSG_major_icon_54.psd:3639a3461412bcd468f36bd0d8808194"),
		new AssetReference("FSG_major_icon_55.psd:6dfe240913765e7439864ae59e906021"),
		new AssetReference("FSG_major_icon_56.psd:50542c563f4226746948db1227e041a8"),
		new AssetReference("FSG_major_icon_57.psd:c35838a239a1a9d4fbe40886c1836151"),
		new AssetReference("FSG_major_icon_58.psd:0153d881bf26d904eafe57c0f4069b68"),
		new AssetReference("FSG_major_icon_59.psd:7ea63d9dc56429843ba7ad8e662d6be4"),
		new AssetReference("FSG_major_icon_60.psd:2da17d5f4dd8218458a8a47cc5a5315b"),
		new AssetReference("FSG_major_icon_61.psd:7138870a6a857f5439fda6dbf723ee8a"),
		new AssetReference("FSG_major_icon_62.psd:56c1d251d05be7849b21a92236292231"),
		new AssetReference("FSG_major_icon_63.psd:956d1ae251106c043aeea63cc82a8dc0"),
		new AssetReference("FSG_major_icon_64.psd:8507174dedd9fbf46ad2a3eb3608d3d0"),
		new AssetReference("FSG_major_icon_65.psd:f1c75986e3593584ab58710c5b9afc16"),
		new AssetReference("FSG_major_icon_66.psd:547d68ca4a4d9a847ba684b31d672754"),
		new AssetReference("FSG_major_icon_67.psd:c302ac3c5c6208b4d82db58ec2534840"),
		new AssetReference("FSG_major_icon_68.psd:999a9c18e17735246b975f538f5f39e9"),
		new AssetReference("FSG_major_icon_69.psd:d3bd17b65f76e734bb02b6201576613f"),
		new AssetReference("FSG_major_icon_70.psd:df022a496a17dd64c95a5f4f6d9e1dbf"),
		new AssetReference("FSG_major_icon_71.psd:390ccf576a2fc464ab1c3cdf9a01c05f"),
		new AssetReference("FSG_major_icon_72.psd:13c3d60992523af418244d648bec2927"),
		new AssetReference("FSG_major_icon_73.psd:bc28bce291ff2284395f1504ebd4c352"),
		new AssetReference("FSG_major_icon_74.psd:a7d9b83cf0f7ebf45abac1b19bd64f25"),
		new AssetReference("FSG_major_icon_75.psd:74d90f06be30baa4aba2c7d629d56edd"),
		new AssetReference("FSG_major_icon_76.psd:bf7de29836133584cb1529079c315956"),
		new AssetReference("FSG_major_icon_77.psd:1126fe28c57f50c42af8de4e64016dbe"),
		new AssetReference("FSG_major_icon_78.psd:f3d8a417df5cce244802d22ee22fb8f3"),
		new AssetReference("FSG_major_icon_79.psd:705c5ab1b294713469d61cb9b719c21f"),
		new AssetReference("FSG_major_icon_80.psd:7e4b7b718714ecc43b757f31b6f35b5a"),
		new AssetReference("FSG_major_icon_81.psd:16c7b39fdde347e4a80f6912a6a9c20e"),
		new AssetReference("FSG_major_icon_82.psd:5cd2aa06a7003ae449270d3f57698eaf"),
		new AssetReference("FSG_major_icon_83.psd:4e038124f13272e4197dca5ded30e3ec"),
		new AssetReference("FSG_major_icon_84.psd:66c8e00ffd94eaa4bb91ef35c8bfab63"),
		new AssetReference("FSG_major_icon_85.psd:79cc45177c1bbe3438a8404770221fbd")
	};

	private static readonly AssetReference[] m_minorTextures = new AssetReference[43]
	{
		new AssetReference("FSG_minor_icon_01.psd:76f1d2c44969469479ca1d22ed4bb2c5"),
		new AssetReference("FSG_minor_icon_02.psd:5720e69f56a33f343893cbe0bdb83328"),
		new AssetReference("FSG_minor_icon_03.psd:581cd97bcda285d469ea061c2a1b65e0"),
		new AssetReference("FSG_minor_icon_04.psd:eb9a4735aaee11047b850c1639180cc0"),
		new AssetReference("FSG_minor_icon_05.psd:73ffd45b96f36984683d94002aeb687f"),
		new AssetReference("FSG_minor_icon_06.psd:fb79ddcfa27651141a69fe57d707fd31"),
		new AssetReference("FSG_minor_icon_07.psd:2dc56df138ffca54c99c2823d8b8c230"),
		new AssetReference("FSG_minor_icon_08.psd:aa6b9c693dba947459171534c501561c"),
		new AssetReference("FSG_minor_icon_09.psd:1223eaead151e0442b39d44b79d2fe99"),
		new AssetReference("FSG_minor_icon_10.psd:e35d16e74556b824a82a24fdef18ab6e"),
		new AssetReference("FSG_minor_icon_11.psd:686a7f1c092367a4ebefcf0be60a5025"),
		new AssetReference("FSG_minor_icon_12.psd:f6951bde51d82f94b95dd49a17a9acde"),
		new AssetReference("FSG_minor_icon_13.psd:c22f4029bc9a09c47b05ce79910a85c9"),
		new AssetReference("FSG_minor_icon_14.psd:a19fd3d9b22bd5f439df4ceb1bf65b3d"),
		new AssetReference("FSG_minor_icon_15.psd:ef282300e35d6114682429509c1ec6be"),
		new AssetReference("FSG_minor_icon_16.psd:5a72a33d18d433442a381db9aa9c5eae"),
		new AssetReference("FSG_minor_icon_17.psd:b13ef82f1b931c741bbffc35b55ce244"),
		new AssetReference("FSG_minor_icon_18.psd:ba5c373f08f049848b950f3c547edc00"),
		new AssetReference("FSG_minor_icon_19.psd:fd556fad5a1adc0448aa85e14ae0b33b"),
		new AssetReference("FSG_minor_icon_20.psd:ec061cb081c39a749a3d7cc07aa4c5af"),
		new AssetReference("FSG_minor_icon_21.psd:6d47b3264df5bad40847b6a0b2c763ff"),
		new AssetReference("FSG_minor_icon_22.psd:4b21f492cc667104b9b4fc87dabca71f"),
		new AssetReference("FSG_minor_icon_23.psd:4cd0570a850886d41a962273726efc86"),
		new AssetReference("FSG_minor_icon_24.psd:cc11c8161f16f1a4298cc85629fd8f24"),
		new AssetReference("FSG_minor_icon_25.psd:f86b6ce99de7dba48a6a3f9617d9e37a"),
		new AssetReference("FSG_minor_icon_26.psd:b85753f42615a3442a328f41fb214a8d"),
		new AssetReference("FSG_minor_icon_27.psd:76ed51a0cbdac4a4885b6a9a6f35db34"),
		new AssetReference("FSG_minor_icon_28.psd:4e7c0cbabe0df1a4aa75ffb7ad4ecf3d"),
		new AssetReference("FSG_minor_icon_29.psd:45af214ee19ff79408336758bfbbd400"),
		new AssetReference("FSG_minor_icon_30.psd:c3670d1a631e2054984bd6ade942600b"),
		new AssetReference("FSG_minor_icon_31.psd:c7f21d37679fc6d4b980876ece61e1f4"),
		new AssetReference("FSG_minor_icon_32.psd:10746cf72967e2541b7978ff5e23ef79"),
		new AssetReference("FSG_minor_icon_33.psd:af1b2ce747ad74143aff763617ba9691"),
		new AssetReference("FSG_minor_icon_34.psd:c79f56adcf6621b4f9574c1e18adb146"),
		new AssetReference("FSG_minor_icon_35.psd:aed7c2408cd63c94991f2f5dc91a046b"),
		new AssetReference("FSG_minor_icon_36.psd:6409f5c8977ba1b4bbf65d919b35f860"),
		new AssetReference("FSG_minor_icon_37.psd:b4654929917a9b340ac6da0e51c2093b"),
		new AssetReference("FSG_minor_icon_38.psd:2285c681967265847a6b583271ffc132"),
		new AssetReference("FSG_minor_icon_39.psd:a0dc328dcc11e3049905c29c85ffa1fe"),
		new AssetReference("FSG_minor_icon_40.psd:8a69a6cc8757a0643a06083a1c7b4b3d"),
		new AssetReference("FSG_minor_icon_41.psd:4c48d1ce608b0b24684545ce45a08db8"),
		new AssetReference("FSG_minor_icon_42.psd:61224edd16eba5e47bdde26683514baa"),
		new AssetReference("FSG_minor_icon_43.psd:785e9e8832f639647a0ca0a3da6ca9f2")
	};

	private const int MAX_SIGN_INDEX = 8;

	private const int MAX_BACKGROUND_INDEX = 15;

	private const int MAX_MAJOR_INDEX = 85;

	private const int MAX_MINOR_INDEX = 43;

	private GameObject m_transitionInputBlocker;

	private static ReactiveObject<NetCache.NetCacheFeatures> s_guardianVars = ReactiveNetCacheObject<NetCache.NetCacheFeatures>.CreateInstance();

	private static ReactiveObject<FSGFeatureConfig> s_fsgFeaturesConfig = ReactiveNetCacheObject<FSGFeatureConfig>.CreateInstance();

	private static ReactiveObject<NetCache.NetCacheProfileProgress> s_profileProgress = ReactiveNetCacheObject<NetCache.NetCacheProfileProgress>.CreateInstance();

	private static ReactiveObject<NetCache.NetCacheClientOptions> s_clientOptions = ReactiveNetCacheObject<NetCache.NetCacheClientOptions>.CreateInstance();

	private ReactiveEnumOption<FormatType> m_FormatType = ReactiveEnumOption<FormatType>.CreateInstance(Option.FORMAT_TYPE);

	public long m_activeFSGMenu = -1L;

	public FiresideGatheringMode CurrentFiresideGatheringMode { get; set; }

	private GameObject SceneObject
	{
		get
		{
			if (m_sceneObject == null)
			{
				m_sceneObject = new GameObject("FiresideGatheringManagerSceneObject", typeof(HSDontDestroyOnLoad));
			}
			return m_sceneObject;
		}
	}

	private FiresideGatheringManagerData Data { get; set; }

	public bool HasSeenReturnToFSGSceneTooltip
	{
		get
		{
			return Data.m_hasSeenReturnToFSGSceneTooltip;
		}
		set
		{
			Data.m_hasSeenReturnToFSGSceneTooltip = value;
		}
	}

	public static bool IsFSGFeatureEnabled
	{
		get
		{
			if (TemporaryAccountManager.IsTemporaryAccount())
			{
				return false;
			}
			bool result = s_guardianVars.Value?.FSGEnabled ?? false;
			if (!GameUtils.AreAllTutorialsComplete())
			{
				return false;
			}
			return result;
		}
	}

	public static bool IsGpsFeatureEnabled
	{
		get
		{
			if (!IsFSGFeatureEnabled)
			{
				return false;
			}
			return s_fsgFeaturesConfig.Value?.Gps ?? true;
		}
	}

	public static bool IsWifiFeatureEnabled
	{
		get
		{
			if (!IsFSGFeatureEnabled)
			{
				return false;
			}
			return s_fsgFeaturesConfig.Value?.Wifi ?? true;
		}
	}

	public static bool CanRequestNearbyFSG
	{
		get
		{
			if (!IsFSGFeatureEnabled)
			{
				return false;
			}
			if (!IsGpsFeatureEnabled && !IsWifiFeatureEnabled)
			{
				return false;
			}
			return true;
		}
	}

	public bool IsRequestNearbyFSGsPending => m_isRequestNearbyFSGsPending;

	public bool HasFSGToInnkeeperSetup => m_innkeeperFSG != null;

	public FSGConfig FSGToInnkeeperSetup => m_innkeeperFSG;

	public TavernSignData LastSign { get; private set; }

	public FSGConfig CurrentFSG => m_currentFSG;

	public long CurrentFsgId
	{
		get
		{
			if (m_currentFSG != null)
			{
				return m_currentFSG.FsgId;
			}
			return 0L;
		}
	}

	public bool CurrentFsgIsLargeScale
	{
		get
		{
			if (m_currentFSG != null && m_currentFSG.HasIsLargeScaleFsg)
			{
				return m_currentFSG.IsLargeScaleFsg;
			}
			return false;
		}
	}

	public byte[] CurrentFsgSharedSecretKey => m_currentFSGSharedSecretKey;

	public List<GameContentScenario> CurrentFsgBrawls
	{
		get
		{
			TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
			if (mission == null)
			{
				return new List<GameContentScenario>();
			}
			bool useFallbackBrawls = m_innkeeperSelectedBrawlLibraryItemIds.Count == 0;
			return mission.BrawlList.Where(delegate(GameContentScenario scen)
			{
				if (scen.IsRequired)
				{
					return true;
				}
				if (useFallbackBrawls)
				{
					if (scen.IsFallback)
					{
						return true;
					}
				}
				else if (m_innkeeperSelectedBrawlLibraryItemIds.Contains(scen.LibraryItemId))
				{
					return true;
				}
				return false;
			}).ToList();
		}
	}

	[CustomEditField(Hide = true)]
	public bool IsCheckedIn => m_currentFSG != null;

	public bool IsPrerelease
	{
		get
		{
			if (m_currentFSG == null)
			{
				return false;
			}
			return TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)?.IsPrerelease ?? false;
		}
	}

	public List<BnetPlayer> DisplayablePatronList
	{
		get
		{
			if (!IsCheckedIn || CurrentFsgIsLargeScale)
			{
				return new List<BnetPlayer>();
			}
			return m_displayablePatrons.ToList();
		}
	}

	public int DisplayablePatronCount
	{
		get
		{
			if (!IsCheckedIn)
			{
				return 0;
			}
			return DisplayablePatronList.Count;
		}
	}

	public List<BnetPlayer> FullPatronList
	{
		get
		{
			List<BnetPlayer> list = new List<BnetPlayer>();
			if (IsCheckedIn && !CurrentFsgIsLargeScale)
			{
				list.AddRange(m_displayablePatrons);
				list.AddRange(m_pendingPatrons);
			}
			return list;
		}
	}

	private double Latitude
	{
		get
		{
			if (m_locationData == null || m_locationData.location == null)
			{
				return 0.0;
			}
			double num = m_locationData.location.Latitude;
			if (m_gpsCheatOffset != 0.0)
			{
				num += 57.2957763671875 * (m_gpsCheatOffset / 6378137.0);
			}
			return num;
		}
	}

	private double Longitude
	{
		get
		{
			if (m_locationData == null || m_locationData.location == null)
			{
				return 0.0;
			}
			return m_locationData.location.Longitude;
		}
	}

	private double GpsAccuracy
	{
		get
		{
			if (m_locationData == null || m_locationData.location == null)
			{
				return -1.0;
			}
			return m_locationData.location.Accuracy;
		}
	}

	public bool IsGpsLocationValid
	{
		get
		{
			if (!IsGpsFeatureEnabled)
			{
				return false;
			}
			if (m_locationData == null || m_locationData.location == null)
			{
				return false;
			}
			FSGFeatureConfig value = s_fsgFeaturesConfig.Value;
			if (value == null || m_locationData.location.Accuracy > (double)value.MaxAccuracy)
			{
				return false;
			}
			return true;
		}
	}

	private List<string> BSSIDS => m_accumulatedAccessPoints.Select((KeyValuePair<string, AccessPointInfo> kv) => kv.Key).ToList();

	[CustomEditField(Hide = true)]
	public bool AutoCheckInEnabled
	{
		get
		{
			if (!(s_guardianVars.Value?.FSGAutoCheckinEnabled ?? false))
			{
				return false;
			}
			FSGFeatureConfig value = s_fsgFeaturesConfig.Value;
			if (value == null || !value.AutoCheckin)
			{
				return false;
			}
			return true;
		}
	}

	[CustomEditField(Hide = true)]
	public int FriendListPatronCountLimit
	{
		get
		{
			NetCache.NetCacheFeatures value = s_guardianVars.Value;
			if (value == null || value.FSGFriendListPatronCountLimit < 0)
			{
				return 30;
			}
			return value.FSGFriendListPatronCountLimit;
		}
	}

	public ReactiveBoolOption PlayerAccountShouldAutoCheckin { get; set; } = ReactiveBoolOption.CreateInstance(Option.SHOULD_AUTO_CHECK_IN_TO_FIRESIDE_GATHERINGS);


	public ReactiveBoolOption HasManuallyInitiatedFSGScanBefore { get; set; } = ReactiveBoolOption.CreateInstance(Option.HAS_INITIATED_FIRESIDE_GATHERING_SCAN);


	public ReactiveLongOption LastTavernID { get; set; } = new ReactiveLongOption(Option.LAST_TAVERN_JOINED);


	private bool CanAutoCheckInEventually
	{
		get
		{
			if (AutoCheckInEnabled && PlayerAccountShouldAutoCheckin.Value && !m_errorOccuredOnCheckin)
			{
				return GameUtils.AreAllTutorialsComplete();
			}
			return false;
		}
	}

	public static event OnPatronListUpdatedCallback OnPatronListUpdated;

	public event CheckedInToFSGCallback OnJoinFSG;

	public event CheckedOutOfFSGCallback OnLeaveFSG;

	public event RequestNearbyFSGsCallback OnNearbyFSGs;

	public event NearbyFSGsChangedCallback OnNearbyFSGsChanged;

	public event OnInnkeeperSetupFinishedCallback OnInnkeeperSetupFinished;

	public event FSGSignClosedCallback OnSignClosed;

	public event FSGSignShownCallback OnSignShown;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		LoadResource loadData = new LoadResource("ServiceData/FiresideGatheringManagerData", LoadResourceFlags.FailOnError);
		yield return loadData;
		Data = loadData.LoadedAsset as FiresideGatheringManagerData;
		BnetPresenceMgr.Get();
		HearthstoneApplication.Get().WillReset += WillReset;
		serviceLocator.Get<SceneMgr>().RegisterScenePreUnloadEvent(SceneMgr_OnScenePreUnload);
		Action action = delegate
		{
			ChatMgr.Get().OnFriendListToggled += OnFriendListClosed_CloseTooltip;
		};
		if (ChatMgr.Get() == null)
		{
			ChatMgr.OnStarted += action;
		}
		else
		{
			action();
		}
		Action action2 = delegate
		{
			DialogManager.Get().OnDialogShown += CloseTooltip;
		};
		if (DialogManager.Get() == null)
		{
			DialogManager.OnStarted += action2;
		}
		else
		{
			action2();
		}
		Network network = serviceLocator.Get<Network>();
		network.RegisterNetHandler(RequestNearbyFSGsResponse.PacketID.ID, OnRequestNearbyFSGsResponse);
		network.RegisterNetHandler(CheckInToFSGResponse.PacketID.ID, OnCheckInToFSGResponse);
		network.RegisterNetHandler(CheckOutOfFSGResponse.PacketID.ID, OnCheckOutOfFSGResponse);
		network.RegisterNetHandler(InnkeeperSetupGatheringResponse.PacketID.ID, OnInnkeeperSetupGatheringResponse);
		network.RegisterNetHandler(FSGPatronListUpdate.PacketID.ID, OnPatronListUpdateReceivedFromServer);
		s_guardianVars.Init();
		s_clientOptions.Init();
		s_profileProgress.Init();
		s_fsgFeaturesConfig.Init();
		NetCache netCache = serviceLocator.Get<NetCache>();
		netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheFeatures), OnNetCache_GuardianVars);
		netCache.RegisterUpdatedListener(typeof(FSGFeatureConfig), OnNetCache_FSGFeatureConfig);
		netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheProfileProgress), CheckCanBeginLocationDataGatheringForLogin);
		netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheClientOptions), CheckCanBeginLocationDataGatheringForLogin);
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPlayersPresenceChanged);
		Action action3 = delegate
		{
			CollectionManager.Get().RegisterDeckDeletedListener(CollectionManager_DeckDeleted);
		};
		if (CollectionManager.Get() == null)
		{
			CollectionManager.OnCollectionManagerReady += action3.Invoke;
		}
		else
		{
			action3();
		}
		CheckCanBeginLocationDataGatheringForLogin();
	}

	public Type[] GetDependencies()
	{
		return new Type[4]
		{
			typeof(Network),
			typeof(NetCache),
			typeof(FullScreenFXMgr),
			typeof(SceneMgr)
		};
	}

	public void Shutdown()
	{
		HearthstoneApplication.Get().WillReset -= WillReset;
		if (IsCheckedIn)
		{
			Log.FiresideGatherings.Print("OnApplicationQuit: calling check out.");
			CheckOutOfFSG();
		}
	}

	public void Update()
	{
		if (!m_waitingForCheckIn)
		{
			return;
		}
		if (IsCheckedIn)
		{
			TransitionToFSGSceneIfSafe();
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FIRESIDE_GATHERING || SceneMgr.Get().GetNextMode() == SceneMgr.Mode.FIRESIDE_GATHERING)
			{
				m_waitingForCheckIn = false;
			}
		}
		else if (!CanAutoCheckInEventually || m_nearbyFSGs.Count < 1)
		{
			m_waitingForCheckIn = false;
			DialogManager.Get().ShowFiresideGatheringCheckInFailedDialog();
		}
	}

	public void FixedUpdate()
	{
		if (!m_haltFSGNotificationsAndCheckins && GameUtils.AreAllTutorialsComplete())
		{
			AutoInnkeeperSetup();
			AutoCheckIn();
			NotifyFSGNearbyIfNeeded();
		}
		DoStartAndEndTimingEvents();
		m_haltFSGNotificationsAndCheckins = false;
	}

	public float GetSecondsBetweenUpdates()
	{
		return 1f;
	}

	private void WillReset()
	{
		if (IsCheckedIn)
		{
			CheckOutOfFSG();
			LeaveFSG();
		}
		m_nearbyFSGs.Clear();
		m_hasBegunLocationDataGatheringForLogin = false;
		m_fsgSignShown = false;
		m_tooltipShowing = null;
		HasSeenReturnToFSGSceneTooltip = false;
		m_waitingForCheckIn = false;
		m_errorOccuredOnCheckin = false;
		ChatMgr.Get().OnFriendListToggled -= ShowReturnToFSGSceneTooltip_OnFriendListToggled_ShowNextTooltip;
	}

	public static FiresideGatheringManager Get()
	{
		return HearthstoneServices.Get<FiresideGatheringManager>();
	}

	private string GetTavernName_TavernSign(FSGConfig fsg)
	{
		if (string.IsNullOrEmpty(fsg.TavernName))
		{
			return GameStrings.Get("GLOBAL_FIRESIDE_GATHERING_DEFAULT_TAVERN_NAME");
		}
		return fsg.TavernName;
	}

	public string GetTavernName_FriendsList(FSGConfig fsg)
	{
		if (!string.IsNullOrEmpty(fsg.TavernName))
		{
			return fsg.TavernName;
		}
		if (m_innkeepers.TryGetValue(fsg.FsgId, out var value) && value.GetBattleTag() != null)
		{
			return GameStrings.Format("GLOBAL_FIRESIDE_GATHERING_FIRST_TIME_TAVERN_NAME", value.GetBattleTag().ToString());
		}
		return GameStrings.Get("GLOBAL_FIRESIDE_GATHERING_DEFAULT_TAVERN_NAME");
	}

	public void CheckInToFSG(long fsgId)
	{
		m_checkInRequestPending = true;
		m_nearbyFSGsFoundEventSent = true;
		if (BnetPresenceMgr.Get().GetMyPlayer().IsAppearingOffline())
		{
			PromptPlayerToAppearOnline(fsgId);
			return;
		}
		FSGConfig fSGConfig = m_nearbyFSGs.FirstOrDefault((FSGConfig f) => f.FsgId == fsgId);
		string text = ((fSGConfig == null) ? "<notfound>" : fSGConfig.TavernName);
		Log.FiresideGatherings.Print("CheckInToFSG: sending check in to server for {0}-{1}", fsgId, string.IsNullOrEmpty(text) ? "<no name>" : text);
		if (m_gpsCheatingLocation)
		{
			Network.Get().CheckInToFSG(fsgId, m_gpsCheatLatitude, m_gpsCheatLongitude, 0.0, IsWifiFeatureEnabled ? BSSIDS : null);
		}
		else if (IsGpsLocationValid)
		{
			Network.Get().CheckInToFSG(fsgId, Latitude, Longitude, GpsAccuracy, IsWifiFeatureEnabled ? BSSIDS : null);
		}
		else if (IsWifiFeatureEnabled)
		{
			Network.Get().CheckInToFSG(fsgId, BSSIDS);
		}
		else if (m_waitingForCheckIn)
		{
			m_waitingForCheckIn = false;
			ShowNoGPSOrWifiAlertPopup();
		}
	}

	public void SetWaitingForCheckIn()
	{
		m_waitingForCheckIn = true;
	}

	public void ClearErrorOccuredOnCheckIn()
	{
		m_errorOccuredOnCheckin = false;
	}

	public void BeginLocationDataGatheringForLogin()
	{
		if (m_hasBegunLocationDataGatheringForLogin)
		{
			return;
		}
		Log.FiresideGatherings.Print("FiresideGatheringManager.BeginLocationDataGathering");
		if (!IsFSGFeatureEnabled)
		{
			Log.FiresideGatherings.Print("FiresideGatheringManager.BeginLocationDataGathering FEATURE DISABLED");
		}
		else
		{
			if (!HasManuallyInitiatedFSGScanBefore.Value)
			{
				return;
			}
			if (!ClientLocationManager.Get().GPSServicesReady)
			{
				Processor.RunCoroutine(WaitThenBeginLocationDataGatheringForLogin());
				return;
			}
			bool hasValue = Vars.Key("Location.Latitude").HasValue;
			bool hasValue2 = Vars.Key("Location.Longitude").HasValue;
			bool flag = hasValue && hasValue2;
			string[] array = Vars.Key("Location.BSSID").GetStr(string.Empty).Split(new char[3] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
			bool flag2 = array != null && array.Length != 0;
			bool flag3 = IsGpsFeatureEnabled && (flag || ClientLocationManager.Get().GPSAvailable);
			m_hasBegunLocationDataGatheringForLogin = true;
			if (flag || flag2)
			{
				ClientLocationData clientLocationData = null;
				ClientLocationData clientLocationData2 = null;
				if (flag3 && flag)
				{
					double @double = Vars.Key("Location.Latitude").GetDouble(0.0);
					double double2 = Vars.Key("Location.Longitude").GetDouble(0.0);
					clientLocationData = new ClientLocationData();
					clientLocationData.location = new GpsCoordinate(@double, double2, 0.0, TimeUtils.GetElapsedTimeSinceEpoch().TotalSeconds);
					OnLocationDataGPSUpdate(clientLocationData);
				}
				if (IsWifiFeatureEnabled && flag2)
				{
					clientLocationData2 = new ClientLocationData();
					clientLocationData2.accessPointSamples = array.Select((string bssid) => new AccessPointInfo
					{
						bssid = bssid
					}).ToList();
					OnLocationDataWIFIUpdate(clientLocationData2);
				}
				if (clientLocationData != null || clientLocationData2 != null)
				{
					OnLocationDataComplete();
					return;
				}
			}
			if (flag3)
			{
				if (IsWifiFeatureEnabled)
				{
					ClientLocationManager.Get().RequestGPSAndWifiData(OnLocationDataGPSUpdate, OnLocationDataWIFIUpdate, OnLocationDataComplete);
				}
				else
				{
					ClientLocationManager.Get().RequestGPSData(OnLocationDataGPSUpdate, OnLocationDataComplete);
				}
			}
			else if (IsWifiFeatureEnabled)
			{
				ClientLocationManager.Get().RequestWifiData(OnLocationDataWIFIUpdate, OnLocationDataComplete);
			}
			else
			{
				RequestNearbyFSGs(isStateCheck: true);
			}
		}
	}

	private IEnumerator WaitThenBeginLocationDataGatheringForLogin()
	{
		Log.FiresideGatherings.Print("FiresideGatheringManager.WaitThenBeginLocationDataGatheringForLogin");
		yield return new WaitForSeconds(1f);
		BeginLocationDataGatheringForLogin();
	}

	public void CheckOutOfFSG(bool optOut = false)
	{
		if (IsCheckedIn)
		{
			if (optOut)
			{
				PlayerAccountShouldAutoCheckin.Set(newValue: false);
			}
			FSGConfig currentFSG = m_currentFSG;
			BackOutOfFSGScene();
			Network.Get().CheckOutOfFSG(currentFSG.FsgId);
		}
	}

	private void BackOutOfFSGScene()
	{
		if (Get().CurrentFiresideGatheringMode != 0 && SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			Navigation.Clear();
			if (!HearthstoneApplication.Get().IsResetting())
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			}
		}
		CurrentFiresideGatheringMode = FiresideGatheringMode.NONE;
	}

	private void CollectionManager_DeckDeleted(CollectionDeck removedDeck)
	{
		if (removedDeck.Type == DeckType.FSG_BRAWL_DECK)
		{
			UpdateDeckValidity();
		}
	}

	public void UpdateDeckValidity()
	{
		if (!IsCheckedIn)
		{
			BnetPresenceMgr.Get().SetDeckValidity(null);
			return;
		}
		DeckValidity deckValidity = BnetPresenceMgr.Get().GetMyPlayer().GetHearthstoneGameAccount()
			.GetDeckValidity();
		if (deckValidity == null)
		{
			deckValidity = new DeckValidity();
		}
		foreach (FormatType value in Enum.GetValues(typeof(FormatType)))
		{
			if (value != 0 && value != FormatType.FT_WILD)
			{
				deckValidity.ValidFormatDecks.Add(new FormatDeckValidity
				{
					FormatType = value,
					ValidDeck = CollectionManager.Get().AccountHasValidDeck(value)
				});
			}
		}
		deckValidity.ValidFormatDecks.Add(new FormatDeckValidity
		{
			FormatType = FormatType.FT_WILD,
			ValidDeck = (CollectionManager.Get().AccountHasValidDeck(FormatType.FT_STANDARD) || CollectionManager.Get().AccountHasValidDeck(FormatType.FT_WILD))
		});
		deckValidity.ValidTavernBrawlDeck = GenerateBrawlDeckValidity(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		deckValidity.ValidFiresideBrawlDeck = GenerateBrawlDeckValidity(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
		BnetPresenceMgr.Get().SetDeckValidity(deckValidity);
	}

	private List<BrawlDeckValidity> GenerateBrawlDeckValidity(BrawlType brawlType)
	{
		List<BrawlDeckValidity> list = new List<BrawlDeckValidity>();
		if (!TavernBrawlManager.Get().IsTavernBrawlActive(brawlType))
		{
			return list;
		}
		TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(brawlType);
		if (mission == null)
		{
			return list;
		}
		int seasonId = mission.seasonId;
		foreach (GameContentScenario brawl in mission.BrawlList)
		{
			bool validDeck = !mission.CanCreateDeck(brawl.LibraryItemId) || TavernBrawlManager.Get().HasValidDeck(brawlType, brawl.LibraryItemId);
			list.Add(new BrawlDeckValidity
			{
				SeasonId = seasonId,
				BrawlLibraryItemId = brawl.LibraryItemId,
				ValidDeck = validDeck
			});
		}
		return list;
	}

	public bool OpponentHasValidDeckForSelectedPlaymode(BnetPlayer opponent)
	{
		DeckValidity deckValidity = opponent.GetHearthstoneGameAccount().GetDeckValidity();
		switch (CurrentFiresideGatheringMode)
		{
		case FiresideGatheringMode.FRIENDLY_CHALLENGE:
			return deckValidity.ValidFormatDecks.Exists((FormatDeckValidity x) => x.ValidDeck && x.FormatType == m_FormatType.Value);
		case FiresideGatheringMode.FRIENDLY_CHALLENGE_BRAWL:
		{
			List<BrawlDeckValidity> brawlDeckValidity2 = deckValidity?.ValidTavernBrawlDeck;
			return OpponentHasValidTavernBrawlDeck(BrawlType.BRAWL_TYPE_TAVERN_BRAWL, brawlDeckValidity2);
		}
		case FiresideGatheringMode.FIRESIDE_BRAWL:
		{
			List<BrawlDeckValidity> brawlDeckValidity = deckValidity?.ValidFiresideBrawlDeck;
			return OpponentHasValidTavernBrawlDeck(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING, brawlDeckValidity);
		}
		default:
			return false;
		}
	}

	private bool OpponentHasValidTavernBrawlDeck(BrawlType brawlType, List<BrawlDeckValidity> brawlDeckValidity)
	{
		TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(brawlType);
		if (mission == null)
		{
			return false;
		}
		if (!mission.CanCreateDeck(mission.SelectedBrawlLibraryItemId))
		{
			return true;
		}
		if (brawlDeckValidity == null)
		{
			return false;
		}
		return brawlDeckValidity.FirstOrDefault((BrawlDeckValidity brawlInfo) => brawlInfo.SeasonId == mission.seasonId && brawlInfo.BrawlLibraryItemId == mission.SelectedBrawlLibraryItemId)?.ValidDeck ?? false;
	}

	private void JoinFSG(long fsgID, List<FSGPatron> patrons, byte[] sharedSecretKey, List<int> innkeeperSelectedBrawlLibraryItemIds)
	{
		m_currentFSG = null;
		m_currentFSGSharedSecretKey = null;
		m_innkeeperSelectedBrawlLibraryItemIds.Clear();
		foreach (FSGConfig nearbyFSG in m_nearbyFSGs)
		{
			if (nearbyFSG.FsgId == fsgID)
			{
				m_currentFSG = nearbyFSG;
				m_currentFSGSharedSecretKey = sharedSecretKey;
				m_innkeeperSelectedBrawlLibraryItemIds = new HashSet<int>(innkeeperSelectedBrawlLibraryItemIds);
				break;
			}
		}
		if (m_currentFSG == null)
		{
			Log.FiresideGatherings.PrintError("FiresideGatheringManager.OnCheckInToGatheringResponse: Error: Didn't have a corresponding FSG for checkin");
			m_errorOccuredOnCheckin = true;
			return;
		}
		LastTavernID.Set(m_currentFSG.FsgId);
		m_pendingPatrons.Clear();
		m_displayablePatrons.Clear();
		m_knownPatronsFromServer.Clear();
		m_isAppendingPatronList = true;
		RebuildKnownPatronsFromPresence();
		if (!CurrentFsgIsLargeScale && patrons != null)
		{
			foreach (FSGPatron patron in patrons)
			{
				AddKnownPatron(patron, isKnownFromPresence: false);
			}
			FiresideGatheringPresenceManager.Get().AddRemovePatronSubscriptions(patrons, null);
		}
		PlayerAccountShouldAutoCheckin.Set(newValue: true);
		m_isAppendingPatronList = false;
		UpdateMyPresence();
		Processor.ScheduleCallback(FiresideGatheringPresenceManager.PERIODIC_SUBSCRIBE_CHECK_SECONDS, realTime: true, PeriodicCheckForMoreSubscribeOpportunities);
		TransitionToFSGSceneIfSafe();
		if (this.OnJoinFSG != null)
		{
			this.OnJoinFSG(m_currentFSG);
		}
	}

	private void OnSceneLoadedDuringAutoCheckin(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		SceneMgr.Get().UnregisterSceneLoadedEvent(OnSceneLoadedDuringAutoCheckin);
		TransitionToFSGSceneIfSafe();
	}

	private void PromptPlayerToAppearOnline(long fsgId)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_FIRESIDE_GATHERING");
		popupInfo.m_text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_APPEAR_ONLINE_PROMPT");
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES");
		popupInfo.m_cancelText = GameStrings.Get("GLOBAL_BUTTON_NO");
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_responseCallback = OnAppearOnlineResponse;
		popupInfo.m_responseUserData = fsgId;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void OnAppearOnlineResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM)
		{
			BnetPresenceMgr.Get().SetAccountField(12u, val: false);
			long fsgId = (long)userData;
			CheckInToFSG(fsgId);
		}
		else
		{
			m_waitingForCheckIn = false;
			BackOutOfFSGScene();
			LeaveFSG();
		}
	}

	private void LeaveFSG()
	{
		FSGConfig currentFSG = m_currentFSG;
		m_currentFSG = null;
		m_currentFSGSharedSecretKey = null;
		m_innkeeperSelectedBrawlLibraryItemIds.Clear();
		m_nearbyFSGsFoundEventSent = true;
		HideFSGSign(m_fsgSignShown);
		m_fsgSignShown = false;
		HasSeenReturnToFSGSceneTooltip = false;
		m_pendingPatrons.Clear();
		m_displayablePatrons.Clear();
		m_knownPatronsFromServer.Clear();
		m_knownPatronsFromPresence.Clear();
		FiresideGatheringPresenceManager.Get().ClearSubscribedPatrons();
		PlayerAccountShouldAutoCheckin.Set(newValue: false);
		UpdateMyPresence();
		Processor.CancelScheduledCallback(PeriodicCheckForMoreSubscribeOpportunities);
		if (this.OnLeaveFSG != null)
		{
			this.OnLeaveFSG(currentFSG);
		}
	}

	public bool IsCheckedInToFSG(long gatheringID)
	{
		if (m_currentFSG != null)
		{
			return m_currentFSG.FsgId == gatheringID;
		}
		return false;
	}

	public bool IsPlayerInMyFSG(BnetPlayer player)
	{
		if (player == null)
		{
			return false;
		}
		if (IsPlayerInMyFSGAndDisplayable(player))
		{
			return true;
		}
		foreach (BnetPlayer pendingPatron in m_pendingPatrons)
		{
			BnetAccountId accountId = pendingPatron.GetAccountId();
			BnetAccountId accountId2 = player.GetAccountId();
			if (accountId != null && accountId2 != null && accountId.GetLo() == accountId2.GetLo())
			{
				return true;
			}
			BnetGameAccountId bestGameAccountId = pendingPatron.GetBestGameAccountId();
			BnetGameAccountId bestGameAccountId2 = player.GetBestGameAccountId();
			if (bestGameAccountId != null && bestGameAccountId2 != null && bestGameAccountId.GetLo() == bestGameAccountId2.GetLo())
			{
				return true;
			}
		}
		return false;
	}

	public bool IsPlayerInMyFSGAndDisplayable(BnetPlayer player)
	{
		if (player == null || CurrentFsgIsLargeScale)
		{
			return false;
		}
		foreach (BnetPlayer displayablePatron in m_displayablePatrons)
		{
			BnetAccountId accountId = displayablePatron.GetAccountId();
			BnetAccountId accountId2 = player.GetAccountId();
			if (accountId != null && accountId2 != null && accountId.GetLo() == accountId2.GetLo())
			{
				return true;
			}
			BnetGameAccountId bestGameAccountId = displayablePatron.GetBestGameAccountId();
			BnetGameAccountId bestGameAccountId2 = player.GetBestGameAccountId();
			if (bestGameAccountId != null && bestGameAccountId2 != null && bestGameAccountId.GetLo() == bestGameAccountId2.GetLo())
			{
				return true;
			}
		}
		return false;
	}

	public List<FSGConfig> GetFSGs()
	{
		return m_nearbyFSGs;
	}

	public int FiresideGatheringSort(FSGConfig fsg1, FSGConfig fsg2)
	{
		if (IsCheckedInToFSG(fsg1.FsgId))
		{
			return 1;
		}
		if (IsCheckedInToFSG(fsg2.FsgId))
		{
			return -1;
		}
		int num = string.Compare(fsg1.TavernName, fsg2.TavernName);
		if (num != 0)
		{
			return num;
		}
		return (int)(fsg1.FsgId - fsg2.FsgId);
	}

	public int FiresideGatheringPlayerSort(BnetPlayer patron1, BnetPlayer patron2)
	{
		int result = 0;
		bool lhsflag = BnetFriendMgr.Get().IsFriend(patron1);
		bool rhsflag = BnetFriendMgr.Get().IsFriend(patron2);
		if (FriendUtils.FriendFlagSort(patron1, patron2, lhsflag, rhsflag, out result))
		{
			return result;
		}
		return FriendUtils.FriendNameSort(patron1, patron2);
	}

	public bool ShowSignIfNeeded(OnCloseSign callback = null)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (!IsCheckedIn || m_fsgSignShown || SceneMgr.Get().IsTransitioning() || mode != SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			return false;
		}
		m_fsgSignShown = true;
		ShowSign(m_currentFSG.SignData, GetTavernName_TavernSign(m_currentFSG), callback);
		return true;
	}

	public bool ShowSmallSignIfNeeded(Transform smallSignContainer)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (!IsCheckedIn || !m_fsgSignShown || mode != SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			return false;
		}
		m_fsgSignShown = true;
		m_smallSignContainer = smallSignContainer;
		ShowSmallSign(m_currentFSG.SignData, GetTavernName_TavernSign(m_currentFSG));
		return true;
	}

	public void GotoFSGLink()
	{
		Application.OpenURL(ExternalUrlService.Get().GetFSGLink());
	}

	public void ShowFindFSGDialog()
	{
		if (HasManuallyInitiatedFSGScanBefore.Value)
		{
			ClientLocationManager.Get().RequestGPSData(OnLocationDataGPSUpdate);
		}
		DialogManager.Get().ShowFiresideGatheringFindEventDialog(OnFindEventDialogCallBack);
	}

	public void OnLocationDataGPSUpdate(ClientLocationData locationData)
	{
		if (m_locationData == null)
		{
			m_locationData = locationData;
		}
		m_locationData.location = locationData.location;
	}

	public void OnLocationDataWIFIUpdate(ClientLocationData locationData)
	{
		if (m_locationData == null)
		{
			m_locationData = locationData;
		}
		m_locationData.accessPointSamples = locationData.accessPointSamples;
		m_accumulatedAccessPoints.Clear();
		foreach (AccessPointInfo accessPointSample in m_locationData.accessPointSamples)
		{
			if (IsValidBSSID(accessPointSample.bssid))
			{
				m_accumulatedAccessPoints[accessPointSample.bssid] = accessPointSample;
			}
		}
	}

	public void AddWIFIAccessPoints(ClientLocationData locationData)
	{
		if (m_locationData == null)
		{
			m_locationData = locationData;
		}
		if (locationData == null)
		{
			return;
		}
		foreach (AccessPointInfo accessPointSample in locationData.accessPointSamples)
		{
			if (IsValidBSSID(accessPointSample.bssid))
			{
				m_accumulatedAccessPoints[accessPointSample.bssid] = accessPointSample;
			}
		}
	}

	public void RequestNearbyFSGs(bool isStateCheck = false)
	{
		if (!IsFSGFeatureEnabled)
		{
			Log.FiresideGatherings.Print("Not requesting Nearby FSGs because feature is disabled for me.");
			return;
		}
		Log.FiresideGatherings.Print("Requesting Nearby FSGS: gps={0} wifi={1} accuracy={2}", IsGpsFeatureEnabled, IsWifiFeatureEnabled, GpsAccuracy);
		m_isRequestNearbyFSGsPending = true;
		if (m_gpsCheatingLocation)
		{
			Network.Get().RequestNearbyFSGs(m_gpsCheatLatitude, m_gpsCheatLongitude, 0.0, IsWifiFeatureEnabled ? BSSIDS : null);
		}
		else if (isStateCheck)
		{
			Network.Get().RequestNearbyFSGs(null);
		}
		else if (IsGpsLocationValid)
		{
			Network.Get().RequestNearbyFSGs(Latitude, Longitude, GpsAccuracy, IsWifiFeatureEnabled ? BSSIDS : null);
		}
		else if (IsWifiFeatureEnabled)
		{
			Network.Get().RequestNearbyFSGs(BSSIDS);
		}
	}

	public void InnkeeperSetupFSG(bool provideWifiForTavern)
	{
		Log.FiresideGatherings.Print("Doing Innkeeper FSG Setup");
		if (m_innkeeperFSG == null)
		{
			Log.FiresideGatherings.PrintError("FiresideGatheringManager.InnkeeperSetupFSG tried to setup an FSG but no valid FSG exists");
			return;
		}
		long fsgId = m_innkeeperFSG.FsgId;
		if (m_gpsCheatingLocation)
		{
			Network.Get().InnkeeperSetupFSG(m_gpsCheatLatitude, m_gpsCheatLongitude, 0.0, (IsWifiFeatureEnabled && provideWifiForTavern) ? BSSIDS : null, fsgId);
		}
		else if (IsGpsLocationValid)
		{
			Network.Get().InnkeeperSetupFSG(Latitude, Longitude, GpsAccuracy, (IsWifiFeatureEnabled && provideWifiForTavern) ? BSSIDS : null, fsgId);
		}
		else if (IsWifiFeatureEnabled)
		{
			Network.Get().InnkeeperSetupFSG(provideWifiForTavern ? BSSIDS : null, fsgId);
		}
	}

	public void RequestFSGNotificationAndCheckinsHalt()
	{
		m_haltFSGNotificationsAndCheckins = true;
	}

	public void ShowFiresideGatheringInnkeeperSetupDialog()
	{
		ChatMgr.Get().CloseChatUI();
		string tavernName_TavernSign = GetTavernName_TavernSign(m_innkeeperFSG);
		DialogManager.Get().ShowFiresideGatheringInnkeeperSetupDialog(ShowFiresideGatheringInnkeeperSetup_OnResponse, tavernName_TavernSign);
	}

	public void ShowInnkeeperSetupTooltip()
	{
		ShowTooltip(GameStrings.Get("GLUE_FIRESIDE_GATHERING_INNKEEPER_TOOLTIP"), 6f);
	}

	public bool InBrawlMode()
	{
		if (CurrentFiresideGatheringMode != FiresideGatheringMode.FIRESIDE_BRAWL)
		{
			return CurrentFiresideGatheringMode == FiresideGatheringMode.FRIENDLY_CHALLENGE_BRAWL;
		}
		return true;
	}

	public static bool IsValidBSSID(string bssid)
	{
		bool flag = false;
		foreach (char c in bssid)
		{
			bool flag2 = c == ':';
			bool flag3 = (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
			if (!(flag2 || flag3))
			{
				return false;
			}
			flag = flag || (!flag2 && c != '0');
		}
		if (!flag)
		{
			return false;
		}
		return true;
	}

	public void EnableTransitionInputBlocker(bool enabled)
	{
		if (m_transitionInputBlocker == null)
		{
			InitializeTransitionInputBlocker(enabled);
		}
		else
		{
			m_transitionInputBlocker.gameObject.SetActive(enabled);
		}
	}

	public void TransitionToFSGSceneIfSafe()
	{
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.HUB:
		case SceneMgr.Mode.DRAFT:
		case SceneMgr.Mode.ADVENTURE:
		case SceneMgr.Mode.TAVERN_BRAWL:
			if (!PopupDisplayManager.Get().IsShowing && !StoreManager.Get().IsShownOrWaitingToShow())
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.FIRESIDE_GATHERING);
				EnableTransitionInputBlocker(enabled: true);
			}
			break;
		case SceneMgr.Mode.LOGIN:
			SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoadedDuringAutoCheckin);
			break;
		}
	}

	private void OnLocationDataComplete()
	{
		NetCache netCache = NetCache.Get();
		if (netCache != null)
		{
			NetCache.NetCacheFeatures netObject = netCache.GetNetObject<NetCache.NetCacheFeatures>();
			if (netObject != null && netObject.FSGLoginScanEnabled)
			{
				RequestNearbyFSGs();
			}
		}
	}

	private void AutoInnkeeperSetup()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (m_doAutoInnkeeperSetup && !IsCheckedIn && m_innkeeperFSG != null && !m_innkeeperFSG.IsSetupComplete && mode == SceneMgr.Mode.HUB && !SceneMgr.Get().IsTransitioning() && !m_checkInDialogShown && !PopupDisplayManager.Get().IsShowing)
		{
			m_doAutoInnkeeperSetup = false;
			m_haltAutoCheckinWhileInnkeeperSetup = true;
			ShowFiresideGatheringInnkeeperSetupDialog();
		}
	}

	private void AutoCheckIn()
	{
		if (IsCheckedIn || !CanAutoCheckInEventually)
		{
			return;
		}
		FSGConfig preferredFSG = GetPreferredFSG();
		if (preferredFSG == null || m_checkInRequestPending || m_checkInDialogShown || m_haltAutoCheckinWhileInnkeeperSetup || (preferredFSG.IsInnkeeper && !preferredFSG.IsSetupComplete))
		{
			return;
		}
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode != SceneMgr.Mode.GAMEPLAY && mode != SceneMgr.Mode.COLLECTIONMANAGER && !StoreManager.Get().IsShownOrWaitingToShow() && !PopupDisplayManager.Get().IsShowing)
		{
			if (mode == SceneMgr.Mode.LOGIN || mode == SceneMgr.Mode.STARTUP || mode == SceneMgr.Mode.HUB)
			{
				CheckInToFSG(preferredFSG.FsgId);
				return;
			}
			DialogManager.Get().ShowFiresideGatheringNearbyDialog(OnJoinFSGDialogResponse);
			m_checkInDialogShown = true;
		}
	}

	private FSGConfig GetPreferredFSG()
	{
		if (m_nearbyFSGs.Count < 1)
		{
			return null;
		}
		FSGConfig fSGConfig = null;
		for (int i = 1; i < m_nearbyFSGs.Count; i++)
		{
			FSGConfig fSGConfig2 = m_nearbyFSGs[0];
			if (fSGConfig2.IsInnkeeper && fSGConfig2.IsSetupComplete)
			{
				return fSGConfig2;
			}
			if (fSGConfig2.FsgId == LastTavernID.Value)
			{
				fSGConfig = fSGConfig2;
			}
		}
		if (fSGConfig == null)
		{
			return m_nearbyFSGs[0];
		}
		return fSGConfig;
	}

	private void NotifyFSGNearbyIfNeeded()
	{
		if (!IsCheckedIn && !m_checkInRequestPending && !AutoCheckInEnabled && !PlayerAccountShouldAutoCheckin.Value && !m_nearbyFSGsFoundEventSent && !m_haltAutoCheckinWhileInnkeeperSetup && m_fsgAvailableToCheckin && !(m_tooltipShowing != null) && m_nearbyFSGs.Count > 0)
		{
			NotifyFSGNearby();
		}
	}

	private void NotifyFSGNearby()
	{
		m_nearbyFSGsFoundEventSent = true;
		ShowNearbyFSGsTooltip();
		if (this.OnNearbyFSGs != null)
		{
			this.OnNearbyFSGs();
		}
	}

	private void ShowNearbyFSGsTooltip()
	{
		ShowTooltip(GameStrings.Get("GLUE_FSG_NEARBY_TOOLTIP"), 6f);
	}

	private void ShowTooltip(string text, float? durationSeconds = 6f)
	{
		Vector3 position = BaseUI.Get().m_BnetBar.m_friendButton.transform.position;
		position += (Vector3)Data.m_nearbyFiresidePopupOffset;
		Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, Vector3.zero, Data.m_nearbyFiresidePopupScale, text);
		Notification.PopUpArrowDirection direction = (UniversalInputManager.UsePhoneUI ? Notification.PopUpArrowDirection.LeftUp : Notification.PopUpArrowDirection.LeftDown);
		notification.ShowPopUpArrow(direction);
		notification.PulseReminderEveryXSeconds(2f);
		notification.transform.position = position;
		notification.transform.localEulerAngles = Data.m_nearbyFiresidePopupRotation;
		SceneUtils.SetLayer(notification.gameObject, GameLayer.BattleNet);
		m_tooltipShowing = notification;
		if (durationSeconds.HasValue)
		{
			Processor.RunCoroutine(Tooltip_End(durationSeconds.Value, notification));
		}
	}

	private IEnumerator Tooltip_End(float secondsTillDeath, Notification notice)
	{
		if (notice == null)
		{
			yield break;
		}
		if (secondsTillDeath > 0f)
		{
			yield return new WaitForSeconds(secondsTillDeath);
		}
		PegUI.OnReleasePreTrigger -= PegUI_OnReleasePreTrigger;
		if (notice != null)
		{
			notice.PlayDeath();
			if (notice == m_tooltipShowing)
			{
				m_tooltipShowing = null;
			}
		}
	}

	public void ShowReturnToFSGSceneTooltip()
	{
		if (Box.Get().IsTransitioningToSceneMode())
		{
			Box.Get().AddTransitionFinishedListener(ShowReturnToFSGSceneTooltipOnTransitionToBoxFinished);
		}
		else
		{
			ShowReturnToFSGSceneTooltipOnTransitionToBoxFinished(null);
		}
	}

	private void ShowReturnToFSGSceneTooltipOnTransitionToBoxFinished(object data)
	{
		Box.Get().RemoveTransitionFinishedListener(ShowReturnToFSGSceneTooltipOnTransitionToBoxFinished);
		if (HasSeenReturnToFSGSceneTooltip)
		{
			SocialToastMgr.Get().AddToast(UserAttentionBlocker.NONE, string.Empty, SocialToastMgr.TOAST_TYPE.FIRESIDE_GATHERING_IS_HERE_REMINDER, playSound: true);
			return;
		}
		HasSeenReturnToFSGSceneTooltip = true;
		ShowTooltip(GameStrings.Get("GLUE_FIRESIDE_GATHERING_RETURN_TO_SCENE_HERE"), null);
		ChatMgr.Get().OnFriendListToggled += ShowReturnToFSGSceneTooltip_OnFriendListToggled_ShowNextTooltip;
		PegUI.OnReleasePreTrigger += PegUI_OnReleasePreTrigger;
	}

	private void ShowReturnToFSGSceneTooltip_OnFriendListToggled_ShowNextTooltip(bool open)
	{
		if (!open)
		{
			return;
		}
		ChatMgr.Get().OnFriendListToggled -= ShowReturnToFSGSceneTooltip_OnFriendListToggled_ShowNextTooltip;
		CloseTooltip();
		if (!IsCheckedIn)
		{
			return;
		}
		Action action = delegate
		{
			FriendListFSGFrame friendListFSGFrame = ChatMgr.Get().FriendListFrame.FindFirstRenderedItem<FriendListFSGFrame>();
			if (!(friendListFSGFrame == null))
			{
				PegUI.OnReleasePreTrigger += PegUI_OnReleasePreTrigger;
				ChatMgr.Get().FriendListFrame.items.Scrolled += CloseTooltip;
				m_tooltipShowing = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, Vector3.zero, Data.m_nearbyFiresidePopupScale, GameStrings.Get("GLUE_FIRESIDE_GATHERING_RETURN_TO_SCENE_HERE"));
				Notification.PopUpArrowDirection direction = Notification.PopUpArrowDirection.Left;
				m_tooltipShowing.ShowPopUpArrow(direction);
				m_tooltipShowing.PulseReminderEveryXSeconds(2f);
				m_tooltipShowing.transform.position = friendListFSGFrame.transform.position + Data.m_returnToFsgFriendListPopupOffset;
				m_tooltipShowing.transform.localEulerAngles = Data.m_nearbyFiresidePopupRotation;
				SceneUtils.SetLayer(m_tooltipShowing.gameObject, GameLayer.BattleNet);
				Processor.RunCoroutine(Tooltip_End(6f, m_tooltipShowing));
			}
		};
		if (ChatMgr.Get().FriendListFrame.IsStarted)
		{
			action();
		}
		else
		{
			ChatMgr.Get().FriendListFrame.OnStarted += action;
		}
	}

	private void CloseTooltip()
	{
		PegUI.OnReleasePreTrigger -= PegUI_OnReleasePreTrigger;
		if (ChatMgr.Get().FriendListFrame != null)
		{
			ChatMgr.Get().FriendListFrame.items.Scrolled -= CloseTooltip;
		}
		if (m_tooltipShowing != null)
		{
			m_tooltipShowing.CloseWithoutAnimation();
		}
	}

	private void PegUI_OnReleasePreTrigger(PegUIElement elem)
	{
		CloseTooltip();
	}

	private void OnFriendListClosed_CloseTooltip(bool opened)
	{
		if (!opened)
		{
			CloseTooltip();
		}
	}

	private void SceneMgr_OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if (prevMode != SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			CloseTooltip();
		}
	}

	private void DoStartAndEndTimingEvents()
	{
		if (m_nearbyFSGs.Count == 0)
		{
			return;
		}
		long unixTimestampSeconds = TimeUtils.UnixTimestampSeconds;
		for (int num = m_nearbyFSGs.Count - 1; num >= 0; num--)
		{
			FSGConfig fSGConfig = m_nearbyFSGs[num];
			if (fSGConfig.UnixEndTimeWithSlush < unixTimestampSeconds)
			{
				m_nearbyFSGs.RemoveAt(num);
				if (fSGConfig == m_currentFSG)
				{
					CheckOutOfFSG();
				}
			}
		}
	}

	private FiresideGatheringSign GenerateCustomTavernSign(int sign, int background, int major, int minor, string tavernName)
	{
		FiresideGatheringSign signObject = GetSignObject(sign);
		Material material = signObject.GetShieldMeshRenderer().GetMaterial();
		signObject.GetComponentInChildren<UberText>().Text = tavernName;
		AssetHandle<Texture> assetHandle = AssetLoader.Get().LoadAsset<Texture>(m_backgroundTextures[background - 1]);
		AssetHandle<Texture> assetHandle2 = AssetLoader.Get().LoadAsset<Texture>(m_majorTextures[major - 1]);
		AssetHandle<Texture> assetHandle3 = AssetLoader.Get().LoadAsset<Texture>(m_minorTextures[minor - 1]);
		material.SetTexture("_BackgroundTex", assetHandle);
		material.SetTexture("_MajorTex", assetHandle2);
		material.SetTexture("_MinorTex", assetHandle3);
		DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
		disposablesCleaner?.Attach(signObject, assetHandle);
		disposablesCleaner?.Attach(signObject, assetHandle2);
		disposablesCleaner?.Attach(signObject, assetHandle3);
		return signObject;
	}

	private void ShowSign(TavernSignData signData, string tavernName, OnCloseSign callback)
	{
		if (m_currentSign != null)
		{
			HideFSGSign();
		}
		ShowSign(signData, tavernName, callback, OnSignAssetLoaded);
	}

	private void ShowSmallSign(TavernSignData signData, string tavernName)
	{
		ShowSign(signData, tavernName, null, OnSmallSignAssetLoaded);
	}

	private void ShowSign(TavernSignData signData, string tavernName, OnCloseSign callback, PrefabCallback<GameObject> onSignAssetLoadedCallback)
	{
		m_currentSignCallback = callback;
		LastSign = signData;
		if (signData.SignType == TavernSignType.TAVERN_SIGN_TYPE_CUSTOM)
		{
			FiresideGatheringSign firesideGatheringSign = GenerateCustomTavernSign(signData.Sign, signData.Background, signData.Major, signData.Minor, tavernName);
			onSignAssetLoadedCallback("", firesideGatheringSign.gameObject, null);
			return;
		}
		FiresideGatheringManagerData.SignTypeMapping signTypeMapping = Data.m_signTypeMapping.Find((FiresideGatheringManagerData.SignTypeMapping x) => x.m_type == signData.SignType);
		if (signTypeMapping == null || signTypeMapping.m_prefabName == null)
		{
			Error.AddDevFatal("FiresideGatheringManager.ShowSign() - unhandled sign type {0}", signData.SignType);
		}
		else
		{
			AssetLoader.Get().InstantiatePrefab(signTypeMapping.m_prefabName, onSignAssetLoadedCallback);
		}
	}

	private void OnSignAssetLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		FiresideGatheringSign component = go.GetComponent<FiresideGatheringSign>();
		if (!(component == null))
		{
			m_currentSign = component;
			component.OnDestroyEvent += OnSignHidden;
			go.transform.localPosition = Data.m_signPosition;
			go.transform.localScale = Data.m_signScale;
			SceneUtils.SetLayer(go, GameLayer.IgnoreFullScreenEffects);
			SoundManager.Get().LoadAndPlay("GVG_sign_enter.prefab:68c9d25c4da293b4dba44c37615c0ae0");
			FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
			fullScreenFXMgr.SetBlurBrightness(1f);
			fullScreenFXMgr.SetBlurDesaturation(0f);
			fullScreenFXMgr.Vignette();
			fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc);
			PlaySignTween(go);
		}
	}

	private void OnSmallSignAssetLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		FiresideGatheringSign component = go.GetComponent<FiresideGatheringSign>();
		if (!(component == null))
		{
			m_currentSign = component;
			if (m_smallSignContainer == null)
			{
				UnityEngine.Object.Destroy(component);
			}
			go.transform.localScale = Data.m_signScale;
			go.transform.SetParent(m_smallSignContainer, worldPositionStays: false);
			go.transform.localPosition = Vector3.zero;
			SceneUtils.SetLayer(go, GameLayer.Default);
			component.SetSignShadowEnabled(enabled: true);
		}
	}

	private FiresideGatheringSign GetSignObject(int signIndex)
	{
		if (signIndex < 1 || signIndex > 8)
		{
			Log.FiresideGatherings.PrintError("FiresideGatheringManager.GetSignObject passed an invalid sign index: {0}. Using default of 1", signIndex);
			signIndex = 1;
		}
		FiresideGatheringSign component = ((GameObject)GameUtils.InstantiateGameObject(m_tavernSignAsset.ToString())).GetComponent<FiresideGatheringSign>();
		GameObject gameObject = (GameObject)GameUtils.InstantiateGameObject(m_fsgShields[signIndex - 1].ToString());
		GameUtils.SetParent(gameObject, component.m_shieldContainer);
		component.SetSignShield(gameObject.GetComponentInChildren<FiresideGatheringSignShield>());
		return component;
	}

	private void PlaySignTween(GameObject signObject)
	{
		Hashtable hashtable = iTween.Hash("sign", signObject);
		Action<object> action = delegate(object e)
		{
			PlaySignAnimation(e);
		};
		Hashtable args = iTween.Hash("scale", new Vector3(0.01f, 0.01f, 0.01f), "time", 0.25f, "oncomplete", action, "oncompleteparams", hashtable);
		iTween.ScaleFrom(signObject, args);
		Processor.RunCoroutine(CreateSignInputBlocker(signObject));
	}

	private void PlaySignAnimation(object args)
	{
		Animator componentInChildren = ((GameObject)((Hashtable)args)["sign"]).GetComponentInChildren<Animator>();
		componentInChildren.enabled = true;
		componentInChildren.Play("FSG_SignSwing");
		if (this.OnSignShown != null)
		{
			this.OnSignShown();
		}
	}

	private IEnumerator CreateSignInputBlocker(GameObject signObject)
	{
		Camera camera = CameraUtils.FindFirstByLayer(GameLayer.UI);
		GameObject inputBlockerObject = CameraUtils.CreateInputBlocker(camera, "FSGSign");
		inputBlockerObject.transform.parent = signObject.transform;
		inputBlockerObject.transform.localPosition = new Vector3(0f, 1f, 0f);
		PegUIElement fsgSignBlocker = inputBlockerObject.AddComponent<PegUIElement>();
		yield return new WaitForSeconds(2f);
		fsgSignBlocker.AddEventListener(UIEventType.RELEASE, delegate
		{
			HideFSGSign();
			UnityEngine.Object.Destroy(inputBlockerObject);
		});
	}

	private void HideFSGSign(bool hideImmediately = false)
	{
		if (m_currentSign == null)
		{
			OnSignHidden();
			return;
		}
		m_currentSign.gameObject.SetActive(!hideImmediately);
		m_currentSign.m_fxMotes.gameObject.SetActive(value: false);
		if (!hideImmediately)
		{
			SoundManager.Get().LoadAndPlay("GVG_sign_exit.prefab:697b23cceecfd154dacf14bc58b75af2");
		}
		HideSignAnim(m_currentSign.gameObject);
	}

	public void OnTavernSignAnimationComplete()
	{
		if (m_currentSign != null)
		{
			m_currentSign.UnregisterSignSocketAnimationCompleteListener(OnTavernSignAnimationComplete);
		}
		if (this.OnSignClosed != null)
		{
			this.OnSignClosed();
		}
	}

	private void HideSignAnim(GameObject sign)
	{
		Animator componentInChildren = sign.GetComponentInChildren<Animator>();
		componentInChildren.enabled = true;
		componentInChildren.Play(UniversalInputManager.UsePhoneUI ? "FSG_SignSocketIn_phone" : "FSG_SignSocketIn");
		SceneUtils.SetLayer(sign, GameLayer.Default);
		OnSignHidden();
		sign.GetComponent<FiresideGatheringSign>().RegisterSignSocketAnimationCompleteListener(OnTavernSignAnimationComplete);
	}

	private void OnSignHidden()
	{
		m_currentSign = null;
		if (m_currentSignCallback != null)
		{
			m_currentSignCallback();
			m_currentSignCallback = null;
		}
		HideBlur();
	}

	private void HideBlur()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr != null)
		{
			fullScreenFXMgr.StopVignette();
			fullScreenFXMgr.StopBlur();
		}
	}

	private void OnJoinFSGDialogResponse(bool joinFSG)
	{
		if (!joinFSG)
		{
			PlayerAccountShouldAutoCheckin.Set(newValue: false);
			return;
		}
		FSGConfig preferredFSG = GetPreferredFSG();
		if (preferredFSG != null)
		{
			CheckInToFSG(preferredFSG.FsgId);
			if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			}
		}
	}

	private void OnFindEventDialogCallBack(bool searchForGatherings)
	{
		if (searchForGatherings)
		{
			HasManuallyInitiatedFSGScanBefore.Set(newValue: true);
			if (!ClientLocationManager.Get().GPSOrWifiServicesAvailable)
			{
				ShowNoGPSOrWifiAlertPopup();
			}
			else
			{
				DialogManager.Get().ShowFiresideGatheringLocationHelperDialog(null);
			}
		}
		else
		{
			GotoFSGLink();
		}
	}

	private void ShowNoGPSOrWifiAlertPopup()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_FIRESIDE_GATHERING");
		popupInfo.m_text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_SCAN_NO_GPS_OR_WIFI");
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void OnFailedToFindFSGDialogResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM)
		{
			GotoFSGLink();
		}
	}

	private void ShowFiresideGatheringInnkeeperSetup_OnResponse(bool doSetup)
	{
		m_haltAutoCheckinWhileInnkeeperSetup = false;
		if (doSetup)
		{
			DialogManager.Get().ShowFiresideGatheringInnkeeperSetupHelperDialog(null);
		}
		else
		{
			PlayerAccountShouldAutoCheckin.Set(newValue: false);
		}
	}

	private void OnRequestNearbyFSGsResponse()
	{
		m_isRequestNearbyFSGsPending = false;
		RequestNearbyFSGsResponse response = Network.Get().GetRequestNearbyFSGsResponse();
		if (response.ErrorCode != 0)
		{
			Log.FiresideGatherings.PrintError("NearbyFSGsResponse: code={0} {1} fsgCount={2}", (int)response.ErrorCode, response.ErrorCode, response.FSGs.Count);
			if (this.OnNearbyFSGsChanged != null)
			{
				this.OnNearbyFSGsChanged();
			}
			return;
		}
		Log.FiresideGatherings.Print("NearbyFSGsResponse: code={0} {1} fsgCount={2}", (int)response.ErrorCode, response.ErrorCode, response.FSGs.Count);
		m_nearbyFSGs.Clear();
		m_innkeeperFSG = null;
		m_fsgAvailableToCheckin = false;
		for (int i = 0; i < response.FSGs.Count; i++)
		{
			FSGConfig fSGConfig = response.FSGs[i];
			m_nearbyFSGs.Add(fSGConfig);
			if (fSGConfig.IsInnkeeper)
			{
				m_innkeeperFSG = fSGConfig;
			}
			else
			{
				m_fsgAvailableToCheckin = true;
			}
			AddKnownInnkeeper(fSGConfig.FsgId, fSGConfig.FsgInnkeeperAccountId);
		}
		if (response.HasCheckedInFsgId)
		{
			FSGConfig fSGConfig2 = m_nearbyFSGs.FirstOrDefault((FSGConfig fsg) => fsg.FsgId == response.CheckedInFsgId);
			if (fSGConfig2 == null)
			{
				Log.FiresideGatherings.PrintError("NearbyFSGsResponse: Error: already checked into FSG (id={0}) but no corresponding FSGConfig found in nearby list - ignoring. patronCount={1}", response.CheckedInFsgId, response.FsgAttendees.Count);
			}
			else
			{
				Log.FiresideGatherings.Print("NearbyFSGsResponse: already checked into {0}-{1}, showing FSG UI. patronCount={2}", response.CheckedInFsgId, string.IsNullOrEmpty(fSGConfig2.TavernName) ? "<no name>" : fSGConfig2.TavernName, response.FsgAttendees.Count);
				JoinFSG(response.CheckedInFsgId, response.FsgAttendees, response.FsgSharedSecretKey, response.InnkeeperSelectedBrawlLibraryItemId);
			}
		}
		if (this.OnNearbyFSGsChanged != null)
		{
			this.OnNearbyFSGsChanged();
		}
	}

	private void OnCheckInToFSGResponse()
	{
		m_checkInRequestPending = false;
		CheckInToFSGResponse checkInToFSGResponse = Network.Get().GetCheckInToFSGResponse();
		if (checkInToFSGResponse.ErrorCode != 0 && checkInToFSGResponse.ErrorCode != ErrorCode.ERROR_FSG_ALREADY_CHECKED_IN_FETCH_FSG_INFO)
		{
			Log.FiresideGatherings.PrintError("CheckInResponse: code={0} {1} fsgId={2} patronCount={3}", (int)checkInToFSGResponse.ErrorCode, checkInToFSGResponse.ErrorCode, checkInToFSGResponse.FsgId, checkInToFSGResponse.FsgAttendees.Count);
			m_errorOccuredOnCheckin = true;
		}
		else
		{
			Log.FiresideGatherings.Print("CheckInResponse: code={0} {1} fsgId={2} patronCount={3}", (int)checkInToFSGResponse.ErrorCode, checkInToFSGResponse.ErrorCode, checkInToFSGResponse.FsgId, (checkInToFSGResponse.FsgAttendees == null) ? "null" : checkInToFSGResponse.FsgAttendees.Count.ToString());
			FriendChallengeMgr.Get().UpdateMyFsgSharedSecret(checkInToFSGResponse.FsgSharedSecretKey);
			JoinFSG(checkInToFSGResponse.FsgId, checkInToFSGResponse.FsgAttendees, checkInToFSGResponse.FsgSharedSecretKey, checkInToFSGResponse.InnkeeperSelectedBrawlLibraryItemId);
		}
	}

	private void OnCheckOutOfFSGResponse()
	{
		CheckOutOfFSGResponse checkOutOfFSGResponse = Network.Get().GetCheckOutOfFSGResponse();
		if (checkOutOfFSGResponse.ErrorCode != 0)
		{
			Log.FiresideGatherings.PrintError("CheckOutResponse: code={0} {1} fsgId={2}", (int)checkOutOfFSGResponse.ErrorCode, checkOutOfFSGResponse.ErrorCode, checkOutOfFSGResponse.FsgId);
		}
		else
		{
			Log.FiresideGatherings.Print("CheckOutResponse: code={0} {1} fsgId={2}", (int)checkOutOfFSGResponse.ErrorCode, checkOutOfFSGResponse.ErrorCode, checkOutOfFSGResponse.FsgId);
			FriendChallengeMgr.Get().UpdateMyFsgSharedSecret(null);
			LeaveFSG();
		}
	}

	private void CheckCanBeginLocationDataGatheringForLogin()
	{
		if (!m_hasBegunLocationDataGatheringForLogin)
		{
			Log.FiresideGatherings.PrintDebug("FiresideGatheringManager.CheckCanBeginLocationGathering");
			if (s_guardianVars.Value == null)
			{
				Log.FiresideGatherings.PrintDebug("FiresideGatheringManager.CheckCanBeginLocationGathering NO GUARDIAN");
			}
			else if (s_fsgFeaturesConfig.Value == null)
			{
				Log.FiresideGatherings.PrintDebug("FiresideGatheringManager.CheckCanBeginLocationGathering NO FEATURE CONFIG");
			}
			else if (s_profileProgress.Value == null)
			{
				Log.FiresideGatherings.PrintDebug("FiresideGatheringManager.CheckCanBeginLocationGathering NO PROFILE PROGRESS");
			}
			else if (s_clientOptions.Value == null)
			{
				Log.FiresideGatherings.PrintDebug("FiresideGatheringManager.CheckCanBeginLocationGathering NO CLIENT OPTIONS");
			}
			else
			{
				BeginLocationDataGatheringForLogin();
			}
		}
	}

	private void OnNetCache_GuardianVars()
	{
		NetCache.NetCacheFeatures value = s_guardianVars.Value;
		if (value.FSGEnabled != s_cacheFSGEnabled)
		{
			if (!value.FSGEnabled && IsCheckedIn)
			{
				CheckOutOfFSG();
				LeaveFSG();
				m_nearbyFSGs.Clear();
			}
			s_cacheFSGEnabled = value.FSGEnabled;
			CheckCanBeginLocationDataGatheringForLogin();
		}
	}

	private void OnNetCache_FSGFeatureConfig()
	{
		FSGFeatureConfig value = s_fsgFeaturesConfig.Value;
		if (value.Gps != s_cacheGPSEnabled || value.Wifi != s_cacheWifiEnabled)
		{
			s_cacheGPSEnabled = value.Gps;
			s_cacheWifiEnabled = value.Wifi;
			CheckCanBeginLocationDataGatheringForLogin();
		}
	}

	private void RebuildKnownPatronsFromPresence()
	{
		m_knownPatronsFromPresence.Clear();
		if (!IsCheckedIn)
		{
			return;
		}
		long num = ((m_currentFSG == null) ? 0 : m_currentFSG.FsgId);
		foreach (BnetPlayer friend in BnetFriendMgr.Get().GetFriends())
		{
			FiresideGatheringInfo playerFSGInfo = GetPlayerFSGInfo(friend);
			if (playerFSGInfo != null && playerFSGInfo.FsgId == num)
			{
				AddKnownPatronFromPresence(friend);
			}
		}
	}

	private void AddKnownPatronFromPresence(BnetPlayer player)
	{
		if (!m_knownPatronsFromPresence.Contains(player))
		{
			AddKnownPatron(BnetUtils.CreatePegasusBnetId(player.GetAccountId()), BnetUtils.CreatePegasusBnetId(player.GetHearthstoneGameAccountId()), isKnownFromPresence: true, out var _);
		}
	}

	private void PlayersPresenceChanged(BnetPlayerChangelist changelist, out List<BnetPlayer> addedToDisplayablePatronList, out List<BnetPlayer> removedFromDisplayablePatronList)
	{
		addedToDisplayablePatronList = null;
		removedFromDisplayablePatronList = null;
		List<BnetPlayer> list = new List<BnetPlayer>();
		BnetAccountId accountId = BnetPresenceMgr.Get().GetMyPlayer().GetAccountId();
		foreach (BnetPlayer pendingPatron in m_pendingPatrons)
		{
			if (!(pendingPatron.GetAccountId() != accountId) || !FiresideGatheringPresenceManager.IsDisplayable(pendingPatron) || IsPlayerInMyFSGAndDisplayable(pendingPatron))
			{
				continue;
			}
			bool num = m_displayablePatrons.Add(pendingPatron);
			list.Add(pendingPatron);
			if (num)
			{
				if (addedToDisplayablePatronList == null)
				{
					addedToDisplayablePatronList = new List<BnetPlayer>();
				}
				addedToDisplayablePatronList.Add(pendingPatron);
			}
		}
		foreach (BnetPlayer item in list)
		{
			m_pendingPatrons.Remove(item);
		}
		list.Clear();
		long num2 = ((m_currentFSG == null) ? 0 : m_currentFSG.FsgId);
		List<BnetPlayerChange> list2 = changelist?.GetChanges();
		if (list2 == null)
		{
			return;
		}
		for (int i = 0; i < list2.Count; i++)
		{
			BnetPlayerChange bnetPlayerChange = list2[i];
			BnetPlayer newPlayer = bnetPlayerChange.GetNewPlayer();
			bool num3 = newPlayer.GetAccountId() == accountId;
			if (!num3)
			{
				bool flag = false;
				FiresideGatheringInfo playerFSGInfo = GetPlayerFSGInfo(newPlayer);
				if (playerFSGInfo != null && playerFSGInfo.FsgId == num2)
				{
					AddKnownPatronFromPresence(newPlayer);
				}
				else if (m_knownPatronsFromPresence.Contains(newPlayer))
				{
					if (!m_knownPatronsFromServer.Contains(newPlayer))
					{
						flag = m_displayablePatrons.Remove(newPlayer) || flag;
						m_pendingPatrons.Remove(newPlayer);
					}
					m_knownPatronsFromPresence.Remove(newPlayer);
				}
				if (IsPlayerInMyFSGAndDisplayable(newPlayer) && !FiresideGatheringPresenceManager.IsDisplayable(newPlayer))
				{
					flag = m_displayablePatrons.Remove(newPlayer) || flag;
					m_pendingPatrons.Add(newPlayer);
				}
				if (flag)
				{
					if (removedFromDisplayablePatronList == null)
					{
						removedFromDisplayablePatronList = new List<BnetPlayer>();
					}
					removedFromDisplayablePatronList.Add(newPlayer);
				}
			}
			if (num3 && !bnetPlayerChange.GetOldPlayer().IsAppearingOffline() && bnetPlayerChange.GetNewPlayer().IsAppearingOffline() && IsCheckedIn)
			{
				PromptPlayerToAppearOnline(CurrentFsgId);
			}
		}
	}

	private void OnInnkeeperSetupGatheringResponse()
	{
		InnkeeperSetupGatheringResponse innkeeperSetupGatheringResponse = Network.Get().GetInnkeeperSetupGatheringResponse();
		bool flag = true;
		if (innkeeperSetupGatheringResponse.ErrorCode != 0)
		{
			Log.FiresideGatherings.PrintError("InnkeeperSetupResponse: code={0} {1} fsgId={2}", (int)innkeeperSetupGatheringResponse.ErrorCode, innkeeperSetupGatheringResponse.ErrorCode, innkeeperSetupGatheringResponse.FsgId);
			flag = false;
		}
		Log.FiresideGatherings.Print("InnkeeperSetupResponse: code={0} {1} fsgId={2}", (int)innkeeperSetupGatheringResponse.ErrorCode, innkeeperSetupGatheringResponse.ErrorCode, innkeeperSetupGatheringResponse.FsgId);
		if (flag)
		{
			m_innkeeperFSG.IsSetupComplete = true;
		}
		if (this.OnInnkeeperSetupFinished != null)
		{
			this.OnInnkeeperSetupFinished(flag);
		}
		if (flag)
		{
			CheckInToFSG(m_innkeeperFSG.FsgId);
		}
	}

	private BnetPlayer AddKnownPatron(FSGPatron patron, bool isKnownFromPresence)
	{
		bool isNewDisplayablePatron;
		return AddKnownPatron(patron.BnetAccount, patron.GameAccount, isKnownFromPresence, out isNewDisplayablePatron);
	}

	private BnetPlayer AddKnownPatron(BnetId bnetAccountId, BnetId gameAccountId, bool isKnownFromPresence, out bool isNewDisplayablePatron)
	{
		isNewDisplayablePatron = false;
		BnetAccountId accountId = BnetPresenceMgr.Get().GetMyPlayer().GetAccountId();
		if (bnetAccountId.Lo == accountId.GetLo())
		{
			return null;
		}
		BnetAccountId accountId2 = BnetAccountId.CreateFromNet(bnetAccountId);
		BnetPlayer bnetPlayer = BnetPresenceMgr.Get().RegisterPlayer(BnetPlayerSource.FSG_PATRON, accountId2, BnetGameAccountId.CreateFromNet(gameAccountId), BnetProgramId.HEARTHSTONE);
		if (bnetPlayer == null)
		{
			return null;
		}
		if (m_displayablePatrons.Contains(bnetPlayer))
		{
			isNewDisplayablePatron = false;
		}
		else if (FiresideGatheringPresenceManager.IsDisplayable(bnetPlayer))
		{
			m_displayablePatrons.Add(bnetPlayer);
			isNewDisplayablePatron = true;
			m_pendingPatrons.Remove(bnetPlayer);
		}
		else
		{
			m_pendingPatrons.Add(bnetPlayer);
			isNewDisplayablePatron = false;
		}
		if (isKnownFromPresence)
		{
			m_knownPatronsFromPresence.Add(bnetPlayer);
		}
		else
		{
			m_knownPatronsFromServer.Add(bnetPlayer);
		}
		return bnetPlayer;
	}

	private void AddKnownInnkeeper(long fsgId, BnetId bnetAccountId)
	{
		if (bnetAccountId == null)
		{
			return;
		}
		BnetAccountId bnetAccountId2 = BnetAccountId.CreateFromNet(bnetAccountId);
		BnetPlayer bnetPlayer = BnetPresenceMgr.Get().RegisterPlayer(BnetPlayerSource.FSG_PATRON, bnetAccountId2, null, BnetProgramId.HEARTHSTONE);
		if (bnetPlayer != null)
		{
			if (!m_innkeepers.ContainsKey(fsgId))
			{
				m_innkeepers.Add(fsgId, bnetPlayer);
			}
			BnetAccountId accountId = BnetPresenceMgr.Get().GetMyPlayer().GetAccountId();
			if (bnetAccountId.Lo != accountId.GetLo())
			{
				BnetPresenceMgr.RequestPlayerBattleTag(bnetAccountId2);
			}
		}
	}

	private void OnPatronListUpdateReceivedFromServer()
	{
		if (m_currentFSG == null || CurrentFsgIsLargeScale)
		{
			return;
		}
		bool isAppendingPatronList = m_isAppendingPatronList;
		m_isAppendingPatronList = true;
		FSGPatronListUpdate fSGPatronListUpdate = Network.Get().GetFSGPatronListUpdate();
		ulong myselfGameAccountLo = BnetPresenceMgr.Get().GetMyPlayer().GetBestGameAccountId()
			.GetLo();
		fSGPatronListUpdate.AddedPatrons.RemoveAll((FSGPatron patron) => myselfGameAccountLo == patron.GameAccount.Lo);
		List<BnetPlayer> list = null;
		List<BnetPlayer> list2 = null;
		foreach (FSGPatron addedPatron in fSGPatronListUpdate.AddedPatrons)
		{
			bool isNewDisplayablePatron;
			BnetPlayer item = AddKnownPatron(addedPatron.BnetAccount, addedPatron.GameAccount, isKnownFromPresence: false, out isNewDisplayablePatron);
			if (isNewDisplayablePatron)
			{
				if (list == null)
				{
					list = new List<BnetPlayer>();
				}
				list.Add(item);
			}
		}
		foreach (FSGPatron removedPatron in fSGPatronListUpdate.RemovedPatrons)
		{
			BnetGameAccountId id = BnetGameAccountId.CreateFromNet(removedPatron.GameAccount);
			BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(id);
			m_knownPatronsFromServer.Remove(player);
			bool flag = false;
			if (!m_knownPatronsFromPresence.Contains(player))
			{
				flag = m_displayablePatrons.Remove(player);
				m_pendingPatrons.Remove(player);
			}
			if (flag)
			{
				if (list2 == null)
				{
					list2 = new List<BnetPlayer>();
				}
				list2.Add(player);
			}
		}
		FiresideGatheringPresenceManager.Get().AddRemovePatronSubscriptions(fSGPatronListUpdate.AddedPatrons, fSGPatronListUpdate.RemovedPatrons);
		m_isAppendingPatronList = isAppendingPatronList;
		if (FiresideGatheringManager.OnPatronListUpdated != null)
		{
			FiresideGatheringManager.OnPatronListUpdated(list, list2);
		}
	}

	private void OnPlayersPresenceChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (IsCheckedIn && !m_isAppendingPatronList)
		{
			PlayersPresenceChanged(changelist, out var addedToDisplayablePatronList, out var removedFromDisplayablePatronList);
			FiresideGatheringPresenceManager.Get().CheckForMoreSubscribeOpportunities(removedFromDisplayablePatronList, m_pendingPatrons);
			if (FiresideGatheringManager.OnPatronListUpdated != null)
			{
				FiresideGatheringManager.OnPatronListUpdated(addedToDisplayablePatronList, removedFromDisplayablePatronList);
			}
		}
	}

	private void PeriodicCheckForMoreSubscribeOpportunities(object userData)
	{
		if (IsCheckedIn && !HearthstoneApplication.Get().IsResetting() && !HearthstoneApplication.Get().IsExiting())
		{
			FiresideGatheringPresenceManager.Get().CheckForMoreSubscribeOpportunities(null, m_pendingPatrons);
			Processor.ScheduleCallback(FiresideGatheringPresenceManager.PERIODIC_SUBSCRIBE_CHECK_SECONDS, realTime: true, PeriodicCheckForMoreSubscribeOpportunities);
		}
	}

	private void InitializeTransitionInputBlocker(bool enabled)
	{
		if (m_transitionInputBlocker == null)
		{
			Camera camera = CameraUtils.FindFirstByLayer(GameLayer.BattleNetDialog);
			m_transitionInputBlocker = CameraUtils.CreateInputBlocker(camera, "FSGTransitionInputBlocker");
			m_transitionInputBlocker.transform.SetParent(SceneObject.transform);
			TransformUtil.SetPosZ(m_transitionInputBlocker, 1f);
			m_transitionInputBlocker.gameObject.SetActive(enabled);
		}
	}

	private static FiresideGatheringInfo GetPlayerFSGInfo(BnetPlayer player)
	{
		BnetGameAccount bnetGameAccount = player?.GetHearthstoneGameAccount();
		if (bnetGameAccount == null)
		{
			return null;
		}
		byte[] gameFieldBytes = bnetGameAccount.GetGameFieldBytes(25u);
		if (gameFieldBytes != null && gameFieldBytes.Length != 0)
		{
			return ProtobufUtil.ParseFrom<FiresideGatheringInfo>(gameFieldBytes);
		}
		return null;
	}

	private FiresideGatheringInfo GetMyFSGInfoForPresence()
	{
		if (m_currentFSG == null)
		{
			return null;
		}
		return new FiresideGatheringInfo
		{
			FsgId = m_currentFSG.FsgId
		};
	}

	private void UpdateMyPresence()
	{
		FiresideGatheringInfo myFSGInfoForPresence = GetMyFSGInfoForPresence();
		BnetPresenceMgr.Get().SetGameFieldBlob(25u, myFSGInfoForPresence);
	}

	public void Cheat_CheckInToFakeFSG(FSGConfig fsg)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			m_cachedFakeCheatFsg = fsg;
			Network.Get().RegisterNetHandler(TavernBrawlInfo.PacketID.ID, Cheat_OnTavernBrawlInfoCheckInToFakeFSG);
			Network.Get().RequestTavernBrawlInfo(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
		}
	}

	private void Cheat_OnTavernBrawlInfoCheckInToFakeFSG()
	{
		if (!HearthstoneApplication.IsPublic())
		{
			Network.Get().RemoveNetHandler(TavernBrawlInfo.PacketID.ID, Cheat_OnTavernBrawlInfoCheckInToFakeFSG);
			CheckInToFSGResponse checkInToFSGResponse = new CheckInToFSGResponse();
			checkInToFSGResponse.ErrorCode = ErrorCode.ERROR_OK;
			checkInToFSGResponse.PlayerRecord = new TavernBrawlPlayerRecord();
			checkInToFSGResponse.PlayerRecord.SessionStatus = TavernBrawlStatus.TB_STATUS_ACTIVE;
			checkInToFSGResponse.FsgId = m_cachedFakeCheatFsg.FsgId;
			PegasusPacket packet = new PegasusPacket(505, 0, checkInToFSGResponse);
			Network.Get().SimulateReceivedPacketFromServer(packet);
			m_cachedFakeCheatFsg = null;
		}
	}

	public void Cheat_CheckInToFakeFSG()
	{
		if (!HearthstoneApplication.IsPublic())
		{
			FSGConfig fSGConfig = new FSGConfig();
			fSGConfig.FsgId = -1L;
			fSGConfig.TavernName = "Fake Gathering";
			fSGConfig.UnixOfficialStartTime = TimeUtils.UnixTimestampSeconds - 7200;
			fSGConfig.UnixOfficialEndTime = TimeUtils.UnixTimestampSeconds + 14400;
			fSGConfig.UnixStartTimeWithSlush = fSGConfig.UnixOfficialStartTime - 28800;
			fSGConfig.UnixEndTimeWithSlush = fSGConfig.UnixOfficialEndTime + 28800;
			Cheat_CheckInToFakeFSG(fSGConfig);
		}
	}

	public void Cheat_CheckOutOfFakeFSG()
	{
		if (!HearthstoneApplication.IsPublic())
		{
			CheckOutOfFSGResponse checkOutOfFSGResponse = new CheckOutOfFSGResponse();
			checkOutOfFSGResponse.ErrorCode = ErrorCode.ERROR_OK;
			PegasusPacket packet = new PegasusPacket(506, 0, checkOutOfFSGResponse);
			Network.Get().SimulateReceivedPacketFromServer(packet);
		}
	}

	public void Cheat_NearbyFSGNotice()
	{
		NotifyFSGNearby();
	}

	public void Cheat_CreateFakeGatherings(int numGatherings, bool innkeeper = false)
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		RequestNearbyFSGsResponse requestNearbyFSGsResponse = new RequestNearbyFSGsResponse();
		requestNearbyFSGsResponse.ErrorCode = ErrorCode.ERROR_OK;
		requestNearbyFSGsResponse.FSGs = new List<FSGConfig>();
		for (int i = 0; i < numGatherings; i++)
		{
			FSGConfig fSGConfig = new FSGConfig();
			fSGConfig.FsgId = -i - 2;
			fSGConfig.TavernName = "Fake Gathering " + i;
			fSGConfig.UnixOfficialStartTime = TimeUtils.UnixTimestampSeconds - 7200;
			fSGConfig.UnixOfficialEndTime = TimeUtils.UnixTimestampSeconds + 14400;
			fSGConfig.UnixStartTimeWithSlush = fSGConfig.UnixOfficialStartTime - 28800;
			fSGConfig.UnixEndTimeWithSlush = fSGConfig.UnixOfficialEndTime + 28800;
			TavernSignData tavernSignData = new TavernSignData();
			tavernSignData.Sign = UnityEngine.Random.Range(1, 8);
			tavernSignData.Background = UnityEngine.Random.Range(1, 15);
			tavernSignData.Major = UnityEngine.Random.Range(1, 85);
			tavernSignData.Minor = UnityEngine.Random.Range(1, 43);
			tavernSignData.SignType = TavernSignType.TAVERN_SIGN_TYPE_CUSTOM;
			fSGConfig.SignData = tavernSignData;
			if (innkeeper && i == 0)
			{
				fSGConfig.IsInnkeeper = true;
				fSGConfig.IsSetupComplete = false;
			}
			requestNearbyFSGsResponse.FSGs.Add(fSGConfig);
		}
		PegasusPacket packet = new PegasusPacket(504, 0, requestNearbyFSGsResponse);
		Network.Get().SimulateReceivedPacketFromServer(packet);
	}

	public void Cheat_RemoveFakeGatherings()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		RequestNearbyFSGsResponse requestNearbyFSGsResponse = new RequestNearbyFSGsResponse();
		requestNearbyFSGsResponse.ErrorCode = ErrorCode.ERROR_OK;
		requestNearbyFSGsResponse.FSGs = new List<FSGConfig>();
		foreach (FSGConfig nearbyFSG in m_nearbyFSGs)
		{
			if (nearbyFSG.FsgId >= 0)
			{
				requestNearbyFSGsResponse.FSGs.Add(nearbyFSG);
			}
		}
		PegasusPacket packet = new PegasusPacket(504, 0, requestNearbyFSGsResponse);
		Network.Get().SimulateReceivedPacketFromServer(packet);
	}

	public void Cheat_MockInnkeeperSetup()
	{
		if (!HearthstoneApplication.IsPublic())
		{
			InnkeeperSetupGatheringResponse innkeeperSetupGatheringResponse = new InnkeeperSetupGatheringResponse();
			innkeeperSetupGatheringResponse.ErrorCode = ErrorCode.ERROR_OK;
			innkeeperSetupGatheringResponse.FsgId = m_innkeeperFSG.FsgId;
			PegasusPacket packet = new PegasusPacket(508, 0, innkeeperSetupGatheringResponse);
			Network.Get().SimulateReceivedPacketFromServer(packet);
		}
	}

	public void Cheat_ShowSign(TavernSignType type, int sign, int background, int major, int minor, string tavernName)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			TavernSignData tavernSignData = new TavernSignData();
			tavernSignData.SignType = type;
			tavernSignData.Sign = sign;
			tavernSignData.Background = background;
			tavernSignData.Major = major;
			tavernSignData.Minor = minor;
			ShowSign(tavernSignData, tavernName, null);
		}
	}

	public void Cheat_GPSOffset(double offset)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			m_gpsCheatOffset = offset;
		}
	}

	public void Cheat_GPSSet(double latitude, double longitude)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			m_gpsCheatingLocation = true;
			m_gpsCheatLatitude = latitude;
			m_gpsCheatLongitude = longitude;
		}
	}

	public void Cheat_ResetGPSCheating()
	{
		if (!HearthstoneApplication.IsPublic())
		{
			m_gpsCheatingLocation = false;
			m_gpsCheatLatitude = 0.0;
			m_gpsCheatLongitude = 0.0;
			m_gpsCheatOffset = 0.0;
		}
	}

	public void Cheat_GetGPSCheats(out bool isCheatingGPS, out double latitude, out double longitude, out double offset)
	{
		isCheatingGPS = m_gpsCheatingLocation;
		latitude = m_gpsCheatLatitude;
		longitude = m_gpsCheatLongitude;
		offset = m_gpsCheatOffset;
	}

	public void Cheat_ToggleLargeScaleFSG()
	{
		if (m_currentFSG != null)
		{
			m_currentFSG.IsLargeScaleFsg = !m_currentFSG.IsLargeScaleFsg;
			if (this.OnJoinFSG != null)
			{
				this.OnJoinFSG(m_currentFSG);
			}
		}
	}

	public BnetPlayer Cheat_CreateFSGPatron(string fullName, int leagueId, int starLevel, BnetProgramId programId, bool isFriend, bool isOnline)
	{
		BnetPlayer bnetPlayer = BnetFriendMgr.Get().Cheat_CreatePlayer(fullName, leagueId, starLevel, programId, isFriend, isOnline);
		m_displayablePatrons.Add(bnetPlayer);
		return bnetPlayer;
	}

	public int Cheat_RemoveCheatFriends()
	{
		return m_displayablePatrons.RemoveWhere((BnetPlayer player) => player.IsCheatPlayer);
	}
}
