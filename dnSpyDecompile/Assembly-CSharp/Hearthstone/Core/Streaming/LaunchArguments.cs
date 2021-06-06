using System;
using System.Collections.Generic;
using Blizzard.T5.Core;

namespace Hearthstone.Core.Streaming
{
	// Token: 0x0200108E RID: 4238
	public class LaunchArguments
	{
		// Token: 0x0600B74F RID: 46927 RVA: 0x0038169C File Offset: 0x0037F89C
		public static void ReadLaunchArgumentsFromDeeplink()
		{
			string[] array = MobileCallbackManager.ConsumeDeepLink(true);
			Log.Downloader.PrintInfo("Retreived deeplink: {0}", new object[]
			{
				(array != null) ? string.Join(" ", array) : ""
			});
			bool saveClientConfig = true;
			bool flag = false;
			if (array != null)
			{
				Map<string, string> deepLinkArgs = DeeplinkUtils.GetDeepLinkArgs(array);
				if (deepLinkArgs.ContainsKey(LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.UnsaveCfg]))
				{
					saveClientConfig = false;
				}
				string text;
				if (deepLinkArgs.TryGetValue(LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.Pipeline], out text) && text.Equals(VersionPipeline.LIVE.ToString()))
				{
					Log.Downloader.PrintInfo("Live pipeline is overridden through proxy service", Array.Empty<object>());
					Vars.Key("Mobile.LiveOverride").Set("1", false);
				}
				string text2;
				if (deepLinkArgs.TryGetValue(LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.Version], out text2))
				{
					if (!text2.Equals("#"))
					{
						Vars.Key("Aurora.Version.Source").Set("string", saveClientConfig);
					}
					else
					{
						Vars.Key("Aurora.Version.Source").Clear();
					}
					LaunchArguments.SetClientConfig(text2, "Aurora.Version.String", saveClientConfig);
					flag = true;
				}
				if (deepLinkArgs.ContainsKey(LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.DOP]))
				{
					Log.Downloader.PrintInfo("DOP is on from deeplink args", Array.Empty<object>());
					Vars.Key("Aurora.Version.Source").Set("product", saveClientConfig);
					flag = true;
				}
				string text3;
				if (deepLinkArgs.TryGetValue(LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.Env], out text3))
				{
					if (!text3.Equals("#"))
					{
						Vars.Key("Aurora.Env.Override").Set("1", saveClientConfig);
					}
					else
					{
						Vars.Key("Aurora.Env.Override").Clear();
					}
					LaunchArguments.SetClientConfig(text3, "Aurora.Env", saveClientConfig);
					flag = true;
				}
				string text4;
				if (deepLinkArgs.TryGetValue(LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.StartLocale], out text4))
				{
					Locale locale;
					try
					{
						locale = (Locale)Enum.Parse(typeof(Locale), text4);
						Log.Downloader.PrintInfo("Setting locale override from deeplink args {0}", new object[]
						{
							text4
						});
					}
					catch (ArgumentException ex)
					{
						Log.Downloader.PrintError("Invalid locale from deeplink args {0}", new object[]
						{
							text4,
							ex
						});
						locale = Locale.enUS;
					}
					Localization.SetLocale(locale);
				}
				string text5;
				if (deepLinkArgs.TryGetValue(LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.TDK], out text5))
				{
					if (!text5.Equals("#"))
					{
						text5 = string.Format("https://{0}-in.tdk.blizzard.net", text5);
					}
					LaunchArguments.SetClientConfig(text5, "Telemetry.Host", saveClientConfig);
					flag = true;
				}
				string input;
				if (deepLinkArgs.TryGetValue(LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.ContentStackEnv], out input))
				{
					LaunchArguments.SetClientConfig(input, "ContentStack.Env", saveClientConfig);
					flag = true;
				}
				string input2;
				if (deepLinkArgs.TryGetValue(LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.StopDownloadAfter], out input2))
				{
					LaunchArguments.SetClientConfig(input2, "Mobile.StopDownloadAfter", saveClientConfig);
					flag = true;
				}
				string input3;
				if (deepLinkArgs.TryGetValue(LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.BaseVersion], out input3))
				{
					LaunchArguments.SetClientConfig(input3, "Mobile.BinaryVersion", saveClientConfig);
					flag = true;
				}
				string text6;
				if (deepLinkArgs.TryGetValue(LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.Logs], out text6))
				{
					if (!text6.Equals("#"))
					{
						Log.Downloader.PrintInfo("Setting logs override from deeplink args {0}", new object[]
						{
							text6
						});
						LaunchArguments.AddEnabledLogInOptions(text6);
						if (saveClientConfig)
						{
							Options.Get().SetString(Option.ENABLED_LOG_LIST, text6);
						}
					}
					else
					{
						Log.Downloader.PrintInfo("Setting logs override has been cleared", Array.Empty<object>());
						Options.Get().DeleteOption(Option.ENABLED_LOG_LIST);
					}
				}
				string text7;
				if (deepLinkArgs.TryGetValue(LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.Config], out text7))
				{
					Log.Downloader.PrintInfo("Setting additional config override from deeplink args {0}", new object[]
					{
						text7
					});
					LaunchArguments.ProcessConfigArgument(text7, delegate(string key, string value)
					{
						LaunchArguments.SetClientConfig(value, key, saveClientConfig);
					}, delegate(string line)
					{
						LaunchArguments.SetOptionsTxt(line, saveClientConfig);
					});
					flag = true;
				}
			}
			if (saveClientConfig && flag)
			{
				Vars.SaveConfig();
			}
		}

		// Token: 0x0600B750 RID: 46928 RVA: 0x00381A98 File Offset: 0x0037FC98
		public static void AddEnabledLogInOptions(string logList)
		{
			if (string.IsNullOrEmpty(logList))
			{
				logList = Options.Get().GetString(Option.ENABLED_LOG_LIST);
				if (string.IsNullOrEmpty(logList))
				{
					return;
				}
			}
			logList.Split(new char[]
			{
				','
			}).ForEach(delegate(string l)
			{
				Log.Get().AddLogInfo(l);
			});
		}

		// Token: 0x0600B751 RID: 46929 RVA: 0x00381AFC File Offset: 0x0037FCFC
		public static void ProcessConfigArgument(string cfgString, Action<string, string> clientConfigCallback, Action<string> optionsCallback)
		{
			foreach (string text in cfgString.Split(new char[]
			{
				','
			}))
			{
				int num = text.IndexOf(".");
				if (num >= 0)
				{
					string text2 = text.Substring(0, num).Trim().ToLower();
					string text3 = text.Substring(num + 1);
					if (text2 == "client" || text2 == "c")
					{
						int num2 = text3.IndexOf("=");
						if (num2 >= 0)
						{
							string arg = text3.Substring(0, num2).Trim();
							string arg2 = text3.Substring(num2 + 1).Trim();
							if (clientConfigCallback != null)
							{
								clientConfigCallback(arg, arg2);
							}
						}
					}
					else if (text2 == "option" || text2 == "o")
					{
						if (optionsCallback != null)
						{
							optionsCallback(text3);
						}
					}
					else
					{
						Log.Downloader.PrintError("Unknown location string '{0}' is used in {1}", new object[]
						{
							text2,
							cfgString
						});
					}
				}
			}
		}

		// Token: 0x0600B752 RID: 46930 RVA: 0x00381C10 File Offset: 0x0037FE10
		public static string CreateLaunchArgument()
		{
			string str = string.Format("hearthstone://?{0}=", LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.Pipeline]);
			VersionConfigurationService versionConfigurationService = HearthstoneServices.Get<VersionConfigurationService>();
			if (versionConfigurationService != null)
			{
				VersionPipeline pipeline = versionConfigurationService.GetPipeline();
				if (pipeline != VersionPipeline.UNKNOWN)
				{
					str += pipeline.ToString();
				}
				string clientToken = versionConfigurationService.GetClientToken();
				if (!string.IsNullOrEmpty(clientToken))
				{
					str += string.Format("&{0}={1}", LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.Token], clientToken);
				}
			}
			if (Vars.Key("Aurora.Version.Source").GetStr(string.Empty) == "string")
			{
				str += string.Format("&{0}={1}", LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.Version], Vars.Key("Aurora.Version.String").GetStr(string.Empty));
			}
			if (Vars.Key("Aurora.Env.Override").GetInt(0) != 0)
			{
				str += string.Format("&{0}={1}", LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.Env], Vars.Key("Aurora.Env").GetStr(string.Empty));
			}
			str += string.Format("&{0}={1}", LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.StartLocale], Localization.GetLocaleName());
			string str2 = Vars.Key("Telemetry.Host").GetStr(string.Empty);
			if (!string.IsNullOrEmpty(str2))
			{
				str += string.Format("&{0}={1}", LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.TDK], str2.Replace("-in.tdk.blizzard.net", "").Replace("https://", ""));
			}
			string str3 = Vars.Key("ContentStack.Env").GetStr(string.Empty);
			if (!string.IsNullOrEmpty(str3))
			{
				str += string.Format("&{0}={1}", LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.ContentStackEnv], str3);
			}
			return str + string.Format("&{0}={1}", LaunchArguments.Arguments[LaunchArguments.LaunchArgumentType.Logs], string.Join(",", Log.Get().GetEnabledLogNames()));
		}

		// Token: 0x0600B753 RID: 46931 RVA: 0x00381E04 File Offset: 0x00380004
		private static void SetClientConfig(string input, string keyName, bool permenant)
		{
			if (!input.Equals("#"))
			{
				Log.Downloader.PrintInfo("Setting {0} from deeplink args {1}", new object[]
				{
					keyName,
					input
				});
				Vars.Key(keyName).Set(input, permenant);
				return;
			}
			Log.Downloader.PrintInfo("Setting {0} has been cleared", new object[]
			{
				keyName
			});
			Vars.Key(keyName).Clear();
		}

		// Token: 0x0600B754 RID: 46932 RVA: 0x00381E70 File Offset: 0x00380070
		private static void SetOptionsTxt(string input, bool permenant)
		{
			int num = input.IndexOf("=#");
			if (num == -1)
			{
				Log.Downloader.PrintInfo("Option override from deeplink args {0}", new object[]
				{
					input
				});
				LocalOptions.Get().SetByLine(input, permenant);
				return;
			}
			string text = input.Substring(0, num);
			Log.Downloader.PrintInfo("Option '{0}' has been cleared", new object[]
			{
				text
			});
			Options.Get().DeleteOption(text);
		}

		// Token: 0x040097F2 RID: 38898
		private const string CLEAR_OPTION_VAL = "#";

		// Token: 0x040097F3 RID: 38899
		private static Dictionary<LaunchArguments.LaunchArgumentType, string> Arguments = new Dictionary<LaunchArguments.LaunchArgumentType, string>
		{
			{
				LaunchArguments.LaunchArgumentType.Pipeline,
				"pipeline"
			},
			{
				LaunchArguments.LaunchArgumentType.Token,
				"token"
			},
			{
				LaunchArguments.LaunchArgumentType.Version,
				"version_string"
			},
			{
				LaunchArguments.LaunchArgumentType.DOP,
				"dop"
			},
			{
				LaunchArguments.LaunchArgumentType.Env,
				"env"
			},
			{
				LaunchArguments.LaunchArgumentType.StartLocale,
				"start_locale"
			},
			{
				LaunchArguments.LaunchArgumentType.TDK,
				"tdk"
			},
			{
				LaunchArguments.LaunchArgumentType.ContentStackEnv,
				"cenv"
			},
			{
				LaunchArguments.LaunchArgumentType.Logs,
				"logs"
			},
			{
				LaunchArguments.LaunchArgumentType.StopDownloadAfter,
				"stopafter"
			},
			{
				LaunchArguments.LaunchArgumentType.Config,
				"cfg"
			},
			{
				LaunchArguments.LaunchArgumentType.UnsaveCfg,
				"un"
			},
			{
				LaunchArguments.LaunchArgumentType.BaseVersion,
				"base"
			}
		};

		// Token: 0x02002896 RID: 10390
		private enum LaunchArgumentType
		{
			// Token: 0x0400FA34 RID: 64052
			Pipeline,
			// Token: 0x0400FA35 RID: 64053
			Token,
			// Token: 0x0400FA36 RID: 64054
			Version,
			// Token: 0x0400FA37 RID: 64055
			DOP,
			// Token: 0x0400FA38 RID: 64056
			Env,
			// Token: 0x0400FA39 RID: 64057
			StartLocale,
			// Token: 0x0400FA3A RID: 64058
			TDK,
			// Token: 0x0400FA3B RID: 64059
			ContentStackEnv,
			// Token: 0x0400FA3C RID: 64060
			Logs,
			// Token: 0x0400FA3D RID: 64061
			StopDownloadAfter,
			// Token: 0x0400FA3E RID: 64062
			Config,
			// Token: 0x0400FA3F RID: 64063
			UnsaveCfg,
			// Token: 0x0400FA40 RID: 64064
			BaseVersion
		}
	}
}
