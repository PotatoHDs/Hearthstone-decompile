using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
	public enum PopupTextType
	{
		BASIC,
		FANCY
	}

	public enum VisualEmoteType
	{
		NONE,
		HOT_STREAK,
		TRIPLE,
		TECH_UP_01,
		TECH_UP_02,
		TECH_UP_03,
		TECH_UP_04,
		TECH_UP_05,
		TECH_UP_06,
		BATTLEGROUNDS_01,
		BATTLEGROUNDS_02,
		BATTLEGROUNDS_03,
		BATTLEGROUNDS_04,
		BATTLEGROUNDS_05,
		BATTLEGROUNDS_06,
		BANANA
	}

	public class SpeechBubbleOptions
	{
		public string speechText = "";

		public Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.BottomLeft;

		public Actor actor;

		public bool destroyWhenNewCreated = true;

		public bool parentToActor = true;

		public float bubbleScale;

		public VisualEmoteType visualEmoteType;

		public int speechBubbleGroup;

		public Action<int> finishCallback;

		public float emoteDuration;

		public SpeechBubbleOptions WithSpeechText(string speechText)
		{
			this.speechText = speechText;
			return this;
		}

		public SpeechBubbleOptions WithSpeechBubbleDirection(Notification.SpeechBubbleDirection direction)
		{
			this.direction = direction;
			return this;
		}

		public SpeechBubbleOptions WithActor(Actor actor)
		{
			this.actor = actor;
			return this;
		}

		public SpeechBubbleOptions WithParentToActor(bool parentToActor)
		{
			this.parentToActor = parentToActor;
			return this;
		}

		public SpeechBubbleOptions WithDestroyWhenNewCreated(bool destroyWhenNewCreated)
		{
			this.destroyWhenNewCreated = destroyWhenNewCreated;
			return this;
		}

		public SpeechBubbleOptions WithBubbleScale(float bubbleScale)
		{
			this.bubbleScale = bubbleScale;
			return this;
		}

		public SpeechBubbleOptions WithVisualEmoteType(VisualEmoteType visualEmoteType)
		{
			this.visualEmoteType = visualEmoteType;
			return this;
		}

		public SpeechBubbleOptions WithSpeechBubbleGroup(int speechBubbleGroup)
		{
			this.speechBubbleGroup = speechBubbleGroup;
			return this;
		}

		public SpeechBubbleOptions WithFinishCallback(Action<int> finishCallback)
		{
			this.finishCallback = finishCallback;
			return this;
		}

		public SpeechBubbleOptions WithEmoteDuration(float emoteDuration)
		{
			this.emoteDuration = emoteDuration;
			return this;
		}
	}

	private class QuoteSoundCallbackData
	{
		public Notification m_quote;

		public float m_durationSeconds;

		public bool m_persistCharacter;
	}

	public const string KT_PREFAB_PATH = "KT_Quote.prefab:7ad118a1a10e9ab409ade82268a378f5";

	public const string TIRION_PREFAB_PATH = "Tirion_Quote.prefab:2f88f08e8896841429c972fc5c4c7088";

	public const string NORMAL_NEFARIAN_PREFAB_PATH = "NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913";

	public const string ZOMBIE_NEFARIAN_PREFAB_PATH = "NefarianDragon_Quote.prefab:179fec888df7e4c02b8de3b7ad109a23";

	public const string RAGNAROS_PREFAB_PATH = "Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51";

	public const string MAJORDOMO_PREFAB_PATH = "Majordomo_Quote.prefab:72286f87e5b724c21aa1d92d04426614";

	public const string RENO_PREFAB_PATH = "Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a";

	public const string RENO_BIG_PREFAB_PATH = "Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921";

	public const string CARTOGRAPHER_PREFAB_PATH = "Cartographer_Quote.prefab:c6056bfb8c0025a458553adabc8ed537";

	public const string ELISE_BIG_PREFAB_PATH = "Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26";

	public const string FINLEY_BIG_PREFAB_PATH = "Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee";

	public const string BRANN_BIG_PREFAB_PATH = "Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b";

	public const string RAFAAM_WRAP_PREFAB_PATH = "Rafaam_wrap_Quote.prefab:d7100015bf618604ea93bad6b9f54f8b";

	public const string RAFAAM_WRAP_BIG_PREFAB_PATH = "Rafaam_wrap_BigQuote.prefab:ee7dbbb027adc1947b64b05f31d4c124";

	public const string RAFAAM_BIG_PREFAB_PATH = "Rafaam_BigQuote.prefab:ff1fd65bf3d8ba748b144b805fca871f";

	public const string RAFAAM_PREFAB_PATH = "Rafaam_Quote.prefab:d27a824bbfd6bd94185fe10e594f0014";

	public const string BRANN_PREFAB_PATH = "Brann_Quote.prefab:2c11651ab7740924189734944b8d7089";

	public const string BLAGGH_PREFAB_PATH = "Blaggh_Quote.prefab:f5d1e7053e6368e4a930ca3906cff53a";

	public const string MEDIVH_PREFAB_PATH = "Medivh_Quote.prefab:423c4a6b7e7a7f643bf0b2992ad3d31b";

	public const string MEDIVH_BIG_PREFAB_PATH = "Medivh_BigQuote.prefab:78e18a627031f6c48aef27a0fa1123c1";

	public const string MEDIVAS_BIG_PREFAB_PATH = "Medivas_BigQuote.prefab:ad677b060790a304fa6caed25f19bf88";

	public const string MOROES_PREFAB_PATH = "Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf";

	public const string MOROES_BIG_PREFAB_PATH = "Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d";

	public const string CURATOR_PREFAB_PATH = "Curator_Quote.prefab:ab58be80382875e4cbaa766fda73cd39";

	public const string CURATOR_BIG_PREFAB_PATH = "Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81";

	public const string BARNES_PREFAB_PATH = "Barnes_Quote.prefab:2e7e9f28b5bc37149a12b2e5feaa244a";

	public const string BARNES_BIG_PREFAB_PATH = "Barnes_BigQuote.prefab:15c396b2577ab09449f3721d23da3dba";

	public const string AYA_BIG_PREFAB_PATH = "Aya_BigQuote.prefab:26a19c2632327c14dbf648b96f8751d1";

	public const string HANCHO_BIG_PREFAB_PATH = "HanCho_BigQuote.prefab:0b24275caed054c45b2ebcb91fd9112d";

	public const string KAZAKUS_BIG_PREFAB_PATH = "Kazakus_BigQuote.prefab:b0007ae4277fc5a40a8c6f8c774ab823";

	public const string LICHKING_PREFAB_PATH = "LichKing_Quote.prefab:59d5b461e0b2bbe479b7db63e0962d30";

	public const string TIRION_BIG_PREFAB_PATH = "Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8";

	public const string AHUNE_BIG_PREFAB_PATH = "Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482";

	public const string RAGNAROS_BIG_PREFAB_PATH = "Ragnaros_BigQuote.prefab:843c4fab946192943a909b026f755505";

	public const string DEMON_HUNTER_ILLIDAN_PREFAB_PATH = "DemonHunter_Illidan_Popup_Banner.prefab:c2b08a2b89af02e4bb9e80b08526df7a";

	public static readonly float DEPTH = -15f;

	public static readonly Vector3 LEFT_OF_FRIENDLY_HERO = new Vector3(-1f, 0f, 1f);

	public static readonly Vector3 RIGHT_OF_FRIENDLY_HERO = new Vector3(-6f, 0f, 1f);

	public static readonly Vector3 LEFT_OF_ENEMY_HERO = new Vector3(-1f, 0f, -3.5f);

	public static readonly Vector3 RIGHT_OF_ENEMY_HERO = new Vector3(-6f, 0f, -3f);

	public static readonly Vector3 DEFAULT_CHARACTER_POS = new Vector3(100f, DEPTH, 24.7f);

	public static readonly Vector3 CHARACTER_POS_ABOVE_QUEST_TOAST = new Vector3(100f, 50f, 24.7f);

	public static readonly Vector3 ALT_ADVENTURE_SCREEN_POS = new Vector3(104.8f, DEPTH, 131.1f);

	public static readonly Vector3 PHONE_CHARACTER_POS = new Vector3(124.1f, DEPTH, 24.7f);

	public static readonly float PHONE_OVERLAY_UI_CHARACTER_X_OFFSET = -0.5f;

	public GameObject speechBubblePrefab;

	public GameObject speechIndicatorPrefab;

	public GameObject bounceArrowPrefab;

	public GameObject fadeArrowPrefab;

	public GameObject popupTextPrefab;

	public GameObject fancyPopupTextPrefab;

	public GameObject dialogBoxPrefab;

	public GameObject innkeeperQuotePrefab;

	private static NotificationManager s_instance;

	private Map<int, List<Notification>> notificationsToDestroyUponNewNotifier;

	private List<Notification> arrows;

	private List<Notification> popUpTexts;

	private Notification popUpDialog;

	private Notification m_quote;

	private List<string> m_quotesThisSession;

	private const float DEFAULT_QUOTE_DURATION = 8f;

	private Vector3 NOTIFICATION_SCALE = 0.163f * Vector3.one;

	private Vector3 NOTIFICATION_SCALE_PHONE = 0.326f * Vector3.one;

	public static Vector3 NOTIFICATITON_WORLD_SCALE
	{
		get
		{
			if (!UniversalInputManager.UsePhoneUI)
			{
				return 18f * Vector3.one;
			}
			return 25f * Vector3.one;
		}
	}

	public bool IsQuotePlaying => m_quote != null;

	private void Awake()
	{
		s_instance = this;
		m_quotesThisSession = new List<string>();
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	private void Start()
	{
		notificationsToDestroyUponNewNotifier = new Map<int, List<Notification>>();
		arrows = new List<Notification>();
		popUpTexts = new List<Notification>();
	}

	public static NotificationManager Get()
	{
		return s_instance;
	}

	public Notification CreatePopupDialog(string headlineText, string bodyText, string yesOrOKButtonText, string noButtonText)
	{
		return CreatePopupDialog(headlineText, bodyText, yesOrOKButtonText, noButtonText, new Vector3(0f, 0f, 0f));
	}

	public Notification CreatePopupDialog(string headlineText, string bodyText, string yesOrOKButtonText, string noButtonText, Vector3 offset)
	{
		if (popUpDialog != null)
		{
			UnityEngine.Object.Destroy(popUpDialog.gameObject);
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(dialogBoxPrefab);
		Vector3 position = Camera.main.transform.position;
		gameObject.transform.position = position + new Vector3(-0.07040818f, -16.10709f, 1.79612f) + offset;
		popUpDialog = gameObject.GetComponent<Notification>();
		popUpDialog.ChangeDialogText(headlineText, bodyText, yesOrOKButtonText, noButtonText);
		popUpDialog.PlayBirth();
		UniversalInputManager.Get().SetGameDialogActive(active: true);
		return popUpDialog;
	}

	public Notification CreateSpeechBubble(string speechText, Actor actor)
	{
		return CreateSpeechBubble(speechText, Notification.SpeechBubbleDirection.BottomLeft, actor, bDestroyWhenNewCreated: false);
	}

	public Notification CreateSpeechBubble(string speechText, Actor actor, bool bDestroyWhenNewCreated)
	{
		return CreateSpeechBubble(speechText, Notification.SpeechBubbleDirection.BottomLeft, actor, bDestroyWhenNewCreated);
	}

	public Notification CreateSpeechBubble(string speechText, Notification.SpeechBubbleDirection direction, Actor actor)
	{
		return CreateSpeechBubble(speechText, direction, actor, bDestroyWhenNewCreated: false);
	}

	public Notification CreateSpeechBubble(string speechText, Notification.SpeechBubbleDirection direction, Actor actor, bool bDestroyWhenNewCreated, bool parentToActor = true, float bubbleScale = 0f)
	{
		SpeechBubbleOptions options = new SpeechBubbleOptions().WithSpeechText(speechText).WithSpeechBubbleDirection(direction).WithActor(actor)
			.WithDestroyWhenNewCreated(bDestroyWhenNewCreated)
			.WithParentToActor(parentToActor)
			.WithBubbleScale(bubbleScale);
		return CreateSpeechBubble(options);
	}

	public Notification CreateSpeechBubble(SpeechBubbleOptions options)
	{
		DestroyOtherNotifications(options.direction, options.speechBubbleGroup);
		Notification component;
		if (options.speechText == "" && options.visualEmoteType == VisualEmoteType.NONE)
		{
			component = UnityEngine.Object.Instantiate(speechIndicatorPrefab).GetComponent<Notification>();
			component.PlaySmallBirthForFakeBubble();
			component.SetPositionForSmallBubble(options.actor);
			if (!Cheats.Get().IsSpeechBubbleEnabled())
			{
				component.SetPosition(Cheats.Get().SPEECH_BUBBLE_HIDDEN_POSITION);
			}
		}
		else
		{
			component = UnityEngine.Object.Instantiate(speechBubblePrefab).GetComponent<Notification>();
			if (options.visualEmoteType == VisualEmoteType.NONE)
			{
				component.ChangeText(options.speechText);
				component.ChangeEmote(VisualEmoteType.NONE);
			}
			else
			{
				component.ChangeText("");
				component.ChangeEmote(options.visualEmoteType);
			}
			component.FaceDirection(options.direction);
			component.PlayBirth();
			component.SetPosition(options.actor, options.direction);
			if (!Cheats.Get().IsSpeechBubbleEnabled() && options.visualEmoteType == VisualEmoteType.NONE)
			{
				component.SetPosition(Cheats.Get().SPEECH_BUBBLE_HIDDEN_POSITION);
			}
			if (!Mathf.Approximately(options.bubbleScale, 0f))
			{
				GameObject gameObject = new GameObject();
				gameObject.transform.SetParent(options.actor.transform);
				TransformUtil.Identity(gameObject);
				component.SetParentOffsetObject(gameObject);
				gameObject.transform.localScale = new Vector3(options.bubbleScale, options.bubbleScale, options.bubbleScale);
			}
		}
		if (options.destroyWhenNewCreated)
		{
			if (!notificationsToDestroyUponNewNotifier.ContainsKey(options.speechBubbleGroup))
			{
				notificationsToDestroyUponNewNotifier.Add(options.speechBubbleGroup, new List<Notification>());
			}
			notificationsToDestroyUponNewNotifier[options.speechBubbleGroup].Add(component);
		}
		if (options.parentToActor)
		{
			component.transform.parent = options.actor.transform;
		}
		if (options.finishCallback != null)
		{
			Notification notification = component;
			notification.OnFinishDeathState = (Action<int>)Delegate.Combine(notification.OnFinishDeathState, options.finishCallback);
		}
		if (options.emoteDuration > 0f)
		{
			DestroyNotification(component, options.emoteDuration);
		}
		component.notificationGroup = options.speechBubbleGroup;
		return component;
	}

	public Notification CreateBouncingArrow(UserAttentionBlocker blocker, bool addToList)
	{
		if (!SceneMgr.Get().IsInGame() && !UserAttentionManager.CanShowAttentionGrabber(blocker, "NotificationManger.CreateBouncingArrow"))
		{
			return null;
		}
		Notification component = UnityEngine.Object.Instantiate(bounceArrowPrefab).GetComponent<Notification>();
		component.PlayBirth();
		if (addToList)
		{
			arrows.Add(component);
		}
		return component;
	}

	public Notification CreateBouncingArrow(UserAttentionBlocker blocker, Vector3 position, Vector3 rotation)
	{
		return CreateBouncingArrow(blocker, position, rotation, addToList: true);
	}

	public Notification CreateBouncingArrow(UserAttentionBlocker blocker, Vector3 position, Vector3 rotation, bool addToList, float scaleFactor = 1f)
	{
		if (!SceneMgr.Get().IsInGame() && !UserAttentionManager.CanShowAttentionGrabber(blocker, "NotificationManger.CreateBouncingArrow"))
		{
			return null;
		}
		Notification notification = CreateBouncingArrow(blocker, addToList);
		notification.transform.position = position;
		notification.transform.localEulerAngles = rotation;
		notification.transform.localScale = Vector3.one * scaleFactor;
		return notification;
	}

	public Notification CreateFadeArrow(bool addToList)
	{
		Notification component = UnityEngine.Object.Instantiate(fadeArrowPrefab).GetComponent<Notification>();
		component.PlayBirth();
		if (addToList)
		{
			arrows.Add(component);
		}
		return component;
	}

	public Notification CreateFadeArrow(Vector3 position, Vector3 rotation)
	{
		return CreateFadeArrow(position, rotation, addToList: true);
	}

	public Notification CreateFadeArrow(Vector3 position, Vector3 rotation, bool addToList)
	{
		Notification notification = CreateFadeArrow(addToList);
		notification.transform.position = position;
		notification.transform.localEulerAngles = rotation;
		return notification;
	}

	public Notification CreatePopupText(UserAttentionBlocker blocker, Transform bone, string text, bool convertLegacyPosition = true, PopupTextType popupTextType = PopupTextType.BASIC)
	{
		if (convertLegacyPosition)
		{
			return CreatePopupText(blocker, bone.position, bone.localScale, text, convertLegacyPosition, popupTextType);
		}
		return CreatePopupText(blocker, bone.localPosition, bone.localScale, text, convertLegacyPosition, popupTextType);
	}

	public Notification CreatePopupText(UserAttentionBlocker blocker, Vector3 position, Vector3 scale, string text, bool convertLegacyPosition = true, PopupTextType popupTextType = PopupTextType.BASIC)
	{
		if (!SceneMgr.Get().IsInGame() && !UserAttentionManager.CanShowAttentionGrabber(blocker, "NotificationManager.CreatePopupText"))
		{
			return null;
		}
		Vector3 localPosition = position;
		if (convertLegacyPosition)
		{
			Camera camera = ((SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY) ? Box.Get().GetBoxCamera().GetComponent<Camera>() : BoardCameras.Get().GetComponentInChildren<Camera>());
			localPosition = OverlayUI.Get().GetRelativePosition(position, camera, OverlayUI.Get().m_heightScale.m_Center);
		}
		GameObject gameObject = UnityEngine.Object.Instantiate((popupTextType == PopupTextType.BASIC) ? popupTextPrefab : fancyPopupTextPrefab);
		SceneUtils.SetLayer(gameObject, GameLayer.UI);
		gameObject.transform.localPosition = localPosition;
		gameObject.transform.localScale = scale;
		OverlayUI.Get().AddGameObject(gameObject);
		Notification component = gameObject.GetComponent<Notification>();
		component.ChangeText(text);
		component.PlayBirth();
		popUpTexts.Add(component);
		return component;
	}

	public Notification CreateInnkeeperQuote(UserAttentionBlocker blocker, string text, string soundPath, float durationSeconds = 0f, Action<int> finishCallback = null, bool clickToDismiss = false)
	{
		return CreateInnkeeperQuote(blocker, DEFAULT_CHARACTER_POS, text, soundPath, durationSeconds, finishCallback, clickToDismiss);
	}

	public Notification CreateInnkeeperQuote(UserAttentionBlocker blocker, string text, string soundPath, Action<int> finishCallback, bool clickToDismiss = false)
	{
		return CreateInnkeeperQuote(blocker, DEFAULT_CHARACTER_POS, text, soundPath, 0f, finishCallback, clickToDismiss);
	}

	public Notification CreateInnkeeperQuote(UserAttentionBlocker blocker, Vector3 position, string text, string soundPath, float durationSeconds = 0f, Action<int> finishCallback = null, bool clickToDismiss = false)
	{
		if (!SceneMgr.Get().IsInGame() && !UserAttentionManager.CanShowAttentionGrabber(blocker, "NotificationManager.CreateInnkeeperQuote"))
		{
			finishCallback?.Invoke(0);
			return null;
		}
		GameObject obj = UnityEngine.Object.Instantiate(innkeeperQuotePrefab);
		obj.GetComponentInChildren<BoxCollider>().enabled = clickToDismiss;
		Notification component = obj.GetComponent<Notification>();
		component.ignoreAudioOnDestroy = clickToDismiss;
		if (finishCallback != null)
		{
			component.OnFinishDeathState = (Action<int>)Delegate.Combine(component.OnFinishDeathState, finishCallback);
		}
		PlayCharacterQuote(component, position, text, soundPath, durationSeconds);
		return component;
	}

	public Notification CreateKTQuote(string stringTag, string soundPath, bool allowRepeatDuringSession = true)
	{
		return CreateKTQuote(DEFAULT_CHARACTER_POS, stringTag, soundPath, allowRepeatDuringSession);
	}

	public Notification CreateKTQuote(Vector3 position, string stringTag, string soundPath, bool allowRepeatDuringSession = true)
	{
		return CreateCharacterQuote("KT_Quote.prefab:7ad118a1a10e9ab409ade82268a378f5", position, GameStrings.Get(stringTag), soundPath, allowRepeatDuringSession);
	}

	public Notification CreateZombieNefarianQuote(Vector3 position, string stringTag, string soundPath, bool allowRepeatDuringSession)
	{
		return CreateCharacterQuote("NefarianDragon_Quote.prefab:179fec888df7e4c02b8de3b7ad109a23", position, GameStrings.Get(stringTag), soundPath, allowRepeatDuringSession);
	}

	public void PlayBundleInnkeeperLineForClass(TAG_CLASS cardClass)
	{
		bool clickToDismiss = UniversalInputManager.UsePhoneUI;
		string text = string.Empty;
		string soundPath = string.Empty;
		switch (cardClass)
		{
		case TAG_CLASS.DRUID:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_DRUID");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryDruid_01.prefab:2c4672cdfe2a96a45a7ac4f29c17d5b7";
			break;
		case TAG_CLASS.HUNTER:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_HUNTER");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryHunter_01.prefab:77302a32e0268f845a97992117241577";
			break;
		case TAG_CLASS.MAGE:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_MAGE");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryMage_01.prefab:2059ede4ae6efab489ecb4240a08d5bb";
			break;
		case TAG_CLASS.PALADIN:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_PALADIN");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryPaladin_01.prefab:21b7870188f66714b9707961d833b26a";
			break;
		case TAG_CLASS.PRIEST:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_PRIEST");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryPriest_01.prefab:fe9cd14401fd7f14f80950fb99864ce7";
			break;
		case TAG_CLASS.ROGUE:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_ROGUE");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryRogue_01.prefab:aa4c71ab99a240a4885e4a8d034adb1b";
			break;
		case TAG_CLASS.SHAMAN:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_SHAMAN");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryShaman_01.prefab:1101d9f890551164791f277babaa25d9";
			break;
		case TAG_CLASS.WARLOCK:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_WARLOCK");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryWarlock_01.prefab:5eaf5c883b0310e4d91bcfd3debc6eff";
			break;
		case TAG_CLASS.WARRIOR:
			text = GameStrings.Get("GLUE_INKEEPER_RANDOM_CARD_DECK_RECIPE_WARRIOR");
			soundPath = "VO_INKEEPER_Male_Dwarf_ClassLegendaryWarrior_01.prefab:41b4581beb2dae945843ed164a6ec710";
			break;
		}
		if (!string.IsNullOrEmpty(text))
		{
			Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, text, soundPath, null, clickToDismiss);
		}
	}

	public Notification CreateCharacterQuote(string prefabPath, string text, string soundPath, bool allowRepeatDuringSession = true, float durationSeconds = 0f, CanvasAnchor anchorPoint = CanvasAnchor.BOTTOM_LEFT, bool blockAllOtherInput = false)
	{
		return CreateCharacterQuote(prefabPath, DEFAULT_CHARACTER_POS, text, soundPath, allowRepeatDuringSession, durationSeconds, null, anchorPoint, blockAllOtherInput);
	}

	public Notification CreateCharacterQuote(string prefabPath, Vector3 position, string text, string soundPath, bool allowRepeatDuringSession = true, float durationSeconds = 0f, Action<int> finishCallback = null, CanvasAnchor anchorPoint = CanvasAnchor.BOTTOM_LEFT, bool blockAllOtherInput = false)
	{
		if (!allowRepeatDuringSession && m_quotesThisSession.Contains(soundPath))
		{
			return null;
		}
		m_quotesThisSession.Add(soundPath);
		Notification notification = GameUtils.LoadGameObjectWithComponent<Notification>(prefabPath);
		if (notification == null)
		{
			return null;
		}
		notification.ShowWithExistingPopups = true;
		notification.PrefabPath = prefabPath;
		notification.SetClickBlockerActive(blockAllOtherInput);
		if (finishCallback != null)
		{
			notification.OnFinishDeathState = (Action<int>)Delegate.Combine(notification.OnFinishDeathState, finishCallback);
		}
		PlayCharacterQuote(notification, position, text, soundPath, durationSeconds, anchorPoint);
		return notification;
	}

	public Notification CreateBigCharacterQuoteWithGameString(string prefabPath, Vector3 position, string soundPath, string bubbleGameStringID, bool allowRepeatDuringSession = true, float durationSeconds = 0f, Action<int> finishCallback = null, bool useOverlayUI = false, Notification.SpeechBubbleDirection bubbleDir = Notification.SpeechBubbleDirection.None, bool persistCharacter = false, bool altPosition = false)
	{
		if (!allowRepeatDuringSession && m_quotesThisSession.Contains(bubbleGameStringID))
		{
			return null;
		}
		m_quotesThisSession.Add(bubbleGameStringID);
		return CreateBigCharacterQuoteWithText(prefabPath, position, soundPath, GameStrings.Get(bubbleGameStringID), durationSeconds, finishCallback, useOverlayUI, bubbleDir, persistCharacter, altPosition);
	}

	public Notification CreateBigCharacterQuoteWithText(string prefabPath, Vector3 position, string soundPath, string bubbleText, float durationSeconds = 0f, Action<int> finishCallback = null, bool useOverlayUI = false, Notification.SpeechBubbleDirection bubbleDir = Notification.SpeechBubbleDirection.None, bool persistCharacter = false, bool altPosition = false)
	{
		bool animateSpeechBubble = false;
		Notification notification;
		if (prefabPath != null && m_quote != null && m_quote.PersistCharacter && prefabPath.Equals(m_quote.PrefabPath))
		{
			notification = m_quote;
			animateSpeechBubble = true;
		}
		else
		{
			notification = GameUtils.LoadGameObjectWithComponent<Notification>(prefabPath);
		}
		if (notification == null)
		{
			return null;
		}
		notification.PrefabPath = prefabPath;
		notification.PersistCharacter = persistCharacter;
		notification.ShowWithExistingPopups = true;
		if (bubbleDir != 0)
		{
			notification.RepositionSpeechBubbleAroundBigQuote(bubbleDir, animateSpeechBubble);
		}
		if (finishCallback != null)
		{
			Notification notification2 = notification;
			notification2.OnFinishDeathState = (Action<int>)Delegate.Combine(notification2.OnFinishDeathState, finishCallback);
		}
		PlayBigCharacterQuote(notification, bubbleText, soundPath, durationSeconds, position, useOverlayUI, persistCharacter, altPosition);
		return notification;
	}

	public void ForceAddSoundToPlayedList(string soundPath)
	{
		m_quotesThisSession.Add(soundPath);
	}

	public void ForceRemoveSoundFromPlayedList(string soundPath)
	{
		m_quotesThisSession.Remove(soundPath);
	}

	public bool HasSoundPlayedThisSession(string soundPath)
	{
		return m_quotesThisSession.Contains(soundPath);
	}

	public void ResetSoundsPlayedThisSession()
	{
		m_quotesThisSession.Clear();
	}

	private void PlayBigCharacterQuote(Notification quote, string text, string soundPath, float durationSeconds, Vector3 position, bool useOverlayUI = false, bool persistCharacter = false, bool altPosition = false)
	{
		bool flag = true;
		if ((bool)m_quote)
		{
			if (m_quote == quote)
			{
				flag = false;
			}
			else
			{
				UnityEngine.Object.Destroy(m_quote.gameObject);
			}
		}
		m_quote = quote;
		m_quote.ChangeText(text);
		if (useOverlayUI)
		{
			string text2 = (altPosition ? "OffScreenSpeaker2" : "OffScreenSpeaker1");
			TransformUtil.AttachAndPreserveLocalTransform(m_quote.transform, OverlayUI.Get().FindBone(text2));
		}
		else
		{
			TransformUtil.AttachAndPreserveLocalTransform(m_quote.transform, Board.Get().FindBone("OffScreenSpeaker1"));
		}
		Vector3 localPosition = Vector3.zero;
		if (position != DEFAULT_CHARACTER_POS)
		{
			localPosition = position;
		}
		if (useOverlayUI && (bool)UniversalInputManager.UsePhoneUI)
		{
			localPosition.x += PHONE_OVERLAY_UI_CHARACTER_X_OFFSET;
		}
		m_quote.transform.localPosition = localPosition;
		m_quote.transform.localEulerAngles = Vector3.zero;
		if (!useOverlayUI && m_quote.rotate180InGameplay)
		{
			m_quote.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		}
		if (flag)
		{
			m_quote.transform.localScale = Vector3.one * 0.01f;
		}
		if (!string.IsNullOrEmpty(soundPath) && AssetLoader.Get().IsAssetAvailable(soundPath))
		{
			QuoteSoundCallbackData quoteSoundCallbackData = new QuoteSoundCallbackData();
			quoteSoundCallbackData.m_quote = m_quote;
			quoteSoundCallbackData.m_durationSeconds = durationSeconds;
			quoteSoundCallbackData.m_persistCharacter = persistCharacter;
			SoundLoader.LoadSound(soundPath, OnBigQuoteSoundLoaded, quoteSoundCallbackData, SoundManager.Get().GetPlaceholderSound());
			return;
		}
		m_quote.PlayBirthWithForcedScale(Vector3.one);
		if (durationSeconds > 0f)
		{
			if (persistCharacter)
			{
				DestroySpeechBubble(m_quote, durationSeconds);
			}
			else
			{
				DestroyNotification(m_quote, durationSeconds);
			}
		}
	}

	private void PlayCharacterQuote(Notification quote, Vector3 position, string text, string soundPath, float durationSeconds, CanvasAnchor anchorPoint = CanvasAnchor.BOTTOM_LEFT)
	{
		if ((bool)m_quote)
		{
			UnityEngine.Object.Destroy(m_quote.gameObject);
		}
		m_quote = quote;
		m_quote.ChangeText(text);
		m_quote.transform.position = position;
		m_quote.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		OverlayUI.Get().AddGameObject(m_quote.gameObject, anchorPoint);
		if (!string.IsNullOrEmpty(soundPath) && AssetLoader.Get().IsAssetAvailable(soundPath))
		{
			QuoteSoundCallbackData quoteSoundCallbackData = new QuoteSoundCallbackData();
			quoteSoundCallbackData.m_quote = m_quote;
			quoteSoundCallbackData.m_durationSeconds = durationSeconds;
			SoundLoader.LoadSound(soundPath, OnQuoteSoundLoaded, quoteSoundCallbackData, SoundManager.Get().GetPlaceholderSound());
		}
		else
		{
			PlayQuoteWithoutSound(durationSeconds, text);
		}
	}

	private void PlayQuoteWithoutSound(float durationSeconds, string text = null)
	{
		m_quote.PlayBirthWithForcedScale(UniversalInputManager.UsePhoneUI ? NOTIFICATION_SCALE_PHONE : NOTIFICATION_SCALE);
		if (durationSeconds <= 0f && text != null)
		{
			durationSeconds = ClipLengthEstimator.StringToReadTime(text);
		}
		DestroyNotification(m_quote, durationSeconds);
	}

	private void OnQuoteSoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		QuoteSoundCallbackData quoteSoundCallbackData = (QuoteSoundCallbackData)callbackData;
		if (!quoteSoundCallbackData.m_quote)
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		AudioSource audioSource = null;
		if ((bool)go)
		{
			audioSource = go.GetComponent<AudioSource>();
			if ((bool)audioSource && !audioSource.clip)
			{
				audioSource = null;
			}
		}
		if (!audioSource)
		{
			Log.Asset.PrintInfo("Quote Sound failed to load!");
			PlayQuoteWithoutSound((quoteSoundCallbackData.m_durationSeconds > 0f) ? quoteSoundCallbackData.m_durationSeconds : 8f);
			return;
		}
		m_quote.AssignAudio(audioSource);
		SoundManager.Get().PlayPreloaded(audioSource);
		m_quote.PlayBirthWithForcedScale(UniversalInputManager.UsePhoneUI ? NOTIFICATION_SCALE_PHONE : NOTIFICATION_SCALE);
		float delaySeconds = Mathf.Max(quoteSoundCallbackData.m_durationSeconds, audioSource.clip.length);
		DestroyNotification(m_quote, delaySeconds);
		if (m_quote.clickOff != null)
		{
			m_quote.clickOff.SetData(m_quote);
			m_quote.clickOff.AddEventListener(UIEventType.PRESS, ClickNotification);
		}
	}

	private void OnBigQuoteSoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		QuoteSoundCallbackData quoteSoundCallbackData = (QuoteSoundCallbackData)callbackData;
		if (!quoteSoundCallbackData.m_quote)
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		AudioSource audioSource = null;
		if ((bool)go)
		{
			audioSource = go.GetComponent<AudioSource>();
			if ((bool)audioSource && !audioSource.clip)
			{
				audioSource = null;
			}
		}
		if (!audioSource)
		{
			Log.Asset.PrintInfo("Quote Sound failed to load!");
			PlayQuoteWithoutSound((quoteSoundCallbackData.m_durationSeconds > 0f) ? quoteSoundCallbackData.m_durationSeconds : 8f);
			return;
		}
		m_quote.AssignAudio(audioSource);
		SoundManager.Get().PlayPreloaded(audioSource);
		m_quote.PlayBirthWithForcedScale(Vector3.one);
		float num = Mathf.Max(quoteSoundCallbackData.m_durationSeconds, audioSource.clip.length);
		Log.Notifications.Print("Destroying notification or speech bubble after {0} seconds. durationSeconds: {1} source.clip.length: {2} persistCharacter? {3}", num, quoteSoundCallbackData.m_durationSeconds, audioSource.clip.length, quoteSoundCallbackData.m_persistCharacter);
		if (quoteSoundCallbackData.m_persistCharacter)
		{
			DestroySpeechBubble(m_quote, num);
		}
		else
		{
			DestroyNotification(m_quote, num);
		}
		if (m_quote.clickOff != null)
		{
			m_quote.clickOff.SetData(m_quote);
			m_quote.clickOff.AddEventListener(UIEventType.PRESS, ClickNotification);
		}
	}

	public void DestroyAllArrows()
	{
		if (arrows.Count == 0)
		{
			return;
		}
		for (int i = 0; i < arrows.Count; i++)
		{
			if (arrows[i] != null)
			{
				NukeNotificationWithoutPlayingAnim(arrows[i]);
			}
		}
	}

	public void DestroyAllPopUps()
	{
		if (popUpTexts.Count == 0)
		{
			return;
		}
		for (int i = 0; i < popUpTexts.Count; i++)
		{
			if (!(popUpTexts[i] == null))
			{
				NukeNotification(popUpTexts[i]);
			}
		}
		popUpTexts = new List<Notification>();
	}

	private void DestroyOtherNotifications(Notification.SpeechBubbleDirection direction, int speechBubbleGroup)
	{
		if (notificationsToDestroyUponNewNotifier.Count == 0 || !notificationsToDestroyUponNewNotifier.ContainsKey(speechBubbleGroup) || notificationsToDestroyUponNewNotifier[speechBubbleGroup] == null)
		{
			return;
		}
		for (int i = 0; i < notificationsToDestroyUponNewNotifier[speechBubbleGroup].Count; i++)
		{
			if (!(notificationsToDestroyUponNewNotifier[speechBubbleGroup][i] == null) && notificationsToDestroyUponNewNotifier[speechBubbleGroup][i].GetSpeechBubbleDirection() == direction)
			{
				NukeNotificationWithoutPlayingAnim(notificationsToDestroyUponNewNotifier[speechBubbleGroup][i]);
			}
		}
	}

	public void DestroyNotification(Notification notification, float delaySeconds)
	{
		if (!(notification == null))
		{
			if (delaySeconds == 0f)
			{
				NukeNotification(notification);
			}
			else
			{
				StartCoroutine(WaitAndThenDestroyNotification(notification, delaySeconds));
			}
		}
	}

	public void DestroySpeechBubble(Notification notification, float delaySeconds)
	{
		if (!(notification == null))
		{
			if (delaySeconds == 0f)
			{
				NukeSpeechBubble(notification);
			}
			else
			{
				StartCoroutine(WaitAndThenDestroySpeechBubble(notification, delaySeconds));
			}
		}
	}

	public void DestroyNotificationWithText(string text, float delaySeconds = 0f)
	{
		Notification notification = null;
		for (int i = 0; i < popUpTexts.Count; i++)
		{
			if (!(popUpTexts[i] == null) && popUpTexts[i].speechUberText.Text == text)
			{
				notification = popUpTexts[i];
			}
		}
		DestroyNotification(notification, delaySeconds);
	}

	private void ClickNotification(UIEvent e)
	{
		Notification notification = (Notification)e.GetElement().GetData();
		NukeNotification(notification);
		notification.clickOff.RemoveEventListener(UIEventType.PRESS, ClickNotification);
	}

	public void DestroyAllNotificationsNowWithNoAnim()
	{
		if ((bool)popUpDialog)
		{
			NukeNotificationWithoutPlayingAnim(popUpDialog);
		}
		if ((bool)m_quote)
		{
			NukeNotificationWithoutPlayingAnim(m_quote);
		}
		foreach (List<Notification> value in notificationsToDestroyUponNewNotifier.Values)
		{
			for (int i = 0; i < value.Count; i++)
			{
				Notification notification = value[i];
				if (!(notification == null))
				{
					NukeNotificationWithoutPlayingAnim(notification);
				}
			}
		}
		DestroyAllArrows();
		DestroyAllPopUps();
	}

	public void DestroyActiveQuote(float delaySeconds, bool ignoreAudio = false)
	{
		if (!(m_quote == null))
		{
			if (ignoreAudio)
			{
				m_quote.ignoreAudioOnDestroy = true;
			}
			if (delaySeconds == 0f)
			{
				NukeNotification(m_quote);
			}
			else
			{
				StartCoroutine(WaitAndThenDestroyNotification(m_quote, delaySeconds));
			}
		}
	}

	public void DestroyNotificationNowWithNoAnim(Notification notification)
	{
		if (!(notification == null))
		{
			NukeNotificationWithoutPlayingAnim(notification);
		}
	}

	private IEnumerator WaitAndThenDestroyNotification(Notification notification, float amountSeconds)
	{
		yield return new WaitForSeconds(amountSeconds);
		if (notification != null)
		{
			NukeNotification(notification);
		}
	}

	private void NukeNotification(Notification notification)
	{
		if (notification == null)
		{
			Log.All.PrintWarning("Attempting to Nuke a Notification that does not exist!");
			return;
		}
		foreach (List<Notification> value in notificationsToDestroyUponNewNotifier.Values)
		{
			if (value.Contains(notification))
			{
				value.Remove(notification);
			}
		}
		if (!notification.IsDying())
		{
			notification.PlayDeath();
			UniversalInputManager.Get().SetGameDialogActive(active: false);
		}
	}

	private void NukeNotificationWithoutPlayingAnim(Notification notification)
	{
		foreach (List<Notification> value in notificationsToDestroyUponNewNotifier.Values)
		{
			if (value.Contains(notification))
			{
				value.Remove(notification);
			}
		}
		UnityEngine.Object.Destroy(notification.gameObject);
		UniversalInputManager.Get().SetGameDialogActive(active: false);
	}

	private IEnumerator WaitAndThenDestroySpeechBubble(Notification notification, float amountSeconds)
	{
		yield return new WaitForSeconds(amountSeconds);
		if (notification != null)
		{
			NukeSpeechBubble(notification);
		}
	}

	private void NukeSpeechBubble(Notification notification)
	{
		if (notification == null)
		{
			Log.All.PrintWarning("Attempting to Nuke a Speech Bubble for a Notification that does not exist!");
		}
		else if (!notification.IsDying())
		{
			notification.PlaySpeechBubbleDeath();
		}
	}

	public TutorialNotification CreateTutorialDialog(string headlineGameString, string bodyTextGameString, string buttonGameString, UIEvent.Handler buttonHandler, Vector2 materialOffset, bool swapMaterial = false)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("TutorialIntroDialog.prefab:2d189389d0be2f2428bf37ace33e85b1");
		if (gameObject == null)
		{
			Debug.LogError("Unable to load tutorial dialog TutorialIntroDialog prefab.");
			return null;
		}
		TutorialNotification notification = gameObject.GetComponent<TutorialNotification>();
		if (notification == null)
		{
			Debug.LogError("TutorialNotification component does not exist on TutorialIntroDialog prefab.");
			return null;
		}
		TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, OverlayUI.Get().m_heightScale.m_Center);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			gameObject.transform.localScale = 1.5f * gameObject.transform.localScale;
		}
		popUpDialog = notification;
		notification.headlineUberText.Text = GameStrings.Get(headlineGameString);
		notification.speechUberText.Text = GameStrings.Get(bodyTextGameString);
		notification.m_ButtonStart.SetText(GameStrings.Get(buttonGameString));
		if (swapMaterial)
		{
			notification.artOverlay.SetMaterial(notification.swapMaterial);
		}
		notification.artOverlay.GetMaterial().mainTextureOffset = materialOffset;
		notification.m_ButtonStart.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			if (buttonHandler != null)
			{
				buttonHandler(e);
			}
			notification.m_ButtonStart.ClearEventListeners();
			DestroyNotification(notification, 0f);
		});
		popUpDialog.PlayBirth();
		UniversalInputManager.Get().SetGameDialogActive(active: true);
		return notification;
	}
}
