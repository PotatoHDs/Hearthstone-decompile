using System;
using Assets;
using Blizzard.T5.Core;
using UnityEngine;

// Token: 0x0200070F RID: 1807
public class ModularBundleNode : MonoBehaviour
{
	// Token: 0x170005EF RID: 1519
	// (get) Token: 0x060064E6 RID: 25830 RVA: 0x0020EBF3 File Offset: 0x0020CDF3
	// (set) Token: 0x060064E7 RID: 25831 RVA: 0x0020EBFB File Offset: 0x0020CDFB
	public float DelayBeforeEntryAnimation { get; private set; }

	// Token: 0x060064E8 RID: 25832 RVA: 0x0020EC04 File Offset: 0x0020CE04
	public void Initialize(ModularBundleNodeLayout parent, ModularBundleLayoutNodeDbfRecord nodeRecord)
	{
		this.m_nodeRecord = nodeRecord;
		this.m_parentLayout = parent;
		base.gameObject.SetActive(false);
		this.DelayBeforeEntryAnimation = (float)nodeRecord.EntryDelay;
		this.m_entryAnimationTrigger = nodeRecord.EntryAnimation;
		this.m_exitAnimationTrigger = nodeRecord.ExitAnimation;
		this.LoadNodeAsset();
	}

	// Token: 0x060064E9 RID: 25833 RVA: 0x0020EC56 File Offset: 0x0020CE56
	public bool IsReady()
	{
		return this.m_assetsLoaded;
	}

	// Token: 0x060064EA RID: 25834 RVA: 0x0020EC5E File Offset: 0x0020CE5E
	public int GetNodeShakeWeight()
	{
		if (this.m_nodeRecord == null)
		{
			return 0;
		}
		return this.m_nodeRecord.ShakeWeight;
	}

	// Token: 0x060064EB RID: 25835 RVA: 0x0020EC78 File Offset: 0x0020CE78
	public void AttachLoadedPrefabObjectAsChild(GameObject loadedPrefab, bool withRotation = false)
	{
		if (loadedPrefab == null)
		{
			return;
		}
		GameUtils.SetParent(loadedPrefab, this, withRotation);
		this.m_animator = loadedPrefab.GetComponent<Animator>();
		if (this.m_animator == null)
		{
			this.m_animator = loadedPrefab.AddComponent<Animator>();
		}
		ModularBundleSounds component = loadedPrefab.GetComponent<ModularBundleSounds>();
		if (component != null)
		{
			component.Initialize(this.m_nodeRecord.EntrySound, this.m_nodeRecord.LandingSound, this.m_nodeRecord.ExitSound);
		}
		loadedPrefab.transform.localRotation = base.transform.localRotation * loadedPrefab.transform.localRotation;
		loadedPrefab.transform.localScale = Vector3.Scale(base.transform.localScale, loadedPrefab.transform.localScale);
		base.transform.localRotation = Quaternion.identity;
		base.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x060064EC RID: 25836 RVA: 0x0020ED70 File Offset: 0x0020CF70
	public void PlayEntryAnimation()
	{
		base.gameObject.SetActive(true);
		if (this.m_animator == null || string.IsNullOrEmpty(this.m_entryAnimationTrigger))
		{
			return;
		}
		this.m_animator.enabled = true;
		this.m_animator.SetTrigger(this.m_entryAnimationTrigger);
		this.m_animator.speed = (float)this.m_nodeRecord.AnimSpeedMultiplier;
	}

	// Token: 0x060064ED RID: 25837 RVA: 0x0020EDDC File Offset: 0x0020CFDC
	public void PlayExitAnimation()
	{
		if (this.m_animator == null || string.IsNullOrEmpty(this.m_exitAnimationTrigger))
		{
			return;
		}
		this.m_animator.enabled = true;
		this.m_animator.SetTrigger(this.m_exitAnimationTrigger);
		this.m_animator.speed = (float)this.m_nodeRecord.AnimSpeedMultiplier;
	}

	// Token: 0x060064EE RID: 25838 RVA: 0x00028159 File Offset: 0x00026359
	public void EnterImmediately()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x060064EF RID: 25839 RVA: 0x00028167 File Offset: 0x00026367
	public void ExitImmediately()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060064F0 RID: 25840 RVA: 0x0020EE3C File Offset: 0x0020D03C
	private void LoadNodeAsset()
	{
		this.m_assetsLoaded = false;
		switch (this.m_nodeRecord.DisplayType)
		{
		case ModularBundleLayoutNode.DisplayType.BOOSTER:
			this.LoadNodeAsset_Booster();
			return;
		case ModularBundleLayoutNode.DisplayType.TEXT:
			this.LoadNodeAsset_Text();
			return;
		case ModularBundleLayoutNode.DisplayType.DUST:
			this.LoadNodeAsset_Dust();
			return;
		case ModularBundleLayoutNode.DisplayType.PREFAB:
			this.LoadNodeAsset_Prefab();
			return;
		case ModularBundleLayoutNode.DisplayType.HERO_SKIN:
			this.LoadNodeAsset_HeroSkin();
			return;
		case ModularBundleLayoutNode.DisplayType.CARD_BACK:
			this.LoadNodeAsset_CardBack();
			return;
		case ModularBundleLayoutNode.DisplayType.ARENA_TICKET:
			this.LoadNodeAsset_ArenaTicket();
			return;
		default:
			Debug.LogWarningFormat("ModularBundleNode.LoadNodeAsset() - no load function for display type {0}!", new object[]
			{
				this.m_nodeRecord.DisplayType
			});
			return;
		}
	}

	// Token: 0x060064F1 RID: 25841 RVA: 0x0020EED8 File Offset: 0x0020D0D8
	private void LoadNodeAsset_Booster()
	{
		GeneralStorePacksContent parent = this.m_parentLayout.GetDisplay().GetParent();
		if (parent == null)
		{
			Debug.LogError("ModularBundleNode.LoadNodeAsset_Booster() - no parent display!");
			return;
		}
		StorePackId storePackId;
		storePackId.Type = StorePackType.BOOSTER;
		storePackId.Id = this.m_nodeRecord.DisplayData;
		IStorePackDef storePackDef = parent.GetStorePackDef(storePackId);
		ModularBundleNode.NodeCallbackData nodeCallbackData = new ModularBundleNode.NodeCallbackData();
		nodeCallbackData.requester = this;
		nodeCallbackData.callbackData = storePackId;
		AssetLoader.Get().InstantiatePrefab(storePackDef.GetLowPolyPrefab(), new PrefabCallback<GameObject>(ModularBundleNode.OnNodeAssetLoaded_Booster), nodeCallbackData, AssetLoadingOptions.None);
	}

	// Token: 0x060064F2 RID: 25842 RVA: 0x0020EF6C File Offset: 0x0020D16C
	private static void OnNodeAssetLoaded_Booster(AssetReference assetRef, GameObject go, object callbackData)
	{
		ModularBundleNode.NodeCallbackData nodeCallbackData = (ModularBundleNode.NodeCallbackData)callbackData;
		if (nodeCallbackData == null || !GeneralUtils.IsObjectAlive(nodeCallbackData.requester))
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		ModularBundleNode requester = nodeCallbackData.requester;
		StorePackId storePackId = (StorePackId)nodeCallbackData.callbackData;
		requester.m_assetsLoaded = true;
		AnimatedLowPolyPack component = go.GetComponent<AnimatedLowPolyPack>();
		if (component == null)
		{
			Log.All.PrintWarning("Modular Bundle Error: Layout prefab node expected to be a Pack node but loaded cardPackId={0} assetRef={1} does not have AnimatedLowPolyPack component script dbiNodeId={2} dbiNodeLayoutId={3} text={4} for gameObject in hierarchy:\n{5}", new object[]
			{
				storePackId.Id,
				assetRef,
				requester.m_nodeRecord.ID,
				requester.m_nodeRecord.NodeLayoutId,
				requester.m_nodeRecord.DisplayText.GetString(true),
				DebugUtils.GetHierarchyPath(requester, '.')
			});
			Error.AddDevWarning("Modular Bundle Error", string.Format("Layout node={0} expected to be a Pack node but loaded cardPackId={1} does not have AnimatedLowPolyPack component script; layout={3}. See the [All] log for more details.", requester.gameObject.name, storePackId.Id, (requester.m_parentLayout == null) ? "<null>" : requester.m_parentLayout.gameObject.name), Array.Empty<object>());
		}
		else
		{
			component.m_AmountBanner.SetActive(true);
			component.m_AmountBannerText.Text = requester.m_nodeRecord.DisplayCount.ToString();
		}
		SceneUtils.SetLayer(go, GameLayer.PerspectiveUI);
		go.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		requester.AttachLoadedPrefabObjectAsChild(go, true);
	}

	// Token: 0x060064F3 RID: 25843 RVA: 0x0020F0E8 File Offset: 0x0020D2E8
	private void LoadNodeAsset_Text()
	{
		this.m_assetsLoaded = true;
		this.m_animator = base.GetComponentInChildren<Animator>();
		ModularBundleText componentInChildren = base.GetComponentInChildren<ModularBundleText>();
		if (componentInChildren == null)
		{
			Log.All.PrintWarning("Modular Bundle Error: Layout prefab node expected to be a Text node but does not have ModularBundleText component script dbiNodeId={0} dbiNodeLayoutId={1} text={2} for gameObject in hierarchy:\n{3}", new object[]
			{
				this.m_nodeRecord.ID,
				this.m_nodeRecord.NodeLayoutId,
				this.m_nodeRecord.DisplayText.GetString(true),
				DebugUtils.GetHierarchyPath(this, '.')
			});
			Error.AddDevWarning("Modular Bundle Error", string.Format("Layout node={0} expected to be a Text node but does not have ModularBundleText component; layout={1}. See the [All] log for more details.", base.gameObject.name, (this.m_parentLayout == null) ? "<null>" : this.m_parentLayout.gameObject.name), Array.Empty<object>());
			return;
		}
		componentInChildren.Text.Text = this.m_nodeRecord.DisplayText;
		componentInChildren.SetGlowSize(this.m_nodeRecord.DisplayTextGlowSize);
	}

	// Token: 0x060064F4 RID: 25844 RVA: 0x0020F1EC File Offset: 0x0020D3EC
	private void LoadNodeAsset_Dust()
	{
		ModularBundleNode.NodeCallbackData nodeCallbackData = new ModularBundleNode.NodeCallbackData();
		nodeCallbackData.requester = this;
		AssetLoader.Get().InstantiatePrefab(ModularBundleNode.DustJarAssetReference, new PrefabCallback<GameObject>(ModularBundleNode.OnNodeAssetLoaded_Dust), nodeCallbackData, AssetLoadingOptions.None);
	}

	// Token: 0x060064F5 RID: 25845 RVA: 0x0020F224 File Offset: 0x0020D424
	private static void OnNodeAssetLoaded_Dust(AssetReference assetRef, GameObject go, object callbackData)
	{
		ModularBundleNode.NodeCallbackData nodeCallbackData = (ModularBundleNode.NodeCallbackData)callbackData;
		if (nodeCallbackData == null || !GeneralUtils.IsObjectAlive(nodeCallbackData.requester))
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		ModularBundleNode requester = nodeCallbackData.requester;
		requester.m_assetsLoaded = true;
		SceneUtils.SetLayer(go, GameLayer.PerspectiveUI);
		requester.AttachLoadedPrefabObjectAsChild(go, true);
		ModularBundleDustJar component = go.GetComponent<ModularBundleDustJar>();
		if (component == null)
		{
			Log.All.PrintWarning("Modular Bundle Error: Layout prefab node expected to be a DustJar node but loaded assetRef={0} does not have ModularBundleDustJar component script dbiNodeId={1} dbiNodeLayoutId={2} text={3} for gameObject in hierarchy:\n{4}", new object[]
			{
				assetRef,
				requester.m_nodeRecord.ID,
				requester.m_nodeRecord.NodeLayoutId,
				requester.m_nodeRecord.DisplayText.GetString(true),
				DebugUtils.GetHierarchyPath(requester, '.')
			});
			Error.AddDevWarning("Modular Bundle Error", string.Format("Layout node={0} expected to be a DustJar node but does not have ModularBundleDustJar component; layout={1}. See the [All] log for more details.", requester.gameObject.name, (requester.m_parentLayout == null) ? "<null>" : requester.m_parentLayout.gameObject.name), Array.Empty<object>());
			return;
		}
		component.AmountText.Text = requester.m_nodeRecord.DisplayCount.ToString();
		if (!string.IsNullOrEmpty(requester.m_nodeRecord.DisplayText))
		{
			component.HeaderText.Text.Text = requester.m_nodeRecord.DisplayText;
			component.HeaderText.SetGlowSize(requester.m_nodeRecord.DisplayTextGlowSize);
		}
		component.KeepHeaderTextStraight();
	}

	// Token: 0x060064F6 RID: 25846 RVA: 0x0020F39C File Offset: 0x0020D59C
	private void LoadNodeAsset_ArenaTicket()
	{
		ModularBundleNode.NodeCallbackData nodeCallbackData = new ModularBundleNode.NodeCallbackData();
		nodeCallbackData.requester = this;
		AssetLoader.Get().InstantiatePrefab(ModularBundleNode.ArenaTicketAssetReference, new PrefabCallback<GameObject>(ModularBundleNode.OnNodeAssetLoaded_ArenaTicket), nodeCallbackData, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x060064F7 RID: 25847 RVA: 0x0020F3D4 File Offset: 0x0020D5D4
	private static void OnNodeAssetLoaded_ArenaTicket(AssetReference assetRef, GameObject go, object callbackData)
	{
		ModularBundleNode.NodeCallbackData nodeCallbackData = (ModularBundleNode.NodeCallbackData)callbackData;
		if (nodeCallbackData == null || !GeneralUtils.IsObjectAlive(nodeCallbackData.requester))
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		ModularBundleNode requester = nodeCallbackData.requester;
		requester.m_assetsLoaded = true;
		SceneUtils.SetLayer(go, GameLayer.PerspectiveUI);
		requester.AttachLoadedPrefabObjectAsChild(go, true);
		ModularBundleArenaTicket component = go.GetComponent<ModularBundleArenaTicket>();
		if (component == null)
		{
			Log.All.PrintWarning("Modular Bundle Error: Layout prefab node expected to be a ArenaTicket node but loaded assetRef={0} does not have ModularBundleArenaTicket component script dbiNodeId={1} dbiNodeLayoutId={2} text={3} for gameObject in hierarchy:\n{4}", new object[]
			{
				assetRef,
				requester.m_nodeRecord.ID,
				requester.m_nodeRecord.NodeLayoutId,
				requester.m_nodeRecord.DisplayText.GetString(true),
				DebugUtils.GetHierarchyPath(requester, '.')
			});
			Error.AddDevWarning("Modular Bundle Error", string.Format("Layout node={0} expected to be a ArenaTicket node but does not have ModularBundleArenaTicket component; layout={1}. See the [All] log for more details.", requester.gameObject.name, (requester.m_parentLayout == null) ? "<null>" : requester.gameObject.name), Array.Empty<object>());
			return;
		}
		component.AmountText.Text = requester.m_nodeRecord.DisplayCount.ToString();
		if (!string.IsNullOrEmpty(requester.m_nodeRecord.DisplayText))
		{
			component.HeaderText.Text = requester.m_nodeRecord.DisplayText;
		}
	}

	// Token: 0x060064F8 RID: 25848 RVA: 0x0020F524 File Offset: 0x0020D724
	private void LoadNodeAsset_Prefab()
	{
		string displayPrefab = this.m_nodeRecord.DisplayPrefab;
		ModularBundleNode.NodeCallbackData nodeCallbackData = new ModularBundleNode.NodeCallbackData();
		nodeCallbackData.requester = this;
		AssetLoader.Get().InstantiatePrefab(displayPrefab, new PrefabCallback<GameObject>(ModularBundleNode.OnNodeAssetLoaded_Prefab), nodeCallbackData, AssetLoadingOptions.None);
	}

	// Token: 0x060064F9 RID: 25849 RVA: 0x0020F56C File Offset: 0x0020D76C
	private static void OnNodeAssetLoaded_Prefab(AssetReference assetRef, GameObject go, object callbackData)
	{
		ModularBundleNode.NodeCallbackData nodeCallbackData = (ModularBundleNode.NodeCallbackData)callbackData;
		if (nodeCallbackData == null || !GeneralUtils.IsObjectAlive(nodeCallbackData.requester))
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		ModularBundleNode requester = nodeCallbackData.requester;
		requester.m_assetsLoaded = true;
		SceneUtils.SetLayer(go, GameLayer.PerspectiveUI);
		requester.AttachLoadedPrefabObjectAsChild(go, true);
	}

	// Token: 0x060064FA RID: 25850 RVA: 0x0020F5B4 File Offset: 0x0020D7B4
	private void LoadNodeAsset_HeroSkin()
	{
		string input = "Modular_Bundle_Card_Hero_Skin.prefab:ad8fda5915cc96747abd0e15821c9857";
		ModularBundleNode.NodeCallbackData nodeCallbackData = new ModularBundleNode.NodeCallbackData();
		nodeCallbackData.requester = this;
		AssetLoader.Get().InstantiatePrefab(input, new PrefabCallback<GameObject>(ModularBundleNode.OnNodeAssetLoaded_HeroSkin), nodeCallbackData, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x060064FB RID: 25851 RVA: 0x0020F5F4 File Offset: 0x0020D7F4
	private static void OnNodeAssetLoaded_HeroSkin(AssetReference assetRef, GameObject go, object callbackData)
	{
		ModularBundleNode.NodeCallbackData nodeCallbackData = (ModularBundleNode.NodeCallbackData)callbackData;
		if (nodeCallbackData == null || !GeneralUtils.IsObjectAlive(nodeCallbackData.requester))
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		ModularBundleNode requester = nodeCallbackData.requester;
		if (go == null)
		{
			Debug.LogWarningFormat("LoadNodeAsset_HeroSkin - FAILED to load \"{0}\"", new object[]
			{
				assetRef
			});
			requester.m_assetsLoaded = true;
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			requester.m_assetsLoaded = true;
			Log.All.PrintWarning("Modular Bundle Error: Layout prefab node expected to be a HeroSkin node but loaded assetRef={0} does not have Actor component script dbiNodeId={1} dbiNodeLayoutId={2} text={3} for gameObject in hierarchy:\n{4}", new object[]
			{
				assetRef,
				requester.m_nodeRecord.ID,
				requester.m_nodeRecord.NodeLayoutId,
				requester.m_nodeRecord.DisplayText.GetString(true),
				DebugUtils.GetHierarchyPath(requester, '.')
			});
			Error.AddDevWarning("Modular Bundle Error", string.Format("Layout node={0} expected to be a HeroSkin node but does not have Actor component; layout={1}. See the [All] log for more details.", requester.gameObject.name, (requester.m_parentLayout == null) ? "<null>" : requester.gameObject.name), Array.Empty<object>());
			return;
		}
		string cardId = GameUtils.TranslateDbIdToCardId(requester.m_nodeRecord.DisplayData, false);
		ModularBundleNode.NodeCallbackData nodeCallbackData2 = new ModularBundleNode.NodeCallbackData();
		nodeCallbackData2.requester = requester;
		nodeCallbackData2.callbackData = component;
		DefLoader.Get().LoadFullDef(cardId, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(ModularBundleNode.OnCardFullDefLoaded_HeroSkin), nodeCallbackData2, null);
	}

	// Token: 0x060064FC RID: 25852 RVA: 0x0020F74C File Offset: 0x0020D94C
	private static void OnCardFullDefLoaded_HeroSkin(string cardID, DefLoader.DisposableFullDef fullDef, object callbackData)
	{
		try
		{
			ModularBundleNode.NodeCallbackData nodeCallbackData = (ModularBundleNode.NodeCallbackData)callbackData;
			if (nodeCallbackData != null && GeneralUtils.IsObjectAlive(nodeCallbackData.requester))
			{
				ModularBundleNode requester = nodeCallbackData.requester;
				requester.m_assetsLoaded = true;
				Actor actor = (Actor)nodeCallbackData.callbackData;
				actor.SetFullDef(fullDef);
				actor.HideAllText();
				actor.UpdateAllComponents();
				CollectionHeroSkin component = actor.GetComponent<CollectionHeroSkin>();
				if (component != null)
				{
					component.SetClass(fullDef.EntityDef.GetClass());
				}
				SceneUtils.SetLayer(actor.gameObject, GameLayer.PerspectiveUI);
				requester.AttachLoadedPrefabObjectAsChild(actor.gameObject, true);
			}
		}
		finally
		{
			if (fullDef != null)
			{
				((IDisposable)fullDef).Dispose();
			}
		}
	}

	// Token: 0x060064FD RID: 25853 RVA: 0x0020F7F8 File Offset: 0x0020D9F8
	private void LoadNodeAsset_CardBack()
	{
		string actorName = "Modular_Bundle_Card_Back.prefab:939c318747e79d54f81ad2abab4584a2";
		ModularBundleNode.NodeCallbackData nodeCallbackData = new ModularBundleNode.NodeCallbackData();
		nodeCallbackData.requester = this;
		CardBackManager.Get().LoadCardBackByIndex(this.m_nodeRecord.DisplayData, new CardBackManager.LoadCardBackData.LoadCardBackCallback(ModularBundleNode.OnNodeAssetLoaded_CardBack), actorName, nodeCallbackData);
	}

	// Token: 0x060064FE RID: 25854 RVA: 0x0020F83C File Offset: 0x0020DA3C
	private static void OnNodeAssetLoaded_CardBack(CardBackManager.LoadCardBackData cardBackData)
	{
		ModularBundleNode.NodeCallbackData nodeCallbackData = (ModularBundleNode.NodeCallbackData)cardBackData.callbackData;
		if (nodeCallbackData == null || !GeneralUtils.IsObjectAlive(nodeCallbackData.requester))
		{
			UnityEngine.Object.Destroy(cardBackData.m_GameObject);
			return;
		}
		ModularBundleNode requester = nodeCallbackData.requester;
		requester.m_assetsLoaded = true;
		SceneUtils.SetLayer(cardBackData.m_GameObject, GameLayer.PerspectiveUI);
		requester.AttachLoadedPrefabObjectAsChild(cardBackData.m_GameObject, true);
	}

	// Token: 0x040053D3 RID: 21459
	private bool m_assetsLoaded;

	// Token: 0x040053D4 RID: 21460
	private Animator m_animator;

	// Token: 0x040053D5 RID: 21461
	private string m_entryAnimationTrigger;

	// Token: 0x040053D6 RID: 21462
	private string m_exitAnimationTrigger;

	// Token: 0x040053D7 RID: 21463
	private ModularBundleLayoutNodeDbfRecord m_nodeRecord;

	// Token: 0x040053D8 RID: 21464
	private ModularBundleNodeLayout m_parentLayout;

	// Token: 0x040053D9 RID: 21465
	private static readonly AssetReference DustJarAssetReference = new AssetReference("DustJar.prefab:2ae627c7666489a43ab8e0d7cd3c2b78");

	// Token: 0x040053DA RID: 21466
	private static readonly AssetReference ArenaTicketAssetReference = new AssetReference("ArenaTicket_Store.prefab:4d8c687ff2a4dc7469afd2139f4a1dc6");

	// Token: 0x0200229E RID: 8862
	private class NodeCallbackData
	{
		// Token: 0x0400E421 RID: 58401
		public ModularBundleNode requester;

		// Token: 0x0400E422 RID: 58402
		public object callbackData;
	}
}
