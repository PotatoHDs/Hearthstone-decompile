using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008F1 RID: 2289
public class NotificationManager : MonoBehaviour
{
	// Token: 0x17000747 RID: 1863
	// (get) Token: 0x06007EF1 RID: 32497 RVA: 0x002923D8 File Offset: 0x002905D8
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

	// Token: 0x06007EF2 RID: 32498 RVA: 0x00292405 File Offset: 0x00290605
	private void Awake()
	{
		NotificationManager.s_instance = this;
		this.m_quotesThisSession = new List<string>();
	}

	// Token: 0x06007EF3 RID: 32499 RVA: 0x00292418 File Offset: 0x00290618
	private void OnDestroy()
	{
		NotificationManager.s_instance = null;
	}

	// Token: 0x06007EF4 RID: 32500 RVA: 0x00292420 File Offset: 0x00290620
	private void Start()
	{
		this.notificationsToDestroyUponNewNotifier = new Map<int, List<Notification>>();
		this.arrows = new List<Notification>();
		this.popUpTexts = new List<Notification>();
	}

	// Token: 0x06007EF5 RID: 32501 RVA: 0x00292443 File Offset: 0x00290643
	public static NotificationManager Get()
	{
		return NotificationManager.s_instance;
	}

	// Token: 0x06007EF6 RID: 32502 RVA: 0x0029244A File Offset: 0x0029064A
	public Notification CreatePopupDialog(string headlineText, string bodyText, string yesOrOKButtonText, string noButtonText)
	{
		return this.CreatePopupDialog(headlineText, bodyText, yesOrOKButtonText, noButtonText, new Vector3(0f, 0f, 0f));
	}

	// Token: 0x06007EF7 RID: 32503 RVA: 0x0029246C File Offset: 0x0029066C
	public Notification CreatePopupDialog(string headlineText, string bodyText, string yesOrOKButtonText, string noButtonText, Vector3 offset)
	{
		if (this.popUpDialog != null)
		{
			UnityEngine.Object.Destroy(this.popUpDialog.gameObject);
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.dialogBoxPrefab);
		Vector3 position = Camera.main.transform.position;
		gameObject.transform.position = position + new Vector3(-0.07040818f, -16.10709f, 1.79612f) + offset;
		this.popUpDialog = gameObject.GetComponent<Notification>();
		this.popUpDialog.ChangeDialogText(headlineText, bodyText, yesOrOKButtonText, noButtonText);
		this.popUpDialog.PlayBirth();
		UniversalInputManager.Get().SetGameDialogActive(true);
		return this.popUpDialog;
	}

	// Token: 0x06007EF8 RID: 32504 RVA: 0x00292517 File Offset: 0x00290717
	public Notification CreateSpeechBubble(string speechText, Actor actor)
	{
		return this.CreateSpeechBubble(speechText, Notification.SpeechBubbleDirection.BottomLeft, actor, false, true, 0f);
	}

	// Token: 0x06007EF9 RID: 32505 RVA: 0x00292529 File Offset: 0x00290729
	public Notification CreateSpeechBubble(string speechText, Actor actor, bool bDestroyWhenNewCreated)
	{
		return this.CreateSpeechBubble(speechText, Notification.SpeechBubbleDirection.BottomLeft, actor, bDestroyWhenNewCreated, true, 0f);
	}

	// Token: 0x06007EFA RID: 32506 RVA: 0x0029253B File Offset: 0x0029073B
	public Notification CreateSpeechBubble(string speechText, Notification.SpeechBubbleDirection direction, Actor actor)
	{
		return this.CreateSpeechBubble(speechText, direction, actor, false, true, 0f);
	}

	// Token: 0x06007EFB RID: 32507 RVA: 0x00292550 File Offset: 0x00290750
	public Notification CreateSpeechBubble(string speechText, Notification.SpeechBubbleDirection direction, Actor actor, bool bDestroyWhenNewCreated, bool parentToActor = true, float bubbleScale = 0f)
	{
		NotificationManager.SpeechBubbleOptions options = new NotificationManager.SpeechBubbleOptions().WithSpeechText(speechText).WithSpeechBubbleDirection(direction).WithActor(actor).WithDestroyWhenNewCreated(bDestroyWhenNewCreated).WithParentToActor(parentToActor).WithBubbleScale(bubbleScale);
		return this.CreateSpeechBubble(options);
	}

	// Token: 0x06007EFC RID: 32508 RVA: 0x00292594 File Offset: 0x00290794
	public Notification CreateSpeechBubble(NotificationManager.SpeechBubbleOptions options)
	{
		this.DestroyOtherNotifications(options.direction, options.speechBubbleGroup);
		Notification component;
		if (options.speechText == "" && options.visualEmoteType == NotificationManager.VisualEmoteType.NONE)
		{
			component = UnityEngine.Object.Instantiate<GameObject>(this.speechIndicatorPrefab).GetComponent<Notification>();
			component.PlaySmallBirthForFakeBubble();
			component.SetPositionForSmallBubble(options.actor);
			if (!Cheats.Get().IsSpeechBubbleEnabled())
			{
				component.SetPosition(Cheats.Get().SPEECH_BUBBLE_HIDDEN_POSITION);
			}
		}
		else
		{
			component = UnityEngine.Object.Instantiate<GameObject>(this.speechBubblePrefab).GetComponent<Notification>();
			if (options.visualEmoteType == NotificationManager.VisualEmoteType.NONE)
			{
				component.ChangeText(options.speechText);
				component.ChangeEmote(NotificationManager.VisualEmoteType.NONE);
			}
			else
			{
				component.ChangeText("");
				component.ChangeEmote(options.visualEmoteType);
			}
			component.FaceDirection(options.direction);
			component.PlayBirth();
			component.SetPosition(options.actor, options.direction);
			if (!Cheats.Get().IsSpeechBubbleEnabled() && options.visualEmoteType == NotificationManager.VisualEmoteType.NONE)
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
			if (!this.notificationsToDestroyUponNewNotifier.ContainsKey(options.speechBubbleGroup))
			{
				this.notificationsToDestroyUponNewNotifier.Add(options.speechBubbleGroup, new List<Notification>());
			}
			this.notificationsToDestroyUponNewNotifier[options.speechBubbleGroup].Add(component);
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
			this.DestroyNotification(component, options.emoteDuration);
		}
		component.notificationGroup = options.speechBubbleGroup;
		return component;
	}

	// Token: 0x06007EFD RID: 32509 RVA: 0x002927B0 File Offset: 0x002909B0
	public Notification CreateBouncingArrow(UserAttentionBlocker blocker, bool addToList)
	{
		if (!SceneMgr.Get().IsInGame() && !UserAttentionManager.CanShowAttentionGrabber(blocker, "NotificationManger.CreateBouncingArrow"))
		{
			return null;
		}
		Notification component = UnityEngine.Object.Instantiate<GameObject>(this.bounceArrowPrefab).GetComponent<Notification>();
		component.PlayBirth();
		if (addToList)
		{
			this.arrows.Add(component);
		}
		return component;
	}

	// Token: 0x06007EFE RID: 32510 RVA: 0x002927FF File Offset: 0x002909FF
	public Notification CreateBouncingArrow(UserAttentionBlocker blocker, Vector3 position, Vector3 rotation)
	{
		return this.CreateBouncingArrow(blocker, position, rotation, true, 1f);
	}

	// Token: 0x06007EFF RID: 32511 RVA: 0x00292810 File Offset: 0x00290A10
	public Notification CreateBouncingArrow(UserAttentionBlocker blocker, Vector3 position, Vector3 rotation, bool addToList, float scaleFactor = 1f)
	{
		if (!SceneMgr.Get().IsInGame() && !UserAttentionManager.CanShowAttentionGrabber(blocker, "NotificationManger.CreateBouncingArrow"))
		{
			return null;
		}
		Notification notification = this.CreateBouncingArrow(blocker, addToList);
		notification.transform.position = position;
		notification.transform.localEulerAngles = rotation;
		notification.transform.localScale = Vector3.one * scaleFactor;
		return notification;
	}

	// Token: 0x06007F00 RID: 32512 RVA: 0x00292870 File Offset: 0x00290A70
	public Notification CreateFadeArrow(bool addToList)
	{
		Notification component = UnityEngine.Object.Instantiate<GameObject>(this.fadeArrowPrefab).GetComponent<Notification>();
		component.PlayBirth();
		if (addToList)
		{
			this.arrows.Add(component);
		}
		return component;
	}

	// Token: 0x06007F01 RID: 32513 RVA: 0x002928A4 File Offset: 0x00290AA4
	public Notification CreateFadeArrow(Vector3 position, Vector3 rotation)
	{
		return this.CreateFadeArrow(position, rotation, true);
	}

	// Token: 0x06007F02 RID: 32514 RVA: 0x002928AF File Offset: 0x00290AAF
	public Notification CreateFadeArrow(Vector3 position, Vector3 rotation, bool addToList)
	{
		Notification notification = this.CreateFadeArrow(addToList);
		notification.transform.position = position;
		notification.transform.localEulerAngles = rotation;
		return notification;
	}

	// Token: 0x06007F03 RID: 32515 RVA: 0x002928D0 File Offset: 0x00290AD0
	public Notification CreatePopupText(UserAttentionBlocker blocker, Transform bone, string text, bool convertLegacyPosition = true, NotificationManager.PopupTextType popupTextType = NotificationManager.PopupTextType.BASIC)
	{
		if (convertLegacyPosition)
		{
			return this.CreatePopupText(blocker, bone.position, bone.localScale, text, convertLegacyPosition, popupTextType);
		}
		return this.CreatePopupText(blocker, bone.localPosition, bone.localScale, text, convertLegacyPosition, popupTextType);
	}

	// Token: 0x06007F04 RID: 32516 RVA: 0x00292908 File Offset: 0x00290B08
	public Notification CreatePopupText(UserAttentionBlocker blocker, Vector3 position, Vector3 scale, string text, bool convertLegacyPosition = true, NotificationManager.PopupTextType popupTextType = NotificationManager.PopupTextType.BASIC)
	{
		if (!SceneMgr.Get().IsInGame() && !UserAttentionManager.CanShowAttentionGrabber(blocker, "NotificationManager.CreatePopupText"))
		{
			return null;
		}
		Vector3 localPosition = position;
		if (convertLegacyPosition)
		{
			Camera camera;
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
			{
				camera = BoardCameras.Get().GetComponentInChildren<Camera>();
			}
			else
			{
				camera = Box.Get().GetBoxCamera().GetComponent<Camera>();
			}
			localPosition = OverlayUI.Get().GetRelativePosition(position, camera, OverlayUI.Get().m_heightScale.m_Center, 0f);
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>((popupTextType == NotificationManager.PopupTextType.BASIC) ? this.popupTextPrefab : this.fancyPopupTextPrefab);
		SceneUtils.SetLayer(gameObject, GameLayer.UI);
		gameObject.transform.localPosition = localPosition;
		gameObject.transform.localScale = scale;
		OverlayUI.Get().AddGameObject(gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		Notification component = gameObject.GetComponent<Notification>();
		component.ChangeText(text);
		component.PlayBirth();
		this.popUpTexts.Add(component);
		return component;
	}

	// Token: 0x17000748 RID: 1864
	// (get) Token: 0x06007F05 RID: 32517 RVA: 0x002929E8 File Offset: 0x00290BE8
	public bool IsQuotePlaying
	{
		get
		{
			return this.m_quote != null;
		}
	}

	// Token: 0x06007F06 RID: 32518 RVA: 0x002929F6 File Offset: 0x00290BF6
	public Notification CreateInnkeeperQuote(UserAttentionBlocker blocker, string text, string soundPath, float durationSeconds = 0f, Action<int> finishCallback = null, bool clickToDismiss = false)
	{
		return this.CreateInnkeeperQuote(blocker, NotificationManager.DEFAULT_CHARACTER_POS, text, soundPath, durationSeconds, finishCallback, clickToDismiss);
	}

	// Token: 0x06007F07 RID: 32519 RVA: 0x00292A0C File Offset: 0x00290C0C
	public Notification CreateInnkeeperQuote(UserAttentionBlocker blocker, string text, string soundPath, Action<int> finishCallback, bool clickToDismiss = false)
	{
		return this.CreateInnkeeperQuote(blocker, NotificationManager.DEFAULT_CHARACTER_POS, text, soundPath, 0f, finishCallback, clickToDismiss);
	}

	// Token: 0x06007F08 RID: 32520 RVA: 0x00292A28 File Offset: 0x00290C28
	public Notification CreateInnkeeperQuote(UserAttentionBlocker blocker, Vector3 position, string text, string soundPath, float durationSeconds = 0f, Action<int> finishCallback = null, bool clickToDismiss = false)
	{
		if (!SceneMgr.Get().IsInGame() && !UserAttentionManager.CanShowAttentionGrabber(blocker, "NotificationManager.CreateInnkeeperQuote"))
		{
			if (finishCallback != null)
			{
				finishCallback(0);
			}
			return null;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.innkeeperQuotePrefab);
		gameObject.GetComponentInChildren<BoxCollider>().enabled = clickToDismiss;
		Notification component = gameObject.GetComponent<Notification>();
		component.ignoreAudioOnDestroy = clickToDismiss;
		if (finishCallback != null)
		{
			Notification notification = component;
			notification.OnFinishDeathState = (Action<int>)Delegate.Combine(notification.OnFinishDeathState, finishCallback);
		}
		this.PlayCharacterQuote(component, position, text, soundPath, durationSeconds, CanvasAnchor.BOTTOM_LEFT);
		return component;
	}

	// Token: 0x06007F09 RID: 32521 RVA: 0x00292AAD File Offset: 0x00290CAD
	public Notification CreateKTQuote(string stringTag, string soundPath, bool allowRepeatDuringSession = true)
	{
		return this.CreateKTQuote(NotificationManager.DEFAULT_CHARACTER_POS, stringTag, soundPath, allowRepeatDuringSession);
	}

	// Token: 0x06007F0A RID: 32522 RVA: 0x00292AC0 File Offset: 0x00290CC0
	public Notification CreateKTQuote(Vector3 position, string stringTag, string soundPath, bool allowRepeatDuringSession = true)
	{
		return this.CreateCharacterQuote("KT_Quote.prefab:7ad118a1a10e9ab409ade82268a378f5", position, GameStrings.Get(stringTag), soundPath, allowRepeatDuringSession, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
	}

	// Token: 0x06007F0B RID: 32523 RVA: 0x00292AEC File Offset: 0x00290CEC
	public Notification CreateZombieNefarianQuote(Vector3 position, string stringTag, string soundPath, bool allowRepeatDuringSession)
	{
		return this.CreateCharacterQuote("NefarianDragon_Quote.prefab:179fec888df7e4c02b8de3b7ad109a23", position, GameStrings.Get(stringTag), soundPath, allowRepeatDuringSession, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
	}

	// Token: 0x06007F0C RID: 32524 RVA: 0x00292B18 File Offset: 0x00290D18
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
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, text, soundPath, null, clickToDismiss);
		}
	}

	// Token: 0x06007F0D RID: 32525 RVA: 0x00292C34 File Offset: 0x00290E34
	public Notification CreateCharacterQuote(string prefabPath, string text, string soundPath, bool allowRepeatDuringSession = true, float durationSeconds = 0f, CanvasAnchor anchorPoint = CanvasAnchor.BOTTOM_LEFT, bool blockAllOtherInput = false)
	{
		return this.CreateCharacterQuote(prefabPath, NotificationManager.DEFAULT_CHARACTER_POS, text, soundPath, allowRepeatDuringSession, durationSeconds, null, anchorPoint, blockAllOtherInput);
	}

	// Token: 0x06007F0E RID: 32526 RVA: 0x00292C58 File Offset: 0x00290E58
	public Notification CreateCharacterQuote(string prefabPath, Vector3 position, string text, string soundPath, bool allowRepeatDuringSession = true, float durationSeconds = 0f, Action<int> finishCallback = null, CanvasAnchor anchorPoint = CanvasAnchor.BOTTOM_LEFT, bool blockAllOtherInput = false)
	{
		if (!allowRepeatDuringSession && this.m_quotesThisSession.Contains(soundPath))
		{
			return null;
		}
		this.m_quotesThisSession.Add(soundPath);
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
			Notification notification2 = notification;
			notification2.OnFinishDeathState = (Action<int>)Delegate.Combine(notification2.OnFinishDeathState, finishCallback);
		}
		this.PlayCharacterQuote(notification, position, text, soundPath, durationSeconds, anchorPoint);
		return notification;
	}

	// Token: 0x06007F0F RID: 32527 RVA: 0x00292CDC File Offset: 0x00290EDC
	public Notification CreateBigCharacterQuoteWithGameString(string prefabPath, Vector3 position, string soundPath, string bubbleGameStringID, bool allowRepeatDuringSession = true, float durationSeconds = 0f, Action<int> finishCallback = null, bool useOverlayUI = false, Notification.SpeechBubbleDirection bubbleDir = Notification.SpeechBubbleDirection.None, bool persistCharacter = false, bool altPosition = false)
	{
		if (!allowRepeatDuringSession && this.m_quotesThisSession.Contains(bubbleGameStringID))
		{
			return null;
		}
		this.m_quotesThisSession.Add(bubbleGameStringID);
		return this.CreateBigCharacterQuoteWithText(prefabPath, position, soundPath, GameStrings.Get(bubbleGameStringID), durationSeconds, finishCallback, useOverlayUI, bubbleDir, persistCharacter, altPosition);
	}

	// Token: 0x06007F10 RID: 32528 RVA: 0x00292D28 File Offset: 0x00290F28
	public Notification CreateBigCharacterQuoteWithText(string prefabPath, Vector3 position, string soundPath, string bubbleText, float durationSeconds = 0f, Action<int> finishCallback = null, bool useOverlayUI = false, Notification.SpeechBubbleDirection bubbleDir = Notification.SpeechBubbleDirection.None, bool persistCharacter = false, bool altPosition = false)
	{
		bool animateSpeechBubble = false;
		Notification notification;
		if (prefabPath != null && this.m_quote != null && this.m_quote.PersistCharacter && prefabPath.Equals(this.m_quote.PrefabPath))
		{
			notification = this.m_quote;
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
		if (bubbleDir != Notification.SpeechBubbleDirection.None)
		{
			notification.RepositionSpeechBubbleAroundBigQuote(bubbleDir, animateSpeechBubble);
		}
		if (finishCallback != null)
		{
			Notification notification2 = notification;
			notification2.OnFinishDeathState = (Action<int>)Delegate.Combine(notification2.OnFinishDeathState, finishCallback);
		}
		this.PlayBigCharacterQuote(notification, bubbleText, soundPath, durationSeconds, position, useOverlayUI, persistCharacter, altPosition);
		return notification;
	}

	// Token: 0x06007F11 RID: 32529 RVA: 0x00292DD8 File Offset: 0x00290FD8
	public void ForceAddSoundToPlayedList(string soundPath)
	{
		this.m_quotesThisSession.Add(soundPath);
	}

	// Token: 0x06007F12 RID: 32530 RVA: 0x00292DE6 File Offset: 0x00290FE6
	public void ForceRemoveSoundFromPlayedList(string soundPath)
	{
		this.m_quotesThisSession.Remove(soundPath);
	}

	// Token: 0x06007F13 RID: 32531 RVA: 0x00292DF5 File Offset: 0x00290FF5
	public bool HasSoundPlayedThisSession(string soundPath)
	{
		return this.m_quotesThisSession.Contains(soundPath);
	}

	// Token: 0x06007F14 RID: 32532 RVA: 0x00292E03 File Offset: 0x00291003
	public void ResetSoundsPlayedThisSession()
	{
		this.m_quotesThisSession.Clear();
	}

	// Token: 0x06007F15 RID: 32533 RVA: 0x00292E10 File Offset: 0x00291010
	private void PlayBigCharacterQuote(Notification quote, string text, string soundPath, float durationSeconds, Vector3 position, bool useOverlayUI = false, bool persistCharacter = false, bool altPosition = false)
	{
		bool flag = true;
		if (this.m_quote)
		{
			if (this.m_quote == quote)
			{
				flag = false;
			}
			else
			{
				UnityEngine.Object.Destroy(this.m_quote.gameObject);
			}
		}
		this.m_quote = quote;
		this.m_quote.ChangeText(text);
		if (useOverlayUI)
		{
			string name = altPosition ? "OffScreenSpeaker2" : "OffScreenSpeaker1";
			TransformUtil.AttachAndPreserveLocalTransform(this.m_quote.transform, OverlayUI.Get().FindBone(name));
		}
		else
		{
			TransformUtil.AttachAndPreserveLocalTransform(this.m_quote.transform, Board.Get().FindBone("OffScreenSpeaker1"));
		}
		Vector3 localPosition = Vector3.zero;
		if (position != NotificationManager.DEFAULT_CHARACTER_POS)
		{
			localPosition = position;
		}
		if (useOverlayUI && UniversalInputManager.UsePhoneUI)
		{
			localPosition.x += NotificationManager.PHONE_OVERLAY_UI_CHARACTER_X_OFFSET;
		}
		this.m_quote.transform.localPosition = localPosition;
		this.m_quote.transform.localEulerAngles = Vector3.zero;
		if (!useOverlayUI && this.m_quote.rotate180InGameplay)
		{
			this.m_quote.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		}
		if (flag)
		{
			this.m_quote.transform.localScale = Vector3.one * 0.01f;
		}
		if (!string.IsNullOrEmpty(soundPath) && AssetLoader.Get().IsAssetAvailable(soundPath))
		{
			NotificationManager.QuoteSoundCallbackData quoteSoundCallbackData = new NotificationManager.QuoteSoundCallbackData();
			quoteSoundCallbackData.m_quote = this.m_quote;
			quoteSoundCallbackData.m_durationSeconds = durationSeconds;
			quoteSoundCallbackData.m_persistCharacter = persistCharacter;
			SoundLoader.LoadSound(soundPath, new PrefabCallback<GameObject>(this.OnBigQuoteSoundLoaded), quoteSoundCallbackData, SoundManager.Get().GetPlaceholderSound());
			return;
		}
		this.m_quote.PlayBirthWithForcedScale(Vector3.one);
		if (durationSeconds > 0f)
		{
			if (persistCharacter)
			{
				this.DestroySpeechBubble(this.m_quote, durationSeconds);
				return;
			}
			this.DestroyNotification(this.m_quote, durationSeconds);
		}
	}

	// Token: 0x06007F16 RID: 32534 RVA: 0x00293000 File Offset: 0x00291200
	private void PlayCharacterQuote(Notification quote, Vector3 position, string text, string soundPath, float durationSeconds, CanvasAnchor anchorPoint = CanvasAnchor.BOTTOM_LEFT)
	{
		if (this.m_quote)
		{
			UnityEngine.Object.Destroy(this.m_quote.gameObject);
		}
		this.m_quote = quote;
		this.m_quote.ChangeText(text);
		this.m_quote.transform.position = position;
		this.m_quote.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		OverlayUI.Get().AddGameObject(this.m_quote.gameObject, anchorPoint, false, CanvasScaleMode.HEIGHT);
		if (!string.IsNullOrEmpty(soundPath) && AssetLoader.Get().IsAssetAvailable(soundPath))
		{
			NotificationManager.QuoteSoundCallbackData quoteSoundCallbackData = new NotificationManager.QuoteSoundCallbackData();
			quoteSoundCallbackData.m_quote = this.m_quote;
			quoteSoundCallbackData.m_durationSeconds = durationSeconds;
			SoundLoader.LoadSound(soundPath, new PrefabCallback<GameObject>(this.OnQuoteSoundLoaded), quoteSoundCallbackData, SoundManager.Get().GetPlaceholderSound());
			return;
		}
		this.PlayQuoteWithoutSound(durationSeconds, text);
	}

	// Token: 0x06007F17 RID: 32535 RVA: 0x002930F0 File Offset: 0x002912F0
	private void PlayQuoteWithoutSound(float durationSeconds, string text = null)
	{
		this.m_quote.PlayBirthWithForcedScale(UniversalInputManager.UsePhoneUI ? this.NOTIFICATION_SCALE_PHONE : this.NOTIFICATION_SCALE);
		if (durationSeconds <= 0f && text != null)
		{
			durationSeconds = ClipLengthEstimator.StringToReadTime(text);
		}
		this.DestroyNotification(this.m_quote, durationSeconds);
	}

	// Token: 0x06007F18 RID: 32536 RVA: 0x00293144 File Offset: 0x00291344
	private void OnQuoteSoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		NotificationManager.QuoteSoundCallbackData quoteSoundCallbackData = (NotificationManager.QuoteSoundCallbackData)callbackData;
		if (!quoteSoundCallbackData.m_quote)
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		AudioSource audioSource = null;
		if (go)
		{
			audioSource = go.GetComponent<AudioSource>();
			if (audioSource && !audioSource.clip)
			{
				audioSource = null;
			}
		}
		if (!audioSource)
		{
			Log.Asset.PrintInfo("Quote Sound failed to load!", Array.Empty<object>());
			this.PlayQuoteWithoutSound((quoteSoundCallbackData.m_durationSeconds > 0f) ? quoteSoundCallbackData.m_durationSeconds : 8f, null);
			return;
		}
		this.m_quote.AssignAudio(audioSource);
		SoundManager.Get().PlayPreloaded(audioSource);
		this.m_quote.PlayBirthWithForcedScale(UniversalInputManager.UsePhoneUI ? this.NOTIFICATION_SCALE_PHONE : this.NOTIFICATION_SCALE);
		float delaySeconds = Mathf.Max(quoteSoundCallbackData.m_durationSeconds, audioSource.clip.length);
		this.DestroyNotification(this.m_quote, delaySeconds);
		if (this.m_quote.clickOff != null)
		{
			this.m_quote.clickOff.SetData(this.m_quote);
			this.m_quote.clickOff.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.ClickNotification));
		}
	}

	// Token: 0x06007F19 RID: 32537 RVA: 0x0029327C File Offset: 0x0029147C
	private void OnBigQuoteSoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		NotificationManager.QuoteSoundCallbackData quoteSoundCallbackData = (NotificationManager.QuoteSoundCallbackData)callbackData;
		if (!quoteSoundCallbackData.m_quote)
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		AudioSource audioSource = null;
		if (go)
		{
			audioSource = go.GetComponent<AudioSource>();
			if (audioSource && !audioSource.clip)
			{
				audioSource = null;
			}
		}
		if (!audioSource)
		{
			Log.Asset.PrintInfo("Quote Sound failed to load!", Array.Empty<object>());
			this.PlayQuoteWithoutSound((quoteSoundCallbackData.m_durationSeconds > 0f) ? quoteSoundCallbackData.m_durationSeconds : 8f, null);
			return;
		}
		this.m_quote.AssignAudio(audioSource);
		SoundManager.Get().PlayPreloaded(audioSource);
		this.m_quote.PlayBirthWithForcedScale(Vector3.one);
		float num = Mathf.Max(quoteSoundCallbackData.m_durationSeconds, audioSource.clip.length);
		Log.Notifications.Print("Destroying notification or speech bubble after {0} seconds. durationSeconds: {1} source.clip.length: {2} persistCharacter? {3}", new object[]
		{
			num,
			quoteSoundCallbackData.m_durationSeconds,
			audioSource.clip.length,
			quoteSoundCallbackData.m_persistCharacter
		});
		if (quoteSoundCallbackData.m_persistCharacter)
		{
			this.DestroySpeechBubble(this.m_quote, num);
		}
		else
		{
			this.DestroyNotification(this.m_quote, num);
		}
		if (this.m_quote.clickOff != null)
		{
			this.m_quote.clickOff.SetData(this.m_quote);
			this.m_quote.clickOff.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.ClickNotification));
		}
	}

	// Token: 0x06007F1A RID: 32538 RVA: 0x00293400 File Offset: 0x00291600
	public void DestroyAllArrows()
	{
		if (this.arrows.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.arrows.Count; i++)
		{
			if (this.arrows[i] != null)
			{
				this.NukeNotificationWithoutPlayingAnim(this.arrows[i]);
			}
		}
	}

	// Token: 0x06007F1B RID: 32539 RVA: 0x00293458 File Offset: 0x00291658
	public void DestroyAllPopUps()
	{
		if (this.popUpTexts.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.popUpTexts.Count; i++)
		{
			if (!(this.popUpTexts[i] == null))
			{
				this.NukeNotification(this.popUpTexts[i]);
			}
		}
		this.popUpTexts = new List<Notification>();
	}

	// Token: 0x06007F1C RID: 32540 RVA: 0x002934BC File Offset: 0x002916BC
	private void DestroyOtherNotifications(Notification.SpeechBubbleDirection direction, int speechBubbleGroup)
	{
		if (this.notificationsToDestroyUponNewNotifier.Count == 0 || !this.notificationsToDestroyUponNewNotifier.ContainsKey(speechBubbleGroup) || this.notificationsToDestroyUponNewNotifier[speechBubbleGroup] == null)
		{
			return;
		}
		for (int i = 0; i < this.notificationsToDestroyUponNewNotifier[speechBubbleGroup].Count; i++)
		{
			if (!(this.notificationsToDestroyUponNewNotifier[speechBubbleGroup][i] == null) && this.notificationsToDestroyUponNewNotifier[speechBubbleGroup][i].GetSpeechBubbleDirection() == direction)
			{
				this.NukeNotificationWithoutPlayingAnim(this.notificationsToDestroyUponNewNotifier[speechBubbleGroup][i]);
			}
		}
	}

	// Token: 0x06007F1D RID: 32541 RVA: 0x0029355B File Offset: 0x0029175B
	public void DestroyNotification(Notification notification, float delaySeconds)
	{
		if (notification == null)
		{
			return;
		}
		if (delaySeconds == 0f)
		{
			this.NukeNotification(notification);
			return;
		}
		base.StartCoroutine(this.WaitAndThenDestroyNotification(notification, delaySeconds));
	}

	// Token: 0x06007F1E RID: 32542 RVA: 0x00293586 File Offset: 0x00291786
	public void DestroySpeechBubble(Notification notification, float delaySeconds)
	{
		if (notification == null)
		{
			return;
		}
		if (delaySeconds == 0f)
		{
			this.NukeSpeechBubble(notification);
			return;
		}
		base.StartCoroutine(this.WaitAndThenDestroySpeechBubble(notification, delaySeconds));
	}

	// Token: 0x06007F1F RID: 32543 RVA: 0x002935B4 File Offset: 0x002917B4
	public void DestroyNotificationWithText(string text, float delaySeconds = 0f)
	{
		Notification notification = null;
		for (int i = 0; i < this.popUpTexts.Count; i++)
		{
			if (!(this.popUpTexts[i] == null) && this.popUpTexts[i].speechUberText.Text == text)
			{
				notification = this.popUpTexts[i];
			}
		}
		this.DestroyNotification(notification, delaySeconds);
	}

	// Token: 0x06007F20 RID: 32544 RVA: 0x00293620 File Offset: 0x00291820
	private void ClickNotification(UIEvent e)
	{
		Notification notification = (Notification)e.GetElement().GetData();
		this.NukeNotification(notification);
		notification.clickOff.RemoveEventListener(UIEventType.PRESS, new UIEvent.Handler(this.ClickNotification));
	}

	// Token: 0x06007F21 RID: 32545 RVA: 0x00293660 File Offset: 0x00291860
	public void DestroyAllNotificationsNowWithNoAnim()
	{
		if (this.popUpDialog)
		{
			this.NukeNotificationWithoutPlayingAnim(this.popUpDialog);
		}
		if (this.m_quote)
		{
			this.NukeNotificationWithoutPlayingAnim(this.m_quote);
		}
		foreach (List<Notification> list in this.notificationsToDestroyUponNewNotifier.Values)
		{
			for (int i = 0; i < list.Count; i++)
			{
				Notification notification = list[i];
				if (!(notification == null))
				{
					this.NukeNotificationWithoutPlayingAnim(notification);
				}
			}
		}
		this.DestroyAllArrows();
		this.DestroyAllPopUps();
	}

	// Token: 0x06007F22 RID: 32546 RVA: 0x00293718 File Offset: 0x00291918
	public void DestroyActiveQuote(float delaySeconds, bool ignoreAudio = false)
	{
		if (this.m_quote == null)
		{
			return;
		}
		if (ignoreAudio)
		{
			this.m_quote.ignoreAudioOnDestroy = true;
		}
		if (delaySeconds == 0f)
		{
			this.NukeNotification(this.m_quote);
			return;
		}
		base.StartCoroutine(this.WaitAndThenDestroyNotification(this.m_quote, delaySeconds));
	}

	// Token: 0x06007F23 RID: 32547 RVA: 0x0029376C File Offset: 0x0029196C
	public void DestroyNotificationNowWithNoAnim(Notification notification)
	{
		if (notification == null)
		{
			return;
		}
		this.NukeNotificationWithoutPlayingAnim(notification);
	}

	// Token: 0x06007F24 RID: 32548 RVA: 0x0029377F File Offset: 0x0029197F
	private IEnumerator WaitAndThenDestroyNotification(Notification notification, float amountSeconds)
	{
		yield return new WaitForSeconds(amountSeconds);
		if (notification != null)
		{
			this.NukeNotification(notification);
		}
		yield break;
	}

	// Token: 0x06007F25 RID: 32549 RVA: 0x0029379C File Offset: 0x0029199C
	private void NukeNotification(Notification notification)
	{
		if (notification == null)
		{
			Log.All.PrintWarning("Attempting to Nuke a Notification that does not exist!", Array.Empty<object>());
			return;
		}
		foreach (List<Notification> list in this.notificationsToDestroyUponNewNotifier.Values)
		{
			if (list.Contains(notification))
			{
				list.Remove(notification);
			}
		}
		if (notification.IsDying())
		{
			return;
		}
		notification.PlayDeath();
		UniversalInputManager.Get().SetGameDialogActive(false);
	}

	// Token: 0x06007F26 RID: 32550 RVA: 0x00293838 File Offset: 0x00291A38
	private void NukeNotificationWithoutPlayingAnim(Notification notification)
	{
		foreach (List<Notification> list in this.notificationsToDestroyUponNewNotifier.Values)
		{
			if (list.Contains(notification))
			{
				list.Remove(notification);
			}
		}
		UnityEngine.Object.Destroy(notification.gameObject);
		UniversalInputManager.Get().SetGameDialogActive(false);
	}

	// Token: 0x06007F27 RID: 32551 RVA: 0x002938B0 File Offset: 0x00291AB0
	private IEnumerator WaitAndThenDestroySpeechBubble(Notification notification, float amountSeconds)
	{
		yield return new WaitForSeconds(amountSeconds);
		if (notification != null)
		{
			this.NukeSpeechBubble(notification);
		}
		yield break;
	}

	// Token: 0x06007F28 RID: 32552 RVA: 0x002938CD File Offset: 0x00291ACD
	private void NukeSpeechBubble(Notification notification)
	{
		if (notification == null)
		{
			Log.All.PrintWarning("Attempting to Nuke a Speech Bubble for a Notification that does not exist!", Array.Empty<object>());
			return;
		}
		if (notification.IsDying())
		{
			return;
		}
		notification.PlaySpeechBubbleDeath();
	}

	// Token: 0x06007F29 RID: 32553 RVA: 0x002938FC File Offset: 0x00291AFC
	public TutorialNotification CreateTutorialDialog(string headlineGameString, string bodyTextGameString, string buttonGameString, UIEvent.Handler buttonHandler, Vector2 materialOffset, bool swapMaterial = false)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("TutorialIntroDialog.prefab:2d189389d0be2f2428bf37ace33e85b1", AssetLoadingOptions.None);
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
		if (UniversalInputManager.UsePhoneUI)
		{
			gameObject.transform.localScale = 1.5f * gameObject.transform.localScale;
		}
		this.popUpDialog = notification;
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
			this.DestroyNotification(notification, 0f);
		});
		this.popUpDialog.PlayBirth();
		UniversalInputManager.Get().SetGameDialogActive(true);
		return notification;
	}

	// Token: 0x0400667F RID: 26239
	public const string KT_PREFAB_PATH = "KT_Quote.prefab:7ad118a1a10e9ab409ade82268a378f5";

	// Token: 0x04006680 RID: 26240
	public const string TIRION_PREFAB_PATH = "Tirion_Quote.prefab:2f88f08e8896841429c972fc5c4c7088";

	// Token: 0x04006681 RID: 26241
	public const string NORMAL_NEFARIAN_PREFAB_PATH = "NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913";

	// Token: 0x04006682 RID: 26242
	public const string ZOMBIE_NEFARIAN_PREFAB_PATH = "NefarianDragon_Quote.prefab:179fec888df7e4c02b8de3b7ad109a23";

	// Token: 0x04006683 RID: 26243
	public const string RAGNAROS_PREFAB_PATH = "Ragnaros_Quote.prefab:c9e0154894cd1a946b90ebefeb481a51";

	// Token: 0x04006684 RID: 26244
	public const string MAJORDOMO_PREFAB_PATH = "Majordomo_Quote.prefab:72286f87e5b724c21aa1d92d04426614";

	// Token: 0x04006685 RID: 26245
	public const string RENO_PREFAB_PATH = "Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a";

	// Token: 0x04006686 RID: 26246
	public const string RENO_BIG_PREFAB_PATH = "Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921";

	// Token: 0x04006687 RID: 26247
	public const string CARTOGRAPHER_PREFAB_PATH = "Cartographer_Quote.prefab:c6056bfb8c0025a458553adabc8ed537";

	// Token: 0x04006688 RID: 26248
	public const string ELISE_BIG_PREFAB_PATH = "Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26";

	// Token: 0x04006689 RID: 26249
	public const string FINLEY_BIG_PREFAB_PATH = "Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee";

	// Token: 0x0400668A RID: 26250
	public const string BRANN_BIG_PREFAB_PATH = "Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b";

	// Token: 0x0400668B RID: 26251
	public const string RAFAAM_WRAP_PREFAB_PATH = "Rafaam_wrap_Quote.prefab:d7100015bf618604ea93bad6b9f54f8b";

	// Token: 0x0400668C RID: 26252
	public const string RAFAAM_WRAP_BIG_PREFAB_PATH = "Rafaam_wrap_BigQuote.prefab:ee7dbbb027adc1947b64b05f31d4c124";

	// Token: 0x0400668D RID: 26253
	public const string RAFAAM_BIG_PREFAB_PATH = "Rafaam_BigQuote.prefab:ff1fd65bf3d8ba748b144b805fca871f";

	// Token: 0x0400668E RID: 26254
	public const string RAFAAM_PREFAB_PATH = "Rafaam_Quote.prefab:d27a824bbfd6bd94185fe10e594f0014";

	// Token: 0x0400668F RID: 26255
	public const string BRANN_PREFAB_PATH = "Brann_Quote.prefab:2c11651ab7740924189734944b8d7089";

	// Token: 0x04006690 RID: 26256
	public const string BLAGGH_PREFAB_PATH = "Blaggh_Quote.prefab:f5d1e7053e6368e4a930ca3906cff53a";

	// Token: 0x04006691 RID: 26257
	public const string MEDIVH_PREFAB_PATH = "Medivh_Quote.prefab:423c4a6b7e7a7f643bf0b2992ad3d31b";

	// Token: 0x04006692 RID: 26258
	public const string MEDIVH_BIG_PREFAB_PATH = "Medivh_BigQuote.prefab:78e18a627031f6c48aef27a0fa1123c1";

	// Token: 0x04006693 RID: 26259
	public const string MEDIVAS_BIG_PREFAB_PATH = "Medivas_BigQuote.prefab:ad677b060790a304fa6caed25f19bf88";

	// Token: 0x04006694 RID: 26260
	public const string MOROES_PREFAB_PATH = "Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf";

	// Token: 0x04006695 RID: 26261
	public const string MOROES_BIG_PREFAB_PATH = "Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d";

	// Token: 0x04006696 RID: 26262
	public const string CURATOR_PREFAB_PATH = "Curator_Quote.prefab:ab58be80382875e4cbaa766fda73cd39";

	// Token: 0x04006697 RID: 26263
	public const string CURATOR_BIG_PREFAB_PATH = "Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81";

	// Token: 0x04006698 RID: 26264
	public const string BARNES_PREFAB_PATH = "Barnes_Quote.prefab:2e7e9f28b5bc37149a12b2e5feaa244a";

	// Token: 0x04006699 RID: 26265
	public const string BARNES_BIG_PREFAB_PATH = "Barnes_BigQuote.prefab:15c396b2577ab09449f3721d23da3dba";

	// Token: 0x0400669A RID: 26266
	public const string AYA_BIG_PREFAB_PATH = "Aya_BigQuote.prefab:26a19c2632327c14dbf648b96f8751d1";

	// Token: 0x0400669B RID: 26267
	public const string HANCHO_BIG_PREFAB_PATH = "HanCho_BigQuote.prefab:0b24275caed054c45b2ebcb91fd9112d";

	// Token: 0x0400669C RID: 26268
	public const string KAZAKUS_BIG_PREFAB_PATH = "Kazakus_BigQuote.prefab:b0007ae4277fc5a40a8c6f8c774ab823";

	// Token: 0x0400669D RID: 26269
	public const string LICHKING_PREFAB_PATH = "LichKing_Quote.prefab:59d5b461e0b2bbe479b7db63e0962d30";

	// Token: 0x0400669E RID: 26270
	public const string TIRION_BIG_PREFAB_PATH = "Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8";

	// Token: 0x0400669F RID: 26271
	public const string AHUNE_BIG_PREFAB_PATH = "Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482";

	// Token: 0x040066A0 RID: 26272
	public const string RAGNAROS_BIG_PREFAB_PATH = "Ragnaros_BigQuote.prefab:843c4fab946192943a909b026f755505";

	// Token: 0x040066A1 RID: 26273
	public const string DEMON_HUNTER_ILLIDAN_PREFAB_PATH = "DemonHunter_Illidan_Popup_Banner.prefab:c2b08a2b89af02e4bb9e80b08526df7a";

	// Token: 0x040066A2 RID: 26274
	public static readonly float DEPTH = -15f;

	// Token: 0x040066A3 RID: 26275
	public static readonly Vector3 LEFT_OF_FRIENDLY_HERO = new Vector3(-1f, 0f, 1f);

	// Token: 0x040066A4 RID: 26276
	public static readonly Vector3 RIGHT_OF_FRIENDLY_HERO = new Vector3(-6f, 0f, 1f);

	// Token: 0x040066A5 RID: 26277
	public static readonly Vector3 LEFT_OF_ENEMY_HERO = new Vector3(-1f, 0f, -3.5f);

	// Token: 0x040066A6 RID: 26278
	public static readonly Vector3 RIGHT_OF_ENEMY_HERO = new Vector3(-6f, 0f, -3f);

	// Token: 0x040066A7 RID: 26279
	public static readonly Vector3 DEFAULT_CHARACTER_POS = new Vector3(100f, NotificationManager.DEPTH, 24.7f);

	// Token: 0x040066A8 RID: 26280
	public static readonly Vector3 CHARACTER_POS_ABOVE_QUEST_TOAST = new Vector3(100f, 50f, 24.7f);

	// Token: 0x040066A9 RID: 26281
	public static readonly Vector3 ALT_ADVENTURE_SCREEN_POS = new Vector3(104.8f, NotificationManager.DEPTH, 131.1f);

	// Token: 0x040066AA RID: 26282
	public static readonly Vector3 PHONE_CHARACTER_POS = new Vector3(124.1f, NotificationManager.DEPTH, 24.7f);

	// Token: 0x040066AB RID: 26283
	public static readonly float PHONE_OVERLAY_UI_CHARACTER_X_OFFSET = -0.5f;

	// Token: 0x040066AC RID: 26284
	public GameObject speechBubblePrefab;

	// Token: 0x040066AD RID: 26285
	public GameObject speechIndicatorPrefab;

	// Token: 0x040066AE RID: 26286
	public GameObject bounceArrowPrefab;

	// Token: 0x040066AF RID: 26287
	public GameObject fadeArrowPrefab;

	// Token: 0x040066B0 RID: 26288
	public GameObject popupTextPrefab;

	// Token: 0x040066B1 RID: 26289
	public GameObject fancyPopupTextPrefab;

	// Token: 0x040066B2 RID: 26290
	public GameObject dialogBoxPrefab;

	// Token: 0x040066B3 RID: 26291
	public GameObject innkeeperQuotePrefab;

	// Token: 0x040066B4 RID: 26292
	private static NotificationManager s_instance;

	// Token: 0x040066B5 RID: 26293
	private Map<int, List<Notification>> notificationsToDestroyUponNewNotifier;

	// Token: 0x040066B6 RID: 26294
	private List<Notification> arrows;

	// Token: 0x040066B7 RID: 26295
	private List<Notification> popUpTexts;

	// Token: 0x040066B8 RID: 26296
	private Notification popUpDialog;

	// Token: 0x040066B9 RID: 26297
	private Notification m_quote;

	// Token: 0x040066BA RID: 26298
	private List<string> m_quotesThisSession;

	// Token: 0x040066BB RID: 26299
	private const float DEFAULT_QUOTE_DURATION = 8f;

	// Token: 0x040066BC RID: 26300
	private Vector3 NOTIFICATION_SCALE = 0.163f * Vector3.one;

	// Token: 0x040066BD RID: 26301
	private Vector3 NOTIFICATION_SCALE_PHONE = 0.326f * Vector3.one;

	// Token: 0x0200259C RID: 9628
	public enum PopupTextType
	{
		// Token: 0x0400EE2D RID: 60973
		BASIC,
		// Token: 0x0400EE2E RID: 60974
		FANCY
	}

	// Token: 0x0200259D RID: 9629
	public enum VisualEmoteType
	{
		// Token: 0x0400EE30 RID: 60976
		NONE,
		// Token: 0x0400EE31 RID: 60977
		HOT_STREAK,
		// Token: 0x0400EE32 RID: 60978
		TRIPLE,
		// Token: 0x0400EE33 RID: 60979
		TECH_UP_01,
		// Token: 0x0400EE34 RID: 60980
		TECH_UP_02,
		// Token: 0x0400EE35 RID: 60981
		TECH_UP_03,
		// Token: 0x0400EE36 RID: 60982
		TECH_UP_04,
		// Token: 0x0400EE37 RID: 60983
		TECH_UP_05,
		// Token: 0x0400EE38 RID: 60984
		TECH_UP_06,
		// Token: 0x0400EE39 RID: 60985
		BATTLEGROUNDS_01,
		// Token: 0x0400EE3A RID: 60986
		BATTLEGROUNDS_02,
		// Token: 0x0400EE3B RID: 60987
		BATTLEGROUNDS_03,
		// Token: 0x0400EE3C RID: 60988
		BATTLEGROUNDS_04,
		// Token: 0x0400EE3D RID: 60989
		BATTLEGROUNDS_05,
		// Token: 0x0400EE3E RID: 60990
		BATTLEGROUNDS_06,
		// Token: 0x0400EE3F RID: 60991
		BANANA
	}

	// Token: 0x0200259E RID: 9630
	public class SpeechBubbleOptions
	{
		// Token: 0x060133F3 RID: 78835 RVA: 0x0052EE8F File Offset: 0x0052D08F
		public NotificationManager.SpeechBubbleOptions WithSpeechText(string speechText)
		{
			this.speechText = speechText;
			return this;
		}

		// Token: 0x060133F4 RID: 78836 RVA: 0x0052EE99 File Offset: 0x0052D099
		public NotificationManager.SpeechBubbleOptions WithSpeechBubbleDirection(Notification.SpeechBubbleDirection direction)
		{
			this.direction = direction;
			return this;
		}

		// Token: 0x060133F5 RID: 78837 RVA: 0x0052EEA3 File Offset: 0x0052D0A3
		public NotificationManager.SpeechBubbleOptions WithActor(Actor actor)
		{
			this.actor = actor;
			return this;
		}

		// Token: 0x060133F6 RID: 78838 RVA: 0x0052EEAD File Offset: 0x0052D0AD
		public NotificationManager.SpeechBubbleOptions WithParentToActor(bool parentToActor)
		{
			this.parentToActor = parentToActor;
			return this;
		}

		// Token: 0x060133F7 RID: 78839 RVA: 0x0052EEB7 File Offset: 0x0052D0B7
		public NotificationManager.SpeechBubbleOptions WithDestroyWhenNewCreated(bool destroyWhenNewCreated)
		{
			this.destroyWhenNewCreated = destroyWhenNewCreated;
			return this;
		}

		// Token: 0x060133F8 RID: 78840 RVA: 0x0052EEC1 File Offset: 0x0052D0C1
		public NotificationManager.SpeechBubbleOptions WithBubbleScale(float bubbleScale)
		{
			this.bubbleScale = bubbleScale;
			return this;
		}

		// Token: 0x060133F9 RID: 78841 RVA: 0x0052EECB File Offset: 0x0052D0CB
		public NotificationManager.SpeechBubbleOptions WithVisualEmoteType(NotificationManager.VisualEmoteType visualEmoteType)
		{
			this.visualEmoteType = visualEmoteType;
			return this;
		}

		// Token: 0x060133FA RID: 78842 RVA: 0x0052EED5 File Offset: 0x0052D0D5
		public NotificationManager.SpeechBubbleOptions WithSpeechBubbleGroup(int speechBubbleGroup)
		{
			this.speechBubbleGroup = speechBubbleGroup;
			return this;
		}

		// Token: 0x060133FB RID: 78843 RVA: 0x0052EEDF File Offset: 0x0052D0DF
		public NotificationManager.SpeechBubbleOptions WithFinishCallback(Action<int> finishCallback)
		{
			this.finishCallback = finishCallback;
			return this;
		}

		// Token: 0x060133FC RID: 78844 RVA: 0x0052EEE9 File Offset: 0x0052D0E9
		public NotificationManager.SpeechBubbleOptions WithEmoteDuration(float emoteDuration)
		{
			this.emoteDuration = emoteDuration;
			return this;
		}

		// Token: 0x0400EE40 RID: 60992
		public string speechText = "";

		// Token: 0x0400EE41 RID: 60993
		public Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.BottomLeft;

		// Token: 0x0400EE42 RID: 60994
		public Actor actor;

		// Token: 0x0400EE43 RID: 60995
		public bool destroyWhenNewCreated = true;

		// Token: 0x0400EE44 RID: 60996
		public bool parentToActor = true;

		// Token: 0x0400EE45 RID: 60997
		public float bubbleScale;

		// Token: 0x0400EE46 RID: 60998
		public NotificationManager.VisualEmoteType visualEmoteType;

		// Token: 0x0400EE47 RID: 60999
		public int speechBubbleGroup;

		// Token: 0x0400EE48 RID: 61000
		public Action<int> finishCallback;

		// Token: 0x0400EE49 RID: 61001
		public float emoteDuration;
	}

	// Token: 0x0200259F RID: 9631
	private class QuoteSoundCallbackData
	{
		// Token: 0x0400EE4A RID: 61002
		public Notification m_quote;

		// Token: 0x0400EE4B RID: 61003
		public float m_durationSeconds;

		// Token: 0x0400EE4C RID: 61004
		public bool m_persistCharacter;
	}
}
