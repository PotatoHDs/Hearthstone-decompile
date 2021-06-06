using Assets;
using Blizzard.T5.Core;
using UnityEngine;

public class ModularBundleNode : MonoBehaviour
{
	private class NodeCallbackData
	{
		public ModularBundleNode requester;

		public object callbackData;
	}

	private bool m_assetsLoaded;

	private Animator m_animator;

	private string m_entryAnimationTrigger;

	private string m_exitAnimationTrigger;

	private ModularBundleLayoutNodeDbfRecord m_nodeRecord;

	private ModularBundleNodeLayout m_parentLayout;

	private static readonly AssetReference DustJarAssetReference = new AssetReference("DustJar.prefab:2ae627c7666489a43ab8e0d7cd3c2b78");

	private static readonly AssetReference ArenaTicketAssetReference = new AssetReference("ArenaTicket_Store.prefab:4d8c687ff2a4dc7469afd2139f4a1dc6");

	public float DelayBeforeEntryAnimation { get; private set; }

	public void Initialize(ModularBundleNodeLayout parent, ModularBundleLayoutNodeDbfRecord nodeRecord)
	{
		m_nodeRecord = nodeRecord;
		m_parentLayout = parent;
		base.gameObject.SetActive(value: false);
		DelayBeforeEntryAnimation = (float)nodeRecord.EntryDelay;
		m_entryAnimationTrigger = nodeRecord.EntryAnimation;
		m_exitAnimationTrigger = nodeRecord.ExitAnimation;
		LoadNodeAsset();
	}

	public bool IsReady()
	{
		return m_assetsLoaded;
	}

	public int GetNodeShakeWeight()
	{
		if (m_nodeRecord == null)
		{
			return 0;
		}
		return m_nodeRecord.ShakeWeight;
	}

	public void AttachLoadedPrefabObjectAsChild(GameObject loadedPrefab, bool withRotation = false)
	{
		if (!(loadedPrefab == null))
		{
			GameUtils.SetParent(loadedPrefab, this, withRotation);
			m_animator = loadedPrefab.GetComponent<Animator>();
			if (m_animator == null)
			{
				m_animator = loadedPrefab.AddComponent<Animator>();
			}
			ModularBundleSounds component = loadedPrefab.GetComponent<ModularBundleSounds>();
			if (component != null)
			{
				component.Initialize(m_nodeRecord.EntrySound, m_nodeRecord.LandingSound, m_nodeRecord.ExitSound);
			}
			loadedPrefab.transform.localRotation = base.transform.localRotation * loadedPrefab.transform.localRotation;
			loadedPrefab.transform.localScale = Vector3.Scale(base.transform.localScale, loadedPrefab.transform.localScale);
			base.transform.localRotation = Quaternion.identity;
			base.transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}

	public void PlayEntryAnimation()
	{
		base.gameObject.SetActive(value: true);
		if (!(m_animator == null) && !string.IsNullOrEmpty(m_entryAnimationTrigger))
		{
			m_animator.enabled = true;
			m_animator.SetTrigger(m_entryAnimationTrigger);
			m_animator.speed = (float)m_nodeRecord.AnimSpeedMultiplier;
		}
	}

	public void PlayExitAnimation()
	{
		if (!(m_animator == null) && !string.IsNullOrEmpty(m_exitAnimationTrigger))
		{
			m_animator.enabled = true;
			m_animator.SetTrigger(m_exitAnimationTrigger);
			m_animator.speed = (float)m_nodeRecord.AnimSpeedMultiplier;
		}
	}

	public void EnterImmediately()
	{
		base.gameObject.SetActive(value: true);
	}

	public void ExitImmediately()
	{
		base.gameObject.SetActive(value: false);
	}

	private void LoadNodeAsset()
	{
		m_assetsLoaded = false;
		switch (m_nodeRecord.DisplayType)
		{
		case ModularBundleLayoutNode.DisplayType.BOOSTER:
			LoadNodeAsset_Booster();
			break;
		case ModularBundleLayoutNode.DisplayType.TEXT:
			LoadNodeAsset_Text();
			break;
		case ModularBundleLayoutNode.DisplayType.DUST:
			LoadNodeAsset_Dust();
			break;
		case ModularBundleLayoutNode.DisplayType.PREFAB:
			LoadNodeAsset_Prefab();
			break;
		case ModularBundleLayoutNode.DisplayType.HERO_SKIN:
			LoadNodeAsset_HeroSkin();
			break;
		case ModularBundleLayoutNode.DisplayType.CARD_BACK:
			LoadNodeAsset_CardBack();
			break;
		case ModularBundleLayoutNode.DisplayType.ARENA_TICKET:
			LoadNodeAsset_ArenaTicket();
			break;
		default:
			Debug.LogWarningFormat("ModularBundleNode.LoadNodeAsset() - no load function for display type {0}!", m_nodeRecord.DisplayType);
			break;
		}
	}

	private void LoadNodeAsset_Booster()
	{
		GeneralStorePacksContent parent = m_parentLayout.GetDisplay().GetParent();
		if (parent == null)
		{
			Debug.LogError("ModularBundleNode.LoadNodeAsset_Booster() - no parent display!");
			return;
		}
		StorePackId storePackId = default(StorePackId);
		storePackId.Type = StorePackType.BOOSTER;
		storePackId.Id = m_nodeRecord.DisplayData;
		IStorePackDef storePackDef = parent.GetStorePackDef(storePackId);
		NodeCallbackData nodeCallbackData = new NodeCallbackData();
		nodeCallbackData.requester = this;
		nodeCallbackData.callbackData = storePackId;
		AssetLoader.Get().InstantiatePrefab(storePackDef.GetLowPolyPrefab(), OnNodeAssetLoaded_Booster, nodeCallbackData);
	}

	private static void OnNodeAssetLoaded_Booster(AssetReference assetRef, GameObject go, object callbackData)
	{
		NodeCallbackData nodeCallbackData = (NodeCallbackData)callbackData;
		if (nodeCallbackData == null || !GeneralUtils.IsObjectAlive(nodeCallbackData.requester))
		{
			Object.Destroy(go);
			return;
		}
		ModularBundleNode requester = nodeCallbackData.requester;
		StorePackId storePackId = (StorePackId)nodeCallbackData.callbackData;
		requester.m_assetsLoaded = true;
		AnimatedLowPolyPack component = go.GetComponent<AnimatedLowPolyPack>();
		if (component == null)
		{
			Log.All.PrintWarning("Modular Bundle Error: Layout prefab node expected to be a Pack node but loaded cardPackId={0} assetRef={1} does not have AnimatedLowPolyPack component script dbiNodeId={2} dbiNodeLayoutId={3} text={4} for gameObject in hierarchy:\n{5}", storePackId.Id, assetRef, requester.m_nodeRecord.ID, requester.m_nodeRecord.NodeLayoutId, requester.m_nodeRecord.DisplayText.GetString(), DebugUtils.GetHierarchyPath(requester));
			Error.AddDevWarning("Modular Bundle Error", string.Format("Layout node={0} expected to be a Pack node but loaded cardPackId={1} does not have AnimatedLowPolyPack component script; layout={3}. See the [All] log for more details.", requester.gameObject.name, storePackId.Id, (requester.m_parentLayout == null) ? "<null>" : requester.m_parentLayout.gameObject.name));
		}
		else
		{
			component.m_AmountBanner.SetActive(value: true);
			component.m_AmountBannerText.Text = requester.m_nodeRecord.DisplayCount.ToString();
		}
		SceneUtils.SetLayer(go, GameLayer.PerspectiveUI);
		go.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		requester.AttachLoadedPrefabObjectAsChild(go, withRotation: true);
	}

	private void LoadNodeAsset_Text()
	{
		m_assetsLoaded = true;
		m_animator = GetComponentInChildren<Animator>();
		ModularBundleText componentInChildren = GetComponentInChildren<ModularBundleText>();
		if (componentInChildren == null)
		{
			Log.All.PrintWarning("Modular Bundle Error: Layout prefab node expected to be a Text node but does not have ModularBundleText component script dbiNodeId={0} dbiNodeLayoutId={1} text={2} for gameObject in hierarchy:\n{3}", m_nodeRecord.ID, m_nodeRecord.NodeLayoutId, m_nodeRecord.DisplayText.GetString(), DebugUtils.GetHierarchyPath(this));
			Error.AddDevWarning("Modular Bundle Error", string.Format("Layout node={0} expected to be a Text node but does not have ModularBundleText component; layout={1}. See the [All] log for more details.", base.gameObject.name, (m_parentLayout == null) ? "<null>" : m_parentLayout.gameObject.name));
		}
		else
		{
			componentInChildren.Text.Text = m_nodeRecord.DisplayText;
			componentInChildren.SetGlowSize(m_nodeRecord.DisplayTextGlowSize);
		}
	}

	private void LoadNodeAsset_Dust()
	{
		NodeCallbackData nodeCallbackData = new NodeCallbackData();
		nodeCallbackData.requester = this;
		AssetLoader.Get().InstantiatePrefab(DustJarAssetReference, OnNodeAssetLoaded_Dust, nodeCallbackData);
	}

	private static void OnNodeAssetLoaded_Dust(AssetReference assetRef, GameObject go, object callbackData)
	{
		NodeCallbackData nodeCallbackData = (NodeCallbackData)callbackData;
		if (nodeCallbackData == null || !GeneralUtils.IsObjectAlive(nodeCallbackData.requester))
		{
			Object.Destroy(go);
			return;
		}
		ModularBundleNode requester = nodeCallbackData.requester;
		requester.m_assetsLoaded = true;
		SceneUtils.SetLayer(go, GameLayer.PerspectiveUI);
		requester.AttachLoadedPrefabObjectAsChild(go, withRotation: true);
		ModularBundleDustJar component = go.GetComponent<ModularBundleDustJar>();
		if (component == null)
		{
			Log.All.PrintWarning("Modular Bundle Error: Layout prefab node expected to be a DustJar node but loaded assetRef={0} does not have ModularBundleDustJar component script dbiNodeId={1} dbiNodeLayoutId={2} text={3} for gameObject in hierarchy:\n{4}", assetRef, requester.m_nodeRecord.ID, requester.m_nodeRecord.NodeLayoutId, requester.m_nodeRecord.DisplayText.GetString(), DebugUtils.GetHierarchyPath(requester));
			Error.AddDevWarning("Modular Bundle Error", string.Format("Layout node={0} expected to be a DustJar node but does not have ModularBundleDustJar component; layout={1}. See the [All] log for more details.", requester.gameObject.name, (requester.m_parentLayout == null) ? "<null>" : requester.m_parentLayout.gameObject.name));
		}
		else
		{
			component.AmountText.Text = requester.m_nodeRecord.DisplayCount.ToString();
			if (!string.IsNullOrEmpty(requester.m_nodeRecord.DisplayText))
			{
				component.HeaderText.Text.Text = requester.m_nodeRecord.DisplayText;
				component.HeaderText.SetGlowSize(requester.m_nodeRecord.DisplayTextGlowSize);
			}
			component.KeepHeaderTextStraight();
		}
	}

	private void LoadNodeAsset_ArenaTicket()
	{
		NodeCallbackData nodeCallbackData = new NodeCallbackData();
		nodeCallbackData.requester = this;
		AssetLoader.Get().InstantiatePrefab(ArenaTicketAssetReference, OnNodeAssetLoaded_ArenaTicket, nodeCallbackData, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private static void OnNodeAssetLoaded_ArenaTicket(AssetReference assetRef, GameObject go, object callbackData)
	{
		NodeCallbackData nodeCallbackData = (NodeCallbackData)callbackData;
		if (nodeCallbackData == null || !GeneralUtils.IsObjectAlive(nodeCallbackData.requester))
		{
			Object.Destroy(go);
			return;
		}
		ModularBundleNode requester = nodeCallbackData.requester;
		requester.m_assetsLoaded = true;
		SceneUtils.SetLayer(go, GameLayer.PerspectiveUI);
		requester.AttachLoadedPrefabObjectAsChild(go, withRotation: true);
		ModularBundleArenaTicket component = go.GetComponent<ModularBundleArenaTicket>();
		if (component == null)
		{
			Log.All.PrintWarning("Modular Bundle Error: Layout prefab node expected to be a ArenaTicket node but loaded assetRef={0} does not have ModularBundleArenaTicket component script dbiNodeId={1} dbiNodeLayoutId={2} text={3} for gameObject in hierarchy:\n{4}", assetRef, requester.m_nodeRecord.ID, requester.m_nodeRecord.NodeLayoutId, requester.m_nodeRecord.DisplayText.GetString(), DebugUtils.GetHierarchyPath(requester));
			Error.AddDevWarning("Modular Bundle Error", string.Format("Layout node={0} expected to be a ArenaTicket node but does not have ModularBundleArenaTicket component; layout={1}. See the [All] log for more details.", requester.gameObject.name, (requester.m_parentLayout == null) ? "<null>" : requester.gameObject.name));
		}
		else
		{
			component.AmountText.Text = requester.m_nodeRecord.DisplayCount.ToString();
			if (!string.IsNullOrEmpty(requester.m_nodeRecord.DisplayText))
			{
				component.HeaderText.Text = requester.m_nodeRecord.DisplayText;
			}
		}
	}

	private void LoadNodeAsset_Prefab()
	{
		string displayPrefab = m_nodeRecord.DisplayPrefab;
		NodeCallbackData nodeCallbackData = new NodeCallbackData();
		nodeCallbackData.requester = this;
		AssetLoader.Get().InstantiatePrefab(displayPrefab, OnNodeAssetLoaded_Prefab, nodeCallbackData);
	}

	private static void OnNodeAssetLoaded_Prefab(AssetReference assetRef, GameObject go, object callbackData)
	{
		NodeCallbackData nodeCallbackData = (NodeCallbackData)callbackData;
		if (nodeCallbackData == null || !GeneralUtils.IsObjectAlive(nodeCallbackData.requester))
		{
			Object.Destroy(go);
			return;
		}
		ModularBundleNode requester = nodeCallbackData.requester;
		requester.m_assetsLoaded = true;
		SceneUtils.SetLayer(go, GameLayer.PerspectiveUI);
		requester.AttachLoadedPrefabObjectAsChild(go, withRotation: true);
	}

	private void LoadNodeAsset_HeroSkin()
	{
		string text = "Modular_Bundle_Card_Hero_Skin.prefab:ad8fda5915cc96747abd0e15821c9857";
		NodeCallbackData nodeCallbackData = new NodeCallbackData();
		nodeCallbackData.requester = this;
		AssetLoader.Get().InstantiatePrefab(text, OnNodeAssetLoaded_HeroSkin, nodeCallbackData, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private static void OnNodeAssetLoaded_HeroSkin(AssetReference assetRef, GameObject go, object callbackData)
	{
		NodeCallbackData nodeCallbackData = (NodeCallbackData)callbackData;
		if (nodeCallbackData == null || !GeneralUtils.IsObjectAlive(nodeCallbackData.requester))
		{
			Object.Destroy(go);
			return;
		}
		ModularBundleNode requester = nodeCallbackData.requester;
		if (go == null)
		{
			Debug.LogWarningFormat("LoadNodeAsset_HeroSkin - FAILED to load \"{0}\"", assetRef);
			requester.m_assetsLoaded = true;
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			requester.m_assetsLoaded = true;
			Log.All.PrintWarning("Modular Bundle Error: Layout prefab node expected to be a HeroSkin node but loaded assetRef={0} does not have Actor component script dbiNodeId={1} dbiNodeLayoutId={2} text={3} for gameObject in hierarchy:\n{4}", assetRef, requester.m_nodeRecord.ID, requester.m_nodeRecord.NodeLayoutId, requester.m_nodeRecord.DisplayText.GetString(), DebugUtils.GetHierarchyPath(requester));
			Error.AddDevWarning("Modular Bundle Error", string.Format("Layout node={0} expected to be a HeroSkin node but does not have Actor component; layout={1}. See the [All] log for more details.", requester.gameObject.name, (requester.m_parentLayout == null) ? "<null>" : requester.gameObject.name));
		}
		else
		{
			string cardId = GameUtils.TranslateDbIdToCardId(requester.m_nodeRecord.DisplayData);
			NodeCallbackData nodeCallbackData2 = new NodeCallbackData();
			nodeCallbackData2.requester = requester;
			nodeCallbackData2.callbackData = component;
			DefLoader.Get().LoadFullDef(cardId, OnCardFullDefLoaded_HeroSkin, nodeCallbackData2);
		}
	}

	private static void OnCardFullDefLoaded_HeroSkin(string cardID, DefLoader.DisposableFullDef fullDef, object callbackData)
	{
		using (fullDef)
		{
			NodeCallbackData nodeCallbackData = (NodeCallbackData)callbackData;
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
				requester.AttachLoadedPrefabObjectAsChild(actor.gameObject, withRotation: true);
			}
		}
	}

	private void LoadNodeAsset_CardBack()
	{
		string actorName = "Modular_Bundle_Card_Back.prefab:939c318747e79d54f81ad2abab4584a2";
		NodeCallbackData nodeCallbackData = new NodeCallbackData();
		nodeCallbackData.requester = this;
		CardBackManager.Get().LoadCardBackByIndex(m_nodeRecord.DisplayData, OnNodeAssetLoaded_CardBack, actorName, nodeCallbackData);
	}

	private static void OnNodeAssetLoaded_CardBack(CardBackManager.LoadCardBackData cardBackData)
	{
		NodeCallbackData nodeCallbackData = (NodeCallbackData)cardBackData.callbackData;
		if (nodeCallbackData == null || !GeneralUtils.IsObjectAlive(nodeCallbackData.requester))
		{
			Object.Destroy(cardBackData.m_GameObject);
			return;
		}
		ModularBundleNode requester = nodeCallbackData.requester;
		requester.m_assetsLoaded = true;
		SceneUtils.SetLayer(cardBackData.m_GameObject, GameLayer.PerspectiveUI);
		requester.AttachLoadedPrefabObjectAsChild(cardBackData.m_GameObject, withRotation: true);
	}
}
