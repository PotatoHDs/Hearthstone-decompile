using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class BoosterProductPage : ProductPage
{
	[SerializeField]
	private AsyncReference[] m_boosterStackRefs;

	[SerializeField]
	private int m_variableQuantityMax;

	[SerializeField]
	protected float m_packShakeTime = 2f;

	[SerializeField]
	protected float m_packLandShakeDelay = 0.25f;

	[SerializeField]
	protected float m_packLandWeight = 2f;

	[SerializeField]
	protected float m_packLiftWeight = 1f;

	private int m_lastSelectedQuantity;

	private List<BoosterStack> m_boosterStacks;

	private ShakePane m_shakePane;

	private bool m_pendingDistributePacks = true;

	protected override void Awake()
	{
		base.Awake();
		base.OnProductVariantSet += HandleProductVariantSet;
	}

	protected override void Start()
	{
		m_boosterStacks = new List<BoosterStack>(m_boosterStackRefs.Count());
		AsyncReference[] boosterStackRefs = m_boosterStackRefs;
		for (int i = 0; i < boosterStackRefs.Length; i++)
		{
			boosterStackRefs[i].RegisterReadyListener<BoosterStack>(delegate
			{
				if (AreBoosterStacksReady())
				{
					AsyncReference[] boosterStackRefs2 = m_boosterStackRefs;
					foreach (AsyncReference asyncReference in boosterStackRefs2)
					{
						m_boosterStacks.Add(asyncReference.Object as BoosterStack);
					}
				}
			});
		}
		m_shakePane = GetComponentInParent<ShakePane>();
		base.Start();
	}

	protected void Update()
	{
		if (m_pendingDistributePacks && AreBoosterStacksReady() && base.IsOpen && !m_widget.IsChangingStates)
		{
			m_pendingDistributePacks = false;
			DistributeStacks();
		}
	}

	public override void Open()
	{
		SetMusicOverride(MusicPlaylistType.Invalid);
		base.Open();
	}

	protected override void OnProductSet()
	{
		base.OnProductSet();
		RewardItemDataModel rewardItemDataModel = base.Product.Items.FirstOrDefault((RewardItemDataModel item) => item.Booster != null);
		if (rewardItemDataModel == null)
		{
			Log.Store.PrintError("No Boosters in Product \"{0}\"", base.Product.Name);
			return;
		}
		using (AssetHandle<GameObject> assetHandle = ShopUtils.LoadStorePackPrefab(rewardItemDataModel.Booster.Type))
		{
			StorePackDef storePackDef = (assetHandle ? assetHandle.Asset.GetComponent<StorePackDef>() : null);
			SetMusicOverride(storePackDef ? storePackDef.GetPlaylist() : MusicPlaylistType.Invalid);
		}
		foreach (BoosterStack boosterStack in m_boosterStacks)
		{
			boosterStack.SetStacks(0);
		}
		if (m_variableQuantityMax > 0)
		{
			TEST_PopulateVariantsRange(base.Product, 1, m_variableQuantityMax);
		}
		List<IDataModel> list = base.Product.Variants.Cast<IDataModel>().ToList();
		list.Sort(SortProducts);
		int num = list.IndexOf(m_productSelection.Variant);
		if (num < 0)
		{
			num = 0;
		}
		SelectVariant(list.ElementAtOrDefault(num) as ProductDataModel);
	}

	protected void HandleProductVariantSet()
	{
		m_pendingDistributePacks = true;
	}

	protected bool AreBoosterStacksReady()
	{
		return m_boosterStackRefs.All((AsyncReference r) => r.IsReady);
	}

	protected void DistributeStacks()
	{
		ProductDataModel selectedVariant = GetSelectedVariant();
		int num = 0;
		int num2 = ((selectedVariant != null) ? GetBoosterCount(selectedVariant) : 0);
		int num3 = m_boosterStacks.Count();
		int num4 = num2 / num3;
		int num5 = num2 % num3;
		bool flag = num2 > m_lastSelectedQuantity;
		int num6 = (m_lastSelectedQuantity + ((!flag) ? (-1) : 0)) % num3;
		m_lastSelectedQuantity = num2;
		for (int i = 0; i < num3; i++)
		{
			int num7 = (num6 + (flag ? i : (-i)) + num3) % num3;
			bool flag2 = num7 < num5;
			int stackSize = num4 + (flag2 ? 1 : 0);
			BoosterStack boosterStack = m_boosterStacks[num7];
			num += boosterStack.CurrentStackSize;
			boosterStack.StackingDelay = (float)i * boosterStack.StackingBaseDuration / (float)num3;
			boosterStack.SetStacks(stackSize, instantaneous: false);
		}
		if ((bool)m_shakePane)
		{
			int num8 = num2 - num;
			float xRotationAmount = ((num8 > 0) ? ((float)num8 * m_packLandWeight) : ((float)num8 * m_packLiftWeight));
			float delay = ((num8 > 0) ? m_packLandShakeDelay : 0f);
			m_shakePane.Shake(xRotationAmount, m_packShakeTime, delay);
		}
	}

	protected static int GetBoosterCount(ProductDataModel product)
	{
		int num = 0;
		foreach (RewardItemDataModel item in product.Items.Where((RewardItemDataModel i) => i.ItemType == RewardItemType.BOOSTER))
		{
			num += item.Quantity;
		}
		return num;
	}

	protected static int SortProducts(IDataModel A, IDataModel B)
	{
		if (!(A is ProductDataModel) || !(B is ProductDataModel))
		{
			return 0;
		}
		int boosterCount = GetBoosterCount(A as ProductDataModel);
		int boosterCount2 = GetBoosterCount(B as ProductDataModel);
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

	private void TEST_PopulateVariantsRange(ProductDataModel product, int minQuantity = 1, int maxQuantity = 100)
	{
		if (product.Variants.Count >= maxQuantity)
		{
			return;
		}
		ProductDataModel productDataModel = product.Variants.FirstOrDefault();
		RewardItemDataModel rewardItemDataModel = productDataModel.Items.FirstOrDefault((RewardItemDataModel i) => i.Booster != null);
		while (minQuantity <= maxQuantity)
		{
			if (!product.Variants.Any((ProductDataModel p) => GetBoosterCount(p) == minQuantity))
			{
				RewardItemDataModel rewardItemDataModel2 = new RewardItemDataModel();
				rewardItemDataModel2.Booster = rewardItemDataModel.Booster;
				rewardItemDataModel2.ItemId = rewardItemDataModel.ItemId;
				rewardItemDataModel2.ItemType = rewardItemDataModel.ItemType;
				rewardItemDataModel2.Quantity = minQuantity;
				PriceDataModel priceDataModel = new PriceDataModel();
				priceDataModel.Currency = CurrencyType.GOLD;
				priceDataModel.Amount = minQuantity * 100;
				priceDataModel.DisplayText = priceDataModel.Amount.ToString();
				ProductDataModel productDataModel2 = new ProductDataModel();
				productDataModel2.Name = $"TESTDATA {rewardItemDataModel.Booster.Type}x{minQuantity}";
				productDataModel2.Items.Add(rewardItemDataModel2);
				productDataModel2.Prices.Add(priceDataModel);
				productDataModel2.Tags = productDataModel.Tags;
				product.Variants.Add(productDataModel2);
			}
			minQuantity++;
		}
	}
}
