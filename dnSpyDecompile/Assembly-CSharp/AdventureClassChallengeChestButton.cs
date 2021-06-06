using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class AdventureClassChallengeChestButton : PegUIElement
{
	// Token: 0x060001BA RID: 442 RVA: 0x0000A3F4 File Offset: 0x000085F4
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6", base.gameObject);
		this.ShowHighlight(true);
		base.StartCoroutine(this.ShowRewardCard());
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000A424 File Offset: 0x00008624
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.ShowHighlight(false);
		this.HideRewardCard();
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000A433 File Offset: 0x00008633
	public void Press()
	{
		SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6", base.gameObject);
		this.Depress();
		this.ShowHighlight(true);
		base.StartCoroutine(this.ShowRewardCard());
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000A469 File Offset: 0x00008669
	public void Release()
	{
		this.Raise();
		this.ShowHighlight(false);
		this.HideRewardCard();
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000A480 File Offset: 0x00008680
	private void Raise()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_UpBone.localPosition,
			"time",
			0.1f,
			"easeType",
			iTween.EaseType.linear,
			"isLocal",
			true
		});
		iTween.MoveTo(this.m_RootObject, args);
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000A4F8 File Offset: 0x000086F8
	private void Depress()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_DownBone.localPosition,
			"time",
			0.1f,
			"easeType",
			iTween.EaseType.linear,
			"isLocal",
			true
		});
		iTween.MoveTo(this.m_RootObject, args);
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000A570 File Offset: 0x00008770
	private void ShowHighlight(bool show)
	{
		this.m_HighlightPlane.GetComponent<Renderer>().enabled = show;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000A583 File Offset: 0x00008783
	private IEnumerator ShowRewardCard()
	{
		while (this.m_IsRewardLoading)
		{
			yield return null;
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette(0.4f, 0.2f, iTween.EaseType.easeOutCirc, null, null);
		fullScreenFXMgr.Blur(1f, 0.2f, iTween.EaseType.easeOutCirc, null);
		this.m_RewardBone.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.ScaleTo(this.m_RewardBone, new Vector3(10f, 10f, 10f), 0.2f);
		this.m_RewardCard.SetActive(true);
		yield break;
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000A594 File Offset: 0x00008794
	private void HideRewardCard()
	{
		iTween.ScaleTo(this.m_RewardBone, new Vector3(0.1f, 0.1f, 0.1f), 0.2f);
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.StopVignette();
		fullScreenFXMgr.StopBlur(0.2f, iTween.EaseType.easeOutCirc, new Action(this.EffectFadeOutFinished), false);
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000A5E9 File Offset: 0x000087E9
	private void EffectFadeOutFinished()
	{
		if (this == null)
		{
			return;
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.Default);
		if (this.m_RewardCard != null)
		{
			this.m_RewardCard.SetActive(false);
		}
	}

	// Token: 0x04000133 RID: 307
	public GameObject m_RootObject;

	// Token: 0x04000134 RID: 308
	public Transform m_UpBone;

	// Token: 0x04000135 RID: 309
	public Transform m_DownBone;

	// Token: 0x04000136 RID: 310
	public GameObject m_HighlightPlane;

	// Token: 0x04000137 RID: 311
	public GameObject m_RewardBone;

	// Token: 0x04000138 RID: 312
	public GameObject m_RewardCard;

	// Token: 0x04000139 RID: 313
	public bool m_IsRewardLoading;
}
