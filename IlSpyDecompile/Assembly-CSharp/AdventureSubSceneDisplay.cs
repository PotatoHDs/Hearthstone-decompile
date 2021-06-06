using System;
using UnityEngine;

[CustomEditClass]
public class AdventureSubSceneDisplay : MonoBehaviour
{
	[CustomEditField(Sections = "UI")]
	public float m_BigCardScale = 1f;

	[CustomEditField(Sections = "Bones")]
	public GameObject m_BossPowerBone;

	[CustomEditField(Sections = "Bones")]
	public GameObject m_HeroPowerBigCardBone;

	protected Actor m_BossActor;

	protected Actor m_HeroPowerActor;

	protected Actor m_BossPowerBigCard;

	protected Actor m_HeroPowerBigCard;

	protected DefLoader.DisposableFullDef m_CurrentBossHeroPowerFullDef;

	protected Vector3 m_BossPowerTweenOrigin;

	private AssetLoadingHelper m_assetLoadingHelper;

	protected AssetLoadingHelper AssetLoadingHelper
	{
		get
		{
			if (m_assetLoadingHelper == null)
			{
				m_assetLoadingHelper = new AssetLoadingHelper();
				m_assetLoadingHelper.AssetLoadingComplete += OnAssetsLoaded;
			}
			return m_assetLoadingHelper;
		}
	}

	protected virtual void OnDestroy()
	{
		m_CurrentBossHeroPowerFullDef?.Dispose();
		m_CurrentBossHeroPowerFullDef = null;
	}

	private void OnAssetsLoaded(object sender, EventArgs args)
	{
		OnSubSceneLoaded();
	}

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
			Debug.LogWarning($"ERROR actor \"{actorName}\" has no Actor component");
		}
		return component;
	}

	protected bool AddAssetToLoad(int assetCount = 1)
	{
		if (IsSubsceneLoaded())
		{
			return false;
		}
		AssetLoadingHelper.AddAssetToLoad(assetCount);
		return true;
	}

	protected void AssetLoadCompleted()
	{
		if (!IsSubsceneLoaded())
		{
			AssetLoadingHelper.AssetLoadCompleted();
		}
	}

	protected virtual void OnSubSceneLoaded()
	{
		AdventureSubScene component = GetComponent<AdventureSubScene>();
		if (component != null)
		{
			component.AddSubSceneTransitionFinishedListener(OnSubSceneTransitionComplete);
			component.SetIsLoaded(loaded: true);
		}
	}

	private bool IsSubsceneLoaded()
	{
		AdventureSubScene component = GetComponent<AdventureSubScene>();
		if (component != null)
		{
			return component.IsLoaded();
		}
		return false;
	}

	protected virtual void OnSubSceneTransitionComplete()
	{
		AdventureSubScene component = GetComponent<AdventureSubScene>();
		if (component != null)
		{
			component.RemoveSubSceneTransitionFinishedListener(OnSubSceneTransitionComplete);
		}
	}

	protected void ShowBossPowerBigCard()
	{
		Vector3? origin = null;
		if (m_HeroPowerActor != null)
		{
			origin = m_HeroPowerActor.gameObject.transform.position;
		}
		BigCardHelper.ShowBigCard(m_BossPowerBigCard, m_CurrentBossHeroPowerFullDef, m_HeroPowerBigCardBone, m_BigCardScale, origin);
	}

	protected void HideBossPowerBigCard()
	{
		BigCardHelper.HideBigCard(m_BossPowerBigCard);
	}
}
