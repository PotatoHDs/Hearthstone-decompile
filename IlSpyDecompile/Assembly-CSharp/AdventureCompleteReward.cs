using UnityEngine;

[CustomEditClass]
public class AdventureCompleteReward : Reward
{
	private const string s_EventShowHurt = "ShowHurt";

	private const string s_EventShowBadlyHurt = "ShowBadlyHurt";

	private const string s_EventHide = "Hide";

	[CustomEditField(Sections = "State Event Table")]
	public StateEventTable m_StateTable;

	[CustomEditField(Sections = "Banner")]
	public UberText m_BannerTextObject;

	[CustomEditField(Sections = "Banner")]
	public GameObject m_BannerObject;

	[CustomEditField(Sections = "Banner")]
	public Vector3_MobileOverride m_BannerScaleOverride;

	protected override void InitData()
	{
		SetData(new AdventureCompleteRewardData(), updateVisuals: false);
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		if (base.IsShown)
		{
			return;
		}
		AdventureCompleteRewardData adventureCompleteRewardData = base.Data as AdventureCompleteRewardData;
		if (m_StateTable != null)
		{
			string eventName = ((GameUtils.IsModeHeroic(adventureCompleteRewardData.ModeId) && m_StateTable.HasState("ShowBadlyHurt")) ? "ShowBadlyHurt" : "ShowHurt");
			m_StateTable.TriggerState(eventName);
		}
		if (m_BannerTextObject != null)
		{
			m_BannerTextObject.Text = adventureCompleteRewardData.BannerText;
		}
		if (m_BannerObject != null && m_BannerScaleOverride != null)
		{
			Vector3 vector = m_BannerScaleOverride;
			if (vector != Vector3.zero)
			{
				m_BannerObject.transform.localScale = vector;
			}
		}
		FadeFullscreenEffectsIn();
	}

	protected override void PlayShowSounds()
	{
	}

	protected override void HideReward()
	{
		if (base.IsShown)
		{
			base.HideReward();
			if (m_StateTable != null)
			{
				m_StateTable.TriggerState("Hide");
			}
			FadeFullscreenEffectsOut();
		}
	}

	private void FadeFullscreenEffectsIn()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("AdventureCompleteReward: FullScreenFXMgr.Get() returned null!");
			return;
		}
		fullScreenFXMgr.SetBlurBrightness(0.85f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette(0.4f, 0.5f, iTween.EaseType.easeOutCirc);
		fullScreenFXMgr.Blur(1f, 0.5f, iTween.EaseType.easeOutCirc);
	}

	private void FadeFullscreenEffectsOut()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("AdventureCompleteReward: FullScreenFXMgr.Get() returned null!");
			return;
		}
		fullScreenFXMgr.StopVignette(1f, iTween.EaseType.easeOutCirc, DestroyThis);
		fullScreenFXMgr.StopBlur(1f, iTween.EaseType.easeOutCirc);
	}

	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		if (!(base.Data is AdventureCompleteRewardData))
		{
			Debug.LogWarning($"AdventureCompleteReward.OnDataSet() - Data {base.Data} is not AdventureCompleteRewardData");
			return;
		}
		EnableClickCatcher(enabled: true);
		RegisterClickListener(delegate
		{
			HideReward();
		});
		SetReady(ready: true);
	}

	private void DestroyThis()
	{
		Object.DestroyImmediate(base.gameObject);
	}
}
