using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Hearthstone.Http
{
	public class HttpRequest : IHttpRequest, IDisposable
	{
		private readonly UnityWebRequest m_unityRequest;

		private Dictionary<string, string> m_responseHeaders;

		public bool IsNetworkError => m_unityRequest.isNetworkError;

		public bool IsHttpError => m_unityRequest.isHttpError;

		public bool IsDone => m_unityRequest.isDone;

		public int TimeoutSeconds
		{
			get
			{
				return m_unityRequest.timeout;
			}
			set
			{
				m_unityRequest.timeout = value;
			}
		}

		public bool DidTimeout => string.Compare("Request timeout", m_unityRequest.error, ignoreCase: true) == 0;

		public string ErrorString => m_unityRequest.error;

		public int ResponseStatusCode => (int)m_unityRequest.responseCode;

		public Dictionary<string, string> ResponseHeaders
		{
			get
			{
				if (m_responseHeaders == null)
				{
					m_responseHeaders = m_unityRequest.GetResponseHeaders();
				}
				return m_responseHeaders;
			}
		}

		public string ResponseAsString
		{
			get
			{
				byte[] data = m_unityRequest.downloadHandler.data;
				return Encoding.UTF8.GetString(data);
			}
		}

		public Texture ResponseAsTexture => DownloadHandlerTexture.GetContent(m_unityRequest);

		public byte[] ResponseRaw => m_unityRequest.downloadHandler.data;

		public static IHttpRequest Post(string uri, WWWForm form)
		{
			return new HttpRequest(UnityWebRequest.Post(uri, form));
		}

		public static IHttpRequest Post(string uri, string body)
		{
			return new HttpRequest(UnityWebRequest.Post(uri, body));
		}

		public static IHttpRequest Post(string uri, byte[] body)
		{
			UnityWebRequest unityWebRequest = UnityWebRequest.Put(new Uri(uri), body);
			unityWebRequest.method = "POST";
			return new HttpRequest(unityWebRequest);
		}

		public static IHttpRequest Get(string uri)
		{
			return new HttpRequest(UnityWebRequest.Get(uri));
		}

		public static IHttpRequest GetTexture(string uri)
		{
			return new HttpRequest(UnityWebRequestTexture.GetTexture(uri));
		}

		public static IHttpRequest Delete(string uri)
		{
			UnityWebRequest unityWebRequest = UnityWebRequest.Delete(uri);
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			return new HttpRequest(unityWebRequest);
		}

		public static IHttpRequest Put(string uri, string body)
		{
			return new HttpRequest(UnityWebRequest.Put(uri, body));
		}

		public void SetRequestHeader(string name, string value)
		{
			m_unityRequest.SetRequestHeader(name, value);
		}

		public void SetRequestHeaders(IEnumerable<KeyValuePair<string, string>> headers)
		{
			foreach (KeyValuePair<string, string> header in headers)
			{
				m_unityRequest.SetRequestHeader(header.Key, header.Value);
			}
		}

		public AsyncOperation SendRequest()
		{
			return m_unityRequest.SendWebRequest();
		}

		public void Dispose()
		{
			m_unityRequest.Dispose();
			m_responseHeaders = null;
		}

		private HttpRequest(UnityWebRequest unityRequest)
		{
			m_unityRequest = unityRequest;
		}
	}
}
