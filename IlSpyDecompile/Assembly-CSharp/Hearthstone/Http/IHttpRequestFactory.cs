using UnityEngine;

namespace Hearthstone.Http
{
	public interface IHttpRequestFactory
	{
		IHttpRequest CreatePostRequest(string uri, WWWForm form);

		IHttpRequest CreatePostRequest(string uri, string body);

		IHttpRequest CreateGetRequest(string uri);

		IHttpRequest CreateGetTextureRequest(string uri);

		IHttpRequest CreateDeleteRequest(string uri);

		IHttpRequest CreatePutRequest(string uri, string body);
	}
}
