using System;
using System.Collections;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000AF5 RID: 2805
[CustomEditClass]
public class UIBPopup : MonoBehaviour
{
	// Token: 0x06009533 RID: 38195 RVA: 0x00305220 File Offset: 0x00303420
	protected virtual void Awake()
	{
		if (this.m_useStartingPositionForShow)
		{
			this.m_showPosition = base.transform.localPosition;
		}
		if (this.m_useStartingScaleForShow)
		{
			this.m_showScale = base.transform.localScale;
		}
		if (base.GetComponent<WidgetTemplate>() != null)
		{
			this.m_useOverlayUI = false;
			this.m_destroyOnSceneLoad = false;
		}
	}

	// Token: 0x06009534 RID: 38196 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void Start()
	{
	}

	// Token: 0x06009535 RID: 38197 RVA: 0x0030527B File Offset: 0x0030347B
	public virtual bool IsShown()
	{
		return this.m_shown;
	}

	// Token: 0x06009536 RID: 38198 RVA: 0x00305283 File Offset: 0x00303483
	public virtual void Show()
	{
		this.Show(true);
	}

	// Token: 0x06009537 RID: 38199 RVA: 0x0030528C File Offset: 0x0030348C
	public virtual void Show(bool useOverlayUI)
	{
		if (this.m_shown)
		{
			return;
		}
		this.m_useOverlayUI = useOverlayUI;
		if (this.m_useOverlayUI)
		{
			OverlayUI overlayUI = OverlayUI.Get();
			GameObject gameObject = base.gameObject;
			CanvasAnchor anchor = CanvasAnchor.CENTER;
			CanvasScaleMode scaleMode = this.m_scaleMode;
			overlayUI.AddGameObject(gameObject, anchor, this.m_destroyOnSceneLoad, scaleMode);
		}
		this.m_shown = true;
		this.DoShowAnimation(null);
	}

	// Token: 0x06009538 RID: 38200 RVA: 0x001D660B File Offset: 0x001D480B
	public virtual void Hide()
	{
		this.Hide(false);
	}

	// Token: 0x06009539 RID: 38201 RVA: 0x003052DE File Offset: 0x003034DE
	protected virtual void Hide(bool animate)
	{
		if (!this.m_shown)
		{
			return;
		}
		this.m_shown = false;
		this.DoHideAnimation(!animate, new UIBPopup.OnAnimationComplete(this.OnHidden));
	}

	// Token: 0x0600953A RID: 38202 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnHidden()
	{
	}

	// Token: 0x0600953B RID: 38203 RVA: 0x00305307 File Offset: 0x00303507
	protected void DoShowAnimation(UIBPopup.OnAnimationComplete animationDoneCallback = null)
	{
		this.DoShowAnimation(false, animationDoneCallback);
	}

	// Token: 0x0600953C RID: 38204 RVA: 0x00305314 File Offset: 0x00303514
	protected void DoShowAnimation(bool disableAnimation, UIBPopup.OnAnimationComplete animationDoneCallback = null)
	{
		base.transform.localPosition = this.m_showPosition;
		if (this.m_useOverlayUI)
		{
			OverlayUI overlayUI = OverlayUI.Get();
			GameObject gameObject = base.gameObject;
			CanvasAnchor anchor = CanvasAnchor.CENTER;
			CanvasScaleMode scaleMode = this.m_scaleMode;
			overlayUI.AddGameObject(gameObject, anchor, this.m_destroyOnSceneLoad, scaleMode);
		}
		this.EnableAnimationClickBlocker(true);
		if (!disableAnimation && this.m_showAnimTime > 0f)
		{
			base.transform.localScale = this.m_showScale * 0.01f;
			if (!string.IsNullOrEmpty(this.m_showAnimationSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_showAnimationSound);
			}
			Hashtable hashtable = iTween.Hash(new object[]
			{
				"scale",
				this.m_showScale,
				"isLocal",
				false,
				"time",
				this.m_showAnimTime,
				"easetype",
				iTween.EaseType.easeOutBounce,
				"name",
				"SHOW_ANIMATION"
			});
			if (animationDoneCallback != null)
			{
				hashtable.Add("oncomplete", new Action<object>(delegate(object o)
				{
					this.EnableAnimationClickBlocker(false);
					animationDoneCallback();
				}));
			}
			iTween.StopByName(base.gameObject, "SHOW_ANIMATION");
			iTween.ScaleTo(base.gameObject, hashtable);
		}
		else
		{
			if (this.m_playShowSoundWithNoAnimation && !string.IsNullOrEmpty(this.m_showAnimationSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_showAnimationSound);
			}
			base.transform.localScale = this.m_showScale;
			if (animationDoneCallback != null)
			{
				this.EnableAnimationClickBlocker(false);
				animationDoneCallback();
			}
		}
		WidgetTemplate componentInParent = base.GetComponentInParent<WidgetTemplate>();
		if (componentInParent != null)
		{
			componentInParent.TriggerEvent("POPUP_SHOWN", default(Widget.TriggerEventParameters));
		}
	}

	// Token: 0x0600953D RID: 38205 RVA: 0x003054EC File Offset: 0x003036EC
	protected void DoHideAnimation(UIBPopup.OnAnimationComplete animationDoneCallback = null)
	{
		this.DoHideAnimation(false, animationDoneCallback);
	}

	// Token: 0x0600953E RID: 38206 RVA: 0x003054F8 File Offset: 0x003036F8
	protected void DoHideAnimation(bool disableAnimation, UIBPopup.OnAnimationComplete animationDoneCallback = null)
	{
		Action setHidePosition = delegate()
		{
			if (this.transform != null)
			{
				this.transform.localPosition = this.m_hidePosition;
				this.transform.localScale = this.m_showScale;
			}
		};
		if (!disableAnimation && this.m_hideAnimTime > 0f)
		{
			if (!string.IsNullOrEmpty(this.m_hideAnimationSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_hideAnimationSound);
			}
			Hashtable hashtable = iTween.Hash(new object[]
			{
				"scale",
				this.m_showScale * 0.01f,
				"isLocal",
				true,
				"time",
				this.m_hideAnimTime,
				"easetype",
				iTween.EaseType.linear,
				"name",
				"SHOW_ANIMATION"
			});
			if (animationDoneCallback != null)
			{
				hashtable.Add("oncomplete", new Action<object>(delegate(object o)
				{
					setHidePosition();
					animationDoneCallback();
				}));
			}
			else
			{
				hashtable.Add("oncomplete", new Action<object>(delegate(object o)
				{
					setHidePosition();
				}));
			}
			iTween.StopByName(base.gameObject, "SHOW_ANIMATION");
			iTween.ScaleTo(base.gameObject, hashtable);
		}
		else
		{
			if (this.m_playHideSoundWithNoAnimation && !string.IsNullOrEmpty(this.m_hideAnimationSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_hideAnimationSound);
			}
			setHidePosition();
			if (animationDoneCallback != null)
			{
				animationDoneCallback();
			}
		}
		WidgetTemplate componentInParent = base.GetComponentInParent<WidgetTemplate>();
		if (componentInParent != null)
		{
			componentInParent.TriggerEvent("POPUP_HIDDEN", default(Widget.TriggerEventParameters));
		}
	}

	// Token: 0x0600953F RID: 38207 RVA: 0x0030569D File Offset: 0x0030389D
	private void EnableAnimationClickBlocker(bool enable)
	{
		if (this.m_animationClickBlocker != null)
		{
			this.m_animationClickBlocker.gameObject.SetActive(enable);
		}
	}

	// Token: 0x04007D1A RID: 32026
	[CustomEditField(Sections = "Animation & Positioning")]
	public bool m_useStartingPositionForShow;

	// Token: 0x04007D1B RID: 32027
	[CustomEditField(Sections = "Animation & Positioning")]
	public Vector3 m_showPosition = Vector3.zero;

	// Token: 0x04007D1C RID: 32028
	[CustomEditField(Sections = "Animation & Positioning")]
	public bool m_useStartingScaleForShow;

	// Token: 0x04007D1D RID: 32029
	[CustomEditField(Sections = "Animation & Positioning")]
	public Vector3 m_showScale = Vector3.one;

	// Token: 0x04007D1E RID: 32030
	[CustomEditField(Sections = "Animation & Positioning")]
	public float m_showAnimTime = 0.5f;

	// Token: 0x04007D1F RID: 32031
	[CustomEditField(Sections = "Animation & Positioning")]
	public Vector3 m_hidePosition = new Vector3(-1000f, 0f, 0f);

	// Token: 0x04007D20 RID: 32032
	[CustomEditField(Sections = "Animation & Positioning")]
	public float m_hideAnimTime = 0.1f;

	// Token: 0x04007D21 RID: 32033
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_showAnimationSound = "Expand_Up.prefab:775d97ea42498c044897f396362b9db3";

	// Token: 0x04007D22 RID: 32034
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public bool m_playShowSoundWithNoAnimation;

	// Token: 0x04007D23 RID: 32035
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_hideAnimationSound = "Shrink_Down.prefab:a6d5184049ac041418cd5896e7d9a87a";

	// Token: 0x04007D24 RID: 32036
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public bool m_playHideSoundWithNoAnimation;

	// Token: 0x04007D25 RID: 32037
	[CustomEditField(Sections = "Click Blockers")]
	public BoxCollider m_animationClickBlocker;

	// Token: 0x04007D26 RID: 32038
	private const string s_ShowiTweenAnimationName = "SHOW_ANIMATION";

	// Token: 0x04007D27 RID: 32039
	protected bool m_shown;

	// Token: 0x04007D28 RID: 32040
	protected CanvasScaleMode m_scaleMode = CanvasScaleMode.HEIGHT;

	// Token: 0x04007D29 RID: 32041
	protected bool m_destroyOnSceneLoad = true;

	// Token: 0x04007D2A RID: 32042
	protected bool m_useOverlayUI = true;

	// Token: 0x0200272D RID: 10029
	// (Invoke) Token: 0x06013919 RID: 80153
	public delegate void OnAnimationComplete();
}
