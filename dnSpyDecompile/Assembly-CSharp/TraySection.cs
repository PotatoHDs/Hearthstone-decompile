using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200013F RID: 319
public class TraySection : MonoBehaviour
{
	// Token: 0x060014E2 RID: 5346 RVA: 0x00077B69 File Offset: 0x00075D69
	public void ShowDoor(bool show)
	{
		if (!this.m_showDoor)
		{
			show = false;
		}
		this.m_door.gameObject.SetActive(show);
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x00077B87 File Offset: 0x00075D87
	public bool IsOpen()
	{
		return this.m_isOpen;
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x00077B8F File Offset: 0x00075D8F
	public Bounds GetDoorBounds()
	{
		return this.m_door.GetComponent<Renderer>().bounds;
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x00077BA1 File Offset: 0x00075DA1
	public void OpenDoor()
	{
		this.OpenDoor(null);
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x00077BAA File Offset: 0x00075DAA
	public void OpenDoor(TraySection.DelOnDoorStateChangedCallback callback)
	{
		this.OpenDoor(callback, null);
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x00077BB4 File Offset: 0x00075DB4
	public void OpenDoor(TraySection.DelOnDoorStateChangedCallback callback, object callbackData)
	{
		this.OpenDoor(false, callback, callbackData);
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x00077BBF File Offset: 0x00075DBF
	public void OpenDoorImmediately()
	{
		this.OpenDoorImmediately(null);
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x00077BC8 File Offset: 0x00075DC8
	public void OpenDoorImmediately(TraySection.DelOnDoorStateChangedCallback callback)
	{
		this.OpenDoorImmediately(callback, null);
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x00077BD2 File Offset: 0x00075DD2
	public void OpenDoorImmediately(TraySection.DelOnDoorStateChangedCallback callback, object callbackData)
	{
		this.OpenDoor(true, callback, callbackData);
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x00077BDD File Offset: 0x00075DDD
	public void CloseDoor()
	{
		this.CloseDoor(null);
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x00077BE6 File Offset: 0x00075DE6
	public void CloseDoor(TraySection.DelOnDoorStateChangedCallback callback)
	{
		this.CloseDoor(callback, null);
	}

	// Token: 0x060014ED RID: 5357 RVA: 0x00077BF0 File Offset: 0x00075DF0
	public void CloseDoor(TraySection.DelOnDoorStateChangedCallback callback, object callbackData)
	{
		this.CloseDoor(false, callback, callbackData);
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x00077BFB File Offset: 0x00075DFB
	public void CloseDoorImmediately()
	{
		this.CloseDoorImmediately(null);
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x00077C04 File Offset: 0x00075E04
	public void CloseDoorImmediately(TraySection.DelOnDoorStateChangedCallback callback)
	{
		this.CloseDoorImmediately(callback, null);
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x00077C0E File Offset: 0x00075E0E
	public void CloseDoorImmediately(TraySection.DelOnDoorStateChangedCallback callback, object callbackData)
	{
		this.CloseDoor(true, callback, callbackData);
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x00077C19 File Offset: 0x00075E19
	public bool IsDeckBoxShown()
	{
		return this.m_deckBoxShown;
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x00077C21 File Offset: 0x00075E21
	public void EnableDoors(bool show)
	{
		this.m_showDoor = show;
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x00077C2C File Offset: 0x00075E2C
	public void ShowDeckBox(bool immediate = false, TraySection.DelOnDoorStateChangedCallback callback = null)
	{
		base.gameObject.SetActive(true);
		this.m_deckBoxShown = true;
		if (this.m_showDoor)
		{
			this.m_door.gameObject.SetActive(true);
		}
		CollectionDeckBoxVisual.DelOnAnimationFinished <>9__1;
		this.OpenDoor(immediate, delegate(object _1)
		{
			if (this.m_deckBox != null)
			{
				this.m_deckBox.Show();
				CollectionDeckBoxVisual deckBox = this.m_deckBox;
				CollectionDeckBoxVisual.DelOnAnimationFinished callback2;
				if ((callback2 = <>9__1) == null)
				{
					callback2 = (<>9__1 = delegate(object _2)
					{
						this.m_door.gameObject.SetActive(false);
						if (callback != null)
						{
							callback(this);
						}
					});
				}
				deckBox.PlayPopUpAnimation(callback2);
				return;
			}
			this.m_door.gameObject.SetActive(false);
			if (callback != null)
			{
				callback(this);
			}
		}, null);
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x00077C8D File Offset: 0x00075E8D
	public void ShowDeckBoxNoAnim()
	{
		base.gameObject.SetActive(true);
		this.m_deckBoxShown = true;
		this.m_deckBox.Show();
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x00077CB0 File Offset: 0x00075EB0
	public void HideDeckBox(bool immediate = false, TraySection.DelOnDoorStateChangedCallback callback = null)
	{
		this.m_deckBoxShown = false;
		CollectionDeckBoxVisual.DelOnAnimationFinished <>9__1;
		this.CloseDoor(immediate, delegate(object _1)
		{
			this.m_door.gameObject.SetActive(this.m_showDoor);
			if (this.m_deckBox != null)
			{
				CollectionDeckBoxVisual deckBox = this.m_deckBox;
				CollectionDeckBoxVisual.DelOnAnimationFinished callback2;
				if ((callback2 = <>9__1) == null)
				{
					callback2 = (<>9__1 = delegate(object _2)
					{
						this.m_deckBox.Hide();
						if (callback != null)
						{
							callback(this);
						}
					});
				}
				deckBox.PlayPopDownAnimation(callback2);
				return;
			}
			if (callback != null)
			{
				callback(this);
			}
		}, null);
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x00077CEC File Offset: 0x00075EEC
	public void MoveDeckBoxToEditPosition(Vector3 worldSpacePosition, float time, TraySection.DelOnDoorStateChangedCallback callback = null)
	{
		if (this.m_deckBox == null)
		{
			return;
		}
		this.m_deckBox.DisableButtonAnimation();
		this.m_door.gameObject.SetActive(this.m_showDoor);
		this.CloseDoor();
		Vector3 localSpacePosition = this.m_deckBox.transform.parent.InverseTransformPoint(worldSpacePosition);
		Action<object> <>9__1;
		this.m_deckBox.PlayScaleUpAnimation(delegate(object _1)
		{
			GameObject gameObject = this.m_deckBox.gameObject;
			object[] array = new object[10];
			array[0] = "position";
			array[1] = localSpacePosition;
			array[2] = "islocal";
			array[3] = true;
			array[4] = "time";
			array[5] = time;
			array[6] = "easetype";
			array[7] = iTween.EaseType.linear;
			array[8] = "oncomplete";
			int num = 9;
			Action<object> action;
			if ((action = <>9__1) == null)
			{
				action = (<>9__1 = delegate(object _2)
				{
					if (callback != null)
					{
						callback(this);
					}
				});
			}
			array[num] = action;
			iTween.MoveTo(gameObject, iTween.Hash(array));
		});
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x00077D7D File Offset: 0x00075F7D
	public void MoveDeckBoxBackToOriginalPosition(float time, TraySection.DelOnDoorStateChangedCallback callback = null)
	{
		if (this.m_deckBox == null)
		{
			return;
		}
		this.OpenDoor(delegate(object _1)
		{
			this.m_door.gameObject.SetActive(false);
		});
		base.StartCoroutine(this.MoveToOriginalPosition(time, callback));
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x00077DAF File Offset: 0x00075FAF
	private IEnumerator MoveToOriginalPosition(float time, TraySection.DelOnDoorStateChangedCallback callback = null)
	{
		float timeLive = 0f;
		Vector3 startPos = this.m_deckBox.transform.position;
		Vector3 position = base.transform.GetChild(0).position;
		while (timeLive < time)
		{
			position = this.m_parent.position;
			position.y += 3.238702f;
			this.m_deckBox.transform.position = Vector3.Lerp(startPos, position, timeLive / time);
			timeLive += Time.deltaTime;
			yield return 0;
		}
		position = this.m_parent.position;
		position.y += 3.238702f;
		this.m_deckBox.transform.position = position;
		this.m_deckBox.transform.parent = this.m_parent;
		this.m_deckBox.PlayScaleDownAnimation(delegate(object _2)
		{
			if (callback != null)
			{
				callback(this);
			}
			this.m_deckBox.EnableButtonAnimation();
			this.m_door.gameObject.SetActive(false);
		});
		yield break;
	}

	// Token: 0x060014F9 RID: 5369 RVA: 0x00077DCC File Offset: 0x00075FCC
	public void FlipDeckBoxHalfOverToShow(float animTime, TraySection.DelOnDoorStateChangedCallback callback = null)
	{
		this.m_deckBox.gameObject.SetActive(true);
		this.m_deckBox.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		SoundManager.Get().LoadAndPlay("collection_manager_new_deck_edge_flips.prefab:9af01c3ef83086746810abe60b8666fd", base.gameObject);
		iTween.StopByName(this.m_deckBox.gameObject, "rotation");
		iTween.RotateTo(this.m_deckBox.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			TraySection.DECKBOX_LOCAL_EULER_ANGLES,
			"isLocal",
			true,
			"time",
			animTime,
			"easeType",
			iTween.EaseType.easeInCubic,
			"oncomplete",
			new Action<object>(delegate(object _1)
			{
				if (callback != null)
				{
					callback(this);
				}
			}),
			"name",
			"rotation"
		}));
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x00077EDF File Offset: 0x000760DF
	public void ClearDeckInfo()
	{
		if (this.m_deckBox == null)
		{
			return;
		}
		this.m_deckBox.SetDeckName("");
		this.m_deckBox.SetDeckID(-1L);
	}

	// Token: 0x060014FB RID: 5371 RVA: 0x00077F10 File Offset: 0x00076110
	public bool HideIfNotInBounds(Bounds bounds)
	{
		UIBScrollableItem component = base.GetComponent<UIBScrollableItem>();
		if (component == null)
		{
			Debug.LogWarning("UIBScrollableItem not found on a TraySection! This section may not be hidden properly while entering or exiting Collection Manager!");
			return false;
		}
		Bounds bounds2 = default(Bounds);
		Vector3 min;
		Vector3 max;
		component.GetWorldBounds(out min, out max);
		bounds2.SetMinMax(min, max);
		if (!bounds.Intersects(bounds2))
		{
			base.gameObject.SetActive(false);
			return true;
		}
		return false;
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x00077F70 File Offset: 0x00076170
	private void Awake()
	{
		if (this.m_deckBox != null)
		{
			this.m_deckBox.transform.localPosition = CollectionDeckBoxVisual.POPPED_DOWN_LOCAL_POS;
			this.m_deckBox.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
			this.m_deckBox.transform.localEulerAngles = new Vector3(90f, 180f, 0f);
		}
		this.m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
		UIBScrollableItem component = base.GetComponent<UIBScrollableItem>();
		if (component != null)
		{
			component.SetCustomActiveState(new UIBScrollableItem.ActiveStateCallback(this.IsDeckBoxShown));
		}
		this.m_parent = this.m_deckBox.transform.parent;
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x00078030 File Offset: 0x00076230
	private void Update()
	{
		if (this.m_wasTouchModeEnabled != UniversalInputManager.Get().IsTouchMode())
		{
			this.m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
		}
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x00078054 File Offset: 0x00076254
	private void OpenDoor(bool isImmediate, TraySection.DelOnDoorStateChangedCallback callback, object callbackData)
	{
		if (this.m_isOpen)
		{
			if (callback != null)
			{
				callback(callbackData);
			}
			return;
		}
		this.m_isOpen = true;
		this.m_door.GetComponent<Animation>()[this.DOOR_OPEN_ANIM_NAME].time = (isImmediate ? this.m_door.GetComponent<Animation>()[this.DOOR_OPEN_ANIM_NAME].length : 0f);
		this.m_door.GetComponent<Animation>()[this.DOOR_OPEN_ANIM_NAME].speed = 6f;
		this.PlayDoorAnimation(this.DOOR_OPEN_ANIM_NAME, callback, callbackData);
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x000780EC File Offset: 0x000762EC
	private void CloseDoor(bool isImmediate, TraySection.DelOnDoorStateChangedCallback callback, object callbackData)
	{
		if (!this.m_isOpen)
		{
			if (callback != null)
			{
				callback(callbackData);
			}
			return;
		}
		this.m_isOpen = false;
		this.m_door.GetComponent<Animation>()[this.DOOR_CLOSE_ANIM_NAME].time = (isImmediate ? this.m_door.GetComponent<Animation>()[this.DOOR_CLOSE_ANIM_NAME].length : 0f);
		this.m_door.GetComponent<Animation>()[this.DOOR_CLOSE_ANIM_NAME].speed = 6f;
		this.PlayDoorAnimation(this.DOOR_CLOSE_ANIM_NAME, callback, callbackData);
	}

	// Token: 0x06001500 RID: 5376 RVA: 0x00078184 File Offset: 0x00076384
	private void PlayDoorAnimation(string animationName, TraySection.DelOnDoorStateChangedCallback callback, object callbackData)
	{
		this.m_door.GetComponent<Animation>().Play(animationName);
		TraySection.OnDoorStateChangedCallbackData value = new TraySection.OnDoorStateChangedCallbackData
		{
			m_callback = callback,
			m_callbackData = callbackData,
			m_animationName = animationName
		};
		base.StopCoroutine("WaitThenCallDoorAnimationCallback");
		base.StartCoroutine("WaitThenCallDoorAnimationCallback", value);
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x000781D6 File Offset: 0x000763D6
	private IEnumerator WaitThenCallDoorAnimationCallback(TraySection.OnDoorStateChangedCallbackData callbackData)
	{
		if (callbackData.m_callback == null)
		{
			yield break;
		}
		yield return new WaitForSeconds(this.m_door.GetComponent<Animation>()[callbackData.m_animationName].length / this.m_door.GetComponent<Animation>()[callbackData.m_animationName].speed);
		callbackData.m_callback(callbackData.m_callbackData);
		yield break;
	}

	// Token: 0x04000DF2 RID: 3570
	public GameObject m_door;

	// Token: 0x04000DF3 RID: 3571
	public CollectionDeckBoxVisual m_deckBox;

	// Token: 0x04000DF4 RID: 3572
	public Animator m_deckFX;

	// Token: 0x04000DF5 RID: 3573
	private const float DOOR_ANIM_SPEED = 6f;

	// Token: 0x04000DF6 RID: 3574
	private readonly string DOOR_OPEN_ANIM_NAME = "Deck_DoorOpen";

	// Token: 0x04000DF7 RID: 3575
	private readonly string DOOR_CLOSE_ANIM_NAME = "Deck_DoorClose";

	// Token: 0x04000DF8 RID: 3576
	private static readonly Vector3 DECKBOX_LOCAL_EULER_ANGLES = new Vector3(90f, 180f, 0f);

	// Token: 0x04000DF9 RID: 3577
	private bool m_isOpen;

	// Token: 0x04000DFA RID: 3578
	private bool m_wasTouchModeEnabled;

	// Token: 0x04000DFB RID: 3579
	private bool m_deckBoxShown;

	// Token: 0x04000DFC RID: 3580
	private bool m_showDoor = true;

	// Token: 0x04000DFD RID: 3581
	private Transform m_parent;

	// Token: 0x020014E7 RID: 5351
	// (Invoke) Token: 0x0600DCB0 RID: 56496
	public delegate void DelOnDoorStateChangedCallback(object callbackData);

	// Token: 0x020014E8 RID: 5352
	private class OnDoorStateChangedCallbackData
	{
		// Token: 0x0400AB44 RID: 43844
		public TraySection.DelOnDoorStateChangedCallback m_callback;

		// Token: 0x0400AB45 RID: 43845
		public object m_callbackData;

		// Token: 0x0400AB46 RID: 43846
		public string m_animationName;
	}
}
