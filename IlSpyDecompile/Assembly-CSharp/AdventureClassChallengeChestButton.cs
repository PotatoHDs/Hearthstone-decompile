using System.Collections;
using UnityEngine;

public class AdventureClassChallengeChestButton : PegUIElement
{
	public GameObject m_RootObject;

	public Transform m_UpBone;

	public Transform m_DownBone;

	public GameObject m_HighlightPlane;

	public GameObject m_RewardBone;

	public GameObject m_RewardCard;

	public bool m_IsRewardLoading;

	protected override void OnOver(InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6", base.gameObject);
		ShowHighlight(show: true);
		StartCoroutine(ShowRewardCard());
	}

	protected override void OnOut(InteractionState oldState)
	{
		ShowHighlight(show: false);
		HideRewardCard();
	}

	public void Press()
	{
		SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6", base.gameObject);
		Depress();
		ShowHighlight(show: true);
		StartCoroutine(ShowRewardCard());
	}

	public void Release()
	{
		Raise();
		ShowHighlight(show: false);
		HideRewardCard();
	}

	private void Raise()
	{
		Hashtable args = iTween.Hash("position", m_UpBone.localPosition, "time", 0.1f, "easeType", iTween.EaseType.linear, "isLocal", true);
		iTween.MoveTo(m_RootObject, args);
	}

	private void Depress()
	{
		Hashtable args = iTween.Hash("position", m_DownBone.localPosition, "time", 0.1f, "easeType", iTween.EaseType.linear, "isLocal", true);
		iTween.MoveTo(m_RootObject, args);
	}

	private void ShowHighlight(bool show)
	{
		m_HighlightPlane.GetComponent<Renderer>().enabled = show;
	}

	private IEnumerator ShowRewardCard()
	{
		while (m_IsRewardLoading)
		{
			yield return null;
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette(0.4f, 0.2f, iTween.EaseType.easeOutCirc);
		fullScreenFXMgr.Blur(1f, 0.2f, iTween.EaseType.easeOutCirc);
		m_RewardBone.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.ScaleTo(m_RewardBone, new Vector3(10f, 10f, 10f), 0.2f);
		m_RewardCard.SetActive(value: true);
	}

	private void HideRewardCard()
	{
		iTween.ScaleTo(m_RewardBone, new Vector3(0.1f, 0.1f, 0.1f), 0.2f);
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.StopVignette();
		fullScreenFXMgr.StopBlur(0.2f, iTween.EaseType.easeOutCirc, EffectFadeOutFinished);
	}

	private void EffectFadeOutFinished()
	{
		if (!(this == null))
		{
			SceneUtils.SetLayer(base.gameObject, GameLayer.Default);
			if (m_RewardCard != null)
			{
				m_RewardCard.SetActive(value: false);
			}
		}
	}
}
