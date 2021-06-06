using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class DungeonCrawlHeroDetails : MonoBehaviour
{
	// Token: 0x17000052 RID: 82
	// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00020684 File Offset: 0x0001E884
	public Actor HeroActor
	{
		get
		{
			return this.m_heroActor;
		}
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x0002068C File Offset: 0x0001E88C
	private void Awake()
	{
		AssetLoader.Get().InstantiatePrefab("Card_Dungeon_Play_Hero.prefab:183cb9cc59697844e911776ec349fe5e", new PrefabCallback<GameObject>(this.OnHeroActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab("Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af", new PrefabCallback<GameObject>(this.OnHeroPowerActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x000206DF File Offset: 0x0001E8DF
	private void OnDestroy()
	{
		DefLoader.DisposableCardDef heroCardDef = this.m_heroCardDef;
		if (heroCardDef != null)
		{
			heroCardDef.Dispose();
		}
		DefLoader.DisposableCardDef heroPowerCardDef = this.m_heroPowerCardDef;
		if (heroPowerCardDef == null)
		{
			return;
		}
		heroPowerCardDef.Dispose();
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00020704 File Offset: 0x0001E904
	private void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("AdventureHeroDetails.OnHeroActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		this.m_heroActor = go.GetComponent<Actor>();
		if (this.m_heroActor == null)
		{
			Debug.LogWarning(string.Format("AdventureHeroDetails.OnHeroActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		GameUtils.SetParent(go, this.m_hero_Bone, false);
		go.transform.parent = this.m_hero_Bone.transform;
		go.transform.localPosition = new Vector3(0f, 0f, 0f);
		go.layer = base.gameObject.layer;
		this.m_heroActor.SetUnlit();
		UnityEngine.Object.Destroy(this.m_heroActor.m_attackObject);
		this.m_heroActor.Hide();
		this.RefreshHeroInfo();
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x000207D8 File Offset: 0x0001E9D8
	private void OnHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("AdventureHeroDetails.OnHeroPowerActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		this.m_heroPowerActor = go.GetComponent<Actor>();
		if (this.m_heroPowerActor == null)
		{
			Debug.LogWarning(string.Format("AdventureHeroDetails.OnHeroPowerActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		this.m_heroPower = go.AddComponent<PegUIElement>();
		go.AddComponent<BoxCollider>();
		GameUtils.SetParent(go, this.m_heroPower_Bone, false);
		go.transform.parent = this.m_heroPower_Bone.transform;
		go.transform.localPosition = new Vector3(0f, 0f, 0f);
		go.layer = base.gameObject.layer;
		this.m_heroPowerActor.SetUnlit();
		this.m_heroPowerActor.Hide();
		this.m_heroPower.GetComponent<Collider>().enabled = true;
		this.m_heroName.Text = "";
		this.RefreshHeroPowerInfo();
		this.ApplyPendingUIEventsIfLoaded();
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x000208D4 File Offset: 0x0001EAD4
	private void OnHeroClassIconsControllerReady(Widget widget)
	{
		if (widget == null)
		{
			Debug.LogWarning("AdventureDungeonCrawlDisplay.OnHeroIconsControllerReady - widget was null!");
			return;
		}
		if (this.m_heroActor == null)
		{
			Debug.LogWarning("AdventureDungeonCrawlDisplay.OnHeroIconsControllerReady - m_heroActor was null!");
			return;
		}
		HeroClassIconsDataModel heroClassIconsDataModel = new HeroClassIconsDataModel();
		EntityDef entityDef = this.m_heroActor.GetEntityDef();
		if (entityDef == null)
		{
			Debug.LogWarning("AdventureDungeonCrawlDisplay.OnHeroIconsControllerReady - m_heroActor did not contain an entity def!");
			return;
		}
		heroClassIconsDataModel.Classes.Clear();
		foreach (TAG_CLASS item in entityDef.GetClasses(null))
		{
			heroClassIconsDataModel.Classes.Add(item);
		}
		widget.BindDataModel(heroClassIconsDataModel, false);
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00020988 File Offset: 0x0001EB88
	private void RefreshHeroInfo()
	{
		if (this.m_heroActor == null || this.m_heroEntityDef == null || this.m_heroCardDef == null)
		{
			return;
		}
		string name = this.m_heroEntityDef.GetName();
		this.m_heroName.Text = name;
		this.m_heroActor.SetPremium(TAG_PREMIUM.NORMAL);
		this.m_heroActor.SetEntityDef(this.m_heroEntityDef);
		this.m_heroActor.SetCardDef(this.m_heroCardDef);
		this.m_heroActor.UpdateAllComponents();
		this.m_heroActor.SetUnlit();
		this.m_heroActor.Show();
		this.m_heroClassIconsControllerReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnHeroClassIconsControllerReady));
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x00020A34 File Offset: 0x0001EC34
	private void RefreshHeroPowerInfo()
	{
		if (this.m_heroPowerActor == null || this.m_heroPowerEntityDef == null || this.m_heroPowerCardDef == null)
		{
			return;
		}
		this.m_heroPowerActor.SetPremium(TAG_PREMIUM.NORMAL);
		this.m_heroPowerActor.SetEntityDef(this.m_heroPowerEntityDef);
		this.m_heroPowerActor.SetCardDef(this.m_heroPowerCardDef);
		this.m_heroPowerActor.UpdateAllComponents();
		this.m_heroPowerActor.SetUnlit();
		this.m_heroPowerActor.AlwaysRenderPremiumPortrait = false;
		this.m_heroPowerActor.UpdateMaterials();
		this.m_heroPowerActor.SetUnlit();
		this.m_heroPowerActor.Show();
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x00020AD4 File Offset: 0x0001ECD4
	private void ApplyPendingUIEventsIfLoaded()
	{
		if (this.m_heroPower == null)
		{
			return;
		}
		foreach (DungeonCrawlHeroDetails.HeroPowerUIEvent heroPowerUIEvent in this.m_heroPowerPendingUIEvents)
		{
			this.m_heroPower.AddEventListener(heroPowerUIEvent.m_type, heroPowerUIEvent.m_handler);
		}
		this.m_heroPowerPendingUIEvents.Clear();
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00020B54 File Offset: 0x0001ED54
	public void UpdateHeroInfo(DefLoader.DisposableFullDef fullDef)
	{
		this.UpdateHeroInfo((fullDef != null) ? fullDef.EntityDef : null, (fullDef != null) ? fullDef.DisposableCardDef : null);
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x00020B74 File Offset: 0x0001ED74
	public void UpdateHeroInfo(EntityDef entityDef, DefLoader.DisposableCardDef cardDef)
	{
		this.m_heroEntityDef = entityDef;
		DefLoader.DisposableCardDef heroCardDef = this.m_heroCardDef;
		if (heroCardDef != null)
		{
			heroCardDef.Dispose();
		}
		this.m_heroCardDef = ((cardDef != null) ? cardDef.Share() : null);
		this.RefreshHeroInfo();
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x00020BA6 File Offset: 0x0001EDA6
	public void UpdateHeroPowerInfo(DefLoader.DisposableFullDef fullDef)
	{
		this.UpdateHeroPowerInfo((fullDef != null) ? fullDef.EntityDef : null, (fullDef != null) ? fullDef.DisposableCardDef : null);
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x00020BC6 File Offset: 0x0001EDC6
	public void UpdateHeroPowerInfo(EntityDef entityDef, DefLoader.DisposableCardDef cardDef)
	{
		this.m_heroPowerEntityDef = entityDef;
		DefLoader.DisposableCardDef heroCardDef = this.m_heroCardDef;
		if (heroCardDef != null)
		{
			heroCardDef.Dispose();
		}
		this.m_heroPowerCardDef = ((cardDef != null) ? cardDef.Share() : null);
		this.RefreshHeroPowerInfo();
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x00020BF8 File Offset: 0x0001EDF8
	public void AddHeroPowerListener(UIEventType type, UIEvent.Handler handler)
	{
		this.m_heroPowerPendingUIEvents.Add(new DungeonCrawlHeroDetails.HeroPowerUIEvent
		{
			m_type = type,
			m_handler = handler
		});
		this.ApplyPendingUIEventsIfLoaded();
	}

	// Token: 0x040003FA RID: 1018
	public AsyncReference m_heroClassIconsControllerReference;

	// Token: 0x040003FB RID: 1019
	public AsyncReference m_playButtonReference;

	// Token: 0x040003FC RID: 1020
	public Transform m_hero_Bone;

	// Token: 0x040003FD RID: 1021
	public Transform m_heroPower_Bone;

	// Token: 0x040003FE RID: 1022
	public UberText m_heroName;

	// Token: 0x040003FF RID: 1023
	private Actor m_heroActor;

	// Token: 0x04000400 RID: 1024
	private Actor m_heroPowerActor;

	// Token: 0x04000401 RID: 1025
	private Actor m_heroPowerBigCard;

	// Token: 0x04000402 RID: 1026
	private PegUIElement m_heroPower;

	// Token: 0x04000403 RID: 1027
	private EntityDef m_heroEntityDef;

	// Token: 0x04000404 RID: 1028
	private DefLoader.DisposableCardDef m_heroCardDef;

	// Token: 0x04000405 RID: 1029
	private EntityDef m_heroPowerEntityDef;

	// Token: 0x04000406 RID: 1030
	private DefLoader.DisposableCardDef m_heroPowerCardDef;

	// Token: 0x04000407 RID: 1031
	private List<DungeonCrawlHeroDetails.HeroPowerUIEvent> m_heroPowerPendingUIEvents = new List<DungeonCrawlHeroDetails.HeroPowerUIEvent>();

	// Token: 0x02001357 RID: 4951
	private class HeroPowerUIEvent
	{
		// Token: 0x0400A619 RID: 42521
		public UIEventType m_type;

		// Token: 0x0400A61A RID: 42522
		public UIEvent.Handler m_handler;
	}
}
