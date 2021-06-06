using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006A2 RID: 1698
public class BoosterProductPage : ProductPage
{
	// Token: 0x06005EC0 RID: 24256 RVA: 0x001ECBF0 File Offset: 0x001EADF0
	protected override void Awake()
	{
		base.Awake();
		base.OnProductVariantSet += this.HandleProductVariantSet;
	}

	// Token: 0x06005EC1 RID: 24257 RVA: 0x001ECC0C File Offset: 0x001EAE0C
	protected override void Start()
	{
		this.m_boosterStacks = new List<BoosterStack>(this.m_boosterStackRefs.Count<AsyncReference>());
		AsyncReference[] boosterStackRefs = this.m_boosterStackRefs;
		for (int i = 0; i < boosterStackRefs.Length; i++)
		{
			boosterStackRefs[i].RegisterReadyListener<BoosterStack>(delegate(BoosterStack stack)
			{
				if (!this.AreBoosterStacksReady())
				{
					return;
				}
				foreach (AsyncReference asyncReference in this.m_boosterStackRefs)
				{
					this.m_boosterStacks.Add(asyncReference.Object as BoosterStack);
				}
			});
		}
		this.m_shakePane = base.GetComponentInParent<ShakePane>();
		base.Start();
	}

	// Token: 0x06005EC2 RID: 24258 RVA: 0x001ECC6A File Offset: 0x001EAE6A
	protected void Update()
	{
		if (this.m_pendingDistributePacks && this.AreBoosterStacksReady() && base.IsOpen && !this.m_widget.IsChangingStates)
		{
			this.m_pendingDistributePacks = false;
			this.DistributeStacks();
		}
	}

	// Token: 0x06005EC3 RID: 24259 RVA: 0x001ECB16 File Offset: 0x001EAD16
	public override void Open()
	{
		base.SetMusicOverride(MusicPlaylistType.Invalid);
		base.Open();
	}

	// Token: 0x06005EC4 RID: 24260 RVA: 0x001ECCA0 File Offset: 0x001EAEA0
	protected override void OnProductSet()
	{
		base.OnProductSet();
		RewardItemDataModel rewardItemDataModel = base.Product.Items.FirstOrDefault((RewardItemDataModel item) => item.Booster != null);
		if (rewardItemDataModel == null)
		{
			Log.Store.PrintError("No Boosters in Product \"{0}\"", new object[]
			{
				base.Product.Name
			});
			return;
		}
		using (AssetHandle<GameObject> assetHandle = ShopUtils.LoadStorePackPrefab(rewardItemDataModel.Booster.Type))
		{
			StorePackDef storePackDef = assetHandle ? assetHandle.Asset.GetComponent<StorePackDef>() : null;
			base.SetMusicOverride(storePackDef ? storePackDef.GetPlaylist() : MusicPlaylistType.Invalid);
		}
		foreach (BoosterStack boosterStack in this.m_boosterStacks)
		{
			boosterStack.SetStacks(0, true);
		}
		if (this.m_variableQuantityMax > 0)
		{
			this.TEST_PopulateVariantsRange(base.Product, 1, this.m_variableQuantityMax);
		}
		List<IDataModel> list = base.Product.Variants.Cast<IDataModel>().ToList<IDataModel>();
		list.Sort(new Comparison<IDataModel>(BoosterProductPage.SortProducts));
		int num = list.IndexOf(this.m_productSelection.Variant);
		if (num < 0)
		{
			num = 0;
		}
		this.SelectVariant(list.ElementAtOrDefault(num) as ProductDataModel);
	}

	// Token: 0x06005EC5 RID: 24261 RVA: 0x001ECE18 File Offset: 0x001EB018
	protected void HandleProductVariantSet()
	{
		this.m_pendingDistributePacks = true;
	}

	// Token: 0x06005EC6 RID: 24262 RVA: 0x001ECE21 File Offset: 0x001EB021
	protected bool AreBoosterStacksReady()
	{
		return this.m_boosterStackRefs.All((AsyncReference r) => r.IsReady);
	}

	// Token: 0x06005EC7 RID: 24263 RVA: 0x001ECE50 File Offset: 0x001EB050
	protected void DistributeStacks()
	{
		ProductDataModel selectedVariant = base.GetSelectedVariant();
		int num = 0;
		int num2 = (selectedVariant != null) ? BoosterProductPage.GetBoosterCount(selectedVariant) : 0;
		int num3 = this.m_boosterStacks.Count<BoosterStack>();
		int num4 = num2 / num3;
		int num5 = num2 % num3;
		bool flag = num2 > this.m_lastSelectedQuantity;
		int num6 = (this.m_lastSelectedQuantity + (flag ? 0 : -1)) % num3;
		this.m_lastSelectedQuantity = num2;
		for (int i = 0; i < num3; i++)
		{
			int num7 = (num6 + (flag ? i : (-i)) + num3) % num3;
			bool flag2 = num7 < num5;
			int stackSize = num4 + (flag2 ? 1 : 0);
			BoosterStack boosterStack = this.m_boosterStacks[num7];
			num += boosterStack.CurrentStackSize;
			boosterStack.StackingDelay = (float)i * boosterStack.StackingBaseDuration / (float)num3;
			boosterStack.SetStacks(stackSize, false);
		}
		if (this.m_shakePane)
		{
			int num8 = num2 - num;
			float xRotationAmount = (num8 > 0) ? ((float)num8 * this.m_packLandWeight) : ((float)num8 * this.m_packLiftWeight);
			float delay = (num8 > 0) ? this.m_packLandShakeDelay : 0f;
			this.m_shakePane.Shake(xRotationAmount, this.m_packShakeTime, delay, 0f);
		}
	}

	// Token: 0x06005EC8 RID: 24264 RVA: 0x001ECF7C File Offset: 0x001EB17C
	protected static int GetBoosterCount(ProductDataModel product)
	{
		int num = 0;
		foreach (RewardItemDataModel rewardItemDataModel in from i in product.Items
		where i.ItemType == RewardItemType.BOOSTER
		select i)
		{
			num += rewardItemDataModel.Quantity;
		}
		return num;
	}

	// Token: 0x06005EC9 RID: 24265 RVA: 0x001ECFF4 File Offset: 0x001EB1F4
	protected static int SortProducts(IDataModel A, IDataModel B)
	{
		if (!(A is ProductDataModel) || !(B is ProductDataModel))
		{
			return 0;
		}
		int boosterCount = BoosterProductPage.GetBoosterCount(A as ProductDataModel);
		int boosterCount2 = BoosterProductPage.GetBoosterCount(B as ProductDataModel);
		if (boosterCount > boosterCount2)
		{
			return 1;
		}
		if (boosterCount < boosterCount2)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06005ECA RID: 24266 RVA: 0x001ED038 File Offset: 0x001EB238
	private void TEST_PopulateVariantsRange(ProductDataModel product, int minQuantity = 1, int maxQuantity = 100)
	{
		if (product.Variants.Count >= maxQuantity)
		{
			return;
		}
		ProductDataModel productDataModel = product.Variants.FirstOrDefault<ProductDataModel>();
		RewardItemDataModel rewardItemDataModel = productDataModel.Items.FirstOrDefault((RewardItemDataModel i) => i.Booster != null);
		while (minQuantity <= maxQuantity)
		{
			if (!product.Variants.Any((ProductDataModel p) => BoosterProductPage.GetBoosterCount(p) == minQuantity))
			{
				RewardItemDataModel rewardItemDataModel2 = new RewardItemDataModel();
				rewardItemDataModel2.Booster = rewardItemDataModel.Booster;
				rewardItemDataModel2.ItemId = rewardItemDataModel.ItemId;
				rewardItemDataModel2.ItemType = rewardItemDataModel.ItemType;
				rewardItemDataModel2.Quantity = minQuantity;
				PriceDataModel priceDataModel = new PriceDataModel();
				priceDataModel.Currency = CurrencyType.GOLD;
				priceDataModel.Amount = (float)(minQuantity * 100);
				priceDataModel.DisplayText = priceDataModel.Amount.ToString();
				ProductDataModel productDataModel2 = new ProductDataModel();
				productDataModel2.Name = string.Format("TESTDATA {0}x{1}", rewardItemDataModel.Booster.Type, minQuantity);
				productDataModel2.Items.Add(rewardItemDataModel2);
				productDataModel2.Prices.Add(priceDataModel);
				productDataModel2.Tags = productDataModel.Tags;
				product.Variants.Add(productDataModel2);
			}
			int quantity = minQuantity;
			minQuantity = quantity + 1;
		}
	}

	// Token: 0x04004FDC RID: 20444
	[SerializeField]
	private AsyncReference[] m_boosterStackRefs;

	// Token: 0x04004FDD RID: 20445
	[SerializeField]
	private int m_variableQuantityMax;

	// Token: 0x04004FDE RID: 20446
	[SerializeField]
	protected float m_packShakeTime = 2f;

	// Token: 0x04004FDF RID: 20447
	[SerializeField]
	protected float m_packLandShakeDelay = 0.25f;

	// Token: 0x04004FE0 RID: 20448
	[SerializeField]
	protected float m_packLandWeight = 2f;

	// Token: 0x04004FE1 RID: 20449
	[SerializeField]
	protected float m_packLiftWeight = 1f;

	// Token: 0x04004FE2 RID: 20450
	private int m_lastSelectedQuantity;

	// Token: 0x04004FE3 RID: 20451
	private List<BoosterStack> m_boosterStacks;

	// Token: 0x04004FE4 RID: 20452
	private ShakePane m_shakePane;

	// Token: 0x04004FE5 RID: 20453
	private bool m_pendingDistributePacks = true;
}
