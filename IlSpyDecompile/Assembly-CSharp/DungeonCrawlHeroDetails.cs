using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class DungeonCrawlHeroDetails : MonoBehaviour
{
	private class HeroPowerUIEvent
	{
		public UIEventType m_type;

		public UIEvent.Handler m_handler;
	}

	public AsyncReference m_heroClassIconsControllerReference;

	public AsyncReference m_playButtonReference;

	public Transform m_hero_Bone;

	public Transform m_heroPower_Bone;

	public UberText m_heroName;

	private Actor m_heroActor;

	private Actor m_heroPowerActor;

	private Actor m_heroPowerBigCard;

	private PegUIElement m_heroPower;

	private EntityDef m_heroEntityDef;

	private DefLoader.DisposableCardDef m_heroCardDef;

	private EntityDef m_heroPowerEntityDef;

	private DefLoader.DisposableCardDef m_heroPowerCardDef;

	private List<HeroPowerUIEvent> m_heroPowerPendingUIEvents = new List<HeroPowerUIEvent>();

	public Actor HeroActor => m_heroActor;

	private void Awake()
	{
		AssetLoader.Get().InstantiatePrefab("Card_Dungeon_Play_Hero.prefab:183cb9cc59697844e911776ec349fe5e", OnHeroActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab("Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af", OnHeroPowerActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void OnDestroy()
	{
		m_heroCardDef?.Dispose();
		m_heroPowerCardDef?.Dispose();
	}

	private void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"AdventureHeroDetails.OnHeroActorLoaded() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		m_heroActor = go.GetComponent<Actor>();
		if (m_heroActor == null)
		{
			Debug.LogWarning($"AdventureHeroDetails.OnHeroActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			return;
		}
		GameUtils.SetParent(go, m_hero_Bone);
		go.transform.parent = m_hero_Bone.transform;
		go.transform.localPosition = new Vector3(0f, 0f, 0f);
		go.layer = base.gameObject.layer;
		m_heroActor.SetUnlit();
		Object.Destroy(m_heroActor.m_attackObject);
		m_heroActor.Hide();
		RefreshHeroInfo();
	}

	private void OnHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"AdventureHeroDetails.OnHeroPowerActorLoaded() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		m_heroPowerActor = go.GetComponent<Actor>();
		if (m_heroPowerActor == null)
		{
			Debug.LogWarning($"AdventureHeroDetails.OnHeroPowerActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			return;
		}
		m_heroPower = go.AddComponent<PegUIElement>();
		go.AddComponent<BoxCollider>();
		GameUtils.SetParent(go, m_heroPower_Bone);
		go.transform.parent = m_heroPower_Bone.transform;
		go.transform.localPosition = new Vector3(0f, 0f, 0f);
		go.layer = base.gameObject.layer;
		m_heroPowerActor.SetUnlit();
		m_heroPowerActor.Hide();
		m_heroPower.GetComponent<Collider>().enabled = true;
		m_heroName.Text = "";
		RefreshHeroPowerInfo();
		ApplyPendingUIEventsIfLoaded();
	}

	private void OnHeroClassIconsControllerReady(Widget widget)
	{
		if (widget == null)
		{
			Debug.LogWarning("AdventureDungeonCrawlDisplay.OnHeroIconsControllerReady - widget was null!");
			return;
		}
		if (m_heroActor == null)
		{
			Debug.LogWarning("AdventureDungeonCrawlDisplay.OnHeroIconsControllerReady - m_heroActor was null!");
			return;
		}
		HeroClassIconsDataModel heroClassIconsDataModel = new HeroClassIconsDataModel();
		EntityDef entityDef = m_heroActor.GetEntityDef();
		if (entityDef == null)
		{
			Debug.LogWarning("AdventureDungeonCrawlDisplay.OnHeroIconsControllerReady - m_heroActor did not contain an entity def!");
			return;
		}
		heroClassIconsDataModel.Classes.Clear();
		foreach (TAG_CLASS @class in entityDef.GetClasses())
		{
			heroClassIconsDataModel.Classes.Add(@class);
		}
		widget.BindDataModel(heroClassIconsDataModel);
	}

	private void RefreshHeroInfo()
	{
		if (!(m_heroActor == null) && m_heroEntityDef != null && m_heroCardDef != null)
		{
			string text = m_heroEntityDef.GetName();
			m_heroName.Text = text;
			m_heroActor.SetPremium(TAG_PREMIUM.NORMAL);
			m_heroActor.SetEntityDef(m_heroEntityDef);
			m_heroActor.SetCardDef(m_heroCardDef);
			m_heroActor.UpdateAllComponents();
			m_heroActor.SetUnlit();
			m_heroActor.Show();
			m_heroClassIconsControllerReference.RegisterReadyListener<Widget>(OnHeroClassIconsControllerReady);
		}
	}

	private void RefreshHeroPowerInfo()
	{
		if (!(m_heroPowerActor == null) && m_heroPowerEntityDef != null && m_heroPowerCardDef != null)
		{
			m_heroPowerActor.SetPremium(TAG_PREMIUM.NORMAL);
			m_heroPowerActor.SetEntityDef(m_heroPowerEntityDef);
			m_heroPowerActor.SetCardDef(m_heroPowerCardDef);
			m_heroPowerActor.UpdateAllComponents();
			m_heroPowerActor.SetUnlit();
			m_heroPowerActor.AlwaysRenderPremiumPortrait = false;
			m_heroPowerActor.UpdateMaterials();
			m_heroPowerActor.SetUnlit();
			m_heroPowerActor.Show();
		}
	}

	private void ApplyPendingUIEventsIfLoaded()
	{
		if (m_heroPower == null)
		{
			return;
		}
		foreach (HeroPowerUIEvent heroPowerPendingUIEvent in m_heroPowerPendingUIEvents)
		{
			m_heroPower.AddEventListener(heroPowerPendingUIEvent.m_type, heroPowerPendingUIEvent.m_handler);
		}
		m_heroPowerPendingUIEvents.Clear();
	}

	public void UpdateHeroInfo(DefLoader.DisposableFullDef fullDef)
	{
		UpdateHeroInfo(fullDef?.EntityDef, fullDef?.DisposableCardDef);
	}

	public void UpdateHeroInfo(EntityDef entityDef, DefLoader.DisposableCardDef cardDef)
	{
		m_heroEntityDef = entityDef;
		m_heroCardDef?.Dispose();
		m_heroCardDef = cardDef?.Share();
		RefreshHeroInfo();
	}

	public void UpdateHeroPowerInfo(DefLoader.DisposableFullDef fullDef)
	{
		UpdateHeroPowerInfo(fullDef?.EntityDef, fullDef?.DisposableCardDef);
	}

	public void UpdateHeroPowerInfo(EntityDef entityDef, DefLoader.DisposableCardDef cardDef)
	{
		m_heroPowerEntityDef = entityDef;
		m_heroCardDef?.Dispose();
		m_heroPowerCardDef = cardDef?.Share();
		RefreshHeroPowerInfo();
	}

	public void AddHeroPowerListener(UIEventType type, UIEvent.Handler handler)
	{
		m_heroPowerPendingUIEvents.Add(new HeroPowerUIEvent
		{
			m_type = type,
			m_handler = handler
		});
		ApplyPendingUIEventsIfLoaded();
	}
}
