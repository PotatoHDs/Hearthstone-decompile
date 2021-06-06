using System;
using UnityEngine;

namespace Hearthstone.Http
{
	// Token: 0x02001127 RID: 4391
	public class HttpRequestFactory : IHttpRequestFactory
	{
		// Token: 0x0600C08F RID: 49295 RVA: 0x003AA9F4 File Offset: 0x003A8BF4
		public static HttpRequestFactory Get()
		{
			if (HttpRequestFactory.s_instance == null)
			{
				HttpRequestFactory.s_instance = new HttpRequestFactory();
			}
			return HttpRequestFactory.s_instance;
		}

		// Token: 0x0600C090 RID: 49296 RVA: 0x003AAA0C File Offset: 0x003A8C0C
		public IHttpRequest CreatePostRequest(string uri, WWWForm form)
		{
			return HttpRequest.Post(uri, form);
		}

		// Token: 0x0600C091 RID: 49297 RVA: 0x003AAA15 File Offset: 0x003A8C15
		public IHttpRequest CreatePostRequest(string uri, string body)
		{
			return HttpRequest.Post(uri, body);
		}

		// Token: 0x0600C092 RID: 49298 RVA: 0x003AAA1E File Offset: 0x003A8C1E
		public IHttpRequest CreatePostRequest(string uri, byte[] body)
		{
			return HttpRequest.Post(uri, body);
		}

		// Token: 0x0600C093 RID: 49299 RVA: 0x003AAA27 File Offset: 0x003A8C27
		public IHttpRequest CreateGetRequest(string uri)
		{
			return HttpRequest.Get(uri);
		}

		// Token: 0x0600C094 RID: 49300 RVA: 0x003AAA2F File Offset: 0x003A8C2F
		public IHttpRequest CreateGetTextureRequest(string uri)
		{
			return HttpRequest.GetTexture(uri);
		}

		// Token: 0x0600C095 RID: 49301 RVA: 0x003AAA37 File Offset: 0x003A8C37
		public IHttpRequest CreateDeleteRequest(string uri)
		{
			return HttpRequest.Delete(uri);
		}

		// Token: 0x0600C096 RID: 49302 RVA: 0x003AAA3F File Offset: 0x003A8C3F
		public IHttpRequest CreatePutRequest(string uri, string body)
		{
			return HttpRequest.Put(uri, body);
		}

		// Token: 0x0600C097 RID: 49303 RVA: 0x000052CE File Offset: 0x000034CE
		private HttpRequestFactory()
		{
		}

		// Token: 0x04009BF0 RID: 39920
		private static HttpRequestFactory s_instance;
	}
}
