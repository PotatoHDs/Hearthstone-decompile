using System;
using System.Collections;
using UnityEngine;

// Token: 0x020008F0 RID: 2288
public class Notification : MonoBehaviour
{
	// Token: 0x17000744 RID: 1860
	// (get) Token: 0x06007EC4 RID: 32452 RVA: 0x002912FF File Offset: 0x0028F4FF
	// (set) Token: 0x06007EC5 RID: 32453 RVA: 0x00291307 File Offset: 0x0028F507
	public string PrefabPath { get; set; }

	// Token: 0x17000745 RID: 1861
	// (get) Token: 0x06007EC6 RID: 32454 RVA: 0x00291310 File Offset: 0x0028F510
	// (set) Token: 0x06007EC7 RID: 32455 RVA: 0x00291318 File Offset: 0x0028F518
	public bool PersistCharacter { get; set; }

	// Token: 0x17000746 RID: 1862
	// (get) Token: 0x06007EC8 RID: 32456 RVA: 0x00291321 File Offset: 0x0028F521
	// (set) Token: 0x06007EC9 RID: 32457 RVA: 0x00291329 File Offset: 0x0028F529
	public bool ShowWithExistingPopups { get; set; }

	// Token: 0x06007ECA RID: 32458 RVA: 0x00291334 File Offset: 0x0028F534
	private void Start()
	{
		foreach (object obj in Enum.GetValues(typeof(Notification.SpeechBubbleDirection)))
		{
			Notification.SpeechBubbleDirection speechBubbleDirection = (Notification.SpeechBubbleDirection)obj;
			GameObject speechBubble = this.GetSpeechBubble(speechBubbleDirection);
			if (speechBubble != null)
			{
				this.m_speechBubbleScales.Add(speechBubbleDirection, speechBubble.transform.localScale);
			}
		}
	}

	// Token: 0x06007ECB RID: 32459 RVA: 0x002913B8 File Offset: 0x0028F5B8
	private void LateUpdate()
	{
		if (this.upperLeftBubble != null && this.upperRightBubble != null && this.bottomLeftBubble != null && this.bottomRightBubble != null)
		{
			base.gameObject.transform.rotation = Quaternion.identity;
		}
		bool isShowing = PopupDisplayManager.Get().IsShowing;
		if (isShowing && !this.m_hiding && !this.ShowWithExistingPopups)
		{
			Debug.LogFormat("Hiding notification {0} because something else is being shown.", new object[]
			{
				base.gameObject.name
			});
			this.m_hiding = true;
			this.m_localPosition = base.transform.localPosition;
			base.transform.localPosition = this.m_hiddenPosition;
			return;
		}
		if (!isShowing && this.m_hiding)
		{
			this.m_hiding = false;
			base.transform.localPosition = this.m_localPosition;
		}
	}

	// Token: 0x06007ECC RID: 32460 RVA: 0x0029149C File Offset: 0x0028F69C
	private void OnDestroy()
	{
		if (this.m_accompaniedAudio && !this.ignoreAudioOnDestroy && SoundManager.Get() != null)
		{
			SoundManager.Get().Destroy(this.m_accompaniedAudio);
		}
		if (this.m_parentOffsetObject != null)
		{
			UnityEngine.Object.Destroy(this.m_parentOffsetObject);
		}
	}

	// Token: 0x06007ECD RID: 32461 RVA: 0x002914EE File Offset: 0x0028F6EE
	public void ChangeText(string newText)
	{
		this.speechUberText.Text = newText;
	}

	// Token: 0x06007ECE RID: 32462 RVA: 0x002914FC File Offset: 0x0028F6FC
	public void ChangeEmote(NotificationManager.VisualEmoteType emoteType)
	{
		this.techLevelEmote.SetActive(false);
		this.tripleEmote.SetActive(false);
		this.winStreakEmote.SetActive(false);
		this.bgEmote01.SetActive(false);
		this.bgEmote02.SetActive(false);
		this.bgEmote03.SetActive(false);
		this.bgEmote04.SetActive(false);
		this.bgEmote05.SetActive(false);
		this.bgEmote06.SetActive(false);
		switch (emoteType)
		{
		case NotificationManager.VisualEmoteType.HOT_STREAK:
			this.winStreakEmote.SetActive(true);
			return;
		case NotificationManager.VisualEmoteType.TRIPLE:
			this.tripleEmote.SetActive(true);
			return;
		case NotificationManager.VisualEmoteType.TECH_UP_01:
			this.techLevelEmote.SetActive(true);
			this.UpdateTechLevelPlaymaker(1);
			return;
		case NotificationManager.VisualEmoteType.TECH_UP_02:
			this.techLevelEmote.SetActive(true);
			this.UpdateTechLevelPlaymaker(2);
			return;
		case NotificationManager.VisualEmoteType.TECH_UP_03:
			this.techLevelEmote.SetActive(true);
			this.UpdateTechLevelPlaymaker(3);
			return;
		case NotificationManager.VisualEmoteType.TECH_UP_04:
			this.techLevelEmote.SetActive(true);
			this.UpdateTechLevelPlaymaker(4);
			return;
		case NotificationManager.VisualEmoteType.TECH_UP_05:
			this.techLevelEmote.SetActive(true);
			this.UpdateTechLevelPlaymaker(5);
			return;
		case NotificationManager.VisualEmoteType.TECH_UP_06:
			this.techLevelEmote.SetActive(true);
			this.UpdateTechLevelPlaymaker(6);
			return;
		case NotificationManager.VisualEmoteType.BATTLEGROUNDS_01:
			this.bgEmote01.SetActive(true);
			return;
		case NotificationManager.VisualEmoteType.BATTLEGROUNDS_02:
			this.bgEmote02.SetActive(true);
			return;
		case NotificationManager.VisualEmoteType.BATTLEGROUNDS_03:
			this.bgEmote03.SetActive(true);
			return;
		case NotificationManager.VisualEmoteType.BATTLEGROUNDS_04:
			this.bgEmote04.SetActive(true);
			return;
		case NotificationManager.VisualEmoteType.BATTLEGROUNDS_05:
			this.bgEmote05.SetActive(true);
			return;
		case NotificationManager.VisualEmoteType.BATTLEGROUNDS_06:
			this.bgEmote06.SetActive(true);
			return;
		case NotificationManager.VisualEmoteType.BANANA:
			this.bananaEmote.SetActive(true);
			return;
		default:
			return;
		}
	}

	// Token: 0x06007ECF RID: 32463 RVA: 0x002916A8 File Offset: 0x0028F8A8
	private void UpdateTechLevelPlaymaker(int techLevel)
	{
		PlayMakerFSM component = this.techLevelEmote.GetComponent<PlayMakerFSM>();
		if (component == null)
		{
			Log.Gameplay.PrintError("No playmaker attached to tech level icon.", Array.Empty<object>());
			return;
		}
		component.FsmVariables.GetFsmInt("TechLevel").Value = techLevel;
		component.SendEvent("Action");
	}

	// Token: 0x06007ED0 RID: 32464 RVA: 0x00291700 File Offset: 0x0028F900
	public void ChangeDialogText(string headlineString, string bodyString, string yesOrOKstring, string noString)
	{
		this.speechUberText.Text = bodyString;
		this.headlineUberText.Text = headlineString;
	}

	// Token: 0x06007ED1 RID: 32465 RVA: 0x0029171C File Offset: 0x0028F91C
	public void RepositionSpeechBubbleAroundBigQuote(Notification.SpeechBubbleDirection direction, bool animateSpeechBubble)
	{
		GameObject gameObject = this.FaceDirection(direction);
		if (animateSpeechBubble)
		{
			Notification.PlayBirthAnim(gameObject, gameObject.transform.localScale * 0.75f, gameObject.transform.localScale);
		}
		TransformUtil.AttachAndPreserveLocalTransform(this.speechUberText.transform, gameObject.transform);
	}

	// Token: 0x06007ED2 RID: 32466 RVA: 0x00291770 File Offset: 0x0028F970
	public GameObject FaceDirection(Notification.SpeechBubbleDirection direction)
	{
		this.m_bubbleDirection = direction;
		foreach (object obj in Enum.GetValues(typeof(Notification.SpeechBubbleDirection)))
		{
			Notification.SpeechBubbleDirection direction2 = (Notification.SpeechBubbleDirection)obj;
			GameObject speechBubble = this.GetSpeechBubble(direction2);
			if (speechBubble != null)
			{
				iTween.Stop(speechBubble);
				speechBubble.GetComponent<Renderer>().enabled = false;
			}
		}
		GameObject speechBubble2 = this.GetSpeechBubble(direction);
		if (speechBubble2 != null)
		{
			if (this.m_speechBubbleScales.ContainsKey(direction))
			{
				speechBubble2.transform.localScale = this.m_speechBubbleScales[direction];
			}
			speechBubble2.GetComponent<Renderer>().enabled = true;
		}
		return speechBubble2;
	}

	// Token: 0x06007ED3 RID: 32467 RVA: 0x0029183C File Offset: 0x0028FA3C
	private GameObject GetSpeechBubble(Notification.SpeechBubbleDirection direction)
	{
		switch (direction)
		{
		case Notification.SpeechBubbleDirection.TopLeft:
			return this.upperLeftBubble;
		case Notification.SpeechBubbleDirection.TopRight:
			return this.upperRightBubble;
		case Notification.SpeechBubbleDirection.BottomLeft:
			return this.bottomLeftBubble;
		case Notification.SpeechBubbleDirection.BottomRight:
			return this.bottomRightBubble;
		case Notification.SpeechBubbleDirection.MiddleLeft:
			return this.leftBubble;
		default:
			return null;
		}
	}

	// Token: 0x06007ED4 RID: 32468 RVA: 0x0029188C File Offset: 0x0028FA8C
	public void PlaySpeechBubbleDeath()
	{
		Notification.SpeechBubbleDirection bubbleDirection = this.m_bubbleDirection;
		GameObject speechBubble = this.GetSpeechBubble(bubbleDirection);
		if (speechBubble != null)
		{
			iTween.ScaleTo(speechBubble, iTween.Hash(new object[]
			{
				"scale",
				Vector3.zero,
				"easetype",
				iTween.EaseType.easeInExpo,
				"time",
				0.5f,
				"oncomplete",
				"OnBubbleDeathComplete",
				"oncompletetarget",
				base.gameObject,
				"oncompleteparams",
				bubbleDirection
			}));
		}
	}

	// Token: 0x06007ED5 RID: 32469 RVA: 0x00291938 File Offset: 0x0028FB38
	private void OnBubbleDeathComplete(Notification.SpeechBubbleDirection direction)
	{
		GameObject speechBubble = this.GetSpeechBubble(direction);
		if (speechBubble != null)
		{
			speechBubble.GetComponent<Renderer>().enabled = false;
		}
	}

	// Token: 0x06007ED6 RID: 32470 RVA: 0x00291962 File Offset: 0x0028FB62
	public Notification.SpeechBubbleDirection GetSpeechBubbleDirection()
	{
		return this.m_bubbleDirection;
	}

	// Token: 0x06007ED7 RID: 32471 RVA: 0x0029196C File Offset: 0x0028FB6C
	public void ShowPopUpArrow(Notification.PopUpArrowDirection direction)
	{
		switch (direction)
		{
		case Notification.PopUpArrowDirection.Left:
			this.leftPopupArrow.GetComponent<Renderer>().enabled = true;
			return;
		case Notification.PopUpArrowDirection.Right:
			this.rightPopupArrow.GetComponent<Renderer>().enabled = true;
			return;
		case Notification.PopUpArrowDirection.Down:
			this.bottomPopupArrow.GetComponent<Renderer>().enabled = true;
			return;
		case Notification.PopUpArrowDirection.Up:
			this.topPopupArrow.GetComponent<Renderer>().enabled = true;
			return;
		case Notification.PopUpArrowDirection.LeftDown:
			this.bottomLeftPopupArrow.GetComponent<Renderer>().enabled = true;
			return;
		case Notification.PopUpArrowDirection.RightDown:
			this.bottomRightPopupArrow.GetComponent<Renderer>().enabled = true;
			return;
		case Notification.PopUpArrowDirection.RightUp:
			this.topRightPopupArrow.GetComponent<Renderer>().enabled = true;
			return;
		case Notification.PopUpArrowDirection.LeftUp:
			this.topLeftPopupArrow.GetComponent<Renderer>().enabled = true;
			return;
		case Notification.PopUpArrowDirection.BottomThree:
			this.bottomLeftPopupArrow.GetComponent<Renderer>().enabled = true;
			this.bottomRightPopupArrow.GetComponent<Renderer>().enabled = true;
			this.bottomPopupArrow.GetComponent<Renderer>().enabled = true;
			return;
		case Notification.PopUpArrowDirection.TopThree:
			this.topLeftPopupArrow.GetComponent<Renderer>().enabled = true;
			this.topRightPopupArrow.GetComponent<Renderer>().enabled = true;
			this.topPopupArrow.GetComponent<Renderer>().enabled = true;
			return;
		default:
			return;
		}
	}

	// Token: 0x06007ED8 RID: 32472 RVA: 0x00291AA0 File Offset: 0x0028FCA0
	public void SetPosition(Actor actor, Notification.SpeechBubbleDirection direction)
	{
		if (actor.GetBones() == null)
		{
			Debug.LogError("Notification Error - Tried to set the position of a Speech Bubble, but the target actor has no bones!");
			return;
		}
		GameObject gameObject = SceneUtils.FindChildBySubstring(actor.GetBones(), "SpeechBubbleBones");
		if (gameObject == null)
		{
			Debug.LogError("Notification Error - Tried to set the position of a Speech Bubble, but the target actor has no SpeechBubbleBones!");
			return;
		}
		Vector3 position = Vector3.zero;
		switch (direction)
		{
		case Notification.SpeechBubbleDirection.TopLeft:
			position = SceneUtils.FindChildBySubstring(gameObject, "BottomRight").transform.position;
			break;
		case Notification.SpeechBubbleDirection.TopRight:
			position = SceneUtils.FindChildBySubstring(gameObject, "BottomLeft").transform.position;
			break;
		case Notification.SpeechBubbleDirection.BottomLeft:
			position = SceneUtils.FindChildBySubstring(gameObject, "TopRight").transform.position;
			break;
		case Notification.SpeechBubbleDirection.BottomRight:
			position = SceneUtils.FindChildBySubstring(gameObject, "TopLeft").transform.position;
			break;
		case Notification.SpeechBubbleDirection.MiddleLeft:
			position = SceneUtils.FindChildBySubstring(gameObject, "MiddleRight").transform.position;
			break;
		}
		base.transform.position = position;
	}

	// Token: 0x06007ED9 RID: 32473 RVA: 0x00291B91 File Offset: 0x0028FD91
	public void SetPosition(Vector3 position)
	{
		base.transform.position = position;
	}

	// Token: 0x06007EDA RID: 32474 RVA: 0x00291BA0 File Offset: 0x0028FDA0
	public void SetPositionForSmallBubble(Actor actor)
	{
		if (actor.GetBones() == null)
		{
			Debug.LogError("Notification Error - Tried to set the position of a Speech Bubble, but the target actor has no bones!");
			return;
		}
		GameObject gameObject = SceneUtils.FindChildBySubstring(actor.GetBones(), "SpeechBubbleBones");
		if (gameObject == null)
		{
			Debug.LogError("Notification Error - Tried to set the position of a Speech Bubble, but the target actor has no SpeechBubbleBones!");
			return;
		}
		base.transform.position = SceneUtils.FindChildBySubstring(gameObject, "SmallBubble").transform.position;
	}

	// Token: 0x06007EDB RID: 32475 RVA: 0x00291C0B File Offset: 0x0028FE0B
	public void CloseWithoutAnimation()
	{
		this.FinishDeath();
	}

	// Token: 0x06007EDC RID: 32476 RVA: 0x00291C13 File Offset: 0x0028FE13
	private void FinishDeath()
	{
		UnityEngine.Object.Destroy(base.gameObject);
		if (this.OnFinishDeathState != null)
		{
			this.OnFinishDeathState(this.notificationGroup);
		}
	}

	// Token: 0x06007EDD RID: 32477 RVA: 0x00291C3C File Offset: 0x0028FE3C
	public void PlayDeath()
	{
		if (this.destroyEvent != null)
		{
			this.destroyEvent.Activate();
		}
		if (this.bounceObject != null || this.fadeArrowObject != null)
		{
			this.FinishDeath();
			return;
		}
		this.isDying = true;
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			Vector3.zero,
			"easetype",
			iTween.EaseType.easeInExpo,
			"time",
			0.5f,
			"oncomplete",
			"FinishDeath",
			"oncompletetarget",
			base.gameObject
		}));
	}

	// Token: 0x06007EDE RID: 32478 RVA: 0x00291D04 File Offset: 0x0028FF04
	public void Shrink(float duration = -1f)
	{
		this.m_shrunk = true;
		if (duration < 0f)
		{
			duration = 0.5f;
		}
		iTween.Stop(base.gameObject);
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			Vector3.zero,
			"easetype",
			iTween.EaseType.easeInExpo,
			"time",
			duration
		}));
	}

	// Token: 0x06007EDF RID: 32479 RVA: 0x00291D80 File Offset: 0x0028FF80
	public void Unshrink(float duration = -1f)
	{
		if (this.isDying)
		{
			return;
		}
		if (duration < 0f)
		{
			duration = 0.5f;
		}
		iTween.Stop(base.gameObject);
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			this.m_initialScale,
			"easetype",
			iTween.EaseType.easeInExpo,
			"time",
			duration
		}));
		this.m_shrunk = false;
	}

	// Token: 0x06007EE0 RID: 32480 RVA: 0x00291E06 File Offset: 0x00290006
	public bool IsDying()
	{
		return this.isDying;
	}

	// Token: 0x06007EE1 RID: 32481 RVA: 0x00291E10 File Offset: 0x00290010
	public void PlayBirth()
	{
		if (this.showEvent != null)
		{
			this.showEvent.Activate();
		}
		if (this.bounceObject == null && this.fadeArrowObject == null)
		{
			Vector3 localScale = base.transform.localScale;
			Notification.PlayBirthAnim(base.gameObject, new Vector3(0.01f, 0.01f, 0.01f), localScale);
			this.m_initialScale = localScale;
			return;
		}
		if (this.bounceObject != null)
		{
			this.BounceDown();
			return;
		}
		if (this.fadeArrowObject != null)
		{
			this.FadeOut();
		}
	}

	// Token: 0x06007EE2 RID: 32482 RVA: 0x00291EAF File Offset: 0x002900AF
	public void PlayBirthWithForcedScale(Vector3 targetScale)
	{
		Notification.PlayBirthAnim(base.gameObject, base.gameObject.transform.localScale, targetScale);
		this.m_initialScale = base.transform.localScale;
	}

	// Token: 0x06007EE3 RID: 32483 RVA: 0x00291EE0 File Offset: 0x002900E0
	public void PlaySmallBirthForFakeBubble()
	{
		if (this.showEvent != null)
		{
			this.showEvent.Activate();
		}
		if (this.bounceObject == null && this.fadeArrowObject == null)
		{
			float num = 0.25f;
			Vector3 targetScale = new Vector3(num * base.transform.localScale.x, num * base.transform.localScale.y, num * base.transform.localScale.z);
			Vector3 startingScale = new Vector3(0.01f, 0.01f, 0.01f);
			Notification.PlayBirthAnim(base.gameObject, startingScale, targetScale);
			return;
		}
		this.BounceDown();
	}

	// Token: 0x06007EE4 RID: 32484 RVA: 0x00291F90 File Offset: 0x00290190
	public static void PlayBirthAnim(GameObject gameObject, Vector3 startingScale, Vector3 targetScale)
	{
		gameObject.transform.localScale = startingScale;
		iTween.ScaleTo(gameObject, iTween.Hash(new object[]
		{
			"scale",
			targetScale,
			"easetype",
			iTween.EaseType.easeOutElastic,
			"time",
			1f
		}));
	}

	// Token: 0x06007EE5 RID: 32485 RVA: 0x00291FF2 File Offset: 0x002901F2
	public void PulseReminderEveryXSeconds(float seconds)
	{
		base.StartCoroutine(this.PulseReminder(seconds));
	}

	// Token: 0x06007EE6 RID: 32486 RVA: 0x00292002 File Offset: 0x00290202
	private IEnumerator PulseReminder(float seconds)
	{
		WaitForSeconds waitForSecs = new WaitForSeconds(seconds);
		while (!this.isDying)
		{
			yield return waitForSecs;
			if (!this.m_shrunk)
			{
				iTween.PunchScale(base.gameObject, iTween.Hash(new object[]
				{
					"amount",
					Vector3.one,
					"time",
					1f
				}));
			}
		}
		yield break;
	}

	// Token: 0x06007EE7 RID: 32487 RVA: 0x00292018 File Offset: 0x00290218
	private void BounceUp()
	{
		iTween.MoveTo(this.bounceObject, iTween.Hash(new object[]
		{
			"islocal",
			true,
			"z",
			this.bounceObject.transform.localPosition.z - 0.5f,
			"time",
			0.75f,
			"easetype",
			iTween.EaseType.easeInCubic,
			"oncomplete",
			"BounceDown",
			"oncompletetarget",
			base.gameObject
		}));
	}

	// Token: 0x06007EE8 RID: 32488 RVA: 0x002920C4 File Offset: 0x002902C4
	private void BounceDown()
	{
		iTween.MoveTo(this.bounceObject, iTween.Hash(new object[]
		{
			"islocal",
			true,
			"z",
			this.bounceObject.transform.localPosition.z + 0.5f,
			"time",
			0.75f,
			"easetype",
			iTween.EaseType.easeOutCubic,
			"oncomplete",
			"BounceUp",
			"oncompletetarget",
			base.gameObject
		}));
	}

	// Token: 0x06007EE9 RID: 32489 RVA: 0x00292170 File Offset: 0x00290370
	private void FadeOut()
	{
		iTween.MoveTo(this.fadeArrowObject, iTween.Hash(new object[]
		{
			"islocal",
			true,
			"z",
			this.fadeArrowObject.transform.localPosition.z - 0.5f,
			"time",
			0.5f,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"FadeComplete",
			"oncompletetarget",
			base.gameObject
		}));
		AnimationUtil.FadeTexture(this.fadeArrowObject.GetComponentInChildren<MeshRenderer>(), 1f, 0f, 0.5f, 0.15f, null);
	}

	// Token: 0x06007EEA RID: 32490 RVA: 0x00292240 File Offset: 0x00290440
	private void FadeComplete()
	{
		iTween.MoveTo(this.fadeArrowObject, iTween.Hash(new object[]
		{
			"islocal",
			true,
			"z",
			this.fadeArrowObject.transform.localPosition.z + 0.5f,
			"time",
			0f,
			"delay",
			0.5f,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"FadeOut",
			"oncompletetarget",
			base.gameObject
		}));
		AnimationUtil.FadeTexture(this.fadeArrowObject.GetComponentInChildren<MeshRenderer>(), 0f, 1f, 0f, 0.85f, null);
	}

	// Token: 0x06007EEB RID: 32491 RVA: 0x00292327 File Offset: 0x00290527
	public void AssignAudio(AudioSource source)
	{
		this.m_accompaniedAudio = source;
	}

	// Token: 0x06007EEC RID: 32492 RVA: 0x00292330 File Offset: 0x00290530
	public AudioSource GetAudio()
	{
		return this.m_accompaniedAudio;
	}

	// Token: 0x06007EED RID: 32493 RVA: 0x00292338 File Offset: 0x00290538
	public GameObject GetParentOffsetObject()
	{
		return this.m_parentOffsetObject;
	}

	// Token: 0x06007EEE RID: 32494 RVA: 0x00292340 File Offset: 0x00290540
	public void SetParentOffsetObject(GameObject parentOffset)
	{
		if (this.m_parentOffsetObject != null)
		{
			base.transform.parent = null;
			UnityEngine.Object.Destroy(this.m_parentOffsetObject);
		}
		this.m_parentOffsetObject = parentOffset;
		base.transform.SetParent(parentOffset.transform);
	}

	// Token: 0x06007EEF RID: 32495 RVA: 0x0029237F File Offset: 0x0029057F
	public void SetClickBlockerActive(bool active)
	{
		if (this.clickBlocker != null)
		{
			this.clickBlocker.gameObject.SetActive(active);
		}
	}

	// Token: 0x04006646 RID: 26182
	public bool rotate180InGameplay;

	// Token: 0x04006647 RID: 26183
	public UberText speechUberText;

	// Token: 0x04006648 RID: 26184
	public UberText headlineUberText;

	// Token: 0x04006649 RID: 26185
	public GameObject upperLeftBubble;

	// Token: 0x0400664A RID: 26186
	public GameObject bottomLeftBubble;

	// Token: 0x0400664B RID: 26187
	public GameObject upperRightBubble;

	// Token: 0x0400664C RID: 26188
	public GameObject bottomRightBubble;

	// Token: 0x0400664D RID: 26189
	public GameObject leftBubble;

	// Token: 0x0400664E RID: 26190
	public GameObject rightBubble;

	// Token: 0x0400664F RID: 26191
	public GameObject bounceObject;

	// Token: 0x04006650 RID: 26192
	public GameObject fadeArrowObject;

	// Token: 0x04006651 RID: 26193
	public GameObject leftPopupArrow;

	// Token: 0x04006652 RID: 26194
	public GameObject rightPopupArrow;

	// Token: 0x04006653 RID: 26195
	public GameObject bottomPopupArrow;

	// Token: 0x04006654 RID: 26196
	public GameObject topPopupArrow;

	// Token: 0x04006655 RID: 26197
	public GameObject bottomLeftPopupArrow;

	// Token: 0x04006656 RID: 26198
	public GameObject bottomRightPopupArrow;

	// Token: 0x04006657 RID: 26199
	public GameObject topRightPopupArrow;

	// Token: 0x04006658 RID: 26200
	public GameObject topLeftPopupArrow;

	// Token: 0x04006659 RID: 26201
	public GameObject winStreakEmote;

	// Token: 0x0400665A RID: 26202
	public GameObject tripleEmote;

	// Token: 0x0400665B RID: 26203
	public GameObject techLevelEmote;

	// Token: 0x0400665C RID: 26204
	public GameObject bgEmote01;

	// Token: 0x0400665D RID: 26205
	public GameObject bgEmote02;

	// Token: 0x0400665E RID: 26206
	public GameObject bgEmote03;

	// Token: 0x0400665F RID: 26207
	public GameObject bgEmote04;

	// Token: 0x04006660 RID: 26208
	public GameObject bgEmote05;

	// Token: 0x04006661 RID: 26209
	public GameObject bgEmote06;

	// Token: 0x04006662 RID: 26210
	public GameObject bananaEmote;

	// Token: 0x04006663 RID: 26211
	public Spell showEvent;

	// Token: 0x04006664 RID: 26212
	public Spell destroyEvent;

	// Token: 0x04006665 RID: 26213
	public PegUIElement clickOff;

	// Token: 0x04006666 RID: 26214
	public BoxCollider clickBlocker;

	// Token: 0x04006667 RID: 26215
	public bool ignoreAudioOnDestroy;

	// Token: 0x04006668 RID: 26216
	public MeshRenderer artOverlay;

	// Token: 0x04006669 RID: 26217
	public Material swapMaterial;

	// Token: 0x0400666A RID: 26218
	public Action<int> OnFinishDeathState;

	// Token: 0x0400666D RID: 26221
	private const float BOUNCE_SPEED = 0.75f;

	// Token: 0x0400666E RID: 26222
	private const float FADE_SPEED = 0.5f;

	// Token: 0x0400666F RID: 26223
	private const float FADE_PAUSE = 0.85f;

	// Token: 0x04006670 RID: 26224
	private const int MAX_CHARACTERS = 20;

	// Token: 0x04006671 RID: 26225
	private const int MAX_CHARACTERS_IN_DIALOG = 28;

	// Token: 0x04006672 RID: 26226
	public const float DEATH_ANIMATION_DURATION = 0.5f;

	// Token: 0x04006673 RID: 26227
	private bool isDying;

	// Token: 0x04006674 RID: 26228
	private AudioSource m_accompaniedAudio;

	// Token: 0x04006675 RID: 26229
	private Notification.SpeechBubbleDirection m_bubbleDirection;

	// Token: 0x04006676 RID: 26230
	private Vector3 m_initialScale;

	// Token: 0x04006677 RID: 26231
	private GameObject m_parentOffsetObject;

	// Token: 0x04006678 RID: 26232
	private Map<Notification.SpeechBubbleDirection, Vector3> m_speechBubbleScales = new Map<Notification.SpeechBubbleDirection, Vector3>();

	// Token: 0x04006679 RID: 26233
	private Vector3 m_localPosition = Vector3.zero;

	// Token: 0x0400667A RID: 26234
	private Vector3 m_hiddenPosition = new Vector3(999f, 999f, 999f);

	// Token: 0x0400667B RID: 26235
	public int notificationGroup;

	// Token: 0x0400667C RID: 26236
	private bool m_hiding;

	// Token: 0x0400667D RID: 26237
	private bool m_shrunk;

	// Token: 0x02002599 RID: 9625
	public enum SpeechBubbleDirection
	{
		// Token: 0x0400EE16 RID: 60950
		None,
		// Token: 0x0400EE17 RID: 60951
		TopLeft,
		// Token: 0x0400EE18 RID: 60952
		TopRight,
		// Token: 0x0400EE19 RID: 60953
		BottomLeft,
		// Token: 0x0400EE1A RID: 60954
		BottomRight,
		// Token: 0x0400EE1B RID: 60955
		MiddleLeft
	}

	// Token: 0x0200259A RID: 9626
	public enum PopUpArrowDirection
	{
		// Token: 0x0400EE1D RID: 60957
		Left,
		// Token: 0x0400EE1E RID: 60958
		Right,
		// Token: 0x0400EE1F RID: 60959
		Down,
		// Token: 0x0400EE20 RID: 60960
		Up,
		// Token: 0x0400EE21 RID: 60961
		LeftDown,
		// Token: 0x0400EE22 RID: 60962
		RightDown,
		// Token: 0x0400EE23 RID: 60963
		RightUp,
		// Token: 0x0400EE24 RID: 60964
		LeftUp,
		// Token: 0x0400EE25 RID: 60965
		BottomThree,
		// Token: 0x0400EE26 RID: 60966
		TopThree
	}
}
