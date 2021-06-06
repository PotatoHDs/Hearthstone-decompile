using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200010B RID: 267
public class CollectionDeckTrayButton : PegUIElement
{
	// Token: 0x06001034 RID: 4148 RVA: 0x0005A9F0 File Offset: 0x00058BF0
	protected override void Awake()
	{
		base.Awake();
		this.SetEnabled(false, false);
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			this.m_buttonText.Text = string.Empty;
		}
		else
		{
			this.m_buttonText.Text = GameStrings.Get("GLUE_COLLECTION_NEW_DECK");
		}
		UIBScrollableItem component = base.GetComponent<UIBScrollableItem>();
		if (component != null)
		{
			component.SetCustomActiveState(new UIBScrollableItem.ActiveStateCallback(this.IsUsable));
		}
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x0005AA60 File Offset: 0x00058C60
	public void SetIsUsable(bool isUsable)
	{
		this.m_isUsable = isUsable;
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x0005AA69 File Offset: 0x00058C69
	public bool IsUsable()
	{
		return this.m_isUsable;
	}

	// Token: 0x06001037 RID: 4151 RVA: 0x0005AA71 File Offset: 0x00058C71
	public void PlayPopUpAnimation()
	{
		this.PlayPopUpAnimation(null);
	}

	// Token: 0x06001038 RID: 4152 RVA: 0x0005AA7C File Offset: 0x00058C7C
	public void PlayPopUpAnimation(CollectionDeckTrayButton.DelOnAnimationFinished callback)
	{
		this.PlayPopUpAnimation(callback, null, null);
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x0005AA9C File Offset: 0x00058C9C
	public void PlayPopUpAnimation(CollectionDeckTrayButton.DelOnAnimationFinished callback, object callbackData, float? speed = null)
	{
		base.gameObject.SetActive(true);
		if (this.m_isPoppedUp)
		{
			if (callback != null)
			{
				callback(callbackData);
			}
			return;
		}
		this.m_isPoppedUp = true;
		base.GetComponent<Animation>()[this.DECKBOX_POPUP_ANIM_NAME].time = 0f;
		base.GetComponent<Animation>()[this.DECKBOX_POPUP_ANIM_NAME].speed = ((speed != null) ? speed.Value : 2.5f);
		this.PlayAnimation(this.DECKBOX_POPUP_ANIM_NAME, callback, callbackData);
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x0005AB25 File Offset: 0x00058D25
	public void PlayPopDownAnimation()
	{
		this.PlayPopDownAnimation(null);
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x0005AB30 File Offset: 0x00058D30
	public void PlayPopDownAnimation(CollectionDeckTrayButton.DelOnAnimationFinished callback)
	{
		this.PlayPopDownAnimation(callback, null, null);
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x0005AB50 File Offset: 0x00058D50
	public void PlayPopDownAnimation(CollectionDeckTrayButton.DelOnAnimationFinished callback, object callbackData, float? speed = null)
	{
		base.gameObject.SetActive(true);
		if (!this.m_isPoppedUp)
		{
			if (callback != null)
			{
				callback(callbackData);
			}
			return;
		}
		this.m_isPoppedUp = false;
		base.GetComponent<Animation>()[this.DECKBOX_POPDOWN_ANIM_NAME].time = 0f;
		base.GetComponent<Animation>()[this.DECKBOX_POPDOWN_ANIM_NAME].speed = ((speed != null) ? speed.Value : 2.5f);
		this.PlayAnimation(this.DECKBOX_POPDOWN_ANIM_NAME, callback, callbackData);
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x0005ABDC File Offset: 0x00058DDC
	public void FlipHalfOverAndHide(float animTime, CollectionDeckTrayButton.DelOnAnimationFinished finished = null)
	{
		if (!this.m_isPoppedUp)
		{
			Debug.LogWarning("Can't flip over and hide button. It is currently not popped up.");
			return;
		}
		iTween.StopByName(base.gameObject, "rotation");
		iTween.RotateTo(base.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(270f, 0f, 0f),
			"isLocal",
			true,
			"time",
			animTime,
			"easeType",
			iTween.EaseType.easeInCubic,
			"oncomplete",
			new Action<object>(delegate(object _1)
			{
				if (finished != null)
				{
					finished(this);
				}
				this.gameObject.SetActive(false);
				this.transform.localEulerAngles = Vector3.zero;
			}),
			"name",
			"rotation"
		}));
		this.m_isPoppedUp = false;
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x0005ACBF File Offset: 0x00058EBF
	public bool IsPoppedUp()
	{
		return this.m_isPoppedUp;
	}

	// Token: 0x0600103F RID: 4159 RVA: 0x0005ACC7 File Offset: 0x00058EC7
	private void PlayAnimation(string animationName)
	{
		this.PlayAnimation(animationName, null, null);
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x0005ACD4 File Offset: 0x00058ED4
	private void PlayAnimation(string animationName, CollectionDeckTrayButton.DelOnAnimationFinished callback, object callbackData)
	{
		base.GetComponent<Animation>().Play(animationName);
		CollectionDeckTrayButton.OnPopAnimationFinishedCallbackData value = new CollectionDeckTrayButton.OnPopAnimationFinishedCallbackData
		{
			m_callback = callback,
			m_callbackData = callbackData,
			m_animationName = animationName
		};
		base.StopCoroutine("WaitThenCallAnimationCallback");
		base.StartCoroutine("WaitThenCallAnimationCallback", value);
	}

	// Token: 0x06001041 RID: 4161 RVA: 0x0005AD21 File Offset: 0x00058F21
	private IEnumerator WaitThenCallAnimationCallback(CollectionDeckTrayButton.OnPopAnimationFinishedCallbackData callbackData)
	{
		yield return new WaitForSeconds(base.GetComponent<Animation>()[callbackData.m_animationName].length / base.GetComponent<Animation>()[callbackData.m_animationName].speed);
		bool enabled = callbackData.m_animationName.Equals(this.DECKBOX_POPUP_ANIM_NAME);
		this.SetEnabled(enabled, false);
		if (callbackData.m_callback == null)
		{
			yield break;
		}
		callbackData.m_callback(callbackData.m_callbackData);
		yield break;
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x0005AD37 File Offset: 0x00058F37
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("Hub_Mouseover.prefab:40130da7b734190479c527d6bca1a4a8");
		this.m_highlightState.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
	}

	// Token: 0x06001043 RID: 4163 RVA: 0x0005AD5B File Offset: 0x00058F5B
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.m_highlightState.ChangeState(ActorStateType.NONE);
	}

	// Token: 0x04000AD5 RID: 2773
	public HighlightState m_highlightState;

	// Token: 0x04000AD6 RID: 2774
	public UberText m_buttonText;

	// Token: 0x04000AD7 RID: 2775
	private const float BUTTON_POP_SPEED = 2.5f;

	// Token: 0x04000AD8 RID: 2776
	private readonly string DECKBOX_POPUP_ANIM_NAME = "NewDeck_PopUp";

	// Token: 0x04000AD9 RID: 2777
	private readonly string DECKBOX_POPDOWN_ANIM_NAME = "NewDeck_PopDown";

	// Token: 0x04000ADA RID: 2778
	private bool m_isPoppedUp;

	// Token: 0x04000ADB RID: 2779
	private bool m_isUsable;

	// Token: 0x0200143E RID: 5182
	// (Invoke) Token: 0x0600DA22 RID: 55842
	public delegate void DelOnAnimationFinished(object callbackData);

	// Token: 0x0200143F RID: 5183
	private class OnPopAnimationFinishedCallbackData
	{
		// Token: 0x0400A966 RID: 43366
		public string m_animationName;

		// Token: 0x0400A967 RID: 43367
		public CollectionDeckTrayButton.DelOnAnimationFinished m_callback;

		// Token: 0x0400A968 RID: 43368
		public object m_callbackData;
	}
}
