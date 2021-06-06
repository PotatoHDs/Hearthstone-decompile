using System;
using System.Collections.Generic;
using System.IO;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core.Deeplinking;
using MiniJSON;

namespace Hearthstone.Core.Streaming
{
	// Token: 0x02001095 RID: 4245
	public class VersionConfigurationService : IService
	{
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600B801 RID: 47105 RVA: 0x003862A0 File Offset: 0x003844A0
		// (set) Token: 0x0600B802 RID: 47106 RVA: 0x003862A8 File Offset: 0x003844A8
		public string TokenOverride { get; set; }

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x0600B803 RID: 47107 RVA: 0x003862B1 File Offset: 0x003844B1
		// (set) Token: 0x0600B804 RID: 47108 RVA: 0x003862B9 File Offset: 0x003844B9
		public VersionPipeline PipelineOverride { get; set; }

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x0600B805 RID: 47109 RVA: 0x003862C2 File Offset: 0x003844C2
		// (set) Token: 0x0600B806 RID: 47110 RVA: 0x003862CA File Offset: 0x003844CA
		public bool IsRequestingData { get; private set; }

		// Token: 0x0600B807 RID: 47111 RVA: 0x003862D3 File Offset: 0x003844D3
		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			if (!VersionConfigurationService.IsEnabledForCurrentPlatform())
			{
				Log.Downloader.PrintDebug("Not using VersionConfig, not used for platform", Array.Empty<object>());
				yield break;
			}
			Log.Downloader.PrintInfo("Initializing VersionConfigurationService", Array.Empty<object>());
			Map<string, string> deepLinkArgs = serviceLocator.Get<DeeplinkService>().GetDeepLinkArgs();
			if (deepLinkArgs.ContainsKey("pipeline"))
			{
				string text = deepLinkArgs["pipeline"];
				VersionPipeline pipelineOverride;
				if (EnumUtils.TryGetEnum<VersionPipeline>(text, out pipelineOverride))
				{
					Log.Downloader.PrintInfo("Setting pipeline override from deeplink args {0}", new object[]
					{
						text
					});
					this.PipelineOverride = pipelineOverride;
				}
				else
				{
					Log.Downloader.PrintInfo("Pipeline override deeplink arg was found but it was invalid: {0}", new object[]
					{
						text
					});
				}
			}
			if (deepLinkArgs.ContainsKey("version_token"))
			{
				string text2 = deepLinkArgs["version_token"];
				if (text2.Length == 36)
				{
					Log.Downloader.PrintInfo("Setting token override from deeplink args {0}", new object[]
					{
						text2
					});
					this.TokenOverride = text2;
				}
				else
				{
					Log.Downloader.PrintInfo("Token override deeplink arg was found but it was invalid: {0}", new object[]
					{
						text2
					});
				}
			}
			yield return new JobDefinition("VersionConfigurationService.LoadTokenConfigFile", this.Job_LoadTokenConfigFile(), Array.Empty<IJobDependency>());
			if (this.IsPipelinePortNeeded())
			{
				this.IsRequestingData = true;
				this.m_portFetchJob = Processor.QueueJob("VersionConfigurationService.GetPipelinePortsFromVersionService", this.Job_GetPipelinePortsFromVersionService(), Array.Empty<IJobDependency>());
				this.m_portFetchJob.AddJobFinishedEventListener(new JobDefinition.JobFinishedEventListener(this.OnPortFetchJobFinished));
			}
			else
			{
				Log.Downloader.PrintDebug("Not fetching pipeline info, not needed", Array.Empty<object>());
			}
			yield break;
		}

		// Token: 0x0600B808 RID: 47112 RVA: 0x003862E9 File Offset: 0x003844E9
		public Type[] GetDependencies()
		{
			return new Type[]
			{
				typeof(DeeplinkService)
			};
		}

		// Token: 0x0600B809 RID: 47113 RVA: 0x003862FE File Offset: 0x003844FE
		public void Shutdown()
		{
			if (this.m_portFetchJob != null)
			{
				this.m_portFetchJob.RemoveJobFinishedEventListener(new JobDefinition.JobFinishedEventListener(this.OnPortFetchJobFinished));
				this.m_portFetchJob = null;
			}
		}

		// Token: 0x0600B80A RID: 47114 RVA: 0x00386326 File Offset: 0x00384526
		public bool IsPortInformationAvailable()
		{
			return this.m_pipelinePortMap.Count > 0;
		}

		// Token: 0x0600B80B RID: 47115 RVA: 0x00386336 File Offset: 0x00384536
		public string GetClientToken()
		{
			if (!string.IsNullOrEmpty(this.TokenOverride))
			{
				return this.TokenOverride;
			}
			return this.m_tokenFromFile;
		}

		// Token: 0x0600B80C RID: 47116 RVA: 0x00386352 File Offset: 0x00384552
		public VersionPipeline GetPipeline()
		{
			if (this.PipelineOverride != VersionPipeline.UNKNOWN)
			{
				return this.PipelineOverride;
			}
			return this.m_pipelineFromFile;
		}

		// Token: 0x0600B80D RID: 47117 RVA: 0x0038636C File Offset: 0x0038456C
		public bool IsPipelinePortNeeded()
		{
			VersionPipeline pipeline = this.GetPipeline();
			if (pipeline == VersionPipeline.LIVE)
			{
				return Vars.Key("Mobile.LiveOverride").GetBool(false);
			}
			return pipeline > VersionPipeline.UNKNOWN;
		}

		// Token: 0x0600B80E RID: 47118 RVA: 0x0038639C File Offset: 0x0038459C
		public int? GetPipelinePort()
		{
			if (!this.IsPipelinePortNeeded())
			{
				Log.Downloader.PrintWarning("Attempted to get port when it is not needed for pipeline {0}", new object[]
				{
					this.GetPipeline()
				});
				return null;
			}
			if (!this.IsPortInformationAvailable())
			{
				Log.Downloader.PrintError("Attempted to get port when information is not ready yet!", Array.Empty<object>());
				return null;
			}
			VersionPipeline pipeline = this.GetPipeline();
			if (this.m_pipelinePortMap.ContainsKey(pipeline))
			{
				return new int?(this.m_pipelinePortMap[pipeline]);
			}
			Log.Downloader.PrintInfo("No port information found for pipeline {0}", new object[]
			{
				pipeline
			});
			return null;
		}

		// Token: 0x0600B80F RID: 47119 RVA: 0x00381FCE File Offset: 0x003801CE
		public static bool IsEnabledForCurrentPlatform()
		{
			return UpdateUtils.AreUpdatesEnabledForCurrentPlatform;
		}

		// Token: 0x0600B810 RID: 47120 RVA: 0x00386454 File Offset: 0x00384654
		private void OnPortFetchJobFinished(JobDefinition job, bool success)
		{
			this.IsRequestingData = false;
			if (!success)
			{
				Log.Downloader.PrintError("Port Fetch Job Failed!", Array.Empty<object>());
			}
			else
			{
				Log.Downloader.PrintInfo("Port Fetch Job Finished!", Array.Empty<object>());
			}
			if (this.m_portFetchJob != null)
			{
				this.m_portFetchJob.RemoveJobFinishedEventListener(new JobDefinition.JobFinishedEventListener(this.OnPortFetchJobFinished));
				this.m_portFetchJob = null;
			}
		}

		// Token: 0x0600B811 RID: 47121 RVA: 0x003864BB File Offset: 0x003846BB
		private IEnumerator<IAsyncJobResult> Job_LoadTokenConfigFile()
		{
			this.ClearLoadedFileValues();
			if (!VersionConfigurationService.IsEnabledForCurrentPlatform())
			{
				Log.Downloader.PrintError("Attempted to load token.config on a platform that does not support it", Array.Empty<object>());
				yield break;
			}
			string text = VersionConfigurationService.ReadTokenDataFromConfigFile();
			if (string.IsNullOrEmpty(text))
			{
				Log.Downloader.PrintInfo("Token information was missing from token.config!", Array.Empty<object>());
				yield break;
			}
			VersionPipeline pipelineFromFile = VersionPipeline.UNKNOWN;
			string empty = string.Empty;
			if (!VersionConfigurationService.TryParseTokenString(text, out pipelineFromFile, out empty))
			{
				Log.Downloader.PrintError("Could not parse token string from token.config: {0}", new object[]
				{
					text
				});
			}
			Log.Downloader.PrintInfo("Succesfully loaded information from token.confg. {0} : {1}", new object[]
			{
				pipelineFromFile.ToString(),
				empty
			});
			this.m_pipelineFromFile = pipelineFromFile;
			this.m_tokenFromFile = empty;
			yield break;
		}

		// Token: 0x0600B812 RID: 47122 RVA: 0x003864CC File Offset: 0x003846CC
		private static bool TryParseTokenString(string tokenString, out VersionPipeline pipeline, out string token)
		{
			pipeline = VersionPipeline.UNKNOWN;
			token = string.Empty;
			string[] array = tokenString.Split(new char[]
			{
				':'
			});
			if (array.Length != 2)
			{
				Log.Downloader.PrintError("Malformed token loaded from token.config: {0}", new object[]
				{
					tokenString
				});
				return false;
			}
			string text = array[0];
			if (!EnumUtils.TryGetEnum<VersionPipeline>(text, out pipeline))
			{
				Log.Downloader.PrintError("Failed to parse pipeline enum: {0}", new object[]
				{
					text
				});
				return false;
			}
			token = array[1];
			if (token.Length != 36)
			{
				Log.Downloader.PrintError("Unexpected length of token guid {0}", new object[]
				{
					token
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600B813 RID: 47123 RVA: 0x0038656B File Offset: 0x0038476B
		private void ClearLoadedFileValues()
		{
			this.m_tokenFromFile = string.Empty;
			this.m_pipelineFromFile = VersionPipeline.UNKNOWN;
		}

		// Token: 0x0600B814 RID: 47124 RVA: 0x00386580 File Offset: 0x00384780
		private static string ReadTokenDataFromConfigFile()
		{
			string tokenConfigPath = Vars.GetTokenConfigPath();
			if (!File.Exists(tokenConfigPath))
			{
				Log.Downloader.PrintInfo("Token file does not exist at path {0}", new object[]
				{
					tokenConfigPath
				});
				return null;
			}
			string result;
			try
			{
				string text = null;
				using (StreamReader streamReader = File.OpenText(tokenConfigPath))
				{
					text = streamReader.ReadLine();
					if (!streamReader.EndOfStream)
					{
						Log.Downloader.PrintWarning("token.config had more lines than expected", Array.Empty<object>());
					}
				}
				result = text;
			}
			catch (Exception ex)
			{
				Log.Downloader.PrintError("Could not read token.config file: {0}", new object[]
				{
					ex.ToString()
				});
				result = null;
			}
			return result;
		}

		// Token: 0x0600B815 RID: 47125 RVA: 0x00386638 File Offset: 0x00384838
		private IEnumerator<IAsyncJobResult> Job_GetPipelinePortsFromVersionService()
		{
			this.m_pipelinePortMap.Clear();
			if (!VersionConfigurationService.IsEnabledForCurrentPlatform() || !this.IsPipelinePortNeeded())
			{
				Log.Downloader.PrintError("Attempted to request pipeline information on a platform that does not support/need it", Array.Empty<object>());
				yield break;
			}
			VersionPipelineJsonFetcher fetcher = new VersionPipelineJsonFetcher();
			yield return fetcher;
			string pipelineJson = fetcher.PipelineJson;
			if (string.IsNullOrEmpty(pipelineJson))
			{
				Log.Downloader.PrintError("Empty response from version proxy service pipeline list!", Array.Empty<object>());
				yield break;
			}
			VersionConfigurationService.AddPipelinesToPortMap(VersionConfigurationService.ParsePipelineJson(pipelineJson), ref this.m_pipelinePortMap);
			yield break;
		}

		// Token: 0x0600B816 RID: 47126 RVA: 0x00386648 File Offset: 0x00384848
		private static List<VersionConfigurationService.JsonPipelineInfo> ParsePipelineJson(string pipelineJsonString)
		{
			List<VersionConfigurationService.JsonPipelineInfo> list = new List<VersionConfigurationService.JsonPipelineInfo>();
			try
			{
				foreach (object obj in (Json.Deserialize(pipelineJsonString) as JsonList))
				{
					VersionConfigurationService.JsonPipelineInfo pipelineInfoFromJsonNode = VersionConfigurationService.GetPipelineInfoFromJsonNode((JsonNode)obj);
					list.Add(pipelineInfoFromJsonNode);
				}
			}
			catch (Exception ex)
			{
				Log.Downloader.PrintError("Could not parse version proxy pipeline json: {0}", new object[]
				{
					ex.ToString()
				});
				return new List<VersionConfigurationService.JsonPipelineInfo>();
			}
			return list;
		}

		// Token: 0x0600B817 RID: 47127 RVA: 0x003866EC File Offset: 0x003848EC
		private static VersionConfigurationService.JsonPipelineInfo GetPipelineInfoFromJsonNode(JsonNode pipeline)
		{
			return new VersionConfigurationService.JsonPipelineInfo
			{
				ProductName = (pipeline["productName"] as string),
				PipelineName = (pipeline["pipelineName"] as string),
				ProxyPort = Convert.ToInt32(pipeline["proxyPort"])
			};
		}

		// Token: 0x0600B818 RID: 47128 RVA: 0x00386748 File Offset: 0x00384948
		private static void AddPipelinesToPortMap(List<VersionConfigurationService.JsonPipelineInfo> pipelineList, ref Map<VersionPipeline, int> pipelineMap)
		{
			if (pipelineMap == null)
			{
				pipelineMap = new Map<VersionPipeline, int>();
			}
			foreach (VersionConfigurationService.JsonPipelineInfo jsonPipelineInfo in pipelineList)
			{
				if (!jsonPipelineInfo.ProductName.Equals("Hearthstone"))
				{
					Log.Downloader.PrintError("Found unexpected product in version proxy pipeline list: {0}", new object[]
					{
						jsonPipelineInfo.ProductName
					});
				}
				else if (!VersionConfigurationService.PIPELINE_VERSION_MAP.ContainsKey(jsonPipelineInfo.PipelineName))
				{
					Log.Downloader.PrintInfo("Found unsupported pipeline {0} in version port json.", new object[]
					{
						jsonPipelineInfo.PipelineName
					});
				}
				else
				{
					VersionPipeline key = VersionConfigurationService.PIPELINE_VERSION_MAP[jsonPipelineInfo.PipelineName];
					Log.Downloader.PrintInfo("Adding port {0} for pipeline {1}", new object[]
					{
						jsonPipelineInfo.ProxyPort,
						key.ToString()
					});
					pipelineMap[key] = jsonPipelineInfo.ProxyPort;
				}
			}
		}

		// Token: 0x0400984F RID: 38991
		private string m_tokenFromFile = string.Empty;

		// Token: 0x04009850 RID: 38992
		private VersionPipeline m_pipelineFromFile;

		// Token: 0x04009851 RID: 38993
		private Map<VersionPipeline, int> m_pipelinePortMap = new Map<VersionPipeline, int>();

		// Token: 0x04009852 RID: 38994
		private JobDefinition m_portFetchJob;

		// Token: 0x04009853 RID: 38995
		private const int EXPECTED_GUID_LENGTH = 36;

		// Token: 0x04009854 RID: 38996
		private const char TOKEN_FILE_SEPERATOR = ':';

		// Token: 0x04009855 RID: 38997
		private const string JSON_PRODUCT_NAME_KEY = "productName";

		// Token: 0x04009856 RID: 38998
		private const string JSON_PIPELINE_NAME_KEY = "pipelineName";

		// Token: 0x04009857 RID: 38999
		private const string JSON_PROXY_PORT_KEY = "proxyPort";

		// Token: 0x04009858 RID: 39000
		private const string HEARTHSTONE_PRODUCT_NAME = "Hearthstone";

		// Token: 0x04009859 RID: 39001
		private static readonly Map<string, VersionPipeline> PIPELINE_VERSION_MAP = new Map<string, VersionPipeline>
		{
			{
				"Hearthstone-Dev",
				VersionPipeline.DEV
			},
			{
				"Hearthstone-External",
				VersionPipeline.EXTERNAL
			},
			{
				"Hearthstone-RC",
				VersionPipeline.RC
			},
			{
				"Hearthstone-Live",
				VersionPipeline.LIVE
			}
		};

		// Token: 0x020028A5 RID: 10405
		private struct JsonPipelineInfo
		{
			// Token: 0x17002D3B RID: 11579
			// (get) Token: 0x06013C64 RID: 80996 RVA: 0x0053C57C File Offset: 0x0053A77C
			// (set) Token: 0x06013C65 RID: 80997 RVA: 0x0053C584 File Offset: 0x0053A784
			public string PipelineName { get; set; }

			// Token: 0x17002D3C RID: 11580
			// (get) Token: 0x06013C66 RID: 80998 RVA: 0x0053C58D File Offset: 0x0053A78D
			// (set) Token: 0x06013C67 RID: 80999 RVA: 0x0053C595 File Offset: 0x0053A795
			public string ProductName { get; set; }

			// Token: 0x17002D3D RID: 11581
			// (get) Token: 0x06013C68 RID: 81000 RVA: 0x0053C59E File Offset: 0x0053A79E
			// (set) Token: 0x06013C69 RID: 81001 RVA: 0x0053C5A6 File Offset: 0x0053A7A6
			public int ProxyPort { get; set; }
		}
	}
}
