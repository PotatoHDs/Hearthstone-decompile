using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.Http
{
	public interface IHttpRequest : IDisposable
	{
		bool IsNetworkError { get; }

		bool IsHttpError { get; }

		int TimeoutSeconds { get; set; }

		bool DidTimeout { get; }

		bool IsDone { get; }

		string ErrorString { get; }

		int ResponseStatusCode { get; }

		Dictionary<string, string> ResponseHeaders { get; }

		string ResponseAsString { get; }

		Texture ResponseAsTexture { get; }

		byte[] ResponseRaw { get; }

		AsyncOperation SendRequest();

		void SetRequestHeader(string name, string value);

		void SetRequestHeaders(IEnumerable<KeyValuePair<string, string>> headers);
	}
}
