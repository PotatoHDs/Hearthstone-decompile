using UnityEngine;

namespace Hearthstone.Http
{
	public class HttpRequestFactory : IHttpRequestFactory
	{
		private static HttpRequestFactory s_instance;

		public static HttpRequestFactory Get()
		{
			if (s_instance == null)
			{
				s_instance = new HttpRequestFactory();
			}
			return s_instance;
		}

		public IHttpRequest CreatePostRequest(string uri, WWWForm form)
		{
			return HttpRequest.Post(uri, form);
		}

		public IHttpRequest CreatePostRequest(string uri, string body)
		{
			return HttpRequest.Post(uri, body);
		}

		public IHttpRequest CreatePostRequest(string uri, byte[] body)
		{
			return HttpRequest.Post(uri, body);
		}

		public IHttpRequest CreateGetRequest(string uri)
		{
			return HttpRequest.Get(uri);
		}

		public IHttpRequest CreateGetTextureRequest(string uri)
		{
			return HttpRequest.GetTexture(uri);
		}

		public IHttpRequest CreateDeleteRequest(string uri)
		{
			return HttpRequest.Delete(uri);
		}

		public IHttpRequest CreatePutRequest(string uri, string body)
		{
			return HttpRequest.Put(uri, body);
		}

		private HttpRequestFactory()
		{
		}
	}
}
