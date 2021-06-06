using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Hearthstone.Http
{
	// Token: 0x02001126 RID: 4390
	public class HttpRequest : IHttpRequest, IDisposable
	{
		// Token: 0x0600C077 RID: 49271 RVA: 0x003AA7FC File Offset: 0x003A89FC
		public static IHttpRequest Post(string uri, WWWForm form)
		{
			return new HttpRequest(UnityWebRequest.Post(uri, form));
		}

		// Token: 0x0600C078 RID: 49272 RVA: 0x003AA80A File Offset: 0x003A8A0A
		public static IHttpRequest Post(string uri, string body)
		{
			return new HttpRequest(UnityWebRequest.Post(uri, body));
		}

		// Token: 0x0600C079 RID: 49273 RVA: 0x003AA818 File Offset: 0x003A8A18
		public static IHttpRequest Post(string uri, byte[] body)
		{
			UnityWebRequest unityWebRequest = UnityWebRequest.Put(new Uri(uri), body);
			unityWebRequest.method = "POST";
			return new HttpRequest(unityWebRequest);
		}

		// Token: 0x0600C07A RID: 49274 RVA: 0x003AA836 File Offset: 0x003A8A36
		public static IHttpRequest Get(string uri)
		{
			return new HttpRequest(UnityWebRequest.Get(uri));
		}

		// Token: 0x0600C07B RID: 49275 RVA: 0x003AA843 File Offset: 0x003A8A43
		public static IHttpRequest GetTexture(string uri)
		{
			return new HttpRequest(UnityWebRequestTexture.GetTexture(uri));
		}

		// Token: 0x0600C07C RID: 49276 RVA: 0x003AA850 File Offset: 0x003A8A50
		public static IHttpRequest Delete(string uri)
		{
			UnityWebRequest unityWebRequest = UnityWebRequest.Delete(uri);
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			return new HttpRequest(unityWebRequest);
		}

		// Token: 0x0600C07D RID: 49277 RVA: 0x003AA868 File Offset: 0x003A8A68
		public static IHttpRequest Put(string uri, string body)
		{
			return new HttpRequest(UnityWebRequest.Put(uri, body));
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x0600C07E RID: 49278 RVA: 0x003AA876 File Offset: 0x003A8A76
		public bool IsNetworkError
		{
			get
			{
				return this.m_unityRequest.isNetworkError;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x0600C07F RID: 49279 RVA: 0x003AA883 File Offset: 0x003A8A83
		public bool IsHttpError
		{
			get
			{
				return this.m_unityRequest.isHttpError;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x0600C080 RID: 49280 RVA: 0x003AA890 File Offset: 0x003A8A90
		public bool IsDone
		{
			get
			{
				return this.m_unityRequest.isDone;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x0600C081 RID: 49281 RVA: 0x003AA89D File Offset: 0x003A8A9D
		// (set) Token: 0x0600C082 RID: 49282 RVA: 0x003AA8AA File Offset: 0x003A8AAA
		public int TimeoutSeconds
		{
			get
			{
				return this.m_unityRequest.timeout;
			}
			set
			{
				this.m_unityRequest.timeout = value;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x0600C083 RID: 49283 RVA: 0x003AA8B8 File Offset: 0x003A8AB8
		public bool DidTimeout
		{
			get
			{
				return string.Compare("Request timeout", this.m_unityRequest.error, true) == 0;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x0600C084 RID: 49284 RVA: 0x003AA8D3 File Offset: 0x003A8AD3
		public string ErrorString
		{
			get
			{
				return this.m_unityRequest.error;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x0600C085 RID: 49285 RVA: 0x003AA8E0 File Offset: 0x003A8AE0
		public int ResponseStatusCode
		{
			get
			{
				return (int)this.m_unityRequest.responseCode;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x0600C086 RID: 49286 RVA: 0x003AA8EE File Offset: 0x003A8AEE
		public Dictionary<string, string> ResponseHeaders
		{
			get
			{
				if (this.m_responseHeaders == null)
				{
					this.m_responseHeaders = this.m_unityRequest.GetResponseHeaders();
				}
				return this.m_responseHeaders;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x0600C087 RID: 49287 RVA: 0x003AA910 File Offset: 0x003A8B10
		public string ResponseAsString
		{
			get
			{
				byte[] data = this.m_unityRequest.downloadHandler.data;
				return Encoding.UTF8.GetString(data);
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x0600C088 RID: 49288 RVA: 0x003AA939 File Offset: 0x003A8B39
		public Texture ResponseAsTexture
		{
			get
			{
				return DownloadHandlerTexture.GetContent(this.m_unityRequest);
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x0600C089 RID: 49289 RVA: 0x003AA946 File Offset: 0x003A8B46
		public byte[] ResponseRaw
		{
			get
			{
				return this.m_unityRequest.downloadHandler.data;
			}
		}

		// Token: 0x0600C08A RID: 49290 RVA: 0x003AA958 File Offset: 0x003A8B58
		public void SetRequestHeader(string name, string value)
		{
			this.m_unityRequest.SetRequestHeader(name, value);
		}

		// Token: 0x0600C08B RID: 49291 RVA: 0x003AA968 File Offset: 0x003A8B68
		public void SetRequestHeaders(IEnumerable<KeyValuePair<string, string>> headers)
		{
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				this.m_unityRequest.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600C08C RID: 49292 RVA: 0x003AA9C4 File Offset: 0x003A8BC4
		public AsyncOperation SendRequest()
		{
			return this.m_unityRequest.SendWebRequest();
		}

		// Token: 0x0600C08D RID: 49293 RVA: 0x003AA9D1 File Offset: 0x003A8BD1
		public void Dispose()
		{
			this.m_unityRequest.Dispose();
			this.m_responseHeaders = null;
		}

		// Token: 0x0600C08E RID: 49294 RVA: 0x003AA9E5 File Offset: 0x003A8BE5
		private HttpRequest(UnityWebRequest unityRequest)
		{
			this.m_unityRequest = unityRequest;
		}

		// Token: 0x04009BEE RID: 39918
		private readonly UnityWebRequest m_unityRequest;

		// Token: 0x04009BEF RID: 39919
		private Dictionary<string, string> m_responseHeaders;
	}
}
