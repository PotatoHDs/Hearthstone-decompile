using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.AssetManager;
using Hearthstone;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public class GeneralStorePacksContent : GeneralStoreContent
{
	[Serializable]
	public class ToggleableButtonFrame
	{
		public GameObject m_Middle;

		public GameObject m_IBar;
	}

	[Serializable]
	public class MultiSliceEndCaps
	{
		public GameObject m_FullBar;

		public GameObject m_SmallerBar;
	}

	public enum LogoAnimation
	{
		None,
		Slam,
		Fade
	}

	public enum ModularBundleLayoutButtonSize
	{
		None,
		Small,
		Large
	}

	public StoreQuantityPrompt m_quantityPrompt;

	public GameObject m_packContainer;

	public GameObject m_packEmptyDisplay;

	public GeneralStorePacksContentDisplay m_packDisplay;

	[CustomEditField(Sections = "Pack Buy Buttons")]
	public GameObject m_packBuyContainer;

	[CustomEditField(Sections = "Pack Buy Buttons")]
	public MultiSliceElement m_packBuyButtonContainer;

	[CustomEditField(Sections = "Pack Buy Buttons")]
	public GeneralStorePackBuyButton m_packBuyButtonPrefab;

	[CustomEditField(Sections = "Pack Buy Buttons")]
	public MultiSliceElement m_packBuyFrameContainer;

	[CustomEditField(Sections = "Pack Buy Buttons", ListTable = true)]
	public List<ToggleableButtonFrame> m_toggleableButtonFrames = new List<ToggleableButtonFrame>();

	[CustomEditField(Sections = "Pack Buy Buttons", ListTable = true)]
	public List<MultiSliceEndCaps> m_buyBarEndCaps = new List<MultiSliceEndCaps>();

	[CustomEditField(Sections = "Pack Buy Buttons/Bonus Packs")]
	public GeneralStorePackBuyCallout m_packBuyBonusCallout;

	[CustomEditField(Sections = "Pack Buy Buttons/Bonus Packs")]
	public bool m_packBuyBonusCalloutOnlyOncePerSession;

	[CustomEditField(Sections = "Pack Buy Buttons/Bonus Packs")]
	public int m_packBuyBonusCalloutDebugForceDisplay;

	[CustomEditField(Sections = "Pack Buy Buttons/Bonus Packs")]
	public UberText m_packBuyBonusText;

	[CustomEditField(Sections = "Pack Buy Buttons/Limited Time Offer")]
	public UberText m_limitedTimeOfferText;

	[CustomEditField(Sections = "Pack Buy Buttons/Limited Time Offer")]
	public bool m_showLimitedTimeOfferText;

	[CustomEditField(Sections = "Pack Buy Buttons/Limited Time Offer")]
	public Transform m_limitedTimeOfferBone;

	[CustomEditField(Sections = "Pack Buy Buttons/Limited Time Offer")]
	public Transform m_limitedTimeOfferDustBone;

	[CustomEditField(Sections = "Pack Buy Buttons/Preorder")]
	public GameObject m_packBuyPreorderContainer;

	[CustomEditField(Sections = "Pack Buy Buttons/Preorder")]
	public GeneralStorePackBuyButton m_packBuyPreorderButtonPrefab;

	[CustomEditField(Sections = "Pack Buy Buttons/Preorder")]
	public MultiSliceElement m_packBuyPreorderButtonContainer;

	[CustomEditField(Sections = "Pack Buy Buttons/Preorder")]
	public MultiSliceElement m_packBuyPreorderFrameContainer;

	[CustomEditField(Sections = "Pack Buy Buttons/Preorder", ListTable = true)]
	public List<ToggleableButtonFrame> m_toggleablePreorderButtonFrames = new List<ToggleableButtonFrame>();

	[CustomEditField(Sections = "Pack Buy Buttons/Preorder")]
	public UberText m_availableDateText;

	[CustomEditField(Sections = "China Button")]
	public UIBButton m_ChinaInfoButton;

	[CustomEditField(Sections = "Packs")]
	public int m_maxPackBuyButtons = 10;

	[CustomEditField(Sections = "Packs")]
	public LogoAnimation m_logoAnimation;

	[CustomEditField(Sections = "Animation")]
	public float m_packFlyOutAnimTime = 0.1f;

	[CustomEditField(Sections = "Animation")]
	public float m_packFlyOutDelay = 0.005f;

	[CustomEditField(Sections = "Animation")]
	public float m_packFlyInAnimTime = 0.2f;

	[CustomEditField(Sections = "Animation")]
	public float m_packFlyInDelay = 0.01f;

	[CustomEditField(Sections = "Animation")]
	public float m_boxFlyOutAnimTime = 0.2f;

	[CustomEditField(Sections = "Animation")]
	public float m_boxFlyOutDelay = 0.005f;

	[CustomEditField(Sections = "Animation")]
	public float m_boxFlyInAnimTime = 0.5f;

	[CustomEditField(Sections = "Animation")]
	public float m_boxFlyInDelay = 0.1f;

	[CustomEditField(Sections = "Animation")]
	public float m_boxFlyInXShake = 35f;

	[CustomEditField(Sections = "Animation")]
	public float m_boxStoreImpactTranslation = -70f;

	[CustomEditField(Sections = "Animation")]
	public float m_shakeObjectDelayMultiplier = 0.7f;

	[CustomEditField(Sections = "Animation")]
	public float m_backgroundFlipAnimTime = 0.5f;

	[CustomEditField(Sections = "Animation")]
	public float m_maxPackFlyInXShake = 20f;

	[CustomEditField(Sections = "Animation")]
	public float m_maxPackFlyOutXShake = 12f;

	[CustomEditField(Sections = "Animation")]
	public float m_packFlyShakeTime = 2f;

	[CustomEditField(Sections = "Animation")]
	public float m_backgroundFlipShake = 20f;

	[CustomEditField(Sections = "Animation")]
	public float m_backgroundFlipShakeDelay;

	[CustomEditField(Sections = "Animation")]
	public float m_PackYDegreeVariationMag = 2f;

	[CustomEditField(Sections = "Animation")]
	public float m_BoxYDegreeVariationMag = 1f;

	[CustomEditField(Sections = "Animation/Appear")]
	public GameObject m_logoAnimationStartBone;

	[CustomEditField(Sections = "Animation/Appear")]
	public GameObject m_logoAnimationEndBone;

	[CustomEditField(Sections = "Animation/Appear")]
	public MeshRenderer m_logoMesh;

	[CustomEditField(Sections = "Animation/Appear")]
	public MeshRenderer m_logoGlowMesh;

	[CustomEditField(Sections = "Animation/Appear")]
	public Vector3 m_punchAmount;

	[CustomEditField(Sections = "Animation/Appear")]
	public float m_logoHoldTime = 1f;

	[CustomEditField(Sections = "Animation/Appear")]
	public float m_logoDisplayPunchTime = 0.5f;

	[CustomEditField(Sections = "Animation/Appear")]
	public float m_logoIntroTime = 0.25f;

	[CustomEditField(Sections = "Animation/Appear")]
	public float m_logoOutroTime = 0.25f;

	[CustomEditField(Sections = "Animation/Appear")]
	public Vector3 m_logoAppearOffset;

	[CustomEditField(Sections = "Animation/Preorder")]
	public GeneralStoreRewardsCardBack m_preorderCardBackReward;

	[CustomEditField(Sections = "Sounds & Music", T = EditType.SOUND_PREFAB)]
	public string m_backgroundFlipSound;

	public const bool REQUIRE_REAL_MONEY_BUNDLE_OPTION = true;

	private static readonly int MAX_QUANTITY_BOUGHT_WITH_GOLD = 50;

	private const float FIRST_PURCHASE_BUNDLE_INIT_DELAY = 0.5f;

	private StorePackId m_selectedStorePackId;

	private List<GeneralStorePackBuyButton> m_packBuyButtons = new List<GeneralStorePackBuyButton>();

	private List<GeneralStorePackBuyButton> m_packPreorderBuyButtons = new List<GeneralStorePackBuyButton>();

	private int m_currentGoldPackQuantity = 1;

	private int m_visiblePackCount;

	private int m_visibleDustCount;

	private int m_visibleDustBonusCount;

	private bool m_selectedBoosterIsPrePurchase;

	private int m_lastBundleIndex;

	private int m_currentDisplay = -1;

	private Map<StorePackId, IStorePackDef> m_storePackDefs = new Map<StorePackId, IStorePackDef>();

	private HashSet<StorePackId> m_packBuyBonusCalloutSeenForPackId = new HashSet<StorePackId>();

	private const string PREV_PLAYLIST_NAME = "StorePrevCurrentPlaylist";

	private GeneralStorePacksContentDisplay m_packDisplay1;

	private GeneralStorePacksContentDisplay m_packDisplay2;

	private MeshRenderer m_logoMesh1;

	private MeshRenderer m_logoMesh2;

	private MeshRenderer m_logoGlowMesh1;

	private MeshRenderer m_logoGlowMesh2;

	private Coroutine m_logoAnimCoroutine;

	private Coroutine m_packAnimCoroutine;

	private Coroutine m_limitedTimeOfferAnimCoroutine;

	private Coroutine m_bonusPacksCalloutCoroutine;

	private Vector3 m_savedLocalPosition;

	private Vector3 m_limitedTimeTextOrigScale;

	private bool m_animatingLogo;

	private bool m_animatingPacks;

	private bool m_hasLogo;

	private bool m_waitingForBoxAnim;

	private bool m_loadingLogoTexture;

	private bool m_loadingLogoGlowTexture;

	public override void PostStoreFlipIn(bool animatedFlipIn)
	{
		UpdatePacksTypeMusic();
		AnimateLogo(animatedFlipIn);
		if (GameUtils.IsHiddenLicenseBundleBooster(m_selectedStorePackId))
		{
			int firstValidBundleIndex = GetFirstValidBundleIndex(m_selectedStorePackId);
			HandleMoneyPackBuyButtonClick(firstValidBundleIndex);
			Network.Bundle currentMoneyBundle = GetCurrentMoneyBundle();
			if (StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle))
			{
				AnimatePacksFlying(m_visiblePackCount, forceImmediate: true, 0f, showAsSingleStack: true);
				StartCoroutine(ShowFeaturedDustJar());
				if (GameUtils.IsFirstPurchaseBundleBooster(m_selectedStorePackId))
				{
					ShowHiddenBundleCard();
				}
			}
			else
			{
				float delay = 0f;
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					delay = 1f;
				}
				AnimatePacksFlying(m_visiblePackCount, forceImmediate: false, delay);
			}
		}
		else
		{
			AnimatePacksFlying(m_visiblePackCount, !animatedFlipIn);
			HideDust();
		}
		UpdateKoreaInfoButton();
		m_savedLocalPosition = base.gameObject.transform.localPosition;
	}

	public override void PreStoreFlipOut()
	{
		ResetAnimations();
		GetCurrentDisplay().ClearContents();
		UpdateKoreaInfoButton();
	}

	public override void StoreShown(bool isCurrent)
	{
		if (!isCurrent)
		{
			return;
		}
		AnimateLogo(animateLogo: false);
		if (GameUtils.IsHiddenLicenseBundleBooster(m_selectedStorePackId))
		{
			int firstValidBundleIndex = GetFirstValidBundleIndex(m_selectedStorePackId);
			HandleMoneyPackBuyButtonClick(firstValidBundleIndex);
			Network.Bundle currentMoneyBundle = GetCurrentMoneyBundle();
			if (StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle))
			{
				AnimatePacksFlying(m_visiblePackCount, forceImmediate: true, 0f, showAsSingleStack: true);
				StartCoroutine(ShowFeaturedDustJar());
				if (GameUtils.IsFirstPurchaseBundleBooster(m_selectedStorePackId))
				{
					ShowHiddenBundleCard();
				}
			}
			else
			{
				AnimatePacksFlying(m_visiblePackCount, forceImmediate: true);
			}
		}
		else
		{
			Network.Bundle currentMoneyBundle2 = GetCurrentMoneyBundle();
			bool flag = false;
			if (currentMoneyBundle2 != null)
			{
				flag = StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle2);
			}
			if (flag)
			{
				AnimatePacksFlying(m_visiblePackCount, forceImmediate: true, 0f, showAsSingleStack: true, UniversalInputManager.UsePhoneUI);
			}
			else
			{
				AnimatePacksFlying(m_visiblePackCount, forceImmediate: true);
				HideDust();
			}
		}
		UpdatePackBuyButtons();
		UpdatePacksTypeMusic();
		UpdateKoreaInfoButton();
	}

	public override void StoreHidden(bool isCurrent)
	{
		if (isCurrent)
		{
			ResetAnimations();
			GetCurrentDisplay().ClearContents();
		}
	}

	public override bool IsPurchaseDisabled()
	{
		return IsPackIdInvalid(m_selectedStorePackId);
	}

	public override string GetMoneyDisplayOwnedText()
	{
		return GameStrings.Get("GLUE_STORE_PACK_BUTTON_COST_OWNED_TEXT");
	}

	public void SetBoosterId(StorePackId storePackId, bool forceImmediate = false, bool InitialSelection = false)
	{
		if (m_selectedStorePackId == storePackId)
		{
			return;
		}
		bool num = IsPackIdInvalid(m_selectedStorePackId);
		StoreManager.Get().SetCurrentlySelectedStorePack(storePackId);
		GetCurrentDisplay().ClearContents();
		m_visiblePackCount = 0;
		m_visibleDustCount = 0;
		m_selectedStorePackId = storePackId;
		if (num)
		{
			UpdateSelectedBundle();
		}
		ResetAnimations();
		AnimateAndUpdateDisplay(storePackId, forceImmediate);
		if (InitialSelection)
		{
			GetCurrentLogo().gameObject.SetActive(value: false);
		}
		AnimateLogo(!forceImmediate, InitialSelection);
		bool flag = false;
		if (GameUtils.IsHiddenLicenseBundleBooster(m_selectedStorePackId))
		{
			int firstValidBundleIndex = GetFirstValidBundleIndex(m_selectedStorePackId);
			HandleMoneyPackBuyButtonClick(firstValidBundleIndex);
			Network.Bundle currentMoneyBundle = GetCurrentMoneyBundle();
			flag = StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle);
			m_selectedBoosterIsPrePurchase = false;
		}
		else if (GetCurrentGoldBundle() != null)
		{
			SetCurrentGoldBundle(GetCurrentGTAPPTransactionData());
		}
		else if (GetCurrentMoneyBundle() != null)
		{
			HandleMoneyPackBuyButtonClick(m_lastBundleIndex);
			Network.Bundle currentMoneyBundle2 = GetCurrentMoneyBundle();
			flag = StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle2);
			m_selectedBoosterIsPrePurchase = StoreManager.Get().IsProductPrePurchase(currentMoneyBundle2);
		}
		Log.Store.Print("InitialSelection = {0}", InitialSelection);
		if (GameUtils.IsHiddenLicenseBundleBooster(m_selectedStorePackId))
		{
			float num2 = (InitialSelection ? 0.5f : 0f);
			Log.Store.Print("InitialSelection delay={0}", num2);
			if (flag)
			{
				AnimatePacksFlying(m_visiblePackCount, forceImmediate: true, 0f, showAsSingleStack: true);
				StartCoroutine(ShowFeaturedDustJar());
				if (GameUtils.IsFirstPurchaseBundleBooster(m_selectedStorePackId))
				{
					ShowHiddenBundleCard();
				}
			}
			else
			{
				AnimatePacksFlying(m_visiblePackCount, forceImmediate, num2);
			}
		}
		else if (flag)
		{
			bool waitForLogo = (UniversalInputManager.UsePhoneUI ? true : false);
			AnimatePacksFlying(m_visiblePackCount, forceImmediate: true, 0f, !m_selectedBoosterIsPrePurchase, waitForLogo);
			StartCoroutine(ShowFeaturedDustJar(waitForLogo));
		}
		else
		{
			AnimatePacksFlying(m_visiblePackCount, forceImmediate);
			HideDust();
		}
		UpdatePackBuyButtons();
		UpdatePacksDescriptionFromSelectedStorePack();
		UpdatePacksTypeMusic();
		UpdateKoreaInfoButton();
	}

	public StorePackId GetStorePackId()
	{
		return m_selectedStorePackId;
	}

	private int GetFirstValidBundleIndex(StorePackId storePackId)
	{
		int productDataCountFromStorePackId = GameUtils.GetProductDataCountFromStorePackId(storePackId);
		for (int i = 0; i < productDataCountFromStorePackId; i++)
		{
			int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(storePackId, i);
			if (StoreManager.Get().EnumerateBundlesForProductType(ProductType.PRODUCT_TYPE_HIDDEN_LICENSE, requireRealMoneyOption: true, productDataFromStorePackId).Any())
			{
				return i;
			}
		}
		return 0;
	}

	public int GetLastBundleIndex()
	{
		return m_lastBundleIndex;
	}

	public bool SelectedBundleFeaturesDustJar()
	{
		Network.Bundle currentMoneyBundle = GetCurrentMoneyBundle();
		return StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle);
	}

	public void FirstPurchaseBundlePurchased(CardReward cardReward)
	{
		GeneralStorePacksContentDisplay currentDisplay = GetCurrentDisplay();
		if (currentDisplay == null)
		{
			CardRewardData cardRewardData = cardReward.Data as CardRewardData;
			Debug.LogWarningFormat("FirstPurchaseBundlePurchased() failed to get GeneralStorePacksContentDisplay for cardID {0}", cardRewardData.CardID);
		}
		else
		{
			currentDisplay.PurchaseBundleBox(cardReward);
		}
	}

	public Map<StorePackId, IStorePackDef> GetStorePackDefs()
	{
		return m_storePackDefs;
	}

	public IStorePackDef GetStorePackDef(StorePackId packId)
	{
		IStorePackDef value = null;
		m_storePackDefs.TryGetValue(packId, out value);
		return value;
	}

	public void ShakeStore(int numPacks, float maxXRotation, float delay = 0f, float translationAmount = 0f, int weight = 0)
	{
		if (numPacks == 0)
		{
			return;
		}
		int num = 1;
		float xRotationAmount = 0f;
		List<Network.Bundle> packBundles = GetPackBundles(sortByPackQuantity: false);
		if (m_selectedStorePackId.Type == StorePackType.BOOSTER)
		{
			foreach (Network.Bundle item in packBundles)
			{
				Network.BundleItem packsBundleItemFromBundle = GetPacksBundleItemFromBundle(item);
				if (packsBundleItemFromBundle != null)
				{
					num = Mathf.Max(packsBundleItemFromBundle.Quantity, num);
					int num2 = num - 1;
					if (num2 == 0)
					{
						return;
					}
					xRotationAmount = (float)numPacks / (float)num2 * maxXRotation;
				}
			}
		}
		else if (m_selectedStorePackId.Type == StorePackType.MODULAR_BUNDLE)
		{
			int num3 = 100;
			if (weight > num3)
			{
				weight = num3;
			}
			xRotationAmount = maxXRotation * (float)weight / (float)num3;
		}
		float translateAmount = 0f;
		if (GameUtils.IsHiddenLicenseBundleBooster(m_selectedStorePackId))
		{
			translateAmount = translationAmount;
		}
		m_parentStore.ShakeStore(xRotationAmount, m_packFlyShakeTime, delay, translateAmount);
	}

	public void StartAnimatingPacks()
	{
		m_animatingPacks = true;
	}

	public void DoneAnimatingPacks()
	{
		m_animatingPacks = false;
	}

	protected override void OnBundleChanged(NoGTAPPTransactionData goldBundle, Network.Bundle moneyBundle)
	{
		if (IsPackIdFirstPurchaseBundle(m_selectedStorePackId) && moneyBundle == null)
		{
			int firstValidBundleIndex = GetFirstValidBundleIndex(m_selectedStorePackId);
			HandleMoneyPackBuyButtonClick(firstValidBundleIndex);
		}
		else
		{
			if (m_selectedStorePackId.Type == StorePackType.BOOSTER && GameUtils.IsHiddenLicenseBundleBooster(m_selectedStorePackId) && !StoreManager.Get().ShouldShowFeaturedDustJar(moneyBundle))
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			if (goldBundle != null)
			{
				m_visiblePackCount = goldBundle.Quantity;
				m_visibleDustCount = 0;
				m_selectedBoosterIsPrePurchase = false;
			}
			else if (moneyBundle != null)
			{
				m_visiblePackCount = StoreManager.Get().PackQuantityInBundle(moneyBundle);
				int num = StoreManager.Get().DustQuantityInBundle(moneyBundle);
				int num2 = StoreManager.Get().DustBaseQuantityInBundle(moneyBundle);
				if (num2 > 0)
				{
					m_visibleDustCount = num2;
					m_visibleDustBonusCount = Math.Max(num - num2, 0);
				}
				else
				{
					m_visibleDustCount = num;
					m_visibleDustBonusCount = 0;
				}
				flag = ((m_visibleDustCount > 0) ? true : false);
				flag2 = StoreManager.Get().ShouldShowFeaturedDustJar(moneyBundle);
				m_selectedBoosterIsPrePurchase = StoreManager.Get().IsProductPrePurchase(moneyBundle);
			}
			if (flag && flag2)
			{
				bool waitForLogo = (UniversalInputManager.UsePhoneUI ? true : false);
				AnimatePacksFlying(m_visiblePackCount, forceImmediate: true, 0f, !m_selectedBoosterIsPrePurchase, waitForLogo);
				StartCoroutine(ShowFeaturedDustJar(waitForLogo));
			}
			else
			{
				AnimatePacksFlying(m_visiblePackCount);
				HideDust();
				HideHiddenBundleCard();
			}
		}
	}

	protected override void OnRefresh()
	{
		UpdatePackBuyButtons();
		UpdatePacksDescriptionFromSelectedStorePack();
		if (!HasBundleSet() && !IsPackIdInvalid(m_selectedStorePackId))
		{
			UpdateSelectedBundle(forceUpdate: true);
		}
	}

	private void Awake()
	{
		m_packDisplay1 = m_packDisplay;
		m_packDisplay2 = UnityEngine.Object.Instantiate(m_packDisplay);
		m_packDisplay2.transform.parent = m_packDisplay1.transform.parent;
		m_packDisplay2.transform.localPosition = m_packDisplay1.transform.localPosition;
		m_packDisplay2.transform.localScale = m_packDisplay1.transform.localScale;
		m_packDisplay2.transform.localRotation = m_packDisplay1.transform.localRotation;
		m_packDisplay2.gameObject.SetActive(value: false);
		m_logoMesh1 = m_logoMesh;
		m_logoMesh2 = UnityEngine.Object.Instantiate(m_logoMesh);
		m_logoMesh2.transform.parent = m_logoMesh1.transform.parent;
		m_logoMesh2.transform.localPosition = m_logoMesh1.transform.localPosition;
		m_logoMesh2.transform.localScale = m_logoMesh1.transform.localScale;
		m_logoMesh2.transform.localRotation = m_logoMesh1.transform.localRotation;
		m_logoMesh2.gameObject.SetActive(value: false);
		m_logoGlowMesh1 = m_logoGlowMesh;
		m_logoGlowMesh2 = m_logoMesh2.transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
		m_packDisplay1.SetParent(this);
		m_packDisplay2.SetParent(this);
		m_packBuyContainer.SetActive(value: false);
		if (m_limitedTimeOfferText != null)
		{
			m_limitedTimeTextOrigScale = m_limitedTimeOfferText.transform.localScale;
		}
		if (m_packBuyBonusCallout != null)
		{
			m_packBuyBonusCallout.Init();
		}
		if (m_packBuyBonusText != null)
		{
			m_packBuyBonusText.gameObject.SetActive(value: false);
		}
		if (m_ChinaInfoButton != null)
		{
			m_ChinaInfoButton.AddEventListener(UIEventType.RELEASE, OnKoreaInfoPressed);
		}
		StorePackId storePackId;
		foreach (BoosterDbfRecord item in GameUtils.GetPackRecordsWithStorePrefab())
		{
			int iD = item.ID;
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(item.StorePrefab, AssetLoadingOptions.IgnorePrefabPosition);
			if (gameObject == null)
			{
				Debug.LogError($"Unable to load store pack def: {item.StorePrefab}");
				continue;
			}
			IStorePackDef component = gameObject.GetComponent<StorePackDef>();
			if (component == null)
			{
				Debug.LogError($"StorePackDef component not found: {item.StorePrefab}");
				continue;
			}
			storePackId = default(StorePackId);
			storePackId.Type = StorePackType.BOOSTER;
			storePackId.Id = iD;
			StorePackId key = storePackId;
			m_storePackDefs.Add(key, component);
		}
		foreach (ModularBundleDbfRecord record in GameDbf.ModularBundle.GetRecords())
		{
			ModularBundleStorePackDef value = new ModularBundleStorePackDef(record);
			storePackId = default(StorePackId);
			storePackId.Type = StorePackType.MODULAR_BUNDLE;
			storePackId.Id = record.ID;
			StorePackId key2 = storePackId;
			m_storePackDefs.Add(key2, value);
		}
		UpdateKoreaInfoButton();
	}

	private GameObject GetCurrentDisplayContainer()
	{
		return GetCurrentDisplay().gameObject;
	}

	private GameObject GetNextDisplayContainer()
	{
		if ((m_currentDisplay + 1) % 2 != 0)
		{
			return m_packDisplay2.gameObject;
		}
		return m_packDisplay1.gameObject;
	}

	private GeneralStorePacksContentDisplay GetCurrentDisplay()
	{
		if (m_currentDisplay != 0)
		{
			return m_packDisplay2;
		}
		return m_packDisplay1;
	}

	private MeshRenderer GetCurrentLogo()
	{
		if (m_currentDisplay != 0)
		{
			return m_logoMesh2;
		}
		return m_logoMesh1;
	}

	private MeshRenderer GetCurrentGlowLogo()
	{
		if (m_currentDisplay != 0)
		{
			return m_logoGlowMesh2;
		}
		return m_logoGlowMesh1;
	}

	private void UpdateSelectedBundle(bool forceUpdate = false)
	{
		ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(m_selectedStorePackId);
		int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(m_selectedStorePackId, m_lastBundleIndex);
		NoGTAPPTransactionData noGTAPPTransactionData = new NoGTAPPTransactionData
		{
			Product = productTypeFromStorePackType,
			ProductData = productDataFromStorePackId,
			Quantity = 1
		};
		if (StoreManager.Get().GetGoldCostNoGTAPP(noGTAPPTransactionData, out var _))
		{
			SetCurrentGoldBundle(noGTAPPTransactionData);
			return;
		}
		Network.Bundle lowestCostBundle = StoreManager.Get().GetLowestCostBundle(productTypeFromStorePackType, requireRealMoneyOption: false, productDataFromStorePackId);
		if (lowestCostBundle != null)
		{
			SetCurrentMoneyBundle(lowestCostBundle, forceUpdate);
		}
	}

	private void UpdatePacksDescriptionFromSelectedStorePack()
	{
		if (IsPackIdInvalid(m_selectedStorePackId))
		{
			m_parentStore.HideAccentTexture();
			m_parentStore.SetChooseDescription(GameStrings.Get("GLUE_STORE_CHOOSE_PACK"));
		}
		else if (m_selectedStorePackId.Type == StorePackType.BOOSTER)
		{
			UpdatePacksDescriptionForBooster();
		}
		else if (m_selectedStorePackId.Type == StorePackType.MODULAR_BUNDLE)
		{
			UpdatePacksDescriptionForModularBundle();
		}
	}

	private void UpdatePacksDescriptionForBooster()
	{
		BoosterDbfRecord record = GameDbf.Booster.GetRecord(m_selectedStorePackId.Id);
		string text = record.Name;
		string packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_PACK");
		string packDescription = GameStrings.Format("GLUE_STORE_PRODUCT_DETAILS_PACK", text);
		Network.Bundle currentMoneyBundle = GetCurrentMoneyBundle();
		bool flag = false;
		if (currentMoneyBundle != null)
		{
			flag = StoreManager.Get().IsProductPrePurchase(currentMoneyBundle);
			bool flag2 = GameUtils.IsFirstPurchaseBundleBooster(m_selectedStorePackId);
			bool flag3 = StoreManager.Get().ShouldShowFeaturedDustJar(currentMoneyBundle);
			if (!flag && !flag2 && flag3)
			{
				packDescription = GameStrings.Format("GLUE_STORE_PRODUCT_DETAILS_DUST", text);
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_DUST");
			}
			if (flag && 10 == record.ID)
			{
				packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_TGT_PACK_PRESALE");
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_TGT_PACK_PRESALE");
			}
			if (flag && 11 == record.ID)
			{
				packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_OG_PACK_PRESALE");
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_OG_PACK_PRESALE");
			}
			if (flag && 20 == record.ID)
			{
				packDescription = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_GORO_PACK_PRESALE");
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_GORO_PACK_PRESALE");
			}
			if (GameUtils.IsHiddenLicenseBundleBooster(m_selectedStorePackId))
			{
				if (flag2)
				{
					packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_FIRST_PURCHASE_BUNDLE");
					packDescription = ((!flag3) ? GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_FIRST_PURCHASE_BUNDLE") : GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_FIRST_PURCHASE_BUNDLE_DUST"));
				}
				else if (GameUtils.IsMammothBundleBooster(m_selectedStorePackId))
				{
					packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_MAMMOTH_BUNDLE");
					packDescription = ((!flag3) ? GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_MAMMOTH_BUNDLE") : GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_MAMMOTH_BUNDLE_DUST"));
				}
			}
			if (flag && 21 == record.ID)
			{
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_ICC_PACK_PRESALE");
				packDescription = ((!flag3) ? GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_ICC_PACK_PRESALE") : GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_ICC_CN_DUST_PRESALE"));
			}
			if (flag && 30 == record.ID)
			{
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_LOOT_PACK_PRESALE");
				packDescription = ((!flag3) ? GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_LOOT_PACK_PRESALE") : GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_LOOT_CN_DUST_PRESALE"));
			}
			if (flag && 31 == record.ID)
			{
				packDescriptionHeadline = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_GIL_PACK_PRESALE");
				packDescription = ((!flag3) ? GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_GIL_PACK_PRESALE") : GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_GIL_CN_DUST_PRESALE"));
			}
		}
		string accentTextureName = "";
		IStorePackDef storePackDef = GetStorePackDef(m_selectedStorePackId);
		if (storePackDef != null)
		{
			accentTextureName = storePackDef.GetAccentTextureName();
		}
		UpdatePacksDescription(packDescriptionHeadline, packDescription, accentTextureName, flag);
	}

	private void UpdatePacksDescriptionForModularBundle()
	{
		int id = m_selectedStorePackId.Id;
		List<ModularBundleLayoutDbfRecord> regionNodeLayoutsForBundle = StoreManager.Get().GetRegionNodeLayoutsForBundle(id);
		if (m_lastBundleIndex >= regionNodeLayoutsForBundle.Count)
		{
			Log.Store.PrintWarning($"Selected invalid layout at index={m_lastBundleIndex}. Defaulting to layout at index=0.");
			m_lastBundleIndex = 0;
		}
		ModularBundleLayoutDbfRecord modularBundleLayoutDbfRecord = regionNodeLayoutsForBundle[m_lastBundleIndex];
		Network.Bundle currentMoneyBundle = GetCurrentMoneyBundle();
		bool isPreorder = StoreManager.Get().IsProductPrePurchase(currentMoneyBundle);
		UpdatePacksDescription(modularBundleLayoutDbfRecord.DescriptionHeadline, modularBundleLayoutDbfRecord.Description, modularBundleLayoutDbfRecord.AccentTexture, isPreorder);
	}

	private void UpdatePacksDescription(string packDescriptionHeadline, string packDescription, string accentTextureName, bool isPreorder)
	{
		string warning = string.Empty;
		if (StoreManager.Get().IsKoreanCustomer())
		{
			warning = (isPreorder ? GameStrings.Get("GLUE_STORE_KOREAN_PRODUCT_DETAILS_PACKS_PREORDER") : (GameUtils.IsFirstPurchaseBundleBooster(m_selectedStorePackId) ? GameStrings.Get("GLUE_STORE_KOREAN_PRODUCT_DETAILS_FIRST_PURCHASE_BUNDLE") : ((GetCurrentMoneyBundle() == null || !AdventureUtils.IsAdventureBundle(GetCurrentMoneyBundle())) ? GameStrings.Get("GLUE_STORE_KOREAN_PRODUCT_DETAILS_EXPERT_PACK") : GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_ADVENTURE_BUNDLE"))));
		}
		m_parentStore.SetDescription(packDescriptionHeadline, packDescription, warning);
		using AssetHandle<Texture> accentTexture = AssetLoader.Get().LoadAsset<Texture>(accentTextureName);
		m_parentStore.SetAccentTexture(accentTexture);
	}

	private NoGTAPPTransactionData GetCurrentGTAPPTransactionData()
	{
		ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(m_selectedStorePackId);
		return new NoGTAPPTransactionData
		{
			Product = productTypeFromStorePackType,
			ProductData = m_selectedStorePackId.Id,
			Quantity = m_currentGoldPackQuantity
		};
	}

	private void UpdatePackBuyButtons()
	{
		if (!IsPackIdInvalid(m_selectedStorePackId))
		{
			if (StoreManager.Get().IsBoosterHiddenLicenseBundle(m_selectedStorePackId, out var hiddenLicenseBundle) && m_selectedStorePackId.Type != StorePackType.MODULAR_BUNDLE)
			{
				ShowHiddenLicenseBundleBuyButtons(hiddenLicenseBundle);
			}
			else if (m_selectedStorePackId.Type == StorePackType.MODULAR_BUNDLE)
			{
				ShowModularBundleBuyButtons();
			}
			else
			{
				ShowStandardBuyButtons();
			}
		}
	}

	private static Network.BundleItem GetPacksBundleItemFromBundle(Network.Bundle bundle)
	{
		return bundle?.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_BOOSTER);
	}

	private void ShowStandardBuyButtons()
	{
		m_packBuyPreorderContainer.SetActive(value: false);
		m_packBuyContainer.SetActive(value: true);
		Action action = null;
		ClearButtonEventListeners();
		int num = 0;
		GeneralStorePackBuyButton goldButton = GetPackBuyButton(num);
		if (goldButton == null)
		{
			goldButton = CreatePackBuyButton(num);
		}
		goldButton.AddEventListener(UIEventType.PRESS, delegate
		{
			if (IsContentActive())
			{
				HandleGoldPackBuyButtonClick();
				SelectPackBuyButton(goldButton);
			}
		});
		if (!UniversalInputManager.UsePhoneUI)
		{
			goldButton.AddEventListener(UIEventType.DOUBLECLICK, delegate
			{
				HandleGoldPackBuyButtonDoubleClick(goldButton);
			});
		}
		if (!IsPackIdInvalid(m_selectedStorePackId))
		{
			goldButton.UpdateFromGTAPP(GetCurrentGTAPPTransactionData());
		}
		action = delegate
		{
			HandleGoldPackBuyButtonClick();
			SelectPackBuyButton(goldButton);
		};
		goldButton.Unselect();
		List<Network.Bundle> list = GetPackBundles(sortByPackQuantity: true);
		if (list.Count > m_maxPackBuyButtons - 1)
		{
			list = list.GetRange(0, m_maxPackBuyButtons - 1);
		}
		bool flag = false;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < list.Count; i++)
		{
			num++;
			int bundleIndexCopy = i;
			Network.Bundle bundle = list[i];
			Network.BundleItem bundleItem = bundle.Items.Find((Network.BundleItem obj) => obj.ItemType == ProductType.PRODUCT_TYPE_BOOSTER);
			if (bundleItem == null)
			{
				Debug.LogWarning($"GeneralStorePacksContent.UpdatePackBuyButtons() bundle {bundle.PMTProductID} has no packs bundle item!");
				continue;
			}
			GeneralStorePackBuyButton moneyButton = GetPackBuyButton(num);
			if (moneyButton == null)
			{
				moneyButton = CreatePackBuyButton(num);
			}
			moneyButton.AddEventListener(UIEventType.PRESS, delegate
			{
				if (IsContentActive())
				{
					HandleMoneyPackBuyButtonClick(bundleIndexCopy);
					SelectPackBuyButton(moneyButton);
				}
			});
			string packBuyButtonText = GetPackBuyButtonText(bundle, bundleItem);
			if (bundleItem.BaseQuantity > 0)
			{
				if (!flag)
				{
					flag = true;
					num2 = num;
				}
				num3 = num;
			}
			moneyButton.SetMoneyValue(bundle, bundleItem, packBuyButtonText);
			moneyButton.gameObject.SetActive(value: true);
			if (moneyButton.IsSelected() || GetCurrentMoneyBundle() == bundle)
			{
				action = delegate
				{
					HandleMoneyPackBuyButtonClick(bundleIndexCopy);
					SelectPackBuyButton(moneyButton);
				};
			}
			moneyButton.Unselect();
		}
		bool active = StoreManager.Get().CanBuyStorePackWithGold(m_selectedStorePackId);
		goldButton.gameObject.SetActive(active);
		for (int j = num + 1; j < m_packBuyButtons.Count; j++)
		{
			GeneralStorePackBuyButton generalStorePackBuyButton = m_packBuyButtons[j];
			if (generalStorePackBuyButton != null)
			{
				generalStorePackBuyButton.gameObject.SetActive(value: false);
			}
		}
		int num4 = num + 1;
		UpdateToggleableSections(m_toggleableButtonFrames, num4);
		bool flag2 = num4 >= m_toggleableButtonFrames.Count;
		foreach (MultiSliceEndCaps buyBarEndCap in m_buyBarEndCaps)
		{
			buyBarEndCap.m_FullBar.SetActive(flag2);
			buyBarEndCap.m_SmallerBar.SetActive(!flag2);
		}
		if (m_packBuyFrameContainer != null)
		{
			m_packBuyFrameContainer.UpdateSlices();
		}
		m_packBuyButtonContainer.UpdateSlices();
		action?.Invoke();
		if (!(m_packBuyBonusCallout != null))
		{
			return;
		}
		if (m_packBuyBonusCalloutOnlyOncePerSession && m_packBuyBonusCalloutSeenForPackId.Contains(m_selectedStorePackId))
		{
			flag = false;
		}
		if (flag || m_packBuyBonusCalloutDebugForceDisplay > 0)
		{
			HideBonusPacksText();
			int num5 = 0;
			GeneralStorePackBuyButton packBuyButton;
			GeneralStorePackBuyButton packBuyButton2;
			if (m_packBuyBonusCalloutDebugForceDisplay > 0)
			{
				num5 = m_packBuyBonusCalloutDebugForceDisplay;
				packBuyButton = GetPackBuyButton(Math.Max(num - (num5 - 1), 0));
				packBuyButton2 = GetPackBuyButton(num);
			}
			else
			{
				num5 = 1 + Math.Max(num3 - num2, 0);
				packBuyButton = GetPackBuyButton(num2);
				packBuyButton2 = GetPackBuyButton(num3);
			}
			if (m_bonusPacksCalloutCoroutine != null)
			{
				StopCoroutine(m_bonusPacksCalloutCoroutine);
			}
			m_bonusPacksCalloutCoroutine = StartCoroutine(DelayedShowBonusPacksCallout(1f, packBuyButton, packBuyButton2, num5));
		}
		else
		{
			m_packBuyBonusCallout.HideCallout();
		}
	}

	private IEnumerator DelayedShowBonusPacksCallout(float delay, GeneralStorePackBuyButton firstButton, GeneralStorePackBuyButton lastButton, int numButtons)
	{
		yield return new WaitForSeconds(delay);
		m_packBuyBonusCallout.ShowCallout(firstButton, lastButton, numButtons);
	}

	private void UpdateBonusPacksUI(Network.Bundle bundle)
	{
		int num = 0;
		if (bundle != null)
		{
			Network.BundleItem packsBundleItemFromBundle = GetPacksBundleItemFromBundle(bundle);
			if (packsBundleItemFromBundle != null && packsBundleItemFromBundle.BaseQuantity > 0)
			{
				num = Math.Max(packsBundleItemFromBundle.Quantity - packsBundleItemFromBundle.BaseQuantity, 0);
			}
		}
		if (num > 0)
		{
			if (m_packBuyBonusCallout != null)
			{
				if (m_packBuyBonusCallout.IsShown())
				{
					m_packBuyBonusCalloutSeenForPackId.Add(m_selectedStorePackId);
				}
				m_packBuyBonusCallout.HideCallout();
			}
			ShowBonusPacksText(num, StoreManager.Get().ShouldShowFeaturedDustJar(bundle));
		}
		else
		{
			HideBonusPacksText();
		}
	}

	private void ShowBonusPacksText(int numBonusPacks, bool isShowingDustJar)
	{
		m_packBuyBonusText.gameObject.SetActive(value: true);
		if (isShowingDustJar)
		{
			m_packBuyBonusText.Text = GameStrings.Format("GLUE_CHINA_STORE_DUST_PLUS_BONUS_DETAILED", numBonusPacks, numBonusPacks);
		}
		else
		{
			m_packBuyBonusText.Text = GameStrings.Format("GLUE_STORE_BONUS_PACKS", numBonusPacks);
		}
	}

	private void HideBonusPacksText()
	{
		m_packBuyBonusText.gameObject.SetActive(value: false);
	}

	private void UpdateToggleableSections(List<ToggleableButtonFrame> sections, int numSectionsNeeded)
	{
		int num = numSectionsNeeded - 1;
		for (int i = 0; i < sections.Count; i++)
		{
			ToggleableButtonFrame toggleableButtonFrame = sections[i];
			bool active = i <= num;
			if (toggleableButtonFrame.m_IBar != null)
			{
				toggleableButtonFrame.m_IBar.SetActive(active);
			}
			toggleableButtonFrame.m_Middle.SetActive(active);
		}
	}

	private void ShowModularBundleBuyButtons()
	{
		Action action = null;
		Func<int, GeneralStorePackBuyButton> func = null;
		Func<int, GeneralStorePackBuyButton> func2 = null;
		Action<GeneralStorePackBuyButton> selectButtonFunc = null;
		MultiSliceElement multiSliceElement = null;
		MultiSliceElement multiSliceElement2 = null;
		List<ToggleableButtonFrame> list = null;
		List<GeneralStorePackBuyButton> list2 = null;
		ModularBundleDbfRecord record = GameDbf.ModularBundle.GetRecord(m_selectedStorePackId.Id);
		ModularBundleLayoutButtonSize modularBundleLayoutButtonSize = EnumUtils.SafeParse(record.LayoutButtonSize, ModularBundleLayoutButtonSize.None, ignoreCase: true);
		bool flag = modularBundleLayoutButtonSize == ModularBundleLayoutButtonSize.Large;
		if (flag)
		{
			m_packBuyContainer.SetActive(value: false);
			m_packBuyPreorderContainer.SetActive(value: true);
			func = GetPackPreorderBuyButton;
			func2 = CreatePackPreorderBuyButton;
			selectButtonFunc = SelectPackBuyPreorderButton;
			multiSliceElement = m_packBuyPreorderFrameContainer;
			multiSliceElement2 = m_packBuyPreorderButtonContainer;
			list = m_toggleablePreorderButtonFrames;
			list2 = m_packPreorderBuyButtons;
		}
		else
		{
			m_packBuyContainer.SetActive(value: true);
			m_packBuyPreorderContainer.SetActive(value: false);
			func = GetPackBuyButton;
			func2 = CreatePackBuyButton;
			selectButtonFunc = SelectPackBuyButton;
			multiSliceElement = m_packBuyFrameContainer;
			multiSliceElement2 = m_packBuyButtonContainer;
			list = m_toggleableButtonFrames;
			list2 = m_packBuyButtons;
		}
		bool isDev = !HearthstoneApplication.IsPublic() && Vars.Key("ModularBundle.ShowAll").GetBool(def: false);
		ModularBundleLayoutDbfRecord[] array = StoreManager.Get().GetRegionNodeLayoutsForBundle(record.ID).ToArray();
		if (array.Length < 2 || modularBundleLayoutButtonSize == ModularBundleLayoutButtonSize.None)
		{
			m_packBuyContainer.SetActive(value: false);
			m_packBuyPreorderContainer.SetActive(value: false);
			HandleMoneyBuyModularBundleButtonClick(0, isDev);
			return;
		}
		ClearButtonEventListeners();
		int num = 0;
		for (int i = 0; i < array.Length; i++)
		{
			GeneralStorePackBuyButton moneyButton = func(num);
			int bundleIndexCopy = i;
			if (moneyButton == null)
			{
				moneyButton = func2(i);
			}
			moneyButton.AddEventListener(UIEventType.PRESS, delegate
			{
				if (IsContentActive())
				{
					HandleMoneyBuyModularBundleButtonClick(bundleIndexCopy, isDev);
					selectButtonFunc(moneyButton);
				}
			});
			if (i == 0)
			{
				action = delegate
				{
					selectButtonFunc(moneyButton);
				};
			}
			int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(m_selectedStorePackId, i);
			Network.Bundle bundle = StoreManager.Get().EnumerateBundlesForProductType(ProductType.PRODUCT_TYPE_HIDDEN_LICENSE, requireRealMoneyOption: true, productDataFromStorePackId).FirstOrDefault();
			if (!isDev && bundle == null)
			{
				continue;
			}
			Network.BundleItem packsBundleItemFromBundle = GetPacksBundleItemFromBundle(bundle);
			string packBuyButtonText = GetPackBuyButtonText(bundle, packsBundleItemFromBundle, flag);
			moneyButton.SetMoneyValue(bundle, packsBundleItemFromBundle, packBuyButtonText);
			moneyButton.gameObject.SetActive(value: true);
			if (moneyButton.IsSelected() || GetCurrentMoneyBundle() == bundle)
			{
				action = delegate
				{
					HandleMoneyBuyModularBundleButtonClick(bundleIndexCopy, isDev);
					selectButtonFunc(moneyButton);
				};
			}
			moneyButton.Unselect();
			num++;
		}
		if (num == 0)
		{
			m_packBuyPreorderContainer.SetActive(value: false);
			m_packBuyContainer.SetActive(value: false);
		}
		for (int j = num; j < list2.Count; j++)
		{
			GeneralStorePackBuyButton generalStorePackBuyButton = list2[j];
			if (generalStorePackBuyButton != null)
			{
				generalStorePackBuyButton.gameObject.SetActive(value: false);
			}
		}
		UpdateToggleableSections(list, num);
		if (multiSliceElement != null)
		{
			multiSliceElement.UpdateSlices();
		}
		multiSliceElement2.UpdateSlices();
		action?.Invoke();
		UpdatePacksDescriptionFromSelectedStorePack();
	}

	public string GetPackBuyButtonText(Network.Bundle bundle, Network.BundleItem bundleItem, bool useLargeButtons = false)
	{
		if (bundle == null || bundleItem == null)
		{
			return string.Empty;
		}
		bool flag = StoreManager.Get().ShouldShowFeaturedDustJar(bundle);
		if (useLargeButtons)
		{
			if (flag)
			{
				return GameStrings.Format("GLUE_STORE_QUANTITY_DUST_BUNDLE", bundleItem.Quantity);
			}
			return GameStrings.Format("GLUE_STORE_QUANTITY_PACK_BUNDLE", bundleItem.Quantity);
		}
		if (flag)
		{
			return StoreManager.Get().GetProductQuantityText(ProductType.PRODUCT_TYPE_CURRENCY, bundleItem.ProductData, bundleItem.Quantity, bundleItem.BaseQuantity);
		}
		return StoreManager.Get().GetProductQuantityText(bundleItem.ItemType, bundleItem.ProductData, bundleItem.Quantity, bundleItem.BaseQuantity);
	}

	private void ShowHiddenLicenseBundleBuyButtons(Network.Bundle bundle)
	{
		m_packBuyContainer.SetActive(value: false);
		m_packBuyPreorderContainer.SetActive(value: false);
		int firstValidBundleIndex = GetFirstValidBundleIndex(m_selectedStorePackId);
		HandleMoneyPackBuyButtonClick(firstValidBundleIndex);
	}

	private void UpdatePacksTypeMusic()
	{
		if (m_parentStore.GetMode() != 0)
		{
			IStorePackDef storePackDef = GetStorePackDef(m_selectedStorePackId);
			if (storePackDef == null || storePackDef.GetPlaylist() == MusicPlaylistType.Invalid || !MusicManager.Get().StartPlaylist(storePackDef.GetPlaylist()))
			{
				m_parentStore.ResumePreviousMusicPlaylist();
			}
		}
	}

	private void HandleGoldPackBuyButtonClick()
	{
		ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(m_selectedStorePackId);
		SetCurrentGoldBundle(new NoGTAPPTransactionData
		{
			Product = productTypeFromStorePackType,
			ProductData = m_selectedStorePackId.Id,
			Quantity = m_currentGoldPackQuantity
		});
		UpdatePacksDescriptionFromSelectedStorePack();
	}

	private void HandleGoldPackBuyButtonDoubleClick(GeneralStorePackBuyButton button)
	{
		if (m_selectedStorePackId.Type == StorePackType.BOOSTER)
		{
			TelemetryManager.Client().SendChangePackQuantity(m_selectedStorePackId.Id);
		}
		m_parentStore.BlockInterface(blocked: true);
		m_quantityPrompt.Show(MAX_QUANTITY_BOUGHT_WITH_GOLD, delegate(int quantity)
		{
			m_parentStore.BlockInterface(blocked: false);
			m_currentGoldPackQuantity = quantity;
			NoGTAPPTransactionData currentGTAPPTransactionData = GetCurrentGTAPPTransactionData();
			button.UpdateFromGTAPP(currentGTAPPTransactionData);
			SetCurrentGoldBundle(currentGTAPPTransactionData);
		}, delegate
		{
			m_parentStore.BlockInterface(blocked: false);
		});
	}

	private void HandleMoneyPackBuyButtonClick(int bundleIndex)
	{
		Network.Bundle bundle = null;
		List<Network.Bundle> packBundles = GetPackBundles(sortByPackQuantity: true);
		if (packBundles != null && packBundles.Count > 0)
		{
			if (bundleIndex >= packBundles.Count)
			{
				bundleIndex = 0;
			}
			bundle = packBundles[bundleIndex];
		}
		SetCurrentMoneyBundle(bundle, force: true);
		m_lastBundleIndex = bundleIndex;
		UpdatePacksDescriptionFromSelectedStorePack();
		UpdateBonusPacksUI(bundle);
	}

	private void HandleMoneyBuyModularBundleButtonClick(int bundleIndex, bool isDev = false)
	{
		List<ModularBundleLayoutDbfRecord> records = GameDbf.ModularBundleLayout.GetRecords((ModularBundleLayoutDbfRecord r) => r.ModularBundleId == m_selectedStorePackId.Id);
		if (bundleIndex >= records.Count)
		{
			bundleIndex = 0;
		}
		int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(m_selectedStorePackId, bundleIndex);
		Network.Bundle bundle = StoreManager.Get().EnumerateBundlesForProductType(ProductType.PRODUCT_TYPE_HIDDEN_LICENSE, requireRealMoneyOption: true, productDataFromStorePackId).FirstOrDefault();
		if (isDev || bundle != null)
		{
			m_lastBundleIndex = bundleIndex;
			SetCurrentMoneyBundle(bundle, force: true);
			UpdatePacksDescriptionFromSelectedStorePack();
		}
	}

	private void SelectPackBuyButton(GeneralStorePackBuyButton packBuyBtn)
	{
		foreach (GeneralStorePackBuyButton packBuyButton in m_packBuyButtons)
		{
			packBuyButton.Unselect();
		}
		packBuyBtn.Select();
	}

	private void SelectPackBuyPreorderButton(GeneralStorePackBuyButton packBuyBtn)
	{
		foreach (GeneralStorePackBuyButton packPreorderBuyButton in m_packPreorderBuyButtons)
		{
			packPreorderBuyButton.Unselect();
		}
		packBuyBtn.Select();
	}

	private GeneralStorePackBuyButton GetPackBuyButton(int index)
	{
		if (index < m_packBuyButtons.Count)
		{
			return m_packBuyButtons[index];
		}
		return null;
	}

	private GeneralStorePackBuyButton CreatePackBuyButton(int buttonIndex)
	{
		if (buttonIndex >= m_packBuyButtons.Count)
		{
			int num = buttonIndex - m_packBuyButtons.Count + 1;
			for (int i = 0; i < num; i++)
			{
				GeneralStorePackBuyButton generalStorePackBuyButton = (GeneralStorePackBuyButton)GameUtils.Instantiate(m_packBuyButtonPrefab, m_packBuyButtonContainer.gameObject, withRotation: true);
				SceneUtils.SetLayer(generalStorePackBuyButton.gameObject, m_packBuyButtonContainer.gameObject.layer);
				generalStorePackBuyButton.transform.localRotation = Quaternion.identity;
				generalStorePackBuyButton.transform.localScale = Vector3.one;
				m_packBuyButtonContainer.AddSlice(generalStorePackBuyButton.gameObject);
				m_packBuyButtons.Add(generalStorePackBuyButton);
			}
			m_packBuyButtonContainer.UpdateSlices();
		}
		return m_packBuyButtons[buttonIndex];
	}

	private GeneralStorePackBuyButton GetPackPreorderBuyButton(int index)
	{
		if (index < m_packPreorderBuyButtons.Count)
		{
			return m_packPreorderBuyButtons[index];
		}
		return null;
	}

	private GeneralStorePackBuyButton CreatePackPreorderBuyButton(int buttonIndex)
	{
		if (buttonIndex >= m_packPreorderBuyButtons.Count)
		{
			int num = buttonIndex - m_packPreorderBuyButtons.Count + 1;
			for (int i = 0; i < num; i++)
			{
				GeneralStorePackBuyButton generalStorePackBuyButton = (GeneralStorePackBuyButton)GameUtils.Instantiate(m_packBuyPreorderButtonPrefab, m_packBuyPreorderButtonContainer.gameObject, withRotation: true);
				SceneUtils.SetLayer(generalStorePackBuyButton.gameObject, m_packBuyPreorderButtonContainer.gameObject.layer);
				generalStorePackBuyButton.transform.localRotation = Quaternion.identity;
				generalStorePackBuyButton.transform.localScale = Vector3.one;
				m_packBuyPreorderButtonContainer.AddSlice(generalStorePackBuyButton.gameObject);
				m_packPreorderBuyButtons.Add(generalStorePackBuyButton);
			}
			m_packBuyPreorderButtonContainer.UpdateSlices();
		}
		return m_packPreorderBuyButtons[buttonIndex];
	}

	private List<Network.Bundle> GetPackBundles(bool sortByPackQuantity)
	{
		ProductType selectedProductType = StorePackId.GetProductTypeFromStorePackType(m_selectedStorePackId);
		List<Network.Bundle> list = new List<Network.Bundle>();
		int productDataCountFromStorePackId = GameUtils.GetProductDataCountFromStorePackId(m_selectedStorePackId);
		for (int j = 0; j < productDataCountFromStorePackId; j++)
		{
			List<Network.Bundle> allBundlesForProduct = StoreManager.Get().GetAllBundlesForProduct(selectedProductType, requireRealMoneyOption: true, GameUtils.GetProductDataFromStorePackId(m_selectedStorePackId, j));
			list = list.Concat(allBundlesForProduct).ToList();
		}
		if (!GameUtils.IsHiddenLicenseBundleBooster(m_selectedStorePackId))
		{
			list.RemoveAll((Network.Bundle obj) => obj.Items.Find((Network.BundleItem item) => item.ItemType == ProductType.PRODUCT_TYPE_HIDDEN_LICENSE) != null);
		}
		if (sortByPackQuantity)
		{
			list.Sort(delegate(Network.Bundle left, Network.Bundle right)
			{
				int num = left?.Items.Where((Network.BundleItem i) => i.ItemType == selectedProductType).Max((Network.BundleItem i) => i.Quantity) ?? 0;
				int num2 = right?.Items.Where((Network.BundleItem i) => i.ItemType == selectedProductType).Max((Network.BundleItem i) => i.Quantity) ?? 0;
				return num - num2;
			});
		}
		return list;
	}

	private void AnimateLogo(bool animateLogo, bool isFirstStoreOpen = false)
	{
		if (!m_hasLogo || !base.gameObject.activeInHierarchy || IsPackIdInvalid(m_selectedStorePackId))
		{
			return;
		}
		MeshRenderer currentLogo = GetCurrentLogo();
		switch (m_logoAnimation)
		{
		case LogoAnimation.Slam:
			if (animateLogo)
			{
				m_logoAnimCoroutine = StartCoroutine(AnimateSlamLogo(currentLogo));
			}
			else if (!m_animatingLogo && !isFirstStoreOpen)
			{
				currentLogo.transform.localPosition = m_logoAnimationEndBone.transform.localPosition;
				currentLogo.gameObject.SetActive(value: true);
			}
			break;
		case LogoAnimation.Fade:
			if (animateLogo)
			{
				m_logoAnimCoroutine = StartCoroutine(AnimateFadeLogo(currentLogo));
			}
			else if (!m_animatingLogo)
			{
				currentLogo.gameObject.SetActive(value: false);
			}
			break;
		}
	}

	private void AnimatePacksFlying(int numVisiblePacks, bool forceImmediate = false, float delay = 0f, bool showAsSingleStack = false, bool waitForLogo = true)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		GeneralStorePacksContentDisplay currentDisplay = GetCurrentDisplay();
		if (m_packAnimCoroutine != null)
		{
			StopCoroutine(m_packAnimCoroutine);
		}
		if (m_limitedTimeOfferAnimCoroutine != null)
		{
			StopCoroutine(m_limitedTimeOfferAnimCoroutine);
		}
		if (GameUtils.IsHiddenLicenseBundleBooster(m_selectedStorePackId))
		{
			if (m_selectedStorePackId.Type == StorePackType.MODULAR_BUNDLE)
			{
				m_packAnimCoroutine = StartCoroutine(AnimateModularBundle(currentDisplay, forceImmediate, delay, waitForLogo));
			}
			else if (showAsSingleStack && GameUtils.IsFirstPurchaseBundleBooster(m_selectedStorePackId))
			{
				m_packAnimCoroutine = StartCoroutine(AnimatePacks(currentDisplay, numVisiblePacks, forceImmediate, showAsSingleStack, waitForLogo));
			}
			else
			{
				if (StoreManager.IsHiddenLicenseBundleOwned(GameUtils.GetProductDataFromStorePackId(m_selectedStorePackId, m_lastBundleIndex)) && GameUtils.IsFirstPurchaseBundleBooster(m_selectedStorePackId))
				{
					forceImmediate = true;
				}
				m_packAnimCoroutine = StartCoroutine(AnimateBundleBox(currentDisplay, delay, forceImmediate));
			}
		}
		else
		{
			m_packAnimCoroutine = StartCoroutine(AnimatePacks(currentDisplay, numVisiblePacks, forceImmediate, showAsSingleStack, waitForLogo));
		}
		m_limitedTimeOfferAnimCoroutine = StartCoroutine(AnimateLimitedTimeOfferUI(currentDisplay, waitForLogo));
	}

	private IEnumerator AnimateFadeLogo(MeshRenderer logo)
	{
		if (!(logo == null) && m_hasLogo && logo.transform.parent.gameObject.activeInHierarchy)
		{
			while (m_animatingLogo || m_loadingLogoTexture || m_loadingLogoGlowTexture)
			{
				yield return null;
			}
			logo.gameObject.SetActive(value: true);
			m_animatingLogo = true;
			PlayMakerFSM logoFSM = logo.GetComponent<PlayMakerFSM>();
			logo.transform.localPosition = m_logoAnimationStartBone.transform.localPosition;
			iTween.MoveFrom(logo.gameObject, iTween.Hash("position", logo.transform.localPosition - m_logoAppearOffset, "easetype", iTween.EaseType.easeInQuint, "time", m_logoIntroTime, "islocal", true));
			AnimationUtil.FadeTexture(logo, 0f, 1f, m_logoIntroTime, 0f);
			if (logoFSM != null)
			{
				logoFSM.SendEvent("FadeIn");
			}
			if (m_logoHoldTime > 0f)
			{
				yield return new WaitForSeconds(m_logoHoldTime);
			}
			AnimationUtil.FadeTexture(logo, 1f, 0f, m_logoOutroTime, 0f);
			if (logoFSM != null)
			{
				logoFSM.SendEvent("FadeOut");
			}
			yield return new WaitForSeconds(m_logoOutroTime);
			m_animatingLogo = false;
		}
	}

	private IEnumerator AnimateSlamLogo(MeshRenderer logo)
	{
		if (!(logo == null) && m_hasLogo && logo.transform.parent.gameObject.activeInHierarchy)
		{
			while (m_animatingLogo || m_loadingLogoTexture || m_loadingLogoGlowTexture)
			{
				yield return null;
			}
			logo.gameObject.SetActive(value: true);
			m_animatingLogo = true;
			PlayMakerFSM logoFSM = logo.GetComponent<PlayMakerFSM>();
			logo.transform.localPosition = m_logoAnimationStartBone.transform.localPosition;
			iTween.MoveFrom(logo.gameObject, iTween.Hash("position", logo.transform.localPosition - m_logoAppearOffset, "easetype", iTween.EaseType.easeInQuint, "time", m_logoIntroTime, "islocal", true));
			AnimationUtil.FadeTexture(logo, 0f, 1f, m_logoIntroTime, 0f);
			if (logoFSM != null)
			{
				logoFSM.SendEvent("FadeIn");
			}
			yield return new WaitForSeconds(m_logoIntroTime);
			if (m_logoHoldTime > 0f)
			{
				yield return new WaitForSeconds(m_logoHoldTime);
			}
			iTween.MoveTo(logo.gameObject, iTween.Hash("position", m_logoAnimationEndBone.transform.localPosition, "easetype", iTween.EaseType.easeInQuint, "time", m_logoOutroTime, "islocal", true));
			yield return new WaitForSeconds(m_logoOutroTime);
			if (logoFSM != null)
			{
				logoFSM.SendEvent("PostSlamIn");
			}
			base.gameObject.transform.localPosition = m_savedLocalPosition;
			iTween.Stop(base.gameObject);
			iTween.PunchScale(base.gameObject, m_punchAmount, m_logoDisplayPunchTime);
			yield return new WaitForSeconds(m_logoDisplayPunchTime * 0.5f);
			m_animatingLogo = false;
		}
	}

	private IEnumerator AnimatePacks(GeneralStorePacksContentDisplay display, int numVisiblePacks, bool forceImmediate, bool showAsSingleStack, bool waitForLogo)
	{
		while ((m_animatingLogo || m_loadingLogoTexture || m_loadingLogoGlowTexture) && waitForLogo)
		{
			yield return null;
		}
		StartAnimatingPacks();
		int num = display.ShowPacks(numVisiblePacks, m_packFlyInAnimTime, m_packFlyOutAnimTime, m_packFlyInDelay, m_packFlyOutDelay, forceImmediate, showAsSingleStack);
		if (!forceImmediate && num != 0)
		{
			int num2 = Mathf.Abs(num);
			float maxXRotation = ((num2 > 0) ? m_maxPackFlyInXShake : m_maxPackFlyOutXShake);
			float num3 = ((num2 > 0) ? m_packFlyInDelay : m_packFlyOutDelay);
			ShakeStore(num2, maxXRotation, (float)num2 * num3 * m_shakeObjectDelayMultiplier);
			yield return new WaitForSeconds(num3);
		}
		DoneAnimatingPacks();
	}

	public void AnimateModularBundleAfterPurchase(StorePackId storePack)
	{
		List<ModularBundleLayoutDbfRecord> regionNodeLayoutsForBundle = StoreManager.Get().GetRegionNodeLayoutsForBundle(storePack.Id);
		if (m_lastBundleIndex >= regionNodeLayoutsForBundle.Count)
		{
			Log.Store.PrintWarning($"Selected invalid layout at index={m_lastBundleIndex}. Skipping post-purchase animation.");
		}
		else if (regionNodeLayoutsForBundle[m_lastBundleIndex].AnimateAfterPurchase)
		{
			StartCoroutine(AnimateModularBundle(GetCurrentDisplay(), forceImmediate: false, 0f, waitForLogo: true));
		}
	}

	private IEnumerator AnimateModularBundle(GeneralStorePacksContentDisplay display, bool forceImmediate, float delayAnim, bool waitForLogo)
	{
		while ((m_animatingLogo || m_loadingLogoTexture || m_loadingLogoGlowTexture || m_animatingPacks) && waitForLogo)
		{
			yield return null;
		}
		StartAnimatingPacks();
		ModularBundleDbfRecord record = GameDbf.ModularBundle.GetRecord(m_selectedStorePackId.Id);
		float storeShakeDelay = 0f;
		int storeShakeAmount = 0;
		ModularBundleNodeLayout previousBundle = null;
		int nodesAnimatingIn = display.ShowModularBundle(record, forceImmediate, out storeShakeDelay, out storeShakeAmount, out previousBundle, m_lastBundleIndex);
		if (!forceImmediate && nodesAnimatingIn != 0)
		{
			while (previousBundle != null && previousBundle.IsAnimating)
			{
				yield return null;
			}
			ShakeStore(nodesAnimatingIn, m_maxPackFlyInXShake, storeShakeDelay, 0f, storeShakeAmount);
			yield return new WaitForSeconds(delayAnim + 1f);
		}
	}

	private IEnumerator ShowFeaturedDustJar(bool waitForLogo = false)
	{
		while ((m_animatingLogo || m_loadingLogoTexture || m_loadingLogoGlowTexture) && waitForLogo)
		{
			yield return null;
		}
		if (m_visibleDustCount == 0)
		{
			HideDust();
			yield break;
		}
		GeneralStorePacksContentDisplay currentDisplay = GetCurrentDisplay();
		if (currentDisplay != null)
		{
			StartCoroutine(currentDisplay.ShowDustJar(m_visibleDustCount, m_visibleDustBonusCount, m_selectedBoosterIsPrePurchase, m_selectedStorePackId));
			ShowGiftDescription(m_visibleDustCount, m_visibleDustBonusCount, m_selectedBoosterIsPrePurchase, m_selectedStorePackId);
		}
	}

	private void HideDust()
	{
		GeneralStorePacksContentDisplay currentDisplay = GetCurrentDisplay();
		if (currentDisplay != null)
		{
			currentDisplay.HideDustJar();
			HideGiftDescription();
		}
	}

	private void ShowGiftDescription(int amount, int bonusAmount, bool prePurchase, StorePackId selectedStorePackId)
	{
		GeneralStorePacksContentDisplay currentDisplay = GetCurrentDisplay();
		if (currentDisplay != null)
		{
			currentDisplay.ShowGiftDescription(amount, bonusAmount, prePurchase, selectedStorePackId);
		}
	}

	private void HideGiftDescription()
	{
		GeneralStorePacksContentDisplay currentDisplay = GetCurrentDisplay();
		if (currentDisplay != null)
		{
			currentDisplay.HideGiftDescription();
		}
	}

	private void ShowHiddenBundleCard()
	{
		GeneralStorePacksContentDisplay currentDisplay = GetCurrentDisplay();
		if (currentDisplay != null)
		{
			currentDisplay.ShowHiddenBundleCard();
		}
	}

	private void HideHiddenBundleCard()
	{
		GeneralStorePacksContentDisplay currentDisplay = GetCurrentDisplay();
		if (currentDisplay != null)
		{
			currentDisplay.HideHiddenBundleCard();
		}
	}

	private IEnumerator AnimateBundleBox(GeneralStorePacksContentDisplay display, float delayAnim, bool forceImmediate)
	{
		if (!m_waitingForBoxAnim)
		{
			while (m_animatingLogo)
			{
				yield return null;
			}
			Log.Store.Print("AnimateBundleBox: delay = {0}", delayAnim);
			StartAnimatingPacks();
			if (delayAnim > 0f)
			{
				m_waitingForBoxAnim = true;
			}
			int num = display.ShowBundleBox(m_boxFlyInAnimTime, m_boxFlyOutAnimTime, m_boxFlyInDelay, m_boxFlyOutDelay, delayAnim, forceImmediate);
			if (!forceImmediate && num != 0)
			{
				ShakeStore(1, m_boxFlyInXShake, delayAnim + m_boxFlyInAnimTime, m_boxStoreImpactTranslation);
				yield return new WaitForSeconds(delayAnim + 1f);
			}
			DoneAnimatingPacks();
			m_waitingForBoxAnim = false;
		}
	}

	private IEnumerator AnimateLimitedTimeOfferUI(GeneralStorePacksContentDisplay display, bool waitForLogo)
	{
		if (m_limitedTimeOfferText != null)
		{
			m_limitedTimeOfferText.gameObject.SetActive(value: false);
			m_limitedTimeOfferText.transform.localScale = m_limitedTimeTextOrigScale;
		}
		if (!IsContentActive() || IsPackIdInvalid(m_selectedStorePackId) || !m_showLimitedTimeOfferText || !GameUtils.IsLimitedTimeOffer(m_selectedStorePackId))
		{
			yield break;
		}
		while ((m_animatingLogo && waitForLogo) || m_animatingPacks)
		{
			yield return null;
		}
		if (m_limitedTimeOfferText != null)
		{
			StoreManager.Get().IsBoosterHiddenLicenseBundle(m_selectedStorePackId, out var hiddenLicenseBundle);
			if (StoreManager.Get().ShouldShowFeaturedDustJar(hiddenLicenseBundle))
			{
				m_limitedTimeOfferText.transform.position = m_limitedTimeOfferDustBone.position;
			}
			else
			{
				m_limitedTimeOfferText.transform.position = m_limitedTimeOfferBone.position;
			}
			m_limitedTimeOfferText.Text = GameStrings.Get("GLUE_STORE_LIMITED_TIME_OFFER");
			m_limitedTimeOfferText.gameObject.SetActive(value: true);
			m_limitedTimeOfferText.transform.localScale = m_limitedTimeTextOrigScale * 0.01f;
			iTween.ScaleTo(m_limitedTimeOfferText.gameObject, iTween.Hash("scale", m_limitedTimeTextOrigScale, "time", 0.25f, "easetype", iTween.EaseType.easeOutQuad));
		}
	}

	private void ResetAnimations()
	{
		if (m_preorderCardBackReward != null)
		{
			m_preorderCardBackReward.HideCardBackReward();
		}
		if (m_availableDateText != null)
		{
			m_availableDateText.gameObject.SetActive(value: false);
		}
		if (m_limitedTimeOfferText != null)
		{
			m_limitedTimeOfferText.gameObject.SetActive(value: false);
		}
		if (m_logoAnimCoroutine != null)
		{
			iTween.Stop(m_logoMesh1.gameObject);
			iTween.Stop(m_logoMesh2.gameObject);
			StopCoroutine(m_logoAnimCoroutine);
		}
		m_logoMesh1.gameObject.SetActive(value: false);
		m_logoMesh2.gameObject.SetActive(value: false);
		if (m_packAnimCoroutine != null)
		{
			StopCoroutine(m_packAnimCoroutine);
		}
		if (m_limitedTimeOfferAnimCoroutine != null)
		{
			StopCoroutine(m_limitedTimeOfferAnimCoroutine);
		}
		if (m_packBuyBonusCallout != null)
		{
			m_packBuyBonusCallout.DeactivateCallout();
		}
		if (m_bonusPacksCalloutCoroutine != null)
		{
			StopCoroutine(m_bonusPacksCalloutCoroutine);
		}
		m_animatingLogo = false;
		m_animatingPacks = false;
		m_waitingForBoxAnim = false;
	}

	private void AnimateAndUpdateDisplay(StorePackId storePackId, bool forceImmediate = false)
	{
		if (m_preorderCardBackReward != null)
		{
			m_preorderCardBackReward.HideCardBackReward();
		}
		GameObject currDisplay = null;
		GameObject gameObject = null;
		if (m_currentDisplay == -1)
		{
			m_currentDisplay = 1;
			currDisplay = m_packEmptyDisplay;
		}
		else
		{
			currDisplay = GetCurrentDisplayContainer();
		}
		gameObject = GetNextDisplayContainer();
		GetCurrentLogo().gameObject.SetActive(value: false);
		GetCurrentDisplay().ClearContents();
		m_currentDisplay = (m_currentDisplay + 1) % 2;
		gameObject.SetActive(value: true);
		if (!forceImmediate)
		{
			currDisplay.transform.localRotation = Quaternion.identity;
			gameObject.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
			iTween.StopByName(currDisplay, "ROTATION_TWEEN");
			iTween.StopByName(gameObject, "ROTATION_TWEEN");
			iTween.RotateBy(currDisplay, iTween.Hash("amount", new Vector3(0.5f, 0f, 0f), "time", 0.5f, "name", "ROTATION_TWEEN", "oncomplete", (Action<object>)delegate
			{
				currDisplay.SetActive(value: false);
			}));
			iTween.RotateBy(gameObject, iTween.Hash("amount", new Vector3(0.5f, 0f, 0f), "time", 0.5f, "name", "ROTATION_TWEEN"));
			if (!string.IsNullOrEmpty(m_backgroundFlipSound))
			{
				SoundManager.Get().LoadAndPlay(m_backgroundFlipSound);
			}
		}
		else
		{
			gameObject.transform.localRotation = Quaternion.identity;
			currDisplay.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
			currDisplay.SetActive(value: false);
		}
		IStorePackDef packDef = GetStorePackDef(storePackId);
		GetCurrentDisplay().UpdatePackType(packDef);
		MeshRenderer currLogo = GetCurrentLogo();
		if (currLogo != null)
		{
			m_hasLogo = !string.IsNullOrEmpty(packDef.GetLogoTextureName());
			if (m_hasLogo)
			{
				m_loadingLogoTexture = true;
				AssetHandleCallback<Texture> onTextureLoaded = null;
				onTextureLoaded = delegate(AssetReference name, AssetHandle<Texture> loadedTexture, object data)
				{
					m_loadingLogoTexture = false;
					if (loadedTexture == null)
					{
						if ((bool)data)
						{
							Error.AddDevFatal("Loading localized logo failed.  This is normal if we're on android and just switched.  Trying unlocalized.");
							m_loadingLogoTexture = true;
							AssetLoader.Get().LoadAsset(packDef.GetLogoTextureName(), onTextureLoaded, false, AssetLoadingOptions.DisableLocalization);
						}
						else
						{
							Debug.LogError($"Failed to load logo with texture {base.name}!");
						}
					}
					else if (currLogo != null)
					{
						currLogo.GetMaterial().mainTexture = loadedTexture;
						HearthstoneServices.Get<DisposablesCleaner>()?.Attach(currLogo, loadedTexture);
					}
					else
					{
						loadedTexture.Dispose();
					}
				};
				AssetLoader.Get().LoadAsset(packDef.GetLogoTextureName(), onTextureLoaded, true);
				MeshRenderer glowLogo = GetCurrentGlowLogo();
				if (glowLogo != null)
				{
					m_loadingLogoGlowTexture = true;
					AssetLoader.Get().LoadAsset(packDef.GetLogoTextureGlowName(), delegate(AssetReference name, AssetHandle<Texture> loadedTexture, object data)
					{
						m_loadingLogoGlowTexture = false;
						if (loadedTexture == null)
						{
							Debug.LogError($"Failed to load texture {base.name}!");
						}
						else if (glowLogo != null)
						{
							glowLogo.GetMaterial().mainTexture = loadedTexture;
							HearthstoneServices.Get<DisposablesCleaner>()?.Attach(glowLogo, loadedTexture);
						}
						else
						{
							loadedTexture.Dispose();
						}
					});
				}
			}
		}
		AnimateBuyBar();
	}

	private void AnimateBuyBar()
	{
		Network.Bundle preOrderBundle;
		GameObject gameObject = (StoreManager.Get().IsBoosterPreorderActive(GameUtils.GetProductDataFromStorePackId(m_selectedStorePackId, m_lastBundleIndex), StorePackId.GetProductTypeFromStorePackType(m_selectedStorePackId), out preOrderBundle) ? m_packBuyContainer : m_packBuyPreorderContainer);
		if (!IsPackIdInvalid(m_selectedStorePackId))
		{
			iTween.Stop(gameObject);
			gameObject.transform.localRotation = Quaternion.identity;
			iTween.RotateBy(gameObject, iTween.Hash("amount", new Vector3(-1f, 0f, 0f), "time", m_backgroundFlipAnimTime, "delay", 0.001f));
		}
	}

	private void UpdateKoreaInfoButton()
	{
		if (!(m_ChinaInfoButton == null))
		{
			bool active = StoreManager.Get().IsKoreanCustomer() && IsContentActive() && !IsPackIdInvalid(m_selectedStorePackId);
			m_ChinaInfoButton.gameObject.SetActive(active);
		}
	}

	private void OnKoreaInfoPressed(UIEvent e)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_STORE_KOREAN_DISCLAIMER_HEADLINE");
		popupInfo.m_text = GameStrings.Get("GLUE_STORE_KOREAN_DISCLAIMER_DETAILS");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private bool IsPackIdFirstPurchaseBundle(StorePackId storePackId)
	{
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			return storePackId.Id == 181;
		}
		return false;
	}

	private bool IsPackIdInvalid(StorePackId storePackId)
	{
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			return storePackId.Id == 0;
		}
		if (storePackId.Type == StorePackType.MODULAR_BUNDLE)
		{
			return storePackId.Id == 0;
		}
		return true;
	}

	private void ClearButtonEventListeners()
	{
		foreach (GeneralStorePackBuyButton packBuyButton in m_packBuyButtons)
		{
			packBuyButton.ClearEventListeners();
		}
		foreach (GeneralStorePackBuyButton packPreorderBuyButton in m_packPreorderBuyButtons)
		{
			packPreorderBuyButton.ClearEventListeners();
		}
	}
}
