using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x02000700 RID: 1792
[CustomEditClass]
public class GeneralStorePacksContentDisplay : MonoBehaviour
{
	// Token: 0x0600644C RID: 25676 RVA: 0x0020BCBA File Offset: 0x00209EBA
	public void SetParent(GeneralStorePacksContent parent)
	{
		this.m_parent = parent;
	}

	// Token: 0x0600644D RID: 25677 RVA: 0x0020BCC3 File Offset: 0x00209EC3
	public GeneralStorePacksContent GetParent()
	{
		return this.m_parent;
	}

	// Token: 0x0600644E RID: 25678 RVA: 0x0020BCCB File Offset: 0x00209ECB
	private void OnDestroy()
	{
		AssetHandle.SafeDispose<Texture>(ref this.m_packBackgroundTexture);
		AssetHandle.SafeDispose<Material>(ref this.m_packBackgroundMaterial);
	}

	// Token: 0x0600644F RID: 25679 RVA: 0x0020BCE4 File Offset: 0x00209EE4
	public int ShowPacks(int numVisiblePacks, float flyInTime, float flyOutTime, float flyInDelay, float flyOutDelay, bool forceImmediate = false, bool showAsSingleStack = false)
	{
		if (showAsSingleStack)
		{
			return this.ShowPacksAsSingleStack(numVisiblePacks, flyInTime, flyOutTime, flyInDelay, flyOutDelay, forceImmediate);
		}
		this.m_packDisplayType = GeneralStorePacksContentDisplay.PACK_DISPLAY_TYPE.PACK;
		bool flag = this.m_parent.IsContentActive();
		AnimatedLowPolyPack[] array = this.ConfigureAndGetCurrentPackLayout(this.m_parent.GetStorePackId(), numVisiblePacks);
		if (array.Length != 0 && array[0] != null)
		{
			array[0].HideBanner();
		}
		if (this.m_lastVisiblePacks == numVisiblePacks)
		{
			return 0;
		}
		int num = 0;
		for (int i = array.Length - 1; i >= numVisiblePacks; i--)
		{
			AnimatedLowPolyPack animatedLowPolyPack = array[i];
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
		int num2 = 0;
		for (int j = 0; j < numVisiblePacks; j++)
		{
			AnimatedLowPolyPack animatedLowPolyPack2 = array[j];
			if (flag && !forceImmediate)
			{
				if (animatedLowPolyPack2.FlyIn(flyInTime, flyInDelay * (float)num2))
				{
					num2++;
				}
			}
			else
			{
				animatedLowPolyPack2.FlyInImmediate();
			}
		}
		this.FlyLeavingSoonBanner(num2, num, flyInTime, flyOutTime, flyInDelay, flyOutDelay, numVisiblePacks, flag && !forceImmediate);
		this.m_lastVisiblePacks = numVisiblePacks;
		if (num2 > num)
		{
			return num2;
		}
		return -num;
	}

	// Token: 0x06006450 RID: 25680 RVA: 0x0020BDF0 File Offset: 0x00209FF0
	public int ShowModularBundle(ModularBundleDbfRecord modularBundleRecord, bool forceImmediate, out float delay, out int weight, out ModularBundleNodeLayout prevLayout, int selectedIndex = 0)
	{
		List<ModularBundleLayoutDbfRecord> regionNodeLayoutsForBundle = StoreManager.Get().GetRegionNodeLayoutsForBundle(modularBundleRecord.ID);
		if (selectedIndex >= regionNodeLayoutsForBundle.Count)
		{
			Log.Store.PrintWarning(string.Format("Selected invalid sub-bundle at index={0}. Using sub-bundle at index=0", selectedIndex), Array.Empty<object>());
			selectedIndex = 0;
		}
		ModularBundleLayoutDbfRecord currentLayoutRecord = regionNodeLayoutsForBundle[selectedIndex];
		prevLayout = ((this.m_showingModularBundleNodeLayouts.Count > 0) ? this.m_showingModularBundleNodeLayouts[0] : null);
		if (prevLayout != null && currentLayoutRecord.ID == prevLayout.LayoutID)
		{
			weight = 0;
			delay = 0f;
			this.m_parent.DoneAnimatingPacks();
			return 0;
		}
		List<ModularBundleLayoutNodeDbfRecord> records = GameDbf.ModularBundleLayoutNode.GetRecords((ModularBundleLayoutNodeDbfRecord r) => r.NodeLayoutId == currentLayoutRecord.ID, -1);
		records.Sort((ModularBundleLayoutNodeDbfRecord l, ModularBundleLayoutNodeDbfRecord r) => l.NodeIndex.CompareTo(r.NodeIndex));
		weight = 0;
		int num = 0;
		foreach (ModularBundleLayoutNodeDbfRecord modularBundleLayoutNodeDbfRecord in records)
		{
			if (modularBundleLayoutNodeDbfRecord.ShakeWeight > 0)
			{
				num++;
				weight += modularBundleLayoutNodeDbfRecord.ShakeWeight;
			}
		}
		if (this.m_loadingModularBundle)
		{
			delay = 0f;
			return 0;
		}
		this.m_loadingModularBundle = true;
		delay = (float)currentLayoutRecord.StoreShakeDelay;
		ModularBundleNodeLayout.NodeCallbackData nodeCallbackData = new ModularBundleNodeLayout.NodeCallbackData(currentLayoutRecord.ID, records, currentLayoutRecord.Prefab, forceImmediate);
		if (prevLayout != null)
		{
			prevLayout.PlayExitAnimationsInSequence(forceImmediate, new ModularBundleNodeLayout.OnModularBundleAnimationsFinished(this.OnPreviousModularBundleFinishAnimating), nodeCallbackData);
			int outAnimWeight = 0;
			prevLayout.Nodes.ForEach(delegate(ModularBundleNode n)
			{
				outAnimWeight += n.GetNodeShakeWeight();
			});
			this.m_parent.ShakeStore(prevLayout.Nodes.Count, 10f, 0f, 0f, outAnimWeight);
		}
		else
		{
			this.OnPreviousModularBundleFinishAnimating(nodeCallbackData);
		}
		return num;
	}

	// Token: 0x06006451 RID: 25681 RVA: 0x0020C018 File Offset: 0x0020A218
	private void OnPreviousModularBundleFinishAnimating(object callbackData)
	{
		ModularBundleNodeLayout.NodeCallbackData nodeCallbackData = (ModularBundleNodeLayout.NodeCallbackData)callbackData;
		AssetLoader.Get().InstantiatePrefab(new AssetReference(nodeCallbackData.prefab), new PrefabCallback<GameObject>(this.OnModularBundleNodeLayoutLoaded), nodeCallbackData, AssetLoadingOptions.None);
	}

	// Token: 0x06006452 RID: 25682 RVA: 0x0020C058 File Offset: 0x0020A258
	private void OnModularBundleNodeLayoutLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		ModularBundleNodeLayout.NodeCallbackData nodeCallbackData = (ModularBundleNodeLayout.NodeCallbackData)callbackData;
		if (go == null || !go.activeInHierarchy)
		{
			this.m_loadingModularBundle = false;
			this.m_parent.DoneAnimatingPacks();
			return;
		}
		this.ClearContents();
		ModularBundleNodeLayout component = go.GetComponent<ModularBundleNodeLayout>();
		if (component == null)
		{
			this.m_loadingModularBundle = false;
			this.m_parent.DoneAnimatingPacks();
			return;
		}
		GameUtils.SetParent(component, this.m_nodeLayoutBone, true);
		component.Initialize(this, nodeCallbackData.layoutId, nodeCallbackData.layoutNodes);
		this.m_showingModularBundleNodeLayouts.Add(component);
		component.PlayEntranceAnimationsInSequence(nodeCallbackData.forceImmediate, new ModularBundleNodeLayout.OnModularBundleAnimationsFinished(this.OnModularBundleDoneAnimatingIn), null);
		this.m_loadingModularBundle = false;
	}

	// Token: 0x06006453 RID: 25683 RVA: 0x0020C105 File Offset: 0x0020A305
	private void OnModularBundleDoneAnimatingIn(object callbackData)
	{
		this.m_parent.DoneAnimatingPacks();
	}

	// Token: 0x06006454 RID: 25684 RVA: 0x0020C112 File Offset: 0x0020A312
	public IEnumerator ShowDustJar(int dustAmount, int dustAmountBonus, bool prePurchase, StorePackId selectedStorePackId)
	{
		if (this.m_dustJar == null || this.m_dustAmountText == null)
		{
			yield break;
		}
		this.m_dustJar.SetActive(true);
		if (prePurchase)
		{
			TransformUtil.AttachAndPreserveLocalTransform(this.m_dustJar.transform, this.m_prePurchaseDustJarBone.transform);
		}
		else if (GameUtils.IsHiddenLicenseBundleBooster(selectedStorePackId))
		{
			TransformUtil.AttachAndPreserveLocalTransform(this.m_dustJar.transform, this.m_bundleDustJarBone.transform);
		}
		else
		{
			TransformUtil.AttachAndPreserveLocalTransform(this.m_dustJar.transform, this.m_dustJarBone.transform);
		}
		if (dustAmount == this.m_lastVisibleDust && dustAmountBonus == this.m_lastVisibleDustBonus)
		{
			this.UpdateDustJarAmountText(dustAmount, dustAmountBonus);
			yield break;
		}
		if (this.m_dustJarFlashing)
		{
			this.UpdateDustJarAmountText(this.m_lastVisibleDust, this.m_lastVisibleDustBonus);
		}
		this.m_lastVisibleDust = dustAmount;
		this.m_lastVisibleDustBonus = dustAmountBonus;
		if (this.m_jarFlash != null && this.m_jarFlashAnimController != null)
		{
			this.m_jarFlash.SetActive(false);
			this.m_jarFlash.SetActive(true);
			this.m_jarFlashAnimController.enabled = true;
			this.m_jarFlashAnimController.StopPlayback();
			yield return new WaitForEndOfFrame();
			if (this.m_jarFlashAnimController == null)
			{
				yield break;
			}
			this.m_jarFlashAnimController.Play("Flash");
			this.m_dustJarFlashing = true;
			while (this.m_jarFlashAnimController != null && this.m_jarFlashAnimController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
			{
				yield return null;
			}
		}
		this.UpdateDustJarAmountText(dustAmount, dustAmountBonus);
		this.m_dustJarFlashing = false;
		yield break;
	}

	// Token: 0x06006455 RID: 25685 RVA: 0x0020C140 File Offset: 0x0020A340
	private void UpdateDustJarAmountText(int dustAmount, int dustAmountBonus)
	{
		if (dustAmountBonus > 0)
		{
			this.m_dustAmountText.FontSize = this.m_dustAmountTextFontSizeForBonus;
			this.m_dustAmountText.Text = GameStrings.Format("GLUE_CHINA_STORE_DUST_PLUS_BONUS", new object[]
			{
				dustAmount,
				dustAmountBonus
			});
			return;
		}
		this.m_dustAmountText.FontSize = this.m_dustAmountTextFontSize;
		this.m_dustAmountText.Text = dustAmount.ToString();
	}

	// Token: 0x06006456 RID: 25686 RVA: 0x0020C1B3 File Offset: 0x0020A3B3
	public void HideDustJar()
	{
		if (this.m_dustJar == null)
		{
			return;
		}
		this.m_dustJar.SetActive(false);
	}

	// Token: 0x06006457 RID: 25687 RVA: 0x0020C1D0 File Offset: 0x0020A3D0
	public void ShowGiftDescription(int dustAmount, int dustBonusAmount, bool prePurchase, StorePackId selectedStorePackId)
	{
		if (this.m_giftDescription == null || this.m_giftDescriptionText == null || this.m_firstPurchaseBundleGiftDescription == null || this.m_prePurchaseGiftDescriptionBone == null || this.m_giftDescriptionBone == null || this.m_hiddenLicenseBundleGiftDescription == null)
		{
			return;
		}
		if (GameUtils.IsFirstPurchaseBundleBooster(selectedStorePackId))
		{
			this.m_giftDescription.SetActive(false);
			this.m_hiddenLicenseBundleGiftDescription.SetActive(false);
			this.m_firstPurchaseBundleGiftDescription.SetActive(true);
		}
		else if (GameUtils.IsHiddenLicenseBundleBooster(selectedStorePackId))
		{
			this.m_giftDescription.SetActive(false);
			this.m_firstPurchaseBundleGiftDescription.SetActive(false);
			this.m_hiddenLicenseBundleGiftDescription.SetActive(true);
		}
		else
		{
			this.m_giftDescription.SetActive(true);
			this.m_firstPurchaseBundleGiftDescription.SetActive(false);
			this.m_hiddenLicenseBundleGiftDescription.SetActive(false);
			if (prePurchase)
			{
				this.m_giftDescriptionText.Text = GameStrings.Format("GLUE_CHINA_STORE_BOOSTER_GIFT_PREORDER_BONUS", new object[]
				{
					dustAmount
				});
			}
			else if (dustBonusAmount > 0)
			{
				this.m_giftDescriptionText.Text = GameStrings.Format("GLUE_CHINA_STORE_BOOSTER_GIFT_PLUS_BONUS", new object[]
				{
					dustAmount,
					dustBonusAmount
				});
			}
			else
			{
				this.m_giftDescriptionText.Text = GameStrings.Format("GLUE_CHINA_STORE_BOOSTER_GIFT", new object[]
				{
					dustAmount
				});
			}
		}
		if (prePurchase)
		{
			TransformUtil.AttachAndPreserveLocalTransform(this.m_giftDescription.transform, this.m_prePurchaseGiftDescriptionBone.transform);
			return;
		}
		TransformUtil.AttachAndPreserveLocalTransform(this.m_giftDescription.transform, this.m_giftDescriptionBone.transform);
	}

	// Token: 0x06006458 RID: 25688 RVA: 0x0020C374 File Offset: 0x0020A574
	public void HideGiftDescription()
	{
		if (this.m_giftDescription != null)
		{
			this.m_giftDescription.SetActive(false);
		}
		if (this.m_firstPurchaseBundleGiftDescription != null)
		{
			this.m_firstPurchaseBundleGiftDescription.SetActive(false);
		}
		if (this.m_hiddenLicenseBundleGiftDescription != null)
		{
			this.m_hiddenLicenseBundleGiftDescription.SetActive(false);
		}
	}

	// Token: 0x06006459 RID: 25689 RVA: 0x0020C3CF File Offset: 0x0020A5CF
	public void ShowHiddenBundleCard()
	{
		if (this.m_hiddenCard != null)
		{
			this.m_hiddenCard.SetActive(true);
		}
	}

	// Token: 0x0600645A RID: 25690 RVA: 0x0020C3EB File Offset: 0x0020A5EB
	public void HideHiddenBundleCard()
	{
		if (this.m_hiddenCard != null)
		{
			this.m_hiddenCard.SetActive(false);
		}
	}

	// Token: 0x0600645B RID: 25691 RVA: 0x0020C408 File Offset: 0x0020A608
	public int ShowBundleBox(float flyInTime, float flyOutTime, float flyInDelay, float flyOutDelay, float delay = 0f, bool forceImmediate = false)
	{
		Log.Store.Print("ShowBundleBox()", Array.Empty<object>());
		int num = 1;
		if (this.m_lastVisiblePacks == num)
		{
			return 0;
		}
		this.m_packDisplayType = GeneralStorePacksContentDisplay.PACK_DISPLAY_TYPE.BOX;
		bool animated = this.m_parent.IsContentActive();
		AnimatedLowPolyPack[] array = this.ConfigureAndGetCurrentPackLayout(this.m_parent.GetStorePackId(), 1);
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
		this.FlyLeavingSoonBanner(num, 1, flyInTime, flyOutTime, flyInDelay, flyOutDelay, 1, animated);
		this.m_lastVisiblePacks = num;
		return num2;
	}

	// Token: 0x0600645C RID: 25692 RVA: 0x0020C494 File Offset: 0x0020A694
	public void PurchaseBundleBox(CardReward rewardCard)
	{
		AnimatedLowPolyPack[] array = this.ConfigureAndGetCurrentPackLayout(this.m_parent.GetStorePackId(), 1);
		CardRewardData cardRewardData = new CardRewardData();
		if (rewardCard != null)
		{
			cardRewardData = (rewardCard.Data as CardRewardData);
		}
		if (array == null || array.Length < 1)
		{
			Debug.LogWarningFormat("PurchaseBundleBox() didn't caontain any packs for cardID {0}", new object[]
			{
				cardRewardData.CardID
			});
			return;
		}
		AnimatedLowPolyPack animatedLowPolyPack = array[0];
		if (animatedLowPolyPack == null)
		{
			Debug.LogWarningFormat("PurchaseBundleBox() failed to get AnimatedLowPolyPack for cardID {0}", new object[]
			{
				cardRewardData.CardID
			});
			return;
		}
		FirstPurchaseBox firstPurchaseBox = animatedLowPolyPack.GetFirstPurchaseBox();
		if (!(firstPurchaseBox == null))
		{
			firstPurchaseBox.PurchaseBundle(cardRewardData.CardID);
			return;
		}
		if (rewardCard != null)
		{
			rewardCard.transform.localPosition = this.GetRewardLocalPos();
			SceneUtils.SetLayer(rewardCard, GameLayer.PerspectiveUI);
			RewardUtils.ShowReward(UserAttentionBlocker.NONE, rewardCard, true, this.GetRewardPunchScale(), this.GetRewardScale(), new AnimationUtil.DelOnShownWithPunch(this.OnRewardShown), rewardCard);
			return;
		}
		Debug.LogWarning("Null reference on rewardCard object.");
	}

	// Token: 0x0600645D RID: 25693 RVA: 0x0020C588 File Offset: 0x0020A788
	public void UpdatePackType(IStorePackDef packDef)
	{
		this.ClearContents();
		if (this.m_background == null || packDef == null)
		{
			return;
		}
		AssetLoader.Get().LoadAsset<Material>(ref this.m_packBackgroundMaterial, packDef.GetBackgroundMaterial(), AssetLoadingOptions.None);
		if (this.m_packBackgroundMaterial)
		{
			this.m_background.SetMaterial(this.m_packBackgroundMaterial);
		}
		AssetLoader.Get().LoadAsset<Texture>(ref this.m_packBackgroundTexture, packDef.GetBackgroundTexture(), AssetLoadingOptions.None);
		if (this.m_packBackgroundTexture)
		{
			this.m_background.GetMaterial().mainTexture = this.m_packBackgroundTexture;
		}
	}

	// Token: 0x0600645E RID: 25694 RVA: 0x0020C634 File Offset: 0x0020A834
	public void ClearContents()
	{
		foreach (AnimatedLowPolyPack animatedLowPolyPack in this.m_showingPacks)
		{
			UnityEngine.Object.Destroy(animatedLowPolyPack.gameObject);
		}
		this.m_showingPacks.Clear();
		foreach (AnimatedLeavingSoonSign animatedLeavingSoonSign in this.m_showingLeavingSoonSigns)
		{
			UnityEngine.Object.Destroy(animatedLeavingSoonSign.gameObject);
		}
		this.m_showingLeavingSoonSigns.Clear();
		foreach (ModularBundleNodeLayout modularBundleNodeLayout in this.m_showingModularBundleNodeLayouts)
		{
			UnityEngine.Object.Destroy(modularBundleNodeLayout.gameObject);
		}
		this.m_showingModularBundleNodeLayouts.Clear();
		this.m_lastVisiblePacks = 0;
		this.m_lastVisibleDust = 0;
		if (this.m_dustJar != null)
		{
			this.m_dustJar.SetActive(false);
			this.m_dustJarFlashing = false;
		}
		if (this.m_hiddenCard != null)
		{
			this.m_hiddenCard.SetActive(false);
		}
		if (this.m_giftDescription != null)
		{
			this.m_giftDescription.SetActive(false);
		}
		if (this.m_firstPurchaseBundleGiftDescription != null)
		{
			this.m_firstPurchaseBundleGiftDescription.SetActive(false);
		}
		if (this.m_hiddenLicenseBundleGiftDescription != null)
		{
			this.m_hiddenLicenseBundleGiftDescription.SetActive(false);
		}
		this.m_loadingModularBundle = false;
	}

	// Token: 0x0600645F RID: 25695 RVA: 0x0020C7D0 File Offset: 0x0020A9D0
	private int ShowPacksAsSingleStack(int numVisiblePacks, float flyInTime, float flyOutTime, float flyInDelay, float flyOutDelay, bool forceImmediate = false)
	{
		this.m_packDisplayType = GeneralStorePacksContentDisplay.PACK_DISPLAY_TYPE.PACK;
		bool flag = this.m_parent.IsContentActive();
		int id = GameUtils.IsFirstPurchaseBundleBooster(this.m_parent.GetStorePackId()) ? 1 : this.m_parent.GetStorePackId().Id;
		StorePackId storePackId = new StorePackId
		{
			Type = StorePackType.BOOSTER,
			Id = id
		};
		AnimatedLowPolyPack[] array = this.ConfigureAndGetCurrentPackLayout(storePackId, 1);
		this.FlyLeavingSoonBanner(0, 0, flyInTime, flyOutTime, flyInDelay, flyOutDelay, numVisiblePacks, flag && !forceImmediate);
		if (this.m_lastVisiblePacks == 1)
		{
			if (array.Length != 0 && array[0] != null)
			{
				array[0].UpdateBannerCount(numVisiblePacks);
			}
			return 0;
		}
		int num = 0;
		for (int i = array.Length - 1; i >= 1; i--)
		{
			AnimatedLowPolyPack animatedLowPolyPack = array[i];
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
		this.m_lastVisiblePacks = 1;
		return 0;
	}

	// Token: 0x06006460 RID: 25696 RVA: 0x0020C8D4 File Offset: 0x0020AAD4
	private AnimatedLowPolyPack[] ConfigureAndGetCurrentPackLayout(StorePackId storePackId, int count)
	{
		if (count > this.m_showingPacks.Count)
		{
			AnimatedLowPolyPack animatedLowPolyPack = null;
			if (!GeneralStorePacksContentDisplay.s_packTemplates.TryGetValue(storePackId.Id, out animatedLowPolyPack) || !animatedLowPolyPack)
			{
				IStorePackDef storePackDef = this.m_parent.GetStorePackDef(storePackId);
				if (string.IsNullOrEmpty(storePackDef.GetLowPolyPrefab()) && string.IsNullOrEmpty(storePackDef.GetLowPolyDustPrefab()))
				{
					return this.m_showingPacks.ToArray();
				}
				GameObject gameObject;
				if (GameUtils.IsHiddenLicenseBundleBooster(this.m_parent.GetStorePackId()) && !GameUtils.IsFirstPurchaseBundleBooster(this.m_parent.GetStorePackId()) && this.m_parent.SelectedBundleFeaturesDustJar() && !string.IsNullOrEmpty(storePackDef.GetLowPolyDustPrefab()))
				{
					gameObject = AssetLoader.Get().InstantiatePrefab(storePackDef.GetLowPolyDustPrefab(), AssetLoadingOptions.None);
				}
				else
				{
					gameObject = AssetLoader.Get().InstantiatePrefab(storePackDef.GetLowPolyPrefab(), AssetLoadingOptions.None);
				}
				animatedLowPolyPack = gameObject.GetComponent<AnimatedLowPolyPack>();
				GeneralStorePacksContentDisplay.s_packTemplates[storePackId.Id] = animatedLowPolyPack;
				animatedLowPolyPack.gameObject.SetActive(false);
			}
			for (int i = this.m_showingPacks.Count; i < count; i++)
			{
				AnimatedLowPolyPack animatedLowPolyPack2 = UnityEngine.Object.Instantiate<AnimatedLowPolyPack>(animatedLowPolyPack);
				this.SetupLowPolyPack(animatedLowPolyPack2, i, false);
				this.m_showingPacks.Add(animatedLowPolyPack2);
			}
		}
		return this.m_showingPacks.ToArray();
	}

	// Token: 0x06006461 RID: 25697 RVA: 0x0020CA18 File Offset: 0x0020AC18
	private void SetupLowPolyPack(AnimatedLowPolyPack pack, int i, bool useVisiblePacksOnly)
	{
		pack.gameObject.SetActive(true);
		bool forceLastColumn = pack.m_isLeavingSoonBanner && this.m_parent.SelectedBundleFeaturesDustJar();
		int num = this.DeterminePackColumn(i, forceLastColumn);
		GameUtils.SetParent(pack, this.m_packStacks[num], true);
		if (GameUtils.IsHiddenLicenseBundleBooster(this.m_parent.GetStorePackId()) && !GameUtils.IsFirstPurchaseBundleBooster(this.m_parent.GetStorePackId()) && this.m_parent.SelectedBundleFeaturesDustJar())
		{
			pack.transform.localScale = GeneralStorePacksContentDisplay.BOX_BUNDLE_DUST_SCALE;
		}
		else
		{
			pack.transform.localScale = GeneralStorePacksContentDisplay.PACK_SCALE;
		}
		pack.Init(num, this.DeterminePackLocalPos(num, this.m_showingPacks, useVisiblePacksOnly), new Vector3(0f, 3.5f, -0.1f), true, true);
		SceneUtils.SetLayer(pack, this.m_packStacks[num].layer);
		float y = 0f;
		float x = 0f;
		float z = 0f;
		Log.Store.Print("SetupLowPolyPack pack display type: {0}", new object[]
		{
			this.m_packDisplayType
		});
		if (this.m_packDisplayType == GeneralStorePacksContentDisplay.PACK_DISPLAY_TYPE.BOX)
		{
			y = UnityEngine.Random.Range(-this.m_parent.m_BoxYDegreeVariationMag, this.m_parent.m_BoxYDegreeVariationMag);
			x = UnityEngine.Random.Range(-GeneralStorePacksContentDisplay.BOX_FLY_OUT_X_DEG_VARIATION_MAG, GeneralStorePacksContentDisplay.BOX_FLY_OUT_X_DEG_VARIATION_MAG);
			z = UnityEngine.Random.Range(-GeneralStorePacksContentDisplay.BOX_FLY_OUT_Z_DEG_VARIATION_MAG, GeneralStorePacksContentDisplay.BOX_FLY_OUT_Z_DEG_VARIATION_MAG);
		}
		else
		{
			if (pack.m_isLeavingSoonBanner && this.m_parent.SelectedBundleFeaturesDustJar())
			{
				Vector3 vector = new Vector3(this.m_leavingSoonBone.transform.localEulerAngles.x, this.m_leavingSoonBone.transform.localEulerAngles.y, this.m_leavingSoonBone.transform.localEulerAngles.z);
				pack.SetFlyingLocalRotations(vector, vector);
				return;
			}
			if (this.m_packDisplayType == GeneralStorePacksContentDisplay.PACK_DISPLAY_TYPE.PACK)
			{
				y = UnityEngine.Random.Range(-this.m_parent.m_PackYDegreeVariationMag, this.m_parent.m_PackYDegreeVariationMag);
				x = UnityEngine.Random.Range(-GeneralStorePacksContentDisplay.PACK_FLY_OUT_X_DEG_VARIATION_MAG, GeneralStorePacksContentDisplay.PACK_FLY_OUT_X_DEG_VARIATION_MAG);
				z = UnityEngine.Random.Range(-GeneralStorePacksContentDisplay.PACK_FLY_OUT_Z_DEG_VARIATION_MAG, GeneralStorePacksContentDisplay.PACK_FLY_OUT_Z_DEG_VARIATION_MAG);
			}
		}
		Vector3 flyInLocalAngles = new Vector3(0f, y, 0f);
		Vector3 flyOutLocalAngles = new Vector3(x, 0f, z);
		pack.SetFlyingLocalRotations(flyInLocalAngles, flyOutLocalAngles);
	}

	// Token: 0x06006462 RID: 25698 RVA: 0x0020CC58 File Offset: 0x0020AE58
	private Vector3 DeterminePackLocalPos(int column, List<AnimatedLowPolyPack> packs, bool useVisiblePacksOnly)
	{
		List<AnimatedLowPolyPack> list = packs.FindAll((AnimatedLowPolyPack obj) => obj.Column == column && (!useVisiblePacksOnly || obj.GetState() == AnimatedLowPolyPack.State.FLOWN_IN || obj.GetState() == AnimatedLowPolyPack.State.FLYING_IN));
		Vector3 result = Vector3.zero;
		if (this.m_packDisplayType == GeneralStorePacksContentDisplay.PACK_DISPLAY_TYPE.BOX && GameUtils.IsHiddenLicenseBundleBooster(this.m_parent.GetStorePackId()) && !GameUtils.IsFirstPurchaseBundleBooster(this.m_parent.GetStorePackId()) && this.m_parent.SelectedBundleFeaturesDustJar())
		{
			result = new Vector3(-0.06f, 0f, -0.03f);
		}
		else if (this.m_packDisplayType != GeneralStorePacksContentDisplay.PACK_DISPLAY_TYPE.BOX && !GameUtils.IsHiddenLicenseBundleBooster(this.m_parent.GetStorePackId()))
		{
			result.x = UnityEngine.Random.Range(-GeneralStorePacksContentDisplay.PACK_X_VARIATION_MAG, GeneralStorePacksContentDisplay.PACK_X_VARIATION_MAG);
			result.y = GeneralStorePacksContentDisplay.PACK_Y_OFFSET * (float)list.Count;
			result.z = UnityEngine.Random.Range(-GeneralStorePacksContentDisplay.PACK_Z_VARIATION_MAG, GeneralStorePacksContentDisplay.PACK_Z_VARIATION_MAG);
		}
		if (useVisiblePacksOnly && this.m_parent.SelectedBundleFeaturesDustJar())
		{
			result = this.m_leavingSoonBone.transform.localPosition;
		}
		if (column % 2 == 0)
		{
			result.y += 0.03f;
		}
		return result;
	}

	// Token: 0x06006463 RID: 25699 RVA: 0x0020CD84 File Offset: 0x0020AF84
	private int DeterminePackColumn(int packNumber, bool forceLastColumn = false)
	{
		if (forceLastColumn)
		{
			return this.m_packStacks.Count - 1;
		}
		double num = new System.Random(GeneralStorePacksContentDisplay.PACK_STACK_SEED + packNumber).NextDouble();
		double num2 = 0.0;
		float num3 = 1f / (float)this.m_packStacks.Count;
		int i;
		for (i = 0; i < this.m_packStacks.Count - 1; i++)
		{
			num2 += (double)num3;
			if (num <= num2)
			{
				break;
			}
		}
		return i;
	}

	// Token: 0x06006464 RID: 25700 RVA: 0x0020CDF4 File Offset: 0x0020AFF4
	private void FlyLeavingSoonBanner(int numPacksFlyingIn, int numPacksFlyingOut, float flyInTime, float flyOutTime, float flyInDelay, float flyOutDelay, int numVisiblePacks, bool animated)
	{
		foreach (AnimatedLeavingSoonSign animatedLeavingSoonSign in this.m_showingLeavingSoonSigns)
		{
			if (animated)
			{
				animatedLeavingSoonSign.FlyOut(flyOutTime, 0f);
			}
			else
			{
				animatedLeavingSoonSign.FlyOutImmediate();
			}
		}
		foreach (AnimatedLeavingSoonSign animatedLeavingSoonSign2 in this.m_showingLeavingSoonSigns.FindAll((AnimatedLeavingSoonSign l) => l.GetState() == AnimatedLowPolyPack.State.HIDDEN))
		{
			UnityEngine.Object.Destroy(animatedLeavingSoonSign2.gameObject);
		}
		this.m_showingLeavingSoonSigns.RemoveAll((AnimatedLeavingSoonSign l) => l.GetState() == AnimatedLowPolyPack.State.HIDDEN);
		if (string.IsNullOrEmpty(this.m_leavingSoonBannerPrefab))
		{
			return;
		}
		BoosterDbfRecord boosterRecord = GameDbf.Booster.GetRecord(this.m_parent.GetStorePackId().Id);
		if (boosterRecord == null)
		{
			return;
		}
		if (!boosterRecord.LeavingSoon)
		{
			return;
		}
		AnimatedLeavingSoonSign animatedLeavingSoonSign3 = GameUtils.LoadGameObjectWithComponent<AnimatedLeavingSoonSign>(this.m_leavingSoonBannerPrefab);
		if (animatedLeavingSoonSign3 == null)
		{
			return;
		}
		if (animatedLeavingSoonSign3.m_leavingSoonButton != null)
		{
			animatedLeavingSoonSign3.m_leavingSoonButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.OnLeavingSoonButtonClicked(boosterRecord.LeavingSoonText);
			});
		}
		animatedLeavingSoonSign3.m_isLeavingSoonBanner = true;
		this.SetupLowPolyPack(animatedLeavingSoonSign3, numVisiblePacks, true);
		this.m_showingLeavingSoonSigns.Add(animatedLeavingSoonSign3);
		if (animated)
		{
			animatedLeavingSoonSign3.FlyIn(flyInTime, flyInDelay * (float)numPacksFlyingIn);
			return;
		}
		animatedLeavingSoonSign3.FlyInImmediate();
	}

	// Token: 0x06006465 RID: 25701 RVA: 0x0020CFB4 File Offset: 0x0020B1B4
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

	// Token: 0x06006466 RID: 25702 RVA: 0x0020CFEC File Offset: 0x0020B1EC
	private Vector3 GetRewardLocalPos()
	{
		return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
		{
			PC = new Vector3(2.4f, 55f, 306.2f),
			Phone = new Vector3(2.42f, 422.45f, 275f)
		};
	}

	// Token: 0x06006467 RID: 25703 RVA: 0x0020D038 File Offset: 0x0020B238
	private Vector3 GetRewardScale()
	{
		return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
		{
			PC = new Vector3(41f, 41f, 41f),
			Phone = new Vector3(14f, 14f, 14f)
		};
	}

	// Token: 0x06006468 RID: 25704 RVA: 0x0020D084 File Offset: 0x0020B284
	private Vector3 GetRewardPunchScale()
	{
		return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
		{
			PC = new Vector3(41.2f, 41.2f, 41.2f),
			Phone = new Vector3(14.2f, 14.2f, 14.2f)
		};
	}

	// Token: 0x06006469 RID: 25705 RVA: 0x0020D0D0 File Offset: 0x0020B2D0
	private void OnRewardShown(object callbackData)
	{
		Reward reward = callbackData as Reward;
		if (reward == null)
		{
			return;
		}
		reward.RegisterClickListener(new Reward.OnClickedCallback(this.OnRewardClicked));
		reward.EnableClickCatcher(true);
		CardRewardData cardRewardData = reward.Data as CardRewardData;
		if (cardRewardData == null)
		{
			return;
		}
		TAG_CLASS @class = DefLoader.Get().GetEntityDef(cardRewardData.CardID).GetClass();
		NotificationManager.Get().PlayBundleInnkeeperLineForClass(@class);
	}

	// Token: 0x0600646A RID: 25706 RVA: 0x0020D139 File Offset: 0x0020B339
	private void OnRewardClicked(Reward reward, object userData)
	{
		reward.RemoveClickListener(new Reward.OnClickedCallback(this.OnRewardClicked));
		reward.Hide(true);
		((GeneralStorePacksPane)((GeneralStore)StoreManager.Get().GetCurrentStore()).GetCurrentPane()).RemoveFirstPurchaseBundle(0f);
	}

	// Token: 0x0400535B RID: 21339
	public MeshRenderer m_background;

	// Token: 0x0400535C RID: 21340
	public List<GameObject> m_packStacks = new List<GameObject>();

	// Token: 0x0400535D RID: 21341
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_dustJar;

	// Token: 0x0400535E RID: 21342
	public UberText m_dustAmountText;

	// Token: 0x0400535F RID: 21343
	public int m_dustAmountTextFontSize;

	// Token: 0x04005360 RID: 21344
	public int m_dustAmountTextFontSizeForBonus;

	// Token: 0x04005361 RID: 21345
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_hiddenCard;

	// Token: 0x04005362 RID: 21346
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_giftDescription;

	// Token: 0x04005363 RID: 21347
	public UberText m_giftDescriptionText;

	// Token: 0x04005364 RID: 21348
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_firstPurchaseBundleGiftDescription;

	// Token: 0x04005365 RID: 21349
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_hiddenLicenseBundleGiftDescription;

	// Token: 0x04005366 RID: 21350
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_leavingSoonBannerPrefab;

	// Token: 0x04005367 RID: 21351
	public GameObject m_jarFlash;

	// Token: 0x04005368 RID: 21352
	public Animator m_jarFlashAnimController;

	// Token: 0x04005369 RID: 21353
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_dustJarBone;

	// Token: 0x0400536A RID: 21354
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_prePurchaseDustJarBone;

	// Token: 0x0400536B RID: 21355
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_bundleDustJarBone;

	// Token: 0x0400536C RID: 21356
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_giftDescriptionBone;

	// Token: 0x0400536D RID: 21357
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_prePurchaseGiftDescriptionBone;

	// Token: 0x0400536E RID: 21358
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_leavingSoonBone;

	// Token: 0x0400536F RID: 21359
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public GameObject m_nodeLayoutBone;

	// Token: 0x04005370 RID: 21360
	private GeneralStorePacksContentDisplay.PACK_DISPLAY_TYPE m_packDisplayType;

	// Token: 0x04005371 RID: 21361
	private GeneralStorePacksContent m_parent;

	// Token: 0x04005372 RID: 21362
	private List<AnimatedLowPolyPack> m_showingPacks = new List<AnimatedLowPolyPack>();

	// Token: 0x04005373 RID: 21363
	private List<ModularBundleNodeLayout> m_showingModularBundleNodeLayouts = new List<ModularBundleNodeLayout>();

	// Token: 0x04005374 RID: 21364
	private List<AnimatedLeavingSoonSign> m_showingLeavingSoonSigns = new List<AnimatedLeavingSoonSign>();

	// Token: 0x04005375 RID: 21365
	private static readonly Vector3 PACK_SCALE = new Vector3(0.06f, 0.03f, 0.06f);

	// Token: 0x04005376 RID: 21366
	private static readonly Vector3 BOX_BUNDLE_DUST_SCALE = new Vector3(0.045f, 0.03f, 0.045f);

	// Token: 0x04005377 RID: 21367
	private static readonly float PACK_X_VARIATION_MAG = 0.015f;

	// Token: 0x04005378 RID: 21368
	private static readonly float PACK_Y_OFFSET = 0.02f;

	// Token: 0x04005379 RID: 21369
	private static readonly float PACK_Z_VARIATION_MAG = 0.01f;

	// Token: 0x0400537A RID: 21370
	private static readonly float PACK_FLY_OUT_X_DEG_VARIATION_MAG = 10f;

	// Token: 0x0400537B RID: 21371
	private static readonly float PACK_FLY_OUT_Z_DEG_VARIATION_MAG = 10f;

	// Token: 0x0400537C RID: 21372
	private static readonly float BOX_FLY_OUT_X_DEG_VARIATION_MAG = 0f;

	// Token: 0x0400537D RID: 21373
	private static readonly float BOX_FLY_OUT_Z_DEG_VARIATION_MAG = 0f;

	// Token: 0x0400537E RID: 21374
	private static readonly int PACK_STACK_SEED = 2;

	// Token: 0x0400537F RID: 21375
	private int m_lastVisiblePacks;

	// Token: 0x04005380 RID: 21376
	private int m_lastVisibleDust;

	// Token: 0x04005381 RID: 21377
	private int m_lastVisibleDustBonus;

	// Token: 0x04005382 RID: 21378
	private bool m_dustJarFlashing;

	// Token: 0x04005383 RID: 21379
	private bool m_loadingModularBundle;

	// Token: 0x04005384 RID: 21380
	private AssetHandle<Texture> m_packBackgroundTexture;

	// Token: 0x04005385 RID: 21381
	private AssetHandle<Material> m_packBackgroundMaterial;

	// Token: 0x04005386 RID: 21382
	private static Map<int, AnimatedLowPolyPack> s_packTemplates = new Map<int, AnimatedLowPolyPack>();

	// Token: 0x0200228D RID: 8845
	public enum PACK_DISPLAY_TYPE
	{
		// Token: 0x0400E3F3 RID: 58355
		PACK,
		// Token: 0x0400E3F4 RID: 58356
		BOX
	}
}
