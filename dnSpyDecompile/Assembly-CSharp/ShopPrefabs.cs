using System;

// Token: 0x0200071B RID: 1819
public static class ShopPrefabs
{
	// Token: 0x04005411 RID: 21521
	public static readonly PlatformDependentValue<string> ShopPrefab = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "StoreMain.prefab:1bfa916ba1d116f47807925d1a5321d3",
		Phone = "StoreMain_phone.prefab:3afade7b120704c499aa45de0dc70291"
	};

	// Token: 0x04005412 RID: 21522
	public static readonly PlatformDependentValue<string> ShopPurchaseAuthPrefab = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "StorePurchaseAuth.prefab:3db3bb65b4e2e7748a203863d709157e",
		Phone = "StorePurchaseAuth_phone.prefab:d21a7bb17839bea4cad93f64db6772d6"
	};

	// Token: 0x04005413 RID: 21523
	public static readonly PlatformDependentValue<string> ShopSummaryPrefab = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "StoreSummary.prefab:d311dc2ce685bd1499344f10d0d4a3ad",
		Phone = "StoreSummary_phone.prefab:9ff86c97e26dc034ab784eb0ca1be2a1"
	};

	// Token: 0x04005414 RID: 21524
	public static readonly PlatformDependentValue<string> ShopSendToBamPrefab = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "StoreSendToBAM.prefab:d518a59d394e8cf429cd22c56dd5540d",
		Phone = "StoreSendToBAM_phone.prefab:657c0c2da643428489b3e9bfea31c64e"
	};

	// Token: 0x04005415 RID: 21525
	public static readonly PlatformDependentValue<string> ShopDoneWithBamPrefab = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "StoreDoneWithBAM.prefab:788bb1051374f0d4abe3653a6e0e6a79",
		Phone = "StoreDoneWithBAM_phone.prefab:3956c6496dc9ed547a23cad0c35e525d"
	};

	// Token: 0x04005416 RID: 21526
	public static readonly PlatformDependentValue<string> ShopChallengePromptPrefab = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "StoreChallengePrompt.prefab:43f02a51d311c214aa25232228ccefef",
		Phone = "StoreChallengePrompt_phone.prefab:d628cee1b223c2c45865431bb91efbcb"
	};

	// Token: 0x04005417 RID: 21527
	public static readonly PlatformDependentValue<string> ShopLegalBamLinksPrefab = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "StoreLegalBAMLinks.prefab:8794b810f4d30dd488d6428ed4abf91b",
		Phone = "StoreLegalBAMLinks_phone.prefab:c8aa876947d605f4eb913c727f9a3b91"
	};

	// Token: 0x04005418 RID: 21528
	public static readonly PlatformDependentValue<string> ArenaShopPrefab = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "ArenaStoreWidget.prefab:4013cd2053eeb714283f4ddfc31cf7c5",
		Phone = "ArenaStoreWidget.prefab:4013cd2053eeb714283f4ddfc31cf7c5"
	};

	// Token: 0x04005419 RID: 21529
	public static readonly PlatformDependentValue<string> AdventureShopPrefab = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "AdventureStoreWidget.prefab:259c31c29e64a6f4797a3eff2cbbe59a",
		Phone = "AdventureStoreWidget.prefab:259c31c29e64a6f4797a3eff2cbbe59a"
	};

	// Token: 0x0400541A RID: 21530
	public static readonly PlatformDependentValue<string> TavernBrawlShopPrefab = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "TavernBrawlStoreWidget_PC.prefab:4a4d9f5e4ca129442adf3886a858aa1a",
		Phone = "TavernBrawlStoreWidget_phone.prefab:8cfdf5429dcf2784796ec59b7bbb9c21"
	};

	// Token: 0x0400541B RID: 21531
	public const string AdventureWingShopWidgetPrefab = "AdventureStorymodeChapterStore.prefab:b797807e5c127af47badd08be121ea16";

	// Token: 0x0400541C RID: 21532
	public const string AdventureFullShopWidgetPrefab = "AdventureStorymodeBookStore.prefab:922203a90d48c1d47b2f6813ff72f160";
}
