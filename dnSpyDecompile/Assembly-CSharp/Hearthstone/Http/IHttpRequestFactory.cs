using System;
using UnityEngine;

namespace Hearthstone.Http
{
	// Token: 0x02001129 RID: 4393
	public interface IHttpRequestFactory
	{
		// Token: 0x0600C0A8 RID: 49320
		IHttpRequest CreatePostRequest(string uri, WWWForm form);

		// Token: 0x0600C0A9 RID: 49321
		IHttpRequest CreatePostRequest(string uri, string body);

		// Token: 0x0600C0AA RID: 49322
		IHttpRequest CreateGetRequest(string uri);

		// Token: 0x0600C0AB RID: 49323
		IHttpRequest CreateGetTextureRequest(string uri);

		// Token: 0x0600C0AC RID: 49324
		IHttpRequest CreateDeleteRequest(string uri);

		// Token: 0x0600C0AD RID: 49325
		IHttpRequest CreatePutRequest(string uri, string body);
	}
}
