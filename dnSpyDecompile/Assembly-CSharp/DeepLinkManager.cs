using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Core;
using Hearthstone;
using Hearthstone.Core.Deeplinking;
using Hearthstone.DataModels;
using PegasusShared;
using PegasusUtil;

// Token: 0x02000893 RID: 2195
public class DeepLinkManager
{
	// Token: 0x0600787C RID: 30844 RVA: 0x00274FB0 File Offset: 0x002731B0
	public static void TryExecuteDeepLinkOnStartup(bool fromUnpause)
	{
		DeepLinkManager.DeepLinkSource source = DeepLinkManager.DeepLinkSource.NONE;
		string[] array = null;
		DeeplinkService deeplinkService;
		if (HearthstoneServices.TryGet<DeeplinkService>(out deeplinkService))
		{
			array = deeplinkService.GetDeeplink();
		}
		else
		{
			Log.DeepLink.PrintError("Could not get deeplink service!", Array.Empty<object>());
		}
		if (array != null && array.Length != 0 && array[0] != string.Empty)
		{
			source = DeepLinkManager.DeepLinkSource.PUSH_NOTIFICATION;
		}
		else
		{
			string[] commandLineArgs = HearthstoneApplication.CommandLineArgs;
			for (int i = 0; i < commandLineArgs.Length; i++)
			{
				if (commandLineArgs[i] == "--mode")
				{
					int start;
					i = (start = i + 1);
					while (i < commandLineArgs.Length && !commandLineArgs[i].StartsWith("-"))
					{
						i++;
					}
					array = commandLineArgs.Slice(start, i);
					source = DeepLinkManager.DeepLinkSource.COMMAND_LINE_ARGUMENTS;
					break;
				}
			}
		}
		Log.All.PrintDebug("Trying to execute deeplink '{0}' from source '{1}' (unpause:{2})", new object[]
		{
			(array != null) ? string.Join(" ", array) : "null",
			source.ToString(),
			fromUnpause
		});
		if (array == null || array.Length == 0)
		{
			if (!fromUnpause && SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				return;
			}
		}
		else
		{
			if (SetRotationManager.ShouldShowSetRotationIntro())
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", array), source.ToString(), false);
				return;
			}
			if (FiresideGatheringManager.Get() != null && FiresideGatheringManager.Get().IsCheckedIn)
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", array), source.ToString(), false);
				return;
			}
			if (!DeepLinkManager.modeMapping.ContainsKey(array[0]))
			{
				if (!fromUnpause && SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
				{
					SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				}
				TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", array), source.ToString(), false);
				return;
			}
			if (!DeepLinkManager.ExecuteDeepLink(array, source, fromUnpause))
			{
				TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", array), source.ToString(), false);
			}
		}
	}

	// Token: 0x0600787D RID: 30845 RVA: 0x002751D0 File Offset: 0x002733D0
	public static bool ExecuteDeepLink(string[] deepLink, DeepLinkManager.DeepLinkSource source, bool fromUnpause)
	{
		if (deepLink == null || deepLink.Length == 0)
		{
			return false;
		}
		string text = deepLink[0];
		uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
		Action action;
		if (num <= 1706796520U)
		{
			if (num <= 1207278369U)
			{
				if (num <= 273829568U)
				{
					if (num != 181018389U)
					{
						if (num != 273829568U)
						{
							return false;
						}
						if (!(text == "packopening"))
						{
							return false;
						}
						goto IL_250;
					}
					else if (!(text == "tavernbrawl"))
					{
						return false;
					}
				}
				else if (num != 927282899U)
				{
					if (num != 1207278369U)
					{
						return false;
					}
					if (!(text == "battlegrounds"))
					{
						return false;
					}
					action = DeepLinkManager.ShowBattlegrounds(deepLink, source);
					goto IL_2A2;
				}
				else if (!(text == "tb"))
				{
					return false;
				}
				action = DeepLinkManager.ShowTavernBrawl(deepLink, source);
				goto IL_2A2;
			}
			if (num <= 1392454286U)
			{
				if (num != 1358361813U)
				{
					if (num != 1392454286U)
					{
						return false;
					}
					if (!(text == "raf"))
					{
						return false;
					}
					goto IL_282;
				}
				else if (!(text == "credits"))
				{
					return false;
				}
			}
			else if (num != 1680451373U)
			{
				if (num != 1706796520U)
				{
					return false;
				}
				if (!(text == "arena"))
				{
					return false;
				}
				action = DeepLinkManager.ShowArena(deepLink, source);
				goto IL_2A2;
			}
			else if (!(text == "cm"))
			{
				return false;
			}
		}
		else if (num <= 3306152395U)
		{
			if (num <= 2144929856U)
			{
				if (num != 2041830488U)
				{
					if (num != 2144929856U)
					{
						return false;
					}
					if (!(text == "collectionmanager"))
					{
						return false;
					}
				}
				else
				{
					if (!(text == "recruitafriend"))
					{
						return false;
					}
					goto IL_282;
				}
			}
			else if (num != 3268139107U)
			{
				if (num != 3306152395U)
				{
					return false;
				}
				if (!(text == "adventure"))
				{
					return false;
				}
				action = DeepLinkManager.ShowAdventure(deepLink, source);
				goto IL_2A2;
			}
			else if (!(text == "play"))
			{
				return false;
			}
		}
		else if (num <= 3520440507U)
		{
			if (num != 3353852974U)
			{
				if (num != 3520440507U)
				{
					return false;
				}
				if (!(text == "fsg"))
				{
					return false;
				}
			}
			else
			{
				if (!(text == "store"))
				{
					return false;
				}
				action = DeepLinkManager.ShowStore(deepLink, source);
				goto IL_2A2;
			}
		}
		else if (num != 3723353184U)
		{
			if (num != 3834471660U)
			{
				return false;
			}
			if (!(text == "hub"))
			{
				return false;
			}
		}
		else
		{
			if (!(text == "gamemode"))
			{
				return false;
			}
			action = DeepLinkManager.ShowGameMode(deepLink, source);
			goto IL_2A2;
		}
		IL_250:
		action = DeepLinkManager.ShowSceneMode(deepLink, source);
		goto IL_2A2;
		IL_282:
		action = DeepLinkManager.ShowRecruitAFriend(deepLink, source);
		IL_2A2:
		if (action != null)
		{
			DeepLinkManager.GoToMode(action, deepLink, source, fromUnpause);
		}
		return true;
	}

	// Token: 0x0600787E RID: 30846 RVA: 0x0027548C File Offset: 0x0027368C
	private static void GoToMode(Action modeDelegate, string[] deepLink, DeepLinkManager.DeepLinkSource source, bool fromUnpause)
	{
		if (!fromUnpause && SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(DeepLinkManager.OnSceneLoaded), modeDelegate);
			return;
		}
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.HUB)
		{
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), false);
			return;
		}
		if (!SceneMgr.Get().IsTransitioning())
		{
			modeDelegate();
			return;
		}
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(DeepLinkManager.OnSceneLoaded), modeDelegate);
	}

	// Token: 0x0600787F RID: 30847 RVA: 0x00275529 File Offset: 0x00273729
	private static Action ShowSceneMode(string[] deepLink, DeepLinkManager.DeepLinkSource source)
	{
		return delegate()
		{
			SceneMgr.Get().SetNextMode(DeepLinkManager.modeMapping[deepLink[0]], SceneMgr.TransitionHandlerType.SCENEMGR, null);
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), true);
		};
	}

	// Token: 0x06007880 RID: 30848 RVA: 0x0027554C File Offset: 0x0027374C
	private static Action ShowStore(string[] deepLink, DeepLinkManager.DeepLinkSource source)
	{
		DeepLinkManager.<>c__DisplayClass7_0 CS$<>8__locals1 = new DeepLinkManager.<>c__DisplayClass7_0();
		CS$<>8__locals1.deepLink = deepLink;
		CS$<>8__locals1.source = source;
		if (StoreManager.Get().IsVintageStoreEnabled())
		{
			GeneralStoreMode mode = GeneralStoreMode.CARDS;
			if (CS$<>8__locals1.deepLink.Length > 1)
			{
				string str = CS$<>8__locals1.deepLink[1];
				AdventureDbId adventureDbId = EnumUtils.SafeParse<AdventureDbId>(str, AdventureDbId.INVALID, true);
				HeroDbId heroDbId = EnumUtils.SafeParse<HeroDbId>(str, HeroDbId.INVALID, true);
				int num;
				StorePackType val;
				DeepLinkManager.GetBoosterAndStorePackTypeFromGameAction(CS$<>8__locals1.deepLink, out num, out val);
				if (num != 0)
				{
					Options.Get().SetInt(Option.LAST_SELECTED_STORE_BOOSTER_ID, num);
					Options.Get().SetInt(Option.LAST_SELECTED_STORE_PACK_TYPE, (int)val);
					mode = GeneralStoreMode.CARDS;
				}
				else if (heroDbId != HeroDbId.INVALID)
				{
					Options.Get().SetInt(Option.LAST_SELECTED_STORE_HERO_ID, (int)heroDbId);
					mode = GeneralStoreMode.HEROES;
				}
				else if (adventureDbId != AdventureDbId.INVALID)
				{
					Options.Get().SetInt(Option.LAST_SELECTED_STORE_ADVENTURE_ID, (int)adventureDbId);
					mode = GeneralStoreMode.ADVENTURE;
				}
			}
			return delegate()
			{
				StoreManager.Get().StartGeneralTransaction(mode);
				TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", CS$<>8__locals1.deepLink), CS$<>8__locals1.source.ToString(), true);
			};
		}
		long pmtProductId = 0L;
		if (CS$<>8__locals1.deepLink.Length > 1)
		{
			long.TryParse(CS$<>8__locals1.deepLink[1], out pmtProductId);
			if (CS$<>8__locals1.deepLink.Length > 2 && CS$<>8__locals1.deepLink[2].ToLowerInvariant() != "pmt")
			{
				StorePackId storePackId;
				DeepLinkManager.GetBoosterAndStorePackTypeFromGameAction(CS$<>8__locals1.deepLink, out storePackId.Id, out storePackId.Type);
				ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(storePackId);
				int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(storePackId, 0);
				IEnumerable<Network.Bundle> allBundlesForProduct = StoreManager.Get().GetAllBundlesForProduct(productTypeFromStorePackType, false, productDataFromStorePackId, 0, true);
				List<ProductTierDataModel> tiers = StoreManager.Get().Catalog.Tiers;
				Network.Bundle bundle = allBundlesForProduct.FirstOrDefault(delegate(Network.Bundle b)
				{
					Func<ShopBrowserButtonDataModel, bool> <>9__4;
					return tiers.Any(delegate(ProductTierDataModel t)
					{
						IEnumerable<ShopBrowserButtonDataModel> browserButtons = t.BrowserButtons;
						Func<ShopBrowserButtonDataModel, bool> predicate;
						if ((predicate = <>9__4) == null)
						{
							predicate = (<>9__4 = ((ShopBrowserButtonDataModel btn) => btn.DisplayProduct.PmtId == b.PMTProductID.Value));
						}
						return browserButtons.Any(predicate);
					});
				});
				pmtProductId = ((bundle != null) ? bundle.PMTProductID.Value : 0L);
			}
		}
		return delegate()
		{
			Shop.OpenToProductPageWhenReady(pmtProductId, false);
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", CS$<>8__locals1.deepLink), CS$<>8__locals1.source.ToString(), true);
		};
	}

	// Token: 0x06007881 RID: 30849 RVA: 0x00275764 File Offset: 0x00273964
	private static Action ShowAdventure(string[] deepLink, DeepLinkManager.DeepLinkSource source)
	{
		AdventureDbId adventureId = AdventureDbId.INVALID;
		AdventureModeDbId adventureModeId = AdventureModeDbId.LINEAR;
		if (deepLink.Length > 1)
		{
			string str = deepLink[1];
			adventureId = EnumUtils.SafeParse<AdventureDbId>(str, AdventureDbId.INVALID, true);
			adventureModeId = AdventureModeDbId.LINEAR;
			if (deepLink.Length > 2)
			{
				string str2 = deepLink[2];
				adventureModeId = EnumUtils.SafeParse<AdventureModeDbId>(str2, AdventureModeDbId.LINEAR, true);
			}
		}
		return delegate()
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.ADVENTURE, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			if (adventureId != AdventureDbId.INVALID)
			{
				AdventureConfig.Get().SetSelectedAdventureMode(adventureId, adventureModeId);
			}
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), true);
		};
	}

	// Token: 0x06007882 RID: 30850 RVA: 0x002757EA File Offset: 0x002739EA
	private static Action ShowTavernBrawl(string[] deepLink, DeepLinkManager.DeepLinkSource source)
	{
		return delegate()
		{
			if (!TavernBrawlManager.Get().HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL) || !TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
			{
				TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), false);
				return;
			}
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.TAVERN_BRAWL, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), true);
		};
	}

	// Token: 0x06007883 RID: 30851 RVA: 0x0027580A File Offset: 0x00273A0A
	private static Action ShowArena(string[] deepLink, DeepLinkManager.DeepLinkSource source)
	{
		return delegate()
		{
			if (AchieveManager.Get() != null && AchieveManager.Get().HasUnlockedArena() && AchieveManager.Get() != null && HealthyGamingMgr.Get().isArenaEnabled())
			{
				AchieveManager.Get().NotifyOfClick(Achievement.ClickTriggerType.BUTTON_ARENA);
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.DRAFT, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), true);
				return;
			}
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), false);
		};
	}

	// Token: 0x06007884 RID: 30852 RVA: 0x0027582A File Offset: 0x00273A2A
	private static Action ShowRecruitAFriend(string[] deepLink, DeepLinkManager.DeepLinkSource source)
	{
		return delegate()
		{
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), true);
			RAFManager.Get().ShowRAFFrame();
		};
	}

	// Token: 0x06007885 RID: 30853 RVA: 0x0027584A File Offset: 0x00273A4A
	private static Action ShowBattlegrounds(string[] deepLink, DeepLinkManager.DeepLinkSource source)
	{
		return delegate()
		{
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), true);
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.BACON, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		};
	}

	// Token: 0x06007886 RID: 30854 RVA: 0x0027586A File Offset: 0x00273A6A
	private static Action ShowGameMode(string[] deepLink, DeepLinkManager.DeepLinkSource source)
	{
		return delegate()
		{
			TelemetryManager.Client().SendDeepLinkExecuted(string.Join(" ", deepLink), source.ToString(), true);
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAME_MODE, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		};
	}

	// Token: 0x06007887 RID: 30855 RVA: 0x0027588A File Offset: 0x00273A8A
	private static void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode != SceneMgr.Mode.HUB)
		{
			return;
		}
		((Action)userData)();
		SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(DeepLinkManager.OnSceneLoaded), userData);
	}

	// Token: 0x06007888 RID: 30856 RVA: 0x002758B4 File Offset: 0x00273AB4
	public static void GetBoosterAndStorePackTypeFromGameAction(string[] actionTokens, out int boosterId, out StorePackType storePackType)
	{
		string text = actionTokens[1];
		storePackType = StorePackType.BOOSTER;
		if (actionTokens.Length > 2)
		{
			storePackType = EnumUtils.SafeParse<StorePackType>(actionTokens[2], StorePackType.BOOSTER, true);
		}
		boosterId = ((storePackType == StorePackType.MODULAR_BUNDLE) ? int.Parse(text) : ((int)EnumUtils.SafeParse<BoosterDbId>(text, BoosterDbId.INVALID, true)));
	}

	// Token: 0x04005E0D RID: 24077
	public const string URI_SCHEME = "hearthstone://";

	// Token: 0x04005E0E RID: 24078
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

	// Token: 0x020024EE RID: 9454
	public enum DeepLinkSource
	{
		// Token: 0x0400EC1E RID: 60446
		NONE,
		// Token: 0x0400EC1F RID: 60447
		PUSH_NOTIFICATION,
		// Token: 0x0400EC20 RID: 60448
		COMMAND_LINE_ARGUMENTS,
		// Token: 0x0400EC21 RID: 60449
		INNKEEPERS_SPECIAL
	}
}
