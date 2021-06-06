using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
[CustomEditClass]
public class AdventureSubSceneDisplay : MonoBehaviour
{
	// Token: 0x06000489 RID: 1161 RVA: 0x0001B325 File Offset: 0x00019525
	protected virtual void OnDestroy()
	{
		DefLoader.DisposableFullDef currentBossHeroPowerFullDef = this.m_CurrentBossHeroPowerFullDef;
		if (currentBossHeroPowerFullDef != null)
		{
			currentBossHeroPowerFullDef.Dispose();
		}
		this.m_CurrentBossHeroPowerFullDef = null;
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600048A RID: 1162 RVA: 0x0001B33F File Offset: 0x0001953F
	protected AssetLoadingHelper AssetLoadingHelper
	{
		get
		{
			if (this.m_assetLoadingHelper == null)
			{
				this.m_assetLoadingHelper = new AssetLoadingHelper();
				this.m_assetLoadingHelper.AssetLoadingComplete += this.OnAssetsLoaded;
			}
			return this.m_assetLoadingHelper;
		}
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x0001B371 File Offset: 0x00019571
	private void OnAssetsLoaded(object sender, EventArgs args)
	{
		this.OnSubSceneLoaded();
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x0001B37C File Offset: 0x0001957C
	public static Actor OnActorLoaded(string actorName, GameObject actorObject, GameObject container, bool withRotation = false)
	{
		Actor component = actorObject.GetComponent<Actor>();
		if (component != null)
		{
			if (container != null)
			{
				GameUtils.SetParent(component, container, withRotation);
				SceneUtils.SetLayer(component, container.layer);
			}
			component.SetUnlit();
			component.Hide();
		}
		else
		{
			Debug.LogWarning(string.Format("ERROR actor \"{0}\" has no Actor component", actorName));
		}
		return component;
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x0001B3D5 File Offset: 0x000195D5
	protected bool AddAssetToLoad(int assetCount = 1)
	{
		if (this.IsSubsceneLoaded())
		{
			return false;
		}
		this.AssetLoadingHelper.AddAssetToLoad(assetCount);
		return true;
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0001B3EF File Offset: 0x000195EF
	protected void AssetLoadCompleted()
	{
		if (this.IsSubsceneLoaded())
		{
			return;
		}
		this.AssetLoadingHelper.AssetLoadCompleted();
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x0001B408 File Offset: 0x00019608
	protected virtual void OnSubSceneLoaded()
	{
		AdventureSubScene component = base.GetComponent<AdventureSubScene>();
		if (component != null)
		{
			component.AddSubSceneTransitionFinishedListener(new AdventureSubScene.SubSceneTransitionFinished(this.OnSubSceneTransitionComplete));
			component.SetIsLoaded(true);
		}
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0001B440 File Offset: 0x00019640
	private bool IsSubsceneLoaded()
	{
		AdventureSubScene component = base.GetComponent<AdventureSubScene>();
		return component != null && component.IsLoaded();
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0001B468 File Offset: 0x00019668
	protected virtual void OnSubSceneTransitionComplete()
	{
		AdventureSubScene component = base.GetComponent<AdventureSubScene>();
		if (component != null)
		{
			component.RemoveSubSceneTransitionFinishedListener(new AdventureSubScene.SubSceneTransitionFinished(this.OnSubSceneTransitionComplete));
		}
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x0001B498 File Offset: 0x00019698
	protected void ShowBossPowerBigCard()
	{
		Vector3? origin = null;
		if (this.m_HeroPowerActor != null)
		{
			origin = new Vector3?(this.m_HeroPowerActor.gameObject.transform.position);
		}
		BigCardHelper.ShowBigCard(this.m_BossPowerBigCard, this.m_CurrentBossHeroPowerFullDef, this.m_HeroPowerBigCardBone, this.m_BigCardScale, origin);
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0001B4F5 File Offset: 0x000196F5
	protected void HideBossPowerBigCard()
	{
		BigCardHelper.HideBigCard(this.m_BossPowerBigCard);
	}

	// Token: 0x0400032F RID: 815
	[CustomEditField(Sections = "UI")]
	public float m_BigCardScale = 1f;

	// Token: 0x04000330 RID: 816
	[CustomEditField(Sections = "Bones")]
	public GameObject m_BossPowerBone;

	// Token: 0x04000331 RID: 817
	[CustomEditField(Sections = "Bones")]
	public GameObject m_HeroPowerBigCardBone;

	// Token: 0x04000332 RID: 818
	protected Actor m_BossActor;

	// Token: 0x04000333 RID: 819
	protected Actor m_HeroPowerActor;

	// Token: 0x04000334 RID: 820
	protected Actor m_BossPowerBigCard;

	// Token: 0x04000335 RID: 821
	protected Actor m_HeroPowerBigCard;

	// Token: 0x04000336 RID: 822
	protected DefLoader.DisposableFullDef m_CurrentBossHeroPowerFullDef;

	// Token: 0x04000337 RID: 823
	protected Vector3 m_BossPowerTweenOrigin;

	// Token: 0x04000338 RID: 824
	private AssetLoadingHelper m_assetLoadingHelper;
}
