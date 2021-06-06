using System;
using System.Collections.Generic;
using System.IO;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core.Deeplinking;
using MiniJSON;

namespace Hearthstone.Core.Streaming
{
	public class VersionConfigurationService : IService
	{
		private struct JsonPipelineInfo
		{
			public string PipelineName { get; set; }

			public string ProductName { get; set; }

			public int ProxyPort { get; set; }
		}

		private string m_tokenFromFile = string.Empty;

		private VersionPipeline m_pipelineFromFile;

		private Map<VersionPipeline, int> m_pipelinePortMap = new Map<VersionPipeline, int>();

		private JobDefinition m_portFetchJob;

		private const int EXPECTED_GUID_LENGTH = 36;

		private const char TOKEN_FILE_SEPERATOR = ':';

		private const string JSON_PRODUCT_NAME_KEY = "productName";

		private const string JSON_PIPELINE_NAME_KEY = "pipelineName";

		private const string JSON_PROXY_PORT_KEY = "proxyPort";

		private const string HEARTHSTONE_PRODUCT_NAME = "Hearthstone";

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

		public string TokenOverride { get; set; }

		public VersionPipeline PipelineOverride { get; set; }

		public bool IsRequestingData { get; private set; }

		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			if (!IsEnabledForCurrentPlatform())
			{
				Log.Downloader.PrintDebug("Not using VersionConfig, not used for platform");
				yield break;
			}
			Log.Downloader.PrintInfo("Initializing VersionConfigurationService");
			Map<string, string> deepLinkArgs = serviceLocator.Get<DeeplinkService>().GetDeepLinkArgs();
			if (deepLinkArgs.ContainsKey("pipeline"))
			{
				string text = deepLinkArgs["pipeline"];
				if (EnumUtils.TryGetEnum<VersionPipeline>(text, out var outVal))
				{
					Log.Downloader.PrintInfo("Setting pipeline override from deeplink args {0}", text);
					PipelineOverride = outVal;
				}
				else
				{
					Log.Downloader.PrintInfo("Pipeline override deeplink arg was found but it was invalid: {0}", text);
				}
			}
			if (deepLinkArgs.ContainsKey("version_token"))
			{
				string text2 = deepLinkArgs["version_token"];
				if (text2.Length == 36)
				{
					Log.Downloader.PrintInfo("Setting token override from deeplink args {0}", text2);
					TokenOverride = text2;
				}
				else
				{
					Log.Downloader.PrintInfo("Token override deeplink arg was found but it was invalid: {0}", text2);
				}
			}
			yield return new JobDefinition("VersionConfigurationService.LoadTokenConfigFile", Job_LoadTokenConfigFile());
			if (IsPipelinePortNeeded())
			{
				IsRequestingData = true;
				m_portFetchJob = Processor.QueueJob("VersionConfigurationService.GetPipelinePortsFromVersionService", Job_GetPipelinePortsFromVersionService());
				m_portFetchJob.AddJobFinishedEventListener(OnPortFetchJobFinished);
			}
			else
			{
				Log.Downloader.PrintDebug("Not fetching pipeline info, not needed");
			}
		}

		public Type[] GetDependencies()
		{
			return new Type[1] { typeof(DeeplinkService) };
		}

		public void Shutdown()
		{
			if (m_portFetchJob != null)
			{
				m_portFetchJob.RemoveJobFinishedEventListener(OnPortFetchJobFinished);
				m_portFetchJob = null;
			}
		}

		public bool IsPortInformationAvailable()
		{
			return m_pipelinePortMap.Count > 0;
		}

		public string GetClientToken()
		{
			if (!string.IsNullOrEmpty(TokenOverride))
			{
				return TokenOverride;
			}
			return m_tokenFromFile;
		}

		public VersionPipeline GetPipeline()
		{
			if (PipelineOverride != 0)
			{
				return PipelineOverride;
			}
			return m_pipelineFromFile;
		}

		public bool IsPipelinePortNeeded()
		{
			VersionPipeline pipeline = GetPipeline();
			if (pipeline == VersionPipeline.LIVE)
			{
				return Vars.Key("Mobile.LiveOverride").GetBool(def: false);
			}
			return pipeline != VersionPipeline.UNKNOWN;
		}

		public int? GetPipelinePort()
		{
			if (!IsPipelinePortNeeded())
			{
				Log.Downloader.PrintWarning("Attempted to get port when it is not needed for pipeline {0}", GetPipeline());
				return null;
			}
			if (!IsPortInformationAvailable())
			{
				Log.Downloader.PrintError("Attempted to get port when information is not ready yet!");
				return null;
			}
			VersionPipeline pipeline = GetPipeline();
			if (m_pipelinePortMap.ContainsKey(pipeline))
			{
				return m_pipelinePortMap[pipeline];
			}
			Log.Downloader.PrintInfo("No port information found for pipeline {0}", pipeline);
			return null;
		}

		public static bool IsEnabledForCurrentPlatform()
		{
			return UpdateUtils.AreUpdatesEnabledForCurrentPlatform;
		}

		private void OnPortFetchJobFinished(JobDefinition job, bool success)
		{
			IsRequestingData = false;
			if (!success)
			{
				Log.Downloader.PrintError("Port Fetch Job Failed!");
			}
			else
			{
				Log.Downloader.PrintInfo("Port Fetch Job Finished!");
			}
			if (m_portFetchJob != null)
			{
				m_portFetchJob.RemoveJobFinishedEventListener(OnPortFetchJobFinished);
				m_portFetchJob = null;
			}
		}

		private IEnumerator<IAsyncJobResult> Job_LoadTokenConfigFile()
		{
			ClearLoadedFileValues();
			if (!IsEnabledForCurrentPlatform())
			{
				Log.Downloader.PrintError("Attempted to load token.config on a platform that does not support it");
				yield break;
			}
			string text = ReadTokenDataFromConfigFile();
			if (string.IsNullOrEmpty(text))
			{
				Log.Downloader.PrintInfo("Token information was missing from token.config!");
				yield break;
			}
			VersionPipeline pipeline = VersionPipeline.UNKNOWN;
			string token = string.Empty;
			if (!TryParseTokenString(text, out pipeline, out token))
			{
				Log.Downloader.PrintError("Could not parse token string from token.config: {0}", text);
			}
			Log.Downloader.PrintInfo("Succesfully loaded information from token.confg. {0} : {1}", pipeline.ToString(), token);
			m_pipelineFromFile = pipeline;
			m_tokenFromFile = token;
		}

		private static bool TryParseTokenString(string tokenString, out VersionPipeline pipeline, out string token)
		{
			pipeline = VersionPipeline.UNKNOWN;
			token = string.Empty;
			string[] array = tokenString.Split(':');
			if (array.Length != 2)
			{
				Log.Downloader.PrintError("Malformed token loaded from token.config: {0}", tokenString);
				return false;
			}
			string text = array[0];
			if (!EnumUtils.TryGetEnum<VersionPipeline>(text, out pipeline))
			{
				Log.Downloader.PrintError("Failed to parse pipeline enum: {0}", text);
				return false;
			}
			token = array[1];
			if (token.Length != 36)
			{
				Log.Downloader.PrintError("Unexpected length of token guid {0}", token);
				return false;
			}
			return true;
		}

		private void ClearLoadedFileValues()
		{
			m_tokenFromFile = string.Empty;
			m_pipelineFromFile = VersionPipeline.UNKNOWN;
		}

		private static string ReadTokenDataFromConfigFile()
		{
			string tokenConfigPath = Vars.GetTokenConfigPath();
			if (!File.Exists(tokenConfigPath))
			{
				Log.Downloader.PrintInfo("Token file does not exist at path {0}", tokenConfigPath);
				return null;
			}
			try
			{
				string result = null;
				using (StreamReader streamReader = File.OpenText(tokenConfigPath))
				{
					result = streamReader.ReadLine();
					if (!streamReader.EndOfStream)
					{
						Log.Downloader.PrintWarning("token.config had more lines than expected");
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				Log.Downloader.PrintError("Could not read token.config file: {0}", ex.ToString());
				return null;
			}
		}

		private IEnumerator<IAsyncJobResult> Job_GetPipelinePortsFromVersionService()
		{
			m_pipelinePortMap.Clear();
			if (!IsEnabledForCurrentPlatform() || !IsPipelinePortNeeded())
			{
				Log.Downloader.PrintError("Attempted to request pipeline information on a platform that does not support/need it");
				yield break;
			}
			VersionPipelineJsonFetcher fetcher = new VersionPipelineJsonFetcher();
			yield return fetcher;
			string pipelineJson = fetcher.PipelineJson;
			if (string.IsNullOrEmpty(pipelineJson))
			{
				Log.Downloader.PrintError("Empty response from version proxy service pipeline list!");
			}
			else
			{
				AddPipelinesToPortMap(ParsePipelineJson(pipelineJson), ref m_pipelinePortMap);
			}
		}

		private static List<JsonPipelineInfo> ParsePipelineJson(string pipelineJsonString)
		{
			List<JsonPipelineInfo> list = new List<JsonPipelineInfo>();
			try
			{
				foreach (JsonNode item in Json.Deserialize(pipelineJsonString) as JsonList)
				{
					JsonPipelineInfo pipelineInfoFromJsonNode = GetPipelineInfoFromJsonNode(item);
					list.Add(pipelineInfoFromJsonNode);
				}
				return list;
			}
			catch (Exception ex)
			{
				Log.Downloader.PrintError("Could not parse version proxy pipeline json: {0}", ex.ToString());
				return new List<JsonPipelineInfo>();
			}
		}

		private static JsonPipelineInfo GetPipelineInfoFromJsonNode(JsonNode pipeline)
		{
			JsonPipelineInfo result = default(JsonPipelineInfo);
			result.ProductName = pipeline["productName"] as string;
			result.PipelineName = pipeline["pipelineName"] as string;
			result.ProxyPort = Convert.ToInt32(pipeline["proxyPort"]);
			return result;
		}

		private static void AddPipelinesToPortMap(List<JsonPipelineInfo> pipelineList, ref Map<VersionPipeline, int> pipelineMap)
		{
			if (pipelineMap == null)
			{
				pipelineMap = new Map<VersionPipeline, int>();
			}
			foreach (JsonPipelineInfo pipeline in pipelineList)
			{
				if (!pipeline.ProductName.Equals("Hearthstone"))
				{
					Log.Downloader.PrintError("Found unexpected product in version proxy pipeline list: {0}", pipeline.ProductName);
					continue;
				}
				if (!PIPELINE_VERSION_MAP.ContainsKey(pipeline.PipelineName))
				{
					Log.Downloader.PrintInfo("Found unsupported pipeline {0} in version port json.", pipeline.PipelineName);
					continue;
				}
				VersionPipeline key = PIPELINE_VERSION_MAP[pipeline.PipelineName];
				Log.Downloader.PrintInfo("Adding port {0} for pipeline {1}", pipeline.ProxyPort, key.ToString());
				pipelineMap[key] = pipeline.ProxyPort;
			}
		}
	}
}
