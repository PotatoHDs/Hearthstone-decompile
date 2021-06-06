using System;
using System.Collections;
using UnityEngine;

public class TraySection : MonoBehaviour
{
	public delegate void DelOnDoorStateChangedCallback(object callbackData);

	private class OnDoorStateChangedCallbackData
	{
		public DelOnDoorStateChangedCallback m_callback;

		public object m_callbackData;

		public string m_animationName;
	}

	public GameObject m_door;

	public CollectionDeckBoxVisual m_deckBox;

	public Animator m_deckFX;

	private const float DOOR_ANIM_SPEED = 6f;

	private readonly string DOOR_OPEN_ANIM_NAME = "Deck_DoorOpen";

	private readonly string DOOR_CLOSE_ANIM_NAME = "Deck_DoorClose";

	private static readonly Vector3 DECKBOX_LOCAL_EULER_ANGLES = new Vector3(90f, 180f, 0f);

	private bool m_isOpen;

	private bool m_wasTouchModeEnabled;

	private bool m_deckBoxShown;

	private bool m_showDoor = true;

	private Transform m_parent;

	public void ShowDoor(bool show)
	{
		if (!m_showDoor)
		{
			show = false;
		}
		m_door.gameObject.SetActive(show);
	}

	public bool IsOpen()
	{
		return m_isOpen;
	}

	public Bounds GetDoorBounds()
	{
		return m_door.GetComponent<Renderer>().bounds;
	}

	public void OpenDoor()
	{
		OpenDoor(null);
	}

	public void OpenDoor(DelOnDoorStateChangedCallback callback)
	{
		OpenDoor(callback, null);
	}

	public void OpenDoor(DelOnDoorStateChangedCallback callback, object callbackData)
	{
		OpenDoor(isImmediate: false, callback, callbackData);
	}

	public void OpenDoorImmediately()
	{
		OpenDoorImmediately(null);
	}

	public void OpenDoorImmediately(DelOnDoorStateChangedCallback callback)
	{
		OpenDoorImmediately(callback, null);
	}

	public void OpenDoorImmediately(DelOnDoorStateChangedCallback callback, object callbackData)
	{
		OpenDoor(isImmediate: true, callback, callbackData);
	}

	public void CloseDoor()
	{
		CloseDoor(null);
	}

	public void CloseDoor(DelOnDoorStateChangedCallback callback)
	{
		CloseDoor(callback, null);
	}

	public void CloseDoor(DelOnDoorStateChangedCallback callback, object callbackData)
	{
		CloseDoor(isImmediate: false, callback, callbackData);
	}

	public void CloseDoorImmediately()
	{
		CloseDoorImmediately(null);
	}

	public void CloseDoorImmediately(DelOnDoorStateChangedCallback callback)
	{
		CloseDoorImmediately(callback, null);
	}

	public void CloseDoorImmediately(DelOnDoorStateChangedCallback callback, object callbackData)
	{
		CloseDoor(isImmediate: true, callback, callbackData);
	}

	public bool IsDeckBoxShown()
	{
		return m_deckBoxShown;
	}

	public void EnableDoors(bool show)
	{
		m_showDoor = show;
	}

	public void ShowDeckBox(bool immediate = false, DelOnDoorStateChangedCallback callback = null)
	{
		base.gameObject.SetActive(value: true);
		m_deckBoxShown = true;
		if (m_showDoor)
		{
			m_door.gameObject.SetActive(value: true);
		}
		OpenDoor(immediate, delegate
		{
			if (m_deckBox != null)
			{
				m_deckBox.Show();
				m_deckBox.PlayPopUpAnimation(delegate
				{
					m_door.gameObject.SetActive(value: false);
					if (callback != null)
					{
						callback(this);
					}
				});
			}
			else
			{
				m_door.gameObject.SetActive(value: false);
				if (callback != null)
				{
					callback(this);
				}
			}
		}, null);
	}

	public void ShowDeckBoxNoAnim()
	{
		base.gameObject.SetActive(value: true);
		m_deckBoxShown = true;
		m_deckBox.Show();
	}

	public void HideDeckBox(bool immediate = false, DelOnDoorStateChangedCallback callback = null)
	{
		m_deckBoxShown = false;
		CloseDoor(immediate, delegate
		{
			m_door.gameObject.SetActive(m_showDoor);
			if (m_deckBox != null)
			{
				m_deckBox.PlayPopDownAnimation(delegate
				{
					m_deckBox.Hide();
					if (callback != null)
					{
						callback(this);
					}
				});
			}
			else if (callback != null)
			{
				callback(this);
			}
		}, null);
	}

	public void MoveDeckBoxToEditPosition(Vector3 worldSpacePosition, float time, DelOnDoorStateChangedCallback callback = null)
	{
		if (m_deckBox == null)
		{
			return;
		}
		m_deckBox.DisableButtonAnimation();
		m_door.gameObject.SetActive(m_showDoor);
		CloseDoor();
		Vector3 localSpacePosition = m_deckBox.transform.parent.InverseTransformPoint(worldSpacePosition);
		m_deckBox.PlayScaleUpAnimation(delegate
		{
			iTween.MoveTo(m_deckBox.gameObject, iTween.Hash("position", localSpacePosition, "islocal", true, "time", time, "easetype", iTween.EaseType.linear, "oncomplete", (Action<object>)delegate
			{
				if (callback != null)
				{
					callback(this);
				}
			}));
		});
	}

	public void MoveDeckBoxBackToOriginalPosition(float time, DelOnDoorStateChangedCallback callback = null)
	{
		if (!(m_deckBox == null))
		{
			OpenDoor(delegate
			{
				m_door.gameObject.SetActive(value: false);
			});
			StartCoroutine(MoveToOriginalPosition(time, callback));
		}
	}

	private IEnumerator MoveToOriginalPosition(float time, DelOnDoorStateChangedCallback callback = null)
	{
		float timeLive = 0f;
		Vector3 startPos = m_deckBox.transform.position;
		Vector3 position = base.transform.GetChild(0).position;
		while (timeLive < time)
		{
			position = m_parent.position;
			position.y += 3.238702f;
			m_deckBox.transform.position = Vector3.Lerp(startPos, position, timeLive / time);
			timeLive += Time.deltaTime;
			yield return 0;
		}
		position = m_parent.position;
		position.y += 3.238702f;
		m_deckBox.transform.position = position;
		m_deckBox.transform.parent = m_parent;
		m_deckBox.PlayScaleDownAnimation(delegate
		{
			if (callback != null)
			{
				callback(this);
			}
			m_deckBox.EnableButtonAnimation();
			m_door.gameObject.SetActive(value: false);
		});
	}

	public void FlipDeckBoxHalfOverToShow(float animTime, DelOnDoorStateChangedCallback callback = null)
	{
		m_deckBox.gameObject.SetActive(value: true);
		m_deckBox.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		SoundManager.Get().LoadAndPlay("collection_manager_new_deck_edge_flips.prefab:9af01c3ef83086746810abe60b8666fd", base.gameObject);
		iTween.StopByName(m_deckBox.gameObject, "rotation");
		iTween.RotateTo(m_deckBox.gameObject, iTween.Hash("rotation", DECKBOX_LOCAL_EULER_ANGLES, "isLocal", true, "time", animTime, "easeType", iTween.EaseType.easeInCubic, "oncomplete", (Action<object>)delegate
		{
			if (callback != null)
			{
				callback(this);
			}
		}, "name", "rotation"));
	}

	public void ClearDeckInfo()
	{
		if (!(m_deckBox == null))
		{
			m_deckBox.SetDeckName("");
			m_deckBox.SetDeckID(-1L);
		}
	}

	public bool HideIfNotInBounds(Bounds bounds)
	{
		UIBScrollableItem component = GetComponent<UIBScrollableItem>();
		if (component == null)
		{
			Debug.LogWarning("UIBScrollableItem not found on a TraySection! This section may not be hidden properly while entering or exiting Collection Manager!");
			return false;
		}
		Bounds bounds2 = default(Bounds);
		component.GetWorldBounds(out var min, out var max);
		bounds2.SetMinMax(min, max);
		if (!bounds.Intersects(bounds2))
		{
			base.gameObject.SetActive(value: false);
			return true;
		}
		return false;
	}

	private void Awake()
	{
		if (m_deckBox != null)
		{
			m_deckBox.transform.localPosition = CollectionDeckBoxVisual.POPPED_DOWN_LOCAL_POS;
			m_deckBox.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
			m_deckBox.transform.localEulerAngles = new Vector3(90f, 180f, 0f);
		}
		m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
		UIBScrollableItem component = GetComponent<UIBScrollableItem>();
		if (component != null)
		{
			component.SetCustomActiveState(IsDeckBoxShown);
		}
		m_parent = m_deckBox.transform.parent;
	}

	private void Update()
	{
		if (m_wasTouchModeEnabled != UniversalInputManager.Get().IsTouchMode())
		{
			m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
		}
	}

	private void OpenDoor(bool isImmediate, DelOnDoorStateChangedCallback callback, object callbackData)
	{
		if (m_isOpen)
		{
			callback?.Invoke(callbackData);
			return;
		}
		m_isOpen = true;
		m_door.GetComponent<Animation>()[DOOR_OPEN_ANIM_NAME].time = (isImmediate ? m_door.GetComponent<Animation>()[DOOR_OPEN_ANIM_NAME].length : 0f);
		m_door.GetComponent<Animation>()[DOOR_OPEN_ANIM_NAME].speed = 6f;
		PlayDoorAnimation(DOOR_OPEN_ANIM_NAME, callback, callbackData);
	}

	private void CloseDoor(bool isImmediate, DelOnDoorStateChangedCallback callback, object callbackData)
	{
		if (!m_isOpen)
		{
			callback?.Invoke(callbackData);
			return;
		}
		m_isOpen = false;
		m_door.GetComponent<Animation>()[DOOR_CLOSE_ANIM_NAME].time = (isImmediate ? m_door.GetComponent<Animation>()[DOOR_CLOSE_ANIM_NAME].length : 0f);
		m_door.GetComponent<Animation>()[DOOR_CLOSE_ANIM_NAME].speed = 6f;
		PlayDoorAnimation(DOOR_CLOSE_ANIM_NAME, callback, callbackData);
	}

	private void PlayDoorAnimation(string animationName, DelOnDoorStateChangedCallback callback, object callbackData)
	{
		m_door.GetComponent<Animation>().Play(animationName);
		OnDoorStateChangedCallbackData value = new OnDoorStateChangedCallbackData
		{
			m_callback = callback,
			m_callbackData = callbackData,
			m_animationName = animationName
		};
		StopCoroutine("WaitThenCallDoorAnimationCallback");
		StartCoroutine("WaitThenCallDoorAnimationCallback", value);
	}

	private IEnumerator WaitThenCallDoorAnimationCallback(OnDoorStateChangedCallbackData callbackData)
	{
		if (callbackData.m_callback != null)
		{
			yield return new WaitForSeconds(m_door.GetComponent<Animation>()[callbackData.m_animationName].length / m_door.GetComponent<Animation>()[callbackData.m_animationName].speed);
			callbackData.m_callback(callbackData.m_callbackData);
		}
	}
}
