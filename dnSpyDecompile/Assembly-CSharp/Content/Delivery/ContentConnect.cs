using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Hearthstone;
using Hearthstone.Http;
using MiniJSON;
using UnityEngine;

namespace Content.Delivery
{
	// Token: 0x02000FA2 RID: 4002
	public class ContentConnect
	{
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600AE14 RID: 44564 RVA: 0x00363090 File Offset: 0x00361290
		public static bool ContentstackEnabled
		{
			get
			{
				NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
				return netObject == null || netObject.ContentstackEnabled;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600AE15 RID: 44565 RVA: 0x003630B3 File Offset: 0x003612B3
		public bool Ready
		{
			get
			{
				return !string.IsNullOrEmpty(this.ServiceUrl) && !this.InProcessingQuery && !this.IsCachedDataValid;
			}
		}

		// Token: 0x0600AE16 RID: 44566 RVA: 0x003630D5 File Offset: 0x003612D5
		public void ResetServiceURL(string url)
		{
			this.ServiceUrl = url;
		}

		// Token: 0x0600AE17 RID: 44567 RVA: 0x003630DE File Offset: 0x003612DE
		public static string OptionStringFormat(int age, ulong downloadtime, string sha1OfServiceUrl, string hexResponse)
		{
			return string.Format("{0:D2}:{1}|{2}|{3}|{4}", new object[]
			{
				1,
				age,
				downloadtime,
				sha1OfServiceUrl,
				hexResponse
			});
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x0600AE18 RID: 44568 RVA: 0x00363114 File Offset: 0x00361314
		public int ValidSeconds
		{
			get
			{
				if (this.m_data != null)
				{
					ulong num = TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now) - this.m_data.m_lastDownloadTime;
					if (num < (ulong)((long)this.m_data.m_age))
					{
						return this.m_data.m_age - (int)num;
					}
				}
				return -1;
			}
		}

		// Token: 0x0600AE19 RID: 44569 RVA: 0x0036315F File Offset: 0x0036135F
		public IEnumerator Query(ResponseProcessHandler responseProcessHandler, object param, string query, bool force)
		{
			while (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>() == null)
			{
				yield return null;
			}
			if (!ContentConnect.ContentstackEnabled)
			{
				Log.ContentConnect.PrintDebug("Skip to update because Contentstack is disabled: {0}", new object[]
				{
					this.ContentType
				});
				responseProcessHandler(string.Empty, param);
				yield break;
			}
			this.InProcessingQuery = true;
			if (!force && this.IsCachedDataValid)
			{
				Log.ContentConnect.PrintInfo("{0}, Using cached response: {1}; (will refresh after: {2}s)", new object[]
				{
					this.ContentType,
					this.m_data.m_response,
					this.m_data.m_age
				});
			}
			else
			{
				Log.ContentConnect.PrintDebug("Query: {0}", new object[]
				{
					this.ServiceUrl + query
				});
				IHttpRequest httpRequest = HttpRequestFactory.Get().CreateGetRequest(this.ServiceUrl + query);
				httpRequest.SetRequestHeaders(this.m_headers);
				yield return httpRequest.SendRequest();
				if (httpRequest.IsNetworkError || httpRequest.IsHttpError)
				{
					Debug.LogWarning("Failed to download url in ContentConnect: " + this.ServiceUrl);
					Debug.LogWarning(httpRequest.ErrorString);
					responseProcessHandler(string.Empty, param);
					this.InProcessingQuery = false;
					int errorCodeFromJsonResponse = this.GetErrorCodeFromJsonResponse(httpRequest.ResponseAsString);
					TelemetryManager.Client().SendContentConnectFailedToConnect(this.ServiceUrl, httpRequest.ResponseStatusCode, errorCodeFromJsonResponse);
					yield break;
				}
				string str;
				if (httpRequest.ResponseHeaders.TryGetValue("X-Cache", out str))
				{
					Log.InGameMessage.PrintDebug("Cache response (Shield, Local): " + str, Array.Empty<object>());
				}
				this.m_data.m_response = httpRequest.ResponseAsString;
				Log.ContentConnect.PrintDebug("url text is " + this.m_data.m_response, Array.Empty<object>());
				if (this.m_data.m_response.Length > 4096)
				{
					Log.ContentConnect.PrintWarning("Aborting because of excessively large response:{0} > {1}", new object[]
					{
						this.m_data.m_response.Length,
						4096
					});
					this.m_data.m_response = string.Empty;
				}
				this.m_data.m_age = ((this.m_settings.m_overrideAge == 0) ? ContentConnect.GetCacheAge(httpRequest) : this.m_settings.m_overrideAge);
				this.m_data.m_lastDownloadTime = TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now);
				this.Serialize();
				httpRequest = null;
			}
			responseProcessHandler(this.m_data.m_response, param);
			this.InProcessingQuery = false;
			yield break;
		}

		// Token: 0x0600AE1A RID: 44570 RVA: 0x0036318C File Offset: 0x0036138C
		protected void Init(ContentConnectSettings setting)
		{
			this.m_settings = setting;
			this.ServiceUrl = setting.m_url;
			Log.ContentConnect.Print("Url: " + this.ServiceUrl, Array.Empty<object>());
			this.m_headers["Accept"] = "application/json";
			if (!string.IsNullOrEmpty(this.m_settings.m_apiKey))
			{
				this.m_headers["api_key"] = this.m_settings.m_apiKey;
			}
			if (!string.IsNullOrEmpty(this.m_settings.m_accessToken))
			{
				this.m_headers["access_token"] = this.m_settings.m_accessToken;
			}
			this.Deserialize();
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x0600AE1B RID: 44571 RVA: 0x00363240 File Offset: 0x00361440
		// (set) Token: 0x0600AE1C RID: 44572 RVA: 0x0036324D File Offset: 0x0036144D
		private string ServiceUrl
		{
			get
			{
				return this.m_settings.m_url;
			}
			set
			{
				this.m_settings.m_url = value;
				this.m_data.m_sha1OfServiceUrl = Crypto.SHA1.Calc(value);
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x0600AE1D RID: 44573 RVA: 0x0036326C File Offset: 0x0036146C
		private string ContentType
		{
			get
			{
				return this.m_settings.m_contentType;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x0600AE1E RID: 44574 RVA: 0x00363279 File Offset: 0x00361479
		// (set) Token: 0x0600AE1F RID: 44575 RVA: 0x00363281 File Offset: 0x00361481
		private bool InProcessingQuery { get; set; }

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x0600AE20 RID: 44576 RVA: 0x0036328C File Offset: 0x0036148C
		private bool IsCachedDataValid
		{
			get
			{
				int validSeconds = this.ValidSeconds;
				if (validSeconds > 0 && !string.IsNullOrEmpty(this.m_data.m_response))
				{
					Log.ContentConnect.PrintDebug("{0}, still valid within {1}s", new object[]
					{
						this.ContentType,
						validSeconds
					});
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600AE21 RID: 44577 RVA: 0x003632E0 File Offset: 0x003614E0
		private static int GetCacheAge(IHttpRequest httpRequest)
		{
			string empty = string.Empty;
			Dictionary<string, string> responseHeaders = httpRequest.ResponseHeaders;
			if (responseHeaders != null && (responseHeaders.TryGetValue("CACHE-CONTROL", out empty) || responseHeaders.TryGetValue("Strict-Transport-Security", out empty)))
			{
				string[] array = empty.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i].ToLowerInvariant().Trim();
					if (text.StartsWith("max-age"))
					{
						string[] array2 = text.Split(new char[]
						{
							'='
						});
						int result;
						if (array2.Length == 2 && int.TryParse(array2[1], out result))
						{
							return result;
						}
					}
				}
			}
			return 0;
		}

		// Token: 0x0600AE22 RID: 44578 RVA: 0x00363388 File Offset: 0x00361588
		private void Deserialize()
		{
			Option option;
			if (Enum.TryParse<Option>(this.m_settings.m_saveLocation, out option))
			{
				string @string = Options.Get().GetString(option);
				if (string.IsNullOrEmpty(@string))
				{
					return;
				}
				int num = 0;
				int num2 = 1;
				int num3 = 3;
				string[] array;
				if (@string[2] == ':')
				{
					string text = @string.Substring(0, 2);
					int num4;
					if (!int.TryParse(text, out num4) || num4 != 1)
					{
						Log.ContentConnect.PrintDebug("Unknown content version: {0}", new object[]
						{
							text
						});
						return;
					}
					array = @string.Substring(3).Split(new char[]
					{
						'|'
					});
				}
				else
				{
					array = @string.Split(new char[]
					{
						'|'
					});
				}
				if (array.Length > 3)
				{
					if (!int.TryParse(array[num], out this.m_data.m_age))
					{
						Debug.LogWarningFormat("Failed to convert Age to int: {0}", new object[]
						{
							array[num]
						});
					}
					if (array.Length > num2 && !ulong.TryParse(array[num2], out this.m_data.m_lastDownloadTime))
					{
						Debug.LogWarningFormat("Failed to convert LastDownloadTime to ulong: {0}", new object[]
						{
							array[num2]
						});
					}
					this.m_data.m_response = TextUtils.FromHexString(array[num3]);
				}
			}
			else if (File.Exists(this.m_settings.m_saveLocation))
			{
				try
				{
					string json = File.ReadAllText(this.m_settings.m_saveLocation);
					this.m_data = JsonUtility.FromJson<ContentConnectData>(json);
					this.TryMigrationData();
				}
				catch (Exception ex)
				{
					Log.ContentConnect.PrintError("Cannot read the data from '{0}': {1}", new object[]
					{
						this.m_settings.m_saveLocation,
						ex.Message
					});
				}
			}
			this.m_data.m_age = 0;
		}

		// Token: 0x0600AE23 RID: 44579 RVA: 0x0036353C File Offset: 0x0036173C
		private void TryMigrationData()
		{
			int dataVersion = this.m_data.m_dataVersion;
		}

		// Token: 0x0600AE24 RID: 44580 RVA: 0x0036354C File Offset: 0x0036174C
		private void Serialize()
		{
			if (string.IsNullOrEmpty(this.m_data.m_response))
			{
				return;
			}
			Option option;
			if (Enum.TryParse<Option>(this.m_settings.m_saveLocation, out option))
			{
				Options.Get().SetString(option, ContentConnect.OptionStringFormat(this.m_data.m_age, this.m_data.m_lastDownloadTime, this.m_data.m_sha1OfServiceUrl, TextUtils.ToHexString(this.m_data.m_response)));
				return;
			}
			try
			{
				string contents = JsonUtility.ToJson(this.m_data, !HearthstoneApplication.IsPublic());
				File.WriteAllText(this.m_settings.m_saveLocation, contents);
			}
			catch (Exception ex)
			{
				Log.ContentConnect.PrintError("Cannot write the data to '{0}': {1}", new object[]
				{
					this.m_settings.m_saveLocation,
					ex.Message
				});
			}
		}

		// Token: 0x0600AE25 RID: 44581 RVA: 0x0036362C File Offset: 0x0036182C
		private int GetErrorCodeFromJsonResponse(string response)
		{
			try
			{
				JsonNode jsonNode = Json.Deserialize(response) as JsonNode;
				if (jsonNode.ContainsKey("error_code"))
				{
					return (int)Convert.ChangeType(jsonNode["error_code"], typeof(int));
				}
			}
			catch (Exception ex)
			{
				Log.ContentConnect.PrintError("Failed to parse the response: {0}\n'{1}'", new object[]
				{
					ex.Message,
					response
				});
			}
			return 0;
		}

		// Token: 0x040094E0 RID: 38112
		private ContentConnectData m_data = new ContentConnectData();

		// Token: 0x040094E1 RID: 38113
		private Dictionary<string, string> m_headers = new Dictionary<string, string>();

		// Token: 0x040094E2 RID: 38114
		private ContentConnectSettings m_settings;
	}
}
