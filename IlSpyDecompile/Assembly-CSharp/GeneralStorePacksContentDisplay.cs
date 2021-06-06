using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using UnityEngine;

[CustomEditClass]
public class GeneralStorePacksContentDisplay : MonoBehaviour
{
	public enum PACK_DISPLAY_TYPE
	{
		PACK,
		BOX
	}

	public MeshRenderer m_background;

	public List<GameObject> m_packStacks = new List<GameObject>();

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_dustJar;

	public UberText m_dustAmountText;

	public int m_dustAmountTextFontSize;

	public int m_dustAmountTextFontSizeForBonus;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_hiddenCard;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_giftDescription;

	public UberText m_giftDescriptionText;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_firstPurchaseBundleGiftDescription;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_hiddenLicenseBundleGiftDescription;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_leavingSoonBannerPrefab;

	public GameObject m_jarFlash;

	public Animator m_jarFlashAnimController;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_dustJarBone;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_prePurchaseDustJarBone;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_bundleDustJarBone;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_giftDescriptionBone;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_prePurchaseGiftDescriptionBone;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_leavingSoonBone;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_nodeLayoutBone;

	private PACK_DISPLAY_TYPE m_packDisplayType;

	private GeneralStorePacksContent m_parent;

	private List<AnimatedLowPolyPack> m_showingPacks = new List<AnimatedLowPolyPack>();

	private List<ModularBundleNodeLayout> m_showingModularBundleNodeLayouts = new List<ModularBundleNodeLayout>();

	private List<AnimatedLeavingSoonSign> m_showingLeavingSoonSigns = new List<AnimatedLeavingSoonSign>();

	private static readonly Vector3 PACK_SCALE = new Vector3(0.06f, 0.03f, 0.06f);

	private static readonly Vector3 BOX_BUNDLE_DUST_SCALE = new Vector3(0.045f, 0.03f, 0.045f);

	private static readonly float PACK_X_VARIATION_MAG = 0.015f;

	private static readonly float PACK_Y_OFFSET = 0.02f;

	private static readonly float PACK_Z_VARIATION_MAG = 0.01f;

	private static readonly float PACK_FLY_OUT_X_DEG_VARIATION_MAG = 10f;

	private static readonly float PACK_FLY_OUT_Z_DEG_VARIATION_MAG = 10f;

	private static readonly float BOX_FLY_OUT_X_DEG_VARIATION_MAG = 0f;

	private static readonly float BOX_FLY_OUT_Z_DEG_VARIATION_MAG = 0f;

	private static readonly int PACK_STACK_SEED = 2;

	private int m_lastVisiblePacks;

	private int m_lastVisibleDust;

	private int m_lastVisibleDustBonus;

	private bool m_dustJarFlashing;

	private bool m_loadingModularBundle;

	private AssetHandle<Texture> m_packBackgroundTexture;

	private AssetHandle<Material> m_packBackgroundMaterial;

	private static Map<int, AnimatedLowPolyPack> s_packTemplates = new Map<int, AnimatedLowPolyPack>();

	public void SetParent(GeneralStorePacksContent parent)
	{
		m_parent = parent;
	}

	public GeneralStorePacksContent GetParent()
	{
		return m_parent;
	}

	private void OnDestroy()
	{
		AssetHandle.SafeDispose(ref m_packBackgroundTexture);
		AssetHandle.SafeDispose(ref m_packBackgroundMaterial);
	}

	public int ShowPacks(int numVisiblePacks, float flyInTime, float flyOutTime, float flyInDelay, float flyOutDelay, bool forceImmediate = false, bool showAsSingleStack = false)
	{
		if (showAsSingleStack)
		{
			return ShowPacksAsSingleStack(numVisiblePacks, flyInTime, flyOutTime, flyInDelay, flyOutDelay, forceImmediate);
		}
		m_packDisplayType = PACK_DISPLAY_TYPE.PACK;
		bool flag = m_parent.IsContentActive();
		AnimatedLowPolyPack[] array = ConfigureAndGetCurrentPackLayout(m_parent.GetStorePackId(), numVisiblePacks);
		if (array.Length != 0 && array[0] != null)
		{
			array[0].HideBanner();
		}
		if (m_lastVisiblePacks == numVisiblePacks)
		{
			return 0;
		}
		int num = 0;
		for (int num2 = array.Length - 1; num2 >= numVisiblePacks; num2--)
		{
			AnimatedLowPolyPack animatedLowPolyPack = array[num2];
			if (flag && !forceImmediate)
			{
				if (animatedLowPolyPack.FlyOut(flyOutTime, flyOutDelay * (float)num))
				{
					num++;
				}
			}
			else
			{
				animatedLowPolyPack.FlyOutImmediate();
			}
		}
		int num3 = 0;
		for (int i = 0; i < numVisiblePacks; i++)
		{
			AnimatedLowPolyPack animatedLowPolyPack2 = array[i];
			if (flag && !forceImmediate)
			{
				if (animatedLowPolyPack2.FlyIn(flyInTime, flyInDelay * (float)num3))
				{
					num3++;
				}
			}
			else
			{
				animatedLowPolyPack2.FlyInImmediate();
			}
		}
		FlyLeavingSoonBanner(num3, num, flyInTime, flyOutTime, flyInDelay, flyOutDelay, numVisiblePacks, flag && !forceImmediate);
		m_lastVisiblePacks = numVisiblePacks;
		if (num3 > num)
		{
			return num3;
		}
		return -num;
	}

	public int ShowModularBundle(ModularBundleDbfRecord modularBundleRecord, bool forceImmediate, out float delay, out int weight, out ModularBundleNodeLayout prevLayout, int selectedIndex = 0)
	{
		List<ModularBundleLayoutDbfRecord> regionNodeLayoutsForBundle = StoreManager.Get().GetRegionNodeLayoutsForBundle(modularBundleRecord.ID);
		if (selectedIndex >= regionNodeLayoutsForBundle.Count)
		{
			Log.Store.PrintWarning($"Selected invalid sub-bundle at index={selectedIndex}. Using sub-bundle at index=0");
			selectedIndex = 0;
		}
		ModularBundleLayoutDbfRecord currentLayoutRecord = regionNodeLayoutsForBundle[selectedIndex];
		prevLayout = ((m_showingModularBundleNodeLayouts.Count > 0) ? m_showingModularBundleNodeLayouts[0] : null);
		if (prevLayout != null && currentLayoutRecord.ID == prevLayout.LayoutID)
		{
			weight = 0;
			delay = 0f;
			m_parent.DoneAnimatingPacks();
			return 0;
		}
		List<ModularBundleLayoutNodeDbfRecord> records = GameDbf.ModularBundleLayoutNode.GetRecords((ModularBundleLayoutNodeDbfRecord r) => r.NodeLayoutId == currentLayoutRecord.ID);
		records.Sort((ModularBundleLayoutNodeDbfRecord l, ModularBundleLayoutNodeDbfRecord r) => l.NodeIndex.CompareTo(r.NodeIndex));
		weight = 0;
		int num = 0;
		foreach (ModularBundleLayoutNodeDbfRecord item in records)
		{
			if (item.ShakeWeight > 0)
			{
				num++;
				weight += item.ShakeWeight;
			}
		}
		if (m_loadingModularBundle)
		{
			delay = 0f;
			return 0;
		}
		m_loadingModularBundle = true;
		delay = (float)currentLayoutRecord.StoreShakeDelay;
		ModularBundleNodeLayout.NodeCallbackData nodeCallbackData = new ModularBundleNodeLayout.NodeCallbackData(currentLayoutRecord.ID, records, currentLayoutRecord.Prefab, forceImmediate);
		if (prevLayout != null)
		{
			prevLayout.PlayExitAnimationsInSequence(forceImmediate, OnPreviousModularBundleFinishAnimating, nodeCallbackData);
			int outAnimWeight = 0;
			prevLayout.Nodes.ForEach(delegate(ModularBundleNode n)
			{
				outAnimWeight += n.GetNodeShakeWeight();
			});
			m_parent.ShakeStore(prevLayout.Nodes.Count, 10f, 0f, 0f, outAnimWeight);
		}
		else
		{
			OnPreviousModularBundleFinishAnimating(nodeCallbackData);
		}
		return num;
	}

	private void OnPreviousModularBundleFinishAnimating(object callbackData)
	{
		ModularBundleNodeLayout.NodeCallbackData nodeCallbackData = (ModularBundleNodeLayout.NodeCallbackData)callbackData;
		AssetLoader.Get().InstantiatePrefab(new AssetReference(nodeCallbackData.prefab), OnModularBundleNodeLayoutLoaded, nodeCallbackData);
	}

	private void OnModularBundleNodeLayoutLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		ModularBundleNodeLayout.NodeCallbackData nodeCallbackData = (ModularBundleNodeLayout.NodeCallbackData)callbackData;
		if (go == null || !go.activeInHierarchy)
		{
			m_loadingModularBundle = false;
			m_parent.DoneAnimatingPacks();
			return;
		}
		ClearContents();
		ModularBundleNodeLayout component = go.GetComponent<ModularBundleNodeLayout>();
		if (component == null)
		{
			m_loadingModularBundle = false;
			m_parent.DoneAnimatingPacks();
			return;
		}
		GameUtils.SetParent(component, m_nodeLayoutBone, withRotation: true);
		component.Initialize(this, nodeCallbackData.layoutId, nodeCallbackData.layoutNodes);
		m_showingModularBundleNodeLayouts.Add(component);
		component.PlayEntranceAnimationsInSequence(nodeCallbackData.forceImmediate, OnModularBundleDoneAnimatingIn, null);
		m_loadingModularBundle = false;
	}

	private void OnModularBundleDoneAnimatingIn(object callbackData)
	{
		m_parent.DoneAnimatingPacks();
	}

	public IEnumerator ShowDustJar(int dustAmount, int dustAmountBonus, bool prePurchase, StorePackId selectedStorePackId)
	{
		if (m_dustJar == null || m_dustAmountText == null)
		{
			yield break;
		}
		m_dustJar.SetActive(value: true);
		if (prePurchase)
		{
			TransformUtil.AttachAndPreserveLocalTransform(m_dustJar.transform, m_prePurchaseDustJarBone.transform);
		}
		else if (GameUtils.IsHiddenLicenseBundleBooster(selectedStorePackId))
		{
			TransformUtil.AttachAndPreserveLocalTransform(m_dustJar.transform, m_bundleDustJarBone.transform);
		}
		else
		{
			TransformUtil.AttachAndPreserveLocalTransform(m_dustJar.transform, m_dustJarBone.transform);
		}
		if (dustAmount == m_lastVisibleDust && dustAmountBonus == m_lastVisibleDustBonus)
		{
			UpdateDustJarAmountText(dustAmount, dustAmountBonus);
			yield break;
		}
		if (m_dustJarFlashing)
		{
			UpdateDustJarAmountText(m_lastVisibleDust, m_lastVisibleDustBonus);
		}
		m_lastVisibleDust = dustAmount;
		m_lastVisibleDustBonus = dustAmountBonus;
		if (m_jarFlash != null && m_jarFlashAnimController != null)
		{
			m_jarFlash.SetActive(value: false);
			m_jarFlash.SetActive(value: true);
			m_jarFlashAnimController.enabled = true;
			m_jarFlashAnimController.StopPlayback();
			yield return new WaitForEndOfFrame();
			if (m_jarFlashAnimController == null)
			{
				yield break;
			}
			m_jarFlashAnimController.Play("Flash");
			m_dustJarFlashing = true;
			while (m_jarFlashAnimController != null && m_jarFlashAnimController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
			{
				yield return null;
			}
		}
		UpdateDustJarAmountText(dustAmount, dustAmountBonus);
		m_dustJarFlashing = false;
	}

	private void UpdateDustJarAmountText(int dustAmount, int dustAmountBonus)
	{
		if (dustAmountBonus > 0)
		{
			m_dustAmountText.FontSize = m_dustAmountTextFontSizeForBonus;
			m_dustAmountText.Text = GameStrings.Format("GLUE_CHINA_STORE_DUST_PLUS_BONUS", dustAmount, dustAmountBonus);
		}
		else
		{
			m_dustAmountText.FontSize = m_dustAmountTextFontSize;
			m_dustAmountText.Text = dustAmount.ToString();
		}
	}

	public void HideDustJar()
	{
		if (!(m_dustJar == null))
		{
			m_dustJar.SetActive(value: false);
		}
	}

	public void ShowGiftDescription(int dustAmount, int dustBonusAmount, bool prePurchase, StorePackId selectedStorePackId)
	{
		if (m_giftDescription == null || m_giftDescriptionText == null || m_firstPurchaseBundleGiftDescription == null || m_prePurchaseGiftDescriptionBone == null || m_giftDescriptionBone == null || m_hiddenLicenseBundleGiftDescription == null)
		{
			return;
		}
		if (GameUtils.IsFirstPurchaseBundleBooster(selectedStorePackId))
		{
			m_giftDescription.SetActive(value: false);
			m_hiddenLicenseBundleGiftDescription.SetActive(value: false);
			m_firstPurchaseBundleGiftDescription.SetActive(value: true);
		}
		else if (GameUtils.IsHiddenLicenseBundleBooster(selectedStorePackId))
		{
			m_giftDescription.SetActive(value: false);
			m_firstPurchaseBundleGiftDescription.SetActive(value: false);
			m_hiddenLicenseBundleGiftDescription.SetActive(value: true);
		}
		else
		{
			m_giftDescription.SetActive(value: true);
			m_firstPurchaseBundleGiftDescription.SetActive(value: false);
			m_hiddenLicenseBundleGiftDescription.SetActive(value: false);
			if (prePurchase)
			{
				m_giftDescriptionText.Text = GameStrings.Format("GLUE_CHINA_STORE_BOOSTER_GIFT_PREORDER_BONUS", dustAmount);
			}
			else if (dustBonusAmount > 0)
			{
				m_giftDescriptionText.Text = GameStrings.Format("GLUE_CHINA_STORE_BOOSTER_GIFT_PLUS_BONUS", dustAmount, dustBonusAmount);
			}
			else
			{
				m_giftDescriptionText.Text = GameStrings.Format("GLUE_CHINA_STORE_BOOSTER_GIFT", dustAmount);
			}
		}
		if (prePurchase)
		{
			TransformUtil.AttachAndPreserveLocalTransform(m_giftDescription.transform, m_prePurchaseGiftDescriptionBone.transform);
		}
		else
		{
			TransformUtil.AttachAndPreserveLocalTransform(m_giftDescription.transform, m_giftDescriptionBone.transform);
		}
	}

	public void HideGiftDescription()
	{
		if (m_giftDescription != null)
		{
			m_giftDescription.SetActive(value: false);
		}
		if (m_firstPurchaseBundleGiftDescription != null)
		{
			m_firstPurchaseBundleGiftDescription.SetActive(value: false);
		}
		if (m_hiddenLicenseBundleGiftDescription != null)
		{
			m_hiddenLicenseBundleGiftDescription.SetActive(value: false);
		}
	}

	public void ShowHiddenBundleCard()
	{
		if (m_hiddenCard != null)
		{
			m_hiddenCard.SetActive(value: true);
		}
	}

	public void HideHiddenBundleCard()
	{
		if (m_hiddenCard != null)
		{
			m_hiddenCard.SetActive(value: false);
		}
	}

	public int ShowBundleBox(float flyInTime, float flyOutTime, float flyInDelay, float flyOutDelay, float delay = 0f, bool forceImmediate = false)
	{
		Log.Store.Print("ShowBundleBox()");
		int num = 1;
		if (m_lastVisiblePacks == num)
		{
			return 0;
		}
		m_packDisplayType = PACK_DISPLAY_TYPE.BOX;
		bool animated = m_parent.IsContentActive();
		AnimatedLowPolyPack[] array = ConfigureAndGetCurrentPackLayout(m_parent.GetStorePackId(), 1);
		int num2 = 0;
		AnimatedLowPolyPack animatedLowPolyPack = array[0];
		if (!forceImmediate)
		{
			animatedLowPolyPack.FlyIn(flyInTime, delay);
			num2++;
		}
		else
		{
			animatedLowPolyPack.FlyInImmediate();
		}
		FlyLeavingSoonBanner(num, 1, flyInTime, flyOutTime, flyInDelay, flyOutDelay, 1, animated);
		m_lastVisiblePacks = num;
		return num2;
	}

	public void PurchaseBundleBox(CardReward rewardCard)
	{
		AnimatedLowPolyPack[] array = ConfigureAndGetCurrentPackLayout(m_parent.GetStorePackId(), 1);
		CardRewardData cardRewardData = new CardRewardData();
		if (rewardCard != null)
		{
			cardRewardData = rewardCard.Data as CardRewardData;
		}
		if (array == null || array.Length < 1)
		{
			Debug.LogWarningFormat("PurchaseBundleBox() didn't caontain any packs for cardID {0}", cardRewardData.CardID);
			return;
		}
		AnimatedLowPolyPack animatedLowPolyPack = array[0];
		if (animatedLowPolyPack == null)
		{
			Debug.LogWarningFormat("PurchaseBundleBox() failed to get AnimatedLowPolyPack for cardID {0}", cardRewardData.CardID);
			return;
		}
		FirstPurchaseBox firstPurchaseBox = animatedLowPolyPack.GetFirstPurchaseBox();
		if (firstPurchaseBox == null)
		{
			if (rewardCard != null)
			{
				rewardCard.transform.localPosition = GetRewardLocalPos();
				SceneUtils.SetLayer(rewardCard, GameLayer.PerspectiveUI);
				RewardUtils.ShowReward(UserAttentionBlocker.NONE, rewardCard, updateCacheValues: true, GetRewardPunchScale(), GetRewardScale(), OnRewardShown, rewardCard);
			}
			else
			{
				Debug.LogWarning("Null reference on rewardCard object.");
			}
		}
		else
		{
			firstPurchaseBox.PurchaseBundle(cardRewardData.CardID);
		}
	}

	public void UpdatePackType(IStorePackDef packDef)
	{
		ClearContents();
		if (!(m_background == null) && packDef != null)
		{
			AssetLoader.Get().LoadAsset(ref m_packBackgroundMaterial, packDef.GetBackgroundMaterial());
			if ((bool)m_packBackgroundMaterial)
			{
				m_background.SetMaterial(m_packBackgroundMaterial);
			}
			AssetLoader.Get().LoadAsset(ref m_packBackgroundTexture, packDef.GetBackgroundTexture());
			if ((bool)m_packBackgroundTexture)
			{
				m_background.GetMaterial().mainTexture = m_packBackgroundTexture;
			}
		}
	}

	public void ClearContents()
	{
		foreach (AnimatedLowPolyPack showingPack in m_showingPacks)
		{
			UnityEngine.Object.Destroy(showingPack.gameObject);
		}
		m_showingPacks.Clear();
		foreach (AnimatedLeavingSoonSign showingLeavingSoonSign in m_showingLeavingSoonSigns)
		{
			UnityEngine.Object.Destroy(showingLeavingSoonSign.gameObject);
		}
		m_showingLeavingSoonSigns.Clear();
		foreach (ModularBundleNodeLayout showingModularBundleNodeLayout in m_showingModularBundleNodeLayouts)
		{
			UnityEngine.Object.Destroy(showingModularBundleNodeLayout.gameObject);
		}
		m_showingModularBundleNodeLayouts.Clear();
		m_lastVisiblePacks = 0;
		m_lastVisibleDust = 0;
		if (m_dustJar != null)
		{
			m_dustJar.SetActive(value: false);
			m_dustJarFlashing = false;
		}
		if (m_hiddenCard != null)
		{
			m_hiddenCard.SetActive(value: false);
		}
		if (m_giftDescription != null)
		{
			m_giftDescription.SetActive(value: false);
		}
		if (m_firstPurchaseBundleGiftDescription != null)
		{
			m_firstPurchaseBundleGiftDescription.SetActive(value: false);
		}
		if (m_hiddenLicenseBundleGiftDescription != null)
		{
			m_hiddenLicenseBundleGiftDescription.SetActive(value: false);
		}
		m_loadingModularBundle = false;
	}

	private int ShowPacksAsSingleStack(int numVisiblePacks, float flyInTime, float flyOutTime, float flyInDelay, float flyOutDelay, bool forceImmediate = false)
	{
		m_packDisplayType = PACK_DISPLAY_TYPE.PACK;
		bool flag = m_parent.IsContentActive();
		int id = (GameUtils.IsFirstPurchaseBundleBooster(m_parent.GetStorePackId()) ? 1 : m_parent.GetStorePackId().Id);
		StorePackId storePackId = default(StorePackId);
		storePackId.Type = StorePackType.BOOSTER;
		storePackId.Id = id;
		StorePackId storePackId2 = storePackId;
		AnimatedLowPolyPack[] array = ConfigureAndGetCurrentPackLayout(storePackId2, 1);
		FlyLeavingSoonBanner(0, 0, flyInTime, flyOutTime, flyInDelay, flyOutDelay, numVisiblePacks, flag && !forceImmediate);
		if (m_lastVisiblePacks == 1)
		{
			if (array.Length != 0 && array[0] != null)
			{
				array[0].UpdateBannerCount(numVisiblePacks);
			}
			return 0;
		}
		int num = 0;
		for (int num2 = array.Length - 1; num2 >= 1; num2--)
		{
			AnimatedLowPolyPack animatedLowPolyPack = array[num2];
			if (flag && !forceImmediate)
			{
				if (animatedLowPolyPack.FlyOut(flyOutTime, flyOutDelay * (float)num))
				{
					num++;
				}
			}
			else
			{
				animatedLowPolyPack.FlyOutImmediate();
			}
		}
		array[0].FlyInImmediate();
		array[0].UpdateBannerCount(numVisiblePacks);
		m_lastVisiblePacks = 1;
		return 0;
	}

	private AnimatedLowPolyPack[] ConfigureAndGetCurrentPackLayout(StorePackId storePackId, int count)
	{
		if (count > m_showingPacks.Count)
		{
			AnimatedLowPolyPack value = null;
			if (!s_packTemplates.TryGetValue(storePackId.Id, out value) || !value)
			{
				IStorePackDef storePackDef = m_parent.GetStorePackDef(storePackId);
				if (string.IsNullOrEmpty(storePackDef.GetLowPolyPrefab()) && string.IsNullOrEmpty(storePackDef.GetLowPolyDustPrefab()))
				{
					return m_showingPacks.ToArray();
				}
				GameObject gameObject = ((!GameUtils.IsHiddenLicenseBundleBooster(m_parent.GetStorePackId()) || GameUtils.IsFirstPurchaseBundleBooster(m_parent.GetStorePackId()) || !m_parent.SelectedBundleFeaturesDustJar() || string.IsNullOrEmpty(storePackDef.GetLowPolyDustPrefab())) ? AssetLoader.Get().InstantiatePrefab(storePackDef.GetLowPolyPrefab()) : AssetLoader.Get().InstantiatePrefab(storePackDef.GetLowPolyDustPrefab()));
				value = gameObject.GetComponent<AnimatedLowPolyPack>();
				s_packTemplates[storePackId.Id] = value;
				value.gameObject.SetActive(value: false);
			}
			for (int i = m_showingPacks.Count; i < count; i++)
			{
				AnimatedLowPolyPack animatedLowPolyPack = UnityEngine.Object.Instantiate(value);
				SetupLowPolyPack(animatedLowPolyPack, i, useVisiblePacksOnly: false);
				m_showingPacks.Add(animatedLowPolyPack);
			}
		}
		return m_showingPacks.ToArray();
	}

	private void SetupLowPolyPack(AnimatedLowPolyPack pack, int i, bool useVisiblePacksOnly)
	{
		pack.gameObject.SetActive(value: true);
		bool forceLastColumn = pack.m_isLeavingSoonBanner && m_parent.SelectedBundleFeaturesDustJar();
		int num = DeterminePackColumn(i, forceLastColumn);
		GameUtils.SetParent(pack, m_packStacks[num], withRotation: true);
		if (GameUtils.IsHiddenLicenseBundleBooster(m_parent.GetStorePackId()) && !GameUtils.IsFirstPurchaseBundleBooster(m_parent.GetStorePackId()) && m_parent.SelectedBundleFeaturesDustJar())
		{
			pack.transform.localScale = BOX_BUNDLE_DUST_SCALE;
		}
		else
		{
			pack.transform.localScale = PACK_SCALE;
		}
		pack.Init(num, DeterminePackLocalPos(num, m_showingPacks, useVisiblePacksOnly), new Vector3(0f, 3.5f, -0.1f));
		SceneUtils.SetLayer(pack, m_packStacks[num].layer);
		float y = 0f;
		float x = 0f;
		float z = 0f;
		Log.Store.Print("SetupLowPolyPack pack display type: {0}", m_packDisplayType);
		if (m_packDisplayType == PACK_DISPLAY_TYPE.BOX)
		{
			y = UnityEngine.Random.Range(0f - m_parent.m_BoxYDegreeVariationMag, m_parent.m_BoxYDegreeVariationMag);
			x = UnityEngine.Random.Range(0f - BOX_FLY_OUT_X_DEG_VARIATION_MAG, BOX_FLY_OUT_X_DEG_VARIATION_MAG);
			z = UnityEngine.Random.Range(0f - BOX_FLY_OUT_Z_DEG_VARIATION_MAG, BOX_FLY_OUT_Z_DEG_VARIATION_MAG);
		}
		else
		{
			if (pack.m_isLeavingSoonBanner && m_parent.SelectedBundleFeaturesDustJar())
			{
				Vector3 vector = new Vector3(m_leavingSoonBone.transform.localEulerAngles.x, m_leavingSoonBone.transform.localEulerAngles.y, m_leavingSoonBone.transform.localEulerAngles.z);
				pack.SetFlyingLocalRotations(vector, vector);
				return;
			}
			if (m_packDisplayType == PACK_DISPLAY_TYPE.PACK)
			{
				y = UnityEngine.Random.Range(0f - m_parent.m_PackYDegreeVariationMag, m_parent.m_PackYDegreeVariationMag);
				x = UnityEngine.Random.Range(0f - PACK_FLY_OUT_X_DEG_VARIATION_MAG, PACK_FLY_OUT_X_DEG_VARIATION_MAG);
				z = UnityEngine.Random.Range(0f - PACK_FLY_OUT_Z_DEG_VARIATION_MAG, PACK_FLY_OUT_Z_DEG_VARIATION_MAG);
			}
		}
		Vector3 flyInLocalAngles = new Vector3(0f, y, 0f);
		Vector3 flyOutLocalAngles = new Vector3(x, 0f, z);
		pack.SetFlyingLocalRotations(flyInLocalAngles, flyOutLocalAngles);
	}

	private Vector3 DeterminePackLocalPos(int column, List<AnimatedLowPolyPack> packs, bool useVisiblePacksOnly)
	{
		List<AnimatedLowPolyPack> list = packs.FindAll((AnimatedLowPolyPack obj) => obj.Column == column && (!useVisiblePacksOnly || obj.GetState() == AnimatedLowPolyPack.State.FLOWN_IN || obj.GetState() == AnimatedLowPolyPack.State.FLYING_IN));
		Vector3 result = Vector3.zero;
		if (m_packDisplayType == PACK_DISPLAY_TYPE.BOX && GameUtils.IsHiddenLicenseBundleBooster(m_parent.GetStorePackId()) && !GameUtils.IsFirstPurchaseBundleBooster(m_parent.GetStorePackId()) && m_parent.SelectedBundleFeaturesDustJar())
		{
			result = new Vector3(-0.06f, 0f, -0.03f);
		}
		else if (m_packDisplayType != PACK_DISPLAY_TYPE.BOX && !GameUtils.IsHiddenLicenseBundleBooster(m_parent.GetStorePackId()))
		{
			result.x = UnityEngine.Random.Range(0f - PACK_X_VARIATION_MAG, PACK_X_VARIATION_MAG);
			result.y = PACK_Y_OFFSET * (float)list.Count;
			result.z = UnityEngine.Random.Range(0f - PACK_Z_VARIATION_MAG, PACK_Z_VARIATION_MAG);
		}
		if (useVisiblePacksOnly && m_parent.SelectedBundleFeaturesDustJar())
		{
			result = m_leavingSoonBone.transform.localPosition;
		}
		if (column % 2 == 0)
		{
			result.y += 0.03f;
		}
		return result;
	}

	private int DeterminePackColumn(int packNumber, bool forceLastColumn = false)
	{
		if (forceLastColumn)
		{
			return m_packStacks.Count - 1;
		}
		double num = new System.Random(PACK_STACK_SEED + packNumber).NextDouble();
		double num2 = 0.0;
		float num3 = 1f / (float)m_packStacks.Count;
		int i;
		for (i = 0; i < m_packStacks.Count - 1; i++)
		{
			num2 += (double)num3;
			if (num <= num2)
			{
				break;
			}
		}
		return i;
	}

	private void FlyLeavingSoonBanner(int numPacksFlyingIn, int numPacksFlyingOut, float flyInTime, float flyOutTime, float flyInDelay, float flyOutDelay, int numVisiblePacks, bool animated)
	{
		foreach (AnimatedLeavingSoonSign showingLeavingSoonSign in m_showingLeavingSoonSigns)
		{
			if (animated)
			{
				showingLeavingSoonSign.FlyOut(flyOutTime, 0f);
			}
			else
			{
				showingLeavingSoonSign.FlyOutImmediate();
			}
		}
		foreach (AnimatedLeavingSoonSign item in m_showingLeavingSoonSigns.FindAll((AnimatedLeavingSoonSign l) => l.GetState() == AnimatedLowPolyPack.State.HIDDEN))
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		m_showingLeavingSoonSigns.RemoveAll((AnimatedLeavingSoonSign l) => l.GetState() == AnimatedLowPolyPack.State.HIDDEN);
		if (string.IsNullOrEmpty(m_leavingSoonBannerPrefab))
		{
			return;
		}
		BoosterDbfRecord boosterRecord = GameDbf.Booster.GetRecord(m_parent.GetStorePackId().Id);
		if (boosterRecord == null || !boosterRecord.LeavingSoon)
		{
			return;
		}
		AnimatedLeavingSoonSign animatedLeavingSoonSign = GameUtils.LoadGameObjectWithComponent<AnimatedLeavingSoonSign>(m_leavingSoonBannerPrefab);
		if (animatedLeavingSoonSign == null)
		{
			return;
		}
		if (animatedLeavingSoonSign.m_leavingSoonButton != null)
		{
			animatedLeavingSoonSign.m_leavingSoonButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				OnLeavingSoonButtonClicked(boosterRecord.LeavingSoonText);
			});
		}
		animatedLeavingSoonSign.m_isLeavingSoonBanner = true;
		SetupLowPolyPack(animatedLeavingSoonSign, numVisiblePacks, useVisiblePacksOnly: true);
		m_showingLeavingSoonSigns.Add(animatedLeavingSoonSign);
		if (animated)
		{
			animatedLeavingSoonSign.FlyIn(flyInTime, flyInDelay * (float)numPacksFlyingIn);
		}
		else
		{
			animatedLeavingSoonSign.FlyInImmediate();
		}
	}

	private void OnLeavingSoonButtonClicked(string leavingSoonText)
	{
		DialogManager.Get().ShowPopup(new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_STORE_EXPANSION_LEAVING_SOON"),
			m_text = leavingSoonText,
			m_showAlertIcon = true,
			m_responseDisplay = AlertPopup.ResponseDisplay.OK
		});
	}

	private Vector3 GetRewardLocalPos()
	{
		return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
		{
			PC = new Vector3(2.4f, 55f, 306.2f),
			Phone = new Vector3(2.42f, 422.45f, 275f)
		};
	}

	private Vector3 GetRewardScale()
	{
		return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
		{
			PC = new Vector3(41f, 41f, 41f),
			Phone = new Vector3(14f, 14f, 14f)
		};
	}

	private Vector3 GetRewardPunchScale()
	{
		return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
		{
			PC = new Vector3(41.2f, 41.2f, 41.2f),
			Phone = new Vector3(14.2f, 14.2f, 14.2f)
		};
	}

	private void OnRewardShown(object callbackData)
	{
		Reward reward = callbackData as Reward;
		if (!(reward == null))
		{
			reward.RegisterClickListener(OnRewardClicked);
			reward.EnableClickCatcher(enabled: true);
			CardRewardData cardRewardData = reward.Data as CardRewardData;
			if (cardRewardData != null)
			{
				TAG_CLASS @class = DefLoader.Get().GetEntityDef(cardRewardData.CardID).GetClass();
				NotificationManager.Get().PlayBundleInnkeeperLineForClass(@class);
			}
		}
	}

	private void OnRewardClicked(Reward reward, object userData)
	{
		reward.RemoveClickListener(OnRewardClicked);
		reward.Hide(animate: true);
		((GeneralStorePacksPane)((GeneralStore)StoreManager.Get().GetCurrentStore()).GetCurrentPane()).RemoveFirstPurchaseBundle(0f);
	}
}
