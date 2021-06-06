using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Core;
using Hearthstone;
using Hearthstone.Core.Deeplinking;
using Hearthstone.DataModels;
using PegasusShared;
using PegasusUtil;

public class DeepLinkManager
{
	public enum DeepLinkSource
	{
		NONE,
		PUSH_NOTIFICATION,
		COMMAND_LINE_ARGUMENTS,
		INNKEEPERS_SPECIAL
	}

	public const string URI_SCHEME = "hearthstone://";

	private static Dictionary<string, SceneMgr.Mode> modeMapping = new Dictionary<string, SceneMgr.Mode>
	{
		{
			"hub",
			SceneMgr.Mode.HUB
		},
		{
			"play",
			SceneMgr.Mode.TOURNAMENT
		},
		{
			"adventure",
			SceneMgr.Mode.ADVENTURE
		},
		{
			"arena",
			SceneMgr.Mode.DRAFT
		},
		{
			"tb",
			SceneMgr.Mode.TAVERN_BRAWL
		},
		{
			"tavernbrawl",
			SceneMgr.Mode.TAVERN_BRAWL
		},
		{
			"packopening",
			SceneMgr.Mode.PACKOPENING
		},
		{
			"cm",
			SceneMgr.Mode.COLLECTIONMANAGER
		},
		{
			"collectionmanager",
			SceneMgr.Mode.COLLECTIONMANAGER
		},
		{
			"credits",
			SceneMgr.Mode.CREDITS
		},
		{
			"store",
			SceneMgr.Mode.HUB
		},
		{
			"fsg",
			SceneMgr.Mode.HUB
		},
		{
			"raf",
			SceneMgr.Mode.HUB
		},
		{
			"recruitafriend",
			SceneMgr.Mode.HUB
		},
		{
			"battlegrounds",
			SceneMgr.Mode.BACON
		},
		{
			"gamemode",
			SceneMgr.Mode.GAME_MODE
		}
	};

	public static void TryExecuteDeepLinkOnStartup(bool fromUnpause)
	{
		DeepLinkSource source = DeepLinkSource.NONE;
		string[] array = null;
		if (HearthstoneServices.TryGet<DeeplinkService>(out var service))
		{
			array = service.GetDeeplink();
		}
		else
		{
			Log.DeepLink.PrintError("Could not get deeplink service!");
		}
		if (array != null && array.Length != 0 && array[0] != string.Empty)
		{
			source = DeepLinkSource.PUSH_NOTIFICATION;
		}
		else
		{
			string[] commandLineArgs = HearthstoneApplication.CommandLineArgs;
			int num = -1;
			for (int i = 0; i < commandLineArgs.Length; i++)
			{
				if (commandLineArgs[i] == "--mode")
				{
					num = ++i;
					for (; i < commandLineArgs.Length && !commandLineArgs[i].StartsWith("-"); i++)
					{
					}
					array = commandLineArgs.Slice(num, i);
					source = DeepLinkSource.COMMAND_LINE_ARGUMENTS;
					break;
				}
			}
		}
		Log.All.PrintDebug("Trying to execute deeplink '{0}' from source '{1}' (unpause:{2})", (array != null) ? string.Join(" ", array) : "null", source.ToString(), fromUnpause);
		if (array == null || array.Length == 0)
		{
			if (!fromUnpause && SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			}
		}
		else if (SetRotationManager.ShouldShowSetRotationIntro())
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", array), source.ToString(), completed: false);
		}
		else if (FiresideGatheringManager.Get() != null && FiresideGatheringManager.Get().IsCheckedIn)
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", array), source.ToString(), completed: false);
		}
		else if (!modeMapping.ContainsKey(array[0]))
		{
			if (!fromUnpause && SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			}
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", array), source.ToString(), completed: false);
		}
		else if (!ExecuteDeepLink(array, source, fromUnpause))
		{
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", array), source.ToString(), completed: false);
		}
	}

	public static bool ExecuteDeepLink(string[] deepLink, DeepLinkSource source, bool fromUnpause)
	{
		if (deepLink == null || deepLink.Length == 0)
		{
			return false;
		}
		Action action = null;
		switch (deepLink[0])
		{
		case "hub":
		case "play":
		case "fsg":
		case "packopening":
		case "cm":
		case "collectionmanager":
		case "credits":
			action = ShowSceneMode(deepLink, source);
			break;
		case "arena":
			action = ShowArena(deepLink, source);
			break;
		case "adventure":
			action = ShowAdventure(deepLink, source);
			break;
		case "tb":
		case "tavernbrawl":
			action = ShowTavernBrawl(deepLink, source);
			break;
		case "store":
			action = ShowStore(deepLink, source);
			break;
		case "raf":
		case "recruitafriend":
			action = ShowRecruitAFriend(deepLink, source);
			break;
		case "battlegrounds":
			action = ShowBattlegrounds(deepLink, source);
			break;
		case "gamemode":
			action = ShowGameMode(deepLink, source);
			break;
		default:
			return false;
		}
		if (action != null)
		{
			GoToMode(action, deepLink, source, fromUnpause);
		}
		return true;
	}

	private static void GoToMode(Action modeDelegate, string[] deepLink, DeepLinkSource source, bool fromUnpause)
	{
		if (!fromUnpause && SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoaded, modeDelegate);
		}
		else if (SceneMgr.Get().GetMode() != SceneMgr.Mode.HUB)
		{
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), completed: false);
		}
		else if (!SceneMgr.Get().IsTransitioning())
		{
			modeDelegate();
		}
		else
		{
			SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoaded, modeDelegate);
		}
	}

	private static Action ShowSceneMode(string[] deepLink, DeepLinkSource source)
	{
		return delegate
		{
			SceneMgr.Get().SetNextMode(modeMapping[deepLink[0]]);
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), completed: true);
		};
	}

	private static Action ShowStore(string[] deepLink, DeepLinkSource source)
	{
		if (StoreManager.Get().IsVintageStoreEnabled())
		{
			GeneralStoreMode mode = GeneralStoreMode.CARDS;
			if (deepLink.Length > 1)
			{
				string str = deepLink[1];
				AdventureDbId adventureDbId = EnumUtils.SafeParse(str, AdventureDbId.INVALID, ignoreCase: true);
				HeroDbId heroDbId = EnumUtils.SafeParse(str, HeroDbId.INVALID, ignoreCase: true);
				GetBoosterAndStorePackTypeFromGameAction(deepLink, out var boosterId, out var storePackType);
				if (boosterId != 0)
				{
					Options.Get().SetInt(Option.LAST_SELECTED_STORE_BOOSTER_ID, boosterId);
					Options.Get().SetInt(Option.LAST_SELECTED_STORE_PACK_TYPE, (int)storePackType);
					mode = GeneralStoreMode.CARDS;
				}
				else if (heroDbId != 0)
				{
					Options.Get().SetInt(Option.LAST_SELECTED_STORE_HERO_ID, (int)heroDbId);
					mode = GeneralStoreMode.HEROES;
				}
				else if (adventureDbId != 0)
				{
					Options.Get().SetInt(Option.LAST_SELECTED_STORE_ADVENTURE_ID, (int)adventureDbId);
					mode = GeneralStoreMode.ADVENTURE;
				}
			}
			return delegate
			{
				StoreManager.Get().StartGeneralTransaction(mode);
				TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), completed: true);
			};
		}
		long pmtProductId = 0L;
		if (deepLink.Length > 1)
		{
			long.TryParse(deepLink[1], out pmtProductId);
			if (deepLink.Length > 2 && deepLink[2].ToLowerInvariant() != "pmt")
			{
				StorePackId storePackId = default(StorePackId);
				GetBoosterAndStorePackTypeFromGameAction(deepLink, out storePackId.Id, out storePackId.Type);
				ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(storePackId);
				int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(storePackId);
				List<Network.Bundle> allBundlesForProduct = StoreManager.Get().GetAllBundlesForProduct(productTypeFromStorePackType, requireRealMoneyOption: false, productDataFromStorePackId);
				List<ProductTierDataModel> tiers = StoreManager.Get().Catalog.Tiers;
				pmtProductId = allBundlesForProduct.FirstOrDefault((Network.Bundle b) => tiers.Any((ProductTierDataModel t) => t.BrowserButtons.Any((ShopBrowserButtonDataModel btn) => btn.DisplayProduct.PmtId == b.PMTProductID.Value)))?.PMTProductID.Value ?? 0;
			}
		}
		return delegate
		{
			Shop.OpenToProductPageWhenReady(pmtProductId, suppressBox: false);
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), completed: true);
		};
	}

	private static Action ShowAdventure(string[] deepLink, DeepLinkSource source)
	{
		AdventureDbId adventureId = AdventureDbId.INVALID;
		AdventureModeDbId adventureModeId = AdventureModeDbId.LINEAR;
		if (deepLink.Length > 1)
		{
			string str = deepLink[1];
			adventureId = EnumUtils.SafeParse(str, AdventureDbId.INVALID, ignoreCase: true);
			adventureModeId = AdventureModeDbId.LINEAR;
			if (deepLink.Length > 2)
			{
				string str2 = deepLink[2];
				adventureModeId = EnumUtils.SafeParse(str2, AdventureModeDbId.LINEAR, ignoreCase: true);
			}
		}
		return delegate
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.ADVENTURE);
			if (adventureId != 0)
			{
				AdventureConfig.Get().SetSelectedAdventureMode(adventureId, adventureModeId);
			}
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), completed: true);
		};
	}

	private static Action ShowTavernBrawl(string[] deepLink, DeepLinkSource source)
	{
		return delegate
		{
			if (!TavernBrawlManager.Get().HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL) || !TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
			{
				TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), completed: false);
			}
			else
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.TAVERN_BRAWL);
				TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), completed: true);
			}
		};
	}

	private static Action ShowArena(string[] deepLink, DeepLinkSource source)
	{
		return delegate
		{
			if (AchieveManager.Get() != null && AchieveManager.Get().HasUnlockedArena() && AchieveManager.Get() != null && HealthyGamingMgr.Get().isArenaEnabled())
			{
				AchieveManager.Get().NotifyOfClick(Achievement.ClickTriggerType.BUTTON_ARENA);
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.DRAFT);
				TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), completed: true);
			}
			else
			{
				TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), completed: false);
			}
		};
	}

	private static Action ShowRecruitAFriend(string[] deepLink, DeepLinkSource source)
	{
		return delegate
		{
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), completed: true);
			RAFManager.Get().ShowRAFFrame();
		};
	}

	private static Action ShowBattlegrounds(string[] deepLink, DeepLinkSource source)
	{
		return delegate
		{
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), completed: true);
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.BACON);
		};
	}

	private static Action ShowGameMode(string[] deepLink, DeepLinkSource source)
	{
		return delegate
		{
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), completed: true);
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAME_MODE);
		};
	}

	private static void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.HUB)
		{
			((Action)userData)();
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnSceneLoaded, userData);
		}
	}

	public static void GetBoosterAndStorePackTypeFromGameAction(string[] actionTokens, out int boosterId, out StorePackType storePackType)
	{
		string text = actionTokens[1];
		storePackType = StorePackType.BOOSTER;
		if (actionTokens.Length > 2)
		{
			storePackType = EnumUtils.SafeParse(actionTokens[2], StorePackType.BOOSTER, ignoreCase: true);
		}
		boosterId = ((storePackType == StorePackType.MODULAR_BUNDLE) ? int.Parse(text) : ((int)EnumUtils.SafeParse(text, BoosterDbId.INVALID, ignoreCase: true)));
	}
}
