using System;
using Assets;
using UnityEngine;

// Token: 0x020008F6 RID: 2294
public static class OptionsMigration
{
	// Token: 0x06007FAE RID: 32686 RVA: 0x00296900 File Offset: 0x00294B00
	public static bool UpgradeClientOptions()
	{
		int i = Options.Get().GetInt(Option.CLIENT_OPTIONS_VERSION);
		int startingVersion = i;
		if (!Options.Get().HasOption(Option.CLIENT_OPTIONS_VERSION))
		{
			if (!OptionsMigration.UpgradeClientOptions_V2())
			{
				return false;
			}
			i = 2;
		}
		while (i < 4)
		{
			OptionsMigration.UpgradeCallback upgradeCallback;
			if (!OptionsMigration.s_clientUpgradeCallbacks.TryGetValue(i, out upgradeCallback))
			{
				Error.AddDevFatal("OptionsMigration.UpgradeClientOptions() - Current version is {0} and there is no function to upgrade to {1}. Latest is {2}.", new object[]
				{
					i,
					i + 1,
					4
				});
				return false;
			}
			if (!upgradeCallback(startingVersion))
			{
				return false;
			}
			i++;
		}
		return true;
	}

	// Token: 0x06007FAF RID: 32687 RVA: 0x00296987 File Offset: 0x00294B87
	private static bool UpgradeClientOptions_V2()
	{
		Options.Get().SetInt(Option.CLIENT_OPTIONS_VERSION, 2);
		return Options.Get().GetInt(Option.CLIENT_OPTIONS_VERSION) == 2;
	}

	// Token: 0x06007FB0 RID: 32688 RVA: 0x002969A4 File Offset: 0x00294BA4
	private static bool UpgradeClientOptions_V3(int startingVersion)
	{
		Options.Get().SetInt(Option.CLIENT_OPTIONS_VERSION, 3);
		float num = Mathf.Clamp(Options.Get().GetFloat(Option.MUSIC_VOLUME), 0f, 0.5f);
		num /= 0.5f;
		Options.Get().SetFloat(Option.MUSIC_VOLUME, num);
		return Options.Get().GetInt(Option.CLIENT_OPTIONS_VERSION) == 3;
	}

	// Token: 0x06007FB1 RID: 32689 RVA: 0x002969FC File Offset: 0x00294BFC
	private static bool UpgradeClientOptions_V4(int startingVersion)
	{
		Options.Get().SetInt(Option.CLIENT_OPTIONS_VERSION, 4);
		float val = Mathf.Pow(Options.Get().GetFloat(Option.SOUND_VOLUME), 0.5714286f);
		Options.Get().SetFloat(Option.SOUND_VOLUME, val);
		return Options.Get().GetInt(Option.CLIENT_OPTIONS_VERSION) == 4;
	}

	// Token: 0x06007FB2 RID: 32690 RVA: 0x00296A48 File Offset: 0x00294C48
	public static bool UpgradeServerOptions()
	{
		int i = Options.Get().GetInt(Option.SERVER_OPTIONS_VERSION);
		int startingVersion = i;
		if (!Options.Get().HasOption(Option.SERVER_OPTIONS_VERSION))
		{
			if (!OptionsMigration.UpgradeServerOptions_V2())
			{
				return false;
			}
			i = 2;
		}
		while (i < 6)
		{
			OptionsMigration.UpgradeCallback upgradeCallback;
			if (!OptionsMigration.s_serverUpgradeCallbacks.TryGetValue(i, out upgradeCallback))
			{
				Error.AddDevFatal("OptionsMigration.UpgradeServerOptions() - Current version is {0} and there is no function to upgrade to {1}. Latest is {2}.", new object[]
				{
					i,
					i + 1,
					6
				});
				return false;
			}
			if (!upgradeCallback(startingVersion))
			{
				return false;
			}
			i++;
		}
		return true;
	}

	// Token: 0x06007FB3 RID: 32691 RVA: 0x00296AD1 File Offset: 0x00294CD1
	private static bool UpgradeServerOptions_V2()
	{
		Options.Get().SetInt(Option.SERVER_OPTIONS_VERSION, 2);
		return Options.Get().GetInt(Option.SERVER_OPTIONS_VERSION) == 2;
	}

	// Token: 0x06007FB4 RID: 32692 RVA: 0x00296AF0 File Offset: 0x00294CF0
	private static bool UpgradeServerOptions_V3(int startingVersion)
	{
		if (startingVersion != 0)
		{
			bool flag = false;
			if (AchieveManager.Get() != null && AchieveManager.Get().IsReady())
			{
				flag = AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.VANILLA_HEROES);
			}
			else if (Options.Get().GetBool(Option.HAS_SEEN_EXPERT_AI_UNLOCK))
			{
				flag = true;
			}
			if (flag)
			{
				Options.Get().SetBool(Option.HAS_SEEN_UNLOCK_ALL_HEROES_TRANSITION, true);
				Options.Get().SetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_CARD, true);
				Options.Get().SetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_DECK, true);
			}
			if (Options.Get().GetBool(Option.HAS_STARTED_A_DECK))
			{
				Options.Get().SetBool(Option.HAS_REMOVED_CARD_FROM_DECK, true);
			}
		}
		Options.Get().SetInt(Option.SERVER_OPTIONS_VERSION, 3);
		return Options.Get().GetInt(Option.SERVER_OPTIONS_VERSION) == 3;
	}

	// Token: 0x06007FB5 RID: 32693 RVA: 0x00296BAA File Offset: 0x00294DAA
	private static bool UpgradeServerOptions_V4(int startingVersion)
	{
		if (SetRotationManager.HasSeenStandardModeTutorial())
		{
			Options.Get().SetBool(Option.HAS_SEEN_SET_FILTER_TUTORIAL, true);
		}
		Options.Get().SetInt(Option.SERVER_OPTIONS_VERSION, 4);
		return Options.Get().GetInt(Option.SERVER_OPTIONS_VERSION) == 4;
	}

	// Token: 0x06007FB6 RID: 32694 RVA: 0x00296BDF File Offset: 0x00294DDF
	private static bool UpgradeServerOptions_V5(int startingVersion)
	{
		Options.Get().DeleteOption(Option.WHIZBANG_POPUP_COUNTER);
		Options.Get().SetInt(Option.SERVER_OPTIONS_VERSION, 5);
		return Options.Get().GetInt(Option.SERVER_OPTIONS_VERSION) == 5;
	}

	// Token: 0x06007FB7 RID: 32695 RVA: 0x00296C0C File Offset: 0x00294E0C
	private static bool UpgradeServerOptions_V6(int startingVersion)
	{
		Options.Get().DeleteOption(Option.FORMAT_TYPE);
		Options.Get().SetInt(Option.SERVER_OPTIONS_VERSION, 6);
		return Options.Get().GetInt(Option.SERVER_OPTIONS_VERSION) == 6;
	}

	// Token: 0x040066CB RID: 26315
	public const int LATEST_CLIENT_VERSION = 4;

	// Token: 0x040066CC RID: 26316
	public const int LATEST_SERVER_VERSION = 6;

	// Token: 0x040066CD RID: 26317
	private static readonly Map<int, OptionsMigration.UpgradeCallback> s_clientUpgradeCallbacks = new Map<int, OptionsMigration.UpgradeCallback>
	{
		{
			2,
			new OptionsMigration.UpgradeCallback(OptionsMigration.UpgradeClientOptions_V3)
		},
		{
			3,
			new OptionsMigration.UpgradeCallback(OptionsMigration.UpgradeClientOptions_V4)
		}
	};

	// Token: 0x040066CE RID: 26318
	private static readonly Map<int, OptionsMigration.UpgradeCallback> s_serverUpgradeCallbacks = new Map<int, OptionsMigration.UpgradeCallback>
	{
		{
			2,
			new OptionsMigration.UpgradeCallback(OptionsMigration.UpgradeServerOptions_V3)
		},
		{
			3,
			new OptionsMigration.UpgradeCallback(OptionsMigration.UpgradeServerOptions_V4)
		},
		{
			4,
			new OptionsMigration.UpgradeCallback(OptionsMigration.UpgradeServerOptions_V5)
		},
		{
			5,
			new OptionsMigration.UpgradeCallback(OptionsMigration.UpgradeServerOptions_V6)
		}
	};

	// Token: 0x020025B6 RID: 9654
	// (Invoke) Token: 0x0601343A RID: 78906
	private delegate bool UpgradeCallback(int startingVersion);
}
