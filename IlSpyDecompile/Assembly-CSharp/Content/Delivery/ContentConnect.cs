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
	public class ContentConnect
	{
		private ContentConnectData m_data = new ContentConnectData();

		private Dictionary<string, string> m_headers = new Dictionary<string, string>();

		private ContentConnectSettings m_settings;

		public static bool ContentstackEnabled => NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>()?.ContentstackEnabled ?? true;

		public bool Ready
		{
			get
			{
				if (!string.IsNullOrEmpty(ServiceUrl) && !InProcessingQuery)
				{
					return !IsCachedDataValid;
				}
				return false;
			}
		}

		public int ValidSeconds
		{
			get
			{
				if (m_data != null)
				{
					ulong num = TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now) - m_data.m_lastDownloadTime;
					if (num < (ulong)m_data.m_age)
					{
						return m_data.m_age - (int)num;
					}
				}
				return -1;
			}
		}

		private string ServiceUrl
		{
			get
			{
				return m_settings.m_url;
			}
			set
			{
				m_settings.m_url = value;
				m_data.m_sha1OfServiceUrl = Crypto.SHA1.Calc(value);
			}
		}

		private string ContentType => m_settings.m_contentType;

		private bool InProcessingQuery { get; set; }

		private bool IsCachedDataValid
		{
			get
			{
				int validSeconds = ValidSeconds;
				if (validSeconds > 0 && !string.IsNullOrEmpty(m_data.m_response))
				{
					Log.ContentConnect.PrintDebug("{0}, still valid within {1}s", ContentType, validSeconds);
					return true;
				}
				return false;
			}
		}

		public void ResetServiceURL(string url)
		{
			ServiceUrl = url;
		}

		public static string OptionStringFormat(int age, ulong downloadtime, string sha1OfServiceUrl, string hexResponse)
		{
			return $"{1:D2}:{age}|{downloadtime}|{sha1OfServiceUrl}|{hexResponse}";
		}

		public IEnumerator Query(ResponseProcessHandler responseProcessHandler, object param, string query, bool force)
		{
			while (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>() == null)
			{
				yield return null;
			}
			if (!ContentstackEnabled)
			{
				Log.ContentConnect.PrintDebug("Skip to update because Contentstack is disabled: {0}", ContentType);
				responseProcessHandler(string.Empty, param);
				yield break;
			}
			InProcessingQuery = true;
			if (!force && IsCachedDataValid)
			{
				Log.ContentConnect.PrintInfo("{0}, Using cached response: {1}; (will refresh after: {2}s)", ContentType, m_data.m_response, m_data.m_age);
			}
			else
			{
				Log.ContentConnect.PrintDebug("Query: {0}", ServiceUrl + query);
				IHttpRequest httpRequest = HttpRequestFactory.Get().CreateGetRequest(ServiceUrl + query);
				httpRequest.SetRequestHeaders(m_headers);
				yield return httpRequest.SendRequest();
				if (httpRequest.IsNetworkError || httpRequest.IsHttpError)
				{
					Debug.LogWarning("Failed to download url in ContentConnect: " + ServiceUrl);
					Debug.LogWarning(httpRequest.ErrorString);
					responseProcessHandler(string.Empty, param);
					InProcessingQuery = false;
					int errorCodeFromJsonResponse = GetErrorCodeFromJsonResponse(httpRequest.ResponseAsString);
					TelemetryManager.Client().SendContentConnectFailedToConnect(ServiceUrl, httpRequest.ResponseStatusCode, errorCodeFromJsonResponse);
					yield break;
				}
				if (httpRequest.ResponseHeaders.TryGetValue("X-Cache", out var value))
				{
					Log.InGameMessage.PrintDebug("Cache response (Shield, Local): " + value);
				}
				m_data.m_response = httpRequest.ResponseAsString;
				Log.ContentConnect.PrintDebug("url text is " + m_data.m_response);
				if (m_data.m_response.Length > 4096)
				{
					Log.ContentConnect.PrintWarning("Aborting because of excessively large response:{0} > {1}", m_data.m_response.Length, 4096);
					m_data.m_response = string.Empty;
				}
				m_data.m_age = ((m_settings.m_overrideAge == 0) ? GetCacheAge(httpRequest) : m_settings.m_overrideAge);
				m_data.m_lastDownloadTime = TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now);
				Serialize();
			}
			responseProcessHandler(m_data.m_response, param);
			InProcessingQuery = false;
		}

		protected void Init(ContentConnectSettings setting)
		{
			m_settings = setting;
			ServiceUrl = setting.m_url;
			Log.ContentConnect.Print("Url: " + ServiceUrl);
			m_headers["Accept"] = "application/json";
			if (!string.IsNullOrEmpty(m_settings.m_apiKey))
			{
				m_headers["api_key"] = m_settings.m_apiKey;
			}
			if (!string.IsNullOrEmpty(m_settings.m_accessToken))
			{
				m_headers["access_token"] = m_settings.m_accessToken;
			}
			Deserialize();
		}

		private static int GetCacheAge(IHttpRequest httpRequest)
		{
			string value = string.Empty;
			Dictionary<string, string> responseHeaders = httpRequest.ResponseHeaders;
			if (responseHeaders != null && (responseHeaders.TryGetValue("CACHE-CONTROL", out value) || responseHeaders.TryGetValue("Strict-Transport-Security", out value)))
			{
				string[] array = value.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i].ToLowerInvariant().Trim();
					if (text.StartsWith("max-age"))
					{
						string[] array2 = text.Split('=');
						if (array2.Length == 2 && int.TryParse(array2[1], out var result))
						{
							return result;
						}
					}
				}
			}
			return 0;
		}

		private void Deserialize()
		{
			if (Enum.TryParse<Option>(m_settings.m_saveLocation, out var result))
			{
				string @string = Options.Get().GetString(result);
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
					if (!int.TryParse(text, out var result2) || result2 != 1)
					{
						Log.ContentConnect.PrintDebug("Unknown content version: {0}", text);
						return;
					}
					array = @string.Substring(3).Split('|');
				}
				else
				{
					array = @string.Split('|');
				}
				if (array.Length > 3)
				{
					if (!int.TryParse(array[num], out m_data.m_age))
					{
						Debug.LogWarningFormat("Failed to convert Age to int: {0}", array[num]);
					}
					if (array.Length > num2 && !ulong.TryParse(array[num2], out m_data.m_lastDownloadTime))
					{
						Debug.LogWarningFormat("Failed to convert LastDownloadTime to ulong: {0}", array[num2]);
					}
					m_data.m_response = TextUtils.FromHexString(array[num3]);
				}
			}
			else if (File.Exists(m_settings.m_saveLocation))
			{
				try
				{
					string json = File.ReadAllText(m_settings.m_saveLocation);
					m_data = JsonUtility.FromJson<ContentConnectData>(json);
					TryMigrationData();
				}
				catch (Exception ex)
				{
					Log.ContentConnect.PrintError("Cannot read the data from '{0}': {1}", m_settings.m_saveLocation, ex.Message);
				}
			}
			m_data.m_age = 0;
		}

		private void TryMigrationData()
		{
			_ = m_data.m_dataVersion;
			_ = 1;
		}

		private void Serialize()
		{
			if (string.IsNullOrEmpty(m_data.m_response))
			{
				return;
			}
			if (Enum.TryParse<Option>(m_settings.m_saveLocation, out var result))
			{
				Options.Get().SetString(result, OptionStringFormat(m_data.m_age, m_data.m_lastDownloadTime, m_data.m_sha1OfServiceUrl, TextUtils.ToHexString(m_data.m_response)));
				return;
			}
			try
			{
				string contents = JsonUtility.ToJson(m_data, !HearthstoneApplication.IsPublic());
				File.WriteAllText(m_settings.m_saveLocation, contents);
			}
			catch (Exception ex)
			{
				Log.ContentConnect.PrintError("Cannot write the data to '{0}': {1}", m_settings.m_saveLocation, ex.Message);
			}
		}

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
				Log.ContentConnect.PrintError("Failed to parse the response: {0}\n'{1}'", ex.Message, response);
			}
			return 0;
		}
	}
}
