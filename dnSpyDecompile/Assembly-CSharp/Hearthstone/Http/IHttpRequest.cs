using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.Http
{
	// Token: 0x02001128 RID: 4392
	public interface IHttpRequest : IDisposable
	{
		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x0600C099 RID: 49305
		bool IsNetworkError { get; }

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x0600C09A RID: 49306
		bool IsHttpError { get; }

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x0600C09B RID: 49307
		// (set) Token: 0x0600C09C RID: 49308
		int TimeoutSeconds { get; set; }

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x0600C09D RID: 49309
		bool DidTimeout { get; }

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x0600C09E RID: 49310
		bool IsDone { get; }

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x0600C09F RID: 49311
		string ErrorString { get; }

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x0600C0A0 RID: 49312
		int ResponseStatusCode { get; }

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x0600C0A1 RID: 49313
		Dictionary<string, string> ResponseHeaders { get; }

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x0600C0A2 RID: 49314
		string ResponseAsString { get; }

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x0600C0A3 RID: 49315
		Texture ResponseAsTexture { get; }

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x0600C0A4 RID: 49316
		byte[] ResponseRaw { get; }

		// Token: 0x0600C0A5 RID: 49317
		AsyncOperation SendRequest();

		// Token: 0x0600C0A6 RID: 49318
		void SetRequestHeader(string name, string value);

		// Token: 0x0600C0A7 RID: 49319
		void SetRequestHeaders(IEnumerable<KeyValuePair<string, string>> headers);
	}
}
