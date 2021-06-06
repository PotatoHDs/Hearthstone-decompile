using System;
using UnityEngine;

// Token: 0x02000654 RID: 1620
[CustomEditClass]
public class AdventureCompleteReward : Reward
{
	// Token: 0x06005B99 RID: 23449 RVA: 0x001DD6FF File Offset: 0x001DB8FF
	protected override void InitData()
	{
		base.SetData(new AdventureCompleteRewardData(), false);
	}

	// Token: 0x06005B9A RID: 23450 RVA: 0x001DD710 File Offset: 0x001DB910
	protected override void ShowReward(bool updateCacheValues)
	{
		if (base.IsShown)
		{
			return;
		}
		AdventureCompleteRewardData adventureCompleteRewardData = base.Data as AdventureCompleteRewardData;
		if (this.m_StateTable != null)
		{
			string eventName = (GameUtils.IsModeHeroic(adventureCompleteRewardData.ModeId) && this.m_StateTable.HasState("ShowBadlyHurt")) ? "ShowBadlyHurt" : "ShowHurt";
			this.m_StateTable.TriggerState(eventName, true, null);
		}
		if (this.m_BannerTextObject != null)
		{
			this.m_BannerTextObject.Text = adventureCompleteRewardData.BannerText;
		}
		if (this.m_BannerObject != null && this.m_BannerScaleOverride != null)
		{
			Vector3 vector = this.m_BannerScaleOverride;
			if (vector != Vector3.zero)
			{
				this.m_BannerObject.transform.localScale = vector;
			}
		}
		this.FadeFullscreenEffectsIn();
	}

	// Token: 0x06005B9B RID: 23451 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected override void PlayShowSounds()
	{
	}

	// Token: 0x06005B9C RID: 23452 RVA: 0x001DD7DF File Offset: 0x001DB9DF
	protected override void HideReward()
	{
		if (!base.IsShown)
		{
			return;
		}
		base.HideReward();
		if (this.m_StateTable != null)
		{
			this.m_StateTable.TriggerState("Hide", true, null);
		}
		this.FadeFullscreenEffectsOut();
	}

	// Token: 0x06005B9D RID: 23453 RVA: 0x001DD818 File Offset: 0x001DBA18
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
		fullScreenFXMgr.Vignette(0.4f, 0.5f, iTween.EaseType.easeOutCirc, null, null);
		fullScreenFXMgr.Blur(1f, 0.5f, iTween.EaseType.easeOutCirc, null);
	}

	// Token: 0x06005B9E RID: 23454 RVA: 0x001DD878 File Offset: 0x001DBA78
	private void FadeFullscreenEffectsOut()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("AdventureCompleteReward: FullScreenFXMgr.Get() returned null!");
			return;
		}
		fullScreenFXMgr.StopVignette(1f, iTween.EaseType.easeOutCirc, new Action(this.DestroyThis), null);
		fullScreenFXMgr.StopBlur(1f, iTween.EaseType.easeOutCirc, null, false);
	}

	// Token: 0x06005B9F RID: 23455 RVA: 0x001DD8C4 File Offset: 0x001DBAC4
	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		if (!(base.Data is AdventureCompleteRewardData))
		{
			Debug.LogWarning(string.Format("AdventureCompleteReward.OnDataSet() - Data {0} is not AdventureCompleteRewardData", base.Data));
			return;
		}
		base.EnableClickCatcher(true);
		base.RegisterClickListener(delegate(Reward reward, object userData)
		{
			this.HideReward();
		});
		base.SetReady(true);
	}

	// Token: 0x06005BA0 RID: 23456 RVA: 0x000ECB8B File Offset: 0x000EAD8B
	private void DestroyThis()
	{
		UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x04004E39 RID: 20025
	private const string s_EventShowHurt = "ShowHurt";

	// Token: 0x04004E3A RID: 20026
	private const string s_EventShowBadlyHurt = "ShowBadlyHurt";

	// Token: 0x04004E3B RID: 20027
	private const string s_EventHide = "Hide";

	// Token: 0x04004E3C RID: 20028
	[CustomEditField(Sections = "State Event Table")]
	public StateEventTable m_StateTable;

	// Token: 0x04004E3D RID: 20029
	[CustomEditField(Sections = "Banner")]
	public UberText m_BannerTextObject;

	// Token: 0x04004E3E RID: 20030
	[CustomEditField(Sections = "Banner")]
	public GameObject m_BannerObject;

	// Token: 0x04004E3F RID: 20031
	[CustomEditField(Sections = "Banner")]
	public Vector3_MobileOverride m_BannerScaleOverride;
}
