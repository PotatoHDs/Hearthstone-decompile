using System;
using System.Collections;
using UnityEngine;

public class Notification : MonoBehaviour
{
	public enum SpeechBubbleDirection
	{
		None,
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight,
		MiddleLeft
	}

	public enum PopUpArrowDirection
	{
		Left,
		Right,
		Down,
		Up,
		LeftDown,
		RightDown,
		RightUp,
		LeftUp,
		BottomThree,
		TopThree
	}

	public bool rotate180InGameplay;

	public UberText speechUberText;

	public UberText headlineUberText;

	public GameObject upperLeftBubble;

	public GameObject bottomLeftBubble;

	public GameObject upperRightBubble;

	public GameObject bottomRightBubble;

	public GameObject leftBubble;

	public GameObject rightBubble;

	public GameObject bounceObject;

	public GameObject fadeArrowObject;

	public GameObject leftPopupArrow;

	public GameObject rightPopupArrow;

	public GameObject bottomPopupArrow;

	public GameObject topPopupArrow;

	public GameObject bottomLeftPopupArrow;

	public GameObject bottomRightPopupArrow;

	public GameObject topRightPopupArrow;

	public GameObject topLeftPopupArrow;

	public GameObject winStreakEmote;

	public GameObject tripleEmote;

	public GameObject techLevelEmote;

	public GameObject bgEmote01;

	public GameObject bgEmote02;

	public GameObject bgEmote03;

	public GameObject bgEmote04;

	public GameObject bgEmote05;

	public GameObject bgEmote06;

	public GameObject bananaEmote;

	public Spell showEvent;

	public Spell destroyEvent;

	public PegUIElement clickOff;

	public BoxCollider clickBlocker;

	public bool ignoreAudioOnDestroy;

	public MeshRenderer artOverlay;

	public Material swapMaterial;

	public Action<int> OnFinishDeathState;

	private const float BOUNCE_SPEED = 0.75f;

	private const float FADE_SPEED = 0.5f;

	private const float FADE_PAUSE = 0.85f;

	private const int MAX_CHARACTERS = 20;

	private const int MAX_CHARACTERS_IN_DIALOG = 28;

	public const float DEATH_ANIMATION_DURATION = 0.5f;

	private bool isDying;

	private AudioSource m_accompaniedAudio;

	private SpeechBubbleDirection m_bubbleDirection;

	private Vector3 m_initialScale;

	private GameObject m_parentOffsetObject;

	private Map<SpeechBubbleDirection, Vector3> m_speechBubbleScales = new Map<SpeechBubbleDirection, Vector3>();

	private Vector3 m_localPosition = Vector3.zero;

	private Vector3 m_hiddenPosition = new Vector3(999f, 999f, 999f);

	public int notificationGroup;

	private bool m_hiding;

	private bool m_shrunk;

	public string PrefabPath { get; set; }

	public bool PersistCharacter { get; set; }

	public bool ShowWithExistingPopups { get; set; }

	private void Start()
	{
		foreach (SpeechBubbleDirection value in Enum.GetValues(typeof(SpeechBubbleDirection)))
		{
			GameObject speechBubble = GetSpeechBubble(value);
			if (speechBubble != null)
			{
				m_speechBubbleScales.Add(value, speechBubble.transform.localScale);
			}
		}
	}

	private void LateUpdate()
	{
		if (upperLeftBubble != null && upperRightBubble != null && bottomLeftBubble != null && bottomRightBubble != null)
		{
			base.gameObject.transform.rotation = Quaternion.identity;
		}
		bool isShowing = PopupDisplayManager.Get().IsShowing;
		if (isShowing && !m_hiding && !ShowWithExistingPopups)
		{
			Debug.LogFormat("Hiding notification {0} because something else is being shown.", base.gameObject.name);
			m_hiding = true;
			m_localPosition = base.transform.localPosition;
			base.transform.localPosition = m_hiddenPosition;
		}
		else if (!isShowing && m_hiding)
		{
			m_hiding = false;
			base.transform.localPosition = m_localPosition;
		}
	}

	private void OnDestroy()
	{
		if ((bool)m_accompaniedAudio && !ignoreAudioOnDestroy && SoundManager.Get() != null)
		{
			SoundManager.Get().Destroy(m_accompaniedAudio);
		}
		if (m_parentOffsetObject != null)
		{
			UnityEngine.Object.Destroy(m_parentOffsetObject);
		}
	}

	public void ChangeText(string newText)
	{
		speechUberText.Text = newText;
	}

	public void ChangeEmote(NotificationManager.VisualEmoteType emoteType)
	{
		techLevelEmote.SetActive(value: false);
		tripleEmote.SetActive(value: false);
		winStreakEmote.SetActive(value: false);
		bgEmote01.SetActive(value: false);
		bgEmote02.SetActive(value: false);
		bgEmote03.SetActive(value: false);
		bgEmote04.SetActive(value: false);
		bgEmote05.SetActive(value: false);
		bgEmote06.SetActive(value: false);
		switch (emoteType)
		{
		case NotificationManager.VisualEmoteType.TECH_UP_01:
			techLevelEmote.SetActive(value: true);
			UpdateTechLevelPlaymaker(1);
			break;
		case NotificationManager.VisualEmoteType.TECH_UP_02:
			techLevelEmote.SetActive(value: true);
			UpdateTechLevelPlaymaker(2);
			break;
		case NotificationManager.VisualEmoteType.TECH_UP_03:
			techLevelEmote.SetActive(value: true);
			UpdateTechLevelPlaymaker(3);
			break;
		case NotificationManager.VisualEmoteType.TECH_UP_04:
			techLevelEmote.SetActive(value: true);
			UpdateTechLevelPlaymaker(4);
			break;
		case NotificationManager.VisualEmoteType.TECH_UP_05:
			techLevelEmote.SetActive(value: true);
			UpdateTechLevelPlaymaker(5);
			break;
		case NotificationManager.VisualEmoteType.TECH_UP_06:
			techLevelEmote.SetActive(value: true);
			UpdateTechLevelPlaymaker(6);
			break;
		case NotificationManager.VisualEmoteType.TRIPLE:
			tripleEmote.SetActive(value: true);
			break;
		case NotificationManager.VisualEmoteType.HOT_STREAK:
			winStreakEmote.SetActive(value: true);
			break;
		case NotificationManager.VisualEmoteType.BANANA:
			bananaEmote.SetActive(value: true);
			break;
		case NotificationManager.VisualEmoteType.BATTLEGROUNDS_01:
			bgEmote01.SetActive(value: true);
			break;
		case NotificationManager.VisualEmoteType.BATTLEGROUNDS_02:
			bgEmote02.SetActive(value: true);
			break;
		case NotificationManager.VisualEmoteType.BATTLEGROUNDS_03:
			bgEmote03.SetActive(value: true);
			break;
		case NotificationManager.VisualEmoteType.BATTLEGROUNDS_04:
			bgEmote04.SetActive(value: true);
			break;
		case NotificationManager.VisualEmoteType.BATTLEGROUNDS_05:
			bgEmote05.SetActive(value: true);
			break;
		case NotificationManager.VisualEmoteType.BATTLEGROUNDS_06:
			bgEmote06.SetActive(value: true);
			break;
		}
	}

	private void UpdateTechLevelPlaymaker(int techLevel)
	{
		PlayMakerFSM component = techLevelEmote.GetComponent<PlayMakerFSM>();
		if (component == null)
		{
			Log.Gameplay.PrintError("No playmaker attached to tech level icon.");
			return;
		}
		component.FsmVariables.GetFsmInt("TechLevel").Value = techLevel;
		component.SendEvent("Action");
	}

	public void ChangeDialogText(string headlineString, string bodyString, string yesOrOKstring, string noString)
	{
		speechUberText.Text = bodyString;
		headlineUberText.Text = headlineString;
	}

	public void RepositionSpeechBubbleAroundBigQuote(SpeechBubbleDirection direction, bool animateSpeechBubble)
	{
		GameObject gameObject = FaceDirection(direction);
		if (animateSpeechBubble)
		{
			PlayBirthAnim(gameObject, gameObject.transform.localScale * 0.75f, gameObject.transform.localScale);
		}
		TransformUtil.AttachAndPreserveLocalTransform(speechUberText.transform, gameObject.transform);
	}

	public GameObject FaceDirection(SpeechBubbleDirection direction)
	{
		m_bubbleDirection = direction;
		foreach (SpeechBubbleDirection value in Enum.GetValues(typeof(SpeechBubbleDirection)))
		{
			GameObject speechBubble = GetSpeechBubble(value);
			if (speechBubble != null)
			{
				iTween.Stop(speechBubble);
				speechBubble.GetComponent<Renderer>().enabled = false;
			}
		}
		GameObject speechBubble2 = GetSpeechBubble(direction);
		if (speechBubble2 != null)
		{
			if (m_speechBubbleScales.ContainsKey(direction))
			{
				speechBubble2.transform.localScale = m_speechBubbleScales[direction];
			}
			speechBubble2.GetComponent<Renderer>().enabled = true;
		}
		return speechBubble2;
	}

	private GameObject GetSpeechBubble(SpeechBubbleDirection direction)
	{
		return direction switch
		{
			SpeechBubbleDirection.TopLeft => upperLeftBubble, 
			SpeechBubbleDirection.BottomLeft => bottomLeftBubble, 
			SpeechBubbleDirection.TopRight => upperRightBubble, 
			SpeechBubbleDirection.BottomRight => bottomRightBubble, 
			SpeechBubbleDirection.MiddleLeft => leftBubble, 
			_ => null, 
		};
	}

	public void PlaySpeechBubbleDeath()
	{
		SpeechBubbleDirection bubbleDirection = m_bubbleDirection;
		GameObject speechBubble = GetSpeechBubble(bubbleDirection);
		if (speechBubble != null)
		{
			iTween.ScaleTo(speechBubble, iTween.Hash("scale", Vector3.zero, "easetype", iTween.EaseType.easeInExpo, "time", 0.5f, "oncomplete", "OnBubbleDeathComplete", "oncompletetarget", base.gameObject, "oncompleteparams", bubbleDirection));
		}
	}

	private void OnBubbleDeathComplete(SpeechBubbleDirection direction)
	{
		GameObject speechBubble = GetSpeechBubble(direction);
		if (speechBubble != null)
		{
			speechBubble.GetComponent<Renderer>().enabled = false;
		}
	}

	public SpeechBubbleDirection GetSpeechBubbleDirection()
	{
		return m_bubbleDirection;
	}

	public void ShowPopUpArrow(PopUpArrowDirection direction)
	{
		switch (direction)
		{
		case PopUpArrowDirection.Left:
			leftPopupArrow.GetComponent<Renderer>().enabled = true;
			break;
		case PopUpArrowDirection.Right:
			rightPopupArrow.GetComponent<Renderer>().enabled = true;
			break;
		case PopUpArrowDirection.Down:
			bottomPopupArrow.GetComponent<Renderer>().enabled = true;
			break;
		case PopUpArrowDirection.Up:
			topPopupArrow.GetComponent<Renderer>().enabled = true;
			break;
		case PopUpArrowDirection.LeftDown:
			bottomLeftPopupArrow.GetComponent<Renderer>().enabled = true;
			break;
		case PopUpArrowDirection.RightDown:
			bottomRightPopupArrow.GetComponent<Renderer>().enabled = true;
			break;
		case PopUpArrowDirection.RightUp:
			topRightPopupArrow.GetComponent<Renderer>().enabled = true;
			break;
		case PopUpArrowDirection.LeftUp:
			topLeftPopupArrow.GetComponent<Renderer>().enabled = true;
			break;
		case PopUpArrowDirection.BottomThree:
			bottomLeftPopupArrow.GetComponent<Renderer>().enabled = true;
			bottomRightPopupArrow.GetComponent<Renderer>().enabled = true;
			bottomPopupArrow.GetComponent<Renderer>().enabled = true;
			break;
		case PopUpArrowDirection.TopThree:
			topLeftPopupArrow.GetComponent<Renderer>().enabled = true;
			topRightPopupArrow.GetComponent<Renderer>().enabled = true;
			topPopupArrow.GetComponent<Renderer>().enabled = true;
			break;
		}
	}

	public void SetPosition(Actor actor, SpeechBubbleDirection direction)
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
		case SpeechBubbleDirection.TopLeft:
			position = SceneUtils.FindChildBySubstring(gameObject, "BottomRight").transform.position;
			break;
		case SpeechBubbleDirection.BottomLeft:
			position = SceneUtils.FindChildBySubstring(gameObject, "TopRight").transform.position;
			break;
		case SpeechBubbleDirection.TopRight:
			position = SceneUtils.FindChildBySubstring(gameObject, "BottomLeft").transform.position;
			break;
		case SpeechBubbleDirection.BottomRight:
			position = SceneUtils.FindChildBySubstring(gameObject, "TopLeft").transform.position;
			break;
		case SpeechBubbleDirection.MiddleLeft:
			position = SceneUtils.FindChildBySubstring(gameObject, "MiddleRight").transform.position;
			break;
		}
		base.transform.position = position;
	}

	public void SetPosition(Vector3 position)
	{
		base.transform.position = position;
	}

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
		}
		else
		{
			base.transform.position = SceneUtils.FindChildBySubstring(gameObject, "SmallBubble").transform.position;
		}
	}

	public void CloseWithoutAnimation()
	{
		FinishDeath();
	}

	private void FinishDeath()
	{
		UnityEngine.Object.Destroy(base.gameObject);
		if (OnFinishDeathState != null)
		{
			OnFinishDeathState(notificationGroup);
		}
	}

	public void PlayDeath()
	{
		if (destroyEvent != null)
		{
			destroyEvent.Activate();
		}
		if (bounceObject != null || fadeArrowObject != null)
		{
			FinishDeath();
			return;
		}
		isDying = true;
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", Vector3.zero, "easetype", iTween.EaseType.easeInExpo, "time", 0.5f, "oncomplete", "FinishDeath", "oncompletetarget", base.gameObject));
	}

	public void Shrink(float duration = -1f)
	{
		m_shrunk = true;
		if (duration < 0f)
		{
			duration = 0.5f;
		}
		iTween.Stop(base.gameObject);
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", Vector3.zero, "easetype", iTween.EaseType.easeInExpo, "time", duration));
	}

	public void Unshrink(float duration = -1f)
	{
		if (!isDying)
		{
			if (duration < 0f)
			{
				duration = 0.5f;
			}
			iTween.Stop(base.gameObject);
			iTween.ScaleTo(base.gameObject, iTween.Hash("scale", m_initialScale, "easetype", iTween.EaseType.easeInExpo, "time", duration));
			m_shrunk = false;
		}
	}

	public bool IsDying()
	{
		return isDying;
	}

	public void PlayBirth()
	{
		if (showEvent != null)
		{
			showEvent.Activate();
		}
		if (bounceObject == null && fadeArrowObject == null)
		{
			Vector3 localScale = base.transform.localScale;
			PlayBirthAnim(base.gameObject, new Vector3(0.01f, 0.01f, 0.01f), localScale);
			m_initialScale = localScale;
		}
		else if (bounceObject != null)
		{
			BounceDown();
		}
		else if (fadeArrowObject != null)
		{
			FadeOut();
		}
	}

	public void PlayBirthWithForcedScale(Vector3 targetScale)
	{
		PlayBirthAnim(base.gameObject, base.gameObject.transform.localScale, targetScale);
		m_initialScale = base.transform.localScale;
	}

	public void PlaySmallBirthForFakeBubble()
	{
		if (showEvent != null)
		{
			showEvent.Activate();
		}
		if (bounceObject == null && fadeArrowObject == null)
		{
			float num = 0.25f;
			PlayBirthAnim(targetScale: new Vector3(num * base.transform.localScale.x, num * base.transform.localScale.y, num * base.transform.localScale.z), startingScale: new Vector3(0.01f, 0.01f, 0.01f), gameObject: base.gameObject);
		}
		else
		{
			BounceDown();
		}
	}

	public static void PlayBirthAnim(GameObject gameObject, Vector3 startingScale, Vector3 targetScale)
	{
		gameObject.transform.localScale = startingScale;
		iTween.ScaleTo(gameObject, iTween.Hash("scale", targetScale, "easetype", iTween.EaseType.easeOutElastic, "time", 1f));
	}

	public void PulseReminderEveryXSeconds(float seconds)
	{
		StartCoroutine(PulseReminder(seconds));
	}

	private IEnumerator PulseReminder(float seconds)
	{
		WaitForSeconds waitForSecs = new WaitForSeconds(seconds);
		while (!isDying)
		{
			yield return waitForSecs;
			if (!m_shrunk)
			{
				iTween.PunchScale(base.gameObject, iTween.Hash("amount", Vector3.one, "time", 1f));
			}
		}
	}

	private void BounceUp()
	{
		iTween.MoveTo(bounceObject, iTween.Hash("islocal", true, "z", bounceObject.transform.localPosition.z - 0.5f, "time", 0.75f, "easetype", iTween.EaseType.easeInCubic, "oncomplete", "BounceDown", "oncompletetarget", base.gameObject));
	}

	private void BounceDown()
	{
		iTween.MoveTo(bounceObject, iTween.Hash("islocal", true, "z", bounceObject.transform.localPosition.z + 0.5f, "time", 0.75f, "easetype", iTween.EaseType.easeOutCubic, "oncomplete", "BounceUp", "oncompletetarget", base.gameObject));
	}

	private void FadeOut()
	{
		iTween.MoveTo(fadeArrowObject, iTween.Hash("islocal", true, "z", fadeArrowObject.transform.localPosition.z - 0.5f, "time", 0.5f, "easetype", iTween.EaseType.linear, "oncomplete", "FadeComplete", "oncompletetarget", base.gameObject));
		AnimationUtil.FadeTexture(fadeArrowObject.GetComponentInChildren<MeshRenderer>(), 1f, 0f, 0.5f, 0.15f);
	}

	private void FadeComplete()
	{
		iTween.MoveTo(fadeArrowObject, iTween.Hash("islocal", true, "z", fadeArrowObject.transform.localPosition.z + 0.5f, "time", 0f, "delay", 0.5f, "easetype", iTween.EaseType.linear, "oncomplete", "FadeOut", "oncompletetarget", base.gameObject));
		AnimationUtil.FadeTexture(fadeArrowObject.GetComponentInChildren<MeshRenderer>(), 0f, 1f, 0f, 0.85f);
	}

	public void AssignAudio(AudioSource source)
	{
		m_accompaniedAudio = source;
	}

	public AudioSource GetAudio()
	{
		return m_accompaniedAudio;
	}

	public GameObject GetParentOffsetObject()
	{
		return m_parentOffsetObject;
	}

	public void SetParentOffsetObject(GameObject parentOffset)
	{
		if (m_parentOffsetObject != null)
		{
			base.transform.parent = null;
			UnityEngine.Object.Destroy(m_parentOffsetObject);
		}
		m_parentOffsetObject = parentOffset;
		base.transform.SetParent(parentOffset.transform);
	}

	public void SetClickBlockerActive(bool active)
	{
		if (clickBlocker != null)
		{
			clickBlocker.gameObject.SetActive(active);
		}
	}
}
