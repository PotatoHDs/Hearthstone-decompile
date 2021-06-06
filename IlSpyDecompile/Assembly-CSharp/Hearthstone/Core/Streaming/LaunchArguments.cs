using System;
using System.Collections.Generic;
using Blizzard.T5.Core;

namespace Hearthstone.Core.Streaming
{
	public class LaunchArguments
	{
		private enum LaunchArgumentType
		{
			Pipeline,
			Token,
			Version,
			DOP,
			Env,
			StartLocale,
			TDK,
			ContentStackEnv,
			Logs,
			StopDownloadAfter,
			Config,
			UnsaveCfg,
			BaseVersion
		}

		private const string CLEAR_OPTION_VAL = "#";

		private static Dictionary<LaunchArgumentType, string> Arguments = new Dictionary<LaunchArgumentType, string>
		{
			{
				LaunchArgumentType.Pipeline,
				"pipeline"
			},
			{
				LaunchArgumentType.Token,
				"token"
			},
			{
				LaunchArgumentType.Version,
				"version_string"
			},
			{
				LaunchArgumentType.DOP,
				"dop"
			},
			{
				LaunchArgumentType.Env,
				"env"
			},
			{
				LaunchArgumentType.StartLocale,
				"start_locale"
			},
			{
				LaunchArgumentType.TDK,
				"tdk"
			},
			{
				LaunchArgumentType.ContentStackEnv,
				"cenv"
			},
			{
				LaunchArgumentType.Logs,
				"logs"
			},
			{
				LaunchArgumentType.StopDownloadAfter,
				"stopafter"
			},
			{
				LaunchArgumentType.Config,
				"cfg"
			},
			{
				LaunchArgumentType.UnsaveCfg,
				"un"
			},
			{
				LaunchArgumentType.BaseVersion,
				"base"
			}
		};

		public static void ReadLaunchArgumentsFromDeeplink()
		{
			string[] array = MobileCallbackManager.ConsumeDeepLink(retain: true);
			Log.Downloader.PrintInfo("Retreived deeplink: {0}", (array != null) ? string.Join(" ", array) : "");
			bool saveClientConfig = true;
			bool flag = false;
			if (array != null)
			{
				Map<string, string> deepLinkArgs = DeeplinkUtils.GetDeepLinkArgs(array);
				if (deepLinkArgs.ContainsKey(Arguments[LaunchArgumentType.UnsaveCfg]))
				{
					saveClientConfig = false;
				}
				if (deepLinkArgs.TryGetValue(Arguments[LaunchArgumentType.Pipeline], out var value2) && value2.Equals(VersionPipeline.LIVE.ToString()))
				{
					Log.Downloader.PrintInfo("Live pipeline is overridden through proxy service");
					Vars.Key("Mobile.LiveOverride").Set("1", permanent: false);
				}
				if (deepLinkArgs.TryGetValue(Arguments[LaunchArgumentType.Version], out var value3))
				{
					if (!value3.Equals("#"))
					{
						Vars.Key("Aurora.Version.Source").Set("string", saveClientConfig);
					}
					else
					{
						Vars.Key("Aurora.Version.Source").Clear();
					}
					SetClientConfig(value3, "Aurora.Version.String", saveClientConfig);
					flag = true;
				}
				if (deepLinkArgs.ContainsKey(Arguments[LaunchArgumentType.DOP]))
				{
					Log.Downloader.PrintInfo("DOP is on from deeplink args");
					Vars.Key("Aurora.Version.Source").Set("product", saveClientConfig);
					flag = true;
				}
				if (deepLinkArgs.TryGetValue(Arguments[LaunchArgumentType.Env], out var value4))
				{
					if (!value4.Equals("#"))
					{
						Vars.Key("Aurora.Env.Override").Set("1", saveClientConfig);
					}
					else
					{
						Vars.Key("Aurora.Env.Override").Clear();
					}
					SetClientConfig(value4, "Aurora.Env", saveClientConfig);
					flag = true;
				}
				if (deepLinkArgs.TryGetValue(Arguments[LaunchArgumentType.StartLocale], out var value5))
				{
					Locale locale;
					try
					{
						locale = (Locale)Enum.Parse(typeof(Locale), value5);
						Log.Downloader.PrintInfo("Setting locale override from deeplink args {0}", value5);
					}
					catch (ArgumentException ex)
					{
						Log.Downloader.PrintError("Invalid locale from deeplink args {0}", value5, ex);
						locale = Locale.enUS;
					}
					Localization.SetLocale(locale);
				}
				if (deepLinkArgs.TryGetValue(Arguments[LaunchArgumentType.TDK], out var value6))
				{
					if (!value6.Equals("#"))
					{
						value6 = $"https://{value6}-in.tdk.blizzard.net";
					}
					SetClientConfig(value6, "Telemetry.Host", saveClientConfig);
					flag = true;
				}
				if (deepLinkArgs.TryGetValue(Arguments[LaunchArgumentType.ContentStackEnv], out var value7))
				{
					SetClientConfig(value7, "ContentStack.Env", saveClientConfig);
					flag = true;
				}
				if (deepLinkArgs.TryGetValue(Arguments[LaunchArgumentType.StopDownloadAfter], out var value8))
				{
					SetClientConfig(value8, "Mobile.StopDownloadAfter", saveClientConfig);
					flag = true;
				}
				if (deepLinkArgs.TryGetValue(Arguments[LaunchArgumentType.BaseVersion], out var value9))
				{
					SetClientConfig(value9, "Mobile.BinaryVersion", saveClientConfig);
					flag = true;
				}
				if (deepLinkArgs.TryGetValue(Arguments[LaunchArgumentType.Logs], out var value10))
				{
					if (!value10.Equals("#"))
					{
						Log.Downloader.PrintInfo("Setting logs override from deeplink args {0}", value10);
						AddEnabledLogInOptions(value10);
						if (saveClientConfig)
						{
							Options.Get().SetString(Option.ENABLED_LOG_LIST, value10);
						}
					}
					else
					{
						Log.Downloader.PrintInfo("Setting logs override has been cleared");
						Options.Get().DeleteOption(Option.ENABLED_LOG_LIST);
					}
				}
				if (deepLinkArgs.TryGetValue(Arguments[LaunchArgumentType.Config], out var value11))
				{
					Log.Downloader.PrintInfo("Setting additional config override from deeplink args {0}", value11);
					ProcessConfigArgument(value11, delegate(string key, string value)
					{
						SetClientConfig(value, key, saveClientConfig);
					}, delegate(string line)
					{
						SetOptionsTxt(line, saveClientConfig);
					});
					flag = true;
				}
			}
			if (saveClientConfig && flag)
			{
				Vars.SaveConfig();
			}
		}

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
			logList.Split(',').ForEach(delegate(string l)
			{
				Log.Get().AddLogInfo(l);
			});
		}

		public static void ProcessConfigArgument(string cfgString, Action<string, string> clientConfigCallback, Action<string> optionsCallback)
		{
			string[] array = cfgString.Split(',');
			foreach (string text in array)
			{
				int num = text.IndexOf(".");
				if (num < 0)
				{
					continue;
				}
				string text2 = text.Substring(0, num).Trim().ToLower();
				string text3 = text.Substring(num + 1);
				switch (text2)
				{
				case "client":
				case "c":
				{
					int num2 = text3.IndexOf("=");
					if (num2 >= 0)
					{
						string arg = text3.Substring(0, num2).Trim();
						string arg2 = text3.Substring(num2 + 1).Trim();
						clientConfigCallback?.Invoke(arg, arg2);
					}
					break;
				}
				case "option":
				case "o":
					optionsCallback?.Invoke(text3);
					break;
				default:
					Log.Downloader.PrintError("Unknown location string '{0}' is used in {1}", text2, cfgString);
					break;
				}
			}
		}

		public static string CreateLaunchArgument()
		{
			string text = $"hearthstone://?{Arguments[LaunchArgumentType.Pipeline]}=";
			VersionConfigurationService versionConfigurationService = HearthstoneServices.Get<VersionConfigurationService>();
			if (versionConfigurationService != null)
			{
				VersionPipeline pipeline = versionConfigurationService.GetPipeline();
				if (pipeline != 0)
				{
					text += pipeline;
				}
				string clientToken = versionConfigurationService.GetClientToken();
				if (!string.IsNullOrEmpty(clientToken))
				{
					text += $"&{Arguments[LaunchArgumentType.Token]}={clientToken}";
				}
			}
			if (Vars.Key("Aurora.Version.Source").GetStr(string.Empty) == "string")
			{
				text += string.Format("&{0}={1}", Arguments[LaunchArgumentType.Version], Vars.Key("Aurora.Version.String").GetStr(string.Empty));
			}
			if (Vars.Key("Aurora.Env.Override").GetInt(0) != 0)
			{
				text += string.Format("&{0}={1}", Arguments[LaunchArgumentType.Env], Vars.Key("Aurora.Env").GetStr(string.Empty));
			}
			text += $"&{Arguments[LaunchArgumentType.StartLocale]}={Localization.GetLocaleName()}";
			string str = Vars.Key("Telemetry.Host").GetStr(string.Empty);
			if (!string.IsNullOrEmpty(str))
			{
				text += string.Format("&{0}={1}", Arguments[LaunchArgumentType.TDK], str.Replace("-in.tdk.blizzard.net", "").Replace("https://", ""));
			}
			string str2 = Vars.Key("ContentStack.Env").GetStr(string.Empty);
			if (!string.IsNullOrEmpty(str2))
			{
				text += $"&{Arguments[LaunchArgumentType.ContentStackEnv]}={str2}";
			}
			return text + string.Format("&{0}={1}", Arguments[LaunchArgumentType.Logs], string.Join(",", Log.Get().GetEnabledLogNames()));
		}

		private static void SetClientConfig(string input, string keyName, bool permenant)
		{
			if (!input.Equals("#"))
			{
				Log.Downloader.PrintInfo("Setting {0} from deeplink args {1}", keyName, input);
				Vars.Key(keyName).Set(input, permenant);
			}
			else
			{
				Log.Downloader.PrintInfo("Setting {0} has been cleared", keyName);
				Vars.Key(keyName).Clear();
			}
		}

		private static void SetOptionsTxt(string input, bool permenant)
		{
			int num = input.IndexOf("=#");
			if (num == -1)
			{
				Log.Downloader.PrintInfo("Option override from deeplink args {0}", input);
				LocalOptions.Get().SetByLine(input, permenant);
			}
			else
			{
				string text = input.Substring(0, num);
				Log.Downloader.PrintInfo("Option '{0}' has been cleared", text);
				Options.Get().DeleteOption(text);
			}
		}
	}
}
